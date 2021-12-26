<SCRIPT LANGUAGE="JavaScript">

document.write('<DIV id=load_file class=input_form style=left:20%;top:35%; ><span id=load_file_move_bar style=position:absolute;background-color:#99CCFF;width:100%;top:0px;left:0px;  ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("import_exceptions",$_SESSION["language"]);?></b></legend><table id=form_table ><form method=post enctype="multipart/form-data"><tr><td><?echo dictionary("exceptions_file",$_SESSION["language"]);?></td><td><input name=value1 type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input name=formsavebtn1 type=submit value="<?echo dictionary("import",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("load_file").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

Drag.init(document.getElementById("load_file_move_bar"),document.getElementById("load_file"));
document.getElementById("load_file").style.display="none";

function load_ex(){
if (document.getElementById("load_file").style.display!="none") {document.getElementById("load_file").style.display="none";close_tab();}
	else {document.getElementById("load_file").style.display="inline";open_tab("load_file");}
    
}

</SCRIPT>

