<SCRIPT LANGUAGE="JavaScript">

document.write('<DIV id=newphoto style=position:absolute;width:400px;left:50%;top:25%;margin-left:-200px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("add_photo",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("sequence",$_SESSION["language"]);?></td><td><input name=value1 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input name=value2 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("menu_name",$_SESSION["language"]);?></td><td><select size="1" name=value3 style=width:200px ><?$load_form_data=mysql_query("select id,name from www_menu where submenu_parent_id=0 order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$formload=0;while($formload<mysql_num_rows($load_form_data)):echo "<option value=\'".mysql_result($load_form_data,$formload,0)."\'>".mysql_result($load_form_data,$formload,1)."</option>";$formload++;endwhile;?></select></td></tr><tr><td><?echo dictionary("photo",$_SESSION["language"]);?></td><td><input name=value4 type=file accept="image/*" style=width:200px ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input name=formsavebtn4 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("newphoto").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');

document.write('<DIV id=editphoto style=position:absolute;width:400px;left:50%;top:25%;margin-left:-200px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("edit_photo",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("sequence",$_SESSION["language"]);?></td><td><input id=v1 name=value1 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input id=v2 name=value2 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("menu_name",$_SESSION["language"]);?></td><td><select size="1" id=v3 name=value3 style=width:200px ><?$load_form_data=mysql_query("select id,name from www_menu where submenu_parent_id=0 order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$formload=0;while($formload<mysql_num_rows($load_form_data)):echo "<option value=\'".mysql_result($load_form_data,$formload,0)."\'>".mysql_result($load_form_data,$formload,1)."</option>";$formload++;endwhile;?></select></td></tr><tr><td><?echo dictionary("photo",$_SESSION["language"]);?></td><td><input name=value4 type=file accept="image/*" style=width:200px ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=v100 name=value100 type="hidden" value=""><input name=formsavebtn5 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("editphoto").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');
document.getElementById("newphoto").style.display="none";
document.getElementById("editphoto").style.display="none";



function addphoto(){        // zobrazeni newphoto
if (document.getElementById("newphoto").style.display!="none") {document.getElementById("newphoto").style.display="none";}
	else {document.getElementById("newphoto").style.display="inline";}
}


function editphoto(v1,v2,v3,v4){
	document.getElementById("v1").value=v1;
	document.getElementById("v2").value=v2;
	document.getElementById("v3").value=v3;
	document.getElementById("v100").value=v4;
    document.getElementById("editphoto").style.display="inline";
}


function del_record(value){
document.getElementById("act").disabled=false;
document.getElementById("act").value=value;
document.getElementById('form1').submit();
}

</script>

