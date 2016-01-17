using ShtatRaspisanie.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace ShtatRaspisanie.Handlers
{
    public class DataHandler : IDataHandler
    {
        public void HandleData(DataTable unitTable, List<StaffUnit> staffUnits)
        {
        }

        public List<IUnit> HandleUnitTable(Hashtable hashtable)
        {
            var index = 0;
            var units = new List<IUnit>();
            string parentName = "";
            foreach (DictionaryEntry item in hashtable)
            {
                if (item.Value.Equals(""))
                {
                    var parentUnit = new ParentUnit();
                    parentUnit.Name = (string)item.Key;
                    parentName = (string)item.Key;
                } else if (item.Value.Equals(parentName))
                {
                    
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