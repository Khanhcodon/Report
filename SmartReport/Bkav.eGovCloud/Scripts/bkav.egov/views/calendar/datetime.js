(function () {
    eG.GetByType = function () {
        var that = this;
        return {
            daily: function (date, currentDate) {
                var dateDiff = that.DatetimeUtil.dateDiffInDays(date, currentDate)
                switch (dateDiff) {
                    case -2:
                        return "Hôm kia"
                    case -1:
                        return "Hôm qua"
                    case 0:
                        return "Hôm nay"
                    case 1:
                        return "Ngày mai"
                    case 2:
                        return "Ngày kia"
                    default:
                        return that.DatetimeUtil.formatDate(currentDate)
                }
            },
            weekly: function (date, currentDate) {
                var dateDiff = that.DatetimeUtil.dateDiffInDays(date, currentDate)
                switch (dateDiff) {
                    
                    case -1:
                        return "Tuần trước"
                    case 0:
                        return "Tuần nay"
                    case 1:
                        return "Tuần sau"
                  
                    default:
                        return that.DatetimeUtil.formatDate(currentDate)
                }
            },
            monthly: "monthly",
        }
    }

    eG.DatetimeUtil = {
        formatDate: function (d) {
            function pad(s) { return (s < 10) ? '0' + s : s; }
            return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
        },
        getCurrentWeek: function () {
            var dt = new Date(this.getFullYear(), 0, 1);
            return Math.ceil((((this - dt) / 86400000) + dt.getDay() + 1) / 7);
        },
        getFirstDayOfWeek: function (year, wn) {
            return this.w2date(year, wn, 0);
        },
        getLastDayOfWeek: function (year, wn) {
            return this.w2date(year, wn, 6);
        },
        w2date: function (year, wn, dayNb) {
            var j10 = new Date(year, 0, 10, 12, 0, 0),
               j4 = new Date(year, 0, 4, 12, 0, 0),
               mon1 = j4.getTime() - j10.getDay() * 86400000;
            var date = new Date(mon1 + ((wn - 1) * 7 + dayNb) * 86400000);
            date = new Date(date.toDateString())
            return date;
        },
        addDays: function (date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        },
        dateDiffInDays: function (a, b) {
            // Discard the time and time-zone information.
            const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
            const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

            return Math.floor((utc2 - utc1) / 1000 * 60 * 60 * 24);
        },
    }
    return eG
})()