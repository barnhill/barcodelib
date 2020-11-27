using System;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Pharmacode encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Pharmacode: BarcodeCommon, IBarcode
    {
        string _thinBar = "1";
        string _gap = "00";
        string _thickBar = "111";

        /// <summary>
        /// Encodes with Pharmacode.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        public Pharmacode(string input)
        {
            Raw_Data = input;

            if (!CheckNumericOnly(Raw_Data))
            {
                Error("EPHARM-1: Data contains invalid  characters (non-numeric).");
            }//if
            else if (Raw_Data.Length > 6)
            {
                Error("EPHARM-2: Data too long (invalid data input length).");
            }//if
        }

        /// <summary>
        /// Encode the raw data using the Pharmacode algorithm.
        /// </summary>
        private string Encode_Pharmacode()
        {
            int num;

            if (!Int32.TryParse(Raw_Data, out num))
            {
                Error("EPHARM-3: Input is unparseable.");
            }
            else if (num < 3 || num > 131070)
            {
                Error("EPHARM-4: Data contains invalid  characters (invalid numeric range).");
            }//if

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
