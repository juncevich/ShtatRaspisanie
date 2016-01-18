using ClosedXML.Excel;
using ShtatRaspisanie.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ShtatRaspisanie.DataReader
{
    //Класс, который считывает xlsx файл и разбирает его на объекты.
    public class ExcelParser : IParser
    {
        public List<Unit> GetUnitList(string fileName)
        {
            var workbook = new XLWorkbook(fileName);
            var workSheet = workbook.Worksheet(1);
            var firstRowUsed = workSheet.FirstRowUsed();
            var unitRow = firstRowUsed.RowUsed();

            unitRow = unitRow.RowBelow();
            var units = new List<Unit>();
            while (!unitRow.Cell(1).IsEmpty())
            {
                var unit = new Unit();
                var name = unitRow.Cell(1).GetString();
                var parent = unitRow.Cell(2).GetString();
                unit.Name = name;
                unit.Parent = parent;
                units.Add(unit);
                unitRow = unitRow.RowBelow();
            }

            return units;
        }

        public DataTable GetUnitList()
        {
            throw new NotImplementedException();
        }

        public DataTable GetStaffUnitList()
        {
            throw new NotImplementedException();
        }

        public List<StaffUnit> GetStaffUnitList(string fileName)
        {
            var workbook = new XLWorkbook(fileName);
            var workSheet = workbook.Worksheet(1);
            var firstRowUsed = workSheet.FirstRowUsed();
            var staffUnitRow = firstRowUsed.RowUsed();

            staffUnitRow = staffUnitRow.RowBelow();
            var staffUnits = new List<StaffUnit>();
            while (!staffUnitRow.Cell(1).IsEmpty())
            {
                var staffUnit = new StaffUnit();
                var name = staffUnitRow.Cell(1).GetString();
                var parentUnit = staffUnitRow.Cell(2).GetString();
                var rate = Convert.ToInt32(staffUnitRow.Cell(3).GetString());
                staffUnit.Name = name;
                staffUnit.PodrName = parentUnit;
                staffUnit.Rate = rate;
                staffUnits.Add(staffUnit);
                staffUnitRow = staffUnitRow.RowBelow();
            }




            return staffUnits;
        }

        public void ValidateUnitFile( string fileName)
        {
            var workbook = new XLWorkbook(fileName);
            var workSheet = workbook.Worksheet(1);
            var firstRowUsed = workSheet.FirstRowUsed();
            var staffUnitRow = firstRowUsed.RowUsed();
            if (staffUnitRow.Cell(1).GetString() != "Name" &&
                staffUnitRow.Cell(2).GetString() != "Parent" 
                )
            {
                MessageBox.Show(@"Выбран не корректный файл.
                                 1-й столбец должен называться Namе,
                                 2-й столбец должен называться Podr_name,
                                 ");
                Application.Restart();
            }
        }

        public void ValidateStaffUnitFile(string fileName)
        {
            var workbook = new XLWorkbook(fileName);
            var workSheet = workbook.Worksheet(1);
            var firstRowUsed = workSheet.FirstRowUsed();
            var staffUnitRow = firstRowUsed.RowUsed();
            if (staffUnitRow.Cell(1).GetString() != "Name" &&
                staffUnitRow.Cell(2).GetString() != "Podr_name" &&
                staffUnitRow.Cell(3).GetString() != "Rate"
                )
            {
                MessageBox.Show(@"Выбран не корректный файл.
                                 1-й столбец должен называться Namе,
                                 2-й столбец должен называться Podr_name,
                                 3-й столбец должен называться Rate,
                                 ");
                Application.Restart();
            }
        }
    }
}