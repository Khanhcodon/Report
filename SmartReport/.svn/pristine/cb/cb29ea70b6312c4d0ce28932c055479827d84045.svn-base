using System.Web.Mvc;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    public class TimeJobController : EgovApiBaseController
    {
        private TimerJobHelper _timerJobHelper;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public TimeJobController()
        {
            _timerJobHelper = DependencyResolver.Current.GetService<TimerJobHelper>();
        }

        /// <summary>
        /// Chạy timer job
        /// </summary>
        //[System.Web.Http.HttpPost]
        public void Run()
        {
            _timerJobHelper.RunAll();
        }

        /// <summary>
        /// 
        /// </summary>
        [System.Web.Http.HttpGet]
        public void RunGet()
        {
            _timerJobHelper.RunAll();
        }
    }
}