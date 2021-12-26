<script type="text/JavaScript"> 
 
 function loadXMLDoc(dname) 
{
if (window.XMLHttpRequest)
  {
  xhttp=new XMLHttpRequest();
  }
else
  {
  xhttp=new ActiveXObject("Microsoft.XMLHTTP");
  }
xhttp.open("GET",dname,false);
xhttp.send();
return xhttp.responseXML;
}


//fn_off_display("result_image");
var temp_selected_value,function_result,note_temp;

function window_check(){
    close_tab();clean_loading_panel();
        var frame1_target = document.getElementById('program_body');
       try{
        if (frame1_target.contentWindow.document.getElementById('request_form').innerHTML){
            fn_result = true;
                load_demand();
                window_change();
        }
       } catch ( e ){
           
       }
}



function disabling_start_objects(value,value1){
try{
    if (value != "" ){       
        var frame1_target = document.getElementById(value);
        var frame1_content = frame1_target.contentWindow.document.body.innerHTML;
        frame1_target.onload = function() {
        frame1_target.contentWindow.document.getElementById(value1).disabled = true;
            if (value =="demand_form"){
                try{
                    for (var i=1; i <= 10000; i++){
                         frame1_target.contentWindow.document.getElementById('del_btn_'+i).disabled = true;
                    }
                } catch ( e ){
                    //
                }
            }
        }
    } else {
        document.getElementById(value1).disabled = true;
    }
} catch ( e ){
            //
}
    
}



document.write('<DIV id="delete_item" style=z-index:80;></DIV>');
document.getElementById("delete_item").style.display="none";


document.write('<DIV id="atypical_sizes" style=z-index:80;><div style=width:100%;text-align:center; ><?echo dictionary("insert_atyp_sizes",$_SESSION["language"]);?></div><div style="width:100%;height:22px;text-align:center;font-family:verdana;font-size:12px;font-weight:normal;vertical-align:center;padding-top:8px;"><input id=atype_height name=atype_height onclick=select(); type=text value="<?echo dictionary("height",$_SESSION["language"]);?>" style="width:100px;text-align:center;background: url(./images/input_100_22.png) no-repeat left top;border:0px;height:22px;padding-top:3px;" ><span style=width:10px; ></span><input id=atype_width name=atype_width onclick=select(); type=text value="<?echo dictionary("width",$_SESSION["language"]);?>" style="width:100px;text-align:center;background: url(./images/input_100_22.png) no-repeat left top;border:0px;height:22px;padding-top:3px;" ><span style=width:10px; ></span><input name=atype_depth id=atype_depth onclick=select(); type=text value="<?echo dictionary("depth",$_SESSION["language"]);?>" style="width:100px;text-align:center;background: url(./images/input_100_22.png) no-repeat left top;border:0px;height:22px;padding-top:3px;" ><span style=width:10px; ></span><input name=atype_amount id=atype_amount onclick=select(); type=text value="<?echo dictionary("pcs",$_SESSION["language"]);?>" style="width:100px;text-align:center;background: url(./images/input_100_22.png) no-repeat left top;border:0px;height:22px;padding-top:3px;" ></div><div style="width:100%;height:40px;text-align:center;font-family:verdana;font-size:12px;font-weight:normal;vertical-align:center;padding-top:8px;" ><textarea id=atype_note  name=atype_note style="width:430px;height:55px;border:0px;background: url(./images/input_445_55.png) repeat left top;" rows=3 onclick="select();" ><?echo dictionary("note",$_SESSION["language"]);?></textarea></div><div style="width:100%;height:22px;text-align:center;font-family:verdana;font-size:12px;font-weight:normal;vertical-align:center;padding-top:8px;"><span style=width:20px; ></span><span style="width:150px;background: url(./images/add_item_button.png) no-repeat left top;border:0px;height:22px;padding-top:3px;cursor:pointer;color:#ffffff;font-family:verdana;font-size:12px;font-weight:bold;" onmouseout=this.style.color="white" onmouseover=this.style.color="#F2DE41" onclick=insert_atyp(document.getElementById("atype_amount").value,document.getElementById("atype_note").value,document.getElementById("atype_height").value+"x"+document.getElementById("atype_width").value+"x"+document.getElementById("atype_depth").value); ><?echo dictionary("add_to_demand",$_SESSION["language"]);?></span><span style=width:20px; ></span><span style="width:80px;background: url(./images/no.png) no-repeat left top;border:0px;height:22px;padding-top:3px;cursor:pointer;color:#ffffff;font-family:verdana;font-size:12px;font-weight:bold;" onmouseout=this.style.color="white" onmouseover=this.style.color="#F2DE41" onclick="close_tab();" ><?echo dictionary("cancel",$_SESSION["language"]);?></span></div></DIV>');
document.getElementById("atypical_sizes").style.display="none";

