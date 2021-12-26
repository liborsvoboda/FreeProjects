Module app_translate

    Function fn_translate_app() As Boolean
        fn_translate_app = False
        Try

            'application name
            My.Forms.main_form.Text = fn_translate("app_name")

            'tabpages
            My.Forms.main_form.tp_dictionary.Text = fn_translate("app_dictionary")
            My.Forms.main_form.tp_appsetting.Text = fn_translate("app_settings")
            My.Forms.main_form.tp_chat_settings.Text = fn_translate("app_chat_settings")
            My.Forms.main_form.tp_chat_application.Text = fn_translate("mod_chat_application")

            'labels
            My.Forms.main_form.lbl_modules.Text = fn_translate("lbl_modules")
            My.Forms.main_form.lbl_default_lang.Text = fn_translate("lbl_default_lang")
            My.Forms.main_form.lbl_word.Text = fn_translate("lbl_word")
            My.Forms.main_form.lbl_language.Text = fn_translate("lbl_language")
            My.Forms.main_form.lbl_translate.Text = fn_translate("lbl_translate")
            My.Forms.main_form.lbl_server_user.Text = fn_translate("lbl_server_user")
            My.Forms.main_form.lbl_ip_address.Text = fn_translate("lbl_ip_address")
            My.Forms.main_form.lbl_domain.Text = fn_translate("lbl_domain")
            My.Forms.main_form.lbl_pc_name.Text = fn_translate("lbl_pc_name")
            My.Forms.main_form.lbl_chat_port.Text = fn_translate("lbl_chat_port")
            My.Forms.main_form.lbl_lbl_log_line_limit.Text = fn_translate("lbl_log_line_limit")
            My.Forms.main_form.lbl_delay_notification.Text = fn_translate("lbl_delay_notification")

            'ListView
            My.Forms.main_form.lv_chat_Client_list_Status.Text = fn_translate("lbl_status")
            My.Forms.main_form.lv_chat_Client_list_sessionid.Text = fn_translate("lbl_sessionid")
            My.Forms.main_form.lv_chat_client_list_machineid.Text = fn_translate("lbl_machineid")



            'buttons
            My.Forms.main_form.btn_dictionary_save.Text = fn_translate("btn_save")
            My.Forms.main_form.btn_chat_save.Text = fn_translate("btn_save")
            My.Forms.main_form.btn_app_settings_save.Text = fn_translate("btn_save")
            My.Forms.main_form.btn_clear_form.Text = fn_translate("btn_clear_form")
            My.Forms.main_form.btn_chat_clear_history.Text = fn_translate("btn_clear_history")
            My.Forms.main_form.btn_chat_send_to_All.Text = fn_translate("btn_chat_send_to_All")
            My.Forms.main_form.btn_chat_disconnect_all.Text = fn_translate("btn_chat_disconnect_all")
            My.Forms.main_form.btn_open_log_file.Text = fn_translate("btn_open_log_file")
            My.Forms.main_form.btn_show_files.Text = fn_translate("btn_show_files")

            'ContextMenuStrip
            My.Forms.main_form.cms_send_text.Text = fn_translate("cms_send_text")
            My.Forms.main_form.cms_send_file.Text = fn_translate("cms_send_file")
            My.Forms.main_form.cms_disconnect_client.Text = fn_translate("cms_disconnect_client")
            My.Forms.main_form.ts_about_app.Text = fn_translate("ts_about_app")
            My.Forms.main_form.ts_exit_app.Text = fn_translate("ts_exit_app")
            My.Forms.main_form.ts_show_hidden.Text = fn_translate("ts_hidden")


            'checkbox
            My.Forms.main_form.chb_logging_chat_messages.Text = fn_translate("chb_logging_chat_messages")


            fn_translate_app = True
        Catch ex As Exception
        End Try
    End Function



End Module
