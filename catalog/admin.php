<?php
require_once ('./config/main_variables.php');
require_once ("./functions/php/sessions.inc.php");
require_once ("./config/mssql_dbconnect.php");
require_once ("./functions/php/knihovna.php");
require_once './modules/ckeditor/ckeditor/ckeditor.php';
require_once './modules/ckeditor/ckfinder/ckfinder.php';
//utf-8 / windows-1251
?>

<html>
<head>
<link rel="icon" href="http://127.0.0.1/HotLine/modules/catalog/config/company.ico" type="image/x-icon" />
<link rel="shortcut icon" href="http://127.0.0.1/HotLine/modules/catalog/config/company.ico" type="image/x-icon" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href='./css/catalog.css' />

<link rel="stylesheet" href="./modules/ckeditor/shadowbox/shadowbox.css" type="text/css" media="screen" />
<script src="./modules/ckeditor/shadowbox/shadowbox.js" type="text/javascript"></script>
<script type='text/javascript' src='./modules/ckeditor/ckeditor/ckeditor.js'></script>
<script type='text/javascript' src='./modules/ckeditor/ckfinder/ckfinder.js'></script>
<script type="text/javascript">
Shadowbox.init({
    modal: true,
    displayNav:         true,
    autoplayMovies:     true
});
</script>
<?
if (@$_POST["user"] and @$_POST["password"] ) {require_once "./login/login.php";}
//if (!@$_SESSION['lnamed'] && !@$_REQUEST["user"] ){require_once('./functions/php/unset.inc.php');} 
?>

<? //saving
if (isset($_POST["in_btn1"]) && !isset($_POST["in_btn2"])){ // save new record

@$file_name= @$_FILES['in_file1']['name'];@$file_temp = @$_FILES['in_file1']['tmp_name'];@$file_mime = @$_FILES['in_file1']['type'];
if (@$file_mime=="image/pjpeg"){@$file_mime='image/jpeg';}@$file_full = implode('', file("$file_temp"));

if ($_POST[sel_value1]<>"100_dictionary"){
    $sql_insert = "INSERT INTO dbo.[".mssecuresql($_POST[sel_value1])."] (data_type,systemname,create_date,creator,sequence,parent_data_type,system_function)VALUES('".mssecuresql($_POST["in_value1"])."','".mssecuresql($_POST["in_value2"])."','".$dnest."','".mssecuresql(@$_SESSION["lnamed"])."','".mssecuresql($_POST["in_value4"])."','".mssecuresql($_POST["in_value5"])."','".mssecuresql($_POST["in_value8"])."') ";
    }
    else{

    // add language column
    if (@StrPos (" " . $_POST["in_value1"], "lang_")){
        // vyjimky
        $except = "'100_customer_visit','100_login'";
        @$sql = "SELECT * FROM information_schema.tables WHERE TABLE_NAME like '100_%' AND TABLE_NAME NOT IN (".$except.") ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            $temp=explode("_", mssecuresql($_POST["in_value1"]));
             $new_sql = "ALTER TABLE dbo.[".mssecuresql($row[2])."] ADD ckeditor_".mssecuresql($temp[1])." text ";

             //russian special collation
             if ($temp[1]=="ru"){$new_sql .= " COLLATE CYRILLIC_GENERAL_CI_AS ";}
             $new_sql .= " NULL ";
             
 
        if ($_POST["in_value1"] == "lang_ru" ){
            $new_lang_sql = "ALTER TABLE dbo.[".mssecuresql($row[2])."] ADD ".mssecuresql($_POST["in_value1"])." text COLLATE CYRILLIC_GENERAL_CI_AS NULL";
            }else {$new_lang_sql = "ALTER TABLE dbo.[".mssecuresql($row[2])."] ADD ".mssecuresql($_POST["in_value1"])." text NULL";
            }
             $control = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql($row[2])."' AND (COLUMN_NAME = 'ckeditor_".mssecuresql($temp[1])."' OR COLUMN_NAME = '".mssecuresql($_POST["in_value1"])."' ) ";
                @$exist_check = sqlsrv_query( $conn, $control , $params, $options );
                if (!sqlsrv_num_rows($exist_check)){              
                    if ($row[2]<>"100_dictionary"){@$insert_check = sqlsrv_query( $conn, $new_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));}
                        else {@$insert_check = sqlsrv_query( $conn, $new_lang_sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));}
            } else {
             message($_POST["in_value1"]." ".dictionary("exist",$_SESSION["language"]));
            }
        } 
    }
    // end of add language column
        
        
        $fields="";$fields_data="";$count=3;
        @$sql =  "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql($_POST[sel_value1])."' AND COLUMN_NAME like 'lang[_]%' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            
             //russian special collation
            $fields.=",".$row[0];
            if ($row[0] == "lang_ru" ){
                $fields_data.=",N'".mssecuresql(@$_POST["in_value".$count])."'";
            }else{
                $fields_data.=",'".mssecuresql(@$_POST["in_value".$count])."'";
            }
            $count++;
        }
    $sql_insert = "INSERT INTO dbo.[".mssecuresql($_POST[sel_value1])."] (systemname".$fields.")VALUES('".mssecuresql($_POST["in_value1"])."'".$fields_data.") ";
    }
    
    $sql_insert .= "; SELECT SCOPE_IDENTITY() AS IDENTITY_COLUMN_NAME"; 
    $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));

        // picture update