document.write('<DIV onclick=document.getElementById("full_page").style.display="none"; id="full_page" ><DIV onclick=document.getElementById("full_page").style.display="none"; id="present_leaflet" ></DIV></DIV>');
document.getElementById("full_page").style.display="none";

function insert_atyp(value1,value2,value3){
   var temp = value3.split("x"); 
    if (isNaN(value1) == true || value1 == ""){
                    document.getElementById('atype_amount').style.backgroundColor="#FEBEBE";
                    document.getElementById('atype_amount').focus();
                    document.getElementById('atype_amount').select();
         } else {
        if (isNaN(temp[0]) == true || temp[0] == "" || isNaN(temp[1]) == true || temp[1] == "" || isNaN(temp[2]) == true || temp[2] == ""){
            if (isNaN(temp[2]) == true || temp[2] == ""){
                        document.getElementById('atype_depth').style.backgroundColor="#FEBEBE";
                        document.getElementById('atype_depth').focus();
                        document.getElementById('atype_depth').select();
             }
            if (isNaN(temp[1]) == true || temp[1] == ""){
                        document.getElementById('atype_width').style.backgroundColor="#FEBEBE";
                        document.getElementById('atype_width').focus();
                        document.getElementById('atype_width').select();
             }
                         if (isNaN(temp[0]) == true || temp[0] == ""){
                        document.getElementById('atype_height').style.backgroundColor="#FEBEBE";
                        document.getElementById('atype_height').focus();
                        document.getElementById('atype_height').select();
             }

        } else {    
                script = document.createElement('script');
                script.src = './ajax_functions.php?product_working=ATYPE_ADD&amount='+value1+'&note='+value2+'&sizes='+value3;   
                document.getElementsByTagName('head')[0].appendChild(script);
                clean_loading_panel();
                close_tab();
        }
    }
}





function fn_atypical_sizes (value){ fn_check_standard(); 
close_tab();
    if (value == 'other' ) {
             sel_height=document.getElementById('height');
             sel_height=sel_height.options[sel_height.selectedIndex].innerHTML;
             if (document.getElementById('height').options[document.getElementById('height').selectedIndex].value != 'other' && document.getElementById('height').options[document.getElementById('height').selectedIndex].value != '' ){document.getElementById('atype_height').value=sel_height;}
                else {document.getElementById('atype_height').value='<?echo dictionary("height",$_SESSION["language"]);?>';document.getElementById('height').selectedIndex=0;}
             

             sel_width=document.getElementById('width');
             sel_width=sel_width.options[sel_width.selectedIndex].innerHTML;
             if (document.getElementById('width').options[document.getElementById('width').selectedIndex].value != 'other' && document.getElementById('width').options[document.getElementById('width').selectedIndex].value != '' ){document.getElementById('atype_width').value=sel_width;}
                else {document.getElementById('atype_width').value='<?echo dictionary("width",$_SESSION["language"]);?>';document.getElementById('width').selectedIndex=0;}
             
        
             sel_depth=document.getElementById('depth');
             sel_depth=sel_depth.options[sel_depth.selectedIndex].innerHTML;
             if (document.getElementById('depth').options[document.getElementById('depth').selectedIndex].value != 'other' && document.getElementById('depth').options[document.getElementById('depth').selectedIndex].value !='' ){document.getElementById('atype_depth').value=sel_depth;}
                else {document.getElementById('atype_depth').value='<?echo dictionary("depth",$_SESSION["language"]);?>';document.getElementById('depth').selectedIndex=0;}
             
document.getElementById('atype_amount').value="<?echo dictionary("pcs",$_SESSION["language"]);?>";
document.getElementById('atype_note').value="<?echo dictionary("note",$_SESSION["language"]);?>";

        document.getElementById("atypical_sizes").style.display="inline";open_tab("atypical_sizes");
    } else { //fn_check_standard(); //check if is standard only  
    }  
   
}

function confirmed_delete(value,value1){
    clean_loading_panel();
        script = document.createElement('script');
    if (value !="FULL"){script.src = './ajax_functions.php?product_working=DEL&item_line_id='+value+'&frm_type='+value1;}
        else {
            script.src = './ajax_functions.php?product_working=FULL';}    
        document.getElementsByTagName('head')[0].appendChild(script);
        close_tab();
}


function targeting_selected_unit(value){  // target product from demand list
    clean_loading_panel();
    script = document.createElement('script');
    script.src = './ajax_functions.php?target_list='+value;
    document.getElementsByTagName('head')[0].appendChild(script);
    
}


function regenerate_session_permit(value){
    reg_session=value;
}

