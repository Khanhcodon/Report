
(function (egov) {
    egov.views = egov.views || {};

    require(["../../config"], function () {
        require(['egovCore', "ereportth"], function () {
            var report = new window.eReport({ callback: drawDashboard })
            drawDashboard();

            $(".urgency-fields").hover(function () {
                $(".urgencies").addClass("display");
            }, function () {
                $(".urgencies").removeClass("display");
            });
        });
    });

    function drawDashboard() {

    }
})(window.egov = window.egov || {});