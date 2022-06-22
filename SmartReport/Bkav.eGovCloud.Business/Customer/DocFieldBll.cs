using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DocFieldBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 080912</para>
    /// <para>Author      : GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng DocField trong CSDL</para>
    /// </summary>
    public class DocFieldBll : ServiceBase
    {
        private readonly IRepository<DocField> _docFieldRepository;
        private readonly DocTypeBll _doctypeservice;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IRepository<DocfieldDoctypeWorkflow> _docfieldDoctypeWorkflowRepository;

        /// <summary>
        /// Khởi tạo class <see cref="DocFieldBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="doctypeservice">Bll tương ứng với bảng DocType trong CSDL</param>
        /// <param name="resourceService">Bll tương ứng với bảng Resource trong CSDL</param>
        /// <param name="generalSettings"></param>
        public DocFieldBll(IDbCustomerContext context, DocTypeBll doctypeservice, ResourceBll resourceService, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _docFieldRepository = Context.GetRepository<DocField>();
            _doctypeservice = doctypeservice;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _docfieldDoctypeWorkflowRepository = Context.GetRepository<DocfieldDoctypeWorkflow>();
        }

        /// <summary> TienBV 011112
        /// Lất ra tất cả các lĩnh vực theo điều kiện.
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<DocField> Gets(Expression<Func<DocField, bool>> spec = null)
        {
            return _docFieldRepository.GetsReadOnly(spec, Context.Filters.Sort<DocField, string>(d => d.DocFieldName));
        }

        /// <summary>
        /// Lấy ra tất cả lĩnh vực có phân trang và xắp xếp.
        /// </summary>
        /// <param name="totalRecords"> Tổng số bản ghi </param>
        /// <param name="currentPage"> Trang hiện tại </param>
        /// <param name="pageSize"> Số bản ghi trên 1 trang </param>
        /// <param name="sortBy"> Sắp xếp theo </param>
        /// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="categoryBusinessId"></param>
        /// <param name="name"></param>
        /// <returns>Danh sách lĩnh vực</returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
            Expression<Func<DocField, T>> projector,
            int categoryBusinessId, int currentPage = 1,
            int? pageSize = null, string sortBy = "",
            bool isDescending = false, string name = null)
        {
            var spec = DocFieldQuery.WithCateogryBusinessId(categoryBusinessId);
            if (!string.IsNullOrEmpty(name))
            {
                spec = spec.And(p => p.DocFieldName.Contains(name));
            }
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _docFieldRepository.Count(spec);
            if (string.IsNullOrEmpty(sortBy)) sortBy = string.Empty;
            var sort = Context.Filters.CreateSort<DocField>(isDescending, sortBy);
            return _docFieldRepository.GetsAs(projector, spec, sort, Context.Filters.Page<DocField>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra tất cả các lĩnh vực. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocField, TOutput>> projector, Expression<Func<DocField, bool>> spec = null)
        {
            return _docFieldRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra một lĩnh vực
        /// </summary>
        /// <param name="docFieldId">Id của lĩnh vực</param>
        /// <returns>Entity lĩnh vực</returns>
        public DocField Get(int docFieldId)
        {
            DocField docField = null;
            if (docFieldId > 0)
            {
                docField = _docFieldRepository.Get(docFieldId);
            }
            return docField;
        }

        /// <summary>
        /// Trả về DocFieldName theo Id
        /// </summary>
        /// <param name="docFieldId"></param>
        /// <returns></returns>
        public string GetDocFieldName(int? docFieldId)
        {
            if (docFieldId.HasValue)
            {
                var docField = _docFieldRepository.Get(docFieldId);
                if (docField != null)
                {
                    return docField.DocFieldName;
                }
            }
            return "";
        }


        /// <summary>
        /// Lấy ra một lĩnh vực
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="docFieldId">Id của lĩnh vực</param>
        /// <returns>Entity lĩnh vực</returns>
        public T GetAs<T>(Expression<Func<DocField, T>> projector, int docFieldId)
        {
            return _docFieldRepository.GetAs(projector, p => p.DocFieldId == docFieldId);
        }

        /// <summary>
        /// Thêm mới lĩnh vực
        /// </summary>
        /// <param name="docField">Thực thể lĩnh vực</param>
        /// <returns></returns>
        public void Create(DocField docField)
        {
            if (docField == null)
            {
                throw new ArgumentNullException("docField");
            }
            if (_docFieldRepository.Exist(DocFieldQuery.WithDocFieldName(docField.DocFieldName)))
            {
                throw new EgovException(string.Format("Lĩnh vực ({0}) đã tồn tại!", docField.DocFieldName));
            }
            _docFieldRepository.Create(docField);
            Context.SaveChanges();
        }

        /// <summary>
        ///  Thêm mới lĩnh vực
        /// </summary>
        /// <param name="docFields">Danh sách lĩnh vực</param>
        /// <param name="ignoreExist">True: Bỏ qua nếu lĩnh vực đã tồn tại; False: Validate nếu đã tồn tại lĩnh vực</param>
        public void Create(IEnumerable<DocField> docFields, bool ignoreExist)
        {
            if (docFields == null || !docFields.Any())
            {
                throw new ArgumentNullException("docFields");
            }

            var names = docFields.Select(x => x.DocFieldName);
            var exist = _docFieldRepository.GetsAs(p => p.DocFieldName, p => names.Contains(p.DocFieldName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == docFields.Count())
                {
                    throw new EgovException(_resourceService.GetResource("DocField.Create.Exist"));
                }

                var list = docFields.Where(p => !exist.Contains(p.DocFieldName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("DocField.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(docFields);
            }
        }

        private void Create(IEnumerable<DocField> docFields)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var docField in docFields)
            {
                _docFieldRepository.Create(docField);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin lĩnh vực
        /// </summary>
        /// <param name="docField">Entity lĩnh vực</param>
        /// <param name="oldDocFieldName">Tên lĩnh vực trước khi cập nhật</param>
        /// <param name="docTypeIds"></param>
        public void Update(DocField docField, string oldDocFieldName, IEnumerable<Guid> docTypeIds = null)
        {
            if (docField == null)
            {
                throw new ArgumentNullException("docField");
            }
            if (_docFieldRepository.Exist(DocFieldQuery.WithDocFieldName(docField.DocFieldName).And(r => r.DocFieldName.ToLower() != oldDocFieldName.ToLower())))
            {
                throw new EgovException(string.Format("Lĩnh vực ({0}) đã tồn tại!", docField.DocFieldName));
            }


            #region docType

            if (docTypeIds != null && docTypeIds.Any())
            {
                var exists = _doctypeservice.Gets(p => p.DocFieldId == docField.DocFieldId, false);
                if (exists != null && exists.Any())
                {
                    var existIds = exists.Select(p => p.DocTypeId);
                    var removeDocTypeIds = existIds.Where(p => !docTypeIds.Contains(p));
                    if (removeDocTypeIds != null && removeDocTypeIds.Any())
                    {
                        var removeForms = _doctypeservice.Gets(p => removeDocTypeIds.Contains(p.DocTypeId), false);
                        if (removeForms != null && removeForms.Any())
                        {
                            foreach (var item in removeForms)
                            {
                                item.DocFieldId = null;
                            }
                        }
                    }

                    var addDocTypeIds = docTypeIds.Where(p => !existIds.Contains(p));
                    if (addDocTypeIds != null && addDocTypeIds.Any())
                    {
                        var addForms = _doctypeservice.Gets(p => addDocTypeIds.Contains(p.DocTypeId), false);
                        if (addForms != null && addForms.Any())
                        {
                            foreach (var item in addForms)
                            {
                                item.DocFieldId = docField.DocFieldId;
                            }
                        }
                    }
                }
                else
                {
                    var forms = _doctypeservice.Gets(p => docTypeIds.Contains(p.DocTypeId), false);
                    if (forms != null && forms.Any())
                    {
                        foreach (var item in forms)
                        {
                            item.DocFieldId = docField.DocFieldId;
                        }
                    }
                }
            }

            #endregion

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 lĩnh vực
        /// </summary>
        /// <param name="docField">Thực thể lĩnh vực</param>
        public void Delete(DocField docField)
        {
            if (docField == null)
                throw new ArgumentNullException("docField");

            _doctypeservice.Delete(docField.DocFieldId);

            var ddws = _docfieldDoctypeWorkflowRepository.Gets(false, i => i.DocFieldId.HasValue && i.DocFieldId.Value == docField.DocFieldId);
            if (ddws.Any())
            {
                foreach (var ddw in ddws)
                {
                    _docfieldDoctypeWorkflowRepository.Delete(ddw);
                }
            }

            _docFieldRepository.Delete(docField);

            Context.SaveChanges();
        }

        /// <summary>
        /// List danh sách lĩnh vực theo categoryBusinessId(Nghiệp vụ)
        /// </summary>
        /// <param name="categoryBusinessId">Id Nghiệp vụ</param>
        /// <returns>Trả về list lĩnh vực</returns>
        public IEnumerable<DocField> GetDocFields(int categoryBusinessId)
        {
            return _docFieldRepository.Gets(true, DocFieldQuery.WithCateogryBusinessId(categoryBusinessId));
        }

        /// <summary>
        /// Lấy các quy trinh theo loại văn bản
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="projector">Dữ liệu lmap lấy ra</param>
        /// <param name="spec">Bộ lọc</param>
        /// <param name="includeDocField"> True : Join với  bảng DocField</param>
        /// <returns></returns>
        public IEnumerable<T> GetWorkFlows<T>(Expression<Func<DocfieldDoctypeWorkflow, T>> projector,
            Expression<Func<DocfieldDoctypeWorkflow, bool>> spec = null, bool includeDocField = true)
        {
            Func<IQueryable<DocfieldDoctypeWorkflow>, IQueryable<DocfieldDoctypeWorkflow>> preFilter = null;
            if (includeDocField)
            {
                preFilter = Context.Filters.Include<DocfieldDoctypeWorkflow>("DocField");
            }

            return _docfieldDoctypeWorkflowRepository.GetsAs(projector, spec, preFilter);
        }

        /// <summary>
        /// Cập nhật quy trình cho loại văn bản 
        /// </summary>
        /// <param name="docFieldId">Id linh vuc</param>
        /// <param name="workflowIds">Danh sách id quy trình</param>
        public void UpdateWorkflows(int docFieldId, List<int> workflowIds)
        {
            if (workflowIds == null || !workflowIds.Any())
            {
                throw new EgovException(_resourceService.GetResource("DocField.CreateOrEdit.Fields.UpdateWorkflows.NotChoosseWorkflow"));
            }

            var docField = Get(docFieldId);
            if (docField == null)
            {
                throw new EgovException(_resourceService.GetResource("DocField.CreateOrEdit.Fields.NotExist"));
            }

            var exist = GetWorkFlows(p => p.WorkflowId, p => p.DocFieldId == docFieldId, false);
            var notExist = workflowIds.Where(p => !exist.Contains(p));
            if (notExist != null && notExist.Any())
            {
                foreach (var workflowId in notExist)
                {
                    _docfieldDoctypeWorkflowRepository.Create(new DocfieldDoctypeWorkflow
                    {
                        WorkflowId = workflowId,
                        DocFieldId = docFieldId,
                        IsActivated = false
                    });
                }

                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Loại bỏ quy trình khỏi loại văn bản
        /// </summary>
        /// <param name="docFieldId">Id linh vuc</param>
        /// <param name="workflowIds">Danh sach id quy trình</param>
        public void DeleteWorkflows(int docFieldId, List<int> workflowIds)
        {
            if (workflowIds == null || !workflowIds.Any())
            {
                throw new EgovException(_resourceService.GetResource("DocField.CreateOrEdit.Fields.UpdateWorkflows.NotChoosseWorkflow"));
            }

            var docField = Get(docFieldId);
            if (docField == null)
            {
                throw new EgovException(_resourceService.GetResource("DocField.CreateOrEdit.Fields.NotExist"));
            }

            var exist = GetWorkFlows(p => p, p => docFieldId == p.DocFieldId && workflowIds.Contains(p.WorkflowId), false);
            if (exist != null && exist.Any())
            {
                foreach (var item in exist)
                {
                    _docfieldDoctypeWorkflowRepository.Delete(item);
                }
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Cập nhật trạng thái của quy trình 
        /// Note: Trên 1 loại văn bản chỉ có 1 quy trình được hoạt động
        /// </summary>
        /// <param name="docFieldId">Id linh vuc</param>
        /// <param name="workflowId">Id quy trình</param>
        /// <param name="isActivated">Trạng thái</param>
        public void ChangeActivatedWorkflows(int docFieldId, int workflowId, bool isActivated)
        {
            var current = _docfieldDoctypeWorkflowRepository.Get(false, p => docFieldId == p.DocFieldId && p.WorkflowId == workflowId);
            if (current == null)
            {
                throw new EgovException(_resourceService.GetResource("DocField.CreateOrEdit.Fields.DocFieldWorkflows.NotExistWorkflow"));
            }

            if (isActivated)
            {
                var acticvateds = _docfieldDoctypeWorkflowRepository.Gets(false, p => p.IsActivated && p.DocFieldId == docFieldId);
                if (acticvateds != null && acticvateds.Any())
                {
                    foreach (var item in acticvateds)
                    {
                        item.IsActivated = false;
                    }
                }
            }

            current.IsActivated = isActivated;
            Context.SaveChanges();
        }

        /// <summary>
        ///  Trả dữ liệu thô
        /// </summary>
        public IQueryable<DocField> Raw
        {
            get
            {
                return _docFieldRepository.Raw;
            }
        }

        /// <summary>
        /// Trả dữ liệu thô
        /// </summary>
        public IQueryable<DocfieldDoctypeWorkflow> RawDocfieldDoctypeWorkflow
        {
            get
            {
                return _docfieldDoctypeWorkflowRepository.Raw;
            }
        }
    }
}
