#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class MergeObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public T CreateFromObjects<T>(params T[] sources) where T : new()
        {
            var ret = new T();
            MergeObjects(ret, sources);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="sources"></param>
        public void MergeObjects<T>(T target, params T[] sources)
        {
            Func<PropertyInfo, T, bool> predicate = (p, s) =>
            {
                if (p.GetValue(s, null).Equals(GetDefault(p.PropertyType)))
                {
                    return false;
                }

                return true;
            };

            MergeObjects(target, predicate, sources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <param name="sources"></param>
        public void MergeObjects<T>(T target, Func<PropertyInfo, T, bool> predicate, params T[] sources)
        {
            foreach (var propertyInfo in typeof(T).GetProperties().Where(prop => prop.CanRead && prop.CanWrite))
            {
                foreach (var source in sources)
                {
                    if (predicate(propertyInfo, source))
                    {
                        propertyInfo.SetValue(target, propertyInfo.GetValue(source, null), null);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}