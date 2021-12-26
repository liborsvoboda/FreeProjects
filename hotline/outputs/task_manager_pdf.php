<?php

require_once('../modules/tcpdf/tcpdf.php');
//require_once('../modules/tcpdf/config/tcpdf_config.php');
require_once ('../config/main_variables.php');
require_once ("../functions/php/sessions.inc");
require_once ('../config/dbconnect.php');
require_once ("../functions/php/knihovna.php");

// create new PDF document
$pdf = new TCPDF('P', PDF_UNIT, PDF_PAGE_FORMAT, true, 'UTF-8', false);

$pdf->SetCreator(PDF_CREATOR);
$pdf->SetAuthor('company');
$pdf->SetTitle('Intranet Reporting');
$pdf->SetSubject('Task Manager System');
$pdf->SetKeywords('Task, Manager, System, Intranet');

// set default header data
$pdf->SetHeaderMargin('3');

$pdf->SetHeaderData(PDF_HEADER_LOGO, PDF_HEADER_LOGO_WIDTH, PDF_HEADER_TITLE.'', PDF_HEADER_STRING);

// set header and footer fonts
$pdf->setHeaderFont(Array(PDF_FONT_NAME_MAIN, '', PDF_FONT_SIZE_MAIN));
$pdf->setFooterFont(Array(PDF_FONT_NAME_DATA, '', PDF_FONT_SIZE_DATA));

// set default monospaced font
$pdf->SetDefaultMonospacedFont(PDF_FONT_MONOSPACED);

//set margins
//$pdf->SetMargins(PDF_MARGIN_LEFT, PDF_MARGIN_TOP, PDF_MARGIN_RIGHT);
$pdf->SetMargins('6', '22', '6');

//$pdf->SetHeaderMargin(PDF_MARGIN_HEADER);
$pdf->SetFooterMargin(PDF_MARGIN_FOOTER);

//set auto page breaks
$pdf->SetAutoPageBreak(TRUE, PDF_MARGIN_BOTTOM);

//set image scale factor
$pdf->setImageScale(PDF_IMAGE_SCALE_RATIO);

//set some language-dependent strings
$pdf->setLanguageArray($l);

// ---------------------------------------------------------

// IMPORTANT: disable font subsetting to allow users editing the document
$pdf->setFontSubsetting(false);

// add a page
$pdf->AddPage();

//header + first record
$load_header=mysql_query("select * from task_manager_request where document_no ='".securesql(@$_GET["id"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

// delka - 0 cely radek,sirka,data,ram,?,zarovnani,?,link + title,typ radku
//$pdf->Cell(0, 0, @$_GET['id'], 1, 1, 'C', 0, '', 4);
$pdf->AddFont('courier','','courier.php');
$pdf->SetFont('courier', '', 10, '', true);
$pdf->SetFont('helvetica', '', 10, '', false);
$pdf->SetFont('freeserif', '', 10, '', false);

$pdf->Cell(132, 0, dictionary("title",$_SESSION["language"]).": ".mysql_result($load_header,0,1), 1, 0, 'L', 0, '', 3);
$pdf->Cell(66, 0, dictionary("tm_id",$_SESSION["language"]).": ".@$_GET['id'], 1, 0, 'R', 0, '', 2);
$pdf->Ln();

$pdf->MultiCell(66, 9, dictionary("task_manager_priorities",$_SESSION["language"])."\n".mysql_result($load_header,0,4) , 1, 'C', 0, 0, '', '', true);
$pdf->MultiCell(66, 9, dictionary("status",$_SESSION["language"])."\n".mysql_result($load_header,0,3) , 1, 'C', 0, 0, '', '', true);
$pdf->MultiCell(66, 9, dictionary("score",$_SESSION["language"])."\n".mysql_result($load_header,0,13) , 1, 'C', 0, 0, '', '', true);
$pdf->Ln(9);

//xxxxx load attachments
@$load_form_data=mysql_query("select * from task_manager_attachment where parent_no='".securesql(@$_GET["id"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if (@mysql_num_rows(@$load_form_data)){
$pdf->Cell(198, 5, dictionary("attachments",$_SESSION["language"]).": ", 1, 0, 'L', 0, '', 3);
    $cycle=0;while(@mysql_result($load_form_data,$cycle,0)):
        $pdf->Image('../images/attachment.png', 20+(($cycle+1)*5), '36', 4, 4,'PNG','../ajax_functions.php?show_file=yes&tbl=task_manager_attachment&id='.@mysql_result($load_form_data,$cycle,0));
    $cycle++;endwhile;
$pdf->Ln(5);
}
//xxxxx


$load_data=mysql_query("select * from task_manager_history where parent_no ='".securesql(@$_GET["id"])."' order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$cycle=0;while(@mysql_result($load_data,$cycle,0)):
    $pdf->MultiCell(198, 5, dictionary("message",$_SESSION["language"]).": ".str_replace("\t"," : ",mysql_result($load_data,$cycle,3))."\n\n" , 1, 'L', 0, 0, '', '', true);
$pdf->Ln();




$cycle++;endwhile;



// create some HTML content
//$html = "hi";
//$html = <<<EOD
//EOD;
// output the HTML content
//$pdf->writeHTML($html, true, 0, true, 0);

// reset pointer to the last page
$pdf->lastPage();

// ---------------------------------------------------------

//Close and output PDF document
$pdf->Output(@$_GET['id'].'.pdf', 'I');

//============================================================+
// END OF FILE
//============================================================+
