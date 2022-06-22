using System;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class NetworkShare
    {
        const int RESOURCETYPE_DISK = 0x00000001;
        const int CONNECT_UPDATE_PROFILE = 0x00000001;

        #region Errors

        const int NO_ERROR = 0;
        const int ERROR_ACCESS_DENIED = 5;
        const int ERROR_ALREADY_ASSIGNED = 85;
        const int ERROR_BAD_DEVICE = 1200;
        const int ERROR_BAD_NET_NAME = 67;
        const int ERROR_BAD_PROVIDER = 1204;
        const int ERROR_CANCELLED = 1223;
        const int ERROR_EXTENDED_ERROR = 1208;
        const int ERROR_INVALID_ADDRESS = 487;
        const int ERROR_INVALID_PARAMETER = 87;
        const int ERROR_INVALID_PASSWORD = 1216;
        const int ERROR_MORE_DATA = 234;
        const int ERROR_NO_MORE_ITEMS = 259;
        const int ERROR_NO_NET_OR_BAD_PATH = 1203;
        const int ERROR_NO_NETWORK = 1222;
        const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;
        const int ERROR_BAD_PROFILE = 1206;
        const int ERROR_CANNOT_OPEN_PROFILE = 1205;
        const int ERROR_DEVICE_IN_USE = 2404;
        const int ERROR_NOT_CONNECTED = 2250;
        const int ERROR_OPEN_FILES = 2401;

        private static ErrorClass[] ERROR_LIST = new ErrorClass[] {
            new ErrorClass(ERROR_ACCESS_DENIED, "Error: Access Denied"), 
            new ErrorClass(ERROR_ALREADY_ASSIGNED, "Error: Already Assigned"), 
            new ErrorClass(ERROR_BAD_DEVICE, "Error: Bad Device"), 
            new ErrorClass(ERROR_BAD_NET_NAME, "Error: Bad Net Name"), 
            new ErrorClass(ERROR_BAD_PROVIDER, "Error: Bad Provider"), 
            new ErrorClass(ERROR_CANCELLED, "Error: Cancelled"), 
            new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"), 
            new ErrorClass(ERROR_INVALID_ADDRESS, "Error: Invalid Address"), 
            new ErrorClass(ERROR_INVALID_PARAMETER, "Error: Invalid Parameter"), 
            new ErrorClass(ERROR_INVALID_PASSWORD, "Error: Invalid Password"), 
            new ErrorClass(ERROR_MORE_DATA, "Error: More Data"), 
            new ErrorClass(ERROR_NO_MORE_ITEMS, "Error: No More Items"), 
            new ErrorClass(ERROR_NO_NET_OR_BAD_PATH, "Error: No Net Or Bad Path"), 
            new ErrorClass(ERROR_NO_NETWORK, "Error: No Network"), 
            new ErrorClass(ERROR_BAD_PROFILE, "Error: Bad Profile"), 
            new ErrorClass(ERROR_CANNOT_OPEN_PROFILE, "Error: Cannot Open Profile"), 
            new ErrorClass(ERROR_DEVICE_IN_USE, "Error: Device In Use"), 
            new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"), 
            new ErrorClass(ERROR_NOT_CONNECTED, "Error: Not Connected"), 
            new ErrorClass(ERROR_OPEN_FILES, "Error: Open Files"), 
            new ErrorClass(ERROR_SESSION_CREDENTIAL_CONFLICT, "Error: Credential Conflict"),
        };

        #endregion

        private static string GetError(int errNum)
        {
            foreach (ErrorClass er in ERROR_LIST)
            {
                if (er.Num == errNum)
                {
                    return er.Message;
                }
            }
            return "Error: Unknown, " + errNum;
        }

        /// <summary>
        /// Kết nối đến thư mục được chia sẻ qua mạng
        /// </summary>
        /// <param name="uri">Đường dẫn tới thư mục qua mạng (\\10.2.22.184\share)</param>
        /// <param name="username">Tài khoản đăng nhập máy chia sẻ thư mục</param>
        /// <param name="password">Mật khẩu đăng nhập</param>
        /// <returns></returns>
        public static string ConnectToShare(string uri, string username, string password)
        {
            var netResource = new NetResource
            {
                dwType = RESOURCETYPE_DISK,
                lpRemoteName = uri
            };

            //tạo kết nối tới thư mục được chia sẻ
            var result = WNetUseConnection(IntPtr.Zero, netResource, password, username, 0, null, null, null);
            if (result == NO_ERROR)
            {
                return null;
            }
            return NetworkShare.GetError(result);
        }

        /// <summary>
        /// Remove the share from cache.
        /// </summary>
        /// <returns>Null if successful, otherwise error message.</returns>
        public static string DisconnectFromShare(string uri, bool force)
        {
            //Hủy kết nối tới thư mục
            var result = WNetCancelConnection(uri, force);

            if (result == NO_ERROR)
            {
                return null;
            }
            return NetworkShare.GetError(result);
        }

        #region  api

        [DllImport("Mpr.dll")]
        private static extern int WNetUseConnection(
            IntPtr hwndOwner,
            NetResource lpNetResource,
            string lpPassword,
            string lpUserID,
            int dwFlags,
            string lpAccessName,
            string lpBufferSize,
            string lpResult);

        [DllImport("Mpr.dll")]
        private static extern int WNetCancelConnection(string lpName, bool fForce);

        #endregion

        #region private class

        [StructLayout(LayoutKind.Sequential)]
        private class NetResource
        {
            public int dwScope = 0;
            public int dwType = 0;
            public int dwDisplayType = 0;
            public int dwUsage = 0;
            public string lpLocalName = "";
            public string lpRemoteName = "";
            public string lpComment = "";
            public string lpProvider = "";
        }

        private struct ErrorClass
        {
            public int Num;

            public string Message;

            public ErrorClass(int num, string message)
            {
                this.Num = num;
                this.Message = message;
            }
        }

        #endregion
    }
}