using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportRuleBll : ServiceBase
    {
        private readonly IRepository<ReportRule> _reportRuleRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DocTypeBll _doctypeservice;

        /// <summary>
        /// Khởi tạo class <see cref="ReportModeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings"></param>
        //TODO: Anh GiangPN - Tham số đầu vào IPositionDal positionDal nên thay thành PositionBll và IJobTitlesDal jobTitlesDal nên thay thành JobTitleBll
        public ReportRuleBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            DocTypeBll doctypeservice)
            : base(context)
        {
            _generalSettings = generalSettings;
            _reportRuleRepository = Context.GetRepository<ReportRule>();
            _doctypeservice = doctypeservice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ReportRule Get(string code)
        {
            return _reportRuleRepository.Get(false, c => c.Code == code);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReportRule Get(int id)
        {
            return _reportRuleRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportMode"></param>
        public ReportRule GetId(int? id)
        {
            return _reportRuleRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportMode"></param>
        public void Create(ReportRule reportMode)
        {
            if (reportMode == null)
            {
                throw new ArgumentNullException("ReportRule");
            }

            _reportRuleRepository.Create(reportMode);
            Context.SaveChanges();
        }

        public void Update(ReportRule reportMode)
        {
            if (reportMode == null)
            {
                throw new ArgumentNullException("ReportRule");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportMode"></param>
        public void Delete(ReportRule reportMode)
        {
            if (reportMode == null)
            {
                throw new ArgumentNullException("ReportRule");
            }

            _reportRuleRepository.Delete(reportMode);
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ReportRule> Gets(Expression<Func<ReportRule, bool>> spec = null)
        {
            return _reportRuleRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<ReportRule, TOutput>> projector, Expression<Func<ReportRule, bool>> spec = null)
        {
            return _reportRuleRepository.GetsAs(projector, spec);
        }
    }
}
