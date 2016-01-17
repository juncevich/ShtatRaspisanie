using System.Collections.Generic;

namespace ShtatRaspisanie.Entities
{
    public class ParentUnit
    {
        //Наименование подразделения.
        public string Name { get; set; }

        // Потомок родителя.
        public List<Unit> Child { get; set; }

        // Список штатных единиц.
        public List<StaffUnit> StaffUnits { get; set; }

        public override string ToString()
        {
            var view = Name + " " + Parent;
            return view;
        }
    }
}