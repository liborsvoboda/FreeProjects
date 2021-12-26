<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc");
require_once ('./config/dbconnect.php');
require_once ("./functions/php/knihovna.php");
require_once ("./functions/php/mssql_knihovna.php");
require_once ("./config/mssql_dbconnect.php");

//if (@$_SESSION["lnamed"]) {
    ?>
<html>
<head>
<script type='text/javascript'>
 parent.document.getElementById("openned_app").innerHTML ="<?echo dictionary('karat_terminal',$_SESSION['language']);?>";
</script>
<link rel="icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<link rel="shortcut icon" href="http://<?echo $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];?>config/company.ico" type="image/x-icon">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link rel="stylesheet" type="text/css" href='./css/default/main_window.css' />
<link rel="stylesheet" type="text/css" href='./css/default/user_settings.css' />

<? //saving

// end of saving
?>

</head>


<Body>
<table id='fullframetable' onselectstart="return true;" >
<tr style='width:100%;height:25px;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<?
echo "<span class=\"desktop_no_out\" onmouseout=\"className='desktop_no_out';\" onmouseover=\"className='desktop_no_in';\" onclick=\"reload_page();\" style=font-size:12px; >".strtoupper(dictionary("refresh",$_SESSION["language"]))."</span>";
        
        
$load_data=mysql_query("select * from karat_terminal_menu order by position ASC") or die (dictionary("sql_command",$_SESSION["language"])." > ".MySQL_Error());
$load=0;while($load<mysql_num_rows($load_data)):
$checked_menu=0;

        echo "<span ";
        if (@$_REQUEST["option"]==mysql_result($load_data,$load,2) or (!@$_REQUEST["option"] && $load==0)) {echo " class=\"bookmarkin\" ";$checked_menu=1;}
        if ($checked_menu<>1 && @$_REQUEST["option"]<>mysql_result($load_data,$load,2)) {echo " class=\"bookmarkout\" onmouseout=\"className='bookmarkout';\" onmouseover=\"className='bookmarkin';\" onclick=\"window.location.assign('./karat_terminal.php?option=".mysql_result($load_data,$load,2)."')\" ";}
        echo " style=font-size:12px; >".mysql_result($load_data,$load,2)."</span>";

$load++;endwhile;?></td></tr>
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >


<?
if (@$_REQUEST["option"]==""){ @$_REQUEST["option"] = "AMÁDA";} 
?><div id='bookmark' >
<table id='view_table' style='width:100%;font-size:10px;height:100%;' frame="border" rules=all >

<?

echo "<tr style=height:20px;background-color:#B4E3F7;text-align:center; >
<td>".dictionary("date",$_SESSION["language"])."</td>
<td>".dictionary("source",$_SESSION["language"])."</td>
<td>".dictionary("source_name",$_SESSION["language"])."</td>
<td>".dictionary("event",$_SESSION["language"])."</td>
<td>".dictionary("person_id",$_SESSION["language"])."</td>
<td>".dictionary("surname",$_SESSION["language"])."</td>
<td>".dictionary("name",$_SESSION["language"])."</td>
<!--<td>".dictionary("",$_SESSION["language"])."</td>-->
<td>".dictionary("shift",$_SESSION["language"])."</td>
<td>".dictionary("tech_procedure",$_SESSION["language"])."</td>
<td>".dictionary("contract",$_SESSION["language"])."</td>
<td>".dictionary("request_from",$_SESSION["language"])."</td>
<td>".dictionary("request_to",$_SESSION["language"])."</td>
<td>".dictionary("product_time",$_SESSION["language"])."</td>
<td>".dictionary("product",$_SESSION["language"])."</td>
<td>".dictionary("contract_note",$_SESSION["language"])."</td>
<td>".dictionary("operation",$_SESSION["language"])."</td>
<td>".dictionary("plan",$_SESSION["language"])."</td>
<td>".dictionary("finished",$_SESSION["language"])."</td>
<td>".dictionary("scrap",$_SESSION["language"])."</td>
</tr>";

    $sql = "SELECT
     CONVERT(VARCHAR(10), obs.datum, 103) + ' '  + convert(VARCHAR(8), obs.datum, 14) 
    ,process2.zdroj
	,zdroje.nazev
	,'typ_udalosti' = CASE
    WHEN process.typ_udalosti <> '' THEN process.typ_udalosti