if (@$file_name){
        $tsql = "UPDATE dbo.[".mssecuresql($_POST[sel_value1])."] SET icon =(?), mime_type='".mssecuresql($file_mime)."' WHERE id='".mssecuresql(lastInsertId($sql_ins_res))."';";
            $fileStream = fopen(@$_FILES['in_file1']['tmp_name'], "r");
                    $uploadPic = sqlsrv_prepare($conn, $tsql, array(
                               array(&$fileStream, 
                                     SQLSRV_PARAM_IN, 
                                     SQLSRV_PHPTYPE_STREAM(SQLSRV_ENC_BINARY),
                                     SQLSRV_SQLTYPE_VARBINARY('max'))));
                    if( $uploadPic === false )
        die( FormatErrors( sqlsrv_errors() ) );
                    if( sqlsrv_execute($uploadPic) === false )
        die( FormatErrors( sqlsrv_errors() ) );
}        
        // end of picture update
fn_unset_var('in_value',6);
}



if (isset($_POST["in_btn2"])){ // save edited record
 
@$file_name= @$_FILES['in_file1']['name'];@$file_temp = @$_FILES['in_file1']['tmp_name'];@$file_mime = @$_FILES['in_file1']['type'];
if (@$file_mime=="image/pjpeg"){@$file_mime='image/jpeg';}@$file_full = implode('', file("$file_temp"));

if ($_POST[sel_value1]<>"100_dictionary"){$sql_insert = " UPDATE dbo.[".mssecuresql($_POST[sel_value1])."] SET data_type='".mssecuresql($_POST["in_value1"])."', systemname='".mssecuresql($_POST["in_value2"])."' , sequence ='".mssecuresql($_POST["in_value4"])."', parent_data_type ='".mssecuresql($_POST["in_value5"])."', system_function = '".mssecuresql($_POST["in_value8"])."'  where id = '".mssecuresql($_POST["in_value3"])."' ";
    //program_log($sql_insert,'DELETE','sql.log');
    }
    else{$fields="";$fields_data="";$count=3;
        @$sql =  "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '".mssecuresql($_POST[sel_value1])."' AND COLUMN_NAME like 'lang[_]%' ";
        @$check = sqlsrv_query( $conn, $sql , $params, $options );
        while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
            
            //russian special collation
            if ($row[0] == "lang_ru" ){
                $fields.=",".$row[0]."=N'".mssecuresql(@$_POST["in_value".$count])."'";
            }else{
                $fields.=",".$row[0]."='".mssecuresql(@$_POST["in_value".$count])."'";
            }
            $count++;
        }
        $sql_insert = " UPDATE dbo.[".mssecuresql($_POST[sel_value1])."] SET systemname='".mssecuresql($_POST["in_value1"])."' ".$fields." where id = '".mssecuresql($_POST["in_value2"])."' ";
            $_POST["in_value3"]=$_POST["in_value2"];    
    }
    
    $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    //program_log($sql_insert,'DELETE','sql.log');

        // picture update
