<?//session_destroy();
session_set_cookie_params(21600);
session_set_cookie_params(strtotime('tomorrow') - time() );
session_start();
unset($_SESSION['lnamed']);
//unset($_SESSION['language']);
unset($_SESSION['sysadmin']);
unset($_SESSION['userights']);
unset($_SESSION['use_det_rights']);
unset($_SESSION['RTG']);
//unset($_SESSION['dbselect']);
unset($_SESSION['desktop_no']);
unset($_SESSION['sess_id']);
//session_unset();
?>