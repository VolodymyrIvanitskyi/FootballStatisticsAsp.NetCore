using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatistics.Models
{
    public class FootballTeam : IEquatable<FootballTeam>
    {
        public string Name { get; set; }
        public int GoalsScored { get; set; }
        public int MissedBalls { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            FootballTeam objAsFootballTeam = obj as FootballTeam;
            if (objAsFootballTeam == null) return false;
            else return Equals(objAsFootballTeam);
        }

        public bool Equals(FootballTeam other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }

        /*public void PrintInformation()
        {
            Console.WriteLine(Name + ", кількість забитих м'ячів: " + GoalsScored + ", кількість пропущених: " + MissedBalls);
        }*/
    }
}
