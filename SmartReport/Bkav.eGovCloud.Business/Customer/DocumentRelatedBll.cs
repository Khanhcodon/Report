using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Entities.Common;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Business.BI.ParseQuery;
using System.Data;
using System.IO;
using Bkav.eGovCloud.Core.FileSystem;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// </summary>
    public class DocumentRelatedBll : ServiceBase
    {
        private readonly IRepository<DocumentRelated> _docRelatedRepository;

        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileManager _fileManager;
        /// <summary>
        /// Khởi tạo class <see cref="DocumentRelatedBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        //TODO: Anh GiangPN - Tham số đầu vào IPositionDal positionDal nên thay thành PositionBll và IJobTitlesDal jobTitlesDal nên thay thành JobTitleBll
        public DocumentRelatedBll(IDbCustomerContext context,
                    AdminGeneralSettings generalSettings)
            : base(context)
        {
            _docRelatedRepository = Context.GetRepository<DocumentRelated>();
            _generalSettings = generalSettings;
            _fileManager = FileManager.Default;
        }

        #region doc
        /// <summary>
        /// 
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public DocumentRelated Get(int docId)
        {
            DocumentRelated reportQuery = null;
            if (docId > 0)
            {
                reportQuery = _docRelatedRepository.Get(docId);
            }

            return reportQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public void Create(DocumentRelated doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("DocumentRelated");
            }

            _docRelatedRepository.Create(doc);
            Context.SaveChanges();
        }

        public void Update(DocumentRelated doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("DocumentRelated");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public void Delete(DocumentRelated doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("DocumentRelated");
            }
            _docRelatedRepository.Delete(doc);
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<DocumentRelated> Gets(Expression<Func<DocumentRelated, bool>> spec = null)
        {
            return _docRelatedRepository.GetsReadOnly(spec);
        }
        #endregion 
        /// <summary>
        /// Tải tệp mẫu phôi
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="id">Form id</param>
        /// <returns></returns>
        public Stream Download(out string fileName, int id)
        {
            var doc = Get(id);
            if (doc == null)
            {
                throw new EgovException("Không tìm thấy báo cáo");
            }
            return Download(out fileName, doc);
        }


        /// <summary>
        /// Tải tệp mẫu phôi
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="doc">form</param>
        /// <returns></returns>
        public Stream Download(out string fileName, DocumentRelated doc)
        {
            fileName = doc.EmbryonicPath;
            var downloaded = _fileManager.Open(doc.EmbryonicLocationName, ResourceLocation.Default.EmbryonicForm);

            return downloaded;
        }

        /// <summary>
        /// Tải tệp mẫu phôi
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="doc">form</param>
        /// <returns></returns>
        public Dictionary<string, Stream> Downloads(List<int> ids)
        {
            var results = new Dictionary<string, Stream>();
            foreach (var id in ids)
            {
                var fileName = "";
                var down = Download(out fileName, id);

                var name = Path.GetFileNameWithoutExtension(fileName);
                if (!string.IsNullOrEmpty(name))
                {
                    var check = results.Count(x => x.Key.Contains(name));
                    if (check > 0)
                    {
                        var ext = Path.GetExtension(fileName);
                        fileName = $"{name}({check + 1}){ext}";
                    }
                }
                results.Add(fileName,down);
            }
            return results;
        }
       
        /// <summary> TienBV 231012
        /// Lấy ra tất cả các danh mục có sắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<DocumentRelated> GetSorts(string sortBy = "", bool isDescending = false)
        {
            return _docRelatedRepository.GetsReadOnly(null, Context.Filters.CreateSort<DocumentRelated>(isDescending, sortBy));
        }
    }
}
