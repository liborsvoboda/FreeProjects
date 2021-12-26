<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_crystal_reports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_crystal_reports))
        Me.custom_report = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        Me.CR_Viewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CR_Viewer
        '
        Me.CR_Viewer.ActiveViewIndex = -1
        Me.CR_Viewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CR_Viewer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CR_Viewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CR_Viewer.EnableToolTips = False
        Me.CR_Viewer.Location = New System.Drawing.Point(0, 0)
        Me.CR_Viewer.Name = "CR_Viewer"
        Me.CR_Viewer.ShowCloseButton = False
        'Me.CR_Viewer.ShowLogo = False
        Me.CR_Viewer.Size = New System.Drawing.Size(679, 483)
        Me.CR_Viewer.TabIndex = 0
        Me.CR_Viewer.DisplayToolbar = True
        Me.CR_Viewer.DisplayStatusBar = True
        'Me.CR_Viewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'frm_crystal_reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 483)
        Me.Controls.Add(Me.CR_Viewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_crystal_reports"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reporter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents custom_report As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Friend WithEvents CR_Viewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
