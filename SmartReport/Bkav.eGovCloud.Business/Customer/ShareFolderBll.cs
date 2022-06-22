using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : ShareFolderBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : HopCV@bkav.com </para>
    /// </author>
    /// <summary>
    /// </summary>
    public class ShareFolderBll : ServiceBase
    {
        private readonly IRepository<ShareFolder> _shareFolderRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        public ShareFolderBll(IDbCustomerContext context)
            : base(context)
        {
            _shareFolderRepository = Context.GetRepository<ShareFolder>();
        }

        /// <summary>
        ///  Tạo mới cấu hình backup database
        /// </summary>
        /// <param name="shareFolder"></param>
        public void Create(ShareFolder shareFolder)
        {
            if (shareFolder == null)
            {
                throw new ArgumentNullException("shareFolder");
            }

            _shareFolderRepository.Create(shareFolder);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa cáu hình backup
        /// </summary>
        /// <param name="shareFolder"></param>
        public void Delete(ShareFolder shareFolder)
        {
            if (shareFolder == null)
            {
                throw new ArgumentNullException("shareFolder");
            }

            _shareFolderRepository.Delete(shareFolder);
            Context.SaveChanges();
        }

        /// <summary>
        ///Cập nhật đối tượng backup
        /// </summary>
        /// <param name="shareFolder"></param>
        public void Update(ShareFolder shareFolder)
        {
            if (shareFolder == null)
            {
                throw new ArgumentNullException("shareFolder");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng backup
        /// </summary>
        /// <param name="ShareFolderId"></param>
        /// <returns></returns>
        public ShareFolder Get(int ShareFolderId)
        {
            return _shareFolderRepository.Get(false, p => p.ShareFolderId == ShareFolderId);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ShareFolder> GetsReadOnly(Expression<Func<ShareFolder, bool>> spec = null)
        {
            return _shareFolderRepository.GetsReadOnly(spec);
        }
    }
}
