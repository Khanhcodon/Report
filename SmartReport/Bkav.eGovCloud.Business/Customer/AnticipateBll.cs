using System;
using System.Linq;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : AnticipateBll - public - BLL</para>
	/// <para>Access Modifiers:</para> 
	/// <para>Create Date : 180214</para>
	/// <para>Author      : TrungVH</para>
	/// <para>Description : BLL tương ứng với bảng Anticipate trong CSDL</para>
	/// </summary>
	public class AnticipateBll : ServiceBase
	{
		private readonly IRepository<Anticipate> _anticipateRepository;

		/// <summary>
		/// Khởi tạo
		/// </summary>
		public AnticipateBll(IDbCustomerContext context)
			: base(context)
		{
			_anticipateRepository = Context.GetRepository<Anticipate>();
		}

		/// <summary>
		/// Lấy ra dự kiến chuyển id
		/// </summary>
		/// <param name="id">Id của dự kiến chuyển</param>
		/// <returns>Entity dự kiến chuyển</returns>
		public Anticipate Get(int id)
		{
			Anticipate anticipate = null;
			if (id > 0)
			{
				anticipate = _anticipateRepository.Get(id);
			}
			return anticipate;
		}

		/// <summary>
		/// Lấy ra dự kiến chuyển id người nhận dự kiến và documentcopyid
		/// </summary>
		/// <param name="userId">Id của người nhận dự kiến chuyển</param>
		/// <param name="documentCopyId">documentCopyId</param>
		/// <param name="type">Kiểu dự kiến</param>
		/// <returns>Entity dự kiến chuyển</returns>
		public Anticipate Get(int userId, int documentCopyId, AnticipateType type)
		{
			return _anticipateRepository.Get(false,
				a => a.UserId == userId && a.DocumentCopyId == documentCopyId && a.AnticipateType == (byte)type);
		}

		/// <summary>
		/// Lấy ra dự kiến chuyển id người nhận dự kiến và documentcopyid
		/// </summary>
		/// <param name="documentCopyId">documentCopyId</param>
		/// <param name="type">Kiểu dự kiến</param>
		/// <returns>Entity dự kiến chuyển</returns>
		public Anticipate Get(int documentCopyId, AnticipateType type)
		{
			return _anticipateRepository.Get(false,
				a => a.DocumentCopyId == documentCopyId && a.AnticipateType == (byte)type);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentId"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public Anticipate Get(Guid documentId, AnticipateType type)
		{
			return _anticipateRepository.Get(false,
						 a => a.DocumentId == documentId && a.AnticipateType == (byte)type);
		}

		/// <summary>
		/// Tạo mới dự kiến chuyển, nếu đã tồn tại thì sẽ cập nhật lại destination
		/// </summary>
		/// <param name="anticipate">Entity</param>
		/// <param name="isSaveChanges">Có gọi SaveChanges luôn trong hàm hay không?</param>
		public int Create(Anticipate anticipate, bool isSaveChanges = true)
		{
			if (anticipate == null)
			{
				throw new ArgumentNullException("anticipate");
			}

			Anticipate exist = anticipate.DocumentId.Equals(Guid.Empty)? null : Get(anticipate.DocumentId, AnticipateType.PhatHanh);
			if (exist != null)
			{
				exist.Destination = anticipate.Destination;
				exist.UserId = anticipate.UserId;
			}
			else
			{
				_anticipateRepository.Create(anticipate);
			}

			if (isSaveChanges)
			{
				Context.SaveChanges();
			}
			
			var result = _anticipateRepository.GetsReadOnly(a => a.UserId == anticipate.UserId && a.AnticipateType == (int)anticipate.AnticipateTypeInEnum).Last().AnticipateId;
			return result;
		}

		/// <summary>
		/// Tạo mới dự kiến chuyển
		/// </summary>
		/// <param name="anticipateId">Id dự kiến chuyển</param>
		/// <param name="destination">Dự kiến chuyển</param>
		public void Update(int anticipateId, string destination)
		{
			var anticipate = Get(anticipateId);
			if (anticipate == null)
			{
				throw new Exception("Không tìm thấy dự kiến chuyển với id là " + anticipateId);
			}
			anticipate.Destination = destination;
			Context.SaveChanges();
		}

		/// <summary>
		/// Tạo mới dự kiến chuyển
		/// </summary>
		/// <param name="anticipate">Entity</param>
		public void Update(Anticipate anticipate)
		{
			if (anticipate == null)
			{
				throw new ArgumentNullException("anticipate");
			}
			Context.SaveChanges();
		}

		/// <summary>
		/// Set plan to document
		/// </summary>
		/// <param name="planId"></param>
		/// <param name="documentId"></param>
		/// <param name="userId"></param>
		public void Update(int planId, Guid documentId, int userId)
		{
			var publishPlan = Get(documentId, AnticipateType.PhatHanh);
			if(publishPlan == null)
			{
				publishPlan = Get(planId);
			}

			if (publishPlan != null)
			{
				var otherPlan = _anticipateRepository.Get(false, p => p.DocumentId == documentId && p.UserId == userId);
				if (otherPlan != null)
				{
					otherPlan.DocumentId = Guid.Empty;
					otherPlan.DocumentCopyId = 0;
				}

				publishPlan.DocumentId = documentId;
			}

			Context.SaveChanges();
		}
	}
}
