using System.Collections.Generic;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie
{
    public class Unit
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
            var view = Name + " " + Parent;
            return view;
        }
    }
}