using System.Collections.Generic;

namespace BarcodeStandard
{
    /// <summary>
    ///  Barcode interface for symbology layout.
    ///  Written by: Brad Barnhill
    /// </summary>
    internal interface IBarcode
    {
        string Encoded_Value
        {
            get;
        }//Encoded_Value

        string RawData
        {
            get;
        }//Raw_Data

        List<string> Errors
        {
            get;
        }//Errors

    }//interface
}
