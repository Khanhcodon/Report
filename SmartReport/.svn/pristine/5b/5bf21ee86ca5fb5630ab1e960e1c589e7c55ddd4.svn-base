using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class BmailFolderCustomCompare : IComparer<string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            if (x == y)
            {
                return 0;
            }

            //Inbox -> top
            if (x == "inbox")
            {
                return -1;
            }
            if (y == "inbox")
            {
                return 1;
            }

            return String.Compare(x, y);
        }

    }
}
