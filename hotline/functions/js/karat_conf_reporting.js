<script type="text/JavaScript"> 

document.write('<DIV id=whois class=input_form style=left:20%;top:35%; ><span id=whois_move_bar style=position:absolute;background-color:#99CCFF;width:100%;top:0px;left:0px;  ></span><fieldset id=ram><legend id=ram_legenda><b><?echo dictionary("whois",$_SESSION["language"]);?></b></legend><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("whois").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>" ></div><iframe id=whois_field style="width:100%;height:100%;" ></iframe></fieldset></DIV>');

Drag.init(document.getElementById("whois_move_bar"),document.getElementById("whois"));
document.getElementById("whois").style.display="none";

function load_whois(value){
document.getElementById("whois").style.display="inline";open_tab("whois");
   var st_temp =document.getElementById(value);
    for (i = 0; i < st_temp.options.length; i++) {
        if ( st_temp.options[i].selected == true ){
               document.getElementById('whois_field').src ='./ajax_functions.php?whois='+st_temp.options[i].value;
        }
    }
}


function load_graph(value,inc_object){ //img-id,from,to,interval 
    
    document.getElementById(value).style.visibility = "visible";
    document.getElementById(value).src='./ajax_functions.php?counter=yes&counter_name=traffic_chart&from='+
    document.getElementById(inc_object+"1").options[document.getElementById(inc_object+"1").selectedIndex].value + 
    '&to=' + document.getElementById(inc_object+"2").options[document.getElementById(inc_object+"2").selectedIndex].value +
    '&interval=' + document.getElementById(inc_object+"3").options[document.getElementById(inc_object+"3").selectedIndex].value;
        
}


document.getElementById('img_graph').style.visibility = "hidden";
</script>





