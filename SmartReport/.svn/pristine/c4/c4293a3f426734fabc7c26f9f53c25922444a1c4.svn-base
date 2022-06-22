using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Models
{
	[FluentValidation.Attributes.Validator(typeof(UserSettingValidator))]
	public class UserSettingModel
	{
		public UserSettingModel()
		{
			LanguageSettings languageSettings = DependencyResolver.Current.GetService<LanguageSettings>();
			DocumentProfilesKey = new List<DocumentDefaultSetting>();
			GeneralKeyConfigs = new List<GeneralConfigsSetting>();
			QuickView = (byte)QuickViewType.Below;
			FontSize = (byte)FontSizeType.Vua;
			DisplayPopupTransferTheoLo = true;
			Language = languageSettings.Language;
			HasPopupChat = true;
			MUseAvatar = false;
			MUsePopup = false;
			MFontSize = (byte)FontSizeType.Vua;
			AppCreates = new List<Apps>();
			FindProcessDocument = true;
			AutoInsertDocumentInfoScan = false;
			ShowTranferFormWhenQuickAction = false;
			StoreIds = new Dictionary<Guid, int>();
		}

		/// <summary>
		/// lấy hoặc thiết lập hồ sơ mặc đinh của người dùng
		/// </summary>
		[LocalizationDisplayName("User.UserSetting.Fields.DocumentProfile.Label")]
		[JsonProperty("documentProfiles")]
		public List<DocumentDefaultSetting> DocumentProfilesKey { get; set; }

		/// <summary>
		/// lấy hoặc thiết lập cấu hình chung của người dùng
		/// </summary>
		[LocalizationDisplayName("User.UserSetting.Fields.GeneralConfigs.Label")]
		[JsonProperty("generalKeyConfigs")]
		public List<GeneralConfigsSetting> GeneralKeyConfigs { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập Số bản ghi trên 1 trang mặc định
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.DefaultPageSizeHome.Label")]
		[JsonProperty("defaultPageSizeHome")]
		public int? DefaultPageSizeHome { get; set; }

		/// <summary>
		/// Lẩy hoặc thiết lập Danh sách page size (áp dụng cho phân trang để người dùng có thể chọn nhiều loại pagesize khác nhau)
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.ListPageSizeParsed.Label")]
		[JsonProperty("listPageSizeHome")]
		public string ListPageSizeHome { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập sử dụng chức năng load page dạng scroll (giống load page của facebook) hay dạng phân trang thông thường
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.LoadPageScroll.Label")]
		[JsonProperty("isLoadPageScroll")]
		public bool IsLoadPageScroll { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập  có mở hồ sơ văn bản khi load lại trang đối với các hồ sơ văn bản trước đó
		/// </summary>
		[LocalizationDisplayName("Setting.General.Fields.IsSaveOpenTab.Label")]
		[JsonProperty("isSaveOpenTab")]
		public bool IsSaveOpenTab { get; set; }

		/// <summary>
		/// lấy hoặc thiết lập vị trí hiển thị thông tin tóm tắt văn bản (không hiển thị , hiển thị bên phải , hiển thị bên dưới)
		/// </summary>
		[LocalizationDisplayName("User.UserSetting.Fields.QuickView.Label")]
		[JsonProperty("quickView")]
		public byte QuickView { get; set; }

		/// <summary>
		/// Lấy hoặc thiết lập hiển thị xem văn bản, hồ sơ là dạng chi tiết(False) hay là dạng đầy đủ(True)
		/// </summary>
		[LocalizationDisplayName("User.UserSetting.Fields.IsFullQuickView.Label")]
		[JsonProperty("isFullQuickView")]
		public bool isFullQuickView { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.FontSize.Label")]
		[JsonProperty("fontSize")]
		public byte FontSize { get; set; }

		/// <summary>
		/// Trạng thái phân trang thay đổi khi người dùng có sự thay đổi
		/// </summary>
		[JsonProperty("isChangePageType")]
		public bool IsChangePageType { get; set; }

		/// <summary>
		/// Thiết lập trạng thái có hiển thị popup cho ý kiến trên chức năng bàn giao theo lô
		/// </summary>
		[LocalizationDisplayName("User.UserSetting.Fields.DisplayPopupTransferTheoLo.Label")]
		[JsonProperty("displayPopupTransferTheoLo")]
		public bool DisplayPopupTransferTheoLo { get; set; }

		/// <summary>
		/// Sử dụng bộ gõ Tiếng Việt
		/// 1.VNI
		/// 2.Telex
		/// 3.Viqr
		/// 4.Tổng hợp
		/// </summary>
		[LocalizationDisplayName("User.UserSetting.Fields.MudimMethod.Label")]
		[JsonProperty("MudimMethod")]
		public int MudimMethod { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.ViewDocInPopUp.Label")]
		[JsonProperty("ViewDocInPopUp")]
		public bool ViewDocInPopUp { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.PopUpWidth.Label")]
		[JsonProperty("PopUpWidth")]
		public int PopUpWidth { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.PopUpHeight.Label")]
		[JsonProperty("PopUpHeight")]
		public int PopUpHeight { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.Language.Label")]
		[JsonProperty("Language")]
		public Language Language { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.IgnoreConfirmRelation.Label")]
		[JsonProperty("IgnoreConfirmRelation")]
		public bool IgnoreConfirmRelation { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.HasPopupChat.Label")]
		[JsonProperty("hasPopupChat")]
		public bool HasPopupChat { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.PrinterId.Label")]
		[JsonProperty("PrinterId")]
		public int PrinterId { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.HasShowDongGui.Label")]
		[JsonProperty("hasShowDongGui")]
		public bool HasShowDongGui { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.HasShowLuuSo.Label")]
		[JsonProperty("hasHideLuuSo")]
		public bool HasHideLuuSo { get; set; }

		[LocalizationDisplayName("Mobile.UserSetting.Fields.MStartApp.Label")]
		[JsonProperty("MStartApp")]
		public string MStartApp { get; set; }

		[LocalizationDisplayName("Mobile.UserSetting.Fields.MFontSize.Label")]
		[JsonProperty("MFontSize")]
		public byte MFontSize { get; set; }

		[LocalizationDisplayName("Mobile.UserSetting.Fields.MFontFamily.Label")]
		[JsonProperty("MFontFamily")]
		public byte MFontFamily { get; set; }
		[LocalizationDisplayName("Mobile.UserSetting.Fields.MNotifyType.Label")]
		[JsonProperty("MNotifyType")]
		public byte MNotifyType { get; set; }

		[LocalizationDisplayName("Mobile.UserSetting.Fields.MPageSize.Label")]
		[JsonProperty("MPageSize")]
		public int? MPageSize { get; set; }

		[LocalizationDisplayName("Mobile.UserSetting.Fields.MUseAvatar.Label")]
		[JsonProperty("MUseAvatar")]
		public bool MUseAvatar { get; set; }

		[LocalizationDisplayName("Mobile.UserSetting.Fields.MUsePopup.Label")]
		[JsonProperty("MUsePopup")]
		public bool MUsePopup { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.DocTypePinned.Label")]
		[JsonProperty("PinnedDocTypes")]
		public List<Guid> PinnedDocTypes { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.TransferConfig.Label")]
		[JsonProperty("TransferConfig")]
		public string TransferConfigs { get; set; }

		[LocalizationDisplayName("User.UserSetting.Fields.AppCreateModel.Label")]
		[JsonProperty("AppCreateModel")]
		public List<Apps> AppCreates { get; set; }

		public string Avatar { get; set; }

		public bool FindProcessDocument { get; set; }

		public bool AlwaysDisplayFullDocumentInfo { get; set; }

		public bool AutoInsertDocumentInfoScan { get; set; }

		public bool LimitByMAC { get; set; }

		public bool LimitByIp { get; set; }

		/// <summary>
		/// Hien thi form ban giao khi chuyen nhanh: chuyen nguoi khoi tao, chuyen nguoi gui
		/// </summary>
		public bool ShowTranferFormWhenQuickAction { get; set; }

		/// <summary>
		/// Hình thức văn bản gần nhất sử dụng
		/// </summary>
		[JsonProperty("CategoryId")]
		public int? CategoryId { get; internal set; }

		/// <summary>
		/// Sổ văn bản gần nhất sử dụng
		/// </summary>
		[JsonProperty("StoreId")]
		public Dictionary<Guid, int> StoreIds { get; internal set; }

		/// <summary>
		/// Bắt đầu vắng
		/// </summary>
		public string StartAbsent { get; set; }

		/// <summary>
		/// Hết báo văng
		/// </summary>
		public string EndAbsent { get; set; }

		/// <summary>
		/// Trạng thái Có báo văng
		/// </summary>
		public bool HasAbsent { get; set; }
	}

	public class TransferColumnConfig
	{
		public TransferColumn TransferColumn { get; set; }

		public bool IsActive { get; set; }

		public int Position { get; set; }
	}

	public enum TransferColumn
	{
		Account = 0,
		UserName = 1,
		Dept = 2,
		Position = 3
	}

	public class BaseSetting
	{
		/// <summary>
		/// phím ctrl:true
		/// </summary>
		[JsonProperty("isCtrl")]
		public bool IsCtrl { get; set; }

		/// <summary>
		/// phím Shift:true
		/// </summary>
		[JsonProperty("isShift")]
		public bool IsShift { get; set; }

		/// <summary>
		/// phím Alt:true
		/// </summary>
		[JsonProperty("isAlt")]
		public bool IsAlt { get; set; }

		/// <summary>
		/// key code
		/// </summary>
		[JsonProperty("keyCode")]
		public string KeyCode { get; set; }

		/// <summary>
		/// tên phím
		/// </summary>
		[JsonProperty("keyName")]
		public string KeyName { get; set; }

		/// <summary>
		/// tên phím tăt
		/// </summary>
		[JsonProperty("shortKey")]
		public string ShortKey { get; set; }
	}

	public class DocumentDefaultSetting : BaseSetting
	{
		/// <summary>
		/// tham số truyền vào
		/// </summary>
		[JsonProperty("argument")]
		public string Argument { get; set; }

		/// <summary>
		/// tên hàm
		/// </summary>
		[JsonProperty("functionName")]
		public string FunctionName { get; set; }
	}

	public class GeneralConfigsSetting : BaseSetting
	{
		/// <summary>
		/// tham số truyền vào
		/// </summary>
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }

		/// <summary>
		/// tên hàm
		/// </summary>
		[JsonProperty("functionName")]
		public string FunctionName { get; set; }
	}

	/// <summary>
	/// Enum hiển thị tóm tắt văn bản
	/// </summary>
	public enum QuickViewType
	{
		/// <summary>
		/// Không hiển thị
		/// </summary>
		Hide = 0,

		/// <summary>
		/// Bên phải
		/// </summary>
		Right = 1,

		/// <summary>
		/// Bên dưới
		/// </summary>
		Below = 2
	}

	/// <summary>
	/// Enum cỡ chữ
	/// </summary>
	public enum FontSizeType
	{
		/// <summary>
		/// Cỡ chữ nhỏ
		/// </summary>
		Nho = 0,

		/// <summary>
		/// Cỡ chữ vừa
		/// </summary>
		Vua = 1,

		/// <summary>
		/// Cỡ chữ lớn
		/// </summary>
		Lon = 2
	}
}