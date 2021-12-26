<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
require_once ("./config/mssql_dbconnect.php");


if (@$_SESSION["lnamed"]) {
require_once ("./functions/php/ip_visitor.php");
//sqlsrv_close($conn);

?>

<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('karat_tpv_products',$_SESSION['language']);?>";
 var material_count=0;
 var fn_records="";
 </script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />
<link rel="stylesheet" type="text/css" href='./css/default/karat.css' />


<? //saving

// end of saving
?>

</head>


<Body>
<table id='fullframetable' onselectstart="return false;" >
<tr style='width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >
<?
$load_data=mysql_query("select * from karat_tpv_products_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):
$checked_menu=0;

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,2) or (!@$_REQUEST["option"] && $load==0)) {echo " class=\"bookmarkin\" ";$checked_menu=1;}
        if ($checked_menu<>1 && @$_REQUEST["option"]<>mysql_result($load_data,$load,2)) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./karat_tpv_products.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,2),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >


<?if (!@$_REQUEST["option"] or @$_REQUEST["option"]=="karat_tpv_products") { // karat_conf_reporting
?><div id='bookmark' >
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:100%;'><td style='width:100%;height:100%;' >
<form action='<?echo $_SERVER["PHP_SELF"];?>' id='form1' name='form1' method='post' enctype="multipart/form-data">
<table id='table_desc' style='width:100%;height:100%;' >
<tr>
    <td style='width:30%;vertical-align: top;text-align:left;' >
    <?echo dictionary("nomenklatura",$_SESSION["language"]);?>
        <input type="text" id=sel_value1 name=sel_value1 onkeyup="checkcard('sel_value1','checkcard',this.value,'dba.[v_zahlavi]','0');" style="width:100%;text-align:center;" autocomplete="off" >
        <div id='checkcard' ></div><hr />
        <div id='header_info'  ></div>
    </td>
    <td style='width:70%;' >
    <div id='fn_tpv_tree' style="width:100%;height:100%;overflow:auto;" ></div>
    </td>
</tr>
</table>

</form></td></tr></table>
</div><?
}?>


</td></tr></table></body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/karat_tpv_products.js");

sqlsrv_close($conn);

?>



<?}?>