using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
//using iTextSharp;
//using iTextSharp.text.pdf;

namespace Bkav.eGovCloud.Controllers
{
    //[EgovAuthorize]
    //[RequireHttps]
    public class AttachmentController : AsyncController
    {
        private readonly FileUploadSettings _fileUploadSettings;
        private readonly AttachmentBll _attachmentService;
        private readonly SignatureBll _signatureService;
        private readonly ReportBll _reportService;
        private readonly FileBll _fileService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentBll _documentService;
        private readonly FormBll _formService;
        private readonly UserBll _userService;

        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly DocumentRelatedBll _docRelatedService;
        public AttachmentController(
            AttachmentBll attachmentService,
            FileUploadSettings fileUploadSettings,
            ReportBll reportService,
            SignatureBll signatureService,
            FileBll fileService,
            DocumentCopyBll documentCopyService,
            DocumentBll documentService,
            FormBll formService,
            UserBll userService,
            DocumentPermissionHelper documentPermissionHelper,
            DocumentRelatedBll docRelated)
        {
            _attachmentService = attachmentService;
            _fileUploadSettings = fileUploadSettings;
            _reportService = reportService;
            _signatureService = signatureService;
            _fileService = fileService;
            _docCopyService = documentCopyService;
            _documentService = documentService;
            _formService = formService;
            _userService = userService;
            _docRelatedService = docRelated;
            _documentPermissionHelper = documentPermissionHelper;
        }

        #region Download/Upload File Crystall

        [HttpPost]
        public void UploadCrystalAsync(IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            UploadCommon(files, ResourceLocation.Default.CrystalReport, true);
        }

        public JsonResult UploadCrystalCompleted(dynamic data)
        {
            return Json(data);
        }

