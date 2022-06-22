using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Entities.Common;
using Fasterflect;
using Bkav.eGovCloud.Business.Common;
using System.Text;
using System.Net.Http;
using System.Globalization;

namespace Bkav.eGovCloud.Api.Controller
{
    public class MergedCell
    {
        public int row { get; set; }

        public int col { get; set; }

        public int rowspan { get; set; }

        public int colspan { get; set; }

        public bool removed { get; set; }

        public MergedCell(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.rowspan = 1;
            this.colspan = 1;
            this.removed = false;
        }
    }

    public class FormulaRow
    {
        public int type { get; set; }

        public int row { get; set; }

        public int length { get; set; }

        public string formualStr { get; set; }

        public FormulaRow(int row, int type, int length)
        {
            this.row = row;
            this.length = length;
            this.type = type;
            this.formualStr = string.Empty;

        }
    }

    public class DefineField
    {
        public List<List<string>> data;

        public List<MergedCell> mergedCells;

        public int countCols;

        public List<string> colWidths;

        public DefineField()
        {
            data = new List<List<string>>();
            mergedCells = new List<MergedCell>();
            countCols = 0;
            colWidths = new List<string>();
        }
    }

    public class DefineConfig
    {
        public List<List<object>> data;

        public List<object> columns;

        public List<object> hiddenColumns;

        public List<object> autoSizeColumns;

        public List<object> catalogDetail;

        public List<object> catalogDefaults;

        public DefineConfig()
        {
            data = new List<List<object>>();
            columns = new List<object>();
            hiddenColumns = new List<object>();
            autoSizeColumns = new List<object>();
            catalogDetail = new List<object>();
            catalogDefaults = new List<object>();
        }
    }

    public class DefineValue
    {
        public List<List<object>> data;

        public List<object> columns;

        public List<object> mergedCells;

        public List<object> readOnlys;

        public List<List<string>> classCells;

        public DefineValue()
        {
            data = new List<List<object>>();
            columns = new List<object>();
            mergedCells = new List<object>();
            readOnlys = new List<object>();
            classCells = new List<List<string>>();
        }
    }

    public class ReportModeData
    {
        public string data { get; set; }
    }
    
    public class GovSyncController : EgovApiBaseController
    {
        private readonly DocumentCopyBll _documentCopyService;
        private readonly FormGroupBll _formGroupService;
        private readonly DocFieldBll _docFieldService;
        private readonly FormBll _formService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly DocTypeBll _doctypeService;
        private readonly AdminGeneralSettings _adminSetting;
        private readonly DocTypeFormBll _doctypeformService;
        private readonly DocumentPublishPlusBll _docpublishplusService;
        private readonly ReportModeBll _reportModeService;

        private readonly FormHelper _formHelper;

        private const int FORMULA_TYPE_NORMAL = 1;
        private const int FORMULA_TYPE_READONLY = 2;
        private const int FORMULA_TYPE_SUM = 3;
        private const int FORMULA_TYPE_AVG = 4;
        private const int FORMULA_TYPE_MAX = 5;
        private const int FORMULA_TYPE_MIN = 6;

        private List<ReceiveReportStructContentAttributeDataModel> listDataAttr = new List<ReceiveReportStructContentAttributeDataModel>();

        public GovSyncController()
        {
            _documentCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _formGroupService = DependencyResolver.Current.GetService<FormGroupBll>();
            _docFieldService = DependencyResolver.Current.GetService<DocFieldBll>();
            _formService = DependencyResolver.Current.GetService<FormBll>();
            _doctypeFormService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _formHelper = DependencyResolver.Current.GetService<FormHelper>();
            _adminSetting = DependencyResolver.Current.GetService<AdminGeneralSettings>();
            _doctypeformService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _docpublishplusService = DependencyResolver.Current.GetService<DocumentPublishPlusBll>();
            _reportModeService = DependencyResolver.Current.GetService<ReportModeBll>();
        }

