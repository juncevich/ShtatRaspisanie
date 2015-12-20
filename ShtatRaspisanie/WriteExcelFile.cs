using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    class WriteExcelFile
    {
        public static void writeShtatnoeRaspisanie(ArrayList podrazdelenieList, ArrayList shtatnEdinicaList)
        {
            if (podrazdelenieList != null && shtatnEdinicaList != null)
            {
                Console.WriteLine("WriteExcelFile1");
            }
        }

        internal static void writeShtatnoeRaspisanie(Action<FileStream> parseSpisokPodrazdeleniyFile, Action<FileStream> parseSpisokShtatnEdinicaFile)
        {

            if (ParseExcelFile.isSpisokShtatnEdinicaFileExist && ParseExcelFile.isSpisokPodrazdeleniyFileExist)
            {
                Console.WriteLine("WriteExcelFile2");
            } else
            {
                Console.WriteLine("Выберите оба файла.");
            }
        }
    }
}
