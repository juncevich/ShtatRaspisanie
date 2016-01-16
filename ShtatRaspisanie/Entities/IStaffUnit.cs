namespace ShtatRaspisanie.Entities
{
    public interface IStaffUnit
    {
        string Name { get; set; }

        string PodrName { get; set; }

        int Rate { get; set; }

        void DisplayStaffUnit();
    }
}