<SCRIPT LANGUAGE="JavaScript">
document.write('<DIV id=newmenuitem style=position:absolute;width:400px;left:50%;top:25%;margin-left:-200px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("new_menu_item",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("menu_order",$_SESSION["language"]);?></td><td><input name=value1 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input name=value2 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("seo_keywords",$_SESSION["language"]);?></td><td><input name=value3 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("view_after_login",$_SESSION["language"]);?></td><td><input name=value4 type=checkbox ></td></tr><tr><td><?echo dictionary("view_with_subitem",$_SESSION["language"]);?></td><td><input name=value5 type=checkbox ></td></tr><tr><td><?echo dictionary("submenu_item",$_SESSION["language"]);?></td><td><input name=value6 type=checkbox ></td></tr><tr><td><?echo dictionary("display_type",$_SESSION["language"]);?></td><td><select size="1" name=value7 style=width:200px ><option value="EDIT"><?echo dictionary("editor",$_SESSION["language"]);?></option><option value="INCLUDE"><?echo dictionary("source_file",$_SESSION["language"]);?></option></select></td></tr><tr><td><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=value8 type=file accept="image/*" style=width:200px ></td></tr><tr><td><?echo dictionary("tracking_url",$_SESSION["language"]);?></td><td><input name=value9 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input name=formsavebtn type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("newmenuitem").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');
document.getElementById("newmenuitem").style.display="none";

function newmenuitem() {
if (document.getElementById("newmenuitem").style.display!="none") {document.getElementById("newmenuitem").style.display="none";}
	else {document.getElementById("newmenuitem").style.display="inline";}
}


document.write('<DIV id=editmenuitem style=position:absolute;width:400px;left:50%;top:25%;margin-left:-200px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("edit_menu_item",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("menu_order",$_SESSION["language"]);?></td><td><input id=v1 name=value1 type=text value="" autocomplete=off style=width:200px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input id=v2 name=value2 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("seo_keywords",$_SESSION["language"]);?></td><td><input id=v3 name=value3 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("view_after_login",$_SESSION["language"]);?></td><td><input id=v4 name=value4 type=checkbox ></td></tr><tr><td><?echo dictionary("view_with_subitem",$_SESSION["language"]);?></td><td><input id=v5 name=value5 type=checkbox ></td></tr><tr><td><?echo dictionary("submenu_item",$_SESSION["language"]);?></td><td><input  id=v6 name=value6 type=checkbox ></td></tr><tr><td><?echo dictionary("display_type",$_SESSION["language"]);?></td><td><select id=v7 size="1" name=value7 style=width:200px ><option value="EDIT"><?echo dictionary("editor",$_SESSION["language"]);?></option><option value="INCLUDE"><?echo dictionary("source_file",$_SESSION["language"]);?></option></select></td></tr><tr><td><img id=icon src="" width="20" height="20" border="0" align=right style=cursor:pointer; onclick=\'if(confirm("<?echo dictionary("del_icon",$_SESSION["language"]);?>")) del_icon(document.getElementById("v100").value,\"www_menu\");\' alt=<?echo dictionary("delete",$_SESSION["language"]);?> ><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=value8 type=file accept="image/*" style=width:200px ></td></tr><tr><td><?echo dictionary("tracking_url",$_SESSION["language"]);?></td><td><input id=v9 autocomplete=off name=value9 type=text value="" style=width:200px ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=v100 name=value100 type="hidden" value=""><input name=formsavebtn1 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("editmenuitem").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');

document.write('<DIV id=newsubmenuitem style=position:absolute;width:400px;left:50%;top:25%;margin-left:-200px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("new_submenu_item",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("menu_order",$_SESSION["language"]);?></td><td><input name=value1 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input name=value2 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("seo_keywords",$_SESSION["language"]);?></td><td><input name=value3 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("view_after_login",$_SESSION["language"]);?></td><td><input name=value4 type=checkbox ></td></tr><tr><td><?echo dictionary("display_type",$_SESSION["language"]);?></td><td><select size="1" name=value7 style=width:200px ><option value="EDIT"><?echo dictionary("editor",$_SESSION["language"]);?></option><option value="INCLUDE"><?echo dictionary("source_file",$_SESSION["language"]);?></option></select></td></tr><tr><td><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=value8 type=file accept="image/*" style=width:200px ></td></tr><tr><td><?echo dictionary("tracking_url",$_SESSION["language"]);?></td><td><input name=value9 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=v1000 name=value100 type="hidden" value=""><input name=formsavebtn2 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("newsubmenuitem").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');

