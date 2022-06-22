using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Customer;
using FormType = Bkav.eGovCloud.Entities.FormType;
using Bkav.eGovCloud.Core.FileSystem;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportQueryHelper
    {
        private readonly ReportQueryBll _reportQueryService;

        /// <summary>
        /// Khởi tạo <see cref="FormHelper"/>
        /// </summary>
        /// <param name="formBll">Form bll</param>
        public ReportQueryHelper(ReportQueryBll reportQueryService)
        {
            _reportQueryService = reportQueryService;
        }
    }
}