        /// <summary>
        /// Lấy danh sách báo cáo được giao thực hiện tại mỗi thời điểm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public string ReceiveReport(ReceiveReportModel model)
        {
            // Tạo mới DocType & Form nếu chưa có
            // Kiểm tra từng biểu mẫu báo cáo
            foreach (var dataDetail in model.data)
            {
                var docType = new DocType();
                if (CreateDocTypeForm(dataDetail, out docType))
                    CreateDocument(docType, dataDetail.period.ToString());
            }

            // Chuyển đổi dữ liệu sang cấu hình
            return "Thêm báo cáo thành công.";
        }

        [System.Web.Http.HttpPost]
        public string ReceiveReports(ReceiveReportsModel receiveReportsModel)
        {
            var base64EncodedBytes = Convert.FromBase64String(receiveReportsModel.data);
            var rpt_structures = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var reportStructModels = JsonConvert.DeserializeObject<List<ReceiveReportStructModel>>(rpt_structures);
            foreach (var reportStructModel in reportStructModels)
            {
                var receiveReportDataModel = new ReceiveReportDataModel();
                receiveReportDataModel.ReceiveReportStructModel = reportStructModel;
                // Tạo mới DocType & Form nếu chưa có
                // Kiểm tra từng biểu mẫu báo cáo
                // Check DocType có tồn tại bằng rpt_code
                var docType = new DocType();
                CreateDocTypeForm(receiveReportDataModel, out docType);
            }

            return "Thêm báo cáo thành công.";
        }
        [System.Web.Http.HttpPost]
        public string ReportModes(ReportModeData model)
        {
            var base64EncodedBytes = Convert.FromBase64String(model.data);
            var structures = Encoding.UTF8.GetString(base64EncodedBytes);
            var reportStructModels = JsonConvert.DeserializeObject<List<dynamic>>(structures);
            foreach (var reportStructModel in reportStructModels)
            {
                try
                {
                    ReportModeModel models = JsonConvert.DeserializeObject<ReportModeModel>(reportStructModel.header.ToString());
                    var code = _reportModeService.Get(models.Code);
                    if (code == null)
                    {
                        _reportModeService.Create(models.ToEntity());
                    }
                    else
                    {
                        //var id = code.ReportModeId;
                        var report = code;//_reportModeService.Get(id);
                        //report = models.ToEntity(report);
                        report.Name = models.Name;
                        report.Subject = models.Subject;
                        report.IssueDate = models.IssueDate;
                        report.Number = models.Number;
                        report.Notation = models.Notation;
                        report.ReportMode = models.ReportMode;
                        report.IssueOrg = models.IssueOrg;
                        report.RefNotation = models.RefNotation;
                        _reportModeService.Update(report);
                    }
                }
                catch (Exception e)
                {
                    //
                }
                
            }

            return "Thành công.";
        }

        [System.Web.Http.HttpGet]
        public SendReport GetDataReportFromDocumentCopyId(int documentCopyId, int userId)
        {
            var reportData = _documentCopyService.GetDataReportFromDocumentCopyId(documentCopyId, userId);
            return reportData;
        }

        [System.Web.Http.HttpPost]
        public string GetReportStatus(ReceiveStatusReportModel model)
        {
            var docPublishPlus = _docpublishplusService.Gets(false, p => p.DocCode == model.header.Code).ToList(); ;
            if (docPublishPlus != null && docPublishPlus.Count() > 0)
            {
                var updateDocPublishPlus = docPublishPlus.First();
                updateDocPublishPlus.JSonStatus = JsonConvert.SerializeObject(model);
                _docpublishplusService.Update(updateDocPublishPlus);

                return "Update trạng thái của báo cáo phát hành lên chính phủ thành công.";
            }

            return "Báo cáo không tồn tại.";
        }

        #region private

