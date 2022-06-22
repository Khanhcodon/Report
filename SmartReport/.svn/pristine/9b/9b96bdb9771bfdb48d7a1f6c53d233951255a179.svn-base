using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.EReport.Enum;

namespace Bkav.eGovCloud.Business.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportHelper
    {
        //private Report _reportService;
        private readonly ResourceBll _resourceService;
        private readonly ReportBll _reportBll;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="reportBll">Report Bll</param>
        /// <param name="resourceService">ResourceBll</param>
        public ReportHelper(ReportBll reportBll, ResourceBll resourceService)
        {
            _resourceService = resourceService;
            _reportBll = reportBll;
        }

        /// <summary>
        /// Trả về nội dung thống kê dạng html.
        /// </summary>
        /// <param name="report">Báo cáo</param>
        /// <param name="userId">Current User Id</param>
        /// <param name="time">Mốc thời gian</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="groupBy">Nhóm dữ liệu theo</param>
        /// <param name="groupDisplayBy">Hiển thị giá trị nhóm theo</param>
        /// <param name="treeGroupValue">Giá trị của nhóm của node trên cây báo cáo</param>
        /// <param name="treeGroupName">Tên nhóm của node trên cây báo cáo</param>
        /// <returns></returns>
        public string Load(Report report, int userId, DateTimeReport time, DateTime from,
            DateTime to, string groupBy, string groupDisplayBy, string treeGroupName, string treeGroupValue)
        {
            return "";
            //if (report == null)
            //{
            //    throw new Exception("Báo cáo không tồn tại trên hệ thống");
            //}
            //var template = Json2.ParseAs<EReport.Entity.ReportTemplate>(report.ViewContent);
            //var param = GetParameters(userId, from, to, treeGroupValue);
            //var specialFields = GetSpecialField(userId, time, from, to);

            //var totalQuery = ParseSql(report.QueryTotal, groupBy, groupDisplayBy, treeGroupName, string.Empty);
            //var totalRecord = _reportBll.CountTotalRecord(totalQuery, param);

            //var dataQuery = string.IsNullOrEmpty(groupBy)
            //            ? ParseSql(report.QueryStatistics, groupBy, groupDisplayBy, treeGroupName, string.Empty)
            //            : ParseSql(report.QueryGroup, groupBy, groupDisplayBy, treeGroupName, string.Empty);
            //var data = string.IsNullOrEmpty(groupBy)
            //                ? _reportBll.GetDataStatistics(dataQuery, specialFields, param)
            //                : _reportBll.GetDataStatisticsByGroup(dataQuery, specialFields, param);

            //var eReport = new EReport.Report(template, data, totalRecord, string.Empty);
            //return eReport.LoadStatistics(groupBy);
        }

        /// <summary>
        /// Trả về nội dung thống kê dạng html.
        /// Hopcv: 04/03/15
        /// </summary>
        /// <param name="report">Báo cáo</param>
        /// <param name="userId">Current User Id</param>
        /// <param name="time">Mốc thời gian</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="groupBy">Nhóm dữ liệu theo</param>
        /// <param name="groupDisplayBy">Hiển thị giá trị nhóm theo</param>
        /// <param name="treeGroupName">Tên nhóm của node trên cây báo cáo</param>
        /// <param name="treeGroupValue">Giá trị của nhóm của node trên cây báo cáo</param>
        /// <param name="sortBy">Sắp xếp dữ liệu theo</param>
        /// <param name="groupValue">Giá trị của nhóm của node trên cây báo cáo</param>
        /// <param name="total"> Trả ra số lượng văn bản</param>
        /// <param name="totalOverdue"> Trả ra số lượng văn bản quá hạn</param>
        /// <param name="totalProcessed">Trả ra số lượng văn bản đã được xử lý</param>
        /// <returns></returns>
        public string Load(Report report, int userId, DateTimeReport time, DateTime from, DateTime to,
            string groupBy, string groupDisplayBy, string treeGroupName,
            string treeGroupValue, string sortBy, string groupValue, out int total, out int totalOverdue, out int totalProcessed)
        {
            total = totalOverdue = totalProcessed = 0;
            return "";
            //if (report == null)
            //{
            //    throw new Exception("Báo cáo không tồn tại trên hệ thống");
            //}
            //var template = Json2.ParseAs<EReport.Entity.ReportTemplate>(report.ViewContent);
            //var param = GetParameters(userId, from, to, treeGroupValue);
            //var specialFields = GetSpecialField(userId, time, from, to);

            ////Tính tổng số văn bản
            //var totalQuery = ParseSql(report.QueryTotal, groupBy, groupDisplayBy, treeGroupName, sortBy);
            //var totalRecord = _reportBll.CountTotalRecord(totalQuery, param);
            //total = totalRecord;

            ////Tổng số văn bản quá hạn
            //var totalQueryOverdue = ParseSql(report.QueryTotalDocumentIsOverdue, groupBy, groupDisplayBy, treeGroupName, sortBy);
            //totalOverdue = _reportBll.CountTotalRecord(totalQueryOverdue, param);

            ////Tông số văn bản đã xử lý
            //var totalQueryProcessed = ParseSql(report.QueryTotalDocumentProcessed, groupBy, groupDisplayBy, treeGroupName, sortBy);
            //totalProcessed = _reportBll.CountTotalRecord(totalQueryProcessed, param);

            //var dataQuery = string.IsNullOrEmpty(groupBy)
            //             ? ParseSql(report.QueryStatistics, groupBy, groupDisplayBy, treeGroupName, sortBy)
            //             : ParseSql(report.QueryGroup, groupBy, groupDisplayBy, treeGroupName, sortBy);
            //var data = string.IsNullOrEmpty(groupBy)
            //                ? _reportBll.GetDataStatistics(dataQuery, specialFields, param)
            //                : _reportBll.GetDataStatisticsByGroup(dataQuery, specialFields, param);
            //var eReport = new EReport.Report(template, data, totalRecord, groupValue);

            //return eReport.LoadStatistics(groupBy);
        }

        /// <summary>
        /// Trả về nội dung html của thống kê của trang được chọn.
        /// </summary>
        /// <param name="report">Report</param>
        /// <param name="userId">User id</param>
        /// <param name="time">Mốc thời gian</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="page">Trang cần lấy</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="sortBy">Sắp xếp dữ liệu theo</param>
        /// <param name="groupBy">Nhóm dữ liệu theo</param>
        /// <param name="groupValue">Giá trị của nhóm dữ liệu</param>
        /// <param name="isDesc">Sắp xếp tăng dần</param>
        /// <param name="treeGroupName"></param>
        /// <param name="treeGroupValue"></param>
        /// <returns></returns>
        public string GotoPage(Report report, int userId, DateTimeReport time,
            DateTime from, DateTime to, int page, int pageSize,
            string groupBy, string groupValue,
            string sortBy, bool isDesc,
            string treeGroupName,
            string treeGroupValue)
        {
            return "";
            //if (report == null)
            //{
            //    throw new Exception("Báo cáo không tồn tại trên hệ thống");
            //}
            //var template = Json2.ParseAs<EReport.Entity.ReportTemplate>(report.ViewContent);
            //var param = GetParameters(userId, from, to, treeGroupValue, page, pageSize, groupValue);
            //var special = GetSpecialField(userId, time, from, to);

            //var dataQuery = ParseSql(report.QueryStatistics, groupBy, treeGroupName, treeGroupName, sortBy, isDesc);
            //var data = _reportBll.GetDataForPage(dataQuery, special, param);
            //var totalRecord = string.IsNullOrEmpty(groupValue)
            //                    ? _reportBll.CountTotalRecord(ParseSql(report.QueryTotal, groupBy, string.Empty, treeGroupName, sortBy, isDesc), param)
            //                    : _reportBll.CountTotalByGroup(ParseSql(report.QueryGroup, groupBy, string.Empty, treeGroupName, sortBy, isDesc), groupValue, param);
            //var eReport = new EReport.Report(template, data, totalRecord, groupValue);

            //return eReport.GotoStatisticPage(page, pageSize, sortBy, isDesc);
        }

        /// <summary>
        /// Trả về nội dung báo cáo để export.
        /// </summary>
        /// <param name="report">Báo cáo</param>
        /// <param name="userId">User id</param>
        /// <param name="time">Mốc thời gian lấy báo cáo</param>
        /// <param name="from">Từ khoảng thời gian</param>
        /// <param name="to">Đến khoảng thời gian</param>
        /// <param name="group">Nhóm dữ liệu theo</param>
        /// <param name="filePath">Out đường dẫn thư mục chứa các file ảnh.</param>
        /// <returns></returns>
        public string LoadForExport(Report report, int userId, DateTimeReport time, DateTime from, DateTime to, ReportGroup group, out string filePath)
        {
            filePath = "";
            return "";
            //if (report == null)
            //{
            //    throw new Exception("Báo cáo không tồn tại trên hệ thống");
            //}

            //var template = Json2.ParseAs<EReport.Entity.ReportTemplate>(report.Content);
            ////var param = GetParameters(userId, from, to, "");

            //var groupValue = group == null ? string.Empty : group.FieldName;
            //var groupName = group == null ? string.Empty : group.FieldDisplay;
            //var query = ParseSql(report.QueryReport, groupValue, groupName, string.Empty, string.Empty);
            //var data = _reportBll.GetDataForReport(query, userId, time, from, to, string.Empty);
            //var totalRecord = data.DataValues.Count();
            //var eReport = new EReport.Report(template, data, totalRecord, string.Empty);
            //var result = eReport.LoadForExport(group != null, out filePath);

            //return result;
        }

        #region Private Methods

        private Dictionary<string, object> GetSpecialField(int userId, DateTimeReport time, DateTime from, DateTime to)
        {
            var result = new Dictionary<string, object>
            {
                {"CurrentUser", userId},
                {"CurrentDate", _resourceService.GetEnumDescription<DateTimeReport>(time)},
                {"FromDate", from.ToShortDateString()},
                {"ToDate", to.ToShortDateString()}
            };
            return result;
        }

        private object[] GetParameters(int userId, DateTime from, DateTime to,
            string treeGroupValue = "", int page = 1, int pageSize = 30, string groupValue = "")
        {
            return new Object[] { 
                new SqlParameter("@userId", userId), 
                //Todo:HopCV - Tại sao lại fomat Datetime sang string; Đã format lại còn để định dạng kiểu 12h nữa
                //new SqlParameter("@from", from.ToString("yyyy-MM-dd hh:mm:ss")),
                //new SqlParameter("@to", to.ToString("yyyy-MM-dd hh:mm:ss")),
                new SqlParameter("@from", from),
                new SqlParameter("@to", to),
                new SqlParameter("@skip", pageSize * (page - 1)),
                new SqlParameter("@take", pageSize),
                new SqlParameter("@groupValue", groupValue ?? ""),
                new SqlParameter("@treeGroupValue", treeGroupValue ?? "")
            };
        }

        private string ParseSql(string query, string groupBy, string groupDisplayBy,
            string treeGroupName, string sortBy, bool isDesc = false)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return string.Empty;
            }

            var desc = isDesc ? "DESC" : "";
            groupBy = string.IsNullOrEmpty(groupBy) ? "''" : groupBy;
            groupDisplayBy = string.IsNullOrEmpty(groupDisplayBy) ? "''" : groupDisplayBy;
            sortBy = string.IsNullOrEmpty(sortBy) ? "DateCreated" : sortBy;
            treeGroupName = string.IsNullOrEmpty(treeGroupName) ? "''" : treeGroupName;
            query = query.Replace("#treeGroup", treeGroupName);
            query = query.Replace("#sortBy", sortBy);
            query = query.Replace("#isDesc", desc);
            query = query.Replace("#groupName", groupDisplayBy);
            query = query.Replace("#groupValue", groupBy);
            return query;
        }

        #endregion

    }
}
