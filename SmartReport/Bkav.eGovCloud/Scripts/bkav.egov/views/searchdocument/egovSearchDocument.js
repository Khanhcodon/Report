(function () {

    //url ajax
    var searchUrl = {
        searhDocuments: '/SearchDocument/SearchDocument',
        quickSearchs: '/SearchDocument/QuickSearch',
        searchDocumentQSs: '/SearchDocument/SearchDocumentQS',
        searhAdvanceDocuments: '/SearchDocument/SearchAdvanceDocument',
        renderCompendiums: '/SearchDocument/RenderCompendiumData',
        renderReportRules: '/SearchDocument/RenderReportRule',
        renderReportRuleArray: '/SearchDocument/RenderReportRuleArray',
        renderReportRuleDetail: '/SearchDocument/renderReportRuleDetai',
        GetCompendiums: '/SearchDocument/GetCompendium',
        GetAttachment: '/SearchDocument/GetFileData'
    };

    var tabType = {
        document: 0,
        store: 1,
        search: 2,
        report: 4,
        print: 8,
        addImagePacket: 16
    };


    //#region Model
    var SearchDocumentModel = Backbone.Model.extend({
        defaults: {
            Compendium: '', // tên báo cáo
            CategoryId: null,
            KeyWord: '',
            Content: '',
            DocCode: '',
            InOutCode: '',
            UrgentId: null,
            CategoryBusinessId: null, // loại báo cáo
            StoreId: null,
            StorePrivateId: null,
            CurrentUserId: null,
            InOutPlace: '',//đơn vị nhận
            FromDateStr: '',
            ToDateStr: '',
            FromPubDateStr: '',
            ToPubDateStr: '',
            BeforeDate: '',
            AfterDate: '',
            OrganizationCreate: '',
            DocFieldId: null,
            UserSuccessId: null,
            Page: 1,
            UserCreatedName: '', //đơn vị giao
            IsMainProcess: true,
            IsUseCached: false,
            IsRelationDoc: false,
            DocTypeCode: '', // mã báo cáo
            ReportModeId: null,
            Status: null,
            ActionLevel: null,
            TimeKey: '',
            ReportRuleIdOnly: null,
            UnitDelivery: '',
            UnitReceive: ''
        }
    });

    var SearchDocumentModel_New = Backbone.Model.extend({
        defaults: {
            Compendium: '', // tên báo cáo
            CategoryId: null,
            KeyWord: '',
            Content: '',
            DocCode: '',
            InOutCode: '',
            UrgentId: null,
            CategoryBusinessId: null, // loại báo cáo
            StoreId: null,
            StorePrivateId: null,
            CurrentUserId: null,
            InOutPlaceId: null,
            FromDateStr: '',
            ToDateStr: '',
            FromPubDateStr: '',
            ToPubDateStr: '',
            BeforeDate: '',
            AfterDate: '',
            OrganizationCreate: '',
            DocFieldId: null,
            UserSuccessId: null,
            Page: 1,
            UserCreateId: null,
            IsMainProcess: true,
            IsUseCached: false,
            IsRelationDoc: false,
            DocTypeCode: '',
            ReportModeId: null,// mã báo cáo
            Status: null,
            ActionLevel: null,
            TimeKey: ''
        }
    });
    //#endregion
    $.ajax({
        url: searchUrl.quickSearchs,
        data: {
            query: "",
            type: 1,
            IsUseCached: false
        },
        success: function (result) {
            var that = this;
            var egovSearchDocument = new EgovSearchDocument({
                el: "#boxTotalSearch",
                model: result,
                searchQuery: "",
                searchType: 1,
                IsRelationDoc: false
            });
            that.isLoadedContent = true;
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });

    var NUMBER_ROW_IN_PAGE = 25;
    var EgovSearchDocument = Backbone.View.extend({
        template: "#searchNormalDocument",
        preRelationDocItems: [], // lưu lại mảng tìm kiếm trước đó
        documentDetails: [],

        initialize: function (option) {
            this.searchDocumentModel = new SearchDocumentModel();
            this.searchType = 1; //egov.enum.searchType.document
            this.currentPage = 1;

            if (option) {
                if (option.IsRelationDoc) {
                    this.isRelationDoc = option.IsRelationDoc;
                    //this.currentDoc = option.currentDoc;
                }

                this.searchType = option.searchType ? option.searchType : 1;
                this.searchQuery = option.searchQuery ? option.searchQuery : 1;
                this.isLoadSearchAdvance = false;
                this.isQuickSearch = true;
                if (option.model) {
                    this.model = option.model;
                    this.model.SearchQuery = option.searchQuery;
                    this.pageSize = this.model.Items.length;
                }
            }
            this.render();
        },

        events: {
            "click #btnAdv_1": "_showAdvanceDocument",
            "click #btnAdv": "_showSearchNew",
            "change #ActionLevel": "_changeActionLevel",
            "change #ReportModeId": "_changeReportModeId",
            "change #ReportRuleId": "_changeReportRuleId",
            "change  #Attachment": "_changeAttachment"
        },

        render: function () {
            var that = this;
            that._renderAllDocument();
        },

        _renderAllDocument: function () {
            var that = this;
            if (!that.isRelationDoc) {
                //kiem tra neu khong phai tim kiem van ban lien quan
                that.$el.append($.tmpl($(that.template), that.model));
            } else {
                that.$el.append($.tmpl($(that.template), { isRelationDoc: true}));
            }

            if (that.model) {
                if (!that.isRelationDoc) {
                    that.renderPager();
                }
                that.renderResult();
            }

            that.$("form").submit(function (e) {
                that._searchView();
                e.preventDefault();
                return false;
            })
            
            //that._datepickerFunction();
            that._renderData();
            that._select2Option();
        },

        renderPager: function () {
            var that = this;
            that.$el.closest('.content').find('#boxResultTable .paging').html(new eGovPaging({
                total: that.model.TotalResult,
                pageSize: NUMBER_ROW_IN_PAGE,
                select: function (selectedPage) {
                    that._changePage(selectedPage);
                }
            }).$el);

            return;
        },
        
        _renderData: function(){
            
        },

        renderResult: function(){
            //hiển thị kết quả search
            var that = this,
                items = that.model.Items,
                results = [], resultItem;

            if (items.length === 0) {
                $("#example1 tbody").empty(); 
                $("#example1 tbody > tr:first-child").remove();
                $("#example1 tbody").append("<tr><td class='emptyResult' colspan='10'>Không có kết quả hợp lệ</td></tr>");
                $("#example1_wrapper #example1_length").remove();
                $("#example1_wrapper #example1_filter").remove();
                $("#example1_wrapper #example1_info").remove();
                $("#example1_wrapper #example1_paginate").remove();

            }
            if (items.length > 0) {
                $("#example1 tbody").find('.emptyResult').remove();
            }
            
            if (that.searchType == 1) {
                that.$("#example1").show();
            }

            if (that.isRelationDoc) {
                that.preRelationDocItems = that.model.Items;
                for (var i = 0 ; i < items.length; i++) {
                    var doc = items[i];
                    doc.Index = (that.currentPage - 1) * that.pageSize + i + 1;
                    doc.isSelected = false;
                    doc.isRelationDoc = that.isRelationDoc;
                    var item = new SearchDocumentModel_New(doc);
                    results.push(item);
                    //render ra tr td
                    resultItem = new SearchResultTable({
                        model: item,
                        parent: that
                    });
                    that.$("#example1 tbody").append(resultItem.$el);
                }
            } else {
                for (var i = 0; i < items.length; i++) {
                    var doc = items[i];
                    doc.Index = (that.currentPage - 1) * that.pageSize + i + 1;
                    doc.isSelected = false;
                    doc.isRelationDoc = that.isRelationDoc;  
                    if (that.searchType == 1) {
                        doc.DocumentCompendium = unescape(doc.DocumentCompendium);
                        doc.Title = doc.DocumentCompendium;
                        var item = new SearchResult({
                            model: doc,
                            template: "#resultSearchDocument",
                            parent: that
                        });
                    }
                }      
            }
        },

        _changePage: function (page) {
            if (page == 0 || page === this.currentPage) {
                return;
            }

            this.currentPage = page;
            var that = this;

            if (that.isQuickSearch) {
                $.ajax({
                    url: searchUrl.quickSearchs,
                    data: {
                        query: that.searchQuery,
                        type: 1,
                        isUseCached: true,
                        page: page
                    },
                    success: function (result) {
                        that.model = result;
                        that.renderResult();
                    }
                });
            } else {             
                that._searchView(page, true);
            }
        },

        _datepickerFunction: function(){
            var that = this;

            that.$('#FromDateStr').datepicker({
                dateFormat: 'dd/mm/yy'
            });
            that.$("#ToDateStr").datepicker({
                dateFormat: 'dd/mm/yy'
            });

            $('.calendarFrom').click(function () {
                $("#FromDateStr").focus();
            });

            $('.calendarTo').click(function () {
                $('#ToDateStr').focus();
            });
        },

        _select2Option: function(){
            $('#ReportModeId').select2();
            $('#CategoryBusinessId').select2();
            $('#Status').select2();
            $('#ActionLevel').select2();
            $('#CompendiumName').select2();
            $('#Attachment').select2();
            $('#ReportRuleId').select2();
            $('#UserCreatedName').select2();
            $('#InOutPlace').select2();
        },

        _showAdvanceDocument: function () {
            var that = this;
            $('.rowSearch_1').removeClass("d-none");
            //$('.select2-container').css('width', '512.656px');
            //$('#row_1').find('.form-group').find('span.select2-container--default').css('width', '241.328px');
            //$('#row_2').find('.form-group').find('span.select2-container--default').css('width', '241.328px');
            //$('#row_3').find('.form-group').find('span.select2-container--default').css('width', '241.328px');
            //$('#row_4').find('.form-group').find('span.select2-container--default').css('width', '241.328px');
            $('.rowSearch_1').show();
            $('#btnAdv').css('display', 'block');
            $('#btnAdv_1').css('display', 'none');
            $('#btnAdv').removeAttr("style");
            $('#btnAdv').css('border-color', '#CE7A58');
            $('#btnAdv').css('color', '#CE7A58');
            $('#btnAdv').css('background-color', '#fff');
            $('#btnAdv').css('font-weight', 'bold');
            that._datepickerFunction();
        },

        _showSearchNew: function () {
            $('.rowSearch_1').hide();
            $('#btnAdv').css('display', 'none');
            //$('#btnAdv_1').css('display', 'block');
            $('#btnAdv_1').removeAttr("style");
            $('#btnAdv_1').css('border-color', '#CE7A58');
            $('#btnAdv_1').css('color', '#CE7A58');
            $('#btnAdv_1').css('background-color', '#fff');
            $('#btnAdv_1').css('font-weight', 'bold');
        },

        _changeActionLevel: function () {
            var that = this;
            var opval = that.$("#ActionLevel").val();
            var string, stringYear, stringYearMonth, stringMonth, string6Month, stringYear6Month, stringYear9Month, string9Month;
            $("#TimeKeyCommon").find('.modal-body .form-group').remove();
            $("#TimeKeyCommon").find('.modal-body').append('<div class="form-group"></div>');
            if (opval == 1) {
                //năm   
                string = "<label>Chọn năm</label><input type='text' id='TimeKeys' name='TimeKeys' class='form-control checkTimeKey' placeholder='Nhập thời gian' /> "; 
                that._renderModelTimeKey(string, null);
            }
            if (opval == 8) {
                // 9 tháng
                stringYear9Month = "<label>Chọn năm</label><input type='text' id='TimeKeyYear9Month' name='TimeKeyYear9Month' class='form-control checkTimeKey' placeholder='Nhập thời gian' />";
                string9Month = "<label>9 tháng</label><input type='text' id='TimeKey9Month' name='TimeKey9Month' class='form-control checkTimeKey' placeholder='Nhập thời gian' value='9' readonly/>";

                that._renderModelTimeKey(stringYear9Month, string9Month);
            }
            if (opval == 2) {
                //6 tháng
                stringYear6Month = "<label>Chọn năm</label><input type='text' id='TimeKeyYear6Month' name='TimeKeyYear6Month' class='form-control checkTimeKey' placeholder='Nhập thời gian' />";
                string6Month = "<label>6 Tháng</label><select class='form-control select2 w-p100' id='TimeKey6Month' name='TimeKey6Month'>"
                +"<option value='1' selected='selected'>Đầu năm</option><option value='2'>Cuối năm</option>   </select>";
                that._renderModelTimeKey(stringYear6Month, string6Month); 
            }
            if (opval == 3) {
                //quý
                stringYear = "<label>Chọn năm</label><input type='text' id='TimeKeyYearQuy' name='TimeKeyYearQuy' class='form-control checkTimeKey' placeholder='Nhập thời gian' />";
                string = "<label>6 Tháng</label><select class='form-control select2 w-p100' id='TimeKeyQuy' name='TimeKeyQuy'>"
                + "<option value='1' selected='selected'>Quý 1</option><option value='2'>Quý 2</option> <option value='3'>Quý 3</option><option value='4'>Quý 4</option>  </select>";
                that._renderModelTimeKey(stringYear, string);
            }
            if (opval == 4) {
                //tháng
                stringYearMonth = "<label>Chọn năm</label><input type='text' id='TimeKeyYearMonth' name='TimeKeyYearMonth' class='form-control checkTimeKey' placeholder='Nhập thời gian' />";
                stringMonth = "<label>6 Tháng</label><select class='form-control select2 w-p100' id='TimeKeyMonth' name='TimeKeyMonth'>"
                + "<option value='1' selected='selected'>Tháng 1</option><option value='2'>Tháng 2</option> <option value='3'>Tháng 3</option><option value='4'>Tháng 4</option>  "
                + "<option value='5' >Tháng 5</option><option value='6'>Tháng 6</option> <option value='7'>Tháng 7</option><option value='8'>Tháng 8</option>  "
                + "<option value='9' >Tháng 9</option><option value='10'>Tháng 10</option> <option value='11'>Tháng 11</option><option value='12'>Tháng 12</option> </select>";
                that._renderModelTimeKey(stringYearMonth, stringMonth);
            }
        },
        _changeReportRuleId: function () {
            var that = this;
            var valueReportRuleID = parseInt(that.$('#ReportRuleId').val());
            var arrayReportMode;
            if (valueReportRuleID == -1) {
                $.ajax({
                    url: searchUrl.renderReportRules,
                    data: { reportmodeId: valueReportRuleID },
                    success: function (result) {
                        var options = [];
                        _.each(result, function (key, value) {
                            options.push({
                                text: key.DocTypeName,
                                id: key.ReportModeId
                            });
                        });
                        options.unshift({
                            text: "Chọn tất cả",
                            id: -1
                        })
                        $('#ReportModeId').empty().select2({
                            data: options
                        })
                    }
                });
            }
            else {
                //call ajax
                var options = [];
                options.unshift({
                    text: "Chọn tất cả",
                    id: -1
                })
                
                $.ajax({
                    url: searchUrl.renderReportRuleDetail,
                    data: { reportRuleId: valueReportRuleID },
                    success: function (result) {
                        // xu ly repormode
                        var datas = { reportmodeId: JSON.parse(result.ReportMode) };
                        $.ajax({
                            url: searchUrl.renderReportRuleArray,
                            data: datas,
                            traditional: true,
                            success: function (resultRule) {
                                _.each(resultRule, function (key, value) {
                                    options.push({
                                        text: key.DocTypeName,
                                        id: key.ReportModeId
                                    });
                                });
                                $('#ReportModeId').empty().select2({
                                    data: options
                                })
                            }
                        });
                    }
                });               
            } 
        },

        _changeReportModeId: function () {
            var that = this;
            var valueReportModeID = parseInt(that.$('#ReportModeId').val());
            if (valueReportModeID == -1) {
                $.ajax({
                    url: searchUrl.renderCompendiums,
                    data: { reportmodeId: valueReportModeID},
                    success: function (result) {
                        var options = [];
                        _.each(result, function (key, value) {
                            options.push({
                                text: key.DocTypeName,
                                id: key.DocTypeName++
                            });
                        });
                        options.unshift({
                            text: "Chọn tất cả",
                            id: -1
                        })
                        $('#CompendiumName').empty().select2({
                            data: options
                        })
                    }
                });
            } else {
                $.ajax({
                    url: searchUrl.renderCompendiums,
                    data: { reportmodeId: valueReportModeID },
                    success: function (result) {
                        var options = [];
                        _.each(result, function (key, value) {   
                            options.push({
                                text: key.DocTypeName,
                                id: key.DocTypeName
                            });
                        });
                        options.unshift({
                            text: "Chọn tất cả",
                            id: -1
                        })
                        $('#CompendiumName').empty().select2({
                            data: options
                        })
                    }
                });
            }
            
        },

        _changeAttachment: function(){
            var that = this;
            var valueAttachment = parseInt(that.$('#Attachment').val());
            //ajax
            $.ajax({
                url: searchUrl.GetAttachment,
                data: { id: valueAttachment },
                success: function (result) {
                    //call ajax
                    $.ajax({
                        url: '/AttachmentPreview/Index',
                        data: result,
                        beforeSend: function () {
                            showProgress();
                        },
                        success: function (_result) {
                            $('#ViewFile').find('#ViewFileAtt iframe').remove();
                            //$('#BodySearch').find('section').hide();
                            //$('#ifameFile').find('.opentFile').show();
                            $('#ViewFileAtt').append('<iframe id="fileShowJframe" src="/AttachmentPreview/Index?DocumentId='
                                + result.DocumentId + '&AttachmentId=' + result.AttachmentId + '" style="width: 100%; height: 100%; border: none;"></iframe>');
                            $('#ViewFile').modal("show");
                        },
                        error: function (xhr) {
                        },
                        complete: function () {
                        }
                    });
                }
            });
        },

        _renderModelTimeKey: function(tr1, tr2){
            if ($("#TimeKeyCommon").find('.modal-body .form-group input').hasClass("checkTimeKey")) {
                var timess = $('#TimeKeys').val();
                $("#TimeKeyCommon").modal("show");
            } else {
                if (tr2 == null) {
                    $("#TimeKeyCommon").find('.modal-body .form-group')
                .append(tr1);
                    $("#TimeKeyCommon").modal("show");
                } else {
                    $("#TimeKeyCommon").find('.modal-body .form-group')
                    .append(tr1 + '<br>' + tr2);
                    $("#TimeKeyCommon").modal("show");
                }
                
            }
        },

        //$('.content-wrapper').find('#example1 tbody tr td.view button#ViewDetail').html()
        _searchView: function (page, IsUseCached) {
            var that = this,
                currentRels;
            
            this.isQuickSearch = false;

            if (!page) {
                page = 1;
            }
            if (IsUseCached == undefined) {
                IsUseCached = false;
            }
            if (!IsUseCached) {
                this.currentPage = 1;
            }

            var ReportModeId_ = that.$('#ReportModeId').val(); // chế độ báo cáo
            var CategoryBusinessId_ = that.$("#CategoryBusinessId").val(); // loại báo cáo
            var CompendiumName_ = that.$("#CompendiumName").val(); // tên báo cáo
            var DocTypeCode_ = that.$("#DocTypeCode").val(); // mã báo cáo
            var UserCreatedName_ = that.$("#UserCreatedName").val(); //đơn vị giao
            var InOutPlace_ = that.$("#InOutPlace").val() // đơn vị nhận
            var Status_ = that.$("#Status").val();//trạng thái xử lý
            var ActionLevel_ = that.$("#ActionLevel").val()// kiểu kỳ báo cáo
            var ReportRuleValue = that.$('#ReportRuleId').val() // van ban quy dinh

            if (CompendiumName_ == "TuyChon") {
                CompendiumName_ = '';
            }

            var TimeKey_ = '';
            if (ActionLevel_ == 1) {
                //năm
                TimeKey_ = $('#TimeKeys').val();
            } else if (ActionLevel_ == 8) {
                // 9 tháng
                var timeYear = $('#TimeKeyYear9Month').val();
                var timeYear9 = $('#TimeKey9Month').val();
                TimeKey_ = timeYear + "0" + timeYear9;
            } else if (ActionLevel_ == 2) {
                //6 thangs
                var timeYear = $('#TimeKeyYear6Month').val();
                var timeYearQuy = $('#TimeKey6Month').val();
                TimeKey_ = timeYear + "" + timeYearQuy;
            } else if (ActionLevel_ == 3) {
                //quy
                var timeYear = $('#TimeKeyYearQuy').val();
                var timeYearQuy = $('#TimeKeyQuy').val();
                TimeKey_ = timeYear + "" + timeYearQuy;
            } else if (ActionLevel_ == 4) {
                // thang
                var timeYear = $('#TimeKeyYearMonth').val();
                var TimeMonth = $('#TimeKeyMonth').val();
                if (TimeMonth < 10) {
                    TimeKey_ = timeYear + "0" + TimeMonth;
                } else {
                    TimeKey_ = timeYear + "" + TimeMonth;
                }
            }


            if (ReportModeId_ == -1) {
                ReportModeId_ = null;
            }
            if (CategoryBusinessId_ == null) {
                CategoryBusinessId_ = null;
            }
            if (CompendiumName_ == -1) {
                CompendiumName_ = '';
            }
            if (DocTypeCode_ == "") {
                DocTypeCode_ = "";
            }

            if (UserCreatedName_ == '') {
                UserCreatedName_ = '';
            }
            if (InOutPlace_ == '') {
                InOutPlace_ = '';
            }
            if (Status_ == -1) {
                Status_ = null;
            }
            if (ActionLevel_ == -1) {
                ActionLevel_ = null;
            }
            if (CategoryBusinessId_ == -1) {
                CategoryBusinessId_ = null;
            }
            if (ReportRuleValue == -1) {
                ReportRuleValue =  null;
            }

            if (UserCreatedName_ == "") {
                UnitDelivery =  ""  
            } else {
                UnitDelivery = UserCreatedName_;
            }
            if (InOutPlace_ == "") {
                UnitReceive = ""
            } else {
                UnitReceive = InOutPlace_;
            }

            if (that.$("#FromDateStr").val() == '' || that.$("#ToDateStr").val() == '') {
                var stringFromDateStr = '';
                var stringToDateStr = '';
            } else {
                var FromDateStr_ = that.$("#FromDateStr").val().split("/");
                var stringFromDateStr = FromDateStr_[1].toString() + '/' + FromDateStr_[0].toString() + '/' + FromDateStr_[2].toString();

                var ToDateStr_ = that.$("#ToDateStr").val().split("/");
                var stringToDateStr = ToDateStr_[1].toString() + '/' + ToDateStr_[0].toString() + '/' + ToDateStr_[2].toString();
            }
            

            if (that.searchType === 1) {
                //this.serialize();
                this.searchDocumentModel.set('Page', page);
                this.searchDocumentModel.set('IsUseCached', IsUseCached);
                this.searchDocumentModel.set('Compendium', CompendiumName_); // tên báo cáo
                this.searchDocumentModel.set('CategoryBusinessId', CategoryBusinessId_); // loại báo cáo
                this.searchDocumentModel.set('FromDateStr', stringFromDateStr);
                this.searchDocumentModel.set('ToDateStr', stringToDateStr);
                this.searchDocumentModel.set('DocTypeCode', DocTypeCode_); // mã báo cáo
                this.searchDocumentModel.set('ReportModeId', ReportModeId_) // chế độ báo cáo
                this.searchDocumentModel.set('UserCreatedName', UserCreatedName_) // đơn vị giao
                this.searchDocumentModel.set('UnitDelivery', UnitDelivery) // đơn vị giao theo department
                this.searchDocumentModel.set('UnitReceive', UnitReceive) // đơn vị nhận theo department
                this.searchDocumentModel.set('InOutPlace', InOutPlace_)//đơn vị nhận
                this.searchDocumentModel.set('Status', Status_);//trạng thái
                this.searchDocumentModel.set('ActionLevel', ActionLevel_);// Kiểu kỳ báo cáo
                this.searchDocumentModel.set('TimeKey', TimeKey_);
                this.searchDocumentModel.set('ReportRuleIdOnly', ReportRuleValue);

                $('#egovStatuss').css('display', 'block');
                $.ajax({
                    url: searchUrl.searhAdvanceDocuments,
                    data: this.searchDocumentModel.toJSON(),
                    success: function (result) {
                        if (!that.IsRelationDoc) {
                            var temItems = result.Items;
                            that.model = result;
                            that.pageSize = NUMBER_ROW_IN_PAGE;
                            if (!that.IsRelationDoc && page === 1) {
                                that.renderPager();
                            }
                            that.$el.closest('.content').find('#boxResultTable tbody tr').remove();
                            that.renderResult();
                        }
                        $('#egovStatuss').css('display', 'none')
                    }
                });
            }
        }
    });

    var SearchResultTable = Backbone.View.extend({
        tagName: 'tr',
        template: "#resultSearchDocument",

        events: {
            
        },

        initialize: function (option) {
            var that = this;
            this.parent = option.parent;
            this.$el.append($.tmpl($(that.template), that.model.toJSON()));
        }
    });


    var SearchResult = Backbone.View.extend({
        tagName: "tbody",

        el: "#boxResultTable",

        initialize: function (option) {
            this.template = option.template;
            this.model = option.model;
            this.parent = option.parent;
            this.render();
        },

        events: {
            
        },

        render: function () {
            this.$el.find('tbody').append($.tmpl($(this.template), this.model));
        }
    });

    var ExportExcel = Backbone.View.extend({
        tagName: "tbody",

        el: "#boxResultTable",

        initialize: function (option) { 
            this.render();
        },

        events: {

        },

        render: function () {
            this.$el.find('tbody').append($.tmpl($(this.template), this.model));
        }
    });

    var ExportView = Backbone.View.extend({
        el: "#boxResultTable",
        events: {
            "click #btnExportExcelSearch": "_ExportExcelSearch"
        },
        initialize: function (options) {
            var that = this;
            that.render();
        },
        render: function () {
            var that = this;
        },
        _ExportExcelSearch: function () {
            var that = this;
            //var documentId =
            //    $("#example1 input[type=checkbox]:checked").map(function () {
            //        return this.id;
            //    }).get();
            var selected = new Array();
            $('#example1 input[type="checkbox"]:checked').each(function () {
                selected.push($(this).attr('id'));
            });
            
            var data_to_send = JSON.stringify(selected);
            var data = { model: JSON.parse(data_to_send) };
            $.ajax({
                url: "/ReportViewer/GetDocumentExport",
                data: data,
                type: 'GET',
                cache: false,
                traditional: true,
                success: function (result) {
                    if (result.length > 0) {
                        for (var i = 0 ; i < result.length; i++) {
                            var response = result[i];
                            var fileName_ = response.Compendium;
                            var dataProcessInfo = JSON.parse(response.ProcessInfo);
                            var a = _.keys(dataProcessInfo.header);
                            var items = _.map(a, function (element) {
                                return element.split("!!", 1);
                            });

                            //var formHeader = JSON.parse(dataProcessInfo.headerFooter).FormHeader;
                            //var formFooter = JSON.parse(dataProcessInfo.headerFooter).FormFooter;

                            var dt = dataProcessInfo.data;

                            var noteSS = JSON.parse(response.Note);
                            var dataSS = dataProcessInfo.data;
                            dataProcessInfo.data = noteSS;
                            var dataSS = dataProcessInfo.data;


                            var headerSets = dataProcessInfo.extra.headerSetting;

                            var headerNes = dataProcessInfo.extra.mergedCells.length ? dataProcessInfo.extra.mergedCells : dataProcessInfo.headerNested;
                            var b = _.keys(dataProcessInfo.extra.columnSetting);

                            var columnSet = _.map(b, function (element) {
                                return dataProcessInfo.extra.columnSetting[element]["Hidden"];
                            });

                            var colWidth = _.map(dataProcessInfo.colWidths, function (element) {
                                return element;
                            });

                            var classcell = dataProcessInfo.classCells;

                            $.ajax({
                                type: "POST",
                                url: "/SearchDocument/exportToExcel",
                                data: {
                                    header: JSON.stringify(items),
                                    data: JSON.stringify(dataSS),
                                    headerSeting: JSON.stringify(headerSets),
                                    headerNested: JSON.stringify(headerNes),
                                    columnSetting: JSON.stringify(columnSet),
                                    colWidths: JSON.stringify(colWidth),
                                    classCells: JSON.stringify(classcell),
                                    //stringHeader: formHeader,
                                    //stringFooter: formFooter,
                                    fileName: fileName_
                                },
                                success: function (response) {
                                    //$.ajax({
                                    //    url: "/SearchDocument/Download",
                                    //    data: {
                                    //        fileGuid: response.FileGuid,
                                    //        filename: response.FileName
                                    //    },
                                    //    success: function (response) {

                                    //    }
                                    //});
                                    $("<a>").attr("href", response).attr("target", "_blank")[0].click();
                                    //window.location = '/SearchDocument/Download?fileGuid=' + response.FileGuid
                                    //    + '&filename=' + response.FileName;
                                }
                            });
                        }
                    }
                    
                }
            });
        }
    });
    
    var eGovPaging = Backbone.View.extend({
        tagName: "nav",
        template: '<ul class="pagination pagination-sm"></ul>',
        pageTemplate: '<li><a href="#" page="{0}" class="page">{0}</a></li>',

        disabledClass: "disabled",
        activeClass: "active",
        events: {
            'click a.page': '_selectPage',
            'click a.next': '_nextPage',
            'click a.previour': '_prevPage',
            'click .btnGotoPage': '_gotoInput'
        },

        option: {
            pageSizeOption: [50, 100, 150, 200],
            pageSize: 50,
            total: 0,
            totalPage: 0,
            currentPage: 1,
            displayPageCount: 4, // Hiển thị 5 trang gần trang hiện tại
            showPrevious: true,
            showNext: true,
            shopChangePageSize: true,
            showGoInput: true,
            autoHidePrevious: true,
            autoHideNext: true,
            showNavigator: true,
            showPageNumber: true,
            onload: null,
            select: null
        },

        initialize: function (option) {
            this.$el.attr("aria-label", "Page navigation");

            this.option = $.extend({}, this.option, option);

            // Todo: xử lý trường hợp bằng vừa đúng thì mới
            this.option.totalPage = Math.ceil(this.option.total / this.option.pageSize);

            if (!this.option.showPageNumber) {
                this.option.showGoInput = false;
            }

            this.render();
        },

        render: function () {
            this.$el.html(this.template);
            this.$pager = this.$(".pagination");

            // Không hiển thị khi chỉ có 1 trang hoặc không có trang nào
            if (this.option.totalPage === 0 || this.option.totalPage === 1) {
                return this;
            }

            // Trang trước
            if (this.option.showPrevious) {
                this.$pager.append('<li><a href="#" aria-label="Previous" class="previour"><span aria-hidden="true">&laquo;</span></a></li>');
            }

            if (this.option.showPageNumber) {
                // các trang đang xem
                this._renderPages(this.option.currentPage);
            }

            // Trang tiếp theo
            if (this.option.showNext) {
                this.$pager.append('<li><a href="#" aria-label="Next" class="next"><span aria-hidden="true">&raquo;</span></a></li>');
            }

            // Goto page
            if (this.option.showGoInput) {
                var gotoInput = this._bindGotoInput();
                this.$el.append(gotoInput);
            }

            this._activeCurrent();

            // Onloaded
            if (this.option.onload && typeof this.option.onload === "function") {
                this.option.onload(this);
            }
        },

        show: function () {
            this.$el.show();
        },

        hide: function () {
            this.$el.hide();
        },

        getSelectedPage: function () {
            return this.option.currentPage;
        },

        nextPage: function () {
            this._nextPage();
        },

        previourPage: function () {
            this._prevPage();
        },

        _renderPages: function (currentPage) {
            var startPage, toPage;

            if (this.option.totalPage <= this.option.displayPageCount) {
                startPage = 1; toPage = this.option.totalPage;

                for (var i = startPage; i <= toPage ; i++) {                    
                    var stringFormat = this.pageTemplate;
                    this.$pager.append(stringFormat.split("{0}").join(i));
                }

                return;
            }

            var haftPageCount = Math.floor(this.option.displayPageCount / 2);
            startPage = currentPage - haftPageCount;
            if (startPage <= 0) {
                startPage = 1;
            }

            toPage = startPage + this.option.displayPageCount;
            // Trường hợp chọn vào các trang gần cuối => toPage > totalPage => Tính lại
            if (toPage > this.option.totalPage) {
                toPage = this.option.totalPage;
                startPage = toPage - this.option.displayPageCount;
            }

            var firstPage = 1;
            // Hiển thị trang 1
            if (startPage > firstPage) {
                var stringFormat = this.pageTemplate;
                this.$pager.append(stringFormat.split("{0}").join(firstPage));

                if (startPage > haftPageCount + firstPage) {
                    var stringFormat = this.pageTemplate;
                    // Khi startPage > 1 nhiều thì mới hiển thị ...
                    var stringSuccs = this.$pager.append(stringFormat.split("{0}").join('...'));
                    stringSuccs.addClass(this.disabledClass);
                } else {
                    // Hiển thị luôn nếu gần 1 ví dụ: 1  2  3  4  5  6  7
                    for (var i = firstPage + 1; i < startPage; i++) {
                        var stringFormat = this.pageTemplate;
                        this.$pager.append(stringFormat.split("{0}").join(firstPage));
                    }
                }
            }

            for (var i = startPage; i <= toPage ; i++) {
                var stringFormat = this.pageTemplate;
                this.$pager.append(stringFormat.split("{0}").join(i));
            }

            if (toPage < this.option.totalPage) {
                // Hiển thị trang cuối nếu toPage nhỏ hơn totalPage nhiều
                if (this.option.totalPage > haftPageCount + toPage) {
                    var stringFormat = this.pageTemplate;
                    var stringSuccs = this.$pager.append(stringFormat.split("{0}").join('...'));
                    stringSuccs.addClass(this.disabledClass);
                } else {
                    for (var i = toPage + 1; i < this.option.totalPage; i++) {
                        var stringFormat = this.pageTemplate;
                        this.$pager.append(stringFormat.split("{0}").join(i));
                    }
                }
                var stringFormat = this.pageTemplate;
                this.$pager.append(stringFormat.split("{0}").join(this.option.totalPage));
            }
        },

        _nextPage: function () {
            var page = this.option.currentPage + 1;
            this._gotoPage(page);
        },

        _prevPage: function () {
            var page = this.option.currentPage - 1;
            this._gotoPage(page);
        },

        _selectPage: function (e) {
            var target = $(e.target).closest("a");
            if (target.parent().is(".disabled")) {
                return;
            }

            var page = target.attr("page");
            this._gotoPage(page);
        },

        _gotoInput: function (e) {
            var page = this.$(".gotopage").val();
            this._gotoPage(page);
        },

        _gotoPage: function (page) {
            page = parseInt(page);
            if (page > this.option.totalPage) {
                page = this.option.totalPage;
            }

            this.option.currentPage = page;
            this.render();
            this._callback();
        },

        _activeCurrent: function () {
            var currentPage = this.option.currentPage;
            var element = this.$('a[page="' + currentPage + '"]');
            if (element.length === 0) {
                return;
            }

            var parentLi = element.parent();
            if (parentLi) {
                parentLi.siblings().removeClass(this.activeClass);
                parentLi.addClass(this.activeClass);
            }

            if (this.option.showGoInput) {
                this.$(".gotopage").val(currentPage);
            }
        },

        _bindGotoInput: function () {
            var result = $("<ul class='pagination pagination-sm paging-input' style='margin-left:15px;'>");
            result.append("<li style='border: none'><a>Xem trang: <input type='text' class='gotopage' style='width: 20px;border: none;padding: 0;'/></a></li>");
            result.append("<li style='border: none'><a><span href='#' class='btnGotoPage'>xem</span></a></li>");

            return result;
        },

        _callback: function () {
            if (this.option.select && typeof this.option.select === 'function') {
                this.option.select(this.option.currentPage);
            }
        }
    });

    var searchView = new EgovSearchDocument();
    var appviewDownload = new ExportView();
})();