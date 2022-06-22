/*
* eForm.Tool V2.1.0
*
* Copyright 2012: Bkav - Bso - Phòng 2 - eGate Team
* Created by: AnhNGTB@bkav.com.vn
* Edited by: CuongNT@bkav.com.vn, AnhNVA@bkav.com.vn, TienBV @bkav.com.vn
*
* Mô tả về lớp eForm.efTools.
* Public property
*   this._formId_:  Biến dùng khi load form động để Tiếp nhận hồ sơ. Giúp tạo id không trùng lặp giữa form chính và phụ. 
*   Không dùng khi chỉnh sửa hồ sơ.
*   Thư viện ngoài sử dụng: eForm.Controls.js
* Private property
*   formId:         Biến sử dụng tạo row không trùng lặp khi tiếp nhận hồ sơ giữa form chính và phụ. [rid=...]
*   Sử dụng để tạo database lưu control bằng js cho cho form chính và phụ riêng biệt eForm.database['data'+formid];
* Convention
* Public method
*/

(function (eForm, $, undefined) {

    //#region <CONSTANTS>
    eForm.cfgId = {
        rootPanel: "pnl_root",
        propertyPanel: "pnl_root_prop"
    };

    eForm.cfgPrefixId = {
        divRowId: "row_",                   // row_1
        divControlId: "pnl_",               // pnl_1
        divLabelId: "lbl_",                 // lbl_8c9
        controlId: "ctrl_",                 // ctrl_8c9
        divErrorId: "err_",                 // err_8c9
        divPropertyId: "prop_",
        divContainPropertyId: "pnlpr_"      // div chứa label và property cần chỉnh sửa
    };
    //#endregion <CONSTANTS>

    eForm.efTools = new function () {
        var oThis = this;
        var ddlLibs = null;                 // Danh sách các ddl > Catalog của eGate truyền xuống
        var currentId = 0;                  // Id của Control hiện tại
        var currRow = 5;                    // Dòng cuối cùng hiện tại >> Tổng số dòng
        var currOrder = 0;                  // Tương ứng với giá trị x trên trục tọa độ
        var currLine = 0;                   // Tương ứng với giá trị y trên trục tọa độ
        var currCol = 0;                    // ....
        var currFormId = 0;                 // FormId hiện đang focus (AnhNVa thêm)
        var currMatrix = "0.0";             // Vị trí của control đang được focus tại form tiếp nhận (AnhNVa thêm)
        // Sử dụng cho việc xác định formId khi nhấn các phím mũi tên
        var currIsDropdown = false;
        var currentForcusRow = undefined;   // dòng đang được chọn để thêm control
        var activingCtrlId = -1;            // Id của Control được kích hoạt
        var movingCtrlId = -1;              // Id của Control được drag
        var pnlMainId = "";                 // Id gốc của panel >> dùng để thêm dòng là chính
        var arrowKeyActiving = true;
        this._formId_ = "";                 // Biến dùng khi load form động để Tiếp nhận hồ sơ. Giúp tạo id không trùng lặp giữa form chính và phụ. Không dùng khi chỉnh sửa hồ sơ.
        this.formId = "";

        //#region <GLOBAL>
        // Khởi gán thư viện control, property cho eForm.efTools
        this.init = function (lib, rootPanel, pFormId) {
            if (pFormId === undefined || pFormId === null) {
                pFormId = "";
            }
            currentId = 0;              // id của control đang focus
            pnlMainId = rootPanel;      // id của <div> chứa các control
            oThis._formId_ = "_" + pFormId + "_";
            oThis.formId = pFormId;
        }.bind(this);

        // Khởi gán thư viện danh mục động cho eForm.efTools
        this.setDdlLibs = function (json) {
            ddlLibs = json;
        }.bind(this);

        // Lấy lại thư viện danh mục động
        this.getDdlLibs = function () {
            return ddlLibs;
        }.bind(this);
        //#endregion

        //#region <DRAP, DROP, CLICK EDIT>
        // Thiết lập 2 biến đã khai báo bên trên:
        // currentForcusRow: Dòng đang thao tác
        // currOrrder: Số lượng các phần tử con bên trong obj truyền vào (số các phần tử trong dòng)
        var setRowForcus = function (obj) {
            if (currentForcusRow != obj) {
                currentForcusRow = obj;
                currOrder = $(obj).children().size();
            }
        };

        // Load form tùy chỉnh property của 1 control chỉ định
        // Có các sự kiện thay đổi giao diện kèm theo
        // pnlPropId: Id của thẻ div chứa properties
        var settingProperties = function (ctrlId, pnlPropId) {
            var ctrl = eForm.database.Get(ctrlId, oThis.formId);
            if (ctrl == null) return;

            $("#" + pnlPropId).html("");

            // Lấy lại instance của máy tạo control
            var dft = eForm.CreateFormObject(ctrl.getTypeId()); //ctrlLibs

            // Lấy lại danh sách property mặc định của loại control tương ứng
            var proConfig = eForm.getPropertyByControlType(ctrl.getTypeId());

            // Chèn tiêu đề nhóm Property (để trình bày, gom nhóm các Property)
            $('<div class="basicGroup" style="clear: both;"></div>')
                .appendTo("#" + pnlPropId);
            $('<h4>▼ Cơ bản</h4>')
                .click(function () {
                    if ($('.basicGroup > div').css('display') == 'none') {
                        $('.basicGroup > div').show();
                        $.cookie('basicG', '1');
                    }
                    else {
                        $('.basicGroup > div').hide();
                        $.cookie('basicG', '0');
                    }
                })
                .appendTo('.basicGroup');

            $('<div class="styleGroup" style="clear: both;"></div>')
                .appendTo("#" + pnlPropId);
            $('<h4>▼ Định dạng, kích thước, màu sắc</h4>')
                .click(function () {
                    if ($('.styleGroup > div').css('display') == 'none') {
                        $('.styleGroup > div').show();
                        $.cookie('styleG', '1');
                    }
                    else {
                        $('.styleGroup > div').hide();
                        $.cookie('styleG', '0');
                    }
                })
                .appendTo('.styleGroup');

            $('<div class="advanceGroup" style="clear: both;"></div>')
                .appendTo("#" + pnlPropId);
            $('<h4>▼ Nâng cao</h4>')
                .click(function () {
                    if ($('.advanceGroup > div').css('display') == 'none') {
                        $('.advanceGroup > div').show();
                        $.cookie('advanceG', '1');
                    }
                    else {
                        $('.advanceGroup > div').hide();
                        $.cookie('advanceG', '0');
                    }
                })
                .appendTo('.advanceGroup');

            // Cần sửa để thứ tự trong mảng này là thứ tự control xuất hiện trên property
            var basicProp = ["p1", "p13", "p14", "p15", "p17", "p19", "p22", "p23", "p24"];
            var styleProp = ["p2", "p3", "p4", "p5", "p6", "p7", "p8", "p9", "p10", "p18", "p20"];
            var advanceProp = ["p11", "p12", "p16", "p21", "p25", "p26", "p27", "p28", "p29", "p30", "p31"];
            var invisibleProp = ["p24"];
            // Duyệt trên từng Property mới nhất trong thư viện
            for (var i = 0, l = proConfig.length; i < l; i++) {
                // Load label (ui name) của property
                var propBase = eForm.propertylib[proConfig[i].getId()];

                // Tạo div chứa 1 property (label property + control property).
                var divContain = eForm.cfgPrefixId.divContainPropertyId + proConfig[i].getId();
                var divContainProp = "";
                if (jQuery.inArray(proConfig[i].getId(), basicProp) > -1) {
                    divContainProp = "#" + pnlPropId + " .basicGroup";
                }
                else {
                    if (jQuery.inArray(proConfig[i].getId(), styleProp) > -1) {
                        divContainProp = "#" + pnlPropId + " .styleGroup";
                    }
                    else {
                        if (jQuery.inArray(proConfig[i].getId(), advanceProp) > -1) {
                            divContainProp = "#" + pnlPropId + " .advanceGroup";
                        }
                    }
                }
                $("<div></div>")
                    .attr('id', divContain)
                    .css({ "float": "left", clear: "both", width: "280px", "margin-top": "5px", "margin-left": "10px" })
                    .appendTo(divContainProp);

                // Tạo div chứa label property
                $("<div></div>")
                    .html("<b>" + propBase[0].getUIName() + ":</b> ")
                    .css({ "float": "left", "min-width": "100px" })
                    .appendTo('#' + divContain);

                // Tạo control chỉnh sửa property
                // Tìm trong ctrl có property này không. Nếu không thì lấy luôn property thư viện để load lên form cấu hình
                var currPro = ctrl.getProperty(proConfig[i].getId());
                if (!currPro) currPro = proConfig[i];
                dft.LoadProperty(ctrl, currPro, divContain);
            }
            $("#pnlprp15").css('display', 'none');
            $("#" + pnlPropId).show("slow");

            // Expanse hoặc Collapse các nhóm Property theo value đã được lưu vào Cookie:
            if ($.cookie('basicG') == '0') {
                $('.basicGroup > div').hide();
            }
            else {
                $('.basicGroup > div').show();
            }
            if ($.cookie('styleG') == '0') {
                $('.styleGroup > div').hide();
            }
            else {
                $('.styleGroup > div').show();
            }
            if ($.cookie('advanceG') == '0') {
                $('.advanceGroup > div').hide();
            }
            else {
                $('.advanceGroup > div').show();
            }

            // Nút xóa control 
            // Với c9, c10: Chỉ được xóa trong trường hợp form đang lưu tạm (Status = TMP trên database) hay divEdit có tồn tại
            // Với c1: luôn luôn thêm được phép xóa.
            // Điều kiện $('#divEdit').length == 0  phụ thuộc vào code C#: DesignForm.cs
            if ($('#divEdit').length == 0 || ctrl.getTypeId() == 'c1') {
                if ($('#btnRemove').length > 0) {
                    $('#btnRemove').remove();
                }
                $("<input />")
                        .attr("id", "btnRemove")
                        .attr("type", "button")
                        .val("Xóa control")
                        .insertAfter("#btnSave")
                        .click(function () {
                            $("#" + eForm.cfgPrefixId.divControlId + ctrl.getId()).remove();
                            eForm.database.RemoveControl(ctrl.getId(), oThis.formId);
                            $("#" + pnlPropId)
                                .html("")
                                .css({ border: "solid 0px gray", padding: "0px" });
                            $(this).remove();
                            // Tienbv 121109 Bổ sung enable những catalog và exfield đã dc xóa trong ddl.
                            var ctrlType = ctrl.getTypeId();
                            //var ctrlId = ctrl.getControlId();
                            if (ctrlType == "c9") {
                                $("#ddlExtendField option[value='" + ctrl.getControlId() + "']").removeAttr("disabled");
                            } else {
                                $("#Catalogs option[value='" + ctrl.getControlId() + "']").removeAttr("disabled");
                            }
                        });
            } else {
                $('#btnRemove').remove();
            }
        };

        // Move 1 đối tượng control ra trước 1 control chỉ định (dựa vào control id)
        // trong list control đang quản lý (eForm.database)
        var moveCtrlBefore = function (sourceId) {
            var sourceCtrl = eForm.database.Get(sourceId, oThis.formId);
            var destCtrl = eForm.database.Get(movingCtrlId, oThis.formId);
            if (sourceCtrl == null || destCtrl == -1) return;

            var oldOrder = destCtrl.getPosOrder();
            var oldRow = destCtrl.getPosRow();

            var tmpOrder = sourceCtrl.getPosOrder();
            var tmpRow = sourceCtrl.getPosRow();
            var isSameRow = false;
            if (oldRow == tmpRow) {
                if (oldOrder < tmpOrder) {
                    tmpOrder--;
                    isSameRow = true;
                }
            }

            // Dời các control của nơi đến ra sau 1 vị trí
            for (var i = 0, l = eForm.database.Count(oThis.formId) ; i < l; i++) {
                var tmpCtrl = eForm.database.GetAll(oThis.formId)[i];
                var po = tmpCtrl.getPosOrder();
                // Reorder row moi
                if (tmpCtrl.getPosRow() == tmpRow
                    && ((po > (tmpOrder + 1) && isSameRow)
                        || (po >= tmpOrder && !isSameRow))) {
                    tmpCtrl.setPosOrder(po + 1);
                }
                    // Reorder row cu          
                else if (tmpCtrl.getPosRow() == oldRow
                     && po > oldOrder
                         && po <= tmpOrder) {
                    tmpCtrl.setPosOrder(po - 1);
                }
            }

            destCtrl.setPosOrder(tmpOrder);
            destCtrl.setPosRow(tmpRow);
            currOrder = currentForcusRow.children().size();
        };

        // Move 1 đối tượng control vào 1 dòng chỉ định (dựa vào control id & row Id)
        // trong list control đang quản lý (database)
        var moveCtrlAtRow = function (rowId) {
            var rowObj = $("[rid='" + rowId + "']");
            var destCtrl = eForm.database.Get(movingCtrlId, oThis.formId);
            if (destCtrl == null || rowObj == null) return;
            //
            var oldOrder = destCtrl.getPosOrder();
            var oldRow = destCtrl.getPosRow();
            var tmpOrder = $(rowObj).children(".css_NestedElement").length;
            // nếu move cùng dòng thì bỏ đi vị trí của bản copy
            if (rowId == oldRow) {
                tmpOrder--;
                // Nếu vị trí không thay đổi thì thôi ko làm nữa
                if (tmpOrder == oldOrder) return;
            } //
            // Dời các control của nơi đi lên trước 1 vị trí
            for (var i = 0, l = eForm.database.Count(oThis.formId) ; i < l; i++) {
                // Reorder row cu
                var tmpCtrl = eForm.database.GetAll(oThis.formId)[i];
                if (tmpCtrl.getPosRow() == oldRow && tmpCtrl.getPosOrder() > oldOrder) {
                    tmpCtrl.setPosOrder(tmpCtrl.getPosOrder() - 1);
                }
            }
            // Xét tham chiếu
            destCtrl.setPosOrder(tmpOrder);
            destCtrl.setPosRow(rowId);
            currOrder = currentForcusRow.children(".css_NestedElement").length;
        };

        // Chỉnh lại kích thước của control (property: width & height) 
        // trong danh sách các control đang quản lý (trong buffer)
        var reSize = function (ctrlId, obj) {
            var ctrl = eForm.database.Get(ctrlId, oThis.formId);
            if (ctrl == null) return;
            //
            var wIdx = -1, hIdx = -1;
            var w = parseInt($(obj).css("width"));
            var h = parseInt($(obj).css("height"));
            // Lấy index của property width, height (nếu có) của control
            for (var i = 0, l = ctrl.getProperties().length; i < l; i++) {
                if (ctrl.getProperties()[i].getId() == "p6") {
                    if (w > 0) wIdx = i;
                } else if (ctrl.getProperties()[i].getId() == "p7")
                    if (h > 0) hIdx = i;
            }

            w = w + "px";
            h = h + "px";
            if (wIdx > -1) {
                ctrl.getProperties()[wIdx].setValue(w);
                if (activingCtrlId == ctrlId) {
                    $("#prop_p6").val(w);
                    $(obj).css("width", w);
                }
            }
            if (hIdx > -1) {
                ctrl.getProperties()[hIdx].setValue(h);
                if (activingCtrlId == ctrlId) {
                    $("#prop_p7").val(h);
                    $(obj).css("height", h);
                }
            }

            var lblWidth = parseInt($('#' + eForm.cfgPrefixId.divLabelId + ctrl.getKey()).css('width'));
            if (lblWidth > 0)
                $('#' + ctrl.getKey()).css('width', (parseInt(w) - lblWidth - 15) + 'px');
        };

        // Gán sự kiện click, dropable cho các div là row <div class="icontainer"></div>
        this.bindRowEvent = function () {
            //$(".icontainer").unbind('droppable').unbind('click');
            $(".icontainer").click(function () {
                $(".icontainer").removeClass("css_SelectedRow");
                $(this).addClass("css_SelectedRow");
                setRowForcus($(this));
            })
            .hover(function () {
                $(this).children('.btnDeleteRow').css('visibility', 'visible');
            }, function () {
                $(this).children('.btnDeleteRow').css('visibility', 'hidden');
            })
            .droppable({
                accept: ".css_NestedElement",
                //hoverClass: "css_DragHoverItem",
                drop: function (e, ui) {
                    $(this).append($(ui.draggable));
                    moveCtrlAtRow($(this).attr('rid'));
                }
            });
        }.bind(this);

        // Trả về dòng tiếp theo của form (dùng khi thêm dòng mới)
        this.getNextRow = function () {
            currRow++;
            return currRow;
        }.bind(this);

        // Xóa 1 dòng bất kỳ trên form dựa vào rowId
        this.deleteRow = function (rowId) {
            var rowObj = $("[rid='" + rowId + "']");
            // 0. Kiểm tra xem có control trên dòng được xóa không
            var tmpControl = rowObj.children('div[id^="pnl"]').length;
            if (tmpControl > 0) {
                alert("Không thể xóa dòng chứa control.");
                return;
            }

            // 1. Sửa đổi các dòng sau đó trên giao diện (rid, span chứa số dòng)
            $('.icontainer ').each(function () {
                var rid = $(this).attr('rid');
                if (rid > rowId) {
                    $(this).attr('rid', rid - 1);
                    $(this).children('.iRowNumber').text(rid - 1);
                    $(this).children('.btnDeleteRow').unbind('click').click(function () {
                        eForm.efTools.deleteRow(rid - 1);
                    });
                }
            });

            // 2. Xóa dòng đó trên giao diện
            rowObj.remove();

            // 3. Thiết lập lại biến tổng số dòng currRow, dòng được chọn currentForcusRow:
            currRow--;
            currentForcusRow = undefined;

            // 4. Sửa đổi các control có PosRow > row vừa xóa (-1)
            var allControls = eForm.database.GetAll(oThis.formId);
            for (var j = 0, l = eForm.database.Count(oThis.formId) ; j < l; j++) {
                var tmpCtrl = allControls[j];
                if (tmpCtrl.getPosRow() > rowId) {
                    tmpCtrl.setPosRow(tmpCtrl.getPosRow() - 1);
                }
            }
        }.bind(this);
        //#endregion

        //#region <CHỈNH SỬA FORM CHO QUẢN TRỊ>
        // Tạo row mặc định để add control vào <div class="icontainer"></div>
        var createFormRows = function (maxRow) {
            currRow = maxRow;
            currentId = eForm.database.Count(oThis.formId);
            $("#" + pnlMainId).html("");
            for (var i = 1; i <= currRow; i++) {
                $('<div></div>').addClass('icontainer')
                //.attr('rid', oThis.formId + i) //AnhNVa bỏ đi vì oThis._formId_ trả về undefined (không cần khi sửa form)
                .attr('rid', i)
                    .appendTo($("#" + pnlMainId));
                $('<span>' + i + '</span>').addClass('iRowNumber')
                .appendTo($('.icontainer[rid="' + i + '"]'));
                $('<a class="btnDeleteRow"><b>Χ</b></a>')
                .click(function () {
                    var id = $(this).parent().attr('rid');
                    eForm.efTools.deleteRow(id);
                })
                .appendTo($('.icontainer[rid="' + i + '"]'));
            }
        };
        // Tạo panel chứa control khi chỉnh sửa form. Click, resize, drap, drop.
        var createPanel = function (id) {
            if (currentForcusRow == undefined) return;
            var objId = eForm.cfgPrefixId.divControlId + id; // bỏ oThis._formId_ + so với khi tạo panel để tiếp nhận, view.
            $('<div></div>')
            .attr("id", objId)
            .addClass("css_NestedElement")
            .css({ "position": "", top: "", left: "" })
            .appendTo(currentForcusRow)
            .draggable({
                helper: "clone",
                opacity: .65,
                refreshPositions: true, // Performance?
                revert: "invalid",
                revertDuration: 300,
                scroll: true,
                cursor: "pointer",
                delay: 100,
                distance: 10,
                zIndex: 4000,
                start: function (event, ui) {
                    // Đánh dấu Id của control đang kéo (drag)
                    movingCtrlId = id;
                }
            })//end draggable
            .droppable({
                accept: ".css_NestedElement",
                //hoverClass: "css_DragHoverItem",
                drop: function (e, ui) {
                    $(this).before($(ui.draggable));
                    moveCtrlBefore(id);
                } //end drop
            })//end droppable
            .resizable({
                grid: [10, 10] //resize theo grid 10x10 pixel
                ,
                resize: function (event, ui) {
                    $(this).css({ "position": "", top: "", left: "" });
                    if (activingCtrlId == id) {
                        var w = parseInt($(this).css("width"));
                        var h = parseInt($(this).css("height"));
                        if (w > 0 && $("#prop_p6"))
                            $("#prop_p6").val(w + "px");
                        if (h > 0 && $("#p7prop"))
                            $("#prop_p7").val(h + "px");
                    } //end if
                    //
                    var ctr1 = eForm.database.Get(id, oThis.formId);
                    if (ctr1 != null) {
                        var lblWidth = parseInt($('#' + eForm.cfgPrefixId.divLabelId + ctr1.getKey()).css('width'));
                        if (lblWidth > 0)
                            $('#' + ctr1.getKey()).css('width', (parseInt($('#' + objId).css('width')) - lblWidth - 15) + 'px');
                    } //end if
                } //end resize event
                ,
                // Cap nhat trong DB tmp
                stop: function (event, ui) {
                    // doi gia tri trong DB tmp
                    reSize(id, $(this));
                } //end stop event
            })
            // event click de dieu chinh properties cua control
            .click(function () {
                if (activingCtrlId == id) return;
                settingProperties(id, eForm.cfgId.propertyPanel); //pnlMainId + "_prop"
                activingCtrlId = id;
                $(".css_NestedElementActiving").removeClass("css_NestedElementActiving");
                $(this).addClass("css_NestedElementActiving");
                $(".controlId").css('visibility', 'hidden');

                // AnhNVa - 120927: Nếu là chỉnh sửa thì không cho phép sửa mask >> disable ô Mask.
                if ($('#divEdit').length) {
                    $("select#prop_p19").attr('disabled', 'disabled');
                }
            });

            // Chèn thêm id vào mỗi panel để sử dụng khi click các textbox (validate, tự tính JS, C#)
            var ctrl = eForm.database.Get(id, oThis.formId);
            if (ctrl != null) {
                var controlId = ctrl.getControlId();
                $("#" + objId).attr("controlId", controlId);
                if (controlId != "") {
                    $('<span class="controlId" style="position: absolute; top: 0pt; right: 0pt; color: red; border: 1px solid gray; padding: 3px; background-color: yellow; visibility: hidden; opacity: 0.8;">Id = ' + ctrl.getKey() + '</span>').appendTo($('#' + objId));
                }
            }
        };

        this.LoadEditForm = function (json, maxRow) {
            if (!eForm.database.JsonDeserialize(json, oThis.formId))
                return;
            createFormRows(maxRow);
            //
            for (var j = 0, m = eForm.database.Count(oThis.formId) ; j < m; j++) {
                var ctrl = eForm.database.GetAll(oThis.formId)[j];
                setRowForcus($("[rid='" + ctrl.getPosRow() + "']"));
                addControl(ctrl);
            } //end for
        }.bind(this);
        var addControl = function (ctrl) {
            if (parseInt(ctrl.getTypeId()) < 1) return; // ctrl.getTypeId() = c10, c9
            createPanel(ctrl.getId());

            // Khởi tạo control và add control vào buffer
            var dft = eForm.CreateFormObject(ctrl.getTypeId()); //ctrlLibs
            dft.Add(ctrl, eForm.cfgPrefixId.divControlId + ctrl.getId());
        }.bind(this);

        // Thêm 1 control Tiêu đề (nhãn) hoặc ExtendField (Ô nhập liệu (nhãn)) mới vào form động
        this.Create = function (typeId) {
            if (parseInt(typeId) < 1) return;
            var objId = ++currentId;
            // Đảm bảo không có Label nào có Id bị trùng nhau khi xóa bớt control Label đi
            if (typeId == 'c1') {
                // Kiểm tra catalog thêm đã tồn tại trong form chưa. Có rồi thì return.
                do {
                    var tmp = eForm.database.GetByControlId(objId, "c1", oThis.formId);
                    if (tmp != null) {
                        objId = ++currentId;
                    }
                } while (tmp != null);
            }
            createPanel(objId);

            // Khởi tạo control và add control vào buffer
            var dft = eForm.CreateFormObject(typeId); //ctrlLibs
            var ctrl = dft.Create(objId, typeId, eForm.cfgPrefixId.divControlId + objId);

            if (ctrl != false) {
                ctrl.setPosRow(currentForcusRow.attr("rid"));
                ctrl.setPosOrder(++currOrder);
                eForm.database.Add(ctrl, oThis.formId);
            } else
                currentId--;
        }.bind(this);
        // ADD NEW CATALOG, EXFIELD TO FORM
        // Thêm 1 control Catalog mới vào 
        this.CreateCat = function (controlId, controlTitle) {
            if (parseInt(controlId) < 1) return;
            // Kiểm tra catalog thêm đã tồn tại trong form chưa. Có rồi thì return.
            var tmp = eForm.database.GetByControlId(controlId, "c10", oThis.formId);
            if (tmp != null) return;

            var objId = ++currentId;
            createPanel(objId);

            // Khởi tạo control và add control vào buffer
            var dft = eForm.CreateFormObject("c10"); //ctrlLibs
            var ctrl = dft.CreateCat(objId, eForm.cfgPrefixId.divControlId + objId, controlId, controlTitle);

            if (ctrl != false) {
                ctrl.setPosRow(currentForcusRow.attr("rid"));
                ctrl.setPosOrder(++currOrder);
                eForm.database.Add(ctrl, oThis.formId);
            } else
                currentId--;
        }.bind(this);
        // Thêm 1 control Extend Field mới
        this.CreateExtend = function (controlId, controlTitle) {
            if (parseInt(controlId) < 1) return;
            // Kiểm tra exfield thêm đã tồn tại trong form chưa. Có rồi thì return.
            var tmp = eForm.database.GetByControlId(controlId, "c9", oThis.formId);
            if (tmp != null) return;

            var objId = ++currentId;
            createPanel(objId);

            // Khởi tạo control và add control vào buffer
            var dft = eForm.CreateFormObject("c9"); //ctrlLibs
            var ctrl = dft.CreateExtend(objId, eForm.cfgPrefixId.divControlId + objId, controlId, controlTitle);

            if (ctrl != false) {
                ctrl.setPosRow(currentForcusRow.attr("rid"));
                ctrl.setPosOrder(++currOrder);
                eForm.database.Add(ctrl, oThis.formId);
            } else
                currentId--;
        }.bind(this);
        //#endregion

        //#region <LOAD FORM FOR TIEP NHAN AND VIEW>
        // Tạo danh sách các row, sau đó add từng control vào row theo [rid='...'] tương ứng
        var loadFormRows = function (maxRow) {
            currRow = maxRow;
            currentId = eForm.database.Count(oThis.formId);
            $("#" + pnlMainId).html("");
            for (var i = 1; i <= currRow; i++) {
                $('<div></div>').addClass('icontainer')
                    .attr('rid', oThis._formId_ + i) // thêm oThis._formId_ để tránh form chính và phụ trùng id
                    .appendTo($("#" + pnlMainId));
            }
        };
        // Tạo panel chứa control khi sử dụng form.
        var loadPanel = function (id) {
            if (currentForcusRow == undefined) return;
            //TienBV: _formId_ dùng khi load ca form chinh phu
            // pnlMainId: để khi load nhiều form theo các tab văn bản
            var objId = pnlMainId + oThis._formId_ + eForm.cfgPrefixId.divControlId + id;
            $('<div></div>')
            .attr("id", objId)
            .addClass("css_NestedInputForm")
            .appendTo(currentForcusRow)
            // Set position cho event Arrow Keys
            .click(function () {
                var ctrl = eForm.database.Get(id, oThis.formId);
                currCol = ctrl.getPosOrder();
                currLine = ctrl.getPosRow();
                currFormId = $('#' + objId).parents('.sub_pnl_root').attr('formid');
                currMatrix = $(this).attr("matrix");
            });
        };

        // TIEP NHAN FORM
        // 120418 - cuongnt - thêm param formid: tạo database cho các form khác nhau.
        this.LoadForm = function (json, maxRow) {//formid
            if (!eForm.database.JsonDeserialize(json, oThis.formId))
                return;
            // Tạo các bảng row để add control vào
            loadFormRows(maxRow);
            // Duyệt trên từng control --> Tìm row tương ứng --> add control vào row này
            var allCtrls = eForm.database.GetAll(oThis.formId);
            for (var j = 0, m = allCtrls.length; j < m; j++) {//eForm.database.Count()
                var ctrl = allCtrls[j];
                setRowForcus($("#" + pnlMainId).find("[rid='" + oThis._formId_ + ctrl.getPosRow() + "']"));
                loadControl(ctrl);
            }
        }.bind(this);
        var loadControl = function (ctrl) {
            //if (parseInt(ctrl.getTypeId()) < 1) return;// ctrl.getTypeId() = c10, c9
            loadPanel(ctrl.getId());

            // Khởi tạo đối tượng cỗ máy sẽ xử lý việc tạo ra control loại nào đó tương ứng
            // --> Cần sửa chỗ này vì các cỗ máy này hoàn toàn là cố định được
            var dft = eForm.CreateFormObject(ctrl.getTypeId()); //ctrlLibs
            // Xử dụng đối tượng và tạo + dữ liệu control thật từ ctrl để tạo control add vào root panel
            dft.Load(ctrl, pnlMainId + oThis._formId_ + eForm.cfgPrefixId.divControlId + ctrl.getId());
        };

        // VIEW FORM KHI ĐÃ TIẾP NHẬN
        this.ViewForm = function (json, maxRow) {
            if (!eForm.database.JsonDeserialize(json, oThis.formId))
                return;
            loadFormRows(maxRow);
            //
            for (var j = 0, m = eForm.database.Count(oThis.formId) ; j < m; j++) {
                var ctrl = eForm.database.GetAll(oThis.formId)[j];
                setRowForcus($("[rid='" + oThis._formId_ + ctrl.getPosRow() + "']"));
                viewControl(ctrl);
            } //end for
        }.bind(this);
        var viewControl = function (ctrl) {
            if (parseInt(ctrl.getTypeId()) < 1) return;
            loadPanel(ctrl.getId());

            // Khởi tạo control và add control vào buffer
            var dft = eForm.CreateFormObject(ctrl.getTypeId()); //ctrlLibs
            dft.View(ctrl, oThis._formId_ + eForm.cfgPrefixId.divControlId + ctrl.getId());
        }.bind(this);
        //#endregion

        //#region <ARROWKEYS>
        this.ArrowKeys = {
            init: function () {
                currCol = 0;                                            // Cột chứa control được focus
                currLine = 1;                                           // Hàng chứa control được focus
                currFormId = $('.sub_pnl_root').first().attr('formid'); // Form chứa control được focus
                currMatrix = "0.0";

                var mRow = 1;
                var mOrder = 1;
                $('.icontainer').each(function () {
                    mOrder = 1;
                    $(this).children('.css_NestedInputForm').each(function () {
                        $(this).attr('matrix', mRow + "." + mOrder);
                        mOrder++;
                    });
                    mRow++;
                });
                $('.css_NestedInputForm input').focus(function () {
                    currMatrix = $(this).parent().attr("matrix");
                });
                $('.css_NestedInputForm select').focus(function () {
                    currMatrix = $(this).parent().attr("matrix");
                });
                var curOjb = this;

                $('#pnl_root').click(function (e) {
                    if (!arrowKeyActiving) {
                        curOjb.start(e);
                        arrowKeyActiving = true;
                    }
                });

                return true;
            }
            ,
            start: function (event) {
                // $('#pnl_root').keydown(function (event) {
                $('#' + pnlMainId).keydown(function (event) {
                    var nextControlId = eForm.efTools.ArrowKeys.FindNextControlId(event);

                    if ($("#" + nextControlId).is("input")) {
                        $("#" + nextControlId).focus();
                        currIsDropdown = false;
                    }
                    else if ($("#" + nextControlId).is("select")) {
                        $("#" + nextControlId).focus();
                        currIsDropdown = true;
                    }

                    currMatrix = $("#" + nextControlId).parent().attr("matrix");
                });
            }
            ,
            FindNextControlId: function (event) {
                if (currMatrix == "0.0") {
                    return eForm.efTools.ArrowKeys.ReturnFisrtControlId();
                }
                else {
                    var tmpMatrix = currMatrix.split(".");
                    var mCurrRow = parseInt(tmpMatrix[0]);
                    var mCurrOrder = parseInt(tmpMatrix[1]);
                    var sumOfRow = $('.icontainer').length;
                    switch (event.keyCode) {
                        case 37: //left
                            var rowCounter = mCurrRow;
                            var orderCounter = mCurrOrder;
                            for (var i = mCurrRow; i > 0; i--) {
                                orderCounter--;
                                //Tim control ke tiep tai dong hien tai
                                var nextControlId = eForm.efTools.ArrowKeys.FindControlIdByMatrix(rowCounter, orderCounter);
                                if (nextControlId != "") {
                                    return nextControlId;
                                }
                                //Neu khong co thi len dong ben tren tim tiep
                                rowCounter--;
                                var maxOrder = $($('.icontainer')[rowCounter]).children(".css_NestedInputForm").length;
                                orderCounter = maxOrder + 1;
                            }
                            return eForm.efTools.ArrowKeys.ReturnFisrtControlId();
                        case 38: //up
                            var rowCounter = mCurrRow;
                            var orderCounter = mCurrOrder;
                            for (var i = mCurrRow; i > 0; i--) {
                                rowCounter--;
                                //Tim o dong tren xem co control nao cung cot khong?
                                var nextControlId = eForm.efTools.ArrowKeys.FindControlIdByMatrix(rowCounter, orderCounter);
                                if (nextControlId != "") {
                                    //Neu co thi return
                                    return nextControlId;
                                }
                                else {
                                    //Neu khong thi For { tim control co cot - 1 }
                                    for (var j = orderCounter; j > 0; j--) {
                                        orderCounter--;
                                        nextControlId = eForm.efTools.ArrowKeys.FindControlIdByMatrix(rowCounter, orderCounter);
                                        if (nextControlId != "") {
                                            return nextControlId;
                                        }
                                    }
                                }
                                orderCounter = mCurrOrder;
                            }
                            return eForm.efTools.ArrowKeys.ReturnFisrtControlId();
                        case 39: //right
                            var rowCounter = mCurrRow;
                            var orderCounter = mCurrOrder;
                            for (var i = mCurrRow; i <= sumOfRow; i++) {
                                orderCounter++;
                                //Tim control ke tiep tai dong hien tai
                                var nextControlId = eForm.efTools.ArrowKeys.FindControlIdByMatrix(rowCounter, orderCounter);
                                if (nextControlId != "") {
                                    return nextControlId;
                                }
                                //Neu khong co thi xuong dong tim tiep
                                rowCounter++;
                                orderCounter = 0;
                            }
                            return eForm.efTools.ArrowKeys.ReturnFisrtControlId();
                        case 40: //down
                            var rowCounter = mCurrRow;
                            var orderCounter = mCurrOrder;
                            for (var i = mCurrRow; i <= sumOfRow; i++) {
                                rowCounter++;
                                //Tim o dong duoi xem co control nao cung cot khong?
                                var nextControlId = eForm.efTools.ArrowKeys.FindControlIdByMatrix(rowCounter, orderCounter);
                                if (nextControlId != "") {
                                    return nextControlId;
                                }
                                else {
                                    //Neu khong thi for { tim control co cot - 1 }. Tim thay thi return.
                                    for (var j = orderCounter; j > 0; j--) {
                                        orderCounter--;
                                        nextControlId = eForm.efTools.ArrowKeys.FindControlIdByMatrix(rowCounter, orderCounter);
                                        if (nextControlId != "") {
                                            return nextControlId;
                                        }
                                    }
                                }
                                orderCounter = mCurrOrder;
                            }
                            return eForm.efTools.ArrowKeys.ReturnLastControlId();
                        case 13: //enter                            
                            event.preventDefault();
                            var rowCounter = mCurrRow;
                            var orderCounter = mCurrOrder;
                            //Tìm control đầu tiên của dòng phía dưới
                            for (var i = mCurrRow; i < sumOfRow; i++) {
                                rowCounter++;
                                var nextControlId = eForm.efTools.ArrowKeys.ReturnFirstControlIdInRow(rowCounter);
                                if (nextControlId != "") {
                                    return nextControlId;
                                }
                            }
                            //Nếu không có nữa thì trả về chính control này (không di chuyển nữa)
                            return eForm.efTools.ArrowKeys.FindControlIdByMatrix(mCurrRow, mCurrOrder);
                        case 35: //end
                            return eForm.efTools.ArrowKeys.ReturnLastControlIdInRow(mCurrRow);
                        case 36: //home
                            return eForm.efTools.ArrowKeys.ReturnFirstControlIdInRow(mCurrRow);
                    }
                }
            }
            ,
            ReturnFisrtControlId: function () {
                var sumOfPanel = $('.css_NestedInputForm').length;
                for (var i = 0; i < sumOfPanel; i++) {
                    var thisPanel = $('.css_NestedInputForm')[i];
                    var input = $(thisPanel).children("input");
                    var select = $(thisPanel).children("select");
                    if ($(input).length > 0) {
                        return $(input).attr("id");
                    }
                    if ($(select).length > 0) {
                        return $(select).attr("id");
                    }
                }
            }
            ,
            ReturnLastControlId: function () {
                var sumOfPanel = $('.css_NestedInputForm').length;
                var lastControlId = "";
                for (var i = 0; i < sumOfPanel; i++) {
                    var thisPanel = $('.css_NestedInputForm')[i];
                    var input = $(thisPanel).children("input");
                    var select = $(thisPanel).children("select");
                    if ($(input).length > 0) {
                        lastControlId = $(input).attr("id");
                    }
                    if ($(select).length > 0) {
                        lastControlId = $(select).attr("id");
                    }
                }
                return lastControlId;
            }
            ,
            ReturnFirstControlIdInRow: function (mRow) {
                var sumOfPanel = $('.css_NestedInputForm').length;
                var tmpMatrix = new Array();
                for (var i = 0; i < sumOfPanel; i++) {
                    var thisPanel = $('.css_NestedInputForm')[i];
                    var input = $(thisPanel).children("input");
                    var select = $(thisPanel).children("select");
                    if ($(input).length > 0) {
                        tmpMatrix = $(thisPanel).attr("matrix").split(".");
                        if (tmpMatrix[0] == mRow.toString()) {
                            return $(input).attr("id");
                        }
                    }
                    if ($(select).length > 0) {
                        tmpMatrix = $(thisPanel).attr("matrix").split(".");
                        if (tmpMatrix[0] == mRow.toString()) {
                            return $(select).attr("id");
                        }
                    }
                }
                return "";
            }
            ,
            ReturnLastControlIdInRow: function (mRow) {
                var sumOfPanel = $('.css_NestedInputForm').length;
                var controlId = "";
                var tmpMatrix = new Array();
                for (var i = 0; i < sumOfPanel; i++) {
                    var thisPanel = $('.css_NestedInputForm')[i];
                    var input = $(thisPanel).children("input");
                    var select = $(thisPanel).children("select");
                    if ($(input).length > 0) {
                        tmpMatrix = $(thisPanel).attr("matrix").split(".");
                        if (tmpMatrix[0] == mRow.toString()) {
                            controlId = $(input).attr("id");
                        }
                    }
                    if ($(select).length > 0) {
                        tmpMatrix = $(thisPanel).attr("matrix").split(".");
                        if (tmpMatrix[0] == mRow.toString()) {
                            controlId = $(select).attr("id");
                        }
                    }
                }
                return controlId;
            }
            ,
            FindControlIdByMatrix: function (fRow, fOrder) {
                var matrix = fRow + "." + fOrder;
                var tmpPanel = $(".css_NestedInputForm[matrix = '" + matrix + "']");
                if (tmpPanel.length == 0) {
                    return "";
                }
                else {
                    var tmpInput = $(tmpPanel).children("input");
                    var tmpSelect = $(tmpPanel).children("select");
                    if ($(tmpInput).length > 0) {
                        return $(tmpInput).attr("id");
                    }
                    if ($(tmpSelect).length > 0) {
                        return $(tmpSelect).attr("id");
                    }
                    return "";
                }

            }
        };
    };

})(window.eForm = window.eForm || {}, jQuery);