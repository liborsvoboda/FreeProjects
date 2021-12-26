<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc.php");
require_once ("./config/mssql_dbconnect.php");
require_once ("./functions/php/knihovna.php");

?>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/catalog.css' />
</head>
<body id="body">
<div  style="overflow-y:scroll;width:100%;height:100%;" >
<table id=data_table border=2 frame=border rules=all  > 

<tr style=background-color:#98D1FF >
<td><?echo dictionary("action",$_SESSION["language"]);?></td>
<td><?echo dictionary("client_login",$_SESSION["language"]);?></td>
<td><?echo dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]);?></td>
<td><?echo dictionary("company",$_SESSION["language"]);?></td>
<td><?echo dictionary("ico",$_SESSION["language"]);?></td>
<td><?echo dictionary("email",$_SESSION["language"]);?></td>
<td><?echo dictionary("last_login",$_SESSION["language"]);?></td>
<td><?echo dictionary("blocked_account",$_SESSION["language"]);?></td>
</tr>
<?	 
@$sql = " SELECT [id]
      ,[login_name]
      ,[full_name]
      ,[company]
      ,[ico]
      ,[email]
      ,[last_login]
	  ,blocked
  FROM [dbo].[120_registration] ORDER BY id DESC 
";
        //program_log($sql,"","sql.log");
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            echo"<tr >";
                echo "<td  ><img src=./images/reload_email.png style=width:16px;height:16px;border:0px;cursor:pointer; title=\"".dictionary("change_email",$_SESSION["language"])."\" onclick=check_mail_update(\"".$row[0]."\",\"email\",\"120_registration\"); /> <img src=./images/delete.png style=width:16px;height:16px;border:0px;cursor:pointer; title=\"".dictionary("delete",$_SESSION["language"])."\" onclick=check_delete(\"".$row[0]."\",\"".$row[1]."\",\"120_registration\");parent.user_administration(); />";            
                echo "<td  >".$row[1]."</td>";
                echo "<td  >".$row[2]."</td>";
                echo "<td  >".$row[3]."</td>";
                echo "<td >".$row[4]."</td>";
                echo "<td  >".$row[5]."</td>";
                echo "<td  >".$row[6]."</td>";
                echo "<td style='text-align:center;' ><input type=checkbox onclick=fn_update_record(\"".$row[0]."\",this.checked,\"blocked\",\"120_registration\"); ";
                if (@$row[7]==1) {echo "checked=checked ";}
                echo " ></td>";
            echo "</tr>";
        }
echo "</table>";

?>
</div>
</body>
</html>
<script>parent.document.getElementById("loading").style.display="none";</script>
<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/karat_catalog_setting.js");
?>