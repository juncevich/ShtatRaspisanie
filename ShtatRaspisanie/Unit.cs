using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    class Unit
    {
        //Наименование подразделения.
        public string name { get; set; }
        //Родительское подразделение
        public string parent { get; set; }
        //Потомок родителя.
        public string child { get; set; }

    }
}
