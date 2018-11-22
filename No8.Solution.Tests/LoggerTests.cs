using System;
using System.IO;
using No8.Solution.Log;
using NUnit.Framework;

namespace No8.Solution.Tests
{
    [TestFixture]
    public class LoggerTests
    {
        [TestCase(null)]
        [TestCase("test.txt")]
        public void Log_FileToLog_ExpectedEquivalentString(string fileToLog)
        {
            Logger logger = fileToLog == null ? new Logger() : new Logger(fileToLog);

            PrintedEventArgs args = new PrintedEventArgs()
            {
                BrandOfPrinter = "Canon",
                Message = fileToLog,
                ModelOfPrinter = "1532",
                TimeOfEvent = DateTime.Now
            };

            string expected = $"PRINTER [ Brand: {args.BrandOfPrinter,-5} Model: {args.ModelOfPrinter,-5} ] Time of event: {args.TimeOfEvent}\nMessage: {args.Message}".Trim();
            string actual;

            logger.Log(null, args);

            using (var file = File.OpenText(logger.LogFileName))
            {
                actual = file.ReadToEnd().Trim();
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Constructor_TakeNullReferenceToFileName_ExpectedArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Logger(null));
        }
    }
}