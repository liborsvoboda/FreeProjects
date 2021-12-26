<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

if (@$_SESSION["lnamed"]) {
require_once ("./functions/php/whois.php");

?>

<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('karat_conf_reporting',$_SESSION['language']);?>";
</script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/karat.css' />


<? //saving

//preparing records table
$result=mysql_query("select ip_address from karat_conf_ip_list where domain ='' group by ip_address ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$cycle=0;while(@mysql_result($result,$cycle,0)):
    $counter_domain = 'http://domains.yougetsignal.com/domains.php?remoteAddress=' . $counter_ip_value . '&key=';
    $json = file_get_contents($counter_domain);$counter_domain="";
    if (preg_match_all('/\["(.*?)",/i', $json, $match)) {
    //    echo count($match[1]);
        foreach ($match[1] as $list) {
            $counter_domain.=$list.",";
        }
    } 
 mysql_query("UPDATE karat_conf_ip_list SET domain = '".securesql($counter_domain)."' where ip_address= '".securesql(@mysql_result($result,$cycle,0))."' and domain='' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$cycle++;endwhile;
//end off preparing records table









// end of saving
?>

</head>


<body>
<table id='fullframetable' onselectstart="return false;" >
<tr style='width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >
<?
$load_data=mysql_query("select * from karat_conf_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):
$checked_menu=0;
        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,2) or (!@$_REQUEST["option"] && $load==0)) {echo " class=\"bookmarkin\" ";$checked_menu=1;}
        if ($checked_menu<>1 && @$_REQUEST["option"]<>mysql_result($load_data,$load,2) ) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./karat_conf_reporting.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " >".dictionary(mysql_result($load_data,$load,2),$_SESSION["language"])."</span>";

$load++;endwhile;?></td></tr>
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >





<?if (!@$_REQUEST["option"] or @$_REQUEST["option"]=="karat_conf_visitors") { // karat_conf_reporting
?><div id='bookmark' >
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:100%;'><td style='width:100%;height:100%;' >

<form action='<?echo $_SERVER["PHP_SELF"];?>' id='form1' name='form1' method='post' enctype="multipart/form-data">

<?echo
    "<table id='table_desc' style='width:100%;height:100%;' >
    <tr>
    <td style='width:25%;' ><H2>".dictionary("traffic",$_SESSION["language"])."</H2>
    <p id='linespace' ></p>
    ".dictionary("from_date",$_SESSION["language"]).": <select id=s_value1 name=s_value1 style=width:60%; size=1>";
    echo "<option></option>";
    @$load_data = mysql_query("select DATE(visit_date) from karat_conf_ip_list group by DATE(visit_date) order by DATE(visit_date) ASC ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $cykl=0;while (@mysql_result($load_data,$cykl,0)):
    echo "<option value='".@mysql_result($load_data,$cykl,0)."'>".datecs(@mysql_result($load_data,$cykl,0))."</option>";
    $cykl++;endwhile;
    echo "</select>
    <p id='linespace' ></p>
    ".dictionary("to_date",$_SESSION["language"]).": <select id=s_value2 name=s_value2 style=width:60%; size=1>";
    echo "<option></option>";
    @$load_data = mysql_query("select DATE(visit_date) from karat_conf_ip_list group by DATE(visit_date) order by DATE(visit_date) DESC ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $cykl=0;while (@mysql_result($load_data,$cykl,0)):
    echo "<option value='".@mysql_result($load_data,$cykl,0)."'>".datecs(@mysql_result($load_data,$cykl,0))."</option>";
    $cykl++;endwhile;
    echo "</select>
    <p id='linespace' ></p>
    ".dictionary("interval",$_SESSION["language"]).": <select id=s_value3 name=s_value3 style=width:60%; size=1>
    <option></option>
    <option value='hourly' >".dictionary("hourly",$_SESSION["language"])."</option>
    <option value='daily' >".dictionary("daily",$_SESSION["language"])."</option>
    <option value='weekly' >".dictionary("weekly",$_SESSION["language"])."</option>
    <option value='monthly' >".dictionary("monthly",$_SESSION["language"])."</option>
    <option value='yearly' >".dictionary("yearly",$_SESSION["language"])."</option>
    </select>
    <p id='linespace' ></p>
    <input type=button style='' onclick=load_graph('img_graph','s_value'); name=btn_value3 value='".dictionary("display_graph",$_SESSION["language"])."'>
    </td>
<td style='width:75%;' rowspan=2 >";

echo "<img id=img_graph src='' >";
@$load_data=mysql_query("select domain from karat_conf_ip_list group by ip_address,domain order by ip_address,domain") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
echo "</td></tr><tr><td style=vertical-align:bottom;text-align:center; >
<select id=sel_value1 onclick=document.getElementById('btn_value1').disabled=false;document.getElementById('btn_value2').disabled=false; name=sel_value1 size=25 style=width:100%;vertical-align:middle; >";
$cykl=0;while(@mysql_result(@$load_data,$cykl,0)):
    $temp_data=explode (",", @mysql_result($load_data,$cykl,0));
        $temp_cykl=0;while(@$temp_data[$temp_cykl]):
            echo"<option value='".@$temp_data[$temp_cykl]."' >".@$temp_data[$temp_cykl]."</option>";
        $temp_cykl++;endwhile;
$cykl++;endwhile;echo "</select>";

echo "<p id='linespace' ></p>
<input type='button' disabled id='btn_value1' onclick=open_website('sel_value1') name='btn_value1' value='".dictionary('open_site',$_SESSION['language'])."' style=width:48%;>
<input type='button' disabled id='btn_value2' onclick=load_whois('sel_value1') value='".dictionary('domain_owner',$_SESSION['language'])."' style=width:48%;>
</td></tr>
</table>";

?>
</form></td></tr></table>
</div><?
}?>







<?if (!@$_REQUEST["option"] or @$_REQUEST["option"]=="karat_report_graph") { // grafy
?><div id='bookmark' >
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:100%;'><td style='width:100%;height:100%;' >
<form action='<?echo $_SERVER["PHP_SELF"];?>' id='form1' name='form1' method='post' enctype="multipart/form-data">

</form></td></tr></table>
</div><?
}?>




</td></tr></table></body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/karat_conf_reporting.js");
?>




<?}?>