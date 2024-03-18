using System;
using System.Collections.Generic;

namespace BarcodeStandard
{
    internal abstract class BarcodeCommon
    {
        public string RawData { get; protected set; } = "";
        public List<string> Errors { get; } = new List<string>();

        protected void Error(string errorMessage)
        {
            Errors.Add(errorMessage);
            throw new Exception(errorMessage);
        }

        internal static bool CheckNumericOnly(string data)
        {
            foreach (var c in data)
            {
                if (c < '0' && c > '9') return false;
            }

            return true;
        }

        internal static int GetQuietZoneAdjustment(Barcode barcode)
        {
            if (barcode.IncludeLabel && barcode.GuardBarsMode == GuardBarsMode.EnabledFirstCharOnQuietZone)
            {
                return Utils.GetFontWidth("0", barcode.LabelFont) * 2;
            }

            return 0;
        }

        internal static int GetAlignmentShiftAdjustment(Barcode barcode)
        { 
            var quietZoneAdjustment = BarcodeCommon.GetQuietZoneAdjustment(barcode);

            switch (barcode.Alignment)
            {
                case AlignmentPositions.Left:
                    return quietZoneAdjustment;

                case AlignmentPositions.Right:
                    return ((barcode.Width - quietZoneAdjustment * 2) % barcode.EncodedValue.Length) + quietZoneAdjustment;

                case AlignmentPositions.Center:
                default:
                    return ((barcode.Width - quietZoneAdjustment * 2) % barcode.EncodedValue.Length) / 2 + quietZoneAdjustment;
            }//switch
        }
    }//BarcodeVariables abstract class
}//namespace