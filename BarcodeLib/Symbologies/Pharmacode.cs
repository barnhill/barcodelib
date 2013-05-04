using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Pharmacode encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Pharmacode: BarcodeCommon, IBarcode
    {
        string _thinBar = "1";
        string _gap = "00";
        string _thickBar = "111";

        public Pharmacode(string input)
        {
            if (!IsNumeric(input))
            {
                Error("EPHARM-1: Data contains invalid  characters (non-numeric).");
            }//if

            //check valid range
            int num = Convert.ToInt32(input);

            if (input.Length <= 6)
            {
                Error("EPHARM-2: Data too long (invalid data input length).");
            }//if
            else if (num < 3 || num > 131070)
            {
                Error("EPHARM-3: Data contains invalid  characters (invalid numeric range).");
            }//if
        }

        private void Init_Pharmacode()
        { 
        }

        private void Encode_Pharmacode()
        { 
        }

        #region IBarcode Members

        public string Encoded_Value
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
