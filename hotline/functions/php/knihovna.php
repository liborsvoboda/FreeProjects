<?

function securesql($a){
$a=str_replace("  "," ",$a);
$a=htmlspecialchars($a);
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
$b=securesql($b);
$a=@mysql_result(mysql_query("select $b from dictionary where systemname = '".securesql($a)."' "),0,0);
return $a;
}

function webmin_dictionary($a,$b){
$b=securesql($b);
$a=@mysql_result(mysql_query("select $b from webmin_dictionary where systemname = '".securesql($a)."' "),0,0);
return $a;
}

function removedia($a){
$b = Str_Replace(
array('Á','Ä','É','Ë','Ě','Í','Ý','Ó','Ö','Ú','Ů','Ü','Ž','Š','Č','Ř','Ď','Ť','Ň','Ľ','á','ä','é','ë','ě','í','ý','ó','ö','ú','ů','ü','ž','š','č','ř','ď','ť','ň','ľ'),
array('A','A','E','E','E','I','Y','O','O','U','U','U','Z','S','C','R','D','T','N','L','a','a','e','e','e','i','y','o','o','u','u','u','z','s','c','r','d','t','n','l'),
$a);return $b;
}

function iid(){   // vraceni id vlozeneho zaznamu
$a=mysql_insert_id();return $a;
}


