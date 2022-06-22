namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CacheParam - public - Entity
    /// Access Modifiers: 
    /// Create Date : 170912
    /// Author      : TrungVH
    /// Description : Định nghĩa các loại cache
    /// </summary>
    public static class CacheParam
    {
        #region Dùng chung

        /// <summary>
        /// Key cho cache kết nối chính tới cơ sở dữ liệu.
        /// - Với MCTT là kết nối tới egov-admin
        /// </summary>
        public const string MainConnectionString = "eGovCloud.connection";

        /// <summary>
        /// Thời gian time out của cache kết nối
        /// </summary>
        public const int MainConnectionStringCacheTimeOut = 21900;

        /// <summary>
        /// Key cho cache tất cả các resource
        /// - Với bản tập trung sẽ lấy trên resource của db chính
        /// </summary>
        public const string LocalstringresourcesAllKey = "eGovCloud.resource.all";

        /// <summary>
        /// Thời gian time out của cache tất cả các resource
        /// </summary>
        public const int LocalstringresourcesAllCacheTimeOut = 60;

        #endregion

        #region Riêng cho từng domain

        /// <summary>
        /// Key cho cache các cấu hình
        /// </summary>
        public const string SettingsAllKey = "$domain.setting.all";

        /// <summary>
        /// Thời gian timeout của cache các cấu hình
        /// </summary>
        public const int SettingsAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các quyền của tất cả các nhóm người dùng
        /// </summary>
        public const string RolePermissionAllKey = "$domain.role.permission.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các quyền của tất cả các nhóm người dùng
        /// </summary>
        public const int RolePermissionAllCacheTimeOut = 20;

        /// <summary>
        /// Key cho cache tất cả các người dùng
        /// </summary>
        public const string UserAllKey = "$domain.user.all";

        /// <summary>
        /// Key cache người dùng hiện tại
        /// </summary>
        public const string UserCurrent = "$domain.user.current.{0}";

        /// <summary>
        /// Thời gian timeout của cache người dùng hiện tại
        /// </summary>
        public const int UserCurrentCacheTimeOut = 60;

        /// <summary>
        /// Key cache  hồ sơ các nhân người dùng
        /// </summary>
        public const string PrivateStore = "$domain.privatestore.user.{0}";

        /// <summary>
        /// Thời gian timeout của cache hồ sơ các nhân người dùng
        /// </summary>
        public const int PrivateStoreCacheTimeOut = 360;

        /// <summary>
        /// Thời gian timeout của cache tất cả các người dùng
        /// </summary>
        public const int UserAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các phòng ban
        /// </summary>
        public const string DepartmentAllKey = "$domain.department.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các phòng ban
        /// </summary>
        public const int DepartmentAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các chức danh
        /// </summary>
        public const string JobtitlesAllKey = "$domain.jobtitles.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các chức danh
        /// </summary>
        public const int JobtitlesAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các chức vụ
        /// </summary>
        public const string PositionAllKey = "$domain.position.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các chức vụ
        /// </summary>
        public const int PositionAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các chức vụ
        /// </summary>
        public const string UserDepartmentJobtitlePositionAllKey = "$domain.userDepartmentJobtitlePosition.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các chức vụ
        /// </summary>
        public const int UserDepartmentJobtitlePositionAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các ngày nghỉ
        /// </summary>
        public const string HolidayAllKey = "$domain.holiday.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các ngày nghỉ
        /// </summary>
        public const int HolidayAllCacheTimeOut = 1440;

        /// <summary>
        /// Key cho cache tất cả các ngày nghỉ trong tuần
        /// </summary>
        public const string WeekendAllKey = "$domain.weekend.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả các ngày nghỉ trong tuần
        /// </summary>
        public const int WeekendAllCacheTimeOut = 1440;

        /// <summary>
        /// Key cho cache tất cả nhóm cây 
        /// </summary>
        public const string TreeGroupAllKey = "$domain.treeGroup.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả nhóm cây 
        /// </summary>
        public const int TreeGroupAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả cấu hình cây văn bản
        /// </summary>
        public const string FunctionAllKey = "$domain.function.all";

        /// <summary>
        /// Thời gian timeout của cache cấu hình cây văn bản
        /// </summary>
        public const int FunctionAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả các node cây văn bản của user
        /// </summary>
        public const string FunctionUserKey = "$domain.function.{0}.{1}";

        /// <summary>
        /// Thời gian timeout của cache cấu hình cây văn bản
        /// </summary>
        public const int FunctionUserKeyCacheTimeOut = 600;

        /// <summary>
        /// Key cho cache tất cả nhóm cây 
        /// </summary>
        public const string PermissionSettingAllKey = "$domain.permissionSetting.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả nhóm cây 
        /// </summary>
        public const int PermissionSettingAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả nhóm cây cấu hình hiển thị
        /// </summary>
        public const string DocColumnSettingAllKey = "$domain.docColumnSettingAllKey.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả cấu hình hiển thị 
        /// </summary>
        public const int DocColumnSettingKeyAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả cấu hình giao diện
        /// </summary>
        public const string InterfaceConfigAllKey = "$domain.interfaceConfigAllKey.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả giao diện
        /// </summary>
        public const int InterfaceConfigAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả cấu hình
        /// </summary>
        public const string InfomationAllKey = "$domain.infomationAllKey.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả giao diện
        /// </summary>
        public const int InfomationAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache thống kê
        /// </summary>
        public const string StatisticKey = "$domain.statistic.{0}";

        /// <summary>
        /// Thời gian timeout của thống kê
        /// </summary>
        public const int StatisticCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache thống kê
        /// </summary>
        public const string CodeUsedKey = "$domain.CodeUsed.{0}";

        /// <summary>
        /// Thời gian timeout của danh sach code đã đùng
        /// </summary>
        public const int CodeUsedCacheTimeOut = 5;
		
		/// <summary>
		/// Key cho cache bảng mã
		/// </summary>
		public const string CodeKey = "$domain.Code.{0}";

		/// <summary>
		/// Thời gian timeout của danh sach code đã đùng
		/// </summary>
		public const int CodeCacheTimeOut = 60;
		
		/// <summary>
		/// Key cho cache thống kê
		/// </summary>
		public const string ReportKey = "$domain.statistic.{0}";
        /// <summary>
        /// Key cho cache thống kê key
        /// </summary>
        public const string ReportKeyCache = "$domain.statistickey.{0}";
        /// <summary>
        /// Thời gian timeout của thống kê: nửa ngày
        /// </summary>
        public const int ReportCacheTimeOut = 10;

        /// <summary>
        /// Key cho cache thông tin khách hàng
        /// </summary>
        public const string CustomerInfoKey = "$domain.customerInfo.{0}";

        /// <summary>
        /// Thời gian timeout của cache thông tin khách hàng
        /// </summary>
        public const int CustomerInfoCacheTimeOut = 5;

        /// <summary>
        /// Key cho cache tất cả loại hồ sơ, văn bản
        /// </summary>
        public const string DocTypeAllKey = "$domain.doctypeAllKey.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả loại hồ sơ, văn bản
        /// </summary>
        public const int DocTypeAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả hình thức văn bản
        /// </summary>
        public const string CategoryAllKey = "$domain.CategoryAllKey.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả hình thức văn bản
        /// </summary>
        public const int CategoryAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả quy trình
        /// </summary>
        public const string WorkflowAllKey = "$domain.workflowAllKey.all";

        /// <summary>
        /// Thời gian timeout của cache tất cả quy trình
        /// </summary>
        public const int WorkflowAllCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache danh sách ủy quyền cho mình
        /// </summary>
        public const string AuthorizeKey = "$domain.authorize.{0}";

        /// <summary>
        /// Thời gian timeout của cache danh sách ủy quyền cho mình
        /// Lưu ý thời gian cache: thưởng thông tin ủy quyền thay đổi theo ngày, nhưng cứ đặt bé bé cho chắc :))
        /// </summary>
        public const int AuthorizeKeyCacheTimeOut = 160;

        /// <summary>
        /// Key cho cache các store
        /// </summary>
        public const string StoreAllKey = "$domain.store.all";

        /// <summary>
        /// Thời gian timeout cache store.
        /// </summary>
        public const int StoreAllCacheTimeOut = 160;

        /// <summary>
        /// Key cho cache các store
        /// </summary>
        public const string MobiDevicesCache = "$domain.mobidevice.getusers{0}";

        /// <summary>
        /// Thời gian timeout cache store.
        /// </summary>
        public const int MobiDevicesCacheTimeOut = 160;

        /// <summary>
        /// Key cho cache các văn bản đang xử lý
        /// </summary>
        public const string DocumentsCache = "$domain.documents";

        /// <summary>
        /// Thời gian timeout cache store.
        /// </summary>
        public const int DocumentsCacheTimeOut = 1600;

        /// <summary>
        /// Lưu lại tất cả hub connection của người dùng
        /// </summary>
        public const string HubConnectionCache = "$domain.hubsConnection";

        /// <summary>
        /// Thời gian timeout
        /// </summary>
        public const int HubConnectionCacheTimeout = 1600;

        /// <summary>
        /// Cache thông tin văn bản từ scan
        /// {0} tên file lưu tạm trong thư mục temp
        /// </summary>
        public const string DocumentInfoFromImageCache = "$domain.documentinfoimage.{0}";
        /// <summary>
        /// 
        /// </summary>
        public const int DocumentInfoFromImageCacheTimeout = 30;

		/// <summary>
		/// Key cho cache đơn vị phát hành văn bản
		/// </summary>
		public const string AddressCache = "$domain.address";

		/// <summary>
		/// Thời gian timeout AddressCache.
		/// </summary>
		public const int AddressCacheTimeOut = 1600;

        /// <summary>
        /// Key cho cache kỳ báo cáo
        /// </summary>
        public const string ActionLevelKey = "$domain.ActionLevel.{0}";

        /// <summary>
        /// Thời gian timeout của danh sach kỳ báo cáo đã đùng
        /// </summary>
        public const int ActionLevelCacheTimeOut = 60;

        #endregion

        #region search

        /// <summary>
        /// key Search tim kiem nhanh
        /// </summary>
        public const string QuickSearchViewKey = "$domain.quickSearchview.getusers{0}";

        /// <summary>
        /// Thời gian timeout của cache thông tin khách hàng
        /// </summary>
        public const int QuickSearchViewCacheTimeOut = 15;

        /// <summary>
        /// key Search tim kiem nang cao
        /// </summary>
        public const string SearchItemViewKey = "$domain.searchAdvanceview.getusers{0}";

        /// <summary>
        /// Thời gian timeout của cache thông tin khách hàng
        /// </summary>
        public const int SearchItemViewCacheTimeOut = 5;

        #endregion

        #region Queue

        /// <summary>
        /// Key cho cache các đối tượng cần xử lý trong Queue
        /// </summary>
        public const string QueueNameKey = "$domain.queue";

        /// <summary>
        /// Thời gian time out Key cho cache các đối tượng cần xử lý trong Queue
        /// </summary>
        public const int QueueNameCacheTimeOut = 21900;
        
        /// <summary>
        /// Key cho cache các đối tượng cần xử lý trong Queue
        /// </summary>
        public const string NotificationQueueKey = "eGovCloud.notification.queue";

        /// <summary>
        /// Thời gian time out Key cho cache các đối tượng cần xử lý trong Queue
        /// </summary>
        public const int NotificationQueueTimeOut = 21900;

        #endregion

        #region Quản trị tập trung - cache dùng chung

        /// <summary>
        /// Key cho cache connection hiện tại người dùng đang sử dụng.
        /// - {0} là account hiện tại
        /// </summary>
        public const string DomainConnectionKey = "eGovCloud.connection.domain.{0}";

        /// <summary>
        /// Thời gian time out của cache kết nối
        /// </summary>
        public const int DomainConnectionCacheTimeOut = 60;

        /// <summary>
        /// Key cho cache tất cả domain
        /// </summary>
        public const string AllDomainKey = "eGovCloud.domain.all";

        /// <summary>
        /// Thời gian time out của cache tất cả domain
        /// </summary>
        public const int AllDomainCacheTimeOut = 21900;

        /// <summary>
        /// Key cho cache tên domain hiện tại
        /// - {0} account hiện tại
        /// </summary>
        public const string DomainNameKey = "eGovCloud.domain.name.{0}";

        /// <summary>
        /// Thời gian time out của cache tên domain
        /// </summary>
        public const int DomainNameCacheTimeOut = 21900;

        #endregion
    }
}
