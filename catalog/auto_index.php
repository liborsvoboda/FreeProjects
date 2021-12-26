<?php

require_once ('./config/main_variables.php');
require_once ("./config/mssql_dbconnect.php");
require_once ("./functions/php/knihovna.php");

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

        @$karat_check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$karat_row = sqlsrv_fetch_array( @$karat_check, SQLSRV_FETCH_BOTH ) ) {
            $update_command .=" INSERT INTO [dbo].[120_product_search] ([product_group],[model],[karat_inernal_no],[karat_catalog_mark],[sizes],[update_time],[update_user]) VALUES ('$row[0]','$row[1]',";
            for ($karat_pos=0; $karat_pos < sqlsrv_num_fields($karat_check)  ; $karat_pos++) {
            $update_command .="'$karat_row[$karat_pos]',";
            }
            $update_command .="GETDATE(),'TASKSCHEDULER')            
";
        }
    }            
}
@$karat_check = sqlsrv_query( $conn, $update_command , $params, $options );

?>