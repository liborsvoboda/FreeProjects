<SCRIPT LANGUAGE="JavaScript">

function load_user_list(val){

	var date = new Date();
		date.setTime(date.getTime()+(10000000));
		var expires = "; expires="+date.toGMTString();
    var allcookies = document.cookie;

    if (  allcookies.search('tbl='+val) ==-1 && val!=""){document.cookie="tbl="+val+expires+"; path=/";location.reload();} 
        
        document.getElementById('<?echo $_COOKIE['tbl'];?>').disabled =true; 
        document.getElementById('<?echo $_COOKIE['tbl'];?>').className ='initems'; 
        document.getElementById('data').innerHTML ="<?include('./ajax_functions.php');?>";
        
}


</script>

