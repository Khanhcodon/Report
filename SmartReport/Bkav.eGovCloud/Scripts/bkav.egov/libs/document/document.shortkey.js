(function ($) {
    var documentBehavior = function () {
        this.printButtonId = "#btnPrint";
        this.transferButtonId = "#btnTransferDoc";
        this.addPaperId = "#addPaper";
        this.addFeeId = "#addFee";
        this.addRelationId = "#addRelations";
        this.addCommentId = "#addLastestComment";
        this.addAttachmentId = "#fileupload";
        this.tabIndexEle = $(".tabIndex");
    };

    documentBehavior.prototype.saveDocument = function (event) {
        if (egov.cshtml.document.isHoso && egov.cshtml.document.documentCopyId == 0) {
            var frame = window.frameElement.name;
            var transfer = new window.parent.egov.document.transfer(0, frame);
            if (confirm('Lưu hồ sơ và tiếp tục tiếp nhận?')) {
                // Gọi khi bấm ctrl + S.
                transfer.transferSpecialCreate(transfer.actionSpecial.TiepNhanHoSoVaTiepTuc);
            }
        }
        event.preventDefault();
        return false;
    };

    documentBehavior.prototype.print = function () {
        $(this.printButtonId).click();
    };

    documentBehavior.prototype.transfer = function () {
        $(this.transferButtonId).click();
    };

    documentBehavior.prototype.addPaper = function () {
        $(this.addPaperId).click();
    };

    documentBehavior.prototype.addFee = function () {
        $(this.addFeeId).click();
    };

    documentBehavior.prototype.addRelation = function () {
        $(this.addRelationId).click();
    };

    documentBehavior.prototype.addComment = function () {
        $(this.addCommentId).click();
    };

    documentBehavior.prototype.addAttachment = function () {
        $(this.addAttachmentId).click();
    };

    var getCellPosition = function ($cell) {
        var colIndex = $cell.parent().children().index($cell);
        var rowIndex = $cell.parent().parent().children().index($cell.parent());
        return { row: rowIndex, col: colIndex };
    };

    var getTotalColumnInRow = function ($cell) {
        return $cell.siblings('td').length + 1;
    };

    var findControlAcceptFocus = function ($element, isReverse) {
        var $controlFocus;
        var $allControl = isReverse ? $($element.find(selectorControlAcceptFocus).get().reverse()) : $element.find(selectorControlAcceptFocus);
        $allControl.each(function () {
            if ($(this).attr('disabled')) {
                return true;
            }
            $controlFocus = $(this);
            return false;
        });
        return $controlFocus;
    };

    var findNextControlInRowRemain = function ($currentCell, isDown) {
        var $nextControl;
        var $nextRow = $currentCell.parent();
        if (isDown) {
            var indexCell = $currentCell.index();
            while ($nextRow.next().length > 0) {
                $nextRow = $nextRow.next();
                var $cellBelow = $nextRow.children('td:eq(' + indexCell + ')');
                if ($cellBelow.length > 0) {
                    $nextControl = findControlAcceptFocus($cellBelow);
                    if ($nextControl) {
                        break;
                    }
                }
                $nextControl = findControlAcceptFocus($nextRow);
                if ($nextControl) {
                    break;
                }
            }
        } else {
            while ($nextRow.next().length > 0) {
                $nextRow = $nextRow.next();
                $nextControl = findControlAcceptFocus($nextRow);
                if ($nextControl) {
                    break;
                }
            }
        }
        return $nextControl;
    };

    var findPrevControlInRowRemain = function ($currentCell, isUp) {
        var $prevControl;
        var $prevRow = $currentCell.parent();
        if (isUp) {
            var indexCell = $currentCell.index();
            while ($prevRow.prev().length > 0) {
                $prevRow = $prevRow.prev();
                var $cellAbove = $prevRow.children('td:eq(' + indexCell + ')');
                if ($cellAbove.length > 0) {
                    $prevControl = findControlAcceptFocus($cellAbove);
                    if ($prevControl) {
                        break;
                    }

                }
                $prevControl = findControlAcceptFocus($prevRow);
                if ($prevControl) {
                    break;
                }
            }
        } else {
            while ($prevRow.prev().length > 0) {
                $prevRow = $prevRow.prev();
                $prevControl = findControlAcceptFocus($prevRow, true);
                if ($prevControl) {
                    break;
                }
            }
        }
        return $prevControl;
    };

    var findNextControlInLiRemain = function ($currentControl) {
        var $nextControl;
        var classNameControl = $currentControl.attr('class').split(' ')[0];
        var $nextLi = $currentControl.closest('li');
        while ($nextLi.next().length > 0) {
            $nextLi = $nextLi.next();
            $nextControl = $nextLi.find('input.' + classNameControl);
            if ($nextControl.length > 0) {
                if (!$nextControl.attr('disabled')) {
                    return $nextControl;
                }
            }
        }
        return findNextControlInRowRemain($currentControl.closest('td'), true);
    };

    var findPrevControlInLiRemain = function ($currentControl) {
        var $prevControl;
        var classNameControl = $currentControl.attr('class').split(' ')[0];
        var $prevLi = $currentControl.closest('li');
        while ($prevLi.prev().length > 0) {
            $prevLi = $prevLi.prev();
            $prevControl = $prevLi.find('input.' + classNameControl);
            if ($prevControl.length > 0) {
                if (!$prevControl.attr('disabled')) {
                    return $prevControl;
                }
            }
        }
        return findPrevControlInRowRemain($currentControl.closest('td'), true);
    };

    var findNextControlInCellRemain = function ($currentCell) {
        var $nextControl;
        var currentPosition = getCellPosition($currentCell);
        var totalColumn = getTotalColumnInRow($currentCell);
        if (currentPosition.col < totalColumn - 1) {
            var $nextCell = $currentCell;
            while ($nextCell.next().length > 0) {
                $nextCell = $nextCell.next();
                $nextControl = findControlAcceptFocus($nextCell);
                if ($nextControl) {
                    return $nextControl;
                }
            }
            $nextControl = findNextControlInRowRemain($currentCell);
        } else {
            $nextControl = findNextControlInRowRemain($currentCell);
        }
        return $nextControl;
    };

    var findPrevControlInCellRemain = function ($currentCell) {
        var $prevControl;
        var currentPosition = getCellPosition($currentCell);
        if (currentPosition.col > 0) {
            var $prevCell = $currentCell;
            while ($prevCell.prev().length > 0) {
                $prevCell = $prevCell.prev();
                $prevControl = findControlAcceptFocus($prevCell);
                if ($prevControl) {
                    return $prevControl;
                }
            }
            $prevControl = findPrevControlInRowRemain($currentCell);
        } else {
            $prevControl = findPrevControlInRowRemain($currentCell);
        }
        return $prevControl;
    };

    var selectorControlAcceptFocus = 'input[type=text],input[type=checkbox],.ui-multiselect,textarea,select:visible';

    documentBehavior.prototype.moveNext = function (e) {
        var $currentElement = $(document.activeElement);
        if ($currentElement.parents('#tableLayout').length === 0) {
            if ($currentElement.parents('.ui-multiselect-checkboxes').length > 0) {
                if ($('#' + $currentElement.attr('name').replace('multiselect_', '')).parents('#tableLayout').length === 0) {
                    return true;
                }
            } else {
                return true;
            }
        }
        var moveNext = function (currentElement) {
            if (currentElement && currentElement.length > 0) {
                $currentElement = currentElement;
            }
            var $nextControl;
            var $currentCell = $currentElement.closest('td');
            var $allControlInCurrentCell = $currentCell.find(selectorControlAcceptFocus);
            var indexCurrentElementInCell = $.inArray($currentElement[0], $allControlInCurrentCell);
            if (indexCurrentElementInCell < $allControlInCurrentCell.length - 1) {
                for (var i = indexCurrentElementInCell + 1; i < $allControlInCurrentCell.length; i++) {
                    var $control = $($allControlInCurrentCell[i]);
                    if ($control.attr('disabled')) {
                        continue;
                    }
                    $nextControl = $control;
                    $nextControl.focus();
                    return;
                }
                $nextControl = findNextControlInCellRemain($currentCell);
            } else {
                $nextControl = findNextControlInCellRemain($currentCell);
            }
            if ($nextControl) {
                $nextControl.focus();
            }
        };
        //Nếu element là input text
        if ($currentElement.is('input[type="text"]')) {
            //Nếu input không có giá trị hoặc vị trí của caret =  độ dài giá trị của element thì chuyển sang control tiêp theo
            if ($currentElement.val().length === 0 || $currentElement.getCursorPosition() === $currentElement.val().length) {
                moveNext();
                return false;
            } else {
                // Ngược lại thì chỉ ấn tab hoặc ấn enter mới chuyển sang control tiếp theo
                if (e.keyCode === 13 || e.keyCode === 9) {
                    moveNext();
                    return false;
                }
            } // Nếu element là textarea
        } else if ($currentElement.is('textarea')) {
            //Nếu textarea không có giá trị hoặc vị trí của caret =  độ dài giá trị của element
            if ($currentElement.val().length == 0 || $currentElement.getCursorPosition() == $currentElement.val().length) {
                //Chỉ ấn tab hoặc ấn enter mới chuyển sang control tiếp theo
                if (e.keyCode === 39 || e.keyCode === 9) {
                    moveNext();
                    return false;
                }
            } else {
                //Ngược lại thì chỉ ấn tab mới chuyển control tiếp theo
                if (e.keyCode === 9) {
                    moveNext();
                    return false;
                } else {
                    return true;
                }
            }
        } else if ($currentElement.is('input[type="checkbox"]') && $currentElement.parents('.ui-multiselect-checkboxes').length > 0) {
            if (e.keyCode === 9) {
                var nameCurrentElement = $currentElement.attr('name');
                if (nameCurrentElement.indexOf('multiselect_') > -1) {
                    moveNext($('#' + nameCurrentElement.replace('multiselect_', '')).siblings('button'));
                    return false;
                }
            }
        } else if ($currentElement.is('select')) {
            if (e.keyCode === 13) {
                if ($currentElement.data("events").mousedown) {
                    // $currentElement.trigger('mousedown');
                    return false;
                }
                return true;
            } else {
                moveNext();
                return false;
            }
        }
        else {
            //Các control khác thì luôn chuyển
            moveNext();
            return false;
        }
        return true;
    };

    documentBehavior.prototype.moveUp = function () {
        var $nextControl;
        var $currentElement = $(document.activeElement);
        if ($currentElement.parents('#tableLayout').length === 0) {
            return true;
        }
        if ($currentElement.is('input[type="text"]') || $currentElement.is('.ui-multiselect')) {
            if ($currentElement.hasClass('fee-name') || $currentElement.hasClass('fee-price') || $currentElement.hasClass('fee-id') || $currentElement.hasClass('paper-name') || $currentElement.hasClass('paper-amount') || $currentElement.hasClass('paper-id')) {
                $nextControl = findPrevControlInLiRemain($currentElement);
            } else if ($currentElement.hasClass('ui-autocomplete-input') && $('.ui-autocomplete:visible').length > 0) {
                return true;
            } else {
                $nextControl = findPrevControlInRowRemain($currentElement.closest('td'), true);
            }
            if ($nextControl) {
                $nextControl.focus();
            }
            return false;
        } else if ($currentElement.is('textarea')) {
            if ($currentElement.val().length === 0 || $currentElement.getCursorPosition() === 0) {
                $nextControl = findPrevControlInRowRemain($currentElement.closest('td'), true);
                if ($nextControl) {
                    $nextControl.focus();
                }
                return false;
            }
        } else if ($currentElement.is('input[type="checkbox"]') && $currentElement.parents('.ui-multiselect-checkboxes').length === 0) {
            if ($currentElement.hasClass('fee-id') || $currentElement.hasClass('paper-id')) {
                $nextControl = findPrevControlInLiRemain($currentElement);
            } else {
                $nextControl = findPrevControlInRowRemain($currentElement.closest('td'), true);
            }
            if ($nextControl) {
                $nextControl.focus();
            }
            return false;
        } else if ($currentElement.is('select')) {
            $nextControl = findPrevControlInRowRemain($currentElement.closest('td'), true);
            if ($nextControl) {
                $nextControl.focus();
            }
            return false;
        }
        return true;
    };

    documentBehavior.prototype.moveDown = function () {
        var $nextControl;
        var $currentElement = $(document.activeElement);
        if ($currentElement.parents('#tableLayout').length === 0) {
            return true;
        }
        if ($currentElement.is('input[type="text"]')) {
            if ($currentElement.hasClass('fee-name') || $currentElement.hasClass('fee-price') || $currentElement.hasClass('paper-name') || $currentElement.hasClass('paper-amount')) {
                $nextControl = findNextControlInLiRemain($currentElement);
            } else {
                if ($currentElement.hasClass('ui-autocomplete-input') && $('.ui-autocomplete:visible').length > 0) {
                    return true;
                }
                $nextControl = findNextControlInRowRemain($currentElement.closest('td'), true);
            }
            if ($nextControl) {
                $nextControl.focus();
            }
            return false;
        } else if ($currentElement.is('textarea')) {
            if ($currentElement.val().length === 0 || $currentElement.getCursorPosition() === $currentElement.val().length) {
                $nextControl = findNextControlInRowRemain($currentElement.closest('td'), true);
                if ($nextControl) {
                    $nextControl.focus();
                }
                return false;
            }
        } else if ($currentElement.is('input[type="checkbox"]') && $currentElement.parents('.ui-multiselect-checkboxes').length === 0) {
            if ($currentElement.hasClass('fee-id') || $currentElement.hasClass('paper-id')) {
                $nextControl = findNextControlInLiRemain($currentElement);
            } else {
                $nextControl = findNextControlInRowRemain($currentElement.closest('td'), true);
            }
            if ($nextControl) {
                $nextControl.focus();
            }
            return false;
        } else if ($currentElement.is('select')) {
            $nextControl = findNextControlInRowRemain($currentElement.closest('td'), true);
            if ($nextControl) {
                $nextControl.focus();
            }
            return false;
        }
        else if ($currentElement.is('.ui-multiselect')) {
            $currentElement.simulate('click');
        }
        return true;
    };

    documentBehavior.prototype.moveLeft = function () {
        var $currentElement = $(document.activeElement);
        if ($currentElement.parents('#tableLayout').length === 0) {
            return true;
        }
        var movePrev = function () {
            var $prevControl;
            var $currentCell = $currentElement.closest('td');
            var $allControlInCurrentCell = $currentCell.find(selectorControlAcceptFocus);
            var indexCurrentElementInCell = $.inArray(document.activeElement, $allControlInCurrentCell);
            if (indexCurrentElementInCell > 0) {
                for (var i = indexCurrentElementInCell - 1; i >= 0; i--) {
                    var $control = $($allControlInCurrentCell[i]);
                    if ($control.attr('disabled')) {
                        continue;
                    }
                    $prevControl = $control;
                    $prevControl.focus();
                    return;
                }
                $prevControl = findPrevControlInCellRemain($currentCell);
            } else {
                $prevControl = findPrevControlInCellRemain($currentCell);
            }
            if ($prevControl) {
                $prevControl.focus();
            }
        };
        //Nếu element là input text hoặc area
        if ($currentElement.is('input[type="text"]') || $currentElement.is('textarea')) {
            //Nếu input không có giá trị hoặc vị trí của caret =  độ dài giá trị của element thì chuyển sang control tiêp theo
            if ($currentElement.val().length === 0 || $currentElement.getCursorPosition() === 0) {
                movePrev();
                return false;
            }
        } else if ($currentElement.is('input[type="checkbox"]')) {
            if ($currentElement.parents('.ui-multiselect-checkboxes').length === 0) {
                //Các control khác thì luôn chuyển
                movePrev();
                return false;
            } else {
                return true;
            }
        } else {
            //Các control khác thì luôn chuyển
            movePrev();
            return false;
        }
        return true;
    };

    documentBehavior.prototype.moveRight = function () {
        return this.moveNext();
    };

    documentBehavior.prototype.checkbox = function () {
        var $currentElement = $(document.activeElement);
        if ($currentElement.is('input[type="checkbox"]')) {
            $currentElement.simulate('click');
        }
    };

    // simply disables save event for chrome, firefox,ie
    $(window).keypress(function (event) {
        if (!(event.which == 115 && event.ctrlKey) && !(event.which == 19)) return true;
        event.preventDefault();
        return false;
    });

    var isCtrl = false;
    $(document).keyup(function (e) {
        e = e || window.event;
        var key = e.keyCode;
        if (key == 17) isCtrl = false;
    }).keydown(function (e) {
        e = e || window.event;
        var key = e.keyCode;
        if (key == 17) isCtrl = true;
        var docBehavior = new documentBehavior();
        if (isCtrl) {
            switch (key) {
                case 83: // ctrl + s: lưu hồ sơ
                    docBehavior.saveDocument(e);
                    break;
                case 115: // ctrl + s: lưu hồ sơ
                    docBehavior.saveDocument(e);
                    break;
                case 73: // ctrl + i: in
                    docBehavior.print();
                    return false;
                case 72: // ctrl + h : chuyển
                    docBehavior.transfer();
                    return false;
                case 71: // ctrl + g: thêm giấy tờ
                    docBehavior.addPaper();
                    return false;
                case 76: // ctrl + l: thêm lệ phí
                    docBehavior.addFee();
                    return false;
                case 82: // ctrl + r: thêm văn bản liên quan
                    docBehavior.addRelation();
                    return false;
                case 89: // ctrl + y: thêm ý kiến xử lý
                    docBehavior.addComment();
                    return false;
                case 69: //ctrl + e: thêm tài liệu
                    docBehavior.addAttachment();
                    return false;
                default:
                    break;
            }
        }
        else {
            switch (key) {
                case 9: // tab
                case 13: // enter
                    return docBehavior.moveNext(e);
                case 37: // left
                    return docBehavior.moveLeft(e);
                case 38: // up
                    return docBehavior.moveUp(e);
                case 39: // right
                    return docBehavior.moveNext(e);
                case 40: // down
                    return docBehavior.moveDown(e);
                case 32: // spacebar
                    docBehavior.checkbox(e);
                    break;
                default:
                    break;
            }
        }
    });

    $.fn.getCursorPosition = function () {
        var el = $(this).get(0);
        var pos = 0;
        if ('selectionStart' in el) {
            pos = el.selectionStart;
        } else if ('selection' in document) {
            el.focus();
            var sel = document.selection.createRange();
            var selLength = document.selection.createRange().text.length;
            sel.moveStart('character', -el.value.length);
            pos = sel.text.length - selLength;
        }
        return pos;
    };
})
(window.jQuery)