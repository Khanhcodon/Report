define(function () {
    "use strict";

    //#region View

    /// <summary>Đối tượng View thể hiện toolbar</summary>
    egov.views.ToolbarView = Backbone.View.extend({

        events: {
            "keyup #SearchQuery": "_clientQuickSearch"
        },
        // Khởi tạo
        /// <param name="options" type="Object">options.model: ToolbarCollection danh sách các toolbar button. options.el: div container chứa các toolbar button</param>
        initialize: function (options) {
            if (!(options.model instanceof egov.models.toolbarList)) {
                throw 'Model truyền vào chưa đúng định dạng, chỉ chấp nhận model là egov.models.toolbarList';
            }
            if (options.el instanceof jQuery) {
                this.$el = options.el;
            }
            else {
                this.el = options.el;
            }
            this.documents = options.documents;
            this.$el.addClass('nav nav-pills');

            this.render();
        },

        // Render ra các toolbar button theo cấu hình
        render: function () {
            var that = this;
            this.model.each(function (toolbarModel) {
                var toolbarItm = new ToolbarItm({
                    model: toolbarModel
                });
                if (!egov.isMobile) {
                    that.$el.prepend(toolbarItm.$el);
                }
                else {
                    that.$el.append(toolbarItm.$el);
                }
            });
        },

        _clientQuickSearch: function (e) {
            ///<Summary>Tìm kiếm trên client</Summary>
            ///<param name="e"></param>
            if (e) {
                egov.helper.destroyClickEvent(e);
                var value = $(e.target).val();
                if (egov.isMobile) {
                    this.documents.clientQuickSearch(value);
                    egov.events.isQuickFilter = true;
                }
                else if (e.keyCode == 13 || value == null) {
                    egov.events.pressedEnterToSearch = true;
                    this.documents.clientQuickSearch(value);
                }
            }
        }
    });

    /// <summary>Đối tượng View thể hiện 1 nút trên toolbar</summary>
    var ToolbarItm = Backbone.View.extend({
        // Dom element
        tagName: 'li',
        className: 'dropdown',

        events: {
            'click ul.dropdown-menu > li > a, li > a': 'selected',
            'tap li': 'selectedMobile'
        },

        // Khởi tạo
        initialize: function () {
            ///Giao diện bình thường
            if (!egov.isMobile) {
                if (this.model.get('id')) {
                    this.$el.attr('id', this.model.get('id'));
                }
                if (this.model.get('className') !== '') {
                    this.$el.addClass(this.model.get('className'));
                }
                if (this.model.get('disable')) {
                    this.$el.addClass('disabled');
                }
            }
                ///Giao diện tablet-mobile
            else {
                this.$el.addClass(this.model.get('className'));
                this.$el.addClass('pull-left hidden-xs');
            }

            this.render();
        },

        // Đổ dữ liệu vào template
        render: function () {
            var that = this;
            require([getTemplate()], function (ToolbarTemplate) {
                that.$el.html($.tmpl(ToolbarTemplate, that.model.toJSON()));
                if (that.model.get('selected')) {
                    that.$el.addClass('active');
                }
                that.$('a').etip();
            });

            if (this.model.get('showSelected')) {
                this.$selectedText = this.$('.selected-text');
                var data = this.model.get('data');
                if (data !== null && typeof data !== 'function') {
                    var selected = _.find(data, function (item) {
                        return item.selected;
                    });
                    if (selected) {
                        this.$selectedText.text(this.model.get('text') + ':' + selected.text);
                    }
                    else {
                        this.$selectedText.text(this.model.get('text') + ':' + this.model.get('defaultSelectedText'));
                    }
                }
                else {
                    this.$selectedText.text(this.model.get('text') + ':' + this.model.get('defaultSelectedText'));
                }
            }
        },

        selected: function (event) {
            if (!event) {
                return;
            }
            event.preventDefault();

            var target = $(event.target);
            target.closest('li').toggleClass('active');
            if (this.model.get('isDropdownMenu')) {
                if (target.is('.dropdown-toggle')) {
                    return;
                }
                var value = target.attr('value');
                var itmModel = _.find(this.model.get('data'), function (itm) {
                    return itm.value == value;
                });
                if (itmModel && typeof this.model.get('callback') === 'function') {
                    this.model.get('callback')(itmModel, target);
                }
            } else {
                if (typeof this.model.get('callback') === 'function') {
                    this.model.get('callback')(this);
                }
            }
        },

        selectedMobile: function (event) {
            if (!event) {
                return;
            }
            event.preventDefault();

            var target = $(event.target);
            target.parent().siblings().removeClass('active');
            target.parent().addClass('active');
            var value = target.attr('value');
            var itmModel = _.find(this.model.get('data'), function (itm) {
                return itm.value == value;
            });
            if (itmModel && typeof this.model.get('callback') === 'function') {
                this.model.get('callback')(itmModel);
            }
        },

        setSelectedText: function (selected) {
            this.$selectedText.text(this.model.get('text') + ':' + selected);
        }
    });

    //#endregion

    //#region Private Methods

    function getTemplate() {
        /// <summary>
        /// Trả về template cho toolbar
        /// </summary>
        /// <returns type=""></returns>
        return egov.isMobile
                        ? egov.template.documentList.desktopToolbar
                        : egov.template.documentList.mobileToolbar;
    }

    //#endregion

    return egov.views.ToolbarView;
});