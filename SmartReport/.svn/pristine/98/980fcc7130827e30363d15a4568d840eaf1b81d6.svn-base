(function ($) {
    "use strict";

    window.dialogAdapter = function (e) {
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
        setting.dialogClass = 'modal fade in modal-content';
        setting.closeText = "x";
        setting.closeOnEscape = true;
        setting.open = function () {
            $(".ui-widget-overlay").css("background", "transparent none");
            //$(".ui-widget-overlay").click(function () {
            //    setting.close();
            //});
            if (opencallback && typeof opencallback === 'function') {
                opencallback();
            }

            // override css theo bootstrap
            $(this).dialog("option", "height", setting.height);
            $('.ui-widget-overlay').addClass('modal-backdrop fade in');
            $('.ui-dialog-titlebar').addClass('modal-header');
            $('.ui-dialog-title').addClass('bold-text');
            $('.ui-dialog-content').addClass('modal-body');
            $('.ui-dialog-buttonpane').addClass('modal-footer');
            $('.ui-dialog-titlebar-close').addClass('close');
        };

        setting.close = function () {
            if (closecallback && typeof closecallback === 'function') {
                closecallback();
            }
            $this.close($dialog);
        };

        $dialog.dialog(setting);

        if (!egov.isMobile) {
            require(["nicescroll", function () {
                $('.dialog-obj').niceScroll();
            }]);
        }
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
        setting.dialogClass = 'modal fade in modal-content';
        setting.closeText = "x";
        setting.closeOnEscape = true;
        setting.container = '.egov';
        setting.open = function () {
            //$(".ui-widget-overlay").click(function () {
            //    $dialog.dialog('close');
            //    setting.close();
            //});
            if (opencallback && typeof opencallback === 'function') {
                opencallback();
            }

            // override css theo bootstrap
            $(this).dialog("option", "height", setting.height);
            $('.ui-dialog').css('margin-top', '-' + setting.height / 2 + 'px');
            $('.ui-widget-overlay').addClass('modal-backdrop fade in');
            $('.ui-dialog-titlebar').addClass('modal-header');
            $('.ui-dialog-title').addClass('bold');
            $('.ui-dialog-content').addClass('modal-body');
            $('.ui-dialog-buttonpane').addClass('modal-footer');
            $('.ui-dialog-titlebar-close').addClass('close');
            $('.ui-dialog-buttonpane button').addClass('btn btn-default');
            $('.ui-dialog-buttonpane button:last').addClass('btn-primary');
        };
        setting.close = function () {
            if (closecallback && typeof closecallback === 'function') {
                closecallback();
            }
            if (isDestroyWhenClose) {
                $this.close($dialog);
                $dialog.dialog("destroy").remove();
            }
        };
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
            $dialog = $(".dialog-obj");
        }
        // $dialog.getNiceScroll().dialog("destroy");//.remove();
        $dialog.dialog("destroy").remove();
    };
})(window.jQuery);