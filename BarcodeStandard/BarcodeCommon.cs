using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BarcodeLib
{
    abstract class BarcodeCommon
    {
        protected string Raw_Data = "";
        protected List<string> _Errors = new List<string>();

        public string RawData
        {
            get { return this.Raw_Data; }
        }

        public List<string> Errors
        {
            get { return this._Errors; }
        }

        public void Error(string ErrorMessage)
        {
            this._Errors.Add(ErrorMessage);
            throw new Exception(ErrorMessage);
        }

        internal static bool CheckNumericOnly(string Data)
        {
            return Regex.IsMatch(Data, @"^\d+$", RegexOptions.Compiled);
        }
    }//BarcodeVariables abstract class
}//namespace
