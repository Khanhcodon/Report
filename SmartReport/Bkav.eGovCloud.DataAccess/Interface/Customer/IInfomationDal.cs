using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;
using System.Data;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IInfomationDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Infomation trong CSDL
    /// </summary>
    public interface IInfomationDal
    {
        /// <summary>
        /// Thêm cơ quan 
        /// </summary>
        /// <param name="info">Đối tượng cơ quan</param>
        void Create(Infomation info);

        /// <summary>
        /// Thay đổi thông tin cơ quan 
        /// </summary>
        /// <param name="info">Đối tượng cơ quan</param>
        void Update(Infomation info);

        /// <summary>
        /// Trả về đối tượng cơ quan theo Id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Cơ quan tương ứng</returns>
        Infomation Get(int id);

        /// <summary>
        /// Trả về danh sách tất cả các đối tượng cơ quan 
        /// </summary>
        /// <returns>Danh sách các đối tượng cơ quan</returns>
        IEnumerable<Infomation> Gets();

        /// <summary>
        /// Trả về bảng chứa 1 đối tươngInfomation
        /// </summary>
        /// <returns></returns>
        DataTable GetOne();
    }
}
