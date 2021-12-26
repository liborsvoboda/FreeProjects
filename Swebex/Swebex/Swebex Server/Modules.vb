Module Modules

    Function fn_show_module_list() As Boolean
        My.Forms.main_form.lb_modules.Items.Add(fn_translate("mod_settings"))  '1
        My.Forms.main_form.lb_modules.Items.Add(fn_translate("mod_chat_application"))  '1


    End Function



    Function fn_select_module() As Boolean

        If My.Forms.main_form.lb_modules.SelectedItem = fn_translate("mod_chat_application") And My.Forms.main_form.tb_datalist.Contains(My.Forms.main_form.tp_chat_application) = False Then
            My.Forms.main_form.tb_datalist.TabPages.Clear()
            My.Forms.main_form.tb_datalist.TabPages.Insert(0, My.Forms.main_form.tp_chat_application)
        End If

        If My.Forms.main_form.lb_modules.SelectedItem = fn_translate("mod_settings") And My.Forms.main_form.tb_datalist.Contains(My.Forms.main_form.tp_appsetting) = False Then
            My.Forms.main_form.tb_datalist.TabPages.Clear()
            My.Forms.main_form.tb_datalist.TabPages.Insert(0, My.Forms.main_form.tp_appsetting)
            My.Forms.main_form.tb_datalist.TabPages.Insert(1, My.Forms.main_form.tp_chat_settings)
            My.Forms.main_form.tb_datalist.TabPages.Insert(2, My.Forms.main_form.tp_dictionary)
        End If

    End Function


End Module
