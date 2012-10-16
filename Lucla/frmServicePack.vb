Imports Microsoft.VisualBasic.CompilerServices

Public Class frmServicePack

    <System.Runtime.CompilerServices.AccessedThroughProperty("_UpdateProcess")> _
    Private __UpdateProcess As GPSCamera.UpdateProcess
    Private m_PicArrayList As ArrayList
    Private m_PicIndex As Short
    Private m_ProductPic As Bitmap
    Private m_SelectLng As String
    Private m_ServerIndex As Short
    Private m_Timer As System.Timers.Timer
    Private m_Type As String

#Region "    Delegates "

    Private Delegate Sub Delegate_DisplayDbgVersion(ByVal pVersion As String)
    Private Delegate Sub Delegate_DisplayPicCompleted(ByVal pPicture As Bitmap)
    Private Delegate Sub Delegate_DisplayPort(ByVal pPortName As String)
    Private Delegate Sub Delegate_DisplayShowPic(ByVal pPicArrayList As ArrayList)
    Private Delegate Sub Delegate_DisplayStatus(ByVal pStatus As String)
    Private Delegate Sub Delegate_DisplayTime(ByVal pTime As String)
    Private Delegate Sub Delegate_GuiControl(ByVal pEnabled As Boolean)

#End Region

    Private Property _UpdateProcess As GPSCamera.UpdateProcess
        <DebuggerNonUserCode()> _
        Get
            Return Me.__UpdateProcess
        End Get
        <System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized), DebuggerNonUserCode()> _
        Set(ByVal WithEventsValue As GPSCamera.UpdateProcess)
            If (Not Me.__UpdateProcess Is Nothing) Then
                RemoveHandler Me.__UpdateProcess.PicCompleted, New GPSCamera.UpdateProcess.PicCompletedEventHandler(AddressOf Me.UpdateProcess_PicCompleted)
                RemoveHandler Me.__UpdateProcess.SearchPort, New GPSCamera.UpdateProcess.SearchPortEventHandler(AddressOf Me.UpdateProcess_SearchPort)
                RemoveHandler Me.__UpdateProcess.GotDbgVersion, New GPSCamera.UpdateProcess.GotDbgVersionEventHandler(AddressOf Me.UpdateProcess_GotDbgVersion)
                RemoveHandler Me.__UpdateProcess.WorkTime, New GPSCamera.UpdateProcess.WorkTimeEventHandler(AddressOf Me.UpdateProcess_WorkTime)
                RemoveHandler Me.__UpdateProcess.WorkStatus, New GPSCamera.UpdateProcess.WorkStatusEventHandler(AddressOf Me.UpdateProcess_WorkStatus)
                RemoveHandler Me.__UpdateProcess.Working, New GPSCamera.UpdateProcess.WorkingEventHandler(AddressOf Me.UpdateProcess_Working)
                RemoveHandler Me.__UpdateProcess.WorkComplete, New GPSCamera.UpdateProcess.WorkCompliteEventHandler(AddressOf Me.UpdateProcess_WorkComplete)
                RemoveHandler Me.__UpdateProcess.ShowPic, New GPSCamera.UpdateProcess.ShowPicEventHandler(AddressOf Me.UpdateProcess_ShowPic)
            End If
            Me.__UpdateProcess = WithEventsValue
            If (Not Me.__UpdateProcess Is Nothing) Then
                AddHandler Me.__UpdateProcess.PicCompleted, New GPSCamera.UpdateProcess.PicCompletedEventHandler(AddressOf Me.UpdateProcess_PicCompleted)
                AddHandler Me.__UpdateProcess.SearchPort, New GPSCamera.UpdateProcess.SearchPortEventHandler(AddressOf Me.UpdateProcess_SearchPort)
                AddHandler Me.__UpdateProcess.GotDbgVersion, New GPSCamera.UpdateProcess.GotDbgVersionEventHandler(AddressOf Me.UpdateProcess_GotDbgVersion)
                AddHandler Me.__UpdateProcess.WorkTime, New GPSCamera.UpdateProcess.WorkTimeEventHandler(AddressOf Me.UpdateProcess_WorkTime)
                AddHandler Me.__UpdateProcess.WorkStatus, New GPSCamera.UpdateProcess.WorkStatusEventHandler(AddressOf Me.UpdateProcess_WorkStatus)
                AddHandler Me.__UpdateProcess.Working, New GPSCamera.UpdateProcess.WorkingEventHandler(AddressOf Me.UpdateProcess_Working)
                AddHandler Me.__UpdateProcess.WorkComplete, New GPSCamera.UpdateProcess.WorkCompliteEventHandler(AddressOf Me.UpdateProcess_WorkComplete)
                AddHandler Me.__UpdateProcess.ShowPic, New GPSCamera.UpdateProcess.ShowPicEventHandler(AddressOf Me.UpdateProcess_ShowPic)
            End If
        End Set
    End Property


