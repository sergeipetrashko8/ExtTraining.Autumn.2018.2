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
                FileName = fileToLog,
                ModelOfPrinter = "1532",
                TimeOfPrinting = DateTime.Now
            };

            string expected = $"{args.BrandOfPrinter} {args.ModelOfPrinter} {fileToLog} {args.TimeOfPrinting}";
            string actual;

            logger.Log(null, args);

            using (var file = File.OpenText(logger.LogFileName))
            {
                actual = file.ReadToEnd().Trim();
            }

            Assert.AreEqual(expected, actual);
        }
    }
}