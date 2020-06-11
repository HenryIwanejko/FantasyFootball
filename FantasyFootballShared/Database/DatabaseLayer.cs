using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Android.Support.CustomTabs;
using Javax.Crypto.Spec;

namespace FantasyFootballShared
{
    public class DatabaseLayer
    {
        private readonly SQLiteConnection dbConnection;

        public DatabaseLayer(string dbLocation)
        {
            dbConnection = new SQLiteConnection(dbLocation);
        }

        public List<Player> GetAllPlayers()
        {
            return dbConnection.Table<Player>().ToList();
        }

        public List<Player> GetPlayers(int positionId)
        {
            return dbConnection.Table<Player>().Where(player => player.PositionID == positionId).ToList();
        }

        public List<FantasyTeam> GetFantasyTeams()
        {
            return dbConnection.Table<FantasyTeam>().ToList();
        }

        public int AddFantasyTeam(FantasyTeam fantasyTeam)
        {
            return dbConnection.Insert(fantasyTeam);
        }

        public List<Position> GetPositions()
        {
            return dbConnection.Table<Position>().ToList();
        }

        public int GetPostionId(string positionName)
        {
            return dbConnection.Table<Position>().Where(position => position.PositionName == positionName).FirstOrDefault().PositionID;
        }

        public PremierTeam GetPremierTeam(int id)
        {
            return dbConnection.Table<PremierTeam>().Where(premTeam => premTeam.PremierTeamID == id).FirstOrDefault();
        }

        public Position GetPosition(int positionId)
        {
            return dbConnection.Table<Position>().Where(Position => Position.PositionID == positionId).FirstOrDefault();
        }

        public int ResetFantasyTeams()
        {
            List<FantasyTeam> teams = GetFantasyTeams();
            if (teams != null && teams.Count > 0)
            {
                foreach (var team in teams)
                {
                    int result = dbConnection.Table<FantasyTeam>().Delete(x => x.FantasyTeamID == team.FantasyTeamID);
                    if (result != 1)
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }

        public int DeletePlayer(int playerId)
        {
            return dbConnection.Table<Player>().Delete(x => x.PlayerID == playerId);
        }

        public List<PremierTeam> GetPremierTeams()
        {
            return dbConnection.Table<PremierTeam>().ToList();
        }

        public int AddPlayer(Player player)
        {
            return dbConnection.Insert(player);
        }

        public int UpdatePlayer(Player player)
        {
            return dbConnection.Update(player);
        }

        public int DeletePremierTeam(int premierTeamId)
        {
            return dbConnection.Table<PremierTeam>().Delete(x => x.PremierTeamID == premierTeamId);
        }

        public int AddPremierTeam(PremierTeam premierTeam)
        {
            return dbConnection.Insert(premierTeam);
        }

        public int UpdatePremierTeam(PremierTeam premierTeam)
        {
            return dbConnection.Update(premierTeam);
        }
    }
}
