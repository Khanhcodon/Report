using System;
using System.IO;

namespace Bkav.eGovCloud.Core.FileSystem
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FileManager - public - Core
    /// Access Modifiers: 
    /// Create Date : 040312
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Thư viện quản lý file</para>
    /// (TrungVH@bkav.com - 040312)
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        protected FileManager()
        {
        }

        private static readonly Lazy<FileManager> DefaultInitializer = new Lazy<FileManager>(() => new FileManager(), true);
        private static FileManager _default;

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static FileManager Default
        {
            get { return _default ?? (_default = DefaultInitializer.Value); }
            set { _default = value; }
        } 

        /// <summary>
        /// Lấy ra 1 kiểu trả về từ stream
        /// </summary>
        /// <typeparam name="T">Kiểu trả về</typeparam>
        /// <param name="name">Tên file</param>
        /// <param name="parser">Hàm chuyển đổi</param>
        /// <param name="type">Kiểu file</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <returns></returns>
        public T Read<T>(string name, Func<Stream, T> parser, FileType type, DateTime createdDate, string identityFolder, string basePath = null)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }
            using (var stream = Open(name, type, createdDate, identityFolder, basePath))
            {
                return parser(stream);
            }
        }

        /// <summary>
        /// Lấy ra 1 kiểu trả về từ text reader
        /// </summary>
        /// <typeparam name="T">Kiểu trả về</typeparam>
        /// <param name="name">Tên file</param>
        /// <param name="parser">Hàm chuyển đổi</param>
        /// <param name="type">Kiểu file</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <returns></returns>
        public T Read<T>(string name, Func<TextReader, T> parser, FileType type, DateTime createdDate, string identityFolder, string basePath = null)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }
            using (var stream = OpenRead(name, type, createdDate, identityFolder, basePath))
            {
                return parser(stream);
            }
        }

        /// <summary>
        /// Lấy ra 1 kiểu trả về từ stream
        /// </summary>
        /// <typeparam name="T">Kiểu trả về</typeparam>
        /// <param name="name">Tên file</param>
        /// <param name="parser">Hàm chuyển đổi.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns></returns>
        public T Read<T>(string name, Func<Stream, T> parser, string basePath = null)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }
            using (var stream = Open(name, basePath))
            {
                return parser(stream);
            }
        }

        /// <summary>
        /// Lấy ra 1 kiểu trả về từ text reader
        /// </summary>
        /// <typeparam name="T">Kiểu trả êề</typeparam>
        /// <param name="name">Tên file.</param>
        /// <param name="parser">Hàm chuyển đổi.</param>
        /// <param name="basePath">Đường dẫn cơ sở.</param>
        /// <returns></returns>
        public T Read<T>(string name, Func<TextReader, T> parser, string basePath = null)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }
            using (var stream = OpenRead(name, basePath))
            {
                return parser(stream);
            }
        }

        /// <summary>
        /// Lấy ra chuỗi từ 1 file
        /// </summary>
        /// <param name="name">Tên file</param>
        /// <param name="type">Kiểu file</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <returns></returns>
        public string ReadString(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null)
        {
            return Read(name, r => r.ReadToEnd(), type, createdDate, identityFolder, basePath);
        }

        /// <summary>
        /// Lấy ra chuỗi từ 1 file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở.</param>
        /// <returns></returns>
        public string ReadString(string name, string basePath = null)
        {
            return Read(name, r => r.ReadToEnd(), basePath);
        }

        /// <summary>
        /// Tạo mới file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="threshold">Số lượng file tối đa trong một thư mục</param>
        /// <param name="basePath">Đường dẫn cơ sở.</param>
        /// <param name="name">Tên file (nếu null sẽ tự sinh)</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Thông tin file được tạo</returns>
        public FileInfo Create(Stream stream, FileType type, DateTime? createdDate = null, int threshold = 4096, string basePath = null, string name = null, string extension = null)
        {
            string filePath;
            if(!createdDate.HasValue)
            {
                createdDate = DateTime.Now;
            }
            if(threshold < 1)
            {
                threshold = 4096;
            }
            var ensurePath = DirectoryUtil.CreateDirectoryTime(type, createdDate, threshold, basePath);
            const int chunkSize = 65536; 
            var buffer = new byte[chunkSize];
            if(!string.IsNullOrWhiteSpace(name))
            {
                filePath = EnsureExtensionAndToAbsolute(name, extension, ensurePath.FullName);
                if (!File.Exists(filePath))
                {
                    WriteFile(stream, filePath, buffer, chunkSize);
                    return new FileInfo(filePath);
                }
            }
            do
            {
                filePath = EnsureExtensionAndToAbsolute(Guid.NewGuid().ToString("N"), extension, ensurePath.FullName);
            } while (File.Exists(filePath));

            WriteFile(stream, filePath, buffer, chunkSize);
            return new FileInfo(filePath);
        }

        /// <summary>
        /// Tạo mới file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="basePath">Đường dẫn cơ sở.</param>
        /// <param name="name">Tên file (nếu null sẽ tự sinh)</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Thông tin file được tạo</returns>
        public FileInfo Create(Stream stream, string basePath = null, string name = null, string extension = null)
        {
            string filePath;
            const int chunkSize = 65536;
            var buffer = new byte[chunkSize];
            var ensurePath = DirectoryUtil.EnsureBasePath(basePath, null);

            if (!string.IsNullOrWhiteSpace(name))
            {
                filePath = EnsureExtensionAndToAbsolute(name, extension, ensurePath);
                if (!File.Exists(filePath))
                {
                    WriteFile(stream, filePath, buffer, chunkSize);
                    return new FileInfo(filePath);
                }
            }

            // Tạo file với tên file mới
            do
            {
                filePath = EnsureExtensionAndToAbsolute(Guid.NewGuid().ToString("N"), extension, ensurePath);
            } while (File.Exists(filePath));

            WriteFile(stream, filePath, buffer, chunkSize);
            return new FileInfo(filePath);
        }

        private static void WriteFile(Stream stream, string filePath, byte[] buffer, int chunkSize)
        {
            using (var writeStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                if (stream != null)
                {
                    int bytesRead;
                    do
                    {
                        bytesRead = stream.Read(buffer, 0, chunkSize);
                        writeStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                    writeStream.Flush();
                }
                writeStream.Dispose();
            }
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="editor">Hàm cập nhật file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, Action<TextWriter> editor, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            if (editor == null)
            {
                return;
            }

            using (var writer = OpenWrite(name, type, createdDate, identityFolder, basePath, extension))
            {
                editor(writer);
            }
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="editor">Hàm cập nhật file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, Action<TextWriter> editor, string basePath = null, string extension = null)
        {
            if (editor == null)
            {
                return;
            }

            using (var writer = OpenWrite(name, basePath, extension))
            {
                editor(writer);
            }
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="editor">Hàm cập nhật file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, Action<Stream> editor, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            if (editor == null)
            {
                return;
            }

            using (var stream = Open(name, type, createdDate, identityFolder, basePath, true, extension))
            {
                editor(stream);
            }
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="editor">Hàm cập nhật file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, Action<Stream> editor, string basePath = null, string extension = null)
        {
            if (editor == null)
            {
                return;
            }

            using (var stream = Open(name, basePath, true, extension))
            {
                editor(stream);
            }
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="content">Nội dung file dạng stream.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, Stream content, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            Update(name, content.CopyTo, type, createdDate, identityFolder, basePath, extension);
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="content">Nội dung file dạng stream.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, Stream content, string basePath = null, string extension = null)
        {
            Update(name, content.CopyTo, basePath, extension);
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="content">Nội dung file dạng stream.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, string content, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            Update(name, w => w.Write(content), type, createdDate, identityFolder, basePath, extension);
        }

        /// <summary>
        /// Cập nhật nội dung file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="content">Nội dung file dạng stream.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Update(string name, string content, string basePath = null, string extension = null)
        {
            Update(name, w => w.Write(content), basePath, extension);
        }

        /// <summary>
        /// Lấy ra đường dẫn lưu file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns></returns>
        public string Resolve(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            basePath =
                DirectoryUtil.EnsureBasePath(
                    basePath,
                    Path.Combine(type.ToString(), createdDate.ToString(@"yyyy\\M"), identityFolder));

            return EnsureExtensionAndToAbsolute(name, extension, basePath);
        }

        /// <summary>
        /// Lấy ra đường dẫn lưu file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns></returns>
        public string Resolve(string name, string basePath = null, string extension = null)
        {
            return EnsureExtensionAndToAbsolute(name, extension, basePath);
        }

        /// <summary>
        /// Kiểm tra file đã tồn tại hay chưa
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>True: Đã tồn tại, False: Chưa tồn tại</returns>
        public bool Exist(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            return File.Exists(Resolve(name, type, createdDate, identityFolder, basePath, extension));
        }

        /// <summary>
        /// Kiểm tra file đã tồn tại hay chưa
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>True: Đã tồn tại, False: Chưa tồn tại</returns>
        public bool Exist(string name, string basePath = null, string extension = null)
        {
            return File.Exists(Resolve(name, basePath, extension));
        }

        /// <summary>
        /// Mở một file và trả về Text Reader.
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Text reader</returns>
        public TextReader OpenRead(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            return File.OpenText(Resolve(name, type, createdDate, identityFolder, basePath, extension));
        }

        /// <summary>
        /// Mở một file và trả về Text Reader.
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Text reader</returns>
        public TextReader OpenRead(string name, string basePath = null, string extension = null)
        {
            return File.OpenText(Resolve(name, basePath, extension));
        }

        /// <summary>
        /// Mở một file và ghi đè lên file đó
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Text Writer</returns>
        public TextWriter OpenWrite(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            return new StreamWriter(Open(name, type, createdDate, identityFolder, basePath, true, extension));
        }

        /// <summary>
        /// Mở một file và ghi đè lên file đó
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Text Writer</returns>
        public TextWriter OpenWrite(string name, string basePath = null, string extension = null)
        {
            return new StreamWriter(Open(name, basePath, true, extension));
        }

        /// <summary>
        /// Đọc nội dung file 
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="overWrite">Có ghi đè hay không</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Stream</returns>
        public Stream Open(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null, bool overWrite = false, string extension = null)
        {
            var fileName = Resolve(name, type, createdDate, identityFolder, basePath, extension);
            var fileMode = File.Exists(fileName)
                                ? overWrite
                                        ? FileMode.Create
                                        : FileMode.Open
                                : FileMode.CreateNew;

            return new FileStream(fileName, fileMode, fileMode == FileMode.Create || fileMode == FileMode.CreateNew ? FileAccess.ReadWrite : FileAccess.Read);
        }

        /// <summary>
        /// Đọc nội dung file 
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="overWrite">Có ghi đè hay không</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Stream</returns>
        public Stream Open(string name, string basePath = null, bool overWrite = false, string extension = null)
        {
            var fileName = Resolve(name, basePath, extension);
            var fileMode = File.Exists(fileName)
                                ? overWrite
                                        ? FileMode.Create
                                        : FileMode.Open
                                : FileMode.CreateNew;
            return new FileStream(fileName, fileMode, fileMode == FileMode.Create || fileMode == FileMode.CreateNew ? FileAccess.ReadWrite : FileAccess.Read);
        }

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="type">Kiểu file.</param>
        /// <param name="identityFolder">Tên thư mục tự tăng</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="createdDate">Ngày tạo file</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Delete(string name, FileType type, DateTime createdDate, string identityFolder, string basePath = null, string extension = null)
        {
            File.Delete(Resolve(name, type, createdDate, identityFolder, basePath, extension));
        }

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="name">Tên file.</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="extension">Tên đuôi file</param>
        public void Delete(string name, string basePath = null, string extension = null)
        {
            File.Delete(Resolve(name, basePath, extension));
        }

        #region Core facilities

        private static string EnsureExtensionAndToAbsolute(string inputName, string extension, string basePath)
        {
            if (String.IsNullOrEmpty(inputName))
            {
                throw new ArgumentNullException(inputName);
            }
            var result = FileUtil.EnsureExtension(inputName, extension);
            return FileUtil.ToAbsolute(result, basePath);
        }
        #endregion
    }
}
