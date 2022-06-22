var censorCals = new ListCalendar();
var cancelCalendars = new ListCalendar();

var autoincriment = 30;

var eventObject = {};
_.extend(eventObject, Backbone.Events);


(function (codes, eventObject, code, evt) {
    document.addEventListener && // Modern browsers only
    document.addEventListener("keydown", function (e) {
        code = codes[e.keyCode];
        if (e.ctrlKey || e.metaKey && code) {
            if (code) {
                e.preventDefault();
                eventObject.trigger(code);
                //console.log("sends");
            }
        }
    }, false);

}({ 83: "Ctrl_s", 13: "Ctrl_enter" }, eventObject));

function autostt() {
    $("#tbody_censor_calendar>tr").each(function (index) {
        $(this).children().eq(0).text(Number(index + 1))
    })
}

function setModel(that) {
    var selector = that.$el;
    var timeBegin = that.$el.find("input").eq(0).attr("data-time");
    var timeEnd = that.$el.find("input").eq(1).attr("data-time");
    var title = that.$el.find("textarea").eq(0).val();
    var departmentPrimary = that.$el.find("textarea").eq(1).val();
    var userJoin = that.$el.find("textarea").eq(2).val();
    var location = that.$el.find("textarea").eq(3).val();
    var note = that.$el.find("textarea").eq(4).val();
    var issms = that.$el.find(".issms").find(":checked").length == 0 ? false : true;
    that.model.set({
        BeginTime: timeBegin,
        EndTime: timeEnd,
        Title: title,
        DepartmentPrimary: departmentPrimary,
        UserJoin: userJoin,
        Location: location,
        Note: note,
        IsSms: issms
    })
}

function saveDatainServer(listcensor) {
    if ($('#date_calendar').val() == "") {

        $('#date_calendar').focus();

    } else {
        bootbox.confirm("Bạn có muốn lưu lại lịch?",function (res) {
            if (res == true) {
                $.ajax({
                    url: "CreateCalendarUncensor",
                    data: { 'jsonResult': JSON.stringify(listcensor) },
                    type: "Post",
                    success: function (result) {
                        if (result == 0) {
                            bootbox.alert("Bạn chưa nhập giá trị nào")
                        }
                        else {
                            bootbox.alert("Đăng ký thành công " + result + " lịch mới")
                        }
                        window.setTimeout(function () { location.reload() }, 2000)
                    },
                    error: function (xhr) {
                        alert(xhr.statusText);
                    }
                });
            }
        })
    }
}
var sttoffocus
var rowfocus
// View cho bảng tạo lịch
var CalendarEditableView = Backbone.View.extend({

    //el: "#table_calendar tbody>tr",
    //el:"tbody_censor_calendar",
    // template: _.template($('#calendarTemplateLoad').html()),
    tagName: 'tr',
    // el: "#tbody_censor_calendar",
    events: {
        'click .remove_row_censor': 'removeRow',
        'focus textarea,input': 'changeLine',
        'focus .list-user': 'tagAutocomplete',
        'click .chooseUser': 'chooseUser',
        'change .issms': 'configSms'
    },

    initialize: function () {
        //console.log(this.$el)
        this.listenTo(this.model, 'change', this.render);
        this.listenTo(this.model, 'destroy', this.remove);
    },

    render: function () {
        // this.$el.html(this.template(this.model.toJSON()));
        // console.log(this.model.toJSON())
        this.$el.html($('#calendarTemplateLoad').tmpl(this.model.toJSON(), {
            settime: function (timer) {
                if (timer) {
                    return formatTimeTemplate(timer);
                } else {
                    return '';
                }
            },
            identity: function () {
                count++;
                return count;
            }
        }));

        $('.timestart').datetimepicker({
            onChangeDateTime: function (dp, $input) {
                $input.attr("data-time", formatTimeServer(dp));
            },
            //datepicker: false,
            format: 'H:i d/m/Y',
            //step: 30
        });
        $('.timeend').datetimepicker({
            onChangeDateTime: function (dp, $input) {
                $input.attr("data-time", formatTimeServer(dp));
            },
            //datepicker: false,
            format: 'H:i d/m/Y',
        });

       

        return this;
    },

    configSms: function (e) {
        var that = this;
        setModel(that);
    },

    chooseUser: function () {
        var that = this;
        setModel(that);
        var userChecked = that.$el.find(".list-user").val();
        treeUser.initialize(userChecked);
        $("#userDeptModal").modal("show");
        $(".modal-backdrop.fade.in").removeClass("modal-backdrop");
        $("#btnChoose").one("click", function () {
            var users = treeUser.getAccountChecked();
            that.model.set({ "DepartmentPrimary": users });

            $("#userDeptModal").modal("hide");
        })
    },

    tagAutocomplete: function (e) {
        var allUsers = [], allUserFull = egov.locache.get("allUsers");
        for (var i = 0; i < allUserFull.length; i++) {
            allUsers.push(allUserFull[i].username)
        }
        sourceUsers = []
        if (allUsers !== undefined) {
            sourceUsers = allUsers
        }
        var target = $(e.target);
        $(".list-user").tagEditor("destroy");

        target.tagEditor({
            autocomplete: {
                delay: 0, // show suggestions immediately
                position: { collision: 'flip' }, // automatic menu position up/down
                source: sourceUsers
            },
            forceLowercase: false,
            placeholder: 'người tham gia'
        });

        $(document).one("click", function (e) {
            //console.log(e.target);
            if (!$(e.target).hasClass("ui-menu-item")) {
                $(".list-user").tagEditor("destroy");
            }
        });
        //$("input").one("keydown", function (e) {
        //    var keyCode = e.keyCode || e.which;

        //    if (keyCode == 9) {
        //        $(".list-user").tagEditor("destroy");
        //    }
        //})
        //debugger
        //target.next().children().last().find("input").one("blur", function () {
        //    target.tagEditor("destroy")
        //})
    },

    changeLine: function (e) {

        var target = $(e.target);
        var row = target.parents('tr');
        row.height("100px");

        //row.children().eq(0).css("background-color", "red")
        if (sttoffocus === undefined) {
            if (row.length > 0) {
                sttoffocus = Number(row.children().find('.sttcalendar').val())
                rowfocus = row
                //console.log("khơi tạo dong"+sttoffocus)
                autoincriment = Number(sttoffocus)
                // rowfocus = row;
            }
        } else {
            //console.log(sttoffocus + "==" + row.children().find('.sttcalendar').val())
            //nhap gia tri trong 1 dong
            if (sttoffocus == Number(row.children().find('.sttcalendar').val())) {
                //lấy vị trí focus hiện tại
                rowfocus = row;
            }
                //nhap gia tri khi chuyen dong
            else {
                if (rowfocus === undefined) {
                    return;
                }
                rowfocus.height("50px");

                if (validateCalendar(rowfocus) == 3) {
                    var editer = censorCals.findWhere({ stt: Number(rowfocus.children().find('.sttcalendar').val()) });

                    if (editer !== undefined) {
                        censorCals.findWhere({ stt: Number(rowfocus.children().find('.sttcalendar').val()) }).set(calendar.model.setModelConformRow(rowfocus));
                    }
                }

                sttoffocus = row.children().find('.sttcalendar').val();
                rowfocus = row;
                //alert(sttoffocus)
            }
        }
    },

    removeRow: function (e) {
        var models = this.model;
        var idcals = this.model.get("CalendarId")
        if (this.model.get("CalendarId") != -1) {
            bootbox.confirm("Lịch này đã được đăng ký bạn có muốn xóa nó?", function (result) {
                if (result == true) {
                    if (idcals != -1) {
                        DeleteCalendarById(Number(idcals), function (data) {
                            models.destroy();
                            var newModel = calendar.model.emptyModel();
                            newModel.stt = censorCals.length + 1;
                            var calM = new Calendar(calendar.model.emptyModel());

                            censorCals.add(calM);
                            autostt();
                        });
                    }
                }
            });
        }
       
    }
});

