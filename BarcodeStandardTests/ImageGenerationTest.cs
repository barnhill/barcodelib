using BarcodeLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarcodeStandardTests
{
    [TestClass]
    public class ImageGenerationTest
    {
        [TestMethod]
        public void CanUseGeneratedImage()
        {
            var image = Barcode.DoEncode(TYPE.Interleaved2of5, "0123456789");
            var width = image.Width;
        }
    }
}