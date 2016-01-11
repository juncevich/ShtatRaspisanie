using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;

namespace ShtatRaspisanie
{
    class WriteExcelFile
    {

        WriteExcelFile()
        {

        }

        internal static void writeShtatnoeRaspisanie(List<Podrazdelenie> podrazdelenieList, List<ShtatnEdinica> shtatnEdinicaList)
        {
            //ParseExcelFile parseExcelFile = new ParseExcelFile();
            //По условию должны быть выбран файл со списком Подразделений и файл со списком штатных единиц.
            if (ParseExcelFile.isSpisokShtatnEdinicaFileExist && ParseExcelFile.isSpisokPodrazdeleniyFileExist)
            {
                //Список уникальных подразделений.
                HashSet<Podrazdelenie> podrazdelenieSet = new HashSet<Podrazdelenie>();
                //Получаем список подразделений из файла.
                podrazdelenieList = ParseExcelFile.getPodrazdelenieList();
                //Получаем список штатных единиц из файла.
                shtatnEdinicaList = ParseExcelFile.getShtatnEdinicaList();
                //Начинаем перебор в уникальном списке подразделений
                foreach (Podrazdelenie uniqParent in podrazdelenieList)
                {
                    //Переменная для подсчета общего количества ставок по подразделениям.
                    int rate = 0;
                    Console.WriteLine("---------------------------------------");
                    //Вывод родителя подразделения.
                    Console.WriteLine(uniqParent.parent);
                    //Перебираем список со штатными единицами.
                    for(int i =0; i< shtatnEdinicaList.Count;i++){
                        //Выбор конкретных записей, родитель у которых объявлен выше.
                        if (uniqParent.name == shtatnEdinicaList[i].Podr_name)
                        {
                            //Подсчет общего количества ставок по подразделениям.
                            rate = rate + shtatnEdinicaList[i].Rate;
                            //Вывод значений.
                            Console.WriteLine(shtatnEdinicaList[i].NameOfShtatnajaEdinica + " "+ shtatnEdinicaList[i].Podr_name + " " +shtatnEdinicaList[i].Rate);
                            
                        }
                        
                    }
                    //Вывод общего количества ставок по подразделениям.
                    Console.WriteLine("Общее количество по участку "+ uniqParent.name + ": " + rate);
                }


            } else
            {
                //Если оба файла не выбраны, то выкидывает предупреждение.
                MessageBox.Show("Выберите оба файла.");
                
            }
        }
    }
}
