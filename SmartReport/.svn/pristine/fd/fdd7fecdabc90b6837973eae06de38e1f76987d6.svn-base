using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using System;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

using System.Net.Http;
using Bkav.eGovCloud.Core.Utils;

using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Newtonsoft.Json;
using System.IO;
using Bkav.eGovCloud.Core.Exceptions;

namespace Bkav.eGovCloud.Controllers {
    /// <summary>
    /// 
    /// </summary>
    public class MissionController : CustomerBaseController {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly DocumentBll _documentService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly UserBll _userService;
        private readonly MissionSettings _missionSettings;
        

        //private string PartnerGUID = "ed4887dc-6b88-4ee2-a8c8-39d838800c69";
        //private string PartnerToken = "2c9bd9adc4ba41479f1f7402a655b3db";

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="generalSettings"></param>
        /// <param name="searchService"></param>
        /// <param name="searchInDatabaseService"></param>
        /// <param name="searchInSolrService"></param>
        public MissionController(AdminGeneralSettings generalSettings, Helper.UserSetting helperUserSetting,
            UserBll userBll,
            DocumentBll documentService,
            DocumentCopyBll documentCopyServices,
            MissionSettings missionSettings,
            UserBll userService
            ) {
            _generalSettings = generalSettings;
            _helperUserSetting = helperUserSetting;
            _documentService = documentService;
            _docCopyService = documentCopyServices;
            _userService = userBll;
            _missionSettings = missionSettings;
        }
        public string GetDomain() {
            return _missionSettings.ApiDomain;
        }
        /// <summary>
        /// Lấy user từ api
        /// https://apibmmsotnmthn.hcdt.vn:5443/api/MissionCreator/GetListUser?UserNameDelivery=dongnt_tnmt
        /// </summary>
        /// <param name="docIds"></param>
        /// <returns></returns>
        public JsonResult GetListUser() {
            var users = getUsers();
            if (users != null)
                return Json(users, JsonRequestBehavior.AllowGet);
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        public string DownloadAttachmentTempBase64Completed(Stream file)
        {
            return file.ToBase64String();
        }

        /// <summary>
        /// Tạo nhiệm vụ 
        /// https://apibmmsotnmthn.hcdt.vn:5443/api/MissionCreator/CreateMission
        /// </summary>
        /// <param name="content"></param>
        /// <param name="date"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CreateMission(string docIds, string content, string tenbaocao, string kybaocao, string fileContent, string endDate, string user, List<string> coUsers) {

            if (docIds == null ) {
                return Json(new { error = "thiếu thông tin văn bản." });
            }
            if (string.IsNullOrWhiteSpace(content)) {
                return Json(new { error = "thiếu ý kiến chỉ đạo." });
            }
            if (string.IsNullOrWhiteSpace(user)) {
                return Json(new { error = "thiếu người thực hiện thực hiện." });
            }

            var doc = _docCopyService.Get(docIds[0]);
            string username = _userService.CurrentUser.Username;
            string beginDate = DateTime.Now.ToString("MM/dd/yyyy");
            ////test
            //username = "anhdtn_tnmt";
            var sourceJsonData = new SourceJsonData();
            sourceJsonData.TenBaoCao = tenbaocao;
            sourceJsonData.KyBaoCao = kybaocao ;
            var json = JsonConvert.SerializeObject(sourceJsonData);

            var input = new CreateMissioInput() {
                    SourceObjectID = docIds,
		            SourceJsonData = json,
                    UserNameCreate = username,
                    UserNameDelivery = username,
                    UserNamePerform = user,
                    BeginDate = beginDate,
                    EndDate = endDate,
                    MissionContent = content,
                    MissionGroupName = "Nhiệm vụ được tạo từ eForm",
                    IsNotifyEmail = false,
                    IsNotifySMS = false,
                    ListCooperation = new List<ListCooperation>(),
                    ListFileAttachContent = new List<FileAttach>(),
                    ListTag = new List<Tags>(),
                    Note = "",
                    StatusID = 1
                };
            if (coUsers != null && coUsers.Count > 0) {
                foreach (var item in coUsers) {
                    input.ListCooperation.Add(new ListCooperation() { UserNameCooperation = item });
                }
            }

            string _pathName = Server.MapPath("~" + fileContent);
            string _fileContent;
            using (var stream = new FileStream(path: _pathName, mode: FileMode.Open, access: FileAccess.Read, share: FileShare.Read))
            {
                   _fileContent = DownloadAttachmentTempBase64Completed(stream);
            }
            input.ListFileAttachContent.Add(new FileAttach() { FileName = tenbaocao +"-" +kybaocao+".docx", FileContentBase64 = _fileContent });
            var result = AddMission(input);
            if (result.isOk)
                return Json(new { success = "Tạo nhiệm vụ thành công." });
            return Json(new { error = result.Object });
        }

