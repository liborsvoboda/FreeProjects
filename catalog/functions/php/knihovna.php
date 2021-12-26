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



function datetcs($a){
if (StrPos (" " . $a, "-") and $a){
$d=explode(" ", $a);
	$exploze = explode("-", $d[0]);$a   = $exploze[2].".".$exploze[1].".".$exploze[0]." ".$d[1];}
	if ($a=="00.00.0000") {$a="";}
return $a;
}



function datetimedb_to_datecs($a){
if (StrPos (" " . $a, "-") and $a){
$d=explode(" ", $a);
	$exploze = explode("-", $d[0]);$a   = $exploze[2].".".$exploze[1].".".$exploze[0];}
	if ($a=="00.00.0000") {$a="";}
return $a;
}



function datetimedb_to_hour($a){
if (StrPos (" " . $a, "-") and $a){
$d=explode(" ", $a);
	$exploze = explode(":", $d[1]);$a   = $exploze[0];}
return $a;
}



function datecs($a){
if (StrPos (" " . $a, "-") and $a){$exploze = explode("-", $a);$a   = $exploze[2].".".$exploze[1].".".$exploze[0];}
return $a;
}



function sysdate($a){
if (StrPos (" " . $a, "/") and $a){$exploze = explode("/", $a);$a   = $exploze[1].".".$exploze[0].".".$exploze[2];}
return $a;
}




function datedb($a){
if (StrPos (" " . $a, ".") and $a){$exploze = explode(".", $a);$a   = $exploze[2]."-".$exploze[1]."-".$exploze[0];}
return $a;
}



function obdobics($a){
if (StrPos (" " . $a, "-") and $a){$exploze = explode("-", $a);$a   = $exploze[1].".".$exploze[0];}
return $a;
}



function obdobidb($a){
if (StrPos (" " . $a, ".") and $a){$exploze = explode(".", $a);$a   = $exploze[1]."-".$exploze[0];}
return $a;
}



function nactisoubor($a){
include ("./".$a);
}



function code($a){
$a=base64_encode($a);
return $a;
}



function decode($a){
$a=base64_decode($a);
return $a;
}



function dictionary($a,$b){
if (@$a<>""){
    $serverName = "127.0.0.1";
    $params = array();
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $connectionInfo = array( "Database"=>"DBNAME", "UID"=>"dba", "PWD"=>"password","CharacterSet"=>"UTF-8","ReturnDatesAsStrings" => "false" );
    $temp_conn = sqlsrv_connect( $serverName, $connectionInfo);
        @$temp_sql =  " SELECT ".mssecuresql($b)." FROM dbo.[100_dictionary] WHERE systemname = '".mssecuresql($a)."' ";
        @$temp_check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options );
        while( @$temp_row = sqlsrv_fetch_array( @$temp_check, SQLSRV_FETCH_BOTH ) ) {
            @$a=@$temp_row[0];
        }sqlsrv_close(@$temp_conn);
} else {$a='Empty dictionary Request';}
//message ($a.$b);    
return @$a;
}



function ip_visitor(){
@$counter_ip_value= getIpAddress();
if (isset($counter_ip_value)){
    $serverName = "127.0.0.1";
    $params = array();
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $connectionInfo = array( "Database"=>"DBNAME", "UID"=>"dba", "PWD"=>"password","CharacterSet"=>"UTF-8","ReturnDatesAsStrings" => "false" );
    $temp_conn = sqlsrv_connect( $serverName, $connectionInfo);

@$temp_sql =  " SELECT * FROM dbo.[100_customer_visit] WHERE ip_address='".mssecuresql($counter_ip_value)."' AND sses_id='".mssecuresql($_SESSION['sess_id'])."' order by id ";
$temp_count=0;
@$check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$temp_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {$temp_count++;}
    if (@$temp_count == 0) {
        $temp_sql_insert = "INSERT INTO dbo.[100_customer_visit] (ip_address,sses_id,visit_date)VALUES('".mssecuresql($counter_ip_value)."','".mssecuresql($_SESSION['sess_id'])."','".date("Y-m-d")." ".StrFTime("%H:%M:%S", Time())."') ";
        $sql_ins_res = sqlsrv_query( $temp_conn, $temp_sql_insert , $params, $options );
    }
sqlsrv_close($temp_conn);
}
}



function removedia($a){
$b = Str_Replace(
array('Á','Ä','É','Ë','Ě','Í','Ý','Ó','Ö','Ú','Ů','Ü','Ž','Š','Č','Ř','Ď','Ť','Ň','Ľ','á','ä','é','ë','ě','í','ý','ó','ö','ú','ů','ü','ž','š','č','ř','ď','ť','ň','ľ'),
array('A','A','E','E','E','I','Y','O','O','U','U','U','Z','S','C','R','D','T','N','L','a','a','e','e','e','i','y','o','o','u','u','u','z','s','c','r','d','t','n','l'),
$a);return $b;
}





