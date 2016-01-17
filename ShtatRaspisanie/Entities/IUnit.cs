using System.Collections.Generic;

namespace ShtatRaspisanie.Entities
{
    public interface IUnit
    {
        string Name { get; set; }
        string Parent { get; set; }
        List<IUnit> Child { get; set; }
        List<IStaffUnit> StaffUnits { get; set; }
        List<IStaffUnit> ChildStaffUnits { get; set; }

        void InitUnit();

        void DisplayUnit();
    }
}