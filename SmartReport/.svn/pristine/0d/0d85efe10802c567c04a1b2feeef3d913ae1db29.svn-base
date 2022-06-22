using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.SmsService;
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
    public class VoteBll : ServiceBase
    {
        private readonly IRepository<Vote> _voteRepository;
        private readonly IRepository<VoteDetail> _voteDetailRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly SmsSettings _smsSettings;

        /// <summary>
        /// Khởi tạo class <see cref="WardBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="smsSettings">Cấu hình chung</param>
        public VoteBll(IDbCustomerContext context, AdminGeneralSettings generalSettings, SmsSettings smsSettings)
            : base(context)
        {
            _voteRepository = Context.GetRepository<Vote>();
            _voteDetailRepository = Context.GetRepository<VoteDetail>();
            _generalSettings = generalSettings;
            _smsSettings = smsSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="voteId"></param>
        /// <returns></returns>
        public Vote Get(int currentUserId, int voteId)
        {
            var userSearch = ";" + currentUserId.ToString() + ";";
            return _voteRepository.Get(false, v =>
                v.UsersView.Contains(userSearch) && voteId == v.VoteId ||
                v.UsersVote.Contains(userSearch) && voteId == v.VoteId 
                );
        }

        /// <summary>
        /// Hàm lấy ra các cuộc trưng cầu mà người dùng hiện tại có tham gia
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="isFinish"></param>
        /// <returns></returns>
        public IEnumerable<Vote> Gets(int currentUserId, bool? isFinish = null)
        {
            var userSearch = ";" + currentUserId.ToString() + ";";
            return _voteRepository.Gets(true, v =>
                (v.UsersView.Contains(userSearch) || v.UsersVote.Contains(userSearch))
                && (
                    (v.TimeEnd > DateTime.Now && isFinish == true) ||
                    (v.TimeEnd > DateTime.Now && isFinish == false) ||
                    (isFinish == null)
                )
                );
        }

        /// <summary>
        /// Lấy danh sách những cuộc trưng cầu mà user được xem
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="isFinish"></param>
        /// <returns></returns>
        public IEnumerable<Vote> GetsView(int currentUserId, bool? isFinish = null)
        {
            var userSearch = ";" + currentUserId.ToString() + ";";
            return _voteRepository.Gets(true, v =>
                v.UsersView.Contains(userSearch)
                && (
                    (v.TimeEnd > DateTime.Now && isFinish == true) || 
                    (v.TimeEnd > DateTime.Now && isFinish == false) || 
                    (isFinish == null) 
                ) 
                );
        }

       /// <summary>
        /// Lấy danh sách những cuộc trưng cầu mà user được vote
       /// </summary>
       /// <param name="currentUserId"></param>
       /// <param name="isFinish"></param>
       /// <returns></returns>
        public IEnumerable<Vote> GetsVote(int currentUserId, bool? isFinish = null)
        {
            var userSearch = ";" + currentUserId.ToString() + ";";
            return _voteRepository.Gets(true, v =>
                v.UsersVote.Contains(userSearch)
                && (
                    (v.TimeEnd > DateTime.Now && isFinish == true) ||
                    (v.TimeEnd > DateTime.Now && isFinish == false) ||
                    (isFinish == null)
                )
                );
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="vote"></param>
        public void Delete(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException("vote");
            }
            _voteRepository.Delete(vote);
            Context.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="vote"></param>
        public void Update(Vote vote)
        {
            if (vote== null)
            {
                throw new ArgumentNullException("vote");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="vote"></param>
        public void Create(Vote vote)
        {
            if (vote == null)
            {
                throw new ArgumentNullException("vote");
            }

            _voteRepository.Create(vote);
            //var sms = new VTDDSmsService(_smsSettings.ServiceUser,
            //            _smsSettings.ServicePass,
            //            _smsSettings.ServiceCode,
            //            _smsSettings.Alias);
            //sms.SendSms("84978020427", "test sms");
            //sms.SendSms("0984557391", "test sms");

            Context.SaveChanges();
        }
    }
}
