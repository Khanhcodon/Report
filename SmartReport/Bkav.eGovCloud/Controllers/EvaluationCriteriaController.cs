using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;

using Newtonsoft.Json;

using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Api.Service;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Areas.Admin.Models;

namespace Bkav.eGovCloud.Controllers
{
    public class EvaluationCriteriaController : CustomerBaseController
    {
        private readonly CriteriaBll _criteriaService;
        private readonly DepartmentBll _departmentService;
        private readonly InfringeListBll _infringeService;
        private readonly UserBll _userService;
        private readonly UserDepartmentJobTitlesPositionBll _userdepartService;
        private readonly PositionBll _positionService;
        private readonly DocTypeBll _doctypeService;
        private readonly PermissionBll _permissionSerice;
        private readonly CBCLSetting _cbclSettings;

        public EvaluationCriteriaController(
            CriteriaBll criteriaService,
            DepartmentBll departmentService,
            InfringeListBll infringeService,
            UserBll userService,
            UserDepartmentJobTitlesPositionBll userdepartService,
            PositionBll positionService,
            DocTypeBll doctypeService,
            PermissionBll permissionSerice,
            CBCLSetting cbclSettings)
        {
            _criteriaService = criteriaService;
            _departmentService = departmentService;
            _infringeService = infringeService;
            _userService = userService;
            _userdepartService = userdepartService;
            _positionService = positionService;
            _doctypeService = doctypeService;
            _permissionSerice = permissionSerice;
            _cbclSettings = cbclSettings;
        }

        public ActionResult Index()
        {
            var currentUser = _userService.CurrentUser;
            var position = _positionService.GetCacheAllPosition().ToList();
            var isManager = _userdepartService.GetUserPersonnelDown(currentUser.UserId, position).Count();

            ViewBag.IsManager = isManager > 0;
            ViewBag.TemplateSendDoc = (_cbclSettings.HtmlTemplate != null) ? _cbclSettings.HtmlTemplate : "";

            var isAdmin = false;
            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(currentUser.UserId.ToString());
            }
            ViewBag.IsAdmin = isAdmin;

            ViewBag.DepartmentExt = _userdepartService.GetbyUserId(currentUser.UserId).FirstOrDefault();

            return View();
        }

