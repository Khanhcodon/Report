using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportGroupDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng Report - quản lý các báo cáo
    /// </summary>
    public class ReportDal : DataAccessBase, IReportDal
    {
        private readonly IRepository<Report> _reportRepository;

        /// <summary>
        /// Khởi tạo class <see cref="ReportGroupDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public ReportDal(IDbCustomerContext context)
            : base(context)
        {
            _reportRepository = Context.GetRepository<Report>();
        }

#pragma warning disable 1591

        public IEnumerable<Report> Gets(System.Linq.Expressions.Expression<Func<Report, bool>> spec = null)
        {
            return _reportRepository.Find(spec);
        }

        public Report Get(int id)
        {
            return _reportRepository.One(id);
        }

        public void Create(Report entity)
        {
            _reportRepository.Create(entity);
        }

        public void Update(Report entity)
        {
            _reportRepository.Update(entity);
        }

        public void Delete(Report entity)
        {
            _reportRepository.Delete(entity);
        }

        public DataTable GetDataForCrystal(string query, params object[] parameters)
        {
            var result = Context.RawTable(query, parameters);
            return result;
        }

        public IEnumerable<IDictionary<string, object>> GetData(string query, params object[] parameters)
        {
            var result = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        public IEnumerable<IDictionary<string, object>> GetGroups(string query, params object[] parameters)
        {
            return Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
        }

        public int GetTotal(string query, params object[] parameters)
        {
            int total;
            int.TryParse((Context.RawScalar(query, parameters)).ToString(), out total);
            return total;
        }
    }
}
