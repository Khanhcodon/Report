// bt.util.date.js

/*
    Author: TienBV
    DateCreated: 25/06/2015
    Version: 1.0

    Description:

    - Các đơn vị thời gian:
        year: "year",
        quarter: "quarter",
        month: "month",
        week: "week",
        date: "date",
        day: "day",
        hour: "hours",
        minute: "minutes",
        second: "seconds",
        miniSecond: "miniSecond"

    - Các định dạng format
        yy: "yy",                   // Năm: 96, 97, ... 14, 15
        yyyy: "yyyy",               // Năm đầy đủ: 1996, 1997, ... 2014, 2015
        q: "q",                     // Quý: 1, 2, 3, 4
        M: "M",                     // Tháng: 1, 2, ... 12
        MM: "MM",                   // Tháng đầy đủ: 01, 02, ... 12
        MMM: "MMM",                 // Tháng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Tháng: Tháng một, Tháng hai, ... Tháng mười hai
        w: "w",                     // Tuần: 1, 2, ... 52
        ww: "ww",                   // Tuần: 01, 02, ... 52
        d: "d",                     // Ngày trong tháng: 1, 2, ... 31
        dd: "dd",                   // Ngày trong tháng: 01, 02, ... 31
        ddd: "ddd",                 // Ngày trong tuần: Thứ 2, thứ 3, ... 
        h: "h",                     // Giờ (12): 1, 2, ... 12    
        hh: "hh",                   // Giờ (12): 01, 02, ... 12
        H: "H",                     // Giờ (24): 1, 2, ... 23
        HH: "HH",                   // Giờ (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Phút: 1, 2, ... 59
        mm: "mm",                   // Phút: 01, 02, ... 59
        s: "s",                     // Giây: 1, 2, ... 59
        ss: "ss",                   // Giây: 01, 02, ... 59
        sss: "sss",                 // Tích tắc: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST

    - Các method cho kiểu dữ liệu Date

        + parse(value, format): Chuyển đổi string sang datetime theo format chỉ định. Mặc định hệ thống parse theo ISODate format.
            Date.parse("2015-07-15T16:20:04.021Z")  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            Date.parse("2015-07-15", "yyyy-MM-dd)  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            
            Notes: sẽ có trong phiên bản 1.1

        + daysInMonth(month, year): Trả về số ngày trong tháng.
            Date.daysInMonth(7, 2015)  = 31

        + compare(date1, date2): Trả về kết quả so sánh giữa 2 đối tượng ngày tháng.
            Notes: sẽ có trong phiên bản 1.1

        + max([date1, date2, ...]): Trả về đối tượng ngày tháng lớn nhất.
            Notes: sẽ có trong phiên bản 1.1

        + min([date1, date2, ...]): Trả về đối tượng ngày tháng nhỏ nhất.
            Notes: sẽ có trong phiên bản 1.1

    - Các method cho đối tượng dữ liệu String

        + format(partern): Convert ngày tháng theo chuỗi theo định dạng yêu cầu và trả về kết quả; partern mặc định hh:mm dd/MM/yyyy (Vn_vi)
            new Date().format("dd/MM/yy") = "15/07/15"
          
        + subtract(value, unit): Trừ đi một khoảng thời gian theo đơn vị thời gian (ở trên) của đối tượng ban đầu và trả về đối tượng mới.
            new Date().subtract(1, "month").format("dd/MM/yy") = "15/06/15"

        + add(value, unit): Trừ đi một khoảng thời gian theo đơn vị thời gian (ở trên) của đối tượng ban đầu và trả về đối tượng mới.
            new Date().add(1, "month").format("dd/MM/yy") = "15/06/15"

        + month(value): Lấy hoặc thiết lập tháng của đối tượng thời gian hiện tại: 1, 2, ... 12
            new Date().month() = 07
            new Date().month(12).format("dd/MM/yy") = "15/12/15";

        + year(value): Tương tự hàm month ở trên.
            new Date().year() = 2015
            new Date().year(2017).format("dd/MM/yy") = "15/07/17";

        + date(value): Lấy hoặc thiết lập ngày trong tháng của đối tượng thời gian hiện tại: 1, 2, ... 31
            new Date().date() = 15
            new Date().date(24).format("dd/MM/yy") = "24/07/17";

        + day(): Trả về thứ tự ngày trong tuần của đối tượng thời gian hiện tại tính từ chủ nhật: 0, 6
            new Date().day() = 03

        + weekOfYear():  Trả về thứ tự tuần trong năm của đối tượng thời gian hiện tại: 1, 2, ... 52
            new Date().weekOfYear() = 29

        + dayOfYear(): Trả về thứ tự ngày trong năm của đối tượng thời gian hiện tại: 1, 2, ... 365 (366)
            new Date().dayOfYear() = 196

        + hours(value): Lấy hoặc thiết lập giờ của đối tượng thời gian hiện tại: 1, 2, ... 23
            var d = new Date(); d.setHours(15); d.format("hh:mm")    = "15:33"

        + minutes(value): Tương tự hours

        + seconds(value): Tương tự hours

        + miniSeconds(value): Tương tự hours

        + quarter(): Trả về quý của đối tượng thời gian hiện tại
            new Date().quarter() = 3

        + endOf(): Thiết lập đối tượng thời gian hiện tại thành thời điểm cuối cùng theo đơn vị thời gian truyền vào và trả về ngày mới tương ứng.
            new Date().endOf("year").format("dd/MM/yyyy")   = "31/12/2015"          ( Ngày cuối cùng trong năm )
            new Date().endOf("month").format("dd/MM/yyyy")  = "31/07/2015"          ( Ngày cuối cùng trong tháng )
            new Date().endOf("day").format()                = "23:59 15/07/2015"    ( Thời điểm cuối cùng trong ngày)
            ...

        + startOf(): Tương tự endOf
            new Date().startOf("year").format("dd/MM/yyyy")   = "01/01/2015"          ( Ngày đầu tiên trong năm )
            new Date().startOf("month").format("dd/MM/yyyy")  = "01/07/2015"          ( Ngày đầu tiên trong tháng )
            new Date().startOf("day").format()                = "00:00 15/07/2015"    ( Thời điểm đầu tiên trong ngày)
            ...

        + isLeapYear(): Trả về giá trị xác định năm hiện tại có phải là năm nhuận không; true = năm nhuận; false = không.
            new Date().subtract(1, "year").isLeapYear() = true;
*/


