using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatistics.Models
{
    public class GoalsPerDay : IEquatable<GoalsPerDay>
    {
        public DateTime Date { get; set; }
        public int CountOfGoals { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            GoalsPerDay objAsGoalsPerDay = obj as GoalsPerDay;
            if (objAsGoalsPerDay == null) return false;
            else return Equals(objAsGoalsPerDay);
        }
        public bool Equals(GoalsPerDay other)
        {
            if (other == null) return false;
            return (this.Date.Equals(other.Date));
        }
    }
}
