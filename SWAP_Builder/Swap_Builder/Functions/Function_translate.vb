Module Function_translate


    Function fn_translate(ByVal word As String) As String
        Dim language_index As Integer = Convert.ToInt32(My.Forms.Main_Form.lb_global_settings_default_language.SelectedIndex + 1)
        Dim system_word As String
        If word.Length = 0 Then
            system_word = ""
        Else
            system_word = word
            Try
                For temp_i = 0 To My.Forms.Main_Form.language_array.GetLength(0)
                    If UCase(My.Forms.Main_Form.language_array(temp_i, 0)) = UCase(word) Then
                        system_word = Replace(My.Forms.Main_Form.language_array(temp_i, language_index), "|", vbNewLine)
                    End If
                Next

                Return system_word.Replace("|", vbNewLine)
            Catch ex As Exception
                Return system_word.Replace("|", vbNewLine)
            End Try
        End If
        Return system_word.Replace("|", vbNewLine)
    End Function


    Function fn_sys_translate(ByVal word As String) As String
        Dim language_index As Integer = Convert.ToInt32(My.Forms.Main_Form.lb_global_settings_default_language.SelectedIndex + 1)
        Dim system_word As String
        If word.Length = 0 Then
            system_word = ""
        Else
            system_word = word
            Try
                For temp_i = 0 To My.Forms.Main_Form.language_array.GetLength(0)
                    If UCase(My.Forms.Main_Form.language_array(temp_i, language_index)) = UCase(word) Then
                        system_word = Replace(My.Forms.Main_Form.language_array(temp_i, 0), "|", vbNewLine)
                    End If
                Next
                Return system_word.Replace("|", vbNewLine)
            Catch ex As Exception
                Return system_word.Replace("|", vbNewLine)
            End Try
        End If
        Return system_word.Replace("|", vbNewLine)
    End Function


    Function fn_main_button_translate()
        Try
            If Main_Form.tc_data.SelectedTab.Name = "tp_datalist" Then 'DEV USER SELECT 
                Main_Form.btn_main_btn_2.Text = fn_translate("edit_rec")
            Else
                Main_Form.btn_main_btn_2.Text = fn_translate("update_rec")
            End If
        Catch ex As Exception
        End Try
    End Function



    Function fn_translate_main_form()


        For Each ctrl_object As Control In Main_Form.tp_datalist.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next



        For Each ctrl_object As TabPage In Main_Form.tc_menu.TabPages
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next


        For Each ctrl_object As TabPage In Main_Form.tc_user_document.TabPages
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next


        For Each ctrl_object As TabPage In Main_Form.tc_data.TabPages
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As TabPage In Main_Form.tc_dev_menu.TabPages
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Main_Form.tp_view_builder.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Main_Form.tp_dev_note.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Main_Form.tp_dev_detail.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Main_Form.tp_dev_sub_forms.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next


        For Each ctrl_object As Control In Main_Form.tp_global_settings.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next


        For Each ctrl_object As Control In Main_Form.tp_filter.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next



        For Each ctrl_object As Control In Main_Form.tp_menu.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As Control In Main_Form.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next


        For Each ctrl_object As Control In Main_Form.cmd_dataview_menu.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object In Main_Form.cmd_dataview_menu.Items
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        For Each ctrl_object As DataGridViewColumn In Main_Form.dev_dgv_subform_bingings.Columns
            Try
                ctrl_object.HeaderText = fn_translate(ctrl_object.HeaderText)
            Catch ex As Exception
            End Try
        Next


        Main_Form.tsm_dataview_set.Text = fn_translate(Main_Form.tsm_dataview_set.Text)
        Main_Form.tstb_records_count.ToolTipText = fn_translate(Main_Form.tstb_records_count.ToolTipText)

    End Function




    Function fn_translate_login_form()

        For Each ctrl_object As Control In frm_logon.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next
    End Function


End Module
