
define([
    egov.template.document.storePrivate
],
 function (Template) {

     'use strict';

     var _resource = egov.resources.document.docStore;

     /// <summary>Đối tượng view hiển thị form lưu sổ cá nhân</summary>
     var StorePrivate = Backbone.View.extend({
         className: 'docStorePrivate',
         template: Template,

         events: {
             'click .list-group-item': '_selected',
             'dblclick .list-group-item': '_selectAndSend'
         },

         initialize: function (option) {
             /// <summary>
             /// Khởi tạo
             /// </summary>
             this.$el.html(this.template);
             this.$el.bindResources();
             this.$storePrivate = this.$('.storePrivates');
             this.$storeShare = this.$('.storeShares');

             var that = this;

             egov.dataManager.getStorePrivate({
                 success: function (result) {
                     if (result) {
                         that.model = result;
                         var storePrivates = result.storePrivate;
                         var storeSharies = result.storeShare;
                         that.$storePrivate.append($.tmpl('<li class="list-group-item"><a href="#" value="${id}"></a>${name}</li>', storePrivates));
                         that.$storeShare.append($.tmpl('<li class="list-group-item"><a href="#" value="${id}"></a>${name}</li>', storeSharies));
                     }
                 },
                 error: function () {
                     egov.pubsub.publish(egov.events.status.error, egov.resources.treeStore.message.error.selectStore);
                 }
             });

             return this;
         },

         render: function (callback) {
             /// <summary>
             /// Render form
             /// </summary>
             /// <param name="callback">Hàm callback sau khi chọn Hồ sơ.</param>
             this.callback = callback;
             var that = this;

             this.$el.attr("help-content-page", "storePrivate")
             this.$el.dialog({
                 title: _resource.dialogTitle,
                 draggable: true,
                 width: "400px",
                 height: "400px",
                 //keyboard: true,
                 buttons: [
                     {
                         text: _resource.createNew,
                         className: 'pull-left',
                         click: function () {
                             that._createNew();
                         }
                     },
                     {
                         text: _resource.saveButton,
                         className: 'btn-primary',
                         disableProcess: true,
                         click: function (callback2) {
                             if (that.selectedId === undefined) {
                                 egov.pubsub.publish(egov.events.status.warning, _resource.noChooseStore);

                                 egov.callback(callback2);
                                 return;
                             }
                             if (typeof that.callback === 'function') {
                                 that.callback(that.selectedId, callback2);
                             }
                         }
                     },
                      {
                          text: _resource.notSaveButton,
                          className: 'btn-info',
                          disableProcess: true,
                          click: function (callback2) {
                              if (typeof that.callback === 'function') {
                                  that.callback(null, callback2);
                              }
                          }
                      }
                 ]
             });
         },

         _selected: function (e) {
             /// <summary>
             /// Chọn hồ sơ
             /// </summary>
             /// <param name="e"></param>
             var target = $(e.target);
             if (target.is('.list-group-item')) {
                 this.selectedId = target.find('a').attr('value');
                 this.$('.active').removeClass('active');
                 target.addClass('active');
             }
             egov.helper.destroyClickEvent(e);
         },

         _selectAndSend: function (e) {
             /// <summary>
             /// Chọn và tiếp tục
             /// </summary>
             var target = $(e.target);
             if (target.is('.list-group-item')) {
                 this.$('.active').removeClass('active');
                 target.addClass('active');
                 this.selectedId = target.find('a').attr('value');
                 if (typeof this.callback === 'function') {
                     this.callback(this.selectedId);
                 }
             }
         },

         _createNew: function () {
             /// <summary>
             /// Tạo mới hồ sơ cá nhân
             /// </summary>
             var that = this;
             egov.message.prompt(egov.resources.storePrivate.storePrivateNameWarning, _resource.createNew, function (storePrivateName) {
                 if (storePrivateName === '' || storePrivateName === null) {
                     return;
                 }

                 egov.request.createStorePrivate(
                     {
                         data: {
                             storePrivateName: storePrivateName,
                             descStorePrivate: '',
                             parentId: null,
                             userIdJoined: [],
                             __RequestVerificationToken: $('input[name="__RequestVerificationToken"]', "#StorePrivateCreate").val()
                         },
                         success: function (result) {
                             if (!result || !result.success) {
                                 return;
                             }

                             var newId = result.id;
                             that.$('.active').removeClass('active');
                             that.$storePrivate.append($.tmpl('<li class="list-group-item active"><a href="#" value="${id}"></a>${name}</li>', { id: newId, name: storePrivateName }));
                             that.selectedId = newId;

                             //Tạo đối tượng model Sổ hồ sơ
                             var newStore = {
                                 isStorePrivateRoot: false,
                                 isStoreShared: false,
                                 name: storePrivateName,
                                 root: false,
                                 state: "in",
                                 status: 0,
                                 storePrivateId: newId,
                                 storePrivateName: storePrivateName,
                             };
                             newStore = new egov.models.StorePrivateModel(newStore);

                             //Tạo hiển thị trên cấy hồ sơ
                             if (egov.views && egov.views
                                 && egov.views.home.tree
                                 && egov.views.home.tree.storeTree
                                 && egov.views.home.tree.storeTree.storesModel
                                 && egov.views.home.tree.storeTree.storesModel.length > 0) {

                                 var models = egov.views.home.tree.storeTree.storesModel;
                                 for (var i = 0; i < models.length; i++) {

                                     if (!models[i].model.get('isStoreShared')
                                         && (models[i].model.get('id') == null
                                         || models[i].model.get('id') == 0)) {

                                         models[i].add(newStore);
                                         break;
                                     }
                                 }
                             }
                         }
                     }
                 );
             });
         }
     });

     return StorePrivate;
 });