<?
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
?><html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<?require_once ("./functions/js/dragiframe.js"); ?>
</head>
<body onload="addHandle(document.getElementById('help_window'), window);" onunload="window.name=document.body.scrollTop"; onselectstart="return false;" ><div id="help_window" style=z-index:100; bringSelectedIframeToTop(true); allowDragOffScreen(true); DIF_highestZIndex=200;><p style=font-size:36px;font-weight:bolder;background:#A7C0FC; ><?echo dictionary("help",$_SESSION["language"]);?></p>
<?echo @dictionary($_GET["help"],$_SESSION["language"]);?>
<div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=parent.document.getElementById('help_frame').style.display='none'; ><img src='./images/close.png' border=0 width=12 height=12 alt='<?echo dictionary("close",$_SESSION["language"]);?>'></div>
</div>
</body>
</html>