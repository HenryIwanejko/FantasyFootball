using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Attributes;

namespace FantasyFootballSQLDB
{
	[Table("Positions")]
	public class Position
	{
		[PrimaryKey, AutoIncrement]
		public int PositionID { get; set; }

		public string PositionName { get; set; }

		public Position()
		{
		}

		public Position(int positionId, string positionName)
		{
			PositionID = positionId;
			PositionName = positionName;
		}
	}
}

