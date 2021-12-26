/*
 * Copyright (C) 2011 Alfonso Martínez de Lizarrondo
 *
 * 	Plugin for CKEditor to send the current data to the server so it can be previewed in a custom way
 *
 * File Author:
 * 		Alfonso Martínez de Lizarrondo
 *
 * Developed for Digital Impact, Flatbooster
 *
 * How to Use & Change log: docs/install.html
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

CKEDITOR.plugins.add( 'serverpreview',
{

	afterInit : function( editor )
	{
		var createInput = function(name, value)
		{
			var input = document.createElement('INPUT');
			input.type = 'hidden';

			input.name = name;
			input.value = value;
			return input;
		}

		// Override the default "preview" behavior
		var previewCmd = editor.getCommand( 'preview' );

		// This is the function that creates a temporary form, gets the data and sends it to the server
		previewCmd.exec = function()
		{
			// Get the form to submit the data (a custom one, not the real)
			var theForm = document.getElementById('serverPreviewForm');
			if (!theForm) {
				// If it doesn't exist, we create it here
				theForm = document.createElement('FORM');
				theForm.method = 'POST';
				theForm.name = 'serverPreviewForm';
				theForm.id = theForm.name;
				theForm.style.display = 'none';

				// This sets the default page where the data will be posted.
				theForm.action = editor.config.serverPreview_Url;

				// define target for the preview
				theForm.target = 'serverpreview';
				document.body.appendChild( theForm );
			}

			// Clear previous data
			theForm.innerHTML = '';

			// Set the new content

			// Name for the data field sent to the server. It can be overriden in the config
			// Set the data
			// Append the new input to the form
			theForm.appendChild( createInput( editor.config.serverPreview_FieldName ||  'htmlData' , editor.getData() ) );

			// Append additional fields as needed, or set the variables in the serverPreview_Url
			var fields = (editor.config.serverPreview_AdditionalFields || '').split( ';' );
			var $form = editor.element.$.form;
			for( var i=0; i<fields.length; i++)
			{
				var name = fields[ i ],
					extraEditor = CKEDITOR.instances[ name ];

				if ( !name && !extraEditor )
					continue;

				var value = ( extraEditor ? extraEditor.getData() : $form.elements[ name ].value );
				theForm.appendChild( createInput( name, value ) );
			}

			// Send the data to the server
			var w = window.open('about:blank', "serverpreview", "width=1024,height=800,status=yes,scrollbars=yes,resizable=yes");
			if (w)
				w.focus();

			theForm.submit();
		};

	} // afterInit
} );


