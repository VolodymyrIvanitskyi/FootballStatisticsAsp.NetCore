using FootballStatistics.Models;
using FootballStatistics.Statistics.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatistics.Statistics.Realization
{
    public class Statistic : IStatistic
    {
        public static List<GoalsPerDay> DateOfMatch { get; set; } // Всі дати матчів трьох ліг 
        static Statistic() 
        { 
            DateOfMatch = new List<GoalsPerDay>(); 
        }

        
        public League League { get; set; }

        public List<FootballTeam> teams { get; set; } //Список всіх команд ліги

        public Statistic()
        {
            teams = new List<FootballTeam>();
        }

        public void Config()
        {
            //teams = new List<FootballTeam>();
            //DateOfMatch = new List<GoalsPerDay>();

            for (int i = 0; i < League.Matches.Length; i++)
            {
                FootballTeam team = new FootballTeam();
                team.Name = League.Matches[i].team1.ToString();
                if (!teams.Contains(team))
                    teams.Add(team);// Записуємо назви всіх команд

                // На випадок, якщо команда грала всі матчі тільки на виїзді
                /*team.Name = League1.Matches[i].team2.ToString();
                if (!teams.Contains(team))
                    teams.Add(team);*/

                GoalsPerDay goalsPerDay = new GoalsPerDay();
                goalsPerDay.Date = League.Matches[i].date;
                if (!DateOfMatch.Contains(goalsPerDay))
                    DateOfMatch.Add(goalsPerDay);//Записуємо всі дні коли були матчі

            }

            // Підрахунок кількості голів щодня
            foreach (var date in DateOfMatch)
            {
                //date.CountOfGoals = League.Matches.Where(team => team.date == date.Date).Sum(match => match?.score?.ft[1] ?? 0 + match?.score?.ft[0] ?? 0));
                int counts1 = League.Matches.Where(team => team.date == date.Date).Sum(match => match?.score?.ft[1] ?? 0);
                int counts2 = League.Matches.Where(team => team.date == date.Date).Sum(match => match?.score?.ft[0] ?? 0);
                date.CountOfGoals += counts1 + counts2;
            }

            //Підрахунок забитих і пропущених голів для кожної команди
            foreach (var team in teams)
            {
                //Кількість забитих м'ячів вдома
                int countOfGoalsAtHome = League.Matches.Where(match => match.team1 == team.Name).Sum(match => match?.score?.ft[0] ?? 0);
                //Кількість забитих м'ячів на виїзді
                int countofGoalsOnTheRoad = League.Matches.Where(match => match.team2 == team.Name).Sum(match => match?.score?.ft[1] ?? 0);

                team.GoalsScored = countOfGoalsAtHome + countofGoalsOnTheRoad;

                //Кількість пропущених м'ячів вдома
                int countOfMissGoalsAtHome = League.Matches.Where(match => match.team1 == team.Name).Sum(match => match?.score?.ft[1] ?? 0);
                //Кількість пропущених м'ячів на виїзді
                int countofMissGoalsOnTheRoad = League.Matches.Where(match => match.team2 == team.Name).Sum(match => match?.score?.ft[0] ?? 0);

                team.MissedBalls = countOfMissGoalsAtHome + countofMissGoalsOnTheRoad;

            }
        }

        // Краща команда забиті-пропущені.Пріорітет має команда з більшою кількістю забитих голів
        public FootballTeam BestScoredMissed()
        {
            FootballTeam bestTeam = teams.OrderBy(team => (team.GoalsScored - team.MissedBalls)).ThenBy(team => team.GoalsScored).LastOrDefault();
            return bestTeam;
        }

        // Команда яка забила найбільшу кількість мячів
        public FootballTeam MaxGoals()
        {
            int maxGoals = teams.Max(team => team.GoalsScored);
            int maxGoalsIndex = teams.FindIndex(team => team.GoalsScored == maxGoals);
            return teams[maxGoalsIndex];
        }

        //Максимальна кількість голів за день
        public GoalsPerDay MaxGoalsPerDay()
        {
            int maxGoals = DateOfMatch.Max(date => date.CountOfGoals);
            int index = DateOfMatch.FindIndex(i => i.CountOfGoals == maxGoals);
            return DateOfMatch[index];
        }

        // Команда яка пропустила найменшу кількість мячів
        public FootballTeam MinMissBalls()
        {
            int minMissBalls = teams.Min(team => team.MissedBalls);
            int missBallsIndex = teams.FindIndex(team => team.MissedBalls == minMissBalls);
            return teams[missBallsIndex];
        }
    }
}
