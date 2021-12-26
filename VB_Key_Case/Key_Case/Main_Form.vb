Imports System.IO.Ports



Public Class Main_Form

    Friend sql_array_count As Integer = 0
    Friend sql_array(0, 0) As String
    Friend available_com_ports As Array

    Friend temp_string As String
    Friend temp_integer As Integer
    Friend selected_picture As Integer
    Friend last_open As Date = DateTime.Now
    Friend opened As Date = DateTime.Now
    Friend selected_key_group As String = 0
    '    Friend inserted_id As String = 0
    Friend open_reason As String = ""

    Friend user_app_folder As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Key_Case")
    Friend config_dir As String = System.IO.Path.Combine(user_app_folder, "DATA")
    Friend configuration_file As String = System.IO.Path.Combine(config_dir, "settings.ini")
    Friend status = True

    Friend returned_string As String
    'Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data

    'hard settings
    Friend one_open_request = True  'one open request, after open case the button is disabled 

    'Private Declare Function ActivateKeyboardLayout Lib "user32.dll" (ByVal myLanguage As Long, Flag As Boolean) As Long
    'Const LANG_CZECH = 1029
    'Const LANG_ENGLISH = 1033
    'Const LANG_FRENCH = 1036
    'Const LANG_GERMAN = 1031
    'Const LANG_ITALIAN = 1040
    'Const LANG_NORWEGIAN = 1043
    'Const LANG_PORTUGUESE = 1046
    'Const LANG_RUSSIAN = 1049
    'Const LANG_SPANISH = 1034
    'Const LANG_UKRAINE = 1058


    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub Main_Form_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txt_request_name.Focus()
    End Sub


    Private Sub main_form_Showned(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        fn_app_load()
        If fn_check_key_for_issue() = False Then txt_request_name.Enabled = False
        ' My.Forms.Frm_logon.Show()
    End Sub

    Private Sub main_Form_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If status = True Then
            Dim result = MsgBox("Chcete aplikaci opravdu zavřít?", MsgBoxStyle.YesNo, "Zavření Aplikace")
            If result = vbYes Then
                If ComPort.IsOpen = True Then ComPort.Close()
                'Application.ExitThread()
            Else
                e.Cancel = True
            End If
        Else
            MessageBox.Show("Nejdříve musíte ukončit pořizovaný záznam!!!")
            e.Cancel = True
        End If
    End Sub


    Private Sub main_Form_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub


    Private Sub pb_waiting_Click(sender As Object, e As EventArgs) Handles pb_waiting_1.Click, pb_waiting_2.Click, pb_waiting_3.Click, pb_waiting_4.Click, pb_waiting_5.Click
        pb_waiting_1.BorderStyle = BorderStyle.None
        pb_waiting_2.BorderStyle = BorderStyle.None
        pb_waiting_3.BorderStyle = BorderStyle.None
        pb_waiting_4.BorderStyle = BorderStyle.None
        pb_waiting_5.BorderStyle = BorderStyle.None
        Select Case sender.name.ToString
            Case "pb_waiting_1"
                pb_waiting_1.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_2"
                pb_waiting_2.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_3"
                pb_waiting_3.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_4"
                pb_waiting_4.BorderStyle = BorderStyle.Fixed3D
            Case "pb_waiting_5"
                pb_waiting_5.BorderStyle = BorderStyle.Fixed3D
            Case Else
        End Select
    End Sub


    Private Sub btn_door_open_Click(sender As Object, e As EventArgs) Handles btn_door_open.Click
        status = False
        If ComPort.IsOpen = False Then


            If rb_issue.Checked = True Then
                If fn_sql_request("SELECT TOP 1 1 FROM [DBNAME].[dba].[user_privyd_klice] WHERE oscislo = '" + txt_request_name.Text + "' AND skupina IS NULL AND CAST(dat_vyd AS DATE) = CAST(GETDATE() AS DATE)  AND (DATALENGTH(poznamka_vyd) = 0 OR poznamka_vyd IS NULL) AND (DATALENGTH(poznamka_pri)=0 OR poznamka_pri IS NULL) ", "SELECT") = False Then
                    fn_sql_request("INSERT INTO [DBNAME].[dba].[user_privyd_klice] ([id_klice],[oscislo],[dat_vyd],[poznamka_vyd])VALUES(ISNULL((SELECT MAX(id_klice) +1 FROM [DBNAME].[dba].[user_privyd_klice]),1),'" + txt_request_name.Text + "',CAST('" + DateTime.Parse(dt_request_datetime.Value).ToString("yyyy-MM-dd hh:mm:ss ").ToString + "' AS DATETIME),'" + txt_note.Text + "')", "INSERT")
                End If

                btn_save_rec.Text = "Uložit Výdej"
                fn_load_cb_key_pack()

                txt_request_name.Enabled = False
                cb_key_pack.Enabled = True
                txt_note.Enabled = True
                If one_open_request = True Then btn_door_open.Enabled = False
                btn_form_clean.Enabled = True
            End If

            If rb_receipt.Checked = True Then
                btn_save_rec.Text = "Uložit Příjem"
                If one_open_request = True Then btn_door_open.Enabled = False
                txt_note.Enabled = True
                btn_save_rec.Enabled = True
            End If

            tspb_status.ToolTipText = "Otevřeno"
            ComPort.PortName = cb_com_ports.SelectedItem
            'cb_com_ports.Items.AddRange(myPort) 'baudrate
            ComPort.BaudRate = "300"
            ComPort.Parity = Parity.None
            ComPort.StopBits = StopBits.One
            ComPort.DataBits = 8
            ComPort.Handshake = Handshake.XOnXOff
            ComPort.RtsEnable = False
            'ComPort.DtrEnable = True

            ComPort.Open()

            opened = Date.Now.AddMilliseconds(Convert.ToInt64(Me.txt_com_signal_length.Text) * 100)
            tspb_status.Value = 50
        Else
            ComPort.Close()
        End If
    End Sub


    Private Sub txt_request_name_set_en_keyboard(sender As Object, e As EventArgs) Handles txt_request_name.GotFocus
        fn_set_keyboard("LANG_ENGLISH")
    End Sub

    Private Sub txt_request_name_set_cz_keyboard(sender As Object, e As EventArgs) Handles txt_request_name.LostFocus
        fn_set_keyboard("LANG_CZECH")
    End Sub



    Private Sub Tc_control_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles tc_control.Selecting
        If e.TabPageIndex = 0 Then
            fn_load_cb_key_pack()
        ElseIf e.TabPageIndex = 1 Then
            If fn_sql_request("SELECT 'Svazek Klíčů' = upk.skupina,'Název' = upk.nazev,'Příjemce' = RTRIM(prs.oscislo) + ' | '+ prs.prijmeni + ' ' + prs.jmeno,'Datum a Čas Vydání' = upk.dat_vyd,'Poznámka Výdeje' = upk.poznamka_vyd FROM [DBNAME].[dba].[user_privyd_klice] upk,[DBNAME].[dba].[pers] prs WHERE CAST(upk.dat_pri AS DATE) = '1901-01-01' AND upk.skupina IS NOT NULL AND prs.oscislo = upk.oscislo", "SELECT") = True Then
                dgv_released_keys.DataSource = dgw_source
            Else
                dgv_released_keys.DataSource = Nothing
            End If
        End If
    End Sub



    Private Sub txt_request_name_TextChanged(sender As Object, e As EventArgs) Handles txt_request_name.TextChanged, txt_request_name.LostFocus

        dt_request_datetime.Value = Now

        If txt_request_name.Text.Length > 0 And fn_sql_request("SELECT pers.[prijmeni] + ' ' + pers.[jmeno],LTRIM(RTRIM(pers.[oscislo])) FROM [DBNAME].[dba].[pers],[DBNAME].[dba].[ppom]  WHERE pers.oscislo = ppom.oscislo AND ppom.pp_od <= GETDATE() AND ppom.pp_do >=GETDATE() AND (pers.[oscislo] ='" + txt_request_name.Text + "' OR ppom.[kod_karty]='" + txt_request_name.Text + "' )", "SELECTONEITEM") = True Then
            txt_selected_name.Text = sql_array(0, 0)
            txt_request_name.Text = sql_array(0, 1)
            btn_door_open.Enabled = True
        Else
            txt_selected_name.Text = ""
            btn_door_open.Enabled = False
        End If

    End Sub


    ' SETTING PART

    Private Sub btn_sql_login_test_Click(sender As Object, e As EventArgs) Handles btn_sql_login_test.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection("Data Source=" + Me.txt_mssql_server.Text + ";Persist Security Info=True;User ID=" + Me.txt_mssql_name.Text + ";Password=" + Me.txt_mssql_password.Text + "")
            'Initial Catalog=" + Me.txt_db_name.Text + ";
            sqlConnection_string.Open()
            MsgBox("Připojení k Databázi proběhlo úspěšně")
            sqlConnection_string.Close()
        Catch
            MessageBox.Show("Připojení k Databázi se nezdařilo, opravte Konfiguraci")
        End Try
        Me.Cursor = Cursors.Default
        My.MySettings.Default.Item("sql_connection") = "Data Source=" + Me.txt_mssql_server.Text + ";Persist Security Info=True;User ID=" + Me.txt_mssql_name.Text + ";Password=" + Me.txt_mssql_password.Text + ""
    End Sub


    Private Sub btn_save_setting_Click(sender As Object, e As EventArgs) Handles btn_save_setting.Click
        fn_save_file_setting()
    End Sub


    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        If ComPort.IsOpen = True And opened <= Date.Now Then
            ComPort.Close()
            last_open = Date.Now.AddSeconds(1)
        Else
            If ComPort.IsOpen = True And chb_data_cable.Checked = True Then
                ComPort.WriteTimeout = 10
                Try
                    ComPort.Write(" ")
                Catch ex As Exception
                End Try
            End If
        End If

        'sec status bar clear after 1 second
        If Date.Now >= last_open And tspb_status.Value = tspb_status.Maximum And ComPort.IsOpen = False Then
            tspb_status.Value = 0
            cb_key_pack.Focus()
        End If

    End Sub


    Private Sub btn_save_rec_Click(sender As Object, e As EventArgs) Handles btn_save_rec.Click
        status = True
        Try

            If rb_issue.Checked = True Then
                fn_sql_request("UPDATE [DBNAME].[dba].[user_privyd_klice] SET skupina='" + selected_key_group.ToString + "',nazev=(SELECT TOP 1 uk.nazev FROM [DBNAME].[dba].user_kliccis uk WHERE uk.skupina='" + selected_key_group.ToString + "' AND uk.platnost=1 ), poznamka_vyd = '" + txt_note.Text + "' WHERE oscislo= '" + txt_request_name.Text.ToString + "' AND skupina IS NULL AND (DATALENGTH(poznamka_vyd) = 0 OR poznamka_vyd IS NULL) AND (DATALENGTH(poznamka_pri) = 0 OR poznamka_pri IS NULL)   ", "UPDATE")
            ElseIf rb_receipt.Checked = True Then
                fn_sql_request("UPDATE [DBNAME].[dba].[user_privyd_klice] SET dat_pri=CAST('" + DateTime.ParseExact(dt_request_datetime.Text, "dd.MM.yyyy hh:mm:ss", Nothing).ToString("yyyy-MM-dd hh:mm:ss ") + "' AS DATETIME),poznamka_pri='" + txt_note.Text + "' WHERE oscislo='" + txt_request_name.Text.ToString + "' AND skupina = '" + cb_key_pack.Text.ToString + "' AND CAST(dat_pri AS DATE) = '1901-01-01' ", "UPDATE")
            End If

        Catch ex As Exception
        End Try

        btn_form_clean.Enabled = False
        rb_issue.Checked = True
        If fn_check_key_for_issue() = True Then txt_request_name.Enabled = True
        selected_key_group = ""

        cb_key_pack.Text = ""
        txt_note.Text = ""
        txt_searched_pack.Text = ""
        txt_request_name.Text = ""

        btn_door_open.Enabled = False
        cb_key_pack.Enabled = False
        btn_save_rec.Text = "Uložit Výdej"
        btn_save_rec.Enabled = False
        txt_note.Enabled = False

        If fn_check_key_for_issue() = True Then txt_request_name.Focus()
    End Sub


    Private Sub txt_com_signal_lenght_TextChanged(sender As Object, e As KeyPressEventArgs) Handles txt_com_signal_length.KeyPress
        fn_number_keys(e)
    End Sub


    Private Sub cb_key_pack_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_key_pack.SelectedIndexChanged, cb_key_pack.LostFocus, cb_key_pack.TextChanged
        If fn_detect_key_pack(cb_key_pack.Text) = True Then
            btn_save_rec.Enabled = True
        Else
            btn_save_rec.Enabled = False
        End If
    End Sub


    Private Sub dgv_released_keys_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_released_keys.CellContentDoubleClick
        If e.RowIndex >= 0 Then
            If (btn_door_open.Enabled = True And txt_note.Enabled = False) Or (btn_door_open.Enabled = False And txt_note.Enabled = False) Then
                rb_receipt.Checked = True
                Dim person_no = dgv_released_keys.Item(2, e.RowIndex).Value.ToString.Split("|")
                txt_request_name.Text = person_no(0)
                btn_door_open.Enabled = True
                cb_key_pack.Text = dgv_released_keys.Item(0, e.RowIndex).Value
                btn_save_rec.Text = "Uložit Příjem"
                btn_save_rec.Enabled = False
                cb_key_pack.Enabled = False
                tc_control.SelectedIndex = 0
                btn_form_clean.Enabled = True
            End If
        End If
    End Sub


    Private Sub btn_form_clean_Click(sender As Object, e As EventArgs) Handles btn_form_clean.Click
        If (btn_door_open.Enabled = False And one_open_request = True) Or (btn_door_open.Enabled = True And one_open_request = False And txt_note.Enabled = True) Then
            Me.Enabled = False
            frm_open_reason.ShowDialog()
            If open_reason.Length > 0 Then
                btn_form_clean.Enabled = False

                If rb_issue.Checked = True Then
                    fn_sql_request("UPDATE [DBNAME].[dba].[user_privyd_klice] SET poznamka_vyd='Důvod otevření: " + open_reason + "' WHERE id_klice=ISNULL((SELECT MAX(upk.id_klice) FROM [DBNAME].[dba].[user_privyd_klice] upk  WHERE upk.skupina IS NULL AND oscislo = '" + txt_request_name.Text + "' AND (DATALENGTH(upk.poznamka_vyd) = 0 OR upk.poznamka_vyd IS NULL) AND (DATALENGTH(upk.poznamka_pri)=0 OR upk.poznamka_pri IS NULL)  ),0)", "UPDATE")
                ElseIf rb_receipt.Checked = True Then
                    fn_sql_request("INSERT INTO [DBNAME].[dba].[user_privyd_klice] ([id_klice],[oscislo],[dat_pri],[poznamka_pri])VALUES(ISNULL((SELECT MAX(id_klice) +1 FROM [DBNAME].[dba].[user_privyd_klice]),1),'" + txt_request_name.Text + "',CAST('" + DateTime.Parse(dt_request_datetime.Value).ToString("yyyy-MM-dd hh:mm:ss ").ToString + "' AS DATETIME),'Důvod otevření: " + open_reason + "')", "INSERT")
                End If

                selected_key_group = ""
                rb_issue.Checked = True
                If fn_check_key_for_issue() = True Then txt_request_name.Enabled = True
                txt_request_name.Text = ""
                cb_key_pack.Text = ""
                cb_key_pack.Items.Clear()
                txt_note.Text = ""
                btn_door_open.Enabled = False
                cb_key_pack.Enabled = False
                txt_note.Enabled = False
                btn_save_rec.Text = "Uložit Výdej"
                btn_save_rec.Enabled = False
                If fn_check_key_for_issue() = True Then txt_request_name.Focus()
                status = True
                open_reason = ""
            End If
        ElseIf txt_note.Enabled = False Then
            btn_form_clean.Enabled = False
            selected_key_group = ""
            rb_issue.Checked = True
            If fn_check_key_for_issue() = True Then txt_request_name.Enabled = True
            txt_request_name.Text = ""
            cb_key_pack.Text = ""
            cb_key_pack.Items.Clear()
            txt_note.Text = ""
            btn_door_open.Enabled = False
            cb_key_pack.Enabled = False
            txt_note.Enabled = False
            btn_save_rec.Text = "Uložit Výdej"
            btn_save_rec.Enabled = False
            If fn_check_key_for_issue() = True Then txt_request_name.Focus()
            status = True
        End If

    End Sub



End Class


