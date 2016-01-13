using System.Collections.Generic;

namespace ShtatRaspisanie
{
    class UnitBuilder
    {
        List<Unit> parentList;

        public UnitBuilder(List<Unit> parentList)
        {
            this.parentList = parentList;
        }

        public void setChildByAll()
        {
            foreach (Unit unit in parentList)
            {

                unit.child = findChildren(unit);

            }
        }

        private Unit findParent(Unit unit)
        {
            foreach (Unit item in parentList)
            {
                if (item.child.Equals(unit.name))
                {
                    return item;
                }
            }
            return null;
        }

        private List<Unit> findChildren(Unit unit)
        {
            List<Unit> tempList = new List<Unit>();
            foreach (Unit item in parentList)
            {
                if (item.parent.Equals(unit.name))
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }

        private void displayUnitList()
        {
            foreach (Unit item in parentList)
            {
                if (item.child != null)
                {
                    foreach (Unit child in item.child)
                    {
                        System.Console.WriteLine(child.name + " " + child.parent);
                    }
                }
            }
        }


    }
}
