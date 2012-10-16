Imports Microsoft.VisualBasic.CompilerServices


Public Class UpdateProcess

#Region "    Private Variables "

    <System.Runtime.CompilerServices.AccessedThroughProperty("_Client")> _
    Private __Client As Client.Download.ClientDownload
    <System.Runtime.CompilerServices.AccessedThroughProperty("_Clients")> _
    Private __Clients As Client.Download.ClientDownloadCollection
    <System.Runtime.CompilerServices.AccessedThroughProperty("_DownloadTimer")> _
    Private __DownloadTimer As System.Timers.Timer
    <System.Runtime.CompilerServices.AccessedThroughProperty("_GMx")> _
    Private __GMx As Communications.GMx
    <System.Runtime.CompilerServices.AccessedThroughProperty("_WorkTimer")> _
    Private __WorkTimer As WorkTimer
    Private m_Booree As Boolean
    Private m_CanStartWork As Boolean
    Private m_CheckVersion As Boolean
    Private m_ChipInfo As DeviceInformation.ChipInfo
    Private m_ChooseCounty As ArrayList
    Private m_Count As Integer
    Private m_CountyByte As Byte()
    Private m_DbgArea As String
    Private m_DbgPassword As Byte()
    Private m_DbgVersion As String
    Private m_FilePath As String
    Private m_HaveToUpdate As Boolean
    Private m_HaveToWriteCompany As Boolean
    Private m_HaveToWriteMotherboard As Boolean
    Private m_HostInfo As DeviceInformation.HostInfo
    Private m_Hot_Dbg As Boolean
    Private m_Hot_Voc As Boolean
    Private m_Hot_Vot As Boolean
    Private m_Hot_Vow As Boolean
    Private m_InBoot As Boolean
    Private m_IsPublicPic As Boolean
    Private m_IsWork As Boolean
    Private m_Motherboard As String
    Private m_PicArrayList As ArrayList
    Private m_PreviousCounty As Hashtable
    Private m_SelectLng As String
    Private m_ServerIndex As Integer
    Private m_SimilarIndex As Short
    Private m_SimilarMotherboard As Hashtable
    Private m_SimilarType As ArrayList
    Private m_SizeInfo As Hashtable
    Private m_Type As String
    Private m_UserId As String
    Private m_UserPassword As String





    Private pNull As String
    Private pNull2 As String


    Private m_Url As String
    Private m_UrlParameters As String

#End Region

#Region "    Delegates "

    Public Delegate Sub GotDbgVersionEventHandler(ByVal pVersion As String)
    Public Delegate Sub PicCompletedEventHandler(ByVal pPicture As System.Drawing.Bitmap)
    Public Delegate Sub SearchPortEventHandler(ByVal pPortName As String)
    Public Delegate Sub ShowPicEventHandler(ByVal pPicArrayList As ArrayList)
    Public Delegate Sub WorkCompliteEventHandler(ByVal ex As Exception)
    Public Delegate Sub WorkingEventHandler()
    Public Delegate Sub WorkStatusEventHandler(ByVal pMessage As String)
    Public Delegate Sub WorkTimeEventHandler(ByVal pTime As String)

#End Region

#Region "    Events "

    Public Event GotDbgVersion As GotDbgVersionEventHandler
    Public Event PicCompleted As PicCompletedEventHandler
    Public Event SearchPort As SearchPortEventHandler
    Public Event ShowPic As ShowPicEventHandler
    Public Event WorkComplete As WorkCompliteEventHandler
    Public Event Working As WorkingEventHandler
    Public Event WorkStatus As WorkStatusEventHandler
    Public Event WorkTime As WorkTimeEventHandler

#End Region

#Region "    Properties "

    Private Property _Client As Client.Download.ClientDownload
        <DebuggerNonUserCode()> _
        Get
            Return Me.__Client
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As Client.Download.ClientDownload)
            If (Not Me.__Client Is Nothing) Then
                RemoveHandler Me.__Client.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
            End If
            Me.__Client = WithEventsValue
            If (Not Me.__Client Is Nothing) Then
                AddHandler Me.__Client.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
            End If
        End Set
    End Property

    Private Property _Clients As Client.Download.ClientDownloadCollection
        <DebuggerNonUserCode()> _
        Get
            Return Me.__Clients
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As Client.Download.ClientDownloadCollection)
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
                RemoveHandler Me.__DownloadTimer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.DownloadTimer_Elapsed)
            End If
            Me.__DownloadTimer = WithEventsValue
            If (Not Me.__DownloadTimer Is Nothing) Then
                AddHandler Me.__DownloadTimer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.DownloadTimer_Elapsed)
            End If
        End Set
    End Property

    Private Property _GMx As Communications.GMx
        <DebuggerNonUserCode()> _
        Get
            Return Me.__GMx
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As Communications.GMx)
            If (Not Me.__GMx Is Nothing) Then
                RemoveHandler Me.__GMx.Writing, New Communications.GMx.WritingEventHandler(AddressOf Me._GMx_Writing)
            End If
            Me.__GMx = WithEventsValue
            If (Not Me.__GMx Is Nothing) Then
                AddHandler Me.__GMx.Writing, New Communications.GMx.WritingEventHandler(AddressOf Me._GMx_Writing)
            End If
        End Set
    End Property

    Public Property FilePath As String
        Get
            Return m_FilePath
        End Get
        Set(ByVal value As String)
            m_FilePath = value
        End Set
    End Property

    Private Property _WorkTimer As WorkTimer
        <DebuggerNonUserCode()> _
        Get
            Return Me.__WorkTimer
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As WorkTimer)
            If (Not Me.__WorkTimer Is Nothing) Then
                RemoveHandler Me.__WorkTimer.TimerElapsed, New WorkTimer.TimerElapsedEventHandler(AddressOf Me.WorkTimer_TimerElapsed)
            End If
            Me.__WorkTimer = WithEventsValue
            If (Not Me.__WorkTimer Is Nothing) Then
                AddHandler Me.__WorkTimer.TimerElapsed, New WorkTimer.TimerElapsedEventHandler(AddressOf Me.WorkTimer_TimerElapsed)
            End If
        End Set
    End Property

    Public Property CheckVersion As Boolean
        Get
            Return m_CheckVersion
        End Get
        Set(ByVal value As Boolean)
            m_CheckVersion = value
        End Set
    End Property

    Public Property IsPublicPic As Boolean
        Get
            Return m_IsPublicPic
        End Get
        Set(ByVal value As Boolean)
            m_IsPublicPic = value
        End Set
    End Property

    Public Property SelectLng As String
        Get
            Return m_SelectLng
        End Get
        Set(ByVal value As String)
            m_SelectLng = value
        End Set
    End Property

    Public Property ServerIndex As Short
        Get
            Return m_ServerIndex
        End Get
        Set(ByVal value As Short)
            Dim serverList As New System.Collections.ArrayList(My.Settings.Servers.Split("|"c))
            If ((value >= 0) And (value < serverList.Count)) Then
                m_ServerIndex = value
            Else
                Throw New IndexOutOfRangeException()
            End If
        End Set
    End Property

    Public Property Type As String
        Get
            Return m_Type
        End Get
        Set(ByVal value As String)
            m_Type = value
        End Set
    End Property

    Public Property UserId As String
        Get
            Return m_UserId
        End Get
        Set(ByVal value As String)
            m_UserId = value
        End Set
    End Property

    Public Property UserPassword As String
        Get
            Return m_UserPassword
        End Get
        Set(ByVal value As String)
            m_UserPassword = value
        End Set
    End Property

