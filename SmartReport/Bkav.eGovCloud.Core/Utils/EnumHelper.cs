using System.Globalization;
using Bkav.eGovCloud.Core.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConvertHelper - public - Core
    /// Access Modifiers: 
    /// Create Date : 230113
    /// Author      : CuongNT@bkav.com
    /// </author>
    /// <summary>
    /// <para>Cung cấp API trợ giúp cho kiểu dữ liệu enum</para>
    /// <para>(CuongNT@bkav.com - 230113)</para>
    /// </summary>
    public class EnumHelper<T> where T : struct , IConvertible
    {
        /// <summary>
        /// Kiểm tra có tồn tại giá trị enum checkagainst nào đó trong enum combined không.
        /// </summary>
        /// <param name="combined"></param>
        /// <param name="checkagainst"></param>
        /// <returns></returns>
        public static bool ContainFlags(T combined, T checkagainst)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var intValue = (int)(object)combined;
            var intLookingForFlag = (int)(object)checkagainst;
            return ((intValue & intLookingForFlag) == intLookingForFlag);
        }

        /// <summary>
        /// Chuyển đổi từ string sang enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse(string value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Lấy toàn bộ giá trị có trong enum
        /// </summary>
        /// <returns></returns>
        public static IList<T> GetValues()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
        
        /// <summary>
        /// Trả về mô tả của phần tử trên enum. ví dụ
        /// <para> [Description("Mô tả")]</para>
        /// <para> AAA = 2 </para>
        /// <para>(Tienbv@bkav.com 2803013)</para>
        /// </summary>
        /// <returns></returns>
        public static string GetDescription(T en)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be of type System.Enum");
            }
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// Trả về databse value của phần tử trên enum. ví dụ
        /// <para> [DatabaseValue("Mô tả")]</para>
        /// <para> AAA = 2 </para>
        /// <para>(Tienbv@bkav.com 2803013)</para>
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseValue(T en)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be of type System.Enum");
            }
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DatabaseValueAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DatabaseValueAttribute)attrs[0]).Text;
                }
            }
            return en.ToString();
        }
        
        /// <summary>
        /// Chuyển enum thành SelectListItem
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> EnumToSelectList(int? selected = null)
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(T));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((T)val));
                result.Add(new SelectListItem
                {
                    Text = GetDescription((T)val),
                    Value = itemValue.ToString(CultureInfo.InvariantCulture),
                    Selected = selected.HasValue && ((itemValue & (int)selected) == itemValue)
                });
            }
            return result;
        }

        /// <summary>
        /// Chuyển enum thành dictionary có dạng: Key, description
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetNameAndDescription()
        {
            var enumValArray = Enum.GetValues(typeof(T));
            return enumValArray.Cast<object>().ToDictionary(val => ((T) val).ToString(CultureInfo.InvariantCulture), val => GetDescription((T) val));
        }
    }
}
