using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.App_Start;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Web.Framework;
using StackExchange.Profiling;

namespace Bkav.eGovCloud.Controllers
{
	public class ProfilerController : CustomerBaseController
	{
		public void UpdateClient(string json)
		{
			var isActive = ConfigurationManager.AppSettings.Get("miniprofile-active").Equals("true", System.StringComparison.OrdinalIgnoreCase);
			if (!isActive)
			{
				return;
			}

			if (string.IsNullOrEmpty(json))
			{
				return;
			}

			var storage = MiniProfiler.Settings.Storage;
			if (storage == null)
			{
				return;
			}

			var host = Request.UserHostName;
			var profilers = Json2.ParseAs<IEnumerable<MiniProfiler>>(json);
			foreach (var p in profilers)
			{
				p.User = host;
				p.User = "Update";
				storage.Save(p);
			}
		}
	}
}