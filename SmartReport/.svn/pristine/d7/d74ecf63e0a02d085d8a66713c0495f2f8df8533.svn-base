using System;

namespace Bkav.eGovCloud.Core.Document
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - Department 2
    /// Project: eGov Cloud v1.0
    /// Enum : DocumentUtils - public - Core
    /// Access Modifiers: 
    ///     *Inherit: None
    /// Create Date : 190213
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// <para>Các hàm tiện ích xử lý thông tin văn bản/hồ sơ</para>
    /// (GiangPN@bkav.com - 190213)
    /// </summary>
    public class DocumentUtils
    {
        /// <summary>
        /// Mã hóa hạn hồi báo
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>giá trị mã hóa hạn hồi báo</returns>
        public static int SetProprity(DateTime date)
        {
            var year = date.Year.ToString();
            const int nHoibao = (int)((1 << 24) & 0xff000000);
            var nDay = (date.Day << 16) & 0x00ff0000;
            var nMonth = (date.Month << 8) & 0x0000ff00;
            var nYear = (int.Parse(year.Substring(2, 2))) & 0x000000ff;
            return nHoibao | nDay | nMonth | nYear;
        }

        /// <summary>
        /// Giải mã hạn hồi báo để lấy ra chuỗi ngày tháng năm
        /// </summary>
        /// <param name="imucUuTien">giá trị hạn hồi báo được mã hóa</param>
        /// <param name="year">năm tạo công văn</param>
        /// <returns>chuỗi ngày tháng năm</returns>
        public static string GetDateHoiBao(int imucUuTien, string year)
        {
            var sDate = "";
            var yearHb = "";
            if (imucUuTien > 0)
            {
                int dd;
                int mm;
                int yy;
                int yyoo;
                int ooyy;
                int iHoiBao = (imucUuTien & 0xff000000) > 0 ? 1 : 0;

                if (iHoiBao > 0)
                {
                    dd = (imucUuTien & 0x00ff0000) >> 16;
                    mm = (imucUuTien & 0x0000ff00) >> 8;
                    yy = imucUuTien & 0x000000ff;
                    if (year != "")
                    {
                        yyoo = int.Parse(year.Substring(0, 2));
                        ooyy = int.Parse(year.Substring(2, 2));
                        if (ooyy == yy)
                        {
                            yearHb = yyoo.ToString() + yy.ToString();
                        }
                        else
                        {
                            yearHb = (yyoo + 1).ToString() + yy.ToString();
                        }
                    }
                    sDate = mm.ToString() + "/" + dd.ToString() + "/" + yearHb;
                }
            }

            return sDate;
        }
    }
}
