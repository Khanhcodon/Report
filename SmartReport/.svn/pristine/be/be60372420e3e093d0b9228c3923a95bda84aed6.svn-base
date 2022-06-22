using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    /// <author>Bkav Corp. - BSO - eGov - eOffice team
    ///   Project: eGov Cloud v1.0
    ///   Class : HistoryPathUtil
    ///   Access Modifiers: 
    ///   Create Date : 250713
    ///   Author      : CuongNT
    /// </author>
    /// <summary>
    ///   <para> Cung cấp các hàm dùng để thao tác trên kiểu dữ liệu lịch sử xử lý văn bản hồ sơ HistoryPath </para>
    ///   (CuongNT@bkav.com - 250713)
    /// </summary>
    public class HistoryPathHelper
    {
        ///<summary>
        /// Khởi tạo class <see cref="HistoryPathHelper"/>.
        ///</summary>
        public HistoryPathHelper()
        {
        }

        /// <summary>
        /// <para>Tra ve danh sach huong lay lai van ban</para>
        /// <para>CuongNT@bkav.com - 290713</para>
        /// </summary>
        /// <param name="historyPaths">Lich su xu ly van ban</param>
        /// <param name="userId">Id can bo can lay danh sach huong lay lai van ban</param>
        /// <param name="isProcessing"><c>True</c> neu la van ban dang xu ly. <c>False</c> neu nguoc lai.</param>
        /// <returns></returns>
        public static List<UndoTransfer> GetUndoTransfer(List<HistoryPath> historyPaths, int userId, bool isProcessing)
        {
            var result = new List<UndoTransfer>();
            HistoryPath historyPath;
            if (isProcessing)
            {
                historyPath = historyPaths[historyPaths.Count - 1];
                // Neu nguoi can kiem tra khong dang giu van ban
                if (historyPath.UserReceiveId != userId)
                {
                    return result;
                }
            }
            else
            {
                historyPath = historyPaths[historyPaths.Count - 2];
                // Neu nguoi can kiem tra khong phai vua ban giao van ban
                if (historyPath.UserSendId != userId)
                {
                    return result;
                }
            }
            var dateCreateds = historyPath.UserReceives.Select(c => c.DateCreated).Distinct().ToList();

            foreach (var dateCreated in dateCreateds)
            {
                if (isProcessing)
                {
                    // Neu thoi diem chuyen chi toan ban giao dxl, va la ban giao Thong bao, Xin y kien
                    if (historyPath.UserReceives.Where(c => c.DateCreated == dateCreated).All(c => !c.IsXlc &&
                        (c.DocumentCopyType == (int)DocumentCopyTypes.ThongBao || c.DocumentCopyType == (int)DocumentCopyTypes.XinYKien)))
                    {
                        result.Add(new UndoTransfer
                        {
                            Name = "Lay lai van ban chuyen luc " + dateCreated.ToString("dd/MM/yyyy hh:mm"),
                            DateCreated = dateCreated
                        });
                    }
                }
                else
                {
                    // Neu thoi diem chuyen co ban giao xu ly chinh (tuc ban giao thong thuong)
                    if (historyPath.UserReceives.Where(c => c.DateCreated == dateCreated).Any(c => c.IsXlc))
                    {
                        result.Add(new UndoTransfer
                        {
                            Name = "Lay lai van ban chuyen luc " + dateCreated.ToString("dd/MM/yyyy hh:mm"),
                            DateCreated = dateCreated
                        });
                    }
                }
            }
            return result;
        }
        
    }
}