bt_util_date_resource = {
    vi: {
        months: ["tháng 1", "tháng 2", "tháng 3", "tháng 4", "tháng 5", "tháng 6", "tháng 7", "tháng 8", "tháng 9", "tháng 10", "tháng 11", "tháng 12"],
        shortMonths: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
        weeks: ["chủ nhật", "thứ hai", "thứ ba", "thứ tư", "thứ năm", "thứ sáu", "thứ bảy"],
        shortWeeks: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
        times: {
            future: '%s tới',
            ago: '%s trước',
            s: 'vài giây',
            m: 'một phút',
            mm: '%d phút',
            h: 'một giờ',
            hh: '%d giờ',
            d: 'một ngày',
            dd: '%d ngày',
            M: 'một tháng',
            MM: '%d tháng',
            y: 'một năm',
            yy: '%d năm'
        },
        calendar: {
            sameDay: 'Hôm nay',
            nextDay: 'Ngày mai',
            lastDay: 'Hôm qua',
            sameWeek: 'Tuần này',
            nextWeek: 'Tuần tới',
            lastWeek: 'Tuần trước',
            sameMonth: 'Tháng này',
            lastMonth: 'Tháng trước'
        }
    }
};

(function (window) {
    "use strict";

    var _date, _prototype, _unitType, _formatObject, _formatTokens, _defaultFormat, _resource, _defaultFarseFormat;
    _date = Date;
    _prototype = _date.prototype;
    _unitType = {
        year: "year",
        quarter: "quarter",
        month: "month",
        week: "week",
        date: "date",
        day: "day",
        hour: "hours",
        minute: "minutes",
        second: "seconds",
        miniSecond: "miniSecond"
    };
    _formatTokens = /(\[[^\[]*\])|(\\)?(MM?M?M?|dd?d?|ww?|q|yy?yy?|a|A|hh?|HH?|mm?|ss?s?|z|.)/g;
    _defaultFormat = "hh:mm dd/MM/yyyy";
    _defaultFarseFormat = "yyyy-MM-ddTHH:mm:ss";
    _formatObject = {
        yy: "yy",                   // Năm: 96, 97, ... 14, 15
        yyyy: "yyyy",               // Năm: 1996, 1997, ... 2014, 2015
        q: "q",                     // Quý: 1, 2, 3, 4
        M: "M",                     // Tháng: 1, 2, ... 12
        MM: "MM",                   // Tháng: 01, 02, ... 12
        MMM: "MMM",                 // Tháng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Tháng: Tháng một, Tháng hai, ... Tháng mười hai
        w: "w",                     // Tuần: 1, 2, ... 52
        ww: "ww",                   // Tuần: 01, 02, ... 52
        d: "d",                     // Ngày trong tháng: 1, 2, ... 31
        dd: "dd",                   // Ngày trong tháng: 01, 02, ... 31
        ddd: "ddd",                 // Ngày trong tuần: Thứ 2, thứ 3, ... 
        h: "h",                     // Giờ (12): 1, 2, ... 12    
        hh: "hh",                   // Giờ (12): 01, 02, ... 12
        H: "H",                     // Giờ (24): 1, 2, ... 23
        HH: "HH",                   // Giờ (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Phút: 1, 2, ... 59
        mm: "mm",                   // Phút: 01, 02, ... 59
        s: "s",                     // Giây: 1, 2, ... 59
        ss: "ss",                   // Giây: 01, 02, ... 59
        sss: "sss",                 // Tích tắc: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST 
    };
    _resource = bt_util_date_resource["vi"];

    //#region Prototype Methods

    _prototype.format = function Date$format(partern) {
        /// <summary>
        /// Convert ngày tháng thành chuỗi theo định dạng yêu cầu
        /// </summary>
        /// <param name="partern">Định dạng chuỗi cần xuất ra.</param>
        var formats, leng, match, i, formatResult;
        var result = "";

        partern = partern || _defaultFormat;
        formats = partern.match(_formatTokens);
        leng = formats.length;

        for (i = 0; i < leng; i++) {
            match = formats[i].toString();
            match = match.replace("[", "").replace("]", "");
            result += isFormatting(match) ? this._getFormattingValue(match) : match;
        }

        return result;
    }

    _prototype.subtract = function Date$subtract(value, unit) {
        /// <summary>
        /// Trừ đi một khoảng thời gian và trả về khoảng thời gian mới từ thời gian ban đầu.
        /// </summary>
        /// <param name="value">Giá trị trừ</param>
        /// <param name="unit">Trường thời gian cần trừ</param>
        switch (unit) {
            case _unitType.year:
                this.year(this.year() - value);
                break;
            case _unitType.month:
                this.month(this.month() - value);
                break;
            case _unitType.date:
                this.date(this.date() - value);
                return this;
            case _unitType.hour:
                this.hours(this.hours() - value);
                break;
            case _unitType.minute:
                this.minutes(this.minutes() - value);
                break;
            case _unitType.second:
                this.seconds(this.seconds() - value);
                break;
            case _unitType.miniSecond:
                this.miniSeconds(this.miniSeconds() - value);
                break;
            default:
                break;
        }

        return this;
    }

    _prototype.add = function Date$add(value, unit) {
        /// <summary>
        /// Thêm một khoảng thời gian vào thời gian ban đầu và trả về thời gian mới
        /// </summary>
        /// <param name="value">Khoảng thời gian cần thêm</param>
        /// <param name="unit">Trường thời gian cần thêm</param>
        switch (unit) {
            case _unitType.year:
                this.year(this.year() + value);
                break;
            case _unitType.month:
                this.month(this.month() + value);
                break;
            case _unitType.date:
                this.date(this.date() + value);
                return this;
            case _unitType.hour:
                this.hours(this.hours() + value);
                break;
            case _unitType.minute:
                this.minutes(this.minutes() + value);
                break;
            case _unitType.second:
                this.seconds(this.seconds() + value);
                break;
            case _unitType.miniSecond:
                this.miniSeconds(this.miniSeconds() + value);
                break;
            default:
                break;
        }

        return this;
    }

    _prototype.month = function Date$month(value) {
        /// <summary>
        /// Lấy hoặc thiết lập tháng của ngày này: 1,2 ... 12
        /// </summary>

        if (value == 2 && this.date() > 28) {
            this.date(28);
        }

        if (hasValue(value)) {
            this.setMonth(value - 1);
            return this;
        }

        return this.getMonth() + 1;
    }

    _prototype.year = function Date$year(value) {
        /// <summary>
        /// Lấy hoặc thiết lập năm của ngày này
        /// </summary>
        if (hasValue(value)) {
            this.setFullYear(value);
            return this;
        }

        return this.getFullYear();
    }

    _prototype.date = function Date$date(value) {
        /// <summary>
        /// Lấy hoặc thiết lập ngày trong tháng của ngày này
        /// </summary>
        /// <param name="value" type="int">Ngày cần set, gán null để lấy giá trị.</param>

        if (hasValue(value)) {
            this.setDate(value);
            return this;
        }

        return this.getDate();
    }

    _prototype.day = function Date$day() {
        /// <summary>
        /// Trả về ngày trong tuần của ngày này
        /// </summary>

        return this.getDay();
    }

    _prototype.weekOfYear = function Date$week() {
        /// <summary>
        /// Trả về tuần trong năm của ngày này
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - firstDay) / 86400000) + firstDay.day() - 1) / 7);
    }

    _prototype.dayOfYear = function Date$dayOfYear() {
        /// <summary>
        /// Trả về ngày trong năm của ngày này, ví dụ ngày thứ 234 của 365
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((this - firstDay) / 86400000);
    }

    _prototype.hours = function Date$hours(value) {
        /// <summary>
        /// Lấy hoặc thiết lập giờ của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setHours(value);
            return this;
        }

        return this.getHours();
    }

    _prototype.minutes = function Date$minutes(value) {
        /// <summary>
        /// Lấy hoặc thiết lập phút của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setMinutes(value);
            return this;
        }

        return this.getMinutes();
    }

    _prototype.seconds = function Date$seconds(value) {
        /// <summary>
        /// Lấy hoặc thiết lập giây của ngày này
        /// </summary>
        if (hasValue(value)) {
            this.setSeconds(value);
            return this;
        }

        return this.getSeconds();
    }

    _prototype.miniSeconds = function Date$miniSeconds(value) {
        /// <summary>
        /// Lấy hoặc thiết lập mini giây của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setMilliseconds(value);
            return this;
        }

        return this.getMilliseconds();
    }

    _prototype.quarter = function Date$quarter() {
        /// <summary>
        /// Trả về quý của ngày này
        /// </summary>
        return Math.floor(((this.month() - 1) / 3) + 1);
    }

    _prototype.endOf = function Date$endOf(unit) {
        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm cuối cùng của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit">
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày cuối cùng trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày cuối cùng của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày cuối cùng trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày cuối cùng trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 23:59:59 của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 59:59 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 59s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 999ms của giây hiện tại.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "Đơn vị truyền vào không hợp lệ.";
        }

        var year, quarter, month, days, date;
        year = this.year();
        quarter = this.quarter();
        month = this.month();
        date = this.date();

        switch (unit) {
            case _unitType.year:
                this.month(12).date(31).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.quarter:
                month = quarter * 3;
                days = _date.daysInMonth(month, year);
                this.month(month).date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.month:
                days = _date.daysInMonth(month, year);
                this.date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.week:
                days = date - this.day() + 6;
                this.date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.day:
                this.hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.hour:
                this.minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.minute:
                this.seconds(59).miniSeconds(999);
                break;
            case _unitType.second:
                this.miniSeconds(999);
                break;
        }

        return this;
    }

    _prototype.startOf = function Date$startOf(unit) {
        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm đầu tiên của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit">
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày đầu tiên trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày đầu tiên của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày đầu tiên trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày đầu tiên trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 12:00:00 am của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 00:00 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 00s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 000ms của giây hiện tại.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "Đơn vị truyền vào không hợp lệ.";
        }

        var quarter, month, days, date;
        quarter = this.quarter();
        date = this.date();

        switch (unit) {
            case _unitType.year:
                this.month(1).date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.quarter:
                month = quarter * 3 - 2;
                this.month(month).date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.month:
                this.date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.week:
                days = date - this.day();
                this.date(days).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.day:
                this.hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.hour:
                this.minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.minute:
                this.seconds(0).miniSeconds(0);
                break;
            case _unitType.second:
                this.miniSeconds(0);
                break;
        }

        return this;
    }

    _prototype.isLeapYear = function Date$isLeapYear() {
        /// <summary>
        /// Trả về giá trị xác định năm hiện tại có phải là năm nhuận không.
        /// </summary>
        return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
    }

    _prototype._getFormattingValue = function Date$_getFormattingValue(partern) {
        var result;
        switch (partern) {
            case _formatObject.yy:
                result = this.year().toString().substr(2, 2);
                break;
            case _formatObject.yyyy:
                result = this.year();
                break;
            case _formatObject.q:
                result = this.quarter();
                break;
            case _formatObject.M:
                result = this.month();
                break;
            case _formatObject.MM:
                result = toDigit(this.month());
                break;
            case _formatObject.MMM:
                result = _resource.shortMonths[this.month() - 1];
                break;
            case _formatObject.MMMM:
                result = _resource.months[this.month() - 1];
                break;
            case _formatObject.d:
                result = this.date();
                break;
            case _formatObject.dd:
                result = toDigit(this.date());
                break;
            case _formatObject.ddd:
                result = _resource.weeks[this.day()];
                break;
            case _formatObject.h:
                result = to12Hour(this.hours());
                break;
            case _formatObject.hh:
                result = toDigit(to12Hour(this.hours()));
                break;
            case _formatObject.H:
                result = this.hours();
                break;
            case _formatObject.HH:
                result = toDigit(this.hours());
                break;
            case _formatObject.m:
                result = this.minutes();
                break;
            case _formatObject.mm:
                result = toDigit(this.minutes());
                break;
            case _formatObject.s:
                result = this.seconds();
                break;
            case _formatObject.ss:
                result = toDigit(this.seconds());
                break;
            case _formatObject.S:
                result = this.miniSeconds();
                break;
            case _formatObject.Z:
                result = this.year();
                break;
            default:
                result = "";
                break;
        }
        return result;
    }

    _prototype.relativeDate = function () {
        /// <summary>
        /// Time ago
        /// </summary>

        var seconds = Math.floor((new Date() - this) / 1000);
        var interval = Math.floor(seconds / 31536000);

        if (interval > 1) {
            return interval + " năm";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " Thg";
        }
        interval = Math.floor(seconds / 604800);
        if (interval > 1) {
            return interval + " tuần";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " ngày";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " giờ";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " phút";
        }
        return "Vừa mới";
    }

    _prototype.toServerString = function Date$toServerString() {
        //2017-10-25T11:04:23
        return this.format(_defaultFarseFormat);
    }

    _prototype.getVNDay = function () {
        var day = this.getDay();
        return bt_util_date_resource.vi.weeks[day];
    }

    _prototype.timeInWord = function () {
        /// <summary>
        ///
        /// </summary>

        var date = this; var distanceMillis;
        var now = Date.now();

        var today = now.format("yyyyMMwwdd");
        var lastDay = now.subtract(1, _unitType.date).format("yyyyMMwwdd");
        var week = now.format("yyyyMMww");
        var lastWeek = now.subtract(1, _unitType.week).format("yyyyMMww");
        var month = now.format("yyyyMM");
        var lastMonth = now.subtract(1, _unitType.month).format("yyyyMM");

        var year = now.format("yyyy");

        var dateFormat = date.format("yyyyMMwwddhh");
        if (dateFormat.indexOf(today) === 0) {
            return _resource.calendar.sameDay;
        }
        if (dateFormat.indexOf(lastDay) === 0) {
            return _resource.calendar.lastDay;
        }

        if (dateFormat.indexOf(week) === 0) {
            return _resource.calendar.sameWeek;
        }
        if (dateFormat.indexOf(lastWeek) === 0) {
            return _resource.calendar.lastWeek;
        }

        if (dateFormat.indexOf(month) === 0) {
            return _resource.calendar.sameMonth;
        }
        if (dateFormat.indexOf(lastMonth) === 0) {
            return _resource.calendar.lastMonth;
        }

        if (dateFormat.indexOf(year) === 0)
            return date.format("[Tháng] MM");

        return date.format("[Tháng] MM - yyyy");
    }

    _prototype.isToday = function () {
        var today = Date.now().format("yyyyMMdd");
        var date = this.format("yyyyMMdd");
        return date === today;
    }

    _prototype.isYesterday = function () {
        var yesterday = new Date().add(-1, _unitType.date).format("yyyyMMdd");
        var date = this.format("yyyyMMdd");
        return date === yesterday;
    },

    //#endregion

    //#region Statis Methods

    _date.parse = function Date$parse(value, format) {
        /// <summary>
        /// Chuyển đổi string thành định dạng ngày tháng theo format chỉ định
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        format = format || _defaultFarseFormat;

        if (!value)
            return null;

        if (Globalize) {
            return Globalize.parseDate(value, format)
        }

        return null;
    }

    _date.parseFromIsoString = function Date$parseFromIsoString(input) {
        var arr = input.split(/[^0-9]/);
        return new Date(arr[0], arr[1] - 1, arr[2], arr[3], arr[4], arr[5]);
    }

    _date.daysInMonth = function Date$daysInMonth(month, year) {
        /// <summary>
        /// Trả về số ngày trong tháng
        /// </summary>
        /// <param name="month">Tháng</param>
        /// <param name="year">Năm</param>
        return new Date(year, month, 0).date();
    }

    _date.compare = function Date$compare(date1, date2) {
        /// <summary>
        /// So sánh 2 ngày tháng và trả về kết quả so sánh nhỏ hơn, bằng, lớn hơn.
        /// </summary>
        /// <param name="date1">Ngày 1</param>
        /// <param name="date2">Ngày 2</param>
        /// <returns type="int">1-ngày 1 lớn hơn ngày 2; 0-ngày 1 bằng ngày 2; -1-ngày 1 nhỏ hơn ngày 2</returns>
    }

    _date.max = function () {
        /// <summary>
        /// Trả về ngày lớn nhất của kiểu dữ liệu Date
        /// </summary>
    }

    _date.min = function () {
        /// <summary>
        /// Trả về ngày nhỏ nhất của kiểu dữ liệu Date
        /// </summary>
    }

    _date.now = function () {
        return new Date;
    }

    //#endregion

    //#region Private Methods

    var hasValue = function (value) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị phải là null hay không.
        /// </summary>
        /// <param name="value">Giá trị</param>
        return value !== undefined && value !== null;
    };

    var _isObject = function (obj) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị có phải là một object không
        /// </summary>
        /// <param name="obj"></param>
        return typeof obj === "object";
    };

    var _isNumber = function (val) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị hiện tải có phải là 1 number hay không.
        /// </summary>
        /// <param name="val"></param>

    };

    var toDigit = function (number) {
        return number > 9 ? number : ("0" + number);
    };

    var to12Hour = function (value) {
        return value > 12 ? (value - 12) : value;
    };

    var isFormatting = function (value) {
        return _formatObject[value] !== undefined;
    };

    //#endregion
})(this);