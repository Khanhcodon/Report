using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : AddressDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 150813
    /// <para></para> Author      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng Address trong CSDL
    /// </summary>
    public class AddressDal : DataAccessBase, IAddressDal
    {
        private readonly IRepository<Address> _addressRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public AddressDal(IDbCustomerContext context)
            : base(context)
        {
            _addressRepository = context.GetRepository<Address>();
        }

        #pragma warning disable 1591

        #region IAddressDal Members

        public void Create(Address address)
        {
            _addressRepository.Create(address);
        }

        public void Delete(Address address)
        {
            _addressRepository.Delete(address);
        }

        public void Update(Address address)
        {
            _addressRepository.Update(address);
        }

        public Address Get(int id)
        {
            return _addressRepository.One(id);
        }


        /// <summary> 
        /// <para>Trả về danh sách tất cả các đối tượng cơ quan ngoài. </para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các đối tượng cơ quan ngoài</returns>
        public IEnumerable<Address> Gets(Expression<Func<Address, bool>> spec = null, Func<IQueryable<Address>, IQueryable<Address>> preFilter = null, params Func<IQueryable<Address>, IQueryable<Address>>[] postFilters)
        {
            return _addressRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Address, TOutput>> projector, Expression<Func<Address, bool>> spec = null, Func<IQueryable<Address>, IQueryable<Address>> preFilter = null,
            params Func<IQueryable<Address>, IQueryable<Address>>[] postFilters)
        {
            return _addressRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        #endregion

    }
}
