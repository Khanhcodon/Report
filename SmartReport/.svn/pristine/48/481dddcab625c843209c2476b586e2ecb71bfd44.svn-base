using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para>Class : ConfigTypeBll - public - BLL</para> 
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 20191101</para> 
    /// <para>Author      : VuHQ</para> 
    /// <para>Description : Quản lý danh mục loại cấu hình eForm.</para> 
    /// </author>
    /// <summary>
    /// <para> Quản lý những danh mục loại cấu hình sẽ bind ra các select box khi soạn form động.</para>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class ConfigTypeBll : ServiceBase
    {
        private readonly IRepository<ConfigType> _configTypeRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="ConfigTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        public ConfigTypeBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _configTypeRepository = Context.GetRepository<ConfigType>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="configType">ConfigType Entity</param>
        public void Create(ConfigType configType)
        {
            if (configType == null)
            {
                throw new ArgumentNullException("configType");
            }
            if (_configTypeRepository.Exist(c => c.TypeName == configType.TypeName))
            {
                throw new EgovException("Danh mục loại cấu hình đã tồn tại!");
            }

            _configTypeRepository.Create(configType);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy danh mục.
        /// </summary>
        /// <param name="id">the catalog id.</param>
        /// <returns>The Catalog object.</returns>
        public ConfigType Get(Guid id)
        {
            var result = _configTypeRepository.Get(id);
            return result;
        }

        /// <summary>
        /// <para> Xóa danh mục loại cấu hình.</para>
        /// <para> Note:</para>
        /// <para> - Nếu danh mục và các đối đượng của nó đã được sử dụng trong form, hồ sơ thì không được xóa.</para>
        /// <para> - Khi xóa danh mục sẽ xóa hết tất cả các đối tượng con của danh mục đó.</para>
        /// </summary>
        /// <param name="configType">the configType.</param>
        public void Detele(ConfigType configType)
        {
            var values = _configTypeRepository.Gets(false, v => v.ParentId == configType.ParentId);
            foreach (var val in values)
            {
                _configTypeRepository.Delete(val);
            }
            _configTypeRepository.Delete(configType);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<ConfigType> Gets(string sortBy = "", bool isDescending = false)
        {
            return _configTypeRepository.GetsReadOnly(null, Context.Filters.CreateSort<ConfigType>(isDescending, sortBy));
        }

        /// <summary>
        /// Lấy ra tất cả config type theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public IEnumerable<ConfigType> Gets(Expression<Func<ConfigType, bool>> spec = null)
        {
            return _configTypeRepository.GetsReadOnly(spec);
        }
    }
}
