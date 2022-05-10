using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainController : StageController
{
	public Button ButtonPlay;

	public Button ButtonSound;

	public Button ButtonShop;

	public Button ButtonRate;

	public Button ButtonMoregame;

	public Button ButtonTheme;

	public RectTransform RectTransform;

	public Button ButtonWatchAds;

	public Image ImageGem;

	public Button ButtonGem;

	public Image IamgeMyGem;

	public Text TextGem;

	public Text TextHighScore;

	public GameObject Background;

	public Image ImageShop;

	public Image ImageVolume;

	public Image ImageCup;

	private GameObject _background;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private static UnityAction __f__am_cache3;

	private static UnityAction __f__am_cache4;

	private void Start()
	{
		this.ButtonPlay.onClick.AddListener(delegate
		{
			this.Play(false);
		});
		this.ButtonShop.onClick.AddListener(delegate
		{
			GameController.DialogManager.PopupShop.Show();
		});
		this.ButtonGem.onClick.AddListener(delegate
		{
			GameController.DialogManager.PopupShop.Show();
		});
		this.ButtonTheme.onClick.AddListener(delegate
		{
			GameController.DialogManager.DialogThemes.Show();
		});
		this.ButtonWatchAds.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(delegate
			{
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "+20 gem");
				GameController.EffectController.AddGemEffect(this.ImageGem.transform.position, 20, this.IamgeMyGem, -1f);
			});
		});
		this.ButtonMoregame.onClick.AddListener(delegate
		{
			Application.OpenURL(BaseController.GameController.LinkStore());
		});
		this.ButtonRate.onClick.AddListener(delegate
		{
			Application.OpenURL(BaseController.GameController.LinkGame());
		});
		this.TextHighScore.text = Preference.Instance.DataGame.HighScore + string.Empty;
		this.ImageVolume.sprite = Resources.Load<Sprite>((!Preference.Instance.DataGame.IsSound) ? "Images/UI/volume_off" : "Images/UI/volume_on");
		this.ButtonSound.onClick.AddListener(delegate
		{
			Preference.Instance.DataGame.IsSound = !Preference.Instance.DataGame.IsSound;
			this.ImageVolume.sprite = Resources.Load<Sprite>((!Preference.Instance.DataGame.IsSound) ? "Images/UI/volume_off" : "Images/UI/volume_on");
		});
		this.InitTheme();
	}

	private void Update()
	{
		this.TextGem.text = Preference.Instance.DataGame.Coin + string.Empty;
	}

	private void Play(bool timeMode)
	{
		GameController.ScreenManager.OpenStage(ScreenManager.StateGame.PLAY);
		base.OnStageOpen();
	}

	public override void OnStageOpen()
	{
		base.OnStageOpen();
		GameController.AdsController.SetBannerShow(false);
	}

	public void InitTheme()
	{
		if (this._background != null)
		{
			UnityEngine.Object.Destroy(this._background);
		}
		this._background = BaseController.InstantiatePrefab(GameController.ThemeManager.CurrentTheme.BackgroundPrefab);
		this._background.transform.SetParent(this.Background.transform, false);
		this.ButtonPlay.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundPlay;
		this.ButtonMoregame.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundMoregame;
		this.ButtonRate.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundRate;
		this.ButtonTheme.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundTheme;
		this.ButtonWatchAds.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundFreeCoin;
		this.ButtonSound.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.ButtonShop.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.ButtonGem.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.ImageShop.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.ImageVolume.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.TextGem.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.ImageCup.color = GameController.ThemeManager.CurrentTheme.TextBestColor;
		this.TextHighScore.color = GameController.ThemeManager.CurrentTheme.TextBestScoreColor;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			GameController.AdsController.IsShowInter = false;
		}
	}
}
