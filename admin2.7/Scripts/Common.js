(function () {
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
                                    if (['text', 'password', 'hidden', 'email', 'number', 'file'].indexOf(input.type) != -1) {
                                        collectedNames.push(name);
                                        dataObject[name] = input.value;
                                    } else if (['radio', 'checkbox'].indexOf(input.type) != -1) {
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
                    })
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
})();