function show_server_variable(){
    return print_r ($_SERVER);
}



function savesuccess($value){
  if (@$value){$message=dictionary("save_success",$_SESSION['language']).": ".$value;} else {$message=dictionary("save_success",$_SESSION['language']);}
  echo "<script LANGUAGE=\"JavaScript\">alert('".$message."');</script>";$message="";
}



function message($value){
  echo "<script LANGUAGE=\"JavaScript\">alert('".$value."');</script>";
}



function savechangesuccess($value){
  if (@$value){$message=dictionary("save_change_success",$_SESSION['language']).": ".$value;} else {$message=dictionary("save_change_success",$_SESSION['language']);}
  echo "<script LANGUAGE=\"JavaScript\">alert('".$message."');</script>";$message="";
}




function deletesuccess($value){
  if (@$value){$message=dictionary("delete_success",$_SESSION['language']).": ".$value;} else {$message=dictionary("delete_success",$_SESSION['language']);}
  echo "<script LANGUAGE=\"JavaScript\">alert('".$message."');</script>";$message="";
}




function savefailed($value){
  if (@$value){$message=dictionary("save_failed",$_SESSION['language']).": ".$value;} else {$message=dictionary("save_failed",$_SESSION['language']);}
  echo "<script LANGUAGE=\"JavaScript\">alert('".$message."');</script>";$message="";
}




function permalink($permalink) {
    $permalink = str_replace(" ", "-", $permalink);
    $permalink = str_replace(
        array('Á','Ä','É','Ë','Ě','Í','Ý','Ó','Ö','Ú','Ů','Ü','Ž','Š','Č','Ř','Ď','Ť','Ň','Ľ','á','ä','é','ë','ě','í','ý','ó','ö','ú','ů','ü','ž','š','č','ř','ď','ť','ň','ľ'),
        array('a','a','e','e','e','i','y','o','o','u','u','u','z','s','c','r','d','t','n','l','a','a','e','e','e','i','y','o','o','u','u','u','z','s','c','r','d','t','n','l'),
        $permalink);
    $permalink = strtolower($permalink);
    $permalink = str_replace(array('<', '>'), "-", $permalink);
    $permalink = preg_replace("/[^[:alpha:][:digit:]_]/", "-", $permalink);
    $permalink = preg_replace("/[-]+/", "-", $permalink);
    $permalink = trim($permalink, "-");
    return $permalink;
}





function fn_unset_var($a,$b){
    $fn_cycle=1;while($fn_cycle<=$b):
        unset($_REQUEST[$a.$fn_cycle]);
    $fn_cycle++;endwhile;    
}





function getIpAddress() {
    return (empty($_SERVER['HTTP_CLIENT_IP'])?(empty($_SERVER['HTTP_X_FORWARDED_FOR'])?
    $_SERVER['REMOTE_ADDR']:$_SERVER['HTTP_X_FORWARDED_FOR']):$_SERVER['HTTP_CLIENT_IP']);
}





function country_info($fn_a,$fn_b,$fn_c){ //xml,csv,json - Ip,CountryCode,CountryName,RegionCode,RegionName,City,ZipCode,Latitude,Longitude,MetroCode,AreaCode
     $check_country = simplexml_load_file("http://freegeoip.net/".$fn_c."/".@$fn_a);
     return @$check_country->$fn_b;
}




function lastInsertId($queryID) {
        sqlsrv_next_result($queryID);
        sqlsrv_fetch($queryID);
        return sqlsrv_get_field($queryID, 0);
} 




function program_log($a,$b,$c){
if ($b){unlink('./log/'.$c);}
    
if (!is_dir("./log")) {mkdir ("./log",0777);}
        if (@File_Exists("./log/".$c)){
            @$temp_data = file_get_contents("./log/".$c);
        }
        @$f=fopen("./log/".$c,"w");
        fwrite(@$f,@$temp_data.$a."\r\n");fclose($f);
}




