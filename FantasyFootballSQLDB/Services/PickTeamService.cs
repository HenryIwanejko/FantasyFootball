using System;
using System.Collections.Generic;
using System.Text;
using Android.Widget;
using FantasyFootballShared.Utilities;
using Java.Security;
using FantasyFootballShared;

namespace FantasyFootballShared.Services
{
    public class PickTeamService
    {
        private readonly ISQLiteRepository _sqlLiteRepository;

        public PickTeamService()
        {
            _sqlLiteRepository = new SQLiteRepository();
        }
        
        public bool ValidateFantasyTeams(List<FantasyTeam> teams)
        {
            return teams == null || teams.Count != 2;
        }

        public void InitialiseUserTeamData(List<FantasyTeam> teams, Dictionary<FantasyTeam, List<Player>> userTeams)
        {
            foreach (var team in teams)
            {
                userTeams.Add(team, new List<Player>());
            }
        }

        private int SwitchPlayerIndex(int currentTeamIndex)
        {
            return currentTeamIndex == 0 ? 1 : 0;
        }

        public FantasyTeam SetTeamDetails(List<FantasyTeam> teams, FantasyTeam currentTeam, ref int positionCounter, ref int selectionCounter)
        {
            int teamIndex = SwitchPlayerIndex(teams.IndexOf(currentTeam));
            if (selectionCounter == 1)
            {
                if (positionCounter == 1 || positionCounter == 3)
                {
                    teamIndex = SwitchPlayerIndex(teamIndex);
                }
                selectionCounter = 0;
            }
            else
            {
                selectionCounter += 1;
            }
            return teams[teamIndex];
        }

        public List<Player> RetrieveAndCleansePlayerData(Dictionary<FantasyTeam, List<Player>> userTeams, Position position)
        {
            List<Player> players = _sqlLiteRepository.GetPlayers(position.PositionID);
            foreach (var team in userTeams.Values)
            {
                Dictionary<int, int> premierTeams = new Dictionary<int, int>();
                foreach (var player in team)
                {
                    if (premierTeams.ContainsKey(player.PremierTeamID))
                    {
                        premierTeams[player.PremierTeamID] += 1;
                    }
                    else
                    {
                        premierTeams.Add(player.PremierTeamID, 1);
                    }
                    players.RemoveAll(x => x.PlayerID == player.PlayerID);
                    if (premierTeams[player.PremierTeamID] >= 2)
                    {
                        players.RemoveAll(x => x.PremierTeamID == player.PremierTeamID);
                    }
                }
            }
            return players;
        }
    }
}
