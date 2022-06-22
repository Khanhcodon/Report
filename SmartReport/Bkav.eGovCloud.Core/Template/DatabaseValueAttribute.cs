using System;

namespace Bkav.eGovCloud.Core.Template
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseValueAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public DatabaseValueAttribute(string text)
        {
            Text = text;
        }
    }
}
