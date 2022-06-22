
function autostt() {
    $('.stt').each(function (index) {
        $(this).text(index + 1)
    })
}

var department;
var criterias;
var userlist = [];
var userid;
var listcriterias = new ListRateEmployee();
var listcriteriaschildren = new ListRateEmployee();
var AppView = Backbone.View.extend({
    el: $("#bodycriteria"),
    events: {
        'click #savechangeinfringe': 'savechange',
    },
    initialize: function () {
        console.log()
        this.listenTo(listcriterias, 'add', this.addOneCriteria);
        this.listenTo(listcriterias, 'reset', this.resetCriteria);
    },
    addOneCriteria: function (criteria) {
        var view = new CriteriaView({ model: criteria });
        this.$("#tbodyemployeeinfringed").append(view.render().el);
    },
    resetCriteria: function () {
        this.$("#tbodyemployeeinfringed").html('');
    },
    savechange: function () {
        var accountid = userid;
        var rateid = $('#namecriteria :selected').val();
        var datetimeinfringe = new Date($("#datetimepicker").datepicker("getDate").getTime() - ($("#datetimepicker").datepicker("getDate").getTimezoneOffset() * 60000)).toJSON();
        var detail = $("#detailinfringe").val();
        if (accountid !== undefined && rateid && datetimeinfringe != "") {
            console.log(rateid)
            var infr = new InfringeEmployee();
            infr.set({ "Date": datetimeinfringe, "InfringeUserId": Number(accountid), "RateEmployeeId": Number(rateid), "Detail": detail });
            $("#InfringeModal").modal("hide");
            AddInfringeUsers(JSON.stringify(infr), function (data) {
                //alert('them thanfh cong')
                egov.pubsub.publish(egov.events.status.success, "Thêm thành công đánh giá");
            });
        } else {
            $("#dialogerror").fadeIn();
            $("#dialogerror").fadeOut(5000);
        }
    }
});
var App = new AppView;
// Hàm lấy ra các Tiêu chí cha để vẽ giao diện
function getDataCriteriaInfringe(datalist) {
    $("#tbodyemployeeinfringed").html('');
    criterias = { data: datalist };

    for (var i = 0; i < criterias.data.length; i++) {
        if (criterias.data[i].ParentId == null) {
            var criteria = new RateEmployee(criterias.data[i]);
            listcriterias.add(criteria);
        } else {
            var criteria = new RateEmployee(criterias.data[i]);
            listcriteriaschildren.add(criteria);
        }
    }
}
function checkdepartment(criteria, useridpart) {
    var tmp = criteria
    var countdepart = 0;

    if (tmp == useridpart) {
        countdepart++;
    }

    if (countdepart == 0) {
        return false;
    } else {
        return true
    }
}

$(document).ready(function () {
    $("#InfringeModal").draggable({
        handle: ".modal-header"
    });
    // lấy ra danh sách phòng ban
    GetDepartments(function (data) {
        department = data;
        var departmentlist = { datadepartment: data };
        var htmldepartmentload = $('#InfoDepartmentRateTemplate').tmpl(departmentlist);
        $("#choosedepartment").html(htmldepartmentload);

    });

    $('#datetimepicker').datepicker({
        dateFormat: 'dd-mm-yy',
        defaultDate: new Date()
    });
    // Lấy ra các tiêu chí
    GetCriterias(function (data) {
        getDataCriteriaInfringe(data);
        autostt();
    });
    GetAllUser(function (data) {
        for (var i = 0; i < data.length; i++) {
            userlist.push({ UserId: data[i].UserId, UserName: data[i].UserName + ' - ' + data[i].FullName })
        }
    });
    var accountevent = $("#accountname").tautocomplete({
        width: "300px",
        columns: ['UserName'],
        data: function () {
            try {
                var data = userlist;
                // console.log(JSON.stringify( data))
            }
            catch (e) {
                alert(e)
            }
            var filterData = [];

            var searchData = eval("/" + accountevent.searchdata() + "/gi");

            $.each(data, function (i, v) {
                if (v.UserName.search(new RegExp(searchData)) != -1) {
                    filterData.push(v);
                }
            });
            return filterData;
        },
        onchange: function () {

            var user = JSON.stringify(accountevent.all());
            var objuser = JSON.parse(user)
            userid = objuser.UserName;

            GetDepartmentByuser(userid, function (data) {
                if (data.length > 0) {
                    console.log(data);
                    var userdepart = data[0].DepartmentIdExt;

                    // lấy ra tên phòng ban của người vi phạm
                    var userdepartspl = userdepart.split('.')
                    var count = 0;
                    var datalist = '{"data":[';
                    // kiểm tra xem các tiêu chí có trong phong ban của người vi phạm hay không nếu không không hiện ra để đánh dấu vi phạm
                    for (var i = 0; i < criterias.data.length; i++) {
                        for (var j = 0; j < userdepartspl.length; j++) {
                            if (criterias.data[i].ParentId == rateemployeechooser && checkdepartment(criterias.data[i].DepartmentId, userdepartspl[j])) {
                                datalist = datalist + JSON.stringify(criterias.data[i]) + ",";
                                count++;
                            }
                        }
                    }
                    datalist = datalist.substring(0, datalist.length - 1);
                    if (count == 0) {
                        datalist = datalist + '[]}';
                    } else {
                        datalist = datalist + ']}';
                    }
                    var listobj = JSON.parse(datalist)
                    //  console.log(listobj)
                    $("#namecriteria").html($("#NameCriteriaTemplate").tmpl(listobj))
                }
            });

        }
    });
});
$(document).load(function () {
    $('td').tooltip();

});
// hàm lọc theo tên tiêu chí
var accounteventsearch = $(".criterianame").tautocomplete({
    width: "400px",
    columns: ['Tên tiêu chí'],
    data: function () {
        try {
            $('#notall').prop("checked", true);
            var criterialisttmp = []
            var listobj = listcriterias.toJSON();
            for (var i = 0; i < listobj.length; i++) {
                criterialisttmp.push({ id: i, Name: listobj[i].Name })
            }
            var data = criterialisttmp;
        }
        catch (e) {
            alert(e)
        }
        var filterData = [];

        var searchData = eval("/" + accounteventsearch.searchdata() + "/gi");

        $.each(data, function (i, v) {
            if (v.Name.search(new RegExp(searchData)) != -1) {
                filterData.push(v);
            }
        });
        return filterData;
    },
    onchange: function () {
        // datagetview(listbyuserObj, $('#attendees'));

        var listsearch = listcriterias.where({ Name: accounteventsearch.text() });
        if (listsearch.length > 0) {
            listcriterias.reset();
            for (var i = 0; i < listsearch.length; i++) {
                listcriterias.add(listsearch[i].toJSON());
            }
            autostt();
        } else {
            listcriterias.reset();
            GetCriterias(function (data) {
                getDataCriteriaInfringe(data);
                autostt();
            });
        }
    }
});

$("#InfringeModal").on('shown.bs.modal', function () {
    $(this).find(".acontainer").find("input").eq(1).focus();
});
$(".criterianame").parent().find("input").eq(1).keydown(function () {
    var gt = $(this).val();
    console.log(gt)
    if (gt == "") {
        listcriterias.reset();
        GetCriterias(function (data) {
            getDataCriteriaInfringe(data);
            autostt();
        });
    }
});
$(function () {
    $("#bodycriteria").layout({
        resizable: true,
        closable: false,

    });
})
