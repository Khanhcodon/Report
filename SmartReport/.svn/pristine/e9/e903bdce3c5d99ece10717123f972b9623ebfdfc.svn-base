using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Microsoft.AspNet.SignalR;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : NotificationHubs - public - BLL
    /// Access Modifiers: 
    /// Create Date : 300115
    /// Author      : HopCV
    /// Description : BLL thao tác với signalr tạo công kết nối server ==> client, client ==> server
    /// </summary>
    public class NotificationHubs : Hub
    {
        private readonly UserConnectionBll _userConnectionService;
        private readonly SsoSettings _ssoSettings;

        /// <summary>
        /// 
        /// </summary>
        public NotificationHubs()
        {
            _userConnectionService = DependencyResolver.Current.GetService<UserConnectionBll>();
            _ssoSettings = SsoSettings.Instance;
        }

        /// <summary>
        /// Kết nối đến server
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            var userId = Context.User.GetUserId();

            _userConnectionService.DeleteAllAndCreateNewConnection(
                  userId,
                  new UserConnection
                  {
                      UserConnectionId = Context.ConnectionId,
                      UserId = userId,
                      DateCreated = DateTime.Now
                  });

            return base.OnConnected();
        }

        /// <summary>
        /// Hủy kết nối đến server
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            var connectionId = Context.ConnectionId;
            _userConnectionService.Delete(connectionId);

            return base.OnDisconnected();
        }

        /// <summary>
        /// Kết nối lại đến server
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            var dateTime = DateTime.Now;
            var userId = Context.User.GetUserId();
            var userConnection = _userConnectionService.Get(userId, Context.ConnectionId);
            if (userConnection == null)
            {
                _userConnectionService.Create(new UserConnection
                {
                    UserConnectionId = Context.ConnectionId,
                    UserId = userId,
                    DateCreated = dateTime
                });
            }
            else
            {
                _userConnectionService.UpdateDate(userConnection);
            }

            return base.OnReconnected();
        }
    }
}
