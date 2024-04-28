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

        internal static bool IsNumericOnly(string s)
        {
            if (s == null || s == "") return false;

            for (int i = 0; i < s.Length; i++)
                if ((s[i] ^ '0') > 9)
                    return false;

            return true;
        }

        internal static int GetAlignmentShiftAdjustment(Barcode barcode)
        {
            switch (barcode.Alignment)
            {
                case AlignmentPositions.Left:
                    return 0;
                case AlignmentPositions.Right:
                    return (barcode.Width % barcode.EncodedValue.Length);
                case AlignmentPositions.Center:
                default:
                    return (barcode.Width % barcode.EncodedValue.Length) / 2;
            }//switch
        }
    }//BarcodeVariables abstract class
}//namespace
