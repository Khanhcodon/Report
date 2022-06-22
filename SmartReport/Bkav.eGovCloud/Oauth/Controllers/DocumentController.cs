//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Mvc;
//using Bkav.eGovCloud.Business;
//using Bkav.eGovCloud.Business.Customer;
//using Bkav.eGovCloud.Core.Utils;
//using Bkav.eGovCloud.Entities.Common;
//using Bkav.eGovCloud.Entities.Customer;
//using Bkav.eGovCloud.Entities.Enum;
//using Bkav.eGovCloud.Web.Framework;
//using Bkav.eGovOnline.Business.Customer;
//using Newtonsoft.Json;

//namespace Bkav.eGovCloud.Oauth
//{
//    [RequireHttps]
//    [OAuthAuthorizeAttribute(Scope.Document)]
//    public class DocumentController : EgovApiBaseController
//    {
//        private readonly ApiBll _apiService;
//        private readonly FileBll _fileService;
//        private readonly OfficeBll _officeService;
//        private readonly DocTypeBll _doctypeService;
//        private readonly DocumentBll _documentService;
//        private readonly DocumentCopyBll _documentCopyService;

//        public DocumentController()
//        {
//            _apiService = DependencyResolver.Current.GetService<ApiBll>();
//            _fileService = DependencyResolver.Current.GetService<FileBll>();
//            _officeService = DependencyResolver.Current.GetService<OfficeBll>();
//            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
//            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
//            _documentCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
//        }

//        public string GetDocumentProcess(Guid documentId, DateTime? lastUpdated)
//        {
//            var document = _documentService.Get(documentId);
//            var documentCopys = _documentCopyService.Gets(documentId, lastUpdated);
//            var users = new List<UserProcessing>();
//            foreach (var documentCopy in documentCopys)
//            {
//                users.Add(new UserProcessing
//                {
//                    DocumentCopyId = documentCopy.DocumentCopyId,
//                    FullName = documentCopy.UserCurrent.FullName,
//                    Departments = documentCopy.UserCurrent.DepartmentJobTitlesId,
//                    Comment = documentCopy.LastComment,
//                    CommentDate = documentCopy.LastDateComment
//                });
//            }
//            documentCopys.Select(x => x.UserCurrent);

//            var documentProcessing = new DocumentProcessing
//            {
//                DateAppoint = document.DateAppointed,
//                IsReturned = document.IsReturned,
//                IsSuccess = document.IsSuccess,
//                IsSupplemented = document.IsSupplemented,
//                Status = document.Status,
//                Users = users
//            };

//            return JsonConvert.SerializeObject(documentProcessing);
//        }

//        /// <summary>
//        ///
//        /// </summary>
//        /// <param name="currentPage"></param>
//        /// <param name="pageSize"></param>
//        /// <param name="sortBy"></param>
//        /// <param name="isDescending"></param>
//        /// <param name="doccode"></param>
//        /// <param name="officeId"></param>
//        /// <returns></returns>
//        public Dictionary<int, List<Document>> GetDocuments(string doccode = "", int? officeId = null, int page = 1, int? pageSize = null)
//        {
//            if (doccode == null)
//            {
//                doccode = "";
//            }
//            int totalRecords;
//            var models = _apiService.GetDocuments(out totalRecords, doccode, page, pageSize);
//            var dictionary = new Dictionary<int, List<Document>>();
//            dictionary.Add(totalRecords, models.ToList());
//            return dictionary;
//        }

//        /// <summary>
//        ///
//        /// </summary>
//        /// <param name="doccode"></param>
//        /// <returns></returns>
//        public Document GetDocumentByDoccode(string doccode)
//        {
//            return _apiService.GetDocumentByDoccode(doccode);
//        }

//        /// <summary>
//        ///
//        /// </summary>
//        /// <returns></returns>
//        public IEnumerable<Document> GetAllDocument()
//        {
//            return _apiService.GetDocuments();
//        }
//    }
//}