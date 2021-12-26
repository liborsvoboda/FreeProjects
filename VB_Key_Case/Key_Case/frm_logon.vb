Public Class Frm_logon


    Private Sub Form_logon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Forms.Main_form.Enabled = False

        If fn_sql_request("SELECT ppom.oscislo + ' '+ ppom.prijmeni + ' '+ ppom.jmeno FROM [KARAT_PGI].dba.ppom ppom,[KARAT_PGI].dba.zarazeni zar,[KARAT_PGI].dba.funkce fun WHERE ppom.oscislo = zar.oscislo AND ppom.poradipp = zar.poradipp AND ppom.pp_od <=GETDATE() AND ppom.pp_do >=GETDATE() AND zar.od_data  <=GETDATE() AND zar.do_data >=GETDATE() AND zar.funkce = 'VRAT' AND zar.funkce = fun.funkce AND fun.fiskalni_rok = zar.fiskalni_rok", "SELECT") = False Then
            MsgBox("Nebyly nalezeni žádní uživatelé v Databázi")
        Else
            Me.cb_user_list.Items.Clear()
            For Each field As String In My.Forms.Main_Form.sql_array
                Try
                    If Not field.Trim Is Nothing Then
                        cb_user_list.Items.Add(field)
                    End If
                Catch ex As Exception
                End Try
            Next
        End If


        Me.Focus()
        Me.cb_user_list.Focus()

    End Sub


    Private Sub cb_user_list_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_user_list.SelectedIndexChanged
        If cb_user_list.Text.Length > 0 Then
            btn_logon.Enabled = True
        Else
            btn_logon.Enabled = False
        End If
    End Sub


End Class