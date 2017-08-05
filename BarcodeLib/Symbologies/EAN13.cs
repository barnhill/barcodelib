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

		private void create_CountryCodeRange(int startingNumber, int endingNumber, string countryDescription)
		{
			for (int i = startingNumber; i <= endingNumber; i++)
			{
				CountryCodes.Add(i.ToString("00"), countryDescription);
			}	// for
		}   // create_CountryCodeRange

		private void init_CountryCodes()
        {
            CountryCodes.Clear();

			// Source: https://en.wikipedia.org/wiki/List_of_GS1_country_codes
			create_CountryCodeRange(0, 19, "US / CANADA");
			create_CountryCodeRange(20, 29, "IN STORE");
			create_CountryCodeRange(30, 39, "US DRUGS");
			create_CountryCodeRange(40, 49, "Used to issue restricted circulation numbers within a geographic region (MO defined)");
			create_CountryCodeRange(50, 59, "GS1 US reserved for future use");
			create_CountryCodeRange(60, 99, "US / CANADA");
			create_CountryCodeRange(100, 139, "UNITED STATES");
			create_CountryCodeRange(200, 299, "Used to issue GS1 restricted circulation number within a geographic region (MO defined)");
			create_CountryCodeRange(300, 379, "FRANCE AND MONACO");

			create_CountryCodeRange(380, 380, "BULGARIA");
			create_CountryCodeRange(383, 383, "SLOVENIA");
			create_CountryCodeRange(385, 385, "CROATIA");
			create_CountryCodeRange(387, 387, "BOSNIA AND HERZEGOVINA");
			create_CountryCodeRange(389, 389, "MONTENEGRO");
			create_CountryCodeRange(400, 440, "GERMANY");
			create_CountryCodeRange(450, 459, "JAPAN");
			create_CountryCodeRange(460, 469, "RUSSIA");
			create_CountryCodeRange(470, 470, "KYRGYZSTAN");
			create_CountryCodeRange(471, 471, "TAIWAN");
			create_CountryCodeRange(474, 474, "ESTONIA");
			create_CountryCodeRange(475, 475, "LATVIA");
			create_CountryCodeRange(476, 476, "AZERBAIJAN");
			create_CountryCodeRange(477, 477, "LITHUANIA");
			create_CountryCodeRange(478, 478, "UZBEKISTAN");
			create_CountryCodeRange(479, 479, "SRI LANKA");
			create_CountryCodeRange(480, 480, "PHILIPPINES");
			create_CountryCodeRange(481, 481, "BELARUS");
			create_CountryCodeRange(482, 482, "UKRAINE");
			create_CountryCodeRange(483, 483, "TURKMENISTAN");
			create_CountryCodeRange(484, 484, "MOLDOVA");
			create_CountryCodeRange(485, 485, "ARMENIA");
			create_CountryCodeRange(486, 486, "GEORGIA");
			create_CountryCodeRange(487, 487, "KAZAKHSTAN");
			create_CountryCodeRange(488, 488, "TAJIKISTAN");
			create_CountryCodeRange(489, 489, "HONG KONG");
			create_CountryCodeRange(490, 499, "JAPAN");
			create_CountryCodeRange(500, 509, "UNITED KINGDOM");
			create_CountryCodeRange(520, 521, "GREECE");
			create_CountryCodeRange(528, 528, "LEBANON");
			create_CountryCodeRange(529, 529, "CYPRUS");
			create_CountryCodeRange(530, 530, "ALBANIA");
			create_CountryCodeRange(531, 531, "MACEDONIA");
			create_CountryCodeRange(535, 535, "MALTA");
			create_CountryCodeRange(539, 539, "REPUBLIC OF IRELAND");
			create_CountryCodeRange(540, 549, "BELGIUM AND LUXEMBOURG");
			create_CountryCodeRange(560, 560, "PORTUGAL");
			create_CountryCodeRange(569, 569, "ICELAND");
			create_CountryCodeRange(570, 579, "DENMARK, FAROE ISLANDS AND GREENLAND");
			create_CountryCodeRange(590, 590, "POLAND");
			create_CountryCodeRange(594, 594, "ROMANIA");
			create_CountryCodeRange(599, 599, "HUNGARY");
			create_CountryCodeRange(600, 601, "SOUTH AFRICA");
			create_CountryCodeRange(603, 603, "GHANA");
			create_CountryCodeRange(604, 604, "SENEGAL");
			create_CountryCodeRange(608, 608, "BAHRAIN");
			create_CountryCodeRange(609, 609, "MAURITIUS");
			create_CountryCodeRange(611, 611, "MOROCCO");
			create_CountryCodeRange(613, 613, "ALGERIA");
			create_CountryCodeRange(615, 615, "NIGERIA");
			create_CountryCodeRange(616, 616, "KENYA");
			create_CountryCodeRange(618, 618, "IVORY COAST");
			create_CountryCodeRange(619, 619, "TUNISIA");
			create_CountryCodeRange(620, 620, "TANZANIA");
			create_CountryCodeRange(621, 621, "SYRIA");
			create_CountryCodeRange(622, 622, "EGYPT");
			create_CountryCodeRange(623, 623, "BRUNEI");
			create_CountryCodeRange(624, 624, "LIBYA");
			create_CountryCodeRange(625, 625, "JORDAN");
			create_CountryCodeRange(626, 626, "IRAN");
			create_CountryCodeRange(627, 627, "KUWAIT");
			create_CountryCodeRange(628, 628, "SAUDI ARABIA");
			create_CountryCodeRange(629, 629, "UNITED ARAB EMIRATES");
			create_CountryCodeRange(640, 649, "FINLAND");
			create_CountryCodeRange(690, 699, "CHINA");
			create_CountryCodeRange(700, 709, "NORWAY");
			create_CountryCodeRange(729, 729, "ISRAEL");
			create_CountryCodeRange(730, 739, "SWEDEN");
			create_CountryCodeRange(740, 740, "GUATEMALA");
			create_CountryCodeRange(741, 741, "EL SALVADOR");
			create_CountryCodeRange(742, 742, "HONDURAS");
			create_CountryCodeRange(743, 743, "NICARAGUA");
			create_CountryCodeRange(744, 744, "COSTA RICA");
			create_CountryCodeRange(745, 745, "PANAMA");
			create_CountryCodeRange(746, 746, "DOMINICAN REPUBLIC");
			create_CountryCodeRange(750, 750, "MEXICO");
			create_CountryCodeRange(754, 755, "CANADA");
			create_CountryCodeRange(759, 759, "VENEZUELA");
			create_CountryCodeRange(760, 769, "SWITZERLAND AND LIECHTENSTEIN");
			create_CountryCodeRange(770, 771, "COLOMBIA");
			create_CountryCodeRange(773, 773, "URUGUAY");
			create_CountryCodeRange(775, 775, "PERU");
			create_CountryCodeRange(777, 777, "BOLIVIA");
			create_CountryCodeRange(778, 779, "ARGENTINA");
			create_CountryCodeRange(780, 780, "CHILE");
			create_CountryCodeRange(784, 784, "PARAGUAY");
			create_CountryCodeRange(786, 786, "ECUADOR");
			create_CountryCodeRange(789, 790, "BRAZIL");
			create_CountryCodeRange(800, 839, "ITALY, SAN MARINO AND VATICAN CITY");
			create_CountryCodeRange(840, 849, "SPAIN AND ANDORRA");
			create_CountryCodeRange(850, 850, "CUBA");
			create_CountryCodeRange(858, 858, "SLOVAKIA");
			create_CountryCodeRange(859, 859, "CZECH REPUBLIC");
			create_CountryCodeRange(860, 860, "SERBIA");
			create_CountryCodeRange(865, 865, "MONGOLIA");
			create_CountryCodeRange(867, 867, "NORTH KOREA");
			create_CountryCodeRange(868, 869, "TURKEY");
			create_CountryCodeRange(870, 879, "NETHERLANDS");
			create_CountryCodeRange(880, 880, "SOUTH KOREA");
			create_CountryCodeRange(884, 884, "CAMBODIA");
			create_CountryCodeRange(885, 885, "THAILAND");
			create_CountryCodeRange(888, 888, "SINGAPORE");
			create_CountryCodeRange(890, 890, "INDIA");
			create_CountryCodeRange(893, 893, "VIETNAM");
			create_CountryCodeRange(896, 896, "PAKISTAN");
			create_CountryCodeRange(899, 899, "INDONESIA");
			create_CountryCodeRange(900, 919, "AUSTRIA");
			create_CountryCodeRange(930, 939, "AUSTRALIA");
			create_CountryCodeRange(940, 949, "NEW ZEALAND");
			create_CountryCodeRange(950, 950, "GS1 GLOBAL OFFICE SPECIAL APPLICATIONS");
			create_CountryCodeRange(951, 951, "EPC GLOBAL SPECIAL APPLICATIONS");
			create_CountryCodeRange(955, 955, "MALAYSIA");
			create_CountryCodeRange(958, 958, "MACAU");
			create_CountryCodeRange(960, 961, "GS1 UK OFFICE: GTIN-8 ALLOCATIONS");
			create_CountryCodeRange(962, 969, "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			create_CountryCodeRange(977, 977, "SERIAL PUBLICATIONS (ISSN)");
			create_CountryCodeRange(978, 979, "BOOKLAND (ISBN) – 979-0 USED FOR SHEET MUSIC (ISMN-13, REPLACES DEPRECATED ISMN M- NUMBERS)");
			create_CountryCodeRange(980, 980, "REFUND RECEIPTS");
			create_CountryCodeRange(981, 984, "GS1 COUPON IDENTIFICATION FOR COMMON CURRENCY AREAS");
			create_CountryCodeRange(990, 999, "GS1 COUPON IDENTIFICATION");
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
