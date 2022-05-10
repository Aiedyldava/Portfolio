using DG.Tweening;
//using Facebook.Unity;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOver : Popup
{
	private sealed class _StartCongratEff_c__AnonStorey0
	{
		internal Image image;

		internal void __m__0()
		{
			this.image.gameObject.SetActive(true);
		}

		internal void __m__1()
		{
			UnityEngine.Object.Destroy(this.image.gameObject);
		}
	}

	public GameObject Background;

	public Button ButtonHome;

	public Button ButtonRePlay;

	public Button ButtonX2coin;

	public Button ButtonRate;

	public Button ButtonTheme;

	public Button ButtonShop;

	public Button ButtonSound;

	public Image ShopIcon;

	public Image SoundIcon;

	public Button ButtonShare;

	public Text TextScore;

	public Text TextHightScore;

	public Text TextCoin;

	public Image ImageBoard;

	public Image GemBackground;

	public Image ImageGem;

	public Image ImageMyGem;

	private int _score;

	private int _coin;

	private GameObject _background;

	private static UnityAction __f__am_cache0;

	private static UnityAction __f__am_cache1;

	private static UnityAction __f__am_cache2;

	private static UnityAction __f__am_cache3;

	private void Start()
	{
		this.ButtonSound.onClick.AddListener(delegate
		{
			Preference.Instance.DataGame.IsSound = !Preference.Instance.DataGame.IsSound;
			this.SoundIcon.sprite = Resources.Load<Sprite>((!Preference.Instance.DataGame.IsSound) ? "Images/UI/volume_off" : "Images/UI/volume_on");
		});
		this.ButtonRePlay.onClick.AddListener(delegate
		{
			GameController.ScreenManager.PlayController.RePlay();
			this.Hide();
		});
		this.ButtonHome.onClick.AddListener(delegate
		{
			this.Hide();
			GameController.ScreenManager.OpenStage(ScreenManager.StateGame.MAIN);
		});
		this.ButtonX2coin.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(delegate
			{
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "+20 gem");
				GameController.AudioController.PlayOneShot("Audios/Effect/double_hexa");
				GameController.EffectController.AddGemEffect(this.ImageGem.transform.position, 20, this.ImageMyGem, 0.3f);
			});
		});
		this.ButtonRate.onClick.AddListener(delegate
		{
			Application.OpenURL(BaseController.GameController.LinkGame());
		});
		this.ButtonTheme.onClick.AddListener(delegate
		{
			GameController.DialogManager.DialogThemes.Show();
		});
		this.ButtonShare.onClick.AddListener(delegate
		{
//			FB.ShareLink(new Uri(BaseController.GameController.LinkGame()), "Triangle Candy", "I got " + Preference.Instance.DataGame.HighScore + " score! Beat me if you can!", null, null);
		});
		this.ButtonShop.onClick.AddListener(delegate
		{
			GameController.DialogManager.PopupShop.Show();
		});
	}

	private void Update()
	{
		this.TextCoin.text = Preference.Instance.DataGame.Coin + string.Empty;
	}

	public void Show(int score)
	{
		Preference.Instance.DataGame.NumPlay++;
		this.SoundIcon.sprite = Resources.Load<Sprite>((!Preference.Instance.DataGame.IsSound) ? "Images/UI/volume_off" : "Images/UI/volume_on");
		this.InitTheme();
		this.TextScore.text = string.Empty;
		this._score = score;
		this.TextHightScore.text = Preference.Instance.DataGame.HighScore + string.Empty;
		base.Show();
		GameController.AudioController.PlayOneShot("Audios/Effect/tada");
		//GameController.AnalyticsController.LogEvent(AnalyticsController.GAME_OVER, AnalyticsController.SCORE, score);
	}

	public override void OnShowComplete()
	{
		base.OnShowComplete();
		BaseController.TweenNumber(0, this._score, this.TextScore, 1f);
		GameController.AdsController.ShowVideoAd();
	}

	private void StartCongratEff()
	{
		GameController.AudioController.PlayOneShot("Audios/Effect/tada");
		for (int i = 0; i < 60; i++)
		{
			Image image = base.CreateImage("Images/GamePlay/triangle", base.transform);
			image.color = GameController.ThemeManager.CurrentTheme.GameColors[UnityEngine.Random.Range(0, GameController.ThemeManager.CurrentTheme.GameColors.Length)];
			image.transform.eulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
			image.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.2f, 0.5f);
			image.rectTransform.anchoredPosition = new Vector2(UnityEngine.Random.Range(base.GetComponent<RectTransform>().rect.x, base.GetComponent<RectTransform>().rect.width), base.GetComponent<RectTransform>().rect.y + base.GetComponent<RectTransform>().rect.height + 20f);
			image.gameObject.SetActive(false);
			float duration = UnityEngine.Random.Range(0.5f, 1.5f);
			float delay = UnityEngine.Random.Range(0f, 0.5f);
			image.transform.DORotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), duration, RotateMode.Fast).SetEase(Ease.Linear).SetDelay(delay).OnStart(delegate
			{
				image.gameObject.SetActive(true);
			});
			image.rectTransform.DOAnchorPosY(base.GetComponent<RectTransform>().rect.y - 20f, duration, false).SetDelay(delay).SetEase(Ease.Linear).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(image.gameObject);
			});
		}
	}

	public void InitTheme()
	{
		if (this._background != null)
		{
			UnityEngine.Object.Destroy(this._background);
		}
		this._background = BaseController.InstantiatePrefab(GameController.ThemeManager.CurrentTheme.BackgroundPrefab);
		this._background.transform.SetParent(this.Background.transform, false);
		this.ButtonRate.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundRate;
		this.ButtonRePlay.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundPlay;
		this.ButtonTheme.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundTheme;
		this.ButtonHome.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundMoregame;
		this.ButtonX2coin.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundFreeCoin;
		this.ImageBoard.color = GameController.ThemeManager.CurrentTheme.BackgroundFreeCoin;
		this.GemBackground.color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.ButtonShop.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.ButtonSound.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.TextCoin.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.ShopIcon.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.SoundIcon.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
	}
}
