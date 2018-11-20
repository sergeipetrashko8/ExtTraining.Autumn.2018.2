using System.Collections.Generic;
using No8.Solution.Printers;

namespace No8.Solution.Repository
{
    /// <summary>
    ///     Interface of printers repository
    /// </summary>
    public interface IPrintersRepository : IEnumerable<Printer>
    {
        /// <summary>
        ///     Adds printer to repository
        /// </summary>
        /// <param name="newPrinter">Printer to add</param>
        /// <returns>True if printer added successfully else false</returns>
        bool Add(Printer newPrinter);

        /// <summary>
        ///     Checks if printer is into the repository
        /// </summary>
        /// <param name="printer">Printer to check</param>
        /// <returns>True if printer is into the repository else false</returns>
        bool Contains(Printer printer);
    }
}