using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShtatRaspisanie
{
    public class StaffDao
    {
        private List<Unit> _units;

        public void GetAllUnits(DataTable dataTable)
        {
            var units = new List<Unit>();
            var unitTableLenght = dataTable.Rows.Count;
            for (int i = 0; i < unitTableLenght; i++)
            {
                Unit unit = new Unit();
                unit.Name = (string)dataTable.Rows[i][0];
                if (dataTable.Rows[i][1] == DBNull.Value)
                {
                    unit.Parent = " ";
                }
                else
                {
                    unit.Parent = (string)dataTable.Rows[i][1];
                }
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
               
                unit.Child = FindChildren(unit.Name);
            }
        }

        private List<Unit> FindChildren(string name)
        {
            return _units.Where(localUnit => localUnit.Parent.Equals(name)).ToList();
        }

    }
}