Option Explicit On

Imports System.Data.OleDb
Imports SWAPP_Builder.Main_Form
Imports System.Security.AccessControl


Module functions_system

    'draw positioning
    Private menu_position, button_position As Integer

    'array to file
    'IO.File.WriteAllLines(IO.Path.Combine(Application.StartupPath, "test.txt"), Enumerable.Range(0, Main_Form.dev_form_field_list.GetLength(0)).Select(Function(i1) String.Join(",", Enumerable.Range(0, Main_Form.dev_form_field_list.GetLength(1)).Select(Function(i2) Main_Form.dev_form_field_list(i1, i2)).ToArray)).ToArray)


    Function drawRootTabpages(ByVal e As DrawItemEventArgs, ByVal allowed As Boolean) As Boolean
        Try
            If allowed Then
                e.Graphics.FillRectangle(New SolidBrush(Color.Transparent), e.Bounds)
                Dim paddedBounds As Rectangle = e.Bounds
                paddedBounds.Inflate(-2, -2)
                e.Graphics.DrawString(Main_Form.tc_data.TabPages.Item(e.Index).Text, fn_CreateFont("Microsoft Sans Serif", 10, 3, False, False, False, False), SystemBrushes.WindowText, paddedBounds)
            Else
                e.Graphics.FillRectangle(New SolidBrush(Color.Transparent), e.Bounds)
                Dim paddedBounds As Rectangle = e.Bounds
                paddedBounds.Inflate(-2, -2)
                e.Graphics.DrawString(Main_Form.tc_data.TabPages.Item(e.Index).Text, fn_CreateFont("Microsoft Sans Serif", 10, 3, False, False, False, False), SystemBrushes.InactiveCaption, paddedBounds)
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function


    Function setTabPageAllowed(ByVal tabPage As TabPage, ByVal allowed As Boolean) As Boolean
        If allowed Then
            tabPage.Enabled = True
        Else
            tabPage.Enabled = False
            If Main_Form.app_loaded Then
                tabPage.Select()
                'Main_Form.tc_data.SelectedTab = tabPage
            End If
        End If

        Main_Form.tc_data.SelectedIndex = 0
        Return True
    End Function

    Function fn_ImageToByte(ByVal img As Image) As Byte()
        Try
            Dim imgStream As IO.MemoryStream = New IO.MemoryStream()
            img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Jpeg) 'System.Drawing.Imaging.ImageFormat.Png
            imgStream.Close()
            Dim byteArray As Byte() = imgStream.ToArray()
            imgStream.Dispose()
            Return byteArray
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Function fn_getsubformarray(ByVal index) As Array
        Select Case index
            Case 0
                fn_getsubformarray = Main_Form.user_subform_field_list0
            Case 1
                fn_getsubformarray = Main_Form.user_subform_field_list1
            Case 2
                fn_getsubformarray = Main_Form.user_subform_field_list2
            Case 3
                fn_getsubformarray = Main_Form.user_subform_field_list3
            Case 4
                fn_getsubformarray = Main_Form.user_subform_field_list4
            Case 5
                fn_getsubformarray = Main_Form.user_subform_field_list5
            Case 6
                fn_getsubformarray = Main_Form.user_subform_field_list6
            Case 7
                fn_getsubformarray = Main_Form.user_subform_field_list7
            Case 8
                fn_getsubformarray = Main_Form.user_subform_field_list8
            Case 9
                fn_getsubformarray = Main_Form.user_subform_field_list9
            Case Else
        End Select
    End Function


    Function fn_ByteArrayToStr(ByVal dBytes As Byte()) As String
        Dim str As String
        Dim enc As New System.Text.ASCIIEncoding()
        str = enc.GetString(dBytes)

        Return str
    End Function


    Function fn_create_directory(ByVal directory As String)

        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Function


    Function fn_delete_directory(ByVal directory As String)
        If System.IO.Directory.Exists(directory) Then System.IO.Directory.Delete(directory, True)
    End Function

    Function fn_check_directory(ByVal directory As String) As Boolean
        fn_check_directory = System.IO.Directory.Exists(directory)
    End Function

    Function fn_copy_dgv_to_local(ByRef datagridview, ByVal replace_columnName, ByVal newValue)
        Dim copyDatagridView As New DataGridView
        Dim copyRows = New List(Of DataGridViewRow)
        Dim columnIndexReplaced As Integer = -1

        For Each column As DataGridViewColumn In datagridview.Columns
            If Not replace_columnName Is Nothing And column.Name = replace_columnName Then columnIndexReplaced = column.Index
            copyDatagridView.Columns.Add(column.Clone())
        Next

        For Each sourceRow As DataGridViewRow In datagridview.Rows
            Dim targetRow = sourceRow.Clone()
            For Each cell As DataGridViewCell In sourceRow.Cells
                If replace_columnName Is Nothing Or cell.ColumnIndex <> columnIndexReplaced Then
                    targetRow.Cells(cell.ColumnIndex).Value = cell.Value
                ElseIf cell.ColumnIndex = columnIndexReplaced Then
                    targetRow.Cells(cell.ColumnIndex).Value = newValue
                End If
            Next
            copyRows.Add(targetRow)
        Next

        copyDatagridView.Rows.AddRange(copyRows.ToArray())
        datagridview.DataSource = Nothing
        For Each column As DataGridViewColumn In copyDatagridView.Columns
            If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                datagridview.Columns.Add(column.Name, fn_translate(column.HeaderText))
            Else
                datagridview.Columns.Add(column.Name, column.HeaderText)
            End If
        Next

        For i = 0 To (copyDatagridView.Rows.Count - 2)
            datagridview.Rows.Add(copyDatagridView.Rows(i).Cells.Cast(Of DataGridViewCell).Select(Function(c) c.Value).ToArray)
        Next
        datagridview.CurrentCell.Selected = 0
        datagridview.ClearSelection()
    End Function


    Function fn_get_next_gdv_index(ByVal datagridview As DataGridView) As Integer
        Dim idIndex As Long = 0
        For Each row As DataGridViewRow In datagridview.Rows
            If row.Cells.Item(datagridview.Columns("id").Index).Value > idIndex Then idIndex = row.Cells.Item(datagridview.Columns("id").Index).Value
            If row.Cells.Item(datagridview.Columns("Id").Index).Value > idIndex Then idIndex = row.Cells.Item(datagridview.Columns("Id").Index).Value
            If row.Cells.Item(datagridview.Columns("ID").Index).Value > idIndex Then idIndex = row.Cells.Item(datagridview.Columns("Id").Index).Value
        Next
        idIndex += 1
        Return (idIndex)
    End Function

    Function fn_check_file(ByVal file As String) As Boolean
        fn_check_file = System.IO.File.Exists(file)
    End Function


    Function fn_create_file(ByVal file As String) As Boolean
        If Not System.IO.File.Exists(file) Then
            System.IO.File.Create(file).Close()
        End If

        If fn_check_file(file) Then
            fn_create_file = True
        Else
            fn_create_file = False
        End If
    End Function


    Function fn_delete_file(ByVal file As String) As Boolean
        System.IO.File.Delete(file)

        If Not fn_check_file(file) Then
            fn_delete_file = True
        Else
            fn_delete_file = False
        End If
    End Function




    Function fn_numner_keys(ByVal e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = vbBack AndAlso Not e.KeyChar = Chr(27) AndAlso Not e.KeyChar = Chr(13) AndAlso Not e.KeyChar = Chr(8) AndAlso Not e.KeyChar = Chr(44) Then
            e.KeyChar = Nothing
        End If
    End Function


    Function fn_load_user_setting() As Boolean
        'fn_load_default_settings()
        fn_load_user_setting = False
        fn_sql_request("SELECT configuration FROM [dbo].[users] WHERE user_name = '" & fn_search_substitution("sub[user_name]") & "' AND enabled = 1 AND released = 1", "SELECT", "local", False, True, sql_parameter, False, False)
        Try
            Dim record = My.Forms.Main_Form.sql_array(0, 0)
            record = record.Trim()
            If record.Length > 0 Then

                Dim temp As String() = Split(record, "#")

                My.MySettings.Default.Item("external_sql_connection") = "Data Source=" & temp(0).ToString & ";Persist Security Info=True;User ID=" & temp(1).ToString & ";Password=" & temp(2).ToString

                My.Forms.Main_Form.txt_global_settings_mssql_server.Text = temp(0).ToString
                My.Forms.Main_Form.txt_global_settings_mssql_name.Text = temp(1).ToString
                My.Forms.Main_Form.txt_global_settings_mssql_password.Text = temp(2).ToString
                My.Forms.Main_Form.cb_global_settings_default_keyboard.SelectedItem = temp(3).ToString
                If temp(4).ToString = "True" Then My.Forms.Main_Form.chb_global_settings_must_field_message.Checked = True
                If temp(5).ToString = "True" Then My.Forms.Main_Form.chb_global_settings_clean_form_god_sql.Checked = True
                If temp(6).ToString = "True" Then My.Forms.Main_Form.chb_global_settings_select_first_row.Checked = True
                If temp(7).ToString = "True" Then My.Forms.Main_Form.chb_global_settings_clean_form_bad_sql.Checked = True
                If temp(8).ToString = "True" Then My.Forms.Main_Form.chb_global_setting_extensible_rows.Checked = True
                If temp(9).ToString = "True" Then My.Forms.Main_Form.chb_golbal_setting_dgview_multiselect.Checked = True
                If temp(10).Length = 0 Then
                    My.Forms.Main_Form.txt_font_size.Text = 10
                Else
                    My.Forms.Main_Form.txt_font_size.Text = temp(10).ToString
                End If
                If temp(10).Length = 0 Then
                    My.Forms.Main_Form.txt_font_size.Text = 10
                Else
                    My.Forms.Main_Form.txt_font_size.Text = temp(10).ToString
                End If
                My.Forms.Main_Form.lb_global_settings_default_language.SelectedItem = temp(11).ToString


                temp_integer = 0
                For Each temp_string In Main_Form.default_settings
                    If temp_string.ToString.Contains("default_waiting_picture") Then


                        Select Case Main_Form.default_settings((temp_integer / 2), 1).ToString
                            Case "1"
                                Main_Form.pb_waiting_1.BorderStyle = BorderStyle.Fixed3D
                            Case "2"
                                Main_Form.pb_waiting_2.BorderStyle = BorderStyle.Fixed3D
                            Case "3"
                                Main_Form.pb_waiting_3.BorderStyle = BorderStyle.Fixed3D
                            Case "4"
                                Main_Form.pb_waiting_4.BorderStyle = BorderStyle.Fixed3D
                            Case "5"
                                Main_Form.pb_waiting_5.BorderStyle = BorderStyle.Fixed3D
                            Case Else
                                Main_Form.pb_waiting_1.BorderStyle = BorderStyle.Fixed3D
                        End Select

                        Exit For
                    End If
                    temp_integer += 1
                Next

                fn_load_user_setting = True
            End If
        Catch ex As Exception
            MessageBox.Show(fn_translate("configuration_fail"))
        End Try
    End Function


    Function fn_cursor_waiting(ByVal run As Boolean)
        If run Then
            Application.UseWaitCursor = True
            Main_Form.Cursor = Cursors.WaitCursor
        Else
            Application.UseWaitCursor = False
            Main_Form.Cursor = Cursors.Default
        End If
    End Function


    Function fn_cursor_change(ByVal run As Boolean, ByVal start As Boolean)
        Application.UseWaitCursor = True
        Main_Form.Cursor = Cursors.WaitCursor
        If run Then
            If start Then
                Main_Form.tpsb_run_loading.Enabled = True
                Main_Form.tpsb_run_loading.Value = 0
                bar_process_start = False
            End If

            Main_Form.tpsb_run_loading.Enabled = True
            Main_Form.tpsb_run_loading.Value += 10

        Else
            Main_Form.tpsb_run_loading.Enabled = False
            Main_Form.tpsb_run_loading.Value = 100
        End If
        'Application.UseWaitCursor = False
        'Main_Form.Cursor = Cursors.Default

    End Function


    Function fn_byteArrayToImage(ByVal byt As Byte()) As Image

        Dim ms As New System.IO.MemoryStream()
        Dim drwimg As Image = Nothing

        Try
            ms.Write(byt, 0, byt.Length)
            drwimg = New Bitmap(ms)
        Finally
            ms.Close()
        End Try

        Return drwimg

    End Function


    Function fn_save_setting() As Boolean
        fn_save_setting = False
        Try

            Dim sql_cmd As String
            Dim checkbox_status As String = "False"
            If Main_Form.chb_global_settings_must_field_message.Checked Then checkbox_status = "True"
            If Main_Form.chb_global_settings_clean_form_god_sql.Checked Then
                checkbox_status &= "#" & "True"
            Else
                checkbox_status &= "#" & "False"
            End If
            If Main_Form.chb_global_settings_select_first_row.Checked Then
                checkbox_status &= "#" & "True"
            Else
                checkbox_status &= "#" & "False"
            End If
            If Main_Form.chb_global_settings_clean_form_bad_sql.Checked Then
                checkbox_status &= "#" & "True"
            Else
                checkbox_status &= "#" & "False"
            End If
            If Main_Form.chb_global_setting_extensible_rows.Checked Then
                checkbox_status &= "#" & "True"
            Else
                checkbox_status &= "#" & "False"
            End If
            If Main_Form.chb_golbal_setting_dgview_multiselect.Checked Then
                checkbox_status &= "#" & "True"
            Else
                checkbox_status &= "#" & "False"
            End If

            sql_cmd = Main_Form.txt_global_settings_mssql_server.Text & "#" & Main_Form.txt_global_settings_mssql_name.Text & "#" & Main_Form.txt_global_settings_mssql_password.Text & "#" & Main_Form.cb_global_settings_default_keyboard.SelectedItem & "#" & checkbox_status.ToString & "#" & Main_Form.txt_font_size.Text & "#" & Main_Form.lb_global_settings_default_language.SelectedItem.ToString

            fn_sql_request("UPDATE [dbo].[users] SET configuration = '" & sql_cmd & "' WHERE user_name = '" & fn_search_substitution("sub[user_name]") & "' ", "UPDATE", "local", False, True, sql_parameter, False, False)
            fn_sql_request("UPDATE [dbo].[app_setting] SET configuration = '" & Main_Form.lb_global_settings_default_language.SelectedItem.ToString & "' WHERE name = 'default_language' AND enabled = 1 AND released = 1 ", "UPDATE", "local", False, True, sql_parameter, False, False)
            fn_sql_request("UPDATE [dbo].[app_setting] SET configuration = '" & Main_Form.selected_picture.ToString & "' WHERE name = 'default_waiting_picture' AND enabled = 1 AND released = 1 ", "UPDATE", "local", False, True, sql_parameter, False, False)


            fn_save_setting = True
            fn_reload_app()
        Catch ex As Exception
            MessageBox.Show(fn_translate("operation_error"))
        End Try
    End Function


    Function fn_file_detect_encoding(ByVal FileName As String) As System.Text.Encoding
        Dim enc As String = ""
        If System.IO.File.Exists(FileName) Then
            Dim filein As New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
            If (filein.CanSeek) Then
                Dim bom(4) As Byte
                filein.Read(bom, 0, 4)
                'EF BB BF       = utf-8
                'FF FE          = ucs-2le, ucs-4le, and ucs-16le
                'FE FF          = utf-16 and ucs-2
                '00 00 FE FF    = ucs-4
                If (((bom(0) = &HEF) AndAlso (bom(1) = &HBB) AndAlso (bom(2) = &HBF)) OrElse
                    ((bom(0) = &HFF) AndAlso (bom(1) = &HFE)) OrElse
                    ((bom(0) = &HFE) AndAlso (bom(1) = &HFF)) OrElse
                    ((bom(0) = &H0) AndAlso (bom(1) = &H0) AndAlso (bom(2) = &HFE) AndAlso (bom(3) = &HFF))) Then
                    enc = "Unicode"
                Else
                    enc = "ASCII"
                End If
                'Position the file cursor back to the start of the file
                filein.Seek(0, System.IO.SeekOrigin.Begin)
                ' Do more stuff
            End If
            filein.Close()
        End If
        If enc = "Unicode" Then
            Return System.Text.Encoding.UTF8
        Else
            Return System.Text.Encoding.Default
        End If
    End Function



    Function fn_reload_app()
        Dim result = MsgBox(fn_translate("restart_app?"), MsgBoxStyle.YesNo)
        If result = vbYes Then
            Application.Restart()
        End If
    End Function


    Function fn_load_menu(ByVal search_str As String) As Boolean
        Main_Form.tv_menu.Nodes.Clear()
        Main_Form.tv_dev_menu.Nodes.Clear()


        Dim level As Integer = 0

        'load menu tree
        While fn_sql_request("SELECT TOP 1 id FROM [dbo].[menu_list] WHERE level =" + level.ToString, "SELECT", "local", False, True, sql_parameter, False, False) = True
            fn_sql_request("SELECT ml.id,ml.menu_name,ml.parent_menu_id,ml.position,ml.[enabled],ml.[released],ml.[enable_translate] FROM [dbo].[menu_list] ml WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR ml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.[right] = ml.right_menu)) AND ml.level =" + level.ToString + " ORDER BY ml.level,ml.position ASC", "SELECT", "local", False, True, sql_parameter, False, False)
            For i = 0 To My.Forms.Main_Form.sql_array_count - 1

                If My.Forms.Main_Form.sql_array(i, 2).Length = 0 Then
                    If My.Forms.Main_Form.sql_array(i, 4) = True AndAlso My.Forms.Main_Form.sql_array(i, 5) = True Then
                        If My.Forms.Main_Form.sql_array(i, 6) Then
                            My.Forms.Main_Form.tv_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                        Else
                            My.Forms.Main_Form.tv_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(My.Forms.Main_Form.sql_array(i, 1)))
                        End If
                    End If
                    If Main_Form.system_account = True Then
                        If My.Forms.Main_Form.sql_array(i, 6) Then
                            My.Forms.Main_Form.tv_dev_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), My.Forms.Main_Form.sql_array(i, 3) + "_" + UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                        Else
                            My.Forms.Main_Form.tv_dev_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), My.Forms.Main_Form.sql_array(i, 3) + "_" + UCase(My.Forms.Main_Form.sql_array(i, 1)))
                        End If
                    End If

                Else
                    If My.Forms.Main_Form.sql_array(i, 4) = True AndAlso My.Forms.Main_Form.sql_array(i, 5) = True Then
                        If My.Forms.Main_Form.sql_array(i, 6) Then
                            My.Forms.Main_Form.tv_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                        Else
                            My.Forms.Main_Form.tv_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(My.Forms.Main_Form.sql_array(i, 1)))
                        End If
                    End If
                    If Main_Form.system_account = True Then
                        If My.Forms.Main_Form.sql_array(i, 6) Then
                            My.Forms.Main_Form.tv_dev_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), My.Forms.Main_Form.sql_array(i, 3) + "_" + UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                        Else
                            My.Forms.Main_Form.tv_dev_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), My.Forms.Main_Form.sql_array(i, 3) + "_" + UCase(My.Forms.Main_Form.sql_array(i, 1)))
                        End If
                    End If
                End If
            Next

            level += 1
        End While

        'load forms
        For Each node As TreeNode In My.Forms.Main_Form.tv_menu.Nodes
            If fn_sql_request("SELECT id,form_name,form_type,position,[enabled],[released],[enable_translate] FROM [dbo].[form_list] WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR right_read IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND parent_menu_id = " + node.Name.Replace("SQL", "").ToString + " ORDER BY position ASC", "SELECT", "local", False, True, sql_parameter, False, False) = True Then
                For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                    If My.Forms.Main_Form.sql_array(i, 4) = True AndAlso My.Forms.Main_Form.sql_array(i, 5) = True Then
                        If search_str.Length > 0 Then
                            If UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))).Contains(UCase(search_str)) Then
                                If My.Forms.Main_Form.sql_array(i, 6) Then
                                    My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                Else
                                    My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                End If

                            End If
                        Else
                            If My.Forms.Main_Form.sql_array(i, 6) Then
                                My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                            Else
                                My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                            End If
                        End If
                    End If

                    If Main_Form.system_account Then
                        My.Forms.Main_Form.tv_dev_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 3) & "_" & fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                        My.Forms.Main_Form.tv_dev_menu.Nodes(node.Name).Nodes.Item(My.Forms.Main_Form.tv_dev_menu.Nodes(node.Name).Nodes.Count - 1).BackColor = Color.LightGreen
                    End If
                Next
            End If
            For Each subnode As TreeNode In node.Nodes
                If fn_sql_request("SELECT id,form_name,form_type,position,[enabled],[released],[enable_translate] FROM [dbo].[form_list] WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR right_read IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.enabled=1 AND ur.released = 1)) AND parent_menu_id = " + subnode.Name.Replace("SQL", "").ToString + " ORDER BY position ASC", "SELECT", "local", False, True, sql_parameter, False, False) = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                        If My.Forms.Main_Form.sql_array(i, 4) = True AndAlso My.Forms.Main_Form.sql_array(i, 5) = True Then

                            If search_str.Length > 0 Then
                                If UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))).Contains(UCase(search_str)) Then
                                    If My.Forms.Main_Form.sql_array(i, 6) Then
                                        My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                    Else
                                        My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                    End If
                                End If
                            Else
                                If My.Forms.Main_Form.sql_array(i, 6) Then
                                    My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                Else
                                    My.Forms.Main_Form.tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) & My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                End If

                            End If

                        End If

                        If Main_Form.system_account Then
                            My.Forms.Main_Form.tv_dev_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 3) & "_" & fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                            My.Forms.Main_Form.tv_dev_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Item(My.Forms.Main_Form.tv_dev_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Count - 1).BackColor = Color.LightGreen
                        End If
                    Next
                End If
            Next
        Next

        'load favorites menu
        fn_reload_favorite_menu(False)


        Main_Form.btn_dev_menu_delete.Enabled = False
        Main_Form.btn_add_menu.Enabled = False
        Main_Form.btn_add_under_menu.Enabled = False
    End Function





    Public Function fn_set_keyboard(ByVal sel_keyboard As String)
        Select Case sel_keyboard

            Case "LANG_CZECH"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_CZECH, 0)
            Case "LANG_ENGLISH"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_ENGLISH, 0)
            Case "LANG_FRENCH"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_FRENCH, 0)
            Case "LANG_GERMAN"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_GERMAN, 0)
            Case "LANG_ITALIAN"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_ITALIAN, 0)
            Case "LANG_NORWEGIAN"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_NORWEGIAN, 0)
            Case "LANG_PORTUGUESE"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_PORTUGUESE, 0)
            Case "LANG_RUSSIAN"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_RUSSIAN, 0)
            Case "LANG_SPANISH"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_SPANISH, 0)
            Case "LANG_UKRAINE"
                Call Main_Form.ActivateKeyboardLayout(Main_Form.LANG_UKRAINE, 0)
            Case Else
        End Select
    End Function


    Function fn_detect_encoding(ByVal FileName As String) As System.Text.Encoding


        Dim enc As String = ""
        If System.IO.File.Exists(FileName) Then
            Dim filein As New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
            If (filein.CanSeek) Then
                Dim bom(4) As Byte
                filein.Read(bom, 0, 4)
                'EF BB BF       = utf-8
                'FF FE          = ucs-2le, ucs-4le, and ucs-16le
                'FE FF          = utf-16 and ucs-2
                '00 00 FE FF    = ucs-4
                If (((bom(0) = &HEF) AndAlso (bom(1) = &HBB) AndAlso (bom(2) = &HBF)) OrElse
                    ((bom(0) = &HFF) AndAlso (bom(1) = &HFE)) OrElse
                    ((bom(0) = &HFE) AndAlso (bom(1) = &HFF)) OrElse
                    ((bom(0) = &H0) AndAlso (bom(1) = &H0) AndAlso (bom(2) = &HFE) AndAlso (bom(3) = &HFF))) Then
                    enc = "Unicode"
                Else
                    enc = "ASCII"
                End If
                'Position the file cursor back to the start of the file
                filein.Seek(0, System.IO.SeekOrigin.Begin)
                ' Do more stuff
            End If
            filein.Close()
        End If
        If enc = "Unicode" Then
            Return System.Text.Encoding.UTF8
        Else
            Return System.Text.Encoding.Default
        End If
    End Function



    Function fn_load_dev_form_type_list() As Boolean
        fn_load_dev_form_type_list = False
        Main_Form.lb_dev_form_type.Items.Clear()
        If fn_sql_request("SELECT form_type_name FROM dbo.form_type WHERE enabled=1 AND released=1  ", "SELECT", "LOCAL", False, True, sql_parameter, False, False) = True Then
            For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                Main_Form.lb_dev_form_type.Items.Add(My.Forms.Main_Form.sql_array(i, 0))
            Next
            Main_Form.lb_dev_form_type.SelectedIndex = 0
            fn_load_dev_form_type_list = True
        End If
    End Function


    Function fn_load_form_definition(ByVal with_reset_form As Boolean) As Boolean
        fn_load_form_definition = False
        If with_reset_form Then Main_Form.lbl_dev_form_id.ResetText()
        Main_Form.nud_def_form_position.Value = 10
        Main_Form.lb_dev_form_type.SelectedItem = 1
        Main_Form.txt_dev_sql_command.Text = ""
        Main_Form.txt_dev_form_name.Text = ""
        Main_Form.txt_dev_note.Text = ""
        Main_Form.txt_dev_full_save_table_name.Text = ""
        Main_Form.chb_dev_form_enabled.Checked = False
        Main_Form.chb_dev_form_released.Checked = False
        Main_Form.chb_dev_form_enable_translate.Checked = False
        Main_Form.chb_dev_allow_attachments.Checked = False

        Try
            If Main_Form.tv_dev_menu.SelectedNode.Name.Contains("SQL") OrElse Main_Form.tv_dev_menu.SelectedNode.Name.Contains("TERMINAL") Then
                If fn_sql_request("SELECT id,position,form_type,basic_sql,form_name,note,enabled,released,export_enabled,import_enabled,local_db,table_name,enable_translate,user_help,attachments_allowed,basic_after_sql,local_after_db FROM [dbo].[form_list] WHERE id = " & Main_Form.tv_dev_menu.SelectedNode.Name.Replace("SQL", "").Replace("TERMINAL", "") & " ", "SELECT", "local", False, True, sql_parameter, False, False) Then
                    Main_Form.lbl_dev_form_id.Text = My.Forms.Main_Form.sql_array(0, 0)
                    Main_Form.nud_def_form_position.Value = My.Forms.Main_Form.sql_array(0, 1)
                    Main_Form.lb_dev_form_type.SelectedItem = My.Forms.Main_Form.sql_array(0, 2)
                    Main_Form.txt_dev_sql_command.Text = My.Forms.Main_Form.sql_array(0, 3)
                    Main_Form.txt_dev_form_name.Text = My.Forms.Main_Form.sql_array(0, 4)
                    Main_Form.txt_dev_note.Text = My.Forms.Main_Form.sql_array(0, 5)
                    Main_Form.chb_dev_form_enabled.Checked = My.Forms.Main_Form.sql_array(0, 6)
                    Main_Form.chb_dev_form_released.Checked = My.Forms.Main_Form.sql_array(0, 7)
                    Main_Form.chb_dev_export_enabled.Checked = My.Forms.Main_Form.sql_array(0, 8)
                    Main_Form.chb_dev_import_enabled.Checked = My.Forms.Main_Form.sql_array(0, 9)
                    Main_Form.chb_def_localdb.Checked = My.Forms.Main_Form.sql_array(0, 10)
                    Main_Form.txt_dev_full_save_table_name.Text = My.Forms.Main_Form.sql_array(0, 11)
                    Main_Form.chb_dev_form_enable_translate.Checked = My.Forms.Main_Form.sql_array(0, 12)
                    Main_Form.rtb_user_help.Text = My.Forms.Main_Form.sql_array(0, 13)
                    Main_Form.chb_dev_allow_attachments.Checked = My.Forms.Main_Form.sql_array(0, 14)
                    Main_Form.txt_dev_sql_command.Enabled = True
                    Main_Form.btn_dev_sql_clear.Enabled = True
                    Main_Form.btn_dev_show_preview.Enabled = True
                    Main_Form.txt_dev_after_sql_command.Text = My.Forms.Main_Form.sql_array(0, 15)
                    Main_Form.txt_dev_after_sql_command.Enabled = True
                    Main_Form.btn_dev_after_sql_clear.Enabled = True
                    Main_Form.chb_def_after_localdb.Checked = My.Forms.Main_Form.sql_array(0, 16)
                End If
            End If

            fn_load_form_definition = True
        Catch ex As Exception
            Main_Form.txt_dev_sql_command.Enabled = False
            Main_Form.txt_dev_after_sql_command.Enabled = False
            Main_Form.btn_dev_sql_clear.Enabled = False
            Main_Form.btn_dev_after_sql_clear.Enabled = False
            Main_Form.btn_dev_show_preview.Enabled = False
            Main_Form.btn_main_btn_1.Enabled = False
            Main_Form.btn_main_btn_2.Enabled = False
            Main_Form.btn_main_create_copy.Enabled = False
            Main_Form.btn_dev_create_update_form.Enabled = False
            MessageBox.Show(fn_translate("definition_load_error"))
        End Try
    End Function




    Public Function fn_CreateFont(ByVal fontName As String, _
                        ByVal fontSize As Integer, _
                        ByVal fontunit As Integer, _
                        ByVal isBold As Boolean, _
                        ByVal isStrikeout As Boolean, _
                        ByVal isUnderline As Boolean, _
                        ByVal isItalic As Boolean) As Drawing.Font

        Dim styles As FontStyle = FontStyle.Regular

        If (isBold) Then
            styles = styles Or FontStyle.Bold
        End If

        If (isItalic) Then
            styles = styles Or FontStyle.Italic
        End If

        If (isStrikeout) Then
            styles = styles Or FontStyle.Strikeout
        End If

        If (isUnderline) Then
            styles = styles Or FontStyle.Underline
        End If

        Dim unit As GraphicsUnit
        Select Case fontunit
            Case 1 '"Display" Or "1"
                unit = GraphicsUnit.Display
            Case 5 '"DisDocumentplay" Or "5"
                unit = GraphicsUnit.Document
            Case 4 '"Inch" Or "4"
                unit = GraphicsUnit.Inch
            Case 6 '"Millimeter" Or "6"
                unit = GraphicsUnit.Millimeter
            Case 2 '"Pixel" Or "2"
                unit = GraphicsUnit.Pixel
            Case 3 '"Point" Or "3"
                unit = GraphicsUnit.Point
            Case 0 '"World" Or "0"
                unit = GraphicsUnit.World
        End Select

        Dim newFont As New Drawing.Font(fontName, fontSize, styles, unit)
        Return newFont

    End Function


    'Dim startInfo As New ProcessStartInfo()
    'startInfo.FileName = IO.Path.Combine(Application.StartupPath, "fyiviewer", "RdlReader.exe")
    'startInfo.WindowStyle = ProcessWindowStyle.Maximized
    'startInfo.Arguments = """" + tv_print_menu.SelectedNode.Name.Remove(0, (tv_print_menu.SelectedNode.Name.Split("_")(0).Length + 1)) + """ -p ""rec_id=8"""
    'startInfo.UseShellExecute = True
    'Dim result As Process
    'result = Process.Start(startInfo)
    'While Not result.HasExited
    'End While
    'result.Close()

End Module
