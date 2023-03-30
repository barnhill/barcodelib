using SkiaSharp;
using System;
using System.Drawing;

namespace BarcodeLib
{
    class Labels
    {
        /// <summary>
        /// Draws Label for ITF-14 barcodes
        /// </summary>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_ITF14(Barcode Barcode, SKBitmap img)
        {
            try
            {
                SKFont font = Barcode.LabelFont;
                float fontHeight;
                using (var canvas = new SKCanvas(img))
                {
                    //color a box at the bottom of the barcode to hold the string of data
                    using(var paint = new SKPaint(font)) 
                    {
                        paint.FilterQuality = SKFilterQuality.High;
                        paint.IsAntialias = true;
                        paint.ColorF = Barcode.BackColor;
                        paint.Style = SKPaintStyle.Fill;

                        fontHeight = paint.MeasureText("A");

                        var rect = SKRect.Create(0, img.Height - (fontHeight - 2), img.Width, fontHeight);
                        canvas.DrawRect(rect, paint);
                    }
                    
                    //draw datastring under the barcode image
                    using (var foreBrush = new SKPaint(font))
                    {
                        foreBrush.FilterQuality = SKFilterQuality.High;
                        foreBrush.IsAntialias = true;
                        foreBrush.ColorF = Barcode.ForeColor;
                        foreBrush.TextAlign = SKTextAlign.Center;

                        String str = Barcode.AlternateLabel == null ? Barcode.RawData : Barcode.AlternateLabel;
                        SKPoint point = new SKPoint((float)(img.Width / 2), img.Height - fontHeight + 1);
                        canvas.DrawText(str, point, foreBrush);
                    }

                    using (var pen = new SKPaint())
                    {
                        pen.FilterQuality = SKFilterQuality.High;
                        pen.IsAntialias = true;
                        pen.ColorF = Barcode.ForeColor;
                        pen.StrokeWidth = (float)img.Height / 16;

                        canvas.DrawLine(new SKPoint(0, img.Height - (int)fontHeight - 2), new SKPoint(img.Width, img.Height - (int)fontHeight - 2), pen);//bottom
                    }

                    canvas.Save();
                }//using

                return SKImage.FromBitmap(img);
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
        /*public static SKImage Label_Generic(Barcode Barcode, SKBitmap img)
        {
            try
            {
                SKFont font = Barcode.LabelFont;

                using (SKCanvas g = new SKCanvas(img))
                {
                    g.DrawImage(SKImage.FromBitmap(img), 0f, 0f);

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
        }//Label_Generic*/


        /// <summary>
        /// Draws Label for EAN-13 barcodes
        /// </summary>
        /// <param name="img">Image representation of the barcode without the labels</param>
        /// <returns>Image representation of the barcode with labels applied</returns>
        public static SKImage Label_EAN13(Barcode Barcode, SKBitmap img)
        {
            try
            {
                int iBarWidth = Barcode.Width / Barcode.EncodedValue.Length;
                string defTxt = Barcode.RawData;

                using (SKFont labFont = new SKFont(SKTypeface.Default, getFontsize(Barcode, Barcode.Width - Barcode.Width % Barcode.EncodedValue.Length, img.Height, defTxt) * Barcode.DotsPerPointAt96Dpi))
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

                    using (SKCanvas g = new SKCanvas(img))
                    {
                        float labFontHeight;
                        using (SKPaint fontBrush = new SKPaint(labFont))
                        {
                            labFontHeight = fontBrush.MeasureText("A");
                        }
                        
                        int LabelY = 0;

                        //Default alignment for EAN13
                        LabelY = img.Height - (int)labFontHeight;

                        float w1 = iBarWidth * 4; //Width of first block
                        float w2 = iBarWidth * 42; //Width of second block
                        float w3 = iBarWidth * 42; //Width of third block

                        float s1 = shiftAdjustment - iBarWidth;
                        float s2 = s1 + (iBarWidth * 4); //Start position of block 2
                        float s3 = s2 + w2 + (iBarWidth * 5); //Start position of block 3

                        //Draw the background rectangles for each block
                        using (SKPaint backBrush = new SKPaint(labFont))
                        {
                            backBrush.FilterQuality = SKFilterQuality.High;
                            backBrush.ColorF = Barcode.BackColor;
                            g.DrawRect(new SKRect(s2, (float)LabelY, w2, labFontHeight), backBrush);

                        }

                        //draw datastring under the barcode image
                        using (SKPaint foreBrush = new SKPaint())
                        {
                            foreBrush.FilterQuality = SKFilterQuality.High;
                            foreBrush.ColorF = Barcode.ForeColor;
                            foreBrush.TextAlign = SKTextAlign.Left;
                            
                            using (SKFont smallFont = new SKFont(labFont.Typeface, labFont.Size * 0.5f * Barcode.DotsPerPointAt96Dpi))
                            {
                                using (SKPaint foreBrushSmall = new SKPaint(smallFont))
                                {
                                    foreBrushSmall.FilterQuality = SKFilterQuality.High;
                                    foreBrushSmall.ColorF = Barcode.ForeColor;
                                    foreBrushSmall.TextAlign = SKTextAlign.Left;
                                    var fontHeight = foreBrush.MeasureText("A");
                                    g.DrawText(defTxt.Substring(0, 1), s1, (float)img.Width, foreBrushSmall);
                                }
                            }
                            
                            g.DrawText(defTxt.Substring(1, 6), s2, (float)LabelY, foreBrush);
                            g.DrawText(defTxt.Substring(7), s3 - iBarWidth, (float)LabelY, foreBrush);
                        }

                        g.Save();
                    }
                }//using

                return SKImage.FromBitmap(img);
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
        public static SKImage Label_UPCA(Barcode Barcode, SKBitmap img)
        {
            try
            {
                int iBarWidth = (int)(Barcode.Width / Barcode.EncodedValue.Length);
                int halfBarWidth = (int)(iBarWidth * 0.5);
                string defTxt = Barcode.RawData;
                
                using (SKFont labFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), getFontsize(Barcode, (int)((Barcode.Width - Barcode.Width % Barcode.EncodedValue.Length) * 0.9f), img.Height, defTxt) * Barcode.DotsPerPointAt96Dpi))
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

                    using (SKSurface surface = SKSurface.Create(new SKImageInfo().WithSize(img.Width, img.Height)))
                    {
                        surface.Canvas.DrawImage(SKImage.FromBitmap(img), new SKPoint(0, 0));
                        var g = surface.Canvas;
                        g.DrawImage(SKImage.FromBitmap(img), (float)0, (float)0);

                        /*g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;*/

                        /*StringFormat f = new StringFormat();
                        f.Alignment = StringAlignment.Near;
                        f.LineAlignment = StringAlignment.Near;*/
                        int LabelY = 0;

                        var fontMetrics = new SKFontMetrics();
                        labFont.GetFontMetrics(out fontMetrics);

                        //Default alignment for UPCA
                        LabelY = (int)(img.Height - fontMetrics.XHeight);
                        /*f.Alignment = StringAlignment.Near;*/

                        float w1 = iBarWidth * 4; //Width of first block
                        float w2 = iBarWidth * 34; //Width of second block
                        float w3 = iBarWidth * 34; //Width of third block

                        float s1 = shiftAdjustment - iBarWidth;
                        float s2 = s1 + (iBarWidth * 12); //Start position of block 2
                        float s3 = s2 + w2 + (iBarWidth * 5); //Start position of block 3
                        float s4 = s3 + w3 + (iBarWidth * 8) - halfBarWidth;


                        //Draw the background rectangles for each block
                        using (SKPaint backBrush = new SKPaint())
                        {
                            backBrush.ColorF = Barcode.BackColor;

                            g.DrawRect(new SKRect(s2, (float)LabelY, w2, (float)fontMetrics.XHeight), backBrush);
                            g.DrawRect(new SKRect(s3, (float)LabelY, w3, (float)fontMetrics.XHeight), backBrush);
                        }

                        using (SKFont labFontSmall = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), getFontsize(Barcode, (int)((Barcode.Width - Barcode.Width % Barcode.EncodedValue.Length) * 0.9f * 0.5), img.Height, defTxt) * Barcode.DotsPerPointAt96Dpi))
                        {
                            //draw data string under the barcode image
                            using (SKPaint foreBrushSmall = new SKPaint(labFontSmall))
                            {
                                using (SKPaint foreBrushLarge = new SKPaint(labFont))
                                {
                                    foreBrushLarge.ColorF = Barcode.ForeColor;
                                    foreBrushSmall.ColorF = Barcode.ForeColor;

                                    labFontSmall.GetFontMetrics(out fontMetrics);

                                    g.DrawText(defTxt.Substring(0, 1), s1, (float)img.Height - fontMetrics.XHeight, foreBrushSmall);
                                    g.DrawText(defTxt.Substring(1, 5), s2 - iBarWidth, (float)LabelY, foreBrushLarge);
                                    g.DrawText(defTxt.Substring(6, 5), s3 - iBarWidth, (float)LabelY, foreBrushLarge);
                                    g.DrawText(defTxt.Substring(11), s4, (float)img.Height - fontMetrics.XHeight, foreBrushSmall);
                                }
                            }
                        }

                        g.Save();
                        
                        return surface.Snapshot();
                    } 
                }//using
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
                SKImage fakeImage = SKImage.FromBitmap(barcode.CreateBitmap(1, 1)); //As we cannot use CreateGraphics() in a class library, so the fake image is used to load the Graphics.
                var bounds = SKRect.Empty;
                for (int i = 1; i <= 100; i++)
                {
                    using (SKFont test_font = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal), i))
                    {
                        // Make a Graphics object to measure the text.
                        using (var gr = new SKPaint(test_font))
                        {
                            gr.MeasureText(lbl, ref bounds);

                            if ((bounds.Width > wid) || (bounds.Height > hgt))
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