function full_copy( $source, $target ) {
	if ( is_dir( $source ) ) {
		@mkdir( $target );
		$d = dir( $source );
		while ( FALSE !== ( $entry = $d->read() ) ) {
			if ( $entry == '.' || $entry == '..' ) {
				continue;
			}
			$Entry = $source . '/' . $entry;
			if ( is_dir( $Entry ) ) {
				full_copy( $Entry, $target . '/' . $entry );
				continue;
			}
			copy( $Entry, $target . '/' . $entry );
		}

		$d->close();
	}else {
		copy( $source, $target );
	}
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

function access($a){ //1-read,2-write,3-edit,4-del,5-import,6export,7-print

// nova metoda jako zobrazit, pouzit, zmrazit udelat

  if ($a=="on/off") {   //read/write
    if (!@strpos(" ".decode(@$_SESSION["RTG"]), "1") && !@strpos(" ".decode(@$_SESSION["RTG"]), "2")) {$b=" readonly=yes disabled ";}
    if ( @strpos(" ".decode(@$_SESSION["RTG"]), "1") && !@strpos(" ".decode(@$_SESSION["RTG"]), "2")) {$b=" readonly=yes disabled ";}

   if (@strpos(" ".decode(@$_SESSION["RTG"]), "2")) {$b="";}
  }
  IF ($a=="1/2off") {   //edit
    IF (!@strpos(" ".decode(@$_SESSION["RTG"]), "3")) {$b=" readonly=yes disabled";}
    ELSE {$b="";}
}
  IF ($a=="remove") {   //delete
    IF (!@strpos(" ".decode(@$_SESSION["RTG"]), "4")) {$b=" readonly=yes disabled ";}
    ELSE {$b="";}
}

  if ($a=="down/load") { //import/export
    if (@strpos(" ".decode(@$_SESSION["RTG"]), "3") && !@strpos(" ".decode(@$_SESSION["RTG"]), "4")) {$b="";}
    if (@strpos(" ".decode(@$_SESSION["RTG"]), "4")) {$b="";}
  }
  if ($a=="print") {
    if (@strpos(" ".decode(@$_SESSION["RTG"]), "5")) {$b="";}
  }
  return $b;
}


function string_search($fn_v_a,$fn_v_b){
    if (@strpos(" ".$fn_v_a,$fn_v_b)){return true;}
        else {return false;}
}



// backup the db function

// command backup_database_tables('HOST','USERNAME','PASSWORD','DATABASE', '*');
function backup_database_tables($host,$user,$pass,$name,$tables)  {
    $link = mysql_connect($host,$user,$pass);
    mysql_select_db($name,$link);

    //get all of the tables
    if($tables == '*')      {
        $tables = array();
        $result = mysql_query('SHOW TABLES');
        while($row = mysql_fetch_row($result))  {
            $tables[] = $row[0];  }
    }  else  {
        $tables = is_array($tables) ? $tables : explode(',',$tables);
    }

    //cycle through each table and format the data
    foreach($tables as $table)    {
        $result = mysql_query('SELECT * FROM '.$table);
        $num_fields = mysql_num_fields($result);
        $return.= 'DROP TABLE '.$table.';';
        $row2 = mysql_fetch_row(mysql_query('SHOW CREATE TABLE '.$table));
        $return.= "\n\n".$row2[1].";\n\n";
        for ($i = 0; $i < $num_fields; $i++)        {

            while($row = mysql_fetch_row($result)) {
                $return.= 'INSERT INTO '.$table.' VALUES(';

                for($j=0; $j<$num_fields; $j++)                {
                    $row[$j] = addslashes($row[$j]);
                    $row[$j] = ereg_replace("\n","\\n",$row[$j]);
                    if (isset($row[$j])) { $return.= '"'.$row[$j].'"' ; } else { $return.= '""'; }
                    if ($j<($num_fields-1)) { $return.= ','; }
                }
                $return.= ");\n";
            }
        }
        $return.="\n\n\n";
    }

    //save the file
    $handle = fopen('./backup/db-backup-'.time().'-'.(md5(implode(',',$tables))).'.sql','w+');
    fwrite($handle,$return);
    fclose($handle);
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

function sitemap(){
$www_url=mysql_result(mysql_query("select value from webmin_main_sett where name='web_site_url' "),0,0);
$sitemap="./sitemap.xml";$sitem1=mysql_query("select id,name from www_menu order by id");
$sitem="<?xml version='1.0' encoding='UTF-8'?>\r\n<urlset xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>\r\n";
$sitem.="<url><loc>".$www_url."</loc><changefreq>always</changefreq><priority>0.75</priority></url>\r\n";
$sitem.="<url><loc>".$www_url."/</loc><changefreq>always</changefreq><priority>0.75</priority></url>\r\n";
@$sitel=0;while(@$sitel<mysql_num_rows(@$sitem1)):
	$sitem.="<url><loc>".$www_url."/".mysql_result($sitem1,$sitel,0)."-".permalink(mysql_result($sitem1,$sitel,1)).".html</loc><changefreq>always</changefreq><priority>0.75</priority></url>\r\n";
@$sitel++;endwhile;
$sitem.="</urlset>\r\n";
$f=fopen($sitemap,"w");fwrite($f,$sitem);fclose($f);
}


function hotline_mail($status,$DocNo){
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  // k odeslání e-mailu použijeme SMTP server
  $mail->Host = @mysql_result(mysql_query("select param from mainsettings where name='mail_server' "),0,0);  // zadáme adresu SMTP serveru
  $mail->SMTPAuth = false;               // nastavíme true v případě, že server vyžaduje SMTP autentizaci
  $mail->Username = "";   // uživatelské jméno pro SMTP autentizaci
  $mail->Password = "";            // heslo pro SMTP autentizaci
  $mail->From = mysql_result(mysql_query("select param from mainsettings where id='202' "),0,0);   // adresa odesílatele skriptu
  $mail->FromName = ""; // jméno odesílatele skriptu (zobrazí se vedle adresy odesílatele)

//load data
  $load_request = mysql_query("select * from hotline_request where document_no='".$DocNo."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
  $load_files = mysql_query("select * from hotline_attachment where parent_no='".$DocNo."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

// mail to

//all admins and creator
if ($status=="new_request"){@$data1 = mysql_query("select email,name,surname from login where ((loginname='".mysql_result($load_request,0,15)."' or sysadmin='Y' or loginname='".securesql(@$_SESSION["lnamed"])."') and email<>'') group by email ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}
if ($status=="new_step"){@$data1 = mysql_query("select email,name,surname from login where ((loginname='".mysql_result($load_request,0,15)."' or loginname='".securesql(@$_SESSION["lnamed"])."') and email<>'') group by email ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());}

@$cykl=0;while (@mysql_result($data1,$cykl,0)):
	$mail->AddAddress (mysql_result(@$data1,@$cykl,0),"");
@$cykl++;endwhile;


  $mail->Subject = dictionary("hotline",$_SESSION["language"])." ".dictionary($status,$_SESSION["language"]).": ".$DocNo." / ".htmlspecialchars_decode(mysql_result(@$load_request,0,1));    // nastavíme předmět e-mailu

  //body
 $message_Body="<html><head>
<link rel='icon' href='http:".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."config/company.ico' type='image/x-icon'>
<link rel='shortcut icon' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."config/company.ico' type='image/x-icon'>
<meta http-equiv='Content-Type' content='text/html; charset=windows-1250'>
<link rel='stylesheet' type='text/css' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."css/default/main_window.css' />
<link rel='stylesheet' type='text/css' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."css/default/user_settings.css' />
</head><body onselectstart='return false;'>
<legend id=ram_legenda><b>".dictionary($status,$_SESSION["language"])."</b></legend>
<table border=2 cellpading=0 cellspacing=0 style=color:#000080; >
<tr><td>".dictionary("document_no",$_SESSION["language"])."</td><td><a href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."#hotline.php?".code(mysql_result(@$load_request,0,12))."'><input readonly=yes type=text value='".mysql_result(@$load_request,0,12)."' style=width:430px;text-align:center; ></a></td></tr>
<tr><td>".dictionary("title",$_SESSION["language"])."</td><td><input readonly=yes autocomplete=off type=text value='".mysql_result(@$load_request,0,1)."' style=width:430px ></td></tr>
<tr><td style=vertical-align:top; >".dictionary("message",$_SESSION["language"])."</td><td><textarea readonly=yes rows=6 wrap=off style=width:430px;overflow:auto; >".mysql_result(@$load_request,0,2)."</textarea></td></tr>
<tr><td>".dictionary("priority",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(@$load_request,0,4)."' style=width:430px;text-align:center; ></td></tr>";

//attachments
if (@mysql_num_rows($load_files)){
$message_Body.="<tr><td>".dictionary("attachments",$_SESSION["language"])."</td><td>";
$cykl=0;while(@mysql_result($load_files,$cykl,0)):
    $message_Body.="<a id=rfile href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."external_file.php?tbl=hotline_attachment&id=".mysql_result(@$load_files,$cykl,0)."' target=_blank >".@mysql_result($load_files,$cykl,2)."</a> ";
$cykl++;endwhile;
$message_Body.="</td></tr>";}

$message_Body.="<tr><td style=width:120px >".dictionary("requested_date",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".datecs(mysql_result(@$load_request,0,5))."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("solution_to",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".datecs(mysql_result(@$load_request,0,14))."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("solves",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(mysql_query("select concat(surname,' ',name) from login where loginname='".securesql(mysql_result($load_request,0,15))."' "),0,0)."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("status",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(@$load_request,0,3)."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("score",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(@$load_request,0,13)."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr></table></body></html>";

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

 $message_Body="<html><head>
<link rel='icon' href='http:".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."config/company.ico' type='image/x-icon'>
<link rel='shortcut icon' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."config/company.ico' type='image/x-icon'>
<meta http-equiv='Content-Type' content='text/html; charset=windows-1250'>
<link rel='stylesheet' type='text/css' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."css/default/main_window.css' />
<link rel='stylesheet' type='text/css' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."css/default/user_settings.css' />
</head><body>
<a href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."Data/".date("Y-m-d")."-".mysql_result(mysql_query("select param from mainsettings where name ='epdm_file_name' "),0,0)."' target=_blank >".dictionary("open_check_file",$_SESSION["language"])."</a>
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



function task_manager_mail($status,$DocNo,$confirm){
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  // k odeslání e-mailu použijeme SMTP server
  $mail->Host = @mysql_result(mysql_query("select param from mainsettings where name='mail_server' "),0,0);  // zadáme adresu SMTP serveru
  $mail->SMTPAuth = false;               // nastavíme true v případě, že server vyžaduje SMTP autentizaci
  $mail->Username = "";   // uživatelské jméno pro SMTP autentizaci
  $mail->Password = "";            // heslo pro SMTP autentizaci
  $mail->From = mysql_result(mysql_query("select param from mainsettings where id='402' "),0,0);   // adresa odesílatele skriptu
  $mail->FromName = ""; // jméno odesílatele skriptu (zobrazí se vedle adresy odesílatele)
     if (@$confirm<>"") {$mail->ConfirmReadingTo = @$confirm;}
//load data
  $load_request = mysql_query("select * from task_manager_request where document_no='".$DocNo."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
  $load_files = mysql_query("select * from task_manager_attachment where parent_no='".$DocNo."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());

// mail to
//creator email

//all participant

$participant="";$solvers=explode(",",mysql_result($load_request,0,15));
$cykl=1;while(@$solvers[$cykl]):
    $participant.=" loginname = '".@$solvers[$cykl]."' ";
    if (@$solvers[($cykl+1)]){$participant.=" or ";}
$cykl++;endwhile;
if ($participant<>""){$participant.=" or ";}

$data1=mysql_query("select email from login where (($participant loginname='".securesql(mysql_result($load_request,0,10))."') and email<>'') group by email ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
@$cykl=0;while (@mysql_result($data1,$cykl,0)):
	$mail->AddAddress (mysql_result(@$data1,@$cykl,0),"");
@$cykl++;endwhile;

//subject
  $mail->Subject = dictionary("task_manager",$_SESSION["language"])." ".$status.": ".$DocNo." / ".htmlspecialchars_decode(mysql_result(@$load_request,0,1));    // nastavíme předmět e-mailu

  //body
  
 $message_Body="<html><head>
<link rel='icon' href='http:".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."config/company.ico' type='image/x-icon'>
<link rel='shortcut icon' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."config/company.ico' type='image/x-icon'>
<meta http-equiv='Content-Type' content='text/html; charset=windows-1250'>
<link rel='stylesheet' type='text/css' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."css/default/main_window.css' />
<link rel='stylesheet' type='text/css' href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."css/default/user_settings.css' />
</head><body onselectstart='return false;'>
<legend id=ram_legenda><b>".$status."</b></legend>
<table border=2 cellpading=0 cellspacing=0 style=color:#000080; >
<tr><td>".dictionary("document_no",$_SESSION["language"])."</td><td><a href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."#task_manager.php?".code(mysql_result(@$load_request,0,12))."'><input readonly=yes type=text value='".mysql_result(@$load_request,0,12)."' style=width:430px;text-align:center; ></a></td></tr>
<tr><td>".dictionary("title",$_SESSION["language"])."</td><td><input readonly=yes autocomplete=off type=text value='".mysql_result(@$load_request,0,1)."' style=width:430px ></td></tr>
<tr><td style=vertical-align:top; >".dictionary("message",$_SESSION["language"])."</td><td><textarea readonly=yes rows=6 wrap=off style=width:430px;overflow:auto; >".mysql_result(@$load_request,0,2)."</textarea></td></tr>
<tr><td>".dictionary("priority",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(@$load_request,0,4)."' style=width:430px;text-align:center; ></td></tr>";

//attachments
if (@mysql_num_rows($load_files)){
$message_Body.="<tr><td>".dictionary("attachments",$_SESSION["language"])."</td><td>";
$cykl=0;while(@mysql_result($load_files,$cykl,0)):
    $message_Body.="<a id=rfile href='".mysql_result(mysql_query("select param from mainsettings where name='app_url' "),0,0)."external_file.php?tbl=hotline_attachment&id=".mysql_result(@$load_files,$cykl,0)."' target=_blank >".@mysql_result($load_files,$cykl,2)."</a> ";
$cykl++;endwhile;
$message_Body.="</td></tr>";}

$message_Body.="<tr><td style=width:120px >".dictionary("requested_date",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".datecs(mysql_result(@$load_request,0,5))."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("solution_to",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".datecs(mysql_result(@$load_request,0,14))."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("solves",$_SESSION["language"])."</td><td><input readonly=yes type=text value='";

// solvers
@$solvers = explode(",",mysql_result($load_request,0,15));
$cykl=1;while(@$solvers[$cykl]):
    $message_Body.=mysql_result(mysql_query("select concat(surname,' ',name) from login where loginname='".@$solvers[$cykl]."' "),0,0).",";
$cykl++;endwhile;

$message_Body.="' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("status",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(@$load_request,0,3)."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr>
<tr><td style=width:120px >".dictionary("score",$_SESSION["language"])."</td><td><input readonly=yes type=text value='".mysql_result(@$load_request,0,13)."' style=width:380px;text-align:center;font-weight:bold; readonly=yes ></td></tr></table></body></html>";

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

function file_to_db ($fn_command,$fn_variable,$fn_table,$fn_id){ //columns - icon_name,icon,mime_type
    //saving multifiles variable
    if ($fn_command=='insert'){
    $cykl=1;while(@$_FILES[$fn_variable.$cykl]['name']):
        @$filename= @$_FILES[$fn_variable.$cykl]['name'];
        @$temp = @$_FILES[$fn_variable.$cykl]['tmp_name'];@$mime = @$_FILES[$fn_variable.$cykl]['type'];if (@$mime=="image/pjpeg"){@$mime='image/jpeg';}@$logo = implode('', file("$temp"));
        $file_result=mysql_query("insert into $fn_table (icon_name,icon,mime_type)VALUES('".$filename."','".mysql_escape_string(@$logo)."','".securesql(@$mime)."')") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
        if ($file_result<>1) {savefailed($filename);}
    $cykl++;endwhile;
    }
    // end of saving

    // update selected file
    if ($fn_command=='update'){
        @$filename= @$_FILES[$fn_variable]['name'];
        @$temp = @$_FILES[$fn_variable]['tmp_name']; @$mime = @$_FILES[$fn_variable]['type']; if (@$mime=="image/pjpeg"){@$mime='image/jpeg';} @$logo = implode('', file("$temp"));
        $file_result=mysql_query(" update ".$fn_table." SET icon_name = '".securesql($filename)."', icon = '".mysql_escape_string(@$logo)."', mime_type ='".securesql(@$mime)."' where id = '".securesql($fn_id)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
        if ($file_result<>1) {savefailed($filename);}
    }
    // end of update selected file
}


function glob_del($fn_table,$fn_id,$fn_value){
    if (@$fn_value) {$fn_temp = mysql_result(mysql_query("select '".securesql($fn_value)."' from $fn_table where id = '".securesql($fn_id)."' "),0,0);}
        $fn_result =mysql_query("delete from $fn_table where id='".securesql($fn_id)."' ") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());    
    if (@$fn_result==1) {deletesuccess(@$fn_temp);}    
}


function fn_ldap_list($fn_user){
// login je jasny, list, basedn, je slozka - ktera je vychozi 
// a nasledne v ldap_list filtruji OU nebo CN - velka mala pismena nehraji roli - WINDOWS
// v infu pak filtruji na dane pole (OU,CN,NAME,) info [i - zaznam][typ objektu - name,cn, etc..][0 - jeho hodnota, nektere typy mohou mit vic hodnot]
// cmd dsquery - command pro vypis LDAP 
//$basedn = "DC=ldap,DC=local"; // for OU show
$ldaphost = 'ldap://'.@mysql_result(mysql_query("select param from mainsettings where id=8 "),0,0);
$ldapport = @mysql_result(mysql_query("select param from mainsettings where id=9 "),0,0);
$ds = ldap_connect($ldaphost, $ldapport) or die (message(dictionary("ldap_connect_failed",$_SESSION["language"])));
    ldap_set_option($ldapconn, LDAP_OPT_PROTOCOL_VERSION, 3);
    ldap_set_option($ldapconn, LDAP_OPT_REFERRALS, 0);
    ldap_set_option($ds, LDAP_OPT_DEBUG_LEVEL, 7);

    if (@$ds) {
        $username = @mysql_result(mysql_query("select param from mainsettings where id=10 "),0,0);
        $upasswd = @mysql_result(mysql_query("select param from mainsettings where id=11 "),0,0);
        $ldapbind = ldap_bind($ds, $username, $upasswd) or die (message(dictionary("ldap_access_denied",$_SESSION["language"])));
    
        if (@$ldapbind) {
//types of fields: accountexpires;admincount;altrecipient;badpasswordtime;badpwdcount;cn;codepage;countrycode;description;displayname;distinguishedname;dscorepropagationdata;homemdb;homemta;instancetype;iscriticalsystemobject;lastlogoff;lastlogon;lastlogontimestamp;legacyexchangedn;lockouttime;logoncount;mail;mailnickname;managedobjects;mdbusedefaults;memberof;msexchhomeservername;msexchmailboxguid;msexchmailboxsecuritydescriptor;msexchpoliciesincluded;msexchrbacpolicylink;msexchrecipientdisplaytype;msexchrecipienttypedetails;msexchshadowproxyaddresses;msexchtextmessagingstate;msexchumdtmfmap;msexchuseraccountcontrol;msexchuserculture;msexchversion;msexchwhenmailboxcreated;mspkiaccountcredentials;mspkidpapimasterkeys;mspkiroamingtimestamp;mstsexpiredate;mstslicenseversion;mstsmanagingls;name;objectcategory;objectclass;objectguid;objectsid;primarygroupid;protocolsettings;proxyaddresses;publicdelegatesbl;pwdlastset;samaccountname;samaccounttype;serviceprincipalname;showinaddressbook;useraccountcontrol;userprincipalname;usnchanged;usncreated;whenchanged;whencreated;
$temp=explode(".",@mysql_result(mysql_query("select param from mainsettings where id=8 "),0,0));
                        
                $basedn = @mysql_result(mysql_query("select param from mainsettings where id=12 "),0,0).",DC=".$temp[1].",DC=".$temp[2];  //for users show
                    // dotaz na spec ucet nebo na vsechny ucty               
                    if (!@$fn_user){$sr = ldap_list($ds, $basedn,"samaccountname=*");}
                    else {$sr = ldap_list($ds, $basedn,"samaccountname=".$fn_user);}
                $info = ldap_get_entries($ds, $sr);
                    for ($i=0; $i < $info["count"]; $i++) {
                        $a[$i][0] = $info[$i]["samaccountname"][0];
                        $a[$i][1] = $info[$i]["name"][0];
                        $a[$i][2] = $info[$i]["mail"][0];
                    }
        }
    }
    array_multisort(@$a, SORT_ASC);
return @$a;
}


function fn_send_mail($fn_recipient,$fn_subject,$fn_body){
  require "./modules/mailer/class.phpmailer.php";
  $mail = new PHPMailer();
  $mail->IsSMTP();  // k odeslání e-mailu použijeme SMTP server
  $mail->Host = @mysql_result(mysql_query("select param from mainsettings where name='mail_server' "),0,0);  // zadáme adresu SMTP serveru
  $mail->SMTPAuth = false;               // nastavíme true v případě, že server vyžaduje SMTP autentizaci
  $mail->Username = "";   // uživatelské jméno pro SMTP autentizaci
  $mail->Password = "";            // heslo pro SMTP autentizaci
  $mail->From = mysql_result(mysql_query("select param from mainsettings where id='202' "),0,0);  
  $mail->FromName = mysql_result(mysql_query("select param from mainsettings where id='1' "),0,0);
  $mail->AddAddress ($fn_recipient,"");
  $mail->Subject = $fn_subject;
  $mail->Body = $fn_body;
  $mail->WordWrap = 100;   // je vhodné taky nastavit zalomení (po 50 znacích)
//  $mail->CharSet = "windows-1250";   // nastavíme kódování, ve kterém odesíláme e-mail
  $mail->CharSet = "utf-8";   // nastavíme kódování, ve kterém odesíláme e-mail
  $mail->IsHTML(true);

  if(!$mail->Send()) {  // odešleme e-mail
     echo 'Došlo k chybě při odeslání e-mailu.<br>';
     echo 'Chybová zpráva: ' . $mail->ErrorInfo;
  }    
    
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



function program_log($a,$b,$c){
if ($b){unlink('./log/'.$c);}
    
if (!is_dir("./log")) {mkdir ("./log",0777);}
        if (@File_Exists("./log/".$c)){
            @$temp_data = file_get_contents("./log/".$c);
        }

        @$f=fopen("./log/".$c,"w");
        fwrite(@$f,@$temp_data.$a."\r\n");fclose($f);
}


function program_data_file($a,$b,$c){
if ($b){unlink('./data/'.$c);}
    
if (!is_dir("./data")) {mkdir ("./data",0777);}
        if (@File_Exists("./data/".$c)){
            @$temp_data = file_get_contents("./data/".$c);
        }

        @$f=fopen("./data/".$c,"w");
        fwrite(@$f,@$temp_data.$a."\r\n");fclose($f);
}







function SMBMap($username, $password, $server, $dir) {
    $command = "mount -t smbfs -o username=$username,password=$password //$server/$dir /mnt/tmp";
    echo system($command);
}

function SMBRelease() {
    $command = "umount /mnt/tmp";
    echo system($command);
}

function GetFiles($dir) {
    $files = array();
    if (is_dir($dir)) {
        if ($dh = opendir($dir)) {
            while (($file = readdir($dh)) !== false) {
                $files[] = $file."{".filetype("$dir/$file")."}";
            }
            closedir($dh);
        }
    }
    return $files;                
}

//query command 
//SMBMap("Daniel", "", "10.0.0.2", "Kram");
//$any = GetFiles("/mnt/tmp");
//SMBRelease();
//print_r($any);







?>
