
/**************************************
    Webutler V2.1 - www.webutler.de
    Copyright (c) 2008 - 2011
    Autor: Sven Zinke
    Free for any use
    Lizenz: GPL
**************************************/


(function()
{
    CKEDITOR.scriptLoader.load( CKEDITOR.plugins.getPath( 'flashplayer' ) + 'config.js' );
    
    CKEDITOR.plugins.add( 'flashplayer',
    {
        lang : [CKEDITOR.lang.detect(CKEDITOR.config.language)],
        
    	init : function( editor )
    	{
            editor._.newflashFn = CKEDITOR.tools.addFunction(
                function( newSwf )
                {
                    var dialog = CKEDITOR.dialog.getCurrent();
                    dialog.setValueOf('info', 'width', newSwf.newWidth);
                    dialog.setValueOf('info', 'height', newSwf.newHeight);
                },
                editor
            );
            
    		editor.on( 'doubleclick', function( evt )
    		{
    			var element = CKEDITOR.plugins.link.getSelectedLink( editor ) || evt.data.element;
    			
                if ( !element.hasClass( 'flvpopuplink' ) )
    				return null;
    			
                ( evt.data.dialog = 'link' ) === false;
                
                flvOrgLink = element;
                flvLink = element.data( 'cke-pa-onclick' ) || element.getAttribute( 'onclick' );
                flvLinkTxt = element.getText();
                
    			evt.data.dialog = 'flash';
    		});
            
            if(editor.contextMenu)
            {
                editor.contextMenu.addListener( function( element, selection )
                {
                    if ( !element.hasClass( 'flvpopuplink' ) )
                        return null;
                    
                    flvOrgLink = element;
                    flvLink = element.data( 'cke-pa-onclick' ) || element.getAttribute( 'onclick' );
                    flvLinkTxt = element.getText();
                    
                    editor.contextMenu.removeAll();
                    
                    return {
                        cut : editor.getCommand( 'cut' ).state,
                        copy : editor.getCommand( 'copy' ).state,
                        paste : CKEDITOR.TRISTATE_OFF,
                        flash : CKEDITOR.TRISTATE_ON
                    };
                });
            }
    	}
    });
    
    CKEDITOR.on( 'dialogDefinition', function( ev )
    {
    	var dialogName = ev.data.name;
    	var dialogDefinition = ev.data.definition;
        var editor = ev.editor;
    	
    	if ( dialogName == 'flash' )
    	{
            var autoLinkTxt = editor.lang.player.flv.linksource;
            var playerColor = playerConf.color;
            
            var infoTab = dialogDefinition.getContents('info');
    
            infoTab.add
            (
                {
        			type : 'html',
        			id : 'swfsizeframe',
        			html : '<iframe src="' + CKEDITOR.plugins.getPath( 'flashplayer' ) + 'swfsize.php" id="swfsize" name="swfsize" style="width: 0px; height: 0px;" width="0" height="0" scrolling="no" frameborder="0"></iframe>',
                    'style' : 'display: none;'
                },
                'preview'
            );
            
            var srcField = infoTab.get( 'src' );
            srcField.onChange = function()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                var URL = this.getValue();
    
                if( URL.substring(0, playerConf.mediapath.length) == playerConf.mediapath )
                {
                    frames.swfsize.location.replace( CKEDITOR.plugins.getPath( 'flashplayer' ) + 'swfsize.php?movie=' + URL + '&CKEditorFuncNum=' + editor._.newflashFn);
                    hideTabs( 'both' );
                }
                else
                {
                    if ( getTrackParam(URL, 'flv') != '' )
                    {
                        hideTabs( 'flv' );
                        if ( getTrackParam(URL, 'margin') == '' )
                            loadVideoDefault();
                    }
                    else if ( getTrackParam(URL, 'mp3') != '' )
                    {
                        hideTabs( 'mp3' );
                        if ( getTrackParam(URL, 'showloading') == '' )
                            loadAudioDefault();
                    }
                    else
                    {
                        hideTabs( 'both' );
                    }
                }
            };
            
            var widthField = infoTab.get( 'width' );
            var heightField = infoTab.get( 'height' );
            widthField.onChange = heightField.onChange = function()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                var URL = dialog.getValueOf('info', 'src');
                if ( getTrackParam(URL, 'flv') != '' )
                {
                    var defWidth = dialog.getValueOf('info', 'width');
                    var defHeight = dialog.getValueOf('info', 'height');
                    var videoformat = defWidth + 'x' + defHeight;
                    
                    dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                    dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                    dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                    dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                    dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                    dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                    dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                    dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                    
                    if ( dialog.getContentElement( 'FLVconfig', 'video_' + videoformat) )
                        dialog.setValueOf('FLVconfig', 'video_' + videoformat, videoformat );
                }
                else if ( getTrackParam(URL, 'mp3') != '' )
                {
                    if ( dialog.getValueOf('info', 'height') != '20' )
                        dialog.setValueOf('info', 'height', '20')
                    
                    var newWidth = dialog.getValueOf('info', 'width');
                    if ( getTrackParam(URL, 'width') != newWidth )
                    {
                        dialog.setValueOf( 'MP3config', 'audio_width200', false );
                        dialog.setValueOf( 'MP3config', 'audio_width300', false );
                        dialog.setValueOf( 'MP3config', 'audio_width400', false );
                        
                        if ( dialog.getContentElement( 'MP3config', 'audio_width' + newWidth) )
                            dialog.setValueOf('MP3config', 'audio_width' + newWidth, newWidth );
                        
                        setAudioFields();
                    }
                }
            };
            
            function getTrackParam( URL, paramName )
            {
                var reParam = new RegExp('(?:[\?&]|&amp;)' + paramName + '=([^&]+)', 'i') ;
                var match = URL.match(reParam) ;
                
                return (match && match.length > 1) ? match[1] : '' ;
            }
            
            function search_amp( val )
            {
                if(val.substr( 0, 4 ) == 'amp;') {
                    val = val.substring( 4, val.length );
                }
                return val;
            }
            
            function hideTabs( tab )
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                if( tab == 'flv' ) {
            		dialog.showPage( 'FLVconfig' );
            		dialog.hidePage( 'MP3config' );
            		dialog.hidePage( 'properties' );
                    dialog.setValueOf( 'properties', 'allowFullScreen', true );
                }
                else if( tab == 'mp3' ) {
            		dialog.showPage( 'MP3config' );
            		dialog.hidePage( 'FLVconfig' );
            		dialog.hidePage( 'properties' );
                }
                else if( tab == 'both' ) {
            		dialog.hidePage( 'FLVconfig' );
            		dialog.hidePage( 'MP3config' );
            		dialog.showPage( 'properties' );
                }
            }
            
            function loadOnShow()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                if ( typeof flvLink != 'undefined' )
                {
                    var param = new RegExp('openFLVPopup\\(\'([^\']+)\'([^\']+)\'([0-9]+)\'([^\']+)\'([0-9]+)\'\\)', 'i');
                    var funcValue = flvLink.match( param );
                    
                    dialog.setValueOf('info', 'width', funcValue[3]);
                    dialog.setValueOf('info', 'height', funcValue[5]);
                    dialog.setValueOf( 'FLVconfig', 'popup', true );
                    if ( typeof flvLinkTxt != 'undefined' )
                        dialog.setValueOf( 'FLVconfig', 'popuptext', flvLinkTxt );
                    dialog.setValueOf('info', 'src', funcValue[1]);
                }
                
                var URL = dialog.getValueOf('info', 'src');
                if ( URL != '' && URL.substring(0, playerConf.mediapath.length) != playerConf.mediapath )
                {
                    if ( getTrackParam(URL, 'flv') != '' )
                    {
                        if ( getTrackParam(URL, 'margin') != '' )
                            setVideoOptions( URL );
                    }
                    else if ( getTrackParam(URL, 'mp3') != '' )
                    {
                        if ( getTrackParam(URL, 'showloading') != '' )
                            setAudioOptions( URL );
                    }
                }
                else
                {
                    hideTabs( 'both' );
                }
            };
            
            function flvLinkOkFunc()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                if ( dialog.getValueOf( 'FLVconfig', 'popup' ) == true )
                {
                    var src = dialog.getValueOf( 'info', 'src' );
                    var width = dialog.getValueOf( 'info', 'width' );
                    var height = dialog.getValueOf( 'info', 'height' );
                    dialog.setValueOf( 'info', 'src', '' );
                    
                    var newLinkTxt;
                    if ( dialog.getValueOf( 'FLVconfig', 'popuptext' ) != '' )
                        newLinkTxt = dialog.getValueOf( 'FLVconfig', 'popuptext' );
                    else
                        newLinkTxt = autoLinkTxt;
    
                    var flvHtml = '<a class="flvpopuplink" ' + 
                        'data-cke-pa-onclick="openFLVPopup(\'' + 
                        src + '\', \'' + 
                        width + '\', \'' + 
                        height + '\')">' + 
                        newLinkTxt + 
                        '</a>';
                        
                    var flvFromHtml = CKEDITOR.dom.element.createFromHtml( flvHtml );
                    
                    try {
                        if ( typeof flvOrgLink != 'undefined' ) {
                            flvFromHtml.replace( flvOrgLink );
        				}
        				else {
                            var fakeImage = editor.getSelection().getSelectedElement();
                            if ( fakeImage && fakeImage.data( 'cke-real-element-type' ) && fakeImage.data( 'cke-real-element-type' ) == 'flash' )
                                flvFromHtml.replace( fakeImage );
                            else
                                editor.insertHtml( flvHtml );
                        }
                    }
                    catch(e) {}
                    
                    dialog.hide();
                }
            };
            
            function setVideoOptions( URL )
            {
                if ( getTrackParam(URL, 'volume') != '' )
                {
                    var dialog = CKEDITOR.dialog.getCurrent();
                    var defWidth = dialog.getValueOf('info', 'width');
                    var defHeight = dialog.getValueOf('info', 'height');
                    var videoformat = defWidth + 'x' + defHeight;
                    
                    if ( dialog.getContentElement( 'FLVconfig', 'video_' + videoformat) )
                        dialog.setValueOf('FLVconfig', 'video_' + videoformat, videoformat );
                    
                    var urlSplit = URL.split('?');
                    var urlParams = urlSplit[1].split('&');
                	for(var i = 0; i < urlParams.length; i++)
                    {
                        urlParams[i] = search_amp(urlParams[i]);
                        var param = urlParams[i].split('=');
                        
                        if ( dialog.getContentElement( 'FLVconfig', 'video_' + param[0]) )
                        {
                            if ( param[0] == 'autoplay' || param[0] == 'showiconplay' )
                                dialog.setValueOf( 'FLVconfig', 'video_' + param[0], param[0] );
                            else
                                dialog.setValueOf( 'FLVconfig', 'video_' + param[0], true );
                        }
                        else
                        {
                            if ( param[0] == 'volume' )
                                dialog.setValueOf( 'FLVconfig', 'video_' + param[0] + param[1], param[1] );
                        }
                    }
                }
            }
            
            function loadVideoDefault()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                dialog.setValueOf('info', 'width', '160');
                dialog.setValueOf('info', 'height', '120');
                dialog.setValueOf('FLVconfig', 'video_160x120', '160x120');
                
                dialog.setValueOf('FLVconfig', 'video_autoload', true);
                dialog.setValueOf('FLVconfig', 'video_showiconplay', 'showiconplay');
                dialog.setValueOf('FLVconfig', 'video_showstop', true);
                dialog.setValueOf('FLVconfig', 'video_showvolume', true);
                dialog.setValueOf('FLVconfig', 'video_volume150', '150');
                
                setVideoFields();
            }
            
            function setVideoFields()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                var URL = dialog.getValueOf( 'info', 'src' );
                var loadTrack = playerConf.playerpath + '/flvplayer.swf?flv=' + getTrackParam(URL, 'flv');
                dialog.setValueOf( 'info', 'src', loadTrack + getVideoParam() );
            }
            
            function getVideoParam()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                var params = '&margin=0&playeralpha=50&iconplaybgalpha=50&showmouse=autohide&loadingcolor=' + playerColor + '&buttonovercolor=' + playerColor + '&sliderovercolor=' + playerColor;
                
                if( dialog.getValueOf( 'FLVconfig', 'video_loop' ) == true )
                    params = params + '&loop=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_autoload' ) == true )
                    params = params + '&autoload=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_autoplay' ) == 'autoplay' )
                    params = params + '&autoplay=1';
                else if( dialog.getValueOf( 'FLVconfig', 'video_showiconplay' ) == 'showiconplay' )
                    params = params + '&showiconplay=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_showstop' ) == true )
                    params = params + '&showstop=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_showvolume' ) == true )
                    params = params + '&showvolume=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_showtime' ) == true )
                    params = params + '&showtime=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_showfullscreen' ) == true )
                    params = params + '&showfullscreen=1';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_showplayer' ) == true )
                    params = params + '&showplayer=never';
                
                if( dialog.getValueOf( 'FLVconfig', 'video_volume1' ) == '1' )
                    params = params + '&volume=1';
                else if( dialog.getValueOf( 'FLVconfig', 'video_volume50' ) == '50' )
                    params = params + '&volume=50';
                else if( dialog.getValueOf( 'FLVconfig', 'video_volume100' ) == '100' )
                    params = params + '&volume=100';
                else if( dialog.getValueOf( 'FLVconfig', 'video_volume150' ) == '150' )
                    params = params + '&volume=150';
                else if( dialog.getValueOf( 'FLVconfig', 'video_volume200' ) == '200' )
                    params = params + '&volume=200';
                else
                    params = params + '&volume=150';
                
                return params;
            }
            
            var resetVideoFields = function()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                
                dialog.setValueOf( 'FLVconfig', 'video_loop', false );
                dialog.setValueOf( 'FLVconfig', 'video_autoload', false );
                dialog.setValueOf( 'FLVconfig', 'video_autoplay', false );
                dialog.setValueOf( 'FLVconfig', 'video_showiconplay', false );
                dialog.setValueOf( 'FLVconfig', 'video_showstop', false );
                dialog.setValueOf( 'FLVconfig', 'video_showvolume', false );
                dialog.setValueOf( 'FLVconfig', 'video_showtime', false );
                dialog.setValueOf( 'FLVconfig', 'video_showfullscreen', false );
                dialog.setValueOf( 'FLVconfig', 'video_showplayer', false );
                dialog.setValueOf( 'FLVconfig', 'video_volume1', false );
                dialog.setValueOf( 'FLVconfig', 'video_volume50', false );
                dialog.setValueOf( 'FLVconfig', 'video_volume100', false );
                dialog.setValueOf( 'FLVconfig', 'video_volume150', false );
                dialog.setValueOf( 'FLVconfig', 'video_volume200', false );
                
                dialog.setValueOf( 'FLVconfig', 'popup', false );
                dialog.setValueOf( 'FLVconfig', 'popuptext', '' );
                
                hideTabs( 'both' );
                    
                try {
                    if ( typeof flvLink != 'undefined' ) {
                        delete flvOrgLink;
                        delete flvLink;
                        delete flvLinkTxt;
    				}
                }
                catch(e) {}
            }
            
            dialogDefinition.addContents(
            {
    			id : 'FLVconfig',
    			label : editor.lang.player.flv.title,
    			elements :
    			[
        			{
    					type : 'hbox',
    					widths : [ '40%', '60%' ],
    					children :
    					[
    						{
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.flv.settings + ':'
                                    },
                                    {
                            			type : 'vbox',
                                        padding : 3,
                            			'style' : 'padding-left: 25px',
                            			children :
                                        [
                            				{
                            					id : 'video_loop',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.loop,
                            					'default' : '',
                                                onClick : function() {
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_autoload',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.autoload,
                            					'default' : '',
                                                onClick : function() {
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_autoplay',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' ' + editor.lang.player.flv.autoplay, 'autoplay' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if ( this.getValue() != null )
                                                        dialog.setValueOf( 'FLVconfig', 'video_showiconplay', false );
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_showiconplay',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' ' + editor.lang.player.flv.iconplay, 'showiconplay' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if ( this.getValue() != null )
                                                        dialog.setValueOf( 'FLVconfig', 'video_autoplay', false );
                                                    setVideoFields();
                                                }
                            				}
                            			]
                        			}
                    			]
                        	},
    						{
    							type : 'vbox',
    							padding : 3,
    							children :
    							[
                                    {
                            			type : 'html',
                            			html : editor.lang.player.flv.format
                                    },
    								{
                            			type : 'hbox',
                                        widths : [ '50%', '50%' ],
                            			children :
                                        [
                                            {
                                    			type : 'vbox',
                                                padding : 3,
                                    			'style' : 'padding-left: 25px',
                                    			children :
                                                [
                                    				{
                                    					id : 'video_160x120',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 160 x 120', '160x120' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '160');
                                                            dialog.setValueOf('info', 'height', '120');
                                                        }
                                    				},
                                    				{
                                    					id : 'video_320x240',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 320 x 240', '320x240' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '320');
                                                            dialog.setValueOf('info', 'height', '240');
                                                        }
                                    				},
                                    				{
                                    					id : 'video_480x360',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 480 x 360', '480x360' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '480');
                                                            dialog.setValueOf('info', 'height', '360');
                                                        }
                                    				},
                                    				{
                                    					id : 'video_640x480',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 640 x 480', '640x480' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '640');
                                                            dialog.setValueOf('info', 'height', '480');
                                                        }
                                    				}
                                    			]
                                			},
                                            {
                                    			type : 'vbox',
                                                padding : 3,
                                    			'style' : 'padding-left: 15px',
                                    			children :
                                                [
                                    				{
                                    					id : 'video_160x90',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 160 x 90', '160x90' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '160');
                                                            dialog.setValueOf('info', 'height', '90');
                                                        }
                                    				},
                                    				{
                                    					id : 'video_320x180',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 320 x 180', '320x180' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '320');
                                                            dialog.setValueOf('info', 'height', '180');
                                                        }
                                    				},
                                    				{
                                    					id : 'video_480x270',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 480 x 270', '480x270' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x360', false );
                                                            dialog.setValueOf('info', 'width', '480');
                                                            dialog.setValueOf('info', 'height', '270');
                                                        }
                                    				},
                                    				{
                                    					id : 'video_640x360',
                                    					type : 'radio',
                                    					label : '',
                                    					items : [[ ' 640 x 360', '640x360' ]],
                                    					'default' : '',
                                                        onClick : function() {
                                                            var dialog = CKEDITOR.dialog.getCurrent();
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x120', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x240', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x360', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_640x480', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_160x90', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_320x180', false );
                                                            dialog.setValueOf( 'FLVconfig', 'video_480x270', false );
                                                            dialog.setValueOf('info', 'width', '640');
                                                            dialog.setValueOf('info', 'height', '360');
                                                        }
                                    				}
                                    			]
                                			}
                            			]
    								}
    							]
    						}
                        ]
                	},
                	{
    					type : 'hbox',
    					widths : [ '40%', '60%' ],
    					children :
    					[
                            {
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.flv.playerbar + ':'
                            		},
                            		{
                            			type : 'vbox',
                            			padding : 3,
                                        'style' : 'padding-left: 25px',
                            			children :
                                        [
                                    		{
                            					id : 'video_showstop',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.stop,
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if( dialog.getValueOf( 'FLVconfig', 'video_showplayer' ) == true )
                                                        this.setValue( false );
                                                    setVideoFields();
                                                }
                                            },
                                    		{
                            					id : 'video_showvolume',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.volume,
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if( dialog.getValueOf( 'FLVconfig', 'video_showplayer' ) == true )
                                                        this.setValue( false );
                                                    setVideoFields();
                                                }
                                            },
                                    		{
                            					id : 'video_showtime',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.time,
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if( dialog.getValueOf( 'FLVconfig', 'video_showplayer' ) == true )
                                                        this.setValue( false );
                                                    setVideoFields();
                                                }
                                            },
                                    		{
                            					id : 'video_showfullscreen',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.fullscreen,
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if( dialog.getValueOf( 'FLVconfig', 'video_showplayer' ) == true )
                                                        this.setValue( false );
                                                    setVideoFields();
                                                }
                                            },
                                    		{
                            					id : 'video_showplayer',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.activ,
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if( this.getValue() == true ) {
                                                        dialog.setValueOf( 'FLVconfig', 'video_showstop', false );
                                                        dialog.setValueOf( 'FLVconfig', 'video_showvolume', false );
                                                        dialog.setValueOf( 'FLVconfig', 'video_showtime', false );
                                                        dialog.setValueOf( 'FLVconfig', 'video_showfullscreen', false );
                                                    }
                                                    setVideoFields();
                                                }
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.flv.volumestart + ':'
                            		},
                                    {
                            			type : 'vbox',
                            			padding : 3,
                                        'style' : 'padding-left: 25px',
                            			children :
                                        [
                            				{
                            					id : 'video_volume1',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' ' + editor.lang.player.flv.volumenull, '1' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume50', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume100', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume150', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume200', false );
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_volume50',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 25%', '50' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume1', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume100', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume150', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume200', false );
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_volume100',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 50%', '100' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume1', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume50', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume150', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume200', false );
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_volume150',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 75%', '150' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume1', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume50', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume100', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume200', false );
                                                    setVideoFields();
                                                }
                            				},
                            				{
                            					id : 'video_volume200',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 100%', '200' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume1', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume50', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume100', false );
                                                    dialog.setValueOf( 'FLVconfig', 'video_volume150', false );
                                                    setVideoFields();
                                                }
                            				}
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                	{
    					type : 'hbox',
    					widths : [ '40%', '60%' ],
    					children :
    					[
                            {
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.flv.popuptitle + ':'
                            		},
                                	{
                    					type : 'vbox',
                            			padding : 3,
                                        'style' : 'padding-left: 25px',
                    					children :
                    					[
                                    		{
                            					id : 'popup',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.flv.popupdesc,
                            					'default' : '',
                                                onChange : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    var okId = dialog.getButton( 'ok' ).domId;
                                                    var linkOkId = dialog.getButton( 'linkok' ).domId;
                                                    if( this.getValue() == false ) {
                                                        dialog.setValueOf( 'FLVconfig', 'popuptext', '' );
                                                        dialog.disableButton( 'linkok' );
                                                        dialog.enableButton( 'ok' );
                                                        document.getElementById( okId ).style.display = 'block';
                                                        document.getElementById( linkOkId ).style.display = 'none';
                                                    }
                                                    else {
                                                        dialog.setValueOf( 'FLVconfig', 'popuptext', autoLinkTxt );
                                                        dialog.disableButton( 'ok' );
                                                        dialog.enableButton( 'linkok' );
                                                        document.getElementById( okId ).style.display = 'none';
                                                        document.getElementById( linkOkId ).style.display = 'block';
                                                    }
                                                }
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : '&nbsp;'
                            		},
                                	{
                    					type : 'hbox',
                                        widths : [ '16%', '58%', '26%' ],
                    					children :
                    					[
                                            {
                                    			type : 'html',
                                    			html : '<div style="margin-top: 5px">' + editor.lang.player.flv.linktxt + ':</div>'
                                    		},
                                    		{
                            					id : 'popuptext',
                            					type : 'text',
                            					label : ' ',
                            					'default' : '',
                                                onKeyup : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    if( dialog.getValueOf( 'FLVconfig', 'popup' ) == false )
                                                        this.setValue('');
                                                }
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
    		}, 'properties' );
    		
            function loadAudioDefault()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                dialog.setValueOf('info', 'width', '200');
                dialog.setValueOf('info', 'height', '20');
                dialog.setValueOf('MP3config', 'audio_autoload', true );
                dialog.setValueOf('MP3config', 'audio_width200', '200');
                dialog.setValueOf('MP3config', 'audio_showvolume', true );
                dialog.setValueOf('MP3config', 'audio_volume150', '150');
                
                setAudioFields();
            }
            
            function setAudioOptions( URL )
            {
                if ( getTrackParam(URL, 'volume') != '' )
                {
                    var dialog = CKEDITOR.dialog.getCurrent();
                    var defWidth = dialog.getValueOf('info', 'width');
                    
                    if ( dialog.getContentElement( 'MP3config', 'audio_width' + defWidth) )
                        dialog.setValueOf('MP3config', 'audio_width' + defWidth, defWidth );
                    
                    dialog.setValueOf('info', 'height', '20' );
                    
                    var urlSplit = URL.split('?');
                    var urlParams = urlSplit[1].split('&');
                	for(var i = 0; i < urlParams.length; i++)
                    {
                        urlParams[i] = search_amp(urlParams[i]);
                        var param = urlParams[i].split('=');
                        
                        if ( dialog.getContentElement( 'MP3config', 'audio_' + param[0]) )
                        {
                            dialog.setValueOf( 'MP3config', 'audio_' + param[0], true );
                        }
                        else
                        {
                            if ( param[0] == 'width' )
                            {
                                dialog.setValueOf( 'info', 'width', param[1] );
                                if ( dialog.getContentElement( 'MP3config', 'audio_width' + param[1]) )
                                    dialog.setValueOf( 'MP3config', 'audio_width' + param[1], param[1] );
                            }
                            if ( param[0] == 'volume' )
                            {
                                dialog.setValueOf( 'MP3config', 'audio_volume' + param[1], param[1] );
                            }
                        }
                    }
                }
            }
            
            function setAudioFields()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                var URL = dialog.getValueOf( 'info', 'src' );
                var loadTrack = playerConf.playerpath + '/mp3player.swf?mp3=' + getTrackParam(URL, 'mp3');
                dialog.setValueOf( 'info', 'src', loadTrack + getAudioParam() );
            }
            
            function getAudioParam()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                var params = '&showloading=always&loadingcolor=' + playerColor + '&buttonovercolor=' + playerColor + '&sliderovercolor=' + playerColor;
                
                if( dialog.getValueOf( 'MP3config', 'audio_loop' ) == true )
                    params = params + '&loop=1';
                            					
                if( dialog.getValueOf( 'MP3config', 'audio_autoload' ) == true )
                    params = params + '&autoload=1';
                            					
                if( dialog.getValueOf( 'MP3config', 'audio_autoplay' ) == true )
                    params = params + '&autoplay=1';
                
                params = params + '&width=' + dialog.getValueOf( 'info', 'width' );
                
                if( dialog.getValueOf( 'MP3config', 'audio_showstop' ) == true )
                    params = params + '&showstop=1';
                            					
                if( dialog.getValueOf( 'MP3config', 'audio_showinfo' ) == true )
                    params = params + '&showinfo=1';
                            					
                if( dialog.getValueOf( 'MP3config', 'audio_showvolume' ) == true )
                    params = params + '&showvolume=1';
    
                if( dialog.getValueOf( 'MP3config', 'audio_volume1' ) == '1' )
                    params = params + '&volume=1';
                else if( dialog.getValueOf( 'MP3config', 'audio_volume50' ) == '50' )
                    params = params + '&volume=50';
                else if( dialog.getValueOf( 'MP3config', 'audio_volume100' ) == '100' )
                    params = params + '&volume=100';
                else if( dialog.getValueOf( 'MP3config', 'audio_volume150' ) == '150' )
                    params = params + '&volume=150';
                else if( dialog.getValueOf( 'MP3config', 'audio_volume200' ) == '200' )
                    params = params + '&volume=200';
                else
                    params = params + '&volume=150';
    
                return params;
            }
            
            var resetAudioFields = function()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                
                dialog.setValueOf( 'MP3config', 'audio_loop', false );
                dialog.setValueOf( 'MP3config', 'audio_autoload', false );
                dialog.setValueOf( 'MP3config', 'audio_autoplay', false );
                dialog.setValueOf( 'MP3config', 'audio_width200', false );
                dialog.setValueOf( 'MP3config', 'audio_width300', false );
                dialog.setValueOf( 'MP3config', 'audio_width400', false );
                dialog.setValueOf( 'MP3config', 'audio_showstop', false );
                dialog.setValueOf( 'MP3config', 'audio_showinfo', false );
                dialog.setValueOf( 'MP3config', 'audio_showvolume', false );
                dialog.setValueOf( 'MP3config', 'audio_volume1', false );
                dialog.setValueOf( 'MP3config', 'audio_volume50', false );
                dialog.setValueOf( 'MP3config', 'audio_volume100', false );
                dialog.setValueOf( 'MP3config', 'audio_volume150', false );
                dialog.setValueOf( 'MP3config', 'audio_volume200', false );
                
                hideTabs( 'both' );
            }
            
            dialogDefinition.addContents(
            {
    			id : 'MP3config',
    			label : editor.lang.player.mp3.title,
    			elements :
    			[
        			{
    					type : 'hbox',
    					widths : [ '50%', '50%' ],
    					children :
    					[
    						{
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.mp3.settings + ':'
                                    },
                                    {
                            			type : 'vbox',
                                        padding : 3,
                            			'style' : 'padding-left: 25px',
                            			children :
                                        [
                            				{
                            					id : 'audio_loop',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.mp3.loop,
                                                onClick : function() {
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_autoload',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.mp3.autoload,
                                                onClick : function() {
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_autoplay',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.mp3.autoplay,
                                                onClick : function() {
                                                    setAudioFields();
                                                }
                            				}
                            			]
                        			}
                    			]
                        	},
    						{
    							type : 'vbox',
    							padding : 3,
    							children :
    							[
                                    {
                            			type : 'html',
                            			html : editor.lang.player.mp3.format
                                    },
    								{
                            			type : 'vbox',
                                        padding : 3,
                            			'style' : 'padding-left: 25px',
                            			children :
                                        [
                            				{
                            					id : 'audio_width200',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 200 ' + editor.lang.player.mp3.pixel, '200' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_width300', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_width400', false );
                                                    dialog.setValueOf('info', 'width', '200');
                                                    dialog.setValueOf('info', 'height', '20');
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_width300',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 300 ' + editor.lang.player.mp3.pixel, '300' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_width200', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_width400', false );
                                                    dialog.setValueOf('info', 'width', '300');
                                                    dialog.setValueOf('info', 'height', '20');
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_width400',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 400 ' + editor.lang.player.mp3.pixel, '400' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_width200', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_width300', false );
                                                    dialog.setValueOf('info', 'width', '400');
                                                    dialog.setValueOf('info', 'height', '20');
                                                    setAudioFields();
                                                }
                            				}
                            			]
                        			}
    							]
    						}
                        ]
                	},
                	{
    					type : 'hbox',
    					widths : [ '50%', '50%' ],
    					children :
    					[
                            {
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.mp3.playerbar + ':'
                            		},
                            		{
                            			type : 'vbox',
                            			padding : 3,
                                        'style' : 'padding-left: 25px',
                            			children :
                                        [
                                    		{
                            					id : 'audio_showstop',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.mp3.stop,
                                                onClick : function() {
                                                    setAudioFields();
                                                }
                                            },
                                    		{
                            					id : 'audio_showinfo',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.mp3.titleinfo,
                                                onClick : function() {
                                                    setAudioFields();
                                                }
                                            },
                                    		{
                            					id : 'audio_showvolume',
                            					type : 'checkbox',
                            					label : ' ' + editor.lang.player.mp3.volume,
                                                onClick : function() {
                                                    setAudioFields();
                                                }
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                    			type : 'vbox',
                    			padding : 3,
                    			children :
                                [
                                    {
                            			type : 'html',
                            			html : editor.lang.player.mp3.volumestart + ':'
                            		},
                                    {
                            			type : 'vbox',
                            			padding : 3,
                                        'style' : 'padding-left: 25px',
                            			children :
                                        [
                            				{
                            					id : 'audio_volume1',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' ' + editor.lang.player.mp3.volumenull, '1' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_volume50', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume100', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume150', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume200', false );
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_volume50',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 25%', '50' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_volume1', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume100', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume150', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume200', false );
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_volume100',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 50%', '100' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_volume1', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume50', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume150', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume200', false );
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_volume150',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 75%', '150' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_volume1', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume50', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume100', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume200', false );
                                                    setAudioFields();
                                                }
                            				},
                            				{
                            					id : 'audio_volume200',
                            					type : 'radio',
                            					label : '',
                            					items : [[ ' 100%', '200' ]],
                            					'default' : '',
                                                onClick : function() {
                                                    var dialog = CKEDITOR.dialog.getCurrent();
                                                    dialog.setValueOf( 'MP3config', 'audio_volume1', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume50', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume100', false );
                                                    dialog.setValueOf( 'MP3config', 'audio_volume150', false );
                                                    setAudioFields();
                                                }
                            				}
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
            	]
    		}, 'properties' );
    		
            dialogDefinition.addButton(
            {
                type : 'button',
            	id : 'linkok',
            	label : 'OK',
            	title : 'OK',
            	'class' : 'cke_dialog_ui_button_linkok',
            	'style' : 'display: none;',
            	onClick : function() {
            		flvLinkOkFunc();
            	}
            }, 'cancel' );
            
            CKEDITOR.document.appendStyleText( '.cke_dialog_ui_button_linkok span.cke_dialog_ui_button { width: 60px; } ' );
    		
            dialogDefinition.onLoad = function()
            {
                var dialog = CKEDITOR.dialog.getCurrent();
                dialog.on('show', function()
                {
                    loadOnShow();
                }, null, null, 100);
                dialog.on('hide', resetVideoFields);
    		};
    	}
    });
})();

