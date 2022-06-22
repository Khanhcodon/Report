using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ArrayExtension - public - Core
    /// Access Modifiers: 
    /// Create Date : 130912
    /// Author      : TrungVH
    /// </author>
    /// <summary> 
    /// <para>1 thư viện ở rộng cho việc xử lý mảng</para>
    /// (TrungVH@bkav.com - 130912)
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Băm một mảng byte.
        /// </summary>
        /// <param name="input">Mảng byte đầu vào.</param>
        /// <param name="algorithm">Thuật toán.</param>
        /// <returns>Mảng đã được băm</returns>
        public static byte[] Hash(this byte[] input, string algorithm)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            if (String.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException("algorithm");
            }
            using (var algo = HashAlgorithm.Create(algorithm))
            {
                return algo.ComputeHash(input);
            }
        }

        /// <summary>
        /// Chuyển đổi mảng byte thành chuỗi base64.
        /// </summary>
        /// <param name="input">Mảng đầu vào.</param>
        /// <returns>Chuỗi base64</returns>
        public static string AsBase64(this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// So sánh 2 mảng số nguyên
        /// </summary>
        /// <param name="current">Mảng hiện tại</param>
        /// <param name="compare">Mảng cần so sánh</param>
        /// <returns>True nếu bằng nhau và ngược lại</returns>
        public static bool CompareTo(this IEnumerable<int> current, IEnumerable<int> compare)
        {
            if (current == null)
            {
                throw new ArgumentNullException("current");
            }
            if (compare == null)
            {
                throw new ArgumentNullException("compare");
            }
            var currentArray = current.OrderBy(i => i);
            var compareArray = compare.OrderBy(i => i);

            return currentArray.SequenceEqual(compareArray);
        }

        /// <summary>
        /// So sánh 2 mảng số nguyên vả tìm ra những phần tử khác nhau
        /// </summary>
        /// <param name="current">Mảng hiện tại</param>
        /// <param name="compare">Mảng cần so sánh</param>
        /// <param name="delete">Những phần tử có trong mảng muốn so sánh nhưng không có trong mảng được so sánh</param>
        /// <param name="add">Những phần tử có trong mảng được so sánh nhưng không có trong mảng muốn so sánh</param>
        /// <returns></returns>
        public static bool CompareTo(this IEnumerable<int> current, IEnumerable<int> compare, out IEnumerable<int> delete, out IEnumerable<int> add)
        {
            if (current == null)
            {
                throw new ArgumentNullException("current");
            }
            if (compare == null)
            {
                throw new ArgumentNullException("compare");
            }
            var currentArray = current.OrderBy(i => i);
            var compareArray = compare.OrderBy(i => i);
            delete = new int[] { };
            var result = currentArray.SequenceEqual(compareArray);
            if (!result)
            {
                if (currentArray.Any())
                {
                    delete = currentArray.Where(id => !compareArray.Contains(id)).ToArray();
                }
            }
            add = compareArray.Where(id => !currentArray.Contains(id)).ToArray();

            return result;
        }

        /// <summary>
        /// So sánh 2 mảng string và tìm ra những phần tử khác nhau
        /// </summary>
        /// <param name="current">Mảng hiện tại</param>
        /// <param name="compare">Mảng cần so sánh</param>
        /// <param name="delete">Những phần tử có trong mảng muốn so sánh nhưng không có trong mảng được so sánh</param>
        /// <param name="add">Những phần tử có trong mảng được so sánh nhưng không có trong mảng muốn so sánh</param>
        /// <returns></returns>
        public static bool CompareTo(this IEnumerable<string> current, IEnumerable<string> compare, out IEnumerable<string> delete, out IEnumerable<string> add)
        {
            if (current == null)
            {
                throw new ArgumentNullException("current");
            }
            if (compare == null)
            {
                throw new ArgumentNullException("compare");
            }

            var currentArray = current.OrderBy(i => i);
            var compareArray = compare.OrderBy(i => i);

            delete = new string[] { };
            var result = currentArray.SequenceEqual(compareArray);
            if (!result)
            {
                if (currentArray.Any())
                {
                    delete = currentArray.Where(id => !compareArray.Contains(id)).ToArray();
                }
            }
            add = compareArray.Where(id => !currentArray.Contains(id)).ToArray();

            return result;
        }

        /// <summary>
        /// Clone list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldList"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(this List<T> oldList)
        {
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, oldList);
            stream.Position = 0;
            return (List<T>)formatter.Deserialize(stream);
        } 
    }
}
