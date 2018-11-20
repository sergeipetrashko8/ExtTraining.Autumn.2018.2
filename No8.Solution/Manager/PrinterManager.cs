using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using No8.Solution.Log;
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
        ///     Event that happens at start and at end of printing
        /// </summary>
        public event PrinterDelegate Printed = delegate { };

        /// <summary>
        ///     Event that happens to log print information
        /// </summary>
        public event EventHandler<PrintedEventArgs> Logged = delegate { };

        private readonly IPrintersRepository _repository;

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

            OnPrinted($"Print started on {currentPrinter.Brand} {currentPrinter.Model}");

            var text = currentPrinter.Print(fileName);

            OnLogged(new PrintedEventArgs()
            {
                BrandOfPrinter = currentPrinter.Brand,
                ModelOfPrinter = currentPrinter.Model,
                FileName = fileName,
                TimeOfPrinting = DateTime.Now
            });

            OnPrinted($"Print finished on {currentPrinter.Brand} {currentPrinter.Model}");

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
        ///     Invoked on print done
        /// </summary>
        ///<param name="e"><see cref="PrintedEventArgs"/> object</param>
        protected virtual void OnLogged(PrintedEventArgs e)
        {
            Logged(this, e);
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