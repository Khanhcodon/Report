(function () {

    egov = egov || {};

    egov.popup = {
        open: function (url, title, inputWidth, inputHeight) {
            /// <summary>
            /// Mở cửa sổ popup
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="title">title</param>
            /// <param name="inputWidth">chiều rộng popup, nếu không truyền sẽ lấy theo cấu hình của user</param>
            /// <param name="inputHeight">chiều cao popup, nếu không truyền sẽ lấy theo cấu hình của user</param>
            var width,
                height;

            width = inputWidth ? inputWidth : egov.setting && egov.setting.userSetting && egov.setting.userSetting.PopUpWidth ? egov.setting.userSetting.PopUpWidth : 900;
            height = inputHeight ? inputWidth : egov.setting && egov.setting.userSetting && egov.setting.userSetting.PopUpHeight ? egov.setting.userSetting.PopUpHeight : 575;
            window.open(url, title, "width=" + width + ",height=" + height);
        },

        autoSaveSize: function () {
            /// <summary>
            /// Tự động lưu kích cỡ popup khi người dùng resize
            /// </summary>
            var delay = (function () {
                var timer = 0;
                return function (callback, ms) {
                    clearTimeout(timer);
                    timer = setTimeout(callback, ms);
                };
            })();
            $(window).resize(function () {
                var that = this;
                delay(function () {
                    egov.request.setPopUpSize({
                        data:{
                            width: that.innerWidth,
                            height:that.innerHeight
                        }
                    });
                }, 500);
            });
        }
    }
})();