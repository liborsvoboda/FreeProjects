<?php if (@$_POST["user"]){
  require_once ("./modules/captcha/securimage_admin.php");
  $img = new Securimage();
  $valid = $img->check($_REQUEST["code"]);

  if($valid == true) {
        @$sql = "SELECT id,language FROM dbo.[100_login] WHERE login_name = '".mssecuresql($_POST['user'])."' and login_pw = HashBytes('MD5','".mssecuresql($_POST['password'])."') ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
  } 
  
if ($row[0]=="" or $valid == false) {
    message(dictionary('badlogon',$_SESSION['language']));
    $_SESSION['lnamed']="";session_destroy();?><script language="JavaScript">window.location.assign('<?
        @$sql = "select systemname from dbo.[100_main_setting] where data_type='admin_url' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
        echo dictionary(@$row[0],$_SESSION['language']);?>'?>')</script><?}


    else {
require_once('./functions/php/unset.inc.php');

$_SESSION['lnamed']=$_POST['user'];
$_SESSION['language']=$row[1];

    $sql_insert = "UPDATE dbo.[100_login] SET last_login='".mssecuresql($dnest)."' where login_name='".mssecuresql($_POST['user'])."' "; 
    $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));

?><script type="text/javascript">
<?
        @$sql = "select systemname from dbo.[100_main_setting] where data_type='admin_url' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
?>
window.location.href="<?echo $row[0];?>";
//window.open('<?echo $row[0];?>','NEWAPP','toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=no,resizeable=yes,fullscreen=no,width=800,height=600,left=0,top=0');
//parent.window.close();
</script><?
}}?>






