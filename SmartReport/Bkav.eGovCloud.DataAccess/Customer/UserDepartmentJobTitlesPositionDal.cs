using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDepartmentJobTitlesPositionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IUserDal
    /// Create Date : 180912
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng UserDepartmentJobTitlesPosition trong CSDL
    /// </summary>
    public class UserDepartmentJobTitlesPositionDal : DataAccessBase, IUserDepartmentJobTitlesPositionDal
    {
        private readonly IRepository<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositionRepository;

        /// <summary>
        /// Khởi tạo class  <see cref="UserDepartmentJobTitlesPositionDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public UserDepartmentJobTitlesPositionDal(IDbCustomerContext context)
            : base(context)
        {
            _userDepartmentJobTitlesPositionRepository = Context.GetRepository<UserDepartmentJobTitlesPosition>();
        }

        #pragma warning disable 1591

        public IEnumerable<UserDepartmentJobTitlesPosition> Gets(Expression<Func<UserDepartmentJobTitlesPosition, bool>> spec = null)
        {
            return _userDepartmentJobTitlesPositionRepository.Find(spec);
        }

        public void Create(UserDepartmentJobTitlesPosition userDepartmentJobTitlesPosition)
        {
            _userDepartmentJobTitlesPositionRepository.Create(userDepartmentJobTitlesPosition);
        }

        public void Create(IEnumerable<UserDepartmentJobTitlesPosition> userDepartmentJobTitlesPositions)
        {
            foreach (var userDepartmentJobTitlesPosition in userDepartmentJobTitlesPositions)
            {
                _userDepartmentJobTitlesPositionRepository.Create(userDepartmentJobTitlesPosition, false);
            }
            Context.SaveChanges();
        }

        public void Delete(UserDepartmentJobTitlesPosition userDepartmentJobTitlesPosition)
        {
            _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition,false);
        }

        public void Delete(IEnumerable<UserDepartmentJobTitlesPosition> userDepartmentJobTitlesPositions)
        {
            foreach (var userDepartmentJobTitlesPosition in userDepartmentJobTitlesPositions)
            {
                this.Delete(userDepartmentJobTitlesPosition);
            }
            Context.SaveChanges();
        }
    }
}
