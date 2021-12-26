<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc.php");
require_once ("./config/mssql_dbconnect.php");
require_once ("./functions/php/knihovna.php");

?>

<html>
<head>
<link rel="icon" href="http://127.0.0.1/HotLine/modules/catalog/config/company.ico" type="image/x-icon" />
<link rel="shortcut icon" href="http://127.0.0.1/HotLine/modules/catalog/config/company.ico" type="image/x-icon" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href='./css/catalog.css' />

</head>
<body id="body">
<div  style="overflow-y:scroll;width:100%;height:100%;" >
<table border=1px style="width:100%;height:100%border:1px;overflow: visible;font-size:10px;"> 

<tr background-color=blue >
<td><?echo dictionary("product_group",$_SESSION["language"]);?></td>
<td><?echo dictionary("nomenclature_group",$_SESSION["language"]);?></td>
<td><?echo dictionary("model",$_SESSION["language"]);?></td>
<td><?echo dictionary("height",$_SESSION["language"]);?></td>
<td><?echo dictionary("width",$_SESSION["language"]);?></td>
<td><?echo dictionary("depth",$_SESSION["language"]);?></td>
<td><?echo dictionary("language",$_SESSION["language"]);?></td>
<td><?echo dictionary("pricelist",$_SESSION["language"]);?></td>
<td><?echo dictionary("lang_cs",$_SESSION["language"]);?></td>
<td><?echo dictionary("lang_en",$_SESSION["language"]);?></td>
<td><?echo dictionary("lang_de",$_SESSION["language"]);?></td>
<td><?echo dictionary("lang_ru",$_SESSION["language"]);?></td>
<td><?echo dictionary("internal_marking",$_SESSION["language"])."/".
dictionary("name",$_SESSION["language"])."/".
dictionary("catalog_marking",$_SESSION["language"])."/".
dictionary("weight",$_SESSION["language"])."/".
dictionary("measurement_unit",$_SESSION["language"])."/".
dictionary("size",$_SESSION["language"])."/".
dictionary("index",$_SESSION["language"])."/".
dictionary("price",$_SESSION["language"])."/".
dictionary("exist",$_SESSION["language"])." x/"
;?></td>
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
	,pricelist.data_type
	,pricelist.systemname
	,'CESKY' = CASE 
		WHEN DATALENGTH (model.ckeditor_cs) <> 0 THEN 'ANO'
		ELSE 'NE'
	END
	,'ANGLICKY' = CASE 
		WHEN DATALENGTH (model.ckeditor_en) <> 0 THEN 'ANO'
		ELSE 'NE'
	END
	,'NEMECKY' = CASE 
		WHEN DATALENGTH (model.ckeditor_de) <> 0 THEN 'ANO'
		ELSE 'NE'
	END
	,'RUSKY' = CASE 
		WHEN DATALENGTH (model.ckeditor_ru) <> 0 THEN 'ANO'
		ELSE 'NE'
	END

FROM [dbo].[100_product_group] prod_groups
	 ,[dbo].[100_nomenclature_group] nomenclature
	 ,[dbo].[100_model] model
LEFT JOIN [dbo].[100_pricelist] pricelist
ON 1 = 1
WHERE
	prod_groups.data_type = nomenclature.parent_data_type
AND nomenclature.data_type = model.parent_data_type

ORDER BY
	 prod_groups.sequence
	,nomenclature.sequence
	,model.sequence
	 ASC
