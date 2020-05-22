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

        public string ManagerFirstName { get; set; }

        public FantasyTeam()
        {

        }

        public FantasyTeam(int fantasyTeamId, string fantasyTeamName, string managerSurname, string managerFirstName)
        {
            FantasyTeamID = fantasyTeamId;
            FantasyTeamName = fantasyTeamName;
            ManagerSurname = managerSurname;
            ManagerFirstName = managerFirstName;
        }
    }
}
