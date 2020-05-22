using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FantasyFootballSQLDB
{
    [Table("FantasyTeams")]
    public class FantasyTeam
    {
        [PrimaryKey, AutoIncrement]
        public int FantasyTeamID { get; set; }
        
        public string FantasyTeamName { get; set; }

        public string ManagerSurname { get; set; }

        public string ManagerFirstname { get; set; }

        public FantasyTeam()
        {

        }
    }
}
