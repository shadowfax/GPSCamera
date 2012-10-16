Imports Microsoft.VisualBasic.CompilerServices
Imports System.Net.WebClient

Namespace Client.Download

    Public Class ClientDownload
        Inherits System.Net.WebClient

#Region "        Variables "

        <System.Runtime.CompilerServices.AccessedThroughProperty("m_Timer")> _
        Private _m_Timer As System.Timers.Timer
        Private m_Data As Byte()
        Private m_DataSize As Long
        Private m_FileName As String
        Private m_IsCancelled As Boolean
        Private m_IsDownloaded As Boolean
        Private m_Kbs As Single
        Private m_Name As String
        Private m_Percent As Integer
        Private m_RecivedSize As Long
        Private m_Second As Integer
        Private m_Timeout As Integer
        Private m_TotalSecond As Integer
        Private m_TryCount As Integer
        Private m_TryLimit As Integer
        Private m_Uri As Uri

        Private DownloadCompletedEvent As DownloadCompletedEventHandler
        Private DownloadErrorEvent As DownloadErrorEventHandler
        Private ElapsedEvent As System.Timers.ElapsedEventHandler

#End Region

#Region "        Delegates "

        Public Delegate Sub DownloadCompletedEventHandler(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs)
        Public Delegate Sub DownloadErrorEventHandler(ByVal sender As Object, ByVal e As Exception)
        'Public Delegate Sub ElapsedEventHandler(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)

#End Region

#Region "        Events "

        Public Custom Event DownloadCompleted As DownloadCompletedEventHandler
            AddHandler(ByVal value As DownloadCompletedEventHandler)
                Me.DownloadCompletedEvent = DirectCast(System.Delegate.Combine(Me.DownloadCompletedEvent, value), DownloadCompletedEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As DownloadCompletedEventHandler)
                Me.DownloadCompletedEvent = DirectCast(System.Delegate.Remove(Me.DownloadCompletedEvent, value), DownloadCompletedEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs)
                Me.DownloadCompletedEvent.Invoke(sender, e)
            End RaiseEvent
        End Event

        Public Custom Event DownloadError As DownloadErrorEventHandler
            AddHandler(ByVal value As DownloadErrorEventHandler)
                Me.DownloadErrorEvent = DirectCast(System.Delegate.Combine(Me.DownloadErrorEvent, value), DownloadErrorEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As DownloadErrorEventHandler)
                Me.DownloadErrorEvent = DirectCast(System.Delegate.Remove(Me.DownloadErrorEvent, value), DownloadErrorEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As System.Exception)
                Me.DownloadErrorEvent.Invoke(sender, e)
            End RaiseEvent
        End Event

        Public Custom Event Elapsed As System.Timers.ElapsedEventHandler
            AddHandler(ByVal value As System.Timers.ElapsedEventHandler)
                Me.ElapsedEvent = DirectCast(System.Delegate.Combine(Me.ElapsedEvent, value), System.Timers.ElapsedEventHandler)
            End AddHandler

            RemoveHandler(ByVal value As System.Timers.ElapsedEventHandler)
                Me.ElapsedEvent = DirectCast(System.Delegate.Remove(Me.ElapsedEvent, value), System.Timers.ElapsedEventHandler)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
                Me.ElapsedEvent.Invoke(sender, e)
            End RaiseEvent
        End Event

