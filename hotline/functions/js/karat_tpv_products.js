<script type="text/JavaScript"> 

document.getElementById('checkcard').style.display='none';
document.getElementById('header_info').style.display='none';

    function checkcard(value,valuea,value1,value2,value3) {
        document.getElementById('checkcard').style.display='inline';

    script = document.createElement('script');
    script.src = './mssql_checkcard.php?value=' + value + '&valuea='+ valuea + '&value1='+ value1 + '&value2='+ value2 + '&value3='+ value3;
    document.getElementsByTagName('head')[0].appendChild(script);
}

function fn_tpv_add_info(value){
    document.getElementById('loading').style.display='inline';
    document.getElementById('header_info').style.display='inline';
        document.getElementById('header_info').innerHTML='';
     script=document.createElement('script');
script.src="./ajax_functions.php?tpv_header_info="+value;
document.getElementsByTagName('head')[0].appendChild(script);
}



function TableToExcel(value,value1){ //object, file

var header_data="<table style=background-color:#C4F5B3; ><tr><td>" + document.getElementById('hv1').value + "</td><td>" + document.getElementById('hv2').value + "</td><td>" + document.getElementById('hv3').value + "</td><td>" + document.getElementById('hv4').value + "</td></tr></table><br>";
var sum_table=document.getElementById(value).sum_table;
var tabdata=document.getElementById(value).innerHTML;
var final_tabdata=tabdata;

//alert(tabdata);
var temp_tabdata=tabdata.split(';">');
    var temp_tabdata1 = temp_tabdata[0].split('<IMG onclick=run_functs(this.field_list);');
    final_tabdata = temp_tabdata1[0] + temp_tabdata[1];

temp_tabdata="";
temp_tabdata=final_tabdata.split('/images/add.png">');
final_tabdata="";

for ( i = 0; i < temp_tabdata.length; i++) {
    var temp_tabdata1 = temp_tabdata[i].split('<IMG onclick=\'table_line_display');
    final_tabdata = final_tabdata + temp_tabdata1[0];
}

  	  with(document){
	  ir=createElement('iframe');
	  ir.id='ifr';ir.location='about.blank';ir.style.display='none';body.appendChild(ir);
		with(getElementById('ifr').contentWindow.document){
	      	   open("data:application/vnd.ms-excel","replace");write(header_data + final_tabdata + sum_table);
               close();execCommand('SaveAs',false,value1);}
	 body.removeChild(ir);}
}
  
function run_functs(value){
value = value.split(";");
    for (var i = 0; i < value.length; i++) {
        table_line_display(value[i]);
    }  
}




</script>