        private bool CreateDocTypeForm(ReceiveReportDataModel dataDetail, out DocType finalDocType)
        {
            DocType docTypeCreate = new DocType();
            finalDocType = new DocType();

            if (dataDetail.ReceiveReportStructModel == null)
            {
                var base64EncodedBytes = Convert.FromBase64String(dataDetail.rpt_structure);
                var rpt_structure = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                dataDetail.ReceiveReportStructModel = JsonConvert.DeserializeObject<ReceiveReportStructModel>(rpt_structure);
                // var dataDetail.Re = JsonConvert.DeserializeObject<ReceiveReportStructModel>(rpt_structure);
            }

            // TBD docTypeCreate.CreatedByUserId = User.GetUserId();
            docTypeCreate.DocTypeCode = dataDetail.ReceiveReportStructModel.header.Code;
            docTypeCreate.DocTypeName = dataDetail.ReceiveReportStructModel.header.Name;
            docTypeCreate.CompendiumDefault = dataDetail.ReceiveReportStructModel.header.Description;
            docTypeCreate.CreatedOnDate = DateTime.Now;
            docTypeCreate.LastModifiedOnDate = DateTime.Now;
            docTypeCreate.DocTypePermission = 0;
            docTypeCreate.TotalRegisted = 1;//eGovOnline
            docTypeCreate.TotalViewed = 1;//eGovOnline
            docTypeCreate.Unsigned = ConverToUnsign(dataDetail.ReceiveReportStructModel.header.Code); //eGovOnline
            docTypeCreate.CategoryBusinessId = 4;
            docTypeCreate.CategoryId = 5; // default = 5/ Báo cáo.
            docTypeCreate.IsAllowOnline = true;
            docTypeCreate.IsActivated = true;
            docTypeCreate.DocTypePermission = 0;

            // lấy từ Admin - Setting - General
            docTypeCreate.DocFieldId = _adminSetting.GovDocFieldId;
            docTypeCreate.LevelId = _adminSetting.GovLevelId;

            int actionLevel;
            var formCreate = CreateFrom(dataDetail, docTypeCreate.DocFieldId, out actionLevel);
            docTypeCreate.ActionLevel = actionLevel;

            Form finalForm;

            try
            {
                // save DocType & Form
                finalDocType = _doctypeService.CreateNReturn(docTypeCreate);
                finalForm = _formService.CreateNReturn(formCreate);

                _doctypeformService.Create(new DocTypeForm
                {
                    DocTypeId = finalDocType.DocTypeId,
                    FormId = finalForm.FormId,
                    IsPrimary = true,
                    IsActive = true
                });
            }
            catch (EgovException ex)
            {
                return false;
            }

            if (_adminSetting.GovWorkFlowId.HasValue)
            {
                _doctypeService.UpdateWorkflows(finalDocType.DocTypeId, new List<int> { (int)_adminSetting.GovWorkFlowId });
            }

            return true;
        }

        private void CreateDocument(DocType docType, string period)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                    var url = baseUrl + "/webapi/Document/SaveDocDraft?docTypeId=" + docType.DocTypeId;

                    var datePublished = string.Empty;
                    string periodFormat;
                    switch (docType.ActionLevel)
                    {
                        case 1: // bao cao nam
                            periodFormat = "yyyy";
                            break;
                        case 3: // TODO: bao cao quy
                            periodFormat = "yyyy";
                            break;
                        case 4: // bao cao thang
                            periodFormat = "yyyyMM";
                            break;
                        case 7: // bao cao dot xuat
                            periodFormat = "yyyyMMdd";
                            break;
                        default:
                            periodFormat = string.Empty;
                            break;
                    }
                    DateTime date;

                    if (DateTime.TryParseExact(period, periodFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        url += "&datePublished=" + date.ToString("s", CultureInfo.InvariantCulture);
                    }
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var results = response.Content.ReadAsStringAsync().Result as string;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string ConverToUnsign(string text)
        {
            text = text.ToUpper();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                text = text.Replace(convert[i], To[i]);
            }
            return text;
        }

        //private List<string> RecursiveField (ReceiveReportStructContentAttributeDataModel item, List<List<string>> dataField, int dataIndex)
        //{
        //    var dataFieldDetail = new List<string>();
        //    dataFieldDetail.Add(item.Name);
        //    if (item.Children != null && item.Children.Count() > 0)
        //    {
        //        foreach (var itemDetail in item.Children)
        //        {
        //            // dataField = RecursiveField(item, dataFieldDetail, dataIndex);
        //        }
        //    }

