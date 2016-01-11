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
        public static List<Podrazdelenie> podrazdelenieList { get; private set; }
        public static List<ShtatnEdinica> shtatnEdinicaList { get; private set; }
        public static Boolean isSpisokPodrazdeleniyFileExist { get; private set; }
        public static Boolean isSpisokShtatnEdinicaFileExist { get; private set; }

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
            podrazdelenieList = podrazdelenieListLocal;
            isSpisokPodrazdeleniyFileExist = true;
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
            shtatnEdinicaList = shtatnEdinicaListLocal;
            isSpisokShtatnEdinicaFileExist = true;
            Console.WriteLine(shtatnEdinicaListLocal.Count);
            
        }

    }
}
