/*
 * @file Background plugin for CKEditor
 * Copyright (C) 2011 Alfonso Martínez de Lizarrondo
 *
 * == BEGIN LICENSE ==
 *
 * Licensed under the terms of any of the following licenses at your
 * choice:
 *
 *  - GNU General Public License Version 2 or later (the "GPL")
 *    http://www.gnu.org/licenses/gpl.html
 *
 *  - GNU Lesser General Public License Version 2.1 or later (the "LGPL")
 *    http://www.gnu.org/licenses/lgpl.html
 *
 *  - Mozilla Public License Version 1.1 or later (the "MPL")
 *    http://www.mozilla.org/MPL/MPL-1.1.html
 *
 * == END LICENSE ==
 *
 */

// A placeholder just to notify that the plugin has been loaded
CKEDITOR.plugins.add( 'backgrounds',
{
	// Translations, available at the end of this file, without extra requests
	lang : [ 'en', 'es' ],

	init : function( editor )
	{
		// It doesn't add commands, buttons or dialogs, it doesn't do anything here
	} //Init
} );


// This is the real code of the plugin
CKEDITOR.on( 'dialogDefinition', function( ev )
	{
		// Take the dialog name and its definition from the event data.
		var dialogName = ev.data.name,
			dialogDefinition = ev.data.definition,
			editor = ev.editor,
			tabName = '';

		// Check if it's one of the dialogs that we want to modify and note the proper tab name.
		if ( dialogName == 'table' || dialogName == 'tableProperties' )
			tabName = 'advanced';

		if ( dialogName == 'cellProperties' )
			tabName = 'info';

		// Not one of the managed dialogs.
		if ( tabName == '' )
			return;

		// Get a reference to the tab.
		var tab = dialogDefinition.getContents( tabName );

		// The text field
		var textInput =  {
				type : 'text',
				label : editor.lang.backgrounds.label,
				id : 'background',
				setup : function( selectedElement )
				{
					this.setValue( selectedElement.getAttribute( 'background' ) );
				},
				commit : function( data, selectedElement )
				{
					var element = selectedElement || data,
						value = this.getValue();
					if ( value )
						element.setAttribute( 'background', value );
					else
						element.removeAttribute( 'background' );
				}
			};

		// File browser button
		var browseButton =  {
				type : 'button',
				id : 'browse',
				hidden : 'true',
				filebrowser :
				{
					action : 'Browse',
					target: tabName + ':background',
					url: editor.config.filebrowserImageBrowseUrl || editor.config.filebrowserBrowseUrl
				},
				label : editor.lang.common.browseServer
			};

		// Add the elements to the dialog
		if ( tabName == 'advanced')
		{
			// Two rows
			tab.add(textInput);
			tab.add(browseButton);
		}
		else
		{
			// In the cell dialog add it as a single row
			browseButton.style = 'display:inline-block;margin-top:10px;';
			tab.add({
					type : 'hbox',
					widths: [ '', '100px'],
					children : [ textInput, browseButton]
			});
		}

	// inject this listener before the one from the fileBrowser plugin so there are no problems with the new fields.
	}, null, null, 9 );


// Translations for the label
CKEDITOR.plugins.setLang( 'backgrounds', 'en', { backgrounds : { label	: 'Background image'} } );
CKEDITOR.plugins.setLang( 'backgrounds', 'es', { backgrounds : { label	: 'Imagen de fondo'	} } );