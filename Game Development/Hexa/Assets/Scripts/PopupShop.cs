using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupShop : Popup
{
	public Image GemBackground;

	public Text TextGem;

	public Image ImageMyGem;

	public Button ButtonClose;

	public Image CloseIcon;

	public Button ButtonWatchAds;

	public Image ImageGem;

	public GameObject Background;

	public Text TextAnyPurchase;

	public Button ButtonRemoveAds;

	public Button ButtonRestore;

	public Button Product1000;

	public Button Product5000;

	public GameObject GoLoad;

	public Image ImageLoad;

	private Sequence _sequences;

	private GameObject _background;

	private static UnityAction __f__am_cache0;

	public override void Show()
	{
		this.InitTheme();
		base.Show();
		if (Preference.Instance.DataGame.NoAds)
		{
			this.ButtonRemoveAds.gameObject.SetActive(false);
			this.ButtonRestore.gameObject.SetActive(false);
			this.TextAnyPurchase.gameObject.SetActive(false);
		}
		else
		{
			this.ButtonRemoveAds.gameObject.SetActive(true);
			this.ButtonRestore.gameObject.SetActive(true);
			this.TextAnyPurchase.gameObject.SetActive(true);
		}
	}

	private void Start()
	{
		this.GoLoad.gameObject.SetActive(false);
		this.ImageLoad.transform.DORotate(new Vector3(0f, 0f, -360f), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
		this.ButtonClose.onClick.AddListener(new UnityAction(this.Hide));
		if (this._sequences != null)
		{
			this._sequences.Kill(false);
		}
		this.ButtonRemoveAds.onClick.AddListener(delegate
		{
			this.GoLoad.gameObject.SetActive(true);
			this._sequences = DOTween.Sequence().AppendInterval(5f).AppendCallback(delegate
			{
				this.GoLoad.gameObject.SetActive(false);
			});
			GameController.PurchaseController.BuyProductID(PurchaseController.ProductRemoveAd);
		});
		this.Product1000.onClick.AddListener(delegate
		{
			this.GoLoad.gameObject.SetActive(true);
			this._sequences = DOTween.Sequence().AppendInterval(5f).AppendCallback(delegate
			{
				this.GoLoad.gameObject.SetActive(false);
			});
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin1);
		});
		this.Product5000.onClick.AddListener(delegate
		{
			this.GoLoad.gameObject.SetActive(true);
			this._sequences = DOTween.Sequence().AppendInterval(5f).AppendCallback(delegate
			{
				this.GoLoad.gameObject.SetActive(false);
			});
			GameController.PurchaseController.BuyProductID(PurchaseController.PackageCoin2);
		});
		//this.ButtonRestore.onClick.AddListener(delegate
		//{
		//	GameController.PurchaseController.RestorePurchases();
		//});
		this.ButtonWatchAds.onClick.AddListener(delegate
		{
			GameController.AdsController.ShowAd(delegate
			{
				//GameController.AnalyticsController.LogEvent(AnalyticsController.WATCH_ADS, AnalyticsController.WATCH_ADS_TYPE, "+20 gem");
				GameController.EffectController.AddGemEffect(this.ImageGem.transform.position, 20, this.ImageMyGem, -1f);
			});
		});
	}

	private void Update()
	{
		this.TextGem.text = Preference.Instance.DataGame.Coin + string.Empty;
	}

	public void InitTheme()
	{
		if (this._background != null)
		{
			UnityEngine.Object.Destroy(this._background);
		}
		this._background = BaseController.InstantiatePrefab(GameController.ThemeManager.CurrentTheme.BackgroundPrefab);
		this._background.transform.SetParent(this.Background.transform, false);
		this.ButtonRemoveAds.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundRate;
		this.Product1000.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundMoregame;
		this.Product5000.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundMoregame;
		this.ButtonRestore.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundFreeCoin;
		this.TextAnyPurchase.color = GameController.ThemeManager.CurrentTheme.BackgroundRate;
		this.ButtonClose.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.GemBackground.color = GameController.ThemeManager.CurrentTheme.BackgroundButton;
		this.CloseIcon.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.TextGem.color = GameController.ThemeManager.CurrentTheme.IconButtonColor;
		this.ButtonWatchAds.GetComponent<Image>().color = GameController.ThemeManager.CurrentTheme.BackgroundFreeCoin;
	}
}
