using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Utils
{
    /// <summary>
    /// Đối tượng xử lý đưa các công việc cần thực hiện ngầm vào hàng đợi
    /// </summary>
    public class QueueJobHelper
    {
        private string _queueCacheKey = CacheParam.QueueNameKey;
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="cache"></param>
        public QueueJobHelper(MemoryCacheManager cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Thêm đối tượng vào queue
        /// </summary>
        /// <param name="message"></param>
        public void EnQueue(QueueMessage message)
        {
            var cached = GetQueue();
            message.DateCreated = DateTime.Now;
            cached.Add(message);

            _cache.Set(_queueCacheKey, cached, CacheParam.QueueNameCacheTimeOut);
        }

        /// <summary>
        /// Bỏ đối tượng khỏi Queue
        /// </summary>
        /// <param name="message"></param>
        public void DeQueue(QueueMessage message)
        {

        }

        private List<QueueMessage> GetQueue()
        {
            return _cache.Get<List<QueueMessage>>(_queueCacheKey, () =>
            {
                return new List<QueueMessage>();
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueueMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool FailCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QueueMessageType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Data { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum QueueMessageType
    {
        /// <summary>
        /// 
        /// </summary>
        SendNotification,

        /// <summary>
        /// 
        /// </summary>
        SendEmail,

        /// <summary>
        /// 
        /// </summary>
        SendSms,

        /// <summary>
        /// 
        /// </summary>
        LogActivity
    }
}
