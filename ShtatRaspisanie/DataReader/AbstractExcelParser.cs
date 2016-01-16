using System.Collections.Generic;
using System.Data;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.DataReader
{
    public abstract class AbstractExcelParser:IParser
    {
        public abstract DataTable GetUnitList(string fileName);
        public abstract DataTable GetUnitList();

        public abstract List<StaffUnit> GetStuffUnitList(string fileName);
        public abstract DataTable GetStuffUnitList();

    }
}