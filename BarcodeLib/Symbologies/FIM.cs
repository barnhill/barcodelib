using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  FIM encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class FIM: BarcodeCommon, IBarcode
    {
        private string[] FIM_Codes = { "110010011", "101101101", "110101011", "111010111" };
        public enum FIMTypes {FIM_A = 0, FIM_B, FIM_C, FIM_D};

        public FIM(string input)
        {
            input = input.Trim();

            switch (input)
            {
                case "A":
                case "a": Raw_Data = FIM_Codes[(int)FIMTypes.FIM_A];
                    break;
                case "B":
                case "b": Raw_Data = FIM_Codes[(int)FIMTypes.FIM_B];
                    break;
                case "C":
                case "c": Raw_Data = FIM_Codes[(int)FIMTypes.FIM_C];
                    break;
                case "D":
                case "d": Raw_Data = FIM_Codes[(int)FIMTypes.FIM_D];
                    break;
                default: Error("EFIM-1: Could not determine encoding type. (Only pass in A, B, C, or D)");
                    break;
            }//switch
        }

        public string Encode_FIM()
        {
            string Encoded = "";
            foreach (char c in RawData)
            {
                Encoded += c + "0";
            }//foreach

            Encoded = Encoded.Substring(0, Encoded.Length - 1);

            return Encoded;
        }

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_FIM(); }
        }

        #endregion
    }
}
