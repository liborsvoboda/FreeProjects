<?

function mssql_real_escape_string($s) {
	if(get_magic_quotes_gpc()) {
		$s = stripslashes($s);
	}
	$s = str_replace("'","''",$s);
	return $s;
}

function mssecuresql($a){
$a=str_replace("  "," ",$a);
$a=mssql_real_escape_string($a);
return $a;
}

?>
