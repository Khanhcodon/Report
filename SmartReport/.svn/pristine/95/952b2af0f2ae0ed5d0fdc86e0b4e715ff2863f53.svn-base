(function (egov, $, _) {
    var config = {
        table: { cellSelectedClass: 'cellSelected', cellHoverClass: 'cellHover' },
        controls: { wrapControlClass: 'wrapControl', defaultWidthLabel: 150, labelClass: 'label-field' }
    };
    var allControls;
    var $panelFormSelector;
    var $currentCellSelected;

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
                    $input.before('<div style="width:100px;display:inline-block">' + getResource("egov.resources.formtemplate.columnwidth") + ':</div>');
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
        $input.before('<div style="width:100px;display:inline-block">' + getResource("egov.resources.formtemplate.brandwidth") + ':</div>');
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
        $input.before('<div style="width:100px;display:inline-block">' + getResource("egov.resources.formtemplate.height") + ':</div>');
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
        $input.before('<div style="width:100px;display:inline-block">' + getResource("egov.resources.formtemplate.disable") + ':</div>');
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
        $input.before('<div style="width:100px;display:inline-block">' + getResource("egov.resources.formtemplate.verifydata") + ':</div>');
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
        $wrap.width('100%').append('<div><div class="grid"><div class="grid-header"><div class="grid-header-wrap"><table class="table-main" style="width:100%;"><thead><tr><th class="header">'
            + getResource("egov.resources.formtemplate.compendium") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.doccode") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.usercomment") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.createdate1") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.category") + '</th></tr> </thead></table></div></div><div class="grid-content" style="overflow:hidden; height:' + setting.height + 'px; outline:none;"><table class="table-main" style="100%"><tbody></tbody></table></div></div></div>');
        $wrap.mousedown(function () {
            var $heightSetting = createSettingHeight('documentRelation', $wrap.find('.grid-content'), setting.height);
            var $delete = createButtonDelete('documentRelation', $wrap);
            $('#settingControl').html('').append($heightSetting).append($delete);
        });
        return $wrap;
    };

    var createAttachmentControl = function (setting) {
        var $wrap = createWrapControl('attachment', setting.position);
        $wrap.width('100%').append('<div><div class="grid"><div class="grid-header"><div class="grid-header-wrap"><table class="table-main" style="width:100%;"><thead><tr><th class="header">'
            + getResource("egov.resources.formtemplate.filename") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.filesize") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.fileversion") + '</th><th class="header">'
            + getResource("egov.resources.formtemplate.lastupdatefile") + '</th></tr> </thead></table></div></div><div class="grid-content" style="overflow:hidden; height:' + setting.height + 'px; outline:none;"><table class="table-main" style="100%"><tbody></tbody></table></div></div></div>');
        $wrap.mousedown(function () {
            var $heightSetting = createSettingHeight('attachment', $wrap.find('.grid-content'), setting.height);
            var $delete = createButtonDelete('attachment', $wrap);
            $('#settingControl').html('').append($heightSetting).append($delete);
        });
        return $wrap;
    };

    var createReturnTypeControl = function (key, name, setting) {
        var $wrap = createWrapControl(key, setting.position);
        var $label = createLabel(name, key, setting.widthLabel);
        var $checkbox = $('<div class="' + config.controls.labelClass + '" style="width: 20px;margin-left: 0;"><input type="radio" value="0" style="float:left"/> '+$label.html()+' <input type="radio" value="1" /> Trực tiếp</div>');
        var $field = $('<div class="field" style="margin-right:0"></div>').append($checkbox);
        $wrap.append($label).append($field);
        $wrap.mousedown(function () {
            var $widthLabelSetting = createSettingWidthLabel(key, setting.widthLabel);
            var $delete = createButtonDelete(key, $wrap);
            $('#settingControl').html('').append($widthLabelSetting).append($delete);
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
        return $('#' + idTable + ' > tbody > tr:eq(' + position.row + ') td:eq(' + position.col + ')');
    };

    var viewTextAreaControl = function (idwrap, value) {
        var $wrap = $('#' + idwrap);
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
        $wrap.appendTo(getCellByPosition('tableLayout', value.position));
        return $wrap;
    };

    var viewTextControl = function (idwrap, value) {
        var $wrap = $('#' + idwrap);
        if (value.widthLabel) {
            $wrap.children('.' + config.controls.labelClass).width(value.widthLabel);
        }
        var $text = $wrap.find('input[type=text]');
        if (value.disable) {
            $text.attr('disabled', 'disabled');
            $text.after('<input name="' + $text.attr('name') + '" type="hidden" value="' + $text.val() + '" />');
        }
        $wrap.appendTo(getCellByPosition('tableLayout', value.position));
        return $wrap;
    };

    var viewSelectControl = function (idwrap, value, addHidden) {
        if (typeof addHidden !== 'boolean') {
            addHidden = true;
        }
        var $wrap = $('#' + idwrap);
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
        $wrap.appendTo(getCellByPosition('tableLayout', value.position));
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
                        try {
                            egov.formtemplate.controls[key].add(value);
                        }
                        catch (e) {
                            console.log("egov.formtemplate key (" + key + ") not found");
                        }
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
            if (panelSelector) {
                if (panelSelector instanceof jQuery) {
                    if (panelSelector.length > 0) {
                        $panelSelector = panelSelector;
                    }
                } else if (typeof panelSelector === 'string') {
                    $panelSelector = $(panelSelector);
                } else {
                    throw getResource("egov.resources.formtemplate.panelselectorrequire");
                }
                if (template) {
                    $panelSelector.append(template.layout);
                    $('#tableLayout').attr('cellpadding', '3');
                    if (template.controls) {
                        $.each(template.controls, function (key, value) {
                            try {
                                egov.formtemplate.controls[key].view(value);
                            }
                            catch (e) {
                                console.log("egov.formtemplate key (" + key + ") not found");
                            }
                        });
                    }
                }
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
            name: getResource("egov.resources.formtemplate.compendium"),
            add: function (value) {
                if (value) {
                    allControls.controls.compendium = value;
                    createTextAreaControl('compendium', getResource("egov.resources.formtemplate.compendium"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.compendium) {
                        allControls.controls.compendium = { position: getCellPosition($currentCellSelected), disable: false, height: 40, widthLabel: config.controls.defaultWidthLabel };
                        createTextAreaControl('compendium', getResource("egov.resources.formtemplate.compendium"), allControls.controls.compendium).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextAreaControl('wrapCompendium', value);
            }
        },
        organization: {
            name: getResource("egov.resources.formtemplate.publicoffice"),
            add: function (value) {
                if (value) {
                    allControls.controls.organization = value;
                    createTextControl('organization', getResource("egov.resources.formtemplate.publicoffice"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.organization) {
                        allControls.controls.organization = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('organization', getResource("egov.resources.formtemplate.publicoffice"), allControls.controls.organization).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapOrganization', value);
            }
        },
        docCode: {
            name: getResource("egov.resources.formtemplate.doccode"),
            add: function (value) {
                if (value) {
                    allControls.controls.docCode = value;
                    createTextControl('docCode', getResource("egov.resources.formtemplate.doccode"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.docCode) {
                        allControls.controls.docCode = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('docCode', getResource("egov.resources.formtemplate.doccode"), allControls.controls.docCode).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDocCode', value);
            }
        },
        docCodeHSMC: {
            name: getResource("egov.resources.formtemplate.doccode1"),
            add: function (value) {
                if (value) {
                    allControls.controls.docCodeHSMC = value;
                    createTextControl('docCodeHSMC', getResource("egov.resources.formtemplate.doccode1"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.docCodeHSMC) {
                        allControls.controls.docCodeHSMC = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('docCodeHSMC', getResource("egov.resources.formtemplate.doccode1"), allControls.controls.docCodeHSMC).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDocCode', value);
            }
        },
        comment: {
            name: getResource("egov.resources.formtemplate.comment"),
            add: function (value) {
                var commentControl;
                if (value) {
                    allControls.controls.comment = value;
                    commentControl = createTextAreaControl('comment', getResource("egov.resources.formtemplate.comment"), value);
                    commentControl.find('.' + config.controls.labelClass).append('<input type="button" value="+" /><input type="button" value="' + getResource("egov.resources.formtemplate.insertanticipate") + '" />');
                } else {
                    if ($currentCellSelected && !allControls.controls.comment) {
                        allControls.controls.comment = { position: getCellPosition($currentCellSelected), disable: false, height: 90, widthLabel: config.controls.defaultWidthLabel };
                        commentControl = createTextAreaControl('comment', getResource("egov.resources.formtemplate.comment"), allControls.controls.comment).mousedown();
                        commentControl.find('.' + config.controls.labelClass).append('<input type="button" value="+" /><input type="button" value="' + getResource("egov.resources.formtemplate.insertanticipate") + '" />');
                    }
                }
            },
            view: function (value) {
                viewTextAreaControl('wrapComment', value);
            }
        },
        doctype: {
            name: getResource("egov.resources.formtemplate.doctype"),
            add: function (value) {
                if (value) {
                    allControls.controls.doctype = value;
                    createSelectControl('doctype', getResource("egov.resources.formtemplate.doctype"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.doctype) {
                        allControls.controls.doctype = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('doctype', getResource("egov.resources.formtemplate.doctype"), allControls.controls.doctype).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapDoctype', value);
            }
        },
        dateArrived: {
            name: getResource("egov.resources.formtemplate.datearrived"),
            add: function (value) {
                if (value) {
                    allControls.controls.dateArrived = value;
                    createTextControl('dateArrived', getResource("egov.resources.formtemplate.datearrived"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateArrived) {
                        allControls.controls.dateArrived = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateArrived', getResource("egov.resources.formtemplate.datearrived"), allControls.controls.dateArrived).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateArrived', value);
            }
        },
        datePublished: {
            name: getResource("egov.resources.formtemplate.datepublished"),
            add: function (value) {
                if (value) {
                    allControls.controls.datePublished = value;
                    createTextControl('datePublished', getResource("egov.resources.formtemplate.datepublished"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.datePublished) {
                        allControls.controls.datePublished = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('datePublished', getResource("egov.resources.formtemplate.datepublished"), allControls.controls.datePublished).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDatePublished', value);
            }
        },
        category: {
            name: getResource("egov.resources.formtemplate.category"),
            add: function (value) {
                if (value) {
                    allControls.controls.category = value;
                    createSelectControl('category', getResource("egov.resources.formtemplate.category"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.category) {
                        allControls.controls.category = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('category', getResource("egov.resources.formtemplate.category"), allControls.controls.category).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapCategory', value);
            }
        },
        store: {
            name: getResource("egov.resources.formtemplate.store"),
            add: function (value) {
                if (value) {
                    allControls.controls.store = value;
                    createSelectControl('store', getResource("egov.resources.formtemplate.store"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.store) {
                        allControls.controls.store = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('store', getResource("egov.resources.formtemplate.store"), allControls.controls.store).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapStore', value);
            }
        },
        inOutCode: {
            name: getResource("egov.resources.formtemplate.inoutcode"),
            add: function (value) {
                if (value) {
                    allControls.controls.inOutCode = value;
                    createTextControl('inOutCode', getResource("egov.resources.formtemplate.inoutcode"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.inOutCode) {
                        allControls.controls.inOutCode = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('inOutCode', getResource("egov.resources.formtemplate.inoutcode"), allControls.controls.inOutCode).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapInOutCode', value);
            }
        },
        urgent: {
            name: getResource("egov.resources.formtemplate.urgent.name"),
            add: function (value) {
                if (value) {
                    allControls.controls.urgent = value;
                    createSelectControl('urgent', getResource("egov.resources.formtemplate.urgent.name"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.urgent) {
                        allControls.controls.urgent = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('urgent', getResource("egov.resources.formtemplate.urgent.name"), allControls.controls.urgent).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapUrgent', value);
            }
        },
        security: {
            name: getResource("egov.resources.formtemplate.securityid.name"),
            add: function (value) {
                if (value) {
                    allControls.controls.security = value;
                    createSelectControl('security', getResource("egov.resources.formtemplate.securityid.name"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.security) {
                        allControls.controls.security = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('security', getResource("egov.resources.formtemplate.securityid.name"), allControls.controls.security).mousedown();
                    }
                }
            },
            view: function (value) {
                viewSelectControl('wrapSecurity', value);
            }
        },
        totalPage: {
            name: getResource("egov.resources.formtemplate.totalPage"),
            add: function (value) {
                if (value) {
                    allControls.controls.totalPage = value;
                    createSelectControl('totalPage', getResource("egov.resources.formtemplate.totalPage"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.totalPage) {
                        allControls.controls.totalPage = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('totalPage', getResource("egov.resources.formtemplate.totalPage"), allControls.controls.totalPage).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapTotalPage', value);
                $wrap.find('select').attr('data-val', value.validate ? 'true' : 'false');
            }
        },
        docField: {
            name: getResource("egov.resources.formtemplate.docfield"),
            add: function (value) {
                if (value) {
                    allControls.controls.docField = value;
                    createSelectControl('docField', getResource("egov.resources.formtemplate.docfield"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.docField) {
                        allControls.controls.docField = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('docField', getResource("egov.resources.formtemplate.docfield"), allControls.controls.docField).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapDocField', value, false);
                $wrap.find('input[type=hidden]').attr('data-val', value.validate ? 'true' : 'false');
            }
        },
        keyword: {
            name: getResource("egov.resources.formtemplate.keyword"),
            add: function (value) {
                if (value) {
                    allControls.controls.keyword = value;
                    createSelectControl('keyword', getResource("egov.resources.formtemplate.keyword"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.keyword) {
                        allControls.controls.keyword = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('keyword', getResource("egov.resources.formtemplate.keyword"), allControls.controls.keyword).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapKeyword', value, false);
                $wrap.find('input[type=text]').attr('data-val', value.validate ? 'true' : 'false');
            }
        },
        sendtype: {
            name: getResource("egov.resources.formtemplate.sendtype"),
            add: function (value) {
                if (value) {
                    allControls.controls.sendtype = value;
                    createSelectControl('sendtype', getResource("egov.resources.formtemplate.sendtype"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.sendtype) {
                        allControls.controls.sendtype = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('sendtype', getResource("egov.resources.formtemplate.sendtype"), allControls.controls.sendtype).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapSendType', value);
                $wrap.find('select').attr('data-val', value.validate ? 'true' : 'false');
            }
        },
        inOutPlace: {
            name: getResource("egov.resources.formtemplate.inoutplace"),
            add: function (value) {
                if (value) {
                    allControls.controls.inOutPlace = value;
                    createSelectControl('inOutPlace', getResource("egov.resources.formtemplate.inoutplace"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.inOutPlace) {
                        allControls.controls.inOutPlace = { position: getCellPosition($currentCellSelected), disable: false, validate: false, widthLabel: config.controls.defaultWidthLabel };
                        createSelectControl('inOutPlace', getResource("egov.resources.formtemplate.inoutplace"), allControls.controls.inOutPlace).mousedown();
                    }
                }
            },
            view: function (value) {
                var $wrap = viewSelectControl('wrapInOutPlace', value);
                $wrap.find('select').attr('data-val', value.validate ? 'true' : 'false');
            }
        },
        dateAppointed: {
            name: getResource("egov.resources.formtemplate.dateappointed"),
            add: function (value) {
                if (value) {
                    allControls.controls.dateAppointed = value;
                    createTextControl('dateAppointed', getResource("egov.resources.formtemplate.dateappointed"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.dateAppointed) {
                        allControls.controls.dateAppointed = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('dateAppointed', getResource("egov.resources.formtemplate.dateappointed"), allControls.controls.dateAppointed).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateAppointed', value);
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
            }
        },
        dateAppointedHsmc: {
            name: getResource("egov.resources.formtemplate.dateappointed"),
            add: function (value) {
                var $wrap;
                if (value) {
                    allControls.controls.dateAppointedHsmc = value;
                    $wrap = createTextControl('dateAppointedHsmc', getResource("egov.resources.formtemplate.dateappointed"), value);
                    $wrap.children('.field').append('<div style="display:inline-block;float:right"><span style="margin-right: 10px;">' + getResource("egov.resources.formtemplate.receivedays") + '</span><select style="width:50px"><option value="1">1</option></select>');
                    $wrap.find('input').width('50%');
                } else {
                    if ($currentCellSelected && !allControls.controls.dateAppointedHsmc) {
                        allControls.controls.dateAppointedHsmc = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        $wrap = createTextControl('dateAppointedHsmc', getResource("egov.resources.formtemplate.dateappointed"), allControls.controls.dateAppointedHsmc).mousedown();
                        $wrap.children('.field').append('<div style="display:inline-block;float:right"><span style="margin-right: 10px;">' + getResource("egov.resources.formtemplate.receivedays") + '</span><select style="width:50px"><option value="1">1</option></select>');
                        $wrap.find('input').width('50%');
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateAppointed', value);
            }
        },
        dateResponse: {
            name: getResource("egov.resources.formtemplate.dateresponse"),
            add: function (value) {
                var $wrap;
                if (value) {
                    allControls.controls.dateResponse = value;
                    $wrap = createTextControl('dateResponse', getResource("egov.resources.formtemplate.dateresponse"), value);
                    $wrap.find('input').css({ width: '50%', 'float': 'right' }).before('<div style="display:inline-block;margin-top:4px"><input type="checkbox" />&nbsp;' + getResource("egov.resources.formtemplate.requirereport") + '</div>');
                } else {
                    if ($currentCellSelected && !allControls.controls.dateResponse) {
                        allControls.controls.dateResponse = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        $wrap = createTextControl('dateResponse', getResource("egov.resources.formtemplate.dateresponse"), allControls.controls.dateResponse).mousedown();
                        $wrap.find('input').css({ width: '50%', 'float': 'right' }).before('<div style="display:inline-block;margin-top:4px"><input type="checkbox" />&nbsp;' + getResource("egov.resources.formtemplate.requirereport") + '</div>');
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapDateResponse', value);
            }
        },
        citizenName: {
            name: getResource("egov.resources.formtemplate.citizenname"),
            add: function (value) {
                if (value) {
                    allControls.controls.citizenName = value;
                    createTextControl('citizenName', getResource("egov.resources.formtemplate.citizenname"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.citizenName) {
                        allControls.controls.citizenName = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('citizenName', getResource("egov.resources.formtemplate.citizenname"), allControls.controls.citizenName).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapCitizenName', value);
            }
        },
        address: {
            name: getResource("egov.resources.formtemplate.address"),
            add: function (value) {
                if (value) {
                    allControls.controls.address = value;
                    createTextControl('address', getResource("egov.resources.formtemplate.address"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.address) {
                        allControls.controls.address = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('address', getResource("egov.resources.formtemplate.address"), allControls.controls.address).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapAddress', value);
            }
        },
        phone: {
            name: getResource("egov.resources.formtemplate.phone"),
            add: function (value) {
                if (value) {
                    allControls.controls.phone = value;
                    createTextControl('phone', getResource("egov.resources.formtemplate.phone"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.phone) {
                        allControls.controls.phone = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('phone', getResource("egov.resources.formtemplate.phone"), allControls.controls.phone).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapPhone', value);
            }
        },
        papers: {
            name: getResource("egov.resources.formtemplate.docpapers"),
            add: function (value) {
                if (value) {
                    allControls.controls.papers = value;
                    createPaperFeeControl('papers', getResource("egov.resources.formtemplate.docpapers"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.papers) {
                        allControls.controls.papers = { position: getCellPosition($currentCellSelected), widthLabel: config.controls.defaultWidthLabel };
                        createPaperFeeControl('papers', getResource("egov.resources.formtemplate.docpapers"), allControls.controls.papers).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapPapers', value);
            }
        },
        identityCard: {
            name: getResource("egov.resources.formtemplate.identitycard"),
            add: function (value) {
                if (value) {
                    allControls.controls.identityCard = value;
                    createTextControl('identityCard', getResource("egov.resources.formtemplate.identitycard"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.identityCard) {
                        allControls.controls.identityCard = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('identityCard', getResource("egov.resources.formtemplate.identitycard"), allControls.controls.identityCard).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapIdentityCard', value);
            }
        },
        email: {
            name: getResource("egov.resources.formtemplate.email"),
            add: function (value) {
                if (value) {
                    allControls.controls.email = value;
                    createTextControl('email', getResource("egov.resources.formtemplate.email"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.email) {
                        allControls.controls.email = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('email', getResource("egov.resources.formtemplate.email"), allControls.controls.email).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapEmail', value);
            }
        },
        commune: {
            name: getResource("egov.resources.formtemplate.commune"),
            add: function (value) {
                if (value) {
                    allControls.controls.commune = value;
                    createTextControl('commune', getResource("egov.resources.formtemplate.commune"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.commune) {
                        allControls.controls.commune = { position: getCellPosition($currentCellSelected), disable: false, widthLabel: config.controls.defaultWidthLabel };
                        createTextControl('commune', getResource("egov.resources.formtemplate.commune"), allControls.controls.commune).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapCommune', value);
            }
        },
        fees: {
            name: getResource("egov.resources.formtemplate.fees"),
            add: function (value) {
                if (value) {
                    allControls.controls.fees = value;
                    createPaperFeeControl('fees', getResource("egov.resources.formtemplate.fees"), value);
                } else {
                    if ($currentCellSelected && !allControls.controls.fees) {
                        allControls.controls.fees = { position: getCellPosition($currentCellSelected), widthLabel: config.controls.defaultWidthLabel };
                        createPaperFeeControl('fees', getResource("egov.resources.formtemplate.fees"), allControls.controls.fees).mousedown();
                    }
                }
            },
            view: function (value) {
                viewTextControl('wrapFees', value);
            }
        },
        documentRelation: {
            name: getResource("egov.resources.formtemplate.documentrelation"),
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
                var $wrap = $('#wrapDocumentRelation');
                $wrap.appendTo(getCellByPosition('tableLayout', value.position));
                $("#tblRelations").grid({
                    isResizeColumn: true,
                    isFixHeightContent: true,
                    height: value.height ? value.height : 80
                });
            }
        },
        attachment: {
            name: getResource("egov.resources.formtemplate.attachment"),
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
                var $wrap = $('#wrapAttachment');
                $wrap.appendTo(getCellByPosition('tableLayout', value.position));
                if (value.height) {
                    $("#tblFiles").grid({
                        isResizeColumn: true,
                        isFixHeightContent: true,
                        height: value.height ? value.height : 80
                    });
                }
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

        //approver: {
        //    name: getResource("egov.resources.formtemplate.approver"),
        //    add: function (value) {
        //        if (value) {
        //            allControls.controls.approver = value;
        //            createTextControl('approver', getResource("egov.resources.formtemplate.approver"), value);
        //        } else {
        //            if ($currentCellSelected && !allControls.controls.approver) {
        //                allControls.controls.approver = { position: getCellPosition($currentCellSelected), widthLabel: config.controls.defaultWidthLabel };
        //                createTextControl('approver', getResource("egov.resources.formtemplate.docpapers"), allControls.controls.approver).mousedown();
        //            }
        //        }
        //    },
        //    view: function (value) {
        //        viewTextControl('wrapApprover', value);
        //    }
        //},
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