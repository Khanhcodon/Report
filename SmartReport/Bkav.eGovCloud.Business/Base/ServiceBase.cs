using System;
using Bkav.eGovCloud.DataAccess;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DataAccessBase - public - BLL
    /// Access Modifiers:
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Lớp base cho tất cả các Bll
    /// NOTE: gọi Context.SaveChanges() sau khi thay đổi dữ liệu và muốn lưu vào database
    /// </summary>
    public abstract class ServiceBase : IDisposable
    {
#pragma warning disable 1591

        protected IDbContext Context;

        /// <summary>
        /// Khởi tạo class<see cref="ServiceBase"/>
        /// </summary>
        /// <param name="context">Context.</param>
        protected ServiceBase(IDbContext context)
        {
            Context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
