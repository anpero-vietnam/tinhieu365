/** function: small-UploadFile plugin 
* @version: 1.1.1
* @author: thang.td
* @copyright: Copyright (c) 2018 thangtd. All rights reserved.
* @license: Licensed under the MIT license. See http://www.opensource.org/licenses/mit-license.php
* @website: 
*/
(function ($) {
    var ultil = function () { };
    ultil.prototype.guidGenerator = function () {
        var S4 = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    };
    ultil.prototype.clone = function (object) {
        return $.extend({}, object);
    };

    var Ultil = new ultil();

    var smallFileUpload = function (element, options) {
        this.id = "";
        this.settings = $.extend(Ultil.clone(this.defaults), options);
        this.element = element;
        this.getUploadedLink = function (thisElement) {
            if (this.settings.uploadImmediately) {
                return JSON.stringify(this.uploadedFile);
            } else {
                //comming son
                return JSON.stringify([]);
            }
        };
        this.done = function () {
            $.ajax({
                url: "/UploadFileBase/MaskDone"
            });
        };
        this.reset = function (thisElement) {
            this.id = thisElement.attr("f-id");
            $(".view-" + this.id).html("");
            return [];
            //return JSON.stringify(linkList);
        };
        this.deleteCallback = function () { };

        this.uploadedFile = [];
    };
    smallFileUpload.prototype.settings = {};
    smallFileUpload.prototype.defaults = {
        Size: "full", //20x20, 200x200, 300x300, 400x400,600x600,1024x1024

        ReferenceId:"",
        maxSizeInKb: 1024 * 4,
        allowExtension: ".doc,.docx,.xls,.xlsx,.ppt,.pptx,.jpg,.jpeg,.png,.gif,.zip, .pdf, .svg,.rar,.ico",
        privewType: "html",
        url: "/FileBase",
        multiple: false,
        uploadImmediately: true,
        deleteUrl: "/FileBase/Delete",
        success: function (uploadedUrl) { },
        onError: function (msg) {
          Common.showNotice("Lỗi tải file", msg);
        },
        onUpload: function () { },
        isTempFile: false,
        containerElement: "#file-list-contain",
        itemTemplate: "<li class=\"main-list-file clearfix\">\
                            <a href = \"{fileLink}\" class=\"name-file title-sm icon-default icon-file-5 title-sm title-heading-3 f-link link\">\
                            {fileName}<span class=\"format-file\">({fileExtenstion})</span>\
                           </a>\
                          <span class=\"close-file icon-default icon-close-blue f-remove\"> Remove</span>\
                    </li>"
    };
    smallFileUpload.prototype.init = function () {
        var instance = this;
        var element = this.element;
        var newId = Ultil.guidGenerator();

        instance.id = newId;
        if (this.element.attr("f-id")) {
            instance.id = element.attr("f-id");
        } else {
            this.element.attr("f-id", newId);
        }
        if (this.element[0].type != "file") {
            if (this.settings.multiple) {
                element.after('<input type=\"file\" data-id=\"' + instance.id + '\" style=\"display:none;\" multiple>');
                element.css("cursor", "pointer");
            } else {
                element.after('<input type=\"file\" data-id=\"' + instance.id + '\" style=\"display:none;\" >');
                  
            }
            
            element.click(function () { $("[data-id='" + instance.id + "']").click(); });
        } else {
            element.attr("data-id", instance.id);
            if (this.settings.multiple) {
                element.prop("multiple", true);
            }
        }
        element.after('<div class=\"view-' + instance.id + '\"></div>');            
        if (this.settings.containerElement != null) {
            $(instance.settings.containerElement).addClass("view-" + newId);
        }
        $("[data-id='" + instance.id + "']").change(function () {
            if (instance.settings.uploadImmediately) {
                instance.doUpload();
            }
        });


    };
    smallFileUpload.prototype.ValidateSize = function (fileUpload) {
        var inst = this;
        var rs = true;
        if (fileUpload && fileUpload.files && fileUpload.files instanceof FileList) {  
            [].forEach.call(fileUpload.files, function (f) {
                if (Math.round(f.size / 1024) > inst.settings.maxSizeInKb) {
                    rs = false;
                }
            });
        }
        return rs;
    };
    smallFileUpload.prototype.ValidateFileExtenstion = function () {
        var instance = this;
        var oInput = $("[data-id='" + instance.id + "']").get(0);
        var allowExtension = this.settings.allowExtension.split(",");
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            if (sFileName.length > 0) {
                var blnValid = false;
                
                const lastDot = sFileName.lastIndexOf('.');
                const ext ="."+ sFileName.substring(lastDot + 1);
                for (var j = 0; j < allowExtension.length; j++) {
                    var sCurExtension = allowExtension[j];
                    if (ext.toLowerCase().trim() == sCurExtension.toLowerCase().trim()) {
                        blnValid = true;
                        break;
                    }
                }
                if (!blnValid) {                    
                    return false;
                }
            }
        }

        return true;
    };
    smallFileUpload.prototype.doUpload = function () {
        
        var instance = this;
        $(".f-err").remove();
        var fileUpload = $("[data-id='" + instance.id + "']").get(0);
        var valid = true;

        var files = fileUpload.files;
        var data = new window.FormData();

        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                data.append("mediaFile", files[i]);
            }
        }        
        data.append("__RequestVerificationToken", $("input[name=__RequestVerificationToken]").val());
        data.append("isTempFile", instance.settings.isTempFile);
        data.append("Size", instance.settings.Size);
        if (!instance.ValidateSize(fileUpload)) {
            valid = false;
            instance.settings.onError("Dung lượng file không hợp lệ.Dung lượng file không được quá " + instance.settings.maxSizeInKb + "KB");
        }
        if (!instance.ValidateFileExtenstion()) {
            valid = false;
            instance.settings.onError("Định dạng file không hợp lệ. Bạn chỉ có thể tải những file sau " + instance.settings.allowExtension + " files.");

        }
        if (fileUpload.value == null || fileUpload.value == "")
            valid = false;
        //}
        if (valid) {
            instance.settings.onUpload();

            var loadingCt = "<div class=\"f-temp\"><div class=\"f-loading-bar\"><div></div></div></div>";
            $('#showUpload').modal('show');
            $(".view-" + instance.id).append(loadingCt);
            var options = data;
            options.url = instance.settings.url;
            options.type = 'POST';
            options.data = data;
            options.dataType = "json";
            
            options.contentType = 'multipart/form-data';
            options.contentType = false;
            options.processData = false;
            options.success = function (result) {                
               
                if (result.code == 200 && result.uploadedImages.length>0) {
                    
                    for (var i = 0; i < result.uploadedImages.length; i++) {
                        var item = result.uploadedImages[i];
                        if (item.Url != "" && item.Url != null) {
                            instance.uploadedFile.push(item);
                            //if (instance.settings.containerElement && instance.settings.containerElement != "") {
                                //item.filename = instance.getNewFileName(item.filename);
                                //var fileExtenstion = item.url.split(".")[item.url.split(".").length - 1].toUpperCase();
                                //var imgContent = instance.settings.itemTemplate.replace("{fileLink}", item.url).replace("{fileName}", item.filename).replace("{fileExtenstion}", fileExtenstion);
                                //instance.uploadedFile.push(item);
                                //$(".view-" + instance.id).append(imgContent);
                            //}
                        }
                    }
                    $("[data-id='" + instance.id + "']").val(null);
                }
                //else if (result.msg != "") {
                //    instance.settings.onError(result.msg);
                //}
                $('#showUpload').modal('hide');
                $(".f-temp").remove();
                instance.settings.success(result);
                if ($(".f-remove").length) {
                    $(".f-remove").unbind("click");
                    $(".f-remove").click(function () {
                        $(this).parent().remove();
                        var link = $(this).siblings(".f-link").attr("href"); 
                        if (link == null || link == "") {
                            link = $(this).siblings(".f-link").attr("src"); 
                        }
                        instance.delete(link);
                    });
                }
                $("#" + instance.id).val(null);
            };
            $.ajax(options);
        }

    };
    smallFileUpload.prototype.getNewFileName = function (oldFileName) {
        var instance = this;
        var item = instance.uploadedFile;
        var _extractFileExtension = function (fullName) {
            var temp = fullName.split('.');
            var _temp = [];
            for (var i = 0; i < temp.length - 1; i++) {
                _temp.push(temp[i]);
            };
            return {
                name: _temp.join('.'),
                extenstion: temp[temp.length - 1]
            };
        };
        thisFile = oldFileName;
        var fileName = thisFile;
        if (instance.uploadedFile && instance.uploadedFile.length > 0) {
            var listExisted = [];
            [].forEach.call(instance.uploadedFile, function (uf) {
                listExisted.push(uf.filename);
            });
            var i = 1;
            var _info = _extractFileExtension(fileName);
            while (listExisted.indexOf(fileName) !== -1) {
                fileName = _info.name + ' (' + i + ').' + _info.extenstion;
                i = i + 1;
            }
            thisFile = fileName;
        }
        return thisFile;
    };
    smallFileUpload.prototype.delete = function (_filePath) {
        if (_filePath.indexOf("https://img.youtube.com" >= 0)) {
            var instance = this;
            var options = new window.FormData();
            options.url = instance.settings.deleteUrl;
            options.type = 'POST';
            options.data = { filePath: _filePath, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() };
            options.success = function (result) {
                if (instance.uploadedFile != null) {
                    for (var i = 0; i < instance.uploadedFile.length; i++) {
                        if (instance.uploadedFile[i].Url.toLowerCase() == _filePath.toLowerCase()) {
                            instance.uploadedFile.splice(i, 1);
                        }
                    }
                }
                if (typeof _data != "undefined" && typeof _data.Images != "undefined") {
                    _data.Images = "";
                }
            };
            $.ajax(options);
        } else {
            if (instance.uploadedFile != null) {
                for (var i = 0; i < instance.uploadedFile.length; i++) {
                    if (instance.uploadedFile[i].Url.toLowerCase() == _filePath.toLowerCase()) {
                        instance.uploadedFile.splice(i, 1);
                    }
                }
            }
        }
       
    };
    $.fn.smallFileUpload = function (options) {
        var optionsType = typeof options;
        var returnObject = [];
        this.each(function () {
            var $this = $(this);
            var SmallFileUpload = new smallFileUpload($this, options);            
            SmallFileUpload.init();
            returnObject.push(SmallFileUpload);
            
        });
        return returnObject[0];
    };
}(jQuery));
