using System;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Code 11 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Code11 : BarcodeCommon, IBarcode
    {
        private readonly string[] C11_Code = { "101011", "1101011", "1001011", "1100101", "1011011", "1101101", "1001101", "1010011", "1101001", "110101", "101101", "1011001" };

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
            var weight = 1;
            var cTotal = 0;
            var dataToEncodeWithChecksums = Raw_Data;

            //figure the C checksum
            for (var i = Raw_Data.Length - 1; i >= 0; i--)
            {
                //C checksum weights go 1-10
                if (weight == 10) weight = 1;

                if (Raw_Data[i] != '-')
                    cTotal += Int32.Parse(Raw_Data[i].ToString()) * weight++;
                else
                    cTotal += 10 * weight++;
            }//for
            var checksumC = cTotal % 11;

            dataToEncodeWithChecksums += checksumC.ToString();

            //K checksums are recommended on any message length greater than or equal to 10
            if (Raw_Data.Length >= 10)
            {
                weight = 1;
                var kTotal = 0;

                //calculate K checksum
                for (var i = dataToEncodeWithChecksums.Length - 1; i >= 0; i--)
                {
                    //K checksum weights go 1-9
                    if (weight == 9) weight = 1;

                    if (dataToEncodeWithChecksums[i] != '-')
                        kTotal += Int32.Parse(dataToEncodeWithChecksums[i].ToString()) * weight++;
                    else
                        kTotal += 10 * weight++;
                }//for
                var checksumK = kTotal % 11;
                dataToEncodeWithChecksums += checksumK.ToString();
            }//if

            //encode data
            var space = "0";
            var result = C11_Code[11] + space; //start-stop char + interchar space

            foreach (var c in dataToEncodeWithChecksums)
            {
                var index = (c == '-' ? 10 : Int32.Parse(c.ToString()));
                result += C11_Code[index];

                //inter-character space
                result += space;
            }//foreach

            //stop bars
            result += C11_Code[11];

            return result;
        }//Encode_Code11 

        #region IBarcode Members

        public string Encoded_Value => Encode_Code11();

        #endregion
    }//class
}//namespace
