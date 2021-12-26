<?if (@isset($_GET['usrname'])) {
require_once ("./functions/php/sessions.inc");
require_once ("./config/dbconnect.php");
require_once ("./functions/php/knihovna.php");

@$check = mysql_query("SELECT loginname FROM login where loginname like '".securesql($_GET['usrname'])."%' order by loginname ASC");

if (@mysql_num_rows(@$check) > 0) {$checkcolor="#66cc66";

@$panel ="<div id=usr_list style=position:relative;left:-256px;top:23px;border:1px;padding:10px;border-style:double;background:silver; ><select size=6 name=allusers style=width:226px;background:silver;font-size:13pt; ondblclick=this_user(this);document.forms[\"form1\"].submit(); onclick=this_user(this); >";
        @$load=0;while(@mysql_result($check,$load,0)):
$panel.="<option value=\'".mysql_result($check,$load,0)."\'>".mysql_result($check,$load,0)."</option>";
        @$load++;endwhile;
$panel.="</select></div>";


?>document.getElementById('checkusers').innerHTML = '<? echo @$panel; ?>';<?
} else {$checkcolor="#FFB0B0";?>

document.getElementById('new_user').src ='./images/add.png';
document.getElementById('new_user').style.width ='20px';document.getElementById('new_user').style.height ='20px';
document.getElementById('checkusers').innerHTML ='';
document.getElementById('lpassword').disabled =false;document.getElementById('lpassword1').disabled =false;
document.getElementById('lpassword').style.backgroundColor ='#FFFFC0';
<?}



if ($_GET['usrname'] == "") {$checkcolor="#FFB0B0";}
?>
function this_user(value){
    document.getElementById('selected_usr').value = value.options[value.selectedIndex].value;
}

document.getElementById('selected_usr').style.backgroundColor = '<? echo $checkcolor; ?>';
<?if (!@mysql_num_rows($check)){?>
document.getElementById('selected_usr').style.width ='230px';
<?}


}?>

if (document.all){
document.onkeydown = function (){    var key_esc = 27; 
if (key_esc==event.keyCode){document.getElementById('selected_usr').blur();document.getElementById('checkusers').innerHTML ='';}}}