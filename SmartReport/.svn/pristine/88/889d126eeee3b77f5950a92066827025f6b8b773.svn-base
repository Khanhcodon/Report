(function (eR, $) {
    // Lấy config của eReport
    var field = eR.field || {};
    var config = eR.config || {};
    var copied = "";
    var isCtrl = false;

    $(document).keyup(function (e) {
        e = e || window.event;
        var key = e.keyCode;
        if (key == 17) isCtrl = false;
    }).keydown(function (e) {
        e = e || window.event;
        var key = e.keyCode;
        if (key == 17) isCtrl = true;
        var selecteds = $(config.contentPanel.textSelected_class + ", " + config.contentPanel.cellSelected_class);
        if (isCtrl) {
            switch (key) {

                // Ctrl + A:
                case 65:
                    return false;

                // Ctrl + B: Bôi đậm
                case 66:
                    if (selecteds.css("font-weight") === "bold") {
                        eR.commandManager.execute(new eR.commands.addCss(selecteds, "font-weight", "normal"));
                    }
                    else {
                        eR.commandManager.execute(new eR.commands.addCss(selecteds, "font-weight", "bold"));
                    }
                    break;

                // Ctrl + C: copy
                case 67:
                    copied = selecteds;
                    break;

                // Ctrl + E: Căn giữa
                case 69:
                    eR.commandManager.execute(new eR.commands.addCss(selecteds, "text-align", "center"));
                    break;

                // Ctrl + I: In nghiêng
                case 73: 
                    if (selecteds.css("font-style") === "italic") {
                        eR.commandManager.execute(new eR.commands.addCss(selecteds, "font-style", "normal"));
                    }
                    else {
                        eR.commandManager.execute(new eR.commands.addCss(selecteds, "font-style", "italic"));
                    }
                    break;

                // Ctrl + L: Căn trái
                case 76:
                    eR.commandManager.execute(new eR.commands.addCss(selecteds, "text-align", "left"));
                    break;

                // Ctrl + R: Căn phải
                case 82: 
                    eR.commandManager.execute(new eR.commands.addCss(selecteds, "text-align", "right"));
                    return false; // do xung với phím tắt của trình duyệt

                // Ctrl + U: Gạch chân
                case 85:
                    if (selecteds.css("text-decoration") === "underline") {
                        eR.commandManager.execute(new eR.commands.addCss(selecteds, "text-align", "normal"));
                    }
                    else {
                        eR.commandManager.execute(new eR.commands.addCss(selecteds, "text-align", "underline"));
                    }
                    break;

                // Ctrl + V: paste
                case 86:
                    eR.commandManager.execute(new eR.commands.copy(copied));
                    break;

                // Ctrl + Y: Redo
                case 89:
                    eR.commandManager.redo();
                    break;

                // Ctrl + Z: Undo
                case 90:
                    eR.commandManager.undo();
                    break;

                // Ctrl + -: Giảm cỡ chữ
                case 189:
                    eR.commandManager.execute(new eR.commands.decreaseFont(selecteds));
                    return false; // do xung với phím tắt của trình duyệt

                // Ctrl + =: Tăng cỡ chữ
                case 187:
                    eR.commandManager.execute(new eR.commands.increaseFont(selecteds));
                    return false; // do xung với phím tắt của trình duyệt

                // Mặc định
                default:
                    break;
            }
        }
        else {
            switch (key) {
                case 46: // delete
                    eR.commandManager.execute(new eR.commands.delete(selecteds));
                    break;
                case 27: // Escape
                    $(config.tmp.text_class).remove();
                    $(config.tmp.table_class).remove();
                    $(config.tmp.img_class).remove();
                    break;
                default:
                    break;
            }
        }
    });
})(eReport = eReport || {}, jQuery);