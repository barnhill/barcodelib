using System;

namespace BarcodeStandard
{
    public class SaveData : IDisposable
    {
        public SaveData()
        {
        }
        
        public string Type { get; set; }
        public string RawData { get; set; }
        public string EncodedValue { get; set; }
        public double EncodingTime { get; set; }
        public bool IncludeLabel { get; set; }
        public string Forecolor { get; set; }
        public string Backcolor { get; set; }
        public string CountryAssigningManufacturingCode { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string Image { get; set; }
        public System.Drawing.RotateFlipType RotateFlipType { get; set; }
        public int LabelPosition { get; set; }
        public int Alignment { get; set; }
        public string LabelFont { get; set; }
        public string ImageFormat { get; set; }

        public void Dispose()
        {
        }
    }
}
