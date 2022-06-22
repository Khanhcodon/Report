define([
    egov.template.document.supplementary
],
function (SuppTemplate) {
    "use strict";

    //#region Relation View

    var SupplementaryView = Backbone.View.extend({

        initialize: function (options) {
            this.$(".supplementary-list").empty();

            var that = this;
            that.supplementary = options.supplementary;
            that.document = options.document;

            that.model = new egov.models.supplementaryList(that.supplementary);

            options.document.model.set('Supplementary', that.model);

            that.model.on("add", function (model) {
                that.$el.parents('.document-supplementary').show();
                that._addSuppItem(model);
            });

            that.model.on("remove", function (model) {
                model.view.$el.remove();
                if (that.model.length == 0) {
                    that.$el.parent().hide();
                }
            });

            that.render();

            return that;
        },

        render: function () {
            var that = this,
                supplementaryItem;

            if (this.model.length > 0) {
                this.$el.parents('.document-supplementary').show();
                this.model.each(function (supp) {
                    that._addSuppItem(supp);
                });
            } else {
                this.$el.parents('.document-supplementary').hide();
            }

            return this;
        },

        _addSuppItem: function (model) {
            var that = this;
            var supplementaryItem = new SupplementaryItem({
                model: model,
                parent: that
            });
            that.$el.find(".supplementary-list").append(supplementaryItem.$el);
        }
    });

    var SupplementaryItem = Backbone.View.extend({
        tagName: 'tr',
        template: SuppTemplate,
        selectedClass: 'rowSelected',

        events: {
            'click': 'selected',
            'click .supp-open': '_open',
            'dblclick': '_open'
        },

        initialize: function (option) {
            this.parent = option.parent;
            this.render();
            var that = this;

            this.model.on("change:UserReceivedId", function () {
                that.render();
            });
        },

        render: function () {
            this.$el.empty();
            this.$el.append($.tmpl(this.template, this.model.toJSON()));

            this.model.view = this;
            return this;
        },

        selected: function () {
            egov.helper.hideAllContext();
            this.$el.addClass(this.selectedClass);
            this.$el.siblings('.' + this.selectedClass).removeClass(this.selectedClass);
        },

        _open: function () {
            egov.helper.hideAllContext();
            var that = this;
            var message = undefined;

            that.parent.document.receiveSupplementary(that.model.get("SupplementaryId"), this);
        }
    });

    //#endregion

    return SupplementaryView;
});