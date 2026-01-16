var newpost = function () {
 var handleTagsInput = function () {
        if (!jQuery().tagsInput) {
            return;
        }
        $('#tags_id').tagsInput({
            width: 'auto',
            'onAddTag': function () {
                //alert(1);
            },
        });
    }
 return {
        //main function to initiate the module
        init: function () {
            handleTagsInput();
        }
    };
} ();

