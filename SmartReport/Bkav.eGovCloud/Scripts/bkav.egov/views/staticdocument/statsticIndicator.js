(function(){
    var StatisticIndicator = Backbone.View.extend({
        el: "p",
        initialize: function (options) {
            this.renderDepart();
            this.changePeriod();
        },
        year: 2020,
        render: function (arrDepart) {
            var that = this;
            $.ajax({
                url: "/dashboard/GetDataIndicator",
                type: 'Get',
                data: { year: that.year, listDepartId: that.arrDepart },
                error: function (a, b, c) {
                },
                success: function (response) {
                    var data = response.data;
                    var htmlDepart = $("#DepartTemplate").tmpl(data);
                    $("#tableDataIndicator").html(htmlDepart);
                }
            });
        },
        renderDepart: function () {
            var that = this;
            $.ajax({
                url: "/dashboard/GetDepartmentIndicator",
                type: 'Get',
                data: {  },
                error: function (a, b, c) {
                },
                success: function (response) {
                    var data = response.data;
                    var arr = [];
                    _.each(data, function (item) {
                        arr.push(item.DepartmentId)
                    });
                    arrDepart = JSON.stringify(arr);
                    that.arrDepart = arrDepart;
                    that.render(arrDepart)
                }
            });
        },
        changePeriod: function () {
            var that = this;
            $("#btnViewDetail").on('click', function () {
                var year = $("#TimeYear").val();
                that.year = year;
                that.render();
            });
        }
    });
    var st = new StatisticIndicator
})()