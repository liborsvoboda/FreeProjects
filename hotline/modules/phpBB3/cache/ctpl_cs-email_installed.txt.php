<?php if (!defined('IN_PHPBB')) exit; ?>Subject: Nová instalace phpBB

Gratulujeme,

Úspěšně jste nainstalovali phpBB na váš server.

Tento email obsahuje důležité informace a vaší instalaci, prosíme uschovejte jej. Prosíme nezapomeňte, že vaše heslo je zašifrováno v databázi a nelze jej získat zpět, ale je možné si nechat poslat nové. 

----------------------------
Uživatelské jméno: <?php echo (isset($this->_rootref['USERNAME'])) ? $this->_rootref['USERNAME'] : ''; ?>

Adresa fóra:       <?php echo (isset($this->_rootref['U_BOARD'])) ? $this->_rootref['U_BOARD'] : ''; ?>
----------------------------

Užitečné informace o phpBB lze naleznout ve složce docs u vaší instalace, nebo na stránce podpory phpBB - http://www.phpbb.com/support/, případně české podpory - http://www.phpbb.cz

Doporučujeme sledovat novinky v nových verzích našeho systému pro zajištění bezpečnosti vašeho fóra, novinky vám můžeme zasílat automaticky, pokud se přihlásíte na mailing list, který je na výše uvedeném odkazu.

<?php echo (isset($this->_rootref['EMAIL_SIG'])) ? $this->_rootref['EMAIL_SIG'] : ''; ?>