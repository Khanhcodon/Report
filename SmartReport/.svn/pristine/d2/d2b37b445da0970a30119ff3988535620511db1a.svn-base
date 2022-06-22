using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Models;

using Newtonsoft.Json;


namespace Bkav.eGovCloud.Controllers
{
	public class CalendarController : CustomerBaseController
	{
		private readonly CalendarBll _calendarService;
		private readonly UserBll _userService;
		private readonly DepartmentBll _departmentService;
		private readonly PositionBll _positionService;
		private readonly JobTitlesBll _jobtitleService;
		private readonly InfomationBll _infomationService;

		private readonly SendEmailHelper _sendMailService;
		private readonly SendSmsHelper _sendSmsService;

		private SsoSettings _ssoSettings;
		private readonly AdminGeneralSettings _adminGeneralSettings;

		private const string _DTFormat = "dd-MM-yyyy";

		public CalendarController(CalendarBll calendarService, UserBll userService, AdminGeneralSettings adminGeneralSettings,
					SendEmailHelper sendMailService, SendSmsHelper sendSmsService, InfomationBll infomationService,
					DepartmentBll departmentService, PositionBll positionService, JobTitlesBll jobtitleService)
		{
			_calendarService = calendarService;
			_userService = userService;
			_adminGeneralSettings = adminGeneralSettings;
			_sendMailService = sendMailService;
			_sendSmsService = sendSmsService;
			_infomationService = infomationService;
			_departmentService = departmentService;
			_positionService = positionService;
			_jobtitleService = jobtitleService;
		}

		public ActionResult Index()
		{
			_ssoSettings = SsoSettings.Instance;
			ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
			ViewBag.OfficeName = _infomationService.GetCurrentOfficeName();
			ViewBag.HasPermission = HasPermission();
			ViewBag.IsFromDocument = false;
			return View();
		}

		public ActionResult Today()
		{
			ViewBag.OfficeName = _infomationService.GetCurrentOfficeName();
			return View();
		}
		
		public ActionResult Demo()
		{
			ViewBag.OfficeName = _infomationService.GetCurrentOfficeName();
			ViewBag.IsDemo = true;
			return View("Today");
		}

		public ActionResult IndexFromDocument()
		{
			_ssoSettings = SsoSettings.Instance;
			ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
			ViewBag.OfficeName = _infomationService.GetCurrentOfficeName();
			ViewBag.HasPermission = HasPermission();
			ViewBag.IsFromDocument = true;
			return View();
		}

		public JsonResult GetCalendars(DateTime from, DateTime to)
		{
			var fromDate = new DateTime(from.Year, from.Month, from.Day, 0, 0, 0);
			var toDate = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);

