using System;
using System.Collections.Generic;
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

        public static List<Unit> ParseParentList(FileStream stream)
        {
            var unitListFileReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = unitListFileReader.AsDataSet();
            var spisokPodrazdeleniyTable = result.Tables[0];
            var unitTableLenght = spisokPodrazdeleniyTable.Rows.Count;

            if ((string) spisokPodrazdeleniyTable.Rows[0][0] != "NAME" &&
                (string) spisokPodrazdeleniyTable.Rows[0][1] != "PARENT")
            {
                MessageBox.Show("Выбран не корректный файл");
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

            ////Список подразделений.
            //List<Podrazdelenie> podrazdelenieListLocal = new List<Podrazdelenie>();
            //// Перебираем содержимое файла.
            //int i = 1;
            //while (i < spisokPodrazdeleniyTable.Rows.Count)
            //{
            //    //Если поле "PARENT" равно null.
            //    if (spisokPodrazdeleniyTable.Rows[i][1] == DBNull.Value)
            //    {
            //        // Создаем родителя
            //        ParentUnit parentUnit = new ParentUnit();
            //        //Присваиваем имя.
            //        parentUnit.name = (string) spisokPodrazdeleniyTable.Rows[i][0];
            //        //Указываем, что у элемента нет родителя
            //        parentUnit.parent = " ";
            //        i++;
            //        while (i < spisokPodrazdeleniyTable.Rows.Count && spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value)
            //        {
            //            NestedUnit nestedUnit = new NestedUnit();
            //            nestedUnit.name = (string) spisokPodrazdeleniyTable.Rows[i][0];
            //            nestedUnit.parent = (string) spisokPodrazdeleniyTable.Rows[i][1];
            //            parentUnit.NestedUnitList.Add(nestedUnit);
            //            if (spisokPodrazdeleniyTable.Rows[i-1][1] != DBNull.Value  && spisokPodrazdeleniyTable.
            //              Rows[i-1][0]== spisokPodrazdeleniyTable.Rows[i][1])
            //            {
            //                ParentUnit parentUnitLocal = new ParentUnit();
            //                parentUnitLocal.name = (string) spisokPodrazdeleniyTable.Rows[i - 1][0];
            //                parentUnitLocal.parent = parentUnit.name;
            //                i++;
            //                while (i < spisokPodrazdeleniyTable.Rows.Count)
            //                {
            //                    NestedUnit nestedUnitLocal = new NestedUnit();
            //                    nestedUnitLocal.name = (string) spisokPodrazdeleniyTable.Rows[i][0];
            //                    nestedUnitLocal.parent = (string) spisokPodrazdeleniyTable.Rows[i][1];
            //                    parentUnitLocal.NestedUnitList.Add(nestedUnitLocal);
            //                    i++;

            //                }
            //                podrazdelenieListLocal.Add(parentUnitLocal);
            //            }
            //            i++;

            //        }
            //        podrazdelenieListLocal.Add(parentUnit);

            //    }

            //}
            PodrazdelenieList = unitList;
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

        public static void ParseSpisokShtatnEdinicaFile(FileStream stream)
        {
            var openSpisokShtatnEdinicReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = openSpisokShtatnEdinicReader.AsDataSet();
            var spisokShtatnEdinicTable = result.Tables[0];
            var shtatnEdinicaListLocal = new List<StaffUnit>();
            for (var i = 1; i < spisokShtatnEdinicTable.Rows.Count; i++)
            {
                if (spisokShtatnEdinicTable.Rows[i][1] != DBNull.Value)
                {
                    var shtanEdenica = new StaffUnit
                    {
                        NameOfShtatnajaEdinica = (string) spisokShtatnEdinicTable.Rows[i][0],
                        Podr_name = (string) spisokShtatnEdinicTable.Rows[i][1],
                        Rate = Convert.ToInt32(spisokShtatnEdinicTable.Rows[i][2])
                    };
                    Console.WriteLine(shtanEdenica.NameOfShtatnajaEdinica + " " + shtanEdenica.Podr_name + " " +
                                      shtanEdenica.Rate);
                    shtatnEdinicaListLocal.Add(shtanEdenica);
                }
            }
            ShtatnEdinicaList = shtatnEdinicaListLocal;
            IsSpisokShtatnEdinicaFileExist = true;
            Console.WriteLine(shtatnEdinicaListLocal.Count);
        }
    }
}