define([],
    function () {

        /// <summary>Lưu Sổ nội bộ</summary>
        var SavePrivateStore = Backbone.View.extend({
            el: "#SavePrivateStore",
            events: {
                'click .code-list > li': '_selectCode'
            },

            initialize: function (option) {
                /// <summary>
                /// Khởi tạo
                /// </summary>

                this.stores = [];
                this.$stores = this.$('#StoreId');
                this.$codeId = this.$('#Code');
                this.$code = this.$('.code-list');
                return this;
            },

            render: function (option) {
                /// <summary>
                /// Hiển thị form phát hành
                /// </summary>
                /// <param name="option">Các tham số khởi tạo</param>
                /// <returns type=""></returns>
                this.document = option.document;
                this.callback = option.callback;
                this.categoryId = this.document.model.get("CategoryId");
                this.documentId = this.document.model.get("DocumentId");
                this.$stores.off("change");

                this._openDialog();
                return this;
            },

            _openDialog: function () {
                var that = this;
                this.$el.dialog({
                    title: "Cấp số phát hành",
                    width: "400px",
                    height: "120px",
                    draggable: true,
                    onclose: function (e) {
                        that.$el.dialog('hide');
                        return;
                    },
                    buttons: [{
                        text: "Cấp số và chuyển",
                        className: 'btn-primary',
                        disableProcess: true,
                        click: function () {
                            var storeId = that.$stores.val();
                            var code = that.$codeId.val();

                            that.$el.dialog('hide');
                            egov.callback(that.callback, { storeId: storeId, code: code, codeId: that.selectCodeId });
                        }
                    },
                    {
                        text: "Chuyển",
                        className: 'btn-info',
                        disableProcess: true,
                        click: function () {
                            that.$el.dialog('hide');
                            egov.callback(that.callback, { storeId: null, code: null, codeId: null });
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        className: 'btn-close',
                        click: function () {
                            egov.callback(that.callback);
                            that.$el.dialog('hide');
                        }
                    }]
                });

                this._renderData();
                this._changeStore();
            },

            //#region Private Methods

            _renderData: function () {
                /// <summary>
                /// Hiển thị dữ liệu cho form phát hành
                /// </summary>
                var that = this;
                var docTypeId = this.docTypeId ? this.docTypeId : this.document.model.get('DocTypeId');
                var that = this;

                // Hiển thị sổ văn bản
                var storeId = this.document.model.get("StoreId");
                var docTypeId = this.docTypeId ? this.docTypeId : this.document.model.get('DocTypeId');

                egov.request.GetStores({
                    data: { docTypeId: docTypeId },
                    success: function (stores) {
                        that.stores = stores;
                        if (stores.length > 0) {
                            that.$stores.empty();
                            that.$stores.append($.tmpl('<option value="${StoreId}">${StoreName}</option>', stores));
                            that.$stores.find("option[value='" + storeId + "']").attr("selected", "selected");

                            // Hiển thị danh sách bảng mã của Sổ văn bản đầu tiên
                            storeId |= stores[0].StoreId;
                            that._showCode(storeId);
                        }
                    }
                });
            },

            _showCode: function (storeId) {
                /// <summary>
                /// Hiển thị số ký hiệu (template)
                /// </summary>
                var that = this;
                that.$code.empty();
                that.$codeId.val('');

                var data = {
                    storeId: storeId
                };

                data.categoryId = this.categoryId;
                data.documentId = this.documentId;

                egov.request.GetCodes({
                    data: data,
                    success: function (codies) {
                        that.codies = codies;
                        if (codies && codies.length > 0) {
                            that.$code.append($.tmpl('<li class="list-group-item" value="${CodeId}">${Template}</li>', codies));
                            that.$codeId.val(codies[0].Template);
                            that.selectCodeId = codies[0].CodeId;
                        }
                    }
                });
            },

            _selectCode: function (e) {
                /// <summary>
                /// Thay đổi mã hồ sơ
                /// </summary>
                /// <param name="e"></param>
                var code = $(e.target).text();
                var codeId = $(e.target).attr('value');
                this.$codeId.val(code);
                this.selectCodeId = parseInt(codeId);
            },

            _changeStore: function () {
                var that = this;
                this.$stores.change(function () {
                    var storeId = $(this).val();
                    that._showCode(storeId);
                });
            }
            //#endregion
        });

        return SavePrivateStore;
    });