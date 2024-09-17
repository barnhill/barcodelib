namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Standard 2 of 5 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Standard2of5 : BarcodeCommon, IBarcode
    {
        private readonly string[] S25_Code = ["10101110111010", "11101010101110", "10111010101110", "11101110101010", "10101110101110", "11101011101010", "10111011101010", "10101011101110", "11101010111010", "10111010111010"];
        private readonly Type _encodedType = Type.Unspecified;

        internal Standard2of5(string input, Type encodedType)
        {
            RawData = input;
            _encodedType = encodedType;
        }

        /// <summary>
        /// Encode the raw data using the Standard 2 of 5 algorithm.
        /// </summary>
        private string Encode_Standard2of5()
        {
            if (!IsNumericOnly(RawData))
                Error("ES25-1: Numeric Data Only");

            var result = "11011010";

            for (int i = 0; i < RawData.Length; i++)
            {
                result += S25_Code[(int)char.GetNumericValue(RawData, i)];
            }

            result += _encodedType == Type.Standard2Of5Mod10 ? S25_Code[CalculateMod10CheckDigit()] : "";

            //add ending bars
            result += "1101011";
            return result;
        }

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

        public string Encoded_Value => Encode_Standard2of5();

        #endregion
    }
}
