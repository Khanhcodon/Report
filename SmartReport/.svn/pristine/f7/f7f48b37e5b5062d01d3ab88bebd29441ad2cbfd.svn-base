define([
    'text!templates/search/searchTab.html',
    'text!templates/search/searchDocResult.html',
    'text!templates/search/searchFileResult.html',
    'paging',
    'autocomplete'
],

function (SearchTab, SearchDocResultTemplate, SearchFileResultTemplate, EgovPaging) {
    "use strict";

    //#region Model
    var SearchModel = Backbone.Model.extend({
        defaults: {
            Compendium: '',
            CategoryId: null,
            KeyWord: '',
            Content: '',
            DocCode: '',
            InOutCode: '',
            UrgentId: null,
            CategoryBusinessId: null,
            StoreId: null,
            StorePrivateId: null,
            CurrentUserId: null,
            InOutPlaceId: null,
            FromDateStr: '',
            ToDateStr: '',
            FromPubDateStr: '',
            ToPubDateStr: '',
            BeforeDate: '',
            AfterDate: '',
            OrganizationCreate: '',
            DocFieldId: null,
            UserSuccessId: null,
            Page: 1,
            UserCreateId: null,
            IsMainProcess: true,
            IsUseCached: false,
            IsRelationDoc: false
        }
    });

    //#endregion

    var NUMBER_ROW_IN_PAGE = 100;
    var EgovSearch = Backbone.View.extend({
        // Tên plugin dùng để sửa file.
        pluginName: 'eOfficePlus',
        keyCode: {
            s: 83,
            f: 70,
            esc: 27,
            enter: 13,
            tab: 9
        },
        preRelationDocItems: [],// mang luu lai ket qua tim kiem lien quan trang truoc do
        documentDetails: [],
        initialize: function (option) {
            this.searchModel = new SearchModel();
            this.searchType = egov.enum.searchType.document;
            this.currentPage = 1;
            this.FindProcessDocument = egov.setting.user.userSetting.FindProcessDocument;

            if (option) {
                if (option.isRelationDoc) {
                    this.isRelationDoc = option.isRelationDoc;
                    this.currentDoc = option.currentDoc;
                }

                this.searchType = option.searchType ? option.searchType : 1;
                this.searchQuery = option.searchQuery ? option.searchQuery : 1;
                this.isLoadedSearchAdvance = false;
                this.isQuickSearch = true;
                if (option.model) {
                    this.model = option.model;
                    this.model.SearchQuery = option.searchQuery;
                    this.pageSize = this.model.Items.length;
                }
            }

            this.render();
        },

        events: {
            "click #btnAdvanceSearch": "searchAdvance",
            "click #btnAdvanceSearchNew": "searchNew",
            "change #advanceSearch": "toggleSearchAdvanceForm",
            "change #fileSearch": "toggleSearchFileForm",
            "click input[type='text']": "_reSelected",
            "keypress #Compendium": "_keypress",
            "click .sort": "_sort"
        },

        render: function () {
            var that = this;
            if (!that.isRelationDoc) {
                //kiem tra neu khong phai tim kiem van ban lien quan
                that.$el.html($.tmpl(SearchTab, that.model));
            } else {
                that.$el.html($.tmpl(SearchTab, { isRelationDoc: true }));

                that.$('.checkAll').on("click", function (e) {
                    that.checkAndUnCheckAll(e);
                });

                that.$("#divSearchFile").hide();
            }

            if (this.FindProcessDocument) {
                this.$("#IsMainProcess").attr("checked", "checked");
            }

            var frmDate = new Date();
            frmDate = frmDate.month(frmDate.month() - 12).startOf("month");
            that.$('#FromDateStr').datepicker({
                changeMonth: true,
                dateFormat: "dd/mm/yy",
                onClose: function (selectedDate) {
                    that.$("#ToDateStr").datepicker("option", "minDate", selectedDate);
                }
            }).datepicker("setDate", frmDate);

            that.$("#ToDateStr").datepicker({
                changeMonth: true,
                dateFormat: "dd/mm/yy",
                onClose: function (selectedDate) {
                    that.$("#FromDateStr").datepicker("option", "maxDate", selectedDate);
                }
            }).datepicker("setDate", new Date());


            that.$approvers = that.$('#UserSuccessId');
            that.$organization = that.$("#OrganizationCreate");
            that.$stores = that.$("#StoreId");
            that.$InOutPlaceId = that.$("#InOutPlaceId");
            that.$category = that.$("#CategoryId");

            if (that.searchType == egov.enum.searchType.document) {
                that.$("#divSearchFile").hide();
                that.$("#Compendium").focus();
                that.$("#Compendium").setCursorToTextEnd();
                that._renderData();
            } else if (that.searchType == egov.enum.searchType.file) {
                that.$("#divSearchDoc").hide();
                that.$("#advanceSearch").prop("disabled", true);
                that.$("#Keyword").focus();
                that.$("#Keyword").setCursorToTextEnd();;
            }

            if (that.model) {
                if (!that.isRelationDoc) {
                    that.renderPager();
                }

                that.renderResult();
            }

            that.$("form").submit(function (e) {
                that.searchAdvance();
                e.preventDefault();
                return false;
            });
        },

        renderPager: function () {
            var that = this;
            that.$(".paging").html(new EgovPaging({
                total: that.model.TotalResult,
                pageSize: NUMBER_ROW_IN_PAGE,
                select: function (selectedPage) {
                    that._changePage(selectedPage);
                }
            }).$el);

            return;
        },

        renderResult: function () {
            var that = this,
                items = that.model.Items,
                results = [], resultItem;

            var from = that.$('#FromDateStr').val();
            var to = that.$('#ToDateStr').val();

            that.$(".dateRange").html(String.format("Dữ liệu lấy từ <b>{0}</b> đến <b>{1}</b> (thiết lập lại trong tìm kiếm nâng cao).", from, to));

            if (items.length === 0) {
                that.$("#docSearchResult tbody").empty();
                that.$("#docSearchResult tbody").append("<tr><td colspan='9'>Không có kết quả hợp lệ</td></tr>");
                return;
            }
            
            if (that.searchType == egov.enum.searchType.document) {
                that.$("#wrapAttachment").hide();
                that.$("#docSearchResult").show();
                that.$("#docSearchResult tbody").empty();
            } else if (that.searchType == egov.enum.searchType.file) {
                that.$("#wrapAttachment").show();
                that.$("#docSearchResult").hide();
                that.$("#wrapAttachment tbody").empty();
            }

            if (this.currentSort) {
                items = _.sortBy(items, function (i) {
                    if (that.currentSort.column === "DateReceived") {
                        return Date.parse(i[that.currentSort.column]);
                    }
                    return i[that.currentSort.column];
                });

                if (this.currentSort.isDesc) {
                    items = items.reverse();
                }
            }

            if (that.isRelationDoc) {
                that.preRelationDocItems = that.model.Items; // luu lai items ket qua trang truoc cho tim kiem lien quan
                for (var i = 0; i < items.length; i++) {
                    var doc = items[i];
                    doc.Index = (that.currentPage - 1) * that.pageSize + i + 1;
                    doc.isSelected = false;
                    doc.isRelationDoc = that.isRelationDoc;
                    var item = new egov.models.search(doc);
                    results.push(item);
                    resultItem = new SearchRelationResult({
                        model: item,
                        parent: that
                    });
                    that.$("#docSearchResult tbody").append(resultItem.$el);
                }

                //2 bien temp luu lai de phan trang tim kiem van ban lien quan
                var totalPage = that.model.TotalPage;
                var totalResult = that.model.TotalResult;

                that.model = new egov.models.search;
                that.model.TotalPage = totalPage;
                that.model.TotalResult = totalResult;
                that.model.set('Result', new egov.models.searchResult(results));
            } else {
                for (var i = 0; i < items.length; i++) {
                    var doc = items[i];
                    doc.Index = (that.currentPage - 1) * that.pageSize + i + 1;
                    doc.isSelected = false;
                    doc.isRelationDoc = that.isRelationDoc;
                    if (that.searchType == egov.enum.searchType.document) {
                        doc.DocumentCompendium = unescape(doc.DocumentCompendium);
                        doc.Title = doc.DocumentCompendium;
                        var item = new SearchResult({
                            model: doc,
                            template: SearchDocResultTemplate,
                            parent: that
                        });
                        that.$("#docSearchResult tbody").append(item.$el);
                    } else if (that.searchType == egov.enum.searchType.file) {
                        var item = new SearchResult({
                            model: doc,
                            template: SearchFileResultTemplate,
                            parent: that
                        });
                        that.$("#wrapAttachment tbody").append(item.$el);
                    }
                }                
            }

            that.$("#docSearchResult").grid({
                isUsingCustomScroll: false,
                isResizeColumn: true,
                isFixHeightContent: false,
                isAddHoverRow: false,
                isUseCookie: false,
                isRenderPanelGrid: false
            });

            that.$el.bindResources();
        },

        serialize: function () {

            if (this.$('#CurrentUser').val() == "") {
                this.$('#CurrentUserId').val('');
            }

            if (this.$('#UserCreate').val() == "") {
                this.$('#UserCreateId').val('');
            }

            for (var attr in this.searchModel.attributes) {
                var value = this.$('#' + attr).val();
                if (value !== 0) {
                    this.searchModel.set(attr, value);
                }
            }


            this.searchModel.set("IsMainProcess", this.$("#IsMainProcess").is(":checked"));
        },

        searchAdvance: function (page, isUseCached) {
            var that = this,
                currentRels;

            egov.pubsub.publish("status.processing", egov.resources.common.searching);
            this.isQuickSearch = false;

            if (!page) {
                page = 1;
            }

            if (isUseCached == undefined) {
                isUseCached = false;
            }

            if (!isUseCached) {
                this.currentPage = 1;
                that.$("#btnAdvanceSearchNew").removeClass('hidden');
            }

            if (that.searchType === egov.enum.searchType.document) {
                that.serialize();
                that.searchModel.set('Page', page);
                that.searchModel.set('IsUseCached', isUseCached);

                egov.request.searchAdvance({
                    data: this.searchModel.toJSON(),
                    success: function (result) {
                        if (that.isRelationDoc) {
                            var tempItems = result.Items;
                            currentRels = that.currentDoc.relations.model.models;
                            tempItems = _.filter(tempItems, function (i) {
                                for (var j = 0; j < currentRels.length; j++) {
                                    if (i.DocumentCopyId === currentRels[j].get("RelationCopyId")) {
                                        return false;
                                    }
                                }
                                return i.DocumentCopyId != that.currentDoc.model.get("DocumentCopyId");
                            });
                            if (isUseCached) {//ghep voi ket qua trang trước trong tim kiêm liên quan
                                var unionItems = _.union(that.preRelationDocItems, tempItems);
                                result.Items = unionItems;
                            }
                        }

                        that.model = result;
                        that.pageSize = NUMBER_ROW_IN_PAGE;
                        that.$("#totalResult").text(result.TotalResult);

                        if (!that.isRelationDoc && page === 1) {
                            that.renderPager();
                        }

                        egov.pubsub.publish("status.destroy");

                        that.renderResult();

                        if (that.isRelationDoc) {
                            var modal_body = $("#scrollSearchResult").parents(".modal-body");
                            modal_body.unbind('scroll');    //unbind scroll tranh duplicate event
                            modal_body.scroll(_.debounce(function () { that.scrollLoader(modal_body); }, 100));
                        }
                    }
                });
            } else if (that.searchType === egov.enum.searchType.file) {
                egov.request.quickSearch({
                    data: {
                        query: that.$("#Keyword").val(),
                        type: that.searchType
                    },
                    success: function (result) {
                        that.model = result;
                        that.pageSize = NUMBER_ROW_IN_PAGE;
                        that.$("#totalResult").text(result.TotalResult);
                        if (page === 1) {
                            that.renderPager();
                        }
                        that.renderResult();
                    }
                });
            }

            return false;
        },

        searchNew: function () {
            var frmDate = new Date();
            frmDate.month(new Date().month() - 12);
            this.$("#Compendium").val("");
            this.$("#DocCode").val("");
            this.$('#InOutCode').val("");
            this.$('#CategoryId').prop('selectedIndex', 0);
            this.$('#UrgentId').prop('selectedIndex', 0);
            this.$('#StorePrivateId').prop('selectedIndex', 0);
            this.$('#CategoryBusinessId').prop('selectedIndex', 0);
            this.$('#StoreId').prop('selectedIndex', 0);
            this.$('#InOutPlaceId').prop('selectedIndex', 0);
            this.$('#FromDateStr').datepicker("setDate", frmDate);
            this.$("#ToDateStr").datepicker("setDate", new Date());
            this.$("#CurrentUser").val("");
            this.$("#CurrentUserId").val("");
            this.$('#FromPubDateStr').datepicker("setDate", "");
            this.$("#ToPubDateStr").datepicker("setDate", "");
            this.$("#UserSuccessId").prop('selectedIndex', 0);
            this.$('#OrganizationCreate').prop('selectedIndex', 0);
            this.$('#UserCreate').val("");
            this.$('#UserCreateId').val("");
            this.$('#DocFieldId').prop('selectedIndex', 0);
            if (!this.isRelationDoc) {
                this.searchAdvance();
            } else {
                this.$("#Compendium").focus();
            }
            this.$("#btnAdvanceSearchNew").addClass('hidden');
            return false;
        },

        openDialog: function (setting) {
            var that = this;
           
            this.$el.dialog(setting);
            if (setting && setting.addCallBack) {
                setting.addCallBack(that.$el)
            }

            this.$('#Compendium').focus();
        },

        scrollLoader: function (ele) {
            var that = this;
            var position = ele.scrollTop() + 10; //thêm 10 để tránh sát chân trang 
            var bottom = ele.prop("scrollHeight") - ele.outerHeight();
            if (position >= bottom) {
                if (that.currentPage < that.model.TotalPage) {
                    that.currentPage = that.currentPage + 1;
                    that.searchAdvance(that.currentPage, true);
                }
            }
        },

        /// <summary>Trả về danh sách các kết quả được chọn</summary>
        getSelected: function () {
            return this.model.get('Result').select(function (result) {
                return result.get('IsSelected');
            });
        },

        /// <summary>Chọn hoặc bỏ chọn tất cả</summary>
        checkAndUnCheckAll: function (e) {
            var checkAll = this.$(e.target);
            var checked = !checkAll.hasClass('checked');
            if (checked) {
                checkAll.addClass('checked');
            } else {
                checkAll.removeClass('checked');
            }
            this.model.get('Result').each(function (doc) {
                doc.set('IsSelected', checked);
            });
        },

        unCheckAll: function (e) {
            var checkAll = $(e.target).closest('.checkAll');
            checkAll.removeClass("checked");
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

        toggleSearchAdvanceForm: function (e) {
            if (!this.$("#fileSearch").is(":checked")) {
                if ($(e.target).is(":checked")) {
                    if (!this.isLoadedSearchAdvance) {
                        this.bindSearchAdvanceForm();
                        this.isLoadedSearchAdvance = true;
                    }
                    this.$('#divSearchDoc').show();
                    this.$('#divSearchAdvance').show();
                } else {
                    this.$('#divSearchAdvance').hide();
                }
            }
        },

        toggleSearchFileForm: function (e) {
            if ($(e.target).is(":checked")) {
                this.searchType = egov.enum.searchType.file;
                this.$('#divSearchDoc').hide();
                this.$('#divSearchAdvance').hide();
                this.$('#divSearchFile').show();
                this.$("#advanceSearch").prop("disabled", true);
            } else {
                this.$("#advanceSearch").prop("disabled", false);
                this.searchType = egov.enum.searchType.document;
                this.$('#divSearchFile').hide();
                this.$('#divSearchDoc').show();
                if (this.$("#advanceSearch").is(":checked")) {
                    this.$('#divSearchAdvance').show();
                } else {
                    this.$('#divSearchAdvance').hide();
                }
            }
        },

        bindSearchAdvanceForm: function () {
            var that = this;
           
            that.$("#FromPubDateStr").datepicker("destroy");
            that.$("#ToPubDateStr").datepicker("destroy");

            that.$('#FromPubDateStr').datepicker({
                changeMonth: true,
                dateFormat: "dd/mm/yy",
                onClose: function (selectedDate) {
                    that.$("#ToPubDateStr").datepicker("option", "minDate", selectedDate);
                }
            });
            that.$("#ToPubDateStr").datepicker({
                changeMonth: true,
                dateFormat: "dd/mm/yy",
                onClose: function (selectedDate) {
                    that.$("#FromPubDateStr").datepicker("option", "maxDate", selectedDate);
                }
            });

        },

        _changePage: function (page) {
            if (page == 0 || page === this.currentPage) {
                return;
            }

            this.currentPage = page;
            var that = this;

            if (that.isQuickSearch) {
                egov.request.quickSearch({
                    beforeSend: function () {
                        egov.pubsub.publish("status.processing", egov.resources.common.searching);
                    },
                    data: {
                        query: that.searchQuery,
                        type: egov.enum.searchType.document,
                        isUseCached: true,
                        page: page
                    },
                    success: function (result) {
                        that.model = result;
                        that.renderResult();
                    },
                    complete: function () {
                        egov.pubsub.publish("status.destroy");
                    }
                });
            } else {
                that.searchAdvance(page, true);
            }
        },

        _sort: function (e) {
            if (this.isRelationDoc) {
                this.model.Items = this.preRelationDocItems;
            }

            var target = $(e.target).closest('.sort');
            var column = target.attr("data-column");
            var isDescing = target.is(".desc");

            if (isDescing) {
                target.removeClass("desc").addClass("asc");
            } else {
                target.removeClass("asc").addClass("desc");
            }

            this.currentSort = {
                column: column,
                isDesc: !isDescing
            };

            this.renderResult();
        },

        _reSelected: function (e) {
            ///<summary>
            /// selected lại giá trị của o textbox
            ///</summary>
            if (!e)
                return;

            var target = $(e.target);
            var length = target.val().length;
            target[0].setSelectionRange(0, length);
        },

        _keypress: function (e) {
            var keycode = e.keyCode;
            if (keycode === 13) {
                this.searchAdvance(1, false);
                e.preventDefault();
                return false;
            }
        },

        _renderData: function () {
            var that = this;

            var showSigners = function (allUsers, allJobtitle, allUserDeptPosition) {
                that.allUsers = allUsers;
                that.allJobtitle = allJobtitle;
                that.allUserDeptPosition = allUserDeptPosition;

                var approvers = that._getApprovers();

                that.$approvers.append($.tmpl('<option value="${value}">${fullname} - ${username}</option>', approvers));
            };

            egov.dataManager.getAllUsers({
                success: function (allUsers) {
                    that.$("#CurrentUser, #UserCreate").each(function () {
                        var $el = $(this);
                        $(this).autocomplete({
                            source: allUsers,
                            focus: function (event, ui) {
                                $el.val(ui.item.label);
                                return false;
                            },
                            select: function (event, ui) {
                                // xác định các phòng ban user thuộc vào
                                $el.siblings("input:hidden").val(ui.item.value);
                                return false;
                            }
                        })
                        .data("autocomplete")._renderItem = function (ul, item) {
                            ul.addClass('dropdown-menu');
                            return $("<li></li>")
                                .data("item.autocomplete", item)
                                .append("<a>" + item.label + "</a>")
                                .appendTo(ul);
                        };
                    });

                    egov.dataManager.getAllJobtitle({
                        success: function (allJobtitle) {
                            egov.dataManager.getAllUserDeptPosition({
                                success: function (allUserDeptPosition) {
                                    showSigners(allUsers, allJobtitle, allUserDeptPosition);
                                }
                            });
                        }
                    });
                }
            });

            egov.dataManager.getAllAddress({
                success: function (result) {
                    result = _.sortBy(result, function (i) {
                        return i.Name;
                    });

                    that.$(".addressList").html($.tmpl('<li><a href="#">${Name}</a></li>', result));

                    that.$(".addressList li").click(function () {
                        that.$organization.val($(this).text());
                    });

                    var allAddress = _.map(result, function (i) {
                        return { label: i.Name, value: i.Name };
                    });

                    that.$organization.autocomplete({
                        source: allAddress,
                        focus: function (event, ui) {
                            that.$organization.val(ui.item.value);
                            return false;
                        },
                        select: function (event, ui) {
                            // xác định các phòng ban user thuộc vào
                            return false;
                        }
                    })
                        .data("autocomplete")._renderItem = function (ul, item) {
                            ul.addClass('dropdown-menu');
                            return $("<li></li>")
                                .data("item.autocomplete", item)
                                .append("<a>" + item.value + "</a>")
                                .appendTo(ul);
                        };
                }
            });

            egov.request.GetStores({
                data: { docTypeId: null },
                success: function (result) {
                    result = _.sortBy(result, function (i) {
                        return i.StoreName;
                    });
                    that.$stores.append($.tmpl('<option value="${StoreId}">${StoreName}</option>', result));
                }
            });

            egov.dataManager.getAllDept({
                success: function (result) {
                    that.$InOutPlaceId.append($.tmpl('<option value="${attr.id}">${data}</option>', result));
                }
            });

            egov.dataManager.getCategories({
                success: function (result) {
                    result = _.sortBy(result, function (i) {
                        return i.CategoryName;
                    });
                    that.$category.append($.tmpl('<option value="${CategoryId}">${CategoryName}</option>', result));
                }
            });

            egov.dataManager.getStorePrivate({
                success: function (result) {
                    that.$("#StorePrivateId").append($.tmpl('<option value="${id}">${name}</option>', result.storePrivate));
                    that.$("#StorePrivateId").append($.tmpl('<option value="${id}">${name}</option>', result.storeShare));
                }
            })
        },

        _getApprovers: function () {
            /// <summary>
            /// Trả về danh sách người ký duyệt
            /// </summary>
            var approvers = [];
            if (!approvers || approvers.length <= 0) {
                var approverIds = [];
                var allUsers = this.allUsers;
                var jobtitlies = this.allJobtitle;
                var jobDepts = this.allUserDeptPosition;
                jobDepts.forEach(function (jobDept) {
                    var jobId = jobDept.jobtitleid;
                    var checkApprover = _.find(jobtitlies, function (job) {
                        return job.value === jobId && job.isApprover;
                    });
                    if (checkApprover) {
                        approverIds.push(jobDept.userid);
                    }
                });

                approverIds = _.uniq(approverIds);
                approvers = _.filter(allUsers, function (user) {
                    return approverIds.indexOf(user.value) >= 0;
                });
            }

            return approvers;
        }
    });

    //View for search relation
    var SearchRelationResult = Backbone.View.extend({
        tagName: 'tr',
        template: SearchDocResultTemplate,
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

            egov.views.home.tab.openDocument(documentCopyId, that.model.get("DocumentCompendium"));
            return;
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
            });
        }
    });

    //View for search Home Page
    var SearchResult = Backbone.View.extend({
        tagName: "tr",

        initialize: function (option) {
            this.template = option.template;
            this.model = option.model;
            this.parent = option.parent;
            this.render();
            var that = this;
            that.$el.dbclick(
                    function (e) {
                        that.selected(e);
                    },
                    function (e) {
                        egov.views.home.tab.openDocument(that.model.DocumentCopyId, that.model.DocumentCompendium);
                    });
        },

        events: {
            "click .downloadFile": "downloadFile",
            "click .openFile": "openFile"
        },

        render: function () {
            this.$el.html($.tmpl(this.template, this.model));
        },

        selected: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);
            this.$el.parent().find("tr").removeClass("rowSelected");
            this.$el.addClass("rowSelected");
        },

        downloadFile: function (e) {
            /// <summary>
            /// Tải về file đính kèm.
            /// </summary>
            /// <param name="id">Id của file cần tải</param>
            //egov.message.processing(egov.resources.common.processing, false);
            var id = this.model.ContentId;
            if (id !== 0) {
                downloadAttachment(id);
            }
        },

        openFile: function (e) {
            var that = this;
            if (!egov.plugin && $("#egovPlugin").length === 0) {
                EgovPlugin.appendPlugin(that.parent.pluginName, function () {
                    that.openFile();
                });
            }
            else {
                var id = that.model.ContentId;
                if (id !== 0) {
                    if (that.model.isOpen) {
                        egov.plugin.openFile(that.model.fileData, false);
                    }
                    else {
                        egov.request.downloadAttachment({
                            data: { id: id },
                            success: function (data) {
                                var result = JSON.parse(data);
                                if (result) {
                                    if (result.error) {
                                        //egov.message.error(result.error);
                                    } else {
                                        var filename = id + '.' + that.model.Extension;
                                        var filesize = egov.plugin.writeFileBase64(filename, result.content, false);
                                        egov.plugin.openFile(filename, true);
                                        that.model.isOpen = true;
                                        that.model.fileData = filename;
                                    }
                                }
                            },
                            error: function () {
                                //egov.message.error(_resource.errorDownload);
                            },
                            complete: function () {
                                //egov.message.hide();
                            }
                        });
                    }
                }
            }
        }
    });

    var downloadAttachment = function (id) {
        /// <summary>
        /// Tải về file đính kèm.
        /// </summary>
        /// <param name="id">Id của file cần tải</param>
        require(['filedownload'], function () {
            var link = '/Attachment/DownloadAttachment/' + id;
            $.fileDownload(link, {
                successCallback: function () {
                    //egov.message.hide();
                },
                failCallback: function (response) {
                    var html = $(response);
                    try {
                        var json = JSON.parse(html.text());
                        //egov.message.error(json.error);
                    } catch (e) {
                        //egov.message.error(egov.resources.file.errorDownload);
                    }
                }
            });
        });
    };

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

    return EgovSearch;
})