using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BarcodeStandard;
using SkiaSharp;
using Type = BarcodeStandard.Type;

namespace BarcodeStandardExample
{
    /// <summary>
    /// This form is a test form to show what all you can do with the Barcode Library.
    /// Only one call is actually needed to do the encoding and return the image of the
    /// barcode but the rest is just flare and user interface ... stuff.
    /// </summary>
    public partial class TestApp : Form
    {
        private Barcode _b = new Barcode();

        public TestApp()
        {
            InitializeComponent();
        }

        private void TestApp_Load(object sender, EventArgs e)
        {
            cbEncodeType.SelectedIndex = 0;
            cbBarcodeAlign.SelectedIndex = 0;
            cbGuardBarsMode.SelectedIndex = 0;
            cbBearerBarsMode.SelectedIndex = 0;

            //Show library version
            tslblLibraryVersion.Text = @"Barcode Library Version: " + Barcode.Version;

            btnBackColor.BackColor = ColorTranslator.FromHtml(_b.BackColor.ToString());
            btnForeColor.BackColor = ColorTranslator.FromHtml(_b.ForeColor.ToString());
        }//Form1_Load

        private void btnEncode_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            var w = Convert.ToInt32(txtWidth.Text.Trim());
            var h = Convert.ToInt32(txtHeight.Text.Trim());
            _b.Alignment = AlignmentPositions.Center;
            _b.IncludeTopBar = chkTopBar.Checked;

            //barcode alignment
            switch (cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower())
            {
                case "left": _b.Alignment = AlignmentPositions.Left; break;
                case "right": _b.Alignment = AlignmentPositions.Right; break;
                default: _b.Alignment = AlignmentPositions.Center; break;
            }//switch

            //guard bars mode
            switch (cbGuardBarsMode.SelectedItem.ToString().Trim().ToLower())
            {
                case "disabled": _b.GuardBarsMode = GuardBarsMode.Disabled; break;
                case "enabledfirstcharonquietzone": _b.GuardBarsMode = GuardBarsMode.EnabledFirstCharOnQuietZone; break;
                default: _b.GuardBarsMode = GuardBarsMode.Enabled; break;
            }//switch

            //bearer bars mode
            switch (cbBearerBarsMode.SelectedItem.ToString().Trim().ToLower())
            {
                case "disabled": _b.BearerBarsMode = BearerBarsMode.Disabled; break;
                case "bearerbars": _b.BearerBarsMode = BearerBarsMode.BearerBars; break;
                default: _b.BearerBarsMode = BearerBarsMode.Frame; break;
            }//switch

            var type = GetTypeSelected();

            try
            {
                if (type != Type.Unspecified)
                {
                    try
                    {
                        _b.BarWidth = textBoxBarWidth.Text.Trim().Length < 1 ? null : Convert.ToInt32(textBoxBarWidth.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Unable to parse BarWidth: " + ex.Message, ex);
                    }
                    try
                    {
                        _b.AspectRatio = textBoxAspectRatio.Text.Trim().Length < 1 ? null : Convert.ToDouble(textBoxAspectRatio.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Unable to parse AspectRatio: " + ex.Message, ex);
                    }

                    _b.IncludeLabel = chkGenerateLabel.Checked;

                    if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                        _b.AlternateLabel = textBox1.Text;
                    else _b.AlternateLabel = null;

                    //===== Encoding performed here =====
                    barcode.BackgroundImage = Image.FromStream(_b.Encode(type, txtData.Text.Trim(), _b.ForeColor, _b.BackColor, w, h).Encode().AsStream());
                    //===================================

                    //show the encoding time
                    lblEncodingTime.Text = @"(" + Math.Round(_b.EncodingTime, 0, MidpointRounding.AwayFromZero) + @"ms)";

                    txtEncoded.Text = _b.EncodedValue;

                    tsslEncodedType.Text = @"Encoding Type: " + _b.EncodedType;

                    // Read dynamically calculated Width/Height because the user is interested.
                    if (_b.BarWidth.HasValue)
                        txtWidth.Text = _b.Width.ToString();
                    if (_b.AspectRatio.HasValue)
                        txtHeight.Text = _b.Height.ToString();
                }//if

                //reposition the barcode image to the middle
                barcode.Location = new Point((barcode.Location.X + barcode.Width / 2) - barcode.Width / 2, (barcode.Location.Y + barcode.Height / 2) - barcode.Height / 2);
            }//try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//catch
        }//btnEncode_Click

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = @"JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|WEBP (*.webp)|*.webp";
            sfd.FilterIndex = 2;
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var type = SaveTypes.Unspecified;
            switch (sfd.FilterIndex)
            {
                case 1: /* JPG */  type = SaveTypes.Jpg; break;
                case 2: /* PNG */  type = SaveTypes.Png; break;
                case 3: /* WEBP*/  type = SaveTypes.Webp; break;
            }//switch
            _b.SaveImage(sfd.FileName, type);
        }//btnSave_Click

