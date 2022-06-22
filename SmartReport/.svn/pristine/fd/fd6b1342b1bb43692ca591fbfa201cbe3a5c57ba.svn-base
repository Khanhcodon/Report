(function (egov, $, _) {
    var config = {
        table: { cellSelectedClass: 'cellSelected', cellHoverClass: 'cellHover' },
        controls: { wrapControlClass: 'wrapControl', defaultWidthLabel: 150, labelClass: 'label-field' }
    };
    var allControls;
    var $panelFormSelector;
    var $currentCellSelected;
    var $tableId;

    var applyEventTable = function () {
        $panelFormSelector.find('table td')
            .unbind('mousedown')
            .mousedown(function (e) {
                var $cell = $(this);
                if (!e.ctrlKey) {
                    $('.' + config.table.cellSelectedClass).each(function (i, item) {
                        if (item.redips) {
                            REDIPS.table.mark(false, item);
                        }
                    });
                    $('.' + config.table.cellSelectedClass).removeClass(config.table.cellSelectedClass);
                }
                $currentCellSelected = $cell;
                REDIPS.table.mark(true, $cell[0]);
                if (!$cell.hasClass(config.table.cellSelectedClass)) {
                    $cell.addClass(config.table.cellSelectedClass);
                }
                var colspan = $cell.attr('colspan');
                if (!colspan) {
                    var $setting = $('<p></p>');
                    var $input = createTextboxAcceptNumberOnly();
                    $input.css({ 'margin-right': '5px' });
                    var $select = $('<select><option value="px">px</option><option value="%">%</option></select>').change(function () {
                        var value = parseInt($input.val());
                        if (isNaN(value)) {
                            $col.css({ width: '' });
                        }
                        $col.css({ width: value + $select.val() });
                    });
                    var indexCol = getCellPosition($cell).col;
                    var $col = $('#tableLayout col').eq(indexCol);
                    var widthInlineStyle = $col.inlineStyle('width');
                    if (widthInlineStyle) {
                        var width, typewidth;
                        if (widthInlineStyle.indexOf('%') > 0) {
                            typewidth = '%';
                        } else {
                            typewidth = 'px';
                        }
                        $select.find('option[value="' + typewidth + '"]').attr('selected', 'selected');
                        width = widthInlineStyle.replace(typewidth, '');
                        $input.val(width);
                    }
                    $input.keyup(function () {
                        var value = parseInt($(this).val());
                        if (isNaN(value)) {
                            $col.css({ width: '' });
                        }
                        $col.css({ width: value + $select.val() });
                    });
                    $setting.append($input).append($select);
                    $input.before('<div style="width:100px;display:inline-block">Chiều rộng cột:</div>');
                    $('#settingLayout').html($setting);
                } else {
                    $('#settingLayout').html('');
                }
            })
            .unbind('mouseenter mouseleave')
            .hover(function () {
                $(this).addClass(config.table.cellHoverClass);
            }, function () {
                $(this).removeClass(config.table.cellHoverClass);
            })
            .droppable({
                drop: function (e, ui) {
                    var $drag = $(ui.draggable);
                    allControls.controls[$drag.attr('id')].position = getCellPosition($(this));
                    $(this).append($(ui.draggable));
                    $('.' + config.table.cellHoverClass).removeClass(config.table.cellHoverClass);
                }
            });
    };

    var getCellPosition = function (cell) {
        var colIndex = cell.parent().children().index(cell);
        var rowIndex = cell.parent().parent().children().index(cell.parent());
        return { row: rowIndex, col: colIndex };
    };

    var createWrapControl = function (id, position) {
        var $cell;
        if (position) {
            $cell = $('#tableLayout > tbody > tr:eq(' + position.row + ') td:eq(' + position.col + ')');
        } else {
            $cell = $currentCellSelected;
        }
        var wrap = $('<div></div>').attr('id', id)
                .addClass(config.controls.wrapControlClass)
                .draggable({
                    helper: "clone",
                    opacity: .65,
                    refreshPositions: true,
                    revert: "invalid",
                    revertDuration: 300,
                    scroll: true,
                    distance: 10,
                    start: function (e, ui) {
                        if ($(ui.helper).find('.grid').length > 0) {
                            $(this).siblings().width($(this).parent().width());
                        }
                    }
                })
                .appendTo($cell);
        return wrap;
    };

    var createLabel = function (name, key, width) {
        var $label = $('<div class="' + config.controls.labelClass + '"><label ' + (key ? 'for="' + key + '"' : '') + '>' + name + ': </label></div>');
        if (width) {
            $label.width(width);
        }
        return $label;
    };

    var createTextboxAcceptNumberOnly = function () {
        return $('<input type="text" style="width:50px"/>').keydown(function (event) {
            if ($.inArray(event.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
                (event.keyCode == 65 && event.ctrlKey === true) ||
                (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });
    };

    var createSettingWidthLabel = function (key, width) {
        var $setting = $('<p></p>');
        var $input = createTextboxAcceptNumberOnly();
        if (width) {
            $input.attr('value', width);
        } else {
            $input.attr('value', 0);
        }
        $input.keyup(function () {
            var value = parseInt($(this).val());
            if (isNaN(value)) {
                value = config.controls.defaultWidthLabel;
            }
            allControls.controls[key].widthLabel = value;
            $('#' + key).children('.label-field').width(value);
        });
        $setting.append($input);
        $input.before('<div style="width:100px;display:inline-block">Chiều rộng nhãn:</div>');
        return $setting;
    };

    var createSettingHeight = function (key, $elemChange, height) {
        var $setting = $('<p></p>');
        var $input = createTextboxAcceptNumberOnly();
        if (height) {
            $input.attr('value', height);
        } else {
            $input.attr('value', 0);
        }
        $input.keyup(function () {
            var value = parseInt($(this).val());
            if (isNaN(value)) {
                $elemChange.css({ height: '' });
            }
            allControls.controls[key].height = value;
            $elemChange.height(value);
        });
        $setting.append($input);
        $input.before('<div style="width:100px;display:inline-block">Chiều cao:</div>');
        return $setting;
    };

    var createSettingDisable = function (key, $elemChange) {
        var $setting = $('<p></p>');
        var $input = $('<input type="checkbox" />').change(function () {
            var disable = $(this).prop('checked');
            allControls.controls[key].disable = disable;
            if (disable) {
                $elemChange.attr('disabled', 'disabled');
            } else {
                $elemChange.removeAttr('disabled');
            }
        });
        if (allControls.controls[key].disable) {
            $input.attr('checked', 'checked');
        }
        $setting.append($input);
        $input.before('<div style="width:100px;display:inline-block">Vô hiệu hóa:</div>');
        return $setting;
    };

    var createSettingValidate = function (key) {
        var $setting = $('<p></p>');
        var $input = $('<input type="checkbox" />').change(function () {
            var yes = $(this).prop('checked');
            allControls.controls[key].validate = yes;
        });
        if (allControls.controls[key].validate) {
            $input.attr('checked', 'checked');
        }
        $setting.append($input);
        $input.before('<div style="width:100px;display:inline-block">Có kiểm tra dữ liệu:</div>');
        return $setting;
    };

    var createButtonDelete = function (key, $wrap) {
        var $delete = $('<input type="button" value="Xóa"/>').click(function () {
            $wrap.remove();
            delete allControls.controls[key];
            $('#settingControl').html('');
        });
        return $('<p></p>').append($delete);
    };

    var createTextAreaControl = function (key, name, setting) {
        var $wrap = createWrapControl(key, setting.position);
        var $label = createLabel(name, key, setting.widthLabel);
        var $textarea = $('<textarea rows="2" cols="20"></textarea>');
        $textarea.attr('name', key).height(setting.height);
        if (setting.disable) {
            $textarea.attr('disabled', 'disabled');
        }
        var $field = $('<div class="field"></div>').append($textarea);
        $wrap.append($label).append($field);
        $wrap.mousedown(function () {
            var $disableSetting = createSettingDisable(key, $textarea);
            var $widthLabelSetting = createSettingWidthLabel(key, setting.widthLabel);
            var $heightSetting = createSettingHeight(key, $textarea, setting.height);
            var $delete = createButtonDelete(key, $wrap);
            $('#settingControl').html('').append($disableSetting).append($widthLabelSetting).append($heightSetting).append($delete);
        });
        return $wrap;
    };

    var createTextControl = function (key, name, setting) {
        var $wrap = createWrapControl(key, setting.position);
        var $label = createLabel(name, key, setting.widthLabel);
        var $input = $('<input type="text" />');
        $input.attr('name', key);
        if (setting.disable) {
            $input.attr('disabled', 'disabled');
        }
        var $field = $('<div class="field"></div>').append($input);
        $wrap.append($label).append($field);
        $wrap.mousedown(function () {
            var $disableSetting = createSettingDisable(key, $input);
            var $widthLabelSetting = createSettingWidthLabel(key, setting.widthLabel);
            var $delete = createButtonDelete(key, $wrap);
            $('#settingControl').html('').append($disableSetting).append($widthLabelSetting).append($delete);
        });
        return $wrap;
    };

    var createSelectControl = function (key, name, setting) {
        var $wrap = createWrapControl(key, setting.position);
        var $label = createLabel(name, key, setting.widthLabel);
        var $select = $('<select><option>' + name + '</option></select>');
        $select.attr('name', key);
        if (setting.disable) {
            $select.attr('disabled', 'disabled');
        }
        var $field = $('<div class="field"></div>').append($select);
        $wrap.append($label).append($field);
        $wrap.mousedown(function () {
            var $disableSetting = createSettingDisable(key, $select);
            var $widthLabelSetting = createSettingWidthLabel(key, setting.widthLabel);
            var $delete = createButtonDelete(key, $wrap);
            $('#settingControl').html('').append($disableSetting).append($widthLabelSetting).append($delete);
            if (typeof setting.validate === 'boolean') {
                var $validateSetting = createSettingValidate(key);
                $disableSetting.after($validateSetting);
            }
        });
        return $wrap;
    };

    var createDocRelationControl = function (setting) {
        var $wrap = createWrapControl('documentRelation', setting.position);
        $wrap.width('100%').append('<div><div class="grid"><div class="grid-header"><div class="grid-header-wrap"><table class="table-main" style="width:100%;"><thead><tr><th class="header">Trích yếu </th><th class="header">Số hiệu </th><th class="header">Người xử lý </th><th class="header">Ngày tạo </th><th class="header">Hình thức </th></tr> </thead></table></div></div><div class="grid-content" style="overflow:hidden; height:' + setting.height + 'px; outline:none;"><table class="table-main" style="100%"><tbody></tbody></table></div></div></div>');
        $wrap.mousedown(function () {
            var $heightSetting = createSettingHeight('documentRelation', $wrap.find('.grid-content'), setting.height);
            var $delete = createButtonDelete('documentRelation', $wrap);
            $('#settingControl').html('').append($heightSetting).append($delete);
        });
        return $wrap;
    };

    var createAttachmentControl = function (setting) {
        var $wrap = createWrapControl('attachment', setting.position);
        $wrap.width('100%').append('<div><div class="grid"><div class="grid-header"><div class="grid-header-wrap"><table class="table-main" style="width:100%;"><thead><tr><th class="header">Tên tệp</th><th class="header">Kích thước</th><th class="header">Phiên bản</th><th class="header">Người cập nhật cuối</th></tr> </thead></table></div></div><div class="grid-content" style="overflow:hidden; height:' + setting.height + 'px; outline:none;"><table class="table-main" style="100%"><tbody></tbody></table></div></div></div>');
        $wrap.mousedown(function () {
            var $heightSetting = createSettingHeight('attachment', $wrap.find('.grid-content'), setting.height);
            var $delete = createButtonDelete('attachment', $wrap);
            $('#settingControl').html('').append($heightSetting).append($delete);
        });
        return $wrap;
    };

    var createPaperFeeControl = function (key, name, setting) {
        var $wrap = createWrapControl(key, setting.position);
        var $label = createLabel(name, key, setting.widthLabel);
        var $checkbox = $('<div class="' + config.controls.labelClass + '" style="width: 20px;margin-left: 0;"><input type="checkbox" /></div>');
        var $input = $('<div class="field"><input type="text" style="width:80%" /><input type="text" style="width:16%;text-align: right;float:right" /></div>');
        var $field = $('<div class="field" style="margin-right:0"></div>').append($checkbox).append($input);
        $wrap.append($label).append($field);
        $wrap.mousedown(function () {
            var $widthLabelSetting = createSettingWidthLabel(key, setting.widthLabel);
            var $delete = createButtonDelete(key, $wrap);
            $('#settingControl').html('').append($widthLabelSetting).append($delete);
        });
        return $wrap;
    };

    var getCellByPosition = function (idTable, position) {
        //Để tạm
        if (position.row < 0) {
            position.row = 0;
        }
        if (position.col < 0) {
            position.col = 0;
        }

        var idTable = $tableId;
        return $panelFormSelector.find('#' + idTable + ' > tbody > tr:eq(' + position.row + ') td:eq(' + position.col + ')');
    };

    var toggleControl = function (idwrap, value) {
        if (value.isShowFull) {
            var $wrap = $panelFormSelector.find("#tableLayoutCollapse").find('#' + idwrap);
            $wrap.detach().appendTo(getCellByPosition('tableLayout', value.Options.position));
            return
        }
        var $wrap = $panelFormSelector.find("#tableLayout").find('#' + idwrap);
        $wrap.detach().appendTo(getCellByPosition('tableLayoutCollapse', value.Options.position));
    }

    var viewTextAreaControl = function (idwrap, value) {
        var $wrap = $panelFormSelector.find('#' + idwrap);
        if (value.widthLabel) {
            $wrap.children('.' + config.controls.labelClass).width(value.widthLabel);
        }
        var $textarea = $wrap.find('textarea');
        if (value.disable) {
            $textarea.attr('disabled', 'disabled');
            $textarea.after('<input name="' + $textarea.attr('name') + '" type="hidden" value="' + $textarea.val() + '" />');
        }
        if (value.height) {
            $textarea.height(value.height);
        }

        var tdPosition = getCellByPosition('tableLayout', value.position);
        var isFullRow = tdPosition.attr("colspan") === "2";
        // $wrap.find(".control-label").attr("class", isFullRow ? "control-label" : "control-label");
        // $wrap.find(".control-label").next();

        $wrap.appendTo(tdPosition);
        return $wrap;
    };

    var viewTextControl = function (idwrap, value) {
        var $wrap = $panelFormSelector.find('#' + idwrap);
        if (value.widthLabel) {
            $wrap.children('.' + config.controls.labelClass).width(value.widthLabel);
        }
        var $text = $wrap.find('input[type=text]');
        if (value.disable) {
            $text.attr('disabled', 'disabled');
            $text.after('<input name="' + $text.attr('name') + '" type="hidden" value="' + $text.val() + '" />');
        }

        var tdPosition = getCellByPosition('tableLayout', value.position);
        var isFullRow = tdPosition.attr("colspan") === "2";
        //$wrap.find(".control-label").attr("class", isFullRow ? "col-md-2 control-label" : "col-md-4 control-label");
        //$wrap.find(".control-label").next().attr("class", isFullRow ? "col-md-14" : "col-md-12");

        $wrap.appendTo(tdPosition);
        return $wrap;
    };

    var viewSelectControl = function (idwrap, value, addHidden) {
        if (typeof addHidden !== 'boolean') {
            addHidden = true;
        }
        var $wrap = $panelFormSelector.find('#' + idwrap);
        if (value.widthLabel) {
            $wrap.children('.' + config.controls.labelClass).width(value.widthLabel);
        }
        var $select = $wrap.find('select');
        if (value.disable) {
            $select.attr('disabled', 'disabled');
            if (addHidden) {
                $select.after('<input name="' + $select.attr('name') + '" type="hidden" value="' + $select.val() + '" />');
            }
        }

        var tdPosition = getCellByPosition('tableLayout', value.position);
        var isFullRow = tdPosition.attr("colspan") === "2";
        //$wrap.find(".control-label").attr("class", isFullRow ? "col-md-2 control-label" : "col-md-4 control-label");
        //$wrap.find(".control-label").next().attr("class", isFullRow ? "col-md-14" : "col-md-12");

        $wrap.appendTo(tdPosition);
        return $wrap;
    };

    if (!egov.formtemplate) {
        egov.formtemplate = {};
    }

    egov.formtemplate.edit = {
        load: function (panelSelector, template) {
            allControls = { layout: '<table id="tableLayout" class="layout"><colgroup><col/><col/></colgroup><tbody><tr><td></td><td></td></tr></tbody></table>', controls: {} };
            if (panelSelector) {
                if (panelSelector instanceof jQuery) {
                    if (panelSelector.length > 0) {
                        $panelFormSelector = panelSelector;
                    }
                } else if (typeof panelSelector === 'string') {
                    $panelFormSelector = $(panelSelector);
                }
            }

            if (template) {
                if (template.layout) {
                    allControls.layout = template.layout;
                }

                $panelFormSelector.html(allControls.layout);
                if (template.controls) {
                    $.each(template.controls, function (key, value) {
                        egov.formtemplate.controls[key].add(value);
                    });
                }
            } else {
                $panelFormSelector.html(allControls.layout);
            }

            $('#tableLayout').addClass('layout');

            applyEventTable();
        },
        getFormTemplate: function () {
            var $tableLayout = $('#tableLayout').clone();
            $tableLayout.removeAttr('class');
            $tableLayout.find('tbody td').each(function (i, item) {
                $(item).removeAttr('class').html('');
            });
            allControls.layout = $('<div></div>').append($tableLayout).html();
            return allControls;
        },
        clear: function () {
            allControls = { layout: '<table id="tableLayout" class="layout"><colgroup><col/><col/></colgroup><tbody><tr><td></td><td></td></tr></tbody></table>', controls: {} };
            $panelFormSelector.html(allControls.layout);
            $('#tableLayout').addClass('layout');
            applyEventTable();
        }
    };

    egov.formtemplate.view = {
        load: function (panelSelector, template) {
            var $panelSelector;
            if (!panelSelector) {
                return;
            }

            if (panelSelector.length > 0) {
                $panelSelector = panelSelector;
            } else if (typeof panelSelector === 'string') {
                $panelSelector = $(panelSelector);
            } else {
                throw 'Bạn phải truyền tham số panelSelector';
            }

            $panelFormSelector = $panelSelector;
            if (!template) {
                return;
            }

            var tableLayout = $(template.layout);
            $tableId = tableLayout.attr("id");
            if ($panelSelector.find("#" + $tableId).length === 0) {
                $panelSelector.append(tableLayout);
            }

            $panelSelector.find("#" + $tableId).show().siblings("table").hide();
            if (template.controls) {
                $.each(template.controls, function (key, value) {
                    egov.formtemplate.controls[key].view(value);
                });
            }
        },
        reviewToggle: function (panelSelector, templateNew, isShowFull) {
            if (!templateNew) {
                return;
            }

            if (panelSelector.find("#tableLayoutCollapse").length == 0) {
                var layout = templateNew.layout;
                panelSelector.append(layout);
            }

            if (isShowFull) {
                panelSelector.find("#tableLayout").show();
                panelSelector.find("#tableLayoutCollapse").hide();
            } else {
                panelSelector.find("#tableLayout").hide();
                panelSelector.find("#tableLayoutCollapse").show();
            }

            if (templateNew.controls) {
                $.each(templateNew.controls, function (key, value) {
                    var option = {};
                    option["Options"] = value;
                    option["isShowFull"] = isShowFull;
                    egov.formtemplate.controls[key].changeView(option);
                });
            }
        }
    };

    egov.formtemplate.tablelayout = {
        addrow: function (isAbove) {
            if ($currentCellSelected) {
                var index = getCellPosition($currentCellSelected).row;
                if (isAbove) {
                    index--;
                } else {
                    index++;
                }
                REDIPS.table.row('tableLayout', 'insert', index < 0 ? 0 : index);
                applyEventTable();
            }
        },
        addcolumn: function (isLeft) {
            if ($currentCellSelected) {
                var index = getCellPosition($currentCellSelected).col;
                if (isLeft) {
                    $('#tableLayout col').eq(index).before('<col/>');
                    index--;
                    if (index < 0) {
                        index = 0;
                    }
                } else {
                    $('#tableLayout col').eq(index).after('<col/>');
                    index++;
                }
                REDIPS.table.column('tableLayout', 'insert', index);
                applyEventTable();
            }
        },
        deleterow: function () {
            if ($currentCellSelected) {
                var allIndex = [];
                $('.' + config.table.cellSelectedClass).each(function (idx, item) {
                    allIndex.push(getCellPosition($(item)).row);
                });
                allIndex = _.sortBy(_.uniq(allIndex), function (item) { return item; });
                for (var i = allIndex.length - 1; i >= 0; i--) {
                    REDIPS.table.row('tableLayout', 'delete', allIndex[i]);
                }
                applyEventTable();
                var controls = {};
                $('#tableLayout td').children().each(function () {
                    var id = $(this).attr('id');
                    controls[id] = allControls.controls[id];
                });
                allControls.controls = controls;
            }
        },
        deletecolumn: function () {
            if ($currentCellSelected) {
                var allIndex = [];
                $('.' + config.table.cellSelectedClass).each(function (idx, item) {
                    allIndex.push(getCellPosition($(item)).col);
                });
                allIndex = _.sortBy(_.uniq(allIndex), function (item) { return item; });
                for (var i = allIndex.length - 1; i >= 0; i--) {
                    REDIPS.table.column('tableLayout', 'delete', allIndex[i]);
                    var $col = $('#tableLayout col').eq(allIndex[i]);
                    if ($col.length > 0) {
                        $col.remove();
                    }
                }
                applyEventTable();
                var controls = {};
                $('#tableLayout td').children().each(function () {
                    var id = $(this).attr('id');
                    controls[id] = allControls.controls[id];
                });
                allControls.controls = controls;
            }
        },
        splitHorizon: function () {
            REDIPS.table.split('h', 'tableLayout');
            applyEventTable();
        },
        splitVertical: function () {
            REDIPS.table.split('v', 'tableLayout');
            applyEventTable();
        },
        mergeCell: function () {
            REDIPS.table.merge('h', false, 'tableLayout');
            REDIPS.table.merge('v', true, 'tableLayout');
        }
    };
    egov.formtemplate.controls = {
        compendium: {
            name: 'Trích yếu',
            add: function (value) {
                if (value) {
                    allControls.controls.compendium = value;
                    createTextAreaControl('compendium', 'Trích yếu', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.compendium) {
                        allControls.controls.compendium = { position: getCellPosition($currentCellSelected), disable: false, height: 40, widthLabel: config.controls.defaultWidthLabel };
                        createTextAreaControl('compendium', 'Trích yếu', allControls.controls.compendium).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextAreaControl('wrapCompendium', value);
            },
            changeView: function (value) {
                toggleControl("wrapCompendium", value)
            }
        },
        organization: {
            name: 'Cơ quan ban hành',
            add: function (value) {
                if (value) {
                    allControls.controls.organization = value;
                    createTextControl('organization', 'Cơ quan ban hành', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.organization) {
                        allControls.controls.organization = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('organization', 'Cơ quan ban hành', allControls.controls.organization).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapOrganization', value);
            },
            changeView: function (value) {
                toggleControl("wrapOrganization", value)
            }
        },
        docCode: {
            name: 'Số/ký hiệu',
            add: function (value) {
                if (value) {
                    allControls.controls.docCode = value;
                    createTextControl('docCode', 'Số/ký hiệu', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.docCode) {
                        allControls.controls.docCode = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('docCode', 'Số/ký hiệu', allControls.controls.docCode).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDocCode', value);
            },
            changeView: function (value) {
                toggleControl("wrapDocCode", value)
            }
        },
        docCodeHSMC: {
            name: 'Mã hồ sơ',
            add: function (value) {
                if (value) {
                    allControls.controls.docCodeHSMC = value;
                    createTextControl('docCodeHSMC', 'Mã hồ sơ', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.docCodeHSMC) {
                        allControls.controls.docCodeHSMC = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('docCodeHSMC', 'Mã hồ sơ', allControls.controls.docCodeHSMC).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDocCode', value);
            },
            changeView: function (value) {
                toggleControl("wrapDocCode", value)
            }
        },
        comment: {
            name: 'Ý kiến xử lý',
            add: function (value) {
                var commentControl;
                if (value) {
                    allControls.controls.comment = value;
                    commentControl = createTextAreaControl('comment', 'Ý kiến xử lý', value);
                    commentControl.find('.' + config.controls.labelClass).append('<input type="button" value="Yk thường dùng" /><input type="button" value="Dự kiến chuyển" />');
                } else {
                    if ($currentCellSelected && !allControls.controls.comment) {
                        allControls.controls.comment = { position: getCellPosition($currentCellSelected), disable: false, height: 80, widthLabel: config.controls.defaultWidthLabel };
                        commentControl = createTextAreaControl('comment', 'Ý kiến xử lý', allControls.controls.comment).mousedown();
                        commentControl.find('.' + config.controls.labelClass).append('<input type="button" value="Yk thường dùng" /><input type="button" value="Dự kiến chuyển" />');
                    }
                }
            },
            view: function (value) {
                viewTextAreaControl('wrapComment', value);
            },
            changeView: function (value) {
                toggleControl("wrapComment", value)
            }
        },
        dateResponse: {
            name: 'Hồi báo',
            add: function (value) {
                if (value) {
                    allControls.controls.dateResponse = value;
                    createTextControl('dateResponse', 'Hồi báo', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateArrived) {
                        allControls.controls.dateResponse = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateResponse', 'Hồi báo', allControls.controls.dateResponse).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateResponse', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateResponse", value)
            }
        },
        doctype: {
            name: 'Loại văn bản',
            add: function (value) {
                if (value) {
                    allControls.controls.doctype = value;
                    createSelectControl('doctype', 'Loại văn bản', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.doctype) {
                        allControls.controls.doctype = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('doctype', 'Loại văn bản', allControls.controls.doctype).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapDoctype', value);
            },
            changeView: function (value) {
                toggleControl("wrapDoctype", value)
            }
        },
        dateArrived: {
            name: 'Ngày đến',
            add: function (value) {
                if (value) {
                    allControls.controls.dateArrived = value;
                    createTextControl('dateArrived', 'Ngày đến', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateArrived) {
                        allControls.controls.dateArrived = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateArrived', 'Ngày đến', allControls.controls.dateArrived).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateArrived', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateArrived", value)
            }
        },
        datePublished: {
            name: 'Ngày văn bản',
            add: function (value) {
                if (value) {
                    allControls.controls.datePublished = value;
                    createTextControl('datePublished', 'Ngày văn bản', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.datePublished) {
                        allControls.controls.datePublished = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('datePublished', 'Ngày văn bản', allControls.controls.datePublished).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDatePublished', value);
            },
            changeView: function (value) {
                toggleControl("wrapDatePublished", value)
            }
        },
        category: {
            name: 'Hình thức văn bản',
            add: function (value) {
                if (value) {
                    allControls.controls.category = value;
                    createSelectControl('category', 'Hình thức văn bản', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.category) {
                        allControls.controls.category = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('category', 'Hình thức văn bản', allControls.controls.category).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapCategory', value);
            },
            changeView: function (value) {
                toggleControl("wrapCategory", value)
            }
        },
        store: {
            name: 'Sổ hồ sơ',
            add: function (value) {
                if (value) {
                    allControls.controls.store = value;
                    createSelectControl('store', 'Sổ hồ sơ', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.store) {
                        allControls.controls.store = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('store', 'Sổ hồ sơ', allControls.controls.store).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapStore', value);
            },
            changeView: function (value) {
                toggleControl("wrapStore", value)
            }
        },
        inOutCode: {
            name: 'Số văn bản đến',
            add: function (value) {
                if (value) {
                    allControls.controls.inOutCode = value;
                    createTextControl('inOutCode', 'Số văn bản đến', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.inOutCode) {
                        allControls.controls.inOutCode = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('inOutCode', 'Số văn bản đến', allControls.controls.inOutCode).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapInOutCode', value);
            },
            changeView: function (value) {
                toggleControl("wrapInOutCode", value)
            }
        },
        urgent: {
            name: 'Độ khẩn',
            add: function (value) {
                if (value) {
                    allControls.controls.urgent = value;
                    createSelectControl('urgent', 'Độ khẩn', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.urgent) {
                        allControls.controls.urgent = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('urgent', 'Độ khẩn', allControls.controls.urgent).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapUrgent', value);
            },
            changeView: function (value) {
                toggleControl("wrapUrgent", value)
            }
        },
        security: {
            name: 'Độ mật',
            add: function (value) {
                if (value) {
                    allControls.controls.security = value;
                    createSelectControl('security', 'Độ mật', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.security) {
                        allControls.controls.security = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('security', 'Độ mật', allControls.controls.security).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapSecurity', value);
            },
            changeView: function (value) {
                toggleControl("wrapSecurity", value)
            }
        },
        totalPage: {
            name: 'Số trang',
            add: function (value) {
                if (value) {
                    allControls.controls.totalPage = value;
                    createSelectControl('totalPage', 'Số trang', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.totalPage) {
                        allControls.controls.totalPage = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('totalPage', 'Số trang', allControls.controls.totalPage).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapTotalPage', value);
                $wrap.find('select').attr('data-val', value.validate ? 'true' : 'false');
            },
            changeView: function (value) {
                toggleControl("wrapTotalPage", value)
            }
        },
        docField: {
            name: 'Lĩnh vực',
            add: function (value) {
                if (value) {
                    allControls.controls.docField = value;
                    createSelectControl('docField', 'Lĩnh vực', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.docField) {
                        allControls.controls.docField = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('docField', 'Lĩnh vực', allControls.controls.docField).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapDocField', value, false);
                $wrap.find('input[type=hidden]').attr('data-val', value.validate ? 'true' : 'false');
            },
            changeView: function (value) {
                toggleControl("wrapDocField", value)
            }
        },
        keyword: {
            name: 'Từ khóa',
            add: function (value) {
                if (value) {
                    allControls.controls.keyword = value;
                    createSelectControl('keyword', 'Từ khóa', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.keyword) {
                        allControls.controls.keyword = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('keyword', 'Từ khóa', allControls.controls.keyword).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapKeyword', value, false);
                $wrap.find('input[type=hidden]').attr('data-val', value.validate ? 'true' : 'false');
            },
            changeView: function (value) {
                toggleControl("wrapKeyword", value)
            }
        },
        sendtype: {
            name: 'Hình thức gửi',
            add: function (value) {
                if (value) {
                    allControls.controls.sendtype = value;
                    createSelectControl('sendtype', 'Hình thức gửi', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.sendtype) {
                        allControls.controls.sendtype = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('sendtype', 'Hình thức gửi', allControls.controls.sendtype).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapSendType', value);
                $wrap.find('select').attr('data-val', value.validate ? 'true' : 'false');
            },
            changeView: function (value) {
                toggleControl("wrapSendType", value)
            }
        },
        inOutPlace: {
            name: 'Đơn vị',
            add: function (value) {
                if (value) {
                    allControls.controls.inOutPlace = value;
                    createSelectControl('inOutPlace', 'Đơn vị', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.inOutPlace) {
                        allControls.controls.inOutPlace = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('inOutPlace', 'Đơn vị', allControls.controls.inOutPlace).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapInOutPlace', value);
                $wrap.find('select').attr('data-val', value.validate ? 'true' : 'false');
            },
            changeView: function (value) {
                toggleControl("wrapInOutPlace", value)
            }
        },
        dateAppointed: {
            name: 'Thời hạn xử lý',
            add: function (value) {
                if (value) {
                    allControls.controls.dateAppointed = value;
                    createTextControl('dateAppointed', 'Thời hạn xử lý', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateAppointed) {
                        allControls.controls.dateAppointed = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateAppointed', 'Thời hạn xử lý', allControls.controls.dateAppointed).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateAppointed', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateAppointed", value)
            }
        },
        dateOverdue: {
            name: getResource("egov.resources.formtemplate.dateOverdue"),
            add: function (value) {
                if (value) {
                    allControls.controls.dateOverdue = value;
                    createTextControl('dateOverdue', getResource("egov.resources.formtemplate.dateOverdue"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateOverdue) {
                        allControls.controls.dateOverdue = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateOverdue', getResource("egov.resources.formtemplate.dateOverdue"), allControls.controls.dateOverdue).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateOverdue', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateOverdue", value)
            }
        },
        dateAppointedHsmc: {
            name: 'Thời hạn xử lý',
            add: function (value) {
                var $wrap;
                if (value) {
                    allControls.controls.dateAppointedHsmc = value;
                    $wrap = createTextControl('dateAppointedHsmc', 'Thời hạn xử lý', value);
                    $wrap.children('.field').append('<div style="display:inline-block;float:right"><span style="margin-right: 10px;">Số ngày thụ lý</span><select style="width:50px"><option value="1">1</option></select>');
                    $wrap.find('input').width('50%');
                } else {
                    if ($currentCellSelected && !allControls.controls.dateAppointedHsmc) {
                        allControls.controls.dateAppointedHsmc = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        $wrap = createTextControl('dateAppointedHsmc', 'Thời hạn xử lý', allControls.controls.dateAppointedHsmc).mousedown();
                        $wrap.children('.field').append('<div style="display:inline-block;float:right"><span style="margin-right: 10px;">Số ngày thụ lý</span><select style="width:50px"><option value="1">1</option></select>');
                        $wrap.find('input').width('50%');
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateAppointed', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateAppointed", value)
            }
        },
        dateResponse: {
            name: 'Hồi báo',
            add: function (value) {
                var $wrap;
                if (value) {
                    allControls.controls.dateResponse = value;
                    $wrap = createTextControl('dateResponse', 'Hồi báo', value);
                    $wrap.find('input').css({ width: '50%', 'float': 'right' }).before('<div style="display:inline-block;margin-top:4px"><input type="checkbox" />&nbsp;Yêu cầu hồi báo</div>');
                } else {
                    if ($currentCellSelected && !allControls.controls.dateResponse) {
                        allControls.controls.dateResponse = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        $wrap = createTextControl('dateResponse', 'Hồi báo', allControls.controls.dateResponse).mousedown();
                        $wrap.find('input').css({ width: '50%', 'float': 'right' }).before('<div style="display:inline-block;margin-top:4px"><input type="checkbox" />&nbsp;Yêu cầu hồi báo</div>');
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateResponse', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateResponse", value)
            }
        },
        citizenName: {
            name: 'Tên công dân',
            add: function (value) {
                if (value) {
                    allControls.controls.citizenName = value;
                    createTextControl('citizenName', 'Tên công dân', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.citizenName) {
                        allControls.controls.citizenName = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('citizenName', 'Tên công dân', allControls.controls.citizenName).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapCitizenName', value);
            },
            changeView: function (value) {
                toggleControl("wrapCitizenName", value)
            }
        },
        address: {
            name: 'Địa chỉ',
            add: function (value) {
                if (value) {
                    allControls.controls.address = value;
                    createTextControl('address', 'Địa chỉ', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.address) {
                        allControls.controls.address = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('address', 'Địa chỉ', allControls.controls.address).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapAddress', value);
            },
            changeView: function (value) {
                toggleControl("wrapAddress", value)
            }
        },
        phone: {
            name: 'Di động',
            add: function (value) {
                if (value) {
                    allControls.controls.phone = value;
                    createTextControl('phone', 'Di động', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.phone) {
                        allControls.controls.phone = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('phone', 'Di động', allControls.controls.phone).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapPhone', value);
            },
            changeView: function (value) {
                toggleControl("wrapPhone", value)
            }
        },
        papers: {
            name: 'Giấy tờ',
            add: function (value) {
                if (value) {
                    allControls.controls.papers = value;
                    createPaperFeeControl('papers', 'Giấy tờ', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.papers) {
                        allControls.controls.papers = { position: getCellPosition($currentCellSelected), widthLabel: config.controls.defaultWidthLabel };
                        createPaperFeeControl('papers', 'Giấy tờ', allControls.controls.papers).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapPapers', value);
            },
            changeView: function (value) {
                toggleControl("wrapPapers", value)
            }
        },
        identityCard: {
            name: 'CMND',
            add: function (value) {
                if (value) {
                    allControls.controls.identityCard = value;
                    createTextControl('identityCard', 'CMND', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.identityCard) {
                        allControls.controls.identityCard = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('identityCard', 'CMND', allControls.controls.identityCard).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapIdentityCard', value);
            },
            changeView: function (value) {
                toggleControl("wrapIdentityCard", value)
            }
        },
        email: {
            name: 'Email',
            add: function (value) {
                if (value) {
                    allControls.controls.email = value;
                    createTextControl('email', 'Email', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.email) {
                        allControls.controls.email = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('email', 'Email', allControls.controls.email).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapEmail', value);
            },
            changeView: function (value) {
                toggleControl("wrapEmail", value)
            }
        },
        commune: {
            name: 'Xã phường',
            add: function (value) {
                if (value) {
                    allControls.controls.commune = value;
                    createTextControl('commune', 'Xã phường', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.commune) {
                        allControls.controls.commune = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('commune', 'Xã phường', allControls.controls.commune).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapCommune', value);
            },
            changeView: function (value) {
                toggleControl("wrapCommune", value)
            }
        },
        fees: {
            name: 'Lệ phí',
            add: function (value) {
                if (value) {
                    allControls.controls.fees = value;
                    createPaperFeeControl('fees', 'Lệ phí', value);
                } else {
                    if ($currentCellSelected && !allControls.controls.fees) {
                        allControls.controls.fees = { position: getCellPosition($currentCellSelected), widthLabel: config.controls.defaultWidthLabel };
                        createPaperFeeControl('fees', 'Lệ phí', allControls.controls.fees).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapFees', value);
            },
            changeView: function (value) {
                toggleControl("wrapFees", value)
            }
        },
        documentRelation: {
            name: 'Văn bản liên quan',
            add: function (value) {
                if (value) {
                    allControls.controls.documentRelation = value;
                    createDocRelationControl(value);
                } else {
                    if ($currentCellSelected && !allControls.controls.documentRelation) {
                        allControls.controls.documentRelation = { position: getCellPosition($currentCellSelected), height: 80 };
                        createDocRelationControl(allControls.controls.documentRelation).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = $panelFormSelector.find('#wrapDocumentRelation');
                $wrap.appendTo(getCellByPosition('tableLayout', value.position));
                $("#tblRelations").grid({
                    isResizeColumn: true,
                    isFixHeightContent: true,
                    height: value.height ? value.height : 80
                });
            },
            changeView: function (value) {
                toggleControl("wrapDocumentRelation", value)
            }
        },

        typeReturn: {
            name: "Hình thức trả hồ sơ",
            add: function (value) {
                if (value) {
                    allControls.controls.typeReturn = value;
                    createTextControl('typeReturn', getResource("egov.resources.formtemplate.typeReturn"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.typeReturn) {
                        allControls.controls.typeReturn = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('typeReturn', getResource("egov.resources.formtemplate.typeReturn"), allControls.controls.typeReturn).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapReturnType', value);
            },
            changeView: function (value) {
                toggleControl("wrapReturnType", value)
            }
        },
        attachment: {
            name: 'Tệp đính kèm',
            add: function (value) {
                if (value) {
                    allControls.controls.attachment = value;
                    createAttachmentControl(value);
                } else {
                    if ($currentCellSelected && !allControls.controls.attachment) {
                        allControls.controls.attachment = { position: getCellPosition($currentCellSelected), height: 80 };
                        createAttachmentControl(allControls.controls.attachment).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = $panelFormSelector.find('#wrapAttachment');
                $wrap.appendTo(getCellByPosition('tableLayout', value.position));
                if (value.height) {
                    $("#tblFiles").grid({
                        isResizeColumn: true,
                        isFixHeightContent: true,
                        height: value.height ? value.height : 80
                    });
                }
            },
            changeView: function (value) {
                toggleControl("wrapAttachment", value)
            }
        },
        content: {
            name: getResource("egov.resources.formtemplate.content"),
            add: function (value) {
                if (value) {
                    allControls.controls.content = value;
                    createTextControl('content', getResource("egov.resources.formtemplate.content"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.content) {
                        allControls.controls.content = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('content', getResource("egov.resources.formtemplate.content"), allControls.controls.content).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextAreaControl('wrapContent', value);
            },
            changeView: function (value) {
                toggleControl("wrapContent", value)
            }
        },
        hasauthentication: {
            name: getResource("egov.resources.formtemplate.hasauthentication.name"),
            add: function (value) {
                if (value) {
                    allControls.controls.hasauthentication = value;
                    createSelectControl('hasauthentication', getResource("egov.resources.formtemplate.hasauthentication.name"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.hasauthentication) {
                        allControls.controls.hasauthentication = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('hasauthentication', getResource("egov.resources.formtemplate.hasauthentication.name"), allControls.controls.hasauthentication).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapHasAuthentication', value);
            },
            changeView: function (value) {
                toggleControl("wrapHasAuthentication", value)
            }
        },
        original: {
            name: getResource("egov.resources.formtemplate.original.name"),
            add: function (value) {
                if (value) {
                    allControls.controls.original = value;
                    createSelectControl('original', getResource("egov.resources.formtemplate.original.name"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.original) {
                        allControls.controls.original = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('original', getResource("egov.resources.formtemplate.original.name"), allControls.controls.original).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapOriginal', value);
            },
            changeView: function (value) {
                toggleControl("wrapOriginal", value)
            }
        },
        iscomplain: {
            name: getResource("egov.resources.formtemplate.iscomplain.name"),
            add: function (value) {
                if (value) {
                    allControls.controls.iscomplain = value;
                    createSelectControl('iscomplain', getResource("egov.resources.formtemplate.iscomplain.name"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.iscomplain) {
                        allControls.controls.iscomplain = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('iscomplain', getResource("egov.resources.formtemplate.iscomplain.name"), allControls.controls.iscomplain).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapIsComplain', value);
            },
            changeView: function (value) {
                toggleControl("wrapIsComplain", value)
            }
        },
        dateCreated: {
            name: getResource("egov.resources.formtemplate.dateCreated"),
            add: function (value) {
                if (value) {
                    allControls.controls.dateCreated = value;
                    createTextControl('dateCreated', getResource("egov.resources.formtemplate.dateCreated"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateCreated) {
                        allControls.controls.dateCreated = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateCreated', getResource("egov.resources.formtemplate.dateCreated"), allControls.controls.dateCreated).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapDateCreated', value);
            },
            changeView: function (value) {
                toggleControl("wrapDateCreated", value)
            }
        },

        delayReason: {
            name: getResource("egov.resources.formtemplate.delayReason"),
            add: function (value) {
                if (value) {
                    allControls.controls.delayReason = value;
                    createTextControl('delayReason', getResource("egov.resources.formtemplate.delayReason"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.delayReason) {
                        allControls.controls.delayReason = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('delayReason', getResource("egov.resources.formtemplate.delayReason"), allControls.controls.delayReason).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapDelayReason', value);
            },
            changeView: function (value) {
                toggleControl("wrapDelayReason", value)
            }
        },
        note: {
            name: getResource("egov.resources.formtemplate.note"),
            add: function (value) {
                if (value) {
                    allControls.controls.note = value;
                    createTextControl('note', getResource("egov.resources.formtemplate.note"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.note) {
                        allControls.controls.note = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('note', getResource("egov.resources.formtemplate.note"), allControls.controls.note).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapNote', value);
            },
            changeView: function (value) {
                toggleControl("wrapNote", value)
            }
        },


        typeReturn: {
            name: "Hình thức trả hồ sơ",
            add: function (value) {
                if (value) {
                    allControls.controls.typeReturn = value;
                    createTextControl('typeReturn', getResource("egov.resources.formtemplate.typeReturn"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.typeReturn) {
                        allControls.controls.typeReturn = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('typeReturn', getResource("egov.resources.formtemplate.typeReturn"), allControls.controls.typeReturn).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapReturnType', value);
            }
        },

        catalog: {
            name: "Trường dữ liệu động theo lĩnh vực",
            add: function (value) {
                if (value) {
                    allControls.controls.catalog = value;
                    createTextControl('catalog', "catalog", value);
                } else {
                    if ($currentCellSelected && !allControls.controls.catalog) {
                        allControls.controls.catalog = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('catalog', "catalog", allControls.controls.catalog).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapCatalog', value);
            }
        }

    };
})(window.egov = window.egov || {}, window.jQuery, window._);

(function ($) {
    $.fn['inlineStyle'] = function (prop) {
        var styles = $(this).attr("style"),
             value = null;
        styles && styles.split(";").forEach(function (e) {
            var style = e.split(":");
            if ($.trim(style[0]) === prop) {
                value = style[1];
            }
        });
        return value;
    };
})(window.jQuery);