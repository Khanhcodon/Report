using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class DataSourceController : CustomController
    {
        public DataSourceBll _datasourceService;
        public E_DataTableBll _dataTableService;
        public DataFieldBll _dataFieldService;
        public SqlTemplateBll _sqlTemplateService;
        public DepartmentBll _departmentService;
        private readonly AdminGeneralSettings _adminSetting;
        private readonly ResourceBll _resourceService;

        public DataSourceController(
            DataSourceBll datasourceService,
            E_DataTableBll dataTableService, 
            DataFieldBll dataFieldService, 
            SqlTemplateBll sqlTemplateService,
            DepartmentBll departmentService,
            AdminGeneralSettings adminSetting,
            ResourceBll resourceService)
        {
            _datasourceService = datasourceService;
            _dataTableService = dataTableService;
            _dataFieldService = dataFieldService;
            _sqlTemplateService = sqlTemplateService;
            _departmentService = departmentService;
            _adminSetting = adminSetting;
            _resourceService = resourceService;
        }

        #region DataSource
        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Form.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Form.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = _datasourceService.Gets().ToListModel();

            return View(model);
        }

        public JsonResult GetList(int domainId = 0)
        {
            var result = _datasourceService.Gets();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataSource(int id)
        {
            try
            {
                var result = _datasourceService.Get(id);
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Create(DataSourceModel model)
        {
            if (model == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var db = new DataSource()
            {
                Description = model.Description,
                Name = model.Name,
                Server = model.Server,
                Port = model.Port,
                DatabaseName = model.DatabaseName,
                Username = model.Username,
                Password = model.Password,
                DatabaseType = model.DatabaseType,
                Customer = model.Customer,
                DateModified = DateTime.Now,
                UserCreatedId = User.GetUserId(),
                DomainId = model.DepartmentId
            };

            try
            {
                _datasourceService.Create(db);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult Update(DataSourceModel model)
        {
            if (model == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var db = new DataSource()
            {
                DataSourceId = model.DataSourceId,
                Description = model.Description,
                Name = model.Name,
                Server = model.Server,
                Port = model.Port,
                DatabaseName = model.DatabaseName,
                Username = model.Username,
                Password = model.Password,
                DatabaseType = model.DatabaseType,
                Customer = model.Customer,
                DomainId = model.DepartmentId
            };

            try
            {
                _datasourceService.Update(db);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var tables = _datasourceService.GetTables(id);

                IEnumerable<DataField> fields;
                foreach (var table in tables)
                {
                    fields = _datasourceService.GetFields(table.DataTableId);
                    foreach (var field in fields)
                    {
                        _dataFieldService.Delete(field.DataFieldId);
                    }

                    _dataTableService.Delete(table.DataTableId);
                }

                _datasourceService.Delete(id);
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllDepartment()
        {
            var allDepartments = _departmentService.GetCacheAllDepartments();
     
            var result = allDepartments
                            .Select(d =>
                                new
                                {
                                    id = d.DepartmentId,
                                    title = d.DepartmentName,
                                    parentId = d.ParentId,
                                    subs = JsonConvert.DeserializeObject(GetChildren(allDepartments, d.DepartmentId))
                                }
                            )
                            .OrderBy(d => d.title).StringifyJs();

            return Json(new { result = false, value = result }, JsonRequestBehavior.AllowGet);
        }

        private string GetChildren(IEnumerable<DepartmentCached> allDepartments, int parentId)
        {
            var departments = allDepartments
                    .Where(c => c.ParentId == parentId)
                    .Select(d =>
                    new 
                    {
                        id = d.DepartmentId,
                        title = d.DepartmentName,
                        parentId = d.ParentId,
                        subs = JsonConvert.DeserializeObject(GetChildren(allDepartments, d.DepartmentId))
                    }
                ).OrderBy(d => d.title).StringifyJs();

            return departments;
        }

        #endregion

        #region DataTable
        public ActionResult DataTable(int id)
        {
            ViewBag.DataSourceId = id;
            var model = _datasourceService.GetTables(id);
            return View(model);
        }

        public JsonResult GetTable(int id)
        {
            var result = _datasourceService.GetTables(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTableInfor(int id)
        {
            var result = _dataTableService.Get(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateTable(E_DataTable model)
        {
            if (model == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                _dataTableService.Update(model);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult SyncTables(int id)
        {
            try
            {
                _datasourceService.SyncTables(id);
                return RedirectToAction("DataTable", new { id = id });
            }
            catch (Exception)
            {
                return RedirectToAction("DataTable", new { id = id });
            }
        }

        public ActionResult ShowData(int id)
        {
            var table = _dataTableService.Get(id);
            var query = _dataTableService.GetTableQuery(table);
            if (string.IsNullOrEmpty(query))
            {
                return View();
            }
            var ds = _datasourceService.Get(table.DataSourceId);
            if (ds == null)
            {
                return View();
            }

            var model = _dataTableService.GetModel(query, _adminSetting.DashboardConnection, 2);
            ViewBag.Data = Json2.Stringify(model);
            ViewBag.Query = query;
            ViewBag.DataId = id;
            return View();
        }

        #endregion

        #region Template

        public JsonResult GetTemplate(int id)
        {
            var result = _datasourceService.GetTemplates(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTemplateDetail(int id)
        {
            var result = _datasourceService.GetTemplateDetails(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DataField

        public ActionResult DataField(int id)
        {
            var table = _datasourceService.GetTable(id);
            if (table == null)
            {
                return View(new List<DataField>());
            }

            ViewBag.TableName = table.Name;
            ViewBag.DataTableId = id;
            ViewBag.DataSourceId = table.DataSourceId;
            ViewBag.Relations = _datasourceService.GetRelations(id);

            var model = _datasourceService.GetFields(id);
            string jsonModel = JsonConvert.SerializeObject(model);
            ViewBag.JsonModel = jsonModel;
            return View(model);
        }

        public JsonResult GetFields(int id)
        {
            var fieldMaths = _datasourceService.GetFieldMaths(id);
            var fields = _datasourceService.GetFields(id);

            var relations = _datasourceService.GetRelations(id);
            foreach (var relation in relations)
            {
                fieldMaths = fieldMaths.Concat(_datasourceService.GetFieldMaths(relation.TargetTableId));
                fields = fields.Concat(_datasourceService.GetFields(relation.TargetTableId));
            }

            return Json(new { fields, fieldMaths, relations }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFieldInfor(int id)
        {
            var result = _dataFieldService.Get(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateField(DataField model)
        {
            if (model == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                _dataFieldService.Update(model);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult SyncFields(int id)
        {
            try
            {
                _datasourceService.SyncFields(id);
                return RedirectToAction("DataField", new { id = id });
            }
            catch (Exception)
            {
                return RedirectToAction("DataField", new { id = id });
            }
        }

        [HttpPost]
        public JsonResult GetFieldValues(int id, string formula)
        {
            var result = _datasourceService.GetFieldValue(id, formula);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SQL Template
        public ActionResult SqlTemplate(int id)
        {
            ViewBag.DataSourceId = id;
            var model = _sqlTemplateService.Gets(id);
            return View(model);
        }

        public JsonResult GetListSqlTemplate(int datasourceId)
        {
            var result = _sqlTemplateService.Gets(datasourceId);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSqlTemplate(int id)
        {
            try
            {
                var result = _sqlTemplateService.Get(id);
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateSqlTemplate(SqlTemplate model)
        {
            if (model == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var db = new SqlTemplate()
            {
                DataSourceId = model.DataSourceId,
                Name = model.Name,
                QueryString = model.QueryString,
                DateModified = DateTime.Now,
                UserCreatedId = User.GetUserId(),
            };

            string message;
            try
            {
                _sqlTemplateService.Create(db, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult UpdateSqlTemplate(SqlTemplate model)
        {
            if (model == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            string message = string.Empty;
            try
            {
                _sqlTemplateService.Update(model, out message);
            }
            catch
            {
                return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult DeleteSqlTemplate(int id)
        {
            try
            {
                _sqlTemplateService.Delete(id);
                return Json(new { ok = true, mess = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, mess = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult GetFieldTemplateValues(int id, string formula)
        {
            var result = _datasourceService.GetFieldTemplateValue(id, formula);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Relation

        public JsonResult AddRelation(string relation,string operators)
        {

            if (string.IsNullOrEmpty(relation))
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var entity = Json2.ParseAs<Relation>(relation);
            var joinId = _datasourceService.GetJoinId(entity.SourceTableId) + 1;
            if (entity == null || entity.TargetTableId == 0 || entity.TargetColumn == "" || entity.JoinType == 0)
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            entity.JoinId = joinId;
            _datasourceService.AddRelation(entity);
            if (string.IsNullOrEmpty(relation)) return Json(new {success = true}, JsonRequestBehavior.AllowGet);
            var arr = Json2.ParseAs<List<Relation>>(operators);
            foreach (var item in arr.Where(item => item != null && item.TargetTableId != 0 && item.TargetColumn != "" && item.JoinType != 0 && item.JoinOperators != ""))
            {
                item.JoinId = joinId;
                _datasourceService.AddRelation(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DelRelation(int relationId)
        {
            if (relationId == 0)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            var relation = _datasourceService.GetRelation(relationId);
            if (relation == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }

            _datasourceService.DeleteRelation(relation);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}