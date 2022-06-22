using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Search.Entity;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.DSL;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using AutoMapper;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Search
{
    public class EgovSearch : ISearchInDatabase, ISearchInSolr
    {
        private ISolrOperations<EgovIndex> _cacheSolrOperations;
        private readonly LuceneBll _luceneService;
        private readonly AttachmentBll _attachmentService;
        private readonly DocumentContentBll _documentContentService;
        private readonly SearchSettings _searchSettings;
        private readonly DocFinishBll _docFinishService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocFieldBll _docFieldService;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _documentCopyService;
        private readonly UserBll _userService;
        private readonly MemoryCacheManager _cacheManager;
        private const string DocType = "doctype";
        private const string DocField = "docfield";
        private const string CreatedDate = "createddate";
        private const string Title = "title";
        private const string Content = "content";
        private const int FragSize = 300;
        private const string DocumentId = "documentid";
        private const string IsFile = "isfile";
        private readonly ReportModeBll _reportModeService;
        private readonly DepartmentBll _departmentSerivce;

        private static readonly IDictionary<string, ISolrFacetQuery> AllFacetFields = new Dictionary<string, ISolrFacetQuery>
                                                                       {
                                                                           {DocType, new SolrFacetFieldQuery(DocType)},
                                                                           {DocField, new SolrFacetFieldQuery(DocField)},
                                                                           {CreatedDate, new SolrFacetDateQuery(CreatedDate,
                                                                                                  new DateTime(2013, 1,1),
                                                                                                  new DateTime(DateTime.Today.Year, 1, 2),
                                                                                                  "+1YEAR")}
                                                                       };

        protected ISolrOperations<EgovIndex> CurrentSolrOperations
        {
            get { return GetCurrentSolrOperations(); }
            set { _cacheSolrOperations = value; }
        }

        public EgovSearch(SearchSettings searchSettings, LuceneBll luceneService, DocFinishBll docFinishService,
                            DocumentContentBll documentContentService, AttachmentBll attachmentService,
                            DocFieldBll docFieldService, DocTypeBll docTypeService, DocumentBll documentService,
                            DocumentCopyBll documentCopyService, UserBll userService, MemoryCacheManager cacheManager, 
                            ReportModeBll reportModeService, DepartmentBll departmentSerivce)
        {
            _searchSettings = searchSettings;
            _luceneService = luceneService;
            _attachmentService = attachmentService;
            _documentContentService = documentContentService;
            _docFinishService = docFinishService;
            _docFieldService = docFieldService;
            _docTypeService = docTypeService;
            _documentService = documentService;
            _documentCopyService = documentCopyService;
            _userService = userService;
            _cacheManager = cacheManager;
            _reportModeService = reportModeService;
            _departmentSerivce = departmentSerivce;
        }

        protected ISolrOperations<EgovIndex> GetCurrentSolrOperations()
        {
            if (_cacheSolrOperations == null)
            {
                var instances = Startup.Container.GetAllInstances(typeof(ISolrOperations<EgovIndex>));
                var i = instances.Count();
                if (i == 0)
                {
                    Startup.Init<EgovIndex>(_searchSettings.ServerUrl);
                }
                _cacheSolrOperations = ServiceLocator.Current.GetInstance<ISolrOperations<EgovIndex>>();
            }

            return _cacheSolrOperations;
        }

        // Hàm này tạm thời không dùng nên không được xóa
        public SearchView Search(string query, IDictionary<string, string> filter, int userId, int page = 1, int pageSize = 10)
        {
            //var docFinishAccess =
            //    _docFinishService.GetsAs(d => new { d.DocFinishId, d.DocumentId, d.DocumentCopyId, d.UserId },
            //        d => d.UserId == userId).ToList();

            //if (!docFinishAccess.Any())
            //{
            //    return null;
            //}
            var documentCopies = _documentCopyService.GetsByUser(userId);
            var listDocumentIdAccess = documentCopies.Select(d => d.DocumentId).Distinct(); // docFinishAccess.Select(d => d.DocumentId).Distinct().ToList();
            if (!listDocumentIdAccess.Any())
            {
                return null;
            }

            var options = new QueryOptions
            {
                Highlight = new HighlightingParameters
                {
                    Fields = new[] { Title, Content },
                    Snippets = 1,
                    Fragsize = FragSize,
                    MergeContiguous = false,
                    UseFastVectorHighlighter = true,
                    UsePhraseHighlighter = true,
                    HighlightMultiTerm = true
                },

                //Facet = new FacetParameters
                //{
                //    Queries = AllFacetFields.Select(f => f.Value).ToArray(),//.Where(f => !filter.Keys.Contains(f.Key)).Select(f => f.Value).ToArray(),
                //    MinCount = 1
                //},
                Start = (page - 1) * pageSize,
                Rows = pageSize,
                SpellCheck = new SpellCheckingParameters(),
                FilterQueries = filter.Select(f =>
                {
                    if (f.Key.ToLower() != CreatedDate)
                    {
                        return (ISolrQuery)Query.Field(f.Key.ToLower()).Is(f.Value);
                    }
                    int year;
                    if (int.TryParse(f.Value, out year))
                    {
                        return new SolrQueryByRange<DateTime>(f.Key.ToLower(),
                                                              new DateTime(year),
                                                              new DateTime(year, 12,
                                                                           31, 23, 59,
                                                                           59, 999));
                    }
                    return null;
                }).ToList(),
            };
            var queryescape = query.StripSpecialCharactersForSolr();
            var results = CurrentSolrOperations.Query(
                    (new SolrQueryByField("text", query) { Quoted = true } || new SolrQuery(queryescape + "*")) &&
                    new SolrQueryInList("documentid", listDocumentIdAccess.Select(d => d.ToString())), options);

            var resultFacets =
                CurrentSolrOperations.Query(
                    (new SolrQueryByField("text", query) { Quoted = true } || new SolrQuery(queryescape + "*")) &&
                    new SolrQueryInList("documentid", listDocumentIdAccess.Select(d => d.ToString())),
                    new QueryOptions
                    {
                        Rows = 0,
                        Facet = new FacetParameters
                        {
                            Queries = AllFacetFields.Select(f => f.Value).ToArray(),
                            MinCount = 1
                        }
                    });

            var searchItemsView = new List<SearchItemView>();
            var listDocumentAccess =
                _documentService.GetsAs(
                    d => new { d.DocumentId, d.Compendium, d.DateAppointed, d.DateResponsed, d.DateResponsedOverdue, d.UrgentId },
                    d => listDocumentIdAccess.Contains(d.DocumentId));

            var listDocumentCopyIdAccess = documentCopies.Select(d => d.DocumentCopyId).Distinct();
            var listDocumentCopyAccess = _documentCopyService.GetsAs(d => new { d.DocumentCopyId, d.DocumentCopyType, d.DateOverdue }, d => listDocumentCopyIdAccess.Contains(d.DocumentCopyId));
            foreach (var item in results)
            {
                var hl = results.Highlights[item.Id].ContainsKey(Content)
                             ? results.Highlights[item.Id][Content].Aggregate("",
                                                                                (current, h) =>
                                                                                current + string.Join(",", h))
                                                                                : item.Content.FirstOrDefault() == null ? string.Empty : item.Content.FirstOrDefault().Substring(0, FragSize);
                var title = results.Highlights[item.Id].ContainsKey(Title)
                                ? results.Highlights[item.Id][Title].Aggregate("",
                                                                                 (current, h) =>
                                                                                 current + string.Join(",", h))
                                : item.Title;

                var item1 = item;
                var view = new SearchItemView
                {
                    HighLight = hl,
                    Id = item.Id,
                    DocumentId = Guid.Parse(item.DocumentId),
                    IsFile = item.IsFile,
                    Title = title,
                    ContentId = item.ContentId
                };
                if (item.IsFile)
                {
                    var ext = Path.GetExtension(item.Title);
                    view.Extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", "");
                }

                var document = listDocumentAccess.Single(d => d.DocumentId.ToString() == item.DocumentId);
                view.DocumentCompendium = document.Compendium;

                var docCopies = documentCopies.Where(d => d.DocumentId.ToString() == item1.DocumentId).ToList();
                if (docCopies.Any())
                {
                    var lastFinish = docCopies.OrderBy(d => d.DocumentCopyId).Last();
                    view.DocumentCopyId = lastFinish.DocumentCopyId;
                    var documentcopy = listDocumentCopyAccess.Single(d => d.DocumentCopyId == view.DocumentCopyId);
                    view.Color = _documentService.GetColor(document.UrgentId, document.DateAppointed,
                                                           document.DateResponsed, document.DateResponsedOverdue,
                                                           documentcopy.DateOverdue, documentcopy.DocumentCopyType);
                }
                searchItemsView.Add(view);
            }
            var searchView = new SearchView
            {
                TotalResult = results.NumFound,
                Items = searchItemsView,
                DidYouMean = GetSpellCheckingResult(results)
            };
            if (resultFacets.NumFound > 0)
            {
                if (resultFacets.FacetFields.Keys.Any(k => k == DocField))
                {
                    var facetDocField = resultFacets.FacetFields[DocField];
                    var docFieldIdFacet = facetDocField.Select(f => int.Parse(f.Key));
                    var docFieldFacet = _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName },
                                                            d => docFieldIdFacet.Contains(d.DocFieldId));
                    var newFacetDocField = docFieldFacet.Select(field => new KeyValuePair<string, KeyValuePair<string, int>>(field.DocFieldName, facetDocField.Single(d => d.Key == field.DocFieldId.ToString(CultureInfo.InvariantCulture)))).ToList();
                    if (facetDocField.Any(f => f.Key == "0"))
                    {
                        newFacetDocField.Add(new KeyValuePair<string, KeyValuePair<string, int>>("Khác", facetDocField.Single(f => f.Key == "0")));
                    }
                    searchView.FacetDocField = newFacetDocField;
                }
                if (resultFacets.FacetFields.Keys.Any(k => k == DocType))
                {
                    var facetDocType = resultFacets.FacetFields[DocType];
                    var docTypeIdFacet = facetDocType.Select(f => Guid.Parse(f.Key));
                    var docTypeFacet = _docTypeService.GetsAs(d => new { d.DocTypeId, d.DocTypeName },
                                                            d => docTypeIdFacet.Contains(d.DocTypeId));
                    var newFacetDocType = docTypeFacet.Select(field => new KeyValuePair<string, KeyValuePair<string, int>>(field.DocTypeName, facetDocType.Single(d => d.Key == field.DocTypeId.ToString()))).ToList();
                    searchView.FacetDocType = newFacetDocType;
                }
                if (resultFacets.FacetDates.Keys.Any(k => k == CreatedDate))
                {
                    if (resultFacets.FacetDates[CreatedDate].DateResults.Any())
                    {
                        searchView.FacetCreatedDate =
                        resultFacets.FacetDates[CreatedDate].DateResults.Select(
                            k =>
                            new KeyValuePair<string, int>(
                                k.Key.Year.ToString(CultureInfo.InvariantCulture), k.Value));
                    }
                }
            }
            return searchView;
        }

        public IEnumerable<Guid> Search(IEnumerable<Guid> documentIds, string compendium, string content, int userId)
        {
            if (documentIds == null || !documentIds.Any())
            {
                return null;
            }
            var options = new QueryOptions
            {
                Fields = new[] { DocumentId }
            };
            var query = new SolrQueryByField("isfile", "false") &&
                        new SolrQueryInList(DocumentId, documentIds.Select(d => d.ToString()));
            if (!string.IsNullOrWhiteSpace(compendium))
            {
                var compendiumEscape = compendium.StripSpecialCharactersForSolr();
                query = query &&
                        (new SolrQueryByField(Title, compendium) { Quoted = true } ||
                         new SolrQuery(compendiumEscape + "*"));
            }
            if (!string.IsNullOrWhiteSpace(content))
            {
                var contentEscape = content.StripSpecialCharactersForSolr();
                query = query &&
                        (new SolrQueryByField(Content, content) { Quoted = true } ||
                         new SolrQuery(contentEscape + "*"));
            }
            var results = CurrentSolrOperations.Query(query, options);
            if (results == null)
            {
                return null;
            }

            return results.Select(d => Guid.Parse(d.DocumentId));
        }

        public SearchView Search(int userId, string compendium, string content, int? categoryId = null,
            string keyword = null, string docCode = null,
            string inOutCode = null, int? urgentId = null,
            int? categoryBusinessId = null, int? storePrivateId = null, int? userCurrentId = null,
            DateTime? fromDate = null, DateTime? toDate = null,
            int? inOutPlaceId = null, string organizationCreate = null,
            int? docfieldId = null, int? userSuccessId = null, int page = 1, int pageSize = 10)
        {
            var documentIds = _documentService.FindDocuments((dc, d) => d.DocumentId, userId, categoryId, keyword,
                docCode, inOutCode, urgentId, categoryBusinessId, storePrivateId, userCurrentId, fromDate, toDate,
                inOutPlaceId, organizationCreate, docfieldId, userSuccessId);

            if (documentIds == null || !documentIds.Any())
            {
                return null;
            }
            var options = new QueryOptions
            {
                Fields = new[] { "id", DocumentId, Title, Content, "contentid", "isfile" },
                Highlight = new HighlightingParameters
                {
                    Fields = new[] { Title, Content },
                    Snippets = 1,
                    Fragsize = FragSize,
                    MergeContiguous = false,
                    UseFastVectorHighlighter = true,
                    UsePhraseHighlighter = true,
                    HighlightMultiTerm = true
                },
                Start = (page - 1) * pageSize,
                Rows = pageSize
            };
            ISolrQuery query = null;
            if (!string.IsNullOrWhiteSpace(compendium))
            {
                var compendiumEscape = compendium.StripSpecialCharactersForSolr();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var contentEscape = content.StripSpecialCharactersForSolr();
                    query = new SolrQueryInList(DocumentId, documentIds.Select(d => d.ToString())) &&
                        ((new SolrQueryByField(Content, content) { Quoted = true } ||
                          new SolrQuery(contentEscape + "*"))) &&
                            ((new SolrQueryByField(Title, compendium) { Quoted = true } ||
                              new SolrQuery(compendiumEscape + "*")));
                }
                else
                {
                    query = new SolrQueryInList(DocumentId, documentIds.Select(d => d.ToString())) &&
                            ((new SolrQueryByField(Title, compendium) { Quoted = true } ||
                              new SolrQuery(compendiumEscape + "*")));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var contentEscape = content.StripSpecialCharactersForSolr();
                    query = new SolrQueryInList(DocumentId, documentIds.Select(d => d.ToString())) &&
                        ((new SolrQueryByField(Content, content) { Quoted = true } ||
                          new SolrQuery(contentEscape + "*")));
                }
                else
                {
                    query = new SolrQueryInList(DocumentId, documentIds.Select(d => d.ToString()));
                }
            }
            var results = CurrentSolrOperations.Query(query, options);
            if (results == null || !results.Any())
            {
                return null;
            }
            var documentIdsValid = results.Select(d => Guid.Parse(d.DocumentId)).ToList();
            var listDocumentAccess =
                _documentService.FindDocuments(
                    (dc, d) =>
                        new
                        {
                            d.DocumentId,
                            d.Compendium,
                            d.DateAppointed,
                            d.DateResponsed,
                            d.DateResponsedOverdue,
                            d.UrgentId,
                            dc.DocumentCopyId,
                            dc.DocumentCopyType,
                            dc.DateOverdue
                        }, userId, documentIdsValid);
            var searchItemsView = new List<SearchItemView>();
            foreach (var item in results)
            {
                var hl = string.Empty;
                if (results.Highlights[item.Id].ContainsKey(Content))
                {
                    hl = results.Highlights[item.Id][Content].Aggregate("", (current, h) => current + string.Join(",", h));
                }
                else
                {
                    var firstContent = item.Content.FirstOrDefault();
                    if (firstContent != null)
                    {
                        hl = firstContent.Length < FragSize
                                ? firstContent
                                : firstContent.Substring(0, FragSize);
                    }
                }
                var title = results.Highlights[item.Id].ContainsKey(Title)
                    ? results.Highlights[item.Id][Title].Aggregate("",
                        (current, h) =>
                            current + string.Join(",", h))
                    : item.Title;

                var item1 = item;
                var view = new SearchItemView
                {
                    HighLight = hl,
                    Id = item.Id,
                    DocumentId = Guid.Parse(item.DocumentId),
                    IsFile = item.IsFile,
                    Title = title,
                    ContentId = item.ContentId
                };
                if (item.IsFile)
                {
                    var ext = Path.GetExtension(item.Title);
                    view.Extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", "");
                }
                var document = listDocumentAccess.Where(d => d.DocumentId.ToString() == item1.DocumentId).ToList();
                if (document.Count() == 1)
                {
                    var primary = document.First();
                    view.DocumentCompendium = document.First().Compendium;
                    view.DocumentCopyId = primary.DocumentCopyId;
                    view.Color = _documentService.GetColor(primary.UrgentId, primary.DateAppointed,
                                                           primary.DateResponsed, primary.DateResponsedOverdue,
                                                           primary.DateOverdue, primary.DocumentCopyType);
                }
                else if (document.Count() > 1)
                {
                    var primary = document.SingleOrDefault(d => d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh) ??
                                  document.First();
                    view.DocumentCompendium = primary.Compendium;
                    view.DocumentCopyId = primary.DocumentCopyId;
                    view.Color = _documentService.GetColor(primary.UrgentId, primary.DateAppointed,
                                                           primary.DateResponsed, primary.DateResponsedOverdue,
                                                           primary.DateOverdue, primary.DocumentCopyType);
                }

                searchItemsView.Add(view);
            }
            var searchView = new SearchView
            {
                TotalResult = results.NumFound,
                Items = searchItemsView
            };
            return searchView;
        }

        public void AddIndex()
        {
            FileIndex fileIndex = null;
            DatabaseIndex databaseIndex = null;
            var notIndexed = _luceneService.Gets(l => !l.IsIndexed).ToList();
            if (notIndexed.Any())
            {
                var solr = CurrentSolrOperations;
                foreach (var lucene in notIndexed)
                {
                    IContentIndex contentIndex;
                    if (lucene.IsFile)
                    {
                        contentIndex = fileIndex ?? (fileIndex = new FileIndex(_attachmentService));
                    }
                    else
                    {
                        contentIndex = databaseIndex ?? (databaseIndex = new DatabaseIndex(_documentContentService));
                    }
                    var index = contentIndex.GetIndex(lucene);
                    if (index != null)
                    {
                        solr.Add(index);
                    }
                    else
                    {
                        solr.Delete(lucene.LuceneId.ToString(CultureInfo.InvariantCulture));
                    }
                    solr.Commit();
                    solr.BuildSpellCheckDictionary();
                    lucene.IsIndexed = true;
                    _luceneService.Update(lucene);
                }
            }
        }
                
        public SearchView QuickSearch(int userId, string keyWord, bool isUseCached = false, int page = 1, int pageSize = 100, bool isMainProcess = true)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var cacheKey = string.Format(CacheParam.QuickSearchViewKey, currentUserId);
            
            // Không strip Vietnamese chổ này do cần tìm theo số ký hiệu và số đến đi.
            keyWord = keyWord.StripHtml();
            
            // Gọi store truy vấn dữ liệu
            var queryResult = _documentService.QuickSearchDocument<dynamic>(keyWord, userId, isMainProcess, page, pageSize).ToList();

            var totalResultItem = queryResult.Last();
            queryResult.Remove(totalResultItem);
            
            return new SearchView
            {
                TotalResult = totalResultItem.Total,
                Items = ParseResult(queryResult)
            };
        }

        public SearchView SearchAdvance(SearchQuery searchQuery, int page = 1, int pageSize = 100, bool isMainProcess = true)
        {
            searchQuery.Compendium = searchQuery.Compendium.StripHtml();

            CurrentUserCached currentUser = _userService.CurrentUser;

            var queryResult = _documentService.SearchDocument<dynamic>(searchQuery.Compendium, searchQuery.UserId,
                        searchQuery.CategoryId, "", searchQuery.DocCode, searchQuery.InOutCode, searchQuery.UrgentId,
                        searchQuery.CategoryBusinessId, searchQuery.StoreId, searchQuery.StorePrivateId, searchQuery.CurrentUserId,
                        searchQuery.FromDate, searchQuery.ToDate, searchQuery.FromPubDate, searchQuery.ToPubDate,
                        searchQuery.InOutPlaceId, searchQuery.Organization, null,
                        searchQuery.SuccessedUserId, searchQuery.CreatedUserId,searchQuery.DocTypeCode, searchQuery.ReportModeId, 
                        searchQuery.DocTypeId, searchQuery.InOutPlace, searchQuery.UserCreatedName,searchQuery.Status,searchQuery.ActionLevel,
                        searchQuery.TimeKey,isMainProcess, page, pageSize, searchQuery.ReportRuleIdOnly, 
                        searchQuery.UnitDelivery,searchQuery.UnitReceive).ToList();
            

            var totalResultItem = queryResult.Last();
            queryResult.Remove(totalResultItem);

            var searchItemView = ParseResult(queryResult);

            return new SearchView
            {
                TotalResult = totalResultItem.Total,
                Items = searchItemView
            };
        }

        public SearchView SearchAdvanceInDatabase(int userId, string compendium, string content, int? categoryId = null,
            string keyword = null, string docCode = null,
            string inOutCode = null, int? urgentId = null,
            int? categoryBusinessId = null, int? storePrivateId = null, int? userCurrentId = null,
            DateTime? fromDate = null, DateTime? toDate = null,
            int? inOutPlaceId = null, string organizationCreate = null,
            int? docfieldId = null, int? userSuccessId = null, int? userCreatedId = null, int page = 1, int pageSize = 100, bool? isMainProcess = true)
        {
            compendium = compendium.StripHtml().StripVietnameseChars();

            var documents = _documentService.SearchDocument<dynamic>(compendium, userId,
                        categoryId, "", docCode, inOutCode, urgentId,
                        categoryBusinessId, 0, storePrivateId, userCurrentId,
                        fromDate, toDate, null, null,
                        inOutPlaceId, organizationCreate, null,
                        userSuccessId, userCreatedId,"", null, null,null, 
                        null, null, null,null ,false, page, pageSize);

            return ParseSearchResult(documents, pageSize, page);

            //var searchItemsView = new List<SearchItemView>();
            //compendium = compendium == null ? string.Empty : compendium.ToLower();

            //var docs = _documentService.FindDocuments((dc, d) => new
            //{
            //    DocumentId = d.DocumentId,
            //    DocumentCopyId = dc.DocumentCopyId,
            //    DocCode = d.DocCode,
            //    Compendium = d.Compendium,
            //    InOutCode = d.InOutCode,
            //    Status = dc.Status,
            //    CurrentUsername = dc.UserCurrent.FullName,
            //    DateCreate = d.DateCreated,
            //    UserCreatedId = d.UserCreatedId,
            //    Address = d.Address,
            //    CategoryName = d.Category.CategoryName,
            //    CitizenName = d.CitizenName,
            //    DateAppointed = d.DateAppointed,
            //    DateArrived = d.DateAppointed,
            //    DateCreated = d.DateCreated,
            //    LastUserComment = dc.LastUserComment,
            //    UserSuccess = d.UserSuccess.FullName,
            //    DateReceived = dc.DateReceived
            //}, userId, categoryId, keyword,
            //    docCode, inOutCode, urgentId, categoryBusinessId, storePrivateId, userCurrentId, fromDate, toDate,
            //    inOutPlace, organizationCreate, docfieldId, userSuccessId, userCreatedId, isMainProcess: isMainProcess);

            //if (!string.IsNullOrEmpty(compendium))
            //{
            //    docs = docs.Where(d =>
            //        StringExtension.StripVietnameseChars(d.Compendium).ToLower().Contains(StringExtension.StripVietnameseChars(compendium))
            //        || (StringExtension.StripVietnameseChars(d.CitizenName).ToLower().Contains(StringExtension.StripVietnameseChars(compendium)))
            //        || (d.DocCode != null && d.DocCode.ToLower().Contains(compendium))
            //        || (d.InOutCode != null && d.InOutCode.ToLower().Contains(compendium)));
            //}

            //docs = docs.OrderByDescending(d => d.DateCreate).ToList();

            //var count = docs.Count();
            //if (docs.Any())
            //{
            //    var docsInPage = docs.Skip(count > pageSize * (page - 1) ? pageSize * (page - 1) : 0).Take(count > pageSize * page ? pageSize : count % pageSize);
            //    foreach (var d in docsInPage)
            //    {
            //        var userCreated = _userService.GetFromCache(d.UserCreatedId);
            //        searchItemsView.Add(new SearchItemView
            //        {
            //            Title = d.Compendium,
            //            DocumentCompendium = string.IsNullOrEmpty(d.CitizenName) ? d.Compendium : d.CitizenName,
            //            DocumentId = d.DocumentId,
            //            DocumentCopyId = d.DocumentCopyId,
            //            ExtendInfo = new
            //            {
            //                CurrentUsername = d.CurrentUsername,
            //                Status = d.Status,
            //                DocCode = d.DocCode,
            //                DateCreate = d.DateCreate,
            //                CreatedUserName = userCreated != null ? userCreated.Username : null
            //            }
            //        });
            //    }
            //}
            //return new SearchView
            //{
            //    TotalResult = count,
            //    Items = searchItemsView
            //};
        }

        /// <summary>
        /// Author: TienNVg
        /// CreateDate: 05/12/2017
        /// Description: Hàm chuyển đổi dữ liệu lấy từ PROCSTORE chuyển thành dạng list thay the ham ParseSearchResult
        /// </summary>
        /// <param name="docs"></param>
        /// <returns></returns>
        private IEnumerable<SearchItemView> ParseResult(IEnumerable<dynamic> docs)
        {
            var result = new List<SearchItemView>();
            if (docs == null || !docs.Any())
            {
                return result;
            }
            
            foreach (var doc in docs)
            { 
                var docTypeId = _docTypeService.Get(doc.DocTypeId);
                var reportModes = _reportModeService.Get(docTypeId.ReportModeId);
                if(reportModes != null)
                {
                    var reportModeName = reportModes.Name; // chế độ báo cáo
                    var newDepartmentDelivery = new Department();
                    var newDepartmentReceive = new Department();
                    var UnitDeliveryStr_ = "";
                    var UnitReceiveStr_ = "";
                    if (doc.UnitDelivery != null)
                    {
                        newDepartmentDelivery = _departmentSerivce.Get(doc.UnitDelivery);
                        UnitDeliveryStr_ = newDepartmentDelivery.DepartmentName;
                    }
                    else
                    {
                        UnitDeliveryStr_ = "";
                    }

                    if(doc.UnitReceive != null)
                    {
                        newDepartmentReceive = _departmentSerivce.Get(doc.UnitReceive);
                        UnitReceiveStr_ = newDepartmentReceive.DepartmentName;
                    }
                    else
                    {
                        UnitReceiveStr_ = "";
                    }
                    
                    result.Add(new SearchItemView
                    {
                        Title = doc.Compendium,
                        DocumentCompendium = doc.Compendium,
                        DocumentId = doc.DocumentId,
                        DocumentCopyId = doc.DocumentCopyId,
                        DatePublished = doc.DatePublished,
                        DateArrived = doc.DateArrived,
                        CategoryName = doc.CategoryName,
                        DocCode = doc.DocCode,
                        InOutCode = doc.InOutCode,
                        UserSuccessName = doc.UserSuccessName,
                        ExtendInfo = new
                        {
                            Organization = doc.Organization,
                            CurrentUsername = doc.UserCurrentName,
                            Status = doc.Status,
                            DocCode = doc.DocCode,
                            InOutCode = doc.InOutCode,
                            CreatedUserName = doc.UserCreatedName,
                            CurrentDepartmentName = doc.CurrentDepartmentName
                        },
                        IsViewed = true,
                        ReportModeName = reportModeName,
                        UnitDelivery = doc.UnitDelivery,
                        UnitReceive = doc.UnitReceive,
                        UnitDeliveryStr = UnitDeliveryStr_,
                        UnitReceiveStr = UnitReceiveStr_
                    });
                }else
                {


                    var newDepartmentDelivery = new Department();
                    var newDepartmentReceive = new Department();
                    var UnitDeliveryStr_ = "";
                    var UnitReceiveStr_ = "";
                    if (doc.UnitDelivery != null)
                    {
                        newDepartmentDelivery = _departmentSerivce.Get(doc.UnitDelivery);
                        UnitDeliveryStr_ = newDepartmentDelivery.DepartmentName;
                    }
                    else
                    {
                        UnitDeliveryStr_ = "";
                    }

                    if (doc.UnitReceive != null)
                    {
                        newDepartmentReceive = _departmentSerivce.Get(doc.UnitReceive);
                        UnitReceiveStr_ = newDepartmentReceive.DepartmentName;
                    }
                    else
                    {
                        UnitReceiveStr_ = "";
                    }

                    result.Add(new SearchItemView
                    {
                        Title = doc.Compendium,
                        DocumentCompendium = doc.Compendium,
                        DocumentId = doc.DocumentId,
                        DocumentCopyId = doc.DocumentCopyId,
                        DatePublished = doc.DatePublished,
                        DateArrived = doc.DateArrived,
                        CategoryName = doc.CategoryName,
                        DocCode = doc.DocCode,
                        InOutCode = doc.InOutCode,
                        UserSuccessName = doc.UserSuccessName,
                        ExtendInfo = new
                        {
                            Organization = doc.Organization,
                            CurrentUsername = doc.UserCurrentName,
                            Status = doc.Status,
                            DocCode = doc.DocCode,
                            InOutCode = doc.InOutCode,
                            CreatedUserName = doc.UserCreatedName,
                            CurrentDepartmentName = doc.CurrentDepartmentName
                        },
                        IsViewed = true,
                        ReportModeName = "",
                        UnitDelivery = doc.UnitDelivery,
                        UnitReceive = doc.UnitReceive,
                        UnitDeliveryStr = UnitDeliveryStr_,
                        UnitReceiveStr = UnitReceiveStr_
                    });
                }                
            }

            return result;
        }

        private SearchView ParseSearchResult(IEnumerable<dynamic> docs, int pageSize, int page)
        {
            var searchItemsView = new List<SearchItemView>();
            var count = 0;
            if (docs.Any())
            {
                count = docs.Count();
                var skip = (page - 1) * pageSize; // count > pageSize * (page - 1) ? pageSize * (page - 1) : 0;
                var take = pageSize; // count > pageSize * page ? pageSize : count % pageSize;
                var docsInPage = pageSize == 0 ? docs : docs.Skip(skip).Take(take);

                //var currentUserIds = docsInPage.Select(d => (int)d.UserCurrentId).ToList();
                //var createdUserIds = docsInPage.Select(d => (int)d.UserCreatedId).ToList();
                //var currentUsers = _userService.GetCacheAllUsers().Where(u => currentUserIds.Any(cu => cu == u.UserId));
                //var createdUsers = _userService.GetCacheAllUsers().Where(u => createdUserIds.Any(cu => cu == u.UserId));

                foreach (var doc in docsInPage)
                {
                    //var currentUser = currentUsers.SingleOrDefault(u => u.UserId == (int)doc.UserCurrentId);
                    //var createdUser = createdUsers.SingleOrDefault(u => u.UserId == (int)doc.UserCreatedId);

                    searchItemsView.Add(new SearchItemView
                    {
                        Title = doc.Compendium,
                        DocumentCompendium = doc.Compendium,
                        DocumentId = doc.DocumentId,
                        DocumentCopyId = doc.DocumentCopyId,
                        DatePublished = doc.DatePublished,
                        DateReveiced = doc.DateReceived,
                        DocCode = doc.DocCode,
                        InOutCode = doc.InOutCode,
                        ExtendInfo = new
                        {
                            Organization = doc.Organization,
                            Status = doc.Status,
                            DocCode = doc.DocCode,
                            DatePublished = doc.DatePublished,
                            InOutCode = doc.InOutCode,
                            CurrentUsername = "",
                            CreatedUserName = ""
                        },
                        IsViewed = true
                    });
                }
            }

            return new SearchView
            {
                TotalResult = count,
                Items = searchItemsView.OrderByDescending(d => d.DatePublished)
            };
        }

        public SearchView QuickSearchInFile(int userId, string keyWord, int page = 1, int pageSize = 10)
        {
            var searchItemsView = new List<SearchItemView>();
            var options = new QueryOptions
            {
                Fields = new[] { "id", DocumentId, Title, Content, "contentid", IsFile },
                Highlight = new HighlightingParameters
                {
                    Fields = new[] { Title, Content },
                    Snippets = 1,
                    Fragsize = FragSize,
                    MergeContiguous = false,
                    UseFastVectorHighlighter = true,
                    UsePhraseHighlighter = true,
                    HighlightMultiTerm = true,
                },
                Start = (page - 1) * pageSize,
                Rows = pageSize
            };
            ISolrQuery query = null;
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                var keyWordEscape = keyWord.StripSpecialCharactersForSolr();
                query = (new SolrQueryByField(Content, keyWord) { Quoted = true } ||
                          new SolrQuery(keyWordEscape + "*")) &&
                            ((new SolrQueryByField(Title, keyWord) { Quoted = true } ||
                              new SolrQuery(keyWordEscape + "*")))
                              && new SolrQueryByField(IsFile, "true") { Quoted = true };
            }
            var totalResult = CurrentSolrOperations.Query(query).Count;
            var results = CurrentSolrOperations.Query(query, options);
            if (results.Any())
            {
                var documentIdsValid = results.Select(d => Guid.Parse(d.DocumentId)).ToList();
                var listDocumentAccess =
                    _documentService.FindDocuments(
                        (dc, d) =>
                            new
                            {
                                d.DocumentId,
                                d.Compendium,
                                d.DateAppointed,
                                d.DateResponsed,
                                d.DateResponsedOverdue,
                                d.UrgentId,
                                dc.DocumentCopyId,
                                dc.DocumentCopyType,
                                dc.DateOverdue
                            }, userId, documentIdsValid);
                foreach (var item in results)
                {
                    var hl = string.Empty;
                    if (results.Highlights[item.Id].ContainsKey(Content))
                    {
                        hl = results.Highlights[item.Id][Content].Aggregate("", (current, h) => current + string.Join(",", h));
                    }
                    else
                    {
                        var firstContent = item.Content.FirstOrDefault();
                        if (firstContent != null)
                        {
                            hl = firstContent.Length < FragSize
                                    ? firstContent
                                    : firstContent.Substring(0, FragSize);
                        }
                    }
                    var title = results.Highlights[item.Id].ContainsKey(Title)
                        ? results.Highlights[item.Id][Title].Aggregate("",
                            (current, h) =>
                                current + string.Join(",", h))
                        : item.Title;

                    var view = new SearchItemView
                    {
                        HighLight = hl,
                        Id = item.Id,
                        DocumentId = Guid.Parse(item.DocumentId),
                        IsFile = item.IsFile,
                        Title = title,
                        ContentId = item.ContentId,
                        ExtendInfo = new
                        {
                            FileId = 1
                        }
                    };
                    if (item.IsFile)
                    {
                        var ext = Path.GetExtension(item.Title);
                        view.Extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", "");
                    }
                    var document = listDocumentAccess.Where(d => d.DocumentId.ToString() == item.DocumentId).ToList();
                    if (document.Count() == 1)
                    {
                        var primary = document.First();
                        view.DocumentCompendium = document.First().Compendium;
                        view.DocumentCopyId = primary.DocumentCopyId;
                        view.Color = _documentService.GetColor(primary.UrgentId, primary.DateAppointed,
                                                               primary.DateResponsed, primary.DateResponsedOverdue,
                                                               primary.DateOverdue, primary.DocumentCopyType);
                    }
                    else if (document.Count() > 1)
                    {
                        var primary = document.SingleOrDefault(d => d.DocumentCopyType == (int)DocumentCopyTypes.XuLyChinh) ??
                                      document.First();
                        view.DocumentCompendium = primary.Compendium;
                        view.DocumentCopyId = primary.DocumentCopyId;
                        view.Color = _documentService.GetColor(primary.UrgentId, primary.DateAppointed,
                                                               primary.DateResponsed, primary.DateResponsedOverdue,
                                                               primary.DateOverdue, primary.DocumentCopyType);
                    }
                    searchItemsView.Add(view);
                }
            }
            return new SearchView
            {
                TotalResult = totalResult,
                Items = searchItemsView
            };
        }

        private static string GetSpellCheckingResult(SolrQueryResults<EgovIndex> index)
        {
            return string.Join(" ", index.SpellChecking
                                        .Select(c => c.Suggestions.FirstOrDefault())
                                        .Where(c => !string.IsNullOrEmpty(c))
                                        .ToArray());
        }
    }
}