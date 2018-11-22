using System;
using System.IO;

namespace No8.Solution.Printers
{
    /// <summary>
    ///     Class of Canon printer
    /// </summary>
    public class CanonPrinter : Printer
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="model">Model name</param>
        public CanonPrinter(string model) : base("Canon", model)
        {
        }

        /// <summary>
        ///     Concrete print from some <see cref="Stream"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to read for print</param>
        protected override void ConcretePrint(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            var text = streamReader.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"-".PadRight(50, '-')}");
            Console.WriteLine(text);
            Console.WriteLine($"{"-".PadRight(50, '-')}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}