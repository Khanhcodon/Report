using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IAccountDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Account trong CSDL
    /// </summary>
    public class AccountDal : DataAccessBase, IAccountDal
    {
        private readonly IRepository<Account> _accountRepository; 
        /// <summary>
        /// Khởi tạo class <see cref="AccountDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public AccountDal(IDbAdminContext context) : base(context)
        {
            _accountRepository = Context.GetRepository<Account>();
        }

        #pragma warning disable 1591

        public IEnumerable<Account> Gets(Expression<Func<Account, bool>> spec = null)
        {
            return _accountRepository.Find(spec);
        }

        public Account Get(int id)
        {
            return _accountRepository.One(a => a.AccountId == id);
        }

        public Account Get(string usernameEmailDomain, bool? isActive = null)
        {
            return _accountRepository.One(a => a.UsernameEmailDomain == usernameEmailDomain && (a.IsActivated == isActive.Value || !isActive.HasValue));
        }

        public void Create(Account account)
        {
            _accountRepository.Create(account);
        }

        public void Update(Account account)
        {
            _accountRepository.Update(account);
        }

        public void Delete(Account account)
        {
            _accountRepository.Delete(account);
        }

        public bool Exist(Expression<Func<Account, bool>> spec)
        {
            return _accountRepository.Any(spec);
        }
    }
}
