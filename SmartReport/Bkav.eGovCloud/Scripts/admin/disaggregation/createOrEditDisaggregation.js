$(document).ready(function () {
    $('#showSuccess').hide();

    var table = $('#disaggregation').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
        'header': true,
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

    

    $('#btnAdd').click(function (e) {
        e.preventDefault();    
        var form = $("#myForm");
        /*
        var serializeForm = $("#myForm").serializeArray();
        serializeForm.shift();
        var obj = new Object();
        obj.IndicatorDesctiption = serializeForm[1].value;
        obj.IndicatorId = serializeForm[3].value;
        obj.IndicatorName = serializeForm[0].value;
        obj.Indicators = null;
        obj.IsActivated = serializeForm[2].value;

        var data_send = JSON.stringify({ model: obj });
        
        $.ajax({
            type: 'POST',
            url: '/Disaggregation/Index',     
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
            },
            failure: function(response) {  
            },
            error: function (xhr) {
            },
            complete: function () {
                swal("Thành công!", "Xóa tiêu thức thành công", "success");
                setTimeout(function () {
                    window.location.reload();
                }, 1000)
            }
        });  */   
        form.submit();
        $("input[name='IndicatorId']").val("00000000-0000-0000-0000-000000000000");
    });

    $('#deleteMul').click(function (e) {
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
                       var htmlTr = node.html();
                       if (index < info.length && htmlTr.search("checkId") != -1) {
                           var str = htmlTr.search("id");
                           var pusharr = htmlTr.slice(str + 4, str + 40);
                           arr.push(pusharr);
                       }
                   });

                   var data_to_send = JSON.stringify(arr);
                   var data = { model: JSON.parse(data_to_send) };
                   $.ajax({
                       url: '/Disaggregation/DeleteMul',
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
    $('#checkBoxAlls').click(function () {       
        if ($(this).is(":checked")) {
            $(".chkCheckBoxId").prop("checked", true)
            table.rows('.checkId').column().nodes().to$().addClass('checkId');
        }
        else {
            $(".chkCheckBoxId").prop("checked", false)
            table.rows('.checkId').column().nodes().to$().removeClass('checkId');
        }
    });

    

    $('.labelIndicatorID').click(function () {
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
});