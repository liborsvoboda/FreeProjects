<?php
/** 
*
* acp_board [Čeština]
*
* @package language
* @version $Id: board.php 505 2010-11-21 10:32:21Z ameeck $
* @copyright (c)  2007 phpBB.cz
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

// Board Settings
$lang = array_merge($lang, array(
	'ACP_BOARD_SETTINGS_EXPLAIN'	=> 'Zde můžete definovat základní nastavení, jako je název a popis fóra, popř. určit výchozí jazyk a časové pásmo.',
	'CUSTOM_DATEFORMAT'				=> 'Vlastní…',
	'DEFAULT_DATE_FORMAT'			=> 'Formát data',
	'DEFAULT_DATE_FORMAT_EXPLAIN'	=> 'Formát data je stejný jako PHP funkce <code>date</code>.',
	'DEFAULT_LANGUAGE'				=> 'Výchozí jazyk',
	'DEFAULT_STYLE'					=> 'Výchozí styl',
	'DISABLE_BOARD'					=> 'Vypnout fórum',
	'DISABLE_BOARD_EXPLAIN'			=> 'Znepřístupnění fóra pro uživatele. Dle libosti můžete též přidat krátkou (255 znaků) zprávu, která se uživatelům zobrazí.',
	'OVERRIDE_STYLE'				=> 'Vždy použít výchozí styl',
	'OVERRIDE_STYLE_EXPLAIN'		=> 'Ignoruje uživatelovo nastavení vzhledu.',
	'SITE_DESC'						=> 'Popis fóra',
	'SITE_NAME'						=> 'Název fóra',
	'SYSTEM_DST'					=> 'Zapnout letní čas',
	'SYSTEM_TIMEZONE'            => 'Časové pásmo návštěvníků',
  'SYSTEM_TIMEZONE_EXPLAIN'         => 'Nepřihlášení uživatelé uvidí časy na fóru v tomto časovém pásmo. Registrovani uživatele si jej mohou změnit při registraci a později ve svém panelu nastavení.',
	'WARNINGS_EXPIRE'				=> 'Doba platnosti varování',
	'WARNINGS_EXPIRE_EXPLAIN'		=> 'Počet dní, které musí uplynout, než se varování automaticky odstraní z uživatelského profilu. Pro nastavení trvalého varování zadejte 0.',
));

// Board Features
$lang = array_merge($lang, array(
	'ACP_BOARD_FEATURES_EXPLAIN'	=> 'Zde můžete nastavit některé funkce fóra.',
	'ALLOW_ATTACHMENTS'			=> 'Povolit přílohy',
	'ALLOW_BIRTHDAYS'			=> 'Povolit narozeniny',
	'ALLOW_BIRTHDAYS_EXPLAIN'	=> 'Povolit vkládání narozenin a zobrazení věku v&nbsp;uživatelském profilu. Upozorňujeme, že výpis narozenin v&nbsp;obsahu fóra je řízen nastavením zatížení.',
	'ALLOW_BOOKMARKS'			=> 'Povolit záložky',
	'ALLOW_BOOKMARKS_EXPLAIN'	=> 'Uživatelé si mohou označovat oblíbená témata.',
	'ALLOW_BBCODE'				=> 'Povolit BBCode',
	'ALLOW_FORUM_NOTIFY'		=> 'Povolit sledování fór',
	'ALLOW_NAME_CHANGE'			=> 'Povolit změnu uživatelských jmen',
	'ALLOW_NO_CENSORS'			=> 'Povolit deaktivaci cenzury',
	'ALLOW_NO_CENSORS_EXPLAIN'	=> 'Uživatelé si sami mohou zrušit automatickou cenzuru slov v&nbsp;příspěvcích a soukromých zprávách.',
	'ALLOW_PM_ATTACHMENTS'		=> 'Povolit přílohy v&nbsp;soukromých zprávách',
	'ALLOW_PM_REPORT'			=> 'Povolit uživatelům nahlašovat soukromé zprávy',
	'ALLOW_PM_REPORT_EXPLAIN'	=> 'Povolte pro umožnění nahlášení závadné soukromé zprávy uživatelem. Nahlášené zprávy budou viditelné v moderátorském panelu.',
	'ALLOW_QUICK_REPLY'			=> 'Povolit rychlou odpoveď',
	'ALLOW_QUICK_REPLY_EXPLAIN'	=> 'Toto nastavení určí jestli je Rychlá odpoveď povolená nebo ne. Navíc je potřeba povolit toto nastavení pro každé fórum zvlášť.',
	'ALLOW_QUICK_REPLY_BUTTON'	=> 'Po odeslání povolit rychlé odpovědi ve všech fórech',
	'ALLOW_SIG'					=> 'Povolit podpisy',
	'ALLOW_SIG_BBCODE'			=> 'Povolit BBCode v&nbsp;podpisech',
	'ALLOW_SIG_FLASH'			=> 'Povolit použití značky <code>[FLASH]</code> v&nbsp;podpisech',
	'ALLOW_SIG_IMG'				=> 'Povolit použití značky <code>[IMG]</code> v&nbsp;podpisech',
	'ALLOW_SIG_LINKS'			=> 'Povolit odkazy v&nbsp;podpisech',
	'ALLOW_SIG_LINKS_EXPLAIN'	=> 'Je-li volba zakázána, značky <code>[URL]</code> a automatické oživování odkazů jsou vypnuty.',
	'ALLOW_SIG_SMILIES'			=> 'Povolit smajlíky v&nbsp;podpisech',
	'ALLOW_SMILIES'				=> 'Povolit smajlíky',
	'ALLOW_TOPIC_NOTIFY'		=> 'Povolit sledování témat',
	'BOARD_PM'					=> 'Soukromé zprávy',
	'BOARD_PM_EXPLAIN'			=> 'Povolit soukromé zprávy všem uživatelům.',
));

// Avatar Settings
$lang = array_merge($lang, array(
	'ACP_AVATAR_SETTINGS_EXPLAIN'	=> 'Avatary obecně jsou malé obrázky, které si mohou uživatelé zvolit a jimi se specifikovat. Zpravidla bývají zobrazeny pod uživatelským jménem u&nbsp;jejich příspěvků (záleží na grafickém stylu fóra). Zde můžete určit, jak mohou uživatelé své avatary definovat. Berte na vědomí, že pro zpřístupnění možnosti nahrávání avatarů musíte mít na serveru vytvořený adresář, kam budou nahrávány a do kterého má server právo zapisovat. Upozorňujeme také, že omezení velikostí souborů platí jen pro nahrávané soubory, ne pro odkazované ze vzdálených serverů.',

	'ALLOW_AVATARS'					=> 'Povolit avatary',
	'ALLOW_AVATARS_EXPLAIN'			=> 'Umožnit obecné používání avatarů;<br />Pokud vypnete používání avatarů obecně nebo vypnete některý z druhů avatarů, nebudou už zobrazeny na fóru, ale uživatelé si je budou moci stáhnout v uživatelském panelu, aby o ně nepřišli.',
	'ALLOW_LOCAL'					=> 'Povolit avatary z galerie',
	'ALLOW_REMOTE'					=> 'Povolit vzdálené avatary',
	'ALLOW_REMOTE_EXPLAIN'			=> 'Avatary umístěné na jiných serverech.',
	'ALLOW_REMOTE_UPLOAD'			=> 'Povolit vzdálené nahrávání avatarů',
	'ALLOW_REMOTE_UPLOAD_EXPLAIN'	=> 'Umožní nahrát si avatar pomocí odkazu na cizí stránku.',
	'ALLOW_UPLOAD'					=> 'Povolit nahrávání avatarů',
	'AVATAR_GALLERY_PATH'			=> 'Cesta ke galerii avatarů',
	'AVATAR_GALLERY_PATH_EXPLAIN'	=> 'Cesta přes kořenový adresář phpBB pro přednastavené avatary, např. <samp>images/avatars/gallery</samp>',
	'AVATAR_STORAGE_PATH'			=> 'Cesta k&nbsp;ukládání avatarů',
	'AVATAR_STORAGE_PATH_EXPLAIN'	=> 'Cesta přes kořenový adresář phpBB, např. <samp>images/avatars/upload</samp>',
	'MAX_AVATAR_SIZE'				=> 'Maximální rozměry avatarů',
	'MAX_AVATAR_SIZE_EXPLAIN'		=> 'Šířka x výška v&nbsp;pixelech',
	'MAX_FILESIZE'					=> 'Maximální velikost obrázkového souboru',
	'MAX_FILESIZE_EXPLAIN'			=> 'Pro nahrávané avatary. 0 znamená, že velikost nahrávaných souborů je omezena jen konfigurací vašeho PHP.',
	'MIN_AVATAR_SIZE'				=> 'Minimální rozměry avatarů',
	'MIN_AVATAR_SIZE_EXPLAIN'		=> 'Šířka x výška v&nbsp;pixelech',
));

// Message Settings
$lang = array_merge($lang, array(
	'ACP_MESSAGE_SETTINGS_EXPLAIN'		=> 'Zde můžete definovat všechna výchozí nastavení soukromých zpráv.',

	'ALLOW_BBCODE_PM'				=> 'Povolit BBCode v&nbsp;soukromých zprávách',
	'ALLOW_FLASH_PM'				=> 'Povolit použití značky <code>[FLASH]</code>',
	'ALLOW_FLASH_PM_EXPLAIN'	=> 'Upozorňujeme, že možnost použití značky Flash v&nbsp;soukromých zprávách, je-li povoleno, závisí na nastavení oprávnění.',
	'ALLOW_FORWARD_PM'			=> 'Povolit přeposílání soukromých zprávách',
	'ALLOW_IMG_PM'					=> 'Povolit použití značky <code>[IMG]</code>',
	'ALLOW_MASS_PM'					=> 'Povolit posílání soukromých zpráv více uživatelům nebo skupinám',
	'ALLOW_MASS_PM_EXPLAIN'	=> 'Zasílání konkrétním skupinám lze upravit v nastavení té skupiny.',
	'ALLOW_PRINT_PM'			=> 'Povolit zobrazení soukromých zpráv v&nbsp;rozvržení pro tisk',
	'ALLOW_QUOTE_PM'			=> 'Povolit citování soukromých zpráv',
	'ALLOW_SIG_PM'				=> 'Povolit připojení podpisu v&nbsp;soukromých zprávách',
	'ALLOW_SMILIES_PM'			=> 'Povolit smajlíky v&nbsp;soukromých zprávách',
	'BOXES_LIMIT'				=> 'Maximální počet zpráv na schránku',
	'BOXES_LIMIT_EXPLAIN'		=> 'Maximální počet zpráv, které může mít uživatel ve schránce. Nastavte na 0 pro neomezený počet zpráv.',
	'BOXES_MAX'					=> 'Maximální počet složek',
	'BOXES_MAX_EXPLAIN'			=> 'Maximální počet uživatelem vytvořených složek pro soukromé zprávy.',
	'ENABLE_PM_ICONS'			=> 'Povolit použití ikon v&nbsp;záhlaví soukromých zpráv',
	'FULL_FOLDER_ACTION'		=> 'Výchozí akce při zaplněné složce',
	'FULL_FOLDER_ACTION_EXPLAIN'=> 'Akce, k&nbsp;níž dojde, pokud nelze použít uživatelovo nastavení (je-li definováno). Výjimkou je složka „Odeslané zprávy“, kde standardně dojde k&nbsp;automatickému odstranění starých zpráv.',
	'HOLD_NEW_MESSAGES'			=> 'Podržet příchozí zprávy',
	'PM_EDIT_TIME'				=> 'Omezit čas pro úpravu',
	'PM_EDIT_TIME_EXPLAIN'		=> 'Omezuje čas, po který je možné upravovat nedoručenou soukromou zprávu. Pokud je hodnota nastavena na 0, funkce je vypnuta.',
	'PM_MAX_RECIPIENTS'         => 'Povolený počet příjemců',
	'PM_MAX_RECIPIENTS_EXPLAIN'   => 'Maximální počet příjemců pro jednu soukromou zprávu. Pokud je nastavena 0, zprávu lze zaslat neomezenému počtu uživatelů. Toto nastavení lze upravit pro každou skupinu v jejím nastavení.',

));

// Post Settings
$lang = array_merge($lang, array(
	'ACP_POST_SETTINGS_EXPLAIN'			=> 'Zde definujete všechna výchozí nastavení pro přispívání.',
	'ALLOW_POST_LINKS'							=> 'Povolit odkazy v&nbsp;příspěvcích/soukromých zprávách',
	'ALLOW_POST_LINKS_EXPLAIN'			=> 'Je-li zakázáno, značky <code>[URL]</code> a automatické oživování odkazů jsou vypnuty.',
	'ALLOW_POST_FLASH'							=> 'Povolit použití značky<code>[FLASH]</code> v&nbsp;příspěvcích',
	'ALLOW_POST_FLASH_EXPLAIN'			=> 'Je-li zakázáno, nelze v&nbsp;příspěvcích používat značku <code>[FLASH]</code>.',

	'BUMP_INTERVAL'							=> 'Interval pro oživení',
	'BUMP_INTERVAL_EXPLAIN'			=> 'Doba od posledního příspěvku v&nbsp;tématu nutná pro možnost jeho oživení autorem. Pokud zvolíte 0, funkce se vypne.',
	'CHAR_LIMIT'								=> 'Maximální počet znaků v&nbsp;příspěvku',
	'CHAR_LIMIT_EXPLAIN'				=> 'Maximální počet znaků v&nbsp;příspěvku. Pokud je hodnota nastavena na 0, lze použít neomezený počet znaků.',
	'DELETE_TIME'								=> 'Omezení času pro smazaní příspěvku',
	'DELETE_TIME_EXPLAIN'				=> 'Omezí dobu, po kterou uživatel může smazat svůj příspěvek. Pokud je hodnota nastavena na 0, funkce je vypnuta.',
	'DISPLAY_LAST_EDITED'				=> 'Zobrazit informace o&nbsp;poslední úpravě',
	'DISPLAY_LAST_EDITED_EXPLAIN'	=> 'Zvolte, má-li se pod příspěvkem zobrazovat informace o&nbsp;jeho poslední úpravě.',
	'EDIT_TIME'									=> 'Omezení času pro úpravu',
	'EDIT_TIME_EXPLAIN'					=> 'Omezuje dobu, po kterou je možno upravovat nový příspěvek. Pokud je hodnota nastavena na 0, funkce je vypnuta.',
	'FLOOD_INTERVAL'						=> 'Ochranný interval',
	'FLOOD_INTERVAL_EXPLAIN'		=> 'Počet sekund, po jehož uplynutí může uživatel odeslat další příspěvek. Pokud je hodnota nastavena na 0, funkce je vypnuta.',
	'HOT_THRESHOLD'							=> 'Počet příspěvků v&nbsp;oblíbených tématech',
	'HOT_THRESHOLD_EXPLAIN'			=> 'Určete minimální počet příspěvků, při němž se téma automaticky označí jako oblíbené. Pokud je hodnota nastavena na 0, funkce je vypnuta.',
	'MAX_POLL_OPTIONS'						=> 'Počet možností v&nbsp;hlasování',
	'MAX_POST_FONT_SIZE'					=> 'Maximální možná velikost písma',
	'MAX_POST_FONT_SIZE_EXPLAIN'	=> 'Maximální velikost písma použitelná v&nbsp;příspěvku. Nastavte na 0 pro neomezenou velikost.',
	'MAX_POST_IMG_HEIGHT'					=> 'Maximální výška obrázku v&nbsp;příspěvku',
	'MAX_POST_IMG_HEIGHT_EXPLAIN'	=> 'Maximální výška obrázku použitelná v&nbsp;příspěvku. Nastavte na 0 pro neomezenou velikost.',
	'MAX_POST_IMG_WIDTH'					=> 'Maximální šířka obrázku v&nbsp;příspěvku',
	'MAX_POST_IMG_WIDTH_EXPLAIN'	=> 'Maximální šířka obrázku použitelná v&nbsp;příspěvku. Nastavte na 0 pro neomezenou velikost.',
	'MAX_POST_URLS'								=> 'Maximální počet odkazů na příspěvek',
	'MAX_POST_URLS_EXPLAIN'				=> 'Nejvyšší povolený počet odkazů v&nbsp;příspěvku. Nastavte na 0 pro neomezený počet.',
	'MIN_CHAR_LIMIT'							=> 'Minimum znaků ve zprávě',
	'MIN_CHAR_LIMIT_EXPLAIN'			=> 'Uživatelé nesmí zadat méně než tento počet znaků ve zprávě, aby byla odeslána. Toto nastavení je vhodné k omezení spamových a jednoslovných zpráv. Minimální hodnota nastavení je 1 znak.',
	'POSTING'											=> 'Přispívání',
	'POSTS_PER_PAGE'							=> 'Počet příspěvků na stránku',
	'QUOTE_DEPTH_LIMIT'						=> 'Maximum vnořených citací',
	'QUOTE_DEPTH_LIMIT_EXPLAIN'		=> 'Maximální počet citací, které mohou být vnořeny do sebe. 0 znamená neomezenou hloubku.',
	'SMILIES_LIMIT'								=> 'Maximální počet smajlíků na příspěvek',
	'SMILIES_LIMIT_EXPLAIN'				=> 'Maximální počet smajlíků, které lze v&nbsp;příspěvku použít. Pokud je hodnota nastavena na 0, jejich počet není omezen.',
	'SMILIES_PER_PAGE'						=> 'Počet smajlíků na stránku',
	'TOPICS_PER_PAGE'							=> 'Počet témat na stránku',
));

// Signature Settings
$lang = array_merge($lang, array(
	'ACP_SIGNATURE_SETTINGS_EXPLAIN'	=> 'Zde definujete výchozí nastavení podpisů',

	'MAX_SIG_FONT_SIZE'					=> 'Maximální velikost písma v&nbsp;podpisech',
	'MAX_SIG_FONT_SIZE_EXPLAIN'	=> 'Maximální povolená velikost písma v&nbsp;podpisech. Pro neomezenou velikost nastavte 0.',
	'MAX_SIG_IMG_HEIGHT'				=> 'Maximální výška obrázku v&nbsp;podpisu.',
	'MAX_SIG_IMG_HEIGHT_EXPLAIN'=> 'Maximální výška obrázku/flashe v&nbsp;uživatelském podpisu. Pro neomezenou velikost nastavte 0.',
	'MAX_SIG_IMG_WIDTH'					=> 'Maximální šířka obrázku v&nbsp;podpisu.',
	'MAX_SIG_IMG_WIDTH_EXPLAIN'	=> 'Maximální šířka obrázku/flashe v&nbsp;uživatelském podpisu. Pro neomezenou velikost nastavte 0.',
	'MAX_SIG_LENGTH'						=> 'Maximální délka podpisu',
	'MAX_SIG_LENGTH_EXPLAIN'		=> 'Maximální počet znaků v&nbsp;podpisu.',
	'MAX_SIG_SMILIES'						=> 'Maximální počet smajlíků v&nbsp;podpisu.',
	'MAX_SIG_SMILIES_EXPLAIN'		=> 'Maximální počet smajlíků povolený v&nbsp;uživatelském podpisu. Pro neomezený počet nastavte 0.',
	'MAX_SIG_URLS'							=> 'Maximální počet odkazů',
	'MAX_SIG_URLS_EXPLAIN'			=> 'Maximální počet povolených odkazů v&nbsp;podpisu. Pro neomezený počet nastavte 0.',
));

// Registration Settings
$lang = array_merge($lang, array(
	'ACP_REGISTER_SETTINGS_EXPLAIN'		=> 'Zde můžete definovat způsob registrace a nastavení spjatá s&nbsp;uživatelskými profily.',

	'ACC_ACTIVATION'					=> 'Aktivace účtu',
	'ACC_ACTIVATION_EXPLAIN'	=> 'Určuje, mají-li uživatelé okamžitý přístup k&nbsp;fóru, nebo musí registraci účtu nejprve potvrdit. Lze také zcela zakázat nové registrace.',
	'ACC_ADMIN'								=> 'Administrátorem',
	'ACC_DISABLE'							=> 'Vypnout registrace',
	'ACC_NONE'								=> 'Bez aktivace (okamžitý přístup)',
	'ACC_USER'								=> 'Uživatelem (ověření e-mailem)',
//	'ACC_USER_ADMIN'			=> 'Uživatelem a Administrátorem',
	'ALLOW_EMAIL_REUSE'			=> 'Povolit použití shodné e-mailové adresy',
	'ALLOW_EMAIL_REUSE_EXPLAIN'	=> 'Různí uživatelé se mohou registrovat se stejnou e-mailovou adresou.',
	'COPPA'										=> 'COPPA',
	'COPPA_FAX'								=> 'COPPA číslo faxu',
	'COPPA_MAIL'							=> 'COPPA e-mailová adresa',
	'COPPA_MAIL_EXPLAIN'			=> 'Poštovní adresa, na níž budou rodiče zasílat registrační formulář COPPA.',
	'ENABLE_COPPA'						=> 'Povolit COPPA',
	'ENABLE_COPPA_EXPLAIN'		=> 'Vyžaduje od uživatelů stvrzení, že jsou starší 13ti let, jak ukládá U.S. COPPA Act (Vyhláška USA o&nbsp;ochraně mládeže na internetu). Je-li funkce vypnuta, předdefinovaná skupina COPPA nebude použita.',
	'MAX_CHARS'								=> 'Max',
	'MIN_CHARS'								=> 'Min',
	
	'NEW_MEMBER_POST_LIMIT'			=> 'Počet příspěvků pro nové uživatele',
	'NEW_MEMBER_POST_LIMIT_EXPLAIN'	=> 'Nově registrovaní členové fóra jsou ve skupině <em>Noví členové fóra</em>, dokud nedosáhnou tohoto počtu příspěvků. Můžete tuto skupinu použít pro omezení posílání soukromých zpráv nebo vynucení schválení příspěvků. <strong>Pokud je počet nastaven na 0, uživatelé budou automaticky plnohodnotnými členy fóra.</strong>',
	'NEW_MEMBER_GROUP_DEFAULT'		=> 'Nastavit skupinu Noví členové fóra jako výchozí',
	'NEW_MEMBER_GROUP_DEFAULT_EXPLAIN'	=> 'Pokud toto povolíte a uživatel nedosáhne nutného počtu příspěvků pro vystoupení ze skupiny <em>Noví členové fóra</em>, bude navíc tato skupina nastavena jako jejich výchozí. To se hodí, pokud chcete tyto uživatele odlišit například hodností nebo barvou.',
	
	'NO_AUTH_PLUGIN'					=> 'Nebyl nalezen použitelný plugin pro autorizaci.',
	'PASSWORD_LENGTH'					=> 'Délka hesla',
	'PASSWORD_LENGTH_EXPLAIN'	=> 'Minimální a maximální délka hesla.',
	'REG_LIMIT'								=> 'Počet pokusů o&nbsp;registraci',
	'REG_LIMIT_EXPLAIN'				=> 'Počet pokusů o&nbsp;překonání proti-spamové ochrany před dočasným znepřístupněním registrace.',
	'USERNAME_ALPHA_ONLY'			=> 'Pouze alfanumerické',
	'USERNAME_ALPHA_SPACERS'	=> 'Alfanumerické a oddělovače',
	'USERNAME_ASCII'					=> 'ASCII (bez mezinárodních znaků)',
	'USERNAME_LETTER_NUM'			=> 'Jakékoliv písmeno a číslo',
	'USERNAME_LETTER_NUM_SPACERS'	=> 'Libovolná čísla, písmena a oddělovače',
	'USERNAME_CHARS'					=> 'Omezit znaky v&nbsp;uživatelských jménech',
	'USERNAME_CHARS_ANY'			=> 'Jakýkoliv znak',
	'USERNAME_CHARS_EXPLAIN'	=> 'Omezit znaky, které lze použít v&nbsp;uživatelském jménu. Oddělovače: mezera, -, +, _, [ a ]',
	'USERNAME_LENGTH'					=> 'Délka uživatelského jména',
	'USERNAME_LENGTH_EXPLAIN'	=> 'Minimální a maximální počet znaků v&nbsp;uživatelském jméně.',
));

// Feeds
$lang = array_merge($lang, array(
	'ACP_FEED_MANAGEMENT'				=> 'Obecné nastavení exportů ATOM',
	'ACP_FEED_MANAGEMENT_EXPLAIN'		=> 'Na této stránce můžete povolit a upravit různé exporty ATOM (vylepšené RSS), které dokáží zpracovat příspěvky i s BBCode a nabídnout je ke stažení uživatelům do jejich čteček.',

 	'ACP_FEED_GENERAL'							=> 'Nastavení obecného exportu',
 	'ACP_FEED_POST_BASED'					=> 'Nastavení exportu příspěvků',
 	'ACP_FEED_TOPIC_BASED'				=> 'Nastavení exportu témat',
 	'ACP_FEED_SETTINGS_OTHER'			=> 'Ostatní nastavení exportů',

	'ACP_FEED_ENABLE'					=> 'Povolit exporty',
	'ACP_FEED_ENABLE_EXPLAIN'			=> 'Povolí exporty ATOM pro celé fórum.<br />Pokud je toto vypnuté, na další nastavení už nebude brán ohled.',
	'ACP_FEED_LIMIT'					=> 'Počet položek',
	'ACP_FEED_LIMIT_EXPLAIN'			=> 'Počet položek k zobrazení v exportu.',

	'ACP_FEED_OVERALL'					=> 'Povolit export celého fóra',
 	'ACP_FEED_OVERALL_EXPLAIN'			=> 'Příspěvky z celého fóra.',
 	'ACP_FEED_FORUM'					=> 'Povolit exporty jednotlivých fór',
 	'ACP_FEED_FORUM_EXPLAIN'			=> 'Zobrazí nové příspěvky z jednotlivých fór.',
 	'ACP_FEED_TOPIC'					=> 'Povolit exporty z jednotlivých témat',
 	'ACP_FEED_TOPIC_EXPLAIN'			=> 'Zobrazí nové příspěvky z jednotlivých témat.',

	'ACP_FEED_TOPICS_NEW'				=> 'Povolit export nových témat',
 	'ACP_FEED_TOPICS_NEW_EXPLAIN'		=> 'Zobrazí poslední témata na stránkách, včetně jejich prvního příspěvku.',
 	'ACP_FEED_TOPICS_ACTIVE'			=> 'Povolit export aktivních témat',
 	'ACP_FEED_TOPICS_ACTIVE_EXPLAIN'	=> 'Zobrazí aktivní témata na stránkách, včetně jejich posledního příspěvku.',
 	'ACP_FEED_NEWS'						=> 'Export novinek',
 	'ACP_FEED_NEWS_EXPLAIN'				=> 'Vytáhne první příspěvek z těchto fór. Zvolte žádné fórum pro vypnutí této funkce.<br />Vybrat více for můžete podržením <samp>CTRL</samp> a naklikáváním.',

 	'ACP_FEED_OVERALL_FORUMS'			=> 'Povolí export seznamu fór',
 	'ACP_FEED_OVERALL_FORUMS_EXPLAIN'	=> 'Tento export obsahuje seznam všech fór na stránkách.',

 	'ACP_FEED_HTTP_AUTH'				=> 'HTTP přihlášení',
 	'ACP_FEED_HTTP_AUTH_EXPLAIN'		=> 'Umožní uživatelům se přihlásit k exportům přes HTTP přidáním parametru <samp>auth=http</samp> k URL exportu, což jim umožní zobrazit obsah skrytých sekcí, kam mají přístup. Některé instalace PHP vyžadují další úpravy konfigurace, v souboru .htaccess jsou další instrukce, pokud by vám funkce nefungovala.',

	'ACP_FEED_ITEM_STATISTICS'			=> 'Statistiky položek',
	'ACP_FEED_ITEM_STATISTICS_EXPLAIN'	=> 'Zobrazí jednotlivé statistiky každé položky<br />(autor, čas odeslání, počet odpovědí a zobrazení)',
	'ACP_FEED_EXCLUDE_ID'				=> 'Vyloučit tato fóra',
	'ACP_FEED_EXCLUDE_ID_EXPLAIN'		=> 'Obsah z těchto fór <strong>nebude zahrnut v exportech</strong>. Nevyberte žádné fórum pro použití všech.<br />Vyberte více fór podržením tlačítka <samp>CTRL</samp> a vybráním jednotlivých položek.',
));


// Visual Confirmation Settings
$lang = array_merge($lang, array(
	'ACP_VC_SETTINGS_EXPLAIN'				=> 'Zde si můžete vybrat a nastavit jednotlivé plug-iny, které různým způsobem implementují nástroje pro potlačení nechtěných příspěvků a registrací - spamu.',
	'AVAILABLE_CAPTCHAS'						=> 'Dostupné plug-iny',
	'CAPTCHA_UNAVAILABLE'						=> 'Plug-in nebyl nastaven, protože nebyly splněny jeho požadavky.',
	'CAPTCHA_GD'										=> 'GD CAPTCHA',
	'CAPTCHA_GD_3D'									=> 'GD 3D Captcha',
	
	'CAPTCHA_GD_FOREGROUND_NOISE'		=> 'GD CAPTCHA s&nbsp;šumem na popředí',
	'CAPTCHA_GD_EXPLAIN'						=> 'Použít GD pro vytvoření složitější CAPTCHA',
	'CAPTCHA_GD_FOREGROUND_NOISE_EXPLAIN'		=> 'Použít šum na popředí pro složitější CAPTCHA, pokud používáte GD',
	'CAPTCHA_GD_X_GRID'							=> 'GD CAPTCHA s&nbsp;šumem na pozadí v&nbsp;x-ové ose',
	'CAPTCHA_GD_X_GRID_EXPLAIN'			=> 'Použijte nižší hodnoty tohoto nastavení pro ztížení CAPTCHA. 0 vypne šum na x-ové ose.',
	'CAPTCHA_GD_Y_GRID'							=> 'GD CAPTCHA s&nbsp;šumem na pozadí v&nbsp;y-ové ose',
	'CAPTCHA_GD_Y_GRID_EXPLAIN'			=> 'Použijte nižší hodnoty tohoto nastavení pro ztížení CAPTCHA. 0 vypne šum na y-ové ose.',
	
	'CAPTCHA_GD_WAVE'								=> 'Vlnová deformace CAPTCHA',
	'CAPTCHA_GD_WAVE_EXPLAIN'				=> 'Tato možnost deformuje obrázek podle sinové vlny.',
	'CAPTCHA_GD_3D_NOISE'						=> 'Přidat 3D objekty jako šum',
	'CAPTCHA_GD_3D_NOISE_EXPLAIN'		=> 'Tato možnost přidá různé klamné objekty přes písmena.',
	'CAPTCHA_GD_FONTS'							=> 'Použít různé fonty',
	'CAPTCHA_GD_FONTS_EXPLAIN'			=> 'Toto nastavení určuje kolik různých tvarů písmen bude použito. Můžete použít standardní tvary nebo uvést své. Přidání malých písmen je také možné.',
	'CAPTCHA_FONT_DEFAULT'					=> 'Výchozí',
	'CAPTCHA_FONT_NEW'							=> 'Nové tvary',
	'CAPTCHA_FONT_LOWER'						=> 'Použít malá písmena',
	
	'CAPTCHA_NO_GD'									=> 'Jednoduchý obrázek',
	'CAPTCHA_PREVIEW_MSG'						=> 'Vaše změny v nastavení zatím nebyly uloženy, toto je pouze náhled.',
	'CAPTCHA_PREVIEW_EXPLAIN'				=> 'Náhled, jak by vypadal plug-in při současných nastaveních.',

	'CAPTCHA_SELECT'								=> 'Instalované plug-iny CAPTCHA',
	'CAPTCHA_SELECT_EXPLAIN'				=> 'Seznam obsahuje všechny plug-iny, které systém našel. Šedé položky nejsou dostupné v tuto chvíli a před použitím pravděpodobně budou potřebovat nastavit.',
	'CAPTCHA_CONFIGURE'							=> 'Nastavit CAPTCHA',
	'CAPTCHA_CONFIGURE_EXPLAIN'			=> 'Upravit nastavení pro vybranou CAPTCHA.',
	'CONFIGURE'											=> 'Nastavit',
	'CAPTCHA_NO_OPTIONS'						=> 'Tato CAPTCHA nemá žádné volitelné nastavení.',

	'VISUAL_CONFIRM_POST'						=> 'Povolit obranu proti spamu u anonymních příspěvků',
	'VISUAL_CONFIRM_POST_EXPLAIN'		=> 'Vyžaduje, aby anonymní návštěvníci při odeslání příspěvku vložili náhodně vygenerovaný kód nebo splnili jiný úkol pro obranu proti spamu.',
	'VISUAL_CONFIRM_REG'						=> 'Povolit obranu proti spamu při registraci',
	'VISUAL_CONFIRM_REG_EXPLAIN'		=> 'Vyžaduje, aby noví uživatelé při registraci vložili náhodně vygenerovaný kód nebo splnili jiný úkol pro obranu proti hromadným registracím.',
	'VISUAL_CONFIRM_REFRESH'				=> 'Povolit uživatelům obnovit obrázek CAPTCHA nebo získat nový úkol',
	'VISUAL_CONFIRM_REFRESH_EXPLAIN'	=> 'Povolí uživatelům vyžádat si nový obrázek nebo jiný úkol v případě, že jej nemůže vyluštit během registrace. Některé plug-iny toto nemusí podporvat.',
));

// Cookie Settings
$lang = array_merge($lang, array(
	'ACP_COOKIE_SETTINGS_EXPLAIN'		=> 'Zde lze definovat obsah dat obsažených v&nbsp;cookies odesíláných prohlížečům návštěvníků. Ve většině případů by výchozí hodnoty měly bez potíží vyhovovat. Pokud je však třeba některé upravit, buďte opatrní; jejich špatné nastavení může zabránit uživatelům v&nbsp;přihlášení.',

	'COOKIE_DOMAIN'				=> 'Doména cookie',
	'COOKIE_NAME'				=> 'Název cookie',
	'COOKIE_PATH'				=> 'Cesta ke cookie',
	'COOKIE_SECURE'				=> 'Zabezpečená cookie',
	'COOKIE_SECURE_EXPLAIN'		=> 'Pokud váš server běží výhradně přes SSL, aktivujte toto nastavení, v&nbsp;ostatních případech jej ponechte vypnuté. Je-li funkce zapnutá a stránky neběží přes SSL, budou se při přesměrování zobrazovat chyby.',
	'ONLINE_LENGTH'				=> 'Doba přítomnosti uživatele',
	'ONLINE_LENGTH_EXPLAIN'		=> 'Počet minut, po jejichž uplynutí budou neaktivní uživatelé vyřazeni ze seznamu uživatelů online. Čím je hodnota vyšší, tím náročnější je zpracování tohoto seznamu.',
	'SESSION_LENGTH'			=> 'Délka session',
	'SESSION_LENGTH_EXPLAIN'	=> 'Session vyprší po stanoveném počtu sekund.',
));

// Load Settings
$lang = array_merge($lang, array(
	'ACP_LOAD_SETTINGS_EXPLAIN'	=> 'Zde můžete povolit nebo zakázat určité funkce fóra, které mohou ovlivnit jeho vytížení. Na většině serverů není nutné jakékoliv funkce omezovat. Přesto na některých systémech nebo ve sdílených prostředích, jako jsou freehostingy, může být omezení nepotřebných funkcí užitečné. Lze také stanovit limity vytížení serveru a aktivních sessions, při kterých se fórum samo vypne.',

	'CUSTOM_PROFILE_FIELDS'			=> 'Vlastní pole profilu',
	'LIMIT_LOAD'					=> 'Omezit zatížení serveru',
	'LIMIT_LOAD_EXPLAIN'			=> 'Pokud průměr vytížení serveru za 1 minutu přesáhne tuto hodnotu, fórum se samo vypne. Hodnota 1.0 se rovná ~100% vytížení serveru, což funguje pouze na UNIXových serverech.',
	'LIMIT_SESSIONS'				=> 'Omezit sessions',
	'LIMIT_SESSIONS_EXPLAIN'		=> 'Pokud počet sessions za jednu minutu přesáhne tuto hodnotu, fórum se samo vypne. Nastavte na 0 pro neomezený počet sessions.',
	'LOAD_CPF_MEMBERLIST'			=> 'Zobrazit vlastní pole profilu v&nbsp;seznamu uživatelů',
	'LOAD_CPF_VIEWPROFILE'			=> 'Zobrazit vlastní pole profilu v&nbsp;jeho zobrazení',
	'LOAD_CPF_VIEWTOPIC'			=> 'Zobrazit vlastní pole profilu v&nbsp;přehledu témat',
	'LOAD_USER_ACTIVITY'			=> 'Zobrazit uživatelovu aktivitu',
	'LOAD_USER_ACTIVITY_EXPLAIN'	=> 'Zobrazí nejaktivnější téma/fórum v&nbsp;uživatelově profilu a v&nbsp;uživatelském panelu. Tuto vlastnost se doporučuje vypnout u&nbsp;fór s&nbsp;více jak miliónem příspěvků.',
	'RECOMPILE_STYLES'				=> 'Rekompilovat zastaralé součásti stylů',
	'RECOMPILE_STYLES_EXPLAIN'		=> 'Ověří, zda se na serveru nachází nové soubory součástí stylů a rekompiluje staré.',
	'YES_ANON_READ_MARKING'			=> 'Povolit označovaní témat pro návštěvníky',
	'YES_ANON_READ_MARKING_EXPLAIN'	=> 'Ukládá záznam o&nbsp;přečteném/nepřečteném fóru i pro neregistrované návštěvníky. Je-li tato funkce vypnuta, fóra budou vždy označena jako přečtená.',
	'YES_BIRTHDAYS'					=> 'Povolit výpis narozenin',
	'YES_BIRTHDAYS_EXPLAIN'			=> 'Je-li vypnut, nebude výpis narozenin na fóru zobrazen. Pro správnou funkčnost musí být narozeniny povoleny.',
	'YES_JUMPBOX'					=> 'Povolit zobrazení seznamu fór',
	'YES_MODERATORS'				=> 'Povolit zobrazení moderátorů u&nbsp;fór',
	'YES_ONLINE'					=> 'Povolit zobrazení online uživatelů',
	'YES_ONLINE_EXPLAIN'			=> 'Zobrazit stav online v&nbsp;obsahu fóra, v&nbsp;jednotlivých sekcích a v&nbsp;tématech.',
	'YES_ONLINE_GUESTS'				=> 'Povolit vypisování návštěvníků online',
	'YES_ONLINE_GUESTS_EXPLAIN'		=> 'Zobrazí v seznamu uživatelů online i anonymní uživatelé.',
	'YES_ONLINE_TRACK'				=> 'Povolit zobrazení stavu online/offline',
	'YES_ONLINE_TRACK_EXPLAIN'		=> 'Zobrazí uživatelův stav online/offline v&nbsp;profilu a v&nbsp;tématech.',
	'YES_POST_MARKING'				=> 'Povolit témata označená tečkou',
	'YES_POST_MARKING_EXPLAIN'		=> 'Tečka označí uživateli všechna témata, do nichž sám přispěl.',
	'YES_READ_MARKING'				=> 'Povolit ukládání označení nepřečtených fór/témat na serveru',
	'YES_READ_MARKING_EXPLAIN'		=> 'Ukládá stav fóra nebo tématu na serveru namísto v&nbsp;cookies uživatele.',
	'YES_UNREAD_SEARCH'            => 'Povolit hledání nepřečtených příspěvků',
));

// Auth settings
$lang = array_merge($lang, array(
	'ACP_AUTH_SETTINGS_EXPLAIN'	=> 'phpBB podporuje autentifikační plug-iny a moduly, které vám umožňují určit způsob ověření identity uživatelů při přihlášení. Ve výchozí instalaci jsou k&nbsp;dispozici tři plug-iny: DB, LDAP a Apache. Ne všechny metody vyžadují doplňující informace, vyplňte tedy jen ty, které se týkají daného plug-inu.',

	'AUTH_METHOD'				=> 'Zvolit způsob autentifikace',

	'APACHE_SETUP_BEFORE_USE'	=> 'Je třeba nastavit autentifikaci apache před zapnutím této možnosti v&nbsp;phpBB. Nezapomínejte, že uživatelské jméno v&nbsp;apache musí být stejné, jako používáte v&nbsp;phpBB.',

	'LDAP_DN'										=> 'Základní LDAP <var>dn</var>',
	'LDAP_DN_EXPLAIN'						=> 'Distinguished Name (DN) obsahuje všechny údaje o&nbsp;uživatelích, např. <samp>o=Moje firma,c=CZ</samp>.',
	'LDAP_EMAIL'								=> 'Položka e-mailu LDAP',
	'LDAP_EMAIL_EXPLAIN'				=> 'Toto nastavte na název položky e-mailu (pokud existuje) pro automatické přiřazení e-mailové adresy novým uživatelům. Zůstane-li položka prázdná, budou mít uživatelé po prvním přihlášení prázdné pole e-mailu.',
	'LDAP_INCORRECT_USER_PASSWORD'	=> 'Připojení k&nbsp;LDAP serveru se nezdařilo se zadanými údaji.',
	'LDAP_NO_EMAIL'							=> 'Uvedený e-mail neexistuje.',
	'LDAP_NO_IDENTITY'					=> 'Nelze najít přihlašovací identitu pro %s',
	'LDAP_PASSWORD'							=> 'LDAP heslo',
	'LDAP_PASSWORD_EXPLAIN'			=> 'Nechte prázdné pro anonymní přístup. V ostatních případech vyplňte heslo pro zvoleného uživatele. <strong>VAROVÁNÍ:</strong> Heslo bude v&nbsp;databázi uloženo jako prostý text, tudíž dostupné všem, kdo do ní má přístup.',
	'LDAP_PORT'									=> 'Port serveru LDAP',
	'LDAP_PORT_EXPLAIN'					=> 'Můžete dle libosti určit port, který bude používán pro připojení k&nbsp;serveru LDAP namísto výchozího portu 389.',
	'LDAP_SERVER'								=> 'Název LDAP serveru',
	'LDAP_SERVER_EXPLAIN'				=> 'Používáte-li LDAP, jde o&nbsp;jméno či IP adresu serveru LDAP. Jako náhradu můžete určit URL jako ldap://hostname:port/.',
	'LDAP_UID'											=> 'LDAP <var>uid</var>',
	'LDAP_UID_EXPLAIN'							=> 'Toto je klíč, pod kterým se vyhledá přihlašovací identita, např. <var>uid</var>, <var>sn</var>, atd.',
	'LDAP_USER'											=> 'Uživatel LDAP <var>dn</var>',
	'LDAP_USER_EXPLAIN'							=> 'Ponechte nevyplněno pro použití anonymního připojení. Je-li vyplněno, phpBB použije uvedené význačné přihlašovací jméno, např. <samp>uid=Username,ou=MyUnit,o=MyCompany,c=US</samp>. Vyžadováno pro Servery Active Directory.',
	'LDAP_USER_FILTER'							=> 'Filtr uživatelů LDAP',
	'LDAP_USER_FILTER_EXPLAIN'			=> 'Dle libosti můžete dále omezovat vyhledávané objekty pomocí přídavných filtrů. Např. <samp>objectClass=posixGroup</samp> povede k&nbsp;užití <samp>(&amp;(uid=$username)(objectClass=posixGroup))</samp>.',
));

// Server Settings
$lang = array_merge($lang, array(
	'ACP_SERVER_SETTINGS_EXPLAIN'	=> 'Zde můžete změnit nastavení serveru a domény. Ujistěte se, že zadaná data jsou správná, chyby se projeví v&nbsp;odesílaní nesprávných e-mailů a znemožnění přístupu k&nbsp;některým částem fóra. Pamatujte, že název domény neobsahuje http://, ani jiný název protokolu. Číslo portu upravujte pouze tehdy, pokud jste si jisti, že váš server používá jiný port, jinak ponechte hodnotu 80, která je platná pro většinu serverů.',

	'ENABLE_GZIP'				=> 'Povolit GZip kompresi',
	'ENABLE_GZIP_EXPLAIN'		=> 'Generovaný obsah bude před odesláním uživateli zkomprimován. To může snížit objem přenesených dat, ale zvýší zatížení CPU na straně serveru i uživatele. Vyžaduje přítomnost povoleného rozšíření PHP zlib.',
	'FORCE_SERVER_VARS'			=> 'Vynutit nastavení URL serveru',
	'FORCE_SERVER_VARS_EXPLAIN'	=> 'Je-li zapnuto, zde zvolená nastavení budou upřednostněna před automaticky nalezenými hodnotami.',
	'ICONS_PATH'				=> 'Cesta k&nbsp;ikonám témat',
	'ICONS_PATH_EXPLAIN'		=> 'Cesta pod kořenovým adresářem phpBB, např. <samp>images/icons</samp>',
	'PATH_SETTINGS'				=> 'Nastavení cest k&nbsp;souborům',
	'RANKS_PATH'				=> 'Cesta k&nbsp;hodnostem',
	'RANKS_PATH_EXPLAIN'		=> 'Cesta pod základní složkou phpBB, např. <samp>images/ranks</samp>',
	'SCRIPT_PATH'				=> 'Cesta ke skriptům',
	'SCRIPT_PATH_EXPLAIN'		=> 'Cesta relativní od názvu domény, např. <samp>/phpBB3</samp>',
	'SERVER_NAME'				=> 'Název domény',
	'SERVER_NAME_EXPLAIN'		=> 'Název domény, na které tento server běží (například: <samp>www.domena.cz</samp>)',
	'SERVER_PORT'				=> 'Port serveru',
	'SERVER_PORT_EXPLAIN'		=> 'Port, na kterém běží váš server, (standardně 80, upravte pouze tehdy, pokud se liší).',
	'SERVER_PROTOCOL'			=> 'Protokol serveru',
	'SERVER_PROTOCOL_EXPLAIN'	=> 'Je použit jako serverový protokol, jsou-li tato nastavení preferována před výchozími. Pokud necháte pole prázdné nebo nejsou tato nastavení vynucena, protokol je určen nastavením zabezpečení cookies (<samp>http://</samp> nebo <samp>https://</samp>).',
	'SERVER_URL_SETTINGS'		=> 'Nastavení URL serveru',
	'SMILIES_PATH'				=> 'Cesta ke smajlíkům',
	'SMILIES_PATH_EXPLAIN'		=> 'Cesta pod kořenovým adresářem phpBB, např. <samp>images/smilies</samp>',
	'UPLOAD_ICONS_PATH'			=> 'Cesta k&nbsp;ukládání ikon pro skupiny příloh',
	'UPLOAD_ICONS_PATH_EXPLAIN'	=> 'Cesta pod kořenovým adresářem phpBB, např. <samp>images/upload_icons</samp>',
));

// Security Settings
$lang = array_merge($lang, array(
	'ACP_SECURITY_SETTINGS_EXPLAIN'		=> 'Zde můžete změnit nastavení týkající se sessions a přihlašování.',

	'ALL'													=> 'Celá',
	'ALLOW_AUTOLOGIN'							=> 'Povolit trvalá přihlášení', 
	'ALLOW_AUTOLOGIN_EXPLAIN'			=> 'Určuje, zda si uživatelé mohou zapnout automatické přihlašování při opakovaných návštěvách.', 
	'AUTOLOGIN_LENGTH'						=> 'Doba platnosti trvalého přihlášení (v dnech)', 
	'AUTOLOGIN_LENGTH_EXPLAIN'		=> 'Počet dní, po kterých jsou klíče trvalého přihlášení odstraněny, pokud je nastaveno na 0, nebudou mazány nikdy.', 
	'BROWSER_VALID'								=> 'Ověřit prohlížeč',
	'BROWSER_VALID_EXPLAIN'				=> 'Povoluje ověřování prohlížeče pro větší zabezpečení session.',
	'CHECK_DNSBL'									=> 'Zkontrolovat IP v&nbsp;databázi DNS Blackhole',
	'CHECK_DNSBL_EXPLAIN'					=> 'Je-li zapnuto, IP adresa uživatele je při registraci a přispívání kontrolována následujícími službami DNSBL: <a href="http://spamcop.net">spamcop.net</a> a <a href="http://www.spamhaus.org">www.spamhaus.org</a>. Vyhledání může chvíli trvat v&nbsp;závislosti na konfiguraci serveru. Jsou-li zaznamenána zpomalení nebo přichází příliš mnoho kladných hlášení, doporučuje se tuto kontrolu vypnout.',
	'CLASS_B'											=> 'A.B',
	'CLASS_C'											=> 'A.B.C',
	'EMAIL_CHECK_MX'							=> 'Zkontrolovat e-mail na platný záznam MX',
	'EMAIL_CHECK_MX_EXPLAIN'			=> 'Je-li povoleno, během změny e-mailu při registraci nebo v&nbsp;profilu bude doména adresy zkontrolována pro platný MX záznam.',
	'FORCE_PASS_CHANGE'						=> 'Vynucená změna hesla',
	'FORCE_PASS_CHANGE_EXPLAIN'		=> 'Vyžaduje, aby si uživatelé měnili heslo po stanoveném počtu dnů. Je-li nastaveno na 0, žádná změna nebude vyžadována.',
	'FORM_TIME_MAX'								=> 'Maximální limit k&nbsp;odeslání formuláře',
	'FORM_TIME_MAX_EXPLAIN'				=> 'Doba, v&nbsp;níž musí uživatel odeslat formulář. Hodnotou −1 funkci deaktivujete. Mějte na paměti, že po vypršení session bude formulář neplatný bez ohledu na toto nastavení.',
	'FORM_SID_GUESTS'							=> 'Připojit formulář k&nbsp;session návštěvníků',
	'FORM_SID_GUESTS_EXPLAIN'			=> 'Je-li funkce aktivní, bude formulář návštěvníkům vystaven výhradně pro aktivní session. S některými poskytovateli internetu však mohou nastat potíže, kvůli častějším změnám IP adres.',
	'FORWARDED_FOR_VALID'					=> 'Ověření hlavičky <var>X_FORWARDED_FOR</var>',
	'FORWARDED_FOR_VALID_EXPLAIN'	=> 'Uživatel zůstane přihlášený, pouze pokud bude souhlasit odeslaná hlavička <var>X_FORWARDED_FOR</var> s&nbsp;tou z minulého požadavku. Bany budou také kontrolovány podle IP adres v&nbsp;<var>X_FORWARDED_FOR</var>.',
	'IP_VALID'										=> 'Ověření IP pro session',
	'IP_VALID_EXPLAIN'						=> 'Určuje, jak velká část IP adresy se použije při ověření session; <samp>Celá</samp> porovná celou IP adresu, <samp>A.B.C</samp> první tři oktety – x.x.x, <samp>A.B</samp> první dva – x.x, <samp>Žádné</samp> vypne ověření.',
	'IP_LOGIN_LIMIT_MAX'			    => 'Maximální počet přihlášení na IP adresu',
	'IP_LOGIN_LIMIT_MAX_EXPLAIN'	=> 'Počet pokusů o přihlášení přes jednu IP adresu než se spustí ochrana proti spamu. Vložením 0 zabráníte spuštění ochrany proti spamu přes IP adresu.',
	'IP_LOGIN_LIMIT_TIME'			=> 'Vypršení času pro pokusy přihlášení přes IP',
	'IP_LOGIN_LIMIT_TIME_EXPLAIN'	=> 'Pokusy o přihlášení končí po uplynutí této doby.',
	'IP_LOGIN_LIMIT_USE_FORWARDED'	=> 'Omezit pokusy o přihlášení pomocí hlavičky <var>X_FORWARDED_FOR</var>',
	'IP_LOGIN_LIMIT_USE_FORWARDED_EXPLAIN'	=> 'Místo omezení počtu přihášení podle IP adresy, se použijí hodnoty z hlavičky <var>X_FORWARDED_FOR</var>. <br /><em><strong>Upozornění:</strong> Povolte jen v případě, že pracujete s proxy servery, které nastavují hlavičku <var>X_FORWARDED_FOR</var> na spolehlivé hodnoty.</em>',	  
  'MAX_LOGIN_ATTEMPTS'					=> 'Maximální počet pokusů o&nbsp;přihlášení',
	'MAX_LOGIN_ATTEMPTS_EXPLAIN'	=> 'Pokud uživatel překročí zadaný počet pokusů o&nbsp;přihlášení, bude při každém dalším pokusu požádán o&nbsp;potvrzení ověřovacího kódu.',
	'NO_IP_VALIDATION'						=> 'Žádné',
	'NO_REF_VALIDATION'						=> 'Žádné',
	'PASSWORD_TYPE'								=> 'Složitost hesla',
	'PASSWORD_TYPE_EXPLAIN'				=> 'Určuje, jak složité musí být heslo při vytvoření nebo změně účtu. Každá možnost splňuje podmínky předcházejících voleb.',
	'PASS_TYPE_ALPHA'							=> 'Musí obsahovat alfanumerické znaky',
	'PASS_TYPE_ANY'								=> 'Bez požadavků',
	'PASS_TYPE_CASE'							=> 'Velká i malá písmena',
	'PASS_TYPE_SYMBOL'						=> 'Musí obsahovat zvláštní znaky',
	'REF_HOST'										=> 'Ověřit pouze hostitele',
	'REF_PATH'										=> 'Ověřit i cestu',
	'REFERER_VALID'								=> 'Ověřit referer',
	'REFERER_VALID_EXPLAIN'				=> 'Pokud je toto nastavení povoleno, bude porovnán referer u POST dotazů s hodnotami domény/cesty v nastavení. Toto může způsobit potíže na fórech, které používají více domén nebo externí přihlašování..',
	'TPL_ALLOW_PHP'								=> 'Povolit PHP v&nbsp;šablonách',
	'TPL_ALLOW_PHP_EXPLAIN'				=> 'Je-li tato možnost povolena, příkazy <code>PHP</code> a <code>INCLUDEPHP</code> budou rozeznávány a zpracovány v&nbsp;šablonách.',
));

// Email Settings
$lang = array_merge($lang, array(
	'ACP_EMAIL_SETTINGS_EXPLAIN'	=> 'Tyto údaje jsou použity při odesílání e-mailů uživatelům z tohoto fóra. Ujistěte se, že vámi zadaná e-mailová adresa je platná, všechny vrácené nebo nedoručitelné zprávy budou pravděpodobně doručeny na tento e-mail. Pokud váš poskytovatel neumožňuje použití nativních PHP funkcí pro e-mailové služby, můžete posílat e-maily přímo přes SMTP. To vyžaduje zadání názvu odpovídajícího serveru (v případě nutnosti se informujte u&nbsp;poskytovatele), neuvádějte zde žádné zastaralé nebo neplatné jméno! Pokud server vyžaduje přihlášení (a jen pouze pokud ano), vložte potřebné uživatelské jméno a heslo.',

	'ADMIN_EMAIL'					=> 'Zpětná e-mailová adresa',
	'ADMIN_EMAIL_EXPLAIN'			=> 'Tato adresa bude použita jako zpáteční adresa pro všechny e-maily, uvedena jako technická e-mailová adresa. Bude vždy použita jako adresa pro hlavičky <samp>Return-Path</samp> a <samp>Sender</samp>.',
	'BOARD_EMAIL_FORM'				=> 'Uživatelé posílají zprávy přes fórum',
	'BOARD_EMAIL_FORM_EXPLAIN'		=> 'Místo zobrazení e-mailu je uživatelům nabídnut formulář pro odeslání e-mailu přes fórum.',
	'BOARD_HIDE_EMAILS'				=> 'Skrýt e-mailové adresy',
	'BOARD_HIDE_EMAILS_EXPLAIN'		=> 'Je-li povoleno, všechny e-mailové adresy na fóru budou skryty.',
	'CONTACT_EMAIL'					=> 'Kontaktní e-mailová adresa',
	'CONTACT_EMAIL_EXPLAIN'			=> 'Tato adresa bude použita vždy, když je potřeba uvést kontakt na fórum, např. spam, výstupy chyb atd. Bude vždy použita jako adresa pro hlavičky <samp>From</samp> a <samp>Reply-To</samp>.',
	'EMAIL_FUNCTION_NAME'			=> 'Název funkce e-mail',
	'EMAIL_FUNCTION_NAME_EXPLAIN'	=> 'Název PHP funkce použité pro odeslání e-mailů.',
	'EMAIL_PACKAGE_SIZE'			=> 'Velikost balíku e-mailů',
	'EMAIL_PACKAGE_SIZE_EXPLAIN'	=> 'Jedná se o&nbsp;počet e-mailů, které jsou odeslány naráz (tj. v&nbsp;jednom balíku). Nastavte tuto hodnotu na 0, pokud se vám nedoručují e-maily s&nbsp;upozorněními.',
	'EMAIL_SIG'						=> 'Podpis e-mailu',
	'EMAIL_SIG_EXPLAIN'				=> 'Tento text bude připojen ke všem e-mailům odeslaným přes fórum.',

	'ENABLE_EMAIL'					=> 'Povolit e-maily na fóru',
	'ENABLE_EMAIL_EXPLAIN'			=> 'Je-li tato funkce zakázána, nebudou přístupné žádné funkce používající e-maily na fóru. <em>Berte na vědomí, že aktivace účtů uživatelem nebo administrátorem tuto funkci vyžadují. Pokud máte tento druh aktivace zapnutý a vypnete e-maily, účty už nebudou muset být aktivovány a budou ihned ověřené.</em>',

	'SMTP_AUTH_METHOD'				=> 'Způsob autentifikace pro SMTP',
	'SMTP_AUTH_METHOD_EXPLAIN'		=> 'Používá se jen, když je zadáno uživatelské jméno/heslo. Obraťte se na vašeho poskytovatele, pokud si nejste jisti nastavením.',
	'SMTP_CRAM_MD5'					=> 'CRAM-MD5',
	'SMTP_DIGEST_MD5'				=> 'DIGEST-MD5',
	'SMTP_LOGIN'					=> 'LOGIN',
	'SMTP_PASSWORD'					=> 'Heslo SMTP',
	'SMTP_PASSWORD_EXPLAIN'			=> 'Zadejte heslo k&nbsp;SMTP serveru, pouze pokud ho server vyžaduje. <em><strong>Varování:</strong> heslo bude uloženo nezašifrované v databázi.</em>',
	'SMTP_PLAIN'					=> 'PLAIN',
	'SMTP_POP_BEFORE_SMTP'			=> 'POP-BEFORE-SMTP',
	'SMTP_PORT'						=> 'Port SMTP serveru',
	'SMTP_PORT_EXPLAIN'				=> 'Změňte pouze tehdy, pokud SMTP server používá jiné.',
	'SMTP_SERVER'					=> 'Adresa SMTP serveru',
	'SMTP_SETTINGS'					=> 'Nastavení SMTP',
	'SMTP_USERNAME'					=> 'Uživatelské jméno SMTP',
	'SMTP_USERNAME_EXPLAIN'			=> 'Zadejte uživatelské jméno k&nbsp;SMTP serveru, pouze pokud ho server vyžaduje.',
	'USE_SMTP'						=> 'Použít SMTP server pro odesílání',
	'USE_SMTP_EXPLAIN'				=> 'Zvolte „Ano“, pokud chcete, aby byly e-maily odeslány přes označený server místo serverové funkce e-mail.',
));

// Jabber settings
$lang = array_merge($lang, array(
	'ACP_JABBER_SETTINGS_EXPLAIN'	=> 'Zde můžete povolit a nastavit používaní Jabber pro krátké zprávy a oznámení na fóru. Jabber je opensource protokol a tudíž ho může používat kdokoliv. Některé Jabber servery obsahují brány a transporty, které umožňují kontaktovat uživatele na jiných sítích, některé servery ale transporty nepodporují nebo mohou přestat fungovat při změně protokolu. Berte na vědomí, že změna údajů může trvat několik sekund, nepřerušujte skript, dokud nebude úspěšně vykonaný!',
	
	'JAB_ENABLE'						=> 'Povolit Jabber',
	'JAB_ENABLE_EXPLAIN'		=> 'Umožňuje použít Jabber pro odesílání zpráv a oznámení.',
	'JAB_GTALK_NOTE'				=> 'Mějte prosím na paměti, že GTalk nebude fungovat, protože funkce <samp>dns_get_record</samp> nebyla nalezena. Tato funkce není v&nbsp;PHP4 dostupná ani implementovaná na platformě Windows. Současně nefunguje na systémech, pracujících na bázi BSD, včetně operačního systému MacOS.',
	'JAB_PACKAGE_SIZE'			=> 'Velikost balíku zpráv Jabber',
	'JAB_PACKAGE_SIZE_EXPLAIN'	=> 'Počet zpráv, které jsou odeslány naráz. Je-li nastaveno 0, zprávy budou odeslány ihned a nebudou řazeny do fronty.',
	'JAB_PASSWORD'				=> 'Heslo pro Jabber',
	'JAB_PASSWORD_EXPLAIN'			=> '<em><strong>Varování:</strong> heslo bude uloženo nezašifrované v databázi.</em>',
	'JAB_PORT'					=> 'Jabber port',
	'JAB_PORT_EXPLAIN'			=> 'Ponechte prázdné (změňte pouze v&nbsp;případě, že je jiný než 5222).',
	'JAB_SERVER'				=> 'Jabber server',
	'JAB_SERVER_EXPLAIN'		=> 'Na %sjabber.org%s naleznete seznam serverů.',
	'JAB_SETTINGS_CHANGED'		=> 'Nastavení Jabberu bylo aktualizováno.',
	'JAB_USE_SSL'				=> 'Použít SSL při připojení',
	'JAB_USE_SSL_EXPLAIN'		=> 'Pokud povolíte tuto možnost, phpBB se pokusí navázat zabezpečené spojení. Port Jabberu bude nastaven na 5223, pokud je zvolen port 5222.',
	'JAB_USERNAME'				=> 'Uživatelské jméno nebo Jabber ID',
	'JAB_USERNAME_EXPLAIN'		=> 'Vložte uživatelské jméno nebo platné Jabber ID. Tato položka nebude kontrolována, jestli je platná. Pokud uvedete pouze uživatelské jméno (část před zavináčem), vaše Jabber ID bude toto jméno dohromady se serverem, který jste uvedli výše. Další možnost je uvést kompletní JID, např. uzivatel@jabber.org',
));

?>