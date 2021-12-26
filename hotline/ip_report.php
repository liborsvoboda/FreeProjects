<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");

if (@$_SESSION["lnamed"]) {?>
<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('ip_report_form',$_SESSION['language']);?>";
</script>
<link rel="icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon" />
<link rel="shortcut icon" href='http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico' type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />
</head>
<Body onselectstart="return false;" style=background-color:white; >
<form name="IP_Report" method="post" action="./ip_report.php" >
<table style=width:100;border:0;>
<tr>
<td><input name=button type=submit value="Spustit GENEROVÁNÍ DB" /></td>
<td><input name=button type=button onclick="window.open('./add_ons/mysql-connector-odbc-5.2.5-win32.msi','new','');" value="Install MYSQL Client x32" /></td>
<td><input name=button type=button onclick="window.open('./add_ons/Měření IP.xlsx','new','');" value="Spustit EXCEL Report" /></td>
</tr>
</table>
</form>

<?php
$connection = mysqli_connect('127.0.0.1', 'root', 'password');
mysqli_select_db($connection,'ip_report');

$fullrec = 0;
$dir = "//127.0.0.1/SpeedTestResult/Results/";

if (@$_POST["button"]=="Spustit GENEROVÁNÍ DB") {
echo"<table>";
if (is_dir($dir)) {
    if ($dh = opendir($dir)) {
    $cykl=0;
        while (($file = readdir($dh)) !== false) {

IF ( $cykl>1 && @strpos($file, ".csv") ) {
   $openedfile = file_get_contents($dir.$file);
  $ip_name_addres = explode("_",$file);
  $username =explode(".",$ip_name_addres[1]);
  //echo $ip_addres[0]." ".$username[0];
  $records = explode("Test Settings:", $openedfile); //vrati 1 zaznam
  $formatcheck = explode("Local IP:", $openedfile); //vrati 1 zaznam

if (@$formatcheck[1]){
    $load=1;while(@$records[$load]<>""):
     $temp_ip_address=explode("Local IP: ",$records[$load]);
     $ip_address = explode (" ",$temp_ip_address[1]);
     $temp_record_speeds = explode("Sent", $temp_ip_address[2]); //vrati rozpad na poslano-prijato
     $temp_receive_speed = explode ($ip_address[0]." " ,$temp_record_speeds[2]);
     $temp_receive_speed = explode ("----->------>------>------>------>------>",$temp_receive_speed[1]);
     $temp_sent_speed = explode ($ip_address[0]." " ,$temp_record_speeds[1]);
     $temp_other_values = explode (";", $temp_record_speeds[2]);

     $receive_speed = substr($temp_receive_speed[0],15,7);

     if (strlen(str_Replace(" ","",substr($temp_receive_speed[0],23,4)))==4) {$receive_unit = substr($temp_receive_speed[0],23,4);}
     else {$receive_unit = substr($temp_receive_speed[0],22,4);}

IF ($receive_unit=="Gbps") {$receive_speed_Mbps = 1000*$receive_speed;}
IF ($receive_unit=="Mbps") {$receive_speed_Mbps = $receive_speed;}
IF ($receive_unit=="Kbps") {$receive_speed_Mbps = $receive_speed/1000;}

     $sent_speed = substr($temp_sent_speed[1],15,7);
     if (strlen(str_Replace(" ","",substr($temp_sent_speed[1],23,4)))==4) {$sent_unit = substr($temp_sent_speed[1],23,4);}
     else {$sent_unit = substr($temp_sent_speed[1],22,4);}

IF ($sent_unit=="Gbps") {$sent_speed_Mbps = 1000*$sent_speed;}
IF ($sent_unit=="Mbps") {$sent_speed_Mbps = $sent_speed;}
IF ($sent_unit=="Kbps") {$sent_speed_Mbps = $sent_speed/1000;}

     @$text_user_name = $temp_other_values[2];
     @$time = $temp_other_values[1];

     @$temp_date = strlen($temp_other_values[0]);

     @$date = substr($temp_other_values[0],$temp_date-10,10);


$control = mysqli_query($connection,"select id from data_table where ip='".$ip_address[0]."' and date='".$date."' and time='".$time."'  ") or die (MySQL_Error());
if (!@mysqli_num_rows($control) && strlen(str_replace(" ","",$date))==10 ) {

    $critical_send="NE";
		if ($sent_speed_Mbps>100 && $sent_speed_Mbps<700 ){$critical_send="ANO";}
		if ($sent_speed_Mbps<80){$critical_send="ANO";}
    $critical_receive="NE";
		if ($receive_speed_Mbps>100 && $receive_speed_Mbps<500 ){$critical_receive="ANO";}
		if ($receive_speed_Mbps<40){$critical_receive="ANO";}

	mysqli_query($connection,"insert into data_table (ip,usr_name,date,time,sent,sent_Mbps,sent_unit,receive,receive_Mbps,receive_unit,default_unit,datetime,critical_send,critical_receive)VALUES
  ('".$ip_address[0]."','".$text_user_name."','".$date."','".$time."','".$sent_speed."','".$sent_speed_Mbps."','".$sent_unit."','".$receive_speed."','".$receive_speed_Mbps."','".$receive_unit."','Mbps','".date("Y-m-d")."','".$critical_send."','".$critical_receive."') ") or die (MySQL_Error());
echo "<tr><td>INSERTED Record:</td><td>".$ip_address[0]."</td><td>Username:</td><td>".$text_user_name."</td><td>Date:</td><td>".$date."</td><td>Time:</td><td>".$time."</td><td>Sent:</td><td>".$sent_speed."</td><td>".$sent_unit."</td><td>Receive:</td><td>".$receive_speed."</td><td>".$receive_unit."</td></tr>";
}


$load++;
endwhile;



}
$fullrec =$fullrec + $load;


}

     $cykl++;
        }
        closedir($dh);
    }
}
echo "</table><br>Total Records in DB: ".$fullrec."<br>";

}
?>
</body>
</html>

<?
require_once ("./functions/js/standard_scripts.js");
?>


<?}?>