WINDOWNAME:Násilně Ukončit:
SQL:update DBNAME.dba.v_ter_proc2 set stav = 99,autor = 'PRAC',ts = getdate() from DBNAME.dba.v_ter_proc2 pr2 where pr2.id_podprocesu = *USR_FLD2* and pr2.stav = 0
NOAUTOSUM:True
AUTOROOT:1
AUTOSUBMIT:10
