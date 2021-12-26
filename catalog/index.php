<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc.php");
require_once ("./config/mssql_dbconnect.php");
require_once ("./functions/php/knihovna.php");

ip_visitor();

//Currency
@$sql =  "SELECT cenik.id_meny FROM dba.cn_cenik cenik,[dbo].[100_pricelist] price_list WHERE price_list.[systemname] = cenik.cenik AND price_list.[data_type] = '".$_SESSION["language"]."' ";
@$check = sqlsrv_query( $conn, $sql , $params, $options );
@$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
if (@$row[0]) {$_SESSION['Currency_Id']=$row[0];}





?>

<html>
<head>
<title><?@$sql =  "SELECT systemname FROM dbo.[100_main_setting] WHERE data_type = 'title'";@$check = sqlsrv_query( $conn, $sql , $params, $options );
@$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );echo dictionary($row[0],$_SESSION["language"]);?>
</title>
<link rel="icon" href="http://127.0.0.1/HotLine/modules/catalog/config/company.ico" type="image/x-icon" />
<link rel="shortcut icon" href="http://127.0.0.1/HotLine/modules/catalog/config/company.ico" type="image/x-icon" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href='./css/catalog.css' />
<link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" />
<script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script>
<script type="text/javascript">Shadowbox.init();
var fn_items = new Array();
var reg_session = true;
var delivery_adr_count = 0;
</script>

<?
require_once ("./functions/js/jquery.base64.js");


//saving

// end of saving
?>

</head>

<?@$sql =  "SELECT systemname FROM dbo.[100_main_setting] where data_type = 'body_style' ";
@$check = sqlsrv_query( $conn, $sql , $params, $options );@$style = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
?>
<body id="body" style="<?echo $style[0];?>" >
<table id='fullframetable' onselectstart="return false;" >
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >


<div id='bookmark' >
<table style='width:100%;height:100%;border:0px;cellpadding:0px;border:none;text-align:center;overflow:hidden;' >
<tr style='width:100%;height:100%;'><td style='width:100%;height:100%;' >
<form id='form1' method='post' enctype="multipart/form-data">
<table class="catalog">

<?
// header
echo "<tr class='header' >
    <td class='headermenu' ><input type=text id=in_search name=in_search value='".dictionary("search_text",$_SESSION["language"])."' class='search_input' /><span style=width:32%;padding-left:5px;text-align:left;padding-right:5px;><input class=search_button_out onmouseout=\"className='search_button_out';\" onmouseover=\"className='search_button_in';\" type=button value='".dictionary("search",@$_SESSION["language"])."' onclick=search_product(document.getElementById('in_search').value); ></span></td>
    <td class='headerunit' ><table style=width:100%;vertical-align:top;margin:0px;cellspacing:0px;cellpadding:0px;padding:0px; ><tr><td style=vertical-align:top;margin:0px;cellspacing:0px;cellpadding:0px;padding:0px;padding-left:5px; >";

    
   @$sql =  " SELECT * FROM dbo.[100_icon_panel] order by sequence";
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    $mainmenucount=0;
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        
        echo "<img id='main_panel_menu_".$mainmenucount."' alt='".dictionary($row[2],@$_SESSION["language"])."' onclick=\"";
        if (@$mainmenucount == 0) {echo "regenerate_session_permit('false');";}
        echo "check_logon_status('START');main_menu_select('main_panel_menu_".$mainmenucount."');load_datalist('100_icon_panel','".@$row[0]."');disable_sizes();image_radio_reset(radiocount);fn_close_other_menu('x',menucount);\" src='./ajax_functions.php?icon=YES&tbl=".code("100_icon_panel")."&id=".code($row[0])."'";
        if ($mainmenucount == 0 ){echo "class='fast_window_in' onmouseout=\"className='fast_window_out';\" onmouseover=\"className='fast_window_in';\" disabled=disabled />";} else{
                echo "class='fast_window_out' onmouseout=\"className='fast_window_out';\" onmouseover=\"className='fast_window_in';\" />";
                }
        echo "<span id='icon_space'></span>";
        $mainmenucount++;
}    

echo"</td><td style=text-align:right;align:right; ><div style=aling:center;height:25px;vertical-align:middle;cursor:default; >";

        @$sql =  "SELECT * FROM dbo.[100_dictionary] where systemname like 'lang[_]%' order by id ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            echo "<span ";
            if (@$_SESSION["language"] == @$row[3]) {echo "class='flag_on'";}
                else {echo "class='flag_off' onmouseout=\"className='flag_off';\" onmouseover=\"className='flag_on';\"";}
            echo " onclick=confirmed_delete('FULL');regenerate_session_permit('false');change_language('".$row[3]."'); ><image src='./ajax_functions.php?icon=YES&tbl=".code("100_dictionary")."&id=".code($row[0])."' style=height:25px;border:0px;vertical-align:top; alt='".dictionary($row[3],@$_SESSION["language"])."' title='".dictionary($row[3],@$_SESSION["language"])."' ></span> ";
        }    
    echo"</div></td></tr></table>
    </td>
</tr>";

// body
echo "<tr class='middle'>
    <td class='body_item' rowspan=2 >";

