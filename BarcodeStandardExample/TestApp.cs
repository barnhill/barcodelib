using BarcodeLib;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BarcodeStandardExample
{
    /// <summary>
    /// This form is a test form to show what all you can do with the Barcode Library.
    /// Only one call is actually needed to do the encoding and return the image of the 
    /// barcode but the rest is just flare and user interface ... stuff.
    /// </summary>
    public partial class TestApp : Form
    {
        Barcode b = new Barcode();
        
        public TestApp()
        {
            InitializeComponent();
        }

        private void TestApp_Load(object sender, EventArgs e)
        {
            this.cbEncodeType.SelectedIndex = 0;
            this.cbBarcodeAlign.SelectedIndex = 0;
            this.cbLabelLocation.SelectedIndex = 0;

            this.cbRotateFlip.DataSource = Enum.GetNames(typeof(RotateFlipType));

            int i = 0;
            foreach (object o in cbRotateFlip.Items)
            {
                if (o.ToString().Trim().ToLower() == "rotatenoneflipnone")
                    break;
                i++;
            }//foreach
            this.cbRotateFlip.SelectedIndex = i;

            //Show library version
            this.tslblLibraryVersion.Text = "Barcode Library Version: " + Barcode.Version.ToString();

            this.btnBackColor.BackColor = this.b.BackColor;
            this.btnForeColor.BackColor = this.b.ForeColor;
        }//Form1_Load

        private void btnEncode_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int W = Convert.ToInt32(this.txtWidth.Text.Trim());
            int H = Convert.ToInt32(this.txtHeight.Text.Trim());
            b.Alignment = AlignmentPositions.CENTER;

            //barcode alignment
            switch (cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower())
            {
                case "left": b.Alignment = AlignmentPositions.LEFT; break;
                case "right": b.Alignment = AlignmentPositions.RIGHT; break;
                default: b.Alignment = AlignmentPositions.CENTER; break;
            }//switch

            TYPE type = TYPE.UNSPECIFIED;
            switch (cbEncodeType.SelectedItem.ToString().Trim())
            {
                case "UPC-A": type = TYPE.UPCA; break;
                case "UPC-E": type = TYPE.UPCE; break;
                case "UPC 2 Digit Ext.": type = TYPE.UPC_SUPPLEMENTAL_2DIGIT; break;
                case "UPC 5 Digit Ext.": type = TYPE.UPC_SUPPLEMENTAL_5DIGIT; break;
                case "EAN-13": type = TYPE.EAN13; break;
                case "JAN-13": type = TYPE.JAN13; break;
                case "EAN-8": type = TYPE.EAN8; break;
                case "ITF-14": type = TYPE.ITF14; break;
                case "Codabar": type = TYPE.Codabar; break;
                case "PostNet": type = TYPE.PostNet; break;
                case "Bookland/ISBN": type = TYPE.BOOKLAND; break;
                case "Code 11": type = TYPE.CODE11; break;
                case "Code 39": type = TYPE.CODE39; break;
                case "Code 39 Extended": type = TYPE.CODE39Extended; break;
                case "Code 39 Mod 43": type = TYPE.CODE39_Mod43; break;
                case "Code 93": type = TYPE.CODE93; break;
                case "LOGMARS": type = TYPE.LOGMARS; break;
                case "MSI": type = TYPE.MSI_Mod10; break;
                case "Interleaved 2 of 5": type = TYPE.Interleaved2of5; break;
                case "Interleaved 2 of 5 Mod 10": type = TYPE.Interleaved2of5_Mod10; break;
                case "Standard 2 of 5": type = TYPE.Standard2of5; break;
                case "Standard 2 of 5 Mod 10": type = TYPE.Standard2of5_Mod10; break;
                case "Code 128": type = TYPE.CODE128; break;
                case "Code 128-A": type = TYPE.CODE128A; break;
                case "Code 128-B": type = TYPE.CODE128B; break;
                case "Code 128-C": type = TYPE.CODE128C; break;
                case "Telepen": type = TYPE.TELEPEN; break;
                case "FIM": type = TYPE.FIM; break;
                case "Pharmacode": type = TYPE.PHARMACODE; break;
                default: MessageBox.Show("Please specify the encoding type."); break;
            }//switch

            try
            {
                if (type != TYPE.UNSPECIFIED)
                {
                    try
                    {
                        b.BarWidth = textBoxBarWidth.Text.Trim().Length < 1 ? null : (int?)Convert.ToInt32(textBoxBarWidth.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Unable to parse BarWidth: " + ex.Message, ex);
                    }
                    try
                    {
                        b.AspectRatio = textBoxAspectRatio.Text.Trim().Length < 1 ? null : (double?)Convert.ToDouble(textBoxAspectRatio.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Unable to parse AspectRatio: " + ex.Message, ex);
                    }

                    b.IncludeLabel = this.chkGenerateLabel.Checked;
                    b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), this.cbRotateFlip.SelectedItem.ToString(), true);

                    if (!String.IsNullOrEmpty(this.textBox1.Text.Trim()))
                        b.AlternateLabel = this.textBox1.Text;
                    else
                        b.AlternateLabel = this.txtData.Text;

                    //label alignment and position
                    switch (this.cbLabelLocation.SelectedItem.ToString().Trim().ToUpper())
                    {
                        case "BOTTOMLEFT":  b.LabelPosition = LabelPositions.BOTTOMLEFT; break;
                        case "BOTTOMRIGHT": b.LabelPosition = LabelPositions.BOTTOMRIGHT; break;
                        case "TOPCENTER": b.LabelPosition = LabelPositions.TOPCENTER; break;
                        case "TOPLEFT": b.LabelPosition = LabelPositions.TOPLEFT; break;
                        case "TOPRIGHT": b.LabelPosition = LabelPositions.TOPRIGHT; break;
                        default: b.LabelPosition = LabelPositions.BOTTOMCENTER; break;
                    }//switch

                    //===== Encoding performed here =====
                    barcode.BackgroundImage = b.Encode(type, this.txtData.Text.Trim(), this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);
                    //===================================
                    
                    //show the encoding time
                    this.lblEncodingTime.Text = "(" + Math.Round(b.EncodingTime, 0, MidpointRounding.AwayFromZero).ToString() + "ms)";
                    
                    txtEncoded.Text = b.EncodedValue;

                    tsslEncodedType.Text = "Encoding Type: " + b.EncodedType.ToString();

                    // Read dynamically calculated Width/Height because the user is interested.
                    if (b.BarWidth.HasValue)
                        txtWidth.Text = b.Width.ToString();
                    if (b.AspectRatio.HasValue)
                        txtHeight.Text = b.Height.ToString();
                }//if

                //reposition the barcode image to the middle
                barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
            }//try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//catch
        }//btnEncode_Click

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tif)|*.tif";
            sfd.FilterIndex = 2;
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveTypes savetype = SaveTypes.UNSPECIFIED;
                switch (sfd.FilterIndex)
                {
                    case 1: /* BMP */  savetype = SaveTypes.BMP; break;
                    case 2: /* GIF */  savetype = SaveTypes.GIF; break;
                    case 3: /* JPG */  savetype = SaveTypes.JPG; break;
                    case 4: /* PNG */  savetype = SaveTypes.PNG; break;
                    case 5: /* TIFF */ savetype = SaveTypes.TIFF; break;
                    default: break;
                }//switch
                b.SaveImage(sfd.FileName, savetype);
            }//if
        }//btnSave_Click

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
        }//splitContainer1_SplitterMoved

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdialog = new ColorDialog())
            {
                cdialog.AnyColor = true;
                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    this.b.ForeColor = cdialog.Color;
                    this.btnForeColor.BackColor = cdialog.Color;
                }//if
            }//using
        }//btnForeColor_Click

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdialog = new ColorDialog())
            {
                cdialog.AnyColor = true;
                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    this.b.BackColor = cdialog.Color;
                    this.btnBackColor.BackColor = cdialog.Color;
                }//if
            }//using
        }//btnBackColor_Click

        private void btnSaveJSON_Click(object sender, EventArgs e)
        {
            btnEncode_Click(sender, e);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON Files|*.json";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.Write(b.ToJSON(chkIncludeImageInSavedData.Checked));
                    }//using
                }//if
            }//using
        }

        private void btnLoadJSON_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = "JSON Files|*.json";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileContents = File.ReadAllText(ofd.FileName);
                    using (BarcodeStandard.SaveData savedData = Barcode.FromJSON(ofd.OpenFile()))
                    {
                        LoadFromSaveData(savedData);
                    }//using
                }//if
            }//using

            //populate the local object
            btnEncode_Click(sender, e);

            //reposition the barcode image to the middle
            barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            btnEncode_Click(sender, e);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "XML Files|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.Write(b.ToXML(chkIncludeImageInSavedData.Checked));
                    }//using
                }//if
            }//using
        }//btnGetXML_Click

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = "XML Files|*.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileContents = File.ReadAllText(ofd.FileName);
                    using (BarcodeStandard.SaveData savedData = Barcode.FromXML(ofd.OpenFile()))
                    {
                        LoadFromSaveData(savedData);
                    }//using
                }//if
            }//using

            //populate the local object
            btnEncode_Click(sender, e);

            //reposition the barcode image to the middle
            barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
        }

        private void LoadFromSaveData(BarcodeStandard.SaveData saveData)
        {
            //load image from xml
            this.barcode.Width = saveData.ImageWidth;
            this.barcode.Height = saveData.ImageHeight;

            if (saveData.Image != null)
            {
                this.barcode.BackgroundImage = Barcode.GetImageFromSaveData(saveData);
            }

            //populate the screen
            this.txtData.Text = saveData.RawData;
            this.chkGenerateLabel.Checked = saveData.IncludeLabel;

            this.txtEncoded.Text = saveData.EncodedValue;
            this.btnForeColor.BackColor = ColorTranslator.FromHtml(saveData.Forecolor);
            this.btnBackColor.BackColor = ColorTranslator.FromHtml(saveData.Backcolor);
            this.txtWidth.Text = saveData.ImageWidth.ToString();
            this.txtHeight.Text = saveData.ImageHeight.ToString();

            switch (saveData.Type)
            {
                case "UCC12":
                case "UPCA":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC-A");
                    break;
                case "UCC13":
                case "EAN13":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("EAN-13");
                    break;
                case "Interleaved2of5_Mod10":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Interleaved 2 of 5 Mod 10");
                    break;
                case "Interleaved2of5":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Interleaved 2 of 5");
                    break;
                case "Standard2of5_Mod10":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Standard 2 of 5 Mod 10");
                    break;
                case "Standard2of5":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Standard 2 of 5");
                    break;
                case "LOGMARS":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("LOGMARS");
                    break;
                case "CODE39":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39");
                    break;
                case "CODE39Extended":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39 Extended");
                    break;
                case "CODE39_Mod43":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39 Mod 43");
                    break;
                case "Codabar":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Codabar");
                    break;
                case "PostNet":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("PostNet");
                    break;
                case "ISBN":
                case "BOOKLAND":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Bookland/ISBN");
                    break;
                case "JAN13":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("JAN-13");
                    break;
                case "UPC_SUPPLEMENTAL_2DIGIT":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC 2 Digit Ext.");
                    break;
                case "MSI_Mod10":
                case "MSI_2Mod10":
                case "MSI_Mod11":
                case "MSI_Mod11_Mod10":
                case "Modified_Plessey":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("MSI");
                    break;
                case "UPC_SUPPLEMENTAL_5DIGIT":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC 5 Digit Ext.");
                    break;
                case "UPCE":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC-E");
                    break;
                case "EAN8":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("EAN-8");
                    break;
                case "USD8":
                case "CODE11":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 11");
                    break;
                case "CODE128":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128");
                    break;
                case "CODE128A":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-A");
                    break;
                case "CODE128B":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-B");
                    break;
                case "CODE128C":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-C");
                    break;
                case "ITF14":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("ITF-14");
                    break;
                case "CODE93":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 93");
                    break;
                case "FIM":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("FIM");
                    break;
                case "Pharmacode":
                    this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Pharmacode");
                    break;

                default: throw new Exception("ELOADXML-1: Unsupported encoding type in XML.");
            }//switch
        }

        private void btnMassGeneration_Click(object sender, EventArgs e)
        {
            int x = 1000;
            double sum = 0;

            progressBar1.Value = 0;
            progressBar1.Maximum = x;

            for (int i = 0; i < x; i++)
            {
                String data = new Random().Next(10000, 10000000).ToString();

                DateTime dtStartTime = DateTime.Now;
                GetBarCode39(data);
                sum += (DateTime.Now - dtStartTime).TotalMilliseconds;
                progressBar1.Value = i;
            }

            progressBar1.Visible = false;
            lblAverageGenerationTime.Text = Math.Round(sum / x, 2, MidpointRounding.AwayFromZero).ToString() + " ms";
        }

        public byte[] GetBarCode39(string CodeNumber, int Length = 1000, int Height = 200, int FontSize = 40)
        {
            try
            {
                using (Barcode barcode = new Barcode())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        barcode.IncludeLabel = true;
                        barcode.Alignment = AlignmentPositions.CENTER;
                        barcode.LabelFont = new Font(FontFamily.GenericMonospace, FontSize * Barcode.DotsPerPointAt96Dpi, FontStyle.Regular, GraphicsUnit.Pixel);

                        var barcodeImage = barcode.Encode(TYPE.CODE39, CodeNumber, Color.Black, Color.White, Length, Height);

                        barcodeImage.Save(ms, ImageFormat.Jpeg);

                        using (BinaryReader reader = new BinaryReader(ms))
                        {
                            byte[] bytes = (byte[])reader.ReadBytes((int)ms.Length).Clone();

                            reader.Dispose();
                            ms.Dispose();

                            return bytes;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }//class
}//namespace