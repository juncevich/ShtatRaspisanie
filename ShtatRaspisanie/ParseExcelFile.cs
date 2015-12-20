using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    //Класс, который считывает xlsx файл и разбирает его на объекты.
    class ParseExcelFile
    {
        public static ArrayList podrazdelenieList { get; private set; }
        public static ArrayList shtatnEdinicaList { get; private set; }
        public static Boolean isSpisokPodrazdeleniyFileExist { get; private set; }
        public static Boolean isSpisokShtatnEdinicaFileExist { get; private set; }
        public static void parseSpisokPodrazdeleniyFile(FileStream stream)
        {
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            DataTable spisokPodrazdeleniyTable = result.Tables[0];
            ArrayList podrazdelenieList = new ArrayList();
            for (int i =1; i<spisokPodrazdeleniyTable.Rows.Count; i++)
            {
                if (spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value) {
                    Podrazdelenie podrazdelenie = new Podrazdelenie();
                    podrazdelenie.name = (String) spisokPodrazdeleniyTable.Rows[i][0];
                    podrazdelenie.parent = (String) spisokPodrazdeleniyTable.Rows[i][1];
                    Console.WriteLine(podrazdelenie.name + " " + podrazdelenie.parent);
                    podrazdelenieList.Add(podrazdelenie);
                     }
               
            }
            isSpisokPodrazdeleniyFileExist = true;
            Console.WriteLine(podrazdelenieList.Count);
            
        }
        public static ArrayList getPodrazdelenieList()
        {
            return podrazdelenieList;
        }
        public static void parseSpisokShtatnEdinicaFile(FileStream stream)
        {
            IExcelDataReader openSpisokShtatnEdinicReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = openSpisokShtatnEdinicReader.AsDataSet();
            DataTable spisokShtatnEdinicTable = result.Tables[0];
            ArrayList shtatnEdinicaList = new ArrayList();
            for (int i = 1; i < spisokShtatnEdinicTable.Rows.Count; i++)
            {
                if (spisokShtatnEdinicTable.Rows[i][1] != DBNull.Value)
                {
                    ShtatnEdinica shtanEdenica = new ShtatnEdinica();
                    shtanEdenica.NameOfShtatnajaEdinica = (String) spisokShtatnEdinicTable.Rows[i][0];
                    shtanEdenica.Podr_name = (String) spisokShtatnEdinicTable.Rows[i][1];
                    shtanEdenica.Rate = Convert.ToInt32( spisokShtatnEdinicTable.Rows[i][2]);
                    Console.WriteLine(shtanEdenica.NameOfShtatnajaEdinica + " " + shtanEdenica.Podr_name + " " + shtanEdenica.Rate);
                    shtatnEdinicaList.Add(shtanEdenica);
                }

            }
            isSpisokShtatnEdinicaFileExist = true;
            Console.WriteLine(shtatnEdinicaList.Count);
            
        }

        public static ArrayList getShtatnEdinicaList()
        {
            return shtatnEdinicaList;
        }

    }
}