if (@$file_name){
        $tsql = "UPDATE dbo.[".mssecuresql($_POST[sel_value1])."] SET icon =(?), mime_type='".mssecuresql($file_mime)."' WHERE id='".mssecuresql($_POST["in_value3"])."';";
            $fileStream = fopen(@$_FILES['in_file1']['tmp_name'], "r");
                    $uploadPic = sqlsrv_prepare($conn, $tsql, array(
                               array(&$fileStream, 
                                     SQLSRV_PARAM_IN, 
                                     SQLSRV_PHPTYPE_STREAM(SQLSRV_ENC_BINARY),
                                     SQLSRV_SQLTYPE_VARBINARY('max'))));
                    if( $uploadPic === false )
        die( FormatErrors( sqlsrv_errors() ) );
                    if( sqlsrv_execute($uploadPic) === false )
        die( FormatErrors( sqlsrv_errors() ) );
}        
        // end of picture update
    fn_unset_var('in_value',6);
}


if (isset($_REQUEST["in_btn3"])){ // save datalist
    $temp = explode("*:*",$_POST["target"]);$temp_lang=explode("_",$temp[2]); 
     $sql_insert = " UPDATE dbo.[".mssecuresql($temp[0])."] SET ckeditor_".mssecuresql($temp_lang[1])." = ";
     
             //russian special collation
             if ($temp_lang[1]=="ru"){$sql_insert .= "N";}
     
     $sql_insert .= "'".mssecuresql(stripslashes($_POST["editor1"]))."' WHERE id = '".mssecuresql($temp[1])."' ";
        $sql_ins_res = sqlsrv_query( $conn, $sql_insert , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));

@$_REQUEST["sel_value1"]=$temp[0];
$open_loaded_record="<script>edit_record('".$temp[0]."','".$temp[1]."','','');</script>";
unset($_REQUEST["in_btn3"]);
fn_unset_var('in_value',6);
}


// end of saving

    if (@$_SESSION['lnamed']<>""){?><script type="text/JavaScript"><?
            @$sql = "SELECT (name+' '+surname) FROM dbo.[100_login] WHERE login_name = '".mssecuresql(@$_SESSION['lnamed'])."' ";
            //program_log(@$sql,"yes",'sql.log');
            @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
            @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
    echo 'window.status="'.dictionary("logged",$_SESSION["language"]).': '.@$row[0].' / '.@$_SESSION["lnamed"].'";';
    ?></script><?}?>

