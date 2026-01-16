
var Search = (function () {
    
    var Search = function (params) {
        if (typeof (params) !== "undefined") {
            if (typeof (params.keyWord) !== "undefined") {
                this.keyWord = params.keyWord;
            } else {
                this.keyWord = "";
            }
            if (typeof (params.container) != "undefined") {
                this.container = params.container;
            } else {
                this.container = "";
            }
        }
        
    }    
    var SearchContact = function () {
        var datas = {};
        datas.detail = this.keyWord;
        datas.st = Anpero.CurentStore;
        var valid = true;
        var ajax = new Ajax({
            url: "/AddressBook/Search",
            data: datas
        });
        var data = ajax.Execute();
        if (typeof (this.container) != "undefined") {
            $(this.container).html(data);
        }
        //var promise = new Promise(function (resolve, reject) {
        //    resolve(ajax.Execute());
        //});
        //promise.then(function (data) {
        //    debugger
        //    if (typeof (this.container) != "undefined") {
        //        $(this.container).html(data);
        //    }
        //});

    };
    Search.prototype = {
        searchContact: SearchContact,
        keyWord: this.keyWord,
        container: this.container
    };
    return Search;
})()
