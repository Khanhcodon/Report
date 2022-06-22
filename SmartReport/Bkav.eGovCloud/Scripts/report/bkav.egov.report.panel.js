(function (eR, $) {

    // Lấy config của eReport
    var config = eR.config || {};

    // Lấy các mẫu template của eReport
    var template = eR.template || {};

    var field = eR.field || {};

    var reportPanel = function (panelId, jsonContent, isReport) {
        this.panelId = panelId;
        this.jsonContent = jsonContent;
        this.isReport = isReport;

        // set vị trí con chuột để chèn table, text, image
        this.selectedX = 0;
        this.selectedY = 0;
        this.target;
    };

    reportPanel.prototype.init = function () {
        /// <summary>Khởi tạo form cấu hình báo cáo và các chức năng trên form</summary>

        //var panel = $(this.panelId);
        var panel = $("<div>").addClass("report");
        panel.removeClass("ui-layout-container");
        panel.append(_bindToolbarPanel());
        panel.append(_bindToolboxPanel(this));
        panel.append(_bindReportPanel(this));
        panel.append(_bindOptionPanel());

        panel.appendTo($(this.panelId));

        // set layout for report panel;
        panel.layout(config.layoutSettings);

        // Kích hoạt các sự kiện trên panel
        _enablePanelEvents(this);
    }

    //#region Private Methods

    var _bindToolbarPanel = function () {
        /// <summary>Khởi tạo toolbar</summary>

        var result = template.toolbarPanel;
        return $(result);
    }

    var _bindToolboxPanel = function (report) {
        /// <summary>Khởi tạo toolbox panel bao gồm: data field, special field, group field</summary>

        var jsonContent = report.jsonContent;
        var toolbox = $("<div>").attr("id", config.toolbox.id).addClass(config.toolbox.className);
        toolbox.addClass("ui-layout-west"); // class for layout

        // Data-fields
        var dataFields = [];
        var commonFields = [];
        if (jsonContent.Fields == undefined) {
            dataFields = config.defaultDataFields;
            commonFields = config.defaultCommonFields;
        }
        else {
            _.each(jsonContent.Fields, function (itm) {
                if (itm.Type == 1) {
                    dataFields.push({ datatype: itm.DataType, name: itm.Name, value: itm.Key });
                }
                else if (itm.Type == 2) {
                    commonFields.push({ datatype: itm.DataType, name: itm.Name, value: itm.Key });
                }
            });
        }
        $.template('data_tmpl', template.dataFieldPanel);
        $.tmpl("data_tmpl", { dataFields: dataFields }).appendTo(toolbox);

        // Add common-fields
        $.template('common_tmpl', template.commonFieldPanel);
        $.tmpl("common_tmpl", { commonFields: commonFields }).appendTo(toolbox);

        // Group-fields
        var groupFields = { name: "Group Fields", groupFields: config.defaultGroupFieds };
        $.template('tmp', template.groupFieldPanel);
        $.tmpl("tmp", groupFields).appendTo(toolbox);

        // Control fields
        toolbox.append(template.controlFieldPanel);

        return toolbox;
    };

    var _bindReportPanel = function (report) {
        /// <summary>Khởi tạo khung soạn thảo báo cáo</summary>

        var jsonContent = report.jsonContent;
        var isStatistics = !report.isReport;
        var content = jsonContent.Content;
        if (content == undefined) {
            content = {
                header: $("<div style='height: 25px;'>").addClass(config.contentPanel.content_className)[0].outerHTML,
                footer: $("<div style='height: 25px;'>").addClass(config.contentPanel.content_className)[0].outerHTML,
                detail: $("<div style='height: 25px;'>").addClass(config.contentPanel.content_className)[0].outerHTML,
                gHeader: $("<div style='height: 25px; display: none'>").addClass(config.contentPanel.content_className)[0].outerHTML,
                gFooter: $("<div style='height: 25px; display: none'>").addClass(config.contentPanel.content_className)[0].outerHTML
            };
        }
        $.template('tmp', template.reportPanel);
        var reportWrapper = $.tmpl("tmp", content);
        var pageSettup = jsonContent.pageSettup;
        if (isStatistics) {
            reportWrapper.find(".report-panel").addClass("statistics-size");
        }
        else if (pageSettup != undefined && pageSettup.pageSize != null) {
            reportWrapper.find(".report-panel").addClass(pageSettup.pageSize);
        }
        else {
            reportWrapper.find(".report-panel").addClass("a4-portrait-size");
        }
        return $(reportWrapper);
    };

    var _bindOptionPanel = function () {
        /// <summary>Khởi tạo khung cấu hình tùy chỉnh cho báo cáo.</summary>

        var propertyPanel = $('<div>').addClass("ui-layout-east").addClass("page-setup");
        var tmp = template.optionPanel;
        propertyPanel.append(tmp);
        return propertyPanel;
    };

    var _enablePanelEvents = function (_this) {
        /// <summary>Kích hoạt các sự kiện trên panel</summary>

        // Chuột phải
        _enableContextMenu();

        // Thêm table, text, image vào báo cáo
        _enableInsertToolbox(_this);

        // Cho phép resizable các panel
        _enableResizablePanel();

        // Cho phép kéo thả dữ liệu
        _enableToolboxDraggable();

        // Ẩn hiện danh sách các field
        _showHideToolboxField();

        // Cho phép droppable vào panel soạn thảo báo cáo.
        _enableDroppableReportPanel();

        // Kích hoạt hiệu ứng cho các cell: resizable, droppable, click
        _enableTableEffect();

        // Kích hoạt các hiệu ứng cho text
        _enableTextEffect();

        //// Kích hoạt cho phép thay đổi nội dung bằng cách click đúp
        //_enableChangeContent();

        // Bật tắt lưới
        _turnBackgroundGrid();

        // Ẩn hiện group
        _showHideGroup();

        // Thêm trường dữ liệu
        _addField();

        // Xóa trường dữ liệu
        _deleteField();

        // Setup trang báo cáo
        _changePageSetup();

        /// <summary>Các sự kiện click lên panel: thêm field, kéo chọn, dblclick</summary>
        _clickToPanel(_this);

        _bindTextPropertyEvents();

        _bindTablePropertyEvents();

        _bindLayoutEvents();

        //_bindSelectable(_this);

        // Không cho phép kéo thả trong table
        $("table " + config.toolbox.field_class).css("position", "");

    };

    var _enableContextMenu = function () {
        /// <summary>Chuột phải</summary>

        $.contextMenu({
            selector: config.toolbox.field_class,
            build: function () {
                var options = {
                    items: {
                        "Copy": {
                            name: "Copy",
                            icon: "copy",
                            callback: function () {
                                copied = $(config.contentPanel.textSelected_className);
                            }
                        },
                        "Paste": {
                            name: "Paste",
                            icon: "paste",
                            callback: function () {
                                eR.commandManager.execute(new eR.commands.copy(copied));
                            }
                        },
                        "Delete": {
                            name: "Delete",
                            icon: "delete",
                            callback: function () {
                                eR.commandManager.execute(new eR.commands.delete($(config.contentPanel.textSelected_class)));
                            }
                        },
                        "Properties": {
                            name: "Properties",
                            icon: "properties",
                            callback: function () {
                                _openFormatObject($(this));
                            }
                        }
                    }
                };
                return options;
            }
        });

        $.contextMenu({
            selector: "table td, table th",
            build: function () {
                var options = {
                    items: {
                        "Insert Summary": {
                            name: "Summary",
                            icon: "copy",
                            callback: function () {
                                _openSummary($(this));
                            }
                        },
                        "Properties": {
                            name: "Properties",
                            icon: "properties",
                            callback: function () {
                                _openFormatObject($(this));
                            }
                        }
                    }
                };
                return options;
            }
        });
    }

    var _enableResizablePanel = function () {
        /// <summary>Cho phép resizable các panel</summary>

        $(config.contentPanel.jClass).find(
            config.contentPanel.header_class + ', ' +
            config.contentPanel.footer_class + ', ' +
            config.contentPanel.groupHeader_class + ', ' +
            config.contentPanel.detail_class + ', ' +
            config.contentPanel.groupFooter_class)
            .resizable({
                handles: 's',
                stop: function () {
                    var totalH = $(this).height();
                    eR.commandManager.execute(new eR.commands.resizePanel($(this), totalH));
                    $(this).css("width", "100%");
                }
            });
    }

    var _enableInsertToolbox = function (_this) {
        /// <summary>Insert các toolbox</summary>

        $(config.toolbar.buttons.insertTable).click(function (e) {
            var tableTmp = $("<table>").addClass(config.tmp.table_className);
            tableTmp.append($("<tr>").append("<td>").append("<td>").append($("<td>").text("Esc to cancel")));

            $(config.main_class).append(tableTmp);
            tableTmp.css({ top: e.pageY, left: e.pageX });
            $(config.main_class).mousemove(function (e) {
                tableTmp.css({ top: e.pageY + 2, left: e.pageX + 2 });
            });
        });

        $(config.toolbar.buttons.insertText).click(function (e) {
            var textTmp = $("<span>").addClass(config.tmp.text_className).text("Esc to cancel");
            $(config.main_class).append(textTmp);
            textTmp.css({ top: e.pageY, left: e.pageX });
            $(config.main_class).mousemove(function (e) {
                textTmp.css({ top: e.pageY + 2, left: e.pageX + 2 });
            });
        });

        $(config.toolbar.buttons.insertImg).click(function (e) {
            var imgTmp = $("<span>").addClass(config.tmp.img_className).text("Esc to cancel");
            $(config.main_class).append(imgTmp);
            imgTmp.css({ top: e.pageY, left: e.pageX });
            $(config.main_class).mousemove(function (e) {
                imgTmp.css({ top: e.pageY + 2, left: e.pageX + 2 });
            });
        });
    };

    var _enableToolboxDraggable = function () {
        /// <summary> Cho phép kéo thả dữ liệu </summary>

        // Kích hoạt kéo thả cho các toolbox data field.
        $(config.toolbox.field_class).draggable(config.draggableSettings);
    }

    var _showHideToolboxField = function () {
        /// <summary>Ẩn hiện danh sách các field</summary>

        $(config.toolbox.toolboxFieldHeader_class + " span").click(function () {
            $(this).parent().next("ul").toggle();
            if ($(this).html().indexOf("►") >= 0) {
                $(this).html($(this).html().replace('►', '▼'));
            }
            else {
                $(this).html($(this).html().replace('▼', '►'));
            }
        });
    }

    var _enableDroppableReportPanel = function () {
        /// <summary>Kích hoạt cho phép kéo thả vào panel cấu hình báo cáo</summary>

        // drop config
        var dropSettings = {
            accept: "#" + config.toolbox.id + " " + config.toolbox.field_class + ", table " + config.toolbox.field_class,
            greedy: true, // không bị xung với các droppable khác
            tolerance: "fit",
            drop: function (event, ui) {
                eR.commandManager.execute(new eR.commands.drop($(this), ui));
            }
        };
        $(config.contentPanel.content_class).droppable(dropSettings);
    };

    var _enableTableEffect = function () {
        /// <summary>Kích hoạt hiệu ứng cho các cell: resizable, droppable, click</summary>

        new field.table().bindTableEvents();
    };

    var _enableTextEffect = function () {
        /// <summary>Kích hoạt hiệu ứng cho text</summary>

        $(config.contentPanel.jClass)
            .find(config.toolbox.field_class)
            .each(function () {
                if ($(this).attr("type") === "data") {
                    new field.dataField().bindEvents(this);
                }
                else {

                    new field.text().bindEvents(this);
                }
            });
    };

    var _enableChangeContent = function () {
        /// <summary>Cho phép thay đổi text khi double click</summary>

        $(config.contentPanel.content_class + " " + config.toolbox.field_class + ", table td, table th").dblclick(function () {
            var newtext = prompt("Nhập giá trị mới", $(this).text());
            if (newtext == undefined) {
                return;
            }
            eR.commandManager.execute(new eR.commands.changeText($(this), newtext));
        });
    };

    var _turnBackgroundGrid = function () {
        /// <summary>Hiển thị lưới</summary>

        $(config.optionPanel.turnGridClass).click(function () {
            if ($(this).is(":checked")) {
                $(config.contentPanel.jClass).addClass(config.contentPanel.grid);
            }
            else {
                $(config.contentPanel.jClass).removeClass(config.contentPanel.grid);
            }
        });
    };

    var _showHideGroup = function () {
        /// <summary>Ẩn hiện panel group</summary>

        $(config.contentPanel.showHideGroup).each(function () {
            var panel = $(this).parents(config.contentPanel.groupHeader_class + ", " + config.contentPanel.groupFooter_class);
            var content = panel.find(config.contentPanel.content_class);
            if (content.css("display") == "none") {
                $(this).removeAttr("checked");
            }
            else {
                $(this).attr("checked", "checked");
            }
        });
        $(config.contentPanel.showHideGroup).change(function () {
            var groupPanel;
            if ($(this).attr("name") == "header") {
                groupPanel = $(config.contentPanel.groupHeader_class);
            }
            else {
                groupPanel = $(config.contentPanel.groupFooter_class);
            }
            var contentPanel = groupPanel.find(config.contentPanel.content_class);
            if ($(this).is(":checked")) {
                eR.commandManager.execute(new eR.commands.showGroup(contentPanel, $(this)));
                _enableResizablePanel();
            }
            else {
                groupPanel.height("auto");
                eR.commandManager.execute(new eR.commands.hideGroup(contentPanel, $(this)));
                groupPanel.css("position", "");
            }
        });
    };

    var _addField = function () {
        /// <summary>Thêm các trường dữ liệu</summary>

        // Thêm trường dữ liệu
        $(config.toolboxPanel.addDataField_class).click(function () {
            console.log(1);
            addField(true);
        });

        // Thêm trường đặc biệt
        $(config.toolboxPanel.addSpecialField_class).click(function () {
            addField(false);
        });

        var addField = function (isDataField) {
            $(config.contentPanel.jClass).find(config.toolboxPanel.addField_class).remove();
            $(config.contentPanel.jClass).append(template.addField);
            var addFiedPanel = $(config.contentPanel.jClass).find(config.toolboxPanel.addField_class);
            addFiedPanel.find(":text").first().focus();
            addFiedPanel.draggable();
            addFiedPanel.find("[type='button']").click(function () {
                if ($(this).hasClass("cancel")) {
                    addFiedPanel.remove();
                }
                else {
                    var text = addFiedPanel.find(":text.name").val();
                    var value = addFiedPanel.find(":text.value").val();
                    var datatype = addFiedPanel.find(".datatype").val();
                    if (text == "" || value == "") {
                        alert("Nhập đầy đủ dữ liệu.");
                        return;
                    }
                    var isExist = $(config.toolbox.field_class + "[value='" + value + "']").length > 0;
                    if (isExist) {
                        alert("Trường dữ liệu này đã tồn tại.");
                        return;
                    }
                    if (value.match(/^[\w -]+$/) == null) {
                        alert("Giá trị chỉ được chứa các chữ số, chữ cái, dấu gạch chân, dấu gạch ngang, dấu cách.");
                        return;
                    }
                    var newItem = $("<li>").append(
                            $('<div>').addClass(config.toolbox.field_className).attr('value', value).attr('datatype', datatype).text(text));
                    newItem.append('<input type="button" value="x" title="Xóa trường dữ liệu này" class="delete-field"/>');
                    if (isDataField) {
                        $(config.toolboxPanel.dataField_class).find("ul").last().append(newItem)
                    }
                    else {
                        $(config.toolboxPanel.specialField_class).find("ul").last().append(newItem)
                    }
                    _enableToolboxDraggable();
                    _deleteField();
                }
            });
        };
    };

    var _deleteField = function () {
        /// <summary>Xóa trường dữ liệu bên toolbox</summary>
        $(config.toolbox.deleteField_class).click(function () {
            var field = $(this).parent().find(config.toolbox.field_class);
            if (field.length == 0 || field == undefined) {
                return;
            }

            var fieldValue = field.attr("value");
            var used = $(config.contentPanel.content_class).find(config.toolbox.field_class + ":contains('" + fieldValue + "')");
            if (used.length > 0) {
                alert("Trường " + fieldValue + " đang được sử dụng.");
            }
            else {
                var cfm = confirm("Bạn có chắc muốn xóa trường này?");
                if (cfm) {
                    $(this).parent().remove();
                }
            }
        });
    };

    var _changePageSetup = function () {
        /// <summary>Thiết lập trang báo cáo</summary>

        var changePageSetup = function () {
            var pageSize = $(config.optionPanel.changePageSizeClass).val();
            var pageOrientation = $(config.optionPanel.changePageOrientationClass + ":checked").val();
            var size = pageSize + "-" + pageOrientation + "-size";
            var classSizeToRemove = $(config.contentPanel.jClass)[0].className.match(/[0-9a-z-]+size/);
            if (classSizeToRemove != null) {
                for (var i = 0 ; i < classSizeToRemove.length; i++) {
                    $(config.contentPanel.jClass).removeClass(classSizeToRemove[i]);
                }
            }
            $(config.contentPanel.jClass).addClass(size);
        };

        $(config.optionPanel.changePageSizeClass).change(function () {
            changePageSetup();
        });

        $(config.optionPanel.changePageOrientationClass).click(function () {
            changePageSetup();
        });
    };

    var _clickToPanel = function (_this) {
        /// <summary>Sự kiện click mà không chọn 1 đối tượng nào</summary>
        /// <param name="_this" type="Object">Report object</param>
        var isMouseDown = false;
        $(config.contentPanel.content_class)
        .dblclick(function () {
            alert("dlsa");
        })
        .mousedown(function (e) {
            _unSelectedFields(_this, e);

            if ($(e.target).hasClass(config.toolbox.field_className) || $(e.target).is("td, th") || $(e.target).parents("td, th").length > 0) {
                return;
            }

            isMouseDown = true;

            var targetPanel = $(e.target);
            var tmpDiv = $("<div>")
                            .addClass(config.contentPanel.selectable_className)
                            .css("top", e.pageY - targetPanel.offset().top)
                            .css("left", e.pageX - targetPanel.offset().left);
            targetPanel.append(tmpDiv);
            var x = e.pageX;
            var y = e.pageY;
            var fields = targetPanel.find(config.toolbox.field_class);
            var max = fields.length;
            $(config.contentPanel.content_class).mousemove(function (e) {
                if (!isMouseDown) {
                    return;
                }
                var currentX = e.pageX;
                var currentY = e.pageY;
                if (currentX < x) { // kéo sang trái
                    tmpDiv.css("left", currentX - targetPanel.offset().left);
                }
                if (currentY < y) // kéo lên trên
                {
                    tmpDiv.css("top", currentY - targetPanel.offset().top);
                }
                tmpDiv.css("width", Math.abs(x - e.pageX)).css("height", Math.abs(y - e.pageY));
                var a = { X: tmpDiv.offset().left, Y: tmpDiv.offset().top, Width: tmpDiv.width(), Height: tmpDiv.height() };
                fields.each(function () {
                    var b = { X: $(this).offset().left, Y: $(this).offset().top, Width: $(this).width(), Height: $(this).height() };
                    if (_checkSelected(a, b)) {
                        if (!$(this).hasClass(config.contentPanel.textSelected_className)) {
                            $(this).addClass(config.contentPanel.textSelected_className);
                        }
                    }
                    else {
                        $(this).removeClass(config.contentPanel.textSelected_className);
                    }
                });
            });
        })
        .mouseup(function (e) {
            isMouseDown = false;
            $(config.contentPanel.selectable_class).remove();

            var top = e.pageY - $(this).offset().top;
            var left = e.pageX - $(this).offset().left;
            if ($(config.tmp.text_class).length > 0) {
                eR.commandManager.execute(new eR.commands.addText($(this), "", top, left));
            }
            if ($(config.tmp.table_class).length > 0) {
                eR.commandManager.execute(new eR.commands.addTable($(this)));
            }
            if ($(config.tmp.img_class).length > 0) {
                eR.commandManager.execute(new eR.commands.addImage($(this), top, left));
            }

        });
    };

    var _bindTextPropertyEvents = function () {
        /// <summary>Bind các chức năng trên form properties của text hoặc dataField</summary>
        var target;
        var getSelected = function () {
            target = $(config.contentPanel.textSelected_class + ", " + config.contentPanel.cellSelected_class);
            if (target.length == 0) {
                return;
            };
        };

        // Hiển thị thêm dòng, cột
        $(config.toolbar.properties.items.insertTable_class).hover(function () {
            $(config.toolbar.properties.items.insertTableDetail_class).show();
        }, function () {
            $(config.toolbar.properties.items.insertTableDetail_class).hide();
        });

        // Hiển thị các thao tác xóa dòng, cột, table
        $(config.toolbar.properties.items.deleteTableProp_class).hover(function () {
            $(config.toolbar.properties.items.deleteTableDetail_class).show();
        }, function () {
            $(config.toolbar.properties.items.deleteTableDetail_class).hide();
        });

        // Hiển thị chọn border
        $(config.toolbar.properties.items.borderSelect_class).hover(function () {
            $(config.toolbar.properties.items.borderSelectDetail_class).show();
        }, function () {
            $(config.toolbar.properties.items.borderSelectDetail_class).hide();
        });

        // Hiển thị chọn kiểu viền
        $(config.toolbar.properties.items.borderStyle_class).hover(function () {
            $(config.toolbar.properties.items.borderStyleDetail_class).show();

        }, function () {
            $(config.toolbar.properties.items.borderStyleDetail_class).hide();
        });

        // Hiển thị chọn màu chữ
        $(config.toolbar.properties.items.textColor_class).ColorPicker({
            color: $(config.toolbar.properties.items.textColor_class).attr("value"),
            onChange: function (hsb, hex, rgb) {
                getSelected();
                target.css('color', '#' + hex);
            },
            onHide: function (colpkr) {
                $(config.toolbar.properties.items.textColor_class).removeClass(config.toolbar.properties.selected_className);
            }
        });

        // Hiển thị chọn màu nền
        $(config.toolbar.properties.items.backColor_class).ColorPicker({
            color: $(config.toolbar.properties.items.backColor_class).attr("value"),
            onChange: function (hsb, hex, rgb) {
                getSelected();
                target.css('backgroundColor', '#' + hex);
            },
            onHide: function (colpkr) {
                $(config.toolbar.properties.items.backColor_class).removeClass(config.toolbar.properties.selected_className);
            }
        });

        // Hiển thị chọn màu viền
        $(config.toolbar.properties.items.borderColor_class).ColorPicker({
            color: $(config.toolbar.properties.items.borderColor_class).attr("value"),
            onChange: function (hsb, hex, rgb) {
                getSelected();
                target.css('borderColor', '#' + hex);
            },
            onHide: function (colpkr) {
                $(config.toolbar.properties.items.borderColor_class).removeClass(config.toolbar.properties.selected_className);
            }
        });

        // Select change
        $(config.toolbar.properties.jClass).find("select").change(function () {
            getSelected();
            var prop = $(this).attr("prop");
            var value = $(this).val();
            eR.commandManager.execute(new eR.commands.addCss(target, prop, value));
        });

        $(config.toolbar.properties.jClass).find("a, span").click(function () {
            getSelected();

            if ($(this).is("#CustomCss")) {
                _openCustomCss(target);
                return;
            }

            // Lấy tên property - xem attribute trong template
            var prop = $(this).attr("prop");
            if (prop == undefined) {
                return;
            }
            var value = '';
            var selected = config.toolbar.properties.selected_className;

            // Nếu property chưa được chọn
            if (!$(this).hasClass(selected)) {

                // Unselected những giá trị có cùng prop: ví dụ text-align
                $(this).parent().find("[prop='" + prop + "']").removeClass(selected);
                // Set selected cho property
                $(this).addClass(selected);
                value = $(this).attr("value");
            }
            else {
                $(this).removeClass(selected);
            }

            // Add hoặc remove property được chọn
            if (value != undefined) {
                eR.commandManager.execute(new eR.commands.addCss(target, prop, value));
            }
        });
    };

    var _bindTablePropertyEvents = function () {
        /// <summary>Bind các chức năng trên form properties của cell</summary>

        var selected;
        var getSelected = function () {
            selected = $(config.contentPanel.textSelected_class + ", " + config.contentPanel.cellSelected_class);
            if (selected.length == 0) {
                return;
            };
        };

        // Thêm dòng mới
        $(config.toolbar.properties.items.insertAbove_class + "," + config.toolbar.properties.items.insertBelow_class)
        .click(function () {
            getSelected();
            var trSelected = selected.parents("tr");
            var isAbove = $(this).hasClass(config.toolbar.properties.items.insertAbove_className);
            eR.commandManager.execute(new eR.commands.addRow(trSelected, isAbove));
        });

        // Thêm cột mới
        $(config.toolbar.properties.items.insertLeft_class + ", " + config.toolbar.properties.items.insertRight_class)
        .click(function () {
            getSelected();
            var isLeft = $(this).hasClass(config.toolbar.properties.items.insertLeft_className);
            eR.commandManager.execute(new eR.commands.addCol(selected));
        });

        // Xóa dòng được chọn
        $(config.toolbar.properties.items.deleteRow_class).click(function () {
            getSelected();
            eR.commandManager.execute(new eR.commands.removeRow(selected.parents("tr")));
        });

        // Xóa cột được chọn
        $(config.toolbar.properties.items.deleteColumn_class).click(function () {
            getSelected();
            eR.commandManager.execute(new eR.commands.removeCol(selected));
        });

        // Xóa table
        $(config.toolbar.properties.items.deleteTable_class).click(function () {
            getSelected();
            eR.commandManager.execute(new eR.commands.removeTable(selected.parents("table")));
        });

        // Split Cell Vertical
        $(config.toolbar.properties.items.splitCellV_class).click(function () {
            getSelected();
            eR.commandManager.execute(new eR.commands.splitV(selected));
        });

        // Split Cell Horizontal
        $(config.toolbar.properties.items.splitCellH_class).click(function () {
            getSelected();
            eR.commandManager.execute(new eR.commands.splitH(selected));
        });

        // Merge Cell
        $(config.toolbar.properties.items.mergeCell_class).click(function () {
            getSelected();
            eR.commandManager.execute(new eR.commands.mergeCell(selected));
        });
    };

    var _bindLayoutEvents = function () {
        /// <summary>Kích hoạt các chức năng layout trên toolbar</summary>

        $(config.toolbar.layout.jClass).find("a").click(function () {
            var clickedLayout = $(this);
            var selecteds = $(config.contentPanel.textSelected_class);
            if (selecteds.length < 2) {
                return;
            }

            var lastSelected = $(config.contentPanel.lastSelected_class);
            if (lastSelected.length == 0) {
                lastSelected = selecteds.first();
            }
            // Căn trái
            if (clickedLayout.is(config.toolbar.layout.items.alignLefts_class)) {
                var targetLeft = lastSelected.position().left;
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "left", targetLeft));
            }

            // Căn phải
            if (clickedLayout.is(config.toolbar.layout.items.alignRights_class)) {
                var targetRight = lastSelected.position().left + lastSelected.width();
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "left", targetRight));
            }

            // Căn trên
            if (clickedLayout.is(config.toolbar.layout.items.alignTops_class)) {
                var targetTop = lastSelected.position().top;
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "top", targetTop));
            }

            // Căn dưới
            if (clickedLayout.is(config.toolbar.layout.items.alignBottoms_class)) {
                var targetBottom = lastSelected.position().top + lastSelected.height();
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "top", targetBottom - $(this).height()));
            }

            // Căn giữa theo chiều dọc
            if (clickedLayout.is(config.toolbar.layout.items.alignCenters_class)) {
                var targetCenter = lastSelected.position().left + lastSelected.width() / 2;
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "left", targetCenter - $(this).width() / 2));
            }

            // Căn giữa theo chiều ngang
            if (clickedLayout.is(config.toolbar.layout.items.alignMiddles_class)) {
                var targetMiddle = lastSelected.position().top + lastSelected.height() / 2;
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "top", targetMiddle - $(this).height() / 2));
            }

            // Cùng độ dài
            if (clickedLayout.is(config.toolbar.layout.items.sameWidth_class)) {
                var targetW = lastSelected.width();
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "width", targetW));
            }

            // Cùng chiều cao
            if (clickedLayout.is(config.toolbar.layout.items.sameHeight_class)) {
                var targetH = lastSelected.height();
                eR.commandManager.execute(new eR.commands.addCss(selecteds, "height", targetH));
            }

            // Cùng kích thước
            if (clickedLayout.is(config.toolbar.layout.items.sameSize_class)) {
                var targetW = lastSelected.width();
                var targetH = lastSelected.height();
                eR.commandManager.execute(new eR.commands.sameSize(selecteds, targetW, targetH));
            }
        });
    };

    /// <summary> Kéo chuột chọn các trường dữ liệu</summary>
    /// <param name="_this" type="Object">Report object</param>
    var _bindSelectable = function (_this) {
        var isMouseDown = false;
        $(config.contentPanel.content_class).mousedown(function (e) {
            if ($(e.target).hasClass(config.toolbox.field_className) || $(e.target).is("td, th") || $(e.target).parents("td, th").length > 0) {
                return;
            }
            isMouseDown = true;
            var targetPanel = $(_this.target);
            var tmpDiv = $("<div>")
                            .addClass(config.contentPanel.selectable_className)
                            .css("top", e.pageY - targetPanel.offset().top)
                            .css("left", e.pageX - targetPanel.offset().left);
            targetPanel.append(tmpDiv);
            var x = e.pageX;
            var y = e.pageY;
            var fields = targetPanel.find(config.toolbox.field_class);
            var max = fields.length;
            $(config.contentPanel.content_class).mousemove(function (e) {
                if (!isMouseDown) {
                    return;
                }
                var currentX = e.pageX;
                var currentY = e.pageY;
                if (currentX < x) { // kéo sang trái
                    tmpDiv.css("left", currentX - targetPanel.offset().left);
                }
                if (currentY < y) // kéo lên trên
                {
                    tmpDiv.css("top", currentY - targetPanel.offset().top);
                }
                tmpDiv.css("width", Math.abs(x - e.pageX)).css("height", Math.abs(y - e.pageY));
                var a = { X: tmpDiv.offset().left, Y: tmpDiv.offset().top, Width: tmpDiv.width(), Height: tmpDiv.height() };
                fields.each(function () {
                    var b = { X: $(this).offset().left, Y: $(this).offset().top, Width: $(this).width(), Height: $(this).height() };
                    if (_checkSelected(a, b)) {
                        if (!$(this).hasClass(config.contentPanel.textSelected_className)) {
                            $(this).addClass(config.contentPanel.textSelected_className);
                        }
                    }
                    else {
                        $(this).removeClass(config.contentPanel.textSelected_className);
                    }
                });
            });
        })
        .mouseup(function (e) {
            isMouseDown = false;
            $(config.contentPanel.selectable_class).remove();
        })
        .dblclick(function () {
            alert("a");
        });
    };

    var _checkSelected = function (a, b) {
        /// <summary>Kiểm tra trường dữ liệu nằm trong vùng kéo chuột</summary>
        /// <param name="a" type="Object">Tọa độ top, left, width, height của vùng kéo chuột</param>
        /// <param name="b" type="Object">Tọa độ top, left, width, height của trường dữ liệu cần kiểm tra</param>

        if (a.X > (b.X + b.Width) || (a.X + a.Width) < b.X || a.Y > (b.Y + b.Height) || (a.Y + a.Height) < b.Y) {
            return false;
        }
        return true;
    }

    var _isSameColumn = function (obj, target) {
        /// <summary>Kiểm tra xem 2 cell có cùng một column trong table ko</summary>

        var objCellCount = $(obj).parents("tr").find("td, th").length;
        var targetCellCount = $(target).parents("tr").find("td, th").length;
        var cellDiff = objCellCount - targetCellCount; // để tính các trường hợp các cell trước bị merge
        if ($(obj).index() + targetCellCount == $(target).index() + objCellCount) {
            return true;
        }
        return false;
    }

    var _getSameColumns = function (obj) {
        /// <summary>Trả về danh sách các cell cùng cột với cell hiện tại</summary>

        var result = [];
        var rows = $(obj).parents("table").find("tr");
        rows.each(function () {
            var cells = $(this).find("td, th");
            cells.each(function () {
                if (_isSameColumn(obj, this) && !$(this).is(obj)) {
                    result.push(this);
                }
            });
        });
        return result;
    }

    var _openFormatObject = function (obj) {
        /// <summary>Mở form format kiểu dữ liệu cho field được chọn</summary>
        /// <param name="obj" type="Object">Trường dữ liệu được chọn</param>

        var selected = $(config.contentPanel.textSelected_class + ", " + config.contentPanel.cellSelected_class);
        if (selected.length != 1) {
            // Todo: thêm cho phép cấu hình cho nhiều field (Field cùng kiểu dữ liệu)
            return;
        }
        console.log('fuck');
        debugger
        $.template("tmp", template.properties);
        $(config.main_class).find(config.toolboxPanel.formatObject_class).remove();
        $.tmpl("tmp", { datatype: obj.attr("datatype") }).appendTo($(config.main_class));
        var formatObject = $(config.main_class).find(config.toolboxPanel.formatObject_class);
        formatObject.draggable({ containment: "parent" });
        formatObject.find("li").click(function () {
            $(config.toolboxPanel.formatSelected_class).removeClass("selected");
            $(this).addClass("selected");
            if (!formatObject.find(":checkbox").is(":checked")) {
                formatObject.find(".preview p").text($(this).attr("sample"));
            }
        });
        formatObject.find(":checkbox").click(function () {
            if ($(this).is(":checked")) {
                formatObject.find(".preview p").text($(this).attr("sample"));
            }
            else {
                formatObject.find(".preview p").text(($(config.toolboxPanel.formatSelected_class).attr("sample")));
            }
        });
        formatObject.find("[type='button']").click(function () {
            if (!$(this).hasClass("cancel")) {
                var format = "";
                if (formatObject.find(":checkbox").is(":checked")) {
                    format = formatObject.find(":checkbox").val();
                }
                else {
                    format = $(config.toolboxPanel.formatSelected_class).attr("val");
                }
                selected.attr("format", format);
            }
            formatObject.remove();
        });
    }

    var _openSummary = function (target) {
        $(config.main_class).find(config.toolboxPanel.summary_class).remove();
        $(template.summary).appendTo($(config.main_class));
        var summaryObject = $(config.main_class).find(config.toolboxPanel.summary_class);
        summaryObject.draggable({ containment: "parent" });
        _bindSummaryEvent(summaryObject);
        summaryObject.find("[type='button']").click(function () {
            if (!$(this).hasClass("cancel")) {
                var value = $(config.toolboxPanel.summarySelectSummary_class).val();
                var field = $(config.toolboxPanel.summarySelectField_class).val();
                var fieldWith = $(config.toolboxPanel.summarySelectFieldWith_class).val();
                var datatype = $(config.toolboxPanel.summarySelectField_class).find(":selected").attr("datatype");
                eR.commandManager.execute(new eR.commands.addSummary(target, value, datatype, field, fieldWith));
            }
            summaryObject.remove();
        });
    };

    var _bindSummaryEvent = function (summaryObject) {
        var fields = [];
        $(config.toolbox.dataField_class + " " + config.toolbox.field_class).each(function () {
            var itm = { text: $(this).text(), value: $(this).attr("value"), datatype: $(this).attr("datatype") };
            fields.push(itm);
        });
        fields.forEach(function (itm) {
            var option = $("<option>").text(itm.text).attr("value", itm.value).attr("datatype", itm.datatype);
            $(config.toolboxPanel.summarySelectField_class).append(option);
            if (itm.datatype === "number") {
                $(config.toolboxPanel.summarySelectFieldWith_class).append(option);
            }
        });

        $(config.toolboxPanel.summarySelectField_class).change(function () {
            if ($(this).find(":selected").attr("datatype") == "string" || $(this).find(":selected").attr("datatype") == "datetime") {
                $(config.toolboxPanel.summarySelectSummary_class).find("option[for!='" + $(this).find(":selected").attr("datatype") + "']").hide();
            }
            else {
                $(config.toolboxPanel.summarySelectSummary_class).find("option[for!='" + $(this).find(":selected").attr("datatype") + "']").show();
            }
        });

        $(config.toolboxPanel.summarySelectSummary_class).change(function () {
            if ($(this).val() == "TotalWith" || $(this).val() == "SubWith" || $(this).val() == "PercenWith") {
                $(config.toolboxPanel.summarySelectFieldWith_class).show();
            }
            else {
                $(config.toolboxPanel.summarySelectFieldWith_class).hide();
            }
        });
    };

    var _unSelectedFields = function (_this, e) {
        _this.selectedX = e.pageX;
        _this.selectedY = e.pageY;
        _this.target = e.target;

        if (!$(e.target).hasClass(config.toolbox.field_className)
            && !$(e.target).is("th, td")) {

            // Bỏ các textField đang được selected
            $(config.contentPanel.textSelected_class).removeClass(config.contentPanel.textSelected_className);

            // Bỏ các cell đang được selected
            $(config.contentPanel.cellSelected_class).removeClass(config.contentPanel.cellSelected_className);

            // Bỏ last Selected
            $(config.contentPanel.lastSelected_class).removeClass(config.contentPanel.lastSelected_className);
        }
    }

    var _openCustomCss = function (obj) {
        if (obj.length === 0) {
            return;
        }
        $.template("tmp", template.customCss);
        $(config.main_class).find(config.toolboxPanel.customCss_class).remove();
        $.tmpl("tmp", { datatype: obj.attr("datatype") }).appendTo($(config.main_class));
        var customObj = $(config.main_class).find(config.toolboxPanel.customCss_class);
        customObj.draggable({ containment: "parent" });
        customObj.find("[type='button']").click(function () {
            if (!$(this).hasClass("cancel")) {
                var prop = customObj.find("select").val();
                var value = customObj.find("textarea").val();
                obj.attr("style", obj.attr("style") + " " + prop + ": " + value + ";");
            }
            customObj.remove();
        });
    };

    //#endregion

    // Gán vào eReport
    eR.panel = reportPanel;

})(eReport || {}, jQuery)
