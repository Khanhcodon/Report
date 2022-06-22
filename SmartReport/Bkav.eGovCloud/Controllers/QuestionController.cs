using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class QuestionController : CustomerBaseController
    {
        private readonly SendEmailHelper _mailHelper;

        private readonly UserBll _userService;
        private readonly LogBll _logService;
        private readonly ResourceBll _resourceService;
        private readonly QuestionBll _questionService;
        private readonly DocFieldBll _docFieldService;
        private readonly DocumentBll _documentService;
        private readonly PermissionBll _permissioinService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly OnlineRegistrationSettings _onlineRegistrationSettings;
        private readonly EmailSettings _emailSettings;

        /// <summary>
        /// C'tor
        /// </summary>
        public QuestionController(
            SendEmailHelper mailHelper,
            UserBll userService,
            LogBll logService,
            ResourceBll resourceService,
            QuestionBll questionService,
            DocFieldBll docFieldService,
            DocumentBll documentService,
            DocumentCopyBll documentCopyService,
            PermissionBll permissioinService,
            OnlineRegistrationSettings onlineRegistrationSettings,
            EmailSettings emailSettings)
        {
            _mailHelper = mailHelper;
            _userService = userService;
            _logService = logService;
            _resourceService = resourceService;
            _docFieldService = docFieldService;
            _questionService = questionService;
            _documentService = documentService;
            _permissioinService = permissioinService;
            _docCopyService = documentCopyService;

            _onlineRegistrationSettings = onlineRegistrationSettings;
            _emailSettings = emailSettings;
        }

        #region General Methods

        /// <summary>
        /// Lấy danh sách câu hỏi hồ sơ trên cây (theo lĩnh vực)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetNode()
        {
            var documentNode = new List<object>();

            foreach (var docField in _docFieldService.Gets())
            {
                var docNode = new
                {
                    id = docField.DocFieldId,
                    name = docField.DocFieldName,
                    level = docField.Order,
                    status = 1,
                    isGeneral = true,
                };
                documentNode.Add(docNode);
            }

            return Json(new { documentNode }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Lấy toàn bộ câu hỏi
        /// </summary>
        /// <param name="isGetGeneral">Lấy câu hỏi chung - true, câu hỏi hồ sơ - false</param>
        /// <returns></returns>
        public JsonResult GetQuestions(bool? isGetGeneral = null)
        {
            int? userId = null;
            var questionListModel = new List<QuestionModel>();
            var onlineRegistrationSettings = _onlineRegistrationSettings.ToModel();

            if (!isGetGeneral.HasValue)
            {
                isGetGeneral = false;
            }

            if (!onlineRegistrationSettings.HasPermisson(_userService.CurrentUser.UserId))
            {
                userId = User.GetUserId();
            }

            questionListModel = GetsAll(isGetGeneral, userId).ToListModel();

            if (isGetGeneral.HasValue && !isGetGeneral.Value)
            {
                foreach (var item in questionListModel)
                {
                    if (item.DocumentLocalId.HasValue)
                    {
                        // Todo: xem lại chổ này,
                        // Lấy dữ liệu vầy dư thừa rất nhiều
                        // item.CurrentUser = GetUserHolder(_docCopyService.GetCurrentUser(item.DocumentLocalId));
                    }

                    if (item.eGovUserId.HasValue)
                    {
                        item.AnswerHolder = GetUserHolder(_userService.GetFromCache(item.eGovUserId.Value));
                    }

                    item.UserComments = GetUserCommentList(item.Comment);
                }
            }

            return Json(new { questionListModel }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy số lượng toàn bộ câu hỏi
        /// </summary>
        /// <param name="isGetGeneral"></param>
        /// <returns></returns>
        public JsonResult GetTotal(bool? isGetGeneral = null)
        {
            return Json(new { total = Total(isGetGeneral) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Chuyển câu hỏi cho cán bộ trả lời
        /// </summary>
        /// <param name="question"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public JsonResult ForwardQuestion(Question question, int userId)
        //{
        //    //try
        //    //{

        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    throw;
        //    //}
        //    var dbQuestion = _questionService.GetByTag(question.Tag);
        //    if (dbQuestion == null)
        //    {
        //        question.UserId = userId;
        //        _questionService.Create(question);
        //    }
        //    else
        //    {
        //        dbQuestion.UserId = userId;
        //        _questionService.Update(dbQuestion);
        //    }

        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult ForwardQuestion(int questionId, int userId, string comment)
        {
            return Json(new { result = Forward(questionId, userId, comment) });
        }

        /// <summary>
        /// Trả lời câu hỏi
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Answer(int questionId, string answer, bool isActive = true)
        {
            return Json(new { result = AnswerQuestion(questionId, answer, User.GetFullName(), isActive) });
        }

        /// <summary>
        /// Bỏ câu hỏi
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Reject(int questionId, string comment)
        {
            return Json(new { result = Reject(questionId, comment, User.GetFullName()) });
        }

        /// <summary>
        /// Từ chối trả lời (trường hợp không trả lời được, trả lại câu hỏi để quản trị chuyển cho người khác)
        /// </summary>
        /// <param name="questionId">Id câu hỏi</param>
        /// <param name="comment">Ý kiến</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RejectAnswer(int questionId, string comment)
        {
            //var dbQuestion = _questionService.GetById(questionId);
            //var currentUser = _userService.Get(dbQuestion.UserId.Value);
            //if (dbQuestion != null)
            //{
            //    if (currentUser == null)
            //    {
            //        currentUser.Username = "(Unknown)";
            //    }
            //    dbQuestion.Comment
            //            += "["
            //            + DateTime.Now.ToString("(hh:mm dd/MM/yyyy)")
            //            + "-"
            //            + currentUser.Username
            //            + ":"
            //            + comment
            //            + "],";
            //    dbQuestion.UserId = null;
            //    _questionService.Update(dbQuestion);
            //}

            return Json(new { success = NoAnswer(questionId, comment) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lấy ra danh sách cán bộ 1 cửa cần chuyển 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetForwardList()
        {
            //var userList = new List<UserHolder>();
            //var userIdSetting = _onlineRegistrationSettings.UserHasPermission.Replace("[", "").Replace("]", "");
            //var separators = new string[] { "," };
            //foreach (var uId in userIdSetting.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    var userId = int.Parse(uId);
            //    var user = _userService.Get(userId, true);
            //    if (user != null)
            //    {
            //        userList.Add(new UserHolder
            //        {
            //            UserId = userId,
            //            Account = user.Username,
            //            FullAccount = user.UsernameEmailDomain,
            //            FullName = user.FullName,
            //            Department = ""
            //        });
            //    }
            //}

            //return Json(userList, JsonRequestBehavior.AllowGet);
            return null;
        }

        /// <summary>
        /// Lấy danh sách câu hỏi mà cán bộ hiện tại đang giữ theo id hồ sơ
        /// </summary>
        /// <returns></returns>
        public JsonResult GetsHolderList(Guid documentId)
        {
            return Json(GetsHolderQuestion(documentId), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private Api Methods

        /// <summary>
        /// Lấy tổng số câu hỏi chưa trả lời
        /// </summary>
        /// <returns></returns>
        private int Total(bool? isGetGeneral)
        {
            ActiveCert();
            var success = 0;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/GetTotal?isGetGeneral=" + isGetGeneral;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    success = Convert.ToInt32(result);
                }
            }

            return success;
        }

        /// <summary>
        /// Lấy tổng số câu hỏi đã trả lời
        /// </summary>
        /// <returns></returns>
        private int TotalAnswer(bool? isGetGeneral)
        {
            ActiveCert();
            var success = 0;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/GetTotalAnswer?isGetGeneral=" + isGetGeneral;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    success = Convert.ToInt32(result);
                }
            }

            return success;
        }

        /// <summary>
        /// Lấy danh sách câu hỏi chưa trả lời
        /// </summary>
        /// <param name="isGetGeneral"></param>
        /// <returns></returns>
        private List<Question> GetsAll(bool? isGetGeneral, int? userId)
        {
            //ActiveCert();
            var success = new List<Question>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/GetsAll?isGetGeneral=" + isGetGeneral + "&userId=" + userId;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    success = JsonConvert.DeserializeObject<List<Question>>(result);
                }
            }

            return success;
        }

        /// <summary>
        /// Lấy danh sách câu hỏi mà cán bộ hiện tại đang giữ theo id hồ sơ
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        private List<Question> GetsHolderQuestion(Guid documentId)
        {
            // ActiveCert();
            var success = new List<Question>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/GetsHolderQuestion?documentLocalId=" + documentId + "&userId=" + User.GetUserId();
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    success = JsonConvert.DeserializeObject<List<Question>>(result);
                }
            }

            return success;
        }

        /// <summary>
        /// Lấy tổng câu hỏi theo hồ sơ đã trả lời
        /// </summary>
        /// <returns></returns>
        private string GetQuestion(Guid documentId)
        {
            //ActiveCert();
            var success = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/GetByLocalId?documentId=" + documentId;
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    success = response.Content.ReadAsStringAsync().Result as string;
                }
            }

            return success;
        }

        /// <summary>
        /// Gửi câu trả lời
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="username"></param>
        /// <param name="comment"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        private bool AnswerQuestion(int questionId, string answer, string username, bool isActive)
        {
            // ActiveCert();
            var success = false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/Answer";
                var response = client.PostAsJsonAsync(url, new
                {
                    questionId = questionId,
                    answer = answer,
                    username = username,
                    isActive = isActive
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        success = true;
                        if (_emailSettings.IsActivated)
                        {
                            try
                            {
                                var question = JsonConvert.DeserializeObject<Question>(result);

                                _mailHelper.SendAnswerQuestion(question);
                            }
                            catch (Exception ex)
                            {
                                _logService.InsertLog(LogType.Error, ex.Message, ex.StackTrace);
                            }

                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Gửi câu trả lời
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="comment"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool Reject(int questionId, string comment, string username)
        {
            ActiveCert();
            var success = false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/Reject";
                var response = client.PostAsJsonAsync(url, new
                {
                    questionId = questionId,
                    comment = comment,
                    username = username,
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        success = true;
                    }
                    else
                    {
                        var question = JsonConvert.DeserializeObject<Question>(result);
                        if (question != null)
                        {
                            if (_emailSettings.IsActivated)
                            {
                                try
                                {
                                    _mailHelper.SendRejectQuestion(question);
                                }
                                catch (Exception ex)
                                {
                                    _logService.InsertLog(LogType.Error, ex.Message, ex.StackTrace);
                                }
                            }

                            success = true;
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Trả lại câu hỏi cho quản trị do không có câu trả lời
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool NoAnswer(int questionId, string comment)
        {
            ActiveCert();
            var success = false;

            using (var client = new HttpClient())
            {
                var userComment = new UserComment
                {
                    Account = User.GetUserName(),
                    FullName = User.GetFullName(),
                    Comment = comment,
                    CommentDate = DateTime.Now
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/RejectAnswer";
                var response = client.PostAsJsonAsync(url, new
                {
                    questionId = questionId,
                    comment = JsonConvert.SerializeObject(userComment),
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (string.IsNullOrEmpty(result))
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Chuyển câu hỏi co cán bộ cụ thể trả lời
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="userId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private bool Forward(int questionId, int userId, string comment)
        {
            ActiveCert();
            var success = false;

            using (var client = new HttpClient())
            {
                var userComment = new UserComment
                {
                    Account = User.GetUserName(),
                    FullName = User.GetFullName(),
                    Comment = comment,
                    CommentDate = DateTime.Now
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetApiUrl(_onlineRegistrationSettings.ApiUrl) + "/FAQ/SetUserIdReply";
                var response = client.PostAsJsonAsync(url, new
                {
                    questionId = questionId,
                    userId = userId,
                    comment = JsonConvert.SerializeObject(userComment)
                }).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result as string;
                    if (string.IsNullOrEmpty(result))
                    {
                        success = true;
                    }
                }
            }

            return success;
        }


        #endregion

        #region Private General Methods

        private List<UserComment> GetUserCommentList(string comment)
        {
            var result = new List<UserComment>();

            if (!string.IsNullOrWhiteSpace(comment))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<List<UserComment>>("[" + comment.Substring(0, comment.Length - 1) + "]");
                }
                catch (Exception ex)
                {
                    _logService.InsertLog(LogType.Error, ex.Message, ex.StackTrace);
                }
            }

            return result;
        }

        private UserHolder GetUserHolder(User user)
        {
            return user != null ? new UserHolder
            {
                UserId = user.UserId,
                Account = user.Username,
                FullAccount = user.UsernameEmailDomain,
                FullName = user.FirstName
            } : null;
        }

        /// <summary>
        /// Loại bỏ ký tự "/" cuối trên url
        /// </summary>
        /// <param name="apiUrl">url truyền vào</param>
        /// <returns></returns>
        private string GetApiUrl(string apiUrl)
        {
            if (string.IsNullOrWhiteSpace(apiUrl))
                return null;

            if (apiUrl.EndsWith("/", StringComparison.InvariantCultureIgnoreCase))
            {
                apiUrl = apiUrl.Substring(0, apiUrl.LastIndexOf("/"));
            }
            return apiUrl;
        }

        /// <summary>
        /// Alow qua tầng bảo mật của Cert
        /// note: Khi cert của hệ thống bị lỗi thì HttpWebRequest và HttpWebResponse không thao tác được
        /// </summary>
        private void ActiveCert()
        {
            return;
            //ServicePointManager.ServerCertificateValidationCallback =
            //    ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        #endregion
    }
}