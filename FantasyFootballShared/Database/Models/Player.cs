using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyFootballShared
{
    [Table("Players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int PlayerID { get; set; }

        public string Surname { get; set; }

        public string Firstname { get; set; }

        public int PremierTeamID { get; set; }

        public int FantasyTeamID { get; set; }

        public int PositionID { get; set; }

        public decimal Price { get; set; }

        public Player()
        {
            
        }

        public Player(int playerId, string surname, string firstname, decimal price)
        {
                
        }
    }
}
