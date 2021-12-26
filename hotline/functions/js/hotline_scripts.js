<SCRIPT LANGUAGE="JavaScript">

document.write('<DIV id=history class=history_form style=left:10%;top:25%;height:60%;overflow-y:hidden; <?echo access("on/off");?> ><span id=history_move_bar style=position:absolute;background-color:#99CCFF;width:100%;top:0px;left:0px; ></span><fieldset id=ram ><legend id=ram_legenda ><b><Span id=label1 style=z-index:50 ></span></b></legend><img id=addstep src="./images/add.png" width=12px height=12px style=position:absolute;top:5px;left:5px;cursor:pointer; disabled onclick=addstep() alt="<?echo dictionary("new_step",$_SESSION["language"]);?>" /><div id=ht_request></div></fieldset><div id=hist_btn1 style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=show_history(doc_no); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

document.write('<DIV id=newstep class=input_form style=left:20%;top:35%; ><span id=newstep_move_bar class=move_ledge ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("new_step",$_SESSION["language"]);?></b></legend><table id=form_table ><form action=\'<?echo $_SERVER["PHP_SELF"];?>\' method=post enctype="multipart/form-data"><tr><td style=vertical-align:top; ><?echo dictionary("message",$_SESSION["language"]);?></td><td><textarea name=value1 rows=6 wrap="on" style=width:430px;overflow:auto; onselect=true ondblclick=select() ></textarea></td></tr><tr><td><?echo dictionary("priority",$_SESSION["language"]);?></td><td><select size="1" id=nsvalue2 name=value2 style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"hotline_priorities\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 1</td><td><input name=file1 type=file onchange=document.getElementById("file2").disabled=false; style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 2</td><td><input id=file2 name=file2 onchange=document.getElementById("file3").disabled=false; disabled type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 3</td><td><input id=file3 name=file3 type=file disabled style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td style=width:120px;font-size:12px; ><?echo dictionary("request_to",$_SESSION["language"]);?></td><td><INPUT TYPE=button VALUE=\'<?echo dictionary("date",@$_SESSION["language"]);?>\' onClick="cpokus=new calendar(form.value4,\'span_value4\',\'cpokus\');" style=width:50px;><input id=value4 name=value4 type="text" value="<?echo $dnescs;?>" style=width:380px;text-align:center;font-weight:bold; readonly=yes ><span style=position:relative;top:0px;left:-380px; ><div style=position:absolute><SPAN ID=\"span_value4\"></div></span></td></tr><tr <?IF($_SESSION['sysadmin']<>"Yes" && mysql_result(mysql_query("select hotline_worker from login where loginname='".securesql(@$_SESSION["lnamed"])."' "),0,0)<>"Y"){echo "style=\"display: none;\"";} else {echo "style=\"display: inline;\"";}?> ><td style=width:120px; ><?echo dictionary("solution_to",$_SESSION["language"]);?></td><td><INPUT TYPE=button VALUE=\'<?echo dictionary("date",@$_SESSION["language"]);?>\' onClick="cpokus=new calendar(form.value7,\'span_value7\',\'cpokus\');" style=width:50px;><input id=value7 name=value7 type="text" value="<?IF($_SESSION['sysadmin']=="Yes"){echo $dnescs;}?>" style=width:380px;text-align:center;font-weight:bold; readonly=yes ><span style=position:relative;top:0px;left:-380px; ><div style=position:absolute><SPAN ID=\"span_value7\"></div></span></td></tr><tr <?IF($_SESSION['sysadmin']<>"Yes" && mysql_result(mysql_query("select hotline_worker from login where loginname='".securesql(@$_SESSION["lnamed"])."' "),0,0)<>"Y"){echo "style=\"display: none;\"";} else {echo "style=\"display: inline;\"";}?> ><td style=width:120px; ><?echo dictionary("solves",$_SESSION["language"]);?></td><td><select id=nsvalue8 size="1" name=value8 style=width:200px ><option></option><?$load_form_data=mysql_query("select concat(surname,' ',name),loginname from login where hotline_worker='Y' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while(@mysql_result($load_form_data,$cycle,0)):echo"<option value=\'".@mysql_result($load_form_data,$cycle,1)."\' >".@mysql_result($load_form_data,$cycle,0)."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("status",$_SESSION["language"]);?></td><td><select size="1" name=value5 id=nsvalue5 onchange=unblock_save_button(); style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"hotline_statuses\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("score",$_SESSION["language"]);?></td><td><select id=nsvalue6 size="1" onchange=unblock_save_button1(); name=value6 style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"score\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=nsbtn1 name=formsavebtn3 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr><input type=hidden name=value200 value="" id=v200 ></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=addstep(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

document.write('<DIV id=newrequest class=input_form style=left:10%;top:25%; ><span id=newrequest_move_bar style=position:absolute;background-color:#99CCFF;width:100%;height:10px;top:0px;left:0px; ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("new_request",$_SESSION["language"]);?></b></legend><table id=form_table ><form action=\'<?echo $_SERVER["PHP_SELF"];?>\' method=post enctype="multipart/form-data"><tr><td><?echo dictionary("title",$_SESSION["language"]);?></td><td><input name=value1 autocomplete=off type=text value="" style=width:430px;background-color:#FFDEB5; id=nrvalue1 onkeyup=allow_when_selected("input","nrvalue1","nrbtn","DISABLE") ></td></tr><tr><td style=vertical-align:top; ><?echo dictionary("message",$_SESSION["language"]);?></td><td><textarea name=value2 rows=6 wrap="on" style=width:430px;overflow:auto; ></textarea></td></tr><tr><td><?echo dictionary("priority",$_SESSION["language"]);?></td><td><select size="1" name=value3 style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"hotline_priorities\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 1</td><td><input name=file1 onchange=document.getElementById("nfile2").disabled=false; type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 2</td><td><input id=nfile2 name=file2 disabled onchange=document.getElementById("nfile3").disabled=false; type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 3</td><td><input id=nfile3 name=file3 disabled onchange=document.getElementById("nfile4").disabled=false; type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 4</td><td><input id=nfile4 name=file4 disabled onchange=document.getElementById("nfile5").disabled=false; type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td><?echo dictionary("attachment",$_SESSION["language"]);?> 5</td><td><input id=nfile5 name=file5 disabled type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td style=width:120px;font-size:12px; ><?echo dictionary("request_to",$_SESSION["language"]);?></td><td><INPUT TYPE=button VALUE=\'<?echo dictionary("date",@$_SESSION["language"]);?>\' onClick="cpokus=new calendar(form.value5,\'span_value5\',\'cpokus\');" style=width:50px;><input id=value5 name=value5 type="text" value="<?echo $dnescs;?>" style=width:380px;text-align:center;font-weight:bold; readonly=yes ><span style=position:relative;top:0px;left:-380px; ><div style=position:absolute><SPAN ID=\"span_value5\"></div></span></td></tr><tr><td style=width:120px; ><?echo dictionary("solves",$_SESSION["language"]);?></td><td><select id=nsvalue8 size="1" name=value8 style=width:200px ><option></option><?$load_form_data=mysql_query("select concat(surname,' ',name),loginname from login where sysadmin='Yes' or hotline_worker='Y' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while(@mysql_result($load_form_data,$cycle,0)):echo"<option value=\'".@mysql_result($load_form_data,$cycle,1)."\' >".@mysql_result($load_form_data,$cycle,0)."</option>";$cycle++;endwhile;?></select></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input id=nrbtn disabled name=formsavebtn1 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=addrequest(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

document.write('<DIV id=editrequest class=input_form style=left:10%;top:25%; ><span id=editrequest_move_bar style=position:relative;background-color:#99CCFF;width:100%;height:10px;top:0px;left:0px; ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("edit_request",$_SESSION["language"]);?></b></legend><table id=form_table ><form action=\'<?echo $_SERVER["PHP_SELF"];?>\' method=post enctype="multipart/form-data"><tr><td><?echo dictionary("document_no",$_SESSION["language"]);?></td><td><input disabled id=v9 type=text value="" style=width:430px;text-align:center; ></td></tr><tr><td><?echo dictionary("title",$_SESSION["language"]);?></td><td><input <?echo access("1/2off");?> id=v1 name=value1 autocomplete=off type=text value="" style=width:430px ></td></tr><tr><td style=vertical-align:top; ><?echo dictionary("message",$_SESSION["language"]);?></td><td><textarea <?echo access("1/2off");?> id=v2 name=value2 rows=6 wrap="on" style=width:430px;overflow:auto; ></textarea></td></tr><tr><td><?echo dictionary("priority",$_SESSION["language"]);?></td><td><select <?echo access("1/2off");?> id=v3 size="1" name=value3 style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"hotline_priorities\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><img id=icon src="./images/delete.png" width="20" height="20" border="0" align=right style=cursor:pointer; <?echo access("1/2off");?> onclick=\'if(confirm("<?echo dictionary("del_attachment",$_SESSION["language"]);?>")) del_icon(document.getElementById("v100").value,\"hotline_request\");\' alt=<?echo dictionary("delete",$_SESSION["language"]);?> ><a id=rfile href="" target=_blank ><?echo dictionary("attachment",$_SESSION["language"]);?></a></td><td><input <?echo access("1/2off");?> name=value4 type=file style=width:430px title=<?echo dictionary("add_attachment",$_SESSION["language"]);?> ></td></tr><tr><td style=width:120px ><?echo dictionary("request_to",$_SESSION["language"]);?></td><td><INPUT <?echo access("1/2off");?> TYPE=button VALUE=\'<?echo dictionary("date",@$_SESSION["language"]);?>\' onClick="cpokus=new calendar(form.evalue5,\'span_evalue5\',\'cpokus\');" style=width:50px;><input <?echo access("1/2off");?> id=evalue5 name=evalue5 type="text" value="" style=width:380px;text-align:center;font-weight:bold; readonly=yes ><span style=position:relative;top:0px;left:-380px; ><div style=position:absolute><SPAN ID=\"span_evalue5\"></div></span></td></tr><tr><td><?echo dictionary("status",$_SESSION["language"]);?></td><td><select <?echo access("1/2off");?> id=v6 size="1" name=value6 style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"hotline_statuses\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td><?echo dictionary("score",$_SESSION["language"]);?></td><td><select <?echo access("1/2off");?> id=v10 size="1" name=value10 style=width:200px ><?$load_form_data=mysql_query("select param from mainsettings where name=\"score\" ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while($part[$cycle]<>""):echo"<option value=\'".$part[$cycle]."\' >".$part[$cycle]."</option>";$cycle++;endwhile;?></select></td></tr><tr><td colspan=2 style=width:100%;text-align:right; ><input <?echo access("1/2off");?> name=formsavebtn2 type=submit value="<?echo dictionary("save",$_SESSION["language"]);?>"</td></tr><input id=v100 type=hidden name=v100 value=""><input id=v1000 type=hidden name=v1000 value=""></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("editrequest").style.display="none";close_tab(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div></DIV>');

Drag.init(document.getElementById("history_move_bar"),document.getElementById("history"));
Drag.init(document.getElementById("newstep_move_bar"),document.getElementById("newstep"));
Drag.init(document.getElementById("newrequest_move_bar"),document.getElementById("newrequest"));
Drag.init(document.getElementById("editrequest_move_bar"),document.getElementById("editrequest"));
document.getElementById("history").style.display="none";
document.getElementById("newstep").style.display="none";
document.getElementById("newrequest").style.display="none";
document.getElementById("editrequest").style.display="none";
var doc_no;
var previous_step;
var sw_temp;

function show_history(v1){

if (document.getElementById("history").style.display!="none"
&& '<?echo dictionary("request_no",$_SESSION["language"]);?>: ' + v1 == document.getElementById("label1").innerHTML) {document.getElementById("history").style.display="none";
document.getElementById("newstep").style.display="none";
document.getElementById("newrequest").style.display="none";
document.getElementById("history").disabled=false;close_tab();
}
	else {
	       document.getElementById("history").style.height= Math.ceil(window.parent.document.getElementById("program_frame").clientHeight * 0.6);
     document.getElementById("history").style.display="inline";open_tab("history");
    document.getElementById("label1").innerHTML='<?echo dictionary("request_no",$_SESSION["language"]);?>: ' + v1;
    doc_no=v1;
    
    document.getElementById("newstep").style.display="none";
    document.getElementById("newrequest").style.display="none";

    
    script = document.createElement('script');
    script.src = "./history_hotline.php?id="+v1;
    document.getElementsByTagName('head')[0].appendChild(script);    
    document.getElementById("history").disabled=false;
    }
    
}




function unblock_save_button(){
    
var previous_step_data = previous_step.split("|");
allow_when_selected("select|select","nsvalue5|nsvalue6","nsbtn1",previous_step_data[6]+"|DISABLE");

if (document.getElementById("nsbtn1").disabled == true){document.getElementById("nsvalue6").style.backgroundColor="#FFDEB5";
} else {document.getElementById("nsvalue6").style.backgroundColor="#FFFFFF";}

document.getElementById("nsvalue6").value="";
}

function unblock_save_button1(){
    
previous_step_data=previous_step.split("|");allow_when_selected("select|select","nsvalue5|nsvalue6","nsbtn1","DISABLE| ");if (document.getElementById("nsbtn1").disabled === true){document.getElementById("nsvalue6").style.backgroundColor="#FFDEB5";} else {document.getElementById("nsvalue6").style.backgroundColor="#FFFFFF";}
    
}



function addstep(){        // zobrazeni newstep
    if (document.getElementById("newstep").style.display!="none") {
        document.getElementById("newstep").style.display="none";
        document.getElementById("history").disabled=false;close_tab();
    } else {
        
//inserting previous step values
var previous_step_data = previous_step.split("|");
select_match_value(document.getElementById("nsvalue2"),previous_step_data[0]);
if (previous_step_data[1]!="00.00.0000") {document.getElementById("value4").value = previous_step_data[1];}
else {document.getElementById("value4").value ="";}
if (previous_step_data[2]!="00.00.0000") {document.getElementById("value7").value = previous_step_data[2];}
else {document.getElementById("value7").value ="";}
select_match_value(document.getElementById("nsvalue8"),previous_step_data[3]);
select_match_value(document.getElementById("nsvalue5"),previous_step_data[4]);
select_match_value(document.getElementById("nsvalue6"),previous_step_data[5]);


unblock_save_button();


        document.getElementById("history").disabled=true;
        document.getElementById("newstep").style.display="inline";open_tab("newstep");
        document.getElementById("v200").value=doc_no;
        
        }
}

function addrequest(){        // zobrazeni newrequest
    if (document.getElementById("newrequest").style.display!="none") {document.getElementById("newrequest").style.display="none";close_tab();}
    	else {document.getElementById("newrequest").style.display="inline";open_tab("newrequest");
            document.getElementById("history").style.display="none";
            document.getElementById("newstep").style.display="none";
        }
}

function editrequest(v1,v2,v3,v4,v5,v6,v7,v8,v9,v10,laststatus){

    document.getElementById("v10").value=v10;
	document.getElementById("v9").value=v9;
	document.getElementById("v1").value=v1;
	document.getElementById("v2").value=decodeURIComponent(v2);
	document.getElementById("v3").value=v3;
	document.getElementById("evalue5").value=v4;
	document.getElementById("v100").value=v5;
    document.getElementById("v1000").value=v6;
    document.getElementById("v6").value=v7;
    if (v5!=''){
	    document.getElementById("rfile").title="<?echo dictionary("open_file",$_SESSION["language"]);?>: "+v8;
    	document.getElementById("rfile").href="./ajax_functions.php?show_file=yes&tbl=hotline_request&id="+v6;
	    document.getElementById("icon").style.display="inline";
    } else {
    	document.getElementById("rfile").title="";
	    document.getElementById("rfile").removeAttribute('href');
	    document.getElementById("icon").style.display="none";
    }
    if (v7===laststatus){document.getElementById("v10").disabled=false;}
    else {document.getElementById("v10").disabled=true;}

    document.getElementById("editrequest").style.display="inline";open_tab("editrequest");
}

function del_request(value){
document.getElementById("act").disabled=false;
document.getElementById("act").value=value;
document.getElementById('form1').submit();
}

function del_icon(value,table){
script=document.createElement('script');
script.src='./file_funct.php?table='+table+'&del='+value;
document.getElementsByTagName('head')[0].appendChild(script);
window.location.href='<?echo $_SERVER["REQUEST_URI"];?>';
}


 if ('<?echo @$_POST["hidden_v1"];?>' != "" ) {
    document.getElementById('<?echo @$_POST["hidden_v1"];?>').focus();
    document.getElementById('<?echo @$_POST["hidden_v1"];?>').value=document.getElementById('<?echo @$_POST["hidden_v1"];?>').value;        
}

function reset_form(form_name){
var i=1;
  try {
    while (document.getElementById("search_v"+i).disabled == false){
       document.getElementById("search_v"+i).value="";
       i++;                        
    }
  }
  catch ( e ) {document.getElementById(form_name).submit();}
}

</script>

