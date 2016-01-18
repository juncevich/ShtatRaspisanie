using ShtatRaspisanie.Entities;
using System.Collections.Generic;

namespace ShtatRaspisanie.DataWriter
{
    public interface IDataWriter
    {
        void WriteData(List<Unit> units);
    }
}