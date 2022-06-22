define([
    'jquery'
],
    function ($) {
        "use strict";

        if (typeof $ != "function") {
            throw "Trang chưa có Jquery";
        }

        var docShortKey = {
            init: function (document) {
                /// <summary>
                /// Khởi tạo shortKey cho document
                /// </summary>
                /// <param name="document"></param>

                //Nếu chưa từng mở văn bản nào khác thì mới bind shortKey, tránh duplicate event
                //if (!this.doc) {
                //    this.bindShortKey();
                //}

                //Gán shortkey document
                this.doc = document;
                this.bindShortKey();
            },

            bindShortKey: function () {
                var that = this;
                $(that.doc.$el).on("keydown", function (e) {
                    if (e.ctrlKey) {
                        if (e.shiftKey) {
                            switch (e.keyCode) {
                                case 80: // ctrl + P: in
                                    e.preventDefault();
                                    _displayPrintReceipt(that.doc);
                                    break;
                                default:
                                    break;
                            }
                        } else {
                            switch (e.keyCode) {
                                case 83: // ctrl + s: lưu hồ sơ
                                    e.preventDefault();
                                    _saveDocument(that.doc);
                                    break;
                                case 115: // ctrl + f4: lưu hồ sơ
                                    //e.preventDefault();
                                    //docBehavior.saveDocument(e);
                                    break;
                                case 80: // ctrl + P: in
                                    e.preventDefault();
                                    _printReceipt(that.doc);
                                    break;
                                case 72: // ctrl + h : chuyển
                                    e.preventDefault();
                                    _transfer(that.doc);
                                    break;
                                case 82: // ctrl + r: thêm văn bản liên quan
                                    e.preventDefault();
                                    _addRelation(that.doc);
                                    break;
                                case 69: //ctrl + e: thêm tài liệu
                                    e.preventDefault();
                                    _addAttachment(that.doc);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (e.altKey) {
                        switch (e.keyCode) {
                            case 89: // alt + y: thêm ý kiến xử lý
                                e.preventDefault();
                                _addComment(that.doc);
                                break;
                            case 84: //alt + t: thêm tài liệu
                                e.preventDefault();
                                _addAttachment(that.doc);
                                break;
                            default:
                                break;
                        }
                    } else {
                        switch (e.keyCode) {
                            case 9: // tab
                                moveNext(e);
                                e.preventDefault();
                                break;
                            case 13: // enter
                                moveNext(e);
                                break;
                            case 37: // left
                                moveLeft(e);
                                break;
                            case 38: // up
                                moveUp(e);
                                break;
                            case 39: // right
                                moveNext(e);
                                break;
                            case 40: // down
                                //e.preventDefault();
                                moveDown(e);
                                break;
                            case 32: // spacebar
                                checkbox(e);
                                break;
                            default:
                                break;
                        }
                    }
                });
            }
        }

        var _saveDocument = function (doc) {
            /// <summary>
            /// Lưu hồ sơ
            /// </summary>
            /// <param name="doc"></param>
            /// <returns type=""></returns>
            if (doc.isHsmc) {
                doc.transfer({
                    id: "TiepNhanHoSoVaTiepTuc",
                    isSpecial: true,
                    name: "Tiếp nhận và tiếp tục (Ctrl+S)",
                })
                return;
            }
            doc.saveDocument(function (result) {
                egov.pubsub.publish(egov.events.status.success, egov.resources.document.saveSuccess);
                var docInWaitingPacket = _.find(egov.waitingPacket, function (item) {
                    return item.attachment.get("Id") == doc.attachmentIdInWaitingPacket;
                });
                //Chuyển văn bản đã lưu xuống cuối danh sách chờ xử lý theo lô
                if (docInWaitingPacket) {
                    doc.tab.close(true);
                    docInWaitingPacket.saved = true;
                    docInWaitingPacket.documentCopyId = result.documentCopyId;
                    docInWaitingPacket.title = doc.model.get("Compendium");
                    egov.waitingPacket.remove(docInWaitingPacket);
                    egov.waitingPacket.push(docInWaitingPacket);
                    return;
                }

                if (doc.model.get("CategoryBusinessId") === egov.enum.categoryBusiness.vbden) {
                    doc.tab.close(true);
                    egov.views.home.tab.addDocument(doc.model.get("DocTypeId"), doc.model.get("DocTypeName"));
                }
            });
            return false;
        }

        var _displayPrintReceipt = function (doc) {
            doc.printReceiptShortKey(true);
        }

        var _printReceipt = function (doc) {
            doc.printReceiptShortKey();
        }

        var _transfer = function (doc) {
            doc.toolbar.$(".btnTransfer").click();
        }

        var _addRelation = function (doc) {
            doc.toolbar.$(".btnInsertRelation").click();
        }

        var _addComment = function (doc) {
            doc.$("#openDialogCommon").click();
        }

        var _addAttachment = function (doc) {
            doc.toolbar.$(".fileinput-button .fileupload").click();
        }

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
            var $nextControl,
                $nextRow = $currentCell.parent(),
                indexCell,
                $cellBelow;

            if (isDown) {
                indexCell = $currentCell.index();
                while ($nextRow.next().length > 0) {
                    $nextRow = $nextRow.next();
                    $cellBelow = $nextRow.children('td:eq(' + indexCell + ')');
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
            var $prevControl,
                $prevRow = $currentCell.parent(),
                indexCell,
                $cellAbove;

            if (isUp) {
                indexCell = $currentCell.index();
                while ($prevRow.prev().length > 0) {
                    $prevRow = $prevRow.prev();
                    $cellAbove = $prevRow.children('td:eq(' + indexCell + ')');
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
            var $nextControl,
                classNameControl = $currentControl.attr('class').split(' ')[0],
                $nextLi = $currentControl.closest('li');

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
            var $prevControl,
                classNameControl = $currentControl.attr('class').split(' ')[0],
                $prevLi = $currentControl.closest('li');

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
            var $nextControl,
                currentPosition = getCellPosition($currentCell),
                totalColumn = getTotalColumnInRow($currentCell),
                $nextCell;

            if (currentPosition.col < totalColumn - 1) {
                $nextCell = $currentCell;
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
            var $prevControl,
                currentPosition = getCellPosition($currentCell),
                $prevCell;

            if (currentPosition.col > 0) {
                $prevCell = $currentCell;
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

        var moveNext = function (e) {
            var $currentElement = $(document.activeElement),
                $parent = $currentElement.parents(".document-info");

            if ($currentElement.is($parent.find("input[type=text],input[type=checkbox],select,textarea").last())) {
                //$currentElement.blur();
                //egov.utils.homeShortkey && egov.utils.homeShortkey.focusFirstTab();
                return true;
            }

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
                var $nextControl,
                    $currentCell = $currentElement.closest('td'),
                    $allControlInCurrentCell = $currentCell.find(selectorControlAcceptFocus),
                    indexCurrentElementInCell = $.inArray($currentElement[0], $allControlInCurrentCell),
                    $control;

                if (indexCurrentElementInCell < $allControlInCurrentCell.length - 1) {
                    for (var i = indexCurrentElementInCell + 1; i < $allControlInCurrentCell.length; i++) {
                        $control = $($allControlInCurrentCell[i]);
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

                if ($nextControl.is(".hasDatepicker")) {
                    $nextControl.focusAndSelectRange();
                    return;
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
                    //Chỉ ấn tab hoặc ấn right mới chuyển sang control tiếp theo
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
                        //$currentElement.trigger('mousedown');
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
        }

        var moveUp = function (e) {
            var $nextControl,
                $currentElement = $(document.activeElement);

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
                    e.preventDefault();
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
                if ($currentElement.val() === $currentElement.find("option:first").val()) {
                    $nextControl = findPrevControlInRowRemain($currentElement.closest('td'), true);
                    if ($nextControl) {
                        $nextControl.focus();
                    }
                    return false;
                }
            }
            return true;
        };

        var moveDown = function (event) {
            var $nextControl,
                $currentElement = $(document.activeElement);
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
                if ($currentElement.val() === $currentElement.find("option:last").val()) {
                    $nextControl = findNextControlInRowRemain($currentElement.closest('td'), true);
                    event.preventDefault();
                    if ($nextControl) {
                        $nextControl.focus();
                    }
                    return false;
                }
            }
            else if ($currentElement.is('.ui-multiselect')) {
                $currentElement.simulate('click');
            }
            return true;
        };

        var moveLeft = function () {
            var $currentElement = $(document.activeElement);
            if ($currentElement.parents('#tableLayout').length === 0) {
                return true;
            }
            var movePrev = function () {
                var $prevControl,
                    $currentCell = $currentElement.closest('td'),
                    $allControlInCurrentCell = $currentCell.find(selectorControlAcceptFocus),
                    indexCurrentElementInCell = $.inArray(document.activeElement, $allControlInCurrentCell),
                    $control;

                if (indexCurrentElementInCell > 0) {
                    for (var i = indexCurrentElementInCell - 1; i >= 0; i--) {
                        $control = $($allControlInCurrentCell[i]);
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

        var moveRight = function () {
            return this.moveNext();
        };

        var checkbox = function () {
            var $currentElement = $(document.activeElement);
            if ($currentElement.is('input[type="checkbox"]')) {
                $currentElement.simulate('click');
            }
        };

        return docShortKey;
    });