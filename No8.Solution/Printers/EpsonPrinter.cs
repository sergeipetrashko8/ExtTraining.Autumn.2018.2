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
        ///     Performs printing of text from some file 
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Text from file</returns>
        /// <exception cref="FileNotFoundException">Throws if file with this name doesn't exist</exception>
        public override string Print(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);

            string textToPrint;

            using (var fileStream = new StreamReader(File.OpenRead(fileName)))
            {
                textToPrint = fileStream.ReadToEnd();
            }

            return textToPrint;
        }
    }
}