<?//session_destroy();
//session_set_cookie_params(21600);
//session_set_cookie_params(strtotime('tomorrow') - time() );
session_start();
unset($_SESSION['lnamed']);
//unset($_SESSION['language']);
//unset($_SESSION['dbselect']);
unset($_SESSION['sess_id']);
unset($_SESSION['product']);
unset($_SESSION['Currency_Id']);
unset($_SESSION['product_selecting']);
unset($_SESSION[$sess_id.'logged_user']);
unset($_SESSION['customer_info']);
//session_unset();
?>