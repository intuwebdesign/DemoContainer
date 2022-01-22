using System.Collections.Generic;

namespace Web.Model.RemoveDupsFromList
{
    public class DisplayNoDupsFromList
    {
        public IEnumerable<ListOfTestData> DisplayData { get; set; }
    }
    public class ListOfTestData
    {
        public ListOfTestData(string name, int age)
        {
            Name    = name;
            Age     = age;
        }

        public ListOfTestData(){}
        public string Name      { get; set; }
        public int Age          { get; set; }
    }

    public class ListOfTestDataNoDups
    {
        public List<ListOfTestData> ListDataNoDups()
        {
            List<ListOfTestData> data = new List<ListOfTestData>
            {
                new ListOfTestData("Peter", 24),
                new ListOfTestData("Harry", 30),
                new ListOfTestData("Luke", 15),
                new ListOfTestData("Helen", 21),
                new ListOfTestData("Jemma", 17),
                new ListOfTestData("George", 50),
                new ListOfTestData("George", 51)

            };
            return data;
        }
    }

    public class ListOfTestDataWithDups
    {
        public List<ListOfTestData> ListDataWithDups()
        {
            List<ListOfTestData> data = new List<ListOfTestData>
            {
                new ListOfTestData("Peter", 24),
                new ListOfTestData("Charlie", 33),
                new ListOfTestData("Graham", 33),
                new ListOfTestData("George", 50),
                new ListOfTestData("George", 50),
                new ListOfTestData("George", 49),
                new ListOfTestData("Lisa", 33),
                new ListOfTestData("Mary", 45),
                new ListOfTestData("Mary", 45)
            };
            return data;
        }
    }
}