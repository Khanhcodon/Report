#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Web.Framework;


#endregion

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class TimeController : CustomController//BaseController
    {
        #region Readonly & Static Fields

        private readonly int _maxYear = DateTime.Now.Year + 2;
        private readonly int _minYear = DateTime.Now.Year - 1;
        private readonly SettingBll _settingService;
        private readonly TimeBll _timeService;
        private readonly WorktimeHelper _worktimeHelper;
        public ResourceBll _resourceService;

        #endregion

        #region Fields

        private WorkTimeSettings _workTimeSetting;

        #endregion

        #region C'tors

        public TimeController(
            TimeBll holidayService,
            SettingBll settingService,
            WorkTimeSettings workTimeSettings,
            WorktimeHelper worktimeHelper,
            ResourceBll resourceService)
            : base()
        {
            _timeService = holidayService;
            _settingService = settingService;
            _workTimeSetting = workTimeSettings;
            _worktimeHelper = worktimeHelper;
            _resourceService = resourceService;
        }

        #endregion

        #region Instance Methods

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var filter = GetCookieTimeListFilter();
            var yearView = GetValidateYear(filter.Year.ToString(CultureInfo.InvariantCulture));
            ViewBag.IsCreated = true;
            ViewBag.Holiday = true;
            return View(new HolidayModel { HolidayDate = new DateTime(yearView, DateTime.Now.Month, DateTime.Now.Day) });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt="TimeCreate")]
        public ActionResult Create(HolidayModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            ViewBag.IsCreated = true;
            ViewBag.Holiday = true;
            if (ModelState.IsValid)
            {
                var holiday = model.ToEntity();
                try
                {
                    holiday.IsHoliday = true;
                    _timeService.CreateHoliday(holiday);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.Created"));
                    model.HolidayName = string.Empty;
                    return View(model);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
                catch (Exception ex)
                {
                    LogException(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.Create.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.Create.Error"));
                }
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TimeDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var holiday = _timeService.GetHoliday(id);
            if (holiday == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotExist"));
                return RedirectToAction("Index", new { isList = false });
            }

            try
            {
                _timeService.DeteleHoliday(holiday);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Customer.Time.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.Deleted.Error"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.Deleted.Error"));
            }

            return RedirectToAction("Index", new { isList = false });
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            ViewBag.IsCreated = false;
            ViewBag.Holiday = true;
            var holiday = _timeService.GetHoliday(id);
            if (holiday == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotExist"));
                return RedirectToAction("Index", new { isList = false });
            }

            // Xử lý tạm vậy trước khi sửa thành nếu lặp thì chỉ hiện ngày tháng chứ k hiện năm trên view.
            if (holiday.IsRepeated)
            {
                var filter = GetCookieTimeListFilter();
                var yearView = GetValidateYear(filter.Year.ToString(CultureInfo.InvariantCulture));
                holiday.HolidayDate = new DateTime(yearView, holiday.HolidayDate.Month, holiday.HolidayDate.Day);
            }
            return View(holiday.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TimeEdit")]
        public ActionResult Edit(HolidayModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            ViewBag.IsCreated = false;
            ViewBag.Holiday = true;
            if (ModelState.IsValid)
            {
                try
                {
                    var holiday = _timeService.GetHoliday(model.HolidayId);
                    if (holiday == null)
                    {
                        CreateActivityLog(ActivityLogType.Admin,_resourceService.GetResource("Customer.Time.NotExist"));
                        ErrorNotification(_resourceService.GetResource("Customer.Time.NotExist"));
                        return RedirectToAction("Index", new { isList = false });
                    }

                    holiday = model.ToEntity(holiday);
                    _timeService.Update(holiday);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Customer.Time.Updated.Success"));
                    return RedirectToAction("Index", new { isList = false });
                }
                catch (Exception ex)
                {
                    LogException(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.Edit.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.Edit.Error"));
                }
            }
            return View(model);
        }

        public ActionResult GetExtendDate(string year)
        {
            try
            {
                _timeService.CreateExtendHolidays(int.Parse(year));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.ExtendDate.Success"));
                SuccessNotification(_resourceService.GetResource("Customer.Time.ExtendDate.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.ExtendDate.Error"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.ExtendDate.Error"));
            }
            return RedirectToAction("Index", new { isList = false });
        }

        public ActionResult Index(string year = null, bool isList = true, bool? type = null)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            if (!isList)
            {
                int yearView;
                if (string.IsNullOrWhiteSpace(year))
                {
                    var filter = GetCookieTimeListFilter();
                    yearView = GetValidateYear(filter.Year.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    yearView = GetValidateYear(year);
                    CreateCookieTimeListFilter(new HolidaySearchModel { Year = yearView });
                }
                var holidays = _timeService.GetHolidays(yearView, type);
                ViewBag.Years = GetYears(yearView);
                ViewBag.IsList = false;
                ViewBag.Type = type;
                ViewBag.Weekend = _timeService.GetWeekends().Select(c => c.ToString()).StringifyJs();
                var holidayModels = holidays.ToListModel().OrderBy(c => c.HolidayDateInSolar);
                return View(holidayModels);
            }

            ViewBag.IsList = true;
            ViewBag.Weekend = _timeService.GetWeekends().Select(c => c.ToString()).StringifyJs();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TimeSetHoliday")]
        public JsonResult SetHoliday(string dayOfWeek, bool isWeekend)
        {
            DayOfWeek dayId;
            if (Enum.TryParse(dayOfWeek, out dayId))
            {
                try
                {
                    if (isWeekend)
                    {
                        _timeService.CreateWeekend(dayId);
                    }
                    else
                    {
                        _timeService.DeleteWeekend(dayId);
                    }
                    return new JsonResult { Data = true };
                }
                catch
                {
                    return new JsonResult { Data = false };
                }
            }
            return new JsonResult { Data = false };
        }

        public JsonResult SetWorkTime(string workTime)
        {
            try
            {
                var workTimes = Json2.ParseAs<Setting>(workTime);
                if (!_settingService.Exist(workTimes.SettingKey))
                {
                    _settingService.Create(workTimes);
                }
                else
                {
                    _settingService.Update(workTimes, workTimes.SettingKey);
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult WorkTime()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionWorkTime"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionWorkTime"));
                return RedirectToAction("Index");
            }

            var model = _workTimeSetting.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "TimeWorkTime")]
        public ActionResult WorkTime(WorkTimeSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionWorkTime"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionWorkTime"));
                return RedirectToAction("Index");
            }

            _workTimeSetting = model.ToEntity(_workTimeSetting);
            _settingService.SaveSetting(_workTimeSetting);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.WordTime.Updated"));
            SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.WordTime.Updated"));
            return RedirectToAction("WorkTime");
        }

        private int GetValidateYear(string year)
        {
            int result;
            if (string.IsNullOrWhiteSpace(year) || !int.TryParse(year, out result))
            {
                result = DateTime.Now.Year;
            }
            if (result < _minYear || result > _maxYear)
            {
                result = DateTime.Now.Year;
            }
            return result;
        }

        /// <summary>
        ///   Trả về danh sách năm cấu hình ngày nghỉ lễ
        /// </summary>
        /// <param name="yearSelected"> </param>
        /// <returns> </returns>
        private List<SelectListItem> GetYears(int yearSelected)
        {
            var years = new List<SelectListItem>();
            var yearItem = _maxYear;
            while (yearItem >= _minYear)
            {
                years.Add(new SelectListItem
                    {
                        Value = yearItem.ToString(CultureInfo.InvariantCulture),
                        Text = yearItem.ToString(CultureInfo.InvariantCulture),
                        Selected =
                            yearItem.ToString(CultureInfo.InvariantCulture) ==
                            yearSelected.ToString(CultureInfo.InvariantCulture)
                    });
                yearItem--;
            }
            return years;
        }

        private void CreateCookieTimeListFilter(HolidaySearchModel holidayFilter)
        {
            var data = new Dictionary<string, object> { { "HolidayFilter", holidayFilter } };
            var cookie = Request.Cookies[CookieName.ListFilter];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.ListFilter, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }

            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private HolidaySearchModel GetCookieTimeListFilter()
        {
            #region Xu ly bind danh sach theo trang thai cu

            var filter = new HolidaySearchModel { Year = DateTime.Now.Year };
            var httpCookie = Request.Cookies[CookieName.ListFilter];
            var hasCookieValue = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    filter = Json2.ParseAs<HolidaySearchModel>(data["HolidayFilter"].ToString());
                    hasCookieValue = true;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    hasCookieValue = false;
                }
            }
            if (!hasCookieValue)
            {
                filter = new HolidaySearchModel { Year = DateTime.Now.Year };
                CreateCookieTimeListFilter(filter);
            }

            #endregion

            return filter;
        }

        #region Cấu hình ngày làm bù

        /// <summary>
        /// Danh sách các ngày đã dc cấu hình làm bù
        /// <para>HopCV - 030414</para>
        /// </summary>
        /// <param name="year">Năm</param>
        /// <returns></returns>
        public ActionResult DateWorkOffset(string year = null)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionDateWorkOffset"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionDateWorkOffset"));
                return RedirectToAction("Index", "Welcome");
            }

            int yearView;
            if (string.IsNullOrWhiteSpace(year))
            {
                var filter = GetCookieTimeListFilter();
                yearView = GetValidateYear(filter.Year.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                yearView = GetValidateYear(year);
                CreateCookieTimeListFilter(new HolidaySearchModel { Year = yearView });
            }
            var holidays = _timeService.GetDayWorkOffsets(yearView);
            ViewBag.Years = GetYears(yearView);
            ViewBag.Weekend = _timeService.GetWeekends().Select(c => c.ToString()).StringifyJs();
            var model = holidays.ToListModel();
            return View(model);
        }

        /// <summary>
        ///Tạo mới ngày làm bù 
        ///<para>HopCV - 030414</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateDateWorkOffset()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionCreateDateWorkOffset"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionCreateDateWorkOffset"));
                return RedirectToAction("DateWorkOffset", "Time");
            }

            var filter = GetCookieTimeListFilter();
            var yearView = GetValidateYear(filter.Year.ToString(CultureInfo.InvariantCulture));
            ViewBag.IsCreated = true;
            ViewBag.Holiday = false;
            return View(new HolidayModel { HolidayDate = new DateTime(yearView, DateTime.Now.Month, DateTime.Now.Day) });
        }

        /// <summary>
        ///   Tạo mới ngày làm bù 
        /// <para>HopCV</para>
        ///<para> create Date: 030414</para>
        /// </summary>
        /// <param name="model">entities holiday</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDateWorkOffset(HolidayModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionCreateDateWorkOffset"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionCreateDateWorkOffset"));
                return RedirectToAction("Index");
            }

            ViewBag.IsCreated = true;
            ViewBag.Holiday = false;
            if (ModelState.IsValid)
            {
                //Kiểm tra xem ngày chọn có phải là ngày nghỉ hoặc là ngày nghỉ bù trong năm hoặc năm kế tiếp
                if (_worktimeHelper.IsHoliday(model.HolidayDate))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsHoliday"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsHoliday"));
                    return View(model);
                }

                //Kiểm tra ngày chọn xem có phải là ngày làm việc bình thương trong ngày
                //Nếu là ngày bình thường thì đưa ra cảnh báo (validate yêu cầu ngày làm bù là ngày nghỉ trong tuần)
                if (!_worktimeHelper.IsWeekend(model.HolidayDate))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotWeekendDay"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotWeekendDay"));
                    return View(model);
                }

                var holiday = model.ToEntity();
                try
                {
                    holiday.IsHoliday = false;
                    holiday.IsRepeated = false;
                    holiday.IsLunar = false;
                    _timeService.CreateHoliday(holiday);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.CreateSuccess"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.CreateSuccess"));
                    return View(model);
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                catch (Exception ex)
                {
                    LogException(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.CreateError"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.CreateError"));
                    return View(model);
                }
            }

            return View(model);
        }

        /// <summary>
        /// Kiểm tra xem ngày truyền vao có phải là ngày nghỉ lế, nghỉ bù trong năm,năm kế tiếp hoặc là ngày làm bình thường
        /// </summary>
        /// <param name="date">Ngày truyền vào</param>
        /// <returns></returns>
        public JsonResult CheckDateIsNotWeekendOrHoliday(string date)
        {
            bool result = true;
            string message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.CheckSuccess");
            DateTime outDate;
            if (!DateTime.TryParse(date, out outDate))
            {
                result = false;
                message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.DateIsNotFormats");
            }

            //Kiểm tra xem ngày chọn có phải là ngày nghỉ hoặc là ngày nghỉ bù trong năm hoặc năm kế tiếp
            if (_worktimeHelper.IsHoliday(outDate))
            {
                result = false;
                message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsHoliday");
            }

            //Kiểm tra ngày chọn xem có phải là ngày làm việc bình thương trong ngày
            //Nếu là ngày bình thường thì đưa ra cảnh báo (validate yêu cầu ngày làm bù là ngày nghỉ trong tuần)
            if (!_worktimeHelper.IsWeekend(outDate))
            {
                result = false;
                message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotWeekendDay");
            }

            //Kiểm tra xem ngày có trong danh sách ngày làm bù chưa
            if (_timeService.ExistDate(outDate))
            {
                result = false;
                message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsExistDate");
            }

            return Json(new { result, message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteDateWorkOffset(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionDeleteDateWorkOffset"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionDeleteDateWorkOffset"));
                return RedirectToAction("DateWorkOffset", "Time");
            }

            var holiday = _timeService.GetHoliday(id);
            if (holiday == null)
            {
                return RedirectToAction("DateWorkOffset");
            }
            _timeService.DeteleHoliday(holiday);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.DeleteSuccess"));
            SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.DeleteSuccess"));
            return RedirectToAction("DateWorkOffset");
        }

        public ActionResult EditDateWorkOffset(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionUpdateDateWorkOffset"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionUpdateDateWorkOffset"));
                return RedirectToAction("DateWorkOffset", "Time");
            }

            var holiday = _timeService.GetHoliday(id);
            if (holiday == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotExistDate"));
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotExistDate"));
                return RedirectToAction("DateWorkOffset");
            }
            ViewBag.IsCreated = false;
            ViewBag.Holiday = false;
            var model = holiday.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditDateWorkOffset(HolidayModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Time.NotPermissionUpdateDateWorkOffset"));
                ErrorNotification(_resourceService.GetResource("Customer.Time.NotPermissionUpdateDateWorkOffset"));
                return RedirectToAction("Index");
            }

            ViewBag.IsCreated = false;
            ViewBag.Holiday = false;
            if (ModelState.IsValid)
            {
                //Kiểm tra xem ngày chọn có phải là ngày nghỉ hoặc là ngày nghỉ bù trong năm hoặc năm kế tiếp
                if (_worktimeHelper.IsHoliday(model.HolidayDate))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsHoliday"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsHoliday"));
                    return View(model);
                }

                //Kiểm tra ngày chọn xem có phải là ngày làm việc bình thương trong ngày
                //Nếu là ngày bình thường thì đưa ra cảnh báo (validate yêu cầu ngày làm bù là ngày nghỉ trong tuần)
                if (!_worktimeHelper.IsWeekend(model.HolidayDate))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotWeekendDay"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.IsNotWeekendDay"));
                    return View(model);
                }

                var holiday = model.ToEntity();
                try
                {
                    holiday.HolidayDate = model.HolidayDate;
                    _timeService.Update(holiday);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.UpdateSuccess"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.UpdateSuccess"));
                    return RedirectToAction("DateWorkOffset");
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                catch (Exception ex)
                {
                    LogException(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.UpdateError"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Time.UpdateError"));
                    return View(model);
                }
            }

            return View(model);
        }

        #endregion

        #endregion
    }
}