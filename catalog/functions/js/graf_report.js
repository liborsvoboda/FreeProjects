<script type="text/JavaScript"> 


function load_whois(value){
   var st_temp =document.getElementById(value);
    for (i = 0; i < st_temp.options.length; i++) {
        if ( st_temp.options[i].selected == true ){
               document.getElementById("whois_field").src ="./ajax_functions.php?whois="+st_temp.options[i].value;
        }
    }
}


function load_graph(value,inc_object){ //img-id,from,to,interval
    script = document.createElement("script");
    script.src = "./ajax_functions.php?domain_liste=sel_value1";
    document.getElementsByTagName('head')[0].appendChild(script);
 
    
    document.getElementById(value).style.visibility = "visible";
    document.getElementById(value).src="./ajax_functions.php?counter=yes&counter_name=traffic_chart&from="+
    document.getElementById(inc_object+"1").options[document.getElementById(inc_object+"1").selectedIndex].value + 
    "&to=" + document.getElementById(inc_object+"2").options[document.getElementById(inc_object+"2").selectedIndex].value +
    "&interval=" + document.getElementById(inc_object+"3").options[document.getElementById(inc_object+"3").selectedIndex].value;
 
     script=document.createElement('script');
     script.src="./ajax_functions.php?domain_list=sel_value1&from="+document.getElementById(inc_object+"1").options[document.getElementById(inc_object+"1").selectedIndex].value + "&to=" + document.getElementById(inc_object+"2").options[document.getElementById(inc_object+"2").selectedIndex].value;
     
     document.getElementsByTagName('head')[0].appendChild(script);

        
}


document.getElementById("img_graph").style.visibility = "hidden";
</script>





