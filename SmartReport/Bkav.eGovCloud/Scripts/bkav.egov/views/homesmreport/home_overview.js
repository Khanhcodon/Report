
(function () {

    var contentElement = $("#appContent");

    function showCalendar() {
        var template = $('#calendar');
        var _bindCalendar = function (calendars) {
            calendars.data = _parseGroup(calendars.data);

            $('.calendar-content').html($.tmpl(template, calendars));
        };

        $('#menu-calendar li').click(function (e) {
            var li = $(e.target).closest('li');
            li.addClass("selected");
            li.siblings().removeClass("selected");
            var url = li.attr('data-url');
            _getData(url, {}, _bindCalendar.bind(this));
        });

        _getData('/calendar/GetWeekly', {}, _bindCalendar.bind(this));
    }

    function showHsmc() {
        var template = $('#hsmc');
        var _bindDocument = function (documents) {
            $('.hsmc-content').html($.tmpl(template, { name: "Hồ sơ chờ xử lý", data: documents.result }));
        };

        _getData('/mobile/GetProcessings', { c: 4 }, _bindDocument.bind(this));
    }

    function showXlvb() {
        var template = $('#xlvb');
        var _bindDocument = function (documents) {
            $('.xlvb-content').html($.tmpl(template, { name: "Văn bản chờ xử lý", data: documents.result }));
        };

        _getData('/mobile/GetProcessings', {}, _bindDocument.bind(this));
    }

    function showEmail() {
        var template = $('#xlvb');
        var _bindDocument = function (documents) {
            $('.bmail-content').html($.tmpl(template, { name: "Văn bản chờ xử lý", data: documents.result }));
        };

        _getData('/mobile/GetProcessings', {}, _bindDocument.bind(this));
    }

    function showBmm() {
        var template = $('#xlvb');
        var _bindDocument = function (documents) {
            $('.bmm-content').html($.tmpl(template, { name: "Văn bản chờ xử lý", data: documents.result }));
        };

        _getData('/mobile/GetProcessings', {}, _bindDocument.bind(this));
    }

    function _getData(url, data, success) {
        $.ajax({
            url: url,
            type: 'Get',
            data: data,
            beforeSend: function () {

            },
            success: function (result) {
                success(result);
            }
        });
    }

    //#region Private

    function _parseGroup(data) {
        if (!data || data.length === 0) {
            return [];
        }

        var groups = _.groupBy(data, 'Date');
        var that = this;
        var result = [];

        _.each(groups, function (group, key) {
            var rowSpan = group.length + 1; // 1 là row mặc định của group
            _.each(group, function (calendar) {
                calendar.IsMe = calendar.UserCreatedId == that.userId;

                if (calendar.Contents.length > 1) {
                    rowSpan += calendar.Contents.length;
                }
            });

            result.push({
                Date: key,
                BeginTime: group[0].BeginTime,
                IsAdmin: that.isAdmin,
                Count: rowSpan,
                Calendars: group
            });
        });

        result = _.sortBy(result, "BeginTime");

        return result;
    };

    //#endregion

    window.overview = {
        showCalendar: showCalendar,
        showHsmc: showHsmc,
        showXlvb: showXlvb,
        showEmail: showEmail,
        showBmm: showBmm
    };

})()