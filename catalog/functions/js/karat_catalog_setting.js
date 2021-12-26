    <script type="text/JavaScript"> 

document.write('<DIV id=login class=login_form style=left:50%;top:30%; ><span id=login_move_bar style=position:absolute;background-color:#99CCFF;width:100%;top:0px;left:0px; ></span><form action="'+document.URL+'" method=post enctype="multipart/form-data" ><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><tr><td colspan=2 align=center ><b><?echo dictionary("login",$_SESSION["language"]);?></b></td></tr><tr><td><?echo dictionary("username",$_SESSION["language"]);?></td><td><input name="user" type="text" value="" style=width:180px;text-align:center; ></td></tr><tr><td><?echo dictionary("password",$_SESSION["language"]);?></td><td><input name="password" type="password" value="" style=width:180px;text-align:center;></td></tr><tr><td></td><td><img id="siimage" align=left style="vertical-align:top;padding-right:0px;width:156px;height:48px;" border=0 src="./modules/captcha/securimage_show_admin.php?sid=<?php echo md5(time());?>"/><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="24" height="24" id="SecurImage_as3" align="middle"><param name="allowScriptAccess" value="sameDomain" /><param name="allowFullScreen" value="false" /><param name="movie" value="./modules/captcha/securimage_play.swf?audio=./modules/captcha/securimage_play.php?&bgColor1=#777&bgColor2=#fff&iconColor=#000&roundedCorner=5" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" /><embed src="./modules/captcha/securimage_play.swf?audio=./modules/captcha/securimage_play.php?&bgColor1=#777&bgColor2=#fff&iconColor=#000&roundedCorner=5" quality="high" bgcolor="#ffffff" width="24" height="24" name="SecurImage_as3" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" /></object></td></tr><tr><td></td><td><input type="text" name="code" style="width:180px;vertical-align:top;text-align:center;color:black;resize:none;background:#DD4448" value="<?echo dictionary("rewritecode",$_SESSION["language"]);?>" onClick=select() autocomplete="off" /></td></tr><tr><td></td><td align=right><input type="submit" value="<?echo dictionary("login",$_SESSION["language"]);?>"></td></tr></table></form><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=login(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');
document.getElementById("login").style.display="none";

function login(){
if (document.getElementById("login").style.display!="none") {document.getElementById("login").style.display="none";}
 else {document.getElementById("login").style.display="inline";}
}

