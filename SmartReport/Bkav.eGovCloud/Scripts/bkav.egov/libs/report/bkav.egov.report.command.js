
(function (eReport) {

    var config = eReport.config;

    var tableObj;

    //#region command

    var command = function (selecteds) {
        this.selecteds = selecteds;
    };

    command.prototype = {
        addCss: function (name, value) {
            if (this.selecteds.length > 0) {
                this.selecteds.each(function () {
                    $(this).css(name, value);
                });
            } else {
                this.selecteds.css(name, value);
            }
        },
        increaseFont: function () {
            this.selecteds.each(function () {
                var size = parseInt($(this).css("font-size")) + 1;
                $(this).css("font-size", size);
            });
        },
        decreaseFont: function () {
            this.selecteds.each(function () {
                var size = parseInt($(this).css("font-size")) - 1;
                $(this).css("font-size", size);
            });
        },
        remove: function () {
            this.selecteds.remove();
        },
        append: function (content) {
            this.selecteds.append(content);
        },
        appendTo: function (target) {
            target.append(this.selecteds);
        },
        changeText: function (text) {
            this.selecteds.text(text);
            _enableEffect($(this));
        },
        show: function () {
            this.selecteds.show();
        },
        hide: function () {
            this.selecteds.hide();
        }
    };

    //#endregion


    var eGovCommands = {
    };

    //#region Format Css

    eGovCommands.addCss = function (selecteds, cssName, cssValue) {
        this.doc = new command(selecteds);
        this.name = cssName;
        this.value = cssValue;
        var oldValues = [];
        selecteds.each(function () {
            oldValues.push({ obj: this, value: $(this).css(cssName) });
        });
        this.oldValues = oldValues;
    };

    eGovCommands.addCss.prototype = {
        execute: function () {
            this.doc.addCss(this.name, this.value);
        },
        unexecute: function () {
            var prop = this.name;
            this.oldValues.forEach(function (itm) {
                $(itm.obj).css(prop, itm.value);
            });
        }
    };

    eGovCommands.increaseFont = function (selecteds) {
        this.doc = new command(selecteds);
    };

    eGovCommands.increaseFont.prototype = {
        execute: function () {
            this.doc.increaseFont();
        },
        unexecute: function () {
            this.doc.decreaseFont();
        }
    };

    eGovCommands.decreaseFont = function (selecteds) {
        this.doc = new command(selecteds);
    };

    eGovCommands.decreaseFont.prototype = {
        execute: function () {
            this.doc.decreaseFont();
        },
        unexecute: function () {
            this.doc.increaseFont();
        }
    };

    //#endregion

    //#region Same size

    eGovCommands.sameSize = function (selecteds, w, h) {
        this.doc = new command(selecteds);
        this.width = w;
        this.height = h;
        var oldValues = [];
        selecteds.each(function () {
            oldValues.push({ obj: this, w: $(this).width(), h: $(this).height() });
        });
        this.oldValues = oldValues;
    };

    eGovCommands.sameSize.prototype = {
        execute: function () {
            this.doc.addCss("width", this.width);
            this.doc.addCss("height", this.height);
        },
        unexecute: function () {
            this.oldValues.forEach(function (itm) {
                $(itm.obj).width(itm.w).height(itm.h);
            });
        }
    };

    //#endregion

    //#region Remove

    eGovCommands.delete = function (selecteds) {
        this.doc = new command(selecteds);
        var oldValues = [];
        selecteds.each(function () {
            oldValues.push({ obj: this, parent: $(this).parent(), text: $(this).text() });
        });
        this.oldValues = oldValues;
    };

    eGovCommands.delete.prototype = {
        execute: function () {
            if (this.doc.selecteds.is("th, td")) {
                this.doc.selecteds.text("");
            }
            else {
                this.doc.remove();
            }
            _enableEffect(this.doc.selecteds);
        },
        unexecute: function () {
            this.oldValues.forEach(function (itm) {
                if ($(itm.obj).is("th, td")) {
                    $(itm.obj).text(itm.text);
                }
                else {
                    $(itm.parent).append(itm.obj);
                }
                _enableEffect($(itm.obj));
            });
        }
    };

    //#endregion

    //#region Add Text

    eGovCommands.addText = function (target, text, top, left) {
        $(config.tmp.text_class).remove();
        if (text == null || text == "") {
            text = prompt("Nhập nội dung: ", "");
        }
        if (text == null) {
            return;
        }
        this.doc = new command(new eReport.field.text().init(text));
        this.text = text;
        this.target = target;
        this.top = top;
        this.left = left;
    };

    eGovCommands.addText.prototype = {
        execute: function () {
            if (this.target.is("td, th")) {
                this.target.append(this.text);
            }
            else {
                this.doc.addCss("top", this.top);
                this.doc.addCss("left", this.left);
                this.doc.selecteds.addClass("used-field");
                this.doc.appendTo(this.target);
            }
        },
        unexecute: function () {
            if (this.target.is("td, th")) {
                this.target.text(this.target.text().replace(this.text, ""));
            }
            else {
                this.doc.selecteds.remove();
            }
        }
    };

    //#endregion

    //#region Add Field

    eGovCommands.addField = function (target, value, datatype, top, left) {
        this.doc = new command(new eReport.field.dataField().init(value, datatype));
        this.target = target;
        this.top = top;
        this.left = left;
    };

    eGovCommands.addField.prototype = {
        execute: function () {
            this.doc.addCss("top", this.top);
            this.doc.addCss("left", this.left);
            this.doc.selecteds.addClass("used-field");
            this.doc.appendTo(this.target);
        },
        unexecute: function () {
            this.doc.selecteds.remove();
        }
    };

    //#endregion

    //#region Add Image

    eGovCommands.addImage = function (target, top, left) {
        $(config.tmp.img_class).remove();
        this.doc = new command(new eReport.field.image().init());
        this.target = target;
        this.top = top;
        this.left = left;
    };

    eGovCommands.addImage.prototype = {
        execute: function () {
            this.doc.addCss("top", this.top);
            this.doc.addCss("left", this.left);
            this.doc.selecteds.width(30).height(30);
            this.doc.selecteds.addClass("used-field");
            this.doc.appendTo(this.target);
        },
        unexecute: function () {
            this.doc.remove();
        }
    };

    //#endregion

    //#region Add Table

    /// <summary>Description</summary>
    /// <param name="type" type="String">header, footer, detail, gHeader, gFooter</param>
    eGovCommands.addTable = function (target) {
        $(config.tmp.table_class).remove();
        this.target = target;
        if (tableObj == undefined) {
            tableObj = new eReport.field.table().init();
        }
        this.table = tableObj;
    };

    eGovCommands.addTable.prototype = {
        execute: function () {
            if (this.target.parent().hasClass(config.contentPanel.header_className) && $(config.contentPanel.header_class).find("table").length == 0) {
                this.target.append($(this.table.header));
            }

            else if (this.target.parent().hasClass(config.contentPanel.footer_className) && $(config.contentPanel.footer_class).find("table").length == 0) {
                this.target.append($(this.table.footer));
            }

            else if (this.target.parent().hasClass(config.contentPanel.detail_className) && $(config.contentPanel.detail_class).find("table").length == 0) {
                this.target.append($(this.table.detail));
            }

            else if (this.target.parent().hasClass(config.contentPanel.groupHeader_className) && $(config.contentPanel.groupHeader_class).find("table").length == 0) {
                this.target.append($(this.table.groupHeader));
            }

            else if (this.target.parent().hasClass(config.contentPanel.groupFooter_className) && $(config.contentPanel.groupFooter_class).find("table").length == 0) {
                this.target.append($(this.table.groupFooter));
            }
        },
        unexecute: function () {
            this.target.find("table").remove();
            if ($(config.contentPanel.jClass).find("table").length == 0) {
                tableObj = null;
            }
        }
    };

    //#endregion

    //#region Drag one

    eGovCommands.dragOne = function (selected, orgPosition, currentPosition) {
        this.doc = new command(selected);
        this.orgPosition = orgPosition;
        this.currentPosition = currentPosition;
    };

    eGovCommands.dragOne.prototype = {
        execute: function () {
            this.doc.selecteds.css(this.currentPosition);
        },
        unexecute: function () {
            this.doc.selecteds.css(this.orgPosition);
        }
    };

    //#endregion

    //#region Drag multi

    eGovCommands.dragMulti = function (objs, offsetLeft, offsetTop) {
        this.doc = objs;
        this.offsetLeft = offsetLeft;
        this.offsetTop = offsetTop;
    };

    eGovCommands.dragMulti.prototype = {
        execute: function () {
            var $this = this;
            this.doc.forEach(function (itm) {
                var l = itm.originalLeft;
                var t = itm.originalTop;

                $(itm.obj).css('left', l + $this.offsetLeft);
                $(itm.obj).css('top', t + $this.offsetTop);
            });
        },
        unexecute: function () {
            this.doc.forEach(function (itm) {
                var l = itm.originalLeft;
                var t = itm.originalTop;

                $(itm.obj).css('left', l);
                $(itm.obj).css('top', t);
            });
        }
    };

    //#endregion

    //#region Drop

    eGovCommands.drop = function (target, ui) {
        this.target = target;
        this.ui = ui;
        this.top = 0;
        this.left = 0;
        this.text = ui.helper.text();
        this.helperContentParent = ui.helper.parents(config.contentPanel.jClass);
    };

    eGovCommands.drop.prototype = {
        execute: function () {
            if (this.target.parents("table").length > 0) {
                var value;
                if (this.target.parents("table.table-detail").length > 0) // chỉ cho phép kéo trường dữ liệu vào table detail (group, detail). các trường hợp khác sẽ sinh ra text
                {
                    value = "[" + this.ui.helper.attr("value") + "]";
                    this.target
                        .attr("datatype", this.ui.helper.attr("datatype"))
                        .attr("type", "data")
                        .addClass("used-field")
                        .addClass("word-wrap");
                } else {
                    value = $("<div>").addClass(config.toolbox.field_className)
                                     .attr('data-value', this.ui.helper.attr('value'))
                                     .attr('type', 'header')
                                     .text(this.ui.helper.text());
                }
                this.text = value;
                this.target.append(value);
                if (this.helperContentParent.length > 0) {
                    this.ui.helper.remove();
                }
            } else {
                if (this.ui.helper.parents(".special-fields").length > 0) {
                    value = this.ui.helper.attr("value");
                    this.doc = new command(new eReport.field.dataField().init(value, this.ui.helper.attr("datatype")));
                }
                else {
                    this.doc = new command(new eReport.field.text().init(this.ui.helper.text()));
                }
                this.top = this.ui.offset.top - $(this.target).offset().top; // trừ đi top của panel dc drop vô
                this.left = this.ui.offset.left - $(this.target).offset().left; // trừ đi width của toolbox
                this.doc.addCss("top", this.top);
                this.doc.addCss("left", this.left);
                this.doc.selecteds.addClass("used-field");
                this.doc.appendTo(this.target);
            }
        },
        unexecute: function () {
            if (this.helperContentParent.length > 0) {
                this.helperContentParent.append(this.ui.helper);
            }
            if (this.target.parents("table").length > 0) {
                this.target.text(this.target.text().replace(this.text, ""));
            }
            else {
                this.doc.remove();
            }
        }
    };

    //#endregion

    //#region Resize Field

    eGovCommands.resize = function (selected, orgSize, currentSize) {
        this.doc = new command(selected);
        this.orgSize = orgSize;
        this.currentSize = currentSize;
    };

    eGovCommands.resize.prototype = {
        execute: function () {
            this.doc.addCss("width", this.currentSize.width);
            this.doc.addCss("height", this.currentSize.height);
        },
        unexecute: function () {
            this.doc.addCss("width", this.orgSize.width);
            this.doc.addCss("height", this.orgSize.height);
        }
    };

    //#endregion

    //#region Resize Panel

    eGovCommands.resizePanel = function (selected, height) {
        this.doc = new command(selected);
        this.orgHeight = selected.css("height");
        this.height = height;
    };

    eGovCommands.resizePanel.prototype = {
        execute: function () {
            this.doc.addCss("height", this.height);
            var content = $(this.doc.selecteds).find(config.contentPanel.content_class);
            content.height(this.height - 20); // 20 dành cho title
        },
        unexecute: function () {
            this.doc.addCss("height", this.orgHeight);
            var content = $(this.doc.selecteds).find(config.contentPanel.content_class);
            content.height(this.orgHeight - 20); // 20 dành cho title
        }
    };

    //#endregion

    //#region Change text

    eGovCommands.changeText = function (selected, newText) {
        this.doc = new command(selected);
        this.oldText = selected.text();
        this.newText = newText;
    };

    eGovCommands.changeText.prototype = {
        execute: function () {
            this.doc.changeText(this.newText);
        },
        unexecute: function () {
            this.doc.changeText(this.oldText);
        }
    };

    //#endregion

    //#region Change image

    eGovCommands.changeImage = function (selected, newImg) {
        this.target = selected;
        this.oldImg = selected.attr("src");
        this.newImg = newImg;
    };

    eGovCommands.changeImage.prototype = {
        execute: function () {
            var $this = this;
            var FR = new FileReader();
            FR.onload = function (e) {
                $this.target.attr("src", e.target.result);
            };
            FR.readAsDataURL(this.newImg);
        },
        unexecute: function () {
            this.target.attr("src", this.oldImg);
        }
    };

    //#endregion

    //#region Show Hide Group

    eGovCommands.showGroup = function (selected, obj) {
        this.doc = new command(selected);
        this.obj = obj;
    };

    eGovCommands.showGroup.prototype = {
        execute: function () {
            this.doc.show();
        },
        unexecute: function () {
            this.obj.prop("checked", "false");
            this.doc.hide();
        }
    };

    eGovCommands.hideGroup = function (selected, obj) {
        this.doc = new command(selected);
        this.obj = obj;
    };

    eGovCommands.hideGroup.prototype = {
        execute: function () {
            this.doc.hide();
        },
        unexecute: function () {
            this.obj.prop("checked", "true");
            this.doc.show();
        }
    };
    //#endregion

    //#region Copy

    eGovCommands.copy = function (selecteds) {
        this.doc = selecteds;
        this.clone = [];
    };

    eGovCommands.copy.prototype = {
        execute: function () {
            var $this = this;
            this.doc.each(function () {
                var itm = $(this).clone();
                $(this).parent().append(itm);
                $(itm).css("left", parseInt($(this).css("left")) + $(this).width() + 20);
                $(itm).css("top", parseInt($(this).css("top")) + 10);
                if ($(itm).attr("type") === "data") {
                    new eReport.field.dataField().bindEvents(itm);
                }
                else {
                    new eReport.field.text().bindEvents(itm);
                }
                $this.clone.push(itm);
            });
        },
        unexecute: function () {
            this.clone.forEach(function (itm) {
                $(itm).remove();
            });
        }
    };

    //#endregion

    //#region Add Row

    eGovCommands.addRow = function (selected, isAbove) {
        this.target = selected;
        this.doc = selected.clone();
        this.isAbove = isAbove;
    };

    eGovCommands.addRow.prototype = {
        execute: function () {
            if (this.isAbove) {
                this.target.before(this.doc);
            }
            else {
                this.target.after(this.doc);
            }
            this.doc.find("td, th").each(function () {
                $(this).text("");
            });
            this.doc.find(config.contentPanel.cellSelected_class).removeClass(config.contentPanel.cellSelected_className);
            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            this.doc.remove();
        }
    };

    //#endregion

    //#region Remove Row

    eGovCommands.removeRow = function (selected) {
        this.target = selected;
        this.after = selected.after();
        this.parent = selected.parent();
    };

    eGovCommands.removeRow.prototype = {
        execute: function () {
            if (this.parent.find("tr").length > 1) {
                this.target.remove();
            }
            else {
                this.command = new eGovCommands.delete(this.parent);
                this.command.execute();
            }
            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            if (this.parent.find("tr").length > 1) {
                if (this.after == undefined) {

                }
                else {
                    this.after.before(this.target);
                }
            }
            else {
                this.command.unexecute();
            }
            new eReport.field.table().bindTableEvents();
        }
    };

    //#endregion

    //#region Add Col

    eGovCommands.addCol = function (selected, isLeft) {
        this.selected = selected;
        this.colIndex = parseInt($(this.selected).attr("col"));
        this.isLeft = isLeft;
    };

    eGovCommands.addCol.prototype = {
        execute: function () {
            if (isNaN(this.colIndex)) {
                return;
            }
            var colIndex = this.colIndex;
            $(this.selected).parents("table").find("tr").each(function () {
                var col = $(this).find("[col=" + colIndex + "]");
                var itm = $(this.selected).is("td") ? $("<td>") : $("<th>");
                if (col.length == 0) {
                    var idx = colIndex;
                    while (col.length == 0 && idx >= 0) {
                        idx--;
                        col = $(this).find("[col=" + idx + "]");
                    }
                }
                itm.attr("col", colIndex);
                if (this.isLeft) {
                    col.before(itm);
                    col.attr("col", colIndex + 1);
                }
                else {
                    col.after(itm);
                }
                _increaseIdx(col, 1);
                //col.nextAll().each(function () {
                //    $(this).attr("col", parseInt($(this).attr("col")) + 1);
                //});
            });

            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            var idx = this.isLeft ? this.colIndex - 1 : this.colIndex + 1;
            $(this.selected).parents("table").find("tr").each(function () {
                var col = $(this).find("[col=" + idx + "]");
                if (this.isLeft) {
                    col.attr("col", idx + 1);
                }
                _decreaseIdx(col, 1);
                //col.nextAll().each(function () {
                //    $(this).attr("col", parseInt($(this).attr("col")) - 1);
                //});
                col.remove();
            });
            new eReport.field.table().bindTableEvents();
        }
    };

    //#endregion

    //#region Remove Col

    eGovCommands.removeCol = function (selected) {
        this.selected = selected;
        this.colIndex = parseInt($(this.selected).attr("col"));
        this.deleted = [];
    };

    eGovCommands.removeCol.prototype = {
        execute: function () {
            var idx = this.colIndex;
            var $this = this;
            $(this.selected).parents("table").find("tr").each(function () {
                var col = $(this).find("[col=" + idx + "]");
                _decreaseIdx(col, 1);
                //col.nextAll().each(function () {
                //    $(this).attr("col", parseInt($(this).attr("col")) - 1);
                //});
                $this.deleted.push({ col: col, after: col.next(), parent: col.parents("tr") });
                col.remove();
            });

            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            this.deleted.forEach(function (itm) {
                if (itm.after.length > 0) {
                    $(itm.after).before($(itm.col));
                }
                else {
                    $(itm.parent).append(itm.col);
                }
                _increaseIdx($(itm.col), 1);
                //$(itm.col).nextAll().each(function () {
                //    $(this).attr("col", parseInt($(this).attr("col")) + 1);
                //});
            });
            new eReport.field.table().bindTableEvents();
        }
    };

    //#endregion

    //#region Remove Table

    eGovCommands.removeTable = function (selected) {
        this.target = selected;
        this.parent = selected.parent();
    };

    eGovCommands.removeTable.prototype = {
        execute: function () {
            this.target.remove();
            if ($(config.contentPanel.jClass).find("table").length == 0) {
                tableObj = null;
            }
            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            this.parent.append(this.target);
        }
    };

    //#endregion

    //#region Merge Cell

    eGovCommands.mergeCell = function (selected) {
        this.target = selected;
        this.mergeRow = 1;
        this.mergeCol = 1;
    };

    eGovCommands.mergeCell.prototype = {
        execute: function () {
            if (this.target.length == 1) {
                this.target.push(this.target.next()[0]);
            }
            var firstCell = this.target.first();
            var firstIdx = parseInt(firstCell.attr("col"));

            // colspan
            var fCell = firstCell;
            var $this = this;
            this.target.each(function () {
                if (!$(this).is(fCell)) {
                    if (!fCell.next().is($(this))) {
                        fCell = $(this);
                    }
                    else {
                        if ($(this).attr("rowspan") == fCell.attr("rowspan")) {
                            _colspan(fCell, 1);
                            $this.mergeCol++;
                        }
                    }
                }
            });

            // rowspan
            this.target.each(function () {
                if (!$(this).is(firstCell)) {
                    if ($(this).attr("col") == firstIdx && $(this).attr("colspan") == firstCell.attr("colspan")) {
                        _rowspan(firstCell, 1);
                        $this.mergeRow++;
                    }
                }
            });

            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            var $this = this;
            var cell = this.target.first();
            _splitH(cell, this.mergeRow);
            this.target.each(function () {
                if ($(this).attr("col") == cell.attr("col")) {
                    _splitV($(this), $this.mergeCol);
                }
            });
        }
    };

    //#endregion

    //#region Split Cell Vertical

    eGovCommands.splitV = function (selected) {
        this.target = selected;
    };

    eGovCommands.splitV.prototype = {
        execute: function () {
            _splitV(this.target, 2);
            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            _colspan(this.target, 1);
            new eReport.field.table().bindTableEvents();
        }
    };

    //#endregion

    //#region Split Cell Horizontal

    eGovCommands.splitH = function (selected) {
        this.target = selected;
    };

    eGovCommands.splitH.prototype = {
        execute: function () {
            _splitH(this.target, 2);
            new eReport.field.table().bindTableEvents();
        },
        unexecute: function () {
            _rowspan(this.target, 1);
            new eReport.field.table().bindTableEvents();
        }
    };

    //#endregion

    //#region Add Summary

    eGovCommands.addSummary = function (target, value, datatype, field, fieldWith) {
        /// <summary>Chèn summary vào trong báo cáo</summary>
        /// <param name="value" type="String">Giá trị summary</param>
        /// <param name="datatype" type="String">Kiểu dữ liệu</param>
        /// <param name="field" type="String">Trường dữ liệu để tính toán</param>
        /// <param name="fieldWith" type="String">Trường dữ liệu sử dụng cho các công thức như tổng với, hiệu với, tỉ lệ</param>

        this.target = target;
        this.value = value;
        this.field = field;
        this.fieldWith = fieldWith;
        this.datatype = datatype;
    };

    eGovCommands.addSummary.prototype = {
        execute: function () {
            this.target.attr("type", "summary");
            this.target.attr("title", this.value + "(" + this.field + (this.fieldWith == undefined ? "" : "," + this.fieldWith) + ")");
            this.target.attr("datatype", this.datatype);
            this.target.text("[" + this.value + "]");
            this.target.attr("value", this.value);
            this.target.attr("field", this.field);
            this.target.attr("fieldw", this.fieldWith);
            this.target.addClass("used-field");
        },
        unexecute: function () {
            this.target.attr("type", "");
            this.target.attr("title", "");
            this.target.attr("datatype", "");
            this.target.removeClass("used-field");
            this.target.attr("value", "");
            this.target.attr("field", "");
            this.target.attr("fieldw", "");
            this.target.text("");
        }
    };

    //#endregion

    //#region Private Methods

    var _enableEffect = function (itm) {
        if (itm.is("td, th")) {
            new eReport.field.table().bindTableEvents();
        } else {
            if ($(itm).attr("type") === "data") {
                new eReport.field.dataField().bindEvents(itm);
            } else {
                new eReport.field.text().bindEvents(itm);
            }
        }
    };

    var _colspan = function (selected, cols) {
        for (var i = 0; i < cols; i++) {
            var target = selected.next();
            if (target.length == 0) {
                return;
            }
            var width = selected.width() + target.width();
            var colspan = parseInt(selected.attr("colspan"));
            var targetColspan = parseInt(target.attr("colspan"));

            colspan = colspan || 1;
            targetColspan = targetColspan || 1;

            target.remove();
            selected.attr("colspan", colspan + targetColspan);
            selected.width(width);
        }
    };

    var _rowspan = function (selected, rows) {
        for (var i = 0; i < rows; i++) {
            var target = selected.parents("tr").next().find("[col=" + selected.attr("col") + "]");
            if (target.length == 0) {
                return;
            }
            var height = selected.height() + target.height();
            var rowspan = parseInt(selected.attr("rowspan")) || 1;
            var targetRowspan = parseInt(target.attr("rowspan")) || 1;
            target.remove();
            selected.attr("rowspan", rowspan + targetRowspan);
            selected.height(height);
        }
    };

    var _splitH = function (selected, rows) {
        /// <summary>Chia một cell thành nhiều cell theo chiều ngang</summary>
        /// <param name="selected" type="Object">Cell được chọn</param>
        /// <param name="rows" type="int">Số cell sau khi chia</param>
        /// <remarks>
        /// Nguyên tắc chia cell:
        ///     - Nếu CurrentRowspan >= rows: tìm đến các tr tiếp theo tương ứng và chèn td vào vị trí tương ứng.
        ///     - Nếu CurrentRowspan &lt; rows: tính số lượng tr cần thêm (= rows - currentRowspan) và tăng rowspan cho các column bên cạnh.
        /// </remarks>        

        var currentRowspan = parseInt(selected.attr("rowspan"));
        currentRowspan = currentRowspan || 1;
        var rowspan = parseInt(currentRowspan / rows);
        selected.attr("rowspan", rowspan + currentRowspan % rows);
        var cellH = selected.parent().height() / (currentRowspan < rows ? rows : currentRowspan);

        selected.parent().css("height", cellH * (rowspan + currentRowspan % rows));
        selected.css("height", cellH * (rowspan + currentRowspan % rows));

        var trInserted;
        if (currentRowspan >= rows) {
            trInserted = selected.parent("tr");
            var rowspanInserted = rowspan + currentRowspan % rows; // rowspan của cell đang được chọn;
            var idx = selected.attr("col");
            idx = (parseInt(idx) || 1) - 1;
            rows = rows - 1;
            for (var i = 1; i <= currentRowspan; i++) {
                trInserted = trInserted.next();
                if (i == rowspanInserted && rows > 0) {
                    rows--;
                    var col = _getSameCol(trInserted, idx);
                    var cell = $("<td>").text("").height(rowspan * cellH).attr("rowspan", rowspan).attr("colspan", selected.attr("colspan"));
                    if (col.length == 0) {
                        trInserted.prepend(cell);
                    }
                    else {
                        col.after(cell);
                    }
                    rowspanInserted += rowspan;
                }
            }
        }
        else {
            trInserted = $("<tr>").height(cellH);
            trInserted.append($("<td>").text("").height(cellH).attr("rowspan", rowspan));
            selected.parents("tr").after(trInserted);
            selected.parents("tr").find("td, th").each(function () {
                if (!$(this).is(selected)) {
                    var curRowspan = $(this).attr("rowspan");
                    curRowspan = parseInt(curRowspan) || 1;
                    $(this).attr("rowspan", curRowspan + (rows - currentRowspan));
                }
            });
        }
    };

    var _splitV = function (selected, cols) {
        /// <summary>Chia một cell thành nhiều cell theo chiều dọc</summary>
        /// <param name="selected" type="Object">Cell hiện tại</param>
        /// <param name="cols" type="Object">Số cell sau khi chia</param>
        /// <remark>
        /// Các nguyên tắc chia cell:
        ///     - Nếu currentColspan &lt; cols thì mới tăng colspan của các cell cùng column.
        ///     - Colspan của các cell sau khi chia = currentColspan/cols. Riêng cell đầu tiên sẽ cộng thêm currentColspan%cols.
        /// </remark>

        var currentColspan = parseInt(selected.attr("colspan"));
        currentColspan = currentColspan || 1;

        var colspan = parseInt(currentColspan / cols);

        selected.attr("colspan", colspan + currentColspan % cols);

        var cellW = selected.width() / (currentColspan < cols ? cols : currentColspan); //(colspan * cols + currentColspan % cols);

        selected.width(cellW * (colspan + currentColspan % cols));

        for (var i = 1; i < cols; i++) {
            var insert = $("<td>").text("").width((colspan || 1) * cellW);
            insert.attr("colspan", colspan || 1);
            insert.attr("rowspan", selected.attr("rowspan"));
            insert.attr("col", selected.attr("col"));
            selected.after(insert);
        }

        $(selected).parents("table").find("tr").each(function () {
            var idx = parseInt(selected.attr("col"));
            var col = _getSameCol($(this), idx);

            _increaseIdx(col, cols - 1);

            if (currentColspan < cols && !$(col).is(selected)) {
                var colspan1 = parseInt($(col).attr("colspan")) || 1;
                $(col).attr("colspan", colspan1 + (cols - 1));
            }
        });
    };

    var _increaseIdx = function (selectedCell, step) {
        selectedCell.nextAll().each(function () {
            $(this).attr("col", parseInt($(this).attr("col")) + step);
        });
    };

    var _decreaseIdx = function (selectedCell, step) {
        selectedCell.nextAll().each(function () {
            $(this).attr("col", parseInt($(this).attr("col")) - step);
        });
    };

    var _getSameCol = function (tr, idx) {
        /// <summary>Trả về cell có cùng index với cell đang được chọn</summary>
        /// <param name="idx" type="int">Index của cell đang được chọn</param>

        var result = tr.find("[col=" + idx + "]");
        if (result.length == 0) {
            while (idx >= 0 && result.length == 0) {
                result = tr.find("[col=" + --idx + "]");
            }
        }

        return result;
    };

    //#region

    eReport.commands = eGovCommands;

})
(window.eReport = window.eReport || {})