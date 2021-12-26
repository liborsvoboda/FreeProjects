<?//session_set_cookie_params(21600);
//session_set_cookie_params(strtotime('tomorrow') - time() );
session_start();
if (@$_GET["Selected_Product"]){$_SESSION['Selected_Product']=@$_GET["Selected_Product"];}
    else {
        if (@$_GET['global_discount']>100){$_GET['global_discount']=100;}
        if (@$_GET['global_discount'] OR @$_GET['global_discount']==0 ) {$_SESSION['global_discount']=$_GET['global_discount'];}
    }
?>
