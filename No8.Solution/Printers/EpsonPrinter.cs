using System;
using System.IO;

namespace No8.Solution.Printers
{
    /// <summary>
    ///     Class of Epson printer
    /// </summary>
    public class EpsonPrinter : Printer
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="model">Model name</param>
        public EpsonPrinter(string model) : base("Epson", model)
        {
        }

        /// <summary>
        ///     Concrete print from some <see cref="Stream"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to read for print</param>
        protected override void ConcretePrint(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            var encoding = streamReader.CurrentEncoding;
            var text = streamReader.ReadToEnd();
            var bytes = encoding.GetBytes(text);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"-".PadRight(50, '-')}");
            Console.WriteLine(string.Join(" ", bytes));
            Console.WriteLine($"{"-".PadRight(50, '-')}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}