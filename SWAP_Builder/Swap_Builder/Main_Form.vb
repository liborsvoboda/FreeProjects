Imports System.Data
Imports System.Data.Common.DbDataRecord
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Tools

Public Class Main_Form

    'arrays Protected Friend
    Public Shared sql_array_count As Integer = 0
    Public Shared sql_array(0, 0) As String

    Public Shared sql_subarray0(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray1(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray2(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray3(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray4(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray5(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray6(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray7(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray8(0, 0) As String ' loaded form setting for subform definition <- no subform data
    Public Shared sql_subarray9(0, 0) As String ' loaded form setting for subform definition <- no subform data

    Public Shared sql_array_addon_count As Integer = 0
    Public Shared sql_array_addon(0, 0) As String
    Public Shared sql_array_bck(0, 0) As String
    Protected Friend language_array(0, 0) As String
    Protected Friend available_reports(2, 0) As String  'name/file/params
    Protected Friend selected_report As String = ""
    Protected Friend user_variables(0, 2) As String 'field type/name/value
    Protected Friend default_settings(0, 2) As String 'field type/name/value

    Public Shared user_form_field_list(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list0(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list1(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list2(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list3(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list4(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list5(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list6(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list7(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list8(29, 0) As String '+dbtype for saving
    Public Shared user_subform_field_list9(29, 0) As String '+dbtype for saving
    Public Shared user_attachments_field_list(29, 0) As String '+dbtype for saving

    Protected Friend dev_form_field_list(29, 0) As String
    Protected Friend updated_dev_form_field_list(29, 0) As String
    Protected Friend selected_picture As Integer
    Protected Friend substitution(0, 4) As String 'name/default_value/program_value/note
    Protected Friend where_array(5, 0) As String 'field | mark | value | type| object name| data type



    'files
    Public report_Directory As String = "REPORTS"
    Public dir_sql_Directory As String = "SQL_COMMANDS"
    Public dir_sql_SubDirectory As String = "SUB_SQL_COMMANDS"
    '    Public configuration_file As String = "settings.ini"

    'global variables
    Protected Friend username As String
    Protected Friend system_account As Boolean = False

    'temp
    Public temp_array(0, 2) As String
    Public temp_integer As Integer
    Public temp_string As String
    Public Shared sql_parameter As SqlClient.SqlCommand = New SqlClient.SqlCommand("")
    Public app_loaded = False
    Public disabled_reaction = False 'for global using disable reload reaction checked,select another

    Public available_languages As String
    Public btn_command As String = ""
    Public rootPreviousTabPage As Integer = 0

    Public last As String

    'sys addons
    Protected Friend Declare Function ActivateKeyboardLayout Lib "user32.dll" (ByVal myLanguage As Long, Flag As Boolean) As Long
    Protected Friend Const LANG_CZECH = 1029
    Protected Friend Const LANG_ENGLISH = 1033
    Protected Friend Const LANG_FRENCH = 1036
    Protected Friend Const LANG_GERMAN = 1031
    Protected Friend Const LANG_ITALIAN = 1040
    Protected Friend Const LANG_NORWEGIAN = 1043
    Protected Friend Const LANG_PORTUGUESE = 1046
    Protected Friend Const LANG_RUSSIAN = 1049
    Protected Friend Const LANG_SPANISH = 1034
    Protected Friend Const LANG_UKRAINE = 1058


    ' start of root reation

    Private Sub pb_waiting_Click(sender As Object, e As EventArgs) Handles pb_waiting_1.Click, pb_waiting_2.Click, pb_waiting_3.Click, pb_waiting_4.Click, pb_waiting_5.Click
        pb_waiting_1.BorderStyle = BorderStyle.None
        pb_waiting_2.BorderStyle = BorderStyle.None
        pb_waiting_3.BorderStyle = BorderStyle.None
        pb_waiting_4.BorderStyle = BorderStyle.None
        pb_waiting_5.BorderStyle = BorderStyle.None
        Select Case sender.name.ToString
            Case "pb_waiting_1"
                selected_picture = 1
                pb_waiting_1.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_2"
                selected_picture = 2
                pb_waiting_2.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_3"
                selected_picture = 3
                pb_waiting_3.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_4"
                selected_picture = 4
                pb_waiting_4.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_5"
                selected_picture = 5
                pb_waiting_5.BorderStyle = BorderStyle.Fixed3D
            Case Else
        End Select
    End Sub

    'end of  root reaction

    'START OF MAIN FORM object reaction
    Public Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        Application.DoEvents()
        My.MySettings.Default.Item("internal_sql_connection") = "Data Source=(LocalDB)\v13.0;AttachDbFilename=" & IO.Path.Combine(Application.StartupPath, "App_structure.mdf") & ";Initial Catalog=App_structure;Integrated Security=True;Persist Security Info=True;Connect Timeout=60;Context Connection=False"
        fn_load_default_settings()
        ReDim user_variables(0, 2)
        ReDim temp_array(0, 2)
    End Sub

    Private Sub Main_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        fn_load_languages()
        fn_load_substitution()

        temp_integer = 0
        For Each temp_string In default_settings
            If temp_string.ToString.Contains("default_language") Then
                lb_global_settings_default_language.SelectedItem = default_settings(temp_integer, 1)
                Exit For
            End If
            temp_integer += 1
        Next

        fn_translate_main_form()
        fn_remove_user_detail_form()
        fn_load_dev_form_type_list()
        frm_logon.Show()
        frm_logon.Activate()
        frm_logon.Focus()

        'disable doklad 
        setTabPageAllowed(tc_data.TabPages.Item(1), False)

    End Sub




    Private Sub frm_logon_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim result = MsgBox(fn_translate("close_app?"), MsgBoxStyle.YesNo, fn_translate("close_app"))
        If result = vbYes Then
            Application.Exit()
        Else
            e.Cancel = True
        End If
    End Sub





    'END OF MAIN FORM object reaction







    'START OF LEFT menu object reaction



    Private Sub btn_sql_login_test_Click(sender As Object, e As EventArgs) Handles btn_global_settings_sql_login_test.Click
        fn_cursor_waiting(True)
        Try
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection("Data Source=" & Me.txt_global_settings_mssql_server.Text & ";Persist Security Info=True;User ID=" & Me.txt_global_settings_mssql_name.Text & ";Password=" & Me.txt_global_settings_mssql_password.Text & "")
            sqlConnection_string.Open()
            MsgBox(fn_translate("connect_to_database_was_successfully"))
            sqlConnection_string.Close()
        Catch
            MessageBox.Show(fn_translate("connect_to_database_was_not_successfully"))
        End Try
        fn_cursor_waiting(False)
        My.MySettings.Default.Item("external_sql_connection") = "Data Source=" & Me.txt_global_settings_mssql_server.Text & ";Persist Security Info=True;User ID=" & Me.txt_global_settings_mssql_name.Text & ";Password=" & Me.txt_global_settings_mssql_password.Text & ""
    End Sub





    'APLICATION MENU
    Private Sub txt_menu_search_selection(sender As Object, e As EventArgs) Handles txt_menu_search.MouseClick, txt_menu_search.GotFocus
        If Me.Visible Then
            If txt_menu_search.Text = fn_translate("search") Then
                txt_menu_search.SelectAll()
            End If
        End If
    End Sub


    Private Sub txt_menu_search_TextChanged(sender As Object, e As EventArgs) Handles txt_menu_search.TextChanged
        If Me.Visible Then
            If txt_menu_search.Text = fn_translate("search") Then
                txt_menu_search.ForeColor = Color.Gray
            Else
                txt_menu_search.ForeColor = Color.Black
            End If
        End If
    End Sub


    Private Sub btn_search_menu_Click(sender As Object, e As EventArgs) Handles btn_search_menu.Click
        fn_load_menu(txt_menu_search.Text)
        tv_menu.ExpandAll()
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_search_clear_Click(sender As Object, e As EventArgs) Handles btn_search_clear.Click
        txt_menu_search.Text = fn_translate("search")
        txt_menu_search.ForeColor = Color.Gray
        fn_load_menu("")
        fn_cursor_waiting(False)
    End Sub




    'FAVORITE MENU


    Private Sub tv_favorites_menu_Doubleclick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_favorites_menu.DoubleClick
        If tv_favorites_menu.SelectedNode IsNot Nothing AndAlso tv_favorites_menu.SelectedNode.Name.Contains("_") Then
            Me.tv_menu.SelectedNode = Me.tv_menu.Nodes.Find(tv_favorites_menu.SelectedNode.Name.Split("_")(1).ToString, True).FirstOrDefault()
        End If
    End Sub



    Private Sub tv_favorites_menu_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tv_favorites_menu.AfterSelect
        If Me.tv_favorites_menu.SelectedNode IsNot Nothing AndAlso Me.tv_favorites_menu.SelectedNode.Level >= 0 Then
            If Me.tv_favorites_menu.SelectedNode.Nodes.Count = 0 Then
                btn_del_favorite.Enabled = True
            Else
                btn_del_favorite.Enabled = False
            End If

            If Me.tv_favorites_menu.SelectedNode.Level = 0 Then
                btn_add_favorite_item.Enabled = True
            Else
                btn_add_favorite_item.Enabled = False
            End If
        Else
            btn_add_favorite_item.Enabled = False
            btn_del_favorite.Enabled = False
        End If
    End Sub




    Private Sub btn_add_favorite_group_Click(sender As Object, e As EventArgs) Handles btn_add_favorite_group.Click
        'frm_new_menu_item.Menu_id = tv_dev_menu.SelectedNode.Text
        Me.temp_array(0, 2) = 0 'level
        frm_new_menu_item.menu_type = "FAVM"
        frm_new_menu_item.Show()
        Me.Enabled = False
    End Sub


    Private Sub btn_del_favorite_Click(sender As Object, e As EventArgs) Handles btn_del_favorite.Click
        If tv_favorites_menu.SelectedNode IsNot Nothing Then
            Dim result = MsgBox(fn_translate("want_to_delete_favorite_group?") & " " & Me.tv_favorites_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_favorite_group"))
            If result = vbYes Then
                If Me.tv_favorites_menu.SelectedNode.Level = 0 Then
                    fn_sql_request("DELETE FROM [dbo].[favorite_menu_list] WHERE [Id] = " & Me.tv_favorites_menu.SelectedNode.Name & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                Else
                    fn_sql_request("DELETE FROM [dbo].[favorite_form_list] WHERE [Id] = " & Me.tv_favorites_menu.SelectedNode.Name.Split("_")(0).ToString & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                End If
                fn_reload_favorite_menu(True)
            End If
        End If
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_add_favorite_item_Click(sender As Object, e As EventArgs) Handles btn_add_favorite_item.Click
        Me.Enabled = False
        frm_new_favorite_item.Show()
    End Sub


    'PRINT MENU


    Private Sub tv_print_menu_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tv_print_menu.AfterSelect
        Try
            If tv_print_menu.SelectedNode Is Nothing Then

                btn_print_add.Enabled = True
                btn_print_del.Enabled = False
            Else
                btn_print_add.Enabled = True

                If tv_print_menu.SelectedNode.Nodes.Count = 0 Then
                    btn_print_del.Enabled = True
                Else
                    btn_print_del.Enabled = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btn_default_print_Click(sender As Object, e As EventArgs) Handles btn_default_print.Click
        fn_cursor_waiting(True)
        Try
            Dim bindings As String = ""
            For Each substring In fn_search_substitution("sub[default_print_document]").Split("*")(1).Split(",")
                If substring.Length > 0 Then
                    For colindex = 0 To dgw_query_view.Columns.Count
                        If dgw_query_view.Columns(colindex).Name = substring Then
                            bindings &= "&" & substring & "=" & dgw_query_view.Rows(dgw_query_view.CurrentCell.RowIndex).Cells(CInt(colindex)).Value().ToString
                            Exit For
                        End If
                    Next
                End If
            Next
            Dim ID As Integer
            fn_delete_file(IO.Path.Combine(Application.StartupPath, "fyiviewer", "readerstate.xml"))
            ID = Shell(IO.Path.Combine(Application.StartupPath, "fyiviewer", "RdlReader.exe") & " """ & IO.Path.Combine(Application.StartupPath, "REPORTS", fn_search_substitution("sub[default_print_document]").Split("*")(0).ToString) & """ -p ""connect=" & My.Settings.internal_sql_connection & bindings & """", AppWinStyle.MaximizedFocus, True, -1)
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("report_viewer_cannot_be_opened"))
        End Try
        fn_cursor_waiting(False)
    End Sub


    Private Sub tv_print_menu_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_print_menu.DoubleClick
        fn_cursor_waiting(True)
        Try
            Dim bindings As String = ""
            If tv_print_menu.SelectedNode IsNot Nothing Then
                For Each substring In tv_print_menu.SelectedNode.Name.Split("*")(1).Split(",")
                    If substring.Length > 0 Then
                        For colindex = 0 To dgw_query_view.Columns.Count
                            If dgw_query_view.Columns(colindex).Name = substring Then
                                bindings &= "&" & substring & "=" & Me.dgw_query_view.Rows(Me.dgw_query_view.CurrentCell.RowIndex).Cells((CInt(colindex))).Value().ToString
                                Exit For
                            End If
                        Next
                    End If
                Next

                Dim ID As Integer
                fn_delete_file(IO.Path.Combine(Application.StartupPath, "fyiviewer", "readerstate.xml"))
                ID = Shell(IO.Path.Combine(Application.StartupPath, "fyiviewer", "RdlReader.exe") & " """ & IO.Path.Combine(Application.StartupPath, "REPORTS", tv_print_menu.SelectedNode.Name.Remove(0, (tv_print_menu.SelectedNode.Name.Split("_")(0).Length + 1)).Split("*")(0).ToString) & """ -p ""connect=" & My.Settings.internal_sql_connection & bindings & """", AppWinStyle.MaximizedFocus, True, -1)
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("report_viewer_cannot_be_opened"))
        End Try
        fn_cursor_waiting(False)
    End Sub



    Private Sub btn_print_group_add_Click(sender As Object, e As EventArgs) Handles btn_print_group_add.Click
        'frm_new_menu_item.Menu_id = tv_dev_menu.SelectedNode.Text
        Me.temp_array(0, 2) = 0 'level
        frm_new_menu_item.menu_type = "PAVM"
        frm_new_menu_item.Show()
        Enabled = False
    End Sub



    Private Sub btn_print_add_Click(sender As Object, e As EventArgs) Handles btn_print_add.Click
        frm_new_file_item.menu_type = "PAVM"
        frm_new_file_item.Show()
        Enabled = False
    End Sub


    Private Sub btn_print_del_Click(sender As Object, e As EventArgs) Handles btn_print_del.Click
        If Me.tv_print_menu.SelectedNode IsNot Nothing Then
            Dim result
            If Me.tv_print_menu.SelectedNode.Level = 0 Then
                result = MsgBox(fn_translate("want_to_delete_print_group?") & " " & Me.tv_print_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_print_group"))
            Else
                result = MsgBox(fn_translate("want_to_delete_print_document?") & " " & Me.tv_print_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_print_document"))
            End If
            If result = vbYes Then
                If Me.tv_print_menu.SelectedNode.Level = 0 Then
                    If Not Me.tv_print_menu.SelectedNode.Name.Contains("_") Then
                        fn_sql_request("DELETE FROM [dbo].[print_menu_list] WHERE [Id] = " & Me.tv_print_menu.SelectedNode.Name & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                    Else
                        fn_sql_request("DELETE FROM [dbo].[print_form_list] WHERE [Id] = " & Me.tv_print_menu.SelectedNode.Name.Split("_")(0).ToString & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                    End If
                Else
                    fn_sql_request("DELETE FROM [dbo].[print_form_list] WHERE [Id] = " & Me.tv_print_menu.SelectedNode.Name.Split("_")(0).ToString & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                End If

                If dgw_query_view.SelectedCells IsNot Nothing Then
                    fn_reload_print_menu(True, True)
                Else
                    fn_reload_print_menu(False, True)
                End If

            End If
        End If
    End Sub



    'REPORT MENU
    Private Sub tv_report_menu_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tv_report_menu.AfterSelect
        Try
            If tv_report_menu.SelectedNode Is Nothing Then
                btn_report_add.Enabled = True
                btn_report_del.Enabled = False
            Else
                btn_report_add.Enabled = True

                If tv_report_menu.SelectedNode.Nodes.Count = 0 Then
                    btn_report_del.Enabled = True
                Else
                    btn_report_del.Enabled = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub tv_report_menu_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_report_menu.DoubleClick
        fn_cursor_waiting(True)
        Try
            Dim bindings As String = ""
            If tv_report_menu.SelectedNode IsNot Nothing Then
                For Each substring In tv_report_menu.SelectedNode.Name.Split("*")(1).Split(",")
                    If substring.Length > 0 Then
                        For colindex = 0 To dgw_query_view.Columns.Count
                            If dgw_query_view.Columns(colindex).Name = substring Then
                                bindings &= "&" & substring & "=" & dgw_query_view.Rows(Me.dgw_query_view.CurrentCell.RowIndex).Cells(CInt(colindex)).Value().ToString
                                Exit For
                            End If
                        Next
                    End If
                Next

                Dim ID As Integer
                fn_delete_file(IO.Path.Combine(Application.StartupPath, "fyiviewer", "readerstate.xml"))
                ID = Shell(IO.Path.Combine(Application.StartupPath, "fyiviewer", "RdlReader.exe") & " """ & IO.Path.Combine(Application.StartupPath, "REPORTS", tv_report_menu.SelectedNode.Name.Remove(0, (tv_report_menu.SelectedNode.Name.Split("_")(0).Length + 1)).Split("*")(0).ToString) & """ -p ""connect=" & My.Settings.internal_sql_connection & bindings & """", AppWinStyle.MaximizedFocus, True, -1)
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("report_viewer_cannot_be_opened"))
        End Try
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_report_group_add_Click(sender As Object, e As EventArgs) Handles btn_report_group_add.Click

        If tv_menu.SelectedNode.Name.Contains("SQL") Then
            'frm_new_menu_item.Menu_id = tv_dev_menu.SelectedNode.Text
            Me.temp_array(0, 2) = 0 'level
            frm_new_menu_item.menu_type = "RAVM"
            frm_new_menu_item.Show()
            Me.Enabled = False
        End If
    End Sub



    Private Sub btn_report_add_Click(sender As Object, e As EventArgs) Handles btn_report_add.Click
        frm_new_file_item.menu_type = "RAVM"
        frm_new_file_item.Show()
        Me.Enabled = False
    End Sub


    Private Sub btn_report_del_Click(sender As Object, e As EventArgs) Handles btn_report_del.Click
        If Me.tv_report_menu.SelectedNode IsNot Nothing Then
            Dim result = MsgBox(fn_translate("want_to_delete_report_group?") & " " & Me.tv_report_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_report_group"))
            If result = vbYes Then
                If Me.tv_report_menu.SelectedNode.Level = 0 Then
                    If Not Me.tv_report_menu.SelectedNode.Name.Contains("_") Then
                        fn_sql_request("DELETE FROM [dbo].[report_menu_list] WHERE [Id] = " & Me.tv_report_menu.SelectedNode.Name & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                    Else
                        fn_sql_request("DELETE FROM [dbo].[report_form_list] WHERE [Id] = " & Me.tv_report_menu.SelectedNode.Name.Split("_")(0).ToString & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                    End If
                Else
                    fn_sql_request("DELETE FROM [dbo].[report_form_list] WHERE [Id] = " & Me.tv_report_menu.SelectedNode.Name.Split("_")(0).ToString & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                End If

                If dgw_query_view.SelectedCells IsNot Nothing Then
                    fn_reload_report_menu(True, True)
                Else
                    fn_reload_report_menu(False, True)
                End If
            End If
        End If
    End Sub

    'END OF LEFT menu object reaction





    'START of Every dev menu change


    '  blocate move to disabled user tabpages
    Private Sub TabControl_Deselected(sender As Object, e As TabControlEventArgs) Handles tc_data.Deselected
        rootPreviousTabPage = e.TabPageIndex
    End Sub

    Private Sub TabControl_Selected(sender As Object, e As TabControlEventArgs) Handles tc_data.Selected
        If Not tc_data.SelectedTab.Enabled Then
            tc_data.SelectedIndex = rootPreviousTabPage
        End If
    End Sub

    '  all tabpages selection changed
    Public Sub menu_change_reaction(sender As Object, e As EventArgs) Handles tc_dev_menu.SelectedIndexChanged, tc_data.SelectedIndexChanged, tc_user_document.SelectedIndexChanged ', dgw_query_view.CurrentCellChanged
        Try

            'çhecking main buttons
            fn_main_button_translate()
            fn_sql_check_button("", "", True)


            If tc_data.SelectedTab.Name = "tp_datalist" Then 'data view
                'return to dataview enable button
                If fn_check_user_form_definition(Me.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString) Then
                    If fn_sql_request("SELECT * FROM [dbo].[form_definition] WHERE [form_id]=" & lbl_user_form_id.Text & " ORDER BY input_no,value_no ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                        btn_main_btn_1.Enabled = True
                    End If
                End If

            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And tc_user_document.SelectedIndex = 0 Then 'detailview
                btn_main_create_copy.Enabled = False
                If actual_db_task = db_task_list(1) Then
                    btn_main_btn_1.Enabled = True

                End If
                If actual_db_task = db_task_list(2) Then
                    btn_main_btn_2.Enabled = True
                    fn_reload_report_menu(True, True)
                    fn_reload_print_menu(True, True)
                End If
            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And tc_user_document.SelectedIndex > 0 Then 'subform detailview

                'TODO
                btn_main_btn_1.Enabled = True
                tc_user_document.TabPages.Item(tc_user_document.SelectedIndex).Select()
                For Each ctrl As DataGridView In tc_user_document.TabPages.Item(tc_user_document.SelectedIndex).Controls.OfType(Of DataGridView)
                    ctrl.CurrentCell = Nothing
                    ctrl.ClearSelection()
                Next

            ElseIf tc_data.SelectedTab.Name = "tp_dev_builder" Then 'dev menu builder
                If Me.tc_dev_menu.SelectedIndex = 0 Or Me.tc_dev_menu.SelectedIndex = 1 Then 'DEV SELECT and note
                    fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_list WHERE id=" & Me.lbl_dev_form_id.Text, "LOCAL", False)
                    If Me.lbl_dev_form_id.Text.Length = 0 And Me.txt_dev_form_name.Text.Length <> 0 Then btn_main_btn_1.Enabled = True
                ElseIf Me.tc_dev_menu.SelectedIndex = 2 Then 'dev detail
                    fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE form_id=" & Me.lbl_dev_form_id.Text, "LOCAL", False)
                ElseIf Me.tc_dev_menu.SelectedIndex = 3 Then 'dev subforms
                    fn_dev_subform_enable_main_buttons()
                End If
            End If

            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_sql_check_button("", "", True)
            fn_cursor_waiting(False)
        End Try
    End Sub



    'END of  Every menu change






    'START OF GLOBAL SETTING



    Private Sub btn_save_setting_Click(sender As Object, e As EventArgs) Handles btn_save_setting.Click
        fn_save_setting()
    End Sub


    'END OF GLOBAL SETTING





    'DATAVIEW MENU 
    Public Sub tv_menu_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tv_menu.AfterSelect
        fn_cursor_waiting(True)
        Me.dgw_query_view.DataSource = ""
        Me.dgw_query_view.Columns.Clear()
        Me.dgw_query_view.Refresh()
        Me.dgw_query_view.ClearSelection()
        Me.dgw_query_view.CurrentCell = Nothing

        Me.dgw_summary_view.DataSource = ""
        Me.dgw_summary_view.Refresh()
        btn_user_refresh.Enabled = False
        Me.btn_command = ""
        fn_filter_menu_clear()
        fn_report_menu_clear()
        fn_print_menu_clear()
        fn_remove_user_detail_form()
        lbl_record_count_loaded_no.ForeColor = Color.Black
        lbl_record_count_loaded_no.Text = ""
        actual_db_task = ""

        setTabPageAllowed(tc_data.TabPages.Item(1), False)

        ReDim user_variables(0, 2)
        ReDim temp_array(0, 2)
        Try
            menu_change_reaction(sender, e)
            If IsNumeric(Me.tv_menu.SelectedNode.Name) Then
                'tree menu
                tp_datalist.Text = fn_translate("datalist")

            ElseIf Me.tv_menu.SelectedNode.Name.Contains("SQL") Then
                fn_load_basic_form(Me.tv_menu.SelectedNode.Name.ToString)
                tp_datalist.Text = fn_translate("datalist") & ": " & Me.tv_menu.SelectedNode.Text
                btn_user_refresh.Enabled = True
                If fn_check_user_form_definition(Me.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString) Then
                    If fn_load_user_form_definition() Then
                        btn_main_btn_1.Enabled = True
                    Else
                        btn_main_btn_1.Enabled = False
                        btn_main_btn_2.Enabled = False
                        btn_main_btn_3.Enabled = False
                        btn_main_create_copy.Enabled = False
                    End If
                End If
            ElseIf Me.tv_menu.SelectedNode.Name.Contains("TERMINAL") Then
                'load special form for terminal using
                tp_datalist.Text = fn_translate("datalist") & ": " & Me.tv_menu.SelectedNode.Text

            End If
            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_cursor_waiting(False)
        End Try
    End Sub


    Private Sub bnt_logout_Click(sender As Object, e As EventArgs) Handles bnt_logout.Click
        Dim result = MsgBox(fn_translate("New login?"), MsgBoxStyle.YesNo, fn_translate("new_login"))
        If result = vbYes Then
            Application.Restart()
        End If

    End Sub





    'FILTER MENU 
    Public Sub tv_filter_menu_Click(sender As Object, e As TreeViewEventArgs) Handles tv_filter_menu.AfterSelect
        Dim definition_parts As String()
        Me.txt_filter_command.Text = ""
        user_where = ""
        Dim cmd_no As Integer = 0
        ReDim Me.where_array(5, 0)
        btn_filter_clear.Enabled = True
        Try
            lbl_filtername_selected.Text = ""
            If fn_sql_request("SELECT [definition_cmd],[definition_field],systemname,enable_translate FROM [dbo].[form_filter] WHERE [id]=" & tv_filter_menu.SelectedNode.Name.ToString & " ", "SELECTONEITEM", "local", False, True, sql_parameter, False, False) Then
                user_where = sql_array(0, 0)
                Me.txt_filter_command.Text = Me.sql_array(0, 0)

                If Me.sql_array(0, 3) Then
                    Me.lbl_filtername_selected.Text = fn_translate(Me.sql_array(0, 2))
                Else
                    Me.lbl_filtername_selected.Text = Me.sql_array(0, 2)
                End If

                For Each field In System.Text.RegularExpressions.Regex.Split(Me.sql_array(0, 1), System.Text.RegularExpressions.Regex.Escape("||"))
                    If field.Length > 0 Then
                        definition_parts = field.Split("|")

                        ReDim Preserve Me.where_array(5, cmd_no)
                        'field | mark | value | type| object name

                        If definition_parts(3) = "DateTimePicker" Then
                            Me.where_array(0, cmd_no) = "CAST([" & definition_parts(0) & "] AS DATE)"
                        Else
                            Me.where_array(0, cmd_no) = "[" & definition_parts(0) & "]"
                        End If
                        Me.where_array(1, cmd_no) = definition_parts(1)
                        If definition_parts(3) = "CheckBox" Then
                            Me.where_array(2, cmd_no) = CBool(definition_parts(2)).ToString
                        Else
                            Me.where_array(2, cmd_no) = definition_parts(2)
                        End If
                        Me.where_array(3, cmd_no) = definition_parts(3)
                        Me.where_array(4, cmd_no) = definition_parts(4)
                        Me.where_array(5, cmd_no) = definition_parts(5)

                        cmd_no += 1
                    End If
                Next
            End If
        Catch ex As Exception
            ReDim Me.where_array(5, 0)
            MessageBox.Show(fn_translate("filter_is_SQL_type_only"))
        End Try

        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_user_refresh_Click(sender As Object, e As EventArgs) Handles btn_user_refresh.Click
        fn_load_basic_form("")
        If fn_check_user_form_definition(Me.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString) Then
            If fn_load_user_form_definition() Then
                btn_main_btn_1.Enabled = True
            Else
                btn_main_btn_1.Enabled = False
            End If
        End If

        btn_main_btn_2.Enabled = False
        btn_main_btn_3.Enabled = False
        btn_main_create_copy.Enabled = False
        fn_cursor_waiting(False)
    End Sub


    Private Sub Bbtn_filter_add_Click(sender As Object, e As EventArgs) Handles btn_filter_add.Click
        frm_filter.Show()
        Me.Enabled = False
    End Sub


    Private Sub txt_filter_command_TextChanged(sender As Object, e As EventArgs) Handles txt_filter_command.TextChanged
        If Me.Enabled Then
            user_where = txt_filter_command.Text
            btn_filter_status.BackgroundImage = My.Resources.not_filtered
            btn_filter_status.AccessibleDescription = "N"
        End If
    End Sub


    Private Sub btn_filter_clear_Click(sender As Object, e As EventArgs) Handles btn_filter_clear.Click
        ReDim Me.where_array(5, 0)

        If lbl_filtername_selected.Text.Length > 0 Then
            Dim result = MsgBox(fn_translate("delete_filter_definition?") & vbNewLine & tv_filter_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_filter"))
            If result = vbYes Then
                fn_sql_request("DELETE FROM [dbo].[form_filter] where [id]=" & tv_filter_menu.SelectedNode.Name.ToString & " ", "DELETE", "local", False, True, sql_parameter, False, False)
            End If
        End If

        fn_load_basic_form("")
        fn_load_filter_list()
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_filter_status_Click(sender As Object, e As EventArgs) Handles btn_filter_status.Click
        If Me.txt_filter_command.Text.Length > 0 Then
            user_where = Me.txt_filter_command.Text
            If btn_filter_status.AccessibleDescription = "N" Then
                btn_filter_status.BackgroundImage = My.Resources.filtered
                btn_filter_status.AccessibleDescription = "A"
            Else
                btn_filter_status.BackgroundImage = My.Resources.not_filtered
                btn_filter_status.AccessibleDescription = "N"
            End If
        Else
            btn_filter_status.BackgroundImage = My.Resources.not_filtered
            btn_filter_status.AccessibleDescription = "N"
            fn_load_basic_form("")
        End If

        fn_load_basic_form("")
        fn_cursor_waiting(False)
    End Sub





    'EXPORT MENU
    Private Sub tv_export_import_Doubleclick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_export_import_menu.DoubleClick
        If Me.tv_export_import_menu.SelectedNode.Name.Split("/").Length > 1 Then
            If Me.tv_export_import_menu.SelectedNode.Name.Split("/")(1).ToString = "fn_export_to_xls" Then fn_export_to_xls()
            If Me.tv_export_import_menu.SelectedNode.Name.Split("/")(1).ToString = "fn_export_to_xml" Then fn_export_to_xml()
            If Me.tv_export_import_menu.SelectedNode.Name.Split("/")(1).ToString = "fn_export_to_csv" Then fn_export_to_csv()
            If Me.tv_export_import_menu.SelectedNode.Name.Split("/")(1).ToString = "fn_export_to_pdf" Then fn_export_to_pdf()
        End If
    End Sub






    'DATAVIEW AREA
    Private Sub dgw_query_view_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgw_query_view.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                cmd_dataview_menu.Show(MousePosition)
            ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
                If dgw_query_view.CurrentCell.RowIndex >= 0 Then

                    fn_sql_check_button("", "", True)
                    setTabPageAllowed(tc_data.TabPages.Item(1), False)

                    If dgw_query_view.CurrentCell.RowIndex >= 0 AndAlso dgw_query_view.CurrentCell.Selected Then
                        If primary_key Then
                            btn_main_btn_1.Enabled = True
                            btn_main_btn_2.Enabled = True
                            btn_main_create_copy.Enabled = True
                        End If
                    Else
                        If primary_key Then btn_main_btn_1.Enabled = True
                    End If

                    If primary_key AndAlso dgw_query_view.CurrentCell.Selected Then
                        btn_main_btn_3.Enabled = True
                    End If
                End If

                If btn_main_create_copy.Enabled Then
                    fn_reload_report_menu(True, True)
                    fn_reload_print_menu(True, True)
                Else
                    fn_reload_report_menu(False, True)
                    fn_reload_print_menu(False, True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub dgw_query_view_CellContentDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgw_query_view.CellMouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then

            If e.RowIndex >= 0 And btn_main_btn_2.Enabled Then
                setTabPageAllowed(tc_data.TabPages.Item(1), True)
                actual_db_task = db_task_list(2)
                tc_user_document.SelectedIndex = 0
                tc_data.SelectTab(1)
                fn_clear_user_form()
                fn_fill_detail_form_with_selected_rec(False)
            End If
        End If
    End Sub


    Private Sub dgw_query_view_CurrentCellChanged(sender As Object, e As EventArgs) Handles dgw_query_view.CurrentCellChanged
        Try

            If dgw_query_view.CurrentCell.RowIndex >= 0 Then
                fn_sql_check_button("", "", True)
                setTabPageAllowed(tc_data.TabPages.Item(1), False)

                If dgw_query_view.CurrentCell.RowIndex >= 0 AndAlso dgw_query_view.CurrentCell.Selected Then
                    If primary_key Then
                        btn_main_btn_1.Enabled = True
                        btn_main_btn_2.Enabled = True
                        btn_main_create_copy.Enabled = True
                    End If
                Else
                    If primary_key Then btn_main_btn_1.Enabled = True
                End If

                If primary_key AndAlso dgw_query_view.CurrentCell.Selected Then
                    btn_main_btn_3.Enabled = True
                End If
            End If

            If btn_main_create_copy.Enabled Then
                fn_reload_report_menu(True, True)
                fn_reload_print_menu(True, True)
            Else
                fn_reload_report_menu(False, True)
                fn_reload_print_menu(False, True)
            End If

        Catch ex As Exception

        End Try

    End Sub


    'START OF context menu reaction
    Private Sub tstb_records_count_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tstb_records_count.KeyPress
        If Not (Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Chr(27) AndAlso e.KeyChar <> Chr(13) AndAlso e.KeyChar <> Chr(8)) Then
            e.KeyChar = Nothing
        End If
    End Sub


    Private Sub tstb_rec_select_type_Click(sender As Object, e As EventArgs) Handles tstb_rec_select_type.Click
        If dgw_query_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect Then
            dgw_query_view.SelectionMode = DataGridViewSelectionMode.CellSelect
        Else
            dgw_query_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End If
        fn_cursor_waiting(False)
    End Sub


    Private Sub tstb_copy_rec_Click(sender As Object, e As EventArgs) Handles tstb_copy_rec.Click
        Try
            If (dgw_query_view.SelectedRows.Count > 0 AndAlso dgw_query_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect) OrElse (dgw_query_view.SelectedCells.Count > 0 AndAlso dgw_query_view.SelectionMode = DataGridViewSelectionMode.CellSelect) Then
                If dgw_query_view.SelectionMode = DataGridViewSelectionMode.FullRowSelect Then
                    Clipboard.SetDataObject(dgw_query_view.GetClipboardContent())
                Else

                    If dgw_query_view.Columns.Item(dgw_query_view.CurrentCell.ColumnIndex).CellType.Name = "DataGridViewImageCell" Then
                        If dgw_query_view.CurrentCell.Value IsNot DBNull.Value Then
                            Clipboard.SetImage(fn_byteArrayToImage(dgw_query_view.CurrentCell.Value()))
                        Else
                            MessageBox.Show(fn_translate("image_doesnt_exist"))
                        End If
                    Else
                        Clipboard.SetText(dgw_query_view.CurrentCell.Value)
                    End If

                End If
            End If
        Catch ex As Exception
        End Try
        fn_cursor_waiting(False)
    End Sub


    Private Sub EnterPress_beep(sender As Object, e As KeyEventArgs) Handles tstb_records_count.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            cmd_dataview_menu.Close() 'close context menu
        End If
    End Sub

    Private Sub tstb_records_count_Click(sender As Object, e As EventArgs) Handles tstb_records_count.LostFocus
        If tstb_records_count.Text.Length = 0 Or tstb_records_count.Text = "0" Then
            temp_integer = 0
            For Each temp_string In default_settings
                If temp_string.ToString.Contains("default_records_count") Then
                    tstb_records_count.Text = default_settings(temp_integer, 1)
                    Exit For
                End If
                temp_integer += 1
            Next
        End If
        fn_load_basic_form("")
        fn_cursor_waiting(False)
    End Sub


    Private Sub tstb_select_one_multi_rec_Click(sender As Object, e As EventArgs) Handles tstb_select_one_multi_rec.Click
        If dgw_query_view.MultiSelect Then dgw_query_view.MultiSelect = False Else dgw_query_view.MultiSelect = True
    End Sub



    'END OF context menu reaction












    'START OF DEV FORM BUILDER
    Private Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add_menu.Click
        temp_array(0, 0) = fn_translate("next_to") & " '" & Me.tv_dev_menu.SelectedNode.Text & "'"
        temp_array(0, 1) = "next"
        temp_array(0, 2) = Me.tv_dev_menu.SelectedNode.Level
        frm_new_menu_item.menu_type = "MM"
        frm_new_menu_item.Show()
        Me.Enabled = False
    End Sub


    Private Sub btn_add_under_Click(sender As Object, e As EventArgs) Handles btn_add_under_menu.Click
        temp_array(0, 0) = fn_translate("under") & " '" & Me.tv_dev_menu.SelectedNode.Text & "'"
        temp_array(0, 1) = "under"
        temp_array(0, 2) = Me.tv_dev_menu.SelectedNode.Level
        frm_new_menu_item.menu_type = "MM"
        frm_new_menu_item.Show()
        Me.Enabled = False
    End Sub


    Private Sub btn_dev_menu_edit_Click(sender As Object, e As EventArgs) Handles btn_dev_menu_edit.Click
        frm_new_menu_item.Menu_id = tv_dev_menu.SelectedNode.Name
        frm_new_menu_item.menu_type = "MM"
        frm_new_menu_item.Show()
    End Sub


    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_dev_menu_delete.Click
        fn_cursor_waiting(True)
        If IsNumeric(Me.tv_dev_menu.SelectedNode.Name) = True Then 'menu item
            Dim result = MsgBox(fn_translate("delete_menu_item?") & " " & tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_menu_item"))
            If result = vbYes Then
                If fn_sql_request("DELETE FROM [dbo].[menu_list] WHERE id = " & tv_dev_menu.SelectedNode.Name & " ", "DELETE", "local", False, True, sql_parameter, False, False) = True Then
                    fn_load_menu("")
                End If
            End If
        Else 'form item
            Dim result = MsgBox(fn_translate("delete_form_item?") & " " & tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_form_item"))
            If result = vbYes Then
                If fn_sql_request("DELETE FROM [dbo].[form_list] WHERE id = " & tv_dev_menu.SelectedNode.Name.Replace("SQL", "").Replace("TERMINAL", "") & " ", "DELETE", "local", False, True, sql_parameter, False, False) = True Then
                    fn_sql_request("DELETE FROM [dbo].[form_definition] WHERE form_id = " & tv_dev_menu.SelectedNode.Name.Replace("SQL", "").Replace("TERMINAL", "") & " ", "DELETE", "local", False, True, sql_parameter, False, False)
                    fn_load_menu("")
                End If
            End If
        End If
        fn_cursor_waiting(False)
    End Sub


    Private Sub tv_dev_menu_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tv_dev_menu.AfterSelect
        fn_cursor_waiting(True)
        Try

            Me.dgw_query_view.DataSource = ""
            Me.tv_menu.CollapseAll()
            fn_remove_user_detail_form()
            fn_dev_clean_form()
            Me.lbl_dev_form_id.Text = ""
            'node menu only
            If tv_dev_menu.SelectedNode.Name.Length > 0 AndAlso IsNumeric(Me.tv_dev_menu.SelectedNode.Name) Then
                'not exist sub menu and form
                If fn_sql_request("SELECT 1 FROM [dbo].[menu_list] ml,[dbo].[form_list] fl WHERE ml.parent_menu_id = " & tv_dev_menu.SelectedNode.Name & " OR fl.parent_menu_id = " & tv_dev_menu.SelectedNode.Name & " ", "SELECTONEITEM", "local", False, True, sql_parameter, False, False) = False Then
                    btn_dev_menu_delete.Enabled = True
                    btn_dev_menu_edit.Enabled = True
                Else
                    btn_dev_menu_delete.Enabled = False
                    btn_dev_menu_edit.Enabled = False
                End If

                btn_add_menu.Enabled = True
                If tv_dev_menu.SelectedNode.Level < 1 Then btn_add_under_menu.Enabled = True Else btn_add_under_menu.Enabled = False
                Me.txt_dev_sql_command.Text = ""
                Me.txt_dev_after_sql_command.Text = ""
                fn_check_dev_sql_preview()

                fn_enable_disable_subform(False)
            Else
                btn_dev_menu_edit.Enabled = False
                btn_dev_menu_delete.Enabled = False
                btn_add_menu.Enabled = False
                btn_add_under_menu.Enabled = False
                btn_dev_menu_edit.Enabled = False
                btn_dev_menu_delete.Enabled = True
                fn_load_form_definition(True)
                fn_load_sql_preview(txt_dev_sql_command.Text)
                fn_load_dev_form_definition(True)

                fn_enable_disable_subform(True)
                fn_load_existed_subforms("")
            End If

            fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_list WHERE id=" & Me.lbl_dev_form_id.Text, "LOCAL", False)
            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_sql_check_button("", "", True)
            fn_cursor_waiting(False)
        End Try
    End Sub


    Private Sub txt_menu_name_TextChanged(sender As Object, e As EventArgs) Handles txt_dev_form_name.TextChanged
        If lbl_dev_form_id.Text.Length = 0 Then fn_check_dev_sql_preview()
    End Sub


    Private Sub btn_show_preview_Click(sender As Object, e As EventArgs) Handles btn_dev_show_preview.Click
        If fn_load_sql_preview(Me.txt_dev_sql_command.Text) = True Then
            If Me.lbl_dev_form_id.Text.Length = 0 And Me.txt_dev_form_name.Text.Length > 0 Then btn_main_btn_1.Enabled = True
            If Me.lbl_dev_form_id.Text.Length > 0 And Me.txt_dev_form_name.Text.Length > 0 Then btn_main_btn_2.Enabled = True
        End If
    End Sub


    Private Sub txt_sql_command_TextChanged(sender As Object, e As EventArgs) Handles txt_dev_sql_command.TextChanged, txt_dev_full_save_table_name.TextChanged
        fn_sql_check_button("", "", True)

        Me.btn_dev_create_update_form.Enabled = False

        If Me.txt_dev_sql_command.Text.Length > 0 And txt_dev_full_save_table_name.Text.Length > 0 Then
            btn_dev_show_preview.Enabled = True
            chb_dev_format_debug.Enabled = True
        Else
            btn_dev_show_preview.Enabled = False
            chb_dev_format_debug.Enabled = False
        End If
    End Sub


    Private Sub chb_dev_format_debug_CheckedChanged(sender As Object, e As EventArgs) Handles chb_dev_format_debug.CheckedChanged
        btn_dev_create_update_form.Enabled = False
        If Not disabled_reaction Then
            If fn_load_sql_preview(Me.txt_dev_sql_command.Text) = True Then
                If Me.lbl_dev_form_id.Text.Length = 0 And Me.txt_dev_form_name.Text.Length > 0 Then btn_main_btn_1.Enabled = True
                If Me.lbl_dev_form_id.Text.Length > 0 And Me.txt_dev_form_name.Text.Length > 0 Then btn_main_btn_2.Enabled = True
                If Me.lbl_dev_form_id.Text.Length >= 0 And Me.txt_dev_form_name.Text.Length > 0 And chb_dev_format_debug.Checked Then btn_dev_create_update_form.Enabled = True
            Else
                disabled_reaction = True
                If chb_dev_format_debug.Checked Then
                    chb_dev_format_debug.Checked = False
                Else
                    chb_dev_format_debug.Checked = True
                End If
            End If
        Else
            disabled_reaction = False
        End If
    End Sub

    Private Sub btn_dev_create_update_form_Click(sender As Object, e As EventArgs) Handles btn_dev_create_update_form.Click
        fn_cursor_waiting(True)
        fn_load_sql_preview(Me.txt_dev_sql_command.Text)
        fn_dev_create_form()
        fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE form_id=" & Me.lbl_dev_form_id.Text, "LOCAL", False)
        Me.btn_main_btn_1.Enabled = True
        fn_cursor_waiting(False)
    End Sub


    Public Sub dev_panel_moveclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            Dim selected_panel As Panel
            If selected_dev_field = 0 Then
                selected_dev_field = sender.name.ToString.Split("_").GetValue(0)
                For Each field_object In tp_dev_detail.Controls
                    If field_object.Name = selected_dev_field Then
                        selected_panel = field_object
                        Exit For
                    End If
                Next
                selected_panel.BackColor = dev_selectcolor
            Else
                Dim obj_no = 0
                For Each field_object In tp_dev_detail.Controls
                    obj_no += 1
                    If field_object.Name = selected_dev_field Then
                        selected_panel = field_object
                        Exit For
                    End If
                Next
                If dev_form_field_list(27, (obj_no) * 3 - 1) Then
                    selected_panel.BackColor = dev_backcolor
                Else
                    selected_panel.BackColor = not_null_backcolor
                End If
                'selected_panel.BackColor = dev_backcolor
                dev_form_field_list(4, (obj_no) * 3 - 2) = selected_panel.Location.X
                dev_form_field_list(5, (obj_no) * 3 - 2) = selected_panel.Location.Y
                dev_form_field_list(4, (obj_no) * 3 - 1) = selected_panel.Location.X
                dev_form_field_list(5, (obj_no) * 3 - 1) = selected_panel.Location.Y
                'dev_form_field_list(4, (selected_dev_field) * 3) = tp_dev_detail.Controls.Item(selected_dev_field - 1).Location.X
                'dev_form_field_list(5, (selected_dev_field) * 3) = tp_dev_detail.Controls.Item(selected_dev_field - 1).Location.Y
                selected_dev_field = 0
            End If
        Catch ex As Exception

        End Try

    End Sub



    Public Sub dev_form_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tp_dev_detail.MouseMove
        If selected_dev_field <> 0 Then
            Dim selected_panel As Panel
            For Each field_object In tp_dev_detail.Controls
                If field_object.Name = selected_dev_field Then
                    selected_panel = field_object
                    Exit For
                End If
            Next
            selected_panel.Location = New Point(e.X, e.Y)
        End If
    End Sub


    Public Sub dev_panel_change_key(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim obj_no = 0
        For Each field_object In tp_dev_detail.Controls
            obj_no += 1
            If field_object.Name = sender.name.ToString.Split("_").GetValue(0) Then
                Exit For
            End If
        Next

        If dev_form_field_list(23, (obj_no) * 3 - 2) = 0 Then
            dev_form_field_list(23, (obj_no) * 3 - 2) = 1
            sender.image = New Bitmap(My.Resources.key_on)
        Else
            dev_form_field_list(23, (obj_no) * 3 - 2) = 0
            sender.image = New Bitmap(My.Resources.key_off)

        End If
    End Sub


    Public Sub dev_panel_save_to_db(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim obj_no = 0
        For Each field_object In tp_dev_detail.Controls
            obj_no += 1
            If field_object.Name = sender.name.ToString.Split("_").GetValue(0) Then
                Exit For
            End If
        Next
        If dev_form_field_list(25, (obj_no) * 3 - 1) = False Then
            dev_form_field_list(25, (obj_no) * 3 - 1) = True
            sender.image = New Bitmap(My.Resources.db_commit_on)
        Else
            dev_form_field_list(25, (obj_no) * 3 - 1) = False
            sender.image = New Bitmap(My.Resources.db_commit_off)
        End If
    End Sub


    Public Sub dev_panel_editclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        fn_cursor_waiting(True)
        temp_integer = Convert.ToInt64(sender.name.ToString.Replace("_edit", ""))

        Dim obj_no = 0
        For Each field_object In tp_dev_detail.Controls
            obj_no += 1
            If field_object.Name = Convert.ToInt64(sender.name.ToString.Replace("_edit", "")) Then
                Exit For
            End If
        Next
        temp_integer = obj_no 'for dev input form  array pointer
        frm_dev_input.Show()
        Me.Enabled = False
        fn_cursor_waiting(False)
    End Sub


    Public Sub dev_panel_deleteclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        fn_cursor_waiting(True)
        fn_delete_dev_form(True, Convert.ToInt64(sender.name.ToString.Replace("_delete", "")), True)
        menu_change_reaction(sender, e)
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_dev_form_clean_Click(sender As Object, e As EventArgs)
        fn_cursor_waiting(True)
        fn_delete_dev_form(True, 0, True)
        fn_cursor_waiting(False)
    End Sub


    Private Sub txt_dev_note_TextChanged(sender As Object, e As EventArgs) Handles txt_dev_note.TextChanged

        Me.btn_main_btn_1.Enabled = False
        Me.btn_main_btn_2.Enabled = True
        btn_main_create_copy.Enabled = False
    End Sub

    Private Sub btn_dev_sql_clear_Click(sender As Object, e As EventArgs) Handles btn_dev_sql_clear.Click
        Dim result = MsgBox(fn_translate("delete_sql_command?"), MsgBoxStyle.YesNo, fn_translate("delete_sql_command"))
        If result = vbYes Then
            txt_dev_sql_command.Text = ""
        End If
    End Sub

    Private Sub btn_dev_after_sql_clear_Click(sender As Object, e As EventArgs) Handles btn_dev_after_sql_clear.Click
        Dim result = MsgBox(fn_translate("delete_sql_command?"), MsgBoxStyle.YesNo, fn_translate("delete_sql_command"))
        If result = vbYes Then
            txt_dev_after_sql_command.Text = ""
        End If
    End Sub


    Private Sub btn_add_bind_form_Click(sender As Object, e As EventArgs) Handles btn_add_bind_form.Click
        Me.Enabled = False
        frm_form_list.Show()
    End Sub


    Private Sub btn_del_bind_form_Click(sender As Object, e As EventArgs) Handles btn_del_bind_form.Click
        If Not dev_lv_subform_list.SelectedItems Is Nothing Then
            Dim result = MsgBox(fn_translate("want_you_delete_full_subform_bind?") & " " & dev_lv_subform_list.SelectedItems.Item(0).Text, MsgBoxStyle.YesNo, fn_translate("delete_subform_bind"))
            If result = vbYes Then
                fn_delete_all_subform_binds()
                dev_lv_subform_list.SelectedItems.Item(dev_lv_subform_list.FocusedItem.Index).Remove()
            End If
        End If
    End Sub


    Private Sub devlv_subform_list_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dev_lv_subform_list.Click ', lv_subform_list.Click
        Dim subform_selected As Boolean = False
        If Not dev_lv_subform_list.SelectedItems Is Nothing Then
            If dev_lv_subform_list.SelectedItems.Count > 0 Then
                subform_selected = True
                btn_del_bind_form.Enabled = True
                fn_load_destination_datafield_for_subform_binding()
                fn_load_dev_subform_panel(dev_lv_subform_list.SelectedItems(0).Name.Replace("SQL", "").Replace("TERMINAL", ""))
            End If
        End If

        If subform_selected = False Then
            lbl_created_binds.Text = fn_translate("created_binds")
            btn_del_bind_form.Enabled = False
            dev_lb_destination_field_list.Items.Clear()
            dev_dgv_subform_bingings.Rows.Clear()
        End If

        fn_dev_subform_enable_main_buttons()

    End Sub

    Private Sub dev_lb_source_field_list_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dev_lb_source_field_list.SelectedIndexChanged
        fn_dev_subform_enable_main_buttons()
    End Sub


    Private Sub dev_lb_destination_field_list_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dev_lb_destination_field_list.SelectedIndexChanged
        fn_dev_subform_enable_main_buttons()
    End Sub



    Private Sub dev_nud_subform_position_ValueChanged(sender As Object, e As EventArgs) Handles dev_nud_subform_position.ValueChanged, dev_nud_subform_position.Validated
        fn_dev_subform_enable_main_buttons()
    End Sub


    Private Sub dev_txt_subform_bindname_TextChanged(sender As Object, e As EventArgs) Handles dev_txt_subform_bindname.TextChanged
        fn_dev_subform_enable_main_buttons()
    End Sub

    Private Sub chb_dev_form_released_CheckedChanged(sender As Object, e As EventArgs) Handles chb_dev_form_released.CheckedChanged
        If chb_dev_form_released.Checked Then
            chb_dev_allow_attachments.Enabled = True
        Else
            chb_dev_allow_attachments.Enabled = False
            chb_dev_allow_attachments.Checked = False
        End If
    End Sub

    'END OF DEV FORM BUILDER









    'START of dynamic EVENTS

    Private Sub TabControl_DrawOnTabPage(sender As Object, e As DrawItemEventArgs) Handles tc_data.DrawItem
        drawRootTabpages(e, tc_data.TabPages.Item(e.Index).Enabled)
    End Sub

    Private Sub t_system_time_Tick_1(sender As Object, e As EventArgs) Handles t_system_time.Tick
        Me.Text = "SWAPP Builder Ver: 1.15"
        If username <> Nothing Then Me.lbl_time_panel.Text = fn_translate("today_is") & ":" & CStr(Date.Now)
    End Sub

    'SUB dataview Area
    Friend Shared Sub sub_dgv_CellDoubleClick(sender As DataGridView, e As DataGridViewCellEventArgs)
        Dim field_type As String
        Try
            If sender.CurrentRow.Index >= 0 And sender.CurrentCell.Selected Then
                'insert values from selected subform row
                For Each cell As DataGridViewCell In sender.Rows.Item(sender.CurrentRow.Index).Cells
                    For Each ctrl As Control In sender.Parent.Controls.OfType(Of Panel)()
                        Try
                            For Each SubCtrl As Control In ctrl.Controls
                                field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                                Select Case field_type
                                    Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"
                                        If SubCtrl.Name.ToString.Replace("_field", "") = cell.ColumnIndex + 1 Then
                                            SubCtrl.Text = cell.Value
                                        End If
                                End Select
                            Next
                        Catch ex As Exception

                        End Try
                    Next
                Next
                Main_Form.btn_main_btn_1.Enabled = False
                Main_Form.btn_main_btn_2.Enabled = True
                Main_Form.btn_main_btn_3.Enabled = True
                Main_Form.btn_main_create_copy.Enabled = True
            End If
        Catch ex As Exception
            Main_Form.btn_main_btn_1.Enabled = True
            Main_Form.btn_main_btn_2.Enabled = False
            Main_Form.btn_main_btn_3.Enabled = False
            Main_Form.btn_main_create_copy.Enabled = False
        End Try

    End Sub

    Friend Shared Sub sub_dgv_Copy(sender As DataGridView, e As DataGridViewCellEventArgs)
        Dim prev_ctrl As Control = Nothing
        Dim field_type As String
        Dim sub_form = sender.Parent.TabIndex - 1
        Dim idIndex As Long = 0
        Dim datagridview As New DataGridView
        Try

            If sender.CurrentRow.Index >= 0 And sender.CurrentCell.Selected Then
                'insert values from selected subform row
                For Each ctrl As Control In sender.Parent.Controls
                    Try
                        If ctrl.GetType.ToString.Replace("System.Windows.Forms.", "") = "DataGridView" Then
                            datagridview = ctrl
                            idIndex = fn_get_next_gdv_index(datagridview)
                        End If
                    Catch ex As Exception
                    End Try
                Next

                For Each cell As DataGridViewCell In sender.Rows.Item(sender.CurrentRow.Index).Cells
                    For Each ctrl As Control In sender.Parent.Controls.OfType(Of Panel)()
                        Try
                            For Each SubCtrl As Control In ctrl.Controls
                                field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                                Select Case field_type
                                    Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"
                                        For col = 0 To fn_getsubformarray(sub_form).Length / 29
                                            If SubCtrl.Tag = True Then
                                                If (CBool(fn_search_substitution("sub[user_dataview_translate]")) And fn_sys_translate(prev_ctrl.Text) = subBindingField(sub_form) Or (Not CBool(fn_search_substitution("sub[user_dataview_translate]")) And prev_ctrl.Text = subBindingField(sub_form))) And Not subBindingValue(sub_form) Is Nothing Then
                                                    SubCtrl.Text = subBindingValue(sub_form).ToString
                                                ElseIf (CBool(fn_search_substitution("sub[user_dataview_translate]")) And fn_sys_translate(prev_ctrl.Text) = subBindingField(sub_form) Or (Not CBool(fn_search_substitution("sub[user_dataview_translate]")) And prev_ctrl.Text = subBindingField(sub_form))) And subBindingValue(sub_form) Is Nothing Then
                                                    SubCtrl.Text = "MainRecId"
                                                ElseIf SubCtrl.Name.ToString.Replace("_field", "") = cell.ColumnIndex + 1 Then
                                                    SubCtrl.Text = cell.Value
                                                End If
                                            Else 'subprimary_key
                                                SubCtrl.Text = idIndex
                                            End If
                                        Next
                                End Select
                                prev_ctrl = SubCtrl
                            Next
                        Catch ex As Exception
                        End Try
                    Next
                Next
                Main_Form.btn_main_btn_1.Enabled = True
                Main_Form.btn_main_btn_2.Enabled = False
                Main_Form.btn_main_btn_3.Enabled = False
                Main_Form.btn_main_create_copy.Enabled = False
            End If
        Catch ex As Exception
            Main_Form.btn_main_btn_1.Enabled = True
            Main_Form.btn_main_btn_2.Enabled = False
            Main_Form.btn_main_btn_3.Enabled = False
            Main_Form.btn_main_create_copy.Enabled = False
        End Try

    End Sub

    Friend Shared Sub sub_dgv_CellClick(sender As DataGridView, e As DataGridViewCellEventArgs)
        Dim prev_ctrl As Control = Nothing
        Dim field_type As String
        Dim sub_form = sender.Parent.TabIndex - 1
        Dim idIndex As Long = 0
        Dim datagridview As New DataGridView
        Try

            If Not sender.CurrentCell.Selected Then
                ' set subform to default on unselect row
                For Each ctrl As Control In sender.Parent.Controls
                    Try
                        If ctrl.GetType.ToString.Replace("System.Windows.Forms.", "") = "DataGridView" Then
                            datagridview = ctrl
                            idIndex = fn_get_next_gdv_index(datagridview)
                        End If

                        For Each SubCtrl As Control In ctrl.Controls
                            field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                            Select Case field_type
                                Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"
                                    For col = 0 To fn_getsubformarray(sub_form).Length / 29
                                        If SubCtrl.Tag = True Then
                                            If (CBool(fn_search_substitution("sub[user_dataview_translate]")) And fn_sys_translate(prev_ctrl.Text) = subBindingField(sub_form) Or (Not CBool(fn_search_substitution("sub[user_dataview_translate]")) And prev_ctrl.Text = subBindingField(sub_form))) And Not subBindingValue(sub_form) Is Nothing Then
                                                SubCtrl.Text = subBindingValue(sub_form).ToString
                                            ElseIf (CBool(fn_search_substitution("sub[user_dataview_translate]")) And fn_sys_translate(prev_ctrl.Text) = subBindingField(sub_form) Or (Not CBool(fn_search_substitution("sub[user_dataview_translate]")) And prev_ctrl.Text = subBindingField(sub_form))) And subBindingValue(sub_form) Is Nothing Then
                                                SubCtrl.Text = "MainRecId"
                                            ElseIf fn_getsubformarray(sub_form)(1, col) = SubCtrl.Name Then
                                                SubCtrl.Text = fn_getsubformarray(sub_form)(18, col)
                                            End If
                                        Else 'subprimary_key
                                            SubCtrl.Text = idIndex
                                        End If
                                    Next
                            End Select
                            prev_ctrl = SubCtrl
                        Next
                    Catch ex As Exception

                    End Try
                Next
                Main_Form.btn_main_btn_1.Enabled = True
                Main_Form.btn_main_btn_2.Enabled = False
                Main_Form.btn_main_btn_3.Enabled = False
                Main_Form.btn_main_create_copy.Enabled = False
            ElseIf sender.CurrentRow.Index >= 0 And sender.CurrentCell.Selected Then
                Main_Form.btn_main_create_copy.Enabled = True
                Main_Form.btn_main_btn_3.Enabled = True
            End If
        Catch ex As Exception
            Main_Form.btn_main_btn_1.Enabled = True
            Main_Form.btn_main_btn_2.Enabled = False
            Main_Form.btn_main_btn_3.Enabled = False
            Main_Form.btn_main_create_copy.Enabled = False
        End Try
    End Sub

    Friend Shared Sub react_isdigit(sender As Object, e As KeyPressEventArgs)
        fn_numner_keys(e)
    End Sub

    Public Sub react_openfiledialog(sender As Object, e As EventArgs)
        Dim res As String
        res = ofd_open_file.ShowDialog()
        If res = vbOK Then
            For Each Ctrl In tp_dev_detail.Controls.OfType(Of Panel)()
                For Each SubCtrl In Ctrl.Controls.OfType(Of TextBox)()
                    If SubCtrl.Name = sender.name.replace("_selbtn", "_file") Then
                        SubCtrl.Text = ofd_open_file.FileName
                        SubCtrl.Enabled = True
                    End If
                Next
            Next
        End If
    End Sub


    Public Sub react_open_picture_preview(sender As Object, e As EventArgs)
        For Each Ctrl In tc_data.TabPages.Item(tc_data.SelectedIndex).Controls.OfType(Of Panel)()
            For Each SubCtrl In Ctrl.Controls.OfType(Of PictureBox)()
                If SubCtrl.Name = sender.name Then
                    Me.Enabled = False
                    frm_picture_preview.Show()
                    frm_picture_preview.pb_image_preview.Image = SubCtrl.Image
                End If
            Next
        Next
    End Sub

    Public Sub react_openuserfileheaderdialog(sender As Object, e As EventArgs)
        Dim res As String
        res = ofd_open_file.ShowDialog()
        If res = vbOK Then
            For Each Ctrl As Control In tc_user_document.TabPages.Item(sender.Parent.Parent.TabIndex).Controls
                If Ctrl.GetType.ToString.Replace("System.Windows.Forms.", "") = "Panel" Then
                    For Each SubCtrl In Ctrl.Controls.OfType(Of TextBox)()
                        If SubCtrl.Name = sender.name.replace("_selbtn", "_file") Then
                            SubCtrl.Text = ofd_open_file.FileName
                            SubCtrl.Enabled = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Public Sub react_openfiledialog_for_picture(sender As Object, e As EventArgs)
        Dim res As String
        ofd_open_file.Filter = "JPG|*.jpg|PNG|*.png|Bitmap|*.bmp|GIFs|*.gif"
        res = ofd_open_file.ShowDialog()
        If res = vbOK Then
            For Each Ctrl In tc_data.TabPages.Item(tc_data.SelectedIndex).Controls.OfType(Of Panel)()
                For Each SubCtrl In Ctrl.Controls.OfType(Of PictureBox)()
                    If SubCtrl.Name = sender.name.replace("_ofd", "") Then
                        SubCtrl.ImageLocation = ofd_open_file.FileName
                        SubCtrl.Enabled = True
                    End If
                Next
            Next
        End If

    End Sub

    Public Sub dgw_user_sort_reaction_from_toostrip(ByVal sender As Object, ByVal e As EventArgs) Handles tstb_sort_rec.Click
        If IsNumeric(sender.name) Then fn_user_order_by_set(sender.name)
        If sender.name = "removeAll" Then fn_reset_sort_mainForm()
        fn_cursor_waiting(False)
    End Sub


    Public Sub dgw_user_sort_reaction(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles dgw_query_view.ColumnHeaderMouseDoubleClick
        If dgw_query_view.Columns.Item(e.ColumnIndex).SortMode <> DataGridViewColumnSortMode.NotSortable Then
            fn_user_order_by_set(e.ColumnIndex)
            fn_cursor_waiting(False)
        End If
    End Sub

    'END of dynamic EVENTS









    'START of main button reaction  INSERT / EDIT /DELETE / COPY

    'INSERT BUTTON
    Private Sub btn_main_btn_1_Click(sender As Object, e As EventArgs) Handles btn_main_btn_1.Click
        fn_cursor_waiting(True)
        Try
            If tc_data.SelectedTab.Name = "tp_datalist" Then 'dataview go to insert user form
                setTabPageAllowed(tc_data.TabPages.Item(1), True)
                actual_db_task = db_task_list(1)
                fn_load_user_form_definition()

                fn_fill_detail_form_with_empty_rec()

                tc_user_document.SelectedIndex = 0
                tc_data.SelectTab(1)

            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And actual_db_task = db_task_list(1) And tc_user_document.SelectedIndex = 0 Then 'insert new user record
                fn_insert_new_user_rec()
            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And tc_user_document.SelectedIndex > 0 Then
                'save to sublocaldatagridview
                fn_insUpd_new_user_subrec(tc_user_document.SelectedIndex, False)


            ElseIf tc_data.SelectedTab.Name = "tp_dev_builder" Then 'devbuilder
                If Me.tc_dev_menu.SelectedIndex = 0 Or Me.tc_dev_menu.SelectedIndex = 1 Then 'INSERT DEV dataview/note SELECT 
                    Dim result = MsgBox(fn_translate("save_dataview_item?") & " " & Me.txt_dev_form_name.Text, MsgBoxStyle.YesNo, fn_translate("save_dataview_item"))
                    If result = vbYes Then
                        If Me.lbl_dev_form_id.Text.Length = 0 Then
                            fn_sql_request("INSERT INTO dbo.form_list (parent_menu_id,position,form_type,basic_sql,form_name,note,user_help,[enabled],creator,released,export_enabled,import_enabled,local_db,table_name,enable_translate,attachments_allowed,basic_after_sql,local_after_db)VALUES('" & tv_dev_menu.SelectedNode.Name & "'," & nud_def_form_position.Value.ToString & ",'" & lb_dev_form_type.SelectedItem.ToString & "','" & txt_dev_sql_command.Text.Replace("'", "''") & "','" & txt_dev_form_name.Text.Replace("'", "") & "','" & txt_dev_note.Text.Replace("'", "") & "','" & rtb_user_help.Text.Replace("'", "") & "'," & CInt(Int(chb_dev_form_enabled.Checked)).ToString() & ",'" & username & "'," & CInt(Int(chb_dev_form_released.Checked)).ToString() & "," & CInt(Int(chb_dev_export_enabled.Checked)).ToString() & "," & CInt(Int(chb_dev_import_enabled.Checked)).ToString() & "," & CInt(Int(chb_def_localdb.Checked)).ToString() & ",'" & txt_dev_full_save_table_name.Text & "'," & CInt(Int(chb_dev_form_enable_translate.Checked)).ToString() & "," & CInt(Int(chb_dev_allow_attachments.Checked)).ToString() & ",'" & txt_dev_after_sql_command.Text.Replace("'", "''") & "'," & CInt(Int(chb_def_after_localdb.Checked)).ToString() & ")", "INSERT", "LOCAL", False, True, sql_parameter, False, False)
                        End If
                        fn_load_menu("")
                        fn_dev_clean_form()
                    End If

                ElseIf Me.tc_dev_menu.SelectedIndex = 2 Then 'INSERT DEV USER FORM 
                    Dim result = MsgBox(fn_translate("save_form_item?") & " " & Me.tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("save_form_item"))
                    If result = vbYes Then
                        fn_change_upd_dev_detail_form_array()
                        fn_delete_dev_form(False, 0, False)
                        menu_change_reaction(sender, e)
                    End If
                ElseIf Me.tc_dev_menu.SelectedIndex = 3 Then 'INSERT DEV USER SUBFORM BINDS
                    Dim result = MsgBox(fn_translate("save_subform_item?") & " " & Me.dev_txt_subform_bindname.Text, MsgBoxStyle.YesNo, fn_translate("save_subform_item"))
                    If result = vbYes Then
                        fn_save_subform_bind()
                    End If
                End If
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
        End Try
        fn_cursor_waiting(False)
    End Sub

    'SAVE
    Private Sub btn_main_btn_2_Click(sender As Object, e As EventArgs) Handles btn_main_btn_2.Click
        fn_cursor_waiting(True)
        Try
            If tc_data.SelectedTab.Name = "tp_datalist" Then 'dataview prepare user rec for update
                setTabPageAllowed(tc_data.TabPages.Item(1), True)
                actual_db_task = db_task_list(2)

                tc_user_document.SelectedIndex = 0
                tc_data.SelectTab(1)
                fn_clear_user_form()
                fn_fill_detail_form_with_selected_rec(False)

            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And actual_db_task = db_task_list(2) And tc_user_document.SelectedIndex = 0 Then 'update existing record
                If fn_update_selected_user_rec() Then actual_db_task = db_task_list(0)

            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And tc_user_document.SelectedIndex > 0 Then
                'update sublocaldatagridview
                fn_insUpd_new_user_subrec(tc_user_document.SelectedIndex, True)

            ElseIf tc_data.SelectedTab.Name = "tp_dev_builder" Then 'devbuilder
                If Me.tc_dev_menu.SelectedIndex = 0 OrElse Me.tc_dev_menu.SelectedIndex = 1 Then 'UPDATE DEV dataview/note SELECT 
                    Dim result = MsgBox(fn_translate("save_dataview_item?") & " " & tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("save_dataview_item"))
                    If result = vbYes Then
                        If Me.lbl_dev_form_id.Text.Length <> 0 Then
                            fn_sql_request("UPDATE dbo.form_list SET position = " & nud_def_form_position.Value.ToString & ",form_type = '" & lb_dev_form_type.SelectedItem.ToString & "',basic_sql = '" & txt_dev_sql_command.Text.Replace("'", "''") & "',form_name='" & txt_dev_form_name.Text.Replace("'", "") & "',note='" & Me.txt_dev_note.Text.Replace("'", "") & "',user_help='" & Me.rtb_user_help.Text.Replace("'", "") & "',[enabled]=" & CInt(Int(chb_dev_form_enabled.Checked)).ToString() & ",creator='" & username & "',released=" & CInt(Int(chb_dev_form_released.Checked)).ToString() & ",export_enabled=" & CInt(Int(chb_dev_export_enabled.Checked)).ToString() & ",import_enabled=" & CInt(Int(chb_dev_import_enabled.Checked)).ToString() & ",local_db=" & CInt(Int(chb_def_localdb.Checked)).ToString() & ",table_name='" & txt_dev_full_save_table_name.Text & "',enable_translate=" & CInt(Int(chb_dev_form_enable_translate.Checked)).ToString() & ",attachments_allowed=" & CInt(Int(chb_dev_allow_attachments.Checked)).ToString() & ",basic_after_sql = '" & txt_dev_after_sql_command.Text.Replace("'", "''") & "',local_after_db=" & CInt(Int(chb_def_after_localdb.Checked)).ToString() & " WHERE id=" & Me.lbl_dev_form_id.Text & " ", "UPDATE", "local", False, True, sql_parameter, False, False)

                            Me.lbl_dev_form_id.Text = ""
                        End If
                        'fn_load_menu()
                        fn_dev_clean_form()
                        fn_load_form_definition(False)
                        btn_show_preview_Click(sender, e)
                        fn_load_dev_form_definition(True)
                        menu_change_reaction(sender, e)
                    End If

                ElseIf Me.tc_dev_menu.SelectedIndex = 2 Then 'UPDATE  DEV USER FORM 
                    Dim result = MsgBox(fn_translate("save_form_item?") & " " & Me.tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("save_form_item"))
                    If result = vbYes Then
                        fn_change_upd_dev_detail_form_array()
                        fn_delete_dev_form(False, 0, False)
                        menu_change_reaction(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
        End Try
        fn_cursor_waiting(False)
    End Sub



    'DELETE BUTTON
    Private Sub btn_main_btn_3_Click(sender As Object, e As EventArgs) Handles btn_main_btn_3.Click
        fn_cursor_waiting(True)
        Try
            If tc_data.SelectedTab.Name = "tp_datalist" Then 'user datalist delete selected record
                Dim column_list As String() = primary_key_columns.Split(",")
                Dim key_info As String = ""
                Dim attachment_correction = 1
                If Me.dgw_query_view.Columns(0).Name = "sys_Attachment" Then attachment_correction = 0

                For i = 0 To column_list.Count - 2
                    key_info &= Me.dgw_query_view.Rows(Me.dgw_query_view.CurrentCell.RowIndex).Cells((CInt(column_list(i))) - attachment_correction).Value().ToString & ", "
                Next

                Dim result = MsgBox(fn_translate("delete_record?") & " " & key_info, MsgBoxStyle.YesNo, fn_translate("delete_record"))
                If result = vbYes Then
                    fn_fill_detail_form_with_selected_rec(False)
                    If fn_db_operations_with_user_subrecs(True) Then
                        If fn_sql_request("DELETE FROM " & fn_search_substitution("sub[user_dataview_table]") & " WHERE " & fn_prepare_user_where_command() & " ", "DELETE", fn_search_substitution("sub[user_dataview_db_type]"), False, True, sql_parameter, False, False) Then
                            btn_user_refresh_Click(sender, e)
                            tc_data.SelectTab(0)
                            fn_load_basic_form("")
                            btn_main_btn_1.Enabled = True
                        End If
                    End If
                End If
            ElseIf tc_data.SelectedTab.Name = "tp_user_document" And tc_user_document.SelectedIndex > 0 Then
                'delete from sublocaldatagridview
                fn_delete_from_user_subrec_datagrid(tc_user_document.SelectedIndex, True)

            ElseIf tc_data.SelectedTab.Name = "tp_dev_builder" Then 'devbuilder

                If Me.tc_dev_menu.SelectedIndex = 0 Or Me.tc_dev_menu.SelectedIndex = 1 Then 'delete dataview/note SELECT 
                    Dim result = MsgBox(fn_translate("delete_dataview_item?") & " " & tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("delete_dataview_item"))
                    If result = vbYes Then

                        If Me.lbl_dev_form_id.Text.Length > 0 Then
                            fn_sql_request("DELETE FROM dbo.form_list WHERE id=" & Me.lbl_dev_form_id.Text & " ", "DELETE", "LOCAL", False, True, sql_parameter, False, False)
                        End If
                        fn_load_menu("")
                        fn_dev_clean_form()
                    End If
                ElseIf Me.tc_dev_menu.SelectedIndex = 2 Then 'DELETE DEV FORM
                    fn_delete_dev_form(True, 0, True)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            fn_cursor_waiting(False)
        End Try
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_main_create_copy_Click(sender As Object, e As EventArgs) Handles btn_main_create_copy.Click

        If tc_data.SelectedIndex = 0 Then 'copy mainForm
            setTabPageAllowed(tc_data.TabPages.Item(1), True)
            tc_user_document.SelectedIndex = 0
            tc_data.SelectTab(1)
            tc_user_document.SelectTab(0)
            fn_clear_user_form() 'after change tab index
            actual_db_task = db_task_list(1)
            fn_fill_detail_form_with_selected_rec(True)

        ElseIf tc_data.SelectedIndex = 1 And tc_user_document.SelectedIndex > 0 Then 'copy SubForms
            Dim ec As DataGridViewCellEventArgs
            Dim dataGridView As DataGridView

            For Each ctrl In tc_user_document.SelectedTab.Controls.OfType(Of DataGridView)
                dataGridView = ctrl
            Next
            sub_dgv_Copy(dataGridView, ec)

        End If

        menu_change_reaction(sender, e)
    End Sub



    'END of main button reaction




    Function fn_dev_subform_enable_main_buttons()
        Dim subform_selected As Boolean = False
        If Not Me.dev_lv_subform_list.SelectedItems Is Nothing Then
            If Me.dev_lv_subform_list.SelectedItems.Count > 0 And Me.dev_txt_subform_bindname.Text.Length > 0 And Not Me.dev_lb_source_field_list.SelectedItem Is Nothing And Not Me.dev_lb_destination_field_list.SelectedItem Is Nothing And Me.dev_nud_subform_position.Value > 0 Then
                Me.btn_main_btn_1.Enabled = True
                Me.btn_main_btn_2.Enabled = False
                Me.btn_main_btn_3.Enabled = False
            Else
                Me.btn_main_btn_1.Enabled = False
                Me.btn_main_btn_2.Enabled = False
                Me.btn_main_btn_3.Enabled = False
            End If
        End If


    End Function

    Private Sub dgv_dev_subform_bingings_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dev_dgv_subform_bingings.CellContentClick
        If e.ColumnIndex = 6 Then
            Dim result = MsgBox(fn_translate("want_you_delete_subform_bind?") & " " & dev_dgv_subform_bingings.Rows(e.RowIndex).Cells(0).Value & " " & dev_lv_subform_list.SelectedItems.Item(0).Text, MsgBoxStyle.YesNo, fn_translate("delete_bind"))
            Try
                If result = vbYes Then
                    fn_cursor_waiting(True)
                    fn_delete_subform_bind(CInt(dev_dgv_subform_bingings.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.Tag))
                    If Not dev_lv_subform_list.SelectedItems Is Nothing Then
                        If dev_lv_subform_list.SelectedItems.Count > 0 Then
                            fn_load_existed_subforms(dev_lv_subform_list.SelectedItems.Item(0).Text)
                            fn_load_destination_datafield_for_subform_binding()
                            fn_load_dev_subform_panel(dev_lv_subform_list.SelectedItems(0).Name.Replace("SQL", "").Replace("TERMINAL", ""))
                        End If
                    End If
                End If
                fn_cursor_waiting(False)
            Catch ex As Exception
                fn_cursor_waiting(False)
            End Try
        End If
    End Sub

End Class


