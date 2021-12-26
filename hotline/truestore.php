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
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('truestore',$_SESSION['language']);?>";
</script>
<link rel="icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon" />
<link rel="shortcut icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />

<script>
document.write('<DIV id="loading" onselectstart="return TRUE;" style=z-index:100;><BR><?echo dictionary("please_wait",$_SESSION["language"]);?><br /><img src="images/loading.gif" border="0"></DIV>');
    document.getElementById("loading").style.display="none";
    
    function activeX_Control()
    {
        try{objShell = new ActiveXObject("WScript.Shell");}
    catch (e){
        alert ('<?echo dictionary("activeX_is_disabled",$_SESSION["language"]);?>');
    }
} 

activeX_Control();


</script>


</head>


<Body  >

<table id='fullframetable' >
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<div id='bookmark' >

<div style='position:absolute;align:left;left:20px;top:5px;' <? echo access("on/off");?> >
<span style='cursor:pointer;' onclick='reload_page();' class="purpleout" onmouseout="className='purpleout';" onmouseover="className='purplein';" title='<?echo dictionary("refresh",$_SESSION["language"]);?>' ><img src='./images/list.png' width='20' height='20' style='vertical-align:middle;border:0px' /> <?echo dictionary("refresh",$_SESSION["language"]);?></span>
</div>

<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:105%;' ><td style='width:100%;height:105%;' >
<fieldset id='ram'><form id='form1' method='post' enctype="multipart/form-data"><legend id='ram_legenda'><b><?echo dictionary("report_page",$_SESSION["language"]);?></b></legend>
 
<b><?echo dictionary("report_generated",$_SESSION["language"]).": ".$dnest;?></b>
<table id=table_desc style=width:100%><tr id=options>
<td><?echo dictionary("pallet_no",$_SESSION["language"]);?></td>
<td><?echo dictionary("material_no",$_SESSION["language"]);?></td>
<td><?echo dictionary("description",$_SESSION["language"]);?></td>
<td><?echo dictionary("quantity_pcs",$_SESSION["language"]);?></td>
<td><?echo dictionary("loading_sheet",$_SESSION["language"]);?></td>
</tr>




<?
$WshShell = new COM("WScript.Shell"); 
$oExec = $WshShell->Run('"'.mysql_result(mysql_query("select param from mainsettings where id=600 "),0,0).'"', 0, true);




@$temp_data = explode("\n", file_get_contents("./temp/TruStore.cli", "r"));
$master_start=false;$stock_start=false;

$cycle=0; while(@$temp_data[$cycle]):
    if ($master_start ==true){$master_data[$counter]=@$temp_data[$cycle];$counter++;}
    
    //WRITELINE
    if ($stock_start ==true){
    $stock_detail = explode("|", @$temp_data[$cycle]);
    
    $cycle1=0; while (@$master_data[$cycle1]):
    $master_detail = explode("|", $master_data[$cycle1]);
    if (string_search($master_detail[1],$stock_detail[1])==true ){
        echo "<tr>";
        echo "<td>".$stock_detail[5]."</td>";
        echo "<td>".mysql_result(mysql_query("select param from mainsettings where id=601 "),0,0).$master_detail[1]."</td>";
        echo "<td>".$master_detail[11]."</td>";
        echo "<td>".$stock_detail[15]."</td>";
        echo "<td>".$stock_detail[39]."</td>";
        echo "</tr>";
        }
        
        
    $cycle1++; endwhile;
      
    }

    if (string_search(@$temp_data[$cycle],"<MASTER DATA>") ==true){$master_start=true;$counter=0;}
    if (string_search(@$temp_data[$cycle],"</MASTER DATA>")==true){$master_start=false;}
    if (string_search(@$temp_data[$cycle],"<STOCKS>") ==true){$stock_start=true;}
    if (string_search(@$temp_data[$cycle],"</STOCKS>")==true){$stock_start=false;}
    $cycle++;
endwhile;



$d=explode("|", $stock_data[$counter]);
if (StrPos (" " . $a, "-") and $a){}


?>

</table>
 
</form></fieldset></td></tr></table>
</div>

</td></tr></table></body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
?>



<?}?>