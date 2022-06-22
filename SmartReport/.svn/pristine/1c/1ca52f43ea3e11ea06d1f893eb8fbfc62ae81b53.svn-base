using System;

namespace Bkav.eGovCloud.Core.Utils
{

    /// <summary>
    /// 
    /// </summary>
    public struct ActionDisposable : IDisposable
    {
        readonly Action _action;

        /// <summary>
        /// 
        /// </summary>
        public static readonly ActionDisposable Empty = new ActionDisposable(() => { });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public ActionDisposable(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _action();
        }

    }


}
