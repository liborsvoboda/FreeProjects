/*********************************************************************************************************/
/**
 * syntaxhighlight plugin for CKEditor 3.x for SyntaxHighlighter 3.0.83
 * Released: On 2011-06-24
 * Download: http://www.harrisonhills.org/techresources
 * Original plugin written by Lajox found at http://code.google.com/p/lajox
 */
/*********************************************************************************************************/

CKEDITOR.plugins.add('syntaxhighlight',   
  {    
    requires: ['dialog'],
	lang : ['en'], 
    init:function(a) { 
	var b="syntaxhighlight";
	var c=a.addCommand(b,new CKEDITOR.dialogCommand(b));
		c.modes={wysiwyg:1,source:0};
		c.canUndo=false;
	a.ui.addButton("syntaxhighlight",{
					label:a.lang.syntaxhighlight.title,
					command:b,
					icon:this.path+"images/syntaxhighlight.gif"
	});
	CKEDITOR.dialog.add(b,this.path+"dialogs/syntaxhighlight.js")}
});