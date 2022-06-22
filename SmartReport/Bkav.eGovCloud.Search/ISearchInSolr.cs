using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Search
{
    public interface ISearchInSolr
    {
        void AddIndex();

        SearchView Search(int userId, string compendium, string content, int? categoryId = null,
            string keyword = null, string docCode = null,
            string inOutCode = null, int? urgentId = null,
            int? categoryBusinessId = null, int? storePrivateId = null, int? userCurrentId = null,
            DateTime? fromDate = null, DateTime? toDate = null,
            int? inOutPlaceId = null, string organizationCreate = null,
            int? docfieldId = null, int? userSuccessId = null, int page = 1, int pageSize = 10);

        IEnumerable<Guid> Search(IEnumerable<Guid> documentIds, string compendium, string content, int userId);

        SearchView Search(string query, IDictionary<string, string> filter, int userId, int page = 1, int pageSize = 10);

        SearchView QuickSearchInFile(int userId, string keyWord, int page = 1, int pageSize = 10);
    }
}