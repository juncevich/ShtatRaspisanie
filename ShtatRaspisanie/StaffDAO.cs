using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShtatRaspisanie
{
    public class StaffDao
    {
        private List<Unit> _units;

        public void GetAllUnits(Hashtable dataList)
        {
            var units = new List<Unit>();
            foreach (DictionaryEntry value in dataList)
            {
                var unit = new Unit();
                unit.Name = value.Key as string;
                unit.Parent = value.Value as string;
                units.Add(unit);

            }
            
            this._units = units;
        }

        public void SetUnits(List<Unit> units)
        {
            this._units = units;
        }
        public void SetChildToUnitList()
        {
            foreach (var unit in _units)
            {
               
                unit.Child = FindChildren(unit);
            }
        }

        private List<Unit> FindChildren(Unit unit)
        {
            return _units.Where(localUnit => localUnit.Parent.Equals(unit.Name)).ToList();
        }
    }
}