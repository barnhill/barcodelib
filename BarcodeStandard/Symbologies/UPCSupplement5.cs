using System;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  UPC Supplement-5 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class UPCSupplement5 : BarcodeCommon, IBarcode
    {
        private readonly string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private readonly string[] EAN_CodeB = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private readonly string[] UPC_SUPP_5 = { "bbaaa", "babaa", "baaba", "baaab", "abbaa", "aabba", "aaabb", "ababa", "abaab", "aabab" };

        public UPCSupplement5(string input)
        {
            Raw_Data = input;
        }

        /// <summary>
        /// Encode the raw data using the UPC Supplemental 5-digit algorithm.
        /// </summary>
        private string Encode_UPCSupplemental_5()
        {
            if (Raw_Data.Length != 5) Error("EUPC-SUP5-1: Invalid data length. (Length = 5 required)");

            if (!CheckNumericOnly(Raw_Data))
                Error("EUPCA-2: Numeric Data Only");

            //calculate the checksum digit
            var even = 0;
            var odd = 0;

            //odd
            for (var i = 0; i <= 4; i += 2)
            {
                odd += Int32.Parse(Raw_Data.Substring(i, 1)) * 3;
            }//for

            //even
            for (var i = 1; i < 4; i += 2)
            {
                even += Int32.Parse(Raw_Data.Substring(i, 1)) * 9;
            }//for

            var total = even + odd;
            var cs = total % 10;

            var pattern = UPC_SUPP_5[cs];

            var result = "";

            var pos = 0;
            foreach (var c in pattern)
            {
                //Inter-character separator
                if (pos == 0) result += "1011";
                else result += "01";

                switch (c)
                {
                    case 'a':
                        //encode using odd parity
                        result += EAN_CodeA[Int32.Parse(Raw_Data[pos].ToString())]; //if
                        break;
                    case 'b':
                        //encode using even parity
                        result += EAN_CodeB[Int32.Parse(Raw_Data[pos].ToString())]; //else if  
                        break;
                }
                pos++;
            }//foreach
            return result;
        }//Encode_UPCSupplemental_5

        #region IBarcode Members

        public string Encoded_Value => Encode_UPCSupplemental_5();

        #endregion
    }//class
}//namespace
