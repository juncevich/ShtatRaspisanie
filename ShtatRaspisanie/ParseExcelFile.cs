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
        //Список подразделений из файла.
        public static List<Podrazdelenie> podrazdelenieList { get; private set; }
        //Список штатных единиц из файла.
        public static List<ShtatnEdinica> shtatnEdinicaList { get; private set; }
        //Показывает, был ли выбор файла со списком подразделений.
        public static Boolean isSpisokPodrazdeleniyFileExist { get; private set; }
        //Показывает, был ли выбор файла со списком штатных единиц.
        public static Boolean isSpisokShtatnEdinicaFileExist { get; private set; }
        
        //Метод разбирает файл со списком подразделений.
        public static void parseSpisokPodrazdeleniyFile(FileStream stream)
        {
            //передаем файл для обработки библиотекой.
            IExcelDataReader openFileSpisokPodrazdeleniyReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //Полученный результат конвертируем в DataSet.
            DataSet result = openFileSpisokPodrazdeleniyReader.AsDataSet();
            //Получаем таблицу из DataSet.
            DataTable spisokPodrazdeleniyTable = result.Tables[0];
            //Создаем List() в котором будут находиться объекты Podrazdelenie.
            List<Podrazdelenie> podrazdelenieList = new List<Podrazdelenie>();
            //Заполняем список данными из файла.
            for (int i =1; i<spisokPodrazdeleniyTable.Rows.Count; i++)
            {
                //Выбираем записи с непустыми "Parent".
                if (spisokPodrazdeleniyTable.Rows[i][1] != DBNull.Value) {
                    Podrazdelenie podrazdelenie = new Podrazdelenie();
                    podrazdelenie.name = (String) spisokPodrazdeleniyTable.Rows[i][0];
                    podrazdelenie.parent = (String) spisokPodrazdeleniyTable.Rows[i][1];
                    podrazdelenieList.Add(podrazdelenie);
                     }
               
            }
            //Указываем, что файл выбран.
            isSpisokPodrazdeleniyFileExist = true;
            //Присваиваем значение полю класса.
            ParseExcelFile.podrazdelenieList = podrazdelenieList;
            
        }
        
        public static List<Podrazdelenie> getPodrazdelenieList()
        {
            return podrazdelenieList;
        }
        //Метод разбирает файл со списком штатных единиц.
        public static void parseSpisokShtatnEdinicaFile(FileStream stream)
        {
            //передаем файл для обработки библиотекой.
            IExcelDataReader openSpisokShtatnEdinicReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //Полученный результат конвертируем в DataSet.
            DataSet result = openSpisokShtatnEdinicReader.AsDataSet();
            //Получаем таблицу из DataSet.
            DataTable spisokShtatnEdinicTable = result.Tables[0];
            //Создаем List() в котором будут находиться объекты ShtatnEdinica.
            List<ShtatnEdinica> shtatnEdinicaList = new List<ShtatnEdinica>();
            //Заполняем список данными из файла.
            for (int i = 1; i < spisokShtatnEdinicTable.Rows.Count; i++)
            {
                //Выбираем записи с непустыми "Parent".
                if (spisokShtatnEdinicTable.Rows[i][1] != DBNull.Value)
                {
                    ShtatnEdinica shtanEdenica = new ShtatnEdinica();
                    shtanEdenica.NameOfShtatnajaEdinica = (String) spisokShtatnEdinicTable.Rows[i][0];
                    shtanEdenica.Podr_name = (String) spisokShtatnEdinicTable.Rows[i][1];
                    shtanEdenica.Rate = Convert.ToInt32( spisokShtatnEdinicTable.Rows[i][2]);
                    shtatnEdinicaList.Add(shtanEdenica);
                }

            }
            //Присваиваем значение полю класса.
            ParseExcelFile.shtatnEdinicaList = shtatnEdinicaList;
            //Указываем, что файл выбран.
            isSpisokShtatnEdinicaFileExist = true;

            
        }

        public static List<ShtatnEdinica> getShtatnEdinicaList()
        {
            return shtatnEdinicaList;
        }

    }
}
