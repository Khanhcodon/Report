(function (calendar, $, undefined) {
    calendar.cursor = {};
    // Hàm lấy vị trí con trỏ
    var doGetCaretPosition = function (oField) {
        // Initialize
        var iCaretPos = 0;

        // IE Support
        if (document.selection) {
            // Set focus on the element
            oField.focus();
            // To get cursor position, get empty selection range
            var oSel = document.selection.createRange();
            // Move selection start to 0 position
            oSel.moveStart('character', -oField.value.length);
            // The caret position is selection length
            iCaretPos = oSel.text.length;
        }
            // Firefox support
        else if (oField.selectionStart || oField.selectionStart == '0')
            iCaretPos = oField.selectionStart;

        // Return results
        return iCaretPos;
    }

    calendar.cursor.moveRight = function (selector) {
        if (doGetCaretPosition(selector) == $(selector).val().length) {
            var tagTd = $(selector).parent();
            //Thực hiện sang bên Phải thì con trỏ chuột phải về vị trí cuối cùng
            if (tagTd.next().children().length < 2) {
                var html = tagTd.next().children().val();
                var input = tagTd.next().children();
                input.focus().val("").val(html);
                hidetimepicker($(selector), input);
            }
        }
    }

    calendar.cursor.moveLeft = function (selector) {
        if (doGetCaretPosition(selector) == 0) {
            var tagTd = $(selector).parent();
            var input = tagTd.prev().children();
            // Thực hiện sang trái thì con trỏ chuột phải về vị trí đầu tiên
            input.focus();
            input.get(0).setSelectionRange(0, 0);
            hidetimepicker($(selector), input)
        }
    }

    calendar.cursor.moveUp = function (selector) {
        if (doGetCaretPosition(selector) == 0) {
            // Lấy đến tr
            var tagTr = $(selector).parent().parent();
            // Tìm tất cả con của Tr
            var listTd = tagTr.children();
            // Lấy đến td;
            var tagTd = $(selector).parent();
            // Tìm chỉ số của ô so với tất cả td trong tr
            var idx = listTd.index(tagTd);
            // tìm đến ô phía trên
            var input = tagTr.prev().children().eq(idx).children();
            //thực hiện lên trên thì con trỏ sẽ đi về đầu tiên
            input.focus();
            input.get(0).setSelectionRange(0, 0);

            hidetimepicker($(selector), input);
        }
    }

    calendar.cursor.moveDown = function (selector) {
        if (doGetCaretPosition(selector) == $(selector).val().length) {
            var tagTr = $(selector).parent().parent();
            var listTd = tagTr.children();
            var tagTd = $(selector).parent();
            var idx = listTd.index(tagTd);
            //$(this).parent().parent().next().children().eq(idx).children().focus();
            //var el = $(this).parent().parent().next().children().eq(idx).children().get(0);

            var html = tagTr.next().children().eq(idx).children().val();
            var input = tagTr.next().children().eq(idx).children();
            input.focus().val("").val(html);
            hidetimepicker($(selector), input);
        }
    }


    function hidetimepicker(selector, selectornext) {
        selector.datetimepicker('hide');
        selectornext.datetimepicker('show');
    }
})(window.calendar = window.calendar || {}, window.jQuery);