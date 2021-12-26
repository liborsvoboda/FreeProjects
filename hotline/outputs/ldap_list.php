<?php
$ldaphost = 'ldap://server3.ldap.local';
$ldapport = 389;
$ds = ldap_connect($ldaphost, $ldapport) or die("Could not connect to $ldaphost");
    ldap_set_option($ldapconn, LDAP_OPT_PROTOCOL_VERSION, 3);
    ldap_set_option($ldapconn, LDAP_OPT_REFERRALS, 0);
ldap_set_option($ds, LDAP_OPT_DEBUG_LEVEL, 5);
if ($ds) 
{
    $username = "username@ldap.local";
    $upasswd = "*****";
    $username = "user@ldap.local";
    $upasswd = "password";
    $ldapbind = ldap_bind($ds, $username, $upasswd);

   // if ($ldapbind) 
       // {
        //print "Congratulations! $username is authenticated.<br>";
        

// login je jasny, list, basedn, je slozka - ktera je vychozi 
// a nasledne v ldap_list filtruji OU nebo CN - velka mala pismena nehraji roli - WINDOWS
// v infu pak filtruji na dane pole (OU,CN,NAME,) info [i - zaznam][typ objektu - name,cn, etc..][0 - jeho hodnota, nektere typy mohou mit vic hodnot]
// cmd dsquery - command pro vypis LDAP 
        
//$basedn = "DC=ldap,DC=local"; // for OU show
$basedn = "CN=Users,DC=ldap,DC=local"; // for users show
$justthese = array("ou");  // for OU show
$justthese = array("users");  // for users show


//*$sr = ldap_list($ds, $basedn, "ou=*", $justthese);  // for OU show
$sr = ldap_list($ds, $basedn,"CN=*");
$info = ldap_get_entries($ds, $sr);
//$sr = ldap_list($ds, $basedn, "ou=*", $justthese);  // for OU show
//$info = ldap_get_entries($ds, $sr);
echo "Celé Jméno;Funkce;Jméno;Příjmení;Město;Mobil;Mail;Telefon;<BR>";

for ($i=0; $i < $info["count"]; $i++) {
    
for ($j=0; $j < $info[$i]["count"]; $j++) {
   // echo $info[$i][$j]."<br>";
}
    //echo $info[0][$i]."<br>";
// if    ($info[$i]["name"][0] == "Libor Svoboda"){
    echo $info[$i]["name"][0].";"; //full name 
    echo $info[$i]["title"][0].";"; //function
    echo $info[$i]["givenname"][0].";"; //name
    echo $info[$i]["sn"][0].";"; //surname 
    echo $info[$i]["l"][0].";"; //city
    echo $info[$i]["mobile"][0].";"; //mobile
    echo $info[$i]["mail"][0].";"; //email
    echo $info[$i]["telephonenumber"][0].";<BR>"; //phone
    
    
    //www
    //street
    //POSTCODE
    //



//objectclass;cn;sn;title;description;physicaldeliveryofficename;telephonenumber;usercertificate;givenname;initials;distinguishedname;instancetype;whencreated;whenchanged;displayname;usncreated;memberof;usnchanged;department;proxyaddresses;name;objectguid;useraccountcontrol;badpwdcount;codepage;countrycode;badpasswordtime;lastlogoff;lastlogon;pwdlastset;primarygroupid;objectsid;accountexpires;logoncount;samaccountname;samaccounttype;showinaddressbook;legacyexchangedn;userprincipalname;lockouttime;

    
 
    
    }
}
        
//}        //}
    //else 
      //  {print "Access Denied!";}








?> 
