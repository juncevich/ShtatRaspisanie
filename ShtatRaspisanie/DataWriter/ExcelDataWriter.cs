using System.Collections.Generic;
using ClosedXML.Excel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.DataWriter
{
    public class ExcelDataWriter
    {
        public void WriteData(List<Unit> units)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Штатное расписание");

            var colA = worksheet.Column("A");
            colA.Width = 20;
            var colB = worksheet.Column("B");
            colB.Width = 35;


           
            
            

            var childUnitStyle = workbook.Style;
            childUnitStyle.Font.Bold = true;
            childUnitStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



            worksheet.Cell("A1").Value = "Штатное расписание";
            worksheet.Cell("A1").Style.Font.Bold = true;
            worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            
            int index = 2;
           
            foreach (ParentUnit unit in units)
            {
                if (unit.StaffUnits!=null && unit.IsChildStaffUnit(unit))
                {
                    //Инициализация счетчиков.
                    int mainCounter = 0;
                    int mainChildCounter = 0;
                    int childCounter = 0;
                    int nestedChildCounter = 0;
                    int mainNestedChildCounter = 0;
                    // Имя родительского подразделения.
                    worksheet.Cell(index, 1).Value = unit.Name;
                    // Стиль ьекста в ячейке.
                    worksheet.Cell(index, 1).Style.Font.Bold = true;
                    worksheet.Cell(index, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    //Переходим на следующий элемент.
                    index++;
                    // Выводим список штатных единиц главного подразделения.
                    foreach (var staffUnit in unit.StaffUnits)
                    {
                        //Название и количество ставок.
                        worksheet.Cell(index, 2).Value = staffUnit.Name;
                        worksheet.Cell(index, 3).Value = staffUnit.Rate;
                        //Повышоем общий счетчик на количество ставок.
                        mainCounter = mainCounter + staffUnit.Rate;
                        // Переходим к следующему элементу.
                        index++;
                    }
                    // Вывод потомков главного подразделения
                    foreach (var child in unit.Child)
                    {
                        // Обнуляем счетчик.
                        childCounter = 0;
                        //Выводим название потомка.
                        worksheet.Cell(index, 1).Value = child.Name;
                        worksheet.Cell(index, 1).Style = childUnitStyle;
                        //Переходим к выводу следующего элемента.
                        index++;
                        //Вывод штатных единиц потомка.
                        foreach (var childStaffUnit in child.StaffUnits)
                        {
                            //Название ШЕ и количество ставок.
                            worksheet.Cell(index, 2).Value = childStaffUnit.Name;
                            worksheet.Cell(index, 3).Value = childStaffUnit.Rate;
                            //Повышаем счетчик на количество ставок.
                            childCounter = childCounter + childStaffUnit.Rate;
                            //Переходим к выводу следующего элемента.
                            index++;
                        }

                        

                        foreach (var nestedChild in child.Child)
                        {
                            
                            worksheet.Cell(index, 1).Value = "    " + nestedChild.Name;
                            index++;
                            nestedChildCounter = 0;
                            foreach (var nestedStaffUnit in nestedChild.StaffUnits)
                            {
                                worksheet.Cell(index, 2).Value = nestedStaffUnit.Name;
                                worksheet.Cell(index, 3).Value = nestedStaffUnit.Rate;

                                nestedChildCounter = nestedChildCounter + nestedStaffUnit.Rate;
                                
                                index++;
                            }
                            mainNestedChildCounter = mainNestedChildCounter + nestedChildCounter;
                            if (nestedChildCounter != 0)
                            {
                                worksheet.Cell(index, 1).Value = "Итого: " + nestedChild.Name;
                                worksheet.Cell(index, 3).Value = nestedChildCounter;
                                worksheet.Cell(index, 1).Style.Font.Bold = true;
                                worksheet.Cell(index, 1).Style.Font.Italic = true;
                                worksheet.Cell(index, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                                worksheet.Cell(index, 3).Style.Font.Bold = true;
                                index++;
                            }
                        }

                        //mainChildCounter = mainChildCounter + childCounter + mainNestedChildCounter;
                        mainCounter = mainCounter + childCounter + mainNestedChildCounter ;
                        //mainCounter = mainCounter + childCounter ;
                        if (childCounter != 0)
                        {
                            worksheet.Cell(index, 1).Value = "    Итого: " + child.Name;
                            worksheet.Cell(index, 3).Value = childCounter + mainNestedChildCounter;
                            worksheet.Cell(index, 1).Style.Font.Bold = true;
                            worksheet.Cell(index, 1).Style.Font.Italic = true;
                            worksheet.Cell(index, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            worksheet.Cell(index, 3).Style.Font.Bold = true;
                            index++;
                        }

                    }
                    if (mainCounter != 0)
                    {
                        worksheet.Cell(index, 1).Value = "Итого: " + unit.Name;
                        worksheet.Cell(index, 3).Value = mainCounter;
                        worksheet.Cell(index, 1).Style.Font.Bold = true;
                        worksheet.Cell(index, 1).Style.Font.Italic = true;
                        worksheet.Cell(index, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        worksheet.Cell(index, 3).Style.Font.Bold = true; 
                        index++;
                    }

                    index++;
                }
               
            }

            workbook.SaveAs("C:\\1\\StaffingFile.xlsx");
        }
    }
}