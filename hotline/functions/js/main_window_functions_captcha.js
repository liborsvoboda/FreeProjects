<!--//
<script type="text/JavaScript">
javascript:window.history.forward(0);
//
//if (document.all){
//document.onkeydown = function (){
//if (event.keyCode == 116){ event.keyCode = ;return false;} //F5
//if (event.keyCode == 8){ event.keyCode = 27;return false;} //BackSpace
//}}
//
//
function cancelBack() {  
    if ((event.keyCode == 8 ||   
        (event.keyCode == 37 && event.altKey) ||   
        (event.keyCode == 39 && event.altKey))  
         &&   
        (event.srcElement.form == null || event.srcElement.isTextEdit == false)  
       )  
    {  
        event.cancelBubble = true;  
        event.returnValue = false;  
    }  
} 
// 
//
//function Disable() {
//if (event.button == 2)
//{
//alert("Akce je Zakázána!!")
//}}
//document.onmousedown=Disable;
</script>


 <script type="text/javascript">
function doScroll(){
//  if (window.name) window.scrollTo(0, window.name);
 }
</script>


<SCRIPT style="text/javascript">

document.write('<DIV id="loading" onselectstart="return TRUE;" style=z-index:100;><BR><?echo dictionary("please_wait",$_SESSION["language"]);?><br /><img src="images/loading.gif" border="0"></DIV>');

window.onload=function(){
    document.getElementById("loading").style.display="none";doScroll();
}



function allow_when_selected(object_type,selected_object,allow_object,specific_values){
// multi format with "/" separator
//can be used targeting selected_object on specific value and allow some object
// all disable = DISABLE / value=value / none_value ass disable " "
 
object_type = object_type.split('|');
selected_object = selected_object.split('|');

if (specific_values != 'DISABLE'){specific_values = specific_values.split('|');}

    i=0;document.getElementById(allow_object).disabled=false;
    while (object_type[i]){
        inc_object = document.getElementById(selected_object[i]);

      if (object_type[i] ==='select'){
        try {inc_object.options[inc_object.selectedIndex].value}
        catch ( e ) {if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                        document.getElementById(allow_object).disabled=true;alert("block");}}
            if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                if (specific_values[i] === inc_object.options[inc_object.selectedIndex].value ){document.getElementById(allow_object).disabled=true;}
            }
      }
      
      if (object_type[i] ==='input'){
        try {inc_object.value}
        catch ( e ) {if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                        document.getElementById(allow_object).disabled=true;}}
        if (inc_object.value === '' || inc_object.value === ' '){document.getElementById(allow_object).disabled=true;}
            if (specific_values != 'DISABLE' && specific_values[i] != 'DISABLE'){
                if ( specific_values[i] === inc_object.value ){document.getElementById(allow_object).disabled=true;}
            }
      }

      i++;
    }  
}



document.write('<DIV id=login style=z-index:10000;color:#000080;background:silver;><form action="'+document.URL+'" method=post enctype="multipart/form-data"><table border=0 cellpading=0 cellspacing=0 style=color:#000080;><tr><td colspan=2 align=center><b><?echo dictionary("userlogin",$_SESSION["language"]);?></b></td></tr><tr><td><?echo dictionary("username",$_SESSION["language"]);?></td><td><input name="user" type="text" value="" style=width:180px;text-align:center;></td></tr><tr><td><?echo dictionary("password",$_SESSION["language"]);?></td><td><input name="password" type="password" value="" style=width:180px;text-align:center;></td></tr><tr><td></td><td><img id="siimage" align=left style="vertical-align:top;padding-right:0px;" border=0 src="./modules/logcaptcha/securimage_show.php?sid=<?php echo md5(time());?>"/><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="24" height="24" id="SecurImage_as3" align="middle"><param name="allowScriptAccess" value="sameDomain" /><param name="allowFullScreen" value="false" /><param name="movie" value="./modules/logcaptcha/securimage_play.swf?audio=./modules/logcaptcha/securimage_play.php?&bgColor1=#777&bgColor2=#fff&iconColor=#000&roundedCorner=5" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" /><embed src="./modules/logcaptcha/securimage_play.swf?audio=./modules/logcaptcha/securimage_play.php?&bgColor1=#777&bgColor2=#fff&iconColor=#000&roundedCorner=5" quality="high" bgcolor="#ffffff" width="24" height="24" name="SecurImage_as3" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" /></object></td></tr><tr><td></td><td><input type="text" name="code" style="width:180px;vertical-align:top;text-align:center;color:black;resize:none;background:#DD4448" value="<?echo dictionary("rewritecode",$_SESSION["language"]);?>" onClick=select() autocomplete="off" /></td></tr><tr><td></td><td align=right><input type="submit" value="<?echo dictionary("login",$_SESSION["language"]);?>"></td></tr></table></form><div style=position:absolute;right:5px;top:5px;cursor:pointer; onclick=document.getElementById("login").style.display="none"; ><img src="./images/close.png" border="0" width="12" height="12" alt="<?echo dictionary("close",$_SESSION["language"]);?>"></div></DIV>');
document.getElementById("login").style.display="none";


