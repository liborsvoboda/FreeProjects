/*!
 * maps plugin for CKEditor 3.x
 *
 * Copyright 2011, Christian Voigt, bitclinic.de
 * MIT License.
 *
 */

CKEDITOR.plugins.add('maps', {
    requires: ['dialog'],
    lang: ['cs','en', 'de'],
    beforeInit: function(editor) {
        editor.config.menu_groups = editor.config.menu_groups + ',' + 'maps';
    },
    init: function(editor) {
        var pluginName = 'maps';
        //register dialog
        CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/maps.js');
        //register command
        editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));
        //register toolbar button
        editor.ui.addButton(pluginName, {
            label: editor.lang.title,
            command: pluginName,
            icon: this.path + 'maps.png'
        } );
        //register doubleclick
        editor.on('doubleclick', function(evt) {
            var element = evt.data.element;
            if (element && !element.isReadOnly() && element.getAttribute('alt') == 'map')
                evt.data.dialog = pluginName;
            } );
        //register menu item
        if (editor.addMenuItems) {
            editor.addMenuItems(
                {
                    maps:
                    {
                        label: editor.lang.title,
                        command: pluginName,
                        group: pluginName,
                        icon: this.path + 'maps.png'
                    }
                } );
        }
        //register contextmenu
        if (editor.contextMenu) {
            editor.contextMenu.addListener(function(element, selection) {
                if (!element || element.isReadOnly() || element.getAttribute('alt') != 'map')
                    return null;
                return {maps: CKEDITOR.TRISTATE_OFF};
            } );
        }
    }
} );
