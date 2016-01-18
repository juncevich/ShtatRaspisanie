using ShtatRaspisanie.Entities;
using System.Collections.Generic;

namespace ShtatRaspisanie.Handlers
{
    public interface IDataHandler
    {
        List<Unit> HandleUnitTable(List<Unit> listOfString, List<StaffUnit> staffUnits);
    }
}