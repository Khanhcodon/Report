$(document).ready(function () {
    //khoi tao chieu rong chieu cao cua cac thuoc tinh
    window.onload = function () {
        WhenBrowserResize();
        //alert(getViewportWidth() + "");
        $('#hiddenMulti').focus();

        // Gioi han so chu trong Danh sach cac thu tuc thong thuong
        var limitChar = 100;
        var numberChar = 0;
        var textChar;
        var newChar;
        $('div.itmUsual h4').each(function () {
            numberChar = $(this).html().length;
            if (numberChar > limitChar) {
                textChar = $(this).html();
                newChar = textChar.substr(0, limitChar);
                $(this).html(newChar + '...');
            }
        });
    }

    function WhenBrowserResize() {
        // height = chieu cao cua man hinh hien thi (da tru di footer va header)
        var height = getViewportHeight();
        if (height > 0) {
            //document.getElementById("content-wrap").style.height = height + "px";      
            //document.getElementById("details-wrap").style.height = height - 160 + "px";
            //document.getElementById("details").style.height = height - 160 + "px";
            //document.getElementById("usual-list").style.height = height - 262 + "px";
            //document.getElementById("usual-list").style.height = height - 262 + "px";
            //$('body').css({ 'height': getBodyHeight() + '' });
            // cuongnt comment
            //document.getElementById("right-panel").style.width = (getViewportWidth() - 800) + "px";
        }
    }

    //lay chieu cao thuc cua man hinh hien thi (ko phai do phan giai cua may)
    //da tru di do cao cua footer va header
    function getViewportHeight() {
        var h = 0;
        if (self.innerHeight)
            h = window.innerHeight;
        else if (document.documentElement && document.documentElement.clientHeight)
            h = document.documentElement.clientHeight;
        else if (document.body)
            h = document.body.clientHeight;
        return h - 65 - 7;
    }

    //lay chieu rong cua man hinh hien thi
    function getViewportWidth() {
        var w = 0;
        if (self.innerWidth)
            w = window.innerWidth;
        else if (document.documentElement && document.documentElement.clientWidth)
            w = document.documentElement.clientWidth;
        else if (document.body)
            w = document.body.clientWidth;
        return w;
    }

    function getBodyHeight() {
        var h = 0;
        if (self.innerHeight)
            h = window.innerHeight;
        else if (document.documentElement && document.documentElement.clientHeight)
            h = document.documentElement.clientHeight;
        else if (document.body)
            h = document.body.clientHeight;
        return h;
    }

    //su kien khi window resize
    $(window).bind('resize', function () {
        WhenBrowserResize();
    });
});