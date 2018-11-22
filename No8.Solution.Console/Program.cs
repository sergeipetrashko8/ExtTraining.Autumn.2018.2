using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using No8.Solution.Log;
using No8.Solution.Manager;
using No8.Solution.Printers;
using static System.Console;

namespace No8.Solution.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WriteLine("Application started...\n");

            PrinterManager printerManager = PrinterManager.Instance;

            Logger logger = new Logger();

            printerManager.Logged += logger.Log;

            while (true)
            {
                WriteLine("Select your choice:");
                WriteLine("1: Add new printer.");
                WriteLine("2: Print on Epson.");
                WriteLine("3: Print on Canon.");
                WriteLine("4: View all printers.");
                WriteLine("5: Exit.");

                WriteLine();

                Write("Number of your choice: ");

                if (int.TryParse(ReadLine(), out int choice) && Enumerable.Range(1, 5).Contains(choice))
                {
                    switch (choice)
                    {
                        case 1:
                            while (true)
                            {
                                WriteLine("\nPrinter of who brand do you want to add?:");
                                WriteLine("1: Epson.");
                                WriteLine("2: Canon.");
                                WriteLine("3: Cancel.");

                                WriteLine();

                                Write("Number of your choice: ");

                                if (int.TryParse(ReadLine(), out int choice1) && Enumerable.Range(1, 3).Contains(choice1))
                                {
                                    switch (choice1)
                                    {
                                        case 1:
                                            while (true)
                                            {
                                                Write("\nEnter a Epson model name to add: ");
                                                string modelName = ReadLine();

                                                if (printerManager.Add(PrintersFactory.CreatePrinterOrDefault(typeof(EpsonPrinter), modelName)))
                                                {
                                                    WriteLine($"\nPrinter Epson {modelName} was added successfully!\n");

                                                    break;
                                                }

                                                WriteLine($"\nPrinter Epson {modelName} is already exists!");
                                            }

                                            break;
                                        
                                        case 2:
                                            while (true)
                                            {
                                                Write("\nEnter a Canon model name to add: ");
                                                string modelName = ReadLine();

                                                if (printerManager.Add(PrintersFactory.CreatePrinterOrDefault(typeof(CanonPrinter), modelName)))
                                                {
                                                    WriteLine($"\nPrinter Canon {modelName} was added successfully!\n");

                                                    break;
                                                }

                                                WriteLine($"\nPrinter Canon {modelName} is already exists!");
                                            }

                                            break;

                                        case 3:
                                            break;
                                    }

                                    break;
                                }

                                WriteLine("\nInvalid choice! Please try again!");
                            }

                            break;

                        case 2:
                            {
                                WriteLine("\nAll models of Epson:");

                                IEnumerable<Printer> epsonPrinters = printerManager.GetPrintersByBrand(typeof(EpsonPrinter));

                                if (epsonPrinters.Count() == 0)
                                {
                                    WriteLine("There aren't Epson printers!!!\n");
                                    break;
                                }

                                foreach (var printer in epsonPrinters)
                                {
                                    WriteLine(printer);
                                }

                                Write("Chose model: ");

                                string model = ReadLine();

                                if (epsonPrinters.Any(printer => printer.Model == model))
                                {
                                    var openFileDlg = new OpenFileDialog { Multiselect = false, CheckFileExists = true };

                                    while(true)
                                    {
                                        openFileDlg.ShowDialog();
                                        if (!string.IsNullOrWhiteSpace(openFileDlg.FileName)) break;
                                        WriteLine("\nInvalid file name!!! Try again!\n");
                                    }

                                    if (File.Exists(openFileDlg.FileName)) printerManager.Print(printerManager.TakePrinter(typeof(EpsonPrinter), model), openFileDlg.FileName); // TODO
                                    ForegroundColor = ConsoleColor.White;
                                }

                                else
                                {
                                    WriteLine("\nNo printers of this model!\n");
                                }

                            }
                            break;

                            case 3:
                            {
                                WriteLine("\nAll models of Canon:");

                                IEnumerable<Printer> canonPrinters = printerManager.GetPrintersByBrand(typeof(CanonPrinter));

                                if (canonPrinters.Count() == 0)
                                {
                                    WriteLine("There aren't Canon printers!!!\n");
                                    break;
                                }

                                foreach (var printer in canonPrinters)
                                {
                                    WriteLine(printer);
                                }

                                Write("Chose model: ");

                                string model = ReadLine();

                                if (canonPrinters.Any(printer => printer.Model == model))
                                {
                                    var openFileDlg = new OpenFileDialog { Multiselect = false, CheckFileExists = true };

                                    while (true)
                                    {
                                        openFileDlg.ShowDialog();
                                        if (!string.IsNullOrWhiteSpace(openFileDlg.FileName)) break;
                                        WriteLine("\nInvalid file name!!! Try again!\n");
                                    }

                                    if (File.Exists(openFileDlg.FileName)) printerManager.Print(printerManager.TakePrinter(typeof(CanonPrinter), model), openFileDlg.FileName); // TODO
                                    ForegroundColor = ConsoleColor.White;
                                }

                                else
                                {
                                    WriteLine("\nNo printers of this model!\n");
                                }

                            }
                            break;

                        case 4:
                            Write("\nAll available printers:\n");
                            WriteLine(string.Join("\n", printerManager.GetPrintersByBrand(typeof(EpsonPrinter))));
                            WriteLine(string.Join("\n", printerManager.GetPrintersByBrand(typeof(CanonPrinter))));
                            WriteLine("\n");
                            break;

                        case 5:
                            Write("\nApplication closed. Press any key to exit...");
                            Read();
                            return;
                    }
                }
                else
                {
                    WriteLine("\nInvalid choice! Please try again!\n");
                }
            }
        }
    }
}