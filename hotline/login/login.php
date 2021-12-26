<?php if (isset($_POST["user"])){
    
$results=mysql_query("select rights,language,sysadmin,end_date from login where loginname='".securesql($_POST['user'])."' and account_type='active_directory' ");
    
if (!@mysql_num_rows($results)) {$results=mysql_query("select rights,language,sysadmin,end_date from login where loginname='".securesql($_POST['user'])."' and loginpass='".securesql(MD5($_POST['password']))."' ");}
else { //LDAP login control
$ldaphost = 'ldap://'.@mysql_result(mysql_query("select param from mainsettings where id=8 "),0,0);
$ldapport = @mysql_result(mysql_query("select param from mainsettings where id=9 "),0,0);
$ds = ldap_connect($ldaphost, $ldapport) or die (message(dictionary("ldap_connect_failed",$_SESSION["language"])));
    ldap_set_option($ldapconn, LDAP_OPT_PROTOCOL_VERSION, 3);
    ldap_set_option($ldapconn, LDAP_OPT_REFERRALS, 0);
    ldap_set_option($ds, LDAP_OPT_DEBUG_LEVEL, 7);
if ($ds) {
        $domain_name = @mysql_result(mysql_query("select param from mainsettings where id=10 "),0,0);
        $domain_name=explode("@",$domain_name);
    $username = $_POST['user']."@".$domain_name[1];
    $upasswd = $_POST['password'];
    $ldapbind = ldap_bind($ds, $username, $upasswd);
        if (!@$ldapbind) {unset($results);}
    }
}


if (!@mysql_num_rows($results)) {$_SESSION['lnamed']="";session_destroy();?><script type="text/javascript">alert("<?echo dictionary('badlogon',$_SESSION['language']);?>");setTimeout('<?echo mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0);?>', 10000);</script><?}

if (@mysql_num_rows($results) && @mysql_result($results,0,3) <= $dnes && @mysql_result($results,0,3)<>"0000-00-00" ){$_SESSION['lnamed']="";session_destroy();unset($results);?><script language="JavaScript">alert("<?echo dictionary('disabled_account',$_SESSION['language']);?>");setTimeout('<?echo mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0);?>', 10000);</script><?}

if (@mysql_num_rows($results)) {session_unset();
//    require_once('./functions/php/unset.inc.php');
$_SESSION['lnamed']=$_POST['user'];
$_SESSION['language']=mysql_result($results,0,1);
$_SESSION['userights']=mysql_result($results,0,0);
$_SESSION['sysadmin']=mysql_result($results,0,2);


$detail=explode(",",mysql_result($results,0,0));
$load=1;while(@$detail[$load]):
    $_SESSION['use_det_rights'][$load]=@$detail[$load];
$load++;endwhile;

mysql_query ("update login  set lastlogin = '$dnest' where loginname = '".securesql($_POST['user'])."' ")or Die(MySQL_Error());
?><script type="text/javascript">
if (window.location.hash){window.location.href="<?echo mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0);?>"+window.location.hash;}
    else {window.location.href="<?echo mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0);?>";}
window.location.href="<?echo mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0);?>"+window.location.hash;
//window.open('<?echo mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0);?>','NEWAPP','toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=no,resizeable=yes,fullscreen=no,width=800,height=600,left=0,top=0');
//parent.window.close();
</script><?
}}?>



