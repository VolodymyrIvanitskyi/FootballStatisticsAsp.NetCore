using FootballStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatistics.Statistics.Abstract
{
    public interface IStatistic
    {
        void Config();
        GoalsPerDay MaxGoalsPerDay(); // Максимальна кількість голів за день
        FootballTeam MaxGoals(); // Команда яка забила найбільшу кількість мячів
        FootballTeam MinMissBalls();// Команда яка пропустила найменшу кількість мячів
        FootballTeam BestScoredMissed(); // Краща команда забиті-пропущені.Пріорітет має команда з більшою кількістю забитих голів
    }
}
