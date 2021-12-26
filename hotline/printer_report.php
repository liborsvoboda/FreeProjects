<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

if (@$_SESSION["lnamed"]) {?>
<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('printer_report_form',$_SESSION['language']);?>";
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


<? // saving

if (@isset($_POST['value3'])) {
   
$data=explode("+:+", $_POST['value3']);
$field_id=explode("counter",$data[0]);

$result=mysql_query("update printer_properties set counter='".securesql($data[1])."', update_date='".$dnest."', updator='".securesql(@$_SESSION["lnamed"])."' where id='".securesql($field_id[1])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());    
if ($result==1) {savechangesuccess("");}
}


// end of saving

?>

</head>


<Body onselectstart="return false;" style=overflow-y:hidden >
<table id='fullframetable' ><form id='form1' name="form1" method='post' enctype="multipart/form-data">
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


// check of date 
$data_file = @mysql_result(mysql_query("select param from mainsettings where name='printer_source_file_path' "),0,0);
$openedfile = file_get_contents($data_file);

$records = explode("\r\n", $openedfile); //nacteni souboru
 $cycle=0;while($records[$cycle]):
        $fields= explode (",",$records[$cycle]);
        $cycle1=0;while($fields[$cycle1]):

//printer control

if ($cycle==0){
    if (@$cycle1<>0) {
        $value=str_replace("\\\server4\Print Queue(","",str_replace(")\Total Pages Printed","",str_replace("\"","",$fields[$cycle1])));
        $printer_name[$cycle1]=$value;
    }

  $table_control = mysql_result(mysql_query("show COLUMNS  from printers_data like '".securesql($value)."' "),0,0);
    if (!@$table_control){
        mysql_query("ALTER TABLE printers_data ADD COLUMN `".securesql($value)."` INT(10) NOT NULL ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
        mysql_query("insert into printer_properties (name,create_date,creator,update_date,updator)VALUES('".securesql($value)."','".$dnest."','".securesql(@$_SESSION["lnamed"])."','".$dnest."','".securesql(@$_SESSION["lnamed"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    }
} else {
    
 if ($cycle1==0){ //create report empy record
    @$curr_date = explode (" ", $fields[0]);
    $temp_time = explode(" " ,$fields[$cycle1]);
    $temp_time = explode(".",$temp_time[1]);
    $report_time=datedb(sysdate(str_replace("\"","" ,@$curr_date[0])))." ".$temp_time[0];
    
$control=mysql_result(mysql_query("select id from printers_data where report_date='".securesql($report_time)."'"),0,0);
if (!$control) {mysql_query("insert into printers_data (report_date,create_date,creator)VALUES('".$report_time."','".$dnest."','".securesql(@$_SESSION["lnamed"])."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

}

 } else {
$result=mysql_query("update printers_data set `$printer_name[$cycle1]` = '".securesql(str_replace("\"","" ,$fields[$cycle1]))."' where report_date='".securesql($report_time)."'  ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
 } 
}
            
  $cycle1++;
    endwhile;
    $mailbody.="</tr>";
 
 $cycle++;
 endwhile;  
$mailbody.="</table>";
// end of check  date

    $table_control = mysql_query("show COLUMNS from printers_data ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $printer_properties = mysql_query("select * from printer_properties ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load_data=mysql_query("select * from printers_data order by ID ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load_date =mysql_query("select date(report_date) from printers_data group by date(report_date) order by date(report_date) ASC ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

?><div id='bookmark' >
<div style=position:absolute;align:right;left:22px;top:5px; align=right>
<span style='cursor:pointer;' onclick="window.open('./Data/<?echo mysql_result(mysql_query("select param from mainsettings where name ='printer_file_name' "),0,0);?>')" class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("xls_report",$_SESSION["language"]);?>"' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("xls_report",$_SESSION["language"]);?></span>

<span style=width:40px; ></span>
<? echo dictionary("from_date",$_SESSION["language"])." ";?>
<select size="1" id="from_date" name="value1" onchange=check_date() >
<?echo "<option></option>";
$wr=0;while(mysql_result($load_date,$wr,0)):
    echo "<option value='".datecs(@mysql_result($load_date,$wr,0))."'";
        if (@$_POST["value1"]==datecs(@mysql_result($load_date,$wr,0))) {echo " selected ";}
    echo ">".datecs(@mysql_result($load_date,$wr,0))."</option>";
@$wr++;endwhile;?>
</select><span style=width:40px; ></span>

<? echo dictionary("to_date",$_SESSION["language"])." ";?>
<select size="1" id="to_date" name="value2" onchange=submit(this) >
<?echo "<option></option>";
$wr=0;$check=0;while(mysql_result($load_date,$wr,0)):
    echo "<option value='".datecs(@mysql_result($load_date,$wr,0))."'";
        if (@$_POST["value2"]==datecs(@mysql_result($load_date,$wr,0)) && @$_POST["value2"]) {echo " selected ";}
    echo ">".datecs(@mysql_result($load_date,$wr,0))."</option>";
@$wr++;endwhile;
if (@$_POST["value1"] && @$_POST["value2"]) {echo "<script>check_date();</script>";}

?>
</select><span style=width:40px; ></span>
<input id=savebtn1 type='button' disabled value='<?echo dictionary("gen_report",$_SESSION["language"]);?>' onclick="submit(this)" style='padding:0 10 0 10px;font-weight:bold;font-size:13px;'></div>

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:105%;' ><td style='width:100%;height:105%;' >
<fieldset id='ram'><legend id='ram_legenda'><b><?echo dictionary("printer_report_form",$_SESSION["language"]);?></b></legend>
<?

//header
$filebody="<table id=table_desc><tr id=options><td>".dictionary("datetime",$_SESSION["language"])."</td><td>".dictionary("date",$_SESSION["language"])."</td>";
$cycle=4;while(mysql_result($table_control,$cycle,0)):
//xxxx
    $filebody.="<td>".mysql_result($table_control,$cycle,0)."<br/><span style=width:100%;background-color:orange; title='".dictionary("counter_status",$_SESSION["language"])."\r\n".dictionary("engaged_quantity",$_SESSION["language"]).": ".mysql_result($printer_properties,($cycle-4),4)."\r\n".dictionary("to_date",$_SESSION["language"]).": ".datetimedb_to_datecs(mysql_result($printer_properties,($cycle-4),5))."' ondblclick=activate_field('counter".($cycle-3)."'); ><input type=text id=counter".($cycle-3)." name=value3 value='".mysql_result($printer_properties,($cycle-4),4)."' onchange=submit_field(\"form1\",\"counter".($cycle-3)."\"); style=text-align:center;width:100%; ></span></td>";
$cycle++;
endwhile;$filebody.="</tr><tr>";

$summary[1]["total"]=dictionary("total",$_SESSION["language"]);

// body
$where="";
if (@$_POST["value1"] && @$_POST["value2"]) {
    $where=" where date(report_date) >= '".securesql(datedb(@$_POST["value1"]))."' and  date(report_date) <= '".securesql(datedb(@$_POST["value2"]))."' ";            
}
if (@$_POST["value1"] && !@$_POST["value2"]) {
    $where=" where date(report_date) >= '".securesql(datedb(@$_POST["value1"]))."' ";    
}
if (!@$_POST["value1"] && @$_POST["value2"]) {
    $where=" where date(report_date) <= '".securesql(datedb(@$_POST["value2"]))."' ";    
}
 
                 
$load_data=mysql_query("select * from printers_data ".$where." order by id ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

      
$cycle=0;while(mysql_result($load_data,$cycle,0)):
    $cycle1=0;while($cycle1<mysql_num_rows($table_control)):
        if ($cycle1==1 or $cycle1>3){
            $filebody.="<td";
                if (mysql_result($load_data,($cycle),$cycle1) < mysql_result($load_data,($cycle-1),$cycle1) && @mysql_result($load_data,$cycle,0)){$filebody.= " style=background-color:red ";}
            $filebody.=" >";
        if ($cycle1==1) {$filebody.=datetcs(mysql_result($load_data,$cycle,$cycle1))."</td><td>".datetimedb_to_datecs(mysql_result($load_data,$cycle,$cycle1));}
        else {$filebody.=mysql_result($load_data,$cycle,$cycle1);
        
        // ulozeni pocatecni hodnody 
        if ($cycle==0){$summary[$cycle1]["start"]=mysql_result($load_data,$cycle,$cycle1);}
        
            if (mysql_result($load_data,($cycle+1),$cycle1) < mysql_result($load_data,$cycle,$cycle1) && @mysql_result($load_data,($cycle+1),0)){
                $summary[$cycle1]["total"]=$summary[$cycle1]["total"]+ (mysql_result($load_data,$cycle,$cycle1)-$summary[$cycle1]["start"]);
            // ulozeni hodnody po resetu  = reset je chyba hodnota je nastavena na 0    
            //$summary[$cycle1]["start"]=mysql_result($load_data,($cycle+1),$cycle1);
            $summary[$cycle1]["start"]=0;
            }    
            if (!mysql_result($load_data,($cycle+1),0)){
                if ($summary[$cycle1]["total"]==""){$summary[$cycle1]["total"]=$summary[$cycle1]["total"]+mysql_result($load_data,$cycle,$cycle1)-$summary[$cycle1]["start"];
                } else {$summary[$cycle1]["total"]=$summary[$cycle1]["total"]+mysql_result($load_data,$cycle,$cycle1);}
            }
            

// odpocet pocitadla       
if (@datetimedb_to_datecs(@mysql_result($printer_properties,($cycle1-4),5)) <= datetimedb_to_datecs(mysql_result($load_data,$cycle,1)) )
{
    if (@$counter[($cycle1-3)]["start"]=="") {
        //$counter[$cycle1]["start"]=mysql_result($load_data,$cycle,$cycle1);
        $counter[($cycle1-3)]["start"]=mysql_result($printer_properties,($cycle1-4),4);

        // start value        
        if (mysql_result($load_data,($cycle-1),$cycle1) > mysql_result($load_data,$cycle,$cycle1) ) {
            $counter[($cycle1-3)]["start"]=$counter[($cycle1-3)]["start"] + mysql_result($load_data,$cycle,$cycle1);
        } else {$counter[($cycle1-3)]["start"]=$counter[($cycle1-3)]["start"] + (mysql_result($load_data,$cycle,$cycle1) - mysql_result($load_data,($cycle-1),$cycle1));
        }
    } else {
        
        if (mysql_result($load_data,($cycle-1),$cycle1) > mysql_result($load_data,$cycle,$cycle1)) {
           $counter[($cycle1-3)]["start"]=$counter[($cycle1-3)]["start"]+mysql_result($load_data,$cycle,$cycle1);        
        }
        
        if (mysql_result($load_data,($cycle-1),$cycle1) < mysql_result($load_data,$cycle,$cycle1) && @mysql_result($load_data,($cycle+1),$cycle1)) {             
           $counter[($cycle1-3)]["start"]=$counter[($cycle1-3)]["start"]+(mysql_result($load_data,$cycle,$cycle1) - mysql_result($load_data,($cycle-1),$cycle1));
        }
        
    }
}
// konec odpotu po�itadla
            
            
        }
  
        $filebody.="</td>";
    }
    $cycle1++;
    endwhile;
$filebody.="</tr><tr>";
$cycle++;
endwhile;

$filebody.="<td>".$summary[1]["total"]."</td><td></td>";
$counter_result="</tr><tr><td>".dictionary("actual_status",$_SESSION["language"])."</td><td></td>";
$cycle=4;while($cycle<mysql_num_rows($table_control)):

    $filebody.="<td>".$summary[$cycle]["total"]."</td>";
$counter_result.="<td>".$counter[($cycle-3)]["start"]."</td>";    

  $cycle++;
endwhile;
  $filebody.=$counter_result."</tr></table>";

  
    
if (!empty($_POST)){
 // generovani souboru a ulozeni
if (!is_dir("./Data")) {mkdir ("./Data",0777);}
File_Exists("./Data/".mysql_result(mysql_query("select param from mainsettings where name ='printer_file_name' "),0,0));
$f=fopen("./Data/".mysql_result(mysql_query("select param from mainsettings where name ='printer_file_name' "),0,0),"w");
//$mailbody = iconv("utf-8", "windows-1250",mysql_escape_string($mailbody);
fwrite($f,$filebody);fclose($f);
// konec generovani

    
}
echo $filebody;
$cycle=4;while($cycle<mysql_num_rows($table_control)):
    echo "<script>document.getElementById('counter".($cycle-3)."').value='".$counter[($cycle-3)]["start"]."';
    document.getElementById('counter".($cycle-3)."').disabled=true;
    </script>";
  $cycle++;
endwhile;


}?>


</td></tr></table></body>
</html>


<?
require_once ("./functions/js/printer.js");
require_once ("./functions/js/standard_scripts.js");

?>


<?}?>

