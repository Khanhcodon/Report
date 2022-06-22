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
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Class : FormHelper - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 230113
    /// Author      : TienBV
    /// </author>
    /// <summary>
    /// <para>Cung cấp các API hỗ trợ xử lý biểu mẫu động</para>
    /// <para>(TienBV@bkav.com - 230113)</para>
    /// </summary>
    public class FormHelper
    {
        private readonly FormBll _formService;

        /// <summary>
        /// Khởi tạo <see cref="FormHelper"/>
        /// </summary>
        /// <param name="formBll">Form bll</param>
        public FormHelper(FormBll formBll)
        {
            _formService = formBll;
        }

        /// <summary>
        /// Chuyển kiểu Form (bảng Form trong database) sang thành kiểu JsFormModel (biểu mẫu động xử lý ở client)
        /// </summary>
        /// <param name="forms"></param>
        /// <returns></returns>
        public List<JsFormModel> ParseFormModel(IEnumerable<Form> forms)
        {
            var result = forms.Where(f => f.FormTypeId == (int)FormType.DynamicForm).Select(ParseFormModel).ToList();
            return result;
        }

        /// <summary>
        /// Chuyển kiểu Form (bảng Form trong database) sang thành kiểu JsFormModel (biểu mẫu động xử lý ở client)
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public JsFormModel ParseFormModel(Form form)
        {
            JsFormModel result;
            List<JsControl> controls;
            if (!DynamicFormHelper.TryParse(form.Json, out controls))
            {
                result = new JsFormModel
                {
                    FormId = form.FormId.ToString("N"),
                    Description = form.FormName,
                    JssCatalog = new List<JsCatalog>(),
                    JssForm = new List<JsControl>(),
                    MaxRow = 1
                };
            }
            else
            {
                var maxRow = controls.Select(r => r.PosRow).Max();
                result = new JsFormModel
                {
                    FormId = form.FormId.ToString("N"),
                    Description = form.FormName,
                    JssCatalog = _formService.GetCatalogs(form).ToListJsCatalog(),
                    JssForm = controls,
                    MaxRow = maxRow
                };
            }
            return result;
        }

        /// <summary>
        /// Chuyển kiểu JsDocument (dữ liệu đã nhập của biểu mẫu động) về dạng JsFormModel (Biểu mẫu động đã được bind dữ liệu nhập vào để xử lý ở client).
        /// </summary>
        /// <param name="docJsons"></param>
        /// <returns></returns>
        public List<JsFormModel> ParseFormModel(List<JsDocument> docJsons)
        {
            var result = new List<JsFormModel>();
            if (!docJsons.Any())
                return result;
            result.AddRange(docJsons.Select(ParseFormModel));
            return result;
        }

        /// <summary>
        /// Chuyển kiểu JsDocument (dữ liệu đã nhập của biểu mẫu động) về dạng JsFormModel (Biểu mẫu động đã được bind dữ liệu nhập vào để xử lý ở client).
        /// </summary>
        /// <param name="jsDoc"></param>
        /// <returns></returns>
        public JsFormModel ParseFormModel(JsDocument jsDoc)
        {
            if (jsDoc == null)
                return new JsFormModel();

            var formId = jsDoc.FormId;
            var formJson = _formService.GetJson(Guid.Parse(formId));
            JsFormModel result;
            if (string.IsNullOrEmpty(formJson))
            {
                result = new JsFormModel
                {
                    FormId = jsDoc.FormId,
                    Description = jsDoc.Description,
                    JssCatalog = new List<JsCatalog>(),
                    JssForm = new List<JsControl>(),
                    MaxRow = 1
                };
            }
            else
            {
                List<JsControl> formControls;
                if (!DynamicFormHelper.TryParse(formJson, out formControls))
                {
                    throw new Exception("Dữ liệu của form không hợp lệ.");
                }
                formControls = ConcatJson(jsDoc.DocFieldJson, formControls);
                //var formControls = DynamicFormHelper.Parse(json).ToList();
                var maxRow = formControls.Max(c => c.PosRow);
                result = new JsFormModel
                {
                    FormId = jsDoc.FormId,
                    Description = jsDoc.Description,
                    JssCatalog = _formService.GetCatalogsInForm(formControls).ToListJsCatalog(),
                    JssForm = formControls,
                    MaxRow = maxRow
                };
            }
            return result;
        }

        /// <summary>
        /// Xuất form động ra mẫu phôi
        /// </summary>
        /// <param name="jsDoc">Nội dung formd động</param>
        /// <param name="path">Đường dẫn mẫu phôi</param>
        /// <param name="outPath">Đường dẫn file đầu ra</param>
        /// <param name="isPdf">Parse pdf file đầu ra</param>
        /// <returns></returns>
        public void ParseEmbryonic(JsDocument jsDoc, string path, string outPath, bool isPdf = true)
        {
            var docFieldJsons = jsDoc.DocFieldJson;
            var keyValues = GetKeyValuesFromFile(path, docFieldJsons);

            var docxParser = new DocxParser();
            if (isPdf)
            {
                docxParser.ParseTemplateToPdf(path, keyValues, outPath);
            }
            else
            {
                docxParser.ParseTemplateToFile(path, keyValues, outPath);
            }
        }

        /// <summary>
        /// Xuất form động ra mẫu phôi và trả về stream của mẫu
        /// </summary>
        /// <param name="jsDoc">Nội dung formd động</param>
        /// <param name="path">Đường dẫn mẫu phôi</param>
        /// <param name="isPdf">Parse pdf file đầu ra</param>
        /// <returns></returns>
        public Stream ParseEmbryonic(JsDocument jsDoc, string path, bool isPdf = true)
        {
            var docFieldJsons = jsDoc.DocFieldJson;
            var keyValues = GetKeyValuesFromFile(path, docFieldJsons);
            var tempPath = FileUtil.GetRandomGuidFile(Path.GetTempPath(), isPdf ? ".pdf" : ".docx");
            Stream result;
            var ms = new MemoryStream();
            var docxParser = new DocxParser();
            if (isPdf)
            {
                docxParser.ParseTemplateToPdf(path, keyValues, tempPath);
            }
            else
            {
                docxParser.ParseTemplateToFile(path, keyValues, tempPath);
            }

            using (StreamReader reader = new StreamReader(tempPath))
            {
                result = reader.BaseStream;
                result.CopyTo(ms);
                ms.Position = 0;
            }
            try
            {
                System.IO.File.Delete(tempPath);
            }
            catch { };

            return ms;
        }

        private Dictionary<string, string> GetKeyValuesFromFile(string path, List<JsDocumentItem> docFieldJsons)
        {
            var embryonicKeyPattern = @"#\w{10}_([^< ])*#";
            var regex = new Regex(embryonicKeyPattern);
            var result = new Dictionary<string, string>();

            string documentText;
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, isEditable: false))
            {
                using (StreamReader reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    documentText = reader.ReadToEnd();
                }

                var keys = regex.Matches(documentText);
                foreach (Match key in keys)
                {
                    string controlId;
                    if (!GetControlIdFromKey(key.Value, out controlId))
                    {
                        continue;
                    }

                    var field = docFieldJsons.SingleOrDefault(d => d.ControlId.To10Char() == controlId);
                    result[key.Value] = field != null ? field.Value : "";
                }
            }

            return result;
        }

        private bool GetControlIdFromKey(string key, out string id)
        {
            key = key.Replace("#", "");
            id = "";
            if (key.IndexOf("_") > 0)
            {
                id = key.Substring(0, key.IndexOf("_"));
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docFieldJson"></param>
        /// <param name="formControls"></param>
        /// <returns></returns>
        private List<JsControl> ConcatJson(List<JsDocumentItem> docFieldJson, List<JsControl> formControls)
        {
            if (!docFieldJson.Any())
            {
                return formControls;
            }
            if (!formControls.Any())
            {
                return new List<JsControl>();
            }
            foreach (var formControl in formControls)
            {
                foreach (var docControl in docFieldJson)
                {
                    // Textbox
                    if (formControl.TypeId == 9)
                    {
                        if (formControl.TypeId == docControl.TypeId && (formControl.ControlId == docControl.ControlId))
                        {
                            formControl.ControlValue = docControl.Value;
                            break;
                        }
                    }

                    // DropDownList
                    if (formControl.TypeId == 10)
                    {
                        if (formControl.TypeId == docControl.TypeId && (formControl.ControlId == docControl.ControlId))
                        {
                            formControl.ControlValue = docControl.Value;
                            var catalog = docControl.CatalogSelectedObject;
                            if (catalog != null)
                            {
                                var propSelected = formControl.Properties.Single(p => p.Id == 31);
                                propSelected.Value = catalog.Value;
                            }
                            break;
                        }
                    }
                }
            }
            return formControls.ToList();
        }
    }
}
