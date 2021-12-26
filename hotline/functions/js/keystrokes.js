<SCRIPT LANGUAGE="JavaScript">
var openned_program_window = new Array();

if (document.all){
    document.onkeydown = function (){ 
        if (27==event.keyCode){close_tab();} // ESC keypressed
    }
}

function open_tab(value) {
    openned_program_window[(openned_program_window.length)] = value;
 //alert(openned_program_window[(openned_program_window.length-1)]);   
}

function close_tab() {
    if ((openned_program_window.length-1) >= 0) {
 //alert(openned_program_window[(openned_program_window.length-1)]);
        document.getElementById(openned_program_window[(openned_program_window.length-1)]).style.display = "none";
        openned_program_window.splice((openned_program_window.length-1),1);
    }
}
</script>
