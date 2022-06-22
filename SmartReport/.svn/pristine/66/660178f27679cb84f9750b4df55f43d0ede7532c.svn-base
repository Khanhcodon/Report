$('#TimeKey').on('change', function () {
    var time = $('#TimeKey').val();
    $('.inputYear').addClass('addClassDisplay');
    var string;
    if (time == 1) {
        $('.inputMuls').removeClass('addClassDisplay').addClass('inputMul');
    } else if(time == -1) {
        $('.inputYear').addClass('inputMul');
    } else {
        $('.inputMuls').removeClass('inputMul').addClass('addClassDisplay');
    }
        
    $(".inputMuls").find('.form-group').remove();
    $(".inputMuls").append('<div class="form-group"><div class="input-group"></div></div>');
    if (time == 8) {
        string = "<input type='text' id='TimeKey9Month' name='TimeKey9Month' class='form-control checkTimeKey' placeholder='Nhập thời gian' value='9' readonly/>";
        $('.inputMuls').find('.form-group .input-group').append(string);
    } else if(time == 2) {
        string = "<select class='form-control select2 w-p100' id='TimeKey6Month' name='TimeKey6Month'>"
                + "<option value='1' selected='selected'>Đầu năm</option><option value='2'>Cuối năm</option></select>";
        $('.inputMuls').find('.form-group .input-group').append(string);
    } else if (time == 3) {
        string = "<select class='form-control select2 w-p100' id='TimeKeyQuy' name='TimeKeyQuy'>"
            + "<option value='1' selected='selected'>Quý 1</option><option value='2'>Quý 2</option> <option value='3'>Quý 3</option><option value='4'>Quý 4</option>  </select>";
        $('.inputMuls').find('.form-group .input-group').append(string);
    } else {
        string = "<select class='form-control select2 w-p100' id='TimeKeyMonth' name='TimeKeyMonth'>"
            + "<option value='1' selected='selected'>Tháng 1</option><option value='2'>Tháng 2</option> <option value='3'>Tháng 3</option><option value='4'>Tháng 4</option>  "
            + "<option value='5' >Tháng 5</option><option value='6'>Tháng 6</option> <option value='7'>Tháng 7</option><option value='8'>Tháng 8</option>  "
            + "<option value='9' >Tháng 9</option><option value='10'>Tháng 10</option> <option value='11'>Tháng 11</option><option value='12'>Tháng 12</option> </select>";
        $('.inputMuls').find('.form-group .input-group').append(string);
    }
});

$('#docRelated li').click(function () {
    $('#staticResultDetail').removeClass("addClassDisplay");
    $('#staticResultDetail').hide();
    $('#staticResult').show();
    // $('#docRelated li').removeClass('boldClass');
    if ($(this).hasClass('boldClass')) {
        $('#docRelated li').removeClass('boldClass');
    } else {
        $(this).addClass('boldClass');
    }
})

