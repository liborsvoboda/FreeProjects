<?if (@isset($_GET['value'])) {
require_once ("./functions/php/sessions.inc");
require_once ("./config/dbconnect.php");
require_once ("./functions/php/knihovna.php");
require_once ("./config/mssql_dbconnect.php");

    $sql =  "SELECT TOP 10 * FROM ".securesql($_GET['value2'])." WHERE postup LIKE '".securesql($_GET['value1'])."%' AND platnost= '1' ORDER BY postup ASC ";
   
    $check = sqlsrv_query( $conn, $sql , $params, $options ) or die (dictionary("sql_command",$_SESSION["language"])." > ".print_r( sqlsrv_errors(), true));$fn_temp=1;$panel ="";
    
    while( $row = sqlsrv_fetch_array( $check, SQLSRV_FETCH_BOTH ) ) {
if ($fn_temp==1){
    $panel = "<fieldset id=ram><legend id=ram_legenda><b>".dictionary("selection",$_SESSION["language"])."</b></legend><select size=10 name=allvalues style=width:200px;background-color:silver;font-size:13pt; ondblclick=this_value(this); >";
}
 $panel.= "<option value=\'".str_replace(" ","",$row[$_GET['value3']])."\'>".str_replace(" ","",$row[$_GET['value3']])."</option>";

 $fn_temp=0; 
}//sqlsrv_close($conn);
    
$panel.="</select><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=\"close_sel_win();\" ><img src=\'./images/ntick.png\' ></div></fieldset>";
?>document.getElementById('<?echo $_GET['valuea'];?>').innerHTML = '<?echo $panel; ?>';

document.getElementById('<?echo $_GET['value'];?>').style.backgroundColor ='#B6EAB0';
<?program_log('','YES','sql_log');?>
function this_value(value){
    document.getElementById('<?echo $_GET['value'];?>').value = value.options[value.selectedIndex].value;
    document.getElementById('<?echo $_GET['value'];?>').style.backgroundColor ='#FFFFFF';
document.getElementById('<?echo $_GET['valuea'];?>').style.display ='none';
fn_tpv_add_info(value.options[value.selectedIndex].value);
}

function close_sel_win(){
     document.getElementById('<?echo $_GET['value'];?>').style.backgroundColor ='#FFFFFF';
document.getElementById('<?echo $_GET['valuea'];?>').style.display ='none';
}



<?}?>