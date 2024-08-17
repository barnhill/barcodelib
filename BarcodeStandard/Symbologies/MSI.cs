using System;

namespace BarcodeStandard.Symbologies
{
    internal class MSI : BarcodeCommon, IBarcode
    {
        /// <summary>
        ///  MSI encoding
        ///  Written by: Brad Barnhill
        /// </summary>
        private readonly string[] MSI_Code = ["100100100100", "100100100110", "100100110100", "100100110110", "100110100100", "100110100110", "100110110100", "100110110110", "110100100100", "110100100110"];
        private readonly Type Encoded_Type = Type.Unspecified;

        internal MSI(string input, Type encodedType)
        {
            Encoded_Type = encodedType;
            RawData = input;
        }

        /// <summary>
        /// Encode the raw data using the MSI algorithm.
        /// </summary>
        private string Encode_MSI()
        {
            //check for non-numeric chars
            if (!IsNumericOnly(RawData))
                Error("EMSI-1: Numeric Data Only");

            //get checksum
            string withChecksum = Encoded_Type switch
            {
                Type.MsiMod10 => Mod10(RawData),
                Type.Msi2Mod10 => Mod10(Mod10(RawData)),
                Type.MsiMod11 => Mod11(RawData),
                Type.MsiMod11Mod10 => Mod10(Mod11(RawData)),
                _ => null,
            };

            if (String.IsNullOrEmpty(withChecksum)) 
                Error("EMSI-2: Invalid MSI encoding type");

            var result = "110";
            foreach (var c in withChecksum)
            {
                result += MSI_Code[Int32.Parse(c.ToString())];
            }

            //add stop character
            result += "1001";

            return result;
        }

        private string Mod10(string code)
        {
            var odds = "";
            var evens = "";
            for (var i = code.Length - 1; i >= 0; i -= 2)
            {
                odds = code[i] + odds;
                if (i - 1 >= 0)
                    evens = code[i - 1] + evens;
            }

            //multiply odds by 2
            odds = Convert.ToString((Int32.Parse(odds) * 2));

            var evensum = 0;
            var oddsum = 0;
            foreach (var c in evens)
                evensum += Int32.Parse(c.ToString());
            foreach (var c in odds)
                oddsum += Int32.Parse(c.ToString());
            var mod = (oddsum + evensum) % 10;
            var checksum = mod == 0 ? 0 : 10 - mod;
            return code + checksum.ToString();
        }

        private string Mod11(string code)
        {
            var sum = 0;
            var weight = 2;
            for (var i = code.Length - 1; i >= 0; i--)
            {
                if (weight > 7) weight = 2;
                sum += Int32.Parse(code[i].ToString()) * weight++;
            }
            var mod = sum % 11;
            var checksum = mod == 0 ? 0 : 11 - mod;

            return code + checksum.ToString();
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_MSI();

        #endregion
    }
}
