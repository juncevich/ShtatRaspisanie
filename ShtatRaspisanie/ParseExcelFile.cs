using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ShtatRaspisanie
{
    //Класс, который считывает xlsx файл и разбирает его на объекты.
    class ParseExcelFile
    {
        public static List<Unit> PodrazdelenieList { get; private set; }
        public static List<StaffUnit> ShtatnEdinicaList { get; private set; }
        public static Boolean IsSpisokPodrazdeleniyFileExist { get; private set; }
        public static Boolean IsSpisokShtatnEdinicaFileExist { get; private set; }

        
        public static List<Unit> parseParentList(FileStream stream)
        {
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            DataTable spisokPodrazdeleniyTable = result.Tables[0];
            int unitTableLenght = spisokPodrazdeleniyTable.Rows.Count;

            if ((string)spisokPodrazdeleniyTable.Rows[0][0] != "NAME" && (string)spisokPodrazdeleniyTable.Rows[0][1] != "PARENT")
            {
                MessageBox.Show("Выбран не корректный файл");
                return null;
                
            }
            List<Unit> podrazdelenieListLocal = new List<Unit>();
            for (int i=1; i<unitTableLenght; i++)
            {
                Unit podrazdelenie = new Unit();
                podrazdelenie.name = (string)spisokPodrazdeleniyTable.Rows[i][0];
                if (spisokPodrazdeleniyTable.Rows[i][1] == DBNull.Value)
                {
                    podrazdelenie.parent = " ";
                } else
                {
                    podrazdelenie.parent = (string) spisokPodrazdeleniyTable.Rows[i][1];
                }

                podrazdelenieListLocal.Add(podrazdelenie);
            }
            return podrazdelenieListLocal;


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
            //            if (spisokPodrazdeleniyTable.Rows[i-1][1] != DBNull.Value  && spisokPodrazdeleniyTable.Rows[i-1][0]== spisokPodrazdeleniyTable.Rows[i][1])
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
            PodrazdelenieList = podrazdelenieListLocal;
        }

        public static void parseSpisokPodrazdeleniyFile(FileStream stream)
        {
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            DataTable spisokPodrazdeleniyTable = result.Tables[0];
            List<Unit> podrazdelenieListLocal = new List<Unit>();

            for (int i =1; i<spisokPodrazdeleniyTable.Rows.Count; i++)
            {
                if (spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value) {
                    Unit podrazdelenie = new Unit();
                    podrazdelenie.name = (String) spisokPodrazdeleniyTable.Rows[i][0];
                    podrazdelenie.parent = (String) spisokPodrazdeleniyTable.Rows[i][1];
                    Console.WriteLine(podrazdelenie.name + " " + podrazdelenie.parent);
                    podrazdelenieListLocal.Add(podrazdelenie);
                     }
               
            }
            PodrazdelenieList = podrazdelenieListLocal;
            IsSpisokPodrazdeleniyFileExist = true;
            Console.WriteLine(podrazdelenieListLocal.Count);
            
        }

        public static void parseSpisokShtatnEdinicaFile(FileStream stream)
        {
            IExcelDataReader openSpisokShtatnEdinicReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openSpisokShtatnEdinicReader.AsDataSet();
            DataTable spisokShtatnEdinicTable = result.Tables[0];
            List<StaffUnit> shtatnEdinicaListLocal = new List<StaffUnit>();
            for (int i = 1; i < spisokShtatnEdinicTable.Rows.Count; i++)
            {
                if (spisokShtatnEdinicTable.Rows[i][1] != DBNull.Value)
                {
                    StaffUnit shtanEdenica = new StaffUnit();
                    shtanEdenica.NameOfShtatnajaEdinica = (String) spisokShtatnEdinicTable.Rows[i][0];
                    shtanEdenica.Podr_name = (String) spisokShtatnEdinicTable.Rows[i][1];
                    shtanEdenica.Rate = Convert.ToInt32( spisokShtatnEdinicTable.Rows[i][2]);
                    Console.WriteLine(shtanEdenica.NameOfShtatnajaEdinica + " " + shtanEdenica.Podr_name + " " + shtanEdenica.Rate);
                    shtatnEdinicaListLocal.Add(shtanEdenica);
                }

            }
            ShtatnEdinicaList = shtatnEdinicaListLocal;
            IsSpisokShtatnEdinicaFileExist = true;
            Console.WriteLine(shtatnEdinicaListLocal.Count);
            
        }

    }
}