        public void DownloadReportFileBase64Async(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _reportService.Download(out fileName, id);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadReportFileBase64Completed(Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        #endregion

        #region Download/Upload Mẫu phôi

        [HttpPost]
        public void UploadEmbryonicAsync(IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            UploadCommon(files, ResourceLocation.Default.EmbryonicForm, true);
        }

        public JsonResult UploadEmbryonicCompleted(dynamic data)
        {
            return Json(data);
        }

        public void DownloadEmbryonicFileBase64Async(Guid id)
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _formService.Download(out fileName, id);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadEmbryonicFileBase64Completed(Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        #endregion

        #region download docRelated
        public void DownloadEmbryonicFileAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var fileName = string.Empty;
                var file = _docRelatedService.Download(out fileName, id);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadEmbryonicFileCompleted(Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public void DownloadEmbryonicFileAllAsync(string id)
        {
            var arr = string.IsNullOrEmpty(id) ? new List<int>() : id.Split(('-')).Select(int.Parse).ToList();
            var userId = CurrentUserId();
            var userName = User.GetUserName();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var format = DateTime.Now.ToString("yyyyMMddHHmmss");
                var rootPath = Server.MapPath("~/Temp/");
                var inPathFile = Path.Combine(rootPath, userName + "_" + format) + ".zip";
                var files = _docRelatedService.Downloads(arr);
                Business.Common.FileHelper.CreateZip(inPathFile, files);

                #region  Xử lý lây luồng của file zip vừa tạo và xóa file zip

                byte[] result = null;
                using (var stream = System.IO.File.OpenRead(inPathFile))
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        int count = 0;
                        var leng = stream.Length;
                        do
                        {
                            byte[] buf = new byte[leng];
                            count = stream.Read(buf, 0, (int)leng);
                            ms.Write(buf, 0, count);
                        } while (stream.CanRead && count > 0);
                        result = ms.ToArray();
                    }
                }

                System.IO.File.Delete(inPathFile);

                #endregion

                return new { file = result, fileName = format + ".zip" };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }

                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadEmbryonicFileAllCompleted(byte[] file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion

        #region Download/Upload file đính kèm

        [HttpPost]
        public void UploadTempAsync(IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            UploadCommon(files, tempPath);
        }

        public JsonResult UploadTempCompleted(dynamic data)
        {
            return Json(data);
        }

        public void UploadTempXMLAsync(IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            var tempPath = ResourceLocation.Default.FileXML;
            UploadXMLCommon(files, tempPath);
        }

        public JsonResult UploadTempXMLCompleted(dynamic data)
        {
            return Json(data);
        }

        public void DownloadAttachmentAsync(int id, int? storePrivateId)
        {
            var userId = CurrentUserId();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _attachmentService.DownloadAttachment(out fileName, id, storePrivateId, null, userId);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadAttachmentCompleted(Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }

            var encoding = System.Text.Encoding.UTF8;
            Response.Clear();
            Response.ClearHeaders();
            Response.Charset = encoding.WebName;
            Response.HeaderEncoding = encoding;

            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", (Request.Browser.Browser == "IE") ? HttpUtility.UrlEncode(fileName, encoding) : fileName));

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        public void DownloadAttachmentsAsync(Guid id, int? storePrivateId)
        {
            var userId = CurrentUserId();
            var userName = User.GetUserName();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var format = DateTime.Now.ToString("yyyyMMddHHmmss");
                var rootPath = Server.MapPath("~/Temp/");
                var inPathFile = Path.Combine(rootPath, userName + "_" + format) + ".zip";
                var files = _attachmentService.DownloadAttachments(id, userId, storePrivateId, null);
                Bkav.eGovCloud.Business.Common.FileHelper.CreateZip(inPathFile, files);

                #region  Xử lý lây luồng của file zip vừa tạo và xóa file zip

                byte[] result = null;
                using (var stream = System.IO.File.OpenRead(inPathFile))
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        int count = 0;
                        var leng = stream.Length;
                        do
                        {
                            byte[] buf = new byte[leng];
                            count = stream.Read(buf, 0, (int)leng);
                            ms.Write(buf, 0, count);
                        } while (stream.CanRead && count > 0);
                        result = ms.ToArray();
                    }
                }

                System.IO.File.Delete(inPathFile);

                #endregion

                return new { file = result, fileName = format + ".zip" };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }

                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadAttachmentsCompleted(byte[] file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public void DownloadAttachmentBase64Async(int id, int? storePrivateId, int? version)
        {
            var userId = CurrentUserId();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _attachmentService.DownloadAttachment(out fileName, id, storePrivateId, version, userId);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["id"] = id;
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                    AsyncManager.Parameters["version"] = version;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public string DownloadAttachmentBase64Completed(int id, Stream file, string fileName, int? version, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return (new { error = error.InnerException.Message }).StringifyJs();
                }
                throw error.Flatten();
            }
            //resultStream = new CryptoStream(file, new ToBase64Transform(), CryptoStreamMode.Read);
            //Response.AppendHeader("Content-Transfer-Encoding", "base64");
            fileName = id + (version.HasValue ? "_" + version : "") + Path.GetExtension(fileName);

            return (new { fileName, content = file.ToBase64String() }).StringifyJs();
        }

        public void DownloadAttachmentTempBase64Async(string id)
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var file = FileManager.Default.Open(id, ResourceLocation.Default.FileUploadTemp);
                return new { file };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public string DownloadAttachmentTempBase64Completed(Stream file, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return (new { error = error.InnerException.Message }).StringifyJs();
                }
                throw error.Flatten();
            }

            return (new { content = file.ToBase64String() }).StringifyJs();
        }

