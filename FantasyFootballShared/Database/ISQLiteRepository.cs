using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballShared
{
    public interface ISQLiteRepository
    {
        List<FantasyTeam> GetFantasyTeams();

        int AddFantasyTeam(FantasyTeam fantasyTeam);

        int GetNextFantasyTeamId();

        List<Player> GetPlayers(int positionId);

        List<Position> GetPositions();

        PremierTeam GetPremierTeam(int id);

        int GetPostionId(string positionName);

        Position GetPosition(int positionId);

        int ResetFantasyTeams();

        List<Player> GetAllPlayers();

        int DeletePlayer(int playerId);

        List<PremierTeam> GetPremierTeams();

        int AddPlayer(Player player);

        int UpdatePlayer(Player player);

        int DeletePremierTeam(int premierTeamId);
    }
}

