using System.Collections.Generic;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Enum : DocumentStatusMachineHelper - public - Entity Helper
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 100413
    /// Author      : CuongNT@bkav.com
    /// </author>
    /// <summary>
    /// <para>Kiểm soát các trạng thái hợp lệ của văn bản</para>
    /// <para>CuongNT@bkav.com - 100413</para>
    /// </summary>
    public class DocumentStatusMachineHelper
    {
        /// <summary>
        /// <para>Quy tắc hợp lệ cho quan hệ giữa DocumentCopyTypes và DocumentStatus</para>
        /// <para>CuongNT@bkav.com - 090413</para>
        /// </summary>
        private static readonly Dictionary<DocumentCopyTypes, DocumentStatus> Rules1 = new Dictionary<DocumentCopyTypes, DocumentStatus>
            {
                {DocumentCopyTypes.XuLyChinh, DocumentStatus.DuThao|DocumentStatus.DangXuLy|DocumentStatus.LoaiBo|DocumentStatus.KetThuc|DocumentStatus.DungXuLy},
                {DocumentCopyTypes.DongXuLy, DocumentStatus.DangXuLy|DocumentStatus.KetThuc},
                {DocumentCopyTypes.XinYKien, DocumentStatus.DangXuLy|DocumentStatus.KetThuc},
                {DocumentCopyTypes.ThongBao, DocumentStatus.DangXuLy|DocumentStatus.KetThuc},
                {DocumentCopyTypes.DuyetGiaHan, DocumentStatus.DangXuLy|DocumentStatus.KetThuc},
                {DocumentCopyTypes.ChoKetQuaDungXuLy, DocumentStatus.DangXuLy|DocumentStatus.KetThuc},
            };

        /// <summary>
        /// <para>Quy tắc hợp lệ cho chuyển trạng thái từ DocumentStatus sang DocumentStatus mới</para>
        /// <para>CuongNT@bkav.com - 090413</para>
        /// </summary>
        private static readonly Dictionary<DocumentStatus, DocumentStatus> Rules2 = new Dictionary<DocumentStatus, DocumentStatus>
            {
                {DocumentStatus.DuThao, DocumentStatus.DuThao|DocumentStatus.DangXuLy|DocumentStatus.LoaiBo},
                {DocumentStatus.DangXuLy, DocumentStatus.DangXuLy|DocumentStatus.DungXuLy|DocumentStatus.KetThuc|DocumentStatus.LoaiBo},
                {DocumentStatus.DungXuLy, DocumentStatus.DungXuLy|DocumentStatus.DangXuLy},
                {DocumentStatus.LoaiBo, DocumentStatus.LoaiBo|DocumentStatus.DuThao|DocumentStatus.DangXuLy},
                {DocumentStatus.KetThuc, DocumentStatus.KetThuc|DocumentStatus.DangXuLy}
            };

        /// <summary>
        /// <para>Kiểm tra trạng thái mới của văn bản có hợp lệ hay không</para>
        /// <para>CuongNT@bkav.com - 100413</para>
        /// </summary>
        /// <param name="currentDocumentCopyTypes"></param>
        /// <param name="currentStatus"> </param>
        /// <param name="newStatus"> </param>
        /// <returns></returns>
        public static bool ValidateNewDocumentStatus(DocumentCopyTypes currentDocumentCopyTypes, DocumentStatus currentStatus, DocumentStatus newStatus)
        {
            return EnumHelper<DocumentStatus>.ContainFlags(Rules1[currentDocumentCopyTypes], newStatus) &&
                EnumHelper<DocumentStatus>.ContainFlags(Rules2[currentStatus], newStatus);
        }

        /// <summary>
        /// <para>Kiểm tra trạng thái hiện tại của văn bản có hợp lệ hay không</para>
        /// <para>CuongNT@bkav.com - 100413</para>
        /// </summary>
        /// <param name="currentDocumentCopyTypes"></param>
        /// <param name="currentStatus"></param>
        /// <returns></returns>
        public static bool ValidateCurrentDocumentStatus(DocumentCopyTypes currentDocumentCopyTypes, DocumentStatus currentStatus)
        {
            return EnumHelper<DocumentStatus>.ContainFlags(Rules1[currentDocumentCopyTypes], currentStatus);
        }
    }
}