function logout(){
    parent.document.getElementById("sesswindow").src ='./functions/php/unset.inc.php';
    window.location.assign('<?
        @$sql = "select systemname from dbo.[100_main_setting] where data_type='admin_url' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
        echo dictionary(@$row[0],$_SESSION['language']);?>');
}



function fn_demand_list(){
    clean_loading_panel();
    document.getElementById("data_area").style.display='inline';
    document.getElementById("data_area").innerHTML='<iframe type=text/html src=./ajax_functions.php?demand_list=YES style=top:0;left:0;right:0;bottom:0;width:100%;height:100%;z-index:100;margin:0px;padding:0px;scrolling:auto; margin=0px padding=0px align=left frameborder=0 scrolling=yes noresize=noresize ></iframe>';
    document.getElementById("loading").style.display="none";
}



function visits_graph(){
    clean_loading_panel();
    document.getElementById("data_area").style.display='inline';
    document.getElementById("data_area").innerHTML='<iframe type=text/html src=./graph_report.php style=top:0;left:0;right:0;bottom:0;width:100%;height:100%;z-index:100;margin:0px;padding:0px;scrolling:auto; margin=0px padding=0px align=left frameborder=0 scrolling=yes noresize=noresize ></iframe>';
    document.getElementById("loading").style.display="none";
   
}

function fn_update_record(value1,value2,value3,value4){
    parent.document.getElementById("sesswindow").src ='./ajax_functions.php?update_rec=YES&v1='+value1+'&v2='+value2+'&v3='+value3+'&v4='+value4;
    return true;
}


function check_mail_update (value1,value2,value3){
    value4 = prompt("<?echo dictionary("change_mail_address",$_SESSION["language"]).":";?>", "");
    if(value4) {
        if (fn_update_record(value1,value4,value2,value3) == true){        
            parent.user_administration();
        }
    }
}

function update_search_index(){
    clean_loading_panel();
    document.getElementById("data_area").style.display='inline';
    document.getElementById("data_area").innerHTML='<iframe type=text/html src=./create_search_index.php style=top:0;left:0;right:0;bottom:0;width:100%;height:100%;z-index:100;margin:0px;padding:0px;scrolling:auto; margin=0px padding=0px align=left frameborder=0 scrolling=yes noresize=noresize ></iframe>';
}



function user_administration(){
    clean_loading_panel();
    document.getElementById("data_area").style.display='inline';
    document.getElementById("data_area").innerHTML='<iframe type=text/html src=./user_administration.php style=top:0;left:0;right:0;bottom:0;width:100%;height:100%;z-index:100;margin:0px;padding:0px;scrolling:auto; margin=0px padding=0px align=left frameborder=0 scrolling=yes noresize=noresize ></iframe>';
}


document.write("<DIV id=datalist class=datalist style=left:10%;top:25%;height:60%;overflow-y:hidden; ><form id='form3' name='form3' action='"+document.URL+"' method='post' enctype='multipart/form-data' ><span id=datalist_move_bar style=position:absolute;background-color:#99CCFF;width:100%;top:0px;left:0px; ></span><fieldset id=ram ><legend id=ram_legenda ><b><?echo dictionary("datalist",$_SESSION["language"]);?></b></legend><textarea class='ckeditor' id='editor1' name='editor1' ></textarea></fieldset><div style=position:absolute;left:45%;top:0px;text-align:center; id=language_panel ></div><div style=position:absolute;left:0px;top:0px;cursor:pointer; ><input type=hidden name=target value=''><input type=submit name=in_btn3 id=in_btn3 value='<?echo dictionary("save",$_SESSION["language"]);?>' style='font-size:9px;' ></div><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=show_datalist('<?echo $_SESSION["language"];?>'); ><img src='./images/close.png' border='0' width='12px' height='12px' alt='<? echo dictionary("close",$_SESSION["language"]);?>' ></div></form></DIV>");

Drag.init(document.getElementById("datalist_move_bar"),document.getElementById("datalist"));
document.getElementById("datalist").style.display="none";


function fn_data_area(value,val1,val2,val3,val4){
      clean_loading_panel();
// if selected dictionary 
    if (value=='100_dictionary'){
     var Parent = document.getElementById("in_record");
        while(Parent.hasChildNodes()){Parent.removeChild(Parent.firstChild);}

     var row = Parent.insertRow(0);var cell1 = row.insertCell(0);var cell2 = row.insertCell(1);cell1.colSpan="2";cell1.className = 'caption';
        cell1.innerHTML="<p id='linespace' ></p><p id='linespace' ></p><b><?echo dictionary('add_new',$_SESSION['language']);?></spanb></b>";
        
        row = Parent.insertRow(1);cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary ("systemname",$_SESSION["language"]);?>';
            cell2.innerHTML="<input type=text name=in_value1 value='' style=text-align:center; />";

        row = Parent.insertRow(2);cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary ("image",$_SESSION["language"]);?>';
            cell2.innerHTML='<input type=file id=in_file1 name=in_file1 style=width:100%; >';
                
        <?$fn_plus=3;
        @$sql =  "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql("100_dictionary")."' AND COLUMN_NAME like 'lang[_]%' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
         ?>
        row = Parent.insertRow(<?echo $fn_plus;?>);cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary ($row[0],$_SESSION["language"]);?>';
            cell2.innerHTML='<input type=text id=in_value<?echo $fn_plus;?> name=in_value<?echo $fn_plus;?> style=width:100%; />';
         <?$fn_plus++;     
        }sqlsrv_close($conn);?>
         
        
        row = Parent.insertRow(<?echo $fn_plus;?>);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);cell1.colSpan="2";cell1.style.textAlign="right";
        cell1.innerHTML='<input type=submit id=in_btn1 name=in_btn1 value="<?echo dictionary("save",$_SESSION["language"]);?>" />';
    } else 
    
    { // if not selected dictionary
         
     var Parent = document.getElementById("in_record");
        while(Parent.hasChildNodes()){Parent.removeChild(Parent.firstChild);}

     var row = Parent.insertRow(0);var cell1 = row.insertCell(0);var cell2 = row.insertCell(1);cell1.colSpan="2";cell1.className = 'caption';
        cell1.innerHTML="<p id='linespace' ></p><p id='linespace' ></p><b><?echo dictionary('add_new',$_SESSION['language']);?></spanb></b>";
        
        row = Parent.insertRow(1);cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary ("sequence",$_SESSION["language"]);?>';
            cell2.innerHTML="<input type=text name=in_value4 value='' style=text-align:center; />";

        row = Parent.insertRow(2);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<span id=data_type_name ></span>';
            cell2.innerHTML="<input type=text name=in_value1 value='' style=text-align:center; />";
        

        row = Parent.insertRow(3);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary("systemname",$_SESSION["language"])?>';
            cell2.innerHTML="<input type=text name=in_value2 value='' style=text-align:center; />";
        

        row = Parent.insertRow(4);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary("bind",$_SESSION["language"])?>';
            cell2.innerHTML="<select id=in_value5 name=in_value5 style=width:100%; disabled ></select>";

        row = Parent.insertRow(5);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary("image",$_SESSION["language"])?>';
            cell2.innerHTML="<input type=file id=in_file1 name=in_file1 style=width:100%; >";

        row = Parent.insertRow(6);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary("datalist",$_SESSION["language"])?>';
            cell2.innerHTML="<input id=btn_datalist onclick=show_datalist('<?echo $_SESSION["language"];?>'); type=button value='<?echo dictionary("open",$_SESSION["language"]);?>' disabled=disabled />";

        row = Parent.insertRow(7);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);
            cell1.innerHTML='<?echo dictionary("system_function",$_SESSION["language"])?>';
            cell2.innerHTML="<select id=in_value8 name=in_value8 style=width:100%; disabled ><option value='0' ><?echo dictionary("no",$_SESSION["language"]);?></option><option value='1' ><?echo dictionary("yes",$_SESSION["language"]);?></option></select>";

        row = Parent.insertRow(8);
        cell1 = row.insertCell(0);cell2 = row.insertCell(1);cell1.colSpan="2";cell1.style.textAlign="right";
            cell1.innerHTML='<input type=submit id=in_btn1 name=in_btn1 value="<?echo dictionary("save",$_SESSION["language"]);?>" />';

    document.getElementById('data_type_name').innerHTML=val2;
    if (val4 == true ){enable_object('in_value8');}
        else {disable_object('in_value8');}
        
    if (val3){enable_object('in_value5');}
        else {fn_clear_params('select','in_value5');disable_object('in_value5');}
                
    if (value === '100_nomenclature_group'){enable_object('in_file1');}
        else   {document.getElementById('in_record').style.display='none';}
    } 
      
    document.getElementById(val1).style.display='inline';

    if (value) {document.getElementById('in_record').style.display='inline';}
        else   {document.getElementById('in_record').style.display='none';}

     document.getElementById(val1).innerHTML='';
     script=document.createElement('script');
