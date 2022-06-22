#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Business.Objects;
using System.Data.SqlClient;

#endregion

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	///   <para> Bkav Corp. - BSO - eGov - eOffice team </para>
	///   <para> Project: eGov Cloud v1.0 </para>
	///   <para> Class : CodeBll - public - BLL </para>
	///   <para> Access Modifiers: </para>
	///   <para> Create Date : 300712 </para>
	///   <para> Author : DungHV </para>
	///   <para> Description : BLL tương ứng với bảng Code trong CSDL </para>
	/// </summary>
	public class CodeBll : ServiceBase
	{
		#region Readonly & Static Fields

		private readonly IRepository<Code> _codeRepository;
		private readonly IRepository<Increase> _increaseRepository;
		private readonly CategoryBll _categoryService;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly IncreaseBll _increaseService;
		private readonly ResourceBll _resourceService;
		private readonly UserBll _userService;
		private readonly IRepository<Store> _storeRepository;
		private readonly IRepository<StoreCode> _storeCodeRepository;
		private readonly IRepository<CodeTemp> _codeTempRepository;
		private readonly IRepository<CategoryCode> _categoryCodeRepository;
		private readonly IRepository<Document> _docRepository;
		private readonly MemoryCacheManager _cache;

		const int DATE_SUBTRACTS = 2;
		const string CACHE_KEY = CacheParam.CodeKey;
		const int CACHE_TIMEOUT = CacheParam.CodeCacheTimeOut;

		#endregion

		#region C'tors

		///<summary>
		///  Khởi tạo class <see cref="CodeBll" />.
		///</summary>
		///<param name="context">Context</param>
		///<param name="generalSettings"> Cấu hình chung </param>
		///<param name="increaseService"> Bll liên quan đến bảng Increase trong CSDL</param>
		///<param name="categoryService"> Bll liên quan đến bảng Category trong CSDL</param>
		///<param name="resourceService"> Bll liên quan đến bảng Resource trong CSDL</param>
		///<param name="userService"> Bll liên quan đến bảng User trong CSDL</param>
		///<param name="cache"></param>
		public CodeBll(
			IDbCustomerContext context,
			AdminGeneralSettings generalSettings,
			IncreaseBll increaseService,
			CategoryBll categoryService,
			ResourceBll resourceService,
			MemoryCacheManager cache,
			UserBll userService)
			: base(context)
		{
			_codeRepository = Context.GetRepository<Code>();
			_increaseRepository = Context.GetRepository<Increase>();
			_generalSettings = generalSettings;
			_increaseService = increaseService;
			_categoryService = categoryService;
			_resourceService = resourceService;
			_userService = userService;
			_storeRepository = Context.GetRepository<Store>();
			_storeCodeRepository = Context.GetRepository<StoreCode>();
			_codeTempRepository = Context.GetRepository<CodeTemp>();
			_categoryCodeRepository = Context.GetRepository<CategoryCode>();
			_docRepository = Context.GetRepository<Document>();
			_cache = cache;
		}

		#endregion

		#region Quản lý bảng mã

		/// <summary>
		///   Tạo mới mã
		/// </summary>
		/// <param name="code"> Entity bảng mã </param>
		public void Create(Code code)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			if (_codeRepository.Exist(CodeQuery.WithName(code.CodeName)))
			{
				throw new EgovException(string.Format("Tên mã ({0}) đã tồn tại!", code.CodeName));
			}

			_codeRepository.Create(code);
			Context.SaveChanges();

			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// Thêm mới bẳng mã
		/// </summary>
		/// <param name="codes"></param>
		/// <param name="ignoreExist"></param>
		public void Create(IEnumerable<Code> codes, bool ignoreExist)
		{
			if (codes == null || !codes.Any())
			{
				throw new ArgumentNullException("codes");
			}

			var names = codes.Select(x => x.CodeName);
			var exist = _codeRepository.GetsAs(p => p.CodeName, p => names.Contains(p.CodeName));

			if (exist != null && exist.Any())
			{
				if (!ignoreExist || exist.Count() == codes.Count())
				{
					throw new EgovException(_resourceService.GetResource("Code.Create.Exist"));
				}

				var list = codes.Where(p => !exist.Contains(p.CodeName));
				if (list == null || !list.Any())
				{
					throw new EgovException(_resourceService.GetResource("Code.Create.Exist"));
				}
				Create(list);
			}
			else
			{
				Create(codes);
			}
		}

		private void Create(IEnumerable<Code> codes)
		{
			Context.Configuration.AutoDetectChangesEnabled = false;
			foreach (var code in codes)
			{
				_codeRepository.Create(code);
			}
			Context.Configuration.AutoDetectChangesEnabled = true;
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		///   Xóa 1 bảng mã
		/// </summary>
		/// <param name="code"> Thực thể bảng mã </param>
		public void Delete(Code code)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			//kiểm tra ràng buộc dữ liệu trong bảng category_code
			var isUsed = _categoryService.Exist(c => c.CategoryCodes.Any(i => i.CodeId == code.CodeId));
			if (isUsed)
			{
				throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.Delete.Exception.Category"));
			}

			//kiểm tra ràng buộc dữ liệu trong bảng store_code
			isUsed = _storeRepository.Exist(s => s.StoreCodes.Any(k => k.CodeId == code.CodeId));
			if (isUsed)
			{
				throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Code.Delete.Exception.Store"));
			}

			var cateCodes = _categoryCodeRepository.Gets(false, p => p.CodeId == code.CodeId);
			if (cateCodes != null && cateCodes.Any())
			{
				foreach (var item in cateCodes)
				{
					_categoryCodeRepository.Delete(item);
				}
			}

			_codeRepository.Delete(code);
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		///   Lấy ra mã theo id
		/// </summary>
		/// <param name="codeId"> Id của mã </param>
		/// <returns> Entity nhảy số </returns>
		public Code Get(int codeId)
		{
			Code result = null;
			if (codeId > 0)
			{
				result = _codeRepository.Get(codeId);
			}
			return result;
		}

		/// <summary>
		/// Trả về bảng mã theo id, kết quả được lấy từ cache
		/// </summary>
		/// <param name="codeId"></param>
		/// <returns></returns>
		public Code GetFromCache(int codeId)
		{
			return GetsFromCache().SingleOrDefault(c => c.CodeId == codeId);
		}

		/// <summary>
		/// Trả về danh sách bảng mã có cache lại kết quả
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Code> GetsFromCache()
		{
			return _cache.Get<IEnumerable<Code>>(CACHE_KEY, () =>
			{
				return Gets();
			}, CACHE_TIMEOUT);
		}

		/// <summary>
		///   DungHV 170913
		///   Lấy các mã theo điều  kiện kỹ thuật truyền vào. Kết quả chỉ đọc
		/// </summary>
		/// <param name="spec"> </param>
		/// <returns> </returns>
		public IEnumerable<Code> Gets(Expression<Func<Code, bool>> spec = null)
		{
			return _codeRepository.GetsReadOnly(spec);
		}

		/// <summary>
		/// HopCV 231215
		/// Lấy các mã theo sổ văn bản, theo linh vuc. Kết quả chỉ đọc
		/// </summary>
		/// <param name="storeId">Id sổ văn bản</param>
		/// <param name="categoryId">id linh vuc</param>
		/// <returns> </returns>
		public IEnumerable<int> GetCodeIds(int storeId, int categoryId)
		{
			var storeCodes = _storeCodeRepository.GetsAs(p => p.CodeId, s => s.StoreId == storeId);
			if (storeCodes == null || !storeCodes.Any())
			{
				return null;
			}

			var categoryCodeIds = _categoryService.GetsAs(categoryId, c => c.CodeId).ToList();
			if (categoryCodeIds == null || !categoryCodeIds.Any())
			{
				return null;
			}

			var results = new List<int>();
			var exist = storeCodes.Where(p => categoryCodeIds.Contains(p));

			if (exist != null && exist.Any())
			{
				results.AddRange(exist);
			}

			return results.Distinct();
		}

		/// <summary>
		///   Lấy ra danh sách bảng mã
		/// </summary>
		/// <param name="totalRecords"> Tổng số bản ghi </param>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="currentPage"> Trang hiện tại </param>
		/// <param name="pageSize"> Số bản ghi trên 1 trang </param>
		/// <param name="sortBy"> Sắp xếp theo </param>
		/// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
		/// <param name="codename"> Tên sổ văn bản, hồ sơ </param>
		/// <param name="groupId"></param>
		/// <returns> Danh sách bang mã </returns>
		public IEnumerable<T> GetsAs<T>(out int totalRecords, Expression<Func<Code, T>> projector,
			int currentPage = 1,
			int? pageSize = null, string sortBy = "StoreName", bool isDescending = false,
			 string codename = "", int? groupId = null)
		{
			var spec = !string.IsNullOrWhiteSpace(codename)
						   ? CodeQuery.ContainsName(codename)
						   : null;
			if (groupId.HasValue)
			{
				if (spec != null)
					spec = spec.And(p => p.BussinessDocFieldDocTypeGroupId == groupId.Value);
				else
					spec = p => p.BussinessDocFieldDocTypeGroupId == groupId.Value;
			}

			if (!pageSize.HasValue)
			{
				pageSize = _generalSettings.DefaultPageSize;
			}
			totalRecords = _codeRepository.Count(spec);
			var sort = Context.Filters.CreateSort<Code>(isDescending, sortBy);
			return _codeRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Code>(currentPage, pageSize.Value));
		}

		/// <summary>
		/// Lấy ra tất cả các mã phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
		/// </summary>
		/// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="spec">Điều kiện</param>
		/// <returns>Danh sách các thực thể được ánh xạ</returns>
		public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Code, TOutput>> projector,
													Expression<Func<Code, bool>> spec = null)
		{
			return _codeRepository.GetsAs(projector, spec);
		}

		/// <summary>
		/// Trả về nhảy số của bảng mã
		/// </summary>
		/// <param name="codeId"></param>
		/// <returns></returns>
		public Increase GetIncrease(int codeId)
		{
			var code = GetFromCache(codeId);
			return _increaseRepository.Get(code.IncreaseId);
		}

		/// <summary>
		///   Cập nhật thông tin bảng mã
		/// </summary>
		/// <param name="code"> Entity bảng mã </param>
		/// <param name="oldCodeName"> Tên bảng mã trước khi cập nhật </param>
		public void Update(Code code, string oldCodeName)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			if (_codeRepository.Exist(CodeQuery.WithName(code.CodeName).And(r => r.CodeName.ToLower() != oldCodeName.ToLower())))
			{
				throw new Exception(string.Format("Tên mã ({0}) đã tồn tại!", code.CodeName));
			}
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		#endregion

		#region Code Notations

		/// <summary>
		/// Trả về danh sách phần text của skh
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, List<string>> GetCodeNotations(int userId)
		{
			return GetCache().Where(c => c.UserCreatedId == userId && !string.IsNullOrWhiteSpace(c.Organization))
						.OrderByDescending(c => c.DateCreated)
						.GroupBy(c => c.Organization)
						.ToDictionary(c => c.Key, c => c.Select(i => i.DocCode.Split(new char[] { '/' }).Last()).Distinct().ToList());
		}

		#endregion

		#region Cấp mã

		/// <summary>
		/// Trả về mã được cấp
		/// </summary>
		/// <param name="codeId">Id mã tương ứng</param>
		/// <param name="date">Ngày cấp</param>
		/// <param name="categoryBusiness">categoryBusiness</param>
		/// <param name="isDocCode">Lấy số ký hiệu</param>
		/// <param name="storeId"></param>
		/// <returns>Mã được cấp</returns>
		public string GetCode(int codeId, DateTime date, CategoryBusinessTypes categoryBusiness, bool isDocCode = true, int storeId = 0)
		{
			var code = Get(codeId);
			return GetCode(code, date, categoryBusiness, isDocCode, storeId);
		}

		/// <summary>
		/// Trả về mã được cấp
		/// </summary>
		/// <param name="code">Code</param>
		/// <param name="date">Ngày cấp</param>
		/// <param name="isDocCode">Lấy số ký hiệu</param>
		/// <param name="categoryBusiness">categoryBusiness</param>
		/// <param name="organization"></param>
		/// <param name="storeId"></param>
		/// <returns>Mã được cấp</returns>
		public string GetCode(Code code, DateTime date, CategoryBusinessTypes categoryBusiness, bool isDocCode = true, int storeId = 0, string organization = "")
		{
			var result = string.Empty;
			if (code == null)
			{
				return result;
			}

			if (code.HasCapSoTruoc) // Cho phép cấp và giữ số được cấp
			{
				return IssueCode(code, date, null, isTemporaryCode: true);
			}

			var increase = _increaseRepository.Get(code.IncreaseId);
			if (increase == null)
			{
				return result;
			}

			var increaseNumber = increase.Value;

			// Cấp phát số mới nếu không có số thu hồi
			// Cấp phát đến khi mã dc cấp chưa được sử dụng.
			do
			{
				increaseNumber++;
				result = BuildCode(code.Template, increaseNumber, date);
			} while (CodeIsUsed(result, isDocCode, storeId, categoryBusiness, organization));

			// Cập nhật nhảy số mới đến số dc cấp
			// Trường hợp có nhiều số tiếp theo trùng thì sẽ nhảy số hiện tại đến số mới nhất 
			if (increaseNumber - increase.Value > 1)
			{
				increase.Value = increaseNumber - 1;
				// Tạm bỏ để theo transaction
				SaveChanges();
			}

			return result;
		}

		/// <summary>
		/// Trả về mã được cấp hàng loạt
		/// </summary>
		/// <param name="codeIds">Danh sách id mã</param>
		/// <param name="date">Ngày cấp</param>
		/// <param name="categoryBusiness"></param>
		/// <param name="isDocCode">Lấy số ký hiệu</param>
		/// <param name="storeId"></param>
		/// <returns>Danh sách mã tương ứng</returns>
		public Dictionary<int, string> GetCodes(IEnumerable<int> codeIds, DateTime date, CategoryBusinessTypes categoryBusiness, bool isDocCode = true, int storeId = 0)
		{
			var result = new Dictionary<int, string>();

			var codes = GetsFromCache().Where(c => codeIds.Contains(c.CodeId));
			var increaseNumberIds = codes.Select(c => c.IncreaseId).ToList();
			var codeIncreaseNumbers = _increaseService.Gets(i => increaseNumberIds.Contains(i.IncreaseId), isReadOnly: false);

			foreach (var code in codes)
			{
				// Cần gọi hàm: GetCode(Code code, DateTime date, bool isDocCode = true, int storeId = 0, string organization = "") chỗ này

				if (code.HasCapSoTruoc) // Cho phép cấp và giữ số được cấp
				{
					result.Add(code.CodeId, IssueCode(code, date, null, true));
					continue;
				}

				var increaseNumber = codeIncreaseNumbers.SingleOrDefault(c => c.IncreaseId == code.IncreaseId);
				if (increaseNumber == null)
				{
					continue;
				}

				var number = increaseNumber.Value;				
				var newCode = "";
				do
				{
					// Cấp phát số mới nếu không có số thu hồi
					// Cấp phát đến khi mã dc cấp chưa được sử dụng.

					number++;
					newCode = BuildCode(code.Template, number, date);
				} while (CodeIsUsed(newCode, isDocCode, storeId, categoryBusiness));
				
				if (string.IsNullOrEmpty(newCode))
				{
					continue;
				}

				result.Add(code.CodeId, newCode);
			}

			SaveChanges();

			return result;
		}

		/// <summary>
		/// Trả về mã được cấp hàng loạt
		/// </summary>
		/// <param name="codes">Danh sách bảng mã</param>
		/// <param name="date">Ngày cấp</param>
		/// <param name="categoryBusiness"></param>
		/// <param name="isDocCode">Lấy số ký hiệu</param>
		/// <param name="storeId"></param>
		/// <returns>Danh sách mã</returns>
		public IEnumerable<string> GetCodes(IEnumerable<Code> codes, DateTime date, CategoryBusinessTypes categoryBusiness, bool isDocCode = true, int storeId = 0)
		{
			var result = new List<string>();
			if (codes.Any())
			{
				foreach (var code in codes)
				{
					result.Add(GetCode(code, date, categoryBusiness, isDocCode, storeId));
				}
			}

			return result;
		}

		/// <summary>
		/// Xác nhận mã đã cấp: sử dụng khi tiếp nhận, bàn giao hồ sơ văn bản.
		/// </summary>
		/// <param name="codeId">Mã code</param>
		/// <param name="date">Ngày cấp</param>
		/// <param name="documentId">ID hồ sơ</param>
		/// <param name="docCode">Mã đã cấp</param>
		/// <returns></returns>
		public string ConfirmCode(int codeId, DateTime date, Guid? documentId, string docCode = "")
		{
			var code = Get(codeId);
			return ConfirmCode(code, date, documentId, docCode);
		}

		/// <summary>
		/// Xác nhận mã đã cấp: sử dụng khi tiếp nhận, bàn giao hồ sơ văn bản.
		/// </summary>
		/// <param name="code">code</param>
		/// <param name="date">Ngày cấp</param>
		/// <param name="documentId">ID hồ sơ</param>
		/// <param name="docCode">Mã đã cấp</param>
		/// <returns></returns>
		public string ConfirmCode(Code code, DateTime date, Guid? documentId, string docCode = "")
		{
			CodeTemp tempCode = null;
			var result = "";

			if (code == null)
			{
				return docCode ?? result;
			}

			var userId = _userService.CurrentUser.UserId;
			var codeId = code.CodeId;

			if (!code.HasCapSoTruoc)
			{
				if (string.IsNullOrEmpty(docCode))
				{
					docCode = IssueCode(code, date);
				}

				var increase = _increaseRepository.Get(code.IncreaseId);
				_increaseService.IncreaseFromCode(increase, code.Template, docCode);
				SaveChanges();

				return docCode;
			}

			// Cấp số trước
			// Kiểm tra trong mã tạm trước
			if (!string.IsNullOrEmpty(docCode))
			{
				tempCode = _codeTempRepository.Get(false, c => c.CodeId == codeId
						&& c.UserId == userId && c.Code == docCode && c.Type == (int)CodeTempTypeEnum.UsingCode);
			}

			// Trường hợp không tìm thấy mã cấp tạm trong hệ thống
			if (tempCode == null)
			{
				result = string.IsNullOrEmpty(docCode) ? IssueCode(code, date, documentId, false) : docCode;
				if (!string.IsNullOrEmpty(docCode))
				{
					var newCodeTemp = new CodeTemp()
					{
						Code = docCode,
						CodeId = codeId,
						UserId = userId,
						DocumentId = documentId,
						Type = (int)CodeTempTypeEnum.UsedCode,
						DateCreated = date
					};
					CreateCodeTemp(newCodeTemp);
				}
			}
			else
			{
				result = tempCode.Code;
				tempCode.Type = (int)CodeTempTypeEnum.UsedCode;
				tempCode.DocumentId = documentId;
				tempCode.UserId = userId;
				Context.SaveChanges();
			}

			return result;
		}

		/// <summary>
		/// Hủy mã đã cấp
		/// </summary>
		/// <param name="code">Mã đang cấp</param>
		/// <param name="userId">User sử dụng</param>
		/// <remarks>
		/// Hàm này chỉ sử dụng trong trường hợp người dùng tiếp nhận văn bản, hồ sơ mới nhưng chưa lưu
		/// </remarks>
		public void CancelCode(string code, int userId)
		{
			if (string.IsNullOrEmpty(code))
			{
				return;
			}

			var codeTemp = _codeTempRepository.Get(false, c => c.Code == code && c.UserId == userId && c.Type == (int)CodeTempTypeEnum.UsingCode);
			if (codeTemp != null)
			{
				codeTemp.UserId = 0;
				codeTemp.DateCreated = DateTime.MinValue;
				codeTemp.Type = (int)CodeTempTypeEnum.CancelCode;
				Context.SaveChanges();
			}
		}

		/// <summary>
		/// Khoi phuc code do huy document
		/// </summary>
		/// <param name="document">Document Id</param>
		public void ReuseFromDocument(Document document)
		{
			var documentId = document.DocumentId;
			var codeTemp = _codeTempRepository.Get(false, c => c.DocumentId.HasValue && c.DocumentId.Value.Equals(documentId) && c.Type == (int)CodeTempTypeEnum.UsedCode);
			if (codeTemp != null)
			{
				codeTemp.DateCreated = DateTime.MinValue;
				codeTemp.Type = (int)CodeTempTypeEnum.CancelCode;
				Context.SaveChanges();
			}
			else if (document.CodeId.HasValue)
			{
				CreateCodeTemp(new CodeTemp()
				{
					Code = document.DocCode,
					DocumentId = documentId,
					DateCreated = DateTime.Now,
					CodeId = document.CodeId.Value,
					Type = (int)CodeTempTypeEnum.CancelCode
				});
			}
		}

		/// <summary>
		/// Giảm số tự tăng
		/// </summary>
		/// <param name="codeId"></param>
		public void DecreaseDocNumber(int codeId)
		{
			if (codeId < 1)
			{
				return;
			}

			var code = Get(codeId);
			if (code == null)
			{
				return;
			}

			var inscrease = _increaseRepository.Get(code.IncreaseId);
			if (inscrease != null)
			{
				inscrease.Value -= 1;
				Context.SaveChanges();
			}
		}

		#endregion

		#region Cache

		private List<CodeUseds> GetCache()
		{
			var year = DateTime.Now.Year;

			var cacheKey = string.Format(CacheParam.CodeUsedKey, year);
			var documentInYears = _cache.Get<List<CodeUseds>>(cacheKey, CacheParam.CodeUsedCacheTimeOut, () =>
			{
				return GetsCacheDocuments(year).ToList();
			});
			return documentInYears;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="year"></param>
		/// <returns></returns>
		private IEnumerable<CodeUseds> GetsCacheDocuments(int year)
		{
			//var date = new DateTime(year, 1, 1);
			//var result = _docRepository.GetsAs<CodeUseds>(d => new CodeUseds()
			//{
			//	DocumentId = d.DocumentId,

			//	// Số ký hiệu đi với CQBH
			//	Organization = d.Organization,
			//	DocCode = d.DocCode,

			//	// Số đến đi với Sổ đến
			//	InOutCode = d.InOutCode,
			//	StoreId = d.StoreId,

			//	Compendium = d.Compendium,
			//	DateCreated = d.DateCreated,

			//	CategoryBusinessId = d.CategoryBusinessId,

			//	Original = d.Original,
			//	InOutPlace = d.InOutPlace,
			//	UserCreatedId = d.UserCreatedId
			//}, d => d.DateCreated.Year == year
			//			&& d.Status != (int)DocumentStatus.DuThao && d.Status != (int)DocumentStatus.LoaiBo && d.Status != (int)DocumentStatus.LienThongTrungSo);

			var cmd = @"SELECT DocumentId, Organization, DocCode, InOutCode, StoreId, Compendium, DateCreated, CategoryBusinessId, Original, InOutPlace, UserCreatedId
						FROM `document` 
						WHERE DateCreated >= @from and DateCreated <= @to
							AND `Status` not in (1, 8, 32) AND(DocCode != '' Or InOutCode != ''); ";

			var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@from", new DateTime(year, 1, 1)));
			parameters.Add(new SqlParameter("@to", new DateTime(year, 12, 31, 23, 59 , 59)));

			var codes = Context.RawQuery(cmd, parameters.ToArray());
			var result = Json2.ParseAs<IEnumerable<CodeUseds>>(Json2.Stringify(codes));

			return result;
		}

		/// <summary>
		/// Thêm code đã sử dụng vào cache
		/// </summary>
		/// <param name="documentId"></param>
		/// <param name="docCode"></param>
		/// <param name="inOutCode"></param>
		/// <param name="categoryBusinessId"></param>
		/// <param name="organization"></param>
		/// <param name="storeId"></param>
		public void AddUsedCache(Guid documentId, string docCode, string inOutCode, int categoryBusinessId, string organization, int? storeId)
		{
			var caches = GetCache();
			var codeCached = caches.SingleOrDefault(d => d.DocumentId == documentId);
			if (codeCached != null)
			{
				if (!string.IsNullOrEmpty(docCode))
				{
					codeCached.DocCode = docCode;
				}

				if (!string.IsNullOrEmpty(inOutCode))
				{
					codeCached.InOutCode = inOutCode;
				}

				codeCached.Organization = organization;
				codeCached.StoreId = storeId;
				codeCached.CategoryBusinessId = categoryBusinessId;
			}
			else
			{
				caches.Add(new CodeUseds()
				{
					DocCode = docCode,
					InOutCode = inOutCode,
					Organization = organization,
					StoreId = storeId,
					DocumentId = documentId,
					CategoryBusinessId = categoryBusinessId
				});
			}

			var year = DateTime.Now.Year;
			var cacheKey = string.Format(CacheParam.CodeUsedKey, year);
			_cache.Set(cacheKey, caches, CacheParam.CodeUsedCacheTimeOut);
		}

		/// <summary>
		/// Xóa mã được cấp cho văn bản
		/// </summary>
		/// <param name="documentId"></param>
		/// <param name="onlyRemoveInOutCode">Chỉ xóa số đến</param>
		public void RemoveUsedCache(Guid documentId, bool onlyRemoveInOutCode = false)
		{
			var caches = GetCache();
			var codeCached = caches.SingleOrDefault(d => d.DocumentId == documentId);

			if (codeCached == null)
			{
				return;
			}

			if (onlyRemoveInOutCode)
			{
				codeCached.InOutCode = "";
			}
			else
			{
				caches.Remove(codeCached);
			}

			var year = DateTime.Now.Year;
			var cacheKey = string.Format(CacheParam.CodeUsedKey, year);
			_cache.Set(cacheKey, caches, CacheParam.CodeUsedCacheTimeOut);
		}

		#endregion

		#region Private Methods

		/// <summary>
		///   <para>Cấp phát số kí hiệu cho văn bản. Bao gồm tăng số thư tự văn bản $N$.</para>
		///   <para>Lấy số kí hiệu dự kiến để hiển thị, sử dụng hàm AnticipateCode().</para>
		///   <para>CuongNT@bkav.com - 310713</para>
		/// </summary>
		/// <param name="code"> The code </param>
		/// <param name="dateOfIssueCode"> </param>
		/// <param name="isTemporaryCode"></param>
		/// <param name="documentId"></param>
		/// <returns> </returns>
		private string IssueCode(Code code, DateTime dateOfIssueCode, Guid? documentId, bool isTemporaryCode = true)
		{
			var codeId = code.CodeId;
			var userId = _userService.CurrentUser.UserId;
			CodeTemp codeTemp = null;
			var now = DateTime.Now;
			var result = "";

			if (codeTemp != null)
			{
				codeTemp.UserId = userId;
				codeTemp.DateCreated = DateTime.Now;
				codeTemp.Type = isTemporaryCode ? (int)CodeTempTypeEnum.UsingCode : (int)CodeTempTypeEnum.UsedCode;
				codeTemp.DocumentId = documentId;
				Context.SaveChanges();

				return codeTemp.Code;
			}

			// Cấp phát số mới nếu không có số thu hồi
			// Cấp phát đến khi mã dc cấp chưa được sử dụng.
			do
			{
				result = IssueCode(code, dateOfIssueCode);
			} while ((_codeTempRepository.Exist(c => c.Code == result && c.Type == (int)CodeTempTypeEnum.UsedCode)));

			return result;
		}

		private string IssueCode(Code code, DateTime dateOfIssueCode)
		{
			var inscrease = _increaseRepository.Get(code.IncreaseId);
			if (inscrease == null)
			{
				return "";
			}

			var increaseNumber = inscrease.Value + 1;
			_increaseService.IncreaseDocNumber(inscrease);

			var result = BuildCode(code.Template, increaseNumber, dateOfIssueCode);

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="codeTemplate"></param>
		/// <param name="increaseNumber"></param>
		/// <param name="dateOfIssueCode"></param>
		/// <returns></returns>
		private string BuildCode(string codeTemplate, int increaseNumber, DateTime dateOfIssueCode)
		{
			if (string.IsNullOrEmpty(codeTemplate))
			{
				throw new ArgumentNullException("codeTemplate");
			}

			var result = BuildCode(codeTemplate, increaseNumber);

			result = BuildCode(result, dateOfIssueCode);

			return result;
		}

		private string BuildCode(string codeTemplate, int increaseNumber)
		{
			var result = codeTemplate;
			if (result.ToLower().Contains("$n$"))
			{
				result = result.Replace("$n$", increaseNumber.ToString(CultureInfo.InvariantCulture)).Replace("$N$", increaseNumber.ToString(CultureInfo.InvariantCulture));
			}
			return result;
		}

		private string BuildCode(string codeTemplate, DateTime dateOfIssueCode)
		{
			var result = codeTemplate;
			if (result.ToLower().Contains("$y$"))
			{
				result = result.Replace("$y$", dateOfIssueCode.ToString("yyyy")).Replace("$Y$", dateOfIssueCode.ToString("yyyy"));
			}
			if (result.ToLower().Contains("$m$"))
			{
				result = result.Replace("$m$", dateOfIssueCode.ToString("MM")).Replace("$M$", dateOfIssueCode.ToString("MM"));
			}
			if (result.ToLower().Contains("$d$"))
			{
				result = result.Replace("$d$", dateOfIssueCode.ToString("dd")).Replace("$D$", dateOfIssueCode.ToString("dd"));
			}
			return result;
		}

		/// <summary>
		/// Lấy Code tenp đã bị hủy hoặc đang được cấp
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public IEnumerable<CodeTemp> GetCodeTemp(int userId)
		{
			var codeTemps = _codeTempRepository.Gets(true, c => c.UserId == userId
													&& c.Type == (int)CodeTempTypeEnum.CancelCode
													|| c.Type == (int)CodeTempTypeEnum.UsingCode);
			return codeTemps;
		}

		/// <summary>
		/// Xóa CodeTemp theo id
		/// </summary>
		/// <param name="codeTempId"></param>
		public void DeleteCodeTemp(int codeTempId)
		{

			var codeTemp = _codeTempRepository.Get(codeTempId);
			if (codeTemp != null)
			{
				_codeTempRepository.Delete(codeTemp);
			}

			Context.SaveChanges();
		}

		/// <summary>
		/// Xóa tất cả codetemptype = 1 hoặc =3 của 1 user
		/// </summary>
		/// <param name="userId"></param>
		public void DeleteAllCodeTempCancelUsing(int userId)
		{
			var codeTemps = GetCodeTemp(userId);
			if (codeTemps.Count() > 0 && codeTemps.Any())
			{
				foreach (var codeTemp in codeTemps)
				{
					_codeTempRepository.Delete(codeTemp);
				}
			}
			Context.SaveChanges();
		}

		/// <summary>
		/// Tạo record mới cho bảng CodeTemp
		/// </summary>
		/// <param name="codeTemp"></param>
		private void CreateCodeTemp(CodeTemp codeTemp)
		{
			var codeTempExisted = _codeTempRepository.Get(false, c => c.Code.Equals(codeTemp.Code) && c.CodeId == codeTemp.CodeId && c.UserId == codeTemp.UserId);
			if (codeTempExisted == null)
			{
				_codeTempRepository.Create(codeTemp);
			}
			Context.SaveChanges();
		}

		/// <summary>
		/// Kiểm tra mã đã được cấp
		/// </summary>
		/// <param name="code"></param>
		/// <param name="isDocCode"></param>
		/// <param name="storeId"></param>
		/// <param name="categoryBusiness"></param>
		/// <param name="organization"></param>
		/// <param name="documentId">Văn bản hiện tại</param>
		/// <param name="inOutPlace">Đơn vị nhận văn bản: khi phát hành nội bộ, khi liên thông</param>
		/// <returns></returns>
		public bool CodeIsUsed(string code, bool isDocCode, int storeId,
									CategoryBusinessTypes? categoryBusiness = null, string organization = "", Guid? documentId = null,
									string inOutPlace = "")
		{
			/*
             Các trường hợp cần lưu ý:
             - Số Đến kiểm tra theo Sổ, Số ký hiệu kiểm tra theo cơ quan ban hành nếu là Văn bản, Nếu là HSMC thì chỉ cần kiểm tra có tồn tại.
             - Kiểm tra trùng số theo năm, các năm khác sau có thể trùng số.
             - Kiểm tra trùng số theo theo nghiệp vụ, ví dụ văn bản đến và văn bản đi có cùng số vẫn cho phép.
             - Một số trường hợp khác:
                + Văn bản phát hành cho nhiều đơn vị trong nội bộ (Original = 0, CategoryBusinessTypes.VbDen): trường hợp này cần check theo đơn vị nhận văn bản (InOutPlace).
                + Văn bản liên thông cho nhiều phòng ban trong đơn vị (Original = 2, CategoryBusinessTypes.VbDen): trường hợp này cần check theo phòng ban nhận văn bản (InOutPlace).
             */

			var result = false;
			code = code.Trim();
			if (code.Equals("không", StringComparison.OrdinalIgnoreCase) || code.Equals("0"))
			{
				return result;
			}

			// Danh sách số đã sử dụng từ cache
			var usedCode = GetCache();

			if (usedCode == null)
			{
				return result;
			}

			var docs = isDocCode ? usedCode.Where(d => !string.IsNullOrEmpty(d.DocCode) && d.DocCode.Equals(code, StringComparison.OrdinalIgnoreCase))
								: usedCode.Where(d => !string.IsNullOrEmpty(d.InOutCode) && d.InOutCode.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (categoryBusiness.HasValue)
			{
                if (docs == null || !docs.Any())
                {
                    return result;
                }
                docs = docs.Where(d => d.CategoryBusinessIdInEnum == categoryBusiness.Value);
			}

			// Bỏ qua văn bản đang kiểm tra
			if (documentId.HasValue && !documentId.Value.Equals(Guid.Empty))
			{
                if (docs == null || !docs.Any())
                {
                    return result;
                }
                docs = docs.Where(d => d.DocumentId != documentId.Value);
			}

			if (isDocCode)
			{
				if (categoryBusiness.HasValue && categoryBusiness.Value == CategoryBusinessTypes.Hsmc)
				{
					// Hsmc không kiểm tra theo cqbh;
					organization = "";
				}

				if (!string.IsNullOrEmpty(organization))
				{
                    if (docs == null || !docs.Any())
                    {
                        return result;
                    }
                    docs = docs.Where(d => !string.IsNullOrWhiteSpace(d.Organization) && d.Organization.Equals(organization, StringComparison.OrdinalIgnoreCase));
				}

				if (!string.IsNullOrWhiteSpace(inOutPlace) && categoryBusiness.HasValue && categoryBusiness.Value == CategoryBusinessTypes.VbDen)
				{
                    // - Một số trường hợp khác:
                    //    + Văn bản phát hành cho nhiều đơn vị trong nội bộ (Original = 0, CategoryBusinessTypes.VbDen): trường hợp này cần check theo đơn vị nhận văn bản (InOutPlace).
                    //    + Văn bản liên thông cho nhiều phòng ban trong đơn vị (Original = 2, CategoryBusinessTypes.VbDen): trường hợp này cần check theo phòng ban nhận văn bản (InOutPlace).
                    if (docs == null || !docs.Any())
                    {
                        return result;
                    }

                    docs = docs.Where(d => !string.IsNullOrEmpty(d.InOutPlace) && d.InOutPlace.Equals(inOutPlace, StringComparison.OrdinalIgnoreCase));
				}
			}
			else
			{
				if (storeId != 0)
				{
                    if (docs == null || !docs.Any())
                    {
                        return result;
                    }
                    docs = docs.Where(d => d.StoreId.HasValue && d.StoreId.Value == storeId);
				}
			}

			result = docs != null && docs.Any();

			return result;
		}

		/// <summary>
		/// Trả về các số ký hiệu trùng đã được cấp trong năm
		/// </summary>
		/// <param name="code"></param>
		/// <param name="documentId"></param>
		/// <returns></returns>
		public List<CodeUseds> GetDocCodeUsed(string code, Guid? documentId = null)
		{
			var result = new List<CodeUseds>();
			var usedCode = GetCache();

			if (usedCode == null || string.IsNullOrEmpty(code))
			{
				return result;
			}

			result = usedCode.Where(d => d.DocCode.Equals(code, StringComparison.OrdinalIgnoreCase) && (!documentId.HasValue || d.DocumentId != documentId.Value)).ToList();

			return result;
		}

		#endregion
	}
}