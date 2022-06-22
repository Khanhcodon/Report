/*
* bkav.egov.Datatype
*
* Copyright 2012:  Bkav - Bso - Phòng 2 - eGate Team
* Created by: TienBV@bkav.com.vn
* Edited by:
* Requirement: jquery > 1.4
* Mô tả: thêm, override các api xử lý trên các datatype
*/
(function ($) {

    /* String Prototype ====================================================================================*/

    /// <summary>So sánh chuỗi không ép kiểu</summary>
    /// <param name="$target" type="String">Đối tượng được so sánh</param>
    String.prototype.equals = function ($target) {
        return this.toString() === $target;
    };

    /// <summary>So sánh bằng hoặc bằng</summary>
    /// <param name="arg" type="String">Danh sách các chuỗi cần so sánh  $target1, $targer2, $targer3</param>
    String.prototype.equalsOr = function () {
        var result = false;
        var args = arguments;
        for (var i = 0; i < args.length; i++) {
            if (typeof (args[i]).equals("string")) {
                result |= this.equals(args[i]);
            }
        }
        return result;
    };

    /// <summary>Replace all</summary>
    /// <param name="token" type="String">Chuỗi cần thay thế</param>
    /// <param name="newToken" type="String">Chuỗi được thay thế</param>
    /// <param name="ignoreCase" type="String">Không phân biệt chữ hoa thường</param>
    String.prototype.replaceAll = function (token, newToken, ignoreCase) {
        var _token;
        var result = this + "";
        var i = -1;

        if (typeof token === "string") {
            if (ignoreCase) {
                _token = token.toLowerCase();
                while ((
                    i = result.toLowerCase().indexOf(
                        token, i >= 0 ? i + newToken.length : 0
                    )) !== -1
                ) {
                    result = result.substring(0, i) +
                        newToken +
                        result.substring(i + token.length);
                }
            } else {
                return this.split(token).join(newToken);
            }
        }
        return result;
    };
    
    /*  Number Format ==================================================================================*/

    /// <summary>Money format</summary>
    /// <param name="places" type="String">Số chữ số thập phân</param>
    /// <param name="symbol" type="String">Ký hiệu loại tiền</param>
    /// <param name="thousand" type="String">Dấu ngăn cách phần nghìn</param>
    /// <param name="decimal" type="String">Dấu ngăn cách phần thập phân</param>
    Number.prototype.formatMoney = function (places, symbol, thousand, decimal) {
        places = !isNaN(places = Math.abs(places)) ? places : 2;
        symbol = symbol !== undefined ? symbol : "";
        thousand = thousand || "";
        decimal = decimal || "";
        var number = this,
            negative = number < 0 ? "-" : "",
            i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
            j = (j = i.length) > 3 ? j % 3 : 0;
        return negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "") + symbol;
    };

    /// <summary>Định dạng số theo chuẩn</summary>
    /// <param name="places" type="String">Số chữ số thập phân</param>
    /// <param name="thousand" type="String">Dấu ngăn cách phần nghìn</param>
    /// <param name="decimal" type="String">Dấu ngăn cách phần thập phân</param>
    Number.prototype.formatNumber = function (places, thousand, decimal)
    {
        return this.formatMoney(places, "", thousand, decimal);
    }
})
(window.jQuery);