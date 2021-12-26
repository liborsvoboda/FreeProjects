<?php

if (isset($_GET['id'])) {
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

$htmlcode="";

//main setings loading
$control=mysql_query("select param from mainsettings where name='hotline_statuses' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($control,0,0));

//last status
@$search=0;while(@$part[($search+1)]):$search++;endwhile;
@$laststatus=@$part[$search];
// end of load main settings


//header + first record
$load_header=mysql_query("select * from hotline_request where document_no ='".securesql(@$_GET["id"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

//preparing data for new step
$previous_step=mysql_result($load_header,0,4)."|".datecs(mysql_result($load_header,0,5))."|".datecs(mysql_result($load_header,0,14))."|".mysql_result($load_header,0,15)."|".mysql_result($load_header,0,3)."|".mysql_result($load_header,0,13)."|".@$laststatus;

$htmlcode.='<SPAN style=width:100%;height:100%; ><table style=width:100%;height:100%;border:0px;border-collapse:collapse; ><tr style=width:100%;border:0px;background-color:#C5E0E6;><td>'.dictionary("title",$_SESSION["language"]).'</td><td style=text-align:left;width:100%; ><input type=text id=hist_value1 name=hist_value1 value="'.mysql_real_escape_string(mysql_result($load_header,0,1)).'" style=width:80%;font-weight:bold; disabled></td><td style=text-align:center;><font style=font-size:10px;>'.dictionary("request_to",$_SESSION["language"]).'</font><br /><input id=hist_value2 disabled name=hist_value2 type="text" value="'.datecs(mysql_result($load_header,0,5)).'" style=width:100px;text-align:center;font-weight:bold; ></td></tr><tr style=width:100%;border:0px;background-color:#C5E0E6;><td colspan=3 width=100% ><table style=width:100%; ><tr align=middle ><td style=width:25%;>'.dictionary("priority",$_SESSION["language"]).'<BR /><select size="1" name=hist_value3 disabled >';

    $load_form_data=mysql_query("select param from mainsettings where name=\"hotline_priorities\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $part=explode(",",mysql_result($load_form_data,0,0));
    $cycle=0;while($part[$cycle]<>""):
    $htmlcode.="<option value=\'".$part[$cycle]."\'";
    if (mysql_result($load_header,0,4)==$part[$cycle]) {$htmlcode.=" selected ";}
    $htmlcode.=" >".$part[$cycle]."</option>";
    $cycle++;endwhile;
    $htmlcode.='</select>';



$htmlcode.='</td><td style=width:25%;text-align:middle; >'.dictionary("status",$_SESSION["language"]).'<BR /><select '.access("1/2off").' size="1" name=hist_value4 disabled >';
    $load_form_data=mysql_query("select param from mainsettings where name=\"hotline_statuses\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $part=explode(",",mysql_result($load_form_data,0,0));
    $cycle=0;while($part[$cycle]<>""):
    $htmlcode.="<option value=\'".$part[$cycle]."\'";
        if (mysql_result($load_header,0,3)==$part[$cycle]) {$htmlcode.=" selected ";}
    $htmlcode.=" >".$part[$cycle]."</option>";
    $cycle++;endwhile;
    $htmlcode.='</select>';


$htmlcode.='</td><td style=width:25%;text-align:middle;vertical-align:top; >';
$htmlcode.=dictionary("attachments",$_SESSION["language"])."<BR />";
$load_form_data=mysql_query("select * from hotline_attachment where parent_no='".securesql(@$_GET["id"])."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $cycle=0;while(mysql_result($load_form_data,$cycle,0)):
    $htmlcode.='<a style=border:0px href="./ajax_functions.php?show_file=yes&tbl=hotline_attachment&id='.@mysql_result($load_form_data,$cycle,0).'" target=_blank ><image src="./images/attachment.png" border=0 width=24px height=24px title="'.@mysql_result($load_form_data,$cycle,2).'\r\n'.datetimedb_to_datecs(@mysql_result($load_form_data,$cycle,5)).'" /></a> ';
    $cycle++;endwhile;


$htmlcode.='</td><td style=width:25%;text-align:middle; >'.dictionary("score",$_SESSION["language"]).'<br /><select '.access("1/2off").' size="1" name=hist_value5 disabled >';
    $load_form_data=mysql_query("select param from mainsettings where name=\"score\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    $part=explode(",",mysql_result($load_form_data,0,0));
    $cycle=0;while($part[$cycle]<>""):
    $htmlcode.="<option value=\'".$part[$cycle]."\'";
        if (mysql_result($load_header,0,13)==$part[$cycle]) {$htmlcode.=" selected ";}
    $htmlcode.=" >".$part[$cycle]."</option>";
    $cycle++;endwhile;
    $htmlcode.='</select>';
$htmlcode.='</td></tr></table></td></tr>';

//records
$load_data=mysql_query("select * from hotline_history where parent_no ='".securesql(@$_GET["id"])."' order by id DESC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

$htmlcode.='<tr style=width:100%;height:100%; ><td colspan=3 ><div style=width:100%;height:100%;overflow-y:scroll;><hr /><table style=width:100%;border:0px;border-collapse:collapse; >';

$cycle=0;while(@mysql_result($load_data,$cycle,0)):

$htmlcode.='<tr class=`recordoff` onmouseout=`className="recordoff";` onmouseover=`className="recordon";` ><td style=vertical-align:top; ><b>'.dictionary("message",$_SESSION["language"]).'</b>';

// editing check

//if (((mysql_result($load_data,$cycle,4)<>$laststatus && @$_SESSION["lnamed"]==mysql_result($load_data,$cycle,11)) or @$_SESSION["sysadmin"]=="Yes") && !@mysql_result($load_data,($cycle-1),0)) {
//    $htmlcode.='<br><img id=hist_btn2 src="./images/edit.png" border="0" width="24" height="24" alt='.dictionary("editing",$_SESSION["language"]).' style=position:relative;left:0px;top:10px;cursor:pointer; onclick="editrequest(\"'.htmlspecialchars(mysql_result($load_data,$cycle,2)).'\",\"'.rawurlencode(htmlspecialchars(mysql_result($load_data,$cycle,3))).'\",\"'.mysql_result($load_data,$cycle,5).'\",\"'.datecs(mysql_result($load_data,$cycle,6)).'\",\"'.$attachment.'\",\"'.mysql_result($load_data,$cycle,1).'\",\"'.mysql_result($load_data,$cycle,4).'\",\"'.htmlspecialchars(mysql_result($load_data,$cycle,12)).'\",\"'.mysql_result($load_data,$cycle,13).'\",\"'.htmlspecialchars(mysql_result($load_data,$cycle,14)).'\",\"'.$laststatus.'\");" />';
//}
//
////delete
//if (((mysql_result($load_data,$cycle,4)==$part[0] && @$_SESSION["lnamed"]==mysql_result($load_data,$cycle,11)) or @$_SESSION["sysadmin"]=="Yes") && !@mysql_result($load_data,($cycle-1),0)) {
//$htmlcode.='<img id=hist_btn3 src="./images/delete.png" border="0" width="24" height="24" alt="'.dictionary("deleting",$_SESSION["language"]).'" style=position:relative;left:0px;top:10px;cursor:pointer; '.access("remove");
//$htmlcode.=' onclick=`if(confirm("'.dictionary("del_step",$_SESSION['language']).' : '.mysql_result($load_data,$cycle,0).'?")) del_request("'.base64_encode(mysql_result($load_data,$cycle,1)).'");` />';
//}
//

$htmlcode.="<div style=position:relative;left:0px;bottom:0px;height:100%;text-align:center; ><font style=font-size:10px;><span title=\"".dictionary("request_to",$_SESSION["language"])."\" >".datecs(mysql_result($load_data,$cycle,6))."<br/><span title=\"".dictionary("solution_to",$_SESSION["language"])."\" >".datetimedb_to_datecs(mysql_result($load_data,$cycle,14))."</span><br/><span title=\"".dictionary("priority",$_SESSION["language"])."\" >".mysql_result($load_data,$cycle,5)."<br/><span title=\"".dictionary("score",$_SESSION["language"])."\" >".mysql_result($load_data,$cycle,13)."<br/><span title=\"".dictionary("status",$_SESSION["language"])."\" >".mysql_result($load_data,$cycle,4)."<br/><span title=\"".dictionary("create_date",$_SESSION["language"])."\" >".datetimedb_to_datecs(mysql_result($load_data,$cycle,10))."<br/><span title=\"".dictionary("solves",$_SESSION["language"])."\" >".mysql_result($load_data,$cycle,15)."</font></div>";

$htmlcode.='</td><td style=width:100%; colspan=2 ><textarea rows="6" wrap="on" style=width:100%; onclick=select() readonly="readonly">'.mysql_real_escape_string(mysql_result($load_data,$cycle,3)).'</textarea></td></tr><tr><td colspan=3><hr /></td></tr>';

$cycle++;
endwhile;

$htmlcode.='</table></div></td></tr></table></span>';
if ((@mysql_result($load_header,0,10)==@$_SESSION["lnamed"] or @mysql_result($load_header,0,15)==@$_SESSION["lnamed"] or $_SESSION['sysadmin']=="Yes" ) && @mysql_result($load_header,0,3)<>@$laststatus){?>document.getElementById('addstep').disabled=false;document.getElementById('history').disabled=false;<?}
    else {?>document.getElementById('addstep').disabled=true;document.getElementById('history').disabled=true;<?}
?>
previous_step="<?echo $previous_step;?>";
document.getElementById('ht_request').innerHTML = '<? echo @$htmlcode; ?>';
<?}?>


