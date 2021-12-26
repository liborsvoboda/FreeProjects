
/**************************************
    Webutler V2.2 - www.webutler.de
    Copyright (c) 2008 - 2012
    Autor: Sven Zinke
    Free for any use
    Lizenz: GPL
**************************************/


(function()
{
    var iframeWindow = null;
	var cssifyLength = CKEDITOR.tools.cssLength;

    function getStyle(Style, Name)
    {
        var result = '';
        var Styles = Style.split(';');
        for( var i = 0; i < Styles.length; i++ )
        {
            var styleAttr = Styles[i].split(':');
            if( CKEDITOR.tools.trim( styleAttr[0] ).toLowerCase() == Name )
            {
                result = CKEDITOR.tools.trim( styleAttr[1] );
                break;
            }
        }
        return result ? result : '';
    }

	function createFakeElement( editor, realElement, width, height )
	{
		var fakeElement = editor.createFakeParserElement( realElement, 'cke_gmap', 'googlemap', true );
		
		var fakeStyle = fakeElement.attributes.style || '';
        fakeStyle = fakeElement.attributes.style = fakeStyle + 'width:' + cssifyLength( width ) + ';';
        fakeStyle = fakeElement.attributes.style = fakeStyle + 'height:' + cssifyLength( height ) + ';';

		return fakeElement;
	}

    CKEDITOR.plugins.add( 'gmap',   
    {
        lang : [CKEDITOR.lang.detect(CKEDITOR.config.language)],
        
        init : function( editor )
        {
            CKEDITOR.document.appendStyleSheet( this.path + 'dialogs/mapinfowin.css' );
            
        	editor.addCss(
        		'img.cke_gmap {' +
    			'	background-image: url(' + CKEDITOR.getUrl( this.path + 'mapfakeimg.gif' ) + ');' +
    			'	background-position: center center;' +
    			'	background-repeat: no-repeat;' +
    			'	border: 1px solid #A9A9A9;' +
        		'}'
    		);
    		
        	editor.addCommand( 'gmap', new CKEDITOR.dialogCommand( 'gmap' ) );
    		
        	editor.ui.addButton( 'gMap',
            {
                label : editor.lang.googlemaps.title,
                icon : this.path + 'gmap.png',
                command : 'gmap'
        	});
        
    		editor.on( 'doubleclick', function( evt )
    		{
    			var element = evt.data.element;
    			
				if ( element.is( 'img' ) && element.data( 'cke-real-element-type' ) == 'googlemap' )
                {
                    //( evt.data.dialog = 'div' ) === false;
        			evt.data.dialog = 'gmap';
                }
                else
    				return null;
    		});
    		
    		if ( editor.addMenuItems )
    		{
    			editor.addMenuItems(
    			{
    				googlemaps :
    				{
    					label : editor.lang.googlemaps.title,
                        icon : this.path + 'gmap.png',
    					command : 'gmap',
    					group : 'googlemaps'
    				}
    			});
    		}
        
            if(editor.contextMenu)
            {
                editor.contextMenu.addListener( function( element, selection )
                {
					if ( element && element.is( 'img' ) && !element.isReadOnly() && element.data( 'cke-real-element-type' ) == 'googlemap' )
                        return { googlemaps : CKEDITOR.TRISTATE_OFF } ;
                    else
        				return null;
                });
            }
        	
        	CKEDITOR.dialog.add( 'gmap', this.path + 'dialogs/gmap.js' );
        },
    	afterInit : function( editor )
    	{
    		var dataProcessor = editor.dataProcessor,
    			dataFilter = dataProcessor && dataProcessor.dataFilter;
        	
    		if( dataFilter )
    		{
    			dataFilter.addRules(
    			{
    				elements :
    				{
    					div : function( element )
    					{
                            var attributes = element.attributes;
							if(attributes[ 'class' ] == 'googlemapsframe')
							{
                                var width = getStyle(attributes[ 'style' ], 'width') || '100';
                        		var height = getStyle(attributes[ 'style' ], 'height') || '100';
                        		return createFakeElement( editor, element, width, height );
							}
    					}
    				}
    			});
    		}
    	},
        requires : [ 'dialog', 'iframedialog', 'fakeobjects' ]
        //requires : [ 'dialog', 'fakeobjects' ]
    });
})();

