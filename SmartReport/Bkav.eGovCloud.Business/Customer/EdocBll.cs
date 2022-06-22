using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Logging;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.eDoc;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Nghiệp vụ xử lý liên thông hồ sơ, văn bản nội bộ
    /// </summary>
    public class EdocBll : ServiceBase
    {
        private readonly AttachmentBll _attachmentService;
        private readonly AddressBll _addressService;
        private readonly DocumentPublishBll _docPublishService;
        private readonly LogBll _logService;

        private const string DEFAULT_DATETIME_FORMAT = "dd/MM/yyyy";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="addressService"></param>
        /// <param name="attachmentService"></param>
        /// <param name="docPublishService"></param>
        /// <param name="logService"></param>
        public EdocBll(
            IDbCustomerContext context,
            AddressBll addressService,
            AttachmentBll attachmentService,
            DocumentPublishBll docPublishService,
            LogBll logService)
            : base(context)
        {
            _attachmentService = attachmentService;
            _addressService = addressService;
            _docPublishService = docPublishService;
            _logService = logService;
        }

        /// <summary>
        /// Trả về danh sách các DocumentCopyId hồ sơ, văn bản chờ liên thông
        /// </summary>
        /// <returns>Danh sách DocumentId</returns>
        public List<string> GetPendingDocumentIds()
        {
            var pending = _docPublishService.GetPending();
            return pending.Select(p => p.DocumentCopyId.ToString()).Distinct().ToList();
        }

        /// <summary>
        /// Gửi văn bản, hồ sơ liên thông
        /// </summary>
        /// <param name="documentCopy">DocumentCopy</param>
        /// <param name="addressId">Địa chỉ nhận</param>
        /// <param name="dateAppoined">Hạn hồi báo</param>
        /// <returns></returns>
        public bool SendDocument(DocumentCopy documentCopy, int addressId, DateTime? dateAppoined)
        {
            var address = _addressService.Get(addressId);
            if (address == null || string.IsNullOrWhiteSpace(address.Website))
            {
                return false;
            }

            return SendDocument(documentCopy, address, dateAppoined);
        }

        /// <summary>
        /// Gửi văn bản, hồ sơ liên thông
        /// </summary>
        /// <param name="documentCopy">DocumentCopy</param>
        /// <param name="address">Địa chỉ nhận</param>
        /// <param name="dateAppoined">Hạn hồi báo</param>
        /// <returns></returns>
        public bool SendDocument(DocumentCopy documentCopy, Bkav.eGovCloud.Entities.Customer.Address address, DateTime? dateAppoined)
        {
            var document = documentCopy.Document;
            var currentOrganization = _addressService.GetCurrent();

            try
            {
                var result = new Bkav.eGovCloud.Entities.Customer.eDoc.Document();

                // Thông tin công dân
                result.CitizenInfo = new Bkav.eGovCloud.Entities.Customer.eDoc.Citizen()
                {
                    IdCard = document.IdentityCard,
                    Email = document.Email,
                    Address = document.Address,
                    Name = document.CitizenName,
                    Phone = document.Phone
                };

                // Lệ phí
                result.Fees = document.DocFees.Select(df => new Bkav.eGovCloud.Entities.Customer.eDoc.Fee()
                {
                    Name = df.FeeName,
                    Value = df.Price,
                    Currency = "Đồng"
                }).ToList();

                // Giấy tờ
                result.DocumentPapers = document.DocPapers.Select(dp => new Bkav.eGovCloud.Entities.Customer.eDoc.DocumentPaper()
                {
                    Name = dp.PaperName,
                    Amount = dp.Amount
                }).ToList();

                // Cơ quan gửi
                result.Sender = new Bkav.eGovCloud.Entities.Customer.eDoc.Organization()
                {
                    OrganId = currentOrganization.EdocId,
                    OrganName = currentOrganization.Name
                };

                // Cơ quan nhận
                result.Receivers = new List<Bkav.eGovCloud.Entities.Customer.eDoc.Organization>(){
                    new Bkav.eGovCloud.Entities.Customer.eDoc.Organization(){
                        OrganId = address.EdocId,
                        OrganName = address.Name
                    }
                };

                // Hồ sơ liên quan
                result.Relateds = document.DocRelations.Where(d => !string.IsNullOrEmpty(d.Document.DocCode))
                                    .Select(d => new Bkav.eGovCloud.Entities.Customer.eDoc.RelatedDocument()
                {
                    OrganId = currentOrganization.EdocId,
                    PromulgationDate = d.Document.DateSuccess.Value.ToString(DEFAULT_DATETIME_FORMAT),
                    Subject = d.Document.Compendium,
                    CodeNumber = SplitDocCode(d.Document.DocCode)[0],
                    CodeNotation = SplitDocCode(d.Document.DocCode)[1]
                }).ToList();

                // File đính kèm
                var docAttachments = _attachmentService.Gets(document.DocumentId);
                if (!docAttachments.Any())
                {
                    result.Attachments = new List<Bkav.eGovCloud.Entities.Customer.eDoc.Attachment>();
                }
                else
                {
                    var attachmentIds = docAttachments.Select(a => a.AttachmentId).ToList();
                    var attachments = _attachmentService.DownloadAttachmentName(attachmentIds, 123, false, true);
                    result.Attachments = attachments.Select(a => new Bkav.eGovCloud.Entities.Customer.eDoc.Attachment()
                    {
                        Description = "",
                        Name = a.Key,
                        Value = a.Value
                    }).ToList();
                }

                var codes = SplitDocCode(document.DocCode);

                result.DocumentId = documentCopy.DocumentCopyId.ToString();
                result.Subject = document.Compendium;
                result.CodeNumber = codes[0];
                result.CodeNotation = codes[1];
                result.Place = currentOrganization.AddressString;
                result.PromulgationDate = document.DateSuccess.HasValue ? document.DateSuccess.Value.ToString(DEFAULT_DATETIME_FORMAT) : DateTime.Now.ToString(DEFAULT_DATETIME_FORMAT);
                result.TypeCode = document.DocTypeId.ToString();
                result.TypeName = document.DocTypeName;
                result.SignerName = document.UserSuccessName;
                result.DueDate = "";
                result.Priority = 3;
                result.DateAppointed = dateAppoined.HasValue ? dateAppoined.Value.ToString(DEFAULT_DATETIME_FORMAT) : string.Empty;

                var uri = address.Website;
                if (!uri.EndsWith("/"))
                {
                    uri += "/";
                }

                var action = "Webapi/Document/SaveDocument";

                return Post(uri, action, result);
            }
            catch (Exception ex)
            {
                _logService.Error("Liên thông", ex);
                return false;
            }
        }

        /// <summary>
        /// Gửi liên thông lại nếu chưa gửi đi dc.
        /// </summary>
        /// <param name="documentCopy"></param>
        /// <param name="publish"></param>
        public bool ReSendLienThong(DocumentCopy documentCopy, DocPublish publish)
        {
            if (!publish.AddressId.HasValue)
            {
                throw new ArgumentException("AddressId");
            }

            if (documentCopy == null)
            {
                throw new Exception("Văn bản không tồn tại, vui lòng thử lại.");
            }

            var address = _addressService.Get(publish.AddressId.Value);

            // Chỉ hỗ trợ với gửi liên thông nội bộ. 
            // Liên thông qua eDoc tool sẽ xử lý.
            if (address == null || string.IsNullOrWhiteSpace(address.Website))
            {
                throw new Exception("Cơ quan hiện tại không hỗ trợ chức năng này.");
            }

            var resend = SendDocument(documentCopy, address, publish.DateAppointed);
            if (resend)
            {
                publish.IsPending = false;
                Context.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Trả về tiến độ hồ sơ liên thông
        /// </summary>
        /// <param name="docCode">Mã hồ sơ</param>
        /// <param name="addressId">Địa chỉ nhận</param>
        /// <returns></returns>
        public List<DocumentTrace> GetTraces(string docCode, int addressId)
        {
            var address = _addressService.Get(addressId);
            var currentAddress = _addressService.GetCurrent();

            return GetTraces(docCode, address.Website, currentAddress.EdocId);
        }

        /// <summary>
        /// Trả về tiến độ hồ sơ đang liên thông
        /// </summary>
        /// <param name="docCode">Mã hồ sơ</param>
        /// <param name="serviceUrl">Địa chỉ service cơ quan liên thông</param>
        /// <param name="currentEDocId">Mã định danh của cơ quan hiện tại</param>
        /// <returns></returns>
        public List<DocumentTrace> GetTraces(string docCode, string serviceUrl, string currentEDocId)
        {
            var result = new List<DocumentTrace>();
            if (string.IsNullOrWhiteSpace(serviceUrl))
            {
                return result;
            }

            if (!serviceUrl.EndsWith("/"))
            {
                serviceUrl += "/";
            }

            var action = "Webapi/Document/GetListTraces?docCode={0}&organizationCode={1}";
            try
            {
                action = string.Format(action, docCode, currentEDocId); // "?docCode=" + docCode + "&organizationCode=" + currentEDocId;
                result = Get<List<DocumentTrace>>(serviceUrl, action);
            }
            catch
            {

            }
            return result;
        }

        private string[] SplitDocCode(string doccode)
        {
            var firstIndexOfS = doccode.IndexOf("/");
            var code = firstIndexOfS < 0 ? "" : doccode.Substring(0, firstIndexOfS);
            var codeNotation = doccode.Replace(code + "/", "");
            return new string[] { code, codeNotation };
        }

        /// <summary>
        /// Post request lên server và trả về giá trị xác định post thành công hay không
        /// </summary>
        /// <param name="action">Request Action</param>
        /// <param name="data">Dữ liệu post lên</param>
        /// <param name="uri">Đường dẫn webservice base của cơ quan tương ứng</param>
        /// <returns>True - post và thực thi thành công; false - còn lại</returns>
        private bool Post(string uri, string action, object data)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            ObjectContent content = null;

            if (data != null)
            {
                content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
            }

            var responseMessage = client.PostAsync(action, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
                return result;
            }
            else
            {
                InsertLog(responseMessage);
            }

            return false;
        }

        /// <summary>
        /// Get request lên server và trả về dữ liệu theo kiểu mong muôn
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="action">Request Action</param>
        /// <param name="parameters">Tham số uri</param>
        /// <returns>Dữ liệu mong muốn nếu thực thi thành công hoặc kiểu mặc định của dữ liệu mong muốn nếu không thành công.</returns>
        public T Get<T>(string uri, string action, Dictionary<string, string> parameters = null)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (parameters != null && parameters.Any())
            {
                action += "?";
                foreach (var param in parameters)
                {
                    action += string.Concat(param.Key, "=", param.Value, "&&");
                }
            }

            //ServicePointManager
            //    .ServerCertificateValidationCallback +=
            //        (sender, cert, chain, sslPolicyErrors) => true;

            var responseMessage = client.GetAsync(action).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<T>(responseMessage.Content.ReadAsStringAsync().Result);
                return result;
            }
            return default(T);
        }

        /// <summary>
        /// Ghi log
        /// </summary>
        /// <param name="responseMessage"></param>
        private void InsertLog(HttpResponseMessage responseMessage)
        {
            _logService.Information("Liên Thông Error: " + responseMessage.StatusCode
                        + " - " + responseMessage.ReasonPhrase + "\n" + responseMessage.RequestMessage);
        }
    }
}