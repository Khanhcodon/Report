using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FileLocationDal - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : DataAccessBase</para>
    /// <para>    * Implement : IFileLocationDal</para>
    /// <para>Create Date : 060313</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : DAL tương ứng với bảng FileLocation trong CSDL</para>
    /// </summary>
    public class FileLocationDal : DataAccessBase, IFileLocationDal
    {
        private readonly IRepository<FileLocation> _fileLocationRepository;
        /// <summary>
        /// Khởi tạo class <see cref="FileLocationDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public FileLocationDal(IDbCustomerContext context)
            : base(context)
        {
            _fileLocationRepository = Context.GetRepository<FileLocation>();
        }

#pragma warning disable 1591

        public IEnumerable<FileLocation> Gets(Expression<Func<FileLocation, bool>> spec)
        {
            return _fileLocationRepository.Find(spec);
        }

        public FileLocation Get(int id)
        {
            return _fileLocationRepository.One(id);
        }

        public void Create(FileLocation entity)
        {
            _fileLocationRepository.Create(entity);
        }

        public void Update(FileLocation entity)
        {
            _fileLocationRepository.Update(entity);
        }

        public void Delete(FileLocation entity)
        {
            _fileLocationRepository.Delete(entity);
        }
    }
}
