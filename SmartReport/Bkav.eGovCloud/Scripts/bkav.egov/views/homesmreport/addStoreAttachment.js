
define([],
    function () {

        var AddStoreAttachment = Backbone.View.extend({
            id: 'addStoreAttachment',

            initialize: function (option) {
                this.storeId = option.id;
                this.render();
                this.$('#uploadStorePrivateAttachment').unbind('click');
            },

            render: function () {
                var that = this;
                require([egov.template.tree.createStoreAttachment], function (Template) {
                    that.$el.html(Template);
                    that.initFileUploadStorePrivateAttachment();
                });
            },

            initFileUploadStorePrivateAttachment: function () {
                var that = this;
                that.$('#storePrivateAttachment').fileupload({
                    dataType: 'json',
                    autoUpload: false,
                    singleFileUploads: false,
                    add: function (e, data) {
                        var fileNames = _.pluck(data.files, "name").join("; ")
                        that.$('#attachmentName').val(fileNames);
                        that.$('#uploadStorePrivateAttachment').unbind('click');
                        that.$('#uploadStorePrivateAttachment').click(function () {
                            data.submit();
                        });
                    },
                    start: function () {
                        // egov.message.notification(egov.resources.document.attachment.uploading, egov.message.messageTypes.info);
                        egov.pubsub.publish(egov.events.status.processing, egov.resources.document.attachment.uploading);
                    },
                    stop: function () {
                        that.$el.dialog('destroy');
                    },
                    done: function () {
                        if (egov.views
                            && egov.views.home.tree
                            && egov.views.home.tree.storeTree
                            && egov.views.home.tree.storeTree.storeModelSelected) {
                            egov.views.home.tree.storeTree.storeModelSelected.select();
                        }

                        // egov.message.success(egov.resources.document.attachment.uploadSuccess);
                        egov.pubsub.publish(egov.events.status.success, egov.resources.document.attachment.uploadSuccess);
                        that.$el.dialog('destroy');
                    },
                    fail: function () {
                        // egov.message.notification(egov.resources.document.attachment.uploadError, egov.message.messageTypes.error, true);
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.attachment.uploadError);
                    }
                }).bind('fileuploadsubmit', function (e, data) {
                    console.log(data);
                    data.formData = {
                        id: that.storeId,
                        desc: that.$('#descStorePrivateAttachment').val(),
                        __RequestVerificationToken: $("input[name=__RequestVerificationToken]", "#StorePrivateAddAttachment").val()
                    };
                });
            },

            setFocus: function () {
                var setTextFocus = this.$('input[type=text]').first();
                if (setTextFocus && setTextFocus.length > 0) {
                    var length = setTextFocus.val() ? setTextFocus.val().length : 0;
                    setTextFocus.focus();
                    setTextFocus[0].setSelectionRange(length, length);
                }
            }
        });

        return AddStoreAttachment;
    });