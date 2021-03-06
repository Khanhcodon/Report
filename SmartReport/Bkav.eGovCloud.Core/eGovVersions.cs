using System;
using System.Linq;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Core
{
    /// <summary>
    /// Danh sách các phiên bản của eGov
    /// </summary>
    public static class eGovVersions
    {
        private static readonly List<eVersion> _versionChangeHistory = new List<eVersion>()
        {
            // Phiên bản gốc mặc định
            new eVersion("1.0.0"),

            // Todo: cần thay đổi việc xử lý đường dẫn ở đây thành câu lệnh sql fix trong code luôn.
            // Tránh việc người khác sửa sql ngoài kiểu như xóa toàn bộ dữ liệu và chạy update chẳng hạn.
            // Ngoài ra việc cầu hình chính câu sql cũng giúp việc build các phiên bản nó tiện hơn.
            //new eVersion("1.0.09", 
            //    new List<string>(){
            //    "20161107_DocumentCopy_TienBV.sql",
            //    "20161108_templatekey_TienBV.sql",
            //    "20161117_search_update_TienBV.sql",
            //    "20161203_docrelation_TienBV.sql",
            //    "20161208_docrelation_TrinhNVd.sql",
            //    "20161208_approver_TrinhNVd.sql",
            //    "20161208_Comment_TrinhNVd.sql",
            //    "20161220_docextendfield_TienBV.sql",
            //    "20161224_DocumentCopy_TienBV.sql",
            //    "20161224_notification_TrinhNVd.sql",
            //    "20161224_processfunction_vanban_TienBV.sql",
            //    "20170123_processfunction_ho_so_TienBV.sql",
            //    "20170123_Update BRVT_Full_TienBV.sql"

            //}),
            //new eVersion("1.0.10", 
            //    new List<string>(){
            //    "20161224_mobiledevice_TrinhNVd.sql",
            //    "20170220_functions_TienBV.sql",
            //    "20170222_resource-TrinhNVd.sql",
            //    "20170224_user_TienBV.sql",
            //    "20170313_document_TienBV.sql",
            //    "20170315_docextendfield_TienBV.sql",
            //    "20170320_department_TienBV.sql",
            //    "20170416_notifications_TienBV.sql",
            //    "20170420_notifications_TienBV.sql",
            //    "20170404_report_canhan_DungNVl.sql",
            //    "20170517_report_phanloai_DungNVl.sql"
            //}),

            //new eVersion("1.0.11",
            //    new List<string>(){
            //    "20170420_userdept_TienBV.sql",
            //    "20170425_notifications_TienBV.sql",
            //    "20170428_user_notifyinfo_TienBV.sql",
            //    "20170505_storeprivateuser_TienBV.sql",
            //    "20170515_report_TienBV.sql",
            //    "20170517_documentcopy_TienBV.sql",
            //    "20170517_document_TienBV.sql",
            //    "20170524_document_locationreturn_DungNVl.sql"
            //}),
                        
            //new eVersion("1.0.12",
            //    new List<string>(){
            //    "20170522_documentcopy_TienBV.sql",
            //    "20170522_documentcopy_updatedocusers_TienBV.sql",
            //    "20170522_report_TienBV.sql",
            //    "20170523_treegroup_TienBV.sql",
            //    "20170525_processfunction_TienBV.sql",
            //    "20170530_notifications_TienBV.sql"
            //}),
            
            //new eVersion("1.0.13",
            //    new List<string>(){
            //    "20170530_processfunction_TienBV.sql",
            //    "20170530_search_TienBV.sql",
            //    "20170531_infomation_TienBV.sql",
            //    "20170531_processfunctions_TienBV.sql",
            //    "20170531_report_TienBV.sql",
            //    "20170531_usersuccess_TienBV.sql",
            //    "20170606_setting_TienBV.sql"
            //}),

            //new eVersion("1.0.14", 
            //    new List<string>(){
            //    "20170621_document_locationreturn_DungNVl.sql",
            //    "20170605_OTP_QuiBQ.sql",
            //    "20170607_categorycode_addIsDefault_QuiBQ.sql",
            //    "20170607_doctypestore_addIsDefault_QuiBQ.sql",
            //    "20170607_storecode_addIsDefault_QuiBQ.sql",
            //    "20170615_businessLicense_UserCreate_DungNVl.sql",
            //    "20170615_reportBusiness_DungNVl.sql",
            //    "20170704_Template_Update_QuiBQ.sql"
            //}),

            //new eVersion("1.0.15", 
            //    new List<string>(){
            //    "20170713_report_TienBV.sql",
            //    "20170713_searchproceduce_TienBV.sql",
            //    "20170818_processfunction_TienBV.sql",
            //    "20170818_search_statistic_TienBV.sql",
            //    "02082017_Vote_DungNVl.sql",
            //    "20170814_Vote_Update_DungNVl.sql",
            //    "20170831_searchproceduce_DungNVl.sql",
            //    "20170828_storeproceduce_TienBV.sql"
            //}),

            //new eVersion("1.0.16", 
            //    new List<string>(){
            //    "20170912_document_TienBV.sql",
            //    "20171005_mobileDivice_DungNVl.sql",
            //    "20171120_address_DungNVl.sql",
            //    "20171120_userDepartMap_DungNVl.sql"
            //}),
            
            //new eVersion("1.0.17", 
            //    new List<string>(){
            //    "20171128_docColumnSetting_TienBV.sql",
            //    "20171205_docPublish_TienBV.sql",
            //    "20171206_report_TienBV.sql",
            //    "20171206_reportFuncTienBV.sql",
            //    "20171211_QuickSearch_TienNVg.sql",
            //    "20171214_report_DungNVl.sql",
            //    "20171216_statisticDVC_QuiBQ.sql",
            //    "20171223_searchProceduce_TienNVg.sql",
            //    "20171228_addColumnPosition_TienNVg.sql",
            //    "20180111_report_DungNVl.sql",
            //    "20180127_document_rePublish_DungNVl.sql"
            //}),

            //new eVersion("1.0.18", new List<string>(){
            //    "20180514_calendar_TienBV.sql",
            //    "20180522_docrelation_TienBV.sql",
            //    "20180528_mail_TienBV.sql",
            //    "20180528_sms_TienBV.sql",
            //    "20180611_attachment_TienBV.sql",
            //    "20180614_Mobile_TienBV.sql"
            //}),
            //new eVersion("1.0.23", new List<string>()
            //{
            //    "20200731_Insert_treeGroup_processFunction.sql",
            //    "20200730_ResourceSurvey.sql",
            //    "20200723_updateDoctype_surveyImg.sql",
            //    "20200722_PermissionResourceDoctypeCopy.sql",
            //    "20200721_UpdateDoctype_addCol.sql",
            //    "20200716_UpdateDoctype.sql",
            //    "20200715_SurveyCatalog.sql",
            //    "20200807_UpdateReport.sql",
            //    "20200805_UpdateDoctype.sql",
            //    "20200810_UpdateMail.sql",
            //    "20200804_SurveyCatalog_SurveyCatalogValue.sql",
            //    "20200814_Permission_DocTypeChangeIsActivateBatch.sql",
            //    "20200819_UpdateReportFileNew.sql",
            //    "20200819_UpdateResouce_Sms_Mail.sql",
            //    "20200821_UpdateDBMail.sql",
            //    "20200821_UpdateDbSms.sql",
            //    "20200824_SearchProceduce.sql",
            //    "20200824_QuickSearch.sql",
            //    "20200826_UpdateSetting.sql"
            //}),

            //new eVersion("2.0.0", new List<string>()
            //{           
            //    //version 2.0.0
            //    "20200828_UpdateResource.sql",
            //    "20200909_UpdateProSearchDocument.sql",
            //    "20200828_UpdateDocType_TimeJob.sql",
            //    "20200904_UpdateDocType_TimeJobNew.sql",
            //    "20200923_CreateDbIndicator.sql"
            //}),

            //new eVersion("2.0.1", new List<string>(){
            //    "20200828_UpdateDocType_TimeJob.sql",
            //    "20200828_UpdateResource.sql",
            //    "20200904_UpdateDocType_TimeJobNew.sql",
            //    "20180528_sms_TienBV.sql",
            //    "20180611_attachment_TienBV.sql",
            //    "20200909_UpdateProSearchDocument.sql"
            //})
       
            //new eVersion("2.0.1", new List<string>()
            //{
            //    "datafieldtemplate.sql",
            //    "sql_template.sql",
            //    "20201410_Create_dim_in_datatype.sql",
            //    "20200810_CreateProceduce_Pro_UpdateDB.sql",
            //    "20200810_AlterTableDocumentCopt.sql",
            //    "20200810_Create_dim_incatalogvalue.sql",
            //    "20200710_Create_incatalog.sql",
            //    "20200710_Create_incatalogvalue.sql",
            //    "20200710_Create_dim_incatalogvalue.sql",
            //    "20200710_Create_dim_incatalog.sql",
            //    "20200710_Create_dim_disaggregation.sql",
            //    "20200710_Create_dim_datatype.sql",
            //    "20200710_Create_dim_categorydisaggregations.sql",
            //    "20200710_Create_dim_ad_unit.sql",
            //    "20200710_Create_dim_ad_locality.sql"
            //}),
            new eVersion("2.0.2", new List<string>()
            {
                "20201019_Create_locality.sql",
                "20201019_Create_localitydepartmentvalue.sql",
                "20201019_Creat_Indicatorvaluedepartment.sql",
                "20201109_Create_Reportrules.sql",
                "20201110_CreateReportRules.sql",

            }),

            new eVersion("2.0.3", new List<string>()
            {
                "20201109_add_columDocument.sql",
                "20201028_AfterTable.sql",
                "20201110_AftertableAd_Locality.sql",
                "20201028_AfterTable_Locality.sql",
                "20201110_updateStatusOpenCloseDocument.sql",
                "20201103_AfterTalePermistion.sql",
                "20201102_UpdateResource.sql",
                "20201103UpdatePermission.sql",
                "20201024_AfterTableForm.sql",
                "20201110_UpdatePermissionReportRule.sql",
                "20201110_UpdateResourceReportRule.sql",
                "20201113_AfterTableDoctype.sql"
            }),

            // function
            new eVersion("2.0.4", new List<string>()
            {
                "funcStatistic.sql"
            }),

            // Thêm version mới bằng cách đánh dấu từ thấp đến cao ở đây ...
        };

        /// <summary>
        /// Trả về phiên bản mới nhất
        /// </summary>
        public static eVersion CurrentVersion
        {
            get
            {
                return _versionChangeHistory.Last();
            }
        }

        /// <summary>
        /// Trả về tất cả phiên bản
        /// </summary>
        public static List<eVersion> All
        {
            get { return _versionChangeHistory; }
        }
    }
}
