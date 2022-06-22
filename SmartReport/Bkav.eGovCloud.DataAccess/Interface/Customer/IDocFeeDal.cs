using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : IDocFeeDal - public - Interface </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý lệ phí của hồ sơ, vb </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    public interface IDocFeeDal
    {
        /// <summary>
        /// Cập nhật lệ phí của hồ sơ.<br/>
        /// (TienBV@bkav.com - 200213)
        /// </summary>
        void Update(DocFee obj);

        /// <summary>
        /// Xóa lệ phí của hồ sơ.<br/>
        /// (TienBV@bkav.com - 200213)
        /// </summary>
        void Delete(DocFee obj);

        /// <summary>
        /// Thêm lệ phí cho giấy tờ.<br/>
        /// (TienBV@bkav.com - 200213)
        /// </summary>
        void Create(DocFee fee);

        /// <summary>
        /// Trả về lệ phí của hồ sơ theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DocFee Get(int id);
    }
}
