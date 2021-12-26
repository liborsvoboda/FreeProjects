<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_filter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_filter))
        Me.p_filter_list = New System.Windows.Forms.Panel()
        Me.btn_apply = New System.Windows.Forms.Button()
        Me.btn_cancel = New System.Windows.Forms.Button()
        Me.btn_apply_close = New System.Windows.Forms.Button()
        Me.ts_tools = New System.Windows.Forms.ToolStrip()
        Me.tsb_show_hide = New System.Windows.Forms.ToolStripButton()
        Me.tsb_sql_window = New System.Windows.Forms.ToolStripButton()
        Me.tsb_save_new = New System.Windows.Forms.ToolStripButton()
        Me.tsb_save_filter = New System.Windows.Forms.ToolStripButton()
        Me.ts_tools.SuspendLayout()
        Me.SuspendLayout()
        '
        'p_filter_list
        '
        Me.p_filter_list.AllowDrop = True
        Me.p_filter_list.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.p_filter_list.AutoScroll = True
        Me.p_filter_list.Location = New System.Drawing.Point(0, 32)
        Me.p_filter_list.Name = "p_filter_list"
        Me.p_filter_list.Size = New System.Drawing.Size(293, 451)
        Me.p_filter_list.TabIndex = 0
        '
        'btn_apply
        '
        Me.btn_apply.Enabled = False
        Me.btn_apply.Location = New System.Drawing.Point(0, 490)
        Me.btn_apply.Name = "btn_apply"
        Me.btn_apply.Size = New System.Drawing.Size(75, 23)
        Me.btn_apply.TabIndex = 1
        Me.btn_apply.Text = "apply"
        Me.btn_apply.UseVisualStyleBackColor = True
        '
        'btn_cancel
        '
        Me.btn_cancel.Location = New System.Drawing.Point(218, 490)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancel.TabIndex = 2
        Me.btn_cancel.Text = "cancel"
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btn_apply_close
        '
        Me.btn_apply_close.Enabled = False
        Me.btn_apply_close.Location = New System.Drawing.Point(81, 490)
        Me.btn_apply_close.Name = "btn_apply_close"
        Me.btn_apply_close.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btn_apply_close.Size = New System.Drawing.Size(121, 23)
        Me.btn_apply_close.TabIndex = 3
        Me.btn_apply_close.Text = "apply_close"
        Me.btn_apply_close.UseVisualStyleBackColor = True
        '
        'ts_tools
        '
        Me.ts_tools.AutoSize = False
        Me.ts_tools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_show_hide, Me.tsb_sql_window, Me.tsb_save_new, Me.tsb_save_filter})
        Me.ts_tools.Location = New System.Drawing.Point(0, 0)
        Me.ts_tools.Name = "ts_tools"
        Me.ts_tools.Size = New System.Drawing.Size(293, 29)
        Me.ts_tools.TabIndex = 4
        Me.ts_tools.Text = "ToolStrip1"
        '
        'tsb_show_hide
        '
        Me.tsb_show_hide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.tsb_show_hide.Image = Global.SWAPP_Builder.My.Resources.Resources.show
        Me.tsb_show_hide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_show_hide.Name = "tsb_show_hide"
        Me.tsb_show_hide.Size = New System.Drawing.Size(50, 26)
        Me.tsb_show_hide.Text = "hide"
        '
        'tsb_sql_window
        '
        Me.tsb_sql_window.AutoSize = False
        Me.tsb_sql_window.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsb_sql_window.Image = CType(resources.GetObject("tsb_sql_window.Image"), System.Drawing.Image)
        Me.tsb_sql_window.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_sql_window.Margin = New System.Windows.Forms.Padding(0)
        Me.tsb_sql_window.Name = "tsb_sql_window"
        Me.tsb_sql_window.Size = New System.Drawing.Size(42, 26)
        Me.tsb_sql_window.Text = "sql"
        '
        'tsb_save_new
        '
        Me.tsb_save_new.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsb_save_new.Image = Global.SWAPP_Builder.My.Resources.Resources.save
        Me.tsb_save_new.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_save_new.Name = "tsb_save_new"
        Me.tsb_save_new.Size = New System.Drawing.Size(66, 26)
        Me.tsb_save_new.Text = "save_as"
        '
        'tsb_save_filter
        '
        Me.tsb_save_filter.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsb_save_filter.Image = Global.SWAPP_Builder.My.Resources.Resources.save
        Me.tsb_save_filter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_save_filter.Name = "tsb_save_filter"
        Me.tsb_save_filter.Size = New System.Drawing.Size(50, 26)
        Me.tsb_save_filter.Text = "save"
        '
        'frm_filter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 517)
        Me.Controls.Add(Me.ts_tools)
        Me.Controls.Add(Me.btn_apply_close)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_apply)
        Me.Controls.Add(Me.p_filter_list)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_filter"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "set_filter"
        Me.TopMost = True
        Me.ts_tools.ResumeLayout(False)
        Me.ts_tools.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_apply As System.Windows.Forms.Button
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents btn_apply_close As System.Windows.Forms.Button
    Private WithEvents p_filter_list As System.Windows.Forms.Panel
    Friend WithEvents ts_tools As System.Windows.Forms.ToolStrip
    Friend WithEvents tsb_show_hide As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_sql_window As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_save_filter As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_save_new As System.Windows.Forms.ToolStripButton
End Class
