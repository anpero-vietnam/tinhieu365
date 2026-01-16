/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'vi';
    config.allowedContent = true;
    // config.uiColor = '#AADC6E';
    config.toolbarGroups = [
        { name: 'clipboard', groups: ['clipboard'] },
        { name: 'insert' }, // ảnh, table, flash, icon 

        { name: 'document', groups: ['mode'] },
        { name: 'links' },

        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'colors' },
        { name: 'paragraph', groups: ['list', 'align'] },
        { name: 'styles' },
        { name: 'custom' }

    ];
    config.skins = 'moono';
    config.extraPlugins = 'addVideo,imgbutton';
};