using ClosedXML.Excel;
using ShtatRaspisanie.Entities;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShtatRaspisanie.DataWriter
{
    //Класс записывает информацию в файл Excel.
    public class ExcelDataWriter : IDataWriter
    {
        public void WriteData(List<Unit> units)
        {
            //Создаем файл.
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Штатное расписание");
            //Указываем ширину первых двух столбцов.
            var colA = worksheet.Column("A");
            colA.Width = 20;
            var colB = worksheet.Column("B");
            colB.Width = 35;

            //Стиль дочерних подразделений.
            var childUnitStyle = workbook.Style;
            childUnitStyle.Font.Bold = true;
            childUnitStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //Заголовок таблицы.
            worksheet.Cell(2, 1).Value = "Подразделение";
            worksheet.Cell(2, 2).Value = "ШЕ";
            worksheet.Cell(2, 3).Value = "Ставки";
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 2).Style.Font.Bold = true;
            worksheet.Cell(2, 3).Style.Font.Bold = true;
            //Номер начала строки.
            int index = 3;
            //Выводим родительские подразделения.
            foreach (ParentUnit unit in units)
            {
                //Условие выбора,у подразделения должен быть непустой список штатных единиц.
                if (unit.StaffUnits != null && unit.IsChildStaffUnit(unit))
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

                        //Вывод вложенных подразделений.
                        foreach (var nestedChild in child.Child)
                        {
                            //Вывод и оформление названия.
                            worksheet.Cell(index, 1).Value = "    " + nestedChild.Name;
                            worksheet.Cell(index, 1).Style.Font.Bold = true;
                            worksheet.Cell(index, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                            //Переходим к следующему элементу.
                            index++;
                            //Обнуляем счетчик штатных единиц во вложенных подразделениях.
                            nestedChildCounter = 0;
                            //Выводим список штатных единиц во вложенных подразделениях.
                            foreach (var nestedStaffUnit in nestedChild.StaffUnits)
                            {
                                //Выводим название и кол-во ставок.
                                worksheet.Cell(index, 2).Value = nestedStaffUnit.Name;
                                worksheet.Cell(index, 3).Value = nestedStaffUnit.Rate;
                                //Увеличиваем счетчик на кол-во ставок.
                                nestedChildCounter = nestedChildCounter + nestedStaffUnit.Rate;
                                //Переходим к следующему элементу.
                                index++;
                            }
                            //Увеличиваем общий список ставок во вложенных подразделениях.
                            mainNestedChildCounter = mainNestedChildCounter + nestedChildCounter;
                            //Вывод вложенных подразделений.
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

                        mainCounter = mainCounter + childCounter + mainNestedChildCounter;
                        //Вывод дочерних подразделений.
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
                    //Вывод родительских подразделений.
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
            //Рисуем границы.
            worksheet.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //Диалог сохранения файла.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = @"Excel file (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
        }
    }
}