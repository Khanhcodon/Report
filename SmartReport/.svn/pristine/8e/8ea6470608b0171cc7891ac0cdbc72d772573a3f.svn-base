using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Lớp quản lý khách hàng
    /// </summary>
    public class CustomerBll : ServiceBase
    {
        private readonly IRepository<Bkav.eGovCloud.Entities.Admin.Customer> _customerRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resourceService"></param>
        public CustomerBll(IDbAdminContext context, ResourceBll resourceService)
            : base(context)
        {
            _customerRepository = Context.GetRepository<Bkav.eGovCloud.Entities.Admin.Customer>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả Bkav.eGovCloud.Entities.Admin.Customer phù hợp với các điều kiện truyền vào. Nếu tất cả các điều kiện đều là null thì sẽ lấy ra tất cả các Bkav.eGovCloud.Entities.Admin.Customer
        /// </summary>
        /// <returns>Danh sách các Bkav.eGovCloud.Entities.Admin.Customer phù hợp với điều kiện</returns>
        public IEnumerable<Bkav.eGovCloud.Entities.Admin.Customer> Gets()
        {
            return _customerRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra tất cả các tỉnh đã có khách hàng và tổng số khách hàng tương ứng với tỉnh đó
        /// </summary>
        /// <returns>Chuỗi json có dạng [{name:"Tên tỉnh, thành phố + số Bkav.eGovCloud.Entities.Admin.Customer", value:"Tên tỉnh, thành phố"}, {...}] 
        /// </returns>
        public string GetsAvailableProvince()
        {
            return _customerRepository.GetsAs(d => d.Province)
                    .GroupBy(d => d)
                    .Select(d => new { name = d.Key + " (" + d.Count() + ")", value = d.Key })
                    .OrderBy(d => d.name).StringifyJs();
        }

        /// <summary>
        /// Lấy ra Bkav.eGovCloud.Entities.Admin.Customer theo id
        /// </summary>
        /// <param name="id">Id của Bkav.eGovCloud.Entities.Admin.Customer</param>
        /// <returns>Entity Bkav.eGovCloud.Entities.Admin.Customer</returns>
        public Bkav.eGovCloud.Entities.Admin.Customer Get(int id)
        {
            Bkav.eGovCloud.Entities.Admin.Customer result = null;
            if (id > 0)
            {
                result = _customerRepository.Get(id);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới Bkav.eGovCloud.Entities.Admin.Customer
        /// </summary>
        /// <param name="customer">Entity Bkav.eGovCloud.Entities.Admin.Customer</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity Bkav.eGovCloud.Entities.Admin.Customer truyền vào bị null</exception>
        public void Create(Bkav.eGovCloud.Entities.Admin.Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("Bkav.eGovCloud.Entities.Admin.Customer");
            }
            _customerRepository.Create(customer);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer">Khách hàng</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity Bkav.eGovCloud.Entities.Admin.Customer truyền vào bị null</exception>
        public void Update(Bkav.eGovCloud.Entities.Admin.Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("Bkav.eGovCloud.Entities.Admin.Customer");
            }
            Context.SaveChanges();
        }
    }
}
