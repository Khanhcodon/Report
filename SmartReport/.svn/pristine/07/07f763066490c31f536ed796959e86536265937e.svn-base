(function ($, parser) {

    // Các kiểu dữ liệu: date, time, number, currency, text
    parser.mask = "text";

    // datetime format
    parser.datetimeFormat = "dd/MM/yy";

    // Số chữ số thập phân
    parser.places = 2;

    // Dấu ngăn cách phần nghìn
    parser.thousand = ".";
    
    // Dấu ngăn cách phần thập phân
    parser.decimal = ",";

    // Ký hiệu money (sử dụng cho dạng money)
    parser.suffix = "đ";

    // Dấu ngăn định dạng ngày tháng
    parser.formatSymbol = "/";
    
    var datatype = {text: "text", date: "date", time: "time", currency: "currency", number: "number"};

    parser.parse = function(value, $mask)
    {
        if ($mask != undefined) {
            parser.mask = $mask.Mask;
            parser.datetimeFormat = $mask.Format;
            parser.places = $mask.DecimalPlace;
            parser.thousand = $mask.ThousandSymbol;
            parser.decimal = $mask.DecimalSymbol;
            parser.suffix = $mask.Suffix;
            parser.formatSymbol = $mask.FormatSymbol;

            return this.parse(value);
        }
        else {
            return parseValue(value);
        }
    }

    function parseValue(value)
    {
        switch (parser.mask)
        {
            case datatype.number:
                return parseNumber(value);
            case datatype.currency:
                return parseCurrency(value);
            case datatype.date:
                return parseDate(value);
            case datatype.time:
                return parseTime(value);
            default: 
                return value;
        }
    }

    function parseNumber(value)
    {
        if (typeof value === "string")
        {
            value = parseFloat(value);
        }
        return value.formatNumber(parser.places, parser.thousand, parser.decimal);
    }

    function parseCurrency(value)
    {
        if (typeof value === "string") {
            value = parseFloat(value);
        }
        return value.formatMoney(parser.places, parser.suffix, parser.thousand, parser.decimal);
    }

    function parseDate(value)
    {
        var date = new Date(Date.parse(value));
        if (date == undefined)
        {
            return value;
        }
        return $.global.format(date, parser.datetimeFormat.replaceAll("/", parser.formatSymbol), $.global.culture);
    }

    function parseTime(value)
    {
        var objs = value.split(':');
        if (objs.length < 2)
        {
            return "";
        }
        var date = new Date(2012, 2, 2, objs[0], objs[1], objs.length == 2 ? 0 : objs[2]);
        return $.global.format(date, parser.datetimeFormat, $.global.culture);
    }
})
(window.jQuery, window.parser = window.parser || {})