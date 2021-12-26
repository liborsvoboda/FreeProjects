<?php
if (isset($_GET["ico"])) {
$xmlDoc = new DOMDocument();
$xmlDoc->load("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_std.cgi?ico=".$_GET["ico"]);
//$xmlDoc->load("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_bas.cgi?ico=".$_GET["ico"]);
$xml_doc=$xmlDoc->saveXML();
$xpath = new DOMXPath($xmlDoc);
//echo $xml_doc; // - returned XML Form code
?>
/*
document.frames('program_body').document.body.document.getElementById("reg_value1").value = "<?echo $xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/D:VBAS/D:OF)");?>";
*/

document.frames('program_body').document.body.document.getElementById("reg_value3").value = "<?echo $xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Obchodni_firma)");?>";
<?if ($xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Identifikace/are:Adresa_ARES/dtt:Cislo_orientacni)")<>"") {
    $orient_no ="/".$xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Identifikace/are:Adresa_ARES/dtt:Cislo_orientacni)");
} else {$orient_no ="";}?>
document.frames('program_body').document.body.document.getElementById("reg_value4").value = "<?echo $xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Identifikace/are:Adresa_ARES/dtt:Nazev_ulice)")." ".$xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Identifikace/are:Adresa_ARES/dtt:Cislo_domovni)").$orient_no;?>";
document.frames('program_body').document.body.document.getElementById("reg_value5").value = "<?echo $xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Identifikace/are:Adresa_ARES/dtt:Nazev_obce)");?>";
document.frames('program_body').document.body.document.getElementById("reg_value6").value = "<?echo $xpath->evaluate("string(/are:Ares_odpovedi/are:Odpoved/are:Zaznam/are:Identifikace/are:Adresa_ARES/dtt:PSC)");?>";
<?if (isset($_GET["check"])) {echo "parent.prepare_customer_data();";}?>
parent.document.getElementById("loading").style.display="none";
<?}?>
