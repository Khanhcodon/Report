// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

define(['text!conversationdayT', 'text!memberT', 'text!messageT', 'text!messagenextT', 'text!attachmentT',
		'../btalk/btalk.connectionManager', 'jquery.btalk', './btalk.view',
		'../models/btalk.model.message'],
      function (conversationdayT, memberT, messageT, messagenextT, attachmentT) {
          'use strict';

          btalk.view = btalk.view || {};
          if (btalk.view.message === true) {
              return;
          }

          btalk.view.message = true;
          var ConversationDayView = Backbone.View.extend({
              // DOM chua danh sach chatter trong ngay hien tai
              $elUsers: null,
              events: {},
              initialize: function () {
                  this.listenTo(this.model.members, 'add', this.addUserOne);
              },

              render: function () {
                  this.$el.html($.tmpl(conversationdayT, this.model.toJSON()));
                  this.$elUsers = this.$el.find('.chat-detail-users');

                  // CuongNT - 11/4/2016: Fix loi boi chon noi dung chat kho tren firefox.
                  if (btalk.browser.browserName == 'Firefox') {
                      // Firefox phai enable toan bo vung cha ma trong do co chua text thi boi chon moi thuan loi.
                      // Chrome thi nguoc lai, chi can boi chon dung vung co noi dung text.
                      this.$el.find('.chat-detail-users').addClass("enableSelectText");
                  }
                  return this;
              },

              addUserOne: function (user) {
                  var hisuser = new btalk.view.MemberView({ model: user });
                  if (user.get('append') === true) {
                      this.$elUsers.append(hisuser.render().el);
                  } else {
                      this.$elUsers.prepend(hisuser.render().el);
                      this.shareEvents.trigger("preventScrolling");
                  }

                  if (user.attributes.rows && user.attributes.rows.length > 0) {
                      for (var i = 0; i < user.attributes.rows.length; i++) {
                          hisuser.model.messages.add(user.attributes.rows[i]);
                      }
                  }
              }
          });

          var MemberView = Backbone.View.extend({
              // DOM chua danh sach tin nhan cua chatter hien tai
              $elMessages: null,
              events: {},

              initialize: function () {
                  this.listenTo(this.model, 'change:timestamp', this.changeTimestamp);
                  this.listenTo(this.model.messages, 'add', this.addMessageOne);
              },

              render: function () {
                  var imgSrc = btalk.view.getAvatar([this.model.get("account")]);
                  this.$el.html($.tmpl(memberT, this.model.toJSON()));
                  this.$elMessages = this.$el.find('.chat-detail-messages');

                  this.$el.find('.chat-detail-row-timestamp')
                      .addClass('disableSelectText')
                      .removeClass('enableSelectText')
                      .text(btalk.getCoolTime2(this.model.get('timestamp')));

                  this.$el.find("img.user")
                  .on("error", function () {
                      $(this).attr("src", "../../../themes/default/images/noavatar.jpg");
                  })
                  .attr('src', imgSrc);

                  //Added DamBV
                  this.$el.find("img.avatar-extra").on("error", function () {
                      $(this).attr("src", "../../../themes/default/images/noavatar.jpg");
                  })
                  .attr('src', imgSrc);
                  return this;
              },

              addMessageOne: function (msg) {
                  var hismsg = new btalk.view.MessagesView({ model: msg });
                  if (msg.get('append') === true) {
                      // TODO: Xu ly chen message theo thu tu thoi gian
                      this.$elMessages.append(hismsg.render().el);
                  } else {
                      this.$elMessages.prepend(hismsg.render().el);
                      this.shareEvents.trigger("preventScrolling");
                  }
              },

              changeTimestamp: function () {
                  this.$el.find('.chat-detail-row-timestamp').text(btalk.getCoolTime2(this.model.get('timestamp')));
              }
          });

          var MessagesView = Backbone.View.extend({
              $elAttachmentImg: null,
              $elAttachmentOther: null,
              elRow: '<div class="_row"></div>',
              className: "chat-detail-line",
              events: {
                  'click .enableSelectText a': 'openTabWithLink',
                  'click ._cell': 'previewImageInConversation',
                  'dblclick .chat-detail-row-message': 'createMessageReply',
                  'copy .enableSelectText': 'copyMessage',
                  'mousedown .chat-detail-row-message': 'disableSelectText',
              },

              initialize: function () {
                  // [TODO] can sua lai cho nay, viet ham rieng cho state
                  this.listenTo(this.model, 'change:status', this.changeStatus);
                  this.listenTo(this.model, 'change:unread', this.changeUnread);

                  /* TamDN - 4/7/2017
                  Khi danh sách đã xem thay đổi, in lại avatar các tài khoản đã xem */
                  this.listenTo(this.model, 'change:viewedBy', this.printAvatarSeen);

                  // TODO: Chi khoi tao khi message nay la message chua danh sach file gui nhan
                  // this.model.attachments = new btalk.AttachmentList;
                  if (this.model.attachments) {
                      this.listenTo(this.model.attachments, 'add', this.addAttachmentOne);
                  }
              },

              render: function () {

                  var newmsg = this.model.get('message');
                  this.model.set({ 'message': newmsg });
                  this.$el.html($.tmpl(messageT, this.model.toJSON()));
                  this.$elAttachmentImg = this.$el.find('div[data-field="attachments-image"]');
                  this.$elAttachmentOther = this.$el.find('div[data-field="attachments-other"]');
                  // Cap nhat status
                  // [TODO] giu ham nay thi bo dieu kien lien quan trong template,
                  // nguoc lai thi bo sung account vao model message
                  this.changeStatus();

                  // Danh dau cac dong message chua doc --> tuc dong tro ve binh thuong sau 3 giay
                  // Chi tu dong ve da xem khi dang forcus cua so chat
                  var that = this;
                  if (this.model.get('unread') == true && btalk.WINDOWFOCUS) {
                      setTimeout(function () {
                          that.model.set({ 'unread': false });
                      }, 5000);
                  }

                  //In avatar các tài khoản đã xem
                  this.printAvatarSeen();
                  return this;
              },
              /*'client': 0,
                  'server': 1,
                  'success': 2,
                  'viewed': 3,*/
              statusMsgs: {
                  "client": "Chưa gửi",
                  "server": "Đã gửi",
                  "success": "Đã nhận",
                  "viewed": "Đã xem"
              },

              changeStatus: function () {
                  if (this.model.get('status') != null && this.model.get('status').length > 0) {
                      this.$el.find("div[data-field='status']").removeClass()
                          .addClass('statusofmessage')
                          .addClass('_' + this.model.get('status'))
                          .attr("title", this.statusMsgs[this.model.get('status')]);
                  } else {
                      this.$el.find("div[data-field='status']").removeClass();
                  }
              },

              changeUnread: function () {
                  // TODO: Hien tai dang co van de sau:
                  // 1. Click vao chatter -> hien tin chua doc -> Cho trong 10s thi thanh da doc
                  // 2. Click focus -> Cac tin chua doc cua chatter hien tai chuyen danh thanh da doc luon, khong cho 10s.
                  // => Da xu ly khi goi:

                  // Chuyen ve trang thai tin da doc
                  if (this.model.get('unread') == false) {
                      this.$el.find('.chat-detail-row-message').animate({
                          backgroundColor: "#E4E4E4"
                      }, 1000);
                  } else if (this.model.get('unread') == true && btalk.WINDOWFOCUS) {
                      var that = this;
                      setTimeout(function () {
                          that.model.set({ 'unread': false });
                      }, 7000);
                  }
              },

              addAttachmentOne: function (file) {
                  // Chi hien preview voi cac file da gui nhan thanh cong
                  if (file.get('type').indexOf('image') >= 0 && file.get('percentage') >= 0) {
                      var attach = new btalk.view.AttachmentView({ model: file });
                      var $lastrow = this.$elAttachmentImg.find("._row:last");
                      if ($lastrow.length <= 0) {
                          this.$elAttachmentImg.append(this.elRow);
                      }
                      if ($lastrow.find('._img').length < this.model.get('imageColumn')) {
                          // Chen tiep vao row hien tai
                          this.$elAttachmentImg.find("._row:last").append(attach.render().el);
                      } else {
                          // Tao row moi va chen vao row moi
                          this.$elAttachmentImg.append(this.elRow);
                          this.$elAttachmentImg.find("._row:last").append(attach.render().el);
                      }
                  } else {
                      var attach = new btalk.view.AttachmentView({ model: file });
                      this.$elAttachmentOther.append(attach.render().el);
                  }

                  // Chi khi ve len phia tren moi can giu nguyen vi tri scroll
                  if (this.model.get("append") != true) {
                      this.shareEvents.trigger("preventScrolling");
                  }
              },

              openTabWithLink: function (e) {
                  var url = e.target.href;
                  var win = window.open(url, '_blank');
                  win.focus();
                  e.stopPropagation();
                  e.preventDefault();
              },

              // Added DamBV - 18/02/2017: update lam lai phan xem anh trong cuoc hoi thoai.
              previewImageInConversation: function (e) {
                  e.preventDefault();
                  e.stopPropagation();
                  var srcImgSelected = $(e.target).attr('href');
                  btalk.view.previewSharedLocalImage(srcImgSelected);
                  $('#modalpreviewimage').show();
                  btalk.view.showSelectedImage(srcImgSelected);
                  btalk.view.ChatterInfoView.prototype.downloadMoreSharedFile();
              },

              // Update DamBV - 20/03/2017:Trả lời tin nhắn bằng viec dbclick trong.
              createMessageReply: function (e) {
                  // Kiem tra so luong click la nho hon 3, thoa man dblclick.
                  if (e.detail < 3) {
                      var _quote = this.createQuote();
                      btalk.temporaryQuote = _quote;
                      // Gan doi tuong tra loi tin nhan vao voi chatter hien tai (khi chuyen chatter thi se duoc ve lai).
                      btalk.APPVIEW.CURRENTCHATTER.temporaryQuote = _quote;
                      btalk.temporaryQuote.showQuote();
                  }
              },

              // Added DamBV: Dblcick thi khong cho phep select text trong message
              disableSelectText: function (e) {
                  if (e.detail == 2) {
                      e.preventDefault();
                  }
              },

              // Added DamBV - 01/03/2017: Lưữ dữ liệu khi copy vào biến tạm thời để tạo quote.
              copyMessage: function (e) {
                  var _contentMsg = btalk.APPVIEW.replaceEmotionByText(btalk.getSelectionHtml());
                  if (btalk.CAN_QUOTE) {
                      var _quote = this.createQuote(_contentMsg);
                      btalk.temporaryQuote = _quote;
                  }
                  // Gan du lieu de chuyen di.
                  e.originalEvent.clipboardData.setData('text/plain', _contentMsg);
                  e.preventDefault();
              },

              // Added DamBV 02/07/2017: Tao quote
              createQuote: function (contentmsg) {
                  var _senderQuote = this.model.get('senderJid');             // Nguoi chat
                  var _contentQuote = this.model.get('message');             // Noi dung
                  if (contentmsg) {
                      _contentQuote = contentmsg;
                  }
                  var _timeQuote = this.model.get('timestamp').valueOf();   // Thoi gian
                  var quote = new btalk.QuoteObject(_senderQuote, _timeQuote, _contentQuote);
                  return quote;
              },

              /*
               * Update DamBV - 20/03/2017: Thuc hien ve cac avatart cua nguoi da xem tin nhan trong chat nhom.
               * Khi danh sách đã xem thay đổi, thực hiện lại việc in avatar các tài khoản đã xem
               */
              printAvatarSeen: function () {
                  var memberViewed = this.model.get('viewedBy');
                  if (this.model.get('senderJid') != btalk.auth.getJID()) {
                      this.$el.find('#memberviewed').removeClass('memberViewed').addClass('memberViewedOther');
                  }

                  this.$el.find("#memberviewed").html('');

                  var _scroll = btalk.view.IndexView.prototype.scrollBottom();

                  for (var i = 0; i < memberViewed.length; i++) {
                      var account = memberViewed[i].split('@')[0];
                      var htmlAvatarSeen = "<li class='disableSelectText' style='float:right;padding-left:2px;' account='" + account + "'>"
                                          + "<img style='width: 17px !important;'class='img-circle disableSelectText' title='" + account + "'/></li>";
                      this.$el.find("#memberviewed").append(htmlAvatarSeen);
                      btalk.view.setAvatar([account], this.$el.find('#memberviewed img[title ="' + account + '"]'));
                  }

                  // Khong scroll khi dang doc lich su chat.
                  if (_scroll <= 5) {
                      btalk.view.IndexView.prototype.scrollBottom(0);
                  }
              },
          });

          var AttachmentView = Backbone.View.extend({
              //className: "_cell",
              events: {
                  "click .not_connected": "reconnect"
              },

              initialize: function () {
                  this.listenTo(this.model, 'change:percentage', this.changePercentage);
                  this.listenTo(this.model, 'change:url', this.changeUrl);
              },

              render: function () {
                  this.$el.html($.tmpl(attachmentT, this.model.toJSON()));
                  if (this.model.get('type').indexOf('image') >= 0 && this.model.get('percentage') >= 0) {
                      this.$el.addClass("_cell");
                      if (this.model.get('percentage') == 101) {
                          /*
                           * TamDN - Da them filename trong ham getUrl
                           */
                          //this.$el.attr("style", "background-image: url('" + this.model.get('url') + "?filename=" + this.model.get('name') + "&inline&height=250'); background-size: cover; background-repeat: no-repeat; background-position: 50% 50%;");
                          this.$el.attr("style", "background-image: url('" + this.model.get('url') + "&inline&height=250'); background-size: cover; background-repeat: no-repeat; background-position: 50% 50%;");
                      }
                  }
                  return this;
              },

              changePercentage: function () {
                  if (this.model.get('percentage') >= 0 && this.model.get('percentage') <= 100) {
                      this.$el.find("span[data-field='percentage']").text(this.model.get('percentage') + '%');
                  } else if (this.model.get('percentage') == -1) {
                      this.$el.find("span[data-field='percentage']").text("(Gửi lỗi!)");
                  } else {
                      this.render();
                  }
              },

              reconnect: function () {
                  this.model.set('url',
                      btalk.fm.geturl(this.model.get('tenantid'), this.model.get('to'), this.model.get('name')));
              },

              changeUrl: function () {
                  this.render();
              }
          });

          var MessageNextView = Backbone.View.extend({
              events: {
                  'click #message_next': 'loadNextPageOfMessages'
              },

              initialize: function () {
                  this.listenTo(this.model, 'change', this.change);
              },

              render: function () {
                  this.$el.html($.tmpl(messagenextT, this.model.toJSON()));
                  return this;
              },

              change: function () {
                  this.$el.find('img')[0].style.visibility = this.model.get('state') == 2 ? null : 'hidden';
                  this.$el.find('span')[0].innerHTML = this.model.get('textloading');
                  return this;
              },

              loadNextPageOfMessages: function () {
                  // Chuyen trang thai sang loading
                  this.model.startLoading();
                  // Bat dau loading du lieu tu server
                  if (this.model.currentChatter() instanceof Backbone.Model) {
                      btalk.cm.getNextPageOfMessages(this.model.currentChatter().get('jid'),
                          btalk.CONVERSATIONDAYS.counts(), 30, this.model.currentChatter().get("type"));
                  }
              }
          });

          // Export to btalk.view api
          btalk.view = $.extend(btalk.view, {
              // User dang login hien tai
              ConversationDayView: ConversationDayView,
              MemberView: MemberView,
              MessagesView: MessagesView,
              AttachmentView: AttachmentView,
              MessageNextView: MessageNextView
          });
      });