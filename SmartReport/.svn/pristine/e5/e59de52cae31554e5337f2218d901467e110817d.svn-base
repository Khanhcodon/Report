using System;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : PrinterQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 2800314
    /// Author      : HopCV
    /// Description : Các điều kiện truy vấn cho bảng Printer
    /// </summary>
    public static class PrinterQuery
    {
        /// <summary>
        /// printerId == printerId
        /// </summary>
        /// <param name="printerId">Id của máy in.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Printer, bool>> WithId(int printerId)
        {
            return s => s.PrinterId == printerId;
        }

        /// <summary>
        /// printerName == printerName
        /// </summary>
        /// <param name="printerName">Tên của máy in.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Printer, bool>> WithName(string printerName)
        {
            return s => s.PrinterName == printerName;
        }
    }
}
