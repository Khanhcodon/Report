using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Bkav.eGovCloud.Helper
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SettingProvider - public - BLL
    /// Access Modifiers: 
    /// Create Date : 220114
    /// Author      : HopCV
    /// Description : Chỉnh sửa lại chiều cao chiều rộng của  ảnh
    /// </summary>
    public static class ResizeAndCropImage
    {
        /// <summary>
        /// Chỉnh sửa lại kích thước ảnh
        /// </summary>
        /// <param name="imageSavePath">Đường dẫn lưu anh</param>
        /// <param name="fileName">Tên ảnh</param>
        /// <param name="maxWidthSideSize">Độ rộng</param>
        /// <param name="imgInput">Ảnh truyền vào</param>
        public static void ResizeImage(string imageSavePath, string fileName, int maxWidthSideSize, Image imgInput)
        {
            int intNewWidth;
            int intNewHeight;
            ImageCodecInfo myImageCodecInfo;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter;

            //Giá trị width và height nguyên thủy của ảnh;
            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            //Kiểm tra xem ảnh ngang hay dọc;
            int intMaxSide;
            if (intOldWidth >= intOldHeight)
            {
                intMaxSide = intOldWidth;
            }
            else
            {
                intMaxSide = intOldHeight;
            }
            intMaxSide = intOldWidth;
            if (intMaxSide > maxWidthSideSize)
            {
                double dblCoef = maxWidthSideSize / (double)intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }
            Bitmap bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmpResized.Save(imageSavePath + fileName, myImageCodecInfo, myEncoderParameters);

            imgInput.Dispose();
            bmpResized.Dispose();
        }

        /// <summary>
        /// Chỉnh sửa lại kích thước ảnh
        /// </summary>
        /// <param name="imageSavePath">Đường dẫn lưu anh</param>
        /// <param name="fileName">Tên ảnh</param>
        /// <param name="maxWidthSideSize">Độ rộng</param>
        /// <param name="stream">Luồng của ảnh :base64</param>
        public static void ResizeImage(string imageSavePath, string fileName, int maxWidthSize, Stream stream)
        {
            using (Image imgInput = Image.FromStream(stream))
            {
                ResizeImage(imageSavePath, fileName, maxWidthSize, imgInput);
            }
        }

        /// <summary>
        /// Chỉnh sửa lại kích thước ảnh
        /// </summary>
        /// <param name="imageSavePath">Đường dẫn lưu anh</param>
        /// <param name="fileName">Tên ảnh</param>
        /// <param name="maxWidthSideSize">Độ rộng</param>
        /// <param name="filePath">Đường dẫn đến file anh </param>
        public static void CropAndCropResizeImage(Image image, int maxWidth, int maxHeight, string filePath)
        {
            ImageCodecInfo jpgInfo = GetEncoderInfo("image/jpeg");
            Image finalImage = image;
            System.Drawing.Bitmap bitmap = null;
            int left = 0;
            int top = 0;
            int srcWidth = 0;
            int srcHeight = 0;
            bitmap = new System.Drawing.Bitmap(maxWidth, maxHeight);
            double croppedHeightToWidth = (double)maxHeight / maxWidth;
            double croppedWidthToHeight = (double)maxWidth / maxHeight;
            if (image.Width > image.Height)
            {
                srcWidth = (int)(Math.Round(image.Height * croppedWidthToHeight));
                if (srcWidth < image.Width)
                {
                    srcHeight = image.Height;
                    left = (image.Width - srcWidth) / 2;
                }
                else
                {
                    srcHeight = (int)Math.Round(image.Height * ((double)image.Width / srcWidth));
                    srcWidth = image.Width;
                    top = (image.Height - srcHeight) / 2;
                }
            }
            else
            {
                srcHeight = (int)(Math.Round(image.Width * croppedHeightToWidth));
                if (srcHeight < image.Height)
                {
                    srcWidth = image.Width;
                    top = (image.Height - srcHeight) / 2;
                }
                else
                {
                    srcWidth = (int)Math.Round(image.Width * ((double)image.Height / srcHeight));
                    srcHeight = image.Height;
                    left = (image.Width - srcWidth) / 2;
                }
            }
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                new Rectangle(left, top, srcWidth, srcHeight), GraphicsUnit.Pixel);
            }
            finalImage = bitmap;

            using (EncoderParameters encParams = new EncoderParameters(1))
            {
                encParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);
                finalImage.Save(filePath, jpgInfo, encParams);
            }

            if (bitmap != null)
            {
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// Chỉnh sửa lại ảnh: cắt và chỉnh lại kích thước
        /// </summary>
        /// <param name="stream">Luồng của ảnh:base64</param>
        /// <param name="maxWidth">Chiều rộng ảnh để crop</param>
        /// <param name="maxHeight">Chiều cao ảnh để crop</param>
        /// <param name="filePath">Đường dẫn đên file ảnh</param>
        public static void CropAndCropResizeImage(Stream stream, int maxWidth, int maxHeight, string filePath)
        {
            using (Image imgInput = Image.FromStream(stream))
            {
                CropAndCropResizeImage(imgInput, maxWidth, maxHeight, filePath);
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}