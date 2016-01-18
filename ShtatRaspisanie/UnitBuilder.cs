using System;
using System.Collections.Generic;
using System.Linq;
using ShtatRaspisanie.Entities;

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
                unit.Child = FindChildren(unit);
            }
        }

        private Unit FindParent(Unit unit)
        {
            return parentList.FirstOrDefault(item => item.Child.Equals(unit.Name));
        }

        private List<Unit> FindChildren(Unit unit)
        {
            return parentList.Where(item => item.Parent.Equals(unit.Name)).ToList();
        }

        public void DisplayUnitList()
        {
            foreach (var parent in parentList.Where(parent => parent.Parent == " "))
            {
                Console.WriteLine(parent.Name);
                foreach (var child in parent.Child)
                {
                    Console.WriteLine(child);
                    if (child.Child != null)
                    {
                        foreach (var nestedChild in child.Child)
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