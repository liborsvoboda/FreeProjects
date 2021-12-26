<?php
/** 
*
* help_bbcode.php [Čeština]
*
* @package language
* @version $Id: help_bbcode.php 492 2009-09-14 13:28:16Z ameeck $
* @copyright (c)  2007 phpBB.cz
* @license http://opensource.org/licenses/gpl-license.php GNU Public License 
* Original copyright: (c) 2005 phpBB Group
*
*/

/**
*/
if (!defined('IN_PHPBB'))
{
   exit;
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

$help = array(
	array(
		0	=> '--',
		1	=> 'Úvod',
	),
	array(
		0	=> 'Co je BBCode',
		1	=> 'BBCode je jednoduchá implementace HTML. O tom, jestli můžete nebo nemůžete používat BBCode ve vašich příspěvcích rozhoduje administrátor. Navíc můžete sami vypnout zpracování BBCode jednotlivě u&nbsp;každého příspěvku. BBCode je sám o&nbsp;sobě velmi podobný HTML, značky jsou uzavřeny v&nbsp;hranatých závorkách [ a ] narozdíl od HTML, kde jsou použity &lt; a &gt; a dává vám velké možnosti ovlivnit to, jak bude a je příspěvek zobrazován. Záleží také na zvoleném vzhledu fóra, jestli se vám zobrazí jednoduché klikací menu, pomocí kterého můžete velmi jednoduše aplikovat styly. I přesto můžete tento průvodce shledat užitečným.',
	),
	array(
		0	=> '--',
		1	=> 'Formátování textu',
	),
	array(
		0	=> 'Jak vytvořit tučný nebo podtržený text a kurzívu',
		1	=> 'BBCode obsahuje značky pro základní formátování textu. Ty můžete použít takto: <ul><li>Pro ztučnění textu ho vložte mezi značky <strong>[b][/b]</strong>, např. <br /><br />Z <strong>[b]</strong>Ahoj<strong>[/b]</strong><br /><br />se stane <strong>Ahoj</strong></li><li>Pro podtržení použijte <strong>[u][/u]</strong>, např.:<br /><br />Z <strong>[u]</strong>Dobré ráno<strong>[/u]</strong><br /><br />se stane <u>Dobré ráno</u></li><li>Pro aplikaci kurzívy použijte <strong>[i][/i]</strong>, např.<br /><br />Toto je <strong>[i]</strong>Skvělé!<strong>[/i]</strong><br /><br />bude Toto je <i>Skvělé!</i></li></ul>',
	),
	array(
		0	=> 'Jak změnit barvu a velikost textu',
		1	=> 'BBCode umožňuje také upravit barvu a velikost textu. Mějte na paměti, že zobrazení velikosti závisí na jednotlivých prohlížečích: <ul><li>Změna barvy textu je dosažena jeho obalením tagem <strong>[color=][/color]</strong>. Můžete buďto použít jednoduché (anglické) názvy barev (např. red, blue, yellow, etc.) anebo hexadecimální zápis, např. #FFFFFF, #000000. Pro ukázku, pro vytvoření červeného textu můžete použít:<br /><br /><strong>[color=red]</strong>Nazdar!<strong>[/color]</strong><br /><br />nebo<br /><br /><strong>[color=#FF0000]</strong>Nazdar!<strong>[/color]</strong><br /><br />Oba dva způsoby budou mít ve výsledku tuto podobu: <span style="color:red">Nazdar!</span></li><li>Změna velikosti textu je dosažena podobným způsobem, pomocí <strong>[size=][/size]</strong>. Velikost se určuje v procentech a administrátor může zvolit maximální přípustnou hodnotu velikosti písma. Například:<br /><br /><strong>[size=75]</strong>Drobný text<strong>[/size]</strong><br /><br />bude <span style="font-size: 75%; line-height: 116%;">Drobný text</span><br /><br />kdežto:<br /><br /><strong>[size=150]</strong>Zvětšný text<strong>[/size]</strong><br /><br />bude <span style="font-size: 150%; line-height: 116%;">Zvětšený text</span></li></ul>',
	),
	array(
		0	=> 'Mohu kombinovat formátovací značky?',
		1	=> 'Samozřejmě, že ano, například, pokud chcete upoutat pozornost, můžete napsat:<br /><br /><strong>[size=18][color=red][b]</strong>KOUKNĚTE NA MĚ!<strong>[/b][/color][/size]</strong><br /><br />a výsledek bude <span style="color:red;font-size:18px;"><strong>KOUKNĚTE NA MĚ!</strong></span><br /><br />Nedoporučujeme ale, aby většina vašich příspěvků takto vypadala! Mějte na vědomí, že je na vás, aby byly značky správně zakončeny. Například následující příklad je špatně:<br /><br /><strong>[b][u]</strong>Toto je špatně!<strong>[/b][/u]</strong>',
	),
	array(
		0	=> '--',
		1	=> 'Citace a text s&nbsp;pevnou šířkou',
	),
	array(
		0	=> 'Citování příspěvků v&nbsp;odpovědi',
		1	=> 'Jsou dva způsoby citace, se zdrojem nebo bez.<ul><li>Pokud použijete funkci Citace při psaní odpovědi, všimněte si, že citovaný text je uzavřen ve značkách <strong>[quote=""][/quote]</strong>. Tento způsob vám umožňuje připsat koho citujete, např. pro citaci Pana Brambory byste zadali:<br /><br /><strong>[quote="p. Brambora"]</strong>Text, který napsal pan Brambora bude zde<strong>[/quote]</strong><br /><br />Výsledná výstup by přidal p. Brambora napsal: před samotný text. Pamatujte, že <strong>musíte</strong> přidat uvozovky "" kolem jména, které citujete, jsou povinné.</li><li>Druhá metoda umožnuje citovat něco jen naslepo. Pro zobrazení takovéto citace původní text vložte mezi značky <strong>[quote][/quote]</strong>. Když zobrazíte tuto zprávu, výsledek bude jen Citace: před samotným textem.</li></ul>',
	),
	array(
		0	=> 'Kód nebo text s&nbsp;pevně stanovenou šířkou',
		1	=> 'Pokud chcete mít výstup nějakého kódu, nebo de facto čehokoliv co potřebuje mít pevně danou šířku (font typu Courier), vložte tento text mezi značky<strong>[code][/code]</strong>, např.<br /><br /><strong>[code]</strong>echo "Toto je kus kódu";<strong>[/code]</strong><br /><br />Všechna formátování uskutečněná mezi <strong>[code][/code]</strong> se nebudou zpracovávat. PHP kód může být zvýrazněn pomocí značek code takto: <strong>[code=php][/code]</strong>, při odesílání PHP kódu je vhodné použít tuto možnost.',
	),
	array(
		0	=> '--',
		1	=> 'Vytváření seznamů',
	),
	array(
		0	=> 'Vytvoření jednoduchého seznamu',
		1	=> 'BBCode podporuje dva druhy seznamů, číslovaný a nečíslovaný(odrážkovaný). Jsou stejné jako jejich protějšky v&nbsp;HTML. Nečíslovaný seznam zobrazí položky v&nbsp;řadě pod sebou uvozené odrážkou. Pro vytvoření takovéhoto seznamu se používá <strong>[list][/list]</strong> a každou položku seznamu uveďte značkou <strong>[*]</strong>. Např. pro seznam vašich nejoblíbenějších barev použijete:<br /><br /><strong>[list]</strong><br /><strong>[*]</strong>červená<br /><strong>[*]</strong>modrá<br /><strong>[*]</strong>žlutá<br /><strong>[/list]</strong><br /><br />Toto vytvoří následující seznam:<ul><li>červená</li><li>modrá</li><li>žlutá</li></ul>',
	),
	array(
		0	=> 'Vytvoření číslovaného seznamu',
		1	=> 'Druhý typ seznamu vám dává možnost určit co bude zobrazeno před jednotlivými položkami. Pro vytvoření takovéhoto seznamu použijte značky <strong>[list=1][/list]</strong>(číslovaný seznam) nebo alternativně <strong>[list=a][/list]</strong> pro abecední seznam(a,b,c,d). Položky se uvozují stejnou značkou jako u&nbsp;nečíslovaného seznamu, tedy značkou <strong>[*]</strong>. Například:<br /><br /><strong>[list=1]</strong><br /><strong>[*]</strong>první možná odpověď<br /><strong>[*]</strong>druhá možná odpověď<br /><strong>[*]</strong>třetí možná odpověď<br /><strong>[/list]</strong><br /><br />vytvoří následující seznam: <ol style="list-style-type: decimal;"><li>první možná odpověď</li><li>druhá možná odpověď</li><li>třetí možná odpověď</li></ol>Abecední seznam bude vypadat úplně stejně, jenže čísla(1,2,3…) budou nahrazena písmeny(a,b,c…)',
	),
	// This block will switch the FAQ-Questions to the second template column
	array(
		0 => '--',
		1 => '--'
	),
	array(
		0	=> '--',
		1	=> 'Vytváření odkazů',
	),
	array(
		0	=> 'Odkazy na jinou stránku',
		1	=> 'BBCode umožňuje několik způsobu jak vytvořit odkazy v&nbsp;příspěvku<ul><li>Prvním z těchto způsobů je značka <strong>[url=][/url]</strong>, cokoliv zadáte za znak rovnítka(=) bude obsahem odkazu, takže například pro odkaz na stránku české podpory phpBB byste mohli použít:<br /><br /><strong>[url=http://www.phpbb.cz/]</strong>Navštivte phpBB.cz!<strong>[/url]</strong><br /><br />Toto vytvoří následující odkaz, <a href="http://www.phpbb.cz/">Navštivte phpBB.cz!</a> Odkaz se zobrazí buď ve stejném anebo v&nbsp;novém okně podle nastavení prohlížeče.</li><li>Pokud chcete, aby se odkaz zobrazil na fóru sám jak je, tak stačí použít zápis:<br /><br /><strong>[url]</strong>http://www.phpbb.com/<strong>[/url]</strong><br /><br />Toto vytvoří následující odkaz, <a href="http://www.phpbb.com/" target="_blank">http://www.phpbb.com/</a></li><li>phpBB obsahuje také funkci automatické doplnění URL, která přemění nedokončený odkaz v&nbsp;syntakticky správný(i klikatelný), odkaz nemusí být uzavřen v&nbsp;značkách url ani obsahovat uvozující http://. Například pokud napíšete www.phpbb.com ve vaší zprávě, phpBB to automaticky převede na <a href="http://www.phpbb.com/" target="_blank">www.phpbb.com</a>.</li><li>Stejné platí i pro e-mailové adresy, můžete buďto použít tento zápis:<br /><br /><strong>[email]</strong>nekdo@nekde.cz<strong>[/email]</strong><br /><br />, který bude ve výsledku vypadat takto: <a href="emailto:nekdo@nekde.cz">nekdo@nekde.cz</a> anebo můžete naspat jen nekdo@nekde.cz do vaši zprávy, a bude to automaticky převedeno do formy odkazu.</li></ul>Jako všechny BBCode značky můžete vkládat jiné značky do zápisu [url][/url] jako např.: <strong>[img][/img]</strong> (viz. další položku), <strong>[b][/b]</strong>, atd. U formátovacích značek je na vás dodržet správně pořadí uvozujících a ukončujících značek:<br /><br /><strong>[url=http://www.phpbb.com/][img]</strong>http://www.phpbb.com/images/phplogo.gif<strong>[/url][/img]</strong><br /><br /> <u>není</u> správně!',
	),
	array(
		0	=> '--',
		1	=> 'Zobrazení obrázků v&nbsp;příspěvku',
	),
	array(
		0	=> 'Přidání obrázku do příspěvku',
		1	=> 'phpBB BBCode obsahuje také možnost jak zobrazit obrázek přímo ve vašem příspěvku. Je potřeba si pamatovat dvě velmi důležité věci; mnoho uživatelů nemá rádo, když je v&nbsp;příspěvku obsaženo více obrázků, popř. v&nbsp;nerozumné velikosti a za druhé, obrázek musí být dostupný z internetu, nestačí aby byl uložen na vašem počítači. Obrázek lze také nahrát pomocí přílohy, pokud je administrátor povolí na tomto fóru. Pro zobrazení obrázku musíte vložit jeho url mezi značky <strong>[img][/img]</strong>. Např.:<br /><br /><strong>[img]</strong>http://www.phpbb.com/images/phplogo.gif<strong>[/img]</strong><br /><br />Můžete také vložit tento zápis do značek <strong>[url][/url]</strong>pokud si přejete, např.<br /><br /><strong>[url=http://www.phpbb.com/][img]</strong>http://www.phpbb.com/images/phplogo.gif<strong>[/img][/url]</strong><br /><br />bude ve výstupu:<br /><br /><a href="http://www.phpbb.com/"><img src="http://www.phpbb.com/images/phplogo.gif" border="0" alt="" /></a><br />',
	),
	array(
		0 => 'Přidání přílohy k&nbsp;příspěvku',
		1 => 'Přílohy mohou být nyní umístěny kdekoliv v&nbsp;příspěvku použitím nové <strong>[attachment=][/attachment]</strong> BBCode značky, pokud jsou přílohy povoleny správcem fóra&nbsp;&amp;&nbsp;pokud máte dostatečná oprávnění pro přidání přílohy.'
	),
	array(
		0	=> '--',
		1	=> 'Jiné dotazy',
	),
	array(
		0	=> 'Mohu vkládat vlastní značky?',
		1	=> 'Pokud jste administrátorem, tak přes administrační rozhraní můžete přidávat své vlastní BBCode značky, které budou následně použitelné na fóru.',
	),
);

?>