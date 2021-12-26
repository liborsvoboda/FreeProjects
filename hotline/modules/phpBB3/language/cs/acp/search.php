<?php
/** 
*
* acp_search [Čeština]
*
* @package language
* @version $Id: search.php 505 2010-11-21 10:32:21Z ameeck $
* @copyright (c) 2010 phpBB.cz
* @license http://opensource.org/licenses/gpl-license.php GNU Public License 
* Original copyright: (c) 2005 phpBB Group
*
*/

/**
* DO NOT CHANGE
*/
if (!defined('IN_PHPBB'))
{
	exit;
}

if (empty($lang) || !is_array($lang))
{
	$lang = array();
}

// DEVELOPERS PLEASE NOTE
//
// All language files should use UTF-8 as their encoding and the files must not contain a BOM.
//
// Placeholders can now contain order information, e.g. instead of
// 'Page %s of %s' you can (and should) write 'Page %1$s of %2$s', this allows
// translators to re-order the output of data while ensuring it remains correct
//
// You do not need this where single placeholders are used, e.g. 'Message %d' is fine
// equally where a string contains only two placeholders which are used to wrap text
// in a url you again do not need to specify an order e.g., 'Click %sHERE%s' is fine

$lang = array_merge($lang, array(
	'ACP_SEARCH_INDEX_EXPLAIN'				=> 'Zde lze spravovat všechny indexy vyhledávacího backendu. Vzhledem k&nbsp;tomu, že bežně používáte pouze jeden index najednou, je doporučeno druhý odstranit pro úsporu místa s&nbsp;tím, že lze kdykoliv znovu vytvořit. Po změně některých nastavení (např. minimální/maximální počet znaků) bude možná stát za to vytvořit nový index, aby se projevily nové změny.',
	'ACP_SEARCH_SETTINGS_EXPLAIN'			=> 'Zde můžete nastavit jaký backend bude použit pro indexování příspěvků a vyhledávání. Můžete nastavit různé možnosti, které mají vliv na to, jak bude vyhledávání náročné na server. Některá z těchto nastavení jsou stejná pro všechny backendy.',

	'COMMON_WORD_THRESHOLD'						=> 'Hranice často používaného slova',
	'COMMON_WORD_THRESHOLD_EXPLAIN'		=> 'Slova, která se objevují v&nbsp;příspěvcích ve větší míře jsou označená jako častá. Častá slova jsou ignorována při vyhledávání. Nastavením na 0 toto chování vypnete. Tuto funkce lze použít jen při více než 100 příspěvcích. Pokud chcete, aby se projevily změny v nastavení častých slov, musíte znovu vytvořit vyhledávací rejstřík/index.',
	'CONFIRM_SEARCH_BACKEND'					=> 'Přejete si přepnout na jiný vyhledávací backend? Po změně vyhledávacího backendu budete muset vytvořit index pro nový vyhledávací backend. Pokud neplánujete přepnout zpět na předchozí vyhledávací backend můžete smazat staré backendové indexy a uvolnit tak systémové prostředky.',
	'CONTINUE_DELETING_INDEX'					=> 'Pokračovat v&nbsp;předchozím odstraňování indexu',
	'CONTINUE_DELETING_INDEX_EXPLAIN'	=> 'Byl zahájen proces smazaní vyhledávacího indexu. Pro znovuzpřístupnění vyhledávání musíte tuto operaci dokončit.',
	'CONTINUE_INDEXING'								=> 'Pokračovat v&nbsp;předchozím indexačním procesu',
	'CONTINUE_INDEXING_EXPLAIN'				=> 'Byl zahájen proces vytvoření vyhledávacího indexu. Pro znovuzpřístupnění vyhledávání musíte tuto operaci dokončit.',
	'CREATE_INDEX'										=> 'Vytvořit index',

	'DELETE_INDEX'												=> 'Smazat index',
	'DELETING_INDEX_IN_PROGRESS'					=> 'Mazání indexu v&nbsp;průběhu',
	'DELETING_INDEX_IN_PROGRESS_EXPLAIN'	=> 'Vyhledávací backend právě pročišťuje svůj index, toto může trvat několik minut.',

	'FULLTEXT_MYSQL_INCOMPATIBLE_VERSION'	=> 'MySQL fulltext může být použit pouze od MySQL4 a vyšší.',
	'FULLTEXT_MYSQL_NOT_MYISAM'						=> 'MySQL fulltext index lze vytvořit jen v&nbsp;MyISAM tabulkách.',
	'FULLTEXT_MYSQL_TOTAL_POSTS'					=> 'Celkový počet indexovaných příspěvků',
	'FULLTEXT_MYSQL_MBSTRING'							=> 'Podpora UTF-8 znaků mimo znakové sady latin použitím mbstring:',
	'FULLTEXT_MYSQL_PCRE'									=> 'Podpora UTF-8 znaků mimo znakové sady latin použitím PCRE:',
	'FULLTEXT_MYSQL_MBSTRING_EXPLAIN'			=> 'Pokud PCRE neobsahuje vlastnosti unicode znaků, vyhledávání se pokusí využít engine pro regulérní výrazy z mbstring.',
	'FULLTEXT_MYSQL_PCRE_EXPLAIN'					=> 'Tento vyhledávácí backend vyžaduje vlastnosti unicode znaku z PCRE, které jsou dostupné pouze v&nbsp;PHP 4.4, 5.1 a vyšších, pokud chcete vyhledávat znaky, které nejsou v&nbsp;běžných znakových sadách.',
	
	'FULLTEXT_MYSQL_MIN_SEARCH_CHARS_EXPLAIN'   => 'Pouze slova s touto minimální délkou budou brány v úvahu při hledání. Pokud byste chtěli změnit toto nastavení, je potřeb tak učinit přímo v nastavení MySQL, pravděpodobně to bude muset udělat poskytovatel vašeho hostingu.',
    'FULLTEXT_MYSQL_MAX_SEARCH_CHARS_EXPLAIN'   => 'Pouze slova kratší než toto číslo budou brány v úvahu při hledání. Pokud byste chtěli změnit toto nastavení, je potřeb tak učinit přímo v nastavení MySQL, pravděpodobně to bude muset udělat poskytovatel vašeho hostingu.',

	'GENERAL_SEARCH_SETTINGS'				=> 'Obecné nastavení hledání',
	'GO_TO_SEARCH_INDEX'						=> 'Přejít na stránku vyhledávání',

	'INDEX_STATS'										=> 'Statistiky indexu',
	'INDEXING_IN_PROGRESS'					=> 'Indexace probíhá',
	'INDEXING_IN_PROGRESS_EXPLAIN'	=> 'Vyhledávací backend právě indexuje všechny příspěvky na vašem fóru. Toto může trvat několik minut až hodin, podle velikosti vašeho fóra.',

	'LIMIT_SEARCH_LOAD'						=> 'Omezení vyhledávání při zatížení serveru',
	'LIMIT_SEARCH_LOAD_EXPLAIN'		=> 'Pokud průměr vytížení serveru za 1 minutu dosáhne této hodnoty, stránka vyhledávání se automaticky vypne, 1.0 se rovná ~100% využití procesoru. Toto funguje pouze na UNIXových serverech.',

	'MAX_SEARCH_CHARS'								=> 'Maximální počet znaků pro indexaci',
	'MAX_SEARCH_CHARS_EXPLAIN'				=> 'Pouze slova, která se skládají z méně znaků, než je nastaveno, budou zaindexována.',
	'MAX_NUM_SEARCH_KEYWORDS'					=> 'Limit vyhledaných klíčových slov',
	'MAX_NUM_SEARCH_KEYWORDS_EXPLAIN'	=> 'Maximální počet slov, které může uživatel vyhledat. Pokud je zde nastavena 0, není kladeno žádné omezení co do počtu slov.',

	'MIN_SEARCH_CHARS'								=> 'Minimální počet znaků pro indexaci',
	'MIN_SEARCH_CHARS_EXPLAIN'				=> 'Pouze slova, která se skládají z více znaků, než je nastaveno, budou zaindexována.',
	'MIN_SEARCH_AUTHOR_CHARS'					=> 'Minimální počet znaků ve jméně autora',
	'MIN_SEARCH_AUTHOR_CHARS_EXPLAIN'	=> 'Uživatelé musí vložit alespoň tento počet znaků při vyhledávání uživatelského jména s&nbsp;použitím zástupných znaků. Pokud je uživatelovo jméno kratší, než je zde nastaven počet znaků, stále jde vyhledat jeho příspěvky zadaním celého uživatelského jména.',

	'PROGRESS_BAR'							=> 'Postup',

	'SEARCH_GUEST_INTERVAL'					=> 'Ochranný interval pro anonymní',
	'SEARCH_GUEST_INTERVAL_EXPLAIN'	=> 'Počet sekund, který musí anonymní návštěvník vyčkat mezi jednotlivými hledáními. Pokud jeden návštěvník vyhledává, ostatní musejí vyčkat uplynutí této lhůty.',
	'SEARCH_INDEX_CREATE_REDIRECT'	=> 'Příspěvky až k&nbsp;ID %1$d byly dosud zaindexovány, z toho %2$d příspěvků v&nbsp;minulém kroku.<br />Současný průměr indexace je přibližně %3$.1f příspěvků za sekundu.<br />Indexace právě probíhá…',
	'SEARCH_INDEX_DELETE_REDIRECT'	=> 'Příspěvky až k&nbsp;ID %1$d byly dosud odstraněny z indexu vyhledávání.<br />Odstraňování právě probíhá…',
	'SEARCH_INDEX_CREATED'					=> 'Všechny příspěvky v&nbsp;databázi fóra byly úspěšně zaindexovány.',
	'SEARCH_INDEX_REMOVED'					=> 'Vyhledávácí index byl odstraněn z databáze.',
	'SEARCH_INTERVAL'								=> 'Ochranný interval pro uživatele',
	'SEARCH_INTERVAL_EXPLAIN'				=> 'Počet sekund, po kterém je možno znovu hledat. Interval platí zvlášť pro každého uživatele.',
	'SEARCH_STORE_RESULTS'					=> 'Cache výsledků vyhledávání',
	'SEARCH_STORE_RESULTS_EXPLAIN'	=> 'Cachované výsledky vyhledávání se automaticky odstraní po uplynutí této doby. Pokud je nastaveno na 0, cache výsledků je vypnuta.',
	'SEARCH_TYPE'										=> 'Vyhledávací backend',
	'SEARCH_TYPE_EXPLAIN'						=> 'phpBB vám umožňuje vybrat backend, který bude použit pro vyhledávání na fóru. Ve výchozím stavu phpBB použije vlastní fulltextové řešení.',
	'SWITCHED_SEARCH_BACKEND'				=> 'Změnili jste vyhledávácí backend. Abyste mohli využívat nového backendu pro vyhledávání, musíte se ujistit, že je vytvořen index pro vyhledávání.',

	'TOTAL_WORDS'				=> 'Celkový počet indexovaných slov',
	'TOTAL_MATCHES'			=> 'Celkový počet spojení mezi slovy a příspěvky',

	'YES_SEARCH'									=> 'Povolit vyhledávání',
	'YES_SEARCH_EXPLAIN'					=> 'Umožnit uživatelům využívat vyhledávání na fóru včetně vyhledávání uživatelů.',
	'YES_SEARCH_UPDATE'						=> 'Povolit aktualizaci indexu při přispívání',
	'YES_SEARCH_UPDATE_EXPLAIN'		=> 'Aktualizuje index po přidání příspěvku, ignorováno pokud je vyhledávání vypnuto.',
));

?>