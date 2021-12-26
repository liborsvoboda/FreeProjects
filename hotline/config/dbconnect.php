<?mysql_connect('127.0.0.1', 'root', 'password');
mysql_select_db(@$_SESSION['dbselect']) or die (MySQL_Error());
mysql_query("SET NAMES 'utf8'");
?>