using System.Collections.Generic;

namespace ShtatRaspisanie
{
    //Родительское подразделение.
    internal class ParentUnit : Unit
    {
        public ParentUnit()
        {
            var nestedUnitListLocal = new List<NestedUnit>();
            NestedUnitList = nestedUnitListLocal;
        }

        public List<NestedUnit> NestedUnitList { get; set; }
    }
}