'Imports System.Text
'Imports System.Net
'Imports System.Net.Sockets
'Imports System.Threading

Public Class main_form

    ''DECLARATION
    ''folders
    Public config_folder = System.IO.Path.Combine(Application.StartupPath, "config")
    Public log_folder = System.IO.Path.Combine(Application.StartupPath, "logs")
    Public vbscript_folder = System.IO.Path.Combine(Application.StartupPath, "vbscipts")
    Public FilesFolder As String = System.IO.Path.Combine(Application.StartupPath, "Files")

    'files
    Public language_file = System.IO.Path.Combine(Application.StartupPath, "config\language.txt")
    Public config_file = System.IO.Path.Combine(Application.StartupPath, "config\config.ini")
    Public chat_log_file = System.IO.Path.Combine(Application.StartupPath, "logs\chat.log")
    Public clients_file = System.IO.Path.Combine(Application.StartupPath, "logs\clients.log")


    'arrays
    Public file_array(0, 0) As String
    Public language_array(0, 0) As String
    Public config_array(0, 0) As String
    Public clients_array(0, 3) As String    'd.Name,checked_port,local_user,False 'connected status


    'variables
    Public localAddr As System.Net.IPAddress = System.Net.IPAddress.Any
    Public local_user_name As String = System.Net.Dns.GetHostName() + "\" + fn_GetUserName()
    Friend Shared server As Swebex_Server.chat_Server
    Friend Shared lat As Swebex_Server.chat_Utilities.LargeArrayTransferHelper
    Public IsClosing As Boolean = False
    Public client As New Swebex_Server.chat_Client(AddressOf UpdateUIClient, True, 30)
    Public last_check As Date = DateTime.Now
    Public waiting_string As String = "."
    Public founded_clients As String = Nothing
    Public app_closing As Boolean = False
    Public IsStarting = True


    'Public Shared check_all_ip_ports = New Thread(AddressOf fn_checkAllIP) With {
    '.IsBackground = True, .Name = "CheckAPPipport"}

    'constants
    Const search_interval As Integer = 90

    

    'temp variables
    Public line_no As Double
    Public split_pointer As Integer
    Public temp_str As String()
    ''END OF DECLARATION



    ''DELEGATES
    Public WithEvents connect_to_another_clients As System.ComponentModel.BackgroundWorker = New System.ComponentModel.BackgroundWorker With {
        .WorkerReportsProgress = True And .WorkerSupportsCancellation = True}

    '' END OF DELEGATES


    ''DECLARATION END




    ''START APP

    Private Sub main_form_Load(sender As Object, e As EventArgs) Handles Me.Load
        fn_check_file_structure()

        'LOAD_LANGUAGE
        fn_load_data_file_to_array(language_file, ";", False)
        ReDim language_array(file_array.GetLength(0) - 1, (file_array.Length / file_array.GetLength(0)) - 1)
        Array.Copy(file_array, language_array, file_array.Length)
        fn_load_languages()


        'functions
        Me.txt_domain.Text = fn_GetUserDomain()
        Me.txt_server_user.Text = fn_GetUserName()
        Me.txt_ipaddresses.Text = fn_GetIPv4Address()
        Me.txt_pc_name.Text = System.Net.Dns.GetHostName()
        Me.txt_chat_port.Text = 8000


        'LOAD SETTINGS
        fn_load_setting()

        'LOAD MODULES
        tb_datalist.TabPages.Clear()
        fn_show_module_list()

        'LOAD CLIENTS
        fn_load_data_file_to_array(clients_file, ";", False)
        ReDim clients_array(file_array.GetLength(0) - 1, (file_array.Length / file_array.GetLength(0)) - 1)
        Array.Copy(file_array, clients_array, file_array.Length)
        fn_connect_to_array_clients()


    End Sub

    Private Sub main_form_Showned(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        fn_translate_app()


        'AUTOSTARTS
        If IsStarting = True Then
            fn_start_chat_server()
            If fn_get_file_line_count(chat_log_file) > 1 And Convert.ToDouble(txt_chat_log_line_limit.Text) <> 0 Then fn_remove_file_lines(chat_log_file, Convert.ToDouble(txt_chat_log_line_limit.Text))
            IsStarting = False
        Else

            If Me.Visible = True Then
                Me.ts_show_hidden.Text = fn_translate("ts_hidden")
            Else
                Me.ts_show_hidden.Text = fn_translate("ts_show")
            End If
        End If
    End Sub


    ''END OF START APP



    ''START CLOSING APP
    Private Sub MainWindow_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If app_closing = False Then
            If MsgBox(fn_translate("msg_really_close_app"), 4096 + 16 + 4) = vbYes Then
                app_closing = True
                fn_exit_app()
            Else
                e.Cancel = True
            End If
        End If
    End Sub
    ''END OF CLOSING APP














    '' START OF MODULE SELECTION
    Private Sub lb_modules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lb_modules.SelectedIndexChanged
        fn_select_module()
    End Sub
    '' END OF MODULE SELECTION




    ''TRAY ICON/MENU START

    Private Sub ni_tray_menu_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ni_tray_icon.MouseDoubleClick
        If Me.Visible = True Then
            Me.Hide()
        Else
            Me.Show()
        End If
    End Sub

    Private Sub ts_show_hidden_mouseClick(sender As Object, e As EventArgs) Handles ts_show_hidden.Click
        If Me.Visible = True Then
            Me.Hide()
        Else
            Me.Show()
        End If
    End Sub


    Private Sub ni_tray_menu_MouseRightClick(sender As Object, e As MouseEventArgs) Handles ni_tray_icon.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
        End If

    End Sub


    Private Sub ts_exit_app_Click(sender As Object, e As EventArgs) Handles ts_exit_app.Click
        If MsgBox(fn_translate("msg_really_close_app"), 4096 + 16 + 4) = vbYes Then
            app_closing = True
            fn_exit_app()
        End If
    End Sub


    ''TRAY ICON/MENU END











    ''MODULES


    ''DICTIONARY START
    Private Sub dictionary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_word.SelectedIndexChanged, cb_languages.SelectedIndexChanged
        If Me.cb_word.SelectedItem <> "" And Me.cb_languages.SelectedItem <> "" Then
            Me.txt_translate.Text = fn_translate_with_current_index(Me.cb_word.SelectedItem, cb_languages.SelectedIndex + 1)
        End If

        If cb_word.SelectedIndex = -1 Or cb_languages.SelectedIndex = -1 Then txt_translate.Text = ""

    End Sub



    Private Sub btn_clear_form_Click(sender As Object, e As EventArgs) Handles btn_clear_form.Click
        Me.cb_word.SelectedIndex = -1
        Me.cb_languages.SelectedIndex = -1
        Me.txt_translate.Text = ""
    End Sub



    Private Sub btn_dictionary_save_Click(sender As Object, e As EventArgs) Handles btn_dictionary_save.Click
        If cb_word.Text <> "" And cb_languages.Text <> "" And txt_translate.Text <> "" Then

            If cb_word.SelectedIndex = -1 And cb_languages.SelectedIndex = -1 Then
                ReDim Preserve language_array(language_array.GetLength(0) - 1, (language_array.Length / language_array.GetLength(0)))
                language_array(0, cb_languages.Items.Count + 1) = cb_languages.Text
                Me.language_array.SetValue(cb_word.Text, Me.language_array.GetLength(0) - 1, 0)
                Me.language_array.SetValue(txt_translate.Text, Me.language_array.GetLength(0) - 1, cb_languages.Items.Count + 1)
            ElseIf cb_word.SelectedIndex <> -1 And cb_languages.SelectedIndex = -1 Then
                ReDim Preserve language_array(language_array.GetLength(0) - 1, (language_array.Length / language_array.GetLength(0)))
                language_array(0, cb_languages.Items.Count + 1) = cb_languages.Text
                For i = 0 To Me.language_array.GetLength(0) - 1
                    If language_array(i, 0) = cb_word.Text Then language_array(i, cb_languages.Items.Count + 1) = txt_translate.Text
                Next
            ElseIf cb_word.SelectedIndex = -1 And cb_languages.SelectedIndex <> -1 Then
                Me.language_array.SetValue(cb_word.Text, Me.language_array.GetLength(0) - 1, 0)
                Me.language_array.SetValue(txt_translate.Text, Me.language_array.GetLength(0) - 1, cb_languages.SelectedIndex + 1)
            Else
                For i = 0 To Me.language_array.GetLength(0) - 1
                    If language_array(i, 0) = cb_word.Text Then language_array(i, cb_languages.SelectedIndex + 1) = txt_translate.Text
                Next
            End If


            Me.cb_word.SelectedIndex = -1
            Me.cb_word.Text = ""
            Me.cb_languages.SelectedIndex = -1
            Me.cb_languages.Text = ""
            Me.txt_translate.Text = ""


            'SAVE LANGUAGE
            ReDim file_array(language_array.GetLength(0) - 1, (language_array.Length / language_array.GetLength(0)) - 1)
            Array.Copy(language_array, file_array, language_array.Length)
            fn_save_data_file_from_array(language_file, ";", False)


            'RELOAD_LANGUAGE
            fn_load_data_file_to_array(language_file, ";", False)
            ReDim language_array(file_array.GetLength(0) - 1, (file_array.Length / file_array.GetLength(0)) - 1)
            Array.Copy(file_array, language_array, file_array.Length)
            fn_load_languages()

            fn_translate_app()
        Else
            MessageBox.Show(fn_translate("msg_yellow_fields_required"))
        End If
    End Sub
    ''DICTIONARY END



    ''CHAT START

    Public Sub UpdateUIServer(ByVal bytes() As Byte, ByVal sessionID As Int32, ByVal dataChannel As Byte)

        If lat.HandleIncomingBytes(bytes, dataChannel, sessionID) Then Return

        If Me.InvokeRequired() Then
            ' InvokeRequired: We're running on the background thread. Invoke the delegate.
            Me.Invoke(server.ServerCallbackObject, bytes, sessionID, dataChannel)
        Else
            ' We're on the main UI thread now.
            If dataChannel < 251 Then
                rtb_chat_history.AppendText("Session " & sessionID.ToString & ": " & Swebex_Server.chat_Utilities.BytesToString(bytes) + vbNewLine)
                If chb_logging_chat_messages.Checked = True Then fn_write_to_log(Me.chat_log_file, "Session " & sessionID.ToString & ": " & Swebex_Server.chat_Utilities.BytesToString(bytes) + vbNewLine)

                '                temp_str = Split(Swebex_Server.chat_Utilities.BytesToString(bytes).ToString, "<||>")


                If chb_logging_chat_messages.Checked = True Then fn_write_to_log(chat_log_file, "Session " & sessionID.ToString & ": " & Swebex_Server.chat_Utilities.BytesToString(bytes))
            ElseIf dataChannel = 255 Then
                Dim tmp = ""
                Dim msg As String = Swebex_Server.chat_Utilities.BytesToString(bytes)
                Dim dontReport As Boolean = False

                ' server has finished sending the bytes you put into sendBytes()
                If msg.Length > 3 Then tmp = msg.Substring(0, 3)
                If tmp = "UBS" Then ' User Bytes Sent.
                    Dim parts() As String = Split(msg, "UBS:")
                    msg = "Data sent to session: " & parts(1)
                End If

                If msg = "Connected." Then UpdateClientsList()
                If msg.Contains(" MachineID:") Then UpdateClientsList()
                If msg.Contains("Session Stopped. (") Then UpdateClientsList()

                'If Not dontReport Then Me.lbl_chat_server_status.Text = msg
            End If
        End If

    End Sub


    Public Sub UpdateUIClient(ByVal bytes() As Byte, ByVal dataChannel As Byte)

        If Not lat Is Nothing AndAlso lat.HandleIncomingBytes(bytes, dataChannel, , {100, 100}) Then Return

        If Me.InvokeRequired() Then
            ' InvokeRequired: We're running on the background thread. Invoke the delegate.
            Me.Invoke(client.ClientCallbackObject, bytes, dataChannel)
        Else
            ' We're on the main UI thread now.
            Dim dontReport As Boolean = False

            If dataChannel < 251 Then
                If bytes.Length < 65535 Then
                    fn_insert_colored_chat_history(Swebex_Server.chat_Utilities.BytesToString(bytes).ToString())
                Else
                    ' This is a large array delivered by LAT. Display it in the 
                    ' large transfer viewer form.
                    rtb_chat_history.AppendText("Received LAT packet containing " & bytes.Length.ToString & " bytes." + vbNewLine)
                    If chb_logging_chat_messages.Checked = True Then fn_write_to_log(Me.chat_log_file, "Received LAT packet containing " & bytes.Length.ToString & " bytes." + vbNewLine)

                    Dim viewer As frmLatViewer = New frmLatViewer
                    viewer.PassBytes(bytes)
                    viewer.Show()
                End If

            ElseIf dataChannel = 255 Then
                Dim msg As String = Swebex_Server.chat_Utilities.BytesToString(bytes)
                Dim tmp As String = ""

                ' Display info about the incoming file:
                If msg.Length > 15 Then tmp = msg.Substring(0, 15)
                If tmp = "Receiving file:" Then

                    'If btGetFile.Text = "Get File" Then btGetFile.Text = "Cancel"
                    'gbGetFilePregress.Text = "Receiving: " & client.GetIncomingFileName
                    dontReport = True
                End If

                ' Display info about the outgoing file:
                If msg.Length > 13 Then tmp = msg.Substring(0, 13)
                If tmp = "Sending file:" Then
                    'gbSendFileProgress.Text = "Sending: " & client.GetOutgoingFileName
                    dontReport = True
                End If

                ' The file being sent to the client is complete.
                If msg = "->Done" Then
                    'gbGetFilePregress.Text = "File->Client: Transfer complete."
                    'btGetFile.Text = "Get File"
                    dontReport = True
                End If

                ' The file being sent to the server is complete.
                If msg = "<-Done" Then
                    'gbSendFileProgress.Text = "File->Server: Transfer complete."
                    'btSendFile.Text = "Send File"
                    dontReport = True
                End If

                ' The file transfer to the client has been aborted.
                If msg = "->Aborted." Then
                    'gbGetFilePregress.Text = "File->Client: Transfer aborted."
                    dontReport = True
                End If

                ' The file transfer to the server has been aborted.
                If msg = "<-Aborted." Then
                    'gbSendFileProgress.Text = "File->Server: Transfer aborted."
                    dontReport = True
                End If

                ' _Client as finished sending the bytes you put into sendBytes()
                If msg.Length > 4 Then tmp = msg.Substring(0, 4)
                If tmp = "UBS:" Then ' User Bytes Sent on channel:???.
                    'btSendText.Enabled = True
                    dontReport = True
                End If

                ' We have an error message. Could be local, or from the server.
                If msg.Length > 4 Then tmp = msg.Substring(0, 5)
                If tmp = "ERR: " Then
                    Dim msgParts() As String
                    msgParts = Split(msg, ": ")
                    MsgBox("" & msgParts(1), MsgBoxStyle.Critical, "Test Tcp Communications App")
                    dontReport = True
                End If

                If msg.Equals("Disconnected.") Then
                    'btn_chat_connect.Text = fn_translate("btn_chat_connect_start", lb_default_language.SelectedIndex + 1)
                    'tss_chat_status.BackColor = Color.Red
                End If

            End If
        End If

    End Sub


    Private Sub UpdateClientsList()

        Dim sessionList As List(Of Swebex_Server.chat_Server.SessionCommunications) = server.GetSessionCollection()
        Dim lvi As ListViewItem

        Me.lv_chat_Client_list.Items.Clear()

        For Each session As Swebex_Server.chat_Server.SessionCommunications In sessionList
            If session.IsRunning Then
                lvi = New ListViewItem(" Connected", 0, lv_chat_Client_list.Groups.Item(0))
            Else
                lvi = New ListViewItem(" Disconnected", 1, lv_chat_Client_list.Groups.Item(1))
            End If

            lvi.SubItems.Add(session.sessionID.ToString())
            lvi.SubItems.Add(session.machineId)
            Me.lv_chat_Client_list.Items.Add(lvi)
        Next

        If Me.lv_chat_Client_list.Items.Count > 0 Then
            btn_chat_send_to_All.Enabled = True
            btn_chat_disconnect_all.Enabled = True
        Else
            btn_chat_send_to_All.Enabled = False
            btn_chat_disconnect_all.Enabled = False
        End If

    End Sub


    Private Sub SendAFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles cms_send_file.Click
        If lv_chat_Client_list.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = lv_chat_Client_list.SelectedItems.Item(0)
            ' Get the session using the sessionID pulled from the selected listview item
            Dim session As Swebex_Server.chat_Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))
            Dim message As String
            Dim fileName As String

            If session Is Nothing Then
                MsgBox("This session is disconnected.", MsgBoxStyle.Critical)
                Return
            End If

            message = "Select a file to send to " & lvi.SubItems(2).Text

            chat_ofdSendFileToClient.Title = message
            chat_ofdSendFileToClient.FileName = ""
            chat_ofdSendFileToClient.ShowDialog()
            fileName = chat_ofdSendFileToClient.FileName

            If fileName.Trim().Equals("") Then Exit Sub

            If Not server.SendFile(fileName, session.sessionID) Then
                MsgBox("This session is disconnected.", MsgBoxStyle.Critical)
            Else
                If session IsNot Nothing Then server.SendText("<||>" + Path.GetFileName(fileName))
            End If
        End If
    End Sub



    Private Sub SendTextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles cms_send_text.Click
        If lv_chat_Client_list.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = lv_chat_Client_list.SelectedItems.Item(0)
            ' Get the session using the sessionID pulled from the selected listview item
            Dim session As Swebex_Server.chat_Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))
            Dim message, title, defaultValue As String
            Dim retValue As Object

            If session Is Nothing Then
                MsgBox("This session is disconnected.", MsgBoxStyle.Critical)
                Return
            End If

            message = fn_translate("msg_message_for") & lvi.SubItems(2).Text
            title = Application.ProductName
            defaultValue = ""
            retValue = InputBox(message, title, defaultValue)
            If retValue Is "" Then Return

            If session IsNot Nothing Then
                server.SendText(retValue.ToString(), 1, session.sessionID)

            End If

        End If
    End Sub



    Private Sub DisconnectSessionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles cms_disconnect_client.Click
        If lv_chat_Client_list.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = lv_chat_Client_list.SelectedItems.Item(0)
            ' Get the session using the sessionID pulled from the selected listview item
            Dim session As Swebex_Server.chat_Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))

            If session IsNot Nothing Then session.Close()
        End If
    End Sub

    Private Sub TestLargeArrayTransferHelperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestLargeArrayTransferHelperToolStripMenuItem.Click
        If lv_chat_Client_list.SelectedItems.Count = 0 Then
            MsgBox("You must select a connected client to send an array.", MsgBoxStyle.Critical)
            Return
        End If

        Dim lvi As ListViewItem = lv_chat_Client_list.SelectedItems.Item(0)
        ' Get the session using the sessionID pulled from the selected listview item
        Dim session As Swebex_Server.chat_Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))

        If session Is Nothing Then
            MsgBox("You can't send a large array to a disconnected session!", MsgBoxStyle.Critical)
            Return
        End If

        Dim msg = "This version if the library includes a helper function for people attempting to send arrays larger then the maximum packetsize. " & _
            "In those cases, the array will be broken up into multiple packets, and delivered one by one. This helper class can be used to send the large arrays and " & _
            "have LAT (the TcpComm.Utilities.LargeArrayTransferHelper tool) assemble them for you in the remote machine. " & vbCrLf & vbCrLf & _
            "This test will read about 500k of a large text file into a byte array, and send it to the client you selected (this would normally arrive in about 8 pieces). When it arrives, it will be " & _
            "displayed in the 'Lat Viewer', a form with a multiline textbox on it that you can use to verify that all the text has been delivered and assembled " & _
            "properly, and verify the message size."

        Dim retVal As MsgBoxResult = MsgBox(msg, MsgBoxStyle.Information Or MsgBoxStyle.OkCancel)
        If retVal = MsgBoxResult.Ok Then
            If session IsNot Nothing Then
                Dim fileBytes() As Byte = System.IO.File.ReadAllBytes("big.txt")
                Dim errMsg As String = ""
                If Not lat.SendArray(fileBytes, 100, session.sessionID, errMsg) Then MsgBox(errMsg, MsgBoxStyle.Critical, "TcpComm Demo App")
            End If
        End If
    End Sub


    Private Sub btn_chat_send_to_All_Click(sender As Object, e As EventArgs) Handles btn_chat_send_to_All.Click
        If Me.txt_chat_message.Text.Trim.Length > 0 Then
            ' Send a text message to all connected sessions on channel 1.
            server.SendText(Me.txt_chat_message.Text.Trim)
            Me.txt_chat_message.Text = ""
        End If
    End Sub


    Private Sub bt_Start_chat_Server_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btn_chat_clear_history_Click(sender As Object, e As EventArgs) Handles btn_chat_clear_history.Click
        Me.rtb_chat_history.Clear()
        Me.txt_chat_message.Text = ""
    End Sub


    Private Sub btn_chat_disconnect_all_Click(sender As Object, e As EventArgs) Handles btn_chat_disconnect_all.Click
        For Each item As ListViewItem In Me.lv_chat_Client_list.Items
            Dim session As Swebex_Server.chat_Server.SessionCommunications = server.GetSession(Convert.ToInt32(item.SubItems.Item(1).Text))
            If session IsNot Nothing Then session.Close()
        Next
    End Sub


    Private Sub btn_chat_save_Click(sender As Object, e As EventArgs) Handles btn_chat_save.Click
        fn_save_setting()
    End Sub


    Private Sub btn_open_log_file_Click(sender As Object, e As EventArgs) Handles btn_open_log_file.Click
        fn_open_log_file(chat_log_file)
    End Sub


    Public Sub sb_connect_to_another_clients(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles connect_to_another_clients.DoWork
        founded_clients = Nothing
        founded_clients = fn_checkAllIP(Convert.ToInt32(txt_chat_port.Text))
    End Sub


    Public Sub sb_connect_to_another_clients_close(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles connect_to_another_clients.RunWorkerCompleted
        last_check = Date.Now.AddSeconds(search_interval)
        connect_to_another_clients.Dispose()
        tss_search_clients.ForeColor = Color.Black
        tss_search_clients.Text = fn_translate("tss_search_clients_sleep")
        Dim count As Integer = 0
        Dim separate_client As String()
        Try
            If founded_clients.Split("|").Length > 0 Then ReDim clients_array(founded_clients.Split("|").Length - 1, 3)
            separate_client = founded_clients.Split("|")
        Catch ex As Exception
        End Try

        For k = 0 To count
            Try
                separate_client(k) = separate_client(k).Trim() 'kontrola jestli neni pouze prazdny
                If separate_client.Length > 0 Then
                    clients_array(count, 0) = separate_client(k)
                    clients_array(count, 1) = txt_chat_port.Text
                    clients_array(count, 2) = local_user_name
                    clients_array(count, 3) = fn_connect_host_to_chat_service(separate_client(k), txt_chat_port.Text, local_user_name) 'with hostname
                End If
            Catch ex As Exception
            End Try
            count += 1
        Next

        'save clients to file
        Try
            ReDim Preserve clients_array(count - 1, 3)
            'SAVE client_list
            ReDim file_array(clients_array.GetLength(0) - 1, (clients_array.Length / clients_array.GetLength(0)) - 1)
            Array.Copy(clients_array, file_array, clients_array.Length)
            fn_save_data_file_from_array(clients_file, ";", False)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub cms_chat_RightClickMenu_Opened(sender As Object, e As EventArgs) Handles cms_chat_RightClickMenu.Opened
        If lv_chat_Client_list.Items.Count = 0 Then
            cms_chat_RightClickMenu.TopLevel = False
        Else
            cms_chat_RightClickMenu.TopLevel = True
        End If
    End Sub



    Private Sub chat_Link_Clicked(sender As Object, e As LinkClickedEventArgs) Handles rtb_chat_history.LinkClicked
        Try
            System.Diagnostics.Process.Start(e.LinkText)
        Catch ex As Exception
        End Try
    End Sub


    Public Sub chat_open_history_file(sender As Object, e As MouseEventArgs) Handles rtb_chat_history.MouseDoubleClick
        Dim charIndex = rtb_chat_history.GetCharIndexFromPosition(e.Location)
        Try
            Dim start_char = rtb_chat_history.Find("_", 0, charIndex, RichTextBoxFinds.Reverse) + 1
            rtb_chat_history.Undo()
            Dim end_char = rtb_chat_history.Find("_", charIndex, RichTextBoxFinds.MatchCase)
            rtb_chat_history.Undo()

            If start_char > 0 Then
                rtb_chat_history.Select(start_char, end_char - start_char)
                Dim founded_file = rtb_chat_history.SelectedText
                rtb_chat_history.Undo()
                System.Diagnostics.Process.Start(IO.Path.Combine(FilesFolder, rtb_chat_history.SelectedText))
            End If
            rtb_chat_history.Select(charIndex, 0)
        Catch ex As Exception
            rtb_chat_history.Select(charIndex, 0)
        End Try
    End Sub





    ''CHAT END







    ''APP SETTINGS START

    Private Sub btn_app_settings_save_Click(sender As Object, e As EventArgs) Handles btn_app_settings_save.Click
        fn_save_setting()
    End Sub

    ''APP SETTINGS END










    ''START OF STATUS BAR

    Private Sub tss_search_clients_Click(sender As Object, e As EventArgs) Handles tss_search_clients.DoubleClick

        If connect_to_another_clients.IsBusy = False Then

            t_system_timer.Enabled = False

            'LOAD CLIENTS
            fn_load_data_file_to_array(clients_file, ";", False)
            ReDim clients_array(file_array.GetLength(0) - 1, (file_array.Length / file_array.GetLength(0)) - 1)
            Array.Copy(file_array, clients_array, file_array.Length)
            fn_connect_to_array_clients()

            tss_search_clients.Text = fn_translate("tss_search_clients_run")
            tss_search_clients.ForeColor = Color.ForestGreen
            connect_to_another_clients.RunWorkerAsync()
        End If
    End Sub


    Private Sub tss_chat_server_status_Click(sender As Object, e As EventArgs) Handles tss_chat_server_status.DoubleClick
        fn_start_chat_server()
    End Sub


    ''END OF STATUS BAR





    'timer as 1000 milisecond
    Private Sub t_system_timer_Tick(sender As Object, e As EventArgs) Handles t_system_timer.Tick
        Try
            If last_check < Date.Now And connect_to_another_clients.IsBusy = False Then
                tss_search_clients.Text = fn_translate("tss_search_clients_run")
                tss_search_clients.ForeColor = Color.ForestGreen
                connect_to_another_clients.RunWorkerAsync()
            ElseIf connect_to_another_clients.IsBusy = True Then
                If waiting_string.Length < 3 Then
                    waiting_string += "."
                Else : waiting_string = "."
                End If
                tss_search_clients.Text = fn_translate("tss_search_clients_run") + waiting_string
            End If
        Catch ex As Exception
        End Try
    End Sub








    Private Sub chat_btn_show_files_Click(sender As Object, e As EventArgs) Handles btn_show_files.Click
        fn_open_file(FilesFolder)
    End Sub

End Class



