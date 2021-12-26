WINDOWNAME:Zùstatek Dovolené:
INPUT:TEXTBOX:USR_KARTA:KARTA UŽIVATELE:MUST:Submit/False:TabStop/True:CharacterCasing-Normal:
INPUT:TEXTBOX:USR_TEXT:èíslo:Submit/False:TabStop/True:
#INPUT:COMBOBOX:USR_COMBO:Combo:DropDownStyle/2:TabStop/True:SQLITEMS/SELECT name FROM CMP_CATALOG.dbo.test_tbl:
INPUT:DATE:USR_DATE:Datum Výkazu:TabStop/True:CustomFormat/dd.MM.yyyy:
INPUT:BUTTON:USR_SAVEBTN:ULOŽIT:Submit/True:
SQL:INSERT INTO [CMP_CATALOG].[dbo].[test_tbl](name,test,datum,insert_date)VALUES(*USR_KARTA*,CONVERT(FLOAT,REPLACE (*USR_TEXT*, ',', '.' )), CONVERT(DATE,*USR_DATE*,105),GETDATE())
AUTOROOT:60

