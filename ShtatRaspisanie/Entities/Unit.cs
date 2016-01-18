using System;
using System.Collections.Generic;

namespace ShtatRaspisanie.Entities
{
    //Подразделение.
    public class Unit : IUnit
    {
        //Наименование подразделения.
        public string Name { get; set; }

        //Родительское подразделение
        public string Parent { get; set; }

        // Потомок родителя.
        public List<Unit> Child { get; set; }

        // Список штатных единиц .
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

        public void Display(Unit unit)
        {
            Console.WriteLine(unit.Name + @" " + unit.Parent);
            foreach (var staffUnit in unit.StaffUnits)
            {
                Console.WriteLine(staffUnit.Name + @" " + staffUnit.PodrName + @" " + staffUnit.Rate);
            }

            foreach (var child in unit.Child)
            {
                Console.WriteLine(child.Name + @" " + child.Parent);
                foreach (var childStaffUnit in StaffUnits)
                {
                    Console.WriteLine(childStaffUnit.Name + @" " + childStaffUnit.PodrName + @" " + childStaffUnit.Rate);
                }
            }
        }
    }
}