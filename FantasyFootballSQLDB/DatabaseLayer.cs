using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;

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
    }
}
