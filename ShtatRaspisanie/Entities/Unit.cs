using ShtatRaspisanie.Entities;
using System.Collections.Generic;

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

        public Unit()
        {
            Child = new List<Unit>();
            StaffUnits = new List<StaffUnit>();
        }

        public override string ToString()
        {
            var view = Name + " " + Parent;
            return view;
        }
    }
}