<?@$load_data=mysql_query("select * from units_menu where ('".securesql($_SESSION['userights'])."' LIKE CONCAT('%,*',access,':%') or access='') order by position");

?><span id=mainmenu_desc ><?echo dictionary('mainmenu',$_SESSION['language']);?></span><?
$com_load=0;while($com_load<@mysql_num_rows($load_data)):


    @$submenu=mysql_query("select * from units_submenu where id_menu='".securesql(mysql_result($load_data,$com_load,0))."' and ('".securesql($_SESSION['userights'])."' LIKE CONCAT('%,*',access,':%') or access='') order by position ASC");

    	$rtg_fin="";$rtg_load=1;while(@$_SESSION['use_det_rights'][$rtg_load]):
                 if (StrPos(" ".@$_SESSION['use_det_rights'][$rtg_load],"*".mysql_result($load_data,$com_load,3).":")){
                 	$rtg_fin=str_replace("*".mysql_result($load_data,$com_load,3).":","",@$_SESSION['use_det_rights'][$rtg_load]);}
             @$rtg_load++;endwhile;

    echo "<span class=\"units_menu1out\" onmouseout=\"className='units_menu1out';\" onmouseover=\"className='units_menu1on';\"";
    if (@!mysql_num_rows($submenu)){echo " onclick='sesscrt(\"".code($rtg_fin)."\");submenu(".mysql_result($load_data,$com_load,0).");".mysql_result($load_data,$com_load,4)."' ><img src='./ajax_functions.php?icon=yes&tbl=".code("units_menu")."&id=".code(mysql_result($load_data,$com_load,0))."' border='0' width='24' height='24' style=vertical-align:middle;margin-bottom:2px; > ";}
    else {echo " onclick=\"submenu(".mysql_result($load_data,$com_load,0).");\" ><img src='./ajax_functions.php?icon=yes&tbl=".code("units_menu")."&id=".code(mysql_result($load_data,$com_load,0))."' border='0' width='24' height='24' style=vertical-align:middle;margin-bottom:2px; >";
    // tlaciko +/- img vyse jej nahradilo
    //<input id='menu_button".mysql_result($load_data,$com_load,0)."' type='button' value=' + ' style=vertical-align:middle;> "
    }
    echo dictionary(mysql_result($load_data,$com_load,1),$_SESSION['language'])."</span>";

    //submenu
    if (@mysql_num_rows(@$submenu)){echo"<span id='submenu".mysql_result($load_data,$com_load,0)."'>";
            $sub_com_load=0;while($sub_com_load<@mysql_num_rows($submenu)):

             $rtg_fin="";$rtg_load=1;while(@$_SESSION['use_det_rights'][$rtg_load]):
                 if (StrPos(" ".@$_SESSION['use_det_rights'][$rtg_load],"*".mysql_result($submenu,$sub_com_load,4).":")){$rtg_fin=str_replace("*".mysql_result($submenu,$sub_com_load,4).":","",@$_SESSION['use_det_rights'][$rtg_load]);}
             @$rtg_load++;endwhile;


            echo "<span class=\"units_menu1out\" onmouseout=\"className='units_menu1out';\" onmouseover=\"className='units_menu1on';\" onclick='sesscrt(\"".code($rtg_fin)."\");".mysql_result($submenu,$sub_com_load,5)."' >";
            if (($sub_com_load+1)<@mysql_num_rows($submenu)){echo"<b style=margin-left:5px;>├─</b> ";} else {echo"<b style=margin-left:5px;>└─</b> ";}
            echo dictionary(@mysql_result($submenu,$sub_com_load,2),$_SESSION['language'])."</span>";
             $sub_com_load++;endwhile;
            ?></span><script type='text/javascript'>document.getElementById("submenu"+<?echo mysql_result($load_data,$com_load,0);?>).style.display="none";</script><?

    }
    // end of submenu

$com_load++;endwhile;?>