function calculate_global_discount(value,value1){
    close_tab();clean_loading_panel();
    script = document.createElement('script');
    value= value.split("%");
    parent.document.getElementById("sesswindow").src ='./functions/php/product.inc.php?global_discount='+value[0];
    parent.document.getElementById("sesswindow").onload = function() {
        script.src = './ajax_functions.php?product_working=DISCOUNT'+'&frm_type='+value1;
        document.getElementsByTagName('head')[0].appendChild(script);
    }
}


function calculate_line_sum(value1,value2,value3){
    close_tab();clean_loading_panel();
    script = document.createElement('script');
        script.src = './ajax_functions.php?product_working=AMOUNT&amount='+value2+'&item_line_id='+value1+'&frm_type='+value3;
        document.getElementsByTagName('head')[0].appendChild(script);
}


function note_change(value1,value2,value3){
    close_tab();clean_loading_panel();
    script = document.createElement('script');
        script.src = './ajax_functions.php?product_working=NOTE&note='+value2+'&item_line_id='+value1+'&frm_type='+value3;
        document.getElementsByTagName('head')[0].appendChild(script);
}



function window_change(){
        close_tab();clean_loading_panel();
        var frame1_target = document.getElementById('program_body');
        var frame1_content = frame1_target.contentWindow.document.body.innerHTML;
        var frame2_target = document.getElementById('demand_form');
        var frame2_content = frame2_target.contentWindow.document.body.innerHTML;
    
            document.frames('program_body').document.body.innerHTML=frame2_content;
            document.frames('demand_form').document.body.innerHTML=frame1_content;
               try{ 
                   if (frame1_target.contentWindow.document.getElementById('request_form').innerHTML){
                        try{
                            frame1_target.contentWindow.document.getElementById('demand_reload').disabled = false;
                            for (var i=1; i <= 10000; i++){
                                frame1_target.contentWindow.document.getElementById('del_btn_'+i).disabled = false;
                            }
                        } catch ( e ){
                                //
                        }
                   }
               } catch ( e ){
                    try{frame2_target.contentWindow.document.getElementById('demand_reload').disabled = true;
                        for (var i=1; i <= 10000; i++){
                            frame2_target.contentWindow.document.getElementById('del_btn_'+i).disabled = true;
                            }
                        } catch ( e ){
                                //
                        }
               }
        document.getElementById('loading').style.display='none';
}


{
//CKEDITOR.replace( "editor1", {toolbar:'Basic'} );
CKEDITOR.replace( 'editor1', { customConfig : 'basic_config.js' } );
}

function reloadCaptcha() {
    img = document.getElementById('send_image');
    img.src = "./modules/captcha/securimage_show-send.php?spid=" + Math.random();
}


