using System.Collections.Generic;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.DataWriter
{
    public interface IDataWriter
    {
        void WriteData(List<Unit> units);
    }
}