using System.IO;
using System.Linq;
using No8.Solution.Printers;
using NUnit.Framework;

namespace No8.Solution.Tests
{
    [TestFixture]
    public class PrinterTests
    {
        [Test]
        public void Equals_EqualsAndNotEqualsPrinters_ExpectedTrueIfEqualsElseFalse()
        {
            var printerCanon1 = new CanonPrinter("25");
            var printerCanon2 = new CanonPrinter("25");
            var printerCanon3 = new CanonPrinter("452");

            var printerEpson1 = new EpsonPrinter("345");
            var printerEpson2 = new EpsonPrinter("345");
            var printerEpson3 = new EpsonPrinter("123");

            Assert.AreEqual(printerCanon1, printerCanon2);
            Assert.AreNotEqual(printerCanon2, printerCanon3);

            Assert.AreEqual(printerEpson1, printerEpson2);
            Assert.AreNotEqual(printerEpson2, printerEpson3);
        }
    }
}