        public void DownloadAttachmentForSignBase64Async(List<string> fileTempIds, List<int> fileIds, bool convertWordTopdf = false)
        {
            var userId = CurrentUserId();
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                var files = fileIds == null ? null : _attachmentService.DownloadAttachment1(fileIds, userId, convertWordTopdf);
                Dictionary<string, string> filesTemp = null;
                if (fileTempIds != null)
                {
                    filesTemp = new Dictionary<string, string>();
                    if (!convertWordTopdf)
                    {
                        foreach (var fileTempId in fileTempIds)
                        {
                            var file = FileManager.Default.Open(fileTempId, ResourceLocation.Default.FileUploadTemp);
                            if (file != null)
                            {
                                filesTemp.Add(fileTempId, file.ToBase64String());
                            }
                        }
                    }
                    else
                    {
                        foreach (var fileTempId in fileTempIds)
                        {
                            var ext = Path.GetExtension(fileTempId);
                            Stream file = null;
                            var realName = fileTempId;
                            if (string.IsNullOrEmpty(ext))
                            {
                                file = FileManager.Default.Open(fileTempId, ResourceLocation.Default.FileUploadTemp);
                            }
                            else
                            {
                                if (ext == ".doc" || ext == ".docx")
                                {
                                    var pdfTemp = Path.Combine(ResourceLocation.Default.FileTemp,
                        string.Format("{0}.pdf", fileTempId));
                                    realName = fileTempId.Replace(ext, "");
                                    if (!_attachmentService.ConvertWordToPdf(Path.Combine(ResourceLocation.Default.FileUploadTemp,
                                        realName), pdfTemp))
                                    {
                                        continue;
                                    }
                                    file = FileManager.Default.Open(pdfTemp);
                                }
                            }
                            if (file != null)
                            {
                                filesTemp.Add(realName, file.ToBase64String());
                            }
                        }
                    }
                }
                return new { files, filesTemp };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["files"] = t.Result.files;
                    AsyncManager.Parameters["filesTemp"] = t.Result.filesTemp;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public string DownloadAttachmentForSignBase64Completed(Dictionary<string, string> files, Dictionary<string, string> filesTemp, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return (new { error = error.InnerException.Message }).StringifyJs();
                }
                throw error.Flatten();
            }
            Dictionary<string, string> result;
            if (files == null)
            {
                result = filesTemp;
            }
            else
            {
                result = filesTemp == null ? files : files.Concat(filesTemp).ToDictionary(f => f.Key, f => f.Value);
            }

            return (new { signatureConfig = false, files = result }).StringifyJs();
        }

        [HttpPost]
        public void UploadAttachDaPhatHanhOrKetThucAsync(int documentCopyId, IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            var documentCopy = _docCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                throw new Exception("Văn bản không tồn tại");
            }

