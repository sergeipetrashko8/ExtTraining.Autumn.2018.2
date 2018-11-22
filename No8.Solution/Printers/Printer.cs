using System;
using System.IO;

namespace No8.Solution.Printers
{
    /// <summary>
    ///     Printer abstract class
    /// </summary>
    public abstract class Printer : IEquatable<Printer>
    {
        #region Events

        /// <summary>
        ///     Event that happens when printing starts
        /// </summary>
        public event EventHandler<string/*PrintedEventArgs*/> StartPrint = delegate { };

        /// <summary>
        ///     Event that happens when printing ends
        /// </summary>
        public event EventHandler<string/*PrintedEventArgs*/> EndPrint = delegate { };

        #endregion

        #region Properties

        /// <summary>
        ///     Brand of printer
        /// </summary>
        public string Brand { get; }

        /// <summary>
        ///     Model of printer
        /// </summary>
        public string Model { get; }

        #endregion

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="brand">Brand of <see cref="Printer"/></param>
        /// <param name="model">Model of <see cref="Printer"/></param>
        protected Printer(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        /// <summary>
        ///     Print information from some <see cref="Stream"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to read for print</param>
        /// <exception cref="ArgumentNullException">Throws whe <see cref="Stream"/> has null reference</exception>
        public void Print(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            OnStartPrint("Printer has started work...");

            ConcretePrint(stream);

            OnEndPrint("Printer has finished work...");
        }

        /// <summary>
        ///     Concrete print from some <see cref="Stream"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to read for print</param>
        protected abstract void ConcretePrint(Stream stream);

        /// <summary>
        ///     Triggers StartPrint event
        /// </summary>
        /// <param name="e">Some <see cref="String"/> message</param>
        protected virtual void OnStartPrint(string e) => StartPrint(this, e);

        /// <summary>
        ///     Triggers EndPrint event
        /// </summary>
        /// <param name="e">Some <see cref="String"/> message</param>
        protected virtual void OnEndPrint(string e) => EndPrint(this, e);

        #region IEquatable methods

        /// <summary>
        ///     Checks if two printers are equivalent
        /// </summary>
        /// <param name="other">Other printer</param>
        /// <returns>True if they are equivalent else false</returns>
        public bool Equals(Printer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Brand, other.Brand) && string.Equals(Model, other.Model);
        }

        /// <summary>
        ///     Checks if two printers are equivalent
        /// </summary>
        /// <param name="obj">Other printer</param>
        /// <returns>True if they are equivalent else false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Printer) obj);
        }

        /// <summary>
        ///     Returns hashcode of this printer object
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Brand != null ? Brand.GetHashCode() : 0) * 397) ^ (Model != null ? Model.GetHashCode() : 0);
            }
        }

        #endregion

        #region Object methods

        /// <summary>
        ///     Returns string representation of printer object
        /// </summary>
        /// <returns>String representation of printer object</returns>
        public override string ToString()
        {
            return $"{Brand} - {Model}";
        }

        #endregion
    }
}