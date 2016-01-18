using System;
using System.Collections.Generic;

namespace ShtatRaspisanie.Entities
{
    public class ParentUnit : Unit
    {
        //Наименование подразделения.
        public string Name { get; set; }

        //Наименование предка.
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

        public void Display(ParentUnit unit)
        {
            Console.WriteLine(unit.Name + @" " + unit.Parent);
            foreach (var staffUnit in unit.StaffUnits)
            {
                Console.WriteLine("  " + staffUnit.Name + @" " + staffUnit.PodrName + @" " + staffUnit.Rate);
            }

            foreach (var child in unit.Child)
            {
                Console.WriteLine(" " + child.Name + @" " + child.Parent);
                foreach (var childStaffUnit in child.StaffUnits)
                {
                    Console.WriteLine("    " + childStaffUnit.Name + @" " + childStaffUnit.PodrName + @" " + childStaffUnit.Rate);
                }
                foreach (var nestedChild in child.Child)
                {
                    Console.WriteLine("      " + nestedChild.Name + @" " + nestedChild.Parent);
                    foreach (var nestedStaffUnit in nestedChild.StaffUnits)
                    {
                        Console.WriteLine("        " + nestedStaffUnit.Name + @" " + nestedStaffUnit.PodrName + @" " + nestedStaffUnit.Rate);
                    }
                }
            }
        }
    }
}