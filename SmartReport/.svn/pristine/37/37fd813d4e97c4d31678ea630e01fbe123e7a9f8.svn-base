using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class TooltipController : CustomerBaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly UserActivityLogBll _userLogServices;
        private readonly UserBll _userService;

        public TooltipController(
            AdminGeneralSettings generalSettings,
            UserBll userService,
            UserActivityLogBll userLogServices
            )
        {
            _generalSettings = generalSettings;
            _userService = userService;
            _userLogServices = userLogServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DateOverdue()
        {
            return View();
        }

        public ActionResult GotoDate()
        {
            return View();
        }

        public ActionResult DoctypeSelect()
        {
            return View();
        }

        //public ActionResult AlertNotification()
        //{
        //    var notifies = _userLogServices.Gets(User.GetUserId(), true);
        //    var userIds = notifies.Select(u => u.UserSendId);
        //    var users = _userService.Gets(userIds);
        //    var model = new List<UserNotifyModel>();
        //    foreach (var notify in notifies)
        //    {
        //        var user = users.SingleOrDefault(u => u.UserId == notify.UserSendId);
        //        var notifyModel = notify.ToModel();
        //        notifyModel.UserSend = user == null ? string.Empty : user.FullName;
        //        notifyModel.Content = GetContent(notifyModel);
        //        notifyModel.UserSendAvatar = GetAvatar(user.Username);
        //        model.Add(notifyModel);
        //    }
        //    return View(model);
        //}

        private string GetAvatar(string userName)
        {
            string result = string.Empty;
            var avatarFileName = "~/AvatarProfile/" + userName + ".jpg";
            if (System.IO.File.Exists(Server.MapPath(avatarFileName)))
            {
                var file = new System.IO.FileInfo(Server.MapPath(avatarFileName));
                result = avatarFileName + "?date=" + file.LastWriteTime.ToString("ddmmyyyyhhmmss");
            }
            else
            {
                result = "~/AvatarProfile/noavatar.jpg";
            }
            return result;
        }

        //private string GetContent(UserNotifyModel notify)
        //{
        //    var result = string.Empty;
        //    result = "Bạn nhận được 1 {0} được chuyển từ <strong>{1}</strong>";
        //    var docCopyType = EnumHelper<DocumentCopyTypes>.GetDescription(notify.DocumentCopyTypeInEnum);
        //    var userSend = notify.UserSend;

        //    return string.Format(result, docCopyType, userSend);
        //}

        public ActionResult UserOption()
        {
            return View();
        }

        public ActionResult PageSize()
        {
            var user = _userService.CurrentUser;
            var userSetting = user.UserSetting;
            var listPageSizeHome = _generalSettings.ListPageSizeHome;
            var defaultPageSizeHome = _generalSettings.DefaultPageSizeHome;
            if (!string.IsNullOrWhiteSpace(userSetting))
            {
                var userSettingModel = Json2.ParseAs<UserSettingModel>(user.UserSetting);
                listPageSizeHome = (ConvertStringToListTypeInt(userSettingModel.ListPageSizeHome, ',') != null && ConvertStringToListTypeInt(userSettingModel.ListPageSizeHome, ',').Any())
                                ? ConvertStringToListTypeInt(userSettingModel.ListPageSizeHome, ',')
                                : listPageSizeHome;
                defaultPageSizeHome = Convert.ToInt32(userSettingModel.DefaultPageSizeHome.Value) > 0
                                        ? userSettingModel.DefaultPageSizeHome.Value
                                        : defaultPageSizeHome;
            }
            ViewBag.ListPageSize = listPageSizeHome;
            ViewBag.DefaultPageSize = defaultPageSizeHome;
            return View();
        }

        private List<int> ConvertStringToListTypeInt(string str, char charSplit)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            string[] tmp = str.Split(new char[] { charSplit });
            List<int> list = new List<int>();
            foreach (string item in tmp)
            {
                list.Add(Convert.ToInt32(item));
            }
            return list;
        }

        public JsonResult GetDocumentInsertMenu()
        {
#if !XuLyVanBanEdition
            const string result = ""
                                  + "["
                                  + "{\"name\": \"Tiếp nhận theo lô\", \"id\": 0},"
                                  + "{\"name\": \"Văn bản liên quan\", \"id\": 1},"
                                  + "{\"name\": \"Tệp đính kèm\", \"id\": 2},"
                                  + "{\"name\": \"Tệp scan\", \"id\": 3},"
                                  + "{\"name\": \"Giấy phép\", \"id\": 4},"
                                  + "{\"name\": \"Dự kiến chuyển\", \"id\": 5}"
                                  + "]";
#else
            const string result = ""
                                  + "["
                                  + "{\"name\": \"Tiếp nhận theo lô\", \"id\": 0},"
                                  + "{\"name\": \"Văn bản liên quan\", \"id\": 1},"
                                  + "{\"name\": \"Tệp đính kèm\", \"id\": 2},"
                                  + "{\"name\": \"Tệp scan\", \"id\": 3},"
                                  + "{\"name\": \"Dự kiến chuyển\", \"id\": 5}"
                                  + "]";
#endif
            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentImportantSelect()
        {
            return View();
        }

        public ActionResult DocumentViewStateSelect()
        {
            return View();
        }

        public ActionResult Link()
        {
            return View();
        }
    }
}