using System.IO;
using System.Text;

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
        ///     Performs printing of text from some file 
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Text from file</returns>
        /// <exception cref="FileNotFoundException">Throws if file with this name doesn't exist</exception>
        public override string Print(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);

            string textToPrint;

            using (var fileStream = File.OpenRead(fileName))
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);

                textToPrint = Encoding.Unicode.GetString(bytes);
            }

            return textToPrint;
        }
    }
}