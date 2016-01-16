using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using DocumentFormat.OpenXml.Wordprocessing;

namespace ShtatRaspisanie.Entities
{
    public sealed class UnitWithOneNestedGrade:IUnit
    {

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public List<IUnit> Child
        {
            get { return _child; }
            set { _child = value; }
        }
        public List<IStaffUnit> StaffUnits
        { 

            get { return _staffUnits; }
            set { _staffUnits = value; }
        }

        public List<IStaffUnit> ChildStaffUnits
        {
            get { return _childStaffUnits; }
            set { _childStaffUnits = value; }
        }
        private string _name;
        private string _parent;
        private List<IUnit> _child;
        private List<IStaffUnit> _staffUnits;
        private List<IStaffUnit> _childStaffUnits { get; set; }

        public void InitUnit()
        {

        }

        public UnitWithOneNestedGrade(string name, string parent)
        {
            _name = name;
            _parent = parent;
        }

        public UnitWithOneNestedGrade(string name, string parent, List<IUnit> child)
        {
            _name = name;
            _parent = parent;
            _child = child;
        }

        public UnitWithOneNestedGrade(string name, string parent, List<IUnit> child, List<IStaffUnit> staffUnits)
        {   
            _name = name;
            _parent = parent;
            _child = child;
            _staffUnits = staffUnits;
        }

        public void AddChild(IUnit unit)
        {
            Child.Add(unit);
        }

        public void AddStaffUnit(StaffUnit staffUnit)
        {
            StaffUnits.Add(staffUnit);
        }

        public void DisplayUnit()
        {
           
            Console.WriteLine(Name + " " + Parent);
            foreach (var staffUnit in StaffUnits)
            {
                staffUnit.DisplayStaffUnit();
            }

            foreach (var child in Child)
            {
                Console.WriteLine(child.Name + " " + child.Parent);
                foreach (var childStaffUnit in StaffUnits.Cast<StaffUnit>())
                {
                    childStaffUnit.DisplayStaffUnit();
                }
            }
        }


    }
}