function logout(){
close_tab();window_check();clean_loading_panel();
    parent.document.getElementById("sesswindow").src ='./functions/php/unset.inc.php';
     window.location.assign('<?
        @$sql = "select systemname from dbo.[100_main_setting] where data_type='app_url' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
        echo dictionary(@$row[0],$_SESSION['language']);?>');
}

    
function fn_group_image(value){
    disable_sizes();

    if (document.getElementById('submenu'+value).style.display == "inline" ){document.getElementById('group_image'+value).src='./images/minus.png';}
        else {document.getElementById('group_image'+value).src='./images/plus.png';}
}
    
function fn_close_other_menu(val1,val2){
   for(i = 1; i <= val2; i++)
   {
    if ( i != val1 ){
        document.getElementById('group_image'+i).src='./images/plus.png';
        document.getElementById('submenu'+i).style.display = "none";}
   }
}




function load_demand(value){
    clean_loading_panel();
    if (value == 'DEL' || value == 'DISCOUNT' || value == 'AMOUNT' || value == 'NOTE') {
        parent.document.frames('program_body').location.href='./ajax_functions.php?load_demand='+value;
        parent.document.getElementById('program_body').onload = function() {
            parent.document.getElementById('program_body').contentWindow.scrollTo(0,parent.document.getElementById('program_body').contentWindow.document.body.scrollHeight);
        }
    }
    else 
    {
                temp = document.frames('demand_form').location.href; 
                document.frames('demand_form').location.href='./ajax_functions.php?load_demand=MENU';
                document.getElementById('demand_form').onload = function() {
                    parent.document.getElementById('demand_form').contentWindow.document.getElementById('demand_reload').disabled = true;
                        try{
                            for (var i=1; i <= 10000; i++){
                                 parent.document.getElementById('demand_form').contentWindow.document.getElementById('del_btn_'+i).disabled = true;
                            }
                        } catch ( e ){
                                //
                        }
                    window.scrollTo(0, this.window);
                    parent.document.getElementById('demand_form').contentWindow.scrollTo(0,parent.document.getElementById('demand_form').contentWindow.document.body.scrollHeight);
                }
    }
                    document.getElementById('loading').style.display='none';

}



function check_demand_step(value){
close_tab();clean_loading_panel();    
            function_result = true;
            script = document.createElement('script');
            script.src = './ajax_functions.php?check_demand_step='+value;
            document.getElementsByTagName('head')[0].appendChild(script);
}


function finalize_demand(value){
    try{
                temp = document.frames('program_body').location.href;
                document.frames('program_body').location.href='./ajax_functions.php?finalize_demand='+value;
                document.getElementById('program_body').onload = function() {
                    window.scrollTo(0, this.window);
                    document.getElementById('loading').style.display='none';
                }
    }
    catch ( e ){
        document.getElementById('loading').style.display='none';
    } 
}





function doScroll(){
  if (window.name) window.scrollTo(0, window.name);
}



function search_product(value){
    clean_loading_panel();
    script = document.createElement('script');
    script.src = './ajax_functions.php?search_product='+value;
    document.getElementsByTagName('head')[0].appendChild(script);
}



function load_datalist(value,value1){ // table,id
    close_tab();
    window_check();
    if (value && value1){
            if (value === "100_nomenclature_group"){temp_selected_value = value1;}
                clean_loading_panel();
                temp = document.frames('program_body').location.href; 
                document.frames('program_body').location.href='./ajax_functions.php?load_datalist='+value1+'&table='+value+'&nomen='+temp_selected_value;
                document.getElementById('program_body').onload = function() {
                    //last content of datalist
                    if (document.frames('program_body').document.body.innerHTML==""){
                        document.frames('program_body').location.href=temp;   
                    }
   var frame1_target = document.getElementById('program_body');
   try{if (frame1_target.contentWindow.document.getElementById('confirm_amount')){
        frame1_target.contentWindow.document.getElementById('confirm_amount').disabled=true;
        frame1_target.contentWindow.document.getElementById('confirm_button').disabled=true;
    }
   }
    catch ( e ){
        //
    } 
                    // end of last content
                load_demand();    
                }
                
    }
}





function change_language(value){
    script = document.createElement('script');
    script.src = './ajax_functions.php?sess_language='+value;
    document.getElementsByTagName('head')[0].appendChild(script);
 window.parent.location.reload();   
}



function fn_check_standard(){
    window_check(); 
     for (var i=1; i <= radiocount; i++){
        if (document.getElementById('pict_radio'+i).src.match('radio_on.png') !== null){
        model = document.getElementById('pict_radio'+i).value;            
        }
        
        
    //    if (document.getElementById('pict_radio'+i).src) {
    //        model = document.getElementById('radio'+i).value;
    //    }

     }

     sel_height=document.getElementById('height');
     sel_height=sel_height.options[sel_height.selectedIndex].innerHTML;
     
     sel_width=document.getElementById('width');
     sel_width=sel_width.options[sel_width.selectedIndex].innerHTML;

     sel_depth=document.getElementById('depth');
     sel_depth=sel_depth.options[sel_depth.selectedIndex].innerHTML;

        if  (sel_height && sel_width && sel_depth && model){ //control all sizes
                       
            load_demand();
            clean_loading_panel();
            script = document.createElement('script');
            script.src = './ajax_functions.php?std_product='+model+'&height='+sel_height+"&width="+sel_width+"&depth="+sel_depth;
            document.getElementsByTagName('head')[0].appendChild(script);

//            var frame1_target = document.getElementById('program_body');
//            try{if (frame1_target.contentWindow.document.getElementById('confirm_amount')){
//                frame1_target.contentWindow.document.getElementById('confirm_amount').disabled=false;
//                frame1_target.contentWindow.document.getElementById('confirm_button').disabled=false;
//             }
//            }
//            catch ( e ){
//                //
//            } 

//             load price and specific values  and specific datalist for everyone dimension or specific product? 
        }
        else {
            var frame1_target = document.getElementById('program_body');
            try{if (frame1_target.contentWindow.document.getElementById('confirm_amount')){
                frame1_target.contentWindow.document.getElementById('confirm_amount').disabled=true;
                frame1_target.contentWindow.document.getElementById('confirm_button').disabled=true;
             }
            }
            catch ( e ){
                
            } 
        }     

}

function image_radio_reset(value1){ // for original fn_clean_and_disable_objects(fn_type,fn_object), in index was:fn_clean_and_disable_objects('radio','radio-'+radiocount) 
     for (var i=1; i <= value1; i++){
        document.getElementById('pict_radio'+i).src = './images/radio_off.png'; 
        document.getElementById('pict_radio'+i).className ='radio_off'; 
     }
}


function enable_sizes(value,value1){ // radio    + add the datalist from model
     for (var i=1; i <= radiocount; i++){
            document.getElementById('pict_radio'+i).src = './images/radio_off.png';
     }

    document.getElementById(value1).src = './images/radio_on.png';
    document.getElementById(value1).className='radio_on';

    enable_object('height');
    enable_object('width');
    enable_object('depth');
     script = document.createElement('script');
     script.src = './ajax_functions.php?std_sizes='+value+'&nomen='+temp_selected_value;
     document.getElementsByTagName('head')[0].appendChild(script);

}


function disable_sizes(){
    document.getElementById('height').selectedIndex =0;disable_object('height');
    document.getElementById('width').selectedIndex =0;disable_object('width');
    document.getElementById('depth').selectedIndex =0;disable_object('depth');
}


function enable_models(value,value1){
    script = document.createElement('script');
    script.src = './ajax_functions.php?std_model='+value+'&md_type='+value1;
    document.getElementsByTagName('head')[0].appendChild(script);
}


function sel_prod(value){
    script = document.createElement('script');
    script.src = './functions/php/product.inc.php?Selected_Product='+value;
    document.getElementsByTagName('head')[0].appendChild(script);
}



function enable_items(value){
    for (b=0;b<fn_items.length;b++){
        enable_object(fn_items[b]);
        if (value != fn_items[b] ){document.getElementById(fn_items[b]).className = "product_out";}
        else {document.getElementById(fn_items[b]).className = "product_in";}
    }
}



function add_product() {
    script = document.createElement('script');
    script.src = './ajax_functions.php?product_working='+value;
    document.getElementsByTagName('head')[0].appendChild(script);
    load_demand();
}



function print_demand(value){
    if (value == "pdf"){
    window.open('./outputs/demand_template.php','demand');    
    } else  {
        alert("<?echo dictionary("bad_command",$_SESSION["language"]);?>");        
    }
    
}




function get_ip_info(){ //function only for public IP
close_tab();
window_check();
clean_loading_panel();
    script = document.createElement('script');
    script.src = './ajax_functions.php?country_info=YES';
    document.getElementsByTagName('head')[0].appendChild(script);
document.getElementById('loading').style.display='none';
}




function login(){
close_tab();
window_check();
clean_loading_panel();
    //xxxxx
    temp = document.frames('program_body').location.href; 
    document.frames('program_body').location.href='./ajax_functions.php?login=YES';
    document.getElementById('program_body').onload = function() {
        //last content of datalist
        if (document.frames('program_body').document.body.innerHTML==""){
            document.frames('program_body').location.href=temp;
        load_demand();   
        }
    document.getElementById('loading').style.display='none';
    }
    //xxxxx
        
}


function check_login_form(){
    var frame1_target = document.getElementById('program_body');
        try{if (frame1_target.contentWindow.document.getElementById('value1').value ==="" 
                || frame1_target.contentWindow.document.getElementById('value1').value ==="<?echo dictionary("client_login",$_SESSION["language"]);?>" 
                || frame1_target.contentWindow.document.getElementById('value2').value ==="" 
                || frame1_target.contentWindow.document.getElementById('value2').value ==="<?echo dictionary("password",$_SESSION["language"]);?>"
                || frame1_target.contentWindow.document.getElementById('value3').value ==="" 
                || frame1_target.contentWindow.document.getElementById('value3').value ==="<?echo dictionary("rewrite_code",$_SESSION["language"]);?>"
                ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
                    
                } else {
                    frame1_target.contentWindow.document.getElementById("login_form").submit();
                }
            }
            catch ( e ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
        } 
}







function restore_password(){
    var frame1_target = document.getElementById('program_body');
        try{if (frame1_target.contentWindow.document.getElementById('reg_value1').value ==="" 
                || frame1_target.contentWindow.document.getElementById('reg_value1').value ==="<?echo dictionary("rewrite_code",$_SESSION["language"]);?>" 
                || frame1_target.contentWindow.document.getElementById('email_check').status ==="BAD"
                || frame1_target.contentWindow.document.getElementById('email_check').status ===""
                 
                ){frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                frame1_target.contentWindow.delayed_hidden_window_status("window_status");
                } else {
                    parent.close_tab();parent.clean_loading_panel();                                        
                    frame1_target.contentWindow.document.getElementById("forgotten_password").submit();
                }
            }
            catch ( e ){
                frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                frame1_target.contentWindow.delayed_hidden_window_status("window_status");
        } 
}





function new_registration(){
close_tab();
window_check();
clean_loading_panel();
    temp = document.frames('program_body').location.href; 
    document.frames('program_body').location.href='./ajax_functions.php?new_registration=YES';
    document.getElementById('program_body').onload = function() {
        //last content of datalist
        if (document.frames('program_body').document.body.innerHTML==""){
            document.frames('program_body').location.href=temp;
        load_demand();   
        }
    document.getElementById('loading').style.display='none';
    }
}



function forgotten_password(){
close_tab();
window_check();
clean_loading_panel();
    temp = document.frames('program_body').location.href; 
    document.frames('program_body').location.href='./ajax_functions.php?forgotten_password=YES';
    document.getElementById('program_body').onload = function() {
        //last content of datalist
        if (document.frames('program_body').document.body.innerHTML==""){
            document.frames('program_body').location.href=temp;
        load_demand();   
        }
    document.getElementById('loading').style.display='none';
    }
}




function customer_profile(value){
close_tab();
window_check();
clean_loading_panel();
    temp = document.frames('program_body').location.href; 
    document.frames('program_body').location.href='./ajax_functions.php?customer_profile=YES&registration_status='+value;
    document.getElementById('program_body').onload = function() {
        //last content of datalist
        if (document.frames('program_body').document.body.innerHTML==""){
            document.frames('program_body').location.href=temp;
        load_demand();   
        }
    document.getElementById('loading').style.display='none';
    }
}


function profile_edit(){
close_tab();
window_check();
clean_loading_panel();
    temp = document.frames('program_body').location.href; 
    document.frames('program_body').location.href='./ajax_functions.php?profile_edit=YES';
    document.getElementById('program_body').onload = function() {
        //last content of datalist
        if (document.frames('program_body').document.body.innerHTML==""){
            document.frames('program_body').location.href=temp;
        load_demand();   
        }
    document.getElementById('loading').style.display='none';
    }
}




function demand_archive(value1,value2,value3){
    if (value1 === '<?echo dictionary("by_demand_no",$_SESSION["language"]);?>' ) {var value1="";}
    if (value2 == '<?echo dictionary("by_catalog_no",$_SESSION["language"]);?>' ) {var value2="";}
    if (value3 == '<?echo dictionary("by date",$_SESSION["language"]);?>' ) {var value3="";}
close_tab();
window_check();
clean_loading_panel();
    temp = document.frames('program_body').location.href; 
    document.frames('program_body').location.href='./ajax_functions.php?demand_archive=YES&v1='+value1+'&v2='+value2+'&v3='+value3;
    document.getElementById('program_body').onload = function() {
        //last content of datalist
        if (document.frames('program_body').document.body.innerHTML==""){
            document.frames('program_body').location.href=temp;
        load_demand();   
        }
    document.getElementById('loading').style.display='none';
    }
}



function new_registration_request(){
    var frame1_target = document.getElementById('program_body');
        try{if (frame1_target.contentWindow.document.getElementById('value1').value ==="" 
                || frame1_target.contentWindow.document.getElementById('value1').value ==="<?echo dictionary("client_login",$_SESSION["language"]);?>" 
                || frame1_target.contentWindow.document.getElementById('value2').value ===""
                || frame1_target.contentWindow.document.getElementById('value2').value ==="<?echo dictionary("password",$_SESSION["language"]);?>" 
                || frame1_target.contentWindow.document.getElementById('value3').value ===""
                || frame1_target.contentWindow.document.getElementById('value3').value ==="<?echo dictionary("rewrite_code",$_SESSION["language"]);?>"
                || frame1_target.contentWindow.document.getElementById('reg_value9').value ===""
                
                ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
                } else {
                    parent.close_tab();parent.clean_loading_panel();
                    frame1_target.contentWindow.document.getElementById("new_registration").submit();
                }
            }
            catch ( e ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
        } 
}







function edit_registration_request(){
    var frame1_target = document.getElementById('program_body');
        try{if (frame1_target.contentWindow.document.getElementById('value2').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value9').value ===""
        
        ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
                } else {
                    parent.close_tab();parent.clean_loading_panel();
                    frame1_target.contentWindow.document.getElementById("edit_form").submit();
                }
            }
            catch ( e ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
        } 
}


function save_delivery_address(){
    
    var frame1_target = document.getElementById('program_body');
        try{if (frame1_target.contentWindow.document.getElementById('reg_value14').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value14').value ==="<?echo dictionary("new",$_SESSION["language"]);?>"         
         || frame1_target.contentWindow.document.getElementById('reg_value15').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value16').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value17').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value18').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value19').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value20').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value21').value ===""
         || frame1_target.contentWindow.document.getElementById('reg_value22').value ===""
        ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
                } else {
                    parent.close_tab();parent.clean_loading_panel();
                    frame1_target.contentWindow.document.getElementById("check").value="DEVA";
                    frame1_target.contentWindow.document.getElementById("edit_form").submit();
                }
            }
            catch ( e ){
                    frame1_target.contentWindow.document.getElementById("window_status").style.display = "inline";
                    frame1_target.contentWindow.delayed_hidden_window_status("window_status");
        } 
}


