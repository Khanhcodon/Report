using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentPositionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IUserDal
    /// Create Date : 180912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng UserDepartmentPosition trong CSDL
    /// </summary>
    public class UserDepartmentPositionDal : DataAccessBase, IUserDepartmentPositionDal
    {
        private readonly IRepository<UserDepartmentPosition> _userDepartmentPositionRepository;

        /// <summary>
        /// Khởi tạo class  <see cref="UserDepartmentPositionDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public UserDepartmentPositionDal(IDbCustomerContext context)
            : base(context)
        {
            _userDepartmentPositionRepository = Context.GetRepository<UserDepartmentPosition>();
        }

        #pragma warning disable 1591

        public IEnumerable<UserDepartmentPosition> Gets(Expression<Func<UserDepartmentPosition, bool>> spec = null)
        {
            return _userDepartmentPositionRepository.Find(spec);
        }

        public void Create(UserDepartmentPosition userDepartmentPosition)
        {
            _userDepartmentPositionRepository.Create(userDepartmentPosition);
        }

        public void CreateMultipleRecords(IEnumerable<UserDepartmentPosition> userDepartmentPositions)
        {
            foreach (var userDepartmentPosition in userDepartmentPositions)
            {
                _userDepartmentPositionRepository.Create(userDepartmentPosition, false);
            }
            Context.SaveChanges();
        }

        public void Delete(UserDepartmentPosition userDepartmentPosition)
        {
            _userDepartmentPositionRepository.Delete(userDepartmentPosition);
        }

        public void DeleteMultipleRecords(IEnumerable<UserDepartmentPosition> userDepartmentPositions)
        {
            foreach (var userDepartmentPosition in userDepartmentPositions)
            {
                _userDepartmentPositionRepository.Delete(userDepartmentPosition, false);
            }
            Context.SaveChanges();
        }
    }
}
