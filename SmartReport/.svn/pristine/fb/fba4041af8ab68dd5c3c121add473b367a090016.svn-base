define([
    egov.mobile ? egov.template.document.PublishAndFinishView : egov.template.document.PublishAndFinishView
],
function (Template, UserItem) {
    "use strict";

    var _resource = egov.resources.document.transfer;

    //#region View

    /// <summary>Đối tượng thể hiện form bàn giao văn bản</summary>
    var PublishAndFinishView = Backbone.View.extend({
        className: 'transfer-form row',
        template: Template,
        model: egov.viewModels.actionUserList,
        keyCode: {
            s: 83,  //Bàn giao
            f: 70,  // Forcus cn trỏ chuột vào ô tìm kiếm
            esc: 27, //Ẩn form bàn giao
            enter: 13,
            tab: 9  // Chuyển vị trị cn trỏ chuột
        },

        events: {
            'click .checkAll': 'allCoProcessor',
            'click .result-view .checkbox': 'removeUserSelected',
            'click .showCoProcess': 'showHideDg',
            'click .annouce-user .checkbox': 'uncheckThongBao',
            'click .co-process-user .checkbox': 'uncheckPrivateAnoun',
            'click .transfer-tab li': "showMobileTab",
            'change .transfer-filter__position': '_filterUser',
            'change .transfer-filter__dept': '_filterUser'
        },

        initialize: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            var that = this;

            // Đối tượng serialize từ form đồng gửi
            that.dgSerialize = undefined;
            // Đối tượng tổng hợp danh sách nhận bàn giao
            that.destination = {};
            that.$el.append(that.template);
            that.$el.bindResources();

            that.$listUser = that.$('.listUsers:first');
            that.$mainUsers = that.$('.main-process-user > ul:first');
            that.$coUsers = that.$('.co-process-user > ul:first');
            that.$annouceUsers = that.$('.annouce-user > ul:first');
            that.$dg = that.$('.dg-view:first');
            that.$result = that.$('.result-view');
            that.$filter = that.$('.searchUser');
            that.storePrivate = egov.views.home.tree.storeTree;
            that.model.on("reset", function (model) {
                /// <summary>
                /// Trigger khi gọi hàm reset model
                /// - clear hết model cũ
                /// - nếu model.length == 0 => hiển thị cảnh báo không có ai trong hướng chuyển.
                /// - nếu model.length == 1 => chọn luôn người đó là xử lý chính
                /// - filter user
                /// </summary>
                /// <param name="model">danh sách người trong hướng chuyển</param>
                /// <returns type=""></returns>
                that._clear();
                if (model.length === 0) {
                    that.$listUser.append($('<li>').addClass('list-group-item').text(_resource.noUserByAction));
                    that.dialogSetting.height = "100px";

                    this.$el.attr("help-content-page", "transfer");
                    that.$el.dialog(that.dialogSetting);
                    that.$('.listUsers').siblings().hide();
                    return;
                }

                model.each(function (userAction) {
                    var user = that.allUser

                    var userItm = new TransferUserItem({
                        model: userAction,
                        parent: that
                    });

                    that.$listUser.append(userItm.el);
                    if (model.length === 1) {
                        userItm.model.set('isMainProcess', true);
                        userItm.$el.addClass("selected");
                    } else {
                        userItm.model.set('isMainProcess', true);
                        userItm.$el.addClass("selected");
                    }
                });

                that._autocompleteUser();
            });

            // Tiếp tục mở văn bản đến sau khi bàn giao
            that.hasContinue = false;
            that.requireXlc = egov.setting.transfer.requireXlc;

            return this;
        },

        /// <summary>Hiển thị form bàn giao</summary>
        render: function (option) {
            /// <summary>
            /// Pageload form bàn giao
            /// </summary>
            /// <param name="option">{action, document, callback, plan}</param>
            this._clear();
            if (!(option.action instanceof egov.models.action)) {
                option.action = new egov.models.action(option.action);
            }

            this.action = option.action;
            this.document = option.document;
            this.isHSMC = this.document.categoryBusinessId == egov.enum.categoryBusiness.hsmc;
            this.callback = option.callback;
            this.plan = option.plan;
            this.theoLo = false;
            this.isQuickTransfer = option.isQuickTransfer;

            // Chuyển cho người nhận theo hướng chuyển đặc biệt hoặc mở form chuyển
            if (!egov.setting.userSetting.ShowTranferFormWhenQuickAction && this.action.get('isSpecial')) {
                this._transferSpecial();
            }
            else {
                this._open(this.plan ? true : false);
            }

            this.$el.bindResources();
        },

        renderTransferExtends: function () {
            if (!this.$dg.is(':not(:empty)')) {
                var that = this;

                require(['transferExtend'], function (transferExtend) {
                    if (!egov.views.dg) {
                        egov.views.dg = new transferExtend;
                    }

                    egov.views.dg.render(true, false,
                        function () {
                            that._selectDg();
                        },
                        function () {
                            that.$dg.append(egov.views.dg.$el);
                        }
                    );
                });
            }
        },

        renderTheoLo: function (option) {
            /// <summary>
            /// Hiển thị form bàn giao cho xử lý theo lô
            /// </summary>
            /// <param name="option">{action, theolo, comment}</param>
            this._clear();

            if (!(option.action instanceof egov.models.action)) {
                option.action = new egov.models.action(option.action);
            }

            this.action = option.action;
            //Theo lô văn bản
            this.theoLo = true;
            this.documentCopyIds = option.documentCopyIds;

            this.documents = option.documents;
            this.document = option.document;
            this.isHSMC = this.documents && this.documents.length > 0 && this.documents[0].categoryBusinessId == egov.enum.categoryBusiness.hsmc;

            this.comment = option.comment;

            this.callback = option.callback;
            this.callbackCloseForm = option.callbackCloseForm;
            this._openChuyenTheoLo();

            this.$el.bindResources();
        },

        renderAnticipate: function (option) {
            /// <summary>
            /// Mở form bàn giao theo dự kiến
            /// </summary>
            /// <param name="option">{action, parent, model, callback}</param>
            this._clear();
            if (!(option.action instanceof egov.models.action)) {
                option.action = new egov.models.action(option.action);
            }

            //Dự kiến chuyển
            if (!(option.model instanceof egov.models.actionUserList)) {
                option.model = new egov.models.actionUserList(option.model);
            }

            this.theoLo = false;
            this.isHSMC = this.document.categoryBusinessId == egov.enum.categoryBusiness.hsmc;
            this.action = option.action;
            this.parent = option.parent;
            this.model = option.model;
            this.callback = option.callback;
            this._renderAnticipate();
            this.$el.bindResources();
        },

        /// <summary>Bỏ chọn user nhận bàn giao</summary>
        removeUserSelected: function (e) {
            var target = $(e.target).closest('.checkbox');
            var userId = target.find(':checkbox').val();
            var user = this.model.detect(function (user) {
                return user.id == userId;
            });

            if (user) {
                if (user.get('isMainProcess')) {
                    user.set('isMainProcess', false);
                }

                if (user.get('isCoProcess')) {
                    user.set('isCoProcess', false);
                }

                this.showSelected();
            }

            egov.helper.destroyClickEvent(e);
        },

        uncheckPrivateAnoun: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            if (egov.views.dg) {
                var target = $(e.target).closest(':checkbox');
                if (target.length === 0) return;
                egov.views.dg.uncheckPrivateAnoun(target.val());
                this._selectDg();
            }
        },

        uncheckThongBao: function (e) {
            /// <summary>
            /// Bỏ check giám sát
            /// </summary>
            /// <param name="e">event</param>
            if (egov.views.dg) {
                egov.views.dg.uncheckThongBao(e);
                this._selectDg();
            }
        },

        /// <summary>Check, uncheck tất cả đồng xử lý</summary>
        allCoProcessor: function (e) {
            var checkAll = $(e.target).closest('.checkbox');
            checkAll.toggleClass('checked');

            var checked = checkAll.hasClass('checked');

            this.model.each(function (user) {
                var userid = user.get('value');
                var userElement = this.$('.listUsers li[userid="' + userid + '"]');
                if (userElement.is(":visible")) {
                    user.set('isCoProcess', checked);
                    checked
                        ? userElement.find('.co-process').addClass('checked')
                        : userElement.find('.co-process').removeClass('checked');
                }
            }.bind(this));

            egov.helper.destroyClickEvent(e);
        },

        serialize: function () {
            /// <summary>
            /// Serialize form bàn giao để lấy ra destination
            /// </summary>
            var mainProcess = this.model.detect(function (user) {
                return user.get('isMainProcess');
            });

            var coProcess = this.model.select(function (user) {
                return user.get('isCoProcess');
            });

            if (mainProcess === undefined) {
                if (coProcess.length === 0) {
                    return null;
                } else if (coProcess.length === 1) {
                    mainProcess = coProcess[0];
                    coProcess.splice(0, 1);
                }
            }

            // Lấy destination
            var transferType = egov.enum.transferType,
                targetComments = [],
                userIdsDxl = [],
                isDxl = false,
                isThongbao = false;

            if (mainProcess) {
                this.destination.UserIdXlc = mainProcess.get('id');

                targetComments.push({
                    label: mainProcess.get('fullname'),
                    department: mainProcess.get("department"),
                    value: '',
                    type: transferType.xulychinh
                });
                //Lấy danh sách đồng xử lý
                userIdsDxl = _(coProcess).chain().pluck('id').value();
                //Check bỏ người xử lý chính ra khỏi danh sách đồng xử lý
                if (userIdsDxl && userIdsDxl.length > 0) {
                    userIdsDxl = _.filter(userIdsDxl, function (item) {
                        return item !== mainProcess.get('value');
                    })
                }
            } else {
                userIdsDxl = _.pluck(coProcess, "id");
            }

            //Thiết lập hiển thị danh sách ngưởi đồng xử lý
            //Nếu danh sách đxl >0 mới thêm vào, nếu ko thì bỏ qua
            if (coProcess.length > 0) {
                var coProcessFullname = _.map(coProcess, function (p) { return p.get('fullname'); });
                var coProcessDeparments = _.map(coProcess, function (p) { return p.get('department'); });
                coProcessDeparments = _.uniq(coProcessDeparments, function (p) { return p; });

                targetComments.push({
                    label: coProcessFullname.join(', '),
                    department: coProcessDeparments.join(', '),
                    value: '',
                    type: transferType.dongxuly
                });
            }

            var extInfo;
            if (egov.views.dg) {
                //Lấy danh sách đồng gửi(thông báo)
                targetComments = targetComments.concat(egov.views.dg.getDestination());
                extInfo = egov.views.dg.getExtInfo();
                isDxl = egov.views.dg.checkIsDongGui();
                isThongbao = !isDxl;
            }

            this.destination.UserIdsDxl = userIdsDxl;
            this.destination.IsThongbao = isThongbao;
            this.destination.IsDxl = true;
            this.destination.IsAttachYk = false;
            this.destination.UserIdsDg = !egov.views.dg ? [] : egov.views.dg.getUserConsults(); // this._getUserConsults();
            this.destination.UserTb = !egov.views.dg ? [] : egov.views.dg.getUserThongBao();
            this.destination.NextNodeId = this.action.get('nextNodeId');
            this.destination.CurrentNodeId = this.action.get('currentNodeId');
            this.destination.WorkflowId = this.action.get('workflowId');
            this.destination.NewDocTypeId = this.document ? this.document.newDoctype : undefined;
            this.destination.TargetComment = JSON.stringify(targetComments);
            this.destination.ActionId = this.action.get('id');
            this.destination.ExtInfo = JSON.stringify(extInfo);
            this.destination.PublishPlanId = this.document ? this.document.publishPlanId : 0;

            return this.destination;
        },

        showMobileTab: function (e) {
            var that = this;
            var $target = $(e.currentTarget);
            $target.siblings().removeClass("active");
            $target.addClass("active");
            var tabId = $target.attr("tab-show");
            this.$(".transfer-tabs").not("#" + tabId).hide();
            this.$(".transfer-tabs#" + tabId).show();
            if (tabId == "dg-view") {
                this.renderTransferExtends();
            }
        },

        showHideDg: function (isShow) {
            /// <summary>
            /// Ẩn hiện form đồng gửi
            /// </summary>
            var that = this;
            if (isShow) {
                this.$dg.show();
                this.$result.height(320);
            }
            else {
                this.$dg.hide();
                this.$result.height(175);
            }
            // this.$dg.toggle();
            //  this.$result.height(isShow ? 320 : 175);

            this.renderTransferExtends();
        },

        _unCheckShowHideDg: function (target) {
            /// <summary>
            /// Bỏ check hiển thị chọn đồng gửi
            /// </summary>
            if (!target) {
                return;
            }

            if (!(target instanceof jQuery)) {
                target = $(target);
            }

            if ($(target).is('.showCoProcess')) {
                if (target.find('.checkbox').hasClass('checked')) {
                    target.find('.checkbox').removeClass('checked');
                    target.find('input[name="checkDongGui"]').prop('checked', false);
                    this.$dg.removeClass('col-md-9');
                    this.$dg.hide();
                    this.$result.removeClass('col-md-7 show-dg');
                }
            }
        },

        //#region Private Methods

        _clear: function () {
            /// <summary>
            /// Chuyển các giá trị về mặc định
            ///</summary>
            this.model = egov.viewModels.actionUserList;
            this.$listUser.empty();
            this.$mainUsers.empty().append('<li class="list-group-item ">' + _resource.noXlc + '</li>');
            this.$coUsers.empty();
            this.$annouceUsers.empty();
            this.$result.height(175);
            this.destination = {};

            if (this.$dg) {
                this.$dg.hide();
            }
            //reset lai nguoi xu ly chinh
            if (this.mainProcess) {
                this.mainProcess = undefined;
            }

            //reset lại trạng thái hiển thị đồng gửi
            // this._unCheckShowHideDg($('.showCoProcess'));
            this.destroyDg();
        },

        _open: function (isplan) {
            /// <summary>
            /// Mở form chuyển
            /// </summary>
            var that,
                actionId,
                workflowId,
                documentCopyId;

            that = this;
            actionId = that.action.get('id');
            workflowId = that.action.get('workflowId');
            documentCopyId = that.document.model.get('DocumentCopyId');
            that.$filter.empty().focus();
            that._renderDialog();
            var is_fast_transfer = egov.setting.transfer.isfasttransfer;
            if (is_fast_transfer === false) {
                $("#titleSimple").addClass("hidden");
                $("#left_content").removeClass("hidden");
                $("#right_content").removeClass("hidden");
            } else {
                $("#titleSimple").removeClass("hidden");
                $("#left_content").addClass("hidden");
                $("#right_content").addClass("hidden");
            }
            that._bindUsers(isplan);
        },

        _bindUsers: function (isplan) {
            var that = this,
                actionId,
                workflowId,
                documentCopyId,
                isSpecial, success;

            actionId = that.action.get('id');
            workflowId = that.action.get('workflowId');
            documentCopyId = that.document.model.get('DocumentCopyId');
            isSpecial = that.action.get('isSpecial');
            isplan = isplan && !isSpecial;

            success = function (result) {
                isplan
                    ? that._renderDialogPlan()
                    : that._sortUsersByAction(result, function (userActions) {
                        that.model.reset(userActions);
                    });
            };

            if (isSpecial) {
                var userNext = _.find(egov.setting.allUsers, function (u) { return u.value === that.action.get('userIdNext') });
                var users = [userNext];
                return success(users);
            }

            egov.dataManager.getUserByAction(actionId, workflowId, documentCopyId, {
                success: function (result) {
                    success(result);
                    $("#nameDepart").text(result[result.length - 1].position);
                }
            });
        },

        _sortUsersByAction: function (userByActions, success) {
            /// <summary>
            /// Săp xếp danh sách người nhận theo hướng chuyển:
            /// - Người vừa gửi.
            /// - Người khởi tạo.
            /// - Người cùng phòng ban.
            /// - Người còn lại.
            /// Những người còn lại sort theo abc.
            /// </summary>
            /// <param name="userByActions">Danh sách người nhận theo action.</param>
            /// <returns type=""></returns>
            var that,
                result,
                currentUser,
                remainUsers;

            if (userByActions.error) {
                egov.callback(success, []);
                return;
            }

            this.$(".transfer-filter__position").empty().html('<option value="">Tất cả</option>');
            this.$(".transfer-filter__dept").empty().html('<option value="">Tất cả</option>');

            if (userByActions.length === 1) {
                egov.callback(success, userByActions);
                return;
            }

            that = this;
            result = [];

            currentUser = _.find(userByActions, function (user) {
                return user.value === egov.setting.userId;
            });

            //userByActions = _.reject(userByActions, function (u) {
            //    return u.value === egov.setting.userId;
            //});

            //var positions = _.uniq(_.pluck(userByActions, "position"));
            //this.$(".transfer-filter__position").append($.tmpl("<option value='${$data}'>${$data}</option>", positions));

            //var depts = _.uniq(_.pluck(userByActions, "department"));
            //this.$(".transfer-filter__dept").append($.tmpl("<option value='${$data}'>${$data}</option>", depts));

            that._getSpecialUsers(userByActions, function (specialUsers) {
                // Tạm bỏ cho Lạng Sơn
                specialUsers = [];

                remainUsers = _.difference(userByActions, specialUsers);
                //remainUsers = _.sortBy(remainUsers, function (user) {
                //    return user.name;
                //});

                result = _.union(specialUsers, remainUsers);

                if (currentUser) {
                    result.push(currentUser);
                }

                egov.callback(success, result);
            });
        },

        _getSpecialUsers: function (userByActions, success) {
            /// <summary>
            /// Trả về danh sách người nhận ưu tiên trên hướng chuyển
            /// - Người vừa gửi.
            /// - Người khởi tạo.
            /// - Người cùng phòng ban, đơn vị.
            /// </summary>
            /// <param name="userByActions">Danh sách tất cả người nhận theo hướng chuyển</param>
            var that,
                result,
                createUserId,
                lastUserId,
                currentUserId,
                ignoreLength,
                callback,
                currentDepts,
                checkSameDept,
                parentDepts;

            that = this;
            if (!this.theoLo) {
                createUserId = that.document.model.get("UserCreatedId");
                lastUserId = that.document.model.get("CommentList") && that.document.model.get("CommentList").length > 0 ? that.document.model.get("CommentList")[0].UserSendId : undefined;
            }
            currentUserId = egov.setting.userId;
            // Số lượng người nhận theo hướng chuyển cho phép mà không cần phải xử lý ưu tiên (trừ người gửi, khởi tạo).
            // Số lượng người được đặt hợp lý sao cho đủ nhìn trong danh sách.
            ignoreLength = 5;

            // Người vừa gửi
            result = _.find(userByActions, function (user) {
                return user.value === lastUserId;
            });
            if (result) {
                egov.callback(success, [result]);
                return;
            }

            // Người khởi tạo
            result = _.find(userByActions, function (user) {
                return user.value === createUserId;
            });
            if (result) {
                egov.callback(success, [result]);
                return;
            }

            // Trường hợp số lượng user < số lượng cho phép thì không cần xét ưu tiên
            if (userByActions.length <= ignoreLength) {
                egov.callback(success, []);
                return;
            }

            // Xử lý theo quan hệ phòng ban
            callback = {
                success: function (userDepts) {
                    currentDepts = _.pluck(_.filter(userDepts, function (userDept) {
                        return userDept.userid === currentUserId;
                    }), "idext");

                    if (currentDepts.length === 0) {
                        egov.callback(success, []);
                        return;
                    }

                    // Lấy danh sách phòng ban của những người thuộc hướng chuyển
                    _.each(userByActions, function (user) {
                        user.depts = _.pluck(_.filter(userDepts, function (dept) {
                            return dept.userid === user.value;
                        }), "idext");
                    });

                    // Xử lý theo quan hệ: cùng phòng ban
                    // Trả về danh sách các user trong hướng chuyển có cùng phòng ban với người gửi hiện tại.
                    result = _.filter(userByActions, function (user) {
                        checkSameDept = _.intersection(currentDepts, user.depts);
                        if (checkSameDept.length > 0) {
                            user.sameDepts = checkSameDept;
                            return true;
                        }
                        return false;
                    });

                    if (result) {
                        // Phòng ban giống nhau có level càng cao thì độ ưu tiên càng cao (đưa lên đầu)
                        result = _.sortBy(result, function (user) {
                            return egov.getLevelOfDept(_.max(user.sameDepts, function (dept) {
                                return egov.getLevelOfDept(dept);
                            }));
                        });

                        egov.callback(success, result.reverse());
                        return;
                    }

                    // Xử lý theo quan hệ: phòng ban cấp trên
                    parentDepts = _.map(currentDepts, function (dept) {
                        return dept.substr(0, dept.lastIndexOf("."));
                    });
                    result = _.filter(userByActions, function (user) {
                        return _.intersection(parentDepts, user.depts).length > 0;
                    });
                    if (result) {
                        egov.callback(success, result);
                        return;
                    }

                    // Xử lý theo quan hệ: phòng ban cấp dưới
                    result = _.filter(userByActions, function (user) {
                        // Lấy parent của các phòng ban của người nhận
                        parentDepts = _.map(user.depts, function (dept) {
                            return dept.substr(0, dept.lastIndexOf("."));
                        });
                        return _.intersection(currentDepts, parentDepts).length > 0;
                    });

                    if (result) {
                        egov.callback(success, result);
                        return;
                    }
                }
            };
            egov.dataManager.getAllUserDeptPosition(callback);
        },

        _openChuyenTheoLo: function () {
            /// <summary>
            /// Mở form chuyển văn bản theo lô
            /// </summary>
            if (!this.documentCopyIds || this.documentCopyIds.length <= 0) {
                egov.message.error(egov.resources.transfer.HasNoneDocument);
                // egov.message.error("Bạn chưa chọn văn bản!");
                egov.pubsub.publish(egov.events.status.error, "Bạn chưa chọn văn bản!");
                return;
            }

            var that = this;
            that.$filter.empty().focus();
            that._renderDialog();
            egov.request.getUserByActionTheoLo({
                data: {
                    actionId: this.action.get('id'),
                    workflowId: this.action.get('workflowId'),
                    documentCopyIds: this.documentCopyIds,
                },
                success: function (result) {
                    //that.model = new egov.models.actionUserList(result);
                    that._sortUsersByAction(result, function (userActions) {
                        that.model.reset(userActions);
                    });
                }
            });
        },

        _renderAnticipate: function () {
            /// <summary>
            /// Hiển thị bàn giao theo dự kiến chuyển
            /// </summary>
            var that = this;
            if (this.model && this.model.length > 0) {
                for (var i = 0; i < this.model.length; i++) {
                    var user = this.model.at(i);
                    var userItm = new TransferUserItem({
                        model: user,
                        parent: that
                    });

                    this.$('.listUsers').append(userItm.$el);
                }
            }

            $('body').bind("keydown", function (e) {
                that._eventKeyboard(e, this);
            });

            this._autocompleteUser();
            this.$el.bindResources();
            egov.callback(this.callback, this.$el);

            this.$("#searchUser").empty().focus();
            if (this.$('.listUsers')[0].scrollHeight > this.$('.listUsers').height()) {
                this.$('.checkAll').css({ right: "15px" });
            } else {
                var right = this.$('.listUsers')[0].scrollWidth - this.$('.listUsers').width();
                this.$('.checkAll').css({ right: right + "px" });
            }
        },

        _renderDialog: function () {
            var that = this;
            var is_fast_transfer = egov.setting.transfer.isfasttransfer;

            that.$listUser.empty();
            var dialogSetting = {
                title: _resource.dialogTitle,
                width: '900px',
                draggable: true,
                keyboard: true,
                height: "auto",
                resizable: true
            };
            this.$('.listUsers').siblings(':not(.dg-view)').show();
            if (!egov.isMobile) {
                var hasAutoClick = egov.setting.userSetting.hasShowDongGui;
                dialogSetting.confirm = {
                    text: "Nâng cao",
                    id: "transferDg",
                    style: { float: "left", "font-weight": "normal", "font-size": "13px", "color": "blue" },
                    click: function (isChecked) {
                        //isFastTransfer  
                        if (is_fast_transfer === true) {
                            isChecked ? $("#titleSimple").addClass("hidden") : $("#titleSimple").removeClass("hidden");
                            isChecked ? $("#left_content").removeClass("hidden") : $("#left_content").addClass("hidden");
                            isChecked ? $("#right_content").removeClass("hidden") : $("#right_content").addClass("hidden");
                        }
                        that.showHideDg(isChecked);
                    },
                    hasAutoClick: hasAutoClick
                };
            }

            var hasContinue = that.document && that.document.isCreate;
            dialogSetting.buttons = [
                {
                    id: "sendTransferAndContinue",
                    text: "Chuyển và tiếp tục",
                    className: that.action.get('isAllowSign') ? 'btn-info' : 'hidden',
                    disableProcess: true,
                    click: function (callback) {
                        if (that._validXlc()) {
                            that._sendAndContinue(callback);
                        } else {
                            egov.callback(callback);
                        }
                    }
                },
                {
                    id: "sendTransfer",
                    text: _resource.transferButton,
                    className: 'btn-primary',
                    disableProcess: true,
                    click: function (callback) {
                        if (that._validXlc()) {
                            that._send(callback);
                        } else {
                            egov.callback(callback);
                        }
                    }
                },
                {
                    id: "sendTransferAndSign",
                    text: "Ký và Chuyển",
                    className: that.action.get('isAllowSign') ? "btn-info " : "hidden",
                    disableProcess: true,
                    click: function (callback) {
                        if (that._validXlc()) {
                            // that._signAndSend(callback);
                            that._saveInSign(callback)
                        } else {
                            egov.callback(callback);
                        }
                    }
                },
                {
                    id: "closeTransfer",
                    text: egov.resources.common.closeButton,
                    className: 'btn-close',
                    click: function () {
                        if (that.theoLo) {
                            egov.callback(that.callbackCloseForm);
                        }

                        that.destroyDg();
                        that.$el.dialog('hide');
                    }
                }
            ];

            $('body').bind("keydown", function (e) {
                that._eventKeyboard(e, this);
            });

            this.dialogSetting = dialogSetting;
            this.$el.dialog(dialogSetting);

            this._autocompleteUser();

            this.$("#searchUser").empty().focus();
            if (this.$('.listUsers')[0].scrollHeight > this.$('.listUsers').height()) {
                this.$('.checkAll').css({ right: "15px" });
            } else {
                var right = this.$('.listUsers')[0].scrollWidth - this.$('.listUsers').width();
                this.$('.checkAll').css({ right: right + "px" });
            }
        },

        _transferSpecial: function () {
            /// <summary>
            /// Chuyển văn bản theo hướng chuyển đặc biệt
            /// </summary>
            var that = this;
            if (this.document.isCreate) {
                // Nếu là văn bản KNTC, cần lưu hồ sơ trước sau đó gán Id để thêm vào CSDL
                // </summary>
                // <param name="callback"></param>

                that._transferTiepNhan();
            }
            else {
                this.destination.UserIdXlc = this.action.get('userIdNext');
                this.destination.UserIdsDxl = [];
                this.destination.IsThongbao = false;
                this.destination.IsDxl = false;
                this.destination.IsAttachYk = false;
                this.destination.UserIdsDg = [];
                this.destination.NextNodeId = this.action.get('nextNodeId');
                this.destination.CurrentNodeId = this.action.get('currentNodeId');
                this.destination.WorkflowId = this.action.get('workflowId');
                this.destination.TargetComment = "[]";
                this._transferNormal();
            }
        },

        _autocompleteUser: function () {
            /// <summary>
            /// Tìm nhanh trên danh sách user
            /// </summary>
            var that = this,
                firstItem,
                selectFn = function (user) {
                    if (user) {
                        // Nếu user đang được chọn thì bỏ chọn.
                        if (user.get('isMainProcess')) {
                            user.set('isMainProcess', false);
                        }
                        else if (user.get('isCoProcess')) {
                            user.set('isCoProcess', false);
                        }
                        else {
                            // Nếu đã có người xử lý chính rồi thì gán thành đồng xử lý
                            if (that.mainProcess) {
                                user.set('isCoProcess', true);
                            }
                            else {
                                // Không thì gán xử lý chính
                                user.set('isMainProcess', true);
                            }
                        }
                        firstItem = null;
                    }
                    that.$filter.val('');
                };
            this.$filter.autocomplete({
                source: this.model.toJSON(),
                focus: function (event, ui) {
                    that.$filter.val(ui.item.username);
                    return false;
                },
                select: function (event, ui) {
                    var user = that.model.detect(function (u) {
                        return u.id === ui.item.id;
                    });
                    selectFn(user);
                    return false;
                }
            })
            .on("keydown", function (event) {
                //Khi nhấn enter, chọn luôn item đầu tiên trong list đã lọc autocomplete
                if (event.keyCode == 13 && firstItem) {
                    var user = that.model.detect(function (u) {
                        return u.id === firstItem.id;
                    });
                    selectFn(user);
                    $(".ui-autocomplete").hide();
                    return false;
                }
            })
            .data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu');
                firstItem = item;
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            }
        },
        _filterUser: function () {
            var position = this.$(".transfer-filter__position").val();
            var dept = this.$(".transfer-filter__dept").val();
            if (position == "" && dept == "") {
                this.$('.listUsers li').show();
                return;
            };

            this.$('.listUsers li').each(function () {
                var upos = $(this).attr("position");
                var udept = $(this).attr("dept");
                var isShow = true;

                isShow = String.isNullOrEmpty(position) ? isShow : upos === position;
                isShow = String.isNullOrEmpty(dept) ? isShow : udept === dept;
                isShow ? $(this).show() : $(this).hide();
            });
        },

        send: function (callback) {
            this._send(callback);
        },

        _send: function (callback) {
            /// <summary>
            /// Bàn giao văn bản
            /// </summary>
            var that = this;
            var transfer = function () {
                var destination = that.serialize();
                if (!destination) {
                    egov.pubsub.publish(egov.events.status.warning, _resource.noUser);
                    egov.callback(callback);
                    return;
                }

                //Kiểm tra trên hồ sơ 1 cửa thì bắt buộc phải có người xử lý chính
                if (that.isHSMC) {
                    if (destination.UserIdXlc == undefined) {
                        egov.pubsub.publish(egov.events.status.error, _resource.hsmsNoXlc);
                        return;
                    }
                }

                //Chuyển văn bản theo lô
                if (that.theoLo) {
                    that._transferTheoLo(callback);
                }
                else {
                    //Tạo function cho dễ callback
                    var _transferTemp = function () {
                        if (that.document.isCreate) {
                            that.$el.dialog('hide');
                            that._transferNormal(0, function () {
                                egov.callback(callback);
                            });
                            //var setting = egov.setting.userSetting.hasHideLuuSo;
                            //if (setting) {

                            //} else {
                            //    if (that.storePrivate) {
                            //        that.storePrivate.renderDialog(function (storeId, callback2) {
                            //            that._transferNormal(storeId, function () {
                            //                egov.callback(callback);
                            //                egov.callback(callback2);
                            //            });
                            //        });
                            //    }
                            //}
                        }
                        else if (that.document.model.get("HasPrivateSaveToStore") == true) {
                            // Luu so noi bo: cap so truoc
                            that.$el.dialog('hide');
                            require(['savePrivateStore'], function (savePrivateStore) {
                                var savePrivateStore = new savePrivateStore;
                                savePrivateStore.render({
                                    document: that.document,
                                    callback: function (option) {
                                        if (option != undefined) {
                                            that.storeId = option.storeId;
                                            that.code = option.code;
                                            that.codeId = option.codeId;

                                            that._transferNormal(null, function () {
                                                egov.callback(callback);
                                                egov.callback(callback2);
                                            });
                                        }
                                    }
                                });
                            });
                        }
                        else {
                            that._transferNormal(null, callback);
                        }
                    }

                    _transferTemp();
                }
            }

            transfer();
        },

        _signAndSend: function (callback) {
            var that = this;
            //Có cho phép ký hay ko
            if (!that.action.get('isAllowSign')) {
                return;
            }

            if (!Plugin) {
                return;
            }

            var destination = that.serialize();
            if (!destination) {
                egov.pubsub.publish(egov.events.status.warning, _resource.noUser);
                egov.callback(callback);
                return;
            }

            Plugin.appendPlugin(function () {
                Plugin.sign(that.document, function (isSigned, signMessage) {
                    isSigned = isSigned || false;
                    if (!isSigned) return;

                    if (that.document.isTransferTheoLo) {
                        that._send(callback);
                    } else {
                        that.document.model.set("HasCA", true);
                        that.document.attachments.confirmAttachments(function () {
                            that._send(callback);
                        });
                    }
                });
            });
        },

        _saveInSign: function (callback) {
            var that = this

            that.document._exportDocToSign(function (data) {
                var fileName = data.fileName
                var newAttachment = new egov.models.attachment({
                    Id: 0,
                    Name: fileName.fileName,
                    Size: 100,
                    Extension: ".docx",
                    Versions: [{
                        Version: 1,
                        User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                    }],
                    isNew: true,
                    isUploading: true
                });
                newAttachment.set('Id', fileName.key);
                that.document.attachments.model.add(newAttachment);
                that._signAndSend(callback);
            })
        },

        _sendAndContinue: function (callback) {
            var that = this;

            if (that.action.get('isAllowSign')) {
                return;
            }

            that.hasContinue = true;
            that._send(callback);
        },

        _transferNormal: function (storeId, callback) {
            /// <summary>
            /// Bàn giao thông thường
            /// </summary>
            var that = this,
                key = "Normal",
                doc,
                destinationPlan,
                selectedFiles,
                docCopyId = that.document.model.get("DocumentCopyId"),
                isOnlyDxls,
                newContent,
                newCompendium;

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            var closeTab = function () {
                that.destroyDg();
                that.$el.dialog('hide');
                //var setting = egov.setting.userSetting.hasHideLuuSo;
                //if (setting) {
                //    that.storePrivate.closeDialogAndRebind();
                //}

                that.document.isSaveChanged = true;
                if (key == 'TiepNhanHoSoVaTiepTuc') {
                    egov.pubsub.publish(egov.events.status.success, 'Tiếp nhận hồ sơ thành công.');
                } else {
                    if (that.document.isPopUp) {
                        return;
                    }
                    // close tab
                    that.document.$el.removeClass("display");
                    if (!egov.isMobile) {
                        that.document.tab.display(false);
                        egov.views.home.tab.activeTab(0);
                    }
                    //that._closeTab([docCopyId]);
                    var relationId;
                    if (doc && doc.TransferTypeInEnum == "TraLoi") {
                        relationId = parseInt(doc.relationAnswerId);
                        egov.views.home.tab.closeTab([relationId]);
                    }

                    // reload lại cây văn bản
                    that._reloadDocumentList((relationId && isNaN(relationId)) ? [that.document.model.get("DocumentCopyId"), relationId] : [that.document.model.get("DocumentCopyId")]);

                    egov.callback(that.callback);
                }
            };

            var transferSuccess = function (data) {
                /// <summary>
                /// tạo function sử dụng khi tranfer thành công, dùng cho cả 2 trường hợp văn bản có sự thay đổi hay ko
                /// </summary>
                /// <param name="data"></param>
                /// <summary>
                if (data.success) {
                    egov.pubsub.publish(egov.events.status.success, _resource.transferSuccess);

                    that._closeTab([docCopyId]);
                    that._reloadDocumentList([docCopyId]);
                    that.document.cacheCodeNotation();

                    if (that.hasContinue) {
                        var docId = that.document.model.get("DocTypeId");
                        var doctypeName = that.document.model.get("DocTypeName");
                        that.hasContinue = false;

                        egov.views.home.tab.addDocument(docId, doctypeName, null, null, null, null, null, that.document.model);
                    }
                } else {
                    if (!egov.isMobile) {
                        that.document.tab.display(true);
                    }
                    egov.pubsub.publish(egov.events.status.error, data.error);
                    egov.callback(callback);
                }
            };

            var transferError = function (message) {
                /// <summary>
                /// tạo function sử dụng khi tranfer lỗi, dùng cho cả 2 trường hợp văn bản có sự thay đổi hay ko
                /// </summary>
                /// <param name="data"></param>
                /// <summary>
                that.document.tab.display(true)
                that.document.tab.errorTab();

                message = message || _resource.transferError;
                egov.pubsub.publish(egov.events.status.error, message);
            };

            //Nếu không chọn người XLC và chọn lớn hơn 1 người DXL => đây là trường hợp bàn giao toàn ĐXL
            isOnlyDxls = this.destination ? (this.destination.UserIdsDxl.length > 1 && !this.destination.UserIdXlc) : false;

            doc = this.document.serialize();
            if (this.storeId && this.code) {
                doc.StoreId = this.storeId;
                doc.DocCode = this.code;
                doc.CodeId = this.codeId;
            }
            selectedFiles = {};

            // File mới
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew') && !file.get('isRemoved')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });

            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });
            removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

            var modifiedFiles = this.document.attachments.modifiedFiles;

            // Cập nhật nội dung file đã sửa với những file vừa upload lên.
            // Đồng thời xóa file đó trong danh sách file đang chỉnh sửa.
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });

            //Dự kiến chuyển
            destinationPlan = this.document.getDestinationPlan();
            if (destinationPlan) {
                destinationPlan = JSON.stringify(destinationPlan);
            }

            egov.request.transfer({
                data: {
                    "doc": JSON.stringify(doc),
                    "destination": this.destination ? JSON.stringify(this.destination) : "",
                    "files": JSON.stringify(selectedFiles),
                    "modifiedFiles": JSON.stringify(modifiedFiles),
                    "removeAttachmentIds": removeFiles,
                    "storePrivateId": storeId == null ? 0 : storeId,
                    "destinationPlan": destinationPlan
                },
                success: function (data) {
                    transferSuccess(data);
                },
                error: function (data) {
                    transferError(data.statusMessage);
                }
            });

            closeTab();
        },

        _transferTheoLo: function (callback) {
            /// <summary>
            /// Chuyển văn bản theo lô
            /// </summary>
            /// <param name="callback">Hàm gọi lại khi thực thi thành công hoặc thất bại</param>
            var that = this;

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);
            var comment = this.comment;
            if (typeof this.comment === 'string') {
                comment = {};
                _.each(this.documentCopyIds, function (id) {
                    comment[id] = comment;
                });
            }

            egov.request.transferTheoLo({
                data: {
                    documentCopyIds: this.documentCopyIds,
                    destination: this.destination ? JSON.stringify(this.destination) : "",
                    comments: JSON.stringify(comment),
                    modifyAttachments: that.document ? JSON.stringify(that.document.modifyAttachments) : null,
                    files: that.document ? JSON.stringify(that.document.newFiles) : null,
                    removeAttachmentIds: that.document ? JSON.stringify(that.document.removeFiles) : null
                },
                success: function (data) {
                    if (data.success) {
                        egov.callback(callback);
                        that._reloadDocumentList(that.documentCopyIds);
                        egov.pubsub.publish(egov.events.status.success, _resource.transferSuccess);
                    } else {
                        egov.pubsub.publish(egov.events.status.error, data.error);
                        egov.callback(callback);
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.transferError);
                    egov.callback(callback);
                }
            });

            that.destroyDg();
            that.$el.dialog('destroy');
            that.documents.selectedNextCurrent(function (itemSelected) {
                var command = function () {
                    if (itemSelected) {
                        itemSelected.set("Selected", true);
                    }
                }

                that.documents.removeDocumentByIds(that.documentCopyIds, function (itemSelected) {
                    command();
                });
            });

            egov.callback(that.callback);
        },

        _transferTiepNhan: function () {
            /// <summary>
            /// Tiếp nhận hồ sơ
            /// </summary>

            var doc = this.document.serialize();

            var selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });
            var that = this;

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            egov.request.TransferTiepNhan({
                data: {
                    "doc": JSON.stringify(doc),
                    "files": JSON.stringify(selectedFiles)
                },
                success: function (result) {
                    if (result.success) {
                        that._closeTab();
                        //if (that.action.get('id') === egov.enum.actionSpecial.tiepNhanHoSoVaTiepTuc.name) {
                        //    egov.pubsub.publish(egov.events.status.notification, 'Tiếp nhận hồ sơ thành công.');
                        //    egov.views.home.tab.addDocument(that.document.model.get("DocTypeId"), that.document.model.get("DocTypeName"));
                        //    return;
                        //}
                        egov.pubsub.publish(egov.events.status.success, 'Tiếp nhận hồ sơ thành công.');
                        that.document._reloadDocumentTree();
                    }
                    else {
                        if (!egov.isMobile) {
                            that.document.tab.display(true);
                        }
                        egov.pubsub.publish(egov.events.status.error, data.error);
                    }
                },
                error: function () {
                    if (!egov.isMobile) {
                        that.document.tab.display(true)
                    }
                    egov.pubsub.publish(egov.events.status.error, _resource.transferError);
                }
            });

            that.document.$el.removeClass("display");
            if (!egov.isMobile) {
                that.document.tab.display(false)
                if (that.action.get('id') === egov.enum.actionSpecial.tiepNhanHoSoVaTiepTuc.name) {
                    egov.views.home.tab.addDocument(that.document.model.get("DocTypeId"), that.document.model.get("DocTypeName"));
                } else {
                    that.document._reloadDocumentTree();
                    egov.views.home.tab.activeTab(0);
                }
            }

        },

        _selectDg: function () {
            var containner, gsContainer,
                that = this;
            this.$('.co-process-user > ul').empty();
            this.$('.annouce-user > ul').empty();
            this.$(".giamsat-user > ul").empty();
            this.showSelected();
            containner = this.$('.co-process-user > ul');
            gsContainer = this.$('.annouce-user > ul');
            egov.views.dg.selectDg(containner, gsContainer);
            return;
        },

        _getUserConsults: function () {
            /// <summary>
            /// Trả về danh sách các user nhận đồng gửi
            /// </summary>
            /// <returns type="object">Mảng các userid thuộc không gian user được chọn.</returns>
            if (!egov.views.dg) {
                return [];
            }
            var selected = egov.views.dg.serialize();
            var result = [];
            if (selected === undefined) {
                return result;
            }

            var allUserDept = egov.dataManager.getAllUserDeptPosition({
                success: function (allUDept) {
                    return allUDept;
                }
            });

            var allUsers = egov.dataManager.getAllUsers({
                success: function (allUser) {
                    return allUser;
                }
            });
            var listUser;
            if (selected.isAllUser || (selected.isAllDept && selected.isAllJobtitle)) {
                return _.pluck(allUsers, 'value');
            }

            // Thêm các user được chọn
            if (selected.users.length > 0) {
                var values = _.pluck(selected.users, 'value')
                $.extend(result, values);
            }

            // Thêm các phòng ban được chọn
            if (selected.depts.length > 0) {
                $.extend(result, getUserFromDepts(_.pluck(selected.depts, 'value')));
            }

            // Lọc từ chọn chức vụ - phòng ban
            // Chọn tất cả phòng ban
            if (selected.isAllDept) {
                if (!selected.isAllJobtitle && selected.jobtitlies.length > 0) {
                    // Không chọn tất cả chức vụ thì lấy user thuộc danh sách các chức vụ thuộc phòng ban root
                    var depts = _.filter(allUserDept, function (i) {
                        return _.find(selected.jobtitlies, function (jobtitle) {
                            return jobtitle.value === i.jobtitleid;
                        });
                    });

                    if (depts.length > 0) {
                        $.extend(result, getUserFromDepts(_.pluck(depts, 'departmentid')));
                    }
                }
            }
            else if (selected.isAllJobtitle) {// Chọn tất cả chức vụ
                // Trường hợp chọn tất cả phòng ban đã set ở trên
                if (!selected.isAllDept && select.jobDepts.length > 0) {
                    var depts = _.filter(allUserDept, function (i) {
                        return _.find(selected.jobDepts, function (dept) {
                            return dept.departmentid === i.departmentid;
                        });
                    });

                    if (depts.length > 0) {
                        $.extend(result, getUserFromDepts(_.pluck(depts, 'departmentid')));
                    }
                }
            }
            else if (selected.jobtitlies.length > 0) {
                // trường hợp chọn các chức vụ thuộc một vài phòng ban
                var depts = _.filter(allUserDept, function (i) {
                    var checkDept = _.find(selected.jobDepts, function (dept) {
                        return dept.departmentid === i.departmentid;
                    });
                    var checkJob = _.find(selected.jobtitlies, function (jobtitle) {
                        return jobtitle.value === i.jobtitleid;
                    });

                    return checkDept && checkJob;
                });

                if (depts.length > 0) {
                    $.extend(result, getUserFromDepts(_.pluck(depts, 'departmentid')));
                }
            }

            result = _.uniq(result, false);
            return result;
        },

        _eventKeyboard: function (e, $el) {
            ///<sumnary>
            /// Các xử lý phím tắt trên form bàn giao văn bản
            ///</sumnary>
            ///<param name="e" type="object"></param>
            ///<param name="$el" type="object jquery or string"></param>
            if (!e) {
                return;
            }

            if (!($el instanceof jQuery)) {
                $el = $($el);
            }

            // egov.helper.destroyClickEvent(e);
            //Tab chuyển vị trị focus con trỏ chuột
            if (e.keyCode === this.keyCode.tab) {
                if (this.$("#searchUser:focus").length > 0) {
                    $el.find("#sendTransfer").focus();
                }
                else if ($el.find("#sendTransfer:focus").length > 0) {
                    $el.find("#closeTransfer").focus();
                }
                else if ($el.find("#closeTransfer:focus").length > 0) {
                    this.$("#searchUser").focus();
                }

                return;
            }
            else if (e.keyCode === this.keyCode.esc) {
                //Đóng dialog
                this.$el.dialog('destroy');
            }
            else if (e.ctrlKey && e.keyCode === this.keyCode.f) {
                //set focus vào ô tìm kiếm
                this.$("#searchUser").focus();
            }
            else if (e.ctrlKey && e.keyCode === this.keyCode.s) {
                //Nhấn Ctrl+ s để gửi văn bản
                this._send();
            }
        },

        defaultMainAndCoProcess: function () {
            ///<summary>
            /// Chuyển người trong hướng chuyển về mặc định chưa chọn j
            ///</summary>
            if (this.model && this.model.length > 0) {
                this.model.forEach(function (item) {
                    item.set('isMainProcess', false);
                    item.set('isCoProcess', false);
                });
            }
        },

        showSelected: function () {
            ///<summary>
            /// Hiển thị danh sách người nhận bàn giao được chọn
            ///</summary>
            var that = this;
            this.$mainUsers.empty();
            this.$coUsers.empty();
            var num = 0;
            this.model.each(function (user) {
                var userItm;
                var label = user.get('fullname') + " - " + user.get('username');
                if (that.mainProcess) {
                    if (user.get('isMainProcess')) {
                        userItm = parseUserItem(user.get('value'), label, true);
                        that.$mainUsers.append(userItm);
                    }
                    else if (user.get('isCoProcess')) {
                        userItm = parseUserItem(user.get('value'), label);
                        that.$coUsers.append(userItm);
                    }
                }
                else {
                    if (user.get('isMainProcess') || user.get('isCoProcess')) {
                        userItm = parseUserItem(user.get('value'), label, true);
                        that.$mainUsers.append(userItm);
                    }
                }

                if (user.get('isCoProcess')) {
                    num = num + 1;
                }
            });

            if (that.$mainUsers.is(":empty")) {
                that.$mainUsers.append('<li class="list-group-item ">' + _resource.noXlc + '</li>');
            }

            if (num === this.model.length) {
                this.$('.checkAll').addClass('checked');
            } else {
                this.$('.checkAll').removeClass('checked');
            }
        },

        _renderDialogPlan: function () {
            /// <summary>
            /// Hiển thị form dự kiến chuyển đã được bind dữ liệu theo cấu hình trước đó
            /// </summary>
            var that = this;
            this.$listUser.empty();
            this.$dg.empty();
            var dialogSetting = {
                title: _resource.dialogTitle,
                width: '900px',
                draggable: true,
                keyboard: true,
                height: "420px",
                buttons: [
                        {
                            id: "sendTransferPlan",
                            text: _resource.transferButton,
                            className: 'btn-primary',
                            disableProcess: true,
                            click: function (callback) {
                                that._send(callback);
                            }
                        },
                        {
                            id: "closeTransferPlan",
                            className: 'closeDialog',
                            text: egov.resources.common.closeButton,
                            click: function () {
                                if (egov.views.dg) {
                                    egov.views.dg.destroy();
                                }

                                that.$el.dialog('hide');
                            }
                        }
                ]
            };

            this.$('.listUsers').siblings(':not(.dg-view)').show();
            for (var i = 0; i < this.model.length; i++) {
                var user = this.model.at(i);
                var userItm = new TransferUserItem({
                    model: user,
                    parent: that
                });
                if (this.plan.UserIdXlc && user.get('value') == this.plan.UserIdXlc) {
                    userItm.model.set('isMainProcess', true);
                    userItm.$el.addClass("selected");
                }
                this.$listUser.append(userItm.$el);
            }

            this.$el.dialog(dialogSetting);

            this._autocompleteUser();
        },

        _showHideDgInplan: function (item, callback) {
            /// <summary>
            /// Ẩn hiện form đồng gửi trên dự kiến chuyển
            /// </summary>
            var tartget,
                that = this;;

            if ($(item).is('.showCoProcess')) {
                tartget = $(item);
            }
            else {
                tartget = $(item).closest('.showCoProcess');
            }

            if (!tartget.find('.checkbox').hasClass('checked')) {
                tartget.find('.checkbox').addClass('checked');
                tartget.find('input[name="checkDongGui"]').prop('checked', true);
            }
            else {
                tartget.find('.checkbox').removeClass('checked');
                tartget.find('input[name="checkDongGui"]').prop('checked', false);
            }

            this.$dg.addClass('col-md-9');
            this.$dg.toggle();
            this.$result.addClass('col-md-7 show-dg');
            require(['transferExtend'], function (transferExtend) {
                egov.views.dg = new transferExtend;
                egov.views.dg.render(true, false, function () {
                    that._selectDg();
                }, function () {
                    that.$dg.append(egov.views.dg.$el);
                    if (typeof callback == 'function') {
                        callback();
                    }
                });
            });
        },

        _reloadDocumentList: function (removeDocumentCopyIds) {
            /// <summary>
            /// Đồng bộ danh sách văn bản hiện tại
            /// </summary>
            /// <param name="removeDocumentCopyIds">Danh sách các documentCopyId đã chuyển khỏi danh sách hiện tại</param>
            var that = this;

            if (!this.document || !this.document.isCreate) {
                egov.views.home.tree.removeDocuments(removeDocumentCopyIds);
            }
        },

        destroyDg: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        },

        _closeTab: function (documentCopyIds) {
            ///<summay>
            /// Đóng tab văn bản khi bàn giao thành công
            ///<para name="documentCopyIds"> Có thể là mảng chứa danh sách id văn bản hoặc id văn bản</para>
            ///</summay>
            if (this.document) {
                this.document.isSaveChanged = true;
                if (this.document.tab) {
                    this.document.tab.close2(false);
                }
            }
            else if (egov.views.home && egov.views.home.tab) {
                if (!documentCopyIds) {
                    return;
                }

                if (!(documentCopyIds instanceof Array)) {
                    documentCopyIds = [documentCopyIds];
                }

                egov.views.home.tab.closeTab(documentCopyIds);
            }
        },

        _validXlc: function () {
            if (!this.requireXlc) {
                return true;
            }

            var destination = this.serialize();
            //Nếu không chọn người XLC và chọn lớn hơn 1 người DXL => đây là trường hợp bàn giao toàn ĐXL
            var isOnlyDxl = destination ? (destination.UserIdsDxl.length > 1 && !destination.UserIdXlc) : false;

            if (isOnlyDxl) {
                egov.pubsub.publish(egov.events.status.warning, "Hệ thống yêu cầu chọn người xử lý chính, vui lòng chọn lại");
            }

            return !isOnlyDxl;
        },
        _sendSimple: function () {
            console.log("Nang cao");
        },

        //#endregion
    });

    /// <summary>Đối tượng thể hiện 1 user trong danh sách user theo hướng chuyển</summary>
    var TransferUserItem = Backbone.View.extend({
        tagName: 'li',
        className: 'list-group-item',
        template: UserItem,

        events: {
            'click': 'selected2',
            'contextmenu': 'selected2',
            'click .checkbox': 'selected',
            'dblclick': 'selectAndTransfer',
            'tap .chkdoc': "selected"
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (option) {
            this.parent = option.parent;
            this.model.set("docId", this.parent.document == null ? 0 : this.parent.document.id);
            this.render();
            this.$el.attr({
                position: this.model.get('position'),
                dept: this.model.get('department'),
                userid: this.model.get('value')
            });

            this.$checkbox = this.$('.checkbox');

            // Chọn hoặc bỏ chọn người xử lý chính
            this.model.on('change:isMainProcess', function (model, change) {
                this.$('.checkbox.main-process').toggleClass('checked');
                this.$el.toggleClass('xlc');
                if (change) {
                    // Nếu chọn thì bỏ chọn người đang được chọn xử lý chính trước đó
                    if (this.parent.mainProcess) {
                        this.parent.mainProcess.set('isMainProcess', false);
                    }

                    // gán lại người đang được chọn xử lý chính
                    this.parent.mainProcess = this.model;

                    // Bỏ tích chọn đồng xử lý tương ứng
                    model.set('isCoProcess', false);
                } else {
                    // Bỏ người được chọn
                    this.parent.mainProcess = undefined;
                }
                this.parent.showSelected();
            }, this);

            // Chọn hoặc bỏ chọn người đồng xử lý
            this.model.on('change:isCoProcess', function (model, change) {
                this.$('.checkbox.co-process').toggleClass('checked');
                this.$el.toggleClass('dxl');
                if (change) {
                    model.set('isMainProcess', false);
                }
                this.parent.showSelected();
            }, this);
        },

        /// <summary>Hiển thị user</summary>
        render: function () {
            this.$el.append($.tmpl(this.template, this.model.toJSON()));
            if (egov.mobile) {
                this.$(".mdl-checkbox").materialCheckbox();
                this.$(".mdl-js-ripple-effect").materialRipple();
            }
            this.$el.bindResources();
            this.$('.qtooltip').etip();
        },

        /// <summary>Chọn người nhận</summary>
        selected: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);

            var target = $(e.currentTarget);
            var $li = target.closest('li');
            $li.siblings().removeClass('selected');
            $li.addClass('selected');

            var check = egov.isMobile ? target.children('.checkbox') : target.closest('.checkbox');
            var isMainProcess = check.hasClass('main-process');

            if (isMainProcess) {
                this.model.set('isMainProcess', !check.hasClass('checked'));
                this.parent.$('.checkbox.main-process.checked').not(check).removeClass('checked');
                this.$('.checkbox.co-process').removeClass('checked');
            }
            else {
                this.model.set('isCoProcess', !check.hasClass('checked'));
                this.$('.checkbox.main-process').removeClass('checked');
            }
        },

        selected2: function (e) {
            ///<summary>Chọn người nhận
            /// ctrl+ click(chuột trái): chọn người đồng gửi
            /// click chuột trái: chọn người xử lý chính
            /// contextmenu(chuột trái) :hủy bỏ hàng được chọn
            ///</summary>
            if (!e) { return; }
            var $li = $(e.target).closest('li');

            //click chuột trái và các phím ctrl, shift
            if (e.type === 'click') {
                if (this.parent.model.length === 1) {
                    if (this.model.get('isMainProcess') == true) {
                        this.model.set('isMainProcess', false);
                    } else {
                        this.model.set('isMainProcess', true);
                    }
                    return;
                }

                $li.siblings().removeClass('selected');
                $li.addClass('selected');

                if (e.ctrlKey) {
                    //Nếu trên danh sách chưa có xử lý chính thì mặ định chọn là xử lý chính
                    if (!this.parent.mainProcess && this.model.get('isCoProcess') == false) {
                        this.model.set('isMainProcess', true);
                        return;
                    }

                    if (this.model.get('isMainProcess') == true) {
                        //   this.model.set('isCoProcess', false);
                        this.model.set('isMainProcess', false);
                        return;
                    }

                    if (this.model.get('isCoProcess') == false) {
                        this.model.set('isCoProcess', true);
                    }
                    else {
                        //Nếu đã chọn đồng xử lý thì bỏ chọn
                        this.model.set('isCoProcess', false);
                    }
                }
                else {
                    if (this.parent.model.length > 1) {
                        //Nếu đã chọn đông xử lý thì bỏ chọn
                        if (this.model.get('isCoProcess') == true) {
                            this.model.set('isCoProcess', false);
                        }
                            //Nếu chưa chọn đồng xử lý và xử lý chính thì chọn là xlc
                        else if (this.model.get('isMainProcess') == true) {
                            this.model.set('isMainProcess', false);
                        }
                            //Nếu đã chọn là xlc thì chuyển thành dxl
                        else if (this.model.get('isMainProcess') == false) {
                            this.model.set('isMainProcess', true);
                        }
                    }
                }
            }
            else if (e.type === 'contextmenu') {
                $li.removeClass('selected');
                this.model.set('isCoProcess', false);
                this.model.set('isMainProcess', false);
            }
        },

        selectAndTransfer: function (e) {
            /// <summary>
            /// Chọn người bàn giao và bàn giao văn bản
            ///</summary>
            $("#titleSimple").removeClass("hidden");
            $("#left_content").addClass("hidden");
            $("#right_content").addClass("hidden");
            egov.helper.destroyClickEvent(e);
            this.parent.defaultMainAndCoProcess();
            this.model.set('isMainProcess', true);
            this.parent.send();
            return;
        }
    });

    //#endregion

    //#region Private methods

    var parseUserItem = function (value, name, isMainProcess) {
        var result;
        var template = '<li class="list-group-item ">\
                            <div class="row wraptext">\
                                <label class="mdl-checkbox mdl-js-checkbox checkbox checked document-color">\
                                    <input class="mdl-checkbox__input" name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                    <span class="{2}"><i class="icon-check"></i></span>\
                                </label>\
                                <span style="margin-left: 15px;">{1}</span>\
                            </div>\
                        </li>';
        var process = "document-color-1"; //isMainProcess ? "document-color-3" : "document-color-1";
        result = $(String.format(template, value, name, process));
        if (egov.isMobile) {
            $(result).find(".mdl-checkbox").materialCheckbox();
        }
        return result;
    };

    var getUserFromDepts = function (depts) {
        /// <summary>
        /// Trả về danh sách các user thuộc các phòng ban cần lấy
        /// </summary>
        /// <param name="depts">Mảng id các phòng ban</param>
        /// <returns type="">Mảng id các user đã được uniq</returns>
        var result = [];
        egov.dataManager.getAllUserDeptPosition(function (allUserDept) {
            depts.forEach(function (dept) {
                // lấy ra phòng ban và các phòng ban con của nó.
                var depts = _.filter(allUserDept, function (num) {
                    return num.departmentid === dept || num.idext.indexOf('.' + dept + '.') > 0;
                });

                if (depts.length > 0) {
                    $.extend(result, _.pluck(depts, 'userid'));
                }
            });
            result = _.uniq(result, false);
            return result;
        });
    }

    //#endregion

    return PublishAndFinishView;
});