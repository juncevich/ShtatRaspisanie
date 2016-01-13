using System;
using System.Collections.Generic;

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
            foreach (var item in parentList)
            {
                if (item.child.Equals(unit.name))
                {
                    return item;
                }
            }
            return null;
        }

        private List<Unit> FindChildren(Unit unit)
        {
            var tempList = new List<Unit>();
            foreach (var item in parentList)
            {
                if (item.parent.Equals(unit.name))
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }

        public void DisplayUnitList()
        {
            foreach (var item in parentList)
            {
                if (item.child != null)
                {
                    foreach (var child in item.child)
                    {
                        Console.WriteLine(child.name + "" + child.parent);
                    }
                }
            }
        }
    }
}