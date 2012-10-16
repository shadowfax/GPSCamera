<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServicePack
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.picFace = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tssPort = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssDbgVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.ckCheckVersion = New System.Windows.Forms.CheckBox()
        Me.cboServer = New System.Windows.Forms.ComboBox()
        Me.lblServer = New System.Windows.Forms.Label()
        CType(Me.picFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picFace
        '
        Me.picFace.Dock = System.Windows.Forms.DockStyle.Top
        Me.picFace.Location = New System.Drawing.Point(0, 0)
        Me.picFace.Name = "picFace"
        Me.picFace.Size = New System.Drawing.Size(496, 303)
        Me.picFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picFace.TabIndex = 0
        Me.picFace.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssPort, Me.tssTime, Me.tssDbgVersion, Me.tssStatus})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 360)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.ShowItemToolTips = True
        Me.StatusStrip1.Size = New System.Drawing.Size(496, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tssPort
        '
        Me.tssPort.Name = "tssPort"
        Me.tssPort.Size = New System.Drawing.Size(41, 17)
        Me.tssPort.Text = "tssPort"
        '
        'tssTime
        '
        Me.tssTime.Name = "tssTime"
        Me.tssTime.Size = New System.Drawing.Size(43, 17)
        Me.tssTime.Text = "tssTime"
        '
        'tssDbgVersion
        '
        Me.tssDbgVersion.Name = "tssDbgVersion"
        Me.tssDbgVersion.Size = New System.Drawing.Size(75, 17)
        Me.tssDbgVersion.Text = "tssDbgVersion"
        '
        'tssStatus
        '
        Me.tssStatus.Name = "tssStatus"
        Me.tssStatus.Size = New System.Drawing.Size(52, 17)
        Me.tssStatus.Text = "tssStatus"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnUpdate)
        Me.Panel1.Controls.Add(Me.ckCheckVersion)
        Me.Panel1.Controls.Add(Me.cboServer)
        Me.Panel1.Controls.Add(Me.lblServer)
        Me.Panel1.Location = New System.Drawing.Point(0, 300)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(497, 60)
        Me.Panel1.TabIndex = 6
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(360, 11)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(120, 34)
        Me.btnUpdate.TabIndex = 8
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'ckCheckVersion
        '
        Me.ckCheckVersion.AutoSize = True
        Me.ckCheckVersion.Checked = True
        Me.ckCheckVersion.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckCheckVersion.Location = New System.Drawing.Point(192, 26)
        Me.ckCheckVersion.Name = "ckCheckVersion"
        Me.ckCheckVersion.Size = New System.Drawing.Size(95, 17)
        Me.ckCheckVersion.TabIndex = 7
        Me.ckCheckVersion.Text = "Check Version"
        Me.ckCheckVersion.UseVisualStyleBackColor = True
        '
        'cboServer
        '
        Me.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServer.FormattingEnabled = True
        Me.cboServer.Location = New System.Drawing.Point(8, 24)
        Me.cboServer.Name = "cboServer"
        Me.cboServer.Size = New System.Drawing.Size(178, 21)
        Me.cboServer.TabIndex = 6
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(8, 8)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(38, 13)
        Me.lblServer.TabIndex = 5
        Me.lblServer.Text = "Server"
        '
        'frmServicePack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 382)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.picFace)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmServicePack"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lucla Mini-560 Update"
        CType(Me.picFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picFace As System.Windows.Forms.PictureBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents ckCheckVersion As System.Windows.Forms.CheckBox
    Friend WithEvents cboServer As System.Windows.Forms.ComboBox
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents tssPort As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssDbgVersion As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssStatus As System.Windows.Forms.ToolStripStatusLabel

End Class
