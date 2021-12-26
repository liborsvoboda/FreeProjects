WINDOWNAME:ZÁKAZKA:
INPUT:TEXTBOX:USR_OPV:Zák/Nomen:CharacterCasing-Normal:
INPUT:DATE:USR_DATZAH:Datum:CharacterCasing-Normal:
INPUT:BUTTON:USR_BUTTON:ODESLAT:Enabled/True:Submit/True:
#XLS_EXPORT:USR_BTN:Export do XLS:Export/True:
SQL:SELECT v_opv.opv as 'Zakázka', v_opv.nomenklatura as 'Nomenklatura', v_opv.postup as 'Postup', v_opv.popis as 'Popis', v_opv.planvyroba0 as 'PlanVyroby', v_opv.odvedeno as 'Odvedeno', v_opv.atribut_1 as 'Atribut_1', nom.user_cislo_vykresu as 'Číslo výkresu', v_opv.opvfinal as 'OPVFinal' from DBNAME.dba.v_opvvyrza v_opv,DBNAME.dba.nomenklatura nom where ((v_opv.opv = *USR_OPV*) OR  (v_opv.nomenklatura=*USR_OPV*) OR (LEN(*USR_OPV*)=0 AND v_opv.da_pl_zah = CONVERT(DATETIME,*USR_DATZAH*,105))) AND v_opv.opv <>'' AND v_opv.user_vytisteno = 1 AND nom.cislo_nomenklatury = v_opv.nomenklatura
#SUBBUTTON:test.txt:STIM_BTN:TISK:Enabled/True:STIM/1
NOAUTOSUM:True
FORSUBVIEW:USR_FLD1/0:USR_FLD2/2:USR_FLD3/7
STIMBUTTON::STIM_BTN:Tisk:Enabled/True:STIM/1:
STIM_REPORT:Tisk Zakázky:zp_kopie.mrt:pr_opv/*USR_FLD1*
#AUTOROOT:10
PDFBUTTON::PDF_BTN:Výkres:Enabled/True:MESS/True:PATH/\\server5\EPDM_PDF\EPDM_CMP\*USR_FLD2*.pdf;\\server5\EPDM_PDF\EPDM_CMP\*USR_FLD3*.pdf