</head>
<?@$sql =  "SELECT systemname FROM dbo.[100_main_setting] where data_type = 'body_style' ";
@$check = sqlsrv_query( $conn, $sql , $params, $options );@$style = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH );
?>
<body id="body" style="<?echo $style[0];?>" >
<?if (@$_SESSION['lnamed']<>""){?>
<table id='fullframetable' onselectstart="return true;" >
<tr style='width:100%;border:0px;padding:0px;cellpadding:0px;cellspacing:0px;' >
<td style='cellpadding:0px;cellspacing:0px;border:0px;padding:0px;margin:0px;' >

<div id='bookmark' >
<table style='width:100%;height:100%;border:0px;cellpadding:0px;text-align:center;overflow:hidden;background-color:silver;' >
<form id='form1' action='<?echo $_SERVER["PHP_SELF"];?>' method='post' enctype="multipart/form-data">
<tr style='width:100%;height:100%;'><td style='width:100%;height:100%;' >

<table class="catalog"><td style="width:30%;vertical-align:top;">
<?
echo "<table><tr><td style=width:50%; >".dictionary("selection",$_SESSION["language"])."</td><td style=width:50%; >";

    @$bind_table="";
    @$sql =  " SELECT * FROM dbo.[100_data_types] order by id ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    echo "<select name=sel_value1 onchange='fn_data_area(this.options[this.selectedIndex].value,\"data_area\",this.options[this.selectedIndex].innerHTML,this.options[this.selectedIndex].parent,this.options[this.selectedIndex].system_function);' ><option></option>";
    while( @$main_row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "<option value='".$main_row[2]."' parent='".$main_row[5]."' system_function='".$main_row[8]."' ";
            if (@$_REQUEST["sel_value1"]==$main_row[2]){echo " selected=selected ";}
        echo ">".dictionary($main_row[1],$_SESSION["language"])."</option>";
    }
    
echo "</select></td></tr>";


if (isset($_GET["sel_value1"]) && isset($_GET["edit"])){
    @$sql =" SELECT * FROM dbo.[".$_GET["sel_value1"]."] where id = '".mssecuresql($_GET["edit"])."' ";
    @$check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));
    while( @$row = sqlsrv_fetch_array( @$check, SQLSRV_FETCH_BOTH ) ) {
        echo "<tr><td colspan=2 ><table id=in_record >";
        echo "<tr><td colspan=2 style=text-align:center;font-weight:bold; ><p id='linespace' ></p><p id='linespace' ></p>".dictionary("editing",$_SESSION["language"])." <img src=./images/close.png onclick=\"window.location.href('./admin.php');\" style=cursor:pointer; ></td></tr>";
        echo "<input type=hidden name=in_value3 value='".$row[0]."' > ";
        echo "<tr><td>".dictionary("sequence",$_SESSION["language"])."</td><td><input type=text name=in_value4 value='".$row[5]."' style=background-color:#CEF3C2;text-align:center; onkeyup=document.getElementById('in_btn2').disabled=false /></td></tr>";
        echo "<tr><td><span id=data_type_name ></span></td><td><input type=text name=in_value1 value='".$row[1]."' style=background-color:#CEF3C2;text-align:center; onkeyup=document.getElementById('in_btn2').disabled=false /></td></tr>";
        echo "<tr><td>".dictionary("systemname",$_SESSION["language"])."</td><td><input type=text name=in_value2 value='".$row[2]."' style=background-color:#CEF3C2;text-align:center;  onkeyup=document.getElementById('in_btn2').disabled=false /></td></tr>";
        echo "<tr><td>".dictionary("bind",$_SESSION["language"])."</td><td><select id=in_value5 name=in_value5 style=width:100%; disabled ></select></td></tr>";
        echo "<tr><td><table style=width:100%; ><tr><td>".dictionary("image",$_SESSION["language"])."</td><td style=text-align:right;>";
            if (@$row[8]){echo "<img src=./ajax_functions.php?icon=YES&tbl=".code($_GET["sel_value1"])."&id=".code($row[0])." style=height:16px;border:0px;cursor:pointer; onclick=fn_image_del('".$row[0]."','".$row[1]."','".$_GET["sel_value1"]."'); >";}
        echo "</td></tr></table></td><td><input type=file id=in_file1 name=in_file1 style=width:100%; ></td></tr>";

        echo "<tr><td>".dictionary("system_function",$_SESSION["language"])."</td><td><select id=in_value8 name=in_value8 style=width:100%; disabled ><option value='0' >".dictionary("no",$_SESSION["language"])."</option><option value='1' >".dictionary("yes",$_SESSION["language"])."</option></select></td></tr>";
        echo "<tr><td>".dictionary("datalist",$_SESSION["language"])."</td><td><input id=btn_datalist onclick=show_datalist(\"".$_SESSION["language"]."\"); type=button value='".dictionary("open",$_SESSION["language"])."' /></td></tr>";
        echo "<tr><td colspan=2 style=text-align:right ><input type=submit id=in_btn2 name=in_btn2 value='".dictionary("save",$_SESSION["language"])."' disabled /></td></tr>";
        echo "</table></td></tr>";
    }
    
    
    
} else {echo "<tr><td colspan=2 ><table id=in_record >";
            echo "<tr><td colspan=2 style=text-align:center;font-weight:bold; ><p id='linespace' ></p><p id='linespace' ></p>".dictionary("add_new",$_SESSION["language"])."</td></tr>";
            echo "<tr><td>".dictionary("sequence",$_SESSION["language"])."</td><td><input type=text name=in_value4 value='' style=text-align:center; onkeyup=document.getElementById('in_btn1').disabled=false /></td></tr>";
            echo "<tr><td><span id=data_type_name ></span></td><td><input type=text name=in_value1 value='' style=text-align:center; onkeyup=document.getElementById('in_btn1').disabled=false /></td></tr>";
            echo "<tr><td>".dictionary("systemname",$_SESSION["language"])."</td><td><input type=text name=in_value2 value='' style=text-align:center; onkeyup=document.getElementById('in_btn1').disabled=false /></td></tr>";
            echo "<tr><td>".dictionary("bind",$_SESSION["language"])."</td><td><select id=in_value5 name=in_value5 style=width:100%; disabled ></select></td></tr>";
            echo "<tr><td>".dictionary("image",$_SESSION["language"])."</td><td><input type=file id=in_file1 name=in_file1 style=width:100%; ></td></tr>";
            echo "<tr><td>".dictionary("system_function",$_SESSION["language"])."</td><td><select id=in_value8 name=in_value8 style=width:100%; ><option value='0' >".dictionary("no",$_SESSION["language"])."</option><option value='1' >".dictionary("yes",$_SESSION["language"])."</option></select></td></tr>";
            echo "<tr><td>".dictionary("datalist",$_SESSION["language"])."</td><td><input id=btn_datalist onclick=show_datalist(\"".$_SESSION["language"]."\"); type=button value='".dictionary("open",$_SESSION["language"])."' disabled=disabled /></td></tr>";
            echo "<tr><td colspan=2 style=text-align:right ><input type=submit id=in_btn1 name=in_btn1 value='".dictionary("save",$_SESSION["language"])."' disabled /></td></tr>";
        echo "</table></td></tr>";
         
}
        

