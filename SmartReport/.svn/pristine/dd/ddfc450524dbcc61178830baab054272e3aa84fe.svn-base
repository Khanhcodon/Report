using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchController : CustomerBaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly EgovSearch _searchService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly ISearchInDatabase _searchInDatabaseService;
        private readonly ISearchInSolr _searchInSolrService;

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="generalSettings"></param>
        /// <param name="searchService"></param>
        /// <param name="searchInDatabaseService"></param>
        /// <param name="searchInSolrService"></param>
        public SearchController(AdminGeneralSettings generalSettings, EgovSearch searchService,
            ISearchInDatabase searchInDatabaseService, ISearchInSolr searchInSolrService,
                                Helper.UserSetting helperUserSetting)
        {
            _generalSettings = generalSettings;
            _searchService = searchService;
            _searchInDatabaseService = searchInDatabaseService;
            _searchInSolrService = searchInSolrService;
            _helperUserSetting = helperUserSetting;
        }

        /// <summary>
        /// Tìm kiếm văn bản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Search(SearchAdvangeModel model)
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
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

            var result = SearchDocument(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <returns></returns>
        public JsonResult SearchAdvance(SearchAdvangeModel model)
        {
            if (model.SearchType == 0)
            {
                model.SearchType = SearchType.Document;
            }

            if (model.Page == 0)
            {
                model.Page = 1;
            }

            if (model.PageSize == 0)
            {
                model.PageSize = _generalSettings.DefaultPageSize;
            }

            if (model.FromDateStr == null)
            {
                var from = DateTime.Now.AddMonths(-12);
                model.FromDateStr = from.ToString("dd/MM/yyyy").Replace(".", "/");
            }

            var result = SearchAdvanceBus(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private SearchDocumentResultModel SearchAdvanceBus(SearchAdvangeModel model)
        {
            var userId = User.GetUserId();

            model.Compendium = model.Compendium == null ? "" : model.Compendium.Trim();

            model.DocCode = model.DocCode == null ? "" : model.DocCode.Trim();
            model.InOutCode = model.InOutCode == null ? "" : model.InOutCode.Trim();

            model.OrganizationCreate = model.OrganizationCreate == null ? "" : model.OrganizationCreate.Trim();
            model.Content = model.Content == null ? "" : model.Content.Trim();

            ViewBag.SearchModel = model;
            // ViewBag.ListPageSize = _generalSettings.ListPageSize;

            var result = new SearchDocumentResultModel();
            if (model.SearchType == SearchType.File)
            {
                var fileResult = _searchService.Search(userId, model.Compendium, model.Content, model.CategoryId,
                    model.KeyWord,
                    model.DocCode, model.InOutCode, model.UrgentId, model.CategoryBusinessId, model.StorePrivateId,
                    model.CurrentUserId, model.FromDate, model.ToDate,
                    model.InOutPlaceId, model.OrganizationCreate, model.DocFieldId, model.UserSuccessId, model.Page,
                    model.PageSize).ToModel() ??
                             new SearchDocumentResultModel
                             {
                                 Items = new List<SearchDocumentItemResultModel>(),
                                 FacetCreatedDate = new List<KeyValuePair<string, int>>(),
                                 FacetDocField = new List<KeyValuePair<string, KeyValuePair<string, int>>>(),
                                 FacetDocType = new List<KeyValuePair<string, KeyValuePair<string, int>>>()
                             };
                model.TotalPage = fileResult.TotalResult;
                return fileResult;
            }

            var docs = _searchInDatabaseService.SearchAdvance(new Search.Entity.SearchQuery()
            {
                Compendium = model.Compendium,
                UserId = userId,
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
                IsUseCached = model.IsUseCached
            }, model.Page, model.PageSize, model.IsMainProcess);

            result = docs.ToModel();
            result.SearchType = (int)model.SearchType;
            return result;
        }

        private SearchDocumentResultModel SearchDocument(SearchAdvangeModel model)
        {
            var userId = User.GetUserId();

            var result = new SearchDocumentResultModel();

            if (string.IsNullOrWhiteSpace(model.Compendium))
            {
                result = new SearchView().ToModel();
                result.SearchType = (int)SearchType.Document;
                return result;
            }

            var docs = _searchInDatabaseService.QuickSearch(userId, model.Compendium, model.IsUseCached, model.Page, model.PageSize, model.IsMainProcess);

            result = docs.ToModel();
            result.SearchType = (int)SearchType.Document;

            return result;
        }

        private SearchDocumentResultModel SearchFile(SearchAdvangeModel model)
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
    }
}