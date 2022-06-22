using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Customer;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;

namespace Bkav.eGovCloud.Business
{
   
    public class SaveFileUtil
    {
        public static void SaveFormConfig(string key, Dictionary<string, object> fileData, HttpContext context)
        {
            var formService = DependencyResolver.Current.GetService<FormBll>();
            var forms = formService.Gets(f => f.Template.Equals(key), false);
            if (!forms.Any())
            {
                context.Response.Write("{\"error\":0}");
                return;
            }

            var form = forms.First();
            form.Template = "f_" + RandomString(15, false);
            formService.SaveChanges();
            var fileName = Path.GetFileName(form.EmbryonicLocationName);
            var filePath = CommonHelper.MapPath("~/EmbryonicForm/" + fileName);

            var req = WebRequest.Create((string)fileData["url"]);
            using (var stream = req.GetResponse().GetResponseStream())
            using (var fs = System.IO.File.Open(filePath, FileMode.Create))
            {
                var buffer = new byte[4096];
                int readed;
                while ((readed = stream.Read(buffer, 0, 4096)) != 0)
                    fs.Write(buffer, 0, readed);
            }
        }
        public static void SaveDocumentReport(string key, Dictionary<string, object> fileData, HttpContext context)
        {
            var fileName = Path.GetFileName(key) + "_saved.xlsx";
            var filePath = CommonHelper.MapPath("~/Reports/" + fileName);

            var req = WebRequest.Create((string)fileData["url"]);
            using (var stream = req.GetResponse().GetResponseStream())
            using (var fs = System.IO.File.Open(filePath, FileMode.Create))
            {
                var buffer = new byte[4096];
                int readed;
                while ((readed = stream.Read(buffer, 0, 4096)) != 0)
                    fs.Write(buffer, 0, readed);
            }
        }

       public static string CopyDocumentFile(string key, string fileTemplate)
        {
            var fileName = Path.GetFileName(key) + "_saved.xlsx";
            var filePath = CommonHelper.MapPath("~/Reports/" + fileName);
            System.IO.File.Copy(fileTemplate, filePath);

            return "Reports/" + fileName;
        }

        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
