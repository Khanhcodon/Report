using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : KeyWordQuery - public - BLL
    /// Access Modifiers:
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng KeyWord
    /// </summary>
    public static class KeyWordQuery
    {
        /// <summary>
        /// KeyWordId == keyWordId
        /// </summary>
        /// <param name="keyWordId">Id của từ khóa.</param>
        /// <returns></returns>
        public static Expression<Func<KeyWord, bool>> WithId(int keyWordId)
        {
            return s => s.KeyWordId == keyWordId;
        }

        /// <summary>
        /// KeyWordName == keyWordName
        /// </summary>
        /// <param name="keyWordName">Tên của từ khóa.</param>
        /// <returns></returns>
        public static Expression<Func<KeyWord, bool>> WithName(string keyWordName)
        {
            return s => s.KeyWordName == keyWordName;
        }

        /// <summary>
        /// KeyWordName == keyWordName
        /// </summary>
        /// <param name="keyWordName">Tên từ khóa.</param>
        /// <returns></returns>
        public static Expression<Func<KeyWord, bool>> ContainsName(string keyWordName)
        {
            return s => s.KeyWordName.ToLower().Contains(keyWordName.ToLower());
        }
    }
}