using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	///<para> Bkav Corp. - BSO - eGov - eOffice team</para>
	///<para>Project: eGov Cloud v1.0</para>
	///<para>Class : DocTypeBll - public - BLL</para>
	///<para>Access Modifiers: </para>
	///<para>Create Date : 240912</para>
	///<para> Author      : GiangPN</para>
	///<para> Description : BLL tương ứng với bảng DocType trong CSDL</para>
	/// </summary>
	public class DocTypeBll : ServiceBase
	{
		private readonly AdminGeneralSettings _generalSettings;

		private readonly IRepository<DocType> _docTypeRepository;
		private readonly IRepository<DocTypeStore> _docTypeStoreRepository;
		private readonly IRepository<DocTypeLaw> _docTypeLawRepository;
		private readonly IRepository<DocField> _docFieldRepository;
		private readonly IRepository<DoctypePaper> _doctypePaperRepository;
		private readonly IRepository<DoctypeFee> _doctypeFeeRepository;
		private readonly IRepository<DocfieldDoctypeWorkflow> _docfieldDoctypeWorkflowRepository;

		private readonly WorkflowBll _workflowService;
		private readonly ResourceBll _resourceService;
		private readonly FeeBll _feeService;
		private readonly PaperBll _paperService;
		private readonly BusinessLicenseBll _businessLicenseService;
		private readonly DocTypeFormBll _docTypeFormService;
		private readonly StoreBll _storeService;
		private readonly CodeBll _codeService;

		private readonly WorkflowHelper _workflowHelper;
		private readonly MemoryCacheManager _cache;


		/// <summary>
		/// Khởi tạo class <see cref="DocTypeBll"/>.
		/// </summary>
		/// <param name="context">Dal tương ứng với bảng DocType trong CSDL</param>
		/// <param name="generalSettings">Cấu hình chung</param>
		/// <param name="workflowService">Bll tương ứng với bảng WorkFlow trong CSDL</param>
		/// <param name="resourceService">Bll tương ứng với bảng Resource</param>
		/// <param name="workflowHelper"> </param>
		/// <param name="feeService">Bll tương ứng với bảng Fee</param>
		/// <param name="paperService">Bll tương ứng với bảng Paper</param>
		/// <param name="businessLicenseService">Bll tương ứng với bảng BusinessLicense</param>
		/// <param name="docTypeFormService"> bll tương ứng với abngr doctypeForm</param>
		/// <param name="storeService">Bll bangr store</param>
		/// <param name="cache">Memory Cache</param>
		/// <param name="codeService"></param>
		public DocTypeBll(
			IDbCustomerContext context,
			AdminGeneralSettings generalSettings,
			WorkflowBll workflowService,
			ResourceBll resourceService,
			WorkflowHelper workflowHelper,
			FeeBll feeService,
			PaperBll paperService,
			BusinessLicenseBll businessLicenseService,
			DocTypeFormBll docTypeFormService,
			StoreBll storeService, CodeBll codeService,
			MemoryCacheManager cache)
			: base(context)
		{
			_docTypeRepository = Context.GetRepository<DocType>();
			_generalSettings = generalSettings;
			_docTypeStoreRepository = Context.GetRepository<DocTypeStore>();
			_workflowService = workflowService;
			_resourceService = resourceService;
			_workflowHelper = workflowHelper;
			_feeService = feeService;
			_paperService = paperService;
			_businessLicenseService = businessLicenseService;
			_docTypeFormService = docTypeFormService;
			_docTypeLawRepository = Context.GetRepository<DocTypeLaw>();
			_docfieldDoctypeWorkflowRepository = Context.GetRepository<DocfieldDoctypeWorkflow>();
			_docFieldRepository = Context.GetRepository<DocField>();
			_doctypePaperRepository = Context.GetRepository<DoctypePaper>();
			_doctypeFeeRepository = Context.GetRepository<DoctypeFee>();
			_storeService = storeService;
			_cache = cache;
			_codeService = codeService;
		}

		/// <summary>
		/// Test
		/// </summary>
		/// <returns></returns>
		public IRepository<DocType> GetRepository()
		{
			return _docTypeRepository;
		}

		/// <summary>
		/// Lấy ra tất cả các loại hồ sơ có phân trang. Kết quả chỉ để đọc
		/// </summary>
		/// <param name="totalRecords">Tổng số bản ghi</param>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="currentPage">Trang hiện tại</param>
		/// <param name="pageSize">Số bản ghi trên 1 trang</param>
		/// <param name="sortBy">Sắp xếp theo</param>
		/// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
		/// <param name="actionLevel">Id thể loại văn bản (dùng để tìm kiếm) Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các loại hồ sơ thuộc thể loại văn bản truyền vào</param>
		/// <param name="docFieldId">Id lĩnh vực (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua đk tìm kiếm này, nếu có giá trị sẽ tìm tất cả các loại hồ sơ thuộc lĩnh vực truyền vào</param>
		/// <param name="isActivated">Trạng thái kích hoạt(khi tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm, mặc định lấy tất cả các loại hồ sơ kích hoạt và không kích hoạt</param>
		/// <param name="docTypeName"></param>
		/// <returns>Danh sách loại hồ sơ</returns>
		public IEnumerable<T> GetsAs<T>(out int totalRecords,
											Expression<Func<DocType, T>> projector,
											int currentPage = 1,
											int? pageSize = null,
											string sortBy = null,
											bool isDescending = false,
											int? actionLevel = null,
											int? docFieldId = null,
											bool? isActivated = null,
											string docTypeName = "",
                                            string docTypeCode = "",
                                            int? categoryBusinessId = null)
		{
			var spec =
				DocTypeQuery.WithActionLevel(actionLevel)
                .And(DocTypeQuery.WithDocFieldId(docFieldId))
                .And(DocTypeQuery.WithIsActivated(isActivated))
                .And(DocTypeQuery.ContainsDocTypeName(docTypeName))
                .And(DocTypeQuery.ContainsDocTypeCode(docTypeCode))
                .And(DocTypeQuery.WithCategoryBusinessId(categoryBusinessId));
			if (!pageSize.HasValue)
			{
				pageSize = _generalSettings.DefaultPageSize;
			}
			totalRecords = _docTypeRepository.Count(spec);

			var param = string.IsNullOrEmpty(sortBy) ? new[] { "CreatedOnDate", "IsActivated", "DocTypeName" } : new[] { sortBy, "CreatedOnDate", "IsActivated" };
			var sort = Context.Filters.CreateSort<DocType>(isDescending, param);
			return _docTypeRepository.GetsAs(projector, spec, sort, Context.Filters.Page<DocType>(currentPage, pageSize.Value));
		}

		/// <summary> Tienbv 011112
		/// Lấy ra tất cả doctype theo điều kiện truyền vào. Kết quả chỉ đọc
		/// </summary>
		/// <param name="spec">The spec.</param>
		/// <param name="isReadOnly"></param>
		/// <returns></returns>
		public IEnumerable<DocType> Gets(Expression<Func<DocType, bool>> spec = null, bool isReadOnly = true)
		{
			return _docTypeRepository.Gets(isReadOnly, spec);
		}


        /// <summary>
        /// Trả về danh sách doctype theo docfield
        /// </summary>
        /// <param name="docfieldId"></param>
        /// <param name="isReadonly"></param>
        /// <returns></returns>
        public IEnumerable<DocType> Gets(int docfieldId, bool isReadonly = true)
		{
			if (docfieldId <= 0)
			{
				return new List<DocType>();
			}

			return _docTypeRepository.Gets(isReadonly, dt => dt.DocFieldId.HasValue && dt.DocFieldId.Value == docfieldId);
		}

		/// <summary>
		/// Lấy ra tất cả doctype theo điều kiện truyền vào. Kết quả chỉ đọc
		/// </summary>
		/// <param name="spec">The spec.</param>
		/// <param name="isReadOnly"></param>
		/// <returns></returns>
		public IEnumerable<DocType> GetsWithoutLazyLoading(Expression<Func<DocType, bool>> spec = null, bool isReadOnly = true)
		{
			Context.Configuration.LazyLoadingEnabled = false;
			var result = _docTypeRepository.Gets(isReadOnly, spec);
			Context.Configuration.LazyLoadingEnabled = true;
			return result;
		}

		/// <summary>
		/// Trả về tất cả loại hồ sơ văn bản từ cache
		/// </summary>
		/// <returns>Danh sách các loại hồ sơ, văn bản</returns>
		/// <remarks>
		/// Chỉ trả về thông tin cơ bản của Loại hồ sơ, những trường thông tin lớn như Nội dung hồ sơ không lưu.
		/// </remarks>
		public IEnumerable<DocTypeCached> GetAllFromCache()
		{
			return _cache.Get<IEnumerable<DocTypeCached>>(CacheParam.DocTypeAllKey, CacheParam.DocTypeAllCacheTimeOut, () =>
			{
				var doctypes = Gets(spec: dt => dt.IsActivated, isReadOnly: true);
				var doctypeIds = doctypes.Select(dt => dt.DocTypeId);
				var docfieldIds = doctypes.Where(dt => dt.DocFieldId.HasValue).Select(dt => dt.DocFieldId.Value).Distinct();

				var ddWorkflows = _docfieldDoctypeWorkflowRepository.GetsReadOnly(i => i.IsActivated);

				var docfields = _docFieldRepository.GetsReadOnly(dt => docfieldIds.Contains(dt.DocFieldId));

				var doctypeStores = _docTypeStoreRepository.Gets(true);

				// TienBV: không lưu tất cả dữ liệu của Doctype vào cache,
				// Nguyên nhân: do bảng doctype có nhiều trường text, nếu lưu hết vào cache sẽ chiếm dụng rất nhiều Ram
				return doctypes.Select(dt =>
				{
					var workflows = ddWorkflows.Where(i => (i.DocTypeId.HasValue && i.DocTypeId.Value.Equals(dt.DocTypeId)));
					if (!workflows.Any())
					{
						workflows = ddWorkflows.Where(i => !i.DocTypeId.HasValue && i.DocFieldId.HasValue && dt.DocFieldId.HasValue && i.DocFieldId.Value.Equals(dt.DocFieldId.Value));
					}

					var docfield = docfields.SingleOrDefault(df => df.DocFieldId == dt.DocFieldId);

					var docfieldName = docfield != null
											? docfield.DocFieldName
											: EnumHelper<CategoryBusinessTypes>.GetDescription(dt.CategoryBusinessIdInEnum);

					var storeIds = doctypeStores.Where(ds => ds.DocTypeId == dt.DocTypeId).Select(ds => ds.StoreId).ToList();
					var defaultStore = doctypeStores.FirstOrDefault(ds => ds.IsDefault);
					var stores = _storeService.GetsFromCache().Where(s => storeIds.Contains(s.StoreId))
										.OrderBy(s => defaultStore == null? true : s.StoreId == defaultStore.StoreId).ToList();

					return new DocTypeCached()
					{
						DocTypeId = dt.DocTypeId,
						DocTypeName = dt.DocTypeName,
                        DocTypeCode = dt.DocTypeCode,
                        CategoryBusinessId = dt.CategoryBusinessId,
						CategoryId = dt.CategoryId,
						CompendiumDefault = dt.CompendiumDefault,
						DocFieldId = dt.DocFieldId,
						DocFieldName = docfieldName,
						DocTypePermission = dt.DocTypePermission,
						IsActivated = dt.IsActivated,
						IsAllowOnline = dt.IsAllowOnline,
						LevelId = dt.LevelId,
						OfficeId = dt.OfficeId,
						Unsigned = dt.Unsigned,
						WorkflowId = (workflows.Any() ? workflows.First().WorkflowId : 0),
						ActionLevel = dt.ActionLevel,
						HasOverdueInNode = dt.HasOverdueInNode,
						StoreIds = storeIds,
						Stores = stores
					};
				}).ToList();
			});
		}

		/// <summary>
		/// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
		/// </summary>
		/// <param name="spec"></param>
		/// <returns></returns>
		public bool Exist(Expression<Func<DocType, bool>> spec)
		{
			return _docTypeRepository.Exist(spec);
		}

		/// <summary>
		/// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="spec">Điều kiện</param>
		/// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
		/// <returns>Danh sách các thực thể được ánh xạ</returns>
		public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocType, TOutput>> projector, Expression<Func<DocType, bool>> spec = null)
		{
			return _docTypeRepository.GetsAs(projector, spec);
		}

		/// <summary>
		/// Lấy ra một loại hồ sơ
		/// </summary>
		/// <param name="docTypeId">Id của loại hồ sơ</param>
		/// <returns>Entity loại hồ sơ</returns>
		public DocType Get(Guid? docTypeId)
		{
			return _docTypeRepository.Get(docTypeId);
		}
        /// <summary>
        /// Trả về loại văn bản được lưu trong cache
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <returns></returns>
        public DocTypeCached GetFromCache(Guid doctypeId)
		{
			return GetAllFromCache().SingleOrDefault(dt => dt.DocTypeId == doctypeId);
		}

		/// <summary>
		/// Lấy ra một loại hồ sơ
		/// </summary>
		/// <param name="projector"></param>
		/// <param name="docTypeId">Id của loại văn bản</param>
		/// <returns>Entity lĩnh vực</returns>
		public T GetAs<T>(Expression<Func<DocType, T>> projector, Guid docTypeId)
		{
			return _docTypeRepository.GetAs(projector, p => p.DocTypeId == docTypeId);
		}

		/// <summary>
		/// Lấy ra một loại hồ sơ
		/// </summary>
		/// <param name="docTypeId">Id của loại hồ sơ</param>
		/// <param name="projector"></param>
		/// <returns>Entity loại hồ sơ</returns>
		public T Get<T>(Guid docTypeId, Expression<Func<DocType, T>> projector)
		{
			return _docTypeRepository.GetAs(projector, d => d.DocTypeId == docTypeId);
		}

		/// <summary>
		/// Lấy ra các loại hồ sơ(văn bản) được tạo mới.
		/// </summary>
		/// <param name="userId">Id của User đăng nhập</param>
		/// <param name="businessType">Lấy theo business type, để null nếu muốn lấy tất cả.</param>
		/// <returns>Danh sách công văn được tạo mới</returns>
		public IEnumerable<DocTypeCached> GetsByUserId(int userId, CategoryBusinessTypes? businessType = null)
		{
			var result = new List<DocTypeCached>();

			var cachedDoctypes = GetAllFromCache().Where(dt => dt.WorkflowId != 0);
			if (!cachedDoctypes.Any())
			{
				return result;
			}

			var workflowIds = cachedDoctypes.Select(dt => dt.WorkflowId).Distinct();
			var workflows = _workflowService.GetsFromCache().Where(w => workflowIds.Contains(w.WorkflowId)).ToList();

			foreach (var doctype in cachedDoctypes)
			{
				try
				{
					var workflow = workflows.SingleOrDefault(w => w.WorkflowId == doctype.WorkflowId);
					if (workflow == null)
					{
						continue;
					}

					var startNode = _workflowHelper.GetStartNodes(workflow, userId);
					if (startNode == null || !startNode.Any())
					{
						continue;
					}
					result.Add(doctype);
				}
				catch { continue; }
			}

			// Xử lý quyền khởi tạo theo loại văn bản
			if (businessType.HasValue)
			{
				result = result.Where(dt => dt.CategoryBusinessId == (int)businessType.Value).ToList();
			}

			return result.OrderBy(dt => dt.Order).ThenBy(dt => dt.DocTypeName);
		}

		/// <summary>
		/// Trả về quy trình đang active của loại văn bản.
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <returns></returns>
		public Workflow GetWorkflowActive(Guid doctypeId)
		{
			if (doctypeId.Equals(Guid.Empty))
			{
				return null;
			}

			var doctype = GetFromCache(doctypeId);
			if (doctype == null || doctype.WorkflowId < 1)
			{
				return null;
			}

			var result = _workflowService.GetFromCache(doctype.WorkflowId);
			return result;
		}

		/// <summary>
		/// Thêm mới loại hồ sơ
		/// </summary>
		/// <param name="docType">Thực thể loại hồ sơ</param>
		/// <returns></returns>
		public void Create(DocType docType)
		{
			if (docType == null)
			{
				throw new ArgumentNullException("docType");
			}
			if (_docTypeRepository.Exist(DocTypeQuery.WithDocTypeName(docType.DocTypeName)))
			{
				throw new EgovException(string.Format(_resourceService.GetResource("DocType.CreateOrEdit.Fields.DocTypeName.Exist"), docType.DocTypeName));
			}

			docType.DocTypeId = Guid.NewGuid();
			var storeIds = new List<int>();

			if (!string.IsNullOrEmpty(docType.StoreIds))
			{
				if (!docType.StoreIds.Trim().Contains(";"))
				{
					int outStoreIds;
					int.TryParse(docType.StoreIds.Trim(), out outStoreIds);
					storeIds.Add(outStoreIds);

					// docType.DocTypeStores.Add(new DocTypeStore { StoreId = outStoreIds });
				}
				else
				{
					var arrStoreId = docType.StoreIds.Split(';');
					foreach (var storeId in arrStoreId)
					{
						int outStoreId;
						int.TryParse(storeId, out outStoreId);
						storeIds.Add(outStoreId);
						// docType.DocTypeStores.Add(new DocTypeStore { StoreId = outStoreId });
					}
				}
			}

			_docTypeRepository.Create(docType);

			foreach (var storeId in storeIds)
			{
				_docTypeStoreRepository.Create(new DocTypeStore() { StoreId = storeId, DocTypeId = docType.DocTypeId });
			}

			Context.SaveChanges();
			ResetCache();
		}

        /// <summary>
        /// Thêm mới loại hồ sơ
        /// và trả về đối tượng sau khi đã được tạo
        /// </summary>
        /// <param name="docType">Thực thể loại hồ sơ</param>
        /// <returns></returns>
        public DocType CreateNReturn(DocType docType)
        {
            if (docType == null)
            {
                throw new ArgumentNullException("docType");
            }
            if (_docTypeRepository.Exist(DocTypeQuery.WithDocTypeName(docType.DocTypeName)))
            {
                throw new EgovException(string.Format(_resourceService.GetResource("DocType.CreateOrEdit.Fields.DocTypeName.Exist"), docType.DocTypeName));
            }

            docType.DocTypeId = Guid.NewGuid();
            var storeIds = new List<int>();

            if (!string.IsNullOrEmpty(docType.StoreIds))
            {
                if (!docType.StoreIds.Trim().Contains(";"))
                {
                    int outStoreIds;
                    int.TryParse(docType.StoreIds.Trim(), out outStoreIds);
                    storeIds.Add(outStoreIds);

                    // docType.DocTypeStores.Add(new DocTypeStore { StoreId = outStoreIds });
                }
                else
                {
                    var arrStoreId = docType.StoreIds.Split(';');
                    foreach (var storeId in arrStoreId)
                    {
                        int outStoreId;
                        int.TryParse(storeId, out outStoreId);
                        storeIds.Add(outStoreId);
                        // docType.DocTypeStores.Add(new DocTypeStore { StoreId = outStoreId });
                    }
                }
            }

            _docTypeRepository.Create(docType);

            foreach (var storeId in storeIds)
            {
                _docTypeStoreRepository.Create(new DocTypeStore() { StoreId = storeId, DocTypeId = docType.DocTypeId });
            }

            Context.SaveChanges();
            ResetCache();
            return docType;
        }

        /// <summary>
        /// Thêm mới loại hồ sơ/văn bản
        /// </summary>
        /// <param name="docTypes">Danh sách loại hồ sơ/văn bản</param>
        /// <param name="ignoreExist">True: bỏ qua nếu đã tồn tai, fasle: validate nếu đã tồn tại</param>
        public void Create(IEnumerable<DocType> docTypes, bool ignoreExist)
        {
            if (docTypes == null || !docTypes.Any())
            {
                throw new ArgumentNullException("docTypes");
            }

			var names = docTypes.Select(x => x.DocTypeName);
			var exist = _docTypeRepository.GetsAs(p => p.DocTypeName, p => names.Contains(p.DocTypeName));

			if (exist != null && exist.Any())
			{
				if (!ignoreExist || exist.Count() == docTypes.Count())
				{
					throw new EgovException(_resourceService.GetResource("DocType.Create.Exist"));
				}

				var list = docTypes.Where(p => !exist.Contains(p.DocTypeName));
				if (list == null || !list.Any())
				{
					throw new EgovException(_resourceService.GetResource("DocType.Create.Exist"));
				}
				Create(list);
			}
			else
			{
				Create(docTypes);
			}
			ResetCache();
		}

        /// <summary>
        /// Thêm mới loại hồ sơ/văn bản
        /// và trả về đối tượng sau khi đã được tạo
        /// </summary>
        /// <param name="docTypes">Danh sách loại hồ sơ/văn bản</param>
        /// <param name="ignoreExist">True: bỏ qua nếu đã tồn tai, fasle: validate nếu đã tồn tại</param>
        public List<DocType> CreateNReturn(IEnumerable<DocType> docTypes, bool ignoreExist)
        {
            if (docTypes == null || !docTypes.Any())
            {
                throw new ArgumentNullException("docTypes");
            }

            var names = docTypes.Select(x => x.DocTypeName);
            var codes = docTypes.Select(x => x.DocTypeCode);

            var existName = _docTypeRepository.GetsAs(p => p.DocTypeName, p => names.Contains(p.DocTypeName));
            var existCode = _docTypeRepository.GetsAs(p => p.DocTypeCode, p => !string.IsNullOrEmpty(p.DocTypeCode) && codes.Contains(p.DocTypeCode));

            if ((existName != null && existName.Any()) || (existCode != null && existCode.Any()))
            {
                if (!ignoreExist || (existName.Count() == docTypes.Count() || existCode.Count() == docTypes.Count()))
                {
                    throw new EgovException(_resourceService.GetResource("DocType.Create.Exist"));
                }

                var list = docTypes.Where(p => !existName.Contains(p.DocTypeName) || !existCode.Contains(p.DocTypeCode));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("DocType.Create.Exist"));
                }
                Create(list);
                ResetCache();
                return list.ToList();
            }
            else
            {
                Create(docTypes);
                ResetCache();
                return docTypes.ToList();
            }
        }

        /// <summary>
        /// Lấy tổng số các thủ tục
        /// </summary>
        /// <returns></returns>
        public int CountHSMC()
        {
            return _docTypeRepository.Count(d => d.IsActivated && d.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc);
        }

		private void Create(IEnumerable<DocType> docTypes)
		{
			Context.Configuration.AutoDetectChangesEnabled = false;
			foreach (var docType in docTypes)
			{
				if (Exist(x => x.CategoryId == docType.CategoryId && x.CategoryBusinessId == docType.CategoryBusinessId && x.DocTypeName == docType.DocTypeName))
				{
					continue;
				}

				Create(docType);
			}

			Context.Configuration.AutoDetectChangesEnabled = true;
			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật thông tin loại hồ sơ
		/// </summary>
		/// <param name="docType">Entity loại hồ sơ</param>
		/// <param name="oldDocTypeName">Tên loại hồ sơ trước khi cập nhật</param>
		public void Update(DocType docType, string oldDocTypeName, string oldDocTypeCode)
		{
			if (docType == null)
			{
				throw new ArgumentNullException("docType");
			}

			if (_docTypeRepository.Exist(DocTypeQuery.WithDocTypeName(docType.DocTypeName).And(r => r.DocTypeName.ToLower() != oldDocTypeName.ToLower())))
			{
				throw new EgovException(string.Format(_resourceService.GetResource("DocType.CreateOrEdit.Fields.DocTypeName.Exist"), docType.DocTypeName));
			}

            if (!string.IsNullOrEmpty(oldDocTypeCode) && _docTypeRepository.Exist(DocTypeQuery.WithDocTypeCode(docType.DocTypeCode).And(r => r.DocTypeCode.ToLower() != oldDocTypeCode.ToLower())))
            {
                throw new EgovException(string.Format(_resourceService.GetResource("DocType.CreateOrEdit.Fields.DocTypeName.Exist"), docType.DocTypeCode));
            }

            //Cập nhật thông tin về sổ hồ sơ trong loại hồ sơ(văn bản)
            var deleteDocTypeStore = _docTypeStoreRepository.Gets(false, ur => ur.DocTypeId == docType.DocTypeId);

			Context.Configuration.AutoDetectChangesEnabled = false;

			if (deleteDocTypeStore != null && deleteDocTypeStore.Any())
			{
				foreach (var docTypeStore in deleteDocTypeStore)
				{
					_docTypeStoreRepository.Delete(docTypeStore);
				}
			}

			if (!string.IsNullOrEmpty(docType.StoreIds))
			{
				if (!docType.StoreIds.Trim().Contains(";"))
				{
					int outStoreIds;
					int.TryParse(docType.StoreIds.Trim(), out outStoreIds);
					_docTypeStoreRepository.Create(new DocTypeStore { DocTypeId = docType.DocTypeId, StoreId = outStoreIds });
				}
				else
				{
					var arrStoreId = docType.StoreIds.Split(';');
					foreach (var storeId in arrStoreId)
					{
						int outStoreId;
						int.TryParse(storeId, out outStoreId);
						_docTypeStoreRepository.Create(new DocTypeStore { DocTypeId = docType.DocTypeId, StoreId = outStoreId });
					}
				}
			}

			Context.Configuration.AutoDetectChangesEnabled = true;
			Context.SaveChanges();
			ResetCache();
		}

		/// <summary>
		/// Update 
		/// </summary>
		/// <param name="doctype"></param>
		/// <param name="storeIdDefault"></param>
		public void UpdateStoreIdDefault(DocType doctype, int storeIdDefault)
		{
			var oldDoctype = _docTypeStoreRepository.Gets(false, d => d.DocTypeId == doctype.DocTypeId && d.IsDefault == true).FirstOrDefault();
			if (oldDoctype != null)
			{
				if (oldDoctype.StoreId != storeIdDefault)
				{
					oldDoctype.IsDefault = false;
					UpdateDocTypeNewIdDefault(doctype, storeIdDefault);
				}
			}
			else
			{
				UpdateDocTypeNewIdDefault(doctype, storeIdDefault);
			}
			Context.SaveChanges();
		}

		private void UpdateDocTypeNewIdDefault(DocType doctype, int storeIdDefault)
		{
			var newDoctype = _docTypeStoreRepository.Gets(false, d => d.DocTypeId == doctype.DocTypeId && d.StoreId == storeIdDefault).FirstOrDefault();
			if (newDoctype != null)
			{
				newDoctype.IsDefault = true;
			}
		}

		/// <summary>
		/// Xóa 1 loại hồ sơ
		/// </summary>
		/// <param name="docType">Thực thể loại hồ sơ</param>
		public void Delete(DocType docType)
		{
			if (docType == null)
			{
				throw new ArgumentNullException("docType");
			}

			Context.Configuration.AutoDetectChangesEnabled = false;

			var query = GetDeleteCommand();
			var param = new SqlParameter("@id", docType.DocTypeId);
			Context.RawModify(query, new object[] { param });

			Context.Configuration.AutoDetectChangesEnabled = true;

			Context.SaveChanges();

			ResetCache();
		}

		/// <summary>
		/// Xóa các thủ tục theo lĩnh vực
		/// </summary>
		/// <param name="docFieldId"></param>
		public void Delete(int docFieldId)
		{
			if (docFieldId <= 0)
			{
				throw new ArgumentNullException("docFieldId");
			}

			Context.Configuration.AutoDetectChangesEnabled = false;

			var query = GetDeleteCommand(byDocfield: true);
			var param = new SqlParameter("@id", docFieldId);
			Context.RawModify(query, new object[] { param });

			Context.Configuration.AutoDetectChangesEnabled = true;

			Context.SaveChanges();

			ResetCache();
		}
        /// <summary>
		/// Update các thủ tục theo lĩnh vực
		/// </summary>
		/// <param name="doctypeId"></param>
		public void Update(Guid doctypeId, string Link)
        {
            if (doctypeId == null)
            {
                throw new ArgumentNullException("docFieldId");
            }

            Context.Configuration.AutoDetectChangesEnabled = false;

            var doctype = Get(doctypeId);
            doctype.SurveyConfig = Link;

            Context.Configuration.AutoDetectChangesEnabled = true;

            Context.SaveChanges();

            ResetCache();
        }
        /// <summary>  
        /// <para>Tự động sinh mã hồ sơ.</para>
        /// (TienBV@bkav.com - 101212)
        /// </summary>
        /// <param name="dtypeId">The doctype id</param>
        /// <returns></returns>
        public string GetAutoDocCode(Guid dtypeId)
		{
			var result = string.Empty;
			var doctype = Get(dtypeId);

			if (doctype == null)
			{
				return result;
			}
			
			Code code = null;
			if (doctype.CodeId.HasValue)
			{
				code = _codeService.GetFromCache(doctype.CodeId.Value);
			}
			if (code == null)
			{
				return result;
			}
			
			var increase = _codeService.GetIncrease(code.IncreaseId);
			if (increase == null)
			{
				return result;
			}

			result = code.Template;
			var now = DateTime.Now;
			if (result.Contains("$n$") || result.Contains("$N$"))
			{
				var currentDocNum = increase.Value + 1;
				result = result.Replace("$n$", currentDocNum.ToString(CultureInfo.InvariantCulture)).Replace("$N$", currentDocNum.ToString(CultureInfo.InvariantCulture));
			}
			if (result.ToLower().Contains("$y$"))
			{
				result = result.Replace("$y$", now.Year.ToString(CultureInfo.InvariantCulture)).Replace("$Y$", now.Year.ToString(CultureInfo.InvariantCulture));
			}
			if (result.ToLower().Contains("$m$"))
			{
				result = result.Replace("$m$", now.Month.ToString(CultureInfo.InvariantCulture)).Replace("$M$", now.Month.ToString(CultureInfo.InvariantCulture));
			}
			if (result.ToLower().Contains("$d$"))
			{
				result = result.Replace("$d$", now.Day.ToString(CultureInfo.InvariantCulture)).Replace("$D$", now.Day.ToString(CultureInfo.InvariantCulture));
			}
			return result;
		}

		/// <summary>
		/// Trả về DoctypeLaw
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <returns></returns>
		public IEnumerable<DocTypeLaw> GetsDoctypeLaw(Guid doctypeId)
		{
			return _docTypeLawRepository.Gets(true, x => x.DocTypeId == doctypeId);
		}

		/// <summary>
		/// Trả về doctypeName theo Id
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <returns></returns>
		public string GetDoctypeName(Guid? doctypeId)
		{
			if (doctypeId.HasValue)
			{
				var doctype = _docTypeRepository.Get(doctypeId);
				if (doctype != null)
				{
					return doctype.DocTypeName;
				}
			}

			return "";
		}

		/// <summary>
		/// Xóa loại văn bản - văn bản quy phạm
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <param name="lawId"></param>
		public void DeleteDocTypeLaw(Guid doctypeId, int lawId)
		{
			var docTypeLaws = _docTypeLawRepository.Gets(false, p => p.DocTypeId == doctypeId && p.LawId == lawId);
			if (docTypeLaws != null && docTypeLaws.Any())
			{
				foreach (var item in docTypeLaws)
				{
					_docTypeLawRepository.Delete(item);
				}
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Lấy các quy trinh theo loại văn bản
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="projector">Dữ liệu lmap lấy ra</param>
		/// <param name="spec">Bộ lọc</param>
		/// <param name="includeDoctype"> True : Join với  bảng doctype</param>
		/// <returns></returns>
		public IEnumerable<T> GetWorkFlows<T>(Expression<Func<DocfieldDoctypeWorkflow, T>> projector,
			Expression<Func<DocfieldDoctypeWorkflow, bool>> spec = null, bool includeDoctype = true)
		{
			Func<IQueryable<DocfieldDoctypeWorkflow>, IQueryable<DocfieldDoctypeWorkflow>> preFilter = null;
			if (includeDoctype)
			{
				preFilter = Context.Filters.Include<DocfieldDoctypeWorkflow>("DocType");
			}

			return _docfieldDoctypeWorkflowRepository.GetsAs(projector, spec, preFilter);
		}

		/// <summary>
		/// Cập nhật quy trình cho loại văn bản 
		/// </summary>
		/// <param name="docTypeId">Id loại văn bản</param>
		/// <param name="workflowIds">Danh sách id quy trình</param>
		public void UpdateWorkflows(Guid docTypeId, List<int> workflowIds)
		{
			if (workflowIds == null || !workflowIds.Any())
			{
				throw new EgovException(_resourceService.GetResource("DocType.CreateOrEdit.Fields.UpdateWorkflows.NotChoosseWorkflow"));
			}

			var docType = GetFromCache(docTypeId);
			if (docType == null)
			{
				throw new EgovException(_resourceService.GetResource("DocType.CreateOrEdit.Fields.NotExist"));
			}

			var exist = GetWorkFlows(p => p.WorkflowId, p => p.DocTypeId == docTypeId, false);
			var notExist = workflowIds.Where(p => !exist.Contains(p));
			if (notExist != null && notExist.Any())
			{
				foreach (var workflowId in notExist)
				{
					_docfieldDoctypeWorkflowRepository.Create(new DocfieldDoctypeWorkflow
					{
						WorkflowId = workflowId,
						DocTypeId = docTypeId,
						IsActivated = exist.Any() ? false : true
					});
				}

				Context.SaveChanges();
			}
			ResetCache();
		}

		/// <summary>
		/// Loại bỏ quy trình khỏi loại văn bản
		/// </summary>
		/// <param name="docTypeId">id loại văn bản</param>
		/// <param name="workflowIds">Danh sach id quy trình</param>
		public void DeleteWorkflows(Guid docTypeId, List<int> workflowIds)
		{
			if (workflowIds == null || !workflowIds.Any())
			{
				throw new EgovException(_resourceService.GetResource("DocType.CreateOrEdit.Fields.UpdateWorkflows.NotChoosseWorkflow"));
			}

			var docType = GetFromCache(docTypeId);
			if (docType == null)
			{
				throw new EgovException(_resourceService.GetResource("DocType.CreateOrEdit.Fields.NotExist"));
			}

			var exist = GetWorkFlows(p => p, p => docTypeId == p.DocTypeId && workflowIds.Contains(p.WorkflowId), false);
			if (exist != null && exist.Any())
			{
				foreach (var item in exist)
				{
					_docfieldDoctypeWorkflowRepository.Delete(item);
				}
				Context.SaveChanges();
			}
			ResetCache();
		}

		/// <summary>
		/// Cập nhật trạng thái của quy trình 
		/// Note: Trên 1 loại văn bản chỉ có 1 quy trình được hoạt động
		/// </summary>
		/// <param name="docTypeId">Id loại văn bản</param>
		/// <param name="workflowId">Id quy trình</param>
		/// <param name="isActivated">Trạng thái</param>
		public void ChangeActivatedWorkflows(Guid docTypeId, int workflowId, bool isActivated)
		{
			var current = _docfieldDoctypeWorkflowRepository.Get(false, p => docTypeId == p.DocTypeId && p.WorkflowId == workflowId);
			if (current == null)
			{
				throw new EgovException(_resourceService.GetResource("DocType.CreateOrEdit.Fields.DocTypeWorkflows.NotExistWorkflow"));
			}

			if (isActivated)
			{
				var acticvateds = _docfieldDoctypeWorkflowRepository.Gets(false, p => p.IsActivated && p.DocTypeId == docTypeId);
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
			ResetCache();
		}

		/// <summary>
		/// Lấy dữ liêu thô của bảng Doctype=> join các bảng với nhau
		/// </summary>
		public IQueryable<DocType> Raw
		{
			get
			{
				return _docTypeRepository.Raw;
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

		/// <summary>
		/// Reset cache doctype
		/// </summary>
		public void ResetCache()
		{
			_cache.Remove(CacheParam.DocTypeAllKey);
			GetAllFromCache();
		}

		/// <summary>
		/// Cập nhật danh sách lệ phí cho loại hồ sơ
		/// </summary>
		/// <param name="doctypeFees">Danh sách lệ phí</param>
		/// <param name="doctypeId">Loại hồ sơ id</param>
		/// <param name="type">Loại lệ phí</param>
		public void UpdateFees(IEnumerable<Fee> doctypeFees, Guid doctypeId, FeeType type)
		{
			var doctype = Get(doctypeId);
			if (doctype == null)
			{
				return;
			}

			var currentfees = _feeService.Gets(doctypeId, type, false);
			IEnumerable<Fee> deletefees;
			IEnumerable<Fee> createfees;
			if (!doctypeFees.Any())
			{
				deletefees = currentfees;
				createfees = new List<Fee>();
			}
			else
			{
				var feeIds = doctypeFees.Where(p => p.FeeId > 0).Select(p => p.FeeId);
				deletefees = currentfees.Where(p => !feeIds.Contains(p.FeeId));
				createfees = doctypeFees.Where(p => p.FeeId == 0);
			}

			foreach (var fee in deletefees)
			{
				var currentDoctypefees = _doctypeFeeRepository.Gets(false, dp => dp.FeeId == fee.FeeId && dp.DocTypeId == doctypeId);
				if (currentDoctypefees.Any())
				{
					foreach (var doctypeFee in currentDoctypefees)
					{
						_doctypeFeeRepository.Delete(doctypeFee);
					}
				}

				var hasUsed = _doctypeFeeRepository.Exist(dp => dp.FeeId == fee.FeeId && dp.DocTypeId != doctypeId);
				if (!hasUsed)
				{
					_feeService.Delete(fee);
				}
			}

			foreach (var fee in createfees)
			{
				fee.FeeTypeId = (int)type;
				fee.CreatedOnDate = DateTime.Now;
				fee.DocTypeId = doctypeId;
				_feeService.Create(fee);
			}

			Context.SaveChanges();
		}

		/// <summary>
		/// Cập nhật danh sách giấy tờ cho loại hồ sơ
		/// </summary>
		/// <param name="doctypePapers">Danh sách giấy tờ</param>
		/// <param name="doctypeId">Loại hồ sơ id</param>
		/// <param name="type">Loại giấy tờ</param>
		public void UpdatePapers(IEnumerable<Paper> doctypePapers, Guid doctypeId, PaperType type)
		{
			var doctype = Get(doctypeId);
			if (doctype == null)
			{
				return;
			}

			var currentPapers = _paperService.Gets(doctypeId, type, false);
			IEnumerable<Paper> deletePapers;
			IEnumerable<Paper> createPapers;
			if (!doctypePapers.Any())
			{
				deletePapers = currentPapers;
				createPapers = new List<Paper>();
			}
			else
			{
				var paperIds = doctypePapers.Where(p => p.PaperId > 0).Select(p => p.PaperId);
				deletePapers = currentPapers.Where(p => !paperIds.Contains(p.PaperId));
				createPapers = doctypePapers.Where(p => p.PaperId == 0);
			}

			foreach (var paper in deletePapers)
			{
				var currentDoctypePapers = _doctypePaperRepository.Gets(false,
					dp => dp.PaperId == paper.PaperId && dp.DocTypeId == doctypeId);

				if (currentDoctypePapers.Any())
				{
					foreach (var doctypePaper in currentDoctypePapers)
					{
						_doctypePaperRepository.Delete(doctypePaper);
					}
				}

				var hasUsed = _doctypePaperRepository.Exist(dp => dp.PaperId == paper.PaperId && dp.DocTypeId != doctypeId);
				if (!hasUsed)
				{
					_paperService.Delete(paper);
				}
			}

			foreach (var paper in createPapers)
			{
				paper.DocTypeId = doctypeId;
				paper.CreatedOnDate = DateTime.Now;
				paper.PaperTypeId = (int)type;
				_paperService.Create(paper);
			}

			Context.SaveChanges();
		}

		private string GetDeleteCommand(bool byDocfield = false)
		{
			var result = @"SET FOREIGN_KEY_CHECKS=0;

                                UPDATE document d 
                                SET d.`Status` = 8
                                {0}

                                DELETE
	                                bl, df, dp, p, f, dfr, de, dl, ds, ddw, dt
                                FROM doctype dt
                                Left JOIN doctype_fee df on df.DoctypeId = dt.DocTypeId
                                Left JOIN doctype_paper dp on dp.DoctypeId = dt.DocTypeId
                                Left JOIN doctype_form dfr on dfr.DocTypeId = dt.DocTypeId
                                Left JOIN doctype_embryonicform de on de.DocTypeId = dt.DocTypeId
                                Left JOIN doctype_law dl on dl.DocTypeId = dt.DocTypeId
                                Left JOIN doctype_store ds on ds.DocTypeId = dt.DocTypeId
                                Left JOIN docfield_doctype_workflow ddw on ddw.DocTypeId = dt.DocTypeId
                                LEFT JOIN fee f on f.DocTypeId = dt.DocTypeId
                                LEFT JOIN paper p on p.DocTypeId = dt.DocTypeId
                                LEFT JOIN businesslicense bl on bl.DocTypeId = dt.DocTypeId
                                {1}";

			if (byDocfield)
			{
				result = string.Format(result, "WHERE d.DocFieldIds LIKE CONCAT('%;',@id,';%');", "WHERE dt.DocFieldId = @id;");
			}
			else
			{
				result = string.Format(result, "WHERE d.DocTypeId = @id;", "WHERE dt.DocTypeId = @id;");
			}

			return result;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctypeId"></param>
        /// <returns></returns>
        public List<int> GetStoreIds(Guid doctypeId)
		{
			if (doctypeId == null || doctypeId.Equals(Guid.Empty))
				throw new NotImplementedException();

			return _docTypeStoreRepository.GetsAs(d => d.StoreId, d => d.DocTypeId == doctypeId).ToList();
		}

		/// <summary>
		/// Lấy storeid mặc định
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <returns></returns>
		public int GetStoreIdDefault(Guid doctypeId)
		{
			if (doctypeId == null || doctypeId.Equals(Guid.Empty))
				throw new NotImplementedException();
			return _docTypeStoreRepository.GetAs(d => d.StoreId, d => d.DocTypeId == doctypeId && d.IsDefault == true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="doctypeId"></param>
		/// <param name="lawIdToAdds"></param>
		public void AddDoctypeLaws(Guid doctypeId, List<int> lawIdToAdds)
		{
			foreach (var lawId in lawIdToAdds)
			{
				_docTypeLawRepository.Create(new DocTypeLaw()
				{
					DocTypeId = doctypeId,
					LawId = lawId
				});
			}
		}
        public IEnumerable<dynamic> GetActionLevel(Guid doctypeId) {
            string query = "Select * From Doctype Where DocTypeId = @doctypeId";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("doctypeId", doctypeId));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
	}
}
