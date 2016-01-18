using ShtatRaspisanie.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShtatRaspisanie.Handlers
{
    public class DataHandler : IDataHandler
    {
        public void HandleData(DataTable unitTable, List<StaffUnit> staffUnits)
        {
        }

        public ArrayList HandleUnitTable(Hashtable hashtable)
        {
            hashtable.Cast<DictionaryEntry>().OrderBy(entry => entry.Value).ToList();
            ParentUnit parentUnitMain = new ParentUnit();
            parentUnitMain.Name = "";
            parentUnitMain.Parent = "";
            Unit lastItem = null;
            var index = 0;
            var units = new ArrayList();
            string parentName = "";

            foreach (DictionaryEntry item in hashtable)
            {
                if (item.Value.Equals(""))
                {
                    var parentUnit = new ParentUnit();
                    parentUnit.Child = new List<Unit>();
                    parentUnit.Name = (string)item.Key;
                    parentName = (string)item.Key;
                    units.Add(parentUnit);
                    parentUnitMain = parentUnit;
                }
                else if (item.Value.Equals(parentUnitMain.Name))
                {
                    Unit unit = new Unit();
                    unit.Name = (string)item.Key;
                    unit.Parent = (string)item.Value;
                    parentUnitMain.Child.Add(unit);
                    lastItem = unit;
                }
                //else if (!ReferenceEquals(item.Value, "") && lastItem != null && ReferenceEquals(item.Value, lastItem.Name))
                //{
                //    Unit unit = new Unit();

                //}
            }

            //for (var i = 1; i < unitTableLenght; i++)
            //{
            //    var unit = new Unit();
            //    unit.Name = (string) dataTable.Rows[i][0];
            //    if (dataTable.Rows[i][1] == DBNull.Value)
            //    {
            //        unit.Parent = " ";
            //    }
            //    else
            //    {
            //        unit.Parent = (string) dataTable.Rows[i][1];
            //    }
            //    units.Add(unit);

            //    return units;
            //}
            return units;
        }
    }
}