var resultTotal;
$.ajax({
    url: "/dashboard/GetReportStatus",
    data: { reportModeid: -1, actionCode: -1, timekey: '', getss: true },
    beforeSend: function () {

    },
    success: function (result) {
        resultTotal = result;
        var BcSLChuaXuLy = 0, BCTMChuaXuLy = 0, BCSLDangXuLy = 0, BCTMDangXuly = 0, BCSLDaXuLy = 0, BCTMDaXuLy = 0, BCSLLuuNhap = 0, BCTMLuuNhap = 0;
        for (var i = 0 ; i < result.length; i++) {
            var parseJsonResult = result[i].reportDoctype;
            for (var j = 0 ; j < parseJsonResult.length ; j++) {
                if (parseJsonResult[j].Status == 0 && parseJsonResult[j].CategoryBussiness == 4) {
                    BcSLChuaXuLy++;
                }
                if (parseJsonResult[j].Status == 0 && parseJsonResult[j].CategoryBussiness == 8) {
                    BCTMChuaXuLy++;
                }
                if (parseJsonResult[j].Status == 2 && parseJsonResult[j].CategoryBussiness == 4) {
                    BCSLDangXuLy++;
                }
                if (parseJsonResult[j].Status == 2 && parseJsonResult[j].CategoryBussiness == 8) {
                    BCTMDangXuly++;
                }
                if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                    && parseJsonResult[j].CategoryBussiness == 4) {
                    BCSLDaXuLy++;
                }
                if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                    && parseJsonResult[j].CategoryBussiness == 8) {
                    BCTMDaXuLy++;
                }
                if (parseJsonResult[j].Status == 1 && parseJsonResult[j].CategoryBussiness == 4) {
                    BCSLLuuNhap++;
                }
                if (parseJsonResult[j].Status == 1 && parseJsonResult[j].CategoryBussiness == 8) {
                    BCTMLuuNhap++;
                }
            }
        }
        Highcharts.chart('container-chart', {
            colors: ['#4379DE', '#FF5F75'],
            credits: {
                enabled: false
            },
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: [
                    'Báo cáo số liệu', 
                ],
                crosshair: true,
                labels: {
                    style: {
                        fontSize: '14px',
                        fontFamily: 'Roboto-Regular'
                    }
                }
            },
            legend: {
                enabled: false
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                },
                labels: {
                    style: {
                        fontSize: '12px',
                        fontFamily: 'Roboto-Regular'
                    }
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: -0.5,
                    borderWidth: 0,
                    groupPadding: 0.2,
                    pointWidth: 14,
                    padding: 0
                }
            },
            series: [{
                name: 'Báo cáo số liệu chưa xử lý',
                data: [BcSLChuaXuLy]
            }, {
                name: 'Báo cáo số liệu đang lưu lưu nháp',
                data: [BCSLLuuNhap]
            }, {
                name: 'Báo cáo số liệu đang xử lý',
                data: [BCSLDangXuLy]
            }, {
                name: 'Báo cáo số liệu đã xử lý',
                data: [BCSLDaXuLy]
            }
            ]
        });
        Highcharts.chart('container-chart-pie', {
            colors: ['#4379DE', '#FF5F75'],
            credits: {
                enabled: false
            },
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: [
                    'Báo cáo thuyết minh',
                ],
                crosshair: true,
                labels: {
                    style: {
                        fontSize: '14px',
                        fontFamily: 'Roboto-Regular'
                    }
                }
            },
            legend: {
                enabled: false
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                },
                labels: {
                    style: {
                        fontSize: '12px',
                        fontFamily: 'Roboto-Regular'
                    }
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: -0.5,
                    borderWidth: 0,
                    groupPadding: 0.2,
                    pointWidth: 14,
                    padding: 0
                }
            },
            series: [{
                name: 'Báo cáo thuyết minh chưa xử lý',
                data: [BCTMChuaXuLy]
            }, {
                name: 'Báo cáo thuyết minh đang lưu nháp',
                data: [BCTMLuuNhap]
            }, {
                name: 'Báo cáo thuyết minh chờ xử lý',
                data: [BCTMDangXuly]
            }, {
                name: 'Báo cáo thuyết minh đã xử lý',
                data: [BCTMDaXuLy]
            }
            ]
        });

        $('#bcsl').find('ul li.cbc').append("<span class='blue'>" + BcSLChuaXuLy + "</span>")
        $('#bcsl').find('ul li.ln').append("<span class='blue'>" + BCSLLuuNhap + "</span>")
        $('#bcsl').find('ul li.cxl').append("<span class='blue'>" + BCSLDangXuLy + "</span>")
        $('#bcsl').find('ul li.dd').append("<span class='blue'>" + BCSLDaXuLy + "</span>")
        $('#bctm').find('ul li.cbc').append("<span class='blue'>" + BCTMChuaXuLy + "</span>")
        $('#bctm').find('ul li.ln').append("<span class='blue'>" + BCTMLuuNhap + "</span>")
        $('#bctm').find('ul li.cxl').append("<span class='blue'>" + BCTMDangXuly + "</span>")
        $('#bctm').find('ul li.dd').append("<span class='blue'>" + BCTMDaXuLy + "</span>")
    },
    error: function (xhr) { },
    complete: function () {

    }
});


