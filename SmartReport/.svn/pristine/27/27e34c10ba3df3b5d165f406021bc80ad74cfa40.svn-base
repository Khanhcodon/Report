(function (egov, $) {

    var priorities = {
        importantWarning: 0,
        error: 1,
        success: 2,
        processing: 3,
        warning: 4
    };

    $.fn.extend({
        animateCss: function (animationName) {
            var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
            this.addClass('animated ' + animationName).one(animationEnd, function () {
                $(this).removeClass('animated ' + animationName);
            });
        }
    });

    // jQuery UI Widget
    $.widget('egov.status', {
        options: {
            duration: 5000,
            importantWarningDuration: 10000,
            subscribe: function () {
                egov.log('Chưa set thuộc tính subscribe cho option của status.');
            }
        },

        _create: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>

            this.element.addClass("egov-status");

            // Thiết lập vị trí hiển thị của status
            this.element.css({
                left: "10px",
                bottom: "0"
            });

            this._subscribeGlobalEvents();
        },

        currentStatus: null,

        _subscribeGlobalEvents: function () {
            /// <summary>
            /// Đăng ký các events ra global
            /// </summary>
            this.options.subscribe(egov.events.status.status, this._statusSubscription, this);
            this.options.subscribe(egov.events.status.success, this._statusSuccess, this);
            this.options.subscribe(egov.events.status.error, this._statusError, this);
            this.options.subscribe(egov.events.status.processing, this._statusProcessing, this);
            this.options.subscribe(egov.events.status.warning, this._statusWarning, this);
            this.options.subscribe(egov.events.status.destroy, this.destroy, this);
            this.options.subscribe("status.hide", this.destroy, this);
            this.options.subscribe(egov.events.status.importantWarning, this._statusImportantWarning, this);
        },

        _statusSubscription: function (status, position) {
            /// <summary>
            /// Hiển thị một status
            /// </summary>
            /// <param name="status">Status: {message: text, type: priority,duration: 3, undo}</param>
            /// <param name="position">Vị trí hiển thị thông báo (mobile)</param>
            var that = this,
                current = this.currentStatus,
                statusContent, statusClass;
            
            // that.element.show();

            status.priority = this._getPriority(status);

            // Hủy message hiện tại nếu message mới có priority nhỏ hơn (nhỏ hơn thì ưu tiên hơn).
            if (current && (status.priority < current.priority)) {
                clearTimeout(current.timer);
            }

            current = status;

            statusContent = this._getStatusContent(status);
            statusClass = this._getStatusColorClass(status.type);

            if (egov.mobile && status.priority == priorities.processing) {
                if (egov.mobile.isTablet) {
                    egov.mobile.$loading.attr("class", "");
                    egov.mobile.$loading.addClass(this._getStatusPositionClass(position));
                }

                egov.mobile.$loading.fadeIn();
                that.element.hide();
            }
            else {
                this.element.html(statusContent).attr("class", "alert egov-status " + statusClass).show();
            }

            that.element.animateCss("fadeInUp");

            // Trường hợp set duration = 0 hiển thị message đến khi gọi hàm destroy hoặc có notify khác ưu tiên hơn
            if (status.duration !== 0) {
                current.timer = setTimeout(function () {
                    that.element.fadeOut();
                    that.currentStatus = null;
                }, status.duration || this.options.duration);
            }
        },

        _importantWarningStatusSubscription: function (status) {
            /// <summary>
            /// Hiển thị cảnh báo quan trọng
            /// </summary>
            /// <param name="status"></param>
            var that = this,
                statusContent,
                statusClass;

            statusClass = this._getStatusColorClass(status.type);
            statusContent = this._getStatusContent(status);

            this.importantWaringElement.html(statusContent)
                .attr("class", statusClass)
                .fadeIn();

            // Trường hợp set duration = 0 hiển thị message đến khi gọi hàm destroy hoặc có notify khác ưu tiên hơn
            if (status.duration !== 0) {
                status.timer = setTimeout(function () {
                    that.importantWaringElement.fadeOut();
                    that.currentStatus = null;
                }, status.duration || this.options.importantWarningDuration);
            }
        },

        _statusSuccess: function (status) {
            /// <summary>
            /// Hiển thị thông báo xử lý thành công.
            /// </summary>
            /// <param name="status">{message: text, undo: object}</param>
            var status;
            if (typeof status == "string") {
                status = {
                    type: "success",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "success",
                    message: status.message,
                    undo: status.undo
                };
            }

            this._statusSubscription(status);
        },

        _statusError: function (status) {
            /// <summary>
            /// Hiển thị thông báo xử lý lỗi.
            /// </summary>
            /// <param name="status">{message: text, undo: object}</param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "error",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "error",
                    message: status.message,
                    undo: status.undo
                };
            }

            this._statusSubscription(status);
        },

        _statusProcessing: function (message, position) {
            /// <summary>
            /// Hiển thị thông báo đang xử lý
            /// </summary>
            /// <param name="message">text thông báo</param>
            /// <param name="position">Vị trí hiển thị thông báo</param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "processing",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "processing",
                    message: message,
                    duration: 0
                };
            }

            this._statusSubscription(status, position);
        },

        _statusWarning: function (message) {
            /// <summary>
            /// Hiển thị thông báo cảnh báo
            /// </summary>
            /// <param name="message">Text thông báo</param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "warning",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "warning",
                    message: message
                };
            }

            this._statusSubscription(status);
        },

        _statusImportantWarning: function (message) {
            /// <summary>
            /// Các cảnh báo quan trọng được hiển thị trên header của trang web
            /// </summary>
            /// <param name="message"></param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "importantWarning",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "importantWarning",
                    message: message
                };
            }

            this.importantWaringElement = parent.$("#importantWarning");
            this._importantWarningStatusSubscription(status);
        },

        _getPriority: function (status) {
            return priorities[status.type];
        },

        _getStatusContent: function (status) {
            var result, undo, statusIcon;
            result = $("<div>").addClass("status-content");

            switch (status.type) {
                case "success":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/check.svg' >";
                    break;
                case "error":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/error.svg' >";
                    break;
                case "processing":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/processing.svg' >";
                    break;
                case "warning":
                case "importantWarning":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/warning.svg' >";
                    break;
                default:
                    statusIcon = "<img src='../../content/bkav.egov/status/images/info.svg' >";
                    break;
            }
            result.append(statusIcon);

            result.append($("<span>").text(status.message));

            if (status.undo) {
                undo = $("<a href='#' class='alert-link'>").text(status.undo.message);
                undo.click(function (e) {
                    if (typeof status.undo.callback === "function") {
                        status.undo.callback();
                    }
                });

                result.append(undo);
            }

            return result;
        },

        _getStatusColorClass: function (type) {
            switch (type) {
                case "success":
                    return "alert-success";
                case "error":
                    return "alert-danger";
                case "processing":
                    return "alert-info";
                case "warning":
                case "importantWarning":
                    return "alert-warning";
                default:
                    return "alarm-info";
            }
        },

        _getStatusPositionClass: function (position) {
            // position: 
            //  - 1: Hiển thị trên danh sách văn bản
            //  - 2: Hiển thị trên chi tiết văn bản
            switch (position) {
                case 2:
                    return "showInDetail";
                default:
                    return "showInList";
            }
        },

        destroy: function () {
            if (this.currentStatus) {
                clearTimeout(this.currentStatus.timer);
            }
            // $.Widget.prototype.destroy.call(this);
            if (egov.mobile && egov.mobile.$loading) {
                egov.mobile.$loading.fadeOut();
            }
            this.element.hide();
        },
    });

}(this.egov = this.egov || {}, jQuery));