<?
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

?><html>
<head>
<title><?echo @mysql_result(mysql_query("select param from mainsettings where name='app_name' "),0,0);?></title>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon" />
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon" />
<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />

<?require_once ("./functions/js/main_window_functions.js");
  require_once ("./functions/js/dragiframe.js");
  require_once ("./functions/js/jquery.base64.js");

if (@$_POST["user"]  or @$_POST["password"] ) {include "./login/login.php";}
if (!isset($_SESSION['lnamed']) && !@$_REQUEST["user"] ){require_once('./functions/php/unset.inc.php');} 

?>
<script type="text/JavaScript">
<?if (isset($_SESSION['lnamed'])){?>
window.status='<?echo dictionary('logged',$_SESSION['language'])." ".@mysql_result(mysql_query("select CONCAT(name,' ',surname) from login where loginname='".securesql(@$_SESSION['lnamed'])."' "),0,0)."   (".@$_SESSION['lnamed'].")";?>';
<?}

if (@!$_SESSION['lnamed']){?>login();<?}?>
</script>
</head>
<body id=main_body onunload="window.name=document.body.scrollTop;" onkeydown="cancelBack();" >
<table id=main_window_table onselectstart="return false;" >

<tr><td colspan=2 id=options><?require_once('./main_panel.php');?><span id=openned_app ></span>
<span id=app_name ><?echo @mysql_result(mysql_query("select param from mainsettings where name='app_name' "),0,0);?></span>
</td></tr>

<tr><td id=selections><div id=selectmenu><?if (@$_SESSION["lnamed"]) {require_once('./components/units_menu.php');}?></div></td>
<td id=program>
<?if (@$viewer<>"eie") {?>
<iframe id=program_frame <?if (@!$_SESSION['lnamed']){echo "tabindex=-1";}?> onload="check_source('program_frame','loading');" type=text/html src=javascript:window.location.hash style=align:center;width:100%;height:100%;z-index:50; align=left frameborder=0 scrolling=auto noresize=noresize>
</iframe>
<?}
else {?>
<object id=program_frame <?if (@!$_SESSION['lnamed']){echo "tabindex=-1";}?> onload="check_source('program_frame','loading');" type=text/html data='' style=align:center;width:100%;height:100%;z-index:50;overflow:hidden; align=middle frameborder=0 scrolling=auto noresize=noresize >
</object>
<?}?>
</td></tr>

<form id=main_form name=main_form method='POST' >
<tr><td colspan=2 id=footer ><span style=width:50%;text-align:left; >FOOTER</span><span title='<?echo dictionary("database_selection",@$_SESSION["language"]);?>' style=text-align:right;width:50%; >
<img src='./images/database.png' border='0' width='24' height='24' style=vertical-align:middle;text-align:middle;cursor:pointer; <?if (@StrPos(" ".$_SESSION['userights'],",*dbselect:0,")) {echo " ondblclick=activate_field(\"dbselect\"); ";}?> />
<select id=dbselect name=dbselect onchange="if(confirm('<?echo dictionary("change_database",$_SESSION['language'])." ";?>'+document.getElementById('dbselect').value+' ?')) submit(this); document.getElementById('dbselect').value='<?echo $_SESSION['dbselect'];?>';activate_field(); " style=vertical-align:middle;text-align:middle; disabled='disabled' title='<?echo dictionary("selected_database",@$_SESSION["language"]);?>' ><option disabled></option>
<?
$explode=explode(",",$def_dbselect);
$load=0;while(@$explode[$load]):
    echo "<option value='".@$explode[$load]."' ";
        if (@$explode[$load]==$_SESSION['dbselect']) {echo " selected ";}
    echo " >".strtoupper(@$explode[$load])."</option>";
@$load++;endwhile;?><option disabled ></option></select></span>
</td></tr></form>
</table>



</body>
</html>

<script>
    if (window.location.hash && '<?echo @$_SESSION['lnamed'];?>' != '' ) {
var query = location.href.split('#');var temp = '<?echo @$_SESSION['userights'];?>';
query= query[1].split('.');temp = temp.split(query[0]+':');temp = temp[1].split(',');
        sesscrt(base64.encode(temp[0]));
        subprogram ("./" + window.location.hash.substring(1),"");window.location.hash='';
    }
</script>


