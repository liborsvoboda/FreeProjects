Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Stimulsoft.Base
Imports Stimulsoft.Report

Public Class Main_Form

    Public sql_array_count As Integer = 0
    Public sql_array(0, 0) As String
    Public available_reports(3, 0) As String  'name/file/type CR ST/params
    Public pdf_files(1, 0) As String  'button/path with standard variables

    Public selected_report As String = ""

    Public root_directory As String = Application.StartupPath
    Public report_Directory As String = "REPORTS"
    Public dir_sql_Directory As String = "SQL_COMMANDS"
    Public dir_sql_SubDirectory As String = "SUB_SQL_COMMANDS"
    Public setting_file = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Application.ProductName)
    Public configuration_file As String = "settings.ini"
    Public available_languages As String
    Public btn_command As String = ""
    Public sub_where_command As String = ""
    Public user_variables(0, 3) As String 'field type/name/value/field no from datagrid
    Public temp_variables(0, 3) As String

    Public last As String

    Private Declare Function ActivateKeyboardLayout Lib "user32.dll" (ByVal myLanguage As Long, Flag As Boolean) As Long
    Const LANG_CZECH = 1029
    Const LANG_ENGLISH = 1033
    Const LANG_FRENCH = 1036
    Const LANG_GERMAN = 1031
    Const LANG_ITALIAN = 1040
    Const LANG_NORWEGIAN = 1043
    Const LANG_PORTUGUESE = 1046
    Const LANG_RUSSIAN = 1049
    Const LANG_SPANISH = 1034
    Const LANG_UKRAINE = 1058

    Private st_report = New Stimulsoft.Report.StiReport()




    Public Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If fn_check_directory(setting_file) = False Then fn_create_directory(setting_file)
        setting_file = System.IO.Path.Combine(setting_file, configuration_file)
        If fn_check_file(setting_file) = False Then
            fn_copy_file(System.IO.Path.Combine(Application.StartupPath, configuration_file), setting_file)
        End If

        ReDim user_variables(0, 3)
        ReDim temp_variables(0, 3)


        If fn_load_setting_file() = False Then
            Me.tb_control.SelectedIndex = 1
        Else
            Me.tb_control.SelectedIndex = 0
            fn_load_sql_files()
            If Me.cb_default_keyboard.SelectedItem.length > 0 Then fn_set_keyboard(Me.cb_default_keyboard.SelectedItem)
            fn_set_app_size()
            Me.lb_available_reports.SelectedIndex = 0
            fn_load_selected_dataview(Me.lb_available_reports.SelectedItem)
        End If
        lb_available_reports_Click(sender, e)
        'MessageBox.Show(IO.Path.Combine(root_directory, Me.report_Directory))
    End Sub



    Private Sub btn_save_setting_Click(sender As Object, e As EventArgs) Handles btn_save_setting.Click
        fn_save_file_setting()
    End Sub



    Private Sub lb_available_reports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_available_reports.SelectedValueChanged
        Me.p_user_inputs.Controls.Clear()
        Me.p_sub_buttons.Controls.Clear()
        Me.dgw_query_view.DataSource = ""
        Me.dgw_query_view.Refresh()
        Me.dgw_summary_view.DataSource = ""
        Me.dgw_summary_view.Refresh()
        Me.btn_command = ""

        ReDim user_variables(0, 3)
        ReDim temp_variables(0, 3)
        fn_load_selected_dataview(Me.lb_available_reports.SelectedItem)

        Me.tb_control.SelectedIndex = 0
        Try

            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of TextBox)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of CheckBox)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of ComboBox)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of DateTimePicker)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of Button)()
                ctrl_object.Focus()
                Exit Sub
            Next

        Catch ex As Exception

        End Try

    End Sub



    Private Sub lb_available_reports_EnterClick(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lb_available_reports.KeyPress
        If AscW(e.KeyChar) = 13 Then
            Me.p_user_inputs.Controls.Clear()
            Me.p_sub_buttons.Controls.Clear()
            Me.dgw_query_view.DataSource = ""
            Me.dgw_query_view.Refresh()
            Me.dgw_summary_view.DataSource = ""
            Me.dgw_summary_view.Refresh()

            Me.btn_command = ""

            ReDim user_variables(0, 3)
            ReDim temp_variables(0, 3)
            fn_load_selected_dataview(Me.lb_available_reports.SelectedItem)

            Me.tb_control.SelectedIndex = 0
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of TextBox)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of CheckBox)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of ComboBox)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of DateTimePicker)()
                ctrl_object.Focus()
                Exit Sub
            Next
            For Each ctrl_object In Me.p_user_inputs.Controls.OfType(Of Button)()
                ctrl_object.Focus()
                Exit Sub
            Next
        End If
    End Sub




    Private Sub p_user_inputs_enterkey(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If AscW(e.KeyChar) = 13 Then
            Dim ctl As Control
            ctl = CType(sender, Control)
            ctl.SelectNextControl(ActiveControl, True, True, True, True)
        End If
    End Sub


    Private Sub app_inputs_esc(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If AscW(e.KeyChar) = 27 Then
            btn_command = ""
            Me.p_user_inputs.Controls.Clear()
            Me.p_sub_buttons.Controls.Clear()
            Me.dgw_query_view.DataSource = ""
            Me.dgw_query_view.Refresh()
            Me.dgw_summary_view.DataSource = ""
            Me.dgw_summary_view.Refresh()

            Me.btn_back.Enabled = False
            ReDim user_variables(0, 3)
            ReDim temp_variables(0, 3)
            fn_load_selected_dataview(Me.lb_available_reports.SelectedItem.ToString)
        End If
    End Sub



    Public Sub txt_field_LostFocus_for_keymap(ByVal sender As System.Object, ByVal e As System.EventArgs)
        last = DirectCast(sender, Control).Name
        MessageBox.Show(last)
        fn_set_keyboard(Me.cb_default_keyboard.SelectedItem)
    End Sub



    Public Sub txt_field_Focus_for_keymap(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To (user_field_count) Step 1
            For Each textinput In Me.p_user_inputs.Controls.OfType(Of TextBox)()
                If textinput.Name = user_field_list(i, 1) And textinput.Focused = True Then
                    fn_set_keyboard(textinput.AccessibleDescription)
                End If
            Next
        Next
    End Sub


    Public Sub Open_SubForm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim filename_part
        For i As Integer = 0 To (user_field_count) Step 1
            If user_field_list(i, 0) = sender.name Then

                'FILL DATA FROM QUERY TO VARIABLE ARRAY
                For j As Integer = 0 To (user_variables.GetLength(0) - 1) Step 1
                    'If user_variables(j, 0) = "SUBVIEW" Then user_variables(j, 3) = Me.dgw_query_view.SelectedRows.Item(Me.dgw_query_view.CurrentRow.Index).Cells(CInt(user_variables(j, 2))).Value.ToString
                    If user_variables(j, 0) = "SUBVIEW" Then user_variables(j, 3) = Me.dgw_query_view.Item(CInt(user_variables(j, 2)), Me.dgw_query_view.CurrentRow.Index).Value.ToString()
                Next
                '

                filename_part = Split(user_field_list(i, 1), ".")
                Me.p_user_inputs.Controls.Clear()
                Me.p_sub_buttons.Controls.Clear()
                Me.dgw_query_view.DataSource = ""
                Me.dgw_query_view.Refresh()
                Me.dgw_summary_view.DataSource = ""
                Me.dgw_summary_view.Refresh()

                Me.btn_back.Enabled = True
                btn_command = dir_sql_SubDirectory + "\" + filename_part(0)

                fn_load_selected_dataview(dir_sql_SubDirectory + "\" + filename_part(0))
                Exit Sub
            End If
        Next
    End Sub


    Public Sub Xls_Export(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fn_export_to_excel()
    End Sub

    Public Sub Submit_Form(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If autosubmit_interval > 0 Then
            last_autosubmit = Date.Now.AddSeconds(autosubmit_interval)
        End If
        If autoroot_interval > 0 Then
            last_autoroot = Date.Now.AddSeconds(autoroot_interval)
        End If

        Dim bool_result As Byte
        Dim date_result As Date
        Dim datetime_result As DateTime
        Dim combo_result As String
        Me.dgw_query_view.DataSource = ""
        Me.dgw_query_view.Refresh()
        Me.dgw_summary_view.DataSource = ""
        Me.dgw_summary_view.Refresh()

        For Each subbutton In Me.p_sub_buttons.Controls.OfType(Of Button)()
            subbutton.Enabled = False
        Next

        If fn_txt_must_check() = False Then Exit Sub

        'START of preparing query
        Dim slq_query_command_final_command As String = slq_query_command
        For i As Integer = 0 To (user_field_count) Step 1
            Select Case user_field_list(i, 0)
                Case "TEXTBOX"
                    slq_query_command_final_command = slq_query_command_final_command.Replace("*" + user_field_list(i, 1) + "*", "'" + Me.p_user_inputs.Controls(user_field_list(i, 1)).Text.ToString + "'")
                    For j As Integer = 0 To user_variables.GetLength(0) - 1 Step 1
                        If user_variables(j, 1) = user_field_list(i, 1) Then user_variables(j, 3) = "'" + Me.p_user_inputs.Controls(user_field_list(i, 1)).Text.ToString + "'"
                    Next

                Case "CHECKBOX"
                    For Each chkBox In Me.p_user_inputs.Controls.OfType(Of CheckBox)()
                        If chkBox.Name = user_field_list(i, 1) Then
                            If CType(Me.p_user_inputs.Controls(user_field_list(i, 1)), CheckBox).Checked = True Then
                                bool_result = 1
                            Else
                                bool_result = 0
                            End If
                            slq_query_command_final_command = slq_query_command_final_command.Replace("*" + user_field_list(i, 1) + "*", bool_result.ToString)

                            For j As Integer = 0 To user_variables.GetLength(0) - 1 Step 1
                                If user_variables(j, 1) = user_field_list(i, 1) Then user_variables(j, 3) = bool_result.ToString
                            Next

                        End If
                    Next
                Case "BUTTON"

                Case "DATE"
                    For Each dateinput In Me.p_user_inputs.Controls.OfType(Of DateTimePicker)()
                        If dateinput.Name = user_field_list(i, 1) Then
                            date_result = Mid(Replace(dateinput.Value.ToShortDateString, " ", ""), 1, 10)
                        End If
                    Next
                    slq_query_command_final_command = slq_query_command_final_command.Replace("*" + user_field_list(i, 1) + "*", "'" + date_result + "'")

                    For j As Integer = 0 To user_variables.GetLength(0) - 1 Step 1
                        If user_variables(j, 1) = user_field_list(i, 1) Then user_variables(j, 3) = "'" + date_result + "'"
                    Next


                Case "DATETIME"
                    For Each dateinput In Me.p_user_inputs.Controls.OfType(Of DateTimePicker)()
                        If dateinput.Name = user_field_list(i, 1) Then
                            datetime_result = Replace(dateinput.Value.ToString, " ", "")
                        End If
                    Next
                    slq_query_command_final_command = slq_query_command_final_command.Replace("*" + user_field_list(i, 1) + "*", "'" + datetime_result.ToString + "'")

                    For j As Integer = 0 To user_variables.GetLength(0) - 1 Step 1
                        If user_variables(j, 1) = user_field_list(i, 1) Then user_variables(j, 3) = "'" + datetime_result.ToString + "'"
                    Next

                Case "COMBOBOX"
                    For Each comboinput In Me.p_user_inputs.Controls.OfType(Of ComboBox)()
                        If comboinput.Name = user_field_list(i, 1) Then
                            combo_result = comboinput.SelectedItem.ToString
                        End If
                    Next
                    slq_query_command_final_command = slq_query_command_final_command.Replace("*" + user_field_list(i, 1) + "*", "'" + combo_result + "'")

                    For j As Integer = 0 To user_variables.GetLength(0) - 1 Step 1
                        If user_variables(j, 1) = user_field_list(i, 1) Then user_variables(j, 3) = "'" + combo_result + "'"
                    Next
                Case Else
            End Select
        Next
        'END of preparing query


        If fn_sql_request(slq_query_command_final_command, "SELECT") = True Then
            If fn_dgw_tablebuild() = True Then

                '            Me.dgw_query_view.ClearSelection()
                '           Me.dgw_query_view.CurrentCell = Nothing

                'FILL DATA FROM QUERY TO VARIABLE ARRAY
                For j As Integer = 0 To (user_variables.GetLength(0) - 1) Step 1
                    ' If user_variables(j, 0) = "SUBVIEW" Then user_variables(j, 3) = Me.dgw_query_view.SelectedRows.Item(Me.dgw_query_view.CurrentRow.Index).Cells(CInt(user_variables(j, 2))).Value.ToString
                    If user_variables(j, 0) = "SUBVIEW" Then user_variables(j, 3) = Me.dgw_query_view.Item(CInt(user_variables(j, 2)), Me.dgw_query_view.CurrentRow.Index).Value.ToString()

                Next
            End If

            If chb_clean_form_god_sql.Checked = True Then
                If btn_command.Length = 0 Then
                    fn_reload_user_form(Me.lb_available_reports.SelectedItem.ToString)
                Else
                    fn_reload_user_form(btn_command)
                End If

            End If

        End If


        If chb_clean_form_bad_sql.Checked = True Then
            If btn_command.Length = 0 Then
                fn_reload_user_form(Me.lb_available_reports.SelectedItem.ToString)
            Else
                fn_reload_user_form(btn_command)
            End If
        End If


    End Sub



    Public Function fn_set_keyboard(ByVal sel_keyboard As String)
        Select Case sel_keyboard

            Case "LANG_CZECH"
                Call ActivateKeyboardLayout(LANG_CZECH, 0)
            Case "LANG_ENGLISH"
                Call ActivateKeyboardLayout(LANG_ENGLISH, 0)
            Case "LANG_FRENCH"
                Call ActivateKeyboardLayout(LANG_FRENCH, 0)
            Case "LANG_GERMAN"
                Call ActivateKeyboardLayout(LANG_GERMAN, 0)
            Case "LANG_ITALIAN"
                Call ActivateKeyboardLayout(LANG_ITALIAN, 0)
            Case "LANG_NORWEGIAN"
                Call ActivateKeyboardLayout(LANG_NORWEGIAN, 0)
            Case "LANG_PORTUGUESE"
                Call ActivateKeyboardLayout(LANG_PORTUGUESE, 0)
            Case "LANG_RUSSIAN"
                Call ActivateKeyboardLayout(LANG_RUSSIAN, 0)
            Case "LANG_SPANISH"
                Call ActivateKeyboardLayout(LANG_SPANISH, 0)
            Case "LANG_UKRAINE"
                Call ActivateKeyboardLayout(LANG_UKRAINE, 0)
            Case Else
        End Select
    End Function




    Private Sub t_system_time_Tick_1(sender As Object, e As EventArgs) Handles t_system_time.Tick
        Me.Text = "TERMINAL STUDIO VERZE 1.77" + "   " + CStr(Date.Now)

        If last_autosubmit < Date.Now And autosubmit_interval > 0 Then
            last_autosubmit = Date.Now.AddSeconds(autosubmit_interval)
            Me.dgw_query_view.DataSource = ""
            Me.dgw_query_view.Refresh()
            Me.dgw_summary_view.DataSource = ""
            Me.dgw_summary_view.Refresh()

            Me.Submit_Form(Me, System.EventArgs.Empty)
        End If

        If last_autoroot < Date.Now And autoroot_interval > 0 Then
            last_autoroot = Date.Now.AddSeconds(autoroot_interval)
            Me.lb_available_reports.SelectedIndex = 0
            autoroot_interval = 0
            btn_command = ""
            Me.p_user_inputs.Controls.Clear()
            Me.p_sub_buttons.Controls.Clear()
            Me.dgw_query_view.DataSource = ""
            Me.dgw_query_view.Refresh()
            Me.dgw_summary_view.DataSource = ""
            Me.dgw_summary_view.Refresh()

            Me.btn_back.Enabled = False
            ReDim user_variables(0, 3)
            ReDim temp_variables(0, 3)
            fn_load_selected_dataview(Me.lb_available_reports.SelectedItem.ToString)
        End If


    End Sub




    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        btn_command = ""
        Me.p_user_inputs.Controls.Clear()
        Me.p_sub_buttons.Controls.Clear()
        Me.dgw_query_view.DataSource = ""
        Me.dgw_query_view.Refresh()
        Me.dgw_summary_view.DataSource = ""
        Me.dgw_summary_view.Refresh()

        Me.btn_back.Enabled = False
        ReDim user_variables(0, 3)
        ReDim temp_variables(0, 3)
        fn_load_selected_dataview(Me.lb_available_reports.SelectedItem.ToString)
    End Sub



    Private Sub btn_sql_login_test_Click(sender As Object, e As EventArgs) Handles btn_sql_login_test.Click
        'Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection("Data Source=" + Me.txt_db_name.Text + ";Initial Catalog=" + Me.txt_db_name.Text + ";Persist Security Info=True;User ID=" + Me.txt_db_loginname.Text + ";Password=" + Me.txt_db_password.Text + "")
        'Dim cmd As New System.Data.SqlClient.SqlCommand(query, sqlConnection_string)
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection("Data Source=" + Me.txt_mssql_server.Text + ";Persist Security Info=True;User ID=" + Me.txt_mssql_name.Text + ";Password=" + Me.txt_mssql_password.Text + "")
            'Initial Catalog=" + Me.txt_db_name.Text + ";
            sqlConnection_string.Open()
            MsgBox("Připojení k Databázi proběhlo úspěšně")
            sqlConnection_string.Close()
        Catch
            MessageBox.Show("Připojení k Databázi se nezdařilo, opravte Konfiguraci")
        End Try
        Me.Cursor = Cursors.Default
        My.MySettings.Default.Item("sql_connection") = "Data Source=" + Me.txt_mssql_server.Text + ";Persist Security Info=True;User ID=" + Me.txt_mssql_name.Text + ";Password=" + Me.txt_mssql_password.Text + ""
    End Sub


    Private Sub cb_reports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_reports.SelectedIndexChanged

        Try
            If cb_reports.SelectedIndex > 0 Then
                If available_reports(2, cb_reports.SelectedIndex) = "CRYSTAL" Then


                    Me.Enabled = False
                    selected_report = available_reports(0, cb_reports.SelectedIndex) + ";" + available_reports(1, cb_reports.SelectedIndex) + ";" + available_reports(3, cb_reports.SelectedIndex)
                    Dim temp_string = selected_report.Split(";")
                    Me.Text = "Reporter: " + temp_string(0)
                    My.Forms.frm_crystal_reports.Show()
                    frm_crystal_reports.CR_Viewer.Enabled = True
                ElseIf available_reports(2, cb_reports.SelectedIndex) = "STIM_REPORT" Then
                    st_report.Load(IO.Path.Combine(root_directory, Me.report_Directory, available_reports(1, cb_reports.SelectedIndex)))
                    Dim tmp_string() = available_reports(3, cb_reports.SelectedIndex).ToString.Split(";")
                    Dim separate_param
                    For Each i In tmp_string


                        If i.Trim.Length > 0 Then
                            separate_param = i.Split("/")

                            For Each k In st_report.Dictionary.Variables

                                If k.Name = separate_param(0) Then
                                    Try
                                        For s As Integer = 0 To (Me.user_variables.GetLength(0) - 1) Step 1
                                            If separate_param(1).Replace("*", "") = Me.user_variables(s, 1) Then
                                                separate_param(1) = Me.user_variables(s, 3).Replace("'", "")
                                            End If
                                        Next
                                    Catch ex As Exception
                                    End Try
                                    'MessageBox.Show(separate_param(0).ToString + " / " + separate_param(1).ToString)
                                    k.Value = separate_param(1)
                                End If
                            Next
                        End If

                    Next

                    'For Each k In st_report.Dictionary.Variables
                    '    If k.Name = "test" Then
                    '        k.Value = "PLE052888000"
                    '    End If
                    'Next
                    st_report.Reset()
                    st_report.Compile()
                    '        ast_report.Dictionary.Synchronize()
                    st_report.Render()
                    'st_report.ShowWithWpfRibbonGUI()
                    st_report.ShowWithRibbonGUI()
                    'st_report.Show()

                End If

                cb_reports.SelectedIndex = 0
            End If
        Catch ex As Exception
            cb_reports.SelectedIndex = 0
        End Try
    End Sub


    Public Sub run_CRYSTAL_Report(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For j As Integer = 0 To (user_field_count) Step 1
            For Each subbutton In Me.p_sub_buttons.Controls.OfType(Of Button)()
                If subbutton.Name = user_field_list(j, 0) And subbutton.Focused = True Then
                    Me.Enabled = False
                    selected_report = available_reports(0, subbutton.Tag) + ";" + available_reports(1, subbutton.Tag) + ";" + available_reports(3, subbutton.Tag)
                    Dim temp_string = selected_report.Split(";")
                    Me.Text = "Reporter: " + temp_string(0)
                    My.Forms.frm_crystal_reports.Show()
                    frm_crystal_reports.CR_Viewer.Enabled = True
                End If
            Next
        Next
    End Sub



    Public Sub run_STIM_Report(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For j As Integer = 0 To (user_field_count) Step 1
            For Each subbutton In Me.p_sub_buttons.Controls.OfType(Of Button)()
                If subbutton.Name = user_field_list(j, 0) And subbutton.Focused = True Then
                    st_report.Load(IO.Path.Combine(root_directory, Me.report_Directory, available_reports(1, subbutton.Tag)))
                    Dim tmp_string() = available_reports(3, subbutton.Tag).ToString.Split(";")
                    Dim separate_param
                    For Each i In tmp_string
                        If i.Trim.Length > 0 Then
                            separate_param = i.Split("/")
                            For Each k In st_report.Dictionary.Variables
                                If k.Name = separate_param(0) Then
                                    Try
                                        For s As Integer = 0 To (Me.user_variables.GetLength(0) - 1) Step 1
                                            If separate_param(1).Replace("*", "") = Me.user_variables(s, 1) Then
                                                separate_param(1) = Me.user_variables(s, 3).Replace("'", "")
                                            End If
                                        Next
                                    Catch ex As Exception
                                    End Try
                                    k.Value = separate_param(1)
                                End If
                            Next
                        End If
                    Next
                    st_report.Reset()
                    st_report.Compile()
                    st_report.Render()
                    'st_report.ShowWithWpfRibbonGUI()
                    'st_report.ShowDotMatrix()
                    'st_report.ShowDotMatrixWithRibbonGUI()
                    st_report.ShowWithRibbonGUI()
                    'st_report.Show()
                    'Stimulsoft.Report.StiReport.GetReport.ShowWithRibbonGUI()
                End If
            Next
        Next
    End Sub


    Public Sub run_PDF_file(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For j As Integer = 0 To (user_field_count) Step 1
            For Each subbutton In Me.p_sub_buttons.Controls.OfType(Of Button)()

                If subbutton.Name = user_field_list(j, 0) And subbutton.Focused = True Then
                    Dim tmp_string() = subbutton.Tag.ToString.Split(";")
                    Dim separate_param
                    For Each i In tmp_string
                        If i.Trim.Length > 0 Then
                            separate_param = i
                            Try
                                For s As Integer = 0 To (Me.user_variables.GetLength(0) - 1) Step 1
                                    If InStr(separate_param, "*" + Me.user_variables(s, 1) + "*", CompareMethod.Text) Then
                                        separate_param = separate_param.Replace("*" + Me.user_variables(s, 1) + "*", Me.user_variables(s, 3).Replace("'", "").Replace(" ", ""))
                                        'MessageBox.Show(separate_param)
                                        If fn_check_file(separate_param) Then
                                            Dim open_file As New ProcessStartInfo
                                            open_file.FileName = separate_param
                                            open_file.UseShellExecute = True
                                            Process.Start(open_file)
                                            Exit Sub
                                        End If

                                    End If

                                Next
                            Catch ex As Exception

                            End Try

                        End If
                    Next
                    If subbutton.AutoSize = True Then MessageBox.Show("Výkres neexistuje")
                End If
            Next
        Next
    End Sub


    Private Sub dgw_query_view_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgw_query_view.CellContentClick
        For j As Integer = 0 To (user_field_count) Step 1
            If user_variables(j, 0) = "SUBVIEW" Then
                user_variables(j, 3) = Me.dgw_query_view.Item(CInt(user_variables(j, 2)), Me.dgw_query_view.CurrentRow.Index).Value.ToString
            End If

        Next
    End Sub



    Private Sub btn_own_def_folder_Click(sender As Object, e As EventArgs) Handles btn_own_def_folder.Click
        fbd_own_definitions_folder.Reset()

        If txt_own_def_location.Text <> "" Then
            fbd_own_definitions_folder.Reset()
            fbd_own_definitions_folder.SelectedPath = txt_own_def_location.Text
        End If
        Dim result As DialogResult = fbd_own_definitions_folder.ShowDialog()
        If (result = DialogResult.OK) Then
            txt_own_def_location.Text = fbd_own_definitions_folder.SelectedPath
            ' fn_save_config()
        End If
    End Sub


    Private Sub chb_own_def_loc_CheckedChanged(sender As Object, e As EventArgs) Handles chb_own_def_loc.CheckedChanged
        If chb_own_def_loc.Checked = True Then
            btn_own_def_folder.Enabled = True
            txt_own_def_location.Enabled = True
        Else
            btn_own_def_folder.Enabled = False
            txt_own_def_location.Enabled = False
            txt_own_def_location.Text = ""
        End If
    End Sub
End Class