";
//cyklus vsech modelu / duplicitni k poctu ceniku
        //program_log($sql,"DELETE","sql.log");
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {

        echo"<tr >";
            for ($pos=0; $pos < sqlsrv_num_fields($check)  ; $pos++) {
                echo "<td style='border: 1px #004171 solid;' >".$row[$pos]."</td>";
            }    

echo "<td>";

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
  ,nomenklatura.nazev
  ,'user_kat_znaceni_2' = nomenklatura.user_kat_oznaceni + ' - ' + CASE WHEN substring(RIGHT(nomenklatura.cislo_nomenklatury,5),1,1) = '0' THEN RIGHT(nomenklatura.cislo_nomenklatury,4) ELSE RIGHT(nomenklatura.cislo_nomenklatury,5) END
  ,( CONVERT(varchar(50),CONVERT(float,nomenklatura.brutto)) + ' Kg') AS brutto
  ,nomenklatura.id_mj
  ,'".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x
  ".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x
  ".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."' AS size
  ,CASE 
  WHEN substring(RIGHT(nomenklatura.cislo_nomenklatury,6),1,2) = '00' THEN RIGHT(nomenklatura.cislo_nomenklatury,4)
  WHEN substring(RIGHT(nomenklatura.cislo_nomenklatury,6),1,1) = '0' THEN RIGHT(nomenklatura.cislo_nomenklatury,5) 
  WHEN substring(RIGHT(nomenklatura.cislo_nomenklatury,5),1,1) = '0' THEN RIGHT(nomenklatura.cislo_nomenklatury,4) 
  ELSE RIGHT(nomenklatura.cislo_nomenklatury,5) 
  END AS order_number,
  
  (SELECT CONVERT(VARCHAR, CONVERT(float,polozky.cena)) + ' ' + polozky.ref_id_meny 
from dba.cn_cenik cenik,
dba.cn_cenik_polozky polozky
where platnost = 1
AND cenik.cenik = polozky.cenik
AND cenik.cenik = '".mssecuresql($row[7])."'
AND polozky.id_nomen = nomenklatura.id_nomen
AND cenik.poradi =  (SELECT  TOP 1 poradi FROM dba.cn_cenik WHERE cenik='".mssecuresql($row[7])."' ORDER BY poradi DESC)
AND polozky.poradi = (SELECT  TOP 1 poradi FROM dba.cn_cenik_polozky WHERE cenik='".mssecuresql($row[7])."' ORDER BY poradi DESC)
) as cena
,'pocet produktu' = (
SELECT COUNT(nomenklatura.cislo_nomenklatury) FROM dba.[nomenklatura] nomenklatura
     WHERE nomenklatura.nazev LIKE '% ".mssecuresql($temp_row[0])."%' 
--     AND nomenklatura.skupina_nomenklatur LIKE '".mssecuresql($row[1])."%'
     AND nomenklatura.skupina_nomenklatur LIKE '".mssecuresql($row[1])."'
     AND ((nomenklatura.user_std_rozmer LIKE ' ".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '0".mssecuresql($array_height[@floor((@$c/$temp_width_count)/$temp_depth_count)])."x%') 
     AND (nomenklatura.user_std_rozmer LIKE '%x ".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '%x".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x%' 
     OR nomenklatura.user_std_rozmer LIKE '%x0".mssecuresql($array_width[@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count) ])."x%') 
     AND (nomenklatura.user_std_rozmer LIKE '%x ".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."' 
     OR nomenklatura.user_std_rozmer LIKE '%x".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."' 
     OR nomenklatura.user_std_rozmer LIKE '%x0".mssecuresql($array_depth[(@$c - (((@floor((@$c/$temp_width_count)/$temp_depth_count))*$temp_width_count*$temp_depth_count)+((@floor((@$c/$temp_depth_count)) - (@floor((@$c/$temp_width_count)/$temp_depth_count)*$temp_width_count))*$temp_depth_count)))])."')) 
     AND nomenklatura.platnost = '1' AND nomenklatura.typ = '7'
)
";

$temp_row = explode ("_",$row[2]);
    @$sql =  "SELECT TOP 1 $choosed_db_fields FROM dba.[nomenklatura] nomenklatura
     WHERE nomenklatura.nazev LIKE '% ".mssecuresql($temp_row[0])."%' 
     AND nomenklatura.skupina_nomenklatur LIKE '".mssecuresql($row[1])."'
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

echo "<table border=1px style='border:1px;overflow: visible;font-size:10px;' >";
        //program_log($sql,"","sql.log");
        @$karat_check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$karat_row = sqlsrv_fetch_array( @$karat_check, SQLSRV_FETCH_BOTH ) ) {

        echo"<tr >";
            for ($karat_pos=0; $karat_pos < sqlsrv_num_fields($karat_check)  ; $karat_pos++) {
                echo "<td>".$karat_row[$karat_pos]."</td>";
            }
            echo "</tr>";
        }echo "</table>";
     
}
     
echo "</td></tr>";            
}

echo "</table>";
?>
</div>
</body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/karat_catalog_setting.js");

?>