using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  UPC-A encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class UPCA : BarcodeCommon, IBarcode
    {
        private string[] UPC_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] UPC_CodeB = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private string _Country_Assigning_Manufacturer_Code = "N/A";
        private Hashtable CountryCodes = new Hashtable(); //is initialized by init_CountryCodes()

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
            
            string result = "101"; //start with guard bars

            //first number
            result += UPC_CodeA[Int32.Parse(Raw_Data[0].ToString())];

            //second (group) of numbers
            int pos = 0;
            while (pos < 5)
            {
                result += UPC_CodeA[Int32.Parse(Raw_Data[pos + 1].ToString())];
                pos++;
            }//while

            //add divider bars
            result += "01010";

            //third (group) of numbers
            pos = 0;
            while (pos < 5)
            {
                result += UPC_CodeB[Int32.Parse(Raw_Data[(pos++) + 6].ToString())];
            }//while

            //forth
            result += UPC_CodeB[Int32.Parse(Raw_Data[Raw_Data.Length - 1].ToString())];

            //add ending guard bars
            result += "101";

            //get the manufacturer assigning country
            this.init_CountryCodes();
            string twodigitCode = "0" + Raw_Data.Substring(0, 1);
            try
            {
                _Country_Assigning_Manufacturer_Code = CountryCodes[twodigitCode].ToString();
            }//try
            catch
            {
                Error("EUPCA-3: Country assigning manufacturer code not found.");
            }//catch
            finally { CountryCodes.Clear(); }

            return result;
        }//Encode_UPCA
        private void init_CountryCodes()
        {
            CountryCodes.Clear();
            CountryCodes.Add("00", "US / CANADA");
            CountryCodes.Add("01", "US / CANADA");
            CountryCodes.Add("02", "US / CANADA");
            CountryCodes.Add("03", "US / CANADA");
            CountryCodes.Add("04", "US / CANADA");
            CountryCodes.Add("05", "US / CANADA");
            CountryCodes.Add("06", "US / CANADA");
            CountryCodes.Add("07", "US / CANADA");
            CountryCodes.Add("08", "US / CANADA");
            CountryCodes.Add("09", "US / CANADA");
            CountryCodes.Add("10", "US / CANADA");
            CountryCodes.Add("11", "US / CANADA");
            CountryCodes.Add("12", "US / CANADA");
            CountryCodes.Add("13", "US / CANADA");

            CountryCodes.Add("20", "IN STORE");
            CountryCodes.Add("21", "IN STORE");
            CountryCodes.Add("22", "IN STORE");
            CountryCodes.Add("23", "IN STORE");
            CountryCodes.Add("24", "IN STORE");
            CountryCodes.Add("25", "IN STORE");
            CountryCodes.Add("26", "IN STORE");
            CountryCodes.Add("27", "IN STORE");
            CountryCodes.Add("28", "IN STORE");
            CountryCodes.Add("29", "IN STORE");

            CountryCodes.Add("30", "FRANCE");
            CountryCodes.Add("31", "FRANCE");
            CountryCodes.Add("32", "FRANCE");
            CountryCodes.Add("33", "FRANCE");
            CountryCodes.Add("34", "FRANCE");
            CountryCodes.Add("35", "FRANCE");
            CountryCodes.Add("36", "FRANCE");
            CountryCodes.Add("37", "FRANCE");

            CountryCodes.Add("40", "GERMANY");
            CountryCodes.Add("41", "GERMANY");
            CountryCodes.Add("42", "GERMANY");
            CountryCodes.Add("43", "GERMANY");
            CountryCodes.Add("44", "GERMANY");

            CountryCodes.Add("45", "JAPAN");
            CountryCodes.Add("46", "RUSSIAN FEDERATION");
            CountryCodes.Add("49", "JAPAN (JAN-13)");

            CountryCodes.Add("50", "UNITED KINGDOM");
            CountryCodes.Add("54", "BELGIUM / LUXEMBOURG");
            CountryCodes.Add("57", "DENMARK");

            CountryCodes.Add("64", "FINLAND");

            CountryCodes.Add("70", "NORWAY");
            CountryCodes.Add("73", "SWEDEN");
            CountryCodes.Add("76", "SWITZERLAND");

            CountryCodes.Add("80", "ITALY");
            CountryCodes.Add("81", "ITALY");
            CountryCodes.Add("82", "ITALY");
            CountryCodes.Add("83", "ITALY");
            CountryCodes.Add("84", "SPAIN");
            CountryCodes.Add("87", "NETHERLANDS");

            CountryCodes.Add("90", "AUSTRIA");
            CountryCodes.Add("91", "AUSTRIA");
            CountryCodes.Add("93", "AUSTRALIA");
            CountryCodes.Add("94", "NEW ZEALAND");
            CountryCodes.Add("99", "COUPONS");

            CountryCodes.Add("471", "TAIWAN");
            CountryCodes.Add("474", "ESTONIA");
            CountryCodes.Add("475", "LATVIA");
            CountryCodes.Add("477", "LITHUANIA");
            CountryCodes.Add("479", "SRI LANKA");
            CountryCodes.Add("480", "PHILIPPINES");
            CountryCodes.Add("482", "UKRAINE");
            CountryCodes.Add("484", "MOLDOVA");
            CountryCodes.Add("485", "ARMENIA");
            CountryCodes.Add("486", "GEORGIA");
            CountryCodes.Add("487", "KAZAKHSTAN");
            CountryCodes.Add("489", "HONG KONG");

            CountryCodes.Add("520", "GREECE");
            CountryCodes.Add("528", "LEBANON");
            CountryCodes.Add("529", "CYPRUS");
            CountryCodes.Add("531", "MACEDONIA");
            CountryCodes.Add("535", "MALTA");
            CountryCodes.Add("539", "IRELAND");
            CountryCodes.Add("560", "PORTUGAL");
            CountryCodes.Add("569", "ICELAND");
            CountryCodes.Add("590", "POLAND");
            CountryCodes.Add("594", "ROMANIA");
            CountryCodes.Add("599", "HUNGARY");

            CountryCodes.Add("600", "SOUTH AFRICA");
            CountryCodes.Add("601", "SOUTH AFRICA");
            CountryCodes.Add("609", "MAURITIUS");
            CountryCodes.Add("611", "MOROCCO");
            CountryCodes.Add("613", "ALGERIA");
            CountryCodes.Add("619", "TUNISIA");
            CountryCodes.Add("622", "EGYPT");
            CountryCodes.Add("625", "JORDAN");
            CountryCodes.Add("626", "IRAN");
            CountryCodes.Add("690", "CHINA");
            CountryCodes.Add("691", "CHINA");
            CountryCodes.Add("692", "CHINA");

            CountryCodes.Add("729", "ISRAEL");
            CountryCodes.Add("740", "GUATEMALA");
            CountryCodes.Add("741", "EL SALVADOR");
            CountryCodes.Add("742", "HONDURAS");
            CountryCodes.Add("743", "NICARAGUA");
            CountryCodes.Add("744", "COSTA RICA");
            CountryCodes.Add("746", "DOMINICAN REPUBLIC");
            CountryCodes.Add("750", "MEXICO");
            CountryCodes.Add("759", "VENEZUELA");
            CountryCodes.Add("770", "COLOMBIA");
            CountryCodes.Add("773", "URUGUAY");
            CountryCodes.Add("775", "PERU");
            CountryCodes.Add("777", "BOLIVIA");
            CountryCodes.Add("779", "ARGENTINA");
            CountryCodes.Add("780", "CHILE");
            CountryCodes.Add("784", "PARAGUAY");
            CountryCodes.Add("785", "PERU");
            CountryCodes.Add("786", "ECUADOR");
            CountryCodes.Add("789", "BRAZIL");

            CountryCodes.Add("850", "CUBA");
            CountryCodes.Add("858", "SLOVAKIA");
            CountryCodes.Add("859", "CZECH REPUBLIC");
            CountryCodes.Add("860", "YUGLOSLAVIA");
            CountryCodes.Add("869", "TURKEY");
            CountryCodes.Add("880", "SOUTH KOREA");
            CountryCodes.Add("885", "THAILAND");
            CountryCodes.Add("888", "SINGAPORE");
            CountryCodes.Add("890", "INDIA");
            CountryCodes.Add("893", "VIETNAM");
            CountryCodes.Add("899", "INDONESIA");

            CountryCodes.Add("955", "MALAYSIA");
            CountryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
            CountryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
            CountryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
            CountryCodes.Add("980", "REFUND RECEIPTS");
            CountryCodes.Add("981", "COMMON CURRENCY COUPONS");
            CountryCodes.Add("982", "COMMON CURRENCY COUPONS");
        }//init_CountryCodes
        private void CheckDigit()
        {
            try
            {
                string RawDataHolder = Raw_Data.Substring(0, 11);

                //calculate check digit
                int even = 0;
                int odd = 0;

                for (int i = 0; i < RawDataHolder.Length; i++)
                {
                    if (i % 2 == 0)
                        odd += Int32.Parse(RawDataHolder.Substring(i, 1)) * 3;
                    else
                        even += Int32.Parse(RawDataHolder.Substring(i, 1));
                }//for

                int total = even + odd;
                int cs = total % 10;
                cs = 10 - cs;
                if (cs == 10)
                    cs = 0;

                Raw_Data = RawDataHolder + cs.ToString()[0];
            }//try
            catch
            {
                Error("EUPCA-4: Error calculating check digit.");
            }//catch
        }//CheckDigit

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return this.Encode_UPCA(); }
        }

        #endregion
    }
}
