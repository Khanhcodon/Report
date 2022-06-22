using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Oauth
{
    //[RequireHttps]
    public class EgovApiController : EgovApiBaseController
    {
        private readonly OfficeBll _officeService;
        private readonly LevelBll _levelService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocTypeBll _doctypeService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly ApiBll _apiService;
        private readonly GuideBll _guideService;
        private readonly QuestionBll _questionService;
        private readonly LawBll _lawService;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly FormHelper _formHelper;

        //private readonly DocTypeLawBll _doctypeLawService;
        private readonly CitizenBll _peopleService;

        private readonly DocumentOnlineBll _documentOnlineService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly JobTitlesBll _jobTitlesService;

        private readonly Bkav.eGovCloud.Business.Customer.FileBll _fileService;

        //private readonly TemplateBll _tempateService;
        private readonly DoctypeTemplateBll _doctypeTempateService;

        private readonly PaperBll _paperService;
        private readonly FormBll _formService;
        private readonly UserBll _userService;

        public EgovApiController()
        {
            _documentOnlineService = DependencyResolver.Current.GetService<DocumentOnlineBll>();
            _peopleService = DependencyResolver.Current.GetService<CitizenBll>();
            //_doctypeLawService = DependencyResolver.Current.GetService<DocTypeLawBll>();
            _questionService = DependencyResolver.Current.GetService<QuestionBll>();
            _guideService = DependencyResolver.Current.GetService<GuideBll>();
            _apiService = DependencyResolver.Current.GetService<ApiBll>();
            _doctypeFormService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _levelService = DependencyResolver.Current.GetService<LevelBll>();
            _lawService = DependencyResolver.Current.GetService<LawBll>();
            _officeService = DependencyResolver.Current.GetService<OfficeBll>();
            _docfieldService = DependencyResolver.Current.GetService<DocFieldBll>();
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
            _docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
            _fileService = DependencyResolver.Current.GetService<Bkav.eGovCloud.Business.Customer.FileBll>();

            _userService = DependencyResolver.Current.GetService<UserBll>();
            //_tempateService = DependencyResolver.Current.GetService<TemplateBll>();
            _paperService = DependencyResolver.Current.GetService<PaperBll>();
            _doctypeTempateService = DependencyResolver.Current.GetService<DoctypeTemplateBll>();
            _formService = DependencyResolver.Current.GetService<FormBll>();
            _formHelper = DependencyResolver.Current.GetService<FormHelper>();
            _positionService = DependencyResolver.Current.GetService<PositionBll>();
            _jobTitlesService = DependencyResolver.Current.GetService<JobTitlesBll>();
        }

        #region "Enum Scope"

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Scope)]
        public List<string> GetScopeList()
        {
            return _apiService.GetScopeList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Scope)]
        public string GetScopeName(int key)
        {
            return _apiService.GetScopeName(key);
        }

        #endregion "Enum Scope"

        #region "Api Functions"

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Scope)]
        public List<EnumModel> GetApiFunctions()
        {
            return _apiService.GetApiEnums();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Scope)]
        public string GetApiName(int key)
        {
            return _apiService.GetApiName(key);
        }

        #endregion "Api Functions"

        #region"Office"

        /// <summary>
        ///
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public IEnumerable<Office> GetParentOffices(int parentId)
        {
            return _officeService.GetChildren(parentId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public IEnumerable<Office> GetAllOffice()
        {
            return _officeService.GetOffices();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public Office GetOfficeByDoctypeId(Guid docTypeId)
        {
            return _officeService.GetOffice(docTypeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public IEnumerable<Office> GetAllParentOffice()
        {
            return _officeService.GetOffices();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public IEnumerable<Office> GetOfficesByLevelId(int levelId)
        {
            return _officeService.GetOfficesByLevelId(levelId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="officeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public Office GetOfficeById(int officeId)
        {
            return _officeService.GetOffice(officeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="officeName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Office)]
        public Dictionary<int, List<Office>> GetOffices(
                                      int currentPage = 1,
                                      int? pageSize = null,
                                      string sortBy = "",
                                      bool isDescending = false,
                                      string officeName = "",
                                      string levelId = "")
        {
            int totalRecords;
            var models = _officeService.GetOffices(out totalRecords, currentPage, pageSize, sortBy, isDescending, officeName, levelId);
            var dictionary = new Dictionary<int, List<Office>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        #endregion

        #region "DocField"

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Docfield)]
        public DocField GetDocFieldByDoctypeId(Guid doctypeId)
        {
            return _apiService.GetDocField(doctypeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocField> GetAllDocField()
        {
            return _apiService.GetDocFields();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="docfieldName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Docfield)]
        public Dictionary<int, List<DocField>> GetDocFields(
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string docfieldName = "")
        {
            int totalRecords;
            var models = _apiService.GetDocFields(out totalRecords, currentPage, pageSize, sortBy, isDescending, docfieldName);
            var dictionary = new Dictionary<int, List<DocField>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Docfield)]
        public DocField GetDocFieldById(int id)
        {
            return _apiService.GetDocField(id);
        }

        #endregion

        #region "Law"

        [OAuthAuthorizeAttribute(Scope.Law)]
        public Dictionary<int, List<Law>> GetLaws(int currentPage = 1,
            int? pageSize = null, string sortBy = "", bool isDescending = false,
            string lawName = "")
        {
            int totalRecords;
            var models = _lawService.GetLaws(out totalRecords, currentPage, pageSize, sortBy, isDescending, lawName);
            var dictionary = new Dictionary<int, List<Law>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Law)]
        public Law GetLawById(int id)
        {
            return _lawService.GetById(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lawId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Law)]
        public string GetNumberSign(int lawId)
        {
            return _lawService.NumberSign(lawId);
        }

        /// <summary>
        /// Trả về Law theo doctypeId
        /// </summary>
        /// <param name="doctypeId">Mã hồ sơ</param>
        /// <returns></returns>
        // [OAuthAuthorizeAttribute(Scope.Law)]
        public IEnumerable<Law> GetsLawByDoctypeId(Guid doctypeId)
        {
            var laws = _doctypeService.GetsDoctypeLaw(doctypeId).Select(x => x.Law);
            foreach (var law in laws)
            {
                foreach (var file in law.Files)
                {
                    law.FileIds += file.FileId + ",";
                }
            }

            return laws;
        }

        #endregion

        #region "Level"

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public IEnumerable<Administrative> GetLevelsByType(int type)
        {
            return _levelService.GetLevelsByType(type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public int GetLevelByType(int type)
        {
            return _levelService.GetLevelByType(type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public IEnumerable<Administrative> GetAllLevel()
        {
            return _levelService.GetLevels();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public Administrative GetLevelByDoctypeId(Guid doctypeId)
        {
            return _levelService.GetLevel(doctypeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="officeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public Administrative GetLevelByOfficeId(int officeId)
        {
            return _levelService.GetLevel(officeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public Administrative GetLevelById(int id)
        {
            return _levelService.Get(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="levelName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Level)]
        public Dictionary<int, List<Administrative>> GetLevels(
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string levelName = "")
        {
            int totalRecords;
            var models = _levelService.GetLevels(out totalRecords, currentPage, pageSize, sortBy, isDescending, levelName);
            var dictionary = new Dictionary<int, List<Administrative>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        #endregion

        #region "DocType"

        /// <summary>
        ///
        /// </summary>
        /// <param name="docfieldId"></param>
        /// <returns></returns>
        // [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IList<DocType> GetDocTypesByDocFieldId(int docfieldId)
        {
            return _apiService.GetsByDocFieldId(docfieldId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="docfieldId"></param>
        /// <returns></returns>
        // [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IList<DocType> GetDocTypesByOfficeId(int officeId)
        {
            return _apiService.GetDocTypesByOfficeId(officeId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="doctypeName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public Dictionary<int, List<DocType>> GetDocTypes(int currentPage = 1,
                                                int? pageSize = null,
                                                string sortBy = "",
                                                bool isDescending = false,
                                                string doctypeName = "",
                                                string officeId = "",
                                                string docFieldId = "",
                                                string levelId = "")
        {
            int totalRecords;
            var doctypes = _apiService.GetDocTypes(out totalRecords, currentPage, pageSize, sortBy, isDescending, doctypeName, officeId, docFieldId, levelId)
                .Where(d => (d.CategoryBusinessId == 4) && (d.IsAllowOnline == true));
            foreach (var item in doctypes)
            {
                //  item.DocField = _docfieldService.Get((int)item.DocFieldId);
                //   item.Level = _levelService.Get((int)item.LevelId);
                item.Office = _officeService.GetOffice((int)item.OfficeId);
            }
            var dictionary = new Dictionary<int, List<DocType>>();
            dictionary.Add(totalRecords, doctypes.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public DocType GetDocTypeById(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            return _apiService.GetDocType(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DocType> GetAllDocTypes()
        {
            return _apiService.GetDocTypes();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByActionLevelWithDoctypeName(string doctypeName)
        {
            if (doctypeName == null)
            {
                doctypeName = "";
            }
            var doctypes = _apiService.GetDocTypesByActionLevel(doctypeName);
            foreach (var item in doctypes)
            {
                //  item.DocField = _docfieldService.Get((int)item.DocFieldId);
                //  item.Level = _levelService.Get((int)item.LevelId);
                item.Office = _officeService.GetOffice((int)item.OfficeId);
            }
            return doctypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByDoctypeName(string doctypeName)
        {
            if (doctypeName == null)
            {
                doctypeName = "";
            }
            var doctypes = _apiService.GetDocTypesByDoctypeName(doctypeName);
            foreach (var item in doctypes)
            {
                //  item.DocField = _docfieldService.Get((int)item.DocFieldId);
                //item.Level = _levelService.Get((int)item.LevelId);
                item.Office = _officeService.GetOffice((int)item.OfficeId);
            }
            return doctypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nameNotMarks"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByNameNotMarks(string nameNotMarks)
        {
            if (nameNotMarks == null)
            {
                nameNotMarks = "";
            }
            return _apiService.GetDocTypesByNameNotMarks(nameNotMarks);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeName"></param>
        /// <param name="officeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByActionLevelWithDoctypeNameAndOfficeIdAndDocfieldId(string doctypeName, int? officeId = null, int? docfieldId = null)
        {
            if (doctypeName == null)
            {
                doctypeName = "";
            }
            var doctypes = _apiService.GetDocTypesByActionLevel(doctypeName, officeId, docfieldId);
            foreach (var item in doctypes)
            {
                //  item.DocField = _docfieldService.Get((int)item.DocFieldId);
                // item.Level = _levelService.Get((int)item.LevelId);
                item.Office = _officeService.GetOffice((int)item.OfficeId);
            }
            return doctypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doctypeName"></param>
        /// <param name="officeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByDoctypeNameAndOfficeIdAndDocfieldId(string doctypeName = "", int? officeId = null, int? docfieldId = null)
        {
            var doctypes = _apiService.GetDocTypes(doctypeName, officeId, docfieldId);
            foreach (var item in doctypes)
            {
                if (item.OfficeId != null)
                {
                    item.Office = _officeService.GetOffice((int)item.OfficeId);
                }
            }
            return doctypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetAllDocType()
        {
            var doctypes = _apiService.GetDocTypes();
            foreach (var item in doctypes)
            {
                if (item.OfficeId != null)
                {
                    item.Office = _officeService.GetOffice((int)item.OfficeId);
                }
            }
            return doctypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetMostViewedDocTypes()
        {
            return _apiService.GetsMostViewed();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByActionLevel()
        {
            var doctypes = _apiService.GetDocTypes();
            foreach (var item in doctypes)
            {
                if (item.OfficeId != null)
                {
                    item.Office = _officeService.GetOffice((int)item.OfficeId);
                }
            }
            return doctypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="levelId"></param>
        /// <param name="docfieldId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetDocTypesByLevelIdAndDocfieldId(int? levelId = null, int? docfieldId = null)
        {
            var doctypes = _apiService.GetDocTypes(levelId, docfieldId);
            foreach (var item in doctypes)
            {
                item.Office = _officeService.GetOffice((int)item.OfficeId);
            }
            return doctypes;
        }

        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetFilterDocType(
            string name = null, int? officeId = null, int levelId = 0,
            int? docFieldId = null, string nameNotMarks = null,
            int currentPage = 1, int pageSize = 25)
        {
            var result = _doctypeService.Gets(d =>
                (!string.IsNullOrEmpty(name) ? d.DocTypeName.Contains(name) : true)
                && (officeId.HasValue ? d.OfficeId == officeId : true)
                && (levelId != 0 ? d.LevelId == levelId : true)
                && (docFieldId.HasValue ? d.DocFieldId == docFieldId : true)
                && (!string.IsNullOrEmpty(nameNotMarks) ? d.Unsigned.ToLower().Contains(nameNotMarks.ToLower()) : true)
                && (levelId != 0 ? d.LevelId == levelId : true)
                );
            result = result.Skip((currentPage - 1) * pageSize).Take(pageSize);
            foreach (var item in result)
            {
                if (item.OfficeId != null)
                {
                    item.Office = _officeService.GetOffice((int)item.OfficeId);
                }
            }
            return result;
        }

        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocType> GetLookupDocTypes(string doctypeName = "",
           int? officeId = null, int? docfieldId = null)
        {
            return _doctypeService.Gets(x => x.DocTypeName.ToLower().Contains(doctypeName.ToLower())
                && x.OfficeId == officeId && x.DocFieldId == docfieldId && x.CategoryBusinessId == 4);
        }

        // [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DocTypeForm> GetDocTypeFormsByDocTypeId(Guid doctypeId)
        {
            if (doctypeId == null)
            {
                throw new ArgumentNullException("doctypeId");
            }
            return _doctypeFormService.GetsByDoctypeId(doctypeId);
        }

        // [OAuthAuthorizeAttribute(Scope.Doctype)]
        public List<DoctypePaper> GetDocTypePapersByDocTypeId(Guid doctypeId)
        {
            var results = new List<DoctypePaper>();
            var doctype = GetDocTypeById(doctypeId);

            if (doctype != null)
            {
                foreach (var doctypePaper in doctype.DoctypePapers)
                {
                    var paper = _paperService.Get(doctypePaper.PaperId);
                    if (paper != null)
                    {
                        doctypePaper.PaperName = paper.PaperName;
                        doctypePaper.Amount = 1;
                    }
                }
                results = doctype.DoctypePapers.ToList();
            }

            return results;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Doctype)]
        //public IEnumerable<Template> GetTemplatesByDocTypeId(Guid doctypeId)
        //{
        //    return _tempateService.GetsByDoctypeId(doctypeId);
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<DoctypeTemplate> GetsDoctypeTemplateByDocTypeId(Guid doctypeId)
        {
            var doctypeTemplates = _doctypeTempateService.GetsByDoctypeId(doctypeId);
            foreach (var doctypeTemplate in doctypeTemplates)
            {
                if (doctypeTemplate.OnlineTemplate != null)
                {
                    doctypeTemplate.FileId = doctypeTemplate.OnlineTemplate.FileId;
                }
            }
            return doctypeTemplates;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Doctype)]
        public IEnumerable<Form> GetsFormByDoctypeId(Guid doctypeId)
        {
            var forms = new List<Form>();
            var doctypeForms = _doctypeFormService.Gets(x => x.DocTypeId == doctypeId);
            foreach (var doctypeForm in doctypeForms)
            {
                var form = _formService.Get(doctypeForm.FormId);
                if (form != null)
                {
                    form.Json = _formHelper.ParseFormModel(form).StringifyJs();
                    form.IsPrimary = doctypeForm.IsPrimary;
                    forms.Add(form);
                }
            }
            return forms;
        }

        [OAuthAuthorizeAttribute(Scope.Document)]
        public string GetDocumentProcess(Guid documentId, DateTime? lastUpdated)
        {
            var document = _documentService.Get(documentId);
            var documentCopys = _docCopyService.Gets(documentId, lastUpdated);
            var users = new List<UserProcessing>();
            foreach (var documentCopy in documentCopys)
            {
                users.Add(new UserProcessing
                {
                    DocumentCopyId = documentCopy.DocumentCopyId,
                    FullName = documentCopy.UserCurrent.FullName,
                    Departments = documentCopy.UserCurrent.DepartmentJobTitlesId,
                    Comment = documentCopy.LastComment,
                    CommentDate = documentCopy.LastDateComment
                });
            }
            documentCopys.Select(x => x.UserCurrent);

            var documentProcessing = new DocumentProcessing
            {
                DateAppoint = document.DateAppointed,
                IsReturned = document.IsReturned,
                IsSuccess = document.IsSuccess,
                IsSupplemented = document.IsSupplemented,
                Status = document.Status,
                Users = users
            };

            return JsonConvert.SerializeObject(documentProcessing);
        }

        #endregion

        #region "Form"

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="formName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Form)]
        public Dictionary<int, List<Form>> GetForms(
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string formName = "")
        {
            int totalRecords;
            var models = _apiService.GetForms(out totalRecords, currentPage, pageSize, sortBy, isDescending, formName).Where(f => f.FormTypeId == 2);
            var dictionary = new Dictionary<int, List<Form>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Form)]
        public string GetJson(Guid formId)
        {
            return _apiService.GetJson(formId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Form)]
        public Form GetFormById(Guid formId)
        {
            return _apiService.GetForm(formId);
        }

        #endregion

        #region "FormGroup"

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.FormGroup)]
        public IEnumerable<FormGroup> GetAllFormGroup()
        {
            return _apiService.GetFormGroups();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.FormGroup)]
        public Form GetForm(Guid formId)
        {
            return _apiService.GetForm(formId);
        }

        #endregion

        #region "Guide"

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Guide)]
        public IEnumerable<Guide> GetAllGuide()
        {
            return _guideService.GetGuides();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="questionName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Guide)]
        public Dictionary<int, List<Guide>> GetGuides(
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string guideName = "")
        {
            int totalRecords;
            var models = _guideService.GetGuides(out totalRecords, currentPage, pageSize, sortBy, isDescending, guideName);
            var dictionary = new Dictionary<int, List<Guide>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="guideUrl"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Guide)]
        public Guide GetGuideByGuideUrl(string guideUrl)
        {
            return _guideService.GetByUrl(guideUrl);
        }

        #endregion

        #region"Question"

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="questionName"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Question)]
        public Dictionary<int, List<Question>> GetQuestions(
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string questionName = "")
        {
            int totalRecords;
            var models = _questionService.GetQuestions(out totalRecords, currentPage, pageSize, sortBy, isDescending, questionName);
            var dictionary = new Dictionary<int, List<Question>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Question)]
        public Dictionary<int, List<Question>> GetQuestionsForHome()
        {
            int totalRecords;
            var models = _questionService.GetQuestionsForHome(totalRecords: out totalRecords, page: 1, pageSize: 5, sortBy: "Date", isDescending: true);
            var dictionary = new Dictionary<int, List<Question>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Question)]
        public Question GetQuestionByTagName(string tag)
        {
            return _questionService.GetByTag(tag);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Question)]
        public Question GetQuestionById(int id)
        {
            return _questionService.GetById(id);
        }

        /// <summary>
        /// 60
        /// </summary>
        /// <param name="askPeople"></param>
        /// <param name="date"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [OAuthAuthorizeAttribute(Scope.Question)]
        public string GetQuestionCreate(string askPeople, DateTime date, string detail)
        {
            return _questionService.GetQuestionCreate(askPeople, date, detail);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [OAuthAuthorizeAttribute(Scope.Question)]
        public IEnumerable<Question> GetAllQuestion()
        {
            return _questionService.Gets();
        }

        #endregion

        #region "File"

        [OAuthAuthorizeAttribute(Scope.File)]
        public string GetFileInBase64(int fileId)
        {
            string fileName;
            var fileStream = _fileService.DownloadFile(out fileName, fileId);
            var fileBase64 = StreamToBase64(fileStream);
            return fileBase64;
        }

        // [OAuthAuthorizeAttribute(Scope.File)]
        public HttpResponseMessage GetStreamFile(int fileId)
        {
            string fileName;
            var stream = _fileService.DownloadFile(out fileName, fileId);
            if (stream != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return response;
            }

            return null;
        }

        // [OAuthAuthorizeAttribute(Scope.File)]
        public File GetFile(int fileId)
        {
            return _fileService.GetFile(fileId);
        }

        #endregion

        #region"Document"

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public Document GetDocumentById(Guid id)
        {
            return _apiService.GetDocumentById(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Document)]
        public DocumentOnline GetDocumentOnlineById(Guid id)
        {
            return _apiService.GetDocumentOnlineById(id);
        }

        /// <summary>
        /// 55
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<DocumentOnline> GetWaitingForSupplementUserDocuments(string identityCard, string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetWaitingForSupplementUserDocuments(identityCard, doccode);
        }

        /// <summary>
        /// 54
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<DocumentOnline> GetRefusedUserDocuments(string identityCard, string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetRefusedUserDocuments(identityCard, doccode);
        }

        /// <summary>
        /// 53
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<Document> GetWaitingForPaymentUserDocuments(string identityCard, string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetWaitingForPaymentUserDocuments(identityCard, doccode);
        }

        /// <summary>
        /// 52
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<Document> GetProcessingUserDocuments(string identityCard, string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetProcessingUserDocuments(identityCard, doccode);
        }

        /// <summary>
        /// 51
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<Document> GetWaitingForReceiveResultUserDocuments(string identityCard, string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetWaitingForReceiveResultUserDocuments(identityCard, doccode);
        }

        /// <summary>
        /// 50
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<Document> GetHavingResultUserDocuments(string identityCard, string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetHavingResultUserDocuments(identityCard, doccode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="doccode"></param>
        /// <param name="officeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public Dictionary<int, List<Document>> GetDocuments(string doccode = "", int? officeId = null, int page = 1, int? pageSize = null)
        {
            if (doccode == null)
            {
                doccode = "";
            }
            int totalRecords;
            var models = _apiService.GetDocuments(out totalRecords, doccode, page, pageSize);
            var dictionary = new Dictionary<int, List<Document>>();
            dictionary.Add(totalRecords, models.ToList());
            return dictionary;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public Document GetDocumentByDoccode(string doccode)
        {
            return _apiService.GetDocumentByDoccode(doccode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<Document> GetAllDocument()
        {
            return _apiService.GetDocuments();
        }

        /// <summary>
        /// 48
        /// </summary>
        /// <param name="officeId"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<Document> GetDocumentsByOfficeId(int? officeId = null)
        {
            return _apiService.GetDocuments().Where(x => x.DateReturned.Value.Date == DateTime.Now.Date).OrderByDescending(x => x.DateReturned.Value);
        }

        /// <summary>
        /// 50
        /// </summary>
        /// <param name="identityCard"></param>
        /// <param name="doccode"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Document)]
        public IEnumerable<DocumentOnline> GetWaitingForReceiveUserDocuments(string identityCard,
                                                                             string doccode = "")
        {
            if (doccode == null)
            {
                doccode = "";
            }
            return _apiService.GetDocuments(identityCard, doccode);
        }

        #endregion

        #region "DoctypeLaw"

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.Doctype)]
        public ICollection<DocTypeLaw> GetDocTypeLawByDoctypeId(Guid id)
        {
            var docType = _doctypeService.Get(id);
            return docType.DocTypeLaws;
        }

        #endregion

        #region "People"

        /// <summary>
        /// 50
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.People)]
        public string GetUserUpdate(string account, string password, string fullName, string firstName, string lastName, string phoneNumber, string email, string idCardNumber, DateTime? dateOfIssue, string placeOfIssue, bool isActivated)
        {
            return _peopleService.UserUpdate(account, password, fullName, firstName, lastName, phoneNumber, email, idCardNumber, dateOfIssue, placeOfIssue, isActivated);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="fullName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="idCardNumber"></param>
        /// <param name="dateOfIssue"></param>
        /// <param name="placeOfIssue"></param>
        /// <param name="isActivated"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.People)]
        public string GetUserCreate(string account, string password, string fullName, string firstName, string lastName, string phoneNumber, string email, string idCardNumber, DateTime? dateOfIssue, string placeOfIssue, bool isActivated)
        {
            return _peopleService.UserCreate(account, password, fullName, firstName, lastName, phoneNumber, email, idCardNumber, dateOfIssue, placeOfIssue, isActivated);
        }

        //
        /// <summary>
        /// 49
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.People)]
        public Citizen GetUserByAccountAndPassword(string account, string password)
        {
            return _peopleService.GetByAccount(account, password);
        }

        /// <summary>
        /// 50
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.People)]
        public Citizen GetUserByAccount(string account)
        {
            return _peopleService.GetByAccount(account);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.People)]
        public Citizen GetUserById(int id)
        {
            return _peopleService.GetById(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [OAuthAuthorizeAttribute(Scope.People)]
        public int GetUserIdByAccount(string account)
        {
            return _peopleService.GetUserIdByAccount(account);
        }

        #endregion

        #region "Statistic"

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="startdate"></param>
        ///// <param name="startdate"></param>
        ///// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Document)]
        //public IEnumerable<Statistic> GetStatisticAll(string startdate, string enddate)
        //{
        //    var model = _officeService.GetOffices();
        //    var officeids = new List<int>();
        //    foreach (var item in model)
        //    {
        //        var officeid = item.OfficeId;
        //        officeids.Add(officeid);
        //    }
        //    return _apiService.GetStatisticByOffice(startdate, enddate, officeids);
        //}

        //public string GetDeptAndUsers()
        //{
        //    try
        //    {
        //        var depts = _departmentService.GetReadOnlys().Where(x => x.CreatedOnDate != null);
        //        foreach (var dept in depts)
        //        {
        //            var id = dept.DepartmentId;
        //            var userDepartmentJobTitlesPositions = dept.UserDepartmentJobTitlesPositions;
        //            foreach (var userDepartmentJobTitlesPosition in userDepartmentJobTitlesPositions)
        //            {
        //                var user = userDepartmentJobTitlesPosition.User;
        //                if (user == null)
        //                {
        //                    throw new Exception("user - userDepartmentJobTitlesPosition:" + userDepartmentJobTitlesPosition.UserDepartmentJobTitlesPositionId);
        //                }
        //            }
        //        }
        //        var results = _departmentService.GetsAs(p => new
        //        {
        //            DepartmentId = p.DepartmentId,
        //            ParentId = p.ParentId,
        //            DepartmentName = p.DepartmentName,
        //            DepartmentIdExt = p.DepartmentIdExt,
        //            DepartmentPath = p.DepartmentPath,
        //            Order = p.Order,
        //            Level = p.Level
        //            //Users = p.UserDepartmentJobTitlesPositions.Select(x => x.User)
        //            //.Select(x => new
        //            //{
        //            //    x.UserId,
        //            //    x.Username,
        //            //    x.FullName
        //            //})
        //        }, true).Stringify();

        //        return results;
        //    }
        //    catch (System.Data.EntityCommandExecutionException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //#endregion

        ///// <param name="endTime"></param>
        ///// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Document)]
        //public IEnumerable<Statistic> GetStatistics(string officeIds, string startDate, string endDate)
        //{
        //    officeIds = officeIds.Replace("[", "").Replace("]", "");
        //    var array = officeIds.Split(',');
        //    var list = new List<int>();
        //    foreach (var item in array)
        //    {
        //        list.Add(Convert.ToInt32(item));
        //    }
        //    var model = _officeService.GetOffices();
        //    return _apiService.GetStatisticByOffice(startDate, endDate, list);
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //[OAuthAuthorizeAttribute(Scope.Document)]
        //public IEnumerable<Statistic> GetStatisticByDocField(int officeId, string startDate, string endDate)
        //{
        //    var doctypeIds = new List<Guid>();
        //    var model = _doctypeService.Gets().Where(d => d.CategoryBusinessId == 4 && d.OfficeId == officeId);
        //    foreach (var item in model)
        //    {
        //        doctypeIds.Add(item.DocTypeId);
        //    }
        //    return _apiService.GetStatisticByDocField(startDate, endDate, doctypeIds);
        //}

        //#region "Test Statistic"

        ///// <summary>
        ///// Lấy ra thống kê theo cấp hành chính
        ///// </summary>
        ///// <param name="startDate">Ngày bắt đầu thống kê</param>
        ///// <param name="endDateint">Ngày kết thúc thống kê</param>
        ///// <param name="levelId">Id cấp hành chính</param>
        ///// <returns></returns>
        //public IEnumerable<Statistic> GetStatisticByLevel()
        //{
        //    var startDate = "2013/10/10";
        //    var endDate = "2014/10/21";
        //    var levelIds = new List<int>
        //    {
        //       1,6,9
        //    };
        //    return _apiService.GetStatisticByOffice(startDate, endDate, levelIds);
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<DocFieldStatistic> GetStatisticByDocType(int officeId)
        //{
        //    var startDate = "2013/10/10";
        //    var endDate = "2014/10/21";
        //    var doctypeIds = new List<Guid>();
        //    var model = _doctypeService.Gets().Where(d => d.CategoryBusinessId == 4 && d.OfficeId == officeId);
        //    foreach (var item in model)
        //    {
        //        doctypeIds.Add(item.DocTypeId);
        //    }
        //    return _apiService.GetStatisticByDocType(startDate, endDate, doctypeIds);
        //}

        //#endregion

        #endregion "Statistic"

        #region người dùng, phòng ban, chức vụ

        public IEnumerable<dynamic> GetAllDepartment()
        {
            var result = _departmentService
                .GetCacheAllDepartments(true)
                .Select(u => new
                {
                    value = u.DepartmentId,
                    parentid = u.ParentId.HasValue ? u.ParentId : 0,
                    data = u.DepartmentName,
                    metadata = new { id = u.DepartmentId },
                    idext = u.DepartmentIdExt,
                    state = "leaf",
                    order = u.Order,
                    level = u.Level,
                    attr = new { id = u.DepartmentId, rel = "dept", idext = u.DepartmentIdExt, label = u.DepartmentPath },
                    label = u.DepartmentPath
                });
            return result;
        }

        /// <summary>
        ///   Trả về danh sách user trong cơ quan
        /// </summary>
        /// <returns>Json object danh sách tất cả user.</returns>
        public IEnumerable<dynamic> GetAllUsers()
        {
            var result = _userService.GetCacheAllUsers(true)
                .Select(u => new
                {
                    value = u.UserId,
                    label = u.Username + " - " + u.FullName,
                    fullname = u.FullName,
                    username = u.Username
                })
                .OrderBy(u => u.username);
            return result;
        }

        /// <summary>
        /// Trả về danh sách tất cả các quan hệ người dùng - phòng ban - chức vụ.
        /// </summary>
        /// <returns>Json object danh sách quan hệ người dùng - phòng ban - chức vụ.</returns>
        public IEnumerable<dynamic> GetAllUserDepartmentJobTitlesPosition()
        {
            var result = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
                t =>
                    new
                    {
                        departmentid = t.DepartmentId,
                        userid = t.UserId,
                        positionid = t.PositionId,
                        idext = t.DepartmentIdExt,
                        jobtitleid = t.JobTitlesId
                    });
            return result;
        }

        public IEnumerable<dynamic> GetAllPositions()
        {
            var result = _positionService.GetCacheAllPosition().Select(
                    u => new { positionId = u.PositionId, positionName = u.PositionName });
            return result;
        }

        public IEnumerable<dynamic> GetAllJobTitles()
        {
            var result = _jobTitlesService.GetCacheAllJobtitles()
                .Select(u => new { jobTitlesId = u.JobTitlesId, jobTitlesName = u.JobTitlesName });
            return result;
        }

        #endregion

        public IEnumerable<dynamic> GetSyncDocTypes()
        {
            var syncPermission = (int)DocTypePermissions.DuocPhepLienThong;
            var docTypes = _doctypeService.Gets(d => d.DocTypePermission.HasValue && (d.DocTypePermission.Value & syncPermission) == syncPermission).Select(u => new
            {
                DocTypeId = u.DocTypeId,
                DocTypeName = u.DocTypeName
            });
            return docTypes;
        }

        private string StreamToBase64(System.IO.Stream stream)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                using (stream)
                {
                    var buffer = new byte[4096];
                    while (true)
                    {
                        var count = stream.Read(buffer, 0, 4096);
                        if (count != 0)
                            ms.Write(buffer, 0, count);
                        else
                            break;
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}