using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  EAN-8 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class EAN8 : BarcodeCommon, IBarcode
    {
        private string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] EAN_CodeC = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };

        public EAN8(string input)
        {
            Raw_Data = input;

            CheckDigit();
        }
        /// <summary>
        /// Encode the raw data using the EAN-8 algorithm.
        /// </summary>
        private string Encode_EAN8()
        {
            //check length
            if (Raw_Data.Length != 8 && Raw_Data.Length != 7) Error("EEAN8-1: Invalid data length. (7 or 8 numbers only)");

            //check numeric only
            if (!CheckNumericOnly(Raw_Data)) Error("EEAN8-2: Numeric only.");

            //encode the data
            string result = "101";

            //first half (Encoded using left hand / odd parity)
            for (int i = 0; i < Raw_Data.Length / 2; i++)
            {
                result += EAN_CodeA[Int32.Parse(Raw_Data[i].ToString())];
            }//for

            //center guard bars
            result += "01010";

            //second half (Encoded using right hand / even parity)
            for (int i = Raw_Data.Length / 2; i < Raw_Data.Length; i++)
            {
                result += EAN_CodeC[Int32.Parse(Raw_Data[i].ToString())];
            }//for

            result += "101";

            return result;
        }//Encode_EAN8

        private void CheckDigit()
        {
            //calculate the checksum digit if necessary
            if (Raw_Data.Length == 7)
            {
                //calculate the checksum digit
                int even = 0;
                int odd = 0;

                //odd
                for (int i = 0; i <= 6; i += 2)
                {
                    odd += Int32.Parse(Raw_Data.Substring(i, 1)) * 3;
                }//for

                //even
                for (int i = 1; i <= 5; i += 2)
                {
                    even += Int32.Parse(Raw_Data.Substring(i, 1));
                }//for

                int total = even + odd;
                int checksum = total % 10;
                checksum = 10 - checksum;
                if (checksum == 10)
                    checksum = 0;

                //add the checksum to the end of the 
                Raw_Data += checksum.ToString();
            }//if
        }

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_EAN8(); }
        }

        #endregion
    }
}
