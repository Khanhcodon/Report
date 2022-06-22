using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Oauth
{
    [RequireHttps]
    [OAuthAuthorizeAttribute(Scope.Doctype)]
    public class DoctypeController : EgovApiBaseController
    {
        private readonly ApiBll _apiService;
        private readonly OfficeBll _officeService;
        private readonly DocTypeBll _doctypeService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly PaperBll _paperService;
        private readonly DoctypeTemplate _doctypeTempateService;

        public DoctypeController()
        {
            _apiService = DependencyResolver.Current.GetService<ApiBll>();
            _officeService = DependencyResolver.Current.GetService<OfficeBll>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _doctypeFormService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _paperService = DependencyResolver.Current.GetService<PaperBll>();
            _doctypeTempateService = DependencyResolver.Current.GetService<DoctypeTemplate>();

        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="docfieldId"></param>
        /// <returns></returns>
        public IList<DocType> GetDocTypesByDocFieldId(int docfieldId)
        {
            return _apiService.GetsByDocFieldId(docfieldId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="docfieldId"></param>
        /// <returns></returns>
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
        public IEnumerable<DocType> GetMostViewedDocTypes()
        {
            return _apiService.GetsMostViewed();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
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
        public IEnumerable<DocType> GetDocTypesByLevelIdAndDocfieldId(int? levelId = null, int? docfieldId = null)
        {
            var doctypes = _apiService.GetDocTypes(levelId, docfieldId);
            foreach (var item in doctypes)
            {
                item.Office = _officeService.GetOffice((int)item.OfficeId);
            }
            return doctypes;
        }


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


        public IEnumerable<DocType> GetLookupDocTypes(string doctypeName = "",
           int? officeId = null, int? docfieldId = null)
        {
            return _doctypeService.Gets(x => x.DocTypeName.ToLower().Contains(doctypeName.ToLower())
                && x.OfficeId == officeId && x.DocFieldId == docfieldId && x.CategoryBusinessId == 4);
        }


        public IEnumerable<DocTypeForm> GetDocTypeFormsByDocTypeId(Guid doctypeId)
        {
            if (doctypeId == null)
            {
                throw new ArgumentNullException("doctypeId");
            }
            return _doctypeFormService.GetsByDoctypeId(doctypeId);
        }


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
                        doctypePaper.Amount = paper.Amount;
                        doctypePaper.IsRequired = paper.IsRequired;
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
        //
        //public IEnumerable<Template> GetTemplatesByDocTypeId(Guid doctypeId)
        //{
        //    return _tempateService.GetsByDoctypeId(doctypeId);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public IEnumerable<DoctypeTemplate> GetsDoctypeTemplateByDocTypeId(Guid doctypeId)
        //{
        //    var doctypeTemplates = _doctypeTempateService.GetsByDoctypeId(doctypeId);
        //    foreach (var doctypeTemplate in doctypeTemplates)
        //    {
        //        if (doctypeTemplate.OnlineTemplate != null)
        //        {
        //            doctypeTemplate.FileId = doctypeTemplate.OnlineTemplate.FileId;
        //        }
        //    }
        //    return doctypeTemplates;
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="formId"></param>
        ///// <returns></returns>

        //public IEnumerable<Form> GetsFormByDoctypeId(Guid doctypeId)
        //{
        //    var forms = new List<Form>();
        //    var doctypeForms = _doctypeFormService.Gets(x => x.DocTypeId == doctypeId);
        //    foreach (var doctypeForm in doctypeForms)
        //    {
        //        var form = _formService.Get(doctypeForm.FormId);
        //        if (form != null)
        //        {
        //            form.IsPrimary = doctypeForm.IsPrimary;
        //            forms.Add(form);
        //        }
        //    }
        //    return forms;
        //}
    }
}