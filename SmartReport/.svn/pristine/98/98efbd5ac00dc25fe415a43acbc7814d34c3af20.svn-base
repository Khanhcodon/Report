using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Class này dùng để quản lý OTP
    /// </summary>
    public class OtpBll : ServiceBase
    {
        private readonly IRepository<Otp> _otpRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public OtpBll(IDbCustomerContext context)
            : base(context)
        {
            _otpRepository = Context.GetRepository<Otp>();
        }

        /// <summary>
        /// Tạo mới 1 otp
        /// </summary>
        /// <param name="otp"></param>
        public void CreateOTP(Otp otp)
        {
            if (otp == null)
            {
                throw new ArgumentNullException("otp");
            }

            _otpRepository.Create(otp);

            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra xem user đã được kích hoạt chưa
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool CheckActivedOTP(string phone, int userid)
        {
            var userActived = _otpRepository.Gets(true, o => o.UserId == userid && o.Sms == phone && o.Status == true).FirstOrDefault();
            if (userActived != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra xem user đã được insert vào otp chưa
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool CheckExistOtp(int userid)
        {
            var userActived = _otpRepository.Get(true, o => o.UserId == userid);
            if (userActived != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lấy otp theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Otp Get(int id)
        {
            return _otpRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách otp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Otp> Gets(int userId)
        {
            if (userId == 0)
            {
                return new List<Otp>();
            }

            return _otpRepository.GetsReadOnly(o => o.UserId == userId);
        }

        /// <summary>
        /// Lấy otp theo userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Otp GetByUserId(int userId)
        {
            if (userId == 0)
            {
                return null;
            }
            return _otpRepository.Get(true, o => o.UserId == userId);

        }

        /// <summary>
        /// Trả về xác nhận của user theo email.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Otp Get(int userId, string email)
        {
            if (userId == 0 || string.IsNullOrEmpty(email))
            {
                return null;
            }

            try
            {
                return _otpRepository.Get(false, o => o.UserId == userId && o.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về xác nhận của user theo sms
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Otp GetFromPhone(int userId, string phone)
        {
            if(userId == 0 || string.IsNullOrEmpty(phone))
            {
                return null;
            }
            try
            {
                return _otpRepository.Get(false, o => o.UserId == userId && o.Sms.Equals(phone));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Check xem người dùng có nhập đúng mã code không
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email">Email can update</param>
        /// <param name="sms">Sms can update</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckActivedCodeValidate(int userId, string code, string email, string sms)
        {
            var userActive = _otpRepository.Get(false, o => o.UserId == userId && o.ActivedCode == code);
            if (userActive != null && userActive.Status == false)
            {
                userActive.Status = true;
                if (email != null)
                {
                    userActive.Email = email;
                }
                if (sms != null)
                {
                    userActive.Sms = sms;
                }
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra xem mã code còn thời gian để kích hoạt không
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsTimeActive(int userId, string code)
        {
            var userActive = _otpRepository.Get(true, o => o.UserId == userId && o.ActivedCode == code);
            var now = DateTime.Now;
            if (userActive != null)
            {
                if (now < userActive.DateLimit)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update mã code mới
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public void UpdateCode(int userId, string code, int time)
        {
            var userActive = _otpRepository.Get(false, o => o.UserId == userId);
            if (userActive != null)
            {
                userActive.ActivedCode = code;
                userActive.Status = false;
                userActive.DateCreated = DateTime.Now;
                TimeSpan timeLimit = new TimeSpan(0, time, 0);
                userActive.DateLimit = userActive.DateCreated.Add(timeLimit);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Cập nhật email và sdt nếu cần
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        public void UpdateEmailAndPhone(int userId, string email, string phone)
        {
            var user = _otpRepository.Get(false, o => o.UserId == userId);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    user.Email = email;
                }
                if (!string.IsNullOrEmpty(phone))
                {
                    user.Sms = phone;
                }
                user.Status = true;
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Kiểm tra xem email đã được active chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailActive(int userId, string email)
        {
            var userActive = _otpRepository.Get(true, o => o.UserId == userId && o.Email == email);
            if (userActive != null && userActive.Status == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra xem số điện thoại đã được kích hoạt chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sms"></param>
        /// <returns></returns>
        public bool IsSmsActive(int userId, string sms)
        {
            var userActive = _otpRepository.Get(true, o => o.UserId == userId && o.Sms == sms);
            if (userActive != null && userActive.Status)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sinh random 6 ký tự
        /// </summary>
        /// <returns></returns>
        public string GenerateOTP(bool isString)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string characters = numbers;
            if (isString)
            {
                characters += alphabets + small_alphabets + numbers;
            }

            int length = 6;//Ma OTP trả về có 6 ký tự
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);

                otp += character;
            }
            return otp;
        }

        /// <summary>
        /// Cập nhật otp
        /// </summary>
        /// <param name="otp"></param>
        public void Update(Otp otp)
        {
            if (otp == null)
            {
                throw new ArgumentNullException("otp");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy email kích hoạt từ user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetEmailFromUser(User user)
        {
            var otp = _otpRepository.Get(true, o => o.UserId == user.UserId);
            if (otp != null)
            {
                return otp.Email;
            }
            return "";
        }

        /// <summary>
        /// Lấy số điện thoại từ user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetPhoneFromUser(User user)
        {
            var otp = _otpRepository.Get(true, o => o.UserId == user.UserId);
            if (otp != null)
            {
                return otp.Sms;
            }
            return "";
        }
    }
}