#End Region

    Private Sub _CHx_Wirtting(ByVal pDataSource As String, ByVal pCurrentSize As Long, ByVal pTotalSize As Long)
        Dim num As Integer = CInt(Math.Round(CDbl(((CDbl(pCurrentSize) / CDbl(pTotalSize)) * 100))))
        Dim str As String = ""
        Dim str2 As String = Strings.UCase(pDataSource)
        If (str2 = "DRVG") Then
            str = My.Resources.GPSCamera.DeviceInitialization
        ElseIf (str2 = "DRVS") Then
            str = My.Resources.GPSCamera.DeviceReset
        ElseIf (str2 = "DRVB") Then
            str = My.Resources.GPSCamera.DeviceRevise
        ElseIf (str2 = "DBG") Then
            str = My.Resources.GPSCamera.DatabaseUpdate
        ElseIf (str2 = "VOC") Then
            str = My.Resources.GPSCamera.VoiceUpdate
        ElseIf (str2 = "VOT") Then
            str = My.Resources.GPSCamera.VoiceTableUpdate
        ElseIf (str2 = "VOW") Then
            str = My.Resources.GPSCamera.WelcomeSpeechUpdate
        Else
            str = My.Resources.GPSCamera.DeviceUpdate
        End If
        Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
        If (Not workStatusEvent Is Nothing) Then
            workStatusEvent.Invoke(String.Format("{0} {1}%", str, num))
        End If
    End Sub




    Private Sub _GMx_Writing(ByVal pItem As Communications.DataItem, ByVal pWritedBytes As Long, ByVal pTotalBytes As Long, ByVal pPercent As Integer, ByVal pBuffer As Byte())
        Me._CHx_Wirtting(pItem.ToString, pWritedBytes, pTotalBytes)
    End Sub

    Private Sub CheckDataAgain()
        Try
            Me.m_InBoot = True
            If Me.m_CanStartWork Then
                Me.m_HostInfo = New DeviceInformation.HostInfo(Me._GMx.ReadHostInfo(Me.m_Count, 0))
                Dim str As String = Convert.ToBase64String(Me.m_HostInfo.Company)
                If ((str <> Me.pNull) AndAlso (str <> Me.pNull2)) Then
                    If Operators.ConditionalCompareObjectEqual(Me.m_SizeInfo.Item("Patch"), "Y", False) Then
                        Me.WriteBooree()
                    End If
                    Me.ReadChipInfo()
                Else
                    Me.m_InBoot = False
                    Me.CheckDevice()
                End If
            Else
                Me.CheckDevice()
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Function CheckDbgCounts(ByRef pData As Byte()) As Boolean
        Dim flag As Boolean
        Try
            If ((pData.Length Mod &H10) <> 0) Then
                Return False
            End If
            Dim bytes As Byte() = Tools.DataConvert.GetBytes(4, 0)
            Dim num As Integer = CInt(Math.Round(CDbl(((CDbl(pData.Length) / 16) - 2))))
            Dim sourceArray As Byte() = Tools.DataConvert.GetBytes(&H10, &HFF)
            Dim destinationArray As Byte() = Tools.DataConvert.GetBytes(&H10, &HFF)
            Array.ConstrainedCopy(sourceArray, 0, pData, (pData.Length - &H10), &H10)
            Array.ConstrainedCopy(pData, (num * &H10), destinationArray, 0, destinationArray.Length)
            Do While (Convert.ToBase64String(destinationArray) = Convert.ToBase64String(sourceArray))
                num -= 1
                Array.ConstrainedCopy(pData, (num * &H10), destinationArray, 0, destinationArray.Length)
            Loop
            bytes = BitConverter.GetBytes(num)
            Array.Reverse(bytes)
            Array.ConstrainedCopy(bytes, 0, pData, 0, bytes.Length)
            flag = True
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            flag = False
            ProjectData.ClearProjectError()
        End Try
        Return flag
    End Function


    Private Sub CheckDevice()
        Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
        If (Not workStatusEvent Is Nothing) Then
            workStatusEvent.Invoke(My.Resources.GPSCamera.Authentication)
        End If
        Try
            Me.m_HostInfo = New DeviceInformation.HostInfo(Me._GMx.ReadHostInfo(Me.m_Count, 0))
            Dim str As String = Convert.ToBase64String(Me.m_HostInfo.Company)
            If ((str <> Me.pNull) AndAlso (str <> Me.pNull2)) Then
                Me.m_ChipInfo = New DeviceInformation.ChipInfo(Me._GMx.ReadChipInfo(Me.m_Count))
                Dim str2 As String = Me.CheckHostInfo
                If Not String.IsNullOrEmpty(str2) Then
                    Throw New Exception(str2)
                End If
                If Me.m_CanStartWork Then
                    Me.GetData("TIME")
                    Return
                End If
                Me.m_CanStartWork = True
            Else
                If (Me.m_SimilarMotherboard.Count <= 0) Then
                    Throw New Exception("Error:1002")
                End If
                Me.m_SimilarIndex = CShort((Me.m_SimilarIndex + 1))
                If (Me.m_SimilarIndex > (Me.m_SimilarMotherboard.Count - 1)) Then
                    Throw New Exception("Error:1001")
                End If
                Me.m_Motherboard = CStr(NewLateBinding.LateIndexGet(Me.m_SimilarMotherboard.Item(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Me.m_SimilarType.Item(Me.m_SimilarIndex))), New Object() {0}, Nothing))
            End If
            Me.GetData("SIZE")
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Function CheckHostInfo() As String
        Dim num2 As Short
        Dim str3 As String = ""
        Dim str2 As String = My.Resources.GPSCamera.WrongModel
        Dim flag As Boolean = False
        Dim falsePart As Short = 0
        Do
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(String.Format("{0} {1}", My.Resources.GPSCamera.Authentication, System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Interaction.IIf((falsePart = 0), "", falsePart))))
            End If
            If (falsePart <> 0) Then
                Me.m_HostInfo = New DeviceInformation.HostInfo(Me._GMx.ReadHostInfo(Me.m_Count, 0))
            End If
            System.Threading.Thread.Sleep(&H3E8)
            flag = False
            Dim str5 As String = Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.Model, &HFF))
            If Not String.IsNullOrEmpty(str5) Then
                Dim enumerator As IDictionaryEnumerator = Me.m_SimilarMotherboard.GetEnumerator
                Do While enumerator.MoveNext
                    Dim current As DictionaryEntry = DirectCast(enumerator.Current, DictionaryEntry)
                    If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateIndexGet(current.Value, New Object() {0}, Nothing), str5, False) Then
                        Dim left As Object = NewLateBinding.LateIndexGet(current.Value, New Object() {1}, Nothing)
                        If Operators.ConditionalCompareObjectEqual(left, "Y", False) Then
                            Me.m_SimilarIndex = CShort(Me.m_SimilarType.IndexOf(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(current.Key)))
                            Me.m_Motherboard = str5
                            Me.m_DbgArea = Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.Company, &HFF))
                            Me.m_HaveToWriteMotherboard = False
                            Return Nothing
                        End If
                        If Operators.ConditionalCompareObjectEqual(left, "N", False) Then
                            str2 = My.Resources.GPSCamera.ModelIsLock
                            flag = True
                            Exit Do
                        End If
                    End If
                Loop
            End If
            Dim str4 As String = Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.Company, &HFF))
            Dim str6 As String = Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.Product, &HFF))
            If (str4.IndexOf("_") > -1) Then
                str4 = str4.Split(New Char() {"_"c})(0)
            End If
            If ((Not String.IsNullOrEmpty(str4) And Not String.IsNullOrEmpty(str6)) And Not flag) Then
                Dim enumerator2 As IDictionaryEnumerator = Me.m_SimilarMotherboard.GetEnumerator
                Do While enumerator2.MoveNext
                    Dim entry2 As DictionaryEntry = DirectCast(enumerator2.Current, DictionaryEntry)
                    If Operators.ConditionalCompareObjectEqual(entry2.Key, String.Format("{0}[{1}", str4, str6.Replace("+", "!")), False) Then
                        Dim obj3 As Object = NewLateBinding.LateIndexGet(entry2.Value, New Object() {1}, Nothing)
                        If Operators.ConditionalCompareObjectEqual(obj3, "Y", False) Then
                            Me.m_SimilarIndex = CShort(Me.m_SimilarType.IndexOf(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(entry2.Key)))
                            Me.m_Motherboard = Conversions.ToString(NewLateBinding.LateIndexGet(entry2.Value, New Object() {0}, Nothing))
                            Me.m_DbgArea = Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.Company, &HFF))
                            Me.m_HaveToWriteMotherboard = True
                            Return Nothing
                        End If
                        If Operators.ConditionalCompareObjectEqual(obj3, "N", False) Then
                            str2 = My.Resources.GPSCamera.ModelIsLock
                            Exit Do
                        End If
                    End If
                Loop
            End If
            str3 = String.Format("{0}|{1}", str6, str5)
            falsePart = CShort((falsePart + 1))
            num2 = 5
        Loop While (falsePart <= num2)
        Return String.Format("{0} ({1})", str2, str3)
    End Function

    Private Function CheckSameId() As Boolean
        Dim str2 As String = Convert.ToBase64String(Me.m_ChipInfo.SN)
        If Me.m_Booree Then
            Me.m_ChipInfo = New DeviceInformation.ChipInfo(Me._GMx.ReadChipInfo(Me.m_Count))
        Else
            Me.m_ChipInfo = New DeviceInformation.ChipInfo(Me._GMx.ReadChipInfo)
        End If
        Dim str As String = Convert.ToBase64String(Me.m_ChipInfo.SN)
        Return (str2 = str)
    End Function


    Private Sub ClientDownload_DownloadError(ByVal sender As Object, ByVal e As Exception)
        Dim download As Client.Download.ClientDownload = DirectCast(sender, Client.Download.ClientDownload)
        Dim message As String = My.Resources.GPSCamera.NetworkDetected
        Select Case download.Name
            Case "DRVG"
                message = My.Resources.GPSCamera.AuthenticationFailed
                Exit Select
            Case "DRVS"
                message = My.Resources.GPSCamera.AuthorizationCodeFailed
                Exit Select
            Case "DRVB"
                message = My.Resources.GPSCamera.ReviseFailed
                Exit Select
            Case "DBG"
                message = My.Resources.GPSCamera.DatabaseFailed
                Exit Select
            Case "USERDBG"
                message = My.Resources.GPSCamera.UserDatabaseFailed
                Exit Select
            Case "VOC"
                message = My.Resources.GPSCamera.VoiceFailed
                Exit Select
            Case "VOT"
                message = My.Resources.GPSCamera.VoiceTableFailed
                Exit Select
            Case "VOW"
                message = My.Resources.GPSCamera.WelcomeSpeechVoiceFailed
                Exit Select
        End Select
        Me._DownloadTimer.Stop()
        Me._WorkTimer.Stop()
        If (Not e Is Nothing) Then
            download.DownloadCancelAsync()
            Dim workCompleteEvent As WorkCompliteEventHandler = Me.WorkCompleteEvent
            If (Not workCompleteEvent Is Nothing) Then
                workCompleteEvent.Invoke(New Exception(message))
            End If
        End If
    End Sub

    Private Sub CompareTickAndVersion(ByVal pData As String)
        Me._GMx.Dbg = Nothing
        Me._GMx.Voc = Nothing
        Me._GMx.Vot = Nothing
        Me._GMx.Vow = Nothing
        Me.m_Hot_Dbg = False
        Me.m_Hot_Voc = False
        Me.m_Hot_Vot = False
        Me.m_Hot_Vow = False
        Try
            Dim strArray As String() = pData.Split(New Char() {":"c})
            Dim str As String = strArray(0)
            Dim falsePart As String = strArray(1)
            Dim str4 As String = strArray(2)
            Dim str5 As String = strArray(3)
            Select Case Me.CheckVersion
                Case False
                    Me.m_Hot_Dbg = True
                    If (falsePart <> "N") Then
                        Me.m_Hot_Voc = True
                    End If
                    If (str4 <> "N") Then
                        Me.m_Hot_Vot = True
                    End If
                    If (str5 <> "N") Then
                        Me.m_Hot_Vow = True
                    End If
                    Exit Select
                Case True
                    If ((str <> "N") And (str.IndexOf("|") > -1)) Then
                        Dim destinationArray As Byte() = New Byte(8 - 1) {}
                        Array.ConstrainedCopy(Me.m_HostInfo.DatabaseUpdateUTC, 0, destinationArray, 0, 8)
                        Dim inputStr As String = Conversions.ToString(Tools.DataConvert.ByteToDateTime(destinationArray).Ticks)
                        Dim str7 As String = Conversion.Val(Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.DatabaseVersion, &HFF))).ToString("0000")
                        Dim strArray2 As String() = str.Split(New Char() {"|"c})
                        Dim str8 As String = Conversions.ToString(Interaction.IIf(String.IsNullOrEmpty(strArray2(0)), "0", strArray2(0)))
                        Dim str9 As String = Conversions.ToString(Interaction.IIf(String.IsNullOrEmpty(strArray2(1)), "0", strArray2(1)))
                        If ((str8 <> "0") And (Conversion.Val(str8) > Conversion.Val(inputStr))) Then
                            Me.m_Hot_Dbg = True
                        End If
                        If ((str9 <> "0") And (str9 <> str7)) Then
                            Me.m_Hot_Dbg = True
                        End If
                        If ((str8 = "0") And (str9 = "0")) Then
                            Me.m_Hot_Dbg = True
                        End If
                        If Me.m_HaveToUpdate Then
                            Me.m_Hot_Dbg = True
                        End If
                    End If
                    If (falsePart <> "N") Then
                        Dim buffer2 As Byte() = New Byte(8 - 1) {}
                        Array.ConstrainedCopy(Me.m_HostInfo.VoiceUpdateUTC, 0, buffer2, 0, 8)
                        Dim str11 As String = Conversions.ToString(Tools.DataConvert.ByteToDateTime(buffer2).Ticks)
                        Dim str10 As String = Conversions.ToString(Interaction.IIf(String.IsNullOrEmpty(falsePart), "0", falsePart))
                        If ((str10 <> "0") And (Conversion.Val(str10) > Conversion.Val(str11))) Then
                            Me.m_Hot_Voc = True
                        End If
                    End If
                    If (str4 <> "N") Then
                        Me.m_Hot_Vot = True
                    End If
                    If (str5 <> "N") Then
                        Me.m_Hot_Vow = True
                    End If
                    Exit Select
            End Select
            Dim str2 As String = Convert.ToBase64String(Me.m_HostInfo.Password)
            If ((str2 = Me.pNull) Or (str2 = Me.pNull2)) Then
                Me.m_Hot_Dbg = True
            End If
            Me.GetLCS()
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            Me.m_Hot_Dbg = True
            Me.GetLCS()
            ProjectData.ClearProjectError()
        End Try
    End Sub






    Private Sub DeviceInitialize()
        Try
            Dim workStatusEvent As WorkStatusEventHandler
            If (Me.m_InBoot And Me.m_CanStartWork) Then
                workStatusEvent = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(String.Format("{0}{1}", My.Resources.GPSCamera.CheckInBoot, Me.m_Motherboard))
                End If
                Me.CheckDataAgain()
            Else
                workStatusEvent = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(String.Format("{0}{1}", My.Resources.GPSCamera.DeviceInitialization, Me.m_Motherboard))
                End If
                Dim left As Object = Me.m_SizeInfo.Item("Booree")
                If Operators.ConditionalCompareObjectEqual(left, "Y", False) Then
                    Dim num3 As Integer
                    Dim num As Integer = 0
                    Do
                        If (num >= 5) Then
                            Throw New Exception(My.Resources.GPSCamera.FindDeviceFailed)
                        End If
                        num += 1
                        System.Threading.Thread.Sleep(&H3E8)
                    Loop While Not Me._GMx.DeviceDetect
                    num = 0
                    Do
                        If (num >= 5) Then
                            Throw New Exception(My.Resources.GPSCamera.InitializationFailure)
                        End If
                        num += 1
                        System.Threading.Thread.Sleep(&H3E8)
                    Loop While Not Me._GMx.GoToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree")))
                    num = 0
                    Do
                        If (num >= 5) Then
                            Throw New Exception(My.Resources.GPSCamera.FindDeviceFailed)
                        End If
                        num += 1
                        System.Threading.Thread.Sleep(&H3E8)
                    Loop While Not Me._GMx.DeviceDetect
                    If Not Me._GMx.Write(Communications.DataItem.DrvG, Conversions.ToShort(Me.m_SizeInfo.Item("Drv"))) Then
                        Throw New Exception(My.Resources.GPSCamera.WriteInitializationFailure)
                    End If
                    System.Threading.Thread.Sleep(&H7D0)
                    num = 0
                    Do
                        If (num >= 5) Then
                            Throw New Exception(My.Resources.GPSCamera.FindDeviceFailed)
                        End If
                        num += 1
                        System.Threading.Thread.Sleep(&H3E8)
                    Loop While Not Me._GMx.DeviceDetect
                    Dim num2 As Integer = 5
                    Do
                        workStatusEvent = Me.WorkStatusEvent
                        If (Not workStatusEvent Is Nothing) Then
                            workStatusEvent.Invoke(String.Format("{0}{1}", My.Resources.GPSCamera.DeviceInitialization, num2))
                        End If
                        Me._GMx.FinishToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree")), False)
                        System.Threading.Thread.Sleep(&H3E8)
                        num2 = (num2 + -1)
                        num3 = 1
                    Loop While (num2 >= num3)
                    Me.CheckDataAgain()
                ElseIf Operators.ConditionalCompareObjectEqual(left, "N", False) Then
                    If Not Me._GMx.GoToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree"))) Then
                        Throw New Exception(My.Resources.GPSCamera.InitializationFailure)
                    End If
                    Me._GMx.Write(Communications.DataItem.DrvG, Conversions.ToShort(Me.m_SizeInfo.Item("Drv")))
                    If Not Me._GMx.FinishToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree")), False) Then
                        Throw New Exception(My.Resources.GPSCamera.InitializationFailure)
                    End If
                    Me.CheckDataAgain()
                End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub DeviceReset(Optional ByVal pUpdateHasError As Boolean = False)
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.DeviceReset)
            End If
            Me._GMx.Close()
            Dim num As Integer = 0
            Do
                If (num >= 5) Then
                    Throw New Exception(My.Resources.GPSCamera.FindDeviceFailed)
                End If
                num += 1
                System.Threading.Thread.Sleep(&H3E8)
            Loop While Not Me._GMx.DeviceDetect
            Dim left As Object = Me.m_SizeInfo.Item("Booree")
            If Operators.ConditionalCompareObjectEqual(left, "Y", False) Then
                If Not Me._GMx.GoToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree"))) Then
                    Throw New Exception(My.Resources.GPSCamera.ResetFailed)
                End If
                num = 0
                Do
                    If (num >= 5) Then
                        Throw New Exception(My.Resources.GPSCamera.FindDeviceFailed)
                    End If
                    num += 1
                    System.Threading.Thread.Sleep(&H3E8)
                Loop While Not Me._GMx.DeviceDetect
                If Not Me._GMx.Write(Communications.DataItem.DrvS, Conversions.ToShort(Me.m_SizeInfo.Item("Drv"))) Then
                    Throw New Exception(My.Resources.GPSCamera.WriteResetFailed)
                End If
                System.Threading.Thread.Sleep(&H7D0)
                If Not Me._GMx.FinishToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree")), False) Then
                    Throw New Exception(My.Resources.GPSCamera.ResetFailed)
                End If
                Me.WorkEnd(pUpdateHasError)
            ElseIf Operators.ConditionalCompareObjectEqual(left, "N", False) Then
                If Not Me._GMx.GoToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree"))) Then
                    Throw New Exception(My.Resources.GPSCamera.ResetFailed)
                End If
                Me._GMx.Write(Communications.DataItem.DrvS, Conversions.ToShort(Me.m_SizeInfo.Item("Drv")))
                If Not Me._GMx.FinishToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree")), False) Then
                    Throw New Exception(My.Resources.GPSCamera.ResetFailed)
                End If
                Me.WorkEnd(pUpdateHasError)
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub DeviceUpdate()
        Try
            Dim workStatusEvent As WorkStatusEventHandler
            Dim gotDbgVersionEvent As GotDbgVersionEventHandler
            If (((Me.m_Hot_Dbg Or Me.m_Hot_Voc) Or Me.m_Hot_Vot) Or Me.m_Hot_Vow) Then
                workStatusEvent = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(My.Resources.GPSCamera.DeviceUpdate)
                End If
                Dim mx As Communications.GMx = Me._GMx
                If (Not mx.Dbg Is Nothing) Then
                    System.Threading.Thread.Sleep(&HBB8)
                    If Not mx.Write(Communications.DataItem.Dbg, Conversions.ToShort(Me.m_SizeInfo.Item("Dbg"))) Then
                        Throw New Exception(My.Resources.GPSCamera.WriteDatabaseFailed)
                    End If
                Else
                    gotDbgVersionEvent = Me.GotDbgVersionEvent
                    If (Not gotDbgVersionEvent Is Nothing) Then
                        gotDbgVersionEvent.Invoke(Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.DatabaseVersion, &HFF)))
                    End If
                End If
                If (Not mx.Voc Is Nothing) Then
                    System.Threading.Thread.Sleep(&HBB8)
                    If Not mx.Write(Communications.DataItem.Voc, Conversions.ToShort(Me.m_SizeInfo.Item("Voc"))) Then
                        Throw New Exception(My.Resources.GPSCamera.WriteVoiceFailed)
                    End If
                End If
                If (Not mx.Vot Is Nothing) Then
                    System.Threading.Thread.Sleep(&HBB8)
                    If Not mx.Write(Communications.DataItem.Vot, Conversions.ToShort(Me.m_SizeInfo.Item("Vot"))) Then
                        Throw New Exception(My.Resources.GPSCamera.WriteVoiceTableFailed)
                    End If
                End If
                If (Not mx.Vow Is Nothing) Then
                    System.Threading.Thread.Sleep(&HBB8)
                    If Not mx.Write(Communications.DataItem.Vow, Conversions.ToShort(Me.m_SizeInfo.Item("Vow"))) Then
                        Throw New Exception(My.Resources.GPSCamera.WriteWelcomeSpeechVoiceFailed)
                    End If
                End If
                mx.Close()
                mx = Nothing
                System.Threading.Thread.Sleep(&HBB8)
                Me.ReadWriteDeviceInfo()
            Else
                gotDbgVersionEvent = Me.GotDbgVersionEvent
                If (Not gotDbgVersionEvent Is Nothing) Then
                    gotDbgVersionEvent.Invoke(Strings.Trim(Tools.DataConvert.ByteToText(Me.m_HostInfo.DatabaseVersion, &HFF)))
                End If
                workStatusEvent = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(My.Resources.GPSCamera.IsNewVersion)
                End If
                System.Threading.Thread.Sleep(&H1388)
                Me.DeviceReset(False)
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub



    Private Sub DownloadDrvG()
        Try
            Dim enumerator As IEnumerator = Nothing
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.GetCode)
            End If
            Me._Clients.Clear()
            Try
                enumerator = DirectCast(Interaction.IIf(Operators.ConditionalCompareObjectEqual(Me.m_SizeInfo.Item("Patch"), "Y", False), New String() {"DRVG", "DRVS", "DRVB"}, New String() {"DRVG", "DRVS"}), IEnumerable).GetEnumerator
                Do While enumerator.MoveNext
                    Dim download As New Client.Download.ClientDownload(Conversions.ToString(enumerator.Current))
                    Dim download2 As Client.Download.ClientDownload = download
                    download2.Url = Me.GetServer(download2.Name, "", "")
                    download2.Url = New Uri(Conversions.ToString(Operators.ConcatenateObject((download2.Url.AbsoluteUri & "&type="), Me.m_SimilarType.Item(Me.m_SimilarIndex))))
                    AddHandler download2.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
                    Dim downloadThread As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf download2.DownloadAsync))
                    downloadThread.Start()
                    Me._Clients.Add(download.Name, download)
                    download2 = Nothing
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
            Me._DownloadTimer.Start()
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub


    Private Sub DownloadTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim enumerator As IEnumerator = Nothing
        Dim str As String = ""
        Dim num As Integer = 0
        Try
            enumerator = Me._Clients.Keys.GetEnumerator
            Do While enumerator.MoveNext
                Dim str2 As String = Conversions.ToString(enumerator.Current)
                Dim download As Client.Download.ClientDownload = DirectCast(Me._Clients.Item(str2), Client.Download.ClientDownload)
                num = (num + download.Percent)
                If (download.Second > 5) Then
                    str = My.Resources.GPSCamera.NetworkCongestion
                End If
                str = String.Concat(New String() {str, download.Name, "(", Conversions.ToString(download.Percent), ") "})
            Loop
        Finally
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        num = CInt(Math.Round(Conversion.Int(CDbl(((CDbl(num) / CDbl((Me._Clients.Count * 100))) * 100)))))
        Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
        If (Not workStatusEvent Is Nothing) Then
            workStatusEvent.Invoke(String.Format("{0} ... {1}% {2} {3}", New Object() {My.Resources.GPSCamera.Downloading, num, str, Me.m_Motherboard}))
        End If
        If (num = 100) Then
            Dim enumerator2 As IEnumerator = Nothing
            Dim download2 As New Client.Download.ClientDownload
            Dim mx As Communications.GMx = Me._GMx
            Try
                enumerator2 = Me._Clients.Keys.GetEnumerator
                Do While enumerator2.MoveNext
                    Dim str4 As String = Conversions.ToString(enumerator2.Current)
                    download2 = DirectCast(Me._Clients.Item(str4), Client.Download.ClientDownload)
                    Dim name As String = download2.Name
                    If (name = "AGPS") Then
                        mx.Agps = download2.Data
                    Else
                        If (name = "LCS") Then
                            Me.m_DbgPassword = download2.Data
                            Continue Do
                        End If
                        If (name = "DRVG") Then
                            mx.DrvG = download2.Data
                            Continue Do
                        End If
                        If (name = "DRVS") Then
                            mx.DrvS = download2.Data
                            Continue Do
                        End If
                        If (name = "DRVB") Then
                            mx.DrvB = download2.Data
                            Continue Do
                        End If
                        If (name = "DBG") Then
                            mx.Dbg = download2.Data
                            Continue Do
                        End If
                        If (name = "VOC") Then
                            mx.Voc = download2.Data
                            Continue Do
                        End If
                        If (name = "VOT") Then
                            mx.Vot = download2.Data
                            Continue Do
                        End If
                        If (name = "VOW") Then
                            mx.Vow = download2.Data
                        End If
                    End If
                Loop
            Finally
                If TypeOf enumerator2 Is IDisposable Then
                    TryCast(enumerator2, IDisposable).Dispose()
                End If
            End Try
            mx = Nothing
            Me._DownloadTimer.Stop()
            Dim message As String = System.Text.Encoding.UTF8.GetString(download2.Data)
            If ((((message.IndexOf("Error") > -1) And (download2.Name <> "AD")) And (download2.Name <> "PIC")) And (download2.Name <> "DRVB")) Then
                Me.ExceptionProcess(New Exception(message))
            Else
                Dim showPicEvent As ShowPicEventHandler
                Select Case download2.Name
                    Case "PIC"
                        If (download2.Data.Length > &H400) Then
                            Dim stream As System.IO.Stream = New System.IO.MemoryStream(download2.Data)
                            Dim picCompletedEvent As PicCompletedEventHandler = Me.PicCompletedEvent
                            If (Not picCompletedEvent Is Nothing) Then
                                picCompletedEvent.Invoke(New System.Drawing.Bitmap(stream))
                            End If
                        End If
                        If (Me.m_PicArrayList.Count > 0) Then
                            showPicEvent = Me.ShowPicEvent
                            If (Not showPicEvent Is Nothing) Then
                                showPicEvent.Invoke(Me.m_PicArrayList)
                            End If
                        End If
                        Me.GetADPic()
                        Return
                    Case "AD"
                        If (download2.Data.Length > &H400) Then
                            Dim num2 As Integer = 80
                            Dim destinationArray As Byte() = New Byte(((num2 - 1) + 1) - 1) {}
                            Array.ConstrainedCopy(download2.Data, 0, destinationArray, 0, destinationArray.Length)
                            Dim num3 As Short = 0
                            Do
                                Dim buffer4 As Byte() = New Byte(&H10 - 1) {}
                                Array.ConstrainedCopy(destinationArray, (num3 * &H10), buffer4, 0, &H10)
                                If (Convert.ToBase64String(buffer4) <> Convert.ToBase64String(Tools.DataConvert.GetBytes(&H10, &HFF))) Then
                                    Dim buffer5 As Byte() = New Byte(4 - 1) {}
                                    Dim buffer2 As Byte() = New Byte(4 - 1) {}
                                    Array.ConstrainedCopy(buffer4, 0, buffer5, 0, 4)
                                    Array.ConstrainedCopy(buffer4, 4, buffer2, 0, 4)
                                    Dim buffer3 As Byte() = New Byte((Conversions.ToInteger(Operators.SubtractObject(Operators.SubtractObject(Tools.DataConvert.ByteToDecimal(buffer2), Tools.DataConvert.ByteToDecimal(buffer5)), 1)) + 1) - 1) {}
                                    Array.ConstrainedCopy(download2.Data, Conversions.ToInteger(Tools.DataConvert.ByteToDecimal(buffer5)), buffer3, 0, buffer3.Length)
                                    Dim stream2 As System.IO.Stream = New System.IO.MemoryStream(buffer3)
                                    Me.m_PicArrayList.Add(New System.Drawing.Bitmap(stream2))
                                End If
                                num3 = CShort((num3 + 1))
                            Loop While (num3 <= 4)
                            If (Me.m_PicArrayList.Count > 0) Then
                                showPicEvent = Me.ShowPicEvent
                                If (Not showPicEvent Is Nothing) Then
                                    showPicEvent.Invoke(Me.m_PicArrayList)
                                End If
                            End If
                        End If
                        Me.GetModel()
                        Return
                    Case "MODEL"
                        Dim strArray As String() = message.Split(New Char() {"|"c})
                        Dim num7 As Short = CShort((strArray.Length - 1))
                        Dim i As Short = 0
                        Do While (i <= num7)
                            Dim strArray2 As String() = strArray(i).Split(New Char() {":"c})
                            If (strArray2(0) = "SimilarModel") Then
                                If (strArray2(1).IndexOf(";") > -1) Then
                                    Dim strArray3 As String() = strArray2(1).Split(New Char() {";"c})
                                    Dim num8 As Short = CShort((strArray3.Length - 1))
                                    Dim j As Short = 0
                                    Do While (j <= num8)
                                        Dim strArray4 As String() = strArray3(j).Split(New Char() {">"c})
                                        Me.m_SimilarMotherboard.Add(strArray4(0), New String() {strArray4(1), strArray4(2)})
                                        Me.m_SimilarType.Add(strArray4(0))
                                        j = CShort((j + 1))
                                    Loop
                                Else
                                    Dim strArray5 As String() = strArray2(1).Split(New Char() {">"c})
                                    Me.m_SimilarMotherboard.Add(strArray5(0), New String() {strArray5(1), strArray5(2)})
                                    Me.m_SimilarType.Add(strArray5(0))
                                End If
                            End If
                            If (strArray2(0) = "Booree") Then
                                Me.m_Booree = Conversions.ToBoolean(Interaction.IIf((strArray2(1) = "Y"), True, False))
                            End If
                            i = CShort((i + 1))
                        Loop
                        Me.CheckDevice()
                        Return
                    Case "SIZE"
                        Me.m_SizeInfo.Clear()
                        If (message.IndexOf("|") > -1) Then
                            Dim strArray6 As String() = message.Split(New Char() {"|"c})
                            Dim num9 As Short = CShort((strArray6.Length - 1))
                            Dim k As Short = 0
                            Do While (k <= num9)
                                Dim strArray7 As String() = strArray6(k).Split(New Char() {":"c})
                                Me.m_SizeInfo.Add(strArray7(0), strArray7(1))
                                k = CShort((k + 1))
                            Loop
                            If (Me.m_SizeInfo.Count = 0) Then
                                Me.ExceptionProcess(New Exception(My.Resources.GPSCamera.SizeIsNull))
                                Return
                            End If
                            Me.SetBaseData()
                            If Me.m_CanStartWork Then
                                Me.GetData("INFO")
                            Else
                                Me.GetData("DRVG")
                            End If
                        End If
                        Return
                    Case "INFO"
                        Me.ShowDbgMap(download2.Data)
                        Return
                    Case "TIME"
                        Me.CompareTickAndVersion(System.Text.Encoding.UTF8.GetString(download2.Data))
                        Return
                    Case "LCS"
                        Me.DownloadDrvG()
                        Return
                    Case "DRVG", "AGPS"
                        Me.DeviceInitialize()
                        Return
                    Case "DBG"
                        Me.m_DbgVersion = Me.GetDbgVersion(Me._GMx.Dbg)
                        Dim gotDbgVersionEvent As GotDbgVersionEventHandler = Me.GotDbgVersionEvent
                        If (Not gotDbgVersionEvent Is Nothing) Then
                            gotDbgVersionEvent.Invoke(Me.m_DbgVersion)
                        End If
                        Dim mx2 As Communications.GMx = Me._GMx
                        Dim dbg As Byte() = mx2.Dbg
                        Me.CheckDbgCounts(dbg)
                        mx2.Dbg = dbg
                        Me.DeviceUpdate()
                        Return
                End Select
                Me.DeviceUpdate()
            End If
        End If
    End Sub



    Private Sub DownloadUpdateBinFile(ByVal pItems As ArrayList)
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.DownloadSoftware)
            End If
            Me._Clients.Clear()
            Dim num3 As Short = CShort((pItems.Count - 1))
            Dim i As Short = 0
            Do While (i <= num3)
                Dim objArray As Object() = New Object(1 - 1) {}
                Dim list As ArrayList = pItems
                Dim num4 As Short = i
                objArray(0) = System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(list.Item(num4))
                Dim arguments As Object() = objArray
                Dim copyBack As Boolean() = New Boolean() {True}
                If copyBack(0) Then
                    list.Item(num4) = System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(arguments(0))
                End If
                Dim pName As String = Conversions.ToString(NewLateBinding.LateGet(Nothing, GetType(Strings), "UCase", arguments, Nothing, Nothing, copyBack))
                Dim pSN As String = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(Me.m_ChipInfo.SN))
                Dim pRN As String = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(Me.m_HostInfo.Password))
                Dim download As New Client.Download.ClientDownload(pName)
                Dim download2 As Client.Download.ClientDownload = download
                If (download2.Name = "USERDBG") Then
                    ' Added for database updates
                    download2.Url = Me.GetServer(download2.Name, pSN, pRN)
                    download2.Url = New Uri((download2.Url.AbsoluteUri & "&filepath=" & Me.FilePath))
                    download2.FileName = Me.FilePath
                    AddHandler download2.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
                    Dim uploadThread As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf download.UploadFileAsync))
                    uploadThread.Start()
                Else
                    Select Case download2.Name
                        Case "DBG"
                            download2.Url = Me.GetServer(download2.Name, pSN, pRN)
                            Dim str4 As String = ""
                            Dim num5 As Short = CShort((Me.m_ChooseCounty.Count - 1))
                            Dim j As Short = 0
                            Do While (j <= num5)
                                str4 = (str4 & Me.m_ChooseCounty.Item(j).ToString & "|")
                                j = CShort((j + 1))
                            Loop
                            str4 = str4.Remove((str4.Length - 1), 1)
                            download2.Url = New Uri(String.Concat(New String() {download2.Url.AbsoluteUri, "&area=", Me.m_DbgArea, "&county=", str4}))
                            Exit Select
                        Case "VOT", "VOC", "VOW"
                            download2.Url = Me.GetServer(download2.Name, pSN, pRN)
                            download2.Url = New Uri(Conversions.ToString(Operators.ConcatenateObject(((download2.Url.AbsoluteUri & "&area=") & Me.m_DbgArea & "&type="), Me.m_SimilarType.Item(Me.m_SimilarIndex))))
                            Exit Select
                        Case Else
                            download2.Url = Me.GetServer(download2.Name, "", "")
                            Exit Select
                    End Select
                    AddHandler download2.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
                    Dim downloadThread As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf download.DownloadAsync))
                    downloadThread.Start()
                End If
                Me._Clients.Remove(download2.Name)
                Me._Clients.Add(download2.Name, download)
                download2 = Nothing
                i = CShort((i + 1))
            Loop
            Me._DownloadTimer.Start()
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub







    Private Sub ExceptionProcess(ByVal ex As Exception)
        Me._GMx.Close()
        Me.m_IsWork = False
        Me._WorkTimer.Stop()
        Dim workCompleteEvent As WorkCompliteEventHandler = Me.WorkCompleteEvent
        If (Not workCompleteEvent Is Nothing) Then
            workCompleteEvent.Invoke(ex)
        End If
    End Sub

    Private Sub GetADPic()
        Me.GetModel()
    End Sub

    Private Sub GetData(ByVal pItem As String)
        Dim num As Integer
        Try
            Me._Clients.Clear()
            Dim download As New Client.Download.ClientDownload(pItem)
            Dim download2 As Client.Download.ClientDownload = download
            Select Case pItem
                Case "AD", "PIC", "MODEL"
                    download2.Url = Me.GetServer(download2.Name, "", "")
                    download2.Url = New Uri((download2.Url.AbsoluteUri & "&type=" & Me.Type))
                    Exit Select
                Case "DRVG", "DRVS"
                    download2.Url = Me.GetServer(download2.Name, "", "")
                    download2.Url = New Uri(Conversions.ToString(Operators.ConcatenateObject((download2.Url.AbsoluteUri & "&type="), Me.m_SimilarType.Item(Me.m_SimilarIndex))))
                    Exit Select
                Case "SIZE"
                    download2.Url = Me.GetServer(download2.Name, "", "")
                    Exit Select
                Case "INFO"
                    Dim pSN As String = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(Me.m_ChipInfo.SN))
                    download2.Url = Me.GetServer(download2.Name, pSN, "")
                    download2.Url = New Uri((download2.Url.AbsoluteUri & "&area=" & Me.m_DbgArea))
                    Exit Select
                Case "TIME"
                    download2.Url = Me.GetServer(download2.Name, "", "")
                    download2.Url = New Uri(Conversions.ToString(Operators.ConcatenateObject(((download2.Url.AbsoluteUri & "&area=") & Me.m_DbgArea & "&type="), Me.m_SimilarType.Item(Me.m_SimilarIndex))))
                    Exit Select
            End Select
            AddHandler download2.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
            Dim downloadThread As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf download2.DownloadAsync))
            downloadThread.Start()
            Me._Clients.Remove(download.Name)
            num = 1
            Me._Clients.Add(download.Name, download)
            download2 = Nothing
            Me._DownloadTimer.Start()
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1, num)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub






    Private Sub GetDbgArea()
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.GetAreaAccess)
            End If
            Dim pItems As New ArrayList
            If Me.m_Hot_Voc Then
                pItems.Add("VOC")
            End If
            If Me.m_Hot_Dbg Then
                pItems.Add("USERDBG")
            End If
            If Me.m_Hot_Vot Then
                pItems.Add("VOT")
            End If
            If Me.m_Hot_Vow Then
                pItems.Add("VOW")
            End If
            Me.DownloadUpdateBinFile(pItems)
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Function GetDbgVersion(ByVal pData As Byte()) As String
        Dim str As String
        Try
            Dim num2 As Integer
            Dim str2 As String = ""
            Dim index As Integer = 4
            Do
                str2 = (str2 & Conversions.ToString(Strings.Chr(pData(index))))
                index += 1
                num2 = 7
            Loop While (index <= num2)
            str = str2
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            str = ""
            ProjectData.ClearProjectError()
            Return str
            ProjectData.ClearProjectError()
        End Try
        Return str
    End Function

    Private Sub GetLCS()
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.UserAuthentication)
            End If
            Me._Clients.Clear()
            Dim str As String
            For Each str In New String() {"LCS"}
                Dim download As New Client.Download.ClientDownload(str)
                Dim download2 As Client.Download.ClientDownload = download
                download2.Url = Me.GetServer(download2.Name, "", "")
                AddHandler download2.DownloadError, New Client.Download.ClientDownload.DownloadErrorEventHandler(AddressOf Me.ClientDownload_DownloadError)
                Dim downloadThread As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf download2.DownloadAsync))
                downloadThread.Start()
                Me._Clients.Add(download.Name, download)
                download2 = Nothing
            Next
            Me._DownloadTimer.Start()
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub


    Private Sub GetModel()
        If (Me.m_SimilarMotherboard.Count = 0) Then
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.CheckModel)
            End If
            Me.GetData("MODEL")
        Else
            Me.CheckDevice()
        End If
    End Sub



    Private Function GetServer(ByVal pItemName As String, Optional ByVal pSN As String = "", Optional ByVal pRN As String = "") As Uri
        Dim str As String = String.Format(Me.m_UrlParameters, New Object() {pItemName, pSN, pRN, Me.m_UserId, Me.m_UserPassword, Me.m_Motherboard})
        Return New Uri(String.Format("{0}?{1}", String.Format(Me.m_Url, System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(Data.GetServer(Me.m_SelectLng).Item(1), New Object() {Me.m_ServerIndex}, Nothing))), str))
    End Function

    Private Sub ReadChipInfo()
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.ReadingInfo)
            End If
            Dim num As Integer = 0
            Dim left As Object = Me.m_SizeInfo.Item("Booree")
            If Operators.ConditionalCompareObjectEqual(left, "Y", False) Then
                Do
                    If (num >= 5) Then
                        Throw New Exception(My.Resources.GPSCamera.FindDeviceFailed)
                    End If
                    num += 1
                    System.Threading.Thread.Sleep(&H3E8)
                Loop While Not Me._GMx.DeviceDetect
                If Not Me._GMx.IsOpen Then
                    Me._GMx.Open()
                End If
                Me._GMx.DiscardOutBuffer()
                Me._GMx.Close()
                System.Threading.Thread.Sleep(&HBB8)
                If ((Convert.ToBase64String(Me.m_ChipInfo.SN) = Convert.ToBase64String(Tools.DataConvert.GetBytes(&H10, &HFF))) OrElse (Convert.ToBase64String(Me.m_ChipInfo.SN) = Convert.ToBase64String(Tools.DataConvert.GetBytes(&H10, 0)))) Then
                    Me.m_ChipInfo = New DeviceInformation.ChipInfo(Me._GMx.ReadChipInfo(Me.m_Count))
                End If
                Me.m_HostInfo = New DeviceInformation.HostInfo(Me._GMx.ReadHostInfo(Me.m_Count, 0))
            ElseIf Operators.ConditionalCompareObjectEqual(left, "N", False) Then
                Do
                    If (num >= 10) Then
                        Throw New TimeoutException
                    End If
                    num += 1
                Loop While Not Me._GMx.DeviceDetect
                If Not Me._GMx.IsOpen Then
                    Me._GMx.Open()
                End If
                Me._GMx.DiscardOutBuffer()
                Me._GMx.Close()
                System.Threading.Thread.Sleep(&H3E8)
                Me.m_ChipInfo = New DeviceInformation.ChipInfo(Me._GMx.ReadChipInfo)
                Me.m_HostInfo = New DeviceInformation.HostInfo(Me._GMx.ReadHostInfo(0))
            End If
            If ((Me.m_ChipInfo Is Nothing) Or (Me.m_HostInfo Is Nothing)) Then
                workStatusEvent = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(My.Resources.GPSCamera.ReadingInfoFailed)
                End If
                System.Threading.Thread.Sleep(&H1388)
                Me.DeviceReset(True)
            Else
                If Me.m_Hot_Dbg Then
                    Me.m_HostInfo.Password = DirectCast(Me.m_DbgPassword.Clone, Byte())
                End If
                Me.GetDbgArea()
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub ReadWriteDeviceInfo()
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.DeviceInfoUpdate)
            End If
            If Not String.IsNullOrEmpty(Me.m_DbgVersion) Then
                Dim info2 As DeviceInformation.HostInfo = Me.m_HostInfo
                info2.DatabaseVersion = Tools.DataConvert.TextToByte(Me.m_DbgVersion, &H10, &HFF)
                If Me.m_Hot_Dbg Then
                    info2.DatabaseUpdateUTC = Tools.DataConvert.DateTimeToByte(DateTime.UtcNow, &H10, &HFF)
                    info2.County = Me.m_CountyByte
                End If
                If Me.m_Hot_Voc Then
                    info2.VoiceUpdateUTC = Tools.DataConvert.DateTimeToByte(DateTime.UtcNow, &H10, &HFF)
                End If
                info2 = Nothing
            End If
            If Me.m_HaveToWriteCompany Then
                Me.m_HostInfo.Company = Tools.DataConvert.TextToByte(Me.m_DbgArea, &H10, &HFF)
            End If
            If Me.m_HaveToWriteMotherboard Then
                Me.m_HostInfo.Model = Tools.DataConvert.TextToByte(Me.m_Motherboard, &H10, &HFF)
            End If
            Dim info As DeviceInformation.HostInfo = Nothing
            Dim flag As Boolean = False
            Dim num3 As Short = CShort((Me.m_Count - 1))
            Dim i As Short = 0
            Do While (i <= num3)
                Dim left As Object = Me.m_SizeInfo.Item("Booree")
                If Operators.ConditionalCompareObjectEqual(left, "Y", False) Then
                    Dim info3 As DeviceInformation.HostInfo = Me.m_HostInfo
                    If (Me.m_HaveToWriteCompany AndAlso Not Me._GMx.WriteHostInfo(info3.Company, 0, Communications.HostInfo.Company)) Then
                        Throw New Exception("Company info update break!")
                    End If
                    If Me.m_Hot_Dbg Then
                        If Not Me._GMx.WriteHostInfo(info3.Password, 0, Communications.HostInfo.Password) Then
                            Throw New Exception("Device info update break!")
                        End If
                        If Not Me._GMx.WriteHostInfo(info3.DatabaseVersion, 0, Communications.HostInfo.DbgVersion) Then
                            Throw New Exception("Version info update break!")
                        End If
                        If Not Me._GMx.WriteHostInfo(info3.DatabaseUpdateUTC, 0, Communications.HostInfo.DbgUpdateTime) Then
                            Throw New Exception("Update time update break!")
                        End If
                    End If
                    If Not Me._GMx.WriteHostInfo(info3.VoiceUpdateUTC, 0, Communications.HostInfo.VoiceUpdateTime) Then
                        Throw New Exception("Voice updated time update break!")
                    End If
                    If Not Me._GMx.WriteHostInfo(info3.Product, 0, Communications.HostInfo.Product) Then
                        Throw New Exception("Product updated time update break!")
                    End If
                    If Not Me._GMx.WriteHostInfo(info3.County, 0, Communications.HostInfo.County) Then
                        Throw New Exception("Selected province update break!")
                    End If
                    If (Me.m_HaveToWriteMotherboard AndAlso Not Me._GMx.WriteHostInfo(info3.Model, 0, Communications.HostInfo.Motherboard)) Then
                        Throw New Exception("Motherboard province update break!")
                    End If
                    info3 = Nothing
                ElseIf (Operators.ConditionalCompareObjectEqual(left, "N", False) AndAlso Not Me._GMx.WriteHostInfo(Me.m_HostInfo.GetFullData, 0)) Then
                    Throw New Exception("Device info update break!")
                End If
                System.Threading.Thread.Sleep(&H3E8)
                info = New DeviceInformation.HostInfo(Me._GMx.ReadHostInfo(Me.m_Count, 0))
                Dim fullData As Byte() = Me.m_HostInfo.GetFullData
                Dim buffer2 As Byte() = info.GetFullData
                Dim num4 As Short = CShort((fullData.Length - 1))
                Dim j As Short = 0
                Do While (j <= num4)
                    If (fullData(j) <> buffer2(j)) Then
                        flag = True
                        Exit Do
                    End If
                    j = CShort((j + 1))
                Loop
                If Not flag Then
                    Exit Do
                End If
                i = CShort((i + 1))
            Loop
            Me.DeviceReset(False)
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Public Sub Run()
        Dim workTimeEvent As WorkTimeEventHandler = Me.WorkTimeEvent
        If (Not workTimeEvent Is Nothing) Then
            workTimeEvent.Invoke("00:00:00")
        End If
        Me._WorkTimer.Play()
        Me.m_IsWork = True
        Me.WorkStart()
    End Sub


    Private Sub SetBaseData()
        Dim left As Object = Me.m_SizeInfo.Item("Booree")
        If Operators.ConditionalCompareObjectEqual(left, "Y", False) Then
            Me._GMx.SetBaudRate = &H1C2000
        ElseIf Operators.ConditionalCompareObjectEqual(left, "N", False) Then
            Me._GMx.SetBaudRate = &H1C200
        End If
    End Sub

    Private Sub ShowDbgMap(ByVal pData As Byte())
        Try
            If (pData.Length = 0) Then
                Me.GetData("INFO")
            End If
            Dim hashtable As New Hashtable
            Dim num As Integer = 0
            Dim num2 As Integer = 0
            Me.m_CountyByte = Tools.DataConvert.GetBytes(&H10, &HFF)
            Dim str As String = ""
            Dim index As Integer = 0
            Do
                Dim str2 As String = pData(index).ToString("X2")
                str = (str & str2)
                index += 1
            Loop While (index <= 3)
            num = CInt(Convert.ToInt64(str, &H10))
            Me.m_PreviousCounty.Clear()
            Dim num13 As Integer = (pData.Length - 20)
            Dim i As Integer = 20
            Do While (i <= num13)
                Dim str4 As String = ""
                Dim str3 As String = ""
                Dim num5 As Integer = 0
                Do
                    If (pData((i + num5)) <> &HFF) Then
                        str4 = (str4 & Conversions.ToString(Strings.Chr(pData((i + num5)))))
                    End If
                    num5 += 1
                Loop While (num5 <= 7)
                str4 = Strings.Trim(str4)
                Dim num6 As Integer = &H10
                Do
                    Dim str5 As String = pData((i + num6)).ToString("X2")
                    str3 = (str3 & str5)
                    num6 += 1
                Loop While (num6 <= &H13)
                str3 = Conversions.ToString(Convert.ToInt64(str3, &H10))
                hashtable.Add(str4, str3)
                num2 = CInt(Math.Round(CDbl((num2 + Conversion.Val(str3)))))
                i = (i + 20)
            Loop
            If (Convert.ToBase64String(Me.m_HostInfo.County) <> Convert.ToBase64String(Tools.DataConvert.GetBytes(&H10, &HFF))) Then
                Dim destinationArray As Byte() = New Byte(5 - 1) {}
                Array.ConstrainedCopy(Me.m_HostInfo.County, 0, destinationArray, 0, 5)
                Dim num14 As Short = CShort((destinationArray.Length - 1))
                Dim j As Short = 0
                Do While (j <= num14)
                    Dim num8 As Short = 0
