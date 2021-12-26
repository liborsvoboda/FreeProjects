<SCRIPT LANGUAGE="JavaScript">

document.write('<DIV id=add_icon class=input_form style=left:20%;top:35%; ><span id=add_icon_move_bar class=move_ledge ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("new_icon",$_SESSION["language"]);?></b></legend><table id=form_table ><form action=\'<?echo $_SERVER["PHP_SELF"];?>\' method=post enctype="multipart/form-data"><tr><td style=vertical-align:top; ><?echo dictionary("title",$_SESSION["language"]);?></td><td><input type=input name=value1 value="" style=width:100%;text-align:center; onclick=select() ></td></tr><tr><td style=vertical-align:top; ><?echo dictionary("command",$_SESSION["language"]);?></td><td><textarea name=value2 rows=6 wrap="on" style=width:100%;overflow:auto; ></textarea></td></tr><tr><td><?echo dictionary("command_type",$_SESSION["language"]);?></td><td><select size="1" id=nsvalue3 name=value3 style=width:200px; disabled=disabled ><?$load_form_data=mysql_query("SHOW COLUMNS FROM user_desktop ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",str_replace("'","",substr(mysql_result($load_form_data,9,1),5,strlen(mysql_result($load_form_data,9,1))-7)));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".dictionary($part[$cycle],$_SESSION["language"])."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("icon",$_SESSION["language"]);?></td><td><input name=file1 type=file style=width:460px title=<?echo dictionary("icon",$_SESSION["language"]);?> ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input name=formsavebtn1 id=nsbtn1 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=new_icon(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

document.write('<DIV id=icon_edit class=input_form style=left:20%;top:35%; ><span id=icon_edit_move_bar class=move_ledge ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("icon_edit",$_SESSION["language"]);?></b></legend><table id=form_table ><form action=\'<?echo $_SERVER["PHP_SELF"];?>\' method=post enctype="multipart/form-data"><tr><td style=vertical-align:top; ><?echo dictionary("title",$_SESSION["language"]);?></td><td><input id=ie_value1 type=input name=value1 value="" style=width:100%;text-align:center; onclick=select() ></td></tr><tr><td style=vertical-align:top; ><?echo dictionary("command",$_SESSION["language"]);?></td><td><textarea id=ie_value2 name=value2 rows=6 wrap="on" style=width:100%;overflow:auto; ></textarea></td></tr><tr><td><?echo dictionary("command_type",$_SESSION["language"]);?></td><td><select size="1" id=nsvalue3 name=value3 style=width:200px ><?$load_form_data=mysql_query("SHOW COLUMNS FROM user_desktop ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",str_replace("'","",substr(mysql_result($load_form_data,9,1),5,strlen(mysql_result($load_form_data,9,1))-7)));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".dictionary($part[$cycle],$_SESSION["language"])."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("icon",$_SESSION["language"]);?><img src="" id=ico_file style="text-align:right;cursor:pointer;width:24px;height:24px;" ></td><td><input name=file1 type=file style=width:460px title=<?echo dictionary("icon",$_SESSION["language"]);?> ></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input name=formsavebtn2 id=nsbtn1 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr><input id=ie_value100 type=hidden name=value100 value=""></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=icon_edit(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

Drag.init(document.getElementById("add_icon_move_bar"),document.getElementById("add_icon"));
document.getElementById("add_icon").style.display="none";

Drag.init(document.getElementById("icon_edit_move_bar"),document.getElementById("icon_edit"));
document.getElementById("icon_edit").style.display="none";

function new_icon(){        // zobrazeni add_icon
    if (document.getElementById("add_icon").style.display!="none") {
        document.getElementById("add_icon").style.display="none";close_tab();
    } else {
        document.getElementById("add_icon").style.display="inline";open_tab("add_icon");
        }
}

function icon_edit(v1,v2,v3,v4){        // editace ikony
    if (document.getElementById("icon_edit").style.display!="none") {
        document.getElementById("icon_edit").style.display="none";close_tab();
    } else {
        document.getElementById("icon_edit").style.display="inline";open_tab("icon_edit");
        document.getElementById("ie_value1").value=v1;
        document.getElementById("ie_value2").value=v2;
        document.getElementById("ie_value100").value=v3;
        if (v4=="YES"){document.getElementById("ico_file").src='./ajax_functions.php?pictures=yes&tbl=user_desktop&id='+v3;
        document.getElementById("ico_file").style.visibility='visible';}
        else {document.getElementById("ico_file").src='';document.getElementById("ico_file").style.visibility='hidden';}

    }
}        


function dragable_icon(value){
var i;
   for(i = 0; i < (value+1); i++){
       Drag_icon.init("id_"+i+"_icon",document.getElementById("id_"+i),document.getElementById("id_"+i));
   }
}


function fn_mouse_xy(value,value1){      //  icon fix x,y
    var icon_object = document.getElementById("id_"+value);
    var icon_positions = icon_object.getBoundingClientRect();
//alert("Coordinates: " + positions.left + "px, " + positions.top + "px");   
window.location.href="<?echo @$rooturl[0];?>?fix="+value1+"&fixx="+icon_positions.left+"&fixy="+icon_positions.top;
  
}

function select_desktop(v1){
       for(var i = 1; i < 5; i++)
   {
      if(i == v1){document.getElementById("desktop_no_"+i).disabled=true;document.getElementById("desktop_no").value=i;
      document.getElementById("desktop_no_"+i).className ='desktop_no_in';document.getElementById("form1").submit();}
      else {document.getElementById("desktop_no_"+i).className ='desktop_no_out';document.getElementById("desktop_no_"+i).disabled=false;}
   }
}


function run_cmd(value,inputparms){
    activeX_Control();
try{
    
    var x = new ActiveXObject("WScript.Shell");    
    x.run(value); 
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



