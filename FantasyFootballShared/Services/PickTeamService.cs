using System;
using System.Collections.Generic;
using System.Text;
using Android.Widget;
using FantasyFootballShared.Utilities;
using Java.Security;
using FantasyFootballShared;
using System.Collections;
using System.Linq;

namespace FantasyFootballShared
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

        public List<FantasyTeam> GetFantasyTeams()
        {
            return _sqlLiteRepository.GetFantasyTeams();
        }

        public List<Position> GetPositions()
        {
            return _sqlLiteRepository.GetPositions();
        }

        public Dictionary<int, KeyValuePair<FantasyTeam, List<Player>>> PackageUpData(Dictionary<FantasyTeam, List<Player>> userTeams)
        {
            Dictionary<int, KeyValuePair<FantasyTeam, List<Player>>> dto = new Dictionary<int, KeyValuePair<FantasyTeam, List<Player>>>();
            for (int i = 1; i < userTeams.Count + 1; i++)
            {
                KeyValuePair<FantasyTeam, List<Player>> team = userTeams.FirstOrDefault(x => x.Key.FantasyTeamID == i);
                dto.Add(i - 1, team);
            }
            return dto;
        }

    }
}