script.src="./ajax_functions.php?karat_catalog_sett_data_area="+val1+"&table="+value+"&parent="+val3;
document.getElementsByTagName('head')[0].appendChild(script);

}



function show_datalist(value){
    if (document.getElementById("datalist").style.display!="none") {document.getElementById("datalist").style.display="none";close_tab();
        }  else {
     script=document.createElement('script');
     script.src="./ajax_functions.php?language_panel="+value;
     document.getElementsByTagName('head')[0].appendChild(script);
            
    temp=document.getElementById('sel_value1');
    document.getElementById("target").value=temp.options[temp.selectedIndex].value+"*:*"+document.getElementById('in_value3').value+"*:*"+value;
            client = new XMLHttpRequest();
            client.open('GET', './ajax_functions.php?ckedit='+document.getElementById('in_value3').value+'&table='+temp.options[temp.selectedIndex].value+'&data_language='+value );
            client.onreadystatechange = function() {
                CKEDITOR.instances.editor1.setData(client.responseText);
            }
    client.send();            
            
    document.getElementById("datalist").style.display="inline";open_tab("datalist");    
    }
}



function load_editor_data(value){
         script=document.createElement('script');
     script.src="./ajax_functions.php?language_panel="+value;
     document.getElementsByTagName('head')[0].appendChild(script);

    temp=document.getElementById('sel_value1');
    
    document.getElementById("target").value=temp.options[temp.selectedIndex].value+"*:*"+document.getElementById('in_value3').value+"*:*"+value;
            client = new XMLHttpRequest();
            client.open('GET', './ajax_functions.php?ckedit='+document.getElementById('in_value3').value+'&table='+temp.options[temp.selectedIndex].value+'&data_language='+value );
            client.onreadystatechange = function() {
            CKEDITOR.instances.editor1.setData(client.responseText);
            }
    client.send();
}



function check_delete (value1,value2,value3){  
    var temp= '?sel_value1='+value3;
    if (confirm("<?echo dictionary("del_record",$_SESSION["language"])." ";?>"+value2)) (mssql_delete_item(value3,value1,temp))
}



function fn_image_del (value1,value2,value3){
    var temp= '?sel_value1='+value3;
    if (confirm("<?echo dictionary("del_image",$_SESSION["language"])." ";?>"+value2)) (mssql_delete_image(value3,value1,temp))
}


function edit_record (value1,value2,value3,value4){ //table & record id
    document.getElementById('editor1').value='';fn_off_display('datalist');
  
script=document.createElement('script');
    script.src="./ajax_functions.php?edit_data_id="+value2+"&table="+value1+"&parent="+value3+"&sys_fn="+value4;
document.getElementsByTagName('head')[0].appendChild(script);

}


var editor = CKEDITOR.replace( 'editor1' );
CKFinder.setupCKEditor( editor, { basePath : '/catalog/modules/ckeditor/ckfinder' } ) ;

document.getElementById('data_area').style.display='none';
document.getElementById('in_record').style.display='none';

</script>





