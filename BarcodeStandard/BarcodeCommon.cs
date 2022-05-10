using System;
using System.Collections.Generic;

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

        public void Error(string errorMessage)
        {
            this._Errors.Add(errorMessage);
            throw new Exception(errorMessage);
        }

        internal static bool CheckNumericOnly(string data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                if (!char.IsDigit(data[i]))
                {
                    return false;
                }
            }
            return data.Length > 0;
        }
    }//BarcodeVariables abstract class
}//namespace
