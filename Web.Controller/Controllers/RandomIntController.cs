using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Web.Mvc;

namespace Web.Controller.Controllers
{
    public class RandomIntController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            ViewBag.LineOne    = "";
            ViewBag.LineTwo    = "";
            ViewBag.LineThree  = "";
            ViewBag.LineFour   = "";

            return View();
        }

        [HttpPost]
        public ActionResult GetLotteryNumbers()
        {
            const int min       = 1;
            const int max       = 60;
            const int rndMax    = 61;

            ViewBag.LineOne         = NewRandom.LoopLotteryNumbers(5, min, rndMax, 1).OrderBy(x=>x);
            ViewBag.LineTwo         = NewRandom.LoopLotteryNumbers(5, min, rndMax, 2).OrderBy(x => x);
            ViewBag.LineThree       = NewRandom.LoopLotteryNumbers(5, min, max, 3).OrderBy(x => x);
            ViewBag.LineFour        = NewRandom.LoopLotteryNumbers(5, min, max, 3).OrderBy(x => x);


            return View("~/Views/RandomInt/Home.cshtml");
        }
    }

    public static class NewRandom
    {
        public static List<int> LoopLotteryNumbers(int numberOfLoops, int min, int max, int type)
        {
            
            List<int> lstNumbers = new List<int>();
            lstNumbers.Clear();

            while (true)
            {
                for (int i = 0; i <= numberOfLoops; i++)
                {
                    switch (type)
                    {
                        case 1:
                            Thread.Sleep(200);//Comment this line to see what happens
                            lstNumbers.Add(GetRandomNumber(min, max));
                            break;
                        case 2:
                            lstNumbers.Add(GetRandomRangeNumber(min, max));
                            break;
                        case 3:
                            lstNumbers.Add(GetRandomNumberCrypto(min, max));
                            break;
                    }
                }
                int lstdups = lstNumbers.GroupBy(x => x).Sum(x=>x.Count() - 1);
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

        private static int GetRandomNumber(int min, int max)
        {
            //Very bad for anything slightly complex
            Random rnd = new Random();
            int lotteryNumber = rnd.Next(min, max);

            return lotteryNumber;
        }

        private static readonly Random GetNewRandomNumber = new Random();

        private static int GetRandomRangeNumber(int min, int max)
        {
            lock (GetNewRandomNumber)
            {
                return GetNewRandomNumber.Next(min, max);
            }
        }

        private static int GetRandomNumberCrypto(int min, int max)
        {
            int result;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                int value = BitConverter.ToInt32(randomNumber, 0);
                result = value;

                var prop = (max - min + 0d) / int.MaxValue;
                result = Math.Abs((int) Math.Round(result * prop));
                result += min;
            }

            return result;
        }
    }
}