function login(){
var pocet=0;
if (document.getElementById("login").style.display!="none") {pocet=1;document.getElementById("login").style.display="none";}
if (document.getElementById("login").style.display=="none" && pocet==0) {document.getElementById("login").style.display="inline";}
}

function logout(){
    parent.document.getElementById("sesswindow").src ='./functions/php/unset.inc';
}


function mainmenu(value){
var pocet=0;
      for (i = 1; i < 1000; i++) {
                if (document.getElementById("mainmenu"+i) != null && i!= value) {
                                document.getElementById("mainmenu"+i).style.display="none";}
        }
        if (document.getElementById("mainmenu"+value) != null){
            if (document.getElementById("mainmenu"+value).style.display!="none") {pocet=1;document.getElementById("mainmenu"+value).style.display="none";}
            if (document.getElementById("mainmenu"+value).style.display=="none" && pocet==0) {document.getElementById("mainmenu"+value).style.display="inline";}
        }
}


function closemainmenu(value){
  if (document.getElementById("mainmenu"+value) != null){
        document.getElementById("mainmenu"+value).style.display="none";
  }
}


function submenu(value){
var pocet=0;
      for (i = 1; i < 1000; i++) {
                if (document.getElementById("submenu"+i) != null && i!= value) {
                                document.getElementById("submenu"+i).style.display="none";
                }
                if (document.getElementById("menu_button"+i) != null && i!= value) {
                                //document.getElementById("menu_button"+i).value=" + ";
                }
        }
        if (document.getElementById("submenu"+value) != null){
            if (document.getElementById("submenu"+value).style.display!="none") {pocet=1;document.getElementById("submenu"+value).style.display="none";//document.getElementById("menu_button"+value).value=" + ";
            }
            if (document.getElementById("submenu"+value).style.display=="none" && pocet==0) {document.getElementById("submenu"+value).style.display="inline";//document.getElementById("menu_button"+value).value=" – ";
            }
        }
}



function subprogram (value,value1) {
              
    if (value){   
       if ("<?echo @$viewer;?>"=="ie") {document.frames("program_frame").location.href=value;}
        else {document.getElementById('program_frame').contentWindow.location.href=value;}                     
        document.getElementById("openned_app").innerHTML =value1;
        document.getElementById("loading").style.display="inline";doScroll();
    }    
}

function main_menu_frame (value,value1) {
    if (value){   
       if ("<?echo @$viewer;?>"=="ie") {document.frames("program_frame").location.href=value;}
        else {document.getElementById('program_frame').contentWindow.location.href=value;}
        document.getElementById("openned_app").innerHTML =value1;
        document.getElementById("loading").style.display="inline";doScroll();
    }
}

document.write('<iframe id="help_frame" style=z-index:100;></iframe>');
document.getElementById("help_frame").style.display="none";

function help(value){
document.getElementById("help_frame").style.display="inline";
document.getElementById("help_frame").src ='./help.php?help='+value;
}

document.write('<iframe id="sesswindow" style=position:absolute;right:50px;z-index:100;></iframe>');
document.getElementById("sesswindow").style.display="none";
function sesscrt(value){
parent.document.getElementById("sesswindow").src ='./functions/php/access.inc.php?RTG='+value;
}

function activate_field(value){
var pocet=0;
if (document.getElementById(value).disabled==true) {document.getElementById(value).disabled=false;pocet=1;}
if (document.getElementById(value).disabled==false && pocet==0) {document.getElementById(value).disabled=true;}
}

function get_object_size(value){
    var a = document.getElementById(value).clientHeight;
    var b = document.getElementById(value).clientWidth;
return(a,b);
}


</script>

