<?php
require_once ("./functions/php/sessions.inc.php");
require_once ("./config/mssql_dbconnect.php");
require_once ("./functions/php/knihovna.php");
require_once ("./functions/php/whois.php");

?>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/catalog.css' />
<?
//preparing records table

//opravit nacteni zlobi counter_domain
@$sql ="SELECT ip_address FROM dbo.[100_customer_visit] WHERE ([domain] = '' OR ISNULL([domain],'TRUE') = 'TRUE') GROUP BY ip_address ORDER BY ip_address";
@$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionasry("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
       $counter_domain = 'http://domains.yougetsignal.com/domains.php?remoteAddress=' . $row[0] . '&key=';
     $json = file_get_contents($counter_domain);$counter_domain="";
    if (preg_match_all('/\["(.*?)",/i', $json, $match)) {
        count($match[1]);
        foreach ($match[1] as $list) {
            $counter_domain.=$list.",";
        }
    }
  if ($counter_domain =="") {$counter_domain = gethostbyaddr ( $row[0] );}
@$update_sql = " UPDATE dbo.[100_customer_visit] SET domain = '".mssecuresql($counter_domain)."' WHERE ip_address= '".mssecuresql($row[0])."' AND ([domain] = '' OR ISNULL([domain],'TRUE') = 'TRUE') ";
sqlsrv_query( $conn, $update_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
}
//end off preparing records table
?>
</head>


<body>
<div id="basic_table" >
<table style="width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;" >
<tr style="width:100%;height:100%;"><td style="width:100%;height:100%;" >

<form action="<?echo $_SERVER["PHP_SELF"];?>" id="form1" name="form1" method="post" enctype="multipart/form-data">

<?echo
    "<table id=\"fullframetable\" style=\"width:100%;height:100%;\" >
    <tr><td colspan=2 style=\"width:100%;height:5%;text-align:center;\" ><H2>".dictionary("traffic",$_SESSION["language"])."</H2></td></tr>
    
    <tr><td style=\"width:30%;height:75%;text-align:center;\" >
    <table style=width:100%;height:100%;><tr><td style=width:40%; >
    ".dictionary("from_date",$_SESSION["language"])."</td><td style=width:60%; > 
    <select id=s_value1 name=s_value1 style=width:100%; size=1>";
    echo "<option></option>";
    @$sql = "SELECT CONVERT(VARCHAR(10),visit_date,104) from dbo.[100_customer_visit] GROUP BY CONVERT(VARCHAR(10),visit_date,104) ORDER BY CONVERT(DATETIME,CONVERT(VARCHAR(10),visit_date,104),104) ASC ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "<option value=\"".@$row[0]."\" >".@$row[0]."</option>";
    }
    echo "</select></td></tr>
    <tr><td>
    ".dictionary("to_date",$_SESSION["language"]).":</td><td>
    <select id=s_value2 name=s_value2 style=width:100%; size=1>";
    echo "<option></option>";
    @$sql = "SELECT CONVERT(VARCHAR(10),visit_date,104) from dbo.[100_customer_visit] GROUP BY CONVERT(VARCHAR(10),visit_date,104) ORDER BY CONVERT(DATETIME,CONVERT(VARCHAR(10),visit_date,104),104) DESC ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "<option value=\"".@$row[0]."\" >".@$row[0]."</option>";
    }
    echo "</select></td></tr>
    <tr><td>
    ".dictionary("interval",$_SESSION["language"]).":</td><td style=width:60%;>
    <select id=s_value3 name=s_value3 style=width:100%; size=1>
    <option></option>
    <option value=\"hourly\" >".dictionary("hourly",$_SESSION["language"])."</option>
    <option value=\"daily\" >".dictionary("daily",$_SESSION["language"])."</option>
    <option value=\"weekly\" >".dictionary("weekly",$_SESSION["language"])."</option>
    <option value=\"monthly\" >".dictionary("monthly",$_SESSION["language"])."</option>
    <option value=\"yearly\" >".dictionary("yearly",$_SESSION["language"])."</option>
    </select></td></tr>
    <tr><td colspan=2>
    <input type=button onclick=load_graph(\"img_graph\",\"s_value\"); name=btn_value3 value=\"".dictionary("display_graph",$_SESSION["language"])."\">
    </td></tr>
    <tr><td colspan=2 style=width:60%;>
    <select id=sel_value1 onclick=document.getElementById(\"btn_value1\").disabled=false;document.getElementById(\"btn_value2\").disabled=false; name=sel_value1 size=20 style=width:100%;vertical-align:middle; >";

    @$sql = " SELECT domain from dbo.[100_customer_visit] GROUP BY ip_address,domain ORDER BY ip_address,domain ";
    //program_log($sql,'','sql.log');
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
    $temp_data=explode (",", @$row[0]);
        $temp_cykl=0;while(@$temp_data[$temp_cykl]):
            echo"<option value=\"".@$temp_data[$temp_cykl]."\" >".@$temp_data[$temp_cykl]."</option>";
        $temp_cykl++;endwhile;
    }echo "</select><p id=\"linespace\" ></p>
    <input type=\"button\" disabled id=\"btn_value1\" onclick=open_website(\"sel_value1\") name=\"btn_value1\" value=\"".dictionary("open_site",$_SESSION["language"])."\" style=width:48%; >
    <input type=\"button\" disabled id=\"btn_value2\" onclick=load_whois(\"sel_value1\") value=\"".dictionary("domain_owner",$_SESSION["language"])."\" style=width:48%; >
    </td></tr></table></td><td style=\"width:70%;height:75%;text-align:center;\" >";

echo "<img id=img_graph src=\"\" ></td></tr>
<tr><td colspan=2 style=border:2px; >
".dictionary("whois",$_SESSION["language"])."
<iframe id=whois_field style=width:100%;height:100%; ></iframe>

</td></tr>
</table>";

?>
</form></td></tr></table>
</div>






</body>
</html>
<script>parent.document.getElementById("loading").style.display="none";</script>
<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/graf_report.js");
?>




