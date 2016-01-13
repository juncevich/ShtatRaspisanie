using System.Collections.Generic;

namespace ShtatRaspisanie
{
    //Вложенные подразделения.
    internal class NestedUnit : Unit
    {
        public NestedUnit()
        {
            var shtatnEdinicaListLocal = new List<StaffUnit>();
            ShtatnEdinicaList = shtatnEdinicaListLocal;
        }

        public List<StaffUnit> ShtatnEdinicaList { get; set; }
    }
}