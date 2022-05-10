using System;
using System.Collections.Generic;

[Serializable]
public class DataGame
{
	public int DestroyPrice;

	public int NumPlay;

	public int Combo5Line;

	public int ThemeId;

	public bool NoAds;

	public bool IsSound;

	public int Coin;

	public bool FirstOpen;

	public bool FirstMulti;

	public List<bool> DailyRewardStatus;

	public int LastDay;

	public PlayData PlayData = new PlayData();

	public int HighScore;

	public ThemeStatus[] ThemeStatus;

	public DataGame()
	{
		this.NoAds = false;
		this.ThemeId = 1;
		this.IsSound = true;
		this.FirstOpen = true;
		this.FirstMulti = true;
		this.DailyRewardStatus = new List<bool>();
		this.ThemeStatus = new ThemeStatus[12];
		for (int i = 0; i < this.ThemeStatus.Length; i++)
		{
			this.ThemeStatus[i] = new ThemeStatus();
		}
		this.ThemeStatus[0].IsOpen = true;
		this.ThemeStatus[1].IsOpen = true;
		this.ThemeStatus[2].IsOpen = true;
		this.DestroyPrice = 10;
		this.Coin = 0;
	}
}
