<?php
require_once ("./Auto_conf_files/dbconnect.php");
require_once ("./Auto_conf_files/knihovna.php");
require_once ("./config/mssql_dbconnect.php");

$mailbody="<table><tr>
<td>".dictionary("opv","lang_cs")."</td>
<td>".dictionary("nomenklatura","lang_cs")."</td>
<td>".dictionary("description","lang_cs")."</td>
<td>".dictionary("procedure","lang_cs")."</td>
<td>".dictionary("catalogue_number","lang_cs")."</td>
<td>".dictionary("drawing_no","lang_cs")."</td>
<td>".dictionary("status","lang_cs")."</td>
<td>".dictionary("file","lang_cs")."</td>
<td>".dictionary("terminzac","lang_cs")."</td>
</tr>";

$founded="No";
$line=0;

$data_file = @mysql_result(mysql_query("select param from mainsettings where name='epdm_source_file_path' "),0,0);
$openedfile = file_get_contents($data_file);

//upload file list
 $records = explode("\r\n", $openedfile); //nacteni souboru
 $cycle=7;while($records[($cycle+2)]):
  $file_name=substr($records[$cycle],38,(strlen($records[$cycle])-37));
    IF ($file_name[0]==" "){$file_name=substr($file_name,1,(strlen($file_name)-1));}
  $sql = "SELECT * FROM dbo.[10_epdm_draw_file_list] where file_name like '".$file_name."%' ";
  $stmt = sqlsrv_query( $conn, $sql , $params, $options );
   if (sqlsrv_num_rows($stmt)==0) {
    $sql_insert = "insert into dbo.[10_epdm_draw_file_list] (file_name)VALUES('".securesql($file_name)."') ";
    $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options );
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
    $stmt = sqlsrv_query( $conn, $sql , $params, $options );
while( $row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_BOTH) ) {

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
$stmt1 = sqlsrv_query( $conn, $sql1 , $params, $options );
$founded="No";$file_result="";
while( $row1 = sqlsrv_fetch_array( $stmt1, SQLSRV_FETCH_NUMERIC) ) {
    if (StrPos (" " . $row1[0], trim($row[2])) && $row[2]<>"" && $founded=="No") {$founded="Nomen";}  // nalezena nomenklatura 2
    if (StrPos (" " . $row1[0], trim($row[8])) && $row[8]<>"" && $founded<>"ZP") {$founded="Drawing";}  // nalezen 1
    if (StrPos (" " . $row1[0], trim($row[4])) && $row[4]<>"") {$founded="ZP";}  // nalezen ZP postup 0
$file_result= $row1[0];
}

// if ($founded=="ZP"){$mailbody.="<tr>
//     <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
//     <td>Zakázkový Postup je v souboru</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
//}
 if ($founded=="Nomen"){$mailbody.="<tr>
     <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
     <td>Nomenklatura je v souboru</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
 }
// if ($founded=="Drawing"){$mailbody.="<tr>
//     <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
//     <td>Výkres je v souboru</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
//}
 if ($founded=="No"){$mailbody.="<tr>
     <td>".$row[1]."</td><td>".$row[2]."</td><td>".$row[3]."</td><td>".$row[4]."</td><td>".$row[7]."</td><td>".$row[8]."</td>
     <td>K ZP neexistuje žádný Soubor</td><td>".$file_result."</td><td>".datecs(substr($row['terminzac'],0,10))."</td></tr>";
 }

}sqlsrv_close($conn);

 $mailbody.="</Table>";

 // generovani souboru a ulozeni
if (!is_dir("./Data")) {mkdir ("./Data",0777);}
File_Exists("./Data/".date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0));
$f=fopen("./Data/".date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0),"w");
//$mailbody = iconv("utf-8", "windows-1250",mysql_escape_string($mailbody);
fwrite($f,$mailbody);fclose($f);
procedures_mail();

// konec generovani

//<script type='text/javascript'>
//window.open('', '_self', ''); window.close();
//</script>
?>

