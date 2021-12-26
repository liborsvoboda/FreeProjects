<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
if (@$_SESSION["lnamed"]) {?>
 <html>
<head>

<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('global_config',$_SESSION['language']);?>";
</script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />


<?//saving
$rooturl=explode("?",$_SERVER["REQUEST_URI"]);

if ( @$_POST["option"]=="global_config" ){   //mainsettings
@$save=1; while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where id<100 "),0,0)+1)):
$result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
  $save++;endwhile;
if ($result==1) {savesuccess("");}
}

if ( @$_POST["option"]=="xls_reporting" ){   //xls_reporting

@$sqlcount = @$save = 100;if (@$_POST["value".$save]<>""){ while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > '".(@$sqlcount-1)."' and id < '".(@$sqlcount+100)."' ) "),0,0)+@$sqlcount)):
  $result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;}

@$sqlcount = @$save = 300;if (@$_POST["value".$save]){ while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > '".(@$sqlcount-1)."' and id < '".(@$sqlcount+100)."' ) "),0,0)+@$sqlcount)):
    $result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;}

@$sqlcount = @$save = 700;if (@$_POST["value".$save]){ while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > '".(@$sqlcount-1)."' and id < '".(@$sqlcount+100)."' ) "),0,0)+@$sqlcount)):
    $result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;}

if ($result==1) {savesuccess("");}  
}




if ( @$_POST["option"]=="machine_tools" ){   //machine_tools

@$sqlcount = @$save = 600;if (@$_POST["value".$save]<>""){ while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > '".(@$sqlcount-1)."' and id < '".(@$sqlcount+100)."' ) "),0,0)+@$sqlcount)):
  $result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;}

if ($result==1) {savesuccess("");}  
}






if ( @$_POST["option"]=="user_desktop" ){  //user_desktop
@$sqlcount = @$save = 500;if (@$_POST["value".$save]<>""){ while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > '".(@$sqlcount-1)."' and id < '".(@$sqlcount+500)."' ) "),0,0)+@$sqlcount)):
  $result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;}
if ($result==1) {savesuccess("");}  
}


if ( @$_POST["option"]=="task_manager" && @$_REQUEST["savebtn5"]){   // save task_manager groups
echo"<script>".alert("hi")."</script>";
}






if ( @$_POST["option"]=="hotline" && @$_REQUEST["edit"]==""){   // new hotline doc line
    if (@$_POST["value1a"]){$result=mysql_query("insert into hotline_marking (prefix,value,start_date,end_date,create_date,creator)VALUES('".securesql(@$_POST["value1a"])."','".securesql(@$_POST["value1b"])."','".securesql(datedb(@$_POST["value1c"]))."','".securesql(datedb(@$_POST["value1d"]))."','".securesql($dnest)."','".securesql($_SESSION['loginname'])."')") or Die(MySQL_Error());}
@$save=200; while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > 199 and id < '300' ) "),0,0)+201)):

 if ($save<>204) {$result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
 
    if ($save==204) {  // hotline workers
    $workers=@$_POST["value".$save];
    mysql_query("update login set hotline_worker='N'") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $cykl=0;while ($cykl<mysql_num_rows(mysql_query("select id from login"))):
        mysql_query("update login set hotline_worker='Y' where loginname='".securesql(@$workers[$cykl])."'") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    @$cykl++;
    endwhile;
    }
  $save++;endwhile;
  
if ($result==1) {savesuccess(@$_REQUEST["value1"]);}
}
 
 
if ( @$_REQUEST["option"]=="hotline" && @$_REQUEST["del"]) { //delete hotline doc line
$rec=mysql_result(mysql_query("select CONCAT(prefix,' ',value) from hotline_marking where id='".@$_REQUEST["del"]."' "),0,0);
$result=mysql_query("delete from hotline_marking where id='".securesql(@$_REQUEST["del"])."' ");
if ($result==1) {deletesuccess($rec);}
}

