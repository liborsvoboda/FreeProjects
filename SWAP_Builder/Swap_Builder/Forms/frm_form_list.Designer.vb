<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_form_list
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_form_list))
        Me.tv_menu = New System.Windows.Forms.TreeView()
        Me.btn_search_menu = New System.Windows.Forms.Button()
        Me.btn_search_clear = New System.Windows.Forms.Button()
        Me.txt_menu_search = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'tv_menu
        '
        Me.tv_menu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tv_menu.FullRowSelect = True
        Me.tv_menu.HideSelection = False
        Me.tv_menu.HotTracking = True
        Me.tv_menu.LineColor = System.Drawing.Color.DarkGray
        Me.tv_menu.Location = New System.Drawing.Point(0, 30)
        Me.tv_menu.Name = "tv_menu"
        Me.tv_menu.Size = New System.Drawing.Size(257, 425)
        Me.tv_menu.TabIndex = 2
        '
        'btn_search_menu
        '
        Me.btn_search_menu.BackgroundImage = Global.SWAPP_Builder.My.Resources.Resources.search
        Me.btn_search_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_search_menu.ForeColor = System.Drawing.Color.Black
        Me.btn_search_menu.Location = New System.Drawing.Point(0, 2)
        Me.btn_search_menu.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_search_menu.Name = "btn_search_menu"
        Me.btn_search_menu.Size = New System.Drawing.Size(28, 27)
        Me.btn_search_menu.TabIndex = 20
        Me.btn_search_menu.Tag = ""
        Me.btn_search_menu.UseVisualStyleBackColor = True
        '
        'btn_search_clear
        '
        Me.btn_search_clear.BackgroundImage = Global.SWAPP_Builder.My.Resources.Resources.button_cancel
        Me.btn_search_clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_search_clear.ForeColor = System.Drawing.Color.Black
        Me.btn_search_clear.Location = New System.Drawing.Point(229, 2)
        Me.btn_search_clear.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_search_clear.Name = "btn_search_clear"
        Me.btn_search_clear.Size = New System.Drawing.Size(28, 27)
        Me.btn_search_clear.TabIndex = 19
        Me.btn_search_clear.Tag = ""
        Me.btn_search_clear.UseVisualStyleBackColor = True
        '
        'txt_menu_search
        '
        Me.txt_menu_search.AcceptsReturn = True
        Me.txt_menu_search.AcceptsTab = True
        Me.txt_menu_search.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_menu_search.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_menu_search.ForeColor = System.Drawing.Color.Gray
        Me.txt_menu_search.Location = New System.Drawing.Point(31, 4)
        Me.txt_menu_search.Name = "txt_menu_search"
        Me.txt_menu_search.Size = New System.Drawing.Size(195, 22)
        Me.txt_menu_search.TabIndex = 18
        Me.txt_menu_search.Text = "search"
        Me.txt_menu_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txt_menu_search.WordWrap = False
        '
        'frm_form_list
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(259, 456)
        Me.Controls.Add(Me.btn_search_menu)
        Me.Controls.Add(Me.btn_search_clear)
        Me.Controls.Add(Me.txt_menu_search)
        Me.Controls.Add(Me.tv_menu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_form_list"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "select_form"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tv_menu As System.Windows.Forms.TreeView
    Friend WithEvents btn_search_menu As System.Windows.Forms.Button
    Friend WithEvents btn_search_clear As System.Windows.Forms.Button
    Friend WithEvents txt_menu_search As System.Windows.Forms.TextBox
End Class
