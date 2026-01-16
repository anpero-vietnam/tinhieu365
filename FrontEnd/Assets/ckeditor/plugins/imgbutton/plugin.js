/**
 * Basic sample plugin inserting current date and time into CKEditor editing area.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_intro
 */

// Register the plugin within the editor.
CKEDITOR.plugins.add( 'imgbutton', {

	// Register the icons. They must match command names.
	//icons: 'imgbutton',

	// The plugin initialization logic goes inside this method.
    icons: 'imgbutton',
	init: function( editor ) {

		// Define an editor command that inserts a imgupload.
		editor.addCommand( 'imgbutton', {

			// Define the function that will be fired when the command is executed.
			exec: function( editor ) {
				callUpload();
			}
		});
		// Create the toolbar button that executes the above command.
		editor.ui.addButton( 'imgbutton', {
			label: 'Tải ảnh lên',
			command: 'imgbutton',
            toolbar: 'custom'
            
		});
	}
});

function callUpload(){
    $('#file2').trigger('click');
}
$(document).ready(function () {
    $("#file2").smallFileUpload({
		Size: "1024x1024",
		multiple: true,
		dataType: "json",
		allowExtension: ".mp3, .mp4, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .jpg, .jpeg, .png, .gif, .zip, .pdf, .svg,.rar,.ico",
		success: function (rs) {
			if (rs !== "") {
				var imgTag = "";
				if (rs.code === 200) {
					rs.uploadedImages.forEach(function (imgesLink) {
						if (imgesLink.Url.indexOf(".pdf") > 0) {
							imgTag += '<a href="' + imgesLink.Url + '"><img src="http://cdn.anpero.com/images/38/112020/stilrent-icon-set-pd2020110501074279.jpg" /></a>';
						} else if (imgesLink.Url.indexOf(".xls") > 0) {							
							imgTag += '<a href="' + imgesLink.Url + '"><img src="http://cdn.anpero.com/images/38/112020/xlsx2020110501045090.png" /></a>';
						} else if (imgesLink.Url.indexOf(".doc") > 0 || imgesLink.Url.indexOf(".pdf") > 0) {
							imgTag += '<a href="' + imgesLink.Url + '"><img src="http://cdn.anpero.com/images/38/112020/doc2020110500564978.png" /></a>';
						} else {
							imgTag += "<a href=\"" + imgesLink.Url + "\" class=\"lazy\"><img loading=lazy src=\"" + imgesLink.Url + "\" /></a>";							
                        }
					});
					InsertHTML(imgTag);
				} else {
					showNotice("Lỗi tải file", rs.messege);
				}				
			}      
        }
    });    
});