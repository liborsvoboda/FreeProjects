<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
require_once ("./config/mssql_dbconnect.php");
?>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />
</head>

<Body>
<?

    echo "neco";



  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  // k odeslání e-mailu použijeme SMTP server
  $mail->Host = 'smtpServer';
  $mail->SMTPAuth = false;               // nastavíme true v případě, že server vyžaduje SMTP autentizaci
  $mail->Username = "";   // uživatelské jméno pro SMTP autentizaci
  $mail->Password = "";            // heslo pro SMTP autentizaci
  $mail->From = 'osoba@company.cz';
  $mail->FromName = 'osoba';
  $mail->AddAddress ('author@company.cz',"");
  $mail->ConfirmReadingTo = 'osoba@company.cz';
  $mail->Subject = 'test precteni';
  $mail->Body = 'potvrd precteni emailu';
  $mail->WordWrap = 100;   // je vhodné taky nastavit zalomení (po 50 znacích)
//  $mail->CharSet = "windows-1250";   // nastavíme kódování, ve kterém odesíláme e-mail
  $mail->CharSet = "utf-8";   // nastavíme kódování, ve kterém odesíláme e-mail
  $mail->IsHTML(true);

  if(!$mail->Send()) {  // odešleme e-mail
     echo 'Došlo k chybě při odeslání e-mailu.<br>';
     echo 'Chybová zpráva: ' . $mail->ErrorInfo;
  }



?>


</body>
</html>