function delete_delivery_address(){
    parent.close_tab();parent.clean_loading_panel();
    var frame1_target = document.getElementById('program_body');
    frame1_target.contentWindow.document.getElementById("check").value="DEL_DEVA";
    frame1_target.contentWindow.document.getElementById("edit_form").submit();
}



function mfcr(value) {
    clean_loading_panel();
    script = document.createElement('script');
    script.src = './modules/mfcr/mfcr.php?ico=' + value;
    document.getElementsByTagName('head')[0].appendChild(script);
}


function mfcr_check(value) {
    clean_loading_panel();
    script = document.createElement('script');
    script.src = './modules/mfcr/mfcr.php?ico=' + value+'&check="YES"';
    document.getElementsByTagName('head')[0].appendChild(script);
}






function check_username(){
close_tab();
window_check();
clean_loading_panel();
var frame1_target = document.getElementById('program_body');
frame1_target.contentWindow.document.getElementById('user_check').innerHTML='<img src=\"./images/denied.png\" width=20px height=20px>';
       try{if (frame1_target.contentWindow.document.getElementById('value1').value) {
                script = document.createElement('script');
                script.src = './ajax_functions.php?check_username='+frame1_target.contentWindow.document.getElementById('value1').value;
                document.getElementsByTagName('head')[0].appendChild(script);
            }
        }
            catch ( e ){
           document.getElementById('loading').style.display='none';     
        }
        document.getElementById('loading').style.display='none'; 
}




