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
            int mainCounter = 0;
            int childCounter = 0;
            int nestedChildCounter = 0;
            Console.WriteLine(unit.Name + @" " + unit.Parent);
            foreach (var staffUnit in unit.StaffUnits)
            {
                Console.WriteLine("  " + staffUnit.Name + @" " + staffUnit.PodrName + @" " + staffUnit.Rate);
                mainCounter = mainCounter + staffUnit.Rate;
            }

            foreach (var child in unit.Child)
            {
                childCounter = 0;
                Console.WriteLine(" " + child.Name + @" " + child.Parent);
                foreach (var childStaffUnit in child.StaffUnits)
                {
                    Console.WriteLine("    " + childStaffUnit.Name + @" " + childStaffUnit.PodrName + @" " + childStaffUnit.Rate);
                    childCounter = childCounter + childStaffUnit.Rate;
                }
                foreach (var nestedChild in child.Child)
                {
                    nestedChildCounter = 0;
                    Console.WriteLine("      " + nestedChild.Name + @" " + nestedChild.Parent);
                    foreach (var nestedStaffUnit in nestedChild.StaffUnits)
                    {
                        Console.WriteLine("        " + nestedStaffUnit.Name + @" " + nestedStaffUnit.PodrName + @" " + nestedStaffUnit.Rate);
                        nestedChildCounter = nestedChildCounter + nestedStaffUnit.Rate;
                    }

                    if (nestedChildCounter!=0)
                    {
                        Console.WriteLine("      Итого в "+nestedChild.Name+": "+nestedChildCounter);
                    }
                }
                mainCounter = mainCounter + childCounter + nestedChildCounter;
                if (childCounter!=0)
                {
                    Console.WriteLine("    Итого в " + child.Name + ": " + childCounter); 
                }
                
            }
            if (mainCounter!=0)
            {
                Console.WriteLine("Итого в " + unit.Name + ": " + mainCounter);
            }
            
        }
    }
}