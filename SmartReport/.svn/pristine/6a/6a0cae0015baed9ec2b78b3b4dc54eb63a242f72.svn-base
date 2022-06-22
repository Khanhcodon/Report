using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Oauth
{
    [RequireHttps]
    [OAuthAuthorizeAttribute(Scope.File)]
    public class StatisticController : EgovApiBaseController
    {
        private readonly ApiBll _apiService;
        private readonly FileBll _fileService;

        public StatisticController()
        {
            _apiService = DependencyResolver.Current.GetService<ApiBll>();
            _fileService = DependencyResolver.Current.GetService<FileBll>();
        }

        public string GetFileInBase64(int fileId)
        {
            string fileName;
            var fileStream = _fileService.DownloadFile(out fileName, fileId);
            var fileBase64 = StreamToBase64(fileStream);
            return fileBase64;
        }

        public HttpResponseMessage GetStreamFile(int fileId)
        {
            string fileName;
            var stream = _fileService.DownloadFile(out fileName, fileId);
            if (stream != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return response;
            }

            return null;
        }

        public File GetFile(int fileId)
        {
            return _fileService.GetFile(fileId);
        }

        private string StreamToBase64(System.IO.Stream stream)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                using (stream)
                {
                    var buffer = new byte[4096];
                    while (true)
                    {
                        var count = stream.Read(buffer, 0, 4096);
                        if (count != 0)
                            ms.Write(buffer, 0, count);
                        else
                            break;
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}