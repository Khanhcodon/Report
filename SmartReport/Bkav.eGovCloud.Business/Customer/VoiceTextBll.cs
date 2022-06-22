using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class VoiceTextBll : ServiceBase
    {
        private readonly IRepository<VoiceText> _voiceTextRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public VoiceTextBll(IDbCustomerContext context) : base(context)
        {
            _voiceTextRepository = context.GetRepository<VoiceText>();
        }

        /// <summary>
        /// Trả về 10 tin tức sự kiện gần nhất
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public IEnumerable<VoiceText> Gets()
        {

            return _voiceTextRepository.GetsReadOnly();
        }

        public IEnumerable<VoiceText> Get(string name)
        {
            return _voiceTextRepository.GetsReadOnly(d => d.Name.Equals(name));
        }

        /// <summary>
        /// Thêm tin tức
        /// </summary>
        /// <param name="newVoiceText"></param>
        public void Create(VoiceText newVoiceText)
        {
            _voiceTextRepository.Create(newVoiceText);
            Context.SaveChanges();
        }
    }
}