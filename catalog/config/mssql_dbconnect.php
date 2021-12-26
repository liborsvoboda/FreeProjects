<?
$serverName = "127.0.0.1";
$params = array();
//$options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
$options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
$connectionInfo = array( "Database"=>"DBNAME", "UID"=>"dba", "PWD"=>"password","CharacterSet"=>"UTF-8","ReturnDatesAsStrings" => "false" );
$conn = sqlsrv_connect( $serverName, $connectionInfo);
if( $conn === false ) {
    die( print_r( sqlsrv_errors(), true));
sqlsrv_close($conn);    
}
?>