if ( @$_POST["option"]=="hotline" && @$_POST["edit"]<>"" ){   // edit hotline doc line
$result=mysql_query("update hotline_marking set prefix = '".securesql(@$_POST["value1a"])."',value = '".securesql(@$_POST["value1b"])."',start_date = '".securesql(datedb(@$_POST["value1c"]))."',end_date = '".securesql(datedb(@$_POST["value1d"]))."',edit_date = '".securesql($dnest)."',edit_name = '".securesql($_SESSION['loginname'])."' where id= '".@$_POST["edit"]."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {savechangesuccess(@$_REQUEST["value1a"].@$_REQUEST["value1b"]);}

}



if ( @$_POST["option"]=="task_manager" && @$_REQUEST["edit"]==""){   // new task_manager doc line
    if (@$_POST["value1a"]){$result=mysql_query("insert into task_manager_marking (prefix,value,start_date,end_date,create_date,creator)VALUES('".securesql(@$_POST["value1a"])."','".securesql(@$_POST["value1b"])."','".securesql(datedb(@$_POST["value1c"]))."','".securesql(datedb(@$_POST["value1d"]))."','".securesql($dnest)."','".securesql($_SESSION['loginname'])."')") or Die(MySQL_Error());}
@$save=400; while(@$save<(mysql_result(mysql_query("select COUNT(id) from mainsettings where (id > 399 and id < '500' ) "),0,0)+401)):
if (@$save<>404) {$result=mysql_query("update mainsettings set param='".securesql(@$_POST["value".$save])."' where id='".securesql($save)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}

    if ($save==404) {  // hotline workers
    $t_m_admins=@$_POST["value".$save];
    mysql_query("update login set task_manager_admin='N'") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $cykl=0;while ($cykl<mysql_num_rows(mysql_query("select id from login"))):
        mysql_query("update login set task_manager_admin='Y' where loginname='".securesql(@$t_m_admins[$cykl])."'") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    @$cykl++;
    endwhile;
    }

  $save++;endwhile;
if ($result==1) {savesuccess(@$_REQUEST["value1"]);}
}
 
if ( @$_REQUEST["option"]=="task_manager" && @$_REQUEST["del"]) { //delete task_manager doc line
$rec=mysql_result(mysql_query("select CONCAT(prefix,' ',value) from task_manager_marking where id='".@$_REQUEST["del"]."' "),0,0);
$result=mysql_query("delete from task_manager_marking where id='".securesql(@$_REQUEST["del"])."' ");
if ($result==1) {deletesuccess($rec);}
}

if ( @$_POST["option"]=="task_manager"  && @$_POST["edit"]<>"" ){   // edit task_manager doc line
$result=mysql_query("update task_manager_marking set prefix = '".securesql(@$_POST["value1a"])."',value = '".securesql(@$_POST["value1b"])."',start_date = '".securesql(datedb(@$_POST["value1c"]))."',end_date = '".securesql(datedb(@$_POST["value1d"]))."',edit_date = '".securesql($dnest)."',edit_name = '".securesql($_SESSION['loginname'])."' where id= '".@$_POST["edit"]."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if ($result==1) {savechangesuccess(@$_REQUEST["value1a"].@$_REQUEST["value1b"]);}

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
        if (!@$_REQUEST["option"] or @$_REQUEST["option"]=='global_config') {echo " class=\"bookmarkin\" ";}
        if (@$_REQUEST["option"] and @$_REQUEST["option"]<>'global_config') {echo" class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./glob_conf.php?option=global_config')\" ";}
        echo" >".dictionary("global_config",$_SESSION["language"])."</span>";

$load_data=mysql_query("select * from units_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,1)) {echo " class=\"bookmarkin\" ";}
        if (!@$_REQUEST["option"] or @$_REQUEST["option"]<>mysql_result($load_data,$load,1)) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./glob_conf.php?option=".mysql_result($load_data,$load,1)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,1),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>

<tr style=width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;>
<td style=cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;>






<?$loaddata=mysql_query("select * from mainsettings order by id") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());


if (!@$_REQUEST["option"] or @$_REQUEST["option"]=='global_config') { // option glob_conf
?><div id=bookmark>

    <div style='position:absolute;align:left;left:20px;top:5px;' >
    <span id="funct_btn1" <?if (!@mysql_result(mysql_query("select param from mainsettings where id=10 "),0,0)) {echo " disabled=disabled ";}?> 
     style='cursor:pointer;' onclick='load_AD_user();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("add_ad_user",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("add_ad_user",$_SESSION["language"]);?></span>
    </div>


<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("app_setting",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn1 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?  //1
echo "<tr><td width=40%>".dictionary("app_name",$_SESSION["language"]).":</td>";
echo "<td><input id=value1 name=value1 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=1 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value1\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("app_url",$_SESSION["language"]).":</td>";
echo "<td><input id=value2 name=value2 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=2 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value2\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("mainmenu_show_time",$_SESSION["language"]).":</td>";
echo "<td><input id=value3 name=value3 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=3 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value3\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ><font size=2pt>(msec.)</font></td></tr>";

echo "<tr><td width=40%>".dictionary("mail_server",$_SESSION["language"]).":</td>";
echo "<td><input id=value4 name=value4 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=4 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value4\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("full_ad_server_dns_name",$_SESSION["language"]).":</td>";
echo "<td><input id=value8 name=value8 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=8 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value8\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("ldap_port",$_SESSION["language"]).":</td>";
echo "<td><input id=value9 name=value9 type='text' value='";if (@mysql_result(mysql_query("select param from mainsettings where id=9 "),0,0)) {echo @mysql_result(mysql_query("select param from mainsettings where id=9 "),0,0);} else{echo "389";} echo "' onkeyup='checkcolorfield(\"form1\",\"value9\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("full_sys_ad_account",$_SESSION["language"]).":</td>";
echo "<td><input id=value10 name=value10 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=10 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value10\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("ldap_account_password",$_SESSION["language"]).":</td>";
echo "<td><input id=value11 name=value11 type='password' value='".@mysql_result(mysql_query("select param from mainsettings where id=11 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value11\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("ldap_ou/cn_folder",$_SESSION["language"]).":</td>";
echo "<td><input id=value12 name=value12 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=12 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value12\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

?>
</table>
</fieldset>



</td><td rowspan=2>
<fieldset id=ram_big >
<legend id=ram_legenda><b><?echo dictionary("specific_settings",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn2 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?
echo "<tr><td width=40%>".dictionary("ocr_app",$_SESSION["language"]).":</td>";
echo "<td><select id=value5 name=value5 onchange='checkfield(\"form1\",\"savebtn2\",\"savebtn2\"); return false;' ".access("on/off")." size=1 style=width:250px;text-align:center;font-weight:bold; >";
$load_data=mysql_result(mysql_query("select variables from mainsettings where id=5 "),0,0);
$load_data = explode(",",$load_data);
$cykl=0;while(@$load_data[$cykl] or @$load_data[($cykl+1)]):
    echo "<option value='".$load_data[$cykl]."' "; 
    if (@mysql_result(mysql_query("select param from mainsettings where id=5 "),0,0)==$load_data[$cykl]) { echo " selected=selected ";}
    echo " >".$load_data[$cykl]."</option>";
$cykl++;
endwhile;echo "</select></td></tr>"; 

echo "<tr><td width=40%>".dictionary("language",$_SESSION["language"])."</td>";
echo "<td><select id=value6 name=value6 onchange='checkfield(\"form1\",\"savebtn2\",\"savebtn2\"); return false;' ".access("on/off")." size=1 style=width:250px;text-align:center;font-weight:bold; >";
$load_data=mysql_result(mysql_query("select variables from mainsettings where id=6 "),0,0);
$load_data = explode(",",$load_data);
$cykl=0;while(@$load_data[$cykl] or @$load_data[($cykl+1)]):
    echo "<option value='".$load_data[$cykl]."' "; 
    if (@mysql_result(mysql_query("select param from mainsettings where id=6 "),0,0)==$load_data[$cykl]) { echo " selected=selected ";}
    echo " >".$load_data[$cykl]."</option>";
$cykl++;
endwhile;echo "</select></td></tr>"; 

echo "<tr><td width=40%>".dictionary("output_file",$_SESSION["language"]).":</td>";
echo "<td><input id=value7 name=value7 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=7 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value7\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

if (@mysql_result(mysql_query("select param from mainsettings where id=5 "),0,0)){
    echo "<tr><td colspan=2 style=text-align:right><input type=button value='".dictionary("ocr_test",$_SESSION["language"])."' onclick='ocr_check();' /></td></tr>";    
}
?>
</table>

</fieldset>


</td></tr><tr style=width:100%;height:50%;><td>

<fieldset id=ram>
<legend id=ram_legenda><b><?echo dictionary("backup_settings",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn3 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("add_new_task",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;padding:0px;'></div>
<table id=data_table>
<?  //1
echo "<tr><td width=40%>".dictionary("server",$_SESSION["language"]).":</td>";
echo "<td><input id=value1008 name=value1008 type='text' value='' onkeyup='checkfield(\"form1\",\"value1008\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("username",$_SESSION["language"]).":</td>";
echo "<td><input id=value1009 name=value1009 type='text' value='' onkeyup='checkfield(\"form1\",\"value1009\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("password",$_SESSION["language"]).":</td>";
echo "<td><input id=value1010 name=value1010 type='text' value='' onkeyup='checkfield(\"form1\",\"value1010\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td width=40%>".dictionary("database",$_SESSION["language"]).":</td>";
echo "<td><input id=value1011 name=value1011 type='text' value='' onkeyup='checkfield(\"form1\",\"value1011\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

echo "<tr><td>".dictionary("start_date",$_SESSION["language"])."</td>";
echo "<td><input id=value1012 name=value1012 type='text' value='' style=width:200px;text-align:center;font-weight:bold; readonly=yes ><INPUT TYPE=button ".access("on/off")." VALUE='".dictionary("date",@$_SESSION["language"])."' onClick=\"cpokus=new calendar(form1.value1012,'span_value12','cpokus');document.getElementById('savebtn3').disabled=false;\" style=width:50px;>
<span style=position:relative;top:0px;left:0px; ><div style=position:absolute><SPAN ID=\"span_value1012\"></div></span></td></tr>";

echo "<tr><td>".dictionary("end_date",$_SESSION["language"])."</td>";
echo "<td><input id=value1013 name=value1013 type='text' value='' style=width:200px;text-align:center;font-weight:bold; readonly=yes ><INPUT TYPE=button ".access("on/off")." VALUE='".dictionary("date",@$_SESSION["language"])."' onClick=\"cpokus=new calendar(form1.value1013,'span_value13','cpokus');document.getElementById('savebtn3').disabled=false;\" style=width:50px;>
<span style=position:relative;top:0px;left:0px; ><div style=position:absolute><SPAN ID=\"span_value1013\"></div></span></td></tr>";

echo "<tr><td width=40%>".dictionary("start_time",$_SESSION["language"]).":</td>";
echo "<td style=text-align:left ><select style=width:50px >";
    $load=0;while($load<24):
    echo "<option value='"; if (strlen($load)==1){echo "0".$load;} else {echo $load;} echo"'>"; if (strlen($load)==1){echo "0".$load;} else {echo $load;} echo"</option>";
    $load++;endwhile;
echo"</select>";
echo "<select style=width:50px>";
    $load=0;while($load<60):
    echo "<option value='"; if (strlen($load)==1){echo "0".$load;} else {echo $load;} echo"'>"; if (strlen($load)==1){echo "0".$load;} else {echo $load;} echo"</option>";
    $load++;endwhile;
echo"</select><input type='button' value='".dictionary("clean_form",$_SESSION["language"])."' onClick='reset();document.getElementById(\"savebtn3\").disabled=true;' style=width:150px /></td></tr>";


?>
<input type='hidden' name=option value="global_config" >
</form></table>

<?
echo "<p></p>
<table id=view_table >
<tr><td>a</td><td>a</td><td>a</td><td>a</td><td>a</td></tr>
<tr><td>a</td><td>a</td><td>a</td><td>a</td><td>a</td></tr>
<tr><td>a</td><td>a</td><td>a</td><td>a</td><td>a</td></tr>
<tr><td>a</td><td>a</td><td>a</td><td>a</td><td>a</td></tr>
</table>";
?>

</fieldset>
</td></tr></table>
</div>
<?} // end of glob_conf



if (@$_REQUEST["option"]=='app_setting') { // app_setting
?><div id=bookmark>app_setting</div>
<?} // end of app_setting








if (@$_REQUEST["option"]=='xls_reporting') { // nastaveni reportu
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;>


<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("epdm_report_form",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn1 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?
 echo "<tr><td width=40%>".dictionary("app_mail_address",$_SESSION["language"]).":</td>";
 echo "<td><input id=value100 name=value100 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=100 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value100\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 echo "<tr><td width=40%>".dictionary("receipt_mail_addresses",$_SESSION["language"]).":</td>";
 echo "<td><input id=value101 name=value101 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=101 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value101\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 echo "<tr><td width=40%>".dictionary("report_file_name",$_SESSION["language"]).":</td>";
 echo "<td><input id=value102 name=value102 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=102 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value102\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 echo "<tr><td width=40%>".dictionary("source_file_name",$_SESSION["language"]).":</td>";
 echo "<td><input id=value103 name=value103 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=103 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value103\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

?>    
</table>
<input type='hidden' name=option value="xls_reporting" >
</form></fieldset></td>


<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form2 method='post'><legend id=ram_legenda><b><?echo dictionary("printer_report_form",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn2 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form2').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?
 echo "<tr><td width=40%>".dictionary("source_file_name",$_SESSION["language"]).":</td>";
 echo "<td><input id=value300 name=value300 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=300 "),0,0)."' onkeyup='checkcolorfield(\"form2\",\"value300\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 echo "<tr><td width=40%>".dictionary("report_file_name",$_SESSION["language"]).":</td>";
 echo "<td><input id=value301 name=value301 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=301 "),0,0)."' onkeyup='checkcolorfield(\"form2\",\"value301\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";

?>    
</table>
<input type='hidden' name=option value="xls_reporting" >
</form></fieldset></td>
</tr>


<tr style=width:100%;height:50%;>
<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form3 method='post'><legend id=ram_legenda><b><?echo dictionary("epdm_metadata_report",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn3 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form3').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

    <table id=data_table>
<?    
 echo "<tr><td width=40%>".dictionary("source_file_name",$_SESSION["language"]).":</td>";
 echo "<td><input id=value700 name=value700 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=700 "),0,0)."' onkeyup='checkcolorfield(\"form3\",\"value700\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 echo "<tr><td width=40%>".dictionary("scheduled_task_command",$_SESSION["language"]).":</td>";
 echo "<td><input id=value701 name=value701 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=701 "),0,0)."' onkeyup='checkcolorfield(\"form3\",\"value701\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
?> 
    </table>
    
<input type='hidden' name=option value="xls_reporting" >
</form></fieldset></td>

<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form4 method='post'><legend id=ram_legenda><b><?echo dictionary("epdm_report_form",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn4 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form4').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

    <table id=data_table>
    <tr><td></td></tr>
    </table>
    
<input type='hidden' name=option value="xls_reporting" >
</form></fieldset></td>

</tr>

</table>
</div>
<?} // end of xls reports









if (@$_REQUEST["option"]=='machine_tools') { // nastaveni reportu
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;>
<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("truestore",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn1 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?
 echo "<tr><td width=40%>".dictionary("file_access",$_SESSION["language"]).":</td>";
 echo "<td><input id=value600 name=value600 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=600 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value600\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 echo "<tr><td width=40%>".dictionary("material_prefix",$_SESSION["language"]).":</td>";
 echo "<td><input id=value601 name=value601 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=601 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value601\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
 
 

?>    
</table>
<input type='hidden' name=option value="machine_tools" >
</form></fieldset></td>


<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form2 method='post'><legend id=ram_legenda><b><?echo dictionary("",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn2 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form2').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

<input type='hidden' name=option value="machine_tools" >
</form></fieldset></td>
</tr>


<tr style=width:100%;height:50%;>
<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form3 method='post'><legend id=ram_legenda><b><?echo dictionary("",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn3 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form3').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

    <table id=data_table>
    <tr><td></td></tr>
    </table>
    
<input type='hidden' name=option value="machine_tools" >
</form></fieldset></td>

<td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form4 method='post'><legend id=ram_legenda><b><?echo dictionary("",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn4 disabled type='button' onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form4').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

    <table id=data_table>
    <tr><td></td></tr>
    </table>
    
<input type='hidden' name=option value="machine_tools" >
</form></fieldset></td>

</tr>

</table>
</div>
<?} // end of xls reports





if (@$_REQUEST["option"]=='user_desktop') { // nastaveni plochy
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;>


<td style=width:100%;height:100%; >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("app_setting",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn1 type='button' disabled onclick="if(confirm('<?echo dictionary("save_changes?",$_SESSION['language']);?>')) document.getElementById('form1').submit();"  value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<table id=data_table>
<?
 echo "<tr><td width=40%>".dictionary("desktop_count",$_SESSION["language"]).":</td>";
 echo "<td><select id=value500 name=value500 onchange='checkfield(\"form1\",\"value500\",\"savebtn1\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; >";
  $cycle=1;while ($cycle<=10):
    echo "<option";
        if ($cycle==@mysql_result(mysql_query("select param from mainsettings where id=500 "),0,0)) {echo " selected=selected ";}
    echo ">".$cycle."</option>"; 
 $cycle++;endwhile;
 echo "</td></tr>";
?>    
</table>
<input type='hidden' name=option value="user_desktop" >
</form></fieldset></td>


</table>
</div>
<?} // end of nastaveni plochy


if (@$_REQUEST["option"]=='hotline') { // nastaveni HOTLINE
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("app_setting",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn3 disabled type='button' <?echo "onclick=\"if(confirm('";if (!@$_GET["edit"]){echo dictionary("save?",$_SESSION['language']);} else {echo dictionary("save_changes?",$_SESSION['language']);}echo "')) document.getElementById('form1').submit();\"";?> value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

<table id=data_table><tr><?
echo "<tr><td width=40%>".dictionary("hotline_statuses",$_SESSION["language"]).":</td>";
echo "<td><input id=value200 name=value200 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=200 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value200\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40%>".dictionary("hotline_priorities",$_SESSION["language"]).":</td>";
echo "<td><input id=value201 name=value201 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=201 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value201\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40%>".dictionary("app_mail_address",$_SESSION["language"]).":</td>";
echo "<td><input id=value202 name=value202 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=202 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value202\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40%>".dictionary("score",$_SESSION["language"]).":</td>";
echo "<td><input id=value203 name=value203 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=203 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value203\",\"savebtn3\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40% style=vertical-align:top; >".dictionary("department_workers",$_SESSION["language"]).":</td>";
$workers=mysql_query("select loginname,CONCAT(surname,' ',name),hotline_worker from login order by loginname") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "<td><select onchange='checkcolorfield(\"form1\",\"value204\",\"savebtn3\"); return false;' ".access("on/off")." id=value204 size=5 multiple=multiple name=value204[] style=width:250px;text-align:center;font-weight:bold; >";
$cykl=0;while(mysql_result($workers,$cykl,0)):
    echo "<option value='".mysql_result($workers,$cykl,0)."'";
if (mysql_result($workers,$cykl,2)=="Y"){echo " selected=selected ";}    
    echo ">".mysql_result($workers,$cykl,1)."</option>";
$cykl++;endwhile;
echo"</select></td></tr>";

?></tr></table></td>


<td style="width:50%;height: 70%;">
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("groups",$_SESSION["language"]);?></b></legend>
</fieldset>
</td></tr>



<tr><td colspan="2">
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("document_lines",$_SESSION["language"]);?></b></legend><table id=table_desc width=100% >
<tr id=options >
<td colspan=2><?if (!@$_REQUEST["edit"]){echo dictionary("new_hotline_marking",$_SESSION['language']);} else{echo dictionary("edit_hotline_marking",$_SESSION['language']);}?></td>
<td><?echo dictionary("start_date",$_SESSION['language']);?></td>
<td><?echo dictionary("end_date",$_SESSION['language']);?></td>
</tr>

<?
if (@$_GET["edit"]){echo "<input name=edit type=hidden value='".@$_GET["edit"]."' >";
	$data1=mysql_query("select * from hotline_marking where id='".securesql(@$_REQUEST["edit"])."' ") or Die(MySQL_Error());}

echo"
<tr height=25px >
<td nowrap=\"nowrap\" colspan=2><input name=\"value1a\" type=\"text\" value='".@mysql_result($data1,0,1)."' size=25 style=text-align:center; autocomplete=off onkeyup='checkcolorfield(\"form1\",\"value1a\",\"savebtn3\"); return false;' ".access("on/off")." >
<input name=\"value1b\" type=\"text\" value='";if (@$_REQUEST["edit"]){echo @mysql_result($data1,0,2);} else {echo "0";}
echo "' style=width:80px;text-align:center; autocomplete=off onkeyup='checkcolorfield(\"form1\",\"value1b\",\"savebtn2\"); return false;' ".access("on/off")." ></td>
<td nowrap=\"nowrap\"><input type=\"text\" name=\"value1c\" value='".datecs(@mysql_result($data1,0,3))."' readonly=yes style=width:120px;height:23px;text-align:center; onkeyup='checkcolorfield(\"form1\",\"value1c\",\"savebtn3\"); return false;' ".access("on/off")." ><INPUT TYPE=\"button\" VALUE=\"Datum\" onClick=\"cpokus=new calendar(form.value1c,'span_value1c','cpokus');document.getElementById('savebtn2').disabled=false;\" style=width:80px; ><div style=position:relative;top:0px;left:-68px; ><div style=position:absolute><SPAN ID=\"span_value1c\"></div></td>
<td nowrap=\"nowrap\"><input type=\"text\" name=\"value1d\" value='".datecs(@mysql_result($data1,0,4))."' readonly=yes style=width:120px;height:23px;text-align:center; onkeyup='checkcolorfield(\"form1\",\"value1d\",\"savebtn3\"); return false;' ".access("on/off")." ><INPUT TYPE=\"button\" VALUE=\"Datum\" onClick=\"cpokus=new calendar(form.value1d,'span_value1d','cpokus');document.getElementById('savebtn2').disabled=false;\" style=width:80px; ><div style=position:relative;top:0px;left:-68px; ><div style=position:absolute><SPAN ID=\"span_value1d\"></div></td>
</tr>";

echo "<tr>
<td colspan=4 style=text-align:center><br />".dictionary("exist_hotline_markinging",$_SESSION['language'])."</td></tr>
<tr id=options><td>".dictionary("options",$_SESSION['language'])."</td>
<td>".dictionary("exist_hotline_doc_lines",$_SESSION['language'])."</td>
<td>".dictionary("start_date",$_SESSION['language'])."</td>
<td>".dictionary("end_date",$_SESSION['language'])."</td>
</tr>";
$load_data=mysql_query("select * from hotline_marking order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while(@mysql_result($load_data,$load,0)):
echo "<tr class=\"viewoff\" onmouseout=\"className='viewoff';\" onmouseover=\"className='viewon';\">


<td>
<img src='./images/edit.png' border='0' width='24' height='24' alt='".dictionary("editing",$_SESSION["language"])."' style='cursor:pointer'
 onclick=\"if(confirm('".dictionary("edit_document_line",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) window.location.href('".@$rooturl[0]."?edit=".mysql_result($load_data,$load,0)."&option=".@$_REQUEST["option"]."');\"

/>

<img src='./images/delete.png' border='0' width='24' height='24' alt='".dictionary("deleting",$_SESSION["language"])."' style='cursor:pointer' onclick=\"if(confirm('".dictionary("del_doc_line",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) window.location.href('".@$rooturl[0]."?del=".mysql_result($load_data,$load,0)."&option=".@$_REQUEST["option"]."');\" />


</td>



<td>".@mysql_result($load_data,$load,1).@mysql_result($load_data,$load,2)."</td>
<td>".datecs(@mysql_result($load_data,$load,3))."</td>
<td>".datecs(@mysql_result($load_data,$load,4))."</td>
</tr>";

$load++;
endwhile;?>

<input type='hidden' name=option value='<?echo @$_REQUEST["option"];?>' >
</form></td></tr></table>
</td>
</fieldset>
<?}?>





<?if (@$_REQUEST["option"]=='task_manager') { // nastaveni task_manager
?><div id=bookmark>
<table style=width:100%;height:100%;border:0px;cellpadding:10px; >
<tr style=width:100%;height:50%;><td style=width:50%;height:50%; >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("app_setting",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn2 name=savebtn2 disabled type='button' <?echo "onclick=\"if(confirm('";if (!@$_GET["edit"]){echo dictionary("save?",$_SESSION['language']);} else {echo dictionary("save_changes?",$_SESSION['language']);}echo "')) document.getElementById('form1').submit();\"";?> value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

<table id=data_table><tr><?
echo "<tr><td width=40%>".dictionary("task_manager_statuses",$_SESSION["language"]).":</td>";
echo "<td><input id=value400 name=value400 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=400 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value400\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40%>".dictionary("task_manager_priorities",$_SESSION["language"]).":</td>";
echo "<td><input id=value401 name=value401 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=401 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value401\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40%>".dictionary("app_mail_address",$_SESSION["language"]).":</td>";
echo "<td><input id=value402 name=value402 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=402 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value402\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
echo "<tr><td width=40%>".dictionary("score",$_SESSION["language"]).":</td>";
echo "<td><input id=value403 name=value403 type='text' value='".@mysql_result(mysql_query("select param from mainsettings where id=403 "),0,0)."' onkeyup='checkcolorfield(\"form1\",\"value403\",\"savebtn2\"); return false;' ".access("on/off")." style=width:250px;text-align:center;font-weight:bold; autocomplete='off' ></td></tr>";
$t_m_admins=mysql_query("select loginname,CONCAT(surname,' ',name),task_manager_admin from login order by loginname") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "<tr><td width=40% style=vertical-align:top; >".dictionary("task_manager_admins",$_SESSION["language"]).":</td>";
echo "<td><select onchange='checkfield(\"form1\",\"value404\",\"savebtn2\"); return false;' ".access("on/off")." id=value404 size=5 multiple=multiple name=value404[] style=width:250px;text-align:center;font-weight:bold; >";
$cykl=0;while(mysql_result($t_m_admins,$cykl,0)):
    echo "<option value='".mysql_result($t_m_admins,$cykl,0)."'";
if (mysql_result($t_m_admins,$cykl,2)=="Y"){echo " selected=selected ";}    
    echo ">".mysql_result($t_m_admins,$cykl,1)."</option>";
$cykl++;endwhile;
echo"</select></td></tr>";
?></tr></table>

<td style="width:50%;height: 70%;">
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("groups",$_SESSION["language"]);?></b></legend>
<div style=position:relative;align:right;right:0%;top:-10px; align=right><input id=savebtn5 name=savebtn5 type='button' <?echo "onclick=\"if(confirm('";if (!@$_GET["edit"]){echo dictionary("save?",$_SESSION['language']);} else {echo dictionary("save_changes?",$_SESSION['language']);}echo "')) document.getElementById('form1').submit();\"";?> value='<?echo dictionary("save",$_SESSION["language"]);?>' style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>
<?

$load_data=mysql_query("select * from task_manager_groups order by name ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

?>

<select multiple=multiple size="20" style=height:80%;margin:10px; >
<?
$cykl=0;while(mysql_result($load_data,$cykl,0)):
echo "<option value='".mysql_result($load_data,$cykl,0)."'>".mysql_result($load_data,$cykl,1)."</option>";
$cykl++;endwhile;
?>
</select>
</fieldset>
</td></tr>

<tr><td colspan=2 >
<fieldset id=ram><form action=./glob_conf.php id=form1 method='post'><legend id=ram_legenda><b><?echo dictionary("document_lines",$_SESSION["language"]);?></b></legend>
<table id=table_desc width=100%>
<tr id=options>
<td colspan=2><?if (!@$_REQUEST["edit"]){echo dictionary("new_task_manager_marking",$_SESSION['language']);} else{echo dictionary("edit_task_manager_marking",$_SESSION['language']);}?></td>
<td><?echo dictionary("start_date",$_SESSION['language']);?></td>
<td><?echo dictionary("end_date",$_SESSION['language']);?></td>
</tr>

<?
if (@$_GET["edit"]){echo "<input name=edit type=hidden value='".@$_GET["edit"]."' >";
	$data1=mysql_query("select * from task_manager_marking where id='".securesql(@$_REQUEST["edit"])."' ") or Die(MySQL_Error());}

echo"
<tr>
<td nowrap=\"nowrap\" colspan=2><input name=\"value1a\" type=\"text\" value='".@mysql_result($data1,0,1)."' size=25 style=text-align:center; autocomplete=off onkeyup='checkcolorfield(\"form1\",\"value1a\",\"savebtn2\"); return false;' ".access("on/off")." >
<input name=\"value1b\" type=\"text\" value='";if (@$_REQUEST["edit"]){echo @mysql_result($data1,0,2);} else {echo "0";}
echo "' style=width:80px;text-align:center; autocomplete=off onkeyup='checkcolorfield(\"form1\",\"value1b\",\"savebtn2\"); return false;' ".access("on/off")." ></td>
<td nowrap=\"nowrap\"><input type=\"text\" name=\"value1c\" value='".datecs(@mysql_result($data1,0,3))."' readonly=yes style=width:120px;height:23px;text-align:center; onkeyup='checkcolorfield(\"form1\",\"value1c\",\"savebtn2\"); return false;' ".access("on/off")." ><INPUT TYPE=\"button\" VALUE=\"Datum\" onClick=\"cpokus=new calendar(form.value1c,'span_value1c','cpokus');document.getElementById('savebtn2').disabled=false;\" style=width:80px; ><div style=position:relative;top:0px;left:-68px; ><div style=position:absolute><SPAN ID=\"span_value1c\"></div></td>
<td nowrap=\"nowrap\"><input type=\"text\" name=\"value1d\" value='".datecs(@mysql_result($data1,0,4))."' readonly=yes style=width:120px;height:23px;text-align:center; onkeyup='checkcolorfield(\"form1\",\"value1d\",\"savebtn2\"); return false;' ".access("on/off")." ><INPUT TYPE=\"button\" VALUE=\"Datum\" onClick=\"cpokus=new calendar(form.value1d,'span_value1d','cpokus');document.getElementById('savebtn2').disabled=false;\" style=width:80px; ><div style=position:relative;top:0px;left:-68px; ><div style=position:absolute><SPAN ID=\"span_value1d\"></div></td>
</tr>";

echo "<tr>
<td colspan=4 style=text-align:center><br />".dictionary("exist_task_manager_markinging",$_SESSION['language'])."</td></tr>
<tr id=options><td>".dictionary("options",$_SESSION['language'])."</td>
<td>".dictionary("exist_task_manager_doc_lines",$_SESSION['language'])."</td>
<td>".dictionary("start_date",$_SESSION['language'])."</td>
<td>".dictionary("end_date",$_SESSION['language'])."</td>
</tr>";
$load_data=mysql_query("select * from task_manager_marking order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while(@mysql_result($load_data,$load,0)):
echo "<tr class=\"viewoff\" onmouseout=\"className='viewoff';\" onmouseover=\"className='viewon';\">


<td>
<img src='./images/edit.png' border='0' width='24' height='24' alt='".dictionary("editing",$_SESSION["language"])."' style='cursor:pointer'
 onclick=\"if(confirm('".dictionary("edit_document_line",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) window.location.href('".@$rooturl[0]."?edit=".mysql_result($load_data,$load,0)."&option=".@$_REQUEST["option"]."');\"

/>

<img src='./images/delete.png' border='0' width='24' height='24' alt='".dictionary("deleting",$_SESSION["language"])."' style='cursor:pointer' onclick=\"if(confirm('".dictionary("del_doc_line",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) window.location.href('".@$rooturl[0]."?del=".mysql_result($load_data,$load,0)."&option=".@$_REQUEST["option"]."');\" />


</td>



<td>".@mysql_result($load_data,$load,1).@mysql_result($load_data,$load,2)."</td>
<td>".datecs(@mysql_result($load_data,$load,3))."</td>
<td>".datecs(@mysql_result($load_data,$load,4))."</td>
</tr>";
echo  @$rooturl[2];
$load++;
endwhile;?>

<input type='hidden' name=option value='<?echo @$_REQUEST["option"];?>' >
</form></td></tr></table>
</td>
</fieldset>
<?}?>




</td></tr></table>
</body></html>



<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/glob_conf.js");
?>


<?}?>


