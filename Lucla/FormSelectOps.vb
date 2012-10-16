Public Class FormSelectOps

    Private m_DialogResult As CustomDialogResult

    Public Enum CustomDialogResult
        Firmware = 0
        Database = 1
        Ignore = 999
    End Enum

    Private Sub FormSelectOps_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Ugly but works!</remarks>
    Public Shadows Function ShowDialog() As CustomDialogResult
        Try
            Dim modalThread As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf MyBase.ShowDialog))
            modalThread.Start()

            modalThread.Join()

            Return m_DialogResult
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Public Shadows Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window) As DialogResult
    '    If (MyBase.ShowDialog(owner) = System.Windows.Forms.DialogResult.OK) Then
    '        Return m_DialogResult
    '    Else
    '        Return DialogResults.Database
    '    End If
    'End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        m_DialogResult = CustomDialogResult.Firmware
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        m_DialogResult = CustomDialogResult.Database
        'MyBase.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class