var CancelCalendarView = Backbone.View.extend({
    tagName: 'tr',
    // el: "#tbody_censor_calendar",
    events: {
        'click .success_calendar': 'createRow',
        'click .remove_calendar': 'removeRow'
    },

    initialize: function () {
        this.listenTo(this.model, 'destroy', this.remove);
    },

    render: function () {
        // this.$el.html(this.template(this.model.toJSON()));
        this.$el.html($('#cancelCalendarTemplate').tmpl(this.model.toJSON(), {
            settime: function (timer) {
                if (timer) {
                    return formatTimeTemplate(timer);
                } else {
                    return '';
                }
            }
        }));

        return this;
    },
    createRow: function (e) {
        var target = $(e.target);
        var that = this;
        var idsltor = that.model.get("CalendarId")
        var objCalendar = censorCals.where(calendar.model.emptyModel())
        UpdateisAcceptedById(Number(idsltor), 1, "", function () {
            that.model.set({ stt: objCalendar[0].get("stt") })
            objCalendar[0].set(that.model.toJSON())
            that.model.destroy();
        });
    },

    removeRow: function (e) {
        var idcals = this.model.get("CalendarId")
        var that = this;
        if (idcals != -1) {
            bootbox.confirm("Bạn có muốn xóa nó?", function (result) {
                if (result == true) {
                    if (idcals != -1) {
                        DeleteCalendarById(Number(idcals), function (data) {
                            that.model.destroy();
                        });
                    }
                }
            });
        }
    },

    remove: function () {
        this.$el.remove()
    }
});


function validateCalendar(rowFocus) {
    var cells = rowFocus.children(".insert");
    var rowEmpty = true;
    cells.each(function (e) {
        $(this).children().removeClass("error-row")
        var value = $(this).children().val();
        if (value != "") {
            rowEmpty = false;
        }
    });
    if (rowEmpty) {
        return 1;//trường hợp tất cả đều rỗng
    }
    var timeStart = cells.eq(0).children().val();
    var timeEnd = cells.eq(1).children().val();
    var content = cells.eq(2).children().val();

    var errorCheck = false;
    if (timeStart == "") {
        cells.eq(0).children().addClass("error-row");
        errorCheck = true;
    }
    if (timeEnd == "") {
        cells.eq(1).children().addClass("error-row");
        errorCheck = true;
    }
    if (content == "") {
        cells.eq(2).children().addClass("error-row");
        errorCheck = true;
    }
    var valueStart = new Date(cells.eq(0).children().attr("data-time"));
    var valueEnd = new Date(cells.eq(1).children().attr("data-time"))

    if (valueStart.getTime() > valueEnd.getTime()) {
        cells.eq(0).children().addClass("error-row");
        cells.eq(1).children().addClass("error-row");
        errorCheck = true;
    }
    if (errorCheck) {
        return 2;// có lỗi xảy ra về việc nhập dữ liệu
    } else {
        return 3;// Không có lỗi gì có thể lưu vào model
    }
}