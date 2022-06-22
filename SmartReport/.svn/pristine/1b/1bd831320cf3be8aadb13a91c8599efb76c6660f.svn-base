//hàm cộng ngày tháng với 1 số
function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}

//hàm biễn chuỗi ngày tháng có định dạng dd/MM/yyyy thành ngày tháng
function getDateformatstring(strdate) {
    var arrdate = strdate.split('/');
    var day = new Date(arrdate[2], arrdate[1] - 1, arrdate[0]);
    return day;
}

//hàm biến ngày tháng thành chuỗi định dạng dd/mm/yyyy
function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
}

//hàm lấy ngày theo ngày hiện tại
function getDaterangeofDay(day) {

    if (getDateformatstring(day).getDate() == new Date().getDate()) {
        $('.btnLeftDay').parent().find('span').text("Hôm nay");
    } else if (getDateformatstring(day) < new Date() && getDateformatstring(day) > addDays(new Date(), -2)) {
        $('.btnLeftDay').parent().find('span').text("Hôm qua");
    } else if (getDateformatstring(day) > new Date() && getDateformatstring(day) < addDays(new Date(), 1)) {
        $('.btnLeftDay').parent().find('span').text("Ngày mai");
    } else {
        $('.btnLeftDay').parent().find('span').text(day);
    }
}

//hàm lấy tuần hiện tại
function getCurrentWeekNumber(d) {
    // Copy date so don't modify original
    d = new Date(+d);
    d.setHours(0, 0, 0);
    d.setDate(d.getDate() + 4 - (d.getDay() || 7));
    // Get first day of year
    var yearStart = new Date(d.getFullYear(), 0, 1);
    // Calculate full weeks to nearest Thursday
    var weekNo = Math.ceil((((d - yearStart) / 86400000) + 1) / 7);
    // Return array of year and week number
    return weekNo - 1;
}

//hàm lấy ngày đầu và cuối tuần khi biết tuần thứ bao nhiêu
var w2date = function (year, wn, dayNb) {
    var j10 = new Date(year, 0, 10, 12, 0, 0),
        j4 = new Date(year, 0, 4, 12, 0, 0),
        mon1 = j4.getTime() - j10.getDay() * 86400000;
    var date = new Date(mon1 + ((wn - 1) * 7 + dayNb) * 86400000);
    date = new Date(date.toDateString())
    return date;
};

//hàm định dạng cho ngày tháng
function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
}

//hàm lấy ngày tháng theo tuần
function getDaterangeofweek(week, year) {
    $("#period_week").find('span').text('(Từ ngày ' + convertDate(w2date(Number(year), Number(week), 0)) + ' đến ngày ' + convertDate(w2date(Number(year), Number(week), 6)) + ")")
    if (week == getCurrentWeekNumber(new Date())) {
        $('.btnLeftweek').parent().find('span').text("Tuần này");
    } else if (getCurrentWeekNumber(new Date()) - 2 < week && week < getCurrentWeekNumber(new Date())) {
        $('.btnLeftweek').parent().find('span').text("Tuần trước");
    } else if (getCurrentWeekNumber(new Date()) < week && week < getCurrentWeekNumber(new Date()) + 2) {
        $('.btnLeftweek').parent().find('span').text("Tuần sau");
    } else {
        $('.btnLeftweek').parent().find('span').text("Tuần " + week);
    }
}

function formatTimeServer(time) {
    if (!time) {
        return "";
    }
    function pad(s) { return (s < 10) ? '0' + s : s; }
    return time.getFullYear() + '-' + pad(time.getMonth() + 1) + '-' + pad(time.getDate()) + 'T' + pad(time.getHours()) + ':' + pad(time.getMinutes()) + ':' + pad(time.getSeconds());
}

function formatTimeTemplate(time) {
    //debugger
    var times = time.split("T")
    if (times.length == 2) {
        var arrDate = times[0].split('-');
        var arrTime = times[1].split(':');
        var month = Number(arrDate[1]);
        return arrTime[0] + ':' + arrTime[1] + ' ' + arrDate[2] + '/' + month + '/' + arrDate[0];
    }
}


// Class thực hiện convert ngày tháng và so sánh ngày
var dates = {
    convert: function (d) {
        // Converts the date in d to a date-object. The input can be:
        //   a date object: returned without modification
        //  an array      : Interpreted as [year,month,day]. NOTE: month is 0-11.
        //   a number     : Interpreted as number of milliseconds
        //                  since 1 Jan 1970 (a timestamp) 
        //   a string     : Any format supported by the javascript engine, like
        //                  "YYYY/MM/DD", "MM/DD/YYYY", "Jan 31 2009" etc.
        //  an object     : Interpreted as an object with year, month and date
        //                  attributes.  **NOTE** month is 0-11.
        return (
            d.constructor === Date ? d :
            d.constructor === Array ? new Date(d[0], d[1], d[2]) :
            d.constructor === Number ? new Date(d) :
            d.constructor === String ? new Date(d) :
            typeof d === "object" ? new Date(d.year, d.month, d.date) :
            NaN
        );
    },
    compare: function (a, b) {
        // Compare two dates (could be of any type supported by the convert
        // function above) and returns:
        //  -1 : if a < b
        //   0 : if a = b
        //   1 : if a > b
        // NaN : if a or b is an illegal date
        // NOTE: The code inside isFinite does an assignment (=).
        return (
            isFinite(a = this.convert(a).valueOf()) &&
            isFinite(b = this.convert(b).valueOf()) ?
            (a > b) - (a < b) :
            NaN
        );
    },
    inRange: function (d, start, end) {
        // Checks if date in d is between dates in start and end.
        // Returns a boolean or NaN:
        //    true  : if d is between start and end (inclusive)
        //    false : if d is before start or after end
        //    NaN   : if one or more of the dates is illegal.
        // NOTE: The code inside isFinite does an assignment (=).
        return (
             isFinite(d = this.convert(d).valueOf()) &&
             isFinite(start = this.convert(start).valueOf()) &&
             isFinite(end = this.convert(end).valueOf()) ?
             start <= d && d <= end :
             NaN
         );
    }
}