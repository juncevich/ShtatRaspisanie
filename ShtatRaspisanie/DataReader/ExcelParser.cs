using ClosedXML.Excel;
using Excel;
using ShtatRaspisanie.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            var StaffUnitRow = firstRowUsed.RowUsed();
            StaffUnitRow = StaffUnitRow.RowBelow();
            var staffUnits = new List<StaffUnit>();
            while (!StaffUnitRow.Cell(1).IsEmpty())
            {
                var staffUnit = new StaffUnit();
                var name = StaffUnitRow.Cell(1).GetString();
                var parentUnit = StaffUnitRow.Cell(2).GetString();
                var rate = Convert.ToInt32(StaffUnitRow.Cell(3).GetString());
                staffUnit.Name = name;
                staffUnit.PodrName = parentUnit;
                staffUnit.Rate = rate;
                staffUnits.Add(staffUnit);
                StaffUnitRow = StaffUnitRow.RowBelow();
            }

            //if ((string)staffUnitListTable.Rows[0][0] != "Name" &&
            //    (string)staffUnitListTable.Rows[0][1] != "Podr_name" &&
            //    (string)staffUnitListTable.Rows[0][1] != "RATE")
            //{
            //    MessageBox.Show(@"Выбран не корректный файл");
            //    return null;
            //}

            //var staffUnits = new List<StaffUnit>();
            //for (var i = 1; i < staffUnitListTable.Rows.Count; i++)
            //{
            //    if (staffUnitListTable.Rows[i][1] != DBNull.Value)
            //    {
            //        var staffUnit = new StaffUnit();
            //        {
            //            staffUnit.Name = (string)staffUnitListTable.Rows[i][0];
            //            staffUnit.PodrName = (string)staffUnitListTable.Rows[i][1];
            //            staffUnit.Rate = Convert.ToInt32(staffUnitListTable.Rows[i][2]);
            //        };
            //        Console.WriteLine(staffUnit.Name + " " + staffUnit.PodrName + " " +
            //                          staffUnit.Rate);
            //        staffUnits.Add(staffUnit);
            //    }
            //}

            return staffUnits;
        }
    }
}