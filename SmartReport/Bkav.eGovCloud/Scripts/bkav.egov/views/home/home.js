define(function () {

    "use strict";

    var listSize = {
        small: 0, // chế độ xem nhỏ nhất: padding: 2
        medium: 1, // Chế độ xem bình thường: padding: 5
        large: 2 // chế độ xem lớn: padding: 8 (default)
    };

    /// <summary>Đối tượng View trang chủ xử lý hồ sơ</summary>
    var HomeView = Backbone.View.extend({
        // Dom element
        el: ".egov",

        events: {
            'click .tab-root': 'activeRootTab',
            'click #NewDocument': 'showDocType',
            'click #ListSearchIcon': 'quickSearchToggle',
            'touchend #NewDocument': 'showDocType',
            'touchend #ListSearchIcon': 'quickSearchToggle'
        },

        initialize: function (options) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="options"></param>
            var that = this,
                viewSize,
                eventName;

            if (!egov.isMobile) {
                // require(["homeShortkey"]);
                // Layout trang
                this.initLayout();
            }

            this.render(function () {
                viewSize = egov.cookie.viewSize();
                if (viewSize === undefined || isNaN(viewSize)) {
                    viewSize = listSize.large;
                }
                that.changeSize(viewSize);
                if (!egov.isMobile) {
                    $(window.document).keydown(function (e) {
                        var keycode = e.keyCode;
                        if (keycode === 116) {
                            window.parent.saveChangeBeforeUnload(e);
                        }
                    });

                    // Sự kiện click vào một nơi bất kỳ trên giao diện
                    $('body').on('click contextmenu', function (e) {
                        //  - Ẩn tất cả các toolbar đang hiển thị nội dung
                        that.hideAllContext(e);
                        // Ẩn các nội dung dropdown trên trang main
                        $(window.parent.document.body).click();
                    });
                }
                that._disableRightClick();

                $('body').on('contextmenu', function (e) {
                    return false;
                });
            });
        },

        // Layout trang chủ
        initLayout: function () {
            var resize = function () {
                if (egov.views.home.documentLayout) {
                    egov.views.home.documentLayout.resizeAll();
                    $('.grid-content').css({ height: "auto" });
                }
            };

            $("#center").layout({
                resizable: false,
                closable: false,
                north__spacing_open: 0,
                north__size: 38,
                center__paneSelector: "#tabContents",
                north__paneSelector: "#ulTabs",
                onresize: function () {
                    resize();
                }
            });

            /// Cấu hình layout phần chia phần toolbar và phần danh sách văn bản
            var documentSettingMain = {
                resizable: true,
                closable: false,
                spacing_closed: 4,
                spacing_open: 1,
                north__spacing_open: 0,
                north__size: 42,
                north__paneSelector: ".toolbar",
                center__paneSelector: ".document-list",
                onresize: function () {
                    resize();
                }
            };

            $(".document-process-main").layout(documentSettingMain);

            ///Cấu hình layout cho khung phần chia với phần tóm tắt văn bản với khung chứa toolbar và danh sách văn bản
            var documentSetting = {
                resizable: true,
                closable: true,
                east__initClosed: true,
                south__initClosed: true,
                south__spacing_open: 5,
                spacing_closed: 0,
                spacing_open: 1,
                center__paneSelector: ".document-process-main",
                south__paneSelector: ".preview-below",
                east__paneSelector: ".preview-right",
                east__minWidth: 300,
                east__size: 383,
                south__size: 250,
                onresize: function () {
                    resize();
                }
            };

            this.layout = $(".document-process").layout(documentSetting);

            var preView = egov.cookie.getQuickView();
            this.showPreview(preView);
        },

        // Render ra các vùng trên trang chủ
        render: function (callback) {
            ///Trên giao diện bình thường thì cho mở nhiều tab trên trang
            var that = this;

            if (egov.isMobile) {
                this.renderMobile();
                egov.callback(callback);
            }
            else {
                this.renderDefault(callback);
            }
        },

        renderDefault: function (callback) {
            var that = this;
            var command = function () {
                if (typeof callback === "function") {
                    callback();
                }
            }

            require(['tabView'], function (TabView) {
                // Render ra các tab
                that.tab = new TabView;
                // Render ra tree
                require(['treeView'], function (TreeView) {
                    that.tree = new TreeView({
                        subscribe: egov.pubsub.subscribe
                    });
                    command();
                });

                that._showDocumentNotificationSelected();
            });
        },

        renderMobile: function () {
            var that = this;
            // Render ra tree
            require(['treeView'], function (TreeView) {
                that.tree = new TreeView({
                    subscribe: egov.pubsub.subscribe
                });

                // Sự kiện click vào một nơi bất kỳ trên giao diện
                $('body').on('tap', function (e) {
                    //  - Ẩn tất cả các toolbar đang hiển thị nội dung
                    that.hideAllContext(e);
                });
            });
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

        // Hiển thị danh sách các loại văn bản, hồ sơ được phép khởi tạo
        showDocType: function () {
            var that = this;
            var leng;
            this.$DoctypeList.append($('<li>').append(egov.helper.loading));

            egov.dataManager.getCurrentDoctypes({
                success: function (result) {
                    that.$DoctypeList.find('.loading').parent().remove();
                    if (result === null) {
                        that.$DoctypeList.append($('<li>').text(egov.resources.home.docType.message.error.loading));
                        return;
                    }
                    that.$DoctypeList.find('ul').empty();
                    for (var i = 0; leng = result.length, i < leng; i++) {
                        var doctype = result[i];
                        var element = $('<a href="#">');
                        element.text(doctype.DocTypeName);
                        element.attr('id', doctype.DocTypeId).attr('name', doctype.DocTypeName);
                        element.bind('click', function () {
                            that.tab.addDocument($(this).attr('id'), $(this).attr('name'));
                        });

                        var li = $('<li>').addClass('list-group-item').html(element);
                        if (doctype.CategoryBusinessId === egov.enum.businessType.vbden) {
                            that.$('#vbDen ul').append(li);
                        }
                        else if (doctype.CategoryBusinessId === egov.enum.businessType.vbdi) {
                            that.$('#vbDi ul').append(li);
                        }
                        else {
                            that.$('#hsmc ul').append(li);
                        }

                        that.$DoctypeList.find('li > ul').each(function () {
                            if ($(this).find('li').length === 0) {
                                $(this).parent().hide();
                            }
                        });
                    }
                }
            });
        },

        getActiveTab: function () {
            return $('#ulTabs').children("li.tabs-active");
        },

        editDynamicForm: function (options) {
            require(['knockout'], function (ko) {
                // window.ko = ko;
                require(['views/document/toolbar/editFormDynamic-view'],
                    function (EditFormDynamicView) {
                        new EditFormDynamicView({
                            frame: options.frame,
                            doctypeId: options.doctypeId,
                            contentId: options.contentId,
                            isMain: options.isMain,
                            contentName: options.contentName
                        });
                    }
                );
            });
        },

        editHtmlForm: function (options) {
            require(['views/document/toolbar/editFormHTML-view'],
                function (EditFormHTMlView) {
                    new EditFormHTMlView({
                        frame: options.frame,
                        doctypeId: options.doctypeId,
                        contentId: options.contentId
                    });
                }
            );
        },

        renderDynamicForm: function (options) {
            require(['knockout'], function (ko) {
                // window.ko = ko;
                require(['views/document/toolbar/formDynamic-view'],
                    function (FormDynamicView) {
                        new FormDynamicView({
                            target: options.target,
                            formJson: options.formJson,
                            isView: options.isView,
                            frame: options.frame
                        });
                    }
                );
            });
        },

        changeSize: function (value) {
            /// <summary>
            /// Thay đổi kích thước các dòng trong danh sách văn bản
            /// </summary>
            /// <param name="value">egov.locache.documentListSize</param>            

            if (value === listSize.small) {
                this.$("#colorTheme").removeClass('medium-size large-size');
            }
            else if (value === listSize.medium) {
                this.$("#colorTheme").addClass('medium-size');
                this.$("#colorTheme").removeClass('large-size');
            }
            else {
                this.$("#colorTheme").addClass('large-size');
                this.$("#colorTheme").removeClass('medium-size');
            }

            egov.cookie.viewSize(value);

            //if (egov.locache.hasSupportLocalStorage) {
            //    egov.localStorage.viewSize(value);
            //}
            //else {
            //    egov.cookie.viewSize(value);
            //}


        },

        showPreview: function (value, hasUpdateServer) {
            /// <summary>
            /// Hiển thị preview
            /// </summary>
            /// <param name="value">egov.helper.quickViewType</param>

            if (value === undefined || isNaN(value)) {
                value = egov.enum.quickViewType.below;
            }

            else if (typeof value !== "number") {
                value = parseInt(value);
            }

            egov.setting.user.userSetting.quickView = value;
            if (value === egov.enum.quickViewType.hide) {
                this.layout.close('south');
                this.layout.close('east');
            }
            else if (value === egov.enum.quickViewType.right) {
                this.layout.close('south');
                this.layout.open('east');
            }
            else {
                // mặc định hiển thị ở dưới
                this.layout.close('east');
                this.layout.open('south');
            }

            if (false) {//egov.locache.hasSupportLocalStorage
                egov.localStorage.setQuickView(value);
            }
            else {
                egov.cookie.setQuickView(value);
            }

            //Hiển thị lại tóm tắt văn bản tại vị trí mới chọn
            if (egov.views && egov.views.home && egov.views.home.documents) {
                egov.views.home.documents.reQuickView();
            }

            //  Lưu thiết lập về server
            if (hasUpdateServer) {
                egov.request.setUserConfig({
                    data: {
                        QuickView: value
                    }
                });
            }
        },

        _disableRightClick: function () {
            // Disable right click
            this.$el.bind('contextMenu', function (e) {
                e.preventDefault();
                that.hideAllContext(e);
            });
        },

        _showDocumentNotificationSelected: function () {
            /// <summary>
            /// Hiển thị văn bản notify được chọn
            /// </summary>

        }
    });

    return HomeView;
});