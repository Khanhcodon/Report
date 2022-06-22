using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IUserDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng User trong CSDL
    /// </summary>
    public class UserDal : DataAccessBase, IUserDal
    {
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Khởi tạo class  <see cref="UserDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public UserDal(IDbCustomerContext context) : base(context)
        {
            _userRepository = Context.GetRepository<User>();
        }

        #pragma warning disable 1591

        public IEnumerable<User> Gets(Expression<Func<User, bool>> spec = null, Func<IQueryable<User>, IQueryable<User>> preFilter = null, params Func<IQueryable<User>, IQueryable<User>>[] postFilters)
        {
            return _userRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<User, TOutput>> projector, Expression<Func<User, bool>> spec = null, Func<IQueryable<User>, IQueryable<User>> preFilter = null, params Func<IQueryable<User>, IQueryable<User>>[] postFilters)
        {
            return _userRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public User Get(int id, bool? isActivated = null)
        {
            Expression<Func<User, bool>> spec;
            if(isActivated.HasValue)
            {
                spec = u => u.UserId == id && u.IsActivated == isActivated.Value;
            }
            else
            {
                spec = u => u.UserId == id;
            }
            return _userRepository.One(spec);
        }

        public TOutput GetAs<TOutput>(Expression<Func<User, TOutput>> projector, Expression<Func<User, bool>> spec = null)
        {
            return _userRepository.OneAs(projector, spec);
        }

        public User Get(string usernameEmailDomain, bool? isActivated = null)
        {
            return _userRepository.One(u => u.UsernameEmailDomain == usernameEmailDomain && (u.IsActivated == isActivated.Value || !isActivated.HasValue));
        }

        public User GetByOpenId(string openId, bool? isActivated = new bool?())
        {
            Expression<Func<User, bool>> spec;
            if (isActivated.HasValue)
            {
                spec = u => u.OpenId == openId && u.IsActivated == isActivated.Value;
            }
            else
            {
                spec = u => u.OpenId == openId;
            }
            return _userRepository.One(spec);
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public void Create(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                _userRepository.Create(user, false);
            }
            Context.SaveChanges();
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public bool Exist(Expression<Func<User, bool>> spec)
        {
            return _userRepository.Any(spec);
        }

        public int Count(Expression<Func<User, bool>> spec = null)
        {
            return _userRepository.Count(spec);
        }
    }
}
