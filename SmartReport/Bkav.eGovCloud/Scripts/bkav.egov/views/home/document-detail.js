define(function () {
    "use strict";

    var DocumentDetail = Backbone.View.extend({

        tagName: 'div',

        initialize: function (options) {
            if (!(options.model instanceof egov.models.document))
                this.model = new egov.models.document(options.model);
            else
                this.model = options.model;
            this.render();
        },

        render: function () {
            var that = this;

            egov.dataManager.getCategories({
                success: function (data) {
                    var categoryForDoctype = _.find(data, function (itm) {
                        return itm.CategoryId === that.model.get('CategoryId');
                    });
                    var categoryName = categoryForDoctype ? categoryForDoctype.CategoryName : "";
                    that.model.set('CategoryName', categoryName)
                }
            });

            egov.dataManager.getCurrentDoctypes({
                success: function (data) {
                    var doctype = _.find(data, function (itm) {
                        return itm.DocTypeId === that.model.get('DocTypeId');
                    });
                    if (doctype === undefined) {
                        // Trường hợp mở văn bản không thuộc quy trình được khởi tạo: thông báo, xin ý kiến.
                        //egov.query.getDoctype({ id: that.model.get('DocTypeId') },
                        //    function (doctype) {
                        //        that.model.set('DocTypeName', doctype.DocTypeName);
                        //        data.push(doctype);
                        //    });
                    }
                    else {
                        that.model.set('DocTypeName', doctype.DocTypeName);
                    }
                }
            });

            egov.dataManager.getAllUsers({
                success: function (data) {
                    ///Người ký duyệt
                    var userSuccess = _.find(data, function (item) {
                        return item.value == that.model.get('UserSuccessId');
                    });
                    if (userSuccess)
                        that.model.set('UserNameSuccess', userSuccess.fullname + "(" + userSuccess.username + ")")

                    ///Người trả kết quả
                    var userReturned = _.find(data, function (item) {
                        return item.value == that.model.get('UserReturnedId');
                    });
                    if (userReturned)
                        that.model.set('UserNameReturned', userReturned.fullname + "(" + userReturned.username + ")");
                }
            })

            var objectDocument = this.model.toJSON();
            // Định dạng lại các giá trị datetime của đối tượng documentitem
            Object.getOwnPropertyNames(objectDocument).forEach(function (val, idx, array) {
                var objItem = objectDocument[val];
                if (objItem && Date.parse(objItem)) {
                    //format datetime
                    objItem = Globalize.format(new Date(objItem), "dd/MM/yyyy hh:mm tt")
                }
                objectDocument[val] = objItem;
            });

            require([egov.template.document.detail], function (DocumentDetailTemplate) {
                that.$el.append($.tmpl(DocumentDetailTemplate, objectDocument));
            })
        }
    });

    return DocumentDetail;
});