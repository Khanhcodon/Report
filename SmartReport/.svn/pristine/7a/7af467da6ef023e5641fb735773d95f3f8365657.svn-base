using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    public class ParallelController : AsyncController
    {
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly DocColumnSettingBll _docColumnSettingSerice;
        private readonly NotificationBll _notificationService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly MemoryCacheManager _cache;

        public ParallelController(ProcessFunctionBll processFunctionService, NotificationBll notificationService,
                    DocColumnSettingBll docColumnSettingSerice,
                    Helper.UserSetting helperUserSetting, MemoryCacheManager cache)
        {
            _processFunctionService = processFunctionService;
            _notificationService = notificationService;
            _docColumnSettingSerice = docColumnSettingSerice;
            _helperUserSetting = helperUserSetting;
            _cache = cache;
        }

        #region Cây văn bản

        [HttpPost]
        public void GetDocumentsAsync(int id, GetDocumentParameters parameters, string paramsQuery = null)
        {
            var userId = User.GetUserId();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var result = GetDocuments(id, parameters, userId, paramsQuery);
                return new { result };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["result"] = t.Result.result;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult GetDocumentsCompleted(dynamic result)
        {
            return Json(result);
        }

        private dynamic GetDocuments(int id, GetDocumentParameters parameters, int userId, string paramsQuery = null)
        {
            var function = _processFunctionService.GetFromCache(id);
            if (function == null)
            {
                return null;
            }

            IEnumerable<IDictionary<string, object>> documents;

            if (!string.IsNullOrWhiteSpace(paramsQuery) && paramsQuery != "[]")
            {
                var objectParams = Json2.ParseAs<List<ObjectParams>>(paramsQuery);
                if (objectParams != null && objectParams.Any())
                {
                    foreach (var item in objectParams)
                    {
                        parameters.Params.Add(item.Key, item.Value);
                    }
                }
            }

            var pageSize = 100;

            documents = _processFunctionService.GetDocumentOlderByFunction(function, DateTime.Now, pageSize + 1, userId, parameters.Params);

            var listSetting = _docColumnSettingSerice.GetAllCaches().FirstOrDefault(p => p.DocColumnSettingId == function.DocColumnSettingId);

            var functionType = function.ProcessFunctionType;
            if (functionType != null)
            {
                var paramDocField = functionType.ListParam.FirstOrDefault(p => p.ValueField == "DocFieldId");
                if (paramDocField != null)
                {
                    if (!parameters.Params.ContainsKey(paramDocField.ParamName))
                    {
                        throw new Exception("Chuỗi query string không có tham số " + paramDocField.ParamName);
                    }
                }
            }

            var columnSetting = listSetting != null
                             ? Json2.ParseAs<List<Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting>>(listSetting.DisplayColumn)
                             : new List<Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting>();

            var docTypeColumnSettings = new Dictionary<string, List<Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting>>();

            if (documents.Any())
            {
                var listColumnNames = documents.First().Keys;

                if (columnSetting.Count == 0)
                {
                    columnSetting.AddRange(listColumnNames.Select(columnName => new Bkav.eGovCloud.Entities.Customer.Settings.ColumnSetting
                    {
                        ColumnName = columnName,
                        DisplayName = columnName
                    }));
                }
            }

            return new
            {
                totalUnread = 0,
                page = 1,
                pageSize = pageSize,
                totalDocuments = 0,
                enablePaging = function.IsEnablePaging,
                isFilterByDocFieldDocType = false,
                dateFilter = string.IsNullOrEmpty(function.DateFilter) ? "" : function.DateFilter,
                dateFilterView = string.IsNullOrEmpty(function.DateFilterView) ? "" : function.DateFilterView,
                isOverdueFilter = function.IsOverdueFilter,
                isDateFilter = function.IsDateFilter,
                columnSetting = columnSetting,
                documents = documents,
                docTypeColumnSettings
            };
        }

        [HttpPost]
        public void GetTotalDocumentUnreadMultiFunctionAsync(string ids)
        {
            var functionIds = string.IsNullOrEmpty(ids)? new List<int>() : Json2.ParseAs<IEnumerable<int>>(ids);
            var userId = User.GetUserId();

            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var result = new List<dynamic>();
#if !DEBUG
                result = _processFunctionService.GetTotalDocumentUnreadMultiFunction(functionIds, userId);
#endif
                result = _processFunctionService.GetTotalDocumentUnreadMultiFunction(functionIds, userId);
                return new { result };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["result"] = t.Result.result;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult GetTotalDocumentUnreadMultiFunctionCompleted(List<dynamic> result)
        {
            return Json(result);
        }

        #endregion

        #region Xử lý văn bản

        public JsonResult GetDocumentInfoFromScan(List<string> ids)
        {
            List<DocumentInfoFromImage> result = new List<DocumentInfoFromImage>();

            var hasAutoInsertDocumentInfoScan = _helperUserSetting.GetUserCurrentSetting().AutoInsertDocumentInfoScan;
            if (!hasAutoInsertDocumentInfoScan)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var pdfParser = new PdfParser(48);
            var imageToText = new ImageToText();
            var tempPath = ResourceLocation.Default.FileUploadTemp + "/";

            var cacheKey = CacheParam.DocumentInfoFromImageCache;

            foreach (var id in ids)
            {
                ThreadPool.QueueUserWorkItem(fileId =>
                {
                    DocumentInfoFromImage docInfo = null;
                    var file = Path.Combine(tempPath, id);
                    var imageInfo = pdfParser.ConvertToImages(file, tempPath, 1, 0, 300);
                    docInfo = imageToText.GetDocumentInfo(tempPath + imageInfo[0]);

                    _cache.Set(string.Format(cacheKey, fileId), docInfo, 30);
                }, id);
            }

            Thread.Sleep(8000);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void GetDocumentInfoFromScan1Async(List<string> ids)
        {
            var pdfParser = new PdfParser(48);
            var tempPath = ResourceLocation.Default.FileUploadTemp + "/";
            var hasAutoInsertDocumentInfoScan = _helperUserSetting.GetUserCurrentSetting().AutoInsertDocumentInfoScan;
            var imageToText = new ImageToText();

            List<DocumentInfoFromImage> abc = new List<DocumentInfoFromImage>();

            AsyncManager.OutstandingOperations.Increment();

            var task =
                ids.Select(id => Task.Factory.StartNew(() =>
                {
                    DocumentInfoFromImage result = null;
                    if (hasAutoInsertDocumentInfoScan)
                    {
                        var file = Path.Combine(tempPath, id);
                        var imageInfo = pdfParser.ConvertToImages(file, tempPath, 1, 0, 300);
                        result = imageToText.GetDocumentInfo(tempPath + imageInfo[0]);
                    }
                    return new { result };
                })).ToList();

            Task.Factory.ContinueWhenAll(task.ToArray(), tasks =>
            {
                var result = tasks.Select(item => item.Result);
                AsyncManager.Parameters["result"] = result;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult GetDocumentInfoFromScan1Completed(dynamic result)
        {
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Notification

        public void Notification_GetNotSentsAsync(int typeNotification = 0, int pageSize = 30)
        {
            var user = User.GetUserId();

            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
				var result = _notificationService.GetNotSents(user, typeNotification, pageSize);
                return new { result };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["result"] = t.Result.result;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult Notification_GetNotSentsCompleted(IEnumerable<Notifications> result)
        {
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}