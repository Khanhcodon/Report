using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using Bkav.eGovCloud.Entities;
using Bkav.eGovOnline.Business.Common;

namespace Bkav.eGovCloud.Api.Controller
{
    public class SyncBWSSController : EgovApiBaseController
    {
        private readonly UserBll _userService;
        private readonly CategoryBll _categoryService;
        private readonly DepartmentBll _departmentService;
        private readonly ResourceBll _resourceService;
        private readonly PositionBll _positionService;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly AuthenticationSettings _authenticationSettings;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public SyncBWSSController()
        {
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            _positionService = DependencyResolver.Current.GetService<PositionBll>();
            _jobTitlesService = DependencyResolver.Current.GetService<JobTitlesBll>();
            _authenticationSettings = DependencyResolver.Current.GetService<AuthenticationSettings>();
            _categoryService = DependencyResolver.Current.GetService<CategoryBll>();
        }

        [System.Web.Http.HttpGet]
        public Dictionary<int, string> GetUrgents()
        {
            var jsonLocation = new Dictionary<int, string>();
            foreach (Urgent location in Enum.GetValues(typeof(Urgent)))
            {
                var description = eGovCloud.Core.Utils.EnumHelper<Urgent>.GetDescription(location);
                var descriptionExample = _resourceService.GetLocaleStringResourceByName(description);
                if (descriptionExample!=null)
                {
                    description = descriptionExample.ResourceValue;
                }
                jsonLocation.Add((int)location, description);
            }

            return jsonLocation;
        }

        public IEnumerable<Category> GetCategorys()
        {
            return _categoryService.GetsFromCache();
        }

        private bool IsValidToken(string token)
        {
            return token.Equals("1659433a-0ed4-44c6-ad16-7a72a86ffb87");
        }
    }
}