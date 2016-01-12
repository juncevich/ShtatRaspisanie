using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShtatRaspisanie
{
    //Родительское подразделение.
    class ParentUnit:Unit
    {
        public List<NestedUnit> NestedUnitList { get; set; }
        public ParentUnit()
        {
            List<NestedUnit> nestedUnitListLocal = new List<NestedUnit>();
            NestedUnitList = nestedUnitListLocal;
        }
    }
}
