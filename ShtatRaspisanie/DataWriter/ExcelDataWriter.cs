using System.Collections.Generic;
using System.Windows.Forms;
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
            

            worksheet.Cell(2, 1).Value = "Подразделение";
            worksheet.Cell(2, 2).Value = "ШЕ";
            worksheet.Cell(2, 3).Value = "Ставки";
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 2).Style.Font.Bold = true;
            worksheet.Cell(2, 3).Style.Font.Bold = true;

            int index = 3;
           
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
                            worksheet.Cell(index, 1).Style.Font.Bold = true;
                            worksheet.Cell(index, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
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
            worksheet.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            
        }
    }
}