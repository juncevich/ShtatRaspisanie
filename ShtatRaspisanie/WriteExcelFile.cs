using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ShtatRaspisanie
{
    class WriteExcelFile
    {


        internal static void writeShtatnoeRaspisanie(Action<FileStream> parseSpisokPodrazdeleniyFile, Action<FileStream> parseSpisokShtatnEdinicaFile)
        {

            if (ParseExcelFile.isSpisokShtatnEdinicaFileExist && ParseExcelFile.isSpisokPodrazdeleniyFileExist)
            {

                //Список уникальных подразделений.
                HashSet<Podrazdelenie> podrazdelenieSet = new HashSet<Podrazdelenie>();
                //Получаем список подразделений из файла.
                List<Podrazdelenie> podrazdelenieList = ParseExcelFile.podrazdelenieList;
                //Получаем список штатных единиц из файла.
                List<ShtatnEdinica> shtatnEdinicaList = ParseExcelFile.shtatnEdinicaList;
                //Начинаем перебор в уникальном списке подразделений
                foreach (Podrazdelenie uniqParent in podrazdelenieList)
                {
                    //Переменная для подсчета общего количества ставок по подразделениям.
                    int rate = 0;
                    Console.WriteLine("---------------------------------------");
                    //Вывод родителя подразделения.
                    Console.WriteLine(uniqParent.parent);
                    //Перебираем список со штатными единицами.
                    for (int i = 0; i < shtatnEdinicaList.Count; i++)
                    {
                        //Выбор конкретных записей, родитель у которых объявлен выше.
                        if (uniqParent.name == shtatnEdinicaList[i].Podr_name)
                        {
                            //Подсчет общего количества ставок по подразделениям.
                            rate = rate + shtatnEdinicaList[i].Rate;
                            //Вывод значений.
                            Console.WriteLine(shtatnEdinicaList[i].NameOfShtatnajaEdinica + " " + shtatnEdinicaList[i].Podr_name + " " + shtatnEdinicaList[i].Rate);

                        }

                    }
                    //Вывод общего количества ставок по подразделениям.
                    Console.WriteLine("Общее количество по участку " + uniqParent.name + ": " + rate);
                }

                //Начало создания файла.
                 string fileName = @"d:\ShtatnoeRaspisanie.xlsx";
                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                    Create(fileName, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Штатное расписание"
                    
                };
                SheetData sheetData = new SheetData();
 
                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();

            } else
            {
              
                MessageBox.Show("Выберите оба файла.");
                
            }
        }
    }
}
