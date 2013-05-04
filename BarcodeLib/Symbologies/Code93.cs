using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib.Symbologies
{
    /// <summary>
    ///  Code 93 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    class Code93 : BarcodeCommon, IBarcode
    {
        private System.Data.DataTable C93_Code = new System.Data.DataTable("C93_Code");

        /// <summary>
        /// Encodes with Code93.
        /// </summary>
        /// <param name="input">Data to encode.</param>
        public Code93(string input)
        {
            Raw_Data = input;
        }//Code93

        /// <summary>
        /// Encode the raw data using the Code 93 algorithm.
        /// </summary>
        private string Encode_Code93()
        {
            this.init_Code93();

            string FormattedData = Add_CheckDigits(Raw_Data);

            string result = C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();
            foreach (char c in FormattedData)
            {
                try
                {
                    result += C93_Code.Select("Character = '" + c.ToString() + "'")[0]["Encoding"].ToString();
                }//try
                catch
                {
                        Error("EC93-1: Invalid data.");
                }//catch
            }//foreach

            result += C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();

            //termination bar
            result += "1";

            //clear the hashtable so it no longer takes up memory
            this.C93_Code.Clear();

            return result;
        }//Encode_Code93
        private void init_Code93()
        {
            C93_Code.Rows.Clear();
            C93_Code.Columns.Clear();
            C93_Code.Columns.Add("Value");
            C93_Code.Columns.Add("Character");
            C93_Code.Columns.Add("Encoding");
            C93_Code.Rows.Add(new object[] { "0", "0", "100010100" });
            C93_Code.Rows.Add(new object[] { "1", "1", "101001000" });
            C93_Code.Rows.Add(new object[] { "2", "2", "101000100" });
            C93_Code.Rows.Add(new object[] { "3", "3", "101000010" });
            C93_Code.Rows.Add(new object[] { "4",  "4", "100101000" });
            C93_Code.Rows.Add(new object[] { "5",  "5", "100100100" });
            C93_Code.Rows.Add(new object[] { "6",  "6", "100100010" });
            C93_Code.Rows.Add(new object[] { "7",  "7", "101010000" });
            C93_Code.Rows.Add(new object[] { "8",  "8", "100010010" });
            C93_Code.Rows.Add(new object[] { "9",  "9", "100001010" });
            C93_Code.Rows.Add(new object[] { "10", "A", "110101000" });
            C93_Code.Rows.Add(new object[] { "11", "B", "110100100" });
            C93_Code.Rows.Add(new object[] { "12", "C", "110100010" });
            C93_Code.Rows.Add(new object[] { "13", "D", "110010100" });
            C93_Code.Rows.Add(new object[] { "14", "E", "110010010" });
            C93_Code.Rows.Add(new object[] { "15", "F", "110001010" });
            C93_Code.Rows.Add(new object[] { "16", "G", "101101000" });
            C93_Code.Rows.Add(new object[] { "17", "H", "101100100" });
            C93_Code.Rows.Add(new object[] { "18", "I", "101100010" });
            C93_Code.Rows.Add(new object[] { "19", "J", "100110100" });
            C93_Code.Rows.Add(new object[] { "20", "K", "100011010" });
            C93_Code.Rows.Add(new object[] { "21", "L", "101011000" });
            C93_Code.Rows.Add(new object[] { "22", "M", "101001100" });
            C93_Code.Rows.Add(new object[] { "23", "N", "101000110" });
            C93_Code.Rows.Add(new object[] { "24", "O", "100101100" });
            C93_Code.Rows.Add(new object[] { "25", "P", "100010110" });
            C93_Code.Rows.Add(new object[] { "26", "Q", "110110100" });
            C93_Code.Rows.Add(new object[] { "27", "R", "110110010" });
            C93_Code.Rows.Add(new object[] { "28", "S", "110101100" });
            C93_Code.Rows.Add(new object[] { "29", "T", "110100110" });
            C93_Code.Rows.Add(new object[] { "30", "U", "110010110" });
            C93_Code.Rows.Add(new object[] { "31", "V", "110011010" });
            C93_Code.Rows.Add(new object[] { "32", "W", "101101100" });
            C93_Code.Rows.Add(new object[] { "33", "X", "101100110" });
            C93_Code.Rows.Add(new object[] { "34", "Y", "100110110" });
            C93_Code.Rows.Add(new object[] { "35", "Z", "100111010" });
            C93_Code.Rows.Add(new object[] { "36", "-", "100101110" });
            C93_Code.Rows.Add(new object[] { "37", ".", "111010100" });
            C93_Code.Rows.Add(new object[] { "38", " ", "111010010" });
            C93_Code.Rows.Add(new object[] { "39", "$", "111001010" });
            C93_Code.Rows.Add(new object[] { "40", "/", "101101110" });
            C93_Code.Rows.Add(new object[] { "41", "+", "101110110" });
            C93_Code.Rows.Add(new object[] { "42", "%", "110101110" });
            C93_Code.Rows.Add(new object[] { "43", "(", "100100110" });//dont know what character actually goes here
            C93_Code.Rows.Add(new object[] { "44", ")", "111011010" });//dont know what character actually goes here
            C93_Code.Rows.Add(new object[] { "45", "#", "111010110" });//dont know what character actually goes here
            C93_Code.Rows.Add(new object[] { "46", "@", "100110010" });//dont know what character actually goes here
            C93_Code.Rows.Add(new object[] { "-",  "*", "101011110" });
        }//init_Code93
        private string Add_CheckDigits(string input)
        {
            //populate the C weights
            int[] aryCWeights = new int[input.Length];
            int curweight = 1;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (curweight > 20)
                    curweight = 1;
                aryCWeights[i] = curweight;
                curweight++;
            }//for

            //populate the K weights
            int[] aryKWeights = new int[input.Length + 1];
            curweight = 1;
            for (int i = input.Length; i >= 0; i--)
            {
                if (curweight > 15)
                    curweight = 1;
                aryKWeights[i] = curweight;
                curweight++;
            }//for

            //calculate C checksum
            int SUM = 0;
            for (int i = 0; i < input.Length; i++)
            {
                SUM += aryCWeights[i] * Int32.Parse(C93_Code.Select("Character = '" + input[i].ToString() + "'")[0]["Value"].ToString());
            }//for
            int ChecksumValue = SUM % 47;

            input += C93_Code.Select("Value = '" + ChecksumValue.ToString() + "'")[0]["Character"].ToString();

            //calculate K checksum
            SUM = 0;
            for (int i = 0; i < input.Length; i++)
            {
                SUM += aryKWeights[i] * Int32.Parse(C93_Code.Select("Character = '" + input[i].ToString() + "'")[0]["Value"].ToString());
            }//for
            ChecksumValue = SUM % 47;

            input += C93_Code.Select("Value = '" + ChecksumValue.ToString() + "'")[0]["Character"].ToString();

            return input;
        }//Calculate_CheckDigits
        
        #region IBarcode Members

        public string Encoded_Value
        {
            get { return Encode_Code93(); }
        }

        #endregion
    }//class
}//namespace
