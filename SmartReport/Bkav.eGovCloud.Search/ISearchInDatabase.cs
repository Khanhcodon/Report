using System;
using Bkav.eGovCloud.Search.Entity;

namespace Bkav.eGovCloud.Search
{
    public interface ISearchInDatabase
    {
        SearchView QuickSearch(int userId, string keyWord, bool isUseCached, int page = 1, int pageSize = 100, bool isMainProcess = true);

        SearchView SearchAdvance(SearchQuery query, int page = 1, int pageSize = 25, bool isMainProcess = true);

        // Todo: xem bỏ hàm này đi
        SearchView SearchAdvanceInDatabase(int userId, string compendium, string content, int? categoryId = null,
            string keyword = null, string docCode = null,
            string inOutCode = null, int? urgentId = null,
            int? categoryBusinessId = null, int? storePrivateId = null, int? userCurrentId = null,
            DateTime? fromDate = null, DateTime? toDate = null,
            int? inOutPlaceId = null, string organizationCreate = null,
            int? docfieldId = null, int? userSuccessId = null, int? userCreatedId = null, int page = 1, int pageSize = 10, bool? isMainProcess = true);
    }
}