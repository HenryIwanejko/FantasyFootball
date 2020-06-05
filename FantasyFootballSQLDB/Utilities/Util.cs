using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FantasyFootballShared.Utilities
{
    public class Util
    {
        public static bool ValidateText(params string[] args)
        {
            foreach (var arg in args)
            {
                if (String.IsNullOrEmpty(arg))
                {
                    return false;
                }
            }
            return true;
        }

        public static decimal CalculateTeamCost(Dictionary<FantasyTeam, List<Player>> userTeams, FantasyTeam currentTeam)
        {
            decimal total = 0;
            List<Player> userPlayers = userTeams.FirstOrDefault(x => x.Key.FantasyTeamID == currentTeam.FantasyTeamID).Value;
            foreach (var player in userPlayers)
            {
                total += player.Price;
            }
            return total;
        }
        // move into service class
        public static decimal CalculateTeamCost(List<Player> players)
        {
            decimal total = 0;
            foreach (var player in players)
            {
                total += player.Price;
            }
            return total;
        }

        public static decimal CalculateAveragePlayerCost(List<Player> players)
        {
            decimal totalTeamCost = Util.CalculateTeamCost(players);
            return totalTeamCost / players.Count;
        }

        public static decimal CalculateTeamBudget(List<Player> players)
        {
            decimal averagePlayer = CalculateAveragePlayerCost(players);
            return (averagePlayer * 5) + 1;
        }

        public static string VerifyTeamBudget(decimal budget, decimal totalCost)
        {
            if (budget >= totalCost)
            {
                return "";
            }
            return "Exceeded budget";
        }
    }
}