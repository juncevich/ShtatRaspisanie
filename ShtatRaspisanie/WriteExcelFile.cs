using System;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ShtatRaspisanie
{
    internal class WriteExcelFile
    {
        internal static void WriteShtatnoeRaspisanie(Action<FileStream> parseSpisokPodrazdeleniyFile,
            Action<FileStream> parseSpisokShtatnEdinicaFile)
        {
            if (ParseExcelFile.IsSpisokShtatnEdinicaFileExist && ParseExcelFile.IsSpisokPodrazdeleniyFileExist)
            {
                //Получаем список подразделений из файла.
                var podrazdelenieList = ParseExcelFile.PodrazdelenieList;
                //Получаем список штатных единиц из файла.
                var shtatnEdinicaList = ParseExcelFile.ShtatnEdinicaList;
                //Начинаем перебор в уникальном списке подразделений
                foreach (var uniqParent in podrazdelenieList)
                {
                    //Переменная для подсчета общего количества ставок по подразделениям.
                    var rate = 0;
                    Console.WriteLine("---------------------------------------");
                    //Вывод родителя подразделения.
                    Console.WriteLine(uniqParent.Parent);
                    //Перебираем список со штатными единицами.
                    for (var i = 0; i < shtatnEdinicaList.Count; i++)
                    {
                        //Выбор конкретных записей, родитель у которых объявлен выше.
                        if (uniqParent.Name == shtatnEdinicaList[i].Podr_name)
                        {
                            //Подсчет общего количества ставок по подразделениям.
                            rate = rate + shtatnEdinicaList[i].Rate;
                            //Вывод значений.
                            Console.WriteLine(shtatnEdinicaList[i].NameOfShtatnajaEdinica + " " +
                                              shtatnEdinicaList[i].Podr_name + " " + shtatnEdinicaList[i].Rate);
                        }
                    }
                    //Вывод общего количества ставок по подразделениям.
                    Console.WriteLine(@"Общее количество по участку " + uniqParent.Name + ": " + rate);
                }

                //Начало создания файла.
                var fileName = @"d:\ShtatnoeRaspisanie.xlsx";
                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                var spreadsheetDocument = SpreadsheetDocument.
                    Create(fileName, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                var workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                var sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                var sheet = new Sheet
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Штатное расписание"
                };
                var sheetData = new SheetData();

                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }
            else
            {
                MessageBox.Show("Выберите оба файла.");
            }
        }
    }
}