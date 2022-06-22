(function (egov) {
    /*
     * TienBV:
     * Thư viện xử lý quy trình xử lý
     */

    var Workflow, getDefaultAction;

    // Trả về action mặc định
    getDefaultAction = function () {
        return {
            id: 0,
            workflowId: 0,
            name: "",
            userIdNext: 0,
            isSpecial: false,
            nextNodeId: 0,
            currentNodeId: 0,
            isAllow: false,
            isAllowSign: false,
            priority: 0
        }
    };

    Workflow = function () {
        /// <summary>
        /// Đối tượng xử lý luồng văn bản
        /// </summary>
    }

    Workflow.prototype.init = function (workflows, users, depts, userDeptPositions, userId) {
        /// <summary>
        /// Khởi tạo thư viện
        /// </summary>
        /// <param name="workflows" type="array">Danh sách tất cả các quy trình</param>
        /// <param name="users" type="array">Danh sách tất cả người dùng trong hệ thống</param>
        /// <param name="depts" type="array">Danh sách tất cả phòng ban trong hệ thống</param>
        /// <param name="userDeptPositions" type="array">Danh sách tất cả quan hệ Người dùng - Phòng ban - Chức vụ</param>
        /// <param name="userId" type="int">Người dùng hiện tại</param>
        this.workflows = workflows;
        this.users = users;
        this.depts = depts;
        this.userDeptPositions = userDeptPositions;
        this.userId = userId;

        return this;
    }

    Workflow.prototype.getActionCreate = function (workflowId) {
        /// <summary>
        /// Trả về danh sách các hướng chuyển khi tạo mới hồ sơ, văn bản
        /// </summary>
        /// <param name="workflowId" type="int">WorkflowId của loại hồ sơ, văn bản đang tạo</param>

    }

    Workflow.prototype.getActionEdit = function (workflowId, nodeId) {
        /// <summary>
        /// Trả về danh sách các hướng chuyển khi bàn giao hồ sơ, văn bản
        /// </summary>
        /// <param name="workflowId" type="int">WorkflowId của hồ sơ, văn bản đang mở</param>
        /// <param name="nodeId" type="int">Node hiện tại</param>


    }

    Workflow.prototype.getUserByAction = function (workflowId, actionId) {
        /// <summary>
        /// Trả về danh sách người dùng theo hướng chuyển
        /// </summary>
        /// <param name="workflowId" type="int">WorkflowId đang sử dụng</param>
        /// <param name="actionId" type="int">Hướng chuyển đang sử dụng</param>


    }

    // Gán lại vào global
    egov.workflows = new Workflow();
})
(this.egov = this.egov || {})