function check_email(value){
close_tab();
window_check();
clean_loading_panel();
var frame1_target = document.getElementById('program_body');
frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/denied.png\" width=20px height=20px>';
       try{if (frame1_target.contentWindow.document.getElementById('reg_value9').value) {
                script = document.createElement('script');
                script.src = './ajax_functions.php?check_email='+frame1_target.contentWindow.document.getElementById('reg_value9').value+'&type='+value;
                document.getElementsByTagName('head')[0].appendChild(script);
            }
        }
            catch ( e ){
           document.getElementById('loading').style.display='none';     
        }
        document.getElementById('loading').style.display='none'; 
}




function main_menu_select(value){
    try{
        for (var i=0; i <= 20; i++){
            if ( ('main_panel_menu_'+i) ==value){
                    document.getElementById('main_panel_menu_'+i).className ='fast_window_in';
                    document.getElementById('main_panel_menu_'+i).disabled =true;
            }else{
                    document.getElementById('main_panel_menu_'+i).className ='fast_window_out';
                    document.getElementById('main_panel_menu_'+i).disabled=false;
            }
         }
    }
    catch ( e ){
    }
}




function present_list(){
close_tab();clean_loading_panel();
document.getElementById("full_page").style.display="inline";
script = document.createElement('script');
script.src = './ajax_functions.php?load_present_list=YES';
document.getElementsByTagName('head')[0].appendChild(script);
document.getElementById('loading').style.display='none';
}




