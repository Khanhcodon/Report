$(document).ready(function () {
    $('.btnAddsValue').removeAttr('id');
    $('.btnAddsValue').attr('id', 'btnAddIncatalogValue');


    $("#btnAddIncatalogValue").on("click", function (e) {
        if ($('.btnAddsValue').prop('id') == 'btnAddIncatalogValue') {
            e.preventDefault();
            var form = $('#myFormIncatalogValue');
            $("input[name='InCatalogValueId']").val("00000000-0000-0000-0000-000000000000");
            $("input[name='InCatalogId']").val("00000000-0000-0000-0000-000000000000");
            var test = $.trim(JSON.stringify($("#InCatalogIdReplace").val()));
            $('#InCatalogIds').val(test);
            form.submit();  
        } else { // sửa
            e.preventDefault();
            var form = $('#myFormIncatalogValue');
            $("input[name='InCatalogId']").val("00000000-0000-0000-0000-000000000000");
            var test = $.trim(JSON.stringify($("#InCatalogIdReplace").val()));
            $('#InCatalogIds').val(test);
            form.submit();
            $("input[name='InCatalogValueId']").val("00000000-0000-0000-0000-000000000000");
        }
        
    });
});
