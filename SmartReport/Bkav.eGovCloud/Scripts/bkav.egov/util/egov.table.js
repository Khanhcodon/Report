
(function () {
        "use strict";

        /// <summary>Lớp xử lý table</summary>
        var Table = Backbone.View.extend({
            selectedClass: 'rowSelected',
            events: {
                'click tr': 'selected',
                'click .checkAll': 'selectMany',
                "mousedown th": "startResize",
                "mousemove": "onResize",
                "mouseup": "endResize",
            },

            pressed: false,
            startWidth: undefined,

            /// <summary>Khởi tạo</summary>
            initialize: function (option) {
                this.$el = option.el;
                this.resizable = option.resizable;
                this.scrollable = option.scrollable;
                this.render();
            },

            /// <summary>Xử lý các sự kiện</summary>
            render: function () {
                if (this.resizable) {
                    this.renderResizable();
                }
                if (this.scrollable) {
                    this.renderScollable();
                }
            },

            startResize: function () {
                this.pressed = true;
            },

            onResize: function () {
                if (this.pressed) {
                    this.$el.find("colgroup").remove();
                }
            },

            endResize: function () {
                if (this.pressed) {
                    this.pressed = false;
                }
            },

            /// <summary>Selected</summary>
            selected: function (e) {
                var target = $(e.target);
                var row = target.is("tr") ? target : target.parents('tr');
                if (e) {
                    e.preventDefault();
                }
                e = e || {};

                var isMultiSelect = e.ctrlKey || $(e.target).closest('.checkbox, [type="checkbox"]').length > 0;

                // Nếu nhấn nút Shift
                if (e.shiftKey) {
                    // Lấy row có index nhỏ nhất trong số các row đã selected
                    var fromIdx = this.$el.find('.' + this.selectedClass).first().index();

                    // Lấy index của row hiện tại
                    var toIdx = row.index();
                    var allRow = this.$el.find('tr');

                    // Xóa hết các selected
                    this.removeAllSelected();

                    // Gán lại selected cho các row trong vùng được chọn
                    for (var i = fromIdx; i <= toIdx; i++) {
                        var rowItm = allRow.eq(i);
                        rowItm.addClass(this.selectedClass);
                        this.select(rowItm);
                    }
                }
                else if (isMultiSelect) { // Nếu nhấn Ctrl hoặc click vào checkbox
                    if (row.hasClass(this.selectedClass)) {
                        row.find('.checkbox, [type="checkbox"]').removeClass('checked').prop('checked', false);
                        row.removeClass(this.selectedClass);
                    }
                    else {
                        this.select(row);
                    }
                }
                else {
                    this.removeAllSelected();
                    this.select(row);
                }

                if (isMultiSelect) {
                    e.stopPropagation();
                }
            },

            /// <summary>Select row hiện tại</summary>
            select: function (row) {
                row.addClass(this.selectedClass);
                row.find('.checkbox, [type="checkbox"]').addClass('checked').prop('checked', true);
            },

            /// <summary>Select tất cả các row trong table</summary>
            selectAll: function () {
                this.$el.find('tr').addClass(this.selectedClass);
                this.$('tr .checkbox, tr [type="checkbox"]').addClass('checked').prop('checked', true);
            },

            /// <summary>Select, unselect tất cả</summary>
            selectMany: function (e) {
                var checkAll = $(e.target).closest('.checkAll');
                if (checkAll.hasClass("checked") || checkAll.is(":checked")) {
                    this.removeAllSelected();
                    checkAll.removeClass('checked').removeAttr('checked');
                }
                else {
                    this.selectAll();
                    checkAll.addClass('checked').prop('checked', true);
                }
                if (e) {
                    e.stopPropagation();
                    e.preventDefault();
                }
            },

            /// <summary>Remove tất cả các selected</summary>
            removeAllSelected: function () {
                this.$('tr').removeClass(this.selectedClass);
                this.$('tr .checkbox, tr [type="checkbox"]').removeClass('checked').prop('checked', false);
            },

            /// <summary>Cho phép kéo thả các cột trong table</summary>
            renderResizable: function () {
                this.$('th').resizable();
            },

            /// <summary>Cho phép scoll phần body của table</summary>
            renderScollable: function () {

            }
        });

        egov.utils.table = Table;

    })();
