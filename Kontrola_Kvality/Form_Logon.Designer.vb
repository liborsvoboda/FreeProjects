<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form_Logon
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Logon))
        Me.cb_logon = New System.Windows.Forms.TextBox
        Me.btn_logon = New System.Windows.Forms.Button
        Me.lbl_person = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cb_logon
        '
        Me.cb_logon.Location = New System.Drawing.Point(1, 18)
        Me.cb_logon.Name = "cb_logon"
        Me.cb_logon.Size = New System.Drawing.Size(182, 23)
        Me.cb_logon.TabIndex = 25
        '
        'btn_logon
        '
        Me.btn_logon.Enabled = False
        Me.btn_logon.Location = New System.Drawing.Point(107, 47)
        Me.btn_logon.Name = "btn_logon"
        Me.btn_logon.Size = New System.Drawing.Size(76, 20)
        Me.btn_logon.TabIndex = 24
        Me.btn_logon.Text = "Pøihlásit"
        '
        'lbl_person
        '
        Me.lbl_person.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_person.Location = New System.Drawing.Point(0, 0)
        Me.lbl_person.Name = "lbl_person"
        Me.lbl_person.Size = New System.Drawing.Size(183, 20)
        Me.lbl_person.Text = "Zpracovatel"
        Me.lbl_person.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Form_Logon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(186, 74)
        Me.Controls.Add(Me.cb_logon)
        Me.Controls.Add(Me.btn_logon)
        Me.Controls.Add(Me.lbl_person)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form_Logon"
        Me.Text = "Pøihlášení"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cb_logon As System.Windows.Forms.TextBox
    Friend WithEvents btn_logon As System.Windows.Forms.Button
    Friend WithEvents lbl_person As System.Windows.Forms.Label
End Class
