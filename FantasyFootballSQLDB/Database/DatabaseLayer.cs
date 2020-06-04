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
        private SQLiteConnection dbConnection;

        public DatabaseLayer(string dbLocation)
        {
            dbConnection = new SQLiteConnection(dbLocation);
        }

        public Player GetPlayer(int playerId)
        {
            return new Player();
        }

        public List<Player> GetPlayers(int positionId)
        {
            return dbConnection.Table<Player>().Where(player => player.PositionID == positionId).ToList();
        }

        public List<FantasyTeam> GetFantasyTeams()
        {
            return dbConnection.Table<FantasyTeam>().ToList();
        }

        public int GetNextFantasyTeamId()
        {
            return dbConnection.Table<FantasyTeam>().DefaultIfEmpty().Max(fTeam => fTeam == null ? 0 : fTeam.FantasyTeamID);
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
    }
}
