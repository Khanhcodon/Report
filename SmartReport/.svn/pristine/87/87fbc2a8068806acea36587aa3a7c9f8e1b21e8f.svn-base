using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : UserConnectionBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 200812</para>
    /// <para>Author      : Hopcv</para>
    /// <para>Description : BLL tương ứng với bảng User_Connection trong CSDL</para>
    /// </summary>
    public class UserConnectionBll
    {
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo
        /// </summary>  
        /// <param name="cache">Hệ thống cache</param>
        public UserConnectionBll(MemoryCacheManager cache)
        {
            _cache = cache;
        }

        private IEnumerable<UserConnection> GetAll()
        {
            var cached = _cache.Get<IEnumerable<UserConnection>>(CacheParam.HubConnectionCache, () =>
            {
                return new List<UserConnection>();
            }, CacheParam.HubConnectionCacheTimeout);
            return cached;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public UserConnection Get(int userId, string connectionId)
        {
            var allCached = GetAll();
            var result = allCached.SingleOrDefault(c => c.UserId == userId && c.UserConnectionId.Equals(connectionId));

            return result;
        }

        /// <summary>
        /// Tạo mơi kết nối của người dùng vào database
        /// </summary>
        /// <param name="userConnection">entities UserConnection</param>
        public void Create(UserConnection userConnection)
        {
            if (userConnection == null)
            {
                throw new ArgumentNullException("userConnection");
            }

            var allCached = GetAll();
            allCached = allCached.Concat(new List<UserConnection>() { userConnection });

			SetCache(allCached);
        }

        /// <summary>
        /// Cập nhật  kết nối của người dùng vào database
        /// </summary>
        /// <param name="userConnection">entities UserConnection</param>
        public void UpdateDate(UserConnection userConnection)
        {
            if (userConnection == null)
            {
                throw new ArgumentNullException("userConnection");
            }

            var cached = Get(userConnection.UserId, userConnection.UserConnectionId);
            if (cached != null)
            {
                cached.DateCreated = DateTime.Now;
            }
		}

        /// <summary>
        /// Xóa connection của người dùng
        /// </summary>
        /// <param name="connectionId"> entities UserConnection</param>
        public void Delete(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return;
            }

            var allCached = GetAll();
            allCached = allCached.Where(c => c.UserConnectionId != connectionId);

			SetCache(allCached);
		}

        /// <summary>
        ///   Xóa các connection cũ và tạo connection mới
        /// </summary>
        /// <param name="userId">Các connections cũ</param>
        /// <param name="newConnection">connection mới</param>
        public void DeleteAllAndCreateNewConnection(int userId, UserConnection newConnection)
        {
            if (newConnection == null)
            {
                throw new ArgumentNullException("newConnection");
            }

            var allCached = GetAll();
            if (userId > 0)
            {
                allCached = allCached.Where(c => c.UserId != userId).ToList();
            }

            allCached = allCached.Concat(new List<UserConnection>() { newConnection });
            SetCache(allCached);
        }

        /// <summary> 
        /// Lấy ra tất cả UserConnection theo điều kiện truyền vào.
        /// </summary>
        /// <param name="userId">The spec.</param>
        /// <returns></returns>
        public IEnumerable<UserConnection> Gets(int userId)
        {
            return GetAll().Where(c => c.UserId == userId);
        }

        /// <summary> 
        /// Lấy ra tất cả UserConnection theo điều kiện truyền vào.
        /// </summary>
        /// <param name="userIds">The spec.</param>
        /// <returns></returns>
        public IEnumerable<UserConnection> Gets(IEnumerable<int> userIds)
        {
            return GetAll().Where(c => userIds.Contains(c.UserId));
        }

        private void SetCache(IEnumerable<UserConnection> data)
        {
            _cache.Remove(CacheParam.HubConnectionCache);
            _cache.Set(CacheParam.HubConnectionCache, data, CacheParam.HubConnectionCacheTimeout);
        }

    }
}
