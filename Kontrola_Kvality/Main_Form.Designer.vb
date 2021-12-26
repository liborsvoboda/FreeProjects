<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Main_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Form))
        Me.btn_status = New System.Windows.Forms.Button
        Me.sb_application = New System.Windows.Forms.StatusBar
        Me.tb_control = New System.Windows.Forms.TabControl
        Me.tp_quality_control = New System.Windows.Forms.TabPage
        Me.txt_planvyroba = New System.Windows.Forms.TextBox
        Me.btn_save = New System.Windows.Forms.Button
        Me.txt_controled_pcs = New System.Windows.Forms.TextBox
        Me.lbl_controled_pcs = New System.Windows.Forms.Label
        Me.txt_operation = New System.Windows.Forms.TextBox
        Me.lbl_operation = New System.Windows.Forms.Label
        Me.txt_job_no = New System.Windows.Forms.TextBox
        Me.lb_job_no = New System.Windows.Forms.Label
        Me.lbl_request_id = New System.Windows.Forms.Label
        Me.txt_request_id = New System.Windows.Forms.TextBox
        Me.lb_control_type = New System.Windows.Forms.ListBox
        Me.lbl_control_type = New System.Windows.Forms.Label
        Me.tp_infolist = New System.Windows.Forms.TabPage
        Me.chb_sql_debug = New System.Windows.Forms.CheckBox
        Me.btn_export = New System.Windows.Forms.Button
        Me.btn_import = New System.Windows.Forms.Button
        Me.pb_data_status = New System.Windows.Forms.ProgressBar
        Me.lbl_logged_time_info = New System.Windows.Forms.Label
        Me.lbl_logged_time = New System.Windows.Forms.Label
        Me.lbl_logged_info = New System.Windows.Forms.Label
        Me.lbl_logged = New System.Windows.Forms.Label
        Me.lbl_load_percent = New System.Windows.Forms.Label
        Me.tb_control.SuspendLayout()
        Me.tp_quality_control.SuspendLayout()
        Me.tp_infolist.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_status
        '
        Me.btn_status.ForeColor = System.Drawing.Color.Red
        Me.btn_status.Location = New System.Drawing.Point(238, 247)
        Me.btn_status.Name = "btn_status"
        Me.btn_status.Size = New System.Drawing.Size(79, 21)
        Me.btn_status.TabIndex = 2
        Me.btn_status.Text = "OFFLINE"
        '
        'sb_application
        '
        Me.sb_application.Location = New System.Drawing.Point(0, 245)
        Me.sb_application.Name = "sb_application"
        Me.sb_application.Size = New System.Drawing.Size(318, 24)
        Me.sb_application.Text = "Posl.Aktualizace: "
        '
        'tb_control
        '
        Me.tb_control.Controls.Add(Me.tp_quality_control)
        Me.tb_control.Controls.Add(Me.tp_infolist)
        Me.tb_control.Location = New System.Drawing.Point(0, 0)
        Me.tb_control.Name = "tb_control"
        Me.tb_control.SelectedIndex = 0
        Me.tb_control.Size = New System.Drawing.Size(318, 246)
        Me.tb_control.TabIndex = 5
        '
        'tp_quality_control
        '
        Me.tp_quality_control.Controls.Add(Me.txt_planvyroba)
        Me.tp_quality_control.Controls.Add(Me.btn_save)
        Me.tp_quality_control.Controls.Add(Me.txt_controled_pcs)
        Me.tp_quality_control.Controls.Add(Me.lbl_controled_pcs)
        Me.tp_quality_control.Controls.Add(Me.txt_operation)
        Me.tp_quality_control.Controls.Add(Me.lbl_operation)
        Me.tp_quality_control.Controls.Add(Me.txt_job_no)
        Me.tp_quality_control.Controls.Add(Me.lb_job_no)
        Me.tp_quality_control.Controls.Add(Me.lbl_request_id)
        Me.tp_quality_control.Controls.Add(Me.txt_request_id)
        Me.tp_quality_control.Controls.Add(Me.lb_control_type)
        Me.tp_quality_control.Controls.Add(Me.lbl_control_type)
        Me.tp_quality_control.Location = New System.Drawing.Point(4, 25)
        Me.tp_quality_control.Name = "tp_quality_control"
        Me.tp_quality_control.Size = New System.Drawing.Size(310, 217)
        Me.tp_quality_control.Text = "QualityControl"
        '
        'txt_planvyroba
        '
        Me.txt_planvyroba.Enabled = False
        Me.txt_planvyroba.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txt_planvyroba.Location = New System.Drawing.Point(3, 160)
        Me.txt_planvyroba.Name = "txt_planvyroba"
        Me.txt_planvyroba.Size = New System.Drawing.Size(10, 19)
        Me.txt_planvyroba.TabIndex = 1507
        Me.txt_planvyroba.Visible = False
        '
        'btn_save
        '
        Me.btn_save.Enabled = False
        Me.btn_save.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.btn_save.Location = New System.Drawing.Point(226, 154)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(81, 25)
        Me.btn_save.TabIndex = 40
        Me.btn_save.Text = "Uložit"
        '
        'txt_controled_pcs
        '
        Me.txt_controled_pcs.AcceptsReturn = True
        Me.txt_controled_pcs.AcceptsTab = True
        Me.txt_controled_pcs.BackColor = System.Drawing.Color.NavajoWhite
        Me.txt_controled_pcs.Enabled = False
        Me.txt_controled_pcs.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.txt_controled_pcs.Location = New System.Drawing.Point(28, 123)
        Me.txt_controled_pcs.Name = "txt_controled_pcs"
        Me.txt_controled_pcs.Size = New System.Drawing.Size(75, 26)
        Me.txt_controled_pcs.TabIndex = 30
        Me.txt_controled_pcs.WordWrap = False
        '
        'lbl_controled_pcs
        '
        Me.lbl_controled_pcs.Location = New System.Drawing.Point(3, 100)
        Me.lbl_controled_pcs.Name = "lbl_controled_pcs"
        Me.lbl_controled_pcs.Size = New System.Drawing.Size(124, 20)
        Me.lbl_controled_pcs.Text = "Kontrolovaných Ks"
        Me.lbl_controled_pcs.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txt_operation
        '
        Me.txt_operation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txt_operation.Enabled = False
        Me.txt_operation.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txt_operation.Location = New System.Drawing.Point(154, 196)
        Me.txt_operation.Name = "txt_operation"
        Me.txt_operation.ReadOnly = True
        Me.txt_operation.Size = New System.Drawing.Size(153, 19)
        Me.txt_operation.TabIndex = 1502
        Me.txt_operation.TabStop = False
        Me.txt_operation.WordWrap = False
        '
        'lbl_operation
        '
        Me.lbl_operation.Location = New System.Drawing.Point(154, 182)
        Me.lbl_operation.Name = "lbl_operation"
        Me.lbl_operation.Size = New System.Drawing.Size(153, 15)
        Me.lbl_operation.Text = "Operace"
        Me.lbl_operation.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txt_job_no
        '
        Me.txt_job_no.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txt_job_no.Enabled = False
        Me.txt_job_no.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txt_job_no.Location = New System.Drawing.Point(3, 196)
        Me.txt_job_no.Name = "txt_job_no"
        Me.txt_job_no.ReadOnly = True
        Me.txt_job_no.Size = New System.Drawing.Size(145, 19)
        Me.txt_job_no.TabIndex = 1500
        Me.txt_job_no.TabStop = False
        Me.txt_job_no.WordWrap = False
        '
        'lb_job_no
        '
        Me.lb_job_no.Location = New System.Drawing.Point(3, 182)
        Me.lb_job_no.Name = "lb_job_no"
        Me.lb_job_no.Size = New System.Drawing.Size(145, 15)
        Me.lb_job_no.Text = "Zakázka"
        Me.lb_job_no.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbl_request_id
        '
        Me.lbl_request_id.Location = New System.Drawing.Point(3, 7)
        Me.lbl_request_id.Name = "lbl_request_id"
        Me.lbl_request_id.Size = New System.Drawing.Size(100, 20)
        Me.lbl_request_id.Text = "ID Požadavku:"
        '
        'txt_request_id
        '
        Me.txt_request_id.AcceptsReturn = True
        Me.txt_request_id.AcceptsTab = True
        Me.txt_request_id.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.txt_request_id.Location = New System.Drawing.Point(109, 3)
        Me.txt_request_id.Name = "txt_request_id"
        Me.txt_request_id.Size = New System.Drawing.Size(198, 26)
        Me.txt_request_id.TabIndex = 10
        '
        'lb_control_type
        '
        Me.lb_control_type.Enabled = False
        Me.lb_control_type.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lb_control_type.Location = New System.Drawing.Point(130, 33)
        Me.lb_control_type.Name = "lb_control_type"
        Me.lb_control_type.Size = New System.Drawing.Size(177, 116)
        Me.lb_control_type.TabIndex = 20
        '
        'lbl_control_type
        '
        Me.lbl_control_type.Location = New System.Drawing.Point(38, 33)
        Me.lbl_control_type.Name = "lbl_control_type"
        Me.lbl_control_type.Size = New System.Drawing.Size(100, 20)
        Me.lbl_control_type.Text = "Typ Kontroly:"
        '
        'tp_infolist
        '
        Me.tp_infolist.Controls.Add(Me.chb_sql_debug)
        Me.tp_infolist.Controls.Add(Me.btn_export)
        Me.tp_infolist.Controls.Add(Me.btn_import)
        Me.tp_infolist.Controls.Add(Me.pb_data_status)
        Me.tp_infolist.Controls.Add(Me.lbl_logged_time_info)
        Me.tp_infolist.Controls.Add(Me.lbl_logged_time)
        Me.tp_infolist.Controls.Add(Me.lbl_logged_info)
        Me.tp_infolist.Controls.Add(Me.lbl_logged)
        Me.tp_infolist.Controls.Add(Me.lbl_load_percent)
        Me.tp_infolist.Location = New System.Drawing.Point(4, 25)
        Me.tp_infolist.Name = "tp_infolist"
        Me.tp_infolist.Size = New System.Drawing.Size(310, 217)
        Me.tp_infolist.Text = "Pøenos Dat"
        '
        'chb_sql_debug
        '
        Me.chb_sql_debug.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.chb_sql_debug.Location = New System.Drawing.Point(7, 50)
        Me.chb_sql_debug.Name = "chb_sql_debug"
        Me.chb_sql_debug.Size = New System.Drawing.Size(108, 20)
        Me.chb_sql_debug.TabIndex = 53
        Me.chb_sql_debug.Text = "SQLDEBUGER"
        '
        'btn_export
        '
        Me.btn_export.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btn_export.Location = New System.Drawing.Point(214, 168)
        Me.btn_export.Name = "btn_export"
        Me.btn_export.Size = New System.Drawing.Size(91, 31)
        Me.btn_export.TabIndex = 22
        Me.btn_export.Text = "Export"
        '
        'btn_import
        '
        Me.btn_import.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btn_import.Location = New System.Drawing.Point(7, 168)
        Me.btn_import.Name = "btn_import"
        Me.btn_import.Size = New System.Drawing.Size(91, 31)
        Me.btn_import.TabIndex = 21
        Me.btn_import.Text = "Import"
        '
        'pb_data_status
        '
        Me.pb_data_status.Location = New System.Drawing.Point(7, 131)
        Me.pb_data_status.Name = "pb_data_status"
        Me.pb_data_status.Size = New System.Drawing.Size(298, 31)
        '
        'lbl_logged_time_info
        '
        Me.lbl_logged_time_info.Location = New System.Drawing.Point(97, 27)
        Me.lbl_logged_time_info.Name = "lbl_logged_time_info"
        Me.lbl_logged_time_info.Size = New System.Drawing.Size(210, 20)
        Me.lbl_logged_time_info.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbl_logged_time
        '
        Me.lbl_logged_time.Location = New System.Drawing.Point(3, 27)
        Me.lbl_logged_time.Name = "lbl_logged_time"
        Me.lbl_logged_time.Size = New System.Drawing.Size(95, 20)
        Me.lbl_logged_time.Text = "Èas Pøíhlášení:"
        '
        'lbl_logged_info
        '
        Me.lbl_logged_info.Location = New System.Drawing.Point(77, 7)
        Me.lbl_logged_info.Name = "lbl_logged_info"
        Me.lbl_logged_info.Size = New System.Drawing.Size(230, 20)
        Me.lbl_logged_info.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbl_logged
        '
        Me.lbl_logged.Location = New System.Drawing.Point(3, 7)
        Me.lbl_logged.Name = "lbl_logged"
        Me.lbl_logged.Size = New System.Drawing.Size(88, 20)
        Me.lbl_logged.Text = "Pøihlášen:"
        '
        'lbl_load_percent
        '
        Me.lbl_load_percent.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_load_percent.Location = New System.Drawing.Point(123, 165)
        Me.lbl_load_percent.Name = "lbl_load_percent"
        Me.lbl_load_percent.Size = New System.Drawing.Size(68, 22)
        Me.lbl_load_percent.Text = "0%"
        Me.lbl_load_percent.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(318, 269)
        Me.Controls.Add(Me.tb_control)
        Me.Controls.Add(Me.btn_status)
        Me.Controls.Add(Me.sb_application)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main_Form"
        Me.Text = "Kontrola Kvality"
        Me.tb_control.ResumeLayout(False)
        Me.tp_quality_control.ResumeLayout(False)
        Me.tp_infolist.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sb_application As System.Windows.Forms.StatusBar
    Friend WithEvents tb_control As System.Windows.Forms.TabControl
    Friend WithEvents tp_quality_control As System.Windows.Forms.TabPage
    Friend WithEvents lb_control_type As System.Windows.Forms.ListBox
    Friend WithEvents txt_job_no As System.Windows.Forms.TextBox
    Friend WithEvents lbl_request_id As System.Windows.Forms.Label
    Friend WithEvents txt_request_id As System.Windows.Forms.TextBox
    Friend WithEvents lb_job_no As System.Windows.Forms.Label
    Friend WithEvents txt_operation As System.Windows.Forms.TextBox
    Friend WithEvents lbl_operation As System.Windows.Forms.Label
    Friend WithEvents tp_infolist As System.Windows.Forms.TabPage
    Friend WithEvents txt_controled_pcs As System.Windows.Forms.TextBox
    Friend WithEvents lbl_controled_pcs As System.Windows.Forms.Label
    Friend WithEvents lbl_control_type As System.Windows.Forms.Label
    Friend WithEvents btn_save As System.Windows.Forms.Button
    Friend WithEvents lbl_logged_time_info As System.Windows.Forms.Label
    Friend WithEvents lbl_logged_time As System.Windows.Forms.Label
    Friend WithEvents lbl_logged_info As System.Windows.Forms.Label
    Friend WithEvents lbl_logged As System.Windows.Forms.Label
    Friend WithEvents btn_export As System.Windows.Forms.Button
    Friend WithEvents btn_import As System.Windows.Forms.Button
    Friend WithEvents pb_data_status As System.Windows.Forms.ProgressBar
    Friend WithEvents chb_sql_debug As System.Windows.Forms.CheckBox
    Friend WithEvents btn_status As System.Windows.Forms.Button
    Friend WithEvents lbl_load_percent As System.Windows.Forms.Label
    Friend WithEvents txt_planvyroba As System.Windows.Forms.TextBox

End Class
