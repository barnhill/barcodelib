using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Postnet encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Postnet : BarcodeCommon, IBarcode
    {
        private string[] POSTNET_Code = { "11000", "00011", "00101", "00110", "01001", "01010", "01100", "10001", "10010", "10100" };

        public Postnet(string input)
        {
            Raw_Data = input;
        }//Postnet

        /// <summary>
        /// Encode the raw data using the PostNet algorithm.
        /// </summary>
        private string Encode_Postnet()
        {
            //remove dashes if present
            Raw_Data = Raw_Data.Replace("-", "");

            switch (Raw_Data.Length)
            {
                case 5:
                case 6:
                case 9:
                case 11: break;
                default: Error("EPOSTNET-2: Invalid data length. (5, 6, 9, or 11 digits only)"); 
                    break;
            }//switch

            //Note: 0 = half bar and 1 = full bar
            //initialize the result with the starting bar
            string result = "1";
            int checkdigitsum = 0;

            foreach (char c in Raw_Data)
            {
                try
                {
                    int index = Convert.ToInt32(c.ToString());
                    result += POSTNET_Code[index];
                    checkdigitsum += index;
                }//try
                catch (Exception ex)
                {
                    Error("EPOSTNET-2: Invalid data. (Numeric only) --> " + ex.Message);
                }//catch
            }//foreach

            //calculate and add check digit
            int temp = checkdigitsum % 10;
            int checkdigit = 10 - (temp == 0 ? 10 : temp);

            result += POSTNET_Code[checkdigit];

            //ending bar
            result += "1";

            return result;
        }//Encode_PostNet

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_Postnet(); }
        }

        #endregion
    }//class
}//namespace
