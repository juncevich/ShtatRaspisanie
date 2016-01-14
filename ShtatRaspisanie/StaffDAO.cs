using System.Collections;
using System.Collections.Generic;

namespace ShtatRaspisanie
{
    public class StaffDao
    {
        public static List<Unit> GetAllUnits(Hashtable dataList)
        {
            var units = new List<Unit>();
            foreach (DictionaryEntry value in dataList)
            {
                Unit unit = new Unit();
                unit.Name = value.Key as string;
                unit.Parent = value.Value as string;
                units.Add(unit);

            }

            return units;
        }

        public static void SetChildToUnitList()
        {
            
        }
        
         
    }
}