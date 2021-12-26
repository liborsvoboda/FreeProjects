<?php

require_once('../modules/tcpdf/tcpdf.php');
//require_once('../modules/tcpdf/config/tcpdf_config.php');
require_once ('../config/main_variables.php');
require_once ("../functions/php/sessions.inc.php");
//require_once ('../config/dbconnect.php');
require_once ('../config/mssql_dbconnect.php');
require_once ("../functions/php/knihovna.php");

// create new PDF document
$pdf = new TCPDF('P', PDF_UNIT, PDF_PAGE_FORMAT, true, 'UTF-8', false);

$pdf->SetCreator(PDF_CREATOR);
$pdf->SetAuthor('company');
$pdf->SetTitle('Product Catalog');
$pdf->SetSubject('Product Catalog');
$pdf->SetKeywords('Product Catalog, Products, Shop');

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

 //xxxx doc params
         $temp_language =explode("_",$_SESSION["language"]);$temp_language='ckeditor_'.$temp_language[1];
         @$sql = "SELECT data_type,systemname,".mssecuresql($temp_language)." FROM dbo.[100_main_setting] WHERE (data_type LIKE 'company_%' OR data_type='demand_printed_text') ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {

        switch (@$row[0]) {
            case "company_name": 
            $company_name = @$row[1];
            break;
            
            case "company_address": 
            $company_address = @$row[1];
            break;
        
            case "company_country": 
            $company_country = @$row[1];
            break;
            
            case "company_phone": 
            $company_phone = @$row[1];
            break;
            
            case "company_fax": 
            $company_fax = @$row[1];
            break;
            
            case "company_email": 
            $company_email = @$row[1];
            break;
            
            case "company_ic": 
            $company_ic = @$row[1];
            break;
            
            case "company_dic": 
            $company_dic = @$row[1];
            break;
            
            case "demand_printed_text": 
            $demand_printed_text = @$row[2];
            break;
            
        }
    }

    
    
    @$sql = "SELECT TOP 1 * FROM dbo.[120_demand_header] WHERE [demand_id] = '".base64_decode(@$_GET["id"])."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    

 $print_demand_no=  @$row["demand_id"];
 $print_subscriber_name=  @$row["customer_name"];
 $print_subscriber_address=  @$row["street"]."; ".@$row["post_code"]." ".@$row["city"];
 $print_subscriber_country=  @$row["country"];
 $print_subscriber_phone=  @$row["phone"];
 $print_subscriber_email = @$row["email"];
 $print_subscriber_ic = @$row["ico"];
 $print_subscriber_dic = @$row["dic"];
 $print_delivery_date = $dnescst;
 $print_invoice_receipt = dictionary(@$row["payment_terms"],@$_SESSION["language"]);
 $print_shipping = dictionary(@$row["shipping"],@$_SESSION["language"]); //[dba].[dpcis]
 $print_offer_validity =" 2 měsíce"; //
//xxxxx end of definition 



$fontname = $pdf->addTTFfont('../modules/tcpdf/fonts/verdana.ttf', 'TrueTypeUnicode', 'verdana', 32);
$fontname = $pdf->addTTFfont('../modules/tcpdf/fonts/verdanab.ttf', 'TrueTypeUnicode', 'verdanab', 32);
$pdf->SetFont('verdanab', '', 18, '', true);
$pdf->MultiCell(160,10,dictionary('recap_demand',@$_SESSION["language"]).": ".$print_demand_no,0,"L",0,1,"","16");

$pdf->SetFont('verdanab', '', 10, '', true);
$pdf->Cell(99, 0, dictionary("supplier",@$_SESSION["language"]), 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("subscriber",@$_SESSION["language"]), 0, 1, 'L', 0, '', 0);

$pdf->SetFont('verdanab', '', 12, '', true);
$pdf->Cell(99, 0, $company_name, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, $print_subscriber_name, 0, 1, 'L', 0, '', 0);

$pdf->SetFont('verdana', '', 10, '', true);
$pdf->Cell(99, 0, $company_address, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, $print_subscriber_address, 0, 1, 'L', 0, '', 0);
$pdf->Cell(99, 0, $company_country, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, $print_subscriber_country, 0, 1, 'L', 0, '', 0);

