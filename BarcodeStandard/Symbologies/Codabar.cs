namespace BarcodeStandard.Symbologies
{
    /// <summary>
    ///  Codabar encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Codabar : BarcodeCommon, IBarcode
    {
        private readonly System.Collections.Hashtable Codabar_Code = []; //is initialized by init_Codabar()

        internal Codabar(string input)
        {
            RawData = input;
        }

        /// <summary>
        /// Encode the raw data using the Codabar algorithm.
        /// </summary>
        private string Encode_Codabar()
        {
            if (RawData.Length < 2) Error("ECODABAR-1: Data format invalid. (Invalid length)");

            //check first char to make sure its a start/stop char
            switch (RawData[0].ToString().ToUpper().Trim())
            {
                case "A": break;
                case "B": break;
                case "C": break;
                case "D": break;
                default: Error("ECODABAR-2: Data format invalid. (Invalid START character)");
                    break;
            }

            //check the ending char to make sure its a start/stop char
            switch (RawData[RawData.Trim().Length - 1].ToString().ToUpper().Trim())
            {
                case "A": break;
                case "B": break;
                case "C": break;
                case "D": break;
                default: Error("ECODABAR-3: Data format invalid. (Invalid STOP character)");
                    break;
            }

            //populate the hashtable to begin the process
            Init_Codabar();

            //replace non-numeric VALID chars with empty strings before checking for all numerics
            var temp = RawData;

            foreach (char c in Codabar_Code.Keys)
            {
                if (!IsNumericOnly(c.ToString()))
                {
                    temp = temp.Replace(c, '1');
                }
            }

            //now that all the valid non-numeric chars have been replaced with a number check if all numeric exist
            if (!IsNumericOnly(temp))
                Error("ECODABAR-4: Data contains invalid  characters.");

            var result = "";

            foreach (var c in RawData)
            {
                result += Codabar_Code[c].ToString();
                result += "0"; //inter-character space
            }

            //remove the extra 0 at the end of the result
            result = result.Remove(result.Length - 1);

            //clears the hashtable so it no longer takes up memory
            Codabar_Code.Clear();

            //change the Raw_Data to strip out the start stop chars for label purposes
            RawData = RawData.Trim().Substring(1, RawData.Trim().Length - 2);

            return result;
        }
        private void Init_Codabar()
        {
            Codabar_Code.Clear();
            Codabar_Code.Add('0', "101010011");
            Codabar_Code.Add('1', "101011001");
            Codabar_Code.Add('2', "101001011");
            Codabar_Code.Add('3', "110010101");
            Codabar_Code.Add('4', "101101001");
            Codabar_Code.Add('5', "110101001");
            Codabar_Code.Add('6', "100101011");
            Codabar_Code.Add('7', "100101101");
            Codabar_Code.Add('8', "100110101");
            Codabar_Code.Add('9', "110100101");
            Codabar_Code.Add('-', "101001101");
            Codabar_Code.Add('$', "101100101");
            Codabar_Code.Add(':', "1101011011");
            Codabar_Code.Add('/', "1101101011");
            Codabar_Code.Add('.', "1101101101");
            Codabar_Code.Add('+', "1011011011");
            Codabar_Code.Add('A', "1011001001");
            Codabar_Code.Add('B', "1001001011");
            Codabar_Code.Add('C', "1010010011");
            Codabar_Code.Add('D', "1010011001");
            Codabar_Code.Add('a', "1011001001");
            Codabar_Code.Add('b', "1010010011");
            Codabar_Code.Add('c', "1001001011");
            Codabar_Code.Add('d', "1010011001");
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_Codabar();

        #endregion

    }
}
