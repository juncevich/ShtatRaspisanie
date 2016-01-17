using System;
using System.Collections.Generic;
using System.Data;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.Handlers
{
    public class DataHandler : IDataHandler
    {
        public void HandleData(DataTable unitTable, List<StaffUnit> staffUnits)
        {

        }

        public List<IUnit> HandleUnitTable(DataTable dataTable)
        {
            int index = 0;
            var units = new List<IUnit>();
            var unitTableLenght = dataTable.Rows.Count;
            
            // Перечисление всего спискка, пока поле name не пустое.
            while (dataTable.Rows[index][0] != DBNull.Value)
            {
                int tempIndex = index;
                bool simple = true;
                
                if (dataTable.Rows[index][1] == DBNull.Value)
                {
                    while (dataTable.Rows[tempIndex][1] != DBNull.Value)
                    {
                        if (dataTable.Rows[index][1] == dataTable.Rows[index - 1 ][0])
                        {
                            simple = false;
                        }
                        tempIndex++;
                    }

                    // Заполняем простое подразделение
                    if (simple == true)
                    {
                        string name = (string)dataTable.Rows[index][1];
                        string parent = "";
                        IUnit unit = new UnitWithOneNestedGrade(name, parent);

                        while (dataTable.Rows[index][1] == DBNull.Value)
                        {
                            
                            name = (string) dataTable.Rows[index][0];
                            parent = (string) dataTable.Rows[index][1];
                            IUnit childUnit = new UnitWithOneNestedGrade(name, parent);
                            unit.Child.Add(childUnit);
                            index++;
                        }
                        units.Add(unit);

                    }

                    if (simple == false)
                    {
                        string name = (string)dataTable.Rows[index][1];
                        string parent = "";
                        IUnit unit = new UnitWithTwoNestedGrade(name, parent);
                    }



                    

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