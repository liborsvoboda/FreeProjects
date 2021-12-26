Module Settings_functions


    Function fn_exit_app()
        Try
            If My.Forms.main_form.tss_chat_server_status.BackColor = Color.LightGreen Then
                If My.Forms.main_form.server IsNot Nothing Then My.Forms.main_form.server.Close()
            End If
        Catch ex As Exception
        End Try

        Try
            My.Forms.main_form.IsClosing = True
            If My.Forms.main_form.client IsNot Nothing AndAlso My.Forms.main_form.client.isClientRunning Then
                My.Forms.main_form.client.Close(True)
                While My.Forms.main_form.client.isClientRunning
                    Application.DoEvents()
                End While
            End If
        Catch ex As Exception
        End Try

        Try
            If My.Forms.main_form.connect_to_another_clients.IsBusy = True Then
                My.Forms.main_form.connect_to_another_clients.Dispose()
            End If
        Catch ex As Exception
        End Try
        Application.Exit()

    End Function




End Module
