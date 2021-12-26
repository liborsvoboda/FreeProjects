Imports SWAPP_Builder.Classes

Module Functions_dev_builder

    Private object_part = 1




    Function fn_load_sql_preview(ByVal query As String) As Boolean
        My.Forms.Main_Form.dgv_dev_sql_preview.DataSource = ""
        fn_load_sql_preview = False
        Dim db_type As String = "DB"
        If Main_Form.chb_def_localdb.Checked = True Then db_type = "local"

        If fn_sql_request(query, "SELECT", db_type, False, False, Main_Form.sql_parameter, False, False) = True Then

            My.Forms.Main_Form.dgv_dev_sql_preview.AutoGenerateColumns = True
            If Main_Form.chb_dev_format_debug.Checked = True Then
                My.Forms.Main_Form.dgv_dev_sql_preview.DataSource = dgw_table_schema_sql_preview
            Else
                My.Forms.Main_Form.dgv_dev_sql_preview.DataSource = dgw_source_sql_preview
            End If

            fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_list WHERE id=" + Main_Form.lbl_dev_form_id.Text, "LOCAL", False)

            If Main_Form.tp_dev_detail.Controls.OfType(Of Panel)().Count = 0 And Main_Form.chb_dev_format_debug.Checked = True And Main_Form.lbl_dev_form_id.Text.Length > 0 Then Main_Form.btn_dev_create_update_form.Enabled = True
            fn_load_sql_preview = True
        Else

            fn_sql_check_button("", "", True)

            Main_Form.btn_dev_create_update_form.Enabled = False
            If query_correct = True Then
                'MessageBox.Show(fn_translate("nodata_show_stucture"))
                'message not exist data
                My.Forms.Main_Form.dgv_dev_sql_preview.DataSource = dgw_table_schema_sql_preview
                fn_load_sql_preview = True
            Else
                fn_load_sql_preview = False
            End If
        End If

    End Function





    Function fn_check_dev_sql_preview() As Boolean
        If Main_Form.txt_dev_form_name.Text.Length > 0 And Main_Form.btn_add_menu.Enabled = True Then
            Main_Form.txt_dev_sql_command.Enabled = True
        Else
            Main_Form.btn_dev_sql_clear.Enabled = False
            Main_Form.txt_dev_sql_command.Enabled = False
            Main_Form.btn_dev_show_preview.Enabled = False
            Main_Form.chb_dev_format_debug.Enabled = False
            Main_Form.btn_dev_create_update_form.Enabled = False
        End If

    End Function



    Function fn_dev_create_form() As Boolean
        Try

            Dim New_panel, panel_reaction As Panel
            Dim text_reaction As TextBox
            Dim button_reaction As Button
            Dim label_reaction As Label
            Dim picture_reaction As PictureBox

            Dim New_Field
            Dim column_pos = 0, lenght = 0

            For Each row As DataGridViewRow In Main_Form.dgv_dev_sql_preview.Rows
                lenght = 0

                New_panel = New Panel
                New_panel.TabIndex = row.Index * 10
                New_panel.Name = (row.Index + 1)
                'MessageBox.Show(col.ValueType.Name)

                If {"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint", "bit", "binary", "varbinary", "date", "datetime", "datetime2", "datetimeoffset", "time", "timestamp", "char", "nchar", "ntext", "nvarchar", "text", "varchar", "image", "xml", "uniqueidentifier"}.Contains(row.Cells.Item(24).Value) Then
                    'new field unique_key
                    New_Field = New PictureBox
                    New_Field.Name = (row.Index + 1).ToString + "_key"
                    New_Field.size = New Drawing.Size(25, 20)
                    New_Field.Location = New Point(lenght + 2, 5)
                    New_Field.backcolor = dev_transparent
                    New_Field.Cursor = Cursors.Hand
                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                    lenght = lenght + 25

                    If row.Cells.Item(18).Value = True Then
                        New_Field.image = New Bitmap(My.Resources.key_on)
                        New_Field.image.tag = True
                    Else
                        New_Field.image = New Bitmap(My.Resources.key_off)
                        New_Field.image.tag = False
                    End If

                    picture_reaction = DirectCast(New_Field, PictureBox)
                    AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_change_key
                    New_panel.Controls.Add(New_Field)
                End If

                New_Field = New PictureBox
                New_Field.Name = (row.Index + 1).ToString + "_dbsave"
                New_Field.size = New Drawing.Size(25, 20)
                New_Field.Location = New Point(lenght + 2, 5)
                New_Field.backcolor = dev_transparent
                New_Field.Cursor = Cursors.Hand
                New_Field.sizemode = PictureBoxSizeMode.StretchImage
                lenght = lenght + 30
                If row.Cells.Item(22).Value = True Or {"uniqueidentifier"}.Contains(row.Cells.Item(24).Value) Then 'isreadonly in DB
                    New_Field.image = New Bitmap(My.Resources.db_commit_off)
                    New_Field.Enabled = False
                    New_Field.image.tag = False
                Else
                    New_Field.image = New Bitmap(My.Resources.db_commit_on)
                    New_Field.Enabled = True
                    New_Field.image.tag = True
                End If

                picture_reaction = DirectCast(New_Field, PictureBox)
                New_Field.Tag = row.Cells.Item(21).Value 'isdblong

                AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_save_to_db
                New_panel.Controls.Add(New_Field)


                'new field move icon
                New_Field = New PictureBox
                New_Field.Name = (row.Index + 1).ToString + "_move"
                New_Field.size = New Drawing.Size(25, 20)
                New_Field.Location = New Point(lenght + 2, 5)
                New_Field.backcolor = dev_transparent
                New_Field.Cursor = Cursors.Hand
                New_Field.sizemode = PictureBoxSizeMode.StretchImage
                lenght = lenght + 30
                New_Field.image = New Bitmap(My.Resources.move)
                picture_reaction = DirectCast(New_Field, PictureBox)

                New_Field.Tag = row.Cells.Item(13).Value 'allow null

                AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_moveclick

                New_panel.Controls.Add(New_Field)

                'new fields label
                New_Field = New Label
                New_Field.Name = (row.Index + 1).ToString + "_label"
                New_Field.text = row.Cells.Item(0).Value
                New_Field.size = New Drawing.Size(120, 20)
                New_Field.Location = New Point(lenght, 5)
                New_Field.BackColor = dev_transparent
                New_Field.Cursor = Cursors.Default
                lenght = lenght + 130
                label_reaction = DirectCast(New_Field, Label)
                New_panel.Controls.Add(New_Field)

                'new 
                'MessageBox.Show(col.ValueType.Name)
                'MessageBox.Show(dgw_table_schema.Rows(col.Index).Item(22).ToString)  'get table columns property set Item is position from structure
                Select Case row.Cells.Item(24).Value
                    Case "int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                        New_Field = New TextBox
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = "" 'fn_translate("input_field")
                        New_Field.size = New Drawing.Size(150, 20)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 155
                        text_reaction = DirectCast(New_Field, TextBox)
                        AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit
                        New_panel.Controls.Add(New_Field)

                    Case "char", "nchar", "ntext", "nvarchar", "text", "varchar"
                        New_Field = New TextBox
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = "" 'fn_translate("input_field")
                        New_Field.size = New Drawing.Size(150, 20)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 155
                        text_reaction = DirectCast(New_Field, TextBox)
                        New_panel.Controls.Add(New_Field)


                    Case "uniqueidentifier"
                        New_Field = New TextBox
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = "" 'fn_translate("input_field")
                        New_Field.size = New Drawing.Size(150, 20)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 155
                        New_Field.enabled = False
                        text_reaction = DirectCast(New_Field, TextBox)
                        New_panel.Controls.Add(New_Field)

                    Case "bit"
                        New_Field = New CheckBox
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = ""
                        New_Field.size = New Drawing.Size(13, 13)
                        New_Field.Location = New Point(lenght, 5)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 25
                        New_panel.Controls.Add(New_Field)

                    Case "date"
                        New_Field = New DateTimePicker
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = ""
                        New_Field.Format = DateTimePickerFormat.Short
                        New_Field.size = New Drawing.Size(150, 25)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 155
                        New_panel.Controls.Add(New_Field)

                    Case "time"
                        New_Field = New DateTimePicker
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = ""
                        New_Field.Format = DateTimePickerFormat.Time
                        New_Field.size = New Drawing.Size(150, 25)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 155
                        New_panel.Controls.Add(New_Field)

                    Case "datetime", "datetime2", "datetimeoffset", "timestamp"
                        New_Field = New DateTimePicker
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        New_Field.text = ""
                        'New_Field.Format = DateTimePickerFormat.Short
                        New_Field.size = New Drawing.Size(150, 25)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 155
                        New_panel.Controls.Add(New_Field)

                    Case "image"
                        New_Field = New PictureBox
                        New_Field.Name = (row.Index + 1).ToString + "_field"
                        'New_Field.Image = ""
                        New_Field.SizeMode = PictureBoxSizeMode.StretchImage
                        New_Field.size = New Drawing.Size(25, 25)
                        New_Field.Location = New Point(lenght, 2)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 30
                        New_panel.Controls.Add(New_Field)

                    Case "binary", "varbinary", "xml"
                        New_Field = New TextBox
                        New_Field.Name = (row.Index + 1).ToString + "_file"
                        New_Field.text = ""
                        New_Field.size = New Drawing.Size(100, 25)
                        New_Field.Location = New Point(lenght, 4)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 105
                        text_reaction = DirectCast(New_Field, TextBox)
                        New_panel.Controls.Add(New_Field)

                        New_Field = New Button
                        New_Field.Name = (row.Index + 1).ToString + "_selbtn"
                        New_Field.text = fn_translate("Select_file")
                        New_Field.size = New Drawing.Size(80, 25)
                        New_Field.Location = New Point(lenght, 3)
                        New_Field.BackColor = dev_backcolor
                        New_Field.Cursor = Cursors.Hand
                        lenght = lenght + 85
                        button_reaction = DirectCast(New_Field, Button)
                        AddHandler button_reaction.Click, AddressOf Main_Form.react_openfiledialog
                        New_panel.Controls.Add(New_Field)
                    Case Else

                End Select


                'new field note
                New_Field = New Label
                New_Field.Name = (row.Index + 1).ToString + "_note"
                New_Field.text = fn_translate("note")
                New_Field.size = New Drawing.Size(150, 20)
                New_Field.Location = New Point(lenght, 5)
                New_Field.backcolor = dev_transparent
                New_Field.visible = False
                New_Field.Cursor = Cursors.Default
                'lenght = lenght + 160
                label_reaction = DirectCast(New_Field, Label)
                New_panel.Controls.Add(New_Field)


                'new field edit icon
                New_Field = New PictureBox
                New_Field.Name = (row.Index + 1).ToString + "_edit"
                New_Field.size = New Drawing.Size(20, 25)
                New_Field.Location = New Point(lenght, 2)
                New_Field.backcolor = dev_transparent
                New_Field.Cursor = Cursors.Hand
                New_Field.sizemode = PictureBoxSizeMode.StretchImage
                lenght = lenght + 20
                New_Field.image = New Bitmap(My.Resources.edit)
                picture_reaction = DirectCast(New_Field, PictureBox)
                AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_editclick
                New_panel.Controls.Add(New_Field)

                'new field delete icon
                New_Field = New PictureBox
                New_Field.Name = (row.Index + 1).ToString + "_delete"
                New_Field.size = New Drawing.Size(20, 18)
                New_Field.Location = New Point(lenght, 6)
                New_Field.backcolor = dev_transparent
                New_Field.Cursor = Cursors.Hand
                New_Field.sizemode = PictureBoxSizeMode.StretchImage
                lenght = lenght + 20
                New_Field.image = New Bitmap(My.Resources.button_cancel)
                picture_reaction = DirectCast(New_Field, PictureBox)
                AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_deleteclick
                New_panel.Controls.Add(New_Field)


                'final panel size AND position calculating
                'If Main_Form.tp_dev_detail.Size.Width > 2 * (lenght) Then
                '    If ((row.Index + 1) / 2) = Math.Round(((row.Index + 1) / 2), 0) Then
                '        New_panel.Size = New Drawing.Size(lenght, 30)
                '        New_panel.Location = New Point(10 + (column_pos) + 10, 10 + (30 * Math.Floor(row.Index / 2)) + (2 * Math.Floor(row.Index / 2)))
                '        column_pos = 0
                '    Else
                '        New_panel.Size = New Drawing.Size(lenght, 30)
                '        New_panel.Location = New Point(10, 10 + (30 * Math.Floor(row.Index / 2)) + (2 * Math.Floor(row.Index / 2)))
                '        column_pos = lenght
                '    End If
                'Else
                New_panel.Size = New Drawing.Size(lenght, 30)
                New_panel.Location = New Point(10, 10 + (30 * row.Index) + (2 * row.Index))
                'End If


                If row.Cells.Item(13).Value = False Then
                    New_panel.BackColor = not_null_backcolor
                Else
                    ' New_panel.BackColor = dev_backcolor
                End If

                New_panel.Cursor = Cursors.Default
                panel_reaction = DirectCast(New_panel, Panel)

                Main_Form.tp_dev_detail.Controls.Add(New_panel)
            Next

            fn_dev_form_field_array_from_objects()


            Main_Form.tc_dev_menu.SelectedIndex = 2
        Catch ex As Exception

        End Try

    End Function



    Function fn_dev_form_field_array_from_sql() As Boolean
        fn_dev_form_field_array_from_sql = False

        ReDim Main_Form.dev_form_field_list(29, Main_Form.sql_array_count + 1)
        For rec = 0 To Main_Form.sql_array_count
            Main_Form.dev_form_field_list(0, rec + 1) = Main_Form.sql_array(rec, 2) * 10
            For i = 0 To 29


                If i >= 11 And i <= 16 Then
                    If Main_Form.sql_array(rec, i + 3) = "" Then
                        Main_Form.dev_form_field_list(i, rec + 1) = False
                    Else
                        Main_Form.dev_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                    End If
                ElseIf i = 24 Then 'isreadonly 
                    Main_Form.dev_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 8)
                ElseIf i >= 25 Then 'savetodb,isdblong,allow_null,subsql_local
                    Main_Form.dev_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 10)
                    'ElseIf i = 26 Then
                    '    Main_Form.dev_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, 25)
                Else
                    Main_Form.dev_form_field_list(i, rec + 1) = Main_Form.sql_array(rec, i + 3)
                End If
            Next
        Next
        fn_dev_form_field_array_from_sql = True
    End Function




    Function fn_dev_form_field_array_from_objects()
        temp_string = "no"
        ReDim Main_Form.dev_form_field_list(29, 1 + (Main_Form.tp_dev_detail.Controls.OfType(Of Panel)().Count) * 3)
        temp_integer = 1
        For Each Ctrl In Main_Form.tp_dev_detail.Controls.OfType(Of Panel)()
            Try
                If Ctrl.GetType.Name = "Panel" Then 'And Ctrl.TabIndex.ToString <> "" Then

                    For Each Ctrl1 In Ctrl.Controls
                        temp_string = "yes"
                        Main_Form.dev_form_field_list(0, temp_integer) = Ctrl.TabIndex
                        If Ctrl1.name.ToString.Contains("_label") Then
                            Main_Form.dev_form_field_list(1, temp_integer) = Ctrl1.name.ToString '"label"
                        ElseIf Ctrl1.name.ToString.Contains("_field") Then
                            Main_Form.dev_form_field_list(1, temp_integer) = Ctrl1.name.ToString ' "field"
                        ElseIf Ctrl1.name.ToString.Contains("_file") Then
                            Main_Form.dev_form_field_list(1, temp_integer) = Ctrl1.name.ToString ' "file"
                        ElseIf Ctrl1.name.ToString.Contains("_note") Then
                            Main_Form.dev_form_field_list(1, temp_integer) = Ctrl1.name.ToString ' "note"
                        ElseIf Ctrl1.name.ToString.Contains("_key") Then
                            If Ctrl1.image.tag = True Then 'primary key
                                Main_Form.dev_form_field_list(23, temp_integer) = 1
                            Else
                                Main_Form.dev_form_field_list(23, temp_integer) = 0
                            End If
                            temp_string = "no"
                        ElseIf Ctrl1.name.ToString.Contains("_dbsave") Then
                            If Ctrl1.Enabled = True Then 'isreadonly
                                Main_Form.dev_form_field_list(24, temp_integer + 1) = False
                            Else
                                Main_Form.dev_form_field_list(24, temp_integer + 1) = True
                            End If
                            If Ctrl1.image.tag = True Then 'dbsave
                                Main_Form.dev_form_field_list(25, temp_integer + 1) = True
                            Else
                                Main_Form.dev_form_field_list(25, temp_integer + 1) = False
                            End If
                            If Ctrl1.tag = True Then 'isdblong
                                Main_Form.dev_form_field_list(26, temp_integer + 1) = True
                            Else
                                Main_Form.dev_form_field_list(26, temp_integer + 1) = False
                            End If
                            temp_string = "no"
                        ElseIf Ctrl1.name.ToString.Contains("_move") Then 'allow null
                            If Ctrl1.tag = True Then
                                Main_Form.dev_form_field_list(27, temp_integer + 1) = True
                            Else
                                Main_Form.dev_form_field_list(27, temp_integer + 1) = False
                            End If
                            temp_string = "no"
                        Else
                            temp_string = "no"
                        End If

                        If temp_string = "yes" Then
                            Main_Form.dev_form_field_list(2, temp_integer) = Ctrl1.Size.Width
                            Main_Form.dev_form_field_list(3, temp_integer) = Ctrl1.Size.Height
                            Main_Form.dev_form_field_list(4, temp_integer) = Ctrl.Location.X
                            Main_Form.dev_form_field_list(5, temp_integer) = Ctrl.Location.Y
                            Main_Form.dev_form_field_list(6, temp_integer) = Ctrl1.Font.Name
                            Main_Form.dev_form_field_list(7, temp_integer) = Ctrl1.Font.Size
                            Main_Form.dev_form_field_list(8, temp_integer) = Ctrl1.Font.Unit
                            Main_Form.dev_form_field_list(9, temp_integer) = Ctrl1.ForeColor.ToArgb.ToString
                            Main_Form.dev_form_field_list(10, temp_integer) = Ctrl1.BackColor.ToArgb.ToString
                            Main_Form.dev_form_field_list(11, temp_integer) = Ctrl1.Font.Bold
                            Main_Form.dev_form_field_list(12, temp_integer) = Ctrl1.Font.Strikeout
                            Main_Form.dev_form_field_list(13, temp_integer) = Ctrl1.Font.Underline
                            Main_Form.dev_form_field_list(14, temp_integer) = Ctrl1.Font.Italic
                            Main_Form.dev_form_field_list(15, temp_integer) = False 'password
                            If Ctrl1.name.ToString.Contains("_note") Then
                                Main_Form.dev_form_field_list(16, temp_integer) = True 'hidden
                            Else
                                Main_Form.dev_form_field_list(16, temp_integer) = False 'hidden
                            End If
                            Main_Form.dev_form_field_list(17, temp_integer) = "" ' ""  'format
                            Main_Form.dev_form_field_list(18, temp_integer) = Ctrl1.text ' ""  'default value
                            Main_Form.dev_form_field_list(19, temp_integer) = "" ' ""  'sql_code
                            Main_Form.dev_form_field_list(20, temp_integer) = "" ' ""  'note
                            Main_Form.dev_form_field_list(21, temp_integer) = Ctrl1.GetType.Name.ToString  'field_type

                            If Ctrl1.name.ToString.Contains("_field") Or Ctrl1.name.ToString.Contains("_file") Then 'field_type
                                Main_Form.dev_form_field_list(22, temp_integer) = Main_Form.dgv_dev_sql_preview.Rows.Item(CInt(Ctrl1.name.ToString.Split("_").GetValue(0) - 1)).Cells.Item(24).Value.ToString
                                Main_Form.dev_form_field_list(23, temp_integer) = 0
                            Else
                                Main_Form.dev_form_field_list(22, temp_integer) = ""  'field_type
                                If Main_Form.dev_form_field_list(23, temp_integer) = Nothing Then Main_Form.dev_form_field_list(23, temp_integer) = 0
                                Main_Form.dev_form_field_list(24, temp_integer) = False
                                Main_Form.dev_form_field_list(25, temp_integer) = False
                                Main_Form.dev_form_field_list(26, temp_integer) = False
                                Main_Form.dev_form_field_list(27, temp_integer) = False
                            End If
                            Main_Form.dev_form_field_list(28, temp_integer) = False 'subsql_local
                            Main_Form.dev_form_field_list(29, temp_integer) = True 'editable
                            temp_integer += 1
                        Else



                        End If

                    Next
                Else
                    Main_Form.dev_form_field_list(0, temp_integer) = 1000
                End If
            Catch ex As Exception
                MessageBox.Show(fn_translate("creating_input_exception"))
            End Try
        Next
    End Function





    Function fn_dev_clean_form() As Boolean 'called only from closing/saving/ not cleaning functions
        Main_Form.dgv_dev_sql_preview.DataSource = ""
        Main_Form.txt_dev_form_name.Text = ""
        Main_Form.txt_dev_sql_command.Text = ""
        Main_Form.txt_dev_after_sql_command.Text = ""
        Main_Form.txt_dev_note.Text = ""
        Main_Form.txt_dev_full_save_table_name.Text = ""
        Main_Form.chb_dev_export_enabled.Checked = False
        Main_Form.chb_dev_import_enabled.Checked = False
        Main_Form.btn_dev_sql_clear.Enabled = False
        Main_Form.txt_dev_sql_command.Enabled = False
        Main_Form.txt_dev_after_sql_command.Enabled = False
        Main_Form.txt_dev_after_sql_command.Enabled = False

        Main_Form.btn_main_btn_1.Enabled = False
        Main_Form.btn_main_btn_2.Enabled = False
        Main_Form.btn_main_create_copy.Enabled = False
        Main_Form.btn_dev_show_preview.Enabled = False
        Main_Form.chb_dev_format_debug.Enabled = False
        Main_Form.btn_dev_create_update_form.Enabled = False
        fn_delete_dev_form(False, 0, False)
    End Function



    Function fn_delete_dev_form(ByVal db_remove As Boolean, ByVal object_no As Integer, ByVal question As Boolean)

        Dim result = vbNo
        Dim sel_name As String
        Dim line_for_delete As Integer

        If question = True And object_no = 0 Then
            result = MsgBox(fn_translate("clear_form?") + " " + Main_Form.tv_dev_menu.SelectedNode.Text, MsgBoxStyle.YesNo, fn_translate("clear_form"))
        ElseIf question = True And object_no <> 0 Then

            Try
                For i As Integer = 0 To (Main_Form.dev_form_field_list.Length / 29) - 2 Step 1
                    If Main_Form.dev_form_field_list(1, i) = object_no.ToString + "_label" Then
                        sel_name = Main_Form.dev_form_field_list(18, i).ToString
                        line_for_delete = i
                        Exit For
                    End If
                Next
            Catch ex As Exception
            End Try

            result = MsgBox(fn_translate("delete_input_field") + sel_name, MsgBoxStyle.YesNo, fn_translate("clear_form"))

        End If
        If result = vbYes Or question = False Then
            While Main_Form.tp_dev_detail.Controls.OfType(Of Panel)().Count <> 0
                For Each Ctrl In Main_Form.tp_dev_detail.Controls.OfType(Of Panel)()
                    Try
                        If (Ctrl.GetType.Name = "Panel" And object_no = 0) Or object_no = Ctrl.Name.ToString Then
                            Main_Form.tp_dev_detail.Controls.Remove(Ctrl)

                            If db_remove = True Then
                                fn_sql_request("DELETE FROM [dbo].[form_definition] WHERE form_id=" + Main_Form.lbl_dev_form_id.Text + " AND input_no = " + Ctrl.Name.ToString + " ", "DELETE", "local", False, True, Main_Form.sql_parameter, False, False)
                            End If
                        End If
                        Ctrl.Dispose()
                    Catch ex As Exception
                        Ctrl.Dispose()
                    End Try
                Next
            End While

            fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE id=" + Main_Form.lbl_dev_form_id.Text, "LOCAL", False)
            Main_Form.btn_main_btn_1.Enabled = False
            ReDim Main_Form.dev_form_field_list(29, 0)

            If Main_Form.lbl_dev_form_id.Text.Length <> 0 Then
                fn_load_form_definition(True)
                If Main_Form.lbl_dev_form_id.Text.Length <> 0 Then fn_load_dev_form_definition(False)
                fn_sql_check_button("", "", True)
            End If
        End If

    End Function



    Function fn_change_upd_dev_detail_form_array()
        temp_integer = 1
        For Each Ctrl As Control In Main_Form.tp_dev_detail.Controls.OfType(Of Panel)()
            Try
                If Ctrl.GetType.Name = "Panel" Then 'And Ctrl.TabIndex.ToString <> "" Then
                    object_part = 1
                    For Each Ctrl1 In Ctrl.Controls
                        ' MessageBox.Show(Ctrl1.Name.ToString + "|" + Main_Form.dev_form_field_list(1, temp_integer))
                        If Ctrl1.Name.ToString = Main_Form.dev_form_field_list(1, temp_integer) Then
                            temp_string = "yes"
                        Else
                            temp_string = "no"
                        End If

                        If temp_string = "yes" Then

                            ' Main_Form.dev_form_field_list(1, temp_integer) = "label"

                            If object_part = 1 Then
                                Ctrl.Location = New Drawing.Point(CInt(Main_Form.dev_form_field_list(4, temp_integer)), CInt(Main_Form.dev_form_field_list(5, temp_integer)))
                            End If

                            Ctrl1.Size = New Drawing.Size(CInt(Main_Form.dev_form_field_list(2, temp_integer)), CInt(Main_Form.dev_form_field_list(3, temp_integer)))
                            Ctrl1.ForeColor = Color.FromArgb(Main_Form.dev_form_field_list(9, temp_integer))
                            Ctrl1.BackColor = Color.FromArgb(Main_Form.dev_form_field_list(10, temp_integer))
                            Ctrl1.font = fn_CreateFont(Main_Form.dev_form_field_list(6, temp_integer), CInt(Main_Form.dev_form_field_list(7, temp_integer)), CInt(Main_Form.dev_form_field_list(8, temp_integer)), Main_Form.dev_form_field_list(11, temp_integer), Main_Form.dev_form_field_list(12, temp_integer), Main_Form.dev_form_field_list(13, temp_integer), Main_Form.dev_form_field_list(14, temp_integer))

                            If Ctrl1.GetType.Name <> "ComboBox" Then
                                If Main_Form.dev_form_field_list(15, temp_integer) = True Then
                                    Ctrl1.UseSystemPasswordChar = True
                                ElseIf Ctrl1.name.ToString.Contains("field") And Main_Form.dev_form_field_list(21, temp_integer) = "TextBox" Then
                                    Ctrl1.UseSystemPasswordChar = False
                                End If
                            End If

                            Ctrl1.visible = Not CBool(Main_Form.dev_form_field_list(16, temp_integer))

                            If Main_Form.dev_form_field_list(17, temp_integer) <> "" And Main_Form.dev_form_field_list(21, temp_integer) = "DateTimePicker" Then
                                Ctrl1.format = DateTimePickerFormat.Custom
                                Ctrl1.CustomFormat = "" + Main_Form.dev_form_field_list(17, temp_integer) + ""
                            End If
                            'Main_Form.dev_form_field_list(17, temp_integer) = ""  'format
                            'Main_Form.dev_form_field_list(18, temp_integer) = ""  'default value
                            'Main_Form.dev_form_field_list(19, temp_integer) = ""  'sql_code
                            'Main_Form.dev_form_field_list(20, temp_integer) = ""  'note


                            Select Case True
                                Case Ctrl1.Name.ToString.Contains("_label") : object_part = 1
                                Case Ctrl1.Name.ToString.Contains("_field") : object_part = 2
                                Case Ctrl1.Name.ToString.Contains("_file") : object_part = 2
                                Case Ctrl1.Name.ToString.Contains("_note") : object_part = 3
                            End Select

                            If fn_sql_request("SELECT id FROM [dbo].[form_definition] WHERE form_id =" + Main_Form.lbl_dev_form_id.Text + " AND input_no = " + CInt(Ctrl1.name.ToString.Split("_").GetValue(0)).ToString + " AND value_no = " + object_part.ToString + " ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) Then
                                fn_sql_request("UPDATE [dbo].[form_definition] SET [object_name]='" + Main_Form.dev_form_field_list(1, temp_integer).ToString + "',[panel_size_x]=" + Main_Form.dev_form_field_list(2, temp_integer).ToString + ",[panel_size_y]=" + Main_Form.dev_form_field_list(3, temp_integer).ToString + ",[panel_location_x]=" + Main_Form.dev_form_field_list(4, temp_integer).ToString + ",[panel_location_y]=" + Main_Form.dev_form_field_list(5, temp_integer).ToString + ",[font_name]= '" + Main_Form.dev_form_field_list(6, temp_integer) + "',[font_size]=" + Main_Form.dev_form_field_list(7, temp_integer).ToString.Replace(",", ".") + ",[font_unit]=" + Main_Form.dev_form_field_list(8, temp_integer).ToString.Replace(",", ".") + ",[font_textcolor]=" + Main_Form.dev_form_field_list(9, temp_integer).ToString + ",[font_backcolor]=" + Main_Form.dev_form_field_list(10, temp_integer).ToString + ",[font_bold]='" + Main_Form.dev_form_field_list(11, temp_integer).ToString() + "',[font_strikeout]='" + Main_Form.dev_form_field_list(12, temp_integer).ToString() + "',[font_underline]='" + Main_Form.dev_form_field_list(13, temp_integer).ToString() + "',[font_italic]='" + Main_Form.dev_form_field_list(14, temp_integer).ToString() + "',[font_password]='" + Main_Form.dev_form_field_list(15, temp_integer).ToString() + "',[object_hidden]='" + Main_Form.dev_form_field_list(16, temp_integer).ToString() + "',[object_format]='" + Main_Form.dev_form_field_list(17, temp_integer) + "',[object_default_value]='" + Main_Form.dev_form_field_list(18, temp_integer) + "',[object_sql_code]='" + Main_Form.dev_form_field_list(19, temp_integer).Replace("'", "''") + "',[object_note]='" + Main_Form.dev_form_field_list(20, temp_integer) + "',[field_type]='" + Main_Form.dev_form_field_list(21, temp_integer) + "',[input_type]='" + Main_Form.dev_form_field_list(22, temp_integer) + "',[max_length]=" + Main_Form.dev_form_field_list(23, temp_integer) + ",[creator]='" + fn_search_substitution("sub[user_name]") + "',created=GETDATE(),isreadonly='" + Main_Form.dev_form_field_list(24, temp_integer).ToString() + "',savetodb='" + Main_Form.dev_form_field_list(25, temp_integer).ToString() + "',isdblong='" + Main_Form.dev_form_field_list(26, temp_integer).ToString() + "',allow_null='" + Main_Form.dev_form_field_list(27, temp_integer).ToString() + "',subsql_local='" + Main_Form.dev_form_field_list(28, temp_integer).ToString() + "',editable='" + Main_Form.dev_form_field_list(29, temp_integer).ToString() + "' WHERE [form_id]=" + Main_Form.lbl_dev_form_id.Text + " AND [input_no] = " + CInt(Ctrl1.name.ToString.Split("_").GetValue(0)).ToString + " AND[value_no] = " + object_part.ToString + " ", "UPDATE", "local", False, True, Main_Form.sql_parameter, False, False)
                            Else
                                fn_sql_request("INSERT INTO [dbo].[form_definition] ([form_id],[input_no],[value_no],[object_name],[panel_size_x],[panel_size_y],[panel_location_x],[panel_location_y],[font_name],[font_size],[font_unit],[font_textcolor],[font_backcolor],[font_bold],[font_strikeout],[font_underline],[font_italic],[font_password],[object_hidden],[object_format],[object_default_value],[object_sql_code],[object_note],[field_type],[input_type],[max_length],[creator],[isreadonly],[savetodb],[isdblong],[allow_null],[subsql_local],[editable]) VALUES(" + Main_Form.lbl_dev_form_id.Text + "," + CInt(Ctrl1.name.ToString.Split("_").GetValue(0)).ToString + "," + object_part.ToString + ",'" + Main_Form.dev_form_field_list(1, temp_integer).ToString + "'," + Main_Form.dev_form_field_list(2, temp_integer).ToString + "," + Main_Form.dev_form_field_list(3, temp_integer).ToString + "," + Main_Form.dev_form_field_list(4, temp_integer).ToString + "," + Main_Form.dev_form_field_list(5, temp_integer).ToString + ",'" + Main_Form.dev_form_field_list(6, temp_integer) + "'," + Main_Form.dev_form_field_list(7, temp_integer).ToString.Replace(",", ".") + "," + Main_Form.dev_form_field_list(8, temp_integer).ToString.Replace(",", ".") + "," + Main_Form.dev_form_field_list(9, temp_integer).ToString + "," + Main_Form.dev_form_field_list(10, temp_integer).ToString + ",'" + Main_Form.dev_form_field_list(11, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(12, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(13, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(14, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(15, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(16, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(17, temp_integer) + "','" + Main_Form.dev_form_field_list(18, temp_integer) + "','" + Main_Form.dev_form_field_list(19, temp_integer).Replace("'", "''") + "','" + Main_Form.dev_form_field_list(20, temp_integer) + "','" + Main_Form.dev_form_field_list(21, temp_integer) + "','" + Main_Form.dev_form_field_list(22, temp_integer) + "'," + Main_Form.dev_form_field_list(23, temp_integer) + ",'" + fn_search_substitution("sub[user_name]") + "','" + Main_Form.dev_form_field_list(24, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(25, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(26, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(27, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(28, temp_integer).ToString() + "','" + Main_Form.dev_form_field_list(29, temp_integer).ToString() + "')", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
                            End If

                            temp_integer += 1
                        End If
                    Next
                Else
                    Main_Form.dev_form_field_list(0, temp_integer) = 1000
                End If

            Catch ex As Exception
                MessageBox.Show(fn_translate("apply_changes_input_fields_exception") + vbNewLine + Ctrl.Name.ToString)
            End Try
        Next
    End Function





    Function fn_load_dev_form_definition(ByRef showErrorMessage As Boolean) ' create form from database
        Try


            While Main_Form.tp_dev_detail.Controls.OfType(Of Panel)().Count <> 0
                For Each Ctrl In Main_Form.tp_dev_detail.Controls.OfType(Of Panel)()
                    Ctrl.Dispose()
                Next
            End While

            Dim New_panel, panel_reaction As Panel
            Dim text_reaction As TextBox
            Dim button_reaction As Button
            Dim combo_reaction As ComboBox
            Dim label_reaction As Label
            Dim picture_reaction As PictureBox

            Dim New_Field
            Dim column_pos = 0, lenght = 0

            If fn_sql_request("SELECT * FROM [dbo].[form_definition] WHERE [form_id]=" + Main_Form.lbl_dev_form_id.Text + " ORDER BY input_no,value_no ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) Then

                For col = 0 To Main_Form.sql_array_count
                    If IsNumeric(Main_Form.sql_array(col, 2)) Then
                        Try 'separate inputs field

                            'MessageBox.Show(Main_Form.sql_array(col, 26))

                            Select Case Main_Form.sql_array(col, 3)

                                Case 1
                                    'load label property
                                    'MessageBox.Show(Main_Form.sql_array(col + 1, 25))

                                    lenght = 0
                                    New_panel = New Panel
                                    New_panel.TabIndex = Main_Form.sql_array(col, 2) * 10
                                    New_panel.Name = Main_Form.sql_array(col, 2)
                                    New_panel.Text = Main_Form.sql_array(col, 2)
                                    New_panel.Location = New Point(Main_Form.sql_array(col, 7), Main_Form.sql_array(col, 8))

                                    'new field unique_key
                                    If {"int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint", "bit", "binary", "varbinary", "date", "datetime", "datetime2", "datetimeoffset", "time", "timestamp", "char", "nchar", "ntext", "nvarchar", "text", "varchar", "image", "xml", "uniqueidentifier"}.Contains(Main_Form.sql_array(col + 1, 25)) Then
                                        New_Field = New PictureBox
                                        New_Field.Name = (Main_Form.sql_array(col, 2)).ToString + "_key"
                                        New_Field.size = New Drawing.Size(25, 20)
                                        New_Field.Location = New Point(lenght + 2, 5)
                                        New_Field.backcolor = dev_transparent
                                        New_Field.Cursor = Cursors.Hand
                                        New_Field.sizemode = PictureBoxSizeMode.StretchImage
                                        lenght = lenght + 25
                                        If Main_Form.sql_array(col, 26) = 1 Then
                                            New_Field.image = New Bitmap(My.Resources.key_on)
                                        Else
                                            New_Field.image = New Bitmap(My.Resources.key_off)
                                        End If
                                        picture_reaction = DirectCast(New_Field, PictureBox)
                                        AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_change_key
                                        New_panel.Controls.Add(New_Field)
                                    End If

                                    New_Field = New PictureBox
                                    New_Field.Name = (Main_Form.sql_array(col, 2)).ToString + "_dbsave"
                                    New_Field.size = New Drawing.Size(25, 20)
                                    New_Field.Location = New Point(lenght + 2, 5)
                                    New_Field.backcolor = dev_transparent
                                    New_Field.Cursor = Cursors.Hand
                                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                                    lenght = lenght + 30
                                    If CBool(Main_Form.sql_array(col + 1, 35)) Then
                                        New_Field.image = New Bitmap(My.Resources.db_commit_on)
                                    Else
                                        New_Field.image = New Bitmap(My.Resources.db_commit_off)
                                    End If
                                    picture_reaction = DirectCast(New_Field, PictureBox)

                                    New_Field.enabled = Not CBool(Main_Form.sql_array(col + 1, 32))
                                    New_Field.Tag = Not CBool(Main_Form.sql_array(col + 1, 33))

                                    AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_save_to_db
                                    New_panel.Controls.Add(New_Field)


                                    'new field move icon
                                    New_Field = New PictureBox
                                    New_Field.Name = (Main_Form.sql_array(col, 2)).ToString + "_move"
                                    New_Field.size = New Drawing.Size(25, 20)
                                    New_Field.Location = New Point(lenght + 2, 5)
                                    New_Field.backcolor = dev_transparent
                                    New_Field.Cursor = Cursors.Hand
                                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                                    lenght = lenght + 30
                                    New_Field.image = New Bitmap(My.Resources.move)
                                    picture_reaction = DirectCast(New_Field, PictureBox)

                                    New_Field.Tag = Not CBool(Main_Form.sql_array(col + 1, 34))
                                    AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_moveclick
                                    New_panel.Controls.Add(New_Field)

                                    'new fields label
                                    New_Field = New Label
                                    New_Field.Name = Main_Form.sql_array(col, 4)
                                    New_Field.text = Main_Form.sql_array(col, 21)
                                    New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                    New_Field.Location = New Point(lenght, 5)
                                    New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                    New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                    New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), CBool(Main_Form.sql_array(col, 14)), CBool(Main_Form.sql_array(col, 15)), CBool(Main_Form.sql_array(col, 16)), CBool(Main_Form.sql_array(col, 17)))
                                    New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                    New_Field.Cursor = Cursors.Default
                                    If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 130
                                    label_reaction = DirectCast(New_Field, Label)
                                    New_panel.Controls.Add(New_Field)

                                Case 2
                                    'load field property
                                    Select Case Main_Form.sql_array(col, 25)
                                        Case "int", "bigint", "decimal", "float", "numeric", "real", "smallint", "tinyint"
                                            If Main_Form.sql_array(col, 22) = Nothing Then
                                                New_Field = New TextBox
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
                                                        New_Field.UseSystemPasswordChar = CBool(Main_Form.sql_array(col, 18))
                                                        text_reaction = DirectCast(New_Field, TextBox)
                                                        AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit

                                                        New_Field.text = Main_Form.sql_array_addon(0, 1)
                                                    Else
                                                        New_Field = New ComboBox
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
                                            New_Field.enabled = If((Not CBool(Main_Form.sql_array(col, 32)) AndAlso CBool(Main_Form.sql_array(col, 39))), True, False)
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
                                                        Try

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
                                                        Catch ex As Exception
                                                            MessageBox.Show(fn_translate("sql_command_addon_error") + ": SELECT XX,YY,'YES/NO' - Inserted Value,showned Value,Editable")
                                                        End Try
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
                                            New_Field.enabled = False
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            text_reaction = DirectCast(New_Field, TextBox)
                                            New_panel.Controls.Add(New_Field)

                                        Case "bit"
                                            New_Field = New CheckBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            New_Field.text = ""
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 5)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 25
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

                                            New_Field.Format = DateTimePickerFormat.Time
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))

                                            If Main_Form.sql_array(col, 20).Length > 0 Then
                                                New_Field.format = DateTimePickerFormat.Short
                                                New_Field.CustomFormat = Main_Form.sql_array(col, 20)
                                                Main_Form.dgv_dev_sql_preview.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                            End If

                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
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
                                                Main_Form.dgv_dev_sql_preview.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                            End If

                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
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
                                                Main_Form.dgv_dev_sql_preview.Columns.Item(CInt(Main_Form.sql_array(col, 4).Replace("_field", "")) - 1).DefaultCellStyle.Format = Main_Form.sql_array(col, 20)
                                            End If

                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 155
                                            New_panel.Controls.Add(New_Field)

                                        Case "image"
                                            New_Field = New PictureBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            'New_Field.Image = ""
                                            New_Field.SizeMode = PictureBoxSizeMode.StretchImage
                                            New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                            New_Field.Location = New Point(lenght, 2)
                                            New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 30
                                            New_panel.Controls.Add(New_Field)

                                        Case "binary", "varbinary", "xml"
                                            New_Field = New TextBox
                                            New_Field.Name = Main_Form.sql_array(col, 4)
                                            New_Field.text = Main_Form.sql_array(col, 21)
                                            New_Field.size = New Drawing.Size(100, 25)
                                            New_Field.Location = New Point(lenght, 4)
                                            New_Field.ForeColor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                            New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                            New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 105
                                            text_reaction = DirectCast(New_Field, TextBox)
                                            New_panel.Controls.Add(New_Field)

                                            New_Field = New Button
                                            New_Field.Name = Main_Form.sql_array(col, 4).Replace("_file", "_selbtn")
                                            New_Field.text = fn_translate("Select_file")
                                            New_Field.size = New Drawing.Size(80, 25)
                                            New_Field.Location = New Point(lenght, 3)
                                            New_Field.BackColor = dev_backcolor
                                            New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                            New_Field.Cursor = Cursors.Hand
                                            If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 85
                                            button_reaction = DirectCast(New_Field, Button)
                                            AddHandler button_reaction.Click, AddressOf Main_Form.react_openfiledialog
                                            New_panel.Controls.Add(New_Field)

                                        Case Else

                                    End Select




                                Case 3
                                    'load note property
                                    'new field note
                                    New_Field = New Label
                                    New_Field.Name = Main_Form.sql_array(col, 4)
                                    New_Field.text = Main_Form.sql_array(col, 21)
                                    New_Field.size = New Drawing.Size(Main_Form.sql_array(col, 5), Main_Form.sql_array(col, 6))
                                    New_Field.Location = New Point(lenght, 5)
                                    New_Field.forecolor = Color.FromArgb(Main_Form.sql_array(col, 12))
                                    New_Field.BackColor = Color.FromArgb(Main_Form.sql_array(col, 13))
                                    New_Field.font = fn_CreateFont(Main_Form.sql_array(col, 9), CInt(Main_Form.sql_array(col, 10)), CInt(Main_Form.sql_array(col, 11)), Main_Form.sql_array(col, 14), Main_Form.sql_array(col, 15), Main_Form.sql_array(col, 16), Main_Form.sql_array(col, 17))
                                    New_Field.visible = Not CBool(Main_Form.sql_array(col, 19))
                                    New_Field.Cursor = Cursors.Default
                                    If Not CBool(Main_Form.sql_array(col, 19)) Then lenght = lenght + 160
                                    label_reaction = DirectCast(New_Field, Label)
                                    New_panel.Controls.Add(New_Field)


                                    'new field edit icon
                                    New_Field = New PictureBox
                                    New_Field.Name = (Main_Form.sql_array(col, 2)).ToString + "_edit"
                                    New_Field.size = New Drawing.Size(20, 25)
                                    New_Field.Location = New Point(lenght, 2)
                                    New_Field.backcolor = dev_transparent
                                    New_Field.Cursor = Cursors.Hand
                                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                                    lenght = lenght + 20
                                    New_Field.image = New Bitmap(My.Resources.edit)
                                    picture_reaction = DirectCast(New_Field, PictureBox)
                                    AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_editclick
                                    New_panel.Controls.Add(New_Field)


                                    'new field delete icon
                                    New_Field = New PictureBox
                                    New_Field.Name = (Main_Form.sql_array(col, 2)).ToString + "_delete"
                                    New_Field.size = New Drawing.Size(20, 18)
                                    New_Field.Location = New Point(lenght, 6)
                                    New_Field.backcolor = dev_transparent
                                    New_Field.Cursor = Cursors.Hand
                                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                                    lenght = lenght + 20
                                    New_Field.image = New Bitmap(My.Resources.button_cancel)
                                    picture_reaction = DirectCast(New_Field, PictureBox)
                                    AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_panel_deleteclick
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
                                        New_panel.BackColor = dev_backcolor
                                    End If


                                    New_panel.Cursor = Cursors.Default
                                    panel_reaction = DirectCast(New_panel, Panel)

                                    Main_Form.tp_dev_detail.Controls.Add(New_panel)
                            End Select


                        Catch ex As Exception
                            fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE id=" + Main_Form.lbl_dev_form_id.Text, "LOCAL", False)

                        End Try

                    End If
                Next
                fn_dev_form_field_array_from_sql()
                Main_Form.tc_dev_menu.SelectedIndex = 2
                fn_sql_check_button("SELECT TOP 1 id FROM dbo.form_definition WHERE id=" + Main_Form.lbl_dev_form_id.Text, "LOCAL", False)
            Else
                If showErrorMessage Then MessageBox.Show(fn_translate("form_definition_doesnt_exist"))
            End If


        Catch ex As Exception

        End Try

    End Function


    'SUBFORM SECTION
    Function fn_enable_disable_subform(ByVal enable As Boolean)
        Main_Form.lbl_created_binds.Text = fn_translate("created_binds")
        Main_Form.dev_lv_subform_list.Clear()
        Main_Form.dev_lb_source_field_list.Items.Clear()
        Main_Form.dev_lb_destination_field_list.Items.Clear()
        Main_Form.dev_dgv_subform_bingings.Rows.Clear()

        If enable Then
            Main_Form.btn_add_bind_form.Enabled = True
            Main_Form.dev_lv_subform_list.Enabled = True
            Main_Form.dev_lb_source_field_list.Enabled = True
            Main_Form.dev_lb_destination_field_list.Enabled = True
            Main_Form.dev_nud_subform_position.Enabled = True
            Main_Form.dev_txt_subform_bindname.Enabled = True
        Else
            Main_Form.btn_add_bind_form.Enabled = False
            Main_Form.dev_lv_subform_list.Enabled = False
            Main_Form.dev_lb_source_field_list.Enabled = False
            Main_Form.dev_lb_destination_field_list.Enabled = False
            Main_Form.dev_nud_subform_position.Enabled = False
            Main_Form.dev_txt_subform_bindname.Enabled = False
            Main_Form.btn_del_bind_form.Enabled = False
        End If
    End Function


    Function fn_load_existed_subforms(ByVal selected_subform_text As String) 'load existed subform_bindings
        Try
            Main_Form.dev_lv_subform_list.Items.Clear()

            Dim query As String
            query = "SELECT DISTINCT fl.[id],fl.[form_type],fl.[form_name],fl.[position],fl.[enable_translate] FROM [dbo].[subform_binds] sb,[dbo].[form_list] fl WHERE sb.[mainform_id] = " + Main_Form.lbl_dev_form_id.Text + " AND sb.[subform_id] = fl.[id] ORDER BY fl.[position] ASC"
            If fn_load_sql_addon(query, True, "subform_field_list") = True And Main_Form.sql_array_addon_count > 0 Then
                For i = 1 To Main_Form.sql_array_addon_count
                    Dim item As New ListViewItem
                    If Main_Form.sql_array_addon(i - 1, 4) Then
                        item.Text = fn_translate(Main_Form.sql_array_addon(i - 1, 2).ToString)
                    Else
                        item.Text = Main_Form.sql_array_addon(i - 1, 2).ToString
                    End If
                    item.Name = Main_Form.sql_array_addon(i - 1, 0).ToString + Main_Form.sql_array_addon(i - 1, 1).ToString
                    Main_Form.dev_lv_subform_list.Items.Add(item)
                Next
                Main_Form.dev_lb_source_field_list.Items.Clear()
                For Each col As DataGridViewColumn In Main_Form.dgv_dev_sql_preview.Columns
                    Main_Form.dev_lb_source_field_list.Items.Add(col.Name)
                Next
            End If

            If selected_subform_text.Length > 0 Then
                If Not Main_Form.dev_lv_subform_list.FindItemWithText(selected_subform_text) Is Nothing Then
                    Main_Form.dev_lv_subform_list.FindItemWithText(selected_subform_text).Selected = True
                End If
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("subform_definition_cannot_be_loaded"))
        End Try
    End Function



    Function fn_load_destination_datafield_for_subform_binding()
        Try
            Main_Form.dev_lb_destination_field_list.Items.Clear()
            If Not Main_Form.dev_lv_subform_list.SelectedItems Is Nothing Then
                If Main_Form.dev_lv_subform_list.SelectedItems.Count > 0 Then

                    Dim query As String
                    query = "SELECT [basic_sql],[local_db] FROM [dbo].[form_list] WHERE [id] = " + Main_Form.dev_lv_subform_list.SelectedItems.Item(0).Name.Replace("SQL", "").Replace("TERMINAL", "") + ""
                    If fn_load_sql_addon(query, True, "subform_field_list") = True Then
                        If fn_load_sql_addon(Main_Form.sql_array_addon(0, 0), CBool(Main_Form.sql_array_addon(0, 1)), "subform_field_list") = True Then
                            For Each item As Data.DataRow In dgw_table_schema_addon.Rows
                                Main_Form.dev_lb_destination_field_list.Items.Add(item.Item(0).ToString)
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(fn_translate("subform_definition_cannot_be_loaded"))
        End Try
    End Function


    Function fn_save_subform_bind()
        Try
            Dim query As String
            query = "SELECT id FROM [dbo].[subform_binds] WHERE [mainform_id]=" + Main_Form.lbl_dev_form_id.Text + " AND [subform_id]=" + Main_Form.dev_lv_subform_list.SelectedItems.Item(0).Name.Replace("SQL", "").Replace("TERMINAL", "") + " AND [mainform_field]='" + Main_Form.dev_lb_source_field_list.SelectedItem + "' AND [subform_field]='" + Main_Form.dev_lb_destination_field_list.SelectedItem + "'"
            fn_load_sql_addon(query, True, "subform_field_list")
            If Main_Form.sql_array_addon_count = 0 Then
                query = "INSERT INTO [dbo].[subform_binds] ([mainform_id],[subform_id],[position],[bind_name],[mainform_field],[subform_field],[note],[creator],[tablename_join]) VALUES (" + Main_Form.lbl_dev_form_id.Text + "," + Main_Form.dev_lv_subform_list.SelectedItems.Item(0).Name.Replace("SQL", "").Replace("TERMINAL", "") + "," + Main_Form.dev_nud_subform_position.Value.ToString + ",'" + Main_Form.dev_txt_subform_bindname.Text + "','" + Main_Form.dev_lb_source_field_list.SelectedItem + "','" + Main_Form.dev_lb_destination_field_list.SelectedItem + "','','" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(Main_Form.chb_dev_tablename_join.Checked)).ToString() + ")"
                fn_sql_request(query, "INSERT", "local", False, False, Main_Form.sql_parameter, False, False)
            Else
                query = "UPDATE [dbo].[subform_binds] SET [position]=" + Main_Form.dev_nud_subform_position.Value.ToString + ",[bind_name]='" + Main_Form.dev_txt_subform_bindname.Text + "',[creator]='" + fn_search_substitution("sub[user_name]") + "', [tablename_join]=" + CInt(Int(Main_Form.chb_dev_tablename_join.Checked)).ToString() + "  WHERE id = " + Main_Form.sql_array_addon(0, 0) + ""
                fn_sql_request(query, "UPDATE", "local", False, False, Main_Form.sql_parameter, False, False)
            End If

            If Not Main_Form.dev_lv_subform_list.SelectedItems Is Nothing Then
                If Main_Form.dev_lv_subform_list.SelectedItems.Count > 0 Then
                    fn_load_destination_datafield_for_subform_binding()
                    fn_load_dev_subform_panel(Main_Form.dev_lv_subform_list.SelectedItems(0).Name.Replace("SQL", "").Replace("TERMINAL", ""))
                End If
            End If
            Main_Form.dev_txt_subform_bindname.Text = ""
            Main_Form.chb_dev_tablename_join.Checked = False
            Main_Form.dev_lb_source_field_list.ClearSelected()
            Main_Form.dev_lb_destination_field_list.ClearSelected()
            '  Main_Form.nud_subform_position.Value += 10
        Catch ex As Exception
            fn_cursor_waiting(False)
        End Try
    End Function



    Function fn_load_dev_subform_panel(ByVal selected_subform_id As Integer) As Boolean
        Try
            Dim query As String

            query = "SELECT sb.[id],sb.[position],sb.[bind_name],fl.[form_name],sb.[mainform_field],sb.[subform_field],sb.[tablename_join] FROM [dbo].[subform_binds] sb,[dbo].[form_list] fl WHERE sb.[mainform_id]=" + Main_Form.lbl_dev_form_id.Text + " AND sb.[subform_id]=" + selected_subform_id.ToString + " AND sb.[subform_id] = fl.id ORDER BY sb.[position] ASC"
            fn_load_sql_addon(query, True, "subform_field_list")


            'bind datagrigview cleaning
            Main_Form.dev_dgv_subform_bingings.Rows.Clear()

            If Not Main_Form.dev_lv_subform_list.SelectedItems Is Nothing Then
                If Main_Form.dev_lv_subform_list.SelectedItems.Count > 0 Then
                    Main_Form.lbl_created_binds.Text = fn_translate("created_binds") + ": " + Main_Form.dev_lv_subform_list.SelectedItems(0).Text
                End If
            End If

            'create graphics binding
            For i = 1 To Main_Form.sql_array_addon_count

                Dim image = SWAPP_Builder.My.Resources.Resources.button_cancel
                image.Tag = Main_Form.sql_array_addon(i - 1, 0)

                Main_Form.dev_dgv_subform_bingings.Rows.Add(
                    Main_Form.sql_array_addon(i - 1, 1),
                    Main_Form.sql_array_addon(i - 1, 2),
                    Main_Form.sql_array_addon(i - 1, 3),
                    Main_Form.sql_array_addon(i - 1, 4),
                    Main_Form.sql_array_addon(i - 1, 5),
                    Main_Form.sql_array_addon(i - 1, 6),
                    image
                )
                ' AddHandler picture_reaction.MouseClick, AddressOf Main_Form.dev_subformpanel_deleteclick
            Next

            If Main_Form.sql_array_addon_count > 0 Then
                Main_Form.dev_nud_subform_position.Value = Main_Form.sql_array_addon(Main_Form.sql_array_addon_count - 1, 1) + 10
            Else
                Main_Form.dev_nud_subform_position.Value = 10
            End If

        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("data_bindings_cannot_be_shown"))
        End Try
    End Function




    Function fn_delete_subform_bind(ByVal bind_id As Integer)
        Dim query, selected_subform_id, selected_subform_text

        query = "DELETE FROM [dbo].[subform_binds] WHERE id=" + bind_id.ToString + ""
        fn_sql_request(query, "DELETE", "local", False, False, Main_Form.sql_parameter, False, False)
        If Not Main_Form.dev_lv_subform_list.SelectedItems Is Nothing Then
            If Main_Form.dev_lv_subform_list.SelectedItems.Count > 0 Then
                selected_subform_id = Main_Form.dev_lv_subform_list.SelectedItems(0).Name.Replace("SQL", "").Replace("TERMINAL", "")
                selected_subform_text = Main_Form.dev_lv_subform_list.SelectedItems(0).Text
            End If
        End If
        fn_load_existed_subforms(selected_subform_text)
        ' fn_enable_disable_subform(True)
        fn_load_destination_datafield_for_subform_binding()
        fn_load_dev_subform_panel(selected_subform_id)
    End Function


    Function fn_delete_all_subform_binds()
        Dim query
        query = "DELETE FROM [dbo].[subform_binds] WHERE [mainform_id]='" + Main_Form.lbl_dev_form_id.Text + "' and [subform_id]='" + Main_Form.dev_lv_subform_list.SelectedItems(0).Name.Replace("SQL", "").Replace("TERMINAL", "") + "'"
        fn_sql_request(query, "DELETE", "local", False, False, Main_Form.sql_parameter, False, False)
        If Not Main_Form.dev_lv_subform_list.SelectedItems Is Nothing Then
            If Main_Form.dev_lv_subform_list.SelectedItems.Count > 0 Then
                fn_load_destination_datafield_for_subform_binding()
                fn_load_dev_subform_panel(Main_Form.dev_lv_subform_list.SelectedItems(0).Name.Replace("SQL", "").Replace("TERMINAL", ""))
            End If
        End If
    End Function



End Module
