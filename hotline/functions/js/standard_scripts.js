<SCRIPT LANGUAGE="JavaScript">
javascript:window.history.forward(0);

var i,st_temp,inc_object;

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
 

    function checkcolorfield(formname,fieldname,savebtn){
         if(document.forms[formname][fieldname].value != "") {
             document.getElementById(savebtn).disabled=false;
             document.forms[formname][fieldname].style.background="#FDECAA";}
    }

    function checkfield(formname,fieldname,savebtn){
         if(document.forms[formname][fieldname].value != "" || document.forms[formname][fieldname].selectedIndex !="") {document.getElementById(savebtn).disabled=false;}
         else {document.getElementById(savebtn).disabled=true;}
    }

    function checkvalue(value,value1,value2,value3) { //value - field id, value1 - id zaznamu, value2 =tabulka, value3 = cislo sloupce
    if (document.getElementById("checkvalue").style.display !='none'){document.getElementById("checkvalue").style.display ='none';document.getElementById(value).style.backgroundColor ='#FFFFFF';}
    else {document.getElementById("checkvalue").style.display ='inline';
    script = document.createElement('script');
    script.src = './checkvalue.php?value=' + value +'&value1=' + value1 +'&value2=' + value2 +'&value3=' + value3;
    document.getElementsByTagName('head')[0].appendChild(script);}
}

function activate_field(value){
var pocet=0;
if (document.getElementById(value).disabled==true) {document.getElementById(value).disabled=false;pocet=1;}
if (document.getElementById(value).disabled==false && pocet==0) {document.getElementById(value).disabled=true;}
}

function submit_field(form,value){
document.getElementById(value).value=value + "+:+" + document.getElementById(value).value;
document.getElementById(form).submit();
}


function submit_field_from(submit_form,from,hidden_obj){
    document.getElementById(hidden_obj).value=from;
    document.getElementById(submit_form).submit();
}

function select_match_value (selectObj, txtObj)
{
   for(i = 0; i < selectObj.length; i++)
   {
      if(selectObj.options[i].value == txtObj)
      selectObj.selectedIndex = i;
   }
}


function open_website (selectObj){
   var st_temp =document.getElementById(selectObj);
    for (i = 0; i < st_temp.options.length; i++) {
        if ( st_temp.options[i].selected == true ){
               window.open("http://www."+st_temp.options[i].value);
        }
    }
}        





function allow_when_selected(object_type,selected_object,allow_object,specific_values){
// multi format with "/" separator
//can be used targeting selected_object on specific value and allow some object
// all disable types = DISABLE / value=value / none_value ass disable " "
 
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




$(document).keydown(function(e) {
    var doPrevent;
    if (e.keyCode == 8) {
        var d = e.srcElement || e.target;
        if (d.tagName.toUpperCase() == 'INPUT' || d.tagName.toUpperCase() == 'TEXTAREA') {
            doPrevent = d.readOnly || d.disabled;
        }
        else
            doPrevent = true;
    }
    else
        doPrevent = false;

    if (doPrevent)
        e.preventDefault();
});



function htmlentities(input)
{
    var code="";
    for(i=0; i < input.length; i++)
    {
        code=code+"&#"+input.charCodeAt(i)+";";
    }
    return code;
}

function disable_object(value){
    document.getElementById(value).disabled=true;
}

function enable_object(value){
    document.getElementById(value).disabled=false;
}

function fn_visibility_object(value){
    if (document.getElementById(value).visible != true ){ document.getElementById(value).visible=true; }
        else {document.getElementById(value).visible=false;}    
}

function fn_visible_object(value){
    document.getElementById(value).visible=true;
}

function fn_hidden_object(value){
    document.getElementById(value).visible=false;
}


function fn_on_off_display(value){
        if (document.getElementById(value).style.display != "none" ){document.getElementById(value).style.display="none"; }
        else {document.getElementById(value).style.display="inline";}    
}


function fn_on_off_display(value){
        if (document.getElementById(value).style.display != "none" ){document.getElementById(value).style.display="none"; }
        else {document.getElementById(value).style.display="inline";}    
}


function fn_on_display(value){
    document.getElementById(value).style.display="inline";
}


function fn_off_display(value){
    document.getElementById(value).style.display="none";
}


function mssql_delete_item(value1,value2,value3){ //value1 - tabulka, value2 - id_rec ,  value3 + GET info uRL
script=document.createElement('script');
script.src='./ajax_functions.php?mssql_delete_item='+value1+'&id='+value2;
document.getElementsByTagName('head')[0].appendChild(script);
window.location.href='<?echo $_SERVER["PHP_SELF"];?>'+value3;
} 


function fn_clear_params(fn_type,fn_object){ 
    //input type (select,input,radio) , object for cleaning  fn_object,fn_object-5 -cycle 1 to 5 
fn_type = fn_type.split('|');
fn_object = fn_object.split('|');
    i=0;
    while (fn_type[i]){
       fn_cycle= fn_object[i].split('-');
       if (typeof fn_cycle[1] != 'undefined') {
        for (rep=1; rep <= fn_cycle[1]; rep++) {
            fn_object[i]=fn_cycle[0]+rep; 
                inc_object = document.getElementById(fn_object[i]);
              if (fn_type[i] ==='select'){
                try {inc_object.options[inc_object.selectedIndex].value}
                catch ( e ) {}
                inc_object.options.length=0;
              }
              if (fn_type[i] ==='input'){
                try {inc_object.value}
                catch ( e ) {}
                inc_object.value="";
              }
              if (fn_type[i] ==='radio'){
                try {inc_object.value}
                catch ( e ) {}
                inc_object.checked=false;
              }
            }} 
            else {
              inc_object = document.getElementById(fn_object[i]);
              if (fn_type[i] ==='select'){
                try {inc_object.options[inc_object.selectedIndex].value}
                catch ( e ) {}
                inc_object.options.length=0;
              }
              if (fn_type[i] ==='input'){
                try {inc_object.value}
                catch ( e ) {}
                inc_object.value="";
              }
              if (fn_type[i] ==='radio'){
                try {inc_object.value}
                catch ( e ) {}
                inc_object.checked=false;
            }
            }
      i++;
    }  
}


function fn_set_simple_inner_source(value1,value2,value3,value4){ //target,value,parent,action(visibility direct or parent)

    if (value4 == "visible"){
          
        if (value3 != ''){document.getElementById(value3).visible = true;}
            else {document.getElementById(value1).visible = true;}
          
        document.getElementById(value1).innerHTML=value2;
    } else {
        if (value3 !=''){document.getElementById(value3).visible = false;}
            else {document.getElementById(value1).visible = false;}
        document.getElementById(value1).innerHTML="";
    }
    
}

function table_line_display(value){
    if (document.getElementById(value).style.display != "none") {document.getElementById(value).style.display = "none";}    
        else {document.getElementById(value).style.display = "inline";}
}

function reload_page(){
    document.getElementById("loading").style.display ='inline';
    window.location.reload(true);
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