using RazorEngine.Templating;
using System;
using System.Globalization;

namespace Bkav.eGovCloud.Core.RazorEngine
{
    /// <summary>
    /// Custom template base cho các kiểu dữ liệu.
    /// <para> Format giá trị các key theo các format mask khác nhau.</para>
    /// <para> (Tienbv@bkav 230313)</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MyCustomTemplateBase<T> : TemplateBase<T>
    {
        #region Datetime format

        /// <summary>
        /// Convert obj to datetime
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>unsuccess: null</returns>
        public DateTime? ToDateTime(object obj)
        {
            try
            {
                if (obj != DBNull.Value && !string.IsNullOrEmpty(obj.ToString()))
                {
                    return Convert.ToDateTime(obj);
                }
            }
            catch
            {
            }
            return null; // xem xét
        }

        /// <summary>
        /// Convert obj to datetime theo format.
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="format">datetime format.</param>
        /// <returns>unsuccess: null</returns>
        public DateTime? ToDateTime(object obj, string format)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                if (obj == DBNull.Value || obj.ToString() == string.Empty)
                    return null;
                return DateTime.ParseExact(obj.ToString(), format, provider);
            }
            catch
            {
            }
            return null;
        }

        #endregion Datetime format

        #region Number

        /// <summary>
        /// Convert sang kiểu double
        /// </summary>
        /// <param name="obj">Dữ liệu muốn convert</param>
        /// <returns>
        ///         null: nếu dữ liệu không hợp lệ
        /// </returns>
        public double? ToDouble(object obj)
        {
            try
            {
                if (obj != DBNull.Value && obj.ToString() != "")
                    return Convert.ToDouble(obj);
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// Convert sang kiểu float (Single)
        /// </summary>
        /// <param name="obj">Dữ liệu muốn convert</param>
        /// <returns>
        ///         Null: nếu dữ liệu không hợp lệ
        /// </returns>
        public float? ToFloat(object obj)
        {
            try
            {
                if (obj != DBNull.Value && obj.ToString() != "")
                    return Convert.ToSingle(obj);
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// Convert sang kiểu int 32bit
        /// </summary>
        /// <param name="obj">Dữ liệu muốn convert</param>
        ///<returns>
        ///     Null: nếu dữ liệu không hợp lệ
        /// </returns>
        public int? ToInt(object obj)
        {
            try
            {
                if (obj != DBNull.Value && obj.ToString() != "")
                    return Convert.ToInt32(obj);
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// Convert sang kiểu int 64bit (long)
        /// </summary>
        /// <param name="obj">Dữ liệu muốn convert</param>
        /// <returns>
        /// 0: nếu dữ liệu không hợp lệ (null/string/...)
        /// </returns>
        public long? ToLong(object obj)
        {
            try
            {
                if (obj != DBNull.Value && obj.ToString() != "")
                    return Convert.ToInt64(obj);
            }
            catch
            { }
            return null;
        }

        /// <summary>
        /// Convert sang kiểu int 16bit (short)
        /// </summary>
        /// <param name="obj">Dữ liệu muốn convert</param>
        /// <returns>
        ///         Null: nếu dữ liệu không hợp lệ
        /// </returns>
        public short? ToShort(object obj)
        {
            try
            {
                if (obj != DBNull.Value && obj.ToString() != "")
                    return Convert.ToInt16(obj);
            }
            catch
            {
            }
            return null;
        }

        #endregion Number
    }
}