using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using System.IO;
using Bkav.eGovCloud.Core.FileSystem;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : CodeBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 301012</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Cung cấp các API quản lý các mẫu form, template, mẫu phôi cho loại hồ sơ.</para>
    /// </summary>
    public class FormBll : ServiceBase
    {
        #region Readonly & Static Fields

        private readonly CatalogBll _catalogService;
        private readonly ExtendFieldBll _extendFieldService;
        private readonly IRepository<Form> _formRepository;
        private readonly IRepository<FormType> _formTypeRepository;
        private readonly ResourceBll _resourceService;
        private readonly DocTypeFormBll _doctypeformService;
        private readonly AdminGeneralSettings _generalSettings;

        private readonly FileManager _fileManager;

        #endregion

        #region C'tors

        /// <summary>
        /// Khởi tạo <see cref="FormBll"/>.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="resourceService">Resource service. </param>
        /// <param name="extendFieldService">Exfield Bll tương tứng. </param>
        /// <param name="catalogService">Dal danh mục. </param>
        /// <param name="generalSettings">Cấu hình chung. </param>
        /// <param name="doctypeformService">Bll tương ứng với bảng doctype_form. </param>
        public FormBll(IDbCustomerContext context,
                       ResourceBll resourceService,
                       ExtendFieldBll extendFieldService,
                       CatalogBll catalogService,
                       AdminGeneralSettings generalSettings,
                       DocTypeFormBll doctypeformService)
            : base(context)
        {
            _formTypeRepository = Context.GetRepository<FormType>();
            _formRepository = Context.GetRepository<Form>();

            _resourceService = resourceService;
            _extendFieldService = extendFieldService;
            _catalogService = catalogService;
            _generalSettings = generalSettings;
            _doctypeformService = doctypeformService;

            _fileManager = FileManager.Default;
        }

        #endregion

        #region Instance Methods

        /// <summary> Tienbv 081112
        /// Copy biểu mẫu động từ biểu mẫu khác.
        /// </summary>
        /// <param name="fId">The target form id.</param>
        /// <param name="name">The new form name.</param>
        /// <param name="des">The new form des.</param>
        /// <param name="groupid">The target doctype id.</param>
        public bool Copy(Guid fId, string name, string des, int groupid)
        {
            var targetForm = Get(fId);
            if (targetForm == null)
            {
                return false;
            }
            var newId = Guid.NewGuid();
            var newForm = new Form
                              {
                                  FormId = newId,
                                  FormGroupId = groupid,
                                  FormName = name,
                                  Description = des,
                                  IsActivated = 3,
                                  // Mặc định form copy là lưu tạm. Cho phép có nhiều form lưu tạm khi copy.
                                  IsPrimary = targetForm.IsPrimary,
                                  Json = targetForm.Json,
                                  Template = targetForm.Template,
                                  FormTypeId = targetForm.FormTypeId,
                                  EmbryonicPath = targetForm.EmbryonicPath,
                                  CreatedOnDate = targetForm.CreatedOnDate
                              };

            _formRepository.Create(newForm);
            Context.SaveChanges();
            return true;
        }

        /// <summary> TienBV 301012
        /// Tạo một mẫu mới cho loại hồ sơ (chưa bao gồm nội dung html hoặc form động).
        /// <para>Nếu mẫu form được tạo có trạng thái active và là form chính thì sẽ tự động unactive tất cả form chính khác.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException">Form null.</exception>
        /// <param name="form">the form obj.</param>
        public void Create(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            form.FormId = Guid.NewGuid();
            _formRepository.Create(form);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo một mẫu mới cho loại hồ sơ (chưa bao gồm nội dung html hoặc form động).
        /// <para>Nếu mẫu form được tạo có trạng thái active và là form chính thì sẽ tự động unactive tất cả form chính khác.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException">Form null.</exception>
        /// <param name="forms">the form obj.</param>
        public void Create(IEnumerable<Form> forms)
        {
            if (forms == null || !forms.Any())
            {
                throw new ArgumentNullException("form");
            }

            foreach (var form in forms)
            {
                if (!Exist(x => x.FormName.Equals(form.FormName, StringComparison.OrdinalIgnoreCase)
                    && x.FormType == form.FormType
                    && x.FormGroupId == form.FormGroupId))
                {
                    form.FormId = Guid.NewGuid();
                    _formRepository.Create(form);
                }
            }

            Context.SaveChanges();
        }

        /// <summary> TienBV 301012
        /// Tạo một mẫu mới cho loại hồ sơ (chưa bao gồm nội dung html hoặc form động).
        /// và trả về đối tượng sau khi đã được tạo
        /// <para>Nếu mẫu form được tạo có trạng thái active và là form chính thì sẽ tự động unactive tất cả form chính khác.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException">Form null.</exception>
        /// <param name="form">the form obj.</param>
        public Form CreateNReturn(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            form.FormId = Guid.NewGuid();
            _formRepository.Create(form);
            Context.SaveChanges();
            return form;
        }

        /// <summary>
        /// Tạo một mẫu mới cho loại hồ sơ (chưa bao gồm nội dung html hoặc form động).
        /// và trả về đối tượng sau khi đã được tạo
        /// <para>Nếu mẫu form được tạo có trạng thái active và là form chính thì sẽ tự động unactive tất cả form chính khác.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException">Form null.</exception>
        /// <param name="forms">the form obj.</param>
        public List<Form> CreateNReturn(IEnumerable<Form> forms)
        {
            if (forms == null || !forms.Any())
            {
                throw new ArgumentNullException("form");
            }

            var finalForms = new List<Form>();

            foreach (var form in forms)
            {
                if (!Exist(x => x.FormName.Equals(form.FormName, StringComparison.OrdinalIgnoreCase)
                    && x.FormType == form.FormType
                    && x.FormGroupId == form.FormGroupId))
                {
                    form.FormId = Guid.NewGuid();
                    _formRepository.Create(form);
                    finalForms.Add(form);
                }
            }

            Context.SaveChanges();
            return finalForms;
        }

        /// <summary> TienBV 301012
        /// Xóa một mẫu form.
        /// </summary>
        /// <exception cref="EgovException">Mẫu đã được sử dụng. Bạn không được xóa.</exception>
        /// <param name="form">The form obj.</param>
        public void Detele(Form form)
        {
            if (_doctypeformService.Exist(d => d.FormId == form.FormId))
            {
                throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Form.Delete.Exception.DocTypeForm"));
            }

            _formRepository.Delete(form);
            Context.SaveChanges();
        }

        /// <summary> Tienbv 151112
        /// Lấy danh sách các giá trị của catalog trong form
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        public List<Catalog> GetCatalogs(Guid formId)
        {
            var form = Get(formId);
            return GetCatalogs(form);
        }

        /// <summary> Tienbv 151112
        /// Lấy danh sách các giá trị của catalog trong form
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public List<Catalog> GetCatalogs(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            var json = GetJson(form);
            var catalogIds = DynamicFormHelper.GetCatalogIds(json);
            var catalogs = _catalogService.Gets(c => catalogIds.Contains(c.CatalogId)).ToList();
            return catalogs;
        }

        /// <summary>
        /// Trả về danh sách các exfield trong form.
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public List<ExtendField> GetExfields(Guid formId)
        {
            var json = GetJson(formId);
            var exfieldIds = DynamicFormHelper.GetExtendFieldIds(json);
            var exfields = _extendFieldService.Gets(e => exfieldIds.Contains(e.ExtendFieldId)).ToList();
            return exfields;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formControls"></param>
        /// <returns></returns>
        public List<Catalog> GetCatalogsInForm(List<JsControl> formControls)
        {
            IEnumerable<Guid> catalogIds = DynamicFormHelper.GetCatalogIds(formControls);
            var catalogs = _catalogService.Gets(c => catalogIds.Contains(c.CatalogId)).ToList();
            return catalogs;
        }

        /// <summary> TienBV 081112
        /// Lấy chuỗi json của form: đã đồng bộ với cấu hình mới nhất.
        /// </summary>
        /// <param name="formId">The form guid id.</param>
        /// <returns>
        /// <para>
        ///     - Success: return chuỗi json đã được đồng bộ với cấu hình mới nhất của form động.
        /// </para>
        /// <para>
        ///     - UnSuccess: return string.Empty và set Json của form về null.
        /// </para>
        /// </returns>
        public string GetJson(Guid formId)
        {
            var result = string.Empty;
            var form = Get(formId);
            if (form == null)
            {
                return result;
            }
            result = GetJson(form);
            return result;
        }

        /// <summary> TienBV 081112
        /// Lấy chuỗi json của form: đã đồng bộ với cấu hình mới nhất.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        /// <para>
        ///     - Success: return chuỗi json đã được đồng bộ với cấu hình mới nhất của form động.
        /// </para>
        /// <para>
        ///     - UnSuccess: return string.Empty và set Json của form về null.
        /// </para>
        /// </returns>
        public string GetJson(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            var result = string.Empty;
            var json = form.Json;
            List<JsControl> controls;
            if (DynamicFormHelper.TryParse(json, out controls))
            {
                controls = controls.ToNewestJsControl();
                result = controls.ToJsonString();
            }
            else
            {
                form.Json = null;
                Context.SaveChanges();
            }
            return result;
        }

        /// <summary> TienBV 301012
        /// Lấy tất cả các loại form: các loại form là cố định được định nghĩa trong bảng formtype.
        /// </summary>
        /// <returns>All form type.</returns>
        public IEnumerable<FormType> GetTypes()
        {
            return _formTypeRepository.GetsReadOnly();
        }

        /// <summary> Tienbv 081112
        /// Lấy ra danh sách các mẫu form theo điều kiện kỹ thuật truyền vào. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="isReadOnly"></param>
        public IEnumerable<Form> Gets(Expression<Func<Form, bool>> spec = null, bool isReadOnly = true)
        {
            return _formRepository.Gets(isReadOnly, spec);
        }

        /// <summary> Tienbv 081112
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào không
        /// </summary>
        /// <param name="spec">The spec.</param>
        public bool Exist(Expression<Func<Form, bool>> spec)
        {
            return _formRepository.Exist(spec);
        }

        /// <summary> Tienbv 081112
        /// Lấy ra danh sách các mẫu form theo điều kiện kỹ thuật truyền vào. Kết quả sẽ được ánh xạ sang một dạng khác do người dùng cung cấp
        /// </summary>
        /// <param name="projector">projector</param>
        /// <param name="spec">The spec.</param>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Form, T>> projector, Expression<Func<Form, bool>> spec = null)
        {
            return _formRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra tất cả biểu mẫu. Kết quả chỉ đọc
        /// </summary>
        /// <param name="totalRecords"> Tổng số bản ghi </param>
        /// <param name="projector"></param>
        /// <param name="currentPage"> Trang hiện tại </param>
        /// <param name="pageSize"> Số bản ghi trên 1 trang </param>
        /// <param name="sortBy"> Sắp xếp theo </param>
        /// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
        /// <param name="formGroupId"> Id nhóm biểu mẫu </param>
        /// <param name="formTypeId"> Id loại mẫu </param>
        /// <param name="docTypeId">Loại hồ sơ đang sử dụng</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns> Danh sách nhóm biểu mẫu </returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
                                        Expression<Func<Form, T>> projector,
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = null,
                                        bool isDescending = false,
                                        int? formGroupId = null,
                                        int? formTypeId = null,
                                        Guid? docTypeId = null,
                                        string search = "")
        {
            var spec = FormQuery.WithFormGroupId(formGroupId).And(FormQuery.WithFormTypeId(formTypeId));
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            if (!string.IsNullOrEmpty(search))
            {
                spec = spec.And(f => f.FormName.ToLower().Contains(search.ToLower()));
            }

            if (docTypeId != null)
            {
                spec = spec.And(f => f.DocTypeForms.Any(df => df.DocTypeId.Equals(docTypeId.Value)));
            }

            totalRecords = _formRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Form>(isDescending, sortBy);
            return _formRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Form>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Trả về tất cả form động
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Form> GetAllDynamic(bool isActive = true)
        {
            return _formRepository.Gets(false, f => f.FormTypeId == (int)Entities.FormType.DynamicForm && (!isActive || (isActive && f.IsActivated == 1)));
        }

        /// <summary>
        /// Trả về danh sách các form động đang dc active theo loại hồ sơ.
        /// <para> (Tienbv@bkav.com 150313)</para>
        /// </summary>
        /// <param name="doctypeId">doctypeId</param>
        /// <returns></returns>
        public IEnumerable<Form> GetDynamics(Guid doctypeId)
        {

            var result = _doctypeformService.GetsAs(df => df.Form, 
                                df => df.DocTypeId == doctypeId && df.Form.IsActivated == 1 && df.Form.FormTypeId == (int)Entities.FormType.DynamicForm);

            return result.OrderBy(f => f.IsPrimary);
        }

        /// <summary> Tienbv 301012
        /// Lấy ra một mẫu form tương ứng theo id.
        /// </summary>
        /// <param name="id">The form id.</param>
        public Form Get(Guid id)
        {
            return _formRepository.Get(id);
        }

        /// <summary> Tienbv 301012
        /// Cập nhật thông tin mẫu form (không bao gồm nội dung html hoặc form động).
        /// </summary>
        /// <exception cref="ArgumentNullException">Form null.</exception>
        /// <param name="form">The form obj.</param>
        public void Update(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            Context.SaveChanges();
        }

        /// <summary> Tienbv 311012
        /// Cập nhật nội dung form động cho mẫu.
        /// </summary>
        /// <remarks>
        /// <para> Trước khi cập nhật chuỗi json vào db, tiến hành duyệt trên tất cả các control có trong chuỗi json:</para>
        /// <para> - Nếu là catalog (c.TypeId = 10) thì cập nhật vào danh sách các catalog được sử dụng cho loại hồ sơ trong bảng doctype_form_catalog.</para>
        /// <para> - Nếu là extendfield (c.TypeId = 9) Add các exfield chưa có trong cơ sở dữ liệu và cập nhật lại id, sau đó cập nhật vào danh sách các exfield
        ///          được sử dụng cho loại hồ sơ vào bảng doctype_form_extendfield </para>
        /// </remarks>
        /// <exception cref="ArgumentException">Resource(Doctype.Form.Exception.Json.Empty)</exception>
        /// <param name="formid">The form id.</param>
        /// <param name="json">The json.</param>
        public bool UpdateForm(Guid formid, string json)
        {
            if (json == string.Empty && IsNoControlInForm(json))
            {
                return false;
            }
            var form = Get(formid);
            if (form == null)
            {
                return false;
            }
            List<JsControl> controls;
            if (DynamicFormHelper.TryParse(json, out controls))
            {
                if (form.IsActivated == 3)
                {
                    // Form ở trạng thái lưu tạm cần update các control của nó vào db.
                    controls = UpdateControls(formid, controls);
                }
                else
                {
                    List<JsControl> oldControls;
                    // Nếu form đã được sử dụng kt có trùng với bản lưu trên db ko => nếu trùng mới cho lưu.
                    if (DynamicFormHelper.TryParse(form.Json, out oldControls)
                        && !controls.CompareTo(oldControls))
                    {
                        return false;
                    }
                }
                // Sắp xếp lại để client có thể bind đúng thứ tự.
                controls = controls.OrderBy(c => c.PosRow).ThenBy(c => c.PosOrder).ToList();
                controls = controls.ToNewestJsControl();
                form.Json = controls.ToJsonString();
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra có cấu hình exfield và catalog control trong hay ko?
        /// </summary>
        /// <param name="json">The form json.</param>
        /// <returns>True: nếu không có control catalog hay exfield nào.</returns>
        private static bool IsNoControlInForm(string json)
        {
            IEnumerable<Guid> extendfieldIds = DynamicFormHelper.GetExtendFieldIds(json);
            IEnumerable<Guid> catalogIds = DynamicFormHelper.GetExtendFieldIds(json);
            return extendfieldIds.Count() + catalogIds.Count() == 0;
        }

        /// <summary> TienBV 311012
        /// Update các control được cấu hình trong form vào db.
        /// <para> - Nếu là catalog (c.TypeId = 10) thì cập nhật vào danh sách các catalog được sử dụng cho loại hồ sơ trong bảng doctype_form_catalog.</para>
        /// <para> - Nếu là extendfield (c.TypeId = 9) Add các exfield chưa có trong cơ sở dữ liệu và cập nhật lại id, sau đó cập nhật vào danh sách các exfield
        ///          được sử dụng cho loại hồ sơ vào bảng doctype_form_extendfield </para>
        /// </summary>
        /// <param name="formid">The form id.</param>
        /// <param name="controls">The list control in form.</param>
        /// <returns>Danh sách các control đã được cập nhật vào csdl.</returns>
        private List<JsControl> UpdateControls(Guid formid, List<JsControl> controls)
        {
            var result = new List<JsControl>();
            List<JsControl> exfields = controls
                .Where(c => c.TypeId.Equals((int)ControlType.Textbox))
                .Select(c => c).ToList();
            List<JsControl> oldExfields = exfields
                .Where(c => c.ControlId != new Guid())
                .Select(c => c).ToList();
            List<JsControl> newExfields = exfields
                .Where(c => c.ControlId == new Guid())
                .Select(c => c).ToList();

            foreach (JsControl exfield in newExfields)
            {
                Guid newId = Guid.NewGuid();
                var newItm = new ExtendField
                                 {
                                     ExtendFieldId = newId,
                                     ExtendFieldName = exfield.ControlName,
                                     Mask = exfield.MaskType
                                 };
                _extendFieldService.Create(newItm);
                exfield.ControlId = newId;
            }

            Context.Configuration.AutoDetectChangesEnabled = false;
            IEnumerable<Guid> exfieldIds = oldExfields.Concat(newExfields).Select(c => c.ControlId);

            List<JsControl> catalogs = controls.Where(
                c => c.TypeId.Equals((int)ControlType.DropDownList) ||
                     c.TypeId.Equals((int)ControlType.CheckBoxList)).
                Select(c => c).ToList();
            IEnumerable<Guid> catalogIds = catalogs.Select(c => c.ControlId);
            Context.Configuration.AutoDetectChangesEnabled = true;

            List<JsControl> labels = controls
                .Where(c => c.TypeId.Equals((int)ControlType.Label))
                .Select(c => c).ToList();
            result.AddRange(catalogs);
            result.AddRange(newExfields);
            result.AddRange(oldExfields);
            result.AddRange(labels);
            return result.ToList();
        }
        
        #endregion

        /// <summary>
        /// Convert form từ eGate cũ:
        /// Control trên eGate cũ có thuộc tính ControlId(15) là int còn trên eGov là Guid
        /// </summary>
        public void ConvertFromEgateForm()
        {
            var forms = GetAllDynamic(false);
            foreach (var form in forms)
            {
                try
                {

                    var formJson = form.Json;
                    List<JsControl> controls;
                    if (DynamicFormHelper.TryParse(formJson, out controls))
                    {
                        foreach (var control in controls)
                        {
                            if (control.TypeId == 9 || control.TypeId == 10)
                            {
                                control.ControlId = control.GlobalCode;
                            }
                        }

                        form.Json = controls.StringifyJs();
                    }

                }
                catch
                {
                    continue;
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Tải tệp mẫu phôi
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="id">Form id</param>
        /// <returns></returns>
        public Stream Download(out string fileName, Guid id)
        {
            var form = Get(id);
            if (form == null)
            {
                throw new EgovException("Không tìm thấy báo cáo");
            }
            return Download(out fileName, form);
        }


        /// <summary>
        /// Tải tệp mẫu phôi
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="form">form</param>
        /// <returns></returns>
        public Stream Download(out string fileName, Form form)
        {
            fileName = form.EmbryonicPath;
            var downloaded = _fileManager.Open(form.EmbryonicLocationName, ResourceLocation.Default.EmbryonicForm);

            return downloaded;
        }


        public IEnumerable<IDictionary<string, object>> GetDataForm(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }
            if (parameters == null)
            {
                parameters = new List<Object> {
                    new SqlParameter("@all", 1)
                }.ToArray();
            }
            query = query.Trim();
            var isConnectDashboard = query.StartsWith("dashboard:", StringComparison.OrdinalIgnoreCase);
            if (isConnectDashboard)
            {
                query = query.Replace("dashboard:", "");
                using (var context = new DataAccess.EfContext(new MySqlConnection(_generalSettings.DashboardConnection)))
                {

                    var command = "";
                    try
                    {
                        if (query.StartsWith("call", StringComparison.OrdinalIgnoreCase))
                        {
                            command = query.Split(' ').Last();
                            var result = context.RawProcedure(command, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
                            return result;
                        }
                        else
                        {
                            
                            command = query;
                            var result = context.RawQuery(query, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
                            return result;
                        }
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                }
            }

            if (query.StartsWith("CAll", StringComparison.OrdinalIgnoreCase))
            {
                var stores = query.Split(' ');
                if (stores.Length <= 1)
                {
                    return new List<IDictionary<string, object>>();
                }
                return Context.RawProcedure(stores[1], parameters) as IEnumerable<IDictionary<string, object>>;
            }
            var kq = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return kq;
        }
    }
}