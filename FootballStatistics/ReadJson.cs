using FootballStatistics.Models;
using FootballStatistics.Statistics.Realization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatistics
{
    public static class ReadJson
    {
        public static Statistic ReadFile(Statistic statictic, string path)
        {
            statictic.League = JsonConvert.DeserializeObject<League>(File.ReadAllText(path));
            return statictic;
        }
    }
}
