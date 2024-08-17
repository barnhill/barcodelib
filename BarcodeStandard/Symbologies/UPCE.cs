using System;
using System.Linq;
using System.Text;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  UPC-E encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class UPCE : BarcodeCommon, IBarcode
    {
        private readonly string[] EAN_Code_A = ["0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011"];
        private readonly string[] EAN_Code_B = ["0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111"];
        private readonly string[] UPC_E_Code0 = ["bbbaaa", "bbabaa", "bbaaba", "bbaaab", "babbaa", "baabba", "baaabb", "bababa", "babaab", "baabab"];
        private readonly string[] UPC_E_Code1 = ["aaabbb", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba"];

        /// <summary>
        /// Encodes a UPC-E symbol.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        internal UPCE(string input)
        {
            RawData = input;
        }//UPCE
        /// <summary>
        /// Encode the raw data using the UPC-E algorithm.
        /// </summary>
        private string Encode_UPCE()
        {
            if (RawData.Length != 6 && RawData.Length != 8 && RawData.Length != 12)
            {
                Error("EUPCE-1: Invalid data length. (6, 8 or 12 numbers only)");
            }

            int numberSystem = 0;
            //check numeric only
            if (!IsNumericOnly(RawData))
            {
                Error("EUPCE-2: Numeric Data Only");
            }

            if (RawData.Length == 8)
            {
                //strip check digit off to recalculate
                RawData = RawData.Substring(1, 6);
            }

            //Convert to UPC-E from UPC-A if necessary
            if (RawData.Length == 12)
            {
                numberSystem = Int16.Parse(RawData[0].ToString());
                RawData = ConvertUPCAToUPCE();
            }

            int checkDigit = Int16.Parse(CalculateCheckDigit(ConvertUPCEToUPCA(RawData)));

            //get encoding pattern 
            String pattern = GetPattern(checkDigit, numberSystem);

            //encode the data
            StringBuilder result = new("101");

            int pos = 0;
            foreach (char c in pattern)
            {
                int i = Int16.Parse(RawData[pos++].ToString());
                if (c == 'a')
                {
                    result.Append(EAN_Code_A[i]);
                }
                else if (c == 'b')
                {
                    result.Append(EAN_Code_B[i]);
                }
            }

            //end-guard bars
            result.Append("010101");

            return result.ToString();
        }

        private String GetPattern(int checkDigit, int numberSystem)
        {
            if (numberSystem != 0 && numberSystem != 1)
            {
                Error("EUPCE-3: Invalid Number System (only 0 & 1 are valid)");
            }

            String pattern;

            if (numberSystem == 0)
            {
                pattern = UPC_E_Code0[checkDigit];
            }
            else
            {
                pattern = UPC_E_Code1[checkDigit];
            }

            return pattern;
        }

        private String ConvertUPCAToUPCE()
        {
            String UPCECode = "";

            //break apart into components
            String manufacturer = RawData.Substring(1, 5);
            String productCode = RawData.Substring(6, 5);

            int numericProductCode = Int32.Parse(productCode);

            if ((manufacturer.EndsWith("000") || manufacturer.EndsWith("100") || manufacturer.EndsWith("200")) && numericProductCode <= 999)
            {
                //rule 1
                UPCECode += manufacturer.Substring(0, 2); //first two of manufacturer
                UPCECode += productCode.Substring(2, 3); //last three of product
                UPCECode += manufacturer[2]; //third of manufacturer
            }
            else if (manufacturer.EndsWith("00") && numericProductCode <= 99)
            {
                //rule 2
                UPCECode += manufacturer.Substring(0, 3); //first three of manufacturer
                UPCECode += productCode.Substring(3, 2); //last two of product
                UPCECode += "3"; //number 3
            }
            else if (manufacturer.EndsWith("0") && numericProductCode <= 9)
            {
                //rule 3
                UPCECode += manufacturer.Substring(0, 4); //first four of manufacturer
                UPCECode += productCode[4]; //last digit of product
                UPCECode += "4"; //number 4
            }
            else if (!manufacturer.EndsWith("0") && numericProductCode <= 9 && numericProductCode >= 5)
            {
                //rule 4
                UPCECode += manufacturer; //manufacturer
                UPCECode += productCode[4]; //last digit of product
            }
            else
            {
                Error("EUPCE-4: Illegal UPC-A entered for conversion.  Unable to convert.");
            }

            return UPCECode;
        }

        private String ConvertUPCEToUPCA(String UPCECode)
        {
            String UPCACode = "0";
            if (UPCECode.EndsWith("0") || UPCECode.EndsWith("1") || UPCECode.EndsWith("2"))
            {
                //rule 1
                UPCACode += UPCECode.Substring(0, 2) + UPCECode[5] + "00"; //manufacturer
                UPCACode += "00" + UPCECode.Substring(2, 3); //product
            }
            else if (UPCECode.EndsWith("3"))
            {
                //rule 2
                UPCACode += UPCECode.Substring(0, 3) + "00"; //manufacturer
                UPCACode += "000" + UPCECode.Substring(3, 2); //product
            }
            else if (UPCECode.EndsWith("4"))
            {
                //rule 3
                UPCACode += UPCECode.Substring(0, 4) + "0"; //manufacturer
                UPCACode += "0000" + UPCECode[4]; //product
            }
            else
            {
                //rule 4
                UPCACode += UPCECode.Substring(0, 5); //manufacturer
                UPCACode += "0000" + UPCECode[5]; //product
            }

            return UPCACode;
        }

        private String CalculateCheckDigit(String upcA)
        {
            int cs = 0;
            try
            {
                //calculate check digit
                int sum = 0;

                for (int i = 0; i < upcA.Length; i++)
                {
                    int parseInt = Int16.Parse(upcA.Substring(i, 1));
                    if (i % 2 == 0)
                    {
                        sum += parseInt * 3;
                    }
                    else
                    {
                        sum += parseInt;
                    }
                }

                cs = (10 - sum % 10) % 10;
            }
            catch (Exception)
            {
                Error("EUPCE-5: Error calculating check digit.");
            }

            return cs.ToString();
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_UPCE();

        #endregion
    }
}
