using System.Data;

namespace ShtatRaspisanie.DataReader
{
    public interface IParser
    {
        DataTable GetUnitList();
        DataTable GetStaffUnitList();

    }
}