            var userId = CurrentUserId();
            var document = documentCopy.Document;
            if (document == null)
            {
                throw new Exception("Văn bản không tồn tại");
            }

            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, userId, out userSendId))
            {
                throw new Exception("Không có quyền xử lý văn bản này.");
            }

            var task =
               files.Select(f => Task.Factory.StartNew(() =>
               {
                   var length = f.InputStream.Length;
                   if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
                   {
                       return
                           new
                           {
                               key = "",
                               size = length,
                               name = f.FileName,
                               extension = "",
                               error = "Tệp đính kèm quá dung lượng quy định"
                           };
                   }
                   var ext = Path.GetExtension(f.FileName);

                   if (!string.IsNullOrEmpty(ext) && !_fileUploadSettings.FileUploadAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
                   {
                       return
                           new
                           {
                               key = "",
                               size = length,
                               name = f.FileName,
                               extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                               error = "Loại tệp này không cho phép tải lên"
                           };
                   }

                   var att = _attachmentService.AddAttachmentInDoc(f.FileName, f.InputStream, userId, document);
                   return
                       new
                       {
                           key = att.AttachmentId.ToString(),
                           size = (long)att.Size,
                           name = f.FileName,
                           extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                           error = ""
                       };
               })).ToList();
            Task.Factory.ContinueWhenAll(task.ToArray(), tasks =>
            {
                var listFile = tasks.Select(item => item.Result);
                AsyncManager.Parameters["data"] = listFile;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult UploadAttachDaPhatHanhOrKetThucCompleted(dynamic data)
        {
            return Json(data);
        }

        [HttpPost]
        public void UploadAttachmentInLinkAsync(string urls)
        {
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            var webClient = new WebClient();
            AsyncManager.OutstandingOperations.Increment();
            var listUrl = Json2.ParseAs<List<FileTemp>>(urls);
            var task =
                listUrl.Select(f => Task.Factory.StartNew(() =>
                {
                    var data = webClient.DownloadData(f.Url);
                    var stream = new MemoryStream(data);
                    var length = stream.Length;
                    var fileName = f.FileName;
                    var ext = Path.GetExtension(f.FileName);

                    if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
                    {
                        return
                            new
                            {
                                key = "",
                                size = length,
                                name = fileName,
                                extension = "",
                                error = "Tệp đính kèm quá dung lượng quy định"
                            };
                    }

                    if (!string.IsNullOrEmpty(ext) && !_fileUploadSettings.FileUploadAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
                    {
                        return
                            new
                            {
                                key = "",
                                size = length,
                                name = fileName,
                                extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                                error = "Loại tệp này không cho phép tải lên"
                            };
                    }

                    var temp = FileManager.Default.Create(stream, tempPath);

                    return
                        new
                        {
                            key = temp.Name,
                            size = temp.Length,
                            name = fileName,
                            extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                            error = ""
                        };
                })).ToList();

            Task.Factory.ContinueWhenAll(task.ToArray(), tasks =>
            {
                var listFile = tasks.Select(item => item.Result);
                AsyncManager.Parameters["data"] = listFile;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public JsonResult UploadAttachmentInLinkCompleted(dynamic data)
        {
            return Json(data);
        }

        #endregion

        #region Upload Scan

        [HttpPost]
        public void UploadTempScanAsync(string files)
        {
            AsyncManager.OutstandingOperations.Increment();
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            var listFile = Json2.ParseAs<List<IDictionary<string, string>>>(files);
            if (listFile != null && listFile.All(f => f.ContainsKey("id") && f.ContainsKey("name") && f.ContainsKey("value")))
            {
                var task =
                listFile.Select(f => Task.Factory.StartNew(() =>
                {
                    var ext = Path.GetExtension(f["name"]);
                    if (!string.IsNullOrEmpty(ext) && !_fileUploadSettings.FileUploadAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
                    {
                        return
                            new
                            {
                                id = f["id"],
                                key = "",
                                size = (long)0,
                                name = f["name"],
                                extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                                error = "Loại tệp này không cho phép tải lên"
                            };
                    }
                    var contentBase64 = f["value"];
                    var contentStream = new MemoryStream(Convert.FromBase64String(contentBase64));
                    var length = contentStream.Length;
                    if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
                    {
                        return
                            new
                            {
                                id = f["id"],
                                key = "",
                                size = length,
                                name = f["name"],
                                extension = "",
                                error = "Tệp đính kèm quá dung lượng quy định"
                            };
                    }

                    var temp = FileManager.Default.Create(contentStream, tempPath);
                    return
                        new
                        {
                            id = f["id"],
                            key = temp.Name,
                            size = temp.Length,
                            name = f["name"],
                            extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                            error = ""
                        };
                })).ToList();
                Task.Factory.ContinueWhenAll(task.ToArray(), tasks =>
                {
                    AsyncManager.Parameters["data"] = tasks.Select(item => item.Result);
                    AsyncManager.OutstandingOperations.Decrement();
                });
            }
            else
            {
                AsyncManager.Parameters["data"] = null;
                AsyncManager.OutstandingOperations.Decrement();
            }
        }

        public JsonResult UploadTempScanCompleted(dynamic data)
        {
            return Json(data);
        }

        #endregion

        #region Upload Doctype Icon

        [HttpPost]
        public void UploadDoctypeIconAsync(IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            var tempPath = Server.MapPath("/Content/Images/Icons");//Tạm fix cứng link
            UploadCommon(files, tempPath, true);
        }

        public JsonResult UploadDoctypeIconCompleted(dynamic data)
        {
            return Json(data);
        }

        #endregion

        #region Download/Update Chat files

        [HttpPost]
        public void UploadChatAsync(IEnumerable<HttpPostedFileBase> files)
        {
            AsyncManager.OutstandingOperations.Increment();
            var tempPath = ResourceLocation.Default.Chat;
            UploadCommon(files, tempPath);
        }

        public JsonResult UploadChatCompleted(dynamic data)
        {
            return Json(data);
        }
        
        #endregion

        #region Download File

        public void DownloadFileAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                string fileName;
                var file = _fileService.DownloadFile(out fileName, id);
                return new { file, fileName };
            }).ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    AsyncManager.Parameters["file"] = t.Result.file;
                    AsyncManager.Parameters["fileName"] = t.Result.fileName;
                }
                else if (t.Exception != null)
                {
                    AsyncManager.Parameters["error"] = t.Exception;
                }
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DownloadFileCompleted(Stream file, string fileName, AggregateException error)
        {
            if (error != null)
            {
                if (error.InnerException is EgovException)
                {
                    return Json(new { error = error.InnerException.Message }, JsonRequestBehavior.AllowGet);
                }
                throw error.Flatten();
            }

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        #endregion

        public JsonResult GetSignConfig()
        {
            var userId = CurrentUserId();
            var signatureConfig = _signatureService.Gets(s => s.UserId == userId).Select(s => new { Title = s.SignatureName, FindText = s.SearchWord, FindType = s.IsFindType ? 1 : 0, SignType = s.IsTypeImage ? 0 : 1, PosType = s.SignaturePosition, OffsetX = 0, OffsetY = 0, TextInfor = s.IsDispplayCertificate ? 1 : 0, ImagePath = s.Image, Ext = s.ImageExtension });
            return Json(signatureConfig, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFileUploadSettings()
        {
            var maximumSizeBytes = _fileUploadSettings.FileUploadMaximumSizeBytes;
            var allowedExtensions = _fileUploadSettings.FileUploadAllowedExtensions;
            return Json(new { MaximumSizeBytes = maximumSizeBytes, AllowedExtensions = allowedExtensions }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveAttachment(int id, int documentCopyId)
        {
            var documentCopy = _docCopyService.Get(documentCopyId);
            if (documentCopy == null)
            {
                return Json(new { error = "Văn bản không tồn tại." });
            }

            int userSendId;
            if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(documentCopy, CurrentUserId(), out userSendId))
            {
                return Json(new { error = "Không có quyền xử lý văn bản này." });
            }

            var att = _attachmentService.Get(id);
            if (att == null)
            {
                return Json(new { error = "Tệp đính kèm không tồn tai." });
            }

            try
            {
                _attachmentService.Delete(att);
                return Json(new { success = "Xóa tệp đính kèm thành công." });
            }
            catch (Exception)
            {
                return Json(new { error = "Lỗi trong quá trình xóa tệp đính kèm.Vui long xem lại." });
            };
        }

        #region Private Methods

        private void UploadCommon(IEnumerable<HttpPostedFileBase> files, string tempPath, bool hasExtension = false)
        {
            if (files == null)
            {
                return;
            }

            var task =
                files.Select(f => Task.Factory.StartNew(() =>
                {
                    var length = f.InputStream.Length;
                    if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
                    {
                        return
                            new
                            {
                                key = "",
                                size = length,
                                name = f.FileName,
                                extension = "",
                                error = "Tệp đính kèm quá dung lượng quy định"
                            };
                    }
                    var ext = Path.GetExtension(f.FileName);

                    if (!string.IsNullOrEmpty(ext) && !_fileUploadSettings.FileUploadAllowedExtensions.Any(t => t.ToLower().Equals(ext.ToLower())))
                    {
                        return
                            new
                            {
                                key = "",
                                size = length,
                                name = f.FileName,
                                extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                                error = "Loại tệp này không cho phép tải lên"
                            };
                    }
                    var fileName = f.FileName;
                    var temp = FileManager.Default.Create(f.InputStream, tempPath, null, hasExtension ? ext : null);
                    return
                        new
                        {
                            key = temp.Name,
                            size = temp.Length,
                            name = fileName,
                            extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                            error = ""
                        };
                })).ToList();

            Task.Factory.ContinueWhenAll(task.ToArray(), tasks =>
            {
                var listFile = tasks.Select(item => item.Result);
                AsyncManager.Parameters["data"] = listFile;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        private void UploadXMLCommon(IEnumerable<HttpPostedFileBase> files, string tempPath, bool hasExtension = false)
        {
            var task =
                files.Select(f => Task.Factory.StartNew(() =>
                {
                    var length = f.InputStream.Length;
                    if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
                    {
                        return
                            new
                            {
                                key = "",
                                size = length,
                                name = f.FileName,
                                extension = "",
                                error = "Tệp đính kèm quá dung lượng quy định"
                            };
                    }
                    var ext = Path.GetExtension(f.FileName);

                    if (!string.IsNullOrEmpty(ext) && !ext.ToLower().Equals(".xml"))
                    {
                        return
                            new
                            {
                                key = "",
                                size = length,
                                name = f.FileName,
                                extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                                error = "Tệp tải lên phải là định dạng xml"
                            };
                    }
                    var fileName = f.FileName;
                    var temp = FileManager.Default.Create(f.InputStream, tempPath, null, hasExtension ? ext : null);
                    return
                        new
                        {
                            key = temp.Name,
                            size = temp.Length,
                            name = fileName,
                            extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
                            error = ""
                        };
                })).ToList();
            Task.Factory.ContinueWhenAll(task.ToArray(), tasks =>
            {
                var listFile = tasks.Select(item => item.Result);
                AsyncManager.Parameters["data"] = listFile;
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }

        #endregion
    }
}