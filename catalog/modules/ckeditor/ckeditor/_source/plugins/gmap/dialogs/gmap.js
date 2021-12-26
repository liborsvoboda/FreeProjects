
/**************************************
    Webutler V2.2 - www.webutler.de
    Copyright (c) 2008 - 2012
    Autor: Sven Zinke
    Free for any use
    Lizenz: GPL
**************************************/


CKEDITOR.dialog.add( 'gmap', function( editor )
{
    function getStyle(Style, Name)
    {
        var result = '';
        var Styles = Style.split(';');
        for( var i = 0; i < Styles.length; i++ )
        {
            var styleAttr = Styles[i].split(':');
            if( CKEDITOR.tools.trim( styleAttr[0] ).toLowerCase() == Name )
            {
                result = parseInt( CKEDITOR.tools.trim( styleAttr[1] ) );
                break;
            }
        }
        return result;
    }
                            			
	function defaultMapVar( realelem, suche )
	{
        var term = 'var ' + suche + ' = ';
        var start = realelem.indexOf( term );
        var sel = realelem.substr(start + term.length, realelem.length);
        var value = sel.substr(0, sel.indexOf( ';' ));
        value.replace(/\'/g, "");
        
        return CKEDITOR.tools.trim( value );
    }
    
	return {
		title : editor.lang.googlemaps.wintitle,
		minWidth : 450,
		minHeight : 350,
		onOk : function()
        {
			if(this.getValueOf( 'info', 'marker' ) != true) {
                alert(editor.lang.googlemaps.setmarker);
                return false;
            }
            
            var lat = iframeWindow.lat;
            var lng = iframeWindow.lng;
			var zoom = iframeWindow.map.getZoom();
			var height = this.getValueOf( 'info', 'mapHeight' );
			var width = this.getValueOf( 'info', 'mapWidth' );
			var infowintext = document.getElementById( 'mapinfowin_' + editor.name ).getElementsByTagName('textarea')[0].value;
			infowintext = infowintext.replace(/\r/g, '').replace(/\n/g, ' ');
			
			var mapHtml = '<div class="googlemapsframe" style="width: ' + width + 'px; height: ' + height + 'px">\n' +
                '<scr' + 'ipt type="text/javascript">\n' +
                '  /*<![CDATA[*/\n' +
                '    document.writeln(\'<scr\' + \'ipt type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></scr\' + \'ipt>\' +\n' +
                '    \'    <scr\' + \'ipt type="text/javascript">\' +\n' +
                '    \'    /*<![CDATA[*/\' +\n' +
                '    \'    (function() {\' +\n' +
                '    \'        if (window.addEventListener)\' +\n' +
                '    \'            window.addEventListener(\\\'load\\\', initgooglemap, false);\' +\n' +
                '    \'        else\' +\n' +
                '    \'            window.attachEvent(\\\'onload\\\', initgooglemap);\' +\n' +
                '    \'    })();\' +\n' +
                '    \'    function initgooglemap() {\' +\n' +
                '    \'        var mapZoom = ' + zoom + ';\' +\n' +
                '    \'        var mapLat = ' + lat + ';\' +\n' +
                '    \'        var mapLng = ' + lng + ';\' +\n' +
                '    \'        var mapInfo = \\\'' + infowintext + '\\\';\' +\n' +
                '    \'        var latlng = new google.maps.LatLng(mapLat,mapLng);\' +\n' +
                '    \'        var myOptions = {\' +\n' +
                '    \'            zoom: mapZoom,\' +\n' +
                '    \'            center: latlng,\' +\n' +
                '    \'            mapTypeId: google.maps.MapTypeId.ROADMAP\' +\n' +
                '    \'        };\' +\n' +
                '    \'        var map = new google.maps.Map(document.getElementById(\\\'googlemapcanvas\\\'), myOptions);\' +\n' +
                '    \'        var marker = new google.maps.Marker({\' +\n' +
                '    \'            position: latlng,\' +\n' +
                '    \'            map: map\' +\n' +
                '    \'        });\' +\n' +
                '    \'        marker.setMap(map);\' +\n' +
                '    \'        var infowindow = new google.maps.InfoWindow({\' +\n' +
                '    \'            content: mapInfo\' +\n' +
                '    \'        });\' +\n' +
                '    \'        infowindow.open(map, marker);\' +\n' +
                '    \'        google.maps.event.addListener(marker, \\\'click\\\', function() {\' +\n' +
                '    \'            infowindow.open(map, marker);\' +\n' +
                '    \'        });\' +\n' +
                '    \'    };\' +\n' +
                '    \'    /*]]>*/\' +\n' +
                '    \'    </scr\' + \'ipt>\' +\n' +
                '    \'    <div id="googlemapcanvas" style="width: 100%; height: 100%"></div>\');\n' +
                '  /*]]>*/\n' +
                '</scr' + 'ipt>\n' +
                '</div>';
            
            editor.insertHtml( mapHtml );
		},
		contents:
        [
            {
                id : 'info',
                label : '',
                'style' : 'overflow: hidden',
                elements:
                [
                    {
                        type : 'vbox',
                        padding : 0,
                        children :
                        [
                            {
        						type : 'hbox',
								widths : [ '10%', '100%', '10%' ],
        						children :
        						[
									{
										type : 'html',
										html : editor.lang.googlemaps.address + ':'
									},
            						{
                                        type : 'text',
                                        id : 'adresse',
                                        label : '',
                                        'style' : 'width: 100%',
                                        'default' : editor.lang.googlemaps.typeaddress,
                                        onClick : function()
                                        {
                                            this.setValue( '' );
                                        }
                                        /*
                                        ,
                                        onEnter : function()
                                        {
                                            iframeWindow.showAddress(editor.lang.googlemaps.addressnotfound);
                                        }
                                        */
                                    },
                                    {
                                        type : 'button',
                                        id : 'suche',
                                        label : editor.lang.googlemaps.search,
                                        onClick : function()
                                        {
                                            iframeWindow.showAddress(editor.lang.googlemaps.addressnotfound);
                                        }
                                    }
        						]
    						},
    						{
                                id : 'iframe',
                                label : 'googlemapframe',
                                expand : true,
                                type : 'iframe',
                                src : CKEDITOR.plugins.getPath( 'gmap' ) + 'dialogs/gmap.htm',
                                width : '450',
                                height : '280',
                                'style' : 'padding: 10px 0px 10px 0px',
                                onContentLoad : function() {
                                    var iframe = document.getElementById(this._.frameId);
                                    iframeWindow = iframe.contentWindow;
                                    var dialog = CKEDITOR.dialog.getCurrent();
                                    var element = editor.getSelection().getSelectedElement();
                                    if(element)
                                    {
                                        var realElement = editor.restoreRealElement( element );
                            			if(realElement.getAttribute( 'class' ) == 'googlemapsframe')
                            			{
                                            var width = getStyle(realElement.getAttribute( 'style' ), 'width') || '100';
                                    		var height = getStyle(realElement.getAttribute( 'style' ), 'height') || '100';
                                			dialog.setValueOf( 'info', 'mapHeight', height );
                                			dialog.setValueOf( 'info', 'mapWidth', width );
                                            dialog.setValueOf( 'info', 'marker', true );
                                
                                            var mapScript = decodeURIComponent( realElement.getHtml() ).toString();
                            
                                            var map_infotext = defaultMapVar( mapScript, 'mapInfo' );
                                            map_infotext = map_infotext.substr(2, map_infotext.length-4);
                                    		document.getElementById( 'mapinfowin_' + editor.name ).getElementsByTagName('textarea')[0].value = map_infotext;
                                    		
                                            var gmap_zoom = defaultMapVar( mapScript, 'mapZoom' );
                                    		var gmap_lat = defaultMapVar( mapScript, 'mapLat' );
                                    		var gmap_lng = defaultMapVar( mapScript, 'mapLng' );
                                    		
                                            iframeWindow.zoomer = parseInt( gmap_zoom );
                                            iframeWindow.infowintext = map_infotext;
                                            iframeWindow.lat = parseFloat( gmap_lat );
                                            iframeWindow.lng = parseFloat( gmap_lng );
                                            //setTimeout("iframeWindow.reloadMap()", 2000);
                                            iframeWindow.reloadMap();
                            			}
                                    }
                                }
                            },
        					{
        						type : 'hbox',
								widths : [ '0px', '50px', '40px', '30px', '35px', '30px', '35px', '160px' ],
        						children :
        						[
									{
										type : 'html',
										html : '<div id="mapinfowin_' + editor.name + '" style="display: none" class="infowin">' +
                                            '<textarea class="infoeditor"></textarea>' +
										    '<img class="infobutton" src="' + CKEDITOR.plugins.getPath( 'gmap' ) + 'dialogs/close.gif" onclick="if(this.parentNode.getElementsByTagName(\'textarea\')[0].value != \'\') { if(confirm(\'' + editor.lang.googlemaps.confirm + '\')) { this.parentNode.getElementsByTagName(\'textarea\')[0].value = \'\'; iframeWindow.infowintext = \'\'; iframeWindow.setInfoWin(); } else { return false; } } this.parentNode.style.display=\'none\';" title="' + editor.lang.googlemaps.cancel + '" />' +
										    '<img class="infobutton" src="' + CKEDITOR.plugins.getPath( 'gmap' ) + 'dialogs/ok.gif" onclick="iframeWindow.infowintext = this.parentNode.getElementsByTagName(\'textarea\')[0].value; iframeWindow.setInfoWin(); this.parentNode.style.display=\'none\';" title="' + editor.lang.googlemaps.accept + '" style="margin-top: 35px" />' +
                                            '</div>'
									},
                                    {
                    					id : 'marker',
                    					type : 'checkbox',
                    					label : ' ' + editor.lang.googlemaps.marker,
                                        onClick : function()
                                        {
                                            iframeWindow.setMarker();
                                        }
                                    },
									{
                                        type : 'button',
                                        id : 'infotext',
                                        label : editor.lang.googlemaps.text,
                                        onClick : function()
                                        {
                                            this.getElement().getDocument().getById( 'mapinfowin_' + editor.name ).setStyle( 'display', 'block' );
                                            document.getElementById( 'mapinfowin_' + editor.name ).getElementsByTagName('textarea')[0].focus();
                                        }
									},
									{
										type : 'html',
										html : editor.lang.googlemaps.width + ':'
									},
									{
										type : 'text',
										id : 'mapWidth',
										label : '',
										'style' : 'width: 35px',
										'default' : '500',
										required : true,
										validate : CKEDITOR.dialog.validate.notEmpty(editor.lang.googlemaps.nowidth)
									},
									{
										type : 'html',
										html : editor.lang.googlemaps.height + ':'
									},
									{
										type : 'text',
										id : 'mapHeight',
										label : '',
										'style' : 'width: 35px',
										'default' : '300',
										required : true,
										validate : CKEDITOR.dialog.validate.notEmpty(editor.lang.googlemaps.noheight)
									},
									{
										type : 'html',
										html : '&nbsp;'
									}
                                ]
                            }
                        ]
                    }
                ]
            }
        ]
    };
});


	
