define(function () {

    //#region View

    /// <summary>Đối tượng View thể hiện context menu</summary>
    var ContextMenuView = Backbone.View.extend({

        // Dom element
        className: 'dropdown-menu',
        tagName: 'ul',

        // Khởi tạo
        initialize: function () {
            this.selector = this.model.get('selector');
            $('body').append(this.$el);
            this.$el.css(this.model.get('style'));
            if (this.model.get('isShowLoading')) {
                this.renderLoading();
            }

            this.$el.hide();
            // Gán lại toolbar đang hiển thị
            if (egov.views.home.showingContext) {
                egov.views.home.showingContext.hide();
            }

            return this;
        },

        // Render dữ liệu
        render: function () {
            var that = this;
            if (egov.views.home.showingContext) {
                egov.views.home.showingContext.hide();
            }
            egov.views.home.showingContext = that;
            that.show();

            // Nếu đã bind dữ liệu rồi thì ko cần bind nữa
            if (that.$el.html() !== '') {
                return;
            }

            if (that.model.get('isDatePicker')) {
                that.showDatePickerContent(function (dateText, inst) {
                    if (typeof that.model.get('callback') === 'function') {
                        that.model.get('callback')(inst, dateText);
                    }
                    that.hide();
                });
            }
            else {
                if (typeof that.model.get('data') === 'function') {
                    that.renderLoading();
                    that.model.set('data', that.model.get('data')());
                    that.bind();
                }
                else if (that.model.get('data') === null) {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

                    that.renderLoading();

                    this.isGettingData = $.get(that.model.get('dataUrl'), that.model.get('param'))
                       .done(function (result) {
                           that.model.set('data', new egov.models.contextMenuList(result));
                           that.bind();
                       })
                       .always(function () {
                           that.removeLoading();
                           egov.pubsub.publish(egov.events.status.destroy);
                       });
                }
                else {
                    that.bind();
                }
            }
        },

        bind: function () {
            this.show();
            if (this.isLoading()) {
                this.removeLoading();
            }
            var that = this;
            if (!(this.model.get('data') instanceof egov.models.contextMenuList)) {
                this.model.set('data', new egov.models.contextMenuList(this.model.get('data')));
            }
            this.model.get('data').each(function (contextModel) {
                var contentItem = new ContextMenuItem({ model: contextModel });
                that.$el.append(contentItem.$el);

                // Hàm thực thi trước khi gọi callback của các item trong context
                contentItem.on('callback', function () {
                    // Ẩn content khi click chọn 1 item
                    that.hide();
                    // Thực thi hàm callback cho cả contextmenu trước khi thực thi hàm callback cho mỗi item trong context
                    if (typeof that.model.get('callback') === 'function') {
                        that.model.get('callback')(contextModel, contextModel.get('text'));
                    }
                });
            });
            var lengthData = this.model.get('data').length;
            // set position
            var position = this.model.get('position');
            if (position.of == undefined) {
                position.of = this.selector;
            }
            this.$el.position(position);

            var heightContextMenu = this.$el.outerHeight();
            var pageHeight = $(document).height()
            var pageTop = position.of.pageX;
            var pageDown = pageHeight - pageTop;
            if (pageHeight < heightContextMenu) {
                this.$el.css({
                    "height": "100%",
                    "overflow": "auto",
                    "top": "0px"
                })
            } else if (pageDown < heightContextMenu && pageTop < heightContextMenu) {
                this.$el.css({
                    "overflow": "auto",
                    "top": "0px"
                })
            } else if (this.$el.position().top < 0) {
                var positionTop = heightContextMenu - pageTop;
                var top = positionTop + "px"
                this.$el.css({
                    "top": 0,
                    "bottom": "auto"
                })
            } if (this.$el.position().top + heightContextMenu > pageHeight) {
                var positionBottom = heightContextMenu - pageDown;
                var bottom = positionBottom + "px"
                this.$el.css({
                    "top": "auto",
                    "bottom": 0
                })
            } 
            
        },

        // Hiển thị loading khi đang lấy dữ liệu
        renderLoading: function () {
            this.$el.addClass('dropdown-loading');

            // set position
            var position = this.model.get('position');
            if (position.of == undefined) {
                position.of = this.selector;
            }

            this.$el.position(position);

            this.show();
        },

        // Đang hiển thị loading
        isLoading: function () {
            return this.$el.hasClass('dropdown-loading');
        },

        // Bỏ loading trước khi bind dữ liệu
        removeLoading: function () {
            this.$el.removeClass('dropdown-loading');
            if (this.model.get('height') !== 0) {
                this.$el.height(this.model.get('height'));
            }
            else {
                this.$el.height('auto');
            }
        },

        // Hiển thị content
        show: function () {
            this.$el.show();
            egov.views.hasContextmenu = true;
        },

        // Hiển thị content dạng datepicker
        showDatePickerContent: function (callback) {
            var that = this;
            this.$el.html($('<div>').datepicker({
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateText, inst) {
                    that.hide();
                    if (typeof callback === 'function') {
                        callback(dateText, inst);
                    }
                }
            }));

            // set position
            var position = this.model.get('position');
            if (position.of == undefined) {
                position.of = this.selector;
            }

            this.$el.position(position);
        },

        // Ẩn content
        hide: function () {
            egov.views.hasContextmenu = false;

            //Hủy bỏ ajax
            if (this.isGettingData) {
                this.isGettingData.abort();
            }

            this.$el.fadeOut('fast');
        }
    });

    /// <summary>Đối tượng View thể hiện 1 item trong nội dung context</summary>
    var ContextMenuItem = Backbone.View.extend({
        tagName: 'li',
        className: '',
        selectedClass: 'active',
        separatedClass: 'divider',

        events: {
            'click': 'select',
            'contextmenu': 'select'
        },

        initialize: function () {
            if (this.model.get('name') === '---') {
                this.renderSeparated();
            }
            else {
                this.render();
                this.model.view = this;
                if (this.model.get('className')) {
                    this.$el.addClass(this.model.get('className'));
                }
                var that = this;
                this.model.on('change:selected', function (model, selected) {
                    if (selected) {
                        model.collection.each(function (itm) {
                            if (itm.get('selected') && itm.get('text') !== model.get('text')) {
                                itm.set('selected', false);
                            }
                        });
                    }
                    that.$el.siblings().removeClass(that.selectedClass);
                    that.$el.addClass(that.selectedClass);
                });
            }
        },

        renderSeparated: function () {
            this.$el.addClass(this.separatedClass);
        },

        render: function () {
            var html = '<a href="#"><div><span class="icon ${iconClass}"></span><span>${text}</span></div></a>';
            var data = { iconClass: this.model.get('iconClass'), text: this.model.get('text') };
            this.$el.html($.tmpl(html, data)).addClass("contextMenuTest");
        },

        select: function (e) {
            if (!e)
                return;

            this.model.set('selected', true);
            this.trigger('callback');
            if (typeof this.model.get('callback') === 'function') {
                this.model.get('callback')(this.model);
            }
        }
    });

    //#endregion

    window.ContextMenu = ContextMenuView;
});
