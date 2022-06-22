define([], function (template) {
    var BuildQuery = Backbone.View.extend({
        tagName: "p",
        initialize: function (options) {
            var that = this;
            that.doc = options.document;
            that.departmentId = options.departmentId;
                that.renderForm();
        },
        renderForm: function () {
            var that = this;
            var query = "";
            var data = that.doc.dataFormTable.getSourceData();
            query = that.renderQuery(data);

            var OrganizationCode = that.doc.$el.find("#InOutPlace").val();
            var timeKey = that.doc.getTimeKeyStandard();
            $.ajax({
                type: "POST",
                url: '/DocumentReport/GetDataByQuery',
                traditional: true,
                data: { 'formId': that.doc.FormId, 'timekey': timeKey, 'organizationkey': OrganizationCode },
                success: function (response) {
                    var result = response;
                    var dataTable = result.data;

                    // xử lý NaN
                    _.each(dataTable, function (item, index) {
                        _.each(item, function (itemx, indexx) {
                            if (itemx == null) {
                                dataTable[index][indexx] = "";
                            }
                        });
                    });
                    if (dataTable) {
                        that.doc.dataFormTable.getInstance().loadData(dataTable);
                        that.doc.dataFormTable.render();
                    }
                }
            });
           
        },

        renderQuery: function (data) {
            var query = ""
            for (var i = 0; i < data.length; i++) {
                var subQuery = "Select ";
                var items = _.values(data[i]);
                var keys = _.keys(data[i]);
                for (var j = 0; j < items.length; j++) {
                    var dataEl = items[j];
                    if ((typeof dataEl === 'string' || dataEl instanceof String)) {
                        if (dataEl.trim().startsWith("LK(") || dataEl.trim().startsWith("TB(")) {
                        } else {
                            dataEl = "'" + items[j] + "'"
                        }
                    }
                    subQuery += dataEl + " as " + "'" + keys[j] + "'"
                    if (j != items.length - 1) {
                        subQuery += ", ";
                    } 
                }
                query += subQuery;
                if (i != data.length - 1) {
                    query += " union ";
                }
            }
            query = query.replaceAll("LK(", "Get_LuyKe(").replaceAll("TB(", "Get_FactModel(");
            return query;
        },

        checkForm: function () {
            var that = this;
            var data = that.doc.dataFormTable.getData();
            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < data[i].length; j++) {
                    var el = data[i][j];
                    if ((typeof el === 'string' || el instanceof String) && (el.trim().startsWith("TB(") || el.trim().startsWith("LK("))) {
                        return true;
                    }
                }
            }
            return false;
        }
    });

    return BuildQuery;
});