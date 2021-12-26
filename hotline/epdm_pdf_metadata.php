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
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('epdm_metadata_report',$_SESSION['language']);?>";
</script>
<link rel="icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon" />
<link rel="shortcut icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />
<script>
document.write('<DIV id="loading" onselectstart="return TRUE;" style=z-index:100;><BR><?echo dictionary("please_wait",$_SESSION["language"]);?><br /><img src="images/loading.gif" border="0"></DIV>');

function run_cmd(value,inputparms){
    activeX_Control();
try{
    
    var x = new ActiveXObject("WScript.Shell");    
    x.run('cmd /c "'+value+'"');
    }
    catch (e){
        alert ('<?echo dictionary("bad_command",$_SESSION["language"]);?>');
    }
    
}

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
<form id='form1' method='post' enctype="multipart/form-data">
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >
<div id='bookmark' >
<div style='position:absolute;align:left;left:20px;top:0px;' <? echo access("on/off");?> >
<span style="vertical-text-align:top;"><SELECT multiple name=pagesize[] size=2 >
    <option>A0</option><option>A1</option><option>A2</option><option>A3</option><option>A4</option><option>A5</option><option>OTHER</option>
</SELECT></span><span><input type=submit name=load_metafile value="<?echo dictionary("load_data",$_SESSION["language"]);?>" >
    <input type=submit name="run_scheduled_task" value="<?echo dictionary("metadata_export",$_SESSION["language"]);?>" >
</span>
</div>

<?
if (isset($_POST["run_scheduled_task"])){
    echo "<script>run_cmd('".htmlspecialchars(str_replace(chr(92),chr(92).chr(92),@mysql_result(mysql_query("select param from mainsettings where id=701 "),0,0)))."','');</script>";
    message(dictionary("will_be_infromed_by_email",$_SESSION["language"]));
}

?>
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:105%;' ><td style='width:100%;height:105%;' >
<fieldset id='ram'><legend id='ram_legenda'><b><?echo dictionary("report_page",$_SESSION["language"]);?></b></legend>
<table id=table_desc style=width:100% border=1 ><tr id=options>

<td><?echo dictionary("sequence",$_SESSION["language"]);?></td>
<td><?echo dictionary("filename",$_SESSION["language"]);?></td>
<td><?echo dictionary("pagesize",$_SESSION["language"]);?></td>
<td><?echo dictionary("pagesize",$_SESSION["language"]);?></td>
</tr>

<?

if (isset($_POST["pagesize"]) && isset($_POST["load_metafile"])){
    @$temp_data = explode("\n", file_get_contents(@mysql_result(mysql_query("select param from mainsettings where id=700 "),0,0), "r"));

$unit_no=1;    
$cycle=0; while(@$temp_data[$cycle]):
    if (strpos(@$temp_data[$cycle], "FileName:") !== false){
        $title = explode("FileName:",preg_replace('/\s+/', ' ', trim(@$temp_data[$cycle])));
        $size = explode("Page size:",preg_replace('/\s+/', ' ', trim(@$temp_data[$cycle+1])));
        $size_result=$size[1];
if (strpos($size[1],"595.28 x 419.53") !== false){ $size_result="A5";}
if (strpos($size[1],"595.276 x 841.89") !== false){ $size_result="A4";}
if (strpos($size[1],"595 x 842") !== false){ $size_result="A4";}
if (strpos($size[1],"841.89 x 595.28") !== false){ $size_result="A4";}
if (strpos($size[1],"841.89 x 595.276") !== false){ $size_result="A4";}
if (strpos($size[1],"841.92 x 595.2") !== false){ $size_result="A4";}
if (strpos($size[1],"595.08 x 841.68") !== false){ $size_result="A4";}
if (strpos($size[1],"842 x 595") !== false){ $size_result="A4";}
if (strpos($size[1],"842 x 596") !== false){ $size_result="A4";}
if (strpos($size[1],"579.12 x 812.4") !== false){ $size_result="A4";}
if (strpos($size[1],"595.2 x 841.92") !== false){ $size_result="A4";}
if (strpos($size[1],"579.12 x 810.24") !== false){ $size_result="A4";}
if (strpos($size[1],"1191 x 842") !== false){ $size_result="A3";}
if (strpos($size[1],"1224 x 792") !== false){ $size_result="A3";}
if (strpos($size[1],"1190.55 x 841.89") !== false){ $size_result="A3";}
if (strpos($size[1],"842 x 1191") !== false){ $size_result="A3";}
if (strpos($size[1],"1191 x 1684") !== false){ $size_result="A2";}
if (strpos($size[1],"1683.78 x 1190.55") !== false){ $size_result="A2";}
if (strpos($size[1],"2383.94 x 1683.78") !== false){ $size_result="A1";}
if (strpos($size[1],"3370.39 x 2383.94") !== false){ $size_result="A0";}



        foreach ($_POST["pagesize"] as $selectedOption){
            IF ($selectedOption == $size_result || ($selectedOption == "OTHER" AND $size_result[0] <> "A")) {
                echo "<tr>";
                echo "<td>".$unit_no."</td>";
                echo "<td style=cursor:pointer; onclick=window.open('file://server5/EPDM_PDF/EPDM_CMP/".preg_replace('/\s+/', ' ', trim($title[1]))."'); >".$title[1]."</td>";
                echo "<td>".$size_result."</td>";
                echo "<td>".$size[1]."</td></tr>";
                $unit_no++;
            }
            
        }
        $cycle = $cycle+2 ;
                
    } 
    else {$cycle++;}

endwhile;
}
?>

</table>
</fieldset>
</td></tr></table>
</div>

</td></tr></form></table></body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
?>
<script>document.getElementById("loading").style.display="none";</script>


<?}?>