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

            PrinterManager printerManager = new PrinterManager();

            Logger logger = new Logger();

            printerManager.Printed += logger.Log;

            while (true)
            {
                WriteLine("Select your choice:");
                WriteLine("1: Add new printer.");
                WriteLine("2: Print on Epson.");
                WriteLine("3: Print on Canon.");
                WriteLine("4: Exit.");

                WriteLine();

                Write("Number of your choice: ");

                if (int.TryParse(ReadLine(), out int choice) && Enumerable.Range(1, 4).Contains(choice))
                {
                    switch (choice)
                    {
                        case 1:
                        {

                            while (true)
                            {
                                WriteLine("\nChose a brand:");
                                WriteLine("1: Epson.");
                                WriteLine("2: Canon.");
                                WriteLine("3: Cancel.");

                                WriteLine();

                                Write("Number of your choice: ");

                                    if (int.TryParse(ReadLine(), out int choice1) &&
                                    Enumerable.Range(1, 3).Contains(choice1))
                                {
                                    switch (choice1)
                                    {
                                        case 1:
                                        {

                                            while (true)
                                            {
                                                Write("\nEnter a Epson model name: ");
                                                string modelName = ReadLine();

                                                if (printerManager.Add(typeof(EpsonPrinter), modelName))
                                                {
                                                    WriteLine($"Printer Epson {modelName} added successfully!\n");

                                                    break;
                                                }

                                                WriteLine($"Printer Epson {modelName} is already exists!\n");
                                                }
                                                break;
                                        }

                                        case 2:
                                            while (true)
                                            {
                                                Write("\nEnter a Canon model name: ");
                                                string modelName = ReadLine();

                                                if (printerManager.Add(typeof(CanonPrinter), modelName))
                                                {
                                                    WriteLine($"Printer Canon {modelName} added successfully!\n");
                                                    break;
                                                }

                                                WriteLine($"Printer Epson {modelName} is already exists!\n");
                                            }
                                                break;

                                        case 3:
                                            break;
                                    }

                                    break;
                                }
                            }
                            break;
                        }
                        case 2:
                        {
                            WriteLine("\nAll models of Epson:");

                            IEnumerable<Printer> epsonPrinters =
                                printerManager.Where(printer => printer.Brand == "Epson");

                            foreach (var printer in epsonPrinters)
                            {
                                WriteLine(printer);
                            }

                            Write("Chose model: ");

                            string model = ReadLine();

                            if (epsonPrinters.Any(printer => printer.Model == model))
                            {
                                var openFileDlg = new OpenFileDialog();
                                openFileDlg.Multiselect = false;
                                openFileDlg.CheckFileExists = true;
                                openFileDlg.ShowDialog();

                                if (File.Exists(openFileDlg.FileName)) WriteLine(printerManager.Print(typeof(EpsonPrinter), model, openFileDlg.FileName));
                            }

                            else
                            {
                                WriteLine("No printers of this model!\n");
                            }

                            }
                            break;
                        case 3:
                        {
                            WriteLine("\nAll models of Canon:");

                            IEnumerable<Printer> canonPrinters =
                                printerManager.Where(printer => printer.Brand.Equals("Canon"));

                            foreach (var printer in canonPrinters)
                            {
                                WriteLine(printer);
                            }

                            Write("Chose model: ");

                            string model = ReadLine();

                            if (canonPrinters.Any(printer => printer.Model == model))
                            {
                                var openFileDlg = new OpenFileDialog {Multiselect = false, CheckFileExists = true};
                                openFileDlg.ShowDialog();

                                if (File.Exists(openFileDlg.FileName)) WriteLine(printerManager.Print(typeof(CanonPrinter), model, openFileDlg.FileName));
                               
                            }

                            else
                            {
                                WriteLine("No printers of this model!\n");
                            }
                        }
                            break;
                        case 4:
                            WriteLine("Application closed. Press any key to exit...");
                            Read();
                            return;
                    }
                }
            }
        }
    }
}

/*

возможность добавления нового принтера в систему с указанием его имени и модели, которые должны быть уникальными;

после добавления принтер становится доступным в списке принтеров для печати;

возможность масштабируемости и легкости сопровождения системы.
*/
