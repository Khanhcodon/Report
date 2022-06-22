#region

using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities.Customer;
using Action = Bkav.eGovCloud.Core.Workflow.Action;
using UserDepartmentPosition = Bkav.eGovCloud.Core.Workflow.UserDepartmentPosition;

#endregion

namespace Bkav.eGovCloud.Business
{
    /// <author>Bkav Corp. - BSO - eGov - Department 2
    ///   Project: eGov Cloud v1.0
    ///   Class : WorkflowHelper - public - BLL
    ///   Access Modifiers: 
    ///   *Inherit: None
    ///   Create Date : 240513
    ///   Author      : GiangPN</author>
    /// <summary>
    ///   <para> Các hàm xử lý trên workflow </para>
    /// </summary>
    public class WorkflowHelper
    {
        #region Readonly & Static Fields

        private readonly DepartmentBll _departmentService;
        private readonly WorkflowBll _workflowService;
        private readonly ResourceBll _resourceService;

        #endregion

        #region Fields

        private List<UserDepartmentPosition> _userDepartmentPositions;

        #endregion

        #region C'tors

        ///<summary>
        ///  Khởi tạo class <see cref="IncreaseBll" />.
        ///</summary>
        ///<param name="departmentService"> Bll tương ứng của bảng Department trong hệ thống </param>
        ///<param name="workflowService"> Bll tương ứng của bảng Workflow trong hệ thống </param>
        ///<param name="resourceService"> Bll tương ứng của bảng ResourceBll trong hệ thống </param>
        public WorkflowHelper(DepartmentBll departmentService,
                              WorkflowBll workflowService,
                              ResourceBll resourceService)
        {
            _departmentService = departmentService;
            _workflowService = workflowService;
            _resourceService = resourceService;
        }

        #endregion

        #region Instance Properties

        private IEnumerable<UserDepartmentPosition> UserDepartmentPositions
        {
            get
            {
                if (_userDepartmentPositions == null)
                {
                    _userDepartmentPositions =
                        _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
                            t =>
                            new UserDepartmentPosition
                                {
                                    Id = t.UserDepartmentJobTitlesPositionId,
                                    UserId = t.UserId,
                                    DepartmentId = t.DepartmentId,
                                    DepartmentIdExt = t.DepartmentIdExt,
                                    PositionId = t.PositionId,
                                    //IsPrimary = t.IsPrimary
                                }).ToList();
                }

                if (!_userDepartmentPositions.Any())
                {
                    throw new Exception("Không tìm thấy hướng chuyển!");
                }

                return _userDepartmentPositions;
            }
        }

        #endregion

        #region Instance Methods

        ///// <summary>
        ///// </summary>
        ///// <param name="doctypeId"> </param>
        ///// <param name="actionId"> </param>
        ///// <returns> </returns>
        //public Action GetAction(Guid doctypeId, string actionId)
        //{
        //    var workflow = _doctypeService.GetWorkflowActive(doctypeId);
        //    if (workflow == null)
        //    {
        //        throw new WorkflowNotFoundException();
        //    }
        //    return GetAction(workflow.JsonInObject, actionId);
        //}

        ///// <summary>
        ///// </summary>
        ///// <param name="workflowId"> </param>
        ///// <param name="actionId"> </param>
        ///// <returns> </returns>
        //public Action GetAction(int workflowId, string actionId)
        //{
        //    var workflow = _workflowService.Get(workflowId);
        //    if (workflow == null)
        //    {
        //        throw new WorkflowNotFoundException();
        //    }
        //    return GetAction(workflow.JsonInObject, actionId);
        //}

        /// <summary>
        /// </summary>
        /// <param name="path"> </param>
        /// <param name="actionId"> </param>
        /// <returns> </returns>
        private Action GetAction(Path path, string actionId)
        {
            return path.GetAction(actionId);
        }

        ///// <summary>
        ///// </summary>
        ///// <param name="doctypeId"> </param>
        ///// <param name="userId"> </param>
        ///// <param name="workflowId"> </param>
        ///// <returns> </returns>
        //private IEnumerable<Action> GetActionsCreate(Guid doctypeId, int userId, out int workflowId)
        //{
        //    var workflow = _workflowService.GetIsActived(doctypeId);
        //    if (workflow == null)
        //    {
        //        throw new WorkflowNotFoundException();
        //    }
        //    workflowId = workflow.WorkflowId;
        //    return GetActionsCreate(workflow, userId);
        //}

        ///// <summary>
        ///// </summary>
        ///// <param name="workflowId"> </param>
        ///// <param name="userId"> </param>
        ///// <returns> </returns>
        //private IEnumerable<Action> GetActionsCreate(int workflowId, int userId)
        //{
        //    var workflow = _workflowService.Get(workflowId);
        //    return GetActionsCreate(workflow, userId);
        //}

