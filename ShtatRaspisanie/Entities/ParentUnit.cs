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

        public bool IsChildStaffUnit(ParentUnit unit)
        {
            bool result = false;
            foreach (var childUnit in unit.Child)
            {
                if (childUnit.StaffUnits.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }
        public void Display(ParentUnit unit)
        {
            
            
        }
    }
}