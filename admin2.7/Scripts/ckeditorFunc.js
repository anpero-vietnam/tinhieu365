function InsertText(value) {
    // Get the editor instance that we want to interact with.
    var editor = CKEDITOR.instances.editor1;

    if (editor.mode == 'wysiwyg') {
        // Insert as plain text.
        // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-insertText
        editor.insertText(value);
    }
    else
        alert('You must be in WYSIWYG mode!');
}
function InsertHTML(value) {
    // Get the editor instance that we want to interact with.
    var editor = CKEDITOR.instances.editor1;


    // Check the active editing mode.
    if (editor.mode == 'wysiwyg') {
        // Insert HTML code.
        // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-insertHtml
        editor.insertHtml(value);
    }
    else
        alert('You must be in WYSIWYG mode!');
}
function GetContents() {
    // Get the editor instance that you want to interact with.
    var editor = CKEDITOR.instances.editor1;

    // Get editor contents
    // http://docs.ckeditor.com/#!/api/CKEDITOR.editor-method-getData
    alert(editor.getData());
}
$(document).ready(function () {


    $('#view').append('<div id="top">Back to Top</div>');
});
$(window).scroll(function () { 
    if ($(window).scrollTop() != 0) { $('#top').fadeIn(); } else { $('#top').fadeOut(); } }); $('#top').click(function () { $('html, body').animate({ scrollTop: 0 }, 500); }); function gotop() { $('html, body').animate({ scrollTop: 0 }, 500); }
function toURLEncode(s) { s = encodeURIComponent(s); s = s.replace(/\~/g, '%7E').replace(/\!/g, '%21').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\'/g, '%27'); s = s.replace(/%20/g, '+'); return s; };
function toURLDecode(s) { s = s.replace(/\+/g, '%20'); s = decodeURIComponent(s); return s; }
function ValidURL(str) {
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
    '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
    '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
    '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
    '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
    '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
    if (!pattern.test(str)) {
        return false;
    } else {
        return true;
    }
}
function ValidImagesURL(str) {
    var pattern = new RegExp('https?://(?:[a-z\-\.]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg)$', 'i'); // fragment locator
    if (!pattern.test(str)) {
        return false;
    } else {
        return true;
    }
}
    