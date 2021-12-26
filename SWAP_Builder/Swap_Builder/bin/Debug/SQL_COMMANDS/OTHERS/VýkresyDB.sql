WINDOWNAME:OBRÁZEK:
INPUT:BUTTON:USR_KARTA:Načíst Data:Submit/True:
SQL:SELECT TOP 100 * FROM [DNBAME].[dba].[v_cad_epdmsldw_vazby] product_picture  WHERE DATALENGTH(product_picture.user_png) <> '0'
MESSAGE:Zpráva pro uživatele | čus
AUTOROOT:60