#Region "   UpdateProcess Events "

    Private Sub UpdateProcess_GotDbgVersion(ByVal pVersion As String)
        Me.DisplayDbgVersion(Conversions.ToString(Interaction.IIf(Not String.IsNullOrEmpty(pVersion), ("V" & pVersion), "")))
    End Sub

    Private Sub UpdateProcess_PicCompleted(ByVal pPicture As Bitmap)
        Me.DisplayPicCompleted(pPicture)
    End Sub

    Private Sub UpdateProcess_SearchPort(ByVal pPortName As String)
        Me.DisplayPort(pPortName)
    End Sub

    Private Sub UpdateProcess_ShowPic(ByVal pPicArrayList As ArrayList)
        Me.DisplayShowPic(pPicArrayList)
    End Sub

    Private Sub UpdateProcess_WorkComplete(ByVal ex As Exception)
        Me.GuiControl(True)
        If (Not ex Is Nothing) Then
            Me.DisplayStatus(ex.Message)
        End If
    End Sub

    Private Sub UpdateProcess_Working()
        Me.GuiControl(False)
    End Sub

    Private Sub UpdateProcess_WorkStatus(ByVal pMessage As String)
        Me.DisplayStatus(pMessage)
    End Sub

    Private Sub UpdateProcess_WorkTime(ByVal pTime As String)
        Me.DisplayTime(pTime)
    End Sub

#End Region