--		WHEN process.typ_udalosti = 'DS' THEN 'DS docházka příchod na pracoviště'
--		WHEN process.typ_udalosti = 'DA' THEN 'DA přerušení'
--		WHEN process.typ_udalosti = 'DF' THEN 'DF odchod z pracoviště'
--		WHEN process.typ_udalosti = 'GS' THEN 'GS začátek skupinové operace'
--		WHEN process.typ_udalosti = 'GA' THEN 'GA přerušení skupinové operace'
--		WHEN process.typ_udalosti = 'GF' THEN 'GF dokončení skupinové operace'
--		WHEN process.typ_udalosti = 'PS' THEN 'PS začátek seřízení'
--		WHEN process.typ_udalosti = 'PA' THEN 'PA přerušení seřízení'
--		WHEN process.typ_udalosti = 'PF' THEN 'PF konec seřízení'
--		WHEN process.typ_udalosti = 'PN' THEN 'PN příprava normočasem'
--		WHEN process.typ_udalosti = 'PT' THEN 'PT Příprava se zadaným časem'
--		WHEN process.typ_udalosti = 'PU' THEN 'PU Dokončení přípravy/seřízení a operace'
--		WHEN process.typ_udalosti = 'WS' THEN 'WS zahájení operace'
--		WHEN process.typ_udalosti = 'WA' THEN 'WA přerušení operace'
--		WHEN process.typ_udalosti = 'WF' THEN 'WF ukončení operace'
--		WHEN process.typ_udalosti = 'WC' THEN 'WC předání práce'
--		WHEN process.typ_udalosti = 'WG' THEN 'WG převzetí práce'
--		WHEN process.typ_udalosti = 'WN' THEN 'WN odvedení normočasem'
--		WHEN process.typ_udalosti = 'WX' THEN 'WX ukončení časové operace'
--		WHEN process.typ_udalosti = 'WT' THEN 'WT Výrobní operace se zadaným časem'
--		WHEN process.typ_udalosti = 'SV' THEN 'SV výdej ze skladu do výroby'
--		WHEN process.typ_udalosti = 'SP' THEN 'SP příjem na sklad'
	ELSE 'Nedefinováno'
	END
    ,osoby.osoba
	,osoby.prijmeni
	,osoby.jmeno
	--,process.id_vyr_oper
	,'obs.smena_vp' = CASE
		WHEN obs.smena_vp = 1 THEN 'Ranní Směna'
		WHEN obs.smena_vp = 2 THEN 'Odpolední Směna'
		WHEN obs.smena_vp = 3 THEN 'Noční Směna'
		WHEN obs.smena_vp = 4 THEN '4'
		WHEN obs.smena_vp = 5 THEN '5'
		WHEN obs.smena_vp = 6 THEN '6'
		ELSE 'Nedefinováno'
	END
    ,'tech_procedure' = CASE
        WHEN ter_out.opv <>'' THEN ISNULL((SELECT TOP 1  postup FROM dba.[v_opvvyrza] WHERE opv = ter_out.opv ),'') 
        ELSE ''
        END
	,ter_out.opv
    ,'request_from' = CASE
        WHEN ter_out.opv <>'' THEN ISNULL((SELECT TOP 1  CONVERT(VARCHAR(10), [terminzac], 103) + ' '  + convert(VARCHAR(8), [terminzac], 14) FROM dba.[v_opvoper] WHERE opv = ter_out.opv AND polozka = ter_out.polozka),'') 
        ELSE ''
        END
    ,'request_to' = CASE
        WHEN ter_out.opv <>'' THEN ISNULL((SELECT TOP 1  CONVERT(VARCHAR(10), [terminkon], 103) + ' '  + convert(VARCHAR(8), [terminkon], 14) FROM dba.[v_opvoper] WHERE opv = ter_out.opv AND polozka = ter_out.polozka),'') 
        ELSE ''
        END
    ,'product_time' = CASE
    WHEN ter_out.opv <>'' THEN 
				convert(VARCHAR(8),(SELECT TOP 1 CONVERT(VARCHAR(12),
    			CAST(FLOOR((((tas * ISNULL(planvyroba,1)) + tbs)*60) /3600) AS  VARCHAR(6))
    			+':'+
    			CAST(FLOOR((tas * ISNULL(planvyroba,1))+tbs) - (FLOOR(((tas * ISNULL(planvyroba,1))+tbs)/60)*60) AS VARCHAR(2))
    			+':'+
    			CAST (FLOOR(((tas * ISNULL(planvyroba,1))+tbs)*60) - (FLOOR((tas * ISNULL(planvyroba,1))+tbs)*60) AS VARCHAR(2))
                )
                FROM dba.[v_opvoper] WHERE opv = ter_out.opv AND polozka = ter_out.polozka)
                , 14)  
    ELSE ''
    END       
    ,ter_out.popis_zp
	,ter_out.popis_oper
	,ter_out.polozka
	,CONVERT(FLOAT,ter_out.planvyroba)
	,CONVERT(FLOAT,ter_out.odvedeno)
	,CONVERT(FLOAT,ter_out.zmetky)
    ,typ_udalosti.typ_udalosti
    ,pers.mobil
    ,'delayed' =  CASE
    WHEN ter_out.opv <>'' THEN CASE
			WHEN (((tas * ISNULL((ter_out.planvyroba - ter_out.odvedeno-ter_out.zmetky),1)) + tbs)*60) > 0 THEN CASE
				WHEN 
				(SELECT TOP 1  [terminkon] FROM dba.[v_opvoper] WHERE opv = ter_out.opv AND polozka = ter_out.polozka) >= DATEADD(ss,(((tas * ISNULL((ter_out.planvyroba - ter_out.odvedeno-ter_out.zmetky),1)) + tbs)*60),obs.datum) THEN 'OK'
				ELSE 'DELAYED'
				END
			ELSE 'NADLIMIT'	
			END
	ELSE 'GROUP'
	END