?>
</table></td>

<td style="width:70%;"><table style='width:100%;height:100%;' border=2 frame=border rules=all ><tr>
<td><div id="data_area" style="width:700px;height:100%;overflow-x:auto;overflow-y:auto;" ></div></td>
</tr></table>
</td></tr></table>

<div style="position:relative;text-align:left;width:100%;">
<input type="button" value="<?echo dictionary("login",$_SESSION["language"]);?>" onclick="login();" >
<input type="button" value="<?echo dictionary("logout",$_SESSION["language"]);?>" onclick="logout();" >
<input type="button" value="<?echo dictionary("visit_graph",$_SESSION["language"]);?>" onclick="parent.document.getElementById('sel_value1').value='';visits_graph();fn_data_area('','','','','');" >
<input type="button" value="<?echo dictionary("control_form",$_SESSION["language"]);?>" onclick="window.open('./check_settings.php');" >
<input type="button" value="<?echo dictionary("create_product_search_index",$_SESSION["language"]);?>" onclick="parent.document.getElementById('sel_value1').value='';update_search_index();fn_data_area('','','','','');" >
<input type="button" value="<?echo dictionary("client_administration",$_SESSION["language"]);?>" onclick="parent.document.getElementById('sel_value1').value='';user_administration();fn_data_area('','','','','');" >
<input type="button" value="<?echo dictionary("demand_list",$_SESSION["language"]);?>" onclick="parent.document.getElementById('sel_value1').value='';fn_demand_list();fn_data_area('','','','','');" >
</div>

</td></tr>
</form>
</table>
</div>

</td></tr></table><?}?>

</body>
</html>

<?
require_once ("./functions/js/keystrokes.js");
require_once ("./functions/js/program_frame_drag.js");
require_once ("./functions/js/main_window_functions.js");
require_once ("./functions/js/standard_scripts.js");
require_once ("./functions/js/karat_catalog_setting.js");

if (@$_SESSION['lnamed']=="") {?><script type="text/JavaScript">login();</script><?}

if (isset($_POST["in_btn1"]) or isset($_POST["in_btn2"]) or isset($_REQUEST["sel_value1"])){ // load same data_list
    echo"<script>var sel_val=document.getElementById('sel_value1');
    fn_data_area('".$_REQUEST[sel_value1]."','data_area',sel_val.options[sel_val.selectedIndex].innerHTML,sel_val.options[sel_val.selectedIndex].parent,sel_val.options[sel_val.selectedIndex].system_function);
    </script>";
}
if (@$open_loaded_record){echo $open_loaded_record;}

@sqlsrv_close($conn);
?>
<script>window.setInterval(function(){coloring_loading_panel();},1000);</script>


