using No8.Solution.Printers;
using NUnit.Framework;

namespace No8.Solution.Tests
{
    [TestFixture]
    public class PrintersFactoryTests
    {
        [Test]
        public void CreatePrinterOrDefault_GiveValidTypeOfPrinter_ExpectedPrinterObject()
        {
            var brand1 = typeof(CanonPrinter);
            var model1 = "131443";

            var brand2 = typeof(EpsonPrinter);
            var model2 = "63454";

            var expected1 = new CanonPrinter(model1);
            var actual1 = PrintersFactory.CreatePrinterOrDefault(brand1, model1);

            var expected2 = new EpsonPrinter(model2);
            var actual2 = PrintersFactory.CreatePrinterOrDefault(brand2, model2);

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [Test]
        public void CreatePrinterOrDefault_GiveInvalidTypeOfPrinter_ExpectedNull()
        {
            var brand1 = typeof(int);
            var model1 = "131443";

            var brand2 = typeof(string);
            var model2 = "63454";

            var actual1 = PrintersFactory.CreatePrinterOrDefault(brand1, model1);

            var actual2 = PrintersFactory.CreatePrinterOrDefault(brand2, model2);

            Assert.AreEqual(null, actual1);
            Assert.AreEqual(null, actual2);
        }
    }
}