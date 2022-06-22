(function () {
    var AppView = Backbone.View.extend({
        el: "#content-agv",
        events: {
            "click .jstree-anchor": "renderClickTree",
            "change #TimeKey": "changeTimeKey",
            "click #btnViewDetail": "renderClickDetail",
            "keyup #deliverable_search": "renderSearchStatitic",
            "click #ClickViewDetail" : "renderClickDetailView"
        },
        initialize: function (options) {
            var that = this;
            that.render();
        },
        render: function () {
            var that = this;
            var ShowDetail = [];
            that.renderIndicatorValues();
            that.renderSelect2();
            that.dataTable_();
        },
        dataTable_: function(){
            
        },
        renderSelect2: function () {
            $('#StatusStatic').select2();
            $('#TimeKey').select2();
            $('#TimeYear').select2();
            $('#ReportModeId').select2();
        },

        changeTimeKey: function(){
            var time = $('#TimeKey').val();
            $('.inputYear').addClass('addClassDisplay');
            var string;
            if (time == 1) {
                $('.inputMuls').removeClass('addClassDisplay').addClass('inputMul');
            } else if (time == -1) {
                $('.inputYear').addClass('inputMul');
            } else {
                $('.inputMuls').removeClass('inputMul').addClass('addClassDisplay');
            }

            $(".inputMuls").find('.form-group').remove();
            $(".inputMuls").append('<div class="form-group"><div class="input-group"></div></div>');
            if (time == 8) {      
                string = "<select class='form-control select2 w-p100' id='TimeKey9Month' name='TimeKey9Month'>"
                        + "<option value='9' selected='selected'>Chín tháng</option></select>";
                $('.inputMuls').find('.form-group .input-group').append(string);
                $('#TimeKey9Month').select2();
            } else if (time == 2) {
                string = "<select class='form-control select2 w-p100' id='TimeKey6Month' name='TimeKey6Month'>"
                        + "<option value='1' selected='selected'>Đầu năm</option><option value='2'>Cuối năm</option></select>";
                $('.inputMuls').find('.form-group .input-group').append(string);
                $('#TimeKey6Month').select2();
            } else if (time == 3) {
                string = "<select class='form-control select2 w-p100' id='TimeKeyQuy' name='TimeKeyQuy'>"
                    + "<option value='1' selected='selected'>Quý 1</option><option value='2'>Quý 2</option> <option value='3'>Quý 3</option><option value='4'>Quý 4</option>  </select>";
                $('.inputMuls').find('.form-group .input-group').append(string);
                $('#TimeKeyQuy').select2();
            } else {
                string = "<select class='form-control select2 w-p100' id='TimeKeyMonth' name='TimeKeyMonth'>"
                    + "<option value='1' selected='selected'>Tháng 1</option><option value='2'>Tháng 2</option> <option value='3'>Tháng 3</option><option value='4'>Tháng 4</option>  "
                    + "<option value='5' >Tháng 5</option><option value='6'>Tháng 6</option> <option value='7'>Tháng 7</option><option value='8'>Tháng 8</option>  "
                    + "<option value='9' >Tháng 9</option><option value='10'>Tháng 10</option> <option value='11'>Tháng 11</option><option value='12'>Tháng 12</option> </select>";
                $('.inputMuls').find('.form-group .input-group').append(string);
                $('#TimeKeyMonth').select2();
            }
        },
        renderSearchStatitic: function () {;
            var to = false;
            $('#docTreeDepartment').jstree(true).close_all();
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#deliverable_search').val();
                if (!v) {
                    $('#docTreeDepartment').jstree(true).open_all();
                }
                $('#docTreeDepartment').jstree(true).search(v);
            }, 250);
        },
        renderIndicatorValues: function () {
            $.ajax({
                url: "/dashboard/GetDepartment",
                type: "Get",
                error: function (a, b, c) {

                },
                success: function (result) {
                    //treeIncatalog
                    var kpis = _.map(result, function (kpi) {
                        kpi["icon"] = "fa fa-tree"
                        return kpi
                    });
                    $("#docTreeDepartment").jstree({
                        'core': {
                            "mulitple": false,
                            "animation": 100,
                            "check_callback": true,
                            "themes": {
                                "variant": "medium",
                                "dots": true
                            },
                            'data': kpis
                        },
                        "types": {
                            "root": {
                                "icon": "glyphicon glyphicon-plus"
                            },
                            "child": {
                                "icon": "glyphicon glyphicon-leaf"
                            },
                            "default": {
                            }
                        },
                        "checkbox": {
                            "keep_selected_style": false,
                            "three_state": true,
                            "whole_node": true
                        },
                        "conditionalselect": function (node, event) {
                            return false;
                        },
                        "search": {
                            "show_only_matches": true,
                            "show_only_matches_children": true
                        },
                        "plugins": [
                              "dnd",
                             "types",
                             "unique",
                             "changed",
                             "html_data",
                             "themes",
                             "ui",
                             "crrm",
                             "search",
                             'checkbox'
                        ]
                    }).bind("loaded.jstree", function (event, data) {
                        $(this).jstree("open_all");
                    });
                }
            });
        },
        renderClickTree: function (e) {
            var currentTarget = e.currentTarget;
            var closestLi = $(currentTarget).closest('li')
            var valueID = $(closestLi).attr('id');
            if ($(currentTarget).hasClass('jstree-clicked')) {
            } else {
            }

            //ajax


            //var typeTimeKey = $('#TimeKey').val();

            //var timeKey = ConvertTime(typeTimess, typeTimeKey);

            //var docmodesId = $(this).attr('dataid');
        },
        renderClickDetail: function (e) {

            var that = this;
            //$('#staticResultDepartment').find('tbody tr').remove();
            table.clear().draw();
            var typeTimess = parseInt($('#TimeYear').val());
            var typeTimeKey = $('#TimeKey').val();
            var valueReportModeID = parseInt(that.$('#ReportModeId').val());

            var timeKey;
            if (typeTimeKey == 1) {
                timeKey = typeTimess;
            } else if (typeTimeKey == 8) {
                var time9Month = $('#TimeKey9Month').val();
                timeKey = typeTimess + "0" + time9Month;
            } else if (typeTimeKey == 2) {
                var timeYearQuy = $('#TimeKey6Month').val();
                timeKey = typeTimess + "" + timeYearQuy;
            } else if (typeTimeKey == 3) {
                var timeYearQuy = $('#TimeKeyQuy').val();
                timeKey = typeTimess + "" + timeYearQuy;
            } else {
                //4
                var TimeMonth = $('#TimeKeyMonth').val();
                if (TimeMonth < 10) {
                    timeKey = typeTimess + "0" + TimeMonth;
                } else {
                    timeKey = typeTimess + "" + TimeMonth;
                }
            }
            

            //get id department
            var valueID;
            var jsTree = $('#docTreeDepartment').find('ul');
            for (var i = 0 ; i < jsTree.length; i++) {
                var el = jsTree[i];
                var els = $(el).find('a');
                _.each(els, function (item, index) {
                    if ($(item).hasClass('jstree-clicked')) {
                        var closestLi = $(item).closest('li')
                        valueID = $(closestLi).attr('id');
                    }
                });
                if (i == 0) {
                    break;
                }
            }

            
            var checked_ids = [];
            var selectedNodes = $('#docTreeDepartment').jstree("get_selected", true);
            $.each(selectedNodes, function () {
                checked_ids.push(this.id);
            });
            // You can assign checked_ids to a hidden field of a form before submitting to the server
            var ValueDeparts = checked_ids;

            var data_to_send = JSON.stringify(ValueDeparts);


            $('#egovStatuss').css('display', 'block');
            //ajax old
            /*
            $.ajax({
                url: '/Dashboard/GetReportStatiticDocument',
                type: 'GET',
                data: { depart: valueID, reportModeid: valueReportModeID, actionCode: typeTimeKey, timekey_: timeKey },
                success: function (result) {   
                    for (var i = 0 ; i < result.length; i++) {
                        var parserTotal = result[i].doctype;
                        var arrayDoctype = [], tt = 0, objDoctype = {};
                        objDoctype.DocTypeId = parserTotal.DocTypeId;
                        if (parserTotal.CategoryBusinessId == 4) {
                            objDoctype.CategoryBusinessId = "Báo cáo số liệu"
                        }else if(parserTotal.CategoryBusinessId == 8){
                            objDoctype.CategoryBusinessId = "Báo cáo thuyết minh"
                        }

                        objDoctype.DocTypeName = parserTotal.DocTypeName;
                        objDoctype.docmodeId = parserTotal.ReportModeId;
                        var parseJsonResult = result[i].reportDoctype;
                        for (var j = 0 ; j < parseJsonResult.length ; j++) {
                            if (parseJsonResult[j].Status == 0 && (parseJsonResult[j].CategoryBussiness == 4 || parseJsonResult[j].CategoryBussiness == 8)) {
                                // chua xu ly
                                objDoctype.Status = "Chưa xử lý";
                            }
                            if (parseJsonResult[j].Status == 2 && (parseJsonResult[j].CategoryBussiness == 4 || parseJsonResult[j].CategoryBussiness == 8)) {
                                // dang xu ly
                                objDoctype.Status = "Đang xử lý";
                            }
                            if (parseJsonResult[j].StatusReport == 4 && (parseJsonResult[j].Status == 2 || parseJsonResult[j].Status == 4)
                                && (parseJsonResult[j].CategoryBussiness == 4 || parseJsonResult[j].CategoryBussiness == 8)) {
                                // da xu ly
                                objDoctype.Status = "Đã báo cáo";
                            }
                            objDoctype.Stt = j;
                            objDoctype.departmentName = parseJsonResult[j].DepartmentName;
                            objDoctype.NameDocType = parseJsonResult[j].nameDocType;
                            objDoctype.actionLevel = parseJsonResult[j].ActionLevel;
                            objDoctype.userName = parseJsonResult[j].UserName;

                            arrayDoctype.push(objDoctype);
                            var data = $.tmpl($('#templaceStaticDepartment'), arrayDoctype);
                            $('#staticResultDepartment').find('tbody').append(data);
                            arrayDoctype = [];
                        }
                        $('#egovStatuss').css('display', 'none');
                        
                        
                    }
                },
                error: function (xhr) { },
                complete: function () {
                }
            });
            // end ajax old
            */

            var statusNumber = $('#StatusStatic').val();
            var statusReport_1 = 0, statusReport_2 = 0;
            if (statusNumber == 424 || statusNumber == 212) {
                statusReport_1 = statusNumber % 10; // Status = 4
                statusReport_2 = Math.round((statusNumber % 100) / 10); // Status = 2
                statusNumber = Math.round(statusNumber / 100);// StatusReport = 4
            }
            
            var formatFactory = function (html) {
                    function parse(html, tab = 0) {
                        var tab;
                        var html = $.parseHTML(html);
                        var formatHtml = new String();   

                        function setTabs () {
                            var tabs = new String();

                            for (i=0; i < tab; i++){
                                tabs += '\t';
                            }
                            return tabs;    
                        };


                        $.each( html, function( i, el ) {
                            if (el.nodeName == '#text') {
                                if (($(el).text().trim()).length) {
                                    formatHtml += setTabs() + $(el).text().trim() + '\n';
                                }    
                            } else {
                                var innerHTML = $(el).html().trim();
                                $(el).html(innerHTML.replace('\n', '').replace(/ +(?= )/g, ''));
                

                                if ($(el).children().length) {
                                    $(el).html('\n' + parse(innerHTML, (tab + 1)) + setTabs());
                                    var outerHTML = $(el).prop('outerHTML').trim();
                                    formatHtml += setTabs() + outerHTML + '\n'; 

                                } else {
                                    var outerHTML = $(el).prop('outerHTML').trim();
                                    formatHtml += setTabs() + outerHTML + '\n';
                                }      
                            }
                        });

                        return formatHtml;
                    };   
    
                        return parse(html.replace(/(\r\n|\n|\r)/gm," ").replace(/ +(?= )/g,''));
                    };
            

            //begin ajaxnew
            $.ajax({
                url: '/Dashboard/GetAllStatiticDocument',
                type: 'GET',
                data: {
                    depart: JSON.parse(data_to_send),
                    reportModeid: valueReportModeID,
                    actionCode: typeTimeKey,
                    timekey_: timeKey,
                    Status: statusNumber,
                    StatusReport_1: statusReport_1,
                    StatusReport_2: statusReport_2
                },
                traditional: true,
                success: function (result) {
                    $('#egovStatuss').css('display', 'none');
                    for (var i = 0 ; i < result.length; i++) {
                        var parserTotal = result[i];
                        var arrayDoctype = [], tt = 0, objDoctype = {};
                        objDoctype.DocTypeId = parserTotal.DocTypeId;
                        objDoctype.DocumentId = parserTotal.DocumentId;
                        if (parserTotal.CategoryBusinessDocument == 4) {
                            objDoctype.CategoryBusinessId = "Báo cáo số liệu"
                        } else if (parserTotal.CategoryBusinessDocument == 8) {
                            objDoctype.CategoryBusinessId = "Báo cáo thuyết minh"
                        }

                        objDoctype.DocTypeName = parserTotal.Compendium;
                        objDoctype.docmodeId = parserTotal.ReportModeId;

                        if (parserTotal.Status == 0 && (parserTotal.CategoryBusinessDocument == 4 
                            || parserTotal.CategoryBusinessDocument == 8)) {
                            // chua xu ly
                            objDoctype.Status = "Chưa xử lý";
                        }
                        if (parserTotal.StatusReport != undefined) {
                            if (parserTotal.Status == 2 && (parserTotal.StatusReport == 2 || parserTotal.StatusReport == 1)
                                && (parserTotal.CategoryBusinessDocument == 4 || parserTotal.CategoryBusinessDocument == 8
                                || parserTotal.CategoryBusinessDocument == 64)) {
                                // dang xu ly
                                objDoctype.Status = "Đang xử lý";
                            }
                            if (parserTotal.StatusReport == 4 && (parserTotal.Status == 2 || parserTotal.Status == 4)
                                    && (parserTotal.CategoryBusinessDocument == 4 || parserTotal.CategoryBusinessDocument == 8 ||
                                    parserTotal.CategoryBusinessDocument == 64)) {
                                // da xu ly
                                objDoctype.Status = "Đã báo cáo";
                            }
                            if (parserTotal.Status == 8) {
                                continue;
                            }
                            objDoctype.Stt = i;
                            objDoctype.departmentName = parserTotal.InOutPlace;
                            objDoctype.NameDocType = parserTotal.DocTypeNameDocType;
                            objDoctype.actionLevel = parserTotal.ActionLevel;
                            objDoctype.userName = parserTotal.UserCreatedName;
                            objDoctype.Name = parserTotal.Name;
                            //xl date
                            var newDate = new Date(parserTotal.DateCreated);
                            let date = ("0" + newDate.getDate()).slice(-2);
                            let month = ("0" + (newDate.getMonth() + 1)).slice(-2);
                            let year = newDate.getFullYear();
                            let hours = newDate.getHours();
                            let minutes = newDate.getMinutes();
                            let seconds = newDate.getSeconds();
                            var dateStrEnd = year + "-" + month + "-" + date + " " + hours + ":" + minutes + ":" + seconds;
                            //end date
                            objDoctype.DateCreated = dateStrEnd;
                            arrayDoctype.push(objDoctype);

                            var data = $.tmpl($("#templaceStaticDepartment"), arrayDoctype);
                            var strS = data[0].outerHTML;
                            var strResult = formatFactory(strS);
                            table.rows.add($(strResult)).draw();

                            

                            //var data = $.tmpl($('#templaceStaticDepartment'), arrayDoctype);
                            //$('#staticResultDepartment').find('tbody').append(data);
                            arrayDoctype = [];
                        }
                        
                        
                       
                    }
                },
                error: function (xhr) { },
                complete: function () {
                }
            });
            //end ajaxnew
        },
        renderClickDetailView: function (e) {
            var that = this;
        }
    });

    var appview = new AppView();
})();