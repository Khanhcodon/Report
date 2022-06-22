using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    ///
    /// </summary>
    public class CitizenBll : ServiceBase
    {
        private readonly IRepository<Citizen> _citizenRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public CitizenBll(IDbCustomerContext context)
            : base(context)
        {
            _citizenRepository = Context.GetRepository<Citizen>();
        }

        /// <summary>
        /// Thêm mới người dùng
        /// </summary>
        /// <param name="citizen">Entity người dùng</param>
        public void Create(Citizen citizen)
        {
            if (citizen == null)
            {
                throw new ArgumentNullException("citizen");
            }
            if (_citizenRepository.Exist(CitizenQuery.WithAccount(citizen.Account)))
            {
                throw new Exception("citizen");
            }
            _citizenRepository.Create(citizen);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="citizen">Entity người dùng</param>
        public void Update(Citizen citizen)
        {
            if (citizen == null)
            {
                throw new ArgumentNullException("citizen");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra người dùng bằng id
        /// </summary>
        /// <param name="id">Id của người dùng</param>
        /// <returns>Entity người dùng</returns>
        public Citizen GetById(int id)
        {
            Citizen citizen = null;
            if (id > 0)
            {
                citizen = _citizenRepository.Get(id);
            }
            return citizen;
        }

        /// <summary>
        /// Lấy ra tất cả người dùng có phân trang. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="citizenName">account (dùng để tìm kiếm) Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các Citizen có account gần giống với account truyền vào</param>
        /// <returns>Danh sách người dùng đã được phân trang</returns>
        public IEnumerable<Citizen> Gets(out int totalRecords, int currentPage = 1, int? pageSize = null, string sortBy = "", bool isDescending = false, string citizenName = "")
        {
            var spec = !string.IsNullOrWhiteSpace(citizenName)
                           ? CitizenQuery.ContainsCitizenName(citizenName)
                           : null;
            var defaultPageSize = 20; // Cần đưa giá trị vào quản trị
            if (!pageSize.HasValue)
            {
                pageSize = defaultPageSize;
            }
            totalRecords = _citizenRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Citizen>(isDescending, sortBy);
            return _citizenRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Citizen>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// check Login
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public bool Login(string account, string password)
        {
            bool result = false;
            var citizen = GetByAccount(account);
            if (citizen != null)
            {
                if (citizen.PasswordHash.Equals(password) && citizen.IsActivated)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Lọc người dân
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idCardNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public IEnumerable<Citizen> Filter(string name = null, string idCardNumber = null, string phoneNumber = null, string email = null, string address = null)
        {
            name = StringExtension.StripVietnameseChars(name).ToLower();
            idCardNumber = StringExtension.StripVietnameseChars(idCardNumber).ToLower();
            phoneNumber = StringExtension.StripVietnameseChars(phoneNumber).ToLower();
            email = StringExtension.StripVietnameseChars(email).ToLower();
            address = StringExtension.StripVietnameseChars(address).ToLower();

            Expression<Func<Citizen, bool>> specCitizen = c => (
                (string.IsNullOrEmpty(idCardNumber) || c.IdentityCard.ToLower().Contains(idCardNumber.ToLower()))
                && (string.IsNullOrEmpty(phoneNumber) || c.Phone.ToLower().Contains(phoneNumber.ToLower()))
                && (string.IsNullOrEmpty(email) || c.Email.ToLower().Contains(email.ToLower())));

            // &&(string.IsNullOrEmpty(address) || c.Address.ToLower().Contains(address.ToLower())));
            var citizens = _citizenRepository.Raw.Where(specCitizen).ToList();

            var results = citizens.Where(c =>
                (string.IsNullOrEmpty(name) || StringExtension.StripVietnameseChars(c.CitizenName).ToLower().Contains(name))
                && (string.IsNullOrEmpty(address) || StringExtension.StripVietnameseChars(c.Address).ToLower().Contains(address)));

            return results;
        }

        /// <summary>
        /// lấy ra user dựa vào account
        /// </summary>
        /// <param name="account">account</param>
        /// <returns>entity user</returns>
        public Citizen GetByAccount(string account)
        {
            Citizen citizen = null;
            if (account != null)
            {
                citizen = _citizenRepository.Get(false, u => u.Account == account);
            }
            return citizen;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Citizen GetByAccount(string account, string password)
        {
            var model = _citizenRepository.Get(false, u => u.Account == account && u.IsActivated == true);
            var citizen = new Citizen();
            if (model == null)
            {
                citizen = null;
            }
            else
            {
                var inputPwdHash = Generate.GetInputPasswordHash(password, model.PasswordSalt);
                if (model.PasswordHash.SequenceEqual(inputPwdHash))
                {
                    //    model.Password = password;
                    //   model.Role = "User";
                    citizen = model;
                }
            }
            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="fullName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="idCardNumber"></param>
        /// <param name="dateOfIssue"></param>
        /// <param name="placeOfIssue"></param>
        /// <param name="isActivated"></param>
        /// <returns></returns>
        public string UserUpdate(string account, string password, string fullName, string firstName,
                                string lastName, string phoneNumber, string email, string idCardNumber,
                                 DateTime? dateOfIssue, string placeOfIssue, bool isActivated)
        {
            var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var hash = Generate.GetInputPasswordHash(password, salt);
            var citizen = _citizenRepository.Get(false, p => p.Account == account);
            if (citizen == null)
            {
                return "false";
            }
            else
            {
                citizen.IsActivated = isActivated;
                citizen.Account = account;
                citizen.FirstName = firstName;
                citizen.LastName = citizen.LastName;
                citizen.CitizenName = fullName;
                citizen.Phone = phoneNumber;
                citizen.Email = email;
                citizen.IdentityCard = idCardNumber;
                citizen.DateOfIssue = dateOfIssue;
                citizen.PlaceOfIssue = placeOfIssue;
                citizen.PasswordHash = hash;
                citizen.PasswordSalt = salt;
                citizen.IsActivated = isActivated;
                Context.SaveChanges();
                return "true";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReadOnly"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public Citizen Get(bool isReadOnly, Expression<Func<Citizen, bool>> spec)
        {
            return _citizenRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="fullName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="idCardNumber"></param>
        /// <param name="dateOfIssue"></param>
        /// <param name="placeOfIssue"></param>
        /// <param name="isActivated"></param>
        /// <returns></returns>
        public string UserCreate(string account, string password, string fullName, string firstName,
                        string lastName, string phoneNumber, string email, string idCardNumber,
                         DateTime? dateOfIssue, string placeOfIssue, bool isActivated)
        {
            var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var hash = Generate.GetInputPasswordHash(password, salt);
            var citizen = _citizenRepository.Gets(false, p => p.Email == email).ToList();
            if (citizen.Count > 0)
            {
                return "false";
            }
            else
            {
                var model = new Citizen();
                model.Account = account;
                model.FirstName = firstName;
                model.LastName = lastName;
                model.CitizenName = fullName;
                model.Phone = phoneNumber;
                model.Email = email;
                model.IdentityCard = idCardNumber;
                model.DateOfIssue = dateOfIssue;
                model.PlaceOfIssue = placeOfIssue;
                model.PasswordHash = hash;
                model.PasswordSalt = salt;
                model.IsActivated = false;
                _citizenRepository.Create(model);
                Context.SaveChanges();
                return "true";
            }
        }

        /// <summary>
        /// lấy ra user dựa vào account
        /// </summary>
        /// <param name="email">account</param>
        /// <returns>entity user</returns>
        public Citizen GetByEmail(string email)
        {
            Citizen citizen = null;
            if (email != null)
            {
                citizen = _citizenRepository.Gets(false, u => u.Email == email).FirstOrDefault();
            }
            return citizen;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int GetUserIdByAccount(string account)
        {
            var citizen = _citizenRepository.Get(true, p => p.Account == account);
            return citizen == null ? 0 : citizen.Id;
        }

        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="citizen">người dùng</param>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// <returns>true nếu cập nhật thành công và ngược lại</returns>
        public void ResetPassword(Citizen citizen, string newPassword)
        {
            if (citizen == null)
            {
                throw new Exception("citizen is null");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new Exception("newPassword is null or empty.");
            }

            var newSalt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var newHash = Generate.GetInputPasswordHash(newPassword, newSalt);
            citizen.PasswordSalt = newSalt;
            citizen.PasswordHash = newHash;
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="citizen"></param>
        public void Delete(Citizen citizen)
        {
            if (citizen == null)
            {
                throw new ArgumentNullException("citizen");
            }
            _citizenRepository.Delete(citizen);
            Context.SaveChanges();
        }
    }
}