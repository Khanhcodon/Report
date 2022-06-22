function clearmodal() {
    $("#InfringeModal").find("input").eq(1).val("");
    $("#detailinfringe").val("");
    $("#datetimepicker").datepicker().datepicker("setDate", new Date());
}

var rateemployeechooser
var CriteriaView = Backbone.View.extend({
	tagName: 'tr',
	// el: "#tbody_censor_calendar",
	events: {
	    'click .userinfringe': 'createuserinfringe',
		'click .detail': 'showdialog',
	},

	initialize: function () {
		this.listenTo(this.model, 'destroy', this.remove);
		this.listenTo(this.model, 'add', this.render);
		
	},

	render: function () {
	  
		// this.$el.html(this.template(this.model.toJSON()));
	    this.$el.html($('#CheckInfringe').tmpl(this.model.toJSON()));

		return this;
	},

	createuserinfringe: function () {
	    clearmodal();
	    var count = 0;
	    rateemployeechooser = this.model.get("RateEmployeeId");
	    var datalist = '{"data":[';
	    for (var i = 0; i < criterias.data.length; i++) {
	        if( criterias.data[i].ParentId == this.model.get("RateEmployeeId")){
	            datalist = datalist + JSON.stringify(criterias.data[i]) + ",";
	            count++;
	        }
	    }
	    datalist = datalist.substring(0, datalist.length - 1);
	    if (count==0) {
	        datalist = datalist + '[]}'
	    } else {
	        datalist = datalist + ']}'
	    }
        
        var listobj=JSON.parse(datalist)
       
        $("#namecriteria").html($("#NameCriteriaTemplate").tmpl(listobj))
        $("#InfringeModal").modal("show");
        $(".modal-backdrop.fade.in").removeClass("modal-backdrop")
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
});

