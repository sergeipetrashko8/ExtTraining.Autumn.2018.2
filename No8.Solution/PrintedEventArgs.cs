using System;

namespace No8.Solution
{
    public class PrintedEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public string ModelOfPrinter { get; set; }
        public string BrandOfPrinter { get; set; }
        public DateTime TimeOfPrinting { get; set; }
    }
}