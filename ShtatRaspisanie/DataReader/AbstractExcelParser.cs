using System.Data;

namespace ShtatRaspisanie.DataReader
{
    public abstract class AbstractExcelParser:IParser
    {
        public abstract DataTable GetUnitData(string filename);
        public abstract DataTable GetUnitData();

        public abstract DataTable GetStuffUnitData();

    }
}