using System;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using Ionic.Zip;
namespace Bkav.eGovCloud.Core.FileSystem
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DirectoryUtil - public - Core
    /// Access Modifiers: 
    /// Create Date : 090812
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>1 thư viện hỗ trợ cho phép kiểm soát đọc ghi thư mục trên hệ thống</para>
    /// (TrungVH@bkav.com - 090812)
    /// </summary>
    public static class DirectoryUtil
    {
        /// <summary>
        /// Lấy ra đường dẫn tuyệt đối và đảm bảm rằng đường dẫn đó tồn tại
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <returns>Đường dẫn tuyệt đối</returns>
        public static string ToAbsoluteAndEnsureExist(string path)
        {
            var ensurePath = FileUtil.ToAbsolute(path, null);
            if (ensurePath != null && !Directory.Exists(ensurePath))
            {
                Directory.CreateDirectory(ensurePath);
            }
            return ensurePath;
        }

        /// <summary>
        /// Lấy ra đường dẫn cơ sở và đảm bảo rằng đường dẫn đó tồn tại
        /// </summary>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <param name="containingPath">Đường dẫn 2</param>
        /// <returns>Đường dẫn</returns>
        public static string EnsureBasePath(string basePath, string containingPath)
        {
            var absolutePath = Path.Combine(string.IsNullOrEmpty(basePath) ? AppDomain.CurrentDomain.BaseDirectory : basePath,
                                containingPath ?? string.Empty);
            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            return absolutePath;
        }

        /// <summary>
        /// Tạo thư mục có dạng: FileType\Year\Month\IdentityFolder\File
        /// </summary>
        /// <param name="type">Loại file</param>
        /// <param name="date">Ngày tạo</param>
        /// <param name="threshold">Số lượng file tối đa trong 1 thư mục</param>
        /// <param name="basePath">Thư mục gốc</param>
        /// <returns>Thông tin thư mục</returns>
        public static DirectoryInfo CreateDirectoryTime(FileType type, DateTime? date = null, int threshold = 4096, string basePath = null)
        {
            if (date == null)
            {
                date = DateTime.Today;
            }
            if (threshold < 1)
            {
                threshold = 4096;
            }
            basePath = EnsureBasePath(basePath,
                                        Path.Combine(type.ToString(), date.Value.ToString(@"yyyy\\M")));
            var directoryBase = new DirectoryInfo(basePath);
            int index;
            var pathOflastFolder = directoryBase.GetDirectories().Where(d => int.TryParse(d.Name, out index)).Max(d => d.FullName);
            if (string.IsNullOrEmpty(pathOflastFolder))
            {
                return new DirectoryInfo(EnsureBasePath(basePath, "0"));
            }
            var totalFileInLastFolder = Directory.GetFiles(pathOflastFolder).Length;
            if (totalFileInLastFolder < threshold)
            {
                return new DirectoryInfo(pathOflastFolder);
            }
            var lastFolder = new DirectoryInfo(pathOflastFolder);
            var newFolder = (int.Parse(lastFolder.Name) + 1).ToString();

            return new DirectoryInfo(EnsureBasePath(basePath, newFolder));
        }

        /// <summary>
        /// Zip folder.
        /// </summary>
        /// <param name="path">Đường dẫn folder cần Zip.</param>
        /// <param name="outPath">Đường dẫn file zip output.</param>
        public static void Zip(string path, string outPath)
        {
            using (ZipFile zip = new ZipFile(outPath))
            {
                zip.AddDirectory(path);
                zip.Save();
            }
        }

        /// <summary>
        /// Zip danh sách các file và thư mục vào 1 file.
        /// </summary>
        /// <param name="listPath">Danh sách đường dẫn các file và thư mục cần zip.</param>
        /// <param name="outPath">File đầu ra.</param>
        /// <param name="deleteAfterZip">Xóa các file gốc sau khi zip.</param>
        public static void Zip(string[] listPath, string outPath, bool deleteAfterZip = false)
        {
            var tempDir = EnsureBasePath(Path.GetDirectoryName(outPath), Path.GetFileNameWithoutExtension(outPath));
            foreach (var path in listPath)
            {
                if (Path.HasExtension(path))
                {
                    if (File.Exists(path))
                    {
                        File.Copy(path, Path.Combine(tempDir, Path.GetFileName(path)));
                    }
                }
                else
                {
                    if (Directory.Exists(path))
                    {
                        DirectoryCopy(path, Path.Combine(tempDir, new DirectoryInfo(path).Name), true);
                    }
                }
            }

            if (File.Exists(outPath))
            {
                File.Delete(outPath);
            }

            using (ZipFile zip = new ZipFile(outPath))
            {
                zip.AddDirectory(tempDir);
                zip.Save();
            }
            if (deleteAfterZip)
            {
                Directory.Delete(tempDir, true);
                foreach (var path in listPath)
                {
                    if (Path.HasExtension(path))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        if (Directory.Exists(path))
                        {
                            Directory.Delete(path, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Copy thư mục.
        /// </summary>
        /// <param name="sourceDirName">Thư mục cần copy.</param>
        /// <param name="destDirName">Thư mục copy tới.</param>
        /// <param name="copySubDirs">Copy các thư mục con.</param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Trả về tổng dung lượng file trong thư mục
        /// </summary>
        /// <param name="path">Đường dẫn thư mục</param>
        /// <returns></returns>
        public static long GetDirectorySize(string path)
        {
            long result = 0;

            if (!Directory.Exists(path))
            {
                return result;
            }

            try
            {
                try
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        if (File.Exists(file))
                        {
                            var finfo = new FileInfo(file);
                            result += finfo.Length;
                        }
                    }

                    // Tính toán trong các thư mục con
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        result += GetDirectorySize(dir);
                    }
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine("Có lỗi xảy ra khi tính toán dung lượng của thư mục: {0}", e.Message);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Có lỗi xảy ra khi tính toán dung lượng của thư mục: {0}", e.Message);
            }

            return result;
        }

        /// <summary>
        /// Xóa tất cả mọi thứ trong thư mục
        /// </summary>
        /// <param name="path">đường dẫn thư mục</param>
        public static void EmptyFolder(string path)
        {
            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                ToAbsoluteAndEnsureExist(path);
                return;
            }

            directory.Delete(true);
            ToAbsoluteAndEnsureExist(path);
        }
    }
}
