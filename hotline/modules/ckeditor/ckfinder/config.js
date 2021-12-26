/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckfinder.com/license
*/

CKFinder.customConfig = function( config )
{
	// Define changes to default configuration here. For example:
	 config.skin = 'kama';
	 config.language = 'cs';
     //config.startupPath = 'neco';
     
config.filebrowserImageBrowseUrl = '/modules/ckeditor/ckfinder/ckfinder.html?type=Images';
config.filebrowserImageUploadUrl = '/modules/ckeditor/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
};