Label_019F:
                    If ((CDbl((destinationArray(j) And CLng(Math.Round(Math.Pow(2, CDbl(num8)))))) / Math.Pow(2, CDbl(num8))) <> 0) Then
                        Dim falsePart As Short = CShort(((&H41 + (j * 8)) + num8))
                        Dim key As String = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("CN_", Interaction.IIf((falsePart < &H5B), "A", "B")), Strings.Chr(Conversions.ToInteger(Interaction.IIf((falsePart > 90), (&H40 + (falsePart Mod 90)), falsePart)))))
                        Me.m_PreviousCounty.Add(key, key)
                    End If
                    If Not ((j = (destinationArray.Length - 1)) And (num8 = 0)) Then
                        num8 = CShort((num8 + 1))
                        If (num8 <= 7) Then
                            GoTo Label_019F
                        End If
                    End If
                    j = CShort((j + 1))
                Loop
            End If
            If (hashtable.Count = 1) Then
                Dim enumerator As IDictionaryEnumerator = hashtable.GetEnumerator
                Do While enumerator.MoveNext
                    Dim current As DictionaryEntry = DirectCast(enumerator.Current, DictionaryEntry)
                    If Operators.ConditionalCompareObjectEqual(current.Key, "TW", False) Then
                        Me.m_CountyByte(0) = 0
                        Me.m_CountyByte(1) = 0
                        Me.m_CountyByte(2) = 0
                        Me.m_CountyByte(3) = 0
                        Me.m_CountyByte(4) = CByte((Me.m_CountyByte(4) And CLng(Math.Round(CDbl((255 - Math.Pow(2, 0)))))))
                    End If
                    Me.m_ChooseCounty.Add(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(current.Key))
                Loop
                Me.GetData("TIME")
            Else
                If (hashtable.Count = 0) Then
                    Throw New Exception(My.Resources.GPSCamera.AreaCodeFailure)
                End If
                Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(My.Resources.GPSCamera.ChoiceArea)
                End If
                Dim map As New GPSCamera.Windows.Forms.dlgShowDbgMap With { _
                    .DbgInfo = hashtable _
                }
                If (num2 > num) Then
                    map.PreviousCounty = Me.m_PreviousCounty
                    map.Amount = num
                    map.ShowDialog()
                Else
                    map.SetCountyCode()
                End If
                Me.m_ChooseCounty.Clear()
                If (map.DialogResult = System.Windows.Forms.DialogResult.OK) Then
                    Me.m_ChooseCounty = map.ChooseCounty
                    If (Me.m_ChooseCounty.Count = Me.m_PreviousCounty.Count) Then
                        Dim num15 As Short = CShort((Me.m_ChooseCounty.Count - 1))
                        Dim k As Short = 0
                        Do While (k <= num15)
                            If Not Me.m_PreviousCounty.ContainsKey(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Me.m_ChooseCounty.Item(k))) Then
                                Me.m_HaveToUpdate = True
                                Exit Do
                            End If
                            k = CShort((k + 1))
                        Loop
                    Else
                        Me.m_HaveToUpdate = True
                    End If
                    Dim enumerator2 As IDictionaryEnumerator = map.CountyCode.GetEnumerator
                    Do While enumerator2.MoveNext
                        Dim entry2 As DictionaryEntry = DirectCast(enumerator2.Current, DictionaryEntry)
                        Dim num12 As Short = CShort((CLng(Math.Round(Conversion.Val(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(entry2.Value)))) / 8))
                        Dim num11 As Short = CShort(Math.Round(CDbl((Conversion.Val(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(entry2.Value)) Mod 8))))
                        Me.m_CountyByte(num12) = CByte((Me.m_CountyByte(num12) And CLng(Math.Round(CDbl((255 - Math.Pow(2, CDbl(num11))))))))
                    Loop
                    Me.m_CountyByte(4) = CByte((Me.m_CountyByte(4) And CLng(Math.Round(CDbl((255 - Math.Pow(2, 1)))))))
                    Me.GetData("TIME")
                Else
                    Me.ExceptionProcess(New Exception(My.Resources.GPSCamera.Cancel))
                End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub


    Private Sub WorkEnd(Optional ByVal pUpdateHasError As Boolean = False)
        Try
            Dim pMessage As String = ""
            If pUpdateHasError Then
                pMessage = My.Resources.GPSCamera.DeviceUpdateUnfinished
            Else
                pMessage = My.Resources.GPSCamera.DeviceUpdateCompleted
            End If
            Me._GMx.Close()
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(pMessage)
            End If
            Dim workCompliteEvent As WorkCompliteEventHandler = Me.WorkCompleteEvent
            If (Not workCompliteEvent Is Nothing) Then
                workCompliteEvent.Invoke(Nothing)
            End If
            Me.m_ChooseCounty.Clear()
            Me.m_IsWork = False
            Me._WorkTimer.Stop()
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub





    Private Sub WorkStart()
        Try
            Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
            If (Not workStatusEvent Is Nothing) Then
                workStatusEvent.Invoke(My.Resources.GPSCamera.OperationsBegin)
            End If
            Dim workingEvent As WorkingEventHandler = Me.WorkingEvent
            If (Not workingEvent Is Nothing) Then
                workingEvent.Invoke()
            End If
            Dim i As Integer = (My.Computer.Ports.SerialPortNames.Count - 1)
            Do While (i >= 0)
                Dim pPortName As String = My.Computer.Ports.SerialPortNames.Item(i)
                Dim searchPortEvent As SearchPortEventHandler = Me.SearchPortEvent
                If (Not searchPortEvent Is Nothing) Then
                    searchPortEvent.Invoke(pPortName)
                End If
                Me._GMx.Close()
                Me._GMx.PortName = pPortName
                If Me.m_Booree Then
                    Dim num2 As Integer = 0
                    Do
                        If Me._GMx.DeviceDetect Then
                            Exit Do
                        End If
                        System.Threading.Thread.Sleep(200)
                        num2 += 1
                    Loop While (num2 <= 4)
                End If
                If Me._GMx.DeviceDetect Then
                    Me._GMx.Close()
                    searchPortEvent = Me.SearchPortEvent
                    If (Not searchPortEvent Is Nothing) Then
                        searchPortEvent.Invoke(pPortName)
                    End If
                    Me.m_HaveToUpdate = False
                    Me.m_CanStartWork = False
                    Me.m_InBoot = False
                    Me.m_SimilarIndex = -1
                    If (Me.m_ChooseCounty.Count > 0) Then
                        If Me.CheckSameId Then
                            If (Me.m_ChooseCounty.Count = Me.m_PreviousCounty.Count) Then
                                Dim num4 As Short = CShort((Me.m_ChooseCounty.Count - 1))
                                Dim j As Short = 0
                                Do While (j <= num4)
                                    If Not Me.m_PreviousCounty.ContainsKey(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Me.m_ChooseCounty.Item(j))) Then
                                        Me.m_HaveToUpdate = True
                                        Exit Do
                                    End If
                                    j = CShort((j + 1))
                                Loop
                            Else
                                Me.m_HaveToUpdate = True
                            End If
                        Else
                            Me.m_ChooseCounty.Clear()
                        End If
                    End If
                    If Me.m_IsPublicPic Then
                        Me.GetData("PIC")
                    Else
                        Me.GetADPic()
                    End If
                    Return
                End If
                i = (i + -1)
            Loop
            Throw New Exception(My.Resources.GPSCamera.NoFindDevice)
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub



    Private Sub WorkTimer_TimerElapsed(ByVal pTime As String)
        Dim workTimeEvent As WorkTimeEventHandler = Me.WorkTimeEvent
        If (Not workTimeEvent Is Nothing) Then
            workTimeEvent.Invoke(pTime)
        End If
    End Sub

    Private Sub WriteBooree()
        Try
            If (Me._GMx.DrvB.Length >= 100) Then
                System.Threading.Thread.Sleep(&HBB8)
                Dim workStatusEvent As WorkStatusEventHandler = Me.WorkStatusEvent
                If (Not workStatusEvent Is Nothing) Then
                    workStatusEvent.Invoke(String.Format("{0}{1}", My.Resources.GPSCamera.DeviceRevise, Me.m_Motherboard))
                End If
                If Not Me._GMx.SwitchToBooree Then
                    Throw New Exception(My.Resources.GPSCamera.SwitchToBooreeFailure)
                End If
                System.Threading.Thread.Sleep(&HBB8)
                If Not Me._GMx.GoToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree"))) Then
                    Throw New Exception(My.Resources.GPSCamera.DeviceReviseFailure)
                End If
                Me._GMx.Write(Communications.DataItem.DrvB, Conversions.ToShort(Me.m_SizeInfo.Item("Drv")))
                If Not Me._GMx.FinishToWork(Conversions.ToString(Me.m_SizeInfo.Item("Booree")), False) Then
                    Throw New Exception(My.Resources.GPSCamera.DeviceReviseFailure)
                End If
                System.Threading.Thread.Sleep(&HBB8)
                If Not Me._GMx.SwitchToAtmel Then
                    Throw New Exception(My.Resources.GPSCamera.SwitchToAtmel)
                End If
                System.Threading.Thread.Sleep(&HBB8)
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim ex As Exception = exception1
            Me.ExceptionProcess(ex)
            ProjectData.ClearProjectError()
        End Try
    End Sub






