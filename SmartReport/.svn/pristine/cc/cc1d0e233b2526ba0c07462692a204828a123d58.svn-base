using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Logging;

namespace Bkav.eGovCloud
{
	/// <summary>
	/// Summary description for WebEditor
	/// </summary>
	public class WebEditor : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			string body;
            StaticLog.Log(new List<string>()
            {
                "Đã Ghi file"
            });
            using (var reader = new StreamReader(context.Request.InputStream))
				body = reader.ReadToEnd();
           
            var fileData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(body);

			if (fileData != null)
			{
				var status = (int)fileData["status"];
				if (status == (int)EditorStatus.ReadyForSaving)
				{
					var key = (string)context.Request["name"];
                    StaticLog.Log(new List<string>()
                    {
                        key
                    });
                    var isConfigForm = key.StartsWith("f_", StringComparison.OrdinalIgnoreCase);
					if (isConfigForm)
					{
                        SaveFileUtil.SaveFormConfig(key, fileData, context);
					}
					else
					{
                        SaveFileUtil.SaveDocumentReport(key, fileData, context);
					}
				}				
			}

			context.Response.Write("{\"error\":0}");
		}
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

	public enum EditorStatus
	{
		/*
		 0 - no document with the key identifier could be found,
		1 - document is being edited,
		2 - document is ready for saving,
		3 - document saving error has occurred,
		4 - document is closed with no changes,
		6 - document is being edited, but the current document state is saved,
		7 - error has occurred while force saving the document.
		 */

		NoDocument = 0,
		BeingEdited = 1,
		ReadyForSaving = 2,
		SavingError = 3,
		NoChanges = 4,
		BeingEditedButNotSaved = 6,
		ErrorSaving = 7
	}
}