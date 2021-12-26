<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>CKEditor - Server preview</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="robots" content="noindex, nofollow">
	</head>
	<body>
	<p>Warning: to avoid XSS attacks you should remove this php sample</p>
	<p>Some content generated at the server...</p>
<?php

	$data = $_POST['htmlData'];

	if ( get_magic_quotes_gpc() )
		$data = stripslashes( $data ) ;

	echo $data ;
?>

	</body>
</html>
