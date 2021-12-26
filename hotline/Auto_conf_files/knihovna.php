<?

function securesql($a){
$a=str_replace("  "," ",$a);
$a=mysql_real_escape_string($a);
return $a;
}

function datetcs($a){
if (StrPos (" " . $a, "-") and $a){
$d=explode(" ", $a);
	$exploze = explode("-", $d[0]);$a   = $exploze[2].".".$exploze[1].".".$exploze[0]." ".$d[1];}
	if ($a=="00.00.0000") {$a="";}
return $a;
}

function datecs($a){
if (StrPos (" " . $a, "-") and $a){$exploze = explode("-", $a);$a   = $exploze[2].".".$exploze[1].".".$exploze[0];}
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
$b=securesql($b);
$a=@mysql_result(mysql_query("select $b from dictionary where systemname = '".securesql($a)."' "),0,0);
return $a;
}



function procedures_mail(){
@$data1 = mysql_query("select param from mainsettings where (id >99 and id < 200) ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  // k odeslání e-mailu použijeme SMTP server
  $mail->Host = @mysql_result(mysql_query("select param from mainsettings where name='mail_server' "),0,0);  // zadáme adresu SMTP serveru
  $mail->SMTPAuth = false;               // nastavíme true v případě, že server vyžaduje SMTP autentizaci
  $mail->Username = "";   // uživatelské jméno pro SMTP autentizaci
  $mail->Password = "";            // heslo pro SMTP autentizaci
  $mail->From = mysql_result($data1,0,0);   // adresa odesílatele skriptu
  $mail->FromName = ""; // jméno odesílatele skriptu (zobrazí se vedle adresy odesílatele)

$recipients= explode(";",mysql_result($data1,1,0));

@$cykl=0;while ($recipients[$cykl]):
	$mail->AddAddress ($recipients[$cykl],$recipients[$cykl]);
@$cykl++;endwhile;
  $mail->Subject = mysql_result($data1,0,0);    // nastavíme předmět e-mailu



  //body
  $load_request = mysql_query("select * from hotline_request where document_no='".$DocNo."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());


 $message_Body="<html><head></head><body>
<a href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."Data/".date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0)."' target=_blank >".dictionary("open_check_file","lang_cs")."</a>
</body></html>";
 $mail->Body = $message_Body;
 $mail->WordWrap = 100;   // je vhodné taky nastavit zalomení (po 50 znacích)
//  $mail->CharSet = "windows-1250";   // nastavíme kódování, ve kterém odesíláme e-mail
  $mail->CharSet = "utf-8";   // nastavíme kódování, ve kterém odesíláme e-mail
  $mail->IsHTML(true);

  if(!$mail->Send()) {  // odešleme e-mail
     echo 'Došlo k chybě při odeslání e-mailu.<br>';
     echo 'Chybová zpráva: ' . $mail->ErrorInfo;
  }
// konec varovneho mailu
}
?>