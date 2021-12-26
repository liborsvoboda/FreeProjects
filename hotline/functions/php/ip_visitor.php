<?$counter_ip_value= getIpAddress();

if (isset($counter_ip_value)){
    $control = mysql_query("select * from karat_conf_ip_list where ip_address='".securesql($counter_ip_value)."' and sses_id='".securesql($sess_id)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
    if (!@mysql_num_rows($control)){
        mysql_query("INSERT INTO karat_conf_ip_list (ip_address,sses_id,visit_date)VALUES('".securesql($counter_ip_value)."','".securesql($sess_id)."','".securesql($dnest)."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());    
    }
}
?>