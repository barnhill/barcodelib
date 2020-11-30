using System;
using System.Collections;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  UPC-A encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class UPCA : BarcodeCommon, IBarcode
    {
        private readonly string[] UPC_Code_A = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private readonly string[] UPC_Code_B = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private string _countryAssigningManufacturerCode = "N/A";
        private readonly Hashtable _countryCodes = new Hashtable(); //is initialized by init_CountryCodes()

        public UPCA(string input)
        {
            Raw_Data = input;
        }
        /// <summary>
        /// Encode the raw data using the UPC-A algorithm.
        /// </summary>
        private string Encode_UPCA()
        {
            //check length of input
            if (Raw_Data.Length != 11 && Raw_Data.Length != 12)
                Error("EUPCA-1: Data length invalid. (Length must be 11 or 12)");

            if (!CheckNumericOnly(Raw_Data))
                Error("EUPCA-2: Numeric Data Only");

            CheckDigit();
            
            var result = "101"; //start with guard bars

            //first number
            result += UPC_Code_A[Int32.Parse(Raw_Data[0].ToString())];

            //second (group) of numbers
            var pos = 0;
            while (pos < 5)
            {
                result += UPC_Code_A[Int32.Parse(Raw_Data[pos + 1].ToString())];
                pos++;
            }//while

            //add divider bars
            result += "01010";

            //third (group) of numbers
            pos = 0;
            while (pos < 5)
            {
                result += UPC_Code_B[Int32.Parse(Raw_Data[(pos++) + 6].ToString())];
            }//while

            //forth
            result += UPC_Code_B[Int32.Parse(Raw_Data[Raw_Data.Length - 1].ToString())];

            //add ending guard bars
            result += "101";

            //get the manufacturer assigning country
            init_CountryCodes();
            var twodigitCode = "0" + Raw_Data.Substring(0, 1);
            try
            {
                _countryAssigningManufacturerCode = _countryCodes[twodigitCode].ToString();
            }//try
            catch
            {
                Error("EUPCA-3: Country assigning manufacturer code not found.");
            }//catch
            finally { _countryCodes.Clear(); }

            return result;
        }//Encode_UPCA
        private void init_CountryCodes()
        {
            _countryCodes.Clear();
            _countryCodes.Add("00", "US / CANADA");
            _countryCodes.Add("01", "US / CANADA");
            _countryCodes.Add("02", "US / CANADA");
            _countryCodes.Add("03", "US / CANADA");
            _countryCodes.Add("04", "US / CANADA");
            _countryCodes.Add("05", "US / CANADA");
            _countryCodes.Add("06", "US / CANADA");
            _countryCodes.Add("07", "US / CANADA");
            _countryCodes.Add("08", "US / CANADA");
            _countryCodes.Add("09", "US / CANADA");
            _countryCodes.Add("10", "US / CANADA");
            _countryCodes.Add("11", "US / CANADA");
            _countryCodes.Add("12", "US / CANADA");
            _countryCodes.Add("13", "US / CANADA");

            _countryCodes.Add("20", "IN STORE");
            _countryCodes.Add("21", "IN STORE");
            _countryCodes.Add("22", "IN STORE");
            _countryCodes.Add("23", "IN STORE");
            _countryCodes.Add("24", "IN STORE");
            _countryCodes.Add("25", "IN STORE");
            _countryCodes.Add("26", "IN STORE");
            _countryCodes.Add("27", "IN STORE");
            _countryCodes.Add("28", "IN STORE");
            _countryCodes.Add("29", "IN STORE");

            _countryCodes.Add("30", "FRANCE");
            _countryCodes.Add("31", "FRANCE");
            _countryCodes.Add("32", "FRANCE");
            _countryCodes.Add("33", "FRANCE");
            _countryCodes.Add("34", "FRANCE");
            _countryCodes.Add("35", "FRANCE");
            _countryCodes.Add("36", "FRANCE");
            _countryCodes.Add("37", "FRANCE");

            _countryCodes.Add("40", "GERMANY");
            _countryCodes.Add("41", "GERMANY");
            _countryCodes.Add("42", "GERMANY");
            _countryCodes.Add("43", "GERMANY");
            _countryCodes.Add("44", "GERMANY");

            _countryCodes.Add("45", "JAPAN");
            _countryCodes.Add("46", "RUSSIAN FEDERATION");
            _countryCodes.Add("49", "JAPAN (JAN-13)");

            _countryCodes.Add("50", "UNITED KINGDOM");
            _countryCodes.Add("54", "BELGIUM / LUXEMBOURG");
            _countryCodes.Add("57", "DENMARK");

            _countryCodes.Add("64", "FINLAND");

            _countryCodes.Add("70", "NORWAY");
            _countryCodes.Add("73", "SWEDEN");
            _countryCodes.Add("76", "SWITZERLAND");

            _countryCodes.Add("80", "ITALY");
            _countryCodes.Add("81", "ITALY");
            _countryCodes.Add("82", "ITALY");
            _countryCodes.Add("83", "ITALY");
            _countryCodes.Add("84", "SPAIN");
            _countryCodes.Add("87", "NETHERLANDS");

            _countryCodes.Add("90", "AUSTRIA");
            _countryCodes.Add("91", "AUSTRIA");
            _countryCodes.Add("93", "AUSTRALIA");
            _countryCodes.Add("94", "NEW ZEALAND");
            _countryCodes.Add("99", "COUPONS");

            _countryCodes.Add("471", "TAIWAN");
            _countryCodes.Add("474", "ESTONIA");
            _countryCodes.Add("475", "LATVIA");
            _countryCodes.Add("477", "LITHUANIA");
            _countryCodes.Add("479", "SRI LANKA");
            _countryCodes.Add("480", "PHILIPPINES");
            _countryCodes.Add("482", "UKRAINE");
            _countryCodes.Add("484", "MOLDOVA");
            _countryCodes.Add("485", "ARMENIA");
            _countryCodes.Add("486", "GEORGIA");
            _countryCodes.Add("487", "KAZAKHSTAN");
            _countryCodes.Add("489", "HONG KONG");

            _countryCodes.Add("520", "GREECE");
            _countryCodes.Add("528", "LEBANON");
            _countryCodes.Add("529", "CYPRUS");
            _countryCodes.Add("531", "MACEDONIA");
            _countryCodes.Add("535", "MALTA");
            _countryCodes.Add("539", "IRELAND");
            _countryCodes.Add("560", "PORTUGAL");
            _countryCodes.Add("569", "ICELAND");
            _countryCodes.Add("590", "POLAND");
            _countryCodes.Add("594", "ROMANIA");
            _countryCodes.Add("599", "HUNGARY");

            _countryCodes.Add("600", "SOUTH AFRICA");
            _countryCodes.Add("601", "SOUTH AFRICA");
            _countryCodes.Add("609", "MAURITIUS");
            _countryCodes.Add("611", "MOROCCO");
            _countryCodes.Add("613", "ALGERIA");
            _countryCodes.Add("619", "TUNISIA");
            _countryCodes.Add("622", "EGYPT");
            _countryCodes.Add("625", "JORDAN");
            _countryCodes.Add("626", "IRAN");
            _countryCodes.Add("690", "CHINA");
            _countryCodes.Add("691", "CHINA");
            _countryCodes.Add("692", "CHINA");

            _countryCodes.Add("729", "ISRAEL");
            _countryCodes.Add("740", "GUATEMALA");
            _countryCodes.Add("741", "EL SALVADOR");
            _countryCodes.Add("742", "HONDURAS");
            _countryCodes.Add("743", "NICARAGUA");
            _countryCodes.Add("744", "COSTA RICA");
            _countryCodes.Add("746", "DOMINICAN REPUBLIC");
            _countryCodes.Add("750", "MEXICO");
            _countryCodes.Add("759", "VENEZUELA");
            _countryCodes.Add("770", "COLOMBIA");
            _countryCodes.Add("773", "URUGUAY");
            _countryCodes.Add("775", "PERU");
            _countryCodes.Add("777", "BOLIVIA");
            _countryCodes.Add("779", "ARGENTINA");
            _countryCodes.Add("780", "CHILE");
            _countryCodes.Add("784", "PARAGUAY");
            _countryCodes.Add("785", "PERU");
            _countryCodes.Add("786", "ECUADOR");
            _countryCodes.Add("789", "BRAZIL");

            _countryCodes.Add("850", "CUBA");
            _countryCodes.Add("858", "SLOVAKIA");
            _countryCodes.Add("859", "CZECH REPUBLIC");
            _countryCodes.Add("860", "YUGLOSLAVIA");
            _countryCodes.Add("869", "TURKEY");
            _countryCodes.Add("880", "SOUTH KOREA");
            _countryCodes.Add("885", "THAILAND");
            _countryCodes.Add("888", "SINGAPORE");
            _countryCodes.Add("890", "INDIA");
            _countryCodes.Add("893", "VIETNAM");
            _countryCodes.Add("899", "INDONESIA");

            _countryCodes.Add("955", "MALAYSIA");
            _countryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
            _countryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
            _countryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
            _countryCodes.Add("980", "REFUND RECEIPTS");
            _countryCodes.Add("981", "COMMON CURRENCY COUPONS");
            _countryCodes.Add("982", "COMMON CURRENCY COUPONS");
        }//init_CountryCodes
        private void CheckDigit()
        {
            try
            {
                var rawDataHolder = Raw_Data.Substring(0, 11);

                //calculate check digit
                var sum = 0;

                for (var i = 0; i < rawDataHolder.Length; i++)
                {
                    if (i % 2 == 0)
                        sum += Int32.Parse(rawDataHolder.Substring(i, 1)) * 3;
                    else
                        sum += Int32.Parse(rawDataHolder.Substring(i, 1));
                }//for

                int cs = (10 - sum % 10) % 10;

                //replace checksum if provided by the user and replace with the calculated checksum
                Raw_Data = rawDataHolder + cs;
            }//try
            catch
            {
                Error("EUPCA-4: Error calculating check digit.");
            }//catch
        }//CheckDigit

        #region IBarcode Members

        public string Encoded_Value => Encode_UPCA();

        #endregion
    }
}
