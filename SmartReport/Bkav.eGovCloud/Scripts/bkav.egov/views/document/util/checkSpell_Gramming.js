$(function () {
    var check = false;
    var editor = CKEDITOR.replace('editor1');

    CKEDITOR.config.allowedContent = true;
    CKEDITOR.config.format_span = { name: 'Span', element: 'span' };
    CKEDITOR.config.format_tags = 'p;h2;h3;pre;span';
    CKEDITOR.config.extraPlugins = 'autocomplate';

   

    var tpl = new CKEDITOR.template('<span class="cke-label" style="{sty}" data-toggle="tooltip" data-placement="top" title="{tl}">{label}</span>');
    editor.setData("<p>BKAV bảo vệ máy tính theo cách chuyên nghiệp.</p>");
    var lastData = "";

    editor.on("mode", function () {
        lastData = editor.editable().$.innerText.split(".");
    });

    editor.on('key', function (evt) {
        if (evt.data.keyCode == 13) {
            logCheckGramming();
        } else {
            setTimeout(function () {
                checkSpell();
            }, 100);
        }
    });

    function logCheckGramming() {
        var text = editor.editable().$.innerText;
        $.ajax({
            url: "/Home/test_grammar",
            data: { Str: text },
            dataType: "json",
            type: 'GET',
            success: function (response) {
                try {
                    var message = JSON.parse(response);
                    console.log(message);
                }
                catch (err) {
                    console.log(err.message);
                }
            },
            error: function () {
                alert("Không thể lấy được dữ liệu");
            }
        });
    }

    function checkSpell() {
        var data = editor.editable().$.innerHTML;//mã HTML
        var dt = editor.editable().$.innerText.split(".");
        var para = "";
        if (dt.length != lastData.length) {
            //var arrPara = [];
            for (var i = 0; i < dt.length; i++) {
                if (lastData[i]) {
                    if (dt[i].length != lastData[i].length && dt[i] != "")
                        para = dt[i]; //arrPara.push(dt[i].substr(3));
                } else {
                    if (dt[i] != "")
                        para = dt[i]; //arrPara.push(dt[i-1].substr(3));
                }
            }
        }
        else {
            for (var i = 0; i < dt.length - 1; i++) {
                var count = dt[i].length;
                if (count != lastData[i].length)
                    para = dt[i];
            }
        }

        if (para != "" && para != " ") {
            check = true;
            var that = this;
            if (check) {
                $.ajax({
                    url: "/Home/test_spell",
                    data: { Str: para },
                    dataType: "json",
                    type: 'GET',
                    success: function (response) {
                        try {
                            var res = JSON.parse(response);
                            var error = "";
                            if (res.error.length) {
                                _.each(res.error, function (element) {
                                    error += element.word + ", ";
                                });
                                var newData = data.slice(0, data.lastIndexOf(para)) + tpl.output({ sty: 'background-color: green;', tl: 'Sai từ: ' + error, label: para }) + data.slice(data.lastIndexOf(para) + para.length, data.length);
                                editor.setData(newData);
                            }
                            that.message = res;
                        }
                        catch (err) {
                            console.log(err.message);
                        }
                    }
                });
                lastData = dt;
            }
        }
    }
})