define([egov.template.documentList.DocumentItem], function (DocumentItemTemplate) {
    "use strict";
    
    var menuTmp = '<li class="mdl-list__item mdl-js-ripple-effect is-group" id="treeGroup${TreeGroupId}"><div  class="mdl-border"><img class="c-header" src="../../Content/Images/home/list.png" alt=""> <span class="mdl-list__item-primary-content"><span class="node-name">${TreeGroupName}</span></span><span class="mdl-list__item-secondary-action"><span class="node-count"></span></span><span class="material-icons mdl-list__item-icon">keyboard_arrow_right</span></div></li>';
    var treeTmp = '<li class="mdl-list__item mdl-js-ripple-effect" funcNoteId="${funtionIdNote}" params="${params}" node-id="${functionId}"><div class="mdl-border-child"><img class="c-header" src="../../Content/Images/home/list.png" alt=""><span class="mdl-list__item-primary-content"><span class="node-name">${name}</span></span><span class="mdl-list__item-secondary-action"><span class="node-count"></span></span><span class="material-icons mdl-list__item-icon">keyboard_arrow_right</span></div></li>';
    $(document).on("swipe", function (event) {

    });

    /// <summary>Đối tượng View trang chủ xử lý hồ sơ</summary>
    var HomeView = Backbone.View.extend({
        // Dom element
        el: "#documentFrame",
          
        events: {
            'click #btnSearch': '_openSearch',
            'click #menu-document li': '_changeDocumentsNode',
            'click .list-document li': "_openDocumentHandler",
            'click #btnbacktolistMobile': "_btnbacktolist",
            'click #menu-logout': '_logout',
        },
        pageSize: 30,
        page: 0,
        is_click_document: 0,

        initialize: function (options) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="options"></param>
            var that = this,
                viewSize,
                eventName;

            this._renderMenu();
            this.render();
            this._registerHandler();

            $("#menuDocument").height($("#egovMaincontent").height());

            // 20200115 VuHQ START comment
            //$.ajax({
            //    url: '/Home/GetTotalDocumentUnreadMultiFunction',
            //    method: 'post',
            //    data: { ids: JSON.stringify([342]) },
            //    success: function (result) {
            //        debugger;
            //        var a = result;
            //    }
            //});
            // 20200115 VuHQ END comment

            egov.document = this;
        },

        //taida 191219  --  mobile logout  --  START
        _logout: function (e)
        {
            $.ajax({
                url: '/account/logout',
                success: function (result) {
                    var cookies = document.cookie.split(";");

                    for (var i = 0; i < cookies.length; i++) {
                        var cookie = cookies[i];
                        var eqPos = cookie.indexOf("=");
                        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
                    }
                    location.reload();
                }
            });
        },

        //taida 191219  --  mobile logout  --  END

        _renderMenu: function () {
            var treeGroups = egov.setting.allTreeGroups;
            var documentMenu = this.$("#menu-document");
            documentMenu.html($.tmpl(menuTmp, treeGroups));

            var treeGroupData = _.groupBy(egov.setting.processFunction, (i) => {
                return i.treeGroupId;
            });

            _.each(_.keys(treeGroupData), (groupId) => {
                var data = treeGroupData[groupId];
                var treeNode = documentMenu.find("#treeGroup" + groupId);
                if (treeNode.length === 0) return;

                treeNode.after($.tmpl(treeTmp, data));
            });

            documentMenu.find("li").not(".is-group").first().addClass("active");
            var treeGroups = documentMenu.find(".is-group")
            treeGroups.each(function (e) {
                var target = $(this);
                if (target.next().is(".is-group") || target.next().length == 0) {
                    target.hide()
                }
            })
        },

        render: function () {
            var that = this;

            !egov.documentMenuInMainView && this._showDocuments();

            this._initDocumentCountInterval();

            $('body').on('tap', function (e) {
                //  - Ẩn tất cả các toolbar đang hiển thị nội dung
                that.hideAllContext(e);
            });
        },

        _registerHandler: function () {
            egov.pubsub.subscribe('documents.open', this._openDocument.bind(this));
            egov.pubsub.subscribe('tree.reloadSelected', this._showDocuments.bind(this));
        },

        _openSearch: function (e) {
            egov.mobile.showSearchPage("documents");
            require(['searchDocument'], function (DocumentSearchView) {
                new DocumentSearchView();
            });

            e.preventDefault();
        },

        _changeDocumentsNode: function (e) {
            egov.mobile.autoHideMainMemu();
            var target = $(e.target).closest('li');
            if (target.is(".is-group")) return;

            if (target.is(".active")) {
                // Load văn bản mới
                egov.documentMenuInMainView && this._showDocuments();
                return;
            }

            target.siblings().removeClass("active");
            target.addClass("active");

            this._showDocuments();
        },

        _showDocuments: function () {
            var that = this;
            var menuActive = this.$("#menu-document li.active");

            var nodeName = menuActive.find(".node-name").text();
            this.$(".header-title").text(nodeName);

            var dataUri = menuActive.attr("data-url");
            dataUri = '/HomeSMReport/GetDocuments';

            this._resetDocumentsView();

            if (egov.documentMenuInMainView) {
                this.$('#egovMaincontent').removeClass('hidden');
                this.$('.treelist').addClass('hidden');
            }

            var data = { id: menuActive.attr("funcNoteId") ? menuActive.attr("funcNoteId") : menuActive.attr("node-id"), paramsQuery: menuActive.attr("params") };
            this._getData(dataUri, data, function (documents) {
                if (documents.length === 0) {
                    var icon = menuActive.find(".material-icons").text();
                    var message = nodeName.replace("VB ", "");
                    that._showNoElementPage(icon, message);
                    return;
                }

                that.model = that._parseDocuments(documents);
                that._renderDocuments();
                that._loadMoreHandler();
            });
        },

        _resetDocumentsView: function () {
            this.$('div[data-group="important"], div[data-group="new"], div[data-group="normal"]').hide();
            this.$('#egovMaincontent .list-document').empty()
            this.isLoadAll = false;
            this.page = 0;
        },

        _getData: function (url, data, success) {
            $.ajax({
                url: url,
                data: data,
                type: 'post',
                beforeSend: function () {
                    egov.mobile.showProcessBar();
                },
                success: function (response) {
                    success(response.documents);
                },
                error: function () {

                },
                complete: function () {
                    egov.mobile.hideProcessBar();
                }
            });
        },

        _parseDocuments: function (documents) {
            var result = [];

            var otherDocuments = [];
            _.each(documents, function (doc) {
                var user = _.find(egov.setting.allUsers, function (u) {
                    return u.value === doc.UserCurrentId;
                });

                doc.Avatar = user == undefined ? egov.setting.noavatar : user.avatar;
                doc.date = doc.DateCreated;

                otherDocuments.push(doc);
            });

            result = otherDocuments;
            _.each(result, function (doc, idx) {
                doc.idx = idx;
                doc.pageNumb = Math.floor(idx / 30) + 1;
            });

            return result;
        },

        _renderDocuments: function () {
            if (this.isLoadAll)
                return;

            this._hideNoElementPage();
            var page = ++this.page;
            var that = this;

            var documents = _.where(this.model, { 'pageNumb': page });

            if (documents.length === 0) {
                this.isLoadAll = true;
                return;
            }

            that.$('div[data-group="normal"]').show();
            var tmp = '<div class="mdl-list__item mdl-list__item--two-line "  style="background-color:transparent;box-shadow:0 0.5px 0 #f0f0f0bf, 0 0 0 rgba(0,0,0,.8), 0 -1px 0 rgba(0,0,0,0.16);">' +
            '<span class="mdl-list__item-primary-content" >' +
            ' <img class="mdl-list__item-avatar user-avatar c-header" src="../../Content/Images/home/list.png" alt="">' +
                '<span class="mdl-list__item-sub-title" style="color:white">' + $(document).find('.header-title').html()+'</span>' +
                '</span >'+
            '</div>';
            that.$('div .list-document1').html(tmp);
            _.each(documents, function (doc) {
                doc.isToday && that.$('div[data-group="today"]').show();
                doc.isYesterday && that.$('div[data-group="yesterday"]').show()

                if (doc.isToday) {
                    that.$('div[data-group="today"] .list-document').append($.tmpl(DocumentItemTemplate, doc));
                    return;
                }

                doc.isYesterday ? that.$('div[data-group="yesterday"] .list-document').append($.tmpl(DocumentItemTemplate, doc))
                             : that.$('div[data-group="normal"] .list-document').append($.tmpl(DocumentItemTemplate, doc));
            });
            that.is_click_document = 1;
            this.$(".list-document > li").eq(0).click();

            $("#menuDocument").show();
            if (this._isMobile()) {
                $(".documents-detail").hide();
            }

            $('#btnMenu').show();
            $('#btnbacktolistMobile').hide();

            this.$(".list-document > li").eq(0).addClass("active")
            this.$(".list-document .mdl-list__item-sub-title").dotdotdot();
        },

        _loadMoreHandler: function () {
            return (this.model.length > this.pageSize) && this.$("#egovMaincontent").scrollLoadMore('next', this._renderDocuments.bind(this));
        },

        _openDocumentHandler: function (e) {
            var target = $(e.target).closest("li");
            var that = this;
            var id = target.attr("data-id");
            this._openDocument(id);
            e.preventDefault();

            if (this._isMobile()) {
                if (that.is_click_document > 0) {
                    // 20200304 VuHQ
                    //if (navigator.maxTouchPoints && navigator.maxTouchPoints > 2 && /MacIntel/.test(navigator.platform))
                    //if (/Macintosh/.test(navigator.userAgent) && 'ontouchend' in document)
                    if (navigator.userAgent.match(/iPad/i) != null)
                        $("#menuDocument").show();
                    else
                        $("#menuDocument").hide();

                    $(".documents-detail").show();
                    $("#btnbacktolistMobile").attr('data-id', "3");
                }
                that.is_click_document += 1;
                if (that.is_click_document === 1) {
                    $('#btnMenu').show();
                    $('#btnbacktolistMobile').hide();
                }
                else {
                    $('#btnMenu').hide();
                    $('#btnbacktolistMobile').show();
                }
            }
            else {
                $(".documents-detail").show();
            }
        },
        _btnbacktolist: function (e) {
            var action = $("#btnbacktolistMobile").attr('data-id');
            if (action == "3") {
                $("#menuDocument").show();
                $(".documents-detail").hide();
            }
            $("#btnbacktolistMobile").attr('data-id', (action - 1));
            $('#btnMenu').show();
            $('#btnbacktolistMobile').hide();
        },
        _openDocument: function (documentCopyId) {
            var that = this;
            var doc = _.find(that.model, function (d) {
                return d.DocumentCopyId == documentCopyId;
            });

            require(['documentView'], function (DocumentView) {

                //egov.mobile.showDetailPage("documents");

                var documentView = new DocumentView({
                    id: documentCopyId,
                    parent: that,
                    tab: that,
                    documentInfo: doc || {}
                });

                $('#documentFrame .documents-detail').html(documentView.$el);
                //egov.mobile.current.tab = documentView.$el;

                if (that._hasUpdateViewed(doc)) {
                    that._setViewed(doc.DocumentCopyId, true);
                }
            });
        },

        _hasUpdateViewed: function (doc) {
            // Cấu hình câu truy vấn trong Node chờ xử lý:
            // Thêm select 1 as hasUpdateViewed để xác định trường này cho nhanh.
            return doc.hasUpdateViewed == 1 && doc.IsViewed == false;
        },

        _setViewed: function (documentCopyId, viewed) {
            $(".list-document li[data-id='" + documentCopyId + "']").removeClass('isnew');
            egov.request.setDocumentViewed(
            {
                data: { id: documentCopyId, viewed: viewed },
                success: function (result) {
                    if (!result) {
                        return;
                    }
                }
            });
        },

        _showNoElementPage: function (icon, message) {
            this.$(".no-element-content").removeClass("hidden");
            this.$(".no-element-content .material-icons").text(icon);
            this.$(".no-element-content .message-info").text(message);
            this.$(".page-content").addClass("hidden");
        },

        _hideNoElementPage: function () {
            this.$(".no-element-content").addClass("hidden");
            this.$(".page-content").removeClass("hidden");
        },

        _initDocumentCountInterval: function () {
            var that = this;

            that._updateDocumentCount();

            this.updateDocumentCountInterval = setInterval(function () {
                that._updateDocumentCount();
            }, 2 * 60 * 1000); // 2 phút
        },

        _updateDocumentCount: function () {
            var that = this;
            //$.ajax({
            //    url: "/mobile/GetCountDocuments",
            //    success: function (result) {
            //        that.$("#menu-document .node-count").each(function () {
            //            var count = result[$(this).attr("data-target")];
            //            if (typeof count == "undefined" || count == "" || count == "0") {
            //                $(this).attr('style', 'display:unset;');
            //                $(this).text("0");
            //            } else {
            //                $(this).attr('style', 'display:block;');
            //                $(this).text(count);
            //            }
            //        });
            //    }
            //});

            // 20200115 VuHQ START update count number cho document
            var funtionIdNotes = jQuery.map(egov.setting.processFunction, function (n, i) {
                return n.funtionIdNote;
            });

            var uniqueFuntionIdNotes = $.unique(funtionIdNotes);

            $.ajax({
                url: '/Home/GetTotalDocumentUnreadMultiFunction',
                method: 'post',
                data: { ids: JSON.stringify(uniqueFuntionIdNotes) },
                success: function (result) {
                    var index = 0;
                    that.$("#menu-document .node-count").each(function () {
                        if ($(this).closest(".is-group").css('display') == 'none')
                            return;
                        //var count = result[$(this).attr("data-target")];

                        var count = _.size(result)

                        if (count == 0) {
                            $(this).attr('style', 'display:unset;');
                            $(this).text("0/0");
                        } else {
                            $(this).attr('style', 'display:block;');
                            $(this).text(result[index].totalUnread.toString() + "/" + result[index].total.toString());
                        }

                        index++;
                    });
                }
            });
            // 20200115 VuHQ END update count number cho document
        },

        // Ẩn tất cả các context đang mở
        hideAllContext: function (e) {
            if (!e) {
                return;
            }

            var target = $(e.target);
            if (this.showingContext  // Nếu có toolbar đang hiển thị nội dung
                && !target.is(this.showingContext.selector)  // và target không phải là toolbar button đó
                && this.showingContext.selector.find(target).length === 0) // và cũng không phải là các html con của toolbar button
            {
                // ẩn nội dung của nó đi
                this.showingContext.hide();
            }

            // ẩn các dropdown
            if (!target.is('.custom-dropdown') && $('.custom-dropdown').find(target).length === 0) {
                $('.custom-dropdown').hide();
            }
        },

        //check iphone, ipad giống bên css: iphone => width <= 1024
        _isMobile: function () {

            return document.documentElement.clientWidth <= 1024;
        }
    });

    return HomeView;
});