using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : WorkflowBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 241012</para>
    /// <para>Author      : GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng Workflow trong CSDL</para>
    /// </summary>
    public class WorkflowBll : ServiceBase
    {
        private readonly IRepository<Workflow> _workflowRepository;
        private readonly IRepository<DocfieldDoctypeWorkflow> _docfieldDoctypeWorkflowRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo class <see cref="WorkflowBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Thiết lập chung</param>
        /// <param name="resourceService">Resource Service</param>
        /// <param name="cache">Memory Cache</param>
        public WorkflowBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            ResourceBll resourceService,
            MemoryCacheManager cache)
            : base(context)
        {
            _workflowRepository = Context.GetRepository<Workflow>();
            _generalSettings = generalSettings;
            _docfieldDoctypeWorkflowRepository = Context.GetRepository<DocfieldDoctypeWorkflow>();
            _resourceService = resourceService;
            _cache = cache;
        }

        /// <summary>
        /// Lấy ra tất cả luồng văn bản(hồ sơ). Kết quả chỉ đọc
        /// </summary>
        /// <param name="isActivated">Trạng thái kích hoạt</param>
        /// <param name="isIncludeDocType">Tải thêm cả DocType</param>
        /// <returns>Danh sách luồng văn bản(hồ sơ)</returns>
        public IEnumerable<Workflow> Gets(bool isIncludeDocType, bool? isActivated = null)
        {
            return isIncludeDocType
                ? _workflowRepository.GetsReadOnly(WorkflowQuery.WithIsActivated(isActivated),
                    Context.Filters.Include<Workflow>("DocType"))
                : _workflowRepository.GetsReadOnly(WorkflowQuery.WithIsActivated(isActivated));
        }

        /// <summary>
        /// Trả về tất cả luồng xử lý từ cache
        /// </summary>
        /// <returns>Danh sách luồng văn bản(hồ sơ)</returns>
        public IEnumerable<Workflow> GetsFromCache()
        {
            var result = _cache.Get<IEnumerable<WorkflowCached>>(CacheParam.WorkflowAllKey, CacheParam.WorkflowAllCacheTimeOut, () =>
            {
                var data = _workflowRepository.GetsReadOnly();
                var cacheValue = AutoMapper.Mapper.Map<IEnumerable<Workflow>, IEnumerable<WorkflowCached>>(data);
                return cacheValue;
            });

            return AutoMapper.Mapper.Map<IEnumerable<WorkflowCached>, IEnumerable<Workflow>>(result);
        }

        /// <summary>
        /// Lấy ra một luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflowId">Id của luồng văn bản(hồ sơ)</param>
        /// <returns>Entity luồng văn bản(hồ sơ)</returns>
        public Workflow GetFromCache(int workflowId)
        {
            Workflow workflow = null;
            if (workflowId > 0)
            {
                workflow = GetsFromCache().SingleOrDefault(w => w.WorkflowId == workflowId);
            }
            return workflow;
        }

        /// <summary>
        /// Lất ra tất cả luồng văn bản theo điều kiện.
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<Workflow> Gets(Expression<Func<Workflow, bool>> spec = null)
        {
            return _workflowRepository.Gets(false, spec);
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyển vào hay không
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<Workflow, bool>> spec = null)
        {
            return _workflowRepository.Exist(spec);
        }

        /// <summary>
        /// Lấy ra tất cả các luồng văn bản(hồ sơ). Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Workflow, TOutput>> projector,
            Expression<Func<Workflow, bool>> spec = null)
        {
            return _workflowRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Note: Hàm này chi dùng trong phần Admin để tim kiếm
        /// Lấy danh sách các quy trình
        /// </summary>
        /// <typeparam name="T">Dối tượng lấy ra</typeparam>
        /// <param name="totalRecords">Tông số quy trình trong hệ thông</param>
        /// <param name="projector">Dữ liệu lây ra</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng phần tử lấy ra</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp tăng hay giảm</param>
        /// <param name="isActivated">Trạng thái kích hoạt</param>
        /// <param name="workflowName">Tên quy trình</param>
        /// <param name="docFieldId"> id lĩnh vực</param>
        /// <param name="docTypeId"> id loại văn bản</param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
            Expression<Func<Workflow, T>> projector,
            int currentPage = 1,
            int? pageSize = null,
            string sortBy = null,
            bool isDescending = false,
            bool? isActivated = null,
            string workflowName = "",
            int? docFieldId = null,
            Guid? docTypeId = null)
        {
            Expression<Func<Workflow, bool>> spec = p =>
                                    (string.IsNullOrEmpty(workflowName)
                                    || (!string.IsNullOrEmpty(workflowName) && p.WorkflowName.Contains(workflowName)))
                                    && (!isActivated.HasValue || (isActivated.HasValue && p.IsActivated == isActivated.Value));

            var workflowIds = new List<int>();

            if (docTypeId.HasValue && docTypeId.Value != Guid.Empty)
            {
                workflowIds = _docfieldDoctypeWorkflowRepository.GetsAs(p => p.WorkflowId, p => p.DocTypeId == docTypeId.Value).ToList();

                spec = spec.And(p => workflowIds.Contains(p.WorkflowId));
            }
            else
            {
                if (docFieldId.HasValue)
                {
                    workflowIds = _docfieldDoctypeWorkflowRepository.GetsAs(p => p.WorkflowId,
                        p => p.DocFieldId == docFieldId.Value).ToList();

                    spec = spec.And(p => workflowIds.Contains(p.WorkflowId));
                }
            }

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _workflowRepository.Count(spec);

            var param = string.IsNullOrEmpty(sortBy) ? new[] { "CreatedOnDate", "IsActivated", "WorkflowName" } : new[] { sortBy, "CreatedOnDate", "IsActivated" };
            var sort = Context.Filters.CreateSort<Workflow>(isDescending, param);
            return _workflowRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Workflow>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra một luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflowId">Id của luồng văn bản(hồ sơ)</param>
        /// <returns>Entity luồng văn bản(hồ sơ)</returns>
        public Workflow Get(int workflowId)
        {
            Workflow workflow = null;
            if (workflowId > 0)
            {
                workflow = _workflowRepository.Get(workflowId);
            }
            return workflow;
        }

        /// <summary>
        /// Thêm mới luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflow">Thực thể luồng văn bản(hồ sơ)</param>
        /// <returns></returns>
        public void Create(Workflow workflow)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            _workflowRepository.Create(workflow);
            Context.SaveChanges();

            ResetCache();
        }

        /// <summary>
        /// Thêm mới luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflows">Thực thể luồng văn bản(hồ sơ)</param>
        /// <returns></returns>
        public void Create(IEnumerable<Workflow> workflows)
        {
            if (workflows == null || !workflows.Any())
            {
                throw new ArgumentNullException("workflow");
            }

            foreach (var workflow in workflows)
            {
                _workflowRepository.Create(workflow);
            }

            Context.SaveChanges();

            ResetCache();
        }

        /// <summary>
        /// Cập nhật thông tin luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflow">Entity luồng văn bản(hồ sơ)</param>
        public void Update(Workflow workflow)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            Context.SaveChanges();

            ResetCache();
        }

        /// <summary>
        /// Cập nhật thông tin luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflows"> List Entity luồng văn bản(hồ sơ)</param>
        public void Update(IEnumerable<Workflow> workflows)
        {
            if (workflows == null || !workflows.Any())
            {
                throw new ArgumentNullException("workflow");
            }

            Context.SaveChanges();

            ResetCache();
        }

        /// <summary>
        /// Xóa 1 luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflow">Thực thể luồng văn bản(hồ sơ)</param>
        public void Delete(Workflow workflow)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }
            _workflowRepository.Delete(workflow);
            Context.SaveChanges();

            ResetCache();
        }

        /// <summary>
        /// TODO: Lưu ý hàm này chỉ dùng để cập nhật lại AddStr về string.empty để tương thích với Address theo dạng json mới (Chỉ sử dụng để update lại dữ liệu cũ, nghiêm cấm sử dụng ở bất kỳ đâu)
        /// </summary>
        public void UpdateAllAddStr()
        {
            var workflows = Gets();
            foreach (var workflow in workflows)
            {
                var path = workflow.JsonInObject;
                if (path != null)
                {
                    if (path.Nodes != null)
                    {
                        foreach (var node in path.Nodes)
                        {
                            if (node.Address != null)
                            {
                                foreach (var address in node.Address)
                                {
                                    var a = address.LevelQueries;
                                    var b = address.UserQueries;
                                    var c = address.PositionQueries;
                                    var d = address.RelationQueries;
                                    address.AddStr = string.Empty;
                                }
                            }
                        }
                    }
                }
                workflow.Json = path.Stringify();
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Update id workflow trong chuỗi json trùng với Id ngoài.
        /// Kết quả 
        /// </summary>
        public void UpdateWorkflowId()
        {
            var workflows = Gets();
            foreach (var workflow in workflows)
            {
                if (string.IsNullOrEmpty(workflow.Json))
                {
                    continue;
                }

                try
                {
                    var path = Json2.ParseAs<Path>(workflow.Json);
                    path.Id = workflow.WorkflowId;

                    workflow.Json = Json2.Stringify(path);
                }
                catch
                {
                    continue;
                }
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Update id workflow trong chuỗi json trùng với Id ngoài.
        /// Kết quả 
        /// </summary>
        public void FixSyncError()
        {
            var workflows = Gets();
            foreach (var workflow in workflows)
            {
                if (string.IsNullOrEmpty(workflow.Json))
                {
                    continue;
                }

                try
                {
                    var path = Json2.ParseAs<Path>(workflow.Json);
                    path.Id = workflow.WorkflowId;
                    foreach (var node in path.Nodes)
                    {
                        var top = node.Top > 10000 ? Math.Round(node.Top / 1000000000000, 1) : node.Top;
                        var left = node.Left > 10000 ? Math.Round(node.Left / 1000000000000, 1) : node.Left;
                        node.Top = top;
                        node.Left = left;
                        node.Right = left + 160;
                        node.Bottom = top + 70;
                    }

                    workflow.Json = Json2.Stringify(path);
                }
                catch
                {
                    continue;
                }
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra xem quy trình đã được sử dụng không
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public bool HasUseWorkflow(int workflowId)
        {
            return _docfieldDoctypeWorkflowRepository.Exist(p => p.WorkflowId == workflowId);
        }

        /// <summary>
        /// Thêm mới bảng map giữ doctype, docfield, workflow
        /// </summary>
        /// <param name="docfieldDoctypeWorkflows"></param>
        public void CreateDocFieldDocTypeWorkflow(IEnumerable<DocfieldDoctypeWorkflow> docfieldDoctypeWorkflows)
        {
            if (docfieldDoctypeWorkflows == null || !docfieldDoctypeWorkflows.Any())
            {
                throw new ArgumentNullException("docfieldDoctypeWorkflows is null.");
            }

            foreach (var item in docfieldDoctypeWorkflows)
            {
                _docfieldDoctypeWorkflowRepository.Create(item);
            }
            Context.SaveChanges();

            _cache.Remove(CacheParam.DocTypeAllKey);
        }

        /// <summary>
        /// Lấy danh sách cấu hình giữ quy trinh , lĩnh vực, loại văn bản
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IEnumerable<DocfieldDoctypeWorkflow> GetDocFieldDocTypeWorkflows(Expression<Func<DocfieldDoctypeWorkflow, bool>> spec = null, bool isReadOnly = false)
        {
            return _docfieldDoctypeWorkflowRepository.Gets(isReadOnly, spec);
        }

        /// <summary>
        /// Cập nhật cấu hình giữ quy trinh , lĩnh vực, loại văn bản
        /// </summary>
        /// <param name="docfieldDoctypeWorkflows"></param>
        public void UpdateDocFieldDocTypeWorkflow(IEnumerable<DocfieldDoctypeWorkflow> docfieldDoctypeWorkflows)
        {
            if (docfieldDoctypeWorkflows == null || !docfieldDoctypeWorkflows.Any())
            {
                throw new ArgumentNullException("docfieldDoctypeWorkflows is null.");
            }

            Context.SaveChanges();

            _cache.Remove(CacheParam.DocTypeAllKey);
        }

        /// <summary>
        /// Lấy danh sách quy trình theo loại văn bản
        /// </summary>
        /// <param name="docTypeId">Id của lĩnh vực</param>
        /// <param name="isActivated">Trạng thái kích hoạt</param>
        /// <returns></returns>
        public IEnumerable<Workflow> GetWorkflows(Guid docTypeId, bool? isActivated = null)
        {
            var results = _docfieldDoctypeWorkflowRepository.Raw.Join(
              _workflowRepository.Raw,
               p => p.WorkflowId,
               x => x.WorkflowId,
               (p, x) => new { Workflow = x, DocTypeId = p.DocTypeId, IsActivated = p.IsActivated })
               .Where(p => p.DocTypeId == docTypeId
                   && (!isActivated.HasValue || (isActivated.HasValue && p.IsActivated == isActivated.Value)))
               .Select(p => p.Workflow);
            return results;
        }

        /// <summary>
        /// Trả về dữ liệu thô của bảng quy trình => join lấy dữ liệu 
        /// </summary>
        public IQueryable<Workflow> Raw
        {
            get
            {
                return _workflowRepository.Raw;
            }
        }

        /// <summary>
        /// Trả về dữ liệu thô của bảng  map DocfieldDoctypeWorkflow => join lấy dữ liệu 
        /// </summary>
        public IQueryable<DocfieldDoctypeWorkflow> RawDocfieldDoctypeWorkflow
        {
            get
            {
                return _docfieldDoctypeWorkflowRepository.Raw;
            }
        }

        private void ResetCache()
        {
            _cache.Remove(CacheParam.WorkflowAllKey);
            GetsFromCache();
        }
    }
}