function delivery_address(){
close_tab();clean_loading_panel();    
var frame1_target = document.getElementById('program_body');
if (frame1_target.contentWindow.document.getElementById('new_delivery_address').checked == true)
        {frame1_target.contentWindow.document.getElementById('reg_value23').disabled = false;
            try{
             for ( i=14; i <= 22; i++) {
                frame1_target.contentWindow.document.getElementById('reg_value'+i).value = '';
            }
                for ( i=1; i <= delivery_adr_count; i++) {
                    frame1_target.contentWindow.document.getElementById('dev_addr'+i).disabled = false;
                }
            }
            catch ( e ){
            }
        }
        else 
        {frame1_target.contentWindow.document.getElementById('reg_value23').checked = false;
         frame1_target.contentWindow.document.getElementById('reg_value23').disabled = true;
         
            for ( i=14; i <= 22; i++) {
                frame1_target.contentWindow.document.getElementById('reg_value'+i).value = '';
                frame1_target.contentWindow.document.getElementById('reg_value'+i).disabled = true;
            }
            try{
                for ( i=1; i <= delivery_adr_count; i++) {
                    frame1_target.contentWindow.document.getElementById('dev_addr'+i).disabled = true;
                }
            }
            catch ( e ){
            }
         }
document.getElementById('loading').style.display='none';    
}




function new_delivery_address(value){
var frame1_target = document.getElementById('program_body');
    for ( i=14; i <= 22; i++) {
        if (frame1_target.contentWindow.document.getElementById('reg_value23').checked == true)
            {frame1_target.contentWindow.document.getElementById('reg_value'+i).disabled = false;
             frame1_target.contentWindow.document.getElementById('reg_value'+i).value = frame1_target.contentWindow.document.getElementById('reg_value'+i).default_value;
            }
            else 
            {frame1_target.contentWindow.document.getElementById('reg_value'+i).value ='';
            frame1_target.contentWindow.document.getElementById('reg_value'+i).disabled = true;
            ;
            }
    }
    if (frame1_target.contentWindow.document.getElementById('reg_value23').checked == true){
                try{
                for ( i=1; i <= delivery_adr_count; i++) {
                        frame1_target.contentWindow.document.getElementById('dev_addr'+i).disabled = true;
                    }
                frame1_target.contentWindow.document.getElementById('deva_delete').disabled=true;
                }
                catch ( e ){
                frame1_target.contentWindow.document.getElementById('deva_delete').disabled=true;
                }
    }else{
                try{
                for ( i=1; i <= delivery_adr_count; i++) {
                        frame1_target.contentWindow.document.getElementById('dev_addr'+i).disabled = false;
                    }
                }
                catch ( e ){
                    
                }
    }
}




