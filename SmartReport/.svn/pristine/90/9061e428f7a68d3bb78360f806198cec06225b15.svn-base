using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.RazorEngine;
using Bkav.eGovCloud.Core.Template;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Bkav.eGovCloud.Core.FileSystem;

namespace Bkav.eGovCloud.Business
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : TemplateHelper - public - Helper </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 210313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Lớp xử lý nội dung của mẫu: đưa dữ liệu thật vào trong mẫu. </para>
    /// <para> ( TienBV@bkav.com - 210313) </para>
    /// </summary>
    public class TemplateHelper
    {
        private readonly DocumentBll _documentBll;
        private readonly TemplateKeyBll _templateKeyBll;
        private readonly DocExtendfieldBll _docExfieldBll;
        private readonly ResourceBll _resourceService;
        private readonly DocumentContentBll _docContentBll;
        private readonly ReportBll _reportService;
        private readonly TemplateBll _templateService;
        private readonly UserBll _userService;
        private readonly InfomationBll _infomationService;
        private readonly OnlineRegistrationSettings _onlineSettings;

        private const string DATE_FORMAT = "hh:mm dd/MM/yyyy";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templateKeyBll">Template key bll</param>
        /// <param name="documentBll">Document Bll</param>
        /// <param name="docContentBll">Documetn content bll</param>
        /// <param name="docExfielBll">Document extendfield bll</param>
        /// <param name="resourceService">ResourceBll</param>
        /// <param name="reportService">reportBll</param>
        /// <param name="templateService">templateBll</param>
        /// <param name="userService">UserBll</param>
        /// <param name="infomationService">InfomationBll</param>
        /// <param name="onlineSettings">OnlineRegistrationSettings</param>
        public TemplateHelper(
            TemplateKeyBll templateKeyBll,
            DocumentBll documentBll,
            DocExtendfieldBll docExfielBll,
            DocumentContentBll docContentBll,
            ResourceBll resourceService,
            ReportBll reportService,
            TemplateBll templateService,
            UserBll userService,
            InfomationBll infomationService,
            OnlineRegistrationSettings onlineSettings)
        {
            _templateKeyBll = templateKeyBll;
            _documentBll = documentBll;
            _docExfieldBll = docExfielBll;
            _resourceService = resourceService;
            _docContentBll = docContentBll;
            _reportService = reportService;
            _templateService = templateService;
            _userService = userService;
            _infomationService = infomationService;
            _onlineSettings = onlineSettings;
        }

        #region Template Key

        /// <summary>
        /// Trả về danh sách các key đặc biệt.
        /// </summary>
        /// <returns></returns>
        public List<TemplateKey> GetSpecials()
        {
            var result = new List<TemplateKey>();
            var enumValArray = Enum.GetValues(typeof(SpecialKeyEnum));
            foreach (var val in enumValArray)
            {
                result.Add(new TemplateKey
                {
                    Name = _resourceService.GetEnumDescription<SpecialKeyEnum>((SpecialKeyEnum)val),
                    Code = EnumHelper<SpecialKeyEnum>.GetDatabaseValue((SpecialKeyEnum)val),
                    IsActive = true,
                    Type = (int)TemplateKeyType.Special
                });
            }
            return result;
        }

        /// <summary>
        /// TrinhNVd - 021115: Trả về key cho câu hỏi
        /// </summary>
        /// <returns></returns>
        public List<TemplateKey> GetQuestionKeys()
        {
            var result = new List<TemplateKey>();
            var enumValArray = Enum.GetValues(typeof(QuestionTemplateKeyEnum));
            foreach (var val in enumValArray)
            {
                result.Add(new TemplateKey
                {
                    Name = _resourceService.GetEnumDescription<QuestionTemplateKeyEnum>((QuestionTemplateKeyEnum)val),
                    Code = EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue((QuestionTemplateKeyEnum)val),
                    IsActive = true,
                    Type = (int)TemplateKeyType.Special
                });
            }
            return result;
        }

        /// <summary>
        ///  Trả về key dùng chung cho cả hệ thống
        /// </summary>
        /// <returns></returns>
        public List<TemplateKey> GetCommonKeys()
        {
            var result = new List<TemplateKey>();
            var enumValArray = Enum.GetValues(typeof(CommonTemplateKeyEnum));
            foreach (var val in enumValArray)
            {
                result.Add(new TemplateKey
                {
                    Name = _resourceService.GetEnumDescription<CommonTemplateKeyEnum>((CommonTemplateKeyEnum)val),
                    Code = EnumHelper<CommonTemplateKeyEnum>.GetDatabaseValue((CommonTemplateKeyEnum)val),
                    IsActive = true,
                    Type = (int)TemplateKeyType.Special
                });
            }
            return result;
        }

        /// <summary>
        ///  Trả về key văn bản/hồ sơ online
        /// </summary>
        /// <returns></returns>
        public List<TemplateKey> GetDocumentOnlines()
        {
            var result = new List<TemplateKey>();
            var enumValArray = Enum.GetValues(typeof(DocumentOnlineTemplateKeyEnum));
            foreach (var val in enumValArray)
            {
                result.Add(new TemplateKey
                {
                    Name = _resourceService.GetEnumDescription<DocumentOnlineTemplateKeyEnum>((DocumentOnlineTemplateKeyEnum)val),
                    Code = EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue((DocumentOnlineTemplateKeyEnum)val),
                    IsActive = true,
                    Type = (int)TemplateKeyType.Special
                });
            }
            return result;
        }

        /// <summary>
        /// Trả về mã mặc định cho các key được custom.
        /// <para>(Tienbv@bkav.com 270313)</para>
        /// </summary>
        /// <param name="name">Tên key</param>
        /// <returns></returns>
        public static string GetDefaultCodeCustomKey(string name)
        {
            var result = "{";
            result += name.StripVietnameseChars().Trim()
                            .StripChars(new[] { '(', ')', ':' })
                            .ReplaceCharGroups(
                                new[] { " " },
                                new[] { '_' })
                            .ToLower();
            result += "_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            result += "}";
            return result;
        }

        /// <summary>
        /// Trả về câu sql được tự động sinh cho các key được import.
        /// <para>(Tienbv@bkav.com 270313)</para>
        /// </summary>
        /// <param name="type">Loại key: catalog hay extendfield</param>
        /// <returns></returns>
        public static string GetDefaultSqlCustomKey(int type)
        {
            string result;
            if (type == (int)ControlType.DropDownList || type == (int)ControlType.CheckBoxList)
            {
                result = "Select CatalogValue \nFrom doc_catalog \nWhere CatalogId = @ControlId \n\tAnd FormId = @FormId \n\tAnd DocumentId = @DocId";
            }
            else
            {
                result = "Select ExtendFieldValue \nFrom doc_extendfield \nWhere ExtendFieldId = @ControlId \n\tAnd FormId = @FormId \n\tAnd DocumentId = @DocId";
            }
            return result;
        }

        /// <summary>
        /// Trả về template mặc định cho các key dc custom.
        /// <para>(Tienbv@bkav.com 270313)</para>
        /// </summary>
        /// <param name="type">Loại key: catalog hay extendfield</param>
        /// <returns></returns>
        public static string GetDefaultTemplateCustomKey(int type)
        {
            const string result = "@foreach(var itm in Model) \n{ \n\t@itm.{0} \n}";
            string itm;
            if (type == (int)ControlType.DropDownList || type == (int)ControlType.CheckBoxList)
            {
                itm = "CatalogValue";
            }
            else
            {
                itm = "ExtendFieldValue";
            }
            return result.Replace("{0}", itm);
        }

        /// <summary>
        /// Cập nhật giá trị của key được thay đổi trên form in.
        /// </summary>
        /// <param name="docId"> document id</param>
        /// <param name="changes">Danh sách các key và giá trị của nó  "keycode": "giá trị"</param>
        public void UpdateKey(Guid docId, Dictionary<string, string> changes)
        {
            foreach (var key in changes)
            {
                var keyCode = key.Key;
                var keyElements = keyCode.Replace("{", "").Replace("_form}", "").Split('_');
                if (keyElements.Length < 3)
                {
                    continue;
                }
                Guid controlId;
                if (!Guid.TryParse(keyElements[keyElements.Length - 2], out controlId))
                {
                    continue;
                }
                Guid formId;
                if (!Guid.TryParse(keyElements[keyElements.Length - 1], out formId))
                {
                    continue;
                }

                // Cập nhật trong bảng doc_exfield
                var exfield = _docExfieldBll.Get(docId, formId, controlId);
                if (exfield != null)
                {
                    exfield.ExtendFieldValue = key.Value;
                    _docExfieldBll.Update(exfield);
                }

                // Cập nhật trong form của document
                UpdateKeyInForm(docId, formId, controlId, key.Value);
            }
        }

        #endregion Template Key

        #region Template

        /// <summary>
        /// Trả về nội dung in
        /// <para> Thay thế  key trong nội dung của mẫu bằng giá trị thật và định dạng của chúng</para>
        /// <para> (Tienbv@bkav.com 210313)</para>
        /// </summary>
        /// <param name="template"> Mẫu key</param>
        /// <param name="userId">User dăng nhập hiện tại.</param>
        /// <param name="docId">Document id</param>
        /// <param name="formId">Form id</param>
        /// <param name="paperAddIds">Danh sách các giấy tờ bổ sung.</param>
        /// <param name="feeAddIds">Danh sách các lệ phí bổ sung</param>
        /// <returns>Nội dung của mẫu đã được thay thế giá trị.</returns>
        public string ParseContentNew(Template template, int userId, Guid docId,
            Guid? formId, string paperAddIds = null,
            string feeAddIds = null)
        {
            if (template == null)

            {
                throw new ArgumentNullException("template");
            }

            var document = _documentBll.Get(docId);
            var specialKeys = GetSpecialKeys(document);
            return ParseContentNew(template, userId, document, formId, paperAddIds, feeAddIds, specialKeys);
        }

        /// <summary>
        /// Trả về nội dung in
        /// <para> Thay thế  key trong nội dung của mẫu bằng giá trị thật và định dạng của chúng</para>
        /// <para> (Tienbv@bkav.com 210313)</para>
        /// </summary>
        /// <param name="template"> Mẫu key</param>
        /// <param name="userId">User dăng nhập hiện tại.</param>
        /// <param name="doc">Document id</param>
        /// <param name="formId">Form id</param>
        /// <param name="paperAddIds">Danh sách các giấy tờ bổ sung.</param>
        /// <param name="feeAddIds">Danh sách các lệ phí bổ sung</param>
        /// <param name="specialKeys">Danh sách các key đặc biệt.</param>
        /// <param name="currentUserId">Id người đang giữ</param>
        /// <returns>Nội dung của mẫu đã được thay thế giá trị.</returns>
        public string ParseContentNew(Template template, int userId, Document doc,
            Guid? formId, string paperAddIds = null,
            string feeAddIds = null, Dictionary<string, string> specialKeys = null, int currentUserId = 0)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            if (doc == null)
            {
                throw new Exception("document not exist.");
            }

            if (specialKeys == null)
            {
                specialKeys = GetSpecialKeys(doc);
            }

            var razorEngine = new RazorEngineUtil();
            var keyCodes = TemplateUtil.GetKeysInContent(template.Content);
            var keys = _templateKeyBll.Gets(t => keyCodes.Contains(t.Code) && t.IsActive);
            var view = GetTemplateView(keys, keyCodes, template.Content, doc, formId, specialKeys);
            var model = GetModel(keys, keyCodes, userId, doc.DocumentId, formId, paperAddIds, feeAddIds, specialKeys, 0, currentUserId);

            return razorEngine.FormatStringLiteral(view, model);
        }

        private Dictionary<string, List<IDictionary<string, object>>> GetModel(IEnumerable<TemplateKey> templateKeys,
            IEnumerable<string> keyCodes, int userId, Guid docId, Guid? formId,
            string paperAddIds, string feeAddIds, Dictionary<string, string> specialKeys = null, int docCopyId = 0, int currentUserId = 0)
        {
            var result = new Dictionary<string, List<IDictionary<string, object>>>();
            foreach (var keyCode in keyCodes)
            {
                if (keyCode.EndsWith("_form}") || (specialKeys != null && specialKeys.ContainsKey(keyCode.ToLower())))
                {
                    continue;
                }

                var key = templateKeys.SingleOrDefault(t => t.Code == keyCode);
                if (key != null)
                {
                    var keyValue = _templateKeyBll.GetValue(key, userId, docId, formId, paperAddIds, feeAddIds, docCopyId, currentUserId).ToList();
                    result.Add(keyCode.Replace("{", "").Replace("}", ""), keyValue);
                }
            }
            return result;
        }

        /// <summary>
        /// HopCV:211015
        /// </summary>
        /// <param name="templateKeys">Danh sách key trong mẫu phôi</param>
        /// <param name="keyCodes">Danh sách các mã</param>
        /// <param name="content">Nội dung</param>
        /// <param name="document">Văn bản/hồ sơ (Đối tượng văn bản này có thể chưa tồn tại trên hệ thống)
        /// => Đối tượng tạm trên hệ thống nhưng có đầy đủ các thông tin của 1 văn bản thật
        /// => việc từ chối tiếp nhận văn bản</param>
        /// <param name="formId">Id form động</param>
        /// <param name="specialKeys">Các key đặc biệt</param>
        /// <returns></returns>
        private string GetTemplateView(IEnumerable<TemplateKey> templateKeys, IEnumerable<string> keyCodes,
          string content, Document document, Guid? formId, Dictionary<string, string> specialKeys = null)
        {
            var sb = new StringBuilder(content);

            // Nội dung hồ sơ từ form động
            var contents = GetDocumentDynamicContent(document, formId);

            if (keyCodes != null && keyCodes.Any())
            {
                foreach (var keyCode in keyCodes)
                {
                    string keyValue;

                    // Thay thế giá trị các key đặc biệt trước
                    if (specialKeys != null && specialKeys.ContainsKey(keyCode.ToLower()))
                    {
                        keyValue = specialKeys[keyCode.ToLower()];
                    }
                    else
                    {
                        // Các key được lấy từ form động
                        if (keyCode.EndsWith("_form}"))
                        {
                            var key = keyCode.Replace("{", "").Replace("_form}", "");
                            keyValue = TemplateUtil.GetValueInForm(key, contents);
                        }
                        // Các key thông thường lấy dữ liệu từ db
                        else
                        {
                            var templateKey = templateKeys.SingleOrDefault(t => t.Code == keyCode);
                            keyValue = templateKey == null ? string.Empty : FormatTemplate(templateKey);
                        }
                    }
                    sb.Replace(keyCode, keyValue);
                }
            }

            return sb.ToString();
        }

        private string FormatTemplate(TemplateKey key)
        {
            var result = key.HtmlTemplate;

            var keyCode = key.Code.Replace("{", "").Replace("}", "");
            result = result.Replace("Model", "Model[\"" + keyCode + "\"]");

            return result;
        }

        #region Hỏi đáp

        /// <summary>
        /// Parse nội dung cho câu hỏi
        /// </summary>
        /// <param name="template"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public string ParseQuestionTemplate(Template template, Question question)
        {
            var sb = new StringBuilder(template.Content);
            var keyCodes = TemplateUtil.GetKeysInContent(template.Content);

            var specialKeys = new Dictionary<string, string>
                {
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.QuestionName), question.Name
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.AskContent), question.Detail
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.AskPeople), question.AskPeople
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.AskDate), question.Date.ToString("hh:mm dd/MM/yyyy")
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.AnswerPeople), question.AnswerPeople
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.Answer), HttpUtility.UrlDecode(question.Answer)
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.AnswerDate), question.AnswerDate.Value.ToString("hh:mm dd/MM/yyyy")
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.AnswerDepartment), ""
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.DocCode), question.Doc_Code
                    },
                    {
                        EnumHelper<QuestionTemplateKeyEnum>.GetDatabaseValue(QuestionTemplateKeyEnum.Compendium), question.Doc_Compendium
                    },
                };

            //HopCv:300116=> các key thứ ngày tháng
            var commonKeys = GetCommonKey();

            foreach (var keyCode in keyCodes)
            {
                string keyValue = keyCode;

                if (specialKeys.ContainsKey(keyCode.ToLower()))
                {
                    keyValue = specialKeys[keyCode.ToLower()];
                }
                else if (commonKeys.ContainsKey(keyCode.ToLower()))
                {
                    keyValue = commonKeys[keyCode.ToLower()];
                }

                sb.Replace(keyCode, keyValue);
            }

            return sb.ToString();
        }

        #endregion

        #region crystal report

        /// <summary>
        /// QuangP: parse content vào crystal report, không sử dụng database/dataset
        /// </summary>
        /// <param name="rd">ReportDocument</param>
        /// <param name="userId"></param>
        /// <param name="docId"></param>
        /// <param name="specialKeys"></param>
        public void ParseCrystalReportContent(ReportDocument rd, int userId, Guid docId, Dictionary<string, string> specialKeys = null)
        {
            var keyCodes = GetParameterFields(rd);
            keyCodes = keyCodes.Select(k => "{" + k.Clone() + "}").ToList();//Thêm 2 dấu ngoặc vì templatekey dùng cho html đang sử dung cấu trúc {key}
            var keys = _templateKeyBll.Gets(t => keyCodes.Contains(t.Code) && t.IsActive);
            var model = GetModel(keys, keyCodes, userId, docId, null, null, null, specialKeys);
            foreach (var specialKey in specialKeys)
            {
                BindKeyToCrystalReport(rd, specialKey.Key, specialKey.Value);
            }
            foreach (var m in model)
            {
                var value = m.Value.First().Values.First().ToString();
                BindKeyToCrystalReport(rd, m.Key, value);
            }
        }

        /// <summary>
        /// QuangP: bind giá trị các key vào file report
        /// </summary>
        /// <param name="rd"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void BindKeyToCrystalReport(ReportDocument rd, string key, string value)
        {
            var delimiters = new[] { '{', '}' };
            key = key.StripChars(delimiters);
            var crParameterValues = new ParameterValues();
            var crParameterDiscreteValue = new ParameterDiscreteValue();
            crParameterDiscreteValue.Value = value;
            var crParameterFieldDefinitions = rd.DataDefinition.ParameterFields;
            var crParameterFieldDefinition = crParameterFieldDefinitions[key];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
        }

        private DataSet BindReportDb(ReportDocument rd, int userId)
        {
            var ds = new DataSet();
            var keyCodes = new List<string>();
            Table table = null;

            foreach (var t in rd.Database.Tables)
            {
                var tempTable = t.To<Table>();
                if (t.To<Table>().Name != "Special")
                {
                    table = tempTable;
                }
            }

            var dt = ds.Tables.Add(table.Name);
            foreach (var field in table.Fields)
            {
                var tempField = field.To<DatabaseFieldDefinition>();
                dt.Columns.Add(tempField.Name, typeof(string));
                keyCodes.Add(tempField.Name);
            }
            var specials = _reportService.GetSpecialTable(userId);
            ds.Tables.Add(specials);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rd"></param>
        /// <param name="template"></param>
        /// <param name="docCopyIds"></param>
        /// <param name="userId"></param>
        /// <param name="suppId"></param>
        /// <returns></returns> 
        public DataSet CreateDataSet(ReportDocument rd, Template template,
            List<int> docCopyIds, int userId, int suppId, byte[] imageQr = null)
        {
            // Todo - QuangP: 
            //    - Cần truyền danh sách DocumentCopyId vào câu truy vấn và thực thi 1 lần lấy ra kết quả luôn.
            //    - Lấy danh sách docCopys làm gì nhỉ? câu truy vấn nó cần mỗi id thôi mà? nếu muốn lấy docId nữa thì trước tiên cần kiểm tra
            //      Xem câu truy vấn có @docId không thì mới lấy. Hoặc truyền thẳng từ Client lên luôn, chổ này không cần thiết phải select lấy ra từ Db
            //    - BindReportDb thực chất là SpecialTables => cần đặt tên cho rõ ràng hơn.

            var ds = BindReportDb(rd, userId);
            var dt = ds.Tables[0];
            var data = _templateService.GetValues(template, userId, suppId, docCopyIds);
            if (dt.Columns.Contains("QrDocCode"))
            {
                dt.Columns.Remove("QrDocCode");
                dt.Columns.Add("QrDocCode", System.Type.GetType("System.Byte[]"));
            }

            foreach (var d in data)
            {
                var row = dt.NewRow();
                var a = "";
                foreach (var col in dt.Columns)
                {
                    var tempCol = col.To<DataColumn>();
                    if (imageQr != null && tempCol.ColumnName == "QrDocCode")
                    {
                        row["QrDocCode"] = imageQr;
                        a = tempCol.DataType.ToString();
                    }
                    else if (tempCol.ColumnName == "test")
                    {
                        row["test"] = "co anhr";
                    }
                    else
                    {
                        var colValue = d.ContainsKey(tempCol.ColumnName) ? d[tempCol.ColumnName] : "";
                        row[tempCol.ColumnName] = colValue == null ? "" : colValue.ToString();
                    }
                }
              
                dt.Rows.Add(row);
            }
            
            return ds;
        }

        private List<string> GetParameterFields(ReportDocument rd)
        {
            var paramFields = rd.DataDefinition.ParameterFields;
            var keyCodes = new List<string>();
            foreach (var paramField in paramFields)
            {
                keyCodes.Add(paramField.To<ParameterFieldDefinition>().ParameterFieldName);
            }
            return keyCodes;
        }

        #endregion

        #region

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopy"></param>
        /// <param name="currentUserId"></param>
        /// <param name="pathFile"></param>
        /// <param name="templateId"></param>
        /// <param name="suppId"></param>
        public void CrystalToPDF(DocumentCopy docCopy, int currentUserId, out string pathFile, int templateId = 0, int suppId = 0)
        {
            pathFile = "";
            var rd = GetReport(docCopy, currentUserId, templateId, suppId);
            CrystalToFile(rd, out pathFile);
        }

        /// <summary>
        /// In ra máy
        /// </summary>
        /// <param name="rd">ReportDocument</param>
        /// <param name="pathFile">tên máy in</param>
        private void CrystalToFile(ReportDocument rd, out string pathFile)
        {
            rd.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
            pathFile = System.IO.Path.Combine(ResourceLocation.Default.FileTemp, "Print"+ Guid.NewGuid());
            var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            var nameFile = Guid.NewGuid().ToString();
            var file = FileManager.Default.Create(stream, pathFile, nameFile, ".pdf");
            pathFile = file + "";
            rd.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopy"></param>
        /// <param name="currentUserId"></param>
        /// <param name="templateId"></param>
        /// <param name="suppId"></param>
        /// <returns></returns>
        private ReportDocument GetReport(DocumentCopy docCopy, int currentUserId, int templateId = 0, int suppId = 0)
        {
            Template template;

            template = _templateService.GetTemplateForPrint(templateId, docCopy.DocTypeId, docCopy.Document.ListDocFieldId);

            if (template == null || string.IsNullOrWhiteSpace(template.ContentFileLocalName))
            {
                throw new ArgumentNullException(_resourceService.GetResource("Document.NotExistTemplate"));
            }

            var rptPath = System.IO.Path.Combine(ResourceLocation.Default.CrystalReport, template.ContentFileLocalName);
            if (!System.IO.File.Exists(rptPath))
            {
                throw new ArgumentNullException(_resourceService.GetResource("Document.NotExistTemplate"));
            }

            var rd = new ReportDocument();
            rd.Load(rptPath);
            var docCopyIds = new List<int>() { docCopy.DocumentCopyId };
            var ds = CreateDataSet(rd, template, docCopyIds, currentUserId, suppId);
            rd.SetDataSource(ds);

            return rd;
        }
        #endregion

        #region Document online

        /// <summary>
        ///  Parse nội dung cho việc tiếp nhâ, từ chối, yêu cầu bổ dung phần đăng ký qua mạng
        /// </summary>
        /// <param name="template">Mẫu template</param>
        /// <param name="docFieldName">Tên lĩnh vực</param>
        /// <param name="docCode">Mã hồ sơ</param>
        /// <param name="docTypeName">Tên loại văn bản</param>
        /// <param name="compendium">Trích yếu</param>
        /// <param name="accountCommand">Người thực thi</param>
        /// <param name="dateCommand">Thời gian thực thi thao tác</param>
        /// <param name="citizenName">Tên công dân</param>
        /// <param name="fullNameCommand">Họ tên đầy đủ người thực thi</param>
        /// <param name="officeName">Tên cơ quan</param>
        /// <param name="comment"> Ý kiến</param>
        /// <param name="dateRegister">Ngày đăng ký</param>
        /// <param name="userId">Id người gửi</param>
        /// <param name="supplementary">Yêu cầu bổ sung</param>
        /// <param name="supplementaryDate">Thời hạn bổ sung</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string ParseDocumentOnlineTemplate(
            Template template,
            string docFieldName,
            string docCode,
            string docTypeName,
            string compendium,
            string accountCommand,
            string dateCommand,
            string fullNameCommand,
            string officeName,
            string citizenName,
            string comment,
            string dateRegister,
            int? userId = 0,
            string supplementary = "",
            string supplementaryDate = "",
            string token = "")
        {
            var sb = new StringBuilder(template.Content);

            #region Online key

            var onlineLink = _onlineSettings.OnlineLink.ToLower();
            if (!onlineLink.EndsWith("/"))
            {
                onlineLink += "/";
            }
            onlineLink += string.Format("tra-cuu-tien-do?doccode={0}&token={1}", docCode, token);

            //các key dung cho bên online
            var onlineKeys = new Dictionary<string, string>
                {
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DocFieldName), docFieldName
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DocCode), docCode
                    },
                      {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DocTypeName), docTypeName
                    },
                      {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.Compendium), compendium
                    },
                      {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.AccountCommand), accountCommand
                    },
                      {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DateCommand), dateCommand
                    },
                       {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.FullNameCommand), fullNameCommand
                    },
                      {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.OfficeName), officeName
                    },
                     {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.CitizenName), citizenName
                    }
                    ,
                     {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.Comment), comment
                    },
                      {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DateRegister), dateRegister
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.LinkAccess), onlineLink
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.SupplementaryPaper), supplementary
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.SupplementaryExpire), supplementaryDate
                    }
                };

            //Các key dung chung
            var commonKeys = GetCommonKey();

            #endregion

            var keyCodes = TemplateUtil.GetKeysInContent(template.Content);
            var razorEngine = new RazorEngineUtil();
            var result = new Dictionary<string, List<IDictionary<string, object>>>();
            var keyInDbs = new List<TemplateKey>();

            if (keyCodes != null && keyCodes.Any())
            {
                var listKeyCodes = keyCodes.Where(p =>
                        !onlineKeys.ContainsKey(p.ToLower())
                        || !commonKeys.ContainsKey(p.ToLower())).ToList();


                if (listKeyCodes != null && listKeyCodes.Any())
                {
                    keyInDbs = _templateKeyBll.Gets(t => listKeyCodes.Contains(t.Code) && t.IsActive).ToList();
                }

                foreach (var keyCode in keyCodes)
                {
                    string keyValue = keyCode;

                    if (onlineKeys.ContainsKey(keyCode.ToLower()))
                    {
                        keyValue = onlineKeys[keyCode.ToLower()];
                    }
                    else if (commonKeys.ContainsKey(keyCode.ToLower()))
                    {
                        keyValue = commonKeys[keyCode.ToLower()];
                    }
                    else
                    {
                        if (keyInDbs != null && keyInDbs.Any())
                        {
                            var templateKey = keyInDbs.SingleOrDefault(t => t.Code == keyCode);
                            if (templateKey != null)
                            {
                                keyValue = FormatTemplate(templateKey);
                                var value = _templateKeyBll.GetValue(templateKey, userId.Value, Guid.Empty, null).ToList();
                                result.Add(keyCode.Replace("{", "").Replace("}", ""), value);
                            }
                        }
                    }

                    sb.Replace(keyCode, keyValue);
                }
            }

            return (keyInDbs != null && keyInDbs.Count > 0)
                ? razorEngine.FormatStringLiteral(sb.ToString(), result)
                : sb.ToString();
        }

        #endregion

        #endregion Template

        #region Private Methods

        /// <summary>
        /// HopCv: 211015
        /// Lấy nội dung form động
        /// </summary>
        /// <param name="document">văn bản/hồ sơ</param>
        /// <param name="formId">Id form động</param>
        /// <returns></returns>
        private List<JsDocument> GetDocumentDynamicContent(Document document, Guid? formId)
        {
            var result = new List<JsDocument>();
            if (document == null || (document.DocumentContents == null || !document.DocumentContents.Any()))
            {
                return result;
            }
#if HoSoMotCuaEdition
            foreach (var content in document.DocumentContents)
            {
                if (content.FormTypeIdInEnum != Entities.FormType.DynamicForm)
                {
                    continue;
                }

                JsDocument jsContent;
                if (DynamicFormHelper.TryParse(content.Content, out jsContent))
                {
                    if (formId != null && jsContent.FormId.Equals(((Guid)formId).ToString("N")))
                    {
                        return new List<JsDocument> { jsContent };
                    }
                    result.Add(jsContent);
                }
            }
#endif
            return result;
        }

        private void UpdateKeyInForm(Guid docId, Guid formId, Guid controlId, string newValue)
        {
#if HoSoMotCuaEdition
            var document = _documentBll.Get(docId);
            if (document == null || (document.DocumentContents == null || !document.DocumentContents.Any()))
            {
                return;
            }

            foreach (var content in document.DocumentContents)
            {
                // Nếu không phải là form động => bỏ qua
                if (content.FormTypeIdInEnum != Bkav.eGovCloud.Entities.FormType.DynamicForm)
                {
                    continue;
                }

                JsDocument jsContent;
                // Nếu không parse dc từ json ra object form động tương ứng => bỏ qua
                if (!DynamicFormHelper.TryParse(content.Content, out jsContent))
                {
                    continue;
                }

                // Nếu không phải là form cần update => bỏ qua
                if (!jsContent.FormId.Equals(formId.ToString("N")))
                {
                    continue;
                }

                var controls = jsContent.DocFieldJson.Where(f => f.ControlId == controlId).ToList();
                // Nếu trong form không có control cần update => bỏ qua
                if (!controls.Any())
                {
                    continue;
                }

                // Lấy ra content Id và cập nhập giá trị mới vào chuỗi json
                var contentId = content.DocumentContentId;
                foreach (var control in controls)
                {
                    control.Value = newValue;
                }
                var newContent = jsContent.StringifyJs();

                // Cập nhật content Id cho document tương ứng
                UpdateDocumentContent(document, contentId, newContent);

                break; // thoát
            }
#endif
        }

        private void UpdateDocumentContent(Document document, int contentId, string newContent)
        {
            if (contentId > 0)
            {
                var content = document.DocumentContents.Single(c => c.DocumentContentId == contentId);
                content.Content = newContent;
                _docContentBll.Update(content);
            }
        }

        private string DayOfWeek(DateTime dateTime)
        {
            var result = string.Empty;
            var dayOfWeek = dateTime.DayOfWeek;
            switch (dayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ChuNhat");
                        break;
                    }
                case System.DayOfWeek.Monday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuHai");
                        break;
                    }
                case System.DayOfWeek.Tuesday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuBa");
                        break;
                    }
                case System.DayOfWeek.Wednesday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuTu");
                        break;
                    }
                case System.DayOfWeek.Thursday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuNam");
                        break;
                    }
                case System.DayOfWeek.Friday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuSau");
                        break;
                    }
                case System.DayOfWeek.Saturday:
                    {
                        result = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuBay");
                        break;
                    }
            }

            return result;
        }

        private Dictionary<string, string> GetCommonKey()
        {
            var dateNow = DateTime.Now;
            var thuNgayThangNam = string.Empty;
            try
            {
                var str = _resourceService.GetResource("Template.CommonTemplateKeyEnum.ThuNgayThangNam");
                thuNgayThangNam = string.Format(str, DayOfWeek(dateNow), dateNow.Day, dateNow.Month, dateNow.Year);
            }
            catch { }

            var result = new Dictionary<string, string>
            { 
                {
                    EnumHelper<CommonTemplateKeyEnum>.GetDatabaseValue(CommonTemplateKeyEnum.NgayThangNam), dateNow.ToString(DATE_FORMAT)
                },
                {
                    EnumHelper<CommonTemplateKeyEnum>.GetDatabaseValue(CommonTemplateKeyEnum.ThuNgayThangNam), thuNgayThangNam
                },
            };

            return result;
        }

        private Dictionary<string, string> GetSpecialKeys(Document doc)
        {
            var result = GetCommonKey();
            DocField docField = null;
            var currentUser = _userService.CurrentUser == null ? "" : _userService.CurrentUser.FullName;
            var userCreated = _userService.GetFromCache(doc.UserCreatedId);

            var onlineLink = "";
            //_onlineSettings.OnlineLink.ToLower()
            if (_onlineSettings.OnlineLink == null)
            {
                onlineLink = "";
            }
            else
            {
                onlineLink = _onlineSettings.OnlineLink.ToLower();
            }

            var officeName = _infomationService.Gets().First().Name;
            if (!onlineLink.EndsWith("/"))
            {
                onlineLink += "/";
            }
            onlineLink += string.Format("tra-cuu-tien-do?doccode={0}&token={1}", doc.DocCode, doc.DocumentId);

            var onlineKeys = new Dictionary<string, string>
                {
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DocFieldName), docField == null? "" : docField.DocFieldName
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DocCode), doc.DocCode
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DocTypeName),  doc.DocTypeName
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.Compendium), doc.Compendium
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.AccountCommand), userCreated == null? "" : userCreated.Username
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DateCommand), doc.DateCreated.ToString(DATE_FORMAT)
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.FullNameCommand), userCreated == null? "" : userCreated.FullName
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.OfficeName), officeName
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.CitizenName), doc.CitizenName
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.Comment), ""
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.DateRegister), doc.DateCreated.ToString(DATE_FORMAT)
                    },
                    {
                        EnumHelper<DocumentOnlineTemplateKeyEnum>.GetDatabaseValue(DocumentOnlineTemplateKeyEnum.LinkAccess), onlineLink
                    }
                };

            result = result.Union(onlineKeys).ToDictionary(x => x.Key, x => x.Value);

            result.Add(EnumHelper<SpecialKeyEnum>.GetDatabaseValue(SpecialKeyEnum.NguoiDangNhap), currentUser);
            result.Add(EnumHelper<SpecialKeyEnum>.GetDatabaseValue(SpecialKeyEnum.NgayThangHienTai), DateTime.Now.ToString(DATE_FORMAT));

            return result;
        }

        #endregion Private Methods
    }
}