<?if (@isset($_GET['value'])) {
require_once ("./functions/php/sessions.inc");
require_once ("./config/dbconnect.php");
require_once ("./functions/php/knihovna.php");

@$check = mysql_query("SELECT * FROM ".securesql($_GET['value2'])." where id='".securesql($_GET['value1'])."' order by position ASC");
$values=explode(",",@mysql_result($check,0,(int)$_GET['value3']));


if (@mysql_num_rows(@$check) > 0) {
@$panel ="<fieldset id=ram><legend id=ram_legenda><b>".dictionary("selection",$_SESSION["language"])."</b></legend><select size=6 name=allvalues style=width:200px;background:silver;font-size:13pt; ondblclick=this_value(this); >";
        @$loads=0;while(@$values[$loads]):
$panel.="<option value=\'".@$values[$loads]."\'>".@$values[$loads]."</option>";
        @$loads++;endwhile;
$panel.="</select><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=\"close_sel_win();\" ><img src=\'./images/ntick.png\' ></div></fieldset>";


?>document.getElementById('checkvalue').innerHTML = '<? echo @$panel; ?>';<?
}


?>

document.getElementById('<?echo $_GET['value'];?>').style.backgroundColor ='#B6EAB0';

function this_value(value){
    document.getElementById('<?echo $_GET['value'];?>').value = value.options[value.selectedIndex].value;
    document.getElementById('<?echo $_GET['value'];?>').style.backgroundColor ='#FFFFFF';
document.getElementById("checkvalue").style.display ='none';}

function close_sel_win(){
     document.getElementById('<?echo $_GET['value'];?>').style.backgroundColor ='#FFFFFF';
document.getElementById("checkvalue").style.display ='none';}



<?}?>