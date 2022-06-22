define([
    "BusinessLicenseShowView",
    egov.template.businessLicense,
    egov.template.business,
], function ( BusinessLicenseDocView, Template, TemplateBusiness) {
    // View tạo mới doanh nghiệp
    var BusinessCreateView = Backbone.View.extend({

        template: TemplateBusiness,

        el: "p",

        events: {
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.document = options.model;
            this.elementBusinessLicenseView = options.el;
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var config = { BusinessName: "", BusinessType: 1 }
            this.$el.html($.tmpl(this.template, config));
            var that = this;
            var dialogSetting = {
                width: 600,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Doanh nghiệp",
                buttons: [
                      {
                          text: "Lưu Doanh Nghiệp",
                          className: "btn-success",
                          click: function () {
                          }
                      },
                      {
                           text: egov.resources.common.closeButton,
                           click: function () {
                               that.$el.dialog("hide");
                               var document = that.document
                               var view = new BusinessLicenseView({ document: document });
                           }
                      }
                ]
            };

            that.$el.dialog(dialogSetting);

            return that;
        },
    });

    var BusinessLicenseModel = function (DocumentCopyId, CitizenName) {
        return {
            CitizenName: "",
            BusinessLicenseId: "",
            BusinessId: "",
            BusinessName: "",
            DocTypeId: "",
            DocumentId: "",
            DocumentCopyId: DocumentCopyId,
            LicenseStatusId: "",
            LicenseCode: "",
            LicenseNumber: "",
            RegisDate: getFormattedDate(new Date()),
            IssueDate: getFormattedDate(new Date()),
            ExpireDate: getFormattedDate(new Date()),
            FilePath: "",
        };
    }

    var BusinessLicenseView = Backbone.View.extend({

        template: Template,

        events: {
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            var that = this;
            that.document = options.document;
            egov.request.getBusinessLicense({
                data: {
                    docCopyId: that.document.model.get('DocumentCopyId')
                },
                success: function (result) {
                    if (result) {

                        result.RegisDate = getFormattedDateServer(new Date(result.RegisDate));
                        result.IssueDate = getFormattedDateServer(new Date(result.IssueDate));
                        result.ExpireDate = getFormattedDateServer(new Date(result.ExpireDate));

                        that.model = result;
                        
                        that.model["CitizenName"] = that.document.model.get("CitizenName")
                    } else {
                        that.model = BusinessLicenseModel(that.document.model.get("DocumentCopyId"));
                    }
                    that.render();
                    return that;
                },
                error: function (error) {
                }
            });

        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            var config = that.model;
            this.$el.html($.tmpl(this.template, config));
            var dialogSetting = {
                width: 600,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Giấy phép doanh nghiệp",
                buttons: [
                   {
                       text: "Thêm mới doanh nghiệp",
                       className: "pull-left btn-primary ",
                       click: function () {
                           //that.$el.dialog("hide");
                           var view = new BusinessCreateView({ model: that.document, el: that.$el });
                       }
                   },
                   {
                       text: "Lưu giấy phép",
                       className: "btn-success",
                       click: function () {
                           if (that._isValidate()) {
                               var value = that.$el.find("form").serializeArray();
                               var dataPost = {};
                               for (var i = 0; i < value.length; i++) {
                                   dataPost[value[i].name] = value[i].value;
                               }
                               var data = JSON.stringify(dataPost);
                               egov.request.createLicense({
                                   data: {
                                       docCopyId: that.document.model.get('DocumentCopyId'),
                                       licenseinfo: data
                                   },
                                   success: function (result) {
                                       that.document.renderBusinessLicense();
                                       that.$el.dialog("hide");
                                   },
                                   error: function (error) {
                                   }
                               });
                           }
                       }
                   },
                   {
                       text: egov.resources.common.closeButton,
                       click: function () {
                           that.$el.dialog("hide");
                       }
                   },
                ]
            };
            that.$el.dialog(dialogSetting);
            this._setAttributeForm()

            return that;
        },
        _setAttributeForm: function () {
            var that = this;
            // Upload file đính kèm
      
            that.$el.find('#upload').fileupload({
                dataType: 'json',
                add: function (e, data) {
                    var filename = data.files[0].name;
                    that.$el.find("#LicensePath").text(filename);
                    data.submit();
                },
                done: function (e, data) {
                    var result = data.result[0];
                    var file = {};
                    file[result.key] = result.name;
                    that.$el.find("#FilePath").val(JSON.stringify(file));
                },
                fail: function (e, data) {
                    $("#LicensePath").text("Tải file lỗi");
                }
            });

            // Thời gian
            $.datepicker.regional["vi-VN"] =
              {
                  closeText: "Đóng",
                  prevText: "Trước",
                  nextText: "Sau",
                  currentText: "Hôm nay",
                  monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
                  monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
                  dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
                  dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
                  dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
                  weekHeader: "Tuần",
                  dateFormat: "dd/mm/yy",
                  firstDay: 1,
                  isRTL: false,
                  showMonthAfterYear: false,
                  yearSuffix: ""
              };

            $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
            var dates = that.$el.find("#RegisDate,#IssueDate,#ExpireDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                onSelect: function (selectedDate) {
                    var option = this.id == "RegisDate" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
           
            // Cấu hình lấy dữ liệu tìm kiếm doanh nghiệp
            var setSetting = function (target) {
                /// <summary>
                /// Set settings cho input
                /// </summary>
                /// <param name="target">input target</param>
                return {
                    minLength: 2,
                    source: function (request, response) {
                        $.ajax({
                            url: "/document/FilterCitizen/",
                            data: {
                                name: that.$el.find('#CitizenNameInput').val(),
                                identityCard: "",
                                phone: "",
                                email: "",
                                address: ""
                            },
                            dataType: "json",
                            type: "Get",
                            success: function (data) {
                                response($.map(data, function (obj) {
                                    return {
                                        label: obj.CitizenName,
                                        value: obj.CitizenName,
                                        id: obj.Id,
                                        citizenName: obj.CitizenName,
                                        email: obj.Email,
                                        identityCard: obj.IdentityCard,
                                        phone: obj.Phone,
                                        address: obj.Address
                                    };
                                }));
                            }
                        });
                    },
                    //delay: 300,
                    autoFocus: true,
                    autoSelectFirst: true,
                    select: function (event, ui) {
                        that.$el.find("#CitizenNameHidden").val(ui.item.id);
                        that.$el.find("#CitizenNameInput").val(ui.item.value);
                    }
                };
            };

            // Autocomplete tên doanh nghiệp
            var settings = setSetting("CitizenName");
            that.$el.find('#CitizenNameInput').autocomplete(settings).data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu');
                citizenName = item.citizenName;

                return $("<li>")
                    .data("item.autocomplete", item)
                    .append("<a href='#'>" + citizenName + "</a>")
                    .appendTo(ul);
            };

            var document = that.document.model;
            that.$el.find("#DoctypeNameInput").val(document.get('DocTypeName'));
            that.$el.find("#DoctypeId").val(document.get('DocTypeId'));
        },

        _isValidate: function () {
            var that = this;
            var idCitizen = that.$el.find("#CitizenNameHidden").val();
            if (!idCitizen) {
                that.$el.find("#errorCitizenName").show();
                return false;
            } else {
                that.$el.find("#errorCitizenName").hide();
            }

            var licenseCode = that.$el.find("#LicenseCode").val();
            if (!licenseCode) {
                that.$el.find("#errorLicenseCode").show();
                return false;
            } else {
                that.$el.find("#errorLicenseCode").hide();
            }

            var licenseNumber = that.$el.find("#LicenseNumber").val();
            if (!licenseNumber) {
                that.$el.find("#errorLicenseNumber").show();
                return false;
            } else {
                that.$el.find("#errorLicenseNumber").hide();
            }

            return true;
        }
    });

    function getFormattedDateServer(date) {
        var year = date.getFullYear();

        var month = (1 + date.getMonth()).toString();
        month = month.length > 1 ? month : '0' + month;
        var dateCurrent = date.setHours(date.getHours() + 7)
        var day = new Date(dateCurrent).getDate().toString();
        day = day.length > 1 ? day : '0' + day;

        return day + '/' + month + '/' + year;
    }

    function getFormattedDate(date) {
        var year = date.getFullYear();

        var month = (1 + date.getMonth()).toString();
        month = month.length > 1 ? month : '0' + month;
        var day = date.getDate().toString();
        day = day.length > 1 ? day : '0' + day;

        return day + '/' + month + '/' + year;
    }

    return BusinessLicenseView;
});