using ShtatRaspisanie.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShtatRaspisanie.Handlers
{
    public class DataHandler : IDataHandler
    {
        private List<StaffUnit> _staffUnits; 
        public void HandleData(List<Unit> units, List<StaffUnit> staffUnits)
        {

        }

        public ArrayList HandleUnitTable(List<Unit> listOfString, List<StaffUnit> staffUnits)
        {
            _staffUnits = staffUnits;
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
                    parentUnit.StaffUnits = GetStaffUnits(parentUnit.Name);
                    parentName = (string)item.Name;
                    units.Add(parentUnit);
                    parentUnitMain = parentUnit;
                }
                else if (item.Parent.Equals(parentUnitMain.Name))
                {
                    Unit unit = new Unit();
                    unit.Name = (string)item.Name;
                    unit.Parent = (string)item.Parent;
                    unit.StaffUnits = GetStaffUnits(unit.Name);
                    parentUnitMain.Child.Add(unit);
                    lastItem = unit;
                } else if (!ReferenceEquals(item.Parent, "") && lastItem != null && ReferenceEquals(item.Parent, lastItem.Name))
                {
                    item.StaffUnits = GetStaffUnits(item.Name);
                    lastItem.Child.Add(item);
                    Unit unit = new Unit();

                }
            }

            return units;
        }

        public List<StaffUnit> GetStaffUnits(string unitName)
        {
            List<StaffUnit> staffUnits = new List<StaffUnit>();
            foreach (var item in _staffUnits)
            {

                if (item.PodrName.Equals(unitName))
                {
                    staffUnits.Add(item);
                }

            }

            return staffUnits;
        }
    }
}