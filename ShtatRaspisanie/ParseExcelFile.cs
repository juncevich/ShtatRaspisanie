using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Excel;

namespace ShtatRaspisanie
{
    //Класс, который считывает xlsx файл и разбирает его на объекты.
    internal class ParseExcelFile
    {
        private List<Unit> unitList;

        public ParseExcelFile(List<Unit> unitList)
        {
            this.unitList = unitList;
        }



        public static List<Unit> PodrazdelenieList { get; private set; }
        public static List<StaffUnit> ShtatnEdinicaList { get; private set; }
        public static bool IsSpisokPodrazdeleniyFileExist { get; private set; }
        public static bool IsSpisokShtatnEdinicaFileExist { get; private set; }
        private static FileStream _unitFileStream;
        private static string _unitFileName;


        public static FileStream GetUnitFileStream()
        {
            return _unitFileStream;
        }

        public static void SetUnitFileStream(FileStream stream)
        {
            ParseExcelFile._unitFileStream = stream;
        }

        public static string GetUnitFileName()
        {
            return _unitFileName;
        }

        public static void SetUnitFileName(string fileName)
        {
            ParseExcelFile._unitFileName = fileName;
        }

        public static DataTable ParseUnitFile(string fileName)
        {
            
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            var unitListFileReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = unitListFileReader.AsDataSet();
            var dataUnitsTable = result.Tables[0];
            

            if ((string)dataUnitsTable.Rows[0][0] != "name" &&
                (string)dataUnitsTable.Rows[0][1] != "Parent")
            {
                MessageBox.Show(@"Выбран не корректный файл");
                return null;
            }
            
            return dataUnitsTable;
        }
          
        public static List<Unit> ParseParentList(FileStream stream)
        {
            var unitListFileReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = unitListFileReader.AsDataSet();
            var spisokPodrazdeleniyTable = result.Tables[0];
            var unitTableLenght = spisokPodrazdeleniyTable.Rows.Count;

            if ((string) spisokPodrazdeleniyTable.Rows[0][0] != "NAME" &&
                (string) spisokPodrazdeleniyTable.Rows[0][1] != "PARENT")
            {
                MessageBox.Show(@"Выбран не корректный файл");
                return null;
            }
            var unitList = new List<Unit>();
            for (var i = 1; i < unitTableLenght; i++)
            {
                var unit = new Unit();
                unit.Name = (string) spisokPodrazdeleniyTable.Rows[i][0];
                if (spisokPodrazdeleniyTable.Rows[i][1] == DBNull.Value)
                {
                    unit.Parent = " ";
                }
                else
                {
                    unit.Parent = (string) spisokPodrazdeleniyTable.Rows[i][1];
                }

                unitList.Add(unit);
            }
            return unitList;

        }

        public static void ParseSpisokPodrazdeleniyFile(FileStream stream)
        {
            var openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            var spisokPodrazdeleniyTable = result.Tables[0];
            var podrazdelenieListLocal = new List<Unit>();

            for (var i = 1; i < spisokPodrazdeleniyTable.Rows.Count; i++)
            {
                if (spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value)
                {
                    var podrazdelenie = new Unit
                    {
                        Name = (string) spisokPodrazdeleniyTable.Rows[i][0],
                        Parent = (string) spisokPodrazdeleniyTable.Rows[i][1]
                    };
                    Console.WriteLine(podrazdelenie.Name + " " + podrazdelenie.
Parent);
                    podrazdelenieListLocal.Add(podrazdelenie);
                }
            }
            PodrazdelenieList = podrazdelenieListLocal;
            IsSpisokPodrazdeleniyFileExist = true;
            Console.WriteLine(podrazdelenieListLocal.Count);
        }

        public static DataTable ParseStaffUnitsFile(string fileName)
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

            var shtatnEdinicaListLocal = new List<StaffUnit>();
            for (var i = 1; i < staffUnitListTable.Rows.Count; i++)
            {
                if (staffUnitListTable.Rows[i][1] != DBNull.Value)
                {
                    var shtanEdenica = new StaffUnit
                    {
                        NameOfShtatnajaEdinica = (string) staffUnitListTable.Rows[i][0],
                        PodrName = (string) staffUnitListTable.Rows[i][1],
                        Rate = Convert.ToInt32(staffUnitListTable.Rows[i][2])
                    };
                    Console.WriteLine(shtanEdenica.NameOfShtatnajaEdinica + " " + shtanEdenica.PodrName + " " +
                                      shtanEdenica.Rate);
                    shtatnEdinicaListLocal.Add(shtanEdenica);
                }
            }
            
            IsSpisokShtatnEdinicaFileExist = true;
            return staffUnitListTable;
        }
    }
}