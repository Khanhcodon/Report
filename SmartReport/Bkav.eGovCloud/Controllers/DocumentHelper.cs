using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Controllers
{
	/// <summary>
	/// Lớp hỗ trợ xử lý cập nhật tạo mới văn bản
	/// </summary>
	public class DocumentHelper
	{
		private readonly DocumentBll _documentService;
		private readonly DocTypeBll _docTypeService;
		private readonly WorkflowHelper _workflowHelper;
		private readonly UserBll _userService;
		private readonly DocumentPublishBll _docPublishService;
		private readonly DocumentCopyBll _docCopyService;
		private readonly Helper.NotificationHelper _notificationHelper;
		private readonly SendEmailHelper _mailHelper;

		/// <summary>
		/// Khởi tạo
		/// </summary>
		/// <param name="documentService"></param>
		/// <param name="docTypeService"></param>
		/// <param name="workflowHelper"></param>
		/// <param name="userService"></param>
		/// <param name="docCopyService"></param>
		public DocumentHelper(DocumentBll documentService, DocTypeBll docTypeService, WorkflowHelper workflowHelper, UserBll userService,
							DocumentCopyBll docCopyService, Helper.NotificationHelper notificationHelper, SendEmailHelper mailHelper, DocumentPublishBll docPublishService)
		{
			_documentService = documentService;
			_docTypeService = docTypeService;
			_workflowHelper = workflowHelper;
			_userService = userService;
			_docCopyService = docCopyService;
			_docPublishService = docPublishService;
			_notificationHelper = notificationHelper;
			_mailHelper = mailHelper;
		}

		/// <summary>
		///   <para> Tạo văn bản dự thảo. </para>
		///   <para> Khi tạo mới hồ sơ luôn luôn phải thực hiện tạo văn bản dự thảo trước khi thực hiện các thao tác khác. </para>
		/// </summary>
		/// <param name="tempFiles"></param>
		/// <param name="model"></param>
		/// <param name="userSendId"></param>
		/// <returns></returns>
		public DocumentCopy CreateDocumentDefault(DocumentModel model, int userSendId, IDictionary<string, IDictionary<string, string>> newFiles, Node nodeSend = null)
		{
			var startNode = new List<Node>();
			if (nodeSend == null)
			{
				var workflow = _docTypeService.GetWorkflowActive(model.DocTypeId);
				startNode.AddRange(_workflowHelper.GetStartNodes(workflow, userSendId));
			}
			else
			{
				startNode.Add(nodeSend);
			}

			if (!startNode.Any())
			{
				throw new Exception("Không có quyền khởi tạo văn bản.");
			}

			var relations = model.RelationModels.Where(c => c.IsAddNext).Select(c => new DocRelation
			{
				RelationId = c.RelationId,
				RelationCopyId = c.RelationCopyId,
				RelationType = c.RelationType,
				Compendium = c.Compendium,
				CitizenName = c.CitizenName,
				DateArrived = c.DateCreated,
				DocCode = c.DocCode,
				InOutCode = c.InOutCode,
				CategoryName = c.CategoryName
			}).ToList();

			var result = CreateDocument(newFiles, model, startNode.First(), relations, userSendId);
			
			var relationHoiBao = relations.Where(r => r.RelationType == (int)RelationTypes.LienQuanHoiBao);
			if (relationHoiBao != null && relationHoiBao.Any())
			{
				foreach (var relation in relationHoiBao)
				{
					_documentService.CreateDocRelations(new DocRelation
					{
						DocumentId = relation.RelationId,
						DocumentCopyId = relation.RelationCopyId,
						RelationCopyId = result.DocumentCopyId,
						RelationId = result.DocumentId,
						RelationType = relation.RelationType,
						Compendium = result.Document.Compendium,
						DocCode = result.Document.DocCode,
						DateArrived = result.DateCreated
					}, false);

					var docs = _docPublishService.Gets(false, d => d.AddressName == model.Organization
					&& d.DocumentCopyId == relation.RelationCopyId);
					if (docs == null && !docs.Any())
					{
						throw new Exception(" Không tồn tại cơ quan ban hành trong văn bản phát hành đi, đề nghị nhập lại");
					}
					var docPublish = docs.FirstOrDefault();
					_docPublishService.UpdateResponsed(docPublish, result, "Hồi báo tự nhập tay");
				}
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="newFiles"></param>
		/// <param name="removeAttachmentIds"></param>
		/// <param name="modifiedAttachment"></param>
		/// <param name="userSendId"></param>
		/// <param name="hasUpdateDateAppointed"></param>
		/// <param name="hasChangeAttachment"></param>
		/// <returns></returns>
		public DocumentCopy UpdateDocumentDefault(DocumentModel model,
									IDictionary<string, IDictionary<string, string>> newFiles, IEnumerable<int> removeAttachmentIds, IDictionary<int, string> modifiedAttachment,
									int userSendId,
									bool hasUpdateDateAppointed = false, bool hasChangeAttachment = false, bool isSaveDocDraft = false)
		{
			/*
			 Hàm chỉ xử lý cập nhật thông tin trong bảng document và documentcopy trước khi xử lý các tác vụ liên quan như chuyển văn bản, phát hành văn bản.
			 Các nghiệp vụ khác Lưu sổ, Cấp số, đánh lại số không đưa vào đây.
			 */

			var documentCopy = _docCopyService.Get(model.DocumentCopyId);
			if (documentCopy == null)
			{
				throw new Exception("Văn bản không tồn tại, vui lòng reset và thử lại.");
			}
            
			var document = documentCopy.Document;
			var organId = document.OrganizationCode;
			document = model.ToEntity(document);
			if(document.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen)
			{
				// Không cập nhật mã định danh đơn vị gửi của văn bản đến
				document.OrganizationCode = organId;
			}

			if (model.ExpireProcess.HasValue && hasUpdateDateAppointed)
			{
				document.ExpireProcess = model.ExpireProcess;
			}

			if (hasUpdateDateAppointed)
			{
				document.DateAppointed = model.DateAppointed;
			}

			#region Document Content

			var userSend = _userService.GetFromCache(userSendId);
			if (model.Contents != null && model.Contents.Any())
			{
				foreach (var content in model.Contents)
				{
					if (string.IsNullOrWhiteSpace(content))
					{
						continue;
					}

					var documentContent = Json2.ParseAsJs<DocumentContentModel>(content);
					var docContent = document.DocumentContents.SingleOrDefault(
							d => d.DocumentContentId == documentContent.DocumentContentId);

                    //documentContent.Content = RemoveTagScript(documentContent.Content);

                    if (docContent != null)
					{
						if (!documentContent.Content.Equals(docContent.Content))
						{
							int newVersion = 0;

							if (docContent.DocumentContentDetails.Count > 0)
							{
								newVersion = docContent.DocumentContentDetails.Max(c => c.Version) + 1;
							}

							docContent.DocumentContentDetails.Add(new DocumentContentDetail
							{
						        Content = documentContent.Content,
								CreatedByUserId = userSend.UserId,
								CreatedByUserName = userSend.Username,
								CreatedOnDate = DateTime.Now,
								Version = newVersion
							});
							docContent.Version = newVersion;
						}

						docContent.Content = documentContent.Content;
					}
				}
			}

			#endregion

			#region HSMC

			var docFees = model.DocFees != null && model.DocFees.Any() ? model.DocFees.ToListEntity() : new List<DocFee>();
			var docPapers = model.DocPapers != null && model.DocPapers.Any() ? model.DocPapers.ToListEntity() : new List<DocPaper>();
			document.DocFees = null; // cập nhật giấy tờ lệ phí riêng
			document.DocPapers = null;

			#endregion

			#region Văn bản liên quan

			if (model.RelationModels != null && model.RelationModels.Any())
			{
				foreach (var relation in model.RelationModels)
				{
					if (relation.IsNew)
					{
						if (relation.RelationType == (int)RelationTypes.LienQuanHoiBao)
						{
							_documentService.CreateDocRelations(new DocRelation
							{
								DocumentId = relation.RelationId,
								DocumentCopyId = relation.RelationCopyId,
								RelationCopyId = model.DocumentCopyId,
								RelationId = model.DocumentId,
								RelationType = relation.RelationType,
								Compendium = model.Compendium,
								DocCode = model.DocCode,
								DateArrived = model.DateCreated
							}, false);

                            // update các văn bản hồi báo tự nhập tay
                            var docs = _docPublishService.Gets(false, d => d.AddressName == model.Organization 
                            && d.DocumentCopyId == relation.RelationCopyId);
                            if (docs == null && !docs.Any())
                            {
                                throw new Exception(" Không tồn tại cơ quan ban hành trong văn bản phát hành đi, đề nghị nhập lại");
                            }
                            var docPublish = docs.FirstOrDefault();
                            docPublish.DocCodeResponsed = model.DocCode;
                            docPublish.DateResponsed = model.DateArrived;
                            docPublish.IsResponsed = true;
                            docPublish.Note = "Hồi báo tự nhập tay";
                        }

						_documentService.CreateDocRelations(new DocRelation
						{
							DocumentId = model.DocumentId,
							DocumentCopyId = model.DocumentCopyId,
							RelationCopyId = relation.RelationCopyId,
							RelationId = relation.RelationId,
							RelationType = relation.RelationType,
							Compendium = relation.Compendium,
							DocCode = relation.DocCode,
							DateArrived = relation.DateCreated
						}, false);
					}

					if (relation.IsRemoved)
					{
						_documentService.DeleteDocRelations(model.DocumentCopyId, relation.RelationCopyId, hasSaveChanges: false);
					}
				}
			}

			#endregion

			#region Cập nhật Document Copy

			if (model.DocumentCopyModel != null)
			{
				model.DocumentCopyModel.DocumentCopyId = documentCopy.DocumentCopyId;
				model.DocumentCopyModel.UserThongBao += documentCopy.UserThongBao;
				model.DocumentCopyModel.UserNguoiThamGia += documentCopy.UserNguoiThamGia;
				model.DocumentCopyModel.UserNguoiDaXem += documentCopy.UserNguoiDaXem;
				model.DocumentCopyModel.UserGiamSat += documentCopy.UserGiamSat;
				model.DocumentCopyModel.DocumentUsers += documentCopy.DocumentUsers;

				documentCopy = model.DocumentCopyModel.ToEntity(documentCopy);
			}

            #endregion

            // kiểm tra nếu ko phải lưu nháp sẽ thực hiện chuyển trạng thái từ dự thảo sang đang xl
            if (!isSaveDocDraft)
            {
                if (documentCopy.Status == (int)DocumentStatus.DuThao)
                {
                    documentCopy.Status = (int)DocumentStatus.DangXuLy;
                }
            }
            _documentService.Update(document, newFiles, docFees, docPapers, removeAttachmentIds, modifiedAttachment, userSendId, hasChangeAttachment: hasChangeAttachment);

			// TienBV: - Cần đưa trạng thái này ra ngoài ở nghiệp vụ tương ứng (phát hành lại, cập nhật văn bản đã phát hành).
			//		   - Cần xem lại chức năng này do ảnh hưởng đến việc hồi báo
			//var hasChangeDocCode = !string.IsNullOrEmpty(document.DocCode) && document.DocCode != model.DocCode;
			//if (hasChangeDocCode && document.IsTransferPublish.HasValue)
			//{
			//	_documentPublishService.ChangeDocCode(model.DocumentCopyId, document.DocCode);
			//}

			return documentCopy;
		}

		public string GetCommentTransferText(List<CommentTransfer> commentTransfer)
		{
			var result = new StringBuilder();

			if (commentTransfer == null)
			{
				return "";
			}

			foreach (var transfer in commentTransfer)
			{
				if (transfer == null)
				{
					continue;
				}

				if (transfer.Type == "1" || transfer.Type == "0")
				{
					result.AppendLine("Nơi nhận: " + transfer.Label);
				}

				if (transfer.Type == "2")
				{
					result.AppendLine("Đồng gửi: " + transfer.Label);
				}

				if (transfer.Type == "3")
				{
					result.AppendLine("Thông báo: " + transfer.Label);
				}
				if (transfer.Type == "4")
				{
					result.AppendLine("Gửi qua Email: " + transfer.Label);
				}
			}

			return result.ToString();
		}

		#region Notification

		/// <summary>
		/// Đẩy notify message đến người nhận văn bản, hồ sơ: bao gồm người nhận xử lý chính, đồng xử lý, và thông báo
		/// </summary>
		/// <param name="userReceiveds">Danh sách người nhận notify</param>
		/// <param name="documentCopy">Document Copy</param>
		/// <param name="compendium">Trích yếu</param>
		/// <param name="dateCreated">Ngày tạo</param>
		/// <param name="isCreatingDocument">Notify khi tạo document</param>
		/// <param name="isAdmin">Notify khi là admin</param>
		public void PushNotifyMessage(IEnumerable<int> userReceiveds, DocumentCopy documentCopy, string compendium,
										DateTime dateCreated, bool isCreatingDocument = false, bool isAdmin = false)
		{
			var notifyBody = "";
			var document = documentCopy.Document;

#if HoSoMotCuaEdition

			if (document.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
			{
				notifyBody = string.Format("{0} - {1}", document.DocCode, document.CitizenName);
			}

#endif
			if (string.IsNullOrEmpty(notifyBody))
			{
				notifyBody = document.Compendium;
			}

			try
			{
				_notificationHelper.PushNotifyMessage(userReceiveds, documentCopy, notifyBody, dateCreated, isCreatingDocument, isAdmin);
			}
			catch { }
		}

		#endregion

		#region Send Email

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userSendId"></param>
		/// <param name="document"></param>
		/// <param name="userReceivedIds"></param>
		public void SendDocumentMail(Document document, int userSendId, List<int> userReceivedIds)
		{
			var userSend = _userService.GetFromCache(userSendId);
			var userReceives = _userService.GetAllCached().Where(u => userReceivedIds.Contains(u.UserId)).Select(u => u.Email).ToList();
			if (userReceives != null && userReceives.Any())
			{
				_mailHelper.SendTranferDocumentMail(document, userSend, userReceives);
			}
		}

		#endregion

		#region Private

        private string RemoveTagScript(string input)
        {
            input = System.Web.HttpUtility.UrlDecode(input);
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            var output = rRemScript.Replace(input, "");
            output = System.Web.HttpUtility.UrlEncode(output);
            return output;
        }

		private DocumentCopy CreateDocument(IDictionary<string, IDictionary<string, string>> tempFiles, DocumentModel model,
												Node sendNode, List<DocRelation> relations, int userSendId)
		{
			var document = model.ToEntity();
			document.LienThongStatus = "";

			var userCreate = _userService.GetFromCache(userSendId);
			if (userCreate == null)
			{
				throw new Exception("Tài khoản khởi tạo không đúng.");
			}

			if (model.Contents != null && model.Contents.Any())
			{
				// tránh bị nhân content khi chỉ gửi đồng xử lý
				model.DocumentContents = new List<DocumentContentModel>();
				foreach (var content in model.Contents)
				{
					if (string.IsNullOrWhiteSpace(content))
					{
						continue;
					}
					
                    var documentContent = Json2.ParseAsJs<DocumentContent>(content);
					if (string.IsNullOrWhiteSpace(documentContent.Content)) continue;

					//documentContent.Url = "d_" + RandomString(15, false);

					documentContent.DocumentContentDetails.Add(new DocumentContentDetail
					{
						Content = documentContent.Content,
						CreatedByUserId = userSendId,
						CreatedByUserName = userCreate.Username,
						CreatedOnDate = DateTime.Now,
						Version = 1
					});

					documentContent.Version = 1;
					document.DocumentContents.Add(documentContent);
				}
            }
            else
            {
                if (model.DocumentContents.Any())
                {
                    foreach (var item in model.DocumentContents)
                    {
                        DocumentContent documentContent = item.ToEntity();
                        documentContent.DocumentContentDetails.Add(new DocumentContentDetail
                        {
                            Content = documentContent.Content,
                            CreatedByUserId = userSendId,
                            CreatedByUserName = userCreate.Username,
                            CreatedOnDate = DateTime.Now,
                            Version = 1
                        });
                        documentContent.Version = 1;
                        document.DocumentContents.Add(documentContent);
                    }
                }
            }

           
			
			document.UserCreatedId = userSendId;

			if (model.HasDateOverdue && model.DateOverdue.HasValue)
			{
				// Khi tạo mới nếu có đặt hạn xử lý thì xét hạn đó làm hạn tổng cho văn bản luôn.
				document.DateAppointed = model.DateOverdue.Value;
			}
			else if (model.DateAppointed.HasValue)
			{
				document.DateAppointed = model.DateAppointed.Value;
			}

			if (model.AttachmentModels != null)
			{
				document.Attachments = model.AttachmentModels;
			}

			var result = _documentService.Create(document, sendNode, relations, tempFiles, model.DocumentCopyModel.UserThongBao, model.DocumentCopyModel.DocumentCopyParentPath);
			return result;
		}


		private string RandomString(int size, bool lowerCase)
		{
			var builder = new StringBuilder();
			Random random = new Random();
			char ch;
			for (int i = 0; i < size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}
			if (lowerCase)
				return builder.ToString().ToLower();
			return builder.ToString();
		}

		#endregion
	}
}