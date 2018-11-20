using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using No8.Solution.Printers;
using No8.Solution.Repository;

namespace No8.Solution.Manager
{
    /// <summary>
    ///     Class of printer manager
    /// </summary>
    public class PrinterManager : IPrinterManager
    {
        /// <summary>
        ///     Event that happens during printing
        /// </summary>
        public event PrinterDelegate Printed = delegate { };

        private readonly PrintersRepository _repository;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public PrinterManager()
        {
            _repository = new PrintersRepository();
        }

        /// <summary>
        ///     Adds printers to repository
        /// </summary>
        /// <param name="brand">Brand of printer</param>
        /// <param name="model">Model of printer</param>
        /// <returns>True is added successfully else false</returns>
        public bool Add(Type brand, string model)
        {
            if (typeof(Printer).IsAssignableFrom(brand))
            {
                var newPrinter = (Printer)brand.GetConstructor(new[] { typeof(string) })?.Invoke(new object[] { model });

                return _repository.Add(newPrinter);
            }

            return false;
        }

        /// <summary>
        ///     Producing printing using a printer
        /// </summary>
        /// <param name="brand">Brand of printer</param>
        /// <param name="model">Model of printer</param>
        /// <param name="fileName">File to print</param>
        /// <returns>Text from file</returns>
        /// <exception cref="InvalidOperationException">Throws if printer doesn't exist in repository</exception>
        public string Print(Type brand, string model, string fileName)
        {
            var currentPrinter = _repository.SingleOrDefault(printer => printer.GetType() == brand && printer.Model == model);

            if (currentPrinter == null) throw new InvalidOperationException($"Printer {brand} {model} doesn't exist in repository");

            OnPrinted("Print started...");

            var text = currentPrinter.Print(fileName);

            OnPrinted($"{currentPrinter} \n\rPrinted text: {text}");

            OnPrinted("Print finished...");

            return text;
        }

        /// <summary>
        ///     Invoked during printing
        /// </summary>
        /// <param name="message">Message for subscribers</param>
        protected virtual void OnPrinted(string message)
        {
            Printed(message);
        }
        
        /// <summary>
        ///     Returns enumerator of repository
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Printer> GetEnumerator()
        {
            return _repository.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}