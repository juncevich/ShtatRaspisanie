using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    //Вложенные подразделения.
    class NestedUnit:Podrazdelenie
    {
        public List<ShtatnEdinica> ShtatnEdinicaList { get; set; }
        public NestedUnit()
        {
            List<ShtatnEdinica> shtatnEdinicaListLocal = new List<ShtatnEdinica>();
            ShtatnEdinicaList = shtatnEdinicaListLocal;
        }
    }
}
