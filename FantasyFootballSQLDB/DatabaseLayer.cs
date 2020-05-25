using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using Android.Support.CustomTabs;

namespace FantasyFootballSQLDB
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

        public List<Player> GetPlayers()
        {
            return dbConnection.Table<Player>().ToList();
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
    }
}
