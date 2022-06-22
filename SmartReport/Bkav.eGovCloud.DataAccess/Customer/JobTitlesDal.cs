using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JobTitlesDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IJobTitlesDal
    /// Create Date : 131012
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng JobTitles trong CSDL
    /// </summary>
    public class JobTitlesDal : DataAccessBase, IJobTitlesDal
    {
        private readonly IRepository<JobTitles> _jobTitlesRepository;
        /// <summary>
        /// Khởi tạo class <see cref="JobTitlesDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public JobTitlesDal(IDbCustomerContext context)
            : base(context)
        {
            _jobTitlesRepository = Context.GetRepository<JobTitles>();
        }

        #pragma warning disable 1591

        public IEnumerable<JobTitles> Gets(Expression<Func<JobTitles, bool>> spec = null, Func<IQueryable<JobTitles>, IQueryable<JobTitles>> preFilter = null, params Func<IQueryable<JobTitles>, IQueryable<JobTitles>>[] postFilters)
        {
            return _jobTitlesRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<JobTitles, TOutput>> projector, Expression<Func<JobTitles, bool>> spec = null)
        {
            return _jobTitlesRepository.FindAs(projector, spec);
        }

        public JobTitles Get(int id)
        {
            return _jobTitlesRepository.One(l => l.JobTitlesId == id);
        }

        public void Create(JobTitles jobTitles)
        {
            _jobTitlesRepository.Create(jobTitles);
        }

        public void Update(JobTitles jobTitles)
        {
            _jobTitlesRepository.Update(jobTitles);
        }

        public void Delete(JobTitles jobTitles)
        {
            _jobTitlesRepository.Delete(jobTitles);
        }

        public void Delete(IEnumerable<JobTitles> jobTitless)
        {
            foreach (var jobTitles in jobTitless)
            {
                _jobTitlesRepository.Delete(jobTitles, false);
            }
            Context.SaveChanges();
        }
        public bool Exist(Expression<Func<JobTitles, bool>> spec)
        {
            return _jobTitlesRepository.Any(spec);
        }
        public int Count(Expression<Func<JobTitles, bool>> spec = null)
        {
            return _jobTitlesRepository.Count(spec);
        }
    }
}
