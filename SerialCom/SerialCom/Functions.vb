Module Functions

    Function fn_get_application_path() As String
        Dim path As String
        path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
        fn_get_application_path = path.Replace("file:\", "")
    End Function

    Function fn_create_directory(ByVal directory As String) As Boolean
        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Function

    Function fn_check_directory(ByVal directory As String) As Boolean
        fn_check_directory = System.IO.Directory.Exists(directory)
    End Function

    Function fn_check_file(ByVal file As String) As Boolean
        fn_check_file = System.IO.File.Exists(file)
    End Function


    Function fn_create_file(ByVal file As String) As Boolean
        If Not System.IO.File.Exists(file) Then
            System.IO.File.Create(file).Close()
        End If

        If fn_check_file(file) = True Then
            fn_create_file = True
        Else
            fn_create_file = False
        End If
    End Function

    Function fn_delete_file(ByVal file As String) As Boolean
        If fn_check_file(file) = True Then System.IO.File.Delete(file)

        If fn_check_file(file) = False Then
            fn_delete_file = True
        Else
            fn_delete_file = False
        End If
    End Function

    Function fn_load_setting() As Boolean
        Try
            Dim config_line
            fn_load_setting = False

            If fn_check_directory(My.Forms.main_form.config_dir) = False Then
                fn_create_directory(My.Forms.main_form.config_dir)
            End If

            If fn_check_file(My.Forms.main_form.config_file) = False Then
                MsgBox("Konfigurační soubor neexistuje, Proveďte konfiguraci")
            Else
                Dim objReader As New System.IO.StreamReader(My.Forms.main_form.config_file, True)
                config_line = objReader.ReadLine()
                objReader.Close()
                Dim temp As String() = Split(config_line, "#")

                My.Forms.main_form.cb_com_ports.SelectedItem = temp(0).ToString
                My.Forms.main_form.cb_baud_rate.SelectedItem = temp(1).ToString
                My.Forms.main_form.txt_keystring_length.Text = temp(2).ToString
                If temp(3).ToString = "True" Then My.Forms.main_form.chb_autoenter.Checked = True
                If temp(4).ToString = "True" Then My.Forms.main_form.chb_autotab.Checked = True
                If temp(5).ToString = "True" Then My.Forms.main_form.chb_init_after_start.Checked = True
            End If
            fn_load_setting = True
        Catch ex As Exception
            MessageBox.Show("Chybné nastavení konfiguračního souboru" + vbNewLine + "Proveďte novou konfiguraci")
        End Try
    End Function


    Function fn_save_file_setting() As Boolean
        Try
            If fn_check_file(My.Forms.main_form.config_file) = True Then fn_delete_file(My.Forms.main_form.config_file)
            fn_create_file(My.Forms.main_form.config_file)
            Dim objWriter As New System.IO.StreamWriter(My.Forms.main_form.config_file, True)

            Dim checkbox_status As String = "False"
            If My.Forms.main_form.chb_autoenter.Checked = True Then checkbox_status = "True"
            If My.Forms.main_form.chb_autotab.Checked = True Then
                checkbox_status &= "#" + "True"
            Else
                checkbox_status &= "#" + "False"
            End If
            If My.Forms.main_form.chb_init_after_start.Checked = True Then
                checkbox_status &= "#" + "True"
            Else
                checkbox_status &= "#" + "False"
            End If
            objWriter.WriteLine(My.Forms.main_form.cb_com_ports.SelectedItem + "#" + My.Forms.main_form.cb_baud_rate.SelectedItem + "#" + My.Forms.main_form.txt_keystring_length.Text + "#" + checkbox_status)
            objWriter.Close()
            MessageBox.Show("Nastavení bylo uloženo")
            fn_save_file_setting = True
            My.Forms.main_form.Tb_Control.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Konfigurační soubor se nepodařilo uložit." + vbNewLine + " Zkontrolujte práva k souboru:" + vbNewLine + My.Forms.main_form.config_file)
        End Try
    End Function



    Function CheckSelectedPort() As Boolean
        If Convert.ToInt32(My.Forms.main_form.cb_com_ports.SelectedIndex) = -1 Then
            My.Forms.main_form.btn_com_initialize.Enabled = False
            CheckSelectedPort = False
        Else
            My.Forms.main_form.btn_com_initialize.Enabled = True
            CheckSelectedPort = True
        End If
    End Function

End Module
