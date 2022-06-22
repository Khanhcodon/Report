using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class PaperController : CustomController// BaseController
    {
#if HoSoMotCuaEdition

        private const string DEFAULT_SORT_BY = "PaperName";
        private readonly PaperBll _paperService;
        private readonly DocTypeBll _doctypeService;
        private readonly ResourceBll _resourceService;
        private readonly DocFieldBll _docfieldService;

        public PaperController(
            PaperBll paperService,
            DocTypeBll doctypeService,
            ResourceBll resourceService,
            DocFieldBll docfieldService)
            : base()
        {
            _resourceService = resourceService;
            _paperService = paperService;
            _doctypeService = doctypeService;
            _docfieldService = docfieldService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = new PaperSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchPaper];
            var isInvalidCookie = false;

            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<PaperSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            //if (!search.DocTypeId.HasValue)
            //{
            //    search.DocTypeId = Guid.Empty;
            //}

            var model = GetPaperModels(search, sortAndPage);
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            //ViewBag.CategoryBusinessId = BindCategoryBusiness(search.CategoryBusinessId);
            ViewBag.AllDocFields = GetAllDocFields();
            ViewBag.AllDocTypes = GetAllDocTypes();

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return View(model);
        }

        public ActionResult Search(PaperSearchModel search)
        {
            IEnumerable<PaperModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = false,
                    SortBy = DEFAULT_SORT_BY
                };
                if (ModelState.IsValid)
                {
                    if (search.DocTypeId == Guid.Empty)
                    {
                        search.DocTypeId = null;
                    }
                    model = GetPaperModels(search, sortAndPage);
                    CreateCookieSearch(search, sortAndPage);
                }
                ViewBag.SortAndPage = sortAndPage;
            }
            ViewBag.Search = search;
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(PaperSearchModel search,
                                                string sortBy,
                                                bool isSortDesc)
        {
            IEnumerable<PaperModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                };
                model = GetPaperModels(search, sortAndPage);
                CreateCookieSearch(search, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var search = new PaperSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchPaper];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<PaperSearchModel>(data["Search"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    return View(new PaperModel());
                }
            }
            BindPaperCreate(null, search.CategoryBusinessId);
            var model = new PaperModel();
            if (search.DocFieldId.HasValue)
            {
                model.DocFieldId = search.DocFieldId.Value;
            }

            if (search.DocTypeId.HasValue)
            {
                model.DocTypeId = search.DocTypeId.Value;
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PaperCreate")]
        public ActionResult Create(PaperModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                entity.CreatedByUserId = User.GetUserId();
                entity.CreatedOnDate = DateTime.Now;
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.PaperName.Split(';').Distinct();
                        var list = new List<Paper>();
                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            item.PaperName = name;
                            list.Add(item);
                        }
                        _paperService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _paperService.Create(entity);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Created.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Created.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Created.Error"));
                }

            }
            BindPaperCreate(model.PaperTypeId, model.CategoryBusinessId);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var paper = _paperService.Get(id);
            if (paper == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotExist"));
                return RedirectToAction("Index");
            }

            var model = paper.ToModel();
            ViewBag.PaperTypeId = _resourceService.EnumToSelectList<PaperType>(model.PaperTypeId);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PaperEdit")]
        public ActionResult Edit(PaperModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var paper = _paperService.Get(model.PaperId);
                if (paper == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Customer.Paper.NotExist"));
                    return RedirectToAction("Index");
                }

                var oldName = paper.PaperName;
                model.DocTypeId = paper.DocTypeId;
                paper = model.ToEntity(paper);
                paper.LastModifiedByUserId = User.GetUserId();
                paper.LastModifiedOnDate = DateTime.Now;
                try
                {
                    _paperService.Update(paper, oldName);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Updated.Error"));
                }
            }

            ViewBag.PaperTypeId = _resourceService.EnumToSelectList<PaperType>(model.PaperTypeId);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "PaperDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var paper = _paperService.Get(id);
            if (paper == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Paper.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Paper.NotExist"));
            }
            else
            {
                try
                {
                    _paperService.Delete(paper);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Deleted.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Deleted.Success"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Deleted.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Paper.Deleted.Error"));
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult SyncDoctype()
        {
            try
            {
                _paperService.SyncDoctype();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Content("Đồng bộ lỗi");
            }

            return Content("Đồng bộ thành công");
        }

        #region Private Method

        private void CreateCookieSearch(PaperSearchModel search, SortAndPagingModel sortAndPage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortAndPage } };
            HttpCookie cookie = Request.Cookies[CookieName.SearchPaper];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchPaper, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private string GetAllDocFields()
        {
            var hsmc = (int)CategoryBusinessTypes.Hsmc;
            return
                _docfieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId },
                p => (p.CategoryBusinessId & hsmc) == hsmc)
                    .OrderBy(d => d.DocFieldName)
                    .StringifyJs();
        }

        private string GetAllDocTypes()
        {
            var hsmc = (int)CategoryBusinessTypes.Hsmc;
            return
                _doctypeService.GetsAs(d => new { d.DocTypeId, d.DocTypeName, d.DocFieldId }
                , p => p.CategoryBusinessId == 0 || p.CategoryBusinessId == hsmc)
                    .OrderBy(d => d.DocTypeName)
                    .StringifyJs();
        }

        private IEnumerable<PaperModel> GetPaperModels(PaperSearchModel search, SortAndPagingModel sortAndPagingModel)
        {
            return
                _paperService.GetsAs(f => new
                {
                    f.PaperId,
                    f.PaperName,
                    f.PaperTypeId,
                    f.IsRequired,
                    f.Amount
                }, sortAndPagingModel.SortBy, sortAndPagingModel.IsSortDescending, search.DocTypeId).Select(
                    f =>
                        new PaperModel
                        {
                            PaperId = f.PaperId,
                            PaperName = f.PaperName,
                            PaperTypeId = f.PaperTypeId,
                            PaperTypeName = _resourceService.GetEnumDescription<PaperType>((PaperType)f.PaperTypeId),
                            IsRequired = f.IsRequired,
                            Amount = f.Amount
                        });
        }

        private void BindPaperCreate(int? typeSelected = null, int? categoryBusinessSelected = null)
        {
            ViewBag.PaperTypeId = _resourceService.EnumToSelectList<PaperType>(typeSelected);
            //ViewBag.CategoryBusinessId = BindCategoryBusiness((int)CategoryBusinessTypes.Hsmc);
            ViewBag.CategoryBusinessId = new List<SelectListItem> {
                                            new SelectListItem{
                                                    Value=((int)CategoryBusinessTypes.Hsmc).ToString(),
                                                    Text= _resourceService.GetEnumDescription((CategoryBusinessTypes)CategoryBusinessTypes.Hsmc)
                                               }
                                            };
            ViewBag.AllDocFields = GetAllDocFields();
            ViewBag.AllDocTypes = GetAllDocTypes();
        }

        private List<SelectListItem> BindCategoryBusiness(int? categoryBusinessId = null)
        {
            var result = new List<SelectListItem>();
            result = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
            result = result.Where(l => l.Value == ((int)CategoryBusinessTypes.Hsmc).ToString()).ToList();
            return result;
        }

        #endregion
#endif
    }
}
