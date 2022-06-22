using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Newtonsoft.Json.Converters;
using Microsoft.AspNet.SignalR;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.NotificationService.SignalRMessaging;
using Bkav.eGovCloud.Business.Utils;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class ReferendumController : Controller
    {
        private readonly UserBll _userService;
        private readonly VoteBll _voteService;
        private readonly VoteDetailBll _voteDetailService;
        private VoteSettings _voteSettings;
        private NotificationHelper _notificationHelper;
        private TimerJobHelper _timerJobHelper;
        private IHubContext _context;

        public ReferendumController(VoteBll voteService, VoteDetailBll voteDetailService, UserBll userService, VoteSettings voteSetting, NotificationHelper notificationHelper)
        {
            _voteService = voteService;
            _voteDetailService = voteDetailService;
            _userService = userService;
            _voteSettings = voteSetting;
            _notificationHelper = notificationHelper;
            _timerJobHelper = DependencyResolver.Current.GetService<TimerJobHelper>();
            _context = GlobalHost.ConnectionManager.GetHubContext<Hubs>();
        }

        [HttpPost]
        public JsonResult CreateCommentDiff(int voteId, string commentDiff)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var vote = _voteService.Get(currentUserId, voteId);
            if (vote == null)
            {
                return Json(new { content = "Cuộc trưng cầu không tồn tại", error = true }, JsonRequestBehavior.AllowGet);
            }
            if (vote.IsCommentDiff == false)
            {
                return Json(new { content = "Cuộc trưng cầu không cho phép thêm ý kiến mới", error = true }, JsonRequestBehavior.AllowGet);
            }
            if (!IsTimeVote(vote))
            {
                return Json(new { content = "Đã hết thời gian diễn ra cuộc trưng cầu", error = true }, JsonRequestBehavior.AllowGet);
            }

            var voteDetail = new VoteDetail();
            voteDetail.UserIdCreate = currentUserId;
            voteDetail.VoteId = voteId;
            voteDetail.TitleDetail = commentDiff;

            _voteDetailService.Create(voteDetail);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Vote(string vote)
        {
            var currentUserId = _userService.CurrentUser.UserId;

            if (!_voteSettings.ListUserCreateVote().Contains(currentUserId))
            {
                return Json(new { content = "Không có quyền khởi tạo cuộc trưng cầu", error = true }, JsonRequestBehavior.AllowGet);
            }

            if (!string.IsNullOrEmpty(vote))
            {
                var voteObj = JsonConvert.DeserializeObject<Vote>(vote);
                if (!string.IsNullOrEmpty(voteObj.VoteDetailId))
                {
                    voteObj.UserIdCreate = currentUserId;

                    voteObj.UsersView = voteObj.AddUser(voteObj.UsersView, currentUserId);
                    voteObj.TimeBegin = voteObj.TimeBegin.AddHours(7);
                    voteObj.TimeEnd = voteObj.TimeEnd.AddHours(7);
                    _timerJobHelper.RunAll();
                    _voteService.Create(voteObj);

                    var voteDetails = JsonConvert.DeserializeObject<List<VoteDetail>>(voteObj.VoteDetailId);
                    foreach (var voteDetail in voteDetails)
                    {
                        voteDetail.VoteId = voteObj.VoteId;
                        voteDetail.UserIdCreate = currentUserId;
                        _voteDetailService.Create(voteDetail);
                    }

                    _notificationHelper.PushNotifyVote(voteObj);
                    var result = voteObj.ToModel();
                    result.UsernameCreate = _userService.CurrentUser.Username;

                    return Json(new { content = result, error = false }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { content = "Dữ liệu gửi lên không đúng", error = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult VoteUpdate(string voteStr)
        {
            // quyền update khi thực hiện khi mà thời gian update nhỏ hơn thời gian bắt đầu
            var currentUserId = _userService.CurrentUser.UserId;

            if (!_voteSettings.ListUserCreateVote().Contains(currentUserId))
            {
                return Json(new { content = "Không có quyền khởi tạo", error = true }, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrEmpty(voteStr))
            {
                var voteObj = JsonConvert.DeserializeObject<Vote>(voteStr);
                voteObj.UsersView = voteObj.AddUser(voteObj.UsersView, currentUserId);
                voteObj.TimeBegin = voteObj.TimeBegin.AddHours(7);
                voteObj.TimeEnd = voteObj.TimeEnd.AddHours(7);
                var vote = _voteService.Get(currentUserId, voteObj.VoteId);
                if (DateTime.Now < vote.TimeBegin)
                {
                    vote.TimeBegin = voteObj.TimeBegin;
                    vote.TimeEnd = voteObj.TimeEnd;
                    vote.IsPublic = voteObj.IsPublic;
                    vote.Title = voteObj.Title;
                    vote.IsCommentDiff = voteObj.IsCommentDiff;
                    vote.IsMultiSelect = voteObj.IsMultiSelect;
                    _voteDetailService.DeleteAll(vote.VoteId);
                    var voteDetails = JsonConvert.DeserializeObject<List<VoteDetail>>(voteObj.VoteDetailId);
                    foreach (var voteDetail in voteDetails)
                    {
                        voteDetail.VoteId = voteObj.VoteId;
                        voteDetail.UserIdCreate = currentUserId;
                        _voteDetailService.Create(voteDetail);
                    }
                    return Json(new { content = "Sửa thành công", error = false }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { content = "Dữ liệu tải lên không đúng", error = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteVote(int voteId)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            if (!_voteSettings.ListUserCreateVote().Contains(currentUserId))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var vote = _voteService.Get(currentUserId, voteId);
            if (vote != null)
            {
                _voteDetailService.DeleteAll(vote.VoteId);
                _voteService.Delete(vote);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVotes()
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var votes = _voteService.Gets(currentUserId);
            var models = votes.ToListModel();
            var users = _userService.GetCacheAllUsers();
            var result = models.Select(c =>
            {
                c.CurrentUserId = currentUserId;
                c.UsernameCreate = users.Where(u => u.UserId == c.UserIdCreate).FirstOrDefault().Username;
                if (c.TimeBegin < DateTime.Now && c.TimeEnd > DateTime.Now)
                { c.IsNow = true; }
                else
                { c.IsNow = false; };
                c.IsCreate = c.UserIdCreate == currentUserId ? true : false;
                return c;
            }).OrderByDescending(d => d.IsNow);
            //var votesView = _voteService.GetsView(currentUserId);
            //var votesVote = _voteService.GetsVote(currentUserId);
            //if (votesView == null && votesVote == null)
            //{
            //    return Json("[]", JsonRequestBehavior.AllowGet);
            //}
            //if (votesView == null)
            //{
            //    return Json(votesVote.ToListModel().Select(c => { c.IsVote = true; return c; }).ToList(), JsonRequestBehavior.AllowGet);
            //}
            //if (votesVote == null)
            //{
            //    return Json(votesView.ToListModel().Select(c => { c.IsView = true; return c; }).ToList(), JsonRequestBehavior.AllowGet);
            //}
            //var voteAll = votesView.Where(v => votesVote.Any(vv => vv.VoteId == v.VoteId));
            //var listVote = votesVote.Where(v => !voteAll.Any(vv => vv.VoteId == v.VoteId));
            //var listView = votesView.Where(v => !voteAll.Any(vv => vv.VoteId == v.VoteId));

            //var result = new List<VoteModel>();

            //result.AddRange(listView.ToListModel().Select(c => { c.IsView = true; c.CurrentUserId = currentUserId; c.UsernameCreate= ""; return c; }));
            //result.AddRange(listVote.ToListModel().Select(c => { c.IsVote = true; c.CurrentUserId = currentUserId; return c; }));
            //result.AddRange(voteAll.ToListModel().Select(c => { c.IsView = true; c.CurrentUserId = currentUserId; c.IsVote = true; return c; }));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVoteDetail(int voteId)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var vote = _voteService.Get(currentUserId, voteId);
            if (vote == null)
            {
                return Json(vote, JsonRequestBehavior.AllowGet);
            }
            var listOpinion = _voteDetailService.Gets(voteId);
            var model = vote.ToModel();
            var voted = vote.ListUserVoted();
            model.IsView = HasPermisionView(vote, currentUserId);
            model.IsVote = HasPermisionVote(vote, currentUserId);
            model.IsVoted = voted.Contains(currentUserId) ? true : false;

            model.ListOpinion = JsonConvert.SerializeObject(listOpinion, new IsoDateTimeConverter());
            model.CurrentUserId = currentUserId;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVoteDetailReload(int voteId, int voteDetailCount)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var vote = _voteService.Get(currentUserId, voteId);
            if (vote == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var listOpinion = _voteDetailService.Gets(voteId);
            var isDiff = false;
            if (voteDetailCount < listOpinion.Count())
            {
                isDiff = true;
            }
            var list = listOpinion.Select(v => new
            {
                TotalVote = v.ListUserVote().Count,
                VoteDetailId = v.VoteDetailId,
                UserIdsVote = v.UserIdsVote
            });
            return Json(new { List = list, IsDiff = isDiff }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserInfos(string userIds)
        {
            var listId = ParseUserIds(userIds);
            var users = _userService.GetCacheAllUsers();
            var listUser = users.Where(u => listId.Contains(u.UserId)).Select(v => new
            {
                UserId = v.UserId,
                UserName = v.FullName,
            });
            return Json(listUser, JsonRequestBehavior.AllowGet);
        }

        public bool IsWatchNow(Vote vote)
        {
            // Nếu cuộc trưng cầu chưa kết thúc và thời gian kết thuc
            if (!vote.IsViewResultImmediately && vote.TimeEnd > DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public JsonResult IsCreate()
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var result = _voteSettings.ListUserCreateVote().Contains(currentUserId) ? true : false;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckVoteResult(int voteId, string voteDetailIds)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var vote = _voteService.Get(currentUserId, voteId);
            if (vote == null)
            {
                return Json("Không tồn tại cuộc trưng cầu", JsonRequestBehavior.AllowGet);
            }
            if (!HasPermisionVote(vote, currentUserId))
            {
                return Json("Không có quyền thực hiện trưng cầu", JsonRequestBehavior.AllowGet);
            }
            if (!IsTimeVote(vote))
            {
                return Json("Thời gian trưng cầu chưa đến hoặc đã hết thời gian trưng cầu", JsonRequestBehavior.AllowGet);
            }

            var voteDetails = JsonConvert.DeserializeObject<List<int>>(voteDetailIds);
            var listUserVoted = vote.ListUserVoted();

            if (listUserVoted.Contains(currentUserId))
            {
                return Json("Đã thực hiện trưng cầu, chỉ có thể xem", JsonRequestBehavior.AllowGet);
            }

            if (!HasMultiVote(vote, currentUserId))
            {
                if (voteDetails.Count() > 1)
                {
                    return Json("Cuộc trưng cầu không cho phép chọn nhiều đáp án", JsonRequestBehavior.AllowGet);
                }
            }

            foreach (var voteDetailId in voteDetails)
            {
                var voteDetail = _voteDetailService.Get(voteDetailId);
                if (!CheckVoted(voteDetail, currentUserId))
                {
                    continue;
                }

                var voted = voteDetail.ListUserVote();
                voted.Add(currentUserId);
                voteDetail.UserIdsVote = VoteDetail.UserCompareString(voted);
                _voteDetailService.Update(voteDetail);
            }

            listUserVoted.Add(currentUserId);
            vote.UsersVoted = VoteDetail.UserCompareString(listUserVoted);
            _voteService.Update(vote);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckVote(int voteId, string voteDetailIds, bool uncheckVote = false)
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var vote = _voteService.Get(currentUserId, voteId);
            if (vote == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (!HasPermisionVote(vote, currentUserId))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (!IsTimeVote(vote))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var voteDetails = JsonConvert.DeserializeObject<List<int>>(voteDetailIds);
            // Kiểm tra xem có được lựa chọn nhiều đáp án hay không
            if (!HasMultiVote(vote, currentUserId))
            {
                // Nếu không

                foreach (var item in voteDetails)
                {
                    var voteDetail = _voteDetailService.Get(item);
                    var voted = voteDetail.ListUserVote();
                    if (!uncheckVote)
                    {
                        if (CheckVoted(voteDetail, currentUserId))
                        {
                            var list = removeVoted(currentUserId, vote);

                            voted.Add(currentUserId);
                            voteDetail.UserIdsVote = VoteDetail.UserCompareString(voted);
                            _voteDetailService.Update(voteDetail);

                            list.Add(voteDetail);

                            return Json(list, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in voteDetails)
            {
                var voteDetail = _voteDetailService.Get(item);
                if (voteDetail != null)
                {
                    if (voteDetail.VoteId == vote.VoteId)
                    {

                        var votes = voteDetail.ListUserVote();
                        if (uncheckVote)
                        {
                            votes.Remove(currentUserId);
                        }
                        else
                        {
                            if (CheckVoted(voteDetail, currentUserId))
                            {
                                votes.Add(currentUserId);
                            }
                        }
                        voteDetail.UserIdsVote = VoteDetail.UserCompareString(votes);
                        _voteDetailService.Update(voteDetail);
                        var voteDetailModel = voteDetail.ToModel();
                        return Json(voteDetailModel, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var currentUserId = _userService.CurrentUser.UserId;
            var votes = _voteService.Gets(currentUserId);
            var models = votes.ToListModel();
            var users = _userService.GetCacheAllUsers();
            var result = models.Select(c =>
            {
                c.CurrentUserId = currentUserId;
                c.UsernameCreate = users.Where(u => u.UserId == c.UserIdCreate).FirstOrDefault().Username;
                if (c.TimeBegin < DateTime.Now && c.TimeEnd > DateTime.Now)
                { c.IsNow = true; }
                else
                { c.IsNow = false; };
                c.IsCreate = c.UserIdCreate == currentUserId ? true : false;
                return c;
            }).OrderByDescending(d => d.IsNow);
            ViewBag.ListVotes = result;
            return View();
        }

        private bool IsTimeVote(Vote vote)
        {
            return (vote.TimeBegin < DateTime.Now || vote.TimeEnd > DateTime.Now);
        }

        private bool HasPermisionVote(Vote vote, int currentUserId)
        {
            var search = ";" + currentUserId.ToString() + ";";
            if (vote.UsersVote.IndexOf(search) > -1)
            {
                return true;
            }
            return false;
        }

        private bool HasPermisionView(Vote vote, int currentUserId)
        {
            var search = ";" + currentUserId.ToString() + ";";


            if (vote.UsersView.IndexOf(search) > -1)
            {
                return true;
            }
            return false;
        }

        private bool CheckVoted(VoteDetail voteDetail, int currentUserId)
        {
            if (voteDetail.ListUserVote().Any(item => item == currentUserId))
            {
                return false;
            }

            return true;
        }
        
        private List<VoteDetail> removeVoted(int currentUserId, Vote vote)
        {
            var listVoteChecked = getVoteDetailChecked(currentUserId, vote);
            var listVoteDetail = new List<VoteDetail>();
            foreach (var item in listVoteChecked)
            {
                var voteDetail = _voteDetailService.Get(item.VoteDetailId);
                var voted = voteDetail.ListUserVote();
                voted.Remove(currentUserId);
                voteDetail.UserIdsVote = VoteDetail.UserCompareString(voted);

                _voteDetailService.Update(voteDetail);

                listVoteDetail.Add(voteDetail);
            }

            return listVoteDetail;
        }

        private List<VoteDetail> getVoteDetailChecked(int currentUserId, Vote vote)
        {
            var search = ";" + currentUserId.ToString() + ";";
            var voteDetails = _voteDetailService.Gets(vote.VoteId);
            var listVoteDetail = new List<VoteDetail>();
            foreach (var item in voteDetails)
            {
                if (!string.IsNullOrEmpty(item.UserIdsVote))
                {
                    if (item.UserIdsVote.Contains(search))
                    {
                        listVoteDetail.Add(item);
                    }
                }
            }

            return listVoteDetail;
        }

        /// <summary>
        /// Hàm kiểm tra xem có được chọn nhiều đáp án hay không
        /// </summary>
        /// <param name="vote"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        private bool HasMultiVote(Vote vote, int currentUserId)
        {
            return vote.IsMultiSelect;
        }


        private string ConvertUsers(string users)
        {
            users = users.Replace(",", ";").Replace("[", ";").Replace("]", ";");
            return users;
        }

        private List<int> ParseUserIds(string userStr)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(userStr))
            {
                return result;
            }

            var userIds = userStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();

            return result;
        }
    }
}