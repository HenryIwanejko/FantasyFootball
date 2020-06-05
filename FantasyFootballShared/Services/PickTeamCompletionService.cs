using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballShared
{
    public class PickTeamCompletionService
    {
        ISQLiteRepository _sqlLiteRepository;

        public PickTeamCompletionService()
        {
            _sqlLiteRepository = new SQLiteRepository();
        }
    }
}
