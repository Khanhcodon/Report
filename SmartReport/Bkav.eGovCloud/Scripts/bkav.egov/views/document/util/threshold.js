define([egov.template.document.thresHold], function (template) {
    var Threshold = Backbone.View.extend({
        initialize: function (options) {
            var that = this;
            that.doc = options.document;
            that.renderForm();
        },
        template: template,
        renderForm: function () {
            var that = this;
            that.$el.html($.tmpl(this.template, {}));
            var dialogSetting = {
                width: 700,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Ngưỡng dự báo của chỉ tiêu",
                buttons: [
                   {
                       text: egov.resources.common.closeButton,
                       click: function () {
                           that.$el.dialog("hide");
                       }
                   }    
                ]
            };

            that.$el.dialog(dialogSetting);
            that.renderForecast();
        },
        renderForecast: function () {
            var that = this;
            var dataAjax = that.renderDataAjax();
            that.$el.find('#thresHoldIframe').html('<iframe src="/SmartReport/Forecast?indicator=' + dataAjax.code + '&locality=' + dataAjax.organization + '" frameborder="0" scrolling="no" width="100%" height="400" id="IndicatorFrame"></iframe>');
            $.ajax({
                url: "/smartreport/ForecastData",
                type: 'Get',
                data: { timetype: "monthkey", indicator: dataAjax.code, locality: dataAjax.organization },
                error: function (a, b, c) {
                },
                success: function (response) {
                    var dataDuKien = [], dataThucTe = [];
                    dataDuKien = _.filter(response, function (item) {
                        return item.TypeData == "UTH"
                    });
                    dataThucTe = _.filter(response, function (item) {
                        return item.TypeData == "TH"
                    });

                    dataDuKien = _.map(dataDuKien, function (item) { return Number(item.Measure) });
                    dataThucTe = _.map(dataThucTe, function (item) { return Number(item.Measure) });
                    var thresHoldMax = _threadHold(dataThucTe, 2, 0.1);
                    var thresHoldMin = _threadHold(dataThucTe, 2, -0.1);
                    thresHoldMax = thresHoldMax.points.length >= 1 ? thresHoldMax.points[0].y : 0;
                    thresHoldMin = thresHoldMin.points.length >= 1 ? thresHoldMin.points[0].y : 0;
                    that.$el.find('#thresHoldMin').text(thresHoldMin);
                    that.$el.find('#thresHoldMax').text(thresHoldMax);
                }
            });
        },

        renderDataAjax: function () {
            var that = this;
            var selecteds = that.doc.dataFormTable.getSelectedLast();
            if (!selecteds || selecteds.length < 4) {
                return;
            }
            var rowIndex = selecteds[0];
            var dataSource = that.doc.dataFormTable.getSourceData();
            var config = JSON.parse(that.doc.configHandsontable);
            var data = that.doc.getIndicatorData(config, dataSource, [], rowIndex, 0);
            return data;
        }
    });

    function _convertData(data, x, y) {
        for (i = 0; i < data.length; i++) {
            if (data[i] != null && typeof data[i] === 'number') {//If type of X axis is category
                x.push(i + 1);
                y.push(data[i]);
            }
        }
    }

    function _predict(x, ky, kx) {
        var i = 0, nr = 0, dr = 0, ax = 0, ay = 0, a = 0, b = 0;
        function forecast(ar) {
            var r = 0;
            for (i = 0; i < ar.length; i++) {
                r = r + ar[i];
            }
            return r / ar.length;
        }
        ax = forecast(kx);
        ay = forecast(ky);
        for (i = 0; i < kx.length; i++) {
            nr = nr + ((kx[i] - ax) * (ky[i] - ay));
            dr = dr + ((kx[i] - ax) * (kx[i] - ax))
        }
        b = nr / dr;
        a = ay - b * ax;
        return (a + b * x);
    }

    function _threadHold(data, decimalPlaces, Percent) {
        var results = [], N = data.length;
        var X = [];
        var Y = [];

        if (N > 0) {
            _convertData(data, X, Y);

            var name = data[N - 1].name;

            for (var i = 0; i < 1; i++) {
                var x = N + i;

                var coorY = _predict(x, Y, X) + Percent * _predict(x, Y, X);
                if (decimalPlaces)
                    coorY = parseFloat
                        (coorY.toFixed(decimalPlaces));
                var coordinate = { x: x, y: coorY, name: name };
                results.push(coordinate);
            }

            results.sort(function (a, b) {
                if (a[0] > b[0]) {
                    return 1;
                }
                if (a[0] < b[0]) {
                    return -1;
                }
                return 0;
            });
        }

        return { equation: "", points: results, string: "" };
    }
    return Threshold;
});