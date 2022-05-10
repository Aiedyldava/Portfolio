using System;
using System.Collections.Generic;

public class PlayData
{
	public bool IsPlay;

	public int NumDiamond;

	public List<TriangleInfo> ListTriangle = new List<TriangleInfo>();

	public int CurrentScore;

	public List<MiniHexaInfo> ListMiniHexa = new List<MiniHexaInfo>();

	public bool NeedLoad()
	{
		return this.IsPlay && this.CurrentScore != 0;
	}

	public void Reset()
	{
		this.CurrentScore = 0;
		this.ListTriangle.Clear();
		this.ListMiniHexa.Clear();
	}
}