function new_delivery_demand_address(value){
var frame1_target = document.getElementById('program_body');
    for ( i=14; i <= 22; i++) {
        if (frame1_target.contentWindow.document.getElementById('reg_value23').checked == true)
            {frame1_target.contentWindow.document.getElementById('reg_value'+i).disabled = false;
             frame1_target.contentWindow.document.getElementById('reg_value'+i).value = frame1_target.contentWindow.document.getElementById('reg_value'+i).default_value;
            }
            else 
            {frame1_target.contentWindow.document.getElementById('reg_value'+i).value ='';
            frame1_target.contentWindow.document.getElementById('reg_value'+i).disabled = true;
            ;
            }
    }
    if (frame1_target.contentWindow.document.getElementById('reg_value23').checked == true){
                try{
                for ( i=1; i <= delivery_adr_count; i++) {
                        frame1_target.contentWindow.document.getElementById('dev_addr'+i).disabled = true;
                    }
                }
                catch ( e ){
                }
    }else{
                try{
                for ( i=1; i <= delivery_adr_count; i++) {
                        frame1_target.contentWindow.document.getElementById('dev_addr'+i).disabled = false;
                    }
                }
                catch ( e ){
                    
                }
    }
    prepare_customer_data();
}




function load_delivery_address(value){
close_tab();clean_loading_panel();

script = document.createElement('script');
script.src = './ajax_functions.php?load_delivery_address='+value;
document.getElementsByTagName('head')[0].appendChild(script);
document.getElementById('loading').style.display='none';
var frame1_target = document.getElementById('program_body');
try{
    frame1_target.contentWindow.document.getElementById('deva_delete').disabled=false;
}
    catch ( e ){
    }
}



function load_delivery_demand_address(value){
close_tab();clean_loading_panel();

script = document.createElement('script');
script.src = './ajax_functions.php?load_delivery_address='+value;
document.getElementsByTagName('head')[0].appendChild(script);
document.getElementById('loading').style.display='none';
var frame1_target = document.getElementById('program_body');
}



function load_delivery_address_view(value,value1){
close_tab();clean_loading_panel();
    var frame1_target = document.getElementById('program_body');
        try{
            for ( i=1; i <= 1000; i++) {
            frame1_target.contentWindow.document.getElementById('dev_addr_'+i).disabled = false;
            frame1_target.contentWindow.document.getElementById('dev_addr_'+i).className = 'fast_window_out';
            }
        }
            catch ( e ){
        }
        
        frame1_target.contentWindow.document.getElementById('dev_addr_'+value1).className = 'fast_window_in';
        frame1_target.contentWindow.document.getElementById('dev_addr_'+value1).disabled=true;
        
script = document.createElement('script');
script.src = './ajax_functions.php?load_delivery_address_view='+value;
document.getElementsByTagName('head')[0].appendChild(script);
document.getElementById('loading').style.display='none';
}





function check_logon_status(value){
script = document.createElement('script');
script.src = './ajax_functions.php?check_logon_status='+value;
document.getElementsByTagName('head')[0].appendChild(script);
}



function prepare_customer_data(){
    var frame1_target = document.getElementById('program_body');
    try{
        for ( i=0; i <= 23; i++) {
            if (i==0){
                if (frame1_target.contentWindow.document.getElementById('new_delivery_address').checked === true) {
                    temp_selected_value = "on"; 
                } else { temp_selected_value = "off";}
            }
            else {
                if (i != 23){
                    temp_selected_value = frame1_target.contentWindow.document.getElementById('reg_value'+i).value;
                }else {
                    if (frame1_target.contentWindow.document.getElementById('reg_value'+i).checked === true) {
                        temp_selected_value = "on"; 
                    } else { temp_selected_value = "off";}
                }
            }

  if (i==13){note_temp = temp_selected_value.split( '\n' );temp_selected_value='';
    for(j = 0; j < note_temp.length; j++){
        if (j == 0){temp_selected_value = temp_selected_value + note_temp[j];}
        else {temp_selected_value = temp_selected_value +"<br />"+ note_temp[j];} 
    }
  }
  
script = document.createElement('script');
script.src =  './ajax_functions.php?sequence='+i+'&prepare_customer_data='+temp_selected_value;
document.getElementsByTagName('head')[0].appendChild(script);
}
    }
    catch ( e ){
    }
    check_demand_step("CHECK");
    parent.document.getElementById('loading').style.display='none';
}



</script>







