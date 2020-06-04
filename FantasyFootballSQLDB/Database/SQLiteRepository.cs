using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FantasyFootballShared
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

        public List<FantasyTeam> GetFantasyTeams()
        {
            return dbLayer.GetFantasyTeams();
        }

        public int GetNextFantasyTeamId()
        {
            return dbLayer.GetNextFantasyTeamId();
        }

        public Player GetPlayer(int playerId)
        {
            return dbLayer.GetPlayer(playerId);
        }

        public List<Player> GetPlayers(int positionId)
        {
            return dbLayer.GetPlayers(positionId);
        }

        public List<Position> GetPositions()
        {
            return dbLayer.GetPositions();
        }

        public int GetPostionId(string positionName)
        {
            return dbLayer.GetPostionId(positionName);
        }

        public PremierTeam GetPremierTeam(int id)
        {
            return dbLayer.GetPremierTeam(id);
        }
    }
}
