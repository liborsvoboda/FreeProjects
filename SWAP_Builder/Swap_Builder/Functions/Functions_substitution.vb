Module Functions_substitution


    Function fn_add_login_substitution()
        Try
            For temp_i = 0 To Main_Form.substitution.GetLength(0)
                If Main_Form.substitution(temp_i, 0) = "sub[today_full]" Then
                    Main_Form.substitution(temp_i, 2) = Date.Now
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[today_date]" Then
                    Main_Form.substitution(temp_i, 2) = Date.Now.Date
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[today_time]" Then
                    Main_Form.substitution(temp_i, 2) = Date.Now.TimeOfDay.ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_year]" Then
                    Main_Form.substitution(temp_i, 2) = Date.Now.Year.ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_quarter_no]" Then
                    Main_Form.substitution(temp_i, 2) = DatePart(DateInterval.Quarter, Date.Now).ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_month_no]" Then
                    Main_Form.substitution(temp_i, 2) = Date.Now.Month.ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_month_name]" Then
                    Main_Form.substitution(temp_i, 2) = MonthName(Date.Now.Month, False).ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_week_no]" Then
                    Main_Form.substitution(temp_i, 2) = DatePart(DateInterval.WeekOfYear, Date.Now, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_day_no]" Then
                    Main_Form.substitution(temp_i, 2) = Date.Now.Day.ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_dayofweek_no]" Then
                    Main_Form.substitution(temp_i, 2) = Weekday(Date.Now, FirstDayOfWeek.Monday).ToString
                ElseIf Main_Form.substitution(temp_i, 0) = "sub[actual_day_name]" Then
                    Main_Form.substitution(temp_i, 2) = fn_translate(Date.Now.DayOfWeek.ToString)
                End If
            Next
        Catch ex As Exception
        End Try
    End Function


    Function fn_insert_substitution(ByVal value_name As String, ByVal value As String)
        Try
            For temp_i = 0 To Main_Form.substitution.GetLength(0) - 1
                If Main_Form.substitution(temp_i, 0) = value_name Then
                    Main_Form.substitution(temp_i, 2) = value
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(fn_translate("substitution_doesnt_exist"))
        End Try
    End Function


    Function fn_search_substitution(ByVal substitution As String) As String
        fn_search_substitution = Nothing
        Try
            For temp_i = 0 To Main_Form.substitution.GetLength(0)
                If Main_Form.substitution(temp_i, 0) = substitution Then
                    fn_search_substitution = Main_Form.substitution(temp_i, 2)
                End If
            Next
        Catch ex As Exception
        End Try
    End Function


    Function fn_substitution_dataview_filling()
        Try
            Dim c As DataGridViewColumn = New DataGridViewColumn()
            Dim cell As DataGridViewCell = New DataGridViewTextBoxCell()
            c.CellTemplate = cell
            If CBool(fn_search_substitution("sub[user_dataview_translate]")) Then
                c.HeaderText = fn_translate("subs_actual_value")
            Else
                c.HeaderText = "subs_actual_value"
            End If
            c.Name = "subs_actual_value"
            Main_Form.dgw_query_view.Columns.Remove("subs_actual_value")
            c.Visible = True
            c.SortMode = DataGridViewColumnSortMode.Programmatic
            Main_Form.dgw_query_view.Columns.Insert(2, c)

            For Each row As DataGridViewRow In Main_Form.dgw_query_view.Rows
                'MessageBox.Show(fn_search_substitution(row.Cells.Item(1).Value))
                row.Cells.Item(2).Value = fn_search_substitution(row.Cells.Item(1).Value)
            Next
            Main_Form.dgw_query_view.Refresh()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function


End Module