document.write('<DIV id=editsubmenuitem style=position:absolute;width:400px;left:50%;top:25%;margin-left:-200px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("edit_submenu_item",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("menu_order",$_SESSION["language"]);?></td><td><input id=vs1 name=value1 type=text value="" autocomplete=off style=width:200px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input id=vs2 name=value2 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("seo_keywords",$_SESSION["language"]);?></td><td><input id=vs3 name=value3 autocomplete=off type=text value="" style=width:200px ></td></tr><tr><td><?echo dictionary("view_after_login",$_SESSION["language"]);?></td><td><input id=vs4 name=value4 type=checkbox ></td></tr><tr><td><?echo dictionary("display_type",$_SESSION["language"]);?></td><td><select id=vs7 size="1" name=value7 style=width:200px ><option value="EDIT"><?echo dictionary("editor",$_SESSION["language"]);?></option><option value="INCLUDE"><?echo dictionary("source_file",$_SESSION["language"]);?></option></select></td></tr><tr><td><img id=icons src="" width="20" height="20" border="0" align=right style=cursor:pointer; onclick=\'if(confirm("<?echo dictionary("del_icon",$_SESSION["language"]);?>")) del_icon(document.getElementById("v100").value,\"www_menu\");\' alt=<?echo dictionary("delete",$_SESSION["language"]);?> ><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=value8 type=file accept="image/*" style=width:200px ></td></tr><tr><td><?echo dictionary("tracking_url",$_SESSION["language"]);?></td><td><input id=vs9 autocomplete=off name=value9 type=text value="" style=width:200px ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=vs100 name=value100 type="hidden" value=""><input name=formsavebtn3 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("editsubmenuitem").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');

document.getElementById("editmenuitem").style.display="none";
document.getElementById("editor").style.display="none";
document.getElementById("editmenu").style.display="none";
document.getElementById("delmenu").style.display="none";
document.getElementById("back").style.display="none";
document.getElementById("newsubmenu").style.display="none";
document.getElementById("newsubmenuitem").style.display="none";
document.getElementById("editsubmenu").style.display="none";
document.getElementById("editsubmenuitem").style.display="none";
document.getElementById("savebtn").style.display="none";
document.getElementById("langmenu").style.display="none";


function newsubmenuitem() {
    document.getElementById("v1000").value=document.getElementById("v100").value;
if (document.getElementById("newsubmenuitem").style.display!="none") {document.getElementById("newsubmenuitem").style.display="none";}
	else {document.getElementById("newsubmenuitem").style.display="inline";}
}

function editmenuitem(v1,v2,v3,v4,v5,v6,v7,v8,v9,v10,v11,v12) {  // aktivace edit tlacitka
	document.getElementById("v1").value=v1;
	document.getElementById("v2").value=v2;
	document.getElementById("v3").value=v3;
	if (v4=="ANO"){document.getElementById("v4").checked=true;} else {document.getElementById("v4").checked=false;}
	if (v5=="ANO"){document.getElementById("v5").checked=true;} else {document.getElementById("v5").checked=false;}
	if (v6=="ANO"){document.getElementById("v6").checked=true;document.getElementById("newsubmenu").style.display='inline';} else {document.getElementById("v6").checked=false;document.getElementById("newsubmenu").style.display='none';}
	document.getElementById("v7").value=v7;

    document.getElementById("v100").value=v10;document.getElementById("rec_id").value=v10;

//    CKEDITOR.instances.editor_data.setData(v11);


    if (v8 != ""){document.getElementById("icon").src='./ajax_functions.php?icon=yes&tbl=<?echo base64_encode("www_menu");?>&id='+v10;document.getElementById("icon").style.display='inline';} else {document.getElementById("icon").style.display='none';}
	document.getElementById("v9").value=v9;

	document.getElementById("editor").style.display="inline";
	document.getElementById("editmenu").style.display="inline";
	if (v12=="NO"){document.getElementById("delmenu").style.display="inline";}
    document.getElementById("back").style.display="inline";

document.getElementById('newmenubutton').style.display="none";
document.getElementById("table_desc").style.display = 'none';
document.getElementById("menuname").innerHTML = v1+' '+v2;
document.getElementById("savebtn").style.display="inline";
document.getElementById("langmenu").style.display="inline";

load_data(document.getElementById("sel_lang").value,v10);
}


