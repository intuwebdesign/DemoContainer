using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Controller.Helpers;

namespace Web.Controller.Controllers
{
    public class ForLoopController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            var drinks = ListOfBeverages.TopAlcoholicDrinksUk();

            //Loop over all drinks and count number
            int counterOne = 0;
            for (var i = 0; i < drinks.Count; i++)
            {
                counterOne++;
            }

            //Loop over all drinks and stop at 10
            List<int> counterTwo = new List<int>();
            for (var i = 0; i < drinks.Count; i++)
            {
                if (i < 10)
                {
                    counterTwo.Add(i);
                }
                else
                {
                    break;
                }

            }

            DateTime currentDate    = DateTime.Now;
            DateTime pastDate       = DateTime.Now.AddYears(-121);
            List<int> yearList      = new List<int>();
            //Loop over current date and get the last 121 years in Asc order
            for (int counter = pastDate.Year; counter <= DateTime.Now.Year; counter++)
            {
                yearList.Add(counter);
            }



            DateTime dt = new DateTime(1, 1, 1);

            TimeSpan span = currentDate - pastDate;
            int years = (dt + span).Year;

            List<string> yearDesc = new List<string>();
            //Loop over current date and get the last 121 years in Desc order
            for (int i = 0; i < years; i++)
            {
                yearDesc.Add(currentDate.AddYears(-i).ToString("yyyy"));
            }

            //Get 6 random numbers
            List<int> lstNumbers = new List<int>();
            lstNumbers.Clear();

            for (int i = 0; i <= 6; i++)
            {
                lstNumbers.Add(GetRandomRangeNumber(1, 10));
            }


            //Loop descending
            List<int> lstNumbersDesc = new List<int>();
            for (int i = 10; i > 0; i--)
            {
                lstNumbersDesc.Add(i);
            }

            TempData["allDrinks"]       = counterOne;
            TempData["firstTenDrinks"]  = counterTwo;
            TempData["LoopYears"]       = yearList;
            TempData["LoopYearsDesc"]   = yearDesc;
            TempData["LoopNumbersDesc"] = lstNumbersDesc;

            //Because we start at year 1 for the Gregorian calendar, we must subtract a year here.
            TempData["YearsText"]       = $"There are {yearList.Count - 1} years between {pastDate.Year} and {DateTime.Now.Year}";
            TempData["RandNumbers"]     = lstNumbers.OrderBy(x => x);

            //Get 6 random numbers and check for duplicates, if any found re-run the loop
            TempData["RandomNoDups"]    = GetRandonNUmbersNoDuplicates().OrderBy(x => x);

            return View();
        }

        private static readonly Random GetNewRandomNumber = new Random();

        private static int GetRandomRangeNumber(int min, int max)
        {
            lock (GetNewRandomNumber)
            {
                return GetNewRandomNumber.Next(min, max);
            }
        }

        private static List<int> GetRandonNUmbersNoDuplicates()
        {

            List<int> lstNumbers = new List<int>();

            while (true)
            {
                for (int i = 0; i <= 6; i++)
                {
                    lstNumbers.Add(GetRandomRangeNumber(1, 10));

                }
                //Check for dups
                int lstdups = lstNumbers.GroupBy(x => x).Sum(x => x.Count() - 1);
                if (lstdups > 0)
                {
                    //If we have dupliacte numbers, clear the list and start again
                    lstNumbers.Clear();
                }
                else
                {
                    break;
                }
            }
            return lstNumbers;
        }
    }
}
