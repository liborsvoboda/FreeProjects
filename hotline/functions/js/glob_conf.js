<SCRIPT LANGUAGE="JavaScript" charset="utf-8" >

document.write('<DIV id=ocr_check  class=input_form ><span id=ocr_check_move_bar class=move_ledge ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("ocr_test",$_SESSION["language"]);?></b></legend><table id=form_table ><form id=ocr_form action="./ajax_functions.php" target="subpage" method=post enctype="multipart/form-data"><tr><td style=vertical-align:top;><?echo dictionary("input_file",$_SESSION["language"]);?>:</td><td><input id=oc_value1 name=oc_value1 onchange=run_ocr(); type=file value="" style=width:400px ></td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=ocr_check(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div><iframe id=subpage name=subpage style=visibility:hidden;height:0px; ></iframe></DIV>');

document.getElementById("ocr_check").style.display="none";
Drag.init(document.getElementById("ocr_check_move_bar"),document.getElementById("ocr_check"));

document.write('<DIV id=ad_user_list class=input_form ><span id=ad_user_list_move_bar class=move_ledge ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("ad_user_list",$_SESSION["language"]);?></b></legend><table id=form_table ><form target="subpage" method=post enctype="multipart/form-data"><tr style=text-align:center; ><td style=vertical-align:top;width:245px; ><?echo dictionary("ad_user_list",$_SESSION["language"]);?></td><td style=vertical-align:top;width:55px; ></td><td style=vertical-align:top;width:245px; ><?echo dictionary("exist_users",$_SESSION["language"]);?></td></tr><tr><td><select onclick=disable_object("fn_value4");enable_object("fn_value3");document.getElementById("ad_value2").selectedIndex=-1; id=ad_value1 name=ad_value1[] style=width:245px; size="15" multiple="multiple" ><?@$ldap_users = fn_ldap_list();$cycle=0;while(@$ldap_users[$cycle]):$control=mysql_num_rows(mysql_query("select id from login where loginname = '".securesql($ldap_users[$cycle][0])."' "));if (!@$control){echo "<option value=\"".$ldap_users[$cycle][0]."\">".$ldap_users[$cycle][0]." / ".$ldap_users[$cycle][1]."</option>";}$cycle++;endwhile;?></select></td><td style=text-align:center; ><input id=fn_value3 disabled type=button name=fn_value3 value="" style=" background: url(./images/right-arrow.png); background-size: 20px 20px;font-weight:bold;width:25px;height:25px;"  onclick=move_accounts("ad_value1"); > <br /> <br /><input id=fn_value4 disabled type=button name=fn_value4 width="20" height="20" value="" style=" background: url(./images/disable.png); background-size: 20px 20px;font-weight:bold;width:25px;height:25px;" title="<?echo dictionary("disable_account",$_SESSION["language"]);?>" onclick=disable_account("ad_value2"); ></td><td><select onclick=enable_object("fn_value4");disable_object("fn_value3");document.getElementById("ad_value1").selectedIndex=-1; id=ad_value2 name=ad_value2[] style=width:245px; size="15" multiple="multiple" ><?$load_form_data=mysql_query("select concat(surname,' ',name),loginname,end_date from login where account_type='active_directory' order by loginname,id ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());$part=explode(",",mysql_result($load_form_data,0,0));$cycle=0;while(@mysql_result($load_form_data,$cycle,0)):echo"<option value=\'".@mysql_result($load_form_data,$cycle,1)."\' ";if (@mysql_result($load_form_data,$cycle,2)<>"0000-00-00" && @mysql_result($load_form_data,$cycle,2)<=$dnes ) {echo " disabled=disabled ";} echo ">".@mysql_result($load_form_data,$cycle,0)."</option>";$cycle++;endwhile;?></select></td></tr></form></table></fieldset><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=load_AD_user(); ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');

document.write('<DIV id="loading" onselectstart="return TRUE;" style=z-index:100;><BR><?echo dictionary("please_wait",$_SESSION["language"]);?><br /><img src="images/loading.gif" border="0"></DIV>');
document.getElementById("loading").style.display="none";

document.getElementById("ad_user_list").style.display="none";
Drag.init(document.getElementById("ad_user_list_move_bar"),document.getElementById("ad_user_list"));

function load_AD_user(){
    
    if (document.getElementById("ad_user_list").style.display!="none"){
        document.getElementById("ad_user_list").style.display="none";close_tab();
    } else {
        document.getElementById("ad_user_list").style.display="inline";open_tab("ad_user_list");
    }     
    
    
}



function move_accounts(value){
    selected_object = document.getElementById(value);
    	for(var i = 0; i < selected_object.options.length; i++) {
    		if(selected_object.options[i].selected == true) {
        		value=selected_object.options[i].value;
                st_temp = confirm( "<?echo dictionary("ad_create_account",$_SESSION["language"]);?> " + value + "?" );
                if (st_temp == true)
                  {
                  document.getElementById('subpage').src ="./ajax_functions.php?create_acc="+value;
                  document.getElementById("subpage").onload = function () {
                        parent.document.frames("program_frame").location.reload();      
                     }
                  }
            }
        }
}



function disable_account(value){
    selected_object = document.getElementById(value);
    	for(var i = 0; i < selected_object.options.length; i++) {
    		if(selected_object.options[i].selected == true) {
    		value=selected_object.options[i].value;}
        }
    st_temp = confirm( "<?echo dictionary("deactivate_account",$_SESSION["language"]);?> " + value + "?" );
        if (st_temp == true)
          {
          document.getElementById('subpage').src ="./ajax_functions.php?disable_acc="+value;
          document.getElementById("subpage").onload = function () {
                parent.document.frames("program_frame").location.reload();      
             }
          }
}



function ocr_check(){ 
    if (document.getElementById("ocr_check").style.display!="none"){
        document.getElementById("ocr_check").style.display="none";close_tab();
    } else {
        document.getElementById("ocr_check").style.display="inline";open_tab("ocr_check");
    }     
    
}

function run_ocr(){
    activeX_Control();
    ocr_check();
    var file_name = document.getElementById("oc_value1").value;
    var file_name_data = file_name.split("\\").pop(-1);

    var value='<?$temp = mysql_query("select * from mainsettings where id='5' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());if (mysql_result($temp,0,2) =="tesseract"){echo "".mysql_result($temp,0,2)." ";?>./temp/'+ file_name_data + '<?echo " ./temp/".mysql_result(mysql_query("select param from mainsettings where id='7' "),0,0);$temp = mysql_query("select * from mainsettings where id='6' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());if (@mysql_result($temp,0,2)){echo " -l ".mysql_result($temp,0,2);}}?>';

    
document.getElementById("loading").style.display="inline";

document.getElementById("ocr_form").submit();

var iframe = document.getElementById("subpage");

iframe.onload = function () {
    if (file_name_data.length !=0 ){
        
        document.getElementById('subpage').src ="./ajax_functions.php?command="+value;
        
        iframe.onload = function () {
            document.getElementById("loading").style.display="none";
            window.open('<?echo "./temp/".mysql_result(mysql_query("select param from mainsettings where id='7' "),0,0).".txt";?>');}
        
        file_name_data="";
    }
}
 
 // vytvorit script for delete temp folder


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

