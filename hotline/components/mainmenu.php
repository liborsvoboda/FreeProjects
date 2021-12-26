<?$add_load_data=mysql_query("select * from mainmenuselections where mainmenu_id='".mysql_result($loadata,$load,0)."' and ('".securesql($_SESSION['userights'])."' LIKE CONCAT('%,*',access,':%') or access='') order by position");

if (@mysql_num_rows($add_load_data)){?><div id=mainmenu<?echo mysql_result($loadata,$load,0);?> style="position:absolute;;width:180px;padding-left:5px;border: 1px dotted;font-family: tahoma;font-size: 10pt;background-color:#F0F0F0;left: <?echo $load*100+3;?>px;top:30px;text-align: left;" onmouseout="setTimeout('closemainmenu(<?echo mysql_result($loadata,$load,0);?>)', <?echo @mysql_result(mysql_query("select param from mainsettings where name='mainmenu_show_time' "),0,0);?>);" ><?}
$com_load=0;while($com_load<@mysql_num_rows($add_load_data)):

if ($com_load==0){echo"</br>";}                                                                                                                                                                                                                                                                                                                                     
 
 $rtg_fin="";$rtg_load=1;while(@$_SESSION['use_det_rights'][$rtg_load]):
    if (StrPos(" ".@$_SESSION['use_det_rights'][$rtg_load],"*".mysql_result($add_load_data,$com_load,4).":")){$rtg_fin=str_replace("*".mysql_result($add_load_data,$com_load,4).":","",@$_SESSION['use_det_rights'][$rtg_load]);}
 @$rtg_load++;endwhile;

echo "<span class=\"outmbutton\" onmouseout=\"className='outmbutton';\" onmouseover=\"className='onmbutton';\" onclick='mainmenu(10000);sesscrt(\"".code($rtg_fin)."\");".mysql_result($add_load_data,$com_load,7)."' ><img src='./ajax_functions.php?icon=yes&tbl=".code("mainmenuselections")."&id=".code(mysql_result($add_load_data,$com_load,0))."' border='0' width='24' height='24' style=vertical-align:middle;margin-bottom:2px; > ";
echo dictionary(mysql_result($add_load_data,$com_load,3),$_SESSION['language'])."</span>";
if (($com_load+1)<@mysql_num_rows($add_load_data)) {echo "<hr style='margin-left:-2px;margin-top:-5px;height:1px; '>";} else {echo"<p style=height:0px;margin:0px;font-size:2px; ></p>";}


$com_load++;endwhile;

if (@mysql_num_rows($add_load_data)){?>
<div style=position:absolute;left:160px;top:3px;cursor:pointer; onclick=document.getElementById("mainmenu<?echo mysql_result($loadata,$load,0);?>").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div>
</div>
<script>document.getElementById("mainmenu<?echo mysql_result($loadata,$load,0);?>").style.display="none";</script>
<?}?>
