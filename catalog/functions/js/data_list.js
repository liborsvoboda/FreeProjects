document.write('<DIV id="delete_item" onselectstart="return TRUE;" style=z-index:80;></DIV>');
document.getElementById("delete_item").style.display="none";


function add_product(value,value1) {
    parent.close_tab();parent.clean_loading_panel();
        value1 = value1.replace(",",".");
    if ( isNaN(value1) == true || value1 == "") {
            document.getElementById('confirm_amount').style.backgroundColor="#FEBEBE";
            document.getElementById('confirm_amount').focus();
            document.getElementById('confirm_amount').select();}
    else {
        document.getElementById('confirm_amount').style.backgroundColor="white";
        script = document.createElement('script');
        script.src = './ajax_functions.php?product_working='+value+'&amount='+value1;
        document.getElementsByTagName('head')[0].appendChild(script);
        
    }
}


function load_demand(value){
    parent.clean_loading_panel();
    if (value == 'DEL' || value == 'FULL' || value == 'DISCOUNT') {
        parent.document.frames('program_body').location.href='./ajax_functions.php?load_demand='+value;
        parent.document.getElementById('program_body').onload = function() {
            
            parent.document.getElementById('program_body').contentWindow.scrollTo(0,parent.document.getElementById('program_body').contentWindow.document.body.scrollHeight);
        }
    }
    else 
    {
           parent.document.frames('demand_form').location.href='./ajax_functions.php?load_demand='+value;
            parent.document.getElementById('demand_form').onload = function() {
                parent.document.getElementById('demand_form').contentWindow.scrollTo(0,parent.document.getElementById('demand_form').contentWindow.document.body.scrollHeight);
            }
    }
    parent.document.getElementById('loading').style.display='none';
}


function isNumber(obj) {
    return !isNaN(parseFloat(obj)) 
}


function delete_item_panel(value,value1,value2) {
    if (parent.document.getElementById('delete_item').style.display != "inline" ){
        parent.close_tab();
        parent.open_tab('delete_item');
        parent.document.getElementById('delete_item').style.display="inline";
    }  
    script = document.createElement('script');
    script.src = './ajax_functions.php?delete_item='+value+'&unit='+value1+'&frp_type='+value2;
    document.getElementsByTagName('head')[0].appendChild(script);
}



