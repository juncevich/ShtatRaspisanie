using System.Data;

namespace ShtatRaspisanie.DataReader
{
    public interface IParser
    {
        DataTable GetUnitData();
        DataTable GetStuffUnitData();

    }
}