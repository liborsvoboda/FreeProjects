WINDOWNAME:OPERÁTOR:
INPUT:TEXTBOX:USR_KARTA:Kód karty:CharacterCasing-Upper:UseSystemPasswordChar/True:Submit/True:
SQL:SELECT v_ter_obs.osoba,coalesce((select o.jmeno + ' ' + o.prijmeni from dba.odb_osoby o with(index( pk_odb_osoby)) where o.osoba=v_ter_obs.osoba),'') as 'Jméno',v_ter_obs.smena_vp as 'Směna',v_ter_obs.typ_udalosti as 'Událost',v_ter_obs.datum as 'Datum' FROM dba.v_ter_obs v_ter_obs WHERE 1=1  AND ((select max(ppom.kod_karty) from dba.odb_osoby odb, dba.ppom ppom where odb.oscislo=ppom.oscislo and odb.osoba=v_ter_obs.osoba)=*USR_KARTA* AND v_ter_obs.osoba<>'' AND LEN(*USR_KARTA*)=10 )
SUBBUTTON:PODPROCESY.SQL:SUB_BTN1:Podprocesy:Enabled/True:Submit/Next: 
SUBBUTTON:ODVADENI.SQL:SUB_BTN2:Odvádění:Enabled/True:Submit/Next:
FORSUBVIEW:USR_FLD1/0:
AUTOROOT:60