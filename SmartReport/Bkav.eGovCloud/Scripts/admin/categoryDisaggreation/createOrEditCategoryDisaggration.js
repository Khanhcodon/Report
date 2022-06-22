$(document).ready(function () {
    $('#showSuccess').hide();
    $('#IndicatorId_Dis').select2();

    var table = $('#CategoryDisaggregation').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
        'language': {
            "decimal": "",
            "emptyTable": "Không có dữ liệu",
            "info": "Hiển thị _START_ đến _END_ trong tổng số _TOTAL_ kết quả",
            "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 kết quả",
            "infoFiltered": "(được lọc từ _MAX_ mục)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Hiển thị _MENU_ bản ghi",
            "loadingRecords": "&nbsp;",
            "processing": "<div class='spinner' id='loadingDiv'></div>",
            "search": "Tìm kiếm:",
            "zeroRecords": "Không tìm thấy dữ liệu",
            "paginate": {
                "first": "Đầu tiên",
                "last": "Cuối cùng",
                "next": "&raquo;",
                "previous": "&laquo;"
            },
            "aria": {
                "sortAscending": ": Hiển thị sắp xếp tăng dần",
                "sortDescending": ": Hiển thị sắp xếp giảm dần"
            }
        },
        'createdRow': function (row, data, index) {
            //$(row).addClass('checkId')
        },
        "columnDefs": [
          { className: "checkId", "targets": [0] },
          { targets: 0, sortable: false }
        ],
        order: [[1, "asc"]]
    });
    var tableLeft = $('#CategoryDisaggregationLeft').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
        'language': {
            "decimal": "",
            "emptyTable": "Không có dữ liệu",
            "info": "Hiển thị _START_ đến _END_ trong tổng số _TOTAL_ kết quả",
            "infoEmpty": "Hiển thị 0 đến 0 trong tổng số 0 kết quả",
            "infoFiltered": "(được lọc từ _MAX_ mục)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Hiển thị _MENU_ bản ghi",
            "loadingRecords": "&nbsp;",
            "processing": "<div class='spinner' id='loadingDiv'></div>",
            "search": "Tìm kiếm:",
            "zeroRecords": "Không tìm thấy dữ liệu",
            "paginate": {
                "first": "Đầu tiên",
                "last": "Cuối cùng",
                "next": "&raquo;",
                "previous": "&laquo;"
            },
            "aria": {
                "sortAscending": ": Hiển thị sắp xếp tăng dần",
                "sortDescending": ": Hiển thị sắp xếp giảm dần"
            }
        }    
    });


    $('#btnAddCategoryDisggregation').click(function (e) {
        e.preventDefault();
        var form = $('#myFormCategoryDisaggegation');
        form.submit();
        $("input[name='CategoryDisaggregationId']").val("00000000-0000-0000-0000-000000000000");
    });

    $('#deleteMulDisaggegation').click(function (e) {

        swal({
            title: "Bạn có chắc?",
            text: "Xóa tiêu thức phân tổ này không?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Đồng ý!",
            cancelButtonText: "Hủy bỏ!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
           function (isConfirm) {
               if (isConfirm) {
                   
                   //xu ly
                   e.preventDefault();
                   var arr = [];
                   var info = table.page.info();

                   table.rows().iterator('row', function (context, index) {
                       var node = $(this.row(index).node());
                       var htmlTr = node[0].outerHTML;
                       var strResult = formatFactory(htmlTr);

                       if (index < info.length && strResult.search("checkId") != -1 & strResult.search("odd") != -1) {
                           var str = strResult.search("id");
                           var pusharr = strResult.slice(str + 4, str + 40);
                           arr.push(pusharr);
                       }else if(index < info.length && strResult.search("checkId") != -1 & strResult.search("even") != -1){
                           var str = strResult.search("id");
                           var pusharr = strResult.slice(str + 4, str + 40);
                           arr.push(pusharr);
                       }
                   });

                   var data_to_send = JSON.stringify(arr);
                   var data = { model: JSON.parse(data_to_send) };
                   $.ajax({
                       url: '/CategoryDisaggregations/DeleteMul',
                       type: 'POST',
                       data: data,
                       traditional: true,
                       success: function (result) {

                       },
                       error: function (xhr) { },
                       complete: function () {
                           swal("Thành công!", "Xóa tiêu thức thành công", "success");
                           setTimeout(function () {
                               window.location.reload();
                           }, 1000)
                       }
                   });

               } else {
                   swal("Hủy bỏ", "Hủy xóa thành công!", "warning");
               }
           });  
    });

    table.rows('.checkId').column().nodes().to$().removeClass('checkId');
    $('#checkBoxAllsCategory').click(function () {
        if ($(this).is(":checked")) {
            $(".chkCheckBoxId").prop("checked", true)
            table.rows('.checkId').column().nodes().to$().addClass('checkId');
        }
        else {
            $(".chkCheckBoxId").prop("checked", false)
            table.rows('.checkId').column().nodes().to$().removeClass('checkId');
        }
    });

    table.on('click', 'tbody tr td:first-child .labelIndicatorID', function () {
        if ($('.labelThead').is(":checked")) {
            if ($(this).closest('td').hasClass("checkId")) {
                $(this).closest('td').removeClass("checkId");
            } else {
                $(this).closest('td').addClass("checkId");
            }
        } else {
            if ($(this).closest('td').hasClass("checkId")) {
                $(this).closest('td').removeClass("checkId");
                $(this).prop("checked", true);
            } else {
                $(this).closest('td').addClass("checkId");
                $(this).prop("checked", true);
            }
        }
    });

    function Common(result){
        var str, obj = {}, arr = [];
        var count = 0;
        for (var i = 0 ; i < result.length; i++) {
            obj.CategoryDisaggregationId = result[i].CategoryDisaggregationId;
            obj.IndicatorId = result[i].IndicatorId;
            obj.CategoryDisaggregationName = result[i].CategoryDisaggregationName;
            obj.CategoryDisaggregationCode = result[i].CategoryDisaggregationCode;
            obj.IsActivated = result[i].IsActivated;
            obj.Stt = count;
            arr.push(obj);
            var data = $.tmpl($("#templaceDisagtion"), arr[i]);
            var strS = data[0].outerHTML;
            var strResult = formatFactory(strS);
            table.rows.add($(strResult)).draw();
            table.rows('.checkId').column().nodes().to$().removeClass('checkId');
            count++;
        }
    }

    //click ul li
    tableLeft.on('click', 'tbody tr', function () {
        var $row = tableLeft.row(this).nodes().to$();      
        var hasClass = $row.hasClass('checkRowLeft');
        var rowId = $(this).find("td:first-child").html();
        $('tr').removeClass('checkRowLeft');
        if (hasClass) {          
            $row.removeClass('checkRowLeft')
            //ajax all
            $.ajax({
                url: '/CategoryDisaggregations/GetAll',
                data: { id: rowId },
                success: function (result) {
                    table.clear().draw();
                    Common(result);
                },
                error: function (xhr) { },
                complete: function () {
                }
            });
        } else {
            $row.addClass('checkRowLeft')
            //ajax only

            $.ajax({
                url: '/CategoryDisaggregations/GetIdBy',
                data: { id: rowId },
                success: function (result) {
                    table.clear().draw();
                    Common(result);    
                },
                error: function (xhr) { },
                complete: function () {
                }
            });
        }

    });

    function formatFactory(html) {
        function parse(html, tab = 0) {
            var tab;
            var html = $.parseHTML(html);
            var formatHtml = new String();   

            function setTabs () {
                var tabs = new String();

                for (i=0; i < tab; i++){
                    tabs += '\t';
                }
                return tabs;    
            };


            $.each( html, function( i, el ) {
                if (el.nodeName == '#text') {
                    if (($(el).text().trim()).length) {
                        formatHtml += setTabs() + $(el).text().trim() + '\n';
                    }    
                } else {
                    var innerHTML = $(el).html().trim();
                    $(el).html(innerHTML.replace('\n', '').replace(/ +(?= )/g, ''));
                

                    if ($(el).children().length) {
                        $(el).html('\n' + parse(innerHTML, (tab + 1)) + setTabs());
                        var outerHTML = $(el).prop('outerHTML').trim();
                        formatHtml += setTabs() + outerHTML + '\n'; 

                    } else {
                        var outerHTML = $(el).prop('outerHTML').trim();
                        formatHtml += setTabs() + outerHTML + '\n';
                    }      
                }
            });

            return formatHtml;
        };   
    
            return parse(html.replace(/(\r\n|\n|\r)/gm," ").replace(/ +(?= )/g,''));
        };
    
});