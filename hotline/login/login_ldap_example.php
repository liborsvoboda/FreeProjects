<?php
$ldaphost = 'ldap://server3.ldap.local';
$ldapport = 389;
$ds = ldap_connect($ldaphost, $ldapport) or die("Could not connect to $ldaphost");
    ldap_set_option($ldapconn, LDAP_OPT_PROTOCOL_VERSION, 3);
    ldap_set_option($ldapconn, LDAP_OPT_REFERRALS, 0);
ldap_set_option($ds, LDAP_OPT_DEBUG_LEVEL, 7);
if ($ds) 
{
    $username = "username@ldap.local";
    $upasswd = "*****";
    $ldapbind = ldap_bind($ds, $username, $upasswd);

    if ($ldapbind) 
        {print "Congratulations! $username is authenticated.<br>";
        

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

for ($i=0; $i < $info["count"]; $i++) {
    echo $info[$i]["name"][0]."<br>";
}
        
        }
    else 
        {print "Access Denied!";}
}








?> 
