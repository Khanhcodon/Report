    function getDiff(from, to, template) {
	    var arrIn = [];
	    var $inOutCodes = $("table").find("td[data-column='InOutCode']");
	    _.each($inOutCodes, function ($item) {
		    var text = $($item).text();
		    text = text || "";
		    text = text.replace(template);
		    text = text.trim();
		    arrIn.push(text);
	    })

	    var arrStandard = [];
	    for (var i = from; i <= to; i++) {
		    arrStandard.push(i + "");
	    }

	    var arr = _.difference(arrIn, arrStandard);
	    return arr;
    }