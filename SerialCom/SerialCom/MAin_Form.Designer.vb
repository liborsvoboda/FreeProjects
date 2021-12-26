<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main_form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(main_form))
        Me.btn_write_to_com = New System.Windows.Forms.Button()
        Me.cb_com_ports = New System.Windows.Forms.ComboBox()
        Me.rtb_input = New System.Windows.Forms.RichTextBox()
        Me.rtb_output = New System.Windows.Forms.RichTextBox()
        Me.lbl_input = New System.Windows.Forms.Label()
        Me.lbl_output = New System.Windows.Forms.Label()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.cb_baud_rate = New System.Windows.Forms.ComboBox()
        Me.lbl_comport = New System.Windows.Forms.Label()
        Me.lbl_baudrate = New System.Windows.Forms.Label()
        Me.Tb_Control = New System.Windows.Forms.TabControl()
        Me.tp_debugger = New System.Windows.Forms.TabPage()
        Me.btn_input_clear = New System.Windows.Forms.Button()
        Me.btn_output_clear = New System.Windows.Forms.Button()
        Me.tp_Settings = New System.Windows.Forms.TabPage()
        Me.btn_uninit = New System.Windows.Forms.Button()
        Me.chb_init_after_start = New System.Windows.Forms.CheckBox()
        Me.btn_com_initialize = New System.Windows.Forms.Button()
        Me.btn_save_settings = New System.Windows.Forms.Button()
        Me.chb_autotab = New System.Windows.Forms.CheckBox()
        Me.chb_autoenter = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_keystring_length = New System.Windows.Forms.TextBox()
        Me.lbl_keystring_length = New System.Windows.Forms.Label()
        Me.ni_tray_menu = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Checker_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Tb_Control.SuspendLayout()
        Me.tp_debugger.SuspendLayout()
        Me.tp_Settings.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_write_to_com
        '
        Me.btn_write_to_com.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btn_write_to_com.Location = New System.Drawing.Point(242, 120)
        Me.btn_write_to_com.Name = "btn_write_to_com"
        Me.btn_write_to_com.Size = New System.Drawing.Size(75, 71)
        Me.btn_write_to_com.TabIndex = 50
        Me.btn_write_to_com.Text = "Write To COM"
        Me.btn_write_to_com.UseVisualStyleBackColor = True
        '
        'cb_com_ports
        '
        Me.cb_com_ports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_com_ports.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.cb_com_ports.FormattingEnabled = True
        Me.cb_com_ports.Location = New System.Drawing.Point(191, 11)
        Me.cb_com_ports.Name = "cb_com_ports"
        Me.cb_com_ports.Size = New System.Drawing.Size(121, 28)
        Me.cb_com_ports.TabIndex = 3
        '
        'rtb_input
        '
        Me.rtb_input.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.rtb_input.Location = New System.Drawing.Point(6, 25)
        Me.rtb_input.Name = "rtb_input"
        Me.rtb_input.Size = New System.Drawing.Size(228, 69)
        Me.rtb_input.TabIndex = 10
        Me.rtb_input.Text = ""
        '
        'rtb_output
        '
        Me.rtb_output.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.rtb_output.Location = New System.Drawing.Point(6, 120)
        Me.rtb_output.Name = "rtb_output"
        Me.rtb_output.Size = New System.Drawing.Size(228, 73)
        Me.rtb_output.TabIndex = 30
        Me.rtb_output.Text = ""
        '
        'lbl_input
        '
        Me.lbl_input.AutoSize = True
        Me.lbl_input.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lbl_input.Location = New System.Drawing.Point(2, 2)
        Me.lbl_input.Name = "lbl_input"
        Me.lbl_input.Size = New System.Drawing.Size(46, 20)
        Me.lbl_input.TabIndex = 6
        Me.lbl_input.Text = "Input"
        '
        'lbl_output
        '
        Me.lbl_output.AutoSize = True
        Me.lbl_output.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lbl_output.Location = New System.Drawing.Point(2, 97)
        Me.lbl_output.Name = "lbl_output"
        Me.lbl_output.Size = New System.Drawing.Size(58, 20)
        Me.lbl_output.TabIndex = 7
        Me.lbl_output.Text = "Output"
        '
        'SerialPort1
        '
        Me.SerialPort1.PortName = "COM6"
        '
        'cb_baud_rate
        '
        Me.cb_baud_rate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_baud_rate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.cb_baud_rate.FormattingEnabled = True
        Me.cb_baud_rate.Items.AddRange(New Object() {"9600", "38400", "57600", "115200"})
        Me.cb_baud_rate.Location = New System.Drawing.Point(191, 50)
        Me.cb_baud_rate.Name = "cb_baud_rate"
        Me.cb_baud_rate.Size = New System.Drawing.Size(121, 28)
        Me.cb_baud_rate.TabIndex = 9
        '
        'lbl_comport
        '
        Me.lbl_comport.AutoSize = True
        Me.lbl_comport.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_comport.Location = New System.Drawing.Point(7, 14)
        Me.lbl_comport.Name = "lbl_comport"
        Me.lbl_comport.Size = New System.Drawing.Size(100, 20)
        Me.lbl_comport.TabIndex = 11
        Me.lbl_comport.Text = "COM PORT"
        '
        'lbl_baudrate
        '
        Me.lbl_baudrate.AutoSize = True
        Me.lbl_baudrate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_baudrate.Location = New System.Drawing.Point(7, 53)
        Me.lbl_baudrate.Name = "lbl_baudrate"
        Me.lbl_baudrate.Size = New System.Drawing.Size(111, 20)
        Me.lbl_baudrate.TabIndex = 12
        Me.lbl_baudrate.Text = "BAUD RATE"
        '
        'Tb_Control
        '
        Me.Tb_Control.Controls.Add(Me.tp_debugger)
        Me.Tb_Control.Controls.Add(Me.tp_Settings)
        Me.Tb_Control.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tb_Control.Location = New System.Drawing.Point(0, 0)
        Me.Tb_Control.Name = "Tb_Control"
        Me.Tb_Control.SelectedIndex = 0
        Me.Tb_Control.Size = New System.Drawing.Size(328, 225)
        Me.Tb_Control.TabIndex = 5
        '
        'tp_debugger
        '
        Me.tp_debugger.Controls.Add(Me.btn_input_clear)
        Me.tp_debugger.Controls.Add(Me.btn_output_clear)
        Me.tp_debugger.Controls.Add(Me.rtb_output)
        Me.tp_debugger.Controls.Add(Me.rtb_input)
        Me.tp_debugger.Controls.Add(Me.btn_write_to_com)
        Me.tp_debugger.Controls.Add(Me.lbl_output)
        Me.tp_debugger.Controls.Add(Me.lbl_input)
        Me.tp_debugger.Location = New System.Drawing.Point(4, 22)
        Me.tp_debugger.Name = "tp_debugger"
        Me.tp_debugger.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_debugger.Size = New System.Drawing.Size(320, 199)
        Me.tp_debugger.TabIndex = 0
        Me.tp_debugger.Text = "Debugger"
        Me.tp_debugger.UseVisualStyleBackColor = True
        '
        'btn_input_clear
        '
        Me.btn_input_clear.Location = New System.Drawing.Point(210, 71)
        Me.btn_input_clear.Name = "btn_input_clear"
        Me.btn_input_clear.Size = New System.Drawing.Size(24, 23)
        Me.btn_input_clear.TabIndex = 20
        Me.btn_input_clear.Text = "X"
        Me.btn_input_clear.UseVisualStyleBackColor = True
        '
        'btn_output_clear
        '
        Me.btn_output_clear.Location = New System.Drawing.Point(210, 168)
        Me.btn_output_clear.Name = "btn_output_clear"
        Me.btn_output_clear.Size = New System.Drawing.Size(24, 23)
        Me.btn_output_clear.TabIndex = 40
        Me.btn_output_clear.Text = "X"
        Me.btn_output_clear.UseVisualStyleBackColor = True
        '
        'tp_Settings
        '
        Me.tp_Settings.Controls.Add(Me.btn_uninit)
        Me.tp_Settings.Controls.Add(Me.chb_init_after_start)
        Me.tp_Settings.Controls.Add(Me.btn_com_initialize)
        Me.tp_Settings.Controls.Add(Me.btn_save_settings)
        Me.tp_Settings.Controls.Add(Me.chb_autotab)
        Me.tp_Settings.Controls.Add(Me.chb_autoenter)
        Me.tp_Settings.Controls.Add(Me.Label3)
        Me.tp_Settings.Controls.Add(Me.txt_keystring_length)
        Me.tp_Settings.Controls.Add(Me.lbl_keystring_length)
        Me.tp_Settings.Controls.Add(Me.lbl_comport)
        Me.tp_Settings.Controls.Add(Me.cb_baud_rate)
        Me.tp_Settings.Controls.Add(Me.cb_com_ports)
        Me.tp_Settings.Controls.Add(Me.lbl_baudrate)
        Me.tp_Settings.Location = New System.Drawing.Point(4, 22)
        Me.tp_Settings.Name = "tp_Settings"
        Me.tp_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Settings.Size = New System.Drawing.Size(320, 199)
        Me.tp_Settings.TabIndex = 1
        Me.tp_Settings.Text = "Settings"
        Me.tp_Settings.UseVisualStyleBackColor = True
        '
        'btn_uninit
        '
        Me.btn_uninit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btn_uninit.Location = New System.Drawing.Point(88, 149)
        Me.btn_uninit.Name = "btn_uninit"
        Me.btn_uninit.Size = New System.Drawing.Size(82, 50)
        Me.btn_uninit.TabIndex = 60
        Me.btn_uninit.Text = "COM Uninit"
        Me.btn_uninit.UseVisualStyleBackColor = True
        '
        'chb_init_after_start
        '
        Me.chb_init_after_start.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_init_after_start.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_init_after_start.Location = New System.Drawing.Point(6, 121)
        Me.chb_init_after_start.Name = "chb_init_after_start"
        Me.chb_init_after_start.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_init_after_start.Size = New System.Drawing.Size(152, 24)
        Me.chb_init_after_start.TabIndex = 40
        Me.chb_init_after_start.Text = "Init Aftr.START"
        Me.chb_init_after_start.UseVisualStyleBackColor = True
        '
        'btn_com_initialize
        '
        Me.btn_com_initialize.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btn_com_initialize.Location = New System.Drawing.Point(0, 149)
        Me.btn_com_initialize.Name = "btn_com_initialize"
        Me.btn_com_initialize.Size = New System.Drawing.Size(82, 50)
        Me.btn_com_initialize.TabIndex = 50
        Me.btn_com_initialize.Text = "COM Init"
        Me.btn_com_initialize.UseVisualStyleBackColor = True
        '
        'btn_save_settings
        '
        Me.btn_save_settings.Location = New System.Drawing.Point(202, 173)
        Me.btn_save_settings.Name = "btn_save_settings"
        Me.btn_save_settings.Size = New System.Drawing.Size(110, 23)
        Me.btn_save_settings.TabIndex = 70
        Me.btn_save_settings.Text = "SAVE SETTING"
        Me.btn_save_settings.UseVisualStyleBackColor = True
        '
        'chb_autotab
        '
        Me.chb_autotab.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_autotab.Location = New System.Drawing.Point(182, 111)
        Me.chb_autotab.Name = "chb_autotab"
        Me.chb_autotab.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_autotab.Size = New System.Drawing.Size(130, 24)
        Me.chb_autotab.TabIndex = 30
        Me.chb_autotab.Text = "AutoTAB"
        Me.chb_autotab.UseVisualStyleBackColor = True
        '
        'chb_autoenter
        '
        Me.chb_autoenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_autoenter.Location = New System.Drawing.Point(182, 130)
        Me.chb_autoenter.Name = "chb_autoenter"
        Me.chb_autoenter.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_autoenter.Size = New System.Drawing.Size(130, 24)
        Me.chb_autoenter.TabIndex = 35
        Me.chb_autoenter.Text = "AutoEnter"
        Me.chb_autoenter.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(51, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "(Full / 1 -XX)"
        '
        'txt_keystring_length
        '
        Me.txt_keystring_length.Location = New System.Drawing.Point(191, 85)
        Me.txt_keystring_length.Name = "txt_keystring_length"
        Me.txt_keystring_length.Size = New System.Drawing.Size(121, 20)
        Me.txt_keystring_length.TabIndex = 14
        Me.txt_keystring_length.Text = "Full"
        '
        'lbl_keystring_length
        '
        Me.lbl_keystring_length.AutoSize = True
        Me.lbl_keystring_length.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_keystring_length.Location = New System.Drawing.Point(7, 89)
        Me.lbl_keystring_length.Name = "lbl_keystring_length"
        Me.lbl_keystring_length.Size = New System.Drawing.Size(169, 16)
        Me.lbl_keystring_length.TabIndex = 13
        Me.lbl_keystring_length.Text = "Keyboard String Length"
        '
        'ni_tray_menu
        '
        Me.ni_tray_menu.Icon = CType(resources.GetObject("ni_tray_menu.Icon"), System.Drawing.Icon)
        Me.ni_tray_menu.Text = "RS232 Keyboard Receiver"
        Me.ni_tray_menu.Visible = True
        '
        'Checker_Timer
        '
        Me.Checker_Timer.Enabled = True
        Me.Checker_Timer.Interval = 10000
        '
        'main_form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(328, 225)
        Me.Controls.Add(Me.Tb_Control)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "main_form"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RS232 Keyboard Receiver V1.0"
        Me.TopMost = True
        Me.Tb_Control.ResumeLayout(False)
        Me.tp_debugger.ResumeLayout(False)
        Me.tp_debugger.PerformLayout()
        Me.tp_Settings.ResumeLayout(False)
        Me.tp_Settings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_write_to_com As System.Windows.Forms.Button
    Friend WithEvents cb_com_ports As System.Windows.Forms.ComboBox
    Friend WithEvents rtb_input As System.Windows.Forms.RichTextBox
    Friend WithEvents rtb_output As System.Windows.Forms.RichTextBox
    Friend WithEvents lbl_input As System.Windows.Forms.Label
    Friend WithEvents lbl_output As System.Windows.Forms.Label
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents cb_baud_rate As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_comport As System.Windows.Forms.Label
    Friend WithEvents lbl_baudrate As System.Windows.Forms.Label
    Friend WithEvents Tb_Control As System.Windows.Forms.TabControl
    Friend WithEvents tp_debugger As System.Windows.Forms.TabPage
    Friend WithEvents tp_Settings As System.Windows.Forms.TabPage
    Friend WithEvents lbl_keystring_length As System.Windows.Forms.Label
    Friend WithEvents btn_save_settings As System.Windows.Forms.Button
    Friend WithEvents chb_autotab As System.Windows.Forms.CheckBox
    Friend WithEvents chb_autoenter As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_keystring_length As System.Windows.Forms.TextBox
    Friend WithEvents btn_com_initialize As System.Windows.Forms.Button
    Friend WithEvents chb_init_after_start As System.Windows.Forms.CheckBox
    Friend WithEvents ni_tray_menu As System.Windows.Forms.NotifyIcon
    Friend WithEvents btn_input_clear As System.Windows.Forms.Button
    Friend WithEvents btn_output_clear As System.Windows.Forms.Button
    Friend WithEvents Checker_Timer As System.Windows.Forms.Timer
    Friend WithEvents btn_uninit As System.Windows.Forms.Button

End Class
