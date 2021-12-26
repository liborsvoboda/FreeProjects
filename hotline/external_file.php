<? // for external access
mysql_connect('127.0.0.1', 'root', 'password');
mysql_select_db('multiapp') or die (MySQL_Error());
mysql_query("SET NAMES 'utf8'");

$icodata = mysql_query("select icon,mime_type,icon_name from ".@$_GET["tbl"]." where id='".@$_GET["id"]."'");
//Header ("Content-type:".mysql_result($icodata,0,1)."");
header ("Content-Type: application/download");
header ("Content-Disposition: attachment; filename=".mysql_result($icodata,0,2));
print mysql_result($icodata,0,0);
?>
