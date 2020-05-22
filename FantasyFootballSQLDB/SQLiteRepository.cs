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
                string fileName = "db.sqlite";
                string folderLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(folderLocation, fileName);
            }
        }

        public Player GetPlayer(int playerId)
        {
            return dbLayer.GetPlayer(playerId);
        }

        public List<Player> GetPlayers()
        {
            return dbLayer.GetPlayers();
        }
    }
}
