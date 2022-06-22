
/// <summary>Thư viện tự động xử lý tooltip, các alert thông báo, menu xổ xuống</summary>
/// Author: TienBV@bkav.com
/// DateCreated: 24/12/2013
/// Requires:
///     - jquery 1.6 + download tại http://jquery.com/download/
///     - qtip2 download tại http://qtip2.com/

(function () {

        "use strict";

        //#region Variables

        var _tooltipTarget = ".qtooltip",
            _notifierTarget = ".edropdown",
            _shortKeyHelper = ".shortkey";

        //#endregion

        //#region Functions

        var eGovTip = function () {
        };

        eGovTip.prototype.initShortKeyHelper = function () {
            _initShorkeyHelper();
        }

        eGovTip.prototype.bind = function (objs) {
            if (!(objs instanceof jQuery)) {
                objs = $(objs);
            }
            objs.each(function (idx, obj) {
                obj = $(obj);
                if (obj.is(_tooltipTarget)) {
                    _initTooltip(obj);
                }
                else if (obj.is(_notifierTarget)) {
                    _initNotifier(obj);
                }
                else {
                    var option = _getDefaultSetting();
                    option.position = _getTooltipPosition($(obj));
                    obj.qtip(option);
                }
            });
        }

        eGovTip.prototype.destroy = function (obj) {
            obj.qtip('destroy');
        }

        //#endregion

        //#region Private Functions

        /// <summary>Hiển thị tooltips.</summary>
        var _initTooltip = function (obj) {
            var $this = $(obj);
            var option = _getDefaultSetting();
            option.events = {
                show: function (event, api) {
                    var $element = $this.data('qtip').elements.target;
                    var position = _getTooltipPosition($this);
                    $element.qtip('option', { 'position.at': position.at, 'position.my': position.my });
                }
            };
            $this.qtip(option);
        };

        /// <summary>Trả về tooltip option tương ứng với vị trí của target</summary>
        /// <param name="$this" type="object">target</param>
        var _getTooltipPosition = function ($this) {
            var lof = $this.offset().left;
            var tof = $this.offset().top;
            var h = $this.height();
            var w = $this.width();
            var tipWidth = $this.attr("etip-width");

            var myT = "top", myL = "center", atT = "bottom", atL = "center"; // mặc định hiển phía dưới căn giữa.

            if (lof < 20) // bên trái
            {
                myL = "left";
                atL = "left";
            }
            if ((lof + w) > window.innerWidth - tipWidth / 2 || (lof + w) > window.innerWidth - 50) { //bên phải
                myL = "right";
                atL = "right";
            }
            if (tof < 20) { // góc trên
                atT = "bottom";
                myT = "top";
            }
            if ((tof + h) > window.innerHeight - 20) { // góc dưới
                myT = "bottom";
                atT = "top";
            }
            var result = {
                my: myT + myL,
                at: atT + atL
            };
            return result;
        };

        var _initNotifier = function (obj) {
            var $this = $(obj);
            var contentId = $this.attr("content-id");
            var contentUrl = $this.attr("data-url");
            var isSmallTip = $this.attr("show-tip") == "true";
            var position = _getTooltipPosition($this);
            position.adjust = {
                y: (isSmallTip ? 5 : 13)
            }
            var tipWidth = $this.attr("etip-width");
            var tipHeight = $this.attr("etip-height");
            $this.qtip({
                content:
                {
                    text: function (event, api) {
                        if (contentId != undefined && contentId !== '') {
                            if (contentId.indexOf("#") < 0) {
                                contentId = "#" + contentId;
                            }
                            return $(contentId);
                        }
                        else {
                            $.ajax({
                                url: contentUrl,
                                type: 'POST'
                            })
                            .then(function (content) {
                                api.set('content.text', content);
                            }, function (xhr, status, error) {
                                api.set('content.text', status + ":" + error);
                            });
                        }
                    }
                },
                //position: position,
                events: {
                    show: function (event, api) {
                        var $element = $this.data('qtip').elements.target;
                        var position = _getTooltipPosition($this);
                        position.adjust = {
                            y: (isSmallTip ? 5 : 13)
                        }
                        $element.qtip('option', { 'position.at': position.at, 'position.my': position.my, 'position.adjust.y': position.adjust.y });
                    }
                },
                show: { event: 'click', fixed: true, effect: false, delay: 0 },
                hide: { event: 'click unfocus', fixed: true, effect: false, delay: 0 },
                style: { classes: 'qtip-light', tip: isSmallTip, width: tipWidth, height: tipHeight }
            });
        };

        var _initShorkeyHelper = function () {
            var option = _getDefaultSetting();
            option.style = {
                tip: false,
                classes: 'shortkey-helper'
            };
            option.position = {
                my: "bottom left",
                at: "center right"
            };
            option.show = false;
            $(_shortKeyHelper).qtip(option);
        };

        var _getDefaultSetting = function () {
            return {
                style: {
                    tip: false,
                    classes: 'qtip-dark tooltip'    // qtip-light, qtip-cream, qtip-red, qtip-green, qtip-blue, qtip-rounded, qtip-bootstrap, qtip-tipped,  ...
                },
                position: {                 // Hiển thị ngay phía dưới và căn giữa target
                    my: "top center",
                    at: "bottom center",
                    adjust: {
                        y: 10
                    }
                }
            };
        };

        //#endregion

        return eGovTip;

    })();