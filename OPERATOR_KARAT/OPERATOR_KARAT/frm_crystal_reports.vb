Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports.Design
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms


Public Class frm_crystal_reports

    Protected temp_string() As String
    Protected separate_param() As String


    Private Sub frm_crystal_reports_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        temp_string = My.Forms.Main_Form.selected_report.Split(";")
        Me.Text = "Reporter: " + temp_string(0)

        custom_report.Load(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.report_Directory, temp_string(1)), OpenReportMethod.OpenReportByTempCopy)

        'GET PARAM NAME
        'MessageBox.Show(custom_report.ParameterFields.Item(0).Name)

        'SET DEFAULT VALUE FOR LOADING
        'custom_report.ParameterFields.Item(0).CurrentValues.AddValue(temp_string(3))


        For Each i As ParameterField In custom_report.ParameterFields
            For j = 0 To (temp_string.Count - 3)
                Try
                    If temp_string(j + 2).Trim.Length > 0 Then
                        separate_param = temp_string(j + 2).Split("/")
                        If i.Name = separate_param(0) Then
                            For k As Integer = 0 To (My.Forms.Main_Form.user_variables.GetLength(0) - 1) Step 1
                                If separate_param(1).Replace("*", "") = My.Forms.Main_Form.user_variables(k, 1) Then
                                    separate_param(1) = My.Forms.Main_Form.user_variables(k, 3).Replace("'", "")
                                End If
                            Next
                            i.CurrentValues.AddValue(separate_param(1))
                        End If
                    End If
                Catch
                End Try
            Next
        Next

        CR_Viewer.ReportSource = custom_report

    End Sub


    Private Sub frm_crystal_reports_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Forms.Main_Form.Enabled = True
    End Sub


End Class

