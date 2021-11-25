using System.Collections.Generic;

namespace Web.Model.TupleModel
{
    public static class DisplayListOfPetsTuple
    {
        public static (string Name, bool HasPet, List<string> ListOfPets) ReturnTupleExamples(bool havePet)
        {
            string name = "George";
            bool hasPet = havePet;

            List<string> typeOfPets = new List<string>
            {
                "Dog",
                "Cat",
                "Fish"
            };

            return (name, hasPet, typeOfPets);
        }
    }
}