$pdf->Cell(99, 0, dictionary("short_phone",@$_SESSION["language"]).":".$company_phone, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("short_phone",@$_SESSION["language"]).":".$print_subscriber_phone, 0, 1, 'L', 0, '', 0);

$pdf->Cell(99, 0, dictionary("fax_no",@$_SESSION["language"]).":".$company_fax, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("email",@$_SESSION["language"]).":".$print_subscriber_email, 0, 1, 'L', 0, '', 0);

$pdf->Cell(99, 0, dictionary("email",@$_SESSION["language"]).":".$company_email, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("ic",@$_SESSION["language"]).":".$print_subscriber_ic, 0, 1, 'L', 0, '', 0);

$pdf->Cell(99, 0, dictionary("ic",@$_SESSION["language"]).":".$company_ic, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("dic",@$_SESSION["language"]).":".$print_subscriber_dic, 0, 1, 'L', 0, '', 0);

$pdf->Cell(99, 0, dictionary("dic",@$_SESSION["language"]).":".$company_dic, 0, 0, 'L', 0, '', 0);
$pdf->Cell(99, 0, "", 0, 1, 'L', 0, '', 0);


$pdf->Cell(198,0,"","B",1);
$pdf->Ln(2);

$pdf->Cell(99, 0, dictionary("issue_date",@$_SESSION["language"]).":".$dnescst, 0, 1, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("delivery_date",@$_SESSION["language"]).":".$print_delivery_date, 0, 1, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("invoice_receipt",@$_SESSION["language"]).":".$print_invoice_receipt, 0, 1, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("payment_terms",@$_SESSION["language"]).":".$dnescst, 0, 1, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("shipping",@$_SESSION["language"]).":".$print_shipping, 0, 1, 'L', 0, '', 0);
$pdf->Cell(99, 0, dictionary("offer_validity",@$_SESSION["language"]).":".$print_offer_validity, 0, 1, 'L', 0, '', 0);

$pdf->Cell(198,0,"","B",1);
$pdf->Ln(2);

$pdf->MultiCell(198,10,$demand_printed_text,0,"J",0,1,"","",1,0,1);
$pdf->Cell(198,0,"","B",1);
$pdf->Ln(2);


$pdf->Cell(19, 0, dictionary("nr",@$_SESSION["language"]), 0, 0, 'L', 0, '', 1);
$pdf->Cell(50, 0, dictionary("catalogue_number",@$_SESSION["language"]), 0, 0, 'L', 0, '', 1);
$pdf->Cell(30, 0, dictionary("quantity",@$_SESSION["language"]), 0, 0, 'R', 0, '', 1);
$pdf->Cell(33, 0, dictionary("tax_rate",@$_SESSION["language"]), 0, 0, 'R', 0, '', 1);
$pdf->Cell(33, 0, dictionary("unit_price",@$_SESSION["language"]), 0, 0, 'R', 0, '', 1);
$pdf->Cell(33, 0, dictionary("total_price",@$_SESSION["language"]), 0, 1, 'R', 0, '', 1);



    //to reading field cycle
    
        @$sql = "SELECT * FROM dbo.[120_demand_item] WHERE [demand_id] = '".base64_decode(@$_GET["id"])."' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH )) {


    //$cykl=0;while(@$_SESSION["product"][$cykl]):
    @$data=explode(":", @$row["record"]);

//0:15:141653 CZK:::2STS-000643:Skříň stoj STS  2D Ocel Lak Lt:STS 140404 2D - 0643:44 Kg:KS :1400x400x400:0643:9443.53 CZK:2D:100_model:23:lang_cs:12O2BE0100000001:2D:42VT1904:1400:400:400:

    if (@$_SESSION["global_discount"]<>0){$final_line_price=@$data[4];
                    $final_unit_price=@$data[3];
    } else {$final_line_price=@$data[2];$final_unit_price=@$data[12];
    }
     
    $pdf->Cell(19, 0, (@$data[0]+1), 0, 0, 'L', 0, '', 1);
    $pdf->Cell(50, 0, @$data[5], 0, 0, 'L', 0, '', 1);
    $pdf->Cell(30, 0, @$data[1]." ".@$data[9], 0, 0, 'R', 0, '', 1);
    $pdf->Cell(33, 0, @$data[23], 0, 0, 'R', 0, '', 1);
    $pdf->Cell(33, 0, @$final_unit_price, 0, 0, 'R', 0, '', 1);
    $pdf->Cell(33, 0, @$final_line_price, 0, 1, 'R', 0, '', 1);