        //    return dataFieldDetail;
        //}

        private Form CreateFrom(ReceiveReportDataModel dataDetail, int? docFieldId, out int actionLevel)
        {
            actionLevel = 1;
            var form = new Form();
            var structModel = dataDetail.ReceiveReportStructModel;

            form.FormName = structModel.header.Name;
            form.Description = structModel.header.Description;
            form.IsActivated = 1;
            form.FormTypeId = 2;
            form.IsPrimary = true; //model.IsPrimary;
            // TBD form.CreatedByUserId = User.GetUserId();
            var docFieldname = _docFieldService.Get(docFieldId ?? 0).DocFieldName.TrimStart();
            form.FormGroupId = GetFormGroupIDWith(docFieldname);

            if (dataDetail.ReceiveReportStructModel != null)
            {
                form.Description = structModel.header.Description;
                form.FormName = structModel.header.Name;
                var actionLevelDimension = GetActionLevelDimession();
                for (int i = 0; i < 4; i++)
                {
                    if (actionLevelDimension[0, i].Equals(structModel.header.Type))
                        actionLevel = actionLevelDimension[1, i];
                }

                DefineField defineField;
                Dictionary<string, ReceiveReportStructContentAttributeIndicatorDetailModel> indicatorAttrDic;
                List<List<ReceiveReportStructContentAttributeDataModel>> attrRows;

                var defineFieldJson = BuildDefineFieldJson(structModel, out defineField, out  indicatorAttrDic, out attrRows);

                var defineConfigJson = BuildDefineConfigJson(defineField, indicatorAttrDic, attrRows);

                var defineValueJson = BuildDefineValueJson(structModel, defineField, indicatorAttrDic);

                form.DefineFieldJson = defineFieldJson;
                form.DefineConfigJson = defineConfigJson;
                form.DefineValueJson = defineValueJson;

                //// 1.generate header
                //// 2.generate data
                //// 3.generate data.headerNested
                if (!string.IsNullOrEmpty(form.DefineFieldJson) && !string.IsNullOrEmpty(form.DefineConfigJson)
                    && !string.IsNullOrEmpty(form.DefineValueJson))
                {
                    Dictionary<string, string> header = null;

                    dynamic defineFieldObject = JsonConvert.DeserializeObject(form.DefineFieldJson);
                    dynamic defineConfigObject = JsonConvert.DeserializeObject(form.DefineConfigJson);

                    dynamic defineValueObject = JsonConvert.DeserializeObject(form.DefineValueJson);

                    var handsonToJson = new HandsonToJson(defineFieldObject, defineConfigObject, defineValueObject);

                    // 20191225 VuHQ Cù Trọng Xoay
                    var xoayHeader = new Dictionary<string, string>();
                    header = handsonToJson.ConvertHeaderHandsonToJson(out xoayHeader);

                    // 20191225 VuHQ Cù Trọng Xoay
                    Dictionary<string, HeaderObject> xoayColumnSetting = new Dictionary<string, HeaderObject>();
                    var columnSetting = handsonToJson.ConvertHeaderHandsonToJsonExtra(out xoayColumnSetting, true);

                    // generate data
                    var data = handsonToJson.ConvertHandsonToJson(header, false);
                    var headerNested = defineFieldObject.mergedCells;

                    // 20191225 VuHQ Cù Trọng Xoay
                    var extra = JsonConvert.SerializeObject(new
                    {
                        columnSetting = columnSetting,
                        headerSetting = defineFieldObject.data,
                        mergedCells = defineValueObject.mergedCells,
                        hiddenColumns = defineConfigObject.hiddenColumns,
                        autoSizeColumns = defineConfigObject.autoSizeColumns,
                        xoayObject = handsonToJson.XoayObject
                    });
                    form.FormCode = JsonConvert.SerializeObject(new
                    {
                        header = header,
                        data = data,
                        headerNested = headerNested,
                        extra = JsonConvert.DeserializeObject(extra),
                        colWidths = defineFieldObject.colWidths,
                        mergedCells = defineValueObject.mergedCells,
                        readOnlys = defineValueObject.readOnlys,
                        classCells = defineValueObject.classCells
                    });

                    // tự động generate luôn Json (sử dụng để hiển thị form ở mobile)
                    form.Json = JsonConvert.SerializeObject(columnSetting);
                }
            }

            return form;
        }

