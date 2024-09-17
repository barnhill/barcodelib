using System;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Postnet encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Postnet : BarcodeCommon, IBarcode
    {
        private readonly string[] POSTNET_Code = ["11000", "00011", "00101", "00110", "01001", "01010", "01100", "10001", "10010", "10100"];

        internal Postnet(string input)
        {
            RawData = input;
        }

        /// <summary>
        /// Encode the raw data using the PostNet algorithm.
        /// </summary>
        private string Encode_Postnet()
        {
            //remove dashes if present
            RawData = RawData.Replace("-", "");

            switch (RawData.Length)
            {
                case 5:
                case 6:
                case 9:
                case 11: break;
                default: Error("EPOSTNET-2: Invalid data length. (5, 6, 9, or 11 digits only)"); 
                    break;
            }

            //Note: 0 = half bar and 1 = full bar
            //initialize the result with the starting bar
            var result = "1";
            var checkdigitsum = 0;

            foreach (var c in RawData)
            {
                try
                {
                    var index = Convert.ToInt32(c.ToString());
                    result += POSTNET_Code[index];
                    checkdigitsum += index;
                }
                catch (Exception ex)
                {
                    Error("EPOSTNET-2: Invalid data. (Numeric only) --> " + ex.Message);
                }
            }

            //calculate and add check digit
            var temp = checkdigitsum % 10;
            var checkdigit = 10 - (temp == 0 ? 10 : temp);

            result += POSTNET_Code[checkdigit];

            //ending bar
            result += "1";

            return result;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Postnet();

        #endregion
    }
}
