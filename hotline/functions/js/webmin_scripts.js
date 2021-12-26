<SCRIPT LANGUAGE="JavaScript">

document.write('<DIV id=checklanguage style=position:absolute;width:200px;left:50%;top:25%;margin-left:-100px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("language_selection",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post><?$load_data=mysql_query("select * from webmin_languages where position>1 order by id ASC");$load=0;while(@mysql_result($load_data,$load,0)):$control=mysql_query("SHOW COLUMNS FROM webmin_dictionary");$result="";$ctrl=0;while(mysql_result($control,$ctrl,0)):if (mysql_result($control,$ctrl,0)==@mysql_result($load_data,$load,2)){$result="checked";}$ctrl++;endwhile;echo"<tr onclick=\"selected_lang(\'".@mysql_result($load_data,$load,2)."\',\'".$result."\');\" class=\"purpleslrout\" onmouseout=\"className=\'purpleslrout\';\" onmouseover=\"className=\'purpleslrin\';\" ><td><input name=value".($load+1)." type=checkbox ".$result." ></td><td style=width:100%; >".dictionary(@mysql_result($load_data,$load,2),$_SESSION["language"])."</td></tr>";$load++;endwhile;?></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("checklanguage").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');
document.getElementById("checklanguage").style.display="none";

function checklanguage() {
if (document.getElementById("checklanguage").style.display!="none") {document.getElementById("checklanguage").style.display="none";}
	else {document.getElementById("checklanguage").style.display="inline";}
}

function selected_lang(value,value1) {
if (value1!="checked") {if(confirm('<?echo dictionary("add_new_lang",$_SESSION['language']);?>')) add_lang(value);}
if (value1=="checked") {if(confirm('<?echo dictionary("remove_lang",$_SESSION['language']);?>')) rem_lang(value);}
}

function add_lang(value){
script=document.createElement('script');
script.src='./ajax_functions.php?addlang='+value;
document.getElementsByTagName('head')[0].appendChild(script);
window.location.href='<?echo $_SERVER["REQUEST_URI"];?>';
}

function rem_lang(value){
script=document.createElement('script');
script.src='./ajax_functions.php?dellang='+value;
document.getElementsByTagName('head')[0].appendChild(script);
window.location.href='<?echo $_SERVER["REQUEST_URI"];?>';
}



function add_record(){
document.getElementById("act").disabled=false;
document.getElementById("act").value="add";
document.getElementById('form1').submit();
}

function del_record(value){
document.getElementById("act").disabled=false;
document.getElementById("act").value=value;
document.getElementById('form1').submit();
}

function del_icon(value,table){
script=document.createElement('script');
script.src='./ajax_functions.php?table='+table+'&del='+value;
document.getElementsByTagName('head')[0].appendChild(script);
window.location.href='<?echo $_SERVER["REQUEST_URI"];?>';
}

if (document.all){
document.onkeydown = function (){    var key_esc = 27;
if (key_esc==event.keyCode){
	back();
}}}

function back(){
	if (document.getElementById("checklanguage")) {document.getElementById('checklanguage').style.display="none";}
	if (document.getElementById("newmenuitem")) {document.getElementById('newmenuitem').style.display="none";}
	if (document.getElementById("editmenuitem")) {document.getElementById('editmenuitem').style.display="none";}
	if (document.getElementById("editor")) {document.getElementById("editor").style.display="none";}
	if (document.getElementById("newmenubutton")) {	document.getElementById('newmenubutton').style.display="inline";}
	if (document.getElementById("editmenu")) {document.getElementById("editmenu").style.display="none";}
 	if (document.getElementById("table_desc")) {document.getElementById("table_desc").style.display = 'inline';}
	if (document.getElementById("menuname")) {document.getElementById("menuname").innerHTML = '';}
	if (document.getElementById("delmenu")) {document.getElementById("delmenu").style.display="none";}
	if (document.getElementById("submenu")) {document.getElementById("submenu").style.display="none";}
	if (document.getElementById("back")) {document.getElementById("back").style.display="none";}
    if (document.getElementById("newsubmenuitem")) {document.getElementById("newsubmenuitem").style.display="none";}
    if (document.getElementById("newsubmenu")) {document.getElementById("newsubmenu").style.display="none";}
	if (document.getElementById("rec_id")) {document.getElementById("rec_id").value = '';}
	if (document.getElementById("editsubmenuitem")) {document.getElementById('editsubmenuitem').style.display="none";}
	if (document.getElementById("editsubmenu")) {document.getElementById("editsubmenu").style.display="none";}
	if (document.getElementById("savebtn") && "<?echo @$_REQUEST["option"];?>"=="menu_builder") {document.getElementById("savebtn").style.display="none";}
	if (document.getElementById("newphoto")) {document.getElementById("newphoto").style.display="none";}
	if (document.getElementById("editphoto")) {document.getElementById("editphoto").style.display="none";}
	if (document.getElementById("langmenu")) {document.getElementById("langmenu").style.display="none";}
	if (document.getElementById("newlink")) {document.getElementById("newlink").style.display="none";}
	if (document.getElementById("editlink")) {document.getElementById("editlink").style.display="none";}
	if (document.getElementById("checkvalue")) {document.getElementById("checkvalue").style.display="none";}
}

</script>

