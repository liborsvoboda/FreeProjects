<script type="text/JavaScript">
javascript:window.history.forward(0);



function cancelBack() {  
    if ((event.keyCode == 8 ||   
        (event.keyCode == 37 && event.altKey) ||   
        (event.keyCode == 39 && event.altKey))  
         &&   
        (event.srcElement.form == null || event.srcElement.isTextEdit == false)  
       )  
    {  
        event.cancelBubble = true;  
        event.returnValue = false;  
    }  
} 
 

//function Disable() {
//if (event.button == 2)
//{
//alert("Akce je Zakázána!!")
//}}

//document.onmousedown=Disable;

</script>


 <script type="text/javascript">
function doScroll(){
  if (window.name) window.scrollTo(0, window.name);
 }
</script>


<SCRIPT style="text/javascript">

document.write('<DIV id="loading" style=z-index:100;><div style=margin-top:10px;margin-left:5px; ><?$cykl=1;while($cykl<12):echo "<span id=loading_".$cykl." style=width:15px;height:15px;background-color:#ffffff; ></span><span style=width:5px;></span>";$cykl++;endwhile;echo"</div><div style=padding-top:5px; >".dictionary("please_wait",$_SESSION["language"]);?></div></DIV>');


window.onload=function(){
    document.getElementById("loading").style.display="none";doScroll();
}


function coloring_loading_panel(){
    for ( ij=1; ij < 12; ij++) {
        if (document.getElementById('loading_'+ij).style.backgroundColor == "#ffffff" && ij < 12) {
            document.getElementById('loading_'+ij).style.backgroundColor="#ff0000";return false;
            }
    }
    for ( jj=1; jj < 12; jj++) {
        document.getElementById('loading_'+jj).style.backgroundColor="#ffffff";
    }
}    

function isNumber(obj) {
    return !isNaN(parseFloat(obj)) 
}

function clean_loading_panel(){
    document.getElementById('loading').style.display='inline';
    for ( jj=1; jj < 12; jj++) {
        document.getElementById('loading_'+jj).style.backgroundColor="#ffffff";
    }
}

function allow_when_selected(object_type,selected_object,allow_object,specific_values){
// multi format with "/" separator
//can be used targeting selected_object on specific value and allow some object
// all disable = DISABLE / value=value / none_value ass disable " "
 
object_type = object_type.split('|');
selected_object = selected_object.split('|');

if (specific_values != 'DISABLE'){specific_values = specific_values.split('|');}

    i=0;document.getElementById(allow_object).disabled=false;
    while (object_type[i]){
        inc_object = document.getElementById(selected_object[i]);

      if (object_type[i] ==='select'){
        try {inc_object.options[inc_object.selectedIndex].value}
        catch ( e ) {if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                        document.getElementById(allow_object).disabled=true;alert("block");}}
            if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                if (specific_values[i] === inc_object.options[inc_object.selectedIndex].value ){document.getElementById(allow_object).disabled=true;}
            }
      }
      
      if (object_type[i] ==='input'){
        try {inc_object.value}
        catch ( e ) {if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                        document.getElementById(allow_object).disabled=true;}}
        if (inc_object.value === '' || inc_object.value === ' '){document.getElementById(allow_object).disabled=true;}
            if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                if ( specific_values[i] === inc_object.value ){document.getElementById(allow_object).disabled=true;}
            }
      }

      i++;
    }  
}


// charset solution?
document.write('<iframe id="sesswindow" style=position:absolute;right:50px;z-index:100;><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /></iframe>');
document.getElementById("sesswindow").style.display="none";

function activate_field(value){
var pocet=0;
if (document.getElementById(value).disabled==true) {document.getElementById(value).disabled=false;pocet=1;}
if (document.getElementById(value).disabled==false && pocet==0) {document.getElementById(value).disabled=true;}
}

function get_object_size(value){
    var a = document.getElementById(value).clientHeight;
    var b = document.getElementById(value).clientWidth;
return(a,b);
}


</script>
