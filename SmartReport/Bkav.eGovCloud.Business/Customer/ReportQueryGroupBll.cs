using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Entities.Common;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Business.BI.ParseQuery;
using System.Data;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// </summary>
    public class ReportQueryGroupBll : ServiceBase
    {
        private readonly IRepository<ReportQueryGroup> _reportQueryGroupRepository;
        private readonly IRepository<ReportQueryGroupReportQuery> _reportQueryGroupReportQueryRepository;
        private readonly IRepository<ReportQuery> _reportQueryRepository;
        private readonly IRepository<ReportQueryFilter> _reportQueryFilterRepository;

        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="generalSettings"></param>
        public ReportQueryGroupBll(IDbCustomerContext context,
                    AdminGeneralSettings generalSettings)
            : base(context)
        {
            _reportQueryGroupReportQueryRepository = Context.GetRepository<ReportQueryGroupReportQuery>();
            _reportQueryGroupRepository = Context.GetRepository<ReportQueryGroup>();
            _reportQueryRepository = Context.GetRepository<ReportQuery>();
            _reportQueryFilterRepository = Context.GetRepository<ReportQueryFilter>();
            _generalSettings = generalSettings;
        }

        #region ReportQueryGroup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportQueryId"></param>
        /// <returns></returns>
        public ReportQueryGroup Get(int reportQueryGroupId)
        {
            ReportQueryGroup reportQuery = null;
            if (reportQueryGroupId > 0)
            {
                reportQuery = _reportQueryGroupRepository.Get(reportQueryGroupId);
            }

            return reportQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportQueryGroup"></param>
        public void Create(ReportQueryGroup reportQueryGroup, List<int> reportQueryIds)
        {
            if (reportQueryGroup == null)
            {
                throw new ArgumentNullException("ReportQueryGroup");
            }

            var querys = new List<ReportQueryGroupReportQuery>();
            foreach (var reportQueryId in reportQueryIds)
            {
                querys.Add(new ReportQueryGroupReportQuery { ReportQueryId = reportQueryId });
            }

            reportQueryGroup.Querys = querys;

            _reportQueryGroupRepository.Create(reportQueryGroup);
            Context.SaveChanges();
        }

        public void Update(ReportQueryGroup reportQueryGroup, List<int> reportQueryIds)
        {
            if (reportQueryGroup == null)
            {
                throw new ArgumentNullException("ReportQueryGroup");
            }

            var deletedQuerys = _reportQueryGroupReportQueryRepository.Gets(true, p => p.ReportQueryGroupId == reportQueryGroup.ReportQueryGroupId);
            foreach (var deletedQuery in deletedQuerys)
            {
                _reportQueryGroupReportQueryRepository.Delete(deletedQuery);
            }

            var addedQuerys = new List<ReportQueryGroupReportQuery>();
            if (reportQueryIds != null)
            {
                foreach (var reportQueryId in reportQueryIds)
                {
                    addedQuerys.Add(new ReportQueryGroupReportQuery { ReportQueryId = reportQueryId });
                }

                reportQueryGroup.Querys = addedQuerys;
            }
            
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportQueryGroup"></param>
        public void Delete(ReportQueryGroup reportQueryGroup)
        {
            if (reportQueryGroup == null)
            {
                throw new ArgumentNullException("ReportQueryGroup");
            }

            var querys = new List<ReportQueryGroupReportQuery>();
            foreach (var query in reportQueryGroup.Querys)
            {
                querys.Add(query);
            }

            foreach (var query in querys)
            {
                _reportQueryGroupReportQueryRepository.Delete(query);
            }

            _reportQueryGroupRepository.Delete(reportQueryGroup);

            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ReportQueryGroup> Gets(Expression<Func<ReportQueryGroup, bool>> spec = null)
        {
            return _reportQueryGroupRepository.GetsReadOnly(spec);
        }

        public List<ReportQuery> GetReportQuerys(List<ReportQueryGroupReportQuery> reportQueryGroupReportQueries)
        {
            var reportQuerys = new List<ReportQuery>();
            foreach (var item in reportQueryGroupReportQueries)
            {
                reportQuerys.Add(_reportQueryRepository.Get(item.ReportQueryId));
            }

            return reportQuerys;
        }

        public IEnumerable<ReportQueryGroupReportQuery> GetReportQuerys(Expression<Func<ReportQueryGroupReportQuery, bool>> spec = null)
        {
            return _reportQueryGroupReportQueryRepository.GetsReadOnly(spec);
        }
        #endregion ReportQueryGroup
    }
}
