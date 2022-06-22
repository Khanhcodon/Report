
(function (egov, $) {
    /**
    * PubSub như là một hệ thống EventEmitter.
    * Các Widget đăng ký các sự kiện publish và sự kiện được sử dụng chung cho tất cả các subscriber khác.
    * 
    * Notes: sử dụng egov.events.js để quản lý tên các event được đăng ký.
    */
    egov.pubsub = (function () {
        var queue = [],
            that = {};

        that.publish = function (eventName, data, position) {
            /// <summary>
            /// Thực thi các hàm callback được liên kết với eventName
            /// </summary>
            /// <param name="eventName">Tên event cần thực thi</param>
            /// <param name="data">Dữ liệu truyền cho hàm callback</param>
            var context, intervalId, idx = 0;
            var events = queue[eventName];
            if (!events) return;

            intervalId = setInterval(function () {
                if (events[idx]) {
                    try {
                        context = events[idx].context || this;
                        events[idx].callback.call(context, data, position);
                    } catch (e) {
                        // log the message for developers
                        console.log('Có lỗi xảy ra khi thực thi một trong những hàm callback cho sự kiện "' + eventName + '"');
                        console.log('Lỗi đó là: "' + e + '"');
                    }

                    idx += 1;
                } else {
                    clearInterval(intervalId);
                }
            }, 0);
        };

        that.subscribe = function (eventName, callback, context) {
            /// <summary>
            /// Đăng ký một sự kiện. Cá Sự kiện đăng ký tiếp theo sẽ luôn được thêm vào (chứ không overwrite).
            /// Để hủy bỏ đăng ký một sự kiện, sử dụng hàm unsubscribe.
            /// </summary>
            /// <param name="eventName">Tên sự kiện đăng ký, nên sử dụng dấu . để phân biệt các event</param>
            /// <param name="callback">Hàm thực thi.</param>
            /// <param name="context">Context để thực thi hàm callback</param>
            if (!queue[eventName]) {
                queue[eventName] = [];
            }

            queue[eventName].push({
                callback: callback,
                context: context
            });
        };

        that.unsubscribe = function (eventName, callback, context) {
            /// <summary>
            /// Hủy bỏ đăng ký sự kiện.
            /// </summary>
            /// <param name="eventName">Tên sự kiện</param>
            /// <param name="callback">Hàm callback sau khi hủy bỏ. Sử dụng để chắc chắn rằng sự kiện đã được hủy bỏ.</param>
            /// <param name="context">Context thực thi hàm callback.</param>
            if (queue[eventName]) {
                queue[eventName].pop({
                    callback: callback,
                    context: context
                });
            }
        };

        return that;
    }());

}(this.egov = this.egov || {}, jQuery));
