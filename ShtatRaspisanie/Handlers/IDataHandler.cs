using System.Collections.Generic;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.Handlers
{
    public interface IDataHandler
    {
        List<Unit> HandleUnitTable(List<Unit> listOfString, List<StaffUnit> staffUnits);
    }
}