#Region "    Display Messages Helper "

    Private Sub DisplayDbgVersion(ByVal pVersion As String)
        Try
            Dim method As Delegate_DisplayDbgVersion = New Delegate_DisplayDbgVersion(AddressOf Me.XDisplayDbgVersion)
            Me.BeginInvoke(method, New Object() {pVersion})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub DisplayPicCompleted(ByVal pPicture As Bitmap)
        Try
            Dim method As Delegate_DisplayPicCompleted = New Delegate_DisplayPicCompleted(AddressOf Me.XDisplayPicCompleted)
            Me.BeginInvoke(method, New Object() {pPicture})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub DisplayPort(ByVal pPortName As String)
        Try
            Dim method As Delegate_DisplayPort = New Delegate_DisplayPort(AddressOf Me.XDisplayPort)
            Me.BeginInvoke(method, New Object() {pPortName})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub DisplayShowPic(ByVal pPicArrayList As ArrayList)
        Try
            Dim method As Delegate_DisplayShowPic = New Delegate_DisplayShowPic(AddressOf Me.XDisplayShowPic)
            Me.BeginInvoke(method, New Object() {pPicArrayList})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub DisplayStatus(ByVal pStatus As String)
        Try
            Dim method As Delegate_DisplayStatus = New Delegate_DisplayStatus(AddressOf Me.XDisplayStatus)
            Me.BeginInvoke(method, New Object() {pStatus})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub


    Private Sub DisplayTime(ByVal pTime As String)
        Try
            Dim method As Delegate_DisplayTime = New Delegate_DisplayTime(AddressOf Me.XDisplayTime)
            Me.BeginInvoke(method, New Object() {pTime})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub XDisplayDbgVersion(ByVal pVersion As String)
        Me.tssDbgVersion.Text = pVersion
        My.Application.DoEvents()
    End Sub

    Private Sub XDisplayPicCompleted(ByVal pPicture As Bitmap)
        Me.ProductPic = pPicture
        Me.picFace.Image = Me.ProductPic
        My.Application.DoEvents()
    End Sub

    Private Sub XDisplayPort(ByVal pPortName As String)
        Me.tssPort.Text = pPortName
        My.Application.DoEvents()
    End Sub

    Private Sub XDisplayShowPic(ByVal pPicArrayList As ArrayList)
        Me.m_PicArrayList.Clear()
        If (Me.ProductPic.Flags <> My.Resources._Public.Flags) Then
            Me.m_PicArrayList.Add(Me.ProductPic)
        End If
        Dim num3 As Short = CShort((pPicArrayList.Count - 1))
        Dim i As Short = 0
        Do While (i <= num3)
            Me.m_PicArrayList.Add(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(pPicArrayList.Item(i)))
            i = CShort((i + 1))
        Loop
        Dim num4 As Short = CShort((Me.m_PicArrayList.Count - 1))
        Dim j As Short = 0
        Do While (j <= num4)
            Dim label As Label = DirectCast(Me.Controls.Item(("lbbtnPic" & Conversions.ToString(CInt((j + 1))))), Label)
            label.Visible = True
            If (j = 0) Then
                label.BackColor = Color.DimGray
            End If
            j = CShort((j + 1))
        Loop
        Me.m_PicIndex = CShort((Me.m_PicIndex + 1))
        Me.m_Timer.Start()
        My.Application.DoEvents()
    End Sub

    Private Sub XDisplayStatus(ByVal pStatus As String)
        Me.tssStatus.Text = pStatus
        Me.tssStatus.ToolTipText = pStatus
        My.Application.DoEvents()
    End Sub

    Private Sub XDisplayTime(ByVal pTime As String)
        Me.tssTime.Text = pTime
        My.Application.DoEvents()
    End Sub

    Private Sub XGuiControl(ByVal pEnabled As Boolean)
        Me.Panel1.Enabled = pEnabled
        My.Application.DoEvents()
    End Sub