var resultShow;
function ViewDetail(doctypeID, reportmode) {
    $('#staticResultDetail').find('tbody').empty();
    for (var i = 0 ; i < resultShow.length; i++) {
        var parserTotal = resultShow[i].doctype;
        if (doctypeID == parserTotal.DocTypeId) {
            var parseJsonResults = resultShow[i].reportDoctype;
            for (var j = 0 ; j < parseJsonResults.length ; j++) {
                var parseJsonResult = parseJsonResults[j];
                var stringThead = "<tr style='text-align:center'><th hidden style='font-weight: 700; font-size:14px;height: 50px;padding-top: 16px;'>"
                    + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${Stt}</th>"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${CompendiumDefault}</th>"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${UserName}</th>"
                + "{{if CategoryBussiness == 1}}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'></th>"
                + "{{else CategoryBussiness == 4}}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;color:mediumvioletred'>Báo cáo số liệu</th>"
                + "{{else CategoryBussiness == 8}}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;color:#4915c7'>Báo cáo thuyết minh</th> "
                + "{{else}}<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'></th>{{/if}}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${ActionLevel}</th>"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${OrganizeKey}</th>"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${DepartmentName}</th>"
                + "{{if (Status == 2 || Status == 4) && StatusReport == 4 }}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;color:blueviolet'>Đã phát hành</th>"
                + "{{else Status == 1 && Status == 0}}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;color:#728390'>Lưu nháp</th>"
                + "{{else Status == 2}}"
                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;color:#dc4141'>Chờ xử lý</th> "
                + "{{else}}<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;color:cadetblue'>Chưa báo cáo</th>{{/if}}";        
                var data = $.tmpl(stringThead, parseJsonResult);         
                $('#staticResultDetail').addClass('addClassDisplay');
                $('#staticResultDetail').find('tbody').append(data);
            }
        }
    }
    //show modal
    $('#detailbc').modal('show');
}

function ConvertTime(typeTime, typeTimeKey) {
    var timeKey;
    if (typeTimeKey == 1) {
        timeKey = typeTime;
    } else if (typeTimeKey == 8) {
        var time9Month = $('#TimeKey9Month').val();
        timeKey = typeTime + "0" + time9Month;
    } else if (typeTimeKey == 2) {
        var timeYearQuy = $('#TimeKey6Month').val();
        timeKey = typeTime + "" + timeYearQuy;
    } else if (typeTimeKey == 3) {
        var timeYearQuy = $('#TimeKeyQuy').val();
        timeKey = typeTime + "" + timeYearQuy;
    } else {
        //4
        var TimeMonth = $('#TimeKeyMonth').val();
        if (TimeMonth < 10) {
            timeKey = typeTime + "0" + TimeMonth;
        } else {
            timeKey = typeTime + "" + TimeMonth;
        }
    }
    return timeKey;
}

