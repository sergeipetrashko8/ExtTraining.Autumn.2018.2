using System;
using No8.Solution.Printers;

namespace No8.Solution
{
    /// <summary>
    ///     Factory class of <see cref="Printer"/> objects
    /// </summary>
    public static class PrintersFactory
    {
        /// <summary>
        ///     Returns new <see cref="Printer"/> object
        /// </summary>
        /// <param name="brand">Brand of <see cref="Printer"/></param>
        /// <param name="model">Model of <see cref="Printer"/></param>
        /// <returns><see cref="Printer"/> object if brand of <see cref="Printer"/> is valid else null</returns>
        public static Printer CreatePrinterOrDefault(Type brand, string model)
        {
            if (!typeof(Printer).IsAssignableFrom(brand)) return null;

            return (Printer)brand.GetConstructor(new[] { typeof(string) })?.Invoke(new object[] { model });
        }
    }
}