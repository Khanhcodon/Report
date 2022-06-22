
(function () {

    var Dialog = {
        _confirmEl: "#confirm.mdl-dialog",
        _confirmActionsEl: '#confirmActions.mdl-dialog',
        _tranferEl: "#tranfer.mdl-dialog",

        confirm: function (options) {
            var title = options.title;
            var message = options.message;
            var confirm = options.confirm;
            var cancel = options.cancel;
            var $el = $(this._confirmEl);
            var el = document.querySelector(this._confirmEl);

            document.querySelector(".confirm-dialog .mdl-dialog__content").textContent = message;
            document.querySelector(".confirm-dialog .mdl-dialog__title").textContent = title;

            document.querySelector(".confirm-dialog .btnConfirm, .confirm-dialog .btnCancel").removeEventListener("click", null);

            document.querySelector(".confirm-dialog .btnConfirm").addEventListener("click", function () {
                el.style["display"] = "none";
                el.close();

                if (typeof confirm === "function")
                    confirm();
                return;
            });

            document.querySelector(".confirm-dialog .btnCancel").addEventListener("click", function () {
                el.style["display"] = "none";
                el.close();

                if (typeof cancel === "function")
                    cancel();

                return;
            });

            el.style["display"] = "block";
            if (!el.showModal) {
                dialogPolyfill.registerDialog(el);
            }

            el.showModal();

        },

        confirmActions: function (options) {
            var title = options.title;
            var message = options.message;
            var el = document.querySelector(this._confirmActionsEl);

            $(".confirmActions-dialog .mdl-dialog__content").text(message);
            $(".confirmActions-dialog .mdl-dialog__title").text(title);
            this.firstAction = options.buttons[0];
            this.secondaryAction = options.buttons[1];

            var firstActionEl = $(".confirmActions-dialog .btnFirstAction");
            var secondaryActionEl = $(".confirmActions-dialog .btnSecondaryAction");

            firstActionEl.off('click');
            secondaryActionEl.off('click');

            firstActionEl.removeClass('hidden');
            firstActionEl.text(this.firstAction.text);
            firstActionEl.on("click", this.firstActionHandler.bind(this));

            secondaryActionEl.removeClass('hidden');
            secondaryActionEl.text(this.secondaryAction.text);
            secondaryActionEl.on("click", this.secondaryActionHander.bind(this));

            $(".confirmActions-dialog .btnCancel").on("click", function () {
                el.style["display"] = "none";
                el.close();
                return;
            });

            el.style["display"] = "block";
            if (!el.showModal) {
                dialogPolyfill.registerDialog(el);
            }

            el.showModal();
        },
                
        firstActionHandler: function (e) {
            var el = document.querySelector(this._confirmActionsEl);
            el.style["display"] = "none";
            el.close();

            this.firstAction && (typeof this.firstAction.callback === "function") && this.firstAction.callback();
            e.preventDefault();
        },

        secondaryActionHander: function (e) {
            var el = document.querySelector(this._confirmActionsEl);
            el.style["display"] = "none";
            el.close();

            this.secondaryAction && (typeof this.secondaryAction.callback === "function") && this.secondaryAction.callback();
            e.preventDefault();
        }
};

egov.dialog = Dialog;

})();