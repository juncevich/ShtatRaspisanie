namespace ShtatRaspisanie.Entities
{
    public class StaffUnit:IStaffUnit
    {
        public  string Name { get; set; }
        public  string PodrName { get; set; }
        public  int Rate { get; set; }
        

        public StaffUnit(string name, string podrName, int rate)
        {
            Name = name;
            PodrName = podrName;
            Rate = rate;
        }

        public StaffUnit()
        {
        }

        public void DisplayStaffUnit()
        {
            System.Console.WriteLine(Name + " " + PodrName + " " + Rate);
        }


    }
}