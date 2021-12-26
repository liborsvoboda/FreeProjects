Public Class frm_filter

    Private changed = False
    Private loading = True
    Private max_y As Integer
    Private start_y As Integer = 0
    Private selected_fields As String
    Private starting As Boolean = True
    Friend where_command As String = ""

    Private Sub frm_new_menu_item_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If changed = True Then
            Dim result = MsgBox(fn_translate("forget_changes?"), MsgBoxStyle.YesNo)
            If result = vbYes Then
                Main_Form.Enabled = True
            Else
                e.Cancel = True
            End If
        Else
            Main_Form.Enabled = True
        End If
    End Sub


    Private Sub frm_new_menu_item_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then
            Me.Close()
        End If
    End Sub



    Private Sub frm_dev_input_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub frm_dev_input_shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        where_command = Main_Form.txt_filter_command.Text

        If Main_Form.lbl_filtername_selected.Text.Length = 0 Then tsb_save_filter.Enabled = False

        If fn_create_filter_form() Then
            Me.Text = fn_translate(Me.Text)

            If Main_Form.tv_filter_menu.SelectedNode IsNot Nothing Then
                Me.Text &= ": " & Main_Form.tv_filter_menu.SelectedNode.Text
            End If


            For Each ctrl_object As Control In Me.Controls
                Try
                    ctrl_object.Text = fn_translate(ctrl_object.Text)
                Catch ex As Exception
                End Try
            Next

            For Each ctrl_object In Me.ts_tools.Items
                Try
                    ctrl_object.Text = fn_translate(ctrl_object.Text)
                Catch ex As Exception
                End Try
            Next
        End If
        starting = False
    End Sub



    Function fn_create_filter_form() As Boolean
        fn_create_filter_form = False
        Dim x, y As Integer
        y = start_y

        Try
            Dim New_panel, panel_reaction As Panel
            Dim text_reaction As TextBox
            Dim label_reaction As Label
            Dim checkbox_reaction As CheckBox
            Dim picture_reaction As PictureBox
            Dim combobox_reaction As ComboBox
            Dim datetimepicker_reaction As DateTimePicker
            Dim New_Field
            Dim column_pos = 0
            Dim last_index As Integer
            For Each col As DataGridViewColumn In Main_Form.dgw_query_view.Columns
                If {"Int32", "Int64", "Double", "Decimal", "String", "Boolean", "TimeSpan", "DateTime", "Guid"}.Contains(col.ValueType.Name) Then
                    last_index = col.Index
                End If
            Next

            For Each col As DataGridViewColumn In Main_Form.dgw_query_view.Columns

                x = 0
                If {"Int32", "Int64", "Double", "Decimal", "String", "Boolean", "TimeSpan", "DateTime", "Guid"}.Contains(col.ValueType.Name) Then

                    New_panel = New Panel
                    New_panel.TabIndex = column_pos * 10
                    New_panel.Name = (column_pos)

                    New_Field = New CheckBox
                    New_Field.Name = (column_pos).ToString & "_use"
                    New_Field.text = ""
                    New_Field.size = New Drawing.Size(30, 22)
                    New_Field.Location = New Point(x + 10, 3)
                    New_Field.BackColor = dev_backcolor
                    New_Field.Cursor = Cursors.Hand
                    New_Field.Checked = False
                    x += 30
                    checkbox_reaction = DirectCast(New_Field, CheckBox)
                    AddHandler checkbox_reaction.CheckedChanged, AddressOf Me.checkedchanged_reaction
                    AddHandler checkbox_reaction.CheckedChanged, AddressOf Me.make_where_cmd
                    New_panel.Controls.Add(New_Field)


                    New_Field = New Label
                    New_Field.Name = (column_pos).ToString & "_label"
                    If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                        New_Field.text = fn_translate(col.Name)
                    Else
                        New_Field.text = col.Name
                    End If
                    New_Field.size = New Drawing.Size(110, 20)
                    New_Field.Location = New Point(x + 10, 5)
                    New_Field.BackColor = dev_transparent
                    New_Field.Cursor = Cursors.Default
                    New_Field.enabled = False
                    x += 115
                    label_reaction = DirectCast(New_Field, Label)
                    New_panel.Controls.Add(New_Field)


                    New_Field = New ComboBox
                    New_Field.Name = (column_pos).ToString & "_cond"
                    New_Field.text = ""
                    New_Field.size = New Drawing.Size(60, 22)
                    New_Field.Location = New Point(x + 5, 4)
                    New_Field.BackColor = dev_backcolor
                    New_Field.DropDownStyle = ComboBoxStyle.DropDownList
                    New_Field.Cursor = Cursors.Hand
                    combobox_reaction = DirectCast(New_Field, ComboBox)
                    AddHandler combobox_reaction.SelectedIndexChanged, AddressOf Me.make_where_cmd
                    New_Field.enabled = False
                    x += 65
                    New_panel.Controls.Add(New_Field)


                    New_Field = New PictureBox
                    New_Field.Name = (column_pos).ToString & "_up"
                    New_Field.size = New Drawing.Size(25, 25)
                    New_Field.Location = New Point(x + 2, 14)
                    New_Field.backcolor = dev_transparent
                    New_Field.Cursor = Cursors.Hand
                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                    x += 25
                    New_Field.image = New Bitmap(My.Resources.arrow_up)
                    picture_reaction = DirectCast(New_Field, PictureBox)

                    If column_pos > 0 Then
                        New_Field.enabled = True
                    Else
                        New_Field.enabled = False
                    End If
                    AddHandler picture_reaction.MouseClick, AddressOf Me.item_move_up
                    AddHandler picture_reaction.MouseClick, AddressOf Me.make_where_cmd
                    New_panel.Controls.Add(New_Field)


                    New_Field = New PictureBox
                    New_Field.Name = (column_pos).ToString + "_down"
                    New_Field.size = New Drawing.Size(25, 25)
                    New_Field.Location = New Point(x + 2, 14)
                    New_Field.backcolor = dev_transparent
                    New_Field.Cursor = Cursors.Hand
                    New_Field.sizemode = PictureBoxSizeMode.StretchImage
                    x += 5
                    New_Field.image = New Bitmap(My.Resources.arrow_down)
                    picture_reaction = DirectCast(New_Field, PictureBox)

                    If col.Index = last_index Then
                        New_Field.enabled = False
                    Else
                        New_Field.enabled = True
                    End If
                    AddHandler picture_reaction.MouseClick, AddressOf Me.item_move_down
                    AddHandler picture_reaction.MouseClick, AddressOf Me.make_where_cmd
                    New_panel.Controls.Add(New_Field)

                    x = 10

                    Select Case col.ValueType.Name
                        Case "Int32", "Decimal", "Double", "Int64"
                            Dim oObj As ComboBox = New_panel.Controls.Find((column_pos).ToString & "_cond", True).FirstOrDefault()
                            oObj.Items.Add("=")
                            oObj.Items.Add("<>")
                            oObj.Items.Add(">")
                            oObj.Items.Add("<")
                            oObj.Items.Add(">=")
                            oObj.Items.Add("<=")

                            New_Field = New TextBox
                            New_Field.Name = (column_pos).ToString & "_field"
                            New_Field.text = "" 'fn_translate("input_field")
                            New_Field.size = New Drawing.Size(200, 20)
                            New_Field.Location = New Point(x, 24)
                            New_Field.BackColor = dev_backcolor
                            New_Field.Cursor = Cursors.Hand
                            New_Field.enabled = False
                            New_Field.tag = col.ValueType.Name
                            text_reaction = DirectCast(New_Field, TextBox)
                            AddHandler text_reaction.KeyPress, AddressOf Main_Form.react_isdigit
                            AddHandler text_reaction.TextChanged, AddressOf Me.make_where_cmd


                        Case "String"
                            Dim oObj As ComboBox = New_panel.Controls.Find((column_pos).ToString & "_cond", True).FirstOrDefault()
                            oObj.Items.Add("=")
                            oObj.Items.Add("<>")
                            oObj.Items.Add("LIKE")
                            oObj.Items.Add("NOT LIKE")

                            New_Field = New TextBox
                            New_Field.Name = (column_pos).ToString & "_field"
                            New_Field.text = "" 'fn_translate("input_field")
                            New_Field.size = New Drawing.Size(200, 20)
                            New_Field.Location = New Point(x, 24)
                            New_Field.BackColor = dev_backcolor
                            New_Field.Cursor = Cursors.Hand
                            New_Field.enabled = False
                            text_reaction = DirectCast(New_Field, TextBox)
                            AddHandler text_reaction.TextChanged, AddressOf Me.make_where_cmd




                        Case "Guid"
                            Dim oObj As ComboBox = New_panel.Controls.Find((column_pos).ToString & "_cond", True).FirstOrDefault()
                            oObj.Items.Add("=")
                            oObj.Items.Add("<>")
                            oObj.Items.Add("LIKE")
                            oObj.Items.Add("NOT LIKE")

                            New_Field = New TextBox
                            New_Field.Name = (column_pos).ToString + "_field"
                            New_Field.text = "" 'fn_translate("input_field")
                            New_Field.size = New Drawing.Size(200, 20)
                            New_Field.Location = New Point(x, 24)
                            New_Field.BackColor = dev_backcolor
                            New_Field.Cursor = Cursors.Hand
                            New_Field.enabled = False
                            text_reaction = DirectCast(New_Field, TextBox)


                        Case "Byte", "Boolean"
                            Dim oObj As ComboBox = New_panel.Controls.Find((column_pos).ToString & "_cond", True).FirstOrDefault()
                            oObj.Items.Add("=")
                            oObj.Items.Add("<>")

                            New_Field = New CheckBox
                            New_Field.Name = (column_pos).ToString + "_field"
                            New_Field.text = ""
                            New_Field.size = New Drawing.Size(13, 13)
                            New_Field.Location = New Point(x, 28)
                            New_Field.BackColor = dev_backcolor
                            New_Field.Cursor = Cursors.Hand
                            New_Field.enabled = False
                            checkbox_reaction = DirectCast(New_Field, CheckBox)
                            AddHandler checkbox_reaction.CheckedChanged, AddressOf Me.make_where_cmd


                        Case "TimeSpan"
                            Dim oObj As ComboBox = New_panel.Controls.Find((column_pos).ToString & "_cond", True).FirstOrDefault()
                            oObj.Items.Add("=")
                            oObj.Items.Add("<>")
                            oObj.Items.Add(">")
                            oObj.Items.Add("<")
                            oObj.Items.Add(">=")
                            oObj.Items.Add("<=")

                            New_Field = New DateTimePicker
                            New_Field.Name = (column_pos).ToString & "_field"
                            New_Field.text = ""
                            New_Field.Format = DateTimePickerFormat.Time
                            New_Field.tag = DateTimePickerFormat.Time
                            New_Field.CustomFormat = "H:mm:ss"
                            New_Field.size = New Drawing.Size(200, 24)
                            New_Field.Location = New Point(x, 24)
                            New_Field.BackColor = dev_backcolor
                            New_Field.Cursor = Cursors.Hand
                            New_Field.enabled = False
                            datetimepicker_reaction = DirectCast(New_Field, DateTimePicker)
                            AddHandler datetimepicker_reaction.ValueChanged, AddressOf Me.make_where_cmd


                        Case "DateTime"
                            Dim oObj As ComboBox = New_panel.Controls.Find((column_pos).ToString & "_cond", True).FirstOrDefault()
                            oObj.Items.Add("=")
                            oObj.Items.Add("<>")
                            oObj.Items.Add(">")
                            oObj.Items.Add("<")
                            oObj.Items.Add(">=")
                            oObj.Items.Add("<=")

                            New_Field = New DateTimePicker
                            New_Field.Name = (column_pos).ToString & "_field"
                            New_Field.text = ""
                            New_Field.Format = DateTimePickerFormat.Short
                            New_Field.tag = DateTimePickerFormat.Short
                            New_Field.CustomFormat = "dd.MM.yyyy"
                            New_Field.size = New Drawing.Size(200, 24)
                            New_Field.Location = New Point(x, 24)
                            New_Field.BackColor = dev_backcolor
                            New_Field.Cursor = Cursors.Hand
                            New_Field.enabled = False
                            datetimepicker_reaction = DirectCast(New_Field, DateTimePicker)
                            AddHandler datetimepicker_reaction.ValueChanged, AddressOf Me.make_where_cmd


                        Case Else

                    End Select
                    New_panel.Controls.Add(New_Field)


                    New_panel.Size = New Drawing.Size(275, 50)
                    New_panel.Location = New Point(0, y)
                    max_y = y
                    y += 50

                    New_panel.BackColor = dev_backcolor
                    New_panel.Cursor = Cursors.Default
                    panel_reaction = DirectCast(New_panel, Panel)
                    Me.p_filter_list.Controls.Add(New_panel)
                    column_pos += 1
                End If
            Next
            btn_apply.Enabled = False
            btn_apply_close.Enabled = False

            fn_apply_filter_array_to_simple_form()

            fn_create_filter_form = True
        Catch ex As Exception
            btn_apply.Enabled = False
            btn_apply_close.Enabled = False
            MessageBox.Show(fn_translate("filter_form_generation_error") & vbNewLine & ex.Message.ToString)
            Close()
        End Try

    End Function


    Public Sub checkedchanged_reaction(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not sender.Checked Then
            Me.Controls.Find((sender.name).ToString.Replace("_use", "_cond").ToString, True).FirstOrDefault().Enabled = False
            Me.Controls.Find((sender.name).ToString.Replace("_use", "_label").ToString, True).FirstOrDefault().Enabled = False
            Me.Controls.Find((sender.name).ToString.Replace("_use", "_field").ToString, True).FirstOrDefault().Enabled = False
        Else
            Me.Controls.Find((sender.name).ToString.Replace("_use", "_cond").ToString, True).FirstOrDefault().Enabled = True
            Me.Controls.Find((sender.name).ToString.Replace("_use", "_label").ToString, True).FirstOrDefault().Enabled = True
            Me.Controls.Find((sender.name).ToString.Replace("_use", "_field").ToString, True).FirstOrDefault().Enabled = True
        End If
    End Sub


    Public Sub item_move_up(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim selected_item As Integer
        selected_item = CInt(sender.name.ToString.Split("_").GetValue(0))

        For Each ctrl In p_filter_list.Controls.OfType(Of Panel)()
            If ctrl.Location.Y = (p_filter_list.Controls.Item(selected_item).Location.Y - 50) AndAlso ctrl.Visible Then
                ctrl.Location = New Point(ctrl.Location.X, ctrl.Location.Y + 50)

                'unlock up of previous control - 3 = up button
                If ctrl.Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) <= start_y Then
                    ctrl.Controls.Item(3).Enabled = False
                Else
                    ctrl.Controls.Item(3).Enabled = True
                End If

                'unlock down of previous control  4 =down button
                If ctrl.Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) >= max_y Then
                    ctrl.Controls.Item(4).Enabled = False
                Else
                    ctrl.Controls.Item(4).Enabled = True
                End If

                Exit For
            End If
        Next

        p_filter_list.Controls.Item(selected_item).Location = New Point(p_filter_list.Controls.Item(selected_item).Location.X, p_filter_list.Controls.Item(selected_item).Location.Y - 50)

        'unlock up of sender control
        If p_filter_list.Controls.Item(selected_item).Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) <= start_y Then
            sender.Enabled = False
        Else
            sender.Enabled = True
        End If

        'unlock down of sender control
        If p_filter_list.Controls.Item(selected_item).Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) >= max_y Then
            p_filter_list.Controls.Item(selected_item).Controls.Item(4).Enabled = False
        Else
            p_filter_list.Controls.Item(selected_item).Controls.Item(4).Enabled = True
        End If

    End Sub


    Public Sub item_move_down(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim selected_item As Integer
        selected_item = CInt(sender.name.ToString.Split("_").GetValue(0))


        For Each ctrl In p_filter_list.Controls.OfType(Of Panel)()
            If ctrl.Location.Y = (p_filter_list.Controls.Item(selected_item).Location.Y + 50) AndAlso ctrl.Visible Then
                ctrl.Location = New Point(ctrl.Location.X, ctrl.Location.Y - 50)

                'unlock up of previous control
                If ctrl.Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) <= start_y Then
                    ctrl.Controls.Item(3).Enabled = False
                Else
                    ctrl.Controls.Item(3).Enabled = True
                End If

                'unlock down of previous control
                If ctrl.Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) >= max_y Then
                    ctrl.Controls.Item(4).Enabled = False
                Else
                    ctrl.Controls.Item(4).Enabled = True
                End If

                Exit For
            End If
        Next


        p_filter_list.Controls.Item(selected_item).Location = New Point(p_filter_list.Controls.Item(selected_item).Location.X, p_filter_list.Controls.Item(selected_item).Location.Y + 50)

        'unlock up of sender control
        If p_filter_list.Controls.Item(selected_item).Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) >= max_y Then
            sender.Enabled = False
        Else
            sender.Enabled = True
        End If

        'unlock down of sender control
        If p_filter_list.Controls.Item(selected_item).Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) <= start_y Then
            p_filter_list.Controls.Item(selected_item).Controls.Item(3).Enabled = False
        Else
            p_filter_list.Controls.Item(selected_item).Controls.Item(3).Enabled = True
        End If
    End Sub


    Private Sub tsb_show_hide_Click(sender As Object, e As EventArgs) Handles tsb_show_hide.Click
        Dim last_y = start_y
        Dim new_index = 1
        p_filter_list.AutoScrollPosition = New Point(p_filter_list.AutoScrollPosition.X, 0)
        If tsb_show_hide.Text = fn_translate("hide") Then
            tsb_show_hide.Image = My.Resources.hide
            tsb_show_hide.Text = fn_translate("show")
            For Each ctrl As Control In p_filter_list.Controls.OfType(Of Panel)().OrderBy(Function(c) c.Location.Y)
                For Each sub_ctrl As CheckBox In ctrl.Controls.OfType(Of CheckBox)()
                    If sub_ctrl.Name.Contains("_use") Then
                        If Not sub_ctrl.Checked Then
                            ctrl.Visible = False
                            ctrl.TabIndex = new_index + 1000
                        Else
                            ctrl.Location = New Point(ctrl.Location.X, last_y)
                            ctrl.TabIndex = new_index + 1000
                            last_y += 50
                        End If
                        Exit For
                    End If
                    new_index += 1
                Next
                max_y = last_y - 50
            Next
        Else
            For Each ctrl As Control In p_filter_list.Controls.OfType(Of Panel)().OrderBy(Function(c) c.Location.Y)
                ctrl.Location = New Point(ctrl.Location.X, last_y)
                ctrl.Visible = True
                last_y += 50
            Next
            max_y = last_y - 50
            tsb_show_hide.Image = My.Resources.show
            tsb_show_hide.Text = fn_translate("hide")
        End If
        reset_move_buttons()
    End Sub


    'functions

    Private Function reset_move_buttons() As Boolean
        For Each ctrl As Control In p_filter_list.Controls.OfType(Of Panel)()
            For Each sub_ctrl As CheckBox In ctrl.Controls.OfType(Of CheckBox)()
                If sub_ctrl.Name.Contains("_use") Then
                    If sub_ctrl.Checked Then
                        'unlock up control  3 =up button
                        If ctrl.Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) <= start_y Then
                            ctrl.Controls.Item(3).Enabled = False
                        Else
                            ctrl.Controls.Item(3).Enabled = True
                        End If

                        'unlock down control  4 =down button
                        If ctrl.Location.Y + System.Math.Abs(p_filter_list.AutoScrollPosition.Y) >= max_y Then
                            ctrl.Controls.Item(4).Enabled = False
                        Else
                            ctrl.Controls.Item(4).Enabled = True
                        End If

                    End If
                End If
            Next
        Next

        reset_move_buttons = True
    End Function



    Private Sub make_where_cmd(sender As Object, e As EventArgs)
        Me.where_command = ""
        Dim translated_column, inserted_value As String
        Dim cmd_no As Integer = 0
        If Not starting Then
            If Me.Controls.Find(("SQL_AREA"), True).FirstOrDefault() Is Nothing Then
                changed = False

                For Each ctrl As Control In p_filter_list.Controls.OfType(Of Panel)().OrderBy(Function(c) c.Location.Y)

                    For Each sub_ctrl As CheckBox In ctrl.Controls.OfType(Of CheckBox)()
                        If sub_ctrl.Name.Contains("_use") Then
                            If sub_ctrl.Checked Then
                                changed = True

                                If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                    If ctrl.Controls.Item(5).GetType.Name = "DateTimePicker" Then
                                        translated_column = "CAST([" & fn_sys_translate(ctrl.Controls.Item(1).Text) & "]AS DATE)"
                                    Else
                                        translated_column = "[" & fn_sys_translate(ctrl.Controls.Item(1).Text) & "]"
                                    End If
                                Else
                                    If ctrl.Controls.Item(5).GetType.Name = "DateTimePicker" Then
                                        translated_column = "CAST([" & ctrl.Controls.Item(1).Text & "] AS DATE)"
                                    Else
                                        translated_column = "[" & ctrl.Controls.Item(1).Text & "]"
                                    End If
                                End If


                                If ctrl.Controls.Item(5).GetType.Name = "CheckBox" Then
                                    inserted_value = "'" & CType(ctrl.Controls.Item(5), CheckBox).Checked.ToString & "'"
                                ElseIf ctrl.Controls.Item(5).GetType.Name = "DateTimePicker" Then
                                    inserted_value = " CONVERT(DATE,'" & ctrl.Controls.Item(5).Text & "',105) "
                                ElseIf {"Int32", "Int64", "Double", "Decimal"}.Contains(ctrl.Controls.Item(5).Tag) Then
                                    inserted_value = " '" & ctrl.Controls.Item(5).Text.Replace(",", ".") & "' "
                                Else
                                    inserted_value = " '" & ctrl.Controls.Item(5).Text & "' "
                                End If
                                Me.where_command = where_command & " and " & translated_column & " " & ctrl.Controls.Item(2).Text & inserted_value & vbNewLine


                                ReDim Preserve Main_Form.where_array(5, cmd_no)

                                For i = 0 To 5
                                    Select Case i
                                        Case 0
                                            If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                                                If ctrl.Controls.Item(5).GetType.Name = "DateTimePicker" Then
                                                    Main_Form.where_array(0, cmd_no) = "CAST([" & fn_sys_translate(ctrl.Controls.Item(1).Text) + "]AS DATE)"
                                                Else
                                                    Main_Form.where_array(0, cmd_no) = "[" & fn_sys_translate(ctrl.Controls.Item(1).Text) + "]"
                                                End If
                                            Else
                                                If ctrl.Controls.Item(5).GetType.Name = "DateTimePicker" Then
                                                    Main_Form.where_array(0, cmd_no) = "CAST([" & ctrl.Controls.Item(1).Text & "] AS DATE)"
                                                Else
                                                    Main_Form.where_array(0, cmd_no) = "[" & ctrl.Controls.Item(1).Text & "]"
                                                End If
                                            End If
                                        Case 1
                                            Main_Form.where_array(1, cmd_no) = ctrl.Controls.Item(2).Text
                                        Case 2
                                            If ctrl.Controls.Item(5).GetType.Name = "CheckBox" Then
                                                Main_Form.where_array(2, cmd_no) = CType(ctrl.Controls.Item(5), CheckBox).Checked.ToString
                                            Else
                                                Main_Form.where_array(2, cmd_no) = ctrl.Controls.Item(5).Text
                                            End If
                                        Case 3
                                            Main_Form.where_array(3, cmd_no) = ctrl.Controls.Item(5).GetType.Name
                                        Case 4
                                            Main_Form.where_array(4, cmd_no) = ctrl.Controls.Item(5).Name
                                        Case 5
                                            If ctrl.Controls.Item(5).Tag IsNot Nothing Then
                                                Main_Form.where_array(5, cmd_no) = ctrl.Controls.Item(5).Tag
                                            Else
                                                Main_Form.where_array(5, cmd_no) = " "
                                            End If
                                    End Select
                                Next
                                cmd_no += 1
                            End If
                        End If
                    Next
                Next
                user_where = where_command
            Else
                changed = True
                user_where = Controls.Find(("SQL_AREA"), True).FirstOrDefault().Text
            End If

            If changed Then
                btn_apply.Enabled = True
                btn_apply_close.Enabled = True
            Else
                btn_apply.Enabled = False
                btn_apply_close.Enabled = False
            End If
        End If
    End Sub


    'buttons

    Public Sub tsb_sql_window_Click(sender As Object, e As EventArgs) Handles tsb_sql_window.Click
        Dim New_Field
        Dim text_reaction As TextBox

        If Me.Controls.Find(("SQL_AREA"), True).FirstOrDefault() Is Nothing Then
            Dim result = MsgBox(fn_translate("switch to sql. Do you want really switch to SQL?"), MsgBoxStyle.YesNo, fn_translate("sql_form"))
            If result = vbYes Then
                While p_filter_list.Controls.OfType(Of Panel)().Count > 0
                    For Each ctrl As Control In p_filter_list.Controls.OfType(Of Panel)()
                        ctrl.Dispose()
                    Next
                End While

                New_Field = New TextBox
                New_Field.Name = "SQL_AREA"
                New_Field.text = user_where
                New_Field.size = New Drawing.Size(293, 451)
                New_Field.Multiline = True
                New_Field.ScrollBars = ScrollBars.Both
                New_Field.Location = New Point(0, 0)
                New_Field.BackColor = Color.White
                New_Field.Cursor = Cursors.Hand
                New_Field.enabled = True
                text_reaction = DirectCast(New_Field, TextBox)
                AddHandler text_reaction.TextChanged, AddressOf Me.make_where_cmd
                Me.p_filter_list.Controls.Add(New_Field)

                tsb_show_hide.Enabled = False
            End If
        Else
            If Main_Form.dgw_query_view.Rows.Count = 0 Then
                Dim result = MsgBox(fn_translate("for generating filter must be showned min 1 record"), MsgBoxStyle.OkOnly, fn_translate("back_to_simple_form"))
            Else
                Dim result = MsgBox(fn_translate("command will be deleted. Do you want to continue?"), MsgBoxStyle.YesNo, fn_translate("back_to_simple_form"))
                If result = vbYes Then
                    Me.Controls.Find(("SQL_AREA"), True).FirstOrDefault().Dispose()
                    tsb_show_hide.Enabled = True
                    Main_Form.txt_filter_command.Text = ""
                    user_where = ""
                    If fn_create_filter_form() Then
                        Me.Text = fn_translate(Me.Text)
                        For Each ctrl_object In Me.ts_tools.Items
                            Try
                                ctrl_object.Text = fn_translate(ctrl_object.Text)
                            Catch ex As Exception

                            End Try
                        Next
                    End If
                End If
            End If
        End If

    End Sub



    Private Sub btn_apply_Click(sender As Object, e As EventArgs) Handles btn_apply.Click
        make_where_cmd(sender, e)

        Main_Form.txt_filter_command.Text = Me.where_command
        Main_Form.btn_filter_status.AccessibleDescription = "A"
        Main_Form.btn_filter_status.BackgroundImage = My.Resources.filtered

        Try
            fn_load_basic_form("")
            fn_cursor_waiting(False)
        Catch ex As Exception
            Main_Form.txt_filter_command.Text = ""
            Main_Form.btn_filter_status.AccessibleDescription = "N"
            Main_Form.btn_filter_status.BackgroundImage = My.Resources.not_filtered
            fn_load_basic_form("")
            fn_cursor_waiting(False)
        End Try
    End Sub

    Private Sub btn_apply_close_Click(sender As Object, e As EventArgs) Handles btn_apply_close.Click
        make_where_cmd(sender, e)
        changed = False
        Main_Form.txt_filter_command.Text = Me.where_command
        Main_Form.btn_filter_status.AccessibleDescription = "A"
        Main_Form.btn_filter_status.BackgroundImage = My.Resources.filtered

        Try
            fn_load_basic_form("")
            fn_cursor_waiting(False)
            Me.Close()
        Catch ex As Exception
            Main_Form.txt_filter_command.Text = ""
            Main_Form.btn_filter_status.AccessibleDescription = "N"
            Main_Form.btn_filter_status.BackgroundImage = My.Resources.not_filtered
            fn_load_basic_form("")
            fn_cursor_waiting(False)
        End Try
    End Sub


    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        changed = True
        Me.Close()
    End Sub


    Private Sub tsb_save_Click(sender As Object, e As EventArgs) Handles tsb_save_filter.Click
        If Me.Controls.Find(("SQL_AREA"), True).FirstOrDefault() Is Nothing Then
            make_where_cmd(sender, e)
        Else
            ReDim Main_Form.where_array(5, 0)
        End If

        Dim previous_selected_filter = Main_Form.tv_filter_menu.SelectedNode

        frm_new_menu_item.menu_type = "FM"
        frm_new_menu_item.Menu_id = previous_selected_filter.Name.ToString
        frm_new_menu_item.Show()

        Main_Form.tv_filter_menu.SelectedNode = previous_selected_filter
    End Sub

    Private Sub tsb_save_new_Click(sender As Object, e As EventArgs) Handles tsb_save_new.Click
        If Me.Controls.Find(("SQL_AREA"), True).FirstOrDefault() Is Nothing Then
            make_where_cmd(sender, e)
        Else
            ReDim Main_Form.where_array(5, 0)
        End If
        frm_new_menu_item.menu_type = "FM"
        frm_new_menu_item.Show()

    End Sub



End Class