using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace BarcodeLib
{
    class Labels
    {
        /// <summary>
        /// Draws Label for ITF-14 barcodes
        /// </summary>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static Image Label_ITF14(Barcode Barcode, Bitmap img)
        {
            try
            {
                Font font = Barcode.LabelFont;

                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(img, (float)0, (float)0);

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //color a white box at the bottom of the barcode to hold the string of data
                    using (SolidBrush backBrush = new SolidBrush(Barcode.BackColor))
                    {
                        g.FillRectangle(backBrush, new Rectangle(0, img.Height - (font.Height - 2), img.Width, font.Height));
                    }

                    //draw datastring under the barcode image
                    StringFormat f = new StringFormat();
                    f.Alignment = StringAlignment.Center;

                    using (SolidBrush foreBrush = new SolidBrush(Barcode.ForeColor))
                    {
                        g.DrawString(Barcode.AlternateLabel == null ? Barcode.RawData : Barcode.AlternateLabel, font, foreBrush, (float)(img.Width / 2), img.Height - font.Height + 1, f);
                    }

                    using (Pen pen = new Pen(Barcode.ForeColor, (float)img.Height / 16))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        g.DrawLine(pen, new Point(0, img.Height - font.Height - 2), new Point(img.Width, img.Height - font.Height - 2));//bottom
                    }

                    g.Save();
                }//using
                return img;
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_ITF14-1: " + ex.Message);
            }//catch
        }

        /// <summary>
        /// Draws Label for Generic barcodes
        /// </summary>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static Image Label_Generic(Barcode Barcode, Bitmap img)
        {
            try
            {
                Font font = Barcode.LabelFont;

                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(img, (float)0, (float)0);

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    StringFormat f = new StringFormat();
                    f.Alignment = StringAlignment.Near;
                    f.LineAlignment = StringAlignment.Near;
                    int LabelX = 0;
                    int LabelY = 0;

                    switch (Barcode.LabelPosition)
                    {
                        case LabelPositions.BOTTOMCENTER:
                            LabelX = img.Width / 2;
                            LabelY = img.Height - (font.Height);
                            f.Alignment = StringAlignment.Center;
                            break;
                        case LabelPositions.BOTTOMLEFT:
                            LabelX = 0;
                            LabelY = img.Height - (font.Height);
                            f.Alignment = StringAlignment.Near;
                            break;
                        case LabelPositions.BOTTOMRIGHT:
                            LabelX = img.Width;
                            LabelY = img.Height - (font.Height);
                            f.Alignment = StringAlignment.Far;
                            break;
                        case LabelPositions.TOPCENTER:
                            LabelX = img.Width / 2;
                            LabelY = 0;
                            f.Alignment = StringAlignment.Center;
                            break;
                        case LabelPositions.TOPLEFT:
                            LabelX = img.Width;
                            LabelY = 0;
                            f.Alignment = StringAlignment.Near;
                            break;
                        case LabelPositions.TOPRIGHT:
                            LabelX = img.Width;
                            LabelY = 0;
                            f.Alignment = StringAlignment.Far;
                            break;
                    }//switch

                    //color a background color box at the bottom of the barcode to hold the string of data
                    using (SolidBrush backBrush = new SolidBrush(Barcode.BackColor))
                    {
                        g.FillRectangle(backBrush, new RectangleF((float)0, (float)LabelY, (float)img.Width, (float)font.Height));
                    }

                    //draw datastring under the barcode image
                    using (SolidBrush foreBrush = new SolidBrush(Barcode.ForeColor))
                    {
                        g.DrawString(Barcode.AlternateLabel == null ? Barcode.RawData : Barcode.AlternateLabel, font, foreBrush, new RectangleF((float)0, (float)LabelY, (float)img.Width, (float)font.Height), f);
                    }

                    g.Save();
                }//using
                return img;
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
            }//catch
        }//Label_Generic


        /// <summary>
        /// Draws Label for EAN-13 barcodes
        /// </summary>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static Image Label_EAN13(Barcode Barcode, Bitmap img)
        {
            try
            {
                int iBarWidth = Barcode.Width / Barcode.EncodedValue.Length;
                string defTxt = Barcode.RawData;

                using (Font labFont = new Font("Arial", getFontsize(Barcode, Barcode.Width - Barcode.Width % Barcode.EncodedValue.Length, img.Height, defTxt) * Barcode.DotsPerPointAt96Dpi, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    int shiftAdjustment;
                    switch (Barcode.Alignment)
                    {
                        case AlignmentPositions.LEFT:
                            shiftAdjustment = 0;
                            break;
                        case AlignmentPositions.RIGHT:
                            shiftAdjustment = (Barcode.Width % Barcode.EncodedValue.Length);
                            break;
                        case AlignmentPositions.CENTER:
                        default:
                            shiftAdjustment = (Barcode.Width % Barcode.EncodedValue.Length) / 2;
                            break;
                    }//switch

                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(img, (float)0, (float)0);

                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                        StringFormat f = new StringFormat
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Near
                        };
                        int LabelY = 0;

                        //Default alignment for EAN13
                        LabelY = img.Height - labFont.Height;
                        f.Alignment = StringAlignment.Near;

                        float w1 = iBarWidth * 4; //Width of first block
                        float w2 = iBarWidth * 42; //Width of second block
                        float w3 = iBarWidth * 42; //Width of third block

                        float s1 = shiftAdjustment - iBarWidth;
                        float s2 = s1 + (iBarWidth * 4); //Start position of block 2
                        float s3 = s2 + w2 + (iBarWidth * 5); //Start position of block 3

                        //Draw the background rectangles for each block
                        using (SolidBrush backBrush = new SolidBrush(Barcode.BackColor))
                        {
                            g.FillRectangle(backBrush, new RectangleF(s2, (float)LabelY, w2, (float)labFont.Height));
                            g.FillRectangle(backBrush, new RectangleF(s3, (float)LabelY, w3, (float)labFont.Height));

                        }

                        //draw datastring under the barcode image
                        using (SolidBrush foreBrush = new SolidBrush(Barcode.ForeColor))
                        {
                            using (Font smallFont = new Font(labFont.FontFamily, labFont.SizeInPoints * 0.5f * Barcode.DotsPerPointAt96Dpi, labFont.Style, GraphicsUnit.Pixel))
                            {
                                g.DrawString(defTxt.Substring(0, 1), smallFont, foreBrush, new RectangleF(s1, (float)img.Height - (float)(smallFont.Height * 0.9), (float)img.Width, (float)labFont.Height), f);
                            }
                            g.DrawString(defTxt.Substring(1, 6), labFont, foreBrush, new RectangleF(s2, (float)LabelY, (float)img.Width, (float)labFont.Height), f);
                            g.DrawString(defTxt.Substring(7), labFont, foreBrush, new RectangleF(s3 - iBarWidth, (float)LabelY, (float)img.Width, (float)labFont.Height), f);
                        }

                        g.Save();
                    }
                }//using
                return img;
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_EAN13-1: " + ex.Message);
            }//catch
        }//Label_EAN13

        /// <summary>
        /// Draws Label for UPC-A barcodes
        /// </summary>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static Image Label_UPCA(Barcode Barcode, Bitmap img)
        {
            try
            {
                int iBarWidth = (int)(Barcode.Width / Barcode.EncodedValue.Length);
                int halfBarWidth = (int)(iBarWidth * 0.5);
                string defTxt = Barcode.RawData;

                using (Font labFont = new Font("Arial", getFontsize(Barcode, (int)((Barcode.Width - Barcode.Width % Barcode.EncodedValue.Length) * 0.9f), img.Height, defTxt) * Barcode.DotsPerPointAt96Dpi, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    int shiftAdjustment;
                    switch (Barcode.Alignment)
                    {
                        case AlignmentPositions.LEFT:
                            shiftAdjustment = 0;
                            break;
                        case AlignmentPositions.RIGHT:
                            shiftAdjustment = (Barcode.Width % Barcode.EncodedValue.Length);
                            break;
                        case AlignmentPositions.CENTER:
                        default:
                            shiftAdjustment = (Barcode.Width % Barcode.EncodedValue.Length) / 2;
                            break;
                    }//switch

                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(img, (float)0, (float)0);

                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                        StringFormat f = new StringFormat();
                        f.Alignment = StringAlignment.Near;
                        f.LineAlignment = StringAlignment.Near;
                        int LabelY = 0;

                        //Default alignment for UPCA
                        LabelY = img.Height - labFont.Height;
                        f.Alignment = StringAlignment.Near;

                        float w1 = iBarWidth * 4; //Width of first block
                        float w2 = iBarWidth * 34; //Width of second block
                        float w3 = iBarWidth * 34; //Width of third block

                        float s1 = shiftAdjustment - iBarWidth;
                        float s2 = s1 + (iBarWidth * 12); //Start position of block 2
                        float s3 = s2 + w2 + (iBarWidth * 5); //Start position of block 3
                        float s4 = s3 + w3 + (iBarWidth * 8) - halfBarWidth;

                        //Draw the background rectangles for each block
                        using (SolidBrush backBrush = new SolidBrush(Barcode.BackColor))
                        {
                            g.FillRectangle(backBrush, new RectangleF(s2, (float)LabelY, w2, (float)labFont.Height));
                            g.FillRectangle(backBrush, new RectangleF(s3, (float)LabelY, w3, (float)labFont.Height));
                        }

                        //draw data string under the barcode image
                        using (SolidBrush foreBrush = new SolidBrush(Barcode.ForeColor))
                        {
                            using (Font smallFont = new Font(labFont.FontFamily, labFont.SizeInPoints * 0.5f * Barcode.DotsPerPointAt96Dpi, labFont.Style, GraphicsUnit.Pixel))
                            {
                                g.DrawString(defTxt.Substring(0, 1), smallFont, foreBrush, new RectangleF(s1, (float)img.Height - smallFont.Height, (float)img.Width, (float)labFont.Height), f);
                                g.DrawString(defTxt.Substring(1, 5), labFont, foreBrush, new RectangleF(s2 - iBarWidth, (float)LabelY, (float)img.Width, (float)labFont.Height), f);
                                g.DrawString(defTxt.Substring(6, 5), labFont, foreBrush, new RectangleF(s3 - iBarWidth, (float)LabelY, (float)img.Width, (float)labFont.Height), f);
                                g.DrawString(defTxt.Substring(11), smallFont, foreBrush, new RectangleF(s4, (float)img.Height - smallFont.Height, (float)img.Width, (float)labFont.Height), f);
                            }
                        }

                        g.Save();
                    }
                }//using
                return img;
            }//try
            catch (Exception ex)
            {
                throw new Exception("ELABEL_UPCA-1: " + ex.Message);
            }//catch
        }//Label_UPCA

        public static int getFontsize(Barcode barcode, int wid, int hgt, string lbl)
        {
            //Returns the optimal font size for the specified dimensions
            int fontSize = 10;

            if (lbl.Length > 0)
            {
                Image fakeImage = barcode.CreateBitmap(1, 1); //As we cannot use CreateGraphics() in a class library, so the fake image is used to load the Graphics.

                // Make a Graphics object to measure the text.
                using (Graphics gr = Graphics.FromImage(fakeImage))
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        using (Font test_font = new Font("Arial", i * Barcode.DotsPerPointAt96Dpi, FontStyle.Regular, GraphicsUnit.Pixel))
                        {
                            // See how much space the text would
                            // need, specifying a maximum width.
                            SizeF text_size = gr.MeasureString(lbl, test_font);
                            if ((text_size.Width > wid) || (text_size.Height > hgt))
                            {
                                fontSize = i - 1;
                                break;
                            }
                        }
                    }
                }


            };

            return fontSize;
        }
    }
}
