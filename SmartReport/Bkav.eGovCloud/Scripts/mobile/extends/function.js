(function (egov, $, _) {
    var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;

    egov.commonFn = {
        event: {
            changeTitleForMobile: function (app, title) {
                $("#header-title span[data-app='" + app + "']").text(title);
            },

            showNavbar: function () {
                $("#main-page").removeClass("hidelayoutnav");
            },

            hideNavbar: function () {
                $("#main-page").addClass("hidelayoutnav");
            },

            //Logout
            logout: function () {
                var cookies = document.cookie.split(";");
                for (var i = 0; i < cookies.length; i++) {
                    var eqPos = cookies[i].indexOf("=");
                    var name = eqPos > -1 ? cookies[i].substr(0, eqPos) : cookies[i];
                    name = name.trim();
                    $.cookie(name, "", { domain: document.domain, path: "/", expires: -1, secure: true });
                    $.cookie(name, "", { expires: -1, secure: true });
                }
                if (helper.isTool) {
                    if (typeof window.external.CB_Logout === 'function') {
                        window.external.CB_Logout();
                    }
                    $.cookie("isLogin", "", { expires: -1, secure: true });
                }
                window.document.location.reload();
            },
        },
        util: {
            getCustomTime: function (date, isGetOverdue) {
                /// <summary>
                /// Lấy ngày quá hạn xử lý văn bản cho Mobile
                /// </summary>
                /// <param name="date">Ngày truyền vào</param>
                /// <param name="isGetOverdue">true nếu lấy cả hạn tổng, dùng cho Mobile</param>
                if (date == null || date == '') {
                    return "";
                }

                if (regexUtcDate.test(date) || (date instanceof Date)) {
                    if (regexUtcDate.test(date)) {
                        var array = date.split(/[^0-9]/);
                        date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5])
                    } else {
                        date = new Date(date);
                    }
                }
                else {
                    date = new Date(date);
                }

                var dateNow = new Date();
                var diff = ((dateNow.getTime() - date.getTime()) / 1000);

                //Nếu lấy thời hạn xử lý thì đảo ngược lại
                if (isGetOverdue) {
                    diff = diff * -1;
                }

                var day_diff = Math.floor(diff / 86400);

                if (day_diff < 0) {
                    day_diff = Math.abs(day_diff);
                    if (day_diff > 365) {
                        return String.format(egov.resources.documents.documentNumberYearOverdue, Math.round(day_diff / 365));
                    }
                    else if (day_diff > 30) {
                        return String.format(egov.resources.documents.documentNumberMonthOverdue, Math.round(day_diff / 30));
                    }
                    else if (day_diff > 6) {
                        return String.format(egov.resources.documents.documentNumberWeekOverdue, Math.round(day_diff / 7));
                    }

                    return String.format(egov.resources.documents.documentNumberDayOverdue, day_diff); //  'QH ' + Math.abs(day_diff) + ' ngày';
                }
                else if (day_diff === 0 && date.getDate() === dateNow.getDate()) {
                    if (diff < 120) {
                        return String.format(egov.resources.time.minbefore, 1);
                    }
                    else if (diff < 3600) {
                        return String.format(egov.resources.time.minbefore, Math.floor(diff / 60));
                    }
                    else {
                        return date.format("HH:mm");
                    }
                }
                else if (dateNow.getDate() - date.getDate() === 1 && dateNow.getMonth() == date.getMonth()) {
                    return egov.resources.time.yesterday; // + ", " + date.format("HH:mm")
                }
                else if (date.weekOfYear() === dateNow.weekOfYear()) {
                    return date.getVNDay();
                }
                else if (date.getFullYear() === dateNow.getFullYear()) {
                    return date.format("dd/M");
                }

                return date.format("dd/MM/yy");
            },

            getCommonTime: function (date) {
                /// <summary>
                /// Lấy ngày xử lý thông thường
                /// </summary>
                /// <param name="date">Ngày truyền vào</param>
                if (date == null || date == '') {
                    return egov.resources.documents.unlimitedTime;
                }

                if (regexUtcDate.test(date) || (date instanceof Date)) {
                    if (regexUtcDate.test(date)) {
                        var array = date.split(/[^0-9]/);
                        date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5])
                    } else {
                        date = new Date(date);
                    }
                }
                else {
                    date = new Date(date);
                }

                var dateNow = new Date();
                var diff = ((dateNow.getTime() - date.getTime()) / 1000);

                var day_diff = Math.floor(diff / 86400);
                if (day_diff < 0) {
                    //Trường hợp này thời gian của server chạy nhanh hơn thời gian hiện tại của client
                    return egov.resources.time.justnow;
                }
                else if (day_diff === 0 && date.getDate() === dateNow.getDate()) {
                    if (diff < 120) {
                        return String.format(egov.resources.time.minbefore, 1);
                    }
                    else if (diff < 3600) {
                        return String.format(egov.resources.time.minbefore, Math.floor(diff / 60));
                    }
                    else {
                        return date.format("HH:mm");
                    }
                }
                else if (dateNow.getDate() - date.getDate() === 1 && dateNow.getMonth() == date.getMonth()) {
                    return egov.resources.time.yesterday; //  + ", " + date.format("HH:mm")
                }
                else if (date.weekOfYear() === dateNow.weekOfYear()) {
                    return date.getVNDay();
                }
                else if (date.getFullYear() === dateNow.getFullYear()) {
                    return date.format("dd/MM");
                }
                return date.format("dd/M/yy");
            },

            getDetailDate: function (date, format) {
                if (date == null || date == '') {
                    return egov.resources.documents.unlimitedTime;
                }
                if (regexUtcDate.test(date) || (date instanceof Date)) {
                    if (regexUtcDate.test(date)) {
                        var array = date.split(/[^0-9]/);
                        date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5])
                    } else {
                        date = new Date(date);
                    }
                }
                else {
                    date = new Date(date);
                }
                if (!format) {
                    return date.format("dd/MM/yyyy");
                }
                return date.format(format);
            }
        },

    }
})
(egov = window.egov || {}, $, _);
