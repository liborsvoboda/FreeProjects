<?php
/** 
*
* captcha_recaptcha [Čeština]
*
* @package language
* @version $Id$
* @copyright (c)  2009 phpBB.cz
* @license http://opensource.org/licenses/gpl-license.php GNU Public License 
* Original copyright: (c) 2009 phpBB Group
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
	'RECAPTCHA_LANG'				=> 'cs',
	'RECAPTCHA_NOT_AVAILABLE'		=> 'Pro použití reCaptcha, musíte se registrovat na <a href="http://www.google.com/recaptcha">www.google.com/recaptcha</a>.',
	'CAPTCHA_RECAPTCHA'				=> 'reCaptcha',
	'RECAPTCHA_INCORRECT'			=> 'Ověřovací kód, který jste zadali nebyl správně.',

	'RECAPTCHA_PUBLIC'				=> 'Veřejný reCaptcha klíč',
	'RECAPTCHA_PUBLIC_EXPLAIN'		=> 'Váš veřejný klíč k službě reCaptcha, získáte ho na <a href="http://www.google.com/recaptcha">www.google.com/recaptcha</a>.',
	'RECAPTCHA_PRIVATE'				=> 'Soukromý reCaptcha klíč',
	'RECAPTCHA_PRIVATE_EXPLAIN'		=> 'Váš soukromý klíč k službě reCaptcha, získáte ho na <a href="http://www.google.com/recaptcha">www.google.com/recaptcha</a>.',

	'RECAPTCHA_EXPLAIN'				=> 'Snažíme se omezit automatizované registrace na našem serveru, z toho důvodu je nutné, abyste napsali následující dvě slova do rámečku pod nimi.',
));

?>