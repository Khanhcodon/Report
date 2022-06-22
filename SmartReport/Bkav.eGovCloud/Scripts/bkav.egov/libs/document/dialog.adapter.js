(function ($) {

    window.dialogAdapter = function () {
        this.formClass = "dialog-obj";
    };

    /// <summary> Mở dialog</summary>
    /// <param name="innerHtml" type="String"> Nội dung html của dialog</param>
    /// <param name="setting" type="String"> Dialog setting</param>
    window.dialogAdapter.prototype.open = function (innerHtml, setting, opencallback, closecallback) {
        var $this = this;
        var $dialog = $("<div class='" + this.formClass + "'>").html(innerHtml);
        setting.modal = true;
        setting.draggable = true;
        setting.resizable = true;
        setting.open = function () {
            //$(".ui-widget-overlay").css("background", "transparent none");
            $(".ui-widget-overlay").click(function () {
                setting.close();
            });
            if (opencallback && typeof opencallback === 'function') {
                opencallback();
            }
        };
        setting.create = function () {
            $(".ui-dialog-titlebar").find("a").text("Đóng");
        };

        setting.close = function () {
            if (closecallback && typeof closecallback === 'function') {
                closecallback();
            }
            $this.close($dialog);
        };
        $dialog.dialog(setting);

        $('.dialog-obj').niceScroll();
    };

    /// <summary> Mở dialog (sửa dụng 1 element đã tồn tại)</summary>
    /// <param name="innerHtml" type="String"> Selector jquery (có thể là selector string hoặc object jquery)</param>
    /// <param name="setting" type="String"> Dialog setting</param>
    window.dialogAdapter.prototype.openexist = function (selector, setting, opencallback, closecallback, isDestroyWhenClose) {
        if (typeof isDestroyWhenClose === 'undefined' || isDestroyWhenClose === null) {
            isDestroyWhenClose = true;
        }
        var $this = this;
        var $dialog;
        if (selector instanceof jQuery) {
            if (selector.length > 0) {
                $dialog = selector;
            }
        } else if (typeof selector === 'string') {
            $dialog = $(selector);
        } else {
            return;
        }
        if (!$dialog.hasClass($this.formClass)) {
            $dialog.addClass($this.formClass);
        }
        setting.modal = true;
        setting.draggable = true;
        setting.resizable = true;
        setting.open = function () {
            //$(".ui-widget-overlay").css("background", "transparent none");
            $(".ui-widget-overlay").click(function () {
                $dialog.dialog('close');
                setting.close();
            });
            if (opencallback && typeof opencallback === 'function') {
                opencallback();
            }
        };
        setting.create = function () {
            $(".ui-dialog-titlebar").find("a").text("Đóng");
        };
        setting.close = function () {
            if (closecallback && typeof closecallback === 'function') {
                closecallback();
            }
            if (isDestroyWhenClose) {
                $this.close($dialog);
            }
        };
        $dialog.getNiceScroll().remove();
        $dialog.niceScroll();
        $dialog.dialog(setting);
    };

    /// <summary> Đóng dialog</summary>
    window.dialogAdapter.prototype.close = function (selector) {
        var $dialog;
        if (selector instanceof jQuery) {
            if (selector.length > 0) {
                $dialog = selector;
            }
        } else if (typeof selector === 'string') {
            $dialog = $(selector);
        } else {
            $dialog = $(".dialog-obj:visible");
        }
        $dialog.getNiceScroll().remove();
        //$dialog.dialog("destroy").remove();
        $dialog.dialog("destroy");
    };

})(window.jQuery);