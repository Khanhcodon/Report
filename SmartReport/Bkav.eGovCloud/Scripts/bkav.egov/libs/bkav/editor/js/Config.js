function Config() {

}
Config.viEnabled = true;
Config.useVietKey = true;
Config.URL_IMAGE = "/Scripts/bkav.egov/libs/bkav/editor/";
Config.Image = {
    pic: 'Pic.png',
    I: 'I.png',
    Ip: 'I-p.png',
    B: 'B.png',
    Bp: 'B-p.png',
    U: 'U.png',
    Up: 'U-p.png',
    vietkey: 'vietkey.png',
    justifyleft: 'justifyleft.png',
    justifycenter: 'justifycenter.png',
    justifyright: 'justifyright.png',
    justifyfull: 'justifyfull.png',
    numberedlist: 'numberedlist.png',
    More: 'More.png',
    MoreP: 'More-p.png',
    strikeThrough: 'strikeThrough.png',
    engkey: 'engkey.png',
    format: 'format.png',

    print: 'print.png',
    attachIcon: 'attachIcon.png',
    emoticon: 'emoticon.png',
    textColor: 'text-color.png',
    bgcolor: 'bgcolor.png',
    dottedlist: 'dottedlist.png',
    outdent: 'outdent.png',
    indent: 'indent.png',
    insertLink: 'insertLink.png',
    table: 'table.png',
    fontColor: 'fontColor.png',

};

$.each(Config.Image, function (i, item) {
    Config.Image[i] = Config.URL_IMAGE + "icons/" + item;
});