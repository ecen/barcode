using barcode;
using NUnit.Framework;

namespace barcode_test
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToBinaryStringSimple()
        {
            string s = "▍  ▍ ";
            string expected = "10010";
            Assert.AreEqual(BarcodeProgram.ToBinaryString(s), expected);
        }

        [Test]
        public void ToBinaryStringNewline()
        {
            string s = "▍  ▍ \n";
            string expected = "10010";
            Assert.AreEqual(BarcodeProgram.ToBinaryString(s), expected);
        }

        [Test]
        public void ToBinaryStringTestdata()
        {
            string s = "▍ ▍   ▍▍ ▍ ▍▍   ▍  ▍▍  ▍   ▍▍ ▍   ▍▍ ▍   ▍▍ ▍ ▍ ▍ ▍▍▍  ▍ ▍▍  ▍▍ ▍▍ ▍▍  ▍  ▍▍▍ ▍▍  ▍▍ ▍   ▍  ▍ ▍";
            string expected = "10100011010110001001100100011010001101000110101010111001011001101101100100111011001101000100101";
            Assert.AreEqual(BarcodeProgram.ToBinaryString(s), expected);
        }
    }
}