<?
@$loadata=mysql_query("select * from mainmenu where ('".securesql($_SESSION['userights'])."' LIKE CONCAT('%,*',access,':%') or access='') order by position ASC");
$load=0;while ($load<@mysql_num_rows($loadata)):
    echo "<span class=\"outbutton\" onclick=\"mainmenu(".mysql_result($loadata,$load,0).");\" onmouseout=\"className='outbutton';\" onmouseover=\"className='onbutton';\" ><img src='./ajax_functions.php?icon=yes&tbl=".code("mainmenu")."&id=".code(mysql_result($loadata,$load,0))."' border='0' width='24' height='24' style=vertical-align:middle;>"." ".dictionary(mysql_result($loadata,$load,2),$_SESSION['language'])."</span>";    
    include('./components/mainmenu.php');
$load++;endwhile;
?>