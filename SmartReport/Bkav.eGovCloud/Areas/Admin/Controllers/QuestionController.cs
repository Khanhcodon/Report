using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Controllers;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: QuestionController - public - Controller.
    /// Create Date: 120814.
    /// Author: TrinhNVd.
    /// Description: Chứa các Action người dùng tương tác.
    /// </summary>
    [EgovAuthorize]
    //[RequireHttps]
    public class QuestionController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly QuestionBll _questionService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DefaultSortBy = "Date";

        /// <summary>TrinhNVd - 170714
        /// Khởi tạo giá trị cho _resourceService, _questionService, _pageSettings
        /// </summary>
        /// <param name="questionService"></param>
        /// <param name="pageSettings"></param>
        /// <param name="resourceService"></param>
        public QuestionController(QuestionBll questionService, AdminGeneralSettings generalSettings, ResourceBll resourceService)
        {
            _resourceService = resourceService;
            _questionService = questionService;
            _generalSettings = generalSettings;
        }

        /// <summary>TrinhNVd - 170714
        /// Hiển thị danh sách hướng dẫn
        /// </summary>
        /// <returns>Giao diện danh sách hướng dẫn có phân trang</returns>
        public ActionResult Index()
        {
            var defaultPageSize = _generalSettings.DefaultPageSize;
            var model = GetPageList(string.Empty, 1, defaultPageSize, DefaultSortBy, true);
            return View(model);
        }

        /// <summary>TrinhNVd - 170714
        /// Hiển thị danh sách hướng dẫn theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="questionName">Tên hướng dẫn cần tìm</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <returns>Giao diện danh sách hướng dẫn với từ khóa tìm kiếm, có phân trang</returns>
        public ActionResult Search(string questionName, int pageSize)
        {
            IEnumerable<QuestionModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var search = string.Empty;
                search = questionName.Trim();
                model = GetPageList(search, 1, pageSize, DefaultSortBy, false);
            }
            return PartialView("_List", model);
        }

        /// <summary>TrinhNVd - 170714
        /// Hiển thị danh sách hướng dẫn theo từ khóa tìm kiếm, trường sắp xếp, có phân trang
        /// </summary>
        /// <param name="questionName">Tên hướng dẫn tìm kiếm</param>
        /// <param name="sortBy">Trường được sắp xếp</param>
        /// <param name="isSortDesc">Sắp xếp từ thấp đến cao: false, ngược lại: true</param>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Kích thước 1 trang</param>
        /// <returns>Giao diện danh sách hướng dẫn theo từ khóa tìm kiếm, trường sắp xếp, có phân trang</returns>
        public ActionResult SortAndPaging(string questionName, string sortBy,
                                            bool isSortDesc, int page, int pageSize)
        {
            var model = GetPageList(questionName, page, pageSize, sortBy, isSortDesc);
            return PartialView("_List", model);
        }

        /// <summary>TrinhNVd - 170714
        /// Trả về danh sách theo điều kiện
        /// </summary>
        /// <param name="search">Tên hướng dẫn tìm kiếm</param>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Kích thước 1 trang</param>
        /// <param name="sortBy">Trường được sắp xếp</param>
        /// <param name="isSortDescending">Sắp xếp từ thấp đến cao: false, ngược lại: true</param>
        /// <returns>Danh sách hướng dẫn theo từ khóa tìm kiếm, trường sắp xếp, có phân trang</returns>
        private IEnumerable<QuestionModel> GetPageList(string search, int page, int pageSize, string sortBy, bool isSortDescending)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDescending,
                SortBy = sortBy
            };
            var totalRecords = 0;
            var model = _questionService.GetQuestions(out totalRecords, currentPage: sortAndPage.CurrentPage,
                                            pageSize: sortAndPage.PageSize, sortBy: sortAndPage.SortBy,
                                            isDescending: sortAndPage.IsSortDescending,
                                            questionName: search).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.QuestionName = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return model;
        }

        /// <summary>
        /// Trả về trang chi tiết câu hỏi theo url
        /// </summary>
        /// <param name="url">Đường dẫn thân thiện được đăng ký</param>
        /// <returns>Giao diện trang hướng dẫn với đường dẫn được click</returns>
        public ActionResult Detail(string tag)
        {
            try
            {
                var question = _questionService.GetByTag(tag);
                return View(question.ToModel());
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            var question = _questionService.GetById(id);
            if (question == null)
            {
                ErrorNotification(_resourceService.GetResource("Common.Question.Message.DeleteBeforeEdit.Exist"));
                return RedirectToAction("Index");
            }
            return View(question.ToModel());
        }

        /// <summary>TrinhNVd - 120814
        /// Cập nhật lại thông tin hỏi đáp vào CSDL
        /// </summary>
        /// <param name="questionModel">Hỏi đáp được chỉnh sửa</param>
        /// <returns>
        /// Success: Thông báo thành công và hiển thị giao diện danh sách hỏi đáp.
        /// UnSuccess: Thông báo thất bại và hiển thị lại gieo diện chỉnh sửa với các thông tin được nhập.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(QuestionModel questionModel)
        {
            if (ModelState.IsValid)
            {
                var question = _questionService.GetById(questionModel.QuestionId);
                if (question == null)
                {
                    ErrorNotification(_resourceService.GetResource("Common.Question.Message.DeleteBeforeEdit.Exist"));
                }
                else
                {
                    question = questionModel.ToEntity(question);
                    try
                    {
                        question.Tag = Help.NameToTag(question.Name);
                        if (_questionService.IsExistTag(question.Tag))
                        {
                            question.Tag += "-" + Guid.NewGuid();
                        }
                        _questionService.Update(question);
                        SuccessNotification(_resourceService.GetResource("Common.Edit.Success"));
                    }
                    catch (Exception ex)
                    {
                        ErrorNotification(ex.Message);
                        return View(questionModel);
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ErrorNotification(_resourceService.GetResource("Common.Edit.Fail"));
            }
            return View(questionModel);
        }

        /// <summary>TrinhNVd - 120814
        /// Trả lời câu hỏi
        /// </summary>
        /// <param name="id">Mã câu hỏi</param>
        /// <returns>Giao diện trả lời câu hỏi</returns>
        public ActionResult Answer(int id)
        {
            var question = _questionService.GetById(id);
            if (question == null)
            {
                ErrorNotification(_resourceService.GetResource("Common.Question.Message.DeleteBeforeEdit.Exist"));
                return RedirectToAction("Index");
            }
            return View(question.ToModel());
        }

        /// <summary>TrinhNVd - 120814
        /// Ghi câu trả lời vào CSDL
        /// </summary>
        /// <param name="questionModel">Câu hỏi với câu trả lời</param>
        /// <returns>
        /// Success: Thông báo thành công và hiển thị giao diện danh sách hỏi đáp.
        /// UnSuccess: Thông báo thất bại và hiển thị lại giao diện trả lời với các thông tin được nhập.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Answer(QuestionModel questionModel)
        {
            if (ModelState.IsValid)
            {
                var question = _questionService.GetById(questionModel.QuestionId);
                if (question == null)
                {
                    ErrorNotification(_resourceService.GetResource("Common.Question.Message.DeleteBeforeEdit.Exist"));
                }
                else
                {
                    question = questionModel.ToEntity(question);
                    try
                    {
                        if (question.Name.Length > 0 && question.Answer.Length > 0)
                        {
                            question.Active = true;
                        }
                        question.Tag = Help.NameToTag(question.Name);
                        if (_questionService.IsExistTag(question.Tag))
                        {
                            question.Tag += "-" + Guid.NewGuid();
                        }
                        question.AnswerPeople = Help.ToTitleName(question.AnswerPeople);
                        _questionService.Update(question);
                        SuccessNotification(_resourceService.GetResource("Common.Answer.Success"));
                    }
                    catch (Exception ex)
                    {
                        ErrorNotification(ex.Message);
                        return View(questionModel);
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ErrorNotification(_resourceService.GetResource("Common.Answer.Fail"));
            }
            return View(questionModel);
        }

        /// <summary>TrinhNVd - 170714
        /// Xóa hướng dẫn theo mã
        /// </summary>
        /// <param name="id">Mã hướng dẫn cần xóa</param>
        /// <returns>
        /// Success: Thông báo thành công và chuyển đến giao diện danh sách hướng dẫn.
        /// UnSuccess: Thông báo thất bại và chuyển đến giao diện danh sách hướng dẫn.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var question = _questionService.GetById(id);
            try
            {
                _questionService.Delete(question);
                SuccessNotification(_resourceService.GetResource("Common.Question.Message.Delete.Success"));
            }
            catch (Exception ex)
            {
                ErrorNotification(ex.Message);
            }
            return RedirectToAction("Index");
        }

        /// <summary>TrinhNVd - 120814
        /// Kích hoạt câu hỏi
        /// </summary>
        /// <param name="questionId">Mã câu hỏi</param>
        /// <returns>Cập nhật thành công trạng thái có được lên trang Client hay k của câu hỏi</returns>
        [HttpPost]
        public ActionResult Active(int questionId)
        {
            var question = _questionService.GetById(questionId);
            question.Active ^= true;
            _questionService.Update(question);
            return Json("");
        }
    }
}
