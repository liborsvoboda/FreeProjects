<script type="text/javascript">
var pp = new Array();     
var leftap = new Array();     
var lefta = new Array();     

function position(value) {

for (var i=0;i<value;i++) {   
 pp[i]=document.getElementById("picture"+i);
 leftap[i] = parseInt(document.getElementById("picture"+i).style.left);
 lefta[i]=leftap[i];

    }
}

function movegallery(value,value1) {
if (value=='1' && lefta>=(leftap-90) && lefta>=-(((value1-3)*96)-6)) {
				var tt=setTimeout("movegallery(1)",20);
				lefta=lefta-6;

    for (var i=0;i<value1;i++) {alert("move");
     pp[i].style.left=lefta+"px";  
    }
}                

if (value=='2' && lefta<=(leftap+90) && lefta<=-6) {
				var tt=setTimeout("movegallery(2)",20);
				lefta=lefta+6;
    for (var i=0;i<value1;i++) {
     pp[i].style.left=lefta+"px";  
    }
}

}



</script>