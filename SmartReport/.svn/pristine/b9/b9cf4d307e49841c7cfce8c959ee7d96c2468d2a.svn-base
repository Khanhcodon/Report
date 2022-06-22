define([
    egov.template.ViewTreeHtml,
], function (view) {
    var TreeViewModel = Backbone.Model.extend({
        defaults: function() {
            return {
                DocumentId: "fasdas"
            }
        }
    });
    var treemodel = new TreeViewModel();

    var TreeView = Backbone.View.extend({

        template: view,

        tagName: "p",

        model: treemodel,

        events: {
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            var that = this;
            that.model.set({ DocumentId : options.DocumentId });
            that.document = options.document;
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            this.$el.html(this.template);
            var that = this;
            var dialogSetting = {
                width: "auto",
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Theo dõi báo cáo dạng cây",
                buttons: [
                      {
                          text: egov.resources.common.closeButton,
                          click: function () {
                              that.$el.dialog("hide");
                          }
                      }
                ]
            };

            that.$el.dialog(dialogSetting);
            that.$("#viewLeaf").append(' <iframe id="iframe-tree" style="width: 100%; height: 100%; border: none; " src="/DocumentReport/timeline?docId=' + that.model.get("DocumentId") + '"></iframe> ');

            return that;
        },
    });

    return TreeView;
});