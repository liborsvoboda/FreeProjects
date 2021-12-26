<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

if (@$_SESSION["lnamed"]) {?>
 <html>
<head>

<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('site_builder',$_SESSION['language']);?>";
</script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon" />
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />

<link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" />
<script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script>
<script type="text/javascript">
Shadowbox.init({
    modal: true,
    displayNav:         true,
    autoplayMovies:     true
});
</script>


<script type="text/javascript" src="./functions/js/add_on/tagcanvas.min.js"></script>


<?//saving
$rooturl=explode("?",$_SERVER["REQUEST_URI"]);

if (@$_REQUEST["option"]=='dictionary' && @$_REQUEST["savebtn"]){ //dictionary saving
	$exist_id=mysql_query("select id from webmin_dictionary order by id") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	$load_names=mysql_query("SHOW COLUMNS FROM webmin_dictionary") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	$load=0;while($load<(mysql_num_rows($exist_id)+1)):
		$variables="";$newrec="";$newrec1="";
		$load1=1;while(mysql_result($load_names,$load1,0)):
		    $newrec.=mysql_result($load_names,$load1,0);if (mysql_result($load_names,($load1+1),0)){$newrec.=",";}
		    $newrec1.="'".securesql(@$_REQUEST["value_".mysql_result($exist_id,$load,0)."_".mysql_result($load_names,$load1,0)])."'";if (mysql_result($load_names,($load1+1),0)){$newrec1.=",";}
			$variables.=mysql_result($load_names,$load1,0)."='".securesql(@$_REQUEST["value_".mysql_result($exist_id,$load,0)."_".mysql_result($load_names,$load1,0)])."'";
			if (mysql_result($load_names,($load1+1),0)){$variables.=",";}
		$load1++;endwhile;$variables.=" where id='".mysql_result($exist_id,$load,0)."' ";
if ($load<mysql_num_rows($exist_id)){$result=mysql_query("update webmin_dictionary set ".$variables." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
else {if (@$_REQUEST["value_".mysql_result($exist_id,$load,0)."_".mysql_result($load_names,1,0)]){$result=mysql_query("insert into webmin_dictionary (".$newrec.")VALUES(".$newrec1.") ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}}
$load++;endwhile;if ($result==1) {savechangesuccess("");}
}

if (@$_REQUEST["option"]=='dictionary' && @$_REQUEST["act"] && @$_REQUEST["act"]<>'add'){ //dictionary delete
	$result=mysql_query("delete from webmin_dictionary where id='".securesql(@$_REQUEST["act"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {deletesuccess("");}
}



if (@$_REQUEST["option"]=='main_settings' && @$_REQUEST["savebtn"]){ //main setting saving
	$exist_id=mysql_query("select id,type from webmin_main_sett where type<>'SYSTEM' order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	$load=0;while($load<mysql_num_rows($exist_id)):
        if (mysql_result($exist_id,$load,1)=="FILE" && @$_FILES['value_'.mysql_result($exist_id,$load,0)]['type']){	@$temp = @$_FILES['value_'.mysql_result($exist_id,$load,0)]['tmp_name'];@$mime = @$_FILES['value_'.mysql_result($exist_id,$load,0)]['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
        $val=" icon='".mysql_escape_string(@$logo)."', mime_type='".securesql(@$mime)."' ";}
        else {$val=" value='".securesql(@$_REQUEST["value_".mysql_result($exist_id,$load,0)])."' ";}
		$result=mysql_query("update webmin_main_sett set ".$val." where id='".securesql(mysql_result($exist_id,$load,0))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	$load++;endwhile;
	if ($result==1) {savechangesuccess("");}
}



if (@$_REQUEST["option"]=='menu_builder' && @$_REQUEST["formsavebtn"]){ // menu saving
	@$temp = @$_FILES['value8']['tmp_name'];@$mime = @$_FILES['value8']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
	if (@$_REQUEST["value4"]=='on'){@$_REQUEST["value4"]='ANO';} else {@$_REQUEST["value4"]='NE';}
	if (@$_REQUEST["value5"]=='on'){@$_REQUEST["value5"]='ANO';} else {@$_REQUEST["value5"]='NE';}
	if (@$_REQUEST["value6"]=='on'){@$_REQUEST["value6"]='ANO';} else {@$_REQUEST["value6"]='NE';}
  $result=mysql_query("insert into www_menu (position,name,seo,after_login,show_content,submenu,display_type,icon,mime_type,creator,create_date,tracking_url)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".securesql(@$_REQUEST["value3"])."','".securesql(@$_REQUEST["value4"])."','".securesql(@$_REQUEST["value5"])."','".securesql(@$_REQUEST["value6"])."','".securesql(@$_REQUEST["value7"])."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."','".securesql(@$_SESSION["lnamed"])."','".$dnest."','".securesql(@$_REQUEST["value9"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {sitemap();savesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='menu_builder' && @$_REQUEST["formsavebtn1"]){ // menu edit
@$temp = @$_FILES['value8']['tmp_name'];@$mime = @$_FILES['value8']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
	if (@$_REQUEST["value4"]=='on'){@$_REQUEST["value4"]='ANO';} else {@$_REQUEST["value4"]='NE';}
	if (@$_REQUEST["value5"]=='on'){@$_REQUEST["value5"]='ANO';} else {@$_REQUEST["value5"]='NE';}
	if (@$_REQUEST["value6"]=='on'){@$_REQUEST["value6"]='ANO';} else {@$_REQUEST["value6"]='NE';}
if (@$mime){$result=mysql_query("update www_menu set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',seo='".securesql(@$_REQUEST["value3"])."',after_login='".securesql(@$_REQUEST["value4"])."',show_content='".securesql(@$_REQUEST["value5"])."',submenu='".securesql(@$_REQUEST["value6"])."',display_type='".securesql(@$_REQUEST["value7"])."',icon='".mysql_escape_string(@$logo)."',mime_type='".securesql(@$mime)."',tracking_url='".securesql(@$_REQUEST["value9"])."' where id='".securesql(base64_decode(@$_REQUEST["value100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
	else {$result=mysql_query("update www_menu set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',seo='".securesql(@$_REQUEST["value3"])."',after_login='".securesql(@$_REQUEST["value4"])."',show_content='".securesql(@$_REQUEST["value5"])."',submenu='".securesql(@$_REQUEST["value6"])."',display_type='".securesql(@$_REQUEST["value7"])."',tracking_url='".securesql(@$_REQUEST["value9"])."' where id='".securesql(base64_decode(@$_REQUEST["value100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {sitemap();savechangesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='menu_builder' && @$_REQUEST["act"]){ //menu delete
	$result=mysql_query("delete from www_menu where id='".securesql(base64_decode(@$_REQUEST["act"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {sitemap();deletesuccess("");}
}

if (@$_REQUEST["option"]=='menu_builder' && @$_REQUEST["formsavebtn2"]){ // new submenu saving
	@$temp = @$_FILES['value8']['tmp_name'];@$mime = @$_FILES['value8']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
	if (@$_REQUEST["value4"]=='on'){@$_REQUEST["value4"]='ANO';} else {@$_REQUEST["value4"]='NE';}
	if (@$_REQUEST["value5"]=='on'){@$_REQUEST["value5"]='ANO';} else {@$_REQUEST["value5"]='NE';}
	if (@$_REQUEST["value6"]=='on'){@$_REQUEST["value6"]='ANO';} else {@$_REQUEST["value6"]='NE';}
  $result=mysql_query("insert into www_menu (position,name,seo,after_login,show_content,submenu,display_type,icon,mime_type,creator,create_date,tracking_url,submenu_parent_id)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".securesql(@$_REQUEST["value3"])."','".securesql(@$_REQUEST["value4"])."','".securesql(@$_REQUEST["value5"])."','".securesql(@$_REQUEST["value6"])."','".securesql(@$_REQUEST["value7"])."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."','".securesql(@$_SESSION["lnamed"])."','".$dnest."','".securesql(@$_REQUEST["value9"])."','".securesql(base64_decode(@$_REQUEST["value100"]))."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {sitemap();savesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='menu_builder' && @$_REQUEST["savebtn"]){ // menu value saving
		$result=mysql_query("update www_menu set ".securesql(@$_REQUEST["sel_lang"])." ='".securesql(stripslashes(@$_POST['editor_data']))."' where id='".securesql(base64_decode(@$_REQUEST["rec_id"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	if ($result==1) {sitemap();savechangesuccess("");}
}

if (@$_REQUEST["option"]=='menu_builder' && @$_REQUEST["formsavebtn3"]){ // SUBmenu edit
@$temp = @$_FILES['value8']['tmp_name'];@$mime = @$_FILES['value8']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
	if (@$_REQUEST["value4"]=='on'){@$_REQUEST["value4"]='ANO';} else {@$_REQUEST["value4"]='NE';}
	if (@$_REQUEST["value5"]=='on'){@$_REQUEST["value5"]='ANO';} else {@$_REQUEST["value5"]='NE';}
	if (@$_REQUEST["value6"]=='on'){@$_REQUEST["value6"]='ANO';} else {@$_REQUEST["value6"]='NE';}
if (@$mime){$result=mysql_query("update www_menu set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',seo='".securesql(@$_REQUEST["value3"])."',after_login='".securesql(@$_REQUEST["value4"])."',show_content='".securesql(@$_REQUEST["value5"])."',submenu='".securesql(@$_REQUEST["value6"])."',display_type='".securesql(@$_REQUEST["value7"])."',icon='".mysql_escape_string(@$logo)."',mime_type='".securesql(@$mime)."',tracking_url='".securesql(@$_REQUEST["value9"])."' where id='".securesql(base64_decode(@$_REQUEST["value100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
	else {$result=mysql_query("update www_menu set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',seo='".securesql(@$_REQUEST["value3"])."',after_login='".securesql(@$_REQUEST["value4"])."',show_content='".securesql(@$_REQUEST["value5"])."',submenu='".securesql(@$_REQUEST["value6"])."',display_type='".securesql(@$_REQUEST["value7"])."',tracking_url='".securesql(@$_REQUEST["value9"])."' where id='".securesql(base64_decode(@$_REQUEST["value100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {sitemap();savechangesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='photogallery' && @$_REQUEST["formsavebtn4"]){ // photogallery saving
	@$temp = @$_FILES['value4']['tmp_name'];@$mime = @$_FILES['value4']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
  $result=mysql_query("insert into www_photogallery (position,name,icon,mime_type,creator,create_date,parent_id)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."','".securesql(@$_SESSION["lnamed"])."','".$dnest."','".securesql(@$_REQUEST["value3"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {savesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='photogallery' && @$_REQUEST["act"]){ //photogallery delete
	$result=mysql_query("delete from www_photogallery where id='".securesql(base64_decode(@$_REQUEST["act"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {deletesuccess("");}
}

if (@$_REQUEST["option"]=='photogallery' && @$_REQUEST["formsavebtn5"]){ // photogallery edit
	@$temp = @$_FILES['value4']['tmp_name'];@$mime = @$_FILES['value4']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
if (@$mime){$result=mysql_query("update www_photogallery set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',icon='".mysql_escape_string(@$logo)."',mime_type='".securesql(@$mime)."',parent_id='".securesql(@$_REQUEST["value3"])."' where id='".securesql(base64_decode(@$_REQUEST["value100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
    else {$result=mysql_query("update www_photogallery set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',parent_id='".securesql(@$_REQUEST["value3"])."' where id='".securesql(base64_decode(@$_REQUEST["value100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {savechangesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='htaccess' && @$_REQUEST["savebtn"]){ // htaccess saving
$backupto=fopen("./.htaccess","w");
fwrite($backupto,@$_REQUEST["value1"]);
fclose($backupto);
if ($result==1) {savesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='css' && @$_REQUEST["savebtn"]){ // css saving
$backupto=fopen("./css/website/default.css","w");
fwrite($backupto,@$_REQUEST["value1"]);
fclose($backupto);
if ($result==1) {savesuccess(@$_REQUEST["value2"]);}
}




if (@$_REQUEST["option"]=='links' && @$_REQUEST["formsavebtn6"]){ // link menu saving
	@$temp = @$_FILES['value4']['tmp_name'];@$mime = @$_FILES['value4']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
 if (@$mime){$result=mysql_query("insert into www_links (position,name,value,icon,mime_type,type,style,creator,create_date)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".securesql(@$_REQUEST["value6"])."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."','".securesql(@$_REQUEST["value3"])."','".securesql(@$_REQUEST["value5"])."','".securesql(@$_SESSION["lnamed"])."','".$dnest."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
        else {$result=mysql_query("insert into www_links (position,name,value,type,style,creator,create_date)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".securesql(@$_REQUEST["value6"])."','".securesql(@$_REQUEST["value3"])."','".securesql(@$_REQUEST["value5"])."','".securesql(@$_SESSION["lnamed"])."','".$dnest."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {sitemap();savesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='links' && @$_REQUEST["act"]){ //link menu delete
	$result=mysql_query("delete from www_links where id='".securesql(base64_decode(@$_REQUEST["act"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {sitemap();deletesuccess("");}
}

if (@$_REQUEST["option"]=='links' && @$_REQUEST["formsavebtn7"]){ // menu edit
@$temp = @$_FILES['value4']['tmp_name'];@$mime = @$_FILES['value4']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
if (@$mime){$result=mysql_query("update www_links set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',value='".securesql(@$_REQUEST["value6"])."',icon='".mysql_escape_string(@$logo)."',mime_type='".securesql(@$mime)."',type='".securesql(@$_REQUEST["value3"])."',style='".securesql(@$_REQUEST["value5"])."' where id='".securesql(base64_decode(@$_REQUEST["v100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
	else {$result=mysql_query("update www_links set position='".securesql(@$_REQUEST["value1"])."',name='".securesql(@$_REQUEST["value2"])."',value='".securesql(@$_REQUEST["value6"])."',type='".securesql(@$_REQUEST["value3"])."',style='".securesql(@$_REQUEST["value5"])."' where id='".securesql(base64_decode(@$_REQUEST["v100"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {sitemap();savechangesuccess(@$_REQUEST["value2"]);}
}

if (@$_REQUEST["option"]=='user_lists' && @$_REQUEST["act"] && @$_REQUEST["act"]<>'add'){ //user_lists delete
	$result=mysql_query("delete from ".securesql(@$_REQUEST["tbl"])." where id='".securesql(@$_REQUEST["act"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {deletesuccess("");}
}

if (@$_REQUEST["option"]=='user_lists' && @$_REQUEST["savebtn"]){ //user_lists saving
	$exist_id=mysql_query("select id from ".securesql(@$_REQUEST["tbl"])." order by id") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	$load_names=mysql_query("SHOW COLUMNS FROM ".securesql(@$_REQUEST["tbl"])." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
	$load=0;while($load<(mysql_num_rows($exist_id)+1)):
		$variables="";$newrec="";$newrec1="";
		$load1=1;while(mysql_result($load_names,$load1,0)):
		    $newrec.=mysql_result($load_names,$load1,0);if (mysql_result($load_names,($load1+1),0)){$newrec.=",";}
		    $newrec1.="'".securesql(@$_REQUEST["value_".mysql_result($exist_id,$load,0)."_".mysql_result($load_names,$load1,0)])."'";if (mysql_result($load_names,($load1+1),0)){$newrec1.=",";}
			$variables.=mysql_result($load_names,$load1,0)."='".securesql(@$_REQUEST["value_".mysql_result($exist_id,$load,0)."_".mysql_result($load_names,$load1,0)])."'";
			if (mysql_result($load_names,($load1+1),0)){$variables.=",";}
		$load1++;endwhile;$variables.=" where id='".mysql_result($exist_id,$load,0)."' ";
if ($load<mysql_num_rows($exist_id)){$result=mysql_query("update ".securesql(@$_REQUEST["tbl"])." set ".$variables." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
else {if (@$_REQUEST["value_".mysql_result($exist_id,$load,0)."_".mysql_result($load_names,1,0)]){$result=mysql_query("insert into ".securesql(@$_REQUEST["tbl"])." (".$newrec.")VALUES(".$newrec1.") ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}}
$load++;endwhile;
if ($result==1) {savechangesuccess("");}
}



// end of saving
?>
</head>



<body>

<table id='fullframetable' >
<tr style='width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<?
$load_data=mysql_query("select * from webmin_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,2) or (!@$_REQUEST["option"]&& $load==0)) {echo " class=\"bookmarkin\" ";}
        if (@$_REQUEST["option"]<>mysql_result($load_data,$load,2)) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./webmin.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,2),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>

<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >







<?if (@$_REQUEST["option"]=='preview' or !@$_REQUEST["option"]) { // preview
?><iframe id='fullframetable' src="<?echo mysql_result(mysql_query("select value from webmin_main_sett where name='preview_url' "),0,0);?>">
</iframe>
<?} // end of preview
?>







<?if (@$_REQUEST["option"]=='photogallery') { // photogallery
?><div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:5px;' >
<span style='cursor:pointer;' onclick='addphoto();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("add_photo",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("add_photo",$_SESSION["language"]);?></span>
</div>

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:left;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("photogallery",$_SESSION["language"]);?></b></legend>
<p></p>
<?
$load_data=mysql_query("select * from www_photogallery order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):

echo "<span id=\"photo".mysql_result($load_data,$load,0)."\" class=\"wwwphotogalleryout\" onmouseout=\"className='wwwphotogalleryout';\" onmouseover=\"className='wwwphotogalleryin';\" title=\"".mysql_result($load_data,$load,1)." ".webmin_dictionary(mysql_result($load_data,$load,2),$_SESSION["language"])."\">";
echo "<a href='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_photogallery")."&id=".base64_encode(mysql_result($load_data,$load,0))."' rel='shadowbox;width=800;height=600' title='".dictionary("dictionary_name",$_SESSION["language"]).": ".mysql_result($load_data,$load,2)."' ><img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_photogallery")."&id=".base64_encode(mysql_result($load_data,$load,0))."' width='100' border='0' style='vertical-align:top;margin-bottom:25px;' ></a>";
echo"<span style='position:absolute;' ><span style='position:relative;left:-100px;top:3px;font-size:11px;'><img src='./images/edit.png' border='0' width='12' height='12'alt='".dictionary("edit_photo",$_SESSION["language"])."' onclick=\"editphoto('".mysql_result($load_data,$load,1)."','".htmlspecialchars(mysql_result($load_data,$load,2))."','".mysql_result($load_data,$load,7)."','".base64_encode(mysql_result($load_data,$load,0))."');\"  style='cursor:pointer' /> <img src='./images/close.png' border='0' width='12' height='12' alt='".dictionary("delete",$_SESSION["language"])."' onclick=\"if(confirm('".dictionary("del_record",$_SESSION["language"])." :".mysql_result($load_data,$load,1)." ".mysql_result($load_data,$load,2)."')) del_record('".base64_encode(mysql_result($load_data,$load,0))."');\" style='cursor:pointer' /> ".mysql_result($load_data,$load,1)." ".mysql_result($load_data,$load,2)."</span></span>";
echo "</span>";
$load++;endwhile;?>
<input id="act" name="act" type="hidden" value="" disabled='disabled' />
</form></fieldset></td></tr></table>
</div>
<?require_once ("./functions/js/photo_gallery.js");}
 // end of photogallery
?>



<?if (@$_REQUEST["option"]=='page_object') { // page_object
?><div id='bookmark' >

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("page_object",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>


</form></fieldset></td></tr></table>
</div>
<?} // end of css
?>


<?if (@$_REQUEST["option"]=='webmin_addons') { // webmin_addons
?><div id='bookmark' >

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("webmin_addons",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>

<?
$load_data=mysql_query("select * from webmin_addons order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo"<TABLE style='width:100%;height:91%;border:0px;cellpadding:0px;' ><TR ><TD COLSPAN='2' STYLE='WIDTH:20%;' >
<SELECT NAME='addons' STYLE='width:100%;display: inline-block;' SIZE='10' >";
$tbl=0;while(mysql_result($load_data,$tbl,0)):
echo "<OPTION>".dictionary(mysql_result($load_data,0,2),$_SESSION["language"])."</OPTION>";
$tbl++;endwhile;
echo"</SELECT></TD>
<TD ROWSPAN='6' ><FIELDSET ID='ram_single' ><LEGEND>".dictionary("prewiew",$_SESSION["language"])."</LEGEND>
<DIV ID='addon_preview' STYLE='width:100%;height:90%;'  >
</div></FIELDSET>
</TD></TR>
";

echo "
<td style='text-align:left;'>".dictionary("name",$_SESSION["language"])."</td><td>".dictionary(mysql_result($load_data,0,2),$_SESSION["language"])."</td></tr>
<tr><td style='text-align:left;'>".dictionary("params",$_SESSION["language"])."</td><td><input type=text value='".mysql_result($load_data,0,6)."' /></td></tr>
<tr><td colspan=2 style='text-align:left;' >".dictionary("info",$_SESSION["language"])." ".mysql_result($load_data,0,7)."</td></tr>
<tr><td style='text-align:left;'>".dictionary("price",$_SESSION["language"])."</td><td>".mysql_result($load_data,0,8)."</td></tr>
<TR><TD COLSPAN='2' STYLE='height:100%;'></TD></TR>
";

echo "</table>";?>

</form></fieldset></td></tr></table>
</div>
<?} // end of css
?>






<?if (@$_REQUEST["option"]=='css') { // css
?><div id='bookmark' >

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("css",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>
<textarea name='value1' style='width:100%;rows:30;' rows=30 wrap="off"><?echo file_get_contents("./css/website/default.css");?></textarea>
</form></fieldset></td></tr></table>
</div>
<?} // end of css
?>







<?if (@$_REQUEST["option"]=='user_lists') { // user_lists
?><div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:5px;' >
<?$tables = mysql_query("show tables like 'list%' ");
$tbl=0;while(mysql_result($tables,$tbl,0)):
echo "<span id='".mysql_result($tables,$tbl,0)."' style='cursor:pointer;' onclick=load_user_list('".mysql_result($tables,$tbl,0)."'); class='outitems' onmouseout=\"className='outitems';\" onmouseover=\"className='initems';\" title='".dictionary(mysql_result($tables,$tbl,0),$_SESSION["language"])."' />".dictionary(mysql_result($tables,$tbl,0),$_SESSION["language"])." </span>";
$tbl++;endwhile;
?></div>
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("user_lists",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>

<div id='data'></div>

<input id="act" name="act" type="hidden" value="" disabled='disabled' >
</form></fieldset></td></tr></table>
</div>
<?require_once ("./functions/js/user_lists.js");
if (isset($_COOKIE['tbl'])){echo "<script>load_user_list('".$_COOKIE['tbl']."');</script>";}
} // end of user_lists
?>





<?if (@$_REQUEST["option"]=='htaccess') { // htaccess
?><div id='bookmark' >

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("htaccess",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>
<textarea name='value1' style='width:100%;rows:30;' rows=30 wrap="off"><?echo file_get_contents("./.htaccess");?></textarea>
</form></fieldset></td></tr></table>
</div>
<?} // end of htaccess
?>









<?if (@$_REQUEST["option"]=='links') { // links
?><div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:5px;' >
<span style='cursor:pointer;' onclick='addlink();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("add_link",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("add_link",$_SESSION["language"]);?></span>
</div>
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("links",$_SESSION["language"]);?></b></legend>
<?$load_data=mysql_query("select * from www_links order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "<table id=table_desc ><tr><td colspan=2><b>".dictionary("links",$_SESSION["language"])."</b></td></tr>";
$load=0;while(mysql_result($load_data,$load,0)):
	echo "<tr onmouseover=\"className='menuon';\" onmouseout=\"className='menuoff';\" title='".dictionary("edit_link",$_SESSION["language"])."' onclick=\"editlink('".mysql_result($load_data,$load,1)."','".htmlspecialchars(mysql_result($load_data,$load,2))."','".mysql_result($load_data,$load,6)."','".mysql_result($load_data,$load,7)."','".htmlspecialchars(mysql_result($load_data,$load,3))."','".base64_encode(mysql_result($load_data,$load,0))."','".mysql_result($load_data,$load,5)."');stylelinee();\" ><td style='text-align:left;margin:5px;'>";
    if (@mysql_result($load_data,$load,5)){echo "<img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_links")."&id=".base64_encode(mysql_result($load_data,$load,0))."' width='20' height='20' style=position:relative;top:3px;left:3px; > ";}
    else{echo"<img src='./images/noicon.png' width='18' height='18' border='0' style=vertical-align:middle; > ";}
    echo mysql_result($load_data,$load,1)." ".mysql_result($load_data,$load,2)." </td><td style='padding:5px;padding-left:20px;' ><img src='./images/delete.png' width='18' height='18' alt='".dictionary("delete",$_SESSION["language"])."' border='0' style=vertical-align:sup;cursor:pointer;  onclick=\"if(confirm('".dictionary("del_record",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)." ".mysql_result($load_data,$load,2)."')) del_record('".base64_encode(mysql_result($load_data,$load,0))."');\" ></td></tr>";
$load++;endwhile;
echo "</table>";?>
<input id="act" name="act" type="hidden" value="" disabled='disabled' />
</form></fieldset></td></tr></table>
</div>
<?require_once ("./functions/js/links.js");?>
<div id="checkvalue" ></div>
<script>document.getElementById("checkvalue").style.display ='none';</script>
<?} // end of links
?>






<?if (@$_REQUEST["option"]=='js_games') { // JS Games
?><div id='bookmark' >

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("js_games",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input id='savebtn' name='savebtn4' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>
</form></fieldset></td></tr></table>

</div>
<?} // end of JS Games
?>







<?if (@$_REQUEST["option"]=='menu_builder') { // menu_builder
?><div id='bookmark' >

<div style='position:absolute;align:left;left:20px;top:5px;' >
<span id='back' style='cursor:pointer;' onclick='back();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("back",$_SESSION["language"]);?>' ><img src='./images/back.png' width='20' height='20' border='0' style='vertical-align:middle;' > <?echo dictionary("back",$_SESSION["language"]);?></span>
<span id='newmenubutton' style='cursor:pointer;' onclick='newmenuitem();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("new_menu_item",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' border='0' style=vertical-align:middle; > <?echo dictionary("new_menu_item",$_SESSION["language"]);?></span>
<span id='editmenu' onclick='showeditmenu();' style='cursor:pointer;' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("edit_menu_item",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' border='0' style=vertical-align:middle; > <?echo dictionary("edit_menu_item",$_SESSION["language"]);?></span>
<span id='newsubmenu' style='cursor:pointer;' onclick='newsubmenuitem();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("new_submenu_item",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' border='0' style=vertical-align:middle; > <?echo dictionary("new_submenu_item",$_SESSION["language"]);?></span>
<span id='editsubmenu' onclick='showeditsubmenu();' style='cursor:pointer;' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("edit_submenu_item",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' border='0' style=vertical-align:middle; > <?echo dictionary("edit_submenu_item",$_SESSION["language"]);?></span>
<span id='delmenu' style='cursor:pointer;' onclick="if(confirm('<?echo dictionary("del_record",$_SESSION["language"]);?> :'+document.getElementById('v2').value)) del_record(document.getElementById('v100').value);" class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("delele",$_SESSION["language"]);?>' ><img src='./images/delete.png' width='20' height='20' border='0' style=vertical-align:middle; > <?echo dictionary("delete",$_SESSION["language"]);?></span>
</div>


<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("menu_builder",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input id='savebtn' name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>
<span id='menuname' class='itemname' ></span>
<span id='langmenu' class='langmenu' >
<?$load_names=mysql_query("SHOW COLUMNS FROM webmin_dictionary where field like 'lang_%'") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_names)):
 	echo " <img id='lang".$load."' onclick=slct_lang('".mysql_result($load_names,$load,0)."','".$load."','".mysql_num_rows($load_names)."'); src='./ajax_functions.php?icon=yes&tbl=".base64_encode("webmin_languages")."&id=".base64_encode(mysql_result(mysql_query("select id from webmin_languages where name='".securesql(mysql_result($load_names,$load,0))."' "),0,0))."' alt='".dictionary(mysql_result($load_names,$load,0),$_SESSION["language"])."' class=\"outico\" onmouseout=\"className='outico';\" onmouseover=\"className='inico';\" >";
@$load++;endwhile;?>
</span>
<?
$load_data=mysql_query("select * from www_menu where submenu_parent_id='0' order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "<table id=table_desc ><tr style='vertical-align:top;' >";

$load=0;while($load<mysql_num_rows($load_data)):
$load_sub_data=mysql_query("select * from www_menu where submenu_parent_id='".securesql(mysql_result($load_data,$load,0))."' order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

echo "<td><span id=\"mainmenu".mysql_result($load_data,$load,0)."\" class=\"wwwmainmenuout\" onmouseout=\"className='wwwmainmenuout';\" onmouseover=\"className='wwwmainmenuin';\" onclick=\"editmenuitem('".mysql_result($load_data,$load,1)."','".mysql_result($load_data,$load,2)."','".mysql_result($load_data,$load,3)."','".mysql_result($load_data,$load,4)."','".mysql_result($load_data,$load,5)."','".mysql_result($load_data,$load,6)."','".mysql_result($load_data,$load,7)."','".mysql_result($load_data,$load,9)."','".mysql_result($load_data,$load,12)."','".base64_encode(mysql_result($load_data,$load,0))."','".securesql(htmlspecialchars(mysql_result($load_data,$load,14)))."','";if (@mysql_num_rows($load_sub_data)){echo"YES";}else{echo"NO";}echo "','+document.getElementById(sel_lang).value+');\" >";
if (mysql_result($load_data,$load,9)){echo "<a href='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_menu")."&id=".base64_encode(mysql_result($load_data,$load,0))."' rel='shadowbox;width=200;height=200' title='".dictionary("icon",$_SESSION["language"]).": ".mysql_result($load_data,$load,2)."' ><img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_menu")."&id=".base64_encode(mysql_result($load_data,$load,0))."' width='18' height='18' border='0' style=vertical-align:middle; ></a>";}
	else {echo"<img src='./images/noicon.png' width='18' height='18' border='0' style=vertical-align:middle; >";}

 echo "<font style=font-size:10px;>".mysql_result($load_data,$load,1)."</font> ".mysql_result($load_data,$load,2);
if (mysql_result($load_data,$load,6)=="ANO"){echo "<br /><span style=vertical-text-align:bottom;text-align:right;width:100%;font-size:8pt; > <img src='./images/submenu.png' width='16' height='16' border='0' style=vertical-align:top; > <font style=vertical-align:bottom>submenu</font></span>";}
echo"</span>";

// submenu

    $load1=0;while($load1<mysql_num_rows($load_sub_data)):

    echo "<br /><span id=\"submenu".mysql_result($load_sub_data,$load1,0)."\" class=\"wwwsubmenuout\" onmouseout=\"className='wwwsubmenuout';\" onmouseover=\"className='wwwsubmenuin';\" onclick=\"editsubmenuitem('".mysql_result($load_sub_data,$load1,1)."','".mysql_result($load_sub_data,$load1,2)."','".mysql_result($load_sub_data,$load1,3)."','".mysql_result($load_sub_data,$load1,4)."','".mysql_result($load_sub_data,$load1,5)."','".mysql_result($load_sub_data,$load1,6)."','".mysql_result($load_sub_data,$load1,7)."','".mysql_result($load_sub_data,$load1,9)."','".mysql_result($load_sub_data,$load1,12)."','".base64_encode(mysql_result($load_sub_data,$load1,0))."','".securesql(htmlspecialchars(mysql_result($load_sub_data,$load1,14)))."','NO','+document.getElementById(sel_lang).value+');\" >";
    if (mysql_result($load_sub_data,$load1,9)){echo "<a href='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_menu")."&id=".base64_encode(mysql_result($load_sub_data,$load1,0))."' rel='shadowbox;width=200;height=200' title='".dictionary("icon",$_SESSION["language"]).": ".mysql_result($load_sub_data,$load1,2)."' ><img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("www_menu")."&id=".base64_encode(mysql_result($load_sub_data,$load1,0))."' width='18' height='18' border='0' style=vertical-align:middle; ></a>";}
	else {echo"<img src='./images/noicon.png' width='18' height='18' border='0' style=vertical-align:middle; >";}

    echo "<font style=font-size:10px;>".mysql_result($load_sub_data,$load1,1)."</font> ".mysql_result($load_sub_data,$load1,2);
    if (mysql_result($load_sub_data,$load1,6)=="ANO"){echo "<br /><span style=vertical-text-align:bottom;text-align:right;width:100%;font-size:8pt; > <img src='./images/submenu.png' width='16' height='16' border='0' style=vertical-align:top; > <font style=vertical-align:bottom>submenu</font></span>";}
    echo"</span>";

    $load1++;endwhile;
// end submenu

echo"</td>";
$load++;endwhile;?></tr></table>

<div id='editor'>
<?
include_once ('./modules/ckeditor/ckeditor/ckeditor.php');
include_once ('./modules/ckeditor/ckfinder/ckfinder.php');

$ckeditor = new CKEditor();
$ckeditor->basePath = '';
CKFinder::SetupCKEditor($ckeditor, './modules/ckeditor/ckfinder/');
$ckeditor->editor('editor_data');

?></div>
<input id="sel_lang" name="sel_lang" type="hidden" value="" />
<input id="rec_id" name="rec_id" type="hidden" value="" />
<input id="act" name="act" type="hidden" value="" disabled='disabled' />
</form></fieldset></table>
</div>
<?require_once ("./functions/js/web_builder.js");}
 // end of menu_builder
?>








<?if (@$_REQUEST["option"]=='dictionary') { // dictionary
?><div id='bookmark' >
<table style='width:100%;height:100%;border:0px;cellpadding:10px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram' ><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda' ><b><?echo dictionary("dictionary",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' ><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>
<div style='position:absolute;align:left;left:20px;top:5px;cursor:pointer;' onclick='checklanguage();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("language_selection",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' border='0' style=vertical-align:middle; > <?echo dictionary("language_selection",$_SESSION["language"]);?></div>
<?
$load_names=mysql_query("SHOW COLUMNS FROM webmin_dictionary") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load_data=mysql_query("select * from webmin_dictionary order by id ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

echo "<table id=table_desc><tr><td><img src='./images/add.png' width='18' height='18' alt='".dictionary("add_new",$_SESSION["language"])."' border='0' style=cursor:pointer; onclick=\"add_record();\"></td>";

$load=1;while(mysql_result($load_names,$load,0)):
	echo "<td><img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("webmin_languages")."&id=".base64_encode(mysql_result(mysql_query("select id from webmin_languages where name='".securesql(mysql_result($load_names,$load,0))."' "),0,0))."' width='20' height='20' alt='' border='0' style=position:relative;top:3px;right:5px; >".dictionary(mysql_result($load_names,$load,0),$_SESSION["language"])."</td>";
$load++;endwhile;echo "</tr>";

if (@$_REQUEST["act"]=="add"){$plus=1;} else {$plus=0;}
$load=0;while($load<mysql_num_rows($load_data)+$plus):
  echo "<tr><td>";
	$load1=1;while($load1<mysql_num_rows($load_names)):
		if ($load1==1 && $load<mysql_num_rows($load_data)){echo "<img src='./images/delete.png' width='18' height='18' alt='".dictionary("delete",$_SESSION["language"])."' border='0' style=vertical-align:sup;cursor:pointer;  onclick=\"if(confirm('".dictionary("del_record",$_SESSION['language'])." : ".mysql_result($load_data,$load,$load1)."')) del_record('".mysql_result($load_data,$load,0)."');\" >";}
		echo "</td><td><input type='text' id='value_".mysql_result($load_data,$load,0)."_".mysql_result($load_names,$load1,0)."' name='value_".mysql_result($load_data,$load,0)."_".mysql_result($load_names,$load1,0)."' value='".mysql_result($load_data,$load,$load1)."' style=text-align:center; autocomplete='off' onclick=select() ></td>";
	$load1++;endwhile;echo"</tr>";
$load++;endwhile;echo "</table>";?>

<input id="act" name="act" type="hidden" value="" disabled='disabled' >
</form></fieldset></table>
</div>
<?} // end of dictionary
?>









<?if (@$_REQUEST["option"]=='main_settings') { // main_settings
?><div id='bookmark'>
<table style='width:100%;height:100%;border:0px;cellpadding:10px;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form2' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("main_settings",$_SESSION["language"]);?></b></legend>
<div style='position:absolute;align:right;right:20px;top:10px;' align='right'><input name='savebtn' type='submit' value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;' ></div>
<div id='checkvalue' ></div>
<script>document.getElementById("checkvalue").style.display ='none';</script>
<?$load_data=mysql_query("select * from webmin_main_sett where type<>'SYSTEM' order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "<table id=data_table >";
$load=0;while($load<mysql_num_rows($load_data)):

        echo "<tr><td> ";
    if (mysql_result($load_data,$load,7)){echo"<img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("webmin_main_sett")."&id=".base64_encode(mysql_result($load_data,$load,0))."' width='20' height='20' border='0' align=right style=cursor:pointer; onclick=\"if(confirm('".dictionary("del_icon",$_SESSION["language"])."')) del_icon('".base64_encode(mysql_result($load_data,$load,0))."','webmin_main_sett');\" alt='".dictionary("delete",$_SESSION["language"])."' />";}
        
        echo "".dictionary(mysql_result($load_data,$load,2),$_SESSION["language"]).": </td>";
        echo "<td>";
        if (mysql_result($load_data,$load,5)=="TEXT"){
            if (!mysql_result($load_data,$load,4)){echo" <input name='value_".mysql_result($load_data,$load,0)."' type='text' value='".mysql_result($load_data,$load,3)."' style=text-align:center;width:200px; autocomplete='off' ></td>";}
        	   else {echo" <input id='value".($load+1)."' name='value_".mysql_result($load_data,$load,0)."' type='text' value='".mysql_result($load_data,$load,3)."' onkeyup=\"document.getElementById('value".($load+1)."').style.backgroundColor ='#FFFFFF';document.getElementById('checkvalue').style.display ='none';\" style=text-align:center;width:200px; autocomplete='off' ><img src='./images/list.png' width='20' height='20' alt='' border='0' style=vertical-align:top;cursor:pointer; onclick=\"checkvalue('value".($load+1)."','".mysql_result($load_data,$load,0)."','webmin_main_sett','4');\" /></td>";}
        } else { //file type
           echo "<input type=file value='' name='value_".mysql_result($load_data,$load,0)."' style=text-align:center;width:200px; autocomplete='off' />"; 
        }    

$load++;endwhile;echo "</table>";?>
</form></fieldset></table>
</div>
<?} // end of main_settings
?>








</td></tr></table></body>
</html>

<?
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/webmin_scripts.js");
?>


<?}?>