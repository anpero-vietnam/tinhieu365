/**
 * Basic sample plugin inserting current date and time into CKEditor editing area.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_intro
 */

// Register the plugin within the editor.
CKEDITOR.plugins.add( 'addVideo', {

	// Register the icons. They must match command names.
    icons: 'addVideo',
	init: function( editor ) {

        editor.addCommand( 'addVideo', {
			exec: function( editor ) {
                addVideo();
			}
		});
		// Create the toolbar button that executes the above command.
        editor.ui.addButton( 'addVideo', {
			label: 'Nhúng video Youtube ',
            command: 'addVideo',
			toolbar: 'custom'
		});
	}
});
function addVideo() {    
    $("#youTube").modal("show");
}
$(document).ready(function () {
    $("#btnAddYoutube").click(function () {
        var link = $("#YouTubeLink").val();
        var isAddThumb = $("#isGetYtbThumb").is(":checked");               
        var i = link.lastIndexOf("/");
        var t = link.substring(i, link.length).replace("/", "").replace("watch?v=", "").trim();        
        var video = '<p><iframe src="https://www.youtube.com/embed/'+t+'" frameborder="0"></iframe></p>';
        InsertHTML(video);
        if (isAddThumb) {            
            var thumb = "https://img.youtube.com/vi/" + t.trim() + "/0.jpg";
            if ($("#prThumb").length) {
                $("#prThumb").attr("src", thumb).show(); 
            }                       
        }       
        $("#youTube").modal("hide");
    });
});