<script type="text/JavaScript">
 var cDOW=["PO "," ÚT"," ST"," ČT"," PÁ"," SO"," NE"];var cMOY=["Leden","Únor","Březen","Duben","Květen","Červen","Červenec","Srpen","Září","Říjen","Listopad","Prosinec"];var imgPath="";
 function calendar(cTarget,cName,cId) {this.cId=cId;this.cTarget=cTarget;this.cName=cName;this.cDate=new Date();this.cYear=this.cDate.getFullYear();this.cMonth=this.cDate.getMonth();this.cDay=1;if (document.getElementById(cName).innerHTML =="" || document.getElementById(cName).innerHTML =="</DIV>") {show_calendar(this);} else {document.getElementById(cName).innerHTML="";}}
 function show_calendar(cId) {var cData="";cData+="<DIV CLASS=\"calendar\">\n";cData+=" <FIELDSET style=text-align:left>\n";cData+="  <LEGEND><?echo dictionary("date",@$_SESSION["language"]);?>&nbsp;</LEGEND>\n";cData+="  <DIV STYLE=\"position: relative;\">\n";cData+="   <SELECT NAME=\""+cId.cName+".cMonth\" onChange=\"setNMonth(this.options[selectedIndex].value,"+cId.cId+");\">"; for (var idx_month=0;idx_month<12;++idx_month) cData+="   <OPTION VALUE=\""+idx_month+"\">"+cMOY[idx_month]+"\n"; cData+="   </SELECT>\n";
  cData+="   <INPUT TYPE=\"text\" NAME=\""+cId.cName+".cYear\" STYLE=\"width: 34px;\" onChange=\"setNYear("+cId.cId+");\"'> <IMG SRC=\""+imgPath+'images/'+"inc.png\" STYLE=\"position: absolute; top: 2px;\" onMouseOver=\"this.src='"+imgPath+'images/'+"inc_over.png';\" onMouseOut=\"this.src='"+imgPath+'images/'+"inc.png';\" onClick=\"++window.document.getElementById('"+cId.cName+".cYear').value; setNYear("+cId.cId+");\"> <IMG SRC=\""+imgPath+'images/'+"dec.png\" STYLE=\"position: absolute; top: 11px;\" onMouseOver=\"this.src='"+imgPath+'images/'+"dec_over.png';\" onMouseOut=\"this.src='"+imgPath+'images/'+"dec.png';\" onClick=\"--window.document.getElementById('"+cId.cName+".cYear').value; setNYear("+cId.cId+");\">\n";
  cData+="  </DIV>\n"; cData+="  <DIV CLASS=\"calendar_table\">\n";cData+="  <DIV CLASS=\"calendar_row_cDOW\">";for (var idx_day=0;idx_day<7;++idx_day) cData+="<SPAN STYLE=\"width: 20px\">"+cDOW[idx_day]+"</SPAN>";cData+="  </DIV>\n";cData+="  <DIV ID=\""+cId.cName+".cData\">";cData+="  </DIV>\n";cData+=" </FIELDSET>\n";cData+="</DIV>\n";window.document.getElementById(cId.cName).innerHTML=cData;setCalendar(new Date(cId.cYear,cId.cMonth,1),cId)}
 function setCalendar(dt,cId) { cId.cYear=dt.getFullYear(); cId.cMonth=dt.getMonth(); cId.cDay=dt.getDate(); firstDay=dt.getDay();if ((firstDay-2)<-1) firstDay+=7;dayspermonth=getDaysPerMonth(cId); cData=""; for (var row=0;row<6;++row) {cData+="  <DIV>"; for (var col=1;col<8;++col) {nDay=row*7+col-firstDay+1; cData+="<A HREF=\"\" STYLE=\"width: 20px\" onClick=\"if (this.innerHTML!=='') ShowDate('"+nDay+"',"+cId.cId+"); return false;\">";
 if ((nDay>0)&&(nDay<dayspermonth+1)) cData+=nDay;cData+="   ";cData+="</A>";cData+="   ";} cData+="</DIV>\n";}window.document.getElementById(cId.cName+".cData").innerHTML=cData;window.document.getElementById(cId.cName+".cMonth").value=cId.cMonth;window.document.getElementById(cId.cName+".cYear").value=cId.cYear;}
 function getDaysPerMonth(cId){daysArray=new Array(31,28,31,30,31,30,31,31,30,31,30,31);days=daysArray[cId.cMonth];if (cId.cMonth==1){if((cId.cYear%4)==0) {if(((cId.cYear%100)==0) && (cId.cYear%400)!=0)days = 28; else  days = 29;}}return days;}function setNMonth(cMonth,cId){setCalendar(new Date(cId.cYear,cMonth,1),cId);}
 function setNYear(cId){cYear=parseInt(window.document.getElementById(cId.cName+".cYear").value);if (isNaN(cYear)){alert("Rok musí být číslo");return;}setCalendar(new Date(cYear,cId.cMonth,1),cId);}
 function ShowDate(cDay,cId) {cId.cTarget.value=((cDay<10)?"0"+cDay:cDay)+"."+((cId.cMonth<9)?"0"+(cId.cMonth+1):(cId.cMonth+1))+"."+cId.cYear;window.document.getElementById(cId.cName).innerHTML="";}
</SCRIPT><STYLE TYPE="text/css"><!-- .calendar {width: 160px;background: #9DBFF2;color: #000000;font-family: "Arial CE",Arial;font-size: 12px;} .calendar a {text-decoration: none;background: #B7DCF2;color: #000000;} .calendar a:hover {Xbackground: #0054E3;Xcolor: #FFFFFF;} .calendar input {font-family: "Arial CE",Arial;font-size: 12px;} .calendar select {font-family: "Arial CE",Arial;font-size: 12px;} .calendar_table {background: #B7DCF2;color: #000000;border: 1px solid #ACA899;text-align: center;} .calendar_row_cDOW {background: #7A96DF;color: #FFFFFF;} .calendar_day_of_month {background: #0054E3;color: #FFFFFF;cursor: pointer;}--></STYLE>