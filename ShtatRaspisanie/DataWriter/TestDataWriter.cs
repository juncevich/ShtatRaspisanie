using System.Collections.Generic;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.DataWriter
{
    public class TestDataWriter : IDataWriter
    {
        public void WriteData(List<Unit> units)
        {
            foreach (ParentUnit item in units)
            {
                item.Display(item);
            }
        }
    }
}