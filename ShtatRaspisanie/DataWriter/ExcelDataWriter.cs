using System.Collections.Generic;
using ClosedXML.Excel;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.DataWriter
{
    public class ExcelDataWriter
    {
        public void WriteData(List<Unit> units)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Штатное расписание");
            worksheet.Cell("A1").Value = "Штатное расписание";
            
            int nestedChildCounter = 0;
            int index = 2;
           
            foreach (ParentUnit unit in units)
            {
                if (unit.StaffUnits!=null && unit.IsChildStaffUnit(unit))
                {
                    int mainCounter = 0;
                    int childCounter = 0;
                    worksheet.Cell(index, 1).Value = unit.Name;
                    index++;
                    foreach (var staffUnit in unit.StaffUnits)
                    {
                        worksheet.Cell(index, 2).Value = staffUnit.Name;
                        worksheet.Cell(index, 3).Value = staffUnit.Rate;

                        mainCounter = mainCounter + staffUnit.Rate;
                        index++;
                    }

                    foreach (var child in unit.Child)
                    {
                        childCounter = 0;
                        worksheet.Cell(index, 1).Value = child.Name;
                        index++;
                        foreach (var childStaffUnit in child.StaffUnits)
                        {
                            worksheet.Cell(index, 2).Value = childStaffUnit.Name;
                            worksheet.Cell(index, 3).Value = childStaffUnit.Rate;

                            childCounter = childCounter + childStaffUnit.Rate;
                            index++;
                        }
                        foreach (var nestedChild in child.Child)
                        {
                            nestedChildCounter = 0;
                            worksheet.Cell(index, 1).Value = nestedChild.Name;
                            index++;

                            foreach (var nestedStaffUnit in nestedChild.StaffUnits)
                            {
                                worksheet.Cell(index, 2).Value = nestedStaffUnit.Name;
                                worksheet.Cell(index, 3).Value = nestedStaffUnit.Rate;

                                nestedChildCounter = nestedChildCounter + nestedStaffUnit.Rate;
                                index++;
                            }

                            if (nestedChildCounter != 0)
                            {
                                worksheet.Cell(index, 1).Value = "      Итого в " + nestedChild.Name + ": " + nestedChildCounter;
                                index++;
                            }
                        }
                        mainCounter = mainCounter + childCounter + nestedChildCounter;
                        if (childCounter != 0)
                        {
                            worksheet.Cell(index, 1).Value = "    Итого в " + child.Name + ": " + childCounter;
                            index++;
                        }

                    }
                    if (mainCounter != 0)
                    {
                        worksheet.Cell(index, 1).Value = "Итого в " + unit.Name + ": " + mainCounter;
                        index++;
                    }

                    index++;
                }
               
            }








            workbook.SaveAs("C:\\1\\StaffingFile.xlsx");
        }
    }
}