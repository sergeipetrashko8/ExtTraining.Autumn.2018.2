using System;

namespace No8.Solution.Printers
{
    /// <summary>
    ///     Printer abstract class
    /// </summary>
    public abstract class Printer : IEquatable<Printer>
    {
        /// <summary>
        ///     Brand of printer
        /// </summary>
        public string Brand { get; }

        /// <summary>
        ///     Model of printer
        /// </summary>
        public string Model { get; }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        protected Printer(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        /// <summary>
        ///     Performs printing of text from some file 
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Text from file</returns>
        public abstract string Print(string fileName);

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
            if (obj.GetType() != GetType()) return false;
            return Equals((Printer) obj);
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

        /// <summary>
        ///     Returns string representation of printer object
        /// </summary>
        /// <returns>String representation of printer object</returns>
        public override string ToString()
        {
            return $"{Brand} - {Model}";
        }
    }
}