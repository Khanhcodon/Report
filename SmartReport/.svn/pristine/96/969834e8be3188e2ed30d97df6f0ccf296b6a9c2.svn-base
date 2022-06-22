using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 300712
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Account trong CSDL
    /// </summary>
    public class AccountBll : ServiceBase
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<AccountDomain> _accountDomainRepository;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly LdapProvider _ldap;

        ///<summary>
        /// Khởi tạo class <see cref="AccountBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="ldap"></param>
        ///<param name="authenticationSettings"></param>
        public AccountBll(IDbAdminContext context, LdapProvider ldap, AuthenticationSettings authenticationSettings)
            : base(context)
        {
            _accountRepository = Context.GetRepository<Account>();
            _accountDomainRepository = Context.GetRepository<AccountDomain>();
            _ldap = ldap;
            _authenticationSettings = authenticationSettings;
        }

        /// <summary>
        /// Lấy ra người dùng theo id
        /// </summary>
        /// <param name="accountId">Id của người dùng</param>
        /// <returns>Entity người dùng</returns>
        public Account Get(int accountId)
        {
            Account result = null;
            if (accountId > 0)
            {
                result = _accountRepository.Get(accountId);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra người dùng theo tên đăng nhập dạng username@domain
        /// </summary>
        /// <param name="userEmailDomain"></param>
        /// <param name="isActivated">Kiểm tra thêm điều kiện tài khoản đang hoạt động : true và ngược lại: false. Nếu là null sẽ bỏ qua điều kiện này</param>
        /// <returns>Entity người dùng</returns>
        public Account Get(string userEmailDomain, bool? isActivated = null)
        {
            Account result = null;
            if (!string.IsNullOrWhiteSpace(userEmailDomain))
            {
                result = _accountRepository.Get(false,
                    u =>
                        u.UsernameEmailDomain == userEmailDomain &&
                        (u.IsActivated == isActivated.Value || !isActivated.HasValue));
            }
            return result;
        }

        /// <summary>
        /// Trả về danh sách tất cả account trong hệ thống
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> Gets()
        {
            return _accountRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra tất cả account
        /// </summary>
        /// <param name="totalRecords"> Tổng số bản ghi </param>
        /// <param name="currentPage"> Trang hiện tại </param>
        /// <param name="pageSize"> Số bản ghi trên 1 trang </param>
        /// <param name="sortBy"> Sắp xếp theo </param>
        /// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
        /// <param name="name"> Tên nhảy số </param>
        /// <param name="domainId"></param>
        /// <returns> Danh sách nhảy số </returns>
        public IEnumerable<Account> Gets(out int totalRecords, int currentPage = 1,
             int? pageSize = null, string sortBy = "", bool isDescending = false,
             string name = "", int domainId = 0)
        {
            name = string.IsNullOrWhiteSpace(name) ? "" : name.ToLower();
            if (!pageSize.HasValue)
            {
                pageSize = 100;
            }


            totalRecords = _accountRepository.Count(a => a.Username.ToLower().Contains(name) && (domainId == 0 || a.AccountDomains.Any(ac => ac.DomainId == domainId)));

            var sort = Context.Filters.CreateSort<Account>(isDescending, sortBy);
            return _accountRepository.GetsReadOnly(a => a.Username.ToLower().Contains(name) && (domainId == 0 || a.AccountDomains.Any(ac => ac.DomainId == domainId)),
                            sort, Context.Filters.Page<Account>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isActivated"></param>
        /// <returns></returns>
        public Account GetByUserName(string userName, bool? isActivated = null)
        {
            Account result = null;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                result = _accountRepository.Get(false,
                    u =>
                        u.Username == userName &&
                        (u.IsActivated == isActivated.Value || !isActivated.HasValue));
            }
            return result;
        }

        /// <summary>
        /// Trả về account theo userdomainname
        /// </summary>
        /// <param name="userDomainName">User Domain name</param>
        /// <param name="isActivated"></param>
        /// <returns></returns>
        public Account GetByUserDomainName(string userDomainName, bool? isActivated = null)
        {
            Account result = null;
            if (string.IsNullOrWhiteSpace(userDomainName))
            {
                return result;
            }

            result = _accountRepository.Get(false, u => u.UsernameEmailDomain.Equals(userDomainName, StringComparison.OrdinalIgnoreCase)
                                                    && (u.IsActivated == isActivated.Value || !isActivated.HasValue));

            return result;
        }

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        /// <param name="account">Entity người dùng</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity người dùng truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên đăng nhập đã tồn tại</exception>
        public void Create(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account");
            }
            if (_accountRepository.Exist(AccountQuery.WithUsernameEmailDomain(account.UsernameEmailDomain)))
            {
                throw new Exception("Tên account đã tồn tại!");
            }
            _accountRepository.Create(account);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="account">Entity người dùng</param>
        /// <param name="oldUsername">Tên đăng nhập trước khi cập nhật</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity người dùng truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên đăng nhập đã tồn tại</exception>
        public void Update(Account account, string oldUsername)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account");
            }
            if (_accountRepository.Exist(AccountQuery.WithUsernameEmailDomain(account.UsernameEmailDomain).And(a => a.UsernameEmailDomain != oldUsername)))
            {
                throw new Exception("Tên account đã tồn tại!");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="account">Entity người dùng</param>
        /// <param name="oldUsername">Tên đăng nhập trước khi cập nhật</param>
        /// <param name="domainIds">Danh sách domain account thuộc về</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity người dùng truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên đăng nhập đã tồn tại</exception>
        public void UpdateWithDomain(Account account, string oldUsername, IEnumerable<int> domainIds)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account");
            }

            if (_accountRepository.Exist(AccountQuery.WithUsernameEmailDomain(account.UsernameEmailDomain).And(a => a.UsernameEmailDomain != oldUsername)))
            {
                throw new Exception("Tên account đã tồn tại!");
            }

            var removeDomains = account.AccountDomains.Where(ad => ad.AccountId == account.AccountId && !domainIds.Contains(ad.DomainId)).ToList();
            if (removeDomains.Any())
            {
                foreach (var ad in removeDomains)
                {
                    _accountDomainRepository.Delete(ad);
                }
            }

            foreach (var domainId in domainIds)
            {
                if (_accountDomainRepository.Exist(ad => ad.DomainId == domainId && ad.AccountId == account.AccountId))
                {
                    continue;
                }

                _accountDomainRepository.Create(new AccountDomain() { AccountId = account.AccountId, DomainId = domainId });
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xác thực thông tin đăng nhập
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <param name="isLockoutAccount">Hệ thống cho phép khóa tài khoản khi đăng nhập nhiều lần không thành công: true và ngược lại: false</param>
        /// <param name="maxLoginFailure">Số lần tối đa được đăng nhập không thành công</param>
        /// <param name="lockoutDuration">Thời gian khóa tài khoản (tính bằng giây)</param>
        ///<returns>Identity</returns>
        public Account CustomerAuthenticate(string username, string password, bool isLockoutAccount,
                                            int maxLoginFailure, int lockoutDuration)
        {
            var account = GetByUserDomainName(username, true);
            if (account == null)
            {
                throw new EgovException("Không tìm thấy người dùng! Vui lòng xem lại tên đăng nhập.");
            }
            var currentDate = DateTime.Now;
            if (isLockoutAccount)
            {
                var failedLoginCount = account.FailedPasswordAttemptCount.HasValue
                                       ? account.FailedPasswordAttemptCount.Value
                                       : 0;
                if (account.IsLockedOut)
                {
                    if (!account.LastLockoutDate.HasValue)
                    {
                        account.LastLockoutDate = currentDate;
                        account.FailedPasswordAttemptStart = currentDate;
                        account.FailedPasswordAttemptCount = maxLoginFailure;
                        Context.SaveChanges();
                        throw new EgovException(string.Format("Tài khoản bị khóa trong {0} phút", lockoutDuration / 60));
                    }
                    var unlockTime = account.LastLockoutDate.Value.AddSeconds(lockoutDuration);
                    if (unlockTime.CompareTo(currentDate) > 0)
                    {
                        throw new EgovException(string.Format("Tài khoản bị khóa trong {0} phút", ((int)unlockTime.Subtract(currentDate).TotalMinutes) + 1));
                    }
                }
                var inputPwdHash = Generate.GetInputPasswordHash(password, account.PasswordSalt);

                if (!account.PasswordHash.SequenceEqual(inputPwdHash))
                {
                    LockAccount(account, failedLoginCount, currentDate, lockoutDuration, maxLoginFailure);
                }

                if (failedLoginCount > 0)
                {
                    account.FailedPasswordAttemptCount = 0;
                    account.FailedPasswordAttemptStart = DateTime.Parse("01/01/1900");
                    account.LastLockoutDate = DateTime.Parse("01/01/1900");
                }
            }
            else
            {
                var inputPwdHash = Generate.GetInputPasswordHash(password, account.PasswordSalt);

                if (!account.PasswordHash.SequenceEqual(inputPwdHash))
                {
                    throw new EgovException("Login Failed");
                }
            }
            account.LastLoginDate = currentDate;
            Context.SaveChanges();

            return account;
        }

        /// <summary>
        /// Đăng nhập thông qua ldap
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account CustomerAuthenticateLdap(string username, string password)
        {
            var account = Get(username, true);
            if (account == null)
            {
                throw new EgovException("Login Failed");
            }
            Account result = null;
            if (_authenticationSettings.EnableLdap)
            {
                var isAuthenticate = _ldap.Authenticate(_authenticationSettings.LdapHost, _authenticationSettings.LdapPort, _authenticationSettings.LdapSSL, account.Username, password);

                //var isAuthenticate = _ldap.Authenticate(_authenticationSettings.LdapHost, _authenticationSettings.LdapPort, _authenticationSettings.LdapSSL,
                //                                 _authenticationSettings.LdapBaseDn, _authenticationSettings.LdapUsername,
                //                                 _authenticationSettings.LdapPassword, account.Username, password,
                //                                 _authenticationSettings.LdapAuthenticationFilter);
                if (isAuthenticate)
                {
                    result = account;
                }
            }
            return result;
        }

        private void LockAccount(Account account, int failedLoginCount, DateTime currentDate, int lockoutDuration, int maxLoginFailure)
        {
            if (!account.FailedPasswordAttemptStart.HasValue)
            {
                account.FailedPasswordAttemptCount = 1;
                account.FailedPasswordAttemptStart = currentDate;
            }
            else
            {
                if (account.FailedPasswordAttemptStart.Value.AddSeconds(lockoutDuration).CompareTo(currentDate) > 0)
                {
                    account.FailedPasswordAttemptCount = failedLoginCount + 1;
                    account.FailedPasswordAttemptStart = currentDate;
                }
                else
                {
                    account.FailedPasswordAttemptCount = 1;
                    account.FailedPasswordAttemptStart = currentDate;
                }
            }
            account.IsLockedOut = account.FailedPasswordAttemptCount >= maxLoginFailure;
            if (account.IsLockedOut)
            {
                account.LastLockoutDate = currentDate;
            }

            Context.SaveChanges();
            if (maxLoginFailure > account.FailedPasswordAttemptCount)
            {
                throw new EgovException(string.Format("Bạn còn {0} lần đăng nhập nữa", maxLoginFailure - account.FailedPasswordAttemptCount));
            }
            throw new EgovException(string.Format("Tài khoản bị khóa trong {0} phút", lockoutDuration / 60));
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="username">Tên đăng nhập của người dùng</param>
        /// <param name="currentPassword">Mật khẩu hiện tại</param>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// <param name="isCheckPasswordHistory">Kiểm tra lịch sử mật khẩu : true và ngược lại : false</param>
        /// <param name="historyCount">Số mật khẩu cũ cần kiểm tra</param>
        /// <returns>true nếu cập nhật thành công và ngược lại</returns>
        public bool ChangePassword(string username, string currentPassword, string newPassword, bool isCheckPasswordHistory, int historyCount)
        {
            var account = _accountRepository.Get(false, u => u.UsernameEmailDomain == username && u.IsActivated);
            if (account == null)
            {
                throw new EgovException(string.Format("Tên đăng nhập không tồn tại", username));
            }
            var inputPwdHash = Generate.GetInputPasswordHash(currentPassword, account.PasswordSalt);

            if (!account.PasswordHash.SequenceEqual(inputPwdHash))
            {
                throw new EgovException("Mật khẩu nhập lại không đúng");
            }
            if (isCheckPasswordHistory)
            {
                var histories = account.AccountPasswordHistorys.OrderByDescending(h => h.CreatedOnDate).Take(historyCount);
                foreach (var history in histories)
                {
                    var hash = Generate.GetInputPasswordHash(newPassword, history.PasswordSalt);
                    if (hash.SequenceEqual(history.PasswordHash))
                    {
                        throw new EgovException("Mật khẩu mới trùng với mật khẩu cũ");
                    }
                }
            }

            var now = DateTime.Now;
            account.AccountPasswordHistorys.Add(new AccountPasswordHistory
                                                    {
                                                        AccountId = account.AccountId,
                                                        Username = account.Username,
                                                        PasswordHash = account.PasswordHash,
                                                        PasswordSalt = account.PasswordSalt,
                                                        CreatedOnDate = now
                                                    });
            var newSalt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var newHash = Generate.GetInputPasswordHash(newPassword, newSalt);
            account.PasswordHash = newHash;
            account.PasswordSalt = newSalt;
            account.PasswordLastModifiedOnDate = now;

            Context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="username">Tên đăng nhập của người dùng</param>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// <param name="isCheckPasswordHistory">Kiểm tra lịch sử mật khẩu : true và ngược lại : false</param>
        /// <param name="historyCount">Số mật khẩu cũ cần kiểm tra</param>
        /// <returns>true nếu cập nhật thành công và ngược lại</returns>
        public bool ResetPassword(string username, string newPassword, bool isCheckPasswordHistory, int historyCount)
        {
            var account = _accountRepository.Get(false, u => u.UsernameEmailDomain == username);
            if (account == null)
            {
                throw new EgovException(string.Format("Tên đăng nhập không tồn tại", username));
            }
            if (isCheckPasswordHistory)
            {
                var histories = account.AccountPasswordHistorys.OrderByDescending(h => h.CreatedOnDate).Take(historyCount);
                foreach (var history in histories)
                {
                    var hash = Generate.GetInputPasswordHash(newPassword, history.PasswordSalt);
                    if (hash.SequenceEqual(history.PasswordHash))
                    {
                        throw new EgovException("Mật khẩu mới trùng với mật khẩu cũ");
                    }
                }
            }
            var now = DateTime.Now;
            account.AccountPasswordHistorys.Add(new AccountPasswordHistory
            {
                AccountId = account.AccountId,
                Username = account.Username,
                PasswordHash = account.PasswordHash,
                PasswordSalt = account.PasswordSalt,
                CreatedOnDate = now
            });
            var newSalt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var newHash = Generate.GetInputPasswordHash(newPassword, newSalt);
            account.PasswordHash = newHash;
            account.PasswordSalt = newSalt;
            account.PasswordLastModifiedOnDate = now;

            Context.SaveChanges();
            return true;
        }

    }
}
