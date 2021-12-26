/*!
 * maps plugin for CKEditor 3.x
 *
 * Copyright 2011, Christian Voigt, bitclinic.de
 * MIT License.
 *
 */

function getParameter(parameter, url) {
    //remove url before parameters
    url = url.replace(/.*\?/g,'');
    //built parameterlist and get needed parameter
    var parameterList = url.split('&');
    for (var i=0;i<parameterList.length;i++) {
        var pair = parameterList[i].split('=');
        if (pair[0] == parameter) {
            return decodeURI(pair[1]);
        }
    }
    return null;
}

CKEDITOR.dialog.add('maps',function(editor) {
    return {
        title: editor.lang.title,
        minWidth: 200,
        minHeight: 200,
        onShow: function() {
            var element = this.getParentEditor().getSelection().getSelectedElement();
            this.setupContent(element);
        },
        onOk: function() {
            var address = this.getValueOf('main','addressField');
            var zoom = this.getValueOf('main','zoomField');
            var width = this.getValueOf('main','widthField');
            var height = this.getValueOf('main','heightField');
            var maptype = this.getValueOf('main','maptypeField');
            var setLink = this.getValueOf('main','setLinkField');
            address = encodeURI(address);
            //html code to get image
            var code = '<img src="http://maps.google.com/maps/api/staticmap?center='+address+'&zoom='+zoom+'&size='+width+'x'+height+'&maptype='+maptype+'&markers=color:red|'+address+'&sensor=false" alt="map" />';
            //link image to google maps?
            if (setLink == true) {
                code = '<a alt="map" href="http://maps.google.de/maps?f=q&source=s_q&hl=de&geocode=&q='+address+'&z='+zoom+'" target="_blank" style="text-align:left;font-size:70%;">' + code;
                code += '</a>';
            }
            editor.insertHtml(code);
        },
        contents: [
            {
                id: 'main',
                label: editor.lang.title,
                elements: [
                    {
                        id: 'addressField',
                        type: 'text',
                        label: editor.lang.address,
                        labelLayout: 'vertical',
                        required: true,
                        validate: CKEDITOR.dialog.validate.notEmpty(),
                        setup: function(element) {
                            var src = element.getAttribute('src');
                            this.setValue(getParameter('center',src));
                        }
                    },
                    {
                        id: 'zoomField',
                        type: 'select',
                        label: editor.lang.zoom,
                        labelLayout: 'horizontal',
                        width: '80px',
                        'default': '16',
                        items: [
                            [ '20 '+ editor.lang.nearby, '20'],
                            [ '19' , '19'],
                            [ '18' , '18'],
                            [ '17' , '17'],
                            [ '16' , '16'],
                            [ '15' , '15'],
                            [ '14' , '14'],
                            [ '13' , '13'],
                            [ '12' , '12'],
                            [ '11' , '11'],
                            [ '10 '+ editor.lang.far, '10']
                        ],
                        setup: function(element) {
                            var src = element.getAttribute('src');
                            this.setValue(getParameter('zoom',src));
                        }
                    },
                    {
                        id: 'maptypeField',
                        type: 'select',
                        label: editor.lang.maptype,
                        labelLayout: 'horizontal',
                        width: '80px',
                        'default': 'roadmap',
                        items: [
                            ['roadmap'],
                            ['satellite'],
                            ['hybrid'],
                            ['terrain']
                        ],
                        setup: function(element) {
                            var src = element.getAttribute('src');
                            this.setValue(getParameter('maptype',src));
                        }
                    },
                    {
                        id: 'widthField',
                        type: 'text',
                        label: editor.lang.width,
                        labelLayout: 'horizontal',
                        required: true,
                        validate: CKEDITOR.dialog.validate.notEmpty(),
                        width: '80px',
                        'default': '400',
                        setup: function(element) {
                            var src = element.getAttribute('src');
                            var size = getParameter('size',src).split('x');
                            this.setValue(size[0]);
                        }
                    },
                    {
                        id: 'heightField',
                        type: 'text',
                        label: editor.lang.height,
                        labelLayout: 'horizontal',
                        required: true,
                        validate: CKEDITOR.dialog.validate.notEmpty(),
                        width: '80px',
                        'default': '400',
                        setup: function(element) {
                            var src = element.getAttribute('src');
                            var size = getParameter('size',src).split('x');
                            this.setValue(size[1]);
                        }
                    },
                    {
                        id: 'setLinkField',
                        type: 'checkbox',
                        label: editor.lang.linkImage,
                        labelLayout: 'horizontal',
                        setup: function(element) {
                            var alt = element.getParent().getAttribute('alt');
                            if (alt == 'map') {
                                this.setValue(true);
                            } else {
                                this.setValue(false);
                            }
                        }
                    }
                ]
            }
        ]
    };
} );
