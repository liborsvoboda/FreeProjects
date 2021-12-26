Module Functions_arrays


    Function fn_load_languages()
        Dim tmp_query As String = "SELECT systemname"

        fn_sql_request("SELECT * FROM [dbo].[dictionary] WHERE enabled=1 AND released = 1", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)

        For Each myField In dgw_table_schema.Rows
            'For each property of the field...
            For Each myProperty In dgw_table_schema.Columns
                'Display the field name and value.
                If myProperty.ColumnName = "ColumnName" And myField(myProperty).ToString().Length > 3 Then

                    If myField(myProperty).ToString().Substring(0, 3) = "lb_" Then
                        'MessageBox.Show(myField(myProperty).ToString().Substring(3, myField(myProperty).ToString().Length - 3))
                        tmp_query += "," + myField(myProperty).ToString() + " as '" + myField(myProperty).ToString() + "'"

                        My.Forms.Main_Form.lb_global_settings_default_language.Items.Add(myField(myProperty).ToString().Substring(3, myField(myProperty).ToString().Length - 3))
                    End If
                End If
            Next
        Next

        fn_sql_request(tmp_query + " FROM [dbo].[dictionary] WHERE released = 1 AND enabled = 1", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)

        ReDim My.Forms.Main_Form.language_array(My.Forms.Main_Form.sql_array.GetLength(0), dgw_table_schema.Rows.Count - 1)
        Array.Copy(My.Forms.Main_Form.sql_array, My.Forms.Main_Form.language_array, My.Forms.Main_Form.sql_array.Length)
    End Function


    Function fn_load_default_settings()
        fn_sql_request("SELECT [name],[configuration] FROM [dbo].[app_setting] WHERE enabled = 1 AND released = 1 ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)

        ReDim My.Forms.Main_Form.default_settings(My.Forms.Main_Form.sql_array.GetLength(0), dgw_table_schema.Rows.Count - 1)
        Array.Copy(My.Forms.Main_Form.sql_array, My.Forms.Main_Form.default_settings, My.Forms.Main_Form.sql_array.Length)
    End Function



    Function fn_load_substitution()
        fn_sql_request("SELECT name,CASE WHEN [datatype] = 1 THEN CAST([date] as VARCHAR) WHEN [datatype] = 2 THEN CAST([time] as VARCHAR) WHEN [datatype] = 3 THEN CAST([datetime] as VARCHAR) WHEN [datatype] = 4 THEN CAST([integer] as VARCHAR) WHEN [datatype] = 5 THEN CAST([float] as VARCHAR) WHEN [datatype] = 6 THEN [varchar] WHEN [datatype] = 7 THEN [text] WHEN [datatype] = 8 THEN [image_url] WHEN [datatype] = 9 THEN CAST([bit] as VARCHAR) ELSE '' END as default_value,expression as program_value,note FROM [dbo].[substitution] WHERE enabled = 1 AND released = 1 ", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)

        ReDim My.Forms.Main_Form.substitution(My.Forms.Main_Form.sql_array.GetLength(0) - 1, dgw_table_schema.Rows.Count - 1)
        Array.Copy(My.Forms.Main_Form.sql_array, My.Forms.Main_Form.substitution, My.Forms.Main_Form.sql_array.Length)
    End Function



End Module
