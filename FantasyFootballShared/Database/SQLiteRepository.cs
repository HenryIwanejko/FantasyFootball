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

        public List<Player> GetPlayers(int positionId)
        {
            return dbLayer.GetPlayers(positionId);
        }

        public Position GetPosition(int positionId)
        {
            return dbLayer.GetPosition(positionId);
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

        public int ResetFantasyTeams()
        {
            return dbLayer.ResetFantasyTeams();
        }

        public List<Player> GetAllPlayers()
        {
            return dbLayer.GetAllPlayers();
        }

        public int DeletePlayer(int playerId)
        {
            return dbLayer.DeletePlayer(playerId);
        }

        public List<PremierTeam> GetPremierTeams()
        {
            return dbLayer.GetPremierTeams();
        }

        public int AddPlayer(Player player)
        {
            return dbLayer.AddPlayer(player);
        }


        public int UpdatePlayer(Player player)
        {
            return dbLayer.UpdatePlayer(player);
        }

        public int DeletePremierTeam(int premierTeamId)
        {
            return dbLayer.DeletePremierTeam(premierTeamId);
        }

        public int AddPremierTeam(PremierTeam premierTeam)
        {
            return dbLayer.AddPremierTeam(premierTeam);
        }

        public int UpdatePremierTeam(PremierTeam premierTeam)
        {
            return dbLayer.UpdatePremierTeam(premierTeam);
        }
    }
}
