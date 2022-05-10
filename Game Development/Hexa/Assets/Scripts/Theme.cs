using System;
using System.Collections.Generic;
using UnityEngine;

public class Theme
{
	public string Name;

	public Color[] GameColors;

	public string BackgroundPrefab;

	public Color TriangleColor;

	public Color TextBestColor;

	public Color TextBestScoreColor;

	public Color TextScoreColor;

	public Color IconButtonColor;

	public Color BackgroundButton;

	public Color BackgroundPlay;

	public Color BackgroundMoregame;

	public Color BackgroundRate;

	public Color BackgroundTheme;

	public Color BackgroundFreeCoin;

	public List<string> Patterns = new List<string>();

	public Sprite RandomPattern()
	{
		return Resources.Load<Sprite>("Images/GamePlay/Patterns/" + this.Patterns[UnityEngine.Random.Range(0, this.Patterns.Count)]);
	}
}
