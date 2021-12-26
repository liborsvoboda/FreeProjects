<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
if (@$_SESSION["lnamed"]) {?>
<html>
<head>

<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('task_manager',$_SESSION['language']);?>";
</script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />
  
<?//saving


if (@$_REQUEST["formsavebtn1"]){//new request saving

//doc no
$temp=mysql_query("select value,prefix from task_manager_marking where start_date <='".$dnes."' and end_date>='".$dnes."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$numr=mysql_result($temp,0,0)+1;$numlen=strlen($numr);$add="";$origlen=strlen(mysql_result($temp,0,0));
while($origlen>$numlen):$add.="0";$origlen--;endwhile;
mysql_query("update task_manager_marking set value='".$add.$numr."' where start_date <='".$dnes."' and end_date>='".$dnes."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$DocNo =mysql_result($temp,0,1).$add.$numr;

$load_form_data=mysql_query("select param from mainsettings where name=\"task_manager_statuses\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));

// solves create
$solver=@$_POST["value8"];$solves="";
$cykl=0;while (@$solver[$cykl]<>""):
    $solves.=",".@$solver[$cykl];
@$cykl++;
endwhile;
if ($solves<>""){$solves.=",";}

// groups create
$group=@$_POST["value9"];$groups="";
$cykl=0;while (@$group[$cykl]<>""):
    $groups.=",".@$group[$cykl];
@$cykl++;
endwhile;
if ($groups<>""){$groups.=",";}


$result=mysql_query("insert into task_manager_request (name,message,status,priority,requested_date,create_date,creator,document_no,solver,in_groups,author)VALUES('".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".$part[0]."','".securesql(@$_REQUEST["value3"])."','".securesql(datedb(@$_POST["value5"]))."','".$dnest."','".securesql(@$_SESSION["lnamed"])."','".$DocNo."','".securesql($solves)."','".securesql($groups)."','".securesql(@$_SESSION["lnamed"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$result=mysql_query("insert into task_manager_history (parent_no,name,message,status,priority,requested_date,create_date,creator,solver,in_groups)VALUES('".$DocNo."','".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value2"])."','".$part[0]."','".securesql(@$_REQUEST["value3"])."','".securesql(datedb(@$_POST["value5"]))."','".$dnest."','".securesql(@$_SESSION["lnamed"])."','".securesql($solves)."','".securesql($groups)."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

//saving new attachments
$cykl=1;while(@$_FILES['file'.$cykl]['name']):
    @$filename= @$_FILES['file'.$cykl]['name'];
    @$temp = @$_FILES['file'.$cykl]['tmp_name'];@$mime = @$_FILES['file'.$cykl]['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
    $file_result=mysql_query("insert into task_manager_attachment (parent_no,icon_name,icon,mime_type,create_date,creator,changeable)VALUES('".$DocNo."','".$filename."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."','".$dnest."','".securesql(@$_SESSION["lnamed"])."','YES')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    if ($file_result<>1) {savefailed($filename);}
$cykl++;endwhile;
// saving new attachments

	if ($result==1) {
    savesuccess(@$_REQUEST["value1"]);
            if (@$_REQUEST["value10"]=="on") {$confirm=mysql_result(mysql_query("SELECT email FROM login WHERE loginname='".securesql($_SESSION["lnamed"])."' "),0,0);}
            else {$confirm="";}
    task_manager_mail(dictionary("new_request",$_SESSION["language"]).": ",$DocNo,$confirm);
	fn_unset_var("value",9);   
	}
}


if (@$_REQUEST["formsavebtn3"]){  //new step saving

//load header information
$load_header=mysql_query("select * from task_manager_request where document_no='".securesql(@$_REQUEST["value200"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
// end of load header information

// solves create
$solver=@$_POST["value8"];$solves="";
$cykl=0;while (@$solver[$cykl]<>""):
    $solves.=",".@$solver[$cykl];
@$cykl++;
endwhile;
if ($solves<>""){$solves.=",";}

// groups create
$group=@$_POST["value9"];$groups="";
$cykl=0;while (@$group[$cykl]<>""):
    $groups.=",".@$group[$cykl];
@$cykl++;
endwhile;
if ($groups<>""){$groups.=",";}

$result=mysql_query("insert into task_manager_history (parent_no,name,message,status,priority,requested_date,create_date,creator,score,solution_date,solver,in_groups)VALUES('".securesql(@$_REQUEST["value200"])."','".securesql(mysql_result($load_header,0,12))."','".securesql(@$_REQUEST["value1"])."','".securesql(@$_REQUEST["value5"])."','".securesql(@$_REQUEST["value2"])."','".securesql(datedb(@$_REQUEST["value4"]))."','".$dnest."','".securesql(@$_SESSION["lnamed"])."','".securesql(@$_REQUEST["value6"])."','".securesql(datedb(@$_REQUEST["value7"]))."','".securesql($solves)."','".securesql($groups)."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

// update header information
$result=mysql_query("update task_manager_request set message='".securesql(@$_REQUEST["value1"])."', status ='".securesql(@$_REQUEST["value5"])."', priority='".securesql(@$_REQUEST["value2"])."', requested_date='".securesql(datedb(@$_REQUEST["value4"]))."', score='".securesql(@$_REQUEST["value6"])."', last_change_date='".$dnest."', solution_date='".securesql(datedb(@$_REQUEST["value7"]))."',solver='".securesql($solves)."',in_groups='".securesql($groups)."' where document_no = '".securesql(securesql(@$_REQUEST["value200"]))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

//saving new attachments
$cykl=1;while(@$_FILES['file'.$cykl]['name']):
    @$filename= @$_FILES['file'.$cykl]['name'];
    @$temp = @$_FILES['file'.$cykl]['tmp_name'];@$mime = @$_FILES['file'.$cykl]['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
    $file_result=mysql_query("insert into task_manager_attachment (parent_no,icon_name,icon,mime_type,create_date,creator,changeable)VALUES('".securesql(@$_REQUEST["value200"])."','".$filename."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."','".$dnest."','".securesql(@$_SESSION["lnamed"])."','YES')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    if ($file_result<>1) {savefailed($filename);}
$cykl++;endwhile;
//end of saving new attachments

	if ($result==1) {
        savesuccess(@$_REQUEST["value1"]);
        if (@$_REQUEST["value10"]=="on") {$confirm=mysql_result(mysql_query("SELECT email FROM login WHERE loginname='".securesql($_SESSION["lnamed"])."' "),0,0);}
            else {$confirm="";}
        task_manager_mail(dictionary("new_step",$_SESSION["language"]).": ",securesql(@$_REQUEST["value200"]),$confirm);
    	fn_unset_var("value",10);   
	}

}




if (@$_REQUEST["act"]){ //request delete
$data_control=mysql_query("select id from task_manager_history where parent_no='".securesql(base64_decode(@$_REQUEST["act"]))."' order by id DESC ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
if (@mysql_result($data_control,0,0)){$result=mysql_query("delete from task_manager_history where id='".mysql_result($data_control,0,0)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {deletesuccess($control);}
}


if (@$_REQUEST["formsavebtn2"]){//update request
$DocNo=mysql_result(mysql_query("select document_no from task_manager_request where id='".securesql(@$_REQUEST["v1000"])."' "),0,0);
@$filename= @$_FILES['value4']['name'];@$temp = @$_FILES['value4']['tmp_name'];@$mime = @$_FILES['value4']['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
if (@$mime){$result=mysql_query(" update task_manager_request set name = '".securesql(@$_REQUEST["value1"])."',message = '".securesql(@$_REQUEST["value2"])."',status = '".securesql(@$_REQUEST["value6"])."',priority = '".securesql(@$_REQUEST["value3"])."',requested_date = '".securesql(datedb(@$_REQUEST["evalue5"]))."',icon_name='".$filename."',icon = '".mysql_escape_string(@$logo)."',mime_type = '".securesql(@$mime)."',last_change_date = '".$dnest."',score='".securesql(@$_REQUEST["value10"])."' where id = '".securesql(@$_REQUEST["v1000"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
} else {
	$result=mysql_query(" update task_manager_request set name = '".securesql(@$_REQUEST["value1"])."',message = '".securesql(@$_REQUEST["value2"])."',status = '".securesql(@$_REQUEST["value6"])."',priority = '".securesql(@$_REQUEST["value3"])."',requested_date = '".securesql(datedb(@$_REQUEST["evalue5"]))."',last_change_date = '".$dnest."',score='".securesql(@$_REQUEST["value10"])."' where id = '".securesql(@$_REQUEST["v1000"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($result==1) {
	savechangesuccess(@$_REQUEST["value1"]);
	task_manager_mail(dictionary("edit_request",$_SESSION["language"]).": ",$DocNo);
	fn_unset_var("value",9);   
	}
}// end of saving


$rooturl=explode("?",$_SERVER["REQUEST_URI"]);
?>
</head>




<body  onkeydown="cancelBack()">


<table id='fullframetable' onselectstart="return false;" >
<tr style='width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >
<?



// first open - my request

if (!@$_REQUEST["option"]) {@$_REQUEST["option"]="my_task_manager";}

//end of start setting




$load_data=mysql_query("select * from task_manager_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):

if ($load==0){
// first main list    
     echo "<span ";
     if (mysql_result(mysql_query("select task_manager_admin from login where loginname='".securesql(@$_SESSION['lnamed'])."' "),0,0)=="N") {echo " DISABLED ";}
        if (@$_REQUEST["option"]=="task_manager_list" or !@$_REQUEST["option"]) {echo " class=\"bookmarkin\" onclick=\"window.location.assign('./task_manager.php?option=task_manager_list')\" ";}
        if (@$_REQUEST["option"]<>"task_manager_list" and @$_REQUEST["option"]) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./task_manager.php?option=task_manager_list')\" ";}
        echo " >".dictionary("task_manager_list",$_SESSION["language"])."</span>";
    
}

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,2)) {echo " class=\"bookmarkin\" ";}
        if (!@$_REQUEST["option"] or @$_REQUEST["option"]<>mysql_result($load_data,$load,2)) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./task_manager.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,2),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>

<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >





<?if (@$_REQUEST["option"]=='task_manager_list' or !@$_REQUEST["option"]) { // přehled požadavků
?><div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:5px;' <? echo access("on/off");?> >
<span id="funct_btn1" style='cursor:pointer;' onclick='addrequest();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("new_request",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("new_request",$_SESSION["language"]);?></span>
<span style='cursor:pointer;' onclick='reset_form("form1");' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("clean_filter",$_SESSION["language"]);?>' ><img src='./images/delete1.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("clean_filter",$_SESSION["language"]);?></span>
</div>
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' name='form1' action='<?echo $_SERVER["PHP_SELF"];?>' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("task_manager_list",$_SESSION["language"]);?></b><img src="./images/help.png" style="width:20px;height:20px;" title="<?echo dictionary("filter_small_help",$_SESSION["language"]);?>" /></legend>
<input type=hidden id=hidden_v1 name=hidden_v1 value='<?echo @$_POST["hidden_v1"];?>'>

<div style='position:absolute;align:right;right:23px;top:10px;' <? echo access("on/off");?> >
<span ><?echo dictionary("show_closed",$_SESSION["language"]);?> <input name=filter onclick="submit(this)" type="checkbox" <?if (@$_POST["filter"]){echo " checked ";}?> /></span>
</div>

<?
echo "<table id=table_desc style=width:100%>";
echo "<tr id=options>
<td>".dictionary("document_no",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v1','hidden_v1') type=text id=search_v1 name=search_v1 value='".@$_POST["search_v1"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("title",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v2','hidden_v1') type=text id=search_v2 name=search_v2 value='".@$_POST["search_v2"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("request_to",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v3','hidden_v1') type=text id=search_v3 name=search_v3 value='".@$_POST["search_v3"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("solution_to",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v4','hidden_v1') type=text id=search_v4 name=search_v4 value='".@$_POST["search_v4"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("solves",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v5','hidden_v1') type=text id=search_v5 name=search_v5 value='".@$_POST["search_v5"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("status",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v6','hidden_v1') type=text id=search_v6 name=search_v6 value='".@$_POST["search_v6"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("priority",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v7','hidden_v1') type=text id=search_v7 name=search_v7 value='".@$_POST["search_v7"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("creator",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v10','hidden_v1') type=text id=search_v10 name=search_v10 value='".@$_POST["search_v10"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("last_change_date",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v8','hidden_v1') type=text id=search_v8 name=search_v8 value='".@$_POST["search_v8"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("create_date",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v9','hidden_v1') type=text id=search_v9 name=search_v9 value='".@$_POST["search_v9"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("author",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v11','hidden_v1') type=text id=search_v11 name=search_v11 value='".@$_POST["search_v11"]."' style=width:100%;font-size:10px;text-align:center; ></td>
</tr>";
 //<td>".dictionary("options",$_SESSION["language"])."</td>

// list of searched fields
$search_var=",document_no,name,requested_date,solution_date,solver,status,priority,last_change_date,create_date,creator,author";
$searched_name=explode(",",$search_var);
$where="where (";$cycle=1;while(@$searched_name[$cycle]):

    if (@$_POST["search_v".$cycle] && $where<>"where (" && $cycle<>1){$where.=" and ";}
    
    if (@$_POST["search_v".$cycle] && @$searched_name[$cycle]=='solver'){$where.=@$searched_name[$cycle]." like '%,".securesql(@$_POST["search_v".$cycle])."%'";}
    if (@$_POST["search_v".$cycle] && @$searched_name[$cycle]<>'solver'){$where.=@$searched_name[$cycle]." like '".securesql(@$_POST["search_v".$cycle])."%'";}


$cycle++;
endwhile;
if ($where<>"where ("){$where.=")";} else {$where="";}

// conrol for rights

$control=mysql_query("select param from mainsettings where name='task_manager_statuses' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($control,0,0));
//last status
@$search=0;while(@$part[($search+1)]):$search++;endwhile;@$laststatus=@$part[$search];

if (!@$_POST["filter"]){
    if ($where==""){$where=" where status <> '".securesql($laststatus)."' ";}
    else {$where.=" and status <> '".securesql($laststatus)."' ";}
}

$load_data=mysql_query("select * from task_manager_request $where order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$load=0;while($load<mysql_num_rows($load_data)):
@$attachment="";
if (@mysql_result($load_data,$load,11)<>"" ){@$attachment=base64_encode(mysql_result($load_data,$load,0));}
	ELSE{$attachment="";}


  echo "<tr id=\"funct_btn2\" class=\"viewoff\" onmouseout=\"className='viewoff';\" onmouseover=\"className='viewon';\" onclick=show_history('".rawurlencode(htmlspecialchars(mysql_result($load_data,$load,12)))."'); >
  <td>".mysql_result($load_data,$load,12)."</td>
  <td>".mysql_result($load_data,$load,1)."</td>
  <td>".datecs(mysql_result($load_data,$load,5))."</td>
  <td>";if (mysql_result($load_data,$load,14)<>"0000-00-00"){echo datecs(mysql_result($load_data,$load,14));}echo"</td>
  <td>".mysql_result($load_data,$load,15)."</td>
  <td>".mysql_result($load_data,$load,3)."</td>
  <td>".mysql_result($load_data,$load,4)."</td>
  <td>".mysql_result($load_data,$load,10)."</td>
  <td>".datetcs(mysql_result($load_data,$load,8))."</td>
  <td>".datetcs(mysql_result($load_data,$load,9))."</td>
  <td>".mysql_result($load_data,$load,17)."</td>
  </tr>";
  //<td>
//<img src='./images/edit.png' border='0' width='24' height='24' alt='".dictionary("editing",$_SESSION["language"])."' style='cursor:pointer'";
//if (((mysql_result($load_data,$load,3)<>$part[0] && mysql_result($load_data,$load,3)<>$laststatus) or @$_SESSION["lnamed"]<>mysql_result($load_data,$load,10)) && @$_SESSION["sysadmin"]<>"Yes") {echo " disabled ";}
//echo " onclick=\"editrequest('".htmlspecialchars(mysql_result($load_data,$load,1))."','".rawurlencode(htmlspecialchars(mysql_result($load_data,$load,2)))."','".mysql_result($load_data,$load,4)."','".datecs(mysql_result($load_data,$load,5))."','".$attachment."','".mysql_result($load_data,$load,0)."','".mysql_result($load_data,$load,3)."','".htmlspecialchars(mysql_result($load_data,$load,11))."','".mysql_result($load_data,$load,12)."','".htmlspecialchars(mysql_result($load_data,$load,13))."','".$laststatus."');\"/>

//<img src='./images/delete.png' border='0' width='24' height='24' alt='".dictionary("deleting",$_SESSION["language"])."' style='cursor:pointer' ".access("remove");
//if ((mysql_result($load_data,$load,3)<>$part[0] or @$_SESSION["lnamed"]<>mysql_result($load_data,$load,10)) && @$_SESSION["sysadmin"]<>"Yes") {echo " disabled ";}
//echo" onclick=\"if(confirm('".dictionary("del_request",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) del_request('".base64_encode(mysql_result($load_data,$load,0))."');\"/></td></tr>";
@$load++;
endwhile;echo"</table>"?>

<input id="act" name="act" type="hidden" value="" disabled='disabled' />
</form></fieldset></td></tr></table>
</div>
<?} // end of request List
?>










<?if (@$_REQUEST["option"]=='groups_tasks') { // přehled požadavků skupiny
?><div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:5px;' <? echo access("on/off");?> >
<span id="funct_btn1" style='cursor:pointer;' onclick='addrequest();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("new_request",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("new_request",$_SESSION["language"]);?></span>
<span style='cursor:pointer;' onclick='reset_form("form1");' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("clean_filter",$_SESSION["language"]);?>' ><img src='./images/delete1.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("clean_filter",$_SESSION["language"]);?></span>
</div>
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' name='form1' action='<?echo $_SERVER["PHP_SELF"];?>' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("task_manager_list",$_SESSION["language"]);?></b><img src="./images/help.png" style="width:20px;height:20px;" title="<?echo dictionary("filter_small_help",$_SESSION["language"]);?>" /></legend>
<input type=hidden id=hidden_v1 name=hidden_v1 value='<?echo @$_POST["hidden_v1"];?>'>

<div style='position:absolute;align:right;right:23px;top:10px;' <? echo access("on/off");?> >
<span ><?echo dictionary("show_closed",$_SESSION["language"]);?> <input name=filter onclick="submit(this)" type="checkbox" <?if (@$_POST["filter"]){echo " checked ";}?> /></span>
</div>

<?
echo "<table id=table_desc style=width:100%>";
echo "<tr id=options>
<td>".dictionary("document_no",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v1','hidden_v1') type=text id=search_v1 name=search_v1 value='".@$_POST["search_v1"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("title",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v2','hidden_v1') type=text id=search_v2 name=search_v2 value='".@$_POST["search_v2"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("request_to",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v3','hidden_v1') type=text id=search_v3 name=search_v3 value='".@$_POST["search_v3"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("solution_to",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v4','hidden_v1') type=text id=search_v4 name=search_v4 value='".@$_POST["search_v4"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("solves",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v5','hidden_v1') type=text id=search_v5 name=search_v5 value='".@$_POST["search_v5"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("status",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v6','hidden_v1') type=text id=search_v6 name=search_v6 value='".@$_POST["search_v6"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("priority",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v7','hidden_v1') type=text id=search_v7 name=search_v7 value='".@$_POST["search_v7"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("creator",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v10','hidden_v1') type=text id=search_v10 name=search_v10 value='".@$_POST["search_v10"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("last_change_date",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v8','hidden_v1') type=text id=search_v8 name=search_v8 value='".@$_POST["search_v8"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("create_date",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v9','hidden_v1') type=text id=search_v9 name=search_v9 value='".@$_POST["search_v9"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("author",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v11','hidden_v1') type=text id=search_v11 name=search_v11 value='".@$_POST["search_v11"]."' style=width:100%;font-size:10px;text-align:center; ></td>
</tr>";
 //<td>".dictionary("options",$_SESSION["language"])."</td>

// list of searched fields
$search_var=",document_no,name,requested_date,solution_date,solver,status,priority,last_change_date,create_date,creator,author";
$searched_name=explode(",",$search_var);
$where="where (";$cycle=1;while(@$searched_name[$cycle]):

    if (@$_POST["search_v".$cycle] && $where<>"where (" && $cycle<>1){$where.=" and ";}
    
    if (@$_POST["search_v".$cycle] && @$searched_name[$cycle]=='solver'){$where.=@$searched_name[$cycle]." like '%,".securesql(@$_POST["search_v".$cycle])."%'";}
    if (@$_POST["search_v".$cycle] && @$searched_name[$cycle]<>'solver'){$where.=@$searched_name[$cycle]." like '".securesql(@$_POST["search_v".$cycle])."%'";}


$cycle++;
endwhile;
if ($where<>"where ("){$where.=")";} else {$where="";}

// conrol for rights

$control=mysql_query("select param from mainsettings where name='task_manager_statuses' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($control,0,0));
//last status
@$search=0;while(@$part[($search+1)]):$search++;endwhile;@$laststatus=@$part[$search];

if (!@$_POST["filter"]){
    if ($where==""){$where=" where status <> '".securesql($laststatus)."' ";}
    else {$where.=" and status <> '".securesql($laststatus)."' ";}
}

$my_groups=mysql_result(mysql_query("select groups from login where loginname='".securesql(@$_SESSION["lnamed"])."'"),0,0);
$my_groups=explode(",",$my_groups);


$cycle=1;while(@$my_groups[@$cycle]):

if ($where<>""  && $cycle==1 ){$where.="and ( ";}
if ($where==""  && $cycle==1 ){$where.="where ( ";}
    
        $where.=" in_groups like '%,".@$my_groups[@$cycle].",%' ";
    if (@$my_groups[(@$cycle+1)]){$where.=" or ";}
$cycle++;
endwhile;
if (@$cycle<>1) {$where.=" ) ";}

$load_data=mysql_query("select * from task_manager_request $where order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$load=0;while($load<mysql_num_rows($load_data)):
@$attachment="";
if (@mysql_result($load_data,$load,11)<>"" ){@$attachment=base64_encode(mysql_result($load_data,$load,0));}
	ELSE{$attachment="";}


  echo "<tr id=\"funct_btn2\" class=\"viewoff\" onmouseout=\"className='viewoff';\" onmouseover=\"className='viewon';\" onclick=show_history('".rawurlencode(htmlspecialchars(mysql_result($load_data,$load,12)))."'); >
  <td>".mysql_result($load_data,$load,12)."</td>
  <td>".mysql_result($load_data,$load,1)."</td>
  <td>".datecs(mysql_result($load_data,$load,5))."</td>
  <td>";if (mysql_result($load_data,$load,14)<>"0000-00-00"){echo datecs(mysql_result($load_data,$load,14));}echo"</td>
  <td>".mysql_result($load_data,$load,15)."</td>
  <td>".mysql_result($load_data,$load,3)."</td>
  <td>".mysql_result($load_data,$load,4)."</td>
  <td>".mysql_result($load_data,$load,10)."</td>
  <td>".datetcs(mysql_result($load_data,$load,8))."</td>
  <td>".datetcs(mysql_result($load_data,$load,9))."</td>
  <td>".mysql_result($load_data,$load,17)."</td>
  </tr>";
  //<td>
//<img src='./images/edit.png' border='0' width='24' height='24' alt='".dictionary("editing",$_SESSION["language"])."' style='cursor:pointer'";
//if (((mysql_result($load_data,$load,3)<>$part[0] && mysql_result($load_data,$load,3)<>$laststatus) or @$_SESSION["lnamed"]<>mysql_result($load_data,$load,10)) && @$_SESSION["sysadmin"]<>"Yes") {echo " disabled ";}
//echo " onclick=\"editrequest('".htmlspecialchars(mysql_result($load_data,$load,1))."','".rawurlencode(htmlspecialchars(mysql_result($load_data,$load,2)))."','".mysql_result($load_data,$load,4)."','".datecs(mysql_result($load_data,$load,5))."','".$attachment."','".mysql_result($load_data,$load,0)."','".mysql_result($load_data,$load,3)."','".htmlspecialchars(mysql_result($load_data,$load,11))."','".mysql_result($load_data,$load,12)."','".htmlspecialchars(mysql_result($load_data,$load,13))."','".$laststatus."');\"/>

//<img src='./images/delete.png' border='0' width='24' height='24' alt='".dictionary("deleting",$_SESSION["language"])."' style='cursor:pointer' ".access("remove");
//if ((mysql_result($load_data,$load,3)<>$part[0] or @$_SESSION["lnamed"]<>mysql_result($load_data,$load,10)) && @$_SESSION["sysadmin"]<>"Yes") {echo " disabled ";}
//echo" onclick=\"if(confirm('".dictionary("del_request",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) del_request('".base64_encode(mysql_result($load_data,$load,0))."');\"/></td></tr>";
@$load++;
endwhile;echo"</table>"?>

<input id="act" name="act" type="hidden" value="" disabled='disabled' />
</form></fieldset></td></tr></table>
</div>
<?} // end of groups request List
?>










<?if (@$_REQUEST["option"]=='my_task_manager') { // moje pozadavky
?><div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:5px;' <? echo access("on/off");?> >
<span id="funct_btn1" style='cursor:pointer;' onclick='addrequest();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("new_request",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("new_request",$_SESSION["language"]);?></span>
<span style='cursor:pointer;' onclick='reset_form("form1");' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("clean_filter",$_SESSION["language"]);?>' ><img src='./images/delete1.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("clean_filter",$_SESSION["language"]);?></span>
</div>
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;' >
<tr style='width:100%;height:100%;' ><td style='width:100%;height:100%;' >
<fieldset id='ram'><form id='form1' action='<?echo $_SERVER["PHP_SELF"];?>' name='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("task_manager_list",$_SESSION["language"]);?></b><img src="./images/help.png" style="width:20px;height:20px;" title="<?echo dictionary("filter_small_help",$_SESSION["language"]);?>" /></legend>
<input type=hidden id=hidden_v1 name=hidden_v1 value='<?echo @$_POST["hidden_v1"];?>'>

<div style='position:absolute;align:right;right:23px;top:10px;' <? echo access("on/off");?> >
<span ><?echo dictionary("show_closed",$_SESSION["language"]);?> <input name=filter onclick="submit(this)" type="checkbox" <?if (@$_POST["filter"]){echo " checked ";}?> /></span>
</div>

<?
echo "<table id=table_desc style=width:100%>";
echo "<tr id=options>
<td>".dictionary("document_no",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v1','hidden_v1') type=text id=search_v1 name=search_v1 value='".@$_POST["search_v1"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("title",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v2','hidden_v1') type=text id=search_v2 name=search_v2 value='".@$_POST["search_v2"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("request_to",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v3','hidden_v1') type=text id=search_v3 name=search_v3 value='".@$_POST["search_v3"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("solution_to",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v4','hidden_v1') type=text id=search_v4 name=search_v4 value='".@$_POST["search_v4"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("solves",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v5','hidden_v1') type=text id=search_v5 name=search_v5 value='".@$_POST["search_v5"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("status",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v6','hidden_v1') type=text id=search_v6 name=search_v6 value='".@$_POST["search_v6"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("priority",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v7','hidden_v1') type=text id=search_v7 name=search_v7 value='".@$_POST["search_v7"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("creator",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v10','hidden_v1') type=text id=search_v10 name=search_v10 value='".@$_POST["search_v10"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("last_change_date",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v8','hidden_v1') type=text id=search_v8 name=search_v8 value='".@$_POST["search_v8"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("create_date",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v9','hidden_v1') type=text id=search_v9 name=search_v9 value='".@$_POST["search_v9"]."' style=width:100%;font-size:10px;text-align:center; ></td>
<td>".dictionary("author",$_SESSION["language"])."<input onkeyup=submit_field_from('form1','search_v11','hidden_v1') type=text id=search_v11 name=search_v11 value='".@$_POST["search_v11"]."' style=width:100%;font-size:10px;text-align:center; ></td>
</tr>";
 //<td>".dictionary("options",$_SESSION["language"])."</td>

// list of searched fields
$search_var=",document_no,name,requested_date,solution_date,solver,status,priority,last_change_date,create_date,creator,author";
$searched_name=explode(",",$search_var);
$where="where (";$cycle=1;while(@$searched_name[$cycle]):

    if (@$_POST["search_v".$cycle] && $where<>"where (" && $cycle<>1){$where.=" and ";}

    if (@$_POST["search_v".$cycle] && @$searched_name[$cycle]=='solver'){$where.=@$searched_name[$cycle]." like '%,".securesql(@$_POST["search_v".$cycle])."%'";}
    if (@$_POST["search_v".$cycle] && @$searched_name[$cycle]<>'solver'){$where.=@$searched_name[$cycle]." like '".securesql(@$_POST["search_v".$cycle])."%'";}

$cycle++;
endwhile;
if ($where<>"where ("){$where.=" and (creator='".securesql(@$_SESSION["lnamed"])."' or solver like '%,".securesql(@$_SESSION["lnamed"]).",%') )";}
    else {$where=" where (creator='".securesql(@$_SESSION["lnamed"])."' or solver like '%,".securesql(@$_SESSION["lnamed"]).",%') ";}

// conrol for rights
$control=mysql_query("select param from mainsettings where name='task_manager_statuses' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($control,0,0));
//last status
@$search=0;while(@$part[($search+1)]):$search++;endwhile;@$laststatus=@$part[$search];

if (!@$_POST["filter"]){
    if ($where==""){$where=" where status <> '".securesql($laststatus)."' ";}
    else {$where.=" and status <> '".securesql($laststatus)."' ";}
}

$load_data=mysql_query("select * from task_manager_request $where order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$load=0;while($load<mysql_num_rows($load_data)):
@$attachment="";
if (@mysql_result($load_data,$load,11)<>"" ){@$attachment=base64_encode(mysql_result($load_data,$load,0));}
	ELSE{$attachment="";}


  echo "<tr id=\"funct_btn2\" class=\"viewoff\" onmouseout=\"className='viewoff';\" onmouseover=\"className='viewon';\" onclick=show_history('".rawurlencode(htmlspecialchars(mysql_result($load_data,$load,12)))."'); >
  <td>".mysql_result($load_data,$load,12)."</td>
  <td>".mysql_result($load_data,$load,1)."</td>
  <td>".datecs(mysql_result($load_data,$load,5))."</td>
  <td>";if (mysql_result($load_data,$load,14)<>"0000-00-00"){echo datecs(mysql_result($load_data,$load,14));}echo"</td>
  <td>".mysql_result($load_data,$load,15)."</td>
  <td>".mysql_result($load_data,$load,3)."</td>
  <td>".mysql_result($load_data,$load,4)."</td>
  <td>".mysql_result($load_data,$load,10)."</td>
  <td>".datetcs(mysql_result($load_data,$load,8))."</td>
  <td>".datetcs(mysql_result($load_data,$load,9))."</td>
  <td>".mysql_result($load_data,$load,17)."</td>
  </tr>";
  //<td>
//<img src='./images/edit.png' border='0' width='24' height='24' alt='".dictionary("editing",$_SESSION["language"])."' style='cursor:pointer'";
//if (((mysql_result($load_data,$load,3)<>$part[0] && mysql_result($load_data,$load,3)<>$laststatus) or @$_SESSION["lnamed"]<>mysql_result($load_data,$load,10)) && @$_SESSION["sysadmin"]<>"Yes") {echo " disabled ";}
//echo " onclick=\"editrequest('".htmlspecialchars(mysql_result($load_data,$load,1))."','".rawurlencode(htmlspecialchars(mysql_result($load_data,$load,2)))."','".mysql_result($load_data,$load,4)."','".datecs(mysql_result($load_data,$load,5))."','".$attachment."','".mysql_result($load_data,$load,0)."','".mysql_result($load_data,$load,3)."','".htmlspecialchars(mysql_result($load_data,$load,11))."','".mysql_result($load_data,$load,12)."','".htmlspecialchars(mysql_result($load_data,$load,13))."','".$laststatus."');\"/>

//<img src='./images/delete.png' border='0' width='24' height='24' alt='".dictionary("deleting",$_SESSION["language"])."' style='cursor:pointer' ".access("remove");
//if ((mysql_result($load_data,$load,3)<>$part[0] or @$_SESSION["lnamed"]<>mysql_result($load_data,$load,10)) && @$_SESSION["sysadmin"]<>"Yes") {echo " disabled ";}
//echo" onclick=\"if(confirm('".dictionary("del_request",$_SESSION['language'])." : ".mysql_result($load_data,$load,1)."?')) del_request('".base64_encode(mysql_result($load_data,$load,0))."');\"/></td></tr>";
@$load++;
endwhile;echo"</table>"?>

<input id="act" name="act" type="hidden" value="" disabled='disabled' />
</form></fieldset></td></tr></table>
</div>
<?} // end of request List
?>












</td></tr></table></body>
</html>



<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/task_manager_scripts.js");

// open specific doc from external link
if (!@strpos(" ".@$rooturl[1],"=") && @$rooturl[1]){
    if (@mysql_num_rows(mysql_query("select id from task_manager_request where (creator='".securesql(@$_SESSION["lnamed"])."' or solver like '%,".securesql(@$_SESSION["lnamed"]).",%' or author='".securesql(@$_SESSION["lnamed"])."') and document_no='".securesql(decode(@$rooturl[1]))."' "))){    
       echo "<script>show_history('".rawurlencode(htmlspecialchars(decode(@$rooturl[1])))."')</script>";
       } else {message(dictionary("access_denied",$_SESSION["language"]));    
    }
}

?>

<?}?>


