using System;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Interleaved 2 of 5 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Interleaved2of5 : BarcodeCommon, IBarcode
    {
        private readonly string[] _i25Code = ["NNWWN", "WNNNW", "NWNNW", "WWNNN", "NNWNW", "WNWNN", "NWWNN", "NNNWW", "WNNWN", "NWNWN"];
        private readonly Type _encodedType;

        internal Interleaved2of5(string input, Type encodedType)
        {
            _encodedType = encodedType;
            RawData = input;
        }
        /// <summary>
        /// Encode the raw data using the Interleaved 2 of 5 algorithm.
        /// </summary>
        private string Encode_Interleaved2of5()
        {
            //check length of input (only even if no checkdigit, else with check digit odd)
            if (RawData.Length % 2 != (_encodedType == Type.Interleaved2Of5Mod10 ? 1 : 0))
                Error("EI25-1: Data length invalid.");

            if (!IsNumericOnly(RawData))
                Error("EI25-2: Numeric Data Only");
            
            var result = "1010";
            var data = RawData + (_encodedType == Type.Interleaved2Of5Mod10 ? CalculateMod10CheckDigit().ToString() : "");

            for (int i = 0; i < data.Length; i += 2)
            {
                var bars = true;
                var patternbars = _i25Code[(int)char.GetNumericValue(data, i)];
                var patternspaces = _i25Code[(int)char.GetNumericValue(data, i + 1)];
                var patternmixed = "";

                //interleave
                while (patternbars.Length > 0)
                {
                    patternmixed += patternbars[0].ToString() + patternspaces[0].ToString();
                    patternbars = patternbars.Substring(1);
                    patternspaces = patternspaces.Substring(1);
                }

                foreach (char c1 in patternmixed)
                {
                    if (bars)
                    {
                        if (c1 == 'N')
                            result += "1";
                        else
                            result += "11";
                    }
                    else
                    {
                        if (c1 == 'N')
                            result += "0";
                        else
                            result += "00";
                    }
                    bars = !bars;
                }
            }
            
            //add ending bars
            result += "1101";
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

        public string Encoded_Value => this.Encode_Interleaved2of5();

        #endregion
    }
}
