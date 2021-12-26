<?php

//require_once('../modules/tcpdf/config/tcpdf_config.php');
require_once ('../config/main_variables.php');
require_once ("../functions/php/sessions.inc.php");
//require_once ('../config/dbconnect.php');
require_once ('../config/mssql_dbconnect.php');
require_once ("../functions/php/knihovna.php");


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

$xml = new SimpleXMLElement('<xml/>');


    $track = $xml->addChild('demand_header');
    $track->addChild('demand_no', base64_decode(@$_GET["id"]));
    $track->addChild('supplier_name', $company_name);
    $track->addChild('supplier_address', $company_address);
    $track->addChild('supplier_coutry', $company_country);
    $track->addChild('supplier_phone', $company_phone);
    $track->addChild('supplier_fax', $company_fax);
    $track->addChild('supplier_email', $company_email);
    $track->addChild('supplier_id', $company_ic);
    $track->addChild('supplier_vat', $company_dic);

    @$sql = "SELECT TOP 1 * FROM dbo.[120_demand_header] WHERE [demand_id] = '".base64_decode(@$_GET["id"])."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    
   $track->addChild('customer_name', @$row["customer_name"]);
    $track->addChild('customer_address', @$row["street"]."; ".@$row["post_code"]." ".@$row["city"]);
    $track->addChild('customer_country', @$row["country"]);
    $track->addChild('customer_phone', @$row["phone"]);
    $track->addChild('customer_email', @$row["email"] );
    $track->addChild('customer_id', @$row["ico"] );
    $track->addChild('customer_vat', @$row["dic"] );
    $track->addChild('delivery_date', $dnescst);
    $track->addChild('payment_terms', dictionary(@$row["payment_terms"],@$_SESSION["language"]));
    $track->addChild('shipping', dictionary(@$row["shipping"],@$_SESSION["language"]) );
    $track->addChild('offer_validity', " 2 měsíce");

   // $track = $xml->appendChild('demand_items');

@$sql = "SELECT * FROM dbo.[120_demand_item] WHERE [demand_id] = '".base64_decode(@$_GET["id"])."' ";
@$check = sqlsrv_query( $conn, $sql , $params, $options );


while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH )) {
    @$data=explode(":", @$row["record"]);
    if (@$_SESSION["global_discount"]<>0){$final_line_price=@$data[4];
                    $final_unit_price=@$data[3];
    } else {$final_line_price=@$data[2];$final_unit_price=@$data[12];
    }

    $track = $xml->addChild('demand_item');
    $track->addChild('demand_item_no', (@$data[0]+1));
    $track->addChild('catalog_no', @$data[5]);
    $track->addChild('amount', @$data[1]);
    $track->addChild('measure_unit', @$data[9]);
    $track->addChild('vat_rate', @$data[23]);
    $track->addChild('unit_price', @$final_unit_price);
    $track->addChild('line_price', @$final_line_price);

}

echo $xml->asXML();

?>
