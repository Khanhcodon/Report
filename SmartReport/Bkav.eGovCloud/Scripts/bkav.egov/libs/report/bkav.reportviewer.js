
(function ($) {

    var eReport = function () {
        return new eReport.prototype.init();
    }

    eReport.prototype = {
        init: function () {
            // Lưu cache
            this.cache = [];
            pagingClick(this.data);

            return this;
        }
    };

    var pagingClick = function (datas) {
        $("tr.pager a").bind("click", function () {
            $(this).siblings().removeClass("selected");
            $(this).addClass("selected");
            gotoPage();
            return false;
        });

        $("select.page-size").bind("change", function () {
            $(".selected").removeClass("selected");
            gotoPage();
        });
    };

    var gotoPage = function () {
        var url = "/EReport/GotoPage";
        var data = {
            page: $("tr.pager a.selected").length == 0 ? 1 : parseInt($("tr.pager a.selected").attr("page")),
            pageSize: $("#pageSize").length == 0 ? 30 : parseInt($("#pageSize").val()),
            groupBy: $("#groupBy").val()
        };
        $.ajax({
            url: url,
            type: "Post",
            data: data,
            success: function (result) {
                if (result.success) {
                    var result = $(result.success);
                    showResult(result);
                }
                else {
                    alert(result.error);
                }
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        });
    };

    var showResult = function (result) {
        var target = $(".pager");
        target.siblings(".detail, .group-header, .group-footer").remove();
        target.after($(result));
        target.remove();
    }

    window.eReport = new eReport();
})
(window.jQuery)