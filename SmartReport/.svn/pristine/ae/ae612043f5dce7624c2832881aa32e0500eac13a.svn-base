using System;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer.eDoc;

namespace Bkav.eGovCloud.Controllers
{
	public class CommonController : CustomerBaseController
	{
		private readonly DepartmentBll _departmentService;
		private readonly UserBll _userService;
		private readonly JobTitlesBll _jobTitlesService;
		private readonly PositionBll _positionService;
		private readonly AddressBll _addressService;
		private readonly DocumentBll _documentService;
		private readonly CodeBll _codeService;
		private readonly StoreBll _storeService;
		private readonly Helper.UserSetting _helperUserSetting;
		private readonly KeyWordBll _keyWordService;
		private readonly CategoryBll _categoryService;
		private readonly ResourceBll _resourceService;
		private readonly DocFieldBll _docfieldService;
		private readonly DocTypeBll _doctypeService;


		public CommonController(DepartmentBll departmentService, UserBll userService,
					JobTitlesBll jobTitleService, PositionBll positionService,
					AddressBll addressService, DocumentBll documentService,
					CodeBll codeService, StoreBll storeService,
					Helper.UserSetting helperUserSetting,
					KeyWordBll keyworkService, CategoryBll categoryService,
					ResourceBll resourceService, DocFieldBll docfieldService, DocTypeBll doctypeService)
		{
			_departmentService = departmentService;
			_userService = userService;
			_jobTitlesService = jobTitleService;
			_positionService = positionService;
			_addressService = addressService;
			_documentService = documentService;
			_codeService = codeService;
			_storeService = storeService;
			_keyWordService = keyworkService;
			_categoryService = categoryService;
			_resourceService = resourceService;
			_docfieldService = docfieldService;
			_doctypeService = doctypeService;
			_helperUserSetting = helperUserSetting;
		}

		#region Lấy dữ liệu cho các Form bàn giao, thông báo, phát hành, lưu sổ

