using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Objects.CacheObjects;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Action = Bkav.eGovCloud.Core.Workflow.Action;

namespace Bkav.eGovCloud.Controllers
{
	public class WorkflowController : CustomerBaseController
	{
		private readonly DocTypeBll _docTypeService;
		private readonly WorkflowBll _workflowService;
		private readonly ResourceBll _resourceService;
		private readonly DocumentCopyBll _docCopyService;
		private readonly UserBll _userService;
		private readonly DepartmentBll _departmentService;
		private readonly JobTitlesBll _jobTitlesService;

		private readonly DocumentPermissionHelper _documentPermissionHelper;
		private readonly WorkflowHelper _workflowHelper;

		public WorkflowController(DocTypeBll doctypeService, WorkflowBll workflowService,
				ResourceBll resourceService, DocumentCopyBll docCopyService, UserBll userService,
				DepartmentBll departmentService, JobTitlesBll jobTitlesService,
				DocumentPermissionHelper documentPermissionHelper, WorkflowHelper workflowHelper)
		{
			_docTypeService = doctypeService;
			_workflowService = workflowService;
			_resourceService = resourceService;
			_docCopyService = docCopyService;
			_userService = userService;
			_departmentService = departmentService;
			_jobTitlesService = jobTitlesService;

			_documentPermissionHelper = documentPermissionHelper;
			_workflowHelper = workflowHelper;
		}

		#region Xử lý các hướng chuyển

