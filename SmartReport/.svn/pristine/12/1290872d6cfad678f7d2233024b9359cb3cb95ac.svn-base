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
    /// <para>Class : KeyWordBll - public - BLL</para>
    /// <para>Create Date : 221113</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng KeyWord trong CSDL</para>
    /// </summary>
    public class KeyWordBll : ServiceBase
    {
        private readonly IRepository<KeyWord> _keywordRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="CityBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="resourceService"></param>
        public KeyWordBll(IDbCustomerContext context
            , AdminGeneralSettings generalSettings
            , ResourceBll resourceService)
            : base(context)
        {
            _keywordRepository = Context.GetRepository<KeyWord>();
            _generalSettings = generalSettings;
            _resourceService = resourceService;
        }

        /// <summary>
        /// Xóa 1 từ khóa
        /// </summary>
        /// <param name="keyword">Thực thể từ khóa</param>
        public void Delete(KeyWord keyword)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }
            _keywordRepository.Delete(keyword);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả từ khóa theo điều kiện. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<KeyWord> Gets(Expression<Func<KeyWord, bool>> spec = null)
        {
            return _keywordRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra một từ khóa
        /// </summary>
        /// <param name="keywordId">Id của từ khóa</param>
        /// <returns>Entity từ khóa</returns>
        public KeyWord Get(int keywordId)
        {
            KeyWord keyword = null;
            if (keywordId > 0)
            {
                keyword = _keywordRepository.Get(keywordId);
            }
            return keyword;
        }

        /// <summary>
        /// Thêm mới từ khóa
        /// </summary>
        /// <param name="keyword">Thực thể từ khóa</param>
        /// <returns></returns>
        public void Create(KeyWord keyword)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }
            if (_keywordRepository.Exist(KeyWordQuery.WithName(keyword.KeyWordName)))
            {
                throw new EgovException(string.Format("Từ khóa ({0}) đã tồn tại!", keyword.KeyWordName));
            }
            _keywordRepository.Create(keyword);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới từ khóa
        /// </summary>
        /// <param name="keyWords">Danh sách từ khóa</param>
        /// <param name="ignoreExist">True: Bỏ qua các từ khóa khi thêm mới nếu đã tồn tai; False: validate khi thêm nếu đã tồn tại</param>
        public void Create(IEnumerable<KeyWord> keyWords, bool ignoreExist)
        {
            if (keyWords == null || !keyWords.Any())
            {
                throw new ArgumentNullException("keyWords");
            }

            var names = keyWords.Select(x => x.KeyWordName);
            var exist = _keywordRepository.GetsAs(p => p.KeyWordName, p => names.Contains(p.KeyWordName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == keyWords.Count())
                {
                    throw new EgovException(_resourceService.GetResource("KeyWord.Create.Exist"));
                }

                var list = keyWords.Where(p => !exist.Contains(p.KeyWordName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("KeyWord.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(keyWords);
            }
        }

        private void Create(IEnumerable<KeyWord> keyWords)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in keyWords)
            {
                _keywordRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin từ khóa
        /// </summary>
        /// <param name="keyword">Entity từ khóa</param>
        /// <param name="oldKeyWordName">Tên từ khóa trước khi cập nhật</param>
        public void Update(KeyWord keyword, string oldKeyWordName)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException("keyword");
            }
            if (_keywordRepository.Exist(KeyWordQuery.WithName(keyword.KeyWordName.Trim()).And(r => r.KeyWordName.ToLower() != oldKeyWordName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Từ khóa ({0}) đã tồn tại!", keyword.KeyWordName));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các từ khóa có phân trang
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="keywordname">Key của resource (dùng để tìm kiếm) Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các resource có key gần giống với key truyền vào</param>
        /// <returns>Danh sách các từ khóa đã được phân trang</returns>
        public IEnumerable<KeyWord> Gets(out int totalRecords,
                                                        int currentPage = 1,
                                                        int? pageSize = null,
                                                        string sortBy = "",
                                                        bool isDescending = false,
                                                        string keywordname = "")
        {
            var spec = !string.IsNullOrWhiteSpace(keywordname)
                        ? KeyWordQuery.ContainsName(keywordname)
                        : null;
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _keywordRepository.Count(spec);
            var sort = Context.Filters.CreateSort<KeyWord>(isDescending, sortBy);
            return _keywordRepository.GetsReadOnly(spec, sort, Context.Filters.Page<KeyWord>(currentPage, pageSize.Value));
        }
    }
}
