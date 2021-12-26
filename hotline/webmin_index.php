<?
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
?>
<html>
<head>
<meta name="google-site-verification" content="XSq8LlGyJ4Z2m6oiiDUDuv6cjIvBfbru6-p9kQahunI" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<link rel="icon" href='<?echo mysql_result(mysql_query("select value from webmin_main_sett where name='web_site_url' "),0,0);?>/ajax_functions.php?icon=yes&tbl=<?echo base64_encode("webmin_main_sett")."&id=".base64_encode("6");?>' type="image/x-icon" />
<link rel="shortcut icon" href='<?echo mysql_result(mysql_query("select value from webmin_main_sett where name='web_site_url' "),0,0);?>/ajax_functions.php?icon=yes&tbl=<?echo base64_encode("webmin_main_sett")."&id=".base64_encode("6");?>' type="image/x-icon">
<meta http-equiv="Content-language" content="cs">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title><?echo mysql_result(mysql_query("select value from webmin_main_sett where name='site_name' "),0,0);?></title>

<link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" />
<link rel="stylesheet" href="./css/website/default.css" type="text/css" />

<?require_once ("./functions/js/website/functions.js");?>
<script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script>
<script type="text/javascript">
Shadowbox.init({
    modal: true,
    displayNav:         true,
    autoplayMovies:     true
});
</script>

</head>
<body style='text-align:center;width:<?echo mysql_result(mysql_query("select value from webmin_main_sett where name='site_width' "),0,0);?>;' >

<div id="banner"></div>

<div id="full_data"><span id="links">
<?$load_data=mysql_query("select * from www_links order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while (mysql_result($load_data,$load,0)):
echo "<p class=\"link_out_item\" onmouseout=\"className='link_out_item';\" onmouseover=\"className='link_on_item';\">".mysql_result($load_data,$load,2)."</p>";
$load++;endwhile;?>
</span><span id="data">
<?
$load_data=mysql_query("select * from www_menu where submenu_parent_id='0' order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while (mysql_result($load_data,$load,0)):

echo "<span class='out_menu' onmouseout=\"className='out_menu';\" onmouseover=\"className='on_menu';\" >".mysql_result($load_data,$load,2)."</span>";



$load++;endwhile;


?><span id="search"><input type="text" /></span></span><span id="reklama"></span>

</div>




<?$load_data=mysql_query("select * from www_photogallery order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());?>
<div id="photogallery"><span id="left_arrow" onclick="position('<?echo mysql_num_rows($load_data);?>');movegallery('2','<?echo mysql_num_rows($load_data);?>');"></span><span id="photos">
<div style=position:relative;width:30000;height:100px;><?

$load=0;while (mysql_result($load_data,$load,0)):

echo "<a href='./ajax_functions.php?icon=yes&?tbl=".base64_encode("www_photogallery")."&id=".base64_encode(mysql_result($load_data,$load,0))."' rel='shadowbox;width=200;height=200' ><img id='picture".$load."' class='picture' style='left:0px;' src='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_photogallery")."&id=".base64_encode(mysql_result($load_data,$load,0))."' /></a>";
 
$load++;endwhile;?></div></span><span id="right_arrow" onclick="position(<?echo mysql_num_rows($load_data);?>);movegallery('1','<?echo mysql_num_rows($load_data);?>');"></span></div>



<div id="webservice">© COPYRIGHT 2013 ALL RIGHTS RESERVED KLIKNETEZDE.CZ | • Web provozuje společnost © <a id="mailto" href=mailto:author@KlikneteZde.Cz>KLIKNETEZDE.CZ 2013</a></div>


</body>
</html>
