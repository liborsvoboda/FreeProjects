<SCRIPT LANGUAGE="JavaScript">

document.write('<DIV id=newlink style=position:absolute;width:400px;left:50%;top:25%;margin-left:-220px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("add_link",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("sequence",$_SESSION["language"]);?></td><td><input name=value1 autocomplete=off type=text value="" style=width:220px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input name=value2 autocomplete=off type=text value="" style=width:220px ></td></tr><tr><td><?echo dictionary("display_type",$_SESSION["language"]);?></td><td><select id="value3" size="1" name=value3 style=width:220px onchange=styleline() ><option value="DIV_WINDOW"><?echo dictionary("DIV_WINDOW",$_SESSION["language"]);?></option><option value="NEW_WINDOW"><?echo dictionary("NEW_WINDOW",$_SESSION["language"]);?></option><option value="GOTOPAGE"><?echo dictionary("GOTOPAGE",$_SESSION["language"]);?></option></select></td></tr><tr><td><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=value4 type=file accept="image/*" style=width:220px ></td></tr><tr id=styleline ><td><?echo dictionary("style_css",$_SESSION["language"]);?></td><td><input id=chkval1 name=value5 autocomplete=off type=text value="" style="width:220px;" ><img src="./images/list.png" width="20" height="20" border="0" style="vertical-align:top;cursor:pointer;" onclick=checkvalue("chkval1","7","webmin_main_sett","4"); /></td></tr><tr><td style="vertical-align:top;"><?echo dictionary("value",$_SESSION["language"]);?></td><td><textarea name="value6" rows="5" wrap="off" style="width:220px;overflow:auto;" ></textarea></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input name=formsavebtn6 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("newlink").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');



document.write('<DIV id=editlink style=position:absolute;width:400px;left:50%;top:25%;margin-left:-220px;background:silver;border:3px;padding:5px;border-style:outset; ><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("edit_link",$_SESSION["language"]);?></b></legend><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("sequence",$_SESSION["language"]);?></td><td><input id=valuee1 name=value1 autocomplete=off type=text value="" style=width:220px ></td></tr><tr><td><?echo dictionary("dictionary_name",$_SESSION["language"]);?></td><td><input id=valuee2 name=value2 autocomplete=off type=text value="" style=width:220px ></td></tr><tr><td><?echo dictionary("display_type",$_SESSION["language"]);?></td><td><select id="valuee3" size="1" name=value3 style=width:220px onchange=stylelinee() ><option value="DIV_WINDOW"><?echo dictionary("DIV_WINDOW",$_SESSION["language"]);?></option><option value="NEW_WINDOW"><?echo dictionary("NEW_WINDOW",$_SESSION["language"]);?></option><option value="GOTOPAGE"><?echo dictionary("GOTOPAGE",$_SESSION["language"]);?></option></select></td></tr><tr><td><img id=icon src="" width="20" height="20" border="0" align=right style=cursor:pointer; onclick=\'if(confirm("<?echo dictionary("del_icon",$_SESSION["language"]);?>")) del_icon(document.getElementById("v100").value,\"www_links\");\' alt=<?echo dictionary("delete",$_SESSION["language"]);?> ><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=value4 type=file accept="image/*" style=width:220px ></td></tr><tr id=stylelinee ><td><?echo dictionary("style_css",$_SESSION["language"]);?></td><td><input id=chkvale1 name=value5 autocomplete=off type=text value="" style="width:220px;" ><img src="./images/list.png" width="20" height="20" border="0" style="vertical-align:top;cursor:pointer;" onclick=checkvalue("chkvale1","7","webmin_main_sett","4"); /></td></tr><tr><td style="vertical-align:top;"><?echo dictionary("value",$_SESSION["language"]);?></td><td><textarea id=valuee6 name="value6" rows="5" wrap="off" style="width:220px;overflow:auto;" ></textarea></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=v100 type=hidden name=v100 value=""><input name=formsavebtn7 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("editlink").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');

document.getElementById("newlink").style.display="none";
document.getElementById("editlink").style.display="none";


function addlink(){        // zobrazeni newlink
document.getElementById("checkvalue").style.display ='none';
if (document.getElementById("newlink").style.display!="none") {document.getElementById("newlink").style.display="none";}
	else {document.getElementById("newlink").style.display="inline";}
}


function styleline(){
    if (document.getElementById("value3").value!="DIV_WINDOW"){
        document.getElementById("styleline").style.display ='none';document.getElementById("chkval1").disabled =true;
    }else{document.getElementById("chkval1").disabled =false;document.getElementById("styleline").style.display ='inline';}
}

function stylelinee(){
    if (document.getElementById("valuee3").value!="DIV_WINDOW"){
        document.getElementById("stylelinee").style.display ='none';document.getElementById("chkvale1").disabled =true;
    }else{document.getElementById("chkvale1").disabled =false;document.getElementById("stylelinee").style.display ='inline';}
}

function editlink(v1,v2,v3,v4,v5,v6,v7){
	document.getElementById("valuee1").value=v1;
	document.getElementById("valuee2").value=v2;
	document.getElementById("valuee3").value=v3;
	document.getElementById("chkvale1").value=v4;
	document.getElementById("valuee6").value=v5;
	document.getElementById("v100").value=v6;
    document.getElementById("editlink").style.display="inline";
        if (v7 != ""){document.getElementById("icon").src='./ajax_functions.php?icon=yes&tbl=<?echo base64_encode("www_links");?>&id='+v6;document.getElementById("icon").style.display='inline';} else {document.getElementById("icon").style.display='none';}
}


</script>

