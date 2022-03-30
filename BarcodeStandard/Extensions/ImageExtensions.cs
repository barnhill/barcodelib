using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BarcodeLib;

namespace BarcodeStandard.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] EncodedImageBytes(this Image image, ImageFormat imageFormat)
        {
            if (image == null)
                return null;

            using (var ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                return ms.ToArray();
            }
        }
        
        
        /// <summary>
        /// Returns the size of the EncodedImage in real world coordinates (millimeters or inches).
        /// </summary>
        /// <param name="metric">Millimeters if true, otherwise Inches.</param>
        /// <returns></returns>
        public static Barcode.ImageSize GetSizeOfImage(this Image image, bool metric)
        {
            double Width = 0;
            double Height = 0;
            if (image != null && image.Width > 0 && image.Height > 0)
            {
                double MillimetersPerInch = 25.4;
                using (Graphics g = Graphics.FromImage(image))
                {
                    Width = image.Width / g.DpiX;
                    Height = image.Height / g.DpiY;

                    if (metric)
                    {
                        Width *= MillimetersPerInch;
                        Height *= MillimetersPerInch;
                    }//if
                }//using
            }//if

            return new Barcode.ImageSize(Width, Height, metric);
        }

        public static CrystalReportsWrapper ToCrystalReportsWrapper(this Image image, ImageFormat format) => new CrystalReportsWrapper()
            {
                Image = image,
                ImageFormat = format
            };
    }
}