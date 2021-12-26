Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel


Public Class main_form

    Public returned_string As String
    Public config_dir As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RS232_Receiver")
    Public config_file As String = System.IO.Path.Combine(config_dir, "config.ini")


    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data


    Private Sub Main_Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        myPort = IO.Ports.SerialPort.GetPortNames()
        cb_com_ports.Items.AddRange(myPort)
        btn_write_to_com.Enabled = False
        If fn_load_setting() = False Then
            Tb_Control.SelectedIndex = 1
        Else
            If Me.chb_init_after_start.Checked = True Then
                Try
                    btn_com_initialize_Click(Me, e)
                Catch ex As Exception
                    MessageBox.Show("Zadaný Com Port nebyl Nalezen." + vbNewLine + "Připojte zařízení a Spusťte Inicializaci.", "Chyba Inicializace Portu")
                End Try
            End If
        End If
    End Sub


    Private Sub Main_Form_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Main_Form_minimized(Me, e)
        CheckSelectedPort()
    End Sub


    Private Sub Main_Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim result = MsgBox("Chcete Aplikaci skutečně Ukončit?", MsgBoxStyle.YesNo)
        If result = vbYes Then
            SerialPort1.Close()
            Application.Exit()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Main_Form_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SerialPort1.Close()
        Application.Exit()
    End Sub



    Private Sub Main_Form_minimized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Visible = True Then
            Me.WindowState = FormWindowState.Normal
            Me.Visible = False
        End If
    End Sub


    Private Sub ComboBox1_Click(sender As System.Object, e As System.EventArgs) Handles cb_com_ports.Click
    End Sub


    Private Sub btn_com_initialize_Click(sender As Object, e As EventArgs) Handles btn_com_initialize.Click
        'If CheckSelectedPort() = False Then Exit Sub
        Try

            SerialPort1.PortName = cb_com_ports.Text
            SerialPort1.BaudRate = cb_baud_rate.Text
            'SerialPort1.Parity = Parity.None
            'SerialPort1.StopBits = StopBits.One
            'SerialPort1.DataBits = 8
            'SerialPort1.Handshake = Handshake.XOnXOff
            'SerialPort1.RtsEnable = True
            SerialPort1.Open()
            btn_com_initialize.Enabled = False
            btn_write_to_com.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Com Port se nepodařilo Inicializovat." + vbNewLine + "Zkontrolujte nastavení.")
        End Try
    End Sub


    Private Sub btn_write_to_com_Click(sender As System.Object, e As System.EventArgs) Handles btn_write_to_com.Click
        SerialPort1.Write(rtb_input.Text & vbCr) 'concatenate with \n
    End Sub

    Private Sub btn_uninit_Click(sender As Object, e As EventArgs) Handles btn_uninit.Click
        SerialPort1.Close()
        If CheckSelectedPort() = True Then Me.btn_com_initialize.Enabled = True
    End Sub


    Private Sub SerialPort1_DataReceived(sender As System.Object, e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        returned_string &= SerialPort1.ReadExisting()
        If (returned_string.Length = 12) Then
            ReceivedText(returned_string.ToString)
            returned_string = ""
        End If

    End Sub

    Private Sub ReceivedText(ByVal [text] As String) 'input from ReadExisting
        If Me.rtb_output.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            If Me.txt_keystring_length.Text <> "Full" Then
                If Me.chb_autoenter.Checked = True Then
                    My.Computer.Keyboard.SendKeys(Mid([text], 1, CInt(Me.txt_keystring_length.Text)) + vbTab)
                ElseIf Me.chb_autotab.Checked = True Then
                    'My.Computer.Keyboard.SendKeys(Mid([text], 1, CInt(Me.txt_keystring_length.Text)) + vbCrLf)
                    My.Computer.Keyboard.SendKeys(Mid([text], 1, CInt(Me.txt_keystring_length.Text)) + vbCr)
                Else
                    My.Computer.Keyboard.SendKeys(Mid([text], 1, CInt(Me.txt_keystring_length.Text)))
                End If
                If Me.Visible = True Then Me.rtb_output.Text = Mid([text], 1, CInt(Me.txt_keystring_length.Text)) 'append text
            Else
                If Me.chb_autoenter.Checked = True Then
                    My.Computer.Keyboard.SendKeys([text] + vbTab)
                ElseIf Me.chb_autotab.Checked = True Then
                    'My.Computer.Keyboard.SendKeys([text] + vbCrLf)
                    My.Computer.Keyboard.SendKeys([text] + vbCr)
                Else
                    My.Computer.Keyboard.SendKeys([text])
                End If
                If Me.Visible = True Then Me.rtb_output.Text = [text] 'append text
            End If
        End If
    End Sub

    Private Sub btn_save_settings_Click(sender As Object, e As EventArgs) Handles btn_save_settings.Click
        fn_save_file_setting()
    End Sub

    Private Sub ni_tray_menu_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ni_tray_menu.MouseDoubleClick
        If Me.Visible = False Then
            Me.Visible = True
        Else
            Me.Visible = False
        End If
    End Sub

    Private Sub btn_output_clear_Click(sender As Object, e As EventArgs) Handles btn_output_clear.Click
        Me.rtb_output.Clear()
    End Sub

    Private Sub btn_input_clear_Click(sender As Object, e As EventArgs) Handles btn_input_clear.Click
        Me.rtb_input.Clear()
    End Sub


    Private Sub Checker_Timer_Tick(sender As Object, e As EventArgs) Handles Checker_Timer.Tick
        Try
            If SerialPort1.IsOpen = False And chb_init_after_start.Checked = True Then
                btn_com_initialize_Click(Me, e)
            End If
        Catch ex As Exception
            If CheckSelectedPort() = True Then Me.btn_com_initialize.Enabled = True
        End Try
    End Sub

    Private Sub cb_com_ports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_com_ports.SelectedIndexChanged
        CheckSelectedPort()
    End Sub

End Class






