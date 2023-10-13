/*using BarcodeStandard;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Codabar encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Codabar : BarcodeCommon, IBarcode
    {
        private readonly System.Collections.Hashtable Codabar_Code = new System.Collections.Hashtable(); //is initialized by init_Codabar()

        public Codabar(string input)
        {
            RawData = input;
        }//Codabar

        /// <summary>
        /// Encode the raw data using the Codabar algorithm.
        /// </summary>
        private string Encode_Codabar()
        {
            if (RawData.Length < 2) Error("ECODABAR-1: Data format invalid. (Invalid length)");

            //check first char to make sure its a start/stop char
            switch (RawData[0].ToString().ToUpper().Trim())
            {
                case "A": break;
                case "B": break;
                case "C": break;
                case "D": break;
                default:
                    Error("ECODABAR-2: Data format invalid. (Invalid START character)");
                    break;
            }//switch

            //check the ending char to make sure its a start/stop char
            switch (RawData[RawData.Trim().Length - 1].ToString().ToUpper().Trim())
            {
                case "A": break;
                case "B": break;
                case "C": break;
                case "D": break;
                default:
                    Error("ECODABAR-3: Data format invalid. (Invalid STOP character)");
                    break;
            }//switch

            //populate the hashtable to begin the process
            init_Codabar();

            //replace non-numeric VALID chars with empty strings before checking for all numerics
            var temp = RawData;

            foreach (char c in Codabar_Code.Keys)
            {
                if (!CheckNumericOnly(c.ToString()))
                {
                    temp = temp.Replace(c, '1');
                }//if
            }//if

            //now that all the valid non-numeric chars have been replaced with a number check if all numeric exist
            if (!CheckNumericOnly(temp))
                Error("ECODABAR-4: Data contains invalid  characters.");

            var result = "";

            foreach (var c in RawData)
            {
                result += Codabar_Code[c].ToString();
                result += "0"; //inter-character space
            }//foreach

            //remove the extra 0 at the end of the result
            result = result.Remove(result.Length - 1);

            //clears the hashtable so it no longer takes up memory
            Codabar_Code.Clear();

            //change the Raw_Data to strip out the start stop chars for label purposes
            RawData = RawData.Trim().Substring(1, RawData.Trim().Length - 2);

            return result;
        }//Encode_Codabar
        private void init_Codabar()
        {
            Codabar_Code.Clear();
            Codabar_Code.Add('0', "101010011");
            Codabar_Code.Add('1', "101011001");
            Codabar_Code.Add('2', "101001011");
            Codabar_Code.Add('3', "110010101");
            Codabar_Code.Add('4', "101101001");
            Codabar_Code.Add('5', "110101001");
            Codabar_Code.Add('6', "100101011");
            Codabar_Code.Add('7', "100101101");
            Codabar_Code.Add('8', "100110101");
            Codabar_Code.Add('9', "110100101");
            Codabar_Code.Add('-', "101001101");
            Codabar_Code.Add('$', "101100101");
            Codabar_Code.Add(':', "1101011011");
            Codabar_Code.Add('/', "1101101011");
            Codabar_Code.Add('.', "1101101101");
            Codabar_Code.Add('+', "1011011011");
            Codabar_Code.Add('A', "1011001001");
            Codabar_Code.Add('B', "1001001011");
            Codabar_Code.Add('C', "1010010011");
            Codabar_Code.Add('D', "1010011001");
            Codabar_Code.Add('a', "1011001001");
            Codabar_Code.Add('b', "1010010011");
            Codabar_Code.Add('c', "1001001011");
            Codabar_Code.Add('d', "1010011001");
        }//init_Codeabar

        #region IBarcode Members

        public string Encoded_Value => Encode_Codabar();

        #endregion

    }//class
}//namespace
*/
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
            //Codabar_Code.Clear();
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
