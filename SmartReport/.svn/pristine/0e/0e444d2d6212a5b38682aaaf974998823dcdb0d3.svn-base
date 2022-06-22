using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : Path - public - Entity
    /// Access Modifiers: 
    /// Create Date : 091012
    /// Author      : GiangPN
    /// *************************************************
    /// Modify Date: 240113
    /// Editor: CuongNT
    /// Resons:  Chuyển một số hàm từ WorkflowUtil vào Node.cs và Path.cs do nó là hàm xử lý dữ liệu nội bộ của các class này.
    /// *************************************************
    /// </author>
    /// <summary>
    /// <para>Định nghĩa 1 quy trình (1 luồng công văn)</para>
    /// (GiangPN@bkav.com - 091012)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Path
    {
        private List<Node> _nodes;

        /// <summary>
        /// Lấy hoặc thiết lập ID quy trình
        /// </summary>
        /// <value>ID</value>
        [JsonProperty("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của quy trình
        /// </summary>
        /// <value>AddStr</value>
        [JsonProperty("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ID loại hồ sơ(văn bản)
        /// </summary>
        /// <value>DocTypeId</value>
        [JsonProperty("DOCTYPEID")]
        public int DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái kích hoạt của quy trình đối với loại hồ sơ
        /// </summary>
        /// <value>True or False</value>
        [JsonProperty("ISACTIVATED")]
        public bool IsActivated { get; set; }

        #region Children
        /// <summary>
        /// Lấy hoặc thiết lập danh sách các Node trong quy trình.
        /// </summary>
        /// <value>Danh sách Node</value>
        [JsonProperty("NODE")]
        public List<Node> Nodes
        {
            get
            {
                return _nodes ?? (_nodes = new List<Node>());
            }
            set { _nodes = value; }
        }
        #endregion

        /// <summary>
        /// <para>Trả về 1 nút yêu cầu trong quy trình</para>
        /// CuongNT - 240113
        /// </summary>
        /// <param name="nodeId">Id của nút yêu cầu</param>
        /// <returns></returns>
        public Node GetNode(int nodeId)
        {
            return Nodes.SingleOrDefault(c => c.Id == nodeId);
        }

        /// <summary>
        /// Trả về danh sách các Node user thuộc vào
        /// </summary>
        /// <param name="userId">Id user</param>
        /// <param name="userDeptPos">Danh sách quan hệ người dùng phòng ban chức vụ</param>
        /// <returns></returns>
        public IEnumerable<Node> GetNodes(int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var result = new List<Node>();
            var userDepartmentJobTitleses = userDeptPos as List<UserDepartmentPosition> ?? userDeptPos.ToList();
            foreach (var node in Nodes)
            {
                var defaultAddress = node.GetDefaultAddress();
                List<string> depIdExt;

                if (defaultAddress == null || (defaultAddress.Queries.Any() && !defaultAddress.Queries.ContainUser(userId, userDepartmentJobTitleses, out depIdExt)))
                {
                    continue;
                }
                result.Add(node);
            }
            return result;
        }

        /// <summary>
        /// <para>Trả về danh sách node có quyền khởi tạo trong quy trình</para>
        /// (CuongNT@bkav.com - 020413)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> GetStartNodes()
        {
            return Nodes.Where(t => t.IsStart);
        }

        /// <summary>
        /// <para>Trả về danh sách node có quyền khởi tạo của cán bộ</para>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> GetStartNodes(int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var result = new List<Node>();
            var userDepartmentJobTitleses = userDeptPos as List<UserDepartmentPosition> ?? userDeptPos.ToList();
            var nodeStart = Nodes.Where(t => t.IsStart).OrderBy(t => t.Id);
            foreach (var node in nodeStart)
            {
                var defaultAddress = node.GetDefaultAddress();
                List<string> depIdExt;

                if (defaultAddress == null || (defaultAddress.Queries.Any() && !defaultAddress.Queries.ContainUser(userId, userDepartmentJobTitleses, out depIdExt)))
                {
                    continue;
                }
                result.Add(node);
            }
            return result;
        }

        /// <summary>
        /// <para>Trả về node khởi tạo văn bản của cán bộ (nếu có nhiều hơn 1 node khởi tạo thì sẽ lấy node có Id bé hơn).</para>
        /// (CuongNT@bkav.com - 020413)
        /// </summary>
        /// <param name="userId">Id của User đăng nhập</param>
        /// <param name="userDeptPos">Danh sách đối tượng UserDepartmentPosition(User - Phòng ban - Chức vụ)</param>
        /// <returns>Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User</returns>
        public Node GetStartNode(int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            Node result = null;
            var userDepartmentJobTitleses = userDeptPos as List<UserDepartmentPosition> ?? userDeptPos.ToList();
            var nodeStart = Nodes.Where(t => t.IsStart).OrderBy(t => t.Id);
            foreach (var node in nodeStart)
            {
                var defaultAddress = node.GetDefaultAddress();
                List<string> depIdExt;

                if (defaultAddress == null || (defaultAddress.Queries.Any() && !defaultAddress.Queries.ContainUser(userId, userDepartmentJobTitleses, out depIdExt)))
                {
                    continue;
                }
                result = node;
                break;
            }
            return result;
        }

        /// <summary>
        /// <para>Kiểm tra quyền khởi tạo văn bản của cán bộ userId.</para>
        /// (CuongNT@bkav.com - 020413)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userDeptPos"></param>
        /// <returns></returns>
        public bool CheckQuyenKhoiTao(int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var startNode = GetStartNode(userId, userDeptPos);
            return startNode != null;
        }

        /// <summary>
        /// <para>Trả về tất cả các Action(hướng chuyển) của 1 đường đi(quy trình).</para>
        /// CuongNT - 240113
        /// </summary>
        /// <returns>Danh sách hướng chuyển</returns>
        public IEnumerable<Action> GetActions()
        {
            var list = new List<Action>();
            foreach (var action in Nodes.Select(n => n.Actions))
            {
                list.AddRange(action);
            }
            return list;
        }

        /// <summary>
        /// <para>Trả về hướng chuyển yêu cầu</para>
        /// CuongNT - 240113
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public Action GetAction(string actionId)
        {
            var actions = GetActions();
            var result = actions.SingleOrDefault(t => t.Id == actionId);
            return result;
        }
    }
}