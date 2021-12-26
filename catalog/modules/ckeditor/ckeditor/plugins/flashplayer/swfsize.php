<?PHP 

$server_path = 'root/path/to/your/files';

header ("Pragma:no-cache");
header ("Cache-Control:private,no-store,no-cache,must-revalidate");

if($_GET['movie'] != "")
{
	$filename = $_GET['movie'];
	$ext = substr( $filename, ( strrpos($filename, '.') + 1 ) ) ;
	if($ext == "swf")
    {
		$file = $server_path.'/'.$filename;
		if(file_exists($file))
        {
			$format = GetImageSize($file);
			$wh = explode(' ', $format[3]);
			$swfwidth = preg_replace('/[^0-9]/', '', $wh[0]);
			$swfheight = preg_replace('/[^0-9]/', '', $wh[1]);
		}
	}
?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="robots" content="noindex, nofollow">
<script type="text/javascript">
/* <![CDATA[ */
	var win;
	if(document.all)
		win = parent ;
	else
		win = top ;
    var CKEopener = win.CKEDITOR;

	if (CKEopener)
	{
        CKEopener.tools.callFunction( <?php echo $_GET['CKEditorFuncNum']; ?>,
        {
            newWidth : <?php echo $swfwidth; ?>,
            newHeight : <?php echo $swfheight; ?>
        });
	}
/* ]]> */
</script>
</head>
<body></body>
</html>
<?PHP } ?>
