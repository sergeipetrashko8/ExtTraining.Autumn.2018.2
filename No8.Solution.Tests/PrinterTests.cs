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

        [Test]
        public void Print()
        {
            var printerCanon1 = new CanonPrinter("25");
            var printerEpson1 = new EpsonPrinter("345");

            string fileName1 = "test1.txt";
            string fileName2 = "test2.txt";

            string information = "98374ch345ocw3ih478cy3298u4c8923x9 289";

            using (var fileStream = File.CreateText(fileName1))
            {
                fileStream.WriteLine(information);
            }

            var actual1 = string.Join("", printerCanon1.Print(fileName1).Trim().Split(' ').Select(str => ((char)byte.Parse(str)).ToString())).Trim();

            Assert.AreEqual(actual1, information);

            using (var fileStream = File.CreateText(fileName2))
            {
                fileStream.WriteLine(information);
            }

            var actual2 = printerEpson1.Print(fileName1).Trim();

            Assert.AreEqual(actual2, information);
        }
    }
}