Option Explicit On

Imports System.Data.OleDb
Imports Microsoft.Office.Interop

Module Functions


    Public slq_query_message As String
    Public slq_query_command As String
    'Public slq_fields_count As Integer
    Public user_field_list(0, 0) As String 'FOR INPUT: type,ID,must,label,value,backcolor,forecolor FOR SUBBUTTON:ID,file,enable,sql_where_command,value
    Public user_field_count As Integer
    Public temp_string As String
    Public autosubmit_interval As Integer = 0
    Public last_autosubmit As Date = DateTime.Now
    Public autoroot_interval As Integer = 0
    Public last_autoroot As Date = DateTime.Now
    Public multiselect_posibility As Boolean = False
    Public noautosum As Boolean = False
    Public columnsum As String = ""


    'color declaration
    Public must_color As Color = Color.PapayaWhip
    Public selected_color As Color = Color.Orange
    Public user_backcolor As Color = Color.White
    Public user_forecolor As Color = Color.Black


    'draw positioning
    Private menu_position, button_position As Integer


    Function fn_create_directory(ByVal directory As String)

        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Function


    Function fn_delete_directory(ByVal directory As String)
        If System.IO.Directory.Exists(directory) Then System.IO.Directory.Delete(directory, True)
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
        System.IO.File.Delete(file)

        If fn_check_file(file) = False Then
            fn_delete_file = True
        Else
            fn_delete_file = False
        End If
    End Function

    Function fn_copy_file(ByVal file As String, ByVal file1 As String) As Boolean
        fn_copy_file = False
        System.IO.File.Copy(file, file1)
        If fn_check_file(file1) = True Then fn_copy_file = True
    End Function




    Function fn_load_sql_files() As Boolean
        If fn_check_directory(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.dir_sql_Directory)) = True Then
            My.Forms.Main_Form.lb_available_reports.Items.Clear()
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.dir_sql_Directory))
                My.Forms.Main_Form.lb_available_reports.Items.Add(System.IO.Path.GetFileNameWithoutExtension(foundFile))
            Next
            fn_load_sql_files = True
        Else
            fn_load_sql_files = False
            MessageBox.Show("Složka: " + My.Forms.Main_Form.dir_sql_Directory + " s SQL soubory nebyla nalezena")
        End If
    End Function



    Function fn_load_selected_dataview(ByVal filename As String) As Boolean
        multiselect_posibility = False
        noautosum = False
        columnsum = ""
        slq_query_message = ""
        Dim record

        ReDim My.Forms.Main_Form.available_reports(3, 0)
        My.Forms.Main_Form.available_reports(0, 0) = "Tiskové Sestavy"
        My.Forms.Main_Form.available_reports(1, 0) = ""
        My.Forms.Main_Form.available_reports(2, 0) = ""
        My.Forms.Main_Form.available_reports(3, 0) = ""
        My.Forms.Main_Form.cb_reports.Items.Clear()

        My.Forms.Main_Form.Cursor = Cursors.WaitCursor
        Try
            Dim parts, New_field, field_property, cb_items
            Dim count = 0, param_no = 1, forsub_count = 0, temp_count = 0, user_variables_count = 0
            menu_position = -(CInt(My.Forms.Main_Form.txt_font_size.Text) * 2)
            button_position = 5
            autosubmit_interval = 0
            autoroot_interval = 0
            'fn_detect_encoding(System.IO.Path.Combine(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.dir_sql_Directory), filename + ".sql"))


            If fn_check_file(System.IO.Path.Combine(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.dir_sql_Directory), filename + ".sql")) = True Then

                Dim objReader As New System.IO.StreamReader(System.IO.Path.Combine(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.dir_sql_Directory), filename + ".sql"), fn_detect_encoding(System.IO.Path.Combine(System.IO.Path.Combine(Main_Form.root_directory, My.Forms.Main_Form.dir_sql_Directory), filename + ".sql")))
                record = objReader.ReadLine()
                user_field_count = 0

                Do While Not (record Is Nothing)
                    record = record.Trim() 'kontrola jestli neni pouze prazdny
                    If record.Length > 0 Then
                        parts = Split(record, ":")
                        If parts(0) = "INPUT" Or parts(0) = "SUBBUTTON" Or parts(0) = "STIMBUTTON" Or parts(0) = "CRYSTALBUTTON" Or parts(0) = "PDFBUTTON" Then user_field_count += 1
                        If parts(0) = "FORSUBVIEW" Then
                            For Each part In parts
                                If temp_count > 0 Then
                                    If part.length > 0 Then forsub_count += 1
                                End If
                                temp_count += 1
                            Next
                        End If
                    End If
                    record = objReader.ReadLine()
                Loop

                ReDim user_field_list((user_field_count).ToString, 6)
                Dim parent_label As Label

                If My.Forms.Main_Form.btn_command.Length > 0 Then

                    ReDim My.Forms.Main_Form.temp_variables(My.Forms.Main_Form.user_variables.GetLength(0) - 1, 3)  'field type/name/value/field no from datagrid
                    Array.Copy(My.Forms.Main_Form.user_variables, My.Forms.Main_Form.temp_variables, My.Forms.Main_Form.user_variables.Length)
                    ReDim My.Forms.Main_Form.user_variables((My.Forms.Main_Form.temp_variables.GetLength(0) - 1 + forsub_count + user_field_count).ToString, 3)  'field type/name/value

                Else
                    ReDim My.Forms.Main_Form.temp_variables(0, 0)  'field type/name/value
                    ReDim My.Forms.Main_Form.user_variables((forsub_count + user_field_count).ToString, 3)  'field type/name/value
                End If


                objReader.BaseStream.Position = 0
                record = objReader.ReadLine()

                Dim fouded_correct_line = False
                Do While Not (record Is Nothing)
                    fouded_correct_line = False

                    record = record.Trim() 'kontrola jestli neni pouze prazdny
                    If record.Length > 0 Then
                        parts = Split(record, ":")

                        If count = 0 Then
                            My.Forms.Main_Form.tp_datalist.Text = parts(1)
                            fouded_correct_line = True
                        End If


                        'INPUT TYPES DEFINITIONS
                        If count > 0 And parts(0) = "INPUT" Then

                            user_field_list((count), 0) = parts(1)
                            user_field_list((count), 1) = parts(2)
                            user_field_list((count), 3) = parts(3)
                            user_field_list((count), 5) = "White"
                            user_field_list((count), 6) = "Black"

                            My.Forms.Main_Form.user_variables(user_variables_count, 0) = parts(1)
                            My.Forms.Main_Form.user_variables(user_variables_count, 1) = parts(2)

                            If parts(1) = "TEXTBOX" Then


                                New_field = New Label
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                New_field.visible = True
                                New_field.Name = "lbl" + parts(2)
                                New_field.text = parts(3)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.textalign = ContentAlignment.MiddleCenter
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                                parent_label = New_field

                                New_field = New TextBox
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3
                                New_field.Location = New Point(1, menu_position)
                                New_field.Name = parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.TabIndex = (count * 10) + 1000
                                New_field.CharacterCasing = CharacterCasing.Upper
                                'Start of Property Definition

                                Dim focus_reaction As TextBox = Nothing
                                focus_reaction = DirectCast(New_field, TextBox)
                                AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                                AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                                With New_field
                                    param_no = 1
                                    For Each part In parts
                                        If param_no > 4 Then  'hard set position on command
                                            field_property = part.split("/")
                                            Select Case field_property(0)
                                                Case "Enabled"
                                                    .Enabled = field_property(1)
                                                Case "Keyboard"
                                                    Dim tb As TextBox = Nothing
                                                    tb = DirectCast(New_field, TextBox)
                                                    AddHandler tb.LostFocus, AddressOf My.Forms.Main_Form.txt_field_LostFocus_for_keymap
                                                    AddHandler tb.GotFocus, AddressOf My.Forms.Main_Form.txt_field_Focus_for_keymap
                                                    .AccessibleDescription = field_property(1)
                                                Case "BackColor"
                                                    .Backcolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 5) = field_property(1)
                                                Case "ForeColor"
                                                    .Forecolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 6) = field_property(1)
                                                Case "MUST"
                                                    .BackColor = must_color
                                                    user_field_list((count), 2) = "True"
                                                Case "Text"
                                                    user_field_list((count), 4) = field_property(1)
                                                    .Text = field_property(1)
                                                Case "CharacterCasing-Normal"
                                                    .CharacterCasing = CharacterCasing.Normal
                                                Case "CharacterCasing-Upper"
                                                    .CharacterCasing = CharacterCasing.Upper
                                                Case "CharacterCasing-Lower"
                                                    .CharacterCasing = CharacterCasing.Lower
                                                Case "Visible"
                                                    .Visible = field_property(1)
                                                    If field_property(1) = "False" Then
                                                        menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3) - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10)
                                                        parent_label.Visible = False
                                                    End If
                                                Case "UseSystemPasswordChar"
                                                    .UseSystemPasswordChar = field_property(1)
                                                Case "ReadOnly"
                                                    .ReadOnly = field_property(1)
                                                Case "TabStop"
                                                    .TabStop = field_property(1)
                                                Case "Submit"
                                                    If field_property(1) = "True" Then
                                                        Dim btn As TextBox = Nothing
                                                        btn = DirectCast(New_field, TextBox)
                                                        AddHandler btn.LostFocus, AddressOf My.Forms.Main_Form.Submit_Form   ' From answer by Reed.
                                                    End If
                                                Case Else

                                            End Select
                                        End If
                                        param_no = param_no + 1
                                    Next
                                End With
                                'End of Property Definition
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                            End If


                            If parts(1) = "CHECKBOX" Then
                                New_field = New CheckBox
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(10, menu_position)
                                New_field.Name = parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.Text = parts(3)
                                New_field.TabIndex = (count * 10) + 1000
                                'Start of Property Definition

                                'Dim focus_reaction As CheckBox = Nothing
                                'focus_reaction = DirectCast(New_field, CheckBox)
                                'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                                'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                                With New_field
                                    For Each part In parts
                                        If InStr(part, "/") <> 0 Then
                                            field_property = part.split("/")
                                            Select Case field_property(0)
                                                Case "BackColor"
                                                    .Backcolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 5) = field_property(1)
                                                Case "ForeColor"
                                                    .Forecolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 6) = field_property(1)
                                                Case "Checked"
                                                    .Checked = field_property(1)
                                                    user_field_list((count), 4) = field_property(1)
                                                Case "Enabled"
                                                    .Enabled = field_property(1)
                                                Case "Visible"
                                                    .Visible = field_property(1)
                                                    If field_property(1) = "False" Then menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10)
                                                Case "TabStop"
                                                    .TabStop = field_property(1)
                                                Case "Submit"
                                                    If field_property(1) = "True" Then
                                                        Dim btn As CheckBox = Nothing
                                                        btn = DirectCast(New_field, CheckBox)
                                                        AddHandler btn.Click, AddressOf My.Forms.Main_Form.Submit_Form   ' From answer by Reed.
                                                    End If
                                                Case Else
                                            End Select
                                        End If
                                    Next
                                End With
                                'End of Property Definition
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                            End If


                            If parts(1) = "BUTTON" Then
                                New_field = New Button
                                'Start of Property Definition
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.8)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 20
                                New_field.Location = New Point(1, menu_position)
                                New_field.Name = parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.Text = parts(3)
                                New_field.BackColor = Color.White
                                New_field.TabIndex = (count * 10) + 1000

                                'Dim focus_reaction As Button = Nothing
                                'focus_reaction = DirectCast(New_field, Button)
                                'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                                'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                                With New_field
                                    For Each part In parts
                                        If InStr(part, "/") <> 0 Then
                                            field_property = part.split("/")
                                            Select Case field_property(0)
                                                Case "BackColor"
                                                    .Backcolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 5) = field_property(1)
                                                Case "ForeColor"
                                                    .Forecolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 6) = field_property(1)
                                                Case "Enabled"
                                                    .Enabled = field_property(1)
                                                Case "Visible"
                                                    .Visible = field_property(1)
                                                    If field_property(1) = "False" Then menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 20)
                                                Case "TabStop"
                                                    .TabStop = field_property(1)
                                                Case "Submit"
                                                    If field_property(1) = "True" Then
                                                        Dim btn As Button = Nothing
                                                        btn = DirectCast(New_field, Button)
                                                        AddHandler btn.Click, AddressOf My.Forms.Main_Form.Submit_Form   ' From answer by Reed.
                                                    End If
                                                Case Else
                                            End Select
                                        End If
                                    Next


                                End With
                                'End of Property Definition
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)

                            End If


                            If parts(1) = "DATE" Then
                                New_field = New Label
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                New_field.visible = True
                                New_field.Name = "lbl" + parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.text = parts(3)
                                New_field.textalign = ContentAlignment.MiddleCenter
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                                parent_label = New_field

                                New_field = New DateTimePicker
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                New_field.Name = parts(2)
                                New_field.Width = 101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text))
                                New_field.Height = CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.format = DateTimePickerFormat.Custom
                                'New_field.CustomFormat = "dd.MM.yyyy"
                                New_field.TabIndex = (count * 10) + 1000
                                'Start of Property Definition

                                'Dim focus_reaction As DateTimePicker = Nothing
                                'focus_reaction = DirectCast(New_field, DateTimePicker)
                                'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                                'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                                With New_field
                                    For Each part In parts
                                        If InStr(part, "/") <> 0 Then
                                            field_property = part.split("/")
                                            Select Case field_property(0)
                                                Case "BackColor"
                                                    .Backcolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 5) = field_property(1)
                                                Case "ForeColor"
                                                    .Forecolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 6) = field_property(1)
                                                Case "Enabled"
                                                    .Enabled = field_property(1)
                                                Case "Visible"
                                                    .Visible = field_property(1)
                                                    If field_property(1) = "False" Then
                                                        menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10) - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10)
                                                        parent_label.Visible = False
                                                    End If
                                                Case "TabStop"
                                                    .TabStop = field_property(1)
                                                Case "CustomFormat"
                                                    .CustomFormat = "" + field_property(1) + ""
                                                Case "Value"
                                                    If field_property(1) = "Now" Then
                                                        user_field_list((count), 4) = DateTime.Now
                                                        .value = DateTime.Now
                                                    Else
                                                        user_field_list((count), 4) = field_property(1)
                                                        .value = field_property(1)
                                                    End If
                                                Case "Submit"
                                                    If field_property(1) = "True" Then
                                                        Dim btn As DateTimePicker = Nothing
                                                        btn = DirectCast(New_field, DateTimePicker)
                                                        AddHandler btn.LostFocus, AddressOf My.Forms.Main_Form.Submit_Form   ' From answer by Reed.
                                                    End If
                                                Case Else
                                            End Select
                                        End If
                                    Next
                                End With
                                'End of Property Definition
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                            End If


                            If parts(1) = "DATETIME" Then
                                New_field = New Label
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                New_field.visible = True
                                New_field.Name = "lbl" + parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.text = parts(3)
                                New_field.textalign = ContentAlignment.MiddleCenter
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                                parent_label = New_field

                                New_field = New DateTimePicker
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                New_field.Name = parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.format = DateTimePickerFormat.Custom
                                'New_field.CustomFormat = "dd.MM.yyyy HH:mm:ss"
                                New_field.TabIndex = (count * 10) + 1000
                                'Start of Property Definition

                                'Dim focus_reaction As DateTimePicker = Nothing
                                'focus_reaction = DirectCast(New_field, DateTimePicker)
                                'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                                'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                                With New_field
                                    For Each part In parts
                                        If InStr(part, "/") <> 0 Then
                                            field_property = part.split("/")
                                            Select Case field_property(0)
                                                Case "BackColor"
                                                    .Backcolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 5) = field_property(1)
                                                Case "ForeColor"
                                                    .Forecolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 6) = field_property(1)
                                                Case "Enabled"
                                                    .Enabled = field_property(1)
                                                Case "Visible"
                                                    .Visible = field_property(1)
                                                    If field_property(1) = "False" Then
                                                        menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10) - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10)
                                                        parent_label.Visible = False
                                                    End If
                                                Case "TabStop"
                                                    .TabStop = field_property(1)
                                                Case "CustomFormat"
                                                    .CustomFormat = "" + field_property(1) + ""
                                                Case "Value"
                                                    If field_property(1) = "Now" Then
                                                        user_field_list((count), 4) = DateTime.Now
                                                        .value = DateTime.Now
                                                    Else
                                                        user_field_list((count), 4) = field_property(1)
                                                        .value = field_property(1)
                                                    End If
                                                Case "Submit"
                                                    If field_property(1) = "True" Then
                                                        Dim btn As DateTimePicker = Nothing
                                                        btn = DirectCast(New_field, DateTimePicker)
                                                        AddHandler btn.LostFocus, AddressOf My.Forms.Main_Form.Submit_Form   ' From answer by Reed.
                                                    End If
                                                Case Else
                                            End Select
                                        End If
                                    Next
                                End With
                                'End of Property Definition
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                            End If




                            If parts(1) = "COMBOBOX" Then
                                New_field = New Label
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                New_field.visible = True
                                New_field.Name = "lbl" + parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.text = parts(3)
                                New_field.textalign = ContentAlignment.MiddleCenter
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                                parent_label = New_field

                                New_field = New ComboBox
                                New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                                menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                New_field.Location = New Point(1, menu_position)
                                'New_field.Items.Add("new")
                                New_field.Name = parts(2)
                                New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                                New_field.TabIndex = (count * 10) + 1000

                                Dim focus_reaction As ComboBox = Nothing
                                focus_reaction = DirectCast(New_field, ComboBox)
                                AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                                AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                                'Start of Property Definition
                                With New_field
                                    param_no = 1
                                    For Each part In parts
                                        'If InStr(part, "/") <> 0 Then
                                        If param_no > 4 Then  'hard set position on command
                                            field_property = part.split("/")
                                            Select Case field_property(0)
                                                Case "BackColor"
                                                    .Backcolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 5) = field_property(1)
                                                Case "ForeColor"
                                                    .Forecolor = Color.FromName(field_property(1))
                                                    user_field_list((count), 6) = field_property(1)
                                                Case "Enabled"
                                                    .Enabled = field_property(1)
                                                Case "Visible"
                                                    .Visible = field_property(1)
                                                    If field_property(1) = "False" Then
                                                        menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10) - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10)
                                                        parent_label.Visible = False
                                                    End If
                                                Case "DropDownStyle"
                                                    .DropDownStyle = field_property(1)
                                                Case "TabStop"
                                                    .TabStop = field_property(1)
                                                Case "MUST"
                                                    .BackColor = must_color
                                                    user_field_list((count), 2) = "True"
                                                Case "ITEMS"
                                                    cb_items = field_property(1).split(",")
                                                    For Each cb_item In cb_items
                                                        New_field.Items.Add(Replace(cb_item.ToString, "!", ""))
                                                        If cb_item.ToString.Length > 0 And InStr(cb_item.ToString, "!") > 0 Then
                                                            user_field_list((count), 4) = Replace(cb_item.ToString, "!", "")
                                                            New_field.SelectedItem = Replace(cb_item.ToString, "!", "")
                                                        End If
                                                    Next
                                                Case "SQLITEMS"
                                                    If fn_sql_request(field_property(1), "SELECTONEITEM") = True Then
                                                        For k = 0 To My.Forms.Main_Form.sql_array_count - 1 Step 1
                                                            New_field.Items.Add(Replace(My.Forms.Main_Form.sql_array(k, 0), "!", ""))
                                                            If InStr(My.Forms.Main_Form.sql_array(k, 0), "!") > 0 Then
                                                                user_field_list((count), 4) = Replace(My.Forms.Main_Form.sql_array(k, 0), "!", "")
                                                                New_field.SelectedItem = Replace(My.Forms.Main_Form.sql_array(k, 0), "!", "")
                                                            End If
                                                        Next
                                                    End If
                                                Case "Submit"
                                                    If field_property(1) = "True" Then
                                                        Dim btn As ComboBox = Nothing
                                                        btn = DirectCast(New_field, ComboBox)
                                                        AddHandler btn.LostFocus, AddressOf My.Forms.Main_Form.Submit_Form
                                                    End If
                                                Case Else
                                            End Select
                                        End If
                                        param_no = param_no + 1
                                    Next
                                End With
                                'End of Property Definition
                                My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                            End If

                            fouded_correct_line = True
                            user_variables_count += 1
                        End If
                        'END OF INPUT TYPE DEFINITIONS


                        'LOAD SQL COMMAND
                        If count > 0 And parts(0) = "SQL" Then
                            slq_query_command = parts(1)
                            If My.Forms.Main_Form.temp_variables.GetLength(0) > 1 Then
                                For j As Integer = 0 To (My.Forms.Main_Form.temp_variables.GetLength(0) - 1) Step 1
                                    If InStr(slq_query_command, "*" + My.Forms.Main_Form.temp_variables(j, 1) + "*") <> 0 Then
                                        slq_query_command = slq_query_command.Replace("*" + My.Forms.Main_Form.temp_variables(j, 1) + "*", My.Forms.Main_Form.temp_variables(j, 3))
                                    End If
                                Next

                            End If
                        End If


                        If count > 0 And parts(0) = "SUBBUTTON" Then
                            'Start of Property Definition
                            user_field_list((count), 0) = parts(2)
                            user_field_list((count), 1) = parts(1)
                            user_field_list((count), 3) = parts(3)

                            New_field = New Button
                            New_field.Size = New Drawing.Size((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 2)
                            New_field.Location = New Point(button_position, 2)
                            button_position = button_position + (8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5
                            New_field.Name = parts(2)
                            New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                            New_field.Text = parts(3)
                            New_field.BackColor = Color.White
                            New_field.TabIndex = (count * 10) + 1000
                            If My.Forms.Main_Form.dgw_query_view.Rows.Count = 0 Then New_field.Enabled = False

                            'Dim focus_reaction As Button = Nothing
                            'focus_reaction = DirectCast(New_field, Button)
                            'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                            'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                            With New_field
                                For Each part In parts
                                    If InStr(part, "/") <> 0 Then
                                        field_property = part.split("/")
                                        Select Case field_property(0)
                                            Case "BackColor"
                                                .Backcolor = Color.FromName(field_property(1))
                                                user_field_list((count), 5) = field_property(1)
                                            Case "ForeColor"
                                                .Forecolor = Color.FromName(field_property(1))
                                                user_field_list((count), 6) = field_property(1)
                                            Case "Enabled"
                                                If field_property(1) = "True" Then user_field_list((count), 2) = "True"
                                                If field_property(1) = "False" Then
                                                    user_field_list((count), 2) = "False"
                                                    .Enabled = False
                                                End If

                                            Case "Visible"
                                                .Visible = field_property(1)
                                                If field_property(1) = "False" Then button_position = button_position - ((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5)
                                            Case "TabStop"
                                                .TabStop = field_property(1)
                                            Case "Submit"
                                                If field_property(1) = "Next" Then
                                                    Dim btn As Button = Nothing
                                                    btn = DirectCast(New_field, Button)
                                                    AddHandler btn.Click, AddressOf My.Forms.Main_Form.Open_SubForm   ' From answer by Reed.
                                                End If
                                            Case Else
                                        End Select
                                    End If
                                Next


                            End With
                            'End of Property Definition
                            My.Forms.Main_Form.p_sub_buttons.Controls.Add(New_field)
                            fouded_correct_line = True
                        End If




                        If count > 0 And parts(0) = "STIMBUTTON" Then
                            'Start of Property Definition
                            user_field_list((count), 0) = parts(2)
                            user_field_list((count), 1) = parts(1)
                            user_field_list((count), 3) = parts(3)

                            New_field = New Button
                            New_field.Size = New Drawing.Size((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 2)
                            New_field.Location = New Point(button_position, 2)
                            button_position = button_position + (8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5
                            New_field.Name = parts(2)
                            New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                            New_field.Text = parts(3)
                            New_field.BackColor = Color.White
                            New_field.TabIndex = (count * 10) + 1000
                            If My.Forms.Main_Form.dgw_query_view.Rows.Count = 0 Then New_field.Enabled = False

                            'Dim focus_reaction As Button = Nothing
                            'focus_reaction = DirectCast(New_field, Button)
                            'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                            'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                            With New_field
                                For Each part In parts
                                    If InStr(part, "/") <> 0 Then
                                        field_property = part.split("/")
                                        Select Case field_property(0)
                                            Case "BackColor"
                                                .Backcolor = Color.FromName(field_property(1))
                                                user_field_list((count), 5) = field_property(1)
                                            Case "ForeColor"
                                                .Forecolor = Color.FromName(field_property(1))
                                                user_field_list((count), 6) = field_property(1)
                                            Case "Enabled"
                                                If field_property(1) = "True" Then user_field_list((count), 2) = "True"
                                                If field_property(1) = "False" Then
                                                    user_field_list((count), 2) = "False"
                                                    .Enabled = False
                                                End If

                                            Case "Visible"
                                                .Visible = field_property(1)
                                                If field_property(1) = "False" Then button_position = button_position - ((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5)
                                            Case "TabStop"
                                                .TabStop = field_property(1)
                                            Case "STIM"
                                                If IsNumeric(field_property(1)) = True Then
                                                    .Tag = field_property(1)
                                                    Dim btn As Button = Nothing
                                                    btn = DirectCast(New_field, Button)
                                                    AddHandler btn.Click, AddressOf My.Forms.Main_Form.run_STIM_Report   ' From answer by Reed.
                                                End If
                                            Case Else
                                        End Select
                                    End If
                                Next


                            End With
                            'End of Property Definition
                            My.Forms.Main_Form.p_sub_buttons.Controls.Add(New_field)
                            fouded_correct_line = True
                        End If

                        If count > 0 And parts(0) = "PDFBUTTON" Then
                            'Start of Property Definition
                            user_field_list((count), 0) = parts(2)
                            user_field_list((count), 1) = parts(1)
                            user_field_list((count), 3) = parts(3)

                            New_field = New Button
                            New_field.Size = New Drawing.Size((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 2)
                            New_field.Location = New Point(button_position, 2)
                            button_position = button_position + (8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5
                            New_field.Name = parts(2)
                            New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                            New_field.Text = parts(3)
                            New_field.BackColor = Color.White
                            New_field.TabIndex = (count * 10) + 1000
                            If My.Forms.Main_Form.dgw_query_view.Rows.Count = 0 Then New_field.Enabled = False

                            'Dim focus_reaction As Button = Nothing
                            'focus_reaction = DirectCast(New_field, Button)
                            'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                            'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                            With New_field
                                For Each part In parts
                                    If InStr(part, "/") <> 0 Then
                                        field_property = part.split("/")
                                        Select Case field_property(0)
                                            Case "BackColor"
                                                .Backcolor = Color.FromName(field_property(1))
                                                user_field_list((count), 5) = field_property(1)
                                            Case "ForeColor"
                                                .Forecolor = Color.FromName(field_property(1))
                                                user_field_list((count), 6) = field_property(1)
                                            Case "Enabled"
                                                If field_property(1) = "True" Then user_field_list((count), 2) = "True"
                                                If field_property(1) = "False" Then
                                                    user_field_list((count), 2) = "False"
                                                    .Enabled = False
                                                End If

                                            Case "Visible"
                                                .Visible = field_property(1)
                                                If field_property(1) = "False" Then button_position = button_position - ((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5)
                                            Case "TabStop"
                                                .TabStop = field_property(1)
                                            Case "MESS"
                                                .AutoSize = field_property(1)
                                            Case "PATH"
                                                .Tag = field_property(1)
                                                Dim btn As Button = Nothing
                                                btn = DirectCast(New_field, Button)
                                                AddHandler btn.Click, AddressOf My.Forms.Main_Form.run_PDF_file   ' From answer by Reed.
                                            Case Else
                                        End Select
                                    End If
                                Next


                            End With
                            'End of Property Definition
                            My.Forms.Main_Form.p_sub_buttons.Controls.Add(New_field)
                            fouded_correct_line = True
                        End If

                        If count > 0 And parts(0) = "CRYSTALBUTTON" Then
                            'Start of Property Definition
                            user_field_list((count), 0) = parts(2)
                            user_field_list((count), 1) = parts(1)
                            user_field_list((count), 3) = parts(3)

                            New_field = New Button
                            New_field.Size = New Drawing.Size((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 2)
                            New_field.Location = New Point(button_position, 2)
                            button_position = button_position + (8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5
                            New_field.Name = parts(2)
                            New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                            New_field.Text = parts(3)
                            New_field.BackColor = Color.White
                            New_field.TabIndex = (count * 10) + 1000
                            If My.Forms.Main_Form.dgw_query_view.Rows.Count = 0 Then New_field.Enabled = False

                            'Dim focus_reaction As Button = Nothing
                            'focus_reaction = DirectCast(New_field, Button)
                            'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                            'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                            With New_field
                                For Each part In parts
                                    If InStr(part, "/") <> 0 Then
                                        field_property = part.split("/")
                                        Select Case field_property(0)
                                            Case "BackColor"
                                                .Backcolor = Color.FromName(field_property(1))
                                                user_field_list((count), 5) = field_property(1)
                                            Case "ForeColor"
                                                .Forecolor = Color.FromName(field_property(1))
                                                user_field_list((count), 6) = field_property(1)
                                            Case "Enabled"
                                                If field_property(1) = "True" Then user_field_list((count), 2) = "True"
                                                If field_property(1) = "False" Then
                                                    user_field_list((count), 2) = "False"
                                                    .Enabled = False
                                                End If

                                            Case "Visible"
                                                .Visible = field_property(1)
                                                If field_property(1) = "False" Then button_position = button_position - ((8 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 5)
                                            Case "TabStop"
                                                .TabStop = field_property(1)
                                            Case "CRYSTAL"
                                                If IsNumeric(field_property(1)) = True Then
                                                    .Tag = field_property(1)
                                                    Dim btn As Button = Nothing
                                                    btn = DirectCast(New_field, Button)
                                                    AddHandler btn.Click, AddressOf My.Forms.Main_Form.run_CRYSTAL_Report   ' From answer by Reed.
                                                End If
                                            Case Else
                                        End Select
                                    End If
                                Next


                            End With
                            'End of Property Definition
                            My.Forms.Main_Form.p_sub_buttons.Controls.Add(New_field)
                            fouded_correct_line = True
                        End If

                        If count > 0 And parts(0) = "FORSUBVIEW" Then
                            temp_count = 0
                            For Each part In parts
                                If Replace(part.length, " ", "") > 0 Then
                                    field_property = part.split("/")
                                    If temp_count > 0 Then
                                        My.Forms.Main_Form.user_variables(user_variables_count, 0) = "SUBVIEW"
                                        My.Forms.Main_Form.user_variables(user_variables_count, 1) = field_property(0)
                                        My.Forms.Main_Form.user_variables(user_variables_count, 2) = field_property(1)
                                        My.Forms.Main_Form.user_variables(user_variables_count, 3) = 0

                                        user_variables_count += 1
                                    End If
                                    temp_count += 1
                                End If
                            Next
                            fouded_correct_line = False
                        End If



                        If count > 0 And parts(0) = "MESSAGE" Then
                            field_property = parts(1).split("|")
                            temp_string = ""
                            For Each part In field_property
                                temp_string &= part + vbNewLine
                            Next
                            MessageBox.Show(temp_string)
                        End If


                        If count > 0 And parts(0) = "SQLMESSAGE" Then
                            field_property = parts(1).split("|")
                            slq_query_message = ""
                            For Each part In field_property
                                slq_query_message &= part + vbNewLine
                            Next
                        End If

                        If count > 0 And parts(0) = "AUTOSUBMIT" Then
                            autosubmit_interval = parts(1)
                            last_autosubmit = DateTime.Now
                        End If


                        If count > 0 And parts(0) = "AUTOROOT" Then
                            autoroot_interval = parts(1)
                            last_autoroot = Date.Now.AddSeconds(autoroot_interval)
                        End If


                        If count > 0 And parts(0) = "MULTISELECT" Then
                            multiselect_posibility = parts(1)
                        End If

                        If count > 0 And parts(0) = "REPORT" Then
                            ReDim Preserve My.Forms.Main_Form.available_reports(My.Forms.Main_Form.available_reports.GetLength(0) - 1, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)))
                            My.Forms.Main_Form.available_reports(0, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = parts(1)
                            My.Forms.Main_Form.available_reports(1, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = parts(2)
                            My.Forms.Main_Form.available_reports(2, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = "CRYSTAL"
                            My.Forms.Main_Form.available_reports(3, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = parts(3)
                        End If

                        If count > 0 And parts(0) = "STIM_REPORT" Then
                            ReDim Preserve My.Forms.Main_Form.available_reports(My.Forms.Main_Form.available_reports.GetLength(0) - 1, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)))
                            My.Forms.Main_Form.available_reports(0, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = parts(1)
                            My.Forms.Main_Form.available_reports(1, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = parts(2)
                            My.Forms.Main_Form.available_reports(2, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = "STIM_REPORT"
                            My.Forms.Main_Form.available_reports(3, (My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1) = parts(3)
                        End If

                        If parts(0) = "LABEL" Then
                            New_field = New Label
                            New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.3)
                            menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                            New_field.Location = New Point(1, menu_position)
                            New_field.visible = True
                            New_field.Name = "lbl" + parts(2)
                            New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                            New_field.textalign = ContentAlignment.MiddleCenter
                            With New_field
                                For Each part In parts
                                    field_property = part.split("/")
                                    Select Case field_property(0)
                                        Case "Enabled"
                                            .Enabled = field_property(1)
                                        Case "Keyboard"
                                            Dim tb As TextBox = Nothing
                                            tb = DirectCast(New_field, TextBox)
                                            AddHandler tb.LostFocus, AddressOf My.Forms.Main_Form.txt_field_LostFocus_for_keymap
                                            AddHandler tb.GotFocus, AddressOf My.Forms.Main_Form.txt_field_Focus_for_keymap
                                            .AccessibleDescription = field_property(1)
                                        Case "BackColor"
                                            .Backcolor = Color.FromName(field_property(1))
                                            user_field_list((count), 5) = field_property(1)
                                        Case "ForeColor"
                                            .Forecolor = Color.FromName(field_property(1))
                                            user_field_list((count), 6) = field_property(1)
                                        Case "Text"
                                            user_field_list((count), 4) = field_property(1)
                                            .Text = field_property(1)
                                        Case "Visible"
                                            .Visible = field_property(1)
                                            If field_property(1) = "False" Then menu_position = menu_position - CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 10
                                        Case Else
                                    End Select
                                Next
                            End With
                            'End of Property Definition
                            My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)
                        End If

                        If parts(0) = "XLS_EXPORT" Then
                            New_field = New Button
                            'Start of Property Definition
                            New_field.Size = New Drawing.Size(101 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.8)
                            menu_position = menu_position + CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 20
                            New_field.Location = New Point(1, menu_position)
                            New_field.Name = parts(2)
                            New_field.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.8)
                            New_field.Text = parts(3)
                            New_field.BackColor = Color.White
                            New_field.TabIndex = (count * 10) + 1000

                            'Dim focus_reaction As Button = Nothing
                            'focus_reaction = DirectCast(New_field, Button)
                            'AddHandler focus_reaction.LostFocus, AddressOf fn_meLostFocus
                            'AddHandler focus_reaction.GotFocus, AddressOf fn_meGotFocus

                            With New_field
                                For Each part In parts
                                    If InStr(part, "/") <> 0 Then
                                        field_property = part.split("/")
                                        Select Case field_property(0)
                                            Case "BackColor"
                                                .Backcolor = Color.FromName(field_property(1))
                                                user_field_list((count), 5) = field_property(1)
                                            Case "ForeColor"
                                                .Forecolor = Color.FromName(field_property(1))
                                                user_field_list((count), 6) = field_property(1)
                                            Case "Enabled"
                                                .Enabled = field_property(1)
                                            Case "Visible"
                                                .Visible = field_property(1)
                                                If field_property(1) = "False" Then menu_position = menu_position - (CInt(My.Forms.Main_Form.txt_font_size.Text) * 1.5 + 20)
                                            Case "TabStop"
                                                .TabStop = field_property(1)
                                            Case "Export"
                                                If field_property(1) = "True" Then
                                                    Dim btn As Button = Nothing
                                                    btn = DirectCast(New_field, Button)
                                                    AddHandler btn.Click, AddressOf My.Forms.Main_Form.Xls_Export
                                                End If
                                            Case Else
                                        End Select
                                    End If
                                Next


                            End With
                            'End of Property Definition
                            My.Forms.Main_Form.p_user_inputs.Controls.Add(New_field)

                        End If


                        If count > 0 And parts(0) = "NOAUTOSUM" Then
                            noautosum = parts(1)
                        End If

                        If count > 0 And parts(0) = "COLUMNSUM" Then
                            columnsum = parts(1)
                        End If


                        If fouded_correct_line = True Then count += 1
                    End If
                    record = objReader.ReadLine()

                Loop
                objReader.Close()
            Else
                MessageBox.Show(" V adresáři aplikace + \ " + My.Forms.Main_Form.dir_sql_Directory + vbNewLine + "Soubor: " + filename + ".slq neexistuje.")
            End If


            ' FILL user_variables
            If My.Forms.Main_Form.temp_variables.GetLength(0) - 1 > 1 Then
                For i = 0 To My.Forms.Main_Form.temp_variables.GetLength(0) - 1 Step 1
                    My.Forms.Main_Form.user_variables(i + forsub_count + user_field_count, 0) = My.Forms.Main_Form.temp_variables(i, 0)
                    My.Forms.Main_Form.user_variables(i + forsub_count + user_field_count, 1) = My.Forms.Main_Form.temp_variables(i, 1)
                    My.Forms.Main_Form.user_variables(i + forsub_count + user_field_count, 2) = My.Forms.Main_Form.temp_variables(i, 2)
                    My.Forms.Main_Form.user_variables(i + forsub_count + user_field_count, 3) = My.Forms.Main_Form.temp_variables(i, 3)

                Next
            End If
            ' FILL user_variables



            For tmp = 0 To ((My.Forms.Main_Form.available_reports.Length / My.Forms.Main_Form.available_reports.GetLength(0)) - 1)
                My.Forms.Main_Form.cb_reports.Items.Add(My.Forms.Main_Form.available_reports(0, tmp).ToString)
            Next
            My.Forms.Main_Form.selected_report = ""
            My.Forms.Main_Form.cb_reports.SelectedIndex = 0

            fn_load_selected_dataview = True
            My.Forms.Main_Form.Cursor = Cursors.Default

            Dim ctl As Control
            ctl = CType(My.Forms.Main_Form.p_user_inputs, Control)
            ctl.SelectNextControl(My.Forms.Main_Form.ActiveControl, True, True, True, True)

        Catch ex As Exception
            fn_load_selected_dataview = False
            My.Forms.Main_Form.Cursor = Cursors.Default
            MessageBox.Show("Soubor nelze otevřít nebo má chybnou syntaxi" + vbNewLine + "Zkontrolujte soubor: " + filename + ".slq" + vbNewLine + "v adresáři aplikace + \" + My.Forms.Main_Form.dir_sql_Directory + vbNewLine + "Chyba v příkazu: " + record)
        End Try

    End Function



    Function fn_load_setting_file() As Boolean
        fn_load_setting_file = False
        If fn_check_file(Main_Form.setting_file) = True Then
            Try
                Dim objReader As New System.IO.StreamReader(System.IO.Path.GetFullPath(Main_Form.setting_file), True)
                Dim record = objReader.ReadLine()
                objReader.Close()
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    'My.Forms.Inventura_majetku.txt_databases.Text = record

                    Dim temp As String() = Split(record, "#")

                    'My.MySettings.Default.Item("sql_connection") = "Data Source=" + temp(1).ToString + ";Initial Catalog=" + temp(2).ToString + ";Persist Security Info=True;User ID=" + temp(3).ToString + ";Password=" + temp(4).ToString + ""
                    My.MySettings.Default.Item("sql_connection") = "Data Source=" + temp(0).ToString + ";Persist Security Info=True;User ID=" + temp(1).ToString + ";Password=" + temp(2).ToString

                    My.Forms.Main_Form.txt_mssql_server.Text = temp(0).ToString
                    My.Forms.Main_Form.txt_mssql_name.Text = temp(1).ToString
                    My.Forms.Main_Form.txt_mssql_password.Text = temp(2).ToString
                    My.Forms.Main_Form.cb_default_keyboard.SelectedItem = temp(3).ToString
                    If temp(4).ToString = "True" Then My.Forms.Main_Form.chb_must_field_message.Checked = True
                    If temp(5).ToString = "True" Then My.Forms.Main_Form.chb_clean_form_god_sql.Checked = True
                    If temp(6).ToString = "True" Then My.Forms.Main_Form.chb_select_first_row.Checked = True
                    If temp(7).ToString = "True" Then My.Forms.Main_Form.chb_clean_form_bad_sql.Checked = True
                    If temp(8).ToString = "True" Then My.Forms.Main_Form.chb_extensible_rows.Checked = True
                    If temp(9).ToString = "True" Then My.Forms.Main_Form.chb_dgview_multiselect.Checked = True

                    If temp(10).Length = 0 Then
                        My.Forms.Main_Form.txt_font_size.Text = 10
                    Else
                        My.Forms.Main_Form.txt_font_size.Text = temp(10).ToString
                    End If

                    If temp(11).Length <> 0 Then
                        If fn_check_directory(System.IO.Path.Combine(temp(11).ToString, Main_Form.dir_sql_Directory)) = False Then
                            MessageBox.Show("Složka s vlatními definicemi nebyla nalezena" + vbNewLine + "Nastavení složky bylo resetováno.")
                        Else
                            My.Forms.Main_Form.txt_own_def_location.Text = temp(11).ToString
                            Main_Form.chb_own_def_loc.Checked = True
                            Main_Form.btn_own_def_folder.Enabled = True
                            Main_Form.root_directory = temp(11).ToString
                        End If
                    End If

                    fn_load_setting_file = True
                End If
                If fn_load_setting_file = False Then MessageBox.Show("Zadejte Nastavení DB v " + Main_Form.setting_file)
            Catch ex As Exception
                MessageBox.Show("Konfigurační soubor nebyl nalezen")
            End Try
        End If
    End Function



    Function fn_save_file_setting() As Boolean
        fn_delete_file(Main_Form.setting_file)
        fn_create_file(Main_Form.setting_file)
        Dim objWriter As New System.IO.StreamWriter(Main_Form.setting_file, True)

        Dim checkbox_status As String = "False"
        If My.Forms.Main_Form.chb_must_field_message.Checked = True Then checkbox_status = "True"
        If My.Forms.Main_Form.chb_clean_form_god_sql.Checked = True Then
            checkbox_status &= "#" + "True"
        Else
            checkbox_status &= "#" + "False"
        End If
        If My.Forms.Main_Form.chb_select_first_row.Checked = True Then
            checkbox_status &= "#" + "True"
        Else
            checkbox_status &= "#" + "False"
        End If
        If My.Forms.Main_Form.chb_clean_form_bad_sql.Checked = True Then
            checkbox_status &= "#" + "True"
        Else
            checkbox_status &= "#" + "False"
        End If
        If My.Forms.Main_Form.chb_extensible_rows.Checked = True Then
            checkbox_status &= "#" + "True"
        Else
            checkbox_status &= "#" + "False"
        End If
        If My.Forms.Main_Form.chb_dgview_multiselect.Checked = True Then
            checkbox_status &= "#" + "True"
        Else
            checkbox_status &= "#" + "False"
        End If


        objWriter.WriteLine(My.Forms.Main_Form.txt_mssql_server.Text + "#" + My.Forms.Main_Form.txt_mssql_name.Text + "#" + My.Forms.Main_Form.txt_mssql_password.Text + "#" + My.Forms.Main_Form.cb_default_keyboard.SelectedItem + "#" + checkbox_status.ToString + "#" + My.Forms.Main_Form.txt_font_size.Text + "#" + Main_Form.txt_own_def_location.Text)
        objWriter.Close()
        MessageBox.Show("Nastavení bylo uloženo")
        fn_save_file_setting = True
        My.Forms.Main_Form.tb_control.SelectedIndex = 0
    End Function



    Function fn_dgw_tablebuild() As Boolean

        fn_dgw_tablebuild = False
        Dim summaryline()
        Try

            My.Forms.Main_Form.dgw_query_view.AutoGenerateColumns = True
            If My.Forms.Main_Form.chb_format_debbuger.Checked = True Then
                My.Forms.Main_Form.dgw_query_view.DataSource = dgw_table_schema
            Else
                My.Forms.Main_Form.dgw_query_view.DataSource = dgw_source
            End If

            If My.Forms.Main_Form.dgw_query_view.ColumnCount > 0 Then
                If (My.Forms.Main_Form.Bounds.Width - (131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 43)) < (My.Forms.Main_Form.dgw_query_view.ColumnCount * (100 * (1 + (CInt(My.Forms.Main_Form.txt_font_size.Text) / 100)))) Then
                    My.Forms.Main_Form.dgw_query_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    My.Forms.Main_Form.dgw_query_view.AutoResizeColumns()
                Else
                    My.Forms.Main_Form.dgw_query_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    My.Forms.Main_Form.dgw_query_view.AutoResizeColumns()
                End If
            End If


            Try
                My.Forms.Main_Form.dgw_query_view.SelectedRows.Clear()
            Catch ex As Exception
            End Try



            'enable SUBBUTTON
            For i As Integer = 0 To (user_field_count) Step 1
                For Each subbutton In My.Forms.Main_Form.p_sub_buttons.Controls.OfType(Of Button)()
                    If subbutton.Name = user_field_list(i, 0) And user_field_list(i, 2) = "True" Then

                        subbutton.Enabled = True
                    End If
                Next
            Next


            If My.Forms.Main_Form.dgw_query_view.Rows.Count > 0 Then My.Forms.Main_Form.btn_back.Enabled = True

            Dim sumcolpart = columnsum.Split("/")

            ReDim summaryline(My.Forms.Main_Form.dgw_query_view.Rows(0).Cells.Count - 1)
            For Each row As DataGridViewRow In My.Forms.Main_Form.dgw_query_view.Rows
                For Each cell In row.Cells
                    If row.Cells.IndexOf(cell) > 0 Then

                        '            If columnsum.Length > 0 Then

                        If IsNumeric(cell.value) = True And ((columnsum.Length > 0 And sumcolpart.Contains(row.Cells.IndexOf(cell) + 1)) Or columnsum.Length = 0) Then
                            summaryline(row.Cells.IndexOf(cell)) = summaryline(row.Cells.IndexOf(cell)) + cell.value
                        Else
                            summaryline(row.Cells.IndexOf(cell)) = ""
                        End If
                    Else
                        summaryline(row.Cells.IndexOf(cell)) = "Celkem"
                    End If
                Next
            Next


            My.Forms.Main_Form.dgw_summary_view.CancelEdit()
            My.Forms.Main_Form.dgw_summary_view.Columns.Clear()
            My.Forms.Main_Form.dgw_summary_view.DataSource = Nothing

            If noautosum = False Or (noautosum = True And columnsum.Length > 0) Then
                For i = 0 To summaryline.Length - 1 Step 1
                    My.Forms.Main_Form.dgw_summary_view.Columns.Add(i.ToString, i.ToString)
                Next

                My.Forms.Main_Form.dgw_summary_view.Rows.Add()
                For i = 0 To summaryline.Length - 1 Step 1
                    My.Forms.Main_Form.dgw_summary_view.Rows(0).Cells(i).ValueType = summaryline(i).GetType
                    My.Forms.Main_Form.dgw_summary_view.Rows(0).Cells(i).Value = CStr(summaryline(i))
                Next
            End If

            My.Forms.Main_Form.dgw_summary_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            My.Forms.Main_Form.dgw_summary_view.AutoResizeColumns()
            My.Forms.Main_Form.dgw_summary_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            My.Forms.Main_Form.dgw_summary_view.AutoResizeColumns()
            'My.Forms.Main_Form.dgw_summary_view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            'My.Forms.Main_Form.dgw_summary_view.AutoResizeColumns()

            If multiselect_posibility = True Then
                My.Forms.Main_Form.dgw_query_view.MultiSelect = True
            Else
                My.Forms.Main_Form.dgw_query_view.MultiSelect = False
            End If
            If My.Forms.Main_Form.chb_extensible_rows.Checked = True Then
                My.Forms.Main_Form.dgw_query_view.AllowUserToResizeRows = True
            Else
                My.Forms.Main_Form.dgw_query_view.AllowUserToResizeRows = False
            End If

            If My.Forms.Main_Form.chb_dgview_multiselect.Checked = True Then
                My.Forms.Main_Form.dgw_query_view.MultiSelect = True
            Else
                My.Forms.Main_Form.dgw_query_view.MultiSelect = False
            End If

            If My.Forms.Main_Form.chb_select_first_row.Checked = True Then
                My.Forms.Main_Form.dgw_query_view.ClearSelection()
                My.Forms.Main_Form.dgw_query_view.Rows(0).Selected = True
            Else
                My.Forms.Main_Form.dgw_query_view.ClearSelection()
            End If


            fn_dgw_tablebuild = True
        Catch ex As Exception
            For Each subbutton In My.Forms.Main_Form.p_sub_buttons.Controls.OfType(Of Button)()
                subbutton.Enabled = False
            Next
            MessageBox.Show("chyba")
        End Try

    End Function



    Function fn_txt_must_check() As Boolean
        fn_txt_must_check = True
        For i As Integer = 0 To (user_field_count) Step 1
            For Each textinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of TextBox)()
                If textinput.Name = user_field_list(i, 1) And user_field_list(i, 2) = "True" And textinput.Text.Length = 0 Then
                    fn_txt_must_check = False
                    If My.Forms.Main_Form.chb_must_field_message.Checked = True Then MessageBox.Show("Pole: " + UCase(user_field_list(i, 3)) + " je povinné")
                    textinput.Focus()
                End If
            Next
            For Each comboinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of ComboBox)()
                If comboinput.Name = user_field_list(i, 1) And user_field_list(i, 2) = "True" And comboinput.SelectedItem = "" Then
                    If My.Forms.Main_Form.chb_must_field_message.Checked = True Then MessageBox.Show("Pole: " + UCase(user_field_list(i, 3)) + " je povinné")
                    fn_txt_must_check = False
                    comboinput.Focus()
                End If
            Next
        Next
    End Function




    Function fn_reload_user_form(target_file As String)
        'My.Forms.Main_Form.p_user_inputs.Controls.Clear()
        'My.Forms.Main_Form.p_sub_buttons.Controls.Clear()

        For i = 0 To user_field_list.GetLength(0) - 1 Step 1
            Select Case user_field_list(i, 0)
                Case "TEXTBOX"
                    For Each textinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of TextBox)()
                        If textinput.Name = user_field_list(i, 1) Then
                            textinput.Text = user_field_list(i, 4)
                        End If
                    Next
                    For Each comboinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of ComboBox)()
                        If comboinput.Name = user_field_list(i, 1) Then
                            comboinput.SelectedItem = user_field_list(i, 4)
                        End If
                    Next
                    For Each checkinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of CheckBox)()
                        If checkinput.Name = user_field_list(i, 1) Then
                            checkinput.Checked = user_field_list(i, 4)
                        End If
                    Next
                    For Each dateinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of DateTimePicker)()
                        If dateinput.Name = user_field_list(i, 1) Then
                            dateinput.Value = user_field_list(i, 4)
                        End If
                    Next
            End Select
        Next
    End Function


    Function fn_set_app_size()
        My.Forms.Main_Form.lbl_available_reports.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.6)
        My.Forms.Main_Form.lbl_available_reports.Size = New Drawing.Size(131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text))
        My.Forms.Main_Form.lb_available_reports.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text))
        My.Forms.Main_Form.lb_available_reports.Location = New Point(3, CInt(My.Forms.Main_Form.txt_font_size.Text) + 10)
        My.Forms.Main_Form.lb_available_reports.Size = New Drawing.Size(131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text) * 5)
        My.Forms.Main_Form.lbl_user_inputs.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text) * 0.6)
        My.Forms.Main_Form.lbl_user_inputs.Size = New Drawing.Size(131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), CInt(My.Forms.Main_Form.txt_font_size.Text))
        My.Forms.Main_Form.lbl_user_inputs.Location = New Point(3, CInt(My.Forms.Main_Form.txt_font_size.Text) * 5 + CInt(My.Forms.Main_Form.txt_font_size.Text) + 5)
        My.Forms.Main_Form.p_user_inputs.Location = New Point(3, CInt(My.Forms.Main_Form.txt_font_size.Text) * 5 + CInt(My.Forms.Main_Form.txt_font_size.Text) + 5 + CInt(My.Forms.Main_Form.txt_font_size.Text))
        My.Forms.Main_Form.p_user_inputs.Size = New Drawing.Size(131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)), My.Forms.Main_Form.Bounds.Height - (39 + CInt(My.Forms.Main_Form.txt_font_size.Text) + CInt(My.Forms.Main_Form.txt_font_size.Text) * 5 + CInt(My.Forms.Main_Form.txt_font_size.Text) + 5 + CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5))
        My.Forms.Main_Form.tp_datalist.Font = New Font("Microsoft Sans Serif", CInt(My.Forms.Main_Form.txt_font_size.Text))
        My.Forms.Main_Form.tb_control.Size = New Point(My.Forms.Main_Form.Bounds.Width - (131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 23), My.Forms.Main_Form.Bounds.Height - CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5 - 38)
        My.Forms.Main_Form.tb_control.Location = New Point(131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 3, 0)

        My.Forms.Main_Form.dgw_query_view.Size = New Drawing.Size(My.Forms.Main_Form.Bounds.Width - (131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 43), My.Forms.Main_Form.Bounds.Height - CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5 - 118)
        My.Forms.Main_Form.dgw_query_view.RowTemplate.Height = CInt(My.Forms.Main_Form.txt_font_size.Text) + 20

        My.Forms.Main_Form.dgw_summary_view.Size = New Drawing.Size(My.Forms.Main_Form.Bounds.Width - (131 + (7 * CInt(My.Forms.Main_Form.txt_font_size.Text)) + 43), 48)
        My.Forms.Main_Form.dgw_summary_view.RowTemplate.Height = CInt(My.Forms.Main_Form.txt_font_size.Text) + 20

        My.Forms.Main_Form.lbl_subfunction.Location = New Point(3, My.Forms.Main_Form.Bounds.Height - CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5 - 38)
        My.Forms.Main_Form.lbl_subfunction.Size = New Drawing.Size(117, CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5)
        My.Forms.Main_Form.p_sub_buttons.Location = New Point(117, My.Forms.Main_Form.Bounds.Height - CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5 - 38)
        My.Forms.Main_Form.p_sub_buttons.Size = New Drawing.Size(My.Forms.Main_Form.Bounds.Width - 215, CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5)
        My.Forms.Main_Form.btn_back.Location = New Point(My.Forms.Main_Form.Bounds.Width - 95, My.Forms.Main_Form.Bounds.Height - CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5 - 38)
        My.Forms.Main_Form.btn_back.Size = New Drawing.Size(80, CInt(My.Forms.Main_Form.txt_font_size.Text) * 2.5)

    End Function




    Function fn_meLostFocus()
        For i As Integer = 0 To (user_field_count) Step 1
            For Each ctr In My.Forms.Main_Form.p_user_inputs.Controls
                If ctr.Name = user_field_list(i, 1) Then
                    If user_field_list(i, 2) = "True" Then
                        ctr.BackColor = must_color
                        ctr.ForeColor = Color.FromName(user_field_list(i, 6).ToString)
                    Else
                        If ctr.GetType.ToString = "System.Windows.Forms.CheckBox" And user_field_list(i, 5).ToString = "White" Then
                        Else
                            ctr.BackColor = Color.FromName(user_field_list(i, 5).ToString)
                        End If
                        ctr.ForeColor = Color.FromName(user_field_list(i, 6).ToString)
                    End If
                End If
            Next

        Next
    End Function


    Function fn_meGotFocus()
        For i As Integer = 0 To (user_field_count) Step 1
            ' For Each textinput In My.Forms.Main_Form.p_user_inputs.Controls.OfType(Of TextBox)()
            For Each ctr In My.Forms.Main_Form.p_user_inputs.Controls
                If ctr.Name = user_field_list(i, 1) And ctr.Focused = True Then
                    ctr.SelectAll()
                    If ctr.GetType.ToString <> "System.Windows.Forms.ComboBox" Then
                        ctr.BackColor = selected_color
                    End If

                End If
            Next
        Next
    End Function





    Function fn_detect_encoding(ByVal FileName As String) As System.Text.Encoding


        Dim enc As String = ""
        If System.IO.File.Exists(FileName) Then
            Dim filein As New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
            If (filein.CanSeek) Then
                Dim bom(4) As Byte
                filein.Read(bom, 0, 4)
                'EF BB BF       = utf-8
                'FF FE          = ucs-2le, ucs-4le, and ucs-16le
                'FE FF          = utf-16 and ucs-2
                '00 00 FE FF    = ucs-4
                If (((bom(0) = &HEF) And (bom(1) = &HBB) And (bom(2) = &HBF)) Or _
                    ((bom(0) = &HFF) And (bom(1) = &HFE)) Or _
                    ((bom(0) = &HFE) And (bom(1) = &HFF)) Or _
                    ((bom(0) = &H0) And (bom(1) = &H0) And (bom(2) = &HFE) And (bom(3) = &HFF))) Then
                    enc = "Unicode"
                Else
                    enc = "ASCII"
                End If
                'Position the file cursor back to the start of the file
                filein.Seek(0, System.IO.SeekOrigin.Begin)
                ' Do more stuff
            End If
            filein.Close()
        End If
        If enc = "Unicode" Then
            Return System.Text.Encoding.UTF8
        Else
            Return System.Text.Encoding.Default
        End If
    End Function


    Function fn_export_to_excel()
        Try
            My.Forms.Main_Form.Cursor = Cursors.WaitCursor
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Excel.ApplicationClass
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets.Item(1)

            For Each col As DataGridViewColumn In My.Forms.Main_Form.dgw_query_view.Columns
                Dim mybytearray As Byte()
                Dim myimage As Image
                xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText
                For Each rowa As DataGridViewRow In My.Forms.Main_Form.dgw_query_view.Rows
                    'rowa.Cells(col.Index).Selected = True
                    If rowa.Cells(col.Index).ValueType.Name = "TimeSpan" Then
                        xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1).value = Convert.ToString(rowa.Cells(col.Index).FormattedValue.ToString)
                    ElseIf rowa.Cells(col.Index).ValueType.Name = "Byte[]" Then
                        mybytearray = rowa.Cells(col.Index).Value
                        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
                        myimage = System.Drawing.Image.FromStream(ms)
                        Clipboard.Clear()
                        Clipboard.SetDataObject(myimage, True)
                        'xlWorkSheet.Paste(CType(xlWorkSheet.Range("A14:A14"), Excel.Range), a)
                        xlWorkSheet.Paste(CType(xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1), Excel.Range), New Bitmap(myimage))
                    Else
                        xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1).value = rowa.Cells(col.Index).Value
                    End If
                    'rowa.Cells(col.Index).Selected = False
                Next
            Next
            xlApp.Visible = True
            My.Forms.Main_Form.Cursor = Cursors.Default
        Catch ex As Exception
            My.Forms.Main_Form.Cursor = Cursors.Default
            MessageBox.Show("Chyba Exportu do XLS")
        End Try


    End Function


End Module
