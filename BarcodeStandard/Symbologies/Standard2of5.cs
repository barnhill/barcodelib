using System;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Standard 2 of 5 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Standard2of5 : BarcodeCommon, IBarcode
    {
        private readonly string[] S25_Code = { "10101110111010", "11101010101110", "10111010101110", "11101110101010", "10101110101110", "11101011101010", "10111011101010", "10101011101110", "11101010111010", "10111010111010" };
        private readonly TYPE Encoded_Type = TYPE.UNSPECIFIED;

        public Standard2of5(string input, TYPE EncodedType)
        {
            Raw_Data = input;
            Encoded_Type = EncodedType;
        }//Standard2of5

        /// <summary>
        /// Encode the raw data using the Standard 2 of 5 algorithm.
        /// </summary>
        private string Encode_Standard2of5()
        {
            if (!CheckNumericOnly(Raw_Data))
                Error("ES25-1: Numeric Data Only");

            string result = "11011010";

            foreach (char c in Raw_Data)
            {
                result += S25_Code[Int32.Parse(c.ToString())];
            }//foreach

            result += Encoded_Type == TYPE.Standard2of5_Mod10 ? S25_Code[CalculateMod10CheckDigit()] : "";

            //add ending bars
            result += "1101011";
            return result;
        }//Encode_Standard2of5

        private int CalculateMod10CheckDigit()
        {
            int sum = 0;
            bool even = true;
            for (int i = Raw_Data.Length - 1; i >= 0; --i)
            {
                sum += Raw_Data[i] * (even ? 3 : 1);
                even = !even;
            }

            return sum % 10;
        }

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_Standard2of5(); }
        }

        #endregion
    }
}