#End Region

        ' Methods
        Public Sub New()
            AddHandler MyBase.DownloadDataCompleted, New System.Net.DownloadDataCompletedEventHandler(AddressOf Me.ClientDownload_DownloadDataCompleted)
            AddHandler DownloadError, New DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
            AddHandler MyBase.DownloadProgressChanged, New System.Net.DownloadProgressChangedEventHandler(AddressOf Me.ClientDownload_DownloadProgressChanged)
            Me.m_Timer = New System.Timers.Timer()
            Me.Initionlize()
        End Sub

        Public Sub New(ByVal pName As String)
            AddHandler MyBase.DownloadDataCompleted, New System.Net.DownloadDataCompletedEventHandler(AddressOf Me.ClientDownload_DownloadDataCompleted)
            AddHandler DownloadError, New DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
            AddHandler MyBase.DownloadProgressChanged, New System.Net.DownloadProgressChangedEventHandler(AddressOf Me.ClientDownload_DownloadProgressChanged)
            Me.m_Timer = New System.Timers.Timer()
            Me.Name = pName
            Me.Initionlize()
        End Sub

        Private Sub ClientDownload_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs)
            Dim downloadErrorEvent As DownloadErrorEventHandler
            Try
                Me.m_IsCancelled = e.Cancelled
                If (Not e.Error Is Nothing) Then
                    downloadErrorEvent = Me.DownloadErrorEvent
                    If (Not downloadErrorEvent Is Nothing) Then
                        downloadErrorEvent.Invoke(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(sender), e.Error)
                    End If
                End If
                If ((e.Result.Length > 0) And (e.Result.Length >= Me.m_DataSize)) Then
                    Me.m_Timer.Stop()
                    Me.m_Second = 0
                    Me.m_IsDownloaded = True
                    Me.m_Data = e.Result
                    Dim downloadCompletedEvent As DownloadCompletedEventHandler = Me.DownloadCompletedEvent
                    If (Not downloadCompletedEvent Is Nothing) Then
                        downloadCompletedEvent.Invoke(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(sender), e)
                    End If
                Else
                    downloadErrorEvent = Me.DownloadErrorEvent
                    If (Not downloadErrorEvent Is Nothing) Then
                        downloadErrorEvent.Invoke(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(sender), New System.Net.HttpListenerException)
                    End If
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(sender), exception)
                End If
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Private Sub ClientDownload_DownloadError(ByVal sender As Object, ByVal e As Exception)
            Me.m_Data = Nothing
            Me.m_IsDownloaded = False
        End Sub

        Private Sub ClientDownload_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)
            Me.m_TotalSecond = (Me.m_TotalSecond + Me.Second)
            If (Me.Second > 0) Then
                Me.m_Kbs = CSng((Math.Round(CDbl(((CDbl(e.BytesReceived) / 1024) / CDbl(Me.Second)))) / 10))
            End If
            Me.m_Percent = e.ProgressPercentage
            Me.m_RecivedSize = e.BytesReceived
            Me.m_DataSize = e.TotalBytesToReceive
            Me.m_Second = 0
        End Sub

        Public Sub DownloadAsync()
            Dim downloadErrorEvent As DownloadErrorEventHandler
            Try
                Me.Initionlize()
                Me.m_Timer.Start()
                MyBase.DownloadDataAsync(Me.Url)
            Catch exception1 As System.Net.WebException
                ProjectData.SetProjectError(exception1)
                Dim e As System.Net.WebException = exception1
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, e)
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, exception2)
                End If
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Public Sub DownloadAsync(ByVal pAddress As Uri)
            Dim downloadErrorEvent As DownloadErrorEventHandler
            Try
                Me.Url = pAddress
                Me.Initionlize()
                Me.m_Timer.Start()
                MyBase.DownloadDataAsync(Me.Url)
            Catch exception1 As System.Net.WebException
                ProjectData.SetProjectError(exception1)
                Dim e As System.Net.WebException = exception1
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, e)
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, exception2)
                End If
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Public Sub DownloadCancelAsync()
            Dim downloadErrorEvent As DownloadErrorEventHandler
            Try
                Me.m_Timer.Stop()
                Me.m_Second = 0
                MyBase.CancelAsync()
            Catch exception1 As System.Net.WebException
                ProjectData.SetProjectError(exception1)
                Dim e As System.Net.WebException = exception1
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, e)
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, exception2)
                End If
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Public Sub Initionlize()
            Me.m_Timer.Stop()
            Me.m_Timer.Interval = 1000
            Me.m_Second = 0
            Me.m_Timeout = 180
            Me.m_TryLimit = 5
            Me.m_Data = Nothing
            Me.m_DataSize = 0
            Me.m_RecivedSize = 0
            Me.m_IsDownloaded = False
            Me.m_IsCancelled = False
            Me.m_Percent = 0
        End Sub

        Private Sub m_Timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
            Dim downloadErrorEvent As DownloadErrorEventHandler
            Try
                If (Me.m_TryCount > Me.m_TryLimit) Then
                    Me.DownloadCancelAsync()
                    Me.m_IsCancelled = True
                    Throw New TimeoutException
                End If
                If (Me.m_Second >= Me.m_Timeout) Then
                    Me.DownloadCancelAsync()
                    System.Threading.Thread.SpinWait(&H1388)
                    Me.m_Data = Nothing
                    Me.m_DataSize = 0
                    Me.DownloadAsync(Me.Url)
                    Me.m_TryCount += 1
                End If
                Me.m_Second += 1
                Dim elapsedEvent As System.Timers.ElapsedEventHandler = Me.ElapsedEvent
                If (Not elapsedEvent Is Nothing) Then
                    elapsedEvent.Invoke(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(sender), e)
                End If
            Catch exception1 As System.Net.WebException
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Net.WebException = exception1
                Me.m_Data = Nothing
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, exception)
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                Me.m_Data = Nothing
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, exception2)
                End If
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Public Overloads Sub UploadFileAsync()
            Dim downloadErrorEvent As DownloadErrorEventHandler
            Try
                Me.Initionlize()
                Me.m_Timer.Start()
                MyBase.UploadFileAsync(Me.Url, "POST", Me.m_FileName)
            Catch exception1 As System.Net.WebException
                ProjectData.SetProjectError(exception1)
                Dim e As System.Net.WebException = exception1
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, e)
                End If
                ProjectData.ClearProjectError()
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                downloadErrorEvent = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(Me, exception2)
                End If
                ProjectData.ClearProjectError()
            End Try
        End Sub



        ' Properties
        Public ReadOnly Property Data As Byte()
            Get
                Return Me.m_Data
            End Get
        End Property

        Public ReadOnly Property DataLength As Integer
            Get
                Return CInt(Me.m_DataSize)
            End Get
        End Property

        Public Property FileName As String
            Get
                Return m_FileName
            End Get
            Set(ByVal value As String)
                m_FileName = value
            End Set
        End Property

        Public ReadOnly Property IsCancelled As Boolean
            Get
                Return Me.m_IsCancelled
            End Get
        End Property

        Public ReadOnly Property IsDownloaded As Boolean
            Get
                Return Me.m_IsDownloaded
            End Get
        End Property

        Public ReadOnly Property Kbs As Single
            Get
                Return Me.m_Kbs
            End Get
        End Property

        'Private Overridable Property m_Timer As System.Timers.Timer
        Private Property m_Timer As System.Timers.Timer
            <DebuggerNonUserCode()> _
            Get
                Return Me._m_Timer
            End Get
            <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
            Set(ByVal WithEventsValue As System.Timers.Timer)
                If (Not Me._m_Timer Is Nothing) Then
                    RemoveHandler Me._m_Timer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.m_Timer_Elapsed)
                End If
                Me._m_Timer = WithEventsValue
                If (Not Me._m_Timer Is Nothing) Then
                    AddHandler Me._m_Timer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.m_Timer_Elapsed)
                End If
            End Set
        End Property

        Public Property Name As String
            Get
                Return Strings.UCase(Me.m_Name)
            End Get
            Set(ByVal value As String)
                Me.m_Name = Strings.UCase(value)
            End Set
        End Property

        Public ReadOnly Property Percent As Integer
            Get
                Return Me.m_Percent
            End Get
        End Property

        Public ReadOnly Property RecivedLength As Integer
            Get
                Return CInt(Me.m_RecivedSize)
            End Get
        End Property

        Public ReadOnly Property Second As Integer
            Get
                Return Me.m_Second
            End Get
        End Property

        Public Property Timeout As Integer
            Get
                Return Me.m_Timeout
            End Get
            Set(ByVal value As Integer)
                If (value < 1) Then
                    value = 30
                End If
                Me.m_Timeout = value
            End Set
        End Property

        Public ReadOnly Property TotalSecond As Integer
            Get
                Return Me.m_TotalSecond
            End Get
        End Property

        Public ReadOnly Property TryCount As Integer
            Get
                Return Me.m_TryCount
            End Get
        End Property

        Public Property TryLimit As Integer
            Get
                Return Me.m_TryLimit
            End Get
            Set(ByVal value As Integer)
                If (value < 1) Then
                    value = 5
                End If
                Me.m_TryLimit = value
            End Set
        End Property

        Public Property Url As Uri
            Get
                Return Me.m_Uri
            End Get
            Set(ByVal value As Uri)
                Me.m_Uri = value
            End Set
        End Property




    End Class

End Namespace
