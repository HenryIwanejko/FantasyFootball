using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballShared
{
    public class PickTeamCompletionService
    {

        public PickTeamCompletionService()
        {
 
        }

        public decimal CalculateTeamCost(List<Player> players)
        {
            decimal total = 0;
            foreach (var player in players)
            {
                total += player.Price;
            }
            return total;
        }

        public decimal CalculateAveragePlayerCost(List<Player> players)
        {
            decimal totalTeamCost = CalculateTeamCost(players);
            return totalTeamCost / players.Count;
        }

        public decimal CalculateTeamBudget(List<Player> players)
        {
            decimal averagePlayer = CalculateAveragePlayerCost(players);
            return (averagePlayer * 5) + 1;
        }

        public string VerifyTeamBudget(decimal budget, decimal totalCost)
        {
            if (budget >= totalCost)
            {
                return "";
            }
            return "Exceeded budget";
        }
    }
}
