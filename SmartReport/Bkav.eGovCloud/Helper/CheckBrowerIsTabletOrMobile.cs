using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Helper
{
    public static class CheckBrowerIsTabletOrMobile
    {
        /// <summary>
        /// Author: HopCV
        /// Created:190914
        /// 
        /// Descriptions: kiểm tra trinh duyệt của người dùng hiện tại 
        /// đang sử dụng trên tablet hay mobile.
        /// 
        /// </summary>
        /// <returns></returns>
        public static DeviceType GetDeviceCurrent()
        {
            //Lấy request hiện tại của người dùng khi gửi lên server
            HttpRequest request = HttpContext.Current.Request;

            //kiểm tra HTTP_X_WAP_PROFILE HEADER
            //Nếu  là null thì trả về là mobile
            if (request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return DeviceType.Mobile;
            }

            // Kiêm tra HTTP_ACCEPT nếu tồn tại và có chưa WAP
            if (request.ServerVariables["HTTP_ACCEPT"] != null &&
                request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return DeviceType.Mobile;
            }

            //Kiểm tra header của request
            if (request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                var userAgent = request.ServerVariables["HTTP_USER_AGENT"].ToLower();
                var tablets = new[] { "ipad", "android 3", "xoom", "sch-i800", "tablet", "kindle", "playbook" };

                //Duyệt qua các đối tuowngjcuar tablet và kiểm trả header
                if (tablets.Any(userAgent.Contains)
                    || (userAgent.Contains("android")
                    && !userAgent.Contains("mobile")))
                {
                    return DeviceType.Tablet;
                }

                //Các đối tượng kiểm tra mobile
                var mobiles = new[]
                                {
                                    "midp", "j2me", "avant", "docomo",
                                    "novarra", "palmos", "palmsource",
                                    "240x320", "opwv", "chtml",
                                    "pda", "windows ce", "mmp/",
                                    "blackberry", "mib/", "symbian",
                                    "wireless", "nokia", "hand", "mobi",
                                    "phone", "cdm", "up.b", "audio",
                                    "SIE-", "SEC-", "samsung", "HTC",
                                    "mot-", "mitsu", "sagem", "sony"
                                    , "alcatel", "lg", "eric", "vx",
                                    "NEC", "philips", "mmm", "xx",
                                    "panasonic", "sharp", "wap", "sch",
                                    "rover", "pocket", "benq", "java",
                                    "pt", "pg", "vox", "amoi",
                                    "bird", "compal", "kg", "voda",
                                    "sany", "kdd", "dbt", "sendo",
                                    "sgh", "gradi", "jb", "dddi",
                                    "moto", "iphone", "Opera Mini"
                                };

                //Duyệt qua các đối tượng mobile 
                //và trả về DeviceType mobile nếu  request hiện tại nằm trong danh sách kiểm tra là mobile
                if (mobiles.Any(userAgent.Contains))
                {
                    return DeviceType.Mobile;
                }
            }

            //Do hàm này khi chạy trên tablet hay mobile thì mặc định trả về là true 
            //nên không xác định chính xác được là  DeviceType nào 
            //nên các điều kiện trên kìa chưa xác định được thì dung điều kiến này để xác định và mặc định trả về mobile
            if (request.Browser.IsMobileDevice)
            {
                return DeviceType.Mobile;
            }

            return DeviceType.Default;
        }
    }

    public enum DeviceType
    {
        /// <summary>
        /// Mặc định trên máy PC hoặc Laptop
        /// </summary>
        Default = 0,

        /// <summary>
        /// Trên mobile
        /// </summary>
        Mobile = 1,

        /// <summary>
        /// Trên tablet(Máy tính bẳng)
        /// </summary>
        Tablet = 2,

        /// <summary>
        /// Trên tvivi
        /// </summary>
        Television = 4
    }
}