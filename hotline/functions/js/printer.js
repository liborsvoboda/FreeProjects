<script language="JavaScript">

function check_date(){
  var object_name_from = document.getElementById('from_date');
  var object_name_to = document.getElementById('to_date');
  var date_num=object_name_from.options[object_name_from.selectedIndex].value;
  var disabling=true;

     for(var i = 0; i < object_name_to.options.length; i++) {
        
      		if(object_name_from.options[i].selected == true) {
                disabling=false;
               
                if (object_name_to.options[object_name_to.selectedIndex].value ==  "" )  {
                    object_name_to.options[i].selected = true;}
                document.getElementById('savebtn1').disabled=false;
            }

                if (disabling==true){object_name_to.options[i].disabled = true;}
                else{object_name_to.options[i].disabled = false;}
     }
}

</script>



