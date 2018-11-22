using System.Linq;
using No8.Solution.Manager;
using No8.Solution.Printers;
using NUnit.Framework;

namespace No8.Solution.Tests
{
    [TestFixture]
    public class PrinterManagerTests
    {
        [Test]
        public void Singleton_CompareInstances_ExpectedTrue()
        {
            PrinterManager printerManager1 = PrinterManager.Instance;
            PrinterManager printerManager2 = PrinterManager.Instance;

            Assert.AreEqual(printerManager1, printerManager2);
        }

        [Test]
        public void Add_AddedSomeDistinctPrintersGetPrintersByBrandAndCompareWithSourcePrintersFromArrayWithTheSameBrand_ExpectedTrue()
        {
            PrinterManager printerManager = PrinterManager.Instance;
            printerManager.Clear();

            Printer[] printers = { new CanonPrinter("3412"), new EpsonPrinter("6487") , new CanonPrinter("3235"), new EpsonPrinter("6463") };

            foreach (var printer in printers)
            {
                printerManager.Add(printer);
            }

            var canonPrinters = printerManager.GetPrintersByBrand(typeof(CanonPrinter));
            var epsonPrinters = printerManager.GetPrintersByBrand(typeof(EpsonPrinter));

            Assert.True(canonPrinters.SequenceEqual(printers.Where(printer => printer.GetType() == typeof(CanonPrinter))));
            Assert.True(epsonPrinters.SequenceEqual(printers.Where(printer => printer.GetType() == typeof(EpsonPrinter))));
        }

        [Test]
        public void Add_AddedPrinterThatNotInManager_ExpectedTrue()
        {
            PrinterManager printerManager = PrinterManager.Instance;
            printerManager.Clear();

            var printer = new CanonPrinter("214");

            printerManager.Add(printer);

            var actual = printerManager.Add(new CanonPrinter("52323"));
            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Add_AddedPrinterThatAlreadyInManager_ExpectedFalse()
        {
            PrinterManager printerManager = PrinterManager.Instance;
            printerManager.Clear();

            var printer = new CanonPrinter("214");

            printerManager.Add(printer);

            var actual = printerManager.Add(printer);
            var expected = false;

            Assert.AreEqual(expected, actual);
        }
    }
}