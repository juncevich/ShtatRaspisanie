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
        //Список подразделений.
        public static ArrayList podrazdelenieList { get; private set; }
        //Список родителей.
        public static ArrayList parentList { get; private set; }
        //Список штатных единиц.
        public static ArrayList shtatnEdinicaList { get; private set; }
        //Есть ли файл со списком подразделений.
        public static Boolean isSpisokPodrazdeleniyFileExist { get; private set; }
        //Есть ли файл со списком штатных единиц.
        public static Boolean isSpisokShtatnEdinicaFileExist { get; private set; }

        //Разбираем файл со списком подразделений.
        public static void parseSpisokPodrazdeleniyFile(FileStream stream)
        {
            //Загружаем файл.
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //Представляем загруженный файл как DataSet.
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            //Выбираем из DataSet первую таблицу.
            DataTable spisokPodrazdeleniyTable = result.Tables[0];
            //Создаем список подразделений.
            ArrayList podrazdelenieList = new ArrayList();
            //Создаем список родителей.
            ArrayList parentList = new ArrayList();
            //Перебор значений в таблице.
            for (int i = 1; i < spisokPodrazdeleniyTable.Rows.Count; i++)
            {

                if (spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value) {
                    Podrazdelenie podrazdelenie = new Podrazdelenie();
                    podrazdelenie.name = (String) spisokPodrazdeleniyTable.Rows[i][0];
                    podrazdelenie.parent = (String) spisokPodrazdeleniyTable.Rows[i][1];
                    Console.WriteLine(podrazdelenie.name + " " + podrazdelenie.parent);
                    podrazdelenieList.Add(podrazdelenie);
                     }

                if (spisokPodrazdeleniyTable.Rows[i][0] != DBNull.Value && spisokPodrazdeleniyTable.Rows[i][1] == DBNull.Value)
                {
                    Podrazdelenie podrazdelenie = new Podrazdelenie();
                    podrazdelenie.name = (String)spisokPodrazdeleniyTable.Rows[i][0];
                    podrazdelenie.parent = " ";
                    Console.WriteLine(podrazdelenie.name + " " + podrazdelenie.parent);
                    parentList.Add(podrazdelenie);
                }
               
            }
            isSpisokPodrazdeleniyFileExist = true;
            Console.WriteLine(podrazdelenieList.Count);
            
        }
        // Получаем список подразделений.
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
