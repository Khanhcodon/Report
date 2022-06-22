define([egov.template.invoice.importinvoice ], function (importTemplate) {
    var InvoiceView = Backbone.View.extend({

        template: importTemplate,

        tagName: "p",

        events: {
            "click .add-product": "addProduct"
        },

        initialize: function (options) {
            this.document = options.document;
            var modelDocument = this.document.model;
            this.model = new InvoiceModel({
                key: modelDocument.get("DocCode"),
                CusName: modelDocument.get("CitizenName"),
                CusPhone: modelDocument.get("Phone")
            });
            this.listenTo(this.model, 'change:Amount', this._renderAmount);
            this.render();
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            var config = {};

            that.$el.html($.tmpl(this.template, this.model.toJSON()));
            var dialogSetting = {
                width: 600,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Tạo biên lai",
                buttons: [
                      {
                          text: "Tạo mới",
                          className: "btn-success",
                          click: function () {
                              that._success()
                          }
                      },
                       {
                           text: egov.resources.common.closeButton,
                           click: function () {
                               that.$el.dialog("hide");
                               var document = that.document
                           }
                       },
                ]
            };

            that.$el.dialog(dialogSetting);
            that._setAttributeForm();
            that.addProduct();
            that.addProduct();
            return that;
        },

        addProduct: function (e) {
            that = this;
            var template = getTemplateProduct();
            var product = new ProductView({ invoice: that });
            that.$el.find("#formProducts").append(product.render().$el);
        },

        _getTotalAmount: function myfunction() {
            var $prices = this.$el.find(".product-price");
            var total = 0;
            _.each($prices, function ($price) {
                var price = $($price).attr("data-price");
                total = total + Number(price);
            })
            var text = DOCSO.doc(total) + " đồng";
           
            this.model.set("AmountsText", formatMoney(total, 0));
            this.model.set("AmountInWords", text);
            that.model.set("Amount", total);
        },

        _setAttributeForm: function () {
            var that = this;
            var $amount = that.$el.find(".amount");
            $amount.keyup(function (e) {
                var val = $(this).val();
                var num = money2Number(val);
                that.model.set("AmountsText", formatMoney(num, 0));
                var text = DOCSO.doc(num) + " đồng";
                that.model.set("AmountInWords", text);
                that.model.set("Amount", num);
                that.model.set("Total", num);
            })
        },

        _seriabbleForm: function () {
            var that = this;
            var form = that.$el.find("#formImportInvoice").serializeArray();
            var products = [];
            for (var i = 0; i < form.length; i++) {
                if (form[i].name == "proName") {
                    if (form[i].name) {
                    }
                } else {
                    that.model.set(form[i].name, form[i].value);
                }
            }

           var productels = that.$el.find("#formProducts").find(".row");
           _.each(productels, function (product) {
               var $product = $(product);
               if ($product.find(".product-name").val()) {
                   products.push({
                       Code: "",
                       ProdName: $product.find(".product-name").val(),
                       ProdUnit: "",
                       ProdQuantity: "",
                       ProdPrice: "",
                       Amount: money2Number($product.find(".product-price").val()),
                       Total: "",
                   })
               }
           })
            that.model.set("Products", products);
        },

        _success: function () {
            var that = this;
            that._seriabbleForm();

            egov.request.createInvoice({
                data: {
                    strInvoice: JSON.stringify(that.model.toJSON())
                },
                success: function (result) {
                    that.$el.dialog("hide");
                    if (result.data.indexOf("OK") > -1) {
                        egov.pubsub.publish(egov.events.status.success, 'Tạo mới biên lai thành công');
                        that.document.renderInvoice();
                    } else {
                        egov.pubsub.publish(egov.events.status.error, 'Có lỗi trong quá trình tạo biên lai');
                    }
                },
                error: function (error) {
                    egov.pubsub.publish(egov.events.status.error, 'Có lỗi trong quá trình tạo biên lai');
                }
            });
        },

        _renderAmount: function () {
            var that = this;
            var $amount = that.$el.find(".amount");
            var $amountInWords = that.$el.find(".amountinwords");
            $amount.val(that.model.get("AmountsText"))
            $amountInWords.val(that.model.get("AmountInWords"))
        }
    });

    var ProductView = Backbone.View.extend({

        template: '<label class="col-md-4" data-res=""></label>\
                <div class="col-md-12">\
               <div class="row">\
                   <div class="col-md-7">\
                       <input type="text" class="form-control product-name" name="" value="" />\
                   </div>\
                   <div class="col-md-7">\
                       <input type="text" class="form-control product-price" name="" value="" data-price=""  />\
                   </div>\
                   <div style="color:red" class="col-md-2"><i class="icon icon-close"></i></div>\
               </div>\
            </div>',

        tagName: "div",

        events: {
            "click .icon-close": "removeProduct",
            "keyup .product-price": "price"
        },

        removeProduct: function () {
            var that = this;
            that.$el.remove();
        },

        price: function (e) {
            var $target = $(e.target).closest(".product-price");
            var value = $target.val();
            var num = money2Number(value);
            $target.val(formatMoney(num, 0));
            $target.attr("data-price", num);
            this.invoice._getTotalAmount();
        },

        initialize: function (options) {
            this.invoice = options.invoice;
            that.$el.addClass("row-product")
        },

        render: function () {
            var that = this;
            that.$el.html(that.template)
            return that;
        },
    });

    var InvoiceModel = Backbone.Model.extend({
        defaults: {
            key: "",
            CusCode: "KH000001",
            ArisingDate: "",
            CusName: "",
            CusPhone: "",
            CusTaxCode: "",
            Total: "0",
            Type: "",
            KindOfService: "",
            Amount: "",
            AmountInWords: "",
            VATAmount: "",
            VATRate: "",
            CusAddress: "",
            PaymentMethod: "TM",
            Extra: "",
            Products: "",
            ProductJson: "",
        }
    });

    var ProductModel = Backbone.Model.extend({
        defaults: {
            Code: "",
            ProdName: "",
            ProdUnit: "",
            ProdQuantity: "",
            ProdPrice: "",
            ProdPriceText: "",
            Amount: "",
            Total: "0",
            Type: ""
        }
    });

    var DOCSO = function () { var t = ["không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"], r = function (r, n) { var o = "", a = Math.floor(r / 10), e = r % 10; return a > 1 ? (o = " " + t[a] + " mươi", 1 == e && (o += " mốt")) : 1 == a ? (o = " mười", 1 == e && (o += " một")) : n && e > 0 && (o = " lẻ"), 5 == e && a >= 1 ? o += " lăm" : 4 == e && a >= 1 ? o += " tư" : (e > 1 || 1 == e && 0 == a) && (o += " " + t[e]), o }, n = function (n, o) { var a = "", e = Math.floor(n / 100), n = n % 100; return o || e > 0 ? (a = " " + t[e] + " trăm", a += r(n, !0)) : a = r(n, !1), a }, o = function (t, r) { var o = "", a = Math.floor(t / 1e6), t = t % 1e6; a > 0 && (o = n(a, r) + " triệu", r = !0); var e = Math.floor(t / 1e3), t = t % 1e3; return e > 0 && (o += n(e, r) + " ngàn", r = !0), t > 0 && (o += n(t, r)), o }; return { doc: function (r) { if (0 == r) return t[0]; var n = "", a = ""; do ty = r % 1e9, r = Math.floor(r / 1e9), n = r > 0 ? o(ty, !0) + a + n : o(ty, !1) + a + n, a = " tỷ"; while (r > 0); return n.trim() } } }();
    function formatMoney(n, c, d, t) {
        var c = isNaN(c = Math.abs(c)) ? 2 : c,
          d = d == undefined ? "," : d,
          t = t == undefined ? "." : t,
          s = n < 0 ? "-" : "",
          i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
          j = (j = i.length) > 3 ? j % 3 : 0;

        return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    };

    function money2Number(money) {
        var num = money.split(".").join("");
        return num
    }

    function getTemplateProduct() {
        return '<label class="col-md-4" data-res=""></label>\
            <div class="col-md-12">\
               <div class="row">\
                   <div class="col-md-7">\
                       <input type="text" class="form-control" name="name" value="" />\
                   </div>\
                   <div class="col-md-7">\
                       <input type="text" class="form-control" name="name" value="" />\
                   </div>\
                   <div class="col-md-2"><i class="icon icon-remove"></i></div>\
               </div>\
        </div>';
    }

    return InvoiceView;
});