        private Type GetTypeSelected()
        {
            var type = Type.Unspecified;
            switch (cbEncodeType.SelectedItem.ToString().Trim())
            {
                case "UPC-A": type = Type.UpcA; break;
                case "UPC-E": type = Type.UpcE; break;
                case "UPC 2 Digit Ext.": type = Type.UpcSupplemental2Digit; break;
                case "UPC 5 Digit Ext.": type = Type.UpcSupplemental5Digit; break;
                case "EAN-13": type = Type.Ean13; break;
                case "JAN-13": type = Type.Jan13; break;
                case "EAN-8": type = Type.Ean8; break;
                case "ITF-14": type = Type.Itf14; break;
                case "Codabar": type = Type.Codabar; break;
                case "PostNet": type = Type.PostNet; break;
                case "Bookland/ISBN": type = Type.Bookland; break;
                case "Code 11": type = Type.Code11; break;
                case "Code 39": type = Type.Code39; break;
                case "Code 39 Extended": type = Type.Code39Extended; break;
                case "Code 39 Mod 43": type = Type.Code39Mod43; break;
                case "Code 93": type = Type.Code93; break;
                case "LOGMARS": type = Type.Logmars; break;
                case "MSI Mod 10": type = Type.MsiMod10; break;
                case "MSI Mod 11": type = Type.MsiMod11; break;
                case "MSI 2 Mod 10": type = Type.Msi2Mod10; break;
                case "MSI Mod 11 Mod 10": type = Type.MsiMod11Mod10; break;
                case "Interleaved 2 of 5": type = Type.Interleaved2Of5; break;
                case "Interleaved 2 of 5 Mod 10": type = Type.Interleaved2Of5Mod10; break;
                case "Standard 2 of 5": type = Type.Standard2Of5; break;
                case "Standard 2 of 5 Mod 10": type = Type.Standard2Of5Mod10; break;
                case "Code 128": type = Type.Code128; break;
                case "Code 128-A": type = Type.Code128A; break;
                case "Code 128-B": type = Type.Code128B; break;
                case "Code 128-C": type = Type.Code128C; break;
                case "Telepen": type = Type.Telepen; break;
                case "FIM": type = Type.Fim; break;
                case "Pharmacode": type = Type.Pharmacode; break;
                default: MessageBox.Show(@"Please specify the encoding type."); break;
            }//switch

            return type;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            barcode.Location = new Point((barcode.Location.X + barcode.Width / 2) - barcode.Width / 2, (barcode.Location.Y + barcode.Height / 2) - barcode.Height / 2);
        }//splitContainer1_SplitterMoved

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.AnyColor = true;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _b.ForeColor = new SKColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, colorDialog.Color.A);
                    btnForeColor.BackColor = colorDialog.Color;
                }//if
            }//using
        }//btnForeColor_Click

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.AnyColor = true;
                if (colorDialog.ShowDialog() != DialogResult.OK) return;
                _b.BackColor = new SKColor(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, colorDialog.Color.A);
                btnBackColor.BackColor = colorDialog.Color;
                //if
            }//using
        }//btnBackColor_Click

        private void btnSaveJSON_Click(object sender, EventArgs e)
        {
            btnEncode_Click(sender, e);

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = @"JSON Files|*.json";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                using (var sw = new StreamWriter(sfd.FileName))
                {
                    sw.Write(_b.ToJson(chkIncludeImageInSavedData.Checked));
                }//using
                //if
            }//using
        }

        private void btnLoadJSON_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = @"JSON Files|*.json";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var savedData = Barcode.FromJson(ofd.OpenFile()))
                    {
                        LoadFromSaveData(savedData);
                    }//using
                }//if
            }//using

            //populate the local object
            btnEncode_Click(sender, e);

            //reposition the barcode image to the middle
            barcode.Location = new Point((barcode.Location.X + barcode.Width / 2) - barcode.Width / 2, (barcode.Location.Y + barcode.Height / 2) - barcode.Height / 2);
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            btnEncode_Click(sender, e);

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = @"XML Files|*.xml";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                using (var sw = new StreamWriter(sfd.FileName))
                {
                    sw.Write(_b.ToXml(chkIncludeImageInSavedData.Checked));
                }//using
            }//using
        }//btnGetXML_Click

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = @"XML Files|*.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var savedData = Barcode.FromXml(ofd.OpenFile()))
                    {
                        LoadFromSaveData(savedData);
                    }//using
                }//if
            }//using

            //populate the local object
            btnEncode_Click(sender, e);

            //reposition the barcode image to the middle
            barcode.Location = new Point((barcode.Location.X + barcode.Width / 2) - barcode.Width / 2, (barcode.Location.Y + barcode.Height / 2) - barcode.Height / 2);
        }

        private void LoadFromSaveData(SaveData saveData)
        {
            //load image from xml
            barcode.Width = saveData.ImageWidth;
            barcode.Height = saveData.ImageHeight;

            if (saveData.Image != null)
            {
                barcode.BackgroundImage = Image.FromStream(Barcode.GetImageFromSaveData(saveData).Encode().AsStream());
            }

            //populate the screen
            txtData.Text = saveData.RawData;
            chkGenerateLabel.Checked = saveData.IncludeLabel;

            txtEncoded.Text = saveData.EncodedValue;
            btnForeColor.ForeColor = ColorTranslator.FromHtml(saveData.Forecolor);
            btnBackColor.BackColor = ColorTranslator.FromHtml(saveData.Backcolor);
            txtWidth.Text = saveData.ImageWidth.ToString();
            txtHeight.Text = saveData.ImageHeight.ToString();

            switch (saveData.Type)
            {
                case "UCC12":
                case "UPCA":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("UPC-A");
                    break;

                case "UCC13":
                case "EAN13":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("EAN-13");
                    break;

                case "Interleaved2of5_Mod10":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Interleaved 2 of 5 Mod 10");
                    break;

                case "Interleaved2of5":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Interleaved 2 of 5");
                    break;

                case "Standard2of5_Mod10":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Standard 2 of 5 Mod 10");
                    break;

                case "Standard2of5":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Standard 2 of 5");
                    break;

                case "LOGMARS":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("LOGMARS");
                    break;

                case "CODE39":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 39");
                    break;

                case "CODE39Extended":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 39 Extended");
                    break;

                case "CODE39_Mod43":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 39 Mod 43");
                    break;

                case "Codabar":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Codabar");
                    break;

                case "PostNet":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("PostNet");
                    break;

                case "ISBN":
                case "BOOKLAND":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Bookland/ISBN");
                    break;

                case "JAN13":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("JAN-13");
                    break;

                case "UPC_SUPPLEMENTAL_2DIGIT":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("UPC 2 Digit Ext.");
                    break;

                case "MSI_Mod10":
                case "MSI_2Mod10":
                case "MSI_Mod11":
                case "MSI_Mod11_Mod10":
                case "Modified_Plessey":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("MSI");
                    break;

                case "UPC_SUPPLEMENTAL_5DIGIT":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("UPC 5 Digit Ext.");
                    break;

                case "UPCE":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("UPC-E");
                    break;

                case "EAN8":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("EAN-8");
                    break;

                case "USD8":
                case "CODE11":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 11");
                    break;

                case "CODE128":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 128");
                    break;

                case "CODE128A":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 128-A");
                    break;

                case "CODE128B":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 128-B");
                    break;

                case "CODE128C":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 128-C");
                    break;

                case "ITF14":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("ITF-14");
                    break;

                case "CODE93":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Code 93");
                    break;

                case "FIM":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("FIM");
                    break;

                case "Pharmacode":
                    cbEncodeType.SelectedIndex = cbEncodeType.FindString("Pharmacode");
                    break;

                default: throw new Exception("ELOADXML-1: Unsupported encoding type in XML.");
            }//switch
        }

        private void btnMassGeneration_Click(object sender, EventArgs e)
        {
            var x = 1000;
            double sum = 0;

            progressBar1.Value = 0;
            progressBar1.Maximum = x;
            progressBar1.Visible = true;
            lblAverageGenerationTime.Text = "";
            string data;
            var random = new Random();

            using (var b = new Barcode())
            {
                for (var i = 0; i < x; i++)
                {
                    data = txtData.Text.Aggregate("", (current, _) => current + random.Next(0, 9));

                    var dtStartTime = DateTime.Now;
                    var bitmap = GetBarCode(b, data, GetTypeSelected());

                    if (bitmap == null)
                    {
                        // error encountered so bail out
                        progressBar1.Visible = false;
                        lblAverageGenerationTime.Text = @"Error";
                        return;
                    }

                    sum += (DateTime.Now - dtStartTime).TotalMilliseconds;
                    progressBar1.Value = i;
                }
            }

            progressBar1.Visible = false;
            lblAverageGenerationTime.Text = Math.Round(sum / x, 2, MidpointRounding.AwayFromZero) + @" ms";
        }

        private static SKBitmap GetBarCode(Barcode b, string codeNumber, Type type, int length = 1000, int height = 200, int fontSize = 40)
        {
            try
            {
                b.IncludeLabel = false;
                b.Alignment = AlignmentPositions.Center;
                b.LabelFont = new SKFont(SKTypeface.Default, fontSize);

                var barcodeImage = b.Encode(type, codeNumber, SKColors.Black, SKColors.White, length, height);
                return SKBitmap.FromImage(barcodeImage);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }//class
}//namespace