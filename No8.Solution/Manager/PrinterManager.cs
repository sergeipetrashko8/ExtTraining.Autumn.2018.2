using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using No8.Solution.Log;
using No8.Solution.Printers;

namespace No8.Solution.Manager
{
    /// <summary>
    ///     Class of printer manager
    /// </summary>
    public class PrinterManager
    {
        #region Singleton realisation

        private static readonly Lazy<PrinterManager> LazyInstance = new Lazy<PrinterManager>(() => new PrinterManager());

        /// <summary>
        ///     Returns instance of singleton <see cref="PrinterManager"/>
        /// </summary>
        public static PrinterManager Instance => LazyInstance.Value;

        static PrinterManager() { }

        #endregion

        /// <summary>
        ///     Event that happens to log print information
        /// </summary>
        public event EventHandler<PrintedEventArgs> Logged = delegate { };

        private readonly IList<Printer> _printers;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public PrinterManager()
        {
            _printers = new List<Printer>();
        }

        /// <summary>
        ///     Adds printers to <see cref="Printer"/> list
        /// </summary>
        /// <param name="printer"><see cref="Printer"/> to add</param>
        /// <returns>True when <see cref="Printer"/> was added successfully else false</returns>
        /// <exception cref="ArgumentNullException">Throws when <see cref="Printer"/> has null reference</exception>
        public bool Add(Printer printer)
        {
            if (printer == null) throw new ArgumentNullException(nameof(printer));

            if (_printers.Contains(printer)) return false;

            _printers.Add(printer);

            printer.StartPrint += PrinterEventHandler;
            printer.EndPrint += PrinterEventHandler;
            
            return true;
        }

        /// <summary>
        ///     Triggers <see cref="Printer"/> work
        /// </summary>
        /// <param name="printer"><see cref="Printer"/> object</param>
        /// <param name="fileName"><see cref="String"/> file name</param>
        /// <exception cref="ArgumentNullException">Throws when <see cref="Printer"/> or fileName has null reference</exception>
        /// <exception cref="FileNotFoundException">Throws when file with this name is not founded</exception>
        public void Print(Printer printer, string fileName)
        {
            if (printer == null) throw new ArgumentNullException(nameof(printer));
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);

            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                printer.Print(fileStream);
            }
        }

        /// <summary>
        ///     Returns <see cref="Printer"/> object from <see cref="PrinterManager"/>
        /// </summary>
        /// <param name="brand">Brand of printer</param>
        /// <param name="model">Model of printer</param>
        /// <returns><see cref="Printer"/> object if it exists in <see cref="PrinterManager"/> else null</returns>
        public Printer TakePrinter(Type brand, string model)
        {
            return _printers.SingleOrDefault(printer => printer.GetType() == brand && printer.Model == model);
        }

        /// <summary>
        ///     Returns sequence of <see cref="Printer"/> objects by brand of <see cref="Printer"/>
        /// </summary>
        /// <param name="brand">Brand of <see cref="Printer"/></param>
        /// <returns><see cref="IEnumerable{Printer}"/> sequence of <see cref="Printer"/> objects</returns>
        public IEnumerable<Printer> GetPrintersByBrand(Type brand)
        {
            return _printers.Where(printer => printer.GetType() == brand);
        }
        
        /// <summary>
        ///     Handler of printer events
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e"><see cref="String"/> message</param>
        private void PrinterEventHandler(object sender, /*PrintedEventArgs*/string e)
        {
            var printer = sender as Printer;

            OnLogged(new PrintedEventArgs()
            {
                BrandOfPrinter = printer?.Brand,
                ModelOfPrinter = printer?.Model,
                TimeOfEvent = DateTime.Now,
                Message = e
            });
        }

        /// <summary>
        ///     Triggers Logged event
        /// </summary>
        /// <param name="e"><see cref="PrinterEventHandler"/> object</param>
        protected virtual void OnLogged(PrintedEventArgs e)
        {
            Logged(this, e);
        }
    }
}