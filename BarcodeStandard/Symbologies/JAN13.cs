namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  JAN-13 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class JAN13 : BarcodeCommon, IBarcode
    {
        internal JAN13(string input)
        {
            RawData = input;
        }
        /// <summary>
        /// Encode the raw data using the JAN-13 algorithm.
        /// </summary>
        private string Encode_JAN13()
        {
            if (!RawData.StartsWith("49")) Error("EJAN13-1: Invalid Country Code for JAN13 (49 required)");
            if (!IsNumericOnly(RawData))
                Error("EJAN13-2: Numeric Data Only");

            EAN13 ean13 = new(RawData);
            return ean13.Encoded_Value;
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_JAN13();

        #endregion
    }
}
