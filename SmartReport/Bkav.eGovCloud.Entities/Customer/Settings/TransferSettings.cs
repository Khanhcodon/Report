using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer.eDoc;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Thiết lập văn bản liên thông
    /// </summary>
    public class TransferSettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ của service Edoc
        /// </summary>
        public string EdocServiceUrl { get; set; }

        /// <summary>
        /// Tài khoản edoc
        /// </summary>
        public string OrganId { get; set; }

        /// <summary>
        /// Mật khẩu edoc
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Thời gian lấy văn bản mới- tính bằng phút
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Thời gian kiểm tra notify, tính bằng phút
        /// </summary>
        public int TimeForCheckNotify { get; set; }

        /// <summary>
        /// Id người tiếp nhận văn bản đến
        /// </summary>
        public int UserReceiveId { get; set; }

        /// <summary>
        /// Account người tiếp nhận văn bản đến
        /// </summary>
        public string UserReceiveName { get; set; }

        /// <summary>
        /// Lấy và thiết lập danh sách người tiếp nhận hồ sơ một cửa liên thông
        /// </summary>
        public string UserReceiveHsmc { get; set; }

        /// <summary>
        /// Trả về danh sách các userid người nhận hồ sơ một cửa liên thông _ Cách cũ.
        /// </summary>
        /// <returns></returns>
        public List<int> GetUserReceiveHsmcOld()
        {
            var result = new List<int>();

            if (string.IsNullOrWhiteSpace(UserReceiveHsmc))
            {
                return result;
            }

            var userDictionary = Json2.ParseAs<List<Dictionary<int, string>>>(UserReceiveHsmc);
            if (userDictionary != null && userDictionary.Any())
            {
                foreach (var ud in userDictionary)
                {
                    result.AddRange(new List<int>(ud.Keys));
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy và thiết lập danh sách người tiếp nhận văn bản đến liên thông
        /// </summary>
        public string UserReceiveVbDen { get; set; }

        /// <summary>
        /// Trả về danh sách Id các user nhận văn bản đến liên thông _ cách cũ
        /// </summary>
        /// <returns></returns>
        public List<int> GetUserReceiveVbDenOld()
        {
            var result = new List<int>();

            if (string.IsNullOrWhiteSpace(UserReceiveVbDen))
            {
                return result;
            }

            var userDictionary = Json2.ParseAs<List<Dictionary<int, string>>>(UserReceiveVbDen);
            if (userDictionary != null && userDictionary.Any())
            {
                foreach (var ud in userDictionary)
                {
                    result.AddRange(new List<int>(ud.Keys));
                }
            }

            return result;
        }

        /// <summary>
        /// Trả về danh sách Id các user nhận hsmc liên thông
        /// </summary>
        /// <param name="eDocReceiveId">Mã định danh nhận văn bản, trường hợp = null lấy mã chính</param>
        /// <returns></returns>
        public List<UserReceivesSettings> GetUserReceiveHsmc(string eDocReceiveId = null)
        {
            var result = new List<UserReceivesSettings>();
            try
            {
                // Lấy cách cũ trước
                result.AddRange(GetUserReceiveHsmcOld().Select(u => new UserReceivesSettings()
                {
                    DepartmentId = 0,
                    DepartmentName = "",
                    UserId = u
                }));

                return result;
            }
            catch
            {
                result = Json2.ParseAs<IEnumerable<UserReceivesSettings>>(UserReceiveHsmc ?? "[]").ToList();
                if (!string.IsNullOrEmpty(eDocReceiveId))
                {
                    result = result.Where(u => u.EDocId.Trim().Equals(eDocReceiveId, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            return result;
        }

        /// <summary>
        /// Trả về danh sách Id các user nhận văn bản đến liên thông
        /// </summary>
        /// <param name="eDocReceiveId">Mã định danh nhận văn bản, trường hợp = null lấy mã chính</param>
        /// <returns></returns>
        public List<UserReceivesSettings> GetUserReceiveVbDen(string eDocReceiveId = null)
        {
            var result = new List<UserReceivesSettings>();
            try
            {
                // Lấy cách cũ trước
                result.AddRange(GetUserReceiveVbDenOld().Select(u => new UserReceivesSettings()
                {
                    DepartmentId = 0,
                    DepartmentName = "",
                    UserId = u
                }));

                return result;
            }
            catch
            {
                result = Json2.ParseAs<List<UserReceivesSettings>>(UserReceiveVbDen ?? "[]");
                if (!string.IsNullOrEmpty(eDocReceiveId))
                {
                    result = result.Where(u => u.EDocId.Equals(eDocReceiveId, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // result = userReceives.Select(u => u.UserId).ToList();
            }

            return result;
        }

        /// <summary>
        /// Trả về danh sách tất cả mã định danh của cơ quan hiện tại.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Organization> GetCurrents()
        {
            var userReceiveVbdens = Json2.ParseAs<IEnumerable<UserReceivesSettings>>(UserReceiveVbDen ?? "[]");
            var result = new List<Organization>();

            result.AddRange(userReceiveVbdens.Select(u => new Organization()
            {
                OrganId = u.EDocId,
                OrganName = u.DepartmentName
            }));

            // Hsmc ko lieen thoong qua tool
            // var userReceiveHsmcs = Json2.ParseAs<IEnumerable<UserReceivesSettings>>(UserReceiveHsmc ?? "[]");
            //result.AddRange(userReceiveHsmcs.Select(u => new Organization()
            //{
            //    OrganId = u.EDocId,
            //    OrganName = u.DepartmentName
            //}));

            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserReceivesSettings
    {
        /// <summary>
        /// Mã định danh
        /// </summary>
        public string EDocId { get; set; }

        /// <summary>
        /// Id người nhận
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Tên người nhận
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
    }
}