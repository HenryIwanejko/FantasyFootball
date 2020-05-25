using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballSQLDB
{
    public interface ISQLiteRepository
    {
        int AddFantasyTeam(FantasyTeam fantasyTeam);

        int GetNextFantasyTeamId();

        Player GetPlayer(int playerId);

        List<Player> GetPlayers();

        List<Position> GetPositions();
    }
}

