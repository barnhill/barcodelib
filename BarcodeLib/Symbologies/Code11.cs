using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Code 11 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Code11 : BarcodeCommon, IBarcode
    {
        private string[] C11_Code = { "101011", "1101011", "1001011", "1100101", "1011011", "1101101", "1001101", "1010011", "1101001", "110101", "101101", "1011001" };

        public Code11(string input)
        {
            Raw_Data = input;
        }//Code11
        /// <summary>
        /// Encode the raw data using the Code 11 algorithm.
        /// </summary>
        private string Encode_Code11()
        {
            if (!CheckNumericOnly(Raw_Data.Replace("-", "")))
                Error("EC11-1: Numeric data and '-' Only");

            //calculate the checksums
            int weight = 1;
            int CTotal = 0;
            string Data_To_Encode_with_Checksums = Raw_Data;

            //figure the C checksum
            for (int i = Raw_Data.Length - 1; i >= 0; i--)
            {
                //C checksum weights go 1-10
                if (weight == 10) weight = 1;

                if (Raw_Data[i] != '-')
                    CTotal += Int32.Parse(Raw_Data[i].ToString()) * weight++;
                else
                    CTotal += 10 * weight++;
            }//for
            int checksumC = CTotal % 11;

            Data_To_Encode_with_Checksums += checksumC.ToString();

            //K checksums are recommended on any message length greater than or equal to 10
            if (Raw_Data.Length >= 1)
            {
                weight = 1;
                int KTotal = 0;

                //calculate K checksum
                for (int i = Data_To_Encode_with_Checksums.Length - 1; i >= 0; i--)
                {
                    //K checksum weights go 1-9
                    if (weight == 9) weight = 1;

                    if (Data_To_Encode_with_Checksums[i] != '-')
                        KTotal += Int32.Parse(Data_To_Encode_with_Checksums[i].ToString()) * weight++;
                    else
                        KTotal += 10 * weight++;
                }//for
                int checksumK = KTotal % 11;
                Data_To_Encode_with_Checksums += checksumK.ToString();
            }//if

            //encode data
            string space = "0";
            string result = C11_Code[11] + space; //start-stop char + interchar space

            foreach (char c in Data_To_Encode_with_Checksums)
            {
                int index = (c == '-' ? 10 : Int32.Parse(c.ToString()));
                result += C11_Code[index];

                //inter-character space
                result += space;
            }//foreach

            //stop bars
            result += C11_Code[11];

            return result;
        }//Encode_Code11 

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_Code11(); }
        }

        #endregion
    }//class
}//namespace
