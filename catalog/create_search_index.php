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
<table border=1px style="width:100%;height:100%border:1px;overflow: visible;font-size:10px;"> 

<tr background-color=blue >
<td><?echo dictionary("product_group",$_SESSION["language"]);?></td>
<td><?echo dictionary("nomenclature_group",$_SESSION["language"]);?></td>
<td><?echo dictionary("karat_id",$_SESSION["language"]);?></td>
<td><?echo dictionary("catalog_marking",$_SESSION["language"]);?></td>
<td><?echo dictionary("std_prod_size",$_SESSION["language"]);?></td>
</tr>
<?	 
@$sql = " SELECT 
	 prod_groups.data_type
	,model.systemname
	,model.data_type
	,'height' = CASE 
		WHEN (SELECT '1' FROM [dbo].[100_std_prod_size] WHERE parent_data_type = model.data_type AND systemname = 'height') = 1 THEN 
			(SELECT data_type FROM [dbo].[100_std_prod_size] WHERE parent_data_type = model.data_type AND systemname = 'height')
		ELSE (SELECT data_type FROM [dbo].[100_std_prod_size] WHERE parent_data_type = '' AND systemname = 'height')
	END
	,'width' = CASE 
		WHEN (SELECT '1' FROM [dbo].[100_std_prod_size] WHERE parent_data_type = model.data_type AND systemname = 'width') = 1 THEN 
			(SELECT data_type FROM [dbo].[100_std_prod_size] WHERE parent_data_type = model.data_type AND systemname = 'width')
		ELSE (SELECT data_type FROM [dbo].[100_std_prod_size] WHERE parent_data_type = '' AND systemname = 'width')
	END
	,'depth' = CASE 
		WHEN (SELECT '1' FROM [dbo].[100_std_prod_size] WHERE parent_data_type = model.data_type AND systemname = 'depth') = 1 THEN 
			(SELECT data_type FROM [dbo].[100_std_prod_size] WHERE parent_data_type = model.data_type AND systemname = 'depth')
		ELSE (SELECT data_type FROM [dbo].[100_std_prod_size] WHERE parent_data_type = '' AND systemname = 'depth')
	END
FROM [dbo].[100_product_group] prod_groups
	 ,[dbo].[100_nomenclature_group] nomenclature
	 ,[dbo].[100_model] model
WHERE
	prod_groups.data_type = nomenclature.parent_data_type
AND nomenclature.data_type = model.parent_data_type

ORDER BY
	 prod_groups.sequence
	,nomenclature.sequence
	,model.sequence
	 ASC
";

$update_command=" TRUNCATE TABLE [dbo].[120_product_search] ";

//cyklus vsech modelu / duplicitni k poctu ceniku
        //program_log($sql,"DELETE","sql.log");
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {

$temp_height = explode("-", @$row[3]); 
$temp_width = explode("-", @$row[4]);
$temp_depth = explode("-", @$row[5]); 

unset($array_height);  
unset($array_width);  
unset($array_depth);  




$temp_height_count=0;$temp_width_count=0;$temp_depth_count=0;
for ($c=$temp_height[0]; $c <= $temp_height[1]  ; $c=$c+$temp_height[2]) {
    $array_height[$temp_height_count]=$c;
    $temp_height_count++;
}
for ($c=$temp_width[0]; $c <= $temp_width[1]  ; $c=$c+$temp_width[2]) {
    $array_width[$temp_width_count]=$c;
    $temp_width_count++;
    }
for ($c=$temp_depth[0]; $c <= $temp_depth[1]  ; $c=$c+$temp_depth[2]) {
    $array_depth[$temp_depth_count]=$c;
    $temp_depth_count++;
    }


for ($c=0; $c <= ($temp_height_count*$temp_width_count*$temp_depth_count)  ; $c++) {

$choosed_db_fields=" nomenklatura.cislo_nomenklatury
  ,'user_kat_znaceni_2' = nomenklatura.user_kat_oznaceni + ' - ' + CASE WHEN substring(RIGHT(nomenklatura.cislo_nomenklatury,5),1,1) = '0' THEN RIGHT(nomenklatura.cislo_nomenklatury,4) ELSE RIGHT(nomenklatura.cislo_nomenklatury,5) END
  ,'".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."' AS size";

$temp_row = explode ("_",$row[2]);
    @$sql =  "SELECT TOP 1 $choosed_db_fields FROM dba.[nomenklatura] nomenklatura
     WHERE nomenklatura.nazev LIKE '% ".mssecuresql($temp_row[0])."%' 
     AND nomenklatura.skupina_nomenklatur LIKE '".mssecuresql($row[1])."%'
     AND ((nomenklatura.user_std_rozmer LIKE ' ".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '0".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x%') 
     AND (nomenklatura.user_std_rozmer LIKE '%x ".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '%x".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '%x0".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x%') 
     AND (nomenklatura.user_std_rozmer LIKE '%x ".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."' 
     OR nomenklatura.user_std_rozmer LIKE '%x".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."' 
     OR nomenklatura.user_std_rozmer LIKE '%x0".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."')) 
     AND nomenklatura.platnost = '1' AND nomenklatura.typ = '7' ";

        //program_log($sql,"","sql.log");
        @$karat_check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$karat_row = sqlsrv_fetch_array( @$karat_check, SQLSRV_FETCH_BOTH ) ) {
            $update_command .=" INSERT INTO [dbo].[120_product_search] ([product_group],[model],[karat_inernal_no],[karat_catalog_mark],[sizes],[update_time],[update_user]) VALUES ('$row[0]','$row[1]',";
            echo"<tr >";
                echo "<td style='border: 1px #004171 solid;' >".$row[0]."</td>";
                echo "<td style='border: 1px #004171 solid;' >".$row[1]."</td>";
            for ($karat_pos=0; $karat_pos < sqlsrv_num_fields($karat_check)  ; $karat_pos++) {
                echo "<td style='border: 1px #004171 solid;'>".$karat_row[$karat_pos]."</td>";
            $update_command .="'$karat_row[$karat_pos]',";
            }
            echo "</tr>";
            $update_command .="GETDATE(),'".mssecuresql(@$_SESSION['lnamed'])."')            
";
        }
    }            
} echo "</table>";
@$karat_check = sqlsrv_query( $conn, $update_command , $params, $options );



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