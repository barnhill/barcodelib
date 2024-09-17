namespace BarcodeStandard.Symbologies
{
    internal class IATA2of5 : BarcodeCommon, IBarcode
    {
        private readonly string[] IATA2of5_Code = ["10101110111010", "11101010101110", "10111010101110", "11101110101010", "10101110101110", "11101011101010", "10111011101010", "10101011101110", "11101010111010", "10111010111010"];

        internal IATA2of5(string input)
        {
            RawData = input;
        }

        /// <summary>
        /// Encode the raw data using the IATA 2 of 5 algorithm.
        /// </summary>
        private string Encode_IATA2of5()
        {
            //check length of input
            if (RawData.Length > 17 || RawData.Length < 16)
                Error("EIATA25-1: Data length invalid. (Length must be 16 or 17)");

            if (!IsNumericOnly(RawData))
                Error("EIATA25-2: Numeric Data Only");

            //strip check digit if provided so it can be recalculated
            RawData = RawData.Length == 17 ? RawData.Substring(0, 16) : RawData;

            //calculate check digit
            var checkdigit = CalculateMod10CheckDigit();
            RawData += checkdigit.ToString();

            var result = "1010";

            for (int i = 0; i < RawData.Length; i++)
            {
                result += IATA2of5_Code[(int)char.GetNumericValue(RawData, i)];
            }

            //add check digit
            result += IATA2of5_Code[checkdigit];

            //add ending bars
            result += "01101";
            return result;
        }//Encode_Standard2of5

        private int CalculateMod10CheckDigit()
        {
            var sum = 0;
            var even = true;

            for (var i = RawData.Length - 1; i >= 0; --i)
            {
                //convert numeric in char format to integer and
                //multiply by 3 or 1 based on if an even index from the end
                sum += (RawData[i] - '0') * (even ? 3 : 1);
                even = !even;
            }

            return (10 - sum % 10) % 10;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_IATA2of5();

        #endregion
    }
}
