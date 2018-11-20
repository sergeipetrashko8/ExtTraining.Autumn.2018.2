using System.Linq;
using No8.Solution.Printers;
using No8.Solution.Repository;
using NUnit.Framework;

namespace No8.Solution.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void Contains_RepositoryWithPrinterAndWithoutPrinter_ExpectedTrueIfContainsElseFalse()
        {
            Assert.True(Data.Repository.Contains(Data.Repository.ElementAt(0)));
            Assert.False(Data.Repository.Contains(new EpsonPrinter("676")));
        }

        [Test]
        public void Add_AddPrinterThatIsInRepositoryAndIsNotInRepository_ExpectedTrueIfIsNotElseFalse()
        {
            Assert.True(Data.Repository.Add(new CanonPrinter("74363")));
            Assert.False(Data.Repository.Add(new CanonPrinter("12")));
        }
    }

    public static class Data
    {
        public static PrintersRepository Repository => new PrintersRepository()
        {
            new CanonPrinter("12"),
            new EpsonPrinter("31"),
            new EpsonPrinter("654"),
            new CanonPrinter("5433"),
            new EpsonPrinter("42311")
        };
    }
}