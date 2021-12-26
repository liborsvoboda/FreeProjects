<html>
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">

<!--// lightbox     picture
    <link rel="stylesheet" href="./ckeditor/lightbox/css/lightbox.css" type="text/css" media="screen" />
    <script src="./ckeditor/lightbox/js/prototype.js" type="text/javascript"></script>
    <script src="./ckeditor/lightbox/js/scriptaculous.js?load=effects,builder" type="text/javascript"></script>
    <script src="./ckeditor/lightbox/js/lightbox.js" type="text/javascript"></script>
//-->

<!--// shadowbox    picture,swf,mp4,iframe//-->
    <link rel="stylesheet" href="./ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" />
    <script src="./ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script>
<script type="text/javascript">
Shadowbox.init();
</script>



</head><body style=margin:0px;><form method="post" enctype="multipart/form-data">
<?
include_once './ckeditor/ckeditor/ckeditor.php';
include_once './ckeditor/ckfinder/ckfinder.php';

$ckeditor = new CKEditor();
$ckeditor->basePath = '';
CKFinder::SetupCKEditor($ckeditor, '/intranet/modules/ckeditor/ckfinder/');
$ckeditor->editor('CKEditor1');


echo stripslashes( $_POST['CKEditor1'] )
?><input type="submit" value="Send"></form>

<!--//  lightbox
    <a href="./ckeditor/ckfinder/userfiles/images/skupiny.png" rel="lightbox">SuperBoxa</a>
//-->

<!--//  shadowbox  //-->
    <a href="./ckeditor/ckfinder/userfiles/images/skupiny.png" rel="shadowbox">SuperBox</a>