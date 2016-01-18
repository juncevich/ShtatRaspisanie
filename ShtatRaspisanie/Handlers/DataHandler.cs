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

        public ArrayList HandleUnitTable(List<Unit> listOfString)
        {
            
            ParentUnit parentUnitMain = new ParentUnit();
            parentUnitMain.Name = "";
            parentUnitMain.Parent = "";
            Unit lastItem = new Unit();
            var index = 0;
            var units = new ArrayList();
            string parentName = "";

            foreach (var item in listOfString)
            {
                if (item.Parent.Equals(""))
                {
                    var parentUnit = new ParentUnit();
                    parentUnit.Child = new List<Unit>();
                    parentUnit.Name = (string)item.Name;
                    parentUnit.Parent = "";
                    parentName = (string)item.Name;
                    units.Add(parentUnit);
                    parentUnitMain = parentUnit;
                }
                else if (item.Parent.Equals(parentUnitMain.Name))
                {
                    Unit unit = new Unit();
                    unit.Name = (string)item.Name;
                    unit.Parent = (string)item.Parent;
                    parentUnitMain.Child.Add(unit);
                    lastItem = unit;
                } else if (!ReferenceEquals(item.Parent, "") && lastItem != null && ReferenceEquals(item.Parent, lastItem.Name))
                {
                    lastItem.Child.Add(item);
                    Unit unit = new Unit();

                }
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