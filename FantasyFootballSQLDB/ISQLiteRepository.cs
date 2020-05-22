using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballSQLDB
{
    public interface ISQLiteRepository
    {

        Player GetPlayer(int playerId);

        List<Player> GetPlayers();
    }
}

