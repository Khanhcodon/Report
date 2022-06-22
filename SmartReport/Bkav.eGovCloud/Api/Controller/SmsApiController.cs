using System;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //Todo: đưa phần này vào chung với phần API riêng, không để linh tinh thế này
    //[RequireHttps]
    public class SmsApiController : EgovApiBaseController
    {
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _documentCopyService;
        private readonly UserBll _userService;
        private readonly SendSmsHelper _smsHelper;

        public SmsApiController()
        {
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _documentCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _smsHelper = DependencyResolver.Current.GetService<SendSmsHelper>();
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public string Default()
        {
            return "Bkav eGov";
        }

#if HoSoMotCuaEdition

        ///// <summary>
        ///// Tra cứu tiến độ hồ sơ
        ///// </summary>
        ///// <param name="docCode"></param>
        ///// <returns></returns>
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //public EDocumentModel GetDocumentByDocCode(string docCode)
        //{
        //    if (string.IsNullOrEmpty(docCode))
        //        return null;
        //    try
        //    {
        //        var document = _documentService.GetHsmc(docCode);
        //        if (document != null)
        //        {
        //            //Lấy văn bản /hồ sơ copy chính
        //            var docCopy = _documentCopyService.GetMain(document.DocumentId);
        //            var docCurrentUser = _userService.GetCacheAllUsers().First(p => p.UserId == docCopy.UserCurrentId);
        //            var deptName = string.Empty;
        //            var depts = docCurrentUser.UserDepartmentJobTitless;
        //            var deptPrimary = depts.FirstOrDefault(p => p.IsPrimary);
        //            if (deptPrimary != null)
        //            {
        //                deptName = deptPrimary.Department.DepartmentName;
        //            }
        //            else
        //            {
        //                deptName = depts.Select(p => p.Department).FirstOrDefault().DepartmentName;
        //            }

        //            var result = new EDocumentModel()
        //            {
        //                DocCode = document.DocCode,
        //                DeptName = deptName,
        //                StaffName = docCurrentUser.FullName,
        //                CompleteDate = document.DateAppointed.HasValue ? document.DateAppointed.Value : DateTime.Now,
        //                GivePerson = document.CitizenName,
        //                Status = MapStatus(document),
        //                IsCompleted = document.IsSuccess.HasValue ? document.IsSuccess.Value : false
        //            };
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex);
        //    }
        //    return null;
        //}

        [HttpGet]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public bool MessageReceive(string phone, string docCode)
        {
            try
            {
                var doc = _documentService.Get(docCode);
                _smsHelper.MOReceiver(phone, doc);
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                return false;
            }
        }

#endif

        private void LogException(Exception exc)
        {
            if (exc == null)
            {
                return;
            }
            var logService = DependencyResolver.Current.GetService<LogBll>();
            logService.Error(exc.Message, exc);
        }

        private string MapStatus(Document doc)
        {
            switch (doc.StatusInEnum)
            {
                case DocumentStatus.DuThao:
                    return "LT";//"Hồ sơ lưu tạm";
                case DocumentStatus.DangXuLy:
                    {
                        if (doc.IsSuccess == true)
                        {
                            return "DD";//"Hồ sơ được duyệt";
                        }
                        if (doc.IsReturned == true)
                        {
                            return "TV";//"Hồ sơ bị trả về";
                        }
                        if (doc.IsSupplemented == true)
                        {
                            return "CBS";// "Hồ sơ cần bổ sung";
                        }
                        return "CXL";// "Hồ sơ đang chờ xử lý";
                    }
                case DocumentStatus.DungXuLy:
                    {
                        if (doc.IsSuccess == true)
                        {
                            return "DD"; //"Hồ sơ được duyệt";
                        }
                        if (doc.IsReturned == true)
                        {
                            return "TV";//"Hồ sơ bị trả về";
                        }
                        if (doc.IsSupplemented == true)
                        {
                            return "CBS";// "Hồ sơ cần bổ sung";
                        }
                        return "DXL";// "Hồ sơ đang xử lý";
                    }
                case DocumentStatus.KetThuc:
                    return "KT";//"Hồ sơ đã kết thúc xử lý";
                case DocumentStatus.LoaiBo:
                    return "LB";//"Hồ sơ bị loại";
                default:
                    return "LT";//"Hồ sơ lưu tạm";
            }
        }
    }
}