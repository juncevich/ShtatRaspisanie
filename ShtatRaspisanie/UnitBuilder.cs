using System;
using System.Collections.Generic;
using System.Linq;

namespace ShtatRaspisanie
{
    internal class UnitBuilder
    {
        private readonly List<Unit> parentList;

        public UnitBuilder(List<Unit> parentList)
        {
            this.parentList = parentList;
        }

        public void SetChildByAll()
        {
            foreach (var unit in parentList)
            {
                unit.child = FindChildren(unit);
            }
        }

        private Unit FindParent(Unit unit)
        {
            return parentList.FirstOrDefault(item => item.child.Equals(unit.name));
        }

        private List<Unit> FindChildren(Unit unit)
        {
            return parentList.Where(item => item.parent.Equals(unit.name)).ToList();
        }

        public void DisplayUnitList()
        {
            foreach (var parent in parentList.Where(parent => parent.parent == " "))
            {
                Console.WriteLine(parent.name);
                foreach (var child in parent.child)
                {
                    Console.WriteLine(child);
                    if (child.child !=null)
                    {
                        foreach (var nestedChild in child.child)
                        {
                            Console.WriteLine(nestedChild);
                        }
                    }
                }
//                foreach (var child in parentList.Where(item => item.parent == parent.name).SelectMany(item => item.child))
//                {
//                    Console.WriteLine(child.name + " " + child.parent);
//                }
            }
        }
    }
}