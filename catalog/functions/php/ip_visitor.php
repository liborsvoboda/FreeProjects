<?$counter_ip_value= getIpAddress();
if (isset($counter_ip_value)){
@$sql =  " SELECT * FROM dbo.[100_customer_visit] where ip_address='".mssecuresql($counter_ip_value)."' AND sses_id='".securesql($_SESSION['sess_id'])."' order by id ";
$count=0;
@$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$main_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {$count++;}
    if ($count > 0) {
        $sql_insert = "INSERT INTO dbo.[100_customer_visit] (ip_address,sses_id,visit_date)VALUES('".mssecuresql($counter_ip_value)."','".securesql($_SESSION['sess_id'])."','".securesql($dnest)."') ";
        $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    }
sqlsrv_close($conn);
}
?>