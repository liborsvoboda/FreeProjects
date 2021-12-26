Imports System.Data
Imports SWAPP_Builder.Classes

Module Functions_user





    Function fn_load_basic_form(ByVal id As String) As Boolean
        Dim filter_status As String
        Dim export_enabled, import_enabled As Boolean

        Main_Form.dgw_query_view.ClearSelection()
        Main_Form.dgw_query_view.CurrentCell = Nothing

        Main_Form.dgw_query_view.Columns.Clear()
        Main_Form.dgw_query_view.Refresh()
        Main_Form.lbl_record_count_loaded_no.Text = 0

        setTabPageAllowed(Main_Form.tc_data.TabPages.Item(1), False)

        If id.Length > 0 Then
            fn_insert_substitution("sub[user_dataview_db_type]", "DB")

            If fn_sql_request("SELECT [basic_sql],[local_db],[table_name],[enable_translate],[export_enabled],[import_enabled],[id],[user_help],[attachments_allowed],[basic_after_sql],[local_after_db] FROM [dbo].[form_list] WHERE id=" & id.Replace("SQL", "") & " ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) Then

                Main_Form.rtb_user_help_view.Text = Main_Form.sql_array(0, 7)
                If CBool(Main_Form.sql_array(0, 1)) Then fn_insert_substitution("sub[user_dataview_db_type]", "local")

                user_data_command = Main_Form.sql_array(0, 0)
                attachments_allowed = Main_Form.sql_array(0, 8)
                fn_insert_substitution("sub[user_dataview_table]", Main_Form.sql_array(0, 2))
                fn_insert_substitution("sub[user_dataview_translate]", Main_Form.sql_array(0, 3))

                fn_insert_substitution("sub[after_sql_command]", Main_Form.sql_array(0, 9))
                fn_insert_substitution("sub[after_sql_local]", Main_Form.sql_array(0, 10))

                'user_translate = Main_Form.sql_array(0, 3)
                export_enabled = Main_Form.sql_array(0, 4)
                import_enabled = Main_Form.sql_array(0, 5)
                fn_insert_substitution("sub[form_id]", Main_Form.sql_array(0, 6))
                user_order_by = ""

                If fn_sql_request(Main_Form.sql_array(0, 0), "SELECT", fn_search_substitution("sub[user_dataview_db_type]"), False, False, Main_Form.sql_parameter, True, True) Then
                    My.Forms.Main_Form.dgw_query_view.AutoGenerateColumns = True
                    If Main_Form.chb_global_setting_extensible_rows.Checked Then
                        My.Forms.Main_Form.dgw_query_view.AllowUserToResizeRows = True
                    Else
                        My.Forms.Main_Form.dgw_query_view.AllowUserToResizeRows = False
                    End If

                    If My.Forms.Main_Form.chb_format_debbuger.Checked Then
                        Main_Form.dgw_query_view.DataSource = dgw_table_schema
                        Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Black
                        Main_Form.lbl_record_count_loaded_no.Text = 0
                    Else
                        Main_Form.dgw_query_view.DataSource = dgw_source

                        'If attachments_allowed Then
                        '    Dim attachment = New DataGridViewImageColumn() With {.Width = 70, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None, .Name = "sys_Attachment", .HeaderText = "sys_Attachment", .Resizable = False, .[ReadOnly] = True, .Image = My.Resources.attachment, .ImageLayout = DataGridViewImageCellLayout.Zoom}
                        '    Main_Form.dgw_query_view.Columns.Insert(0, attachment)
                        'End If

                        Main_Form.lbl_record_count_loaded_no.Text = dgw_source.Count
                        If CInt(Main_Form.tstb_records_count.Text) = dgw_source.Count Then
                            Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Red
                        Else
                            Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Black
                        End If
                    End If

                    ' End If
                    fn_insert_substitution("sub[dataview_record_count]", Main_Form.lbl_record_count_loaded_no.Text)

                    'substitution datalist load actual values
                    If fn_search_substitution("sub[user_dataview_table]").Contains("substitution") Then
                        fn_substitution_dataview_filling()
                    End If

                    Main_Form.tstb_sort_rec.DropDownItems.Clear()

                    Dim added_column = 0
                    Dim innerItem As New ToolStripMenuItem
                    If Main_Form.dgw_query_view.Columns.Item(0).Name = "sys_Attachment" Then added_column = 1

                    For Each column As DataGridViewColumn In Main_Form.dgw_query_view.Columns  'sort marks for loaded definition
                        Try

                            If column.Name <> "sys_Attachment" Then
                                If Not dgw_table_schema.Rows(column.Index - added_column).Item(21) Then
                                    column.SortMode = DataGridViewColumnSortMode.Programmatic
                                Else
                                    column.SortMode = DataGridViewColumnSortMode.NotSortable
                                End If
                            Else
                                column.SortMode = DataGridViewColumnSortMode.NotSortable
                            End If

                            If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                column.HeaderText = fn_translate(column.Name)
                            End If

                            If column.Name <> "sys_Attachment" Then
                                If Not dgw_table_schema.Rows(column.Index - added_column).Item(21) Then
                                    If user_order_by.Contains("[" & column.Name & "] ASC") Then
                                        column.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                                        innerItem = New ToolStripMenuItem
                                        innerItem.Name = column.Index ' + added_column
                                        innerItem.Text = column.HeaderText & " DESC"
                                        AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip
                                        Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)

                                    ElseIf user_order_by.Contains("[" & column.Name & "] DESC") Then
                                        column.HeaderCell.SortGlyphDirection = SortOrder.Descending
                                        innerItem = New ToolStripMenuItem
                                        innerItem.Name = column.Index ' + added_column
                                        innerItem.Text = column.HeaderText & " DEL"
                                        AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip 'fn_user_order_by_set(column.index)
                                        Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                                    Else
                                        column.HeaderCell.SortGlyphDirection = SortOrder.None
                                        innerItem = New ToolStripMenuItem
                                        innerItem.Name = column.Index ' + added_column
                                        innerItem.Text = column.HeaderText & " ASC"
                                        AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip 'fn_user_order_by_set(column.index)
                                        Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                                    End If
                                End If
                            End If

                        Catch ex As Exception

                        End Try
                    Next
                    innerItem = New ToolStripMenuItem
                    innerItem.Name = "removeAll"
                    innerItem.Text = fn_translate("removeAll")
                    AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip 'reset sorting
                    Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                End If

                fn_load_filter_list()
                fn_reload_exp_imp_menu(export_enabled, import_enabled)
                fn_reload_report_menu(True, False)
                fn_reload_print_menu(True, False)

                fn_load_basic_form = True
            Else

                Main_Form.lbl_record_count_loaded_no.Text = Main_Form.sql_array_count
                If CInt(Main_Form.tstb_records_count.Text) = dgw_source.Count Then
                    Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Red
                Else
                    Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Black
                End If
            End If

        Else 'reload datalist with WHERE AND ORDER BY
            Dim sql_query As String

            If Main_Form.btn_filter_status.AccessibleDescription = "N" Then
                filter_status = ""
            ElseIf Main_Form.btn_filter_status.AccessibleDescription = "A" Then
                filter_status = " WHERE 1=1 " & user_where
            End If

            If user_order_by.Length > 0 Then
                sql_query = user_data_command & filter_status & " ORDER BY " & user_order_by
            Else
                sql_query = user_data_command & filter_status
            End If

            If fn_sql_request(sql_query, "SELECT", fn_search_substitution("sub[user_dataview_db_type]"), False, False, Main_Form.sql_parameter, True, False) Then
                Main_Form.dgw_query_view.AutoGenerateColumns = True
                If Main_Form.chb_global_setting_extensible_rows.Checked Then
                    Main_Form.dgw_query_view.AllowUserToResizeRows = True
                Else
                    Main_Form.dgw_query_view.AllowUserToResizeRows = False
                End If

                If Main_Form.chb_format_debbuger.Checked Then
                    Main_Form.dgw_query_view.DataSource = dgw_table_schema
                    Main_Form.tstb_records_count.ForeColor = Color.Black
                    Main_Form.lbl_record_count_loaded_no.Text = 0

                Else
                    Main_Form.dgw_query_view.DataSource = dgw_source

                    'If attachments_allowed Then
                    '    Dim attachment = New DataGridViewImageColumn() With {.Width = 70, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None, .Name = "sys_Attachment", .HeaderText = "sys_Attachment", .Resizable = False, .[ReadOnly] = True, .Image = My.Resources.attachment, .ImageLayout = DataGridViewImageCellLayout.Zoom}
                    '    Main_Form.dgw_query_view.Columns.Insert(0, attachment)
                    'End If

                    Main_Form.lbl_record_count_loaded_no.Text = dgw_source.Count
                    If CInt(Main_Form.tstb_records_count.Text) = dgw_source.Count Then
                        Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Red
                    Else
                        Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Black
                    End If

                End If

                fn_insert_substitution("sub[dataview_record_count]", Main_Form.lbl_record_count_loaded_no.Text)

                'substitution datalist load actual values
                If fn_search_substitution("sub[user_dataview_table]").Contains("substitution") Then
                    fn_substitution_dataview_filling()
                End If

                Dim innerItem As New ToolStripMenuItem
                Dim added_column = 0
                If Main_Form.dgw_query_view.Columns.Item(0).Name = "sys_Attachment" Then added_column = 1

                Main_Form.tstb_sort_rec.DropDownItems.Clear()
                For Each column As DataGridViewColumn In Main_Form.dgw_query_view.Columns  'sort mark for user change definition
                    Try
                        If column.Name <> "sys_Attachment" Then
                            If Not dgw_table_schema.Rows(column.Index - added_column).Item(21) Then
                                column.SortMode = DataGridViewColumnSortMode.Programmatic
                            Else
                                column.SortMode = DataGridViewColumnSortMode.NotSortable
                            End If
                        Else
                            column.SortMode = DataGridViewColumnSortMode.NotSortable
                        End If

                        If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                            column.HeaderText = fn_translate(column.Name)
                        End If

                        If column.Name <> "sys_Attachment" Then
                            If Not dgw_table_schema.Rows(column.Index - added_column).Item(21) Then
                                If user_order_by.Contains("[" & column.Name & "] ASC") Then
                                    column.HeaderCell.SortGlyphDirection = SortOrder.Ascending
                                    innerItem = New ToolStripMenuItem
                                    innerItem.Name = column.Index ' + added_column
                                    innerItem.Text = column.HeaderText & " DESC"
                                    AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip
                                    Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                                ElseIf user_order_by.Contains("[" & column.Name & "] DESC") Then
                                    column.HeaderCell.SortGlyphDirection = SortOrder.Descending
                                    innerItem = New ToolStripMenuItem
                                    innerItem.Name = column.Index ' + added_column
                                    innerItem.Text = column.HeaderText & " DEL"
                                    AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip 'fn_user_order_by_set(column.index)
                                    Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                                Else
                                    column.HeaderCell.SortGlyphDirection = SortOrder.None
                                    innerItem = New ToolStripMenuItem
                                    innerItem.Name = column.Index ' + added_column
                                    innerItem.Text = column.HeaderText & " ASC"
                                    AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip 'fn_user_order_by_set(column.index)
                                    Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                                End If
                            End If
                        End If

                    Catch ex As Exception

                    End Try
                Next
                innerItem = New ToolStripMenuItem
                innerItem.Name = "removeAll"
                innerItem.Text = fn_translate("removeAll")
                AddHandler innerItem.Click, AddressOf Main_Form.dgw_user_sort_reaction_from_toostrip 'reset sorting
                Main_Form.tstb_sort_rec.DropDownItems.Add(innerItem)
                fn_load_basic_form = True
            Else

                Main_Form.lbl_record_count_loaded_no.Text = Main_Form.sql_array_count
                If CInt(Main_Form.tstb_records_count.Text) = dgw_source.Count Then
                    Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Red
                Else
                    Main_Form.lbl_record_count_loaded_no.ForeColor = Color.Black
                End If
            End If


        End If

        If Main_Form.lbl_record_count_loaded_no.Text = 0 Then
            Main_Form.btn_filter_add.Enabled = False
        Else
            Main_Form.dgw_query_view.CurrentCell.Selected = False
            Main_Form.btn_filter_add.Enabled = True
        End If


    End Function


    Function fn_apply_filter_array_to_simple_form() As Boolean
        fn_apply_filter_array_to_simple_form = False

        Try 'implement existed filter to filter form
            If Main_Form.where_array(0, 0) IsNot Nothing Then
                For i = 0 To Main_Form.where_array.Length / Main_Form.where_array.GetLength(0) - 1
                    If Main_Form.where_array(4, i) IsNot Nothing Then

                        CType(frm_filter.Controls.Find(Replace(Main_Form.where_array(4, i), "_field", "_use").ToString(), True).FirstOrDefault(), CheckBox).Checked = True
                        CType(frm_filter.Controls.Find(Replace(Main_Form.where_array(4, i), "_field", "_cond").ToString, True).FirstOrDefault(), ComboBox).SelectedItem = Main_Form.where_array(1, i).ToString()
                        Select Case Main_Form.where_array(3, i)
                            Case "TextBox", "Guid"
                                CType(frm_filter.Controls.Find(Main_Form.where_array(4, i).ToString(), True).FirstOrDefault(), TextBox).Text = Main_Form.where_array(2, i).ToString()
                            Case "DateTimePicker"
                                CType(frm_filter.Controls.Find(Main_Form.where_array(4, i).ToString(), True).FirstOrDefault(), DateTimePicker).Value = Main_Form.where_array(2, i).ToString()
                            Case "CheckBox"
                                CType(frm_filter.Controls.Find(Main_Form.where_array(4, i).ToString(), True).FirstOrDefault(), CheckBox).Checked = Main_Form.where_array(2, i)
                        End Select
                    End If
                Next
            End If

            user_where = Main_Form.txt_filter_command.Text
            If Main_Form.where_array(0, 0) Is Nothing AndAlso user_where.Length > 0 Then
                MessageBox.Show(fn_translate("filter_is_SQL_type_only"))
                frm_filter.tsb_sql_window_Click("", System.EventArgs.Empty)
            End If

            fn_apply_filter_array_to_simple_form = True
        Catch ex As Exception
            user_where = Main_Form.txt_filter_command.Text
            ReDim Main_Form.where_array(5, 0)
            MessageBox.Show(fn_translate("filter_is_SQL_type_only"))
            frm_filter.tsb_sql_window_Click("", System.EventArgs.Empty)
        End Try

    End Function


    Function fn_load_filter_list() As Boolean
        Try
            Dim selected_filter = Nothing
            fn_cursor_waiting(True)
            If Main_Form.tv_filter_menu.SelectedNode IsNot Nothing Then selected_filter = Main_Form.tv_filter_menu.SelectedNode.Index

            fn_filter_menu_clear()


            If fn_sql_request("SELECT id,systemname,enable_translate FROM [dbo].[form_filter] WHERE [form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + " AND enabled=1 AND released=1 ORDER BY [position] ASC ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                    If Main_Form.sql_array(i, 2) Then
                        Main_Form.tv_filter_menu.Nodes.Add(Main_Form.sql_array(i, 0), fn_translate(Main_Form.sql_array(i, 1)))
                    Else
                        Main_Form.tv_filter_menu.Nodes.Add(Main_Form.sql_array(i, 0), Main_Form.sql_array(i, 1))
                    End If
                Next
            End If

            If Not selected_filter Is Nothing Then Main_Form.tv_filter_menu.SelectedNode = Main_Form.tv_filter_menu.Nodes.Item(selected_filter)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Function fn_filter_menu_clear()
        Main_Form.tv_filter_menu.Nodes.Clear()
        Main_Form.lbl_filtername_selected.Text = ""
        Main_Form.txt_filter_command.Text = ""
        user_where = ""
        Main_Form.btn_filter_status.BackgroundImage = My.Resources.not_filtered
        Main_Form.btn_filter_status.AccessibleDescription = "N"
        'Main_Form.btn_filter_add.Enabled = False
        Main_Form.btn_filter_clear.Enabled = False
    End Function


    Function fn_report_menu_clear()
        Main_Form.tv_report_menu.Nodes.Clear()
        Main_Form.btn_report_group_add.Enabled = False
        Main_Form.btn_report_del.Enabled = False
        Main_Form.btn_report_add.Enabled = False
    End Function


    Function fn_print_menu_clear()
        Main_Form.tv_print_menu.Nodes.Clear()
        Main_Form.btn_print_group_add.Enabled = False
        Main_Form.btn_print_add.Enabled = False
        Main_Form.btn_print_del.Enabled = False
    End Function


    Function fn_check_user_form_definition(ByVal id As Integer) As Boolean
        Try
            If fn_sql_request("SELECT id,position,form_type,basic_sql,form_name,note,enabled,released,export_enabled,import_enabled FROM [dbo].[form_list] WHERE id = " + id.ToString + " ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                Main_Form.lbl_user_form_id.Text = My.Forms.Main_Form.sql_array(0, 0)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(fn_translate("definition_load_error"))
            Return False
        End Try
    End Function

    Function fn_user_subform_field_array_from_sql(ByVal subFormTabIndex) As Boolean
        Select Case subFormTabIndex
            Case 0
                Try
                    ReDim Main_Form.user_subform_field_list0(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list0(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29
                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list0(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list0(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list0(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list0(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list0(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list0(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 1
                Try
                    ReDim Main_Form.user_subform_field_list1(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list1(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list1(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list1(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list1(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list1(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list1(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list1(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 2
                Try
                    ReDim Main_Form.user_subform_field_list2(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list2(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list2(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list2(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list2(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list2(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list2(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list2(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 3
                Try
                    ReDim Main_Form.user_subform_field_list3(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list3(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list3(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list3(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list3(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list3(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list3(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list3(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 4
                Try
                    ReDim Main_Form.user_subform_field_list4(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list4(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list4(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list4(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list4(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list4(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list4(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list4(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 5
                Try
                    ReDim Main_Form.user_subform_field_list5(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list5(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list5(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list5(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list5(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list5(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list5(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list5(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 6
                Try
                    ReDim Main_Form.user_subform_field_list6(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list6(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list6(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list6(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list6(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list6(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list6(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list6(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 7
                Try
                    ReDim Main_Form.user_subform_field_list7(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list7(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list7(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list7(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list7(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list7(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list7(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list7(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 8
                Try
                    ReDim Main_Form.user_subform_field_list8(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list8(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list8(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list8(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list8(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list8(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list8(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list8(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case 9
                Try
                    ReDim Main_Form.user_subform_field_list9(29, Main_Form.sql_array_count + 1)
                    For rec = 0 To Main_Form.sql_array_count
                        Main_Form.user_subform_field_list9(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                        For i = 0 To 29


                            If i >= 11 And i <= 16 Then
                                If Main_Form.sql_array(rec, i + 3) = "" Then
                                    Main_Form.user_subform_field_list9(i, rec + 1) = False
                                Else
                                    Main_Form.user_subform_field_list9(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                                End If
                            ElseIf i = 24 Then 'isreadonly 
                                Main_Form.user_subform_field_list9(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                            ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                                Main_Form.user_subform_field_list9(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                            ElseIf i = 26 Then 'data type
                                Main_Form.user_subform_field_list9(i, rec + 1) = Main_Form.sql_array(rec, 25)
                            Else
                                Main_Form.user_subform_field_list9(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                            End If
                        Next
                    Next
                    Return True
                Catch
                    Return False
                End Try
            Case Else
        End Select

    End Function


    Function fn_user_form_field_array_from_sql() As Boolean
        Try
            ReDim Main_Form.user_form_field_list(29, Main_Form.sql_array_count + 1)
            For rec = 0 To Main_Form.sql_array_count
                Main_Form.user_form_field_list(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
                For i = 0 To 29


                    If i >= 11 And i <= 16 Then
                        If Main_Form.sql_array(rec, i + 3) = "" Then
                            Main_Form.user_form_field_list(i, rec + 1) = False
                        Else
                            Main_Form.user_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                        End If
                    ElseIf i = 24 Then 'isreadonly 
                        Main_Form.user_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                    ElseIf i = 25 Or i = 27 Or i = 28 Or i = 29 Then 'savetodb,isdblong,allow_null,subsql_local,editable
                        Main_Form.user_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                    ElseIf i = 26 Then 'data type
                        Main_Form.user_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, 25)
                    Else
                        Main_Form.user_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                    End If
                Next
            Next
            Return True
        Catch
            Return False
        End Try
    End Function


    Function fn_remove_user_detail_form()
        While Main_Form.tc_user_document.TabPages.Count <> 0
            Main_Form.tc_user_document.TabPages.Item(0).Dispose()
        End While
    End Function


    Function fn_load_user_form_definition() As Boolean
        primary_key = False
        primary_key_columns = ""
        ReDim primary_subkey_columns(9)
        ReDim primary_subkey(9)
        ReDim subBindingField(9)
        ReDim subBindingValue(9)
        ReDim subBindingTableJoin(9)
        Dim idIndex As Long = 0
        Try

            Dim New_panel, panel_reaction As Panel
            Dim text_reaction As TextBox
            Dim combo_reaction As ComboBox
            Dim label_reaction As Label
            Dim picture_reaction As PictureBox
            Dim button_reaction As Button

            Dim New_Field
            Dim column_pos = 0, lenght = 0

            fn_remove_user_detail_form()

            If fn_sql_request("SELECT * FROM [dbo].[form_definition] WHERE [form_id]=" + Main_Form.lbl_user_form_id.Text + " ORDER BY input_no,value_no ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) Then
                Main_Form.tc_user_document.TabPages.Add("tp_user_document")
                Main_Form.tc_user_document.TabPages.Item(0).Name = "tp_user_document"
                Main_Form.tc_user_document.TabPages.Item(0).Text = fn_translate("header")
                Main_Form.tc_user_document.TabPages.Item(0).BackColor = Color.Transparent

                idIndex = fn_get_sql_index() 'fn_get_next_gdv_index(Main_Form.dgw_query_view)

                'ADD SUBFORM TABCONTROL
                fn_load_sql_addon("SELECT * FROM [dbo].[subform_binds] WHERE [mainform_id]=" + Main_Form.lbl_user_form_id.Text + " AND [enabled]=1 AND [released]=1 ORDER BY [position] ASC", True, "tc_user_document")
                If Main_Form.sql_array_addon_count > 0 Then

                    Dim subformsCount = Main_Form.sql_array_addon_count
                    For col = 0 To Main_Form.sql_array_addon_count - 1
                        Main_Form.tc_user_document.TabPages.Add("tp_user_subform:" + Main_Form.sql_array_addon(col, 0))
                        Main_Form.tc_user_document.TabPages.Item(col + 1).Name = "tp_user_subform:" + Main_Form.sql_array_addon(col, 0)
                        Main_Form.tc_user_document.TabPages.Item(col + 1).Text = fn_translate(Main_Form.sql_array_addon(col, 4))
                        Main_Form.tc_user_document.TabPages.Item(col + 1).BackColor = Color.Transparent
                        Main_Form.tc_user_document.TabPages.Item(col + 1).Tag = Main_Form.sql_array_addon(col, 5) & "|" & Main_Form.sql_array_addon(col, 6) & "/" & CBool(Main_Form.sql_array_addon(col, 12))
                    Next

                End If
                'END OF SUBFORM CONTROL ADDON

                For col = 0 To Main_Form.sql_array_count
                    If IsNumeric(Main_Form.sql_array(col, 2)) Then


                        Try 'separate inputs field
                            Select Case Main_Form.sql_array(col, 3)
                                Case 1
                                    'load label property
                                    If Main_Form.sql_array(col, 26) = 1 Then
                                        primary_key_columns += Main_Form.sql_array(col, 2) + ","
                                        primary_key = True
                                    End If


                                    lenght = 0
                                    'MessageBox.Show(col.ValueType.Name)
                                    New_panel = New Panel
                                    New_panel.TabIndex = Main_Form.sql_array(col, 2) * 10
                                    New_panel.Name = Main_Form.sql_array(col, 2)
                                    New_panel.Location = New Point(Main_Form.sql_array(col, 7), Main_Form.sql_array(col, 8))

                                    'new fields label
                                    New_Field = New Label
                                    New_Field.Name = Main_Form.sql_array(col, 4)
                                    If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                        New_Field.text = fn_translate(Main_Form.sql_array(col, 21))
                                    Else
                                        New_Field.text = Main_Form.sql_array(col, 21)
                                    End If
                                    New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                    New_Field.Location = New Point(lenght, 5)
                                    New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                    New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                    New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), CBool(Main_Form.sql_array(col, 14)), CBool(Main_Form.sql_array(col, 15)), CBool(Main_Form.sql_array(col, 16)), CBool(Main_Form.sql_array(col, 17)))
                                    New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                    New_Field.Cursor = Cursors.Default
                                    If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + Main_Form.sql_array(col, 5)
                                    label_reaction = DirectCast(New_Field, Label)
                                    New_panel.Controls.Add(New_Field)

                                Case 2  'load field property
                                    Select Case Main_Form.sql_array(col, 25)

                                        Case "int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"

                                            If Main_Form.sql_array(col - 1, 26) = 1 Then 'set primary index in mainform
                                                New_Field = New TextBox
                                                New_Field.Text = idIndex
                                                New_Field.enabled = False
                                            ElseIf Main_Form.sql_array(col, 22) = Nothing Then
                                                New_Field = New TextBox
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                text_reaction = DirectCast(New_Field, TextBox)
                                                AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit


                                                If Not fn_search_substitution(Main_Form.sql_array(col, 21)) Is Nothing Then
                                                    New_Field.text = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                Else
                                                    New_Field.text = Main_Form.sql_array(col, 21)
                                                End If
                                            Else
                                                If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then

                                                    If Main_Form.sql_array_addon_count = 1 Then
                                                        New_Field = New TextBox
                                                        New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                        New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                        text_reaction = DirectCast(New_Field, TextBox)
                                                        AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit

                                                        New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                    Else
                                                        Try
                                                            New_Field = New ComboBox
                                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                            combo_reaction = DirectCast(New_Field, ComboBox)
                                                            AddHandler combo_reaction.KeyPress, AddressOf Main_Form.react_isdigit

                                                            'insert custom value
                                                            If UCase(Main_Form.sql_array_addon(0, 2)) = "YES" Then
                                                                New_Field.DropDownStyle = ComboBoxStyle.DropDown
                                                            Else 'insert exist value only
                                                                New_Field.DropDownStyle = ComboBoxStyle.DropDownList
                                                            End If

                                                            'fill list values, set default value 0 - none, X-set
                                                            For each_rec = 0 To Main_Form.sql_array_addon_count - 1
                                                                'New_Field.Items.Insert(each_rec, Main_Form.sql_array_addon(each_rec, 0))
                                                                New_Field.Items.Add(New ComboBoxItem(Of String)(Main_Form.sql_array_addon(each_rec, 1), Main_Form.sql_array_addon(each_rec, 0)))
                                                                If ((Main_Form.sql_array_addon(each_rec, 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).ToString()) Or (Main_Form.sql_array_addon(each_rec, 3).Contains("#") AndAlso (each_rec + 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).Replace("#", ""))) Then
                                                                    New_Field.SelectedIndex = New_Field.items.count - 1
                                                                End If
                                                            Next
                                                            'New_panel.Controls.Add(New_Field)
                                                        Catch ex As Exception
                                                            MessageBox.Show(fn_translate("sql_command_addon_error") + ": SELECT XX,YY,'YES/NO' - Inserted Value,showned Value,Editable")
                                                        End Try
                                                    End If
                                                Else
                                                    New_Field = New TextBox
                                                    New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                    New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                    text_reaction = DirectCast(New_Field, TextBox)
                                                    AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit
                                                End If
                                            End If
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            New_panel.Controls.Add(New_Field)


                                        Case "char", "nchar", "ntext", "nvarchar", "text", "varchar"
                                            If Main_Form.sql_array(col, 22) = Nothing Then
                                                New_Field = New TextBox
                                                New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                text_reaction = DirectCast(New_Field, TextBox)

                                                If Not fn_search_substitution(Main_Form.sql_array(col, 21)) Is Nothing Then
                                                    New_Field.text = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                Else
                                                    New_Field.text = Main_Form.sql_array(col, 21)
                                                End If
                                            Else
                                                If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then

                                                    If Main_Form.sql_array_addon_count = 1 Then
                                                        New_Field = New TextBox
                                                        New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                        text_reaction = DirectCast(New_Field, TextBox)

                                                        New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                    Else
                                                        New_Field = New ComboBox
                                                        combo_reaction = DirectCast(New_Field, ComboBox)

                                                        'insert custom value
                                                        If UCase(Main_Form.sql_array_addon(0, 2)) = "YES" Then
                                                            New_Field.DropDownStyle = ComboBoxStyle.DropDown
                                                        Else 'insert exist value only
                                                            New_Field.DropDownStyle = ComboBoxStyle.DropDownList
                                                        End If

                                                        'fill list values, set default value 0 - none, X-set
                                                        For each_rec = 0 To Main_Form.sql_array_addon_count - 1
                                                            'New_Field.Items.Insert(each_rec, Main_Form.sql_array_addon(each_rec, 0))
                                                            New_Field.Items.Add(New ComboBoxItem(Of String)(Main_Form.sql_array_addon(each_rec, 1), Main_Form.sql_array_addon(each_rec, 0)))
                                                            If ((Main_Form.sql_array_addon(each_rec, 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).ToString()) Or (Main_Form.sql_array_addon(each_rec, 3).Contains("#") AndAlso (each_rec + 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).Replace("#", ""))) Then
                                                                New_Field.SelectedIndex = New_Field.items.count - 1
                                                            End If
                                                        Next
                                                    End If
                                                Else
                                                    New_Field = New TextBox
                                                    New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                    text_reaction = DirectCast(New_Field, TextBox)
                                                End If
                                            End If
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            New_panel.Controls.Add(New_Field)


                                        Case "uniqueidentifier"
                                            New_Field = New TextBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            New_Field.text = "" 'fn_translate("input_field")
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.enabled = False
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            text_reaction = DirectCast(New_Field, TextBox)
                                            New_panel.Controls.Add(New_Field)

                                        Case "bit"
                                            New_Field = New CheckBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            If Main_Form.sql_array(col, 22) = Nothing Then
                                                If Not fn_search_substitution(Main_Form.sql_array(col, 21)) Is Nothing Then
                                                    Boolean.TryParse(fn_search_substitution(Main_Form.sql_array(col, 21)), New_Field.checked)
                                                Else
                                                    Boolean.TryParse(Main_Form.sql_array(col, 21), New_Field.checked)
                                                End If
                                            Else
                                                'provést SQL
                                            End If

                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 5)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 25
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            New_panel.Controls.Add(New_Field)


                                        Case "time"
                                            New_Field = New DateTimePicker
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            If Main_Form.sql_array(col, 22) = Nothing Then
                                                If IsDate(fn_search_substitution(Main_Form.sql_array(col, 21))) Then
                                                    New_Field.value = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                ElseIf Main_Form.sql_array(col, 21).ToUpper = "NOW" Then
                                                    New_Field.value = DateTime.Now
                                                ElseIf IsDate(Main_Form.sql_array(col, 21)) Then
                                                    Date.TryParse(Main_Form.sql_array(col, 21), New_Field.value)
                                                End If
                                            Else
                                                If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then
                                                    If Main_Form.sql_array_addon_count = 1 Then
                                                        New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                    End If
                                                End If
                                            End If
                                            New_Field.Format = DateTimePickerFormat.Time
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                            If Main_Form.sql_array(col, 20).Length > 0 Then
                                                New_Field.format = DateTimePickerFormat.Custom
                                                New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                Main_Form.dgw_query_view.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                            End If
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            New_panel.Controls.Add(New_Field)

                                        Case "date"
                                            New_Field = New DateTimePicker
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            If Main_Form.sql_array(col, 22) = Nothing Then
                                                If IsDate(fn_search_substitution(Main_Form.sql_array(col, 21))) Then
                                                    New_Field.value = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                ElseIf Main_Form.sql_array(col, 21).ToUpper = "NOW" Then
                                                    New_Field.value = DateTime.Now
                                                ElseIf IsDate(Main_Form.sql_array(col, 21)) Then
                                                    Date.TryParse(Main_Form.sql_array(col, 21), New_Field.value)
                                                End If
                                            Else
                                                If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then
                                                    If Main_Form.sql_array_addon_count = 1 Then
                                                        New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                    End If
                                                End If
                                            End If
                                            New_Field.Format = DateTimePickerFormat.Short
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                            If Main_Form.sql_array(col, 20).Length > 0 Then
                                                New_Field.format = DateTimePickerFormat.Custom
                                                New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                Main_Form.dgw_query_view.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                            End If
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            New_panel.Controls.Add(New_Field)

                                        Case "datetime", "datetime2", "datetimeoffset", "timestamp"
                                            New_Field = New DateTimePicker
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            If Main_Form.sql_array(col, 22) = Nothing Then
                                                If IsDate(fn_search_substitution(Main_Form.sql_array(col, 21))) Then
                                                    New_Field.value = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                ElseIf Main_Form.sql_array(col, 21).ToUpper = "NOW" Then
                                                    New_Field.value = DateTime.Now
                                                ElseIf IsDate(Main_Form.sql_array(col, 21)) Then
                                                    Date.TryParse(Main_Form.sql_array(col, 21), New_Field.value)
                                                End If
                                            Else
                                                If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then
                                                    If Main_Form.sql_array_addon_count = 1 Then
                                                        New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                    End If
                                                End If
                                            End If
                                            'New_Field.Format = DateTimePickerFormat.Short
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                            If Main_Form.sql_array(col, 20).Length > 0 Then
                                                New_Field.format = DateTimePickerFormat.Custom
                                                New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                Main_Form.dgw_query_view.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                            End If
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            New_panel.Controls.Add(New_Field)


                                        Case "image"
                                            New_Field = New PictureBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            'New_Field.Image = ""
                                            New_Field.SizeMode = PictureBoxSizeMode.StretchImage
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 30
                                            New_Field.enabled = False
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            picture_reaction = DirectCast(New_Field, PictureBox)
                                            AddHandler picture_reaction.Click, AddressOf Main_Form.react_open_picture_preview
                                            New_panel.Controls.Add(New_Field)

                                            New_Field = New Button
                                            New_Field.margin = New Padding(0, 0, 0, 0)
                                            New_Field.name = Main_Form.sql_array(col, 4) + "_ofd"
                                            New_Field.text = fn_translate("insert_picture")
                                            New_Field.size = New Drawing.Size(75, 25)
                                            New_Field.Location = New Point(lenght, 2)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 80
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            button_reaction = DirectCast(New_Field, Button)
                                            AddHandler button_reaction.Click, AddressOf Main_Form.react_openfiledialog_for_picture
                                            New_panel.Controls.Add(New_Field)

                                        Case "binary", "varbinary", "xml"
                                            New_Field = New TextBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            New_Field.text = "" 'fn_translate("input_field")
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                            text_reaction = DirectCast(New_Field, TextBox)
                                            New_panel.Controls.Add(New_Field)

                                            New_Field = New Button
                                            New_Field.Name = Main_Form.sql_array(col, 4).Replace("_file", "_selbtn")
                                            New_Field.text = fn_translate("Select_file")
                                            New_Field.size = New Drawing.Size(80, 25)
                                            New_Field.Location = New Point(lenght, 3)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 85
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                            button_reaction = DirectCast(New_Field, Button)
                                            AddHandler button_reaction.Click, AddressOf Main_Form.react_openuserfileheaderdialog
                                            New_panel.Controls.Add(New_Field)

                                        Case Else

                                    End Select




                                Case 3
                                    'load note property
                                    'new field note
                                    New_Field = New Label
                                    New_Field.Name = Main_Form.sql_array(col, 4)
                                    If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                        New_Field.text = fn_translate(Main_Form.sql_array(col, 21))
                                    Else
                                        New_Field.text = Main_Form.sql_array(col, 21)
                                    End If
                                    New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                    New_Field.Location = New Point(lenght, 5)
                                    New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                    New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                    New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                    New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                    New_Field.Cursor = Cursors.Default
                                    If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + Main_Form.sql_array(col, 5)
                                    label_reaction = DirectCast(New_Field, Label)
                                    New_panel.Controls.Add(New_Field)

                                    'final panel size AND position calculating
                                    If Main_Form.tp_dev_detail.Size.Width > 2 * (lenght) Then
                                        If ((col + 1) / 2) = Math.Round(((col + 1) / 2), 0) Then
                                            New_panel.Size = New Drawing.Size(lenght, 30)
                                            column_pos = 0
                                        Else
                                            New_panel.Size = New Drawing.Size(lenght, 30)
                                            column_pos = lenght
                                        End If
                                    Else
                                        New_panel.Size = New Drawing.Size(lenght, 30)
                                    End If


                                    If Main_Form.sql_array(col - 1, 37) = False Then
                                        New_panel.BackColor = not_null_backcolor
                                    Else
                                        'New_panel.BackColor = dev_backcolor
                                    End If

                                    New_panel.Cursor = Cursors.Default
                                    panel_reaction = DirectCast(New_panel, Panel)

                                    Main_Form.tc_user_document.TabPages.Item(0).Controls.Add(New_panel)
                            End Select


                        Catch ex As Exception
                            fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE id=" + Main_Form.lbl_dev_form_id.Text, "LOCAL", False)
                            My.Forms.Main_Form.Cursor = Cursors.Default
                        End Try

                    End If
                Next

                fn_user_form_field_array_from_sql()
                Return True
            Else
                ' MessageBox.Show(fn_translate("form_definition_doesnt_exist"))
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Function fn_load_subforms(ByVal origIdMainIndex As Long) As Boolean
        Try

            Dim New_panel, panel_reaction As Panel
            Dim text_reaction As TextBox
            Dim combo_reaction As ComboBox
            Dim label_reaction As Label
            Dim picture_reaction As PictureBox
            Dim button_reaction As Button
            Dim datagrid As DataGridView
            Dim idIndex As Long = 0

            Dim New_Field
            Dim column_pos = 0, lenght = 0

            For sub_form = 1 To Main_Form.tc_user_document.TabPages.Count - 1
                'cleaning subforms
                For i = 0 To 2
                    For Each ctrl In Main_Form.tc_user_document.TabPages.Item(sub_form).Controls
                        ctrl.Dispose()
                    Next
                Next

                If fn_sql_request("SELECT * FROM [dbo].[form_definition] WHERE [form_id]=(SELECT sb.subform_id FROM subform_binds sb WHERE id=" + Main_Form.tc_user_document.TabPages.Item(sub_form).Name.Replace("tp_user_subform:", "") + " ) ORDER BY input_no,value_no ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) Then

                    datagrid = New DataGridView
                    datagrid.TabIndex = 5
                    datagrid.Name = "dgw_subform:" + (sub_form - 1).ToString
                    datagrid.Location = New Point(0, 0)
                    datagrid.Anchor = AnchorStyles.Left
                    datagrid.Anchor = AnchorStyles.Right
                    datagrid.Dock = DockStyle.Top
                    datagrid.Size = New Drawing.Size(483, subform_dataview_y_size)
                    datagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    datagrid.AutoGenerateColumns = True
                    datagrid.RowHeadersVisible = False
                    datagrid.AllowUserToResizeRows = False
                    datagrid.AllowUserToAddRows = False
                    datagrid.AllowUserToDeleteRows = False
                    datagrid.EditMode = DataGridViewEditMode.EditProgrammatically
                    datagrid.CurrentCell = Nothing
                    datagrid.TabStop = False
                    datagrid.ClearSelection()
                    AddHandler datagrid.CellDoubleClick, AddressOf Main_Form.sub_dgv_CellDoubleClick
                    AddHandler datagrid.CellClick, AddressOf Main_Form.sub_dgv_CellClick

                    'WHERE CLAUSE
                    If origIdMainIndex > 0 Then ' load copied subformdata
                        Main_Form.sql_parameter.Parameters.AddWithValue(Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(0), origIdMainIndex)
                    End If

                    If Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Contains(":") Then
                        If origIdMainIndex = 0 Then Main_Form.sql_parameter.Parameters.AddWithValue(Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(0), Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(0).Split(":")(1))
                        subBindingTableJoin(sub_form - 1) = Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(1)
                        subBindingField(sub_form - 1) = Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(0)
                        subBindingValue(sub_form - 1) = Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(0).Split(":")(1)
                    Else
                        If origIdMainIndex = 0 Then Main_Form.sql_parameter.Parameters.AddWithValue(Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(0), "")
                        subBindingTableJoin(sub_form - 1) = Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(1)
                        subBindingField(sub_form - 1) = Main_Form.tc_user_document.TabPages.Item(sub_form).Tag.ToString.Split("|")(1).Split("/")(0)
                        subBindingValue(sub_form - 1) = Nothing
                    End If

                    fn_sql_load_subform_field("SELECT [basic_sql],[local_db],[table_name],[enable_translate],[export_enabled],[import_enabled],[id],[basic_after_sql],[local_after_db] FROM dbo.form_list WHERE id= " + Main_Form.sql_array(0, 1) + ";", True, sub_form - 1, Main_Form.sql_parameter)

                    Select Case (sub_form - 1)
                        Case 0
                            datagrid.DataSource = dgw_subform_data0
                            fn_insert_substitution("sub[user_subdataview_db_type_0]", Main_Form.sql_subarray0(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_0]", Main_Form.sql_subarray0(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_0]", Main_Form.sql_subarray0(0, 3))
                            fn_insert_substitution("sub[subform_id_0]", Main_Form.sql_subarray0(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_0]", dgw_subform_data0.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_0]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_0]", Main_Form.sql_subarray0(0, 8))
                        Case 1
                            datagrid.DataSource = dgw_subform_data1
                            fn_insert_substitution("sub[user_subdataview_db_type_1]", Main_Form.sql_subarray1(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_1]", Main_Form.sql_subarray1(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_1]", Main_Form.sql_subarray1(0, 3))
                            fn_insert_substitution("sub[subform_id_1]", Main_Form.sql_subarray1(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_1]", dgw_subform_data1.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_1]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_1]", Main_Form.sql_subarray0(0, 8))
                        Case 2
                            datagrid.DataSource = dgw_subform_data2
                            fn_insert_substitution("sub[user_subdataview_db_type_2]", Main_Form.sql_subarray2(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_2]", Main_Form.sql_subarray2(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_2]", Main_Form.sql_subarray2(0, 3))
                            fn_insert_substitution("sub[subform_id_2]", Main_Form.sql_subarray2(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_2]", dgw_subform_data2.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_2]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_2]", Main_Form.sql_subarray0(0, 8))
                        Case 3
                            datagrid.DataSource = dgw_subform_data3
                            fn_insert_substitution("sub[user_subdataview_db_type_3]", Main_Form.sql_subarray3(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_3]", Main_Form.sql_subarray3(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_3]", Main_Form.sql_subarray3(0, 3))
                            fn_insert_substitution("sub[subform_id_3]", Main_Form.sql_subarray3(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_3]", dgw_subform_data3.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_3]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_3]", Main_Form.sql_subarray0(0, 8))
                        Case 4
                            datagrid.DataSource = dgw_subform_data4
                            fn_insert_substitution("sub[user_subdataview_db_type_4]", Main_Form.sql_subarray4(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_4]", Main_Form.sql_subarray4(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_4]", Main_Form.sql_subarray4(0, 3))
                            fn_insert_substitution("sub[subform_id_4]", Main_Form.sql_subarray4(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_4]", dgw_subform_data4.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_4]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_4]", Main_Form.sql_subarray0(0, 8))
                        Case 5
                            datagrid.DataSource = dgw_subform_data5
                            fn_insert_substitution("sub[user_subdataview_db_type_5]", Main_Form.sql_subarray5(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_5]", Main_Form.sql_subarray5(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_5]", Main_Form.sql_subarray5(0, 3))
                            fn_insert_substitution("sub[subform_id_5]", Main_Form.sql_subarray5(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_5]", dgw_subform_data5.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_5]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_5]", Main_Form.sql_subarray0(0, 8))
                        Case 6
                            datagrid.DataSource = dgw_subform_data6
                            fn_insert_substitution("sub[user_subdataview_db_type_6]", Main_Form.sql_subarray6(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_6]", Main_Form.sql_subarray6(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_6]", Main_Form.sql_subarray6(0, 3))
                            fn_insert_substitution("sub[subform_id_6]", Main_Form.sql_subarray6(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_6]", dgw_subform_data6.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_6]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_6]", Main_Form.sql_subarray0(0, 8))
                        Case 7
                            datagrid.DataSource = dgw_subform_data7
                            fn_insert_substitution("sub[user_subdataview_db_type_7]", Main_Form.sql_subarray7(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_7]", Main_Form.sql_subarray7(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_7]", Main_Form.sql_subarray7(0, 3))
                            fn_insert_substitution("sub[subform_id_7]", Main_Form.sql_subarray7(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_7]", dgw_subform_data7.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_7]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_7]", Main_Form.sql_subarray0(0, 8))
                        Case 8
                            datagrid.DataSource = dgw_subform_data8
                            fn_insert_substitution("sub[user_subdataview_db_type_8]", Main_Form.sql_subarray8(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_8]", Main_Form.sql_subarray8(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_8]", Main_Form.sql_subarray8(0, 3))
                            fn_insert_substitution("sub[subform_id_8]", Main_Form.sql_subarray8(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_8]", dgw_subform_data8.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_8]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_8]", Main_Form.sql_subarray0(0, 8))
                        Case 9
                            datagrid.DataSource = dgw_subform_data9
                            fn_insert_substitution("sub[user_subdataview_db_type_9]", Main_Form.sql_subarray9(0, 1))
                            fn_insert_substitution("sub[user_subdataview_table_9]", Main_Form.sql_subarray9(0, 2))
                            fn_insert_substitution("sub[user_subdataview_translate_9]", Main_Form.sql_subarray9(0, 3))
                            fn_insert_substitution("sub[subform_id_9]", Main_Form.sql_subarray9(0, 6))
                            fn_insert_substitution("sub[subdataview_record_count_9]", dgw_subform_data9.Count)
                            fn_insert_substitution("sub[subform_after_sql_command_9]", Main_Form.sql_subarray0(0, 7))
                            fn_insert_substitution("sub[subform_after_sql_local_9]", Main_Form.sql_subarray0(0, 8))

                    End Select

                    datagrid.VirtualMode = False
                    datagrid.CreateControl()

                    Main_Form.tc_user_document.TabPages.Item(sub_form).Controls.Add(datagrid)

                    'calculate next index field
                    idIndex = fn_get_next_gdv_index(datagrid)

                    If idIndex > 1 Then
                        fn_copy_dgv_to_local(datagrid, subBindingField(sub_form - 1), subBindingValue(sub_form - 1))
                    End If

                    For col = 0 To Main_Form.sql_array_count Step 1
                        If IsNumeric(Main_Form.sql_array(col, 2)) Then

                            Try 'separate inputs field
                                Select Case Main_Form.sql_array(col, 3)
                                    Case 1

                                        If Main_Form.sql_array(col, 26) = 1 Then
                                            primary_subkey_columns(sub_form - 1) += Main_Form.sql_array(col, 2) + ","
                                            primary_subkey(sub_form - 1) = True
                                        End If

                                        lenght = 0
                                        'MessageBox.Show(col.ValueType.Name)
                                        New_panel = New Panel
                                        New_panel.TabIndex = Main_Form.sql_array(col, 2) * 10
                                        New_panel.Name = Main_Form.sql_array(col, 2)
                                        New_panel.Location = New Point(Main_Form.sql_array(col, 7), subform_dataview_y_size + Main_Form.sql_array(col, 8))

                                        'new fields label
                                        New_Field = New Label
                                        New_Field.Name = Main_Form.sql_array(col, 4)
                                        If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                            New_Field.text = fn_translate(Main_Form.sql_array(col, 21))
                                        Else
                                            New_Field.text = Main_Form.sql_array(col, 21)
                                        End If
                                        New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                        New_Field.Location = New Point(lenght, 5)
                                        New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                        New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                        New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), CBool(Main_Form.sql_array(col, 14)), CBool(Main_Form.sql_array(col, 15)), CBool(Main_Form.sql_array(col, 16)), CBool(Main_Form.sql_array(col, 17)))
                                        New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                        New_Field.Cursor = Cursors.Default
                                        If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + Main_Form.sql_array(col, 5)
                                        label_reaction = DirectCast(New_Field, Label)
                                        New_panel.Controls.Add(New_Field)

                                    Case 2  'load field property
                                        Select Case Main_Form.sql_array(col, 25)

                                            Case "int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                                If Main_Form.sql_array(col - 1, 21) = subBindingField(sub_form - 1) And Not subBindingValue(sub_form - 1) Is Nothing Then
                                                    New_Field = New TextBox
                                                    New_Field.Text = subBindingValue(sub_form - 1).ToString
                                                    New_Field.enabled = False
                                                ElseIf Main_Form.sql_array(col - 1, 21) = subBindingField(sub_form - 1) And subBindingValue(sub_form - 1) Is Nothing Then
                                                    New_Field = New TextBox
                                                    New_Field.Text = "MainRecId"
                                                    New_Field.enabled = False
                                                ElseIf Main_Form.sql_array(col - 1, 26) = 1 Then
                                                    New_Field = New TextBox
                                                    New_Field.Text = idIndex
                                                    New_Field.enabled = False
                                                ElseIf Main_Form.sql_array(col, 22) = Nothing Then
                                                    New_Field = New TextBox
                                                    New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                    New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                    text_reaction = DirectCast(New_Field, TextBox)
                                                    AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit

                                                    If Not fn_search_substitution(Main_Form.sql_array(col, 21)) Is Nothing Then
                                                        New_Field.text = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                    Else
                                                        New_Field.text = Main_Form.sql_array(col, 21)
                                                    End If
                                                Else
                                                    If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then

                                                        If Main_Form.sql_array_addon_count = 1 Then
                                                            New_Field = New TextBox
                                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                            New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                            text_reaction = DirectCast(New_Field, TextBox)
                                                            AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit
                                                            New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                        Else
                                                            New_Field = New ComboBox
                                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                            combo_reaction = DirectCast(New_Field, ComboBox)
                                                            AddHandler combo_reaction.KeyPress, AddressOf Main_Form.react_isdigit


                                                            'insert custom value
                                                            If UCase(Main_Form.sql_array_addon(0, 2)) = "YES" Then
                                                                New_Field.DropDownStyle = ComboBoxStyle.DropDown
                                                            Else 'insert exist value only
                                                                New_Field.DropDownStyle = ComboBoxStyle.DropDownList
                                                            End If

                                                            'fill list values, set default value 0 - none, X-set
                                                            For each_rec = 0 To Main_Form.sql_array_addon_count - 1
                                                                'New_Field.Items.Insert(each_rec, Main_Form.sql_array_addon(each_rec, 0))
                                                                New_Field.Items.Add(New ComboBoxItem(Of String)(Main_Form.sql_array_addon(each_rec, 1), Main_Form.sql_array_addon(each_rec, 0)))
                                                                If ((Main_Form.sql_array_addon(each_rec, 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).ToString()) Or (Main_Form.sql_array_addon(each_rec, 3).Contains("#") AndAlso (each_rec + 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).Replace("#", ""))) Then
                                                                    New_Field.SelectedIndex = New_Field.items.count - 1
                                                                End If
                                                            Next
                                                            'New_panel.Controls.Add(New_Field)
                                                        End If
                                                    Else
                                                        New_Field = New TextBox
                                                        New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                        New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                        text_reaction = DirectCast(New_Field, TextBox)
                                                        AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit
                                                    End If
                                                End If
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                New_panel.Controls.Add(New_Field)


                                            Case "char", "nchar", "ntext", "nvarchar", "text", "varchar"
                                                If Main_Form.sql_array(col - 1, 21) = "table_name" And subBindingTableJoin(sub_form - 1) Then
                                                    New_Field = New TextBox
                                                    New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                    text_reaction = DirectCast(New_Field, TextBox)
                                                    New_Field.text = fn_search_substitution("sub[user_dataview_table]")
                                                    New_Field.enabled = False
                                                ElseIf Main_Form.sql_array(col, 22) = Nothing Then
                                                    New_Field = New TextBox
                                                    New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                    text_reaction = DirectCast(New_Field, TextBox)
                                                    New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)

                                                    If Not fn_search_substitution(Main_Form.sql_array(col, 21)) Is Nothing Then
                                                        New_Field.text = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                    Else
                                                        New_Field.text = Main_Form.sql_array(col, 21)
                                                    End If
                                                Else
                                                    If fn_load_sql_addon(Main_Form.sql_array(col, 22), CBool(Main_Form.sql_array(col, 38)), Main_Form.sql_array(col - 1, 21)) Then

                                                        If Main_Form.sql_array_addon_count = 1 Then
                                                            New_Field = New TextBox
                                                            New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                            text_reaction = DirectCast(New_Field, TextBox)
                                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)

                                                            New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                        Else
                                                            New_Field = New ComboBox
                                                            combo_reaction = DirectCast(New_Field, ComboBox)
                                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                            'insert custom value
                                                            If UCase(Main_Form.sql_array_addon(0, 2)) = "YES" Then
                                                                New_Field.DropDownStyle = ComboBoxStyle.DropDown
                                                            Else 'insert exist value only
                                                                New_Field.DropDownStyle = ComboBoxStyle.DropDownList
                                                            End If

                                                            'fill list values, set default value 0 - none, X-set
                                                            For each_rec = 0 To Main_Form.sql_array_addon_count - 1
                                                                'New_Field.Items.Insert(each_rec, Main_Form.sql_array_addon(each_rec, 0))
                                                                New_Field.Items.Add(New ComboBoxItem(Of String)(Main_Form.sql_array_addon(each_rec, 1), Main_Form.sql_array_addon(each_rec, 0)))
                                                                If ((Main_Form.sql_array_addon(each_rec, 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).ToString()) Or (Main_Form.sql_array_addon(each_rec, 3).Contains("#") AndAlso (each_rec + 1).ToString() = Main_Form.sql_array_addon(each_rec, 3).Replace("#", ""))) Then
                                                                    New_Field.SelectedIndex = New_Field.items.count - 1
                                                                End If
                                                            Next
                                                        End If
                                                    Else
                                                        New_Field = New TextBox
                                                        New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                        text_reaction = DirectCast(New_Field, TextBox)
                                                        New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                    End If
                                                End If
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155

                                                New_Field.tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                New_panel.Controls.Add(New_Field)


                                            Case "uniqueidentifier"
                                                New_Field = New TextBox
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                New_Field.text = "" 'fn_translate("input_field")
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                                New_Field.enabled = False
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                text_reaction = DirectCast(New_Field, TextBox)
                                                New_panel.Controls.Add(New_Field)

                                            Case "bit"
                                                New_Field = New CheckBox
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                If Main_Form.sql_array(col, 22) = Nothing Then
                                                    If Not fn_search_substitution(Main_Form.sql_array(col, 21)) Is Nothing Then
                                                        Boolean.TryParse(fn_search_substitution(Main_Form.sql_array(col, 21)), New_Field.checked)
                                                    Else
                                                        Boolean.TryParse(Main_Form.sql_array(col, 21), New_Field.checked)
                                                    End If
                                                Else
                                                    'provést SQL
                                                End If

                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 5)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 25
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                New_panel.Controls.Add(New_Field)


                                            Case "time"
                                                New_Field = New DateTimePicker
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                If Main_Form.sql_array(col, 22) = Nothing Then
                                                    If IsDate(fn_search_substitution(Main_Form.sql_array(col, 21))) Then
                                                        New_Field.value = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                    ElseIf Main_Form.sql_array(col, 21).ToUpper = "NOW" Then
                                                        New_Field.value = DateTime.Now
                                                    ElseIf IsDate(Main_Form.sql_array(col, 21)) Then
                                                        Date.TryParse(Main_Form.sql_array(col, 21), New_Field.value)
                                                    End If
                                                Else
                                                    'Do SQL
                                                End If
                                                New_Field.Format = DateTimePickerFormat.Time
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                                If Main_Form.sql_array(col, 20).Length > 0 Then
                                                    New_Field.format = DateTimePickerFormat.Custom
                                                    New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                    Main_Form.dgw_query_view.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                                End If
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                New_panel.Controls.Add(New_Field)

                                            Case "date"
                                                New_Field = New DateTimePicker
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                If Main_Form.sql_array(col, 22) = Nothing Then
                                                    If IsDate(fn_search_substitution(Main_Form.sql_array(col, 21))) Then
                                                        New_Field.value = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                    ElseIf Main_Form.sql_array(col, 21).ToUpper = "NOW" Then
                                                        New_Field.value = DateTime.Now
                                                    ElseIf IsDate(Main_Form.sql_array(col, 21)) Then
                                                        Date.TryParse(Main_Form.sql_array(col, 21), New_Field.value)
                                                    End If
                                                Else
                                                    'Do SQL
                                                End If
                                                New_Field.Format = DateTimePickerFormat.Short
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                                If Main_Form.sql_array(col, 20).Length > 0 Then
                                                    New_Field.format = DateTimePickerFormat.Custom
                                                    New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                    Main_Form.dgw_query_view.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                                End If
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                New_panel.Controls.Add(New_Field)

                                            Case "datetime", "datetime2", "datetimeoffset", "timestamp"
                                                New_Field = New DateTimePicker
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                If Main_Form.sql_array(col, 22) = Nothing Then
                                                    If IsDate(fn_search_substitution(Main_Form.sql_array(col, 21))) Then
                                                        New_Field.value = fn_search_substitution(Main_Form.sql_array(col, 21))
                                                    ElseIf Main_Form.sql_array(col, 21).ToUpper = "NOW" Then
                                                        New_Field.value = DateTime.Now
                                                    ElseIf IsDate(Main_Form.sql_array(col, 21)) Then
                                                        Date.TryParse(Main_Form.sql_array(col, 21), New_Field.value)
                                                    End If
                                                Else
                                                    'Do SQL
                                                End If
                                                'New_Field.Format = DateTimePickerFormat.Short
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                                If Main_Form.sql_array(col, 20).Length > 0 Then
                                                    New_Field.format = DateTimePickerFormat.Custom
                                                    New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                    Main_Form.dgw_query_view.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                                End If
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                New_panel.Controls.Add(New_Field)


                                            Case "image"
                                                New_Field = New PictureBox
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                'New_Field.Image = ""
                                                New_Field.SizeMode = PictureBoxSizeMode.StretchImage
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 30
                                                New_Field.enabled = False
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                picture_reaction = DirectCast(New_Field, PictureBox)
                                                AddHandler picture_reaction.Click, AddressOf Main_Form.react_open_picture_preview
                                                New_panel.Controls.Add(New_Field)

                                                New_Field = New Button
                                                New_Field.margin = New Padding(0, 0, 0, 0)
                                                New_Field.name = Main_Form.sql_array(col, 4) + "_ofd"
                                                New_Field.text = fn_translate("insert_picture")
                                                New_Field.size = New Drawing.Size(75, 25)
                                                New_Field.Location = New Point(lenght, 2)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 80
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                button_reaction = DirectCast(New_Field, Button)
                                                AddHandler button_reaction.Click, AddressOf Main_Form.react_openfiledialog_for_picture
                                                New_panel.Controls.Add(New_Field)

                                            Case "binary", "varbinary", "xml"
                                                New_Field = New TextBox
                                                New_Field.Name = Main_Form.sql_array(col, 4)
                                                New_Field.text = "" 'fn_translate("input_field")
                                                New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                                New_Field.Location = New Point(lenght, 4)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 105
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                New_Field.Tag = CBool(Main_Form.sql_array(col, 35)) 'save_to_db
                                                text_reaction = DirectCast(New_Field, TextBox)
                                                New_panel.Controls.Add(New_Field)

                                                New_Field = New Button
                                                New_Field.margin = New Padding(0, 0, 0, 0)
                                                New_Field.name = Main_Form.sql_array(col, 4).Replace("_file", "_selbtn")
                                                New_Field.text = fn_translate("Select_file")
                                                New_Field.size = New Drawing.Size(80, 25)
                                                New_Field.Location = New Point(lenght, 3)
                                                New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                                New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                                New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                                New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                                New_Field.Cursor = Cursors.Hand
                                                If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 85
                                                New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
                                                button_reaction = DirectCast(New_Field, Button)
                                                AddHandler button_reaction.Click, AddressOf Main_Form.react_openuserfileheaderdialog
                                                New_panel.Controls.Add(New_Field)
                                            Case Else

                                        End Select




                                    Case 3
                                        'load note property
                                        'new field note
                                        New_Field = New Label
                                        New_Field.Name = Main_Form.sql_array(col, 4)
                                        If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                            New_Field.text = fn_translate(Main_Form.sql_array(col, 21))
                                        Else
                                            New_Field.text = Main_Form.sql_array(col, 21)
                                        End If
                                        New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                        New_Field.Location = New Point(lenght, 5)
                                        New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                        New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                        New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                        New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                        New_Field.Cursor = Cursors.Default
                                        If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + Main_Form.sql_array(col, 5)
                                        label_reaction = DirectCast(New_Field, Label)
                                        New_panel.Controls.Add(New_Field)

                                        'final panel size AND position calculating
                                        If Main_Form.tp_dev_detail.Size.Width > 2 * (lenght) Then
                                            If ((col + 1) / 2) = Math.Round(((col + 1) / 2), 0) Then
                                                New_panel.Size = New Drawing.Size(lenght, 30)
                                                column_pos = 0
                                            Else
                                                New_panel.Size = New Drawing.Size(lenght, 30)
                                                column_pos = lenght
                                            End If
                                        Else
                                            New_panel.Size = New Drawing.Size(lenght, 30)
                                        End If


                                        If Main_Form.sql_array(col - 1, 37) = False Then
                                            New_panel.BackColor = not_null_backcolor
                                        Else
                                            'New_panel.BackColor = dev_backcolor
                                        End If

                                        New_panel.Cursor = Cursors.Default
                                        panel_reaction = DirectCast(New_panel, Panel)
                                        New_panel.CreateControl()

                                        Main_Form.tc_user_document.TabPages.Item(sub_form).Controls.Add(New_panel)
                                End Select


                            Catch ex As Exception
                                fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE id=" + Main_Form.lbl_dev_form_id.Text, "LOCAL", False)
                                My.Forms.Main_Form.Cursor = Cursors.Default
                            End Try

                        End If
                    Next

                Else
                End If

                fn_user_subform_field_array_from_sql(sub_form - 1)

            Next

            Return True
        Catch ex As Exception
            MessageBox.Show(fn_translate("subform_load_error") + ": " + ex.Message)
            Return False
        End Try
    End Function


    'check required fileds in subform
    Function fn_check_required_new_user_subrec_ok(ByVal subMenuIndex As Integer) As Boolean
        fn_check_required_new_user_subrec_ok = False
        Dim field_type As String
        Dim prev_ctrl As Control = Nothing
        Dim line_no As Integer = 1
        Dim datagridview As New DataGridView
        Dim requiredFieldError As Boolean = False
        Try
            For Each Ctrl As Control In Main_Form.tc_user_document.TabPages.Item(subMenuIndex).Controls '.OfType(Of Panel)()
                Try
                    If Ctrl.GetType.ToString.Replace("System.Windows.Forms.", "") = "DataGridView" Then
                        datagridview = Ctrl
                    End If
                    For Each SubCtrl In Ctrl.Controls
                        field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                        Select Case field_type
                            Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"
                                If field_type = "DateTimePicker" And SubCtrl.tag = True Then
                                    If fn_getsubformarray(subMenuIndex - 1)(17, line_no).Length > 0 Then
                                    Else
                                    End If
                                ElseIf field_type = "CheckBox" And SubCtrl.tag = True Then
                                ElseIf field_type = "PictureBox" And SubCtrl.tag = True Then
                                    If SubCtrl.ImageLocation <> Nothing Then
                                    ElseIf (SubCtrl.ImageLocation = Nothing And SubCtrl.enabled = True) Then
                                    Else
                                    End If
                                ElseIf field_type = "TextBox" And SubCtrl.tag = True And SubCtrl.name.ToString.Contains("_file") Then
                                    If SubCtrl.text <> Nothing Then
                                    Else
                                    End If
                                ElseIf SubCtrl.tag = True Then 'String - textbox,combobox
                                    For i = 0 To fn_getsubformarray(subMenuIndex - 1).Length - 1
                                        If fn_getsubformarray(subMenuIndex - 1)(1, i) = SubCtrl.name Then
                                            If SubCtrl.text.length = 0 Then
                                                If Not CBool(fn_getsubformarray(subMenuIndex - 1)(27, i)) Then requiredFieldError = True
                                            ElseIf fn_getsubformarray(subMenuIndex - 1)(26, i) = "char" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "ntext" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nvarchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "text" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "varchar" Then
                                            Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                            End If
                                            Exit For
                                        End If
                                    Next
                                ElseIf SubCtrl.tag = False Then ' primary keys which are not saved
                                    For i = 0 To fn_getsubformarray(subMenuIndex - 1).Length - 1
                                        If fn_getsubformarray(subMenuIndex - 1)(1, i) = SubCtrl.name Then
                                            If fn_getsubformarray(subMenuIndex - 1)(26, i) = "char" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "ntext" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nvarchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "text" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "varchar" Then
                                            Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                            End If
                                            Exit For
                                        End If
                                    Next
                                End If

                            Case Else
                                prev_ctrl = SubCtrl
                        End Select
                    Next SubCtrl
                Catch ex As Exception
                End Try
            Next

            If requiredFieldError Then
                MessageBox.Show(fn_translate("notfilledallrequiredfields"))
            Else
                fn_check_required_new_user_subrec_ok = True
            End If
        Catch ex As Exception
        End Try
    End Function


    ' insert/update new line to local subdatagridview
    Function fn_insUpd_new_user_subrec(ByVal subMenuIndex As Integer, ByVal update As Boolean) As Boolean
        fn_insUpd_new_user_subrec = False
        Dim field_type As String
        Dim prev_ctrl As Control = Nothing
        Dim line_no As Integer = 1
        Dim newRecord As String()
        Dim newCell As New DataGridViewTextBoxCell
        Dim datagridview As New DataGridView
        Dim datagridviewColumns As New DataGridViewColumn
        Dim columnIndex = 0
        Dim insertHeader As Boolean = False
        'Dim idIndex As Long = 0

        If Not fn_check_required_new_user_subrec_ok(subMenuIndex) Then
            Exit Function
        Else
            Try
                If update Then 'remove old subrecord
                    fn_delete_from_user_subrec_datagrid(subMenuIndex, False)
                End If

                For Each Ctrl As Control In Main_Form.tc_user_document.TabPages.Item(subMenuIndex).Controls '.OfType(Of Panel)()
                    Try
                        If Ctrl.GetType.ToString.Replace("System.Windows.Forms.", "") = "DataGridView" Then
                            datagridview = Ctrl
                            If datagridview.Columns.Count = 0 Then insertHeader = True
                            'idIndex = fn_get_next_gdv_index(datagridview)
                        End If

                        For Each SubCtrl In Ctrl.Controls
                            newCell.Value = Nothing
                            datagridviewColumns.Name = Nothing

                            field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                            Select Case field_type
                                Case "Label" ' create new header
                                    If SubCtrl.Name.ToString.Contains("_label") AndAlso insertHeader Then
                                        For i = 0 To fn_getsubformarray(subMenuIndex - 1).Length - 1
                                            If fn_getsubformarray(subMenuIndex - 1)(1, i) = SubCtrl.name Then
                                                datagridviewColumns.Name = fn_getsubformarray(subMenuIndex - 1)(18, i)
                                                If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                                    datagridviewColumns.HeaderText = fn_translate(fn_getsubformarray(subMenuIndex - 1)(18, i))
                                                Else
                                                    datagridviewColumns.HeaderText = fn_getsubformarray(subMenuIndex - 1)(18, i)
                                                End If
                                                Exit For
                                            End If
                                        Next
                                    End If

                                Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"

                                    If field_type = "DateTimePicker" And SubCtrl.tag = True Then
                                        If fn_getsubformarray(subMenuIndex - 1)(17, line_no).Length > 0 Then
                                            newCell.Value = Format(SubCtrl.value, fn_getsubformarray(subMenuIndex - 1)(17, line_no).ToString)
                                            SubCtrl.value = Nothing
                                        Else
                                            newCell.Value = Format(SubCtrl.value.date, "yyyy-MM-dd")
                                            SubCtrl.value = ""
                                        End If

                                    ElseIf field_type = "CheckBox" And SubCtrl.tag = True Then
                                        newCell.Value = SubCtrl.Checked.ToString
                                        SubCtrl.Checked = False
                                    ElseIf field_type = "PictureBox" And SubCtrl.tag = True Then
                                        If SubCtrl.ImageLocation <> Nothing Then
                                            newCell.Value = "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.ImageLocation + "', SINGLE_BLOB)as temp_picture) "
                                        ElseIf (SubCtrl.ImageLocation = Nothing And SubCtrl.enabled = True) Then
                                            Dim imageConverter As New ImageConverter()
                                            Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(SubCtrl.Image, GetType(Byte())), Byte())
                                            newCell.Value = "@" + prev_ctrl.Text + ""
                                            SubCtrl.Image = Nothing
                                        Else
                                            newCell.Value = ""
                                        End If

                                    ElseIf field_type = "TextBox" And SubCtrl.tag = True And SubCtrl.name.ToString.Contains("_file") Then
                                        If SubCtrl.text <> Nothing Then
                                            newCell.Value = "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.text + "', SINGLE_BLOB)as temp_picture) "
                                            SubCtrl.text = Nothing
                                        Else
                                            newCell.Value = ""
                                        End If
                                    ElseIf SubCtrl.tag = True Then 'String - textbox,combobox

                                        For i = 0 To fn_getsubformarray(subMenuIndex - 1).Length - 1
                                            If fn_getsubformarray(subMenuIndex - 1)(1, i) = SubCtrl.name Then
                                                If SubCtrl.text.length = 0 Then
                                                    newCell.Value = ""
                                                ElseIf fn_getsubformarray(subMenuIndex - 1)(26, i) = "char" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "ntext" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nvarchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "text" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "varchar" Then
                                                    newCell.Value = Replace(SubCtrl.text, "'", "''")
                                                    If SubCtrl.text <> "MainRecId" Then SubCtrl.text = Nothing
                                                Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                                    newCell.Value = SubCtrl.text.replace(",", ".")
                                                    If SubCtrl.text <> "MainRecId" Then SubCtrl.text = Nothing
                                                End If
                                                Exit For
                                            End If
                                        Next
                                    ElseIf SubCtrl.tag = False Then ' primary keys which are not saved
                                        For i = 0 To fn_getsubformarray(subMenuIndex - 1).Length - 1
                                            If fn_getsubformarray(subMenuIndex - 1)(1, i) = SubCtrl.name Then
                                                'If SubCtrl.text.length = 0 Then
                                                '    newCell.Value = idIndex
                                                'Else
                                                If fn_getsubformarray(subMenuIndex - 1)(26, i) = "char" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "ntext" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "nvarchar" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "text" Or fn_getsubformarray(subMenuIndex - 1)(26, i) = "varchar" Then
                                                    newCell.Value = Replace(SubCtrl.text, "'", "''")
                                                    SubCtrl.text = Nothing
                                                Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                                    newCell.Value = SubCtrl.text.replace(",", ".")
                                                    SubCtrl.text = Nothing
                                                End If
                                                Exit For
                                            End If
                                        Next
                                    End If

                                Case Else
                                    prev_ctrl = SubCtrl
                            End Select

                            If datagridviewColumns.Name.Length > 0 Then
                                datagridview.Columns.Add(datagridviewColumns.Name, datagridviewColumns.HeaderText)
                            End If

                            If Not newCell.Value Is Nothing Then
                                ReDim Preserve newRecord(columnIndex)
                                newRecord(columnIndex) = newCell.Value
                                columnIndex += 1
                            End If

                        Next SubCtrl
                    Catch ex As Exception
                    End Try
                Next

                datagridview.DataSource = Nothing
                datagridview.Rows.Add(newRecord)
                datagridview.CurrentCell.Selected = 0
                datagridview.ClearSelection()

                Dim e As DataGridViewCellEventArgs
                Main_Form.sub_dgv_CellClick(datagridview, e)
                fn_insUpd_new_user_subrec = True

            Catch ex As Exception
                MessageBox.Show(fn_translate("subform_cannot_be_saved"))
            End Try
        End If
    End Function

    'save new user record
    Function fn_insert_new_user_rec() As Boolean
        fn_insert_new_user_rec = False
        Dim int_query As String()
        Dim fld_list As String()
        Dim value_list As String()
        Dim field_type As String
        Dim prev_ctrl As Control = Nothing
        Dim line_no As Integer = 1

        '0 = mainform
        Dim tab_index As Integer = 0

        ReDim Preserve int_query(tab_index)
        ReDim Preserve fld_list(tab_index)
        ReDim Preserve value_list(tab_index)
        int_query(tab_index) = "INSERT INTO " + fn_search_substitution("sub[user_dataview_table]") + " ("
        fld_list(tab_index) = ""
        value_list(tab_index) = ") VALUES ("


        For Each Ctrl In Main_Form.tc_user_document.TabPages.Item(0).Controls.OfType(Of Panel)()
            Try
                For Each SubCtrl In Ctrl.Controls

                    field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                    Select Case field_type
                        Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"

                            ' If SubCtrl.tag = True Then
                            If fld_list(tab_index).Length > 0 Then
                                fld_list(tab_index) += ","
                                value_list(tab_index) += ","
                            End If

                            If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                fld_list(tab_index) += "[" + fn_sys_translate(prev_ctrl.Text) + "]"
                            Else
                                fld_list(tab_index) += "[" + prev_ctrl.Text + "]"
                            End If

                            If field_type = "DateTimePicker" Then
                                If Main_Form.user_form_field_list(17, line_no).Length > 0 Then
                                    value_list(tab_index) += "'" + Format(SubCtrl.value, Main_Form.user_form_field_list(17, line_no).ToString) + "'"
                                Else
                                    value_list(tab_index) += "'" + Format(SubCtrl.value.date, "yyyy-MM-dd") + "'"
                                End If

                            ElseIf field_type = "CheckBox" Then
                                value_list(tab_index) += "'" + SubCtrl.Checked.ToString + "'"

                            ElseIf field_type = "PictureBox" Then
                                If SubCtrl.ImageLocation <> Nothing Then
                                    value_list(tab_index) += "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.ImageLocation + "', SINGLE_BLOB)as temp_picture) "
                                ElseIf (SubCtrl.ImageLocation = Nothing And SubCtrl.enabled = True) Then
                                    Dim imageConverter As New ImageConverter()
                                    Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(SubCtrl.Image, GetType(Byte())), Byte())
                                    value_list(tab_index) += "@" + prev_ctrl.Text + ""
                                    Main_Form.sql_parameter.Parameters.AddWithValue("@" + prev_ctrl.Text, imageByte)
                                Else
                                    value_list(tab_index) += "NULL"
                                End If

                            ElseIf field_type = "TextBox" And SubCtrl.name.ToString.Contains("_file") Then
                                If SubCtrl.text <> Nothing Then
                                    value_list(tab_index) += "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.text + "', SINGLE_BLOB)as temp_picture) "
                                Else
                                    value_list(tab_index) += "NULL"
                                End If
                            Else 'String - textbox,combobox

                                For i = 0 To Main_Form.user_form_field_list.Length / 29
                                    If Main_Form.user_form_field_list(1, i) = SubCtrl.name Then
                                        If SubCtrl.text.length = 0 Then
                                            value_list(tab_index) += "NULL"
                                        ElseIf Main_Form.user_form_field_list(26, i) = "char" Or Main_Form.user_form_field_list(26, i) = "nchar" Or Main_Form.user_form_field_list(26, i) = "ntext" Or Main_Form.user_form_field_list(26, i) = "nvarchar" Or Main_Form.user_form_field_list(26, i) = "text" Or Main_Form.user_form_field_list(26, i) = "varchar" Then
                                            If Not SubCtrl.GetType.GetProperty("SelectedIndex") Is Nothing Then
                                                value_list(tab_index) += "'" + Replace(SubCtrl.SelectedItem.Value, "'", "''") + "'"
                                            Else
                                                value_list(tab_index) += "'" + Replace(SubCtrl.text, "'", "''") + "'"
                                            End If


                                        Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                            If Not SubCtrl.GetType.GetProperty("SelectedIndex") Is Nothing Then
                                                value_list(tab_index) += SubCtrl.SelectedItem.Value.replace(",", ".")
                                            Else
                                                value_list(tab_index) += SubCtrl.text.replace(",", ".")
                                            End If


                                        End If
                                            Exit For
                                    End If
                                Next
                            End If
                            '  End If
                        Case Else
                            prev_ctrl = SubCtrl
                    End Select
                    line_no += 1
                Next SubCtrl
            Catch ex As Exception

            End Try

        Next

        If fn_sql_request(int_query(tab_index) + fld_list(tab_index) + ",[creator],[created]" + value_list(tab_index) + ",'" + fn_search_substitution("sub[user_name]") + "',GETDATE()" + ")", "INSERT", fn_search_substitution("sub[user_dataview_db_type]"), False, False, Main_Form.sql_parameter, False, False) = False Then
            MessageBox.Show(fn_translate("rec_not_be_saved"))
        Else
            ' run SQL after transaction
            If fn_search_substitution("sub[after_sql_command]").Length > 0 Then
                fn_run_sql_transaction(fn_search_substitution("sub[after_sql_command]"), CType(fn_search_substitution("sub[after_sql_local]"), Boolean))
            End If

            ' saved user record and reloaded data
            If fn_db_operations_with_user_subrecs(False) Then
                fn_clear_user_form()
                Main_Form.tc_data.SelectedIndex = 0
                fn_load_basic_form("")
                Main_Form.btn_main_btn_1.Enabled = True
                fn_insert_new_user_rec = True
            End If
        End If

    End Function


    'save new user joined subrecords
    Function fn_db_operations_with_user_subrecs(ByVal deleteOnly) As Boolean
        fn_db_operations_with_user_subrecs = True
        Dim int_query(Main_Form.tc_user_document.TabPages.Count - 2) As String
        Dim fld_list(Main_Form.tc_user_document.TabPages.Count - 2) As String
        Dim value_join As String = ") VALUES "
        Dim value_list(Main_Form.tc_user_document.TabPages.Count - 2) As String
        Dim subValue_list(0) As String
        Dim field_type As String
        Dim prev_ctrl As Control = Nothing
        Dim line_no As Integer = 1
        Dim dataGridView As New DataGridView
        Dim deleteJoin(Main_Form.tc_user_document.TabPages.Count - 2) As String
        For tabIndex = 1 To Main_Form.tc_user_document.TabPages.Count - 1

            For Each ctrl As DataGridView In Main_Form.tc_user_document.TabPages.Item(tabIndex).Controls.OfType(Of DataGridView)()
                dataGridView = ctrl
            Next

            deleteJoin(tabIndex - 1) = "DELETE FROM " & fn_search_substitution("sub[user_subdataview_table_" & (tabIndex - 1) & "]") & " WHERE " & Main_Form.tc_user_document.TabPages.Item(tabIndex).Tag.ToString.Split("|")(1).Split("/")(0) & " = " & Main_Form.tc_user_document.TabPages.Item(tabIndex).Tag.ToString.Split("|")(0).Split(":")(1) & ";"
            If deleteOnly Then
                If fn_sql_request(deleteJoin(tabIndex - 1), "DELETE", fn_search_substitution("sub[user_subdataview_db_type_" & (tabIndex - 1) & "]"), False, False, Main_Form.sql_parameter, False, False) = False Then
                    MessageBox.Show(fn_translate("subrec_not_be_removed"))
                    fn_db_operations_with_user_subrecs = False
                End If
            Else

                ReDim Preserve subValue_list(dataGridView.Rows.Count - 1)
                int_query(tabIndex - 1) = "INSERT INTO " + fn_search_substitution("sub[user_subdataview_table_" & (tabIndex - 1) & "]") + " ("
                fld_list(tabIndex - 1) = ""

                For Each Ctrl In Main_Form.tc_user_document.TabPages.Item(tabIndex).Controls.OfType(Of Panel)()
                    Try
                        For Each SubCtrl In Ctrl.Controls

                            field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                            Select Case field_type
                                Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"

                                    '  If SubCtrl.Tag = True Then
                                    If fld_list(tabIndex - 1).Length > 0 Then
                                        fld_list(tabIndex - 1) += ","
                                        For Each row As DataGridViewRow In dataGridView.Rows
                                            subValue_list(row.Index) += ","
                                        Next
                                    End If

                                    If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                        fld_list(tabIndex - 1) += "[" + fn_sys_translate(prev_ctrl.Text) + "]"
                                    Else
                                        fld_list(tabIndex - 1) += "[" + prev_ctrl.Text + "]"
                                    End If


                                    If field_type = "DateTimePicker" Then
                                        If fn_getsubformarray(tabIndex - 1)(17, line_no).Length > 0 Then
                                            For Each row As DataGridViewRow In dataGridView.Rows
                                                subValue_list(row.Index) += "'" + Format(row.Cells.Item(CType(CType(SubCtrl, Control).Name.Replace("_field", ""), Int32) - 1).Value, fn_getsubformarray(tabIndex - 1)(17, line_no).ToString) + "'"
                                            Next

                                        Else
                                            For Each row As DataGridViewRow In dataGridView.Rows
                                                subValue_list(row.Index) += "'" + Format(row.Cells.Item(CType(CType(SubCtrl, Control).Name.Replace("_field", ""), Int32) - 1).Value, "yyyy-MM-dd") + "'"
                                            Next
                                        End If

                                    ElseIf field_type = "CheckBox" Then
                                        For Each row As DataGridViewRow In dataGridView.Rows
                                            subValue_list(row.Index) += "'" + row.Cells.Item(CType(CType(SubCtrl, Control).Name.Replace("_field", ""), Int32) - 1).Value.Checked.ToString + "'"
                                        Next

                                        'ElseIf field_type = "PictureBox" Then
                                        '    If SubCtrl.ImageLocation <> Nothing Then
                                        '        value_list(tabIndex - 1) += "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.ImageLocation + "', SINGLE_BLOB)as temp_picture) "
                                        '    ElseIf (SubCtrl.ImageLocation = Nothing And SubCtrl.Enabled = True) Then
                                        '        Dim imageConverter As New ImageConverter()
                                        '        Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(SubCtrl.Image, GetType(Byte())), Byte())
                                        '        value_list(tabIndex - 1) += "@" + prev_ctrl.Text + ""
                                        '        Main_Form.sql_parameter.Parameters.AddWithValue("@" + prev_ctrl.Text, imageByte)
                                        '    Else
                                        '        value_list(tabIndex - 1) += "NULL"
                                        '    End If

                                        'ElseIf field_type = "TextBox" And SubCtrl.Name.ToString.Contains("_file") Then
                                        '    If SubCtrl.Text <> Nothing Then
                                        '        value_list(tabIndex - 1) += "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.Text + "', SINGLE_BLOB)as temp_picture) "
                                        '    Else
                                        '        value_list(tabIndex - 1) += "NULL"
                                        '    End If
                                    Else 'String - textbox,combobox

                                        For i = 0 To fn_getsubformarray(tabIndex - 1).Length / 29
                                            If fn_getsubformarray(tabIndex - 1)(1, i) = SubCtrl.Name Then
                                                For Each row As DataGridViewRow In dataGridView.Rows
                                                    If row.Cells.Item(CType(CType(SubCtrl, Control).Name.Replace("_field", ""), Int32) - 1).Value.ToString.Length = 0 Then
                                                        subValue_list(row.Index) += "NULL"
                                                    ElseIf fn_getsubformarray(tabIndex - 1)(26, i) = "char" Or fn_getsubformarray(tabIndex - 1)(26, i) = "nchar" Or fn_getsubformarray(tabIndex - 1)(26, i) = "ntext" Or fn_getsubformarray(tabIndex - 1)(26, i) = "nvarchar" Or fn_getsubformarray(tabIndex - 1)(26, i) = "text" Or fn_getsubformarray(tabIndex - 1)(26, i) = "varchar" Then
                                                        subValue_list(row.Index) += "'" + row.Cells.Item(CType(CType(SubCtrl, Control).Name.Replace("_field", ""), Int32) - 1).Value.ToString.Replace("'", "''") + "'"
                                                    Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                                        subValue_list(row.Index) += row.Cells.Item(CType(CType(SubCtrl, Control).Name.Replace("_field", ""), Int32) - 1).Value.ToString.Replace(",", ".")
                                                    End If
                                                Next
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    '  End If
                                Case Else
                                    prev_ctrl = SubCtrl
                            End Select
                            line_no += 1
                        Next SubCtrl
                    Catch ex As Exception

                    End Try

                Next

                If subValue_list.Length > 0 Then
                    For Each line In subValue_list
                        line = "(" & line & ",'" + fn_search_substitution("sub[user_name]") + "',GETDATE()),"
                        value_list(tabIndex - 1) += line
                    Next
                    value_list(tabIndex - 1) = value_list(tabIndex - 1).Remove(value_list(tabIndex - 1).Length - 1, 1)
                End If

                Dim insertCmd = Nothing
                If subValue_list.Length > 0 Then
                    insertCmd = int_query(tabIndex - 1) + fld_list(tabIndex - 1) + ",[creator],[created]" + value_join + value_list(tabIndex - 1)
                End If

                If fn_sql_request(deleteJoin(tabIndex - 1) & insertCmd, If(insertCmd Is Nothing, "DELETE", "INSERT"), fn_search_substitution("sub[user_subdataview_db_type_" & (tabIndex - 1) & "]"), False, False, Main_Form.sql_parameter, False, False) = False Then
                    fn_db_operations_with_user_subrecs = False
                    MessageBox.Show(fn_translate("subrec_not_be_saved"))
                Else
                    'run subform after SQL transaction
                    If fn_search_substitution("sub[subform_after_sql_command_" & (tabIndex - 1) & "]").Length > 0 Then
                        fn_run_sql_transaction(fn_search_substitution("sub[subform_after_sql_command_" & (tabIndex - 1) & "]"), CType(fn_search_substitution("sub[subform_after_sql_local_" & (tabIndex - 1) & "]"), Boolean))
                    End If
                End If
            End If
        Next
    End Function



    Function fn_update_selected_user_rec() As Boolean
        fn_update_selected_user_rec = False

        Dim int_query As String = "UPDATE " + fn_search_substitution("sub[user_dataview_table]") + " SET "
        Dim field_type As String
        Dim prev_ctrl As Control
        Dim next_val As Boolean = False
        Dim line_no As Integer = 1

        For Each Ctrl In Main_Form.tc_user_document.TabPages.Item(0).Controls '.OfType(Of Panel)()
            Try
                For Each SubCtrl In Ctrl.Controls

                    field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")

                    Select Case field_type
                        Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"

                            If SubCtrl.tag = True Then
                                If next_val Then int_query += ","

                                If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                    int_query += " [" + fn_sys_translate(prev_ctrl.Text) + "]="
                                    next_val = True
                                Else
                                    int_query += " [" + prev_ctrl.Text + "]="
                                    next_val = True
                                End If


                                If field_type = "DateTimePicker" Then
                                    If Main_Form.user_form_field_list(17, line_no).Length > 0 Then
                                        int_query += "'" + Format(SubCtrl.value, Main_Form.user_form_field_list(17, line_no).ToString) + "'"
                                    Else
                                        int_query += "'" + Format(SubCtrl.value.date, "yyyy-MM-dd") + "'"
                                    End If

                                ElseIf field_type = "CheckBox" Then
                                    int_query += "'" + SubCtrl.Checked.ToString + "'"
                                ElseIf field_type = "PictureBox" Then

                                    If SubCtrl.ImageLocation <> Nothing Then
                                        int_query += "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.ImageLocation + "', SINGLE_BLOB)as temp_picture) "
                                    ElseIf (SubCtrl.ImageLocation = Nothing And SubCtrl.enabled = True) Then
                                        If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                            Dim imageConverter As New ImageConverter()
                                            Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(SubCtrl.Image, GetType(Byte())), Byte())
                                            int_query &= "@" + prev_ctrl.Text & ""
                                            Main_Form.sql_parameter.Parameters.AddWithValue("@" & prev_ctrl.Text, imageByte)
                                        Else
                                            int_query = int_query.Replace(", [" & prev_ctrl.Text & "]=", "")
                                        End If
                                    Else
                                        int_query &= "NULL"
                                    End If
                                ElseIf field_type = "TextBox" AndAlso SubCtrl.name.ToString.Contains("_file") Then
                                    If SubCtrl.text <> Nothing Then
                                        int_query &= "(SELECT * FROM OPENROWSET(BULK N'" + SubCtrl.text + "', SINGLE_BLOB)as temp_picture) "
                                    Else
                                        int_query &= "NULL"
                                    End If
                                Else 'String - textbox,combobox
                                    For i = 0 To Main_Form.user_form_field_list.Length / 29
                                        If Main_Form.user_form_field_list(1, i) = SubCtrl.name Then
                                            If SubCtrl.text.length = 0 Then
                                                int_query &= "NULL"
                                            ElseIf Main_Form.user_form_field_list(26, i) = "char" OrElse Main_Form.user_form_field_list(26, i) = "nchar" OrElse Main_Form.user_form_field_list(26, i) = "ntext" OrElse Main_Form.user_form_field_list(26, i) = "nvarchar" OrElse Main_Form.user_form_field_list(26, i) = "text" OrElse Main_Form.user_form_field_list(26, i) = "varchar" Then
                                                int_query &= "'" & Replace(SubCtrl.text, "'", "''") & "'"
                                            Else '"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                                int_query &= SubCtrl.text.replace(",", ".")
                                            End If
                                            Exit For
                                        End If



                                    Next
                                End If
                            End If
                        Case Else
                            prev_ctrl = SubCtrl
                    End Select
                    line_no += 1
                Next SubCtrl
            Catch ex As Exception
            End Try
        Next

        If fn_sql_request(int_query + ",[creator] ='" + fn_search_substitution("sub[user_name]") + "',[created]=GETDATE() WHERE " + fn_prepare_user_where_command(), "UPDATE", fn_search_substitution("sub[user_dataview_db_type]"), False, False, Main_Form.sql_parameter, False, False) = False Then
            MessageBox.Show(fn_translate("rec_not_be_saved"))
        Else
            ' save after transaction
            'If fn_search_substitution("sub[after_sql_command]").Length > 0 Then
            '    fn_run_sql_transaction(fn_search_substitution("sub[after_sql_command]"), CType(fn_search_substitution("sub[after_sql_local]"), Boolean))
            'End If

            If fn_db_operations_with_user_subrecs(False) Then
                fn_clear_user_form()
                Main_Form.tc_data.SelectedIndex = 0
                fn_load_basic_form("")
                Main_Form.btn_main_btn_1.Enabled = True
                fn_update_selected_user_rec = True
            End If
        End If

    End Function


    Function fn_delete_from_user_subrec_datagrid(ByVal subFormIndex As Integer, ByVal refresh As Boolean) As Boolean
        Dim dataGridView As DataGridView
        Try
            For Each ctrl As DataGridView In Main_Form.tc_user_document.TabPages.Item(subFormIndex).Controls.OfType(Of DataGridView)()
                dataGridView = ctrl
            Next

            dataGridView.Rows.RemoveAt(dataGridView.CurrentRow.Index)

            If refresh Then
                dataGridView.CurrentRow.Selected = False
                Dim e As DataGridViewCellEventArgs
                Main_Form.sub_dgv_CellClick(dataGridView, e)
            End If

            fn_delete_from_user_subrec_datagrid = True
        Catch ex As Exception
            fn_delete_from_user_subrec_datagrid = False
        End Try
    End Function


    Function fn_clear_user_form() As Boolean 'use after change correct tab index
        fn_clear_user_form = False
        Dim field_type As String
        For Each Ctrl In Main_Form.tc_user_document.TabPages.Item(0).Controls.OfType(Of Panel)()
            For Each SubCtrl In Ctrl.Controls
                Try
                    field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                    Select Case field_type
                        Case "CheckBox"
                            SubCtrl.checked = False
                        Case "TextBox", "ComboBox"
                            SubCtrl.ResetText()
                        Case "DateTimePicker"
                            SubCtrl.value.date = Now
                        Case "PictureBox"
                            SubCtrl.image = Nothing
                            SubCtrl.ImageLocation = Nothing
                        Case Else
                    End Select
                Catch ex As Exception
                    SubCtrl.ResetText()
                End Try
            Next
        Next
        fn_clear_user_form = True
    End Function





    Function fn_fill_detail_form_with_selected_rec(ByVal iscopy As Boolean) As Boolean  'filling detail form for edit
        fn_fill_detail_form_with_selected_rec = False

        Dim origIdIdex As Int64
        Dim col_name As String = ""
        Dim field_type As String
        Dim prev_ctrl As Control
        Dim idIndex As Long = fn_get_sql_index() 'fn_get_next_gdv_index(Main_Form.dgw_query_view)

        For Each Ctrl In Main_Form.tc_user_document.TabPages.Item(Main_Form.tc_user_document.SelectedIndex).Controls

            For Each SubCtrl In Ctrl.Controls

                field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                Select Case field_type

                    Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"

                        'If SubCtrl.tag = True Then removed for read keys fos subforms
                        If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                            col_name = fn_sys_translate(prev_ctrl.Text)
                        Else
                            col_name = prev_ctrl.Text
                        End If

                        For Each column In Main_Form.dgw_query_view.Columns
                            Try
                                If col_name = column.name Then

                                    ' replace tabpage tag with selected bind value subform key value
                                    For Each subFormCtrl As TabPage In Main_Form.tc_user_document.TabPages
                                        If Not subFormCtrl.Tag Is Nothing Then
                                            If subFormCtrl.Tag.StartsWith(col_name & "|") Then
                                                If Main_Form.dgw_query_view.CurrentRow.Index >= 0 And Not iscopy Then
                                                    subFormCtrl.Tag = subFormCtrl.Tag.ToString.Replace(col_name & "|", col_name & ":" & Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value() & "|")
                                                    'subFormCtrl.Text = subFormCtrl.Tag
                                                Else
                                                    origIdIdex = Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()
                                                    subFormCtrl.Tag = subFormCtrl.Tag.ToString.Replace(col_name & "|", col_name & ":" & idIndex & "|")
                                                    'subFormCtrl.Text = subFormCtrl.Tag

                                                End If
                                            ElseIf subFormCtrl.Tag.StartsWith(col_name & ":") Then
                                                If Main_Form.dgw_query_view.CurrentRow.Index >= 0 And Not iscopy Then
                                                    subFormCtrl.Tag = col_name & ":" & Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value() & "|" & subFormCtrl.Tag.ToString.Split("|")(1)
                                                    'subFormCtrl.Text = subFormCtrl.Tag
                                                Else
                                                    origIdIdex = Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()
                                                    subFormCtrl.Tag = col_name & ":" & idIndex & "|" & subFormCtrl.Tag.ToString.Split("|")(1)
                                                    'subFormCtrl.Text = subFormCtrl.Tag
                                                End If
                                            End If
                                        End If
                                    Next

                                    If field_type = "PictureBox" And SubCtrl.tag Then

                                        SubCtrl.Image = New Bitmap(fn_byteArrayToImage(Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()))
                                        'Dim pictureData As Byte() = DirectCast(Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value(), Byte())
                                        'Dim stream = New IO.MemoryStream(pictureData)
                                        'temp_picture = Image.FromStream(stream)

                                        'Dim tt As String
                                        'For i = 0 To pictureData.Length
                                        '    tt += pictureData(i).ToString
                                        'Next
                                        'MessageBox.Show(tt)

                                        'temp_picture.Save(System.IO.Path.Combine(user_picture_dir, "temp_picture.jpg"))
                                        'SubCtrl.ImageLocation = System.IO.Path.Combine(user_picture_dir, "temp_picture.jpg")
                                        SubCtrl.enabled = True
                                    ElseIf field_type = "CheckBox" And SubCtrl.tag Then
                                        SubCtrl.checked = Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()
                                    ElseIf SubCtrl.tag Then
                                        SubCtrl.text = Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()
                                    ElseIf Not SubCtrl.tag Then
                                        If Main_Form.dgw_query_view.CurrentRow.Index >= 0 And Not iscopy Then
                                            SubCtrl.text = Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()
                                        Else
                                            origIdIdex = Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentRow.Index).Cells(column.index).Value()
                                            SubCtrl.text = idIndex 'set primary main index from copy
                                        End If
                                    End If

                                End If
                            Catch ex As Exception
                            End Try
                        Next

                        'End If

                    Case Else
                        prev_ctrl = SubCtrl
                End Select
            Next
        Next

        'exist subforms
        If Main_Form.tc_user_document.TabPages.Count > 1 Then
            fn_load_subforms(origIdIdex)
        End If

        fn_fill_detail_form_with_selected_rec = True
    End Function



    Function fn_fill_detail_form_with_empty_rec() As Boolean  'filling detail form for new
        fn_fill_detail_form_with_empty_rec = False

        Dim col_name As String = ""
        Dim field_type As String
        Dim prev_ctrl As Control
        Dim idIndex As Long = fn_get_sql_index() 'fn_get_next_gdv_index(Main_Form.dgw_query_view)

        For Each Ctrl In Main_Form.tc_user_document.TabPages.Item(Main_Form.tc_user_document.SelectedIndex).Controls
            For Each SubCtrl In Ctrl.Controls
                field_type = SubCtrl.GetType.ToString.Replace("System.Windows.Forms.", "")
                Select Case field_type
                    Case "TextBox", "ComboBox", "DateTimePicker", "CheckBox", "PictureBox"
                        'If SubCtrl.tag = True Then removed for read keys fos subforms
                        If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                            col_name = fn_sys_translate(prev_ctrl.Text)
                        Else
                            col_name = prev_ctrl.Text
                        End If

                        For Each column In Main_Form.dgw_query_view.Columns
                            Try
                                If col_name = column.name Then
                                    ' replace tabpage tag with selected bind value subform key value
                                    For Each subFormCtrl As TabPage In Main_Form.tc_user_document.TabPages
                                        If Not subFormCtrl.Tag Is Nothing Then
                                            If subFormCtrl.Tag.StartsWith(col_name & "|") Then
                                                subFormCtrl.Tag = subFormCtrl.Tag.ToString.Replace(col_name & "|", col_name & ":" & idIndex & "|")
                                            ElseIf subFormCtrl.Tag.StartsWith(col_name & ":") Then
                                                subFormCtrl.Tag = col_name & ":" & idIndex & "|" & subFormCtrl.Tag.ToString.Split("|")(1)
                                            End If
                                        End If
                                    Next
                                End If
                            Catch ex As Exception
                            End Try
                        Next
                    Case Else
                        prev_ctrl = SubCtrl
                End Select
            Next
        Next

        If Main_Form.dgw_query_view.Rows.Count = 0 Then 'not exist any rows fill for id only
            For Each subFormCtrl As TabPage In Main_Form.tc_user_document.TabPages
                If Not subFormCtrl.Tag Is Nothing Then
                    If subFormCtrl.Tag.StartsWith("id|") Then
                        subFormCtrl.Tag = subFormCtrl.Tag.ToString.Replace("id|", "id:" & idIndex & "|")
                    ElseIf subFormCtrl.Tag.StartsWith("id:") Then
                        subFormCtrl.Tag = "id:" & idIndex & "|" & subFormCtrl.Tag.ToString.Split("|")(1)
                    End If
                End If
            Next
        End If

        'exist subforms
        If Main_Form.tc_user_document.TabPages.Count > 1 Then
            fn_load_subforms(0)
        End If

        fn_fill_detail_form_with_empty_rec = True
    End Function



    Function fn_prepare_user_where_command() As String
        Try
            Dim jumpedFields = 0
            Dim column_list As String() = primary_key_columns.Split(",")
            Dim SQL_Where As String = ""
            'Dim key_info As String = ""
            For i = 0 To column_list.Count - 2

                'jumping sys_Attachmens column
                If Main_Form.dgw_query_view.Columns((CInt(column_list(i))) - 1).Name.ToString = "sys_Attachment" Then jumpedFields = 1

                If i > 0 Then SQL_Where &= " AND "
                If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                    SQL_Where &= fn_sys_translate(Main_Form.dgw_query_view.Columns((CInt(column_list(i))) - 1 + jumpedFields).Name.ToString) + " = "
                Else
                    SQL_Where &= Main_Form.dgw_query_view.Columns((CInt(column_list(i))) - 1 + jumpedFields).Name.ToString + " = "
                End If


                ' key_info &= Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentCell.RowIndex).Cells((CInt(column_list(i + jumpedFields))) - 1).Value().ToString + ", "
                Select Case Main_Form.dgw_query_view.Columns((CInt(column_list(i))) - 1 + jumpedFields).ValueType.Name.ToString
                    Case "Int32", "Decimal", "Double", "Int64"
                        SQL_Where &= " " + Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentCell.RowIndex).Cells((CInt(column_list(i))) - 1 + jumpedFields).Value().ToString.Replace(",", ".") + " "
                    Case "String", "Byte[]", "TimeSpan", "DateTime"
                        SQL_Where &= " '" + Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentCell.RowIndex).Cells((CInt(column_list(i))) - 1 + jumpedFields).Value().ToString + "' "
                    Case "Boolean", "Byte"
                        SQL_Where &= " " + CBool(Main_Form.dgw_query_view.Rows(Main_Form.dgw_query_view.CurrentCell.RowIndex).Cells((CInt(column_list(i))) - 1 + jumpedFields).Value().ToString) + " "
                    Case Else
                End Select
            Next
            fn_prepare_user_where_command = SQL_Where
        Catch ex As Exception
            fn_prepare_user_where_command = ""
        End Try
    End Function



    Function fn_reload_favorite_menu(ByVal expand As Boolean) As Boolean
        Main_Form.btn_add_favorite_item.Enabled = False
        Main_Form.btn_del_favorite.Enabled = False
        Try
            My.Forms.Main_Form.tv_favorites_menu.Nodes.Clear()
            fn_sql_request("SELECT fml.id,fml.menu_name,fml.enabled,fml.released,fml.enable_translate FROM [dbo].[favorite_menu_list] fml WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR fml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.[right] = fml.right_menu)) ORDER BY fml.position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)
            For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                If (My.Forms.Main_Form.sql_array(i, 2) = True And My.Forms.Main_Form.sql_array(i, 3) = True) Or Main_Form.system_account = True Then
                    If Not CBool(Main_Form.sql_array(i, 4)) Then
                        My.Forms.Main_Form.tv_favorites_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(My.Forms.Main_Form.sql_array(i, 1)))
                    Else
                        My.Forms.Main_Form.tv_favorites_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                    End If
                End If
            Next
            For Each node As TreeNode In My.Forms.Main_Form.tv_favorites_menu.Nodes
                If fn_sql_request("SELECT [id],[favorite_name],[position],[enabled],[released],[enable_translate],[form_id] FROM [dbo].[favorite_form_list] WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND group_id = " + node.Name.ToString + " ORDER BY position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                        If (My.Forms.Main_Form.sql_array(i, 3) = True And My.Forms.Main_Form.sql_array(i, 4) = True) Or Main_Form.system_account = True Then
                            If Not CBool(Main_Form.sql_array(i, 5)) Then
                                My.Forms.Main_Form.tv_favorites_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + "_" + My.Forms.Main_Form.sql_array(i, 6), My.Forms.Main_Form.sql_array(i, 1))
                            Else
                                My.Forms.Main_Form.tv_favorites_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + "_" + My.Forms.Main_Form.sql_array(i, 6), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                            End If
                        End If
                    Next
                End If
            Next
            If expand Then My.Forms.Main_Form.tv_favorites_menu.ExpandAll()

            'fn_cursor_waiting(False)
            Return True
        Catch ex As Exception
            'fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("favorite_menu_cannot_be_loaded") & vbNewLine & ex.Message)
            Return False
        End Try
    End Function


    Function fn_reload_exp_imp_menu(ByVal export_enabled As Boolean, ByVal import_enabled As Boolean) As Boolean
        Try
            My.Forms.Main_Form.tv_export_import_menu.Nodes.Clear()
            fn_sql_request("SELECT eiml.id,eiml.menu_name,eiml.enabled,eiml.released,eiml.enable_translate FROM [dbo].[exp_imp_menu_list] eiml WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR eiml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) ORDER BY eiml.position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)
            For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                If (My.Forms.Main_Form.sql_array(i, 2) = True And My.Forms.Main_Form.sql_array(i, 3) = True) Or Main_Form.system_account = True Then
                    If Not CBool(Main_Form.sql_array(i, 4)) Then
                        My.Forms.Main_Form.tv_export_import_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(My.Forms.Main_Form.sql_array(i, 1)))
                    Else
                        My.Forms.Main_Form.tv_export_import_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                    End If
                End If
            Next
            For Each node As TreeNode In My.Forms.Main_Form.tv_export_import_menu.Nodes
                If fn_sql_request("SELECT [id],[expimp_name],[position],[enabled],[released],[enable_translate],[function_name],[command_type] FROM [dbo].[exp_imp_funct_list] WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND group_id = " + node.Name.ToString + " ORDER BY position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                        If (My.Forms.Main_Form.sql_array(i, 3) = True And My.Forms.Main_Form.sql_array(i, 4) = True) Or Main_Form.system_account = True Then
                            If (Main_Form.sql_array(i, 7) = "E" And export_enabled = True) Or (Main_Form.sql_array(i, 7) = "I" And import_enabled = True) Then
                                If Not CBool(Main_Form.sql_array(i, 5)) Then
                                    My.Forms.Main_Form.tv_export_import_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + "/" + My.Forms.Main_Form.sql_array(i, 6), My.Forms.Main_Form.sql_array(i, 1))
                                Else
                                    My.Forms.Main_Form.tv_export_import_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + "/" + My.Forms.Main_Form.sql_array(i, 6), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                End If
                            End If
                        End If
                    Next
                End If
            Next
            'fn_cursor_waiting(False)
            Return True
        Catch ex As Exception
            'fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("exp_imp_menu_cannot_be_loaded") + vbNewLine + ex.Message)
            Return False
        End Try
    End Function









    Function fn_reload_report_menu(ByVal enable_items As Boolean, ByVal expand As Boolean) As Boolean
        Try
            Dim select_via_data_binding As String
            If Not enable_items Then select_via_data_binding = " AND (DATALENGTH(data_binding) IS NULL or DATALENGTH(data_binding) =0) "

            My.Forms.Main_Form.tv_report_menu.Nodes.Clear()
            If fn_load_sql_addon("SELECT rml.id,rml.menu_name,rml.enabled,rml.released,rml.enable_translate FROM [dbo].[report_menu_list] rml WHERE rml.[form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR rml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.[right] = rml.right_menu)) ORDER BY rml.position ASC", True, "report_list") Then
                For i = 0 To My.Forms.Main_Form.sql_array_addon_count - 1
                    If (My.Forms.Main_Form.sql_array_addon(i, 2) = True And My.Forms.Main_Form.sql_array_addon(i, 3) = True) Or Main_Form.system_account = True Then
                        If Not CBool(Main_Form.sql_array_addon(i, 4)) Then
                            My.Forms.Main_Form.tv_report_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0), UCase(My.Forms.Main_Form.sql_array_addon(i, 1)))
                        Else
                            My.Forms.Main_Form.tv_report_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array_addon(i, 1))))
                        End If
                    End If
                Next
            End If

            For Each node As TreeNode In My.Forms.Main_Form.tv_report_menu.Nodes
                If fn_load_sql_addon("SELECT [id],[report_name],[position],[enabled],[released],[enable_translate],[report_path],[data_binding] FROM [dbo].[report_form_list] WHERE [form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + select_via_data_binding + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND group_id = " + node.Name.ToString + " ORDER BY position ASC", True, "report_list") = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_addon_count - 1
                        If (My.Forms.Main_Form.sql_array_addon(i, 3) = True And My.Forms.Main_Form.sql_array_addon(i, 4) = True) Or Main_Form.system_account = True Then
                            If Not CBool(Main_Form.sql_array_addon(i, 5)) Then
                                My.Forms.Main_Form.tv_report_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 7), My.Forms.Main_Form.sql_array_addon(i, 1))
                            Else
                                My.Forms.Main_Form.tv_report_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 7), fn_translate(My.Forms.Main_Form.sql_array_addon(i, 1)))
                            End If
                        End If
                    Next
                End If
            Next
            If fn_load_sql_addon("SELECT [id],[report_name],[position],[enabled],[released],[enable_translate],[report_path],[data_binding] FROM [dbo].[report_form_list] WHERE [form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + select_via_data_binding + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND group_id = 0 ORDER BY position ASC", True, "report_list") = True Then
                For i = 0 To My.Forms.Main_Form.sql_array_addon_count - 1
                    If (My.Forms.Main_Form.sql_array_addon(i, 3) = True And My.Forms.Main_Form.sql_array_addon(i, 4) = True) Or Main_Form.system_account = True Then
                        If Not CBool(Main_Form.sql_array_addon(i, 5)) Then
                            My.Forms.Main_Form.tv_report_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 7), My.Forms.Main_Form.sql_array_addon(i, 1))
                        Else
                            My.Forms.Main_Form.tv_report_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 7), fn_translate(My.Forms.Main_Form.sql_array_addon(i, 1)))
                        End If
                    End If
                Next
            End If
            Main_Form.btn_report_group_add.Enabled = True
            Main_Form.btn_report_add.Enabled = True
            Main_Form.btn_report_del.Enabled = False

            If expand Then My.Forms.Main_Form.tv_report_menu.ExpandAll()
            'fn_cursor_waiting(False)
            Return True
        Catch ex As Exception
            'fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("report_menu_cannot_be_loaded") & vbNewLine & ex.Message)
            Return False
        End Try
    End Function





    Function fn_reload_print_menu(ByVal enable_items As Boolean, ByVal expand As Boolean) As Boolean
        Try
            Dim select_via_data_binding As String
            If Not enable_items Then select_via_data_binding = " AND (DATALENGTH(data_binding) IS NULL or DATALENGTH(data_binding) =0) "
            fn_insert_substitution("sub[default_print_document]", "")

            My.Forms.Main_Form.tv_print_menu.Nodes.Clear()
            If fn_load_sql_addon("SELECT rml.id,rml.menu_name,rml.enabled,rml.released,rml.enable_translate FROM [dbo].[print_menu_list] rml WHERE rml.[form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR rml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.[right] = rml.right_menu)) ORDER BY rml.position ASC", True, "print doc list") Then
                For i = 0 To My.Forms.Main_Form.sql_array_addon_count - 1
                    If (My.Forms.Main_Form.sql_array_addon(i, 2) = True And My.Forms.Main_Form.sql_array_addon(i, 3) = True) Or Main_Form.system_account = True Then
                        If Not CBool(Main_Form.sql_array_addon(i, 4)) Then
                            My.Forms.Main_Form.tv_print_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0), UCase(My.Forms.Main_Form.sql_array_addon(i, 1)))
                        Else
                            My.Forms.Main_Form.tv_print_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array_addon(i, 1))))
                        End If
                    End If
                Next
            End If

            For Each node As TreeNode In My.Forms.Main_Form.tv_print_menu.Nodes
                If fn_load_sql_addon("SELECT [id],[print_name],[position],[enabled],[released],[enable_translate],[print_path],[default_document],[data_binding] FROM [dbo].[print_form_list] WHERE [form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + select_via_data_binding + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND group_id = " + node.Name.ToString + " ORDER BY position ASC", True, "print_doc_list") = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_addon_count - 1
                        If (My.Forms.Main_Form.sql_array_addon(i, 3) = True And My.Forms.Main_Form.sql_array_addon(i, 4) = True) Or Main_Form.system_account = True Then
                            If Not CBool(Main_Form.sql_array_addon(i, 5)) Then
                                My.Forms.Main_Form.tv_print_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 8), My.Forms.Main_Form.sql_array_addon(i, 1))
                            Else
                                My.Forms.Main_Form.tv_print_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 8), fn_translate(My.Forms.Main_Form.sql_array_addon(i, 1)))
                            End If
                            If CBool(Main_Form.sql_array_addon(i, 7)) Then
                                fn_insert_substitution("sub[default_print_document]", Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 8))
                            End If
                        End If
                    Next
                End If
            Next

            If fn_load_sql_addon("SELECT [id],[print_name],[position],[enabled],[released],[enable_translate],[print_path],[default_document],[data_binding] FROM [dbo].[print_form_list] WHERE [form_id]=" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + select_via_data_binding + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND group_id = 0 ORDER BY position ASC", True, "print_doc_list") = True Then
                For i = 0 To My.Forms.Main_Form.sql_array_addon_count - 1
                    If (My.Forms.Main_Form.sql_array_addon(i, 3) = True AndAlso My.Forms.Main_Form.sql_array_addon(i, 4) = True) Or Main_Form.system_account = True Then
                        If Not CBool(Main_Form.sql_array_addon(i, 5)) Then
                            My.Forms.Main_Form.tv_print_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 8), My.Forms.Main_Form.sql_array_addon(i, 1))
                        Else
                            My.Forms.Main_Form.tv_print_menu.Nodes.Add(My.Forms.Main_Form.sql_array_addon(i, 0) + "_" + My.Forms.Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 8), fn_translate(My.Forms.Main_Form.sql_array_addon(i, 1)))
                        End If
                        If CBool(Main_Form.sql_array_addon(i, 7)) Then
                            fn_insert_substitution("sub[default_print_document]", Main_Form.sql_array_addon(i, 6) + "*" + My.Forms.Main_Form.sql_array_addon(i, 8))
                        End If
                    End If
                Next
            End If

            If fn_search_substitution("sub[default_print_document]").Length > 0 Then
                Main_Form.btn_default_print.Enabled = True
            Else
                Main_Form.btn_default_print.Enabled = False
            End If

            Main_Form.btn_print_group_add.Enabled = True
            Main_Form.btn_print_add.Enabled = True
            Main_Form.btn_print_del.Enabled = False

            If expand Then My.Forms.Main_Form.tv_print_menu.ExpandAll()
            'fn_cursor_waiting(False)
            Return True
        Catch ex As Exception
            'fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("print_menu_cannot_be_loaded") & vbNewLine & ex.Message)
            Return False
        End Try
    End Function


    Function fn_user_order_by_set(ByVal column_index As Integer)
        If user_order_by = Nothing Then
            user_order_by = "[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] ASC"
        ElseIf user_order_by.Contains("[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] ASC") Then
            user_order_by = user_order_by.Replace("[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] ASC", "[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC")
        ElseIf user_order_by.Contains("[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC") Then
            If user_order_by.Contains(",[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC") Then
                user_order_by = user_order_by.Replace(",[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC", "")
            ElseIf user_order_by.Contains("[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC,") Then
                user_order_by = user_order_by.Replace("[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC,", "")
            Else
                user_order_by = user_order_by.Replace("[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] DESC", "")
            End If
        Else
            user_order_by += ",[" & Main_Form.dgw_query_view.Columns.Item(column_index).Name & "] ASC"
        End If
        fn_load_basic_form("")
    End Function

    Function fn_reset_sort_mainForm()
        user_order_by = ""
        fn_load_basic_form("")
    End Function

End Module
