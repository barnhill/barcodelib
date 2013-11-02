using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  JAN-13 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class JAN13 : BarcodeCommon, IBarcode
    {
        public JAN13(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the JAN-13 algorithm.
        /// </summary>
        private string Encode_JAN13()
        {
            if (!Raw_Data.StartsWith("49")) Error("EJAN13-1: Invalid Country Code for JAN13 (49 required)");
            if (!CheckNumericOnly(Raw_Data))
                Error("EJAN13-2: Numeric Data Only");

            EAN13 ean13 = new EAN13(Raw_Data);
            return ean13.Encoded_Value;
        }//Encode_JAN13

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_JAN13(); }
        }

        #endregion
    }
}
