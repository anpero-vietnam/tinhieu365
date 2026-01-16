//var datas = $('#ec-search-user').collectData();
//datas.POST().to("/ec/searchUser").then(function (data) {
//    alert(data);
//});
//datas.GET('json').to("/ec/searchUser").then(function (data) {
//    alert(data);
//});
(function () {
    Object.defineProperty(Object.prototype, 'toFormData', {
        value: function toFormData() {
            if (this instanceof Object) {
                var o = this;
                var d = new FormData();
                for (var p in o) {
                    if (o[p]) {
                        if (o[p].length && o[p].length > 0 && o[p][0].size && o[p][0].size > 0) {
                            if (o[p].length == 1) {
                                d.append(p, o[p][0]);
                            } else {
                                for (var i = 0; i < p[p].length; i++) {
                                    d.append(p, o[p][i]);
                                }
                            }
                        } else {
                            d.append(p, o[p]);
                        }
                    }
                }
                return d;
            } else {
                return new FormData();
            }
        },
        writable: true
    });
    Object.defineProperty(Object.prototype, 'collectData', {
        value: function collectData() {
            var root = this;
            if (root instanceof Element) {  // 'this' is result of document.getElementById('...')
                return root.collectDataAsObject().toFormData();
            } else if (root.length && root.length > 0 && (root[0] instanceof Element)) {   // 'this' is result of $('#...')
                return root[0].collectDataAsObject().toFormData();
            } else {
                return new FormData();
            }
        },
        writable: true
    });
      Object.defineProperty(Object.prototype, 'collectDataAsObject', {
            value: function collectDataAsObject() {
                var root = null;
                if (this instanceof Element) { // 'this' is result of document.getElementById('...')
                    root = this;
                } else if (this.length && this.length > 0 && (this[0] instanceof Element)) {   // 'this' is result of $('#...')
                    root = this[0];
                }
                if (root instanceof Element) {
                    var dataObject = {};
                    var collectedNames = [];
                    var list = root.querySelectorAll('input');
                    if (list && list.length > 0) {
                        [].forEach.call(list, function (input) {
                            try {
                                if (input.attributes['name']) {
                                    var name = input.attributes['name'].value;
                                    if (collectedNames.indexOf(name) == -1) {
                                        if (['text', 'password', 'hidden', 'email', 'number'].indexOf(input.type) != -1) {
                                            collectedNames.push(name);
                                            dataObject[name] = input.value;
                                        } else if (['file'].indexOf(input.type) != -1) {
                                            collectedNames.push(name);
                                            if (input.files && input.files.length > 0) {
                                                if (input.multiple) {
                                                    dataObject[name] = input.files;
                                                } else {
                                                    dataObject[name] = input.files[0];
                                                }
                                            }
                                        }
                                        else if (['radio', 'checkbox'].indexOf(input.type) != -1) {
                                            var isSingle = (root.querySelectorAll('[name=' + name + ']').length == 1);
                                            if (isSingle) {
                                                dataObject[name] = input.checked;
                                            } else {
                                                var checked = root.querySelectorAll('[type=' + input.type + '][name=' + name + ']:checked');
                                                if (checked && checked.length && checked.length > 0) {
                                                    var listVal = [];
                                                    [].forEach.call(checked, function (c) {
                                                        listVal.push(c.value);
                                                    });
                                                    collectedNames.push(name);
                                                    dataObject[name] = listVal;
                                                } else {
                                                    collectedNames.push(name);
                                                }
                                            }
                                        }
                                    }
                                }
                            } catch (e) {
                                console.log(e);
                            }
                        })
                    }
                    list = root.querySelectorAll('select');
                    if (list && list.length > 0) {
                        [].forEach.call(list, function (input) {
                            try {
                                if (input.attributes['name'] && collectedNames.indexOf(input.attributes['name'].value) == -1) {
                                    collectedNames.push(input.attributes['name'].value);
                                    dataObject[input.attributes['name'].value] = input.value;
                                }
                            } catch (e) {
                                console.log(e);
                            }
                        });
                    }
                    list = root.querySelectorAll('textarea');
                    if (list && list.length > 0) {
                        [].forEach.call(list, function (input) {
                            try {
                                if (input.attributes['name'] && collectedNames.indexOf(input.attributes['name'].value) == -1) {
                                    collectedNames.push(input.attributes['name'].value);
                                    dataObject[input.attributes['name'].value] = input.value;
                                }
                            } catch (e) {
                                console.log(e);
                            }
                        })
                    }
                    return dataObject;
                } else {
                    return {};
                }
            },
            writable: true
        });
    Object.defineProperty(Object.prototype, 'toObject', {
        value: function toObject() {
            var o = this;
            if (o && (o instanceof FormData)) {
                try {
                    var data = JSON.stringify(o);
                    return JSON.parse(data);
                } catch (e) {
                    console.log(e);
                    return {};
                }
            } else if (o && (o instanceof Object)) {
                try {
                    return o;
                } catch (e) {
                    console.log(e);
                    return {};
                }
            } else {
                return {};
            }
        },
        writable: true
    });
    Object.defineProperty(String.prototype, 'toObject', {
        value: function toObject() {
            var o = this;
            if (o && (typeof (o)) == 'string') {
                try {
                    var tmp = JSON.parse(o);
                    return o;
                } catch (e) {
                    console.log(e);
                    return {};
                }
            } else {
                return {};
            }
        },
        writable: true
    });
    Object.defineProperty(Object.prototype, 'toUrlEncode', {
        value: function toUrlEncode(url) {
            var obj = this.toObject();
            var rs = encodeURI(url);
            if (obj) {
                for (var prop in obj) {
                    if (rs.indexOf('?') != -1) {
                        rs = rs + '&' + prop + '=' + encodeURI(obj[prop]);
                    } else {
                        rs = rs + '?' + prop + '=' + encodeURI(obj[prop]);
                    }
                }
            }
            return rs;
        },
        writable: true
    });
    Object.defineProperty(Object.prototype, 'bindTo', {
        value: function bindTo(el) {
            if (el && (el instanceof Element)) {
                for (var p in this) {
                    try {
                        var value = this[p];
                        var inputs = el.querySelectorAll('[name=' + p + ']');
                        if (inputs && inputs.length > 0) {
                            if (inputs.length == 1) {
                                if (['radio', 'checkbox'].indexOf(inputs[0].type) != -1
                                    && (typeof (value)) == 'boolean') {
                                    inputs[0].checked = value;
                                } else if (['text', 'number', 'password', 'email', 'hidden'].indexOf(inputs[0].type) != -1
                                    || ['textarea', 'select'].indexOf(inputs[0].nodeName.toLowerCase()) != -1) {
                                    inputs[0].value = value;
                                }
                            } else {
                                if (['radio', 'checkbox'].indexOf(inputs[0].type) != -1
                                    && (value instanceof Array)) {
                                    if (value.length > 0) {
                                        [].forEach.call(inputs, function (input) {
                                            input.checked = false;
                                        });
                                        [].forEach.call(value, function (v) {
                                            try {
                                                el.querySelectorAll('[name=' + p + '][value=\'' + v + '\']')[0].checked = true;
                                            } catch (e) {
                                                console.log(e);
                                            }
                                        });
                                    } else {
                                        [].forEach.call(inputs, function (input) {
                                            input.checked = false;
                                        });
                                    }
                                }
                            }
                        }
                    } catch (e) {
                        console.log(e);
                        continue;
                    }
                }
            }
        },
        writable: true
    });
    var diagnostic = {
        contentType: function (input) {
            if (input && (typeof (input)) == 'string') {
                if (input.indexOf('urlencoded') != -1) {
                    return 'application/x-www-form-urlencoded; charset=utf-8';
                } else if (input.indexOf('json') != -1) {
                    return 'application/json; charset=utf-8';
                } else {
                    return null;
                }
            } else {
                return null;
            }
        },
        dataType: function (input) {
            if (input && (typeof (input)) == 'string') {
                if (input.indexOf('html') != -1) {
                    return 'application/x-www-form-urlencoded; charset=utf-8';
                } else if (input.indexOf('json') != -1) {
                    return 'application/json; charset=utf-8';
                } else if (input.indexOf('urlencoded')) {
                    return null;
                } else {
                    return null;
                }
            } else {
                return null;
            }
        },
        response: function (type, res) {
            if (type && (typeof (type)) == 'string') {
                if (['xml', 'html', 'text'].indexOf(type.trim().toLowerCase()) != -1) {
                    return res;
                } else if (type.toLowerCase().indexOf('json') != -1) {
                    return JSON.parse(res);
                } else {
                    return res;
                }
            } else {
                try {
                    var rs = JSON.parse(res);
                    return rs;
                } catch (e) {
                    return res;
                }
            }
        },
        data: function (obj, type) {

            var _toStringifyJSON = function (o) {
                if (o && (o instanceof Element)) {
                    try {
                        var _o = o.collectData();
                        var data = JSON.stringify(_o);
                        return data;
                    } catch (e) {
                        console.log(e);
                        return JSON.stringify(new FormData());
                    }
                } else if (o && (o instanceof FormData)) {
                    try {
                        var data = JSON.stringify(o);
                        return data;
                    } catch (e) {
                        console.log(e);
                        return '';
                    }
                } else if (o && (o instanceof Object)) {
                    try {
                        return JSON.stringify(o);
                    } catch (e) {
                        console.log(e);
                        return '';
                    }
                } else if (o && (typeof (o)) == 'string') {
                    try {
                        var tmp = JSON.parse(o);
                        return o;
                    } catch (e) {
                        console.log(e);
                        return null;
                    }
                } else {
                    return null;
                }
            }

            var _toFormData = function (o) {
                if (o && (o instanceof Element)) {
                    o.collectData();
                } else if (o && (o instanceof FormData)) {
                    return o;
                } else if (o && (o instanceof Object)) {
                    return o.toFormData();
                } else if (o && (typeof (o)) == 'string') {
                    try {
                        var tmp = JSON.parse(o);
                        return tmp.toFormData();
                    } catch (e) {
                        console.log(e);
                        return new FormData();
                    }
                } else {
                    return new FormData();;
                }
            }

            if (type && (typeof (type)) == 'string') {
                if (type.indexOf('json') != -1) {
                    return _toStringifyJSON(obj);
                } else if (type.indexOf('form-data') != -1) {
                    return _toFormData(obj);
                } else if (type.indexOf('urlencoded') != -1) {
                    return null;
                }
            } else {
                return _toFormData(obj);
            }

        }
    };
    var _exec = function (f) {
        if (f && (typeof (f)) == 'function') {
            try {
                f();
            } catch (e) {
                console.log(e);
            }

        }
    }
    var _post = function (config) {
        return new Promise(function (resolve, reject) {

            if (config && config.url && (typeof (config.url)) == 'string' && config.url.trim()) {            
                var xhttp = new XMLHttpRequest() || ActiveXObject();
                xhttp.onreadystatechange = function () {
                    if (this.readyState == 4 && this.status == 200) {
                        _exec(config._done);
                        //var res = diagnostic.response(config.dataType, );
                        resolve(this.responseText);
                    } else if (this.readyState == 4 && this.status != 201 && this.status != 200) {
                        _exec(config._done);
                        reject(this.status);
                    }
                };

                var contentType = '';
                if (config.contentType && (typeof (config.contentType)) == 'string') {
                    contentType = diagnostic.contentType(config.contentType);
                }

                var url = config.url;
                if (contentType && contentType.indexOf('urlencoded') != -1) {
                    url = config.data.toUrlEncode(url);
                }

                data = diagnostic.data(config.data, contentType);

                if (config._username && config._password) {
                    xhttp.open('POST', url, true, config._username, config._password);
                } else {
                    xhttp.open('POST', url, true);
                }

                if (contentType) {
                    xhttp.setRequestHeader('Content-Type', contentType);
                }
                if (config._authorization) {
                    xhttp.setRequestHeader("Authorization", config._authorization);
                }

                if (config._header && (config._header instanceof Array) && config._header.length > 0) {
                    for (var i = 0; i < config._header.length; i++) {
                        xhttp.setRequestHeader(config._header[i]['key'], config._header[i]['value']);
                    }
                }

                xhttp.send(data);
            } else {
                reject(404);
            }
        });
    };
    var _get = function (config) {
        return new Promise(function (resolve, reject) {

            if (config && config.url && (typeof (config.url)) == 'string' && config.url.trim()) {
                var url = encodeURI(config.url);
                if (config.dataType && (typeof (config.dataType)) == 'string' && config.dataType.trim().toLowerCase() == 'file') {

                    fetch(url).then(function (res) {
                        _exec(config._done);
                        resolve(res);
                    }).catch(function () {
                        _exec(config._done);
                        reject(404);
                    });
                } else {

                    var xhttp = new XMLHttpRequest() || ActiveXObject();
                    xhttp.onreadystatechange = function () {
                        if (this.readyState == 4 && this.status == 200) {
                            _exec(config._done);
                            var res = diagnostic.response(config.dataType, this.responseText);
                            resolve(res);
                        } else if (this.readyState == 4 && this.status != 201 && this.status != 200) {
                            _exec(config._done);
                            reject(this.status);
                        }
                    };

                    if (config._username && config._password) {
                        xhttp.open('GET', url, true, config._username, config._password);
                    } else {
                        xhttp.open('GET', url, true);
                    }

                    if (config._authorization) {
                        xhttp.setRequestHeader("Authorization", config._authorization);
                    }

                    if (config._header && (config._header instanceof Array) && config._header.length > 0) {
                        for (var i = 0; i < config._header.length; i++) {
                            xhttp.setRequestHeader(config._header[i]['key'], config._header[i]['value']);
                        }
                    }

                    xhttp.send();
                }

            } else {
                reject(404);
            }
        });
    };


    var ajaxConfig = function (type, data) {
        this.url = '';

        if (type && (typeof (type)) == 'string' && ['POST', 'GET'].indexOf(type) != -1) {
            this.type = type;
        } else {
            this.type = 'POST'; // 'POST', 'GET'
        }

        if (data) {
            this.data = data;
        } else {
            this.data = new FormData();
        }

        this.contentType = 'form-data';
        this.dataType = 'text';
        this._done = null;
        
        this._header = [];
        this._authorization = null;
        this._username = null;
        this._password = null;
        this._respone = null;
    };

    ajaxConfig.prototype.as = function (type) {
        var inst = this;
        if (type && (typeof (type)) == 'string' && ['form-data', 'json', 'text', 'html', 'file', 'xml', 'urlencoded'].indexOf(type) != -1) {
            if (inst.type == 'POST') {
                inst.contentType = type;
            } else if (inst.type == 'GET') {
                inst.dataType = type;
            }
        }
        return inst;
    };
    ajaxConfig.prototype.authorization = function (tokenString) {
        var inst = this;
        if (tokenString && (typeof (tokenString)) == 'string') {
            inst._authorization = tokenString;
        }
        return inst;
    };
    ajaxConfig.prototype.user = function (username, password) {
        var inst = this;
        if (username && (typeof (username)) == 'string' && password && (typeof (password)) == 'string') {
            inst._username = username;
            inst._password = password;
        }
        return inst;
    }
    ajaxConfig.prototype.header = function (p1, p2) {
        var inst = this;
        if (p1 && (typeof (p1)) == 'string' && p2 && (typeof (p2)) == 'string') {
            var hdr = { key: p1, value: p2 };
            inst._header.push(hdr);
        } else if ((p1 instanceof Array) && p1.length > 0) {
            for (var i = 0; i < p1.length; i++) {
                try {
                    if (p1[i]['key'] && (typeof (p1[i]['key'])) == 'string' && p1[i]['value'] && (typeof (p1[i]['value'])) == 'string') {
                        var hdr = { key: p1[i]['key'], value: p1[i]['value'] };
                        inst._header.push(hdr);
                    }
                } catch (e) {
                    console.log(e);
                    continue;
                }
            }
        }
        return inst;
    }
    ajaxConfig.prototype.done = function (fn) {
        var inst = this;
        if (fn && (typeof (fn)) == 'function') {
            inst._done = fn;
        }
        return inst;
    }
    ajaxConfig.prototype.to = function (url) {
        var inst = this;

        if (url && (typeof (url)) == 'string' && inst.type == 'POST') {
            inst.url = url;
            return _post(inst);
        } else {
            return new Promise(function (resolve, reject) { });
        }
    }
    ajaxConfig.prototype.from = function (url) {
        var inst = this;

        if (url && (typeof (url)) == 'string' && inst.type == 'GET') {
            inst.url = url;
            return _get(inst);
        } else {
            return new Promise(function (resolve, reject) { });
        }
    };
    Object.defineProperty(String.prototype, 'POST', {
        value: function POST(type) {
            var data = this + '';
            var config = new ajaxConfig('POST', data);
            if (type && (typeof (type)) == 'string'
                && ['json', 'text', 'xml', 'html', 'file'].indexOf(type.toLowerCase().trim()) != -1) {
                config.contentType = type;
            }
            return config;
        },
        writable: true
    });

    Object.defineProperty(Object.prototype, 'POST', {
        value: function POST(type) {
            var data = this;
            var config = new ajaxConfig('POST', data);
            if (type && (typeof (type)) == 'string'
                && ['json', 'form-data', 'urlencoded'].indexOf(type.toLowerCase().trim()) != -1) {
                config.contentType = type;
            }
            return config;
        },
        writable: true
    });

    Object.defineProperty(Object.prototype, 'GET', {
        value: function GET(type) {
            var link = this + '';
            var config = new ajaxConfig('GET');
            config.url = link;
            if (type && (typeof (type)) == 'string'
                && ['json', 'text', 'xml', 'html', 'file'].indexOf(type.toLowerCase().trim()) != -1) {
                config.dataType = type;
            } else {
                config.dataType = 'text';
            }
            return config;
        },
        writable: true
    });

})();
