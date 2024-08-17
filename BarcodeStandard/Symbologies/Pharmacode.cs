using System;

namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Pharmacode encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Pharmacode : BarcodeCommon, IBarcode
    {
        readonly string _thinBar = "1";
        readonly string _gap = "00";
        readonly string _thickBar = "111";

        /// <summary>
        /// Encodes with Pharmacode.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        internal Pharmacode(string input)
        {
            RawData = input;

            if (!IsNumericOnly(RawData))
            {
                Error("EPHARM-1: Data contains invalid  characters (non-numeric).");
            }
            else if (RawData.Length > 6)
            {
                Error("EPHARM-2: Data too long (invalid data input length).");
            }
        }

        /// <summary>
        /// Encode the raw data using the Pharmacode algorithm.
        /// </summary>
        private string Encode_Pharmacode()
        {
            if (!Int32.TryParse(RawData, out int num))
            {
                Error("EPHARM-3: Input is unparseable.");
            }
            else if (num < 3 || num > 131070)
            {
                Error("EPHARM-4: Data contains invalid  characters (invalid numeric range).");
            }

            var result = String.Empty;
            do
            {
                if ((num & 1) == 0)
                {
                    result = _thickBar + result;
                    num = (num - 2) / 2;
                }
                else
                {
                    result = _thinBar + result;
                    num = (num - 1) / 2;
                }

                if (num != 0)
                {
                    result = _gap + result;
                }
            } while (num != 0);

            return result;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Pharmacode();

        #endregion
    }
}