FROM dba.v_ter_proc process
	,dba.v_ter_proc2 process2
	,dba.odb_osoby osoby
	,dba.v_ter_obs obs
	,dba.v_ter_out ter_out
	,dba.zdr_zdr zdroje
    ,dba.zdr_prac pracoviste
    ,dba.v_ter_obs logged
    ,dba.v_ter_typ_udalosti typ_udalosti
    ,dba.pers pers
WHERE 
    osoby.osoba = process2.osoba
AND process.id_procesu = process2.id_procesu
AND process2.stav = 0
AND process2.zdroj <>''
AND process2.osoba = obs.osoba
AND ter_out.id_vyr_oper = process.id_vyr_oper
AND process2.zdroj = zdroje.zdroj
AND zdroje.[pracoviste] = pracoviste.[pracoviste]
AND pracoviste.[nazev] = '".@$_REQUEST["option"]."'
AND osoby.osoba = logged.osoba
AND logged.typ_udalosti = typ_udalosti.typ_udalosti
AND osoby.osoba = pers.oscislo

ORDER BY
    obs.datum,
    process2.zdroj
";
    program_log($sql,"DELETE","sql.log");
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "<tr title='".dictionary("mobile",$_SESSION["language"]).": ".@$row[20]."' class='view_dataoff' onmouseout=\"className='view_dataoff';\" onmouseover=\"className='view_dataon';\" ";
        
$current_color="NO";
$temp_time = explode (" ",$row[0]);
$temp_date = explode ("/",$temp_time[0]);
$temp_time1 = explode (" ",$row[11]);
$temp_date1 = explode ("/",$temp_time1[0]);
$temp_time2 = explode (" ",$row[10]);
$temp_date2 = explode ("/",$temp_time2[0]);

//coloring records

if (@$row[19] == "DA" && $current_color=="NO") {
    echo "status=PAUSE style=background:#B1DDFE";$current_color="YES";}

if (strtotime($temp_date[2]."-".$temp_date[1]."-".$temp_date[0]." ".$temp_time[1]) > 
strtotime($temp_date1[2]."-".$temp_date1[1]."-".$temp_date1[0]." ".$temp_time1[1]) and $row[11]<>""
&& $current_color=="NO") { echo "status=DELAYED style=background:#FEB4B4";$current_color="YES";}

if (@$row[21]=="DELAYED" && $current_color=="NO") { echo "status=ORANGE style=background-color:#FFD586 ";$current_color="YES";}

        echo" >";
            for ($pos=0; $pos < (sqlsrv_num_fields($check) - 2) ; $pos++) {
                echo "<td>".$row[$pos]."</td>";
            }    
        echo "</tr>";
    }
?>
<tr></tr>
</table>
</div>



</td></tr></table></body>

<div style="position:absolute;right:22px;top:33px;vertical-align:middle;" >
<?
echo " 
<button onclick=\"show_records('ALL');\" style=position:relative;top:-1px;height:25px;font-size:12px; >".strtoupper(dictionary("all",$_SESSION["language"]))."</button>
<button onclick=\"show_records('PAUSE');\" style=position:relative;top:-1px;height:25px;font-size:12px;background-color:#B1DDFE; >".strtoupper(dictionary("pause",$_SESSION["language"]))."</button>
<button onclick=\"show_records('ORANGE');\" style=position:relative;top:-1px;height:25px;font-size:12px;background-color:#FFD586; >".strtoupper(dictionary("delayed",$_SESSION["language"]))."</button>
<button onclick=\"show_records('DELAYED');\" style=position:relative;top:-1px;height:25px;font-size:12px;background-color:#FEB4B4; >".strtoupper(dictionary("start_after_term",$_SESSION["language"]))."</button>
<span style='padding:4px;height:18px;font-size:12px;' >:".strtoupper(dictionary("legend",$_SESSION["language"]))."</span>";
?>
</div>
</html>

<?
require_once ("./functions/js/karat_terminal.js");
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/standard_scripts.js");

sqlsrv_close($conn);
?>
<script>
//setInterval('reload_page()', 30000 );
</script>


<?
//}
?>