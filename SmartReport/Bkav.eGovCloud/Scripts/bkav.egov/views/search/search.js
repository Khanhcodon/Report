define([
    egov.template.search.main,
    egov.template.search.result,
    'validateDateTime'
],
function (Template, SearchResultTemplate) {

    "use strict";

    var Search = Backbone.View.extend({

        className: 'search-form',
        template: Template,
        url: '/Document/SearchDocuments',
        keyCode: {
            s: 83,
            f: 70,
            esc: 27,
            enter: 13,
            tab: 9
        },
        documentDetails: [],

        // Các sự kiện
        events: {
            'click .cbxAdvance': 'showHideAdvance',
            'click .btnSearch': 'search',
            "click input[type='text']": "_reSelected"
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.model = new egov.models.search;
            this.currentDoc = options.currentDoc
            this.render();
        },

        /// <summary>Hiển thị form search</summary>
        render: function () {
            this.$el.empty().append(this.template);
            this.$result = this.$('.searchResult table tbody');
            // this.$('#Compendium').focus();

            // Hủy sự kiện submit của form để tìm kiếm = ajax
            this.$('form').submit(function () {
                return false;
            });
            this.$el.bindResources();
            return this;
        },

        /// <summary>Mở form search dạng dialog</summary>
        openDialog: function (setting) {
            var that = this;
            this.$el.dialog(setting);
            this.$('#Compendium').focus();
        },

        /// <summary>Ẩn hiện tìm kiếm nâng cao</summary>
        showHideAdvance: function () {
            this.$('.search-advance').toggle();
            if (!this.$('.search-advance').is(':hidden'))
                this._parseValue();
        },

        /// <summary>Serialize form to model</summary>
        serialize: function () {
            for (var attr in this.model.attributes) {
                var value = this.$('#' + attr).val();
                if (value != 0) {
                    this.model.set(attr, value);
                }
            }
        },

        /// <summary>Tìm kiếm</summary>
        search: function (e) {
            var that = this,
                items,
                results,
                currentRels,
                resultItem,
                item;

            if (!this.isValid()) {
                egov.message.error("Trích yếu không được để trống!");
                this.$('#Compendium').focus();
                return;
            }

            this._disable(true);
            this.serialize();
            this.model.searchType = egov.enum.searchType.document;
            egov.message.processing(egov.resources.common.searching);
            egov.request.searchAdvance({
                data: this.model.toJSON(),
                success: function (result) {
                    if (result.error) {
                        that.$result.append($('<tr>').append($('<td colspan="9"></td>').text(result.error)));
                        that._disable(false);
                        return;
                    }

                    items = result.Items;
                    currentRels = that.currentDoc.relations.model.models;
                    results = [];

                    //Loại bỏ văn bản hiện tại và những văn bản đã đính kèm vào văn bản hiện tại trong kết quả tìm kiếm
                    items = _.filter(items, function (i) {
                        for (var j = 0; j < currentRels.length; j++) {
                            if (i.DocumentCopyId === currentRels[j].get("RelationCopyId")) {
                                return false;
                            }
                        }
                        return i.DocumentCopyId != that.currentDoc.model.get("DocumentCopyId");
                    });

                    for (var i = 0; i < items.length; i++) {
                        item = new egov.models.search({
                            Address: items[i].ExtendInfo.Address,
                            Compendium: items[i].DocumentCompendium,
                            CategoryName: items[i].ExtendInfo.CategoryName,
                            DateAppointed: items[i].ExtendInfo.DateAppointed,
                            DateArrived: items[i].ExtendInfo.DateArrived,
                            DateCreated: items[i].ExtendInfo.DateCreate,
                            DateReceived: items[i].DateReceived,
                            DocCode: items[i].ExtendInfo.DocCode,
                            DocumentCopyId: items[i].DocumentCopyId,
                            DocumentId: items[i].DocumentId,
                            CurrentUsername: items[i].ExtendInfo.CurrentUsername,
                            Status: items[i].ExtendInfo.Status,
                            CitizenName: items[i].DocumentCompendium,
                            InOutCode: items[i].ExtendInfo.InOutCode,
                            LastUserComment: items[i].ExtendInfo.LastUserComment,
                            UserSuccess: items[i].ExtendInfo.UserSuccess,
                        });
                        results.push(item);
                    }
                    that.model.set('Result', new egov.models.searchResult(results));
                    that.$result.empty();
                    if (results.length === 0) {
                        that.$result.append($('<tr>').append($('<td colspan="9"></td>').text(egov.resources.search.noresult)));
                        return;
                    }
                    that.model.get('Result').each(function (result) {
                        resultItem = new SearchItem({
                            model: result,
                            parent: that
                        });
                        that.$result.append(resultItem.$el);
                    });
                    that.$('table').table({ resizable: true });

                    that.$('.checkAll').on("click", function (e) {
                        that.checkAndUnCheckAll(e);
                    });
                    that._disable(false);
                },
                error: function () {
                    that.$result.append($('<tr>').append($('<td colspan="9"></td>').text(egov.resources.search.error)));
                },
                complete: function () {
                    egov.message.hide();
                    that._disable(false);
                }
            });
        },

        /// <summary>Trả về danh sách các kết quả được chọn</summary>
        getSelected: function () {
            return this.model.get('Result').select(function (result) {
                return result.get('IsSelected');
            });
        },

        /// <summary>Chọn hoặc bỏ chọn tất cả</summary>
        checkAndUnCheckAll: function (e) {
            var checkAll = $(e.target).closest('.checkAll');
            var checked = !checkAll.hasClass('checked');
            this.model.get('Result').each(function (doc) {
                doc.set('IsSelected', checked);
            });
        },

        unCheckAll: function (e) {
            var checkAll = $(e.target).closest('.checkAll');
            this.model.get('Result').each(function (doc) {
                doc.set('IsSelected', false);
            });
        },

        isValid: function () {
            var keyword = this.$('#Compendium').val() + this.$('#DocCode').val() + this.$('#InOutCode').val();
            return keyword !== '';
        },

        _disable: function (value) {
            if (value) {
                this.$(".btnSearch").attr('disable', value);
            }
            else {
                this.$(".btnSearch").removeAttr('disable');
            }
        },

        _parseValue: function () {
            /// <summary>Hiển thị giá trị của các dropdownlist</summary>
            var that = this;

            // Lấy tên thể loại văn bản từ cache
            that.$('#CategoryId').empty();
            egov.locache.get('categories', function (categories) {
                that.$('#CategoryId').append(' <option value="0">Tất cả</option>');
                if (categories && categories.length > 0) {
                    that.$('#CategoryId')
                        .append($.tmpl('<option value="${CategoryId}">${CategoryName}</option>', categories));
                }
            });

            // Từ khóa
            that.$('#KeyWord').empty();
            egov.locache.get('allKeyword', function (allKeyword) {
                that.$('#KeyWord').append(' <option value="0">Tất cả</option>');
                if (allKeyword && allKeyword.length > 0) {
                    that.$('#KeyWord').append($.tmpl('<option value="${KeyWordId}">${KeyWordName}</option>', allKeyword));
                }
            });

            //Độ khẩn
            that.$('#UrgentId').empty();
            egov.locache.get('urgents', function (urgents) {
                that.$('#UrgentId').append(' <option value="0">Tất cả</option>');
                if (urgents && urgents.length > 0) {
                    that.$('#UrgentId').append($.tmpl('<option value="${value}">${name}</option>', urgents));
                }
            });

            //Hình thức
            that.$('#DocFieldId').empty();
            egov.locache.get('allDocField', function (allDocField) {
                that.$('#DocFieldId').append(' <option value="0">Tất cả</option>');
                if (allDocField && allDocField.length > 0) {
                    that.$('#DocFieldId').append($.tmpl('<option value="${DocFieldId}">${DocFieldName}</option>', allDocField));
                }
            });

            //nghiep vu CategoryBusinessId


            //Người ký và người đang giữ
            var allUsers = egov.locache.get("allUsers");
            if (allUsers && allUsers.length > 0) {
                this.$("input[name='UserSuccessId'], input[name='CurrentUserId']")
                    .on("keyup", function () {
                        var id = "#" + this.name;
                        that.$(id).val(0);
                        $(this).autocomplete({
                            source: allUsers,
                            select: function (event, ui) {
                                this.value = ui.item.fullname;
                                that.$(id).val(ui.item.value);

                                return false;
                            }
                        }).data("autocomplete")._renderItem = function (ul, item) {
                            ul.addClass('dropdown-menu');
                            return $("<li></li>")
                                .data("item.autocomplete", item)
                                .append("<a>" + item.label + "</a>")
                                .appendTo(ul);
                        };
                    }
                );
            }

            //từ ngày đến ngày
            this.$("#FromDateStr,#ToDateStr").datepicker({
                showOtherMonths: true,
                selectOtherMonths: true,
                dateFormat: "dd/mm/yy"
            });

            //validate ngày bắt đầu và ngày đến
            this.$('form').validate({
                errorPlacement: $.datepicker.errorPlacement,
                rules: {
                    FromDateStr: {
                        dpCompareDate: ['before', '#ToDateStr']
                    },
                    ToDateStr: {
                        dpCompareDate: { after: '#FromDateStr' }
                    }
                },
                messages: {
                    FromDateStr: 'Ngày bắt đầu không được phép lớn hơn ngày đến!',
                    ToDateStr: 'Ngày đến không được nhỏ hơn ngày bắt đầu!'
                }
            });
        },

        _reSelected: function (e) {
            if (!e)
                return;

            var target = $(e.target);
            var length = target.val().length;
            target[0].setSelectionRange(0, length);
        }
    });

    var SearchItem = Backbone.View.extend({
        tagName: 'tr',
        template: SearchResultTemplate,
        selectedClass: 'rowSelected',

        events: {
            'click .checkbox': 'selectMany',
            'click': 'selected',
            'dblclick': 'showDetailInfo'
        },

        initialize: function (options) {
            this.parent = options.parent;
            this.$el.append($.tmpl(this.template, this.model.toJSON()));
            var that = this;
            this.model.on('change:IsSelected', function (model, isSelected) {
                if (isSelected) {
                    that.$el.addClass(that.selectedClass);
                    that.$el.find('.checkbox, input[type="checkbox"]').addClass("checked");
                } else {
                    that.$el.removeClass(that.selectedClass);
                    that.$el.find('.checkbox, input[type="checkbox"]').removeClass("checked");
                }
            });

            return this;
        },

        selected: function (e) {
            if (!e || this.$(e.taget).parents("th").length > 0) {
                return;
            }

            egov.helper.destroyClickEvent(e);

            if (e.ctrlKey) {
                this.model.set('IsSelected', !this.model.get('IsSelected'));
            }
            else {
                this.parent.unCheckAll(e);
                this.model.set('IsSelected', true);
            }
        },

        selectMany: function (e) {
            this.model.set('IsSelected', !this.model.get('IsSelected'));
            egov.helper.destroyClickEvent(e);
        },

        showDetailInfo: function (e) {
            if (!e) {
                return;
            }

            var that = this,
                documentCopyId,
                documentCopy;

            egov.helper.destroyClickEvent(e);
            documentCopyId = this.model.get("DocumentCopyId");

            if (this.parent.documentDetails && this.parent.documentDetails.length > 0) {
                documentCopy = _.find(this.parent.documentDetails, function (item) {
                    return item.DocumentCopyId === documentCopyId;
                });

                if (documentCopy) {
                    getDocumentDetail(documentCopy);
                    return;
                }
            }
            egov.dataManager.getDocumentDetail(documentCopyId, {
                success: function (data) {
                    if (data.error) {
                        egov.message.notification('Văn bản không tồn tại', egov.message.messageTypes.error);
                        return;
                    }
                    data.DocumentCopyId = documentCopyId;
                    that.parent.documentDetails.push(data);
                    getDocumentDetail(data);
                }
            })
        }
    });


    ///Parse thông tin văn bản và show lên dialog
    var getDocumentDetail = function (document) {
        require(['documentDetail'], function (DocumentDetail) {
            var documentDetail,
                settings;

            //render thông tin chi tiết văn bản
            documentDetail = new DocumentDetail({ model: document });
            //Thiết lập dialog
            settings = {
                width: 800,
                title: egov.resources.documents.title.documentDetail,
                buttons: [{
                    text: egov.resources.common.closeButton,
                    className: 'btn-close',
                    click: function () {
                        documentDetail.$el.dialog('destroy');
                    }
                }],
                draggable: true,
                keyboard: true,
            };

            ///show dialog
            documentDetail.$el.dialog(settings);
        });
    }

    return Search;
})