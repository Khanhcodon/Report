using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
using System.Data;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IReportDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng Report - quản lý các báo cáo
    /// </summary>
    public interface IReportDal
    {
        /// <summary>
        /// Trả về danh sách tất cả các nhóm báo cáo hiện có theo điều kiện kỹ thuật truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện kỹ thuật</param>
        /// <returns></returns>
        IEnumerable<Report> Gets(Expression<Func<Report, bool>> spec = null);

        /// <summary>
        /// Trả về báo cáo theo id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        Report Get(int id);

        /// <summary>
        /// Tạo mới báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        void Create(Report entity);

        /// <summary>
        /// Cập nhật báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(Report entity);

        /// <summary>
        /// Xóa nhóm báo cáo
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(Report entity);

        /// <summary>
        /// Trả về dánh sách dữ liệu cho báo cáo theo câu truy vấn được cấu hình
        /// </summary>
        /// <param name="query">Report</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetData(string query, params object[] parameters);

        /// <summary>
        /// Trả về danh sách các group và số lượng report trong group
        /// </summary>
        /// <param name="query">Câu truy vấn lấy dữ liệu group của báo cáo</param>
        /// <param name="parameters">Danh sách các parameters</param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetGroups(string query, params object[] parameters);

        /// <summary>
        /// Trả về tổng số record của báo cáo từ sql
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int GetTotal(string query, params object[] parameters);

        /// <summary>
        /// Trả về dữ liệu báo cáo cho crystal
        /// </summary>
        /// <param name="query">Sql query</param>
        /// <param name="parameters">Sql parameter</param>
        /// <returns>Dữ liệu dạng table</returns>
        DataTable GetDataForCrystal(string query, object[] parameters);
    }
}
