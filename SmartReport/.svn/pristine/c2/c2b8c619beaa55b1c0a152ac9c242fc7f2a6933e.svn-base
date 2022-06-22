using System.Collections.Generic;
using AutoMapper;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Search;

namespace Bkav.eGovCloud.Models
{
    internal static class MappingModelEntityExtensions
    {
        #region "Document"
        public static DocumentModel ToModel(this Document entity)
        {
            return Mapper.Map<Document, DocumentModel>(entity);
        }

        public static IEnumerable<DocumentModel> ToListModel(this IEnumerable<Document> listEntity)
        {
            return Mapper.Map<IEnumerable<Document>, IEnumerable<DocumentModel>>(listEntity);
        }

        public static Document ToEntity(this DocumentModel model)
        {
            return Mapper.Map<DocumentModel, Document>(model);
        }

        public static Document ToEntity(this DocumentModel model, Document destination)
        {
            return Mapper.Map(model, destination);
        }

        public static DocumentContent ToEntity(this DocumentContentModel model)
        {
            return Mapper.Map<DocumentContentModel, DocumentContent>(model);
        }
        #endregion

        #region Docpaper

        public static DocPaperModel ToModel(this DocPaper entity)
        {
            return Mapper.Map<DocPaper, DocPaperModel>(entity);
        }

		public static DocPaper ToEntity(this DocPaperModel entity)
		{
			return Mapper.Map<DocPaperModel, DocPaper>(entity);
		}

		public static IEnumerable<DocPaperModel> ToListModel(this IEnumerable<DocPaper> listEntity)
        {
            return Mapper.Map<IEnumerable<DocPaper>, IEnumerable<DocPaperModel>>(listEntity);
		}

		public static IEnumerable<DocPaper> ToListEntity(this IEnumerable<DocPaperModel> listModel)
		{
			return Mapper.Map<IEnumerable<DocPaperModel>, IEnumerable<DocPaper>>(listModel);
		}

		#endregion

		#region DocFee

		public static DocFeeModel ToModel(this DocFee entity)
        {
            return Mapper.Map<DocFee, DocFeeModel>(entity);
        }

        public static IEnumerable<DocFeeModel> ToListModel(this IEnumerable<DocFee> listEntity)
        {
            return Mapper.Map<IEnumerable<DocFee>, IEnumerable<DocFeeModel>>(listEntity);
        }

		public static IEnumerable<DocFee> ToListEntity(this IEnumerable<DocFeeModel> listModel)
		{
			return Mapper.Map<IEnumerable<DocFeeModel>, IEnumerable<DocFee>>(listModel);
		}

		#endregion

		#region DocumentCopy

		public static DocumentCopy ToEntity(this DocumentCopyModel model, DocumentCopy destination)
		{
			return Mapper.Map(model, destination);
		}

        public static IEnumerable<DocumentCopyModel> ToListModel(this IEnumerable<DocumentCopy> listEntity)
        {
            return Mapper.Map<IEnumerable<DocumentCopy>, IEnumerable<DocumentCopyModel>>(listEntity);
        }

        #endregion

        #region Comment

        public static CommentModel ToModel(this Comment entity)
        {
            return Mapper.Map<Comment, CommentModel>(entity);
        }

        public static IEnumerable<CommentModel> ToListModel(this IEnumerable<Comment> listEntity)
        {
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(listEntity);
        }

        #endregion

        #region Search

        public static SearchViewModel ToSearchModel(this DocumentCopy entity)
        {
            return Mapper.Map<DocumentCopy, SearchViewModel>(entity);
        }

        public static IEnumerable<SearchViewModel> ToListSearchModel(this IEnumerable<DocumentCopy> listEntity)
        {
            return Mapper.Map<IEnumerable<DocumentCopy>, IEnumerable<SearchViewModel>>(listEntity);
        }

        #endregion

        #region Approver

        public static ApproverModel ToModel(this Approver entity)
        {
            return Mapper.Map<Approver, ApproverModel>(entity);
        }

        public static IEnumerable<ApproverModel> ToListModel(this IEnumerable<Approver> listEntity)
        {
            return Mapper.Map<IEnumerable<Approver>, IEnumerable<ApproverModel>>(listEntity);
        }

        public static Approver ToEntity(this ApproverModel model)
        {
            return Mapper.Map<ApproverModel, Approver>(model);
        }

