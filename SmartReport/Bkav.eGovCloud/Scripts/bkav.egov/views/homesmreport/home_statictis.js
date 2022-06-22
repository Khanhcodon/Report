
(function () {

    //#region XLVB

    function showXlvb() {
        _xlvb_bindTotal();
        _xlvb_bindDetail();
    }

    var _xlvb_bindTotal = function () {
        var hsmcStatElement = $(".xlvb-stat");
        hsmcStatElement.find(".statistic-time").text("Tháng " + 1 + "/" + 2019);
        hsmcStatElement.find(".statistic-percen").text(data.xlvb.unduePercen + "%");
        hsmcStatElement.find(".statistic-piechat").show();
        $("#xlvb_piechart").empty();
        var model = google.visualization.arrayToDataTable([
            ['Task', 'Thống kê'],
            ['Đúng hẹn', data.xlvb.Undue],
            ['Khác', data.xlvb.Total - data.xlvb.Undue]
        ]);

        if ($("#xlvb_piechart").length > 0) {
            var options = {
                chartArea: { width: '90%', height: '90%' },
                colors: ['#48b9dd', '#f3f3f3'],
                legend: 'none',
                pieSliceText: 'none',
                pieHole: 0.8,
                animation: {
                    duration: 500
                }
            };

            var chart = new google.visualization.PieChart(document.getElementById('xlvb_piechart'));
            chart.draw(model, options);
        }
    };

    var _xlvb_bindDetail = function () {
        var model = google.visualization.arrayToDataTable(data.xlvb.months);

        var options = {
            colors: ['#48B9DD', '#ff5f11'],
            bars: 'horizontal',
            width: 650,
            hAxis: { direction: 1, slantedText: true, slantedTextAngle: 30, textStyle: { fontSize: 11 } }
        };

        var chart = new google.visualization.BarChart(document.getElementById('xlvb_columnChart'));

        chart.draw(model, options);
    };

    //#endregion

    //#region HSMC

    function showHsmc() {
        _hsmc_bindTotal();
        _hsmc_bindDetail();
    }
    
    var _hsmc_bindTotal = function () {
        var hsmcStatElement = $(".hsmc-stat");
        hsmcStatElement.find(".statistic-time").text("Tháng " + 1 + "/" + 2019);
        hsmcStatElement.find(".statistic-percen").text(data.hsmc.unduePercen + "%");
        hsmcStatElement.find(".statistic-piechat").show();
        $("#hsmc_piechart").empty();
        var model = google.visualization.arrayToDataTable([
            ['Task', 'Thống kê'],
            ['Đúng hẹn', data.hsmc.Undue],
            ['Khác', data.hsmc.Total - data.hsmc.Undue]
        ]);

        if ($("#hsmc_piechart").length > 0) {
            var options = {
                chartArea: { width: '90%', height: '90%' },
                colors: ['#48b9dd', '#f3f3f3'],
                legend: 'none',
                pieSliceText: 'none',
                pieHole: 0.8,
                animation: {
                    duration: 500
                }
            };

            var chart = new google.visualization.PieChart(document.getElementById('hsmc_piechart'));
            chart.draw(model, options);
        }
        $("#statisticOffice").addClass("carousel slide");
    };

    var _hsmc_bindDetail = function () {
        var that = this;
        that.template = $("#statisticTemp");
        var idx = 0;
        data.hsmc.Offices.forEach(function (office) {
            var id = "office" + office.OfficeId;

            // Indicators
            var indicator = $("<li>").attr("data-target", "#statisticOffice").attr("data-slide-to", idx);
            indicator.attr("title", office.OfficeName);
            indicator.attr("data-toggle", "tooltip");

            // Slide
            var slide = $("<div>").attr("id", "office" + office.OfficeId).addClass("item");
            slide.html($.tmpl(that.template, office));

            if (idx === 0) {
                indicator.addClass("active");
                slide.addClass("active");
            }

            $("#statisticOffice").find(".carousel-indicators").append(indicator);
            $("#statisticOffice").find(".carousel-inner").append(slide);
            idx++;
        });
        $("#statisticOffice").tooltip();        
        $("#statisticOffice").carousel();
    };

    //#endregion

    //#region DVC

    function showDvc() {
        _dvc_bindTotal();
        _dvc_bindDetail();
    }

    var _dvc_bindTotal = function () {
        var hsmcStatElement = $(".dvc-stat");
        hsmcStatElement.find(".statistic-time").text("Tháng " + 1 + "/" + 2019);
        hsmcStatElement.find(".statistic-percen").text(data.dvc.unduePercen + "%");
        hsmcStatElement.find(".statistic-piechat").show();
        $("#dvc_piechart").empty();
        var model = google.visualization.arrayToDataTable([
            ['Task', 'Thống kê'],
            ['Đúng hẹn', data.dvc.Undue],
            ['Khác', data.dvc.Total - data.dvc.Undue]
        ]);

        if ($("#dvc_piechart").length > 0) {
            var options = {
                chartArea: { width: '90%', height: '90%' },
                colors: ['#48b9dd', '#f3f3f3'],
                legend: 'none',
                pieSliceText: 'none',
                pieHole: 0.8,
                animation: {
                    duration: 500
                }
            };

            var chart = new google.visualization.PieChart(document.getElementById('dvc_piechart'));
            chart.draw(model, options);
        }
    };

    var _dvc_bindDetail = function () {
        var model = google.visualization.arrayToDataTable(data.dvc.months);

        var options = {
            colors: ['#48B9DD', '#ff5f11'],
            chartArea: { width: '90%', height: '350px' },
            legend: { position: 'bottom', maxLines: 1 },
            hAxis: { title: "Tháng", direction: 1, slantedText: true, slantedTextAngle: 30, textStyle: { fontSize: 11 } }
        };

        var chart = new google.visualization.ColumnChart(document.getElementById('dvc_columnChart'));

        chart.draw(model, options);
    }

    //#endregion

    window.homeStatictis = {
        showXlvb: showXlvb,
        showHsmc: showHsmc,
        showDvc: showDvc
    };

    //#region datas

    var data = {
        xlvb: { "Undue": 976, "Total": 1117, "unduePercen": 87, "months": [["Loại văn bản", "Số văn bản", "Chưa xử lý"], ["Văn bản đến", 934, 345], ["Văn bản đi", 587, 432], ["VB liên thông", 431, 23], ["Văn bản hồi báo", 432, 23]] },
        hsmc: {"Undue":3011,"Total":3115,"Offices":[{"OfficeName":"Sở Thông tin và Truyền thông","OnlineUrl":"http://motcua.sotttt.baria-vungtau.gov.vn","OfficeId":34,"ServiceUrl":"http://motcua.sotttt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":10,"Pending":5,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":5,"SolvedInTime":9,"SolvedInTimePercent":"90","SolvedLate":1,"SolvedLatePercent":"10","Total":15,"TotalPending":5,"TotalSolved":10}},{"OfficeName":"Sở Kế hoạch - Đầu Tư BRVT","OnlineUrl":"http://motcua.sokhdt.baria-vungtau.gov.vn","OfficeId":45,"ServiceUrl":"http://motcua.sokhdt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":28,"Pending":58,"PendingLate":2,"PendingLatePercent":"3.3","PendingPercent":"96.7","PreExtisting":46,"SolvedInTime":14,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":74,"TotalPending":60,"TotalSolved":14}},{"OfficeName":"Ban Quản lý Khu công nghiệp BRVT","OnlineUrl":"http://motcua.banqlkcn.baria-vungtau.gov.vn","OfficeId":46,"ServiceUrl":"http://motcua.banqlkcn.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":100,"Pending":66,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":42,"SolvedInTime":76,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":142,"TotalPending":66,"TotalSolved":76}},{"OfficeName":"Sở Khoa Học Công Nghệ BRVT","OnlineUrl":"http://motcua.sokhcn.baria-vungtau.gov.vn","OfficeId":47,"ServiceUrl":"http://motcua.sokhcn.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":12,"Pending":7,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":7,"SolvedInTime":12,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":19,"TotalPending":7,"TotalSolved":12}},{"OfficeName":"Sở Nội vụ BRVT","OnlineUrl":"http://motcua.sonoivu.baria-vungtau.gov.vn","OfficeId":48,"ServiceUrl":"http://motcua.sonoivu.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":22,"Pending":66,"PendingLate":4,"PendingLatePercent":"5.7","PendingPercent":"94.3","PreExtisting":60,"SolvedInTime":12,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":82,"TotalPending":70,"TotalSolved":12}},{"OfficeName":"Ban Dân Tộc BRVT","OnlineUrl":"http://motcua.bandt.baria-vungtau.gov.vn/","OfficeId":49,"ServiceUrl":"http://motcua.bandt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":0,"Pending":0,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"0","PreExtisting":0,"SolvedInTime":0,"SolvedInTimePercent":100,"SolvedLate":0,"SolvedLatePercent":"0","Total":0,"TotalPending":0,"TotalSolved":0}},{"OfficeName":"Sở Giao thông Vận tải BRVT","OnlineUrl":"http://motcua.sogtvt.baria-vungtau.gov.vn","OfficeId":50,"ServiceUrl":"http://motcua.sogtvt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":45,"Pending":35,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":29,"SolvedInTime":39,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":74,"TotalPending":35,"TotalSolved":39}},{"OfficeName":"Sở Nông nghiệp và Phát triển Nông Thôn","OnlineUrl":"http://motcua.sonnptnt.baria-vungtau.gov.vn","OfficeId":51,"ServiceUrl":"http://motcua.sonnptnt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":491,"Pending":196,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":106,"SolvedInTime":401,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":597,"TotalPending":196,"TotalSolved":401}},{"OfficeName":"Sở Y Tế BRVT","OnlineUrl":"http://motcua.soyte.baria-vungtau.gov.vn","OfficeId":52,"ServiceUrl":"http://motcua.soyte.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":51,"Pending":133,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":150,"SolvedInTime":68,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":201,"TotalPending":133,"TotalSolved":68}},{"OfficeName":"Sở Lao Động Thương binh Xã hội BRVT","OnlineUrl":"http://motcua.soldtbxh.baria-vungtau.gov.vn","OfficeId":53,"ServiceUrl":"http://motcua.soldtbxh.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":53,"Pending":35,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":35,"SolvedInTime":53,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":88,"TotalPending":35,"TotalSolved":53}},{"OfficeName":"Sở Tư pháp BRVT","OnlineUrl":"http://motcua.sotp.baria-vungtau.gov.vn","OfficeId":54,"ServiceUrl":"http://motcua.sotp.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":447,"Pending":536,"PendingLate":37,"PendingLatePercent":"6.5","PendingPercent":"93.5","PreExtisting":525,"SolvedInTime":389,"SolvedInTimePercent":"97.5","SolvedLate":10,"SolvedLatePercent":"2.5","Total":972,"TotalPending":573,"TotalSolved":399}},{"OfficeName":"Sở Ngoại vụ BRVT","OnlineUrl":"http://motcua.songoaivu.baria-vungtau.gov.vn","OfficeId":55,"ServiceUrl":"http://motcua.songoaivu.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":0,"Pending":1,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":1,"SolvedInTime":0,"SolvedInTimePercent":100,"SolvedLate":0,"SolvedLatePercent":"0","Total":1,"TotalPending":1,"TotalSolved":0}},{"OfficeName":"Sở Công Thương BRVT","OnlineUrl":"http://motcua.soct.baria-vungtau.gov.vn","OfficeId":56,"ServiceUrl":"http://motcua.soct.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":224,"Pending":14,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":19,"SolvedInTime":229,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":243,"TotalPending":14,"TotalSolved":229}},{"OfficeName":"Sở Văn hóa và Thể thao","OnlineUrl":"http://motcua.sovhttdl.baria-vungtau.gov.vn","OfficeId":57,"ServiceUrl":"http://motcua.sovhttdl.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":24,"Pending":37,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":31,"SolvedInTime":18,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":55,"TotalPending":37,"TotalSolved":18}},{"OfficeName":"Sở Xây dựng BRVT","OnlineUrl":"http://motcua.soxd.baria-vungtau.gov.vn","OfficeId":58,"ServiceUrl":"http://motcua.soxd.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":51,"Pending":71,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":61,"SolvedInTime":41,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":112,"TotalPending":71,"TotalSolved":41}},{"OfficeName":"Sở Giáo dục Đào tạo BRVT","OnlineUrl":"http://motcua.sogddt.baria-vungtau.gov.vn","OfficeId":59,"ServiceUrl":"http://motcua.sogddt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":75,"Pending":24,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":27,"SolvedInTime":78,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":102,"TotalPending":24,"TotalSolved":78}},{"OfficeName":"Văn phòng UBND","OnlineUrl":"http://egovvpub.baria-vungtau.gov.vn","OfficeId":61,"ServiceUrl":"http://egovvpub.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":39,"Pending":43,"PendingLate":42,"PendingLatePercent":"49.4","PendingPercent":"50.6","PreExtisting":69,"SolvedInTime":16,"SolvedInTimePercent":"69.6","SolvedLate":7,"SolvedLatePercent":"30.4","Total":108,"TotalPending":85,"TotalSolved":23}},{"OfficeName":"Sở Tài nguyên Môi trường BRVT","OnlineUrl":"http://motcua.sotnmt.baria-vungtau.gov.vn","OfficeId":62,"ServiceUrl":"http://motcua.sotnmt.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":66,"Pending":157,"PendingLate":1,"PendingLatePercent":"0.6","PendingPercent":"99.4","PreExtisting":150,"SolvedInTime":58,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":216,"TotalPending":158,"TotalSolved":58}},{"OfficeName":"Sở Du Lịch","OnlineUrl":"http://motcua.sodl.baria-vungtau.gov.vn","OfficeId":63,"ServiceUrl":"http://motcua.sodl.baria-vungtau.gov.vn/webapi/","error":false,"Statistic":{"Name":null,"NewReception":4,"Pending":13,"PendingLate":0,"PendingLatePercent":"0","PendingPercent":"100","PreExtisting":10,"SolvedInTime":1,"SolvedInTimePercent":"100","SolvedLate":0,"SolvedLatePercent":"0","Total":14,"TotalPending":13,"TotalSolved":1}}],"Services":[],"unduePercen":97},
        dvc: { "Undue": 976, "Total": 1117, "unduePercen": 87, "months": [["Tháng", "Hồ sơ đăng ký", "Chưa xử lý"], ["Tháng 1", 233, 12], ["Tháng 2", 342, 54], ["Tháng 3", 431, 23], ["Tháng 4", 987, 122], ["Tháng 5", 576, 12], ["Tháng 6", 678, 32], ["Tháng 7", 876, 123], ["Tháng 8", 768, 98], ["Tháng 9", 786, 198], ["Tháng 10", 1033, 201], ["Tháng 11", 454, 12], ["Tháng 12", 566, 12]] }
    };

    //#endregion
})()