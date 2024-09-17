using System;
using System.Collections;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  UPC-A encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class UPCA : BarcodeCommon, IBarcode
    {
        private readonly string[] UPC_Code_A = ["0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011"];
        private readonly string[] UPC_Code_B = ["1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100"];

        internal UPCA(string input)
        {
            RawData = input;
        }
        /// <summary>
        /// Encode the raw data using the UPC-A algorithm.
        /// </summary>
        private string Encode_UPCA()
        {
            //check length of input
            if (RawData.Length != 11 && RawData.Length != 12)
                Error("EUPCA-1: Data length invalid. (Length must be 11 or 12)");

            if (!IsNumericOnly(RawData))
                Error("EUPCA-2: Numeric Data Only");

            CheckDigit();
            
            var result = "101"; //start with guard bars

            //first number
            result += UPC_Code_A[Int32.Parse(RawData[0].ToString())];

            //second (group) of numbers
            var pos = 0;
            while (pos < 5)
            {
                result += UPC_Code_A[Int32.Parse(RawData[pos + 1].ToString())];
                pos++;
            }

            //add divider bars
            result += "01010";

            //third (group) of numbers
            pos = 0;
            while (pos < 5)
            {
                result += UPC_Code_B[Int32.Parse(RawData[(pos++) + 6].ToString())];
            }

            //forth
            result += UPC_Code_B[Int32.Parse(RawData[RawData.Length - 1].ToString())];

            //add ending guard bars
            result += "101";

            return result;
        }
        
        private void CheckDigit()
        {
            try
            {
                var rawDataHolder = RawData.Substring(0, 11);

                //calculate check digit
                var sum = 0;

                for (var i = 0; i < rawDataHolder.Length; i++)
                {
                    if (i % 2 == 0)
                        sum += Int32.Parse(rawDataHolder.Substring(i, 1)) * 3;
                    else
                        sum += Int32.Parse(rawDataHolder.Substring(i, 1));
                }

                int cs = (10 - sum % 10) % 10;

                //replace checksum if provided by the user and replace with the calculated checksum
                RawData = rawDataHolder + cs;
            }
            catch
            {
                Error("EUPCA-4: Error calculating check digit.");
            }
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_UPCA();

        #endregion
    }
}
