using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPause : Popup
{
	public Button ButtonMain;

	public Button ButtonRestart;

	public Button ButtonResume;

	public Button ButtonThemes;

	public Button ButtonSound;

	public Image ImageVolume;

	private void Start()
	{
		this.ButtonResume.onClick.AddListener(new UnityAction(this.Hide));
		this.ButtonRestart.onClick.AddListener(delegate
		{
			GameController.ScreenManager.PlayController.RePlay();
			this.Hide();
			GameController.AdsController.ShowVideoAd();
		});
		this.ButtonThemes.onClick.AddListener(delegate
		{
			this.Hide();
			GameController.DialogManager.DialogThemes.Show();
		});
		this.ButtonMain.onClick.AddListener(delegate
		{
			this.Hide();
			GameController.ScreenManager.OpenStage(ScreenManager.StateGame.MAIN);
		});
		this.ButtonSound.onClick.AddListener(delegate
		{
			Preference.Instance.DataGame.IsSound = !Preference.Instance.DataGame.IsSound;
			this.ImageVolume.sprite = Resources.Load<Sprite>((!Preference.Instance.DataGame.IsSound) ? "Images/UI/volume_off" : "Images/UI/volume_on");
		});
	}

	public override void Show()
	{
		this.ImageVolume.sprite = Resources.Load<Sprite>((!Preference.Instance.DataGame.IsSound) ? "Images/UI/volume_off" : "Images/UI/volume_on");
		this.InitTheme();
		base.Show();
		GameController.AdsController.SetBanner2Show(true);
	}

	public override void Hide()
	{
		base.Hide();
		GameController.AdsController.SetBanner2Show(false);
	}

	public void InitTheme()
	{
		this.ButtonMain.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundRate;
		this.ButtonResume.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundPlay;
		this.ButtonRestart.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundMoregame;
		this.ButtonThemes.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundTheme;
	}

	private void Update()
	{
	}
}
