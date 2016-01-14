using System.Collections.Generic;

namespace ShtatRaspisanie
{
    internal class Unit
    {
        //Наименование подразделения.
        public string Name { get; set; }

        //Родительское подразделение
        public string Parent { get; set; }

        // Потомок родителя.
        public List<Unit> Child { get; set; }
        // Список штатных единиц.
        public List<StaffUnit> StaffUnits { get; set; }

        public override string ToString()
        {
            string view = Name + " " + Parent;
            return view;
        }
    }
}