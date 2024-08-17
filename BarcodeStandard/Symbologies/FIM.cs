namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  FIM encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class FIM : BarcodeCommon, IBarcode
    {
        private readonly string[] FIM_Codes = ["110010011", "101101101", "110101011", "111010111", "101000101"];
        public enum FIMTypes {FIM_A = 0, FIM_B, FIM_C, FIM_D, FIM_E};

        internal FIM(string input)
        {
            input = input.Trim();

            switch (input)
            {
                case "A":
                case "a": RawData = FIM_Codes[(int)FIMTypes.FIM_A];
                    break;
                case "B":
                case "b": RawData = FIM_Codes[(int)FIMTypes.FIM_B];
                    break;
                case "C":
                case "c": RawData = FIM_Codes[(int)FIMTypes.FIM_C];
                    break;
                case "D":
                case "d": RawData = FIM_Codes[(int)FIMTypes.FIM_D];
                    break;
                case "E":
                case "e": RawData = FIM_Codes[(int)FIMTypes.FIM_E];
                    break;
                default: Error("EFIM-1: Could not determine encoding type. (Only pass in A, B, C, D, or E)");
                    break;
            }
        }

        internal string Encode_FIM()
        {
            string encoded = "";
            foreach (char c in RawData)
            {
                encoded += c + "0";
            }

            encoded = encoded.Substring(0, encoded.Length - 1);

            return encoded;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_FIM();

        #endregion
    }
}
