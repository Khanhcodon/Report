// Tra ve danh sach lich da duoc duyet trong tuan cua ca co quan


// Tra ve danh sach lich chua duoc duyet theo ngay cua user hien tai
function ViewCalendarUncensorOfDayByUser(date, callback) {
    return $.ajax({
        type: 'Get',
        url: 'GetUnCensorByUserId',
        dataType: 'json',
        data: { date: date.toJSON() }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}


//trả về danh danh chưa được duyệt trong ngày của tất cả cơ quan
function ViewCalendarUncensorofDay(date, callback) {
    return $.ajax({
        type: 'Get',
        url: 'ViewCalendarofDay',
        dataType: 'json',
        data: { date: date.toJSON() }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

//trả về danh danh không được duyệt
function ViewCalendarCancels(callback) {
    return $.ajax({
        type: 'Get',
        url: 'ViewCalendarCancels',
        dataType: 'json',
        data: { }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Trả về tất cả danh sách lịch đã duyệt trong 1 ngày
function ViewCalendarcensorofDay(date, callback) {
    return $.ajax({
        type: 'Get',
        url: 'ViewCalendarofDayCensored',
        dataType: 'json',
        data: { date: date.toJSON() }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

// Xóa lịch theo ID
function DeleteCalendarById(Id, callback) {
    return $.ajax({
        type: 'Get',
        url: 'DeleteCalendarDay',
        dataType: 'json',
        data: { calendarId: Id }
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}

//Hàm update trang thai duyệt hay chưa duyệt lịch
// status: trạng thái lịch
// 0: Lịch không được duyệt
// 1: Lịch chưa được duyệt
// 2: Lịch đã được duyệt
function UpdateisAcceptedById(Id, status, cause, callback) {
    return $.ajax({
        type: 'Get',
        url: 'UpdateisAcceptedById',
        dataType: 'json',
        data: { calendarId: Id, status: status, cause: cause}
    })
.done(callback)
.fail(function (jqXHR, textStatus, errorThrown) {
    // Handle error
});
}