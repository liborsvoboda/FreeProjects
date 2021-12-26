<?
include ("./config/dbconnect.php");
$icodata = mysql_query("select icon,mime_type from ".base64_decode(@$_GET["tbl"])." where id='".base64_decode(@$_GET["id"])."'");
Header ("Content-type: mysql_result($icodata,0,1)");
print mysql_result($icodata,0,0).".jpg";
?>