        /// <summary>
        /// Chi tiết nhiệm vụ
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        public JsonResult LinkDetailMission(List<Int32> docIds)
        {
            if (docIds == null || docIds.Count == 0)
            {
                return Json(new { error = "thiếu thông tin văn bản." });
            }
            int documentId = docIds[0];
            if (documentId > 0)
            {
                var docCopy = _docCopyService.Get(documentId);
                string url = _missionSettings.ApiDomain + "/nhiem-vu/giao?PartnerID=" + _missionSettings.PartnerGUID + "&PartnerObjectID=" + docCopy.Document.DocumentId;
                return Json(new { url = url, success = "success" }, JsonRequestBehavior.AllowGet);
            } else
            {
                return Json(new { error = "Khong ton tai document." });
            }
        }

       
        private GetUserResponse getUsers() {
            string url = _missionSettings.ApiDomain+"/api/MissionCreator/GetListUser?UserNameDelivery="+ _userService.CurrentUser.Username;
            GetUserResponse result = null;
            using (var client = new HttpClient()) {
                string[] splitSoucreGUIID = _missionSettings.PartnerGUID.ToString().Split(';');
                string[] splitSoucreToken = _missionSettings.PartnerToken.ToString().Split(';');
                client.DefaultRequestHeaders.Add("SourceGUID", splitSoucreGUIID[0]); 
                client.DefaultRequestHeaders.Add("SourceToken", splitSoucreToken[0]) ;
               
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode) {
                    string responseStr = response.Content.ReadAsStringAsync().Result as string;
                    result = Json2.ParseAs<GetUserResponse>(responseStr);
                }
            }
            return result;
        }

        private CreateMissionOutput AddMission(CreateMissioInput input) {
            string url =  _missionSettings.ApiDomain + "/api/MissionCreator/CreateMission";
  
            using (var client = new HttpClient()) {
                string[] splitSoucreGUIID = _missionSettings.PartnerGUID.ToString().Split(';');
                string[] splitSoucreToken = _missionSettings.PartnerToken.ToString().Split(';');
                client.DefaultRequestHeaders.Add("SourceGUID", splitSoucreGUIID[1]);
                client.DefaultRequestHeaders.Add("SourceToken", splitSoucreToken[1]);
                string s = input.ToJson();
                var req = new HttpRequestMessage(HttpMethod.Post, url) {
                    Content = new StringContent(input.ToJson(),
                                Encoding.UTF8,
                                "application/json")
                };
                var response = client.SendAsync(req).Result;
                if (response.IsSuccessStatusCode) {
                    var responseStr = response.Content.ReadAsStringAsync().Result as string;
                    return Json2.ParseAs<CreateMissionOutput>(responseStr);
                }
            }
            return null;
        }
        private class SourceJsonData {
            public string TenBaoCao { get; set; }
            public string KyBaoCao { get; set; }
        }

        private class GetUserResponse {
            public int status { get; set; }
            public List<MissionUser> Object { get; set; } = new List<MissionUser>();
            public bool isOk { get; set; }
            public bool isError { get; set; }
        }
        private class MissionUser {
            public int UserID { get; set; }
            public string UrlAvatar { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string UserDeptName { get; set; }
        }

        /// <summary>
        /// MissionCreator
        /// </summary>
        private class CreateMissioInput {


            public string SourceObjectID { get; set;  }
            public string SourceJsonData { get; set; }
            /// <summary>
            /// Tên nhóm nhiệm vụ
            /// </summary>
            public string MissionGroupName { get; set; }

            /// <summary>
            ///  UserName người tạo
            /// </summary>
            public string UserNameCreate { get; set; }

            /// <summary>
            /// UserName người giao
            /// </summary>
            public string UserNameDelivery { get; set; }

            /// <summary>
            /// UserName người thực hiện
            /// </summary>
            public string UserNamePerform { get; set; }

            /// <summary>
            /// Danh sách Người phối hợp
            /// </summary>
            public List<ListCooperation> ListCooperation { get; set; }

            /// <summary>
            /// Ngày bắt đầu
            /// 2020-03-15
            /// </summary>
            public string BeginDate { get; set; }

            /// <summary>
            /// Ngày kết thúc
            /// 2020-03-15
            /// </summary>
            public string EndDate { get; set; }

            /// <summary>
            /// Nội dung nhiệm vụ
            /// </summary>
            public string MissionContent { get; set; }


            /// <summary>
            ///  Ghi chú
            /// </summary>
            public string Note { get; set; }

            /// <summary>
            /// Danh sách Nhãn (Tag)
            /// </summary>
            public List<Tags> ListTag { get; set; }

            /// <summary>
            /// Thông báo qua email
            /// </summary>
            public bool IsNotifyEmail { get; set; }

            /// <summary>
            ///  Thông báo qua SMS
            /// </summary>
            public bool IsNotifySMS { get; set; }

            /// <summary>
            /// Trạng thái
            /// 1: Mới tạo
            /// 3: Đã giao
            /// 4: Đã xem và Đang thực hiện ĐgTH
            /// </summary>
            public int StatusID { get; set; }

            /// <summary>
            /// Danh sách file đính kèm
            /// </summary>
            public List<FileAttach> ListFileAttachContent { get; set; }

            public string ToJson() {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { MissionCreator = this });
            }

        }

        private class CreateMissionOutput {
            public int Status { get; set; }

            public object Object { get; set; }

            public bool isOk { get; set; }
            public bool isError { get; set; }
        }

        private class ListCooperation {
            public string UserNameCooperation { get; set; }
        }

        private class Tags {
            public string TagName { get; set; }
        }

        private class FileAttach {
            /// <summary>
            /// GUID của file đính kèm
            /// </summary>
            public string FileName { get; set; }
            public string FileContentBase64 { get; set; }
        }

    }
}