using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Kiểm tra folder có tồn tại hay không
        /// </summary>
        /// <param name="pathFolder"></param>
        /// <returns></returns>
        public static bool IsExistFolder(string pathFolder)
        {
            return Directory.Exists(pathFolder);
        }

        /// <summary>
        /// Sao chép tất cả các tệp trong thư mục
        /// </summary>
        /// <param name="sourceDirName">Đường dãn dolder copy</param>
        /// <param name="destDirName">Đường dẫn chứa folder copy</param>
        /// <param name="copySubDirs">True: copy cả folder con, False: không copy folder con</param>
        /// <param name="overWriteFile"> True : Ghi đè các file trùng nhau; False: bỏ qua khi đã tồn tại file</param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = true, bool overWriteFile = true)
        {
            if (string.IsNullOrEmpty(sourceDirName))
                throw new ArgumentNullException("sourceDirName is not exist.");

            if (string.IsNullOrEmpty(destDirName))
                throw new ArgumentNullException("destDirName is not exist.");

            var dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);

            if (!Directory.Exists(destDirName))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + destDirName);

            var files = dir.GetFiles();
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, overWriteFile);
                }
            }

            if (copySubDirs)
            {
                var dirs = dir.GetDirectories();
                if (dirs != null && dirs.Any())
                {
                    foreach (var subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs, overWriteFile);
                    }
                }
            }
        }

        /// <summary>
        /// Sao chép tệp tới 1 thư mục
        /// </summary>
        /// <param name="pathFile">Tệp cần sao chép</param>
        /// <param name="dirFolder">Thư mục đích lưu tệp sao chép</param>
        /// <param name="overWriteFile">True: ghi đè lên tệp có trung tên đã tồn tại từ trước;,False: bỏ qua khi đã tồn tại tệp trùng nhau</param>
        public static void FileCoppy(string pathFile, string dirFolder, bool overWriteFile = true)
        {
            if (string.IsNullOrEmpty(pathFile))
                throw new ArgumentNullException("pathFile is not exist.");

            if (string.IsNullOrEmpty(dirFolder))
                throw new ArgumentNullException("dirFolder is not exist.");

            if (!System.IO.File.Exists(pathFile))
                throw new FileNotFoundException("File does not exist or could not be found: " + pathFile);

            if (Directory.Exists(dirFolder))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + dirFolder);

            string fileName = Path.GetFileName(pathFile);
            var destFileName = Path.Combine(dirFolder, fileName);
            System.IO.File.Copy(pathFile, destFileName, overWriteFile);
        }

        /// <summary>
        /// Tạo file định dạng *.zip  cả folder
        /// </summary>
        /// <param name="sourceDirName">Thư mục chứa các tệp cần tạo file zip</param>
        /// <param name="destDirName">Thư mục lưu trữ file zip sau khi tạo</param>
        /// <param name="fileName">Tên file khi tạo</param>
        /// <param name="password">Mật khẩu mã hóa file zip khi tạo</param>
        /// <param name="encryptType">Kiểu mã hóa, giải mã mật khẩu</param>
        public static void CreateZipFolder(
            string sourceDirName,
            string destDirName,
            string fileName,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256)
        {
            if (string.IsNullOrEmpty(sourceDirName))
                throw new ArgumentNullException("sourceDirName is not exist.");

            if (string.IsNullOrEmpty(destDirName))
                throw new ArgumentNullException("destDirName is not exist.");

            if (!Directory.Exists(sourceDirName))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);

            if (!Directory.Exists(destDirName))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + destDirName);

            var zip = new ZipFile(Encoding.UTF8);
            if (!string.IsNullOrWhiteSpace(password))
            {
                zip.Password = password;
                zip.Encryption = encryptType;
            }

            zip.AddDirectory(sourceDirName, "");
            var path = Path.Combine(destDirName, fileName);
            zip.Save(path);
            zip.Dispose();
        }

        /// <summary>
        /// Tạo file zip 
        /// </summary>
        /// <param name="pathFile">đường dẫn file đích</param>
        /// <param name="inputs">Dữ liệu đầu vào để tạo file</param>
        /// <param name="password">Mật khẩu</param>
        /// <param name="encryptType">Loại mã hóa</param>
        public static void CreateZip(
            string pathFile,
            Dictionary<string, Stream> inputs,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256)
        {
            if (inputs == null)
                throw new ArgumentNullException("streams is not exist.");

            using (var zip = new ZipFile(Encoding.UTF8))
            {
                if (!string.IsNullOrWhiteSpace(password))
                {
                    zip.Password = password;
                    zip.Encryption = encryptType;
                }

                foreach (var item in inputs)
                {
                    zip.AddEntry(item.Key, item.Value);
                }

                zip.Save(pathFile);
                zip.Dispose();
            }
        }

        /// <summary>
        /// Tạo file zip 
        /// </summary>
        /// <param name="inputs">Dữ liệu đầu vào để tạo file</param>
        /// <param name="password">Mật khẩu</param>
        /// <param name="encryptType">Loại mã hóa</param>
        /// <param name="streamFile"></param>
        public static void CreateZip(
            out Stream streamFile,
            Dictionary<string, Stream> inputs,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256)
        {
            if (inputs == null)
                throw new ArgumentNullException("streams is not exist.");

            using (var zip = new ZipFile(Encoding.UTF8))
            {
                streamFile = new MemoryStream();
                if (!string.IsNullOrWhiteSpace(password))
                {
                    zip.Password = password;
                    zip.Encryption = encryptType;
                }

                foreach (var item in inputs)
                {
                    zip.AddEntry(item.Key, item.Value);
                }

                zip.Save(streamFile);
                zip.Dispose();
            }
        }

        /// <summary>
        /// Tạo file định dạng *.zip 
        /// </summary>
        /// <param name="filePath">đường dẫn file them vào file zip</param>
        /// <param name="destDirName">Thư mục lưu trữ file zip sau khi tạo</param>
        /// <param name="fileName">Tên file khi tạo</param>
        /// <param name="deleteSource"></param>
        /// <param name="password">Mật khẩu mã hóa file zip khi tạo</param>
        /// <param name="encryptType">Kiểu mã hóa, giải mã mật khẩu</param>
        public static void CreateZipFile(string filePath,
            string destDirName,
            string fileName,
            bool deleteSource = false,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath is not exist.");

            if (string.IsNullOrEmpty(destDirName))
                throw new ArgumentNullException("destDirName is not exist.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Source file does not exist or could not be found: " + filePath);

            if (!Directory.Exists(destDirName))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + destDirName);

            var zip = new ZipFile(Encoding.UTF8);
            if (!string.IsNullOrWhiteSpace(password))
            {
                zip.Password = password;
                zip.Encryption = encryptType;
            }

            zip.AddFile(filePath, "");
            var path = Path.Combine(destDirName, fileName);
            zip.Save(path);
            zip.Dispose();

            if (deleteSource)
                File.Delete(filePath);
        }

        /// <summary>
        /// Tạo file định dạng *.zip 
        /// </summary>
        /// <param name="filePaths">đường dẫn file them vào file zip</param>
        /// <param name="destDirName">Thư mục lưu trữ file zip sau khi tạo</param>
        /// <param name="fileName">Tên file khi tạo</param>
        /// <param name="continueError"></param>
        /// <param name="deleteSource"></param>
        /// <param name="password">Mật khẩu mã hóa file zip khi tạo</param>
        /// <param name="encryptType">Kiểu mã hóa, giải mã mật khẩu</param>
        public static void CreateZipFile(List<string> filePaths,
            string destDirName,
            string fileName,
            bool continueError = true,
            bool deleteSource = false,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256)
        {
            if (filePaths == null || !filePaths.Any())
                throw new ArgumentNullException("filePath is not exist.");

            if (string.IsNullOrEmpty(destDirName))
                throw new ArgumentNullException("destDirName is not exist.");

            if (!Directory.Exists(destDirName))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + destDirName);

            var exists = new List<string>();
            foreach (var filePath in filePaths)
            {
                if (!File.Exists(filePath))
                {
                    if (continueError)
                        continue;

                    throw new FileNotFoundException("Source file does not exist or could not be found: " + filePath);
                }

                exists.Add(filePath);
            }

            if (exists == null || !exists.Any())
                throw new ArgumentNullException("filePath is not exist.");

            var zip = new ZipFile(Encoding.UTF8);
            if (!string.IsNullOrWhiteSpace(password))
            {
                zip.Password = password;
                zip.Encryption = encryptType;
            }

            foreach (var filePath in exists)
            {
                zip.AddFile(filePath, "");
            }

            var path = Path.Combine(destDirName, fileName);
            zip.Save(path);
            zip.Dispose();

            if (deleteSource)
            {
                foreach (var filePath in exists)
                {
                    File.Delete(filePath);
                }
            }
        }

        /// <summary>
        /// Giải nén file zip
        /// </summary>
        /// <param name="zipFilePath">File  cần giải nén</param>
        /// <param name="dirToUnzipTo">Thư mục lưu trữ file sau khi giải nén</param>
        /// <param name="password">Mật khảu giải nén file zip</param>
        /// <param name="encryptType">Kiểu mã hóa, giải mã mật khẩu</param>
        /// <param name="action">Hành động khi giải nén </param>
        /// <param name="deleteSource"></param>
        public static void UnZip(string zipFilePath,
            string dirToUnzipTo,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256,
            ExtractExistingFileAction action = ExtractExistingFileAction.OverwriteSilently,
            bool deleteSource = true)
        {
            var zip = ZipFile.Read(zipFilePath, new ReadOptions { Encoding = Encoding.UTF8 });
            Unzip(zip, dirToUnzipTo, password, encryptType, action);

            if (deleteSource)
                File.Delete(zipFilePath);
        }

        /// <summary>
        /// Giải nén file zip
        /// </summary>
        /// <param name="stream">stream </param>
        /// <param name="dirToUnzipTo">Thư mục lưu trữ file sau khi giải nén</param>
        /// <param name="password">Mật khảu giải nén file zip</param>
        /// <param name="encryptType">Kiểu mã hóa, giải mã mật khẩu</param>
        /// <param name="action">Hành động khi giải nén </param>
        public static void UnZip(Stream stream,
            string dirToUnzipTo,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256,
            ExtractExistingFileAction action = ExtractExistingFileAction.OverwriteSilently)
        {
            var zip = ZipFile.Read(new ZipInputStream(stream), new ReadOptions { Encoding = Encoding.UTF8 });
            Unzip(zip, dirToUnzipTo, password, encryptType, action);
        }

        /// <summary>
        /// Kiểm tra tệp xem có phải định dạng đuôi là zip hay không
        /// </summary>
        /// <param name="fileName">Đường dẫn tệp</param>
        /// <param name="testExtract">True: có mở để test , Flase: không mở test</param>
        /// <returns></returns>
        public static bool IsFileZip(string fileName, bool testExtract = false)
        {
            return ZipFile.IsZipFile(fileName, testExtract);
        }

        /// <summary>
        /// Kiểm tra luông dữ liệu có phải là là file zip hay không
        /// </summary>
        /// <param name="stream">Luồng dữ liệu</param>
        /// <param name="testExtract">True: có mở để test , Flase: không mở test</param>
        /// <returns></returns>
        public static bool IsFileZip(Stream stream, bool testExtract = false)
        {
            return ZipFile.IsZipFile(stream, testExtract);
        }

        /// <summary>
        /// Giải nén file zip
        /// </summary>
        /// <param name="zip">zip </param>
        /// <param name="dirToUnzipTo">Thư mục lưu trữ file sau khi giải nén</param>
        /// <param name="encryptType">Kiểu mã hóa, giải mã mật khẩu</param>
        /// <param name="password">Mật khẩu</param>
        /// <param name="action">Hành động khi giải nén </param>
        private static void Unzip(
            ZipFile zip,
            string dirToUnzipTo,
            string password = null,
            EncryptionAlgorithm encryptType = EncryptionAlgorithm.WinZipAes256,
            ExtractExistingFileAction action = ExtractExistingFileAction.OverwriteSilently)
        {
            if (zip == null)
                throw new ArgumentNullException("zip is nul or empty.");

            if (string.IsNullOrEmpty(dirToUnzipTo))
                throw new ArgumentNullException("dirToUnzipTo is not exist.");

            if (!Directory.Exists(dirToUnzipTo))
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + dirToUnzipTo);

            zip.ToList().ForEach(entry =>
            {
                if (string.IsNullOrEmpty(password))
                {
                    entry.Extract(dirToUnzipTo);
                }
                else
                {
                    entry.Encryption = encryptType;
                    entry.ExtractWithPassword(dirToUnzipTo, action, password);
                }
            });

            zip.Dispose();
        }
    }
}