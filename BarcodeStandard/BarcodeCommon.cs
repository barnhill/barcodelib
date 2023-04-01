using System;
using System.Collections.Generic;
using System.Linq;

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
            if (data.Any(c => !char.IsDigit(c))) return false;
            return data.Length > 0;
        }
    }//BarcodeVariables abstract class
}//namespace
