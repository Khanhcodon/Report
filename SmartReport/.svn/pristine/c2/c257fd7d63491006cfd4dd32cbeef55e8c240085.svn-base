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
    /// Class : DocTimelineDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocTimelineDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Modify Date: 190213
    /// Modifier: GiangPN
    /// Description : DAL tương ứng với bảng DocTimeline trong CSDL
    /// </summary>
    public class DocTimelineDal : DataAccessBase, IDocTimelineDal
    {
        private readonly IRepository<DocTimeline> _docTimelineRepository;
        /// <summary>
        /// Khởi tạo class <see cref="DocTimelineDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocTimelineDal(IDbCustomerContext context) : base(context)
        {
            _docTimelineRepository = Context.GetRepository<DocTimeline>();
        }

        #pragma warning disable 1591

        public IEnumerable<DocTimeline> Gets(Expression<Func<DocTimeline, bool>> spec = null, Func<IQueryable<DocTimeline>, IQueryable<DocTimeline>> preFilter = null, params Func<IQueryable<DocTimeline>, IQueryable<DocTimeline>>[] postFilters)
        {
            return _docTimelineRepository.Find(spec, preFilter, postFilters);
        }

        public DocTimeline Get(int id)
        {
            return _docTimelineRepository.One(l => l.DocTimelineId == id);
        }

        public DocTimeline Get(Guid documentId,int documentCopyId,int userId)
        {
            return
                _docTimelineRepository.One(
                    l => l.DocumentId == documentId && l.DocumentCopyId == documentCopyId && l.UserId == userId);
        }

        public void Create(DocTimeline docTimeline)
        {
            _docTimelineRepository.Create(docTimeline);
        }

        public void Update(DocTimeline docTimeline)
        {
            _docTimelineRepository.Update(docTimeline);
        }

        public void Delete(DocTimeline docTimeline)
        {
            _docTimelineRepository.Delete(docTimeline);
        }

        public void Delete(IList<DocTimeline> docTimelines)
        {
            foreach (var docTimeline in docTimelines)
            {
                _docTimelineRepository.Delete(docTimeline, false);
            }
            Context.SaveChanges();
        }

        public bool Exist(Expression<Func<DocTimeline, bool>> spec)
        {
            return _docTimelineRepository.Any(spec);
        }

        public int Count(Expression<Func<DocTimeline, bool>> spec = null)
        {
            return _docTimelineRepository.Count(spec);
        }
    }
}
