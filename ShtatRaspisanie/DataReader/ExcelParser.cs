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
            var categoryRow = firstRowUsed.RowUsed();

            categoryRow = categoryRow.RowBelow();
            ArrayList unitsArrayList = new ArrayList();

            DataTable dataUnitsTable = new DataTable();
            List<Unit> units = new List<Unit>();
            while (!categoryRow.Cell(1).IsEmpty())
            {
                Unit unit = new Unit();
                var name = categoryRow.Cell(1).GetString();
                var parent = categoryRow.Cell(2).GetString();
                unit.Name = name;
                unit.Parent = parent;
                units.Add(unit);
                categoryRow = categoryRow.RowBelow();
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
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            var openStaffUnitFileReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = openStaffUnitFileReader.AsDataSet();
            var staffUnitListTable = result.Tables[0];

            if ((string)staffUnitListTable.Rows[0][0] != "Name" &&
                (string)staffUnitListTable.Rows[0][1] != "Podr_name" &&
                (string)staffUnitListTable.Rows[0][1] != "RATE")
            {
                MessageBox.Show(@"Выбран не корректный файл");
                return null;
            }

            var staffUnits = new List<StaffUnit>();
            for (var i = 1; i < staffUnitListTable.Rows.Count; i++)
            {
                if (staffUnitListTable.Rows[i][1] != DBNull.Value)
                {
                    var staffUnit = new StaffUnit();
                    {
                        staffUnit.Name = (string)staffUnitListTable.Rows[i][0];
                        staffUnit.PodrName = (string)staffUnitListTable.Rows[i][1];
                        staffUnit.Rate = Convert.ToInt32(staffUnitListTable.Rows[i][2]);
                    };
                    Console.WriteLine(staffUnit.Name + " " + staffUnit.PodrName + " " +
                                      staffUnit.Rate);
                    staffUnits.Add(staffUnit);
                }
            }

            return staffUnits;
        }
    }
}