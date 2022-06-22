define([], function () {
    "use strict";

    /// <summary> Class hỗ trợ lấy lại văn bản</summary>
    var RetrieveView = Backbone.View.extend({
        initialize: function (options) {
            this.docCopyId = options.docCopyId;
            this.dateCreated = options.dateCreated;
            this.callback = options.callback;
            this.open();
        },

        /// <summary> Mở form lấy lại văn bản</summary>
        /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
        open: function () {
            var _this = this;
            var settings = {
                width: 500,
                title: "Lấy lại văn bản",
                buttons: [
                    {
                        text: "Đồng ý",
                        className: 'btn-primary',
                        click: function () {
                            _this.save(function () {
                                _this.$el.dialog('destroy');
                            });
                        }
                    },
                     {
                         text: "Bỏ qua",
                         className: 'btn-close',
                         click: function () {
                             _this.$el.dialog('destroy');
                         }
                     }
                ]
            };
            var htmlAdd = "<span>Bạn có đồng ý lấy lại văn bản/hồ sơ này không?</span></br><div></div><div id='viewDetailRetrieve' style='height: 40px; overflow-y:scroll;'></div>";
            this.$el.html(htmlAdd);
            this.$el.dialog(settings);
        },

        ///<summary>Hàm xử lý chức năng lấy lại văn bản(trên contextmenu)</summary>
        save: function (callback) {
            var _this = this;
            var dateCreated = _this.dateCreated;
            egov.request.undoTransfering({
                data: { documentCopyId: _this.docCopyId, dateCreated: dateCreated },
                beforeSend: function () {
                    egov.pubsub.publish(egov.events.status.processing, "Đang xử lý!");
                },
                success: function (data) {
                    if (data.error) {
                        egov.pubsub.publish(egov.events.status.error, data.error);
                    }
                    else {
                        egov.pubsub.publish(egov.events.status.success, data.success);
                        egov.callback(_this.callback);
                        egov.callback(callback);
                    }
                },
                error: function (xhr) {
                    egov.pubsub.publish(egov.events.status.error, xhr.statusText);
                }
            });
        }
    });
    
    return RetrieveView;
});