        public JsonResult GetActionList()
        {
            if (_cbclSettings.DoctypeConfig == null)
            {
                return Json("[]", JsonRequestBehavior.AllowGet);
            }

            var doctypeId = Guid.Parse(_cbclSettings.DoctypeConfig);
            var egovApiService = new DocumentApiService();

            var actions = egovApiService.GetActionList(doctypeId, _userService.CurrentUser.Username).Select(d => new
            {
                Id = d.Id,
                Name = d.Name,
                NodeId = d.NextNodeId
            });

            return Json(actions.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersByAction(string actionId)
        {
            if (_cbclSettings.DoctypeConfig == null)
            {
                return Json("[]", JsonRequestBehavior.AllowGet);
            }

            var egovApiService = new DocumentApiService();
            var doctypeId = Guid.Parse(_cbclSettings.DoctypeConfig);
            var userActions = egovApiService.GetUsersByAction(doctypeId, actionId, _userService.CurrentUser.Username);

            return Json(userActions.ToList(), JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Gửi công văn
        /// </summary>
        /// <param name="account"></param>
        /// <param name="fileName"></param>
        /// <param name="compendium"></param>
        /// <param name="ideally"></param>
        /// <param name="contentHtml"></param>
        /// <param name="nextNodeId"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public JsonResult SendDispatches(string account, string fileName, string compendium, string ideally, string contentHtml, int nextNodeId)
        {
            contentHtml = System.Net.WebUtility.HtmlDecode(contentHtml);

            var positionByUser = _userdepartService.GetbyUserId(_userService.CurrentUser.UserId).Where(d => d.IsPrimary == true).FirstOrDefault();
            var positionCurentUser = _positionService.Get(positionByUser.PositionId);
            var departmentNameCurentUser = _departmentService.Get(positionByUser.DepartmentId).DepartmentName;

            contentHtml = contentHtml.Replace("{acccreatpos}", positionCurentUser.PositionName);
            contentHtml = contentHtml.Replace("{departmentname}", departmentNameCurentUser);
            contentHtml = contentHtml.Replace("{acccreate}", _userService.CurrentUser.FullName);

            var attachment = GetWordTemplate().Replace("{0}", contentHtml);
            var listFileAtt = new List<ReceivedAttachment>();
            string base64string = HTMLtoDOCX.GetBase64DOCX(attachment);
            listFileAtt.Add(new ReceivedAttachment
                    {
                        Data = base64string,
                        Name = fileName
                    });

            // Todo: có thể xử lý dưới client
            var userRec = new List<String>();
            var separator = new string[] { "@" };
            var username = account.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0];
            userRec.Add(username);

            var documentApiService = new DocumentApiService();
            // Trạng thái của công văn
            // - Nếu bằng -1 là gửi công văn thất bại
            var sendStatus = documentApiService.ReceiveDocument(new ReceivedDocument
            {
                Compendium = compendium,
                Attachments = listFileAtt.ToArray(),
                Comment = ideally,
                Content = contentHtml,
                ContentUrl = "",
                From = _userService.CurrentUser.Username,
                To = userRec.ToArray(),
                NodeReceived = 1,
                // Danh sách văn bản liên quan
                RelationDocumentCopysId = new List<int>()
            });

            return Json(sendStatus, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xóa người vi phạm theo ID
        /// </summary>
        /// <param name="infringeid">Id bảng vi phạm</param>
        /// <returns></returns>
        public JsonResult DeleteUserInfringebyId(int infringeId)
        {
            var infringeRemove = _infringeService.GetbyID(infringeId);

            try
            {
                _infringeService.Delete(infringeRemove);
            }
            catch (Exception)
            {
                return Json("Xảy ra lỗi khi xóa", JsonRequestBehavior.AllowGet);
                throw;
            }

            return Json("xóa thành công", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xem vi phậm của người dùng hiện tai dưa theo khoảng thời gian
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public JsonResult GetsCheckInfringeViewByCurrentUser(DateTime dateBegin, DateTime dateEnd)
        {
            var currentUser = _userService.CurrentUser;
            var infringes = _infringeService.GetbyDatetimeByUser(dateBegin, dateEnd, currentUser.UserId);
            var listInfringe = GetInfringes(infringes.ToList());

            return Json(listInfringe.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xem tất cả các vi phạm trong 1 khoảng thời gian
        /// </summary>
        /// <param name="dateBegin">Thời gian bắt đầu</param>
        /// <param name="dateEnd">Thời gian kết thúc</param>
        /// <returns></returns>
        public JsonResult GetsCheckInfringebyDateRange(DateTime dateBegin, DateTime dateEnd)
        {
            var isAdmin = false;
            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(_userService.CurrentUser.UserId.ToString());
            }
            // Kiểm tra xem co phải là người quản trị hay ko
            if (isAdmin)
            {
                var infringes = _infringeService.GetbyDatetime(dateBegin, dateEnd).ToList();
                var infringe = GetInfringes(infringes.ToList());

                return Json(infringe, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var currentUser = _userService.CurrentUser;
                var position = _positionService.GetCacheAllPosition().ToList();
                //var listUser = _userdepartService.GetUserPersonnelDown(currentId.UserId, position).Select(u => u.UserId);
                var listUserDepartment = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition(); ;
                var listUser = _userService.GetNhanVienCapDuoi(currentUser.UserId, listUserDepartment, position);

                if (!listUser.Any())
                {
                    return Json("none", JsonRequestBehavior.AllowGet);
                }

                var userinfringes = listUser.ToList();
                var infriges = _infringeService.GetByDateTimeByUserRole(dateBegin, dateEnd, userinfringes);
                var listinfringe = GetInfringes(infriges.ToList());

                return Json(listinfringe.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateCriteria()
        {
            var isAdmin = false;

            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(_userService.CurrentUser.UserId.ToString());
            }

            // Kiểm tra xem co phải là người quản trị hay ko
            if (isAdmin)
            {
                ViewBag.IsAdmin = true;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetsDepartment()
        {
            var listCriteria = _departmentService.GetCacheAllDepartments().Select(d => new
            {
                DepartmentName = d.DepartmentName,
                DepartmentId = d.DepartmentId,
                DepartmentPath = d.DepartmentPath,
                DepartmentIDExt = d.DepartmentIdExt,
                ParentId = d.ParentId
            }).ToList();

            return Json(listCriteria, JsonRequestBehavior.AllowGet);
        }
        // lấy tất cả các tiêu chí
        public JsonResult GetsCriteria()
        {
            var listcrit = _criteriaService.GetReadOnlys().Select(d => new
            {
                RateEmployeeId = d.RateEmployeeId,
                DepartmentId = d.DepartmentId,
                ParentId = d.ParentId,
                Name = d.Name,
                Description = d.Description,
                Point = d.Point,
                IsActived = d.IsActive,
                ParentRateEmployee = d.ParentRateEmployee,
                RateEmployeeChildrens = d.RateEmployeeChildrens,
                Department = d.Department,
                CheckInfringes = d.CheckInfringes,
                DepartmentName = GetDepartment(d.DepartmentId),
                DepartmentIdExt = _departmentService.GetbyId(GetDepartmentID(d.DepartmentId)).DepartmentIdExt,
                DepartmentPath = _departmentService.GetbyId(GetDepartmentID(d.DepartmentId)).DepartmentPath
            }).ToList();

            return Json(listcrit, JsonRequestBehavior.AllowGet);
        }

        // Hàm lấy tên phòng ban
        private string GetDepartment(int? departmentId)
        {
            var departmentName = "";

            if (departmentId.HasValue)
            {
                var item = _departmentService.GetbyId(departmentId.Value);
                if (item != null)
                {
                    departmentName = item.DepartmentName;
                }
            }

            return departmentName;
        }

        private int GetDepartmentID(int? departmentId)
        {
            var departmentidtmp = _departmentService.GetReadOnlys().FirstOrDefault().DepartmentId;

            if (departmentId.HasValue)
            {
                var item = _departmentService.GetbyId(departmentId.Value);
                if (item != null)
                {
                    departmentidtmp = item.DepartmentId;
                }
            }

            return departmentidtmp;
        }

        /// <summary>
        /// Lọc các tiêu chí theo phòng ban
        /// </summary>
        /// <param name="departmentId">ID phòng ban</param>
        /// <returns></returns>
        public JsonResult GetsCriteriabyDepartment(int departmentId)
        {
            var listCriteria = _criteriaService.GetReadOnlysByDepartment(departmentId).Select(d => new
            {
                RateEmployeeId = d.RateEmployeeId,
                DepartmentId = d.DepartmentId,
                ParentId = d.ParentId,
                Name = d.Name,
                Description = d.Description,
                Point = d.Point,
                IsActived = d.IsActive,
                ParentRateEmployee = d.ParentRateEmployee,
                RateEmployeeChildrens = d.RateEmployeeChildrens,
                Department = d.Department,
                CheckInfringes = d.CheckInfringes,
                DepartmentName = GetDepartment(d.DepartmentId),
                DepartmentIdExt = _departmentService.GetbyId(d.DepartmentId.Value).DepartmentIdExt
            }).ToList();
            return Json(listCriteria, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lọc các tiêu chí theo nhiều phòng ban
        /// </summary>
        /// <param name="listDepartmentId">chuỗi Json các id phòng ban</param>
        /// <returns></returns>
        public JsonResult GetsCriteriabyDepartment(string listDepartmentId)
        {
            var arrayDepartmentId = JsonConvert.DeserializeObject<List<int>>(listDepartmentId);
            var listCriteria = _criteriaService.GetReadOnlysByDepartment(arrayDepartmentId).ToList();

            return Json(listCriteria, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xóa các tiêu chí theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteCriteria(int rateemployeeId)
        {
            var isAdmin = false;
            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(_userService.CurrentUser.UserId.ToString());
            }

            // Kiểm tra xem co phải là người quản trị hay ko
            if (isAdmin)
            {
                if (_infringeService.CheckByRateEmployeeID(rateemployeeId) == 0)
                {
                    var criteria = new RateEmployee();
                    criteria = _criteriaService.Get(rateemployeeId);
                    _criteriaService.Delete(criteria);
                    return Json("Delete", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Bạn cần xóa hết các người vi phạm thuộc tiêu chí này", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Không có quyền xóa", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Thêm hoặc sửa các tiêu chí
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public JsonResult CreateorUpdateCriteria(string criteria)
        {
            var isAdmin = false;
            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(_userService.CurrentUser.UserId.ToString());
            }
            // Kiểm tra xem co phải là người quản trị hay ko
            if (isAdmin)
            {
                if (string.IsNullOrEmpty(criteria))
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                try
                {
                    var criteriaObject = new RateEmployee();
                    var criteriaObjectUpdate = new RateEmployee();
                    criteriaObject = JsonConvert.DeserializeObject<RateEmployee>(criteria);
                    criteriaObjectUpdate = _criteriaService.Get(criteriaObject.RateEmployeeId);

                    if (criteriaObject.RateEmployeeId != -1)
                    {
                        criteriaObjectUpdate.Name = criteriaObject.Name;
                        criteriaObjectUpdate.DepartmentId = criteriaObject.DepartmentId;
                        criteriaObjectUpdate.Point = criteriaObject.Point;
                        criteriaObjectUpdate.Description = criteriaObject.Description;

                        _criteriaService.Update(criteriaObjectUpdate);
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var criteriaCreate = new RateEmployee();
                        criteriaCreate.Name = criteriaObject.Name;
                        if (criteriaObject.ParentId == 0)
                        {
                            criteriaCreate.ParentId = null;
                        }
                        else
                        {
                            criteriaCreate.ParentId = criteriaObject.ParentId;
                        }
                        criteriaCreate.DepartmentId = criteriaObject.DepartmentId;
                        criteriaCreate.Point = criteriaObject.Point;
                        criteriaCreate.Description = criteriaObject.Description;
                        _criteriaService.Create(criteriaCreate);
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Lỗi khi Thêm/Sửa");
                }
            }
            else
            {
                return Json("Không có quyền sửa");
            }

        }

        /// <summary>
        /// Trang đánh dấu các tiêu chí thi đua 
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckCriteria()
        {
            return View();
        }

        /// <summary>
        /// Lấy ra danh sach các user
        /// </summary>
        /// <returns>Nếu là quan lý sẽ lấy được tất cả</returns>
        /// <returns>Nếu không phải quản lý chỉ lấy được chính mình và nhân viên cấp dưới</returns>
        public JsonResult GetsAllUser()
        {
            var isAdmin = false;

            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(_userService.CurrentUser.UserId.ToString());
            }

            // Kiểm tra xem co phải là người quản trị hay ko
            if (isAdmin)
            {
                var users = _userService.Gets().Select(x => new
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    FullName = x.FullName,
                }).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var currentUser = _userService.CurrentUser;
                // var pos = new List<Position>();
                var positionUser = _positionService.GetCacheAllPosition().ToList();
                // lấy ra nhân viên cấp dưới
                var userIds = _userdepartService.GetUserPersonnelDown(currentUser.UserId, positionUser).Select(u => u.UserId).ToList();
                var users = new List<User>();

                foreach (var userId in userIds)
                {
                    var user = _userService.Get(userId);
                    users.Add(user);
                }

                users.Add(new User() { UserId = currentUser.UserId, Username = currentUser.Username, FullName = currentUser.FullName });
                var userjson = users.Select(x => new
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    FullName = x.FullName,
                }).Distinct();

                return Json(userjson, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetUserDown()
        {
            var isAdmin = false;

            if (!string.IsNullOrEmpty(_cbclSettings.AccountsName))
            {
                var userConfigs = _cbclSettings.AccountsName.Split(',');
                isAdmin = userConfigs.Contains(_userService.CurrentUser.UserId.ToString());
            }
            // Kiểm tra xem co phải là người quản trị hay ko

            if (isAdmin)
            {
                var userJson = _userService.Gets().Select(x => new
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    FullName = x.FullName,
                }).Distinct();
                return Json(userJson, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var currentUser = _userService.CurrentUser;
                // var pos = new List<Position>();
                var pos = _positionService.GetCacheAllPosition().ToList();
                // var list = new List<UserDepartmentJobTitlesPosition>();
                var userIds = _userdepartService.GetUserPersonnelDown(currentUser.UserId, pos).Select(u => u.UserId).ToList();
                var users = new List<User>();
                foreach (var userid in userIds)
                {
                    var user = _userService.Get(userid);
                    users.Add(user);
                }
                //users.Add(current);
                var userJson = users.Select(x => new
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    FullName = x.FullName,
                }).Distinct();
                return Json(userJson, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetsCheckInfringe()
        {
            var infringes = _infringeService.GetReadOnlys().ToList();
            return Json(infringes, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Thêm mới người vi phạm
        /// </summary>
        /// <param name="infringe">Json đối tượng vi phạm</param>
        /// <returns></returns>
        public JsonResult AddInfringeUser(string infringe)
        {
            if (string.IsNullOrEmpty(infringe))
            {
                return Json("");
            }
            CheckInfringe infringeModel = new CheckInfringe();
            CheckInfringe infringeUpdate = new CheckInfringe();
            infringeModel = JsonConvert.DeserializeObject<CheckInfringe>(infringe);
            infringeUpdate = _infringeService.GetbyID(infringeModel.CheckInfringeId);
            if (infringeModel.CheckInfringeId != -1)
            {
                infringeUpdate.Date = infringeModel.Date;
                infringeUpdate.Detail = infringeModel.Detail;
                infringeUpdate.InfringeUserId = infringeModel.InfringeUserId;
                infringeUpdate.RateEmployeeId = infringeModel.RateEmployeeId;
                _infringeService.Update(infringeUpdate);
            }
            else
            {
                CheckInfringe infringeCreate = new CheckInfringe();
                infringeCreate.Date = infringeModel.Date;
                infringeCreate.Detail = infringeModel.Detail;
                infringeCreate.InfringeUserId = infringeModel.InfringeUserId;
                infringeCreate.RateEmployeeId = infringeModel.RateEmployeeId;
                infringeCreate.CreateUserId = _userService.CurrentUser.UserId;
                infringeCreate.IsActived = true;
                infringeCreate.Cause = "false";
                _infringeService.Create(infringeCreate);
            }
            try
            {
            }
            catch (Exception ex)
            {
                return Json("Lỗi khi Thêm/Sửa");
            }
            return Json("thành công", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xem trước công văn gửi đi
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult ReviewArchives(DateTime start, DateTime end)
        {

            var currentUser = _userService.CurrentUser;
            // var pos = new List<Position>();
            var positions = _positionService.GetCacheAllPosition().ToList();
            // var list = new List<UserDepartmentJobTitlesPosition>();
            var list = _userdepartService.GetUserPersonnelDown(currentUser.UserId, positions).Select(u => u.UserId);
            var userinfringes = list.ToList();

            if (_cbclSettings.HtmlTemplate != null)
            {
                ViewBag.HtmlTemplate = _cbclSettings.HtmlTemplate;
            }
            else
            {
                ViewBag.HtmlTemplate = "";
            }
            var listInfringeUser = _infringeService.GetByDateTimeByUserRole(start, end, userinfringes).Select(d => new CriteriaDepartment
            {
                RateEmployeeId = d.RateEmployeeId,
                CheckInfringeId = d.CheckInfringeId,
                Point = _criteriaService.Get(d.RateEmployeeId).Point,
                ParentId = _criteriaService.Get(d.RateEmployeeId).ParentId,
                InfringeUserId = d.InfringeUserId,
                UserName = _userService.Get(d.InfringeUserId).FullName,
                PositionName = _positionService.Get(_userdepartService.GetbyUserId(d.InfringeUserId).FirstOrDefault().PositionId).PositionName
            }).OrderBy(d => d.InfringeUserId);

            var userInfringe = _userService.Get(currentUser.UserId);
            var listCriteria = _criteriaService.GetReadOnlys().ToList();

            ViewBag.User = userInfringe;
            ViewBag.UserDepart = _positionService.Get(_userdepartService.GetbyUserId(userInfringe.UserId).Where(d => d.IsPrimary == true).FirstOrDefault().PositionId).PositionName;
            ViewBag.Department = _departmentService.Get(_userdepartService.GetbyUserId(userInfringe.UserId).Where(d => d.IsPrimary == true).FirstOrDefault().DepartmentId).DepartmentName;
            ViewBag.ListInfringe = listInfringeUser.ToList();

            return View(listCriteria);
        }

        //Hàm lấy giá trị của bang liên kết phòng ban và nhân viên theo nhân viên
        public JsonResult GetUserDepartmentsById(int userId)
        {
            var list = _userdepartService.GetbyUserId(userId).Select(d => new
              {
                  UserId = d.UserId,
                  DepartmentIdExt = d.DepartmentIdExt,
              }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private string GetWordTemplate()
        {
            return @"<html xmlns:v=""urn:schemas-microsoft-com:vml""
                        xmlns:o=""urn:schemas-microsoft-com:office:office""
                        xmlns:w=""urn:schemas-microsoft-com:office:word""
                        xmlns:m=""http://schemas.microsoft.com/office/2004/12/omml""
                        xmlns=""http://www.w3.org/TR/REC-html40"">"
                    + @"<head>
                        <meta http-equiv=Content-Type content=""text/html; charset=UTF-8"">
                        <meta name=ProgId content=Word.Document>
                        <link rel=File-List href=""download_files/filelist.xml"">
                        <link rel=Edit-Time-Data href=""download_files/editdata.mso"">"
                    + ""
                    + "</head>"
                    + "<body lang=EN-US style='tab-interval:36.0pt'>"
                    + "{0}"
                    + "</body>"
                    + "</html>";
        }

        private IEnumerable<InfringeModel> GetInfringes(List<CheckInfringe> infriges)
        {
            var userIds = infriges.Select(i => i.InfringeUserId).ToList();
            var criterias = _criteriaService.GetReadOnlys().ToList();
            var users = _userService.Gets(userIds.Distinct(), true);

            userIds.AddRange(infriges.Select(i => i.CreateUserId));

            if (!infriges.Any())
            {
                Json("[]", JsonRequestBehavior.AllowGet);
            }

            var listInfringe = infriges.Select(d =>
            {
                var account = users.SingleOrDefault(u => u.UserId == d.InfringeUserId);
                var userCreated = users.SingleOrDefault(u => u.UserId == d.CreateUserId);
                var parentname = d.RateEmployee.ParentId.HasValue ? criterias.SingleOrDefault(u => u.RateEmployeeId == d.RateEmployee.ParentId.Value).Name : "";
                if (account == null || userCreated == null)
                {
                    account = new User();
                    userCreated = new User();
                }
                return new InfringeModel
                {
                    CheckInfringeId = d.CheckInfringeId,
                    Account = account.Username,
                    NameParent = parentname,
                    Name = d.RateEmployee.Name,
                    Detail = d.Detail,
                    Point = d.RateEmployee.Point,
                    Times = 1,
                    Date = d.Date,
                    CreateUserIdName = userCreated.Username,
                };
            });

            return listInfringe;
        }

    }
}
