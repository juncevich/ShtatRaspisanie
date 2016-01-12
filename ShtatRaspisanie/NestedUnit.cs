using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    //Вложенные подразделения.
    class NestedUnit:Unit
    {
        public List<StaffUnit> ShtatnEdinicaList { get; set; }
        public NestedUnit()
        {
            List<StaffUnit> shtatnEdinicaListLocal = new List<StaffUnit>();
            ShtatnEdinicaList = shtatnEdinicaListLocal;
        }
    }
}
