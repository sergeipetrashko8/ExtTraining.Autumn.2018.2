using System.Collections;
using System.Collections.Generic;
using No8.Solution.Printers;

namespace No8.Solution.Repository
{
    /// <summary>
    ///     Class of printer repository
    /// </summary>
    public class PrintersRepository : IPrintersRepository
    {
        private readonly List<Printer> _printers;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public PrintersRepository()
        {
            _printers = new List<Printer>();
        }

        /// <summary>
        ///     Adds printer to repository
        /// </summary>
        /// <param name="newPrinter">Printer to add</param>
        /// <returns>True if printer added successfully else false</returns>
        public bool Add(Printer newPrinter)
        {
            if (Contains(newPrinter))
            {
                _printers.Add(newPrinter);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks if printer is into the repository
        /// </summary>
        /// <param name="printer">Printer to check</param>
        /// <returns>True if printer is into the repository else false</returns>
        public bool Contains(Printer printer)
        {
            return !_printers.Contains(printer);
        }

        /// <summary>
        ///     Returns enumerator of repository
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Printer> GetEnumerator()
        {
            return _printers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}