function demand_mail($fn_recipient,$temp_mail_body,$fn_demand_no){
  $sel_lang =explode ("_",$_SESSION["language"]);
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  
  $mail->SMTPAuth = false;
  $mail->Username = ""; 
  $mail->Password = ""; 
  $mail->AddAddress ($fn_recipient,"");
  
    $serverName = "127.0.0.1";
    $params = array();
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $connectionInfo = array( "Database"=>"DBNAME", "UID"=>"dba", "PWD"=>"password","CharacterSet"=>"UTF-8","ReturnDatesAsStrings" => "false" );
    $temp_conn = sqlsrv_connect( $serverName, $connectionInfo);

@$temp_sql =  " SELECT data_type,systemname,ckeditor_".$sel_lang[1]." FROM dbo.[100_main_setting] WHERE [data_type]='smtp_server' OR [data_type] ='app_url' OR [data_type]='smtp_port' OR [data_type]='mail_forgotten_pw' OR [data_type]='system_mail_address' OR [data_type] = 'demand_email_tmp' OR [data_type] = 'company_email' ORDER BY sequence ASC";
@$check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$temp_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
      switch (@$temp_row[0]) {
        case "smtp_server":
            $mail->Host = @$temp_row[1];
            break;
        case "smtp_port":
            $mail->Port = @$temp_row[1];
            break;
        case "system_mail_address":
            $mail->From = @$temp_row[1];
            $mail->FromName = @$temp_row[1];
            break;
        case "app_url":
            $tmp_app_url = @$temp_row[1];
            break;
        case "mail_forgotten_pw":
            $tmp_mail_values = @$temp_row[1];
            $tmp_mail_forgotten_pw = @$temp_row[2];
            break;
        case "demand_email_tmp":
            $mail->Subject = dictionary(@$temp_row[1],$_SESSION["language"]).$fn_demand_no;
            break;
        case "company_email":
            $mail->AddAddress (@$temp_row[1],"");
            break;
    }
}  
  $mail->AddStringAttachment(file_get_contents("http://192.168.1.1/catalog/outputs/demand_template_pdf.php?id=".base64_encode($fn_demand_no)),$fn_demand_no.".pdf"); 
  $mail->Body = '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><BODY style="padding:0px;margin:8px;" >'.str_replace('src="/catalog/','src="'.$tmp_app_url,str_replace("./",$tmp_app_url,$temp_mail_body)).'</BODY></HTML>';
  //$mail->WordWrap = 100;   
  $mail->CharSet = "utf-8";  
  $mail->IsHTML(true);
      if(!$mail->Send()) {
            message(dictionary("email_can_not_be_sent",$_SESSION["language"]).'\n'.dictionary("error_message",$_SESSION["language"]).': '. $mail->ErrorInfo);
        return false;}
      else
        {return true;} 
}
    
   








function mail_restore_password($fn_recipient,$fn_new_pass){
  $sel_lang =explode ("_",$_SESSION["language"]);
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  
  $mail->SMTPAuth = false;
  $mail->Username = ""; 
  $mail->Password = ""; 
  $mail->AddAddress ($fn_recipient,"");
  
    $serverName = "127.0.0.1";
    $params = array();
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $connectionInfo = array( "Database"=>"DBNAME", "UID"=>"dba", "PWD"=>"password","CharacterSet"=>"UTF-8","ReturnDatesAsStrings" => "false" );
    $temp_conn = sqlsrv_connect( $serverName, $connectionInfo);

@$temp_sql =  " SELECT data_type,systemname,ckeditor_".$sel_lang[1]." FROM dbo.[100_main_setting] WHERE [data_type]='smtp_server' OR [data_type] ='app_url' OR [data_type]='smtp_port' OR [data_type]='mail_forgotten_pw' OR [data_type]='system_mail_address' ORDER BY sequence ASC";
@$check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$temp_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
    
    
    
    switch (@$temp_row[0]) {
        case "smtp_server":
            $mail->Host = @$temp_row[1];
            break;
        case "smtp_port":
            $mail->Port = @$temp_row[1];
            break;
        case "system_mail_address":
            $mail->From = @$temp_row[1];
            $mail->FromName = @$temp_row[1];
            break;
        case "app_url":
            $tmp_app_url = @$temp_row[1];
            break;
        case "mail_forgotten_pw":
            $tmp_mail_values = @$temp_row[1];
            $tmp_mail_forgotten_pw = @$temp_row[2];
            break;
            
    }
}
$email_value = explode(",",$tmp_mail_values);
@$temp_sql =" SELECT * FROM dbo.[120_registration] WHERE [email]='".mssecuresql($fn_recipient)."' ";
@$check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$temp_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
  foreach($email_value as $tmp_i =>$key) {
    $full_key = $key;
    $key = str_replace("xxXX","",$key);
    $key = str_replace("XXxx","",$key);
        if (@$temp_row[$key] && $key<>"login_password") {
            $tmp_mail_forgotten_pw = str_replace($full_key,@$temp_row[$key],$tmp_mail_forgotten_pw);
        }
  }
}