//    if (@$_SESSION["product"][$cykl][23]){
//        $pdf->Cell(19, 0,"");$pdf->MultiCell(179,0,dictionary("note",@$_SESSION["language"]).": ".@$_SESSION["product"][$cykl][23],0,"J",0,1,"","",1,0,1);
//    }


//$cykl++;endwhile;
}    

//header + first record
//$load_header=mysql_query("select * from task_manager_request where document_no ='".securesql(@$_GET["id"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
//
//// delka - 0 cely radek,sirka,data,ram,?,zarovnani,?,link + title,typ radku
////$pdf->Cell(0, 0, @$_GET['id'], 1, 1, 'C', 0, '', 4);
//$pdf->AddFont('courier','','courier.php');
//$pdf->SetFont('courier', '', 10, '', true);
//$pdf->SetFont('helvetica', '', 10, '', false);
//$pdf->SetFont('freeserif', '', 10, '', false);
//
//$pdf->Cell(132, 0, dictionary("title",$_SESSION["language"]).": ".mysql_result($load_header,0,1), 1, 0, 'L', 0, '', 3);
//$pdf->Cell(66, 0, dictionary("tm_id",$_SESSION["language"]).": ".@$_GET['id'], 1, 0, 'R', 0, '', 2);
//$pdf->Ln();
//
//$pdf->MultiCell(66, 9, dictionary("task_manager_priorities",$_SESSION["language"])."\n".mysql_result($load_header,0,4) , 1, 'C', 0, 0, '', '', true);
//$pdf->MultiCell(66, 9, dictionary("status",$_SESSION["language"])."\n".mysql_result($load_header,0,3) , 1, 'C', 0, 0, '', '', true);
//$pdf->MultiCell(66, 9, dictionary("score",$_SESSION["language"])."\n".mysql_result($load_header,0,13) , 1, 'C', 0, 0, '', '', true);
//$pdf->Ln(9);
//
////load attachments
//@$load_form_data=mysql_query("select * from task_manager_attachment where parent_no='".securesql(@$_GET["id"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
//if (@mysql_num_rows(@$load_form_data)){
//$pdf->Cell(198, 5, dictionary("attachments",$_SESSION["language"]).": ", 1, 0, 'L', 0, '', 3);
//    $cycle=0;while(@mysql_result($load_form_data,$cycle,0)):
//        $pdf->Image('../images/attachment.png', 20+(($cycle+1)*5), '36', 4, 4,'PNG','../ajax_functions.php?show_file=yes&tbl=task_manager_attachment&id='.@mysql_result($load_form_data,$cycle,0));
//    $cycle++;endwhile;
//$pdf->Ln(5);
//}
//
//
//
//$load_data=mysql_query("select * from task_manager_history where parent_no ='".securesql(@$_GET["id"])."' order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
//
//$cycle=0;while(@mysql_result($load_data,$cycle,0)):
//    $pdf->MultiCell(198, 5, dictionary("message",$_SESSION["language"]).": ".str_replace("\t"," : ",mysql_result($load_data,$cycle,3))."\n\n" , 1, 'L', 0, 0, '', '', true);
//$pdf->Ln();
//
//
//
//
//$cycle++;endwhile;
//


// create some HTML content
//$html = "hi";
//$html = <<<EOD
//EOD;
// output the HTML content
//$pdf->writeHTML($html, true, 0, true, 0);

// reset pointer to the last page
$pdf->lastPage();

$pdf->Output(base64_decode(@$_GET["id"]).'.pdf', 'I');
?>
