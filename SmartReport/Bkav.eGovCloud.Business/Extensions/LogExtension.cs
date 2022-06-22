using System;
using System.Data.Entity.Validation;
using System.Text;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Logging;
using Bkav.eGovCloud.Entities;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Business
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : LogExtension - public - BLL
    /// Access Modifiers: 
    /// Create Date : 270812
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Các hàm mở rộng cho LogBll</para>
    /// <para>(TrungVH@bkav.com - 270812)</para>
    /// </summary>
    public static class LogExtension
    {
        /// <summary>
        /// Thêm mới log có kiểu debug
        /// </summary>
        /// <remarks>
        /// Sử dụng để lưu lại thông tin về giá trị của các tham số để giúp người phát triển có thể 
        /// biết được chính xác tham số gây ra lỗi có giá trị như thế nào giúp cho việc debug dễ dàng hơn
        /// </remarks>
        /// <param name="logger">Log service</param>
        /// <param name="message">Nội dung tóm tắt</param>
        /// <param name="exception">Ngoại lệ (exception)</param>
        /// <param name="args">Các tham số dùng để format string cho message</param>
        public static void Debug(this LogBll logger, string message, Exception exception = null, params object[] args)
        {
            FilteredLog(logger, LogType.Debug, message, exception, args);
        }

        /// <summary>
        /// Thêm mới log có kiểu thông tin
        /// </summary>
        /// <remarks>
        /// Sử dụng để lưu những thông tin hữu ích về các sự kiện (VD như: Bắt đầu chạy dịch vụ, bắt đầu cập nhật…), 
        /// những thông tin thường ít được quan tâm trong những trường hợp bình thường. 
        /// Khi nhìn vào log này người dùng có thể biết được trạng thái của hệ thống
        /// </remarks>
        /// <param name="logger">Log service</param>
        /// <param name="message">Nội dung tóm tắt</param>
        /// <param name="exception">Ngoại lệ (exception)</param>
        /// <param name="args">Các tham số dùng để format string cho message</param>
        public static void Information(this LogBll logger, string message, Exception exception = null, params object[] args)
        {
            FilteredLog(logger, LogType.Information, message, exception, args);
        }

        /// <summary>
        /// Thêm mới log có kiểu cảnh báo
        /// </summary>
        /// <remarks>
        /// Sử dụng khi xảy ra lỗi không quan trọng, hệ thống vẫn có thể làm việc bình thường khi xảy ra lỗi này 
        /// (VD như: Không tìm thấy chuỗi tài nguyên với key là XXX). 
        /// Tuy nhiên trong một số trường hợp thì ERROR và WARNING sẽ khó phân biệt, do đó sẽ phụ thuộc vào nhận định của người phát triển
        /// </remarks>
        /// <param name="logger">Log service</param>
        /// <param name="message">Nội dung tóm tắt</param>
        /// <param name="exception">Ngoại lệ (exception)</param>
        /// <param name="args">Các tham số dùng để format string cho message</param>
        public static void Warning(this LogBll logger, string message, Exception exception = null, params object[] args)
        {
            FilteredLog(logger, LogType.Warning, message, exception, args);
        }

        /// <summary>
        /// Thêm mới log có kiểu lỗi
        /// </summary>
        /// <remarks>
        /// Sử dụng khi hệ thống xảy ra lỗi khiến một yêu cầu bị gián đoạn (VD như: Không tìm thấy tập tin, xảy ra ngoại lệ…)
        /// </remarks>
        /// <param name="logger">Log service</param>
        /// <param name="message">Nội dung tóm tắt</param>
        /// <param name="exception">Ngoại lệ (exception)</param>
        /// <param name="args">Các tham số dùng để format string cho message</param>
        public static void Error(this LogBll logger, string message, Exception exception = null, params object[] args)
        {
            FilteredLog(logger, LogType.Error, message, exception, args);
        }

        /// <summary>
        /// Thêm mới log có kiểu lỗi nghiêm trọng
        /// </summary>
        /// <remarks>
        /// Sử dụng khi xảy ra nhưng lỗi nghiêm trọng bắt buộc phải tắt hệ thống hoặc làm hệ thống không thể chạy được tiếp (VD như: Mất dữ liệu…)
        /// </remarks>
        /// <param name="logger">Log service</param>
        /// <param name="message">Nội dung tóm tắt</param>
        /// <param name="exception">Ngoại lệ (exception)</param>
        /// <param name="args">Các tham số dùng để format string cho message</param>
        public static void Fatal(this LogBll logger, string message, Exception exception = null, params object[] args)
        {
            FilteredLog(logger, LogType.Fatal, message, exception, args);
        }

        /// <summary>
        /// FilteredLog
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        private static void FilteredLog(LogBll logger, LogType logType, string message, Exception exception = null, params object[] args)
        {
            if ((exception != null) && (exception is System.Threading.ThreadAbortException))
            {
                return;
            }

            if (args != null && args.Length > 0)
            {
                message = string.Format(message, args);
            }

            var errorMessage = new StringBuilder();
            errorMessage.AppendLine(message);

            try
            {
                var entityErrors = (DbEntityValidationException)exception;
                errorMessage.AppendLine("----DbEntityValidation EXCEPTION---------------------------");
                foreach (var error in entityErrors.EntityValidationErrors)
                {
                    foreach (var itm in error.ValidationErrors)
                    {
                        errorMessage.AppendLine(itm.PropertyName + " == " + itm.ErrorMessage);
                    }
                }
            }
            catch
            {
                if (exception != null)
                {
                    errorMessage.AppendLine(exception.Message);
                    errorMessage.AppendLine(exception.StackTrace);
                }
            }

            var ex = exception == null ? null : exception.InnerException;
            while (ex != null)
            {
                errorMessage.AppendLine("INNER EXCEPTION =======================");
                errorMessage.AppendLine(ex.Message);
                errorMessage.AppendLine(ex.StackTrace);

                ex = ex.InnerException;
            }

            var fullMessage = exception == null ? null : exception.ToString();

            try
            {
                logger.InsertLog(logType, message, errorMessage.ToString(), exception != null);
            }
            catch
            {
                StaticLog.Log(new List<string>() { errorMessage.ToString() });
            }
        }
    }
}