        /// <summary>
        /// Type của báo cáo chính phủ: 
        /// 1: Đột xuất
        /// 2: Tháng
        /// 3: Quý
        /// 4: Năm
        /// Type của eform:
        /// 7: Đột xuất
        /// 4: Tháng
        /// 3: Quý
        /// 1: Năm
        /// </summary>
        /// <returns></returns>
        private int[,] GetActionLevelDimession()
        {
            return new int[,] { { 1, 2, 3, 4},
                { 7, 4, 3, 1 }
            };
        }

        private int GetFormGroupIDWith(string docFieldName)
        {
            var datas = _formGroupService.GetsAs(p => new
            {
                p.FormGroupId
            }, p => p.FormGroupName == docFieldName);

            if (datas == null || datas.Count() == 0)
            {
                var formGroup = new FormGroup() { FormGroupName = docFieldName };
                _formGroupService.Create(formGroup);
                return formGroup.FormGroupId;
            }
            return datas.ElementAt(0).FormGroupId;
        }

        private string BuildDefineFieldJson(
            ReceiveReportStructModel structModel,
            out DefineField defineField,
            out Dictionary<string, ReceiveReportStructContentAttributeIndicatorDetailModel> indicatorAttrDic,
            out List<List<ReceiveReportStructContentAttributeDataModel>> attrRows)
        {
            attrRows = new List<List<ReceiveReportStructContentAttributeDataModel>>();
            var attrRow = structModel.Content.Attribute.DataAttr.DeepClone();

            // Flat DataAttr tree
            while (attrRow.Any(item => item != null))
            {
                attrRows.Add(attrRow);
                attrRow = attrRow.SelectMany(item => item?.Children ?? new List<ReceiveReportStructContentAttributeDataModel> { null }).ToList();
            }

            // Add IndicatorAttr
            indicatorAttrDic = new Dictionary<string, ReceiveReportStructContentAttributeIndicatorDetailModel>();

            // Với trường hợp thông tin chỉ tiêu null thì sẽ set name và width cho nó
            if (structModel.Content.Attribute.IndicatorAttr.Index != null && !structModel.Content.Attribute.IndicatorAttr.Index.Enable.Equals("0"))
            {
                if (structModel.Content.Attribute.IndicatorAttr.Index.Width.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Index.Width = "50";
                if (structModel.Content.Attribute.IndicatorAttr.Index.Name.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Index.Name = "STT";

                indicatorAttrDic.Add("Index", structModel.Content.Attribute.IndicatorAttr.Index);
            }
                
            if (structModel.Content.Attribute.IndicatorAttr.Code != null && !structModel.Content.Attribute.IndicatorAttr.Code.Enable.Equals("0"))
            {
                if (structModel.Content.Attribute.IndicatorAttr.Code.Width.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Code.Width = "150";
                if (structModel.Content.Attribute.IndicatorAttr.Code.Name.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Code.Name = "Mã chỉ tiêu";

                indicatorAttrDic.Add("Code", structModel.Content.Attribute.IndicatorAttr.Code);
            }
                
            if (structModel.Content.Attribute.IndicatorAttr.Name != null && !structModel.Content.Attribute.IndicatorAttr.Name.Enable.Equals("0"))
            {
                if (structModel.Content.Attribute.IndicatorAttr.Name.Width.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Name.Width = "250";
                if (structModel.Content.Attribute.IndicatorAttr.Name.Name.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Name.Name = "Tên chỉ tiêu";

                indicatorAttrDic.Add("Name", structModel.Content.Attribute.IndicatorAttr.Name);
            }
                
            if (structModel.Content.Attribute.IndicatorAttr.Unit != null && !structModel.Content.Attribute.IndicatorAttr.Unit.Enable.Equals("0"))
            {
                if (structModel.Content.Attribute.IndicatorAttr.Unit.Width.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Unit.Width = "100";
                if (structModel.Content.Attribute.IndicatorAttr.Unit.Name.Equals("null"))
                    structModel.Content.Attribute.IndicatorAttr.Unit.Name = "DVT";

                indicatorAttrDic.Add("Unit", structModel.Content.Attribute.IndicatorAttr.Unit);
            }

            attrRows[0].InsertRange(0, indicatorAttrDic.Select(attr =>
                                    new ReceiveReportStructContentAttributeDataModel
                                    {
                                        Name = attr.Value.Name == "" ? " " : attr.Value.Name,
                                        Width = attr.Value.Width,
                                        Type = 3
                                    }));
            for (var i = 1; i < attrRows.Count; i++) attrRows[i].InsertRange(0, new ReceiveReportStructContentAttributeDataModel[indicatorAttrDic.Count]);

            for (var i = 0; i < attrRows.Count - 1; i++)
            {
                attrRow = attrRows[i];
                for (var j = 0; j < attrRow.Count; j++)
                {
                    var item = attrRow[j];
                    if (item?.Children != null)
                    {
                        for (var index = 0; index <= i; index++)
                        {
                            attrRows[index].InsertRange(j + 1, new ReceiveReportStructContentAttributeDataModel[item.Children.Count - 1]);
                        }
                    }
                }
            }

            defineField = new DefineField();
            defineField.data = attrRows.Select(row => row.Select(item => item?.Name ?? "").ToList()).ToList();
            defineField.countCols = attrRows[0].Count;

            // Config colWidths
            for (var j = 0; j < defineField.countCols; j++)
            {
                var item = attrRows[attrRows.Count - 1][j];
                if (item != null) defineField.colWidths.Add(item.Width);
                else
                {
                    var i = attrRows.Count - 2;
                    while (attrRows[i][j] == null) i--;
                    defineField.colWidths.Add(attrRows[i][j].Width);
                }
            }

            // Config mergedCells
            for (var i = 0; i < defineField.data.Count - 1; i++)
            {
                var row = defineField.data[i];
                for (var j = 0; j < row.Count; j++)
                {
                    var item = row[j];
                    if (item == "") continue;
                    var mergedCell = new MergedCell(i, j);
                    if (defineField.data[i + 1][j] == "") mergedCell.rowspan = attrRows.Count - i;
                    while (j + 1 < row.Count && row[j + 1] == "")
                    {
                        mergedCell.colspan++;
                        j++;
                    }
                    if (mergedCell.rowspan + mergedCell.colspan > 2) defineField.mergedCells.Add(mergedCell);
                }
            }

            return defineField.Stringify();
        }

        private string BuildDefineConfigJson(
            DefineField defineField,
            Dictionary<string, ReceiveReportStructContentAttributeIndicatorDetailModel> indicatorAttrDic,
            List<List<ReceiveReportStructContentAttributeDataModel>> attrRows)
        {
            var defineConfig = new DefineConfig();
            for (var j = 0; j < defineField.data[0].Count; j++)
            {
                ReceiveReportStructContentAttributeDataModel item = null;
                var itemConfig = new List<object>();
                for (var i = 0; i < defineField.data.Count; i++)
                {
                    var name = defineField.data[i][j];
                    if (name == "") name = null;
                    itemConfig.Add(name);
                    item = item ?? attrRows[i][j];
                }

                var typeStr = string.Empty;
                object defaultValue = 0;
                object typeStrDetail = null;
                if (item.Type == 1) typeStr = "Số nguyên";
                else if (item.Type == 2) typeStr = "Số thực";
                else
                {
                    typeStr = "Kí tự";
                    typeStrDetail = "Kí tự ngắn";
                    defaultValue = string.Empty;
                }

                itemConfig.Add(typeStr);
                itemConfig.Add(typeStrDetail);
                itemConfig.Add(defaultValue);
                itemConfig.Add(true);
                itemConfig.AddRange(new List<object>[9]);

                // Nếu Enable = 0 hoặc là mã chỉ tiêu sẽ ẩn cột desktop & mobile
                if (j < indicatorAttrDic.Count)
                {
                    if (indicatorAttrDic.ElementAt(j).Value.Enable.Equals("0") || j == 1) 
                        itemConfig[attrRows.Count + 7] = itemConfig[attrRows.Count + 10] = true;
                    if (j <= 3)
                        itemConfig[attrRows.Count + 4] = true;
                    if (j == 0 || j == 3)
                        itemConfig[attrRows.Count + 3] = false;
                }

                defineConfig.data.Add(itemConfig);
                
            }

            // tính toán columns
            for (var j = 0; j < defineField.data.Count; j++)
            {
                defineConfig.columns.Add(new { readOnly = true });
            }

            // kiểu dữ liệu
            defineConfig.columns.Add(new object());

            // chi tiết
            defineConfig.columns.Add(new { allowInvalid = true });

            // giá trị mặc định
            defineConfig.columns.Add(new { allowInvalid = true });

            //bat buoc/chi doc, phạm vi
            defineConfig.columns.Add(new object());
            defineConfig.columns.Add(new object());
            defineConfig.columns.Add(new { className = "inputMasked" });

            // Ghi đè
            defineConfig.columns.Add(new object());

            // Ẩn cột
            defineConfig.columns.Add(new object());

            // Xoay
            defineConfig.columns.Add(new object());

            // Giá trị
            defineConfig.columns.Add(new object());

            // Ẩn cột mobile
            defineConfig.columns.Add(new object());

            // Inline
            defineConfig.columns.Add(new object());

            // Autosize
            defineConfig.columns.Add(new object());

            return defineConfig.Stringify();
        }

        private string BuildDefineValueJson(
            ReceiveReportStructModel structModel,
            DefineField defineField,
            Dictionary<string, ReceiveReportStructContentAttributeIndicatorDetailModel> indicatorAttrDic)
        {
            var defineValue = new DefineValue();
            var formulaRows = new List<FormulaRow>();
            defineValue.columns.AddRange(Enumerable.Repeat(new { allowInvalid = true }, defineField.countCols));
            var indicatorRows = structModel.Content.Indicator.DeepClone();
            defineValue.data = GetIndicatorData(indicatorAttrDic, indicatorRows, 1, ref defineValue.classCells, ref formulaRows);
            defineValue.data.ForEach(row => row.AddRange(new object[defineField.countCols - row.Count]));
            defineValue.classCells.ForEach(row => row.AddRange(Enumerable.Repeat("htCenter", defineField.countCols - row.Count)));

            // VuHQ START Config formulaCells
            var row_count = GetRowCount(structModel.Content.Attribute.DataAttr);
            var col_count = GetColCount(structModel.Content.Attribute.DataAttr);
            col_count += indicatorAttrDic.Count();

            foreach (var formulaRow in formulaRows)
            {
                for (var colIndex = indicatorAttrDic.Count(); colIndex < defineValue.data.ElementAt(formulaRow.row).Count(); colIndex++)
                {
                    defineValue.data.ElementAt(formulaRow.row)[colIndex] = GetFormulaByType(formulaRow.type, formulaRow.row + 2, colIndex + 1, formulaRow.length);
                }
            }
            // VuHQ END Config formulaCellss

            return defineValue.Stringify();
        }

        private List<List<object>> GetIndicatorData(
            Dictionary<string, ReceiveReportStructContentAttributeIndicatorDetailModel> indicatorAttr,
            List<ReceiveReportStructContentIndicatorModel> indicatorModels,
            int level,
            ref List<List<string>> classCells,
            ref List<FormulaRow> formulas)
        {
            var result = new List<List<object>>();
            foreach (var indicator in indicatorModels)
            {
                // Không lấy chỉ tiêu đầu tiên (nó chỉ là header), chỉ lấy các children
                if (!indicator.Code.Equals("-"))
                {
                    var rowData = new List<object>();
                    var classCell = new List<string>();

                    foreach (var attr in indicatorAttr)
                    {
                        var indicatorValue = indicator.GetType().GetProperty(attr.Key).GetValue(indicator, null);
                        var @class = "htCenter";
                        if (attr.Key == "Name")
                        {
                            switch (level)
                            {
                                case 1:
                                    @class += " htBold";
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    indicatorValue = $"  (-) {indicatorValue}";
                                    break;
                                case 4:
                                    indicatorValue = $"    (+) {indicatorValue}";
                                    break;
                                default:
                                    indicatorValue = $"{new string(' ', (level - 2) * 2)}{indicatorValue}";
                                    break;
                            }
                        }
                        rowData.Add(indicatorValue);
                        classCell.Add(@class);
                    }
                    result.Add(rowData);
                    classCells.Add(classCell);
                }

                if (indicator.children != null)
                {
                    var tempData = GetIndicatorData(indicatorAttr, indicator.children, level + 1, ref classCells, ref formulas);
                    result.AddRange(tempData);
                    if (indicator.Type != FORMULA_TYPE_NORMAL && indicator.Type != FORMULA_TYPE_READONLY)
                    {
                        var formulaRow = new FormulaRow(level - 1, indicator.Type, tempData.Count() - 1);
                        formulas.Add(formulaRow);
                    }
                }
                    
            }

            return result;
        }

        private int GetColCount(List<ReceiveReportStructContentAttributeDataModel> dataAttrs)
        {
            var col_count = 0;
            foreach (var dataAttr in dataAttrs)
            {
                if (dataAttr.Children != null)
                    col_count += +GetColCount(dataAttr.Children);
                else
                    col_count++;
            }

            return col_count;
        }

        private int GetRowCount(List<ReceiveReportStructContentAttributeDataModel> dataAttrs)
        {
            var row_count = 1;
            var tmp_count = 0;
            foreach (var dataAttr in dataAttrs)
            {
                if (dataAttr.Children != null)
                {
                    tmp_count = 1 + GetRowCount(dataAttr.Children);
                    if (tmp_count > row_count)
                        row_count = tmp_count;
                }
            }

            return row_count;
        }

        private int GetColSpan(ReceiveReportStructContentAttributeDataModel dataAttr)
        {
            var colSpan = 1;
            if (dataAttr.Children != null)
            {
                colSpan = 0;
                foreach (var children in dataAttr.Children)
                {
                    colSpan += GetColSpan(children);
                }
            }

            return colSpan;
        }

        private Dictionary<int, string> GetAlphabetNumbers()
        {
            var result = new Dictionary<int, string>();
            var i = 1;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                result.Add(i, c.ToString());
                i++;
            }

            return result;
        }

        private string GetFormulaByType(int type, int crow, int ccol, int length)
        {
            var strFormula = new StringBuilder();
            var alphabetNumbers = GetAlphabetNumbers();
            switch (type)
            {
                case FORMULA_TYPE_READONLY:
                    break;
                case FORMULA_TYPE_SUM:
                    strFormula.AppendFormat("=SUM({0}{1}:{2}{3})",
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow,
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow  + length
                    );
                    break;
                case FORMULA_TYPE_AVG:
                    strFormula.AppendFormat("=AVG({0}{1}:{2}{3})",
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow,
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow + length
                    );
                    break;
                case FORMULA_TYPE_MAX:
                    strFormula.AppendFormat("=MAX({0}{1}:{2}{3})",
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow,
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow + length
                    );
                    break;
                case FORMULA_TYPE_MIN:
                    strFormula.AppendFormat("=MIN({0}{1}:{2}{3})",
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow,
                        alphabetNumbers.Where(p => p.Key == ccol).Select(p => p.Value).FirstOrDefault(), crow + length
                    );
                    break;
                case FORMULA_TYPE_NORMAL:
                default:
                    strFormula.Append(string.Empty);
                    break;
            }

            return strFormula.ToString();
        }

        #endregion
    }
}