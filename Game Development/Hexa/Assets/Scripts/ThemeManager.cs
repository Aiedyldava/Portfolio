using System;
using UnityEngine;

public class ThemeManager : BaseController
{
	public ThemeInfo ThemeInfo;

	private GameObject _background;

	public Theme CurrentTheme
	{
		get
		{
			return ThemeInfo.Themes[Preference.Instance.DataGame.ThemeId];
		}
	}

	private void Start()
	{
		this.ThemeInfo = new ThemeInfo();
	}

	private void Update()
	{
	}

	public void InitTheme()
	{
		if (GameController.ScreenManager.currentStage is PlayController)
		{
			GameController.ScreenManager.PlayController.InitTheme();
		}
		if (GameController.ScreenManager.currentStage is MainController)
		{
			GameController.ScreenManager.MainController.InitTheme();
		}
		if (GameController.DialogManager._gameOver != null)
		{
			GameController.DialogManager._gameOver.InitTheme();
		}
		if (GameController.DialogManager._popupPause != null)
		{
			GameController.DialogManager._popupPause.InitTheme();
		}
	}
}
