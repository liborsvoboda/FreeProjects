<?if (@$_SESSION['sess_id'] <> session_id()) {session_regenerate_id();session_destroy();}
session_start();
if (!@$_SESSION['language']){$_SESSION['language']=$def_language;}
if (!@$_SESSION['product_selecting']){$_SESSION['product_selecting']='';}
if (!@$_SESSION['product']){$_SESSION['product']='';}
if (!@$_SESSION['last_datalist']){$_SESSION['last_datalist']='';}
if (!@$_SESSION['global_discount']){$_SESSION['global_discount']='';}
if (!@$_SESSION['Selected_Product']){$_SESSION['Selected_Product']='';}
if (!@$_SESSION['Currency_Id']){$_SESSION['Currency_Id']='';}
if (!$_SESSION['sess_id']){$_SESSION['sess_id']='';}
if (!@$_SESSION['customer_info']){$_SESSION['customer_info']='';}
@$sess_id = session_id();
@$_SESSION['sess_id']=$sess_id;

if (!@$_SESSION[$sess_id.'logged_user']){$_SESSION[$sess_id.'logged_user']='';}
?>