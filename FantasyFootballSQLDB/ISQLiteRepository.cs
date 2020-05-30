using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballSQLDB
{
    public interface ISQLiteRepository
    {
        List<FantasyTeam> GetFantasyTeams();

        int AddFantasyTeam(FantasyTeam fantasyTeam);

        int GetNextFantasyTeamId();

        Player GetPlayer(int playerId);

        List<Player> GetPlayers(int positionId);

        List<Position> GetPositions();

        PremierTeam GetPremierTeam(int id);

        int GetPostionId(string positionName);
    }
}

