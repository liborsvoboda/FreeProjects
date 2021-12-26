<?if (@$_SESSION['sess_id'] <> session_id() or !@$_SESSION['lnamed']) {session_unset();session_destroy();}
//session_set_cookie_params(21600);
//session_set_cookie_params(strtotime('tomorrow') - time() );
session_start();
$sess_id = session_id();
if (!$_SESSION['language']){$_SESSION['language']=$def_language;}
if (@$_POST['dbselect'])    {$_SESSION['dbselect']=$_POST['dbselect'];}
if (!$_SESSION['dbselect']){$selected=explode(",",$def_dbselect);$_SESSION['dbselect']=$selected[0];}
if (!$_SESSION['sysadmin']) {$_SESSION['sysadmin']="";}
if (!$_SESSION['desktop_no']) {$_SESSION['desktop_no']="1";}
if (!$_SESSION['sess_id']) {$_SESSION['sess_id']=$sess_id;}
if (!@$_SESSION['RTG']) {@$_SESSION['RTG']="xxx";} 
?>