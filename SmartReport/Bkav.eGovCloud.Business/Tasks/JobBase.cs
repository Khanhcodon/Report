using Bkav.eGovCloud.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Lớp cơ bản của các Job
    /// </summary>
    public abstract class JobBase
    {
        // Log theo tháng
        private string logPath = CommonHelper.MapPath("~/Logs/schedule_" + DateTime.Now.ToString("ddMMyyyy"));

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public JobBase()
        {

        }

        /// <summary>
        /// Log lỗi trong quá trình bàn giao.
        /// </summary>
        /// <param name="exception">Exception lỗi</param>
        /// <param name="message"></param>
        public void Log(Exception exception, string message = "")
        {
            if ((exception != null) && (exception is System.Threading.ThreadAbortException))
            {
                return;
            }

            var errorMessage = new StringBuilder();
            if (!string.IsNullOrEmpty(message))
            {
                errorMessage.AppendLine(message);
            }

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

            Log(new List<string>() { fullMessage });
        }

        /// <summary>
        /// Log message trong quá trình thực thi job.
        /// </summary>
        /// <param name="messages">Danh sách các message</param>
        /// <param name="logName">Tên file log</param>
        public void Log(IEnumerable<string> messages, string logName = "schedule")
        {
            StaticLog.Log(messages, logName);
        }
    }
}