function editsubmenuitem(v1,v2,v3,v4,v5,v6,v7,v8,v9,v10,v11,v12) {  // aktivace edit subtlacitka
	document.getElementById("vs1").value=v1;
	document.getElementById("vs2").value=v2;
	document.getElementById("vs3").value=v3;
	if (v4=="ANO"){document.getElementById("vs4").checked=true;} else {document.getElementById("vs4").checked=false;}
//	if (v5=="ANO"){document.getElementById("vs5").checked=true;} else {document.getElementById("vs5").checked=false;}
//	if (v6=="ANO"){document.getElementById("vs6").checked=true;document.getElementById("newsubmenu").style.display='inline';} else {document.getElementById("vs6").checked=false;document.getElementById("newsubmenu").style.display='none';}
	document.getElementById("vs7").value=v7;

    document.getElementById("vs100").value=v10;document.getElementById("rec_id").value=v10;
    CKEDITOR.instances.editor_data.setData(v11);


    if (v8 != ""){document.getElementById("icons").src='./ajax_functions.php?icon=yes&tbl=<?echo base64_encode("www_menu");?>&id='+v10;document.getElementById("icons").style.display='inline';} else {document.getElementById("icons").style.display='none';}
	document.getElementById("vs9").value=v9;

	document.getElementById("editor").style.display="inline";
	document.getElementById("editsubmenu").style.display="inline";

	if (v12=="NO"){
	   document.getElementById("v2").value=v2;document.getElementById("v100").value=v10;
	   document.getElementById("delmenu").style.display="inline";
    }

    document.getElementById("back").style.display="inline";

document.getElementById('newmenubutton').style.display="none";
document.getElementById("table_desc").style.display = 'none';
document.getElementById("menuname").innerHTML = v1+' '+v2;
document.getElementById("savebtn").style.display="inline";
document.getElementById("langmenu").style.display="inline";

load_data(document.getElementById("sel_lang").value,v10);
}

function showeditmenu(){        // zobrazeni edit menu
if (document.getElementById("editmenuitem").style.display!="none") {document.getElementById("editmenuitem").style.display="none";}
	else {document.getElementById("editmenuitem").style.display="inline";}
}

function showeditsubmenu(){        // zobrazeni edit submenu
if (document.getElementById("editsubmenuitem").style.display!="none") {document.getElementById("editsubmenuitem").style.display="none";}
	else {document.getElementById("editsubmenuitem").style.display="inline";}
}


   

function load_data(vad1,vad2){
var client = new XMLHttpRequest();
client.open('GET', './ajax_functions.php?ckedit=yes&lang='+vad1+'&id='+vad2);
client.onreadystatechange = function() {
    CKEDITOR.instances.editor_data.setData(client.responseText);
}
    client.send();
}



function slct_lang(value,slct,cycle){
 
 for (var i=0;i<cycle;i++) { 
     if (i==slct){
        document.getElementById('lang'+i).disabled =true; 
        document.getElementById('lang'+i).className ='inico'; 
        document.getElementById('sel_lang').value=value;
    } else 
    {
        document.getElementById('lang'+i).disabled =false; 
        document.getElementById('lang'+i).className ='outico'; 
    }
 }
        va1=document.getElementById("rec_id").value;
        load_data(value,va1);
} 


</script>

