WINDOWNAME:Nomenklatury:
LABEL::
INPUT:TEXTBOX:USR_FILTER:Nomenklatura:Text/2X:Submit/True:
SQL:SELECT TOP 100 [cislo_nomenklatury],[platnost_od],CAST([dph] as int) FROM [CATALOG].[dbo].[nomenklatura] with(NOLOCK) WHERE [cislo_nomenklatury] LIKE '%'+*USR_FILTER*+'%'
NOAUTOSUM:True:
COLUMNSUM:3: