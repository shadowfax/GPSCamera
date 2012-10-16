Imports Microsoft.VisualBasic.CompilerServices

Public NotInheritable Class SplashScreen1

    'TODO: Este formulario se puede establecer fácilmente como pantalla de presentación para la aplicación desde la ficha "Aplicación"
    '  del Diseñador de proyectos ("Propiedades" bajo el menú "Proyecto").
    <System.Runtime.CompilerServices.AccessedThroughProperty("_Clients")> _
    Private __Clients As GPSCamera.Client.Download.ClientDownloadCollection
    <System.Runtime.CompilerServices.AccessedThroughProperty("_DownloadTimer")> _
    Private __DownloadTimer As System.Timers.Timer

    Private m_CheckValidityCount As Short
    Private m_CheckValidityFinish As Boolean
    Private m_DownloadPicCount As Short
    Private m_DownloadPicFinish As Boolean
    Private m_DownloadServerCount As Short
    Private m_DownloadServerFinish As Boolean
    Private m_Lng As String
    Private m_MinIndex As Integer
    Private m_ProductPic As Bitmap
    Private m_Progress As Short
    Private m_RunMessage As String
    Private m_Type As String
    Private m_Url As String
    Private m_UrlParamaters As String

    
    Private Event PicCompleted As PicCompletedEventHandler
    Private Event PingCompleted As System.Net.NetworkInformation.PingCompletedEventHandler
    Private Event Status As StatusEventHandler
    Private Event Progress As ProgressEventHandler

    Private Event ExceptionProcess As ExceptionProcessEventHandler
    Private Event ServerCompleted As ServerCompletedEventHandler
    Private Event ValidityCompleted As ValidityCompletedEventHandler



    Private DownloadErrorEvent As DownloadErrorEventHandler


    Private Delegate Sub Delegate_DisplayExceptionProcess(ByVal pMessage As String)
    Private Delegate Sub Delegate_DisplayProgress(ByVal pValue As Short)
    Private Delegate Sub Delegate_DisplayShow()
    Private Delegate Sub Delegate_DisplayStatus(ByVal pStatus As String)


    Private Delegate Sub DownloadErrorEventHandler(ByVal pMessage As String)
    Private Delegate Sub ExceptionProcessEventHandler(ByVal pMessage As String)
    Private Delegate Sub PicCompletedEventHandler()
    Private Delegate Sub ProgressEventHandler(ByVal pValue As Short)
    Private Delegate Sub ServerCompletedEventHandler()
    Private Delegate Sub StatusEventHandler(ByVal pStatus As String)
    Private Delegate Sub ValidityCompletedEventHandler()


    Private Property _Clients As GPSCamera.Client.Download.ClientDownloadCollection
        <DebuggerNonUserCode()> _
        Get
            Return Me.__Clients
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As GPSCamera.Client.Download.ClientDownloadCollection)
            Me.__Clients = WithEventsValue
        End Set
    End Property

    Private Property _DownloadTimer As System.Timers.Timer
        <DebuggerNonUserCode()> _
        Get
            Return Me.__DownloadTimer
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As System.Timers.Timer)
            If (Not Me.__DownloadTimer Is Nothing) Then
                RemoveHandler Me.__DownloadTimer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me._DownloadTimer_Elapsed)
            End If
            Me.__DownloadTimer = WithEventsValue
            If (Not Me.__DownloadTimer Is Nothing) Then
                AddHandler Me.__DownloadTimer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me._DownloadTimer_Elapsed)
            End If
        End Set
    End Property






    Private Sub _Client_DownloadError(ByVal sender As Object, ByVal e As Exception)
        Dim download As GPSCamera.Client.Download.ClientDownload = DirectCast(sender, GPSCamera.Client.Download.ClientDownload)
        Me._DownloadTimer.Stop()
        If (Not e Is Nothing) Then
            download.DownloadCancelAsync()
        End If
        Dim downloadErrorEvent As DownloadErrorEventHandler = Me.DownloadErrorEvent
        If (Not downloadErrorEvent Is Nothing) Then
            downloadErrorEvent.Invoke(e.Message)
        End If
    End Sub

    Private Sub _DownloadTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim enumerator As IEnumerator = Nothing
        Dim str As String = ""
        Dim num As Integer = 0
        Dim str2 As String = ""
        Try
            enumerator = Me._Clients.Keys.GetEnumerator
            Do While enumerator.MoveNext
                Dim str3 As String = Conversions.ToString(enumerator.Current)
                Dim download As GPSCamera.Client.Download.ClientDownload = DirectCast(Me._Clients.Item(str3), GPSCamera.Client.Download.ClientDownload)
                num = (num + download.Percent)
                If (download.Second > 5) Then
                    str = My.Resources.Lucla.NetworkCongestion
                End If
                Dim name As String = download.Name
                If (name = "VAL") Then
                    str2 = My.Resources.Lucla.CheckValidity
                Else
                    If (name = "PIC") Then
                        str2 = My.Resources.Lucla.GetModelPic
                        Continue Do
                    End If
                    If (name = "SERVER") Then
                        str2 = My.Resources.Lucla.LinkServer
                    End If
                End If
            Loop
        Finally
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        num = CInt(Math.Round(Conversion.Int(CDbl(((CDbl(num) / CDbl((Me._Clients.Count * 100))) * 100)))))
        Dim statusEvent As StatusEventHandler = Me.StatusEvent
        If (Not statusEvent Is Nothing) Then
            statusEvent.Invoke(String.Format("{0} ... {1}", str2, str))
        End If
        If (num = 100) Then
            Me._DownloadTimer.Stop()
            Try
                Dim enumerator2 As IEnumerator = Nothing
                Dim progressEvent As ProgressEventHandler
                Dim download2 As New GPSCamera.Client.Download.ClientDownload
                Try
                    enumerator2 = Me._Clients.Keys.GetEnumerator
                    Do While enumerator2.MoveNext
                        Dim str4 As String = Conversions.ToString(enumerator2.Current)
                        download2 = DirectCast(Me._Clients.Item(str4), GPSCamera.Client.Download.ClientDownload)
                    Loop
                Finally
                    If TypeOf enumerator2 Is IDisposable Then
                        TryCast(enumerator2, IDisposable).Dispose()
                    End If
                End Try
                Dim str8 As String = download2.Name
                Select Case str8
                    Case "VAL"
                        Dim exceptionProcessEvent As ExceptionProcessEventHandler
                        If (download2.Data.Length < 4) Then
                            If (Me.m_CheckValidityCount > 4) Then
                                exceptionProcessEvent = Me.ExceptionProcessEvent
                                If (Not exceptionProcessEvent Is Nothing) Then
                                    exceptionProcessEvent.Invoke(My.Resources.Lucla.FailedtoLinkServer)
                                End If
                            Else
                                Me.GetData("VAL")
                                Return
                            End If
                        End If
                        Dim pMessage As String = System.Text.Encoding.UTF8.GetString(download2.Data)
                        If (pMessage <> "TRUE") Then
                            Dim str9 As String = pMessage
                            If (str9 = "NOOPEN") Then
                                exceptionProcessEvent = Me.ExceptionProcessEvent
                                If (Not exceptionProcessEvent Is Nothing) Then
                                    exceptionProcessEvent.Invoke(My.Resources.Lucla.ModelIsLock)
                                End If
                            ElseIf (str9 = "NOTYPE") Then
                                exceptionProcessEvent = Me.ExceptionProcessEvent
                                If (Not exceptionProcessEvent Is Nothing) Then
                                    exceptionProcessEvent.Invoke(My.Resources.Lucla.ModelIsNull)
                                End If
                            Else
                                exceptionProcessEvent = Me.ExceptionProcessEvent
                                If (Not exceptionProcessEvent Is Nothing) Then
                                    exceptionProcessEvent.Invoke(pMessage)
                                End If
                            End If
                        Else
                            Me.m_CheckValidityFinish = True
                            Me.m_Progress = CShort((Me.m_Progress + 20))
                            progressEvent = Me.ProgressEvent
                            If (Not progressEvent Is Nothing) Then
                                progressEvent.Invoke(Me.m_Progress)
                            End If
                            Dim validityCompletedEvent As ValidityCompletedEventHandler = Me.ValidityCompletedEvent
                            If (Not validityCompletedEvent Is Nothing) Then
                                validityCompletedEvent.Invoke()
                            End If
                        End If
                        Return
                    Case "PIC"
                        Dim picCompletedEvent As PicCompletedEventHandler
                        If (download2.Data.Length > &H400) Then
                            Dim stream As System.IO.Stream = New System.IO.MemoryStream(download2.Data)
                            Me.m_ProductPic = New System.Drawing.Bitmap(stream)
                            Me.m_DownloadPicFinish = True
                            Me.m_Progress = CShort((Me.m_Progress + 15))
                            progressEvent = Me.ProgressEvent
                            If (Not progressEvent Is Nothing) Then
                                progressEvent.Invoke(Me.m_Progress)
                            End If
                            picCompletedEvent = Me.PicCompletedEvent
                            If (Not picCompletedEvent Is Nothing) Then
                                picCompletedEvent.Invoke()
                            End If
                        ElseIf (Me.m_DownloadPicCount > 4) Then
                            ' ToDo: Set a default image inside resources
                            'Me.m_ProductPic = My.Resources._public
                            Me.m_DownloadPicFinish = True
                            picCompletedEvent = Me.PicCompletedEvent
                            If (Not picCompletedEvent Is Nothing) Then
                                picCompletedEvent.Invoke()
                            End If
                        Else
                            Me.GetData("PIC")
                        End If
                        Return
                End Select
                If (str8 = "SERVER") Then
                    Dim serverCompletedEvent As ServerCompletedEventHandler
                    If (download2.Data.Length < 5) Then
                        If (Me.m_DownloadServerCount > 4) Then
                            Me.m_DownloadServerFinish = True
                            serverCompletedEvent = Me.ServerCompletedEvent
                            If (Not serverCompletedEvent Is Nothing) Then
                                serverCompletedEvent.Invoke()
                            End If
                        Else
                            Me.GetData("SERVER")
                            Return
                        End If
                    End If
                    Dim str6 As String = System.Text.Encoding.UTF8.GetString(download2.Data)
                    If (str6.IndexOf("Error") = -1) Then
                        Dim strArray3 As String() = str6.Split(New Char() {"|"c})
                        Dim list2 As New ArrayList
                        Dim list As ArrayList = GPSCamera.Data.GetServer(Me.m_Lng)
                        Dim num5 As Short = Conversions.ToShort(Operators.SubtractObject(NewLateBinding.LateGet(list.Item(0), Nothing, "Length", New Object(0 - 1) {}, Nothing, Nothing, Nothing), 1))
                        Dim i As Short = 0
                        Do While (i <= num5)
                            Dim num6 As Short = CShort((strArray3.Length - 1))
                            Dim k As Short = 0
                            Do While (k <= num6)
                                If Operators.ConditionalCompareObjectEqual(strArray3(k).Replace("{", "."), NewLateBinding.LateIndexGet(list.Item(1), New Object() {i}, Nothing), False) Then
                                    list2.Add(i)
                                End If
                                k = CShort((k + 1))
                            Loop
                            i = CShort((i + 1))
                        Loop
                        Dim list3 As New ArrayList
                        Dim strArray2 As String() = New String(((list2.Count - 1) + 1) - 1) {}
                        Dim strArray As String() = New String(((list2.Count - 1) + 1) - 1) {}
                        Dim num7 As Short = CShort((list2.Count - 1))
                        Dim j As Short = 0
                        Do While (j <= num7)
                            strArray2(j) = GPSCamera.Data.GetName(Me.m_Lng)(Conversions.ToInteger(list2.Item(j)))
                            strArray(j) = GPSCamera.Data.GetAddress(Conversions.ToInteger(list2.Item(j)))
                            j = CShort((j + 1))
                        Loop
                        list3.Add(strArray2)
                        list3.Add(strArray)
                        If (list2.Count > 0) Then
                            'GPSCamera.Data.GetServer(Me.m_Lng, list3)
                            GPSCamera.Data.GetServer(Me.m_Lng) = list3
                        End If
                        Me.m_DownloadServerFinish = True
                        Me.m_Progress = CShort((Me.m_Progress + 15))
                        progressEvent = Me.ProgressEvent
                        If (Not progressEvent Is Nothing) Then
                            progressEvent.Invoke(Me.m_Progress)
                        End If
                        serverCompletedEvent = Me.ServerCompletedEvent
                        If (Not serverCompletedEvent Is Nothing) Then
                            serverCompletedEvent.Invoke()
                        End If
                    ElseIf (Me.m_DownloadServerCount > 4) Then
                        Me.m_DownloadServerFinish = True
                        serverCompletedEvent = Me.ServerCompletedEvent
                        If (Not serverCompletedEvent Is Nothing) Then
                            serverCompletedEvent.Invoke()
                        End If
                    Else
                        Me.GetData("SERVER")
                    End If
                End If
            Catch ex As Exception
                Dim downloadErrorEvent As DownloadErrorEventHandler = Me.DownloadErrorEvent
                If (Not downloadErrorEvent Is Nothing) Then
                    downloadErrorEvent.Invoke(ex.Message)
                Else
                    Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
                    If (Not exceptionProcessEvent Is Nothing) Then
                        exceptionProcessEvent.Invoke(ex.Message)
                    Else
                        Me.DisplayExceptionProcess(ex.Message)
                    End If
                End If
            End Try
        End If
    End Sub

    Private Sub DisplayExceptionProcess(ByVal pMessage As String)
        Try
            Dim method As Delegate_DisplayExceptionProcess = New Delegate_DisplayExceptionProcess(AddressOf Me.XDisplayExceptionProcess)
            Me.BeginInvoke(method, New Object() {pMessage})
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
    End Sub





    Private Sub DisplayProgress(ByVal pValue As Short)
        Try
            Dim method As Delegate_DisplayProgress = New Delegate_DisplayProgress(AddressOf Me.XDisplayProgress)
            Me.BeginInvoke(method, New Object() {pValue})
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
    End Sub

    Private Sub DisplayShow()
        Try
            Dim method As Delegate_DisplayShow = New Delegate_DisplayShow(AddressOf Me.XDisplayShow)
            Me.BeginInvoke(method)
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
    End Sub






    Private Sub DisplayStatus(ByVal pStatus As String)
        Try
            Dim method As Delegate_DisplayStatus = New Delegate_DisplayStatus(AddressOf Me.XDisplayStatus)
            Me.BeginInvoke(method, New Object() {pStatus})
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
    End Sub

    Private Sub GetData(ByVal pItem As String)
        Try
            Dim statusEvent As StatusEventHandler = Me.StatusEvent
            If (Not statusEvent Is Nothing) Then
                statusEvent.Invoke(My.Resources.Lucla.GetData)
            End If
            Me.m_RunMessage = String.Format((My.Resources.Lucla.GetData & "({0})"), pItem)
            Me._Clients.Clear()
            Dim download As New GPSCamera.Client.Download.ClientDownload(Strings.UCase(pItem))
            Dim download2 As GPSCamera.Client.Download.ClientDownload = download
            download2.Url = Me.GetServer(Conversions.ToString(NewLateBinding.LateIndexGet(GPSCamera.Data.GetServer(Me.m_Lng).Item(1), New Object() {Me.m_MinIndex}, Nothing)), pItem, Me.m_Type)
            If (download2.Url.AbsoluteUri.IndexOf("Error") > -1) Then
                Throw New Exception(download2.Url.AbsoluteUri)
            End If
            AddHandler download2.DownloadError, New GPSCamera.Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me._Client_DownloadError)
            Dim downloadThread As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf download2.DownloadAsync))
            downloadThread.Start()
            Me._Clients.Remove(download.Name)
            Me._Clients.Add(download.Name, download)
            Select Case pItem
                Case "VAL"
                    Me.m_CheckValidityCount = CShort((Me.m_CheckValidityCount + 1))
                    Exit Select
                Case "PIC"
                    Me.m_DownloadPicCount = CShort((Me.m_DownloadPicCount + 1))
                    Exit Select
                Case "SERVER"
                    Me.m_DownloadServerCount = CShort((Me.m_DownloadServerCount + 1))
                    Exit Select
            End Select
            download2 = Nothing
            Me._DownloadTimer.Start()
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
    End Sub

    Private Sub GetQueryStringParameters()
        Try
            Dim values As New System.Collections.Specialized.NameValueCollection
            ' Cambio a GPS-868 (Lucla Mini 560)
            Me.m_Type = My.Settings.Device.Replace("+", "!").Replace("/", "[")
            Dim allKeys As String() = values.AllKeys
            Dim num2 As Short = CShort((allKeys.Length - 1))
            Dim i As Short = 0
            Do While (i <= num2)
                Select Case allKeys(i)
                    Case "type"
                        values.Item("type") = values.Item("type").Replace("/", "[").Replace("+", "!")
                        If (values.Item("type").IndexOf("|") > -1) Then
                            Me.m_Type = values.Item("type").Split(New Char() {"|"c})(0)
                        Else
                            Me.m_Type = values.Item("type")
                        End If
                        Exit Select
                    Case "lng"
                        Me.m_Lng = values.Item("lng")
                        Exit Select
                End Select
                i = CShort((i + 1))
            Loop
            If String.IsNullOrEmpty(Me.m_Lng) Then
                ' Default to english
                'Me.m_Lng = "zh-cht"
                Me.m_Lng = "en"
            End If
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
    End Sub

    Private Function GetServer(ByVal pIPAddress As String, ByVal pItemName As String, Optional ByVal pType As String = "") As Uri
        Dim uri As Uri = Nothing
        Try
            Dim str As String = String.Format(Me.m_Url, pIPAddress)
            Dim str2 As String = String.Format(Me.m_UrlParamaters, pItemName, System.Web.HttpUtility.UrlEncode(pType))
            Me.m_RunMessage = String.Format("Get Uri({0}){1}|{2}{3}", New Object() {pItemName, pIPAddress, pType, ChrW(13) & ChrW(10)})
            uri = New Uri(String.Format("{0}?{1}", str, str2))
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            Else
                Me.DisplayExceptionProcess(ex.Message)
            End If
        End Try
        Return uri
    End Function





    Private Sub hPingCompleted()
        Me.GetData("VAL")
    End Sub


    Private Sub PingServer()
        Dim statusEvent As StatusEventHandler = Me.StatusEvent
        If (Not statusEvent Is Nothing) Then
            statusEvent.Invoke(My.Resources.Lucla.PingServer)
        End If
        Me.m_RunMessage = My.Resources.Lucla.PingServer
        Me.m_Progress = CShort((Me.m_Progress + 10))
        Dim progressEvent As ProgressEventHandler = Me.ProgressEvent
        If (Not progressEvent Is Nothing) Then
            progressEvent.Invoke(Me.m_Progress)
        End If
        Dim ping As New System.Net.NetworkInformation.Ping
        Dim options As New System.Net.NetworkInformation.PingOptions(120, True)
        Try
            Dim list As ArrayList = GPSCamera.Data.GetServer(Me.m_Lng)
            Dim num3 As Short = Conversions.ToShort(Operators.SubtractObject(NewLateBinding.LateGet(list.Item(0), Nothing, "Length", New Object(0 - 1) {}, Nothing, Nothing, Nothing), 1))
            Dim minTime As Short = Short.MaxValue
            Dim i As Short = 0
            Do While (i <= num3)
                Dim hostNameOrAddress As String = Conversions.ToString(NewLateBinding.LateIndexGet(list.Item(1), New Object() {i}, Nothing))
                Dim s As String = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                Dim bytes As Byte() = System.Text.Encoding.ASCII.GetBytes(s)
                Dim reply As System.Net.NetworkInformation.PingReply = ping.Send(hostNameOrAddress, 3000, bytes, options)
                If ((reply.Status = System.Net.NetworkInformation.IPStatus.Success) AndAlso (reply.RoundtripTime < minTime)) Then
                    minTime = CInt(reply.RoundtripTime)
                    Me.m_MinIndex = i
                End If
                Me.m_Progress = CShort((Me.m_Progress + 10))
                progressEvent = Me.ProgressEvent
                If (Not progressEvent Is Nothing) Then
                    progressEvent.Invoke(Me.m_Progress)
                End If
                i = CShort((i + 1))
            Loop
        Catch ex As Exception
            Dim exceptionProcessEvent As ExceptionProcessEventHandler = Me.ExceptionProcessEvent
            If (Not exceptionProcessEvent Is Nothing) Then
                exceptionProcessEvent.Invoke(ex.Message)
            End If
            Me.m_MinIndex = 2
        Finally
            Dim pingCompletedEvent As System.Net.NetworkInformation.PingCompletedEventHandler = Me.PingCompletedEvent
            If (Not pingCompletedEvent Is Nothing) Then
                pingCompletedEvent.Invoke(Nothing, Nothing)
            End If
        End Try
    End Sub





    Private Sub XDisplayExceptionProcess(ByVal pMessage As String)
        System.Windows.Forms.MessageBox.Show(pMessage, My.Resources.Lucla.ErrorTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
        Me.Close()
        My.Application.DoEvents()
    End Sub


    Private Sub XDisplayProgress(ByVal pValue As Short)
        Me.pbProgress.Value = Conversions.ToInteger(Interaction.IIf((pValue > 100), 100, pValue))
        My.Application.DoEvents()
    End Sub

    Private Sub XDisplayShow()
        Dim frmServicePack As frmServicePack = My.Forms.frmServicePack
        frmServicePack.ServerIndex = Me.m_MinIndex
        frmServicePack.Type = Me.m_Type
        frmServicePack.ProductPic = Me.m_ProductPic
        'frmServicePack.SelectLng = Me.m_Lng
        frmServicePack.Show()
        frmServicePack = Nothing
        Me.Close()
        My.Application.DoEvents()
    End Sub






    Private Sub XDisplayStatus(ByVal pStatus As String)
        Me.lblStatus.Text = pStatus
        My.Application.DoEvents()
    End Sub

    Private Sub SplashScreen_ExceptionProcess(ByVal pMessage As String) Handles Me.ExceptionProcess
        Me.DisplayExceptionProcess(pMessage)
    End Sub

    Private Sub SplashScreen_PicCompleted() Handles Me.PicCompleted
        Me.GetData("SERVER")
    End Sub



    Private Sub splashScreen_Status(ByVal pStatus As String) Handles Me.Status
        Me.DisplayStatus(pStatus)
    End Sub


    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Configure el texto del cuadro de diálogo en tiempo de ejecución según la información del ensamblado de la aplicación.  

        'TODO: Personalice la información del ensamblado de la aplicación en el panel "Aplicación" del cuadro de diálogo 
        '  propiedades del proyecto (bajo el menú "Proyecto").
        Try
            Me.GetQueryStringParameters()
            Me.DisplayStatus("")
            Me.DisplayProgress(Me.m_Progress)
            Me.m_RunMessage = My.Resources.Lucla.CheckArgument
            If (Me.m_Type.Length = 0) Then
                Throw New Exception(My.Resources.Lucla.NoArgument)
            End If
            'New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf Me.PingServer)).Start
            Dim pingThread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf Me.PingServer))
            pingThread.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Resources.Lucla.ErrorTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error)
        End Try
        
       
    End Sub

    Public Sub New()
        'AddHandler DownloadError, New DownloadErrorEventHandler(AddressOf Me.splashScreen_DownloadError)
        AddHandler Status, New StatusEventHandler(AddressOf Me.splashScreen_Status)
        AddHandler Progress, New ProgressEventHandler(AddressOf Me.splashScreen_Progress)
        AddHandler PingCompleted, New System.Net.NetworkInformation.PingCompletedEventHandler(AddressOf Me.hPingCompleted)
        AddHandler ValidityCompleted, New ValidityCompletedEventHandler(AddressOf Me.splashScreen_ValidityCompleted)
        AddHandler PicCompleted, New PicCompletedEventHandler(AddressOf Me.splashScreen_PicCompleted)
        AddHandler ServerCompleted, New ServerCompletedEventHandler(AddressOf Me.splashScreen_ServerCompleted)
        AddHandler ExceptionProcess, New ExceptionProcessEventHandler(AddressOf Me.splashScreen_ExceptionProcess)


        Me.m_Type = ""
        Me.m_Lng = ""
        Me.m_MinIndex = 0
        'Me.m_ProductPic = Nothing
        Me.m_Progress = 0
        Me.m_UrlParamaters = "item={0}&type={1}"
        Me.m_Url = "http://{0}/GM/com_data_file.aspx"
        Me.m_CheckValidityCount = 0
        Me.m_DownloadPicCount = 0
        Me.m_DownloadServerCount = 0
        Me.m_RunMessage = ""
        Me.m_CheckValidityFinish = False
        Me.m_DownloadPicFinish = False
        Me.m_DownloadServerFinish = False
        'Me._Client = New GPSCamera.Client.Download.ClientDownload
        Me._Clients = New GPSCamera.Client.Download.ClientDownloadCollection
        Me._DownloadTimer = New System.Timers.Timer

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub SplashScreen_Progress(ByVal pValue As Short) Handles Me.Progress
        Me.DisplayProgress(pValue)
    End Sub

    Private Sub SplashScreen_ValidityCompleted() Handles Me.ValidityCompleted
        Me.GetData("PIC")
    End Sub

    Private Sub SplashScreen_ServerCompleted() Handles Me.ServerCompleted
        Me.DisplayShow()
    End Sub

    Private Sub SplashScreen1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus

    End Sub
End Class
