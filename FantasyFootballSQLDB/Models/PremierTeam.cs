using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FantasyFootballSQLDB
{
    [Table("PremierTeams")]
    public class PremierTeam
    {
        [PrimaryKey, AutoIncrement]
        public int PremierTeamID { get; set; }

        public string PremierTeamName { get; set; }

        public PremierTeam()
        {

        }

        public PremierTeam(int premierTeamId, string premierTeamName)
        {

        }
    }
}
