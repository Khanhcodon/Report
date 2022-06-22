(function (eForm, $, undefined) {

    // Xuất thông báo cho khách trong quá trình nhập liệu/submit form
    eForm.Status = {
        lstInvalidCtrl: new Array()   // Lưu các control ko hợp lệ
    ,
        setStatus: function (objId, status, controlId) {
            if (trim(objId) != "")
                this.setStatusByObjID(objId, status);
            else if (controlId != "")
                this.setStatusByCtrlId(controlId, status);
            //end if   
        } //end setStatus
	,
        getStatus: function (objId) {
            for (var i = 0, l = this.lstInvalidCtrl.length; i < l; i++) {
                if (this.lstInvalidCtrl[i].Value == objId) {
                    return this.lstInvalidCtrl[i].Text;
                } //end if
            } //end for
            return -1;
        } //end getStatus
	,
        // Hàm đếm số lượng control bị lỗi trong form
        Count: function () {
            return this.lstInvalidCtrl.length;
        } //end Count
	,
        // Hiện các thông báo lỗi của control (nếu có)
        Show: function () {
            if (this.Count() == 0) return; // nếu có
            for (var i = 0, l = this.lstInvalidCtrl.length; i < l; i++) {
                switch (parseInt(this.lstInvalidCtrl[i].Text)) {
                    case 1:
                        $('#' + this.lstInvalidCtrl[i].Value + '_required').css('display', 'inline');
                        break;
                    case 2:
                        $('#' + this.lstInvalidCtrl[i].Value + 'Error').css('display', 'inline');
                        break;
                } //end switch
            } //end for
            $('#' + this.lstInvalidCtrl[0].Value).focus();
        } //end Show
	,
        /* 
        * ObjId chính là ctrlId + ctrlTypeId
        * Status: 
        * - 0 là ko có lỗi gì, ko add vào, nếu có thì remove đi luôn
        * - 1 là lỗi Required
        * - 2 là lỗi Kiểu dữ liệu - Mask (datetime/CMND/numbers/...)
        */
        setStatusByObjID: function (objId, status) {
            // Kiểm tra và update lại control đã có status
            for (var i = 0, l = this.lstInvalidCtrl.length; i < l; i++) {
                if (this.lstInvalidCtrl[i].Value == objId) {
                    if (this.lstInvalidCtrl[i].Text == status) return;
                    //
                    switch (status) {
                        case 0:
                            this.lstInvalidCtrl.remove(i);
                            break;
                        default:
                            this.lstInvalidCtrl[i].Text = status;
                            break;
                    } //end switch
                    return;
                } //end if
            } //end for
            //
            if (status == 0) return;
            this.lstInvalidCtrl[this.lstInvalidCtrl.length] = new ListItem(status, objId);
        } //end setStatusByObjID
	,
        setStatusByCtrlId: function (controlId, status) {
            if (controlId <= 0) return;
            //Tìm objId dựa vào property controlId (p15)
            var lstctrl = database.FindControlsByProp('p15', controlId);
            if (lstctrl.length == 1) {
                var objId = lstctrl[0].getKey(); // lstctrl[0].getId() + lstctrl[0].getTypeId();
                if (objId != "")
                    this.setStatusByObjID(objId, status);
            } //end if
        } //end setStatusByCtrlId
    }//end class
    ControlStatus = eForm.Status;

    /* function ListItem(text, value, selected)
    Entity of ListItems (for Dropdown list, Checkbox list, ...)
    */
    //#region
    function ListItem(text, value, selected) {
        this.Text = text;
        this.Value = value;
        this.Selected = selected;
    }
})(window.eForm = window.eForm || {}, jQuery);