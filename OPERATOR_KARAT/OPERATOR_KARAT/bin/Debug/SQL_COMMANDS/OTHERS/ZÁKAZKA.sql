WINDOWNAME:ZÁKAZKA:
INPUT:TEXTBOX:USR_OPV:Zák/Nomen:CharacterCasing-Normal:
INPUT:DATE:USR_DATZAH:Datum:CharacterCasing-Normal:
INPUT:BUTTON:USR_BUTTON:ODESLAT:Enabled/True:Submit/True:
#XLS_EXPORT:USR_BTN:Export do XLS:Export/True:
SQL:SELECT opv as 'Zakázka', nomenklatura as 'Nomenklatura', postup as 'Postup', popis as 'Popis', planvyroba0 as 'PlanVyroby', odvedeno as 'Odvedeno', atribut_1 as 'Atribut_1', opvfinal as 'OPVFinal' from DBNAME.dba.v_opvvyrza where ((opv = *USR_OPV*) OR  (nomenklatura=*USR_OPV*) OR (LEN(*USR_OPV*)=0 AND da_pl_zah = CONVERT(DATETIME,*USR_DATZAH*,105))) AND opv <>'' AND user_vytisteno = 1
#SUBBUTTON:test.txt:STIM_BTN:TISK:Enabled/True:STIM/1
NOAUTOSUM:True
FORSUBVIEW:USR_FLD1/0
STIMBUTTON::STIM_BTN:Tisk:Enabled/True:STIM/1:
STIM_REPORT:Tisk Zakázky:zp_kopie.mrt:pr_opv/*USR_FLD1*
#AUTOROOT:10