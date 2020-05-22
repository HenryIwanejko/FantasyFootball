using System;

public class Positions
{
	public int PositionID { get; set; }

	public string PositionName { get; set; }

	public Positions()
	{
	}

    public Positions(int positionId, string positionName)
    {
		PositionID = positionId;
		PositionName = positionName;
    }
}
