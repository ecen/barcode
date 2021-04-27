using NUnit.Framework;

namespace barcode
{
    [TestFixture]
    class TestClass
    {
        [Test]
        public void MyFunction()
        {
            Assert.That(Program.MyFunction(), Is.EqualTo(true));
        }
    }
}
