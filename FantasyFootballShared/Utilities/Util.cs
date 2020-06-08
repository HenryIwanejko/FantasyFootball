using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Print;
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

        public static bool ValidateDecimal(string value, ref decimal result)
        {
            if (decimal.TryParse(value, out result))
            {
                return true;
            }
            return false;
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
     
    }
}