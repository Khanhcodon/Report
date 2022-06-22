define([],
    function () {
        "use strict";

        var _dataManager = egov.dataManager;
        var _resource = egov.resources.document.lienthong;

        var LienThong = Backbone.View.extend({

            tagName: "div",
            className: "egov-lienthong",

            initialize: function (options) {
                var that = this;

                that.document = options.document;

                _dataManager.getAllAddress({
                    success: function (result) {
                        that.address = _.filter(result, function (address) {
                            return !String.isNullOrEmpty(address.EdocId);
                        });
                        that.render();
                    }
                });

                return this;
            },

            render: function () {
                var that = this;
                var publisheds = that.document.model.get("LienThongs");
                require([egov.template.document.lienthong], function (LienthongTmp) {
                    $.tmpl(LienthongTmp, { addresses: that.address }).appendTo(that.$el);

                    _.each(publisheds, function (publish) {
                        if (!publish.IsResponsed) {
                            var addressId = publish.AddressId;
                            var $address = that.$("#address" + addressId);
                            if ($address.length > 0) {
                                $address.attr("disabled", "disabled");
                                $address.parent().addClass("disable");
                                $address.next().append("<span> (Đang gửi liên thông)</span>");
                            }
                        }
                    });

                    that.$el.dialog({
                        title: _resource.dialogTitle,
                        width: 450,
                        height: "auto",
                        buttons: [
                            {
                                text: _resource.sendButton,
                                className: "btn-primary",
                                click: function () {
                                    that.$el.dialog("hide");
                                    that.send();
                                }
                            },
                            {
                                text: egov.resources.common.closeButton,
                                click: function () {
                                    that.$el.dialog("hide");
                                }
                            }
                        ]
                    });

                    that.$("#comment").focus();
                    that.$("input.datetime").datepicker({
                        dateFormat: "dd/m/yy"
                    });
                    that.searchAddress();
                });
            },

            searchAddress: function () {
                var that = this;
                var addressItem = $(".address-item");
                require(["vendors"], function () {
                    var $search = that.$("#searchAddress");
                    $search.keyup(function (e) {
                        var keySearch = $search.val();
                        if (keySearch == "") {
                            addressItem.show();
                        }
                        var address = addressItem.each(function (u) {
                            var $item = $(this);
                            var addr = egov.utilities.string.stripVietnameseChars($item.data("name")).toUpperCase();
                            var searchAdr = egov.utilities.string.stripVietnameseChars(keySearch).toUpperCase();
                            if (addr.indexOf(searchAdr) > -1) {
                                $item.show();
                            } else {
                                $item.hide();
                            }
                        });
                    })
                })
            },

            send: function () {
                var addressIds = [];
                this.$(".address-list :checked").each(function () {
                    addressIds.push(parseInt($(this).val()));
                });

                if (addressIds.length === 0) {
                    alert(_resource.noAddressChoised);
                    return;
                }

                var comment = this.$("#comment").val();
                var dateAppointed = this.$("input.datetime").val();
                var date;
                if (!String.isNullOrEmpty(dateAppointed)) {
                    date = Globalize.parseDate(dateAppointed);
                }
                this.document.transferLienThong(comment, addressIds, date);
            }
        });

        return LienThong;
    }
);