<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

if (@$_SESSION["lnamed"]) {?>
 <html>
<head>

<SCRIPT LANGUAGE="JavaScript">
parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary("user_settings",$_SESSION['language']);?>";
</script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />

<style>
input[id='lpassword'],
.passwordStrengthBar {
    width: 250px;
}
.passwordStrengthBar div {
    height: 5px;
    width: 0;
}
.passwordStrengthBar div.strong {
    background-color: #32cd32;
}
.passwordStrengthBar div.medium {
    background-color: yellow;
}
.passwordStrengthBar div.weak {
    background-color: orange;
}
.passwordStrengthBar div.useless {
    background-color: red;
}
</style>

<?//saving
        @$rights_type=mysql_query("select * from rights order by position") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());


if ((@$_POST["value1"] or @$_POST["value2"] or (@$_POST["lpassword"] && @$_POST["lpassword"]==@$_POST["lpassword1"])) && $_SESSION["lnamed"] && @$_POST["form"]=="password") {
    if (@$_POST["lpassword"] && @$_POST["lpassword"]==@$_POST["lpassword1"]) {mysql_query("update login set loginpass='".securesql(md5(@$_POST["lpassword"]))."', start_date='".securesql(datedb(@$_POST["value1"]))."', end_date='".securesql(datedb(@$_POST["value2"]))."' where loginname='".securesql(@$_SESSION["lnamed"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
    if (!@$_POST["lpassword"] or @$_POST["lpassword"]<>@$_POST["lpassword1"]) {mysql_query("update login set start_date='".securesql(datedb(@$_POST["value1"]))."', end_date='".securesql(datedb(@$_POST["value2"]))."' where loginname='".securesql(@$_SESSION["lnamed"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
savesuccess("");
}


if (@$_POST["value1"] && @$_POST["value2"] && $_POST["value3"] && @$_POST["form"]=="name" && $_SESSION["lnamed"] ) {
  $result=("update login set name='".securesql(@$_POST["value1"])."',surname='".securesql(@$_POST["value2"])."',language='".securesql(@$_POST["value3"])."',email='".securesql(@$_POST["value4"])."',sysadmin='".securesql(@$_POST["value5"])."' where loginname='".securesql(@$_SESSION["lnamed"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $sel_groups=@$_POST["value6"];$user_groups=",";
    $cykl=0;while ($cykl<mysql_num_rows(mysql_query("select id from task_manager_groups "))):
        if (@$sel_groups[$cykl]){$user_groups.=$sel_groups[$cykl].",";}
    @$cykl++;endwhile;if ($user_groups==","){$user_groups="";}
    mysql_query("update login set groups='".securesql($user_groups)."' where loginname='".securesql(@$_SESSION["lnamed"])."'") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

if ($result=="1"){savesuccess(@$_POST["value1"]." ".@$_POST["value2"]);}
}


if (@$_REQUEST["menurights"] && $_SESSION["lnamed"] && @$_POST["form"]=="rights"){
@$usr_access=mysql_query("select rights from login where loginname='".securesql($_SESSION["lnamed"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$menuright=explode(":+:", @$_REQUEST["menurights"]);
if (!@StrPos (" ".mysql_result($usr_access,0,0), ",*".$menuright[1].":")) {$allrights="";$allrights.=mysql_result($usr_access,0,0);}
 else {$allrights="";@$usr_access=explode(",",mysql_result($usr_access,0,0));$load=1;while(@$usr_access[@$load]):
                        if (!StrPos (" ".@$usr_access[@$load], "*".$menuright[1].":")) {$allrights.=",".$usr_access[$load];}
                $load++;endwhile;$allrights.=",";}

$allrightsnr="";$load=0;while($load<mysql_num_rows($rights_type)):
    if (@$_REQUEST[mysql_result($rights_type,$load,1)]=="on") {$allrightsnr.=mysql_result($rights_type,$load,3);}
$load++;endwhile; if (@$allrightsnr <>"") {$allrights.="*".$menuright[1].":".$allrightsnr.",";}
mysql_query("update login set rights='".securesql($allrights)."' where loginname='".securesql($_SESSION["lnamed"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
}

// end of saving
?>
</head>
<body onselectstart="return false;">
<table id=fullframetable>

<tr style=width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;>
<td style=cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;>


        <?
        echo "<span ";
        if (!@$_REQUEST["option"] or @$_REQUEST["option"]=='userset') {echo " class=\"bookmarkin\" ";}
        if (@$_REQUEST["option"] and @$_REQUEST["option"]<>'userset') {echo" class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./user_settings.php?option=userset')\" ";}
        echo" >".dictionary("user_settings",$_SESSION["language"])."</span>";


        $load_data=mysql_query("select * from units_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,1)) {echo " class=\"bookmarkin\" ";}
        if (!@$_REQUEST["option"] or @$_REQUEST["option"]<>mysql_result($load_data,$load,1)) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./user_settings.php?option=".mysql_result($load_data,$load,1)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,1),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>

<tr style=width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;>
<td style=cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;>


<?if (!@$_REQUEST["option"] or @$_REQUEST["option"]=='userset') { // option userset
?><div id=bookmark>

<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >


<fieldset id=ram>
<form action=./user_settings.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("logon",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn1 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<? //1
$usr_data=mysql_query("select * from login where loginname='".securesql($_SESSION["lnamed"])."' ")  or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$lang_data=mysql_query("show columns from dictionary") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

echo "<tr><td width=40%>".dictionary("user_name",$_SESSION["language"])."</td>";
echo "<td><input type='text' value='".$_SESSION["lnamed"]."' style=width:250px;text-align:center;font-weight:bold; disabled></td></tr>";

echo "<tr><td>".dictionary("password_change",$_SESSION["language"])."</td>";
echo "<td><input id=lpassword name='lpassword' type='text' value='' style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ".access("on/off")." ></td></tr>";

echo "<tr><td>".dictionary("password_authentication",$_SESSION["language"])."</td>";
echo "<td><input id=lpassword1 name='lpassword1' type='password' value='' onkeyup='checkPass(); return false;' style=width:230px;text-align:center;font-weight:bold; ".access("on/off")." ><span id='confirmMessageImg' class='confirmMessage'></span></td></tr>";

echo "<tr><td>".dictionary("start_date",$_SESSION["language"])."</td>";
echo "<td><input id=value1 name=value1 type='text' value='"; if (@mysql_result($usr_data,0,8) && @mysql_result($usr_data,0,8)<>"0000-00-00") {echo datecs(@mysql_result($usr_data,0,8));} else {echo $dnescs;} echo "' style=width:200px;text-align:center;font-weight:bold; readonly=yes ><INPUT TYPE=button ".access("on/off")." VALUE='".dictionary("date",@$_SESSION["language"])."' onClick=\"cpokus=new calendar(form1.value1,'span_value1','cpokus');document.getElementById('savebtn1').disabled=false;\" style=width:50px;>
<span style=position:relative;top:0px;left:0px; ><div style=position:absolute><SPAN ID=\"span_value1\"></div></span></td></tr>";

echo "<tr><td>".dictionary("end_date",$_SESSION["language"])."</td>";
echo "<td><input onkeyup=document.getElementById('savebtn1').disabled=false; id=value2 name=value2 type='text' value='"; if (@mysql_result($usr_data,0,9) && @mysql_result($usr_data,0,9)<>"0000-00-00") {echo datecs(@mysql_result($usr_data,0,9));} echo "' style=width:200px;text-align:center;font-weight:bold; ><INPUT TYPE=button ".access("on/off")." VALUE='".dictionary("date",@$_SESSION["language"])."' onClick=\"cpokus=new calendar(form1.value2,'span_value2','cpokus');document.getElementById('savebtn1').disabled=false;\" style=width:50px;>
<span style=position:relative;top:0px;left:0px; ><div style=position:absolute><SPAN ID=\"span_value2\"></div></span></td></tr>";

echo "<tr><td>".dictionary("account_type",$_SESSION["language"])."</td>";
echo "<td><input type=text disabled style=width:250px;text-align:center;font-weight:bold; autocomplete='off' value='";if (@mysql_result($usr_data,0,15)=='local'){echo dictionary("local",$_SESSION["language"]);} else {echo dictionary("active_directory",$_SESSION["language"]);} echo "' ></td></tr>";

?><input type='hidden' name=form value=password></form></table>

</fieldset>





</td><td rowspan=2>
<fieldset id=ram_big >
<form action=./user_settings.php id=form3 method='post'><legend id=ram_legenda><b><?echo dictionary("access_rights",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn3 disabled type='button' value='<?echo dictionary("saved",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<?$main_rights=mysql_query("select id,name,type from mainmenu where access<>'' order by position") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

?>
<table style='width:100%;height:90%;'><tr><td style='width:50%;'>
<select size=10 name=menurights style='height:60%;width:200px;margin-left:20px;margin-right:50px;vertical-align: middle;' onchange='cleanrights("<?echo mysql_num_rows($rights_type);?>");rightsoptions("<?echo @mysql_num_rows($rights_type);?>","menurights","<?echo @mysql_result($usr_data,0,3);?>");' >
<option disabled ><?echo dictionary('mainmenu',$_SESSION["language"]);?></option>
<?if (@$_SESSION["lnamed"]){
$load=0;while ($load<mysql_num_rows($main_rights)):

  echo "<option value='".mysql_result($main_rights,$load,2).":+:".mysql_result($main_rights,$load,1)."' >".dictionary(mysql_result($main_rights,$load,1),$_SESSION["language"])."</option>";

    $submain_rights=mysql_query("select id,name,type from mainmenuselections where access<>'' and mainmenu_id='".securesql(mysql_result($main_rights,$load,0))."' order by position") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
        $load1=0;while($load1<mysql_num_rows($submain_rights)):
                echo"<option value='".mysql_result($submain_rights,$load1,2).":+:".mysql_result($submain_rights,$load1,1)."' > - ".dictionary(mysql_result($submain_rights,$load1,1),$_SESSION["language"])."</option>";
        $load1++;endwhile;echo "<option disabled></option>";

$load++;endwhile;?>

<option disabled ><?echo dictionary('left_menu',$_SESSION["language"]);?></option>
<?$left_rights=mysql_query("select id,name,type from units_menu where access<>'' order by position") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while ($load<mysql_num_rows($left_rights)):

  echo "<option value='".mysql_result($left_rights,$load,2).":+:".mysql_result($left_rights,$load,1)."' >".dictionary(mysql_result($left_rights,$load,1),$_SESSION["language"])."</option>";

    $subleft_rights=mysql_query("select id,name,type from units_submenu where access<>'' and id_menu='".securesql(mysql_result($left_rights,$load,0))."' order by position") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
        $load1=0;while($load1<mysql_num_rows($subleft_rights)):
                echo"<option value='".mysql_result($subleft_rights,$load1,2).":+:".mysql_result($subleft_rights,$load1,1)."' > - ".dictionary(mysql_result($subleft_rights,$load1,1),$_SESSION["language"])."</option>";
        $load1++;endwhile;if (mysql_result($left_rights,$load,2)<>mysql_result($left_rights,($load+1),2) OR mysql_num_rows($subleft_rights)){echo "<option disabled></option>";}


$load++;endwhile;

echo "<option disabled>".dictionary("system_options",@$_SESSION["language"])."</options>";
echo "<option value='MAIN:+:dbselect'>".dictionary("database_selection",@$_SESSION["language"])."</options>";

}?></select></td>
<td style='width:50%;vertical-align: top;font-size:14px;font-weight:bold;'>
<?
$load=0;while ($load<mysql_num_rows($rights_type)):
    echo "<p id='rights".$load."' name='".mysql_result($rights_type,$load,4)."' disabled ><input name='".mysql_result($rights_type,$load,1)."' id='right".$load."' type='checkbox' ".access("on/off")." onclick=document.getElementById('form3').submit();document.getElementById('savebtn3').style.background='green';document.getElementById('savebtn3').value='".dictionary("saving",$_SESSION["language"])."'; ";
    echo"><span style=margin-left:20px;>".dictionary(mysql_result($rights_type,$load,1),@$_SESSION["language"])."</span></p>";
$load++;endwhile;
?></td></tr>
<input type='hidden' name=form value=rights></form></table>
</fieldset></td>
</tr><tr style=width:100%;height:50%;><td>




<fieldset id=ram><form action=./user_settings.php id=form2 method='post'><legend id=ram_legenda><b><?echo dictionary("detailed_settings",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn2 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form2').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?  //2
echo "<tr><td width=40%>".dictionary("name",$_SESSION["language"])."</td>";
echo "<td><input id=value1 name=value1 type='text' value='".@mysql_result($usr_data,0,5)."' onkeyup='checkfield(); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; onClick=select(); autocomplete='off' ></td></tr>";

echo "<tr><td>".dictionary("surname",$_SESSION["language"])."</td>";
echo "<td><input id=value2 name=value2 type='text' value='".@mysql_result($usr_data,0,6)."' onkeyup='checkfield(); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; onClick=select(); autocomplete='off' ></td></tr>";

echo "<tr><td>".dictionary("language",$_SESSION["language"])."</td>";

echo"<td><select size=1 name='value3' style='width:250px;' onchange='checkfield(); return false;' ".access("on/off")." >";
$load=2;while($load<(mysql_num_rows($lang_data)-1)):
echo"<option style=text-align:center;";if (@mysql_result($usr_data,0,7)==@mysql_result($lang_data,$load,0)) {echo "background:#D6FEA9; selected=selected ";}echo " value='".mysql_result($lang_data,$load,0)."' >".dictionary(mysql_result($lang_data,$load,0),$_SESSION["language"])."</option>";
$load++;endwhile;echo"</select></td></tr>";

echo "<tr><td width=40%>".dictionary("email",$_SESSION["language"])."</td>";
echo "<td><input id=value4 name=value4 type='text' value='".@mysql_result($usr_data,0,11)."' onkeyup='checkfield(); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; onClick=select(); autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("sysadmin",$_SESSION["language"])."</td>";
echo "<td><select size=1 name='value5' style='width:250px;' onchange='checkfield(); return false;' ".access("on/off")." >";
	IF (@mysql_result($usr_data,0,10)=="Yes"){echo"<option value='Yes'>".dictionary("Yes",@$_SESSION["language"])."</option><option value='No'>".dictionary("No",@$_SESSION["language"])."</option>";}
	ELSE {echo"<option value='No'>".dictionary("No",@$_SESSION["language"])."</option><option value='Yes'>".dictionary("Yes",@$_SESSION["language"])."</option>";}
echo "</select></td></tr>";

echo "<tr><td>".dictionary("in_groups",$_SESSION["language"])."</td>";
$load_data=mysql_query("select * from task_manager_groups ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "<td><select name=value6[] style='width:250px;' multiple=multiple onchange='checkfield(); return false;' ".access("on/off")."  size=5>";
$cykl=0;while(@mysql_result($load_data,$cykl,0)):
echo "<option value='".mysql_result($load_data,$cykl,1)."'";
        if (strpos(" ".@mysql_result($usr_data,0,13),','.mysql_result($load_data,$cykl,1).',')) {echo " selected=selected ";}
echo " >".mysql_result($load_data,$cykl,1)."</option>";
$cykl++;endwhile;
echo"</select></td></tr>"; 



?>
<input type='hidden' name=form value=name></form></table></fieldset>
</td></tr></table>
</div>
<?} // end of option userset






if (@$_REQUEST["option"]=='system_configuration') { // system configuration
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
</td></tr></table>
</div>

<?} // system configuration



if (@$_REQUEST["option"]=='hotline') { // hotline
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
</td></tr></table>
</div>

<?} // end of hotline


if (@$_REQUEST["option"]=='task_manager') { // task_manager
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
</td></tr></table>
</div>

<?} // end of task_manager

if (@$_REQUEST["option"]=='xls_reporting') { // task_manager
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
</td></tr></table>
</div>

<?} // end of task_manager

?>
</td></tr></table></body>
</html>

<?
require_once("./functions/js/jquery.min.js");
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/user_settings.js");
?>


<script>setselected('menurights','<?echo @$_REQUEST["menurights"];?>');<?echo"rightsoptions('".mysql_num_rows($rights_type)."','menurights','<?echo $allrights;?>');";?></script>

<?}?>