#Region "    Constructor / Destructor "

    Public Sub New()

        Me._GMx = New Communications.GMx
        Me._WorkTimer = New WorkTimer
        Me.m_SimilarMotherboard = New Hashtable
        Me.m_PicArrayList = New ArrayList
        Me.m_Type = ""
        Me.m_UserId = ""
        Me.m_UserPassword = ""
        Me.m_SelectLng = ""
        Me.pNull = Convert.ToBase64String(Tools.DataConvert.GetBytes(&H10, 0))
        Me.pNull2 = Convert.ToBase64String(Tools.DataConvert.GetBytes(&H10, &HFF))
        Me.m_SimilarType = New ArrayList
        Me.m_SimilarIndex = -1
        Me.m_SizeInfo = New Hashtable
        Me.m_Motherboard = ""
        Me.m_Hot_Dbg = False
        Me.m_Hot_Voc = False
        Me.m_Hot_Vot = False
        Me.m_Hot_Vow = False
        Me.m_ChooseCounty = New ArrayList
        Me.m_PreviousCounty = New Hashtable
        Me.m_CountyByte = Nothing
        Me.m_InBoot = False
        Me.m_HaveToWriteCompany = False
        Me.m_DbgArea = ""
        Me.m_DbgPassword = Nothing
        Me.m_DbgVersion = ""
        Me.m_Count = 5
        Me._Client = New Client.Download.ClientDownload
        Me._Clients = New Client.Download.ClientDownloadCollection
        Me._DownloadTimer = New System.Timers.Timer
        Me.m_UrlParameters = "item={0}&sn={1}&rn={2}&uid={3}&upd={4}&model={5}"
        Me.m_Url = "http://{0}/GM/com_data_file.aspx"
        Me._DownloadTimer.Interval = 1000
        Me.m_IsWork = False
        Me.m_FilePath = String.Empty

    End Sub

#End Region

End Class

