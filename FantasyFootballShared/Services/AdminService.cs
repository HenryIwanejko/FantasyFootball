using System;
using System.Collections.Generic;
using System.Text;
using FantasyFootballShared;

namespace FantasyFootballShared
{
    public class AdminService
    {
        ISQLiteRepository _sqlLiteRepository;

        public AdminService()
        {
            _sqlLiteRepository = new SQLiteRepository();
        }

        public bool ResetFantasyTeams()
        {
            int queryResponse = _sqlLiteRepository.ResetFantasyTeams();
            if (queryResponse != 1)
            {
                return false;
            }
            return true;
        }
    }
}
