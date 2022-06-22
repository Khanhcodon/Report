
(function (eR) {

    // Lấy config của eReport
    var config = eR.config || {};
    // Lấy các mẫu template của eReport
    var template = eR.template || {};

    // Contructor
    var field = function () {
        this.text = textField;
        this.dataField = dataField;
        this.table = dataTable;
        this.image = imageField;
    };

    //#region Text field

    /// <summary>Tạo và xử lý đối tượng text field</summary>
    var textField = function text() {

        /// <summary>Show properties của đối tượng dc chọn</summary>
        /// <param name="selected" type="Dom">Đối tượng đang được chọn</param>
        this.loadProperties = function(selected) {
            _showProperties(selected);
        };
    };

    /// <summary>Khởi tạo một đối tượng text field</summary>
    textField.prototype.init = function (text) {
        if (text == undefined) {
            text = prompt("Nhập giá trị", "");
        }
        if (text == undefined) {
            return null;
        }
        var result = _textEvents.createField(text, this);
        result.attr("type", "text");
        return result;
    };

    textField.prototype.bindEvents = function (obj) {
        var result = $(obj);
        _bindTextEvents(result, this);
    };

    //#endregion

    //#region Data field

    /// <summary>Tạo và xử lý đối tượng data field</summary>
    var dataField = function data() {

        /// <summary>Show properties của đối tượng dc chọn</summary>
        /// <param name="obj" type="Dom">Đối tượng đang được chọn</param>
        this.loadProperties = function(obj) {
            // Cho phép kéo thả text
            $(obj).draggable({ containment: "parent" });

            _showProperties(obj);

            // Gán selected cho field được chọn
            $(obj).addClass(config.contentPanel.textSelected_className);
        };
    };

    /// <summary>Các hàm xử lý data field</summary>
    dataField.prototype = {

        /// <summary>Khởi tạo một đối tượng data field</summary>
        /// <param name="value" type="String">Value của field được kéo vào panel</param>
        init: function (value, datatype) {
            value = '[' + value + ']';
            var result = _textEvents.createField(value, this);
            result.attr("datatype", datatype);
            result.attr("type", "data");
            result.attr("val", value);
            return result;
        },

        bindEvents: function (obj) {
            var result = $(obj);
            _bindTextEvents(result, this);
        }
    };

    //#endregion

    //#region Image field

    /// <summary>Tạo và xử lý đối tượng image field</summary>
    var imageField = function image() {
        
    };

    /// <summary>Khởi tạo một đối tượng image field</summary>
    imageField.prototype.init = function () {
        var result = $("<div>").addClass(config.toolbox.field_className);
        result.append($("<img>").addClass("img-class").addClass("no-image").attr("src", config.tmp.img_noImage_src));
        result.append($("<input type='file'/>").hide());
        result.attr("type", "image");
        _bindTextEvents(result);
        return result;
    };

    //#endregion

    //#region Control Field

    var controlField = function () {

    };

    controlField.prototype.init = function () {

    };

    //#endregion

    //#region Table field

    /// <summary>Tạo và xử lý đối tượng table của báo cáo</summary>
    var dataTable = function () {
        this.header = "";
        this.detail = "";
        this.footer = "";
        this.groupHeader = "";
        this.groupFooter = "";
    };

    /// <summary>Các hàm xử lý đối tượng table</summary>
    dataTable.prototype = {

        /// <summary>Khởi tạo đối tượng table</summary>
        init: function () {
            var cols = parseInt(prompt("Nhập số cột", 1));
            if (isNaN(cols) || cols == undefined) {
                return;
            }

            this.cols = cols;
            this.header = this.createHeader();
            this.footer = this.createFooter();
            this.groupHeader = this.createGroupHeader();
            this.groupFooter = this.createGroupFooter();
            this.detail = this.createDetail();

            return this;
        },

        createHeader: function () {
            var result = _tableEvents.createTableHeader(this.cols);
            return result;
        },

        createFooter: function () {
            var result = _tableEvents.createTableFooter(this.cols);
            return result;
        },

        createGroupHeader: function () {
            return _tableEvents.createGroupHeader(this.cols);
        },

        createGroupFooter: function () {
            return _tableEvents.createGroupFooter(this.cols);
        },

        createDetail: function () {
            var result = _tableEvents.createTableDetail(this.cols);
            return result;
        },

        bindTableEvents: function () {
            _tableEvents.bindTableEvents();
        }
    };

    //#endregion

    //#region Private Methods

    /// <summary>Các sự kiện trên text;</summary>
    var _textEvents = {

        /// <summary>Tạo html field</summary>
        createField: function (text, fieldObj) {
            var result = $("<div>")
                            .addClass(config.toolbox.field_className)
                            .css("font-size", "12pt")
                            .attr("datatype", "string")
                            .attr("value", text);
            result.text(text);

            _bindTextEvents(result, fieldObj);

            result.css("min-width", "20px");

            return result;
        }
    };

    /// <summary>Các sự kiện trên table</summary>
    var _tableEvents = {

        /// <summary>Tạo table header cho báo cáo</summary>
        /// <param name="cols" type="Integer">Số cột của table</param>
        createTableHeader: function (cols) {
            $.template("tbl_header", template.tableHeader);
            var header = $.tmpl("tbl_header", { cols: new Array(cols) });

            //// Cho phép thay đổi vị trí table
            $(header).draggable({
                axis: "x",
                containment: "parent",
                drag: function (event, ui) {
                    var position = ui.position;
                    var tableDetail = $(config.tableDetailClass);
                    $(tableDetail).css("left", position.left);
                    var tableFooter = $(config.tableFooterClass);
                    $(tableFooter).css("left", position.left);
                },
                stop: function (event, ui) {
                    eR.commandManager.execute(new eR.commands.dragOne($(this), ui.originalPosition, $(this).position()));
                }
            });

            $(header).css("position", "absolute");

            if ($(config.contentPanel.jClass).hasClass("statistics-size")) {
                var cells = $(header).find("th, td");
                cells.width($(config.contentPanel.jClass).width() / (cells.length));
                $(header).width("100%");
            }

            var idx = 0;
            header.find("th").each(function () {
                $(this).attr("col", idx++);
            });

            _enableTableEffect(header);

            return header;
        },

        /// <summary>Tạo table detail cho báo cáo</summary>
        /// <param name="cols" type="Integer">Số cột của table</param>
        createTableDetail: function (cols) {
            return createDetail("detail", cols);
        },

        createGroupHeader: function (cols) {
            return createDetail("group-header", cols);
        },

        createGroupFooter: function (cols) {
            return createDetail("group-footer", cols);
        },

        /// <summary>Tạo table footer</summary>
        /// <param name="cols" type="Integer">Số cột</param>
        createTableFooter: function (cols) {
            $.template("tmp", template.tableFooter);
            var footer = $.tmpl("tmp", { className: "footer", cols: new Array(cols) });

            // Cho phép thay đổi vị trí table
            $(footer).draggable({
                axis: "x",
                containment: "parent"
            });

            $(footer).css("position", "absolute");
            // Cho phép droppable cho các cột của bảng
            $(footer).find("th").droppable({
                greedy: true, // không bị xung với các droppable khác
                accept: "#" + config.toolbox.id + " " + config.toolbox.field_class,
                drop: function (event, ui) {
                    if ($(ui.draggable).attr("datatype") != "table") {
                        var itm;
                        if ($(ui.draggable).attr("datatype") == "text") {
                            itm = new textField().init();
                        }
                        else {
                            itm = new dataField().init(ui.helper.attr("value"));
                        }
                        itm.css("top", "0").css("left", "0");
                        $(this).append(itm);
                    }
                }
            });

            if ($(config.contentPanel.jClass).hasClass("statistics-size")) {
                var cells = $(footer).find("th, td");
                cells.width($(config.contentPanel.jClass).width() / (cells.length));
                $(footer).width("100%");
            }

            var idx = 0;
            footer.find("th").each(function () {
                $(this).attr("col", idx++);
            });

            _enableTableEffect(footer);

            return footer;
        },

        bindTableEvents: function () {

            _enableTableEffect($("table"));

            // Cho phép thay đổi vị trí table
            $("table").draggable({
                axis: "x",
                containment: "parent",
                stop: function () {
                    $(this).css("top", "");
                }
            });
        }
    };

    var createDetail = function (className, cols) {
        $.template("tmp", template.tableDetail);
        var result = $.tmpl("tmp", { className: className, cols: new Array(cols) });

        // Cho phép thay đổi vị trí table
        $(result).draggable({
            axis: "x",
            containment: "parent",
            stop: function (event, ui) {
                eR.commandManager.execute(new eR.commands.dragOne($(this), ui.originalPosition, $(this).position()));
            }
        });

        if ($(config.contentPanel.jClass).hasClass("statistics-size")) {
            var cells = $(result).find("th, td");
            cells.width($(config.contentPanel.jClass).width() / (cells.length));
            $(result).width("100%");
        }

        var idx = 0;
        result.find("td").each(function () {
            $(this).attr("col", idx++);
        });

        _enableTableEffect(result);
        return result;
    };

    var _bindTextEvents = function (obj, fieldObj) {
        var selectedObjs = [];
        var _moveSelected = function (ol, ot) {
            if (selectedObjs.length > 1) {
                selectedObjs.forEach(function($this) {
                    if (!$($this.obj).is(obj)) {
                        var l = $this.originalLeft;
                        var t = $this.originalTop;

                        $($this.obj).css('left', l + ol);
                        $($this.obj).css('top', t + ot);
                    }
                });
            }
        };

        // Cho phép kéo thả text
        obj.draggable("destroy").draggable({
            //containment: "parent",
            cancel: "td .report-toolbox-field, th .report-toolbox-field",
            start: function () {
                $(config.contentPanel.textSelected_class).each(function () {
                    var selected = {};
                    selected.obj = this;
                    selected.originalTop = this.offsetTop;
                    selected.originalLeft = this.offsetLeft;
                    selectedObjs.push(selected);
                });
            },
            drag: function (event, ui) {
                var currentLoc = $(this).position();
                var orig = ui.originalPosition;

                var offsetLeft = currentLoc.left - orig.left;
                var offsetTop = currentLoc.top - orig.top;

                _moveSelected(offsetLeft, offsetTop);
            },
            stop: function (event, ui) {
                var currentLoc = $(this).position();
                var orig = ui.originalPosition;

                var offsetLeft = currentLoc.left - orig.left;
                var offsetTop = currentLoc.top - orig.top;
                eR.commandManager.execute(new eR.commands.dragMulti(selectedObjs, offsetLeft, offsetTop));
                selectedObjs = [];
            }
        });

        // Cho phép resize text 
        obj.resizable("destroy").resizable({
            containment: "parent",
            handles: 'e,s',
            stop: function (event, ui) {
                eR.commandManager.execute(new eR.commands.resize(obj, ui.originalSize, ui.size));
            }
        });

        obj.css("position", "absolute"); // sử dụng cho các chức năng layout, do mặc định thư viện jqueryui sinh position relative

        obj.dblclick(function () {
            if (obj.attr("type") === "text")
            {
                var newtext = prompt("Nhập giá trị mới", $(this).text());
                if (newtext == undefined) {
                    return;
                }
                eR.commandManager.execute(new eR.commands.changeText($(this), newtext));
            }
            else
            {
                obj.find(":file").click();
                obj.find(":file").change(function () {
                    if (this.files && this.files[0]) {
                        eR.commandManager.execute(new eR.commands.changeImage($(obj.find("img")), this.files[0]));
                    }
                });
            }
        });

        // Bật properies khi chọn text
        obj.mouseup(function (e) {
            debugger
            // Un-selected các selected khác
            if (!e.ctrlKey) {
                $(config.contentPanel.textSelected_class).removeClass(config.contentPanel.textSelected_className);
            }

            // Gán selected cho field được chọn
            $(this).addClass(config.contentPanel.textSelected_className);

            // Remove last selected trước
            $(config.contentPanel.lastSelected_class).removeClass(config.contentPanel.lastSelected_className);
            if ($(config.contentPanel.textSelected_class).length > 1) {
                // Nếu có nhiều hơn 1 trường được selected thì gán cái được chọn cuối cùng
                // Gán last selected cho field được chọn
                $(this).addClass(config.contentPanel.lastSelected_className); // sử dụng cho các chức năng layout
            }

            if (fieldObj != undefined)
            {
                // Load properties của field được chọn
                fieldObj.loadProperties(this);
            }
        }).mousedown(function () {
            // Gán selected cho field được chọn
            $(this).addClass(config.contentPanel.textSelected_className);
        });

    };

    // Kích hoạt hiệu ứng cho các cell: resizable, droppable, click
    var _enableTableEffect = function($this) {
        var orW;
        debugger
        $($this).find("th, td")

            // Hiển thị properties khi click chọn
            .mousedown(function(e) {
                if (!e.ctrlKey) {
                    // Un-selected các selected khác
                    $(config.contentPanel.cellSelected_class).removeClass(config.contentPanel.cellSelected_className);
                }

                var selected = this;
                // Gán selected cho field được chọn
                $(selected).addClass(config.contentPanel.cellSelected_className);

                // Load properties của field được chọn
                _showProperties(selected);
            })

            // kích hoạt chức năng resizable cho các td, th
            .resizable("destroy")
            .resizable({
                containment: "parent",
                handles: 'e, s, se',
                start: function() {
                    var nextObj = $(this).next();
                    orW = nextObj.width();
                },
                resize: function(event, ui) {
                    if ($(config.contentPanel.jClass).hasClass("statistics-size")) {
                        var nextObj = $(this).next();
                        var off = ui.originalSize.width - ui.helper.width();
                        nextObj.width(orW + off);
                    }
                },
                stop: function(event, ui) {
                    eR.commandManager.execute(new eR.commands.resize($(this), ui.originalSize, ui.size));
                }
            })

            // kích hoạt chức năng droppable cho các td, th
            .droppable({
                greedy: true, // không bị xung với các droppable khác
                accept: config.toolbox.field_class,
                drop: function(event, ui) {
                    if (ui.helper.attr("type") == "link") {
                        _addControl($(this));
                    } else {
                        eR.commandManager.execute(new eR.commands.drop($(this), ui));
                    }
                }
            });
        if ($(config.contentPanel.jClass).hasClass("statistics-size")) {
            $($this).find("th, td").last().resizable("destroy");
        }
    };

    /// <summary>Hiển thị form chỉnh sửa properties</summary>
    var _showProperties = function(obj) {

        var setProperties = function(prop1) {
            var value1 = $(obj).css(prop1);
            var property1 = $(config.toolbar.properties.jClass).find("[prop='" + prop1 + "']");
            if (property1.is("select")) {
                var itm = property1.find("option[value~='" + value1 + "']");
                if (itm.length != 0) {
                    itm.attr("selected", "selected");
                } else {
                    property1.find("option").first().attr("selected", "selected");
                }
            } else if (property1.is("span")) {
                if (property1.attr("value").indexOf(value1) > 0) {
                    property1.addClass(config.toolbar.properties.selected_className);
                } else {
                    property1.removeClass(config.toolbar.properties.selected_className);
                }
            } else if (property1.is("a")) {
                property1.each(function () {
                    if ($(this).attr("value") == value1) {
                        $(this).addClass(config.toolbar.properties.selected_className);
                    } else {
                        $(this).removeClass(config.toolbar.properties.selected_className);
                    }
                });
            }
        };

        var properties = ["font-family", "font-size", "border-bottom-style", "border-left-style", "border-right-style", "font-weight", "font-style", "text-decoration", "text-align"];
        properties.forEach(function(prop1) {
            setProperties(prop1);
        });

        // Color
        var prop = "color";
        var value = $(obj).css(prop);
        var property = $(config.toolbar.properties.jClass).find("[prop='" + prop + "']");
        property.attr("value", value);

        // background-color
        prop = "background-color";
        value = $(obj).css(prop);
        property = $(config.toolbar.properties.jClass).find("[prop='" + prop + "']");
        property.attr("value", value);
    };

    var _addControl = function($this) {
        var addControl = $(template.addControl);
        addControl.draggable();
        addControl.css("position", "");
        addControl.find("select.select-action").change(function() {
            if ($(this).val() === "sort") {
                _showSortValue();
            } else {
                $(".sort-value").hide();
            }
        });
        addControl.find("[type='button']").click(function () {
            debugger
            if ($(this).hasClass("cancel")) {
                addControl.remove();
                return "";
            } else {
                var name = addControl.find(":text.name").val();
                if (name == "") {
                    alert("Nhập đầy đủ dữ liệu.");
                    return;
                }
                var functionName = addControl.find(".select-action").val();
                var sortBy = $(".sort-by").val();
                if (functionName === "sort") {
                    if (sortBy == "") {
                        alert("Nhập đầy đủ dữ liệu.");
                        return;
                    }
                    functionName = "sortDocument('" + sortBy + "')";
                }
                var newItem = $("<div>").addClass(config.toolbox.field_className);
                newItem.append($("<a>").attr("href", "#").attr("onclick", functionName).text(name));
                $this.append(newItem);
                addControl.remove();
            }
        });
        $(config.contentPanel.jClass).append(addControl);
        addControl.find(":text.name").focus();
    };

    var _showSortValue = function () {
        $(".sort-value").show();
        $(config.toolbox.dataField_class + " " + config.toolbox.field_class).each(function () {
            $(".field-collection").append($("<option>").text($(this).text()).val($(this).attr("value")));
        });
        $(".field-collection").change(function () {
            $(".sort-by").val($(this).val());
        });
    };

    //#endregion

    // khởi tạo thành đối tượng của eReport
    eReport.field = new field();

})(eReport = eReport || {});