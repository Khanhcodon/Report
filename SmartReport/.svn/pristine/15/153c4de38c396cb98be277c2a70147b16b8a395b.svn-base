using System;
using System.Timers;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;

namespace Bkav.eGovCloud
{
    public class TimerJob
    {
        private Timer _commonTimer;
        
        private const int _timeInterval = 30;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timejobService"></param>
        public TimerJob(TimeJobBll timejobService)
        {
            _commonTimer = new Timer
            {
                Interval = TimeSpan.FromSeconds(_timeInterval).TotalMilliseconds,
                AutoReset = true
            };

            _commonTimer.Elapsed += TimeJob;
            GC.KeepAlive(_commonTimer);
        }

        /// <summary>
        /// Thực thi timer
        /// </summary>
        public void Run()
        {
            _commonTimer.Start();
        }

        #region private method

        private void TimeJob(object sender, ElapsedEventArgs e)
        {
            var _timerJobHelper = DependencyResolver.Current.GetService<TimerJobHelper>();
            _timerJobHelper.RunAll();
        }

        #endregion
    }
}