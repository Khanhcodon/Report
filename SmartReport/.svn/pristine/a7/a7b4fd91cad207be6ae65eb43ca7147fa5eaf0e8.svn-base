using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class SyncDocTypeBll : ServiceBase
    {
        private readonly IRepository<SyncDocType> _syncDoctypeRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        public SyncDocTypeBll(IDbCustomerContext context)
            : base(context)
        {
            _syncDoctypeRepository = Context.GetRepository<SyncDocType>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public void Create(SyncDocType s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("SyncDoctype");
            }

            _syncDoctypeRepository.Create(s);
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncDoctypes"></param>
        public void Create(ICollection<SyncDocType> syncDoctypes)
        {
            if (syncDoctypes == null || !syncDoctypes.Any())
            {
                throw new ArgumentNullException("SyncDoctypes");
            }
            foreach (var s in syncDoctypes)
            {
                var existDocType = _syncDoctypeRepository.Get(false, d => d.OutsideDocTypeId == s.OutsideDocTypeId);
                if (existDocType != null)
                {
                    existDocType.InsideDocTypeId = s.InsideDocTypeId;
                    Update(existDocType);
                    continue;
                }
                Create(s);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<SyncDocType> Gets(Expression<Func<SyncDocType, bool>> spec = null)
        {
            return _syncDoctypeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SyncDocType Get(int id)
        {
            return _syncDoctypeRepository.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public void Update(SyncDocType s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("SyncDoctype");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncDoctypes"></param>
        public void Update(IEnumerable<SyncDocType> syncDoctypes)
        {
            if (syncDoctypes == null || !syncDoctypes.Any())
            {
                throw new ArgumentNullException("SyncDoctypes");
            }

            Context.SaveChanges();
        }
    }
}