echo "<div class=product_menu >".dictionary("products",@$_SESSION["language"])."</div>";
   @$sql =  " SELECT * FROM dbo.[100_product_group] order by sequence";
   $menucount=0;
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "<div onclick=\"main_menu_select('main_panel_menu_none');enable_items('');load_datalist('100_product_group','".@$row[0]."');fn_on_off_display('submenu".$row[0]."');image_radio_reset(radiocount);fn_group_image('".$row[0]."');fn_close_other_menu('".$row[0]."',menucount);\" class=product_group_out onmouseout=\"className='product_group_out';\" onmouseover=\"className='product_group_in';\" >
        <img id='group_image".$row[0]."' src=./images/plus.png class=plus_icon > ".dictionary($row[2],$_SESSION["language"]); 
      echo "</div>";  

   echo "<div id='submenu".$row[0]."' style=display:none;  >";
   @$sub_sql =  " SELECT * FROM dbo.[100_nomenclature_group] where parent_data_type = '".mssecuresql($row[1])."' order by sequence ";
    @$sub_check = sqlsrv_query( $conn, $sub_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    while( @$sub_row = sqlsrv_fetch_array( @$sub_check, SQLSRV_FETCH_BOTH ) ) {
        echo "<span onclick=enable_items('submenu_item".$row[0]."-".$sub_row[0]."');disable_object('submenu_item".$row[0]."-".$sub_row[0]."');load_datalist('100_nomenclature_group','".@$sub_row[0]."');sel_prod('".$sub_row[1]."');disable_sizes();enable_models('".$sub_row[1]."','MENU'); id=submenu_item".$row[0]."-".$sub_row[0]." class=product_out onmouseout=\"className='product_out';\" onmouseover=\"className='product_in';\" >
        ".dictionary($sub_row[2],$_SESSION["language"])."</span>";
        //'submenu_item".$row[0]."-".$sub_row[0]."';
        echo "<script>fn_items[fn_items.length]='submenu_item".$row[0]."-".$sub_row[0]."';</script>"; 
    }
      echo "</div>";  
      $menucount++;
      
    }
echo"<div class=product_submenu ><p id='linespace' ></p>".dictionary("size",@$_SESSION["language"])."<p id='linespace' ></p></div><div id=sizes style=width:95%; >";

$tbl_line_1="";
    @$sql =  " SELECT systemname FROM dbo.[100_main_setting] WHERE data_type = 'model' ORDER BY id ";
      @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
          
            @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
            $temp_data=explode(",",$row[0]);$cykl=0;
                while(@$temp_data[$cykl]):
                echo"<div class='model_line' >
                <image id=pict_radio".($cykl+1)." src='./images/radio_off.png' class='radio_off' value='".@$temp_data[$cykl]."' onclick='sel_prod(radio".($cykl+1).".sel_prod);enable_sizes(this.value,\"pict_radio".($cykl+1)."\");load_datalist(\"100_model\",\"".@$temp_data[$cykl]."\");' disabled=disabled >
                <input type='radio' value='".@$temp_data[$cykl]."' name=model id=radio".($cykl+1)." title='' onclick='enable_sizes(this.value,\"pict_radio".($cykl+1)."\");load_datalist(\"100_model\",\"".@$temp_data[$cykl]."\");' disabled=disabled style=display:none /> <span id=pict_radio_title".($cykl+1)." >".dictionary(@$temp_data[$cykl],$_SESSION["language"])."</span></div>";
                $cykl++;endwhile;$radiocount=$cykl;
            echo "<div class='model_line' ><span class=size_item ><select style=width:100%; id='height' name='height' onchange=fn_atypical_sizes(this.value); disabled=disabled ><option disabled=disabled>".dictionary("height",$_SESSION["language"])."</option></select></span>";
            echo "<span class=size_item ><select style=width:100%; id='width' name='width' onchange=fn_atypical_sizes(this.value); disabled=disabled ><option disabled=disabled>".dictionary("width",$_SESSION["language"])."</option></select></span>";            
            echo "<span class=size_item ><select style=width:100%; id='depth' name='depth' onchange=fn_atypical_sizes(this.value); disabled=disabled ><option disabled=disabled>".dictionary("depth",$_SESSION["language"])."</option></select></span></div>";
    
    
    
   echo"</div>";
echo "</form>";
//accessories
echo"<div class=product_submenu ><p id='linespace' ></p>".dictionary("accessories",@$_SESSION["language"])."<p id='linespace' ></p></div>";
  // body size 684x534px;
    echo"</td><td style='height:550px;margin:0px;padding:0px;'>
<iframe id=program_body type=text/html src='./ajax_functions.php?load_datalist=6&table=100_main_setting' style='top:0;left:0;right:0;bottom:0;width:100%;height:100%;z-index:100;margin:0px;padding:0px;scrolling:auto;' rows='50%, 50%' margin=0px padding=0px align=left frameborder=0 scrolling=yes noresize=noresize ></iframe></td></tr>
<tr><td style='height:150px;vertical-align:top;margin:0px;padding:0px;' >
<iframe id=demand_form type=text/html src='' style='top:0;left:0;right:0;bottom:0;width:100%;height:148px;z-index:100;margin:0px;padding:0px;scrolling:auto;' margin=0px padding=0px align=left frameborder=0 scrolling=yes noresize=noresize ></iframe>
</td></tr>";
?>

</table>
</td></tr></table>
</div>



</td></tr></table>
</body>
</html>


<?

echo "<script>menucount = '".$menucount."';radiocount='".$radiocount."';</script>";

require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/karat_catalog.js");
?>

<script type="text/javascript">
load_demand();
disabling_start_objects('demand_form','demand_reload');
window.setInterval(function(){coloring_loading_panel();},1000);
//top.location.href = "<?echo str_replace("lang_","",@$_SESSION["language"]."/");?>";


  
window.onbeforeunload = function() {
    if (reg_session===true) {script = document.createElement('script');
        script.src = './ajax_functions.php?regenerate_user_sess=YES';
        document.getElementsByTagName('head')[0].appendChild(script);
    }
  }
<?
    @$sql =  " SELECT [systemname] FROM dbo.[100_main_setting] WHERE data_type = 'show_start_list' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
if (@$row[0] == "ANO"){echo "present_list();";}?>
check_logon_status("START");
</script>

<?sqlsrv_close($conn);?>

