using BarcodeStandard;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  JAN-13 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class JAN13 : BarcodeCommon, IBarcode
    {
        private readonly EAN13 _ean13;

        public JAN13(string input)
        {
            _ean13 = new EAN13(input);
            RawData = _ean13.RawData;
        }

        /// <summary>
        /// Encode the raw data using the JAN-13 algorithm.
        /// </summary>
        private string Encode_JAN13()
        {
            if (!RawData.StartsWith("49")) Error("EJAN13-1: Invalid Country Code for JAN13 (49 required)");
            if (!CheckNumericOnly(RawData))
                Error("EJAN13-2: Numeric Data Only");

            return _ean13.Encoded_Value;
        }//Encode_JAN13

        #region IBarcode Members

        public string Encoded_Value => Encode_JAN13();

        #endregion IBarcode Members
    }
}