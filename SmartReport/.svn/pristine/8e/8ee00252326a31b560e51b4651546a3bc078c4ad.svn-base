#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>Bkav Corp. - BSO - eGov - eOffice team
    ///   Project: eGov Cloud v1.0
    ///   Class : WorkflowUtil
    ///   Access Modifiers: 
    ///   Create Date : 141112
    ///   Author      : GiangPN
    ///   *************************************************
    ///   Modify Date: 240113
    ///   Editor: CuongNT
    ///   Resons:  Chuyển một số hàm từ WorkflowUtil vào Node.cs và Path.cs do nó là hàm xử lý dữ liệu nội bộ của các class này.
    ///   Path.cs: 
    ///   public Node GetNode(int nodeId);
    ///   public IEnumerable Action GetActions();
    ///   public Action GetAction(string actionId)
    ///   Node.cs:
    ///   public Address GetDefaultAddress()
    ///   public IEnumerable Address GetRelationAddresses()
    ///   public Address GetRelationAddress(int relationNodeId)
    ///   public IEnumerable Action GetActions()
    ///   private string RetrieveDescription(string name, Type enumeration)
    ///   *************************************************</author>
    /// <summary>
    ///   <para> Cung cấp các hàm dùng để lấy danh sách hướng chuyển, lấy danh sách user theo hướng chuyển </para>
    ///   (GiangPN@bkav.com - 141112)
    /// </summary>
    public class WorkflowUtil
    {
        #region Class Methods

        /// <summary>
        ///   Trả về danh sách hướng chuyển(Action) của 1 cán bộ khi khởi tạo văn bản/hồ sơ
        /// </summary>
        /// <param name="path"> Quy trình xử lý </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="userDeptPos"> Danh sách UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <para> trường hợp có giá trị là khi bàn giao 1 công văn được chuyển đến. </para>
        /// <returns> Danh sách hướng chuyển </returns>
        public static IEnumerable<Action> GetActionsCreate(Path path, int userId,
                                                           IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var userDeptPosTmp = userDeptPos as List<UserDepartmentPosition> ?? userDeptPos.ToList();

            var startNodes = path.GetStartNodes(userId, userDeptPosTmp);
            if (startNodes == null)
            {
                throw new ApplicationException(
                    "Không có quyền tạo văn bản do quy trình không có nút có vai trò khởi tạo.");
            }
            var result = new List<Action>();
            foreach (var startNode in startNodes)
            {
                result.AddRange(GetActions(userId, userDeptPosTmp, startNode));
            }
            return result;
        }

        /// <summary>
        ///   Trả về danh sách hướng chuyển(Action) của 1 cán bộ khi edit văn bản/hồ sơ
        /// </summary>
        /// <param name="path"> Quy trình xử lý </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="currentNodeId"> Node hiện tại của công văn: trường hợp không có node hiện tại là khi khởi tạo văn bản. </param>
        /// <param name="userDeptPos"> Danh sách UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <para> trường hợp có giá trị là khi bàn giao 1 công văn được chuyển đến. </para>
        /// <returns> Danh sách hướng chuyển </returns>
        public static IEnumerable<Action> GetActionsEdit(Path path, int userId,
                                                         IEnumerable<UserDepartmentPosition> userDeptPos,
                                                         int currentNodeId)
        {
            return GetActions(path, userId, userDeptPos, currentNodeId);
        }

        /// <summary>
        ///   <para> Lấy quyền xử lý nghiệp vụ trên Node </para>
        ///   (GiangPN@bkav.com - 200313)
        /// </summary>
        /// <param name="node"> </param>
        /// <returns> Giá trị thể hiện quyền nghiệp vụ trên Node </returns>
        public static NodePermissions GetNodePermission(Node node)
        {
            // CuongNT@bkav.com - 020413 sửa để tránh lặp code và chuyển hàm này vào trong đối tượng Node.
            return node.GetNodePermission();
        }

        /// <summary>
        ///   Trả về node khởi tạo văn bản của cán bộ (nếu có nhiều hơn 1 node khởi tạo thì sẽ lấy node có Id bé hơn).
        /// </summary>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="path"> Quy trình xử lý </param>
        /// <param name="userDeptPos"> Danh sách đối tượng UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User </returns>
        public static Node GetStartNode(Path path, int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            // CuongNT@bkav.com - 020413 sửa để tránh lặp code và chuyển hàm này vào trong đối tượng Path.
            return path.GetStartNode(userId, userDeptPos);
        }

        /// <summary>
        ///   Trả về danh sách node khởi tạo văn bản của cán bộ userId.
        /// </summary>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="path"> Quy trình xử lý </param>
        /// <param name="userDeptPos"> Danh sách đối tượng UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Null: không có quyền khởi tạo. Ngược lại là Id của Node hiện tại của User </returns>
        public static IEnumerable<Node> GetStartNodes(Path path, int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            return path.GetStartNodes(userId, userDeptPos);
        }

        /// <summary>
        /// Trả về danh sách các Node mà user thuộc vào trong quy trình
        /// </summary>
        /// <param name="path">Quy trình</param>
        /// <param name="userId">Id người dùng</param>
        /// <param name="userDeptPos">Danh sách quan hệ người dùng, phòng ban chức vụ</param>
        /// <returns></returns>
        public static IEnumerable<Node> GetNodes(Path path, int userId, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            return path.GetNodes(userId, userDeptPos);
        }

        /// <summary>
        ///   Lấy ra danh sách user theo hướng chuyển
        /// </summary>
        /// <param name="actionId"> Id của hướng chuyển </param>
        /// <param name="path"> Cấu hình quy trình </param>
        /// <param name="userId"> Id user đăng nhập </param>
        /// <param name="userDeptPos"> Chuỗi danh sách UserDepartmentPosition(User - Phòng ban - Chức )) </param>
        /// <returns> Danh sách Id user </returns>
        public static IEnumerable<int> GetUsers(Path path, int userId, IEnumerable<UserDepartmentPosition> userDeptPos,
                                                string actionId)
        {
            var results = new List<int>();

            var action = path.GetAction(actionId);
            var nextNode = path.GetNode(action.Next);

            // Lấy không gian user từ relationAddress trước nếu có
            var queries = new List<QueryBase>();
            var relationAddress = nextNode.GetRelationAddress(action.Current);
            if (relationAddress != null)
            {
                queries = relationAddress.Queries;
            }
            else
            {
                var defaultAddress = nextNode.GetDefaultAddress();
                if (defaultAddress != null)
                {
                    queries = defaultAddress.Queries;
                }
            }

            // Nếu không gian cán bộ rỗng + cán bộ không tồn tại trên hệ thống
            if (!queries.Any() || userDeptPos.All(c => c.UserId != userId))
            {
                return results;
            }

            // Không gian theo Cán bộ "userid"
            var userQueries = queries.Where(c => c.Type == QueryType.TheoCanBo).Select(o => (UserQuery)o).ToList();
            results.AddRange(GetUsers(userQueries, userDeptPos));

            // Không gian theo cấp "level"
            var levelQueries = queries.Where(c => c.Type == QueryType.TheoCap).Select(o => (LevelQuery)o).ToList();
            results.AddRange(GetUsers(levelQueries, userDeptPos));

            // Không gian theo vị trí "pos"
            var positionQueries =
                queries.Where(c => c.Type == QueryType.TheoViTri).Select(o => (PositionQuery)o).ToList();
            results.AddRange(GetUsers(positionQueries, userDeptPos));

            // Không gian theo quan hệ "relation"
            var relationQueries =
                queries.Where(c => c.Type == QueryType.TheoQuanHe).Select(o => (RelationQuery)o).ToList();
            var oldNode = path.Nodes.Single(t => t.Id == action.Current);
            results.AddRange(GetUsers(relationQueries, userDeptPos, oldNode, userId));

            return results;
        }

        /// <summary>
        /// Trả về danh sách user thuộc không gian mặc định của Node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="userDeptPos"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetUsers(Node node, IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var defaultAddress = node.GetDefaultAddress();
            var results = new List<int>();
            if (defaultAddress == null)
            {
                return results;
            }

            var queries = defaultAddress.Queries;

            if (!queries.Any())
            {
                return results;
            }

            // Không gian theo Cán bộ "userid"
            var userQueries = queries.Where(c => c.Type == QueryType.TheoCanBo).Select(o => (UserQuery)o).ToList();
            results.AddRange(GetUsers(userQueries, userDeptPos));

            // Không gian theo cấp "level"
            var levelQueries = queries.Where(c => c.Type == QueryType.TheoCap).Select(o => (LevelQuery)o).ToList();
            results.AddRange(GetUsers(levelQueries, userDeptPos));

            // Không gian theo vị trí "pos"
            var positionQueries =
                queries.Where(c => c.Type == QueryType.TheoViTri).Select(o => (PositionQuery)o).ToList();
            results.AddRange(GetUsers(positionQueries, userDeptPos));

            return results.Distinct();
        }

        /// <summary>
        ///   Trả về danh sách hướng chuyển(Action) của 1 cán bộ tại node hiện tại trên quy trình
        /// </summary>
        /// <param name="path"> Quy trình xử lý </param>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="currentNodeId">
        ///   <para> Node hiện tại của công văn: </para>
        ///   <para> Trường hợp không có node hiện tại là khi khởi tạo văn bản. </para>
        ///   <para> Trường hợp có node hiện tại là khi bàn giao 1 công văn đang xem. </para>
        /// </param>
        /// <param name="userDeptPos"> Danh sách UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Danh sách hướng chuyển </returns>
        private static IEnumerable<Action> GetActions(Path path, int userId,
                                                      IEnumerable<UserDepartmentPosition> userDeptPos,
                                                      int currentNodeId)
        {
            var currentNode = path.GetNode(currentNodeId);
            return currentNode == null
                       ? new List<Action>()
                       : GetActions(userId, userDeptPos, currentNode);
        }

        /// <summary>
        ///   Trả về danh sách hướng chuyển(Action) của 1 cán bộ tại node hiện tại trên quy trình
        /// </summary>
        /// <param name="userId"> Id của User đăng nhập </param>
        /// <param name="currentNode">
        ///   <para> Node hiện tại của công văn: </para>
        ///   <para> Trường hợp không có node hiện tại là khi khởi tạo văn bản. </para>
        ///   <para> Trường hợp có node hiện tại là khi bàn giao 1 công văn đang xem. </para>
        /// </param>
        /// <param name="userDeptPos"> Danh sách UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Danh sách hướng chuyển </returns>
        private static IEnumerable<Action> GetActions(int userId, IEnumerable<UserDepartmentPosition> userDeptPos, Node currentNode)
        {
            var results = new List<Action>();

            // Kiểm tra không gian user hiện tại có chứa userId
            // TienBV: bỏ check điều kiện này vì phải ràng buộc nhiều thứ mới được chuyển:
            // - Phải có user trong 1 phòng ban nào đấy, các user mới tạo thì không chuyển được.
            // - Chỉ check cấu hình "Bất kỳ" mặc định của node hiện tại. Nên nếu người ta cấu hình 2 node đến 2 không gian user khác nhau thì ko chuyển được.
            // => Nếu check tất cả các node đến thì rất chậm
            // => Thấy không cần thiết nên tạm bỏ đi, cần thì thêm vào sau.
            //List<string> depIdExt;
            //if (!currentNode.ContainUser(userId, userDeptPos, out depIdExt))
            //{
            //    return results;
            //}

            // Nếu có thì lấy các hướng chuyển
            results.AddRange(currentNode.GetActions());

            return results.OrderBy(t => t.Name);
        }

        /// <summary>
        ///   Lấy cấp độ của phòng ban
        /// </summary>
        /// <param name="depIdExt"> Id phòng ban mở rộng: dạng 2.34.45 </param>
        /// <returns> </returns>
        private static int GetLevel(string depIdExt)
        {
            return depIdExt.ToCharArray().Where(t => t == '.').Count() + 1;
        }

        /// <summary>
        ///   Lấy phòng ban cha của phòng ban
        /// </summary>
        /// <param name="departmentIdExt"> Id mở rộng của phòng ban </param>
        /// <returns> Id mở rộng phòng ban cấp cha </returns>
        private static string GetParentIdExt(string departmentIdExt)
        {
            if (!departmentIdExt.Contains("."))
                return string.Empty;
            return departmentIdExt.Substring(0, departmentIdExt.LastIndexOf('.'));
        }

        /// <summary>
        ///   Lấy ra danh sách user theo không gian user được định nghĩa trong từng Node.
        /// </summary>
        /// <param name="userQueries"> Không gian User được định nghĩa </param>
        /// <param name="userDeptPos"> Chuỗi danh sách UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Danh sách Id user </returns>
        private static IEnumerable<int> GetUsers(IEnumerable<UserQuery> userQueries,
                                                 IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var results = new List<int>();
            foreach (var userQuery in userQueries)
            {
                if (userDeptPos.Any(c => c.UserId == userQuery.UserId))
                {
                    results.Add(userQuery.UserId);
                }
            }
            return results;
        }

        /// <summary>
        ///   Lấy danh sách user từ không gian user cấu hình theo vị trí
        /// </summary>
        /// <param name="positionQueries"> Không gian user lấy theo vị trí tương đối với 1 node trên cây phòng ban </param>
        /// <param name="userDeptPos"> Danh sách đối tượng UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Danh sách Id user </returns>
        private static IEnumerable<int> GetUsers(IEnumerable<PositionQuery> positionQueries,
                                                 IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var results = new List<int>();
            foreach (var positionQuery in positionQueries)
            {
                var userIds = new List<int>();
                if (positionQuery.Position == PositionType.DonViCapDuoi)
                {
                    userIds = userDeptPos.Where(t => t.DepartmentIdExt.Contains(positionQuery.DepId + ".")
                                                      &&
                                                      GetLevel(t.DepartmentIdExt) ==
                                                      GetLevel(positionQuery.DepId.ToString()) + 1
                                                      &&
                                                      (positionQuery.PositionId == -1 ||
                                                       t.PositionId == positionQuery.PositionId))
                        .Select(t => t.UserId).ToList();
                }
                else if (positionQuery.Position == PositionType.DonViTrucThuoc)
                {
                    userIds = userDeptPos.Where(t => t.DepartmentIdExt.Contains(positionQuery.DepId + ".")
                                                      &&
                                                      (positionQuery.PositionId == -1 ||
                                                       t.PositionId == positionQuery.PositionId))
                        .Select(t => t.UserId).ToList();
                }
                else if (positionQuery.Position == PositionType.DonViHienTai)
                {
                    userIds =
                        userDeptPos.Where(t => (t.DepartmentId == positionQuery.DepId 
                            && (
                                positionQuery.PositionId == -1 ||
                                t.PositionId == positionQuery.PositionId)))
                            .Select(t => t.UserId).ToList();
                }
                results.AddRange(userIds);
            }

            return results;
        }

        /// <summary>
        ///   Lấy danh sách user từ không gian user theo kiểu cấp
        /// </summary>
        /// <param name="levelQueries"> Không gian user theo kiểu cấp </param>
        /// <param name="userDeptPos"> Danh sách đối tượng UserDepartmentPosition(User - Phòng ban - Chức vụ) </param>
        /// <returns> Danh sách </returns>
        private static IEnumerable<int> GetUsers(IEnumerable<LevelQuery> levelQueries,
                                                 IEnumerable<UserDepartmentPosition> userDeptPos)
        {
            var results = new List<int>();

            foreach (var levelQuery in levelQueries)
            {
                var userIds = userDeptPos.Where(t => GetLevel(t.DepartmentIdExt) == levelQuery.Level
                                                      &&
                                                      (levelQuery.PositionId == -1 || t.PositionId == levelQuery.PositionId))
                    .Select(t => t.UserId).ToList();
                results.AddRange(userIds);
            }
            return results;
        }

        /// <summary>
        ///   Trả về danh sách UserId theo quan hệ với nút hiện tại
        /// </summary>
        /// <param name="relationNode"> Nút liên quan tới không gian cán bộ quan hệ </param>
        /// <param name="relationQueries"> Query không gian cán bộ theo quan hện </param>
        /// <param name="userDeptPos"> Quan hệ cán bộ, phòng ban, chức vụ </param>
        /// <param name="userId"> UserId của cán bộ đang bàn giao </param>
        /// <returns> </returns>
        private static IEnumerable<int> GetUsers(IEnumerable<RelationQuery> relationQueries,
                                                 IEnumerable<UserDepartmentPosition> userDeptPos,
                                                 Node relationNode, int userId)
        {
            var results = new List<int>();

            // Lấy không gian cấu hình 
            var oldQueries = relationNode.Address.Single(t => t.NodeFrom == 0).Queries;
            // Nếu chưa cấu hình không gian user của relationNode
            if (!oldQueries.Any())
            {
                return results;
            }

            // Tìm lại phòng ban bàn userId thuộc vào ở relationNode
            // string deptIdExtOfUserId;
            List<string> deptIdExtOfUserIds;
            var hasUser = oldQueries.ContainUser(userId, userDeptPos, out deptIdExtOfUserIds);
            // Nếu userId không thuộc không gian cán bộ relationNode
            if (!hasUser || !deptIdExtOfUserIds.Any())
            {
                return results;
            }

            foreach (var relationQuery in relationQueries)
            {
                foreach (var deptIdExtOfUserId in deptIdExtOfUserIds)
                {
                    var userIds = new List<int>();
                    switch (relationQuery.Relation)
                    {
                        case RelationType.CanBoCungDonVi:
                            userIds =
                                userDeptPos.Where(t => t.DepartmentIdExt == deptIdExtOfUserId
                                                        &&
                                                        (relationQuery.PositionId == -1 ||
                                                         t.PositionId == relationQuery.PositionId))
                                    .Select(t => t.UserId).ToList();
                            break;
                        case RelationType.CanBoCapDuoi:
                            userIds =
                                userDeptPos.Where(t => GetLevel(t.DepartmentIdExt) == GetLevel(deptIdExtOfUserId) + 1
                                                        && t.DepartmentIdExt.Contains(deptIdExtOfUserId + ".")
                                                        &&
                                                        (relationQuery.PositionId == -1 ||
                                                         t.PositionId == relationQuery.PositionId))
                                    .Select(t => t.UserId).ToList();
                            break;
                        case RelationType.CanBoCapTren:
                            userIds =
                                userDeptPos.Where(t => GetLevel(t.DepartmentIdExt) == GetLevel(deptIdExtOfUserId) - 1
                                                        && deptIdExtOfUserId.Contains(t.DepartmentIdExt + ".")
                                                        &&
                                                        (relationQuery.PositionId == -1 ||
                                                         t.PositionId == relationQuery.PositionId))
                                    .Select(t => t.UserId).ToList();
                            break;
                        case RelationType.CanBoCungCap:
                            userIds =
                                userDeptPos.Where(t => GetLevel(t.DepartmentIdExt) == GetLevel(deptIdExtOfUserId)
                                                        &&
                                                        (relationQuery.PositionId == -1 ||
                                                         t.PositionId == relationQuery.PositionId))
                                    .Select(t => t.UserId).ToList();
                            break;
                        case RelationType.CanBoCungNutCha:
                            userIds =
                                userDeptPos.Where(t => GetLevel(t.DepartmentIdExt) == GetLevel(deptIdExtOfUserId)
                                                        &&
                                                        GetParentIdExt(t.DepartmentIdExt) ==
                                                        GetParentIdExt(deptIdExtOfUserId)
                                                        &&
                                                        (relationQuery.PositionId == -1 ||
                                                         t.PositionId == relationQuery.PositionId))
                                    .Select(t => t.UserId).ToList();
                            break;
                    }

                    results.AddRange(userIds);
                }
            }
            return results;
        }

        #endregion
    }
}