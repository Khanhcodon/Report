using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Utils;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Bkav.eGovCloud.Entities.Enum;
using Newtonsoft.Json.Linq;
using Bkav.eGovCloud.Entities.Admin.TimerJobSchedules;
using Bkav.eGovCloud.Business.Tasks;
using FluentScheduler;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Core.Caching;
using System.Data;
using System.IO;
using System.Text;
using Bkav.eGovCloud.Models;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public partial class DocumentController : CustomController
    {
        #region
        
        private const string DEFAULT_SORT_BY = "DocumentTypeName";
        private readonly DocumentBll _documentService;
        private readonly DocTypeBll _docTypeService;
        private readonly FormBll _formService;
        private readonly ReportModeBll _reportModeService;
        private readonly ResourceBll _resourceService;
        private readonly DocTypeTimeJobBll _docTypeTimeJobService;


        public DocumentController(
                DocumentBll documentService,
                DocTypeBll docTypeService,
                FormBll formService,
                ReportModeBll reportModeService,
                ResourceBll resourceService,
                DocTypeTimeJobBll docTypeTimeJobService
            )
            : base()
        {
            _documentService = documentService;
            _docTypeService = docTypeService;
            _formService = formService;
            _reportModeService = reportModeService;
            _resourceService = resourceService;
            _docTypeTimeJobService = docTypeTimeJobService;
        }
        #endregion end module

        [HttpGet]
        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocType.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.DocType.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }
            var model = _documentService.GetsAs(c =>
                    new DocumentOpenCloseModel
                    {
                        documentID = c.DocumentId,
                        docTypeID = c.DocTypeId,
                        CitizenName = c.CitizenName,
                        Compendium = c.Compendium,
                        DateCreated = c.DateCreated,
                        UserCreatedId = c.UserCreatedId,
                        Status = c.Status,
                        CategoryBusinessId = c.CategoryBusinessId,
                        InOutPlace = c.InOutPlace,
                        OrganizationCode = c.OrganizationCode,
                        UserCreatedName = c.UserCreatedName,
                        DocTypeName = c.DocTypeName,
                        TimeKey = c.TimeKey,
                        StatusReport = c.StatusReport,
                        StatusOpenClose = c.StatusOpenClose
                    }).Where(d => d.StatusOpenClose == true);

            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Document.OpenOrClose"));
            SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Document.OpenOrClose"));      
            return View(model);
        }
  


        public JsonResult TimeJobOutOfDate(Guid id, string OutOfDate,string typeOutOfDate)
        {
            var timeJobDocType = _docTypeTimeJobService.Get(id);
            if(timeJobDocType == null)
            {
                return null;
            }
            timeJobDocType.ScheduleConfigOutOfDate = OutOfDate;
            switch (typeOutOfDate)
            {
                case "HangNgayOutOfDate":
                    timeJobDocType.ScheduleTypeOutOfDate = 3;
                    break;
                case "HangTuanOutOfDate":
                    timeJobDocType.ScheduleTypeOutOfDate = 4;
                    break;
                case "HangThangDueDatOutOfDate":
                    timeJobDocType.ScheduleTypeOutOfDate = 5;
                    break;
                case "HangQuyOutOfDate":
                    timeJobDocType.ScheduleTypeOutOfDate = 6;
                    break;
                case "HangNamOutOfDate":
                    timeJobDocType.ScheduleTypeOutOfDate = 7;
                    break;
            }
            _docTypeTimeJobService.SaveChanges();
            return Json(timeJobDocType, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloseOrEdit(Guid id)
        {
            var doc = _documentService.Get(id);
            if (doc == null)
            {
                return null;
            }
            doc.StatusOpenClose = false;
            try
            {
                _documentService.Update(doc);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Document.OpenOrClose"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Document.OpenOrClose"));
                return RedirectToAction("Index","Document");
            }
            catch(Exception ex)
            {

            } 
            return RedirectToAction("Index","Document");
        }

        /// <summary>
        /// gia hạn báo cáo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ExtendBC(Guid id)
        {
            var docType = _docTypeTimeJobService.Get(id);
            if(docType == null)
            {
                return null;
            }
            return Json(docType, JsonRequestBehavior.AllowGet);
        }
    }
}