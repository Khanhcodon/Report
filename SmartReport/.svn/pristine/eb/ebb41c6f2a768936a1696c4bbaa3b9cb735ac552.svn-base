using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IResourceDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Resource trong CSDL
    /// </summary>
    public class ResourceDal : DataAccessBase, IResourceDal
    {
        private readonly IRepository<Resource> _resourceRepository;

        /// <summary>
        /// Khởi tạo class <see cref="ResourceDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public ResourceDal(IDbAdminContext context)
            : base(context)
        {
            _resourceRepository = context.GetRepository<Resource>();
        }

        /// <summary>
        /// Khởi tạo class <see cref="ResourceDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public ResourceDal(IDbCustomerContext context)
            : base(context)
        {
            _resourceRepository = context.GetRepository<Resource>();
        }

        #pragma warning disable 1591

        public IEnumerable<Resource> Gets(Expression<Func<Resource, bool>> spec = null,
                                            Func<IQueryable<Resource>, IQueryable<Resource>> preFilter = null,
                                            params Func<IQueryable<Resource>, IQueryable<Resource>>[] postFilters)
        {
            return _resourceRepository.Find(spec, preFilter, postFilters);
        }

        public Resource Get(int id)
        {
            return _resourceRepository.One(r => r.ResourceId == id);
        }

        public Resource Get(string resourceKey)
        {
            return _resourceRepository.One(r => r.ResourceKey == resourceKey);
        }

        public void Create(Resource resource)
        {
            _resourceRepository.Create(resource);
        }

        public void Create(IEnumerable<Resource> resources)
        {
            foreach (var resource in resources)
            {
                _resourceRepository.Create(resource, false);
            }
            Context.SaveChanges();
        }

        public void Update(Resource resource)
        {
            _resourceRepository.Update(resource);
        }

        public void Delete(Resource resource)
        {
            _resourceRepository.Delete(resource);
        }

        public bool Exist(Expression<Func<Resource, bool>> spec)
        {
            return _resourceRepository.Any(spec);
        }

        public int Count(Expression<Func<Resource, bool>> spec = null)
        {
            return _resourceRepository.Count(spec);
        }
    }
}
