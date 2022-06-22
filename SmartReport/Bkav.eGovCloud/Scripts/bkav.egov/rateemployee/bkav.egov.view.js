function clearModal() {
    $("#setparentrate").find("option[value='0']").prop('selected', false);
    $("#namecriteria").find("textarea").val("");
    $("#pointcriteria").find("input").val("");
    $("#descriptioncriteria").find("textarea").val("");
    //$('#departmentmodalname').val(departmentiddefault.name)
    $('#departmentmodalname').prop("disabled", false);
    $('#searchbydepartmentmodal').show();
   // departmentid = departmentiddefault
}

var departmentid = {};// khai báo lấy giá trị id của phòng ban trong hàm ready sẽ gán giá trị id của root
var departmentiddefault = {};
function getDataCriteria(datas) {
    $('#tbody_rateemployee').html('');
    var datalist = datas;
    for (var i = 0; i <datalist.length; i++) {
        if (datalist[i].ParentId === undefined) {
            datalist[i].ParentId = null
        }
    }
    var criterias = { data: datalist };
    for (var i = 0; i < criterias.data.length; i++) {
        if (criterias.data[i].ParentId == null) {
            var criteria = new RateEmployee(criterias.data[i]);
            listcriterias.add(criteria);
            for (var j = 0; j < criterias.data.length; j++) {
                if (criterias.data[j].ParentId == criterias.data[i].RateEmployeeId) {

                    var criteriatmp = new RateEmployee(criterias.data[j]);

                    listcriterias.add(criteriatmp);
                }
            }
        }
    }
    var htmlparent = $('#InfoParentRateTemplate').tmpl(criterias);
    $("#setparentrate").html(htmlparent);
}
var CriteriaView = Backbone.View.extend({
	tagName: 'tr',
	// el: "#tbody_censor_calendar",
	events: {
		'click .addcriteria': 'createRow',
		'click .editcriteria': 'editRow',
		'click .deletecriteria': 'removeRow',
		'click .detail': 'showdialog',
		'click .togglerow': 'togglechildren'
	},

	initialize: function () {
		this.listenTo(this.model, 'destroy', this.remove);
		this.listenTo(this.model, 'add', this.render);
		this.listenTo(this.model, 'change', this.render);
		
	},

	render: function () {
		// this.$el.html(this.template(this.model.toJSON()));
		this.$el.html($('#RateTemplate').tmpl(this.model.toJSON()));
		return this;
	},

	createRow: function () {
	    $(".modal-title > b").text("Thêm mới tiêu chí")
	    clearModal();
	    $("#namecriteria").find("input[name=RateEmployeeId]").val("")
	    $('.selectparent').prop("disabled", false);
	    $("#choosedepartment").find("#selectdepartmentrate").prop('disabled', '');
		$("#setparentrate").find("option[value=" + this.model.get("RateEmployeeId") + "]").prop('selected', true);
		$("#RateEmployeeModal").modal("show");
		$(".modal-backdrop.fade.in").removeClass("modal-backdrop")
		$('#choosedepartmentloadmodal').jstree("deselect_all");
		$('#choosedepartmentloadmodal').jstree("select_node", this.model.get("DepartmentId"));
	},
	
	editRow: function () {
	    $(".modal-title > b").text("Sửa tiêu chí")
	    clearModal();
	    $('.selectparent').prop("disabled", true);
        // không cho phép sửa phòng ban
	    $('#departmentmodalname').val(this.model.get('DepartmentPath'))
	    $('#departmentmodalname').prop("disabled", true);
	    $('#searchbydepartmentmodal').parent().parent().removeClass('open')
	    $('#searchbydepartmentmodal').hide();
	   
	    $("#namecriteria").find("input[name=RateEmployeeId]").val(this.model.get("RateEmployeeId"));
	    if (this.model.get("ParentId")!=null) {
	        $("#setparentrate").find("option[value=" + this.model.get("ParentId") + "]").prop('selected', true);
	    } else {
	        $("#setparentrate").find("option[value=" + 0 + "]").prop('selected', true);
	    }
	    departmentid = { 'id': this.model.get('DepartmentId'), 'name': this.model.get('DepartmentName') };// gán giá trị mặc định cho root
		$("#namecriteria").find("textarea").val(this.model.get("Name"));
		$("#pointcriteria").find("input").val(this.model.get("Point"));
		$("#descriptioncriteria").find("textarea").val(this.model.get("Description"));
		$("#RateEmployeeModal").modal("show");
		$(".modal-backdrop.fade.in").removeClass("modal-backdrop")
	},

	removeRow: function () {
	    var modelslt = this.model;
	    bootbox.confirm("Bạn có muốn xoá tiêu chí này?", function (rsl) {
	        if (rsl == true) {
	            if (modelslt.get("ParentId") == null) {
	                var remaining = listcriterias.remaining(modelslt.get("RateEmployeeId")).length;
	                if (remaining > 0) {
	                    bootbox.alert("bạn cần xóa các thanh phần con trước", function () {

	                    })
	                }
	                else {
	                    DeleteCriterias(Number(modelslt.get("RateEmployeeId")), function (data) {
	                        if (data != "Delete") {
	                            bootbox.alert(data, function () { })
	                        } else {
	                            modelslt.destroy();
	                        }
	                    })
	                }
	            } else {
	                DeleteCriterias(Number(modelslt.get("RateEmployeeId")), function (data) {
	                    if (data != "Delete") {
	                        bootbox.alert(data, function () { })
	                    } else {
	                        modelslt.destroy();
	                    }
	                })
	            }
	        }
	    })
		
	},
	

	showdialog: function () {
      
        $("#dialog > p").text(this.model.get("Description"))
        $("#dialog").dialog({
            open: function () {
                $(this).closest(".ui-dialog")
                .find(".ui-dialog-titlebar-close")
                .html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");
            },
            autoOpen: false,
            show: {
            },
            hide: {
            }
        });
        $("#dialog").dialog("open");
	},
	togglechildren: function () {
	    var rateid = this.model.get("RateEmployeeId")
	    var btntoggle = this.$(".togglerow");
	    if (btntoggle.hasClass("icon-arrow-down7")) {
	        btntoggle.removeClass("icon-arrow-down7");
	        btntoggle.addClass(" icon-arrow-right7")
	    } else {
	        btntoggle.addClass("icon-arrow-down7");
	        btntoggle.removeClass(" icon-arrow-right7")
	    }
	    $("#tbody_rateemployee>tr").each(function () {
	        var slrrow = $(this);
	        if ($(this).find("input[name='ParentId']").val() == rateid) {
	            slrrow.toggle();
	        }
	    })
	},
});