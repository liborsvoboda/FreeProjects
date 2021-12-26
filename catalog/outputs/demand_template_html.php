<?php

//require_once('../modules/tcpdf/config/tcpdf_config.php');
require_once ('../config/main_variables.php');
require_once ("../functions/php/sessions.inc.php");
//require_once ('../config/dbconnect.php');
require_once ('../config/mssql_dbconnect.php');
require_once ("../functions/php/knihovna.php");


         $temp_language =explode("_",$_SESSION["language"]);$temp_language='ckeditor_'.$temp_language[1];
         @$sql = "SELECT html_file FROM dbo.[120_demand_header] WHERE [demand_id]='".base64_decode(@$_GET["id"])."' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ); 


echo @$row[0];

?>


    
