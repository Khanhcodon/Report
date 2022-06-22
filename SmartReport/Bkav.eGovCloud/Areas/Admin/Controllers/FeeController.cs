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
    public class FeeController : CustomController
    {
#if HoSoMotCuaEdition
        private const string DEFAULT_SORT_BY = "FeeName";
        private readonly FeeBll _feeService;
        private readonly DocTypeBll _doctypeService;
        private readonly ResourceBll _resourceService;
        private readonly DocFieldBll _docfieldService;

        public FeeController(
            FeeBll feeService,
            DocTypeBll doctypeService,
            ResourceBll resourceService,
            DocFieldBll docfieldService)
            : base()
        {
            _resourceService = resourceService;
            _feeService = feeService;
            _doctypeService = doctypeService;
            _docfieldService = docfieldService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = new FeeSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchFee];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<FeeSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            if (!search.DocTypeId.HasValue)
            {
                search.DocTypeId = Guid.Empty;
            }
            var model = GetFeeModels(search, sortAndPage);
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.AllDocFields = GetAllDocFields();
            ViewBag.AllDocTypes = GetAllDocTypes();
            return View(model);
        }

        public ActionResult Search(FeeSearchModel search)
        {
            IEnumerable<FeeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = false,
                    SortBy = DEFAULT_SORT_BY
                };
                if (ModelState.IsValid)
                {
                    model = GetFeeModels(search, sortAndPage);
                    CreateCookieSearch(search, sortAndPage);
                }
                ViewBag.SortAndPage = sortAndPage;
            }
            ViewBag.Search = search;
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            FeeSearchModel search,
            string sortBy,
            bool isSortDesc)
        {
            IEnumerable<FeeModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                };
                model = GetFeeModels(search, sortAndPage);
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var search = new FeeSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchFee];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<FeeSearchModel>(data["Search"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    return View(new FeeModel());
                }
            }

            ////BindFeeCreate(null, search.CategoryBusinessId);
            BindFeeCreate(null);
            var model = new FeeModel { DocFieldId = search.DocFieldId };
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
        //[ValidateAntiForgeryToken(Salt = "FeeCreate")]
        public ActionResult Create(FeeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
#if XuLyVanBanEdition
                if (model.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotExist"));
                    //BindFeeCreate(model.FeeTypeId, model.CategoryBusinessId);
                    BindFeeCreate(model.FeeTypeId);
                    return View(model);
                }
#elif HoSoMotCuaEdition
                if (model.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen ||
                    model.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDi)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.CategoryBusiness.NotExist"));
                    //BindFeeCreate(model.FeeTypeId, model.CategoryBusinessId);
                    BindFeeCreate(model.FeeTypeId);
                    return View(model);

                }
#endif
                var entity = model.ToEntity();
                entity.CreatedByUserId = User.GetUserId();
                entity.CreatedOnDate = DateTime.Now;
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.FeeName.Split(';').Distinct();
                        var list = new List<Fee>();
                        foreach (var name in names)
                        {
                            var item = entity.Clone();
                            item.FeeName = name;
                            list.Add(item);
                        }
                        _feeService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _feeService.Create(entity);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Created.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Created.Success"));
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
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Created.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Created.Error"));
                }
            }
            GetModelError();
            BindFeeCreate(model.FeeTypeId);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var fee = _feeService.Get(id);
            if (fee == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotExist"));
                return RedirectToAction("Index");
            }

            var model = fee.ToModel();
            ViewBag.FeeTypeId = _resourceService.EnumToSelectList<FeeType>(model.FeeTypeId);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "FeeEdit")]
        public ActionResult Edit(FeeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var fee = _feeService.Get(model.FeeId);
            if (fee == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotExist"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var oldName = fee.FeeName;
                model.DocTypeId = fee.DocTypeId;
                fee = model.ToEntity(fee);
                fee.LastModifiedByUserId = User.GetUserId();
                fee.LastModifiedOnDate = DateTime.Now;
                try
                {
                    _feeService.Update(fee, oldName);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Updated.Error"));
                }

            }
            GetModelError();
            ViewBag.FeeTypeId = _resourceService.EnumToSelectList<FeeType>(model.FeeTypeId);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "FeeDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var fee = _feeService.Get(id);
            if (fee == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Fee.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Fee.NotExist"));
            }
            else
            {
                try
                {
                    _feeService.Delete(fee);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Deleted.Success"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Deleted.Success"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Deleted.Error"));
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Fee.Deleted.Error"));
                }
            }

            return RedirectToAction("Index");
        }

        #region Private Method

        private void CreateCookieSearch(FeeSearchModel search, SortAndPagingModel sortAndPage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortAndPage } };
            HttpCookie cookie = Request.Cookies[CookieName.SearchFee];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchFee, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
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

        private IEnumerable<FeeModel> GetFeeModels(FeeSearchModel search, SortAndPagingModel sortAndPagingModel)
        {
            return
                _feeService.GetsAs(f => new
                {
                    f.FeeId,
                    f.FeeName,
                    f.FeeTypeId,
                    IsRequired = f.IsRequired,
                    f.Price
                }, sortAndPagingModel.SortBy, sortAndPagingModel.IsSortDescending, search.DocTypeId).Select(
                    f =>
                        new FeeModel
                        {
                            FeeId = f.FeeId,
                            FeeName = f.FeeName,
                            FeeTypeId = f.FeeTypeId,
                            FeeTypeName = _resourceService.GetEnumDescription<FeeType>((FeeType)f.FeeTypeId),
                            IsRequired = f.IsRequired,
                            Price = f.Price
                        });
        }

        private void BindFeeCreate(int? typeSelected = null, int? categoryBusinessSelected = null)
        {
            ViewBag.FeeTypeId = _resourceService.EnumToSelectList<FeeType>(typeSelected);
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
            var listCategoryBusiness = _resourceService.EnumToSelectList<CategoryBusinessTypes>(categoryBusinessId);
#if XuLyVanBanEdition
            listCategoryBusiness = listCategoryBusiness.Where(l => l.Value != ((int)CategoryBusinessTypes.Hsmc).ToString()).ToList();
#elif HoSoMotCuaEdition
            listCategoryBusiness = listCategoryBusiness.Where(l => l.Value == ((int)CategoryBusinessTypes.Hsmc).ToString()).ToList();
#endif
            return listCategoryBusiness;
        }

        #endregion

#endif
    }
}
