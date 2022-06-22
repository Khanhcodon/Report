using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models.Settings;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Mail;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Mailers;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Mvc.Mailer;
using Newtonsoft.Json;
using Bkav.eGovCloud.Entities.Customer.Settings;
using System.Web;
using System.IO;
using System.Json;
using Bkav.eGovCloud.Core.Exceptions;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Net.Http;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using Bkav.eGovCloud.Helper;
using OfficeOpenXml;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class SettingController : CustomController//BaseController
    {
        private CBCLSetting _cbclSettings;
        private FAQSetting _faqSettings;
        private AdminGeneralSettings _generalSettings;
        private FileUploadSettings _fileUploadSettings;
        private EmailSettings _emailSettings;
        private AuthenticationSettings _authenticationSettings;
        private PasswordPolicySettings _passwordPolicySettings;
        private FileLocationSettings _fileLocationSettings;
        private SearchSettings _searchSettings;
        private ImageSettings _imageSettings;
        private SmsSettings _smsSettings;
        private TransferSettings _transferSettings;
        private LanguageSettings _languageSettings;
        private NotificationSettings _notificationSettings;
        private ConnectionSettings _connectionSettings;
        private SsoSettings _ssoSettings;
        private OnlineRegistrationSettings _onlineRegistrationSettings;
        private WarningSettings _warningSettings;
        private VoteSettings _voteSettings;
        private OtpSettings _otpSettings;
        private SSOSettings _sSOSettings;
        private MissionSettings _missionSettings;
        private ChatSettings _chatSettings;
        private ReportConfigSettings _reportConfigSettings;
        private SSOAPISettings _ssoapiSettings;

        private readonly AddressBll _addressSerice;
        private readonly PermissionBll _permissionSerice;
        private readonly SettingBll _settingService;
        private readonly ResourceBll _resourceService;
        private readonly LdapProvider _ldapProvider;
        private readonly IUserMailer _userMailer;
        private readonly FileLocationBll _fileLocationService;
        private readonly TemplateBll _templateService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly UserBll _userService;
        private readonly TreeGroupBll _treeGroupService;
        private readonly DocTypeBll _doctypeService;
        private readonly UserDepartmentJobTitlesPositionBll _userdepartService;
        private readonly NotifyConfigBll _notifyConfigService;
        private readonly PermissionSettingBll _permissionSettingService;
        private readonly VersionTreeSetting _versionSettingService;
        private readonly DocFieldBll _docFieldService;
        private readonly WorkflowBll _workflowService;

        public SettingController(
            JobTitlesBll jobTitlesService,
            SettingBll settingService,
            ResourceBll resourceService,
            LdapProvider ldapProvider,
            AdminGeneralSettings generalSettings,
            FileUploadSettings fileUploadSettings,
            EmailSettings emailSettings,
            AuthenticationSettings authenticationSettings,
            PasswordPolicySettings passwordPolicySettings,
            FileLocationSettings fileLocationSettings,
            SearchSettings searchSettings,
            ImageSettings imageSettings,
            SmsSettings smsSettigns,
            ConnectionSettings connectionSettings,
            VoteSettings voteSettings,
            TransferSettings transferSetting,
            IUserMailer userMailer,
            FileLocationBll fileLocationService,
            TemplateBll templateService,
            LanguageSettings languageSetting,
            OnlineRegistrationSettings onlineRegistrationSettings,
            PermissionSettingBll permissionSettingService,
            DepartmentBll departmentService,
            PositionBll positionService,
            UserBll userService,
            TreeGroupBll treeGroupService,
            NotifyConfigBll notifyConfigService,
            DocTypeBll doctypeService,
            CBCLSetting cbclSettings,
            FAQSetting faqSettings,
            UserDepartmentJobTitlesPositionBll userdepartService,
            PermissionBll permissionSerice,
            NotificationSettings notificationSettings,
            WarningSettings warningSettings,
            OtpSettings otpSettings,
            VersionTreeSetting versionSettingService,
            AddressBll addressSerice,
            SSOSettings sSOSettings,
            DocFieldBll docFieldService,
            WorkflowBll workflowService,
            MissionSettings missionSettings,
            ChatSettings chatSettings,
            ReportConfigSettings reportConfigSettings,
            SSOAPISettings ssoapiSettings
            )
            : base()
        {
            _settingService = settingService;
            _resourceService = resourceService;
            _ldapProvider = ldapProvider;
            _generalSettings = generalSettings;
            _fileUploadSettings = fileUploadSettings;
            _emailSettings = emailSettings;
            _authenticationSettings = authenticationSettings;
            _passwordPolicySettings = passwordPolicySettings;
            _userMailer = userMailer;
            _fileLocationSettings = fileLocationSettings;
            _cbclSettings = cbclSettings;
            _faqSettings = faqSettings;
            _fileLocationService = fileLocationService;
            _searchSettings = searchSettings;
            _imageSettings = imageSettings;
            _templateService = templateService;
            _smsSettings = smsSettigns;
            _transferSettings = transferSetting;
            _languageSettings = languageSetting;
            _ssoSettings = SsoSettings.Instance;
            _sSOSettings = sSOSettings;
            _connectionSettings = connectionSettings;
            //_kntcSettings = kntcSettings;
            _onlineRegistrationSettings = onlineRegistrationSettings;
            _permissionSettingService = permissionSettingService;
            _departmentService = departmentService;
            _notificationSettings = notificationSettings;
            _positionService = positionService;
            _jobTitlesService = jobTitlesService;
            _userService = userService;
            _treeGroupService = treeGroupService;
            _notifyConfigService = notifyConfigService;
            _doctypeService = doctypeService;
            _userdepartService = userdepartService;
            _permissionSerice = permissionSerice;
            _warningSettings = warningSettings;
            _otpSettings = otpSettings;
            _voteSettings = voteSettings;
            _versionSettingService = versionSettingService;
            _addressSerice = addressSerice;
            _docFieldService = docFieldService;
            _workflowService = workflowService;
            _missionSettings = missionSettings;
            _chatSettings = chatSettings;
            _reportConfigSettings = reportConfigSettings;
            _ssoapiSettings = ssoapiSettings;
        }

        #region Index
        public ActionResult Index()
        {
            ////Hopcv: 190614
            ////Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            //ViewBag.HasUseHSMC = HasUseHSMC();
            //return View();
            return RedirectToAction("General");
        }
        #endregion

        #region SyncAll_API
        /// <summary>
        /// Convert Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// Get token API
        /// </summary>
        /// <returns></returns>
        public string getToken()
        {
            var clientidSSO = _settingService.Get("ssoapisettings.clientidsso");
            var secretkeySSO = _settingService.Get("ssoapisettings.secretkeysso");
            var clientID = clientidSSO.SettingValue.ToString();
            var secretkey = secretkeySSO.SettingValue.ToString();
            var sercet = clientID + ":" + secretkey;
            var secretBase64 = Base64Encode(sercet);
            var token_access = "";
            using (var client = new HttpClient())
            {
                var fieldHttpAddress = _settingService.Get("ssoapisettings.httpaddresssso");
                var httpAddress = fieldHttpAddress.SettingValue.ToString();
                client.BaseAddress = new Uri(httpAddress);
                client.DefaultRequestHeaders.Add("Authorization", "Basic "+ secretBase64);
                var content = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("grant_type", "client_credentials")
                });
                var postToken = client.PostAsync("token", content);
                postToken.Wait();
                var postResult = postToken.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    var response = postToken.Result.Content.ReadAsStringAsync().Result;
                    var lst = JsonConvert.DeserializeObject<TokenModel>(response);
                    token_access = lst.access_token;
                }
            }
            return token_access;
        }
        /// <summary>
        /// Get data department từ API
        /// </summary>
        /// <returns></returns>
        public List<DepartmentAPI> getDepartment()
        {
            List<DepartmentAPI> lstDeparment = new List<DepartmentAPI>();
            var token = getToken();
            using (var client = new HttpClient())
            {
                var fieldHttpAddress = _settingService.Get("ssoapisettings.httpaddresssso");
                var httpAddress = fieldHttpAddress.SettingValue.ToString();
                client.BaseAddress = new Uri(httpAddress);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var postToken = client.GetAsync("hrm/1.0.0/departments?getActiveOnly=true");
                postToken.Wait();
                var postResult = postToken.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    var response = postToken.Result.Content.ReadAsStringAsync().Result;
                    var lst = JsonConvert.DeserializeObject<DataModel>(response);
                    if(lst == null)
                    {
                        RedirectToAction("SyncSSO");
                    }else
                    {
                        var _data = lst.data.units;
                        foreach (var data in _data)
                        {
                            lstDeparment.Add(new DepartmentAPI()
                            {
                                ParentID = data.ParentID,
                                ID = data.ID,
                                ShortName = data.ShortName,
                                Name = data.Name,
                                FullName = data.FullName,
                                IsActive = data.IsActive,
                                Lever = data.Lever
                            });
                        }
                    }
                }
            }
            return lstDeparment;
        }
        /// <summary>
        /// Get data Employee từ API
        /// </summary>
        /// <returns></returns>
        public List<EmployeeAPI> getEmployee()
        {
            List<EmployeeAPI> lstEmployee = new List<EmployeeAPI>();
            var token = getToken();
            using (var client = new HttpClient())
            {
                var fieldHttpAddress = _settingService.Get("ssoapisettings.httpaddresssso");
                var httpAddress = fieldHttpAddress.SettingValue.ToString();
                client.BaseAddress = new Uri(httpAddress);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var postToken = client.GetAsync("hrm/1.0.0/employees?getActiveOnly=true");
                postToken.Wait();
                var postResult = postToken.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    var response = postToken.Result.Content.ReadAsStringAsync().Result;
                    var lst = JsonConvert.DeserializeObject<EmployeeResApi>(response);
                    if(lst == null)
                    {
                        RedirectToAction("SyncSSO");
                    }else
                    {
                        var _data = lst.data.units;
                        foreach (var data in _data)
                        {
                            EmployeeAPI emp = new EmployeeAPI();
                            emp.ID = data.ID;
                            emp.Account = data.Account;
                            emp.FullName = data.FullName;
                            emp.IsActive = data.IsActive;
                            emp.Mobile = data.Mobile;
                            emp.CompanyEmail = data.CompanyEmail;
                            emp.PersonalEmail = data.PersonalEmail;
                            emp.Sex = data.Sex;
                            emp.BeginDate = data.BeginDate;
                            emp.EndDate = emp.EndDate;
                            emp.ListDept = new List<ListDept>();
                            foreach (var l in data.ListDept)
                            {
                                ListDept lstDept = new ListDept();
                                lstDept.ID = l.ID;
                                lstDept.IsConcurrently = l.IsConcurrently;
                                lstDept.DeptID = l.DeptID;
                                lstDept.PositionID = l.PositionID;
                                lstDept.TitleID = l.TitleID;
                                lstDept.OfficeID = l.OfficeID;
                                lstDept.BeginDate = l.BeginDate;
                                lstDept.EndDate = l.EndDate;
                                lstDept.IsActive = l.IsActive;
                                emp.ListDept.Add(lstDept);
                            }
                            lstEmployee.Add(emp);
                        }
                    }

                }
            }
            return lstEmployee;
        }
        public List<PositionsAPI> getPositions()
        {
            List<PositionsAPI> lstPositions= new List<PositionsAPI>();
            var token = getToken();
            using (var client = new HttpClient())
            {
                var fieldHttpAddress = _settingService.Get("ssoapisettings.httpaddresssso");
                var httpAddress = fieldHttpAddress.SettingValue.ToString();
                client.BaseAddress = new Uri(httpAddress);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var postToken = client.GetAsync("hrm/1.0.0/positions?getActiveOnly=true");
                postToken.Wait();
                var postResult = postToken.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    var response = postToken.Result.Content.ReadAsStringAsync().Result;
                    var lst = JsonConvert.DeserializeObject<PositionResAPI>(response);
                    if(lst == null)
                    {
                        RedirectToAction("SyncSSO");
                    }else
                    {
                        var _data = lst.data.units;
                        foreach (var data in _data)
                        {
                            PositionsAPI pos = new PositionsAPI();
                            pos.ID = data.ID;
                            pos.ParentID = data.ParentID;
                            pos.Name = data.Name;
                            pos.Level = data.Level;
                            pos.IsActive = data.IsActive;
                            lstPositions.Add(pos);
                        }
                    }

                }else
                {
                    SuccessNotification(_resourceService.GetResource("Server.Error"));
                    RedirectToAction("SyncSSO");
                }
            }
            return lstPositions;
        }

        /// <summary>
        /// SSO phòng ban
        /// </summary>
        public void SSOEmployee()
        {
            var lst = getEmployee();
            var usersImport = new List<User>();
            foreach (var row in lst)
            {
                     var _user = _userService.GetCacheAllUsers().FirstOrDefault(u => u.Username == row.Account);
                    if (_user == null ) {
                        var now = DateTime.Now;
                        bool gender = row.Sex;
                        var domainName = "";
                        string firstName, lastName;
                        string userName = row.Account;
                        string fullName = row.FullName;
                        string usernameemaildomain = row.PersonalEmail;
                        string separator = "@";

                        // Part 1: get index of separator.
                        int separatorIndex = usernameemaildomain.IndexOf(separator);

                        // Part 2: if separator exists, get substring.
                        if (separatorIndex >= 0)
                        {
                            string result = usernameemaildomain.Substring(separatorIndex + separator.Length);
                            domainName += result;
                        }

                        var lastSpaceIndex = fullName.LastIndexOf(' ');
                        lastName = fullName.Substring(0, lastSpaceIndex).Trim();
                        firstName = fullName.Substring(lastSpaceIndex + 1).Trim();
                        var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                        var defaultPass = "123456a@";
                        var hash = Generate.GetInputPasswordHash(defaultPass, salt);
                        bool isactived = (row.IsActive) ? true : false;
                        var user = new User()
                        {
                            Username = userName,
                            UsernameEmailDomain = usernameemaildomain,
                            FullName = fullName,
                            FirstName = firstName,
                            LastName = lastName,
                            Gender = gender,
                            CreatedByUserId = User.GetUserId(),
                            CreatedOnDate = now,
                            PasswordHash = hash,
                            PasswordSalt = salt,
                            VersionDateTime = now,
                            PasswordLastModifiedOnDate = now,
                            IsActivated = isactived,
                            IsLockedOut = false,
                            DomainName = domainName
                        };

                        usersImport.Add(user);
                    }
            }
            try
            {
                try
                {
                    _userService.Create(usersImport);
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
            catch (EgovException ex)
            {
                LogException(ex);
            }
        }
        /// <summary>
        /// De quy theo cha con
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        List<Department> LoopDepartment(string parentID, int level = 0)
        {
           
            var result = getDepartment();
            List<Department> lst = new List<Department>();
            List<Department> lstChild = new List<Department>();
            
            foreach (var i in result)
            {
                if (i.ParentID == parentID)
                {
                    Department unit = new Department();
                    string id = i.ID.ToString();
                    unit.DepartmentName = i.Name;
                    string _path = i.FullName.Replace("/", "\\");
                    unit.DepartmentPath = _path;
                    unit.IsActivated = i.IsActive;
                    unit.Level = level;
                    unit.CreatedOnDate = DateTime.Now;
                    lst.Add(unit);
                    lstChild = LoopDepartment(id, level + 1);
                    lst = lst.Concat(lstChild).ToList();
                }
            }

            return lst;
        }
        /// <summary>
        /// SSO chức vụ
        /// </summary>
        /// <param name="model"></param>
        public void SSOPosition(PositionModel model, JobTitlesModel model1)
        {
            var lstAll = getPositions();
            try
            {
              
                var entity = model.ToEntity();
                var entity1 = model1.ToEntity();
                if (model.HasCreatePacket && model1.HasCreatePacket)
                {
                    List<Position> list = new List<Position>();
                    var listJob = new List<JobTitles>();
                    foreach (var lst in lstAll)
                    {
                        list.Add(new Position
                        {
                            PositionName = lst.Name,
                            PriorityLevel = lst.Level
                        });
                        _positionService.CreateByAPI(list, model.IgnoreExist);

                        listJob.Add(new JobTitles
                        {
                            JobTitlesName = lst.Name,
                            PriorityLevel = lst.Level,
                            IsApproved = lst.IsActive,
                            CanGetDocumentCome = false,
                            IsClerical = false
                        });
                        _jobTitlesService.CreateByAPI(listJob, model.IgnoreExist);
                    }
                }
                else
                {
                    _positionService.Create(entity);
                    _jobTitlesService.Create(entity1);
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// SSO  3 table
        /// </summary>
        /// <returns></returns>
        public void SSOAll()
        {
            dynamic departments = getDepartment();
            dynamic employees = getEmployee();
            dynamic positions = getPositions();
            var userdepartmentpositions = new List<UserDepartmentJobTitlesPosition>();
            foreach (var employee in employees)
            {
                string username = employee.Account;

                var result = _userService.getIDbyUserName(username);
                foreach (var list in employee.ListDept)
                {
                    foreach (var i in result)
                    {
                        int userId = i.UserId;
                        var checkExists = _userdepartService.GetbyUserId(userId);
                        if (checkExists.Count() == 0)
                        {
                            var depID1 = list.DeptID;
                            var posID1 = list.PositionID;
                            foreach (var depart in departments)
                            {
                                var depID2 = depart.ID;
                                if (depID1 == depID2)
                                {
                                    var _path = "\\" + depart.FullName.Replace("/", "\\");
                                    foreach (var re in _departmentService.getIDbyPath(_path))
                                    {
                                        foreach (var pos in positions)
                                        {
                                            var posID2 = pos.ID;
                                            if (posID1 == posID2)
                                            {
                                                foreach (var k in _positionService.getIDbyName(pos.Name))
                                                {
                                                    var lstIdJobTitle = _jobTitlesService.getIDbyName(k.PositionName, k.PriorityLevel);
                                                    foreach (var j in lstIdJobTitle)
                                                    {
                                                        var userdepart = new UserDepartmentJobTitlesPosition()
                                                        {
                                                            PositionId = k.PositionId,
                                                            JobTitlesId = j.JobTitlesId,
                                                            UserId = i.UserId,
                                                            DepartmentId = re.DepartmentId,
                                                            DepartmentIdExt = re.DepartmentIdExt,
                                                            IsPrimary = true,
                                                            IsAdmin = false
                                                        };
                                                        userdepartmentpositions.Add(userdepart);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

            }
            try
            {
                _userdepartService.Create(userdepartmentpositions);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult SyncSSO()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SyncSSO(DepartmentModel model, PositionModel model1, JobTitlesModel model2)
        {
            SSOPosition(model1, model2);
            SSOEmployee();
            try
            {
                var entity = model.ToEntity();

                if (model.HasCreatePacket)
                {
                    var lst1 = new List<Department>();
                    lst1 = LoopDepartment("");
                    foreach (var list in lst1)
                    {
                        _departmentService.CreateByAPI(list);
                    }

                }
                else
                {
                    _departmentService.Create(entity);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Department.Position.Created"));
                SuccessNotification(_resourceService.GetResource("SyncAll.Position"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }
            SSOAll();
            return RedirectToAction("SyncSSO");
        }

        #endregion

        #region Export Excel
        public void ExportExcel()
        {
            var employee = _userService.Gets(true).ToList();
            var position = _positionService.GetAll().ToList();
            var jobtitle = _jobTitlesService.GetAll().ToList();
            var department = _departmentService.Gets().ToList();
            var all = _userdepartService.Gets().ToList();

            var list = (from a in all
                        join e in employee on a.UserId equals e.UserId
                        join j in jobtitle on a.JobTitlesId equals j.JobTitlesId
                        join p in position on a.PositionId equals p.PositionId
                        join d in department on a.DepartmentId equals d.DepartmentId
                        select new FileExport
                        {
                            Name = e.FullName,
                            Account = e.Username,
                            Sex = e.Gender,
                            PositionName = p.PositionName,
                            JobTitleName = j.JobTitlesName,
                            DepartmentName = d.DepartmentPath

                        }).ToList();
            var listAll = new List<FileExport>();
            foreach (var item in list)
            {

                FileExport a = new FileExport();
                a.Name = item.Name;
                a.Account = item.Account;
                a.Sex = item.Sex;
                a.PositionName = item.PositionName;
                a.JobTitleName = item.JobTitleName;
                a.DepartmentName = item.DepartmentName;
                listAll.Add(a);

            }
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ////ws.Cells["A1"].Value = "Date";
            ////ws.Cells["B1"].Value = string.Format("{0:dd MMMM yyyy} at {0:HH: mm tt}",DateTimeOffset.Now);
            ////ws.Cells["A3"].Value = "Tên thật";
            ////ws.Cells["B3"].Value = "Tài khoản";
            ////ws.Cells["C3"].Value = "Giới tính";
            ////ws.Cells["D3"].Value = "Chức vụ";
            ////ws.Cells["E3"].Value = "Chức danh";
            ////ws.Cells["F3"].Value = "Tên phòng ban";

            int rowStart = 1;
            foreach (var row in listAll)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = row.Name;
                ws.Cells[string.Format("B{0}", rowStart)].Value = row.Account;
                string sex = row.Sex ? "" : "Mr.";
                ws.Cells[string.Format("C{0}", rowStart)].Value = sex;
                ws.Cells[string.Format("D{0}", rowStart)].Value = row.PositionName;
                ws.Cells[string.Format("E{0}", rowStart)].Value = row.JobTitleName;
                ws.Cells[string.Format("F{0}", rowStart)].Value = row.DepartmentName;
                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filname=" + "Export.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        #endregion

        #region SyncAllFileJSON
        public string ConvertCharacter(string input)
        {
            var path = input.Trim(new Char[] { '\'', '"' });
            var _path = Regex.Replace(path, "[@\\\";'\\\\]", string.Empty);
            return _path;
        }
        List<Department> Lap(string parentID, int level = 0)
        {
            WebClient webClient = new WebClient();
            dynamic result = JsonValue.Parse(webClient.DownloadString("S:/Eformyenbai/Bkav.eGovCloud/Areas/Admin/departments.json"));
            List<Department> lst = new List<Department>();
            List<Department> lstChild = new List<Department>();
            foreach (var i in result.data.units)
            {
                string parentId = RemoveDigits(i.ParentID.ToString());
                if (parentId == parentID)
                {
                    Department unit = new Department();
                    string id = RemoveDigits(i.ID.ToString());
                    unit.DepartmentName = i.Name;
                    var departmentpath = ConvertCharacter(i.FullName.ToString());
                    var _path = departmentpath.Replace("/", "\\");
                    unit.DepartmentPath = "\\" + _path;
                    unit.IsActivated = true;
                    unit.Level = level;
                    lst.Add(unit);

                    lstChild = Lap(id, level + 1);
                    lst = lst.Concat(lstChild).ToList();
                }
            }

            return lst;
        }

        List<Position> LapPos(string parentID, int level = 0)
        {
            WebClient webClient = new WebClient();
            dynamic result = JsonValue.Parse(webClient.DownloadString("S:/Eformyenbai/Bkav.eGovCloud/Areas/Admin/positions.json"));
            List<Position> lst = new List<Position>();
            List<Position> lstChild = new List<Position>();
            foreach (var i in result.data.units)
            {
                string parentId = RemoveDigits(i.ParentID.ToString());
                if (parentId == parentID)
                {
                    Position unit = new Position();
                    string id = RemoveDigits(i.ID.ToString());
                    unit.PositionName = i.Name;
                    unit.PriorityLevel = level;
                    lst.Add(unit);
                    lstChild = LapPos(id, level + 1);
                    lst = lst.Concat(lstChild).ToList();
                }
            }

            return lst;
        }
        List<PositionUser> LapPosUser(string parentID, int level = 0)
        {
            WebClient webClient = new WebClient();
            dynamic result = JsonValue.Parse(webClient.DownloadString("S:/Eformyenbai/Bkav.eGovCloud/Areas/Admin/positions.json"));
            List<PositionUser> lst = new List<PositionUser>();
            List<PositionUser> lstChild = new List<PositionUser>();
            foreach (var i in result.data.units)
            {
                string parentId = RemoveDigits(i.ParentID.ToString());
                if (parentId == parentID)
                {
                    PositionUser unit = new PositionUser();
                    string id = RemoveDigits(i.ID.ToString());
                    unit.positionID = i.ID;
                    unit.positionName = i.Name;
                    unit.Level = level;
                    lst.Add(unit);
                    lstChild = LapPosUser(id, level + 1);
                    lst = lst.Concat(lstChild).ToList();
                }
            }

            return lst;
        }
        public static string RemoveDigits(string key)
        {
            return Regex.Replace(key, "(?:[^a-z0-9 ]|(?<=['\"])s)", "");
        }
      

        [HttpGet]
        public ActionResult SyncAll()
        {
            return View("SyncAll");
        }

        [HttpPost]
        public  ActionResult SyncAll(HttpPostedFileBase jsonFile1, PositionModel model)
        {
            if (jsonFile1 != null && jsonFile1.ContentLength > 0)
            {
                string _nameFile = jsonFile1.FileName;
                if (!Path.GetFileName(_nameFile).EndsWith(".json"))
                {
                    ViewBag.Error1 = "File không hợp lệ";
                }
                else
                {
                    jsonFile1.SaveAs(Server.MapPath("~/JSON/" + Path.GetFileName(_nameFile)));
                    string _pathName = Server.MapPath("~/JSON/" + Path.GetFileName(_nameFile));
                    WebClient webClient = new WebClient();
                    dynamic result = JsonValue.Parse(webClient.DownloadString(_pathName));

                    try
                    {
                        var maxPriorityLevel = _positionService.GetMaxPriorityLevel() + 1;
                        var entity = model.ToEntity();

                        if (model.HasCreatePacket)
                        {
                            var list = new List<Position>();
                            list = LapPos("");
                            _positionService.Create(list, model.IgnoreExist);
                        }
                        else
                        {
                            entity.PriorityLevel = maxPriorityLevel;
                            _positionService.Create(entity);
                        }
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Position.Created"));
                        SuccessNotification(_resourceService.GetResource("SyncAll.Position"));
                        return RedirectToAction("SyncAll");

                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                    }
                }
            }else
            {
                ViewBag.Error1 = "Hãy chọn file";
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SyncDe(HttpPostedFileBase jsonFile, DepartmentModel model, bool hasCreatePacket = true)
        {
            if (jsonFile != null && jsonFile.ContentLength > 0) {
                string _nameFile = jsonFile.FileName;
                if (!Path.GetFileName(_nameFile).EndsWith(".json"))
                {
                    ViewBag.Error = "File không hợp lệ";
                }
                else
                {
                    jsonFile.SaveAs(Server.MapPath("~/JSON/" + Path.GetFileName(_nameFile)));
                    string _pathName = Server.MapPath("~/JSON/" + Path.GetFileName(_nameFile));

                    WebClient webclient2 = new WebClient();
                    dynamic result2 = JsonValue.Parse(webclient2.DownloadString(_pathName));
                    try
                    {
                        var entity = model.ToEntity();

                        if (model.HasCreatePacket)
                        {
                            var lst1 = new List<Department>();
                            lst1 = Lap("");
                            _departmentService.CreateSync(lst1, model.IgnoreExist);
                        }
                        else
                        {
                            _departmentService.Create(entity);
                        }
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Department.Position.Created"));
                        SuccessNotification(_resourceService.GetResource("SyncAll.Position"));
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                    }

                    return RedirectToAction("SyncAll");
                }
            }else
            {
                ViewBag.Error = "Hãy chọn file";
            }
            return View("SyncAll");
        }

        [HttpPost]
        public ActionResult SyncUser(HttpPostedFileBase jsonFile2, UserModel model)
        {
            if (jsonFile2 != null && jsonFile2.ContentLength > 0)
            {
                string _nameFile = jsonFile2.FileName;
                if (!Path.GetFileName(_nameFile).EndsWith(".json"))
                {
                    ViewBag.Error2 = "File không hợp lệ";
                }
                else
                {
                    jsonFile2.SaveAs(Server.MapPath("~/JSON/" + Path.GetFileName(_nameFile)));
                    string _pathName = Server.MapPath("~/JSON/" + Path.GetFileName(_nameFile));

                    WebClient webclient2 = new WebClient();
                    dynamic result2 = JsonValue.Parse(webclient2.DownloadString(_pathName));
                    var usersImport = new List<User>();
                        foreach (var row in result2.data.units)
                        {
                            using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                var now = DateTime.Now;
                                bool gender = (row.Sex) ? true : false;
                                var domainName = "";
                                string firstName, lastName;
                                string userName = row.Account.ToString().Replace("\"", "");
                                string fullName = row.FullName.ToString();
                                string fullname = fullName.Replace("\"", "");
                                string usernameemaildomain = row.PersonalEmail.ToString().Replace("\"", "");
                                string separator = "@";

                                // Part 1: get index of separator.
                                int separatorIndex = usernameemaildomain.IndexOf(separator);

                                // Part 2: if separator exists, get substring.
                                if (separatorIndex >= 0)
                                {
                                    string result = usernameemaildomain.Substring(separatorIndex + separator.Length);
                                    domainName += result;
                                }

                                var lastSpaceIndex = fullname.LastIndexOf(' ');
                                lastName = fullname.Substring(0, lastSpaceIndex).Trim();
                                firstName = fullname.Substring(lastSpaceIndex + 1).Trim();
                                var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                                var defaultPass = "123456a@";
                                var hash = Generate.GetInputPasswordHash(defaultPass, salt);
                            bool isactived = (row.IsActive) ? true : false;
                            var user = new User()
                                {
                                    Username = userName,
                                    UsernameEmailDomain = usernameemaildomain,
                                    FullName = fullname,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Gender = gender,
                                    CreatedByUserId = User.GetUserId(),
                                    CreatedOnDate = now,
                                    PasswordHash = hash,
                                    PasswordSalt = salt,
                                    VersionDateTime = now,
                                    PasswordLastModifiedOnDate = now,
                                    IsActivated = isactived,
                                    IsLockedOut = false,
                                    DomainName = domainName
                                };
                           
                            usersImport.Add(user);
                                transactionUser.Complete();
                            }
                        }
                    try
                    {
                        try
                        {
                            _userService.Create(usersImport);
                            SuccessNotification(_resourceService.GetResource("User.Created"));
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                            throw;
                        }
                      
                    }
                    catch (EgovException ex)
                        {
                        LogException(ex);
                        }
                    return RedirectToAction("SyncAll");
                }
            }
            else
            {
                ViewBag.Error2 = "Hãy chọn file";
            }
            return View("SyncAll");
        }
        [HttpPost]
        public ActionResult SyncUserDepaert()
        {
            try
            {
                WebClient webClient = new WebClient();
                dynamic departments = JsonValue.Parse(webClient.DownloadString("S:/Eformyenbai/Bkav.eGovCloud/Areas/Admin/departments.json"));
                dynamic employees = JsonValue.Parse(webClient.DownloadString("S:/Eformyenbai/Bkav.eGovCloud/Areas/Admin/employees.json"));
                dynamic positions = JsonValue.Parse(webClient.DownloadString("S:/Eformyenbai/Bkav.eGovCloud/Areas/Admin/positions.json"));
                var userdepartmentpositions = new List<UserDepartmentJobTitlesPosition>();
                var userposition = LapPosUser("");
                foreach (var employee in employees.data.units)
                {
                    string username = ConvertCharacter(employee.Account.ToString());

                    var result = _userService.getIDbyUserName(username);
                    foreach (var list in employee.ListDept)
                    {
                        foreach (var i in result)
                        {
                            int userId = i.UserId;
                            var checkExists = _userdepartService.GetbyUserId(userId);
                            if (checkExists.Count() == 0)
                            {
                                var depID1 = list.DeptID.ToString();
                                var posID1 = ConvertCharacter(list.PositionID.ToString());
                                foreach (var depart in departments.data.units)
                                {
                                    var depID2 = depart.ID.ToString();
                                    if (depID1 == depID2)
                                    {

                                        var _path = ConvertCharacter(depart.FullName.ToString());
                                        var _path2 = "\\" + _path.Replace("/", "\\");
                                        foreach (var re in _departmentService.getIDbyPath(_path2))
                                        {
                                            foreach (var user in userposition)
                                            {
                                                var posID2 = user.positionID;
                                                if (posID1 == posID2)
                                                {
                                                    foreach (var k in _positionService.getIDbyNameandLevel(user.positionName, user.Level))
                                                    {
                                                        var userdepart = new UserDepartmentJobTitlesPosition()
                                                        {
                                                            PositionId = k.PositionId,
                                                            UserId = i.UserId,
                                                            JobTitlesId = 8031,
                                                            DepartmentId = re.DepartmentId,
                                                            DepartmentIdExt = re.DepartmentIdExt,
                                                            IsPrimary = true,
                                                            IsAdmin = false
                                                        };
                                                        userdepartmentpositions.Add(userdepart);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                    }

                }
                try
                {
                    _userdepartService.Create(userdepartmentpositions);
                    SuccessNotification("Đồng bộ xong");
                   
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("SyncAll");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
          
            return View("SyncAll");
        }
        #endregion

        #region SSOSettings
        public ActionResult SSOApiSettings()
        {
            var model = _ssoapiSettings.ToModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult SSOApiSettings(SSOAPISettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _ssoapiSettings = model.ToEntity(_ssoapiSettings);
                _settingService.SaveSetting(_ssoapiSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("SSOApiSettings");
            }
            return View(model);
        }
        #endregion

        #region MissionSettings
        public ActionResult MissionSettings()
        {
            var model = _missionSettings.ToModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult MissionSettings(MissionSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _missionSettings = model.ToEntity(_missionSettings);
                _settingService.SaveSetting(_missionSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("MissionSettings");
            }
            return View(model);
        }
        #endregion

        #region ChatSettings
        public ActionResult ChatSettings()
        {
            var model = _chatSettings.ToModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ChatSettings(ChatSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _chatSettings = model.ToEntity(_chatSettings);
                _settingService.SaveSetting(_chatSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("ChatSettings");
            }
            return View(model);
        }
        #endregion

        #region ReportConfigSettings
        public ActionResult ReportConfigSettings()
        {
            var model = _reportConfigSettings.ToModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ReportConfigSettings(ReportConfigSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _reportConfigSettings = model.ToEntity(_reportConfigSettings);
                _settingService.SaveSetting(_reportConfigSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("ReportConfigSettings");
            }
            return View(model);
        }
        #endregion
        #region ConnectionSetting

        public ActionResult ConnectionSetting()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            ViewBag.ConnectionString = DataSettings.Current.DataConnectionString;
            return View();
        }

        [HttpPost]
        public ActionResult ConnectionSetting(string connectionSetting)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            var setting = DataSettings.Current;
            setting.DataConnectionString = connectionSetting;
            setting.Save();

            RestartApplication();

            ViewBag.ConnectionString = connectionSetting;
            return View();
        }

        private void RestartApplication()
        {
            WebHelper.RestartApplication();
        }

        #endregion

        #region General

        public ActionResult General()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index");
            }

            ViewBag.ListShowAcountType = GetListShowAcountType();
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.UserAcceptCalendars = _userService.GetAllCached(true).Where(u => _generalSettings.UserAcceptCalendarList.Contains(u.UserId));

            var model = _generalSettings.ToModel();
            if (_generalSettings.ListPageSize.Count != 0)
            {
                for (var i = 0; i < _generalSettings.ListPageSize.Count; i++)
                {
                    model.ListPageSizeParsed += _generalSettings.ListPageSize[i];
                    if (i != _generalSettings.ListPageSize.Count - 1)
                    {
                        model.ListPageSizeParsed += ",";
                    }
                }
            }

            if (_generalSettings.ListPageSizeHome.Count != 0)
            {
                for (var i = 0; i < _generalSettings.ListPageSizeHome.Count; i++)
                {
                    model.ListPageSizeParsedHome += _generalSettings.ListPageSizeHome[i];
                    if (i != _generalSettings.ListPageSizeHome.Count - 1)
                    {
                        model.ListPageSizeParsedHome += ",";
                    }
                }
            }

            ViewBag.AllUsers = GetAllUsers();
            LoadDropDownList();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult General(AdminGeneralSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _generalSettings = model.ToEntity(_generalSettings);

                _generalSettings.ListPageSize.Clear();

                if (!string.IsNullOrWhiteSpace(model.ListPageSizeParsed))
                {
                    foreach (string pageS in Regex.Split(model.ListPageSizeParsed, @"\D+"))
                    {
                        int pageSize;
                        if (int.TryParse(pageS, out pageSize))
                        {
                            _generalSettings.ListPageSize.Add(pageSize);
                        }
                    }
                }

                _generalSettings.ListPageSizeHome.Clear();
                if (!string.IsNullOrWhiteSpace(model.ListPageSizeParsedHome))
                {
                    foreach (string pageSH in Regex.Split(model.ListPageSizeParsedHome, @"\D+"))
                    {
                        int pageSizeHome;
                        if (int.TryParse(pageSH, out pageSizeHome))
                        {
                            _generalSettings.ListPageSizeHome.Add(pageSizeHome);
                        }
                    }
                }
                _settingService.SaveSetting(_generalSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));

                return RedirectToAction("General");
            }

            ViewBag.ListShowAcountType = GetListShowAcountType();
            ViewBag.AllUsers = GetAllUsers();

            return View(model);
        }

        private List<SelectListItem> GetListDocumentNotifyType()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(DocumentNotifyType));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((DocumentNotifyType)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<DocumentNotifyType>((DocumentNotifyType)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }

        private List<SelectListItem> GetListMailNotifyType()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(MailNotifyType));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((MailNotifyType)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<MailNotifyType>((MailNotifyType)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }

        private List<SelectListItem> GetListShowAcountType()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(ShowAcountType));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((ShowAcountType)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<ShowAcountType>((ShowAcountType)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }

        private List<SelectListItem> GetListMailType()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(MailType));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((MailType)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<MailType>((MailType)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }

        #endregion General

        #region SSO

        public ActionResult SSO()
        {
            var model = _sSOSettings.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SSO(SSOSettingsModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.ApiUrl[model.ApiUrl.Length - 1] == '/')
                    model.ApiUrl = model.ApiUrl.Substring(0, model.ApiUrl.Length - 1);
                _sSOSettings = model.ToEntity(_sSOSettings);

                _settingService.SaveSetting(_sSOSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("SSO");
            }
            return View(model);
        }
        #endregion

        #region FileUpload

        public ActionResult FileUpload()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionFileUpload"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionFileUpload"));
                return RedirectToAction("Index");
            }

            var model = _fileUploadSettings.ToModel();
            for (var i = 0; i < _fileUploadSettings.FileUploadAllowedExtensions.Count; i++)
            {
                model.FileUploadAllowedExtensionsParsed += _fileUploadSettings.FileUploadAllowedExtensions[i];
                if (i != _fileUploadSettings.FileUploadAllowedExtensions.Count - 1)
                {
                    model.FileUploadAllowedExtensionsParsed += ",";
                }
            }
            for (var i = 0; i < _fileUploadSettings.ProfilePictureAllowedExtensions.Count; i++)
            {
                model.ProfilePictureAllowedExtensionsParsed += _fileUploadSettings.ProfilePictureAllowedExtensions[i];
                if (i != _fileUploadSettings.ProfilePictureAllowedExtensions.Count - 1)
                {
                    model.ProfilePictureAllowedExtensionsParsed += ",";
                }
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingFileUpload")]
        public ActionResult FileUpload(FileUploadSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionFileUpload"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionFileUpload"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _fileUploadSettings = model.ToEntity(_fileUploadSettings);

                _fileUploadSettings.FileUploadAllowedExtensions.Clear();
                if (!string.IsNullOrWhiteSpace(model.FileUploadAllowedExtensionsParsed))
                {
                    foreach (var extension in model.FileUploadAllowedExtensionsParsed.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        _fileUploadSettings.FileUploadAllowedExtensions.Add(extension);
                    }
                }
                _fileUploadSettings.ProfilePictureAllowedExtensions.Clear();
                if (model.ProfilePictureAllowedExtensionsParsed != null && model.ProfilePictureAllowedExtensionsParsed != "")
                {
                    foreach (var extension in model.ProfilePictureAllowedExtensionsParsed.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        _fileUploadSettings.ProfilePictureAllowedExtensions.Add(extension);
                    }
                }
                _settingService.SaveSetting(_fileUploadSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("FileUpload");
            }
            return View(model);
        }

        #endregion FileUpload

        #region Email

        public ActionResult Email()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionEmail"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionEmail"));
                return RedirectToAction("Index", "Welcome");
            }
            Notify();
            ViewBag.IsMail = true;
            return View(_emailSettings.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingEmail")]
        public ActionResult Email(EmailSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionEmail"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionEmail"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.SmtpPassword))
                {
                    model.SmtpPassword = _emailSettings.SmtpPassword.Base64Decode();
                }

                _emailSettings = model.ToEntity(_emailSettings);

                _settingService.SaveSetting(_emailSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Email");
            }

            Notify();
            ViewBag.IsMail = true;
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingTestSendMail")]
        public JsonResult SendMail(string mail, string body, bool enableSsl, string host, int port, string username, string password)
        {
            if (!mail.IsEmailAddress())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.MailIsValid"));
                return Json(new
                {
                    error = _resourceService.GetResource("Customer.Setting.MailIsValid")
                });
            }

            if (string.IsNullOrEmpty(body))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.BodyMailIsValid"));
                return Json(new
                {
                    error = _resourceService.GetResource("Customer.Setting.BodyMailIsValid")
                });
            }

            try
            {
                var mailMessage = _userMailer.Test();
                mailMessage.To.Add(mail);
                mailMessage.Body = body;
                var smtpClient = new SmtpClient
                {
                    EnableSsl = enableSsl,
                    Host = host,
                    Port = port,
                    Credentials = new NetworkCredential(username, password)
                };
                //hiepns
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                ISmtpClient smtpClientWrapper = new SmtpClientWrapper(smtpClient);
                smtpClient.Send(mailMessage);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.TestSendMailSuccess"));
                return Json(new
                {
                    success = _resourceService.GetResource("Customer.Setting.TestSendMailSuccess")
                });
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.TestSendMailError"));
                return Json(new
                {
                    error = _resourceService.GetResource("Customer.Setting.TestSendMailError")
                });
            }
        }

        public ActionResult TestSendMail()
        {
            return View("TestSendMail");
        }

        #endregion Email

        #region Authentication

        public ActionResult Authentication()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionAuthentication"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionAuthentication"));
                return RedirectToAction("Index");
            }

            return View(_authenticationSettings.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingAuthentication")]
        public ActionResult Authentication(AuthenticationSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionAuthentication"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionAuthentication"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var oldPassword = _authenticationSettings.LdapPassword;
                _authenticationSettings = model.ToEntity(_authenticationSettings);
                if (string.IsNullOrWhiteSpace(model.LdapPassword))
                {
                    _authenticationSettings.LdapPassword = oldPassword;
                }

                _settingService.SaveSetting(_authenticationSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Authentication");
            }
            return View(model);
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public JsonResult TestConnectionLdap(
            string host,
            string port,
            string basedn,
            string username,
            string password,
            bool useSSL)
        {
            return Json(new { result = _ldapProvider.Authenticate(host, port, useSSL, username, password) },
                        JsonRequestBehavior.AllowGet);
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public JsonResult TestConnectionIMAPPOP3(
            string host,
            string port,
            bool useSSL)
        {
            var mailPop3 = new MailPop3IMapUtil(host, int.Parse(port), useSSL);

            return Json(new { result = mailPop3.TestConnect() },
                        JsonRequestBehavior.AllowGet);
        }


        public ActionResult PasswordPolicy()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionPasswordPolicy"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionPasswordPolicy"));
                return AccessDeniedView();
            }

            var model = _passwordPolicySettings.ToModel();
            model.MaximumAge = model.MaximumAge / 86400;
            model.WarningTime = model.WarningTime / 86400;
            model.LockoutDuration = model.LockoutDuration / 60;
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingPasswordPolicy")]
        public ActionResult PasswordPolicy(PasswordPolicySettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionPasswordPolicy"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionPasswordPolicy"));
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                model.MaximumAge = model.MaximumAge * 86400;
                model.WarningTime = model.WarningTime * 86400;
                model.LockoutDuration = model.LockoutDuration * 60;
                _passwordPolicySettings = model.ToEntity(_passwordPolicySettings);

                _settingService.SaveSetting(_passwordPolicySettings);

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("PasswordPolicy");
            }
            return View(model);
        }

        #endregion Authentication

        #region FileLocation

        public ActionResult FileLocation()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionFileLocation"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionFileLocation"));
                return RedirectToAction("Index");
            }

            if (_fileLocationSettings.Threshold < 1)
            {
                _fileLocationSettings.Threshold = 4096;
            }
            var listFileLocation = _fileLocationService.Gets();
            ViewBag.ListFileLocation = listFileLocation.ToListModel();
            ViewBag.FileLocation = new FileLocationModel();
            return View(_fileLocationSettings.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingFileLocation")]
        public ActionResult FileLocation(FileLocationSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionFileLocation"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionFileLocation"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _fileLocationSettings = model.ToEntity(_fileLocationSettings);

                _settingService.SaveSetting(_fileLocationSettings);

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("FileLocation");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingCreateOrEditFileLocation")]
        public JsonResult CreateFileLocation(FileLocationModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionCreateFileLocation"));
                return Json(new { message = _resourceService.GetResource("Customer.Setting.NotPermissionCreateFileLocation"), error = true });
            }

            if (ModelState.IsValid)
            {
                _fileLocationService.Create(model.ToEntity());
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Created"));
                return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Created") });
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Create.Eror"));
            return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Create.Eror"), error = true });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingCreateOrEditFileLocation")]
        public JsonResult EditFileLocation(FileLocationModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionUpdateFileLocation"));
                return Json(new { message = _resourceService.GetResource("Customer.Setting.NotPermissionUpdateFileLocation"), error = true });
            }

            if (ModelState.IsValid)
            {
                var fileLocation = _fileLocationService.Get(model.FileLocationId);
                if (fileLocation == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotFindId"));
                    return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotFindId") + model.FileLocationId, error = true });
                }
                // TienBV sửa: đã được sử dụng thì vẫn có thể active lại
                if (!_fileLocationService.IsUsed(model.FileLocationId))
                {
                    // return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotChange"), error = true });
                    fileLocation = model.ToEntity(fileLocation);
                }
                if (fileLocation.IsActivated && !model.IsActivated)
                {
                    model.IsActivated = true;
                }
                fileLocation.IsActivated = model.IsActivated;
                _fileLocationService.Update(fileLocation);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Updated"));
                return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Updated") });
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Update.Error"));
            return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Update.Error"), error = true });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingDeleteFileLocation")]
        public JsonResult DeleteFileLocation(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionDeleteFileLocation"));
                return Json(new { message = _resourceService.GetResource("Customer.Setting.NotPermissionDeleteFileLocation"), error = true });
            }

            if (_fileLocationService.IsUsed(id))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotPermission"));
                return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotPermission"), error = true });
            }
            var fileLocation = _fileLocationService.Get(id);
            if (fileLocation == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotFindId"));
                return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.NotFindId") + id, error = true });
            }
            _fileLocationService.Delete(fileLocation);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Deleted"));
            return Json(new { message = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Setting.FileLocation.Deleted") });
        }

        #endregion FileLocation

        #region Search

        public ActionResult Search()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionSearch"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionSearch"));
                return RedirectToAction("Index");
            }
            GetDefaultInfo();
            if (string.IsNullOrWhiteSpace(_searchSettings.ServerUrl))
            {
                _searchSettings.ServerUrl = "http://localhost:8983/solr";
            }
            return View(_searchSettings.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingSearch")]
        public ActionResult Search(SearchSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionSearch"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionSearch"));
                return RedirectToAction("Index");
            }
            GetDefaultInfo();
            if (ModelState.IsValid)
            {
                _searchSettings = model.ToEntity(_searchSettings);

                _settingService.SaveSetting(_searchSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Search");
            }
            return View(model);
        }

        #endregion Search

        #region image

        [HttpGet]
        public ActionResult Image()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionImage"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionImage"));
                return RedirectToAction("Index");
            }

            var model = _imageSettings.ToModel();
            ViewBag.Bits = GetColorBits();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingImage")]
        public ActionResult Image(ImageSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionImage"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionImage"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (model.IsGrayImage == true && model.ColorBits != 8)
                {
                    ViewBag.Bits = GetColorBits();
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                    ErrorNotification(_resourceService.GetResource("Setting.Updated"));
                    return View(model);
                }
                _imageSettings = model.ToEntity(_imageSettings);
                _settingService.SaveSetting(_imageSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Image");
            }
            ViewBag.Bits = GetColorBits();
            return View(model);
        }

        /// <summary>
        /// Lấy danh sách bit màu
        /// </summary>
        /// <returns>Danh sách bit màu</returns>
        private List<SelectListItem> GetColorBits()
        {
            return new List<SelectListItem>(){
                     new SelectListItem(){Value="1",Text="1"},
                     new SelectListItem(){Value="8",Text="8"},
                     new SelectListItem(){Value="24",Text="24"}
                };
        }

        #endregion image

        #region Sms

        public ActionResult Sms()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionSms"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionSms"));
                return RedirectToAction("Index");
            } 

            var model = _smsSettings.ToModel();
            model.ServicePass = EncryptionHelper.Decrypt(model.ServicePass);
            Notify();
            ViewBag.IsMail = false;

            return View(model);
        }

        public JsonResult TestSms(string phone = "0964681635", string mess = "test")
        {
            var _timerJobHelper = DependencyResolver.Current.GetService<TimerJobHelper>();
            var result = _timerJobHelper.TestSendSms(phone, mess);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingSms")]
        public ActionResult Sms(SmsSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionSms"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionSms"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if(model.ServicePass == null)
                {
                    _smsSettings = model.ToEntity(_smsSettings);
                    _settingService.SaveSetting(_smsSettings);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Update"));
                    SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                    return RedirectToAction("Sms");
                }
                else
                {
                    _smsSettings = model.ToEntity(_smsSettings);
                    _smsSettings.ServicePass = EncryptionHelper.Encrypt(model.ServicePass);
                    _settingService.SaveSetting(_smsSettings);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                    SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                    return RedirectToAction("Sms");
                }            
            }

            Notify();
            ViewBag.IsMail = false;
            return View(model);
        }

        #endregion Sms

        #region Transfer

        public ActionResult Transfer()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionTransfer"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionTransfer"));
                return RedirectToAction("Index");
            }

            ViewBag.AllUsers = GetAllUsers();
            ViewBag.AllDepts = GetAllDepartments();

            _transferSettings = MigraceToNewVersion(_transferSettings);

            var model = _transferSettings.ToModel();

            return View(model);
        }

        /// <summary>
        /// Chuyển cách lưu dữ liệu cũ userId: userName sang kiểu dữ liệu mới
        /// </summary>
        /// <param name="_transferSettings"></param>
        /// <returns></returns>
        private TransferSettings MigraceToNewVersion(TransferSettings transferSettings)
        {
            try
            {
                var rootDept = _departmentService.GetRoot();
                var currentOrgan = _addressSerice.GetCurrent();

                var userReceiveVbds = transferSettings.GetUserReceiveVbDenOld();
                if (userReceiveVbds.Any())
                {
                    var users = _userService.GetAllCached().Where(u => userReceiveVbds.Contains(u.UserId));
                    var setting = users.Select(u => new UserReceivesModel()
                    {
                        UserId = u.UserId,
                        UserName = u.Username,
                        EDocId = currentOrgan.EdocId,
                        DepartmentName = currentOrgan.Name,
                        DepartmentId = rootDept.DepartmentId
                    });

                    transferSettings.UserReceiveVbDen = setting.Stringify();
                }
                
                var userReceiveHscm = transferSettings.GetUserReceiveHsmcOld();
                if (userReceiveHscm.Any())
                {
                    var users = _userService.GetAllCached().Where(u => userReceiveHscm.Contains(u.UserId));
                    var setting = users.Select(u => new UserReceivesModel()
                    {
                        UserId = u.UserId,
                        UserName = u.Username,
                        EDocId = currentOrgan.EdocId,
                        DepartmentName = currentOrgan.Name,
                        DepartmentId = rootDept.DepartmentId
                    });

                    transferSettings.UserReceiveHsmc = setting.Stringify();
                }

                _settingService.SaveSetting(transferSettings);
            }
            catch
            {
                // TH cấu hình mới, bỏ qua
            }

            return transferSettings;
        }

        [HttpPost]
        public ActionResult Transfer(TransferSettingsModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionTransfer"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionTransfer"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                // Loại bỏ những thành phần dư thừa khi client post lên là string
                model.UserReceiveVbDen = Json2.ParseAs<List<UserReceivesModel>>(model.UserReceiveVbDen?? "[]").Stringify();
                model.UserReceiveHsmc = Json2.ParseAs<List<UserReceivesModel>>(model.UserReceiveHsmc?? "[]").Stringify();

                _transferSettings = model.ToEntity(_transferSettings);
                _settingService.SaveSetting(_transferSettings);

                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Transfer");
            }
            return View(model);
        }

        public string GetCurrentAddresses()
        {
            var address = _addressSerice.GetCurrent();
            if (address != null)
            {
                var addresses = _addressSerice.GetAddresses(address.AddressId, true);
                return addresses.StringifyJs();
            }

            return "[]";
        }

        #endregion Transfer

        #region Language

        public ActionResult Language()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionLanguage"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionLanguage"));
                return RedirectToAction("Index");
            }
            return View(_languageSettings.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingLanguage")]
        public ActionResult Language(LanguageSettingsModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionLanguage"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionLanguage"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _languageSettings = model.ToEntity(_languageSettings);

                _settingService.SaveSetting(_languageSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Language");
            }
            return View(model);
        }

        #endregion Language

        #region Notification

        public ActionResult Notification()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionNotification"));
            //    return RedirectToAction("Index");
            //}


            var model = _notificationSettings.ToModel();

            if (!string.IsNullOrWhiteSpace(model.NotifyUrl))
            {
                var notifyUri = new Uri(model.NotifyUrl);
                model.NotifyPathUrl = notifyUri.AbsolutePath;
                model.DomainUrl = notifyUri.GetFullDomainUrl();
            }
            else
            {
                model.NotifyPathUrl = "/n";
                model.DomainUrl = Request.GetFullDomainUrl();
            }

            ViewBag.ListDocumentNotifyType = GetListDocumentNotifyType();
            ViewBag.ListMailNotifyType = GetListMailNotifyType();

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingNotification")]
        public ActionResult Notification(NotificationSettingsModel model)
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionNotification"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                if (!model.NotifyPathUrl.StartsWith("/"))
                {
                    model.NotifyPathUrl = "/" + model.NotifyPathUrl;
                }
                model.NotifyUrl = model.DomainUrl + model.NotifyPathUrl;
                _notificationSettings = model.ToEntity(_notificationSettings);

                _settingService.SaveSetting(_notificationSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Notification");
            }
            ViewBag.ListDocumentNotifyType = GetListDocumentNotifyType();
            ViewBag.ListMailNotifyType = GetListMailNotifyType();

            return View(model);
        }

        #endregion Notification

        #region Connection

        public ActionResult Connection()
        {
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionConnection"));
            //    return RedirectToAction("Index");
            //}

            ViewBag.ListMailType = GetListMailType();
            ViewBag.AllUsers = GetAllUsers();

            var model = _ssoSettings.ToModel();
            model.MailType = _connectionSettings.MailType;
            model.AppsParse = JsonConvert.DeserializeObject<List<Apps>>(_connectionSettings.Apps);
            model.AppsParse = model.AppsParse.OrderBy(x => x.Order).ToList();
            model.BmailLink = _connectionSettings.BmailLink;
            //ObjectHelper.CopyValues(model, _connectionSettings);

            return View(model);
        }

        private partial class AppOrder
        {
            public int appId { get; set; }
            public int order { get; set; }
        }

        [HttpPost]
        public ActionResult Connection(ConnectionSettingsModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionConnection"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionConnection"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _ssoSettings = model.ToEntity(_ssoSettings);
                _ssoSettings.Save();

                var apps = GetListCreateApp();

                #region Save App Order
                if (!string.IsNullOrWhiteSpace(model.AppOrder))
                {
                    var appsOrderParse = JsonConvert.DeserializeObject<List<AppOrder>>(model.AppOrder);
                    foreach (var app in apps)
                    {
                        var appOrder = appsOrderParse.FirstOrDefault(x => x.appId == app.Id);
                        app.Order = appOrder.order;
                    }
                }

                #endregion

                _connectionSettings = model.ToEntity(_connectionSettings);
                _connectionSettings.Apps = apps.Stringify();
                _settingService.SaveSetting(_connectionSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Connection");
            }
            ViewBag.ListMailType = GetListMailType();

            return View(model);
        }

        public ActionResult CreateApp()
        {
            return View(new Apps());
        }

        [HttpPost]
        public ActionResult CreateApp(Apps model)
        {
            bool success = false;
            try
            {
                var appsParse = GetListCreateApp();

                var id = appsParse.Count() + 1;
                model.Id = id;
                model.Order = id;
                model.IsDefaultApp = model.IsDefaultApp && !appsParse.Any(x => x.IsDefaultApp);

                appsParse.Add(model);

                _connectionSettings.Apps = appsParse.Stringify();
                _settingService.SaveSetting(_connectionSettings);

                success = true;
                return Json(new { success, message = "Success" });
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return Json(new { success, message = "Error" });
        }

        public ActionResult EditApp(int id)
        {
            var appsParse = GetListCreateApp();
            var app = appsParse.FirstOrDefault(x => x.Id == id);
            if (app == null)
            {
                app = new Apps();
            }
            return View(app);
        }

        [HttpPost]
        public ActionResult EditApp(Apps model)
        {
            bool success = false;
            try
            {
                var appsParse = GetListCreateApp();
                foreach (var app in appsParse)
                {
                    if (app.Id == model.Id)
                    {
                        app.IconUrl = model.IconUrl;
                        app.AppUrl = model.AppUrl;
                        app.IsBackgroundApp = model.IsBackgroundApp;
                        app.IsDefaultApp = model.IsDefaultApp && !appsParse.Any(x => x.IsDefaultApp);
                        app.Name = model.Name;
                        app.Title = model.Title;
                    }
                }
                _connectionSettings.Apps = appsParse.Stringify();
                _settingService.SaveSetting(_connectionSettings);

                success = true;
                return Json(new { success, message = "Success" });
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return Json(new { success, message = "Error" });
        }

        public ActionResult DeleteApp(int id)
        {
            try
            {
                var appsParse = GetListCreateApp();

                var deleteApp = appsParse.FirstOrDefault(x => x.Id == id);
                if (deleteApp != null)
                {
                    appsParse.Remove(deleteApp);
                }
                for (int i = 0; i < appsParse.Count; i++)
                {
                    appsParse[i].Id = i + 1;
                }
                _connectionSettings.Apps = appsParse.Stringify();
                _settingService.SaveSetting(_connectionSettings);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return RedirectToAction("Connection", "Setting");
        }

        [HttpPost]
        public ActionResult ChangeActiveApp(int appId)
        {
            bool success = false;
            try
            {
                var appsParse = GetListCreateApp();
                foreach (var app in appsParse)
                {
                    if (app.Id == appId)
                    {
                        app.IsActived ^= true;
                    }
                }
                _connectionSettings.Apps = appsParse.Stringify();
                _settingService.SaveSetting(_connectionSettings);

                success = true;
                return Json(new { success, message = "Success" });
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return Json(new { success, message = "Error" });
        }

        [HttpPost]
        public ActionResult ChangeDefaultApp(int appId)
        {
            bool success = false;
            try
            {
                var appsParse = GetListCreateApp();
                foreach (var app in appsParse)
                {
                    app.IsDefaultApp = app.Id == appId;
                }
                _connectionSettings.Apps = appsParse.Stringify();
                _settingService.SaveSetting(_connectionSettings);
                success = true;
                return Json(new { success, message = "Success" });
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return Json(new { success, message = "Error" });
        }

        [HttpPost]
        public ActionResult ChangeBackgroundApp(int appId)
        {
            bool success = false;
            try
            {
                var appsParse = GetListCreateApp();
                foreach (var app in appsParse)
                {
                    if (app.Id == appId)
                    {
                        app.IsBackgroundApp ^= true;
                    }
                }
                _connectionSettings.Apps = appsParse.Stringify();
                _settingService.SaveSetting(_connectionSettings);

                success = true;
                return Json(new { success, message = "Success" });
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            return Json(new { success, message = "Error" });
        }

        public JsonResult AppIconUpload()
        {
            var file = Request.Files["file"];
            bool success = false;
            if (file != null && file.ContentLength > 0)
            {
                if (file.InputStream.Length > _fileUploadSettings.ProfilePictureMaximumSizeBytes)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Account.Avartar.Fields.FileUploadMaximumSizeBytes") + " (" + _fileUploadSettings.ProfilePictureMaximumSizeBytes + " KB)");
                    return Json(new { success, message = _resourceService.GetResource("Account.Avartar.Fields.FileUploadMaximumSizeBytes") + " (" + _fileUploadSettings.ProfilePictureMaximumSizeBytes + " KB)" });
                }
                var ext = System.IO.Path.GetExtension(file.FileName);
                if (!_fileUploadSettings.ProfilePictureAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Account.Avartar.Fields.FileUploadAllowedExtensions"));
                    return Json(new { success, message = _resourceService.GetResource("Account.Avartar.Fields.FileUploadAllowedExtensions") });
                }

                Helper.ResizeAndCropImage.CropAndCropResizeImage(file.InputStream, 25, 25,
                                                                Server.MapPath("~/ImagesUpload/") + file.FileName + ".jpg");
                success = true;
                return Json(new { success, Avatar = "/ImagesUpload/" + file.FileName + ".jpg" });
            }

            return Json(new { success, message = "Error" });
        }

        #endregion

        #region CBCL
        public ActionResult CBCL()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionCBCL"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionCBCL"));
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ActiveConfig = _cbclSettings.IsActive;
            ViewBag.CurrentUser = GetUserCurrent();
            ViewBag.AllUsers = GetAllUsers();

            ViewBag.Template = _cbclSettings.HtmlTemplate != null ? _cbclSettings.HtmlTemplate : "";

            return View();
        }

        public JsonResult GetDoctype()
        {
            var listDoctype = _doctypeService.Gets();

            return Json(listDoctype, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDoctypeCurrent()
        {
            var doctypeid = new Guid(_cbclSettings.DoctypeConfig);
            var doctype = _doctypeService.Get(doctypeid);

            return Json(doctype, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfigDoctype(string doctypeId)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionCBCL.ConfigDoctype"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionCBCL.ConfigDoctype"));
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            _cbclSettings.DoctypeConfig = doctypeId;
            _settingService.SaveSetting(_cbclSettings);

            return Json(_cbclSettings.DoctypeConfig, JsonRequestBehavior.AllowGet);
        }

        private List<User> GetUserCurrent()
        {
            var userid = _cbclSettings.AccountsName;
            List<User> currentusers = new List<User>();
            if (string.IsNullOrEmpty(userid))
            {
                currentusers = new List<User>();
            }
            else
            {
                var users = userid.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < users.Length; i++)
                {
                    currentusers.Add(_userService.Get(Convert.ToInt32(users[i])));
                }
            }

            return currentusers;
        }

        public JsonResult ConfigUser(string active, string userid)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionCBCL.ConfigUser"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionCBCL.ConfigUser"));
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var oldValue = _cbclSettings.AccountsName;
            if (oldValue == null)
            {
                oldValue = userid;
            }
            else
            {
                if (active == "Add")
                {
                    if (oldValue == "")
                    {
                        oldValue = userid;
                    }
                    else
                    {
                        string[] users = oldValue.Split(',');
                        if (users.Contains(userid))
                        {
                            return Json("Error", JsonRequestBehavior.AllowGet);
                        }

                        if (_userService.GetNhanVienCapDuoi(int.Parse(userid),
                                    _departmentService.GetCacheAllUserDepartmentJobTitlesPosition(),
                                    _positionService.GetCacheAllPosition()).Count() > 0)
                        {
                            return Json("Manager", JsonRequestBehavior.AllowGet);
                        }

                        oldValue = oldValue + "," + userid;
                    }
                }
                else
                {
                    string[] users = oldValue.Split(',');
                    int count = 0;
                    oldValue = "";
                    for (int i = 0; i < users.Length; i++)
                    {
                        if (userid != users[i])
                        {
                            count++;
                            if (count == 1)
                            {
                                oldValue = users[i];
                            }
                            else
                            {
                                oldValue = oldValue + "," + users[i];
                            }
                        }
                    }
                }
            }
            _cbclSettings.AccountsName = oldValue;
            _settingService.SaveSetting(_cbclSettings);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfigActive(bool value)
        {
            _cbclSettings.IsActive = value;

            _settingService.SaveSetting(_cbclSettings);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult ConfigTemplateDocument(string value)
        {
            _cbclSettings.HtmlTemplate = value;
            _settingService.SaveSetting(_cbclSettings);

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vote
        public ActionResult Vote()
        {
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.IsActive = _voteSettings.IsActive;
            ViewBag.UserCreates = _voteSettings.UserCreates;
            ViewBag.ListUserCreateVote = GetsUserCreate(_voteSettings.ListUserCreateVote());
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "SettingTransfer")]
        public ActionResult Vote(int UsersCreate = 0, int removeId = 0)
        {
            if (ModelState.IsValid)
            {
                var users = _voteSettings.ListUserCreateVote();
                if (UsersCreate != 0)
                {
                    if (!IsExist(users, UsersCreate))
                    {
                        users.Add(UsersCreate);
                    }
                }
                if (removeId != 0)
                {
                    users.Remove(removeId);
                }
                _voteSettings.UserCreates = VoteSettings.UserCompareString(users);

                _settingService.SaveSetting(_voteSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("Vote");
            }
            return View();
        }
        private bool IsExist(List<int> users, int userAdd)
        {
            if (users.Any(item => item == userAdd))
            {
                return true;
            }

            return false;
        }
        private IEnumerable<User> GetsUserCreate(List<int> users)
        {
            return _userService.Gets(users);
        }

        #endregion

        #region FAQ Setting
        public ActionResult FAQSetting()
        {
            if (!HasUseHSMC())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Setting.FunctionIsNotExist"));
                return RedirectToAction("Index", "Welcome");
            }

            BindData();
            var model = _faqSettings.ToModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult FAQSetting(FAQSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (!HasUseHSMC())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Setting.FunctionIsNotExist"));
                return RedirectToAction("Index", "Welcome");
            }

            if (ModelState.IsValid)
            {
                _faqSettings = model.ToEntity(_faqSettings);
                _settingService.SaveSetting(_faqSettings);

                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("FAQSetting");
            }

            BindData();
            return View(model);
        }
        #endregion

        #region onlineRegistrationSettings

        public ActionResult OnlineRegistration()
        {
            if (!HasUseHSMC())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.FunctionIsNotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.FunctionIsNotExist"));
                return RedirectToAction("Index", "Welcome");
            }

            BindData();
            var model = _onlineRegistrationSettings.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult OnlineRegistration(OnlineRegistrationSettingsModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (!HasUseHSMC())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.FunctionIsNotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.FunctionIsNotExist"));
                return RedirectToAction("Index", "Welcome");
            }

            if (ModelState.IsValid)
            {
                _onlineRegistrationSettings = model.ToEntity(_onlineRegistrationSettings);
                _settingService.SaveSetting(_onlineRegistrationSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
                return RedirectToAction("OnlineRegistration");
            }

            BindData();
            return View(model);
        }

        private void BindData()
        {
            ViewBag.AllPermissionSettings = _permissionSettingService.GetCacheAllPermissionSettings()
                .Select(p => new SelectListItem
                {
                    Value = p.PermissionSettingId.ToString(),
                    Text = p.PermissionSettingName
                }).ToList();

            ViewBag.AllTreeGroups = GetTreeGroups();
        }

        private string GetAllDepartments()
        {
            var result = "[]";
            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            if (allDepartments != null)
            {
                result = allDepartments
                            .Select(d =>
                                new
                                {
                                    label = d.DepartmentPath,
                                    value = d.DepartmentId,
                                    departmentName = d.DepartmentName,
                                    parentId = d.ParentId
                                }
                            )
                            .OrderBy(d => d.label).StringifyJs();
            }
            return result;
        }

        private string GetAllUsers()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            return allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                    fullname = u.FullName,
                                    username = u.Username,
                                    phone = u.Phone
                                }).StringifyJs();
        }

        private List<SelectListItem> GetTreeGroups(int? treeGroupId = null)
        {
            var groups = _treeGroupService.GetCacheAllTreeGroups();
            if (groups != null && groups.Any())
            {
                return groups.Select(p => new SelectListItem()
                {
                    Value = p.TreeGroupId.ToString(),
                    Text = p.TreeGroupName,
                    Selected = (treeGroupId.HasValue && treeGroupId.Value == p.TreeGroupId)
                }).ToList();
            }

            return new List<SelectListItem>();
        }

        #endregion

        #region cache

        public void ClearCache(string returnUrl = null)
        {
            _versionSettingService.CacheVersion = DateTime.Now.ToString();
            _settingService.SaveSetting(_versionSettingService);
            ClearAllCache(returnUrl);
        }

        #endregion

        #region notify

        public void Notify()
        {
            var cfgs = Enum.GetValues(typeof(NotifyConfigType)).Cast<NotifyConfigType>().ToList();
            var allNotifyConfigs = _notifyConfigService.Gets().ToList();

            var model = allNotifyConfigs.ToListModel().ToList();
            foreach (var cfg in cfgs)
            {
                var exist = allNotifyConfigs.Exists(p => p.Key.Equals(cfg.ToString()));
                if (!exist)
                {
                    var tmp = new NotifyConfigModel
                    {
                        Key = cfg.ToString(),
                        Description = _resourceService.GetEnumDescription<NotifyConfigType>(cfg)
                    };
                    model.Add(tmp);
                }
            }
            ViewBag.AllNotify = model;
        }

        public ActionResult UpdateNotify(string key, bool isMail = false)
        {
            var model = new NotifyConfigModel();
            var notify = _notifyConfigService.Get(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (notify == null)
            {
                var description = string.Empty;

                var keyEnum = (NotifyConfigType)Enum.Parse(typeof(NotifyConfigType), key, true);
                var options = Enum.GetValues(typeof(NotifyConfigType)).Cast<NotifyConfigType>().ToList();
                foreach (var opt in options)
                {
                    if (opt == keyEnum)
                    {
                        description = _resourceService.GetEnumDescription<NotifyConfigType>(opt);
                        break;
                    }
                }

                model.Key = key;
                model.Description = description;
            }
            else
            {
                model = notify.ToModel();
            }

            BindTemplates();
            ViewBag.IsMail = isMail;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateNotify(NotifyConfigModel model, bool isMail = false)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notify = _notifyConfigService.Get(p => p.Key.Equals(model.Key, StringComparison.OrdinalIgnoreCase));
                    if (notify == null)
                    {
                        _notifyConfigService.Create(model.ToEntity());
                    }
                    else
                    {
                        notify = model.ToEntity(notify);
                        _notifyConfigService.Update(notify);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.NotifyConfig.Edit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.NotifyConfig.Edit.Success"));
                    var redirect = isMail ? "email" : "Sms";
                    return RedirectToAction(redirect);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.NotifyConfig.Edit.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.NotifyConfig.Edit.Error"));
                }
            }
            var erorr = ModelState.Values.SelectMany(m => m.Errors);
            BindTemplates();
            ViewBag.IsMail = isMail;
            return View(model);
        }

        public ActionResult OTPConfig(bool isMail = false)
        {
            var model = _otpSettings.ToModel();
            model.ActiveMailTemplateName = GetTemplateName(model.ActiveMailTemplateId);
            model.ActiveSmsTemplateName = GetTemplateName(model.ActiveSmsTemplateId);
            model.ResetPassMailTemplateName = GetTemplateName(model.ResetPassMailTemplateId);
            model.ResetPassSmsTemplateName = GetTemplateName(model.ResetPassSmsTemplateId);
            BindTemplates();
            ViewBag.IsMail = isMail;
            return View(model);
        }

        [HttpPost]
        public ActionResult OTPConfig(OTPSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var otp_setting = model.ToEntity();
                    _settingService.SaveSetting(otp_setting);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.NotifyConfig.Edit.Success"));
                    SuccessNotification(_resourceService.GetResource("Common.NotifyConfig.Edit.Success"));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.NotifyConfig.Edit.Error"));
                    ErrorNotification(_resourceService.GetResource("Common.NotifyConfig.Edit.Error"));
                }
            }
            BindTemplates();
            return View(model);
        }

        private string GetTemplateName(int templateId)
        {
            var template = _templateService.Get(templateId);
            if (template != null)
            {
                return template.Name;
            }
            return "";
        }

        private void BindTemplates()
        {
            ViewBag.Templates = _templateService.GetsAs(p => new
            {
                value = p.TemplateId,
                label = p.Name,
                type = p.Type,
            }, p => p.IsActive
               && (p.Type == (int)TemplateType.Email
                    || p.Type == (int)TemplateType.Sms)).Stringify();
        }

        #endregion

        #region private function

        /// <summary>
        /// Lấy ra danh sách các ứng dụng được tích hợp
        /// </summary>
        /// <returns></returns>
        private List<Apps> GetListCreateApp()
        {
            return JsonConvert.DeserializeObject<List<Apps>>(_connectionSettings.Apps);
        }

        /// <summary>
        /// Lấy ra danh sách người dùng, phòng ban, vị trí cho vào viewbag nhằm autocomplete
        /// </summary>
        private void GetDefaultInfo()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            ViewBag.AllUsers = allUsers != null ? allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                }).StringifyJs() : "[]";

            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            ViewBag.AllDepartments = allDepartments != null ? allDepartments
                        .Select(d =>
                            new
                            {
                                label = d.DepartmentPath,
                                value = d.DepartmentId,
                                departmentName = d.DepartmentName,
                                parentId = d.ParentId
                            }
                        )
                        .OrderBy(d => d.label).StringifyJs() : "[]";

            var allPositions = _positionService.GetCacheAllPosition();
            ViewBag.AllPositions = allPositions != null ? allPositions
                        .Select(d =>
                            new
                            {
                                label = d.PositionName,
                                value = d.PositionId,
                            }
                        )
                        .OrderBy(d => d.label).StringifyJs() : "[]";
        }

        private void LoadDropDownList(int? levelId = 0)
        {
            ViewBag.ListLevel = _resourceService.EnumToSelectList<LevelType>(levelId);
            ViewBag.AllDocFields = _docFieldService.GetsAs(
                        d => new DocFieldModel { DocFieldId = d.DocFieldId, DocFieldName = d.DocFieldName })
                        .OrderBy(t => t.DocFieldName)
                        .Select(d => new SelectListItem {
                            Value = d.DocFieldId.ToString(),
                            Text = d.DocFieldName });
            ViewBag.AllWorkflows = _workflowService.GetsAs(
                        c => new WorkflowModel { WorkflowId = c.WorkflowId, WorkflowName = c.WorkflowName, IsActivated = c.IsActivated })
                        .Select(p => new SelectListItem {
                            Value = p.WorkflowId.ToString(),
                            Text = p.WorkflowName + " | " + (p.IsActivated ? "Hoạt động" : "Không hoạt động")
                        });
        }

        #endregion

        #region Warning

        public ActionResult Warning()
        {
            var model = _warningSettings.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Warning(WarningSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _warningSettings = model.ToEntity(_warningSettings);
                _settingService.SaveSetting(_warningSettings);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Setting.Updated"));
                SuccessNotification(_resourceService.GetResource("Setting.Updated"));
            }

            return RedirectToAction("Warning");
        }

        public string TestWarning()
        {
            var timeJobHelper = DependencyResolver.Current.GetService<TimerJobHelper>();
            timeJobHelper.TestSendWarning();

            return "Ok";
        }

        #endregion
    }
    public class UserDepartmentPosition
    {
        public int userID { get; set; }
        public int departID { get; set; }
        public int posID { get; set; }
        public string departIDExt { get; set; }
        public string departmentID { get; set; }
        public string positionID { get; set; }
    }

    public class PositionUser
    {
        public string positionID { get; set; }
        public string positionName { get; set; }
        public int Level { get; set; }

    }

    public class TokenModel
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    #region DepartmentAPI
    public class DepartmentAPI
    {
        public string ParentID { get; set; }
        public string ID { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public int Lever { get; set; }
    }

    public class Data
    {
        public List<DepartmentAPI> units { get; set; }
    }

    public class DataModel
    {
        public int code { get; set; }
        public string msg { get; set; }
        public Data data { get; set; }
    }
   
    #endregion

    #region PositionsAPI

    public class PositionsAPI
    {
        public string ParentID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsActive { get; set; }
    }

    public class DataPosition
    {
        public List<PositionsAPI> units { get; set; }
    }

    public class PositionResAPI
    {
        public int code { get; set; }
        public string msg { get; set; }
        public DataPosition data { get; set; }
    }

    #endregion

    #region User
    public class ListDept
    {
        public string ID { get; set; }
        public bool IsConcurrently { get; set; }
        public string DeptID { get; set; }
        public string PositionID { get; set; }
        public string TitleID { get; set; }
        public string OfficeID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeAPI
    {
        public string ID { get; set; }
        public string Account { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
        public bool Sex { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ListDept> ListDept { get; set; }
    }

    public class DataEmployee
    {
        public List<EmployeeAPI> units { get; set; }
    }

    public class EmployeeResApi
    {
        public int code { get; set; }
        public string msg { get; set; }
        public DataEmployee data { get; set; }
    }
    #endregion

    public class FileExport
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public bool Sex { get; set; }
        public string PositionName { get; set; }
        public string JobTitleName { get; set; }
        public string DepartmentName { get; set; }

    }
}