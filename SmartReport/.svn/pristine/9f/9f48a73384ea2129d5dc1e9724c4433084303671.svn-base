
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : MobileDeviceBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 18102016
    /// Author      : TrinhNVd
    /// Description : BLL tương ứng với bảng MobileDevice trong CSDL
    /// </summary>
    public class MobileDeviceBll : ServiceBase
    {
        private readonly IRepository<MobileDevice> _mobiledevicesRepository;
        private readonly MemoryCacheManager _cacheManager;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resourceService"></param>
        /// <param name="cacheManager"></param>
        public MobileDeviceBll(IDbCustomerContext context, ResourceBll resourceService, MemoryCacheManager cacheManager)
            : base(context)
        {
            _mobiledevicesRepository = Context.GetRepository<MobileDevice>();
            _resourceService = resourceService;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Kiểm tra đăng nhập của người dùng trên thiết bị hiện tại.
        /// Todo: xử lý thêm mã otm cho phép người dùng nhập mã để đăng nhập luôn.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="device"></param>
        /// <param name="hasLimitDevice"></param>
        /// <param name="isMobileDevice"></param>
        /// <param name="currentToken">Token đăng nhập trước đó</param>
        /// <returns></returns>
        /// <remarks>
        /// - Nếu chưa có thiết bị nào trong hệ thống, mặc định add luôn vào thiết bị mặc định.
        /// - Gửi mail cảnh báo nếu có thiết bị khác đăng nhập và bị chặn.
        /// - Kiểm tra theo địa chỉ Mac trước nếu có cấu hình. (do IP sẽ hạn chế trường hợp cùng mạng Lan mà kết nối ra ngoài Internet).
        /// </remarks>
        public MobileDevice CheckDevices(int userId, MobileDevice device, bool hasLimitDevice, bool isMobileDevice, string currentToken = "")
        {
            MobileDevice currentDevice = null;

            var allLoggedDevices = Gets(m => m.UserId == userId, isReadOnly: false);
            var isFirstLogin = allLoggedDevices.Count() == 0;

            if (!hasLimitDevice)
            {
                // Không giới hạn thiết bị
                currentDevice = isFirstLogin ? null : GetBySerial(device.Serial, userId);
                if (currentDevice != null)
                {
                    if (!currentDevice.IsActive)
                    {
                        currentDevice.IsActive = true;
                    }

                    Update(currentDevice);
                    return currentDevice;
                }

                device.IsActive = true;
				device.HasBlock = false;

				Create(device);

                return device;
            }
			
            if (hasLimitDevice)
            {
                currentDevice = allLoggedDevices.FirstOrDefault(d => d.Serial.Equals(currentToken) && d.UserId == userId);
            }

            if (currentDevice == null)
            {
                if (!isFirstLogin)
                {
                    device.IsActive = false;
                    device.HasBlock = true;
                }
                else
                {
                    device.IsActive = true;
                    device.HasBlock = false;
                }

                Create(device);
                return device;
            }

            if (currentDevice.HasBlock)
            {
                // Thiết bị người dùng đã khóa
                return null;
            }

            // Cập nhật trạng thái đăng nhập hiện tại cho thiết bị.
            currentDevice.LastUpdate = DateTime.Now;
            currentDevice.LoginToken = device.LoginToken;
            currentDevice.Browser = device.Browser;
            currentDevice.IsActive = true;

            // Update địa chỉ Mac
            currentDevice.Serial = device.Serial;

            // Xử lý history ở đây
            // ...

            Update(currentDevice);

            return currentDevice;
        }

        /// <summary>
        /// Set thiết bị được phép hay không được phép
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="hasBlock"></param>
        /// <param name="curentUserId">user</param>
        public string ActiveDevice(int deviceId, bool hasBlock, int? curentUserId = null)
        {
            if (deviceId == 0)
            {
                var devices = Gets(d => d.UserId == curentUserId.Value);
                foreach (var d in devices)
                {
                    Delete(d, hasSaveChange: false);
                }

                Context.SaveChanges();
                RemoveCache(curentUserId.Value);
                return "";
            }

            var device = Get(deviceId);
            if (device != null)
            {
                device.HasBlock = hasBlock;
                Context.SaveChanges();
            }

            RemoveCache(device.UserId);
            return device.Serial;
        }

        /// <summary>
        /// Tạo mới đối tượng gủi MobileDevice 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(MobileDevice entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("MobileDevice in null.");
            }

            if (_mobiledevicesRepository.Exist(m => m.UserId == entity.UserId && m.Serial == entity.Serial))
            {
                return;
            }

            try
            {
                _mobiledevicesRepository.Create(entity);
                Context.SaveChanges();

                RemoveCache(entity.UserId);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Cập nhật đối tượng MobileDevice
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MobileDevice entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("MobileDevice in null.");
            }

            Context.SaveChanges();
            RemoveCache(entity.UserId);
        }

        /// <summary>
        /// Xóa 1 đối tượng MobileDevice
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hasSaveChange"></param>
        public void Delete(MobileDevice entity, bool hasSaveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("MobileDevice in null.");
            }

            _mobiledevicesRepository.Delete(entity);

            if (hasSaveChange)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Lấy đối tượng MobileDevice theo id
        /// </summary>
        /// <param name="mobileDeviceId">Id của đối tượng MobileDevice</param>
        /// <returns></returns>
        public MobileDevice Get(int mobileDeviceId)
        {
            return _mobiledevicesRepository.Get(mobileDeviceId);
        }

        /// <summary>
        /// Lấy danh sách đối tượng MobileDevice chỉ có thể đọc ghi theo điều kiện truyền vào
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IEnumerable<MobileDevice> Gets(Expression<Func<MobileDevice, bool>> spec = null, bool isReadOnly = true)
        {
            return _mobiledevicesRepository.Gets(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy danh sách thiết bị của user tương ứng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<MobileDevice> Gets(int userId)
        {
            return _mobiledevicesRepository.Gets(true, x => x.UserId == userId);
        }

        /// <summary>
        /// Lấy danh sách thiết bị của user tương ứng, kết quả được lưu cache lại
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<MobileDevice> GetsFromCache(int userId, bool isActive = true)
        {
            var cacheKey = string.Format(CacheParam.MobiDevicesCache, userId);

            var mobidevices = _cacheManager.Get(cacheKey,
                                        CacheParam.MobiDevicesCacheTimeOut,
                                        () => _mobiledevicesRepository.GetsReadOnly(d => d.UserId == userId));

            if (isActive)
            {
                mobidevices = mobidevices.Where(m => m.IsActive).ToList();
            }

            return mobidevices;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MobileDevice GetBySerial(string serial, int userId, bool isReadOnly = true)
        {
            return _mobiledevicesRepository.Get(isReadOnly, x => x.Serial == serial && x.UserId == userId);
        }
		
        private void RemoveCache(int userId)
        {
            var cacheKey = string.Format(CacheParam.MobiDevicesCache, userId);
            _cacheManager.Remove(cacheKey);
        }

        /// <summary>
        /// Đăng xuất khỏi thiết bị
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mac"></param>
        public void LogOut(int userId, string mac)
        {
            var device = _mobiledevicesRepository.Gets(false, d => d.UserId == userId && d.Serial == mac);
            if (device != null && device.Any())
            {
                foreach (var d in device)
                {
                    d.IsActive = false;
                }
                SaveChanges();
            }

            RemoveCache(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveAll(int userId)
        {
            var devices = _mobiledevicesRepository.Gets(false, x => x.UserId == userId);
            foreach (var device in devices)
            {
                Delete(device, false);
            }

            SaveChanges();
        }
    }
}
