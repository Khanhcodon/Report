// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function (btalk) {
    'use strict';

    btalk.view = btalk.view || {};

    if (btalk.view.ready === true) {
        return;
    }

    btalk.view.ready = true;

    // Cho phep tu view nay goi function cua view khac. Xem tai:
    // https://lostechies.com/derickbailey/2011/07/19/references-routing-and-the-event-aggregator-coordinating-views-in-backbone-js/
    // http://stackoverflow.com/questions/7708195/access-function-in-one-view-from-another-in-backbone-js
    // Su dung cho btalk.view.message.js goi scrollBottom trong btalk.view.app.js
    Backbone.View.prototype.shareEvents = _.extend({}, Backbone.Events);

    btalk.view = {
        options: {
            AVATAR_URL: "",
            PATTERN: /\{(\d+)\}/g
        },

        init: function (options) {
            this.options = $.extend({}, this.options, options);
        },

        getAvatar: function (avatarInfoArr, targetSelector) {
            if ($.isArray(avatarInfoArr) != true) {
                console.log('btalk.view.getAvatar: param accounts khong phai la Array');
                return "";
            }

            var avatarUrl = this.options.AVATAR_URL;
            var pattern = this.options.PATTERN;
            var imgSrc = avatarUrl.replace(pattern, function (match, key, value) {
                return avatarInfoArr[key] || "";
            });

            return imgSrc;
        },

        setAvatar: function (avatarInfoArr, $target) {
            //var imgSrc = this.getAvatar(avatarInfoArr);

            //if ($target) {
            //    $target
            //    .on("error", function () {
            //        $(this).attr("src", "../../../themes/default/images/noavatar.jpg");
            //    })
            //    .attr('src', imgSrc);
            //}
        },

        //  Hien thi nhieu anh da chia se có trong bo nho da tai ve.
        previewSharedLocalImage: function (srcImgSelected) {
            //this.ChatterInfoView.prototype.downloadMoreSharedFile();
            $('.previewimage .list-image').html('');
            // Added gan trang thai la dang xem preview anh.
            btalk.STATUS_PREVIEWIMAGE = true;
            //var listImage = $('#sharedimage .item-image');
            var listFile = btalk.APPVIEW.CURRENTCHATTER.get('collectionShareImage');

            listFile = _.sortBy(listFile, function (item) {
                return parseInt(item.object);
            });
            listFile = listFile.reverse();
            for (var k = 0; k < listFile.length; k++) {
                this.displayFooterImage(listFile[k].url, srcImgSelected);
            }

            if (listFile.length > 1) {
                this.showCompomentPreviewImage();
            } else {
                this.hiddenCompomentPreviewImage();
            }
        },

        // Vẽ thêm 1 ảnh ở phía dưới ảnh chính.
        displayFooterImage: function (_src, srcImgSelected) {
            var viewTemplate;
            if (srcImgSelected == _src) {
                viewTemplate = "<li class='item-image selected' onclick='btalk.view.selectedImage(this)'><img onload='' type='image/png' src='" + _src + "' /></li>";
            } else {
                viewTemplate = "<li class='item-image' onclick='btalk.view.selectedImage(this)'><img onload='' type='image/png' src='" + _src + "' /></li>";
            }
            $('.previewimage .list-image').append(viewTemplate);
        },

        // Vẽ ảnh chính khi preview ảnh, tự auto srcoll đến vị tương ứng.
        showSelectedImage: function (srcImage) {
            var previewImage = $('.previewimage');
            previewImage.find('.list-image li').removeClass('selected');
            previewImage.find('#img01').attr('src', srcImage);
            previewImage.find('.dowload a').attr('href', srcImage);
            previewImage.find('.list-image li img[src="' + srcImage + '"]').parents('li').addClass('selected');

            this.autoScrollImageFooter();
        },

        // ẩn danh các phần liên quan đến preview (các ảnh trong khác, các nút chuyển ảnh).
        hiddenCompomentPreviewImage: function () {
            $('.content-left img').hide();
            $('.content-right img ').hide();
            $('.modal-footer').hide();
        },

        // hien thi danh các phần liên quan đến preview (các ảnh trong khác, các nút chuyển ảnh).
        showCompomentPreviewImage: function () {
            $('.content-left img').show();
            $('.content-right img').show();
            $('.modal-footer').show();
        },

        // Chon xem anh o phan
        selectedImage: function (e) {
            var srcImage = $(e).children('img').attr('src');
            this.showSelectedImage(srcImage);
            this.autoScrollImageFooter();
        },

        // Lui lai anh vua xem.
        previousImage: function () {
            var preview = $('.previewimage .list-image');
            var imageNext = preview.find('.selected').next();

            var _srcImage = null;
            // neu khong ton tai anh tiep den theo thi chuyen ve vi tri dau tien.
            if (imageNext.length > 0) {
                _srcImage = imageNext.find('img').attr('src');
            } else {
                var listImage = preview.find('li');
                var imagePrev = listImage[0];
                _srcImage = $(imagePrev).find('img').attr('src');
            }
            this.showSelectedImage(_srcImage);

            // set position
            this.autoScrollImageFooter();
        },

        // Chuyen anh tiep theo
        nextImage: function () {
            var preview = $('.previewimage .list-image');
            var imagePrev = preview.find('.selected').prev();

            var _srcImage = null;
            if (imagePrev.length > 0) {
                _srcImage = imagePrev.find('img').attr('src');
            } else {
                var listImage = preview.find('li');
                var imagePrev = listImage[listImage.length - 1];
                _srcImage = $(imagePrev).find('img').attr('src');
            }
            this.showSelectedImage(_srcImage);

            // set position
            this.autoScrollImageFooter();
        },

        // Kiem tra viec dich chuyen cua thanh footer trong viec xem anh.
        autoScrollImageFooter: function () {
            var outter = $('.previewimage .modal-footer ul.list-image');
            //var target = $('.previewimage .modal-footer ul.list-image li.selected');
            var target = outter.find('li.selected');
            var x = outter.width();

            if (target.length < 1) {
                return;
            }

            var offsetX = target[0].offsetLeft;
            var scrollLeft;
            if (offsetX > (x / 2 + target.width())) {
                scrollLeft = offsetX - (x - target.width()) / 2;
            } else {
                scrollLeft = 0;
            }
            outter.animate({
                "scrollLeft": scrollLeft
            }, 10)
        }
    };
})(window.btalk = window.btalk || {});