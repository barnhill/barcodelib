using BarcodeStandard;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Codabar encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Codabar : BarcodeCommon, IBarcode
    {
        private static readonly Dictionary<char, string> Codabar_Code = new Dictionary<char, string>();

        static Codabar()
        {
            InitCodabar();
        }

        public Codabar(string input)
        {
            RawData = input;
        }

        /// <summary>
        /// Encode the raw data using the Codabar algorithm.
        /// </summary>
        private string Encode_Codabar()
        {
            if (RawData.Length < 2) Error("ECODABAR-1: Invalid length.");

            // Check that the start & stop is valid
            ThrowIfInvalidStartOrStop((char)(RawData[0] & ~32));
            ThrowIfInvalidStartOrStop((char)(RawData[RawData.Length - 1] & ~32));

            var temp = RawData.Substring(1, RawData.Length - 2);

            // Check that chars are numeric
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] < '0' || temp[i] > '9')
                {
                    Error($"ECODABAR-4: Data contains invalid character [{temp[i]}].");
                }
            }

            var result = "";
            for (int i = 0; i < RawData.Length; i++)
            {
                result += Codabar_Code[RawData[i]];
                result += "0"; // Inter character space
            }

            // Remove the extra 0 at the end of the result
            result = result.Remove(result.Length - 1);

            // Change the Raw_Data to strip out the start stop chars for label purposes
            RawData = temp;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfInvalidStartOrStop(char c)
        {
            if (c < 'A' || c > 'D') Error($"ECODABAR-2: Data format invalid. (Invalid start or stop character [{c}])");
        }

        private static void InitCodabar()
        {
            Codabar_Code['0'] = "101010011";
            Codabar_Code['1'] = "101011001";
            Codabar_Code['2'] = "101001011";
            Codabar_Code['3'] = "110010101";
            Codabar_Code['4'] = "101101001";
            Codabar_Code['5'] = "110101001";
            Codabar_Code['6'] = "100101011";
            Codabar_Code['7'] = "100101101";
            Codabar_Code['8'] = "100110101";
            Codabar_Code['9'] = "110100101";
            Codabar_Code['-'] = "101001101";
            Codabar_Code['$'] = "101100101";
            Codabar_Code[':'] = "1101011011";
            Codabar_Code['/'] = "1101101011";
            Codabar_Code['.'] = "1101101101";
            Codabar_Code['+'] = "1011011011";
            Codabar_Code['A'] = "1011001001";
            Codabar_Code['B'] = "1001001011";
            Codabar_Code['C'] = "1010010011";
            Codabar_Code['D'] = "1010011001";
            Codabar_Code['a'] = "1011001001";
            Codabar_Code['b'] = "1010010011";
            Codabar_Code['c'] = "1001001011";
            Codabar_Code['d'] = "1010011001";
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Codabar();

        #endregion
    }
}
