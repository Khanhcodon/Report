// Thêm mới tiêu chí vào danh sách các tiêu chí
function CreateCriteria(data, callback) {
    return $.ajax({
        type: 'Post',
        url: 'CreateorUpdateCriteria',
        dataType: 'json',
        data: { criteria: data }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Lấy dữ liệu về các tiêu chí

function GetCriterias(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsCriteria',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Lấy dữ liệu về các tiêu chí theo phòng ban được chọn

function GetCriteriasbyDepartment(department, callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsCriteriabyDepartment',
        dataType: 'json',
        data: { departmentid: department }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Lấy dữ liệu về các phòng ban

function GetDepartments(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsDepartment',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Xóa tiêu chí

function DeleteCriterias(idcrit, callback) {
    return $.ajax({
        type: 'Post',
        url: 'DeleteCriteria',
        dataType: 'json',
        data: { CriteriaId: idcrit }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lấy về tất cả các danh sách người vi phạm

function GetInfringe(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsCheckInfringeView',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lấy về tất cả các danh sách người vi phạm theo 1 khoảng thời gian

function GetInfringeByDate(dateBegin, dateend, callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsCheckInfringebyDateRange',
        dataType: 'json',
        data: { dateBegin: dateBegin, dateend: dateend }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lấy về tất cả các danh sách người vi phạm theo 1 khoảng thời gian theo user

function GetInfringeByDateuserid(dateBegin, dateend, callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsCheckInfringeViewByCurrentUser',
        dataType: 'json',
        data: { dateBegin: dateBegin, dateend: dateend }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lấy về tất cả các danh sách người vi phạm theo ngày hiện tại

function GetInfringeByDateNow(datenow, callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsCheckInfringebyDate',
        dataType: 'json',
        data: { date: datenow }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}


// lấy về các danh sách người trong công ty

function GetAllUser(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetsAllUser',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lấy về các danh sách phòng ban của người trong công ty

function GetDepartmentByuser(userid, callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetUserDepartmentsById',
        dataType: 'json',
        data: { userid: userid }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Thêm mới người vi phạm trong công ty

function AddInfringeUsers(infringemodel, callback) {
    return $.ajax({
        type: 'Post',
        url: 'AddInfringeUser',
        dataType: 'json',
        data: { infringe: infringemodel }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Xóa người vi phạm trong công ty

function DeleteInfringeUsers(infringemodel, callback) {
    return $.ajax({
        type: 'Post',
        url: 'DeleteUserInfringebyId',
        dataType: 'json',
        data: { infringeId: infringemodel }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// lấy phòng ban theo user trong công ty

function getDepartmentUsers(useridpart, callback) {
    return $.ajax({
        type: 'Post',
        url: 'DeleteUserInfringebyId',
        dataType: 'json',
        data: { userid: useridpart }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// chuyển công văn sang dạng văn bản docx

function Convertdocx(ct, callback) {
    return $.ajax({
        type: 'Post',
        url: 'convertDocxdocumentary',
        dataType: 'json',
        data: { content: ct }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}


// Lấy tên hướng chuyển công văn

function GetActionList(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetActionList',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}


// Lấy người cần chuyển công văn

function GetUsersByAction(actionid, callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetUsersByAction',
        dataType: 'json',
        data: { actionId: actionid }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

function SendDispatches(acc, filename, tencv, ykien, html, nextnode, callback) {
    return $.ajax({
        type: 'Post',
        url: 'SendDispatches',
        dataType: 'json',
        data: { account: acc, fileName: filename, compendium: tencv, ideally: ykien, contentHtml: html, nextNodeId: nextnode }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Lấy tất cả loại công văn
function GetDoctype(callback) {
    return $.ajax({
        type: 'Post',
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

// Lây nhân viên cấp dưới
function GetsUserDown(callback) {
    return $.ajax({
        type: 'Post',
        url: 'GetUserDown',
        dataType: 'json',
        data: {}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}