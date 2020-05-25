using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FantasyFootballSQLDB
{
    public class SQLiteRepository: ISQLiteRepository
    {
        private readonly DatabaseLayer dbLayer = null;

        public SQLiteRepository()
        {
            dbLayer = new DatabaseLayer(DatabasePathOfFile);
        }

        private string DatabasePathOfFile
        {
            get
            {
                string dbName = "db.sqlite";
                string dbfolderLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(dbfolderLocation, dbName);
            }
        }

        public int AddFantasyTeam(FantasyTeam fantasyTeam)
        {
            return dbLayer.AddFantasyTeam(fantasyTeam);
        }

        public int GetNextFantasyTeamId()
        {
            return dbLayer.GetNextFantasyTeamId();
        }

        public Player GetPlayer(int playerId)
        {
            return dbLayer.GetPlayer(playerId);
        }

        public List<Player> GetPlayers()
        {
            return dbLayer.GetPlayers();
        }

        public List<Position> GetPositions()
        {
            return dbLayer.GetPositions();
        }
    }
}
