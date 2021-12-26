<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

if (@$_SESSION["lnamed"]) {
if (isset($_REQUEST["desktop_no"])){@$_SESSION["desktop_no"]=@$_REQUEST["desktop_no"];}    
    ?>
<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('user_desktop',$_SESSION['language']);?>";
</script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />

<? //saving

$rooturl=explode("?",$_SERVER["REQUEST_URI"]);
if (@$_REQUEST["formsavebtn1"]){//new program icon saving
if (!@isset($_REQUEST["value3"])){@$_REQUEST["value3"]='RUN';}
    $result=mysql_query("insert into user_desktop (name,command,desktop_no,type,create_date,creator)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".securesql(@$_SESSION["desktop_no"])."','".securesql(@$_REQUEST["value3"])."','".$dnest."','".securesql(@$_SESSION["lnamed"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    file_to_db ('update','file1','user_desktop',iid());
  //  if ($result==1) {savesuccess(@$_REQUEST["value1"]);}
}

if (@$_REQUEST["formsavebtn2"]){//update program icon 
if (!@isset($_REQUEST["value3"])){@$_REQUEST["value3"]='RUN';}
$result=mysql_query("UPDATE user_desktop set name='".securesql(@$_REQUEST["value1"])."', command='".securesql(@$_REQUEST["value2"])."', desktop_no='".securesql(@$_SESSION["desktop_no"])."',type='".securesql(@$_REQUEST["value3"])."' where id= '".securesql(@$_REQUEST["value100"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    if(@$_FILES["file1"]['name']){file_to_db ('update','file1','user_desktop',securesql(@$_REQUEST["value100"]));}
 //   if ($result==1) {savesuccess(@$_REQUEST["value1"]);}
}



if (@$_REQUEST["del"]<>""){//delete icon using by glob function
    glob_del('user_desktop',$_REQUEST["del"],'name');
    unset($_REQUEST["del"]);    
}

if (isset($_REQUEST["fix"])){ //save icon xy position
$result=mysql_query("UPDATE user_desktop SET position_x='".securesql($_REQUEST["fixx"])."', position_y='".securesql($_REQUEST["fixy"])."' where id='".securesql($_REQUEST["fix"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    unset($_REQUEST["fixx"]);unset($_REQUEST["fixy"]);    
}


require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/program_frame_icon_drag.js"); xxxx

// end of saving
?>

</head>


<Body onselectstart="return false;" >
<table id='fullframetable' onselectstart="return false;" >
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<?if (!@$_REQUEST["option"]) { // user desktop
?><div id='bookmark' onselectstart="return false;" >
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:105%;'><td style='width:100%;height:105%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("user_desktop",$_SESSION["language"]);?></b></legend>
<?

$load_data=mysql_query("select * from user_desktop where creator='".securesql($_SESSION["lnamed"])."' and desktop_no='".securesql(@$_SESSION["desktop_no"])."' order by id ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$cycle=0;while(@mysql_result($load_data,$cycle,0)):
    echo "<span ondblclick=\"run_cmd('".htmlspecialchars(str_replace(chr(92),chr(92).chr(92),@mysql_result($load_data,$cycle,2)))."','');\" class=desktop_icon id='id_".$cycle."' title='".mysql_result($load_data,$cycle,1)."'";
    if (@mysql_result($load_data,$cycle,3) && @mysql_result($load_data,$cycle,4) ){echo "style=position:absolute;left:".(mysql_result($load_data,$cycle,3)-2)."px;top:".(mysql_result($load_data,$cycle,4)-2)."px; ";}
    else{echo "style=position:absolute;left:35px;top:55px; ";}
    echo" ><span class=icon_panel ><span style=width:35%;text-align:left;padding-top:1px;padding-right:2px; ><img src='./images/delete.png' width=9px heigth=9px title='".dictionary("delete",$_SESSION["language"])."' onclick=\"if(confirm('".dictionary("del_ico",$_SESSION['language'])." : ".mysql_result($load_data,$cycle,1)."?')) window.location.href('".@$rooturl[0]."?del=".mysql_result($load_data,$cycle,0)."&option=".@$_REQUEST["option"]."');\" ></span><span width=30% ><img src='./images/edit.png' onclick=\"icon_edit('".htmlspecialchars(mysql_result($load_data,$cycle,1))."','".htmlspecialchars(str_replace(chr(92),chr(92).chr(92),(mysql_result($load_data,$cycle,2))))."','".mysql_result($load_data,$cycle,0)."',";if (@mysql_result($load_data,$cycle,7)){echo "'YES'";}else {echo "'NO'";}echo ");\" style=width:9px;height:9px; title='".dictionary("editing",$_SESSION["language"])."'></span><span style=width:35%;text-align:right;padding-top:1px;padding-right:2px; ><img src=";
    if (!@mysql_result($load_data,$cycle,3) && !@mysql_result($load_data,$cycle,4)){echo "'./images/red-tack.png' onclick=fn_mouse_xy('".$cycle."','".mysql_result($load_data,$cycle,0)."'); ";}
    else {echo "'./images/blue-tack.png' onclick=fn_mouse_xy('".$cycle."','".mysql_result($load_data,$cycle,0)."'); ";
    } 
    echo " id='id_".$cycle."_icon' width=9px heigth=9px title='".dictionary("fix_icon",$_SESSION["language"])."' ></span></span>
    <img src='./ajax_functions.php?icon=yes&tbl=".code("user_desktop")."&id=".code(mysql_result($load_data,$cycle,0))."' border='0' width='36' height='36' style=vertical-align:middle;margin-bottom:2px; ><br /><font style=font-size:9px; >".mysql_result($load_data,$cycle,1)."</font></span>";
        
$icons_count=$cycle++;endwhile;

?>
<div style='position:absolute;align:left;left:20px;top:5px;' >
<span id="funct_btn1" style='cursor:pointer;' onclick='new_icon();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("add_icon",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("add_icon",$_SESSION["language"]);?></span>
</div>

<div style='position:absolute;align:left;right:25px;top:3px;' >
<?
$cycle=1;while ($cycle<=@mysql_result(mysql_query("select param from mainsettings where id=500 "),0,0)):
echo "<span id=desktop_no_".$cycle." onclick=select_desktop('".$cycle."')";
    if (@$_SESSION["desktop_no"]==$cycle) { echo ' class="desktop_no_in" disabled=disabled ';}else{ echo ' class="desktop_no_out" ';}
echo " onmouseout=\"className='desktop_no_out';\" onmouseover=\"className='desktop_no_in';\" title='".dictionary("desktop_no",$_SESSION["language"])."' >".$cycle."</span> ";

$cycle++;
endwhile;?>
<input id="desktop_no" type="hidden" name="desktop_no" value="">
</div>


<input type="hidden" id=position name=position value="">

</form></fieldset></td></tr></table>
</div><?
}?>


</td></tr></table></body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/user_desktop_scripts.js");
?>
<script>dragable_icon(<? echo $icons_count;?>);</script>

<?}?>