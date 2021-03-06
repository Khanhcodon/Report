using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Core.Utils;
using System.Linq;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Areas.Admin.Models;

namespace Bkav.eGovCloud.Controllers
{
    /// <summary>
    /// 
    /// timg kiếm các document
    /// </summary>
    public class SearchDocumentController : CustomerBaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly EgovSearch _searchService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly ISearchInDatabase _searchInDatabaseService;
        private readonly ISearchInSolr _searchInSolrService;
        private readonly ReportModeBll _reportModeService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocumentBll _documentService;
        private readonly AttachmentBll _attachmentService;
        private readonly DocumentCopyBll _documentCopyService;
        private readonly ReportRuleBll _reportRuleService;
        private readonly AttachmentDetailBll _attachmentDetailSerive;
        private readonly DepartmentBll _departmentSerive;

        // <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="generalSettings"></param>
        /// <param name="searchService"></param>
        /// <param name="searchInDatabaseService"></param>
        /// <param name="searchInSolrService"></param>
        public SearchDocumentController(
            AdminGeneralSettings generalSettings, 
            EgovSearch searchService,
            ISearchInDatabase searchInDatabaseService, 
            ISearchInSolr searchInSolrService,
            Helper.UserSetting helperUserSetting,
            ReportModeBll reportModels,
            DocTypeBll doctypeService,
            DocumentBll documentService,
            AttachmentBll attachmentService,
            DocumentCopyBll documentCopyService,
            ReportRuleBll reportRuleService,
            AttachmentDetailBll attachmentDetailSerive,
            DepartmentBll departmentSerive)
        {
            _generalSettings = generalSettings;
            _searchService = searchService;
            _searchInDatabaseService = searchInDatabaseService;
            _searchInSolrService = searchInSolrService;
            _helperUserSetting = helperUserSetting;
            _reportModeService = reportModels;
            _docTypeService = doctypeService;
            _documentService = documentService;
            _attachmentService = attachmentService;
            _documentCopyService = documentCopyService;
            _reportRuleService = reportRuleService;
            _attachmentDetailSerive = attachmentDetailSerive;
            _departmentSerive = departmentSerive;
        }

