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
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : TemplateKeyBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 130313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm xử lý template key </para>
    /// <para> ( TienBV@bkav.com - 130313) </para>
    /// </summary>
    public class TemplateKeyCategoryBll : ServiceBase
    {
        private readonly IRepository<TemplateKeyCategory> _templateKeyCategoryRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public TemplateKeyCategoryBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings)
            : base(context)
        {
            _templateKeyCategoryRepository = Context.GetRepository<TemplateKeyCategory>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Trả về danh sách các template key điều kiện kỹ thuật truyền vào. Kết quả chỉ đọc
        /// (Tienbv@bkav.com 200313)
        /// </summary>
        /// <param name="spec">The spec</param>
        /// <returns></returns>
        public IEnumerable<TemplateKeyCategory> Gets(Expression<Func<TemplateKeyCategory, bool>> spec = null)
        {
            return _templateKeyCategoryRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về tất cả template key có phân trang và sort. Kết quả chỉ đọc
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector"></param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trong trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">true: lớn -> nhỏ, false: ngược lại</param>
        /// <param name="keySearch">search theo tên</param>
        /// <returns>Danh sách các template tương ứng</returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
                                        Expression<Func<TemplateKeyCategory, T>> projector,
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string keySearch = "",
                                        int? type = null)
        {

            // 20200210 VuHQ START Phase 2 - REQ 0
            //var spec = !string.IsNullOrWhiteSpace(keySearch)
            //            ? TemplateKeyQuery.ContainsKey(keySearch)
            //            : null;
            var spec = TemplateKeyCategoryQuery.ContainsKey(keySearch);
            // 20200210 VuHQ END Phase 2 - REQ 0

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _templateKeyCategoryRepository.Count(spec);
            var sort = Context.Filters.CreateSort<TemplateKeyCategory>(isDescending, sortBy);
            return _templateKeyCategoryRepository.GetsAs(projector, spec, sort, Context.Filters.Page<TemplateKeyCategory>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Trả về template key theo id.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="id">The template key id</param>
        /// <returns></returns>
        public TemplateKeyCategory Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return _templateKeyCategoryRepository.Get(id);
        }

        /// <summary>
        /// Thêm key
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TemplateKeyCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            //if (!Exist(entity.Name))
            //{
            _templateKeyCategoryRepository.Create(entity);
            Context.SaveChanges();
            //}
        }

        /// <summary>
        /// Xóa templale key.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="entity">Template key entity</param>
        public void Delete(TemplateKeyCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _templateKeyCategoryRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật template key.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="entity">Template key entity</param>
        public void Update(TemplateKeyCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật template key.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="entity">Template key entity</param>
        public string GetTemplateKeyCategoryName(int? templateKeyCategoryId)
        {
            var templateKeyCategory = _templateKeyCategoryRepository.Get(templateKeyCategoryId);
            if (templateKeyCategory == null)
            {
                return string.Empty;
            }

            return templateKeyCategory.Name;
        }
    }
}
