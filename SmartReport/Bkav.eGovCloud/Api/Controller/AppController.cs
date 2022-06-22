using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Logging;

namespace Bkav.eGovCloud.Api.Controller
{
    public class AppController : EgovApiBaseController
    {
        private readonly UserBll _userService;
        private readonly MobileDeviceBll _mobileDeviceService;

        public AppController()
        {
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _mobileDeviceService = DependencyResolver.Current.GetService<MobileDeviceBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public string SuccessCheck(int a, int b)
        {
            return (a * a + 5 * b).ToString();
        }

        [System.Web.Http.HttpGet]
        public bool UpdateDeviceToken(string account, string token, string devicename, string serial, string osversion, bool isAndroidOS = true)
        {
            var result = false;
            User user;

            if (!string.IsNullOrWhiteSpace(account))
            {
                user = _userService.GetByUserName(account, true);
            }
            else
            {
                user = _userService.CurrentEditableUser;
            }
            var os = isAndroidOS ? DeviceOS.Android : DeviceOS.IOS;
            if (user != null)
            {
                try
                {
                    var device = _mobileDeviceService.GetBySerial(serial, user.UserId, false);

                    if (device == null)
                    {
                        device = new MobileDevice
                        {
                            OS = (int)os,
                            Serial = serial,
                            UserId = user.UserId,
                            Token = token,
                            DeviceName = devicename,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };

                        _mobileDeviceService.Create(device);
                    }
                    else
                    {
                        device.Token = token;
                        device.LastUpdate = DateTime.Now;
                        device.UserId = user.UserId;
                        device.DeviceName = devicename;
                        device.IsActive = true;
                        _mobileDeviceService.Update(device);
                    }
                    result = true;
                }
                catch (Exception)
                {
                }
            }

            return result;

        }
    }
}