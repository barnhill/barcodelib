using System;
using System.Collections;
namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  EAN-13 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class EAN13 : BarcodeCommon, IBarcode
    {
        private readonly string[] EAN_CodeA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private readonly string[] EAN_CodeB = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private readonly string[] EAN_CodeC = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private readonly string[] EAN_Pattern = { "aaaaaa", "aababb", "aabbab", "aabbba", "abaabb", "abbaab", "abbbaa", "ababab", "ababba", "abbaba" };
        private readonly Hashtable _countryCodes = new Hashtable(); //is initialized by init_CountryCodes()
        private string _countryAssigningManufacturerCode = "N/A";

		public string CountryAssigningManufacturerCode { get => _countryAssigningManufacturerCode; set => _countryAssigningManufacturerCode = value; }

		public EAN13(string input, bool disableCountryCode = false)
        {
            Raw_Data = input;
			DisableCountryCode = disableCountryCode;

			CheckDigit();
        }

		/// <summary>
		/// Disables EAN13 country code parsing
		/// </summary>
		public bool DisableCountryCode { get; set; } = false;

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

            var patterncode = EAN_Pattern[Int32.Parse(Raw_Data[0].ToString())];
            var result = "101";

            //first
            //result += EAN_CodeA[Int32.Parse(RawData[0].ToString())];

            //second
            var pos = 0;
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
            var cs = Int32.Parse(Raw_Data[Raw_Data.Length - 1].ToString());

            //add checksum
            result += EAN_CodeC[cs];

            //add ending bars
            result += "101";

			if (!DisableCountryCode)
			{
				ParseCountryCode();
			}

			return result;
        }//Encode_EAN13

		private void ParseCountryCode()
        {
			//get the manufacturer assigning country
			Init_CountryCodes();
			CountryAssigningManufacturerCode = "N/A";
			var twodigitCode = Raw_Data.Substring(0, 2);
			var threedigitCode = Raw_Data.Substring(0, 3);

			var cc = _countryCodes[threedigitCode];
			if (cc == null)
			{
				cc = _countryCodes[twodigitCode].ToString();
				if (cc == null)
				{
					Error("EEAN13-3: Country assigning manufacturer code not found.");
				}
				else
				{
					CountryAssigningManufacturerCode = cc.ToString();
				}
			}
			else
			{
				CountryAssigningManufacturerCode = cc.ToString();
			}

			_countryCodes.Clear();
		}

		private void Create_CountryCodeRange(int startingNumber, int endingNumber, string countryDescription)
		{
			for (var i = startingNumber; i <= endingNumber; i++)
			{
				_countryCodes.Add(i.ToString("00"), countryDescription);
			}	// for
		}   // create_CountryCodeRange

		private void Init_CountryCodes()
        {
            _countryCodes.Clear();

			// Source: https://en.wikipedia.org/wiki/List_of_GS1_country_codes
			Create_CountryCodeRange(0, 19, "US / CANADA");
			Create_CountryCodeRange(20, 29, "IN STORE");
			Create_CountryCodeRange(30, 39, "US DRUGS");
			Create_CountryCodeRange(40, 49, "Used to issue restricted circulation numbers within a geographic region (MO defined)");
			Create_CountryCodeRange(50, 59, "GS1 US reserved for future use");
			Create_CountryCodeRange(60, 99, "US / CANADA");
			Create_CountryCodeRange(100, 139, "UNITED STATES");
			Create_CountryCodeRange(200, 299, "Used to issue GS1 restricted circulation number within a geographic region (MO defined)");
			Create_CountryCodeRange(300, 379, "FRANCE AND MONACO");

			Create_CountryCodeRange(380, 380, "BULGARIA");
			Create_CountryCodeRange(383, 383, "SLOVENIA");
			Create_CountryCodeRange(385, 385, "CROATIA");
			Create_CountryCodeRange(387, 387, "BOSNIA AND HERZEGOVINA");
			Create_CountryCodeRange(389, 389, "MONTENEGRO");
			Create_CountryCodeRange(400, 440, "GERMANY");
			Create_CountryCodeRange(450, 459, "JAPAN");
			Create_CountryCodeRange(460, 469, "RUSSIA");
			Create_CountryCodeRange(470, 470, "KYRGYZSTAN");
			Create_CountryCodeRange(471, 471, "TAIWAN");
			Create_CountryCodeRange(474, 474, "ESTONIA");
			Create_CountryCodeRange(475, 475, "LATVIA");
			Create_CountryCodeRange(476, 476, "AZERBAIJAN");
			Create_CountryCodeRange(477, 477, "LITHUANIA");
			Create_CountryCodeRange(478, 478, "UZBEKISTAN");
			Create_CountryCodeRange(479, 479, "SRI LANKA");
			Create_CountryCodeRange(480, 480, "PHILIPPINES");
			Create_CountryCodeRange(481, 481, "BELARUS");
			Create_CountryCodeRange(482, 482, "UKRAINE");
			Create_CountryCodeRange(483, 483, "TURKMENISTAN");
			Create_CountryCodeRange(484, 484, "MOLDOVA");
			Create_CountryCodeRange(485, 485, "ARMENIA");
			Create_CountryCodeRange(486, 486, "GEORGIA");
			Create_CountryCodeRange(487, 487, "KAZAKHSTAN");
			Create_CountryCodeRange(488, 488, "TAJIKISTAN");
			Create_CountryCodeRange(489, 489, "HONG KONG");
			Create_CountryCodeRange(490, 499, "JAPAN");
			Create_CountryCodeRange(500, 509, "UNITED KINGDOM");
			Create_CountryCodeRange(520, 521, "GREECE");
			Create_CountryCodeRange(528, 528, "LEBANON");
			Create_CountryCodeRange(529, 529, "CYPRUS");
			Create_CountryCodeRange(530, 530, "ALBANIA");
			Create_CountryCodeRange(531, 531, "MACEDONIA");
			Create_CountryCodeRange(535, 535, "MALTA");
			Create_CountryCodeRange(539, 539, "REPUBLIC OF IRELAND");
			Create_CountryCodeRange(540, 549, "BELGIUM AND LUXEMBOURG");
			Create_CountryCodeRange(560, 560, "PORTUGAL");
			Create_CountryCodeRange(569, 569, "ICELAND");
			Create_CountryCodeRange(570, 579, "DENMARK, FAROE ISLANDS AND GREENLAND");
			Create_CountryCodeRange(590, 590, "POLAND");
			Create_CountryCodeRange(594, 594, "ROMANIA");
			Create_CountryCodeRange(599, 599, "HUNGARY");
			Create_CountryCodeRange(600, 601, "SOUTH AFRICA");
			Create_CountryCodeRange(603, 603, "GHANA");
			Create_CountryCodeRange(604, 604, "SENEGAL");
			Create_CountryCodeRange(608, 608, "BAHRAIN");
			Create_CountryCodeRange(609, 609, "MAURITIUS");
			Create_CountryCodeRange(611, 611, "MOROCCO");
			Create_CountryCodeRange(613, 613, "ALGERIA");
			Create_CountryCodeRange(615, 615, "NIGERIA");
			Create_CountryCodeRange(616, 616, "KENYA");
			Create_CountryCodeRange(618, 618, "IVORY COAST");
			Create_CountryCodeRange(619, 619, "TUNISIA");
			Create_CountryCodeRange(620, 620, "TANZANIA");
			Create_CountryCodeRange(621, 621, "SYRIA");
			Create_CountryCodeRange(622, 622, "EGYPT");
			Create_CountryCodeRange(623, 623, "BRUNEI");
			Create_CountryCodeRange(624, 624, "LIBYA");
			Create_CountryCodeRange(625, 625, "JORDAN");
			Create_CountryCodeRange(626, 626, "IRAN");
			Create_CountryCodeRange(627, 627, "KUWAIT");
			Create_CountryCodeRange(628, 628, "SAUDI ARABIA");
			Create_CountryCodeRange(629, 629, "UNITED ARAB EMIRATES");
			Create_CountryCodeRange(640, 649, "FINLAND");
			Create_CountryCodeRange(690, 699, "CHINA");
			Create_CountryCodeRange(700, 709, "NORWAY");
			Create_CountryCodeRange(729, 729, "ISRAEL");
			Create_CountryCodeRange(730, 739, "SWEDEN");
			Create_CountryCodeRange(740, 740, "GUATEMALA");
			Create_CountryCodeRange(741, 741, "EL SALVADOR");
			Create_CountryCodeRange(742, 742, "HONDURAS");
			Create_CountryCodeRange(743, 743, "NICARAGUA");
			Create_CountryCodeRange(744, 744, "COSTA RICA");
			Create_CountryCodeRange(745, 745, "PANAMA");
			Create_CountryCodeRange(746, 746, "DOMINICAN REPUBLIC");
			Create_CountryCodeRange(750, 750, "MEXICO");
			Create_CountryCodeRange(754, 755, "CANADA");
			Create_CountryCodeRange(759, 759, "VENEZUELA");
			Create_CountryCodeRange(760, 769, "SWITZERLAND AND LIECHTENSTEIN");
			Create_CountryCodeRange(770, 771, "COLOMBIA");
			Create_CountryCodeRange(773, 773, "URUGUAY");
			Create_CountryCodeRange(775, 775, "PERU");
			Create_CountryCodeRange(777, 777, "BOLIVIA");
			Create_CountryCodeRange(778, 779, "ARGENTINA");
			Create_CountryCodeRange(780, 780, "CHILE");
			Create_CountryCodeRange(784, 784, "PARAGUAY");
			Create_CountryCodeRange(786, 786, "ECUADOR");
			Create_CountryCodeRange(789, 790, "BRAZIL");
			Create_CountryCodeRange(800, 839, "ITALY, SAN MARINO AND VATICAN CITY");
			Create_CountryCodeRange(840, 849, "SPAIN AND ANDORRA");
			Create_CountryCodeRange(850, 850, "CUBA");
			Create_CountryCodeRange(858, 858, "SLOVAKIA");
			Create_CountryCodeRange(859, 859, "CZECH REPUBLIC");
			Create_CountryCodeRange(860, 860, "SERBIA");
			Create_CountryCodeRange(865, 865, "MONGOLIA");
			Create_CountryCodeRange(867, 867, "NORTH KOREA");
			Create_CountryCodeRange(868, 869, "TURKEY");
			Create_CountryCodeRange(870, 879, "NETHERLANDS");
			Create_CountryCodeRange(880, 880, "SOUTH KOREA");
			Create_CountryCodeRange(884, 884, "CAMBODIA");
			Create_CountryCodeRange(885, 885, "THAILAND");
			Create_CountryCodeRange(888, 888, "SINGAPORE");
			Create_CountryCodeRange(890, 890, "INDIA");
			Create_CountryCodeRange(893, 893, "VIETNAM");
			Create_CountryCodeRange(896, 896, "PAKISTAN");
			Create_CountryCodeRange(899, 899, "INDONESIA");
			Create_CountryCodeRange(900, 919, "AUSTRIA");
			Create_CountryCodeRange(930, 939, "AUSTRALIA");
			Create_CountryCodeRange(940, 949, "NEW ZEALAND");
			Create_CountryCodeRange(950, 950, "GS1 GLOBAL OFFICE SPECIAL APPLICATIONS");
			Create_CountryCodeRange(951, 951, "EPC GLOBAL SPECIAL APPLICATIONS");
			Create_CountryCodeRange(955, 955, "MALAYSIA");
			Create_CountryCodeRange(958, 958, "MACAU");
			Create_CountryCodeRange(960, 961, "GS1 UK OFFICE: GTIN-8 ALLOCATIONS");
			Create_CountryCodeRange(962, 969, "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			Create_CountryCodeRange(977, 977, "SERIAL PUBLICATIONS (ISSN)");
			Create_CountryCodeRange(978, 979, "BOOKLAND (ISBN) – 979-0 USED FOR SHEET MUSIC (ISMN-13, REPLACES DEPRECATED ISMN M- NUMBERS)");
			Create_CountryCodeRange(980, 980, "REFUND RECEIPTS");
			Create_CountryCodeRange(981, 984, "GS1 COUPON IDENTIFICATION FOR COMMON CURRENCY AREAS");
			Create_CountryCodeRange(990, 999, "GS1 COUPON IDENTIFICATION");
        }//init_CountryCodes
        private void CheckDigit()
        {
            try
            {
                var rawDataHolder = Raw_Data.Substring(0, 12);

                var even = 0;
                var odd = 0;

                for (var i = 0; i < rawDataHolder.Length; i++)
                {
                    if (i % 2 == 0)
                        odd += Int32.Parse(rawDataHolder.Substring(i, 1));
                    else
                        even += Int32.Parse(rawDataHolder.Substring(i, 1)) * 3;
                }//for

                var total = even + odd;
                var cs = total % 10;
                cs = 10 - cs;
                if (cs == 10)
                    cs = 0;

                Raw_Data = rawDataHolder + cs.ToString()[0];
            }//try
            catch
            {
                Error("EEAN13-4: Error calculating check digit.");
            }//catch
        }

        #region IBarcode Members

        public string Encoded_Value => Encode_EAN13();

        #endregion
    }
}
