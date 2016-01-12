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
        public static List<Podrazdelenie> PodrazdelenieList { get; private set; }
        public static List<ShtatnEdinica> ShtatnEdinicaList { get; private set; }
        public static Boolean IsSpisokPodrazdeleniyFileExist { get; private set; }
        public static Boolean IsSpisokShtatnEdinicaFileExist { get; private set; }

        public static void parseParentList(FileStream stream)
        {
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            DataTable spisokPodrazdeleniyTable = result.Tables[0];

            if ((string)spisokPodrazdeleniyTable.Rows[0][0] != "NAME" && (string)spisokPodrazdeleniyTable.Rows[0][1] != "PARENT")
            {
                MessageBox.Show("Выбран не корректный файл");
                return;
                
            }
            List<Podrazdelenie> podrazdelenieListLocal = new List<Podrazdelenie>();
            // Перебираем содержимое файла.
            int i = 1;
            while (i < spisokPodrazdeleniyTable.Rows.Count)
            {
                
                //Если поле "PARENT" равно null.
                if (spisokPodrazdeleniyTable.Rows[i][1] == DBNull.Value)
                {
                    ParentUnit parentUnit = new ParentUnit();
                    parentUnit.name = (string) spisokPodrazdeleniyTable.Rows[i][0];
                    parentUnit.parent = " ";
                    i++;
                    while (i < spisokPodrazdeleniyTable.Rows.Count && spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value)
                    {
                        NestedUnit nestedUnit = new NestedUnit();
                        nestedUnit.name = (string) spisokPodrazdeleniyTable.Rows[i][0];
                        nestedUnit.parent = (string) spisokPodrazdeleniyTable.Rows[i][1];
                        parentUnit.NestedUnitList.Add(nestedUnit);
                        if (spisokPodrazdeleniyTable.Rows[i-1][1] != DBNull.Value  && spisokPodrazdeleniyTable.Rows[i-1][0]== spisokPodrazdeleniyTable.Rows[i][1])
                        {
                            ParentUnit parentUnitLocal = new ParentUnit();
                            parentUnitLocal.name = (string) spisokPodrazdeleniyTable.Rows[i - 1][0];
                            parentUnitLocal.parent = parentUnit.name;
                            i++;
                            while (i < spisokPodrazdeleniyTable.Rows.Count)
                            {
                                
                                NestedUnit nestedUnitLocal = new NestedUnit();
                                nestedUnitLocal.name = (string) spisokPodrazdeleniyTable.Rows[i][0];
                                nestedUnitLocal.parent = (string) spisokPodrazdeleniyTable.Rows[i][1];
                                parentUnitLocal.NestedUnitList.Add(nestedUnitLocal);
                                i++;

                            }
                            podrazdelenieListLocal.Add(parentUnitLocal);
                        }
                        i++;

                    }
                    podrazdelenieListLocal.Add(parentUnit);

                }


            }
            PodrazdelenieList = podrazdelenieListLocal;
        }

        public static void parseSpisokPodrazdeleniyFile(FileStream stream)
        {
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            DataTable spisokPodrazdeleniyTable = result.Tables[0];
            List<Podrazdelenie> podrazdelenieListLocal = new List<Podrazdelenie>();

            for (int i =1; i<spisokPodrazdeleniyTable.Rows.Count; i++)
            {
                if (spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value) {
                    Podrazdelenie podrazdelenie = new Podrazdelenie();
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
            List<ShtatnEdinica> shtatnEdinicaListLocal = new List<ShtatnEdinica>();
            for (int i = 1; i < spisokShtatnEdinicTable.Rows.Count; i++)
            {
                if (spisokShtatnEdinicTable.Rows[i][1] != DBNull.Value)
                {
                    ShtatnEdinica shtanEdenica = new ShtatnEdinica();
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