        public static Approver ToEntity(this ApproverModel model, Approver destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region Supplementary

        public static SupplementaryModel ToModel(this Supplementary entity)
        {
            return Mapper.Map<Supplementary, SupplementaryModel>(entity);
        }

        public static IEnumerable<SupplementaryModel> ToListModel(this IEnumerable<Supplementary> listEntity)
        {
            return Mapper.Map<IEnumerable<Supplementary>, IEnumerable<SupplementaryModel>>(listEntity);
        }

        public static Supplementary ToEntity(this SupplementaryModel model)
        {
            return Mapper.Map<SupplementaryModel, Supplementary>(model);
        }

        public static Supplementary ToEntity(this SupplementaryModel model, Supplementary destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region Renewals

        public static RenewalsModel ToModel(this Renewals entity)
        {
            return Mapper.Map<Renewals, RenewalsModel>(entity);
        }

        public static IEnumerable<RenewalsModel> ToListModel(this IEnumerable<Renewals> listEntity)
        {
            return Mapper.Map<IEnumerable<Renewals>, IEnumerable<RenewalsModel>>(listEntity);
        }

        public static Renewals ToEntity(this RenewalsModel model)
        {
            return Mapper.Map<RenewalsModel, Renewals>(model);
        }

        public static Renewals ToEntity(this RenewalsModel model, Renewals destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region Search

        public static SearchDocumentItemResultModel ToModel(this SearchItemView entity)
        {
            return Mapper.Map<SearchItemView, SearchDocumentItemResultModel>(entity);
        }

        public static IEnumerable<SearchDocumentItemResultModel> ToListModel(this IEnumerable<SearchItemView> listEntity)
        {
            return Mapper.Map<IEnumerable<SearchItemView>, IEnumerable<SearchDocumentItemResultModel>>(listEntity);
        }

        public static SearchDocumentResultModel ToModel(this SearchView entity)
        {
            return Mapper.Map<SearchView, SearchDocumentResultModel>(entity);
        }

        public static IEnumerable<SearchDocumentResultModel> ToListModel(this IEnumerable<SearchView> listEntity)
        {
            return Mapper.Map<IEnumerable<SearchView>, IEnumerable<SearchDocumentResultModel>>(listEntity);
        }

        #endregion

        #region DailyProcess

        public static DailyProcessModel ToModel(this DailyProcess entity)
        {
            return Mapper.Map<DailyProcess, DailyProcessModel>(entity);
        }

        public static IEnumerable<DailyProcessModel> ToListModel(this IEnumerable<DailyProcess> listEntity)
        {
            return Mapper.Map<IEnumerable<DailyProcess>, IEnumerable<DailyProcessModel>>(listEntity);
        }

        #endregion

        #region "Business"

        public static BusinessModel ToModel(this Entities.Customer.Business entity)
        {
            return Mapper.Map<Entities.Customer.Business, BusinessModel>(entity);
        }

        public static IEnumerable<BusinessModel> ToListModel(this IEnumerable<Entities.Customer.Business> listEntity)
        {
            return Mapper.Map<IEnumerable<Entities.Customer.Business>, IEnumerable<BusinessModel>>(listEntity);
        }

        public static Entities.Customer.Business ToEntity(this BusinessModel model)
        {
            return Mapper.Map<BusinessModel, Entities.Customer.Business>(model);
        }

        public static Entities.Customer.Business ToEntity(this BusinessModel model, Entities.Customer.Business destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region "BusinessLicense"
        public static BusinessLicenseModel ToModel(this BusinessLicense entity)
        {
            return Mapper.Map<BusinessLicense, BusinessLicenseModel>(entity);
        }

        public static IEnumerable<BusinessLicenseModel> ToListModel(this IEnumerable<BusinessLicense> listEntity)
        {
            return Mapper.Map<IEnumerable<BusinessLicense>, IEnumerable<BusinessLicenseModel>>(listEntity);
        }

        public static BusinessLicense ToEntity(this BusinessLicenseModel model)
        {
            return Mapper.Map<BusinessLicenseModel, BusinessLicense>(model);
        }

        public static BusinessLicense ToEntity(this BusinessLicenseModel model, BusinessLicense destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region Print Document

        public static IEnumerable<PrintDocumentModel> ToListPrintModel(this IEnumerable<Document> listEntity)
        {
            return Mapper.Map<IEnumerable<Document>, IEnumerable<PrintDocumentModel>>(listEntity);
        }

        #endregion

        #region "User profile"
        public static UserProfileModel ToModel(this User entity)
        {
            return Mapper.Map<User, UserProfileModel>(entity);
        }

        public static IEnumerable<UserProfileModel> ToListModel(this IEnumerable<User> listEntity)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserProfileModel>>(listEntity);
        }

        public static User ToEntity(this UserProfileModel model)
        {
            return Mapper.Map<UserProfileModel, User>(model);
        }

        public static User ToEntity(this UserProfileModel model, User destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region OTP
        public static Otp ToEntity(this OtpModel model, Otp destination)
        {
            return Mapper.Map(model, destination);
        }

        public static OtpModel ToModel(this Otp entity)
        {
            return Mapper.Map<Otp, OtpModel>(entity);
        }
        #endregion

        #region "Printer"
        public static PrinterModel ToModel(this Printer entity)
        {
            return Mapper.Map<Printer, PrinterModel>(entity);
        }

        public static IEnumerable<PrinterModel> ToListModel(this IEnumerable<Printer> listEntity)
        {
            return Mapper.Map<IEnumerable<Printer>, IEnumerable<PrinterModel>>(listEntity);
        }

        public static Printer ToEntity(this PrinterModel model)
        {
            return Mapper.Map<PrinterModel, Printer>(model);
        }

        public static Printer ToEntity(this PrinterModel model, Printer destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region "Signature"
        public static SignatureModel ToModel(this Signature entity)
        {
            return Mapper.Map<Signature, SignatureModel>(entity);
        }

        public static IEnumerable<SignatureModel> ToListModel(this IEnumerable<Signature> listEntity)
        {
            return Mapper.Map<IEnumerable<Signature>, IEnumerable<SignatureModel>>(listEntity);
        }

        public static Signature ToEntity(this SignatureModel model)
        {
            return Mapper.Map<SignatureModel, Signature>(model);
        }

        public static Signature ToEntity(this SignatureModel model, Signature destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region "UserActivityLog"
        public static UserActivityLogModel ToModel(this UserActivityLog entity)
        {
            return Mapper.Map<UserActivityLog, UserActivityLogModel>(entity);
        }

        public static IEnumerable<UserActivityLogModel> ToListModel(this IEnumerable<UserActivityLog> listEntity)
        {
            return Mapper.Map<IEnumerable<UserActivityLog>, IEnumerable<UserActivityLogModel>>(listEntity);
        }

        public static Notification ToNotify(this UserActivityLogModel entity)
        {
            return Mapper.Map<UserActivityLogModel, Notification>(entity);
        }

        #endregion

        #region "DocumentOnline"
        public static DocumentOnlineModel ToModel(this DocumentOnline entity)
        {
            return Mapper.Map<DocumentOnline, DocumentOnlineModel>(entity);
        }

        public static IEnumerable<DocumentOnlineModel> ToListModel(this IEnumerable<DocumentOnline> listEntity)
        {
            return Mapper.Map<IEnumerable<DocumentOnline>, IEnumerable<DocumentOnlineModel>>(listEntity);
        }

        public static DocumentOnline ToEntity(this DocumentOnlineModel model)
        {
            return Mapper.Map<DocumentOnlineModel, DocumentOnline>(model);
        }

        public static DocumentOnline ToEntity(this DocumentOnlineModel model, DocumentOnline destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region "Authorize"
        public static AuthorizeModel ToModel(this Authorize entity)
        {
            return Mapper.Map<Authorize, AuthorizeModel>(entity);
        }

        public static IEnumerable<AuthorizeModel> ToListModel(this IEnumerable<Authorize> listEntity)
        {
            return Mapper.Map<IEnumerable<Authorize>, IEnumerable<AuthorizeModel>>(listEntity);
        }

        public static Authorize ToEntity(this AuthorizeModel model)
        {
            return Mapper.Map<AuthorizeModel, Authorize>(model);
        }

        public static Authorize ToEntity(this AuthorizeModel model, Authorize destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region "OnlineRegistrationSettings"
        public static OnlineRegistrationSettingsModel ToModel(this OnlineRegistrationSettings entity)
        {
            return Mapper.Map<OnlineRegistrationSettings, OnlineRegistrationSettingsModel>(entity);
        }

        public static OnlineRegistrationSettings ToEntity(this Bkav.eGovCloud.Models.OnlineRegistrationSettingsModel model)
        {
            return Mapper.Map<Bkav.eGovCloud.Models.OnlineRegistrationSettingsModel, OnlineRegistrationSettings>(model);
        }

        public static OnlineRegistrationSettings ToEntity(this Bkav.eGovCloud.Models.OnlineRegistrationSettingsModel model, OnlineRegistrationSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region "FAQ"

        public static FAQSettingsModel ToModel(this FAQSetting entity)
        {
            return Mapper.Map<FAQSetting, FAQSettingsModel>(entity);
        }

        public static FAQSetting ToEntity(this Bkav.eGovCloud.Models.FAQSettingsModel model)
        {
            return Mapper.Map<Bkav.eGovCloud.Models.FAQSettingsModel, FAQSetting>(model);
        }

        public static FAQSetting ToEntity(this Bkav.eGovCloud.Models.FAQSettingsModel model, FAQSetting destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region "Mail"

        public static MailModel ToModel(this Mail entity)
        {
            return Mapper.Map<Mail, MailModel>(entity);
        }

        public static IEnumerable<MailModel> ToListModel(this IEnumerable<Mail> listEntity)
        {
            return Mapper.Map<IEnumerable<Mail>, IEnumerable<MailModel>>(listEntity);
        }

        public static Mail ToEntity(this MailModel model)
        {
            return Mapper.Map<MailModel, Mail>(model);
        }

        public static Mail ToEntity(this MailModel model, Mail destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Mail> ToListEntity(this IEnumerable<MailModel> listmodel)
        {
            return Mapper.Map<IEnumerable<MailModel>, IEnumerable<Mail>>(listmodel);
        }

        #endregion

        #region "Sms"

        public static SmsModel ToModel(this Sms entity)
        {
            return Mapper.Map<Sms, SmsModel>(entity);
        }

        public static IEnumerable<SmsModel> ToListModel(this IEnumerable<Sms> listEntity)
        {
            return Mapper.Map<IEnumerable<Sms>, IEnumerable<SmsModel>>(listEntity);
        }

        public static Sms ToEntity(this SmsModel model)
        {
            return Mapper.Map<SmsModel, Sms>(model);
        }

        public static Sms ToEntity(this SmsModel model, Sms destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Sms> ToListEntity(this IEnumerable<SmsModel> listmodel)
        {
            return Mapper.Map<IEnumerable<SmsModel>, IEnumerable<Sms>>(listmodel);
        }

        #endregion

        #region "Notification"

        public static NotificationModel ToModel(this Notification entity)
        {
            return Mapper.Map<Notification, NotificationModel>(entity);
        }

        public static IEnumerable<NotificationModel> ToListModel(this IEnumerable<Notification> listEntity)
        {
            return Mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationModel>>(listEntity);
        }

        public static Notification ToEntity(this NotificationModel model)
        {
            return Mapper.Map<NotificationModel, Notification>(model);
        }

        public static Notification ToEntity(this NotificationModel model, Notification destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Notification> ToListEntity(this IEnumerable<NotificationModel> listmodel)
        {
            return Mapper.Map<IEnumerable<NotificationModel>, IEnumerable<Notification>>(listmodel);
        }

        #endregion

        #region "TreeGroup"

        public static TreeGroupModel ToModel(this TreeGroup entity)
        {
            return Mapper.Map<TreeGroup, TreeGroupModel>(entity);
        }

        public static IEnumerable<TreeGroupModel> ToListModel(this IEnumerable<TreeGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<TreeGroup>, IEnumerable<TreeGroupModel>>(listEntity);
        }

        #endregion

        #region "AttachmentPreview"

        public static IEnumerable<AttachmentPreviewModel> ToListModel(this IEnumerable<Attachment> listEntity)
        {
            return Mapper.Map<IEnumerable<Attachment>, IEnumerable<AttachmentPreviewModel>>(listEntity);
        }

        #endregion

        #region "Vote"

        public static IEnumerable<VoteModel> ToListModel(this IEnumerable<Vote> listEntity)
        {
            return Mapper.Map<IEnumerable<Vote>, IEnumerable<VoteModel>>(listEntity);
        }

        public static VoteModel ToModel(this Vote entity)
        {
            return Mapper.Map<Vote, VoteModel>(entity);
        }

        public static IEnumerable<VoteDetailModel> ToListModel(this IEnumerable<VoteDetail> listEntity)
        {
            return Mapper.Map<IEnumerable<VoteDetail>, IEnumerable<VoteDetailModel>>(listEntity);
        }

        public static VoteDetailModel ToModel(this VoteDetail entity)
        {
            return Mapper.Map<VoteDetail, VoteDetailModel>(entity);
        }
        #endregion

        #region Calendar

        public static IEnumerable<CalendarModel> ToListModel(this IEnumerable<Calendar> listEntity)
        {
            return Mapper.Map<IEnumerable<Calendar>, IEnumerable<CalendarModel>>(listEntity);
        }

        public static CalendarModel ToModel(this Calendar entity)
        {
            return Mapper.Map<Calendar, CalendarModel>(entity);
        }
        
        #endregion
    }
}