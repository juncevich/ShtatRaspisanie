using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShtatRaspisanie
{
    public class StaffDao
    {
        //Список подразделений.
        private List<Unit> _units;
        //Список штатных единиц.
        private List<StaffUnit> _staffUnits;
        // Класс синглтон.
        private static StaffDao staffDao;

        private StaffDao()
        {
        }
        // Получить экземпляр синглтона.
        public static StaffDao GetInstance()
        {
            if (staffDao == null)
            {
                lock (typeof(StaffDao))
                {
                    if (staffDao == null)
                        staffDao = new StaffDao();
                }
            }

            return staffDao;
        }
        // Создать список подразделений. 
        public void MakeAllUnits(DataTable dataTable)
        {

            _units = new List<Unit>();
            var unitTableLenght = dataTable.Rows.Count;
            for (int i = 1; i < unitTableLenght; i++)
            {
                var unit = new Unit();
                unit.Name = (string) dataTable.Rows[i][0];
                if (dataTable.Rows[i][1] == DBNull.Value)
                {
                    unit.Parent = " ";
                }
                else
                {
                    unit.Parent = (string) dataTable.Rows[i][1];
                }
                _units.Add(unit);
            }

            
        }

        public void SetUnits(List<Unit> units)
        {
            _units = units;
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

        public void MakeAllStaffUnits(DataTable datatable)
        {
            var staffUnits = new List<StaffUnit>();

            for (var i = 1; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i][1] != DBNull.Value)
                {
                    var shtanEdenica = new StaffUnit
                    {
                        NameOfShtatnajaEdinica = (string) datatable.Rows[i][0],
                        PodrName = (string) datatable.Rows[i][1],
                        Rate = Convert.ToInt32(datatable.Rows[i][2])
                    };
                    Console.WriteLine(shtanEdenica.NameOfShtatnajaEdinica + " " + shtanEdenica.PodrName + " " +
                                      shtanEdenica.Rate);
                    staffUnits.Add(shtanEdenica);
                }
            }
            _staffUnits = staffUnits;
        }

        public void SetStaffUnitsToUnits(Unit unit)
        {
            
            foreach (var localStaffUnit in _staffUnits)
            {
                if (localStaffUnit.PodrName.Equals(unit.Name))
                {
                    unit.StaffUnits.Add(localStaffUnit);

                }
            }

        }

        public void Init()
        {
            foreach (var unit in _units)
            {
                SetStaffUnitsToUnits(unit);
            }
        }
    }
}