using System;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  UPC-E encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class UPCE : BarcodeCommon, IBarcode
    {
        private readonly string[] EAN_Code_A = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private readonly string[] EAN_Code_B = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private readonly string[] EAN_Pattern = { "aaaaaa", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba" };
        private readonly string[] UPC_E_Code0 = { "bbbaaa", "bbabaa", "bbaaba", "bbaaab", "babbaa", "baabba", "baaabb", "bababa", "babaab", "baabab" };
        private readonly string[] UPC_E_Code1 = { "aaabbb", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba" };

        /// <summary>
        /// Encodes a UPC-E symbol.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        public UPCE(string input)
        {
            Raw_Data = input;
        }//UPCE
        /// <summary>
        /// Encode the raw data using the UPC-E algorithm.
        /// </summary>
        private string Encode_UPCE()
        {
            if (Raw_Data.Length != 6 && Raw_Data.Length != 8 && Raw_Data.Length != 12) 
                Error("EUPCE-1: Invalid data length. (8 or 12 numbers only)");

            if (!CheckNumericOnly(Raw_Data)) 
                Error("EUPCE-2: Numeric only.");

            //check for a valid number system
            var numberSystem = Int32.Parse(Raw_Data[0].ToString());
            if (numberSystem != 0 && numberSystem != 1) 
                Error("EUPCE-3: Invalid Number System (only 0 & 1 are valid)");

            var CheckDigit = Int32.Parse(Raw_Data[Raw_Data.Length - 1].ToString());
            
            //Convert to UPC-E from UPC-A if necessary
            if (Raw_Data.Length == 12)
            {
                var UPCECode = "";

                //break apart into components
                var manufacturer = Raw_Data.Substring(1, 5);
                var productCode = Raw_Data.Substring(6, 5);
                
                if (manufacturer.EndsWith("000") || manufacturer.EndsWith("100") || manufacturer.EndsWith("200") && Int32.Parse(productCode) <= 999)
                {
                    //rule 1
                    UPCECode += manufacturer.Substring(0, 2); //first two of manufacturer
                    UPCECode += productCode.Substring(2, 3); //last three of product
                    UPCECode += manufacturer[2].ToString(); //third of manufacturer
                }//if
                else if (manufacturer.EndsWith("00") && Int32.Parse(productCode) <= 99)
                {
                    //rule 2
                    UPCECode += manufacturer.Substring(0, 3); //first three of manufacturer
                    UPCECode += productCode.Substring(3, 2); //last two of product
                    UPCECode += "3"; //number 3
                }//else if
                else if (manufacturer.EndsWith("0") && Int32.Parse(productCode) <= 9)
                {
                    //rule 3
                    UPCECode += manufacturer.Substring(0, 4); //first four of manufacturer
                    UPCECode += productCode[4]; //last digit of product
                    UPCECode += "4"; //number 4
                }//else if
                else if (!manufacturer.EndsWith("0") && Int32.Parse(productCode) <= 9 && Int32.Parse(productCode) >= 5)
                {
                    //rule 4
                    UPCECode += manufacturer; //manufacturer
                    UPCECode += productCode[4]; //last digit of product
                }//else if
                else
                    Error("EUPCE-4: Illegal UPC-A entered for conversion.  Unable to convert.");

                Raw_Data = UPCECode;
            }//if

            //get encoding pattern 
            var pattern = "";

            if (numberSystem == 0) pattern = UPC_E_Code0[CheckDigit];
            else pattern = UPC_E_Code1[CheckDigit];

            //encode the data
            var result = "101";

            var pos = 0;
            foreach (var c in pattern)
            {
                var i = Int32.Parse(Raw_Data[pos++].ToString());
                if (c == 'a')
                {
                    result += EAN_Code_A[i];
                }//if
                else if (c == 'b')
                {
                    result += EAN_Code_B[i];
                }//else if
            }//foreach

            //guard bars
            result += "01010";

            //end bars
            result += "1";

            return result;
        }//Encode_UPCE

        #region IBarcode Members

        public string Encoded_Value => Encode_UPCE();

        #endregion
    }
}