        /// <summary>
        /// </summary>
        /// <param name="workflow"> </param>
        /// <param name="userId"> </param>
        /// <returns> </returns>
        public IEnumerable<Action> GetActionsCreate(Workflow workflow, int userId)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            if (string.IsNullOrWhiteSpace(workflow.Json))
            {
                throw new WorkflowNotFoundException();
            }
            var path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            return GetActionsCreate(path, userId);
        }

        /// <summary>
        /// </summary>
        /// <param name="path"> </param>
        /// <param name="userId"> </param>
        /// <returns> </returns>
        private IEnumerable<Action> GetActionsCreate(Path path, int userId)
        {
            return WorkflowUtil.GetActionsCreate(path, userId, UserDepartmentPositions);
        }

        ///// <summary>
        /////   Trả về danh sách hướng chuyển(Action) của 1 cán bộ khi edit văn bản/hồ sơ
        ///// </summary>
        ///// <param name="workflowId"> Quy trình xử lý </param>
        ///// <param name="userId"> Id của User đăng nhập </param>
        ///// <param name="currentNodeId"> Node hiện tại của công văn: trường hợp không có node hiện tại là khi khởi tạo văn bản. </param>
        ///// <para> trường hợp có giá trị là khi bàn giao 1 công văn được chuyển đến. </para>
        ///// <returns> Danh sách hướng chuyển </returns>
        //public IEnumerable<Action> GetActionsEdit(int workflowId, int userId, int currentNodeId)
        //{
        //    var workflow = _workflowService.Get(workflowId);
        //    if (workflow == null)
        //    {
        //        throw new Exception("Workflow not found");
        //    }
        //    return GetActionsEdit(workflow, userId, currentNodeId);
        //}

        /// <summary>
        ///   Trả về danh sách hướng chuyển(Action) của 1 cán bộ khi edit văn bản/hồ sơ
        /// </summary>
        /// <param name="workflow"> Quy trình xử lý </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="currentNodeId"> Node hiện tại của công văn: trường hợp không có node hiện tại là khi khởi tạo văn bản. </param>
        /// <para> trường hợp có giá trị là khi bàn giao 1 công văn được chuyển đến. </para>
        /// <returns> Danh sách hướng chuyển </returns>
        public IEnumerable<Action> GetActionsEdit(Workflow workflow, int userId, int currentNodeId)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            if (string.IsNullOrWhiteSpace(workflow.Json))
            {
                throw new WorkflowNotFoundException();
            }
            var path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            if (path == null)
            {
                throw new WorkflowNotFoundException();
            }
            return GetActionsEdit(path, userId, currentNodeId);
        }

        /// <summary>
        ///   Trả về danh sách hướng chuyển(Action) của 1 cán bộ khi edit văn bản/hồ sơ
        /// </summary>
        /// <param name="path"> Quy trình xử lý </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="currentNodeId"> Node hiện tại của công văn: trường hợp không có node hiện tại là khi khởi tạo văn bản. </param>
        /// <para> trường hợp có giá trị là khi bàn giao 1 công văn được chuyển đến. </para>
        /// <returns> Danh sách hướng chuyển </returns>
        private IEnumerable<Action> GetActionsEdit(Path path, int userId, int currentNodeId)
        {
            var actionsEdit = WorkflowUtil.GetActionsEdit(path, userId, UserDepartmentPositions, currentNodeId).ToList();
        
            //HopCV
            //Todo; chỗ này cần lấy ra resource làm j nhỉ, việc hiển thị tên hướng chuyển như nào là khi cấu hình quy trinh để vậy luôn sao lại lấy theo rescource 
            //for (var i = 0; i < actionsEdit.Count; i++)
            //{
            //    actionsEdit[i].Name = _resourceService.GetResource(actionsEdit[i].Name, false);
            //}

            return actionsEdit;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="doctypeId"></param>
        ///// <param name="nodeId"></param>
        ///// <param name="workflowId"> </param>
        ///// <returns></returns>
        //private Node GetNode(Guid doctypeId, int nodeId, out int workflowId)
        //{
        //    var workflow = _workflowService.GetIsActived(doctypeId);
        //    if (workflow == null)
        //    {
        //        throw new WorkflowNotFoundException();
        //    }
        //    workflowId = workflow.WorkflowId;
        //    return GetNode(workflow, nodeId);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public Node GetNode(int workflowId, int nodeId)
        {
            var workflow = _workflowService.GetFromCache(workflowId);
            return GetNode(workflow, nodeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflow"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public Node GetNode(Workflow workflow, int nodeId)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            var path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            return GetNode(path, nodeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public Node GetNode(Path path, int nodeId)
        {
            if (path == null)
            {
                throw new Exception("Không tìm thấy quy trình.");
            }
            var node = path.GetNode(nodeId);
            if (node == null)
            {
                throw new Exception("Quy trình lỗi! Node đến không tồn tại");
            }
            return node;
        }

        /// <summary>
        ///   Hàm lấy Permission của 1 Node
        /// </summary>
        /// <param name="workflowId"> </param>
        /// <param name="nodeId"> </param>
        /// <returns> </returns>
        public int GetNodePermission(int workflowId, int nodeId)
        {
            var workflow = _workflowService.GetFromCache(workflowId);
            return GetNodePermission(workflow, nodeId);
        }

        /// <summary>
        ///   Hàm lấy Permission của 1 Node
        /// </summary>
        /// <param name="workflow"> </param>
        /// <param name="nodeId"> </param>
        /// <returns> </returns>
        public int GetNodePermission(Workflow workflow, int nodeId)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            var path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            return GetNodePermission(path, nodeId);
        }

        /// <summary>
        ///   Hàm lấy Permission của 1 Node
        /// </summary>
        /// <param name="path"> </param>
        /// <param name="nodeId"> </param>
        /// <returns> </returns>
        public int GetNodePermission(Path path, int nodeId)
        {
            var node = path.GetNode(nodeId);
            if (node == null)
            {
                throw new Exception("Quy trình lỗi! Node đến không tồn tại");
            }
            return (int)node.GetNodePermission();
        }

        ///// <summary>
        /////   Trả về node khởi tạo văn bản của cán bộ userId.
        ///// </summary>
        ///// <param name="doctypeId"> Loại hồ sơ </param>
        ///// <param name="userId"> Id của User đăng nhập </param>
        ///// <param name="workflowId"> </param>
        ///// <returns> Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User </returns>
        //private IEnumerable<Node> GetStartNodes(Guid doctypeId, int userId, out int workflowId)
        //{
        //    var workflow = _workflowService.GetIsActived(doctypeId);
        //    if (workflow == null)
        //    {
        //        throw new WorkflowNotFoundException();
        //    }
        //    workflowId = workflow.WorkflowId;
        //    return GetStartNodes(workflow, userId);
        //}
        
        /// <summary>
        ///   Trả về node khởi tạo văn bản của cán bộ userId.
        /// </summary>
        /// <param name="workflowId"> Loại hồ sơ </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <returns> Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User </returns>
        public IEnumerable<Node> GetStartNodes(int workflowId, int userId)
        {
            var workflow = _workflowService.GetFromCache(workflowId);
            if (workflow == null)
            {
                throw new WorkflowNotFoundException();
            }
            return GetStartNodes(workflow, userId);
        }

        /// <summary>
        ///   Trả về node khởi tạo văn bản của cán bộ userId.
        /// </summary>
        /// <param name="workflow"> Quy trình </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <returns> Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User </returns>
        public IEnumerable<Node> GetStartNodes(Workflow workflow, int userId)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            var path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            return GetStartNodes(path, userId);
        }

        /// <summary>
        ///   Trả về node khởi tạo văn bản của cán bộ userId.
        /// </summary>
        /// <param name="path"> Quy trình </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <returns> Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User </returns>
        public IEnumerable<Node> GetStartNodes(Path path, int userId)
        {
            return WorkflowUtil.GetStartNodes(path, userId, UserDepartmentPositions);
        }

        /// <summary>
        /// Trả về danh sách các Node mà user thuộc vào trong quy trình
        /// </summary>
        /// <param name="path">Quy trình</param>
        /// <param name="userId">User</param>
        /// <returns></returns>
        public IEnumerable<Node> GetNodes(Path path, int userId)
        {
            return WorkflowUtil.GetNodes(path, userId, UserDepartmentPositions);
        }

        /// <summary>
        ///   Trả về danh sách các cán bộ thuộc hướng chuyển được chọn
        /// </summary>
        /// <param name="actionId"> Id hướng chuyển </param>
        /// <param name="workflowId"> Id của quy trình </param>
        /// <param name="userId"> Id user đăng nhập </param>
        /// <returns> </returns>
        public IEnumerable<int> GetUsersByActionId(int workflowId, string actionId, int userId)
        {
            var workflow = _workflowService.GetFromCache(workflowId);

            if (workflow == null || string.IsNullOrEmpty(workflow.Json))
            {
                throw new WorkflowNotFoundException();
            }
            if (string.IsNullOrEmpty(actionId))
            {
                throw new ActionNotFoundException();
            }

            Path path;
            try
            {
                path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            }
            catch (Exception)
            {
                throw new WorkflowFormatException();
            }

            return WorkflowUtil.GetUsers(path, userId, UserDepartmentPositions, actionId);
        }

        /// <summary>
        /// Trả về danh sách tất cả user trong không gian user mặc định của node.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns></returns>
        public IEnumerable<int> GetNodeUsers(Node node)
        {
            return WorkflowUtil.GetUsers(node, UserDepartmentPositions);
        }

        #endregion
    }
}