#End Region

    Private Sub GuiControl(ByVal pEnabled As Boolean)
        Try
            Dim method As Delegate_GuiControl = New Delegate_GuiControl(AddressOf Me.XGuiControl)
            Me.BeginInvoke(method, New Object() {pEnabled})
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            ProjectData.ClearProjectError()
        End Try
    End Sub






    Public Property ProductPic As Bitmap
        Get
            Return Me.m_ProductPic
        End Get
        Set(ByVal value As Bitmap)
            m_ProductPic = value
            Me.picFace.Image = value
            '496 x 303
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

    Public Property ServerIndex As Short
        Get
            Return m_ServerIndex
        End Get
        Set(ByVal value As Short)
            ' toDo: sanity check
            m_ServerIndex = value
        End Set
    End Property

    Private Sub m_Timer_Tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        If (Me.m_PicIndex > (Me.m_PicArrayList.Count - 1)) Then
            Me.m_PicIndex = 0
        End If
        Me.picFace.Image = DirectCast(Me.m_PicArrayList.Item(Me.m_PicIndex), Image)
        Dim num As Short = 0
        Do
            Dim label As Label = DirectCast(NewLateBinding.LateGet(Me.Controls, Nothing, "Item", New Object() {Operators.ConcatenateObject("lbbtnPic", Interaction.IIf((num = 0), System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Interaction.IIf((Me.m_PicIndex = 0), Me.m_PicArrayList.Count, Me.m_PicIndex)), (Me.m_PicIndex + 1)))}, Nothing, Nothing, Nothing), Label)
            label.BackColor = DirectCast(Interaction.IIf((num = 0), Color.White, Color.DimGray), Color)
            num = CShort((num + 1))
        Loop While (num <= 1)
        Me.m_PicIndex = CShort((Me.m_PicIndex + 1))
    End Sub

    Private Sub SetTimerValue()
        Dim timer As System.Timers.Timer = Me.m_Timer
        AddHandler timer.Elapsed, New System.Timers.ElapsedEventHandler(AddressOf Me.m_Timer_Tick)
        timer.Stop()
        timer.Interval = 10000
        timer = Nothing
    End Sub



    Private Sub frmServicePack_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmServicePack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DisplayPort("")
        Me.DisplayTime("")
        Me.DisplayStatus("")
        Me.DisplayDbgVersion("")
        Dim strArray As String() = Me.Type.Split(New Char() {"["c})
        Me.Text = String.Format("{0} {1}", System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Interaction.IIf((Me.m_SelectLng = "en"), "", (strArray(1).Replace("!", "+") & " "))), My.Resources.Lucla.TitleName)
        Me.picFace.Image = Me.ProductPic
        Me.SetTimerValue()
        Me.btnUpdate.Text = My.Resources.Lucla.btnUpdate
        Me.ckCheckVersion.Text = My.Resources.Lucla.ckCheckVersion
        Me.lblServer.Text = My.Resources.Lucla.lblServer
        Me.cboServer.Items.Clear()
        Dim list As ArrayList = GPSCamera.Data.GetServer(Me.m_SelectLng)
        Dim num2 As Short = Conversions.ToShort(Operators.SubtractObject(NewLateBinding.LateGet(list.Item(0), Nothing, "Length", New Object(0 - 1) {}, Nothing, Nothing, Nothing), 1))
        Dim i As Short = 0
        Do While (i <= num2)
            Me.cboServer.Items.Add(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(NewLateBinding.LateIndexGet(list.Item(0), New Object() {i}, Nothing)))
            i = CShort((i + 1))
        Loop
        Do While (Me.ServerIndex > (Me.cboServer.Items.Count - 1))
            Me.ServerIndex -= 1
        Loop
        Me.cboServer.SelectedIndex = Me.ServerIndex
    End Sub
    Private Sub pbFace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picFace.Click

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.DisplayDbgVersion("")
        Dim process As GPSCamera.UpdateProcess = Me._UpdateProcess
        process.UserId = "CVIJMDSAUG"
        process.UserPassword = "3B2C6F-B3CCEF-88D9F3-6C2B5A-ABE3CF"
        process.ServerIndex = Me.cboServer.SelectedIndex
        process.Type = Me.Type
        process.CheckVersion = Me.ckCheckVersion.Checked
        process.IsPublicPic = Conversions.ToBoolean(Interaction.IIf((Me.ProductPic.Flags = My.Resources._Public.Flags), True, False))
        process.SelectLng = Me.m_SelectLng
        If (String.IsNullOrEmpty(process.UserId) And String.IsNullOrEmpty(process.UserPassword)) Then
            Interaction.MsgBox(My.Resources.Lucla.MSG_IdAndPasswordCanNotEmpty, MsgBoxStyle.Exclamation, Me.Text)
        Else
            process = Nothing
            Dim process1 As GPSCamera.UpdateProcess = Me._UpdateProcess
            Dim runThread As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf process1.Run))
            runThread.Start()
        End If

    End Sub

    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        AddHandler MyBase.Load, New EventHandler(AddressOf Me.frmServicePack_Load)
        AddHandler MyBase.FormClosing, New FormClosingEventHandler(AddressOf Me.frmServicePack_FormClosing)
        Me._UpdateProcess = New GPSCamera.UpdateProcess
        Me.Type = ""
        Me.ProductPic = Nothing
        Me.m_SelectLng = ""
        Me.m_PicArrayList = New ArrayList
        Me.m_PicIndex = 0
        Me.m_Timer = New System.Timers.Timer


    End Sub
End Class
