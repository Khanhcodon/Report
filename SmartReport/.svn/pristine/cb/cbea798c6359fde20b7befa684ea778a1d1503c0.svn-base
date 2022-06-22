using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.Settings;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public static class MappingModelEntityExtensions
    {
        #region "Resource"

        public static ResourceModel ToModel(this Resource entity)
        {
            return Mapper.Map<Resource, ResourceModel>(entity);
        }

        public static IEnumerable<ResourceModel> ToListModel(this IEnumerable<Resource> listEntity)
        {
            return Mapper.Map<IEnumerable<Resource>, IEnumerable<ResourceModel>>(listEntity);
        }

        public static Resource ToEntity(this ResourceModel model)
        {
            return Mapper.Map<ResourceModel, Resource>(model);
        }

        public static Resource ToEntity(this ResourceModel model, Resource destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Resource"

        #region "Ad_Locality"

        public static Ad_LocalityModel ToModel(this Ad_Locality entity)
        {
            return Mapper.Map<Ad_Locality, Ad_LocalityModel>(entity);
        }

        public static IEnumerable<Ad_LocalityModel> ToListModel(this IEnumerable<Ad_Locality> listEntity)
        {
            return Mapper.Map<IEnumerable<Ad_Locality>, IEnumerable<Ad_LocalityModel>>(listEntity);
        }
        public static Ad_Locality ToEntity(this Ad_LocalityModel model, Ad_Locality destinations)
        {
            return Mapper.Map(model, destinations);
        }
       
        public static Ad_Locality ToEntity(this Ad_LocalityModel model)
        {
            return Mapper.Map<Ad_LocalityModel, Ad_Locality>(model);
        }


        public static Ad_Locality ToListEntity(this Ad_LocalityModel model, Ad_Locality destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Locality"

        #region "Log"

        public static LogModel ToModel(this Log entity)
        {
            return Mapper.Map<Log, LogModel>(entity);
        }

        public static IEnumerable<LogModel> ToListModel(this IEnumerable<Log> listEntity)
        {
            return Mapper.Map<IEnumerable<Log>, IEnumerable<LogModel>>(listEntity);
        }

        public static Log ToEntity(this LogModel model)
        {
            return Mapper.Map<LogModel, Log>(model);
        }

        public static Log ToEntity(this LogModel model, Log destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Log"

        #region "Settings"

        #region "General Settings"

        public static AdminGeneralSettingsModel ToModel(this AdminGeneralSettings entity)
        {
            return Mapper.Map<AdminGeneralSettings, AdminGeneralSettingsModel>(entity);
        }

        public static AdminGeneralSettings ToEntity(this AdminGeneralSettingsModel model)
        {
            return Mapper.Map<AdminGeneralSettingsModel, AdminGeneralSettings>(model);
        }

        public static AdminGeneralSettings ToEntity(this AdminGeneralSettingsModel model, AdminGeneralSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "General Settings"

        #region "FileUploadSettings"

        public static FileUploadSettingsModel ToModel(this FileUploadSettings entity)
        {
            return Mapper.Map<FileUploadSettings, FileUploadSettingsModel>(entity);
        }

        public static FileUploadSettings ToEntity(this FileUploadSettingsModel model)
        {
            return Mapper.Map<FileUploadSettingsModel, FileUploadSettings>(model);
        }

        public static FileUploadSettings ToEntity(this FileUploadSettingsModel model, FileUploadSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "FileUploadSettings"

        #region "EmailSettings"

        public static EmailSettingsModel ToModel(this EmailSettings entity)
        {
            return Mapper.Map<EmailSettings, EmailSettingsModel>(entity);
        }

        public static EmailSettings ToEntity(this EmailSettingsModel model)
        {
            return Mapper.Map<EmailSettingsModel, EmailSettings>(model);
        }

        public static EmailSettings ToEntity(this EmailSettingsModel model, EmailSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "EmailSettings"

        #region "AuthenticationSettings"

        public static AuthenticationSettingsModel ToModel(this AuthenticationSettings entity)
        {
            return Mapper.Map<AuthenticationSettings, AuthenticationSettingsModel>(entity);
        }

        public static AuthenticationSettings ToEntity(this AuthenticationSettingsModel model)
        {
            return Mapper.Map<AuthenticationSettingsModel, AuthenticationSettings>(model);
        }

        public static AuthenticationSettings ToEntity(this AuthenticationSettingsModel model, AuthenticationSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "AuthenticationSettings"

        #region "PasswordPolicySettings"

        public static PasswordPolicySettingsModel ToModel(this PasswordPolicySettings entity)
        {
            return Mapper.Map<PasswordPolicySettings, PasswordPolicySettingsModel>(entity);
        }

        public static PasswordPolicySettings ToEntity(this PasswordPolicySettingsModel model)
        {
            return Mapper.Map<PasswordPolicySettingsModel, PasswordPolicySettings>(model);
        }

        public static PasswordPolicySettings ToEntity(this PasswordPolicySettingsModel model, PasswordPolicySettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "PasswordPolicySettings"

        #region "FileLocationSettings"

        public static FileLocationSettingsModel ToModel(this FileLocationSettings entity)
        {
            return Mapper.Map<FileLocationSettings, FileLocationSettingsModel>(entity);
        }

        public static FileLocationSettings ToEntity(this FileLocationSettingsModel model)
        {
            return Mapper.Map<FileLocationSettingsModel, FileLocationSettings>(model);
        }

        public static FileLocationSettings ToEntity(this FileLocationSettingsModel model, FileLocationSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "FileLocationSettings"

        #region "SearchSettings"

        public static SearchSettingsModel ToModel(this SearchSettings entity)
        {
            return Mapper.Map<SearchSettings, SearchSettingsModel>(entity);
        }

        public static SearchSettings ToEntity(this SearchSettingsModel model)
        {
            return Mapper.Map<SearchSettingsModel, SearchSettings>(model);
        }

        public static SearchSettings ToEntity(this SearchSettingsModel model, SearchSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "SearchSettings"

        #region WorkTimeSettings

        public static WorkTimeSettingsModel ToModel(this WorkTimeSettings entity)
        {
            return Mapper.Map<WorkTimeSettings, WorkTimeSettingsModel>(entity);
        }

        public static WorkTimeSettings ToEntity(this WorkTimeSettingsModel model)
        {
            return Mapper.Map<WorkTimeSettingsModel, WorkTimeSettings>(model);
        }

        public static WorkTimeSettings ToEntity(this WorkTimeSettingsModel model, WorkTimeSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion WorkTimeSettings

        #region ImageSettings

        public static ImageSettingsModel ToModel(this ImageSettings entity)
        {
            return Mapper.Map<ImageSettings, ImageSettingsModel>(entity);
        }

        public static ImageSettings ToEntity(this ImageSettingsModel model)
        {
            return Mapper.Map<ImageSettingsModel, ImageSettings>(model);
        }

        public static ImageSettings ToEntity(this ImageSettingsModel model, ImageSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion ImageSettings

        #region SmsSettings

        public static SmsSettingsModel ToModel(this SmsSettings entity)
        {
            return Mapper.Map<SmsSettings, SmsSettingsModel>(entity);
        }

        public static SmsSettings ToEntity(this SmsSettingsModel model)
        {
            return Mapper.Map<SmsSettingsModel, SmsSettings>(model);
        }

        public static SmsSettings ToEntity(this SmsSettingsModel model, SmsSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion SmsSettings

        #region TransferSettings

        public static TransferSettingsModel ToModel(this TransferSettings entity)
        {
            return Mapper.Map<TransferSettings, TransferSettingsModel>(entity);
        }

        public static TransferSettings ToEntity(this TransferSettingsModel model)
        {
            return Mapper.Map<TransferSettingsModel, TransferSettings>(model);
        }

        public static TransferSettings ToEntity(this TransferSettingsModel model, TransferSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion TransferSettings

        #region LanguageSettings

        public static LanguageSettingsModel ToModel(this LanguageSettings entity)
        {
            return Mapper.Map<LanguageSettings, LanguageSettingsModel>(entity);
        }

        public static LanguageSettings ToEntity(this LanguageSettingsModel model)
        {
            return Mapper.Map<LanguageSettingsModel, LanguageSettings>(model);
        }

        public static LanguageSettings ToEntity(this LanguageSettingsModel model, LanguageSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion LanguageSettings

        #region NotificationSettings

        public static NotificationSettingsModel ToModel(this NotificationSettings entity)
        {
            return Mapper.Map<NotificationSettings, NotificationSettingsModel>(entity);
        }

        public static NotificationSettings ToEntity(this NotificationSettingsModel model)
        {
            return Mapper.Map<NotificationSettingsModel, NotificationSettings>(model);
        }

        public static NotificationSettings ToEntity(this NotificationSettingsModel model, NotificationSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion NotificationSettings

        #region OtpSettings
        public static OTPSettingsModel ToModel(this OtpSettings entity)
        {
            return Mapper.Map<OtpSettings, OTPSettingsModel>(entity);
        }

        public static OtpSettings ToEntity(this OTPSettingsModel model)
        {
            return Mapper.Map<OTPSettingsModel, OtpSettings>(model);
        }
        #endregion

        #region ConnectionSettings

        public static ConnectionSettingsModel ToModel(this SsoSettings entity)
        {
            return Mapper.Map<SsoSettings, ConnectionSettingsModel>(entity);
        }

        public static ConnectionSettingsModel ToModel(this ConnectionSettings entity)
        {
            return Mapper.Map<ConnectionSettings, ConnectionSettingsModel>(entity);
        }

        public static SsoSettings ToEntity(this ConnectionSettingsModel model)
        {
            return Mapper.Map<ConnectionSettingsModel, SsoSettings>(model);
        }

        public static SsoSettings ToEntity(this ConnectionSettingsModel model, SsoSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        public static ConnectionSettings ToEntity(this ConnectionSettingsModel model, ConnectionSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region OnlineRegistrationSettings

        //public static Bkav.eGovCloud.Areas.Admin.Models.Settings.OnlineRegistrationSettingsModel ToModel(this OnlineRegistrationSettings entity)
        //{
        //    return Mapper.Map<OnlineRegistrationSettings, Bkav.eGovCloud.Areas.Admin.Models.Settings.OnlineRegistrationSettingsModel>(entity);
        //}

        //public static OnlineRegistrationSettings ToEntity(this Bkav.eGovCloud.Areas.Admin.Models.Settings.OnlineRegistrationSettingsModel model)
        //{
        //    return Mapper.Map<Bkav.eGovCloud.Areas.Admin.Models.Settings.OnlineRegistrationSettingsModel, OnlineRegistrationSettings>(model);
        //}

        //public static OnlineRegistrationSettings ToEntity(this Bkav.eGovCloud.Areas.Admin.Models.Settings.OnlineRegistrationSettingsModel model, OnlineRegistrationSettings destination)
        //{
        //    return Mapper.Map(model, destination);
        //}

        #endregion

        #region WarningSettings

        public static WarningSettingsModel ToModel(this WarningSettings entity)
        {
            return Mapper.Map<WarningSettings, WarningSettingsModel>(entity);
        }

        public static WarningSettings ToEntity(this WarningSettingsModel model)
        {
            return Mapper.Map<WarningSettingsModel, WarningSettings>(model);
        }

        public static WarningSettings ToEntity(this WarningSettingsModel model, WarningSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #endregion "Settings"

        #region "Customer"
        #region "ReportMode"

        public static ReportModeModel ToModel(this ReportModes entity)
        {
            return Mapper.Map<ReportModes, ReportModeModel>(entity);
        }

        public static IEnumerable<ReportModeModel> ToListModel(this IEnumerable<ReportModes> listEntity)
        {
            return Mapper.Map<IEnumerable<ReportModes>, IEnumerable<ReportModeModel>>(listEntity);
        }

        public static ReportModes ToEntity(this ReportModeModel model)
        {
            return Mapper.Map<ReportModeModel, ReportModes>(model);
        }

        public static ReportModes ToEntity(this ReportModeModel model, ReportModes destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<ReportModes> ToEntity(this IEnumerable<ReportModeModel> model)
        {
            return Mapper.Map<IEnumerable<ReportModeModel>, IEnumerable<ReportModes>>(model);
        }

        #endregion

        #region "ReportRule"

        public static ReportRuleModel ToModel(this ReportRule entity)
        {
            return Mapper.Map<ReportRule, ReportRuleModel>(entity);
        }

        public static IEnumerable<ReportRuleModel> ToListModel(this IEnumerable<ReportRule> listEntity)
        {
            return Mapper.Map<IEnumerable<ReportRule>, IEnumerable<ReportRuleModel>>(listEntity);
        }

        public static ReportRule ToEntity(this ReportRuleModel model)
        {
            return Mapper.Map<ReportRuleModel, ReportRule>(model);
        }

        public static ReportRule ToEntity(this ReportRuleModel model, ReportRule destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<ReportRule> ToEntity(this IEnumerable<ReportRuleModel> model)
        {
            return Mapper.Map<IEnumerable<ReportRuleModel>, IEnumerable<ReportRule>>(model);
        }

        #endregion

        #region "IndicatorCatalog"

        public static InCatalogModel ToModel(this InCatalog entity)
        {
            return Mapper.Map<InCatalog, InCatalogModel>(entity);
        }

        public static IEnumerable<InCatalogModel> ToListModel(this IEnumerable<InCatalog> listEntity)
        {
            return Mapper.Map<IEnumerable<InCatalog>, IEnumerable<InCatalogModel>>(listEntity);
        }

        public static InCatalog ToEntity(this InCatalogModel model)
        {
            return Mapper.Map<InCatalogModel, InCatalog>(model);
        }

        public static InCatalog ToEntity(this InCatalogModel model, InCatalog destination)
        {
            return Mapper.Map(model, destination);
        }

        public static Disaggregation ToEntity(this DisaggregationModel model, Disaggregation destinations)
        {
            return Mapper.Map(model, destinations);
        }
        public static Disaggregation ToEntity(this DisaggregationModel model)
        {
            return Mapper.Map<DisaggregationModel, Disaggregation>(model);
        }

        public static CategoryDisaggregations ToEntity(this CategoryDisaggreationModel model)
        {
            return Mapper.Map<CategoryDisaggreationModel, CategoryDisaggregations>(model);
        }

        public static CategoryDisaggregations ToEntity(this CategoryDisaggreationModel model, CategoryDisaggregations desaggrea)
        {
            return Mapper.Map(model, desaggrea);
        }

        public static IEnumerable<Disaggregation> ToEntity(this IEnumerable<DisaggregationModel> model)
        {
            return Mapper.Map<IEnumerable<DisaggregationModel>, IEnumerable<Disaggregation>>(model);
        }

        public static DisaggregationModel ToModel(this Disaggregation entity)
        {
            return Mapper.Map<Disaggregation, DisaggregationModel>(entity);
        }

        public static CategoryDisaggreationModel ToModel(this CategoryDisaggregations entity)
        {
            return Mapper.Map<CategoryDisaggregations, CategoryDisaggreationModel>(entity);
        }

        public static IEnumerable<InCatalog> ToEntity(this IEnumerable<InCatalogModel> model)
        {
            return Mapper.Map<IEnumerable<InCatalogModel>, IEnumerable<InCatalog>>(model);
        }

        public static InCatalogValueModel ToModel(this InCatalogValue entity)
        {
            return Mapper.Map<InCatalogValue, InCatalogValueModel>(entity);
        }

        public static IEnumerable<InCatalogValueModel> ToListModel(this IEnumerable<InCatalogValue> listEntity)
        {
            return Mapper.Map<IEnumerable<InCatalogValue>, IEnumerable<InCatalogValueModel>>(listEntity);
        }

        public static InCatalogValue ToEntity(this InCatalogValueModel model)
        {
            return Mapper.Map<InCatalogValueModel, InCatalogValue>(model);
        }

        public static InCatalogValue ToEntity(this InCatalogValueModel model, InCatalogValue destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<InCatalogValue> ToEntity(this IEnumerable<InCatalogValueModel> model)
        {
            return Mapper.Map<IEnumerable<InCatalogValueModel>, IEnumerable<InCatalogValue>>(model);
        }

        public static IndicatorCatalogModel ToModel(this IndicatorCatalog entity)
        {
            return Mapper.Map<IndicatorCatalog, IndicatorCatalogModel>(entity);
        }

        public static IEnumerable<IndicatorCatalogModel> ToListModel(this IEnumerable<IndicatorCatalog> listEntity)
        {
            return Mapper.Map<IEnumerable<IndicatorCatalog>, IEnumerable<IndicatorCatalogModel>>(listEntity);
        }

        public static IndicatorCatalog ToEntity(this IndicatorCatalogModel model)
        {
            return Mapper.Map<IndicatorCatalogModel, IndicatorCatalog>(model);
        }

        public static IndicatorCatalog ToEntity(this IndicatorCatalogModel model, IndicatorCatalog destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<IndicatorCatalog> ToEntity(this IEnumerable<IndicatorCatalogModel> model)
        {
            return Mapper.Map<IEnumerable<IndicatorCatalogModel>, IEnumerable<IndicatorCatalog>>(model);
        }

        #endregion "Catalog"
        #region "Catalog"

        public static CatalogModel ToModel(this Catalog entity)
        {
            return Mapper.Map<Catalog, CatalogModel>(entity);
        }

        public static IEnumerable<CatalogModel> ToListModel(this IEnumerable<Catalog> listEntity)
        {
            return Mapper.Map<IEnumerable<Catalog>, IEnumerable<CatalogModel>>(listEntity);
        }

        public static Catalog ToEntity(this CatalogModel model)
        {
            return Mapper.Map<CatalogModel, Catalog>(model);
        }

        public static Catalog ToEntity(this CatalogModel model, Catalog destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Catalog> ToEntity(this IEnumerable<CatalogModel> model)
        {
            return Mapper.Map<IEnumerable<CatalogModel>, IEnumerable<Catalog>>(model);
        }

        #endregion "Catalog"

        #region "CatalogValue"

        public static CatalogValueModel ToListModel(this CatalogValue entity)
        {
            return Mapper.Map<CatalogValue, CatalogValueModel>(entity);
        }

        public static IEnumerable<CatalogValueModel> ToListModel(this IEnumerable<CatalogValue> listEntity)
        {
            return Mapper.Map<IEnumerable<CatalogValue>, IEnumerable<CatalogValueModel>>(listEntity);
        }

        public static CatalogValue ToEntity(this CatalogValueModel model)
        {
            return Mapper.Map<CatalogValueModel, CatalogValue>(model);
        }

        public static CatalogValue ToEntity(this CatalogValueModel model, CatalogValue destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<CatalogValue> ToEntity(this IEnumerable<CatalogValueModel> model)
        {
            return Mapper.Map<IEnumerable<CatalogValueModel>, IEnumerable<CatalogValue>>(model);
        }

        #endregion "CatalogValue"

        #region "Category"

        public static CategoryModel ToModel(this Category entity)
        {
            return Mapper.Map<Category, CategoryModel>(entity);
        }

        public static IEnumerable<CategoryModel> ToListModel(this IEnumerable<Category> listEntity)
        {
            return Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(listEntity);
        }

        public static Category ToEntity(this CategoryModel model)
        {
            return Mapper.Map<CategoryModel, Category>(model);
        }

        public static Category ToEntity(this CategoryModel model, Category destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Category"

        #region "Position"

        public static PositionModel ToModel(this Position entity)
        {
            return Mapper.Map<Position, PositionModel>(entity);
        }

        public static IEnumerable<PositionModel> ToListModel(this IEnumerable<Position> listEntity)
        {
            return Mapper.Map<IEnumerable<Position>, IEnumerable<PositionModel>>(listEntity);
        }

        public static Position ToEntity(this PositionModel model)
        {
            return Mapper.Map<PositionModel, Position>(model);
        }

        public static Position ToEntity(this PositionModel model, Position destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Position"

        #region "JobTitles"

        public static JobTitlesModel ToModel(this JobTitles entity)
        {
            return Mapper.Map<JobTitles, JobTitlesModel>(entity);
        }

        public static IEnumerable<JobTitlesModel> ToListModel(this IEnumerable<JobTitles> listEntity)
        {
            return Mapper.Map<IEnumerable<JobTitles>, IEnumerable<JobTitlesModel>>(listEntity);
        }

        public static JobTitles ToEntity(this JobTitlesModel model)
        {
            return Mapper.Map<JobTitlesModel, JobTitles>(model);
        }

        public static JobTitles ToEntity(this JobTitlesModel model, JobTitles destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "JobTitles"

        #region "DocField"

        public static DocFieldModel ToModel(this DocField entity)
        {
            return Mapper.Map<DocField, DocFieldModel>(entity);
        }

        public static IEnumerable<DocFieldModel> ToListModel(this IEnumerable<DocField> listEntity)
        {
            return Mapper.Map<IEnumerable<DocField>, IEnumerable<DocFieldModel>>(listEntity);
        }

        public static DocField ToEntity(this DocFieldModel model)
        {
            return Mapper.Map<DocFieldModel, DocField>(model);
        }

        public static DocField ToEntity(this DocFieldModel model, DocField destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "DocField"

        #region "DocType"

        public static DocTypeModel ToModel(this DocType entity)
        {
            return Mapper.Map<DocType, DocTypeModel>(entity);
        }

        public static IEnumerable<DocTypeModel> ToListModel(this IEnumerable<DocType> listEntity)
        {
            return Mapper.Map<IEnumerable<DocType>, IEnumerable<DocTypeModel>>(listEntity);
        }

        public static DocType ToEntity(this DocTypeModel model)
        {
            return Mapper.Map<DocTypeModel, DocType>(model);
        }

        public static DocType ToEntity(this DocTypeModel model, DocType destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "DocType"

        #region "Role"

        public static RoleModel ToModel(this Role entity)
        {
            return Mapper.Map<Role, RoleModel>(entity);
        }

        public static IEnumerable<RoleModel> ToListModel(this IEnumerable<Role> listEntity)
        {
            return Mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(listEntity);
        }

        public static Role ToEntity(this RoleModel model)
        {
            return Mapper.Map<RoleModel, Role>(model);
        }

        public static Role ToEntity(this RoleModel model, Role destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Role"

        #region "Department"

        public static DepartmentModel ToModel(this Department entity)
        {
            return Mapper.Map<Department, DepartmentModel>(entity);
        }

        public static IEnumerable<DepartmentModel> ToListModel(this IEnumerable<Department> listEntity)
        {
            return Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentModel>>(listEntity);
        }

        public static Department ToEntity(this DepartmentModel model)
        {
            return Mapper.Map<DepartmentModel, Department>(model);
        }

        public static Department ToEntity(this DepartmentModel model, Department destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Department"

        #region "Store"

        public static StoreModel ToModel(this Store entity)
        {
            return Mapper.Map<Store, StoreModel>(entity);
        }

        public static IEnumerable<StoreModel> ToListModel(this IEnumerable<Store> listEntity)
        {
            return Mapper.Map<IEnumerable<Store>, IEnumerable<StoreModel>>(listEntity);
        }

        public static Store ToEntity(this StoreModel model)
        {
            return Mapper.Map<StoreModel, Store>(model);
        }

        public static Store ToEntity(this StoreModel model, Store destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Store"

        #region "User"

        public static UserModel ToModel(this User entity)
        {
            return Mapper.Map<User, UserModel>(entity);
        }

        public static IEnumerable<UserModel> ToListModel(this IEnumerable<User> listEntity)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(listEntity);
        }

        public static User ToEntity(this UserModel model)
        {
            return Mapper.Map<UserModel, User>(model);
        }

        public static User ToEntity(this UserModel model, User destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "User"

        #region "Increase"

        public static IncreaseModel ToModel(this Increase entity)
        {
            return Mapper.Map<Increase, IncreaseModel>(entity);
        }

        public static IEnumerable<IncreaseModel> ToListModel(this IEnumerable<Increase> listEntity)
        {
            return Mapper.Map<IEnumerable<Increase>, IEnumerable<IncreaseModel>>(listEntity);
        }

        public static Increase ToEntity(this IncreaseModel model)
        {
            return Mapper.Map<IncreaseModel, Increase>(model);
        }

        public static Increase ToEntity(this IncreaseModel model, Increase destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Increase"

        #region "Code"

        public static CodeModel ToModel(this Code entity)
        {
            return Mapper.Map<Code, CodeModel>(entity);
        }

        public static IEnumerable<CodeModel> ToListModel(this IEnumerable<Code> listEntity)
        {
            return Mapper.Map<IEnumerable<Code>, IEnumerable<CodeModel>>(listEntity);
        }

        public static Code ToEntity(this CodeModel model)
        {
            return Mapper.Map<CodeModel, Code>(model);
        }

        public static Code ToEntity(this CodeModel model, Code destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Code"

        #region "Fee"

        public static FeeModel ToModel(this Fee entity)
        {
            return Mapper.Map<Fee, FeeModel>(entity);
        }

        public static IEnumerable<FeeModel> ToListModel(this IEnumerable<Fee> listEntity)
        {
            return Mapper.Map<IEnumerable<Fee>, IEnumerable<FeeModel>>(listEntity);
        }

        public static Fee ToEntity(this FeeModel model)
        {
            return Mapper.Map<FeeModel, Fee>(model);
        }

        public static Fee ToEntity(this FeeModel model, Fee destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Fee"

        #region "Paper"

        public static PaperModel ToModel(this Paper entity)
        {
            return Mapper.Map<Paper, PaperModel>(entity);
        }

        public static IEnumerable<PaperModel> ToListModel(this IEnumerable<Paper> listEntity)
        {
            return Mapper.Map<IEnumerable<Paper>, IEnumerable<PaperModel>>(listEntity);
        }

        public static Paper ToEntity(this PaperModel model)
        {
            return Mapper.Map<PaperModel, Paper>(model);
        }

        public static Paper ToEntity(this PaperModel model, Paper destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Paper"

        #region "Permission"

        public static PermissionModel ToModel(this Permission entity)
        {
            return Mapper.Map<Permission, PermissionModel>(entity);
        }

        public static IEnumerable<PermissionModel> ToListModel(this IEnumerable<Permission> listEntity)
        {
            return Mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionModel>>(listEntity);
        }

        #endregion "Permission"

        #region "Form"

        public static FormModel ToModel(this Form entity)
        {
            return Mapper.Map<Form, FormModel>(entity);
        }

        public static IEnumerable<FormModel> ToListModel(this IEnumerable<Form> listEntity)
        {
            return Mapper.Map<IEnumerable<Form>, IEnumerable<FormModel>>(listEntity);
        }

        public static Form ToEntity(this FormModel model)
        {
            return Mapper.Map<FormModel, Form>(model);
        }

        public static Form ToEntity(this FormModel model, Form destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Form> ToEntity(this IEnumerable<FormModel> model)
        {
            return Mapper.Map<IEnumerable<FormModel>, IEnumerable<Form>>(model);
        }

        #endregion "Form"

        #region "Workflow"

        public static WorkflowModel ToModel(this Workflow entity)
        {
            return Mapper.Map<Workflow, WorkflowModel>(entity);
        }

        public static IEnumerable<WorkflowModel> ToListModel(this IEnumerable<Workflow> listEntity)
        {
            return Mapper.Map<IEnumerable<Workflow>, IEnumerable<WorkflowModel>>(listEntity);
        }

        public static Workflow ToEntity(this WorkflowModel model)
        {
            return Mapper.Map<WorkflowModel, Workflow>(model);
        }

        public static Workflow ToEntity(this WorkflowModel model, Workflow destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Workflow> ToEntity(this IEnumerable<WorkflowModel> model)
        {
            return Mapper.Map<IEnumerable<WorkflowModel>, IEnumerable<Workflow>>(model);
        }

        #endregion "Workflow"

        #region "Holiday"

        public static HolidayModel ToModel(this Holiday entity)
        {
            var result = Mapper.Map<Holiday, HolidayModel>(entity);
            result.RangeLunar = entity.HolidayRange < 0 ? -1 : entity.HolidayRange == 0 ? 0 : 1;
            result.UHolidayRang = Math.Abs(entity.HolidayRange);
            return result;
        }

        public static IEnumerable<HolidayModel> ToListModel(this IEnumerable<Holiday> listEntity)
        {
            var holidays = listEntity as List<Holiday> ?? listEntity.ToList();
            var results = Mapper.Map<IEnumerable<Holiday>, List<HolidayModel>>(holidays);
            foreach (var result in results)
            {
                var entity = holidays.Single(c => c.HolidayId == result.HolidayId);
                result.RangeLunar = entity.HolidayRange < 0 ? -1 : entity.HolidayRange == 0 ? 0 : 1;
                result.UHolidayRang = Math.Abs(entity.HolidayRange);
            }
            return results;
        }

        public static Holiday ToEntity(this HolidayModel entity)
        {
            var result = Mapper.Map<HolidayModel, Holiday>(entity);
            result.HolidayRange = entity.UHolidayRang * entity.RangeLunar;
            return result;
        }

        public static IEnumerable<Holiday> ToListEntity(this IEnumerable<HolidayModel> listEntity)
        {
            var holidayModels = listEntity as List<HolidayModel> ?? listEntity.ToList();
            var results = Mapper.Map<IEnumerable<HolidayModel>, List<Holiday>>(holidayModels);
            foreach (var result in results)
            {
                var entity = holidayModels.Single(c => c.HolidayId == result.HolidayId);
                result.HolidayRange = entity.UHolidayRang * entity.RangeLunar;
            }
            return results;
        }

        public static Holiday ToEntity(this HolidayModel model, Holiday destination)
        {
            var result = Mapper.Map(model, destination);
            result.HolidayRange = model.UHolidayRang * model.RangeLunar;
            return result;
        }

        #endregion "Holiday"

        #region Document

        //#region Document

        //public static DocumentModel ToModel(this Document entity)
        //{
        //    return Mapper.Map<Document, DocumentModel>(entity);
        //}

        //public static IEnumerable<DocumentModel> ToListModel(this IEnumerable<Document> listEntity)
        //{
        //    return Mapper.Map<IEnumerable<Document>, IEnumerable<DocumentModel>>(listEntity);
        //}

        //public static Document ToEntity(this DocumentModel entity)
        //{
        //    return Mapper.Map<DocumentModel, Document>(entity);
        //}

        //public static IEnumerable<Document> ToListEntity(this IEnumerable<DocumentModel> listEntity)
        //{
        //    return Mapper.Map<IEnumerable<DocumentModel>, IEnumerable<Document>>(listEntity);
        //}

        //public static Document ToEntity(this DocumentModel model, Document destination)
        //{
        //    return Mapper.Map(model, destination);
        //}

        //#endregion

        #endregion Document

        #region DocumentRelated
        public static DocumentRelatedModel ToModel(this DocumentRelated entity)
        {
            return Mapper.Map<DocumentRelated, DocumentRelatedModel>(entity);
        }

        public static IEnumerable<DocumentRelatedModel> ToListModel(this IEnumerable<DocumentRelated> listEntity)
        {
            return Mapper.Map<IEnumerable<DocumentRelated>, IEnumerable<DocumentRelatedModel>>(listEntity);
        }

        public static DocumentRelated ToEntity(this DocumentRelatedModel model)
        {
            return Mapper.Map<DocumentRelatedModel, DocumentRelated>(model);
        }

        public static DocumentRelated ToEntity(this DocumentRelatedModel model, DocumentRelated destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<DocumentRelated> ToEntity(this IEnumerable<DocumentRelatedModel> model)
        {
            return Mapper.Map<IEnumerable<DocumentRelatedModel>, IEnumerable<DocumentRelated>>(model);
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

        #endregion "Authorize"

        #region ProcessFunction

        public static ProcessFunctionModel ToModel(this ProcessFunction entity)
        {
            return Mapper.Map<ProcessFunction, ProcessFunctionModel>(entity);
        }

        public static IEnumerable<ProcessFunctionModel> ToListModel(this IEnumerable<ProcessFunction> listEntity)
        {
            return Mapper.Map<IEnumerable<ProcessFunction>, IEnumerable<ProcessFunctionModel>>(listEntity);
        }

        public static ProcessFunction ToEntity(this ProcessFunctionModel entity)
        {
            return Mapper.Map<ProcessFunctionModel, ProcessFunction>(entity);
        }

        public static ProcessFunction ToEntity(this ProcessFunctionModel model, ProcessFunction destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion ProcessFunction

        #region ProcessFunctionType

        public static ProcessFunctionTypeModel ToModel(this ProcessFunctionType entity)
        {
            return Mapper.Map<ProcessFunctionType, ProcessFunctionTypeModel>(entity);
        }

        public static IEnumerable<ProcessFunctionTypeModel> ToListModel(this IEnumerable<ProcessFunctionType> listEntity)
        {
            return Mapper.Map<IEnumerable<ProcessFunctionType>, IEnumerable<ProcessFunctionTypeModel>>(listEntity);
        }

        public static ProcessFunctionType ToEntity(this ProcessFunctionTypeModel entity)
        {
            return Mapper.Map<ProcessFunctionTypeModel, ProcessFunctionType>(entity);
        }

        public static ProcessFunctionType ToEntity(this ProcessFunctionTypeModel model, ProcessFunctionType destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion ProcessFunctionType

        #region ProcessFunctionGroup

        public static ProcessFunctionGroupModel ToModel(this ProcessFunctionGroup entity)
        {
            return Mapper.Map<ProcessFunctionGroup, ProcessFunctionGroupModel>(entity);
        }

        public static IEnumerable<ProcessFunctionGroupModel> ToListModel(this IEnumerable<ProcessFunctionGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<ProcessFunctionGroup>, IEnumerable<ProcessFunctionGroupModel>>(listEntity);
        }

        public static ProcessFunctionGroup ToEntity(this ProcessFunctionGroupModel entity)
        {
            return Mapper.Map<ProcessFunctionGroupModel, ProcessFunctionGroup>(entity);
        }

        public static ProcessFunctionGroup ToEntity(this ProcessFunctionGroupModel model, ProcessFunctionGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion ProcessFunctionGroup

        #region ProcessFunctionFilter

        public static ProcessFunctionFilterModel ToModel(this ProcessFunctionFilter entity)
        {
            return Mapper.Map<ProcessFunctionFilter, ProcessFunctionFilterModel>(entity);
        }

        public static IEnumerable<ProcessFunctionFilterModel> ToListModel(this IEnumerable<ProcessFunctionFilter> listEntity)
        {
            return Mapper.Map<IEnumerable<ProcessFunctionFilter>, IEnumerable<ProcessFunctionFilterModel>>(listEntity);
        }

        public static ProcessFunctionFilter ToEntity(this ProcessFunctionFilterModel entity)
        {
            return Mapper.Map<ProcessFunctionFilterModel, ProcessFunctionFilter>(entity);
        }

        public static ProcessFunctionFilter ToEntity(this ProcessFunctionFilterModel model, ProcessFunctionFilter destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion ProcessFunctionFilter

        #region TemplateKey

        public static TemplateKeyModel ToModel(this TemplateKey entity)
        {
            return Mapper.Map<TemplateKey, TemplateKeyModel>(entity);
        }

        public static IEnumerable<TemplateKeyModel> ToListModel(this IEnumerable<TemplateKey> listEntity)
        {
            return Mapper.Map<IEnumerable<TemplateKey>, IEnumerable<TemplateKeyModel>>(listEntity);
        }

        public static TemplateKey ToEntity(this TemplateKeyModel entity)
        {
            return Mapper.Map<TemplateKeyModel, TemplateKey>(entity);
        }

        public static TemplateKey ToEntity(this TemplateKeyModel model, TemplateKey destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion TemplateKey

        #region Template

        public static TemplateModel ToModel(this Template entity)
        {
            return Mapper.Map<Template, TemplateModel>(entity);
        }

        public static IEnumerable<TemplateModel> ToListModel(this IEnumerable<Template> listEntity)
        {
            return Mapper.Map<IEnumerable<Template>, IEnumerable<TemplateModel>>(listEntity);
        }

        public static Template ToEntity(this TemplateModel entity)
        {
            return Mapper.Map<TemplateModel, Template>(entity);
        }

        public static Template ToEntity(this TemplateModel model, Template destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Template

        #region "FileLocation"

        public static IEnumerable<FileLocationModel> ToListModel(this IEnumerable<FileLocation> listEntity)
        {
            return Mapper.Map<IEnumerable<FileLocation>, IEnumerable<FileLocationModel>>(listEntity);
        }

        public static FileLocationModel ToModel(this FileLocation entity)
        {
            return Mapper.Map<FileLocation, FileLocationModel>(entity);
        }

        public static FileLocation ToEntity(this FileLocationModel model)
        {
            return Mapper.Map<FileLocationModel, FileLocation>(model);
        }

        public static FileLocation ToEntity(this FileLocationModel model, FileLocation destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "FileLocation"

        #region Node

        public static IEnumerable<NodeModel> ToListModel(this IEnumerable<Node> listEntity)
        {
            return Mapper.Map<IEnumerable<Node>, IEnumerable<NodeModel>>(listEntity);
        }

        public static NodeModel ToModel(this Node entity)
        {
            return Mapper.Map<Node, NodeModel>(entity);
        }

        public static Node ToEntity(this NodeModel model)
        {
            return Mapper.Map<NodeModel, Node>(model);
        }

        public static Node ToEntity(this NodeModel model, Node destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Node

        #region Report Group

        public static ReportGroupModel ToModel(this ReportGroup entity)
        {
            return Mapper.Map<ReportGroup, ReportGroupModel>(entity);
        }

        public static IEnumerable<ReportGroupModel> ToListModel(this IEnumerable<ReportGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<ReportGroup>, IEnumerable<ReportGroupModel>>(listEntity);
        }

        public static ReportGroup ToEntity(this ReportGroupModel entity)
        {
            return Mapper.Map<ReportGroupModel, ReportGroup>(entity);
        }

        public static ReportGroup ToEntity(this ReportGroupModel model, ReportGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Report Group

        #region Report

        public static ReportModel ToModel(this Entities.Customer.Report entity)
        {
            return Mapper.Map<Entities.Customer.Report, ReportModel>(entity);
        }

        public static IEnumerable<ReportModel> ToListModel(this IEnumerable<Entities.Customer.Report> listEntity)
        {
            return Mapper.Map<IEnumerable<Entities.Customer.Report>, IEnumerable<ReportModel>>(listEntity);
        }

        public static Entities.Customer.Report ToEntity(this ReportModel entity)
        {
            return Mapper.Map<ReportModel, Entities.Customer.Report>(entity);
        }

        public static Entities.Customer.Report ToEntity(this ReportModel model, Entities.Customer.Report destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Report

        #region ReportKey

        public static ReportKeyModel ToModel(this Entities.Customer.ReportKey entity)
        {
            return Mapper.Map<Entities.Customer.ReportKey, ReportKeyModel>(entity);
        }

        public static IEnumerable<ReportKeyModel> ToListModel(this IEnumerable<Entities.Customer.ReportKey> listEntity)
        {
            return Mapper.Map<IEnumerable<Entities.Customer.ReportKey>, IEnumerable<ReportKeyModel>>(listEntity);
        }

        public static Entities.Customer.ReportKey ToEntity(this ReportKeyModel entity)
        {
            return Mapper.Map<ReportKeyModel, Entities.Customer.ReportKey>(entity);
        }

        public static Entities.Customer.ReportKey ToEntity(this ReportKeyModel model, Entities.Customer.ReportKey destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Report
        #region Address

        public static AddressModel ToModel(this Entities.Customer.Address entity)
        {
            return Mapper.Map<Entities.Customer.Address, AddressModel>(entity);
        }

        public static IEnumerable<AddressModel> ToListModel(this IEnumerable<Entities.Customer.Address> listEntity)
        {
            return Mapper.Map<IEnumerable<Entities.Customer.Address>, IEnumerable<AddressModel>>(listEntity);
        }

        public static Entities.Customer.Address ToEntity(this AddressModel entity)
        {
            return Mapper.Map<AddressModel, Entities.Customer.Address>(entity);
        }

        public static Entities.Customer.Address ToEntity(this AddressModel model, Entities.Customer.Address destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Address

        #region Infomation

        public static InfomationModel ToModel(this Infomation entity)
        {
            return Mapper.Map<Infomation, InfomationModel>(entity);
        }

        public static IEnumerable<InfomationModel> ToListModel(this IEnumerable<Infomation> listEntity)
        {
            return Mapper.Map<IEnumerable<Infomation>, IEnumerable<InfomationModel>>(listEntity);
        }

        public static Infomation ToEntity(this InfomationModel entity)
        {
            return Mapper.Map<InfomationModel, Infomation>(entity);
        }

        public static Infomation ToEntity(this InfomationModel model, Infomation destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Infomation

        #region "FormGroup"

        public static FormGroupModel ToModel(this FormGroup entity)
        {
            return Mapper.Map<FormGroup, FormGroupModel>(entity);
        }

        public static IEnumerable<FormGroupModel> ToListModel(this IEnumerable<FormGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<FormGroup>, IEnumerable<FormGroupModel>>(listEntity);
        }

        public static FormGroup ToEntity(this FormGroupModel model)
        {
            return Mapper.Map<FormGroupModel, FormGroup>(model);
        }

        public static IEnumerable<FormGroup> ToListEntity(this IEnumerable<FormGroupModel> listModel)
        {
            return Mapper.Map<IEnumerable<FormGroupModel>, IEnumerable<FormGroup>>(listModel);
        }

        public static FormGroup ToEntity(this FormGroupModel model, FormGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "FormGroup"

        #region "BusinessType"

        public static BusinessTypeModel ToModel(this BusinessType entity)
        {
            return Mapper.Map<BusinessType, BusinessTypeModel>(entity);
        }

        public static IEnumerable<BusinessTypeModel> ToListModel(this IEnumerable<BusinessType> listEntity)
        {
            return Mapper.Map<IEnumerable<BusinessType>, IEnumerable<BusinessTypeModel>>(listEntity);
        }

        public static BusinessType ToEntity(this BusinessTypeModel model)
        {
            return Mapper.Map<BusinessTypeModel, BusinessType>(model);
        }

        public static BusinessType ToEntity(this BusinessTypeModel model, BusinessType destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "BusinessType"

        #region "City"

        public static CityModel ToModel(this City entity)
        {
            return Mapper.Map<City, CityModel>(entity);
        }

        public static IEnumerable<CityModel> ToListModel(this IEnumerable<City> listEntity)
        {
            return Mapper.Map<IEnumerable<City>, IEnumerable<CityModel>>(listEntity);
        }

        public static City ToEntity(this CityModel model)
        {
            return Mapper.Map<CityModel, City>(model);
        }

        public static City ToEntity(this CityModel model, City destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "City"

        #region "District"

        public static DistrictModel ToModel(this District entity)
        {
            return Mapper.Map<District, DistrictModel>(entity);
        }

        public static IEnumerable<DistrictModel> ToListModel(this IEnumerable<District> listEntity)
        {
            return Mapper.Map<IEnumerable<District>, IEnumerable<DistrictModel>>(listEntity);
        }

        public static District ToEntity(this DistrictModel model)
        {
            return Mapper.Map<DistrictModel, District>(model);
        }

        public static District ToEntity(this DistrictModel model, District destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "District"

        #region "Ward"

        public static WardModel ToModel(this Ward entity)
        {
            return Mapper.Map<Ward, WardModel>(entity);
        }

        public static IEnumerable<WardModel> ToListModel(this IEnumerable<Ward> listEntity)
        {
            return Mapper.Map<IEnumerable<Ward>, IEnumerable<WardModel>>(listEntity);
        }

        public static Ward ToEntity(this WardModel model)
        {
            return Mapper.Map<WardModel, Ward>(model);
        }

        public static Ward ToEntity(this WardModel model, Ward destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Ward"

        #region "KeyWord"

        public static KeyWordModel ToModel(this KeyWord entity)
        {
            return Mapper.Map<KeyWord, KeyWordModel>(entity);
        }

        public static IEnumerable<KeyWordModel> ToListModel(this IEnumerable<KeyWord> listEntity)
        {
            return Mapper.Map<IEnumerable<KeyWord>, IEnumerable<KeyWordModel>>(listEntity);
        }

        public static KeyWord ToEntity(this KeyWordModel model)
        {
            return Mapper.Map<KeyWordModel, KeyWord>(model);
        }

        public static KeyWord ToEntity(this KeyWordModel model, KeyWord destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "KeyWord"

        #region "TransferType"

        public static TransferTypeModel ToModel(this TransferType entity)
        {
            return Mapper.Map<TransferType, TransferTypeModel>(entity);
        }

        public static IEnumerable<TransferTypeModel> ToListModel(this IEnumerable<TransferType> listEntity)
        {
            return Mapper.Map<IEnumerable<TransferType>, IEnumerable<TransferTypeModel>>(listEntity);
        }

        public static TransferType ToEntity(this TransferTypeModel model)
        {
            return Mapper.Map<TransferTypeModel, TransferType>(model);
        }

        public static TransferType ToEntity(this TransferTypeModel model, TransferType destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "TransferType"

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

        #endregion "Printer"

        #region "ActivityLog"

        public static ActivityLogModel ToModel(this ActivityLog entity)
        {
            return Mapper.Map<ActivityLog, ActivityLogModel>(entity);
        }

        public static IEnumerable<ActivityLogModel> ToListModel(this IEnumerable<ActivityLog> listEntity)
        {
            return Mapper.Map<IEnumerable<ActivityLog>, IEnumerable<ActivityLogModel>>(listEntity);
        }

        public static ActivityLog ToEntity(this ActivityLogModel model)
        {
            return Mapper.Map<ActivityLogModel, ActivityLog>(model);
        }

        public static ActivityLog ToEntity(this ActivityLogModel model, ActivityLog destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "ActivityLog"

        #region "Level"

        public static LevelModel ToModel(this Administrative entity)
        {
            return Mapper.Map<Administrative, LevelModel>(entity);
        }

        public static IEnumerable<LevelModel> ToListModel(this IEnumerable<Administrative> listEntity)
        {
            return Mapper.Map<IEnumerable<Administrative>, IEnumerable<LevelModel>>(listEntity);
        }

        public static Administrative ToEntity(this LevelModel model)
        {
            return Mapper.Map<LevelModel, Administrative>(model);
        }

        public static Administrative ToEntity(this LevelModel model, Administrative destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Level"

        #region "Law"

        public static LawModel ToModel(this Law entity)
        {
            return Mapper.Map<Law, LawModel>(entity);
        }

        public static IEnumerable<LawModel> ToListModel(this IEnumerable<Law> listEntity)
        {
            return Mapper.Map<IEnumerable<Law>, IEnumerable<LawModel>>(listEntity);
        }

        public static Law ToEntity(this LawModel model)
        {
            return Mapper.Map<LawModel, Law>(model);
        }

        public static Law ToEntity(this LawModel model, Law destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Law"

        #region "Client"

        //public static ClientModel ToModel(this Client entity)
        //{
        //    return Mapper.Map<Client, ClientModel>(entity);
        //}

        //public static IEnumerable<ClientModel> ToListModel(this IEnumerable<Client> listEntity)
        //{
        //    return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientModel>>(listEntity);
        //}

        //public static Client ToEntity(this ClientModel model)
        //{
        //    return Mapper.Map<ClientModel, Client>(model);
        //}

        //public static Client ToEntity(this ClientModel model, Client destination)
        //{
        //    return Mapper.Map(model, destination);
        //}

        #endregion "Client"

        #region "People"

        public static CitizenModel ToModel(this Citizen entity)
        {
            return Mapper.Map<Citizen, CitizenModel>(entity);
        }

        public static IEnumerable<CitizenModel> ToListModel(this IEnumerable<Citizen> listEntity)
        {
            return Mapper.Map<IEnumerable<Citizen>, IEnumerable<CitizenModel>>(listEntity);
        }

        public static Citizen ToEntity(this CitizenModel model)
        {
            return Mapper.Map<CitizenModel, Citizen>(model);
        }

        public static Citizen ToEntity(this CitizenModel model, Citizen destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "People"

        #region "ScopeArea"

        public static ScopeAreaModel ToModel(this ScopeArea entity)
        {
            return Mapper.Map<ScopeArea, ScopeAreaModel>(entity);
        }

        public static IEnumerable<ScopeAreaModel> ToListModel(this IEnumerable<ScopeArea> listEntity)
        {
            return Mapper.Map<IEnumerable<ScopeArea>, IEnumerable<ScopeAreaModel>>(listEntity);
        }

        public static ScopeArea ToEntity(this ScopeAreaModel model)
        {
            return Mapper.Map<ScopeAreaModel, ScopeArea>(model);
        }

        public static ScopeArea ToEntity(this ScopeAreaModel model, ScopeArea destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "ScopeArea"

        #region "ClientScope"

        public static ClientScopeModel ToModel(this ClientScope entity)
        {
            return Mapper.Map<ClientScope, ClientScopeModel>(entity);
        }

        public static IEnumerable<ClientScopeModel> ToListModel(this IEnumerable<ClientScope> listEntity)
        {
            return Mapper.Map<IEnumerable<ClientScope>, IEnumerable<ClientScopeModel>>(listEntity);
        }

        public static ClientScope ToEntity(this ClientScopeModel model)
        {
            return Mapper.Map<ClientScopeModel, ClientScope>(model);
        }

        public static ClientScope ToEntity(this ClientScopeModel model, ClientScope destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "ClientScope"

        #region "Guide"

        public static GuideModel ToModel(this Guide entity)
        {
            return Mapper.Map<Guide, GuideModel>(entity);
        }

        public static IEnumerable<GuideModel> ToListModel(this IEnumerable<Guide> listEntity)
        {
            return Mapper.Map<IEnumerable<Guide>, IEnumerable<GuideModel>>(listEntity);
        }

        public static Guide ToEntity(this GuideModel model)
        {
            return Mapper.Map<GuideModel, Guide>(model);
        }

        public static Guide ToEntity(this GuideModel model, Guide destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Guide"

        #region "Notify"

        public static NotifyModel ToModel(this Notify entity)
        {
            return Mapper.Map<Notify, NotifyModel>(entity);
        }

        public static IEnumerable<NotifyModel> ToListModel(this IEnumerable<Notify> listEntity)
        {
            return Mapper.Map<IEnumerable<Notify>, IEnumerable<NotifyModel>>(listEntity);
        }

        public static Notify ToEntity(this NotifyModel model)
        {
            return Mapper.Map<NotifyModel, Notify>(model);
        }

        public static Notify ToEntity(this NotifyModel model, Notify destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Notify"

        #region "Office"

        public static OfficeModel ToModel(this Office entity)
        {
            return Mapper.Map<Office, OfficeModel>(entity);
        }

        public static IEnumerable<OfficeModel> ToListModel(this IEnumerable<Office> listEntity)
        {
            return Mapper.Map<IEnumerable<Office>, IEnumerable<OfficeModel>>(listEntity);
        }

        public static Office ToEntity(this OfficeModel model)
        {
            return Mapper.Map<OfficeModel, Office>(model);
        }

        public static Office ToEntity(this OfficeModel model, Office destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Office"

        #region "DoctypeTemplate"

        public static DoctypeTemplateModel ToModel(this DoctypeTemplate entity)
        {
            return Mapper.Map<DoctypeTemplate, DoctypeTemplateModel>(entity);
        }

        public static IEnumerable<DoctypeTemplateModel> ToListModel(this IEnumerable<DoctypeTemplate> listEntity)
        {
            return Mapper.Map<IEnumerable<DoctypeTemplate>, IEnumerable<DoctypeTemplateModel>>(listEntity);
        }

        public static DoctypeTemplate ToEntity(this DoctypeTemplateModel model)
        {
            return Mapper.Map<DoctypeTemplateModel, DoctypeTemplate>(model);
        }

        public static DoctypeTemplate ToEntity(this DoctypeTemplateModel model, DoctypeTemplate destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "DoctypeTemplate"

        #region "OnlineTemplate"

        public static OnlineTemplateModel ToModel(this OnlineTemplate entity)
        {
            return Mapper.Map<OnlineTemplate, OnlineTemplateModel>(entity);
        }

        public static IEnumerable<OnlineTemplateModel> ToListModel(this IEnumerable<OnlineTemplate> listEntity)
        {
            return Mapper.Map<IEnumerable<OnlineTemplate>, IEnumerable<OnlineTemplateModel>>(listEntity);
        }

        public static OnlineTemplate ToEntity(this OnlineTemplateModel model)
        {
            return Mapper.Map<OnlineTemplateModel, OnlineTemplate>(model);
        }

        public static OnlineTemplate ToEntity(this OnlineTemplateModel model, OnlineTemplate destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "OnlineTemplate"

        #region "Question"

        public static QuestionModel ToModel(this Question entity)
        {
            return Mapper.Map<Question, QuestionModel>(entity);
        }

        public static List<QuestionModel> ToListModel(this IEnumerable<Question> listEntity)
        {
            return Mapper.Map<IEnumerable<Question>, List<QuestionModel>>(listEntity);
        }

        public static Question ToEntity(this QuestionModel model)
        {
            return Mapper.Map<QuestionModel, Question>(model);
        }

        public static Question ToEntity(this QuestionModel model, Question destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Question"

        #region "EgovJob"

        public static EgovJobModel ToModel(this EgovJob entity)
        {
            return Mapper.Map<EgovJob, EgovJobModel>(entity);
        }

        public static IEnumerable<EgovJobModel> ToListModel(this IEnumerable<EgovJob> listEntity)
        {
            return Mapper.Map<IEnumerable<EgovJob>, IEnumerable<EgovJobModel>>(listEntity);
        }

        public static EgovJob ToEntity(this EgovJobModel model)
        {
            return Mapper.Map<EgovJobModel, EgovJob>(model);
        }

        public static EgovJob ToEntity(this EgovJobModel model, EgovJob destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "EgovJob"

        #region "RequiredSupplementary"

        public static RequiredSupplementaryModel ToModel(this RequiredSupplementary entity)
        {
            return Mapper.Map<RequiredSupplementary, RequiredSupplementaryModel>(entity);
        }

        public static IEnumerable<RequiredSupplementaryModel> ToListModel(this IEnumerable<RequiredSupplementary> listEntity)
        {
            return Mapper.Map<IEnumerable<RequiredSupplementary>, IEnumerable<RequiredSupplementaryModel>>(listEntity);
        }

        public static RequiredSupplementary ToEntity(this RequiredSupplementaryModel model)
        {
            return Mapper.Map<RequiredSupplementaryModel, RequiredSupplementary>(model);
        }

        public static RequiredSupplementary ToEntity(this RequiredSupplementaryModel model, RequiredSupplementary destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "EgovJob"

        #region "TimeJob"

        public static TimeJobModel ToModel(this TimeJob entity)
        {
            return Mapper.Map<TimeJob, TimeJobModel>(entity);
        }

        public static IEnumerable<TimeJobModel> ToListModel(this IEnumerable<TimeJob> listEntity)
        {
            return Mapper.Map<IEnumerable<TimeJob>, IEnumerable<TimeJobModel>>(listEntity);
        }

        public static TimeJob ToEntity(this TimeJobModel model)
        {
            return Mapper.Map<TimeJobModel, TimeJob>(model);
        }

        public static TimeJob ToEntity(this TimeJobModel model, TimeJob destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "TimeJob"

        #region "DocTypeTimeJob"

        public static DocTypeTimeJobModel ToModel(this DocTypeTimeJob entity)
        {
            return Mapper.Map<DocTypeTimeJob, DocTypeTimeJobModel>(entity);
        }

        public static IEnumerable<DocTypeTimeJobModel> ToListModel(this IEnumerable<DocTypeTimeJob> listEntity)
        {
            return Mapper.Map<IEnumerable<DocTypeTimeJob>, IEnumerable<DocTypeTimeJobModel>>(listEntity);
        }

        public static DocTypeTimeJob ToEntity(this DocTypeTimeJobModel model)
        {
            return Mapper.Map<DocTypeTimeJobModel, DocTypeTimeJob>(model);
        }

        public static DocTypeTimeJob ToEntity(this DocTypeTimeJobModel model, DocTypeTimeJob destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "DocTypeTimeJob"

        #region "BackupRestoreConfig"

        public static BackupRestoreConfigModel ToModel(this BackupRestoreConfig entity)
        {
            return Mapper.Map<BackupRestoreConfig, BackupRestoreConfigModel>(entity);
        }

        public static IEnumerable<BackupRestoreConfigModel> ToListModel(this IEnumerable<BackupRestoreConfig> listEntity)
        {
            return Mapper.Map<IEnumerable<BackupRestoreConfig>, IEnumerable<BackupRestoreConfigModel>>(listEntity);
        }

        public static BackupRestoreConfig ToEntity(this BackupRestoreConfigModel model)
        {
            return Mapper.Map<BackupRestoreConfigModel, BackupRestoreConfig>(model);
        }

        public static BackupRestoreConfig ToEntity(this BackupRestoreConfigModel model, BackupRestoreConfig destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "BackupRestoreConfig"

        #region "BackupRestoreHistory"

        public static BackupRestoreHistoryModel ToModel(this BackupRestoreHistory entity)
        {
            return Mapper.Map<BackupRestoreHistory, BackupRestoreHistoryModel>(entity);
        }

        public static IEnumerable<BackupRestoreHistoryModel> ToListModel(this IEnumerable<BackupRestoreHistory> listEntity)
        {
            return Mapper.Map<IEnumerable<BackupRestoreHistory>, IEnumerable<BackupRestoreHistoryModel>>(listEntity);
        }

        public static BackupRestoreHistory ToEntity(this BackupRestoreHistoryModel model)
        {
            return Mapper.Map<BackupRestoreHistoryModel, BackupRestoreHistory>(model);
        }

        public static BackupRestoreHistory ToEntity(this BackupRestoreHistoryModel model, BackupRestoreHistory destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "BackupRestoreHistory"

        #region "ShareFolder"

        public static ShareFolderModel ToModel(this ShareFolder entity)
        {
            return Mapper.Map<ShareFolder, ShareFolderModel>(entity);
        }

        public static IEnumerable<ShareFolderModel> ToListModel(this IEnumerable<ShareFolder> listEntity)
        {
            return Mapper.Map<IEnumerable<ShareFolder>, IEnumerable<ShareFolderModel>>(listEntity);
        }

        public static ShareFolder ToEntity(this ShareFolderModel model)
        {
            return Mapper.Map<ShareFolderModel, ShareFolder>(model);
        }

        public static ShareFolder ToEntity(this ShareFolderModel model, ShareFolder destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "ShareFolder"

        #region "BackupRestoreFileConfig"

        public static BackupRestoreFileConfigModel ToModel(this BackupRestoreFileConfig entity)
        {
            return Mapper.Map<BackupRestoreFileConfig, BackupRestoreFileConfigModel>(entity);
        }

        public static IEnumerable<BackupRestoreFileConfigModel> ToListModel(this IEnumerable<BackupRestoreFileConfig> listEntity)
        {
            return Mapper.Map<IEnumerable<BackupRestoreFileConfig>, IEnumerable<BackupRestoreFileConfigModel>>(listEntity);
        }

        public static BackupRestoreFileConfig ToEntity(this BackupRestoreFileConfigModel model)
        {
            return Mapper.Map<BackupRestoreFileConfigModel, BackupRestoreFileConfig>(model);
        }

        public static BackupRestoreFileConfig ToEntity(this BackupRestoreFileConfigModel model, BackupRestoreFileConfig destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "BackupRestoreFileConfig"

        #region "BackupRestoreManagerModel"

        public static BackupRestoreManagerModel ToModel(this BackupRestoreManager entity)
        {
            return Mapper.Map<BackupRestoreManager, BackupRestoreManagerModel>(entity);
        }

        public static IEnumerable<BackupRestoreManagerModel> ToListModel(this IEnumerable<BackupRestoreManager> listEntity)
        {
            return Mapper.Map<IEnumerable<BackupRestoreManager>, IEnumerable<BackupRestoreManagerModel>>(listEntity);
        }

        public static BackupRestoreManager ToEntity(this BackupRestoreManagerModel model)
        {
            return Mapper.Map<BackupRestoreManagerModel, BackupRestoreManager>(model);
        }

        public static BackupRestoreManager ToEntity(this BackupRestoreManagerModel model, BackupRestoreManager destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "BackupRestoreManagerModel"

        #region "Statistics"

        public static StatisticsModel ToModel(this Statistics entity)
        {
            return Mapper.Map<Statistics, StatisticsModel>(entity);
        }

        public static IEnumerable<StatisticsModel> ToListModel(this IEnumerable<Statistics> listEntity)
        {
            return Mapper.Map<IEnumerable<Statistics>, IEnumerable<StatisticsModel>>(listEntity);
        }

        public static Statistics ToEntity(this StatisticsModel model)
        {
            return Mapper.Map<StatisticsModel, Statistics>(model);
        }

        public static Statistics ToEntity(this StatisticsModel model, Statistics destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Statistics"

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

        #region "TreeGroup"

        public static TreeGroupModel ToModel(this TreeGroup entity)
        {
            return Mapper.Map<TreeGroup, TreeGroupModel>(entity);
        }

        public static IEnumerable<TreeGroupModel> ToListModel(this IEnumerable<TreeGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<TreeGroup>, IEnumerable<TreeGroupModel>>(listEntity);
        }

        public static TreeGroup ToEntity(this TreeGroupModel model)
        {
            return Mapper.Map<TreeGroupModel, TreeGroup>(model);
        }

        public static TreeGroup ToEntity(this TreeGroupModel model, TreeGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<TreeGroup> ToListEntity(this IEnumerable<TreeGroupModel> listmodel)
        {
            return Mapper.Map<IEnumerable<TreeGroupModel>, IEnumerable<TreeGroup>>(listmodel);
        }

        #endregion

        #region "DocColumnSetting"

        public static DocColumnSettingModel ToModel(this DocColumnSetting entity)
        {
            return Mapper.Map<DocColumnSetting, DocColumnSettingModel>(entity);
        }

        public static IEnumerable<DocColumnSettingModel> ToListModel(this IEnumerable<DocColumnSetting> listEntity)
        {
            return Mapper.Map<IEnumerable<DocColumnSetting>, IEnumerable<DocColumnSettingModel>>(listEntity);
        }

        public static DocColumnSetting ToEntity(this DocColumnSettingModel model)
        {
            return Mapper.Map<DocColumnSettingModel, DocColumnSetting>(model);
        }

        public static DocColumnSetting ToEntity(this DocColumnSettingModel model, DocColumnSetting destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<DocColumnSetting> ToListEntity(this IEnumerable<DocColumnSettingModel> listmodel)
        {
            return Mapper.Map<IEnumerable<DocColumnSettingModel>, IEnumerable<DocColumnSetting>>(listmodel);
        }

        #endregion

        #region "PermissionSetting"

        public static PermissionSettingModel ToModel(this PermissionSetting entity)
        {
            return Mapper.Map<PermissionSetting, PermissionSettingModel>(entity);
        }

        public static IEnumerable<PermissionSettingModel> ToListModel(this IEnumerable<PermissionSetting> listEntity)
        {
            return Mapper.Map<IEnumerable<PermissionSetting>, IEnumerable<PermissionSettingModel>>(listEntity);
        }

        public static PermissionSetting ToEntity(this PermissionSettingModel model)
        {
            return Mapper.Map<PermissionSettingModel, PermissionSetting>(model);
        }

        public static PermissionSetting ToEntity(this PermissionSettingModel model, PermissionSetting destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<PermissionSetting> ToListEntity(this IEnumerable<PermissionSettingModel> listmodel)
        {
            return Mapper.Map<IEnumerable<PermissionSettingModel>, IEnumerable<PermissionSetting>>(listmodel);
        }

        #endregion

        #region "BussinessDocFieldDocTypeGroup"

        public static BussinessDocFieldDocTypeGroupModel ToModel(this BussinessDocFieldDocTypeGroup entity)
        {
            return Mapper.Map<BussinessDocFieldDocTypeGroup, BussinessDocFieldDocTypeGroupModel>(entity);
        }

        public static IEnumerable<BussinessDocFieldDocTypeGroupModel> ToListModel(this IEnumerable<BussinessDocFieldDocTypeGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<BussinessDocFieldDocTypeGroup>, IEnumerable<BussinessDocFieldDocTypeGroupModel>>(listEntity);
        }

        public static BussinessDocFieldDocTypeGroup ToEntity(this BussinessDocFieldDocTypeGroupModel model)
        {
            return Mapper.Map<BussinessDocFieldDocTypeGroupModel, BussinessDocFieldDocTypeGroup>(model);
        }

        public static BussinessDocFieldDocTypeGroup ToEntity(this BussinessDocFieldDocTypeGroupModel model, BussinessDocFieldDocTypeGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "BussinessDocFieldDocTypeGroup"

        #region "NotifyConfig"

        public static NotifyConfigModel ToModel(this NotifyConfig entity)
        {
            return Mapper.Map<NotifyConfig, NotifyConfigModel>(entity);
        }

        public static IEnumerable<NotifyConfigModel> ToListModel(this IEnumerable<NotifyConfig> listEntity)
        {
            return Mapper.Map<IEnumerable<NotifyConfig>, IEnumerable<NotifyConfigModel>>(listEntity);
        }

        public static NotifyConfig ToEntity(this NotifyConfigModel model)
        {
            return Mapper.Map<NotifyConfigModel, NotifyConfig>(model);
        }

        public static NotifyConfig ToEntity(this NotifyConfigModel model, NotifyConfig destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "NotifyConfig"

        #region Interface

        public static InterfaceConfigModel ToModel(this InterfaceConfig entity)
        {
            return Mapper.Map<InterfaceConfig, InterfaceConfigModel>(entity);
        }

        public static IEnumerable<InterfaceConfigModel> ToListModel(this IEnumerable<InterfaceConfig> listEntity)
        {
            return Mapper.Map<IEnumerable<InterfaceConfig>, IEnumerable<InterfaceConfigModel>>(listEntity);
        }

        public static InterfaceConfig ToEntity(this InterfaceConfigModel entity)
        {
            return Mapper.Map<InterfaceConfigModel, InterfaceConfig>(entity);
        }

        public static InterfaceConfig ToEntity(this InterfaceConfigModel model, InterfaceConfig destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion Interface

        #endregion "Customer"

        #region "Domain"

        public static DomainModel ToModel(this Domain entity)
        {
            return Mapper.Map<Domain, DomainModel>(entity);
        }

        public static IEnumerable<DomainModel> ToListModel(this IEnumerable<Domain> listEntity)
        {
            return Mapper.Map<IEnumerable<Domain>, IEnumerable<DomainModel>>(listEntity);
        }

        public static Domain ToEntity(this DomainModel model)
        {
            return Mapper.Map<DomainModel, Domain>(model);
        }

        public static Domain ToEntity(this DomainModel model, Domain destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Domain"

        #region "Connection"

        public static Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel ToModel(this Connection entity)
        {
            return Mapper.Map<Connection, Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel>(entity);
        }

        public static IEnumerable<Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel> ToListModel(this IEnumerable<Connection> listEntity)
        {
            return Mapper.Map<IEnumerable<Connection>, IEnumerable<Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel>>(listEntity);
        }

        public static Connection ToEntity(this Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel model)
        {
            return Mapper.Map<Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel, Connection>(model);
        }

        public static Connection ToEntity(this Bkav.eGovCloud.Areas.Admin.Models.ConnectionModel model, Connection destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Connection"

        #region Account

        public static Bkav.eGovCloud.Areas.Admin.Models.AccountModel ToModel(this Account entity)
        {
            return Mapper.Map<Account, Bkav.eGovCloud.Areas.Admin.Models.AccountModel>(entity);
        }

        public static IEnumerable<Bkav.eGovCloud.Areas.Admin.Models.AccountModel> ToListModel(this IEnumerable<Account> listEntity)
        {
            return Mapper.Map<IEnumerable<Account>, IEnumerable<Bkav.eGovCloud.Areas.Admin.Models.AccountModel>>(listEntity);
        }

        #endregion

        #region SSOSetting
        public static SSOSettingsModel ToModel(this SSOSettings entity)
        {
            return Mapper.Map<SSOSettings, SSOSettingsModel>(entity);
        }
        public static SSOSettings ToEntity(this SSOSettingsModel model)
        {
            return Mapper.Map<SSOSettingsModel, SSOSettings>(model);
        }

        public static SSOSettings ToEntity(this SSOSettingsModel model, SSOSettings destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region DataSource
        public static DataSourceModel ToModel(this DataSource entity)
        {
            return Mapper.Map<DataSource, DataSourceModel>(entity);
        }

        public static IEnumerable<DataSourceModel> ToListModel(this IEnumerable<DataSource> listEntity)
        {
            return Mapper.Map<IEnumerable<DataSource>, IEnumerable<DataSourceModel>>(listEntity);
        }

        public static DataSource ToEntity(this DataSourceModel model)
        {
            return Mapper.Map<DataSourceModel, DataSource>(model);
        }

        public static DataSource ToEntity(this DataSourceModel model, DataSource destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<DataSource> ToEntity(this IEnumerable<DataSourceModel> model)
        {
            return Mapper.Map<IEnumerable<DataSourceModel>, IEnumerable<DataSource>>(model);
        }
        #endregion

        #region ReportQuery
        public static ReportQueryModel ToModel(this ReportQuery entity)
        {
            return Mapper.Map<ReportQuery, ReportQueryModel>(entity);
        }

        public static IEnumerable<ReportQueryModel> ToListModel(this IEnumerable<ReportQuery> listEntity)
        {
            return Mapper.Map<IEnumerable<ReportQuery>, IEnumerable<ReportQueryModel>>(listEntity);
        }

        public static ReportQuery ToEntity(this ReportQueryModel model)
        {
            return Mapper.Map<ReportQueryModel, ReportQuery>(model);
        }

        public static ReportQuery ToEntity(this ReportQueryModel model, ReportQuery destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<ReportQuery> ToEntity(this IEnumerable<ReportQueryModel> model)
        {
            return Mapper.Map<IEnumerable<ReportQueryModel>, IEnumerable<ReportQuery>>(model);
        }
        #endregion

        #region ReportQueryFilter
        public static ReportQueryFilterModel ToModel(this ReportQueryFilter entity)
        {
            return Mapper.Map<ReportQueryFilter, ReportQueryFilterModel>(entity);
        }

        public static IEnumerable<ReportQueryFilterModel> ToListModel(this IEnumerable<ReportQueryFilter> listEntity)
        {
            return Mapper.Map<IEnumerable<ReportQueryFilter>, IEnumerable<ReportQueryFilterModel>>(listEntity);
        }

        public static ReportQueryFilter ToEntity(this ReportQueryFilterModel model)
        {
            return Mapper.Map<ReportQueryFilterModel, ReportQueryFilter>(model);
        }

        public static ReportQueryFilter ToEntity(this ReportQueryFilterModel model, ReportQueryFilter destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<ReportQueryFilter> ToEntity(this IEnumerable<ReportQueryFilterModel> model)
        {
            return Mapper.Map<IEnumerable<ReportQueryFilterModel>, IEnumerable<ReportQueryFilter>>(model);
        }

        public static IEnumerable<ReportQueryFilter> ToListEntity(this IEnumerable<ReportQueryFilterModel> listModel)
        {
            return Mapper.Map<IEnumerable<ReportQueryFilterModel>, IEnumerable<ReportQueryFilter>>(listModel);
        }
        #endregion

        #region ReportQueryGroup
        public static ReportQueryGroupModel ToModel(this ReportQueryGroup entity)
        {
            return Mapper.Map<ReportQueryGroup, ReportQueryGroupModel>(entity);
        }

        public static IEnumerable<ReportQueryGroupModel> ToListModel(this IEnumerable<ReportQueryGroup> listEntity)
        {
            return Mapper.Map<IEnumerable<ReportQueryGroup>, IEnumerable<ReportQueryGroupModel>>(listEntity);
        }

        public static ReportQueryGroup ToEntity(this ReportQueryGroupModel model)
        {
            return Mapper.Map<ReportQueryGroupModel, ReportQueryGroup>(model);
        }

        public static ReportQueryGroup ToEntity(this ReportQueryGroupModel model, ReportQueryGroup destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<ReportQueryGroup> ToEntity(this IEnumerable<ReportQueryGroupModel> model)
        {
            return Mapper.Map<IEnumerable<ReportQueryGroupModel>, IEnumerable<ReportQueryGroup>>(model);
        }
        #endregion

        #region MissionSettings
        public static MissionSettingsModel ToModel(this MissionSettings entity)
        {
            return Mapper.Map<MissionSettings, MissionSettingsModel>(entity);
        }
        public static MissionSettings ToEntity(this MissionSettingsModel model)
        {
            return Mapper.Map<MissionSettingsModel, MissionSettings>(model);
        }
        public static MissionSettings ToEntity(this MissionSettingsModel model, MissionSettings destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region ChatSettings
        public static ChatSettingsModel ToModel(this ChatSettings entity)
        {
            return Mapper.Map<ChatSettings, ChatSettingsModel>(entity);
        }
        public static ChatSettings ToEntity(this ChatSettingsModel model)
        {
            return Mapper.Map<ChatSettingsModel, ChatSettings>(model);
        }
        public static ChatSettings ToEntity(this ChatSettingsModel model, ChatSettings destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region ReportConfig
        public static ReportConfigSettingsModel ToModel(this ReportConfigSettings entity)
        {
            return Mapper.Map<ReportConfigSettings, ReportConfigSettingsModel>(entity);
        }
        public static ReportConfigSettings ToEntity(this ReportConfigSettingsModel model)
        {
            return Mapper.Map<ReportConfigSettingsModel, ReportConfigSettings>(model);
        }
        public static ReportConfigSettings ToEntity(this ReportConfigSettingsModel model, ReportConfigSettings destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region SSOApiSettings
        public static SSOAPISettingsModel ToModel(this SSOAPISettings entity)
        {
            return Mapper.Map<SSOAPISettings, SSOAPISettingsModel>(entity);
        }
        public static SSOAPISettings ToEntity(this SSOAPISettingsModel model)
        {
            return Mapper.Map<SSOAPISettingsModel, SSOAPISettings>(model);
        }
        public static SSOAPISettings ToEntity(this SSOAPISettingsModel model, SSOAPISettings destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region "Locality"

        public static LocalityModel ToModel(this Locality entity)
        {
            return Mapper.Map<Locality, LocalityModel>(entity);
        }

        public static IEnumerable<LocalityModel> ToListModel(this IEnumerable<Locality> listEntity)
        {
            return Mapper.Map<IEnumerable<Locality>, IEnumerable<LocalityModel>>(listEntity);
        }

        public static Locality ToEntity(this LocalityModel model)
        {
            return Mapper.Map<LocalityModel, Locality>(model);
        }

        public static Locality ToEntity(this LocalityModel model, Locality destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "Locality"

        #region "ActionLevel"

        public static ActionLevelModel ToModel(this ActionLevel entity)
        {
            return Mapper.Map<ActionLevel, ActionLevelModel>(entity);
        }

        public static IEnumerable<ActionLevelModel> ToListModel(this IEnumerable<ActionLevel> listEntity)
        {
            return Mapper.Map<IEnumerable<ActionLevel>, IEnumerable<ActionLevelModel>>(listEntity);
        }

        public static ActionLevel ToEntity(this ActionLevelModel model)
        {
            return Mapper.Map<ActionLevelModel, ActionLevel>(model);
        }

        public static ActionLevel ToEntity(this ActionLevelModel model, ActionLevel destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "ActionLevel"

        #region "LocalityDepartment"

        public static LocalityDepartmentModel ToModel(this LocalityDepartment entity)
        {
            return Mapper.Map<LocalityDepartment, LocalityDepartmentModel>(entity);
        }

        public static IEnumerable<LocalityDepartmentModel> ToListModel(this IEnumerable<LocalityDepartment> listEntity)
        {
            return Mapper.Map<IEnumerable<LocalityDepartment>, IEnumerable<LocalityDepartmentModel>>(listEntity);
        }

        public static LocalityDepartment ToEntity(this LocalityDepartmentModel model)
        {
            return Mapper.Map<LocalityDepartmentModel, LocalityDepartment>(model);
        }

        public static LocalityDepartment ToEntity(this LocalityDepartmentModel model, LocalityDepartment destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion "LocalityDepartment"

        #region "SurveyCatalog"

        public static SurveyCatalogModel ToModel(this SurveyCatalog entity)
        {
            return Mapper.Map<SurveyCatalog, SurveyCatalogModel>(entity);
        }

        public static IEnumerable<SurveyCatalogModel> ToListModel(this IEnumerable<SurveyCatalog> listEntity)
        {
            return Mapper.Map<IEnumerable<SurveyCatalog>, IEnumerable<SurveyCatalogModel>>(listEntity);
        }

        public static SurveyCatalog ToEntity(this SurveyCatalogModel model)
        {
            return Mapper.Map<SurveyCatalogModel, SurveyCatalog>(model);
        }

        public static SurveyCatalog ToEntity(this SurveyCatalogModel model, SurveyCatalog destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<SurveyCatalog> ToEntity(this IEnumerable<SurveyCatalogModel> model)
        {
            return Mapper.Map<IEnumerable<SurveyCatalogModel>, IEnumerable<SurveyCatalog>>(model);
        }

        public static SurveyCatalogValueModel ToModel(this SurveyCatalogValue entity)
        {
            return Mapper.Map<SurveyCatalogValue, SurveyCatalogValueModel>(entity);
        }

        public static IEnumerable<SurveyCatalogValueModel> ToListModel(this IEnumerable<SurveyCatalogValue> listEntity)
        {
            return Mapper.Map<IEnumerable<SurveyCatalogValue>, IEnumerable<SurveyCatalogValueModel>>(listEntity);
        }

        public static SurveyCatalogValue ToEntity(this SurveyCatalogValueModel model)
        {
            return Mapper.Map<SurveyCatalogValueModel, SurveyCatalogValue>(model);
        }

        public static SurveyCatalogValue ToEntity(this SurveyCatalogValueModel model, SurveyCatalogValue destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<SurveyCatalogValue> ToEntity(this IEnumerable<SurveyCatalogValueModel> model)
        {
            return Mapper.Map<IEnumerable<SurveyCatalogValueModel>, IEnumerable<SurveyCatalogValue>>(model);
        }

        #endregion "SurveyCatalog"
        #region "Ad_Unit"

        public static Ad_UnitModel ToModel(this Ad_Unit entity)
        {
            return Mapper.Map<Ad_Unit, Ad_UnitModel>(entity);
        }

        public static IEnumerable<Ad_UnitModel> ToListModel(this IEnumerable<Ad_Unit> listEntity)
        {
            return Mapper.Map<IEnumerable<Ad_Unit>, IEnumerable<Ad_UnitModel>>(listEntity);
        }

        public static Ad_Unit ToEntity(this Ad_UnitModel model)
        {
            return Mapper.Map<Ad_UnitModel, Ad_Unit>(model);
        }

        public static Ad_Unit ToEntity(this Ad_UnitModel model, Ad_Unit destination)
        {
            return Mapper.Map(model, destination);
        }

        public static IEnumerable<Ad_Unit> ToEntity(this IEnumerable<Ad_UnitModel> model)
        {
            return Mapper.Map<IEnumerable<Ad_UnitModel>, IEnumerable<Ad_Unit>>(model);
        }

        #endregion "Ad_Unit"

        #region "datatype"
        public static dataTypeModel ToModel(this dataType entity)
        {
            return Mapper.Map<dataType, dataTypeModel>(entity);
        }

        public static IEnumerable<dataTypeModel> ToListModel(this IEnumerable<dataType> listEntity)
        {
            return Mapper.Map<IEnumerable<dataType>, IEnumerable<dataTypeModel>>(listEntity);
        }

        public static dataType ToEntity(this dataTypeModel model)
        {
            return Mapper.Map<dataTypeModel, dataType>(model);
        }

        public static dataType ToEntity(this dataTypeModel model, dataType destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion "datatype"

    }
}