function ViewDetaiFlow() {
    $('#staticResult').find('tbody').empty();
    $('#docRelated').each(function () {
        $(this).find('li').each(function () {
            if ($(this).hasClass('boldClass')) {
                var typeTimess = parseInt($('#TimeYear').val());
                var typeTimeKey = $('#TimeKey').val();

                var timeKey = ConvertTime(typeTimess, typeTimeKey);

                var docmodesId = $(this).attr('dataid');

                //ajax
                $.ajax({
                    url: "/dashboard/GetReportStatus",
                    data: { reportModeid: docmodesId, actionCode: typeTimeKey, timekey_: timeKey, getss: false },
                    beforeSend: function () {},
                    success: function (result) {
                        resultShow = result;
                        $('#staticResult').find('tbody tr').remove();
                        var t1 = 0; t2 = 0; t3 = 0; t4 = 0; t5 = 0; t6 = 0; t7 = 0; t7 = 0, t8 = 0;
                        for (var i = 0 ; i < result.length; i++) {
                            var parserTotal = result[i].doctype;
                            var arrayDoctype = [], tt = 0, objDoctype = {};
                            objDoctype.DocTypeId = parserTotal.DocTypeId;
                            objDoctype.CategoryBusinessId = parserTotal.CategoryBusinessId;
                            objDoctype.DocTypeName = parserTotal.DocTypeName;
                            objDoctype.docmodeId = parserTotal.ReportModeId;

                            var BcSLChuaXuLy = 0, BCTMChuaXuLy = 0, BCSLDangXuLy = 0, BCTMDangXuly = 0, BCSLDaXuLy = 0, BCTMDaXuLy = 0, BCSLLuuNhap = 0, BCTMLuuNhap = 0;
                                var parseJsonResult = result[i].reportDoctype;
                                for (var j = 0 ; j < parseJsonResult.length ; j++) {
                                    if (parseJsonResult[j].Status == 0 && parseJsonResult[j].CategoryBussiness == 4) {
                                        BcSLChuaXuLy++;
                                    }
                                    if (parseJsonResult[j].Status == 0 && parseJsonResult[j].CategoryBussiness == 8) {
                                        BCTMChuaXuLy++;
                                    }
                                    if (parseJsonResult[j].Status == 2 && parseJsonResult[j].CategoryBussiness == 4) {
                                        BCSLDangXuLy++;
                                    }
                                    if (parseJsonResult[j].Status == 2 && parseJsonResult[j].CategoryBussiness == 8) {
                                        BCTMDangXuly++;
                                    }
                                    if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                                        && parseJsonResult[j].CategoryBussiness == 4) {
                                        BCSLDaXuLy++;
                                    }
                                    if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                                        && parseJsonResult[j].CategoryBussiness == 8) {
                                        BCTMDaXuLy++;
                                    }
                                    if (parseJsonResult[j].Status == 1 && parseJsonResult[j].CategoryBussiness == 4) {
                                        BCSLLuuNhap++;
                                    }
                                    if (parseJsonResult[j].Status == 1 && parseJsonResult[j].CategoryBussiness == 8) {
                                        BCTMLuuNhap++;
                                    }
                                }
                            var totalCount = BcSLChuaXuLy + BCTMChuaXuLy + BCSLDangXuLy +
                                BCTMDangXuly + BCSLDaXuLy + BCTMDaXuLy + BCSLLuuNhap + BCTMLuuNhap;

                            objDoctype.BcSLChuaXuLy = BcSLChuaXuLy;
                            objDoctype.BCTMChuaXuLy = BCTMChuaXuLy;
                            objDoctype.BCSLDangXuLy = BCSLDangXuLy;
                            objDoctype.BCTMDangXuly = BCTMDangXuly;
                            objDoctype.BCSLDaXuLy = BCSLDaXuLy;
                            objDoctype.BCTMDaXuLy = BCTMDaXuLy;
                            objDoctype.BCSLLuuNhap = BCSLLuuNhap;
                            objDoctype.BCTMLuuNhap = BCTMLuuNhap;
                            objDoctype.stt = i;
                            objDoctype.TotalCount = totalCount;
                            arrayDoctype.push(objDoctype);

                            t1 += BcSLChuaXuLy;
                            t2 += BCTMChuaXuLy;
                            t3 += BCSLDangXuLy;
                            t4 += BCTMDangXuly;
                            t5 += BCSLDaXuLy;
                            t6 += BCTMDaXuLy;
                            t7 += BCSLLuuNhap;
                            t8 += BCTMLuuNhap;


                            var stringThead = "<tr style='text-align:center'><th hidden style='font-weight: 700; font-size:14px;height: 50px;padding-top: 16px;'>"
                                + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${stt}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;' hidden>${DocTypeId}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;' hidden>${docmodeId}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${DocTypeName}</th>"
                            + "{{if CategoryBusinessId == 1}}"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'></th>"
                            + "{{else CategoryBusinessId == 4}}"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>Báo cáo số liệu</th>"
                            + "{{else CategoryBusinessId == 8}}"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>Báo cáo thuyết minh</th> "
                            + "{{else}}<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'></th>{{/if}}"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BcSLChuaXuLy} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCTMChuaXuLy} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCSLDangXuLy} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCTMDangXuly} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCSLDaXuLy} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCTMDaXuLy} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCSLLuuNhap} \\ ${TotalCount}</th>"
                            + "<th style='font-size:12px;text-align: center;height: 50px;padding-top: 16px;'>${BCTMLuuNhap} \\ ${TotalCount}</th>"
                            + "<td style='text-align:center;font-size:12px;text-align: center;height: 50px;padding-top: 16px;' class='view'>"
                            + "<i class='fa fa-eye-slash ViewDetail' onclick=ViewDetail('${DocTypeId}','${docmodeId}'); data-toggle='modal' data-target='#ViewModalProsess' style='font-size: 14px;padding-right: 5px;'></i>";

                            var data = $.tmpl(stringThead, arrayDoctype);
                            $('#staticResult').find('tbody').append(data);
                        }

                        $('#bcsl').find('ul li span').remove();
                        $('#bcsl').find('ul li.cbc').append("<span class='blue'>" + t1 + "</span>")
                        $('#bcsl').find('ul li.ln').append("<span class='blue'>" + t7 + "</span>")
                        $('#bcsl').find('ul li.cxl').append("<span class='blue'>" + t3 + "</span>")
                        $('#bcsl').find('ul li.dd').append("<span class='blue'>" + t5 + "</span>")

                        $('#bctm').find('ul li span').remove();
                        $('#bctm').find('ul li.cbc').append("<span class='blue'>" + t2 + "</span>")
                        $('#bctm').find('ul li.ln').append("<span class='blue'>" + t8 + "</span>")
                        $('#bctm').find('ul li.cxl').append("<span class='blue'>" + t4 + "</span>")
                        $('#bctm').find('ul li.dd').append("<span class='blue'>" + t6 + "</span>")


                        Highcharts.chart('container-chart', {
                            colors: ['#4379DE', '#FF5F75'],
                            credits: {
                                enabled: false
                            },
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: ''
                            },
                            subtitle: {
                                text: ''
                            },
                            xAxis: {
                                categories: [
                                    'Báo cáo số liệu',
                                ],
                                crosshair: true,
                                labels: {
                                    style: {
                                        fontSize: '14px',
                                        fontFamily: 'Roboto-Regular'
                                    }
                                }
                            },
                            legend: {
                                enabled: false
                            },
                            yAxis: {
                                min: 0,
                                title: {
                                    text: ''
                                },
                                labels: {
                                    style: {
                                        fontSize: '12px',
                                        fontFamily: 'Roboto-Regular'
                                    }
                                }
                            },
                            tooltip: {
                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                    '<td style="padding:0"><b>{point.y}</b></td></tr>',
                                footerFormat: '</table>',
                                shared: true,
                                useHTML: true
                            },
                            plotOptions: {
                                column: {
                                    pointPadding: -0.5,
                                    borderWidth: 0,
                                    groupPadding: 0.2,
                                    pointWidth: 14,
                                    padding: 0
                                }
                            },
                            series: [{
                                name: 'Báo cáo số liệu chưa xử lý',
                                data: [t1]
                            }, {
                                name: 'Báo cáo số liệu lưu nháp',
                                data: [t7]
                            }, {
                                name: 'Báo cáo số liệu chờ xử lý',
                                data: [t3]
                            }, {
                                name: 'Báo cáo số liệu đã xử lý',
                                data: [t5]
                            }
                            ]
                        });

                        Highcharts.chart('container-chart-pie', {
                            colors: ['#4379DE', '#FF5F75'],
                            credits: {
                                enabled: false
                            },
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: ''
                            },
                            subtitle: {
                                text: ''
                            },
                            xAxis: {
                                categories: [
                                    'Báo cáo thuyết minh',
                                ],
                                crosshair: true,
                                labels: {
                                    style: {
                                        fontSize: '14px',
                                        fontFamily: 'Roboto-Regular'
                                    }
                                }
                            },
                            legend: {
                                enabled: false
                            },
                            yAxis: {
                                min: 0,
                                title: {
                                    text: ''
                                },
                                labels: {
                                    style: {
                                        fontSize: '12px',
                                        fontFamily: 'Roboto-Regular'
                                    }
                                }
                            },
                            tooltip: {
                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                    '<td style="padding:0"><b>{point.y}</b></td></tr>',
                                footerFormat: '</table>',
                                shared: true,
                                useHTML: true
                            },
                            plotOptions: {
                                column: {
                                    pointPadding: -0.5,
                                    borderWidth: 0,
                                    groupPadding: 0.2,
                                    pointWidth: 14,
                                    padding: 0
                                }
                            },
                            series: [{
                                name: 'Báo cáo thuyết minh chưa xử lý',
                                data: [t2]
                            }, {
                                name: 'Báo cáo thuyết minh lưu nháp',
                                data: [t8]
                            }, {
                                name: 'Báo cáo thuyết minh chờ xử lý',
                                data: [t4]
                            }, {
                                name: 'Báo cáo thuyết minh đã xử lý',
                                data: [t6]
                            }
                            ]
                        }); 
                    },
                    error: function (xhr) { },
                    complete: function () {

                    }
                });
            } else {
                var result = resultTotal;
                var BcSLChuaXuLy = 0, BCTMChuaXuLy = 0, BCSLDangXuLy = 0, BCTMDangXuly = 0, BCSLDaXuLy = 0, BCTMDaXuLy = 0, BCSLLuuNhap = 0, BCTMLuuNhap = 0;
                for (var i = 0 ; i < result.length; i++) {
                    var parseJsonResult = result[i].reportDoctype;
                    for (var j = 0 ; j < parseJsonResult.length ; j++) {
                        if (parseJsonResult[j].Status == 0 && parseJsonResult[j].CategoryBussiness == 4) {
                            BcSLChuaXuLy++;
                        }
                        if (parseJsonResult[j].Status == 0 && parseJsonResult[j].CategoryBussiness == 8) {
                            BCTMChuaXuLy++;
                        }
                        if (parseJsonResult[j].Status == 2 && parseJsonResult[j].CategoryBussiness == 4) {
                            BCSLDangXuLy++;
                        }
                        if (parseJsonResult[j].Status == 2 && parseJsonResult[j].CategoryBussiness == 8) {
                            BCTMDangXuly++;
                        }
                        if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                            && parseJsonResult[j].CategoryBussiness == 4) {
                            BCSLDaXuLy++;
                        }
                        if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                            && parseJsonResult[j].CategoryBussiness == 8) {
                            BCTMDaXuLy++;
                        }
                        if (parseJsonResult[j].Status == 1 && parseJsonResult[j].CategoryBussiness == 4) {
                            BCSLLuuNhap++;
                        }
                        if (parseJsonResult[j].Status == 1 && parseJsonResult[j].CategoryBussiness == 8) {
                            BCTMLuuNhap++;
                        }
                    }
                }

                Highcharts.chart('container-chart', {
                    colors: ['#4379DE', '#FF5F75'],
                    credits: {
                        enabled: false
                    },
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        categories: [
                            'Báo cáo số liệu',
                        ],
                        crosshair: true,
                        labels: {
                            style: {
                                fontSize: '14px',
                                fontFamily: 'Roboto-Regular'
                            }
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: ''
                        },
                        labels: {
                            style: {
                                fontSize: '12px',
                                fontFamily: 'Roboto-Regular'
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y}</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: -0.5,
                            borderWidth: 0,
                            groupPadding: 0.2,
                            pointWidth: 14,
                            padding: 0
                        }
                    },
                    series: [{
                        name: 'Báo cáo số liệu chưa xử lý',
                        data: [BcSLChuaXuLy]
                    }, {
                        name: 'Báo cáo số liệu lưu nháp',
                        data: [BCSLLuuNhap]
                    }, {
                        name: 'Báo cáo số liệu chờ xử lý',
                        data: [BCSLDangXuLy]
                    }, {
                        name: 'Báo cáo số liệu đã xử lý',
                        data: [BCSLDaXuLy]
                    }
                    ]
                });

                Highcharts.chart('container-chart-pie', {
                    colors: ['#4379DE', '#FF5F75'],
                    credits: {
                        enabled: false
                    },
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        categories: [
                            'Báo cáo thuyết minh',
                        ],
                        crosshair: true,
                        labels: {
                            style: {
                                fontSize: '14px',
                                fontFamily: 'Roboto-Regular'
                            }
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: ''
                        },
                        labels: {
                            style: {
                                fontSize: '12px',
                                fontFamily: 'Roboto-Regular'
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y}</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: -0.5,
                            borderWidth: 0,
                            groupPadding: 0.2,
                            pointWidth: 14,
                            padding: 0
                        }
                    },
                    series: [{
                        name: 'Báo cáo thuyết minh chưa xử lý',
                        data: [BCTMChuaXuLy]
                    }, {
                        name: 'Báo cáo thuyết minh lưu nháp',
                        data: [BCTMLuuNhap]
                    }, {
                        name: 'Báo cáo thuyết minh chờ xử lý',
                        data: [BCTMDangXuly]
                    }, {
                        name: 'Báo cáo thuyết minh đã xử lý',
                        data: [BCTMDaXuLy]
                    }
                    ]
                });

                $('#bcsl').find('ul li span').remove();
                $('#bcsl').find('ul li.cbc').append("<span class='blue'>" + BcSLChuaXuLy + "</span>")
                $('#bcsl').find('ul li.ln').append("<span class='blue'>" + BCSLLuuNhap + "</span>")
                $('#bcsl').find('ul li.cxl').append("<span class='blue'>" + BCSLDangXuLy + "</span>")
                $('#bcsl').find('ul li.dd').append("<span class='blue'>" + BCSLDaXuLy + "</span>")

                $('#bctm').find('ul li span').remove();
                $('#bctm').find('ul li.cbc').append("<span class='blue'>" + BCTMChuaXuLy + "</span>")
                $('#bctm').find('ul li.ln').append("<span class='blue'>" + BCTMLuuNhap + "</span>")
                $('#bctm').find('ul li.cxl').append("<span class='blue'>" + BCTMDangXuly + "</span>")
                $('#bctm').find('ul li.dd').append("<span class='blue'>" + BCTMDaXuLy + "</span>")
            }

        });
    });
}