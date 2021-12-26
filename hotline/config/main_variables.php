<?
@$dnes=date("Y-m-d");
@$dnest=date("Y-m-d")." ".StrFTime("%H:%M:%S", Time());
@$dnescs=date("d.m.Y");

$def_dbselect="multiapp,Test_DB";
$def_language="lang_cs";

    $u_agent = $_SERVER['HTTP_USER_AGENT'];$viewer = '';
    if(preg_match('/MSIE/i',$u_agent)) {$viewer = "ie";}
    elseif(preg_match('/Firefox/i',$u_agent)) {$viewer = "firefox";}
    elseif(preg_match('/Safari/i',$u_agent)) {$viewer = "safari";}
    elseif(preg_match('/Chrome/i',$u_agent)) {$viewer = "chrome";}
    elseif(preg_match('/Flock/i',$u_agent)) {$viewer = "flock";}
    elseif(preg_match('/Opera/i',$u_agent)) {$viewer = "opera";}
?>