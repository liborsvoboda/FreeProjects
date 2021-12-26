<script type="text/JavaScript"> 

function show_records(value){
     var data_table = document.getElementById("view_table");
    for (i=1; i < (document.getElementById('view_table').rows.length -1); i++) {
        if (value == "ALL"){
            data_table.rows[i].style.display = "inline";
        }
        else {
            if ( data_table.rows[i].status == value && value !="ALL" ){ 
            data_table.rows[i].style.display = "inline";
            } else {
            data_table.rows[i].style.display = "none";
            }
        }
    }
}


</script>





