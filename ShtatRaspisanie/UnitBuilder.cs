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
            foreach (var child in parentList.Where(item => item.child != null).SelectMany(item => item.child))
            {
                Console.WriteLine(child.name + "" + child.parent);
            }
        }
    }
}