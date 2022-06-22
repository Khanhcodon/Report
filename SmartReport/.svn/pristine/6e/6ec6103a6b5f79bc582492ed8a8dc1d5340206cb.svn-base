// Lấy tất cả loại công văn
function GetDoctype(callback) {
    return $.ajax({
        type: 'Get',
        url: 'GetDoctype',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Cấu hình hướng công văn
function DoctypeConfig(docid, callback) {
    return $.ajax({
        type: 'Post',
        url: 'ConfigDoctype',
        dataType: 'json',
        data: { doctypeid: docid }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lay cau hinh hien tai
function DoctypeCurrent(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetDoctypeCurrent',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Lấy cấu hình người quản trị
function UserConfig(active, userid, callback) {
    return $.ajax({
        type: 'Post',
        url: 'ConfigUser',
        dataType: 'json',
        data: { active: active, userid: userid }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

function ActiveConfig(valueconfig, callback) {
    return $.ajax({
        type: 'Post',
        url: 'ConfigActive',
        dataType: 'json',
        data: { value: valueconfig }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

function TemplateConfig(valueconfig, callback) {
    return $.ajax({
        type: 'Post',
        url: 'ConfigTemplateDocument',
        dataType: 'json',
        data: { value: valueconfig }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}