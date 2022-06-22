using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Workflow;

namespace Bkav.eGovCloud.Business
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeHelper - public - Core
    /// Access Modifiers:
    ///     *Inherit: None
    /// Create Date : 030413
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Kiểm tra quyền tác động lên loại văn bản đối với 1 cán bộ nào đó.</para>
    /// <para>(CuongNT@bkav.com - 030413)</para>
    /// </summary>
    public class DocTypeHelper
    {
        #region Readonly &  Fields

        // CHeck quyền khởi tạo văn bản
        private static DepartmentBll _departmentServices;

        // Check quyền khởi tạo văn bản
        private static WorkflowBll _workflowServices;

        private static DocTypeBll _doctypeService;

        #endregion Readonly &  Fields

        #region C'tors

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="departmentServices">Bll tương ứng với bảng Department</param>
        /// <param name="workflowServices"> Bll tương ứng với bảng Workflow</param>
        /// <param name="doctypeService"></param>
        public DocTypeHelper(DepartmentBll departmentServices, DocTypeBll doctypeService,
                                        WorkflowBll workflowServices)
        {
            _departmentServices = departmentServices;
            _workflowServices = workflowServices;
            _doctypeService = doctypeService;
        }

        #endregion C'tors

        /// <summary>
        /// <para>Kiểm tra cán bộ (userId) có quyền khởi tạo loại văn bản (docTypeId) hay không.</para>
        /// <para>(CuongNT@bkav.com - 030413)</para>
        /// </summary>
        /// <param name="docTypeId">Loại văn bản.</param>
        /// <param name="userId">Id cán bộ.</param>
        /// <returns>[True] nếu có quyền khởi tạo. [False] nếu ngược lại.</returns>
        public static bool CheckForKhoiTaoVanBan(Guid docTypeId, int userId)
        {
            // Lấy quy trình đang sử dụng của loại hồ sơ
            var workflow = _doctypeService.GetWorkflowActive(docTypeId);
            if (workflow == null || string.IsNullOrEmpty(workflow.Json))
            {
                return false;
            }

            // Lấy các Node có quyền khởi tạo trong quy trình
            var path = workflow.JsonInObject;//Json2.ParseAs<Path>(workflow.Json);
            var userDeptJobs =
                    _departmentServices.GetCacheAllUserDepartmentJobTitlesPosition().Select(
                        t =>
                        new UserDepartmentPosition
                        {
                            Id = t.UserDepartmentJobTitlesPositionId,
                            UserId = t.UserId,
                            DepartmentId = t.DepartmentId,
                            DepartmentIdExt = t.DepartmentIdExt,
                            PositionId = t.PositionId
                        }).ToList();

            return path.CheckQuyenKhoiTao(userId, userDeptJobs);
        }

        /// <summary>
        /// <para>Kiểm tra cán bộ (userId) có quyền khởi tạo loại văn bản có quy trình xử lý (path) hay không.</para>
        /// <para>(CuongNT@bkav.com - 030413)</para>
        /// </summary>
        /// <param name="path">Workflow của loại hồ sơ cần kiểm tra quyền khởi tạo</param>
        /// <param name="userDeptJobs">Danh sách quan hệ Cán bộ - Phòng ban - Chức vụ. Sử dụng tìm kiếm không gian cán bộ trong quy trình.</param>
        /// <param name="userId">Id của cán bộ cần kiểm tra</param>
        /// <returns>[True] nếu có quyền khởi tạo. [False] nếu ngược lại.</returns>
        public static bool CheckForKhoiTaoVanBan(Path path, List<UserDepartmentPosition> userDeptJobs, int userId)
        {
            return path.CheckQuyenKhoiTao(userId, userDeptJobs);
        }
    }
}