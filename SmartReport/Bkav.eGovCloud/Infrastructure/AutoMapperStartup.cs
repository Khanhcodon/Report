using System;
using System.Collections.Generic;
using AutoMapper;
using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Business.Objects.CacheObjects;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Microsoft.JScript;

namespace Bkav.eGovCloud.Infrastructure
{
    public class AutoMapperStartup
    {
        public static void Initialize()
        {
            #region "Resource"

            Mapper.CreateMap<Resource, ResourceModel>();
            Mapper.CreateMap<ResourceModel, Resource>();

            #endregion "Resource"

            #region "Log"

            Mapper.CreateMap<Log, LogModel>();
            Mapper.CreateMap<LogModel, Log>().ForMember(dest => dest.LogType, mo => mo.Ignore());

            #endregion "Log"

            #region "Settings"

            #region "General Settings"

            Mapper.CreateMap<AdminGeneralSettings, AdminGeneralSettingsModel>()
                .ForMember(dest => dest.ListPageSizeParsed, mo => mo.Ignore());
            Mapper.CreateMap<AdminGeneralSettingsModel, AdminGeneralSettings>()
                .ForMember(dest => dest.ListPageSize, mo => mo.Ignore());

            #endregion "General Settings"

            #region "FileUploadSettings"

            Mapper.CreateMap<FileUploadSettings, FileUploadSettingsModel>()
                .ForMember(dest => dest.FileUploadAllowedExtensionsParsed, mo => mo.Ignore())
                .ForMember(dest => dest.ProfilePictureAllowedExtensionsParsed, mo => mo.Ignore());
            Mapper.CreateMap<FileUploadSettingsModel, FileUploadSettings>()
                .ForMember(dest => dest.FileUploadAllowedExtensions, mo => mo.Ignore())
                .ForMember(dest => dest.ProfilePictureAllowedExtensions, mo => mo.Ignore());

            #endregion "FileUploadSettings"

            #region "EmailSettings"

            Mapper.CreateMap<EmailSettings, EmailSettingsModel>()
                .ForMember(x => x.SmtpPassword, m => m.MapFrom(s => s.SmtpPassword.Base64Decode()));
            Mapper.CreateMap<EmailSettingsModel, EmailSettings>()
                .ForMember(x => x.SmtpPassword, m => m.MapFrom(s => s.SmtpPassword.Base64Encode()));

            #endregion "EmailSettings"

            #region "AuthenticationSettings"

            Mapper.CreateMap<AuthenticationSettings, AuthenticationSettingsModel>();
            Mapper.CreateMap<AuthenticationSettingsModel, AuthenticationSettings>();

            #endregion "AuthenticationSettings"

            #region "PasswordPolicySettings"

            Mapper.CreateMap<PasswordPolicySettings, PasswordPolicySettingsModel>();
            Mapper.CreateMap<PasswordPolicySettingsModel, PasswordPolicySettings>();

            #endregion "PasswordPolicySettings"

            #region "FileLocationSettings"

            Mapper.CreateMap<FileLocationSettings, FileLocationSettingsModel>();
            Mapper.CreateMap<FileLocationSettingsModel, FileLocationSettings>();

            #endregion "FileLocationSettings"

            #region "SearchSettings"

            Mapper.CreateMap<SearchSettings, SearchSettingsModel>();
            Mapper.CreateMap<SearchSettingsModel, SearchSettings>();

            #endregion "SearchSettings"

            #region WorkTimeSettings

            Mapper.CreateMap<WorkTimeSettings, WorkTimeSettingsModel>();
            Mapper.CreateMap<WorkTimeSettingsModel, WorkTimeSettings>();

            #endregion WorkTimeSettings

            #region ImageSetting

            Mapper.CreateMap<ImageSettings, ImageSettingsModel>();
            Mapper.CreateMap<ImageSettingsModel, ImageSettings>();

            #endregion ImageSetting

            #region SmsSettings

            Mapper.CreateMap<SmsSettings, SmsSettingsModel>();
            Mapper.CreateMap<SmsSettingsModel, SmsSettings>();

            #endregion SmsSettings

            #region TransferSettings

            Mapper.CreateMap<TransferSettings, TransferSettingsModel>();
            Mapper.CreateMap<TransferSettingsModel, TransferSettings>();

            #endregion TransferSettings

            #region LanguageSettings

            Mapper.CreateMap<LanguageSettings, LanguageSettingsModel>();
            Mapper.CreateMap<LanguageSettingsModel, LanguageSettings>();

            #endregion LanguageSettings

            #region NotificationSettings

            Mapper.CreateMap<NotificationSettings, NotificationSettingsModel>();
            Mapper.CreateMap<NotificationSettingsModel, NotificationSettings>();

            #endregion NotificationSettings

            #region ConnectionSettings

            Mapper.CreateMap<SsoSettings, ConnectionSettingsModel>();
            Mapper.CreateMap<ConnectionSettingsModel, SsoSettings>();
            Mapper.CreateMap<ConnectionSettings, ConnectionSettingsModel>();
            Mapper.CreateMap<ConnectionSettingsModel, ConnectionSettings>();

            #endregion ConnectionSettings

            #region ConnectionSettings

            Mapper.CreateMap<WarningSettings, WarningSettingsModel>();
            Mapper.CreateMap<WarningSettingsModel, WarningSettings>();

            #endregion ConnectionSettings

            #region FAQSetting

            Mapper.CreateMap<FAQSetting, FAQSettingsModel>();
            Mapper.CreateMap<FAQSettingsModel, FAQSetting>();

            #endregion ConnectionSettings

            #endregion "Settings"

            #region "Position"

            Mapper.CreateMap<Position, PositionModel>();
            Mapper.CreateMap<PositionModel, Position>();

            #endregion "Position"

            #region "JobTitles"

            Mapper.CreateMap<JobTitles, JobTitlesModel>();
            Mapper.CreateMap<JobTitlesModel, JobTitles>();

            #endregion "JobTitles"

            #region Catalog

            Mapper.CreateMap<Catalog, CatalogModel>();
            Mapper.CreateMap<CatalogModel, Catalog>();

            #endregion Catalog

            #region ReportMode

            Mapper.CreateMap<ReportModes, ReportModeModel>();
            Mapper.CreateMap<ReportModeModel, ReportModes>();

            #endregion

            #region ReportRule

            Mapper.CreateMap<ReportRule, ReportRuleModel>();
            Mapper.CreateMap<ReportRuleModel, ReportRule>();

            #endregion 

            #region IndicatorCatalog

            Mapper.CreateMap<IndicatorCatalog, IndicatorCatalogModel>();
            Mapper.CreateMap<IndicatorCatalogModel, IndicatorCatalog>();

            #endregion Catalog

            #region "Category"

            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<CategoryModel, Category>();

            #endregion "Category"

            #region "DocField"

            Mapper.CreateMap<DocField, DocFieldModel>();
            Mapper.CreateMap<DocFieldModel, DocField>();

            #endregion "DocField"

            #region "DocType"

            Mapper.CreateMap<DocType, DocTypeModel>();
            Mapper.CreateMap<DocTypeModel, DocType>();


            Mapper.CreateMap<DocTypeCached, DocType>();

            #endregion "DocType"

            #region "DocTypeTemplate"

            Mapper.CreateMap<DoctypeTemplate, DoctypeTemplateModel>();
            Mapper.CreateMap<DoctypeTemplateModel, DoctypeTemplate>();

            #endregion "DocTypeTemplate"

            #region "DocTypeTemplate"

            Mapper.CreateMap<OnlineTemplate, OnlineTemplateModel>();
            Mapper.CreateMap<OnlineTemplateModel, OnlineTemplate>();

            #endregion "DocTypeTemplate"

            #region "Question"

            Mapper.CreateMap<Question, QuestionModel>();
            Mapper.CreateMap<QuestionModel, Question>();

            #endregion "Question"

            #region "Role"

            Mapper.CreateMap<Role, RoleModel>()
                .ForMember(dest => dest.UserIds, mo => mo.Ignore())
                .ForMember(dest => dest.IgnorePermissionIds, mo => mo.Ignore())
                .ForMember(dest => dest.GrantPermissionIds, mo => mo.Ignore());
            Mapper.CreateMap<RoleModel, Role>()
                .ForMember(dest => dest.VersionByte, mo => mo.Ignore())
                .ForMember(dest => dest.VersionDateTime, mo => mo.Ignore())
                .ForMember(dest => dest.UserRolePermissions, mo => mo.Ignore())
                .ForMember(dest => dest.UserRoles, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedByUserId, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.LastModifiedByUserId, mo => mo.Ignore())
                .ForMember(dest => dest.LastModifiedOnDate, mo => mo.Ignore());

            #endregion "Role"

            #region "Department"

            Mapper.CreateMap<Department, DepartmentModel>();
            Mapper.CreateMap<DepartmentModel, Department>()
                .ForMember(d => d.DepartmentIdExt, m => m.Ignore())
                .ForMember(d => d.DepartmentPath, m => m.Ignore())
                .ForMember(d => d.Level, m => m.Ignore())
                .ForMember(d => d.Order, m => m.Ignore());

            #endregion "Department"

            #region "Store"

            Mapper.CreateMap<Store, StoreModel>();
            Mapper.CreateMap<StoreModel, Store>();

            #endregion "Store"

            #region "Increase"

            Mapper.CreateMap<Increase, IncreaseModel>();
            Mapper.CreateMap<IncreaseModel, Increase>();

            #endregion "Increase"

            #region "Code"

            Mapper.CreateMap<Code, CodeModel>();
            Mapper.CreateMap<CodeModel, Code>();

            #endregion "Code"

            #region "Paper"

            Mapper.CreateMap<Paper, PaperModel>();
            Mapper.CreateMap<PaperModel, Paper>();

            #endregion "Paper"

            #region "Fee"

            Mapper.CreateMap<Fee, FeeModel>();
            Mapper.CreateMap<FeeModel, Fee>();

            #endregion "Fee"

            #region "User"

            Mapper.CreateMap<User, UserModel>()
                .ForMember(dest => dest.RoleIds, mo => mo.Ignore())
                .ForMember(dest => dest.IgnorePermissionIds, mo => mo.Ignore())
                .ForMember(dest => dest.GrantPermissionIds, mo => mo.Ignore())
                .ForMember(dest => dest.DenyPermissionIds, mo => mo.Ignore())
                .ForMember(dest => dest.DepartmentJobTitlesId, mo => mo.Ignore());
            Mapper.CreateMap<UserModel, User>()
                .ForMember(dest => dest.VersionByte, mo => mo.Ignore())
                .ForMember(dest => dest.VersionDateTime, mo => mo.Ignore())
                .ForMember(dest => dest.PasswordHash, mo => mo.Ignore())
                .ForMember(dest => dest.PasswordSalt, mo => mo.Ignore())
                .ForMember(dest => dest.PasswordLastModifiedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.IsLockedOut, mo => mo.Ignore())
                .ForMember(dest => dest.LastLockoutDate, mo => mo.Ignore())
                .ForMember(dest => dest.LastLoginDate, mo => mo.Ignore())
                .ForMember(dest => dest.FailedPasswordAttemptCount, mo => mo.Ignore())
                .ForMember(dest => dest.FailedPasswordAttemptStart, mo => mo.Ignore())
                .ForMember(dest => dest.UserDepartmentJobTitless, mo => mo.Ignore())
                .ForMember(dest => dest.UserRoles, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedByUserId, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.LastModifiedByUserId, mo => mo.Ignore())
                .ForMember(dest => dest.LastModifiedOnDate, mo => mo.Ignore());

            #endregion "User"

            #region "UserProfile"

            Mapper.CreateMap<User, UserProfileModel>();
            Mapper.CreateMap<UserProfileModel, User>();

            #endregion "UserProfile"

            #region "Permission"

            Mapper.CreateMap<Permission, PermissionModel>();
            Mapper.CreateMap<PermissionModel, Permission>();

            #endregion "Permission"

            #region "Form"

            Mapper.CreateMap<Form, FormModel>();
            Mapper.CreateMap<FormModel, Form>()
                .ForMember(f => f.FormTypeId, mo => mo.Ignore())
                .ForMember(f => f.IsPrimary, mo => mo.Ignore())
                //.ForMember(f => f.Json, mo => mo.Ignore())
                .ForMember(f => f.Template, mo => mo.Ignore());

            #endregion "Form"

            #region "Workflow"

            Mapper.CreateMap<Workflow, WorkflowModel>();
            Mapper.CreateMap<WorkflowModel, Workflow>();

            #endregion "Workflow"

            #region "Holiday"

            Mapper.CreateMap<Holiday, HolidayModel>();
            Mapper.CreateMap<HolidayModel, Holiday>();

            #endregion "Holiday"

            #region Document

            Mapper.CreateMap<DocumentExtension, DocumentExtensionModel>();
            Mapper.CreateMap<DocumentExtensionModel, DocumentExtension>();

            Mapper.CreateMap<DocumentContent, DocumentContentModel>()
                .ForMember(p => p.Content, mo => mo.Ignore())
                .ForMember(p => p.DocumentContentDetails, mo => mo.Ignore());
            Mapper.CreateMap<DocumentContentModel, DocumentContent>();

            Mapper.CreateMap<DocumentContentDetail, DocumentContentDetailModel>();
            Mapper.CreateMap<DocumentContentDetailModel, DocumentContentDetail>();

            Mapper.CreateMap<DocFee, DocFeeModel>();
            Mapper.CreateMap<DocFeeModel, DocFee>();

            Mapper.CreateMap<DocPaper, DocPaperModel>();
            Mapper.CreateMap<DocPaperModel, DocPaper>();

            Mapper.CreateMap<Document, DocumentModel>()
                .ForMember(e => e.Compendium, opts => opts.MapFrom(m => GlobalObject.unescape(m.Compendium)));
            Mapper.CreateMap<DocumentModel, Document>()
                .ForMember(p => p.DateAppointed, mo => mo.Ignore()) // ko tự map ở đây dễ bị mất dữ liệu, khi tạo mới thì mới lấy từ model, còn lại dùng Gia hạn
                .ForMember(p => p.ExpireProcess, mo => mo.Ignore())
                .ForMember(p => p.UserCreatedName, mo => mo.Ignore())
                .ForMember(p => p.CategoryName, mo => mo.Ignore())
                .ForMember(p => p.DocTypeName, mo => mo.Ignore())
                .ForMember(e => e.Compendium, opts => opts.MapFrom(m => GlobalObject.unescape(m.Compendium)))
                .ForMember(e => e.DocumentContents, opts => opts.Ignore());

            #endregion Document

            #region DocumentCopy

            Mapper.CreateMap<DocumentCopy, DocumentCopyModel>();
            Mapper.CreateMap<DocumentCopy, DocumentCopy>();
            Mapper.CreateMap<DocumentCopyModel, DocumentCopy>()
                .IgnoreAllNonExisting()
                .ForAllMembers(opts => opts.Condition(dc => !dc.IsSourceValueNull));

            #endregion DocumentCopy

            #region DocumentRelated

            Mapper.CreateMap<DocumentRelated, DocumentRelatedModel>();
            Mapper.CreateMap<DocumentRelatedModel, DocumentRelated>();

            #endregion 

            #region "Authorize"

            //Trên admin
            Mapper.CreateMap<Authorize, Bkav.eGovCloud.Areas.Admin.Models.AuthorizeModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.AuthorizeModel, Authorize>();

            //Trên client
            Mapper.CreateMap<Authorize, Bkav.eGovCloud.Models.AuthorizeModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Models.AuthorizeModel, Authorize>();

            #endregion "Authorize"

            #region ProcessFunctionType

            Mapper.CreateMap<ProcessFunctionType, ProcessFunctionTypeModel>();
            Mapper.CreateMap<ProcessFunctionTypeModel, ProcessFunctionType>();

            #endregion ProcessFunctionType

            #region ProcessFunctionGroup

            Mapper.CreateMap<ProcessFunctionGroup, ProcessFunctionGroupModel>();
            Mapper.CreateMap<ProcessFunctionGroupModel, ProcessFunctionGroup>();

            #endregion ProcessFunctionGroup

            #region ProcessFunctionFilter

            Mapper.CreateMap<ProcessFunctionFilter, ProcessFunctionFilterModel>();
            Mapper.CreateMap<ProcessFunctionFilterModel, ProcessFunctionFilter>();

            #endregion ProcessFunctionFilter

            #region ProcessFunction

            Mapper.CreateMap<ProcessFunction, ProcessFunctionModel>();
            Mapper.CreateMap<ProcessFunctionModel, ProcessFunction>()
                .ForMember(dest => dest.Order, mo => mo.Ignore());

            #endregion ProcessFunction

            #region Comment

            Mapper.CreateMap<Comment, CommentModel>();
            Mapper.CreateMap<Comment, CommentModel>().ForMember(e => e.Content, opts => opts.MapFrom(m => GlobalObject.unescape(m.Content)));
            Mapper.CreateMap<CommentModel, Comment>().ForMember(e => e.Content, opts => opts.MapFrom(m => GlobalObject.unescape(m.Content)));

            #endregion Comment

            #region Search

            Mapper.CreateMap<DocumentCopy, SearchViewModel>();

            Mapper.CreateMap<SearchItemView, SearchDocumentItemResultModel>();

            Mapper.CreateMap<SearchView, SearchDocumentResultModel>();

            #endregion Search

            #region Approver

            Mapper.CreateMap<Approver, ApproverModel>();
            Mapper.CreateMap<ApproverModel, Approver>()
                .ForMember(m => m.DateCreated, mo => mo.Ignore());

            #endregion Approver

            #region Supplementary

            Mapper.CreateMap<Supplementary, SupplementaryModel>();
            Mapper.CreateMap<SupplementaryModel, Supplementary>();

            #endregion Supplementary

            #region "FileLocation"

            Mapper.CreateMap<FileLocation, FileLocationModel>();
            Mapper.CreateMap<FileLocationModel, FileLocation>();

            #endregion "FileLocation"

            #region TemplateKey

            Mapper.CreateMap<TemplateKey, TemplateKeyModel>();
            Mapper.CreateMap<TemplateKeyModel, TemplateKey>();

            #endregion TemplateKey

            #region TemplateKeyCatalog

            Mapper.CreateMap<TemplateKeyCategory, TemplateKeyCategoryModel>();
            Mapper.CreateMap<TemplateKeyCategoryModel, TemplateKeyCategory>();

            #endregion TemplateKeyCatalog
            #region Template

            Mapper.CreateMap<Template, TemplateModel>();
            Mapper.CreateMap<TemplateModel, Template>();

            #endregion Template

            #region NodeModel

            Mapper.CreateMap<Node, NodeModel>();
            Mapper.CreateMap<NodeModel, Node>();

            #endregion NodeModel

            #region ReportGroup

            Mapper.CreateMap<ReportGroup, ReportGroupModel>();
            Mapper.CreateMap<ReportGroupModel, ReportGroup>();

            #endregion ReportGroup

            #region Report

            Mapper.CreateMap<Entities.Customer.Report, ReportModel>();
            Mapper.CreateMap<ReportModel, Entities.Customer.Report>()
                .ForMember(m => m.FileLocationName, mo => mo.Ignore())
                .ForMember(m => m.DateCreated, mo => mo.Ignore())
                .ForMember(m => m.CrystalPath, mo => mo.Ignore());

            #endregion ReportGroup
            #region ReportKey

            Mapper.CreateMap<ReportKey, ReportKeyModel>();
            Mapper.CreateMap<ReportKeyModel, ReportKey>();
            #endregion

            #region "Address"

            Mapper.CreateMap<Entities.Customer.Address, AddressModel>();
            Mapper.CreateMap<AddressModel, Entities.Customer.Address>();

            #endregion "Address"

            #region "Infomation"

            Mapper.CreateMap<Infomation, InfomationModel>();
            Mapper.CreateMap<InfomationModel, Infomation>();

            #endregion "Infomation"

            #region "FormGroup"

            Mapper.CreateMap<FormGroup, FormGroupModel>();

            Mapper.CreateMap<FormGroupModel, FormGroup>();

            #endregion "FormGroup"

            #region "BusinessType"

            Mapper.CreateMap<BusinessType, BusinessTypeModel>();
            Mapper.CreateMap<BusinessTypeModel, BusinessType>();

            #endregion "BusinessType"

            #region "Business"

            Mapper.CreateMap<Entities.Customer.Business, BusinessModel>();
            Mapper.CreateMap<BusinessModel, Entities.Customer.Business>();

            #endregion "Business"

            #region "City"

            Mapper.CreateMap<City, CityModel>();
            Mapper.CreateMap<CityModel, City>();

            #endregion "City"

            #region "District"

            Mapper.CreateMap<District, DistrictModel>();
            Mapper.CreateMap<DistrictModel, District>();

            #endregion "District"

            #region "Ward"

            Mapper.CreateMap<Ward, WardModel>();
            Mapper.CreateMap<WardModel, Ward>();

            #endregion "Ward"

            #region "BusinessLicense"

            Mapper.CreateMap<BusinessLicense, BusinessLicenseModel>();
            Mapper.CreateMap<BusinessLicenseModel, BusinessLicense>();

            #endregion "BusinessLicense"

            #region "DailyProcess"

            Mapper.CreateMap<DailyProcess, DailyProcessModel>();
            Mapper.CreateMap<DailyProcessModel, DailyProcess>();

            #endregion "DailyProcess"

            #region "Print Document"

            Mapper.CreateMap<Document, PrintDocumentModel>();

            #endregion "Print Document"

            #region "KeyWord"

            Mapper.CreateMap<KeyWord, KeyWordModel>();
            Mapper.CreateMap<KeyWordModel, KeyWord>();

            #endregion "KeyWord"

            #region "TransferType"

            Mapper.CreateMap<TransferType, TransferTypeModel>();
            Mapper.CreateMap<TransferTypeModel, TransferType>();

            #endregion "TransferType"

            #region " Printer"

            Mapper.CreateMap<Printer, Bkav.eGovCloud.Models.PrinterModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Models.PrinterModel, Printer>();

            #endregion " Printer"

            #region "AdminPrinter"

            Mapper.CreateMap<Printer, Bkav.eGovCloud.Areas.Admin.Models.PrinterModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.PrinterModel, Printer>();

            #endregion "AdminPrinter"

            #region "Signature"

            Mapper.CreateMap<Signature, SignatureModel>();
            Mapper.CreateMap<SignatureModel, Signature>();

            #endregion "Signature"

            #region "ActivityLog"

            Mapper.CreateMap<ActivityLog, ActivityLogModel>();
            Mapper.CreateMap<ActivityLogModel, ActivityLog>();
            Mapper.CreateMap<ActivityLogModel, ActivityLog>().ForMember(e => e.Content, opts => opts.MapFrom(m => GlobalObject.unescape(m.Content)));
            Mapper.CreateMap<ActivityLog, ActivityLogModel>().ForMember(e => e.Content, opts => opts.MapFrom(m => GlobalObject.unescape(m.Content)));

            #endregion "ActivityLog"

            #region "UserActivityLog"

            Mapper.CreateMap<UserActivityLog, UserActivityLogModel>().ForMember(e => e.Compendium, opts => opts.MapFrom(m => GlobalObject.unescape(m.Compendium)));
            Mapper.CreateMap<UserActivityLogModel, UserActivityLog>().ForMember(e => e.Compendium, opts => opts.MapFrom(m => GlobalObject.unescape(m.Compendium)));

            Mapper.CreateMap<UserActivityLog, UserActivityLogModel>();
            Mapper.CreateMap<UserActivityLogModel, UserActivityLog>();
            Mapper.CreateMap<UserActivityLogModel, Notification>()
                .ForMember(n => n.Title, i => i.MapFrom(u => u.Compendium))
                .ForMember(n => n.NotificationType, i => i.MapFrom(u => (int)NotificationType.eGov))
                .ForMember(n => n.Date, i => i.MapFrom(u => DateTime.Now))
                .ForMember(n => n.ReceiveDate, i => i.MapFrom(u => u.SentDate))
                .ForMember(n => n.UserId, i => i.MapFrom(u => u.UserReceiveId))
                .ForMember(n => n.SenderUserName, i => i.MapFrom(u => u.UserNameSend))
                .ForMember(n => n.SenderFullName, i => i.MapFrom(u => u.FullNameSend))
                .ForMember(n => n.UserId, i => i.MapFrom(u => u.UserReceiveId));

            #endregion "UserActivityLog"

            #region "Level"

            Mapper.CreateMap<Administrative, LevelModel>();
            Mapper.CreateMap<LevelModel, Administrative>();

            #endregion "Level"

            #region "Client"

            //Mapper.CreateMap<Client, ClientModel>();
            //Mapper.CreateMap<ClientModel, Client>();

            #endregion "Client"

            #region "Law"

            Mapper.CreateMap<Law, LawModel>();
            Mapper.CreateMap<LawModel, Law>();

            #endregion "Law"

            #region "People"

            Mapper.CreateMap<Citizen, CitizenModel>();
            Mapper.CreateMap<CitizenModel, Citizen>();

            #endregion "People"

            #region "ScopeArea"

            Mapper.CreateMap<ScopeArea, ScopeAreaModel>();
            Mapper.CreateMap<ScopeAreaModel, ScopeArea>();

            #endregion "ScopeArea"

            #region "ClientScope"

            Mapper.CreateMap<ClientScope, ClientScopeModel>();
            Mapper.CreateMap<ClientScopeModel, ClientScope>();

            #endregion "ClientScope"

            #region "DocumentOnline"

            Mapper.CreateMap<DocumentOnline, DocumentOnlineModel>();
            Mapper.CreateMap<DocumentOnlineModel, DocumentOnline>();

            #endregion "DocumentOnline"

            #region "Guide"

            Mapper.CreateMap<Guide, GuideModel>();
            Mapper.CreateMap<GuideModel, Guide>();

            #endregion "Guide"

            #region "Notify"

            Mapper.CreateMap<Entities.Customer.Notify, NotifyModel>();
            Mapper.CreateMap<NotifyModel, Entities.Customer.Notify>();

            #endregion "Notify"

            #region "Office"

            Mapper.CreateMap<Office, OfficeModel>();
            Mapper.CreateMap<OfficeModel, Office>();

            #endregion "Office"

            #region "EgovJob"

            Mapper.CreateMap<EgovJob, EgovJobModel>();
            Mapper.CreateMap<EgovJobModel, EgovJob>();

            #endregion "EgovJob"

            #region "RequiredSupplementary"

            Mapper.CreateMap<RequiredSupplementary, RequiredSupplementaryModel>();
            Mapper.CreateMap<RequiredSupplementaryModel, RequiredSupplementary>();

            #endregion "EgovJob"

            #region "TimeJob"

            Mapper.CreateMap<TimeJob, TimeJobModel>();
            Mapper.CreateMap<TimeJobModel, TimeJob>();

            #endregion "TimeJob"

            #region "DocTypeTimeJob"

            Mapper.CreateMap<DocTypeTimeJob, DocTypeTimeJobModel>();
            Mapper.CreateMap<DocTypeTimeJobModel, DocTypeTimeJob>();

            #endregion "DocTypeTimeJob"

            #region "BackupRestoreHistory"

            Mapper.CreateMap<BackupRestoreHistory, BackupRestoreHistoryModel>();
            Mapper.CreateMap<BackupRestoreHistoryModel, BackupRestoreHistory>();

            #endregion "BackupRestoreHistory"

            #region "BackupRestoreConfig"

            Mapper.CreateMap<BackupRestoreConfig, BackupRestoreConfigModel>();
            Mapper.CreateMap<BackupRestoreConfigModel, BackupRestoreConfig>();

            #endregion "BackupRestoreConfig"

            #region "ShareFolder"

            Mapper.CreateMap<ShareFolder, ShareFolderModel>();
            Mapper.CreateMap<ShareFolderModel, ShareFolder>();

            #endregion "ShareFolder"

            #region "BackupRestoreFileConfig"

            Mapper.CreateMap<BackupRestoreFileConfig, BackupRestoreFileConfigModel>();
            Mapper.CreateMap<BackupRestoreFileConfigModel, BackupRestoreFileConfig>();

            #endregion "BackupRestoreFileConfig"

            #region "BackupRestoreManager"

            Mapper.CreateMap<BackupRestoreManager, BackupRestoreManagerModel>();
            Mapper.CreateMap<BackupRestoreManagerModel, BackupRestoreManager>();

            #endregion "BackupRestoreManager"

            #region "OnlineRegistrationSettings"

            Mapper.CreateMap<OnlineRegistrationSettings, Bkav.eGovCloud.Models.OnlineRegistrationSettingsModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Models.OnlineRegistrationSettingsModel, OnlineRegistrationSettings>();

            #endregion "Statistics"

            #region "sms"

            Mapper.CreateMap<Sms, Bkav.eGovCloud.Areas.Admin.Models.SmsModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.SmsModel, Sms>();

            Mapper.CreateMap<Sms, Bkav.eGovCloud.Models.SmsModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Models.SmsModel, Sms>();

            #endregion "sms"

            #region "Mail"

            Mapper.CreateMap<Mail, Bkav.eGovCloud.Areas.Admin.Models.MailModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.MailModel, Mail>();

            Mapper.CreateMap<Mail, Bkav.eGovCloud.Models.MailModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Models.MailModel, Mail>();

            #endregion "Mail"

            #region "Notification"

            Mapper.CreateMap<Notification, NotificationModel>();
            Mapper.CreateMap<NotificationModel, Notification>();

            #endregion "Statistics"

            #region "Statistics"

            Mapper.CreateMap<Statistics, StatisticsModel>();
            Mapper.CreateMap<StatisticsModel, Statistics>();

            #endregion "Statistics"

            #region "TreeGroup"

            Mapper.CreateMap<TreeGroup, Bkav.eGovCloud.Areas.Admin.Models.TreeGroupModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.TreeGroupModel, TreeGroup>();

            Mapper.CreateMap<TreeGroup, Bkav.eGovCloud.Models.TreeGroupModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Models.TreeGroupModel, TreeGroup>();

            #endregion "TreeGroup"

            #region "DocColumnSetting"

            Mapper.CreateMap<DocColumnSetting, Bkav.eGovCloud.Areas.Admin.Models.DocColumnSettingModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.DocColumnSettingModel, DocColumnSetting>();

            #endregion "DocColumnSetting"

            #region "PermissionSetting"

            Mapper.CreateMap<PermissionSetting, Bkav.eGovCloud.Areas.Admin.Models.PermissionSettingModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.PermissionSettingModel, PermissionSetting>();

            #endregion "PermissionSetting"

            #region "Domain"
            Mapper.CreateMap<Domain, DomainModel>()
                .ForMember(dest => dest.DomainIds, mo => mo.Ignore());
            Mapper.CreateMap<DomainModel, Domain>()
                .ForMember(dest => dest.CreatedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedByUserId, mo => mo.Ignore())
                .ForMember(dest => dest.LastModifiedOnDate, mo => mo.Ignore())
                .ForMember(dest => dest.LastModifiedByUserId, mo => mo.Ignore());
            #endregion

            #region "Admin Connection"

            Mapper.CreateMap<Connection, Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel>()
                .ForMember(dest => dest.IsCreateDatabaseIfNotExist, mo => mo.Ignore())
                .ForMember(dest => dest.DbType, mo => mo.MapFrom(c => c.DatabaseTypeIdInEnum));
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel, Connection>()
                .ForMember(dest => dest.DatabaseType, mo => mo.Ignore())
                .ForMember(dest => dest.Domain, mo => mo.Ignore())
                .ForMember(dest => dest.DatabaseTypeIdInEnum, mo => mo.MapFrom(c => c.DbType));

            #endregion

            #region "BussinessDocFieldDocTypeGroup"

            Mapper.CreateMap<BussinessDocFieldDocTypeGroup, Bkav.eGovCloud.Areas.Admin.Models.BussinessDocFieldDocTypeGroupModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.BussinessDocFieldDocTypeGroupModel, BussinessDocFieldDocTypeGroup>();

            #endregion "BussinessDocFieldDocTypeGroup"

            #region "NotifyConfig"

            Mapper.CreateMap<NotifyConfig, Bkav.eGovCloud.Areas.Admin.Models.NotifyConfigModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.NotifyConfigModel, NotifyConfig>();

            #endregion "NotifyConfig"

            #region Account

            Mapper.CreateMap<Account, Bkav.eGovCloud.Areas.Admin.Models.AccountModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.AccountModel, Account>();

            #endregion

            #region "InterfaceConfig"

            Mapper.CreateMap<InterfaceConfig, Bkav.eGovCloud.Areas.Admin.Models.InterfaceConfigModel>();
            Mapper.CreateMap<Bkav.eGovCloud.Areas.Admin.Models.InterfaceConfigModel, InterfaceConfig>();

            #endregion "InterfaceConfig"

            #region Service WFC

            // Doctype
            Mapper.CreateMap<DocType, DocTypeDto>();

            // Docfield
            Mapper.CreateMap<DocField, DocFieldDto>();

            // Category
            Mapper.CreateMap<Category, CategoryDto>();

            // Department
            Mapper.CreateMap<Department, DepartmentDto>();

            // Form Group
            Mapper.CreateMap<FormGroup, FormGroupDto>();

            // Form
            Mapper.CreateMap<Form, FormDto>();

            // Document
            Mapper.CreateMap<Document, SearchResultDto>();

            // Fee
            Mapper.CreateMap<Fee, FeeDto>()
                .ForMember(dest => dest.Name, mo => mo.MapFrom(f => f.FeeName))
                .ForMember(dest => dest.Value, mo => mo.MapFrom(f => f.Price));

            // Paper
            Mapper.CreateMap<Paper, PaperDto>();
            Mapper.CreateMap<PaperDto, Paper>();

            // Supplementary
            Mapper.CreateMap<Supplementary, SupplementaryDto>()
                .ForMember(dest => dest.Papers, mo => mo.Ignore())
                .ForMember(dest => dest.Fees, mo => mo.Ignore());

            #endregion

            #region Attachment Preview

            // Doctype
            Mapper.CreateMap<Attachment, AttachmentPreviewModel>();
            Mapper.CreateMap<AttachmentDetail, AttachmentDetailModel>();

            #endregion

            #region OTP

            Mapper.CreateMap<OtpModel, Otp>();
            Mapper.CreateMap<Otp, OtpModel>();

            #endregion

            #region OTP Settings
            Mapper.CreateMap<OTPSettingsModel, OtpSettings>();
            Mapper.CreateMap<OtpSettings, OTPSettingsModel>();
            #endregion

            #region Vote

            Mapper.CreateMap<Vote, VoteModel>();
            Mapper.CreateMap<VoteDetail, VoteDetailModel>();

            #endregion

            #region Calendar

            Mapper.CreateMap<Calendar, CalendarModel>();
            Mapper.CreateMap<CalendarModel, Calendar>();

            Mapper.CreateMap<CalendarDetail, CalendarDetailModel>();

            #endregion

            #region Cached Map

            #region Resource

            Mapper.CreateMap<Resource, ResourceCached>();
            Mapper.CreateMap<ResourceCached, Resource>();

            #endregion

            #region UserCache

            Mapper.CreateMap<User, CurrentUserCached>();
            Mapper.CreateMap<CurrentUserCached, User>();

            Mapper.CreateMap<User, UserCached>()
                .ForMember(dest => dest.Status, mo => mo.MapFrom(i => i.Address));
            Mapper.CreateMap<UserCached, User>();

            #endregion

            #region Department

            Mapper.CreateMap<Department, DepartmentCached>();
            Mapper.CreateMap<DepartmentCached, Department>();

            #endregion

            #region DocumentCached

            Mapper.CreateMap<Comment, CommentCached>();
            Mapper.CreateMap<DocumentContent, DocumentContentCached>();
            Mapper.CreateMap<DocRelation, DocRelationCached>();
            Mapper.CreateMap<Attachment, AttachmentCached>();
            Mapper.CreateMap<AttachmentDetail, AttachmentDetailCache>();
            Mapper.CreateMap<Document, DocumentCached>();
            Mapper.CreateMap<DocPaper, DocPaperCached>();
            Mapper.CreateMap<DocFee, DocFeeCached>()
                    .ForMember(dest => dest.Price, mo => mo.MapFrom(i => i.Price * 1000));
            Mapper.CreateMap<Approver, ApproverCached>();
            Mapper.CreateMap<Supplementary, SupplementaryCached>();

            #endregion

            #region JobTitles

            Mapper.CreateMap<JobTitles, JobTitlesCached>();
            Mapper.CreateMap<JobTitlesCached, JobTitles>();

            #endregion

            #region Position

            Mapper.CreateMap<Position, PositionCached>();
            Mapper.CreateMap<PositionCached, Position>();

            #endregion

            #region Permission

            Mapper.CreateMap<UserRolePermission, PermissionCache>();

            #endregion

            #region UserDepartmentJobTitlesPosition

            Mapper.CreateMap<UserDepartmentJobTitlesPosition, UserDepartmentJobTitlesPositionCached>();
            Mapper.CreateMap<UserDepartmentJobTitlesPositionCached, UserDepartmentJobTitlesPosition>();

            #endregion

            #region StorePrivate

            Mapper.CreateMap<StorePrivate, StorePrivateCached>();

            #endregion

            #region Holiday

            Mapper.CreateMap<Holiday, HolidayCached>();
            Mapper.CreateMap<HolidayCached, Holiday>();

            #endregion

            #region TreeGroup

            Mapper.CreateMap<TreeGroup, TreeGroupCached>();
            Mapper.CreateMap<TreeGroupCached, TreeGroup>();

            #endregion

            #region ProcessFunction

            Mapper.CreateMap<ProcessFunction, ProcessFunctionCached>();
            Mapper.CreateMap<ProcessFunctionCached, ProcessFunction>();

            #endregion

            #region PermissionSetting

            Mapper.CreateMap<PermissionSetting, PermissionSettingCached>();
            Mapper.CreateMap<PermissionSettingCached, PermissionSetting>();

            #endregion

            #region DocColumnSetting

            Mapper.CreateMap<DocColumnSetting, DocColumnSettingCached>();
            Mapper.CreateMap<DocColumnSettingCached, DocColumnSetting>();

            #endregion

            #region InterfaceConfig

            Mapper.CreateMap<InterfaceConfig, InterfaceConfigCached>();
            Mapper.CreateMap<InterfaceConfigCached, InterfaceConfig>();

            #endregion

            #region Category

            Mapper.CreateMap<Category, CategoryCached>();
            Mapper.CreateMap<CategoryCached, Category>();

            #endregion

            #region Workflow

            Mapper.CreateMap<Workflow, WorkflowCached>();
            Mapper.CreateMap<WorkflowCached, Workflow>();

            #endregion

            #region Authorize

            Mapper.CreateMap<Authorize, AuthorizeCached>();
            Mapper.CreateMap<AuthorizeCached, Authorize>();

            #endregion

            #endregion

            #region SSo
            Mapper.CreateMap<SSOSettings, SSOSettingsModel>();
            Mapper.CreateMap<SSOSettingsModel, SSOSettings>();
            #endregion

            #region SSoApi
            Mapper.CreateMap<SSOAPISettings, SSOAPISettingsModel>();
            Mapper.CreateMap<SSOAPISettingsModel, SSOAPISettings>();
            #endregion

            #region BMM
            Mapper.CreateMap<MissionSettings, MissionSettingsModel>();
            Mapper.CreateMap<MissionSettingsModel, MissionSettings>();
            #endregion

            #region Chat Tin Dieu Hanh
            Mapper.CreateMap<ChatSettings, ChatSettingsModel>();
            Mapper.CreateMap<ChatSettingsModel, ChatSettings>();
            #endregion
			
			#region ReportConfig 
            Mapper.CreateMap<ReportConfigSettings, ReportConfigSettingsModel>();
            Mapper.CreateMap<ReportConfigSettingsModel, ReportConfigSettings>();
            #endregion

            #region DataSource

            Mapper.CreateMap<DataSource, DataSourceModel>();
            Mapper.CreateMap<DataSourceModel, DataSource>();

            #endregion DataSource

            #region ReportQuery

            Mapper.CreateMap<ReportQuery, ReportQueryModel>();
            Mapper.CreateMap<ReportQueryModel, ReportQuery>();

            #endregion ReportQuery

            #region ReportQueryFilter

            Mapper.CreateMap<ReportQueryFilter, ReportQueryFilterModel>();
            Mapper.CreateMap<ReportQueryFilterModel, ReportQueryFilter>();
            Mapper.CreateMap<List<ReportQueryFilter>, List<ReportQueryFilterModel>>();
            Mapper.CreateMap<List<ReportQueryFilterModel>, List<ReportQueryFilter>>();

            #endregion ReportQuery

            #region ReportQueryGroup

            Mapper.CreateMap<ReportQueryGroup, ReportQueryGroupModel>();
            Mapper.CreateMap<ReportQueryGroupModel, ReportQueryGroup>();
            Mapper.CreateMap<List<ReportQueryGroup>, List<ReportQueryGroupModel>>();
            Mapper.CreateMap<List<ReportQueryGroupModel>, List<ReportQueryGroup>>();

            #endregion ReportQueryGroup

            #region InCatalog

            Mapper.CreateMap<InCatalogValue, InCatalogValueModel>();
            Mapper.CreateMap<InCatalogValueModel, InCatalogValue>();

            Mapper.CreateMap<InCatalog, InCatalogModel>();
            Mapper.CreateMap<InCatalogModel, InCatalog>();

            #endregion Catalog


            #region
            Mapper.CreateMap<Ad_Locality, Ad_LocalityModel>();
            Mapper.CreateMap<Ad_LocalityModel, Ad_Locality>();
            #endregion

            #region Disaggregation
            // khai báo model trong arear
            Mapper.CreateMap<Disaggregation, DisaggregationModel>();
            Mapper.CreateMap<DisaggregationModel, Disaggregation>();

            Mapper.CreateMap<CategoryDisaggregations, CategoryDisaggreationModel>();
            Mapper.CreateMap<CategoryDisaggreationModel, CategoryDisaggregations>();

            #endregion indicator and categoryDisagregation

            #region "ActionLevel"

            Mapper.CreateMap<Entities.Customer.ActionLevel, ActionLevelModel>();
            Mapper.CreateMap<ActionLevelModel, Entities.Customer.ActionLevel>();

            #endregion "ActionLevel"

            //#region "LocalityDepartmentValue"

            //Mapper.CreateMap<Entities.Customer.LocalityDepartment, LocalityDepartmentModel>();
            //Mapper.CreateMap<LocalityDepartmentModel, Entities.Customer.LocalityDepartment>();

            //#endregion "LocalityDepartmentValue"


            //#region "Locality"

            //Mapper.CreateMap<Entities.Customer.Locality, LocalityModel>();
            //Mapper.CreateMap<LocalityModel, Entities.Customer.Locality>();

            //#endregion "Locality"

            #region SurveyCatalog

            Mapper.CreateMap<SurveyCatalogValue, SurveyCatalogValueModel>();
            Mapper.CreateMap<SurveyCatalogValueModel, SurveyCatalogValue>();

            Mapper.CreateMap<SurveyCatalog, SurveyCatalogModel>();
            Mapper.CreateMap<SurveyCatalogModel, SurveyCatalog>();

            #endregion SurveyCatalog

            #region Ad_Unit

            Mapper.CreateMap<Ad_Unit, Ad_UnitModel>();
            Mapper.CreateMap<Ad_UnitModel, Ad_Unit>();

            #endregion Ad_Unit and categoryDisagregation

            #region "Indatatype"

            //Mapper.CreateMap<Entities.Customer.Ad_Report.Dim_indicatordatatype, IndatatypeModel>();
            //Mapper.CreateMap<IndatatypeModel, Entities.Customer.Ad_Report.Dim_indicatordatatype>();

            #endregion "Indatatype"

            #region "Indatatype"

            Mapper.CreateMap<dataType, dataTypeModel>();
            Mapper.CreateMap<dataTypeModel, dataType>();

            #endregion "Indatatype"

            #region "Targets"

            Mapper.CreateMap<Entities.Customer.Ad_Report.Ad_targets, Ad_TargetsModel>();
            Mapper.CreateMap<Ad_TargetsModel, Entities.Customer.Ad_Report.Ad_targets>();

            #endregion "Targets"
        }
    }
}