using System;
using System.Collections.Generic;

namespace BarcodeStandard
{
    internal abstract class BarcodeCommon
    {
        public string RawData { get; protected set; } = "";
        public List<string> Errors { get; } = [];

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
            return barcode.Alignment switch
            {
                AlignmentPositions.Left => 0,
                AlignmentPositions.Right => (barcode.Width % barcode.EncodedValue.Length),
                _ => (barcode.Width % barcode.EncodedValue.Length) / 2,
            };
        }
    }//BarcodeVariables abstract class
}
