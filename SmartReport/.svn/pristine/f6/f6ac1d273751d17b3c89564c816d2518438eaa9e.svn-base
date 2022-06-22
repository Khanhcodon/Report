define([],
    function () {
        "use strict";
        
        /// <summary> Class hỗ trợ loại bỏ văn bản khỏi hồ sơ</summary>
        /// <param name="docCopyId" type="String">Id văn bản copy</param>
        var RemoveView = Backbone.View.extend({
            initialize: function (options) {
                this.frame = options.frame;
                this.documentCopyIds = options.documentCopyIds;
                this.render();
            },

            render: function () {
                var _this= this;
                egov.message.show(
                    'Bạn có đồng ý loại bỏ văn bản/hồ sơ này không?',
                    null,
                    egov.message.messageButtons.YesNo,
                    function () {
                        var token = $("input[name='__RequestVerificationToken']", "#DocumentRemoveDocument").val();
                        $.ajax({
                            type: "POST",
                            url: '/Document/RemoveDocument',
                            traditional: true,
                            data: {
                                documentCopyIds: _this.documentCopyIds,
                                __RequestVerificationToken: token
                            },
                            success: function (data) {
                                if (data.success) {
                                    if (egov.currentDocumentObject && typeof egov.currentDocumentObject !== 'undefined') {
                                        egov.currentDocumentObject.clientRemove(documentCopyIds);
                                    }

                                    if (_this.frame && typeof _this.frame !== 'undefined') {
                                       
                                    }

                                    egov.message.notification(data.success, egov.message.messageTypes.success);
                                } else {
                                    egov.message.show(data.error);
                                }
                            },
                            error: function () {
                                egov.message.notification("Có lỗi trong quá trình loại bỏ văn bản/hồ sơ.", egov.message.messageTypes.error);
                            }
                        });
                    }
                );
            }
        });

        return RemoveView;
    });