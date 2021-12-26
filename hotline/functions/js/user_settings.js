<SCRIPT LANGUAGE="JavaScript">

var IW = IW || {};


(function(IW) {

    var secondsInADay = 86400;

    function PasswordValidator() {
    }


    PasswordValidator.prototype.passwordLifeTimeInDays = 365;


    PasswordValidator.prototype.passwordAttemptsPerSecond = 500;


    PasswordValidator.prototype.expressions = [
        {
            regex : /[A-Z]+/,
            uniqueChars : 26
        },
        {
            regex : /[a-z]+/,
            uniqueChars : 26
        },
        {
            regex : /[0-9]+/,
            uniqueChars : 10
        },
        {
            regex : /[!\?.;,\\@$L#*()%~<>{}\[\]]+/,
            uniqueChars : 17
        }
    ];

    PasswordValidator.prototype.checkPassword = function(password) {

        var
                expressions = this.expressions,
                i,
                l = expressions.length,
                expression,
                possibilitiesPerLetterInPassword = 0;

        for (i = 0; i < l; i++) {

            expression = expressions[i];

            if (expression.regex.exec(password)) {
                possibilitiesPerLetterInPassword += expression.uniqueChars;
            }

        }

        var
                totalCombinations = Math.pow(possibilitiesPerLetterInPassword,password.length),
            // how long, on average, it would take to crack this (@ 200 attempts per second)
                crackTime = ((totalCombinations / this.passwordAttemptsPerSecond) / 2) / secondsInADay,
            // how close is the time to the projected time?
                percentage = crackTime / this.passwordLifeTimeInDays;

        return Math.min(Math.max(password.length * 5, percentage * 100), 100);

    };

    IW.PasswordValidator = new PasswordValidator();

})(IW);


(function(IW, jQuery) {

    function updatePassword() {

        var
                percentage = IW.PasswordValidator.checkPassword(this.val()),
                progressBar = this.parent().find(".passwordStrengthBar div");

        progressBar
                .removeClass("strong medium weak useless")
                .stop()
                .animate({"width": percentage + "%"});

        if (percentage > 90) {
            progressBar.addClass("strong");
        } else if (percentage > 50) {
            progressBar.addClass("medium")
        } else if (percentage > 10) {
            progressBar.addClass("weak");
        } else {
            progressBar.addClass("useless");
        }
    }

    jQuery.fn.passwordValidate = function() {

        this
                .bind('keyup', jQuery.proxy(updatePassword, this))
                .after("<div class='passwordStrengthBar' title='<?echo dictionary('password_security',$_SESSION['language'])?>'>" +
                "<div></div>" +
                "</div>");

        updatePassword.apply(this);

        return this; // for chaining

    }

})(IW, jQuery);


jQuery("input[id='lpassword']").passwordValidate();





    function checkPass(){
      var lpassword = document.getElementById('lpassword');
      var lpassword1 = document.getElementById('lpassword1');
      var message = document.getElementById('confirmMessage');
      var goodColor = "#66cc66";
      var badColor = "#FFB0B0";
      var message = document.getElementById('confirmMessageImg');                               
      if(lpassword.value == lpassword1.value){
        lpassword.style.backgroundColor = goodColor;
        lpassword1.style.backgroundColor = goodColor;
        document.getElementById("savebtn1").disabled=false;
        form1.form.disabled =false;
        message.innerHTML = '<img src="./images/tick.png" alt="<?echo dictionary('identical_passwords',$_SESSION['language'])?>" title="<?echo dictionary('identical_passwords',$_SESSION['language'])?>">';
      }else{
        document.getElementById("savebtn1").disabled=true;
        form1.form.disabled =true;
        lpassword1.style.backgroundColor = badColor;
        message.innerHTML = '<img src="./images/ntick.png" alt="<?echo dictionary('non_identical_passwords',$_SESSION['language'])?>" title="<?echo dictionary('non_identical_passwords',$_SESSION['language'])?>">';
      }
    }


    function checkfield(){                      
     if(form2.value1.value != "" && form2.value2.value != ""){
     document.getElementById("savebtn2").disabled=false;} 
        else {document.getElementById("savebtn2").disabled=true;} 
    }

    function cleanrights(value){
        for (i = 0; i < value; i++) {document.getElementById("right"+i).checked=false;}
    }


    function rightsoptions(value,value1,value2){
        var group=document.getElementById(value1).options[document.getElementById(value1).selectedIndex].value;
        var values = group.split(':+:'); var urights = value2.split(',');var cykl=1;
        for (i = 0; i < value; i++) {
            if (document.getElementById("rights"+i).name==values[0]){document.getElementById("rights"+i).disabled=false;
            for(var ni = 1; ni < (urights.length-1); ni++) {
                vysledek=urights[ni].split("*"+values[1]+":")                          
                if (vysledek[1] !==undefined && cykl==1){cykl=0;
                    for (var fin =0; fin < vysledek[1].length; fin++) {document.getElementById("right"+vysledek[1].substring(fin,fin+1)).checked=true;} 
                }
            }}
            else {document.getElementById("rights"+i).disabled=true;} 
        }
    }
                                 
    
                          
    

     function setselected(elmnt,value){  
     var number = document.getElementById(elmnt);
          for (i=0; i<number.options.length; i++){  
              if (number.options[i].value == value){number.options[i].selected=true;}
          }
     }
    

    
    function checkusers(value) {
    script = document.createElement('script');
    script.src = './checkusers.php?usrname=' + value;
    document.getElementsByTagName('head')[0].appendChild(script);
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