        public ActionResult Index(int type = 0)
        {
            // xử lý reportmode
            if (type == 0) {
                var reportModels = _reportModeService.Gets();
                var listReportModels = new List<SelectListItem>();
                listReportModels.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = "Chọn tất cả",
                    Value = "-1"
                });
                foreach (var reportList in reportModels)
                {
                    listReportModels.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = reportList.Name,
                        Value = Convert.ToString(reportList.ReportModeId),
                    });
                }
                ViewBag.ListReportModels = listReportModels;
            } else if (type == 1)
            {
                var reportModels = _reportModeService.Gets(d => d.ReportModeId == 2 || d.ReportModeId == 3);
                var listReportModels = new List<SelectListItem>();
                listReportModels.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = "Chọn tất cả",
                    Value = "-1"
                });
                foreach (var reportList in reportModels)
                {
                    listReportModels.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = reportList.Name,
                        Value = Convert.ToString(reportList.ReportModeId),
                    });
                }
                ViewBag.ListReportModels = listReportModels;
            }
            else if (type == 2)
            {
                var reportModels = _reportModeService.Gets(d => d.ReportModeId == 5 || d.ReportModeId == 6);
                var listReportModels = new List<SelectListItem>();
                listReportModels.Add(new SelectListItem()
                {
                    Selected = true,
                    Text = "Chọn tất cả",
                    Value = "-1"
                });
                foreach (var reportList in reportModels)
                {
                    listReportModels.Add(new SelectListItem()
                    {
                        Selected = false,
                        Text = reportList.Name,
                        Value = Convert.ToString(reportList.ReportModeId),
                    });
                }
                ViewBag.ListReportModels = listReportModels;
            }


            #region begin compendium
            var getCompendium = GetCompendium();
            var listCompendium = new List<SelectListItem>();
            listCompendium.Add(new SelectListItem()
            {
                Selected = true,
                Text = "Chọn tất cả",
                Value = "-1"
            });
            foreach (var item in getCompendium)
            {
                listCompendium.Add(new SelectListItem()
                {
                    Selected = false,
                    Text = item.Value,
                    Value = item.Value
                });
            }
            ViewBag.ListCompendium = listCompendium;

            var getAttachment = GetAttachment();
            var listAttachment = new List<SelectListItem>();
            listAttachment.Add(new SelectListItem()
            {
                Selected = true,
                Text = "Chọn tất cả",
                Value = "-1"
            });
            foreach (var item in getAttachment)
            {
                listAttachment.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.Key.ToString(),
                    Text = item.Value
                });
            }
            ViewBag.ListAttachment = listAttachment;
            #endregion

            #region begin rule
            var reportRules = _reportRuleService.Gets(d => d.IsActive == true);
            var listReportRules = new List<SelectListItem>();
            listReportRules.Add(new SelectListItem()
            {
                Selected = true,
                Text = "Chọn tất cả",
                Value = "-1"
            });
            if (type == 1)
            {
                reportRules = reportRules.Where(d => d.ReportRuleId == 21 || d.ReportRuleId == 20);
            }
            if(type == 2)
            {
                reportRules = reportRules.Where(d => d.ReportRuleId == 17 || d.ReportRuleId == 18);
            }

            foreach (var item in reportRules)
            {
                listReportRules.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.ReportRuleId.ToString(),
                    Text = item.Name
                });
            }    
            ViewBag.ListReportRules = listReportRules;
            #endregion and rule

            #region department
            //select list department
            var listDepartment = _departmentSerive.GetReadOnlys();
            var listSelect = new List<SelectListItem>();
            listSelect.Add(new SelectListItem()
            {
                Selected = true,
                Text = "Chưa chọn đơn vị",
                Value = ""
            });
            foreach (var item in listDepartment)
            {
                listSelect.Add(new SelectListItem()
                {
                    Selected = false,
                    Value = item.DepartmentId.ToString(),
                    Text = item.DepartmentName
                });
            }
            ViewBag.ListSelectDepartment = listSelect;
            #endregion and department
            return View();
        }

        public ActionResult SearchDetail(int documentCopyId)
        {
            var documentCopyOnly = _documentCopyService.Get(documentCopyId);
            try
            {
                if (documentCopyOnly != null)
                {
                    var documentList = _documentService.Get(documentCopyOnly.DocumentId);
                    // check exitFile
                    var checkTypeFile = _attachmentService.Gets(d => d.DocumentId == documentList.DocumentId)
                                        .Join(_attachmentDetailSerive.Gets(),
                                        p => p.AttachmentId,
                                        c => c.AttachmentId,
                                        (p, c) => new ObjAtt
                                        {
                                            AttachmentId = p.AttachmentId,
                                            DocumentId = p.DocumentId,
                                            AttachmentName = p.AttachmentName,
                                            CreatedOnDate = c.CreatedOnDate
                                        }).OrderByDescending(c => c.CreatedOnDate);
                    if (checkTypeFile != null)
                    {
                        ViewBag.checkFile = 1;
                        ViewBag.CountAtt = checkTypeFile.Count();
                        if(ViewBag.CountAtt == 1)
                        {
                            ViewBag.DocId = checkTypeFile.ToList()[0].DocumentId;
                            ViewBag.AttId = checkTypeFile.ToList()[0].AttachmentId;
                        }
                        else
                        {
                            ViewBag.ListAttachFile = checkTypeFile;
                        }
                        
                    }
                    else
                    {
                        ViewBag.checkFile = 0;
                        ViewBag.NameReport = documentList.Compendium;
                        ViewBag.ListDocument = documentList.DocumentId;
                    }
                }
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
            return View();
        }
    
        /// <summary>
        /// Tìm kiếm văn bản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult SearchDocument(SearchAdvangeModel model)
        {
            //return Json(new { }, JsonRequestBehavior.AllowGet);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ResultSaveLocal()
        {
            var result = new List<SearchAdvangeModel>();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// hiển thị file đính kèm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetFileData(int id)
        {

            var reportFile = _attachmentService.Get(id);
            if (reportFile == null)
            {
                return null;
            }
            var result = new ReportFileModel()
            {
                DocumentId = reportFile.DocumentId,
                AttachmentId = reportFile.AttachmentId
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Tìm kiếm nhanh
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult QuickSearch(string query, int? page = 1)
        {
            var userSetting = _helperUserSetting.GetUserCurrentSetting(editable: false);
            var model = new SearchAdvangeModel
            {
                SearchType = SearchType.Document,
                Compendium = query,
                Content = query,
                Page = page ?? 1,
                IsUseCached = false,
                PageSize = _generalSettings.DefaultPageSize,
                IsMainProcess = userSetting.FindProcessDocument
            };
            var result = SearchDocumentQS(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private SearchDocumentResultModel SearchDocumentQS(SearchAdvangeModel model)
        {
            var userID = User.GetUserId();
            var result = new SearchDocumentResultModel();

            if (string.IsNullOrWhiteSpace(model.Compendium))
            {
                result = new SearchView().ToModel();
                result.SearchType = (int)SearchType.Document;
                return result;
            }

            var docs = _searchInDatabaseService.QuickSearch(userID, model.Compendium, model.IsUseCached, model.Page, model.PageSize, model.IsMainProcess);

            result = docs.ToModel();
            result.SearchType = (int)SearchType.Document;
            return result;
        }

        public JsonResult SearchAdvanceDocument(SearchAdvangeModel model)
        {
            if (model.SearchType == 0)
            {
                model.SearchType = SearchType.Document;
            }

            if(model.Page == 0)
            {
                model.Page = 1;
            }

            if(model.PageSize == 0)
            {
                model.PageSize = _generalSettings.DefaultPageSize;
            }

            if(model.FromDateStr == null)
            {
                var from = DateTime.Now.AddMonths(-12);
                model.FromDateStr = from.ToString("dd/MM/yyyy").Replace(".", "/");
            }

            var result = SearchAdvanceBusDocument(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public Dictionary<Guid, string> GetCompendium()
        {
            var result = new Dictionary<Guid, string>();
            using (var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
            {
                var stringQuery = "Select dt.DocTypeName,dt.DocTypeId FROM `doctype` dt INNER JOIN `reportmodes` rm on rm.ReportModeId = dt.ReportModeId Group BY dt.DocTypeId";
                var resultUserCreateName = context.RawQuery(stringQuery);
                foreach (var item in resultUserCreateName)
                {
                    result.Add(item.DocTypeId, item.DocTypeName);
                }
                return result;
            }
        }
        public Dictionary<int, string> GetAttachment()
        {
            var result = new Dictionary<int, string>();
            using(var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
            {
                var stringQuery = "SELECT att.AttachmentId, "
                    + "att.AttachmentName from attachment as att "
                    + "INNER JOIN attachment_detail as att_dt ON att.AttachmentId = att_dt.AttachmentId "
                    + " INNER JOIN document as d ON att.DocumentId = d.DocumentId";
                var resultUserCreateName = context.RawQuery(stringQuery);
                foreach (var item in resultUserCreateName)
                {
                    result.Add(item.AttachmentId, item.AttachmentName);
                }
                return result;
            }
        }

        public JsonResult RenderCompendiumData(int reportmodeId)
        {
            var listObj = new List<ObjQuery>();
            using (var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
            {
                var stringQuery = "";
                if (reportmodeId == -1)
                {
                    stringQuery = "Select dt.DocTypeName,dt.ReportModeId FROM `doctype` dt INNER JOIN `reportmodes` rm on rm.ReportModeId = dt.ReportModeId Group BY dt.DocTypeId";
                }
                else
                {
                    stringQuery = "Select dt.DocTypeName,dt.ReportModeId FROM `doctype` dt INNER JOIN `reportmodes` "
                    + "rm on rm.ReportModeId = dt.ReportModeId where rm.ReportModeId = " + reportmodeId + " Group BY dt.DocTypeId";
                }

                var resultUserCreateName = context.RawQuery(stringQuery);
                foreach (var item in resultUserCreateName)
                {
                    if (reportmodeId == -1)
                    {
                        listObj.Add(new ObjQuery()
                        {
                            ReportModeId = item.ReportModeId,
                            DocTypeName = item.DocTypeName
                        });
                    }
                    else
                    {
                        listObj.Add(new ObjQuery()
                        {
                            ReportModeId = item.ReportModeId,
                            DocTypeName = item.DocTypeName
                        });
                    }

                }
            }
            return Json(listObj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult renderReportRuleDetai(int reportRuleId)
        {
            var result = _reportRuleService.Get(reportRuleId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RenderReportRuleArray(List<int> reportmodeId)
        {
            var listObj = new List<ObjQuery>();
            using (var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
            {
                var stringQuery = "";

                foreach(var item in reportmodeId)
                {
                    stringQuery = "SELECT reportmodes.ReportModeId, reportmodes.`Name` from reportmodes WHERE reportmodes.ReportModeId = " + item + "";
                    var resultUserCreateName = context.RawQuery(stringQuery);
                    foreach (var itemU in resultUserCreateName)
                    { 
                        listObj.Add(new ObjQuery()
                        {
                            ReportModeId = itemU.ReportModeId,
                            DocTypeName = itemU.Name
                        });
 
                    }
                }    
            }
            return Json(listObj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RenderReportRule(int reportmodeId)
        {
            var listObj = new List<ObjQuery>();
            using (var context = new EfContext(new MySqlConnection(DataSettings.Current.DataConnectionString)))
            {
                var stringQuery = "";
                if (reportmodeId == -1)
                {
                    stringQuery = "SELECT reportmodes.ReportModeId, reportmodes.`Name` from reportmodes";
                }
                else
                {
                    stringQuery = "SELECT reportmodes.ReportModeId, reportmodes.`Name` from reportmodes WHERE reportmodes.ReportModeId = " + reportmodeId + "";
                }
                
                var resultUserCreateName = context.RawQuery(stringQuery);
                foreach(var item in resultUserCreateName)
                {
                    if (reportmodeId == -1)
                    {
                        listObj.Add(new ObjQuery()
                        {
                            ReportModeId = item.ReportModeId,
                            DocTypeName = item.Name
                        });
                    }
                    else
                    {
                        listObj.Add(new ObjQuery()
                        {
                            ReportModeId = item.ReportModeId,
                            DocTypeName = item.Name
                        });
                    }
                    
                }
            }
            return Json(listObj, JsonRequestBehavior.AllowGet);
        }
        public class ObjQuery
        {
            public int ReportModeId { get; set; }
            public string DocTypeName { get; set; }
        }
        private SearchDocumentResultModel SearchAdvanceBusDocument(SearchAdvangeModel model)
        {      
  
            var userID = User.GetUserId();
            model.Compendium = model.Compendium == null ? "" : model.Compendium.Trim();

            model.DocCode = model.DocCode == null ? "" : model.DocCode.Trim();
            model.InOutCode = model.InOutCode == null ? "" : model.InOutCode.Trim();

            model.OrganizationCreate = model.OrganizationCreate == null ? "" : model.OrganizationCreate.Trim();
            model.Content = model.Content == null ? "" : model.Content.Trim();

            model.DocTypeCode = model.DocTypeCode == null ? "" : model.DocTypeCode.Trim();
            model.InOutPlace = model.InOutPlace == null ? "" : model.InOutPlace.Trim();
            model.UserCreatedName = model.UserCreatedName == null ? "" : model.UserCreatedName.Trim();

            model.TimeKey = model.TimeKey == null ? "" : model.TimeKey.Trim();

            model.UnitDelivery = model.UnitDelivery == null ? null : model.UnitDelivery;
            model.UnitReceive = model.UnitReceive == null ? null : model.UnitReceive;

            ViewBag.SearchModelDocument = model;
            var result = new SearchDocumentResultModel();

            if (model.SearchType == SearchType.File)
            {
                var fileResultDocument = _searchService.Search(userID, model.Compendium, model.Content, model.CategoryId, model.KeyWord,
                    model.DocCode, model.InOutCode, model.UrgentId, model.CategoryBusinessId, model.StorePrivateId, model.CurrentUserId, model.FromDate,
                    model.ToDate, model.InOutPlaceId, model.OrganizationCreate, model.DocFieldId, model.UserSuccessId, model.Page, model.PageSize).ToModel() ??
                    new SearchDocumentResultModel
                    {
                        Items = new List<SearchDocumentItemResultModel>(),
                        FacetCreatedDate = new List<KeyValuePair<string, int>>(),
                        FacetDocField = new List<KeyValuePair<string, KeyValuePair<string, int>>>(),
                        FacetDocType = new List<KeyValuePair<string, KeyValuePair<string, int>>>()
                    };
                model.TotalPage = fileResultDocument.TotalResult;
                return fileResultDocument;
            }

            /*
            if(model.ReportModeId != null)
            {
                var reportModeId = _reportModeService.Get(Convert.ToInt32(model.ReportModeId));
                var docTypeList = _docTypeService.Gets(x => x.ReportModeId == reportModeId.ReportModeId);
                foreach (var docc in docTypeList)
                {
                    model.DocTypeId = docc.DocTypeId;
                }
            }
            else
            {
                model.DocTypeId = null;
            }
            */           
            model.ReportModeId = model.ReportModeId == null ? null : model.ReportModeId;
            model.ReportRuleIdOnly = model.ReportRuleIdOnly == null ? null : model.ReportRuleIdOnly;

            var docs = _searchInDatabaseService.SearchAdvance(new Search.Entity.SearchQuery()
            {
                Compendium = model.Compendium,
                UserId = userID,
                CategoryId = model.CategoryId,
                CategoryBusinessId = model.CategoryBusinessId,
                DocCode = model.DocCode,
                InOutCode = model.InOutCode,
                UrgentId = model.UrgentId,
                StoreId = model.StoreId,
                StorePrivateId = model.StorePrivateId,
                CurrentUserId = model.CurrentUserId,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                FromPubDate = model.FromPubDate,
                ToPubDate = model.ToPubDate,
                InOutPlaceId = model.InOutPlaceId,
                Organization = model.OrganizationCreate,
                CreatedUserId = model.UserCreateId,
                SuccessedUserId = model.UserSuccessId,
                IsUseCached = model.IsUseCached,
                DocTypeCode = model.DocTypeCode,
                ReportModeId = model.ReportModeId,
                DocTypeId = model.DocTypeId,
                InOutPlace = model.InOutPlace,
                UserCreatedName = model.UserCreatedName,
                Status = model.Status,
                ActionLevel = model.ActionLevel,
                TimeKey = model.TimeKey,
                ReportRuleIdOnly = model.ReportRuleIdOnly,
                UnitDelivery = model.UnitDelivery,
                UnitReceive = model.UnitReceive

            }, model.Page, 25, model.IsMainProcess);

            result = docs.ToModel();
            result.SearchType = (int)model.SearchType;
            return result;
        }

        private SearchDocumentResultModel SearchFileDocument(SearchAdvangeModel model)
        {
            var userId = User.GetUserId();

            var result = new SearchDocumentResultModel();

            if (string.IsNullOrWhiteSpace(model.Compendium))
            {
                result = new SearchView().ToModel();
                result.SearchType = (int)model.SearchType;
                return result;
            }

            // Search in Solr
            var docs = _searchInSolrService.QuickSearchInFile(userId, model.Compendium, model.Page, model.PageSize);
            result = docs.ToModel();
            result.SearchType = (int)SearchType.File;
            return result;
        }


        private static bool isFirstMergeRange(ExcelWorksheet sheet, string address, ref int colspan, ref int rowspan)
        {
            colspan = 1;
            rowspan = 1;
            foreach (var item in sheet.MergedCells)
            {
                var s = item.Split(':');
                if (s.Length > 0 && s[0].Equals(address))
                {

                    ExcelRange range = sheet.Cells[item];
                    colspan = range.End.Column - range.Start.Column;
                    rowspan = range.End.Row - range.Start.Row;
                    if (colspan == 0) colspan = 1;
                    if (rowspan == 0) rowspan = 1;
                    return true;
                }
            }
            return false;
        }

        internal static void ConvertContentTo(HtmlNode node, TextWriter outText, PreceedingDomTextInfo textInfo)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText, textInfo);
            }
        }
        internal static void ConvertTo(HtmlNode node, TextWriter outText, PreceedingDomTextInfo textInfo)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;
                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText, textInfo);
                    break;
                case HtmlNodeType.Text:
                    // script and style must not be output
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                    {
                        break;
                    }
                    // get text
                    html = ((HtmlTextNode)node).Text;
                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                    {
                        break;
                    }
                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Length == 0)
                    {
                        break;
                    }
                    if (!textInfo.WritePrecedingWhiteSpace || textInfo.LastCharWasSpace)
                    {
                        html = html.TrimStart();
                        if (html.Length == 0) { break; }
                        textInfo.IsFirstTextOfDocWritten.Value = textInfo.WritePrecedingWhiteSpace = true;
                    }
                    outText.Write(HtmlEntity.DeEntitize(Regex.Replace(html.TrimEnd(), @"\s{2,}", " ")));
                    if (textInfo.LastCharWasSpace = char.IsWhiteSpace(html[html.Length - 1]))
                    {
                        outText.Write(' ');
                    }
                    break;
                case HtmlNodeType.Element:
                    string endElementString = null;
                    bool isInline;
                    bool skip = false;
                    int listIndex = 0;
                    switch (node.Name)
                    {
                        case "nav":
                            skip = true;
                            isInline = false;
                            break;
                        case "body":
                        case "section":
                        case "article":
                        case "aside":
                        case "h1":
                        case "h2":
                        case "header":
                        case "footer":
                        case "address":
                        case "main":
                        case "div":
                        case "p": // stylistic - adjust as you tend to use
                            if (textInfo.IsFirstTextOfDocWritten)
                            {
                                outText.Write("\r\n");
                            }
                            endElementString = "\r\n";
                            isInline = false;
                            break;
                        case "br":
                            outText.Write("\r\n");
                            skip = true;
                            textInfo.WritePrecedingWhiteSpace = false;
                            isInline = true;
                            break;
                        case "a":
                            if (node.Attributes.Contains("href"))
                            {
                                string href = node.Attributes["href"].Value.Trim();
                                if (node.InnerText.IndexOf(href, StringComparison.InvariantCultureIgnoreCase) == -1)
                                {
                                    endElementString = "<" + href + ">";
                                }
                            }
                            isInline = true;
                            break;
                        case "li":
                            if (textInfo.ListIndex > 0)
                            {
                                outText.Write("\r\n{0}.\t", textInfo.ListIndex++);
                            }
                            else
                            {
                                outText.Write("\r\n*\t"); //using '*' as bullet char, with tab after, but whatever you want eg "\t->", if utf-8 0x2022
                            }
                            isInline = false;
                            break;
                        case "ol":
                            listIndex = 1;
                            goto case "ul";
                        case "ul": //not handling nested lists any differently at this stage - that is getting close to rendering problems
                            endElementString = "\r\n";
                            isInline = false;
                            break;
                        case "img": //inline-block in reality
                            if (node.Attributes.Contains("alt"))
                            {
                                outText.Write('[' + node.Attributes["alt"].Value);
                                endElementString = "]";
                            }
                            if (node.Attributes.Contains("src"))
                            {
                                outText.Write('<' + node.Attributes["src"].Value + '>');
                            }
                            isInline = true;
                            break;
                        default:
                            isInline = true;
                            break;
                    }
                    if (!skip && node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText, isInline ? textInfo : new PreceedingDomTextInfo(textInfo.IsFirstTextOfDocWritten) { ListIndex = listIndex });
                    }
                    if (endElementString != null)
                    {
                        outText.Write(endElementString);
                    }
                    break;
            }
        }

        public static string ConvertDoc(HtmlAgilityPack.HtmlDocument doc)
        {
            using (StringWriter sw = new StringWriter())
            {
                ConvertTo(doc.DocumentNode, sw);
                sw.Flush();
                return sw.ToString();
            }
        }
        public static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            ConvertTo(node, outText, new PreceedingDomTextInfo(false));
        }
        internal class PreceedingDomTextInfo
        {
            public PreceedingDomTextInfo(BoolWrapper isFirstTextOfDocWritten)
            {
                IsFirstTextOfDocWritten = isFirstTextOfDocWritten;
            }
            public bool WritePrecedingWhiteSpace { get; set; }
            public bool LastCharWasSpace { get; set; }
            public readonly BoolWrapper IsFirstTextOfDocWritten;
            public int ListIndex { get; set; }
        }
        internal class BoolWrapper
        {
            public BoolWrapper() { }
            public bool Value { get; set; }
            public static implicit operator bool(BoolWrapper boolWrapper)
            {
                return boolWrapper.Value;
            }
            public static implicit operator BoolWrapper(bool boolWrapper)
            {
                return new BoolWrapper { Value = boolWrapper };
            }
        }
        //Export File
        [HttpPost]
        [ValidateInput(false)]
        public string exportToExcel(string header, string data, string headerSeting,
            string headerNested, string columnSetting, string colWidths, string classCells, 
            /*string stringHeader, string stringFooter,*/ string fileName)
        {           
            var header_ = new JArray();
            var data_ = new JArray();
            var headerSeting_ = new JArray();
            var headerNested_ = new JArray();
            var columnSetting_ = new JArray();
            var colWidths_ = new JArray();
            var classCells_ = new JArray();

            header_ = (JArray)JsonConvert.DeserializeObject(header);
            data_ = (JArray)JsonConvert.DeserializeObject(data);
            headerSeting_ = (JArray)JsonConvert.DeserializeObject(headerSeting);
            headerNested_  = (JArray)JsonConvert.DeserializeObject(headerNested);
            columnSetting_ = (JArray)JsonConvert.DeserializeObject(columnSetting);
            colWidths_ = (JArray)JsonConvert.DeserializeObject(colWidths);
            classCells_ = (JArray)JsonConvert.DeserializeObject(classCells);

            int headerCount = headerSeting_.Count;
            //for(var i = 0; i < headerSeting_.Count; i++)
            //{
            //    var item = (JArray)headerSeting_[i];
            //    headerCount = i;
            //}

            var memoryStream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var ExcelPkg = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add(fileName);
                exportHeader(wsSheet1, headerSeting_, colWidths_, columnSetting_, headerNested_);
                exportData(wsSheet1, headerCount, data_, header_, classCells_, headerSeting_);

                wsSheet1.Protection.IsProtected = false;
                wsSheet1.Protection.AllowSelectLockedCells = false;               

                string date_now = DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fffffff");
                fileName += "-" + date_now;
                string FileName = ReplaceInvalidChars(fileName);
                FileName = FileName.Replace("&", "");
                FileName = FileName.Replace("!", "");
                FileName = FileName.Replace("@", "");
                FileName = FileName.Replace("$", "");
                FileName = FileName.Replace("%", "");
                var path = Path.Combine(ResourceLocation.Default.FileTemp, FileName + ".xlsx");

                System.IO.File.WriteAllBytes(path, ExcelPkg.GetAsByteArray());

                return "/Temp/" + FileName + ".xlsx";
            }

            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //ExcelPackage ExcelPkg = new ExcelPackage();
            //ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add(fileName);

            //test header and footer
            /*
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(stringHeader);
            var strRender = ConvertDoc(doc);
            wsSheet1.Cells[string.Format("A{0}", (1))].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            wsSheet1.Cells[string.Format("A{0}", (1))].Value = strRender;
            doc.LoadHtml(stringFooter);
            var strFooter = ConvertDoc(doc);
            wsSheet1.Cells[string.Format("A{0}", (2))].Value = strRender;
            */

            //exportHeader(wsSheet1, headerSeting_, colWidths_, columnSetting_, headerNested_);
            //exportData(wsSheet1, headerCount, data_, header_, classCells_, headerSeting_);

            //wsSheet1.Protection.IsProtected = false;
            //wsSheet1.Protection.AllowSelectLockedCells = false;

            //string handle = Guid.NewGuid().ToString();

            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    ExcelPkg.SaveAs(memoryStream);
            //    memoryStream.Position = 0;
            //    TempData[handle] = memoryStream.ToArray();
            //}

            //string date_now = DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fffffff");
            //fileName += "-" + date_now;
            //string FileName = ReplaceInvalidChars(fileName);
            //FileName = FileName.Replace("&", "");
            //FileName = FileName.Replace("!", "");
            //FileName = FileName.Replace("@", "");
            //FileName = FileName.Replace("$", "");
            //FileName = FileName.Replace("%", "");
            //var path = Path.Combine(ResourceLocation.Default.FileTemp, FileName + ".xlsx");

            //System.IO.File.WriteAllBytes(path, ExcelPkg.GetAsByteArray());

            //return "/Temp/" + FileName + ".xlsx";

            //return new JsonResult()
            //{
            //    Data = new { FileGuid = handle, FileName = fileName + ".xlsx" }
            //};

        }

        public string ReplaceInvalidChars(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }

        private void exportHeader(ExcelWorksheet wsSheet1, JArray headerSeting, JArray colWidths,
            JArray columnSetting, JArray headerNested)
        {
            for (int k = 0; k < headerSeting.Count; k++)
            {
                var item = (JArray)headerSeting[k];
                for (var i = 0; i < item.Count; i++)
                {
                    if (item[i].ToString() != "")
                    {
                        var cell = wsSheet1.Cells[k + 1, i + 1];
                        cell.Value = item[i].ToString();
                        cell.Style.Font.Size = 13;
                        cell.Style.Font.Bold = true;
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        if (colWidths.Count == item.Count)                        
                            wsSheet1.Column(i + 1).Width = Int32.Parse(colWidths[i].ToString()) / 8;     
                        wsSheet1.Column(i + 1).Hidden = (bool)columnSetting[i];
                            

                        if (i == headerSeting.Count - 1)
                        {
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;
                        }else
                        {
                            var borders = cell.Style.Border;
                            borders.Bottom.Style =
                                borders.Top.Style =
                                borders.Left.Style =
                                borders.Right.Style = ExcelBorderStyle.Thin;
                        }     
                    }
                    wsSheet1.Cells[k + 1, i + 1].Style.WrapText = true;
                                  
                }
                if (item.Count > 1)
                {
                    foreach (var item2 in headerNested)
                    {
                        if (Int32.Parse(item2["row"].ToString()) >= item.Count - 1 && Int32.Parse(item2["colspan"].ToString()) > 1)
                        {
                            for (int i = 0; i < Int32.Parse(item2["colspan"].ToString()); i++)
                            {
                                var cell = wsSheet1.Cells[headerSeting.Count - 1, Int32.Parse(item2["col"].ToString()) + i];
                                cell.Value = headerSeting[headerSeting.Count - 1][item2["col"]];
                                var border = cell.Style.Border;
                                border.Bottom.Style =
                                    border.Top.Style =
                                    border.Left.Style =
                                    border.Right.Style = ExcelBorderStyle.Thin;
                                cell.Style.WrapText = true;
                            }
                        }
                        else
                        {
                            var cell = wsSheet1.Cells[Int32.Parse(item2["row"].ToString()) + 1, Int32.Parse(item2["col"].ToString()) + 1, 
                                Int32.Parse(item2["row"].ToString()) + Int32.Parse(item2["rowspan"].ToString()), 
                                Int32.Parse(item2["col"].ToString()) + Int32.Parse(item2["colspan"].ToString())];
                            cell.Merge = true;
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;
                            cell.Style.WrapText = true;
                        }
                    }
                }
            }
        }

        private void exportData(ExcelWorksheet wsSheet1, int headerCount, JArray data, JArray header,
            JArray classCells, JArray headerSeting)
        {
            foreach(var item in data)
            {
                headerCount = headerCount + 1;
                for (int i = 0; i < header.Count; i++)
                {
                    var cell = wsSheet1.Cells[headerCount, i + 1];
                    cell.Value = item[header[i][0].ToString()].ToString();
                    var border = cell.Style.Border;
                    border.Bottom.Style =
                        border.Top.Style =
                        border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;
                    if (classCells != null)
                    {
                        string classCell = classCells[headerCount - headerSeting.Count() - 1][i].ToString();
                        if (classCell.IndexOf("htCenter") > -1)
                        {
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        }
                        if (classCell.IndexOf("htMiddle") > -1)
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        if (classCell.IndexOf("htBold") > -1)
                            cell.Style.Font.Bold = true;
                        if (classCell.IndexOf("htLeft") > -1)
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    }
                    cell.Style.WrapText = true;
                }
            }
        }

    }
}