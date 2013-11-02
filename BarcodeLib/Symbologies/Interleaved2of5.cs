using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Interleaved 2 of 5 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Interleaved2of5 : BarcodeCommon, IBarcode
    {
        private string[] I25_Code = { "NNWWN", "WNNNW", "NWNNW", "WWNNN", "NNWNW", "WNWNN", "NWWNN", "NNNWW", "WNNWN", "NWNWN" };

        public Interleaved2of5(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the Interleaved 2 of 5 algorithm.
        /// </summary>
        private string Encode_Interleaved2of5()
        {
            //check length of input
            if (Raw_Data.Length % 2 != 0)
                Error("EI25-1: Data length invalid.");

            if (!CheckNumericOnly(Raw_Data))
                Error("EI25-2: Numeric Data Only");

            string result = "1010";

            for (int i = 0; i < Raw_Data.Length; i += 2)
            {
                bool bars = true;
                string patternbars = I25_Code[Int32.Parse(Raw_Data[i].ToString())];
                string patternspaces = I25_Code[Int32.Parse(Raw_Data[i + 1].ToString())];
                string patternmixed = "";

                //interleave
                while (patternbars.Length > 0)
                {
                    patternmixed += patternbars[0].ToString() + patternspaces[0].ToString();
                    patternbars = patternbars.Substring(1);
                    patternspaces = patternspaces.Substring(1);
                }//while

                foreach (char c1 in patternmixed)
                {
                    if (bars)
                    {
                        if (c1 == 'N')
                            result += "1";
                        else
                            result += "11";
                    }//if
                    else
                    {
                        if (c1 == 'N')
                            result += "0";
                        else
                            result += "00";
                    }//else
                    bars = !bars;
                }//foreach

            }//foreach

            //add ending bars
            result += "1101";
            return result;
        }//Encode_Interleaved2of5

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return this.Encode_Interleaved2of5(); }
        }

        #endregion
    }
}
