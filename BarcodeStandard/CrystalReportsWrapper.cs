using System;
using System.Drawing;
using System.Drawing.Imaging;
using BarcodeStandard.Extensions;

namespace BarcodeStandard
{
    public class CrystalReportsWrapper : IDisposable
    {
        public Image Image { get; set; }
        public ImageFormat ImageFormat { get; set; }

        /// <summary>
        /// Gets a byte array representation of the encoded image. (Used for Crystal Reports)
        /// </summary>
        public byte[] Encoded_Image_Bytes => Image.EncodedImageBytes(ImageFormat);

        public void Dispose()
        {
            Image?.Dispose();
        }
    }
}