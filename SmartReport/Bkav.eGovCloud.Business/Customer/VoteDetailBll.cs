using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : WardBll - public - BLL</para>
    /// <para>Create Date : 171013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng Ward trong CSDL</para>
    /// </summary>
    public class VoteDetailBll : ServiceBase
    {
        private readonly IRepository<VoteDetail> _voteDetailRepository;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// Khởi tạo class <see cref="WardBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public VoteDetailBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _voteDetailRepository = Context.GetRepository<VoteDetail>();
            _generalSettings = generalSettings;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="voteDetailId"></param>
      /// <returns></returns>
        public VoteDetail Get(int voteDetailId)
        {
            return _voteDetailRepository.Get(false, v => v.VoteDetailId == voteDetailId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        public IEnumerable<VoteDetail> Gets(int voteId)
        {
            return _voteDetailRepository.Gets(true, v => v.VoteId == voteId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        public void DeleteAll(int voteId)
        {
            var voteDetails = _voteDetailRepository.Gets(false, v => v.VoteId == voteId);
            foreach (var item in voteDetails)
            {
                Delete(item);
            }
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="voteDetail"></param>
        public void Create(VoteDetail voteDetail)
        {
            if (voteDetail == null)
            {
                throw new ArgumentNullException("vote");
            }

            _voteDetailRepository.Create(voteDetail);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="voteDetail"></param>
        public void Delete(VoteDetail voteDetail)
        {
            if (voteDetail == null)
            {
                throw new ArgumentNullException("vote");
            }

            _voteDetailRepository.Delete(voteDetail);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="voteDetail"></param>
        public void Update(VoteDetail voteDetail)
        {
            if (voteDetail == null)
            {
                throw new ArgumentNullException("vote");
            }

            Context.SaveChanges();
        }
    }
}
