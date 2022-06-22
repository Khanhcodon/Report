//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        EmployeeID: $('#EmployeeID').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        State: $('#State').val(),
        Country: $('#Country').val()
    };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function validate() {
    var isValid = true;
    if ($('.doctype-name').val().trim() == "") {
        $('.doctype-name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('.doctype-name').css('border-color', 'lightgrey');
    }
    if ($('.doctype-number').val().trim() == "") {
        $('.doctype-number').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('.doctype-number').css('border-color', 'lightgrey');
    }

    if ($('#ActionLevel').val().trim() == "") {
        $('#ActionLevel').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ActionLevel').css('border-color', 'lightgrey');
    }
    if ($('.place-sent').val() == "") {
        Swal.fire({
            position: 'top-end',
            text: 'Thiếu đơn vị báo cáo',
            icon: 'error',
            width:'400px',
            showConfirmButton: false,
            timer: 1500
        })
        isValid = false;
    }
    else {
        $('.place-sent').css('border-color', 'lightgrey');
    }
    if ($('.place-receiver').val() == "") {
        Swal.fire({
            position: 'top-end',
            text: 'Thiếu đơn vị nhận báo cáo',
            icon: 'error',
            width: '400px',
            showConfirmButton: false,
            timer: 1500
        })
        isValid = false;
    }
    else {
        $('.place-receiver').css('border-color', 'lightgrey');
    }
    return isValid;
}