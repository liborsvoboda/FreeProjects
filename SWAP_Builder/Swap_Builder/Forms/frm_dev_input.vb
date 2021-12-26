Public Class frm_dev_input

    Private changed = False
    Private loading = True






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
        For Each ctrl_object As Control In Me.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As TabPage In Me.tp_dev_input.TabPages
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Me.tp_dev_lbl_label.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Me.tp_dev_input_field.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Me.tp_dev_input_caption.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next


        ReDim Main_Form.updated_dev_form_field_list(Main_Form.dev_form_field_list.GetLength(0) - 1, Main_Form.dev_form_field_list.Length / Main_Form.dev_form_field_list.GetLength(0) - 1)
        Array.Copy(Main_Form.dev_form_field_list, Main_Form.updated_dev_form_field_list, Main_Form.dev_form_field_list.Length)


    End Sub


    Private Sub frm_dev_input_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Text = fn_translate("input_property" + " " + Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3 - 2))

        'label
        txt_dev_lbl_size_x.Text = Main_Form.updated_dev_form_field_list(2, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_size_y.Text = Main_Form.updated_dev_form_field_list(3, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_position_x.Text = Main_Form.updated_dev_form_field_list(4, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_position_y.Text = Main_Form.updated_dev_form_field_list(5, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_font_type.Text = Main_Form.updated_dev_form_field_list(6, Main_Form.temp_integer * 3 - 2) + "|" + Main_Form.updated_dev_form_field_list(7, Main_Form.temp_integer * 3 - 2) + "|" + Main_Form.updated_dev_form_field_list(8, Main_Form.temp_integer * 3 - 2)
        p_dev_lbl_textcolor.BackColor = Color.FromArgb(Main_Form.updated_dev_form_field_list(9, Main_Form.temp_integer * 3 - 2))
        p_dev_lbl_backcolor.BackColor = Color.FromArgb(Main_Form.updated_dev_form_field_list(10, Main_Form.temp_integer * 3 - 2))
        chb_dev_lbl_font_bold.Checked = Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3 - 2)
        chb_dev_lbl_strikeout.Checked = Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3 - 2)
        chb_dev_lbl_underline.Checked = Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3 - 2)
        chb_dev_lbl_italic.Checked = Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3 - 2)
        chb_dev_lbl_password.Checked = Main_Form.updated_dev_form_field_list(15, Main_Form.temp_integer * 3 - 2)
        chb_dev_lbl_hidden.Checked = Main_Form.updated_dev_form_field_list(16, Main_Form.temp_integer * 3 - 2)
        chb_dev_lbl_local_db.Checked = Main_Form.updated_dev_form_field_list(28, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_format.Text = Main_Form.updated_dev_form_field_list(17, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_default_value.Text = Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_sql_code.Text = Main_Form.updated_dev_form_field_list(19, Main_Form.temp_integer * 3 - 2)
        txt_dev_lbl_note.Text = Main_Form.updated_dev_form_field_list(20, Main_Form.temp_integer * 3 - 2)

        chb_dev_input_unique_key.Checked = Main_Form.updated_dev_form_field_list(23, Main_Form.temp_integer * 3 - 2)
        'If Main_Form.updated_dev_form_field_list(23, Main_Form.temp_integer * 3 - 2) = 0 Then
        '    chb_dev_input_unique_key.Checked = False
        'Else
        '    chb_dev_input_unique_key.Checked = True
        'End If






        'input
        txt_dev_input_size_x.Text = Main_Form.updated_dev_form_field_list(2, Main_Form.temp_integer * 3 - 1)
        txt_dev_input_size_y.Text = Main_Form.updated_dev_form_field_list(3, Main_Form.temp_integer * 3 - 1)
        'txt_dev_input_position_x.Text = Main_Form.updated_dev_form_field_list(4, Main_Form.temp_integer * 3 - 1)
        'txt_dev_input_position_y.Text = Main_Form.updated_dev_form_field_list(5, Main_Form.temp_integer * 3 - 1)
        txt_dev_input_font_type.Text = Main_Form.updated_dev_form_field_list(6, Main_Form.temp_integer * 3 - 1) + "|" + Main_Form.updated_dev_form_field_list(7, Main_Form.temp_integer * 3 - 1) + "|" + Main_Form.updated_dev_form_field_list(8, Main_Form.temp_integer * 3 - 1)
        p_dev_input_textcolor.BackColor = Color.FromArgb(Main_Form.updated_dev_form_field_list(9, Main_Form.temp_integer * 3 - 1))
        p_dev_input_backcolor.BackColor = Color.FromArgb(Main_Form.updated_dev_form_field_list(10, Main_Form.temp_integer * 3 - 1))

        chb_dev_input_font_bold.Checked = CBool(Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3 - 1))
        chb_dev_input_strikeout.Checked = CBool(Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3 - 1))
        chb_dev_input_underline.Checked = CBool(Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3 - 1))
        chb_dev_input_italic.Checked = CBool(Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3 - 1))
        chb_dev_input_password.Checked = CBool(Main_Form.updated_dev_form_field_list(15, Main_Form.temp_integer * 3 - 1))
        chb_dev_input_hidden.Checked = CBool(Main_Form.updated_dev_form_field_list(16, Main_Form.temp_integer * 3 - 1))
        chb_dev_input_local_db.Checked = Main_Form.updated_dev_form_field_list(28, Main_Form.temp_integer * 3 - 1)

        chb_dev_input_editable.Enabled = Not CBool(Main_Form.updated_dev_form_field_list(24, Main_Form.temp_integer * 3 - 1))
        If CBool(Main_Form.updated_dev_form_field_list(24, Main_Form.temp_integer * 3 - 1)) Then
            chb_dev_input_editable.Checked = False
            Main_Form.updated_dev_form_field_list(29, Main_Form.temp_integer * 3 - 1) = False
        Else
            chb_dev_input_editable.Checked = CBool(Main_Form.updated_dev_form_field_list(29, Main_Form.temp_integer * 3 - 1))
        End If

        txt_dev_input_format.Text = Main_Form.updated_dev_form_field_list(17, Main_Form.temp_integer * 3 - 1)
        txt_dev_input_default_value.Text = Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3 - 1)
        txt_dev_input_sql_code.Text = Main_Form.updated_dev_form_field_list(19, Main_Form.temp_integer * 3 - 1)
        txt_dev_input_note.Text = Main_Form.updated_dev_form_field_list(20, Main_Form.temp_integer * 3 - 1)
        txt_dev_input_field_type.Text = Main_Form.updated_dev_form_field_list(22, Main_Form.temp_integer * 3 - 1)

        If txt_dev_input_field_type.Text = "String" Then
            txt_dev_input_max_length.Enabled = True
        Else
            txt_dev_input_max_length.Text = ""
            txt_dev_input_max_length.Enabled = False
        End If

        If Main_Form.updated_dev_form_field_list(21, Main_Form.temp_integer * 3 - 1) = "DateTimePicker" Then
            txt_dev_input_format.Enabled = True
        Else
            txt_dev_input_format.Enabled = False
        End If


        chb_dev_input_save_to_db.Enabled = CBool(Main_Form.updated_dev_form_field_list(24, Main_Form.temp_integer * 3 - 1)) = False 'isreadonly
        chb_dev_input_save_to_db.Checked = Main_Form.updated_dev_form_field_list(25, Main_Form.temp_integer * 3 - 1) = True  'savetodb
        txt_dev_input_max_length.Text = Main_Form.updated_dev_form_field_list(23, Main_Form.temp_integer * 3 - 1)

        'note
        txt_dev_note_size_x.Text = Main_Form.updated_dev_form_field_list(2, Main_Form.temp_integer * 3)
        txt_dev_note_size_y.Text = Main_Form.updated_dev_form_field_list(3, Main_Form.temp_integer * 3)
        txt_dev_note_position_x.Text = Main_Form.updated_dev_form_field_list(4, Main_Form.temp_integer * 3)
        txt_dev_note_position_y.Text = Main_Form.updated_dev_form_field_list(5, Main_Form.temp_integer * 3)
        txt_dev_note_font_type.Text = Main_Form.updated_dev_form_field_list(6, Main_Form.temp_integer * 3) + "|" + Main_Form.updated_dev_form_field_list(7, Main_Form.temp_integer * 3) + "|" + Main_Form.updated_dev_form_field_list(8, Main_Form.temp_integer * 3)
        p_dev_note_textcolor.BackColor = Color.FromArgb(Main_Form.updated_dev_form_field_list(9, Main_Form.temp_integer * 3))
        p_dev_note_backcolor.BackColor = Color.FromArgb(Main_Form.updated_dev_form_field_list(10, Main_Form.temp_integer * 3))
        chb_dev_note_font_bold.Checked = Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3)
        chb_dev_note_strikeout.Checked = Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3)
        chb_dev_note_underline.Checked = Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3)
        chb_dev_note_italic.Checked = Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3)
        chb_dev_note_password.Checked = Main_Form.updated_dev_form_field_list(15, Main_Form.temp_integer * 3)
        chb_dev_note_hidden.Checked = Main_Form.updated_dev_form_field_list(16, Main_Form.temp_integer * 3)
        chb_dev_note_local_db.Checked = Main_Form.updated_dev_form_field_list(28, Main_Form.temp_integer * 3)
        txt_dev_note_format.Text = Main_Form.updated_dev_form_field_list(17, Main_Form.temp_integer * 3)
        txt_dev_note_default_value.Text = Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3)
        txt_dev_note_sql_code.Text = Main_Form.updated_dev_form_field_list(19, Main_Form.temp_integer * 3)
        txt_dev_note_note.Text = Main_Form.updated_dev_form_field_list(20, Main_Form.temp_integer * 3)


        loading = False
    End Sub





    'fields
    '-----------------------------------------------------------
    Private Sub btn_color_Click(sender As Object, e As EventArgs) Handles btn_dev_lbl_textcolor.Click, btn_dev_lbl_backcolor.Click, btn_dev_input_textcolor.Click, btn_dev_input_backcolor.Click, btn_dev_note_textcolor.Click, btn_dev_note_backcolor.Click

        If color_dialog.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Select Case sender.name
                Case "btn_dev_lbl_textcolor"
                    p_dev_lbl_textcolor.BackColor = color_dialog.Color
                    Main_Form.updated_dev_form_field_list(9, Main_Form.temp_integer * 3 - 2) = p_dev_lbl_textcolor.BackColor.ToArgb.ToString
                Case "btn_dev_lbl_backcolor"
                    p_dev_lbl_backcolor.BackColor = color_dialog.Color
                    Main_Form.updated_dev_form_field_list(10, Main_Form.temp_integer * 3 - 2) = p_dev_lbl_backcolor.BackColor.ToArgb.ToString
                Case "btn_dev_input_textcolor"
                    p_dev_input_textcolor.BackColor = color_dialog.Color
                    Main_Form.updated_dev_form_field_list(9, Main_Form.temp_integer * 3 - 1) = p_dev_input_textcolor.BackColor.ToArgb.ToString
                Case "btn_dev_input_backcolor"
                    p_dev_input_backcolor.BackColor = color_dialog.Color
                    Main_Form.updated_dev_form_field_list(10, Main_Form.temp_integer * 3 - 1) = p_dev_input_backcolor.BackColor.ToArgb.ToString
                Case "btn_dev_note_textcolor"
                    p_dev_note_textcolor.BackColor = color_dialog.Color
                    Main_Form.updated_dev_form_field_list(9, Main_Form.temp_integer * 3) = p_dev_note_textcolor.BackColor.ToArgb.ToString
                Case "btn_dev_note_backcolor"
                    p_dev_note_backcolor.BackColor = color_dialog.Color
                    Main_Form.updated_dev_form_field_list(10, Main_Form.temp_integer * 3) = p_dev_note_backcolor.BackColor.ToArgb.ToString
            End Select
            changed = True
        End If
    End Sub


    Private Sub size_text_changed(sender As Object, e As KeyPressEventArgs) Handles txt_dev_lbl_size_x.KeyPress, txt_dev_lbl_size_y.KeyPress, txt_dev_input_size_x.KeyPress, txt_dev_input_size_y.KeyPress, txt_dev_note_size_x.KeyPress, txt_dev_note_size_y.KeyPress
        fn_numner_keys(e)
        If loading = False Then
            Select Case sender.name
                Case "txt_dev_lbl_size_x"
                    Main_Form.updated_dev_form_field_list(2, Main_Form.temp_integer * 3 - 2) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_lbl_size_y"
                    Main_Form.updated_dev_form_field_list(3, Main_Form.temp_integer * 3 - 2) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_input_size_x"
                    Main_Form.updated_dev_form_field_list(2, Main_Form.temp_integer * 3 - 1) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_input_size_y"
                    Main_Form.updated_dev_form_field_list(3, Main_Form.temp_integer * 3 - 1) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_note_size_x"
                    Main_Form.updated_dev_form_field_list(2, Main_Form.temp_integer * 3) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_note_size_y"
                    Main_Form.updated_dev_form_field_list(3, Main_Form.temp_integer * 3) = sender.text + e.KeyChar
                    changed = True
            End Select
        End If
    End Sub


    Private Sub position_Changed(sender As Object, e As KeyPressEventArgs) Handles txt_dev_lbl_position_x.KeyPress, txt_dev_lbl_position_y.KeyPress, txt_dev_input_max_length.KeyPress
        fn_numner_keys(e)
        If loading = False Then
            Select Case sender.name
                Case "txt_dev_lbl_position_x"
                    Main_Form.updated_dev_form_field_list(4, Main_Form.temp_integer * 3 - 2) = sender.text + e.KeyChar
                    Main_Form.updated_dev_form_field_list(4, Main_Form.temp_integer * 3 - 1) = sender.text + e.KeyChar
                    Main_Form.updated_dev_form_field_list(4, Main_Form.temp_integer * 3) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_lbl_position_y"
                    Main_Form.updated_dev_form_field_list(5, Main_Form.temp_integer * 3 - 2) = sender.text + e.KeyChar
                    Main_Form.updated_dev_form_field_list(5, Main_Form.temp_integer * 3 - 1) = sender.text + e.KeyChar
                    Main_Form.updated_dev_form_field_list(5, Main_Form.temp_integer * 3) = sender.text + e.KeyChar
                    changed = True
                Case "txt_dev_input_max_length"
                    If IsNumeric(sender.text + e.KeyChar.ToString) Then
                        If CDbl(sender.text + e.KeyChar.ToString) > 8000 Then
                            e.KeyChar = Nothing
                        End If
                    End If
                    Main_Form.updated_dev_form_field_list(23, Main_Form.temp_integer * 3 - 1) = sender.text + e.KeyChar
                    changed = True
            End Select
        End If
    End Sub









    Private Sub font_type_Click(sender As Object, e As EventArgs) Handles btn_dev_lbl_font_type.Click, btn_dev_input_font_type.Click, btn_dev_note_font_type.Click

        If font_dialog.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Select Case sender.name
                Case "btn_dev_lbl_font_type"
                    txt_dev_lbl_font_type.Text = font_dialog.Font.Name.ToString + "|" + font_dialog.Font.Size.ToString + "|" + CStr(font_dialog.Font.Unit)
                    chb_dev_lbl_font_bold.Checked = font_dialog.Font.Bold
                    chb_dev_lbl_strikeout.Checked = font_dialog.Font.Strikeout
                    chb_dev_lbl_underline.Checked = font_dialog.Font.Underline
                    chb_dev_lbl_italic.Checked = font_dialog.Font.Italic

                    Main_Form.updated_dev_form_field_list(6, Main_Form.temp_integer * 3 - 2) = font_dialog.Font.Name.ToString
                    Main_Form.updated_dev_form_field_list(7, Main_Form.temp_integer * 3 - 2) = font_dialog.Font.Size.ToString
                    Main_Form.updated_dev_form_field_list(8, Main_Form.temp_integer * 3 - 2) = CStr(font_dialog.Font.Unit)

                    Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3 - 2) = font_dialog.Font.Bold
                    Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3 - 2) = font_dialog.Font.Strikeout
                    Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3 - 2) = font_dialog.Font.Underline
                    Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3 - 2) = font_dialog.Font.Italic
                    changed = True
                Case "btn_dev_input_font_type"
                    txt_dev_input_font_type.Text = font_dialog.Font.Name.ToString + "|" + font_dialog.Font.Size.ToString + "|" + CStr(font_dialog.Font.Unit)
                    chb_dev_input_font_bold.Checked = font_dialog.Font.Bold
                    chb_dev_input_strikeout.Checked = font_dialog.Font.Strikeout
                    chb_dev_input_underline.Checked = font_dialog.Font.Underline
                    chb_dev_input_italic.Checked = font_dialog.Font.Italic

                    Main_Form.updated_dev_form_field_list(6, Main_Form.temp_integer * 3 - 1) = font_dialog.Font.Name.ToString
                    Main_Form.updated_dev_form_field_list(7, Main_Form.temp_integer * 3 - 1) = font_dialog.Font.Size.ToString
                    Main_Form.updated_dev_form_field_list(8, Main_Form.temp_integer * 3 - 1) = CStr(font_dialog.Font.Unit)

                    Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3 - 1) = font_dialog.Font.Bold
                    Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3 - 1) = font_dialog.Font.Strikeout
                    Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3 - 1) = font_dialog.Font.Underline
                    Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3 - 1) = font_dialog.Font.Italic
                    changed = True
                Case "btn_dev_note_font_type"
                    txt_dev_note_font_type.Text = font_dialog.Font.Name.ToString + "|" + font_dialog.Font.Size.ToString + "|" + CStr(font_dialog.Font.Unit)
                    chb_dev_note_font_bold.Checked = font_dialog.Font.Bold
                    chb_dev_note_strikeout.Checked = font_dialog.Font.Strikeout
                    chb_dev_note_underline.Checked = font_dialog.Font.Underline
                    chb_dev_note_italic.Checked = font_dialog.Font.Italic

                    Main_Form.updated_dev_form_field_list(6, Main_Form.temp_integer * 3) = font_dialog.Font.Name.ToString
                    Main_Form.updated_dev_form_field_list(7, Main_Form.temp_integer * 3) = font_dialog.Font.Size.ToString
                    Main_Form.updated_dev_form_field_list(8, Main_Form.temp_integer * 3) = CStr(font_dialog.Font.Unit)

                    Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3) = font_dialog.Font.Bold
                    Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3) = font_dialog.Font.Strikeout
                    Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3) = font_dialog.Font.Underline
                    Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3) = font_dialog.Font.Italic
                    changed = True
            End Select
        End If
    End Sub


    Private Sub checkbox_Changed(sender As Object, e As EventArgs) Handles chb_dev_lbl_font_bold.CheckedChanged, chb_dev_lbl_strikeout.CheckedChanged, chb_dev_lbl_underline.CheckedChanged, chb_dev_lbl_italic.CheckedChanged, chb_dev_lbl_password.CheckedChanged, chb_dev_lbl_hidden.CheckedChanged, chb_dev_input_font_bold.CheckedChanged, chb_dev_input_strikeout.CheckedChanged, chb_dev_input_underline.CheckedChanged, chb_dev_input_italic.CheckedChanged, chb_dev_input_password.CheckedChanged, chb_dev_input_hidden.CheckedChanged, chb_dev_note_font_bold.CheckedChanged, chb_dev_note_strikeout.CheckedChanged, chb_dev_note_underline.CheckedChanged, chb_dev_note_italic.CheckedChanged, chb_dev_note_password.CheckedChanged, chb_dev_note_hidden.CheckedChanged, chb_dev_input_save_to_db.CheckedChanged, chb_dev_input_unique_key.CheckedChanged, chb_dev_lbl_local_db.CheckedChanged, chb_dev_input_local_db.CheckedChanged, chb_dev_note_local_db.CheckedChanged, chb_dev_input_editable.CheckedChanged
        If loading = False Then
            Select Case sender.name
                Case "chb_dev_lbl_font_bold"
                    Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_font_bold.Checked
                    changed = True
                Case "chb_dev_lbl_strikeout"
                    Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_strikeout.Checked
                    changed = True
                Case "chb_dev_lbl_underline"
                    Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_underline.Checked
                    changed = True
                Case "chb_dev_lbl_italic"
                    Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_italic.Checked
                    changed = True
                Case "chb_dev_lbl_password"
                    Main_Form.updated_dev_form_field_list(15, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_password.Checked
                    changed = True
                Case "chb_dev_lbl_hidden"
                    Main_Form.updated_dev_form_field_list(16, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_hidden.Checked
                    changed = True
                Case "chb_dev_lbl_local_db"
                    Main_Form.updated_dev_form_field_list(28, Main_Form.temp_integer * 3 - 2) = chb_dev_lbl_local_db.Checked
                    changed = True
                Case "chb_dev_input_unique_key"
                    If chb_dev_input_unique_key.Checked = True Then
                        Main_Form.updated_dev_form_field_list(23, Main_Form.temp_integer * 3 - 2) = 1
                    Else
                        Main_Form.updated_dev_form_field_list(23, Main_Form.temp_integer * 3 - 2) = 0
                    End If
                    changed = True

                Case "chb_dev_input_save_to_db"
                    Main_Form.updated_dev_form_field_list(25, Main_Form.temp_integer * 3 - 1) = chb_dev_input_save_to_db.Checked
                    changed = True
                Case "chb_dev_input_font_bold"
                    Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3 - 1) = chb_dev_input_font_bold.Checked
                    changed = True
                Case "chb_dev_input_strikeout"
                    Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3 - 1) = chb_dev_input_strikeout.Checked
                    changed = True
                Case "chb_dev_input_underline"
                    Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3 - 1) = chb_dev_input_underline.Checked
                    changed = True
                Case "chb_dev_input_italic"
                    Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3 - 1) = chb_dev_input_italic.Checked
                    changed = True
                Case "chb_dev_input_password"
                    Main_Form.updated_dev_form_field_list(15, Main_Form.temp_integer * 3 - 1) = chb_dev_input_password.Checked
                    changed = True
                Case "chb_dev_input_hidden"
                    Main_Form.updated_dev_form_field_list(16, Main_Form.temp_integer * 3 - 1) = chb_dev_input_hidden.Checked
                    changed = True
                Case "chb_dev_input_local_db"
                    Main_Form.updated_dev_form_field_list(28, Main_Form.temp_integer * 3 - 1) = chb_dev_input_local_db.Checked
                    changed = True
                Case "chb_dev_input_editable"
                    Main_Form.updated_dev_form_field_list(29, Main_Form.temp_integer * 3 - 1) = chb_dev_input_editable.Checked
                    changed = True

                Case "chb_dev_note_font_bold"
                    Main_Form.updated_dev_form_field_list(11, Main_Form.temp_integer * 3) = chb_dev_note_font_bold.Checked
                    changed = True
                Case "chb_dev_note_strikeout"
                    Main_Form.updated_dev_form_field_list(12, Main_Form.temp_integer * 3) = chb_dev_note_strikeout.Checked
                    changed = True
                Case "chb_dev_note_underline"
                    Main_Form.updated_dev_form_field_list(13, Main_Form.temp_integer * 3) = chb_dev_note_underline.Checked
                    changed = True
                Case "chb_dev_note_italic"
                    Main_Form.updated_dev_form_field_list(14, Main_Form.temp_integer * 3) = chb_dev_note_italic.Checked
                    changed = True
                Case "chb_dev_note_password"
                    Main_Form.updated_dev_form_field_list(15, Main_Form.temp_integer * 3) = chb_dev_note_password.Checked
                    changed = True
                Case "chb_dev_note_hidden"
                    Main_Form.updated_dev_form_field_list(16, Main_Form.temp_integer * 3) = chb_dev_note_hidden.Checked
                    changed = True
                Case "chb_dev_note_local_db"
                    Main_Form.updated_dev_form_field_list(28, Main_Form.temp_integer * 3) = chb_dev_note_local_db.Checked
                    changed = True
            End Select
        End If
    End Sub


    Private Sub Text_Changed(sender As Object, e As EventArgs) Handles txt_dev_lbl_format.TextChanged, txt_dev_lbl_default_value.TextChanged, txt_dev_lbl_sql_code.TextChanged, txt_dev_lbl_note.TextChanged, txt_dev_input_format.TextChanged, txt_dev_input_default_value.TextChanged, txt_dev_input_sql_code.TextChanged, txt_dev_input_note.TextChanged, txt_dev_note_format.TextChanged, txt_dev_note_default_value.TextChanged, txt_dev_note_sql_code.TextChanged, txt_dev_note_note.TextChanged
        If loading = False Then

            Select Case sender.name

                Case "txt_dev_lbl_format"
                    Main_Form.updated_dev_form_field_list(17, Main_Form.temp_integer * 3 - 2) = txt_dev_lbl_format.Text
                    changed = True
                Case "txt_dev_lbl_default_value"
                    Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3 - 2) = txt_dev_lbl_default_value.Text
                    changed = True
                Case "txt_dev_lbl_sql_code"
                    Main_Form.updated_dev_form_field_list(19, Main_Form.temp_integer * 3 - 2) = txt_dev_lbl_sql_code.Text
                    changed = True
                Case "txt_dev_lbl_note"
                    Main_Form.updated_dev_form_field_list(20, Main_Form.temp_integer * 3 - 2) = txt_dev_lbl_note.Text
                    changed = True

                Case "txt_dev_input_format"
                    Main_Form.updated_dev_form_field_list(17, Main_Form.temp_integer * 3 - 1) = txt_dev_input_format.Text
                    changed = True
                Case "txt_dev_input_default_value"
                    Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3 - 1) = txt_dev_input_default_value.Text
                    changed = True
                Case "txt_dev_input_sql_code"
                    Main_Form.updated_dev_form_field_list(19, Main_Form.temp_integer * 3 - 1) = txt_dev_input_sql_code.Text
                    changed = True
                Case "txt_dev_input_note"
                    Main_Form.updated_dev_form_field_list(20, Main_Form.temp_integer * 3 - 1) = txt_dev_input_note.Text
                    changed = True

                Case "txt_dev_note_format"
                    Main_Form.updated_dev_form_field_list(17, Main_Form.temp_integer * 3) = txt_dev_note_format.Text
                    changed = True
                Case "txt_dev_note_default_value"
                    Main_Form.updated_dev_form_field_list(18, Main_Form.temp_integer * 3) = txt_dev_note_default_value.Text
                    changed = True
                Case "txt_dev_note_sql_code"
                    Main_Form.updated_dev_form_field_list(19, Main_Form.temp_integer * 3) = txt_dev_note_sql_code.Text
                    changed = True
                Case "txt_dev_note_note"
                    Main_Form.updated_dev_form_field_list(20, Main_Form.temp_integer * 3) = txt_dev_note_note.Text
                    changed = True

            End Select
        End If
    End Sub


    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Me.Close()
        Main_Form.menu_change_reaction(sender, e)
    End Sub


    Private Sub btn_dev_input_save_Click(sender As Object, e As EventArgs) Handles btn_dev_input_save.Click
        Array.Copy(Main_Form.updated_dev_form_field_list, Main_Form.dev_form_field_list, Main_Form.updated_dev_form_field_list.Length)
        fn_change_upd_dev_detail_form_array()
        changed = False
        fn_delete_dev_form(False, 0, False)
    End Sub


    Private Sub btn_dev_input_save_close_Click(sender As Object, e As EventArgs) Handles btn_dev_input_save_close.Click
        Array.Copy(Main_Form.updated_dev_form_field_list, Main_Form.dev_form_field_list, Main_Form.updated_dev_form_field_list.Length)
        fn_change_upd_dev_detail_form_array()
        changed = False
        fn_delete_dev_form(False, 0, False)
        Main_Form.menu_change_reaction(sender, e)
        Me.Close()
    End Sub

End Class

