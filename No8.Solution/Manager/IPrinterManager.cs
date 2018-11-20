using System;
using System.Collections.Generic;
using No8.Solution.Printers;

namespace No8.Solution.Manager
{
    /// <summary>
    ///     Delegate
    /// </summary>
    /// <param name="arg">Some message</param>
    public delegate void PrinterDelegate(string arg);

    /// <summary>
    ///     Interface of printer manager class
    /// </summary>
    public interface IPrinterManager : IEnumerable<Printer>
    {
        /// <summary>
        ///     Event that happens during printing
        /// </summary>
        event PrinterDelegate Printed;

        /// <summary>
        ///     Adds printers to repository
        /// </summary>
        /// <param name="brand">Brand of printer</param>
        /// <param name="model">Model of printer</param>
        /// <returns>True is added successfully else false</returns>
        bool Add(Type brand, string model);

        /// <summary>
        ///     Producing printing using a printer
        /// </summary>
        /// <param name="brand">Brand of printer</param>
        /// <param name="model">Model of printer</param>
        /// <param name="fileName">File to print</param>
        /// <returns>Text from file</returns>
        string Print(Type brand, string model, string fileName);
    }
}