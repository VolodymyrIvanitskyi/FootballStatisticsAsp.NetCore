using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FootballStatistics.Models;
using FootballStatistics.Statistics.Realization;
using Newtonsoft.Json;
using System.IO;
using FootballStatistics.Statistics.Abstract;

namespace FootballStatistics.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            statistic1 = new Statistic();
            statistic2 = new Statistic();
            statistic3 = new Statistic();

            statistic1 = ReadJson.ReadFile(statistic1, "en.1.json");
            statistic2 = ReadJson.ReadFile(statistic2, "en.2.json");
            statistic3 = ReadJson.ReadFile(statistic3, "en.3.json");

           
            foreach(var date in Statistic.DateOfMatch)
            {
                date.CountOfGoals = 0;
            }
            
            statistic1.Config();
            statistic2.Config();
            statistic3.Config();

            goalsPerDay = new GoalsPerDay();
            goalsPerDay = statistic1.MaxGoalsPerDay();
        }
        public GoalsPerDay goalsPerDay;
        public Statistic statistic1;
        public Statistic statistic2;
        public Statistic statistic3;

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult League1()
        {

            return View("Show",statistic1);
        }
        public IActionResult League2()
        {

            return View("Show", statistic2);
        }
        public IActionResult League3()
        {

            return View("Show", statistic3);
        }

        public IActionResult ShowMaxGoalsPerDay()
        {
            return View("ShowMaxGoalsPerDay", goalsPerDay);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
