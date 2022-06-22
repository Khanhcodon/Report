using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Bkav.eGovCloud.Web.Framework.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class CultureHelper
    {
        // Include ONLY cultures you are implementing as views
        private static readonly Dictionary<String, bool> _cultures = new Dictionary<string, bool> {
            {"en-US", false}, // first culture is the DEFAULT    
            {"vi-VN", false} 
        };

        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US"
        /// </summary>
        /// <param name="name">Culture's name (e.g. en-US)</param>
        public static CultureInfo GetValidCulture(string name)
        {
            var valid = GetValidCultureName(name);
            return CultureInfo.CreateSpecificCulture(valid);
        }

        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US"
        /// </summary>
        /// <param name="name">Culture's name (e.g. en-US)</param>
        public static string GetValidCultureName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetDefaultCultureName(); // return Default culture
            }

            if (_cultures.ContainsKey(name))
            {
                return name;
            }

            var n = GetNeutralCulture(name);
            // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
            // the function will return closes match that is "en-US" because at least the language is the same (ie English)            
            foreach (var c in _cultures.Keys)
            {
                if (c.StartsWith(n))
                {
                    return c;
                }
            }

            // else             
            return GetDefaultCultureName(); // return Default culture as no match found
        }

        /// <summary>
        /// Returns default culture name which is the first name decalared (e.g. en-US)
        /// </summary>
        /// <returns></returns>
        public static CultureInfo GetDefaultCulture()
        {
            return CultureInfo.CreateSpecificCulture(GetDefaultCultureName()); // return Default culture
        }

        /// <summary>
        /// Returns default culture name which is the first name decalared (e.g. en-US)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCultureName()
        {
            return _cultures.Keys.ElementAt(0); // return Default culture
        }

        /// <summary>
        /// Returns default culture name which is the first name decalared (e.g. en-US)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultNeutralCultureName()
        {
            return GetNeutralCulture(GetDefaultCultureName()); // return Default culture
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CultureInfo GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentCultureName()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentNeutralCultureName()
        {
            return GetNeutralCulture(GetCurrentCultureName());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetNeutralCulture(string name)
        {
            return name.Length < 2 ? name : name.Substring(0, 2);
        }

        /// <summary>
        ///  Returns "true" if view is implemented separatley, and "false" if not.
        ///  For example, if "es-CL" is true, then separate views must exist e.g. Index.es-cl.cshtml, About.es-cl.cshtml
        /// </summary>
        /// <param name="name">Culture's name</param>
        /// <returns></returns>
        public static bool IsViewSeparate(string name)
        {
            var validCultur = GetValidCultureName(name);
            return _cultures[validCultur];
        }

    }
}
