function sizeFormat(filesize) {
    if (filesize >= 1073741824) {
        filesize = numberFormat(filesize / 1073741824, 2, '.', '') + ' GB';
    } else {
        if (filesize >= 1048576) {
            filesize = numberFormat(filesize / 1048576, 2, '.', '') + ' MB';
        } else {
            if (filesize >= 1024) {
                filesize = numberFormat(filesize / 1024, 0) + ' KB';
            } else {
                filesize = numberFormat(filesize, 0) + ' bytes';
            };
        };
    };
    return filesize;
}

function numberFormat(number, decimals, decPoint, thousandsSep) {
    var n = number, c = isNaN(decimals = Math.abs(decimals)) ? 2 : decimals;
    var d = decPoint == undefined ? "," : decPoint;
    var t = thousandsSep == undefined ? "." : thousandsSep, s = n < 0 ? "-" : "";
    var i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;

    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}