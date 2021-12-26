<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
require_once ("./config/mssql_dbconnect.php");

if (@$_SESSION["lnamed"]) {?>
<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('epdm_report_form',$_SESSION['language']);?>";
</script>
<link rel="icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon" />
<link rel="shortcut icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />

<script>
document.write('<DIV id="loading" onselectstart="return TRUE;" style=z-index:100;><BR><?echo dictionary("please_wait",$_SESSION["language"]);?><br /><img src="images/loading.gif" border="0"></DIV>');
    document.getElementById("loading").style.display="none";
</script>

<? //saving
if ((!@$_REQUEST["option"] or @$_REQUEST["option"]=='report_page') && @$_REQUEST["formsavebtn1"]){//new request saving
@$temp = @$_FILES['value1']['tmp_name'];
@$file = implode('', file("$temp"));
$imported_exceptions= explode ("\r\n",$file);

$cycle=1;while($imported_exceptions[$cycle]):
    @$control=mysql_result(mysql_query("Select exception from epdm_exceptions where exception='".securesql($imported_exceptions[$cycle])."' "),0,0);
    if (!@$control){
        $result=mysql_query("insert into epdm_exceptions (exception,create_date,creator)VALUES('".securesql($imported_exceptions[$cycle])."','".$dnest."','".securesql(@$_SESSION["lnamed"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());    
    }
$cycle++;
endwhile;
	if ($result==1) {savesuccess(@$_REQUEST["value1"]);}
        else {message(dictionary("any_record_inserted",$_SESSION['language']));}
}    

// end of saving
?>

</head>


<Body onselectstart="return false;" >
<table id='fullframetable' >
<tr style='width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<?

 echo "<span ";
        if (@$_REQUEST["option"]=="report_page" or !@$_REQUEST["option"]) {echo " class=\"bookmarkin\" ";}
        if (@$_REQUEST["option"]<>"report_page" and @$_REQUEST["option"]) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./hotline.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " >".dictionary("report_page",$_SESSION["language"])."</span>";


$load_data=mysql_query("select * from reports_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,2) or !@$_REQUEST["option"]) {echo " class=\"bookmarkin\" ";}
        if (@$_REQUEST["option"]<>mysql_result($load_data,$load,2) and @$_REQUEST["option"]) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./epdm_report.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,2),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>

<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<?if (@$_REQUEST["option"]=='report_page' or !@$_REQUEST["option"]) { // report
?><div id='bookmark' >

<div style='position:absolute;align:left;left:20px;top:5px;' <? echo access("on/off");?> >
<span style='cursor:pointer;' onclick="load_ex();" class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("import_exceptions",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("import_exceptions",$_SESSION["language"]);?></span>
<span style='cursor:pointer;' onclick="window.open('./Data/<?echo date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0);?>')" class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("xls_report",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("xls_report",$_SESSION["language"]);?></span>
</div>

<div style=position:absolute;align:right;right:22px;top:10px; align=right><input id=savebtn1 type='button' value='<?echo dictionary("gen_report",$_SESSION["language"]);?>' onclick="document.getElementById('loading').style.display='inline';window.location.assign('./epdm_report.php?option=<?echo @$_REQUEST["option"];?>&act=GEN')" style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:105%;' ><td style='width:100%;height:105%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("report_page",$_SESSION["language"]);?></b></legend>
<?
$mailbody="<table id=table_desc style=width:100%><tr id=options>
<td>".dictionary("opv",$_SESSION["language"])."</td>
<td>".dictionary("nomenklatura",$_SESSION["language"])."</td>
<td>".dictionary("description",$_SESSION["language"])."</td>
<td>".dictionary("procedure",$_SESSION["language"])."</td>
<td>".dictionary("catalogue_number",$_SESSION["language"])."</td> 
<td>".dictionary("drawing_no",$_SESSION["language"])."</td>
<td>".dictionary("status",$_SESSION["language"])."</td>
<td>".dictionary("file",$_SESSION["language"])."</td>
<td>".dictionary("terminzac",$_SESSION["language"])."</td>
</tr>";

$founded="No";
$line=0;

$data_file = @mysql_result(mysql_query("select param from mainsettings where name='epdm_source_file_path' "),0,0);
$openedfile = file_get_contents($data_file);

//select a provedeni dat
    //$sql = "SELECT  * FROM dbo.[10_epdm_draw_file] ";
    //$stmt = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
// vypis sql dat
//echo sqlsrv_num_rows($stmt);  pocet zaznamu



//upload file list
if ($_GET["act"]=="GEN"){    
 $records = explode("\r\n", $openedfile); //nacteni souboru
 $cycle=7;while($records[($cycle+2)]):
  $file_name=substr($records[$cycle],38,(strlen($records[$cycle])-37));
    IF ($file_name[0]==" "){$file_name=substr($file_name,1,(strlen($file_name)-1));}
  $sql = "SELECT * FROM dbo.[10_epdm_draw_file_list] where file_name like '".$file_name."%' ";
  $stmt = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
   if (sqlsrv_num_rows($stmt)==0) {
    $sql_insert = "insert into dbo.[10_epdm_draw_file_list] (file_name)VALUES('".securesql($file_name)."') ";
    $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    }
 $cycle++;
 endwhile;  
// end of upload file

//select a provedeni dat
$params = array($zp);

$where="";
$load_condition=mysql_query("select exception from epdm_exceptions") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$cr_con=0;while(@mysql_result($load_condition,$cr_con,0)):
    if ($cr_con==0){$where=" where ( ";}
        if (@mysql_result($load_condition,$cr_con,0)) {$where.="nomenklatura not like '%".@mysql_result($load_condition,$cr_con,0)."%'";}
        if (@mysql_result($load_condition,($cr_con+1),0)) {$where.=" and ";}
$cr_con++;
endwhile;

if (@$where<>""){$where.=" and postup not like '%2NSV%' )";}
    else {$where=" where postup not like '%2NSV%' ";}

    $sql = "SELECT * FROM dbo.[10_epdm_draw_file] ".$where;
    $stmt = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( $row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH ) ) {

// where for all file names creation
$where_sql="";        
if ($row[8]){$where_sql.=" where ( file_name like '".trim($row[8])."%'";}
if ($row[4]){
    if ($where_sql<>""){$where_sql.=" or ";} else {$where_sql.=" where ( ";}
    $where_sql.="file_name like '".trim($row[4])."%'";}
if ($row[2]){
    if ($where_sql<>""){$where_sql.=" or ";} else {$where_sql.=" where ( ";}
    $where_sql.="file_name like '".trim($row[2])."%'";}
    if ($where_sql<>""){$where_sql.=" )";}
// end of where creation    
    
$params = array($filelist);
$sql1 = "SELECT * FROM dbo.[10_epdm_draw_file_list] ".$where_sql." ";
$stmt1 = sqlsrv_query( $conn, $sql1 , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
$founded="No";$file_result="";
while( $row1 = sqlsrv_fetch_array( $stmt1, SQLSRV_FETCH_NUMERIC) ) {
    if (StrPos (" " . $row1[0], trim($row[2])) && $row[2]<>"" && $founded=="No") {$founded="Nomen";}  // nalezena nomenklatura 2
    if (StrPos (" " . $row1[0], trim($row[8])) && $row[8]<>"" && $founded<>"ZP") {$founded="Drawing";}  // nalezen 1
    if (StrPos (" " . $row1[0], trim($row[4])) && $row[4]<>"") {$founded="ZP";}  // nalezen ZP postup 0
$file_result= $row1[0];
}

//if ($founded=="ZP"){$mailbody.="<tr>
//    <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
//    <td>Zakázkový Postup je v souboru</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
//}        
 if ($founded=="Nomen"){$mailbody.="<tr>
    <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
    <td>Nomenklatura je v souboru</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
 }        
// ($founded=="Drawing"){$mailbody.="<tr>
//    <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
//    <td>Výkres je v souboru</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
//}        
 if ($founded=="No"){$mailbody.="<tr>
    <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
    <td>K ZP neexistuje žádný Soubor</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
 }
 
}sqlsrv_close($conn);

 echo $mailbody.="</Table>"; 
 
 // generovani souboru a ulozeni
if (!is_dir("./Data")) {mkdir ("./Data",0777);}
File_Exists("./Data/".date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0));
$f=fopen("./Data/".date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0),"w");
//$mailbody = iconv("utf-8", "windows-1250",mysql_escape_string($mailbody);
fwrite($f,$mailbody);fclose($f);
procedures_mail();
// konec generovani
?></form></fieldset></td></tr></table>
</div><?
}}?>


</td></tr></table></body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/epdm.js");
?>



<?}?>