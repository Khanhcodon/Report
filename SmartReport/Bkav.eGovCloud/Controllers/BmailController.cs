using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class BmailController : CustomerBaseController
    {
        private readonly SendSmsHelper _smsHelper;
        private readonly SmsSettings _smsSettings;
        private readonly EmailSettings _emailSettings;

        public BmailController(SendSmsHelper smsHelper,
            SmsSettings smsSettings, EmailSettings emailSettings)
        {
            _smsHelper = smsHelper;
            _smsSettings = smsSettings;
            _emailSettings = emailSettings;
        }

        //
        // GET: /Bmail/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public void Meetings(bool sms, string t, string r, string d, string u, string endDate, string created, string body, bool lastUpdate)
        {
            try
            {
                var users = new List<string>();
                var us = u.Split(',');
                if (us.Length > 0)
                {
                    users = us.ToList();
                }
                if (!users.Any())
                {
                    return;
                }
                var meeting = new Meeting()
                {
                    Title = t,
                    Resource = r,
                    Date = d,
                    ToDate = endDate,
                    Creater = created,
                    LastUpdate = lastUpdate,
                    Body = body,
                    Users = users
                };
                if (sms)
                {
                    if (_smsSettings.SentWhenHasMeeting)
                    {
                        // _smsHelper.SendMeetingSms(meeting);
                    }
                }
                else
                {
                    // _mailHelper.SendMeetingAlert(meeting);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        [HttpGet]
        [JsonpFilter]
        public JsonResult MeetingSetting()
        {
            if (!_smsSettings.SentWhenHasMeeting)
            {
                return Json(new { m = -1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { m = _smsSettings.BeforeMinute }, JsonRequestBehavior.AllowGet);
        }
    }

    public class Meeting
    {
        public string Title { get; set; }

        public string Resource { get; set; }

        public string Date { get; set; }

        public string ToDate { get; set; }

        public string Body { get; set; }

        public string Creater { get; set; }

        public bool LastUpdate { get; set; }

        public List<string> Users { get; set; }
    }

    public class JsonpFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            //
            // see if this request included a "callback" querystring parameter
            //
            string callback = filterContext.HttpContext.Request.QueryString["callback"];
            if (callback != null && callback.Length > 0)
            {
                //
                // ensure that the result is a "JsonResult"
                //
                JsonResult result = filterContext.Result as JsonResult;
                if (result == null)
                {
                    throw new InvalidOperationException("JsonpFilterAttribute must be applied only " +
                        "on controllers and actions that return a JsonResult object.");
                }

                filterContext.Result = new JsonpResult
                {
                    ContentEncoding = result.ContentEncoding,
                    ContentType = result.ContentType,
                    Data = result.Data
                };
            }
        }
    }
}