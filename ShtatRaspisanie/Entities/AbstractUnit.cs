using System.Collections.Generic;

namespace ShtatRaspisanie.Entities
{
    public abstract class AbstractUnit:IUnit
    {
        private string name;
        private string nameparent;
        private List<IUnit> child;
        private List<IStaffUnit> staffUnits;

        public abstract void InitUnit();
        public abstract void DisplayUnit();
    }
}