sqlsrv_close($temp_conn);

  $mail->Subject = dictionary("mail_password_recovery",$_SESSION["language"]);

  $email_value = explode(",",$tmp_mail_values);
  foreach($email_value as $tmp_i =>$key) {
     switch ($key) {
        case "xxXXlogin_passwordXXxx":
            $tmp_mail_forgotten_pw = str_replace($key,$fn_new_pass,$tmp_mail_forgotten_pw);
            break;
     }
  }
  
  $mail->Body = '<HTML><HEAD><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></HEAD><BODY>'.$tmp_mail_forgotten_pw.'</BODY></HTML>';
  $mail->WordWrap = 100;   
  $mail->CharSet = "utf-8";  
  $mail->IsHTML(true);
      if(!$mail->Send()) { 
         echo dictionary("email_can_not_be_sent",$_SESSION["language"]).'<br>';
         echo dictionary("error_message",$_SESSION["language"]) . $mail->ErrorInfo;
      } 
}
    
   
   
   
   
function customer_profile_mail($fn_recipient,$fn_new_pass,$fn_subject){
  $sel_lang =explode ("_",$_SESSION["language"]);
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  
  $mail->SMTPAuth = false;
  $mail->Username = ""; 
  $mail->Password = ""; 
  $mail->AddAddress ($fn_recipient,"");
  
    $serverName = "127.0.0.1";
    $params = array();
    $options =  array( "Scrollable" => SQLSRV_CURSOR_KEYSET );
    $connectionInfo = array( "Database"=>"DBNAME", "UID"=>"dba", "PWD"=>"password","CharacterSet"=>"UTF-8","ReturnDatesAsStrings" => "false" );
    $temp_conn = sqlsrv_connect( $serverName, $connectionInfo);

 @$temp_sql =  " SELECT data_type,systemname,ckeditor_".$sel_lang[1]." FROM dbo.[100_main_setting] WHERE [data_type]='smtp_server' OR [data_type]='smtp_server' OR [data_type] ='app_url' OR [data_type]='smtp_port' OR [data_type]='mail_new_reg' OR [data_type]='system_mail_address' ORDER BY sequence ASC";
 program_log($temp_sql,'YES','sql.log');
 @$check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
 while( @$temp_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
    switch (@$temp_row[0]) {
        case "smtp_server":
            $mail->Host = @$temp_row[1];
            break;
        case "smtp_port":
            $mail->Port = @$temp_row[1];
            break;
        case "system_mail_address":
            $mail->From = @$temp_row[1];
            $mail->FromName = @$temp_row[1];
            break;
        case "app_url":
            $tmp_app_url = @$temp_row[1];
            break;
        case "mail_new_reg":
            $tmp_mail_values = @$temp_row[1];
            $tmp_mail_new_reg = @$temp_row[2];
            break;
            
    }
  }

$email_value = explode(",",$tmp_mail_values);
@$temp_sql =" SELECT * FROM dbo.[120_registration] WHERE [email]='".mssecuresql($fn_recipient)."' ";
@$check = sqlsrv_query( $temp_conn, $temp_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$temp_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
  foreach($email_value as $tmp_i =>$key) {
    $full_key = $key;
    $key = str_replace("xxXX","",$key);
    $key = str_replace("XXxx","",$key);
    //    if (@$temp_row[$key] && $key<>"login_password") {
            if ($key<>"login_password") {
            $tmp_mail_new_reg = str_replace($full_key,@$temp_row[$key],$tmp_mail_new_reg);
        }
    }
  }
  sqlsrv_close($temp_conn);

  $mail->Subject = $fn_subject;
  
  $email_value = explode(",",$tmp_mail_values);
  foreach($email_value as $tmp_i =>$key) {
     switch ($key) {
        case "xxXXemailXXxx":
            $tmp_mail_new_reg = str_replace($key,$fn_recipient,$tmp_mail_new_reg);
            break;
        case "xxXXlogin_passwordXXxx":
            $tmp_mail_new_reg = str_replace($key,$fn_new_pass,$tmp_mail_new_reg);
            break;
     }
  }

  
  $mail->Body = '<HTML><HEAD><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></HEAD><BODY>'.$tmp_mail_new_reg.'</BODY></HTML>';
  $mail->WordWrap = 100;   
  $mail->CharSet = "utf-8";  
  $mail->IsHTML(true);
      if(!$mail->Send()) { 
         echo dictionary("email_can_not_be_sent",$_SESSION["language"]).'<br>';
         echo dictionary("error_message",$_SESSION["language"]) . $mail->ErrorInfo;
      } 
}
   
   
      
    
 
function generate_password( $length = 8 ) {
    $chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-=+;:,.?";
    $password = substr( str_shuffle( $chars ), 0, $length );
return $password;
}   
    





function check_email_exp($fn_email){
    if (preg_match('/^[-!#$%&\'*+\\.\/0-9=?A-Z^_`{|}~]+@([-0-9A-Z]+\.)+([0-9A-Z]){2,4}$/i', $fn_email)){
        return True;
    } else {
        return False;
    }
}   





?>
