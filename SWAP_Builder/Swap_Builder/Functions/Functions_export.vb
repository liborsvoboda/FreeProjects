Imports Microsoft.Office.Interop
Imports iTextSharp.text.xml
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports System.IO
Imports CSharpJExcel
Imports CSharpJExcel.Jxl.Write
Imports CSharpJExcel.Jxl.Format
Imports System.Data

Module Functions_export


    Function fn_export_to_xls()
        Try
            Main_Form.sfd_save_file.Filter = "XLS|*.xls"
            Main_Form.sfd_save_file.FileName = ""
            Dim res As String = Main_Form.sfd_save_file.ShowDialog()
            If res = vbOK Then
                If msoffice_ready = False Then
                    fn_cursor_waiting(True)
                    Dim ws As Jxl.WorkbookSettings = New Jxl.WorkbookSettings()
                    ws.setLocale(New System.Globalization.CultureInfo("en"))
                    Dim xlWorkBook = Jxl.Workbook.createWorkbook(New FileInfo(Main_Form.sfd_save_file.FileName), ws)
                    Dim xlWorkSheet As WritableSheet = xlWorkBook.createSheet(Main_Form.tv_menu.SelectedNode.Text, 0)
                    For Each col As DataGridViewColumn In My.Forms.Main_Form.dgw_query_view.Columns
                        Dim mybytearray As Byte()
                        Dim myimage As Image

                        Dim cell As Jxl.Write.Label = New Label(col.Index, 0, col.HeaderText)
                        xlWorkSheet.addCell(cell)
                        For Each rowa As DataGridViewRow In My.Forms.Main_Form.dgw_query_view.Rows
                            If rowa.Cells(col.Index).ValueType.Name = "TimeSpan" Then
                                cell = New Label(col.Index, rowa.Index + 1, Convert.ToString(rowa.Cells(col.Index).FormattedValue.ToString))
                                xlWorkSheet.addCell(cell)
                            ElseIf rowa.Cells(col.Index).ValueType.Name = "Byte[]" Then
                                If IsDBNull(rowa.Cells(col.Index).Value) Then
                                    cell = New Label(col.Index, rowa.Index + 1, vbNull)
                                Else
                                    mybytearray = rowa.Cells(col.Index).Value
                                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
                                    myimage = System.Drawing.Image.FromStream(ms)
                                    Clipboard.Clear()
                                    Clipboard.SetDataObject(myimage, True)
                                    xlWorkSheet.addImage(New WritableImage(col.Index, rowa.Index + 1, 1, 1, mybytearray))
                                End If
                            ElseIf rowa.Cells(col.Index).ValueType.Name = "Image" Then

                            Else
                                If IsDBNull(rowa.Cells(col.Index).Value) Then
                                    cell = New Label(col.Index, rowa.Index + 1, vbNull)
                                Else
                                    cell = New Label(col.Index, rowa.Index + 1, rowa.Cells(col.Index).Value)
                                End If
                                xlWorkSheet.addCell(cell)
                            End If
                        Next
                    Next
                    xlWorkBook.write()
                    xlWorkBook.close()
                Else
                    'WITH INSTALLED MS OFFICE
                    Dim xlApp As Excel.Application
                    Dim xlWorkBook As Excel.Workbook
                    Dim xlWorkSheet As Excel.Worksheet
                    Dim misValue As Object = System.Reflection.Missing.Value
                    xlApp = New Excel.ApplicationClass
                    xlWorkBook = xlApp.Workbooks.Add(misValue)
                    xlWorkSheet = xlWorkBook.Sheets.Item(1)
                    xlWorkSheet.Name = Main_Form.tv_menu.SelectedNode.Text

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
                            ElseIf rowa.Cells(col.Index).ValueType.Name = "Image" Then

                            Else
                                xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1).value = rowa.Cells(col.Index).Value
                            End If
                            'rowa.Cells(col.Index).Selected = False
                        Next
                    Next
                    xlApp.GetSaveAsFilename(Main_Form.sfd_save_file.FileName)
                    'xlApp.Visible = True
                End If

            End If

            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("export_xls_error") + vbNewLine + ex.Message)
        End Try
        fn_cursor_waiting(False)

    End Function



    Function fn_export_to_pdf()
        Try
            Main_Form.sfd_save_file.Filter = "PDF|*.pdf"
            Main_Form.sfd_save_file.FileName = ""

            Dim res As String = Main_Form.sfd_save_file.ShowDialog()
            If res = vbOK Then
                fn_cursor_waiting(True)


                'Creating iTextSharp Table from the DataTable data
                Dim pdfTable As New PdfPTable(Main_Form.dgw_query_view.ColumnCount)
                pdfTable.DefaultCell.Padding = 3
                pdfTable.WidthPercentage = 100
                pdfTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                pdfTable.DefaultCell.BorderWidth = 1
                ' pdfTable.RunDirection = PdfWriter.PDFXNONE

                'create local arial font / czech
                Dim my_font As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts), "arial.ttf"), "Identity-H", iTextSharp.text.pdf.BaseFont.EMBEDDED)
                Dim fontNormal As iTextSharp.text.Font = New iTextSharp.text.Font(my_font, 9, iTextSharp.text.Font.NORMAL)

                'Adding Header row
                For Each column As DataGridViewColumn In Main_Form.dgw_query_view.Columns
                    Dim cell As New PdfPCell(New iTextSharp.text.Phrase(column.HeaderText, fontNormal))
                    'Dim cell As New PdfPCell(New iTextSharp.text.Phrase(column.HeaderText, iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)))
                    cell.BackgroundColor = New text.Color(240, 240, 240)
                    pdfTable.AddCell(cell)
                Next

                'Adding DataRow
                For Each row As DataGridViewRow In Main_Form.dgw_query_view.Rows
                    For Each cell As DataGridViewCell In row.Cells
                        If cell.Value.ToString() <> "System.Byte[]" Then
                            pdfTable.AddCell(New iTextSharp.text.Phrase(cell.Value.ToString(), fontNormal))
                        Else
                            Dim mybytearray As Byte()
                            Dim myimage As Image
                            mybytearray = cell.Value
                            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(mybytearray)
                            myimage = System.Drawing.Image.FromStream(ms)
                            Dim img_input As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(mybytearray)
                            pdfTable.AddCell(img_input)
                        End If

                    Next
                Next


                'Saving to PDF
                Using stream As New FileStream(Main_Form.sfd_save_file.FileName, FileMode.Create)
                    Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10.0F, 10.0F, 10.0F, 0.0F)
                    PdfWriter.GetInstance(pdfDoc, stream)
                    pdfDoc.Open()
                    pdfDoc.Add(pdfTable)
                    pdfDoc.Close()
                    stream.Close()

                End Using
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("export_pdf_error") + vbNewLine + ex.Message)
        End Try
        fn_cursor_waiting(False)
    End Function


    Function fn_export_to_xml()
        Dim jumpedColumn = 0
        Try
            Main_Form.sfd_save_file.Filter = "XML|*.xml"
            Main_Form.sfd_save_file.FileName = ""

            Dim res As String = Main_Form.sfd_save_file.ShowDialog()
            If res = vbOK Then
                fn_cursor_waiting(True)

                Dim ds As DataSet = New DataSet With {.DataSetName = Main_Form.tv_menu.SelectedNode.Text}
                Dim dt As DataTable = New DataTable With {.TableName = "record"}
                Dim dtrow As DataRow
                Dim row_index As Double = 1
                For Each col As DataGridViewColumn In Main_Form.dgw_query_view.Columns
                    If col.Name <> "sys_Attachment" Then
                        dt.Columns.Add(col.HeaderText, col.ValueType)
                    Else
                        jumpedColumn += 1
                    End If
                Next

                For Each row As DataGridViewRow In Main_Form.dgw_query_view.Rows
                    dtrow = dt.NewRow
                    For i = jumpedColumn To Main_Form.dgw_query_view.Columns.Count - 1
                        If row.Cells.Item(i).ValueType.ToString() <> "System.Drawing.Image" Then
                            dtrow.Item(i - jumpedColumn) = row.Cells.Item(i).Value
                        ElseIf row.Cells.Item(i).Value Then
                            dtrow.Item(i - jumpedColumn) = DBNull.Value
                        End If

                    Next
                    dt.Rows.Add(dtrow)
                    row_index += 1
                Next
                ds.Tables.Add(dt)

                ds.WriteXml(Main_Form.sfd_save_file.FileName)
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("export_xml_error") + vbNewLine + ex.Message)
        End Try
        fn_cursor_waiting(False)
    End Function


    Function fn_export_to_csv()
        Try
            Main_Form.sfd_save_file.Filter = "CSV|*.csv"
            Main_Form.sfd_save_file.FileName = ""

            Dim res As String = Main_Form.sfd_save_file.ShowDialog()
            If res = vbOK Then
                fn_cursor_waiting(True)

                Dim headers = (From header As DataGridViewColumn In Main_Form.dgw_query_view.Columns.Cast(Of DataGridViewColumn)()
                               Select header.HeaderText).ToArray
                Dim rows = From row As DataGridViewRow In Main_Form.dgw_query_view.Rows.Cast(Of DataGridViewRow)() _
                           Where Not row.IsNewRow _
                           Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))
                Using sw As New IO.StreamWriter(Main_Form.sfd_save_file.FileName)
                    sw.WriteLine(String.Join(",", headers))
                    For Each r In rows
                        sw.WriteLine(String.Join(",", r))
                    Next
                End Using
            End If
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("export_csv_error") + vbNewLine + ex.Message)
        End Try
        fn_cursor_waiting(False)
    End Function


End Module