			var result = _calendarService.GetAccepteds(fromDate, toDate);
			return Json(new { data = result.ToListModel(), from = from.ToString(_DTFormat), to = to.ToString(_DTFormat) }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetToday()
		{
			var isAm = DateTime.Now.Hour < 12;
			var today = DateTime.Today;
			var from = isAm ? today.StartOf(DateTimeUnit.Day) : today.StartOf(DateTimeUnit.Day).AddHours(12);
			var to = isAm ? today.StartOf(DateTimeUnit.Day).AddHours(12) : DateTime.Today.EndOf(DateTimeUnit.Day);

			var result = _calendarService.GetToday(from, to);
			return Json(new { data = result.ToListModel(), from = from.ToString(_DTFormat), to = to.ToString(_DTFormat) }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetDaily(int day = 0)
		{
			var from = DateTime.Today.StartOf(DateTimeUnit.Day);
			var to = DateTime.Today.EndOf(DateTimeUnit.Day);
			return GetCalendars(from, to);
		}

		public JsonResult GetWeekly()
		{
			var from = DateTime.Today.StartOf(DateTimeUnit.Week);
			var to = DateTime.Today.EndOf(DateTimeUnit.Week);

			return GetCalendars(from, to);
		}

		public JsonResult GetMonthly()
		{
			var from = DateTime.Today.StartOf(DateTimeUnit.Month);
			var to = DateTime.Today.EndOf(DateTimeUnit.Month);

			return GetCalendars(from, to);
		}

		public JsonResult GetMobile()
		{
			var from = DateTime.Today.StartOf(DateTimeUnit.Day);
			var to = DateTime.Today.EndOf(DateTimeUnit.Week);

			return GetCalendars(from, to);
		}

		#region Quản lý lịch

		public JsonResult GetManager()
		{
			var userId = User.GetUserId();
			var hasPermision = HasPermission();

			// Quản lý lịch lấy từ tuần hiện tại.
			var from = DateTime.Today.StartOf(DateTimeUnit.Week);
			var to = DateTime.Today.EndOf(DateTimeUnit.Year);

			var accepteds = hasPermision ? _calendarService.GetAccepteds(from, to) : _calendarService.GetAccepteds(from, to, userId);
			var notAccepteds = hasPermision ? _calendarService.GetNotAccepteds() : _calendarService.GetNotAccepteds(userId);
			var privates = _calendarService.GetsPrivate(from, to);

			var resource = _calendarService.GetResources().Select(r => new
			{
				value = r.Name,
				label = r.Name,
				id = r.CalendarResourceId
			});

			var allUserDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
			var allPosition = _positionService.GetCacheAllPosition();
			var allJobtitles = _jobtitleService.GetCacheAllJobtitles();

			var users = _userService.GetAllCached(true).Select(u =>
			{
				var uds = allUserDepts.Where(ud => ud.UserId == u.UserId);
				var udj = uds.OrderBy(ud => ud.IsPrimary).FirstOrDefault();
				var position = udj == null ? null : allPosition.SingleOrDefault(p => p.PositionId == udj.PositionId);

				return new
				{
					id = u.UserId,
					value = u.FullName,
					isJobtitle = false,
					label = string.Format("{0} ({1})", u.FullName, position == null ? "" : position.PositionName),
					position = position == null ? "" : position.PositionName,
					positionLevel = position == null ? 100 : position.PriorityLevel,
					firstName = u.FirstName
				};
			});

			var jobtitles = new List<dynamic>();
			foreach (var j in allJobtitles)
			{
				var udj = allUserDepts.Where(ud => ud.JobTitlesId == j.JobTitlesId).Select(ud => ud.PositionId);
				if (!udj.Any())
				{
					continue;
				}

				var positions = allPosition.Where(p => udj.Contains(p.PositionId));

				foreach (var position in positions)
				{
					jobtitles.Add(new
					{
						id = string.Format("{0}-{1}", j.JobTitlesId, position.PositionId),
						value = j.JobTitlesName,
						label = string.Format("{0}-{1}", j.JobTitlesName, position.PositionName),
						isJobtitle = true,
						position = position.PositionName,
						positionLevel = position.PriorityLevel
					});
				}
			}

			var departmentHasCalendar = _departmentService.GetCacheAllDepartments().Where(d => d.HasCalendar);
			var userDepts = allUserDepts.Where(u => u.UserId == userId).OrderByDescending(d => d.IsPrimary).FirstOrDefault();

			return Json(new
			{
				data = new
				{
					accepteds = accepteds.ToListModel(),
					notAccepteds = notAccepteds.ToListModel(),
					privates = privates.ToListModel(),
					resource = resource,
					users = users,
					jobtitles = jobtitles,
					userId = userId,
					offices = departmentHasCalendar.Select(d => new
					{
						departmentIdExt = d.DepartmentIdExt,
						name = d.DepartmentName,
						selected = userDepts == null ? false : userDepts.DepartmentIdExt == d.DepartmentIdExt
					})
				},
				from = from.ToString("d"),
				to = to.ToString("d")
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Create(string model)
		{
			if (string.IsNullOrEmpty(model))
			{
				return Json(new { error = true, message = "" }, JsonRequestBehavior.AllowGet);
			}

			var currentUser = _userService.CurrentUser;
			var calendar = Json2.ParseAs<Calendar>(model);

			calendar.UserCreatedId = currentUser.UserId;
			calendar.UserCreatedName = currentUser.FullName;

			if (HasPermission())
			{
				if (calendar.BeginTime == DateTime.MinValue)
				{
					return Json(new { error = true, message = "Chưa đặt thời gian họp;" }, JsonRequestBehavior.AllowGet);
				}

				if (string.IsNullOrWhiteSpace(calendar.Place))
				{
					return Json(new { error = true, message = "Chưa đặt địa điểm họp;" }, JsonRequestBehavior.AllowGet);
				}

				calendar.IsAccepted = true;
			}

			try
			{
				_calendarService.Create(calendar);
			}
			catch (Exception ex)
			{
				LogException(ex);
				return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
			}

			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Accept(int calendarId, bool isAccept)
		{
			if (!HasPermission())
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

			var calendar = _calendarService.Get(calendarId);
			if (calendar == null)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

			_calendarService.Accept(calendar, isAccept);

			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Delete(int calendarId)
		{
			var calendar = _calendarService.Get(calendarId);
			if (!HasPermission())
			{
				if (!calendar.IsPrivate || calendar.UserCreatedId != User.GetUserId())
				{
					return Json(false, JsonRequestBehavior.AllowGet);
				}
			}

			_calendarService.Delete(calendar);

			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Update(string model)
		{
			if (string.IsNullOrEmpty(model))
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

			var currentUser = _userService.CurrentUser;
			var calendarModel = Json2.ParseAs<Calendar>(model);

			try
			{
				_calendarService.Update(calendarModel);
			}
			catch (Exception ex)
			{
				return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
			}

			return Json(true, JsonRequestBehavior.AllowGet);
		}

		public JsonResult Get(int calendarId)
		{
			var calendar = _calendarService.Get(calendarId);
			if (calendar == null)
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}

			if (!HasPermission())
			{
				if (calendar.UserCreatedId != User.GetUserId())
				{
					return Json(false, JsonRequestBehavior.AllowGet);
				}
			}

			return Json(new { data = calendar }, JsonRequestBehavior.AllowGet);
		}

		private bool HasPermission()
		{
			var result = false;

			if (string.IsNullOrEmpty(_adminGeneralSettings.UserAcceptCalendarIds))
			{
				return result;
			}

			var currentUserId = User.GetUserId();
			var userHasAcceptCalendars = _adminGeneralSettings.UserAcceptCalendarIds
												.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
												.Select(u => int.Parse(u));

			return userHasAcceptCalendars.Contains(currentUserId);
		}

		#endregion

		#region Resource

		[HttpPost]
		public JsonResult AddResource(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return Json(true, JsonRequestBehavior.AllowGet);
			}

			_calendarService.AddResource(resource, User.GetUserId());
			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult DeleteResource(int id)
		{
			_calendarService.DeleteResource(id);
			return Json(true, JsonRequestBehavior.AllowGet);
		}

		#endregion
	}
}
