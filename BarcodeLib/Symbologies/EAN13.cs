using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  EAN-13 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class EAN13 : BarcodeCommon, IBarcode
    {
        private string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] EAN_CodeB = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private string[] EAN_CodeC = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private string[] EAN_Pattern = { "aaaaaa", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba" };
        private Hashtable CountryCodes = new Hashtable(); //is initialized by init_CountryCodes()
        private string _Country_Assigning_Manufacturer_Code = "N/A";

        public EAN13(string input)
        {
            Raw_Data = input;

            CheckDigit();
        }
        /// <summary>
        /// Encode the raw data using the EAN-13 algorithm. (Can include the checksum already.  If it doesnt exist in the data then it will calculate it for you.  Accepted data lengths are 12 + 1 checksum or just the 12 data digits)
        /// </summary>
        private string Encode_EAN13()
        {
            //check length of input
            if (Raw_Data.Length < 12 || Raw_Data.Length > 13)
                Error("EEAN13-1: Data length invalid. (Length must be 12 or 13)");

            if (!CheckNumericOnly(Raw_Data))
                Error("EEAN13-2: Numeric Data Only");

            string patterncode = EAN_Pattern[Int32.Parse(Raw_Data[0].ToString())];
            string result = "101";

            //first
            //result += EAN_CodeA[Int32.Parse(RawData[0].ToString())];

            //second
            int pos = 0;
            while (pos < 6)
            {
                if (patterncode[pos] == 'a')
                    result += EAN_CodeA[Int32.Parse(Raw_Data[pos + 1].ToString())];
                if (patterncode[pos] == 'b')
                    result += EAN_CodeB[Int32.Parse(Raw_Data[pos + 1].ToString())];
                pos++;
            }//while


            //add divider bars
            result += "01010";

            //get the third
            pos = 1;
            while (pos <= 5)
            {
                result += EAN_CodeC[Int32.Parse(Raw_Data[(pos++) + 6].ToString())];
            }//while

            //checksum digit
            int cs = Int32.Parse(Raw_Data[Raw_Data.Length - 1].ToString());

            //add checksum
            result += EAN_CodeC[cs];

            //add ending bars
            result += "101";

            //get the manufacturer assigning country
            init_CountryCodes();
            _Country_Assigning_Manufacturer_Code = "N/A";
            string twodigitCode = Raw_Data.Substring(0, 2);
            string threedigitCode = Raw_Data.Substring(0, 3);
            try
            {
                _Country_Assigning_Manufacturer_Code = CountryCodes[threedigitCode].ToString();
            }//try
            catch
            {
                try
                {
                    _Country_Assigning_Manufacturer_Code = CountryCodes[twodigitCode].ToString();
                }//try
                catch
                {
                    Error("EEAN13-3: Country assigning manufacturer code not found.");
                }//catch 
            }//catch
            finally { CountryCodes.Clear(); }

            return result;
        }//Encode_EAN13
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

            //100-139 United States
            CountryCodes.Add("100", "UNITED STATES");
            CountryCodes.Add("100", "UNITED STATES");
            CountryCodes.Add("101", "UNITED STATES");
            CountryCodes.Add("102", "UNITED STATES");
            CountryCodes.Add("103", "UNITED STATES");
            CountryCodes.Add("104", "UNITED STATES");
            CountryCodes.Add("105", "UNITED STATES");
            CountryCodes.Add("106", "UNITED STATES");
            CountryCodes.Add("107", "UNITED STATES");
            CountryCodes.Add("108", "UNITED STATES");
            CountryCodes.Add("109", "UNITED STATES");
            CountryCodes.Add("110", "UNITED STATES");
            CountryCodes.Add("111", "UNITED STATES");
            CountryCodes.Add("112", "UNITED STATES");
            CountryCodes.Add("113", "UNITED STATES");
            CountryCodes.Add("114", "UNITED STATES");
            CountryCodes.Add("115", "UNITED STATES");
            CountryCodes.Add("116", "UNITED STATES");
            CountryCodes.Add("117", "UNITED STATES");
            CountryCodes.Add("118", "UNITED STATES");
            CountryCodes.Add("119", "UNITED STATES");
            CountryCodes.Add("120", "UNITED STATES");
            CountryCodes.Add("121", "UNITED STATES");
            CountryCodes.Add("122", "UNITED STATES");
            CountryCodes.Add("123", "UNITED STATES");
            CountryCodes.Add("124", "UNITED STATES");
            CountryCodes.Add("125", "UNITED STATES");
            CountryCodes.Add("126", "UNITED STATES");
            CountryCodes.Add("127", "UNITED STATES");
            CountryCodes.Add("128", "UNITED STATES");
            CountryCodes.Add("129", "UNITED STATES");
            CountryCodes.Add("130", "UNITED STATES");
            CountryCodes.Add("131", "UNITED STATES");
            CountryCodes.Add("132", "UNITED STATES");
            CountryCodes.Add("133", "UNITED STATES");
            CountryCodes.Add("134", "UNITED STATES");
            CountryCodes.Add("135", "UNITED STATES");
            CountryCodes.Add("136", "UNITED STATES");
            CountryCodes.Add("137", "UNITED STATES");
            CountryCodes.Add("138", "UNITED STATES");
            CountryCodes.Add("139", "UNITED STATES");

            //300-379	France and Monaco
            CountryCodes.Add("300", "FRANCE AND MONACO");
            CountryCodes.Add("301", "FRANCE AND MONACO");
            CountryCodes.Add("302", "FRANCE AND MONACO");
            CountryCodes.Add("303", "FRANCE AND MONACO");
            CountryCodes.Add("304", "FRANCE AND MONACO");
            CountryCodes.Add("305", "FRANCE AND MONACO");
            CountryCodes.Add("306", "FRANCE AND MONACO");
            CountryCodes.Add("307", "FRANCE AND MONACO");
            CountryCodes.Add("308", "FRANCE AND MONACO");
            CountryCodes.Add("309", "FRANCE AND MONACO");
            CountryCodes.Add("310", "FRANCE AND MONACO");
            CountryCodes.Add("311", "FRANCE AND MONACO");
            CountryCodes.Add("312", "FRANCE AND MONACO");
            CountryCodes.Add("313", "FRANCE AND MONACO");
            CountryCodes.Add("314", "FRANCE AND MONACO");
            CountryCodes.Add("315", "FRANCE AND MONACO");
            CountryCodes.Add("316", "FRANCE AND MONACO");
            CountryCodes.Add("317", "FRANCE AND MONACO");
            CountryCodes.Add("318", "FRANCE AND MONACO");
            CountryCodes.Add("319", "FRANCE AND MONACO");
            CountryCodes.Add("320", "FRANCE AND MONACO");
            CountryCodes.Add("321", "FRANCE AND MONACO");
            CountryCodes.Add("322", "FRANCE AND MONACO");
            CountryCodes.Add("323", "FRANCE AND MONACO");
            CountryCodes.Add("324", "FRANCE AND MONACO");
            CountryCodes.Add("325", "FRANCE AND MONACO");
            CountryCodes.Add("326", "FRANCE AND MONACO");
            CountryCodes.Add("327", "FRANCE AND MONACO");
            CountryCodes.Add("328", "FRANCE AND MONACO");
            CountryCodes.Add("329", "FRANCE AND MONACO");
            CountryCodes.Add("330", "FRANCE AND MONACO");
            CountryCodes.Add("331", "FRANCE AND MONACO");
            CountryCodes.Add("332", "FRANCE AND MONACO");
            CountryCodes.Add("333", "FRANCE AND MONACO");
            CountryCodes.Add("334", "FRANCE AND MONACO");
            CountryCodes.Add("335", "FRANCE AND MONACO");
            CountryCodes.Add("336", "FRANCE AND MONACO");
            CountryCodes.Add("337", "FRANCE AND MONACO");
            CountryCodes.Add("338", "FRANCE AND MONACO");
            CountryCodes.Add("339", "FRANCE AND MONACO");
            CountryCodes.Add("340", "FRANCE AND MONACO");
            CountryCodes.Add("341", "FRANCE AND MONACO");
            CountryCodes.Add("342", "FRANCE AND MONACO");
            CountryCodes.Add("343", "FRANCE AND MONACO");
            CountryCodes.Add("344", "FRANCE AND MONACO");
            CountryCodes.Add("345", "FRANCE AND MONACO");
            CountryCodes.Add("346", "FRANCE AND MONACO");
            CountryCodes.Add("347", "FRANCE AND MONACO");
            CountryCodes.Add("348", "FRANCE AND MONACO");
            CountryCodes.Add("349", "FRANCE AND MONACO");
            CountryCodes.Add("350", "FRANCE AND MONACO");
            CountryCodes.Add("351", "FRANCE AND MONACO");
            CountryCodes.Add("352", "FRANCE AND MONACO");
            CountryCodes.Add("353", "FRANCE AND MONACO");
            CountryCodes.Add("354", "FRANCE AND MONACO");
            CountryCodes.Add("355", "FRANCE AND MONACO");
            CountryCodes.Add("356", "FRANCE AND MONACO");
            CountryCodes.Add("357", "FRANCE AND MONACO");
            CountryCodes.Add("358", "FRANCE AND MONACO");
            CountryCodes.Add("359", "FRANCE AND MONACO");
            CountryCodes.Add("360", "FRANCE AND MONACO");
            CountryCodes.Add("361", "FRANCE AND MONACO");
            CountryCodes.Add("362", "FRANCE AND MONACO");
            CountryCodes.Add("363", "FRANCE AND MONACO");
            CountryCodes.Add("364", "FRANCE AND MONACO");
            CountryCodes.Add("365", "FRANCE AND MONACO");
            CountryCodes.Add("366", "FRANCE AND MONACO");
            CountryCodes.Add("367", "FRANCE AND MONACO");
            CountryCodes.Add("368", "FRANCE AND MONACO");
            CountryCodes.Add("369", "FRANCE AND MONACO");
            CountryCodes.Add("370", "FRANCE AND MONACO");
            CountryCodes.Add("371", "FRANCE AND MONACO");
            CountryCodes.Add("372", "FRANCE AND MONACO");
            CountryCodes.Add("373", "FRANCE AND MONACO");
            CountryCodes.Add("374", "FRANCE AND MONACO");
            CountryCodes.Add("375", "FRANCE AND MONACO");
            CountryCodes.Add("376", "FRANCE AND MONACO");
            CountryCodes.Add("377", "FRANCE AND MONACO");
            CountryCodes.Add("378", "FRANCE AND MONACO");
            CountryCodes.Add("379", "FRANCE AND MONACO");

            CountryCodes.Add("380", "BULGARIA");
            CountryCodes.Add("383", "SLOVENIJA");
            CountryCodes.Add("385", "CROATIA");
            CountryCodes.Add("387", "BOSNIA-HERZEGOVINA");
            CountryCodes.Add("389", "MONTENEGRO");

            //400-440	Germany (440 code inherited from old East Germany on reunification, 1990)
            CountryCodes.Add("400", "GERMANY");
            CountryCodes.Add("401", "GERMANY");
            CountryCodes.Add("402", "GERMANY");
            CountryCodes.Add("403", "GERMANY");
            CountryCodes.Add("404", "GERMANY");
            CountryCodes.Add("405", "GERMANY");
            CountryCodes.Add("406", "GERMANY");
            CountryCodes.Add("407", "GERMANY");
            CountryCodes.Add("408", "GERMANY");
            CountryCodes.Add("409", "GERMANY");

            CountryCodes.Add("410", "GERMANY");
            CountryCodes.Add("411", "GERMANY");
            CountryCodes.Add("412", "GERMANY");
            CountryCodes.Add("413", "GERMANY");
            CountryCodes.Add("414", "GERMANY");
            CountryCodes.Add("415", "GERMANY");
            CountryCodes.Add("416", "GERMANY");
            CountryCodes.Add("417", "GERMANY");
            CountryCodes.Add("418", "GERMANY");
            CountryCodes.Add("419", "GERMANY");

            CountryCodes.Add("420", "GERMANY");
            CountryCodes.Add("421", "GERMANY");
            CountryCodes.Add("422", "GERMANY");
            CountryCodes.Add("423", "GERMANY");
            CountryCodes.Add("424", "GERMANY");
            CountryCodes.Add("425", "GERMANY");
            CountryCodes.Add("426", "GERMANY");
            CountryCodes.Add("427", "GERMANY");
            CountryCodes.Add("428", "GERMANY");
            CountryCodes.Add("429", "GERMANY");

            CountryCodes.Add("430", "GERMANY");
            CountryCodes.Add("431", "GERMANY");
            CountryCodes.Add("432", "GERMANY");
            CountryCodes.Add("433", "GERMANY");
            CountryCodes.Add("434", "GERMANY");
            CountryCodes.Add("435", "GERMANY");
            CountryCodes.Add("436", "GERMANY");
            CountryCodes.Add("437", "GERMANY");
            CountryCodes.Add("438", "GERMANY");
            CountryCodes.Add("439", "GERMANY");

            CountryCodes.Add("440", "GERMANY");

            CountryCodes.Add("450", "JAPAN");
            CountryCodes.Add("451", "JAPAN");
            CountryCodes.Add("452", "JAPAN");
            CountryCodes.Add("453", "JAPAN");
            CountryCodes.Add("454", "JAPAN");
            CountryCodes.Add("455", "JAPAN");
            CountryCodes.Add("456", "JAPAN");
            CountryCodes.Add("457", "JAPAN");
            CountryCodes.Add("458", "JAPAN");
            CountryCodes.Add("459", "JAPAN");

            CountryCodes.Add("460", "RUSSIA");
            CountryCodes.Add("461", "RUSSIA");
            CountryCodes.Add("462", "RUSSIA");
            CountryCodes.Add("463", "RUSSIA");
            CountryCodes.Add("464", "RUSSIA");
            CountryCodes.Add("465", "RUSSIA");
            CountryCodes.Add("466", "RUSSIA");
            CountryCodes.Add("467", "RUSSIA");
            CountryCodes.Add("468", "RUSSIA");
            CountryCodes.Add("469", "RUSSIA");

            CountryCodes.Add("470", "KYRGYZSTAN");
            CountryCodes.Add("471", "TAIWAN");
            CountryCodes.Add("474", "ESTONIA");
            CountryCodes.Add("475", "LATVIA");
            CountryCodes.Add("476", "AZERBAIJAN");
            CountryCodes.Add("477", "LITHUANIA");
            CountryCodes.Add("478", "UZBEKISTAN");
            CountryCodes.Add("479", "SRI LANKA");

            CountryCodes.Add("480", "PHILIPPINES");
            CountryCodes.Add("481", "BELARUS");
            CountryCodes.Add("482", "UKRAINE");
            CountryCodes.Add("483", "TURKMENISTAN");
            CountryCodes.Add("484", "MOLDOVA");
            CountryCodes.Add("485", "ARMENIA");
            CountryCodes.Add("486", "GEORGIA");
            CountryCodes.Add("487", "KAZAKHSTAN");
            CountryCodes.Add("488", "TAJIKISTAN");
            CountryCodes.Add("489", "HONG KONG");

            CountryCodes.Add("490", "JAPAN");
            CountryCodes.Add("491", "JAPAN");
            CountryCodes.Add("492", "JAPAN");
            CountryCodes.Add("493", "JAPAN");
            CountryCodes.Add("494", "JAPAN");
            CountryCodes.Add("495", "JAPAN");
            CountryCodes.Add("496", "JAPAN");
            CountryCodes.Add("497", "JAPAN");
            CountryCodes.Add("498", "JAPAN");
            CountryCodes.Add("499", "JAPAN");

            CountryCodes.Add("500", "UNITED KINGDOM");
            CountryCodes.Add("501", "UNITED KINGDOM");
            CountryCodes.Add("502", "UNITED KINGDOM");
            CountryCodes.Add("503", "UNITED KINGDOM");
            CountryCodes.Add("504", "UNITED KINGDOM");
            CountryCodes.Add("505", "UNITED KINGDOM");
            CountryCodes.Add("506", "UNITED KINGDOM");
            CountryCodes.Add("507", "UNITED KINGDOM");
            CountryCodes.Add("508", "UNITED KINGDOM");
            CountryCodes.Add("509", "UNITED KINGDOM");

            CountryCodes.Add("520", "GREECE");
            CountryCodes.Add("521", "GREECE");
            CountryCodes.Add("528", "LEBANON");
            CountryCodes.Add("529", "CYPRUS");
            CountryCodes.Add("530", "ALBANIA");
            CountryCodes.Add("531", "MACEDONIA");
            CountryCodes.Add("535", "MALTA");
            CountryCodes.Add("539", "IRELAND");

            CountryCodes.Add("540", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("541", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("542", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("543", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("544", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("545", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("546", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("547", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("548", "BELGIUM AND LUXEMBOURG");
            CountryCodes.Add("549", "BELGIUM AND LUXEMBOURG");

            CountryCodes.Add("560", "PORTUGAL");
            CountryCodes.Add("569", "ICELAND");

            CountryCodes.Add("570", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("571", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("572", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("573", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("574", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("575", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("576", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("577", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("578", " DENMARK, FAROE ISLANDS AND GREENLAND");
            CountryCodes.Add("579", " DENMARK, FAROE ISLANDS AND GREENLAND");

            CountryCodes.Add("590", "POLAND");
            CountryCodes.Add("594", "ROMANIA");
            CountryCodes.Add("599", "HUNGARY");

            CountryCodes.Add("600", "SOUTH AFRICA");
            CountryCodes.Add("601", "SOUTH AFRICA");
            CountryCodes.Add("603", "GHANA");
            CountryCodes.Add("604", "SENEGAL");
            CountryCodes.Add("608", "BAHRAIN");
            CountryCodes.Add("609", "MAURITIUS");

            CountryCodes.Add("611", "MOROCCO");
            CountryCodes.Add("613", "ALGERIA");
            CountryCodes.Add("615", "NIGERIA");
            CountryCodes.Add("616", "KENYA");
            CountryCodes.Add("618", "IVORY COAST");
            CountryCodes.Add("619", "TUNISIA");

            CountryCodes.Add("620", "TANZANIA");
            CountryCodes.Add("621", "SYRIA");
            CountryCodes.Add("622", "EGYPT");
            CountryCodes.Add("623", "BRUNEI");
            CountryCodes.Add("624", "LIBYA");
            CountryCodes.Add("625", "JORDAN");
            CountryCodes.Add("626", "IRAN");
            CountryCodes.Add("627", "KUWAIT");
            CountryCodes.Add("628", "SAUDI ARABIA");
            CountryCodes.Add("629", "EMIRATES");

            CountryCodes.Add("640", "FINLAND");
            CountryCodes.Add("641", "FINLAND");
            CountryCodes.Add("642", "FINLAND");
            CountryCodes.Add("643", "FINLAND");
            CountryCodes.Add("644", "FINLAND");
            CountryCodes.Add("645", "FINLAND");
            CountryCodes.Add("646", "FINLAND");
            CountryCodes.Add("647", "FINLAND");
            CountryCodes.Add("648", "FINLAND");
            CountryCodes.Add("649", "FINLAND");

            CountryCodes.Add("690", "CHINA");
            CountryCodes.Add("691", "CHINA");
            CountryCodes.Add("692", "CHINA");
            CountryCodes.Add("693", "CHINA");
            CountryCodes.Add("694", "CHINA");
            CountryCodes.Add("695", "CHINA");
            CountryCodes.Add("696", "CHINA");
            CountryCodes.Add("697", "CHINA");
            CountryCodes.Add("698", "CHINA");
            CountryCodes.Add("699", "CHINA");

            CountryCodes.Add("700", "NORWAY");
            CountryCodes.Add("701", "NORWAY");
            CountryCodes.Add("702", "NORWAY");
            CountryCodes.Add("703", "NORWAY");
            CountryCodes.Add("704", "NORWAY");
            CountryCodes.Add("705", "NORWAY");
            CountryCodes.Add("706", "NORWAY");
            CountryCodes.Add("707", "NORWAY");
            CountryCodes.Add("708", "NORWAY");
            CountryCodes.Add("709", "NORWAY");

            CountryCodes.Add("729", "ISRAEL");

            //730-739    Sweden: EAN / GS1 Sweden
            CountryCodes.Add("730", "SWEDEN");
            CountryCodes.Add("731", "SWEDEN");
            CountryCodes.Add("732", "SWEDEN");
            CountryCodes.Add("733", "SWEDEN");
            CountryCodes.Add("734", "SWEDEN");
            CountryCodes.Add("735", "SWEDEN");
            CountryCodes.Add("736", "SWEDEN");
            CountryCodes.Add("737", "SWEDEN");
            CountryCodes.Add("738", "SWEDEN");
            CountryCodes.Add("739", "SWEDEN");

            CountryCodes.Add("740", "GUATEMALA");
            CountryCodes.Add("741", "EL SALVADOR");
            CountryCodes.Add("742", "HONDURAS");
            CountryCodes.Add("743", "NICARAGUA");
            CountryCodes.Add("744", "COSTA RICA");
            CountryCodes.Add("745", "PANAMA");
            CountryCodes.Add("746", "DOMINICAN REPUBLIC");

            CountryCodes.Add("750", "MEXICO");
            CountryCodes.Add("754", "CANADA");
            CountryCodes.Add("755", "CANADA");
            CountryCodes.Add("759", "VENEZUELA");

            //760-769 Switzerland and Liechtenstein
            CountryCodes.Add("760", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("761", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("762", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("763", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("764", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("765", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("766", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("767", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("768", "SWITZERLAND AND LIECHTENSTEIN");
            CountryCodes.Add("769", "SWITZERLAND AND LIECHTENSTEIN");

            CountryCodes.Add("770", "COLOMBIA");
            CountryCodes.Add("773", "URUGUAY");
            CountryCodes.Add("775", "PERU");
            CountryCodes.Add("777", "BOLIVIA");
            CountryCodes.Add("778", "ARGENTINA");
            CountryCodes.Add("779", "ARGENTINA");

            CountryCodes.Add("780", "CHILE");
            CountryCodes.Add("784", "PARAGUAY");
            CountryCodes.Add("785", "PERU");
            CountryCodes.Add("786", "ECUADOR");
            CountryCodes.Add("789", "BRAZIL");
            CountryCodes.Add("790", "BRAZIL");

            //800-839 Italy, San Marino and Vatican City
            CountryCodes.Add("800", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("801", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("802", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("803", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("804", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("805", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("806", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("807", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("808", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("809", "ITALY, SAN MARINO AND VATICAN CITY");

            CountryCodes.Add("810", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("811", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("812", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("813", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("814", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("815", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("816", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("817", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("818", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("819", "ITALY, SAN MARINO AND VATICAN CITY");

            CountryCodes.Add("820", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("821", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("822", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("823", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("824", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("825", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("826", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("827", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("828", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("829", "ITALY, SAN MARINO AND VATICAN CITY");

            CountryCodes.Add("830", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("831", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("832", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("833", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("834", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("835", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("836", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("837", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("838", "ITALY, SAN MARINO AND VATICAN CITY");
            CountryCodes.Add("839", "ITALY, SAN MARINO AND VATICAN CITY");

            //840-849 Spain and Andorra
            CountryCodes.Add("840", "SPAIN AND ANDORRA");
            CountryCodes.Add("841", "SPAIN AND ANDORRA");
            CountryCodes.Add("842", "SPAIN AND ANDORRA");
            CountryCodes.Add("843", "SPAIN AND ANDORRA");
            CountryCodes.Add("844", "SPAIN AND ANDORRA");
            CountryCodes.Add("845", "SPAIN AND ANDORRA");
            CountryCodes.Add("846", "SPAIN AND ANDORRA");
            CountryCodes.Add("847", "SPAIN AND ANDORRA");
            CountryCodes.Add("848", "SPAIN AND ANDORRA");
            CountryCodes.Add("849", "SPAIN AND ANDORRA");

            CountryCodes.Add("850", "CUBA");
            CountryCodes.Add("858", "SLOVAKIA");
            CountryCodes.Add("859", "CZECH REPUBLIC");

            CountryCodes.Add("860", "SERBIA");
            CountryCodes.Add("865", "MONGOLIA");
            CountryCodes.Add("867", "NORTH KOREA");
            CountryCodes.Add("868", "TURKEY");
            CountryCodes.Add("869", "TURKEY");

            //870-879 Netherlands
            CountryCodes.Add("870", "NETHERLANDS");
            CountryCodes.Add("871", "NETHERLANDS");
            CountryCodes.Add("872", "NETHERLANDS");
            CountryCodes.Add("873", "NETHERLANDS");
            CountryCodes.Add("874", "NETHERLANDS");
            CountryCodes.Add("875", "NETHERLANDS");
            CountryCodes.Add("876", "NETHERLANDS");
            CountryCodes.Add("877", "NETHERLANDS");
            CountryCodes.Add("878", "NETHERLANDS");
            CountryCodes.Add("879", "NETHERLANDS");

            CountryCodes.Add("880", "SOUTH KOREA");
            CountryCodes.Add("884", "CAMBODIA");
            CountryCodes.Add("885", "THAILAND");
            CountryCodes.Add("888", "SINGAPORE");

            CountryCodes.Add("890", "INDIA");
            CountryCodes.Add("893", "VIETNAM");
            CountryCodes.Add("896", "PAKISTAN");
            CountryCodes.Add("899", "INDONESIA");

            //900-919 Austria
            CountryCodes.Add("900", "AUSTRIA");
            CountryCodes.Add("901", "AUSTRIA");
            CountryCodes.Add("902", "AUSTRIA");
            CountryCodes.Add("903", "AUSTRIA");
            CountryCodes.Add("904", "AUSTRIA");
            CountryCodes.Add("905", "AUSTRIA");
            CountryCodes.Add("906", "AUSTRIA");
            CountryCodes.Add("907", "AUSTRIA");
            CountryCodes.Add("908", "AUSTRIA");
            CountryCodes.Add("909", "AUSTRIA");

            //930-939	Australia
            CountryCodes.Add("930", "AUSTRALIA");
            CountryCodes.Add("931", "AUSTRALIA");
            CountryCodes.Add("932", "AUSTRALIA");
            CountryCodes.Add("933", "AUSTRALIA");
            CountryCodes.Add("934", "AUSTRALIA");
            CountryCodes.Add("935", "AUSTRALIA");
            CountryCodes.Add("936", "AUSTRALIA");
            CountryCodes.Add("937", "AUSTRALIA");
            CountryCodes.Add("938", "AUSTRALIA");
            CountryCodes.Add("939", "AUSTRALIA");

            //940-949 New Zealand
            CountryCodes.Add("940", "NEW ZEALAND");
            CountryCodes.Add("941", "NEW ZEALAND");
            CountryCodes.Add("942", "NEW ZEALAND");
            CountryCodes.Add("943", "NEW ZEALAND");
            CountryCodes.Add("944", "NEW ZEALAND");
            CountryCodes.Add("945", "NEW ZEALAND");
            CountryCodes.Add("946", "NEW ZEALAND");
            CountryCodes.Add("947", "NEW ZEALAND");
            CountryCodes.Add("948", "NEW ZEALAND");
            CountryCodes.Add("949", "NEW ZEALAND");

            CountryCodes.Add("950", "GS1 GLOBAL OFFICE SPECIAL APPLICATIONS");
            CountryCodes.Add("951", "EPC GLOBAL SPECIAL APPLICATIONS");

            CountryCodes.Add("955", "MALAYSIA");
            CountryCodes.Add("958", "MACAU");

            CountryCodes.Add("960", "GS1 UK: GTIN-8 ALLOCATIONS");

            //961-969   GS1 Global Office: GTIN-8 allocations
            CountryCodes.Add("961", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("962", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("963", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("964", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("965", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("966", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("967", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("968", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
            CountryCodes.Add("969", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");

            CountryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
            CountryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
            CountryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");

            CountryCodes.Add("980", "REFUND RECEIPTS");
            CountryCodes.Add("981", "COMMON CURRENCY COUPONS");
            CountryCodes.Add("982", "COMMON CURRENCY COUPONS");
            CountryCodes.Add("983", "COMMON CURRENCY COUPONS");
            CountryCodes.Add("984", "COMMON CURRENCY COUPONS");

            CountryCodes.Add("990", "COUPONS");
            CountryCodes.Add("991", "COUPONS");
            CountryCodes.Add("992", "COUPONS");
            CountryCodes.Add("993", "COUPONS");
            CountryCodes.Add("994", "COUPONS");
            CountryCodes.Add("995", "COUPONS");
            CountryCodes.Add("996", "COUPONS");
            CountryCodes.Add("997", "COUPONS");
            CountryCodes.Add("998", "COUPONS");
            CountryCodes.Add("999", "COUPONS");

        }//init_CountryCodes
        private void CheckDigit()
        {
            try
            {
                string RawDataHolder = Raw_Data.Substring(0, 12);

                int even = 0;
                int odd = 0;

                for (int i = 0; i < RawDataHolder.Length; i++)
                {
                    if (i % 2 == 0)
                        odd += Int32.Parse(RawDataHolder.Substring(i, 1));
                    else
                        even += Int32.Parse(RawDataHolder.Substring(i, 1)) * 3;
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
                Error("EEAN13-4: Error calculating check digit.");
            }//catch
        }

        #region IBarcode Members

        public string Encoded_Value
        {
            get { return this.Encode_EAN13(); }
        }

        #endregion
    }
}