		/// <summary>
		///   Trả về hướng chuyển khi tạo mới hay chỉnh sửa văn bản/hồ sơ.
		/// </summary>
		/// <param name="documentTypeId"> Id của DocType (khi tạo mới) hoặc Document (khi xem chi tiết) </param>
		/// <param name="isPhanloai"> </param>
		/// <returns> Danh sách các hướng chuyển </returns>
		/// <remarks>
		///   Khi hồ sơ đang trong trạng thái dừng xử lý thì chỉ load ra hướng chuyển tới các hướng dừng xử lý khác.
		/// </remarks>
		public JsonResult GetActionsCreate(Guid documentTypeId, bool isPhanloai = false)
		{
			if (documentTypeId.Equals(new Guid()))
			{
				LogException(new ArgumentNullException("documentTypeId", "TransferController.GetActionsCreate"));
				return Json(new { error = "Yêu cầu không hợp lệ. Vui lòng thử lại." }, JsonRequestBehavior.AllowGet);
			}

			// Danh sách các hướng chuyển
			var result = new List<Action>();

			// CuongNT@bkav.com - 200613: Xử lý ủy quyền
			var userSendId = CurrentUserId();

			// docType
			var doctype = _docTypeService.GetFromCache(documentTypeId);
			if (doctype == null || doctype.WorkflowId < 0)
			{
				return Json(new { error = "Không tìm thấy quy trình." }, JsonRequestBehavior.AllowGet);
			}

			var workflow = _workflowService.GetFromCache(doctype.WorkflowId);
			if (!isPhanloai)
			{
				// Hướng chuyển fix cứng khi tạo mới
				result.AddRange(GetActionSpecialsCreate(doctype.CategoryBusinessIdInEnum, workflow, userSendId));
			}

			// Hướng chuyển theo quy trình
			var actionsInWorkflow = _workflowHelper.GetActionsCreate(workflow, userSendId).ToList();

			// Loại bỏ các hướng chuyển đặc biệt trong quy trình: Lưu sổ phát hành, Lưu sổ phát hành nội bộ...
			//QuangP: comment lại do thực tế văn thư vẫn cần những hướng chuyển đặc biệt: tạo mới và phát hành luôn
			// RemoveActionsSpecial(ref actionsInWorkflow);

			result.AddRange(actionsInWorkflow);

			// Xử lý ẩn hiện các hướng chuyển nếu văn bản đang ở trạng thái Dừng xử lý
			RecheckActionsForCreate(workflow, ref result);

			var actions = ParseActions(result, workflow.WorkflowId);
			return Json(actions, JsonRequestBehavior.AllowGet);
		}
        /// <summary>
		///   Trả về hướng chuyển khi tạo mới hay chỉnh sửa văn bản/hồ sơ.
		///   Note: Chỉ gọi khi văn bản là tạo mới, đang chờ xử lý, xin ý kiến
		/// </summary>
		/// <param name="documentCopyId"> Id DocumentCotpy </param>
		/// <param name="isGetSpecials"> Có lây các hướng chuyển đặc biệt hay không</param>
		/// <returns> Danh sách các hướng chuyển </returns>
		/// <remarks>
		///   Khi hồ sơ đang trong trạng thái dừng xử lý thì chỉ load ra hướng chuyển tới các hướng dừng xử lý khác.
		/// </remarks>
		public JsonResult GetActions(int documentCopyId, bool isGetSpecials = true)
        {
            var document = _docCopyService.GetFromCache(documentCopyId, CurrentUserId());
            if (document == null)
            {
                return Json(new { error = "Không tìm thấy văn bản." }, JsonRequestBehavior.AllowGet);
            }

            var result = new List<Action>();
            var workflowId = document.WorkflowId;
            var workflow = _workflowService.GetFromCache(workflowId);


            var currentNodeId = document.NodeCurrentId ?? 0;

            if (currentNodeId == 0)
            {
                LogException(string.Format("documentCopy({0}).NodeCurrent không được phép null với hướng xử lý chính và đồng xử lý", documentCopyId));
                return Json(new { error = "Văn bản không hợp lệ. Lỗi đã được lưu lại và chờ xử lý." },
                    JsonRequestBehavior.AllowGet);
            }

            // Hướng chuyển fix cứng khi xem văn bản
            if (isGetSpecials)
            {
                result.AddRange(GetActionSpecialsEdit(document, (int)document.UserSendId));
            }

            // Hướng chuyển theo quy trình
            var actionInWorkflows = _workflowHelper.GetActionsEdit(workflow, (int)document.UserSendId, currentNodeId);
            result.AddRange(actionInWorkflows);

            //Loại bỏ các hướng chuyển đặc biệt trên form dự kiến chuyển
            if (!isGetSpecials)
            {
                result = result.Where(p => !p.IsSpecial).ToList();
            }

            // Xử lý ẩn hiện các hướng chuyển nếu văn bản đang ở trạng thái Dừng xử lý
            RecheckActionsForEdit(document, workflow, ref result);

            var actions2 = ParseActions(result, workflowId);
            return Json(actions2, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   Trả về hướng chuyển khi tạo mới hay chỉnh sửa văn bản/hồ sơ.
        ///   Note: Chỉ gọi khi văn bản là tạo mới, đang chờ xử lý, xin ý kiến
        /// </summary>
        /// <param name="documentCopyId"> Id DocumentCotpy </param>
        /// <param name="isGetSpecials"> Có lây các hướng chuyển đặc biệt hay không</param>
        /// <returns> Danh sách các hướng chuyển </returns>
        /// <remarks>
        ///   Khi hồ sơ đang trong trạng thái dừng xử lý thì chỉ load ra hướng chuyển tới các hướng dừng xử lý khác.
        /// </remarks>
        public JsonResult GetActionsEdit(int documentCopyId, bool isGetSpecials = true)
		{
			var document = _docCopyService.GetFromCache(documentCopyId, CurrentUserId());
			if (document == null)
			{
				return Json(new { error = "Không tìm thấy văn bản." }, JsonRequestBehavior.AllowGet);
			}

			var result = new List<Action>();
			var workflowId = document.WorkflowId;
			var workflow = _workflowService.GetFromCache(workflowId);

			int userSendId;
			if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(document, CurrentUserId(), out userSendId))
			{
				return Json(new { error = "Không có quyền xử lý văn bản." }, JsonRequestBehavior.AllowGet);
			}

			var currentNodeId = document.NodeCurrentId ?? 0;

			if (currentNodeId == 0)
			{
				if (document.DocumentCopyTypeInEnum != DocumentCopyTypes.XinYKien && document.DocumentCopyTypeInEnum != DocumentCopyTypes.DongXuLy)
				{
					// Xử lý đóng tab ... ở client
					LogException(string.Format("documentCopy({0}).NodeCurrent không được phép null với hướng xử lý chính và đồng xử lý", documentCopyId));
					return Json(new { error = "Văn bản không hợp lệ. Lỗi đã được lưu lại và chờ xử lý." },
						JsonRequestBehavior.AllowGet);
				}

				if (document.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
				{
					result.AddRange(GetActionSpecialsXyk(document.Histories, userSendId));
					var actions = ParseActions(result, workflowId);
					return Json(actions, JsonRequestBehavior.AllowGet);
				}

				if (document.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
				{
					// Trường hợp văn bản đồng xử lý nhưng chọn ở dưới phần thông báo.
					// Khi đó những văn bản sinh ra ở trường hợp này ko thuộc node nào trong quy trình.
					// Phải gán lại là node khởi tạo cho người hiện tại.
					var startNodes = _workflowHelper.GetStartNodes(workflow, userSendId);
					if (startNodes.Any())
					{
						currentNodeId = startNodes.First().Id;
					}
				}
			}

			if (currentNodeId == 0)
			{
				LogException(string.Format("documentCopy({0}).NodeCurrent không được phép null với hướng xử lý chính và đồng xử lý", documentCopyId));
				return Json(new { error = "Văn bản không hợp lệ. Lỗi đã được lưu lại và chờ xử lý." },
					JsonRequestBehavior.AllowGet);
			}

			// Hướng chuyển fix cứng khi xem văn bản
			if (isGetSpecials)
			{
				result.AddRange(GetActionSpecialsEdit(document, userSendId));
			}

			// Hướng chuyển theo quy trình
			var actionInWorkflows = _workflowHelper.GetActionsEdit(workflow, userSendId, currentNodeId);
			result.AddRange(actionInWorkflows);

			//Loại bỏ các hướng chuyển đặc biệt trên form dự kiến chuyển
			if (!isGetSpecials)
			{
				result = result.Where(p => !p.IsSpecial).ToList();
			}

			// Xử lý ẩn hiện các hướng chuyển nếu văn bản đang ở trạng thái Dừng xử lý
			RecheckActionsForEdit(document, workflow, ref result);

			var actions2 = ParseActions(result, workflowId);
			return Json(actions2, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Lấy các hướng chuyển của phần dự kiến chuyển
		/// </summary>
		/// <param name="workflowId">Id quy trình</param>
		/// <param name="userId">Id cán bộ dự kiến chuyển</param>
		/// <param name="currentNodeId">Id node hướng chuyển hiện tại</param>
		/// <returns></returns>
		public JsonResult GetActionsTransferPlan(int workflowId, int userId, int currentNodeId)
		{
			var workflow = _workflowService.GetFromCache(workflowId);
			var actionInWorkflows = _workflowHelper.GetActionsEdit(workflow, userId, currentNodeId).Where(a => !a.IsSpecial);
			var result = ParseActions(actionInWorkflows, workflowId);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Lấy ra danh sách người dùng theo hướng chuyển.
		/// </summary>
		/// <param name="actionId">Id hướng chuyển.</param>
		/// <param name="workflowId">Id quy trình đang sử dụng.</param>
		/// <param name="documentCopyId"> Id hướng xử lý văn bản. Bằng 0 khi bàn giao văn bản tạo mới. </param>
		/// <param name="userId">Id người chuyển (tham số này chỉ dùng khi muốn lấy ra hướng chuyển đối với người dự kiến chuyển)</param>
		/// <returns>Json object danh sách các user theo hướng chuyển..</returns>
		public JsonResult GetUserByAction(string actionId, int workflowId, int documentCopyId, int? userId = null)
		{
			var isCreating = documentCopyId <= 0;

			int userSendId;
			userSendId = userId.HasValue ? userId.Value : CurrentUserId();

			if (!isCreating)
			{
				var document = _docCopyService.GetFromCache(documentCopyId, userId ?? 0);
				userSendId = userId.HasValue ? userId.Value : document.UserCurrentId;

				////TienBV: chổ này check có vẻ không cần thiết. Bỏ để hạn chế request vào database.
				//if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopyId, CurrentUserId(), out userSendId))
				//{
				//    return Json(new { error = "Bạn không có quyền xử lý văn bản!" }, JsonRequestBehavior.AllowGet);
				//}
			}

			var userByAction = GetUserByAction(workflowId, actionId, userSendId);

			return Json(userByAction, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// HopCV:150315
		/// Lấy ra danh sách người dùng theo hướng chuyển.
		/// </summary>
		/// <param name="actionId">Id hướng chuyển.</param>
		/// <param name="workflowId">Id quy trình đang sử dụng.</param>
		/// <param name="documentCopyIds">Danh sách Id hướng xử lý văn bản</param>
		/// <returns>Json object danh sách các user theo hướng chuyển..</returns>
		public JsonResult GetUserByActionTheoLo(string actionId, int workflowId, List<int> documentCopyIds)
		{
			if (documentCopyIds == null || !documentCopyIds.Any())
			{
				return Json(new { error = "Bạn không có quyền xử lý văn bản!" }, JsonRequestBehavior.AllowGet);
			}

			var documentCopys = _docCopyService.Gets(p => documentCopyIds.Contains(p.DocumentCopyId));
			if ((documentCopys == null || !documentCopys.Any())
				|| documentCopys.Count() != documentCopyIds.Count)
			{
				return Json(new { error = "Trong danh sách có văn bản không tồn tại. Vui lòng thử lại." });
			}

			List<int> userSendIds;
			if (!CheckForUyQuyenVaXuLyVanBan(documentCopys, CurrentUserId(), out userSendIds))
			{
				return Json(new { error = "Trên danh sách văn bản đã chọn có văn bản bạn không có quyền xử lý!" });
			}

			//Check diều kiện nếu lơn 1 1 người xử lý thì không xử lý
			userSendIds = userSendIds.Distinct().ToList();
			if (userSendIds.Count > 1)
			{
				return Json(new { error = "Không thể thao tác chuyển theo lô!" });
			}

			int userSendId = userSendIds.First();
			var actions = GetUserByAction(workflowId, actionId, userSendId);
			return Json(actions, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// HopCV: 250315
		/// Lấy các hướng chuyển theo lô
		/// </summary>
		/// <param name="documentCopyIds">Danh sách id văn bản copy</param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetActionTheoLoVanBan(List<int> documentCopyIds)
		{
			if (documentCopyIds == null || !documentCopyIds.Any())
			{
				return Json(new { error = "Yêu cầu không hợp lệ. Vui lòng thử lại." });
			}

			var documentCopys = _docCopyService.Gets(p => documentCopyIds.Contains(p.DocumentCopyId));
			if ((documentCopys == null || !documentCopys.Any())
				|| documentCopys.Count() != documentCopyIds.Count)
			{
				return Json(new { error = "Trong danh sách có văn bản không tồn tại. Vui lòng thử lại." });
			}

			List<int> userSendIds = null;
			if (!CheckForUyQuyenVaXuLyVanBan(documentCopys, CurrentUserId(), out userSendIds))
			{
				return Json(new { error = "Trên danh sách văn bản đã chọn có văn bản bạn không có quyền xử lý!" });
			}

			//Lấy ra quy trinh của văn bản
			var workflowIds = documentCopys.Select(p => p.WorkflowId).Distinct();
			if (workflowIds == null || !workflowIds.Any())
			{
				return Json(new { error = "Không tìm thấy quy trình." });
			}

			//Kiểm tra xem các văn bản có cùng thuộc 1 quy trình hay không?
			if (workflowIds.Count() > 1)
			{
				return Json(new { error = "Danh sách văn bản không cùng thuộc một quy trình." });
			}

			var nodeIds = documentCopys.Where(p => p.NodeCurrentId.HasValue).Select(p => p.NodeCurrentId.Value).Distinct();
			if (nodeIds == null || !nodeIds.Any())
			{
				return Json(new { error = "Tìm thấy hướng chuyển hiện tại." });
			}

			//Kiểm tra xem các văn bản có cùng thuộc 1 node hay không?
			if (nodeIds.Count() > 1)
			{
				return Json(new { error = "Danh sách văn bản không cùng thuộc một node." });
			}

			var workflowId = workflowIds.First();//Lấy ra id quy trình
			var workflow = _workflowService.GetFromCache(workflowId);
			var currentNodeId = nodeIds.First();
			var userSendId = userSendIds.First();
			// Danh sách các hướng chuyển
			var result = new List<Action>();
			var actionInWorkflows = _workflowHelper.GetActionsEdit(workflow, userSendId, currentNodeId);
			result.AddRange(actionInWorkflows);
			var actions = ParseActions(result, workflowId);

			return Json(actions);
		}

		#endregion

		#region Private Methods

		private dynamic ParseActions(IEnumerable<Action> listActions, int workflowId)
		{
			return listActions.Select(
					 t => new
					 {
						 id = t.Id,
						 workflowId = workflowId, //t.WorkflowId != 0 ? t.WorkflowId : 
						 name = t.Name,
						 userIdNext = t.UserIdNext,
						 isSpecial = t.IsSpecial,
						 nextNodeId = t.Next,
						 currentNodeId = t.Current,
						 isAllow = t.IsAllow,
						 isAllowSign = t.IsAllowSign,
						 priority = GetActionPriority(t.Id, t.IsSpecial)
					 }
			 );
		}

		private IEnumerable<Action> GetActionSpecialsXyk(HistoryProcess historyProcess, int userSendId)
		{
			var result = new List<Action>();

			if (!historyProcess.HistoryPath.Any())
			{
				return result;
			}

			var user = _userService.GetFromCache(historyProcess.HistoryPath.Last().UserSendId);
			if (user != null)
			{
				result.Add(new Action
				{
					Id = ActionSpecial.ChuyenYKienDongGopVbXinYKien.ToString(),
					Name = string.Format("Chuyển ý kiến cho: {0}({1})", user.FullName, user.Username),
					IsSpecial = true,
					UserIdNext = 0, // Không có thật
					Next = 0, // Không có thật
					WorkflowId = 0 // Không có thật
				});
			}
			return result;
		}

		/// <summary>
		///   <para> Trả về các hướng chuyển fix cứng khi xem chi tiết văn bản/hồ sơ </para>
		///   <para> CuongNT@bkav.com - 120413 </para>
		/// </summary>
		/// <param name="document"> </param>
		/// <param name="userSendId"></param>
		/// <returns> </returns>
		/// <remarks>
		///   Xử lý các trường hợp có hướng chuyển đặc biệt.
		///   1 số hướng chuyển đặc biệt được load khi cấu hình mặc định trên Node, 1 số còn lại có khi mở văn bản/hồ sơ đã có(edit).
		///   5 hướng chuyển không cần xác định nơi nhận
		/// </remarks>
		private IEnumerable<Action> GetActionSpecialsEdit(DocumentCached document, int userSendId)
		{
			var result = new List<Action>();

			#region Văn bản "Chờ cập nhật kết quả dừng"

			if (EnumHelper<DocumentCopyTypes>.ContainFlags(document.DocumentCopyTypeInEnum,
														   DocumentCopyTypes.ChoKetQuaDungXuLy))
			{
				result.AddRange(new List<Action>
					{
						new Action
							{
								Id = ActionSpecial.CapNhatKetQuaDungXuLy.ToString(),
								Name = "Cập nhật kết quả dừng xử lý",
								IsSpecial = true,
								UserIdNext = 0, // Không có thật
                                Next = 0, // Không có thật
                                WorkflowId = 0 // Không có thật
                            }
					});
				return result;
			}

			#endregion

			#region Văn bản "Đang dừng xử lý"

			var isSupplementary = document.StatusInEnum == DocumentStatus.DungXuLy;
			if (isSupplementary && document.StatusInEnum == DocumentStatus.DungXuLy)
			{
				result.AddRange(new List<Action>
					{
						new Action
							{
								Id = ActionSpecial.TiepTucXuLy.ToString(),
								Name = "Tiếp tục xử lý",
								IsSpecial = true,
								UserIdNext = 0, // Không có thật
                                Next = 0, // Không có thật
                                WorkflowId = 0 // Không có thật
                            }
					});
				return result;
			}

			#endregion

			#region Văn bản "Đang xem chi tiết"

			var historyProcess = document.Histories;
			var allUsers = _userService.GetAllCached();

			// Hướng chuyển: Chuyển người khởi tạo
			if (document.UserCreatedId != document.UserCurrentId &&
				document.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh && historyProcess.HistoryPath.Any())
			{
				// Người đầu tiên trong history hướng chính là người khởi tạo
				var fistHistory = historyProcess.HistoryPath.First();
				var userCreate = allUsers.SingleOrDefault(u => u.UserId == fistHistory.UserSendId);
				result.Add(new Action
				{
					Id = ActionSpecial.ChuyenNguoiKhoiTao.ToString(),
					Name = string.Format("{0}: {1} ({2})", "Chuyển người khởi tạo", userCreate.FullName, userCreate.Username),
					IsSpecial = true,
					UserIdNext = fistHistory.UserSendId,
					Next = fistHistory.NodeSendId,
					WorkflowId = fistHistory.WorkflowSendId,
					Current = document.NodeCurrentId ?? 0
				});
			}

			int? parentCurrentUserId = null;
			// Hướng chuyển: Chuyển ý kiến đóng góp
			if (document.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
			{
				result.Add(new Action
				{
					Id = ActionSpecial.ChuyenYKienDongGopVbDxl.ToString(),
					Name = "Chuyển ý kiến đến hướng Xử lý chính và Kết thúc văn bản",
					IsSpecial = true,
					UserIdNext = 0, // Không có thật
					Next = 0, // Không có thật
					WorkflowId = 0 // Không có thật
				});
			}

			// Hướng chuyển: Trả lời ý kiên đóng góp (Xin ý kiến)
			if (document.DocumentCopyTypeInEnum == DocumentCopyTypes.XinYKien)
			{
				if (!parentCurrentUserId.HasValue)
				{
					parentCurrentUserId = _docCopyService.GetAs(d => d.UserCurrentId, document.ParentId.Value);
				}

				var user = allUsers.Single(u => u.UserId == parentCurrentUserId.Value);
				result.Add(new Action
				{
					Id = ActionSpecial.ChuyenYKienDongGopVbXinYKien.ToString(),
					Name = string.Format("Chuyển ý kiến cho: {0} ({1})", user.FullName, user.Username),
					IsSpecial = true,
					UserIdNext = 0, // Không có thật
					Next = 0, // Không có thật
					WorkflowId = 0 // Không có thật
				});
			}

			// Hướng chuyển: Trả lại người vừa gửi
			/*
             TienBV: Với những văn bản đồng xử lý, để tránh việc rất nhiều người gửi nhầm hướng người khởi tạo
             * sẽ sinh ra công văn không đi đúng hướng mong muốn là chuyển ý kiến đóng góp nên sẽ bỏ các hướng này ở văn bản
             * đồng xử lý.
             */
			if (historyProcess.HistoryPath.Any())
			{
				var lastHistory = historyProcess.HistoryPath.Last();
				if (lastHistory.UserReceiveId != lastHistory.UserSendId && document.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh)
				{
					var user = allUsers.Single(u => u.UserId == lastHistory.UserSendId);
					result.Add(new Action
					{
						Id = ActionSpecial.ChuyenNguoiGui.ToString(),
						Name = string.Format("Chuyển người gửi: {0} ({1})", user.FullName, user.Username),
						IsSpecial = true,
						UserIdNext = lastHistory.UserSendId,
						Next = lastHistory.NodeSendId,
						WorkflowId = lastHistory.WorkflowSendId,
						Current = document.NodeCurrentId ?? 0
					});
				}
			}

			return result;

			#endregion
		}

		private dynamic GetUserByAction(int workflowId, string actionId, int userId)
		{
			var listUserIdByAction = _workflowHelper.GetUsersByActionId(workflowId, actionId, userId);
			var departmentsUser = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
			var departments = _departmentService.GetCacheAllDepartments();
			var jobtitles = _jobTitlesService.GetCacheAllJobtitles();

			var userByAction = _userService.GetAllCached(true)
									.Where(u => listUserIdByAction.Contains(u.UserId))
									.Select(u =>
									{
										var dept = departmentsUser.Where(d => d.UserId == u.UserId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();
										var deptName = string.Empty;

										var jobtitle = string.Empty;
										if (dept != null)
										{
											var dept1 = departments.SingleOrDefault(d => d.DepartmentId == dept.DepartmentId);
											deptName = dept1 == null ? string.Empty : dept1.DepartmentName;

											//var pos = positions.SingleOrDefault(p => p.PositionId == dept.PositionId);
											//position = pos == null ? string.Empty : pos.PositionName;

											var jobt = jobtitles.SingleOrDefault(j => j.JobTitlesId == dept.JobTitlesId);
											jobtitle = jobt == null ? string.Empty : jobt.JobTitlesName;
										}

										return new
										{
											value = u.UserId,
											label = string.Format("{0} - {1}", u.Username, u.FullName),
											fullname = u.FullName,
											name = u.FirstName,
											department = deptName,
											username = u.Username,
											avatar = u.Avatar,
											position = jobtitle
										};
									})
									.OrderBy(u => u.username);
			return userByAction;
		}

		/// <summary>
		///   Lấy mức ưu tiên của action (dùng khi sắp xếp danh sách hướng chuyển)
		/// </summary>
		/// <param name="actionId"> </param>
		/// <param name="isSpecial"> </param>
		private int GetActionPriority(string actionId, bool isSpecial)
		{
			var result = 0;
			if (!isSpecial)
			{
				result = 2;
			}
			else
			{
				if (actionId == ActionSpecial.ChuyenYKienDongGopVbDxl.ToString()
					|| actionId == ActionSpecial.ChuyenYKienDongGopVbXinYKien.ToString()
					|| actionId == ActionSpecial.ChuyenNguoiCoQuyenDongGopYKien.ToString()
					|| actionId == ActionSpecial.TiepNhanHoSo.ToString()
					|| actionId == ActionSpecial.TiepNhanHoSoVaTiepTuc.ToString()
					|| actionId == ActionSpecial.TiepTucXuLy.ToString()
					|| actionId == ActionSpecial.CapNhatKetQuaDungXuLy.ToString())
				{
					result = 1;
				}

				if (actionId == ActionSpecial.ChuyenNguoiGui.ToString()
					|| actionId == ActionSpecial.ChuyenNguoiKhoiTao.ToString())
				{
					result = 3;
				}

				if (actionId == ActionSpecial.LuuSoNoiBo.ToString()
					|| actionId == ActionSpecial.LuuSoVaPhatHanhNoiBo.ToString()
					|| actionId == ActionSpecial.LuuSoVaPhatHanhRaNgoai.ToString())
				{
					result = 4;
				}
			}
			return result;
		}

		/// <summary>
		///   <para> Trả về các hướng chuyển fix cứng khi tạo mới văn bản/hồ sơ </para>
		///   <para> CuongNT@bkav.com - 120413 </para>
		/// </summary>
		/// <param name="categoryBusinessTypes"> Kiểu văn bản (Một cửa, Văn bản đi, Văn bản đến...) </param>
		/// <param name="workflow"> Quy trình </param>
		/// <param name="userId"> Cán bộ đang xử lý văn bản </param>
		/// <returns> </returns>
		private IEnumerable<Action> GetActionSpecialsCreate(CategoryBusinessTypes categoryBusinessTypes, Workflow workflow,
															int userId)
		{
			var result = new List<Action>();
			if (categoryBusinessTypes == CategoryBusinessTypes.VbDen ||
					categoryBusinessTypes == CategoryBusinessTypes.VbDi)
			{
				// Hiện chưa có hướng chuyển fix cứng nào cho văn bản khi tạo mới
				return result;
			}

			// Check quyền với loại hồ sơ khi tạo mới
			var startNodes = _workflowHelper.GetStartNodes(workflow, userId);
			if (startNodes == null || !startNodes.Any())
			{
				return result;
			}

			var startNode = startNodes.First();

#if HoSoMotCuaEdition
			//if (categoryBusinessTypes == CategoryBusinessTypes.Hsmc)
			//{
			//	result.Add(new Action
			//	{
			//		Id = ActionSpecial.TiepNhanHoSo.ToString(),
			//		Name = _resourceService.GetEnumDescription<ActionSpecial>(ActionSpecial.TiepNhanHoSo),
			//		IsSpecial = true,
			//		UserIdNext = CurrentUserId(), // Mặc định là người đăng nhập tiếp nhận
			//		Next = startNode.Id,
			//		WorkflowId = workflow.WorkflowId
			//	});
			//	result.Add(new Action
			//	{
			//		Id = ActionSpecial.TiepNhanHoSoVaTiepTuc.ToString(),
			//		Name = _resourceService.GetEnumDescription<ActionSpecial>(ActionSpecial.TiepNhanHoSoVaTiepTuc),
			//		IsSpecial = true,
			//		UserIdNext = CurrentUserId(), // Mặc định là người đăng nhập tiếp nhận
			//		Next = startNode.Id,
			//		WorkflowId = workflow.WorkflowId
			//	});
			//}
#endif

			return result;
		}

		/// <summary>
		///   <para> Ẩn các hướng chuyển thông thường, chỉ hiện các hướng chuyển dừng xử lý chưa bàn giao tới </para>
		///   <para> CuongNT@bkav.com - 270613 </para>
		/// </summary>
		/// <param name="workflow"> </param>
		/// <param name="actions"> Danh sách hướng chuyển cần xử lý hiển thị </param>
		private void RecheckActionsForCreate(Workflow workflow, ref List<Action> actions)
		{
			// Phần xử lý Transfer đã tính xử lý luôn cả trường hợp khi tạo mới thì dừng xử lý luôn được. -
			//-> Xem xét có loại bỏ các hướng này khi khởi tạo văn bản hay không.
			RemoveActionsForSupplementary(workflow, ref actions);
		}

		/// <summary>
		///   <para> Ẩn các hướng chuyển thông thường, chỉ hiện các hướng chuyển dừng xử lý chưa bàn giao tới </para>
		///   <para> CuongNT@bkav.com - 270613 </para>
		/// </summary>
		/// <param name="document"> </param>
		/// <param name="workflow"></param>
		/// <param name="actions"> Danh sách hướng chuyển cần xử lý hiển thị </param>
		private void RecheckActionsForEdit(DocumentCached document, Workflow workflow, ref List<Action> actions)
		{
			// Nếu là văn bản đồng xử lý, thì loại bỏ toàn bộ các hướng chuyển ra các nút Dừng xử lý hoặc Tiếp nhận bổ sung.
			// Chỉ có văn bản chính mới được chuyển ra các hướng chuyển này.
			if (document.DocumentCopyTypeInEnum == DocumentCopyTypes.DongXuLy)
			{
				RemoveActionsForSupplementary(workflow, ref actions);
			}

			// Nếu là văn bản chính, và đang ở trạng thái dừng xử lý thì chỉ enable các hướng chuyển tới các nút Dừng xử lý, hoặc Tiếp nhận bổ sung mà chưa từng nhận văn bản dừng xử lý.
			if (document.DocumentCopyTypeInEnum == DocumentCopyTypes.XuLyChinh)
			{
				if (document.StatusInEnum == DocumentStatus.DungXuLy &&
					document.StatusInEnum == DocumentStatus.DungXuLy)
				{
					DisableActionsForSupplementing(document.DocumentCopyId, workflow, ref actions);
				}
			}
			// Phân loại văn bản? --> Xử lý giống khi thêm mới
		}

		/// <summary>
		///   Loại bỏ các hướng chuyển tới các nút "Dừng xử lý" hoặc "Tiếp nhận bổ sung"
		/// </summary>
		/// <param name="workflow"> </param>
		/// <param name="actions"> </param>
		private void RemoveActionsForSupplementary(Workflow workflow, ref List<Action> actions)
		{
			var path = workflow.JsonInObject;
			var removeActions = new List<Action>();
			foreach (var action in actions)
			{
				if (action.IsSpecial)
				{
					continue;
				}
				var nextNode = path.GetNode(action.Next);
				if (nextNode == null)
				{
					throw new WorkflowFormatException("action.Next bị null.");
				}
				var nextNodePermission = nextNode.GetNodePermission();
				var isRemove =
					EnumHelper<NodePermissions>.ContainFlags(nextNodePermission, NodePermissions.QuyenDungXuLy) ||
					EnumHelper<NodePermissions>.ContainFlags(nextNodePermission, NodePermissions.QuyenTiepNhanBoSung);
				if (isRemove)
				{
					removeActions.Add(action);
				}
			}
			foreach (var removeAction in removeActions)
			{
				actions.Remove(removeAction);
			}
		}

		/// <summary>
		///   <para> Enable các hướng chuyển Dừng xử lý, Tiếp nhận bổ sung mà chưa nhận văn bản gửi tới </para>
		///   <para> Disable các hướng chuyển thông thường, và các hướng chuyển ra các nút Dừng xử lý, Tiếp nhận bổ sung mà chưa nhận văn bản gửi tới </para>
		/// </summary>
		/// <param name="documentCopy"> </param>
		/// <param name="workflow"> </param>
		/// <param name="actions"> </param>
		private void DisableActionsForSupplementing(int documentCopyId, Workflow workflow,
													ref List<Action> actions)
		{
			var sentNodes =
				_docCopyService.GetChildren(documentCopyId, DocumentCopyTypes.ChoKetQuaDungXuLy)
					.Select(n => n.NodeCurrentId ?? 0).ToList();
			var path = workflow.JsonInObject; //Json2.ParseAs<Path>(workflow.Json);
			foreach (var action in actions)
			{
				if (action.IsSpecial)
				{
					// Các hướng chuyển đặc biệt: Lưu sổ phát hành... thì không xét ở đây.
					continue;
				}
				var nextNode = path.GetNode(action.Next);
				if (nextNode == null)
				{
					throw new Exception("Node đến không tồn tại, liên hệ quản trị để kiểm tra lại quy trình.");
				}
				var nextNodePermission = nextNode.GetNodePermission();
				action.IsAllow =
					EnumHelper<NodePermissions>.ContainFlags(nextNodePermission, NodePermissions.QuyenDungXuLy) &&
					!EnumHelper<NodePermissions>.ContainFlags(nextNodePermission, NodePermissions.QuyenTiepNhanBoSung) &&
					!sentNodes.Contains(nextNode.Id);
			}
		}

		/// <summary>
		/// HopCV: 250315
		/// Kiểm tra quyền xử lý văn bản của người dùng trên nhiều văn bản
		/// </summary>
		/// <param name="documentCopys">Danh sách văn bản</param>
		/// <param name="userId">id người kiểm tra</param>
		/// <param name="userSendIds">Danh sách người có quyền xử lý</param>
		/// <returns></returns>
		private bool CheckForUyQuyenVaXuLyVanBan(IEnumerable<DocumentCopy> documentCopys, int userId, out List<int> userSendIds)
		{
			if (documentCopys == null || !documentCopys.Any())
			{
				userSendIds = new List<int>();
				return false;
			}

			var tmpUserSendIds = new List<int>();
			int userSendId;
			foreach (var documentCopy in documentCopys)
			{
				if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, userId, out userSendId))
				{
					userSendIds = new List<int>();
					return false;
				}
				tmpUserSendIds.Add(userSendId);
			}

			userSendIds = tmpUserSendIds;
			return true;
		}

		/// <summary>
		/// todo: Thánh nào làm chỗ này đây
		/// Truyền tham số ref  rồi remove không trả về giá trị gì thì có ý nghĩa quái j nữa
		///   Loại bỏ các hướng chuyển đặc biệt khi tạo mới
		/// </summary>
		/// <param name="actions"> </param>
		private void RemoveActionsSpecial(ref List<Action> actions)
		{
			var removeActions = actions.Where(action => action.IsSpecial).ToList();
			foreach (var removeAction in removeActions)
			{
				actions.Remove(removeAction);
			}
		}

		private int CurrentUserId()
		{
			return _userService.CurrentUser.UserId;
		}

		#endregion
	}
}