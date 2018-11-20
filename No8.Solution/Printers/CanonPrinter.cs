using System.IO;
using System.Text;
using System.Windows.Forms;

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
                var builder = new StringBuilder();

                int current;

                while ((current = fileStream.ReadByte()) > -1)
                {
                    builder.Append(current).Append(" ");
                }

                textToPrint = builder.ToString();
            }

            return textToPrint;
        }
    }
}