define([egov.tempate.transfer.plan], function (Template) {
    "use strict";

    egov.models.action = Backbone.Model.extend({
        defaults: {
            currentNodeId: 0,
            id: "",
            isAllow: true,
            isAllowSign: false,
            isSpecial: false,
            name: "",
            nextNodeId: 0,
            priority: 0,
            userIdNext: 0,
            workflowId: 0
        }
    });
    var tranferPlan = Backbone.View.extend({
        template: Template,

        //Kiểm tra append hướng nhận chưa
        checkmouseDown: false,

        a: [],

        b: [],

        events: {
            'mousedown #dropdownAction': 'dropdownAction',
            'change #dropdownAction': 'changedropdownAction',
            'change #dropdownUserTransfer': 'dropdownUserTransfer',
            'change #dropdownActionPlan': 'dropdownActionPlan',
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.documentCopyId = options.documentCopyId || 0;
            this.doctypeId = options.doctypeId;
            this.isCreatingDocument = options.documentCopyId <= 0;
            this.destination = null;
            this._open();
            this.$el.html(this.template);
            this.$el.bindResources();

        },

        _open : function () {
            var self = this;
            var actions = [];
            var actionsPlan = [];
            var settings = {};
            settings.width = 810;
            settings.title = "eGovCloud - Dự kiến bàn giao hồ sơ, văn bản";
            settings.buttons = [ // danh sách các nút chức năng trên form bàn giao
                    {
                        text: "Đồng ý",
                        click: function () {
                            if ($('#dropdownAction').val() !== '' && $("#dropdownUserTransfer").val() !== '' && $("#dropdownActionPlan").val() !== '') {
                                var destination = egov.cshtml.transfer.getDestination();
                                if (destination) {
                                    var action = _.find(self.actions, function (item) { return item.id === $('#dropdownAction').val(); });
                                    ////var frame = document.getElementById(self.frame).contentWindow;
                                    ////frame.egov.cshtml.document.newtransferplan = {
                                    ////    transferTo: {
                                    ////        actionid: action.id,
                                    ////        workflowid: action.workflowid,
                                    ////        nextnodeid: action.nextnodeid,
                                    ////        currentnodeid: action.currentnodeid,
                                    ////        userid: parseInt($("#dropdownUserTransfer").val()),
                                    ////        isAllowSign: action.isAllowSign
                                    ////    },
                                    //    
                                    ////};
                                    //transferPlan: destination;
                                    self.$el.dialog('destroy');
                                }
                            } else {
                                egov.message.show("Bạn phải chọn hướng chuyển dự kiến");
                            }
                        }
                    },
                    {
                        text: "Bỏ qua",
                        click: function () {
                            self.$el.dialog('destroy');
                        }
                    }
            ];
            var $transfer = $('<div></div>').append($('#transferPlanTemplate').tmpl()).appendTo($('body'));
            $('#dialogTransferPlan #divTransfer').append($('#transferTemplate').tmpl());
            // self.$el.html($transfer);
            var dialog = new dialogAdapter();
            dialog.openexist(self.$el, settings, function () {
                egov.cshtml.transfer.initLayout();
                $('.dialog-center').css({ border: 'none' });
                $('.dialog-south').css({ border: 'none' });
                $('.dialog-center-south').css({ border: 'none' });
                //$('#dropdownAction').mousedown(function (e) {
                //    e.preventDefault();
                //    var data = self.isCreatingDocument ? { documentTypeId: self.doctypeId, isPhanloai: false } : { documentCopyId: self.documentCopyId };
                //    var url = self.isCreatingDocument ? "/Transfer/GetActionsCreate" : "/Transfer/GetActionsEdit";
                //    $.get(url, data, function (result) {
                //        if (result) {
                //            if (result.error) {
                //                egov.message.notification(result.error, egov.message.messageTypes.error);
                //            } else {
                //                actions = _.filter(result, function (item) { return !item.isspecial; });
                //                if (actions.length > 0) {
                //                    $.each(actions, function (i, item) {
                //                        $('#dropdownAction').append('<option value="' + item.id + '">' + item.name + '</option>');
                //                    });
                //                    $("#dropdownAction").unbind('mousedown');
                //                    $("#dropdownAction").change(function () {
                //                        var id = $(this).val();
                //                        if (id) {
                //                            var action = _.find(actions, function (item) { return item.id === id; });
                //                            if (action) {
                //                                $.get('/Transfer/GetUserByAction',
                //                                    {
                //                                        actionId: $(this).val(),
                //                                        workflowId: action.workflowid,
                //                                        documentCopyId: self.documentCopyId
                //                                    },
                //                                    function (resultUser) {
                //                                        if (resultUser) {
                //                                            $("#dropdownUserTransfer").html($("#dropdownUserTransfer option:first"));
                //                                            $.each(resultUser, function (i, item) {
                //                                                $("#dropdownUserTransfer").append('<option value="' + item.value + '">' + item.label + '</option>');
                //                                            });
                //                                        } else {
                //                                            egov.message.show('Bạn không có quyền xử lý văn bản!');
                //                                        }
                //                                    }
                //                                );
                //                            }
                //                        } else {
                //                            $("#dropdownUserTransfer").html($("#dropdownUserTransfer option:first"));
                //                            $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                //                        }
                //                    });
                //                    $("#dropdownUserTransfer").change(function () {
                //                        var id = $(this).val();
                //                        if (id) {
                //                            var action = _.find(actions, function (item) { return item.id === $("#dropdownAction").val(); });
                //                            if (action) {
                //                                $.get('/Transfer/GetActionsTransferPlan',
                //                                    {
                //                                        workflowId: action.workflowid,
                //                                        userId: id,
                //                                        currentNodeId: action.nextnodeid
                //                                    },
                //                                    function (resultActionPlan) {
                //                                        if (resultActionPlan) {
                //                                            actionsPlan = resultActionPlan;
                //                                            $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                //                                            $.each(resultActionPlan, function (i, item) {
                //                                                $("#dropdownActionPlan").append('<option value="' + item.id + '">' + item.name + '</option>');
                //                                            });
                //                                        } else {
                //                                            egov.message.show('Bạn không có quyền xử lý văn bản!');
                //                                        }
                //                                    }
                //                                 );
                //                            }
                //                        } else {
                //                            $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                //                        }
                //                    });
                //                    $("#dropdownActionPlan").change(function () {
                //                        var id = $(this).val();
                //                        if (id) {
                //                            var action = _.find(actionsPlan, function (item) { return item.id === id; });
                //                            if (action) {
                //                                $.get('/Transfer/GetUserByAction', { actionId: id, workflowId: action.workflowid, documentCopyId: 0, userId: $("#dropdownUserTransfer").val() }, function (resultUser) {
                //                                    if (resultUser) {
                //                                        egov.cshtml.transfer.databind(resultUser, action.nextnodeid, action.workflowid, action.currentnodeid, id);
                //                                        $('.dialog-center').css({ border: 'none' });
                //                                        $('.dialog-south').css({ border: 'none' });
                //                                        $('.dialog-center-south').css({ border: 'none' });
                //                                    } else {
                //                                        egov.message.show('Bạn không có quyền xử lý văn bản!');
                //                                    }
                //                                });
                //                            }
                //                        } else {

                //                        }
                //                    });
                //                    //$("#dropdownAction").simulate('mousedown');
                //                }
                //            }
                //        }
                //    });
                //});
            },
            function () {
                if (egov.cshtml.transfer.dialogLayout) {
                    egov.cshtml.transfer.dialogLayout = null;
                }
            });
        },

        dropdownAction: function () {
            var test = [];
            if (!this.checkmouseDown)
            {
                var self = this;
                var data = self.isCreatingDocument ? { documentTypeId: self.doctypeId, isPhanloai: false } : { documentCopyId: self.documentCopyId };
                var url = self.isCreatingDocument ? "/Transfer/GetActionsCreate" : "/Transfer/GetActionsEdit";
                $.get(url, data, function (result) {
                    if (result) {
                        if (result.error) {
                            egov.message.notification(result.error, egov.message.messageTypes.error);
                        } else {
                            var actions = _.filter(result, function (item) { return !item.isspecial; });
                            if (actions.length > 0) {
                                test = actions;
                                $.each(actions, function (i, item) {
                                    $('#dropdownAction').append('<option value="' + item.id + '">' + item.name + '</option>');
                                });
                                $("#dropdownAction").unbind('mousedown');
                            }
                        }
                    }
                });
            }
            if (test.length > 0) {
                this.a = test;
            }
            this.checkmouseDown = true;
        },

        changedropdownAction: function () {
            var self = this;
            var id = $("#dropdownAction").val();
            if (id) {
                var action = _.find(self.a, function (item) { return item.id === id; });
                if (action) {
                    $.ajax({
                        url: '/Transfer/GetUserByAction',
                        type: 'Get',
                        datatype: "json",
                        data: {
                            actionId: id,
                            workflowId: action.workflowId,
                            documentCopyId: self.documentCopyId
                        },
                        success: function (resultUser) {
                            $("#dropdownUserTransfer").html($("#dropdownUserTransfer option:first"));
                            $.each(resultUser, function (i, item) {
                                $("#dropdownUserTransfer").append('<option value="' + item.value + '">' + item.label + '</option>');
                            });
                        },
                        error: function (xhr, status, error) {
                            egov.message.show('Bạn không có quyền xử lý văn bản!');
                        }
                    });
                }
            } else {
                $("#dropdownUserTransfer").html($("#dropdownUserTransfer option:first"));
                $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
            }
                                
        },

        dropdownUserTransfer: function () {
            var id = $('#dropdownUserTransfer').val();
            var test = [];
            var self = this;
            if (id) {
                var action = _.find(self.a, function (item) { return item.id === $("#dropdownAction").val(); });
                if (action) {
                    $.get('/Transfer/GetActionsTransferPlan',
                        {
                            workflowId: action.workflowId,
                            userId: id,
                            currentNodeId: action.nextNodeId
                        },
                        function (resultActionPlan) {
                            if (resultActionPlan) {
                                test = resultActionPlan;
                                this.b= resultActionPlan;
                                $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                                $.each(resultActionPlan, function (i, item) {
                                    $("#dropdownActionPlan").append('<option value="' + item.id + '">' + item.name + '</option>');
                                });
                            } else {
                                egov.message.show('Bạn không có quyền xử lý văn bản!');
                            }
                        }
                     );
                }
            } else {
                $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
            }
            if (test.length > 0) {
                this.b = test;
            }
        },

        dropdownActionPlan: function () {
            var id = $('#dropdownActionPlan').val();
            var self = this;
            if (id) {
                var action = _.find(self.b, function (item) { return item.id === id; });
                if (action) {
                    $.get('/Transfer/GetUserByAction', { actionId: id, workflowId: action.workflowid, documentCopyId: 0, userId: $("#dropdownUserTransfer").val() }, function (resultUser) {
                        if (resultUser) {
                            databind(resultUser, action.nextnodeid, action.workflowid, action.currentnodeid, id);
                            $('.dialog-center').css({ border: 'none' });
                            $('.dialog-south').css({ border: 'none' });
                            $('.dialog-center-south').css({ border: 'none' });
                        }
                        else {
                            egov.message.show('Bạn không có quyền xử lý văn bản!');
                        }
                    }
                         );
                }
            }
        },
    });

    var databind=  function (usersByAction, nextNodeId, workflowId, currentNodeId, actionId, userIdXlc) 
    {
        egov.cshtml.transfer.nextNodeId = nextNodeId;
        egov.cshtml.transfer.workflowId = workflowId;
        egov.cshtml.transfer.currentNodeId = currentNodeId;
        egov.cshtml.transfer.actionId = actionId;

        $("#divViewXulychinh, #divViewDongxuly, #divViewThongbao, #tblUsersTransfer tbody").html("");
        //egov.cshtml.transfer.initLayout();

        //$("#userTransferTemplate").tmpl(usersByAction).appendTo($("#tblUsersTransfer tbody"));
        $('#tblUsersTransfer tbody').append($('#userTransferTemplate').tmpl(usersByAction));
        
        egov.cshtml.transfer.bindSearceUser(usersByAction);

        egov.utilities.checkbox.checkAndUnCheckAll($("#dongxulyAll"), $(".dongxulyItem"));

        egov.cshtml.transfer.bindDxlAllClick();

        egov.cshtml.transfer.bindChooseReceiveItems();

        $('#closeDonggui').click(function () {
            $(".dialog-south").hide();
            $('#openDonggui').show();
            $('#closeDonggui').hide();
        });
        
        if (userIdXlc)
        {
            if (typeof userIdXlc === 'number') {
                $('#tblUsersTransfer tbody tr[id=' + userIdXlc + '] .xulychinhItem').simulate('click');
            }
        }
    }
 
    return tranferPlan;
});