		/// <summary>
		///   <para> Trả về danh sách phòng ban trong cơ quan </para>
		/// </summary>
		/// <returns>Json object danh sách tất cả các phòng ban trong cơ quan.</returns>
		public JsonResult GetAllDepartment()
		{
			var result = _departmentService
				.GetCacheAllDepartments(true)
				.Select(u => new
				{
					value = u.DepartmentId,
					parentid = u.ParentId.HasValue ? u.ParentId : 0,
					data = u.DepartmentName,
					metadata = new { id = u.DepartmentId },
					idext = u.DepartmentIdExt,
					state = "leaf",
					order = u.Order,
					level = u.Level,
					attr = new { id = u.DepartmentId, rel = "dept", idext = u.DepartmentIdExt, label = u.DepartmentPath },
					label = u.DepartmentLabel,
                    edocId = u.Emails
				});

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		///   Trả về danh sách user trong cơ quan
		/// </summary>
		/// <returns>Json object danh sách tất cả user.</returns>
		public JsonResult GetAllUsers()
		{
			var result = _userService.GetAllCached(true)
				.Select(u => new
				{
					value = u.UserId,
					label = u.Username + " - " + u.FullName,
					fullname = u.FullName,
					username = u.Username,
					avatar = u.Avatar
				})
				.OrderBy(u => u.username);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh sách tất cả chức danh
		/// </summary>
		/// <returns>Json object tất cả các chức danh</returns>
		public JsonResult GetAllJobTitlies()
		{
			var result = _jobTitlesService.GetCacheAllJobtitles()
				.Select(u => new { value = u.JobTitlesId, label = u.JobTitlesName, isApprover = u.IsApproved });
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh sách tất cả chức vụ
		/// </summary>
		/// <returns>Json object tất cả các chức vụ</returns>
		public JsonResult GetAllPosition()
		{
			var result = _positionService.GetCacheAllPosition()
				.Select(u => new { value = u.PositionId, label = u.PositionName, level = u.PriorityLevel, isApprover = u.IsApproved });
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh sách tất cả các quan hệ người dùng - phòng ban - chức vụ.
		/// </summary>
		/// <returns>Json object danh sách quan hệ người dùng - phòng ban - chức vụ.</returns>
		public JsonResult GetAllUserDepartmentJobTitlesPosition()
		{
			var result = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
				t =>
					new
					{
						departmentid = t.DepartmentId,
						userid = t.UserId,
						positionid = t.PositionId,
						idext = t.DepartmentIdExt,
						jobtitleid = t.JobTitlesId,
						hasReceiveDocument = t.HasReceiveDocument
					});
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		///   Trả về danh sách cơ quan nhận phát hành.
		/// </summary>
		/// <returns>Json object danh sách các cơ quan nhận phát hành.</returns>
		public JsonResult GetAllAddress()
		{
			var result = _addressService.GetsAs(a => new
			{
				AddressId = a.AddressId,
				Name = a.Name,
				EdocId = a.EdocId,
				AddressString = a.AddressString,
				GroupName = a.GroupName,
				// LevelEdocId = a.LevelEdocId,
				ParentId = a.ParentId
			}).Take(500);

			result = result.OrderBy(a => a.GroupName);

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh sách các mã hồ sơ theo sổ.
		/// </summary>
		/// <param name="storeId">Id của sổ được chọn.</param>
		/// <param name="categoryId"></param>
		/// <returns>Danh sách các mã hồ sơ mới nhất.</returns>
		/// <remarks>
		/// Đang sử dụng cho cấp số phát hành và phát hành nội bộ
		/// </remarks>
		public JsonResult GetCodes(int storeId, Guid? documentId, int? categoryId = null)
		{
			if (!categoryId.HasValue)
			{
				if (!documentId.HasValue)
				{
					return Json(null, JsonRequestBehavior.AllowGet);
				}

				var cateId = _documentService.GetAs(documentId.Value, p => p.CategoryId);
				if (!cateId.HasValue)
				{
					return Json(null, JsonRequestBehavior.AllowGet);
				}
				categoryId = cateId;
			}

			var codeIds = _codeService.GetCodeIds(storeId, categoryId.Value);
			if (codeIds == null || !codeIds.Any())
			{
				return Json(null, JsonRequestBehavior.AllowGet);
			}

			var templates = _codeService.GetCodes(codeIds, DateTime.Now, CategoryBusinessTypes.VbDi, isDocCode: true, storeId: storeId);

			return Json(templates.Select(c => new
			{
				Template = c.Value,
				CodeId = c.Key,
			}), JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh sách các Sổ văn bản trong loại hồ sơ.
		/// </summary>
		/// <param name="docTypeId">Id loại hồ sơ cần lấy.</param>
		/// <returns>Json object danh sách các sổ văn bản thuộc loại hồ sơ.</returns>
		public JsonResult GetStores(Guid? docTypeId = null, int? categoryId = null)
		{
			var userId = _userService.CurrentUser.UserId;
			dynamic listStores = null;
			var result = new List<Business.Objects.CacheObjects.StoreCached>();
			var userSettings = _helperUserSetting.GetUserCurrentSetting();

			if (docTypeId.HasValue)
			{
				var doctype = _doctypeService.GetFromCache(docTypeId.Value);
				if (doctype != null)
				{
					result.AddRange(doctype.Stores.Where(s => s.ListUserViewIds.Contains(userId)));
					if (categoryId.HasValue)
					{
						var categoryCodeIds = _categoryService.GetsAs(categoryId.Value, c => c.CodeId).ToList();
						result = result.Where(s => s.CodeIds.Any(i => categoryCodeIds.Contains(i))).ToList();
					}
				}

				int? storeId = null;
				if (userSettings.StoreIds.ContainsKey(docTypeId.Value))
				{
					storeId = userSettings.StoreIds[docTypeId.Value];
				}

				if (storeId.HasValue)
				{
					result = result.OrderBy(s => s.StoreId == storeId.Value).ToList();
				}
			}
			else
			{
				result.AddRange(_storeService.GetsByUser(userId));
			}
			
			listStores = result.Select(s => new
			{
				StoreId = s.StoreId,
				StoreName = s.StoreName,
				DepartmentId = s.DepartmentId
			});

			return Json(listStores, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh sách từ khóa trong hệ thống.
		/// </summary>
		/// <returns>Json object các </returns>
		public JsonResult GetKeywords()
		{
			var result = _keyWordService.Gets();
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về danh mục thể loại văn bản
		/// </summary>
		/// <returns> </returns>
		public JsonResult GetCategories()
		{
			return Json(_categoryService.GetsFromCache().ToList(), JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// </summary>
		/// <returns> </returns>
		public JsonResult GetDepartmentsByUser()
		{
			return Json(_departmentService.GetsPath(CurrentUserId()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
		/// </summary>
		/// <returns> </returns>
		public JsonResult GetCurrentDepartments()
        {
            return Json(_departmentService.GetsCurrentDepartEdoc(CurrentUserId()).Select(d=> new { DepartmentName = d.DepartmentName, EdocId = d.Emails}), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy list danh sách hình thức gửi
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSendTypes()
		{
			return Json(_resourceService.EnumToSelectList<SendType>(), JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public JsonResult GetDocField()
		{
			var ressult = _docfieldService.GetsAs(s => new { s.DocFieldId, s.DocFieldName }, s => s.IsActivated);
			return Json(ressult, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetDeptAndUsers()
		{
			var depts = _departmentService.GetCacheAllDepartments(true);
			var userDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
			var allUsers = _userService.GetAllCached();

			var results = depts.Select(d =>
			{
				var userIdInDept = userDepts.Where(ud => ud.DepartmentId == d.DepartmentId).Select(ud => ud.UserId);
				var userInDept = allUsers.Where(u => userIdInDept.Contains(u.UserId));

				return new
				{
					DepartmentId = d.DepartmentId,
					ParentId = d.ParentId,
					DepartmentName = d.DepartmentName,
					DepartmentIdExt = d.DepartmentIdExt,
					DepartmentPath = d.DepartmentPath,
					Order = d.Order,
					Level = d.Level,
					Users = userInDept.Select(x => new User
					{
						UserId = x.UserId,
						Username = x.Username,
						FullName = x.FullName
					})
				};
			});

			return Json(results, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Trả về tất cả các địa chỉ ban hành của đơn vị hiện tại.
		/// Địa chỉ ban hành mặc định là tên cơ quan, và danh sách các trung tâm, phòng ban có cấu hình mã định danh riêng.
		/// </summary>
		/// <returns></returns>
		public JsonResult GetOrganizations()
		{
			var currentAddress = _addressService.GetCurrent();
			var userJobtitleDept = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
			var subAddress = userJobtitleDept.Where(i => !string.IsNullOrEmpty(i.EdocId));
			var result = new List<Organization>();

			// Subaddress hiên thị trước
			result.AddRange(subAddress.Select(i => new Organization()
			{
				OrganId = i.EdocId,
				OrganName = i.DepartmentName
			}));
            if (currentAddress != null)
            {
                result.Add(new Organization()
                {
                    OrganId = currentAddress.EdocId,
                    OrganName = currentAddress.Name
                });
            }
			

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		private int CurrentUserId()
		{
			return _userService.CurrentUser.UserId;
		}

		#endregion
	}
}