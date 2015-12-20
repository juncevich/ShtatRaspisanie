using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    class ShtatnEdinica
    {
        //Наименование штатной единицы.
        string nameOfShtatnajaEdinica;
        //Наименование подразделения.
        string podr_name;
        //Кол-во ставок.
        int rate;

        public string NameOfShtatnajaEdinica
        {
            get
            {
                return nameOfShtatnajaEdinica;
            }

            set
            {
                nameOfShtatnajaEdinica = value;
            }
        }



        public int Rate
        {
            get
            {
                return rate;
            }

            set
            {
                rate = value;
            }
        }

        internal string Podr_name
        {
            get
            {
                return podr_name;
            }

            set
            {
                podr_name = value;
            }
        }
    }
}
